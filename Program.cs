// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "DoverAPI";

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json",
                                optional: false,        // File is not optional.
                                reloadOnChange: false);
        }).ConfigureLogging(logBuilder =>
        {
            logBuilder.ClearProviders(); // removes all providers from LoggerFactory
            logBuilder.AddConsole();
            logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
        })
        .UseStartup<Startup>();
    }
}