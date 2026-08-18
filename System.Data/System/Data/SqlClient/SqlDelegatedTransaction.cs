using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020002E3 RID: 739
	internal sealed class SqlDelegatedTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x002A31B8 File Offset: 0x002A25B8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x002A31D8 File Offset: 0x002A25D8
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

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x002A3278 File Offset: 0x002A2678
		internal Transaction Transaction
		{
			get
			{
				return this._atomicTransaction;
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x002A3298 File Offset: 0x002A2698
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

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x002A33A8 File Offset: 0x002A27A8
		internal bool IsActive
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x002A33C8 File Offset: 0x002A27C8
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
				lock (validConnection)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Promote, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						result = this._connection.PromotedDTCToken;
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

		// Token: 0x06002680 RID: 9856 RVA: 0x002A3538 File Offset: 0x002A2938
		public void Rollback(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			Bid.Trace("<sc.SqlDelegatedTransaction.Rollback|RES|CPOOL> %d#, Connection %d#, aborting transaction.\n", this.ObjectID, validConnection.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				lock (validConnection)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						this._active = false;
						this._connection = null;
						validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, null, IsolationLevel.Unspecified, this._internalTransaction, true);
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

		// Token: 0x06002681 RID: 9857 RVA: 0x002A3698 File Offset: 0x002A2A98
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
					lock (validConnection)
					{
						this._active = false;
						this._connection = null;
					}
					enlistment.Aborted(SQL.ConnectionDoomed());
				}
				else
				{
					Exception ex;
					lock (validConnection)
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

		// Token: 0x06002682 RID: 9858 RVA: 0x002A3898 File Offset: 0x002A2C98
		internal void TransactionEnded(Transaction transaction)
		{
			SqlInternalConnection connection = this._connection;
			if (connection != null)
			{
				Bid.Trace("<sc.SqlDelegatedTransaction.TransactionEnded|RES|CPOOL> %d#, Connection %d#, transaction completed externally.\n", this.ObjectID, connection.ObjectID);
				lock (connection)
				{
					if (this._atomicTransaction.Equals(transaction))
					{
						this._active = false;
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x002A3918 File Offset: 0x002A2D18
		private SqlInternalConnection GetValidConnection()
		{
			SqlInternalConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ObjectDisposed(this);
			}
			return connection;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x002A3938 File Offset: 0x002A2D38
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

		// Token: 0x0400184E RID: 6222
		private static int _objectTypeCount;

		// Token: 0x0400184F RID: 6223
		private readonly int _objectID = Interlocked.Increment(ref SqlDelegatedTransaction._objectTypeCount);

		// Token: 0x04001850 RID: 6224
		private SqlInternalConnection _connection;

		// Token: 0x04001851 RID: 6225
		private IsolationLevel _isolationLevel;

		// Token: 0x04001852 RID: 6226
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04001853 RID: 6227
		private Transaction _atomicTransaction;

		// Token: 0x04001854 RID: 6228
		private bool _active;
	}
}
