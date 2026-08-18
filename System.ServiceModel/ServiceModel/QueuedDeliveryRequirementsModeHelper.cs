using System;

namespace System.ServiceModel
{
	// Token: 0x020000CE RID: 206
	internal static class QueuedDeliveryRequirementsModeHelper
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00015258 File Offset: 0x00013458
		public static bool IsDefined(QueuedDeliveryRequirementsMode x)
		{
			return x == QueuedDeliveryRequirementsMode.Allowed || x == QueuedDeliveryRequirementsMode.Required || x == QueuedDeliveryRequirementsMode.NotAllowed;
		}
	}
}
