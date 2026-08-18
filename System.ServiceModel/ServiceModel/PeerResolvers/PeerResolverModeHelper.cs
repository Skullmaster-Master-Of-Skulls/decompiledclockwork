using System;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C9 RID: 457
	internal static class PeerResolverModeHelper
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x000363F5 File Offset: 0x000345F5
		internal static bool IsDefined(PeerResolverMode value)
		{
			return value == PeerResolverMode.Auto || value == PeerResolverMode.Pnrp || value == PeerResolverMode.Custom;
		}
	}
}
