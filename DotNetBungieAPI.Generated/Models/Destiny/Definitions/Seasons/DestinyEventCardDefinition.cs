namespace DotNetBungieAPI.Generated.Models.Destiny.Definitions.Seasons;

/// <summary>
///     Defines the properties of an 'Event Card' in Destiny 2, to coincide with a seasonal event for additional challenges, premium rewards, a new seal, and a special title. For example: Solstice of Heroes 2022.
/// </summary>
public class DestinyEventCardDefinition
{
    [JsonPropertyName("displayProperties")]
    public Destiny.Definitions.Common.DestinyDisplayPropertiesDefinition? DisplayProperties { get; set; }

    [JsonPropertyName("linkRedirectPath")]
    public string? LinkRedirectPath { get; set; }

    [JsonPropertyName("color")]
    public Destiny.Misc.DestinyColor? Color { get; set; }

    [JsonPropertyName("images")]
    public Destiny.Definitions.Seasons.DestinyEventCardImages? Images { get; set; }

    [JsonPropertyName("triumphsPresentationNodeHash")]
    public uint? TriumphsPresentationNodeHash { get; set; }

    [JsonPropertyName("sealPresentationNodeHash")]
    public uint? SealPresentationNodeHash { get; set; }

    [JsonPropertyName("ticketCurrencyItemHash")]
    public uint? TicketCurrencyItemHash { get; set; }

    [JsonPropertyName("ticketVendorHash")]
    public uint? TicketVendorHash { get; set; }

    [JsonPropertyName("ticketVendorCategoryHash")]
    public uint? TicketVendorCategoryHash { get; set; }

    [JsonPropertyName("endTime")]
    public long? EndTime { get; set; }

    /// <summary>
    ///     The unique identifier for this entity. Guaranteed to be unique for the type of entity, but not globally.
    /// <para />
    ///     When entities refer to each other in Destiny content, it is this hash that they are referring to.
    /// </summary>
    [JsonPropertyName("hash")]
    public uint? Hash { get; set; }

    /// <summary>
    ///     The index of the entity as it was found in the investment tables.
    /// </summary>
    [JsonPropertyName("index")]
    public int? Index { get; set; }

    /// <summary>
    ///     If this is true, then there is an entity with this identifier/type combination, but BNet is not yet allowed to show it. Sorry!
    /// </summary>
    [JsonPropertyName("redacted")]
    public bool? Redacted { get; set; }
}
