using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x02000042 RID: 66
	internal class ConStringPool : Pooler<int, List<ConnectionString>>
	{
		// Token: 0x060002FE RID: 766 RVA: 0x000133A4 File Offset: 0x000115A4
		public ConStringPool(int maxCacheSize) : base(maxCacheSize, 1, PoolerItemOwnership.Shared)
		{
			this.m_sync = new object();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000133BC File Offset: 0x000115BC
		public bool Remove(ConnectionString val)
		{
			List<ConnectionString> list = null;
			if (base.ContainsKey(val.m_key))
			{
				list = base.Get(val.m_key);
				if (list != null)
				{
					lock (list)
					{
						return list.Remove(val);
					}
				}
			}
			if (list != null && list.Count == 0)
			{
				lock (this.m_sync)
				{
					if (list.Count == 0)
					{
						return base.Remove(val.m_key);
					}
				}
			}
			return false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001346C File Offset: 0x0001166C
		public void Put(ConnectionString val)
		{
			bool flag = false;
			if (!base.ContainsKey(val.m_key))
			{
				lock (this.m_sync)
				{
					if (!base.ContainsKey(val.m_key))
					{
						List<ConnectionString> list = new List<ConnectionString>(4);
						val.m_bPooled = true;
						list.Add(val);
						base.Put(val.m_key, list);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				bool flag3 = false;
				lock (this.m_sync)
				{
					List<ConnectionString> list = base.Get(val.m_key);
					if (list != null)
					{
						lock (list)
						{
							int num = 0;
							while (num < list.Count && !flag3)
							{
								ConnectionString connectionString = list[num];
								if (connectionString.m_compString == val.m_compString && connectionString.Password == val.Password && connectionString.ProxyPassword == val.ProxyPassword && connectionString.m_osUserName == val.m_osUserName)
								{
									flag3 = true;
								}
								num++;
							}
							if (!flag3)
							{
								val.m_bPooled = true;
								list.Add(val);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000443 RID: 1091
		private object m_sync;
	}
}
