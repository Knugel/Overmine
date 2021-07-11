namespace Overmine.API
{
    public abstract class Mod
    {
        public ModResources Resources { get; }

        public Mod() { }
        
        public Mod(ModResources resources): this()
        {
            Resources = resources;
        }
    }
}