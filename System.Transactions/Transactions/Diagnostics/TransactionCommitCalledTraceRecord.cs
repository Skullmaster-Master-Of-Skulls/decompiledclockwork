using System;
using System.Diagnostics;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000AD RID: 173
	internal class TransactionCommitCalledTraceRecord : TraceRecord
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00040C24 File Offset: 0x00040024
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/TransactionCommitCalledTraceRecord";
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00040C44 File Offset: 0x00040044
		internal static void Trace(string traceSource, TransactionTraceIdentifier txTraceId)
		{
			lock (TransactionCommitCalledTraceRecord.record)
			{
				TransactionCommitCalledTraceRecord.record.traceSource = traceSource;
				TransactionCommitCalledTraceRecord.record.txTraceId = txTraceId;
				DiagnosticTrace.TraceEvent(TraceEventType.Verbose, "http://msdn.microsoft.com/2004/06/System/Transactions/TransactionCommitCalled", SR.GetString("TraceTransactionCommitCalled"), TransactionCommitCalledTraceRecord.record);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00040CB4 File Offset: 0x000400B4
		internal override void WriteTo(XmlWriter xml)
		{
			TraceHelper.WriteTraceSource(xml, this.traceSource);
			TraceHelper.WriteTxId(xml, this.txTraceId);
		}

		// Token: 0x040002A4 RID: 676
		private static TransactionCommitCalledTraceRecord record = new TransactionCommitCalledTraceRecord();

		// Token: 0x040002A5 RID: 677
		private TransactionTraceIdentifier txTraceId;

		// Token: 0x040002A6 RID: 678
		private string traceSource;
	}
}
