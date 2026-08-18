using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001C7 RID: 455
	internal sealed class SqlDelegatedTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x000C9CEC File Offset: 0x000C90EC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x000C9D00 File Offset: 0x000C9100
		internal SqlDelegatedTransaction(SqlInternalConnection connection, Transaction tx)
		{
			this._connection = connection;
			this._atomicTransaction = tx;
			this._active = false;
			IsolationLevel isolationLevel = tx.IsolationLevel;
			switch (isolationLevel)
			{
			case IsolationLevel.Serializable:
				this._isolationLevel = IsolationLevel.Serializable;
				return;
			case IsolationLevel.RepeatableRead:
				this._isolationLevel = IsolationLevel.RepeatableRead;
				return;
			case IsolationLevel.ReadCommitted:
				this._isolationLevel = IsolationLevel.ReadCommitted;
				return;
			case IsolationLevel.ReadUncommitted:
				this._isolationLevel = IsolationLevel.ReadUncommitted;
				return;
			case IsolationLevel.Snapshot:
				this._isolationLevel = IsolationLevel.Snapshot;
				return;
			default:
				throw SQL.UnknownSysTxIsolationLevel(isolationLevel);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000C9DA0 File Offset: 0x000C91A0
		internal Transaction Transaction
		{
			get
			{
				return this._atomicTransaction;
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000C9DB4 File Offset: 0x000C91B4
		public void Initialize()
		{
			SqlInternalConnection connection = this._connection;
			SqlConnection connection2 = connection.Connection;
			Bid.Trace("<sc.SqlDelegatedTransaction.Initialize|RES|CPOOL> %d#, Connection %d#, delegating transaction.\n", this.ObjectID, connection.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (connection.IsEnlistedInTransaction)
				{
					Bid.Trace("<sc.SqlDelegatedTransaction.Initialize|RES|CPOOL> %d#, Connection %d#, was enlisted, now defecting.\n", this.ObjectID, connection.ObjectID);
					connection.EnlistNull();
				}
				this._internalTransaction = new SqlInternalTransaction(connection, TransactionType.Delegated, null);
				connection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, null, this._isolationLevel, this._internalTransaction, true);
				if (connection.CurrentTransaction == null)
				{
					connection.DoomThisConnection();
					throw ADP.InternalError(ADP.InternalErrorCode.UnknownTransactionFailure);
				}
				this._active = true;
			}
			catch (OutOfMemoryException e)
			{
				connection2.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection2.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection2.Abort(e3);
				throw;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x000C9EC4 File Offset: 0x000C92C4
		internal bool IsActive
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000C9ED8 File Offset: 0x000C92D8
		public byte[] Promote()
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			byte[] result = null;
			SqlConnection connection = validConnection.Connection;
			Bid.Trace("<sc.SqlDelegatedTransaction.Promote|RES|CPOOL> %d#, Connection %d#, promoting transaction.\n", this.ObjectID, validConnection.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			Exception ex;
			try
			{
				SqlInternalConnection obj = validConnection;
				lock (obj)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Promote, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						result = this._connection.PromotedDTCToken;
						if (this._connection.IsGlobalTransaction)
						{
							if (SysTxForGlobalTransactions.SetDistributedTransactionIdentifier == null)
							{
								throw SQL.UnsupportedSysTxForGlobalTransactions();
							}
							if (!this._connection.IsGlobalTransactionsEnabledForServer)
							{
								throw SQL.GlobalTransactionsNotEnabled();
							}
							SysTxForGlobalTransactions.SetDistributedTransactionIdentifier.Invoke(this._atomicTransaction, new object[]
							{
								this,
								this.GetGlobalTxnIdentifierFromToken()
							});
						}
						ex = null;
					}
					catch (SqlException ex2)
					{
						ex = ex2;
						ADP.TraceExceptionWithoutRethrow(ex2);
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException ex3)
					{
						ex = ex3;
						ADP.TraceExceptionWithoutRethrow(ex3);
						validConnection.DoomThisConnection();
					}
				}
			}
			catch (OutOfMemoryException e)
			{
				connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection.Abort(e3);
				throw;
			}
			if (ex != null)
			{
				throw SQL.PromotionFailed(ex);
			}
			return result;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000CA0A4 File Offset: 0x000C94A4
		public void Rollback(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			Bid.Trace("<sc.SqlDelegatedTransaction.Rollback|RES|CPOOL> %d#, Connection %d#, aborting transaction.\n", this.ObjectID, validConnection.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SqlInternalConnection obj = validConnection;
				lock (obj)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						this._active = false;
						this._connection = null;
						if (!this._internalTransaction.IsAborted)
						{
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						}
					}
					catch (SqlException e)
					{
						ADP.TraceExceptionWithoutRethrow(e);
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException e2)
					{
						ADP.TraceExceptionWithoutRethrow(e2);
						validConnection.DoomThisConnection();
					}
				}
				validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
				enlistment.Aborted();
			}
			catch (OutOfMemoryException e3)
			{
				connection.Abort(e3);
				throw;
			}
			catch (StackOverflowException e4)
			{
				connection.Abort(e4);
				throw;
			}
			catch (ThreadAbortException e5)
			{
				connection.Abort(e5);
				throw;
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000CA21C File Offset: 0x000C961C
		public void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			Bid.Trace("<sc.SqlDelegatedTransaction.SinglePhaseCommit|RES|CPOOL> %d#, Connection %d#, committing transaction.\n", this.ObjectID, validConnection.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (validConnection.IsConnectionDoomed)
				{
					SqlInternalConnection obj = validConnection;
					lock (obj)
					{
						this._active = false;
						this._connection = null;
					}
					enlistment.Aborted(SQL.ConnectionDoomed());
				}
				else
				{
					SqlInternalConnection obj2 = validConnection;
					Exception ex;
					lock (obj2)
					{
						try
						{
							this.ValidateActiveOnConnection(validConnection);
							this._active = false;
							this._connection = null;
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, this._internalTransaction, true);
							ex = null;
						}
						catch (SqlException ex2)
						{
							ex = ex2;
							ADP.TraceExceptionWithoutRethrow(ex2);
							validConnection.DoomThisConnection();
						}
						catch (InvalidOperationException ex3)
						{
							ex = ex3;
							ADP.TraceExceptionWithoutRethrow(ex3);
							validConnection.DoomThisConnection();
						}
					}
					if (ex != null)
					{
						if (this._internalTransaction.IsCommitted)
						{
							enlistment.Committed();
						}
						else if (this._internalTransaction.IsAborted)
						{
							enlistment.Aborted(ex);
						}
						else
						{
							enlistment.InDoubt(ex);
						}
					}
					validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
					if (ex == null)
					{
						enlistment.Committed();
					}
				}
			}
			catch (OutOfMemoryException e)
			{
				connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				connection.Abort(e3);
				throw;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000CA424 File Offset: 0x000C9824
		internal void TransactionEnded(Transaction transaction)
		{
			SqlInternalConnection connection = this._connection;
			if (connection != null)
			{
				Bid.Trace("<sc.SqlDelegatedTransaction.TransactionEnded|RES|CPOOL> %d#, Connection %d#, transaction completed externally.\n", this.ObjectID, connection.ObjectID);
				SqlInternalConnection obj = connection;
				lock (obj)
				{
					if (this._atomicTransaction.Equals(transaction))
					{
						this._active = false;
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000CA4A4 File Offset: 0x000C98A4
		private SqlInternalConnection GetValidConnection()
		{
			SqlInternalConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ObjectDisposed(this);
			}
			return connection;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000CA4C4 File Offset: 0x000C98C4
		private void ValidateActiveOnConnection(SqlInternalConnection connection)
		{
			if (!this._active || connection != this._connection || connection.DelegatedTransaction != this)
			{
				if (connection != null)
				{
					connection.DoomThisConnection();
				}
				if (connection != this._connection && this._connection != null)
				{
					this._connection.DoomThisConnection();
				}
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000CA520 File Offset: 0x000C9920
		private Guid GetGlobalTxnIdentifierFromToken()
		{
			byte[] array = new byte[16];
			Array.Copy(this._connection.PromotedDTCToken, 4, array, 0, array.Length);
			return new Guid(array);
		}

		// Token: 0x04001046 RID: 4166
		private static int _objectTypeCount;

		// Token: 0x04001047 RID: 4167
		private readonly int _objectID = Interlocked.Increment(ref SqlDelegatedTransaction._objectTypeCount);

		// Token: 0x04001048 RID: 4168
		private const int _globalTransactionsTokenVersionSizeInBytes = 4;

		// Token: 0x04001049 RID: 4169
		private SqlInternalConnection _connection;

		// Token: 0x0400104A RID: 4170
		private IsolationLevel _isolationLevel;

		// Token: 0x0400104B RID: 4171
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x0400104C RID: 4172
		private Transaction _atomicTransaction;

		// Token: 0x0400104D RID: 4173
		private bool _active;
	}
}
