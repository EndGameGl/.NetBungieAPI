﻿using NetBungieAPI.Bungie;
using NetBungieAPI.Services.ApiAccess.Interfaces;
using NetBungieAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace NetBungieAPI.Services.ApiAccess
{
    public class UserMethodsAccess : IUserMethodsAccess
    {
        private IHttpClientInstance _httpClient;
        internal UserMethodsAccess(IHttpClientInstance httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BungieResponse<BungieNetUser>> GetBungieNetUserById(long id)
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<BungieNetUser>>($"/User/GetBungieNetUserById/{id}");
        }
        public async Task<BungieResponse<BungieNetUser[]>> SearchUsers(string query)
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<BungieNetUser[]>>($"/User/SearchUsers/{query}");
        }
        public async Task<BungieResponse<BungieNetUserAccountCredentialType[]>> GetCredentialTypesForTargetAccount(long id)
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<BungieNetUserAccountCredentialType[]>>($"User/GetCredentialTypesForTargetAccount/{id}");
        }
        public async Task<BungieResponse<BungieUserTheme[]>> GetAvailableThemes()
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<BungieUserTheme[]>>($"/User/GetAvailableThemes");
        }
        public async Task<BungieResponse<BungieNetUserWithMemberships>> GetMembershipDataById(long id, BungieMembershipType membershipType)
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<BungieNetUserWithMemberships>>($"/User/GetMembershipsById/{id}/{membershipType}");
        }
        public async Task<BungieResponse<BungieNetUserWithMemberships>> GetMembershipDataForCurrentUser()
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<BungieNetUserWithMemberships>>($"User/GetMembershipsForCurrentUser");
        }
        public async Task<BungieResponse<DestinyHardLinkedUserMembership>> GetMembershipFromHardLinkedCredential(long credential, BungieCredentialType credentialType = BungieCredentialType.SteamId)
        {
            return await _httpClient.GetFromPlatfromAndDeserialize<BungieResponse<DestinyHardLinkedUserMembership>>($"/User/GetMembershipFromHardLinkedCredential/{credentialType}/{credential}");
        }
    }
}
