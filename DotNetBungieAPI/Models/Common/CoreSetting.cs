﻿namespace DotNetBungieAPI.Models.Common
{
    public sealed record CoreSetting
    {
        [JsonPropertyName("identifier")] public string Identifier { get; init; }

        [JsonPropertyName("isDefault")] public bool IsDefault { get; init; }

        [JsonPropertyName("displayName")] public string DisplayName { get; init; }

        [JsonPropertyName("summary")] public string Summary { get; init; }

        [JsonPropertyName("imagePath")] public BungieNetResource ImagePath { get; init; }

        [JsonPropertyName("childSettings")]
        public ReadOnlyCollection<CoreSetting> ChildSettings { get; init; } = ReadOnlyCollections<CoreSetting>.Empty;

        public override string ToString()
        {
            return $"{Identifier} : {DisplayName} | {Summary}";
        }
    }
}