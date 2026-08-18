using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x02000062 RID: 98
	public class FormattedContentResult<T> : IHttpActionResult
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00009824 File Offset: 0x00007A24
		public FormattedContentResult(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, HttpRequestMessage request) : this(statusCode, content, formatter, mediaType, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009838 File Offset: 0x00007A38
		public FormattedContentResult(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, ApiController controller) : this(statusCode, content, formatter, mediaType, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000984C File Offset: 0x00007A4C
		private FormattedContentResult(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException("formatter");
			}
			this._statusCode = statusCode;
			this._content = content;
			this._formatter = formatter;
			this._mediaType = mediaType;
			this._dependencies = dependencies;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00009887 File Offset: 0x00007A87
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000988F File Offset: 0x00007A8F
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00009897 File Offset: 0x00007A97
		public MediaTypeFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000989F File Offset: 0x00007A9F
		public MediaTypeHeaderValue MediaType
		{
			get
			{
				return this._mediaType;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000098A7 File Offset: 0x00007AA7
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000098B4 File Offset: 0x00007AB4
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000098C1 File Offset: 0x00007AC1
		private HttpResponseMessage Execute()
		{
			return FormattedContentResult<T>.Execute(this._statusCode, this._content, this._formatter, this._mediaType, this._dependencies.Request);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000098EC File Offset: 0x00007AEC
		internal static HttpResponseMessage Execute(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, HttpRequestMessage request)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(statusCode);
			try
			{
				httpResponseMessage.Content = new ObjectContent<T>(content, formatter, mediaType);
				httpResponseMessage.RequestMessage = request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x040000C7 RID: 199
		private readonly HttpStatusCode _statusCode;

		// Token: 0x040000C8 RID: 200
		private readonly T _content;

		// Token: 0x040000C9 RID: 201
		private readonly MediaTypeFormatter _formatter;

		// Token: 0x040000CA RID: 202
		private readonly MediaTypeHeaderValue _mediaType;

		// Token: 0x040000CB RID: 203
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
