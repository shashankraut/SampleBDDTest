using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer
{
    public class ConfigReader
    {
        private static string browser = string.Empty;

        /// <summary>
        /// Provides the browser with which the tests needs to be executed
        /// </summary>
        public static string Browser
        {
            get
            {
                if (string.IsNullOrEmpty(browser))
                {
                    string? tempValue = TestContext.Parameters["Browser"] ?? ConfigurationManager.AppSettings.Get("Browser");
                    browser = !string.IsNullOrEmpty(tempValue) ? tempValue : "Chrome"; //Assigning a default value if nothing is found from app.config
                }
                return browser;
            }
        }
    }
}
