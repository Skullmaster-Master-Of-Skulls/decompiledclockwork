using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000005 RID: 5
	internal class BearerHttpHandler : MessageProcessingHandler
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000021CE File Offset: 0x000003CE
		internal BearerHttpHandler(string scheme, TokenUserCredentials tokenCredentials) : base(new HttpClientHandler())
		{
			this._scheme = scheme;
			this.Credentials = tokenCredentials;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021E9 File Offset: 0x000003E9
		protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HttpRequestHeaders headers = request.Headers;
			string scheme = this._scheme;
			TokenUserCredentials credentials = this.Credentials;
			headers.Authorization = new AuthenticationHeaderValue(scheme, (credentials != null) ? credentials.Token : null);
			return request;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000214D File Offset: 0x0000034D
		protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			return response;
		}

		// Token: 0x04000009 RID: 9
		protected readonly string _scheme;

		// Token: 0x0400000A RID: 10
		protected TokenUserCredentials Credentials;
	}
}
