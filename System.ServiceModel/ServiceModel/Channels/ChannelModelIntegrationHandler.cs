using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200086B RID: 2155
	internal class ChannelModelIntegrationHandler : HttpMessageHandler
	{
		// Token: 0x0600515C RID: 20828 RVA: 0x0012B714 File Offset: 0x00129914
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw FxTrace.Exception.ArgumentNull("request");
			}
			cancellationToken.ThrowIfCancellationRequested();
			HttpChannelUtilities.EnsureHttpRequestMessageContentNotNull(request);
			HttpPipeline httpPipeline = HttpPipeline.GetHttpPipeline(request);
			return httpPipeline.Dispatch(request);
		}
	}
}
