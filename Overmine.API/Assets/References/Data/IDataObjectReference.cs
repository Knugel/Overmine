namespace Overmine.API.Assets.References
{
    public interface IDataObjectReference : IAssetReference
    {
        string Guid { get; }
    }
}