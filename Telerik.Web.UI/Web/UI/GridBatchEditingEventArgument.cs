using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020004AE RID: 1198
	public class GridBatchEditingEventArgument
	{
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x0008A0A4 File Offset: 0x000882A4
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x0008A0AC File Offset: 0x000882AC
		public Hashtable OldValues { get; set; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x0008A0B5 File Offset: 0x000882B5
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x0008A0BD File Offset: 0x000882BD
		public Hashtable NewValues { get; set; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x0008A0C6 File Offset: 0x000882C6
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x0008A0CE File Offset: 0x000882CE
		public GridTableView OwnerTableView { get; set; }
	}
}
