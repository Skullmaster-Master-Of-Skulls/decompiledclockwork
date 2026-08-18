using System;
using System.Diagnostics;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000BB RID: 187
	internal class CloneCreatedTraceRecord : TraceRecord
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00041A84 File Offset: 0x00040E84
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/CloneCreatedTraceRecord";
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00041AA4 File Offset: 0x00040EA4
		internal static void Trace(string traceSource, TransactionTraceIdentifier txTraceId)
		{
			lock (CloneCreatedTraceRecord.record)
			{
				CloneCreatedTraceRecord.record.traceSource = traceSource;
				CloneCreatedTraceRecord.record.txTraceId = txTraceId;
				DiagnosticTrace.TraceEvent(TraceEventType.Verbose, "http://msdn.microsoft.com/2004/06/System/Transactions/CloneCreated", SR.GetString("TraceCloneCreated"), CloneCreatedTraceRecord.record);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00041B14 File Offset: 0x00040F14
		internal override void WriteTo(XmlWriter xml)
		{
			TraceHelper.WriteTraceSource(xml, this.traceSource);
			TraceHelper.WriteTxId(xml, this.txTraceId);
		}

		// Token: 0x040002D1 RID: 721
		private static CloneCreatedTraceRecord record = new CloneCreatedTraceRecord();

		// Token: 0x040002D2 RID: 722
		private TransactionTraceIdentifier txTraceId;

		// Token: 0x040002D3 RID: 723
		private string traceSource;
	}
}
