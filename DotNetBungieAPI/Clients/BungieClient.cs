﻿using System.Threading.Tasks;
using DotNetBungieAPI.Models;
using DotNetBungieAPI.Models.Destiny;
using DotNetBungieAPI.Models.Destiny.Definitions.HistoricalStats;
using DotNetBungieAPI.Service.Abstractions;

namespace DotNetBungieAPI.Clients;

/// <summary>
///     <see cref="IBungieClient" /> implementation
/// </summary>
internal sealed class BungieClient : IBungieClient
{
    internal static IBungieClient Instance { get; set; }
    private readonly IBungieClientConfiguration _configuration;

    public BungieClient(
        IBungieApiAccess apiAccess,
        IAuthorizationHandler authorizationHandler,
        IDestiny2DefinitionRepository repository,
        IDefinitionProvider definitionProvider,
        IDestiny2ResetService destiny2ResetService,
        IBungieClientConfiguration configuration,
        IDotNetBungieApiHttpClient dotNetBungieApiHttpClient,
        IBungieNetJsonSerializer bungieNetJsonSerializer)
    {
        ResetService = destiny2ResetService;
        _configuration = configuration;
        Authorization = authorizationHandler;
        Repository = repository;
        ApiAccess = apiAccess;
        DefinitionProvider = definitionProvider;
        Instance = this;
        ApiHttpClient = dotNetBungieApiHttpClient;
        Serializer = bungieNetJsonSerializer;
    }

    /// <summary>
    ///     <inheritdoc cref="IBungieClient.Authorization" />
    /// </summary>
    public IAuthorizationHandler Authorization { get; }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public IBungieNetJsonSerializer Serializer { get; }

    /// <summary>
    ///     <inheritdoc />
    /// </summary>
    public IDotNetBungieApiHttpClient ApiHttpClient { get; }

    /// <summary>
    ///     <inheritdoc cref="IBungieClient.Repository" />
    /// </summary>
    public IDestiny2DefinitionRepository Repository { get; }

    /// <summary>
    ///     <inheritdoc cref="IBungieClient.ApiAccess" />
    /// </summary>
    public IBungieApiAccess ApiAccess { get; }

    public IDefinitionProvider DefinitionProvider { get; }

    public IDestiny2ResetService ResetService { get; }


    /// <summary>
    ///     <inheritdoc cref="IBungieClient.TryGetDefinitionAsync{T}" />
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="locale"></param>
    /// <param name="success"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async ValueTask<bool> TryGetDefinitionAsync<T>(uint hash, BungieLocales locale, Action<T> success)
        where T : IDestinyDefinition
    {
        if (Repository.TryGetDestinyDefinition<T>(hash, locale, out var definition))
        {
            success(definition);
            return true;
        }

        definition = await DefinitionProvider.LoadDefinition<T>(hash, locale);
        if (definition is null)
            return false;
        if (_configuration.CacheDefinitions)
            Repository.AddDefinition(locale, definition);
        success(definition);
        return true;
    }

    public bool TryGetDefinition<T>(uint hash, BungieLocales locale, out T definition) where T : IDestinyDefinition
    {
        if (Repository.TryGetDestinyDefinition(hash, locale, out definition)) return true;

        var defTask = DefinitionProvider.LoadDefinition<T>(hash, locale);
        definition = defTask.GetAwaiter().GetResult();
        if (!defTask.IsCompleted)
            throw new Exception("ValueTask faulted to get result.");
        if (definition is null)
            return false;
        if (_configuration.CacheDefinitions)
            Repository.AddDefinition(locale, definition);
        return true;
    }

    public async ValueTask<bool> TryGetHistoricalStatDefinitionAsync(string key, BungieLocales locale,
        Action<DestinyHistoricalStatsDefinition> success)
    {
        if (Repository.TryGetDestinyHistoricalDefinition(locale, key, out var definition))
        {
            success(definition);
            return true;
        }

        definition = await DefinitionProvider.LoadHistoricalStatsDefinition(key, locale);
        if (definition is null)
            return false;
        if (_configuration.CacheDefinitions)
            Repository.AddDestinyHistoricalDefinition(locale, definition);
        success(definition);
        return true;
    }

    public bool TryGetHistoricalStatDefinition(string key, BungieLocales locale,
        out DestinyHistoricalStatsDefinition definition)
    {
        if (Repository.TryGetDestinyHistoricalDefinition(locale, key, out definition)) return true;

        var getterTask = DefinitionProvider.LoadHistoricalStatsDefinition(key, locale);
        definition = getterTask.GetAwaiter().GetResult();

        if (!getterTask.IsCompleted)
            throw new Exception("ValueTask faulted to get result.");

        if (definition is null)
            return false;
        if (_configuration.CacheDefinitions)
            Repository.AddDestinyHistoricalDefinition(locale, definition);
        return true;
    }

    public void Dispose()
    {
        DefinitionProvider.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await DefinitionProvider.DisposeAsync();
    }
}