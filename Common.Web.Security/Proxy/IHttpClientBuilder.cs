using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000007 RID: 7
	public interface IHttpClientBuilder
	{
		// Token: 0x06000022 RID: 34
		HttpClient CreateHttpClient(IUserCredentials secCredentials);

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35
		string AuthenticationType { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36
		// (set) Token: 0x06000025 RID: 37
		MediaTypeFormatter DefaultMediaTypeFormatter { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38
		// (set) Token: 0x06000027 RID: 39
		string ServiceAddress { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		string DefaultAddressSuffix { get; set; }
	}
}
