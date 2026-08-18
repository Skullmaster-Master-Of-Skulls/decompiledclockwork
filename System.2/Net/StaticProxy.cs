using System;

namespace System.Net
{
	// Token: 0x020001E2 RID: 482
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x000638DA File Offset: 0x00061ADA
		internal StaticProxy(Uri destination, Uri proxy) : base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000638FE File Offset: 0x00061AFE
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

		// Token: 0x04001525 RID: 5413
		private Uri m_Proxy;
	}
}
