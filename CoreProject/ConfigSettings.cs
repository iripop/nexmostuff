using CoreProject.Requests;
using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CoreProject
{
    public sealed class ConfigSettings
    {
        static ConfigSettings()
        {
        }

        private ConfigSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "appSettings:Nexmo.Url.Rest", "https://rest.nexmo.com"},
                    { "appSettings:Nexmo.Url.Api", "https://api.nexmo.com"},
                    { "appSettings:Nexmo.Api.EnsureSuccessStatusCode", "false" }
                })
                .AddJsonFile("settings.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
            ;

            Settings = builder.Build();

            // verify we have a minimum amount of configuration

            var authCapabilities = new List<string>();

            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.api_key"]) &&
                !string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.api_secret"]))
            {
                authCapabilities.Add("Key/Secret");
            }
            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.security_secret"]))
            {
                authCapabilities.Add("Security/Signing");
            }
            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.Application.Id"]) &&
                !string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.Application.Key"]))
            {
                authCapabilities.Add("Application");
            }

            if (authCapabilities.Count == 0)
            {
                Log.Info("No authentication found via configuration. Remember to provide your own.");
            }
            else
            {
                Log.Info("Available authentication: " + $"{authCapabilities}");
            }
        }

        //private static readonly ILog Logger = LogProvider.For<ConfigSettings>();

        public static ConfigSettings Instance { get; } = new ConfigSettings();

        public IConfiguration Settings { get; }

        public HttpMessageHandler ClientHandler { get; set; }

        private HttpClient _client;
        public HttpClient Client
        {
            get
            {
                var reqPerSec = Instance.Settings["appSettings:Nexmo.Api.RequestsPerSecond"];
                if (string.IsNullOrEmpty(reqPerSec))
                    return _client ?? (_client = ClientHandler == null ? new HttpClient() : new HttpClient(ClientHandler));

                var delay = 1 / double.Parse(reqPerSec);
                var execTimeSpanSemaphore = new TimeSpanSemaphore(1, TimeSpan.FromSeconds(delay));
                // TODO: this messes up the unit test mock if throttle config is set
                ThrottlingMessageHandler handler = ClientHandler != null ? new ThrottlingMessageHandler(execTimeSpanSemaphore, ClientHandler) : new ThrottlingMessageHandler(execTimeSpanSemaphore);
                return _client ?? (_client = new HttpClient(handler));
            }
        }
    }
}
