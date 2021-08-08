using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Overmine.API.Assets;
using Thor;
using UnityEngine;

namespace Overmine.Patches
{
    public class AudioExtPatches
    {
        private static Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();
        
        [HarmonyPatch(typeof(AudioExt), "Play")]
        [HarmonyPrefix]
        public static bool PrePlay(ref AudioExt __instance, string name)
        {
            var clip = GetAudioClip(name);
            if (clip == null)
                return true;

            var isMute = Traverse.Create(__instance)
                .Field<bool>("m_mute")
                .Value;
            
            if (isMute)
                return false;

            var source = GetAudioSource(__instance.gameObject);
            source.clip = clip;
            source.Play();
            return false;
        }
        
        [HarmonyPatch(typeof(AudioExt), "PlayOneShot")]
        [HarmonyPrefix]
        public static bool PrePlayOneShot(ref AudioExt __instance, string name)
        {
            var clip = GetAudioClip(name);
            if (clip == null)
                return true;
            
            var isMute = Traverse.Create(__instance)
                .Field<bool>("m_mute")
                .Value;
            
            if (isMute)
                return false;
            
            var source = GetAudioSource(__instance.gameObject);
            source.clip = clip;
            source.PlayOneShot(clip);
            return false;
        }
        
        [HarmonyPatch(typeof(AudioExt), "Stop")]
        [HarmonyPrefix]
        public static bool PreStop(ref AudioExt __instance, string name)
        {
            var clip = GetAudioClip(name);
            if (clip == null)
                return true;
            var source = GetAudioSource(__instance.gameObject);
            source.Stop();
            return false;
        }

        private static AudioClip GetAudioClip(string name)
        {
            if (AudioClips.Count == 0)
                AudioClips = Overmine.Instance.Loader.Resources
                    .LoadAllAssets<AudioDB>()
                    .SelectMany(x => x.GetEntries())
                    .ToDictionary(x => x.Key, x => x.Clip);
            return !AudioClips.TryGetValue(name, out var clip) ? null : clip;
        }

        private static AudioSource GetAudioSource(GameObject go)
        {
            var source = go.GetComponent<AudioSource>();
            return source == null ? go.AddComponent<AudioSource>() : source;
        }
    }
}