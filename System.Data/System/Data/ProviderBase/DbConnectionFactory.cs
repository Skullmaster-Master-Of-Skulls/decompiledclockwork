using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x020001D7 RID: 471
	internal abstract class DbConnectionFactory
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0025CF68 File Offset: 0x0025C368
		protected DbConnectionFactory() : this(DbConnectionPoolCountersNoCounters.SingletonInstance)
		{
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0025CF88 File Offset: 0x0025C388
		protected DbConnectionFactory(DbConnectionPoolCounters performanceCounters)
		{
			this._performanceCounters = performanceCounters;
			this._connectionPoolGroups = new Dictionary<string, DbConnectionPoolGroup>();
			this._poolsToRelease = new List<DbConnectionPool>();
			this._poolGroupsToRelease = new List<DbConnectionPoolGroup>();
			this._pruningTimer = this.CreatePruningTimer();
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0025CFE8 File Offset: 0x0025C3E8
		internal DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._performanceCounters;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001A27 RID: 6695
		public abstract DbProviderFactory ProviderFactory { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0025D008 File Offset: 0x0025C408
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0025D028 File Offset: 0x0025C428
		public void ClearAllPools()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionFactory.ClearAllPools|API> ");
			try
			{
				Dictionary<string, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				foreach (KeyValuePair<string, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
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

		// Token: 0x06001A2A RID: 6698 RVA: 0x0025D0C8 File Offset: 0x0025C4C8
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

		// Token: 0x06001A2B RID: 6699 RVA: 0x0025D138 File Offset: 0x0025C538
		public void ClearPool(string connectionString)
		{
			ADP.CheckArgumentNull(connectionString, "connectionString");
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionFactory.ClearPool|API> connectionString");
			try
			{
				Dictionary<string, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				DbConnectionPoolGroup dbConnectionPoolGroup;
				if (connectionPoolGroups.TryGetValue(connectionString, out dbConnectionPoolGroup))
				{
					dbConnectionPoolGroup.Clear();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0025D1A8 File Offset: 0x0025C5A8
		internal virtual DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0025D1B8 File Offset: 0x0025C5B8
		protected virtual DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			throw ADP.NotSupported();
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0025D1D8 File Offset: 0x0025C5D8
		internal DbConnectionInternal CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup)
		{
			DbConnectionOptions connectionOptions = poolGroup.ConnectionOptions;
			DbConnectionPoolGroupProviderInfo providerInfo = poolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(connectionOptions, providerInfo, null, owningConnection);
			if (dbConnectionInternal != null)
			{
				this.PerformanceCounters.HardConnectsPerSecond.Increment();
				dbConnectionInternal.MakeNonPooledObject(owningConnection, this.PerformanceCounters);
			}
			Bid.Trace("<prov.DbConnectionFactory.CreateNonPooledConnection|RES|CPOOL> %d#, Non-pooled database connection created.\n", this.ObjectID);
			return dbConnectionInternal;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0025D238 File Offset: 0x0025C638
		internal DbConnectionInternal CreatePooledConnection(DbConnection owningConnection, DbConnectionPool pool, DbConnectionOptions options)
		{
			DbConnectionPoolGroupProviderInfo providerInfo = pool.PoolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(options, providerInfo, pool, owningConnection);
			if (dbConnectionInternal != null)
			{
				this.PerformanceCounters.HardConnectsPerSecond.Increment();
				dbConnectionInternal.MakePooledConnection(pool);
			}
			Bid.Trace("<prov.DbConnectionFactory.CreatePooledConnection|RES|CPOOL> %d#, Pooled database connection created.\n", this.ObjectID);
			return dbConnectionInternal;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0025D288 File Offset: 0x0025C688
		internal virtual DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0025D298 File Offset: 0x0025C698
		private Timer CreatePruningTimer()
		{
			TimerCallback callback = new TimerCallback(this.PruneConnectionPoolGroups);
			return new Timer(callback, null, 240000, 30000);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0025D2C8 File Offset: 0x0025C6C8
		protected DbConnectionOptions FindConnectionOptions(string connectionString)
		{
			if (!ADP.IsEmpty(connectionString))
			{
				Dictionary<string, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				DbConnectionPoolGroup dbConnectionPoolGroup;
				if (connectionPoolGroups.TryGetValue(connectionString, out dbConnectionPoolGroup))
				{
					return dbConnectionPoolGroup.ConnectionOptions;
				}
			}
			return null;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0025D2F8 File Offset: 0x0025C6F8
		internal DbConnectionInternal GetConnection(DbConnection owningConnection)
		{
			int num = 5;
			DbConnectionInternal dbConnectionInternal;
			for (;;)
			{
				DbConnectionPoolGroup connectionPoolGroup = this.GetConnectionPoolGroup(owningConnection);
				DbConnectionPool connectionPool = this.GetConnectionPool(owningConnection, connectionPoolGroup);
				if (connectionPool == null)
				{
					connectionPoolGroup = this.GetConnectionPoolGroup(owningConnection);
					dbConnectionInternal = this.CreateNonPooledConnection(owningConnection, connectionPoolGroup);
					this.PerformanceCounters.NumberOfNonPooledConnections.Increment();
				}
				else
				{
					dbConnectionInternal = connectionPool.GetConnection(owningConnection);
					if (dbConnectionInternal != null)
					{
						goto IL_73;
					}
					if (connectionPool.IsRunning)
					{
						break;
					}
					Thread.Sleep(1);
				}
				if (dbConnectionInternal != null || num-- <= 0)
				{
					goto IL_73;
				}
			}
			Bid.Trace("<prov.DbConnectionFactory.GetConnection|RES|CPOOL> %d#, GetConnection failed because a pool timeout occurred.\n", this.ObjectID);
			throw ADP.PooledOpenTimeout();
			IL_73:
			if (dbConnectionInternal == null)
			{
				Bid.Trace("<prov.DbConnectionFactory.GetConnection|RES|CPOOL> %d#, GetConnection failed because a pool timeout occurred and all retries were exhausted.\n", this.ObjectID);
				throw ADP.PooledOpenTimeout();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0025D398 File Offset: 0x0025C798
		private DbConnectionPool GetConnectionPool(DbConnection owningObject, DbConnectionPoolGroup connectionPoolGroup)
		{
			if (connectionPoolGroup.IsDisabled && connectionPoolGroup.PoolGroupOptions != null)
			{
				Bid.Trace("<prov.DbConnectionFactory.GetConnectionPool|RES|INFO|CPOOL> %d#, DisabledPoolGroup=%d#\n", this.ObjectID, connectionPoolGroup.ObjectID);
				DbConnectionPoolGroupOptions poolGroupOptions = connectionPoolGroup.PoolGroupOptions;
				DbConnectionOptions connectionOptions = connectionPoolGroup.ConnectionOptions;
				string connectionString = connectionOptions.UsersConnectionString(false);
				connectionPoolGroup = this.GetConnectionPoolGroup(connectionString, poolGroupOptions, ref connectionOptions);
				this.SetConnectionPoolGroup(owningObject, connectionPoolGroup);
			}
			return connectionPoolGroup.GetConnectionPool(this);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0025D408 File Offset: 0x0025C808
		internal DbConnectionPoolGroup GetConnectionPoolGroup(string connectionString, DbConnectionPoolGroupOptions poolOptions, ref DbConnectionOptions userConnectionOptions)
		{
			if (ADP.IsEmpty(connectionString))
			{
				return null;
			}
			Dictionary<string, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!connectionPoolGroups.TryGetValue(connectionString, out dbConnectionPoolGroup) || (dbConnectionPoolGroup.IsDisabled && dbConnectionPoolGroup.PoolGroupOptions != null))
			{
				DbConnectionOptions dbConnectionOptions = this.CreateConnectionOptions(connectionString, userConnectionOptions);
				if (dbConnectionOptions == null)
				{
					throw ADP.InternalConnectionError(ADP.ConnectionError.ConnectionOptionsMissing);
				}
				string text = connectionString;
				if (userConnectionOptions == null)
				{
					userConnectionOptions = dbConnectionOptions;
					text = dbConnectionOptions.Expand();
					if (text != connectionString)
					{
						return this.GetConnectionPoolGroup(text, null, ref userConnectionOptions);
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
				DbConnectionPoolGroup dbConnectionPoolGroup2 = new DbConnectionPoolGroup(dbConnectionOptions, poolOptions);
				dbConnectionPoolGroup2.ProviderInfo = this.CreateConnectionPoolGroupProviderInfo(dbConnectionOptions);
				lock (this)
				{
					connectionPoolGroups = this._connectionPoolGroups;
					if (!connectionPoolGroups.TryGetValue(text, out dbConnectionPoolGroup))
					{
						Dictionary<string, DbConnectionPoolGroup> dictionary = new Dictionary<string, DbConnectionPoolGroup>(1 + connectionPoolGroups.Count);
						foreach (KeyValuePair<string, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
						dictionary.Add(text, dbConnectionPoolGroup2);
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

		// Token: 0x06001A36 RID: 6710 RVA: 0x0025D598 File Offset: 0x0025C998
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

		// Token: 0x06001A37 RID: 6711 RVA: 0x0025D5C8 File Offset: 0x0025C9C8
		private void PruneConnectionPoolGroups(object state)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> %d#\n", this.ObjectID);
			}
			lock (this._poolsToRelease)
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
			lock (this._poolGroupsToRelease)
			{
				if (this._poolGroupsToRelease.Count != 0)
				{
					DbConnectionPoolGroup[] array3 = this._poolGroupsToRelease.ToArray();
					foreach (DbConnectionPoolGroup dbConnectionPoolGroup in array3)
					{
						if (dbConnectionPoolGroup != null)
						{
							dbConnectionPoolGroup.Clear();
							if (dbConnectionPoolGroup.Count == 0)
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
			}
			lock (this)
			{
				Dictionary<string, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				Dictionary<string, DbConnectionPoolGroup> dictionary = new Dictionary<string, DbConnectionPoolGroup>(connectionPoolGroups.Count);
				foreach (KeyValuePair<string, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
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

		// Token: 0x06001A38 RID: 6712 RVA: 0x0025D828 File Offset: 0x0025CC28
		internal void QueuePoolForRelease(DbConnectionPool pool, bool clearing)
		{
			pool.Shutdown();
			lock (this._poolsToRelease)
			{
				if (clearing)
				{
					pool.Clear();
				}
				this._poolsToRelease.Add(pool);
			}
			this.PerformanceCounters.NumberOfInactiveConnectionPools.Increment();
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0025D898 File Offset: 0x0025CC98
		internal void QueuePoolGroupForRelease(DbConnectionPoolGroup poolGroup)
		{
			Bid.Trace("<prov.DbConnectionFactory.QueuePoolGroupForRelease|RES|INFO|CPOOL> %d#, poolGroup=%d#\n", this.ObjectID, poolGroup.ObjectID);
			lock (this._poolGroupsToRelease)
			{
				this._poolGroupsToRelease.Add(poolGroup);
			}
			this.PerformanceCounters.NumberOfInactiveConnectionPoolGroups.Increment();
		}

		// Token: 0x06001A3A RID: 6714
		protected abstract DbConnectionInternal CreateConnection(DbConnectionOptions options, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection);

		// Token: 0x06001A3B RID: 6715
		protected abstract DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous);

		// Token: 0x06001A3C RID: 6716
		protected abstract DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions options);

		// Token: 0x06001A3D RID: 6717
		internal abstract DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection);

		// Token: 0x06001A3E RID: 6718
		internal abstract DbConnectionInternal GetInnerConnection(DbConnection connection);

		// Token: 0x06001A3F RID: 6719
		protected abstract int GetObjectId(DbConnection connection);

		// Token: 0x06001A40 RID: 6720
		internal abstract void PermissionDemand(DbConnection outerConnection);

		// Token: 0x06001A41 RID: 6721
		internal abstract void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup);

		// Token: 0x06001A42 RID: 6722
		internal abstract void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x06001A43 RID: 6723
		internal abstract bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from);

		// Token: 0x06001A44 RID: 6724
		internal abstract void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x04000F9F RID: 3999
		private const int PruningDueTime = 240000;

		// Token: 0x04000FA0 RID: 4000
		private const int PruningPeriod = 30000;

		// Token: 0x04000FA1 RID: 4001
		private Dictionary<string, DbConnectionPoolGroup> _connectionPoolGroups;

		// Token: 0x04000FA2 RID: 4002
		private readonly List<DbConnectionPool> _poolsToRelease;

		// Token: 0x04000FA3 RID: 4003
		private readonly List<DbConnectionPoolGroup> _poolGroupsToRelease;

		// Token: 0x04000FA4 RID: 4004
		private readonly DbConnectionPoolCounters _performanceCounters;

		// Token: 0x04000FA5 RID: 4005
		private readonly Timer _pruningTimer;

		// Token: 0x04000FA6 RID: 4006
		private static int _objectTypeCount;

		// Token: 0x04000FA7 RID: 4007
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionFactory._objectTypeCount);
	}
}
