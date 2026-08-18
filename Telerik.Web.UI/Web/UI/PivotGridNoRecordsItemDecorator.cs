using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DCA RID: 3530
	public class PivotGridNoRecordsItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x06008382 RID: 33666 RVA: 0x001DFA0A File Offset: 0x001DDC0A
		public PivotGridNoRecordsItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x06008383 RID: 33667 RVA: 0x001DFA13 File Offset: 0x001DDC13
		public override void DecorateItem(RadPivotGrid owner)
		{
			base.Item.CssClass = "rpgNoRecords";
		}
	}
}
