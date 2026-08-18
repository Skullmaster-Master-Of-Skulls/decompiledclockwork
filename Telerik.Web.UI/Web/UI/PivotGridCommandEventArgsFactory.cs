using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C32 RID: 3122
	public class PivotGridCommandEventArgsFactory
	{
		// Token: 0x06007662 RID: 30306 RVA: 0x001B7958 File Offset: 0x001B5B58
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		public static PivotGridCommandEventArgs CreateCommandEventArgs(PivotGridItem item, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			RadPivotGrid ownerPivotGrid = item.OwnerPivotGrid;
			Dictionary<string, Unit> resizedColumnsWidth = ownerPivotGrid.ResizedColumnsWidth;
			ownerPivotGrid.ResizedColumnsWidth = new Dictionary<string, Unit>();
			if (string.Compare(commandName, "Page", true) == 0)
			{
				ownerPivotGrid.ResizedColumnsWidth = resizedColumnsWidth;
				return new PivotGridPageChangedEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "PageSizeChanged", true) == 0)
			{
				ownerPivotGrid.ResizedColumnsWidth = resizedColumnsWidth;
				return new PivotGridPageSizeChangedEventArgs("ChangePageSize", originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ExpandCollapse", true) == 0)
			{
				return new PivotGridExpandCollapseEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ExpandCollapseLevel", true) == 0)
			{
				return new PivotGridExpandCollapseLevelEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Select", true) == 0)
			{
				throw new NotImplementedException();
			}
			if (string.Compare(commandName, "Deselect", true) == 0)
			{
				throw new NotImplementedException();
			}
			if (string.Compare(commandName, "SelectAll", true) == 0)
			{
				throw new NotImplementedException();
			}
			if (string.Compare(commandName, "DeselectAll", true) == 0)
			{
				throw new NotImplementedException();
			}
			if (string.Compare(commandName, "Sort", true) == 0)
			{
				ownerPivotGrid.ResizedColumnsWidth = resizedColumnsWidth;
				return new PivotGridSortEventArgs(item.OwnerPivotGrid, item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "FieldReorder", true) == 0)
			{
				return new PivotGridFieldReorderEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ShowHideField", true) == 0)
			{
				return new PivotGridShowHideFieldEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "UpdateLayout", true) == 0)
			{
				return new PivotGridUpdateLayoutEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "AggregateChange", true) == 0)
			{
				return new PivotGridAggregateLabelChangeEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "InitFilterDialogue", true) == 0)
			{
				ownerPivotGrid.ResizedColumnsWidth = resizedColumnsWidth;
				return new PivotGridInitFilterDialogueEventArgs(item.OwnerPivotGrid, item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Filter", true) == 0)
			{
				return new PivotGridFilterCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "AggregateFunctionChanged", true) == 0)
			{
				return new PivotGridAggregateFunctionChangedEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			return new PivotGridCommandEventArgs(item, commandSource, originalArgs);
		}
	}
}
