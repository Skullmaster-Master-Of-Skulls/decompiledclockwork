using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A4 RID: 164
	internal sealed class ResolveDuplex11AsyncResult : ResolveDuplexAsyncResult<ResolveMessage11, IDiscoveryResponseContract11>
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00012226 File Offset: 0x00010426
		internal ResolveDuplex11AsyncResult(ResolveMessage11 resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(resolveMessage, discoveryServiceImpl, multicastSuppressionImpl, callback, state)
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00012235 File Offset: 0x00010435
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ResolveDuplex11AsyncResult>(result);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001223E File Offset: 0x0001043E
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

		// Token: 0x060006FE RID: 1790 RVA: 0x0001226A File Offset: 0x0001046A
		protected override ResolveCriteria GetResolveCriteria(ResolveMessage11 resolveMessage)
		{
			return resolveMessage.Resolve.ToResolveCriteria();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00012277 File Offset: 0x00010477
		protected override IAsyncResult BeginSendResolveResponse(IDiscoveryResponseContract11 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
		{
			return responseChannel.BeginResolveMatchOperation(ResolveMatchesMessage11.Create(discoveryMessageSequence, matchingEndpoint), callback, state);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001228A File Offset: 0x0001048A
		protected override void EndSendResolveResponse(IDiscoveryResponseContract11 responseChannel, IAsyncResult result)
		{
			responseChannel.EndResolveMatchOperation(result);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00012069 File Offset: 0x00010269
		protected override IAsyncResult BeginSendProxyAnnouncement(IDiscoveryResponseContract11 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return responseChannel.BeginHelloOperation(HelloMessage11.Create(discoveryMessageSequence, proxyEndpointDiscoveryMetadata), callback, state);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001207C File Offset: 0x0001027C
		protected override void EndSendProxyAnnouncement(IDiscoveryResponseContract11 responseChannel, IAsyncResult result)
		{
			responseChannel.EndHelloOperation(result);
		}
	}
}
