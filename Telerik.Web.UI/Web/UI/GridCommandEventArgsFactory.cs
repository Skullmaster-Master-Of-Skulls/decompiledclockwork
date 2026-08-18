using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010D6 RID: 4310
	public class GridCommandEventArgsFactory
	{
		// Token: 0x0600B0B6 RID: 45238 RVA: 0x002635AC File Offset: 0x002617AC
		public static GridCommandEventArgs CreateGridCommandEventArgs(GridItem item, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			if (string.Compare(commandName, "Select", true) == 0)
			{
				return new GridSelectCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Deselect", true) == 0)
			{
				return new GridDeselectCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Sort", true) == 0)
			{
				return new GridSortCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ClearSort", true) == 0)
			{
				return new GridClearSortCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "HeaderSort", true) == 0)
			{
				return new GridHeaderSortCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Page", true) == 0)
			{
				return new GridPageChangedEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ExpandCollapse", true) == 0)
			{
				return new GridExpandCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ExpandCollapseAll", true) == 0)
			{
				return new GridExpandCollapseAllEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "GroupsCustomExpandCollapse", true) == 0)
			{
				return new GridGroupsCustomExpandCollapse(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "GroupsExpandAll", true) == 0)
			{
				return new GridGroupsExpandAllEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "Filter", true) == 0)
			{
				return new GridFilterCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "ClearFilter", true) == 0)
			{
				return new GridClearFilterCommandEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "HeaderContextMenuFilter", true) == 0)
			{
				return new GridHeaderContextMenuFilterEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			if (string.Compare(commandName, "DownloadAttachment", true) == 0)
			{
				IDictionary dictionary;
				if (originalArgs.CommandArgument is IDictionary)
				{
					dictionary = (originalArgs.CommandArgument as IDictionary);
				}
				else
				{
					dictionary = GridAttachmentColumn.DeserializeDownloadArgument(originalArgs.CommandArgument.ToString());
				}
				GridAttachmentColumn column;
				if (dictionary["ColumnUniqueName"] != null)
				{
					string columnName = dictionary["ColumnUniqueName"].ToString();
					column = GridCommandEventArgsFactory.RetrieveAttachmentColumn(columnName, item);
				}
				else
				{
					column = GridAttachmentColumn.GetFirstAttachmentColumn(item.OwnerTableView);
				}
				return new GridDownloadAttachmentCommandEventArgs(item, commandSource, dictionary, column);
			}
			GridCommandEventArgs gridCommandEventArgs = new GridCommandEventArgs(item, commandSource, originalArgs);
			gridCommandEventArgs.Canceled = !item.OwnerTableView.OwnerGrid.ValidationSettings.ValidateCommandName(gridCommandEventArgs.CommandName);
			return gridCommandEventArgs;
		}

		// Token: 0x0600B0B7 RID: 45239 RVA: 0x002637DC File Offset: 0x002619DC
		private static GridAttachmentColumn RetrieveAttachmentColumn(string columnName, GridItem item)
		{
			GridColumn columnSafe = item.OwnerTableView.GetColumnSafe(columnName);
			return columnSafe as GridAttachmentColumn;
		}
	}
}
