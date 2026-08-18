using System;

namespace System.Net
{
	// Token: 0x020004D9 RID: 1241
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x0009DF02 File Offset: 0x0009CF02
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0009DF05 File Offset: 0x0009CF05
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x0009DF08 File Offset: 0x0009CF08
		// (set) Token: 0x060026A0 RID: 9888 RVA: 0x0009DF10 File Offset: 0x0009CF10
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0009DF19 File Offset: 0x0009CF19
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x0400263A RID: 9786
		[NonSerialized]
		private ICredentials m_credentials;
	}
}
