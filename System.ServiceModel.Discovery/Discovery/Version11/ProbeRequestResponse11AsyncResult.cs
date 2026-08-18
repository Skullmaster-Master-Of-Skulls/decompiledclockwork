using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A2 RID: 162
	internal sealed class ProbeRequestResponse11AsyncResult : ProbeRequestResponseAsyncResult<ProbeMessage11, ProbeMatchesMessage11>
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00012169 File Offset: 0x00010369
		internal ProbeRequestResponse11AsyncResult(ProbeMessage11 probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, AsyncCallback callback, object state) : base(probeMessage, discoveryServiceImpl, callback, state)
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00012178 File Offset: 0x00010378
		public static ProbeMatchesMessage11 End(IAsyncResult result)
		{
			ProbeRequestResponse11AsyncResult probeRequestResponse11AsyncResult = AsyncResult.End<ProbeRequestResponse11AsyncResult>(result);
			return probeRequestResponse11AsyncResult.End();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001201E File Offset: 0x0001021E
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

		// Token: 0x060006F1 RID: 1777 RVA: 0x00012040 File Offset: 0x00010240
		protected override FindCriteria GetFindCriteria(ProbeMessage11 probeMessage)
		{
			return probeMessage.Probe.ToFindCriteria();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00012192 File Offset: 0x00010392
		protected override ProbeMatchesMessage11 GetProbeResponse(DiscoveryMessageSequence discoveryMessageSequence, Collection<EndpointDiscoveryMetadata> matchingEndpoints)
		{
			return ProbeMatchesMessage11.Create(discoveryMessageSequence, matchingEndpoints);
		}
	}
}
