using System;
using System.Collections.Generic;
using System.Threading;

namespace OracleInternal.Common
{
	// Token: 0x02000041 RID: 65
	internal class Pooler<keyType, valType>
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0001309C File Offset: 0x0001129C
		public Pooler(int maxCacheSize, int maxSubCacheSize, PoolerItemOwnership ownership)
		{
			this.m_sync = new object();
			this.m_maxCacheSize = maxCacheSize;
			this.m_maxSubCacheSize = maxSubCacheSize;
			this.m_lastUsedCount = 1UL;
			this.m_ownership = ownership;
			this.m_cache = new SortedDictionary<keyType, PoolMember<valType>>();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000130D8 File Offset: 0x000112D8
		internal bool Remove(keyType key)
		{
			return this.m_cache.Remove(key);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000130E8 File Offset: 0x000112E8
		internal PoolMember<valType> RemoveLRU()
		{
			ulong num = ulong.MaxValue;
			keyType key = default(keyType);
			foreach (KeyValuePair<keyType, PoolMember<valType>> keyValuePair in this.m_cache)
			{
				ulong lastUsedTime = keyValuePair.Value.m_LastUsedTime;
				if (lastUsedTime < num)
				{
					SortedDictionary<keyType, PoolMember<valType>>.Enumerator enumerator;
					KeyValuePair<keyType, PoolMember<valType>> keyValuePair2 = enumerator.Current;
					key = keyValuePair2.Key;
					num = lastUsedTime;
				}
			}
			PoolMember<valType> result = this.m_cache[key];
			this.m_cache.Remove(key);
			return result;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00013164 File Offset: 0x00011364
		public virtual valType Put(keyType key, valType val)
		{
			PoolMember<valType> poolMember = null;
			valType result;
			lock (this.m_sync)
			{
				this.m_cache.TryGetValue(key, out poolMember);
				if (poolMember != null)
				{
					if (poolMember.m_list.Count < this.m_maxSubCacheSize)
					{
						poolMember.m_list.Add(val);
						result = default(valType);
					}
					else
					{
						result = val;
					}
				}
				else if (this.m_cache.Count >= this.m_maxCacheSize)
				{
					poolMember = this.RemoveLRU();
					valType valType = default(valType);
					if (poolMember.m_list.Count > 0)
					{
						valType = poolMember.m_list[0];
					}
					poolMember.m_list.Clear();
					poolMember.m_list.Add(val);
					PoolMember<valType> poolMember2 = poolMember;
					ulong lastUsedCount;
					this.m_lastUsedCount = (lastUsedCount = this.m_lastUsedCount) + 1UL;
					poolMember2.m_LastUsedTime = lastUsedCount;
					this.m_cache[key] = poolMember;
					result = valType;
				}
				else
				{
					int maxSubCacheSize = this.m_maxSubCacheSize;
					ulong lastUsedCount2;
					this.m_lastUsedCount = (lastUsedCount2 = this.m_lastUsedCount) + 1UL;
					poolMember = new PoolMember<valType>(val, maxSubCacheSize, lastUsedCount2);
					this.m_cache[key] = poolMember;
					result = default(valType);
				}
			}
			return result;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000132B0 File Offset: 0x000114B0
		public valType Get(keyType key)
		{
			PoolMember<valType> poolMember = null;
			if (this.m_ownership == PoolerItemOwnership.Exclusive)
			{
				Monitor.Enter(this.m_sync);
			}
			valType result;
			try
			{
				this.m_cache.TryGetValue(key, out poolMember);
				if (poolMember != null)
				{
					if (poolMember.m_list.Count > 0)
					{
						valType valType = poolMember.m_list[0];
						if (this.m_ownership == PoolerItemOwnership.Exclusive)
						{
							poolMember.m_list.Remove(valType);
						}
						PoolMember<valType> poolMember2 = poolMember;
						ulong lastUsedCount;
						this.m_lastUsedCount = (lastUsedCount = this.m_lastUsedCount) + 1UL;
						poolMember2.m_LastUsedTime = lastUsedCount;
						result = valType;
					}
					else
					{
						result = default(valType);
					}
				}
				else
				{
					result = default(valType);
				}
			}
			finally
			{
				if (this.m_ownership == PoolerItemOwnership.Exclusive)
				{
					Monitor.Exit(this.m_sync);
				}
			}
			return result;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00013370 File Offset: 0x00011570
		public bool ContainsKey(keyType key)
		{
			bool result;
			try
			{
				result = this.m_cache.ContainsKey(key);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0400043D RID: 1085
		private object m_sync;

		// Token: 0x0400043E RID: 1086
		private ulong m_lastUsedCount;

		// Token: 0x0400043F RID: 1087
		private PoolerItemOwnership m_ownership;

		// Token: 0x04000440 RID: 1088
		private SortedDictionary<keyType, PoolMember<valType>> m_cache;

		// Token: 0x04000441 RID: 1089
		public int m_maxCacheSize;

		// Token: 0x04000442 RID: 1090
		public int m_maxSubCacheSize;
	}
}
