using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coypu;

namespace SeleniumBasicAuth
{
    public class BasicAuthBrowserSession : BrowserSession
    {
        public BasicAuthBrowserSession()
            : base() { }

        public BasicAuthBrowserSession(SessionConfiguration config)
            : base(config) { }

        public new void Dispose()
        {
            BasicAuthSettings.Server.Stop();
            base.Dispose();
        }
    }
}
