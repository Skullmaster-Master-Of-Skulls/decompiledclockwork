using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Results
{
	// Token: 0x0200004B RID: 75
	public class RedirectToRouteResult : IHttpActionResult
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000087DC File Offset: 0x000069DC
		public RedirectToRouteResult(string routeName, IDictionary<string, object> routeValues, UrlHelper urlFactory, HttpRequestMessage request) : this(routeName, routeValues, new RedirectToRouteResult.DirectDependencyProvider(urlFactory, request))
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000087EE File Offset: 0x000069EE
		public RedirectToRouteResult(string routeName, IDictionary<string, object> routeValues, ApiController controller) : this(routeName, routeValues, new RedirectToRouteResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000087FE File Offset: 0x000069FE
		private RedirectToRouteResult(string routeName, IDictionary<string, object> routeValues, RedirectToRouteResult.IDependencyProvider dependencies)
		{
			if (routeName == null)
			{
				throw new ArgumentNullException("routeName");
			}
			this._routeName = routeName;
			this._routeValues = routeValues;
			this._dependencies = dependencies;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008829 File Offset: 0x00006A29
		public string RouteName
		{
			get
			{
				return this._routeName;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008831 File Offset: 0x00006A31
		public IDictionary<string, object> RouteValues
		{
			get
			{
				return this._routeValues;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00008839 File Offset: 0x00006A39
		public UrlHelper UrlFactory
		{
			get
			{
				return this._dependencies.UrlFactory;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008846 File Offset: 0x00006A46
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008853 File Offset: 0x00006A53
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008860 File Offset: 0x00006A60
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Found);
			try
			{
				string text = this._dependencies.UrlFactory.Link(this._routeName, this._routeValues);
				if (text == null)
				{
					throw new InvalidOperationException(SRResources.UrlHelper_LinkMustNotReturnNull);
				}
				httpResponseMessage.Headers.Location = new Uri(text);
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000099 RID: 153
		private readonly string _routeName;

		// Token: 0x0400009A RID: 154
		private readonly IDictionary<string, object> _routeValues;

		// Token: 0x0400009B RID: 155
		private readonly RedirectToRouteResult.IDependencyProvider _dependencies;

		// Token: 0x0200004C RID: 76
		private interface IDependencyProvider
		{
			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600023A RID: 570
			UrlHelper UrlFactory { get; }

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x0600023B RID: 571
			HttpRequestMessage Request { get; }
		}

		// Token: 0x0200004D RID: 77
		private sealed class DirectDependencyProvider : RedirectToRouteResult.IDependencyProvider
		{
			// Token: 0x0600023C RID: 572 RVA: 0x000088E4 File Offset: 0x00006AE4
			public DirectDependencyProvider(UrlHelper urlFactory, HttpRequestMessage request)
			{
				if (urlFactory == null)
				{
					throw new ArgumentNullException("urlFactory");
				}
				if (request == null)
				{
					throw new ArgumentNullException("request");
				}
				this._urlFactory = urlFactory;
				this._request = request;
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x0600023D RID: 573 RVA: 0x00008916 File Offset: 0x00006B16
			public UrlHelper UrlFactory
			{
				get
				{
					return this._urlFactory;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600023E RID: 574 RVA: 0x0000891E File Offset: 0x00006B1E
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x0400009C RID: 156
			private readonly UrlHelper _urlFactory;

			// Token: 0x0400009D RID: 157
			private readonly HttpRequestMessage _request;
		}

		// Token: 0x0200004E RID: 78
		private sealed class ApiControllerDependencyProvider : RedirectToRouteResult.IDependencyProvider
		{
			// Token: 0x0600023F RID: 575 RVA: 0x00008926 File Offset: 0x00006B26
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000240 RID: 576 RVA: 0x00008943 File Offset: 0x00006B43
			public UrlHelper UrlFactory
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.UrlFactory;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000241 RID: 577 RVA: 0x00008956 File Offset: 0x00006B56
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x06000242 RID: 578 RVA: 0x0000896C File Offset: 0x00006B6C
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					UrlHelper urlFactory = this._controller.Url ?? new UrlHelper(request);
					this._resolvedDependencies = new RedirectToRouteResult.DirectDependencyProvider(urlFactory, request);
				}
			}

			// Token: 0x0400009E RID: 158
			private readonly ApiController _controller;

			// Token: 0x0400009F RID: 159
			private RedirectToRouteResult.IDependencyProvider _resolvedDependencies;
		}
	}
}
