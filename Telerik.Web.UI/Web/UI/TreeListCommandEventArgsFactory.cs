using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200120C RID: 4620
	public static class TreeListCommandEventArgsFactory
	{
		// Token: 0x0600BF10 RID: 48912 RVA: 0x002A533C File Offset: 0x002A353C
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		public static TreeListCommandEventArgs CreateCommandEventArgs(TreeListItem item, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			RadTreeList ownerTreeList = item.OwnerTreeList;
			if (string.Compare(commandName, "Page", true) == 0)
			{
				return new TreeListPageChangedEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ExpandCollapse", true) == 0)
			{
				return new TreeListExpandCollapseEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Select", true) == 0)
			{
				return new TreeListSelectEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Deselect", true) == 0)
			{
				return new TreeListDeselectEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "SelectAll", true) == 0)
			{
				return new TreeListSelectAllEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "DeselectAll", true) == 0)
			{
				return new TreeListDeselectAllEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Sort", true) == 0)
			{
				RadTreeList ownerTreeList2 = (item != null) ? ownerTreeList : null;
				return new TreeListSortEventArgs(ownerTreeList2, item, commandSource, originalArgs.CommandArgument);
			}
			TreeListCommandEventArgs treeListCommandEventArgs = new TreeListCommandEventArgs(item, commandSource, originalArgs);
			treeListCommandEventArgs.Canceled = !item.OwnerTreeList.ValidationSettings.ValidateCommandName(treeListCommandEventArgs.CommandName);
			return treeListCommandEventArgs;
		}

		// Token: 0x0600BF11 RID: 48913 RVA: 0x002A5450 File Offset: 0x002A3650
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
		internal static bool HandleCommand(RadTreeList ownerTreeList, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			bool result = false;
			if (string.Compare(commandName, "RebindTreeList", true) == 0)
			{
				ownerTreeList.Rebind();
				result = true;
			}
			else if (string.Compare(commandName, "Sort", true) == 0)
			{
				TreeListSortEventArgs.HandleSorting(ownerTreeList, commandSource, (string)originalArgs.CommandArgument);
				result = true;
			}
			return result;
		}
	}
}
