﻿using NetBungieAPI.Attributes;
using NetBungieAPI.Models.Destiny.Definitions.Common;
using System.Text.Json.Serialization;

namespace NetBungieAPI.Models.Destiny.Definitions.ItemTierTypes
{
    /// <summary>
    /// Defines the tier type of an item. Mostly this provides human readable properties for types like Common, Rare, etc...
    /// <para/>
    /// It also provides some base data for infusion that could be useful.
    /// </summary>
    [DestinyDefinition(DefinitionsEnum.DestinyItemTierTypeDefinition)]
    public sealed record DestinyItemTierTypeDefinition : IDestinyDefinition, IDeepEquatable<DestinyItemTierTypeDefinition>
    {
        [JsonPropertyName("displayProperties")]
        public DestinyDisplayPropertiesDefinition DisplayProperties { get; init; }
        [JsonPropertyName("infusionProcess")]
        public DestinyItemTierTypeInfusionBlock InfusionProcess { get; init; }
        [JsonPropertyName("blacklisted")]
        public bool Blacklisted { get; init; }
        [JsonPropertyName("hash")]
        public uint Hash { get; init; }
        [JsonPropertyName("index")]
        public int Index { get; init; }
        [JsonPropertyName("redacted")]
        public bool Redacted { get; init; }

        public override string ToString()
        {
            return $"{Hash} {DisplayProperties.Name}: {DisplayProperties.Description}";
        }

        public bool DeepEquals(DestinyItemTierTypeDefinition other)
        {
            return other != null &&
                   DisplayProperties.DeepEquals(other.DisplayProperties) &&
                   InfusionProcess.DeepEquals(other.InfusionProcess) &&
                   Blacklisted == other.Blacklisted &&
                   Hash == other.Hash &&
                   Index == other.Index &&
                   Redacted == other.Redacted;
        }
        public void MapValues() { return; }
    }
}
