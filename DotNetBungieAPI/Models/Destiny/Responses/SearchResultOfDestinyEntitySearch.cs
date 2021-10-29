﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DotNetBungieAPI.Defaults;
using DotNetBungieAPI.Models.Queries;

namespace DotNetBungieAPI.Models.Destiny.Responses
{
    public sealed record SearchResultOfDestinyEntitySearch : SearchResultBase
    {
        [JsonPropertyName("results")]
        public ReadOnlyCollection<DestinyEntitySearchResultItem> Results { get; init; } =
            ReadOnlyCollections<DestinyEntitySearchResultItem>.Empty;
    }
}