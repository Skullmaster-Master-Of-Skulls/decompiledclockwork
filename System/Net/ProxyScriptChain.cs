using System;

namespace System.Net
{
	// Token: 0x02000507 RID: 1287
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x06002802 RID: 10242 RVA: 0x000A4F66 File Offset: 0x000A3F66
		internal ProxyScriptChain(WebProxy proxy, Uri destination) : base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000A4F78 File Offset: 0x000A3F78
		protected override bool GetNextProxy(out Uri proxy)
		{
			if (this.m_CurrentIndex < 0)
			{
				proxy = null;
				return false;
			}
			if (this.m_CurrentIndex == 0)
			{
				this.m_ScriptProxies = this.m_Proxy.GetProxiesAuto(base.Destination, ref this.m_SyncStatus);
			}
			if (this.m_ScriptProxies == null || this.m_CurrentIndex >= this.m_ScriptProxies.Length)
			{
				proxy = this.m_Proxy.GetProxyAutoFailover(base.Destination);
				this.m_CurrentIndex = -1;
				return true;
			}
			proxy = this.m_ScriptProxies[this.m_CurrentIndex++];
			return true;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000A5007 File Offset: 0x000A4007
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04002751 RID: 10065
		private WebProxy m_Proxy;

		// Token: 0x04002752 RID: 10066
		private Uri[] m_ScriptProxies;

		// Token: 0x04002753 RID: 10067
		private int m_CurrentIndex;

		// Token: 0x04002754 RID: 10068
		private int m_SyncStatus;
	}
}
