﻿using System;
using System.Linq;
using Enterspeed.Source.Sdk.Domain.Services;
using Enterspeed.Source.SitecoreCms.V8.Models.Configuration;
using Enterspeed.Source.SitecoreCms.V8.Providers;
using Enterspeed.Source.SitecoreCms.V8.Services;
using Enterspeed.Source.SitecoreCms.V8.Services.Serializers;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Globalization;

namespace Enterspeed.Source.SitecoreCms.V8.Events
{
    public class SaveEventHandler
    {
        private readonly BaseItemManager _itemManager;
        private readonly IEnterspeedConfigurationService _enterspeedConfigurationService;
        private readonly IEnterspeedSitecoreIngestService _enterspeedSitecoreIngestService;

        public SaveEventHandler(
            BaseItemManager itemManager,
            IEnterspeedConfigurationService enterspeedConfigurationService,
            IEnterspeedSitecoreIngestService enterspeedSitecoreIngestService)
        {
            _itemManager = itemManager;
            _enterspeedConfigurationService = enterspeedConfigurationService;
            _enterspeedSitecoreIngestService = enterspeedSitecoreIngestService;
        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            SitecoreEventArgs eventArgs = args as SitecoreEventArgs;

            Item sourceItem = eventArgs.Parameters[0] as Item;

            if (sourceItem == null)
            {
                return;
            }
        
            var siteConfigurations = _enterspeedConfigurationService.GetConfiguration();
            foreach (EnterspeedSitecoreConfiguration configuration in siteConfigurations)
            {
                if (!configuration.IsEnabled)
                {
                    continue;
                }

                if (!configuration.IsPreview)
                {
                    continue;
                }

                if (configuration.ConfigurationElement.Excludes.ExcludedIds.Contains(sourceItem.ID.Guid))
                {
                    continue;
                }
                if (configuration.ConfigurationElement.Excludes.ExcludedPath.Any(x=>sourceItem.Paths.ContentPath.Contains(x)))
                {
                    continue;
                }

                EnterspeedIngestService enterspeedIngestService = new EnterspeedIngestService(new SitecoreEnterspeedConnection(configuration), new NewtonsoftJsonSerializer(), new EnterspeedSitecoreConfigurationProvider(_enterspeedConfigurationService));
                Language language = sourceItem.Language;

                // Getting the source item first
                if (sourceItem == null)
                {
                    continue;
                }

                if (!_enterspeedSitecoreIngestService.HasAllowedPath(sourceItem))
                {
                    continue;
                }

                // Handling if the item was published
                if (sourceItem == null || sourceItem.Versions.Count == 0)
                {
                    continue;
                }

                if (!_enterspeedSitecoreIngestService.HasAllowedPath(sourceItem))
                {
                    continue;
                }

                _enterspeedSitecoreIngestService.HandleContentItem(sourceItem, enterspeedIngestService, configuration, false, true, true);
                _enterspeedSitecoreIngestService.HandleRendering(sourceItem, enterspeedIngestService, configuration, false, true, true);
                _enterspeedSitecoreIngestService.HandleDictionary(sourceItem, enterspeedIngestService, configuration, false, true, true);
            }
        }
    }
}