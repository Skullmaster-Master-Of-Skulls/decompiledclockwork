using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x02000054 RID: 84
	public class BadRequestErrorMessageResult : IHttpActionResult
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00008DCA File Offset: 0x00006FCA
		public BadRequestErrorMessageResult(string message, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(message, new NegotiatedContentResult<HttpError>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008DDC File Offset: 0x00006FDC
		public BadRequestErrorMessageResult(string message, ApiController controller) : this(message, new NegotiatedContentResult<HttpError>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008DEB File Offset: 0x00006FEB
		private BadRequestErrorMessageResult(string message, NegotiatedContentResult<HttpError>.IDependencyProvider dependencies)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this._message = message;
			this._dependencies = dependencies;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008E0F File Offset: 0x0000700F
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008E17 File Offset: 0x00007017
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00008E24 File Offset: 0x00007024
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008E31 File Offset: 0x00007031
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008E3E File Offset: 0x0000703E
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008E4C File Offset: 0x0000704C
		private HttpResponseMessage Execute()
		{
			HttpError content = new HttpError(this._message);
			return NegotiatedContentResult<HttpError>.Execute(HttpStatusCode.BadRequest, content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x040000AC RID: 172
		private readonly string _message;

		// Token: 0x040000AD RID: 173
		private readonly NegotiatedContentResult<HttpError>.IDependencyProvider _dependencies;
	}
}
