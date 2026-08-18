using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Data.OleDb
{
	// Token: 0x02000273 RID: 627
	public sealed class OleDbTransaction : DbTransaction
	{
		// Token: 0x06002674 RID: 9844 RVA: 0x00104994 File Offset: 0x00103D94
		internal OleDbTransaction(OleDbConnection connection, OleDbTransaction transaction, IsolationLevel isolevel)
		{
			this._parentConnection = connection;
			this._parentTransaction = transaction;
			if (isolevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolevel == IsolationLevel.Unspecified)
				{
					isolevel = IsolationLevel.ReadCommitted;
					goto IL_7B;
				}
				if (isolevel == IsolationLevel.Chaos || isolevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_7B;
				}
			}
			else if (isolevel <= IsolationLevel.RepeatableRead)
			{
				if (isolevel == IsolationLevel.ReadCommitted || isolevel == IsolationLevel.RepeatableRead)
				{
					goto IL_7B;
				}
			}
			else if (isolevel == IsolationLevel.Serializable || isolevel == IsolationLevel.Snapshot)
			{
				goto IL_7B;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_7B:
			this._isolationLevel = isolevel;
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x00104A24 File Offset: 0x00103E24
		public new OleDbConnection Connection
		{
			get
			{
				return this._parentConnection;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x00104A38 File Offset: 0x00103E38
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x00104A4C File Offset: 0x00103E4C
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (this._transaction == null)
				{
					throw ADP.TransactionZombied(this);
				}
				return this._isolationLevel;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x00104A70 File Offset: 0x00103E70
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x00104A84 File Offset: 0x00103E84
		internal OleDbTransaction Parent
		{
			get
			{
				return this._parentTransaction;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00104A98 File Offset: 0x00103E98
		public OleDbTransaction Begin(IsolationLevel isolevel)
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbTransaction.Begin|API> %d#, isolevel=%d{IsolationLevel}", this.ObjectID, (int)isolevel);
			OleDbTransaction result;
			try
			{
				if (this._transaction == null)
				{
					throw ADP.TransactionZombied(this);
				}
				if (this._nestedTransaction != null && this._nestedTransaction.IsAlive)
				{
					throw ADP.ParallelTransactionsNotSupported(this.Connection);
				}
				OleDbTransaction oleDbTransaction = new OleDbTransaction(this._parentConnection, this, isolevel);
				this._nestedTransaction = new WeakReference(oleDbTransaction, false);
				UnsafeNativeMethods.ITransactionLocal transactionLocal = null;
				try
				{
					transactionLocal = (UnsafeNativeMethods.ITransactionLocal)this._transaction.ComWrapper();
					oleDbTransaction.BeginInternal(transactionLocal);
				}
				finally
				{
					if (transactionLocal != null)
					{
						Marshal.ReleaseComObject(transactionLocal);
					}
				}
				result = oleDbTransaction;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00104B78 File Offset: 0x00103F78
		public OleDbTransaction Begin()
		{
			return this.Begin(IsolationLevel.ReadCommitted);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00104B90 File Offset: 0x00103F90
		internal void BeginInternal(UnsafeNativeMethods.ITransactionLocal transaction)
		{
			OleDbHResult oleDbHResult;
			this._transaction = new OleDbTransaction.WrappedTransaction(transaction, (int)this._isolationLevel, ref oleDbHResult);
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				this._transaction.Dispose();
				this._transaction = null;
				this.ProcessResults(oleDbHResult);
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00104BD0 File Offset: 0x00103FD0
		public override void Commit()
		{
			OleDbConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbTransaction.Commit|API> %d#", this.ObjectID);
			try
			{
				if (this._transaction == null)
				{
					throw ADP.TransactionZombied(this);
				}
				this.CommitInternal();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x00104C34 File Offset: 0x00104034
		private void CommitInternal()
		{
			if (this._transaction == null)
			{
				return;
			}
			if (this._nestedTransaction != null)
			{
				OleDbTransaction oleDbTransaction = (OleDbTransaction)this._nestedTransaction.Target;
				if (oleDbTransaction != null && this._nestedTransaction.IsAlive)
				{
					oleDbTransaction.CommitInternal();
				}
				this._nestedTransaction = null;
			}
			OleDbHResult oleDbHResult = this._transaction.Commit();
			if (!this._transaction.MustComplete)
			{
				this._transaction.Dispose();
				this._transaction = null;
				this.DisposeManaged();
			}
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				this.ProcessResults(oleDbHResult);
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00104CBC File Offset: 0x001040BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeManaged();
				this.RollbackInternal(false);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00104CE4 File Offset: 0x001040E4
		private void DisposeManaged()
		{
			if (this._parentTransaction != null)
			{
				this._parentTransaction._nestedTransaction = null;
			}
			else if (this._parentConnection != null)
			{
				this._parentConnection.LocalTransaction = null;
			}
			this._parentConnection = null;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00104D24 File Offset: 0x00104124
		private void ProcessResults(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, this._parentConnection, this);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00104D44 File Offset: 0x00104144
		public override void Rollback()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbTransaction.Rollback|API> %d#", this.ObjectID);
			try
			{
				if (this._transaction == null)
				{
					throw ADP.TransactionZombied(this);
				}
				this.DisposeManaged();
				this.RollbackInternal(true);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00104DA8 File Offset: 0x001041A8
		internal OleDbHResult RollbackInternal(bool exceptionHandling)
		{
			OleDbHResult oleDbHResult = OleDbHResult.S_OK;
			if (this._transaction != null)
			{
				if (this._nestedTransaction != null)
				{
					OleDbTransaction oleDbTransaction = (OleDbTransaction)this._nestedTransaction.Target;
					if (oleDbTransaction != null && this._nestedTransaction.IsAlive)
					{
						oleDbHResult = oleDbTransaction.RollbackInternal(exceptionHandling);
						if (exceptionHandling && oleDbHResult < OleDbHResult.S_OK)
						{
							SafeNativeMethods.Wrapper.ClearErrorInfo();
							return oleDbHResult;
						}
					}
					this._nestedTransaction = null;
				}
				oleDbHResult = this._transaction.Abort();
				this._transaction.Dispose();
				this._transaction = null;
				if (oleDbHResult < OleDbHResult.S_OK)
				{
					if (exceptionHandling)
					{
						this.ProcessResults(oleDbHResult);
					}
					else
					{
						SafeNativeMethods.Wrapper.ClearErrorInfo();
					}
				}
			}
			return oleDbHResult;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00104E3C File Offset: 0x0010423C
		internal static OleDbTransaction TransactionLast(OleDbTransaction head)
		{
			if (head._nestedTransaction != null)
			{
				OleDbTransaction oleDbTransaction = (OleDbTransaction)head._nestedTransaction.Target;
				if (oleDbTransaction != null && head._nestedTransaction.IsAlive)
				{
					return OleDbTransaction.TransactionLast(oleDbTransaction);
				}
			}
			return head;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x00104E7C File Offset: 0x0010427C
		internal static OleDbTransaction TransactionUpdate(OleDbTransaction transaction)
		{
			if (transaction != null && transaction._transaction == null)
			{
				return null;
			}
			return transaction;
		}

		// Token: 0x0400180B RID: 6155
		private readonly OleDbTransaction _parentTransaction;

		// Token: 0x0400180C RID: 6156
		private readonly IsolationLevel _isolationLevel;

		// Token: 0x0400180D RID: 6157
		private WeakReference _nestedTransaction;

		// Token: 0x0400180E RID: 6158
		private OleDbTransaction.WrappedTransaction _transaction;

		// Token: 0x0400180F RID: 6159
		internal OleDbConnection _parentConnection;

		// Token: 0x04001810 RID: 6160
		private static int _objectTypeCount;

		// Token: 0x04001811 RID: 6161
		internal readonly int _objectID = Interlocked.Increment(ref OleDbTransaction._objectTypeCount);

		// Token: 0x02000408 RID: 1032
		private sealed class WrappedTransaction : WrappedIUnknown
		{
			// Token: 0x060035E1 RID: 13793 RVA: 0x00147890 File Offset: 0x00146C90
			internal WrappedTransaction(UnsafeNativeMethods.ITransactionLocal transaction, int isolevel, out OleDbHResult hr) : base(transaction)
			{
				int num = 0;
				Bid.Trace("<oledb.ITransactionLocal.StartTransaction|API|OLEDB>\n");
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					hr = transaction.StartTransaction(isolevel, 0, IntPtr.Zero, out num);
					if (OleDbHResult.S_OK <= hr)
					{
						this._mustComplete = true;
					}
				}
				Bid.Trace("<oledb.ITransactionLocal.StartTransaction|API|OLEDB|RET> %08X{HRESULT}\n", hr);
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x060035E2 RID: 13794 RVA: 0x00147900 File Offset: 0x00146D00
			internal bool MustComplete
			{
				get
				{
					return this._mustComplete;
				}
			}

			// Token: 0x060035E3 RID: 13795 RVA: 0x00147914 File Offset: 0x00146D14
			internal OleDbHResult Abort()
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				OleDbHResult oleDbHResult;
				try
				{
					base.DangerousAddRef(ref flag);
					Bid.Trace("<oledb.ITransactionLocal.Abort|API|OLEDB> handle=%p\n", this.handle);
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						oleDbHResult = NativeOledbWrapper.ITransactionAbort(base.DangerousGetHandle());
						this._mustComplete = false;
					}
					Bid.Trace("<oledb.ITransactionLocal.Abort|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
				}
				finally
				{
					if (flag)
					{
						base.DangerousRelease();
					}
				}
				return oleDbHResult;
			}

			// Token: 0x060035E4 RID: 13796 RVA: 0x001479A8 File Offset: 0x00146DA8
			internal OleDbHResult Commit()
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				OleDbHResult oleDbHResult;
				try
				{
					base.DangerousAddRef(ref flag);
					Bid.Trace("<oledb.ITransactionLocal.Commit|API|OLEDB> handle=%p\n", this.handle);
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						oleDbHResult = NativeOledbWrapper.ITransactionCommit(base.DangerousGetHandle());
						if (OleDbHResult.S_OK <= oleDbHResult || OleDbHResult.XACT_E_NOTRANSACTION == oleDbHResult)
						{
							this._mustComplete = false;
						}
					}
					Bid.Trace("<oledb.ITransactionLocal.Commit|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
				}
				finally
				{
					if (flag)
					{
						base.DangerousRelease();
					}
				}
				return oleDbHResult;
			}

			// Token: 0x060035E5 RID: 13797 RVA: 0x00147A48 File Offset: 0x00146E48
			protected override bool ReleaseHandle()
			{
				if (this._mustComplete && IntPtr.Zero != this.handle)
				{
					Bid.Trace("<oledb.ITransactionLocal.Abort|API|OLEDB|INFO> handle=%p\n", this.handle);
					OleDbHResult a = NativeOledbWrapper.ITransactionAbort(this.handle);
					this._mustComplete = false;
					Bid.Trace("<oledb.ITransactionLocal.Abort|API|OLEDB|INFO|RET> %08X{HRESULT}\n", a);
				}
				return base.ReleaseHandle();
			}

			// Token: 0x040021C7 RID: 8647
			private bool _mustComplete;
		}
	}
}
