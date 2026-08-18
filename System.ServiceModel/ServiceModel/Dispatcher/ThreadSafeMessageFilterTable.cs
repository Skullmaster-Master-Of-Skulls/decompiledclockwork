using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000477 RID: 1143
	internal class ThreadSafeMessageFilterTable<FilterData> : IMessageFilterTable<FilterData>, IDictionary<MessageFilter, FilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x06002C6D RID: 11373 RVA: 0x000AD8A0 File Offset: 0x000ABAA0
		internal ThreadSafeMessageFilterTable()
		{
			this.table = new MessageFilterTable<FilterData>();
			this.syncRoot = new object();
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x000AD8BE File Offset: 0x000ABABE
		internal object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000AD8C8 File Offset: 0x000ABAC8
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000AD910 File Offset: 0x000ABB10
		public int DefaultPriority
		{
			get
			{
				object obj = this.syncRoot;
				int defaultPriority;
				lock (obj)
				{
					defaultPriority = this.table.DefaultPriority;
				}
				return defaultPriority;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.table.DefaultPriority = value;
				}
			}
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000AD958 File Offset: 0x000ABB58
		internal void Add(MessageFilter filter, FilterData data, int priority)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.table.Add(filter, data, priority);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000AD9A0 File Offset: 0x000ABBA0
		public int Count
		{
			get
			{
				object obj = this.syncRoot;
				int count;
				lock (obj)
				{
					count = this.table.Count;
				}
				return count;
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000AD9E8 File Offset: 0x000ABBE8
		public void Clear()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.table.Clear();
			}
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000ADA30 File Offset: 0x000ABC30
		public bool GetMatchingValue(Message message, out FilterData data)
		{
			object obj = this.syncRoot;
			bool matchingValue;
			lock (obj)
			{
				matchingValue = this.table.GetMatchingValue(message, out data);
			}
			return matchingValue;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000ADA7C File Offset: 0x000ABC7C
		public bool GetMatchingValue(MessageBuffer buffer, out FilterData data)
		{
			object obj = this.syncRoot;
			bool matchingValue;
			lock (obj)
			{
				matchingValue = this.table.GetMatchingValue(buffer, out data);
			}
			return matchingValue;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000ADAC8 File Offset: 0x000ABCC8
		public bool GetMatchingValues(Message message, ICollection<FilterData> results)
		{
			object obj = this.syncRoot;
			bool matchingValues;
			lock (obj)
			{
				matchingValues = this.table.GetMatchingValues(message, results);
			}
			return matchingValues;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000ADB14 File Offset: 0x000ABD14
		public bool GetMatchingValues(MessageBuffer buffer, ICollection<FilterData> results)
		{
			object obj = this.syncRoot;
			bool matchingValues;
			lock (obj)
			{
				matchingValues = this.table.GetMatchingValues(buffer, results);
			}
			return matchingValues;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000ADB60 File Offset: 0x000ABD60
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			object obj = this.syncRoot;
			bool matchingFilter;
			lock (obj)
			{
				matchingFilter = this.table.GetMatchingFilter(message, out filter);
			}
			return matchingFilter;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000ADBAC File Offset: 0x000ABDAC
		public bool GetMatchingFilter(MessageBuffer buffer, out MessageFilter filter)
		{
			object obj = this.syncRoot;
			bool matchingFilter;
			lock (obj)
			{
				matchingFilter = this.table.GetMatchingFilter(buffer, out filter);
			}
			return matchingFilter;
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000ADBF8 File Offset: 0x000ABDF8
		public bool GetMatchingFilters(Message message, ICollection<MessageFilter> results)
		{
			object obj = this.syncRoot;
			bool matchingFilters;
			lock (obj)
			{
				matchingFilters = this.table.GetMatchingFilters(message, results);
			}
			return matchingFilters;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000ADC44 File Offset: 0x000ABE44
		public bool GetMatchingFilters(MessageBuffer buffer, ICollection<MessageFilter> results)
		{
			object obj = this.syncRoot;
			bool matchingFilters;
			lock (obj)
			{
				matchingFilters = this.table.GetMatchingFilters(buffer, results);
			}
			return matchingFilters;
		}

		// Token: 0x17000AB7 RID: 2743
		public FilterData this[MessageFilter key]
		{
			get
			{
				object obj = this.syncRoot;
				FilterData result;
				lock (obj)
				{
					result = this.table[key];
				}
				return result;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.table[key] = value;
				}
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000ADD20 File Offset: 0x000ABF20
		public ICollection<MessageFilter> Keys
		{
			get
			{
				object obj = this.syncRoot;
				ICollection<MessageFilter> keys;
				lock (obj)
				{
					keys = this.table.Keys;
				}
				return keys;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000ADD68 File Offset: 0x000ABF68
		public ICollection<FilterData> Values
		{
			get
			{
				object obj = this.syncRoot;
				ICollection<FilterData> values;
				lock (obj)
				{
					values = this.table.Values;
				}
				return values;
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000ADDB0 File Offset: 0x000ABFB0
		public bool ContainsKey(MessageFilter key)
		{
			object obj = this.syncRoot;
			bool result;
			lock (obj)
			{
				result = this.table.ContainsKey(key);
			}
			return result;
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000ADDF8 File Offset: 0x000ABFF8
		public void Add(MessageFilter key, FilterData value)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.table.Add(key, value);
			}
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000ADE40 File Offset: 0x000AC040
		public bool Remove(MessageFilter key)
		{
			object obj = this.syncRoot;
			bool result;
			lock (obj)
			{
				result = this.table.Remove(key);
			}
			return result;
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000ADE88 File Offset: 0x000AC088
		bool ICollection<KeyValuePair<MessageFilter, !0>>.IsReadOnly
		{
			get
			{
				object obj = this.syncRoot;
				bool isReadOnly;
				lock (obj)
				{
					isReadOnly = ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.table).IsReadOnly;
				}
				return isReadOnly;
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000ADED0 File Offset: 0x000AC0D0
		void ICollection<KeyValuePair<MessageFilter, !0>>.Add(KeyValuePair<MessageFilter, FilterData> item)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.table).Add(item);
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000ADF18 File Offset: 0x000AC118
		bool ICollection<KeyValuePair<MessageFilter, !0>>.Contains(KeyValuePair<MessageFilter, FilterData> item)
		{
			object obj = this.syncRoot;
			bool result;
			lock (obj)
			{
				result = ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.table).Contains(item);
			}
			return result;
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000ADF60 File Offset: 0x000AC160
		void ICollection<KeyValuePair<MessageFilter, !0>>.CopyTo(KeyValuePair<MessageFilter, FilterData>[] array, int arrayIndex)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.table).CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000ADFA8 File Offset: 0x000AC1A8
		bool ICollection<KeyValuePair<MessageFilter, !0>>.Remove(KeyValuePair<MessageFilter, FilterData> item)
		{
			object obj = this.syncRoot;
			bool result;
			lock (obj)
			{
				result = ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.table).Remove(item);
			}
			return result;
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000ADFF0 File Offset: 0x000AC1F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			object obj = this.syncRoot;
			IEnumerator enumerator;
			lock (obj)
			{
				enumerator = ((IEnumerable<KeyValuePair<MessageFilter, FilterData>>)this).GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000AE034 File Offset: 0x000AC234
		IEnumerator<KeyValuePair<MessageFilter, FilterData>> IEnumerable<KeyValuePair<MessageFilter, !0>>.GetEnumerator()
		{
			object obj = this.syncRoot;
			IEnumerator<KeyValuePair<MessageFilter, FilterData>> enumerator;
			lock (obj)
			{
				enumerator = ((IEnumerable<KeyValuePair<MessageFilter, FilterData>>)this.table).GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000AE07C File Offset: 0x000AC27C
		public bool TryGetValue(MessageFilter filter, out FilterData data)
		{
			object obj = this.syncRoot;
			bool result;
			lock (obj)
			{
				result = this.table.TryGetValue(filter, out data);
			}
			return result;
		}

		// Token: 0x04002441 RID: 9281
		private MessageFilterTable<FilterData> table;

		// Token: 0x04002442 RID: 9282
		private object syncRoot;
	}
}
