using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C6 RID: 710
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x06002AF9 RID: 11001 RVA: 0x0011A79C File Offset: 0x00119B9C
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolKey = key;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
			this._state = 1;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0011A7E8 File Offset: 0x00119BE8
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0011A7FC File Offset: 0x00119BFC
		internal DbConnectionPoolKey PoolKey
		{
			get
			{
				return this._poolKey;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0011A810 File Offset: 0x00119C10
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0011A824 File Offset: 0x00119C24
		internal DbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			set
			{
				this._providerInfo = value;
				if (value != null)
				{
					this._providerInfo.PoolGroup = this;
				}
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0011A848 File Offset: 0x00119C48
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x0011A860 File Offset: 0x00119C60
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0011A874 File Offset: 0x00119C74
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x0011A888 File Offset: 0x00119C88
		// (set) Token: 0x06002B02 RID: 11010 RVA: 0x0011A89C File Offset: 0x00119C9C
		internal DbMetaDataFactory MetaDataFactory
		{
			get
			{
				return this._metaDataFactory;
			}
			set
			{
				this._metaDataFactory = value;
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0011A8B0 File Offset: 0x00119CB0
		internal int Clear()
		{
			ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = null;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					concurrentDictionary = this._poolCollection;
					this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
				}
			}
			if (concurrentDictionary != null)
			{
				foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in concurrentDictionary)
				{
					DbConnectionPool value = keyValuePair.Value;
					if (value != null)
					{
						DbConnectionFactory connectionFactory = value.ConnectionFactory;
						connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Decrement();
						connectionFactory.QueuePoolForRelease(value, true);
					}
				}
			}
			return this._poolCollection.Count;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0011A990 File Offset: 0x00119D90
		internal DbConnectionPool GetConnectionPool(DbConnectionFactory connectionFactory)
		{
			DbConnectionPool dbConnectionPool = null;
			if (this._poolGroupOptions != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.NoIdentity;
				if (this._poolGroupOptions.PoolByIdentity)
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
					if (dbConnectionPoolIdentity.IsRestricted)
					{
						dbConnectionPoolIdentity = null;
					}
				}
				if (dbConnectionPoolIdentity != null && !this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
				{
					lock (this)
					{
						if (!this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
						{
							DbConnectionPoolProviderInfo connectionPoolProviderInfo = connectionFactory.CreateConnectionPoolProviderInfo(this.ConnectionOptions);
							DbConnectionPool dbConnectionPool2 = new DbConnectionPool(connectionFactory, this, dbConnectionPoolIdentity, connectionPoolProviderInfo);
							if (this.MarkPoolGroupAsActive())
							{
								dbConnectionPool2.Startup();
								bool flag2 = this._poolCollection.TryAdd(dbConnectionPoolIdentity, dbConnectionPool2);
								connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Increment();
								dbConnectionPool = dbConnectionPool2;
							}
							else
							{
								dbConnectionPool2.Shutdown();
							}
						}
					}
				}
			}
			if (dbConnectionPool == null)
			{
				lock (this)
				{
					this.MarkPoolGroupAsActive();
				}
			}
			return dbConnectionPool;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0011AABC File Offset: 0x00119EBC
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
				Bid.Trace("<prov.DbConnectionPoolGroup.ClearInternal|RES|INFO|CPOOL> %d#, Active\n", this.ObjectID);
			}
			return 1 == this._state;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0011AAF4 File Offset: 0x00119EF4
		internal bool Prune()
		{
			bool result;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
					foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in this._poolCollection)
					{
						DbConnectionPool value = keyValuePair.Value;
						if (value != null)
						{
							if (!value.ErrorOccurred && value.Count == 0)
							{
								DbConnectionFactory connectionFactory = value.ConnectionFactory;
								connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Decrement();
								connectionFactory.QueuePoolForRelease(value, false);
							}
							else
							{
								concurrentDictionary.TryAdd(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
					this._poolCollection = concurrentDictionary;
				}
				if (this._poolCollection.Count == 0)
				{
					if (1 == this._state)
					{
						this._state = 2;
						Bid.Trace("<prov.DbConnectionPoolGroup.ClearInternal|RES|INFO|CPOOL> %d#, Idle\n", this.ObjectID);
					}
					else if (2 == this._state)
					{
						this._state = 4;
						Bid.Trace("<prov.DbConnectionPoolGroup.ReadyToRemove|RES|INFO|CPOOL> %d#, Disabled\n", this.ObjectID);
					}
				}
				result = (4 == this._state);
			}
			return result;
		}

		// Token: 0x04001B77 RID: 7031
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x04001B78 RID: 7032
		private readonly DbConnectionPoolKey _poolKey;

		// Token: 0x04001B79 RID: 7033
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x04001B7A RID: 7034
		private ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> _poolCollection;

		// Token: 0x04001B7B RID: 7035
		private int _state;

		// Token: 0x04001B7C RID: 7036
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x04001B7D RID: 7037
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x04001B7E RID: 7038
		private static int _objectTypeCount;

		// Token: 0x04001B7F RID: 7039
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPoolGroup._objectTypeCount);

		// Token: 0x04001B80 RID: 7040
		private const int PoolGroupStateActive = 1;

		// Token: 0x04001B81 RID: 7041
		private const int PoolGroupStateIdle = 2;

		// Token: 0x04001B82 RID: 7042
		private const int PoolGroupStateDisabled = 4;
	}
}
