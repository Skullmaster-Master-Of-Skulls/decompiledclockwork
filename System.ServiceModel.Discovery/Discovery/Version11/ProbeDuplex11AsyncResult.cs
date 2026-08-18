using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200009E RID: 158
	internal sealed class ProbeDuplex11AsyncResult : ProbeDuplexAsyncResult<ProbeMessage11, IDiscoveryResponseContract11>
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x00012006 File Offset: 0x00010206
		internal ProbeDuplex11AsyncResult(ProbeMessage11 probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(probeMessage, discoveryServiceImpl, multicastSuppressionImpl, callback, state)
		{
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00012015 File Offset: 0x00010215
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ProbeDuplex11AsyncResult>(result);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001201E File Offset: 0x0001021E
		protected override bool ValidateContent(ProbeMessage11 probeMessage)
		{
			if (probeMessage == null || probeMessage.Probe == null)
			{
				if (TD.DiscoveryMessageWithNoContentIsEnabled())
				{
					TD.DiscoveryMessageWithNoContent(null, "Probe");
				}
				return false;
			}
			return true;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00012040 File Offset: 0x00010240
		protected override FindCriteria GetFindCriteria(ProbeMessage11 probeMessage)
		{
			return probeMessage.Probe.ToFindCriteria();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001204D File Offset: 0x0001024D
		protected override IAsyncResult BeginSendFindResponse(IDiscoveryResponseContract11 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
		{
			return responseChannel.BeginProbeMatchOperation(ProbeMatchesMessage11.Create(discoveryMessageSequence, matchingEndpoint), callback, state);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00012060 File Offset: 0x00010260
		protected override void EndSendFindResponse(IDiscoveryResponseContract11 responseChannel, IAsyncResult result)
		{
			responseChannel.EndProbeMatchOperation(result);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00012069 File Offset: 0x00010269
		protected override IAsyncResult BeginSendProxyAnnouncement(IDiscoveryResponseContract11 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return responseChannel.BeginHelloOperation(HelloMessage11.Create(discoveryMessageSequence, proxyEndpointDiscoveryMetadata), callback, state);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001207C File Offset: 0x0001027C
		protected override void EndSendProxyAnnouncement(IDiscoveryResponseContract11 responseChannel, IAsyncResult result)
		{
			responseChannel.EndHelloOperation(result);
		}
	}
}
