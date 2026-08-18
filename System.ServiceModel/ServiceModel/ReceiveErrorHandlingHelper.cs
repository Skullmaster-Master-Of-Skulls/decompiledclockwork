using System;

namespace System.ServiceModel
{
	// Token: 0x020000B3 RID: 179
	internal static class ReceiveErrorHandlingHelper
	{
		// Token: 0x06000307 RID: 775 RVA: 0x00011E60 File Offset: 0x00010060
		internal static bool IsDefined(ReceiveErrorHandling value)
		{
			return value == ReceiveErrorHandling.Fault || value == ReceiveErrorHandling.Drop || value == ReceiveErrorHandling.Reject || value == ReceiveErrorHandling.Move;
		}
	}
}
