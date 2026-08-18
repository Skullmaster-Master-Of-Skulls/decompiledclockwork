using System;

namespace System.Net.Mail
{
	// Token: 0x0200026F RID: 623
	[Flags]
	public enum DeliveryNotificationOptions
	{
		// Token: 0x040017DB RID: 6107
		None = 0,
		// Token: 0x040017DC RID: 6108
		OnSuccess = 1,
		// Token: 0x040017DD RID: 6109
		OnFailure = 2,
		// Token: 0x040017DE RID: 6110
		Delay = 4,
		// Token: 0x040017DF RID: 6111
		Never = 134217728
	}
}
