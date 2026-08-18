using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001944 RID: 6468
	public static class RadListViewCommandEventArgsFactory
	{
		// Token: 0x0600FA71 RID: 64113 RVA: 0x00386698 File Offset: 0x00384898
		public static RadListViewCommandEventArgs CreateCommandEventArgs(RadListViewItem item, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			if (string.Compare(commandName, "Page", true) == 0)
			{
				return new RadListViewPageChangedEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			RadListView ownerListView = item.OwnerListView;
			if (string.Compare(commandName, "Sort", true) == 0)
			{
				RadListView ownerListView2 = (item != null) ? ownerListView : null;
				return new RadListViewSortEventArgs(ownerListView2, item, commandSource, originalArgs.CommandArgument);
			}
			RadListViewDataItem item2 = item as RadListViewDataItem;
			if (string.Compare(commandName, "Select", true) == 0)
			{
				return new RadListViewSelectCommandEventArgs(item2, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Deselect", true) == 0)
			{
				return new RadListViewDeselectCommandEventArgs(item2, commandSource, originalArgs.CommandArgument);
			}
			RadListViewCommandEventArgs radListViewCommandEventArgs = new RadListViewCommandEventArgs(item, commandSource, originalArgs);
			radListViewCommandEventArgs.Canceled = !ownerListView.ValidationSettings.ValidateCommand(radListViewCommandEventArgs.CommandName);
			return radListViewCommandEventArgs;
		}

		// Token: 0x0600FA72 RID: 64114 RVA: 0x0038675C File Offset: 0x0038495C
		internal static bool HandleCommand(RadListView ownerListView, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			bool result = false;
			if (string.Compare(commandName, "Page", true) == 0)
			{
				RadListViewPageChangedEventArgs.HandlePaging(ownerListView, commandSource, (string)originalArgs.CommandArgument);
				result = true;
			}
			else if (string.Compare(commandName, "Sort", true) == 0)
			{
				RadListViewSortEventArgs.HandleSorting(ownerListView, commandSource, (string)originalArgs.CommandArgument);
				result = true;
			}
			else if (string.Compare(commandName, "InitInsert", true) == 0)
			{
				ownerListView.ShowInsertItem();
				result = true;
			}
			else if (string.Compare(commandName, "RebindListView", true) == 0)
			{
				ownerListView.Rebind();
				result = true;
			}
			return result;
		}
	}
}
