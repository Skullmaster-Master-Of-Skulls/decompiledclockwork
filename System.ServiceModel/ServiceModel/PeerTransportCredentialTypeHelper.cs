using System;

namespace System.ServiceModel
{
	// Token: 0x02000172 RID: 370
	internal static class PeerTransportCredentialTypeHelper
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00028A02 File Offset: 0x00026C02
		internal static bool IsDefined(PeerTransportCredentialType value)
		{
			return value == PeerTransportCredentialType.Password || value == PeerTransportCredentialType.Certificate;
		}
	}
}
