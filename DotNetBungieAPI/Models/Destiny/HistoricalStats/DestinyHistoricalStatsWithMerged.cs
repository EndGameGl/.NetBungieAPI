﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DotNetBungieAPI.Defaults;

namespace DotNetBungieAPI.Models.Destiny.HistoricalStats
{
    public class DestinyHistoricalStatsWithMerged
    {
        [JsonPropertyName("results")]
        public ReadOnlyDictionary<string, DestinyHistoricalStatsByPeriod> Results { get; init; } =
            ReadOnlyDictionaries<string, DestinyHistoricalStatsByPeriod>.Empty;

        [JsonPropertyName("merged")] public DestinyHistoricalStatsByPeriod Merged { get; init; }
    }
}