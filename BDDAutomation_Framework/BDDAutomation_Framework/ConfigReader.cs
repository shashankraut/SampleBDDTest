using NUnit.Framework;
using Automation_ApplicationLayer;



namespace BDDAutomation_Test
{
    public class ConfigReader : Automation_ApplicationLayer.ConfigReader
    {
        private static string? sauceApplicationUrl;

        /// <summary>
        /// Provides the browser with which the tests needs to be executed
        /// </summary>
        public static string? SauceApplicationUrl
        {
            get
            {
                if (string.IsNullOrEmpty(sauceApplicationUrl))
                {
                    sauceApplicationUrl = TestContext.Parameters["SauceApplicationURL"] ?? System.Configuration.ConfigurationManager.AppSettings["SauceApplicationURL"];
                }
                return sauceApplicationUrl;
            }
        }
    }
}
