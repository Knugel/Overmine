using System;
using System.Collections.Generic;

namespace Overmine.API
{
    [Serializable]
    public class ModInfo
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Version { get; set; }
        
        public string EntryDLL { get; set; }
        
        public IEnumerable<string> Bundles { get; set; }
    }
}