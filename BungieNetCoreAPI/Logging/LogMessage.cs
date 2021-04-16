﻿using System;
using System.Globalization;
using System.Threading;

namespace NetBungieAPI.Logging
{
    public class LogMessage
    {
        public DateTime Time { get; }
        public string Message { get; }
        public LogType Type { get; }

        internal LogMessage(DateTime time, string message, LogType type)
        {
            Time = time;
            Message = message;
            Type = type;
        }

        public override string ToString()
        {
            return 
                StringBuilderPool
                    .GetBuilder(CancellationToken.None)
                    .Append("[").Append(Type.ToString())
                    .Append("]: [")
                    .Append(Time.ToString(CultureInfo.InvariantCulture))
                    .Append("]: ")
                    .Append(Message)
                    .Build();
        }
    }
}
