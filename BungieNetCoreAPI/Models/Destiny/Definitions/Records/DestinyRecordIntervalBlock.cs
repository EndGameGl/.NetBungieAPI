﻿using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace NetBungieAPI.Models.Destiny.Definitions.Records
{
    public sealed record DestinyRecordIntervalBlock : IDeepEquatable<DestinyRecordIntervalBlock>
    {
        [JsonPropertyName("intervalObjectives")]
        public ReadOnlyCollection<DestinyRecordIntervalObjective> IntervalObjectives { get; init; } = Defaults.EmptyReadOnlyCollection<DestinyRecordIntervalObjective>();
        [JsonPropertyName("intervalRewards")]
        public ReadOnlyCollection<DestinyRecordIntervalRewards> IntervalRewards { get; init; } = Defaults.EmptyReadOnlyCollection<DestinyRecordIntervalRewards>();
        [JsonPropertyName("isIntervalVersionedFromNormalRecord")]
        public bool IsIntervalVersionedFromNormalRecord { get; init; }
        [JsonPropertyName("originalObjectiveArrayInsertionIndex")]
        public int OriginalObjectiveArrayInsertionIndex { get; init; }

        public bool DeepEquals(DestinyRecordIntervalBlock other)
        {
            return other != null &&
                   IntervalObjectives.DeepEqualsReadOnlyCollections(other.IntervalObjectives) &&
                   IntervalRewards.DeepEqualsReadOnlyCollections(other.IntervalRewards) &&
                   IsIntervalVersionedFromNormalRecord == other.IsIntervalVersionedFromNormalRecord &&
                   OriginalObjectiveArrayInsertionIndex == other.OriginalObjectiveArrayInsertionIndex;
        }
    }
}
