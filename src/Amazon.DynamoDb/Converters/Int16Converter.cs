using Carbon.Data;

namespace Amazon.DynamoDb
{
    internal sealed class Int16Converter : IDbValueConverter
    {
        public DbValue FromObject(object value, IMember member) => new DbValue((short)value);

        public object ToObject(DbValue item, IMember member) => item.ToInt16();
    }
}