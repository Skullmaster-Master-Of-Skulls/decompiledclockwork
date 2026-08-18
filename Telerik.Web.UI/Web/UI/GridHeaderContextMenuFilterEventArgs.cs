using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010E3 RID: 4323
	public class GridHeaderContextMenuFilterEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0EF RID: 45295 RVA: 0x0026511F File Offset: 0x0026331F
		public GridHeaderContextMenuFilterEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "HeaderContextMenuFilter", argument)
		{
		}

		// Token: 0x0600B0F0 RID: 45296 RVA: 0x00265130 File Offset: 0x00263330
		public override void ExecuteCommand(object source)
		{
			if (base.Item.OwnerTableView.IsDataSourceViewWithFiltering())
			{
				base.Item.OwnerTableView.CurrentPageIndex = 0;
			}
			string text = string.Empty;
			Triplet triplet = (Triplet)base.CommandArgument;
			string text2 = triplet.First.ToString();
			string text3 = ((Pair)triplet.Second).First.ToString();
			string text4 = ((Pair)triplet.Second).Second.ToString();
			string value = ((Pair)triplet.Third).First.ToString();
			string andCurrentFilterValue = ((Pair)triplet.Third).Second.ToString();
			foreach (GridColumn gridColumn in base.Item.OwnerTableView.RenderColumns)
			{
				if (gridColumn.SupportsFiltering())
				{
					if (gridColumn.UniqueName == text2)
					{
						gridColumn.CurrentFilterValue = text4;
						gridColumn.CurrentFilterFunction = (GridKnownFunction)Enum.Parse(typeof(GridKnownFunction), text3);
						gridColumn.AndCurrentFilterValue = andCurrentFilterValue;
						gridColumn.AndCurrentFilterFunction = (GridKnownFunction)Enum.Parse(typeof(GridKnownFunction), value);
					}
					string text5 = gridColumn.EvaluateFilterExpression();
					if (string.IsNullOrEmpty(text5))
					{
						gridColumn.ResetCurrentFilterValue();
					}
					else
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += " AND ";
						}
						text = text + "(" + text5 + ")";
					}
				}
			}
			base.Item.OwnerTableView.FilterExpression = text;
			base.Item.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst;
			base.Item.OwnerTableView.OwnerGrid.TrackFiltering(text2, text3, text4);
			base.Item.OwnerTableView.Rebind();
		}
	}
}
