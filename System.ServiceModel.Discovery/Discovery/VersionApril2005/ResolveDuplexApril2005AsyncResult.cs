using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200008B RID: 139
	internal sealed class ResolveDuplexApril2005AsyncResult : ResolveDuplexAsyncResult<ResolveMessageApril2005, IDiscoveryResponseContractApril2005>
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x00010F39 File Offset: 0x0000F139
		internal ResolveDuplexApril2005AsyncResult(ResolveMessageApril2005 resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(resolveMessage, discoveryServiceImpl, multicastSuppressionImpl, callback, state)
		{
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00010F48 File Offset: 0x0000F148
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ResolveDuplexApril2005AsyncResult>(result);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00010F51 File Offset: 0x0000F151
		protected override bool ValidateContent(ResolveMessageApril2005 resolveMessage)
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

		// Token: 0x06000634 RID: 1588 RVA: 0x00010F7D File Offset: 0x0000F17D
		protected override ResolveCriteria GetResolveCriteria(ResolveMessageApril2005 resolveMessage)
		{
			return resolveMessage.Resolve.ToResolveCriteria();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00010F8A File Offset: 0x0000F18A
		protected override IAsyncResult BeginSendResolveResponse(IDiscoveryResponseContractApril2005 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
		{
			return responseChannel.BeginResolveMatchOperation(ResolveMatchesMessageApril2005.Create(discoveryMessageSequence, matchingEndpoint), callback, state);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00010F9D File Offset: 0x0000F19D
		protected override void EndSendResolveResponse(IDiscoveryResponseContractApril2005 responseChannel, IAsyncResult result)
		{
			responseChannel.EndResolveMatchOperation(result);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00010DCB File Offset: 0x0000EFCB
		protected override IAsyncResult BeginSendProxyAnnouncement(IDiscoveryResponseContractApril2005 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return responseChannel.BeginHelloOperation(HelloMessageApril2005.Create(discoveryMessageSequence, proxyEndpointDiscoveryMetadata), callback, state);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00010DDE File Offset: 0x0000EFDE
		protected override void EndSendProxyAnnouncement(IDiscoveryResponseContractApril2005 responseChannel, IAsyncResult result)
		{
			responseChannel.EndHelloOperation(result);
		}
	}
}
