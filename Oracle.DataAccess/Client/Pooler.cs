using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000E2 RID: 226
	internal class Pooler
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x00050ADE File Offset: 0x0004FADE
		public Pooler(int maxPools, int maxElemsInPool)
		{
			this.MaxPools = maxPools;
			this.MaxElemsInPool = maxElemsInPool;
			this.Pools = new Hashtable();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00050B00 File Offset: 0x0004FB00
		public void Put(object obj, object key, object val)
		{
			ConnDataPool connDataPool = null;
			lock (this.Pools.SyncRoot)
			{
				this.m_LastUseCnt += 1UL;
				if (object.ReferenceEquals(this.LastUsedObj, obj))
				{
					if (this.LastUsedConnDataPool != null)
					{
						connDataPool = this.LastUsedConnDataPool;
						if (object.ReferenceEquals(this.LastUsedKey, key) && this.LastUsedPoolMember != null)
						{
							this.LastUsedPoolMember.m_Value = val;
							this.LastUsedPoolMember.m_LastUsedTime = this.m_LastUseCnt;
							this.LastUsedConnDataPool.m_LastUsedTime = this.m_LastUseCnt;
							return;
						}
					}
				}
				else
				{
					this.LastUsedObj = obj;
					connDataPool = (this.LastUsedConnDataPool = (ConnDataPool)this.Pools[obj]);
				}
				if (connDataPool == null)
				{
					if (this.Pools.Count == this.MaxPools)
					{
						ulong num = ulong.MaxValue;
						object key2 = null;
						IDictionaryEnumerator enumerator = this.Pools.GetEnumerator();
						while (enumerator.MoveNext())
						{
							ulong lastUsedTime = ((ConnDataPool)enumerator.Value).m_LastUsedTime;
							if (lastUsedTime < num)
							{
								key2 = enumerator.Key;
								num = lastUsedTime;
							}
						}
						connDataPool = (ConnDataPool)this.Pools[key2];
						this.Pools.Remove(key2);
						connDataPool.m_ConnPool.Clear();
						connDataPool.m_ConnPool[key] = (this.LastUsedPoolMember = new PoolMember(val, this.m_LastUseCnt));
						connDataPool.m_LastUsedTime = this.m_LastUseCnt;
						this.LastUsedKey = key;
						this.Pools[obj] = (this.LastUsedConnDataPool = connDataPool);
						this.LastUsedObj = obj;
					}
					else
					{
						connDataPool = new ConnDataPool(new Hashtable(), this.m_LastUseCnt);
						connDataPool.m_ConnPool[key] = (this.LastUsedPoolMember = new PoolMember(val, this.m_LastUseCnt));
						this.LastUsedKey = key;
						this.Pools[obj] = (this.LastUsedConnDataPool = connDataPool);
						this.LastUsedObj = obj;
					}
				}
				else
				{
					connDataPool.m_LastUsedTime = this.m_LastUseCnt;
					PoolMember poolMember = (PoolMember)connDataPool.m_ConnPool[key];
					if (poolMember != null)
					{
						poolMember.m_Value = val;
						poolMember.m_LastUsedTime = this.m_LastUseCnt;
						this.LastUsedPoolMember = poolMember;
						this.LastUsedKey = key;
					}
					else
					{
						if (connDataPool.m_ConnPool.Count >= this.MaxElemsInPool)
						{
							ulong num2 = ulong.MaxValue;
							object key3 = null;
							IDictionaryEnumerator enumerator2 = connDataPool.m_ConnPool.GetEnumerator();
							while (enumerator2.MoveNext())
							{
								ulong lastUsedTime2 = ((PoolMember)enumerator2.Value).m_LastUsedTime;
								if (lastUsedTime2 < num2)
								{
									key3 = enumerator2.Key;
									num2 = lastUsedTime2;
								}
							}
							poolMember = (PoolMember)connDataPool.m_ConnPool[key3];
							connDataPool.m_ConnPool.Remove(key3);
							poolMember.m_LastUsedTime = this.m_LastUseCnt;
							poolMember.m_Value = val;
						}
						else
						{
							poolMember = new PoolMember(val, this.m_LastUseCnt);
						}
						connDataPool.m_ConnPool[key] = (this.LastUsedPoolMember = poolMember);
						this.LastUsedKey = key;
					}
				}
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00050E30 File Offset: 0x0004FE30
		public object Get(object obj, object key)
		{
			ConnDataPool connDataPool = null;
			object result;
			lock (this.Pools.SyncRoot)
			{
				this.m_LastUseCnt += 1UL;
				if (object.ReferenceEquals(this.LastUsedObj, obj))
				{
					if (this.LastUsedConnDataPool != null)
					{
						connDataPool = this.LastUsedConnDataPool;
						if (object.ReferenceEquals(this.LastUsedKey, key))
						{
							if (this.LastUsedPoolMember != null)
							{
								this.LastUsedPoolMember.m_LastUsedTime = this.m_LastUseCnt;
								this.LastUsedConnDataPool.m_LastUsedTime = this.m_LastUseCnt;
								return this.LastUsedPoolMember.m_Value;
							}
							this.LastUsedConnDataPool.m_LastUsedTime = this.m_LastUseCnt;
							return null;
						}
					}
				}
				else
				{
					this.LastUsedObj = obj;
					connDataPool = (this.LastUsedConnDataPool = (ConnDataPool)this.Pools[obj]);
				}
				if (connDataPool != null)
				{
					connDataPool.m_LastUsedTime = this.m_LastUseCnt;
					PoolMember poolMember = (PoolMember)connDataPool.m_ConnPool[key];
					if (poolMember != null)
					{
						poolMember.m_LastUsedTime = this.m_LastUseCnt;
						this.LastUsedPoolMember = poolMember;
						this.LastUsedKey = key;
						return poolMember.m_Value;
					}
					this.LastUsedPoolMember = null;
					this.LastUsedKey = key;
				}
				else
				{
					this.LastUsedPoolMember = null;
					this.LastUsedKey = key;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00050F9C File Offset: 0x0004FF9C
		public void RemovePool(object obj)
		{
			lock (this.Pools.SyncRoot)
			{
				this.Pools.Remove(obj);
				if (object.ReferenceEquals(this.LastUsedObj, obj))
				{
					this.LastUsedObj = null;
					this.LastUsedConnDataPool = null;
					this.LastUsedKey = null;
					this.LastUsedPoolMember = null;
				}
			}
		}

		// Token: 0x040006FE RID: 1790
		private Hashtable Pools;

		// Token: 0x040006FF RID: 1791
		private int MaxElemsInPool;

		// Token: 0x04000700 RID: 1792
		private int MaxPools;

		// Token: 0x04000701 RID: 1793
		private ulong m_LastUseCnt;

		// Token: 0x04000702 RID: 1794
		private object LastUsedObj;

		// Token: 0x04000703 RID: 1795
		private object LastUsedKey;

		// Token: 0x04000704 RID: 1796
		private PoolMember LastUsedPoolMember;

		// Token: 0x04000705 RID: 1797
		private ConnDataPool LastUsedConnDataPool;
	}
}
