using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200047A RID: 1146
	internal class PrefixEndpointAddressMessageFilterTable<TFilterData> : EndpointAddressMessageFilterTable<TFilterData>
	{
		// Token: 0x06002C99 RID: 11417 RVA: 0x000AE27A File Offset: 0x000AC47A
		protected override void InitializeLookupTables()
		{
			this.toHostTable = new UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet>();
			this.toNoHostTable = new UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet>();
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000AE292 File Offset: 0x000AC492
		public override void Add(MessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.Add((PrefixEndpointAddressMessageFilter)filter, data);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000AE2B4 File Offset: 0x000AC4B4
		public override void Add(EndpointAddressMessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException("EndpointAddressMessageFilter cannot be added to PrefixEndpointAddressMessageFilterTable"));
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000AE2E0 File Offset: 0x000AC4E0
		public void Add(PrefixEndpointAddressMessageFilter filter, TFilterData data)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.filters.Add(filter, data);
			byte[] mask = base.BuildMask(filter.HeaderLookup);
			EndpointAddressMessageFilterTable<TFilterData>.Candidate candidate = new EndpointAddressMessageFilterTable<TFilterData>.Candidate(filter, data, mask, filter.HeaderLookup);
			this.candidates.Add(filter, candidate);
			Uri uri = filter.Address.Uri;
			EndpointAddressMessageFilterTable<TFilterData>.CandidateSet candidateSet;
			if (!this.TryMatchCandidateSet(uri, filter.IncludeHostNameInComparison, out candidateSet))
			{
				candidateSet = new EndpointAddressMessageFilterTable<TFilterData>.CandidateSet();
				this.GetAddressTable(filter.IncludeHostNameInComparison).RegisterUri(uri, this.GetComparisonMode(filter.IncludeHostNameInComparison), candidateSet);
			}
			candidateSet.candidates.Add(candidate);
			base.IncrementQNameCount(candidateSet, filter.Address);
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000AE391 File Offset: 0x000AC591
		private HostNameComparisonMode GetComparisonMode(bool includeHostNameInComparison)
		{
			if (!includeHostNameInComparison)
			{
				return HostNameComparisonMode.StrongWildcard;
			}
			return HostNameComparisonMode.Exact;
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000AE399 File Offset: 0x000AC599
		private UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet> GetAddressTable(bool includeHostNameInComparison)
		{
			if (!includeHostNameInComparison)
			{
				return this.toNoHostTable;
			}
			return this.toHostTable;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000AE3AB File Offset: 0x000AC5AB
		internal override bool TryMatchCandidateSet(Uri to, bool includeHostNameInComparison, out EndpointAddressMessageFilterTable<TFilterData>.CandidateSet cset)
		{
			return this.GetAddressTable(includeHostNameInComparison).TryLookupUri(to, this.GetComparisonMode(includeHostNameInComparison), out cset);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000AE3C2 File Offset: 0x000AC5C2
		protected override void ClearLookupTables()
		{
			this.toHostTable = new UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet>();
			this.toNoHostTable = new UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet>();
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000AE3DC File Offset: 0x000AC5DC
		public override bool Remove(MessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			PrefixEndpointAddressMessageFilter prefixEndpointAddressMessageFilter = filter as PrefixEndpointAddressMessageFilter;
			return prefixEndpointAddressMessageFilter != null && this.Remove(prefixEndpointAddressMessageFilter);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000AE40F File Offset: 0x000AC60F
		public override bool Remove(EndpointAddressMessageFilter filter)
		{
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException("EndpointAddressMessageFilter cannot be removed from PrefixEndpointAddressMessageFilterTable"));
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000AE438 File Offset: 0x000AC638
		public bool Remove(PrefixEndpointAddressMessageFilter filter)
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
			EndpointAddressMessageFilterTable<TFilterData>.CandidateSet candidateSet = null;
			if (this.TryMatchCandidateSet(uri, filter.IncludeHostNameInComparison, out candidateSet))
			{
				if (candidateSet.candidates.Count == 1)
				{
					this.GetAddressTable(filter.IncludeHostNameInComparison).UnregisterUri(uri, this.GetComparisonMode(filter.IncludeHostNameInComparison));
				}
				else
				{
					base.DecrementQNameCount(candidateSet, filter.Address);
					candidateSet.candidates.Remove(item);
				}
			}
			this.candidates.Remove(filter);
			base.RebuildMasks();
			return true;
		}

		// Token: 0x04002447 RID: 9287
		private UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet> toHostTable;

		// Token: 0x04002448 RID: 9288
		private UriPrefixTable<EndpointAddressMessageFilterTable<TFilterData>.CandidateSet> toNoHostTable;
	}
}
