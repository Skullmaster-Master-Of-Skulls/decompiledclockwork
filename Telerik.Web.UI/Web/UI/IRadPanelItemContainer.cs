using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200064A RID: 1610
	public interface IRadPanelItemContainer
	{
		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06003AD7 RID: 15063
		IRadPanelItemContainer Owner { get; }

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06003AD8 RID: 15064
		RadPanelItemCollection Items { get; }
	}
}
