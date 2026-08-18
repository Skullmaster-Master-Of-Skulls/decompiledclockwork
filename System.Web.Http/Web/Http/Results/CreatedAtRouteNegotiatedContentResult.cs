using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Results
{
	// Token: 0x02000050 RID: 80
	public class CreatedAtRouteNegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00008A80 File Offset: 0x00006C80
		public CreatedAtRouteNegotiatedContentResult(string routeName, IDictionary<string, object> routeValues, T content, UrlHelper urlFactory, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(routeName, routeValues, content, new CreatedAtRouteNegotiatedContentResult<T>.DirectDependencyProvider(urlFactory, contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008A98 File Offset: 0x00006C98
		public CreatedAtRouteNegotiatedContentResult(string routeName, IDictionary<string, object> routeValues, T content, ApiController controller) : this(routeName, routeValues, content, new CreatedAtRouteNegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008AAA File Offset: 0x00006CAA
		private CreatedAtRouteNegotiatedContentResult(string routeName, IDictionary<string, object> routeValues, T content, CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			if (routeName == null)
			{
				throw new ArgumentNullException("routeName");
			}
			this._routeName = routeName;
			this._routeValues = routeValues;
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00008ADD File Offset: 0x00006CDD
		public string RouteName
		{
			get
			{
				return this._routeName;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008AE5 File Offset: 0x00006CE5
		public IDictionary<string, object> RouteValues
		{
			get
			{
				return this._routeValues;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008AED File Offset: 0x00006CED
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00008AF5 File Offset: 0x00006CF5
		public UrlHelper UrlFactory
		{
			get
			{
				return this._dependencies.UrlFactory;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008B02 File Offset: 0x00006D02
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00008B0F File Offset: 0x00006D0F
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008B1C File Offset: 0x00006D1C
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008B29 File Offset: 0x00006D29
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008B38 File Offset: 0x00006D38
		private HttpResponseMessage Execute()
		{
			ContentNegotiationResult contentNegotiationResult = this._dependencies.ContentNegotiator.Negotiate(typeof(T), this._dependencies.Request, this._dependencies.Formatters);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			try
			{
				if (contentNegotiationResult == null)
				{
					httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
				}
				else
				{
					httpResponseMessage.StatusCode = HttpStatusCode.Created;
					string text = this._dependencies.UrlFactory.Link(this._routeName, this._routeValues);
					if (text == null)
					{
						throw new InvalidOperationException(SRResources.UrlHelper_LinkMustNotReturnNull);
					}
					httpResponseMessage.Headers.Location = new Uri(text);
					httpResponseMessage.Content = new ObjectContent<T>(this._content, contentNegotiationResult.Formatter, contentNegotiationResult.MediaType);
				}
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x040000A2 RID: 162
		private readonly string _routeName;

		// Token: 0x040000A3 RID: 163
		private readonly IDictionary<string, object> _routeValues;

		// Token: 0x040000A4 RID: 164
		private readonly T _content;

		// Token: 0x040000A5 RID: 165
		private readonly CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider _dependencies;

		// Token: 0x02000051 RID: 81
		private interface IDependencyProvider
		{
			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000256 RID: 598
			UrlHelper UrlFactory { get; }

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000257 RID: 599
			IContentNegotiator ContentNegotiator { get; }

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000258 RID: 600
			HttpRequestMessage Request { get; }

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000259 RID: 601
			IEnumerable<MediaTypeFormatter> Formatters { get; }
		}

		// Token: 0x02000052 RID: 82
		private sealed class DirectDependencyProvider : CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x0600025A RID: 602 RVA: 0x00008C20 File Offset: 0x00006E20
			public DirectDependencyProvider(UrlHelper urlFactory, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			{
				if (urlFactory == null)
				{
					throw new ArgumentNullException("urlFactory");
				}
				if (contentNegotiator == null)
				{
					throw new ArgumentNullException("contentNegotiator");
				}
				if (request == null)
				{
					throw new ArgumentNullException("request");
				}
				if (formatters == null)
				{
					throw new ArgumentNullException("formatters");
				}
				this._urlFactory = urlFactory;
				this._contentNegotiator = contentNegotiator;
				this._request = request;
				this._formatters = formatters;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x0600025B RID: 603 RVA: 0x00008C89 File Offset: 0x00006E89
			public UrlHelper UrlFactory
			{
				get
				{
					return this._urlFactory;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600025C RID: 604 RVA: 0x00008C91 File Offset: 0x00006E91
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					return this._contentNegotiator;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600025D RID: 605 RVA: 0x00008C99 File Offset: 0x00006E99
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x0600025E RID: 606 RVA: 0x00008CA1 File Offset: 0x00006EA1
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					return this._formatters;
				}
			}

			// Token: 0x040000A6 RID: 166
			private readonly UrlHelper _urlFactory;

			// Token: 0x040000A7 RID: 167
			private readonly IContentNegotiator _contentNegotiator;

			// Token: 0x040000A8 RID: 168
			private readonly HttpRequestMessage _request;

			// Token: 0x040000A9 RID: 169
			private readonly IEnumerable<MediaTypeFormatter> _formatters;
		}

		// Token: 0x02000053 RID: 83
		private sealed class ApiControllerDependencyProvider : CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x0600025F RID: 607 RVA: 0x00008CA9 File Offset: 0x00006EA9
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000260 RID: 608 RVA: 0x00008CC6 File Offset: 0x00006EC6
			public UrlHelper UrlFactory
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.UrlFactory;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000261 RID: 609 RVA: 0x00008CD9 File Offset: 0x00006ED9
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.ContentNegotiator;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000262 RID: 610 RVA: 0x00008CEC File Offset: 0x00006EEC
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000263 RID: 611 RVA: 0x00008CFF File Offset: 0x00006EFF
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Formatters;
				}
			}

			// Token: 0x06000264 RID: 612 RVA: 0x00008D14 File Offset: 0x00006F14
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
					HttpConfiguration configuration = this._controller.Configuration;
					if (configuration == null)
					{
						throw new InvalidOperationException(SRResources.HttpControllerContext_ConfigurationMustNotBeNull);
					}
					ServicesContainer services = configuration.Services;
					IContentNegotiator contentNegotiator = services.GetContentNegotiator();
					if (contentNegotiator == null)
					{
						throw new InvalidOperationException(Error.Format(SRResources.HttpRequestMessageExtensions_NoContentNegotiator, new object[]
						{
							typeof(IContentNegotiator)
						}));
					}
					IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
					this._resolvedDependencies = new CreatedAtRouteNegotiatedContentResult<T>.DirectDependencyProvider(urlFactory, contentNegotiator, request, formatters);
				}
			}

			// Token: 0x040000AA RID: 170
			private readonly ApiController _controller;

			// Token: 0x040000AB RID: 171
			private CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider _resolvedDependencies;
		}
	}
}
