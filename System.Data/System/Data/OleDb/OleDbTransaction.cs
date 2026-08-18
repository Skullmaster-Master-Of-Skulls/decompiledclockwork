using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Data.OleDb
{
	// Token: 0x0200024E RID: 590
	public sealed class OleDbTransaction : DbTransaction
	{
		// Token: 0x06002063 RID: 8291 RVA: 0x002800F8 File Offset: 0x0027F4F8
		internal OleDbTransaction(OleDbConnection connection, OleDbTransaction transaction, IsolationLevel isolevel)
		{
			this._parentConnection = connection;
			this._parentTransaction = transaction;
			IsolationLevel isolationLevel = isolevel;
			if (isolationLevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolationLevel == IsolationLevel.Unspecified)
				{
					isolevel = IsolationLevel.ReadCommitted;
					goto IL_7D;
				}
				if (isolationLevel == IsolationLevel.Chaos || isolationLevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_7D;
				}
			}
			else if (isolationLevel <= IsolationLevel.RepeatableRead)
			{
				if (isolationLevel == IsolationLevel.ReadCommitted || isolationLevel == IsolationLevel.RepeatableRead)
				{
					goto IL_7D;
				}
			}
			else if (isolationLevel == IsolationLevel.Serializable || isolationLevel == IsolationLevel.Snapshot)
			{
				goto IL_7D;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_7D:
			this._isolationLevel = isolevel;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x00280198 File Offset: 0x0027F598
		public new OleDbConnection Connection
		{
			get
			{
				return this._parentConnection;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x002801B8 File Offset: 0x0027F5B8
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x002801D8 File Offset: 0x0027F5D8
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

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x00280208 File Offset: 0x0027F608
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00280228 File Offset: 0x0027F628
		internal OleDbTransaction Parent
		{
			get
			{
				return this._parentTransaction;
			}
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00280248 File Offset: 0x0027F648
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

		// Token: 0x0600206A RID: 8298 RVA: 0x00280328 File Offset: 0x0027F728
		public OleDbTransaction Begin()
		{
			return this.Begin(IsolationLevel.ReadCommitted);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00280348 File Offset: 0x0027F748
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

		// Token: 0x0600206C RID: 8300 RVA: 0x00280388 File Offset: 0x0027F788
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

		// Token: 0x0600206D RID: 8301 RVA: 0x002803F8 File Offset: 0x0027F7F8
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

		// Token: 0x0600206E RID: 8302 RVA: 0x00280488 File Offset: 0x0027F888
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeManaged();
				this.RollbackInternal(false);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x002804B8 File Offset: 0x0027F8B8
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

		// Token: 0x06002070 RID: 8304 RVA: 0x002804F8 File Offset: 0x0027F8F8
		private void ProcessResults(OleDbHResult hr)
		{
			Exception ex = OleDbConnection.ProcessResults(hr, this._parentConnection, this);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00280518 File Offset: 0x0027F918
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

		// Token: 0x06002072 RID: 8306 RVA: 0x00280588 File Offset: 0x0027F988
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

		// Token: 0x06002073 RID: 8307 RVA: 0x00280628 File Offset: 0x0027FA28
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

		// Token: 0x06002074 RID: 8308 RVA: 0x00280668 File Offset: 0x0027FA68
		internal static OleDbTransaction TransactionUpdate(OleDbTransaction transaction)
		{
			if (transaction != null && transaction._transaction == null)
			{
				return null;
			}
			return transaction;
		}

		// Token: 0x040014F9 RID: 5369
		private readonly OleDbTransaction _parentTransaction;

		// Token: 0x040014FA RID: 5370
		private readonly IsolationLevel _isolationLevel;

		// Token: 0x040014FB RID: 5371
		private WeakReference _nestedTransaction;

		// Token: 0x040014FC RID: 5372
		private OleDbTransaction.WrappedTransaction _transaction;

		// Token: 0x040014FD RID: 5373
		internal OleDbConnection _parentConnection;

		// Token: 0x040014FE RID: 5374
		private static int _objectTypeCount;

		// Token: 0x040014FF RID: 5375
		internal readonly int _objectID = Interlocked.Increment(ref OleDbTransaction._objectTypeCount);

		// Token: 0x02000250 RID: 592
		private sealed class WrappedTransaction : WrappedIUnknown
		{
			// Token: 0x0600207A RID: 8314 RVA: 0x002807B8 File Offset: 0x0027FBB8
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

			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x0600207B RID: 8315 RVA: 0x00280828 File Offset: 0x0027FC28
			internal bool MustComplete
			{
				get
				{
					return this._mustComplete;
				}
			}

			// Token: 0x0600207C RID: 8316 RVA: 0x00280848 File Offset: 0x0027FC48
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

			// Token: 0x0600207D RID: 8317 RVA: 0x002808E8 File Offset: 0x0027FCE8
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

			// Token: 0x0600207E RID: 8318 RVA: 0x00280988 File Offset: 0x0027FD88
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

			// Token: 0x04001500 RID: 5376
			private bool _mustComplete;
		}
	}
}
