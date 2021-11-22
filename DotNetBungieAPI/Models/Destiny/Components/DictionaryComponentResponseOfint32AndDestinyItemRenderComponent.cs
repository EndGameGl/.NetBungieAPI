﻿namespace DotNetBungieAPI.Models.Destiny.Components
{
    public sealed record DictionaryComponentResponseOfint32AndDestinyItemRenderComponent : ComponentResponse
    {
        [JsonPropertyName("data")]
        public ReadOnlyDictionary<int, DestinyItemRenderComponent> Data { get; init; } =
            ReadOnlyDictionaries<int, DestinyItemRenderComponent>.Empty;
    }
}