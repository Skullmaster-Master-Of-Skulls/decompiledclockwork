using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000010 RID: 16
	public class TransactionInformation
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0002A684 File Offset: 0x00029A84
		internal TransactionInformation(InternalTransaction internalTransaction)
		{
			this.internalTransaction = internalTransaction;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0002A6A4 File Offset: 0x00029AA4
		public string LocalIdentifier
		{
			get
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "TransactionInformation.get_LocalIdentifier");
				}
				string transactionIdentifier;
				try
				{
					transactionIdentifier = this.internalTransaction.TransactionTraceId.TransactionIdentifier;
				}
				finally
				{
					if (DiagnosticTrace.Verbose)
					{
						MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "TransactionInformation.get_LocalIdentifier");
					}
				}
				return transactionIdentifier;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0002A724 File Offset: 0x00029B24
		public Guid DistributedIdentifier
		{
			get
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "TransactionInformation.get_DistributedIdentifier");
				}
				Guid result;
				try
				{
					lock (this.internalTransaction)
					{
						result = this.internalTransaction.State.get_Identifier(this.internalTransaction);
					}
				}
				finally
				{
					if (DiagnosticTrace.Verbose)
					{
						MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "TransactionInformation.get_DistributedIdentifier");
					}
				}
				return result;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0002A7D4 File Offset: 0x00029BD4
		public DateTime CreationTime
		{
			get
			{
				return new DateTime(this.internalTransaction.CreationTime);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0002A7F4 File Offset: 0x00029BF4
		public TransactionStatus Status
		{
			get
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "TransactionInformation.get_Status");
				}
				TransactionStatus result;
				try
				{
					result = this.internalTransaction.State.get_Status(this.internalTransaction);
				}
				finally
				{
					if (DiagnosticTrace.Verbose)
					{
						MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "TransactionInformation.get_Status");
					}
				}
				return result;
			}
		}

		// Token: 0x0400009F RID: 159
		private InternalTransaction internalTransaction;
	}
}
