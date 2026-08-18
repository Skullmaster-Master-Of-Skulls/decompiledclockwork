using System;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C7 RID: 455
	internal static class PeerReferralPolicyHelper
	{
		// Token: 0x06000EDF RID: 3807 RVA: 0x000363E6 File Offset: 0x000345E6
		internal static bool IsDefined(PeerReferralPolicy value)
		{
			return value == PeerReferralPolicy.Service || value == PeerReferralPolicy.Share || value == PeerReferralPolicy.DoNotShare;
		}
	}
}
