using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000949 RID: 2377
	public interface IRadToolBarButtonContainer : IRadToolBarItemContainer, IControlItemContainer
	{
		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06005A9C RID: 23196
		RadToolBarButtonCollection Buttons { get; }

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x06005A9D RID: 23197
		Unit DropDownHeight { get; }

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x06005A9E RID: 23198
		Unit DropDownWidth { get; }
	}
}
