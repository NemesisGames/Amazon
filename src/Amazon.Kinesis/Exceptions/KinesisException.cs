﻿using System;
using System.Net;

using Amazon.Scheduling;

namespace Amazon.Kinesis
{
    public sealed class KinesisException : Exception, IException
	{
		private readonly ErrorResult error;

		public KinesisException(ErrorResult error)
			: base(error.Type ?? error.Text)
		{
			this.error = error;
		}

        public string Type => error.Type;

		public HttpStatusCode StatusCode { get; set; }

		public bool IsTransient
			=> string.Equals(error.Type, "ProvisionedThroughputExceededException", StringComparison.Ordinal)
			|| string.Equals(error.Type, "InternalFailure", StringComparison.Ordinal);
	}
}

/*
 {
	"ErrorCode": "ProvisionedThroughputExceededException",
    "ErrorMessage": "Rate exceeded for shard shardId-000000000001 in stream exampleStreamName under account 111111111111."
 },

*/