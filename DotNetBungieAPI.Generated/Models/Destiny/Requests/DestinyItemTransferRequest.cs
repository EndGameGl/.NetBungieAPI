namespace DotNetBungieAPI.Generated.Models.Destiny.Requests;

public sealed class DestinyItemTransferRequest
{

    [JsonPropertyName("itemReferenceHash")]
    public uint ItemReferenceHash { get; init; } // DestinyInventoryItemDefinition

    [JsonPropertyName("stackSize")]
    public int StackSize { get; init; }

    [JsonPropertyName("transferToVault")]
    public bool TransferToVault { get; init; }

    /// <summary>
    ///     The instance ID of the item for this action request.
    /// </summary>
    [JsonPropertyName("itemId")]
    public long ItemId { get; init; }

    [JsonPropertyName("characterId")]
    public long CharacterId { get; init; }

    [JsonPropertyName("membershipType")]
    public BungieMembershipType MembershipType { get; init; }
}
