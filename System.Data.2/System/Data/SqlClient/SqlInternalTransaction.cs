using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001E4 RID: 484
	internal sealed class SqlInternalTransaction
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x000D3BE4 File Offset: 0x000D2FE4
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x000D3BF8 File Offset: 0x000D2FF8
		internal bool RestoreBrokenConnection { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x000D3C0C File Offset: 0x000D300C
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x000D3C20 File Offset: 0x000D3020
		internal bool ConnectionHasBeenRestored { get; set; }

		// Token: 0x06001E29 RID: 7721 RVA: 0x000D3C34 File Offset: 0x000D3034
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction) : this(innerConnection, type, outerTransaction, 0L)
		{
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000D3C4C File Offset: 0x000D304C
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
			this.RestoreBrokenConnection = false;
			this.ConnectionHasBeenRestored = false;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000D3CC8 File Offset: 0x000D30C8
		internal bool HasParentTransaction
		{
			get
			{
				return TransactionType.LocalFromAPI == this._transactionType || (TransactionType.LocalFromTSQL == this._transactionType && this._parent != null);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000D3CF8 File Offset: 0x000D30F8
		internal bool IsAborted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000D3D10 File Offset: 0x000D3110
		internal bool IsActive
		{
			get
			{
				return TransactionState.Active == this._transactionState;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000D3D28 File Offset: 0x000D3128
		internal bool IsCommitted
		{
			get
			{
				return TransactionState.Committed == this._transactionState;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x000D3D40 File Offset: 0x000D3140
		internal bool IsCompleted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState || TransactionState.Committed == this._transactionState || TransactionState.Unknown == this._transactionState;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x000D3D6C File Offset: 0x000D316C
		internal bool IsContext
		{
			get
			{
				return TransactionType.Context == this._transactionType;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000D3D84 File Offset: 0x000D3184
		internal bool IsDelegated
		{
			get
			{
				return TransactionType.Delegated == this._transactionType;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x000D3D9C File Offset: 0x000D319C
		internal bool IsDistributed
		{
			get
			{
				return TransactionType.Distributed == this._transactionType;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000D3DB4 File Offset: 0x000D31B4
		internal bool IsLocal
		{
			get
			{
				return TransactionType.LocalFromTSQL == this._transactionType || TransactionType.LocalFromAPI == this._transactionType || TransactionType.Context == this._transactionType;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000D3DE4 File Offset: 0x000D31E4
		internal bool IsOrphaned
		{
			get
			{
				return this._parent != null && this._parent.Target == null;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000D3E14 File Offset: 0x000D3214
		internal bool IsZombied
		{
			get
			{
				return this._innerConnection == null;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000D3E2C File Offset: 0x000D322C
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000D3E40 File Offset: 0x000D3240
		internal int OpenResultsCount
		{
			get
			{
				return this._openResultCount;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000D3E54 File Offset: 0x000D3254
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

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000D3E80 File Offset: 0x000D3280
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x000D3E94 File Offset: 0x000D3294
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

		// Token: 0x06001E3B RID: 7739 RVA: 0x000D3EA8 File Offset: 0x000D32A8
		internal void Activate()
		{
			this._transactionState = TransactionState.Active;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000D3EBC File Offset: 0x000D32BC
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

		// Token: 0x06001E3D RID: 7741 RVA: 0x000D3F18 File Offset: 0x000D3318
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

		// Token: 0x06001E3E RID: 7742 RVA: 0x000D3F98 File Offset: 0x000D3398
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

		// Token: 0x06001E3F RID: 7743 RVA: 0x000D405C File Offset: 0x000D345C
		internal void Completed(TransactionState transactionState)
		{
			this._transactionState = transactionState;
			this.Zombie();
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000D4078 File Offset: 0x000D3478
		internal int DecrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Decrement(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000D409C File Offset: 0x000D349C
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000D40B8 File Offset: 0x000D34B8
		private void Dispose(bool disposing)
		{
			Bid.PoolerTrace("<sc.SqlInteralTransaction.Dispose|RES|CPOOL> %d#, Disposing\n", this.ObjectID);
			if (disposing && this._innerConnection != null)
			{
				this._disposing = true;
				this.Rollback();
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000D40F0 File Offset: 0x000D34F0
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

		// Token: 0x06001E44 RID: 7748 RVA: 0x000D4188 File Offset: 0x000D3588
		internal int IncrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Increment(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000D41AC File Offset: 0x000D35AC
		internal void InitParent(SqlTransaction transaction)
		{
			this._parent = new WeakReference(transaction);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000D41C8 File Offset: 0x000D35C8
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

		// Token: 0x06001E47 RID: 7751 RVA: 0x000D427C File Offset: 0x000D367C
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

		// Token: 0x06001E48 RID: 7752 RVA: 0x000D4348 File Offset: 0x000D3748
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

		// Token: 0x06001E49 RID: 7753 RVA: 0x000D43E8 File Offset: 0x000D37E8
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

		// Token: 0x06001E4A RID: 7754 RVA: 0x000D4414 File Offset: 0x000D3814
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

		// Token: 0x06001E4B RID: 7755 RVA: 0x000D444C File Offset: 0x000D384C
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

		// Token: 0x0400113B RID: 4411
		internal const long NullTransactionId = 0L;

		// Token: 0x0400113C RID: 4412
		private TransactionState _transactionState;

		// Token: 0x0400113D RID: 4413
		private TransactionType _transactionType;

		// Token: 0x0400113E RID: 4414
		private long _transactionId;

		// Token: 0x0400113F RID: 4415
		private int _openResultCount;

		// Token: 0x04001140 RID: 4416
		private SqlInternalConnection _innerConnection;

		// Token: 0x04001141 RID: 4417
		private bool _disposing;

		// Token: 0x04001142 RID: 4418
		private WeakReference _parent;

		// Token: 0x04001143 RID: 4419
		private static int _objectTypeCount;

		// Token: 0x04001144 RID: 4420
		internal readonly int _objectID = Interlocked.Increment(ref SqlInternalTransaction._objectTypeCount);
	}
}
