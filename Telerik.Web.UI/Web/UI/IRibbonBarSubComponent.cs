using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007C3 RID: 1987
	public interface IRibbonBarSubComponent
	{
		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004555 RID: 17749
		RadRibbonBar RibbonBar { get; }

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004556 RID: 17750
		WebControl ParentWebControl { get; }
	}
}
