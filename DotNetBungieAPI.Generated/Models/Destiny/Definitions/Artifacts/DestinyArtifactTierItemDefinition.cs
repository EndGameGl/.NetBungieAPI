using System.Text.Json.Serialization;

namespace DotNetBungieAPI.Generated.Models.Destiny.Definitions.Artifacts;

public sealed class DestinyArtifactTierItemDefinition
{

    /// <summary>
    ///     The identifier of the Plug Item unlocked by activating this item in the Artifact.
    /// </summary>
    [JsonPropertyName("itemHash")]
    public uint ItemHash { get; init; } // DestinyInventoryItemDefinition
}
