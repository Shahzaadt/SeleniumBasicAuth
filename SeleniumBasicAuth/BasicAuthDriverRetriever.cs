using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using AutomatedTester.BrowserMob;
using Newtonsoft.Json;
using System.Net.Http;
using OpenQA.Selenium.Chrome;

namespace SeleniumBasicAuth
{
    public class BasicAuthDriverRetriever
    {
        public static FirefoxDriver GetFirefoxDriver(FirefoxProfile profile)
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.Platform = OpenQA.Selenium.Platform.CurrentPlatform;
            caps.SetCapability(FirefoxDriver.ProfileCapabilityName, profile);
            FirefoxDriver driver = new FirefoxDriver(caps);
            return driver;
        }

        public static InternetExplorerDriver GetIEDriver(InternetExplorerOptions opts)
        {
            InternetExplorerDriver driver = new InternetExplorerDriver(opts);
            return driver;
        }

        public static ChromeDriver GetChromeDriver(ChromeOptions opts)
        {
            ChromeDriver driver = new ChromeDriver(opts);
            return driver;
        }
    }
}
