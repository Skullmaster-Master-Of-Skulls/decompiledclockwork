using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020002F7 RID: 759
	internal abstract class SqlInternalConnection : DbConnectionInternal
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x002AA608 File Offset: 0x002A9A08
		internal SqlInternalConnection(SqlConnectionString connectionOptions)
		{
			this._connectionOptions = connectionOptions;
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x002AA628 File Offset: 0x002A9A28
		internal SqlConnection Connection
		{
			get
			{
				return (SqlConnection)base.Owner;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x002AA648 File Offset: 0x002A9A48
		internal SqlConnectionString ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x002AA668 File Offset: 0x002A9A68
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x002AA688 File Offset: 0x002A9A88
		internal string CurrentDatabase
		{
			get
			{
				return this._currentDatabase;
			}
			set
			{
				this._currentDatabase = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x002AA6A8 File Offset: 0x002A9AA8
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x002AA6C8 File Offset: 0x002A9AC8
		internal string CurrentDataSource
		{
			get
			{
				return this._currentDataSource;
			}
			set
			{
				this._currentDataSource = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x002AA6E8 File Offset: 0x002A9AE8
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x002AA708 File Offset: 0x002A9B08
		internal SqlDelegatedTransaction DelegatedTransaction
		{
			get
			{
				return this._delegatedTransaction;
			}
			set
			{
				this._delegatedTransaction = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600274B RID: 10059
		internal abstract SqlInternalTransaction CurrentTransaction { get; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x002AA728 File Offset: 0x002A9B28
		internal virtual SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600274D RID: 10061
		internal abstract SqlInternalTransaction PendingTransaction { get; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x002AA748 File Offset: 0x002A9B48
		internal override bool RequireExplicitTransactionUnbind
		{
			get
			{
				return this._connectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x002AA768 File Offset: 0x002A9B68
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x002AA788 File Offset: 0x002A9B88
		internal override bool IsTransactionRoot
		{
			get
			{
				if (this._delegatedTransaction == null)
				{
					return false;
				}
				bool result;
				lock (this)
				{
					result = (this._delegatedTransaction != null && this._delegatedTransaction.IsActive);
				}
				return result;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x002AA7E8 File Offset: 0x002A9BE8
		internal bool HasLocalTransaction
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.IsLocal;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x002AA818 File Offset: 0x002A9C18
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.HasParentTransaction;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x002AA848 File Offset: 0x002A9C48
		internal bool IsEnlistedInTransaction
		{
			get
			{
				return this._isEnlistedInTransaction;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x002AA868 File Offset: 0x002A9C68
		// (set) Token: 0x06002755 RID: 10069 RVA: 0x002AA888 File Offset: 0x002A9C88
		protected internal Transaction ContextTransaction
		{
			get
			{
				return this._contextTransaction;
			}
			set
			{
				this._contextTransaction = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x002AA8A8 File Offset: 0x002A9CA8
		internal Transaction InternalEnlistedTransaction
		{
			get
			{
				Transaction transaction = base.EnlistedTransaction;
				if (null == transaction)
				{
					transaction = this.ContextTransaction;
				}
				return transaction;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002757 RID: 10071
		internal abstract bool IsLockedForBulkCopy { get; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002758 RID: 10072
		internal abstract bool IsShiloh { get; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002759 RID: 10073
		internal abstract bool IsYukonOrNewer { get; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600275A RID: 10074
		internal abstract bool IsKatmaiOrNewer { get; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x002AA8D8 File Offset: 0x002A9CD8
		// (set) Token: 0x0600275C RID: 10076 RVA: 0x002AA8F8 File Offset: 0x002A9CF8
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

		// Token: 0x0600275D RID: 10077
		internal abstract void AddPreparedCommand(SqlCommand cmd);

		// Token: 0x0600275E RID: 10078 RVA: 0x002AA918 File Offset: 0x002A9D18
		public override DbTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginSqlTransaction(iso, null);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x002AA938 File Offset: 0x002A9D38
		internal virtual SqlTransaction BeginSqlTransaction(IsolationLevel iso, string transactionName)
		{
			SqlStatistics statistics = null;
			SNIHandle target = null;
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
				this.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, transactionName, iso, sqlTransaction.InternalTransaction, false);
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

		// Token: 0x06002760 RID: 10080 RVA: 0x002AAA78 File Offset: 0x002A9E78
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

		// Token: 0x06002761 RID: 10081
		protected abstract void ChangeDatabaseInternal(string database);

		// Token: 0x06002762 RID: 10082
		internal abstract void ClearPreparedCommands();

		// Token: 0x06002763 RID: 10083 RVA: 0x002AAAB8 File Offset: 0x002A9EB8
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			if (!base.IsConnectionDoomed)
			{
				this.ClearPreparedCommands();
			}
			base.CloseConnection(owningObject, connectionFactory);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x002AAAE8 File Offset: 0x002A9EE8
		protected override void CleanupTransactionOnCompletion(Transaction transaction)
		{
			SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.TransactionEnded(transaction);
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x002AAB08 File Offset: 0x002A9F08
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new SqlReferenceCollection();
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x002AAB28 File Offset: 0x002A9F28
		protected override void Deactivate()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnection.Deactivate|ADV> %d# deactivating\n", base.ObjectID);
			}
			SNIHandle target = null;
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

		// Token: 0x06002767 RID: 10087
		internal abstract void DisconnectTransaction(SqlInternalTransaction internalTransaction);

		// Token: 0x06002768 RID: 10088 RVA: 0x002AAC28 File Offset: 0x002AA028
		public override void Dispose()
		{
			this._whereAbouts = null;
			base.Dispose();
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x002AAC48 File Offset: 0x002AA048
		protected void Enlist(Transaction tx)
		{
			if (null == tx)
			{
				if (this.IsEnlistedInTransaction)
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

		// Token: 0x0600276A RID: 10090 RVA: 0x002AAC88 File Offset: 0x002AA088
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
					if (tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction))
					{
						flag = true;
						this._delegatedTransaction = sqlDelegatedTransaction;
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
					if (sqlInternalConnectionTds != null && sqlInternalConnectionTds.Parser.State != TdsParserState.OpenLoggedIn)
					{
						throw;
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
				if (this._whereAbouts == null)
				{
					byte[] dtcaddress = this.GetDTCAddress();
					if (dtcaddress == null)
					{
						throw SQL.CannotGetDTCAddress();
					}
					this._whereAbouts = dtcaddress;
				}
				byte[] transactionCookie = SqlInternalConnection.GetTransactionCookie(tx, this._whereAbouts);
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

		// Token: 0x0600276B RID: 10091 RVA: 0x002AAE38 File Offset: 0x002AA238
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

		// Token: 0x0600276C RID: 10092 RVA: 0x002AAE88 File Offset: 0x002AA288
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
			SNIHandle target = null;
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

		// Token: 0x0600276D RID: 10093
		internal abstract void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest);

		// Token: 0x0600276E RID: 10094 RVA: 0x002AAF68 File Offset: 0x002AA368
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

		// Token: 0x0600276F RID: 10095 RVA: 0x002AAF98 File Offset: 0x002AA398
		internal static SNIHandle GetBestEffortCleanupTarget(SqlConnection connection)
		{
			if (connection != null)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = connection.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser != null)
					{
						return parser.GetBestEffortCleanupTarget();
					}
				}
			}
			return null;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x002AAFD8 File Offset: 0x002AA3D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void BestEffortCleanup(SNIHandle target)
		{
			if (target != null)
			{
				target.Dispose();
			}
		}

		// Token: 0x06002771 RID: 10097
		protected abstract byte[] GetDTCAddress();

		// Token: 0x06002772 RID: 10098 RVA: 0x002AAFF8 File Offset: 0x002AA3F8
		private static byte[] GetTransactionCookie(Transaction transaction, byte[] whereAbouts)
		{
			byte[] result = null;
			if (null != transaction)
			{
				result = TransactionInterop.GetExportCookie(transaction, whereAbouts);
			}
			return result;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x002AB028 File Offset: 0x002AA428
		protected virtual void InternalDeactivate()
		{
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x002AB038 File Offset: 0x002AA438
		internal void OnError(SqlException exception, bool breakConnection)
		{
			if (breakConnection)
			{
				base.DoomThisConnection();
			}
			if (this.Connection != null)
			{
				this.Connection.OnError(exception, breakConnection);
				return;
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
		}

		// Token: 0x06002775 RID: 10101
		protected abstract void PropagateTransactionCookie(byte[] transactionCookie);

		// Token: 0x06002776 RID: 10102
		internal abstract void RemovePreparedCommand(SqlCommand cmd);

		// Token: 0x06002777 RID: 10103
		internal abstract void ValidateConnectionForExecute(SqlCommand command);

		// Token: 0x06002778 RID: 10104 RVA: 0x002AB078 File Offset: 0x002AA478
		internal void ValidateTransaction()
		{
			if (this.RequireExplicitTransactionUnbind && null != this.InternalEnlistedTransaction)
			{
				Transaction transaction = Transaction.Current;
				if (this.InternalEnlistedTransaction.TransactionInformation.Status != TransactionStatus.Active || null == transaction || !this.InternalEnlistedTransaction.Equals(transaction))
				{
					throw ADP.TransactionConnectionMismatch();
				}
			}
		}

		// Token: 0x040018F5 RID: 6389
		private readonly SqlConnectionString _connectionOptions;

		// Token: 0x040018F6 RID: 6390
		private string _currentDatabase;

		// Token: 0x040018F7 RID: 6391
		private string _currentDataSource;

		// Token: 0x040018F8 RID: 6392
		private bool _isEnlistedInTransaction;

		// Token: 0x040018F9 RID: 6393
		private byte[] _promotedDTCToken;

		// Token: 0x040018FA RID: 6394
		private SqlDelegatedTransaction _delegatedTransaction;

		// Token: 0x040018FB RID: 6395
		private byte[] _whereAbouts;

		// Token: 0x040018FC RID: 6396
		private Transaction _contextTransaction;

		// Token: 0x020002F8 RID: 760
		internal enum TransactionRequest
		{
			// Token: 0x040018FE RID: 6398
			Begin,
			// Token: 0x040018FF RID: 6399
			Promote,
			// Token: 0x04001900 RID: 6400
			Commit,
			// Token: 0x04001901 RID: 6401
			Rollback,
			// Token: 0x04001902 RID: 6402
			IfRollback,
			// Token: 0x04001903 RID: 6403
			Save
		}
	}
}
