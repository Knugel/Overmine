using Overmine.API.Assets.References;
using Thor;
using UnityEngine;

namespace Overmine.API.Assets
{
    [CreateAssetMenu(fileName = "Loot Table", menuName = "Data/Loot Table")]
    public class LootTable : LootTableData, IDataObjectReference
    {
        public bool Replace;
        
        public string Guid => guid;
    }
}