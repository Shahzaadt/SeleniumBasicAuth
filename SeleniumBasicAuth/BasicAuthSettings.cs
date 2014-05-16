using AutomatedTester.BrowserMob;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasicAuth
{
    public static class BasicAuthSettings
    {
        public static String DefaultBrowserMobDirectory
        {
            get
            {
                return Path.Combine(BasicAuthSettings.DefaultBrowserMobBaseDirectory,"bin");
            }
        }
        private static String _browserMobDirectory = BasicAuthSettings.DefaultBrowserMobDirectory;
        public static String BrowserMobDirectory
        {
            get
            {
                return BasicAuthSettings._browserMobDirectory;
            }
            set
            {
                BasicAuthSettings._browserMobDirectory = value;
            }
        }

        public static String DefaultBrowserMobBaseDirectory
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "browsermob");
            }
        }

        public readonly static String DefaultBrowserMobFile = "browsermob-proxy.bat";
        private static String _browserMobFile = BasicAuthSettings.DefaultBrowserMobFile;
        public static String BrowserMobFile
        {
            get
            {
                return BasicAuthSettings._browserMobFile;
            }
            set
            {
                BasicAuthSettings._browserMobFile = value;
            }
        }

        public static String BrowserMobPath
        {
            get
            {
                BasicAuthSettings.VerifyBrowserMobExists();
                return Path.Combine(BasicAuthSettings.BrowserMobDirectory, BasicAuthSettings.BrowserMobFile);
            }
        }

        public readonly static String DefaultDomain = "localhost";
        private static String _domain = BasicAuthSettings.DefaultDomain;
        public static String Domain
        {
            get
            {
                return BasicAuthSettings._domain;
            }
            set
            {
                BasicAuthSettings._domain = value;
            }
        }

        public readonly static String DefaultUsername = "user";
        private static String _username = BasicAuthSettings.DefaultUsername;
        public static String Username
        {
            get
            {
                return BasicAuthSettings._username;
            }
            set
            {
                BasicAuthSettings._username = value;
            }
        }

        public readonly static String DefaultPassword = "password";
        private static String _password = BasicAuthSettings.DefaultPassword;
        public static String Password
        {
            get
            {
                return BasicAuthSettings._password;
            }
            set
            {
                BasicAuthSettings._password = value;
            }
        }

        private static Server _server;
        public static Server Server
        {
            get
            {
                if (BasicAuthSettings._server == null)
                {
                    BasicAuthSettings._server = new Server(BasicAuthSettings.BrowserMobDirectory);
                }
                return BasicAuthSettings._server;
            }
            set
            {
                BasicAuthSettings._server = value;
            }
        }

        public readonly static BrowserType DefaultBrowserType = BrowserType.Firefox;
        private static BrowserType _browserType = BasicAuthSettings.DefaultBrowserType;
        public static BrowserType BrowserType
        {
            get
            {
                return BasicAuthSettings._browserType;
            }
            set
            {
                BasicAuthSettings._browserType = value;
            }
        }

        private static String _ZipFileName = "browsermob.zip";
        private static String _ZipFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, BasicAuthSettings._ZipFileName);
            }
        }

        public static void VerifyBrowserMobExists()
        {
            if (Directory.Exists(BasicAuthSettings.BrowserMobDirectory) && File.Exists(Path.Combine(BasicAuthSettings.BrowserMobDirectory, BasicAuthSettings.BrowserMobFile)))
            {
                //do nothing -- browsermob is already installed
                Console.WriteLine("Browsermob Already Installed.");
            }
            else
            {
                if(Directory.Exists(BasicAuthSettings.BrowserMobDirectory))
                {
                    Directory.Delete(BasicAuthSettings.BrowserMobDirectory, true);
                }
                Console.WriteLine("getting zip file...");
                string browserMobReleaseAddress = "https://s3-us-west-1.amazonaws.com/lightbody-bmp/browsermob-proxy-2.0-beta-9-bin.zip";
                WebClient client = new WebClient();
                Console.WriteLine("downloading file...");
                client.DownloadFile(browserMobReleaseAddress, BasicAuthSettings._ZipFilePath);
                Console.WriteLine("file downloaded!");
                FastZip fz = new FastZip();
                fz.ExtractZip(BasicAuthSettings._ZipFilePath, AppDomain.CurrentDomain.SetupInformation.ApplicationBase, null);
                if (Directory.Exists(BasicAuthSettings.DefaultBrowserMobBaseDirectory))
                {
                    Directory.Delete(BasicAuthSettings.DefaultBrowserMobBaseDirectory, true);
                }
                DirectoryInfo di = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "browsermob-proxy-2.0-beta-9"));
                di.MoveTo(BasicAuthSettings.DefaultBrowserMobBaseDirectory);
            }
        }
    }
}
