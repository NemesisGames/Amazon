using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Amazon.Kinesis.Firehose
{
    public sealed class KinesisFirehoseClient : AwsClient
    {
        const string Version = "20150804";
        const string TargetPrefix = "Firehose_" + Version;

        private static readonly JsonSerializerSettings serializationOptions;

        static KinesisFirehoseClient()
        {
            serializationOptions = new JsonSerializerSettings 
            { 
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        public KinesisFirehoseClient(AwsRegion region, IAwsCredential credential)
            : base(AwsService.KinesisFirehose, region, credential) { }
        
        public DeliveryStream GetStream(string name)
        {
            return new DeliveryStream(name, this);
        }

        public Task<CreateDeliveryStreamResult> CreateDeliveryStreamAsync(CreateDeliveryStreamRequest request)
        {
            return SendAsync<CreateDeliveryStreamRequest, CreateDeliveryStreamResult>("CreateDeliveryStream", request);
        }

        public Task<DeleteDeliveryStreamResult> DeleteDeliveryStreamAsync(DeleteDeliveryStreamRequest request)
        {
            return SendAsync<DeleteDeliveryStreamRequest, DeleteDeliveryStreamResult>("PutRecordBatch", request);
        }

        public Task<DescribeDeliveryStreamResult> DescribeDeliveryStreamAsync(DescribeDeliveryStreamRequest request)
        {
            return SendAsync<DescribeDeliveryStreamRequest, DescribeDeliveryStreamResult>("DescribeDeliveryStream", request);
        }

        public Task<ListDeliveryStreamsRequest> ListDeliveryStreamsAsync(ListDeliveryStreamsRequest request)
        {
            return SendAsync<ListDeliveryStreamsRequest, ListDeliveryStreamsRequest>("ListDeliveryStreams", request);
        }

        public Task<PutRecordResult> PutRecordAsync(PutRecordRequest request)
        {
            return SendAsync<PutRecordRequest, PutRecordResult>("PutRecord", request);
        }

        public Task<PutRecordBatchResult> PutRecordBatchAsync(PutRecordBatchRequest request)
        {
            return SendAsync<PutRecordBatchRequest, PutRecordBatchResult>("PutRecordBatch", request);
        }

        // public void UpdateDestinationAsync(UpdateDestinationRequest request) { }

        #region Helpers

        private async Task<TResult> SendAsync<TRequest, TResult>(string action, TRequest request)
            where TResult : notnull, new()
        {
            var httpRequest = GetRequestMessage<TRequest>(action, request);

            var responseText = await SendAsync(httpRequest).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<TResult>(responseText, serializationOptions);
        }

        protected override async Task<Exception> GetExceptionAsync(HttpResponseMessage response)
        {
            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // var result = JsonObject.Parse(responseText);

            throw new Exception(responseText);
        }


        private HttpRequestMessage GetRequestMessage<T>(string action, T request)
        {
            string json;
            if (typeof(T) == typeof(PutRecordBatchRequest))
            {
                json = ConvertBatchRequestToJsonManually(request);
            }
            else
            {
                json = JsonConvert.SerializeObject(request, serializationOptions);
            }

            return new HttpRequestMessage(HttpMethod.Post, Endpoint)
            {
                Headers = {
                    { "x-amz-target", TargetPrefix  + "." + action }
                },
                Content = new StringContent(json, Encoding.UTF8, "application/x-amz-json-1.1")
            };
        }

        public static string ConvertBatchRequestToJsonManually<T>(T request)
        {
            string json;
            var batchRequest = request as PutRecordBatchRequest;

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append(@"{""DeliveryStreamName"":""");
            jsonBuilder.Append(batchRequest.DeliveryStreamName);
            jsonBuilder.Append(@""",""Records"":[");
            for (int i = 0; i < batchRequest.Records.Length; i++)
            {
                var record = batchRequest.Records[i];

                if (i > 0)
                {
                    jsonBuilder.Append(",");
                }
                jsonBuilder.Append(@"{""Data"":""");
                jsonBuilder.Append(Convert.ToBase64String(record.Data));
                jsonBuilder.Append(@"""}");
            }
            jsonBuilder.Append("]}");
            json = jsonBuilder.ToString();
            return json;
        }

        #endregion
    }
}
