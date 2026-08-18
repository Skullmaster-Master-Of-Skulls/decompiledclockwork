using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Common;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x0200009A RID: 154
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x00078544 File Offset: 0x00077944
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new HybridDictionary(1, false);
			this._state = 1;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00078594 File Offset: 0x00077994
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000785B4 File Offset: 0x000779B4
		internal int Count
		{
			get
			{
				return this._poolCount;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x000785D4 File Offset: 0x000779D4
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x000785F4 File Offset: 0x000779F4
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

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00078624 File Offset: 0x00077A24
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00078644 File Offset: 0x00077A44
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00078664 File Offset: 0x00077A64
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00078684 File Offset: 0x00077A84
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x000786A4 File Offset: 0x00077AA4
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

		// Token: 0x06000847 RID: 2119 RVA: 0x000786C4 File Offset: 0x00077AC4
		internal void Clear()
		{
			this.ClearInternal(true);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000786E4 File Offset: 0x00077AE4
		private bool ClearInternal(bool clearing)
		{
			bool result;
			lock (this)
			{
				HybridDictionary poolCollection = this._poolCollection;
				if (0 < poolCollection.Count)
				{
					HybridDictionary hybridDictionary = new HybridDictionary(poolCollection.Count, false);
					foreach (object obj in poolCollection)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value != null)
						{
							DbConnectionPool dbConnectionPool = (DbConnectionPool)dictionaryEntry.Value;
							if (clearing || (!dbConnectionPool.ErrorOccurred && dbConnectionPool.Count == 0))
							{
								DbConnectionFactory connectionFactory = dbConnectionPool.ConnectionFactory;
								connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Decrement();
								connectionFactory.QueuePoolForRelease(dbConnectionPool, clearing);
							}
							else
							{
								hybridDictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
							}
						}
					}
					this._poolCollection = hybridDictionary;
					this._poolCount = hybridDictionary.Count;
				}
				if (!clearing && this._poolCount == 0)
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

		// Token: 0x06000849 RID: 2121 RVA: 0x00078864 File Offset: 0x00077C64
		internal DbConnectionPool GetConnectionPool(DbConnectionFactory connectionFactory)
		{
			object obj = null;
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
				if (dbConnectionPoolIdentity != null)
				{
					HybridDictionary poolCollection = this._poolCollection;
					obj = poolCollection[dbConnectionPoolIdentity];
					if (obj == null)
					{
						DbConnectionPoolProviderInfo connectionPoolProviderInfo = connectionFactory.CreateConnectionPoolProviderInfo(this.ConnectionOptions);
						DbConnectionPool dbConnectionPool = new DbConnectionPool(connectionFactory, this, dbConnectionPoolIdentity, connectionPoolProviderInfo);
						lock (this)
						{
							poolCollection = this._poolCollection;
							obj = poolCollection[dbConnectionPoolIdentity];
							if (obj == null && this.MarkPoolGroupAsActive())
							{
								dbConnectionPool.Startup();
								HybridDictionary hybridDictionary = new HybridDictionary(1 + poolCollection.Count, false);
								foreach (object obj2 in poolCollection)
								{
									DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
									hybridDictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
								}
								hybridDictionary.Add(dbConnectionPoolIdentity, dbConnectionPool);
								connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Increment();
								this._poolCollection = hybridDictionary;
								this._poolCount = hybridDictionary.Count;
								obj = dbConnectionPool;
								dbConnectionPool = null;
							}
						}
						if (dbConnectionPool != null)
						{
							dbConnectionPool.Shutdown();
						}
					}
				}
			}
			if (obj == null)
			{
				lock (this)
				{
					this.MarkPoolGroupAsActive();
				}
			}
			return (DbConnectionPool)obj;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00078A24 File Offset: 0x00077E24
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
				Bid.Trace("<prov.DbConnectionPoolGroup.ClearInternal|RES|INFO|CPOOL> %d#, Active\n", this.ObjectID);
			}
			return 1 == this._state;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00078A64 File Offset: 0x00077E64
		internal bool Prune()
		{
			return this.ClearInternal(false);
		}

		// Token: 0x04000548 RID: 1352
		private const int PoolGroupStateActive = 1;

		// Token: 0x04000549 RID: 1353
		private const int PoolGroupStateIdle = 2;

		// Token: 0x0400054A RID: 1354
		private const int PoolGroupStateDisabled = 4;

		// Token: 0x0400054B RID: 1355
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x0400054C RID: 1356
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x0400054D RID: 1357
		private HybridDictionary _poolCollection;

		// Token: 0x0400054E RID: 1358
		private int _poolCount;

		// Token: 0x0400054F RID: 1359
		private int _state;

		// Token: 0x04000550 RID: 1360
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x04000551 RID: 1361
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x04000552 RID: 1362
		private static int _objectTypeCount;

		// Token: 0x04000553 RID: 1363
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPoolGroup._objectTypeCount);
	}
}
