using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200046F RID: 1135
	internal class SequentialMessageFilterTable<FilterData> : IMessageFilterTable<FilterData>, IDictionary<MessageFilter, FilterData>, ICollection<KeyValuePair<MessageFilter, FilterData>>, IEnumerable<KeyValuePair<MessageFilter, FilterData>>, IEnumerable
	{
		// Token: 0x06002BFC RID: 11260 RVA: 0x000AC4AD File Offset: 0x000AA6AD
		public SequentialMessageFilterTable()
		{
			this.filters = new Dictionary<MessageFilter, FilterData>();
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000AC4C0 File Offset: 0x000AA6C0
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000AC4CD File Offset: 0x000AA6CD
		public void Clear()
		{
			this.filters.Clear();
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000AC4DC File Offset: 0x000AA6DC
		public bool GetMatchingValue(Message message, out FilterData data)
		{
			bool flag = false;
			MessageFilter item = null;
			data = default(FilterData);
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(message))
				{
					if (flag)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(item);
						collection.Add(keyValuePair.Key);
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
					}
					item = keyValuePair.Key;
					data = keyValuePair.Value;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000AC594 File Offset: 0x000AA794
		public bool GetMatchingValue(MessageBuffer buffer, out FilterData data)
		{
			bool flag = false;
			MessageFilter item = null;
			data = default(FilterData);
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(buffer))
				{
					if (flag)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(item);
						collection.Add(keyValuePair.Key);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
					}
					item = keyValuePair.Key;
					data = keyValuePair.Value;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000AC650 File Offset: 0x000AA850
		public bool GetMatchingValues(Message message, ICollection<FilterData> results)
		{
			int count = results.Count;
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(message))
				{
					results.Add(keyValuePair.Value);
				}
			}
			return count != results.Count;
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000AC6CC File Offset: 0x000AA8CC
		public bool GetMatchingValues(MessageBuffer buffer, ICollection<FilterData> results)
		{
			int count = results.Count;
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(buffer))
				{
					results.Add(keyValuePair.Value);
				}
			}
			return count != results.Count;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000AC748 File Offset: 0x000AA948
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			filter = null;
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(message))
				{
					if (filter != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filter);
						collection.Add(keyValuePair.Key);
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
					}
					filter = keyValuePair.Key;
				}
			}
			return filter != null;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000AC7EC File Offset: 0x000AA9EC
		public bool GetMatchingFilter(MessageBuffer buffer, out MessageFilter filter)
		{
			filter = null;
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(buffer))
				{
					if (filter != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filter);
						collection.Add(keyValuePair.Key);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
					}
					filter = keyValuePair.Key;
				}
			}
			return filter != null;
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000AC894 File Offset: 0x000AAA94
		public bool GetMatchingFilters(Message message, ICollection<MessageFilter> results)
		{
			int count = results.Count;
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(message))
				{
					results.Add(keyValuePair.Key);
				}
			}
			return count != results.Count;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000AC910 File Offset: 0x000AAB10
		public bool GetMatchingFilters(MessageBuffer buffer, ICollection<MessageFilter> results)
		{
			int count = results.Count;
			foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.filters)
			{
				if (keyValuePair.Key.Match(buffer))
				{
					results.Add(keyValuePair.Key);
				}
			}
			return count != results.Count;
		}

		// Token: 0x17000AA2 RID: 2722
		public FilterData this[MessageFilter key]
		{
			get
			{
				return this.filters[key];
			}
			set
			{
				this.filters[key] = value;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000AC9A9 File Offset: 0x000AABA9
		public ICollection<MessageFilter> Keys
		{
			get
			{
				return this.filters.Keys;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000AC9B6 File Offset: 0x000AABB6
		public ICollection<FilterData> Values
		{
			get
			{
				return this.filters.Values;
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000AC9C3 File Offset: 0x000AABC3
		public bool ContainsKey(MessageFilter key)
		{
			return this.filters.ContainsKey(key);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000AC9D1 File Offset: 0x000AABD1
		public void Add(MessageFilter key, FilterData value)
		{
			this.filters.Add(key, value);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000AC9E0 File Offset: 0x000AABE0
		public bool Remove(MessageFilter key)
		{
			return this.filters.Remove(key);
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000AC9EE File Offset: 0x000AABEE
		bool ICollection<KeyValuePair<MessageFilter, !0>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000AC9F1 File Offset: 0x000AABF1
		void ICollection<KeyValuePair<MessageFilter, !0>>.Add(KeyValuePair<MessageFilter, FilterData> item)
		{
			((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).Add(item);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000AC9FF File Offset: 0x000AABFF
		bool ICollection<KeyValuePair<MessageFilter, !0>>.Contains(KeyValuePair<MessageFilter, FilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).Contains(item);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000ACA0D File Offset: 0x000AAC0D
		void ICollection<KeyValuePair<MessageFilter, !0>>.CopyTo(KeyValuePair<MessageFilter, FilterData>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).CopyTo(array, arrayIndex);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000ACA1C File Offset: 0x000AAC1C
		bool ICollection<KeyValuePair<MessageFilter, !0>>.Remove(KeyValuePair<MessageFilter, FilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).Remove(item);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000ACA2A File Offset: 0x000AAC2A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<MessageFilter, FilterData>>)this).GetEnumerator();
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000ACA32 File Offset: 0x000AAC32
		IEnumerator<KeyValuePair<MessageFilter, FilterData>> IEnumerable<KeyValuePair<MessageFilter, !0>>.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<MessageFilter, FilterData>>)this.filters).GetEnumerator();
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000ACA3F File Offset: 0x000AAC3F
		public bool TryGetValue(MessageFilter filter, out FilterData data)
		{
			return this.filters.TryGetValue(filter, out data);
		}

		// Token: 0x04002437 RID: 9271
		private Dictionary<MessageFilter, FilterData> filters;
	}
}
