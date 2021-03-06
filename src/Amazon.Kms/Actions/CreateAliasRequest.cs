﻿namespace Amazon.Kms
{
    public sealed class CreateAliasRequest : KmsRequest
    {
        public CreateAliasRequest(string targetKeyId, string aliasName)
        {
            TargetKeyId = targetKeyId;
            AliasName   = aliasName;
        }

        public string TargetKeyId { get; }

        public string AliasName { get; }
    }    
}