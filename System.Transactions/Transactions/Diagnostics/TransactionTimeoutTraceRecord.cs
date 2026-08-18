using System;
using System.Diagnostics;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000B8 RID: 184
	internal class TransactionTimeoutTraceRecord : TraceRecord
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00041764 File Offset: 0x00040B64
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/TransactionTimeoutTraceRecord";
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00041784 File Offset: 0x00040B84
		internal static void Trace(string traceSource, TransactionTraceIdentifier txTraceId)
		{
			lock (TransactionTimeoutTraceRecord.record)
			{
				TransactionTimeoutTraceRecord.record.traceSource = traceSource;
				TransactionTimeoutTraceRecord.record.txTraceId = txTraceId;
				DiagnosticTrace.TraceEvent(TraceEventType.Warning, "http://msdn.microsoft.com/2004/06/System/Transactions/TransactionTimeout", SR.GetString("TraceTransactionTimeout"), TransactionTimeoutTraceRecord.record);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000417F4 File Offset: 0x00040BF4
		internal override void WriteTo(XmlWriter xml)
		{
			TraceHelper.WriteTraceSource(xml, this.traceSource);
			TraceHelper.WriteTxId(xml, this.txTraceId);
		}

		// Token: 0x040002C7 RID: 711
		private static TransactionTimeoutTraceRecord record = new TransactionTimeoutTraceRecord();

		// Token: 0x040002C8 RID: 712
		private TransactionTraceIdentifier txTraceId;

		// Token: 0x040002C9 RID: 713
		private string traceSource;
	}
}
