using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.Text.RegularExpressions;

namespace SeleniumTools
{
    public class PhantomChromeDriver : ChromeDriver
    {
        public PhantomChromeDriver() : base(RemovingFlag()) { }
        public PhantomChromeDriver(ChromeOptions options) : base(RemovingFlag(options)) { }

        private static ChromeOptions RemovingFlag() => ModifyOptions(new ChromeOptions());
        private static ChromeOptions RemovingFlag(ChromeOptions options) => ModifyOptions(options);

        private static ChromeOptions ModifyOptions(ChromeOptions options)
        {
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalChromeOption("useAutomationExtension", false);
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-blink-features=AutomationControlled");

            return options;
        }
    }

    //public class Proxy
    //{
    //    IPAddress Ip { set; get; }
    //    int Port { set; get; }
    //    string Name { set; get; }
    //    string Password { set; get; }

    //    public static Proxy Parse(string proxyString)
    //    {
    //        var match = Regex.Match(proxyString, @"(\d+.\d+.\d+.\d+):(\d+):(.+):(.+)");

    //        return new Proxy()
    //        {
    //            Ip = IPAddress.Parse(match.Groups[1].Value),
    //            Port = int.Parse(match.Groups[2].Value),
    //            Name = match.Groups[3].Value,
    //            Password = match.Groups[4].Value
    //        };
    //    }
    //}

    public class WebDriverService
    {
        private PhantomChromeDriver _driver;
        private string _profileName = null;

        public WebDriverService()
        {
        }

        public WebDriverService(string profileName)
        {
            _profileName = profileName;
        }

        public IWebDriver GetDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            options.AddArgument("--dns-prefetch-disable");
            options.AddArgument("--ignore-certificate-errors-spki-list");
            options.AddArgument("--ignore-urlfetcher-cert-requests");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--ignore-ssl-errors");
            if (_profileName != null)
                options.AddArgument($"--user-data-dir={Directory.GetCurrentDirectory()}/{_profileName}");
            var driver = new PhantomChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
