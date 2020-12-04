﻿using BungieNetCoreAPI.Attributes;
using BungieNetCoreAPI.Destiny.Definitions.BreakerTypes;
using BungieNetCoreAPI.Destiny.Definitions.Classes;
using BungieNetCoreAPI.Destiny.Definitions.Collectibles;
using BungieNetCoreAPI.Destiny.Definitions.DamageTypes;
using BungieNetCoreAPI.Destiny.Definitions.ItemCategories;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BungieNetCoreAPI.Destiny.Definitions.InventoryItems
{
    [DestinyDefinition("DestinyInventoryItemDefinition")]
    public class DestinyInventoryItemDefinition : DestinyDefinition
    {
        public uint AcquireRewardSiteHash { get; }
        public uint AcquireUnlockHash { get; }
        public InventoryItemAction Action { get; }
        public bool AllowActions { get; }
        public DestinyColor BackgroundColor { get; }
        public DestinyBreakerTypes BreakerType { get; }
        public DestinyClassType ClassType { get; }
        public DefinitionHashPointer<DestinyCollectibleDefinition> Collectible { get; }
        public DamageTypes.DamageType DefaultDamageType { get; }
        public DestinyDefinitionDisplayProperties DisplayProperties { get; }
        public string DisplaySource { get; }
        public bool DoesPostmasterPullHaveSideEffects { get; }
        public bool Equippable { get; }
        public InventoryItemEquippingBlock EquippingBlock { get; }
        public string IconWatermark { get; }
        public string IconWatermarkShelved { get; }
        public InventoryItemInventoryBlock Inventory { get; }
        public List<InventoryItemInvestmentStat> InvestmentStats { get; }
        public bool IsWrapper { get; }
        public List<DefinitionHashPointer<DestinyItemCategoryDefinition>> ItemCategories { get; }
        public int ItemSubType { get; }
        public int ItemType { get; }
        public string ItemTypeAndTierDisplayName { get; }
        public string ItemTypeDisplayName { get; }
        public bool NonTransferrable { get; }
        public InventoryItemObjectivesBlock Objectives { get; }
        public List<InventoryItemPerk> Perks { get; }
        public string SecondaryIcon { get; }
        public InventoryItemSetDataBlock SetData { get; }
        public InventoryItemPlugBlock Plug { get; }
        public InventoryItemPreviewBlock Preview { get; }
        public InventoryItemQualityBlock Quality { get; }
        public string Screenshot { get; }
        public InventoryItemSocketsBlock Sockets { get; }
        public int SpecialItemType { get; }
        public InventoryItemStatsBlock Stats { get; }
        public DefinitionHashPointer<DestinyInventoryItemDefinition> SummaryItem { get; }
        public InventoryItemTalentGrid TalentGrid { get; }
        public List<InventoryItemTooltipNotification> TooltipNotifications { get; }
        public List<string> TraitIds { get; }
        public InventoryItemTranslationBlock TranslationBlock { get; }
        public string UiItemDisplayStyle { get; }
        public InventoryItemValueBlock Value { get; }

        [JsonConstructor]
        private DestinyInventoryItemDefinition(uint acquireRewardSiteHash, uint acquireUnlockHash, bool allowActions, DestinyColor backgroundColor, InventoryItemAction action,
            DestinyBreakerTypes breakerType, DestinyClassType classType, DestinyDefinitionDisplayProperties displayProperties, DamageTypes.DamageType defaultDamageType, string displaySource,
            bool doesPostmasterPullHaveSideEffects, bool equippable, InventoryItemEquippingBlock equippingBlock, string iconWatermark, string iconWatermarkShelved,
            InventoryItemInventoryBlock inventory, List<InventoryItemInvestmentStat> investmentStats, bool isWrapper, List<uint> itemCategoryHashes, int itemSubType,
            int itemType, string itemTypeAndTierDisplayName, string itemTypeDisplayName, bool nonTransferrable, List<InventoryItemPerk> perks, InventoryItemPreviewBlock preview,
            InventoryItemQualityBlock quality, string screenshot, InventoryItemSocketsBlock sockets, int specialItemType, InventoryItemStatsBlock stats, uint summaryItemHash,
            InventoryItemTalentGrid talentGrid, List<InventoryItemTooltipNotification> tooltipNotifications, List<string> traitIds, InventoryItemTranslationBlock translationBlock,
            string uiItemDisplayStyle, uint collectibleHash, InventoryItemPlugBlock plug, InventoryItemObjectivesBlock objectives, string secondaryIcon, InventoryItemValueBlock value,
            InventoryItemSetDataBlock setData,
            bool blacklisted, uint hash, int index, bool redacted)
            : base(blacklisted, hash, index, redacted)
        {
            AcquireRewardSiteHash = acquireRewardSiteHash;
            AcquireUnlockHash = acquireUnlockHash;
            AllowActions = allowActions;
            BackgroundColor = backgroundColor;
            Action = action;
            DisplayProperties = displayProperties;
            BreakerType = breakerType;
            ClassType = classType;
            Collectible = new DefinitionHashPointer<DestinyCollectibleDefinition>(collectibleHash, "DestinyCollectibleDefinition");
            DefaultDamageType = defaultDamageType;
            DisplaySource = displaySource;
            DoesPostmasterPullHaveSideEffects = doesPostmasterPullHaveSideEffects;
            Equippable = equippable;
            EquippingBlock = equippingBlock;
            IconWatermark = iconWatermark;
            IconWatermarkShelved = iconWatermarkShelved;
            Inventory = inventory;
            InvestmentStats = investmentStats;
            IsWrapper = isWrapper;
            ItemCategories = new List<DefinitionHashPointer<DestinyItemCategoryDefinition>>();
            if (itemCategoryHashes != null)
            {
                foreach (var itemCategoryHash in itemCategoryHashes)
                {
                    ItemCategories.Add(new DefinitionHashPointer<DestinyItemCategoryDefinition>(itemCategoryHash, "DestinyItemCategoryDefinition"));
                }
            }
            ItemSubType = itemSubType;
            ItemType = itemType;
            ItemTypeAndTierDisplayName = itemTypeAndTierDisplayName;
            ItemTypeDisplayName = itemTypeDisplayName;
            NonTransferrable = nonTransferrable;
            Objectives = objectives;
            Perks = perks;
            Plug = plug;
            Preview = preview;
            Quality = quality;
            Screenshot = screenshot;
            Sockets = sockets;
            SpecialItemType = specialItemType;
            Stats = stats;
            SummaryItem = new DefinitionHashPointer<DestinyInventoryItemDefinition>(summaryItemHash, "DestinyInventoryItemDefinition");
            TalentGrid = talentGrid;
            TooltipNotifications = tooltipNotifications;
            TraitIds = traitIds;
            TranslationBlock = translationBlock;
            UiItemDisplayStyle = uiItemDisplayStyle;
            SecondaryIcon = secondaryIcon;
            Value = value;
            SetData = setData;
        }

        public override string ToString()
        {
            return $"{Hash} {DisplayProperties.Name}: {DisplayProperties.Description}";
        }
    }
}
