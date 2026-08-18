using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Results
{
	// Token: 0x02000058 RID: 88
	public class ExceptionResult : IHttpActionResult
	{
		// Token: 0x06000284 RID: 644 RVA: 0x000091D8 File Offset: 0x000073D8
		public ExceptionResult(Exception exception, bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(exception, new ExceptionResult.DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters))
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000091EC File Offset: 0x000073EC
		public ExceptionResult(Exception exception, ApiController controller) : this(exception, new ExceptionResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000091FB File Offset: 0x000073FB
		private ExceptionResult(Exception exception, ExceptionResult.IDependencyProvider dependencies)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this._exception = exception;
			this._dependencies = dependencies;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000921F File Offset: 0x0000741F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009227 File Offset: 0x00007427
		public bool IncludeErrorDetail
		{
			get
			{
				return this._dependencies.IncludeErrorDetail;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00009234 File Offset: 0x00007434
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00009241 File Offset: 0x00007441
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000924E File Offset: 0x0000744E
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000925B File Offset: 0x0000745B
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009268 File Offset: 0x00007468
		private HttpResponseMessage Execute()
		{
			HttpError content = new HttpError(this._exception, this._dependencies.IncludeErrorDetail);
			return NegotiatedContentResult<HttpError>.Execute(HttpStatusCode.InternalServerError, content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x040000B5 RID: 181
		private readonly Exception _exception;

		// Token: 0x040000B6 RID: 182
		private readonly ExceptionResult.IDependencyProvider _dependencies;

		// Token: 0x02000059 RID: 89
		internal interface IDependencyProvider
		{
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x0600028E RID: 654
			bool IncludeErrorDetail { get; }

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600028F RID: 655
			IContentNegotiator ContentNegotiator { get; }

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000290 RID: 656
			HttpRequestMessage Request { get; }

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000291 RID: 657
			IEnumerable<MediaTypeFormatter> Formatters { get; }
		}

		// Token: 0x0200005A RID: 90
		internal sealed class DirectDependencyProvider : ExceptionResult.IDependencyProvider
		{
			// Token: 0x06000292 RID: 658 RVA: 0x000092B8 File Offset: 0x000074B8
			public DirectDependencyProvider(bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			{
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
				this._includeErrorDetail = includeErrorDetail;
				this._contentNegotiator = contentNegotiator;
				this._request = request;
				this._formatters = formatters;
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00009313 File Offset: 0x00007513
			public bool IncludeErrorDetail
			{
				get
				{
					return this._includeErrorDetail;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000294 RID: 660 RVA: 0x0000931B File Offset: 0x0000751B
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					return this._contentNegotiator;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000295 RID: 661 RVA: 0x00009323 File Offset: 0x00007523
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000296 RID: 662 RVA: 0x0000932B File Offset: 0x0000752B
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					return this._formatters;
				}
			}

			// Token: 0x040000B7 RID: 183
			private readonly bool _includeErrorDetail;

			// Token: 0x040000B8 RID: 184
			private readonly IContentNegotiator _contentNegotiator;

			// Token: 0x040000B9 RID: 185
			private readonly HttpRequestMessage _request;

			// Token: 0x040000BA RID: 186
			private readonly IEnumerable<MediaTypeFormatter> _formatters;
		}

		// Token: 0x0200005B RID: 91
		internal sealed class ApiControllerDependencyProvider : ExceptionResult.IDependencyProvider
		{
			// Token: 0x06000297 RID: 663 RVA: 0x00009333 File Offset: 0x00007533
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000298 RID: 664 RVA: 0x00009350 File Offset: 0x00007550
			public bool IncludeErrorDetail
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.IncludeErrorDetail;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000299 RID: 665 RVA: 0x00009363 File Offset: 0x00007563
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.ContentNegotiator;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600029A RID: 666 RVA: 0x00009376 File Offset: 0x00007576
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600029B RID: 667 RVA: 0x00009389 File Offset: 0x00007589
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Formatters;
				}
			}

			// Token: 0x0600029C RID: 668 RVA: 0x0000939C File Offset: 0x0000759C
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
					HttpRequestContext requestContext = this._controller.RequestContext;
					bool includeErrorDetail = requestContext.IncludeErrorDetail;
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
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
					this._resolvedDependencies = new ExceptionResult.DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters);
				}
			}

			// Token: 0x040000BB RID: 187
			private readonly ApiController _controller;

			// Token: 0x040000BC RID: 188
			private ExceptionResult.IDependencyProvider _resolvedDependencies;
		}
	}
}
