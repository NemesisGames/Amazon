using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Amazon.Kinesis.Firehose
{
    public class PutRecordBatchRequest
    {
        public PutRecordBatchRequest(string deliveryStreamName, params Record[] records)
        {
            DeliveryStreamName = deliveryStreamName ?? throw new ArgumentNullException(nameof(deliveryStreamName));
            Records            = records            ?? throw new ArgumentNullException(nameof(records));
        }

        public string DeliveryStreamName { get; }

        public Record[] Records { get; }
    }

    public class PutRecordBatchRequestConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(PutRecordBatchRequest);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new PutRecordBatchRequestConverter();
        }
    }

    public class PutRecordBatchRequestConverter : JsonConverter<PutRecordBatchRequest>
    {
        public override PutRecordBatchRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, PutRecordBatchRequest value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("DeliveryStreamName", value.DeliveryStreamName);
            writer.WriteStartArray("Records");
            foreach (var record in value.Records)
            {
                writer.WriteStartObject();
                writer.WriteBase64String("Data", record.Data);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }

}
