using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyApp.Services
{
    public class IconInformerService
    {
        public string GetIconPath()
        {
            return @"..\..\..\services.ico";
        }

        internal string GetIconText()
        {
            return "This is a sample Icon Text";
        }
    }
}
