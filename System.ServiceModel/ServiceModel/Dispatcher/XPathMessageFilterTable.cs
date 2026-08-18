using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200051E RID: 1310
	[DataContract]
	public class XPathMessageFilterTable<TFilterData> : IMessageFilterTable<TFilterData>, IDictionary<MessageFilter, TFilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x060031B8 RID: 12728 RVA: 0x000BF0D1 File Offset: 0x000BD2D1
		public XPathMessageFilterTable()
		{
			this.Init(-1);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000BF0E0 File Offset: 0x000BD2E0
		public XPathMessageFilterTable(int capacity)
		{
			if (capacity < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("capacity", capacity, SR.GetString("FilterCapacityNegative")));
			}
			this.Init(capacity);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000BF118 File Offset: 0x000BD318
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Init(-1);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000BF121 File Offset: 0x000BD321
		private void Init(int capacity)
		{
			if (capacity <= 0)
			{
				this.filters = new Dictionary<MessageFilter, TFilterData>();
			}
			else
			{
				this.filters = new Dictionary<MessageFilter, TFilterData>(capacity);
			}
			if (this.iqMatcher == null)
			{
				this.iqMatcher = new InverseQueryMatcher(true);
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000BF154 File Offset: 0x000BD354
		private bool CanMatch
		{
			get
			{
				return this.filters.Count > 0 && this.iqMatcher != null;
			}
		}

		// Token: 0x17000BC2 RID: 3010
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

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x000BF1A3 File Offset: 0x000BD3A3
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000BF1B0 File Offset: 0x000BD3B0
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000BF228 File Offset: 0x000BD428
		[DataMember]
		private XPathMessageFilterTable<TFilterData>.Entry[] Entries
		{
			get
			{
				XPathMessageFilterTable<TFilterData>.Entry[] array = new XPathMessageFilterTable<TFilterData>.Entry[this.Count];
				int num = 0;
				foreach (KeyValuePair<MessageFilter, TFilterData> keyValuePair in this.filters)
				{
					array[num++] = new XPathMessageFilterTable<TFilterData>.Entry(keyValuePair.Key, keyValuePair.Value);
				}
				return array;
			}
			set
			{
				this.Init(value.Length);
				for (int i = 0; i < value.Length; i++)
				{
					this.Add(value[i].filter, value[i].data);
				}
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000BF262 File Offset: 0x000BD462
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000BF265 File Offset: 0x000BD465
		public ICollection<MessageFilter> Keys
		{
			get
			{
				return this.filters.Keys;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000BF272 File Offset: 0x000BD472
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000BF280 File Offset: 0x000BD480
		[DataMember]
		public int NodeQuota
		{
			get
			{
				return this.iqMatcher.NodeQuota;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("NodeQuota", value, SR.GetString("FilterQuotaRange")));
				}
				if (this.iqMatcher == null)
				{
					this.iqMatcher = new InverseQueryMatcher(true);
				}
				this.iqMatcher.NodeQuota = value;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000BF2D6 File Offset: 0x000BD4D6
		public ICollection<TFilterData> Values
		{
			get
			{
				return this.filters.Values;
			}
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000BF2E3 File Offset: 0x000BD4E3
		public void Add(MessageFilter filter, TFilterData data)
		{
			this.Add((XPathMessageFilter)filter, data);
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000BF2F2 File Offset: 0x000BD4F2
		public void Add(KeyValuePair<MessageFilter, TFilterData> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000BF308 File Offset: 0x000BD508
		public void Add(XPathMessageFilter filter, TFilterData data)
		{
			this.Add(filter, data, false);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000BF313 File Offset: 0x000BD513
		internal void Add(XPathMessageFilter filter, TFilterData data, bool forceExternal)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.filters.Add(filter, data);
			this.iqMatcher.Add(filter.XPath, filter.Namespaces, filter, forceExternal);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000BF34E File Offset: 0x000BD54E
		public void Clear()
		{
			this.iqMatcher.Clear();
			this.filters.Clear();
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000BF366 File Offset: 0x000BD566
		public bool Contains(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000BF374 File Offset: 0x000BD574
		public bool ContainsKey(MessageFilter filter)
		{
			return this.filters.ContainsKey(filter);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000BF382 File Offset: 0x000BD582
		public void CopyTo(KeyValuePair<MessageFilter, TFilterData>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).CopyTo(array, arrayIndex);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000BF391 File Offset: 0x000BD591
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000BF399 File Offset: 0x000BD599
		public IEnumerator<KeyValuePair<MessageFilter, TFilterData>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<MessageFilter, TFilterData>>)this.filters).GetEnumerator();
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000BF3A6 File Offset: 0x000BD5A6
		public bool GetMatchingValue(Message message, out TFilterData data)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (this.CanMatch)
			{
				return this.ProcessMatch(this.iqMatcher.Match(message, false, null), out data);
			}
			data = default(TFilterData);
			return false;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000BF3E1 File Offset: 0x000BD5E1
		public bool GetMatchingValue(MessageBuffer messageBuffer, out TFilterData data)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			if (this.CanMatch)
			{
				return this.ProcessMatch(this.iqMatcher.Match(messageBuffer, null), out data);
			}
			data = default(TFilterData);
			return false;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000BF41B File Offset: 0x000BD61B
		public bool GetMatchingValue(SeekableXPathNavigator navigator, out TFilterData data)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			if (this.CanMatch)
			{
				return this.ProcessMatch(this.iqMatcher.Match(navigator, null), out data);
			}
			data = default(TFilterData);
			return false;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000BF455 File Offset: 0x000BD655
		public bool GetMatchingValue(XPathNavigator navigator, out TFilterData data)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			if (this.CanMatch)
			{
				return this.ProcessMatch(this.iqMatcher.Match(navigator, null), out data);
			}
			data = default(TFilterData);
			return false;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000BF490 File Offset: 0x000BD690
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			Collection<MessageFilter> collection = new Collection<MessageFilter>();
			this.GetMatchingFilters(message, collection);
			if (collection.Count > 1)
			{
				throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
			}
			if (collection.Count == 1)
			{
				filter = collection[0];
				return true;
			}
			filter = null;
			return false;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000BF4E8 File Offset: 0x000BD6E8
		public bool GetMatchingFilter(MessageBuffer messageBuffer, out MessageFilter filter)
		{
			Collection<MessageFilter> collection = new Collection<MessageFilter>();
			this.GetMatchingFilters(messageBuffer, collection);
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
			}
			if (collection.Count == 1)
			{
				filter = collection[0];
				return true;
			}
			filter = null;
			return false;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000BF544 File Offset: 0x000BD744
		public bool GetMatchingFilter(SeekableXPathNavigator navigator, out MessageFilter filter)
		{
			Collection<MessageFilter> collection = new Collection<MessageFilter>();
			this.GetMatchingFilters(navigator, collection);
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
			}
			if (collection.Count == 1)
			{
				filter = collection[0];
				return true;
			}
			filter = null;
			return false;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000BF5A0 File Offset: 0x000BD7A0
		public bool GetMatchingFilter(XPathNavigator navigator, out MessageFilter filter)
		{
			Collection<MessageFilter> collection = new Collection<MessageFilter>();
			this.GetMatchingFilters(navigator, collection);
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
			}
			if (collection.Count == 1)
			{
				filter = collection[0];
				return true;
			}
			filter = null;
			return false;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000BF5FC File Offset: 0x000BD7FC
		public bool GetMatchingFilters(Message message, ICollection<MessageFilter> results)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (results == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("results", message);
			}
			if (this.CanMatch)
			{
				int count = results.Count;
				this.iqMatcher.ReleaseResult(this.iqMatcher.Match(message, false, results));
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000BF664 File Offset: 0x000BD864
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
			if (this.CanMatch)
			{
				int count = results.Count;
				this.iqMatcher.ReleaseResult(this.iqMatcher.Match(messageBuffer, results));
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000BF6CC File Offset: 0x000BD8CC
		public bool GetMatchingFilters(SeekableXPathNavigator navigator, ICollection<MessageFilter> results)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			if (this.CanMatch)
			{
				int count = results.Count;
				this.iqMatcher.ReleaseResult(this.iqMatcher.Match(navigator, results));
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000BF734 File Offset: 0x000BD934
		public bool GetMatchingFilters(XPathNavigator navigator, ICollection<MessageFilter> results)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			if (this.CanMatch)
			{
				int count = results.Count;
				this.iqMatcher.ReleaseResult(this.iqMatcher.Match(navigator, results));
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000BF79C File Offset: 0x000BD99C
		public bool GetMatchingValues(Message message, ICollection<TFilterData> results)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (results == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("results", message);
			}
			if (this.CanMatch)
			{
				int count = results.Count;
				this.ProcessMatches(this.iqMatcher.Match(message, false, null), results);
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000BF800 File Offset: 0x000BDA00
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
			if (this.CanMatch)
			{
				int count = results.Count;
				this.ProcessMatches(this.iqMatcher.Match(messageBuffer, null), results);
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000BF864 File Offset: 0x000BDA64
		public bool GetMatchingValues(SeekableXPathNavigator navigator, ICollection<TFilterData> results)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			if (this.CanMatch)
			{
				int count = results.Count;
				this.ProcessMatches(this.iqMatcher.Match(navigator, null), results);
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000BF8C8 File Offset: 0x000BDAC8
		public bool GetMatchingValues(XPathNavigator navigator, ICollection<TFilterData> results)
		{
			if (navigator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("navigator");
			}
			if (results == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("results");
			}
			if (this.CanMatch)
			{
				int count = results.Count;
				this.ProcessMatches(this.iqMatcher.Match(navigator, null), results);
				return count != results.Count;
			}
			return false;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000BF92C File Offset: 0x000BDB2C
		private bool ProcessMatch(FilterResult result, out TFilterData data)
		{
			bool result2 = false;
			data = default(TFilterData);
			MessageFilter singleMatch = result.GetSingleMatch();
			if (singleMatch != null)
			{
				data = this.filters[singleMatch];
				result2 = true;
			}
			this.iqMatcher.ReleaseResult(result);
			return result2;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000BF970 File Offset: 0x000BDB70
		private void ProcessMatches(FilterResult result, ICollection<TFilterData> results)
		{
			Collection<MessageFilter> matchList = result.Processor.MatchList;
			int i = 0;
			int count = matchList.Count;
			while (i < count)
			{
				results.Add(this.filters[matchList[i]]);
				i++;
			}
			this.iqMatcher.ReleaseResult(result);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000BF9C4 File Offset: 0x000BDBC4
		public bool Remove(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			XPathMessageFilter xpathMessageFilter = filter as XPathMessageFilter;
			return xpathMessageFilter != null && this.Remove(xpathMessageFilter);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000BF9F7 File Offset: 0x000BDBF7
		public bool Remove(KeyValuePair<MessageFilter, TFilterData> item)
		{
			if (((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Remove(item))
			{
				this.iqMatcher.Remove((XPathMessageFilter)item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000BFA21 File Offset: 0x000BDC21
		public bool Remove(XPathMessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			if (this.filters.Remove(filter))
			{
				this.iqMatcher.Remove(filter);
				return true;
			}
			return false;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000BFA53 File Offset: 0x000BDC53
		public void TrimToSize()
		{
			this.iqMatcher.Trim();
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000BFA60 File Offset: 0x000BDC60
		public bool TryGetValue(MessageFilter filter, out TFilterData data)
		{
			return this.filters.TryGetValue(filter, out data);
		}

		// Token: 0x0400267E RID: 9854
		internal Dictionary<MessageFilter, TFilterData> filters;

		// Token: 0x0400267F RID: 9855
		private InverseQueryMatcher iqMatcher;

		// Token: 0x02000C50 RID: 3152
		[DataContract]
		private class Entry
		{
			// Token: 0x0600779E RID: 30622 RVA: 0x001BF1CD File Offset: 0x001BD3CD
			internal Entry(MessageFilter f, TFilterData d)
			{
				this.filter = f;
				this.data = d;
			}

			// Token: 0x0400446B RID: 17515
			[DataMember(IsRequired = true)]
			internal MessageFilter filter;

			// Token: 0x0400446C RID: 17516
			[DataMember(IsRequired = true)]
			internal TFilterData data;
		}
	}
}
