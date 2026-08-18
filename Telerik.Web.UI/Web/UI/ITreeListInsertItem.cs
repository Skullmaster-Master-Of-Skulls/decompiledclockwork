using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200124D RID: 4685
	public interface ITreeListInsertItem
	{
		// Token: 0x17003E4E RID: 15950
		// (get) Token: 0x0600C13D RID: 49469
		TreeListDataItem ParentItem { get; }

		// Token: 0x17003E4F RID: 15951
		// (get) Token: 0x0600C13E RID: 49470
		bool IsRoot { get; }
	}
}
