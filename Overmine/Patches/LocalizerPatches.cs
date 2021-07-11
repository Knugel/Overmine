using System.Collections.Generic;
using HarmonyLib;
using Overmine.API.Assets;
using Thor;

namespace Overmine.Patches
{
    public class LocalizerPatches
    {
        private static Dictionary<Localizer.LanguageType, Dictionary<string, string>> Localizations = new Dictionary<Localizer.LanguageType, Dictionary<string, string>>();

        [HarmonyPatch(typeof(Localizer), "Load")]
        [HarmonyPostfix]
        public static void OnLoad()
        {
            var dbs = Overmine.Instance.Loader.Resources.LoadAllAssets<LocDB>();
            foreach (var db in dbs)
            {
                Localizations.TryGetValue(db.Language, out var localizations);
                if (localizations == null)
                    localizations = new Dictionary<string, string>();

                for (var i = 0; i < db.Keys.Length; i++)
                {
                    var key = db.Keys[i];
                    var text = db.Texts[i];
                    localizations.Add(key, text);
                }

                Localizations[db.Language] = localizations;
            }
        }
        
        [HarmonyPatch(typeof(Localizer), "GetLocString")]
        [HarmonyPrefix]
        public static bool OnGetLocString(LocID locID, ref string __result)
        {
            var innerText = locID.Text.Value;
            if (string.IsNullOrEmpty(innerText))
                return true;

            if (!Localizations.TryGetValue(Localizer.Instance.Language, out var language)) return true;
            if (!language.ContainsKey(innerText)) return true;

            __result = language[innerText];
            return false;
        }

        [HarmonyPatch(typeof(Localizer), "FormatLocString")]
        [HarmonyPrefix]
        public static bool OnFormatLocString(LocID locID, ref string __result, params object[] args)
        {
            var innerText = locID.Text.Value;
            if (string.IsNullOrEmpty(innerText))
                return true;

            if (!Localizations.TryGetValue(Localizer.Instance.Language, out var language)) return true;
            if (!language.ContainsKey(innerText)) return true;
            
            __result = string.Format(language[innerText], args);
            return false;
        }
    }
}