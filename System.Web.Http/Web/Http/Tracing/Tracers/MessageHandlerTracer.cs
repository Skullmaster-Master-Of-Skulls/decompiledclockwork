using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000170 RID: 368
	internal class MessageHandlerTracer : DelegatingHandler, IDecorator<DelegatingHandler>
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x0001F546 File Offset: 0x0001D746
		public MessageHandlerTracer(DelegatingHandler innerHandler, ITraceWriter traceWriter)
		{
			this._innerHandler = innerHandler;
			this._traceWriter = traceWriter;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001F55C File Offset: 0x0001D75C
		public DelegatingHandler Inner
		{
			get
			{
				return this._innerHandler;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001F598 File Offset: 0x0001D798
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(request, TraceCategories.MessageHandlersCategory, TraceLevel.Info, this._innerHandler.GetType().Name, "SendAsync", null, () => this.<>n__FabricatedMethod5(request, cancellationToken), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x040002D1 RID: 721
		private const string SendAsyncMethodName = "SendAsync";

		// Token: 0x040002D2 RID: 722
		private readonly DelegatingHandler _innerHandler;

		// Token: 0x040002D3 RID: 723
		private readonly ITraceWriter _traceWriter;
	}
}
