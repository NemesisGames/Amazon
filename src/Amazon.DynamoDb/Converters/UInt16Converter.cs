using Carbon.Data;

namespace Amazon.DynamoDb
{
    internal sealed class UInt16Converter : IDbValueConverter
    {
        public DbValue FromObject(object value, IMember member) => new DbValue((ushort)value);

        public object ToObject(DbValue item, IMember member) => item.ToUInt16();
    }
}