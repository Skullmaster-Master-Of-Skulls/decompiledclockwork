using System;
using System.Data.Common;
using System.Threading;
using System.Transactions;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020002F9 RID: 761
	internal sealed class SqlInternalConnectionSmi : SqlInternalConnection
	{
		// Token: 0x06002779 RID: 10105 RVA: 0x002AB0D8 File Offset: 0x002AA4D8
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

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x002AB148 File Offset: 0x002AA548
		internal SmiContext InternalContext
		{
			get
			{
				return this._smiContext;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x002AB168 File Offset: 0x002AA568
		internal SmiConnection SmiConnection
		{
			get
			{
				return this._smiConnection;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x002AB188 File Offset: 0x002AA588
		internal SmiEventSink CurrentEventSink
		{
			get
			{
				return this._smiEventSink;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x002AB1A8 File Offset: 0x002AA5A8
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._currentTransaction;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x002AB1C8 File Offset: 0x002AA5C8
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x002AB1D8 File Offset: 0x002AA5D8
		internal override bool IsShiloh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x002AB1E8 File Offset: 0x002AA5E8
		internal override bool IsYukonOrNewer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x002AB1F8 File Offset: 0x002AA5F8
		internal override bool IsKatmaiOrNewer
		{
			get
			{
				return SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x002AB228 File Offset: 0x002AA628
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x002AB248 File Offset: 0x002AA648
		public override string ServerVersion
		{
			get
			{
				return SmiContextFactory.Instance.ServerVersion;
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x002AB268 File Offset: 0x002AA668
		protected override void Activate(Transaction transaction)
		{
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x002AB278 File Offset: 0x002AA678
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

		// Token: 0x06002786 RID: 10118 RVA: 0x002AB2C8 File Offset: 0x002AA6C8
		internal override void AddPreparedCommand(SqlCommand cmd)
		{
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x002AB2D8 File Offset: 0x002AA6D8
		internal void AutomaticEnlistment()
		{
			Transaction currentTransaction = ADP.GetCurrentTransaction();
			Transaction contextTransaction = this._smiContext.ContextTransaction;
			long contextTransactionId = this._smiContext.ContextTransactionId;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> %d#, contextTransactionId=0x%I64x, contextTransaction=%d#, currentSystemTransaction=%d#.\n", base.ObjectID, contextTransactionId, (null != contextTransaction) ? contextTransaction.GetHashCode() : 0, (null != currentTransaction) ? currentTransaction.GetHashCode() : 0);
			}
			if (0L == contextTransactionId)
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
			base.ContextTransaction = contextTransaction;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x002AB3D8 File Offset: 0x002AA7D8
		internal override void ClearPreparedCommands()
		{
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x002AB3E8 File Offset: 0x002AA7E8
		protected override void ChangeDatabaseInternal(string database)
		{
			this._smiConnection.SetCurrentDatabase(database, this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x002AB418 File Offset: 0x002AA818
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
			base.ContextTransaction = null;
			this._isInUse = 0;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x002AB498 File Offset: 0x002AA898
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.DelegatedTransactionEnded|ADV> %d#, cleaning up after Delegated Transaction Completion\n", base.ObjectID);
			}
			this._currentTransaction = null;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x002AB4D8 File Offset: 0x002AA8D8
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

		// Token: 0x0600278D RID: 10125 RVA: 0x002AB528 File Offset: 0x002AA928
		public override void Dispose()
		{
			this._smiContext.OutOfScope -= this.OnOutOfScope;
			base.Dispose();
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x002AB558 File Offset: 0x002AA958
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlInternalConnectionSmi.ExecuteTransaction|ADV> %d#, transactionRequest=%s, transactionName='%ls', isolationLevel=%s, internalTransaction=#%d transactionId=0x%I64x.\n", base.ObjectID, transactionRequest.ToString(), (transactionName != null) ? transactionName : "null", iso.ToString(), (internalTransaction != null) ? internalTransaction.ObjectID : 0, (internalTransaction != null) ? internalTransaction.TransactionId : 0L);
			}
			switch (transactionRequest)
			{
			case SqlInternalConnection.TransactionRequest.Begin:
				try
				{
					this._pendingTransaction = internalTransaction;
					this._smiConnection.BeginTransaction(transactionName, iso, this._smiEventSink);
					goto IL_121;
				}
				finally
				{
					this._pendingTransaction = null;
				}
				break;
			case SqlInternalConnection.TransactionRequest.Promote:
				base.PromotedDTCToken = this._smiConnection.PromoteTransaction(this._currentTransaction.TransactionId, this._smiEventSink);
				goto IL_121;
			case SqlInternalConnection.TransactionRequest.Commit:
				break;
			case SqlInternalConnection.TransactionRequest.Rollback:
			case SqlInternalConnection.TransactionRequest.IfRollback:
				this._smiConnection.RollbackTransaction(this._currentTransaction.TransactionId, transactionName, this._smiEventSink);
				goto IL_121;
			case SqlInternalConnection.TransactionRequest.Save:
				this._smiConnection.CreateTransactionSavePoint(this._currentTransaction.TransactionId, transactionName, this._smiEventSink);
				goto IL_121;
			default:
				goto IL_121;
			}
			this._smiConnection.CommitTransaction(this._currentTransaction.TransactionId, this._smiEventSink);
			IL_121:
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x002AB6B8 File Offset: 0x002AAAB8
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

		// Token: 0x06002790 RID: 10128 RVA: 0x002AB708 File Offset: 0x002AAB08
		internal void GetCurrentTransactionPair(out long transactionId, out Transaction transaction)
		{
			lock (this)
			{
				transactionId = ((this.CurrentTransaction != null) ? this.CurrentTransaction.TransactionId : 0L);
				transaction = null;
				if (0L != transactionId)
				{
					transaction = base.InternalEnlistedTransaction;
				}
			}
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x002AB778 File Offset: 0x002AAB78
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
				base.ContextTransaction = null;
				this._isInUse = 0;
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x002AB7F8 File Offset: 0x002AABF8
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

		// Token: 0x06002793 RID: 10131 RVA: 0x002AB848 File Offset: 0x002AAC48
		private void TransactionEndedByServer(long transactionId)
		{
			SqlDelegatedTransaction delegatedTransaction = base.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.Transaction.Rollback();
				base.DelegatedTransaction = null;
			}
			this.TransactionEnded(transactionId, TransactionState.Unknown);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x002AB888 File Offset: 0x002AAC88
		private void TransactionEnded(long transactionId, TransactionState transactionState)
		{
			if (this._currentTransaction != null)
			{
				this._currentTransaction.Completed(transactionState);
				this._currentTransaction = null;
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x002AB8B8 File Offset: 0x002AACB8
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

		// Token: 0x06002796 RID: 10134 RVA: 0x002AB918 File Offset: 0x002AAD18
		internal override void RemovePreparedCommand(SqlCommand cmd)
		{
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x002AB928 File Offset: 0x002AAD28
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			SqlDataReader sqlDataReader = base.FindLiveReader(null);
			if (sqlDataReader != null)
			{
				throw ADP.OpenReaderExists();
			}
		}

		// Token: 0x04001904 RID: 6404
		private SmiContext _smiContext;

		// Token: 0x04001905 RID: 6405
		private SmiConnection _smiConnection;

		// Token: 0x04001906 RID: 6406
		private SmiEventSink_Default _smiEventSink;

		// Token: 0x04001907 RID: 6407
		private int _isInUse;

		// Token: 0x04001908 RID: 6408
		private SqlInternalTransaction _pendingTransaction;

		// Token: 0x04001909 RID: 6409
		private SqlInternalTransaction _currentTransaction;

		// Token: 0x020002FA RID: 762
		private sealed class EventSink : SmiEventSink_Default
		{
			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x06002798 RID: 10136 RVA: 0x002AB948 File Offset: 0x002AAD48
			internal override string ServerVersion
			{
				get
				{
					return SmiContextFactory.Instance.ServerVersion;
				}
			}

			// Token: 0x06002799 RID: 10137 RVA: 0x002AB968 File Offset: 0x002AAD68
			protected override void DispatchMessages(bool ignoreNonFatalMessages)
			{
				SqlException ex = base.ProcessMessages(false, ignoreNonFatalMessages);
				if (ex != null)
				{
					if (this._connection.Connection != null && this._connection.Connection.FireInfoMessageEventOnUserErrors)
					{
						this._connection.Connection.OnInfoMessage(new SqlInfoMessageEventArgs(ex));
						return;
					}
					this._connection.OnError(ex, false);
				}
			}

			// Token: 0x0600279A RID: 10138 RVA: 0x002AB9C8 File Offset: 0x002AADC8
			internal EventSink(SqlInternalConnectionSmi connection)
			{
				this._connection = connection;
			}

			// Token: 0x0600279B RID: 10139 RVA: 0x002AB9E8 File Offset: 0x002AADE8
			internal override void DefaultDatabaseChanged(string databaseName)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.DefaultDatabaseChanged|ADV> %d#, databaseName='%ls'.\n", this._connection.ObjectID, databaseName);
				}
				this._connection.CurrentDatabase = databaseName;
			}

			// Token: 0x0600279C RID: 10140 RVA: 0x002ABA28 File Offset: 0x002AAE28
			internal override void TransactionCommitted(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionCommitted|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEnded(transactionId, TransactionState.Committed);
			}

			// Token: 0x0600279D RID: 10141 RVA: 0x002ABA68 File Offset: 0x002AAE68
			internal override void TransactionDefected(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionDefected|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEnded(transactionId, TransactionState.Unknown);
			}

			// Token: 0x0600279E RID: 10142 RVA: 0x002ABAA8 File Offset: 0x002AAEA8
			internal override void TransactionEnlisted(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionEnlisted|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionStarted(transactionId, true);
			}

			// Token: 0x0600279F RID: 10143 RVA: 0x002ABAE8 File Offset: 0x002AAEE8
			internal override void TransactionEnded(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionEnded|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEndedByServer(transactionId);
			}

			// Token: 0x060027A0 RID: 10144 RVA: 0x002ABB28 File Offset: 0x002AAF28
			internal override void TransactionRolledBack(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionRolledBack|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionEnded(transactionId, TransactionState.Aborted);
			}

			// Token: 0x060027A1 RID: 10145 RVA: 0x002ABB68 File Offset: 0x002AAF68
			internal override void TransactionStarted(long transactionId)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlInternalConnectionSmi.EventSink.TransactionStarted|ADV> %d#, transactionId=0x%I64x.\n", this._connection.ObjectID, transactionId);
				}
				this._connection.TransactionStarted(transactionId, false);
			}

			// Token: 0x0400190A RID: 6410
			private SqlInternalConnectionSmi _connection;
		}
	}
}
