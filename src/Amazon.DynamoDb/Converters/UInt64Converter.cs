using Carbon.Data;

namespace Amazon.DynamoDb
{
    internal sealed class UInt64Converter : IDbValueConverter
    {
        public DbValue FromObject(object value, IMember member) => new DbValue((ulong)value);

        public object ToObject(DbValue item, IMember member) => item.ToUInt64();
    }
}