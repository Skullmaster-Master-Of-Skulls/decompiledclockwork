using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000885 RID: 2181
	internal abstract class WebSocketConnectionHandler : HttpMessageHandler
	{
		// Token: 0x060052D2 RID: 21202 RVA: 0x001313C8 File Offset: 0x0012F5C8
		protected internal virtual HttpResponseMessage AcceptWebSocket(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (this.AcceptWebSocket(request))
			{
				return WebSocketConnectionHandler.GetWebSocketAcceptedResponseMessage(request);
			}
			return WebSocketConnectionHandler.GetUpgradeRequiredResponseMessage(request);
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x001313E0 File Offset: 0x0012F5E0
		protected internal virtual bool AcceptWebSocket(HttpRequestMessage request)
		{
			return true;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x001313E4 File Offset: 0x0012F5E4
		protected static HttpResponseMessage GetUpgradeRequiredResponseMessage(HttpRequestMessage request)
		{
			return new HttpResponseMessage(HttpStatusCode.UpgradeRequired)
			{
				RequestMessage = request
			};
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x00131404 File Offset: 0x0012F604
		protected static HttpResponseMessage GetBadRequestResponseMessage(HttpRequestMessage request)
		{
			return new HttpResponseMessage(HttpStatusCode.BadRequest)
			{
				RequestMessage = request
			};
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00131424 File Offset: 0x0012F624
		protected static HttpResponseMessage GetWebSocketAcceptedResponseMessage(HttpRequestMessage request)
		{
			return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols)
			{
				RequestMessage = request
			};
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00131444 File Offset: 0x0012F644
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw FxTrace.Exception.ArgumentNull("request");
			}
			return Task.Factory.StartNew<HttpResponseMessage>(() => this.AcceptWebSocket(request, cancellationToken), cancellationToken);
		}
	}
}
