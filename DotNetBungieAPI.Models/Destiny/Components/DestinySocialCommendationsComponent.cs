﻿using DotNetBungieAPI.Models.Destiny.Definitions.SocialCommendations;

namespace DotNetBungieAPI.Models.Destiny.Components;

public sealed record DestinySocialCommendationsComponent
{
    [JsonPropertyName("totalScore")]
    public int TotalScore { get; init; }

    [JsonPropertyName("scoreDetailValues")]
    public ReadOnlyCollection<int> ScoreDetailValues { get; init; } = ReadOnlyCollections<int>.Empty;

    [JsonPropertyName("commendationNodeScoresByHash")]
    public ReadOnlyDictionary<DefinitionHashPointer<DestinySocialCommendationNodeDefinition>, int> CommendationNodeScoresByHash { get; init; }
        = ReadOnlyDictionaries<DefinitionHashPointer<DestinySocialCommendationNodeDefinition>, int>.Empty;

    [JsonPropertyName("commendationScoresByHash")]
    public ReadOnlyDictionary<DefinitionHashPointer<DestinySocialCommendationDefinition>, int> CommendationScoresByHash { get; init; }
        = ReadOnlyDictionaries<DefinitionHashPointer<DestinySocialCommendationDefinition>, int>.Empty;
}
