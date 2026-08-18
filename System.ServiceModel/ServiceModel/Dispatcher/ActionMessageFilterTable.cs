using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000461 RID: 1121
	[DataContract]
	internal class ActionMessageFilterTable<TFilterData> : IMessageFilterTable<TFilterData>, IDictionary<MessageFilter, TFilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x06002B4C RID: 11084 RVA: 0x000A99F4 File Offset: 0x000A7BF4
		public ActionMessageFilterTable()
		{
			this.Init();
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000A9A02 File Offset: 0x000A7C02
		private void Init()
		{
			this.filters = new Dictionary<MessageFilter, TFilterData>();
			this.actions = new Dictionary<string, List<MessageFilter>>();
			this.always = new List<MessageFilter>();
		}

		// Token: 0x17000A88 RID: 2696
		public TFilterData this[MessageFilter filter]
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
					return;
				}
				this.Add(filter, value);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000A9A59 File Offset: 0x000A7C59
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000A9A68 File Offset: 0x000A7C68
		// (set) Token: 0x06002B52 RID: 11090 RVA: 0x000A9AE0 File Offset: 0x000A7CE0
		[DataMember(IsRequired = true)]
		private ActionMessageFilterTable<TFilterData>.Entry[] Entries
		{
			get
			{
				ActionMessageFilterTable<TFilterData>.Entry[] array = new ActionMessageFilterTable<TFilterData>.Entry[this.Count];
				int num = 0;
				foreach (KeyValuePair<MessageFilter, TFilterData> keyValuePair in this.filters)
				{
					array[num++] = new ActionMessageFilterTable<TFilterData>.Entry(keyValuePair.Key, keyValuePair.Value);
				}
				return array;
			}
			set
			{
				this.Init();
				for (int i = 0; i < value.Length; i++)
				{
					this.Add(value[i].filter, value[i].data);
				}
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000A9B17 File Offset: 0x000A7D17
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000A9B1A File Offset: 0x000A7D1A
		public ICollection<MessageFilter> Keys
		{
			get
			{
				return this.filters.Keys;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000A9B27 File Offset: 0x000A7D27
		public ICollection<TFilterData> Values
		{
			get
			{
				return this.filters.Values;
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000A9B34 File Offset: 0x000A7D34
		public void Add(ActionMessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.filters.Add(filter, data);
			if (filter.Actions.Count == 0)
			{
				this.always.Add(filter);
				return;
			}
			for (int i = 0; i < filter.Actions.Count; i++)
			{
				List<MessageFilter> list;
				if (!this.actions.TryGetValue(filter.Actions[i], out list))
				{
					list = new List<MessageFilter>();
					this.actions.Add(filter.Actions[i], list);
				}
				list.Add(filter);
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000A9BD1 File Offset: 0x000A7DD1
		public void Add(MessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.Add((ActionMessageFilter)filter, data);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000A9BF3 File Offset: 0x000A7DF3
		public void Add(KeyValuePair<MessageFilter, TFilterData> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000A9C09 File Offset: 0x000A7E09
		public void Clear()
		{
			this.filters.Clear();
			this.actions.Clear();
			this.always.Clear();
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000A9C2C File Offset: 0x000A7E2C
		public bool Contains(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000A9C3A File Offset: 0x000A7E3A
		public bool ContainsKey(MessageFilter filter)
		{
			return this.filters.ContainsKey(filter);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000A9C48 File Offset: 0x000A7E48
		public void CopyTo(KeyValuePair<MessageFilter, TFilterData>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).CopyTo(array, arrayIndex);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000A9C57 File Offset: 0x000A7E57
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000A9C5F File Offset: 0x000A7E5F
		public IEnumerator<KeyValuePair<MessageFilter, TFilterData>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<MessageFilter, TFilterData>>)this.filters).GetEnumerator();
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000A9C6C File Offset: 0x000A7E6C
		private MessageFilter InnerMatch(Message message)
		{
			string text = message.Headers.Action;
			if (text == null)
			{
				text = string.Empty;
			}
			List<MessageFilter> list;
			if (this.actions.TryGetValue(text, out list))
			{
				if (this.always.Count + list.Count > 1)
				{
					List<MessageFilter> list2 = new List<MessageFilter>(list);
					list2.AddRange(this.always);
					Collection<MessageFilter> collection = new Collection<MessageFilter>(list2);
					throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
				}
				return list[0];
			}
			else
			{
				if (this.always.Count > 1)
				{
					Collection<MessageFilter> collection2 = new Collection<MessageFilter>(new List<MessageFilter>(this.always));
					throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection2), message);
				}
				if (this.always.Count == 1)
				{
					return this.always[0];
				}
				return null;
			}
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000A9D44 File Offset: 0x000A7F44
		private void InnerMatch(Message message, ICollection<MessageFilter> results)
		{
			for (int i = 0; i < this.always.Count; i++)
			{
				results.Add(this.always[i]);
			}
			string text = message.Headers.Action;
			if (text == null)
			{
				text = string.Empty;
			}
			List<MessageFilter> list;
			if (this.actions.TryGetValue(text, out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					results.Add(list[j]);
				}
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000A9DBC File Offset: 0x000A7FBC
		private void InnerMatchData(Message message, ICollection<TFilterData> results)
		{
			for (int i = 0; i < this.always.Count; i++)
			{
				results.Add(this.filters[this.always[i]]);
			}
			string text = message.Headers.Action;
			if (text == null)
			{
				text = string.Empty;
			}
			List<MessageFilter> list;
			if (this.actions.TryGetValue(text, out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					results.Add(this.filters[list[j]]);
				}
			}
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000A9E4C File Offset: 0x000A804C
		public bool GetMatchingValue(Message message, out TFilterData data)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			MessageFilter messageFilter = this.InnerMatch(message);
			if (messageFilter == null)
			{
				data = default(TFilterData);
				return false;
			}
			data = this.filters[messageFilter];
			return true;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000A9E94 File Offset: 0x000A8094
		public bool GetMatchingValue(MessageBuffer messageBuffer, out TFilterData data)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			MessageFilter messageFilter = null;
			Message message = messageBuffer.CreateMessage();
			try
			{
				messageFilter = this.InnerMatch(message);
			}
			finally
			{
				message.Close();
			}
			if (messageFilter == null)
			{
				data = default(TFilterData);
				return false;
			}
			data = this.filters[messageFilter];
			return true;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000A9F00 File Offset: 0x000A8100
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			filter = this.InnerMatch(message);
			return filter != null;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000A9F24 File Offset: 0x000A8124
		public bool GetMatchingFilter(MessageBuffer messageBuffer, out MessageFilter filter)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			Message message = messageBuffer.CreateMessage();
			bool result;
			try
			{
				filter = this.InnerMatch(message);
				result = (filter != null);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000A9F74 File Offset: 0x000A8174
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

		// Token: 0x06002B67 RID: 11111 RVA: 0x000A9FC4 File Offset: 0x000A81C4
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
			Message message = messageBuffer.CreateMessage();
			bool result;
			try
			{
				int count = results.Count;
				this.InnerMatch(message, results);
				result = (count != results.Count);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000AA034 File Offset: 0x000A8234
		public bool GetMatchingValues(Message message, ICollection<TFilterData> results)
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

		// Token: 0x06002B69 RID: 11113 RVA: 0x000AA084 File Offset: 0x000A8284
		public bool GetMatchingValues(MessageBuffer messageBuffer, ICollection<TFilterData> results)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			Message message = messageBuffer.CreateMessage();
			bool result;
			try
			{
				int count = results.Count;
				this.InnerMatchData(message, results);
				result = (count != results.Count);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000AA0F4 File Offset: 0x000A82F4
		public bool Remove(ActionMessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			if (this.filters.Remove(filter))
			{
				if (filter.Actions.Count == 0)
				{
					this.always.Remove(filter);
				}
				else
				{
					for (int i = 0; i < filter.Actions.Count; i++)
					{
						List<MessageFilter> list = this.actions[filter.Actions[i]];
						if (list.Count == 1)
						{
							this.actions.Remove(filter.Actions[i]);
						}
						else
						{
							list.Remove(filter);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000AA19C File Offset: 0x000A839C
		public bool Remove(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			ActionMessageFilter actionMessageFilter = filter as ActionMessageFilter;
			return actionMessageFilter != null && this.Remove(actionMessageFilter);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000AA1CF File Offset: 0x000A83CF
		public bool Remove(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000AA1EE File Offset: 0x000A83EE
		public bool TryGetValue(MessageFilter filter, out TFilterData data)
		{
			return this.filters.TryGetValue(filter, out data);
		}

		// Token: 0x04002413 RID: 9235
		private Dictionary<MessageFilter, TFilterData> filters;

		// Token: 0x04002414 RID: 9236
		private Dictionary<string, List<MessageFilter>> actions;

		// Token: 0x04002415 RID: 9237
		private List<MessageFilter> always;

		// Token: 0x02000C34 RID: 3124
		[DataContract]
		private class Entry
		{
			// Token: 0x0600773F RID: 30527 RVA: 0x001BDB3E File Offset: 0x001BBD3E
			internal Entry(MessageFilter f, TFilterData d)
			{
				this.filter = f;
				this.data = d;
			}

			// Token: 0x04004433 RID: 17459
			[DataMember(IsRequired = true)]
			internal MessageFilter filter;

			// Token: 0x04004434 RID: 17460
			[DataMember(IsRequired = true)]
			internal TFilterData data;
		}
	}
}
