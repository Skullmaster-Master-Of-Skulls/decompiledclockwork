using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x02000057 RID: 87
	public class UnauthorizedResult : IHttpActionResult
	{
		// Token: 0x0600027D RID: 637 RVA: 0x000090E8 File Offset: 0x000072E8
		public UnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, HttpRequestMessage request) : this(challenges, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000090F7 File Offset: 0x000072F7
		public UnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, ApiController controller) : this(challenges, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009106 File Offset: 0x00007306
		private UnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (challenges == null)
			{
				throw new ArgumentNullException("challenges");
			}
			this._challenges = challenges;
			this._dependencies = dependencies;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000912A File Offset: 0x0000732A
		public IEnumerable<AuthenticationHeaderValue> Challenges
		{
			get
			{
				return this._challenges;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00009132 File Offset: 0x00007332
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000913F File Offset: 0x0000733F
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000914C File Offset: 0x0000734C
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
			try
			{
				foreach (AuthenticationHeaderValue item in this._challenges)
				{
					httpResponseMessage.Headers.WwwAuthenticate.Add(item);
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

		// Token: 0x040000B3 RID: 179
		private readonly IEnumerable<AuthenticationHeaderValue> _challenges;

		// Token: 0x040000B4 RID: 180
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
