﻿using System;
using Microsoft.Extensions.Logging;
using NetBungieAPI.Services.Default.ServiceConfigurations;

namespace NetBungieAPI.Services.Default
{
    internal sealed class DefaultDotNetBungieApiLogger : ILogger
    {
        private readonly DotNetBungieApiLoggerConfiguration _configuration;

        internal DefaultDotNetBungieApiLogger(DotNetBungieApiLoggerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            Console.WriteLine(
                $"[{eventId.Id,2}: {logLevel,-12}]{Environment.NewLine}     DotNetBungieAPI - {formatter(state, exception)}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _configuration.IsEnabled && _configuration.ShouldLogLevel(logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }
    }
}