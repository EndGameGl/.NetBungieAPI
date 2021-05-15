﻿using System;
using NetBungieAPI.Services.ApiAccess.Interfaces;
using NetBungieAPI.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using NetBungieAPI.Authorization;
using NetBungieAPI.Exceptions;
using NetBungieAPI.Models;
using NetBungieAPI.Models.Applications;
using NetBungieAPI.Models.Tokens;

namespace NetBungieAPI.Services.ApiAccess
{
    public class TokenMethodsAccess : ITokenMethodsAccess
    {
        private readonly IHttpClientInstance _httpClient;
        private readonly IConfigurationService _configuration;

        internal TokenMethodsAccess(IHttpClientInstance httpClient, IConfigurationService configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async ValueTask<BungieResponse<PartnerOfferSkuHistoryResponse[]>> GetPartnerOfferSkuHistory(
            AuthorizationTokenData authData,
            int partnerApplicationId,
            long targetBnetMembershipId,
            CancellationToken token = default)
        {
            if (!_configuration.Settings.IdentificationSettings.ApplicationScopes.HasFlag(ApplicationScopes
                .PartnerOfferGrant))
                throw new InsufficientScopeException(ApplicationScopes.PartnerOfferGrant);

            var url = StringBuilderPool
                .GetBuilder(token)
                .Append("/Tokens/Partner/History/")
                .AddUrlParam(partnerApplicationId.ToString())
                .AddUrlParam(targetBnetMembershipId.ToString())
                .Build();
            return await _httpClient.GetFromBungieNetPlatform<PartnerOfferSkuHistoryResponse[]>(url, token,
                authToken: authData.AccessToken);
        }
    }
}