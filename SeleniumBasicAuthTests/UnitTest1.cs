/*
 * Created by SharpDevelop.
 * User: Alan
 * Date: 5/6/2014
 * Time: 11:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using Coypu;
using Coypu.Drivers;
using System.IO;
using System.Reflection;

namespace SeleniumBasicAuth
{
    [TestFixture]
    public class Test1
    {
        BasicAuthWebDriverWrapper basicAuth;
        [Test]
        public void IETest()
        {
            BasicAuthSettings.BrowserType = BrowserType.IE;
            BasicAuthSettings.Domain = "testdomain.com";
            BasicAuthSettings.Username = "testUsername";
            BasicAuthSettings.Password = "testPassword";

            InternetExplorerDriver driver = null;
            basicAuth = new BasicAuthWebDriverWrapper();
            try
            {

                driver = (InternetExplorerDriver)basicAuth.AuthenticatedDriver;
                driver.Navigate().GoToUrl("http://www.google.com/");
            }
            finally
            {
                try
                {
                    basicAuth.Quit();
                }
                catch (Exception e) { }
            }
        }

        [Test]
        public void FirefoxTest()
        {
            BasicAuthSettings.BrowserType = BrowserType.Firefox;
            BasicAuthSettings.Domain = "testdomain.com";
            BasicAuthSettings.Username = "testUsername";
            BasicAuthSettings.Password = "testPassword";
            FirefoxDriver driver = null;
            basicAuth = new BasicAuthWebDriverWrapper();
            try
            {
                driver = (FirefoxDriver)basicAuth.AuthenticatedDriver;
                driver.Navigate().GoToUrl("http://www.google.com/");
            }
            finally
            {
                try
                {
                    basicAuth.Quit();
                }
                catch (Exception e) { }
            }
        }

        [Test]
        public void ChromeTest()
        {
            BasicAuthSettings.BrowserType = BrowserType.Chrome;
            BasicAuthSettings.Domain = "testdomain.com";
            BasicAuthSettings.Username = "testUsername";
            BasicAuthSettings.Password = "testPassword";

            ChromeDriver driver = null;
            basicAuth = new BasicAuthWebDriverWrapper();
            try
            {
                driver = (ChromeDriver)basicAuth.AuthenticatedDriver;
                driver.Navigate().GoToUrl("http://www.google.com/");
            }
            finally
            {
                try
                {
                    basicAuth.Quit();
                }
                catch (Exception e) { }
            }
        }

        [Test]
        public void FirefoxCoypuTest()
        {
            BasicAuthSettings.Domain = "testdomain.com";
            BasicAuthSettings.Username = "testUsername";
            BasicAuthSettings.Password = "testPassword";

            SessionConfiguration config = new SessionConfiguration
            {
                AppHost = "http://www.google.com/",
                Port = 80,
                SSL = true | false,
                Driver = typeof(CoypuFirefoxBasicAuthSeleniumDriver)
            };

            BasicAuthBrowserSession session = new BasicAuthBrowserSession(config);
			session.Visit("http://www.google.com");
            session.Dispose();
        }

        [Test]
        public void IECoypuTest()
        {
            BasicAuthSettings.Domain = "testdomain.com";
            BasicAuthSettings.Username = "testUsername";
            BasicAuthSettings.Password = "testPassword";

            SessionConfiguration config = new SessionConfiguration
            {
                AppHost = "http://www.google.com/",
                Port = 80,
                SSL = true | false,
                Driver = typeof(CoypuIEBasicAuthSeleniumDriver)
            };

            BasicAuthBrowserSession session = new BasicAuthBrowserSession(config);
			session.Visit("http://www.google.com");
            session.Dispose();
        }

        [Test]
        public void ChromeCoypuTest()
        {
            BasicAuthSettings.Domain = "testdomain.com";
            BasicAuthSettings.Username = "testUsername";
            BasicAuthSettings.Password = "testPassword";

            SessionConfiguration config = new SessionConfiguration
            {
                AppHost = "http://www.google.com/",
                Port = 80,
                SSL = true | false,
                Driver = typeof(CoypuChromeBasicAuthSeleniumDriver)
            };

            BasicAuthBrowserSession session = new BasicAuthBrowserSession(config);
			session.Visit("http://www.google.com");
            session.Dispose();
        }
    }
}
