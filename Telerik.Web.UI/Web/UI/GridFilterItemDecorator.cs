using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200111C RID: 4380
	internal class GridFilterItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B334 RID: 45876 RVA: 0x0027061C File Offset: 0x0026E81C
		public GridFilterItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B335 RID: 45877 RVA: 0x00270628 File Offset: 0x0026E828
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			if (!owner.ShowHeader)
			{
				base.Item.Style["display"] = "none";
				return;
			}
			if (!base.Item.Expanded)
			{
				base.Item.Style["display"] = "none";
			}
			base.DecorateItem(owner, columnArray);
		}

		// Token: 0x0600B336 RID: 45878 RVA: 0x00270687 File Offset: 0x0026E887
		public override void SetItemStyle(GridTableView owner)
		{
			base.Item.MergeStyle(owner.RenderFilterItemStyle);
		}

		// Token: 0x0600B337 RID: 45879 RVA: 0x0027069A File Offset: 0x0026E89A
		public override void PrepareCellInColumn(GridTableView owner, GridColumn column, TableCell cell)
		{
			GridGroupSplitterColumn gridGroupSplitterColumn = column as GridGroupSplitterColumn;
			cell.Height = Unit.Empty;
		}

		// Token: 0x0600B338 RID: 45880 RVA: 0x002706AE File Offset: 0x0026E8AE
		public override void PrepareCell(GridTableView owner, TableCell cell)
		{
			if (owner.RenderFilterItemStyle.IsDefault)
			{
				cell.MergeStyle(owner.RenderHeaderStyle);
			}
		}
	}
}
