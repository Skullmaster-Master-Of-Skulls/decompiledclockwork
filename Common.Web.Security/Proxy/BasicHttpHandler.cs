using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using TechnoPro.Common.Web.Security.Credentials;

namespace TechnoPro.Common.Web.Security.Proxy
{
	// Token: 0x02000003 RID: 3
	internal class BasicHttpHandler : MessageProcessingHandler
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020E0 File Offset: 0x000002E0
		internal BasicHttpHandler(string scheme, string serviceAddress, UserNameCredentials userNameCredentials) : base(new HttpClientHandler
		{
			Credentials = new CredentialCache
			{
				{
					new Uri(serviceAddress),
					scheme,
					new NetworkCredential(userNameCredentials.UserName, userNameCredentials.Password)
				}
			}
		})
		{
			this._scheme = scheme;
			this.Credentials = userNameCredentials;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000212F File Offset: 0x0000032F
		protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request.Headers.Add("clientId", this.Credentials.ClientId);
			return request;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000214D File Offset: 0x0000034D
		protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			return response;
		}

		// Token: 0x04000004 RID: 4
		protected readonly string _scheme;

		// Token: 0x04000005 RID: 5
		protected UserNameCredentials Credentials;
	}
}
