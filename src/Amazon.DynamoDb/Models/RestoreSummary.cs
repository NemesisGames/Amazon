﻿using System;

namespace Amazon.DynamoDb
{
    public sealed class RestoreSummary
    {
        public DateTimeOffset RestoreDateTime { get; set; }

        public bool RestoreInProgress { get; set; }

        public string? SourceBackupArn { get; set; }
    }
}