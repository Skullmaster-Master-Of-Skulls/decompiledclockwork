using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x0200005F RID: 95
	public class CreatedNegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000954E File Offset: 0x0000774E
		public CreatedNegotiatedContentResult(Uri location, T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(location, content, new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009562 File Offset: 0x00007762
		public CreatedNegotiatedContentResult(Uri location, T content, ApiController controller) : this(location, content, new NegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009572 File Offset: 0x00007772
		private CreatedNegotiatedContentResult(Uri location, T content, NegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			this._location = location;
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000095A3 File Offset: 0x000077A3
		public Uri Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000095AB File Offset: 0x000077AB
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000095B3 File Offset: 0x000077B3
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000095C0 File Offset: 0x000077C0
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000095CD File Offset: 0x000077CD
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000095DA File Offset: 0x000077DA
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000095E8 File Offset: 0x000077E8
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
					httpResponseMessage.Headers.Location = this._location;
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

		// Token: 0x040000C0 RID: 192
		private readonly Uri _location;

		// Token: 0x040000C1 RID: 193
		private readonly T _content;

		// Token: 0x040000C2 RID: 194
		private readonly NegotiatedContentResult<T>.IDependencyProvider _dependencies;
	}
}
