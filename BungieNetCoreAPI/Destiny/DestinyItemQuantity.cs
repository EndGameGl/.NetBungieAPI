﻿using NetBungieAPI.Destiny.Definitions.InventoryItems;
using Newtonsoft.Json;

namespace NetBungieAPI.Destiny
{
    public class DestinyItemQuantity : IDeepEquatable<DestinyItemQuantity>
    {
        public DefinitionHashPointer<DestinyInventoryItemDefinition> Item { get; }
        public long? ItemInstanceId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        internal DestinyItemQuantity(uint itemHash, long? itemInstanceId, int quantity)
        {
            Item = new DefinitionHashPointer<DestinyInventoryItemDefinition>(itemHash);
            ItemInstanceId = itemInstanceId;
            Quantity = quantity;
        }

        public bool DeepEquals(DestinyItemQuantity other)
        {
            return other != null &&
                   Item.DeepEquals(other.Item) &&
                   ItemInstanceId == other.ItemInstanceId &&
                   Quantity == other.Quantity;
        }
    }
}
