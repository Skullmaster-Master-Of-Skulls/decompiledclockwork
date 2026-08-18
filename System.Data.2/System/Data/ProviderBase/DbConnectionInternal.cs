using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C0 RID: 704
	internal abstract class DbConnectionInternal
	{
		// Token: 0x06002A73 RID: 10867 RVA: 0x00117BE4 File Offset: 0x00116FE4
		protected DbConnectionInternal() : this(ConnectionState.Open, true, false)
		{
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00117BFC File Offset: 0x00116FFC
		internal DbConnectionInternal(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
		{
			this._allowSetConnectionString = allowSetConnectionString;
			this._hidePassword = hidePassword;
			this._state = state;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x00117C44 File Offset: 0x00117044
		internal bool AllowSetConnectionString
		{
			get
			{
				return this._allowSetConnectionString;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x00117C58 File Offset: 0x00117058
		internal bool CanBePooled
		{
			get
			{
				return !this._connectionIsDoomed && !this._cannotBePooled && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x00117C88 File Offset: 0x00117088
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x00117C9C File Offset: 0x0011709C
		protected internal Transaction EnlistedTransaction
		{
			get
			{
				return this._enlistedTransaction;
			}
			set
			{
				Transaction enlistedTransaction = this._enlistedTransaction;
				if ((null == enlistedTransaction && null != value) || (null != enlistedTransaction && !enlistedTransaction.Equals(value)))
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
							transaction2 = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, transaction);
							this._enlistedTransactionOriginal = value;
							value = transaction;
							transaction = null;
						}
					}
					finally
					{
						if (null != transaction2 && transaction2 != this._enlistedTransaction)
						{
							transaction2.Dispose();
						}
						if (null != transaction && transaction != this._enlistedTransaction)
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

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x00117DC0 File Offset: 0x001171C0
		protected bool EnlistedTransactionDisposed
		{
			get
			{
				bool result;
				try
				{
					Transaction enlistedTransactionOriginal = this._enlistedTransactionOriginal;
					bool flag = enlistedTransactionOriginal != null && enlistedTransactionOriginal.TransactionInformation == null;
					result = flag;
				}
				catch (ObjectDisposedException)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x00117E14 File Offset: 0x00117214
		internal bool IsTxRootWaitingForTxEnd
		{
			get
			{
				return this._isInStasis;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x00117E28 File Offset: 0x00117228
		protected virtual bool UnbindOnTransactionCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x00117E38 File Offset: 0x00117238
		protected internal virtual bool IsNonPoolableTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x00117E48 File Offset: 0x00117248
		internal virtual bool IsTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x00117E58 File Offset: 0x00117258
		protected internal bool IsConnectionDoomed
		{
			get
			{
				return this._connectionIsDoomed;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x00117E6C File Offset: 0x0011726C
		internal bool IsEmancipated
		{
			get
			{
				return !this.IsTxRootWaitingForTxEnd && this._pooledCount < 1 && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x00117EA0 File Offset: 0x001172A0
		internal bool IsInPool
		{
			get
			{
				return this._pooledCount == 1;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x00117EB8 File Offset: 0x001172B8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x00117ECC File Offset: 0x001172CC
		protected internal object Owner
		{
			get
			{
				return this._owningObject.Target;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x00117EE4 File Offset: 0x001172E4
		internal DbConnectionPool Pool
		{
			get
			{
				return this._connectionPool;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x00117EF8 File Offset: 0x001172F8
		protected DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._performanceCounters;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x00117F0C File Offset: 0x0011730C
		protected virtual bool ReadyToPrepareTransaction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x00117F1C File Offset: 0x0011731C
		protected internal DbReferenceCollection ReferenceCollection
		{
			get
			{
				return this._referenceCollection;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002A87 RID: 10887
		public abstract string ServerVersion { get; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x00117F30 File Offset: 0x00117330
		public virtual string ServerVersionNormalized
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x00117F44 File Offset: 0x00117344
		public bool ShouldHidePassword
		{
			get
			{
				return this._hidePassword;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00117F58 File Offset: 0x00117358
		public ConnectionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x06002A8B RID: 10891
		protected abstract void Activate(Transaction transaction);

		// Token: 0x06002A8C RID: 10892 RVA: 0x00117F6C File Offset: 0x0011736C
		internal void ActivateConnection(Transaction transaction)
		{
			Bid.PoolerTrace("<prov.DbConnectionInternal.ActivateConnection|RES|INFO|CPOOL> %d#, Activating\n", this.ObjectID);
			this.Activate(transaction);
			this.PerformanceCounters.NumberOfActiveConnections.Increment();
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00117FA0 File Offset: 0x001173A0
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

		// Token: 0x06002A8E RID: 10894
		public abstract DbTransaction BeginTransaction(IsolationLevel il);

		// Token: 0x06002A8F RID: 10895 RVA: 0x00117FE0 File Offset: 0x001173E0
		public virtual void ChangeDatabase(string value)
		{
			throw ADP.MethodNotImplemented("ChangeDatabase");
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00117FF8 File Offset: 0x001173F8
		internal virtual void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			Bid.PoolerTrace("<prov.DbConnectionInternal.CloseConnection|RES|CPOOL> %d# Closing.\n", this.ObjectID);
			if (connectionFactory.SetInnerConnectionFrom(owningObject, DbConnectionOpenBusy.SingletonInstance, this))
			{
				lock (this)
				{
					object lockToken = this.ObtainAdditionalLocksForClose();
					try
					{
						this.PrepareForCloseConnection();
						DbConnectionPool pool = this.Pool;
						this.DetachCurrentTransactionIfEnded();
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
						this.ReleaseAdditionalLocksForClose(lockToken);
						connectionFactory.SetInnerConnectionEvent(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
					}
				}
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00118114 File Offset: 0x00117514
		internal virtual void PrepareForReplaceConnection()
		{
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00118124 File Offset: 0x00117524
		protected virtual void PrepareForCloseConnection()
		{
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00118134 File Offset: 0x00117534
		protected virtual object ObtainAdditionalLocksForClose()
		{
			return null;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00118144 File Offset: 0x00117544
		protected virtual void ReleaseAdditionalLocksForClose(object lockToken)
		{
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x00118154 File Offset: 0x00117554
		protected virtual DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToConstructReferenceCollectionOnStaticObject);
		}

		// Token: 0x06002A96 RID: 10902
		protected abstract void Deactivate();

		// Token: 0x06002A97 RID: 10903 RVA: 0x00118168 File Offset: 0x00117568
		internal void DeactivateConnection()
		{
			Bid.PoolerTrace("<prov.DbConnectionInternal.DeactivateConnection|RES|INFO|CPOOL> %d#, Deactivating\n", this.ObjectID);
			if (this.PerformanceCounters != null)
			{
				this.PerformanceCounters.NumberOfActiveConnections.Decrement();
			}
			if (!this._connectionIsDoomed && this.Pool.UseLoadBalancing && DateTime.UtcNow.Ticks - this._createTime.Ticks > this.Pool.LoadBalanceTimeout.Ticks)
			{
				this.DoNotPoolThisConnection();
			}
			this.Deactivate();
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x001181EC File Offset: 0x001175EC
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

		// Token: 0x06002A99 RID: 10905 RVA: 0x00118274 File Offset: 0x00117674
		public virtual void Dispose()
		{
			this._connectionPool = null;
			this._performanceCounters = null;
			this._connectionIsDoomed = true;
			this._enlistedTransactionOriginal = null;
			Transaction transaction = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, null);
			if (transaction != null)
			{
				transaction.Dispose();
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x001182BC File Offset: 0x001176BC
		protected internal void DoNotPoolThisConnection()
		{
			this._cannotBePooled = true;
			Bid.PoolerTrace("<prov.DbConnectionInternal.DoNotPoolThisConnection|RES|INFO|CPOOL> %d#, Marking pooled object as non-poolable so it will be disposed\n", this.ObjectID);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x001182E0 File Offset: 0x001176E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected internal void DoomThisConnection()
		{
			this._connectionIsDoomed = true;
			Bid.PoolerTrace("<prov.DbConnectionInternal.DoomThisConnection|RES|INFO|CPOOL> %d#, Dooming\n", this.ObjectID);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00118304 File Offset: 0x00117704
		protected internal void UnDoomThisConnection()
		{
			this._connectionIsDoomed = false;
		}

		// Token: 0x06002A9D RID: 10909
		public abstract void EnlistTransaction(Transaction transaction);

		// Token: 0x06002A9E RID: 10910 RVA: 0x00118318 File Offset: 0x00117718
		protected internal virtual DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			DbMetaDataFactory metaDataFactory = factory.GetMetaDataFactory(poolGroup, this);
			return metaDataFactory.GetSchema(outerConnection, collectionName, restrictions);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0011833C File Offset: 0x0011773C
		internal void MakeNonPooledObject(object owningObject, DbConnectionPoolCounters performanceCounters)
		{
			this._connectionPool = null;
			this._performanceCounters = performanceCounters;
			this._owningObject.Target = owningObject;
			this._pooledCount = -1;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0011836C File Offset: 0x0011776C
		internal void MakePooledConnection(DbConnectionPool connectionPool)
		{
			this._createTime = DateTime.UtcNow;
			this._connectionPool = connectionPool;
			this._performanceCounters = connectionPool.PerformanceCounters;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00118398 File Offset: 0x00117798
		internal void NotifyWeakReference(int message)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Notify(message);
			}
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x001183B8 File Offset: 0x001177B8
		internal virtual void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			if (!this.TryOpenConnection(outerConnection, connectionFactory, null, null))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x001183DC File Offset: 0x001177DC
		internal virtual bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(this.State);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x001183F4 File Offset: 0x001177F4
		internal virtual bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.MethodNotImplemented("TryReplaceConnection");
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0011840C File Offset: 0x0011780C
		protected bool TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (connectionFactory.SetInnerConnectionFrom(outerConnection, DbConnectionClosedConnecting.SingletonInstance, this))
			{
				DbConnectionInternal dbConnectionInternal = null;
				try
				{
					connectionFactory.PermissionDemand(outerConnection);
					if (!connectionFactory.TryGetConnection(outerConnection, retry, userOptions, this, out dbConnectionInternal))
					{
						return false;
					}
				}
				catch
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw;
				}
				if (dbConnectionInternal == null)
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
				}
				connectionFactory.SetInnerConnectionEvent(outerConnection, dbConnectionInternal);
				return true;
			}
			return true;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0011848C File Offset: 0x0011788C
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

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00118514 File Offset: 0x00117914
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

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0011859C File Offset: 0x0011799C
		internal void RemoveWeakReference(object value)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Remove(value);
			}
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x001185BC File Offset: 0x001179BC
		protected virtual void CleanupTransactionOnCompletion(Transaction transaction)
		{
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x001185CC File Offset: 0x001179CC
		internal void DetachCurrentTransactionIfEnded()
		{
			Transaction enlistedTransaction = this.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				bool flag;
				try
				{
					flag = (enlistedTransaction.TransactionInformation.Status > TransactionStatus.Active);
				}
				catch (TransactionException)
				{
					flag = true;
				}
				if (flag)
				{
					this.DetachTransaction(enlistedTransaction, true);
				}
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x00118628 File Offset: 0x00117A28
		internal void DetachTransaction(Transaction transaction, bool isExplicitlyReleasing)
		{
			Bid.Trace("<prov.DbConnectionInternal.DetachTransaction|RES|CPOOL> %d#, Transaction Completed. (pooledCount=%d)\n", this.ObjectID, this._pooledCount);
			lock (this)
			{
				DbConnection dbConnection = (DbConnection)this.Owner;
				if (isExplicitlyReleasing || this.UnbindOnTransactionCompletion || dbConnection == null)
				{
					Transaction enlistedTransaction = this._enlistedTransaction;
					if (enlistedTransaction != null && transaction.Equals(enlistedTransaction))
					{
						this.EnlistedTransaction = null;
						if (this.IsTxRootWaitingForTxEnd)
						{
							this.DelegatedTransactionEnded();
						}
					}
				}
			}
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x001186C8 File Offset: 0x00117AC8
		internal void CleanupConnectionOnTransactionCompletion(Transaction transaction)
		{
			this.DetachTransaction(transaction, false);
			DbConnectionPool pool = this.Pool;
			if (pool != null)
			{
				pool.TransactionEnded(transaction, this);
			}
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x001186F0 File Offset: 0x00117AF0
		private void TransactionCompletedEvent(object sender, TransactionEventArgs e)
		{
			Transaction transaction = e.Transaction;
			Bid.Trace("<prov.DbConnectionInternal.TransactionCompletedEvent|RES|CPOOL> %d#, Transaction Completed. (pooledCount=%d)\n", this.ObjectID, this._pooledCount);
			this.CleanupTransactionOnCompletion(transaction);
			this.CleanupConnectionOnTransactionCompletion(transaction);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00118728 File Offset: 0x00117B28
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private void TransactionOutcomeEnlist(Transaction transaction)
		{
			transaction.TransactionCompleted += this.TransactionCompletedEvent;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00118748 File Offset: 0x00117B48
		internal void SetInStasis()
		{
			this._isInStasis = true;
			Bid.PoolerTrace("<prov.DbConnectionInternal.SetInStasis|RES|CPOOL> %d#, Non-Pooled Connection has Delegated Transaction, waiting to Dispose.\n", this.ObjectID);
			this.PerformanceCounters.NumberOfStasisConnections.Increment();
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0011877C File Offset: 0x00117B7C
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

		// Token: 0x06002AB1 RID: 10929 RVA: 0x001187C8 File Offset: 0x00117BC8
		internal virtual bool IsConnectionAlive(bool throwOnException = false)
		{
			return true;
		}

		// Token: 0x04001B28 RID: 6952
		private static int _objectTypeCount;

		// Token: 0x04001B29 RID: 6953
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionInternal._objectTypeCount);

		// Token: 0x04001B2A RID: 6954
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x04001B2B RID: 6955
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x04001B2C RID: 6956
		private readonly bool _allowSetConnectionString;

		// Token: 0x04001B2D RID: 6957
		private readonly bool _hidePassword;

		// Token: 0x04001B2E RID: 6958
		private readonly ConnectionState _state;

		// Token: 0x04001B2F RID: 6959
		private readonly WeakReference _owningObject = new WeakReference(null, false);

		// Token: 0x04001B30 RID: 6960
		private DbConnectionPool _connectionPool;

		// Token: 0x04001B31 RID: 6961
		private DbConnectionPoolCounters _performanceCounters;

		// Token: 0x04001B32 RID: 6962
		private DbReferenceCollection _referenceCollection;

		// Token: 0x04001B33 RID: 6963
		private int _pooledCount;

		// Token: 0x04001B34 RID: 6964
		private bool _connectionIsDoomed;

		// Token: 0x04001B35 RID: 6965
		private bool _cannotBePooled;

		// Token: 0x04001B36 RID: 6966
		private bool _isInStasis;

		// Token: 0x04001B37 RID: 6967
		private DateTime _createTime;

		// Token: 0x04001B38 RID: 6968
		private Transaction _enlistedTransaction;

		// Token: 0x04001B39 RID: 6969
		private Transaction _enlistedTransactionOriginal;
	}
}
