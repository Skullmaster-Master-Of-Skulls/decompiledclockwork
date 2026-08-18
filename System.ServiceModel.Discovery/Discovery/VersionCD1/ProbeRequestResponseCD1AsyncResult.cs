using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000070 RID: 112
	internal sealed class ProbeRequestResponseCD1AsyncResult : ProbeRequestResponseAsyncResult<ProbeMessageCD1, ProbeMatchesMessageCD1>
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x0000FE95 File Offset: 0x0000E095
		internal ProbeRequestResponseCD1AsyncResult(ProbeMessageCD1 probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, AsyncCallback callback, object state) : base(probeMessage, discoveryServiceImpl, callback, state)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public static ProbeMatchesMessageCD1 End(IAsyncResult result)
		{
			ProbeRequestResponseCD1AsyncResult probeRequestResponseCD1AsyncResult = AsyncResult.End<ProbeRequestResponseCD1AsyncResult>(result);
			return probeRequestResponseCD1AsyncResult.End();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000FD4A File Offset: 0x0000DF4A
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

		// Token: 0x06000571 RID: 1393 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		protected override FindCriteria GetFindCriteria(ProbeMessageCD1 probeMessage)
		{
			return probeMessage.Probe.ToFindCriteria();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000FEBE File Offset: 0x0000E0BE
		protected override ProbeMatchesMessageCD1 GetProbeResponse(DiscoveryMessageSequence discoveryMessageSequence, Collection<EndpointDiscoveryMetadata> matchingEndpoints)
		{
			return ProbeMatchesMessageCD1.Create(discoveryMessageSequence, matchingEndpoints);
		}
	}
}
