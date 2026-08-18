using System;
using System.Collections;
using System.Collections.Generic;

namespace OracleInternal.Common
{
	// Token: 0x020000C1 RID: 193
	internal class SyncDictionary<K, V>
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x00045794 File Offset: 0x00043994
		internal SyncDictionary()
		{
			this.m_sync = new object();
			this.m_hashtable = new Dictionary<K, V>();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x000457B4 File Offset: 0x000439B4
		internal int Count
		{
			get
			{
				int count;
				lock (this.m_sync)
				{
					count = this.m_hashtable.Count;
				}
				return count;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000457FC File Offset: 0x000439FC
		internal void Clear()
		{
			lock (this.m_sync)
			{
				this.m_hashtable.Clear();
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00045844 File Offset: 0x00043A44
		internal bool Remove(K k)
		{
			bool result;
			lock (this.m_sync)
			{
				result = this.m_hashtable.Remove(k);
			}
			return result;
		}

		// Token: 0x170001D9 RID: 473
		internal V this[K key]
		{
			get
			{
				V result;
				lock (this.m_sync)
				{
					if (this.m_hashtable.ContainsKey(key))
					{
						result = this.m_hashtable[key];
					}
					else
					{
						result = default(V);
					}
				}
				return result;
			}
			set
			{
				lock (this.m_sync)
				{
					this.m_hashtable[key] = value;
				}
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00045938 File Offset: 0x00043B38
		internal bool ContainsKey(K k)
		{
			bool result;
			lock (this.m_sync)
			{
				result = this.m_hashtable.ContainsKey(k);
			}
			return result;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00045980 File Offset: 0x00043B80
		internal List<K> GetKeys()
		{
			List<K> result;
			lock (this.m_sync)
			{
				List<K> list = new List<K>(this.m_hashtable.Keys.Count);
				foreach (K item in this.m_hashtable.Keys)
				{
					list.Add(item);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00045A20 File Offset: 0x00043C20
		internal List<V> GetValues()
		{
			List<V> result;
			lock (this.m_sync)
			{
				List<V> list = new List<V>(this.m_hashtable.Values.Count);
				foreach (V item in this.m_hashtable.Values)
				{
					list.Add(item);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00045AC0 File Offset: 0x00043CC0
		public IEnumerator GetEnumerator()
		{
			return this.m_hashtable.GetEnumerator();
		}

		// Token: 0x04000A20 RID: 2592
		private object m_sync;

		// Token: 0x04000A21 RID: 2593
		private Dictionary<K, V> m_hashtable;
	}
}
