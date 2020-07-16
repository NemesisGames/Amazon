﻿using Amazon.DynamoDb.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Amazon.DynamoDb
{
    public class TableDescription
    {
        public TableDescription() { }

        public ArchivalSummary? ArchivalSummary { get; set; }
        public AttributeDefinition[]? AttributeDefinitions { get; set; }
        public BillingModeSummary? BillingModeSummary { get; set; }
        public DateTimeOffset CreationDateTime { get; set; }
        public GlobalSecondaryIndexDescription[]? GlobalSecondaryIndexes { get; set; }
        public string? GlobalTableVersion { get; set; }
        public long ItemCount { get; set; }
        public KeySchemaElement[]? KeySchema { get; set; }
        public string? LatestStreamArn { get; set; }
        public string? LatestStreamLabel { get; set; }
        public LocalSecondaryIndexDescription[]? LocalSecondaryIndexes { get; set; }
        public ProvisionedThroughput? ProvisionedThroughput { get; set; }
        public ReplicaDescription[]? Replicas { get; set; }
        public RestoreSummary[]? RestoreSummary { get; set;}
        public SSEDescription? SSEDescription { get; set; }
        public StreamSpecification? StreamSpecification { get; set; }
        public string? TableArn { get; set; }
        public string? TableId { get; set; }
        public string? TableName { get; set; }
        public long TableSizeBytes { get; set; }
        public TableStatus TableStatus { get; set; }

    }
}
