using System;

namespace Telerik.Web.UI
{
	// Token: 0x020005D4 RID: 1492
	public interface IRadMenuItemContainer
	{
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060035C1 RID: 13761
		IRadMenuItemContainer Owner { get; }

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060035C2 RID: 13762
		RadMenuItemCollection Items { get; }
	}
}
