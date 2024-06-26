﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//prueba conflicto local
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PartsUnlimited.WebJobs.UpdateProductInventory
{
    public class Program
    {
        public int Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.Add(new JsonConfigurationSource { Path = "config.json" });
            var config = builder.Build();
            var webjobsConnectionString = config["Data:AzureWebJobsStorage:ConnectionString"];
            var dbConnectionString = config["Data:DefaultConnection:ConnectionString"];

            if (string.IsNullOrWhiteSpace(webjobsConnectionString))
            {
                Console.WriteLine("The configuration value for Azure Web Jobs Connection String is missing.");
                return 10;
            }

            if (string.IsNullOrWhiteSpace(dbConnectionString))
            {
                Console.WriteLine("The configuration value for Database Connection String is missing.");
                return 10;
            }

            var jobHostConfig = new JobHostConfiguration(webjobsConnectionString);
            var host = new JobHost(jobHostConfig);

            host.RunAndBlock();
            return 0;
        }
    }
}
