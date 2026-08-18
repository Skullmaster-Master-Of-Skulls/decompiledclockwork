using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200016A RID: 362
	internal class ContentNegotiatorTracer : IContentNegotiator, IDecorator<IContentNegotiator>
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0001E562 File Offset: 0x0001C762
		public ContentNegotiatorTracer(IContentNegotiator innerNegotiator, ITraceWriter traceWriter)
		{
			this._innerNegotiator = innerNegotiator;
			this._traceWriter = traceWriter;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001E578 File Offset: 0x0001C778
		public IContentNegotiator Inner
		{
			get
			{
				return this._innerNegotiator;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001E674 File Offset: 0x0001C874
		public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			ContentNegotiationResult result = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.FormattingCategory, TraceLevel.Info, this._innerNegotiator.GetType().Name, "Negotiate", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceNegotiateFormatter, new object[]
				{
					type.Name,
					FormattingUtilities.FormattersToString(formatters)
				});
			}, delegate
			{
				result = this._innerNegotiator.Negotiate(type, request, formatters);
			}, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceSelectedFormatter, new object[]
				{
					(result == null) ? SRResources.TraceNoneObjectMessage : MediaTypeFormatterTracer.ActualMediaTypeFormatter(result.Formatter).GetType().Name,
					(result == null || result.MediaType == null) ? SRResources.TraceNoneObjectMessage : result.MediaType.ToString()
				});
			}, null);
			if (result != null)
			{
				result.Formatter = MediaTypeFormatterTracer.CreateTracer(result.Formatter, this._traceWriter, request);
			}
			return result;
		}

		// Token: 0x040002BC RID: 700
		private const string NegotiateMethodName = "Negotiate";

		// Token: 0x040002BD RID: 701
		private readonly IContentNegotiator _innerNegotiator;

		// Token: 0x040002BE RID: 702
		private readonly ITraceWriter _traceWriter;
	}
}
