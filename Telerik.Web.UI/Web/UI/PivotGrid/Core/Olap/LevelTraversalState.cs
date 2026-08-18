using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D14 RID: 3348
	internal class LevelTraversalState
	{
		// Token: 0x06007CC0 RID: 31936 RVA: 0x001CA0B6 File Offset: 0x001C82B6
		public LevelTraversalState(HierarchyTraversalState hierarchy)
		{
			this.Hierarchy = hierarchy;
		}

		// Token: 0x170027CC RID: 10188
		// (get) Token: 0x06007CC1 RID: 31937 RVA: 0x001CA0C5 File Offset: 0x001C82C5
		// (set) Token: 0x06007CC2 RID: 31938 RVA: 0x001CA0CD File Offset: 0x001C82CD
		public HierarchyTraversalState Hierarchy { get; private set; }

		// Token: 0x170027CD RID: 10189
		// (get) Token: 0x06007CC3 RID: 31939 RVA: 0x001CA0D6 File Offset: 0x001C82D6
		// (set) Token: 0x06007CC4 RID: 31940 RVA: 0x001CA0DE File Offset: 0x001C82DE
		public Group Group { get; set; }

		// Token: 0x170027CE RID: 10190
		// (get) Token: 0x06007CC5 RID: 31941 RVA: 0x001CA0E7 File Offset: 0x001C82E7
		// (set) Token: 0x06007CC6 RID: 31942 RVA: 0x001CA0EF File Offset: 0x001C82EF
		public string LevelName { get; set; }

		// Token: 0x170027CF RID: 10191
		// (get) Token: 0x06007CC7 RID: 31943 RVA: 0x001CA0F8 File Offset: 0x001C82F8
		// (set) Token: 0x06007CC8 RID: 31944 RVA: 0x001CA100 File Offset: 0x001C8300
		public string UniqueName { get; set; }

		// Token: 0x170027D0 RID: 10192
		// (get) Token: 0x06007CC9 RID: 31945 RVA: 0x001CA109 File Offset: 0x001C8309
		// (set) Token: 0x06007CCA RID: 31946 RVA: 0x001CA111 File Offset: 0x001C8311
		public int LevelNumber { get; set; }

		// Token: 0x170027D1 RID: 10193
		// (get) Token: 0x06007CCB RID: 31947 RVA: 0x001CA11A File Offset: 0x001C831A
		public bool IsTotal
		{
			get
			{
				return this.Hierarchy != null && this.Hierarchy.AllMemberName == this.UniqueName;
			}
		}
	}
}
