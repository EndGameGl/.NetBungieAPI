﻿using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NetBungieAPI.Authorization;
using NetBungieAPI.Clients;
using NetBungieAPI.Exceptions;
using NetBungieAPI.Models;
using NetBungieAPI.Models.Applications;
using NetBungieAPI.Services.ApiAccess.Interfaces;
using NetBungieAPI.Services.Interfaces;

namespace NetBungieAPI.Services.ApiAccess
{
    public class AppMethodsAccess : IAppMethodsAccess
    {
        private readonly BungieClientConfiguration _configuration;
        private readonly IDotNetBungieApiHttpClient _dotNetBungieApiHttpClient;

        internal AppMethodsAccess(
            IDotNetBungieApiHttpClient dotNetBungieApiHttpClient, 
            BungieClientConfiguration configuration)
        {
            _dotNetBungieApiHttpClient = dotNetBungieApiHttpClient;
            _configuration = configuration;
        }

        public async ValueTask<BungieResponse<Application[]>> GetBungieApplications(
            CancellationToken cancellationToken = default)
        {
            return await _dotNetBungieApiHttpClient
                .GetFromBungieNetPlatform<Application[]>("/App/FirstParty/", cancellationToken)
                .ConfigureAwait(false);
        }

        public async ValueTask<BungieResponse<ApiUsage>> GetApplicationApiUsage(
            AuthorizationTokenData authorizationToken,
            int applicationId,
            DateTime? start = null,
            DateTime? end = null,
            CancellationToken cancellationToken = default)
        {
            if (!_configuration.HasSufficientRights(ApplicationScopes.ReadUserData))
                throw new InsufficientScopeException(ApplicationScopes.ReadUserData);
            if (start.HasValue && end.HasValue && (end.Value - start.Value).TotalHours > 48)
                throw new Exception("Can't request more than 48 hours.");
            end ??= DateTime.Now;
            start ??= end.Value.AddHours(-24);
            var url = StringBuilderPool
                .GetBuilder(cancellationToken)
                .Append("/App/ApiUsage/")
                .AddQueryParam("start",
                    start.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture))
                .AddQueryParam("end",
                    end.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture))
                .Build();

            return await _dotNetBungieApiHttpClient
                .GetFromBungieNetPlatform<ApiUsage>(url, cancellationToken, authorizationToken.AccessToken)
                .ConfigureAwait(false);
        }
    }
}