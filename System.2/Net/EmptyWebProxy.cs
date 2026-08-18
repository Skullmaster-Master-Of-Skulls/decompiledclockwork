using System;

namespace System.Net
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x0005BBD2 File Offset: 0x00059DD2
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0005BBD5 File Offset: 0x00059DD5
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x0005BBE0 File Offset: 0x00059DE0
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

		// Token: 0x060010FA RID: 4346 RVA: 0x0005BBE9 File Offset: 0x00059DE9
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x040013F1 RID: 5105
		[NonSerialized]
		private ICredentials m_credentials;
	}
}
