﻿using Amazon.DynamoDb.Models;
using System;
using System.Text.Json;

namespace Amazon.DynamoDb
{
    public sealed class TableResult
    {
        public TableResult(TableDescription tableDescription)
        {
            TableDescription = tableDescription;
        }

        public TableDescription TableDescription { get; set; }

        public static TableResult FromJsonElement(JsonElement el)
        {
            foreach (var property in el.EnumerateObject())
            {
                if (property.NameEquals("TableName"))
                {
                    result.TableName = JsonSerializer.Deserialize<TableDescription>(property)
                }
            }

            throw new ArgumentException("Json is missing required TableName element");
        }
    }
}

/*
{
   "TableDescription": { 
      "ArchivalSummary": { 
         "ArchivalBackupArn": "string",
         "ArchivalDateTime": number,
         "ArchivalReason": "string"
      },
      "AttributeDefinitions": [
         { 
            "AttributeName": "string",
            "AttributeType": "string"
         }
      ],
      "BillingModeSummary": { 
         "BillingMode": "string",
         "LastUpdateToPayPerRequestDateTime": number
      },
      "CreationDateTime": number,
      "GlobalSecondaryIndexes": [
         { 
            "Backfilling": boolean,
            "IndexArn": "string",
            "IndexName": "string",
            "IndexSizeBytes": number,
            "IndexStatus": "string",
            "ItemCount": number,
            "KeySchema": [
               { 
                  "AttributeName": "string",
                  "KeyType": "string"
               }
            ],
            "Projection": { 
               "NonKeyAttributes": [ "string" ],
               "ProjectionType": "string"
            },
            "ProvisionedThroughput": { 
               "LastDecreaseDateTime": number,
               "LastIncreaseDateTime": number,
               "NumberOfDecreasesToday": number,
               "ReadCapacityUnits": number,
               "WriteCapacityUnits": number
            }
         }
      ],
      "GlobalTableVersion": "string",
      "ItemCount": number,
      "KeySchema": [
         { 
            "AttributeName": "string",
            "KeyType": "string"
         }
      ],
      "LatestStreamArn": "string",
      "LatestStreamLabel": "string",
      "LocalSecondaryIndexes": [
         { 
            "IndexArn": "string",
            "IndexName": "string",
            "IndexSizeBytes": number,
            "ItemCount": number,
            "KeySchema": [
               { 
                  "AttributeName": "string",
                  "KeyType": "string"
               }
            ],
            "Projection": { 
               "NonKeyAttributes": [ "string" ],
               "ProjectionType": "string"
            }
         }
      ],
      "ProvisionedThroughput": { 
         "LastDecreaseDateTime": number,
         "LastIncreaseDateTime": number,
         "NumberOfDecreasesToday": number,
         "ReadCapacityUnits": number,
         "WriteCapacityUnits": number
      },
      "Replicas": [
         { 
            "GlobalSecondaryIndexes": [
               { 
                  "IndexName": "string",
                  "ProvisionedThroughputOverride": { 
                     "ReadCapacityUnits": number
                  }
               }
            ],
            "KMSMasterKeyId": "string",
            "ProvisionedThroughputOverride": { 
               "ReadCapacityUnits": number
            },
            "RegionName": "string",
            "ReplicaStatus": "string",
            "ReplicaStatusDescription": "string",
            "ReplicaStatusPercentProgress": "string"
         }
      ],
      "RestoreSummary": { 
         "RestoreDateTime": number,
         "RestoreInProgress": boolean,
         "SourceBackupArn": "string",
         "SourceTableArn": "string"
      },
      "SSEDescription": { 
         "InaccessibleEncryptionDateTime": number,
         "KMSMasterKeyArn": "string",
         "SSEType": "string",
         "Status": "string"
      },
      "StreamSpecification": { 
         "StreamEnabled": boolean,
         "StreamViewType": "string"
      },
      "TableArn": "string",
      "TableId": "string",
      "TableName": "string",
      "TableSizeBytes": number,
      "TableStatus": "string"
   }
}
*/