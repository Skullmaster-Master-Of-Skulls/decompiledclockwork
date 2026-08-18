using System;

namespace System.ServiceModel
{
	// Token: 0x0200015E RID: 350
	internal static class WSFederationHttpSecurityModeHelper
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x00026B73 File Offset: 0x00024D73
		internal static bool IsDefined(WSFederationHttpSecurityMode value)
		{
			return value == WSFederationHttpSecurityMode.None || value == WSFederationHttpSecurityMode.Message || value == WSFederationHttpSecurityMode.TransportWithMessageCredential;
		}
	}
}
