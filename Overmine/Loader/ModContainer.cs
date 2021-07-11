using System.Reflection;
using Overmine.API;

namespace Overmine.Loader
{
    public class ModContainer
    {
        public ModResources Resources { get; internal set; }

        public Assembly Assembly { get; internal set; }
        
        public ModInfo Info { get; internal set; }
        
        public Mod Instance { get; internal set; }
    }
}