using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000072 RID: 114
	internal sealed class ResolveDuplexCD1AsyncResult : ResolveDuplexAsyncResult<ResolveMessageCD1, IDiscoveryResponseContractCD1>
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0000FF52 File Offset: 0x0000E152
		internal ResolveDuplexCD1AsyncResult(ResolveMessageCD1 resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(resolveMessage, discoveryServiceImpl, multicastSuppressionImpl, callback, state)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000FF61 File Offset: 0x0000E161
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ResolveDuplexCD1AsyncResult>(result);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000FF6A File Offset: 0x0000E16A
		protected override bool ValidateContent(ResolveMessageCD1 resolveMessage)
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

		// Token: 0x0600057E RID: 1406 RVA: 0x0000FF96 File Offset: 0x0000E196
		protected override ResolveCriteria GetResolveCriteria(ResolveMessageCD1 resolveMessage)
		{
			return resolveMessage.Resolve.ToResolveCriteria();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000FFA3 File Offset: 0x0000E1A3
		protected override IAsyncResult BeginSendResolveResponse(IDiscoveryResponseContractCD1 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
		{
			return responseChannel.BeginResolveMatchOperation(ResolveMatchesMessageCD1.Create(discoveryMessageSequence, matchingEndpoint), callback, state);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000FFB6 File Offset: 0x0000E1B6
		protected override void EndSendResolveResponse(IDiscoveryResponseContractCD1 responseChannel, IAsyncResult result)
		{
			responseChannel.EndResolveMatchOperation(result);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000FD95 File Offset: 0x0000DF95
		protected override IAsyncResult BeginSendProxyAnnouncement(IDiscoveryResponseContractCD1 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return responseChannel.BeginHelloOperation(HelloMessageCD1.Create(discoveryMessageSequence, proxyEndpointDiscoveryMetadata), callback, state);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		protected override void EndSendProxyAnnouncement(IDiscoveryResponseContractCD1 responseChannel, IAsyncResult result)
		{
			responseChannel.EndHelloOperation(result);
		}
	}
}
