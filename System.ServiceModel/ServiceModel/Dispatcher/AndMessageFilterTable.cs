using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000463 RID: 1123
	internal class AndMessageFilterTable<FilterData> : IMessageFilterTable<FilterData>, IDictionary<MessageFilter, FilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x06002B75 RID: 11125 RVA: 0x000AA2E9 File Offset: 0x000A84E9
		public AndMessageFilterTable()
		{
			this.filters = new Dictionary<MessageFilter, FilterData>();
			this.filterData = new Dictionary<MessageFilter, AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table = new MessageFilterTable<AndMessageFilterTable<FilterData>.FilterDataPair>();
		}

		// Token: 0x17000A90 RID: 2704
		public FilterData this[MessageFilter filter]
		{
			get
			{
				return this.filters[filter];
			}
			set
			{
				if (this.filters.ContainsKey(filter))
				{
					this.filters[filter] = value;
					this.filterData[filter].data = value;
					return;
				}
				this.Add(filter, value);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000AA358 File Offset: 0x000A8558
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000AA365 File Offset: 0x000A8565
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000AA368 File Offset: 0x000A8568
		public ICollection<MessageFilter> Keys
		{
			get
			{
				return this.filters.Keys;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000AA375 File Offset: 0x000A8575
		public ICollection<FilterData> Values
		{
			get
			{
				return this.filters.Values;
			}
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000AA382 File Offset: 0x000A8582
		public void Add(MessageFilter filter, FilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.Add((AndMessageFilter)filter, data);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000AA3A4 File Offset: 0x000A85A4
		public void Add(KeyValuePair<MessageFilter, FilterData> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000AA3BC File Offset: 0x000A85BC
		public void Add(AndMessageFilter filter, FilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.filters.Add(filter, data);
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = new AndMessageFilterTable<FilterData>.FilterDataPair(filter, data);
			this.filterData.Add(filter, filterDataPair);
			this.table.Add(filter.Filter1, filterDataPair);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000AA410 File Offset: 0x000A8610
		public void Clear()
		{
			this.filters.Clear();
			this.filterData.Clear();
			this.table.Clear();
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000AA433 File Offset: 0x000A8633
		public bool Contains(KeyValuePair<MessageFilter, FilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).Contains(item);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000AA441 File Offset: 0x000A8641
		public bool ContainsKey(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			return this.filters.ContainsKey(filter);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000AA462 File Offset: 0x000A8662
		public void CopyTo(KeyValuePair<MessageFilter, FilterData>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).CopyTo(array, arrayIndex);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000AA471 File Offset: 0x000A8671
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000AA479 File Offset: 0x000A8679
		public IEnumerator<KeyValuePair<MessageFilter, FilterData>> GetEnumerator()
		{
			return this.filters.GetEnumerator();
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000AA48C File Offset: 0x000A868C
		private AndMessageFilterTable<FilterData>.FilterDataPair InnerMatch(Message message)
		{
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table.GetMatchingValues(message, list);
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(message))
				{
					if (filterDataPair != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filterDataPair.filter);
						collection.Add(list[i].filter);
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
					}
					filterDataPair = list[i];
				}
			}
			return filterDataPair;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000AA524 File Offset: 0x000A8724
		private AndMessageFilterTable<FilterData>.FilterDataPair InnerMatch(MessageBuffer messageBuffer)
		{
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table.GetMatchingValues(messageBuffer, list);
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(messageBuffer))
				{
					if (filterDataPair != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filterDataPair.filter);
						collection.Add(list[i].filter);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
					}
					filterDataPair = list[i];
				}
			}
			return filterDataPair;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000AA5C0 File Offset: 0x000A87C0
		private void InnerMatch(Message message, ICollection<MessageFilter> results)
		{
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table.GetMatchingValues(message, list);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(message))
				{
					results.Add(list[i].filter);
				}
			}
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000AA620 File Offset: 0x000A8820
		private void InnerMatchData(Message message, ICollection<FilterData> results)
		{
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table.GetMatchingValues(message, list);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(message))
				{
					results.Add(list[i].data);
				}
			}
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000AA680 File Offset: 0x000A8880
		private void InnerMatch(MessageBuffer messageBuffer, ICollection<MessageFilter> results)
		{
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table.GetMatchingValues(messageBuffer, list);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(messageBuffer))
				{
					results.Add(list[i].filter);
				}
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000AA6E0 File Offset: 0x000A88E0
		private void InnerMatchData(MessageBuffer messageBuffer, ICollection<FilterData> results)
		{
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			this.table.GetMatchingValues(messageBuffer, list);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(messageBuffer))
				{
					results.Add(list[i].data);
				}
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000AA740 File Offset: 0x000A8940
		internal bool GetMatchingValue(Message message, out FilterData data, out bool addressMatched)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			List<AndMessageFilterTable<FilterData>.FilterDataPair> list = new List<AndMessageFilterTable<FilterData>.FilterDataPair>();
			addressMatched = this.table.GetMatchingValues(message, list);
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].filter.Filter2.Match(message))
				{
					if (filterDataPair != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(filterDataPair.filter);
						collection.Add(list[i].filter);
						throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
					}
					filterDataPair = list[i];
				}
			}
			if (filterDataPair == null)
			{
				data = default(FilterData);
				return false;
			}
			data = filterDataPair.data;
			return true;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000AA804 File Offset: 0x000A8A04
		public bool GetMatchingValue(Message message, out FilterData data)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = this.InnerMatch(message);
			if (filterDataPair == null)
			{
				data = default(FilterData);
				return false;
			}
			data = filterDataPair.data;
			return true;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000AA848 File Offset: 0x000A8A48
		public bool GetMatchingValue(MessageBuffer messageBuffer, out FilterData data)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = this.InnerMatch(messageBuffer);
			if (filterDataPair == null)
			{
				data = default(FilterData);
				return false;
			}
			data = filterDataPair.data;
			return true;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000AA88C File Offset: 0x000A8A8C
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = this.InnerMatch(message);
			if (filterDataPair == null)
			{
				filter = null;
				return false;
			}
			filter = filterDataPair.filter;
			return true;
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000AA8C8 File Offset: 0x000A8AC8
		public bool GetMatchingFilter(MessageBuffer messageBuffer, out MessageFilter filter)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			AndMessageFilterTable<FilterData>.FilterDataPair filterDataPair = this.InnerMatch(messageBuffer);
			if (filterDataPair == null)
			{
				filter = null;
				return false;
			}
			filter = filterDataPair.filter;
			return true;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000AA904 File Offset: 0x000A8B04
		public bool GetMatchingFilters(Message message, ICollection<MessageFilter> results)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			this.InnerMatch(message, results);
			return count != results.Count;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000AA954 File Offset: 0x000A8B54
		public bool GetMatchingFilters(MessageBuffer messageBuffer, ICollection<MessageFilter> results)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			this.InnerMatch(messageBuffer, results);
			return count != results.Count;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000AA9A4 File Offset: 0x000A8BA4
		public bool GetMatchingValues(Message message, ICollection<FilterData> results)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			this.InnerMatchData(message, results);
			return count != results.Count;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000AA9F4 File Offset: 0x000A8BF4
		public bool GetMatchingValues(MessageBuffer messageBuffer, ICollection<FilterData> results)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			int count = results.Count;
			this.InnerMatchData(messageBuffer, results);
			return count != results.Count;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000AAA44 File Offset: 0x000A8C44
		public bool Remove(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			AndMessageFilter andMessageFilter = filter as AndMessageFilter;
			return andMessageFilter != null && this.Remove(andMessageFilter);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000AAA77 File Offset: 0x000A8C77
		public bool Remove(KeyValuePair<MessageFilter, FilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, FilterData>>)this.filters).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000AAA98 File Offset: 0x000A8C98
		public bool Remove(AndMessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			if (this.filters.Remove(filter))
			{
				this.filterData.Remove(filter);
				this.table.Remove(filter.Filter1);
				return true;
			}
			return false;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000AAAE8 File Offset: 0x000A8CE8
		public bool TryGetValue(MessageFilter filter, out FilterData data)
		{
			return this.filters.TryGetValue(filter, out data);
		}

		// Token: 0x04002418 RID: 9240
		private Dictionary<MessageFilter, FilterData> filters;

		// Token: 0x04002419 RID: 9241
		private Dictionary<MessageFilter, AndMessageFilterTable<FilterData>.FilterDataPair> filterData;

		// Token: 0x0400241A RID: 9242
		private MessageFilterTable<AndMessageFilterTable<FilterData>.FilterDataPair> table;

		// Token: 0x02000C35 RID: 3125
		internal class FilterDataPair
		{
			// Token: 0x06007740 RID: 30528 RVA: 0x001BDB54 File Offset: 0x001BBD54
			internal FilterDataPair(AndMessageFilter filter, FilterData data)
			{
				this.filter = filter;
				this.data = data;
			}

			// Token: 0x04004435 RID: 17461
			internal AndMessageFilter filter;

			// Token: 0x04004436 RID: 17462
			internal FilterData data;
		}
	}
}
