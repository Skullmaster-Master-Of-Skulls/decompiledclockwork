using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FF RID: 511
	internal class Lookup<TKey, TElement> : ILookup<TKey, TElement>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x000394FB File Offset: 0x000376FB
		internal Lookup(IEqualityComparer<TKey> comparer)
		{
			this.m_comparer = comparer;
			this.m_dict = new Dictionary<TKey, IGrouping<TKey, TElement>>(this.m_comparer);
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003951C File Offset: 0x0003771C
		public int Count
		{
			get
			{
				int num = this.m_dict.Count;
				if (this.m_defaultKeyGrouping != null)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170002D8 RID: 728
		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				if (this.m_comparer.Equals(key, default(TKey)))
				{
					if (this.m_defaultKeyGrouping != null)
					{
						return this.m_defaultKeyGrouping;
					}
					return Enumerable.Empty<TElement>();
				}
				else
				{
					IGrouping<TKey, TElement> result;
					if (this.m_dict.TryGetValue(key, out result))
					{
						return result;
					}
					return Enumerable.Empty<TElement>();
				}
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00039594 File Offset: 0x00037794
		public bool Contains(TKey key)
		{
			if (this.m_comparer.Equals(key, default(TKey)))
			{
				return this.m_defaultKeyGrouping != null;
			}
			return this.m_dict.ContainsKey(key);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000395D0 File Offset: 0x000377D0
		internal void Add(IGrouping<TKey, TElement> grouping)
		{
			if (this.m_comparer.Equals(grouping.Key, default(TKey)))
			{
				this.m_defaultKeyGrouping = grouping;
				return;
			}
			this.m_dict.Add(grouping.Key, grouping);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00039613 File Offset: 0x00037813
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			foreach (IGrouping<TKey, TElement> grouping in this.m_dict.Values)
			{
				yield return grouping;
			}
			IEnumerator<IGrouping<TKey, TElement>> enumerator = null;
			if (this.m_defaultKeyGrouping != null)
			{
				yield return this.m_defaultKeyGrouping;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00039622 File Offset: 0x00037822
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<IGrouping<TKey, TElement>>)this).GetEnumerator();
		}

		// Token: 0x04000935 RID: 2357
		private IDictionary<TKey, IGrouping<TKey, TElement>> m_dict;

		// Token: 0x04000936 RID: 2358
		private IEqualityComparer<TKey> m_comparer;

		// Token: 0x04000937 RID: 2359
		private IGrouping<TKey, TElement> m_defaultKeyGrouping;
	}
}
