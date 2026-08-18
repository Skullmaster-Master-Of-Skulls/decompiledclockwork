using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001D2 RID: 466
	internal abstract class SqlInternalConnection : DbConnectionInternal
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000CEE30 File Offset: 0x000CE230
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x000CEE44 File Offset: 0x000CE244
		internal string CurrentDatabase { get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x000CEE58 File Offset: 0x000CE258
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x000CEE6C File Offset: 0x000CE26C
		internal string CurrentDataSource { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000CEE80 File Offset: 0x000CE280
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x000CEE94 File Offset: 0x000CE294
		internal SqlDelegatedTransaction DelegatedTransaction { get; set; }

		// Token: 0x06001D46 RID: 7494 RVA: 0x000CEEA8 File Offset: 0x000CE2A8
		internal SqlInternalConnection(SqlConnectionString connectionOptions)
		{
			this._connectionOptions = connectionOptions;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x000CEEC4 File Offset: 0x000CE2C4
		internal SqlConnection Connection
		{
			get
			{
				return (SqlConnection)base.Owner;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x000CEEDC File Offset: 0x000CE2DC
		internal SqlConnectionString ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001D49 RID: 7497
		internal abstract SqlInternalTransaction CurrentTransaction { get; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x000CEEF0 File Offset: 0x000CE2F0
		internal virtual SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001D4B RID: 7499
		internal abstract SqlInternalTransaction PendingTransaction { get; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x000CEF04 File Offset: 0x000CE304
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x000CEF18 File Offset: 0x000CE318
		internal override bool IsTransactionRoot
		{
			get
			{
				SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
				return delegatedTransaction != null && delegatedTransaction.IsActive;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x000CEF38 File Offset: 0x000CE338
		internal bool HasLocalTransaction
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.IsLocal;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x000CEF5C File Offset: 0x000CE35C
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.HasParentTransaction;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x000CEF80 File Offset: 0x000CE380
		internal bool IsEnlistedInTransaction
		{
			get
			{
				return this._isEnlistedInTransaction;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001D51 RID: 7505
		internal abstract bool IsLockedForBulkCopy { get; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001D52 RID: 7506
		internal abstract bool IsShiloh { get; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001D53 RID: 7507
		internal abstract bool IsYukonOrNewer { get; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001D54 RID: 7508
		internal abstract bool IsKatmaiOrNewer { get; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000CEF94 File Offset: 0x000CE394
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x000CEFA8 File Offset: 0x000CE3A8
		internal byte[] PromotedDTCToken
		{
			get
			{
				return this._promotedDTCToken;
			}
			set
			{
				this._promotedDTCToken = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000CEFBC File Offset: 0x000CE3BC
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x000CEFD0 File Offset: 0x000CE3D0
		internal bool IsGlobalTransaction
		{
			get
			{
				return this._isGlobalTransaction;
			}
			set
			{
				this._isGlobalTransaction = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x000CEFE4 File Offset: 0x000CE3E4
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x000CEFF8 File Offset: 0x000CE3F8
		internal bool IsGlobalTransactionsEnabledForServer
		{
			get
			{
				return this._isGlobalTransactionEnabledForServer;
			}
			set
			{
				this._isGlobalTransactionEnabledForServer = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x000CF00C File Offset: 0x000CE40C
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x000CF020 File Offset: 0x000CE420
		internal bool IsAzureSQLConnection
		{
			get
			{
				return this._isAzureSQLConnection;
			}
			set
			{
				this._isAzureSQLConnection = value;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000CF034 File Offset: 0x000CE434
		public override DbTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginSqlTransaction(iso, null, false);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000CF04C File Offset: 0x000CE44C
		internal virtual SqlTransaction BeginSqlTransaction(IsolationLevel iso, string transactionName, bool shouldReconnect)
		{
			SqlStatistics statistics = null;
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SqlTransaction result;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this.Connection);
				statistics = SqlStatistics.StartTimer(this.Connection.Statistics);
				SqlConnection.ExecutePermission.Demand();
				this.ValidateConnectionForExecute(null);
				if (this.HasLocalTransactionFromAPI)
				{
					throw ADP.ParallelTransactionsNotSupported(this.Connection);
				}
				if (iso == IsolationLevel.Unspecified)
				{
					iso = IsolationLevel.ReadCommitted;
				}
				SqlTransaction sqlTransaction = new SqlTransaction(this, this.Connection, iso, this.AvailableInternalTransaction);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = shouldReconnect;
				this.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, transactionName, iso, sqlTransaction.InternalTransaction, false);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = false;
				result = sqlTransaction;
			}
			catch (OutOfMemoryException e)
			{
				this.Connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this.Connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this.Connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000CF19C File Offset: 0x000CE59C
		public override void ChangeDatabase(string database)
		{
			SqlConnection.ExecutePermission.Demand();
			if (ADP.IsEmpty(database))
			{
				throw ADP.EmptyDatabaseName();
			}
			this.ValidateConnectionForExecute(null);
			this.ChangeDatabaseInternal(database);
		}

		// Token: 0x06001D60 RID: 7520
		protected abstract void ChangeDatabaseInternal(string database);

		// Token: 0x06001D61 RID: 7521 RVA: 0x000CF1D0 File Offset: 0x000CE5D0
		protected override void CleanupTransactionOnCompletion(Transaction transaction)
		{
			SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.TransactionEnded(transaction);
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000CF1F0 File Offset: 0x000CE5F0
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new SqlReferenceCollection();
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000CF204 File Offset: 0x000CE604
		protected override void Deactivate()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnection.Deactivate|ADV> %d# deactivating\n", base.ObjectID);
			}
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this.Connection);
				SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
				if (sqlReferenceCollection != null)
				{
					sqlReferenceCollection.Deactivate();
				}
				this.InternalDeactivate();
			}
			catch (OutOfMemoryException)
			{
				base.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				base.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				base.DoomThisConnection();
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				base.DoomThisConnection();
				ADP.TraceExceptionWithoutRethrow(e);
			}
		}

		// Token: 0x06001D64 RID: 7524
		internal abstract void DisconnectTransaction(SqlInternalTransaction internalTransaction);

		// Token: 0x06001D65 RID: 7525 RVA: 0x000CF2FC File Offset: 0x000CE6FC
		public override void Dispose()
		{
			this._whereAbouts = null;
			base.Dispose();
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000CF318 File Offset: 0x000CE718
		protected void Enlist(Transaction tx)
		{
			if (null == tx)
			{
				if (this.IsEnlistedInTransaction)
				{
					this.EnlistNull();
					return;
				}
				Transaction enlistedTransaction = base.EnlistedTransaction;
				if (enlistedTransaction != null && enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					this.EnlistNull();
					return;
				}
			}
			else if (!tx.Equals(base.EnlistedTransaction))
			{
				this.EnlistNonNull(tx);
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000CF378 File Offset: 0x000CE778
		private void EnlistNonNull(Transaction tx)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnection.EnlistNonNull|ADV> %d#, transaction %d#.\n", base.ObjectID, tx.GetHashCode());
			}
			bool flag = false;
			if (this.IsYukonOrNewer)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnection.EnlistNonNull|ADV> %d#, attempting to delegate\n", base.ObjectID);
				}
				SqlDelegatedTransaction sqlDelegatedTransaction = new SqlDelegatedTransaction(this, tx);
				try
				{
					if (this._isGlobalTransaction)
					{
						if (SysTxForGlobalTransactions.EnlistPromotableSinglePhase == null)
						{
							flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
						}
						else
						{
							flag = (bool)SysTxForGlobalTransactions.EnlistPromotableSinglePhase.Invoke(tx, new object[]
							{
								sqlDelegatedTransaction,
								SqlInternalConnection._globalTransactionTMID
							});
						}
					}
					else
					{
						flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
					}
					if (flag)
					{
						this.DelegatedTransaction = sqlDelegatedTransaction;
						if (Bid.AdvancedOn)
						{
							long a = 0L;
							int a2 = 0;
							if (this.CurrentTransaction != null)
							{
								a = this.CurrentTransaction.TransactionId;
								a2 = this.CurrentTransaction.ObjectID;
							}
							Bid.Trace("<sc.SqlInternalConnection.EnlistNonNull|ADV> %d#, delegated to transaction %d# with transactionId=0x%I64x\n", base.ObjectID, a2, a);
						}
					}
				}
				catch (SqlException ex)
				{
					if (ex.Class >= 20)
					{
						throw;
					}
					SqlInternalConnectionTds sqlInternalConnectionTds = this as SqlInternalConnectionTds;
					if (sqlInternalConnectionTds != null)
					{
						TdsParser parser = sqlInternalConnectionTds.Parser;
						if (parser == null || parser.State != TdsParserState.OpenLoggedIn)
						{
							throw;
						}
					}
					ADP.TraceExceptionWithoutRethrow(ex);
				}
			}
			if (!flag)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnection.EnlistNonNull|ADV> %d#, delegation not possible, enlisting.\n", base.ObjectID);
				}
				byte[] transactionCookie;
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.GetPromotedToken == null)
					{
						throw SQL.UnsupportedSysTxForGlobalTransactions();
					}
					transactionCookie = (byte[])SysTxForGlobalTransactions.GetPromotedToken.Invoke(tx, null);
				}
				else
				{
					if (this._whereAbouts == null)
					{
						byte[] dtcaddress = this.GetDTCAddress();
						if (dtcaddress == null)
						{
							throw SQL.CannotGetDTCAddress();
						}
						this._whereAbouts = dtcaddress;
					}
					transactionCookie = SqlInternalConnection.GetTransactionCookie(tx, this._whereAbouts);
				}
				this.PropagateTransactionCookie(transactionCookie);
				this._isEnlistedInTransaction = true;
				if (Bid.AdvancedOn)
				{
					long a3 = 0L;
					int a4 = 0;
					if (this.CurrentTransaction != null)
					{
						a3 = this.CurrentTransaction.TransactionId;
						a4 = this.CurrentTransaction.ObjectID;
					}
					Bid.Trace("<sc.SqlInternalConnection.EnlistNonNull|ADV> %d#, enlisted with transaction %d# with transactionId=0x%I64x\n", base.ObjectID, a4, a3);
				}
			}
			base.EnlistedTransaction = tx;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000CF59C File Offset: 0x000CE99C
		internal void EnlistNull()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnection.EnlistNull|ADV> %d#, unenlisting.\n", base.ObjectID);
			}
			this.PropagateTransactionCookie(null);
			this._isEnlistedInTransaction = false;
			base.EnlistedTransaction = null;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnection.EnlistNull|ADV> %d#, unenlisted.\n", base.ObjectID);
			}
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000CF5EC File Offset: 0x000CE9EC
		public override void EnlistTransaction(Transaction transaction)
		{
			this.ValidateConnectionForExecute(null);
			if (this.HasLocalTransaction)
			{
				throw ADP.LocalTransactionPresent();
			}
			if (null != transaction && transaction.Equals(base.EnlistedTransaction))
			{
				return;
			}
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this.Connection);
				this.Enlist(transaction);
			}
			catch (OutOfMemoryException e)
			{
				this.Connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this.Connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this.Connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
		}

		// Token: 0x06001D6A RID: 7530
		internal abstract void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest);

		// Token: 0x06001D6B RID: 7531 RVA: 0x000CF6C8 File Offset: 0x000CEAC8
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			SqlDataReader result = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				result = sqlReferenceCollection.FindLiveReader(command);
			}
			return result;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000CF6F0 File Offset: 0x000CEAF0
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			SqlCommand result = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				result = sqlReferenceCollection.FindLiveCommand(stateObj);
			}
			return result;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000CF718 File Offset: 0x000CEB18
		internal static TdsParser GetBestEffortCleanupTarget(SqlConnection connection)
		{
			if (connection != null)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = connection.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					return sqlInternalConnectionTds.Parser;
				}
			}
			return null;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000CF740 File Offset: 0x000CEB40
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void BestEffortCleanup(TdsParser target)
		{
			if (target != null)
			{
				target.BestEffortCleanup();
			}
		}

		// Token: 0x06001D6F RID: 7535
		protected abstract byte[] GetDTCAddress();

		// Token: 0x06001D70 RID: 7536 RVA: 0x000CF758 File Offset: 0x000CEB58
		private static byte[] GetTransactionCookie(Transaction transaction, byte[] whereAbouts)
		{
			byte[] result = null;
			if (null != transaction)
			{
				result = TransactionInterop.GetExportCookie(transaction, whereAbouts);
			}
			return result;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000CF77C File Offset: 0x000CEB7C
		protected virtual void InternalDeactivate()
		{
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000CF78C File Offset: 0x000CEB8C
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction = null)
		{
			if (breakConnection)
			{
				base.DoomThisConnection();
			}
			SqlConnection connection = this.Connection;
			if (connection != null)
			{
				connection.OnError(exception, breakConnection, wrapCloseInAction);
				return;
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
		}

		// Token: 0x06001D73 RID: 7539
		protected abstract void PropagateTransactionCookie(byte[] transactionCookie);

		// Token: 0x06001D74 RID: 7540
		internal abstract void ValidateConnectionForExecute(SqlCommand command);

		// Token: 0x040010BC RID: 4284
		private readonly SqlConnectionString _connectionOptions;

		// Token: 0x040010BD RID: 4285
		private bool _isEnlistedInTransaction;

		// Token: 0x040010BE RID: 4286
		private byte[] _promotedDTCToken;

		// Token: 0x040010BF RID: 4287
		private byte[] _whereAbouts;

		// Token: 0x040010C0 RID: 4288
		private bool _isGlobalTransaction;

		// Token: 0x040010C1 RID: 4289
		private bool _isGlobalTransactionEnabledForServer;

		// Token: 0x040010C2 RID: 4290
		private static readonly Guid _globalTransactionTMID = new Guid("1c742caf-6680-40ea-9c26-6b6846079764");

		// Token: 0x040010C3 RID: 4291
		private bool _isAzureSQLConnection;

		// Token: 0x020003C4 RID: 964
		internal enum TransactionRequest
		{
			// Token: 0x040020D1 RID: 8401
			Begin,
			// Token: 0x040020D2 RID: 8402
			Promote,
			// Token: 0x040020D3 RID: 8403
			Commit,
			// Token: 0x040020D4 RID: 8404
			Rollback,
			// Token: 0x040020D5 RID: 8405
			IfRollback,
			// Token: 0x040020D6 RID: 8406
			Save
		}
	}
}
