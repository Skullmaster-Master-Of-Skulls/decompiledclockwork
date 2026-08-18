using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Results
{
	// Token: 0x02000068 RID: 104
	public class StatusCodeResult : IHttpActionResult
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00009C5C File Offset: 0x00007E5C
		public StatusCodeResult(HttpStatusCode statusCode, HttpRequestMessage request) : this(statusCode, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009C6B File Offset: 0x00007E6B
		public StatusCodeResult(HttpStatusCode statusCode, ApiController controller) : this(statusCode, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009C7A File Offset: 0x00007E7A
		private StatusCodeResult(HttpStatusCode statusCode, StatusCodeResult.IDependencyProvider dependencies)
		{
			this._statusCode = statusCode;
			this._dependencies = dependencies;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00009C90 File Offset: 0x00007E90
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00009C98 File Offset: 0x00007E98
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009CA5 File Offset: 0x00007EA5
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009CB2 File Offset: 0x00007EB2
		private HttpResponseMessage Execute()
		{
			return StatusCodeResult.Execute(this._statusCode, this._dependencies.Request);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009CCC File Offset: 0x00007ECC
		internal static HttpResponseMessage Execute(HttpStatusCode statusCode, HttpRequestMessage request)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(statusCode);
			try
			{
				httpResponseMessage.RequestMessage = request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x040000D6 RID: 214
		private readonly HttpStatusCode _statusCode;

		// Token: 0x040000D7 RID: 215
		private readonly StatusCodeResult.IDependencyProvider _dependencies;

		// Token: 0x02000069 RID: 105
		internal interface IDependencyProvider
		{
			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060002F7 RID: 759
			HttpRequestMessage Request { get; }
		}

		// Token: 0x0200006A RID: 106
		internal sealed class DirectDependencyProvider : StatusCodeResult.IDependencyProvider
		{
			// Token: 0x060002F8 RID: 760 RVA: 0x00009D04 File Offset: 0x00007F04
			public DirectDependencyProvider(HttpRequestMessage request)
			{
				if (request == null)
				{
					throw new ArgumentNullException("request");
				}
				this._request = request;
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x00009D21 File Offset: 0x00007F21
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x040000D8 RID: 216
			private readonly HttpRequestMessage _request;
		}

		// Token: 0x0200006B RID: 107
		internal sealed class ApiControllerDependencyProvider : StatusCodeResult.IDependencyProvider
		{
			// Token: 0x060002FA RID: 762 RVA: 0x00009D29 File Offset: 0x00007F29
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060002FB RID: 763 RVA: 0x00009D46 File Offset: 0x00007F46
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._request;
				}
			}

			// Token: 0x060002FC RID: 764 RVA: 0x00009D54 File Offset: 0x00007F54
			private void EnsureResolved()
			{
				if (this._request == null)
				{
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					this._request = request;
				}
			}

			// Token: 0x040000D9 RID: 217
			private readonly ApiController _controller;

			// Token: 0x040000DA RID: 218
			private HttpRequestMessage _request;
		}
	}
}
