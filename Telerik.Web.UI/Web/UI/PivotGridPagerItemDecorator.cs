using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DCB RID: 3531
	public class PivotGridPagerItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x06008384 RID: 33668 RVA: 0x001DFA25 File Offset: 0x001DDC25
		public PivotGridPagerItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x06008385 RID: 33669 RVA: 0x001DFA2E File Offset: 0x001DDC2E
		protected override void SetItemStyle(RadPivotGrid owner)
		{
			base.Item.MergeStyle(owner.PagerStyle);
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x001DFA44 File Offset: 0x001DDC44
		public override void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			PivotGridPagerItem pivotGridPagerItem = (PivotGridPagerItem)base.Item;
			pivotGridPagerItem.PagerContentCell.CssClass = "rpgPagerCell";
			PivotGridPagerItem pivotGridPagerItem2 = pivotGridPagerItem;
			pivotGridPagerItem2.CssClass += string.Format(" {0}", pivotGridPagerItem.IsTopItem ? "rpgPagerTop" : "rpgPagerBottom");
		}
	}
}
