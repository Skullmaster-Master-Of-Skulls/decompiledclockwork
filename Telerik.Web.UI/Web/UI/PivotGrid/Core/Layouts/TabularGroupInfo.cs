using System;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CF0 RID: 3312
	internal class TabularGroupInfo
	{
		// Token: 0x06007BAA RID: 31658 RVA: 0x001C64F0 File Offset: 0x001C46F0
		public TabularGroupInfo(object item, TabularGroupInfo parent, bool isExpanded, int level, int line, int index, int lastSubItemSlot)
		{
			this.Line = line;
			this.Item = item;
			this.Parent = parent;
			this.IsExpanded = isExpanded;
			this.Level = level;
			this.Index = index;
			this.LastSubItemSlot = lastSubItemSlot;
		}

		// Token: 0x17002785 RID: 10117
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x001C652D File Offset: 0x001C472D
		// (set) Token: 0x06007BAC RID: 31660 RVA: 0x001C6535 File Offset: 0x001C4735
		public int Line { get; set; }

		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x001C653E File Offset: 0x001C473E
		// (set) Token: 0x06007BAE RID: 31662 RVA: 0x001C6546 File Offset: 0x001C4746
		public TabularGroupInfo Parent { get; private set; }

		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x001C654F File Offset: 0x001C474F
		// (set) Token: 0x06007BB0 RID: 31664 RVA: 0x001C6557 File Offset: 0x001C4757
		public object Item { get; private set; }

		// Token: 0x17002788 RID: 10120
		// (get) Token: 0x06007BB1 RID: 31665 RVA: 0x001C6560 File Offset: 0x001C4760
		// (set) Token: 0x06007BB2 RID: 31666 RVA: 0x001C6568 File Offset: 0x001C4768
		public int LastSubItemSlot { get; set; }

		// Token: 0x17002789 RID: 10121
		// (get) Token: 0x06007BB3 RID: 31667 RVA: 0x001C6571 File Offset: 0x001C4771
		// (set) Token: 0x06007BB4 RID: 31668 RVA: 0x001C6579 File Offset: 0x001C4779
		public int Level { get; private set; }

		// Token: 0x1700278A RID: 10122
		// (get) Token: 0x06007BB5 RID: 31669 RVA: 0x001C6582 File Offset: 0x001C4782
		// (set) Token: 0x06007BB6 RID: 31670 RVA: 0x001C658A File Offset: 0x001C478A
		public int Index { get; set; }

		// Token: 0x1700278B RID: 10123
		// (get) Token: 0x06007BB7 RID: 31671 RVA: 0x001C6593 File Offset: 0x001C4793
		// (set) Token: 0x06007BB8 RID: 31672 RVA: 0x001C659B File Offset: 0x001C479B
		public bool IsExpanded { get; set; }

		// Token: 0x06007BB9 RID: 31673 RVA: 0x001C65A4 File Offset: 0x001C47A4
		internal bool IsVisible()
		{
			return this.Parent == null || (this.Parent.IsExpanded && this.Parent.IsVisible());
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x001C65CA File Offset: 0x001C47CA
		internal int GetLineSpan()
		{
			return this.LastSubItemSlot - this.Line + 1;
		}
	}
}
