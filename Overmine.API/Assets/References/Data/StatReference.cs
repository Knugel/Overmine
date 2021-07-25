using Thor;
using UnityEngine;

namespace Overmine.API.Assets.References
{
    [CreateAssetMenu(fileName = "Stat Reference", menuName = "References/Stat")]
    public class StatReference : StatData, IDataObjectReference
    {
        public string Guid => guid;
    }
}