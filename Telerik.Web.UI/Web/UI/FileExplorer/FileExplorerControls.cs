using System;

namespace Telerik.Web.UI.FileExplorer
{
	// Token: 0x0200184C RID: 6220
	[Flags]
	public enum FileExplorerControls
	{
		// Token: 0x04004580 RID: 17792
		TreeView = 1,
		// Token: 0x04004581 RID: 17793
		Grid = 2,
		// Token: 0x04004582 RID: 17794
		Toolbar = 4,
		// Token: 0x04004583 RID: 17795
		AddressBox = 8,
		// Token: 0x04004584 RID: 17796
		ContextMenus = 16,
		// Token: 0x04004585 RID: 17797
		ListView = 32,
		// Token: 0x04004586 RID: 17798
		FileList = 34,
		// Token: 0x04004587 RID: 17799
		All = 65535
	}
}
