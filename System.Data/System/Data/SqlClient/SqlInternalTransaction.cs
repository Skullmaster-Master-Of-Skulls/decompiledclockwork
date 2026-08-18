using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020002FF RID: 767
	internal sealed class SqlInternalTransaction
	{
		// Token: 0x060027DF RID: 10207 RVA: 0x002AD5E8 File Offset: 0x002AC9E8
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction) : this(innerConnection, type, outerTransaction, 0L)
		{
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x002AD608 File Offset: 0x002ACA08
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction, long transactionId)
		{
			Bid.PoolerTrace("<sc.SqlInternalTransaction.ctor|RES|CPOOL> %d#, Created for connection %d#, outer transaction %d#, Type %d\n", this.ObjectID, innerConnection.ObjectID, (outerTransaction != null) ? outerTransaction.ObjectID : -1, (int)type);
			this._innerConnection = innerConnection;
			this._transactionType = type;
			if (outerTransaction != null)
			{
				this._parent = new WeakReference(outerTransaction);
			}
			this._transactionId = transactionId;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x002AD678 File Offset: 0x002ACA78
		internal bool HasParentTransaction
		{
			get
			{
				return TransactionType.LocalFromAPI == this._transactionType || (TransactionType.LocalFromTSQL == this._transactionType && this._parent != null);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x002AD6B8 File Offset: 0x002ACAB8
		internal bool IsAborted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x002AD6D8 File Offset: 0x002ACAD8
		internal bool IsActive
		{
			get
			{
				return TransactionState.Active == this._transactionState;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x002AD6F8 File Offset: 0x002ACAF8
		internal bool IsCommitted
		{
			get
			{
				return TransactionState.Committed == this._transactionState;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x002AD718 File Offset: 0x002ACB18
		internal bool IsCompleted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState || TransactionState.Committed == this._transactionState || TransactionState.Unknown == this._transactionState;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x002AD748 File Offset: 0x002ACB48
		internal bool IsContext
		{
			get
			{
				return TransactionType.Context == this._transactionType;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x002AD768 File Offset: 0x002ACB68
		internal bool IsDelegated
		{
			get
			{
				return TransactionType.Delegated == this._transactionType;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x002AD788 File Offset: 0x002ACB88
		internal bool IsDistributed
		{
			get
			{
				return TransactionType.Distributed == this._transactionType;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x002AD7A8 File Offset: 0x002ACBA8
		internal bool IsLocal
		{
			get
			{
				return TransactionType.LocalFromTSQL == this._transactionType || TransactionType.LocalFromAPI == this._transactionType || TransactionType.Context == this._transactionType;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x002AD7D8 File Offset: 0x002ACBD8
		internal bool IsOrphaned
		{
			get
			{
				return this._parent != null && this._parent.Target == null;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x002AD808 File Offset: 0x002ACC08
		internal bool IsZombied
		{
			get
			{
				return null == this._innerConnection;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x002AD828 File Offset: 0x002ACC28
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x002AD848 File Offset: 0x002ACC48
		internal int OpenResultsCount
		{
			get
			{
				return this._openResultCount;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x002AD868 File Offset: 0x002ACC68
		internal SqlTransaction Parent
		{
			get
			{
				SqlTransaction result = null;
				if (this._parent != null)
				{
					result = (SqlTransaction)this._parent.Target;
				}
				return result;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x002AD898 File Offset: 0x002ACC98
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x002AD8B8 File Offset: 0x002ACCB8
		internal long TransactionId
		{
			get
			{
				return this._transactionId;
			}
			set
			{
				this._transactionId = value;
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x002AD8D8 File Offset: 0x002ACCD8
		internal void Activate()
		{
			this._transactionState = TransactionState.Active;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x002AD8F8 File Offset: 0x002ACCF8
		private void CheckTransactionLevelAndZombie()
		{
			try
			{
				if (!this.IsZombied && this.GetServerTransactionLevel() == 0)
				{
					this.Zombie();
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e);
				this.Zombie();
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x002AD958 File Offset: 0x002ACD58
		internal void CloseFromConnection()
		{
			SqlInternalConnection innerConnection = this._innerConnection;
			Bid.PoolerTrace("<sc.SqlInteralTransaction.CloseFromConnection|RES|CPOOL> %d#, Closing\n", this.ObjectID);
			bool flag = true;
			try
			{
				innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				if (flag)
				{
					this.Zombie();
				}
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x002AD9D8 File Offset: 0x002ACDD8
		internal void Commit()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlInternalTransaction.Commit|API> %d#", this.ObjectID);
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, null, false);
				if (!this.IsZombied && !this._innerConnection.IsYukonOrNewer)
				{
					this.Zombie();
				}
				else
				{
					this.ZombieParent();
				}
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x002ADAA8 File Offset: 0x002ACEA8
		internal void Completed(TransactionState transactionState)
		{
			this._transactionState = transactionState;
			this.Zombie();
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x002ADAC8 File Offset: 0x002ACEC8
		internal int DecrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Decrement(ref this._openResultCount);
			if (num < 0)
			{
				throw ADP.InvalidOperation("Internal Error: Open Result Count Exceeded");
			}
			return num;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x002ADAF8 File Offset: 0x002ACEF8
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x002ADB18 File Offset: 0x002ACF18
		private void Dispose(bool disposing)
		{
			Bid.PoolerTrace("<sc.SqlInteralTransaction.Dispose|RES|CPOOL> %d#, Disposing\n", this.ObjectID);
			if (disposing && this._innerConnection != null)
			{
				this._disposing = true;
				this.Rollback();
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x002ADB58 File Offset: 0x002ACF58
		private int GetServerTransactionLevel()
		{
			int result;
			using (SqlCommand sqlCommand = new SqlCommand("set @out = @@trancount", (SqlConnection)this._innerConnection.Owner))
			{
				sqlCommand.Transaction = this.Parent;
				SqlParameter sqlParameter = new SqlParameter("@out", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlCommand.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, "GetServerTransactionLevel");
				result = (int)sqlParameter.Value;
			}
			return result;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x002ADBF8 File Offset: 0x002ACFF8
		internal int IncrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Increment(ref this._openResultCount);
			if (num < 0)
			{
				throw ADP.InvalidOperation("Internal Error: Open Result Count Exceeded");
			}
			return num;
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x002ADC28 File Offset: 0x002AD028
		internal void InitParent(SqlTransaction transaction)
		{
			this._parent = new WeakReference(transaction);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x002ADC48 File Offset: 0x002AD048
		internal void Rollback()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlInternalTransaction.Rollback|API> %d#", this.ObjectID);
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
				this.Zombie();
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				this.CheckTransactionLevelAndZombie();
				if (!this._disposing)
				{
					throw;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x002ADD08 File Offset: 0x002AD108
		internal void Rollback(string transactionName)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlInternalTransaction.Rollback|API> %d#, transactionName='%ls'", this.ObjectID, transactionName);
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				if (ADP.IsEmpty(transactionName))
				{
					throw SQL.NullEmptyTransactionName();
				}
				try
				{
					this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, transactionName, IsolationLevel.Unspecified, null, false);
					if (!this.IsZombied && !this._innerConnection.IsYukonOrNewer)
					{
						this.CheckTransactionLevelAndZombie();
					}
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						this.CheckTransactionLevelAndZombie();
					}
					throw;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x002ADDD8 File Offset: 0x002AD1D8
		internal void Save(string savePointName)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlInternalTransaction.Save|API> %d#, savePointName='%ls'", this.ObjectID, savePointName);
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				if (ADP.IsEmpty(savePointName))
				{
					throw SQL.NullEmptyTransactionName();
				}
				try
				{
					this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Save, savePointName, IsolationLevel.Unspecified, null, false);
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						this.CheckTransactionLevelAndZombie();
					}
					throw;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x002ADE78 File Offset: 0x002AD278
		internal void Zombie()
		{
			this.ZombieParent();
			SqlInternalConnection innerConnection = this._innerConnection;
			this._innerConnection = null;
			if (innerConnection != null)
			{
				innerConnection.DisconnectTransaction(this);
			}
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x002ADEA8 File Offset: 0x002AD2A8
		private void ZombieParent()
		{
			if (this._parent != null)
			{
				SqlTransaction sqlTransaction = (SqlTransaction)this._parent.Target;
				if (sqlTransaction != null)
				{
					sqlTransaction.Zombie();
				}
				this._parent = null;
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x002ADEE8 File Offset: 0x002AD2E8
		internal string TraceString()
		{
			return string.Format(null, "(ObjId={0}, tranId={1}, state={2}, type={3}, open={4}, disp={5}", new object[]
			{
				this.ObjectID,
				this._transactionId,
				this._transactionState,
				this._transactionType,
				this._openResultCount,
				this._disposing
			});
		}

		// Token: 0x0400192B RID: 6443
		internal const long NullTransactionId = 0L;

		// Token: 0x0400192C RID: 6444
		private TransactionState _transactionState;

		// Token: 0x0400192D RID: 6445
		private TransactionType _transactionType;

		// Token: 0x0400192E RID: 6446
		private long _transactionId;

		// Token: 0x0400192F RID: 6447
		private int _openResultCount;

		// Token: 0x04001930 RID: 6448
		private SqlInternalConnection _innerConnection;

		// Token: 0x04001931 RID: 6449
		private bool _disposing;

		// Token: 0x04001932 RID: 6450
		private WeakReference _parent;

		// Token: 0x04001933 RID: 6451
		private static int _objectTypeCount;

		// Token: 0x04001934 RID: 6452
		internal readonly int _objectID = Interlocked.Increment(ref SqlInternalTransaction._objectTypeCount);
	}
}
