using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Results
{
	// Token: 0x02000061 RID: 97
	public class InvalidModelStateResult : IHttpActionResult
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00009742 File Offset: 0x00007942
		public InvalidModelStateResult(ModelStateDictionary modelState, bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) : this(modelState, new ExceptionResult.DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters))
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009756 File Offset: 0x00007956
		public InvalidModelStateResult(ModelStateDictionary modelState, ApiController controller) : this(modelState, new ExceptionResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009765 File Offset: 0x00007965
		private InvalidModelStateResult(ModelStateDictionary modelState, ExceptionResult.IDependencyProvider dependencies)
		{
			if (modelState == null)
			{
				throw new ArgumentNullException("modelState");
			}
			this._modelState = modelState;
			this._dependencies = dependencies;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00009789 File Offset: 0x00007989
		public ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00009791 File Offset: 0x00007991
		public bool IncludeErrorDetail
		{
			get
			{
				return this._dependencies.IncludeErrorDetail;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000979E File Offset: 0x0000799E
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000097AB File Offset: 0x000079AB
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000097B8 File Offset: 0x000079B8
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000097C5 File Offset: 0x000079C5
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000097D4 File Offset: 0x000079D4
		private HttpResponseMessage Execute()
		{
			HttpError content = new HttpError(this._modelState, this._dependencies.IncludeErrorDetail);
			return NegotiatedContentResult<HttpError>.Execute(HttpStatusCode.BadRequest, content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x040000C5 RID: 197
		private readonly ModelStateDictionary _modelState;

		// Token: 0x040000C6 RID: 198
		private readonly ExceptionResult.IDependencyProvider _dependencies;
	}
}
