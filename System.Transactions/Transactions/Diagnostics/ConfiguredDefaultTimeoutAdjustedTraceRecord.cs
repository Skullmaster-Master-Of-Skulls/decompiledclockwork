using System;
using System.Diagnostics;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000C8 RID: 200
	internal class ConfiguredDefaultTimeoutAdjustedTraceRecord : TraceRecord
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00042884 File Offset: 0x00041C84
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/ConfiguredDefaultTimeoutAdjustedTraceRecord";
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000428A4 File Offset: 0x00041CA4
		internal static void Trace(string traceSource)
		{
			lock (ConfiguredDefaultTimeoutAdjustedTraceRecord.record)
			{
				ConfiguredDefaultTimeoutAdjustedTraceRecord.record.traceSource = traceSource;
				DiagnosticTrace.TraceEvent(TraceEventType.Warning, "http://msdn.microsoft.com/2004/06/System/Transactions/ConfiguredDefaultTimeoutAdjusted", SR.GetString("TraceConfiguredDefaultTimeoutAdjusted"), ConfiguredDefaultTimeoutAdjustedTraceRecord.record);
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00042914 File Offset: 0x00041D14
		internal override void WriteTo(XmlWriter xml)
		{
			TraceHelper.WriteTraceSource(xml, this.traceSource);
		}

		// Token: 0x040002F7 RID: 759
		private static ConfiguredDefaultTimeoutAdjustedTraceRecord record = new ConfiguredDefaultTimeoutAdjustedTraceRecord();

		// Token: 0x040002F8 RID: 760
		private string traceSource;
	}
}
