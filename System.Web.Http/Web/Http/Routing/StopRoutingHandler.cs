using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Routing
{
	// Token: 0x0200009E RID: 158
	public sealed class StopRoutingHandler : HttpMessageHandler
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}
	}
}
