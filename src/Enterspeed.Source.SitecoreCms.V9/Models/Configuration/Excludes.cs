using System;
using System.Collections.Generic;

namespace Enterspeed.Source.SitecoreCms.V8.Models.Configuration
{
    public class Excludes
    {
        public IList<Guid> ExcludedIds { get;  set; }
        public IList<string> ExcludedPath { get;  set; }
    }
}