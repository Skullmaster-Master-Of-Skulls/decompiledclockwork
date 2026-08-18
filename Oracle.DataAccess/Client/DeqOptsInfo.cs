using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000CF RID: 207
	internal enum DeqOptsInfo
	{
		// Token: 0x0400064D RID: 1613
		None,
		// Token: 0x0400064E RID: 1614
		ConsumerName,
		// Token: 0x0400064F RID: 1615
		Correlation,
		// Token: 0x04000650 RID: 1616
		DeliveryMode = 4,
		// Token: 0x04000651 RID: 1617
		DequeueMode = 8,
		// Token: 0x04000652 RID: 1618
		MessageId = 16,
		// Token: 0x04000653 RID: 1619
		NavigationMode = 32,
		// Token: 0x04000654 RID: 1620
		Visibility = 64,
		// Token: 0x04000655 RID: 1621
		Wait = 128,
		// Token: 0x04000656 RID: 1622
		All = 65535
	}
}
