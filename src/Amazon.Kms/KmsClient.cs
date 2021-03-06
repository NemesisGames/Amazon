﻿using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amazon.Kms
{
    public sealed class KmsClient : AwsClient
    {
        public const string Version = "2014-11-01";

        public KmsClient(AwsRegion region, IAwsCredential credential)
            : base(AwsService.Kms, region, credential)
        { }

        #region Aliases

        public Task<CreateAliasResponse> CreateAliasAsync(CreateAliasRequest request)
        {
            return SendAsync<CreateAliasRequest, CreateAliasResponse>("CreateAlias", request);
        }

        #endregion

        #region Grants

        public Task<CreateGrantResponse> CreateGrantAsync(CreateGrantRequest request)
        {
            return SendAsync<CreateGrantRequest, CreateGrantResponse>("CreateGrant", request);
        }

        public Task<RetireGrantResponse> RetireGrantAsync(RetireGrantRequest request)
        {
            return SendAsync<RetireGrantRequest, RetireGrantResponse>("RetireGrant", request);
        }

        public Task<ListGrantsResponse> ListGrantsAsync(ListGrantsRequest request)
        {
            return SendAsync<ListGrantsRequest, ListGrantsResponse>("ListGrants", request);
        }

        #endregion

        public Task<EncryptResponse> EncryptAsync(EncryptRequest request)
        {
            return SendAsync<EncryptRequest, EncryptResponse>("Encrypt", request);
        }

        public Task<DecryptResponse> DecryptAsync(DecryptRequest request)
        {
            return SendAsync<DecryptRequest, DecryptResponse>("Decrypt", request);
        }

        #region Data Keys

        public Task<GenerateDataKeyResponse> GenerateDataKeyAsync(GenerateDataKeyRequest request)
        {
            return SendAsync<GenerateDataKeyRequest, GenerateDataKeyResponse>("GenerateDataKey", request);
        }

        public Task<GenerateDataKeyResponse> GenerateDataKeyWithoutPlaintextAsync(GenerateDataKeyRequest request)
        {
            return SendAsync<GenerateDataKeyRequest, GenerateDataKeyResponse>("GenerateDataKeyWithoutPlaintext", request);
        }

        #endregion

        #region Helpers

        private static readonly JsonSerializerOptions jsoIgnoreNullValues = new JsonSerializerOptions { IgnoreNullValues = true };

        private async Task<TResult> SendAsync<TRequest, TResult>(string action, TRequest request)
            where TRequest  : KmsRequest
            where TResult : KmsResponse
        {
            byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(request, jsoIgnoreNullValues);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint) {
                Headers = {
                    { "x-amz-target", "TrentService." + action }
                },
                Content = new ByteArrayContent(jsonBytes) {
                    Headers = {
                        { "Content-Type", "application/x-amz-json-1.1" }
                    }
                }
            };

            string responseText = await SendAsync(httpRequest).ConfigureAwait(false);

            if (responseText.Length == 0) return null!;

            return JsonSerializer.Deserialize<TResult>(responseText);
        }

        protected override async Task<Exception> GetExceptionAsync(HttpResponseMessage response)
        {
            string responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (responseText.Length > 0 && responseText[0] == '{')
            {
                var error = JsonSerializer.Deserialize<KmsError>(responseText);

                return error.Type switch {
                    "AccessDeniedException"       => new AccessDeniedException(error.Message),
                    "ServiceUnavailableException" => new ServiceUnavailableException(error.Message),
                    "KeyUnavailableException"     => new KeyUnavailableException(error.Message),
                    _                             => new KmsException(error, response.StatusCode)
                };
            }
            else
            {
                throw new Exception(responseText);
            }
        }

        #endregion
    }
}

// http://docs.aws.amazon.com/kms/latest/APIReference/Welcome.html