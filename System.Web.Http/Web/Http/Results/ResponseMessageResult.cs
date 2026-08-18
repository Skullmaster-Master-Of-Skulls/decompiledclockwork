using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x02000071 RID: 113
	public class ResponseMessageResult : IHttpActionResult
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000A01E File Offset: 0x0000821E
		public ResponseMessageResult(HttpResponseMessage response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this._response = response;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000A03B File Offset: 0x0000823B
		public HttpResponseMessage Response
		{
			get
			{
				return this._response;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A043 File Offset: 0x00008243
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this._response);
		}

		// Token: 0x040000ED RID: 237
		private readonly HttpResponseMessage _response;
	}
}
