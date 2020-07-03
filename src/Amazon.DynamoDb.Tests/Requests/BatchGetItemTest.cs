﻿using System.Text;
using System.Text.Json;
using Carbon.Data;

using Xunit;

namespace Amazon.DynamoDb.Tests
{
    public class BatchGetItemRequestTests
    {
        [Fact]
        public void BatchGetItemRequestTest1()
        {
            var request = new BatchGetItemRequest(
                new TableKeys("Table1", new RecordKey("id", 1), new RecordKey("id", 2)),
                new TableKeys("Table2", new RecordKey("id", 3), new RecordKey("id", 4))
            );

            var expected = @"{""RequestItems"":{""Table1"":{""Keys"":[{""id"":{""N"":""1""}},{""id"":{""N"":""2""}}]},""Table2"":{""Keys"":[{""id"":{""N"":""3""}},{""id"":{""N"":""4""}}]}}}";

            Assert.Equal(expected, request.ToJson().ToString(pretty: false));

            Assert.Equal(expected, Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(request.ToJson())));          
        }
    }
}