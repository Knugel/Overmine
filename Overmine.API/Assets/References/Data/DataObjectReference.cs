using Thor;
using UnityEngine;

namespace Overmine.API.Assets.References
{
    [CreateAssetMenu(fileName = "Data Object Reference", menuName = "References/Data Object")]
    public class DataObjectReference : DataObject, IDataObjectReference
    {
        public string Guid => guid;
    }
}