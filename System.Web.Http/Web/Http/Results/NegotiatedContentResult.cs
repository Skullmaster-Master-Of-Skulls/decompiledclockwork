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
	// Token: 0x02000064 RID: 100
	public class NegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x000099C9 File Offset: 0x00007BC9
		public NegotiatedContentResult(HttpStatusCode statusCode, T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(statusCode, content, new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000099DD File Offset: 0x00007BDD
		public NegotiatedContentResult(HttpStatusCode statusCode, T content, ApiController controller) : this(statusCode, content, new NegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000099ED File Offset: 0x00007BED
		private NegotiatedContentResult(HttpStatusCode statusCode, T content, NegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			this._statusCode = statusCode;
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00009A0A File Offset: 0x00007C0A
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00009A12 File Offset: 0x00007C12
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00009A1A File Offset: 0x00007C1A
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00009A27 File Offset: 0x00007C27
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00009A34 File Offset: 0x00007C34
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009A41 File Offset: 0x00007C41
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009A4E File Offset: 0x00007C4E
		private HttpResponseMessage Execute()
		{
			return NegotiatedContentResult<T>.Execute(this._statusCode, this._content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009A84 File Offset: 0x00007C84
		internal static HttpResponseMessage Execute(HttpStatusCode statusCode, T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			ContentNegotiationResult contentNegotiationResult = contentNegotiator.Negotiate(typeof(T), request, formatters);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			try
			{
				if (contentNegotiationResult == null)
				{
					httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
				}
				else
				{
					httpResponseMessage.StatusCode = statusCode;
					httpResponseMessage.Content = new ObjectContent<T>(content, contentNegotiationResult.Formatter, contentNegotiationResult.MediaType);
				}
				httpResponseMessage.RequestMessage = request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x040000CE RID: 206
		private readonly HttpStatusCode _statusCode;

		// Token: 0x040000CF RID: 207
		private readonly T _content;

		// Token: 0x040000D0 RID: 208
		private readonly NegotiatedContentResult<T>.IDependencyProvider _dependencies;

		// Token: 0x02000065 RID: 101
		internal interface IDependencyProvider
		{
			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060002E3 RID: 739
			IContentNegotiator ContentNegotiator { get; }

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060002E4 RID: 740
			HttpRequestMessage Request { get; }

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060002E5 RID: 741
			IEnumerable<MediaTypeFormatter> Formatters { get; }
		}

		// Token: 0x02000066 RID: 102
		internal sealed class DirectDependencyProvider : NegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x00009B00 File Offset: 0x00007D00
			public DirectDependencyProvider(IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
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
				this._contentNegotiator = contentNegotiator;
				this._request = request;
				this._formatters = formatters;
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009B52 File Offset: 0x00007D52
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					return this._contentNegotiator;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009B5A File Offset: 0x00007D5A
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009B62 File Offset: 0x00007D62
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					return this._formatters;
				}
			}

			// Token: 0x040000D1 RID: 209
			private readonly IContentNegotiator _contentNegotiator;

			// Token: 0x040000D2 RID: 210
			private readonly HttpRequestMessage _request;

			// Token: 0x040000D3 RID: 211
			private readonly IEnumerable<MediaTypeFormatter> _formatters;
		}

		// Token: 0x02000067 RID: 103
		internal sealed class ApiControllerDependencyProvider : NegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x060002EA RID: 746 RVA: 0x00009B6A File Offset: 0x00007D6A
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060002EB RID: 747 RVA: 0x00009B87 File Offset: 0x00007D87
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.ContentNegotiator;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060002EC RID: 748 RVA: 0x00009B9A File Offset: 0x00007D9A
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060002ED RID: 749 RVA: 0x00009BAD File Offset: 0x00007DAD
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Formatters;
				}
			}

			// Token: 0x060002EE RID: 750 RVA: 0x00009BC0 File Offset: 0x00007DC0
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
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
					this._resolvedDependencies = new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters);
				}
			}

			// Token: 0x040000D4 RID: 212
			private readonly ApiController _controller;

			// Token: 0x040000D5 RID: 213
			private NegotiatedContentResult<T>.IDependencyProvider _resolvedDependencies;
		}
	}
}
