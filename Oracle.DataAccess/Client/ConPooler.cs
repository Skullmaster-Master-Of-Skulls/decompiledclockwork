using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000E3 RID: 227
	internal class ConPooler
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00051014 File Offset: 0x00050014
		public ConPooler(int maxElemsInPool)
		{
			this.MaxElemsInPool = maxElemsInPool;
			this.ConPoolMembers = new Hashtable();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0005102E File Offset: 0x0005002E
		public void ModifyConPoolerSize(int maxElemsInPool)
		{
			if (maxElemsInPool > ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON)
			{
				this.MaxElemsInPool = maxElemsInPool;
				return;
			}
			this.MaxElemsInPool = ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0005104C File Offset: 0x0005004C
		public void Put(object key, object val)
		{
			lock (this.ConPoolMembers.SyncRoot)
			{
				this.m_LastUseCnt += 1UL;
				PoolMember poolMember = (PoolMember)this.ConPoolMembers[key];
				if (poolMember != null)
				{
					poolMember.m_Value = val;
					poolMember.m_LastUsedTime = this.m_LastUseCnt;
				}
				else
				{
					if (this.ConPoolMembers.Count >= this.MaxElemsInPool)
					{
						object key2 = null;
						while (this.ConPoolMembers.Count >= this.MaxElemsInPool)
						{
							ulong num = ulong.MaxValue;
							IDictionaryEnumerator enumerator = this.ConPoolMembers.GetEnumerator();
							while (enumerator.MoveNext())
							{
								ulong lastUsedTime = ((PoolMember)enumerator.Value).m_LastUsedTime;
								if (lastUsedTime < num)
								{
									key2 = enumerator.Key;
									num = lastUsedTime;
								}
							}
							poolMember = (PoolMember)this.ConPoolMembers[key2];
							this.ConPoolMembers.Remove(key2);
						}
						poolMember.m_LastUsedTime = this.m_LastUseCnt;
						poolMember.m_Value = val;
					}
					else
					{
						poolMember = new PoolMember(val, this.m_LastUseCnt);
					}
					this.ConPoolMembers[key] = poolMember;
				}
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00051190 File Offset: 0x00050190
		public object Get(object key)
		{
			lock (this.ConPoolMembers.SyncRoot)
			{
				this.m_LastUseCnt += 1UL;
				PoolMember poolMember = (PoolMember)this.ConPoolMembers[key];
				if (poolMember != null)
				{
					poolMember.m_LastUsedTime = this.m_LastUseCnt;
					return poolMember.m_Value;
				}
			}
			return null;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0005120C File Offset: 0x0005020C
		public void Clear()
		{
			lock (this.ConPoolMembers.SyncRoot)
			{
				this.ConPoolMembers.Clear();
			}
		}

		// Token: 0x04000706 RID: 1798
		public static int DEFAULT_MAX_ELEMS_IN_POOL_TUNING_OFF = 200;

		// Token: 0x04000707 RID: 1799
		public static int DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON = 50;

		// Token: 0x04000708 RID: 1800
		private Hashtable ConPoolMembers;

		// Token: 0x04000709 RID: 1801
		private int MaxElemsInPool;

		// Token: 0x0400070A RID: 1802
		private ulong m_LastUseCnt;
	}
}
