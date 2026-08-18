using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000004 RID: 4
	public class BearerHttpClientBuilder : IHttpClientBuilder
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002150 File Offset: 0x00000350
		public HttpClient CreateHttpClient(IUserCredentials userCredentials)
		{
			TokenUserCredentials tokenUserCredentials = userCredentials as TokenUserCredentials;
			if (tokenUserCredentials == null)
			{
				throw new SecurityException("Authentication Header cannot be created for Bearer Authentication if not token credentials are provided");
			}
			return new HttpClient(new BearerHttpHandler(this.AuthenticationType, tokenUserCredentials))
			{
				BaseAddress = new Uri(this.ServiceAddress)
			};
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002194 File Offset: 0x00000394
		public string AuthenticationType
		{
			get
			{
				return "Bearer";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000219B File Offset: 0x0000039B
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021A3 File Offset: 0x000003A3
		public MediaTypeFormatter DefaultMediaTypeFormatter { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021AC File Offset: 0x000003AC
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021B4 File Offset: 0x000003B4
		public string ServiceAddress { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021BD File Offset: 0x000003BD
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021C5 File Offset: 0x000003C5
		public string DefaultAddressSuffix { get; set; }
	}
}
