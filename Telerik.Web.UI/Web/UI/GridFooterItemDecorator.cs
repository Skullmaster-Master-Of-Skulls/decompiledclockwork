using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001120 RID: 4384
	internal class GridFooterItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B341 RID: 45889 RVA: 0x002707DA File Offset: 0x0026E9DA
		public GridFooterItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B342 RID: 45890 RVA: 0x002707E3 File Offset: 0x0026E9E3
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
			if (!owner.ShowFooter)
			{
				base.Item.Visible = false;
				return;
			}
			base.SetItemVisibility(owner, columnArray);
		}

		// Token: 0x0600B343 RID: 45891 RVA: 0x00270802 File Offset: 0x0026EA02
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			base.DecorateItem(owner, columnArray);
		}

		// Token: 0x0600B344 RID: 45892 RVA: 0x0027080C File Offset: 0x0026EA0C
		public override void SetItemStyle(GridTableView owner)
		{
			base.Item.MergeStyle(owner.RenderFooterStyle);
			if (!base.Item.CssClass.Contains("rgFooter"))
			{
				GridItem item = base.Item;
				item.CssClass += " rgFooter";
			}
		}

		// Token: 0x0600B345 RID: 45893 RVA: 0x0027085C File Offset: 0x0026EA5C
		public override void PrepareCellInColumn(GridTableView owner, GridColumn column, TableCell cell)
		{
			cell.MergeStyle(column.FooterStyleInternal);
		}
	}
}
