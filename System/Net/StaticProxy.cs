using System;

namespace System.Net
{
	// Token: 0x02000509 RID: 1289
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x06002807 RID: 10247 RVA: 0x000A503A File Offset: 0x000A403A
		internal StaticProxy(Uri destination, Uri proxy) : base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000A505E File Offset: 0x000A405E
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = this.m_Proxy;
			if (proxy == null)
			{
				return false;
			}
			this.m_Proxy = null;
			return true;
		}

		// Token: 0x04002756 RID: 10070
		private Uri m_Proxy;
	}
}
