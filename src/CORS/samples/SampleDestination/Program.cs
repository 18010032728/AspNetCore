// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleDestination
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Type startupType = null;

            var startup = Environment.GetEnvironmentVariable("CORS_STARTUP");
            switch (startup)
            {
                case "Startup":
                    startupType = typeof(Startup);
                    break;
                case "StartupWithoutEndpointRouting":
                    startupType = typeof(StartupWithoutEndpointRouting);
                    break;
            }

            if (startupType == null)
            {
                throw new InvalidOperationException("Could not resolve the startup type. Unexpected CORS_STARTUP environment variable.");
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://+:9000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging(factory => factory.AddConsole())
                .UseStartup(startupType)
                .Build();

            host.Run();
        }
    }
}
