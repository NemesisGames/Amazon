﻿using System.Text.Json.Serialization;

namespace Amazon.DynamoDb
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum KeyType : byte
    {
        HASH,
        RANGE,
    };
}
