using System;

namespace System.Net
{
	// Token: 0x020001E0 RID: 480
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x00063805 File Offset: 0x00061A05
		internal ProxyScriptChain(WebProxy proxy, Uri destination) : base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00063818 File Offset: 0x00061A18
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
			Uri[] scriptProxies = this.m_ScriptProxies;
			int currentIndex = this.m_CurrentIndex;
			this.m_CurrentIndex = currentIndex + 1;
			proxy = scriptProxies[currentIndex];
			return true;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000638A7 File Offset: 0x00061AA7
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04001520 RID: 5408
		private WebProxy m_Proxy;

		// Token: 0x04001521 RID: 5409
		private Uri[] m_ScriptProxies;

		// Token: 0x04001522 RID: 5410
		private int m_CurrentIndex;

		// Token: 0x04001523 RID: 5411
		private int m_SyncStatus;
	}
}
