﻿using System;
using System.Text.Json.Serialization;

namespace NetBungieAPI.Models.User
{
    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OptInFlags : long
    {
        None = 0,
        Newsletter = 1,
        System = 2,
        Marketing = 4,
        UserResearch = 8,
        CustomerService = 16,
        Social = 32,
        PlayTests = 64,
        PlayTestsLocal = 128,
        Careers = 256
    }
}