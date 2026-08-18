using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010E2 RID: 4322
	public class GridClearFilterCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0ED RID: 45293 RVA: 0x0026506B File Offset: 0x0026326B
		public GridClearFilterCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "ClearFilter", argument)
		{
		}

		// Token: 0x0600B0EE RID: 45294 RVA: 0x0026507C File Offset: 0x0026327C
		public override void ExecuteCommand(object source)
		{
			foreach (GridColumn gridColumn in base.Item.OwnerTableView.RenderColumns)
			{
				if (gridColumn.SupportsFiltering())
				{
					gridColumn.CurrentFilterValue = string.Empty;
					gridColumn.CurrentFilterFunction = GridKnownFunction.NoFilter;
				}
			}
			if (base.Item.OwnerTableView.IsDataSourceViewWithFiltering())
			{
				base.Item.OwnerTableView.CurrentPageIndex = 0;
			}
			base.Item.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst;
			base.Item.OwnerTableView.FilterExpression = string.Empty;
			base.Item.OwnerTableView.Rebind();
		}
	}
}
