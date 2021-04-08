﻿using NetBungieAPI.Destiny.Definitions.InventoryItems;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetBungieAPI.Destiny.Profile.Components.Contracts
{
    public class DestinyHistoricalWeaponStats
    {
        public DefinitionHashPointer<DestinyInventoryItemDefinition> ItemReference { get; init; }
        public ReadOnlyDictionary<string, DestinyHistoricalStatsValue> Values { get; init; }

        [JsonConstructor]
        internal DestinyHistoricalWeaponStats(uint referenceId, Dictionary<string, DestinyHistoricalStatsValue> values)
        {
            ItemReference = new DefinitionHashPointer<DestinyInventoryItemDefinition>(referenceId, DefinitionsEnum.DestinyInventoryItemDefinition);
            Values = values.AsReadOnlyDictionaryOrEmpty();
        }
    }
}
