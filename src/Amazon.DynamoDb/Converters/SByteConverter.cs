using Carbon.Data;

namespace Amazon.DynamoDb
{
    internal sealed class SByteConverter : IDbValueConverter
    {
        public DbValue FromObject(object value, IMember member) => new DbValue((sbyte)value);

        public object ToObject(DbValue item, IMember member) => item.ToSByte();
    }
}