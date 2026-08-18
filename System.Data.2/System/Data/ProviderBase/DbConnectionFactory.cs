using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002BF RID: 703
	internal abstract class DbConnectionFactory
	{
		// Token: 0x06002A4F RID: 10831 RVA: 0x00116FB8 File Offset: 0x001163B8
		protected DbConnectionFactory() : this(DbConnectionPoolCountersNoCounters.SingletonInstance)
		{
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00116FD0 File Offset: 0x001163D0
		protected DbConnectionFactory(DbConnectionPoolCounters performanceCounters)
		{
			this._performanceCounters = performanceCounters;
			this._connectionPoolGroups = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>();
			this._poolsToRelease = new List<DbConnectionPool>();
			this._poolGroupsToRelease = new List<DbConnectionPoolGroup>();
			this._pruningTimer = this.CreatePruningTimer();
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x00117028 File Offset: 0x00116428
		internal DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._performanceCounters;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002A52 RID: 10834
		public abstract DbProviderFactory ProviderFactory { get; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x0011703C File Offset: 0x0011643C
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00117050 File Offset: 0x00116450
		public void ClearAllPools()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionFactory.ClearAllPools|API> ");
			try
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					DbConnectionPoolGroup value = keyValuePair.Value;
					if (value != null)
					{
						value.Clear();
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x001170EC File Offset: 0x001164EC
		public void ClearPool(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionFactory.ClearPool|API> %d#", this.GetObjectId(connection));
			try
			{
				DbConnectionPoolGroup connectionPoolGroup = this.GetConnectionPoolGroup(connection);
				if (connectionPoolGroup != null)
				{
					connectionPoolGroup.Clear();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00117150 File Offset: 0x00116550
		public void ClearPool(DbConnectionPoolKey key)
		{
			ADP.CheckArgumentNull(key.ConnectionString, "key.ConnectionString");
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionFactory.ClearPool|API> connectionString");
			try
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				DbConnectionPoolGroup dbConnectionPoolGroup;
				if (connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
				{
					dbConnectionPoolGroup.Clear();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x001171B8 File Offset: 0x001165B8
		internal virtual DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x001171C8 File Offset: 0x001165C8
		protected virtual DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			throw ADP.NotSupported();
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x001171E0 File Offset: 0x001165E0
		internal DbConnectionInternal CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup, DbConnectionOptions userOptions)
		{
			DbConnectionOptions connectionOptions = poolGroup.ConnectionOptions;
			DbConnectionPoolGroupProviderInfo providerInfo = poolGroup.ProviderInfo;
			DbConnectionPoolKey poolKey = poolGroup.PoolKey;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(connectionOptions, poolKey, providerInfo, null, owningConnection, userOptions);
			if (dbConnectionInternal != null)
			{
				this.PerformanceCounters.HardConnectsPerSecond.Increment();
				dbConnectionInternal.MakeNonPooledObject(owningConnection, this.PerformanceCounters);
			}
			Bid.Trace("<prov.DbConnectionFactory.CreateNonPooledConnection|RES|CPOOL> %d#, Non-pooled database connection created.\n", this.ObjectID);
			return dbConnectionInternal;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00117240 File Offset: 0x00116640
		internal DbConnectionInternal CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
		{
			DbConnectionPoolGroupProviderInfo providerInfo = pool.PoolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(options, poolKey, providerInfo, pool, owningObject, userOptions);
			if (dbConnectionInternal != null)
			{
				this.PerformanceCounters.HardConnectsPerSecond.Increment();
				dbConnectionInternal.MakePooledConnection(pool);
			}
			Bid.Trace("<prov.DbConnectionFactory.CreatePooledConnection|RES|CPOOL> %d#, Pooled database connection created.\n", this.ObjectID);
			return dbConnectionInternal;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00117294 File Offset: 0x00116694
		internal virtual DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x001172A4 File Offset: 0x001166A4
		private Timer CreatePruningTimer()
		{
			TimerCallback callback = new TimerCallback(this.PruneConnectionPoolGroups);
			return new Timer(callback, null, 240000, 30000);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x001172D0 File Offset: 0x001166D0
		protected DbConnectionOptions FindConnectionOptions(DbConnectionPoolKey key)
		{
			if (!ADP.IsEmpty(key.ConnectionString))
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				DbConnectionPoolGroup dbConnectionPoolGroup;
				if (connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
				{
					return dbConnectionPoolGroup.ConnectionOptions;
				}
			}
			return null;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x00117304 File Offset: 0x00116704
		private static Task<DbConnectionInternal> GetCompletedTask()
		{
			if (DbConnectionFactory.s_completedTask == null)
			{
				TaskCompletionSource<DbConnectionInternal> taskCompletionSource = new TaskCompletionSource<DbConnectionInternal>();
				taskCompletionSource.SetResult(null);
				DbConnectionFactory.s_completedTask = taskCompletionSource.Task;
			}
			return DbConnectionFactory.s_completedTask;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00117338 File Offset: 0x00116738
		internal bool TryGetConnection(DbConnection owningConnection, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, out DbConnectionInternal connection)
		{
			DbConnectionFactory.<>c__DisplayClass31_0 CS$<>8__locals1 = new DbConnectionFactory.<>c__DisplayClass31_0();
			CS$<>8__locals1.retry = retry;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.owningConnection = owningConnection;
			CS$<>8__locals1.userOptions = userOptions;
			CS$<>8__locals1.oldConnection = oldConnection;
			connection = null;
			int num = 10;
			int num2 = 1;
			for (;;)
			{
				CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
				DbConnectionPool connectionPool = this.GetConnectionPool(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup);
				if (connectionPool == null)
				{
					CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
					if (CS$<>8__locals1.retry != null)
					{
						break;
					}
					connection = this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
					this.PerformanceCounters.NumberOfNonPooledConnections.Increment();
				}
				else
				{
					if (CS$<>8__locals1.owningConnection.ForceNewConnection)
					{
						connection = connectionPool.ReplaceConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.userOptions, CS$<>8__locals1.oldConnection);
					}
					else if (!connectionPool.TryGetConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.retry, CS$<>8__locals1.userOptions, out connection))
					{
						return false;
					}
					if (connection == null)
					{
						if (connectionPool.IsRunning)
						{
							goto Block_8;
						}
						Thread.Sleep(num2);
						num2 *= 2;
					}
				}
				if (connection != null || num-- <= 0)
				{
					goto IL_271;
				}
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			Task<DbConnectionInternal>[] obj = DbConnectionFactory.s_pendingOpenNonPooled;
			Task<DbConnectionInternal> task3;
			lock (obj)
			{
				int i;
				for (i = 0; i < DbConnectionFactory.s_pendingOpenNonPooled.Length; i++)
				{
					Task task4 = DbConnectionFactory.s_pendingOpenNonPooled[i];
					if (task4 == null)
					{
						DbConnectionFactory.s_pendingOpenNonPooled[i] = DbConnectionFactory.GetCompletedTask();
						break;
					}
					if (task4.IsCompleted)
					{
						break;
					}
				}
				if (i == DbConnectionFactory.s_pendingOpenNonPooled.Length)
				{
					i = DbConnectionFactory.s_pendingOpenNonPooledNext++ % DbConnectionFactory.s_pendingOpenNonPooled.Length;
				}
				Task<DbConnectionInternal> task2 = DbConnectionFactory.s_pendingOpenNonPooled[i];
				Func<Task<DbConnectionInternal>, DbConnectionInternal> continuationFunction;
				if ((continuationFunction = CS$<>8__locals1.<>9__1) == null)
				{
					continuationFunction = (CS$<>8__locals1.<>9__1 = delegate(Task<DbConnectionInternal> _)
					{
						Transaction currentTransaction = ADP.GetCurrentTransaction();
						DbConnectionInternal result;
						try
						{
							ADP.SetCurrentTransaction(CS$<>8__locals1.retry.Task.AsyncState as Transaction);
							DbConnectionInternal dbConnectionInternal = CS$<>8__locals1.<>4__this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
							if (CS$<>8__locals1.oldConnection != null && CS$<>8__locals1.oldConnection.State == ConnectionState.Open)
							{
								CS$<>8__locals1.oldConnection.PrepareForReplaceConnection();
								CS$<>8__locals1.oldConnection.Dispose();
							}
							result = dbConnectionInternal;
						}
						finally
						{
							ADP.SetCurrentTransaction(currentTransaction);
						}
						return result;
					});
				}
				task3 = task2.ContinueWith<DbConnectionInternal>(continuationFunction, cancellationTokenSource.Token, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
				DbConnectionFactory.s_pendingOpenNonPooled[i] = task3;
			}
			if (CS$<>8__locals1.owningConnection.ConnectionTimeout > 0)
			{
				int millisecondsDelay = CS$<>8__locals1.owningConnection.ConnectionTimeout * 1000;
				cancellationTokenSource.CancelAfter(millisecondsDelay);
			}
			task3.ContinueWith(delegate(Task<DbConnectionInternal> task)
			{
				cancellationTokenSource.Dispose();
				if (task.IsCanceled)
				{
					CS$<>8__locals1.retry.TrySetException(ADP.ExceptionWithStackTrace(ADP.NonPooledOpenTimeout()));
					return;
				}
				if (task.IsFaulted)
				{
					CS$<>8__locals1.retry.TrySetException(task.Exception.InnerException);
					return;
				}
				if (CS$<>8__locals1.retry.TrySetResult(task.Result))
				{
					CS$<>8__locals1.<>4__this.PerformanceCounters.NumberOfNonPooledConnections.Increment();
					return;
				}
				task.Result.DoomThisConnection();
				task.Result.Dispose();
			}, TaskScheduler.Default);
			return false;
			Block_8:
			Bid.Trace("<prov.DbConnectionFactory.GetConnection|RES|CPOOL> %d#, GetConnection failed because a pool timeout occurred.\n", this.ObjectID);
			throw ADP.PooledOpenTimeout();
			IL_271:
			if (connection == null)
			{
				Bid.Trace("<prov.DbConnectionFactory.GetConnection|RES|CPOOL> %d#, GetConnection failed because a pool timeout occurred and all retries were exhausted.\n", this.ObjectID);
				throw ADP.PooledOpenTimeout();
			}
			return true;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x001175F0 File Offset: 0x001169F0
		private DbConnectionPool GetConnectionPool(DbConnection owningObject, DbConnectionPoolGroup connectionPoolGroup)
		{
			if (connectionPoolGroup.IsDisabled && connectionPoolGroup.PoolGroupOptions != null)
			{
				Bid.Trace("<prov.DbConnectionFactory.GetConnectionPool|RES|INFO|CPOOL> %d#, DisabledPoolGroup=%d#\n", this.ObjectID, connectionPoolGroup.ObjectID);
				DbConnectionPoolGroupOptions poolGroupOptions = connectionPoolGroup.PoolGroupOptions;
				DbConnectionOptions connectionOptions = connectionPoolGroup.ConnectionOptions;
				connectionPoolGroup = this.GetConnectionPoolGroup(connectionPoolGroup.PoolKey, poolGroupOptions, ref connectionOptions);
				this.SetConnectionPoolGroup(owningObject, connectionPoolGroup);
			}
			return connectionPoolGroup.GetConnectionPool(this);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00117654 File Offset: 0x00116A54
		internal DbConnectionPoolGroup GetConnectionPoolGroup(DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolOptions, ref DbConnectionOptions userConnectionOptions)
		{
			if (ADP.IsEmpty(key.ConnectionString))
			{
				return null;
			}
			Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup) || (dbConnectionPoolGroup.IsDisabled && dbConnectionPoolGroup.PoolGroupOptions != null))
			{
				DbConnectionOptions dbConnectionOptions = this.CreateConnectionOptions(key.ConnectionString, userConnectionOptions);
				if (dbConnectionOptions == null)
				{
					throw ADP.InternalConnectionError(ADP.ConnectionError.ConnectionOptionsMissing);
				}
				string text = key.ConnectionString;
				if (userConnectionOptions == null)
				{
					userConnectionOptions = dbConnectionOptions;
					text = dbConnectionOptions.Expand();
					if (text != key.ConnectionString)
					{
						DbConnectionPoolKey dbConnectionPoolKey = (DbConnectionPoolKey)((ICloneable)key).Clone();
						dbConnectionPoolKey.ConnectionString = text;
						return this.GetConnectionPoolGroup(dbConnectionPoolKey, null, ref userConnectionOptions);
					}
				}
				if (poolOptions == null && ADP.IsWindowsNT)
				{
					if (dbConnectionPoolGroup != null)
					{
						poolOptions = dbConnectionPoolGroup.PoolGroupOptions;
					}
					else
					{
						poolOptions = this.CreateConnectionPoolGroupOptions(dbConnectionOptions);
					}
				}
				lock (this)
				{
					connectionPoolGroups = this._connectionPoolGroups;
					if (!connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
					{
						DbConnectionPoolGroup dbConnectionPoolGroup2 = new DbConnectionPoolGroup(dbConnectionOptions, key, poolOptions);
						dbConnectionPoolGroup2.ProviderInfo = this.CreateConnectionPoolGroupProviderInfo(dbConnectionOptions);
						Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(1 + connectionPoolGroups.Count);
						foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
						dictionary.Add(key, dbConnectionPoolGroup2);
						this.PerformanceCounters.NumberOfActiveConnectionPoolGroups.Increment();
						dbConnectionPoolGroup = dbConnectionPoolGroup2;
						this._connectionPoolGroups = dictionary;
					}
					return dbConnectionPoolGroup;
				}
			}
			if (userConnectionOptions == null)
			{
				userConnectionOptions = dbConnectionPoolGroup.ConnectionOptions;
			}
			return dbConnectionPoolGroup;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00117810 File Offset: 0x00116C10
		internal DbMetaDataFactory GetMetaDataFactory(DbConnectionPoolGroup connectionPoolGroup, DbConnectionInternal internalConnection)
		{
			DbMetaDataFactory dbMetaDataFactory = connectionPoolGroup.MetaDataFactory;
			if (dbMetaDataFactory == null)
			{
				bool flag = false;
				dbMetaDataFactory = this.CreateMetaDataFactory(internalConnection, out flag);
				if (flag)
				{
					connectionPoolGroup.MetaDataFactory = dbMetaDataFactory;
				}
			}
			return dbMetaDataFactory;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00117840 File Offset: 0x00116C40
		private void PruneConnectionPoolGroups(object state)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> %d#\n", this.ObjectID);
			}
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (this._poolsToRelease.Count != 0)
				{
					DbConnectionPool[] array = this._poolsToRelease.ToArray();
					foreach (DbConnectionPool dbConnectionPool in array)
					{
						if (dbConnectionPool != null)
						{
							dbConnectionPool.Clear();
							if (dbConnectionPool.Count == 0)
							{
								this._poolsToRelease.Remove(dbConnectionPool);
								if (Bid.AdvancedOn)
								{
									Bid.Trace("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> %d#, ReleasePool=%d#\n", this.ObjectID, dbConnectionPool.ObjectID);
								}
								this.PerformanceCounters.NumberOfInactiveConnectionPools.Decrement();
							}
						}
					}
				}
			}
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				if (this._poolGroupsToRelease.Count != 0)
				{
					DbConnectionPoolGroup[] array3 = this._poolGroupsToRelease.ToArray();
					foreach (DbConnectionPoolGroup dbConnectionPoolGroup in array3)
					{
						if (dbConnectionPoolGroup != null && dbConnectionPoolGroup.Clear() == 0)
						{
							this._poolGroupsToRelease.Remove(dbConnectionPoolGroup);
							if (Bid.AdvancedOn)
							{
								Bid.Trace("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> %d#, ReleasePoolGroup=%d#\n", this.ObjectID, dbConnectionPoolGroup.ObjectID);
							}
							this.PerformanceCounters.NumberOfInactiveConnectionPoolGroups.Decrement();
						}
					}
				}
			}
			lock (this)
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(connectionPoolGroups.Count);
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					if (keyValuePair.Value != null)
					{
						if (keyValuePair.Value.Prune())
						{
							this.PerformanceCounters.NumberOfActiveConnectionPoolGroups.Decrement();
							this.QueuePoolGroupForRelease(keyValuePair.Value);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				this._connectionPoolGroups = dictionary;
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00117AB8 File Offset: 0x00116EB8
		internal void QueuePoolForRelease(DbConnectionPool pool, bool clearing)
		{
			pool.Shutdown();
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (clearing)
				{
					pool.Clear();
				}
				this._poolsToRelease.Add(pool);
			}
			this.PerformanceCounters.NumberOfInactiveConnectionPools.Increment();
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00117B2C File Offset: 0x00116F2C
		internal void QueuePoolGroupForRelease(DbConnectionPoolGroup poolGroup)
		{
			Bid.Trace("<prov.DbConnectionFactory.QueuePoolGroupForRelease|RES|INFO|CPOOL> %d#, poolGroup=%d#\n", this.ObjectID, poolGroup.ObjectID);
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				this._poolGroupsToRelease.Add(poolGroup);
			}
			this.PerformanceCounters.NumberOfInactiveConnectionPoolGroups.Increment();
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00117BA4 File Offset: 0x00116FA4
		protected virtual DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection);
		}

		// Token: 0x06002A67 RID: 10855
		protected abstract DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection);

		// Token: 0x06002A68 RID: 10856
		protected abstract DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous);

		// Token: 0x06002A69 RID: 10857
		protected abstract DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions options);

		// Token: 0x06002A6A RID: 10858
		internal abstract DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection);

		// Token: 0x06002A6B RID: 10859
		internal abstract DbConnectionInternal GetInnerConnection(DbConnection connection);

		// Token: 0x06002A6C RID: 10860
		protected abstract int GetObjectId(DbConnection connection);

		// Token: 0x06002A6D RID: 10861
		internal abstract void PermissionDemand(DbConnection outerConnection);

		// Token: 0x06002A6E RID: 10862
		internal abstract void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup);

		// Token: 0x06002A6F RID: 10863
		internal abstract void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x06002A70 RID: 10864
		internal abstract bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from);

		// Token: 0x06002A71 RID: 10865
		internal abstract void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x04001B1C RID: 6940
		private Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> _connectionPoolGroups;

		// Token: 0x04001B1D RID: 6941
		private readonly List<DbConnectionPool> _poolsToRelease;

		// Token: 0x04001B1E RID: 6942
		private readonly List<DbConnectionPoolGroup> _poolGroupsToRelease;

		// Token: 0x04001B1F RID: 6943
		private readonly DbConnectionPoolCounters _performanceCounters;

		// Token: 0x04001B20 RID: 6944
		private readonly Timer _pruningTimer;

		// Token: 0x04001B21 RID: 6945
		private const int PruningDueTime = 240000;

		// Token: 0x04001B22 RID: 6946
		private const int PruningPeriod = 30000;

		// Token: 0x04001B23 RID: 6947
		private static int _objectTypeCount;

		// Token: 0x04001B24 RID: 6948
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionFactory._objectTypeCount);

		// Token: 0x04001B25 RID: 6949
		private static int s_pendingOpenNonPooledNext = 0;

		// Token: 0x04001B26 RID: 6950
		private static Task<DbConnectionInternal>[] s_pendingOpenNonPooled = new Task<DbConnectionInternal>[Environment.ProcessorCount];

		// Token: 0x04001B27 RID: 6951
		private static Task<DbConnectionInternal> s_completedTask;
	}
}
