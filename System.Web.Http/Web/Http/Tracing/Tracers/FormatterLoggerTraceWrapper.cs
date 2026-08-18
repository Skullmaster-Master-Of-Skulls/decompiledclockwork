using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x020000A7 RID: 167
	internal class FormatterLoggerTraceWrapper : IFormatterLogger
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000C50F File Offset: 0x0000A70F
		public FormatterLoggerTraceWrapper(IFormatterLogger formatterLogger, ITraceWriter traceWriter, HttpRequestMessage request, string operatorName, string operationName)
		{
			this._formatterLogger = formatterLogger;
			this._traceWriter = traceWriter;
			this._request = request;
			this._operatorName = operatorName;
			this._operationName = operationName;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000C57C File Offset: 0x0000A77C
		public void LogError(string errorPath, string errorMessage)
		{
			this._traceWriter.Trace(this._request, TraceCategories.FormattingCategory, TraceLevel.Error, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Trace;
				traceRecord.Operator = this._operatorName;
				traceRecord.Operation = this._operationName;
				traceRecord.Message = errorMessage;
			});
			this._formatterLogger.LogError(errorPath, errorMessage);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C614 File Offset: 0x0000A814
		public void LogError(string errorPath, Exception exception)
		{
			this._traceWriter.Trace(this._request, TraceCategories.FormattingCategory, TraceLevel.Error, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Trace;
				traceRecord.Operator = this._operatorName;
				traceRecord.Operation = this._operationName;
				traceRecord.Exception = exception;
			});
			this._formatterLogger.LogError(errorPath, exception);
		}

		// Token: 0x04000121 RID: 289
		private readonly IFormatterLogger _formatterLogger;

		// Token: 0x04000122 RID: 290
		private readonly ITraceWriter _traceWriter;

		// Token: 0x04000123 RID: 291
		private readonly HttpRequestMessage _request;

		// Token: 0x04000124 RID: 292
		private readonly string _operatorName;

		// Token: 0x04000125 RID: 293
		private readonly string _operationName;
	}
}
