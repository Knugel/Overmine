using Thor;
using UnityEngine;

namespace Overmine.API.Assets.References
{
    [CreateAssetMenu(fileName = "Loot Table Reference", menuName = "References/Loot Table")]
    public class LootTableReference : LootTableData, IDataObjectReference
    {
        public string Guid => guid;
    }
}