using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000086 RID: 134
	internal sealed class ProbeDuplexApril2005AsyncResult : ProbeDuplexAsyncResult<ProbeMessageApril2005, IDiscoveryResponseContractApril2005>
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00010D5E File Offset: 0x0000EF5E
		internal ProbeDuplexApril2005AsyncResult(ProbeMessageApril2005 probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(probeMessage, discoveryServiceImpl, multicastSuppressionImpl, callback, state)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00010D6D File Offset: 0x0000EF6D
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ProbeDuplexApril2005AsyncResult>(result);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00010D76 File Offset: 0x0000EF76
		protected override bool ValidateContent(ProbeMessageApril2005 probeMessage)
		{
			if (probeMessage == null || probeMessage.Probe == null)
			{
				if (TD.DiscoveryMessageWithNoContentIsEnabled())
				{
					TD.DiscoveryMessageWithNoContent(base.Context.EventTraceActivity, "Probe");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		protected override FindCriteria GetFindCriteria(ProbeMessageApril2005 probeMessage)
		{
			return probeMessage.Probe.ToFindCriteria();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00010DAF File Offset: 0x0000EFAF
		protected override IAsyncResult BeginSendFindResponse(IDiscoveryResponseContractApril2005 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
		{
			return responseChannel.BeginProbeMatchOperation(ProbeMatchesMessageApril2005.Create(discoveryMessageSequence, matchingEndpoint), callback, state);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00010DC2 File Offset: 0x0000EFC2
		protected override void EndSendFindResponse(IDiscoveryResponseContractApril2005 responseChannel, IAsyncResult result)
		{
			responseChannel.EndProbeMatchOperation(result);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00010DCB File Offset: 0x0000EFCB
		protected override IAsyncResult BeginSendProxyAnnouncement(IDiscoveryResponseContractApril2005 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return responseChannel.BeginHelloOperation(HelloMessageApril2005.Create(discoveryMessageSequence, proxyEndpointDiscoveryMetadata), callback, state);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00010DDE File Offset: 0x0000EFDE
		protected override void EndSendProxyAnnouncement(IDiscoveryResponseContractApril2005 responseChannel, IAsyncResult result)
		{
			responseChannel.EndHelloOperation(result);
		}
	}
}
