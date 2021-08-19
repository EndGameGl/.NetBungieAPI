﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using NetBungieAPI.Attributes;
using NetBungieAPI.Models.Destiny.Definitions.Vendors;

namespace NetBungieAPI.Models.Destiny.Definitions.Locations
{
    /// <summary>
    ///     A "Location" is a sort of shortcut for referring to a specific combination of Activity, Destination, Place, and
    ///     even Bubble or NavPoint within a space.
    /// </summary>
    [DestinyDefinition(DefinitionsEnum.DestinyLocationDefinition)]
    public sealed record DestinyLocationDefinition : IDestinyDefinition, IDeepEquatable<DestinyLocationDefinition>
    {
        /// <summary>
        ///     If the location has a Vendor on it, this is the Vendor.
        /// </summary>
        [JsonPropertyName("vendorHash")]
        public DefinitionHashPointer<DestinyVendorDefinition> Vendor { get; init; } =
            DefinitionHashPointer<DestinyVendorDefinition>.Empty;

        /// <summary>
        ///     A Location may refer to different specific spots in the world based on the world's current state. This is a list of
        ///     those potential spots, and the data we can use at runtime to determine which one of the spots is the currently
        ///     valid one.
        /// </summary>
        [JsonPropertyName("locationReleases")]
        public ReadOnlyCollection<DestinyLocationReleaseDefinition> LocationReleases { get; init; } =
            Defaults.EmptyReadOnlyCollection<DestinyLocationReleaseDefinition>();

        public bool DeepEquals(DestinyLocationDefinition other)
        {
            return other != null &&
                   LocationReleases.DeepEqualsReadOnlyCollections(other.LocationReleases) &&
                   Vendor.DeepEquals(other.Vendor) &&
                   Blacklisted == other.Blacklisted &&
                   Hash == other.Hash &&
                   Index == other.Index &&
                   Redacted == other.Redacted;
        }

        public DefinitionsEnum DefinitionEnumValue => DefinitionsEnum.DestinyLocationDefinition;
        [JsonPropertyName("blacklisted")] public bool Blacklisted { get; init; }
        [JsonPropertyName("hash")] public uint Hash { get; init; }
        [JsonPropertyName("index")] public int Index { get; init; }
        [JsonPropertyName("redacted")] public bool Redacted { get; init; }

        public void MapValues()
        {
            Vendor.TryMapValue();
            foreach (var locationRelease in LocationReleases)
            {
                locationRelease.Activity.TryMapValue();
                locationRelease.ActivityGraph.TryMapValue();
                locationRelease.Destination.TryMapValue();
            }
        }

        public void SetPointerLocales(BungieLocales locale)
        {
            Vendor.SetLocale(locale);
            foreach (var locationRelease in LocationReleases)
            {
                locationRelease.Activity.SetLocale(locale);
                locationRelease.ActivityGraph.SetLocale(locale);
                locationRelease.Destination.SetLocale(locale);
            }
        }

        public override string ToString()
        {
            return
                $"{Hash} ({LocationReleases.Count} location releases.) {LocationReleases.LastOrDefault()?.DisplayProperties.Name}";
        }
    }
}