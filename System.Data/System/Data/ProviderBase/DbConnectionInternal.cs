using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020001DC RID: 476
	internal abstract class DbConnectionInternal
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0025E388 File Offset: 0x0025D788
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0025E3A8 File Offset: 0x0025D7A8
		internal DbConnectionInternal NextPooledObject
		{
			get
			{
				return this._nextPooledObject;
			}
			set
			{
				this._nextPooledObject = value;
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0025E3C8 File Offset: 0x0025D7C8
		protected DbConnectionInternal() : this(ConnectionState.Open, true, false)
		{
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0025E3E8 File Offset: 0x0025D7E8
		internal DbConnectionInternal(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
		{
			this._allowSetConnectionString = allowSetConnectionString;
			this._hidePassword = hidePassword;
			this._state = state;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0025E438 File Offset: 0x0025D838
		internal bool AllowSetConnectionString
		{
			get
			{
				return this._allowSetConnectionString;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0025E458 File Offset: 0x0025D858
		internal bool CanBePooled
		{
			get
			{
				return !this._connectionIsDoomed && !this._cannotBePooled && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0025E488 File Offset: 0x0025D888
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0025E4A8 File Offset: 0x0025D8A8
		protected internal Transaction EnlistedTransaction
		{
			get
			{
				return this._enlistedTransaction;
			}
			set
			{
				if ((null == this._enlistedTransaction && null != value) || (null != this._enlistedTransaction && !this._enlistedTransaction.Equals(value)))
				{
					Transaction transaction = null;
					Transaction transaction2 = null;
					try
					{
						if (null != value)
						{
							transaction = value.Clone();
						}
						lock (this)
						{
							transaction2 = this._enlistedTransaction;
							this._enlistedTransaction = transaction;
							value = transaction;
							transaction = null;
						}
					}
					finally
					{
						if (null != transaction2)
						{
							transaction2.Dispose();
						}
						if (null != transaction)
						{
							transaction.Dispose();
						}
					}
					if (null != value)
					{
						if (Bid.IsOn(Bid.ApiGroup.Pooling))
						{
							int hashCode = value.GetHashCode();
							Bid.PoolerTrace("<prov.DbConnectionInternal.set_EnlistedTransaction|RES|CPOOL> %d#, Transaction %d#, Enlisting.\n", this.ObjectID, hashCode);
						}
						this.TransactionOutcomeEnlist(value);
					}
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0025E5B8 File Offset: 0x0025D9B8
		internal bool IsTxRootWaitingForTxEnd
		{
			get
			{
				return this._isInStasis;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0025E5D8 File Offset: 0x0025D9D8
		internal virtual bool RequireExplicitTransactionUnbind
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0025E5E8 File Offset: 0x0025D9E8
		protected internal virtual bool IsNonPoolableTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0025E5F8 File Offset: 0x0025D9F8
		internal virtual bool IsTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0025E608 File Offset: 0x0025DA08
		public bool HasEnlistedTransaction
		{
			get
			{
				return null != this.EnlistedTransaction;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0025E628 File Offset: 0x0025DA28
		protected internal bool IsConnectionDoomed
		{
			get
			{
				return this._connectionIsDoomed;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0025E648 File Offset: 0x0025DA48
		internal bool IsEmancipated
		{
			get
			{
				return !this.IsTxRootWaitingForTxEnd && this._pooledCount < 1 && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0025E688 File Offset: 0x0025DA88
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0025E6A8 File Offset: 0x0025DAA8
		protected internal object Owner
		{
			get
			{
				return this._owningObject.Target;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0025E6C8 File Offset: 0x0025DAC8
		internal DbConnectionPool Pool
		{
			get
			{
				return this._connectionPool;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0025E6E8 File Offset: 0x0025DAE8
		protected DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._performanceCounters;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0025E708 File Offset: 0x0025DB08
		protected virtual bool ReadyToPrepareTransaction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0025E718 File Offset: 0x0025DB18
		protected internal DbReferenceCollection ReferenceCollection
		{
			get
			{
				return this._referenceCollection;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001A7F RID: 6783
		public abstract string ServerVersion { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0025E738 File Offset: 0x0025DB38
		public virtual string ServerVersionNormalized
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0025E758 File Offset: 0x0025DB58
		public bool ShouldHidePassword
		{
			get
			{
				return this._hidePassword;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0025E778 File Offset: 0x0025DB78
		public ConnectionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x06001A83 RID: 6787
		protected abstract void Activate(Transaction transaction);

		// Token: 0x06001A84 RID: 6788 RVA: 0x0025E798 File Offset: 0x0025DB98
		internal void ActivateConnection(Transaction transaction)
		{
			Bid.PoolerTrace("<prov.DbConnectionInternal.ActivateConnection|RES|INFO|CPOOL> %d#, Activating\n", this.ObjectID);
			this.Activate(transaction);
			this.PerformanceCounters.NumberOfActiveConnections.Increment();
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0025E7D8 File Offset: 0x0025DBD8
		internal void AddWeakReference(object value, int tag)
		{
			if (this._referenceCollection == null)
			{
				this._referenceCollection = this.CreateReferenceCollection();
				if (this._referenceCollection == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateReferenceCollectionReturnedNull);
				}
			}
			this._referenceCollection.Add(value, tag);
		}

		// Token: 0x06001A86 RID: 6790
		public abstract DbTransaction BeginTransaction(IsolationLevel il);

		// Token: 0x06001A87 RID: 6791 RVA: 0x0025E818 File Offset: 0x0025DC18
		public virtual void ChangeDatabase(string value)
		{
			throw ADP.MethodNotImplemented("ChangeDatabase");
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0025E838 File Offset: 0x0025DC38
		internal virtual void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			Bid.PoolerTrace("<prov.DbConnectionInternal.CloseConnection|RES|CPOOL> %d# Closing.\n", this.ObjectID);
			if (connectionFactory.SetInnerConnectionFrom(owningObject, DbConnectionOpenBusy.SingletonInstance, this))
			{
				try
				{
					DbConnectionPool pool = this.Pool;
					Transaction enlistedTransaction = this.EnlistedTransaction;
					if (null != enlistedTransaction && enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
					{
						this.DetachTransaction(enlistedTransaction);
					}
					if (pool != null)
					{
						pool.PutObject(this, owningObject);
					}
					else
					{
						this.Deactivate();
						this.PerformanceCounters.HardDisconnectsPerSecond.Increment();
						this._owningObject.Target = null;
						if (this.IsTransactionRoot)
						{
							this.SetInStasis();
						}
						else
						{
							this.PerformanceCounters.NumberOfNonPooledConnections.Decrement();
							if (base.GetType() != typeof(SqlInternalConnectionSmi))
							{
								this.Dispose();
							}
						}
					}
				}
				finally
				{
					connectionFactory.SetInnerConnectionEvent(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
				}
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0025E928 File Offset: 0x0025DD28
		protected virtual DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToConstructReferenceCollectionOnStaticObject);
		}

		// Token: 0x06001A8A RID: 6794
		protected abstract void Deactivate();

		// Token: 0x06001A8B RID: 6795 RVA: 0x0025E948 File Offset: 0x0025DD48
		internal void DeactivateConnection()
		{
			Bid.PoolerTrace("<prov.DbConnectionInternal.DeactivateConnection|RES|INFO|CPOOL> %d#, Deactivating\n", this.ObjectID);
			this.PerformanceCounters.NumberOfActiveConnections.Decrement();
			if (!this._connectionIsDoomed && this.Pool.UseLoadBalancing && DateTime.UtcNow.Ticks - this._createTime.Ticks > this.Pool.LoadBalanceTimeout.Ticks)
			{
				this.DoNotPoolThisConnection();
			}
			this.Deactivate();
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0025E9C8 File Offset: 0x0025DDC8
		internal virtual void DelegatedTransactionEnded()
		{
			Bid.Trace("<prov.DbConnectionInternal.DelegatedTransactionEnded|RES|CPOOL> %d#, Delegated Transaction Completed.\n", this.ObjectID);
			if (1 != this._pooledCount)
			{
				if (-1 == this._pooledCount && !this._owningObject.IsAlive)
				{
					this.TerminateStasis(false);
					this.Deactivate();
					this.PerformanceCounters.NumberOfNonPooledConnections.Decrement();
					this.Dispose();
				}
				return;
			}
			this.TerminateStasis(true);
			this.Deactivate();
			DbConnectionPool pool = this.Pool;
			if (pool == null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectWithoutPool);
			}
			pool.PutObjectFromTransactedPool(this);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0025EA58 File Offset: 0x0025DE58
		public virtual void Dispose()
		{
			this._connectionPool = null;
			this._performanceCounters = null;
			this._connectionIsDoomed = true;
			this._enlistedTransaction = null;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0025EA88 File Offset: 0x0025DE88
		protected internal void DoNotPoolThisConnection()
		{
			this._cannotBePooled = true;
			Bid.PoolerTrace("<prov.DbConnectionInternal.DoNotPoolThisConnection|RES|INFO|CPOOL> %d#, Marking pooled object as non-poolable so it will be disposed\n", this.ObjectID);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0025EAB8 File Offset: 0x0025DEB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected internal void DoomThisConnection()
		{
			this._connectionIsDoomed = true;
			Bid.PoolerTrace("<prov.DbConnectionInternal.DoomThisConnection|RES|INFO|CPOOL> %d#, Dooming\n", this.ObjectID);
		}

		// Token: 0x06001A90 RID: 6800
		public abstract void EnlistTransaction(Transaction transaction);

		// Token: 0x06001A91 RID: 6801 RVA: 0x0025EAE8 File Offset: 0x0025DEE8
		protected internal virtual DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			DbMetaDataFactory metaDataFactory = factory.GetMetaDataFactory(poolGroup, this);
			return metaDataFactory.GetSchema(outerConnection, collectionName, restrictions);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0025EB18 File Offset: 0x0025DF18
		internal void MakeNonPooledObject(object owningObject, DbConnectionPoolCounters performanceCounters)
		{
			this._connectionPool = null;
			this._performanceCounters = performanceCounters;
			this._owningObject.Target = owningObject;
			this._pooledCount = -1;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0025EB48 File Offset: 0x0025DF48
		internal void MakePooledConnection(DbConnectionPool connectionPool)
		{
			this._createTime = DateTime.UtcNow;
			this._connectionPool = connectionPool;
			this._performanceCounters = connectionPool.PerformanceCounters;
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0025EB78 File Offset: 0x0025DF78
		internal void NotifyWeakReference(int message)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Notify(message);
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0025EB98 File Offset: 0x0025DF98
		internal virtual void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			throw ADP.ConnectionAlreadyOpen(this.State);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0025EBB8 File Offset: 0x0025DFB8
		internal void PrePush(object expectedOwner)
		{
			if (expectedOwner == null)
			{
				if (this._owningObject.Target != null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasOwner);
				}
			}
			else if (this._owningObject.Target != expectedOwner)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
			if (this._pooledCount != 0)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PushingObjectSecondTime);
			}
			if (Bid.IsOn(Bid.ApiGroup.Pooling))
			{
				Bid.PoolerTrace("<prov.DbConnectionInternal.PrePush|RES|CPOOL> %d#, Preparing to push into pool, owning connection %d#, pooledCount=%d\n", this.ObjectID, 0, this._pooledCount);
			}
			this._pooledCount++;
			this._owningObject.Target = null;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0025EC48 File Offset: 0x0025E048
		internal void PostPop(object newOwner)
		{
			if (this._owningObject.Target != null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectHasOwner);
			}
			this._owningObject.Target = newOwner;
			this._pooledCount--;
			if (Bid.IsOn(Bid.ApiGroup.Pooling))
			{
				Bid.PoolerTrace("<prov.DbConnectionInternal.PostPop|RES|CPOOL> %d#, Preparing to pop from pool,  owning connection %d#, pooledCount=%d\n", this.ObjectID, 0, this._pooledCount);
			}
			if (this.Pool != null)
			{
				if (this._pooledCount != 0)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectInPoolMoreThanOnce);
				}
			}
			else if (-1 != this._pooledCount)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.NonPooledObjectUsedMoreThanOnce);
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0025ECD8 File Offset: 0x0025E0D8
		internal void RemoveWeakReference(object value)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Remove(value);
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0025ECF8 File Offset: 0x0025E0F8
		protected virtual void CleanupTransactionOnCompletion(Transaction transaction)
		{
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0025ED08 File Offset: 0x0025E108
		internal void DetachTransaction(Transaction transaction)
		{
			Bid.Trace("<prov.DbConnectionInternal.DetachTransaction|RES|CPOOL> %d#, Transaction Completed. (pooledCount=%d)\n", this.ObjectID, this._pooledCount);
			lock (this)
			{
				DbConnection dbConnection = (DbConnection)this.Owner;
				if ((!this.RequireExplicitTransactionUnbind || dbConnection == null) && this._enlistedTransaction != null && transaction.Equals(this._enlistedTransaction))
				{
					this.EnlistedTransaction = null;
					if (this.IsTxRootWaitingForTxEnd)
					{
						this.DelegatedTransactionEnded();
					}
				}
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0025EDA8 File Offset: 0x0025E1A8
		internal void CleanupConnectionOnTransactionCompletion(Transaction transaction)
		{
			this.DetachTransaction(transaction);
			DbConnectionPool pool = this.Pool;
			if (pool != null)
			{
				pool.TransactionEnded(transaction, this);
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0025EDD8 File Offset: 0x0025E1D8
		private void TransactionCompletedEvent(object sender, TransactionEventArgs e)
		{
			Transaction transaction = e.Transaction;
			Bid.Trace("<prov.DbConnectionInternal.TransactionCompletedEvent|RES|CPOOL> %d#, Transaction Completed. (pooledCount=%d)\n", this.ObjectID, this._pooledCount);
			this.CleanupTransactionOnCompletion(transaction);
			this.CleanupConnectionOnTransactionCompletion(transaction);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0025EE18 File Offset: 0x0025E218
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private void TransactionOutcomeEnlist(Transaction transaction)
		{
			transaction.TransactionCompleted += this.TransactionCompletedEvent;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0025EE38 File Offset: 0x0025E238
		internal void SetInStasis()
		{
			this._isInStasis = true;
			Bid.PoolerTrace("<prov.DbConnectionInternal.SetInStasis|RES|CPOOL> %d#, Non-Pooled Connection has Delegated Transaction, waiting to Dispose.\n", this.ObjectID);
			this.PerformanceCounters.NumberOfStasisConnections.Increment();
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0025EE78 File Offset: 0x0025E278
		private void TerminateStasis(bool returningToPool)
		{
			if (returningToPool)
			{
				Bid.PoolerTrace("<prov.DbConnectionInternal.TerminateStasis|RES|CPOOL> %d#, Delegated Transaction has ended, connection is closed.  Returning to general pool.\n", this.ObjectID);
			}
			else
			{
				Bid.PoolerTrace("<prov.DbConnectionInternal.TerminateStasis|RES|CPOOL> %d#, Delegated Transaction has ended, connection is closed/leaked.  Disposing.\n", this.ObjectID);
			}
			this.PerformanceCounters.NumberOfStasisConnections.Decrement();
			this._isInStasis = false;
		}

		// Token: 0x04000FB3 RID: 4019
		private static int _objectTypeCount;

		// Token: 0x04000FB4 RID: 4020
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionInternal._objectTypeCount);

		// Token: 0x04000FB5 RID: 4021
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x04000FB6 RID: 4022
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x04000FB7 RID: 4023
		private readonly bool _allowSetConnectionString;

		// Token: 0x04000FB8 RID: 4024
		private readonly bool _hidePassword;

		// Token: 0x04000FB9 RID: 4025
		private readonly ConnectionState _state;

		// Token: 0x04000FBA RID: 4026
		private readonly WeakReference _owningObject = new WeakReference(null, false);

		// Token: 0x04000FBB RID: 4027
		private DbConnectionInternal _nextPooledObject;

		// Token: 0x04000FBC RID: 4028
		private DbConnectionPool _connectionPool;

		// Token: 0x04000FBD RID: 4029
		private DbConnectionPoolCounters _performanceCounters;

		// Token: 0x04000FBE RID: 4030
		private DbReferenceCollection _referenceCollection;

		// Token: 0x04000FBF RID: 4031
		private int _pooledCount;

		// Token: 0x04000FC0 RID: 4032
		private bool _connectionIsDoomed;

		// Token: 0x04000FC1 RID: 4033
		private bool _cannotBePooled;

		// Token: 0x04000FC2 RID: 4034
		private bool _isInStasis;

		// Token: 0x04000FC3 RID: 4035
		private DateTime _createTime;

		// Token: 0x04000FC4 RID: 4036
		private Transaction _enlistedTransaction;
	}
}
