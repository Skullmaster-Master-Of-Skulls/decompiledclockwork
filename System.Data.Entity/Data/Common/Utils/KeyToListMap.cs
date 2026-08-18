using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000395 RID: 917
	internal class KeyToListMap<TKey, TValue> : InternalBase
	{
		// Token: 0x060032A4 RID: 12964 RVA: 0x000C5CE8 File Offset: 0x000C3EE8
		internal KeyToListMap(IEqualityComparer<TKey> comparer)
		{
			this.m_map = new Dictionary<TKey, List<TValue>>(comparer);
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000C5CFC File Offset: 0x000C3EFC
		internal IEnumerable<TKey> Keys
		{
			get
			{
				return this.m_map.Keys;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000C5D0C File Offset: 0x000C3F0C
		internal IEnumerable<TValue> AllValues
		{
			get
			{
				foreach (TKey key in this.Keys)
				{
					foreach (TValue tvalue in this.ListForKey(key))
					{
						yield return tvalue;
					}
					IEnumerator<TValue> enumerator2 = null;
				}
				IEnumerator<TKey> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000C5D29 File Offset: 0x000C3F29
		internal IEnumerable<KeyValuePair<TKey, List<TValue>>> KeyValuePairs
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000C5D31 File Offset: 0x000C3F31
		internal bool ContainsKey(TKey key)
		{
			return this.m_map.ContainsKey(key);
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000C5D40 File Offset: 0x000C3F40
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

		// Token: 0x060032AA RID: 12970 RVA: 0x000C5D78 File Offset: 0x000C3F78
		internal void AddRange(TKey key, IEnumerable<TValue> values)
		{
			foreach (TValue value in values)
			{
				this.Add(key, value);
			}
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000C5DC4 File Offset: 0x000C3FC4
		internal bool RemoveKey(TKey key)
		{
			return this.m_map.Remove(key);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000C5DD2 File Offset: 0x000C3FD2
		internal ReadOnlyCollection<TValue> ListForKey(TKey key)
		{
			return new ReadOnlyCollection<TValue>(this.m_map[key]);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000C5DE8 File Offset: 0x000C3FE8
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

		// Token: 0x060032AE RID: 12974 RVA: 0x000C5E13 File Offset: 0x000C4013
		internal IEnumerable<TValue> EnumerateValues(TKey key)
		{
			List<TValue> list;
			if (this.m_map.TryGetValue(key, out list))
			{
				foreach (TValue tvalue in list)
				{
					yield return tvalue;
				}
				List<TValue>.Enumerator enumerator = default(List<TValue>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000C5E2C File Offset: 0x000C402C
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

		// Token: 0x04001662 RID: 5730
		private Dictionary<TKey, List<TValue>> m_map;
	}
}
