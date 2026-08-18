using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000025 RID: 37
	internal class TransactionStatePromotedP0Wave : TransactionStatePromotedBase
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0002E764 File Offset: 0x0002DB64
		internal override void EnterState(InternalTransaction tx)
		{
			base.CommonEnterState(tx);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0002E784 File Offset: 0x0002DB84
		internal override void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0002E7B4 File Offset: 0x0002DBB4
		internal override void Phase0VolatilePrepareDone(InternalTransaction tx)
		{
			try
			{
				TransactionState._TransactionStatePromotedCommitting.EnterState(tx);
			}
			catch (TransactionException ex)
			{
				if (tx.innerException == null)
				{
					tx.innerException = ex;
				}
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), ex);
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0002E814 File Offset: 0x0002DC14
		internal override bool ContinuePhase0Prepares()
		{
			return true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0002E824 File Offset: 0x0002DC24
		internal override void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
			if (tx.innerException == null)
			{
				tx.innerException = e;
			}
			TransactionState._TransactionStatePromotedP0Aborting.EnterState(tx);
		}
	}
}
