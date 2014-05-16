/*
 * Created by SharpDevelop.
 * User: Alan
 * Date: 5/6/2014
 * Time: 11:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Http;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Linq;
using AutomatedTester.BrowserMob;
using OpenQA.Selenium;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace SeleniumBasicAuth
{
	public enum BrowserType
	{
		Firefox,
		IE,
		Chrome
	}
	
	/// <summary>
	/// Description of MyClass.
	/// </summary>
    public class BasicAuthWebDriverWrapper
	{
        private RemoteWebDriver driver;
        public RemoteWebDriver AuthenticatedDriver
        {
            get
            {
                if (driver == null)
                {
                    driver = GetAuthenticatedDriver(BasicAuthSettings.BrowserType, BasicAuthSettings.BrowserMobPath, BasicAuthSettings.Domain, BasicAuthSettings.Username, BasicAuthSettings.Password);
                }
                return driver;
            }
        }

        public BasicAuthWebDriverWrapper()
        {
        }

		private RemoteWebDriver GetAuthenticatedDriver(BrowserType browserType,String browserMobPath, String domain, String username, String password)
		{
			if(browserType.Equals(BrowserType.Firefox))
			{
                return GetModifiedFirefoxDriver(browserMobPath, domain, username, password);
			}
			else if(browserType.Equals(BrowserType.IE))
			{
				return GetModifiedIEDriver(browserMobPath, domain,username,password);
			}
			else if(browserType.Equals(BrowserType.Chrome))
			{
                return GetModifiedChromeDriver(browserMobPath, domain, username, password);
			}
			else
			{
				throw new Exception("BrowserType: "+browserType.ToString() + " not supported");
			}
		}

        private FirefoxDriver GetModifiedFirefoxDriver(String browserMobPath, String domain, String username, String password)
		{
            FirefoxProfile prof = new FirefoxProfile();

            BasicAuthSettings.Server = new Server(browserMobPath);
            BasicAuthSettings.Server.Start();

            Proxy seleniumProxy = SetUpProxy(domain, username, password).Result;
            prof.SetProxyPreferences(seleniumProxy);

            return BasicAuthDriverRetriever.GetFirefoxDriver(prof);
		}
		
		private InternetExplorerDriver GetModifiedIEDriver(String browserMobPath,String domain,String username, String password)
		{
            BasicAuthSettings.Server = new Server(browserMobPath);
            BasicAuthSettings.Server.Start();
            Proxy seleniumProxy = SetUpProxy(domain, username, password).Result;
			
			InternetExplorerOptions opts = new InternetExplorerOptions();
			opts.Proxy = seleniumProxy;
            return BasicAuthDriverRetriever.GetIEDriver(opts);
		}

        private ChromeDriver GetModifiedChromeDriver(String browserMobPath, String domain, String username, String password)
        {
            BasicAuthSettings.Server = new Server(browserMobPath);
            BasicAuthSettings.Server.Start();
            Proxy seleniumProxy = SetUpProxy(domain, username, password).Result;

            ChromeOptions opts = new ChromeOptions();
            opts.Proxy = seleniumProxy;
            return BasicAuthDriverRetriever.GetChromeDriver(opts);
        }

        protected async Task<Proxy> SetUpProxy(String domain, String username, String password)
        {
            Client client = BasicAuthSettings.Server.CreateProxy();

            String requestURL = "http://" + client.SeleniumProxy + "/proxy/8080/auth/basic/" + domain;
            BasicAuthObject authObj = new BasicAuthObject(username, password);
            String authObjJson = JsonConvert.SerializeObject(authObj);
            HttpClient requestClient = new HttpClient();
            HttpResponseMessage response = await requestClient.PostAsJsonAsync(requestURL, authObjJson);
            HttpContent responseContent = response.Content;
            return new Proxy { HttpProxy = client.SeleniumProxy };
        }

        public void Quit()
        {
            if (driver != null)
            {
                BasicAuthSettings.Server.Stop();
                driver.Quit();
            }
            else
            {
                Console.WriteLine("Driver is null, Quit does nothing unless the driver is accessed at lest once");
            }
        }
	}
}