using System;

namespace System.Net.Mail
{
	// Token: 0x0200069F RID: 1695
	[Flags]
	public enum DeliveryNotificationOptions
	{
		// Token: 0x0400303E RID: 12350
		None = 0,
		// Token: 0x0400303F RID: 12351
		OnSuccess = 1,
		// Token: 0x04003040 RID: 12352
		OnFailure = 2,
		// Token: 0x04003041 RID: 12353
		Delay = 4,
		// Token: 0x04003042 RID: 12354
		Never = 134217728
	}
}
