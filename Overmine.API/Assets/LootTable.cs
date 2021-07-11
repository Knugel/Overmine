using Thor;
using UnityEngine;

namespace Overmine.API.Assets
{
    [CreateAssetMenu(fileName = "Loot Table", menuName = "Data/Loot Table")]
    public class LootTable : LootTableData
    {
        public bool Replace;
    }
}