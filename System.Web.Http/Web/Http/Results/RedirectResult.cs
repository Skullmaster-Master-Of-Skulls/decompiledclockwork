using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x0200004F RID: 79
	public class RedirectResult : IHttpActionResult
	{
		// Token: 0x06000243 RID: 579 RVA: 0x000089BE File Offset: 0x00006BBE
		public RedirectResult(Uri location, HttpRequestMessage request) : this(location, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000089CD File Offset: 0x00006BCD
		public RedirectResult(Uri location, ApiController controller) : this(location, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000089DC File Offset: 0x00006BDC
		private RedirectResult(Uri location, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			this._location = location;
			this._dependencies = dependencies;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00008A06 File Offset: 0x00006C06
		public Uri Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00008A0E File Offset: 0x00006C0E
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008A1B File Offset: 0x00006C1B
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008A28 File Offset: 0x00006C28
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Found);
			try
			{
				httpResponseMessage.Headers.Location = this._location;
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x040000A0 RID: 160
		private readonly Uri _location;

		// Token: 0x040000A1 RID: 161
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
