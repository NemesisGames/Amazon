using Amazon.Kinesis.Firehose;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Text.Json;

using Xunit;

namespace Amazon.Kinesis.Tests
{
    public class PutRecordBatchRequestTests
    {
        [Fact]
        public void Serialize()
        {
            var serializationOptions = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            string record = @"{""stream"":""load-funnel"",""globals"":{""phash"":""S3NSYEB4WF"",""pname"":null,""cty"":""US"",""env"":""Local"",""svc"":""Client"",""ver"":""1"",""sha"":""DEV"",""host"":""DESKTOP-0LLGBT8"",""maj"":2,""min"":1599750396,""locmin"":0,""bhgDvId"":""18b72547-36c1-4b29-9c1c-30e12e877bd9"",""idfa"":null,""idfv"":null,""gps_adid"":null,""adid"":null,""os"":""Windows 10  (10.0.0) 64bit"",""devN"":""DESKTOP-0LLGBT8"",""devV"":""MS-7C75 (Micro-Star International Co., Ltd.)"",""quality"":""HD"",""res"":""1001x527"",""rlm"":0,""trc"":0,""lvl"":0},""ts"":""2020-09-10T21:15:55.4541025Z"",""fun"":""Load"",""stage"":""D"",""context"":""ALA""}";
            string record2 = @"{""stream"":""load-funnel"",""globals"":{""phash"":""S3NSYEB4WF"",""pname"":null,""cty"":""US"",""env"":""Local"",""svc"":""Client"",""ver"":""1"",""sha"":""DEV"",""host"":""DESKTOP-0LLGBT8"",""maj"":2,""min"":1599750396,""locmin"":0,""bhgDvId"":""18b72547-36c1-4b29-9c1c-30e12e877bd9"",""idfa"":null,""idfv"":null,""gps_adid"":null,""adid"":null,""os"":""Windows 10  (10.0.0) 64bit"",""devN"":""DESKTOP-0LLGBT8"",""devV"":""MS-7C75 (Micro-Star International Co., Ltd.)"",""quality"":""HD"",""res"":""1001x527"",""rlm"":0,""trc"":0,""lvl"":0},""ts"":""2020-09-10T21:15:55.4541025Z"",""fun"":""Load"",""stage"":""SU"",""context"":""ALA""}";

            string text = @"{""DeliveryStreamName"":""test-stream"",""Records"":[{""Data"":""eyJzdHJlYW0iOiJsb2FkLWZ1bm5lbCIsImdsb2JhbHMiOnsicGhhc2giOiJTM05TWUVCNFdGIiwicG5hbWUiOm51bGwsImN0eSI6IlVTIiwiZW52IjoiTG9jYWwiLCJzdmMiOiJDbGllbnQiLCJ2ZXIiOiIxIiwic2hhIjoiREVWIiwiaG9zdCI6IkRFU0tUT1AtMExMR0JUOCIsIm1haiI6MiwibWluIjoxNTk5NzUwMzk2LCJsb2NtaW4iOjAsImJoZ0R2SWQiOiIxOGI3MjU0Ny0zNmMxLTRiMjktOWMxYy0zMGUxMmU4NzdiZDkiLCJpZGZhIjpudWxsLCJpZGZ2IjpudWxsLCJncHNfYWRpZCI6bnVsbCwiYWRpZCI6bnVsbCwib3MiOiJXaW5kb3dzIDEwICAoMTAuMC4wKSA2NGJpdCIsImRldk4iOiJERVNLVE9QLTBMTEdCVDgiLCJkZXZWIjoiTVMtN0M3NSAoTWljcm8tU3RhciBJbnRlcm5hdGlvbmFsIENvLiwgTHRkLikiLCJxdWFsaXR5IjoiSEQiLCJyZXMiOiIxMDAxeDUyNyIsInJsbSI6MCwidHJjIjowLCJsdmwiOjB9LCJ0cyI6IjIwMjAtMDktMTBUMjE6MTU6NTUuNDU0MTAyNVoiLCJmdW4iOiJMb2FkIiwic3RhZ2UiOiJEIiwiY29udGV4dCI6IkFMQSJ9""},{""Data"":""eyJzdHJlYW0iOiJsb2FkLWZ1bm5lbCIsImdsb2JhbHMiOnsicGhhc2giOiJTM05TWUVCNFdGIiwicG5hbWUiOm51bGwsImN0eSI6IlVTIiwiZW52IjoiTG9jYWwiLCJzdmMiOiJDbGllbnQiLCJ2ZXIiOiIxIiwic2hhIjoiREVWIiwiaG9zdCI6IkRFU0tUT1AtMExMR0JUOCIsIm1haiI6MiwibWluIjoxNTk5NzUwMzk2LCJsb2NtaW4iOjAsImJoZ0R2SWQiOiIxOGI3MjU0Ny0zNmMxLTRiMjktOWMxYy0zMGUxMmU4NzdiZDkiLCJpZGZhIjpudWxsLCJpZGZ2IjpudWxsLCJncHNfYWRpZCI6bnVsbCwiYWRpZCI6bnVsbCwib3MiOiJXaW5kb3dzIDEwICAoMTAuMC4wKSA2NGJpdCIsImRldk4iOiJERVNLVE9QLTBMTEdCVDgiLCJkZXZWIjoiTVMtN0M3NSAoTWljcm8tU3RhciBJbnRlcm5hdGlvbmFsIENvLiwgTHRkLikiLCJxdWFsaXR5IjoiSEQiLCJyZXMiOiIxMDAxeDUyNyIsInJsbSI6MCwidHJjIjowLCJsdmwiOjB9LCJ0cyI6IjIwMjAtMDktMTBUMjE6MTU6NTUuNDU0MTAyNVoiLCJmdW4iOiJMb2FkIiwic3RhZ2UiOiJTVSIsImNvbnRleHQiOiJBTEEifQ==""}]}";

            var request = new PutRecordBatchRequest("test-stream",
                new Firehose.Record[2]
                {
                    new Firehose.Record(Encoding.UTF8.GetBytes(record)),
                    new Firehose.Record(Encoding.UTF8.GetBytes(record2)),
                });

            var result = JsonConvert.SerializeObject(request, serializationOptions);

            var result2 = KinesisFirehoseClient.ConvertBatchRequestToJsonManually(request);

            Assert.Equal(text, result);
            Assert.Equal(text, result2);
        }
    }
}
