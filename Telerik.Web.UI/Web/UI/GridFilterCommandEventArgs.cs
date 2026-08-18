using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010E1 RID: 4321
	public class GridFilterCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0EB RID: 45291 RVA: 0x00264EB8 File Offset: 0x002630B8
		public GridFilterCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "Filter", argument)
		{
		}

		// Token: 0x0600B0EC RID: 45292 RVA: 0x00264EC8 File Offset: 0x002630C8
		public override void ExecuteCommand(object source)
		{
			if (base.Item.OwnerTableView.IsDataSourceViewWithFiltering())
			{
				base.Item.OwnerTableView.CurrentPageIndex = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			Pair pair = (Pair)base.CommandArgument;
			string text = (string)pair.First;
			string text2 = (string)pair.Second;
			bool filterByCalculatedColumn = false;
			string value = string.Empty;
			foreach (GridColumn gridColumn in base.Item.OwnerTableView.RenderColumns)
			{
				if (gridColumn.SupportsFiltering())
				{
					if (text2 == gridColumn.UniqueName)
					{
						gridColumn.RefreshCurrentFilterValue(base.Item as GridFilteringItem, text);
						value = gridColumn.CurrentFilterValue;
					}
					else
					{
						gridColumn.RefreshCurrentFilterValue(base.Item as GridFilteringItem);
					}
					string text3 = gridColumn.EvaluateFilterExpression(base.Item as GridFilteringItem);
					if (string.IsNullOrEmpty(text3))
					{
						gridColumn.ResetCurrentFilterValue(base.Item as GridFilteringItem);
					}
					else
					{
						if (gridColumn is GridCalculatedColumn)
						{
							filterByCalculatedColumn = true;
						}
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(" AND ");
						}
						stringBuilder.AppendFormat("({0})", text3);
					}
				}
			}
			base.Item.OwnerTableView.FilterByCalculatedColumn = filterByCalculatedColumn;
			base.Item.OwnerTableView.FilterExpression = stringBuilder.ToString();
			base.Item.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst;
			base.Item.OwnerTableView.OwnerGrid.TrackFiltering(text2, text, value);
			base.Item.OwnerTableView.Rebind();
		}
	}
}
