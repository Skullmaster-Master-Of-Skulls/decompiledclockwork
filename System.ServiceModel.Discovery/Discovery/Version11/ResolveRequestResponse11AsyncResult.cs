using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A8 RID: 168
	internal sealed class ResolveRequestResponse11AsyncResult : ResolveRequestResponseAsyncResult<ResolveMessage11, ResolveMatchesMessage11>
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x00012319 File Offset: 0x00010519
		internal ResolveRequestResponse11AsyncResult(ResolveMessage11 resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, AsyncCallback callback, object state) : base(resolveMessage, discoveryServiceImpl, callback, state)
		{
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00012328 File Offset: 0x00010528
		public static ResolveMatchesMessage11 End(IAsyncResult result)
		{
			ResolveRequestResponse11AsyncResult resolveRequestResponse11AsyncResult = AsyncResult.End<ResolveRequestResponse11AsyncResult>(result);
			return resolveRequestResponse11AsyncResult.End();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00012342 File Offset: 0x00010542
		protected override bool ValidateContent(ResolveMessage11 resolveMessage)
		{
			if (resolveMessage == null || resolveMessage.Resolve == null)
			{
				if (TD.DiscoveryMessageWithNoContentIsEnabled())
				{
					TD.DiscoveryMessageWithNoContent(base.Context.EventTraceActivity, "Resolve");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001226A File Offset: 0x0001046A
		protected override ResolveCriteria GetResolveCriteria(ResolveMessage11 resolveMessage)
		{
			return resolveMessage.Resolve.ToResolveCriteria();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001236E File Offset: 0x0001056E
		protected override ResolveMatchesMessage11 GetResolveResponse(DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint)
		{
			return ResolveMatchesMessage11.Create(discoveryMessageSequence, matchingEndpoint);
		}
	}
}
