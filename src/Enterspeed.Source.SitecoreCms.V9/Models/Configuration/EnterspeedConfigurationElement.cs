using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Xml;

namespace Enterspeed.Source.SitecoreCms.V8.Models.Configuration
{
    public class EnterspeedConfigurationElement
    {
        public Excludes Excludes { get; private set; }

        public static EnterspeedConfigurationElement GetConfiguration()
        {
            var config = new EnterspeedConfigurationElement();
            var xmlConfiguration = Factory.GetConfigNode("enterSpeed");
            if (xmlConfiguration == null)
            {
                return config;
            }

            var subnodes = xmlConfiguration.ChildNodes.Cast<XmlNode>();
            config.Excludes = GetExcludes(subnodes);
            return config;
        }

        //The method who return the list of my custom elements
        private static Excludes GetExcludes(IEnumerable<XmlNode> xmlNodeList)
        {
            Excludes lst = new Excludes()
            {
                ExcludedIds = new List<Guid>(),
                ExcludedPath = new List<string>()
            };
            var excludesObjects = xmlNodeList.FirstOrDefault(x => x.Name == "excludes");
            if (excludesObjects == null)
            {
                return lst;
            }

            //Read the configuration nodes
            foreach (XmlNode node in excludesObjects.ChildNodes)
            {
                var id = XmlUtil.GetAttribute("id", node);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    lst.ExcludedIds.Add(Guid.Parse(id));
                    continue;
                }

                var path = XmlUtil.GetAttribute("path", node);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    lst.ExcludedPath.Add(path);
                    continue;
                }

                //There might be potentially other ways to skip so continue after each passed if
            }

            return lst;
        }
    }
}