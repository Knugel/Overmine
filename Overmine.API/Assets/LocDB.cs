using System;
using System.Collections.Generic;
using System.Linq;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets
{
    [CreateAssetMenu(fileName = "LocDB", menuName = "Data/LocDB")]
    public class LocDB : ScriptableObject, ISerializationCallbackReceiver
    {
        public Localizer.LanguageType Language;
        
        public List<Entry> Entries;

        [HideInInspector]
        public string[] Keys;

        [HideInInspector]
        public string[] Texts;

        [Serializable]
        public class Entry
        {
            public string Key;

            public string Text;
        }

        public void OnBeforeSerialize()
        {
            Keys = Entries?.Select(x => x.Key).ToArray();
            Texts = Entries?.Select(x => x.Text).ToArray();
        }

        public void OnAfterDeserialize() { }
    }
}