using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x02000060 RID: 96
	public class OkNegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x000096A4 File Offset: 0x000078A4
		public OkNegotiatedContentResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(content, new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000096B6 File Offset: 0x000078B6
		public OkNegotiatedContentResult(T content, ApiController controller) : this(content, new NegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000096C5 File Offset: 0x000078C5
		private OkNegotiatedContentResult(T content, NegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000096DB File Offset: 0x000078DB
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000096E3 File Offset: 0x000078E3
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000096F0 File Offset: 0x000078F0
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000096FD File Offset: 0x000078FD
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000970A File Offset: 0x0000790A
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(NegotiatedContentResult<T>.Execute(HttpStatusCode.OK, this._content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters));
		}

		// Token: 0x040000C3 RID: 195
		private readonly T _content;

		// Token: 0x040000C4 RID: 196
		private readonly NegotiatedContentResult<T>.IDependencyProvider _dependencies;
	}
}
