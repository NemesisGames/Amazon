using Carbon.Data;

namespace Amazon.DynamoDb
{
    internal sealed class ByteConverter : IDbValueConverter
    {
        public DbValue FromObject(object value, IMember member) => new DbValue((byte)value);

        public object ToObject(DbValue item, IMember member) => item.ToByte();
    }
}