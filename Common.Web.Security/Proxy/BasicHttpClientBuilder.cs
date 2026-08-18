using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000002 RID: 2
	public class BasicHttpClientBuilder : IHttpClientBuilder
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000205F File Offset: 0x0000025F
		public MediaTypeFormatter DefaultMediaTypeFormatter { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public string ServiceAddress { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002079 File Offset: 0x00000279
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002081 File Offset: 0x00000281
		public string DefaultAddressSuffix { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x0000208C File Offset: 0x0000028C
		public HttpClient CreateHttpClient(IUserCredentials secCredentials)
		{
			UserNameCredentials userNameCredentials = secCredentials as UserNameCredentials;
			if (userNameCredentials == null)
			{
				throw new SecurityException("Authentication Header cannot be created for Basic Authentication if not username and password credentials are provided");
			}
			return new HttpClient(new BasicHttpHandler(this.AuthenticationType, this.ServiceAddress, userNameCredentials))
			{
				BaseAddress = new Uri(this.ServiceAddress)
			};
		}
	}
}
