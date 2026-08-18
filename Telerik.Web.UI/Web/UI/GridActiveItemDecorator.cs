using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200111F RID: 4383
	internal class GridActiveItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B33F RID: 45887 RVA: 0x002707BE File Offset: 0x0026E9BE
		public GridActiveItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B340 RID: 45888 RVA: 0x002707C7 File Offset: 0x0026E9C7
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			base.Item.MergeStyle(owner.RenderActiveItemStyle);
		}
	}
}
