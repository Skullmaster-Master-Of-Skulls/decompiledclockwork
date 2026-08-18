using System;
using System.ServiceModel.Channels;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200053B RID: 1339
	internal sealed class TransactedBatchContext : IEnlistmentNotification
	{
		// Token: 0x0600329F RID: 12959 RVA: 0x000C35BC File Offset: 0x000C17BC
		internal TransactedBatchContext(SharedTransactedBatchContext shared)
		{
			this.shared = shared;
			this.transaction = TransactionBehavior.CreateTransaction(shared.IsolationLevel, shared.TransactionTimeout);
			this.transaction.EnlistVolatile(this, EnlistmentOptions.None);
			if (shared.TransactionTimeout <= TimeSpan.Zero)
			{
				this.commitNotLaterThan = DateTime.MaxValue;
			}
			else
			{
				this.commitNotLaterThan = DateTime.UtcNow + TimeSpan.FromMilliseconds(shared.TransactionTimeout.TotalMilliseconds * 4.0 / 5.0);
			}
			this.commits = 0;
			this.batchFinished = false;
			this.inDispatch = false;
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000C3666 File Offset: 0x000C1866
		internal bool AboutToExpire
		{
			get
			{
				return DateTime.UtcNow > this.commitNotLaterThan;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000C3678 File Offset: 0x000C1878
		internal bool IsActive
		{
			get
			{
				if (this.batchFinished)
				{
					return false;
				}
				bool result;
				try
				{
					result = (this.transaction.TransactionInformation.Status == TransactionStatus.Active);
				}
				catch (ObjectDisposedException ex)
				{
					MsmqDiagnostics.ExpectedException(ex);
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000C36C4 File Offset: 0x000C18C4
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x000C36CC File Offset: 0x000C18CC
		internal bool InDispatch
		{
			get
			{
				return this.inDispatch;
			}
			set
			{
				bool flag = this.inDispatch;
				this.inDispatch = value;
				if (this.inDispatch)
				{
					this.shared.DispatchStarted();
					return;
				}
				this.shared.DispatchEnded();
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000C36FD File Offset: 0x000C18FD
		internal SharedTransactedBatchContext Shared
		{
			get
			{
				return this.shared;
			}
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000C3708 File Offset: 0x000C1908
		internal void ForceRollback()
		{
			try
			{
				this.transaction.Rollback();
			}
			catch (ObjectDisposedException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
			catch (TransactionException ex2)
			{
				MsmqDiagnostics.ExpectedException(ex2);
			}
			this.batchFinished = true;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000C3758 File Offset: 0x000C1958
		internal void ForceCommit()
		{
			try
			{
				this.transaction.Commit();
			}
			catch (ObjectDisposedException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
			catch (TransactionException ex2)
			{
				MsmqDiagnostics.ExpectedException(ex2);
			}
			this.batchFinished = true;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000C37A8 File Offset: 0x000C19A8
		internal void Complete()
		{
			this.commits++;
			if (this.commits >= this.shared.CurrentBatchSize || DateTime.UtcNow >= this.commitNotLaterThan)
			{
				this.ForceCommit();
			}
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000C37E3 File Offset: 0x000C19E3
		void IEnlistmentNotification.Prepare(PreparingEnlistment preparingEnlistment)
		{
			preparingEnlistment.Prepared();
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000C37EB File Offset: 0x000C19EB
		void IEnlistmentNotification.Commit(Enlistment enlistment)
		{
			this.shared.ReportCommit();
			this.shared.BatchDone();
			enlistment.Done();
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000C3809 File Offset: 0x000C1A09
		void IEnlistmentNotification.Rollback(Enlistment enlistment)
		{
			this.shared.ReportAbort();
			this.shared.BatchDone();
			enlistment.Done();
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000C3827 File Offset: 0x000C1A27
		void IEnlistmentNotification.InDoubt(Enlistment enlistment)
		{
			this.shared.ReportAbort();
			this.shared.BatchDone();
			enlistment.Done();
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000C3845 File Offset: 0x000C1A45
		internal Transaction Transaction
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x04002725 RID: 10021
		private SharedTransactedBatchContext shared;

		// Token: 0x04002726 RID: 10022
		private CommittableTransaction transaction;

		// Token: 0x04002727 RID: 10023
		private DateTime commitNotLaterThan;

		// Token: 0x04002728 RID: 10024
		private int commits;

		// Token: 0x04002729 RID: 10025
		private bool batchFinished;

		// Token: 0x0400272A RID: 10026
		private bool inDispatch;
	}
}
