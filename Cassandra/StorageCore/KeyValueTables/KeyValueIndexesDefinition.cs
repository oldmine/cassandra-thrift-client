using GroboSerializer;

namespace StorageCore.KeyValueTables
{
    public abstract class KeyValueIndexesDefinition : SearchQueryIndexesDefinition<KeyToValue>
    {
        protected KeyValueIndexesDefinition(ISerializer serializer)
            : base(serializer)
        {
        }
    }
}