﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NetBungieAPI.Logging;
using NetBungieAPI.Models;
using NetBungieAPI.Models.Destiny;
using NetBungieAPI.Models.Destiny.Definitions.HistoricalStats;
using NetBungieAPI.Services.Interfaces;

namespace NetBungieAPI.Repositories
{
    /// <summary>
    ///     Repository class for storing and accessing all classes with <see cref="Attributes.DestinyDefinitionAttribute" />
    ///     attribute
    /// </summary>
    public class DestinyDefinitionsRepository
    {
        private readonly IDefinitionAssemblyData _assemblyData;
        private readonly IConfigurationService _config;

        private readonly ConcurrentDictionary<DefinitionsEnum, DestinyDefinitionTypeRepository> _definitionRepositories;
        private readonly ConcurrentDictionary<string, DestinyHistoricalStatsDefinition> _historicalStatsDefinitions;
        private readonly ILogger _logger;

        /// <summary>
        ///     Class .ctor
        /// </summary>
        /// <param name="locale">Locale for this repository</param>
        internal DestinyDefinitionsRepository(
            BungieLocales locale,
            IDefinitionAssemblyData assemblyData,
            IConfigurationService configuration,
            ILogger logger)
        {
            _assemblyData = assemblyData;
            _config = configuration;
            _logger = logger;
            Locale = locale;

            var definitionsLoaded = _config.Settings.DefinitionLoadingSettings.AllowedDefinitions.Length;
            var concurrencyLevel = _config.Settings.DefinitionLoadingSettings.AppConcurrencyLevel;

            _logger.Log(
                $"Initializing definitions repository with settings: Locale: {Locale}, Concurrency level: {concurrencyLevel}, Capacity: {definitionsLoaded}",
                LogType.Debug);

            _definitionRepositories = new ConcurrentDictionary<DefinitionsEnum, DestinyDefinitionTypeRepository>(
                concurrencyLevel,
                definitionsLoaded);
            foreach (var definition in configuration.Settings.DefinitionLoadingSettings.AllowedDefinitions)
                _definitionRepositories.TryAdd(definition,
                    new DestinyDefinitionTypeRepository(
                        _assemblyData.DefinitionsToTypeMapping[definition].DefinitionType,
                        concurrencyLevel));

            _historicalStatsDefinitions =
                new ConcurrentDictionary<string, DestinyHistoricalStatsDefinition>(concurrencyLevel, 31);
        }

        /// <summary>
        ///     Locale of this repository
        /// </summary>
        public BungieLocales Locale { get; }

        public IEnumerable<DestinyHistoricalStatsDefinition> GetAllHistoricalStats =>
            _historicalStatsDefinitions.Select(x => x.Value);

        /// <summary>
        ///     Adds definition from repository, if possible
        /// </summary>
        /// <param name="definitionType"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        public bool AddDefinition(DefinitionsEnum definitionType, IDestinyDefinition definition)
        {
            return _definitionRepositories.TryGetValue(definitionType, out var repository) &&
                   repository.Add(definition);
        }

        public bool AddDestinyHistoricalStatsDefinition(DestinyHistoricalStatsDefinition definition)
        {
            return _historicalStatsDefinitions.TryAdd(definition.StatId, definition);
        }

        /// <summary>
        ///     Removes definition from repository, if possible
        /// </summary>
        /// <param name="definitionType"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool RemoveDefinition(DefinitionsEnum definitionType, uint hash)
        {
            return _definitionRepositories.TryGetValue(definitionType, out var repository) && repository.Remove(hash);
        }

        public bool TryGetHistoricalStatsDefinition(string name, out DestinyHistoricalStatsDefinition val)
        {
            return _historicalStatsDefinitions.TryGetValue(name, out val);
        }

        /// <summary>
        ///     Gets definition from repository, if possible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hash"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        public bool TryGetDefinition<T>(DefinitionsEnum enumValue, uint hash, out T definition)
            where T : IDestinyDefinition
        {
            definition = default;
            if (_definitionRepositories.TryGetValue(enumValue, out var repository))
            {
                if (repository.TryGetDefinition<T>(hash, out var value))
                {
                    definition = value;
                    return definition != null;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        ///     Gets definition from repository, if possible
        /// </summary>
        /// <param name="definitionType"></param>
        /// <param name="hash"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        public bool TryGetDefinition(DefinitionsEnum definitionType, uint hash, out IDestinyDefinition definition)
        {
            definition = default;
            if (_definitionRepositories.TryGetValue(definitionType, out var repository))
            {
                if (repository.TryGetDefinition(hash, out definition))
                    return definition != null;
                return false;
            }

            return false;
        }

        /// <summary>
        ///     Searches through repository with given predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Search<T>(DefinitionsEnum definitionType, Func<IDestinyDefinition, bool> predicate)
            where T : IDestinyDefinition
        {
            return _definitionRepositories.TryGetValue(definitionType, out var repository)
                ? repository.Where(predicate).Cast<T>()
                : null;
        }

        public IEnumerable<T> GetAll<T>(DefinitionsEnum definitionType)
        {
            return _definitionRepositories.TryGetValue(definitionType, out var repository)
                ? repository.EnumerateValues().Cast<T>()
                : null;
        }

        internal void PremapPointers()
        {
            foreach (var repository in _definitionRepositories.Select(x => x.Value)) repository.MapValues();
        }
    }
}