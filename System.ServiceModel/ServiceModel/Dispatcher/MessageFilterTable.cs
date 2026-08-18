using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000471 RID: 1137
	[DataContract]
	public class MessageFilterTable<TFilterData> : IMessageFilterTable<TFilterData>, IDictionary<MessageFilter, TFilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x06002C1D RID: 11293 RVA: 0x000ACAA1 File Offset: 0x000AACA1
		public MessageFilterTable() : this(0)
		{
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000ACAAA File Offset: 0x000AACAA
		public MessageFilterTable(int defaultPriority)
		{
			this.Init(defaultPriority);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000ACAB9 File Offset: 0x000AACB9
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Init(0);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000ACAC2 File Offset: 0x000AACC2
		private void Init(int defaultPriority)
		{
			this.CreateEmptyTables();
			this.defaultPriority = defaultPriority;
		}

		// Token: 0x17000AA7 RID: 2727
		public TFilterData this[MessageFilter filter]
		{
			get
			{
				return this.filters[filter];
			}
			set
			{
				if (this.ContainsKey(filter))
				{
					int priority = this.GetPriority(filter);
					this.Remove(filter);
					this.Add(filter, value, priority);
					return;
				}
				this.Add(filter, value, this.defaultPriority);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000ACB1E File Offset: 0x000AAD1E
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x000ACB2B File Offset: 0x000AAD2B
		// (set) Token: 0x06002C25 RID: 11301 RVA: 0x000ACB33 File Offset: 0x000AAD33
		[DataMember]
		public int DefaultPriority
		{
			get
			{
				return this.defaultPriority;
			}
			set
			{
				this.defaultPriority = value;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000ACB3C File Offset: 0x000AAD3C
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x000ACBC0 File Offset: 0x000AADC0
		[DataMember]
		private MessageFilterTable<TFilterData>.Entry[] Entries
		{
			get
			{
				MessageFilterTable<TFilterData>.Entry[] array = new MessageFilterTable<TFilterData>.Entry[this.Count];
				int num = 0;
				foreach (KeyValuePair<MessageFilter, TFilterData> keyValuePair in this.filters)
				{
					array[num++] = new MessageFilterTable<TFilterData>.Entry(keyValuePair.Key, keyValuePair.Value, this.GetPriority(keyValuePair.Key));
				}
				return array;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					MessageFilterTable<TFilterData>.Entry entry = value[i];
					this.Add(entry.filter, entry.data, entry.priority);
				}
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000ACBF7 File Offset: 0x000AADF7
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x000ACBFA File Offset: 0x000AADFA
		public ICollection<MessageFilter> Keys
		{
			get
			{
				return this.filters.Keys;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000ACC07 File Offset: 0x000AAE07
		public ICollection<TFilterData> Values
		{
			get
			{
				return this.filters.Values;
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000ACC14 File Offset: 0x000AAE14
		public void Add(MessageFilter filter, TFilterData data)
		{
			this.Add(filter, data, this.defaultPriority);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000ACC24 File Offset: 0x000AAE24
		public void Add(MessageFilter filter, TFilterData data, int priority)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			if (this.filters.ContainsKey(filter))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("filter", SR.GetString("FilterExists"));
			}
			Type type = filter.GetType();
			Type o = null;
			IMessageFilterTable<TFilterData> messageFilterTable = null;
			if (this.filterTypeMappings.TryGetValue(type, out o))
			{
				for (int i = 0; i < this.tables.Count; i++)
				{
					if (this.tables[i].priority == priority && this.tables[i].table.GetType().Equals(o))
					{
						messageFilterTable = this.tables[i].table;
						break;
					}
				}
				if (messageFilterTable == null)
				{
					messageFilterTable = this.CreateFilterTable(filter);
					this.ValidateTable(messageFilterTable);
					if (!messageFilterTable.GetType().Equals(o))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FilterTableTypeMismatch")));
					}
					messageFilterTable.Add(filter, data);
					this.tables.Add(new MessageFilterTable<TFilterData>.FilterTableEntry(priority, messageFilterTable));
				}
				else
				{
					messageFilterTable.Add(filter, data);
				}
			}
			else
			{
				messageFilterTable = this.CreateFilterTable(filter);
				this.ValidateTable(messageFilterTable);
				this.filterTypeMappings.Add(type, messageFilterTable.GetType());
				MessageFilterTable<TFilterData>.FilterTableEntry item = new MessageFilterTable<TFilterData>.FilterTableEntry(priority, messageFilterTable);
				int num = this.tables.IndexOf(item);
				if (num >= 0)
				{
					messageFilterTable = this.tables[num].table;
				}
				else
				{
					this.tables.Add(item);
				}
				messageFilterTable.Add(filter, data);
			}
			this.filters.Add(filter, data);
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000ACDBD File Offset: 0x000AAFBD
		public void Add(KeyValuePair<MessageFilter, TFilterData> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000ACDD3 File Offset: 0x000AAFD3
		public void Clear()
		{
			this.filters.Clear();
			this.tables.Clear();
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000ACDEB File Offset: 0x000AAFEB
		public bool Contains(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000ACDF9 File Offset: 0x000AAFF9
		public bool ContainsKey(MessageFilter filter)
		{
			return this.filters.ContainsKey(filter);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000ACE07 File Offset: 0x000AB007
		public void CopyTo(KeyValuePair<MessageFilter, TFilterData>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).CopyTo(array, arrayIndex);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000ACE16 File Offset: 0x000AB016
		private void CreateEmptyTables()
		{
			this.filterTypeMappings = new Dictionary<Type, Type>();
			this.filters = new Dictionary<MessageFilter, TFilterData>();
			this.tables = new SortedBuffer<MessageFilterTable<TFilterData>.FilterTableEntry, MessageFilterTable<TFilterData>.TableEntryComparer>(MessageFilterTable<TFilterData>.staticComparerInstance);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000ACE40 File Offset: 0x000AB040
		protected virtual IMessageFilterTable<TFilterData> CreateFilterTable(MessageFilter filter)
		{
			IMessageFilterTable<TFilterData> messageFilterTable = filter.CreateFilterTable<TFilterData>();
			if (messageFilterTable == null)
			{
				return new SequentialMessageFilterTable<TFilterData>();
			}
			return messageFilterTable;
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000ACE5E File Offset: 0x000AB05E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000ACE66 File Offset: 0x000AB066
		public IEnumerator<KeyValuePair<MessageFilter, TFilterData>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<MessageFilter, TFilterData>>)this.filters).GetEnumerator();
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000ACE74 File Offset: 0x000AB074
		public int GetPriority(MessageFilter filter)
		{
			TFilterData tfilterData = this.filters[filter];
			for (int i = 0; i < this.tables.Count; i++)
			{
				if (this.tables[i].table.ContainsKey(filter))
				{
					return this.tables[i].priority;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("FilterTableInvalidForLookup")));
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000ACEE8 File Offset: 0x000AB0E8
		public bool GetMatchingValue(Message message, out TFilterData data)
		{
			bool flag = false;
			int num = int.MinValue;
			data = default(TFilterData);
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || !flag))
			{
				num = this.tables[num2].priority;
				TFilterData tfilterData;
				if (this.tables[num2].table.GetMatchingValue(message, out tfilterData))
				{
					if (flag)
					{
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, null), message);
					}
					data = tfilterData;
					flag = true;
				}
				num2++;
			}
			return flag;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000ACF84 File Offset: 0x000AB184
		internal bool GetMatchingValue(Message message, out TFilterData data, out bool addressMatched)
		{
			bool flag = false;
			int num = int.MinValue;
			data = default(TFilterData);
			addressMatched = false;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || !flag))
			{
				num = this.tables[num2].priority;
				IMessageFilterTable<TFilterData> table = this.tables[num2].table;
				AndMessageFilterTable<TFilterData> andMessageFilterTable = table as AndMessageFilterTable<TFilterData>;
				TFilterData tfilterData;
				bool matchingValue;
				if (andMessageFilterTable != null)
				{
					bool flag2;
					matchingValue = andMessageFilterTable.GetMatchingValue(message, out tfilterData, out flag2);
					addressMatched = (addressMatched || flag2);
				}
				else
				{
					matchingValue = table.GetMatchingValue(message, out tfilterData);
				}
				if (matchingValue)
				{
					if (flag)
					{
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, null), message);
					}
					addressMatched = true;
					data = tfilterData;
					flag = true;
				}
				num2++;
			}
			return flag;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000AD059 File Offset: 0x000AB259
		public bool GetMatchingValue(MessageBuffer buffer, out TFilterData data)
		{
			return this.GetMatchingValue(buffer, null, out data);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000AD064 File Offset: 0x000AB264
		internal bool GetMatchingValue(MessageBuffer buffer, Message messageToReadHeaders, out TFilterData data)
		{
			bool flag = false;
			int num = int.MinValue;
			data = default(TFilterData);
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || !flag))
			{
				num = this.tables[num2].priority;
				TFilterData tfilterData;
				bool matchingValue;
				if (messageToReadHeaders != null && this.tables[num2].table is ActionMessageFilterTable<TFilterData>)
				{
					matchingValue = this.tables[num2].table.GetMatchingValue(messageToReadHeaders, out tfilterData);
				}
				else
				{
					matchingValue = this.tables[num2].table.GetMatchingValue(buffer, out tfilterData);
				}
				if (matchingValue)
				{
					if (flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, null));
					}
					data = tfilterData;
					flag = true;
				}
				num2++;
			}
			return flag;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000AD14C File Offset: 0x000AB34C
		public bool GetMatchingValues(Message message, ICollection<TFilterData> results)
		{
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			int num = int.MinValue;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || count == results.Count))
			{
				num = this.tables[num2].priority;
				this.tables[num2].table.GetMatchingValues(message, results);
				num2++;
			}
			return count != results.Count;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000AD1E4 File Offset: 0x000AB3E4
		public bool GetMatchingValues(MessageBuffer buffer, ICollection<TFilterData> results)
		{
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			int num = int.MinValue;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || count == results.Count))
			{
				num = this.tables[num2].priority;
				this.tables[num2].table.GetMatchingValues(buffer, results);
				num2++;
			}
			return count != results.Count;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000AD27C File Offset: 0x000AB47C
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			int num = int.MinValue;
			filter = null;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || filter == null))
			{
				num = this.tables[num2].priority;
				MessageFilter messageFilter;
				if (this.tables[num2].table.GetMatchingFilter(message, out messageFilter))
				{
					if (filter != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filter);
						collection.Add(messageFilter);
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
					}
					filter = messageFilter;
				}
				num2++;
			}
			return filter != null;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000AD32C File Offset: 0x000AB52C
		public bool GetMatchingFilter(MessageBuffer buffer, out MessageFilter filter)
		{
			int num = int.MinValue;
			filter = null;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || filter == null))
			{
				num = this.tables[num2].priority;
				MessageFilter messageFilter;
				if (this.tables[num2].table.GetMatchingFilter(buffer, out messageFilter))
				{
					if (filter != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filter);
						collection.Add(messageFilter);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
					}
					filter = messageFilter;
				}
				num2++;
			}
			return filter != null;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000AD3E4 File Offset: 0x000AB5E4
		public bool GetMatchingFilters(Message message, ICollection<MessageFilter> results)
		{
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			int num = int.MinValue;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || count == results.Count))
			{
				num = this.tables[num2].priority;
				this.tables[num2].table.GetMatchingFilters(message, results);
				num2++;
			}
			return count != results.Count;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000AD47C File Offset: 0x000AB67C
		public bool GetMatchingFilters(MessageBuffer buffer, ICollection<MessageFilter> results)
		{
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			int num = int.MinValue;
			int num2 = 0;
			while (num2 < this.tables.Count && (num <= this.tables[num2].priority || count == results.Count))
			{
				num = this.tables[num2].priority;
				this.tables[num2].table.GetMatchingFilters(buffer, results);
				num2++;
			}
			return count != results.Count;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000AD514 File Offset: 0x000AB714
		public bool Remove(MessageFilter filter)
		{
			for (int i = 0; i < this.tables.Count; i++)
			{
				if (this.tables[i].table.Remove(filter))
				{
					if (this.tables[i].table.Count == 0)
					{
						this.tables.RemoveAt(i);
					}
					return this.filters.Remove(filter);
				}
			}
			return false;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000AD582 File Offset: 0x000AB782
		public bool Remove(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000AD5A1 File Offset: 0x000AB7A1
		public bool TryGetValue(MessageFilter filter, out TFilterData data)
		{
			return this.filters.TryGetValue(filter, out data);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000AD5B0 File Offset: 0x000AB7B0
		private void ValidateTable(IMessageFilterTable<TFilterData> table)
		{
			Type type = base.GetType();
			if (type.IsInstanceOfType(table))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FilterBadTableType")));
			}
		}

		// Token: 0x04002439 RID: 9273
		private Dictionary<Type, Type> filterTypeMappings;

		// Token: 0x0400243A RID: 9274
		private Dictionary<MessageFilter, TFilterData> filters;

		// Token: 0x0400243B RID: 9275
		private SortedBuffer<MessageFilterTable<TFilterData>.FilterTableEntry, MessageFilterTable<TFilterData>.TableEntryComparer> tables;

		// Token: 0x0400243C RID: 9276
		private int defaultPriority;

		// Token: 0x0400243D RID: 9277
		private static readonly MessageFilterTable<TFilterData>.TableEntryComparer staticComparerInstance = new MessageFilterTable<TFilterData>.TableEntryComparer();

		// Token: 0x02000C40 RID: 3136
		private struct FilterTableEntry
		{
			// Token: 0x0600775A RID: 30554 RVA: 0x001BDDBB File Offset: 0x001BBFBB
			internal FilterTableEntry(int pri, IMessageFilterTable<TFilterData> t)
			{
				this.priority = pri;
				this.table = t;
			}

			// Token: 0x04004449 RID: 17481
			internal IMessageFilterTable<TFilterData> table;

			// Token: 0x0400444A RID: 17482
			internal int priority;
		}

		// Token: 0x02000C41 RID: 3137
		private class TableEntryComparer : IComparer<MessageFilterTable<TFilterData>.FilterTableEntry>
		{
			// Token: 0x0600775C RID: 30556 RVA: 0x001BDDD4 File Offset: 0x001BBFD4
			public int Compare(MessageFilterTable<TFilterData>.FilterTableEntry x, MessageFilterTable<TFilterData>.FilterTableEntry y)
			{
				int num = y.priority.CompareTo(x.priority);
				if (num != 0)
				{
					return num;
				}
				return x.table.GetType().FullName.CompareTo(y.table.GetType().FullName);
			}

			// Token: 0x0600775D RID: 30557 RVA: 0x001BDE20 File Offset: 0x001BC020
			public bool Equals(MessageFilterTable<TFilterData>.FilterTableEntry x, MessageFilterTable<TFilterData>.FilterTableEntry y)
			{
				int num = y.priority.CompareTo(x.priority);
				return num == 0 && x.table.GetType().FullName.Equals(y.table.GetType().FullName);
			}

			// Token: 0x0600775E RID: 30558 RVA: 0x001BDE6A File Offset: 0x001BC06A
			public int GetHashCode(MessageFilterTable<TFilterData>.FilterTableEntry table)
			{
				return table.GetHashCode();
			}
		}

		// Token: 0x02000C42 RID: 3138
		[DataContract]
		private class Entry
		{
			// Token: 0x0600775F RID: 30559 RVA: 0x001BDE79 File Offset: 0x001BC079
			internal Entry(MessageFilter f, TFilterData d, int p)
			{
				this.filter = f;
				this.data = d;
				this.priority = p;
			}

			// Token: 0x0400444B RID: 17483
			[DataMember(IsRequired = true)]
			internal MessageFilter filter;

			// Token: 0x0400444C RID: 17484
			[DataMember(IsRequired = true)]
			internal TFilterData data;

			// Token: 0x0400444D RID: 17485
			[DataMember(IsRequired = true)]
			internal int priority;
		}
	}
}
