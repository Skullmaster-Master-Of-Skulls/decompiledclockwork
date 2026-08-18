using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000466 RID: 1126
	internal class EndpointAddressMessageFilterTable<TFilterData> : IMessageFilterTable<TFilterData>, IDictionary<MessageFilter, TFilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
	{
		// Token: 0x06002BA7 RID: 11175 RVA: 0x000AAFA8 File Offset: 0x000A91A8
		public EndpointAddressMessageFilterTable()
		{
			this.processorPool = new WeakReference(null);
			this.size = 0;
			this.nextBit = 0;
			this.filters = new Dictionary<MessageFilter, TFilterData>();
			this.candidates = new Dictionary<MessageFilter, EndpointAddressMessageFilterTable<TFilterData>.Candidate>();
			this.headerLookup = new Dictionary<string, EndpointAddressProcessor.HeaderBit[]>();
			this.InitializeLookupTables();
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000AAFFC File Offset: 0x000A91FC
		protected virtual void InitializeLookupTables()
		{
			this.toHostLookup = new Dictionary<Uri, EndpointAddressMessageFilterTable<TFilterData>.CandidateSet>(EndpointAddressMessageFilter.HostUriComparer.Value);
			this.toNoHostLookup = new Dictionary<Uri, EndpointAddressMessageFilterTable<TFilterData>.CandidateSet>(EndpointAddressMessageFilter.NoHostUriComparer.Value);
		}

		// Token: 0x17000A9A RID: 2714
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
					this.candidates[filter].data = value;
					return;
				}
				this.Add(filter, value);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000AB064 File Offset: 0x000A9264
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000AB071 File Offset: 0x000A9271
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000AB074 File Offset: 0x000A9274
		public ICollection<MessageFilter> Keys
		{
			get
			{
				return this.filters.Keys;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000AB081 File Offset: 0x000A9281
		public ICollection<TFilterData> Values
		{
			get
			{
				return this.filters.Values;
			}
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000AB08E File Offset: 0x000A928E
		public virtual void Add(MessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.Add((EndpointAddressMessageFilter)filter, data);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000AB0B0 File Offset: 0x000A92B0
		public virtual void Add(EndpointAddressMessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.filters.Add(filter, data);
			byte[] mask = this.BuildMask(filter.HeaderLookup);
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = new EndpointAddressMessageFilterTable<TFilterData>.Candidate(filter, data, mask, filter.HeaderLookup);
			this.candidates.Add(filter, candidate);
			Uri uri = filter.Address.Uri;
			EndpointAddressMessageFilterTable<TFilterData>.CandidateSet candidateSet;
			if (filter.IncludeHostNameInComparison)
			{
				if (!this.toHostLookup.TryGetValue(uri, out candidateSet))
				{
					candidateSet = new EndpointAddressMessageFilterTable<TFilterData>.CandidateSet();
					this.toHostLookup.Add(uri, candidateSet);
				}
			}
			else if (!this.toNoHostLookup.TryGetValue(uri, out candidateSet))
			{
				candidateSet = new EndpointAddressMessageFilterTable<TFilterData>.CandidateSet();
				this.toNoHostLookup.Add(uri, candidateSet);
			}
			candidateSet.candidates.Add(candidate);
			this.IncrementQNameCount(candidateSet, filter.Address);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000AB17C File Offset: 0x000A937C
		protected void IncrementQNameCount(EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset, EndpointAddress address)
		{
			for (int i = 0; i < address.Headers.Count; i++)
			{
				AddressHeader addressHeader = address.Headers[i];
				EndpointAddressProcessor.QName key;
				key.name = addressHeader.Name;
				key.ns = addressHeader.Namespace;
				int num;
				if (cset.qnames.TryGetValue(key, out num))
				{
					cset.qnames[key] = num + 1;
				}
				else
				{
					cset.qnames.Add(key, 1);
				}
			}
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000AB1F4 File Offset: 0x000A93F4
		public void Add(KeyValuePair<MessageFilter, TFilterData> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000AB20C File Offset: 0x000A940C
		protected byte[] BuildMask(Dictionary<string, EndpointAddressProcessor.HeaderBit[]> headerLookup)
		{
			byte[] result = null;
			foreach (KeyValuePair<string, EndpointAddressProcessor.HeaderBit[]> keyValuePair in headerLookup)
			{
				EndpointAddressProcessor.HeaderBit[] array;
				if (this.headerLookup.TryGetValue(keyValuePair.Key, out array))
				{
					if (array.Length < keyValuePair.Value.Length)
					{
						int num = array.Length;
						Array.Resize<EndpointAddressProcessor.HeaderBit>(ref array, keyValuePair.Value.Length);
						for (int i = num; i < keyValuePair.Value.Length; i++)
						{
							EndpointAddressProcessor.HeaderBit[] array2 = array;
							int num2 = i;
							int num3 = this.nextBit;
							this.nextBit = num3 + 1;
							array2[num2] = new EndpointAddressProcessor.HeaderBit(num3);
						}
						this.headerLookup[keyValuePair.Key] = array;
					}
				}
				else
				{
					array = new EndpointAddressProcessor.HeaderBit[keyValuePair.Value.Length];
					for (int j = 0; j < keyValuePair.Value.Length; j++)
					{
						EndpointAddressProcessor.HeaderBit[] array3 = array;
						int num4 = j;
						int num3 = this.nextBit;
						this.nextBit = num3 + 1;
						array3[num4] = new EndpointAddressProcessor.HeaderBit(num3);
					}
					this.headerLookup.Add(keyValuePair.Key, array);
				}
				for (int k = 0; k < keyValuePair.Value.Length; k++)
				{
					array[k].AddToMask(ref result);
				}
			}
			if (this.nextBit == 0)
			{
				this.size = 0;
			}
			else
			{
				this.size = (this.nextBit - 1) / 8 + 1;
			}
			return result;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000AB398 File Offset: 0x000A9598
		public void Clear()
		{
			this.size = 0;
			this.nextBit = 0;
			this.filters.Clear();
			this.candidates.Clear();
			this.headerLookup.Clear();
			this.ClearLookupTables();
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000AB3CF File Offset: 0x000A95CF
		protected virtual void ClearLookupTables()
		{
			if (this.toHostLookup != null)
			{
				this.toHostLookup.Clear();
			}
			if (this.toNoHostLookup != null)
			{
				this.toNoHostLookup.Clear();
			}
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000AB3F7 File Offset: 0x000A95F7
		public bool Contains(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000AB405 File Offset: 0x000A9605
		public bool ContainsKey(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			return this.filters.ContainsKey(filter);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000AB426 File Offset: 0x000A9626
		public void CopyTo(KeyValuePair<MessageFilter, TFilterData>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).CopyTo(array, arrayIndex);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000AB438 File Offset: 0x000A9638
		private EndpointAddressProcessor CreateProcessor(int length)
		{
			EndpointAddressProcessor endpointAddressProcessor = null;
			WeakReference obj = this.processorPool;
			lock (obj)
			{
				EndpointAddressMessageFilterTable<TFilterData>.ProcessorPool processorPool = this.processorPool.Target as EndpointAddressMessageFilterTable<TFilterData>.ProcessorPool;
				if (processorPool != null)
				{
					endpointAddressProcessor = processorPool.Pop();
				}
			}
			if (endpointAddressProcessor != null)
			{
				endpointAddressProcessor.Clear(length);
				return endpointAddressProcessor;
			}
			return new EndpointAddressProcessor(length);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000AB4A4 File Offset: 0x000A96A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000AB4AC File Offset: 0x000A96AC
		public IEnumerator<KeyValuePair<MessageFilter, TFilterData>> GetEnumerator()
		{
			return this.filters.GetEnumerator();
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000AB4BE File Offset: 0x000A96BE
		internal virtual bool TryMatchCandidateSet(Uri to, bool includeHostNameInComparison, out EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset)
		{
			if (includeHostNameInComparison)
			{
				return this.toHostLookup.TryGetValue(to, out cset);
			}
			return this.toNoHostLookup.TryGetValue(to, out cset);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000AB4E0 File Offset: 0x000A96E0
		private EndpointAddressMessageFilterTable<TFilterData>.Candidate InnerMatch(Message message)
		{
			Uri to = message.Headers.To;
			if (to == null)
			{
				return null;
			}
			EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset = null;
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = null;
			if (this.TryMatchCandidateSet(to, true, out cset))
			{
				candidate = this.GetSingleMatch(cset, message);
			}
			if (this.TryMatchCandidateSet(to, false, out cset))
			{
				EndpointAddressMessageFilterTable<TFilterData>.Candidate singleMatch = this.GetSingleMatch(cset, message);
				if (singleMatch != null)
				{
					if (candidate != null)
					{
						Collection<MessageFilter> collection = new Collection<MessageFilter>();
						collection.Add(candidate.filter);
						collection.Add(singleMatch.filter);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection));
					}
					candidate = singleMatch;
				}
			}
			return candidate;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000AB57C File Offset: 0x000A977C
		private EndpointAddressMessageFilterTable<TFilterData>.Candidate GetSingleMatch(EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset, Message message)
		{
			int count = cset.candidates.Count;
			if (cset.qnames.Count != 0)
			{
				EndpointAddressProcessor endpointAddressProcessor = this.CreateProcessor(this.size);
				endpointAddressProcessor.ProcessHeaders(message, cset.qnames, this.headerLookup);
				EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = null;
				List<EndpointAddressMessageFilterTable<TFilterData>.Candidate> list = cset.candidates;
				for (int i = 0; i < count; i++)
				{
					if (endpointAddressProcessor.TestMask(list[i].mask))
					{
						if (candidate != null)
						{
							Collection<MessageFilter> collection = new Collection<MessageFilter>();
							collection.Add(candidate.filter);
							collection.Add(list[i].filter);
							throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection), message);
						}
						candidate = list[i];
					}
				}
				this.ReleaseProcessor(endpointAddressProcessor);
				return candidate;
			}
			if (count == 0)
			{
				return null;
			}
			if (count == 1)
			{
				return cset.candidates[0];
			}
			Collection<MessageFilter> collection2 = new Collection<MessageFilter>();
			for (int j = 0; j < count; j++)
			{
				collection2.Add(cset.candidates[j].filter);
			}
			throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches"), null, collection2), message);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000AB6A4 File Offset: 0x000A98A4
		private void InnerMatchData(Message message, ICollection<TFilterData> results)
		{
			Uri to = message.Headers.To;
			if (to != null)
			{
				EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset;
				if (this.TryMatchCandidateSet(to, true, out cset))
				{
					this.InnerMatchData(message, results, cset);
				}
				if (this.TryMatchCandidateSet(to, false, out cset))
				{
					this.InnerMatchData(message, results, cset);
				}
			}
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000AB6F0 File Offset: 0x000A98F0
		private void InnerMatchData(Message message, ICollection<TFilterData> results, EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset)
		{
			EndpointAddressProcessor endpointAddressProcessor = this.CreateProcessor(this.size);
			endpointAddressProcessor.ProcessHeaders(message, cset.qnames, this.headerLookup);
			List<EndpointAddressMessageFilterTable<TFilterData>.Candidate> list = cset.candidates;
			for (int i = 0; i < list.Count; i++)
			{
				if (endpointAddressProcessor.TestMask(list[i].mask))
				{
					results.Add(list[i].data);
				}
			}
			this.ReleaseProcessor(endpointAddressProcessor);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000AB764 File Offset: 0x000A9964
		protected void InnerMatchFilters(Message message, ICollection<MessageFilter> results)
		{
			Uri to = message.Headers.To;
			if (to != null)
			{
				EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset;
				if (this.TryMatchCandidateSet(to, true, out cset))
				{
					this.InnerMatchFilters(message, results, cset);
				}
				if (this.TryMatchCandidateSet(to, false, out cset))
				{
					this.InnerMatchFilters(message, results, cset);
				}
			}
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000AB7B0 File Offset: 0x000A99B0
		private void InnerMatchFilters(Message message, ICollection<MessageFilter> results, EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset)
		{
			EndpointAddressProcessor endpointAddressProcessor = this.CreateProcessor(this.size);
			endpointAddressProcessor.ProcessHeaders(message, cset.qnames, this.headerLookup);
			List<EndpointAddressMessageFilterTable<TFilterData>.Candidate> list = cset.candidates;
			for (int i = 0; i < list.Count; i++)
			{
				if (endpointAddressProcessor.TestMask(list[i].mask))
				{
					results.Add(list[i].filter);
				}
			}
			this.ReleaseProcessor(endpointAddressProcessor);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000AB824 File Offset: 0x000A9A24
		public bool GetMatchingValue(Message message, out TFilterData data)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = this.InnerMatch(message);
			if (candidate == null)
			{
				data = default(TFilterData);
				return false;
			}
			data = candidate.data;
			return true;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000AB868 File Offset: 0x000A9A68
		public bool GetMatchingValue(MessageBuffer messageBuffer, out TFilterData data)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			Message message = messageBuffer.CreateMessage();
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = null;
			try
			{
				candidate = this.InnerMatch(message);
			}
			finally
			{
				message.Close();
			}
			if (candidate == null)
			{
				data = default(TFilterData);
				return false;
			}
			data = candidate.data;
			return true;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000AB8CC File Offset: 0x000A9ACC
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

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000AB91C File Offset: 0x000A9B1C
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

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000AB98C File Offset: 0x000A9B8C
		public bool GetMatchingFilter(Message message, out MessageFilter filter)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = this.InnerMatch(message);
			if (candidate != null)
			{
				filter = candidate.filter;
				return true;
			}
			filter = null;
			return false;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000AB9C8 File Offset: 0x000A9BC8
		public bool GetMatchingFilter(MessageBuffer messageBuffer, out MessageFilter filter)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			Message message = messageBuffer.CreateMessage();
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = null;
			try
			{
				candidate = this.InnerMatch(message);
			}
			finally
			{
				message.Close();
			}
			if (candidate != null)
			{
				filter = candidate.filter;
				return true;
			}
			filter = null;
			return false;
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000ABA24 File Offset: 0x000A9C24
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
			this.InnerMatchFilters(message, results);
			return count != results.Count;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000ABA74 File Offset: 0x000A9C74
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
				this.InnerMatchFilters(message, results);
				result = (count != results.Count);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000ABAE4 File Offset: 0x000A9CE4
		protected void RebuildMasks()
		{
			this.nextBit = 0;
			this.size = 0;
			this.headerLookup.Clear();
			foreach (EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate in this.candidates.Values)
			{
				candidate.mask = this.BuildMask(candidate.headerLookup);
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000ABB60 File Offset: 0x000A9D60
		private void ReleaseProcessor(EndpointAddressProcessor processor)
		{
			WeakReference obj = this.processorPool;
			lock (obj)
			{
				EndpointAddressMessageFilterTable<TFilterData>.ProcessorPool processorPool = this.processorPool.Target as EndpointAddressMessageFilterTable<TFilterData>.ProcessorPool;
				if (processorPool == null)
				{
					processorPool = new EndpointAddressMessageFilterTable<TFilterData>.ProcessorPool();
					this.processorPool.Target = processorPool;
				}
				processorPool.Push(processor);
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000ABBC8 File Offset: 0x000A9DC8
		public virtual bool Remove(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			EndpointAddressMessageFilter endpointAddressMessageFilter = filter as EndpointAddressMessageFilter;
			return endpointAddressMessageFilter != null && this.Remove(endpointAddressMessageFilter);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000ABBFC File Offset: 0x000A9DFC
		public virtual bool Remove(EndpointAddressMessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			if (!this.filters.Remove(filter))
			{
				return false;
			}
			EndpointAddressMessageFilterTable<TFilterData>.Candidate item = this.candidates[filter];
			Uri uri = filter.Address.Uri;
			EndpointAddressMessageFilterTable<TFilterData>.CandidateSet candidateSet;
			if (filter.IncludeHostNameInComparison)
			{
				candidateSet = this.toHostLookup[uri];
			}
			else
			{
				candidateSet = this.toNoHostLookup[uri];
			}
			this.candidates.Remove(filter);
			if (candidateSet.candidates.Count == 1)
			{
				if (filter.IncludeHostNameInComparison)
				{
					this.toHostLookup.Remove(uri);
				}
				else
				{
					this.toNoHostLookup.Remove(uri);
				}
			}
			else
			{
				this.DecrementQNameCount(candidateSet, filter.Address);
				candidateSet.candidates.Remove(item);
			}
			this.RebuildMasks();
			return true;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000ABCD0 File Offset: 0x000A9ED0
		protected void DecrementQNameCount(EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset, EndpointAddress address)
		{
			for (int i = 0; i < address.Headers.Count; i++)
			{
				AddressHeader addressHeader = address.Headers[i];
				EndpointAddressProcessor.QName key;
				key.name = addressHeader.Name;
				key.ns = addressHeader.Namespace;
				int num = cset.qnames[key];
				if (num == 1)
				{
					cset.qnames.Remove(key);
				}
				else
				{
					cset.qnames[key] = num - 1;
				}
			}
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000ABD49 File Offset: 0x000A9F49
		public bool Remove(KeyValuePair<MessageFilter, TFilterData> item)
		{
			return ((ICollection<KeyValuePair<MessageFilter, TFilterData>>)this.filters).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000ABD68 File Offset: 0x000A9F68
		public bool TryGetValue(MessageFilter filter, out TFilterData data)
		{
			return this.filters.TryGetValue(filter, out data);
		}

		// Token: 0x04002425 RID: 9253
		protected Dictionary<MessageFilter, TFilterData> filters;

		// Token: 0x04002426 RID: 9254
		protected Dictionary<MessageFilter, EndpointAddressMessageFilterTable<TFilterData>.Candidate> candidates;

		// Token: 0x04002427 RID: 9255
		private WeakReference processorPool;

		// Token: 0x04002428 RID: 9256
		private int size;

		// Token: 0x04002429 RID: 9257
		private int nextBit;

		// Token: 0x0400242A RID: 9258
		private Dictionary<string, EndpointAddressProcessor.HeaderBit[]> headerLookup;

		// Token: 0x0400242B RID: 9259
		private Dictionary<Uri, EndpointAddressMessageFilterTable<TFilterData>.CandidateSet> toHostLookup;

		// Token: 0x0400242C RID: 9260
		private Dictionary<Uri, EndpointAddressMessageFilterTable<TFilterData>.CandidateSet> toNoHostLookup;

		// Token: 0x02000C39 RID: 3129
		internal class ProcessorPool
		{
			// Token: 0x0600774D RID: 30541 RVA: 0x001BDBE2 File Offset: 0x001BBDE2
			internal ProcessorPool()
			{
			}

			// Token: 0x0600774E RID: 30542 RVA: 0x001BDBEC File Offset: 0x001BBDEC
			internal EndpointAddressProcessor Pop()
			{
				EndpointAddressProcessor endpointAddressProcessor = this.processor;
				if (endpointAddressProcessor != null)
				{
					this.processor = endpointAddressProcessor.next;
					endpointAddressProcessor.next = null;
					return endpointAddressProcessor;
				}
				return null;
			}

			// Token: 0x0600774F RID: 30543 RVA: 0x001BDC19 File Offset: 0x001BBE19
			internal void Push(EndpointAddressProcessor p)
			{
				p.next = this.processor;
				this.processor = p;
			}

			// Token: 0x0400443A RID: 17466
			private EndpointAddressProcessor processor;
		}

		// Token: 0x02000C3A RID: 3130
		internal class Candidate
		{
			// Token: 0x06007750 RID: 30544 RVA: 0x001BDC2E File Offset: 0x001BBE2E
			internal Candidate(MessageFilter filter, TFilterData data, byte[] mask, Dictionary<string, EndpointAddressProcessor.HeaderBit[]> headerLookup)
			{
				this.filter = filter;
				this.data = data;
				this.mask = mask;
				this.headerLookup = headerLookup;
			}

			// Token: 0x0400443B RID: 17467
			internal MessageFilter filter;

			// Token: 0x0400443C RID: 17468
			internal TFilterData data;

			// Token: 0x0400443D RID: 17469
			internal byte[] mask;

			// Token: 0x0400443E RID: 17470
			internal Dictionary<string, EndpointAddressProcessor.HeaderBit[]> headerLookup;
		}

		// Token: 0x02000C3B RID: 3131
		internal class CandidateSet
		{
			// Token: 0x06007751 RID: 30545 RVA: 0x001BDC53 File Offset: 0x001BBE53
			internal CandidateSet()
			{
				this.qnames = new Dictionary<EndpointAddressProcessor.QName, int>(EndpointAddressProcessor.QNameComparer);
				this.candidates = new List<EndpointAddressMessageFilterTable<TFilterData>.Candidate>();
			}

			// Token: 0x0400443F RID: 17471
			internal Dictionary<EndpointAddressProcessor.QName, int> qnames;

			// Token: 0x04004440 RID: 17472
			internal List<EndpointAddressMessageFilterTable<TFilterData>.Candidate> candidates;
		}
	}
}
