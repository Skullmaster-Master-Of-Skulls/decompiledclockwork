using System;
using System.Diagnostics;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000C3 RID: 195
	internal class ExceptionConsumedTraceRecord : TraceRecord
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00042364 File Offset: 0x00041764
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/ExceptionConsumedTraceRecord";
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00042384 File Offset: 0x00041784
		internal static void Trace(string traceSource, Exception exception)
		{
			lock (ExceptionConsumedTraceRecord.record)
			{
				ExceptionConsumedTraceRecord.record.traceSource = traceSource;
				ExceptionConsumedTraceRecord.record.exception = exception;
				DiagnosticTrace.TraceEvent(TraceEventType.Verbose, "http://msdn.microsoft.com/2004/06/System/Transactions/ExceptionConsumed", SR.GetString("TraceExceptionConsumed"), ExceptionConsumedTraceRecord.record);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000423F4 File Offset: 0x000417F4
		internal override void WriteTo(XmlWriter xml)
		{
			TraceHelper.WriteTraceSource(xml, this.traceSource);
			xml.WriteElementString("ExceptionMessage", this.exception.Message);
			xml.WriteElementString("ExceptionStack", this.exception.StackTrace);
		}

		// Token: 0x040002E8 RID: 744
		private static ExceptionConsumedTraceRecord record = new ExceptionConsumedTraceRecord();

		// Token: 0x040002E9 RID: 745
		private Exception exception;

		// Token: 0x040002EA RID: 746
		private string traceSource;
	}
}
