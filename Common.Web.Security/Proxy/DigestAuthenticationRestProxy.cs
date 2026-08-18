using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x0200000A RID: 10
	public abstract class DigestAuthenticationRestProxy<T> : RestProxy<T>, IWebService where T : IWebService
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003562 File Offset: 0x00001762
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000356A File Offset: 0x0000176A
		protected override string DefaultAuthenticationMethod { get; set; } = "Digest";

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003574 File Offset: 0x00001774
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000035A9 File Offset: 0x000017A9
		public new DigestCredentials ClientCredentials
		{
			get
			{
				DigestCredentials result;
				if ((result = (this._userSecCredentials as DigestCredentials)) == null)
				{
					result = (DigestCredentials)(this._userSecCredentials = ObjectFactory.Resolve<IUserCredentials>(this.DefaultAuthenticationMethod));
				}
				return result;
			}
			set
			{
				this._userSecCredentials = value;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000035B2 File Offset: 0x000017B2
		protected DigestAuthenticationRestProxy(string serviceAddress, DigestCredentials credentials) : base(serviceAddress, "Digest", credentials)
		{
			this.ClientCredentials = credentials;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035D3 File Offset: 0x000017D3
		protected DigestAuthenticationRestProxy(string serviceAddress, string serviceAddressSuffix, DigestCredentials credentials) : base(serviceAddress, serviceAddressSuffix, "Digest", credentials)
		{
			this.ClientCredentials = credentials;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035F8 File Offset: 0x000017F8
		protected override void SetProxyProperties(HttpClient httpClient)
		{
			base.SetProxyProperties(httpClient);
			HttpRequestHeaders defaultRequestHeaders = httpClient.DefaultRequestHeaders;
			if (!defaultRequestHeaders.Contains("clientId"))
			{
				defaultRequestHeaders.Add("clientId", this.ClientCredentials.ClientId);
			}
			if (!defaultRequestHeaders.Contains("audienceUri"))
			{
				defaultRequestHeaders.Add("audienceUri", this.ClientCredentials.AudienceUri);
			}
		}
	}
}
