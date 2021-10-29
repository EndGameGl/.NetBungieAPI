﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DotNetBungieAPI.Defaults;
using DotNetBungieAPI.Models.Forum;

namespace DotNetBungieAPI.Models.Queries
{
    public sealed record SearchResultOfPostResponse : SearchResultBase
    {
        [JsonPropertyName("results")]
        public ReadOnlyCollection<PostResponse> Results { get; init; } = ReadOnlyCollections<PostResponse>.Empty;
    }
}