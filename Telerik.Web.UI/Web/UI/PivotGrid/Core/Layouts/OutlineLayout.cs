using System;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CEF RID: 3311
	internal class OutlineLayout : CompactLayout
	{
		// Token: 0x06007BA7 RID: 31655 RVA: 0x001C64DA File Offset: 0x001C46DA
		public OutlineLayout(IHierarchyAdapter adapter) : base(adapter)
		{
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x001C64E3 File Offset: 0x001C46E3
		internal override int GetLayoutLevel(ItemInfo itemInfo, GroupInfo parentGroupInfo)
		{
			return base.GetIndent(itemInfo, parentGroupInfo);
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x001C64ED File Offset: 0x001C46ED
		internal override int GetIndent(ItemInfo itemInfo, GroupInfo parentGroupInfo)
		{
			return 0;
		}
	}
}
