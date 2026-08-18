using System;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CEC RID: 3308
	internal class GroupInfo
	{
		// Token: 0x06007B98 RID: 31640 RVA: 0x001C6408 File Offset: 0x001C4608
		public GroupInfo(object item, GroupInfo parent, bool isExpanded, int level, int index, int lastSubItemSlot)
		{
			this.Item = item;
			this.Parent = parent;
			this.IsExpanded = isExpanded;
			this.Level = level;
			this.Index = index;
			this.LastSubItemSlot = lastSubItemSlot;
		}

		// Token: 0x1700277F RID: 10111
		// (get) Token: 0x06007B99 RID: 31641 RVA: 0x001C643D File Offset: 0x001C463D
		// (set) Token: 0x06007B9A RID: 31642 RVA: 0x001C6445 File Offset: 0x001C4645
		public GroupInfo Parent { get; private set; }

		// Token: 0x17002780 RID: 10112
		// (get) Token: 0x06007B9B RID: 31643 RVA: 0x001C644E File Offset: 0x001C464E
		// (set) Token: 0x06007B9C RID: 31644 RVA: 0x001C6456 File Offset: 0x001C4656
		public object Item { get; private set; }

		// Token: 0x17002781 RID: 10113
		// (get) Token: 0x06007B9D RID: 31645 RVA: 0x001C645F File Offset: 0x001C465F
		// (set) Token: 0x06007B9E RID: 31646 RVA: 0x001C6467 File Offset: 0x001C4667
		public int LastSubItemSlot { get; set; }

		// Token: 0x17002782 RID: 10114
		// (get) Token: 0x06007B9F RID: 31647 RVA: 0x001C6470 File Offset: 0x001C4670
		// (set) Token: 0x06007BA0 RID: 31648 RVA: 0x001C6478 File Offset: 0x001C4678
		public int Level { get; private set; }

		// Token: 0x17002783 RID: 10115
		// (get) Token: 0x06007BA1 RID: 31649 RVA: 0x001C6481 File Offset: 0x001C4681
		// (set) Token: 0x06007BA2 RID: 31650 RVA: 0x001C6489 File Offset: 0x001C4689
		public int Index { get; set; }

		// Token: 0x17002784 RID: 10116
		// (get) Token: 0x06007BA3 RID: 31651 RVA: 0x001C6492 File Offset: 0x001C4692
		// (set) Token: 0x06007BA4 RID: 31652 RVA: 0x001C649A File Offset: 0x001C469A
		public bool IsExpanded { get; set; }

		// Token: 0x06007BA5 RID: 31653 RVA: 0x001C64A3 File Offset: 0x001C46A3
		internal int GetLineSpan()
		{
			return this.LastSubItemSlot - this.Index + 1;
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x001C64B4 File Offset: 0x001C46B4
		internal bool IsVisible()
		{
			return this.Parent == null || (this.Parent.IsExpanded && this.Parent.IsVisible());
		}
	}
}
