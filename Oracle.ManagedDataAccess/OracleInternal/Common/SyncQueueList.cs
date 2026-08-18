using System;
using System.Collections.Generic;

namespace OracleInternal.Common
{
	// Token: 0x020000BF RID: 191
	internal class SyncQueueList<T>
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00045240 File Offset: 0x00043440
		internal SyncQueueList(int max = 2147483647)
		{
			this.m_list = new List<T>();
			this.m_sync = new object();
			this.m_max = max;
			if (this.m_max != 2147483647)
			{
				this.m_bMaxExplicitlySet = true;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0004527C File Offset: 0x0004347C
		internal List<T> GetList()
		{
			List<T> list = new List<T>();
			lock (this.m_sync)
			{
				for (int i = 0; i < this.m_list.Count; i++)
				{
					list.Add(this.m_list[i]);
				}
			}
			return list;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000452E8 File Offset: 0x000434E8
		internal void Enqueue(T t)
		{
			if (!this.m_bMaxExplicitlySet || this.m_list.Count < this.m_max)
			{
				lock (this.m_sync)
				{
					this.m_list.Add(t);
				}
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0004534C File Offset: 0x0004354C
		internal void Enqueue(ref T t)
		{
			if (!this.m_bMaxExplicitlySet || this.m_list.Count < this.m_max)
			{
				lock (this.m_sync)
				{
					this.m_list.Add(t);
				}
			}
			t = default(T);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000453BC File Offset: 0x000435BC
		internal bool Dequeue(out T t)
		{
			bool result;
			lock (this.m_sync)
			{
				if (this.m_list.Count == 0)
				{
					t = default(T);
					result = false;
				}
				else
				{
					t = this.m_list[0];
					this.m_list.Remove(t);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00045438 File Offset: 0x00043638
		internal virtual T Dequeue()
		{
			T result;
			lock (this.m_sync)
			{
				if (this.m_list.Count == 0)
				{
					result = default(T);
				}
				else
				{
					T t = this.m_list[0];
					this.m_list.Remove(t);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000454AC File Offset: 0x000436AC
		internal void Add(T t)
		{
			lock (this.m_sync)
			{
				this.m_list.Add(t);
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000454F4 File Offset: 0x000436F4
		internal bool AddIfNotExist(T t)
		{
			lock (this.m_sync)
			{
				if (!this.m_list.Contains(t))
				{
					this.m_list.Add(t);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00045550 File Offset: 0x00043750
		internal int IndexOf(T t)
		{
			int result;
			lock (this.m_sync)
			{
				int num = this.m_list.IndexOf(t);
				result = num;
			}
			return result;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0004559C File Offset: 0x0004379C
		internal bool Remove(T t, int minRequirement)
		{
			if (this.m_list.Count > minRequirement)
			{
				lock (this.m_sync)
				{
					if (this.m_list.Count > minRequirement)
					{
						return this.m_list.Remove(t);
					}
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00045608 File Offset: 0x00043808
		internal bool Remove(T t)
		{
			bool result;
			lock (this.m_sync)
			{
				result = this.m_list.Remove(t);
			}
			return result;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00045650 File Offset: 0x00043850
		internal void Clear()
		{
			lock (this.m_sync)
			{
				this.m_list.Clear();
			}
		}

		// Token: 0x170001D6 RID: 470
		internal T this[int index]
		{
			get
			{
				T result;
				lock (this.m_sync)
				{
					result = this.m_list[index];
				}
				return result;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000456E0 File Offset: 0x000438E0
		internal int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x04000A1B RID: 2587
		internal List<T> m_list;

		// Token: 0x04000A1C RID: 2588
		internal object m_sync;

		// Token: 0x04000A1D RID: 2589
		internal int m_max;

		// Token: 0x04000A1E RID: 2590
		internal bool m_bMaxExplicitlySet;
	}
}
