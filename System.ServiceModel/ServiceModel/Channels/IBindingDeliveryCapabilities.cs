using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006FB RID: 1787
	public interface IBindingDeliveryCapabilities
	{
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600447E RID: 17534
		bool AssuresOrderedDelivery { get; }

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600447F RID: 17535
		bool QueuedDelivery { get; }
	}
}
