using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Common;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x02000277 RID: 631
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x0600214A RID: 8522 RVA: 0x00285078 File Offset: 0x00284478
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new HybridDictionary(1, false);
			this._state = 1;
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x002850C8 File Offset: 0x002844C8
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x002850E8 File Offset: 0x002844E8
		internal int Count
		{
			get
			{
				return this._poolCount;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x00285108 File Offset: 0x00284508
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x00285128 File Offset: 0x00284528
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00285158 File Offset: 0x00284558
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00285178 File Offset: 0x00284578
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00285198 File Offset: 0x00284598
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x002851B8 File Offset: 0x002845B8
		// (set) Token: 0x06002153 RID: 8531 RVA: 0x002851D8 File Offset: 0x002845D8
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

		// Token: 0x06002154 RID: 8532 RVA: 0x002851F8 File Offset: 0x002845F8
		internal void Clear()
		{
			this.ClearInternal(true);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00285218 File Offset: 0x00284618
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

		// Token: 0x06002156 RID: 8534 RVA: 0x00285398 File Offset: 0x00284798
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

		// Token: 0x06002157 RID: 8535 RVA: 0x00285558 File Offset: 0x00284958
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
				Bid.Trace("<prov.DbConnectionPoolGroup.ClearInternal|RES|INFO|CPOOL> %d#, Active\n", this.ObjectID);
			}
			return 1 == this._state;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00285598 File Offset: 0x00284998
		internal bool Prune()
		{
			return this.ClearInternal(false);
		}

		// Token: 0x040015AC RID: 5548
		private const int PoolGroupStateActive = 1;

		// Token: 0x040015AD RID: 5549
		private const int PoolGroupStateIdle = 2;

		// Token: 0x040015AE RID: 5550
		private const int PoolGroupStateDisabled = 4;

		// Token: 0x040015AF RID: 5551
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x040015B0 RID: 5552
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x040015B1 RID: 5553
		private HybridDictionary _poolCollection;

		// Token: 0x040015B2 RID: 5554
		private int _poolCount;

		// Token: 0x040015B3 RID: 5555
		private int _state;

		// Token: 0x040015B4 RID: 5556
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x040015B5 RID: 5557
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x040015B6 RID: 5558
		private static int _objectTypeCount;

		// Token: 0x040015B7 RID: 5559
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPoolGroup._objectTypeCount);
	}
}
