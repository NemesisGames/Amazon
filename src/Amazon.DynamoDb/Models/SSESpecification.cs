﻿namespace Amazon.DynamoDb
{
    public sealed class SSESpecification
    {
        public bool? Enabled { get; set; }

        public string? KMSMasterKeyId { get; set; }

        public SSEType? SSEType { get; set; }
    }
}
