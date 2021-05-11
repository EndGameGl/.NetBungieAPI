﻿using NetBungieAPI.Models.Destiny.Definitions.Common;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace NetBungieAPI.Models.Destiny.Definitions.Vendors
{
    public sealed record DestinyVendorDisplayPropertiesDefinition : DestinyDisplayPropertiesDefinition,
        IDeepEquatable<DestinyVendorDisplayPropertiesDefinition>
    {
        /// <summary>
        /// I regret calling this a "large icon". It's more like a medium-sized image with a picture of the vendor's mug on it, trying their best to look cool. Not what one would call an icon.
        /// </summary>
        [JsonPropertyName("largeIcon")]
        public string LargeIcon { get; init; }

        /// <summary>
        /// This is apparently the "Watermark". I am not certain offhand where this is actually used in the Game UI, but some people may find it useful.
        /// </summary>
        [JsonPropertyName("largeTransparentIcon")]
        public string LargeTransparentIcon { get; init; }

        /// <summary>
        /// This is the icon used in the map overview, when the vendor is located on the map.
        /// </summary>
        [JsonPropertyName("mapIcon")]
        public string MapIcon { get; init; }

        /// <summary>
        /// If we replaced the icon with something more glitzy, this is the original icon that the vendor had according to the game's content. It may be more lame and/or have less razzle-dazzle. But who am I to tell you which icon to use.
        /// </summary>
        [JsonPropertyName("originalIcon")]
        public string OriginalIcon { get; init; }

        /// <summary>
        /// Vendors, in addition to expected display property data, may also show some "common requirements" as statically defined definition data. This might be when a vendor accepts a single type of currency, or when the currency is unique to the vendor and the designers wanted to show that currency when you interact with the vendor.
        /// </summary>
        [JsonPropertyName("requirementsDisplay")]
        public ReadOnlyCollection<DestinyVendorRequirementDisplayEntryDefinition> RequirementsDisplay { get; init; } =
            Defaults.EmptyReadOnlyCollection<DestinyVendorRequirementDisplayEntryDefinition>();

        /// <summary>
        /// This is the icon used in parts of the game UI such as the vendor's waypoint.
        /// </summary>
        [JsonPropertyName("smallTransparentIcon")]
        public string SmallTransparentIcon { get; init; }

        [JsonPropertyName("subtitle")] public string Subtitle { get; init; }

        public bool DeepEquals(DestinyVendorDisplayPropertiesDefinition other)
        {
            return other != null &&
                   Description == other.Description &&
                   HasIcon == other.HasIcon &&
                   Icon == other.Icon &&
                   Name == other.Name &&
                   HighResolutionIcon == other.HighResolutionIcon &&
                   IconSequences.DeepEqualsReadOnlyCollections(other.IconSequences) &&
                   LargeIcon == other.LargeIcon &&
                   LargeTransparentIcon == other.LargeTransparentIcon &&
                   MapIcon == other.MapIcon &&
                   OriginalIcon == other.OriginalIcon &&
                   RequirementsDisplay.DeepEqualsReadOnlyCollections(other.RequirementsDisplay) &&
                   SmallTransparentIcon == other.SmallTransparentIcon &&
                   Subtitle == other.Subtitle;
        }
    }
}