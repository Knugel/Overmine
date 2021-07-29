using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Overmine.API.Assets;
using Thor;

namespace Overmine.API.Extensions
{
    public static class GameDataExtensions
    {
        public static void RegisterAll(this GameData data, ModResources resources)
        {
            data.RegisterItems(resources);
            data.RegisterStatusEffects(resources);
            data.RegisterFamiliars(resources);
            data.RegisterLootTables(resources);
            
            data.RegisterPopups(resources);
        }

        public static void RegisterItems(this GameData data, ModResources resources)
        {
            var items = resources.LoadAllAssets<ItemData>();
            var defaultUnlocked = data.GetField<List<string>>("mDefaultUnlocked");
            var defaultDiscovered = data.GetField<List<string>>("mDefaultDiscovered");
            
            foreach (var item in items)
            {
                data.Items.Add(item);
                
                if((item.Hint & ItemData.ItemHint.Relic) != 0)
                    data.RelicCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Curse) != 0)
                    data.CurseCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Blessing) != 0)
                    data.BlessingCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Familiar) != 0)
                    data.FamiliarCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Misc) != 0)
                    data.MiscCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Potion) != 0)
                    data.PotionCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Health) != 0)
                    data.HexCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Resource) != 0)
                    data.ResourceItemCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Artifact) != 0)
                    data.ArtifactCollection.Add(item);
                if((item.Hint & ItemData.ItemHint.Skin) != 0)
                    data.SkinCollection.Add(item);
                
                if(item.IsDefault)
                    defaultUnlocked.Add(item.guid);
                if(item.IsDefaultDiscovered)
                    defaultDiscovered.Add(item.guid);
            }
        }

        public static void RegisterStatusEffects(this GameData data, ModResources resources)
        {
            var statusEffects = resources.LoadAllObjectsWithComponent<StatusEffectExt>();
            foreach (var effect in statusEffects)
            {
                data.StatusEffects.Add(effect);
            }
        }

        public static void RegisterFamiliars(this GameData data, ModResources resources)
        {
            var petExtensions = resources.LoadAllObjectsWithComponent<PetExt>();
            foreach (var extension in petExtensions)
            {
                data.Familiars.Add(extension);
            }
        }
        
        public static void RegisterLootTables(this GameData data, ModResources resources)
        {
            var lootTables = resources.LoadAllAssets<LootTable>();
            foreach (var table in lootTables)
            {
                var orig = data.LootTables.FirstOrDefault(x => x.guid == table.guid);
                if(orig == null)
                    data.LootTables.Add(table);
                else
                {
                    if (table.Replace)
                        orig.Loot.Clear();
                    orig.Loot.AddRange(table.Loot);
                }
            }
        }

        public static void RegisterPopups(this GameData data, ModResources resources)
        {
            var popups = resources.LoadAllAssets<Popup>();
            data.Popups.AddRange(popups);
        }

        private static T GetField<T>(this GameData data, string name)
        {
            var field = typeof(GameData).GetField(name,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return (T) field.GetValue(data);
        }
    }
}