﻿namespace DotNetBungieAPI.Models.Applications
{
    public sealed record Application
    {
        /// <summary>
        ///     Unique ID assigned to the application
        /// </summary>
        [JsonPropertyName("applicationId")]
        public int ApplicationId { get; init; }

        /// <summary>
        ///     Name of the application
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; }

        /// <summary>
        ///     URL used to pass the user's authorization code to the application
        /// </summary>
        [JsonPropertyName("redirectUrl")]
        public string RedirectUrl { get; init; }

        /// <summary>
        ///     Link to website for the application where a user can learn more about the app.
        /// </summary>
        [JsonPropertyName("link")]
        public string Link { get; init; }

        /// <summary>
        ///     Permissions the application needs to work
        /// </summary>
        [JsonPropertyName("scope")]
        public ApplicationScopes Scope { get; init; }

        /// <summary>
        ///     Value of the Origin header sent in requests generated by this application.
        /// </summary>
        [JsonPropertyName("origin")]
        public string Origin { get; init; }

        /// <summary>
        ///     Current status of the application.
        /// </summary>
        [JsonPropertyName("status")]
        public ApplicationStatus Status { get; init; }

        /// <summary>
        ///     Date the application was first added to our database.
        /// </summary>
        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; init; }

        /// <summary>
        ///     ate the application status last changed.
        /// </summary>
        [JsonPropertyName("statusChanged")]
        public DateTime StatusChanged { get; init; }

        /// <summary>
        ///     Date the first time the application status entered the 'Public' status.
        /// </summary>
        [JsonPropertyName("firstPublished")]
        public DateTime FirstPublished { get; init; }

        /// <summary>
        ///     List of team members who manage this application on Bungie.net. Will always consist of at least the application
        ///     owner.
        /// </summary>
        [JsonPropertyName("team")]
        public ReadOnlyCollection<ApplicationDeveloper> Team { get; init; } =
            ReadOnlyCollections<ApplicationDeveloper>.Empty;
    }
}