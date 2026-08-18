using System;
using System.Web;

namespace TechnoPro.ClockWorkServer.Client.Services.Pool
{
	// Token: 0x02000174 RID: 372
	public class WcfConnectionPoolHandler : IHttpModule
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x000258F7 File Offset: 0x00023AF7
		public WcfConnectionPoolHandler()
		{
			ProxyConnectionPool.EnableConnectionPool = true;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00025908 File Offset: 0x00023B08
		public void Dispose()
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002590B File Offset: 0x00023B0B
		public void Init(HttpApplication context)
		{
			context.EndRequest += this.context_EndRequest;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00025924 File Offset: 0x00023B24
		private void context_EndRequest(object sender, EventArgs e)
		{
			bool enableConnectionPool = ProxyConnectionPool.EnableConnectionPool;
			if (enableConnectionPool)
			{
				ProxyConnectionPool.Current.Dispose();
			}
		}
	}
}
