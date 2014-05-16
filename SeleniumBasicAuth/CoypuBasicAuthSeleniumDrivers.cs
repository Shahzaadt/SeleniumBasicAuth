using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasicAuth
{
    public class CoypuFirefoxBasicAuthSeleniumDriver : SeleniumWebDriver
    {
        public CoypuFirefoxBasicAuthSeleniumDriver(Browser browser)
            : base(GetDriver(), Browser.Firefox)
        { }

        private static RemoteWebDriver GetDriver()
        {
            BasicAuthSettings.BrowserType = BrowserType.Firefox;
            BasicAuthWebDriverWrapper wrapper = new BasicAuthWebDriverWrapper();
            return wrapper.AuthenticatedDriver;
        }

        public new void Dispose()
        {
            BasicAuthSettings.Server.Stop();
            base.Dispose();
        }
    }

    public class CoypuIEBasicAuthSeleniumDriver : SeleniumWebDriver
    {
        public CoypuIEBasicAuthSeleniumDriver(Browser browser)
            : base(GetDriver(), Browser.InternetExplorer)
        { }

        private static RemoteWebDriver GetDriver()
        {
            BasicAuthSettings.BrowserType = BrowserType.IE;
            BasicAuthWebDriverWrapper wrapper = new BasicAuthWebDriverWrapper();
            return wrapper.AuthenticatedDriver;
        }

        public new void Dispose()
        {
            BasicAuthSettings.Server.Stop();
            base.Dispose();
        }
    }

    public class CoypuChromeBasicAuthSeleniumDriver : SeleniumWebDriver
    {
        public CoypuChromeBasicAuthSeleniumDriver(Browser browser)
            :base(GetDriver(),Browser.Chrome)
        { }

        private static RemoteWebDriver GetDriver()
        {
            BasicAuthSettings.BrowserType = BrowserType.Chrome;
            BasicAuthWebDriverWrapper wrapper = new BasicAuthWebDriverWrapper();
            return wrapper.AuthenticatedDriver;
        }

        public new void Dispose()
        {
            BasicAuthSettings.Server.Stop();
            base.Dispose();
        }
    }
}
