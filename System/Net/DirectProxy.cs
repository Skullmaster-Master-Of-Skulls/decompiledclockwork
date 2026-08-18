using System;

namespace System.Net
{
	// Token: 0x02000508 RID: 1288
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x06002805 RID: 10245 RVA: 0x000A501A File Offset: 0x000A401A
		internal DirectProxy(Uri destination) : base(destination)
		{
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000A5023 File Offset: 0x000A4023
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = null;
			if (this.m_ProxyRetrieved)
			{
				return false;
			}
			this.m_ProxyRetrieved = true;
			return true;
		}

		// Token: 0x04002755 RID: 10069
		private bool m_ProxyRetrieved;
	}
}
