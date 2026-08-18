using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200000F RID: 15
	internal class DefaultDiscoveryService : DiscoveryService
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000038F1 File Offset: 0x00001AF1
		public DefaultDiscoveryService(DiscoveryServiceExtension discoveryServiceExtension, DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator, int duplicateMessageHistoryLength) : base(discoveryMessageSequenceGenerator, duplicateMessageHistoryLength)
		{
			this.publishedEndpoints = discoveryServiceExtension.PublishedEndpoints;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003907 File Offset: 0x00001B07
		protected override IAsyncResult OnBeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state)
		{
			this.Match(findRequestContext);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000031C9 File Offset: 0x000013C9
		protected override void OnEndFind(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003917 File Offset: 0x00001B17
		protected override IAsyncResult OnBeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<EndpointDiscoveryMetadata>(this.Match(resolveCriteria), callback, state);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003927 File Offset: 0x00001B27
		protected override EndpointDiscoveryMetadata OnEndResolve(IAsyncResult result)
		{
			return CompletedAsyncResult<EndpointDiscoveryMetadata>.End(result);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003930 File Offset: 0x00001B30
		private EndpointDiscoveryMetadata Match(ResolveCriteria criteria)
		{
			for (int i = 0; i < this.publishedEndpoints.Count; i++)
			{
				if (this.publishedEndpoints[i].Address.Equals(criteria.Address))
				{
					return this.publishedEndpoints[i];
				}
			}
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003980 File Offset: 0x00001B80
		private void Match(FindRequestContext findRequestContext)
		{
			FindCriteria criteria = findRequestContext.Criteria;
			if (!ScopeCompiler.IsSupportedMatchingRule(criteria.ScopeMatchBy))
			{
				return;
			}
			CompiledScopeCriteria[] compiledScopeMatchCriterias = ScopeCompiler.CompileMatchCriteria(criteria.InternalScopes, criteria.ScopeMatchBy);
			int num = 0;
			for (int i = 0; i < this.publishedEndpoints.Count; i++)
			{
				if (criteria.IsMatch(this.publishedEndpoints[i], compiledScopeMatchCriterias))
				{
					findRequestContext.AddMatchingEndpoint(this.publishedEndpoints[i]);
					num++;
					if (num == criteria.MaxResults)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly ReadOnlyCollection<EndpointDiscoveryMetadata> publishedEndpoints;
	}
}
