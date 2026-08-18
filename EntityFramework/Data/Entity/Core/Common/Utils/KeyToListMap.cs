using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000329 RID: 809
	internal class KeyToListMap<TKey, TValue> : InternalBase
	{
		// Token: 0x06001BD9 RID: 7129 RVA: 0x00088F28 File Offset: 0x00087128
		internal KeyToListMap(IEqualityComparer<TKey> comparer)
		{
			this.m_map = new Dictionary<TKey, List<TValue>>(comparer);
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00088F3C File Offset: 0x0008713C
		internal IEnumerable<TKey> Keys
		{
			get
			{
				return this.m_map.Keys;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0008916C File Offset: 0x0008736C
		internal IEnumerable<TValue> AllValues
		{
			get
			{
				foreach (TKey key in this.Keys)
				{
					foreach (TValue value in this.ListForKey(key))
					{
						yield return value;
					}
				}
				yield break;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00089189 File Offset: 0x00087389
		internal IEnumerable<KeyValuePair<TKey, List<TValue>>> KeyValuePairs
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00089191 File Offset: 0x00087391
		internal bool ContainsKey(TKey key)
		{
			return this.m_map.ContainsKey(key);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000891A0 File Offset: 0x000873A0
		internal void Add(TKey key, TValue value)
		{
			List<TValue> list;
			if (!this.m_map.TryGetValue(key, out list))
			{
				list = new List<TValue>();
				this.m_map[key] = list;
			}
			list.Add(value);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000891D8 File Offset: 0x000873D8
		internal void AddRange(TKey key, IEnumerable<TValue> values)
		{
			foreach (TValue value in values)
			{
				this.Add(key, value);
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00089224 File Offset: 0x00087424
		internal bool RemoveKey(TKey key)
		{
			return this.m_map.Remove(key);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00089232 File Offset: 0x00087432
		internal ReadOnlyCollection<TValue> ListForKey(TKey key)
		{
			return new ReadOnlyCollection<TValue>(this.m_map[key]);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00089248 File Offset: 0x00087448
		internal bool TryGetListForKey(TKey key, out ReadOnlyCollection<TValue> valueCollection)
		{
			valueCollection = null;
			List<TValue> list;
			if (this.m_map.TryGetValue(key, out list))
			{
				valueCollection = new ReadOnlyCollection<TValue>(list);
				return true;
			}
			return false;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00089428 File Offset: 0x00087628
		internal IEnumerable<TValue> EnumerateValues(TKey key)
		{
			List<TValue> values;
			if (this.m_map.TryGetValue(key, out values))
			{
				foreach (TValue value in values)
				{
					yield return value;
				}
			}
			yield break;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0008944C File Offset: 0x0008764C
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (TKey tkey in this.Keys)
			{
				StringUtil.FormatStringBuilder(builder, "{0}", new object[]
				{
					tkey
				});
				builder.Append(": ");
				IEnumerable<TValue> list = this.ListForKey(tkey);
				StringUtil.ToSeparatedString(builder, list, ",", "null");
				builder.Append("; ");
			}
		}

		// Token: 0x040009BA RID: 2490
		private readonly Dictionary<TKey, List<TValue>> m_map;
	}
}
