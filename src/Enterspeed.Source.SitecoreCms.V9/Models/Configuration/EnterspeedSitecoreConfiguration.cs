using System.Collections.Generic;
using System.Linq;
using Enterspeed.Source.Sdk.Configuration;
using Enterspeed.Source.SitecoreCms.V8.Models.Configuration;
using Sitecore.Data.Items;

namespace Enterspeed.Source.SitecoreCms.V9.Models.Configuration
{
    public class EnterspeedSitecoreConfiguration : EnterspeedConfiguration
    {
        public EnterspeedSitecoreConfiguration()
        {
            ConfigurationElement = EnterspeedConfigurationElement.GetConfiguration();
        }

        public bool IsEnabled { get; set; }

        public bool IsPreview { get; set; }

        public string ItemNotFoundUrl { get; set; }

        public List<EnterspeedSiteInfo> SiteInfos { get; } = new List<EnterspeedSiteInfo>();
        public EnterspeedConfigurationElement ConfigurationElement { get; }

        public EnterspeedSiteInfo GetSite(Item item)
        {
            if (item == null)
            {
                return null;
            }

            return SiteInfos?.FirstOrDefault(x => x.IsItemOfSite(item));
        }
    }
}