using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000006 RID: 6
	public class DigestHttpClientBuilder : IHttpClientBuilder
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002214 File Offset: 0x00000414
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000221C File Offset: 0x0000041C
		public MediaTypeFormatter DefaultMediaTypeFormatter { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002225 File Offset: 0x00000425
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000222D File Offset: 0x0000042D
		public string ServiceAddress { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002236 File Offset: 0x00000436
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000223E File Offset: 0x0000043E
		public string DefaultAddressSuffix { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002247 File Offset: 0x00000447
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002250 File Offset: 0x00000450
		public HttpClient CreateHttpClient(IUserCredentials userCredentials)
		{
			DigestCredentials digestCredentials = userCredentials as DigestCredentials;
			if (digestCredentials == null)
			{
				throw new SecurityException("Authentication Header cannot be created for Digest Authentication if not username and password credentials are provided");
			}
			CredentialCache credentials = new CredentialCache
			{
				{
					new Uri(this.ServiceAddress),
					this.AuthenticationType,
					new NetworkCredential(digestCredentials.UserName, digestCredentials.Password)
				}
			};
			return new HttpClient(new HttpClientHandler
			{
				Credentials = credentials,
				PreAuthenticate = true
			})
			{
				BaseAddress = new Uri(this.ServiceAddress)
			};
		}
	}
}
