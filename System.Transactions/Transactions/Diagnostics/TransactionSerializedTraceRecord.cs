using System;
using System.Diagnostics;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000BF RID: 191
	internal class TransactionSerializedTraceRecord : TraceRecord
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00041EE4 File Offset: 0x000412E4
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/TransactionSerializedTraceRecord";
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00041F04 File Offset: 0x00041304
		internal static void Trace(string traceSource, TransactionTraceIdentifier txTraceId)
		{
			lock (TransactionSerializedTraceRecord.record)
			{
				TransactionSerializedTraceRecord.record.traceSource = traceSource;
				TransactionSerializedTraceRecord.record.txTraceId = txTraceId;
				DiagnosticTrace.TraceEvent(TraceEventType.Information, "http://msdn.microsoft.com/2004/06/System/Transactions/TransactionSerialized", SR.GetString("TraceTransactionSerialized"), TransactionSerializedTraceRecord.record);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00041F74 File Offset: 0x00041374
		internal override void WriteTo(XmlWriter xml)
		{
			TraceHelper.WriteTraceSource(xml, this.traceSource);
			TraceHelper.WriteTxId(xml, this.txTraceId);
		}

		// Token: 0x040002DE RID: 734
		private static TransactionSerializedTraceRecord record = new TransactionSerializedTraceRecord();

		// Token: 0x040002DF RID: 735
		private TransactionTraceIdentifier txTraceId;

		// Token: 0x040002E0 RID: 736
		private string traceSource;
	}
}
