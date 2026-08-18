using System;

namespace System.Net
{
	// Token: 0x020001E1 RID: 481
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x060012CF RID: 4815 RVA: 0x000638BA File Offset: 0x00061ABA
		internal DirectProxy(Uri destination) : base(destination)
		{
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000638C3 File Offset: 0x00061AC3
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

		// Token: 0x04001524 RID: 5412
		private bool m_ProxyRetrieved;
	}
}
