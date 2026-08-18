using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F22 RID: 3874
	public class TreeListReorderedColumn
	{
		// Token: 0x060093DE RID: 37854 RVA: 0x00212D53 File Offset: 0x00210F53
		public TreeListReorderedColumn(TreeListColumn column, int index)
		{
			this.TreeListColumn = column;
			this.OldOrderIndex = index;
		}

		// Token: 0x17002EC5 RID: 11973
		// (get) Token: 0x060093DF RID: 37855 RVA: 0x00212D69 File Offset: 0x00210F69
		// (set) Token: 0x060093E0 RID: 37856 RVA: 0x00212D71 File Offset: 0x00210F71
		public TreeListColumn TreeListColumn { get; set; }

		// Token: 0x17002EC6 RID: 11974
		// (get) Token: 0x060093E1 RID: 37857 RVA: 0x00212D7A File Offset: 0x00210F7A
		// (set) Token: 0x060093E2 RID: 37858 RVA: 0x00212D82 File Offset: 0x00210F82
		public int OldOrderIndex { get; set; }
	}
}
