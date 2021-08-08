using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Overmine.API.Assets
{
    [CreateAssetMenu(fileName = "AudioDB", menuName = "Data/AudioDB")]
    public class AudioDB : ScriptableObject, ISerializationCallbackReceiver
    {
        public List<Entry> Entries;
        
        [HideInInspector]
        public string[] Keys;

        [HideInInspector]
        public AudioClip[] Clips;
        
        [Serializable]
        public class Entry
        {
            public string Key;

            public AudioClip Clip;
        }

        public void OnBeforeSerialize()
        {
            Keys = Entries?.Select(x => x.Key).ToArray();
            Clips = Entries?.Select(x => x.Clip).ToArray();
        }

        public void OnAfterDeserialize() { }

        public IEnumerable<Entry> GetEntries()
        {
            return Keys.Select((value, index) => new Entry {Key = value, Clip = Clips[index]}).ToList();
        }
    }
}