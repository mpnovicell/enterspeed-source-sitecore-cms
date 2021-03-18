﻿using System;

namespace Enterspeed.Source.SitecoreCms.V9.Services.DataProperties.Formatters
{
    public class EnterspeedDateFormatter
    {
        public string FormatDate(DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}