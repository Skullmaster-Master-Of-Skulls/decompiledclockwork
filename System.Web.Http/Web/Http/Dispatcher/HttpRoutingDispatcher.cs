using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Hosting;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000B4 RID: 180
	public class HttpRoutingDispatcher : HttpMessageHandler
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000CAEE File Offset: 0x0000ACEE
		public HttpRoutingDispatcher(HttpConfiguration configuration) : this(configuration, new HttpControllerDispatcher(configuration))
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CAFD File Offset: 0x0000ACFD
		public HttpRoutingDispatcher(HttpConfiguration configuration, HttpMessageHandler defaultHandler)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (defaultHandler == null)
			{
				throw Error.ArgumentNull("defaultHandler");
			}
			this._configuration = configuration;
			this._defaultInvoker = new HttpMessageInvoker(defaultHandler);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000CB34 File Offset: 0x0000AD34
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			IHttpRouteData routeData = request.GetRouteData();
			if (routeData == null)
			{
				routeData = this._configuration.Routes.GetRouteData(request);
				if (routeData != null)
				{
					request.SetRouteData(routeData);
				}
			}
			if (routeData == null || (routeData.Route != null && routeData.Route.Handler is StopRoutingHandler))
			{
				request.Properties.Add(HttpPropertyKeys.NoRouteMatched, true);
				return Task.FromResult<HttpResponseMessage>(request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
				{
					request.RequestUri
				}), SRResources.NoRouteData));
			}
			routeData.RemoveOptionalRoutingParameters();
			HttpMessageInvoker httpMessageInvoker = (routeData.Route == null || routeData.Route.Handler == null) ? this._defaultInvoker : new HttpMessageInvoker(routeData.Route.Handler, false);
			return httpMessageInvoker.SendAsync(request, cancellationToken);
		}

		// Token: 0x0400012E RID: 302
		private readonly HttpConfiguration _configuration;

		// Token: 0x0400012F RID: 303
		private readonly HttpMessageInvoker _defaultInvoker;
	}
}
