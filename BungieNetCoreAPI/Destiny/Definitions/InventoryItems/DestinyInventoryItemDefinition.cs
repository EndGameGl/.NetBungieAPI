﻿using BungieNetCoreAPI.Attributes;
using BungieNetCoreAPI.Destiny.Definitions.BreakerTypes;
using BungieNetCoreAPI.Destiny.Definitions.Classes;
using BungieNetCoreAPI.Destiny.Definitions.Collectibles;
using BungieNetCoreAPI.Destiny.Definitions.DamageTypes;
using BungieNetCoreAPI.Destiny.Definitions.ItemCategories;
using BungieNetCoreAPI.Destiny.Definitions.Lores;
using BungieNetCoreAPI.Destiny.Definitions.Objectives;
using BungieNetCoreAPI.Destiny.Definitions.Seasons;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BungieNetCoreAPI.Destiny.Definitions.InventoryItems
{
    /// <summary>
    /// So much of what you see in Destiny is actually an Item used in a new and creative way. This is the definition for Items in Destiny, which started off as just entities that could exist in your Inventory but ended up being the backing data for so much more: quests, reward previews, slots, and subclasses.
    /// <para/>
    /// In practice, you will want to associate this data with "live" item data from a Bungie.Net Platform call: these definitions describe the item in generic, non-instanced terms: but an actual instance of an item can vary widely from these generic definitions.
    /// </summary>
    [DestinyDefinition(type: DefinitionsEnum.DestinyInventoryItemDefinition, presentInSQLiteDB: true, shouldBeLoaded: true)]
    public class DestinyInventoryItemDefinition : IDestinyDefinition, IDeepEquatable<DestinyInventoryItemDefinition>
    {
        /// <summary>
        /// If this item has a collectible related to it, this is that collectible entry.
        /// </summary>
        public DefinitionHashPointer<DestinyCollectibleDefinition> Collectible { get; }
        /// <summary>
        /// There are times when the game will show you a "summary/vague" version of an item - such as a description of its type represented as a DestinyInventoryItemDefinition - rather than display the item itself.
        /// </summary>
        public DefinitionHashPointer<DestinyInventoryItemDefinition> SummaryItem { get; }
        public ReadOnlyCollection<DefinitionHashPointer<DestinyItemCategoryDefinition>> ItemCategories { get; }
        public uint AcquireRewardSiteHash { get; }
        public uint AcquireUnlockHash { get; }
        public bool AllowActions { get; }
        /// <summary>
        /// Sometimes, an item will have a background color. Most notably this occurs with Emblems, who use the Background Color for small character nameplates such as the "friends" view you see in-game. There are almost certainly other items that have background color as well, though I have not bothered to investigate what items have it nor what purposes they serve: use it as you will.
        /// </summary>
        public DestinyColor BackgroundColor { get; }
        public DestinyBreakerTypes BreakerTypeEnumValue { get; }
        public DefinitionHashPointer<DestinyBreakerTypeDefinition> BreakerType { get; }
        public DestinyClassType ClassType { get; }
        public DamageType DefaultDamageTypeEnumValue { get; }
        public DefinitionHashPointer<DestinyDamageTypeDefinition> DefaultDamageType { get; }
        public ItemSubType ItemSubType { get; }
        public ItemType ItemType { get; }
        public SpecialItemType SpecialItemType { get; }
        public DestinyDefinitionDisplayProperties DisplayProperties { get; }
        /// <summary>
        /// In theory, it is a localized string telling you about how you can find the item. I really wish this was more consistent. Many times, it has nothing. Sometimes, it's instead a more narrative-forward description of the item. Which is cool, and I wish all properties had that data, but it should really be its own property.
        /// </summary>
        public string DisplaySource { get; }
        public bool DoesPostmasterPullHaveSideEffects { get; }
        public bool Equippable { get; }
        /// <summary>
        /// If available, this is the original 'active' release watermark overlay for the icon. If the item has different versions, this can be overridden by the 'display version watermark icon' from the 'quality' block. Alternatively, if there is no watermark for the version, and the item version has a power cap below the current season power cap, this can be overridden by the iconWatermarkShelved property.
        /// </summary>
        public string IconWatermark { get; }
        /// <summary>
        /// If available, this is the 'shelved' release watermark overlay for the icon. If the item version has a power cap below the current season power cap, it can be treated as 'shelved', and should be shown with this 'shelved' watermark overlay.
        /// </summary>
        public string IconWatermarkShelved { get; }
        public bool IsWrapper { get; }
        /// <summary>
        /// It became a common enough pattern in our UI to show Item Type and Tier combined into a single localized string that I'm just going to go ahead and start pre-creating these for items.
        /// </summary>
        public string ItemTypeAndTierDisplayName { get; }
        /// <summary>
        /// The localized title/name of the item's type. This can be whatever the designers want, and has no guarantee of consistency between items.
        /// </summary>
        public string ItemTypeDisplayName { get; }
        /// <summary>
        /// A string identifier that the game's UI uses to determine how the item should be rendered in inventory screens and the like. This could really be anything - at the moment, we don't have the time to really breakdown and maintain all the possible strings this could be, partly because new ones could be added ad hoc. But if you want to use it to dictate your own UI, or look for items with a certain display style, go for it!
        /// </summary>
        public string UiItemDisplayStyle { get; }
        public bool NonTransferrable { get; }
        /// <summary>
        /// A secondary icon associated with the item. Currently this is used in very context specific applications, such as Emblem Nameplates.
        /// </summary>
        public string SecondaryIcon { get; }
        /// <summary>
        /// Pulled from the secondary icon, this is the "secondary background" of the secondary icon. Confusing? Sure, that's why I call it "overlay" here: because as far as it's been used thus far, it has been for an optional overlay image. We'll see if that holds up, but at least for now it explains what this image is a bit better.
        /// </summary>
        public string SecondaryOverlay { get; }
        /// <summary>
        /// Pulled from the Secondary Icon, this is the "special" background for the item. For Emblems, this is the background image used on the Details view: but it need not be limited to that for other types of items.
        /// </summary>
        public string SecondarySpecial { get; }
        /// <summary>
        /// If we were able to acquire an in-game screenshot for the item, the path to that screenshot will be returned here. Note that not all items have screenshots: particularly not any non-equippable items.
        /// </summary>
        public string Screenshot { get; }
        /// <summary>
        /// An identifier that the game UI uses to determine what type of tooltip to show for the item. These have no corresponding definitions that BNet can link to: so it'll be up to you to interpret and display your UI differently according to these styles (or ignore it).
        /// </summary>
        public string TooltipStyle { get; }
        public ReadOnlyCollection<string> TraitIds { get; }
        /// <summary>
        /// If this item can have stats (such as a weapon, armor, or vehicle), this block will be non-null and populated with the stats found on the item.
        /// </summary>
        public InventoryItemStatsBlock Stats { get; }
        /// <summary>
        /// If the item has a Talent Grid, this will be non-null and the properties of the grid defined herein. Note that, while many items still have talent grids, the only ones with meaningful Nodes still on them will be Subclass/"Build" items.
        /// </summary>
        public InventoryItemTalentGrid TalentGrid { get; }
        /// <summary>
        /// If this item can be rendered, this block will be non-null and will be populated with rendering information.
        /// </summary>
        public InventoryItemTranslationBlock TranslationBlock { get; }
        /// <summary>
        /// The conceptual "Value" of an item, if any was defined.
        /// </summary>
        public InventoryItemValueBlock Value { get; }
        /// <summary>
        /// If this item is a quest, this block will be non-null. In practice, I wish I had called this the Quest block, but at the time it wasn't clear to me whether it would end up being used for purposes other than quests. It will contain data about the steps in the quest, and mechanics we can use for displaying and tracking the quest.
        /// </summary>
        public InventoryItemSetDataBlock SetData { get; }
        /// <summary>
        /// If this item *is* a Plug, this will be non-null and the info defined herein.
        /// </summary>
        public InventoryItemPlugBlock Plug { get; }
        /// <summary>
        /// If this item can be Used or Acquired to gain other items (for instance, how Eververse Boxes can be consumed to get items from the box), this block will be non-null and will give summary information for the items that can be acquired.
        /// </summary>
        public InventoryItemPreviewBlock Preview { get; }
        /// <summary>
        /// If this item can have a level or stats, this block will be non-null and will be populated with default quality (item level, "quality", and infusion) data. See the block for more details, there's often less upfront information in D2 so you'll want to be aware of how you use quality and item level on the definition level now.
        /// </summary>
        public InventoryItemQualityBlock Quality { get; }
        /// <summary>
        /// If this item has Objectives (extra tasks that can be accomplished related to the item... most frequently when the item is a Quest Step and the Objectives need to be completed to move on to the next Quest Step), this block will be non-null and the objectives defined herein.
        /// </summary>
        public InventoryItemObjectivesBlock Objectives { get; }
        /// <summary>
        /// If this item can exist in an inventory, this block will be non-null. In practice, every item that currently exists has one of these blocks. But note that it is not necessarily guaranteed.
        /// </summary>
        public InventoryItemInventoryBlock Inventory { get; }
        /// <summary>
        /// If the item can be "used", this block will be non-null, and will have data related to the action performed when using the item. (Guess what? 99% of the time, this action is "dismantle". Shocker)
        /// </summary>
        public InventoryItemAction Action { get; }
        /// <summary>
        /// If this item can be equipped, this block will be non-null and will be populated with the conditions under which it can be equipped.
        /// </summary>
        public InventoryItemEquippingBlock EquippingBlock { get; }
        /// <summary>
        /// If this item has any Sockets, this will be non-null and the individual sockets on the item will be defined herein.
        /// </summary>
        public InventoryItemSocketsBlock Sockets { get; }
        /// <summary>
        /// If the item has stats, this block will be defined. It has the "raw" investment stats for the item. These investment stats don't take into account the ways that the items can spawn, nor do they take into account any Stat Group transformations. I have retained them for debugging purposes, but I do not know how useful people will find them.
        /// </summary>
        public ReadOnlyCollection<InventoryItemInvestmentStat> InvestmentStats { get; }
        /// <summary>
        /// If the item has any *intrinsic* Perks (Perks that it will provide regardless of Sockets, Talent Grid, and other transitory state), they will be defined here.
        /// </summary>
        public ReadOnlyCollection<InventoryItemPerk> Perks { get; }
        /// <summary>
        /// Tooltips that only come up conditionally for the item. Check the live data DestinyItemComponent.tooltipNotificationIndexes property for which of these should be shown at runtime.
        /// </summary>
        public ReadOnlyCollection<InventoryItemTooltipNotification> TooltipNotifications { get; }
        /// <summary>
        /// If this item is a "reward sack" that can be opened to provide other items, this will be non-null and the properties of the sack contained herein.
        /// </summary>
        public InventoryItemSackBlock Sack { get; }
        /// <summary>
        /// If this item has related items in a "Gear Set", this will be non-null and the relationships defined herein.
        /// </summary>
        public InventoryItemGearsetBlock Gearset { get; }
        /// <summary>
        /// If the item is an emblem that has a special Objective attached to it - for instance, if the emblem tracks PVP Kills, or what-have-you. This is a bit different from, for example, the Vanguard Kill Tracker mod, which pipes data into the "art channel". When I get some time, I would like to standardize these so you can get at the values they expose without having to care about what they're being used for and how they are wired up, but for now here's the raw data.
        /// </summary>
        public DefinitionHashPointer<DestinyObjectiveDefinition> EmblemObjective { get; }
        /// <summary>
        /// If this item has a known source, this block will be non-null and populated with source information. Unfortunately, at this time we are not generating sources: that is some aggressively manual work which we didn't have time for, and I'm hoping to get back to at some point in the future.
        /// </summary>
        public InventoryItemSourceBlock SourceData { get; }
        /// <summary>
        /// If this item has available metrics to be shown, this block will be non-null have the appropriate hashes defined.
        /// </summary>
        public InventoryItemMetricBlock Metrics { get; }
        public InventoryItemSummaryBlock Summary { get; }
        public DefinitionHashPointer<DestinyLoreDefinition> Lore { get; }
        public ReadOnlyCollection<InventoryItemAnimationReference> Animations { get; }
        public ReadOnlyCollection<HyperlinkReference> Links { get; }
        public ReadOnlyCollection<DefinitionHashPointer<DestinyDamageTypeDefinition>> DamageTypes { get; }
        public ReadOnlyCollection<DamageType> DamageTypeEnumValues { get; }
        public DefinitionHashPointer<DestinySeasonDefinition> Season { get; }
        public bool Blacklisted { get; }
        public uint Hash { get; }
        public int Index { get; }
        public bool Redacted { get; }

        [JsonConstructor]
        internal DestinyInventoryItemDefinition(uint acquireRewardSiteHash, uint acquireUnlockHash, bool allowActions, DestinyColor backgroundColor, InventoryItemAction action,
            DestinyBreakerTypes breakerType, DestinyClassType classType, DestinyDefinitionDisplayProperties displayProperties, DamageType defaultDamageType, string displaySource,
            bool doesPostmasterPullHaveSideEffects, bool equippable, InventoryItemEquippingBlock equippingBlock, string iconWatermark, string iconWatermarkShelved,
            InventoryItemInventoryBlock inventory, InventoryItemInvestmentStat[] investmentStats, bool isWrapper, uint[] itemCategoryHashes, ItemSubType itemSubType,
            ItemType itemType, string itemTypeAndTierDisplayName, string itemTypeDisplayName, bool nonTransferrable, InventoryItemPerk[] perks, InventoryItemPreviewBlock preview,
            InventoryItemQualityBlock quality, string screenshot, InventoryItemSocketsBlock sockets, SpecialItemType specialItemType, InventoryItemStatsBlock stats, uint summaryItemHash,
            InventoryItemTalentGrid talentGrid, InventoryItemTooltipNotification[] tooltipNotifications, string[] traitIds, InventoryItemTranslationBlock translationBlock,
            string uiItemDisplayStyle, uint collectibleHash, InventoryItemPlugBlock plug, InventoryItemObjectivesBlock objectives, string secondaryIcon, InventoryItemValueBlock value,
            InventoryItemSetDataBlock setData, InventoryItemSackBlock sack, InventoryItemGearsetBlock gearset, string secondaryOverlay, string secondarySpecial,
            string tooltipStyle, uint? emblemObjectiveHash, InventoryItemSourceBlock sourceData, InventoryItemMetricBlock metrics, InventoryItemSummaryBlock summary,
            uint? loreHash, InventoryItemAnimationReference[] animations, HyperlinkReference[] links, uint? breakerTypeHash, uint[] damageTypeHashes, DamageType[] damageTypes,
            uint? defaultDamageTypeHash, uint? seasonHash,
            bool blacklisted, uint hash, int index, bool redacted)
        {
            AcquireRewardSiteHash = acquireRewardSiteHash;
            AcquireUnlockHash = acquireUnlockHash;
            AllowActions = allowActions;
            BackgroundColor = backgroundColor;
            Action = action;
            DisplayProperties = displayProperties;
            BreakerTypeEnumValue = breakerType;
            ClassType = classType;
            Collectible = new DefinitionHashPointer<DestinyCollectibleDefinition>(collectibleHash, DefinitionsEnum.DestinyCollectibleDefinition);
            DefaultDamageTypeEnumValue = defaultDamageType;
            DisplaySource = displaySource;
            DoesPostmasterPullHaveSideEffects = doesPostmasterPullHaveSideEffects;
            Equippable = equippable;
            EquippingBlock = equippingBlock;
            IconWatermark = iconWatermark;
            IconWatermarkShelved = iconWatermarkShelved;
            Inventory = inventory;
            InvestmentStats = investmentStats.AsReadOnlyOrEmpty();
            IsWrapper = isWrapper;
            ItemCategories = itemCategoryHashes.DefinitionsAsReadOnlyOrEmpty<DestinyItemCategoryDefinition>(DefinitionsEnum.DestinyItemCategoryDefinition);
            ItemSubType = itemSubType;
            ItemType = itemType;
            ItemTypeAndTierDisplayName = itemTypeAndTierDisplayName;
            ItemTypeDisplayName = itemTypeDisplayName;
            NonTransferrable = nonTransferrable;
            Objectives = objectives;
            Perks = perks.AsReadOnlyOrEmpty();
            Plug = plug;
            Preview = preview;
            Quality = quality;
            Screenshot = screenshot;
            Sockets = sockets;
            SpecialItemType = specialItemType;
            Stats = stats;
            SummaryItem = new DefinitionHashPointer<DestinyInventoryItemDefinition>(summaryItemHash, DefinitionsEnum.DestinyInventoryItemDefinition);
            TalentGrid = talentGrid;
            TooltipNotifications = tooltipNotifications.AsReadOnlyOrEmpty();
            TraitIds = traitIds.AsReadOnlyOrEmpty();
            TranslationBlock = translationBlock;
            UiItemDisplayStyle = uiItemDisplayStyle;
            SecondaryIcon = secondaryIcon;
            Value = value;
            SetData = setData;
            Sack = sack;
            Gearset = gearset;
            Blacklisted = blacklisted;
            Hash = hash;
            Index = index;
            Redacted = redacted;
            SecondaryOverlay = secondaryOverlay;
            SecondarySpecial = secondarySpecial;
            TooltipStyle = tooltipStyle;
            EmblemObjective = new DefinitionHashPointer<DestinyObjectiveDefinition>(emblemObjectiveHash, DefinitionsEnum.DestinyObjectiveDefinition);
            SourceData = sourceData;
            Metrics = metrics;
            Summary = summary;
            Lore = new DefinitionHashPointer<DestinyLoreDefinition>(loreHash, DefinitionsEnum.DestinyLoreDefinition);
            Animations = animations.AsReadOnlyOrEmpty();
            Links = links.AsReadOnlyOrEmpty();
            BreakerType = new DefinitionHashPointer<DestinyBreakerTypeDefinition>(breakerTypeHash, DefinitionsEnum.DestinyBreakerTypeDefinition);
            DamageTypes = damageTypeHashes.DefinitionsAsReadOnlyOrEmpty<DestinyDamageTypeDefinition>(DefinitionsEnum.DestinyDamageTypeDefinition);
            DamageTypeEnumValues = damageTypes.AsReadOnlyOrEmpty();
            DefaultDamageType = new DefinitionHashPointer<DestinyDamageTypeDefinition>(defaultDamageTypeHash, DefinitionsEnum.DestinyDamageTypeDefinition);
            Season = new DefinitionHashPointer<DestinySeasonDefinition>(seasonHash, DefinitionsEnum.DestinySeasonDefinition);
        }

        public override string ToString()
        {
            return $"{Hash} {DisplayProperties.Name}: {DisplayProperties.Description}";
        }

        public bool DeepEquals(DestinyInventoryItemDefinition other)
        {
            return other != null &&
                   Collectible.DeepEquals(other.Collectible) &&
                   SummaryItem.DeepEquals(other.SummaryItem) &&
                   ItemCategories.DeepEqualsReadOnlyCollections(other.ItemCategories) &&
                   AcquireRewardSiteHash == other.AcquireRewardSiteHash &&
                   AcquireUnlockHash == other.AcquireUnlockHash &&
                   AllowActions == other.AllowActions &&
                   (BackgroundColor != null ? BackgroundColor.DeepEquals(other.BackgroundColor) : other.BackgroundColor == null) &&
                   BreakerTypeEnumValue == other.BreakerTypeEnumValue &&
                   BreakerType.DeepEquals(other.BreakerType) &&
                   ClassType == other.ClassType &&
                   DefaultDamageTypeEnumValue == other.DefaultDamageTypeEnumValue &&
                   DefaultDamageType.DeepEquals(other.DefaultDamageType) &&
                   ItemSubType == other.ItemSubType &&
                   ItemType == other.ItemType &&
                   SpecialItemType == other.SpecialItemType &&
                   DisplayProperties.DeepEquals(other.DisplayProperties) &&
                   DisplaySource == other.DisplaySource &&
                   DoesPostmasterPullHaveSideEffects == other.DoesPostmasterPullHaveSideEffects &&
                   Equippable == other.Equippable &&
                   IconWatermark == other.IconWatermark &&
                   IconWatermarkShelved == other.IconWatermarkShelved &&
                   IsWrapper == other.IsWrapper &&
                   ItemTypeAndTierDisplayName == other.ItemTypeAndTierDisplayName &&
                   ItemTypeDisplayName == other.ItemTypeDisplayName &&
                   UiItemDisplayStyle == other.UiItemDisplayStyle &&
                   NonTransferrable == other.NonTransferrable &&
                   SecondaryIcon == other.SecondaryIcon &&
                   SecondaryOverlay == other.SecondaryOverlay &&
                   SecondarySpecial == other.SecondarySpecial &&
                   Screenshot == other.Screenshot &&
                   TooltipStyle == other.TooltipStyle &&
                   TraitIds.DeepEqualsReadOnlySimpleCollection(other.TraitIds) &&
                   (Stats != null ? Stats.DeepEquals(other.Stats) : other.Stats == null) &&
                   (TalentGrid != null ? TalentGrid.DeepEquals(other.TalentGrid) : other.TalentGrid == null) &&
                   (TranslationBlock != null ? TranslationBlock.DeepEquals(other.TranslationBlock) : other.TranslationBlock == null) &&
                   (Value != null ? Value.DeepEquals(other.Value) : other.Value == null) &&
                   (SetData != null ? SetData.DeepEquals(other.SetData) : other.SetData == null) &&
                   (Plug != null ? Plug.DeepEquals(other.Plug) : other.Plug == null) &&
                   (Preview != null ? Preview.DeepEquals(other.Preview) : other.Preview == null) &&
                   (Quality != null ? Quality.DeepEquals(other.Quality) : other.Quality == null) &&
                   (Objectives != null ? Objectives.DeepEquals(other.Objectives) : other.Objectives == null) &&
                   Inventory.DeepEquals(other.Inventory) &&
                   (Action != null ? Action.DeepEquals(other.Action) : other.Action == null) &&
                   (EquippingBlock != null ? EquippingBlock.DeepEquals(other.EquippingBlock) : other.EquippingBlock == null) &&
                   (Sockets != null ? Sockets.DeepEquals(other.Sockets) : other.Sockets == null) &&
                   InvestmentStats.DeepEqualsReadOnlyCollections(other.InvestmentStats) &&
                   Perks.DeepEqualsReadOnlyCollections(other.Perks) &&
                   TooltipNotifications.DeepEqualsReadOnlyCollections(other.TooltipNotifications) &&
                   (Sack != null ? Sack.DeepEquals(other.Sack) : other.Sack == null) &&
                   (Gearset != null ? Gearset.DeepEquals(other.Gearset) : other.Gearset == null) &&
                   EmblemObjective.DeepEquals(other.EmblemObjective) &&
                   (SourceData != null ? SourceData.DeepEquals(other.SourceData) : other.SourceData == null) &&
                   (Metrics != null ? Metrics.DeepEquals(other.Metrics) : other.Metrics == null) &&
                   (Summary != null ? Summary.DeepEquals(other.Summary) : other.Summary == null) &&
                   Lore.DeepEquals(other.Lore) &&
                   Animations.DeepEqualsReadOnlyCollections(other.Animations) &&
                   Links.DeepEqualsReadOnlyCollections(other.Links) &&
                   DamageTypes.DeepEqualsReadOnlyCollections(other.DamageTypes) &&
                   DamageTypeEnumValues.DeepEqualsReadOnlySimpleCollection(other.DamageTypeEnumValues) &&
                   Season.DeepEquals(other.Season) &&
                   Blacklisted == other.Blacklisted &&
                   Hash == other.Hash &&
                   Index == other.Index &&
                   Redacted == other.Redacted;
        }
    }
}
