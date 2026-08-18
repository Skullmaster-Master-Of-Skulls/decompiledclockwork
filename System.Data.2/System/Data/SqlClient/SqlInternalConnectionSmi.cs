using System;
using System.Data.Common;
using System.Threading;
using System.Transactions;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001D3 RID: 467
	internal sealed class SqlInternalConnectionSmi : SqlInternalConnection
	{
		// Token: 0x06001D76 RID: 7542 RVA: 0x000CF7E0 File Offset: 0x000CEBE0
		internal SqlInternalConnectionSmi(SqlConnectionString connectionOptions, SmiContext smiContext) : base(connectionOptions)
		{
			this._smiContext = smiContext;
			this._smiContext.OutOfScope += this.OnOutOfScope;
			this._smiConnection = this._smiContext.ContextConnection;
			this._smiEventSink = new SqlInternalConnectionSmi.EventSink(this);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.ctor|ADV> %d#, constructed new SMI internal connection\n", base.ObjectID);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x000CF848 File Offset: 0x000CEC48
		internal SmiContext InternalContext
		{
			get
			{
				return this._smiContext;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000CF85C File Offset: 0x000CEC5C
		internal SmiConnection SmiConnection
		{
			get
			{
				return this._smiConnection;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x000CF870 File Offset: 0x000CEC70
		internal SmiEventSink CurrentEventSink
		{
			get
			{
				return this._smiEventSink;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x000CF884 File Offset: 0x000CEC84
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._currentTransaction;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x000CF898 File Offset: 0x000CEC98
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x000CF8A8 File Offset: 0x000CECA8
		internal override bool IsShiloh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x000CF8B8 File Offset: 0x000CECB8
		internal override bool IsYukonOrNewer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x000CF8C8 File Offset: 0x000CECC8
		internal override bool IsKatmaiOrNewer
		{
			get
			{
				return SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x000CF8EC File Offset: 0x000CECEC
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000CF900 File Offset: 0x000CED00
		public override string ServerVersion
		{
			get
			{
				return SmiContextFactory.Instance.ServerVersion;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x000CF918 File Offset: 0x000CED18
		protected override bool UnbindOnTransactionCompletion
		{
			get
			{
				return base.ConnectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ImplicitUnbind;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000CF934 File Offset: 0x000CED34
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x000CF948 File Offset: 0x000CED48
		private Transaction ContextTransaction { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x000CF95C File Offset: 0x000CED5C
		private Transaction InternalEnlistedTransaction
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

		// Token: 0x06001D85 RID: 7557 RVA: 0x000CF984 File Offset: 0x000CED84
		protected override void Activate(Transaction transaction)
		{
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000CF994 File Offset: 0x000CED94
		internal void Activate()
		{
			int num = Interlocked.Exchange(ref this._isInUse, 1);
			if (num != 0)
			{
				throw SQL.ContextConnectionIsInUse();
			}
			base.CurrentDatabase = this._smiConnection.GetCurrentDatabase(this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000CF9DC File Offset: 0x000CEDDC
		internal void AutomaticEnlistment()
		{
			Transaction currentTransaction = ADP.GetCurrentTransaction();
			Transaction contextTransaction = this._smiContext.ContextTransaction;
			long contextTransactionId = this._smiContext.ContextTransactionId;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> %d#, contextTransactionId=0x%I64x, contextTransaction=%d#, currentSystemTransaction=%d#.\n", base.ObjectID, contextTransactionId, (null != contextTransaction) ? contextTransaction.GetHashCode() : 0, (null != currentTransaction) ? currentTransaction.GetHashCode() : 0);
			}
			if (contextTransactionId == 0L)
			{
				if (null == currentTransaction)
				{
					this._currentTransaction = null;
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> %d#, no transaction.\n", base.ObjectID);
						return;
					}
				}
				else
				{
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> %d#, using current System.Transaction.\n", base.ObjectID);
					}
					base.Enlist(currentTransaction);
				}
				return;
			}
			if (null != currentTransaction && contextTransaction != currentTransaction)
			{
				throw SQL.NestedTransactionScopesNotSupported();
			}
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> %d#, using context transaction with transactionId=0x%I64x\n", base.ObjectID, contextTransactionId);
			}
			this._currentTransaction = new SqlInternalTransaction(this, TransactionType.Context, null, contextTransactionId);
			this.ContextTransaction = contextTransaction;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000CFAD4 File Offset: 0x000CEED4
		protected override void ChangeDatabaseInternal(string database)
		{
			this._smiConnection.SetCurrentDatabase(database, this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000CFB00 File Offset: 0x000CEF00
		protected override void InternalDeactivate()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.Deactivate|ADV> %d#, Deactivating.\n", base.ObjectID);
			}
			if (!this.IsNonPoolableTransactionRoot)
			{
				base.Enlist(null);
			}
			if (this._currentTransaction != null)
			{
				if (this._currentTransaction.IsContext)
				{
					this._currentTransaction = null;
				}
				else if (this._currentTransaction.IsLocal)
				{
					this._currentTransaction.CloseFromConnection();
				}
			}
			this.ContextTransaction = null;
			this._isInUse = 0;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000CFB78 File Offset: 0x000CEF78
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.DelegatedTransactionEnded|ADV> %d#, cleaning up after Delegated Transaction Completion\n", base.ObjectID);
			}
			this._currentTransaction = null;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000CFBAC File Offset: 0x000CEFAC
		internal override void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.DisconnectTransaction|ADV> %d#, Disconnecting Transaction %d#.\n", base.ObjectID, internalTransaction.ObjectID);
			}
			if (this._currentTransaction != null && this._currentTransaction == internalTransaction)
			{
				this._currentTransaction = null;
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000CFBF0 File Offset: 0x000CEFF0
		public override void Dispose()
		{
			this._smiContext.OutOfScope -= this.OnOutOfScope;
			base.Dispose();
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000CFC1C File Offset: 0x000CF01C
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.ExecuteTransaction|ADV> %d#, transactionRequest=%ls, transactionName='%ls', isolationLevel=%ls, internalTransaction=#%d transactionId=0x%I64x.\n", base.ObjectID, transactionRequest.ToString(), (transactionName != null) ? transactionName : "null", iso.ToString(), (internalTransaction != null) ? internalTransaction.ObjectID : 0, (internalTransaction != null) ? internalTransaction.TransactionId : 0L);
			}
			switch (transactionRequest)
			{
			case SqlInternalConnection.TransactionRequest.Begin:
				try
				{
					this._pendingTransaction = internalTransaction;
					this._smiConnection.BeginTransaction(transactionName, iso, this._smiEventSink);
					goto IL_123;
				}
				finally
				{
					this._pendingTransaction = null;
				}
				break;
			case SqlInternalConnection.TransactionRequest.Promote:
				base.PromotedDTCToken = this._smiConnection.PromoteTransaction(this._currentTransaction.TransactionId, this._smiEventSink);
				goto IL_123;
			case SqlInternalConnection.TransactionRequest.Commit:
				break;
			case SqlInternalConnection.TransactionRequest.Rollback:
			case SqlInternalConnection.TransactionRequest.IfRollback:
				this._smiConnection.RollbackTransaction(this._currentTransaction.TransactionId, transactionName, this._smiEventSink);
				goto IL_123;
			case SqlInternalConnection.TransactionRequest.Save:
				this._smiConnection.CreateTransactionSavePoint(this._currentTransaction.TransactionId, transactionName, this._smiEventSink);
				goto IL_123;
			default:
				goto IL_123;
			}
			this._smiConnection.CommitTransaction(this._currentTransaction.TransactionId, this._smiEventSink);
			IL_123:
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000CFD74 File Offset: 0x000CF174
		protected override byte[] GetDTCAddress()
		{
			byte[] dtcaddress = this._smiConnection.GetDTCAddress(this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
			if (Bid.AdvancedOn)
			{
				if (dtcaddress != null)
				{
					Bid.TraceBin("<sc.SqlInternalConnectionSmi.GetDTCAddress|ADV> whereAbouts", dtcaddress, (ushort)dtcaddress.Length);
				}
				else
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.GetDTCAddress|ADV> whereAbouts=null\n");
				}
			}
			return dtcaddress;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000CFDC4 File Offset: 0x000CF1C4
		internal void GetCurrentTransactionPair(out long transactionId, out Transaction transaction)
		{
			lock (this)
			{
				transactionId = ((this.CurrentTransaction != null) ? this.CurrentTransaction.TransactionId : 0L);
				transaction = null;
				if (transactionId != 0L)
				{
					transaction = this.InternalEnlistedTransaction;
				}
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000CFE30 File Offset: 0x000CF230
		private void OnOutOfScope(object s, EventArgs e)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.OutOfScope|ADV> %d# context is out of scope\n", base.ObjectID);
			}
			base.DelegatedTransaction = null;
			DbConnection dbConnection = (DbConnection)base.Owner;
			try
			{
				if (dbConnection != null && 1 == this._isInUse)
				{
					dbConnection.Close();
				}
			}
			finally
			{
				this.ContextTransaction = null;
				this._isInUse = 0;
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000CFEA8 File Offset: 0x000CF2A8
		protected override void PropagateTransactionCookie(byte[] transactionCookie)
		{
			if (Bid.AdvancedOn)
			{
				if (transactionCookie != null)
				{
					Bid.TraceBin("<sc.SqlInternalConnectionSmi.PropagateTransactionCookie|ADV> transactionCookie", transactionCookie, (ushort)transactionCookie.Length);
				}
				else
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.PropagateTransactionCookie|ADV> null\n");
				}
			}
			this._smiConnection.EnlistTransaction(transactionCookie, this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000CFEF8 File Offset: 0x000CF2F8
		private void TransactionEndedByServer(long transactionId, TransactionState transactionState)
		{
			SqlDelegatedTransaction delegatedTransaction = base.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.Transaction.Rollback();
				base.DelegatedTransaction = null;
			}
			this.TransactionEnded(transactionId, transactionState);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000CFF2C File Offset: 0x000CF32C
		private void TransactionEnded(long transactionId, TransactionState transactionState)
		{
			if (this._currentTransaction != null)
			{
				this._currentTransaction.Completed(transactionState);
				this._currentTransaction = null;
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000CFF54 File Offset: 0x000CF354
		private void TransactionStarted(long transactionId, bool isDistributed)
		{
			this._currentTransaction = this._pendingTransaction;
			this._pendingTransaction = null;
			if (this._currentTransaction != null)
			{
				this._currentTransaction.TransactionId = transactionId;
			}
			else
			{
				TransactionType type = isDistributed ? TransactionType.Distributed : TransactionType.LocalFromTSQL;
				this._currentTransaction = new SqlInternalTransaction(this, type, null, transactionId);
			}
			this._currentTransaction.Activate();
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000CFFAC File Offset: 0x000CF3AC
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			SqlDataReader sqlDataReader = base.FindLiveReader(null);
			if (sqlDataReader != null)
			{
				throw ADP.OpenReaderExists();
			}
		}

		// Token: 0x040010C7 RID: 4295
		private SmiContext _smiContext;

		// Token: 0x040010C8 RID: 4296
		private SmiConnection _smiConnection;

		// Token: 0x040010C9 RID: 4297
		private SmiEventSink_Default _smiEventSink;

		// Token: 0x040010CA RID: 4298
		private int _isInUse;

		// Token: 0x040010CB RID: 4299
		private SqlInternalTransaction _pendingTransaction;

		// Token: 0x040010CC RID: 4300
		private SqlInternalTransaction _currentTransaction;

		// Token: 0x020003C5 RID: 965
		private sealed class EventSink : SmiEventSink_Default
		{
			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x06003523 RID: 13603 RVA: 0x00143F50 File Offset: 0x00143350
			internal override string ServerVersion
			{
				get
				{
					return SmiContextFactory.Instance.ServerVersion;
				}
			}

			// Token: 0x06003524 RID: 13604 RVA: 0x00143F68 File Offset: 0x00143368
			protected override void DispatchMessages(bool ignoreNonFatalMessages)
			{
				SqlException ex = base.ProcessMessages(false, ignoreNonFatalMessages);
				if (ex != null)
				{
					SqlConnection connection = this._connection.Connection;
					if (connection != null && connection.FireInfoMessageEventOnUserErrors)
					{
						connection.OnInfoMessage(new SqlInfoMessageEventArgs(ex));
						return;
					}
					this._connection.OnError(ex, false, null);
				}
			}

			// Token: 0x06003525 RID: 13605 RVA: 0x00143FB4 File Offset: 0x001433B4
			internal EventSink(SqlInternalConnectionSmi connection)
			{
				this._connection = connection;
			}

			// Token: 0x06003526 RID: 13606 RVA: 0x00143FD0 File Offset: 0x001433D0
			internal override void DefaultDatabaseChanged(string databaseName)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.DefaultDatabaseChanged|ADV> %d#, databaseName='%ls'.\n", this._connection.ObjectID, databaseName);
				}
				this._connection.CurrentDatabase = databaseName;
			}

			// Token: 0x06003527 RID: 13607 RVA: 0x00144008 File Offset: 0x00143408
			internal override void TransactionCommitted(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionCommitted|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEnded(transactionId, TransactionState.Committed);
			}

			// Token: 0x06003528 RID: 13608 RVA: 0x00144040 File Offset: 0x00143440
			internal override void TransactionDefected(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionDefected|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEnded(transactionId, TransactionState.Unknown);
			}

			// Token: 0x06003529 RID: 13609 RVA: 0x00144078 File Offset: 0x00143478
			internal override void TransactionEnlisted(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionEnlisted|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionStarted(transactionId, true);
			}

			// Token: 0x0600352A RID: 13610 RVA: 0x001440B0 File Offset: 0x001434B0
			internal override void TransactionEnded(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionEnded|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEndedByServer(transactionId, TransactionState.Unknown);
			}

			// Token: 0x0600352B RID: 13611 RVA: 0x001440E8 File Offset: 0x001434E8
			internal override void TransactionRolledBack(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionRolledBack|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEndedByServer(transactionId, TransactionState.Aborted);
			}

			// Token: 0x0600352C RID: 13612 RVA: 0x00144120 File Offset: 0x00143520
			internal override void TransactionStarted(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionStarted|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionStarted(transactionId, false);
			}

			// Token: 0x040020D7 RID: 8407
			private SqlInternalConnectionSmi _connection;
		}
	}
}
