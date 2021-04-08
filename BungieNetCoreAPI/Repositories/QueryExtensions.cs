﻿using NetBungieAPI.Models;
using NetBungieAPI.Models.Destiny.Definitions.InventoryItems;
using NetBungieAPI.Models.Destiny.Definitions.ItemCategories;
using System.Collections.Generic;

namespace NetBungieAPI.Repositories
{
    public static class QueryExtensions
    {
        public static Dictionary<DestinyItemCategoryDefinition, List<DestinyInventoryItemDefinition>> GetItemsCategorized(this ILocalisedDestinyDefinitionRepositories repo, BungieLocales locale)
        {
            var result = new Dictionary<DestinyItemCategoryDefinition, List<DestinyInventoryItemDefinition>>();
            foreach (var category in repo.GetAll<DestinyItemCategoryDefinition>(locale))
            {
                List<DestinyInventoryItemDefinition> categorizedItems = new List<DestinyInventoryItemDefinition>();
                result.Add(category, categorizedItems);
            }
            foreach (var item in repo.GetAll<DestinyInventoryItemDefinition>(locale))
            {
                foreach (var category in item.ItemCategories)
                {
                    if (category.TryGetDefinition(out var value))
                    {
                        result[value].Add(item);
                    }
                }
            }
            return result;
        }

    }
}
