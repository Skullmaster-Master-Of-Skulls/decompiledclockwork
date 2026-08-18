using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200006C RID: 108
	internal sealed class ProbeDuplexCD1AsyncResult : ProbeDuplexAsyncResult<ProbeMessageCD1, IDiscoveryResponseContractCD1>
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0000FD32 File Offset: 0x0000DF32
		internal ProbeDuplexCD1AsyncResult(ProbeMessageCD1 probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(probeMessage, discoveryServiceImpl, multicastSuppressionImpl, callback, state)
		{
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000FD41 File Offset: 0x0000DF41
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ProbeDuplexCD1AsyncResult>(result);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000FD4A File Offset: 0x0000DF4A
		protected override bool ValidateContent(ProbeMessageCD1 probeMessage)
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

		// Token: 0x0600055C RID: 1372 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		protected override FindCriteria GetFindCriteria(ProbeMessageCD1 probeMessage)
		{
			return probeMessage.Probe.ToFindCriteria();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000FD79 File Offset: 0x0000DF79
		protected override IAsyncResult BeginSendFindResponse(IDiscoveryResponseContractCD1 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state)
		{
			return responseChannel.BeginProbeMatchOperation(ProbeMatchesMessageCD1.Create(discoveryMessageSequence, matchingEndpoint), callback, state);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		protected override void EndSendFindResponse(IDiscoveryResponseContractCD1 responseChannel, IAsyncResult result)
		{
			responseChannel.EndProbeMatchOperation(result);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000FD95 File Offset: 0x0000DF95
		protected override IAsyncResult BeginSendProxyAnnouncement(IDiscoveryResponseContractCD1 responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return responseChannel.BeginHelloOperation(HelloMessageCD1.Create(discoveryMessageSequence, proxyEndpointDiscoveryMetadata), callback, state);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		protected override void EndSendProxyAnnouncement(IDiscoveryResponseContractCD1 responseChannel, IAsyncResult result)
		{
			responseChannel.EndHelloOperation(result);
		}
	}
}
