using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000171 RID: 369
	internal class RequestMessageHandlerTracer : DelegatingHandler
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0001F621 File Offset: 0x0001D821
		public RequestMessageHandlerTracer(ITraceWriter traceWriter)
		{
			this._traceWriter = traceWriter;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001F74C File Offset: 0x0001D94C
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(request, TraceCategories.RequestCategory, TraceLevel.Info, string.Empty, string.Empty, delegate(TraceRecord tr)
			{
				tr.Message = ((request.RequestUri == null) ? SRResources.TraceNoneObjectMessage : request.RequestUri.ToString());
			}, () => this.<>n__FabricatedMethod6(request, cancellationToken), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				MediaTypeHeaderValue mediaTypeHeaderValue = (response == null) ? null : ((response.Content == null) ? null : response.Content.Headers.ContentType);
				long? num = (response == null) ? null : ((response.Content == null) ? null : response.Content.Headers.ContentLength);
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
				tr.Message = Error.Format(SRResources.TraceRequestCompleteMessage, new object[]
				{
					(mediaTypeHeaderValue == null) ? SRResources.TraceNoneObjectMessage : mediaTypeHeaderValue.ToString(),
					(num != null) ? num.Value.ToString(CultureInfo.CurrentCulture) : SRResources.TraceUnknownMessage
				});
			}, null);
		}

		// Token: 0x040002D5 RID: 725
		private readonly ITraceWriter _traceWriter;
	}
}
