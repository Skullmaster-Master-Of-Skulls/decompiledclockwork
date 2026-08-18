using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000559 RID: 1369
	internal class EndpointDispatcherTable
	{
		// Token: 0x06003563 RID: 13667 RVA: 0x000CF9B0 File Offset: 0x000CDBB0
		public EndpointDispatcherTable(object thisLock)
		{
			this.thisLock = thisLock;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000CF9BF File Offset: 0x000CDBBF
		public int Count
		{
			get
			{
				return ((this.cachedEndpoints != null) ? this.cachedEndpoints.Count : 0) + ((this.filters != null) ? this.filters.Count : 0);
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000CF9EE File Offset: 0x000CDBEE
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000CF9F8 File Offset: 0x000CDBF8
		public void AddEndpoint(EndpointDispatcher endpoint)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				MessageFilter endpointFilter = endpoint.EndpointFilter;
				int filterPriority = endpoint.FilterPriority;
				if (this.filters == null)
				{
					if (this.cachedEndpoints == null)
					{
						this.cachedEndpoints = new List<EndpointDispatcher>(2);
					}
					if (this.cachedEndpoints.Count < 2)
					{
						this.cachedEndpoints.Add(endpoint);
					}
					else
					{
						this.filters = new MessageFilterTable<EndpointDispatcher>();
						for (int i = 0; i < this.cachedEndpoints.Count; i++)
						{
							int filterPriority2 = this.cachedEndpoints[i].FilterPriority;
							MessageFilter endpointFilter2 = this.cachedEndpoints[i].EndpointFilter;
							this.filters.Add(endpointFilter2, this.cachedEndpoints[i], filterPriority2);
						}
						this.filters.Add(endpointFilter, endpoint, filterPriority);
						this.cachedEndpoints = null;
					}
				}
				else
				{
					this.filters.Add(endpointFilter, endpoint, filterPriority);
				}
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000CFB0C File Offset: 0x000CDD0C
		public void RemoveEndpoint(EndpointDispatcher endpoint)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.filters == null)
				{
					if (this.cachedEndpoints != null && this.cachedEndpoints.Contains(endpoint))
					{
						this.cachedEndpoints.Remove(endpoint);
					}
				}
				else
				{
					MessageFilter endpointFilter = endpoint.EndpointFilter;
					this.filters.Remove(endpointFilter);
				}
			}
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000CFB88 File Offset: 0x000CDD88
		private EndpointDispatcher LookupInCache(Message message, out bool addressMatched)
		{
			EndpointDispatcher endpointDispatcher = null;
			int num = int.MinValue;
			bool flag = false;
			addressMatched = false;
			if (this.cachedEndpoints != null && this.cachedEndpoints.Count > 0)
			{
				for (int i = 0; i < this.cachedEndpoints.Count; i++)
				{
					EndpointDispatcher endpointDispatcher2 = this.cachedEndpoints[i];
					int filterPriority = endpointDispatcher2.FilterPriority;
					MessageFilter endpointFilter = endpointDispatcher2.EndpointFilter;
					AndMessageFilter andMessageFilter = endpointFilter as AndMessageFilter;
					bool flag2;
					if (andMessageFilter != null)
					{
						bool flag3;
						flag2 = andMessageFilter.Match(message, out flag3);
						addressMatched = (addressMatched || flag3);
					}
					else
					{
						flag2 = endpointFilter.Match(message);
					}
					if (flag2)
					{
						addressMatched = true;
						if (filterPriority > num || endpointDispatcher == null)
						{
							endpointDispatcher = endpointDispatcher2;
							num = filterPriority;
							flag = false;
						}
						else if (filterPriority == num && endpointDispatcher != null)
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				throw TraceUtility.ThrowHelperError(new MultipleFilterMatchesException(SR.GetString("FilterMultipleMatches")), message);
			}
			return endpointDispatcher;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000CFC60 File Offset: 0x000CDE60
		public EndpointDispatcher Lookup(Message message, out bool addressMatched)
		{
			EndpointDispatcher endpointDispatcher = null;
			endpointDispatcher = this.LookupInCache(message, out addressMatched);
			if (endpointDispatcher == null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					endpointDispatcher = this.LookupInCache(message, out addressMatched);
					if (endpointDispatcher == null && this.filters != null)
					{
						this.filters.GetMatchingValue(message, out endpointDispatcher, out addressMatched);
					}
				}
			}
			return endpointDispatcher;
		}

		// Token: 0x04002878 RID: 10360
		private MessageFilterTable<EndpointDispatcher> filters;

		// Token: 0x04002879 RID: 10361
		private object thisLock;

		// Token: 0x0400287A RID: 10362
		private const int optimizationThreshold = 2;

		// Token: 0x0400287B RID: 10363
		private List<EndpointDispatcher> cachedEndpoints;
	}
}
