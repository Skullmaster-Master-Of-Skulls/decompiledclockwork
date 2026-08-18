using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200120B RID: 4619
	public class TreeListCommandEventArgs : CommandEventArgs, ITreeListCommandEvent
	{
		// Token: 0x17003DA5 RID: 15781
		// (get) Token: 0x0600BF07 RID: 48903 RVA: 0x002A500D File Offset: 0x002A320D
		// (set) Token: 0x0600BF08 RID: 48904 RVA: 0x002A5015 File Offset: 0x002A3215
		public virtual TreeListItem Item { get; set; }

		// Token: 0x17003DA6 RID: 15782
		// (get) Token: 0x0600BF09 RID: 48905 RVA: 0x002A501E File Offset: 0x002A321E
		// (set) Token: 0x0600BF0A RID: 48906 RVA: 0x002A5026 File Offset: 0x002A3226
		public object EventSource { get; set; }

		// Token: 0x0600BF0B RID: 48907 RVA: 0x002A502F File Offset: 0x002A322F
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListCommandEventArgs(TreeListItem treeListItem, object eventSource, CommandEventArgs args) : base(args)
		{
			this.Item = treeListItem;
			this.EventSource = eventSource;
		}

		// Token: 0x0600BF0C RID: 48908 RVA: 0x002A5046 File Offset: 0x002A3246
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal TreeListCommandEventArgs(TreeListItem treeListItem, object eventSource, string name, object argument) : base(name, argument)
		{
			this.Item = treeListItem;
			this.EventSource = eventSource;
		}

		// Token: 0x17003DA7 RID: 15783
		// (get) Token: 0x0600BF0D RID: 48909 RVA: 0x002A505F File Offset: 0x002A325F
		// (set) Token: 0x0600BF0E RID: 48910 RVA: 0x002A5067 File Offset: 0x002A3267
		public virtual bool Canceled { get; set; }

		// Token: 0x0600BF0F RID: 48911 RVA: 0x002A5070 File Offset: 0x002A3270
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.String.Compare(System.String,System.String,System.Boolean,System.Globalization.CultureInfo)")]
		public virtual void ExecuteCommand(object source)
		{
			string commandName = base.CommandName;
			RadTreeList ownerTreeList = this.Item.OwnerTreeList;
			if (string.Compare(commandName, "RebindTreeList", true, CultureInfo.InvariantCulture) == 0)
			{
				ownerTreeList.Rebind();
			}
			if (string.Compare(commandName, "ExportToExcel", true, CultureInfo.InvariantCulture) == 0)
			{
				if (this.Canceled)
				{
					return;
				}
				ownerTreeList.ExportToExcel();
			}
			if (string.Compare(commandName, "ExportToWord", true, CultureInfo.InvariantCulture) == 0)
			{
				if (this.Canceled)
				{
					return;
				}
				ownerTreeList.ExportToWord();
			}
			if (string.Compare(commandName, "ExportToPdf", true, CultureInfo.InvariantCulture) == 0)
			{
				if (this.Canceled)
				{
					return;
				}
				ownerTreeList.ExportToPdf();
			}
			if (string.Compare(commandName, "Edit", true, CultureInfo.InvariantCulture) == 0)
			{
				ownerTreeList.CallOnEditCommand(this);
				if (this.Canceled)
				{
					return;
				}
				TreeListEditableItem treeListEditableItem = this.Item as TreeListEditableItem;
				if (treeListEditableItem != null)
				{
					treeListEditableItem.Edit = true;
					ownerTreeList.Rebind();
				}
				return;
			}
			else if (string.Compare(commandName, "InitInsert", true, CultureInfo.InvariantCulture) == 0)
			{
				if (this.Canceled)
				{
					return;
				}
				TreeListDataItem treeListDataItem = this.Item as TreeListDataItem;
				if (treeListDataItem != null)
				{
					ownerTreeList.InsertChildItem(treeListDataItem);
					return;
				}
				TreeListHeaderItem treeListHeaderItem = this.Item as TreeListHeaderItem;
				if (treeListHeaderItem != null)
				{
					ownerTreeList.InsertItem();
				}
				return;
			}
			else if (string.Compare(commandName, "PerformInsert", true, CultureInfo.InvariantCulture) == 0)
			{
				ownerTreeList.CallOnInsertCommand(this);
				if (this.Canceled)
				{
					return;
				}
				ITreeListInsertItem treeListInsertItem = this.Item as ITreeListInsertItem;
				if (treeListInsertItem != null)
				{
					TreeListEditableItem treeListEditableItem2 = (TreeListEditableItem)treeListInsertItem;
					if (ownerTreeList.IsUsingModelBinding || ownerTreeList.IsBoundUsingDataSourceIDInternal)
					{
						ownerTreeList.PerformInsert(treeListEditableItem2);
						return;
					}
					treeListEditableItem2.Edit = false;
					ownerTreeList.Rebind();
				}
				return;
			}
			else if (string.Compare(commandName, "Update", true, CultureInfo.InvariantCulture) == 0)
			{
				ownerTreeList.CallOnUpdateCommand(this);
				if (this.Canceled)
				{
					return;
				}
				TreeListEditableItem treeListEditableItem3 = this.Item as TreeListEditableItem;
				if (ownerTreeList.IsUsingModelBinding || ownerTreeList.IsBoundUsingDataSourceIDInternal)
				{
					ownerTreeList.PerformUpdate(treeListEditableItem3, true);
				}
				else if (treeListEditableItem3 != null)
				{
					treeListEditableItem3.Edit = false;
				}
				if (ownerTreeList.IsUsingModelBinding)
				{
					if (!treeListEditableItem3.Edit)
					{
						ownerTreeList.Rebind();
						return;
					}
				}
				else
				{
					ownerTreeList.Rebind();
				}
				return;
			}
			else
			{
				if (string.Compare(commandName, "Delete", true, CultureInfo.InvariantCulture) != 0)
				{
					if (string.Compare(commandName, "Cancel", true, CultureInfo.InvariantCulture) == 0)
					{
						ownerTreeList.CallOnCancelCommand(this);
						if (this.Canceled)
						{
							return;
						}
						ITreeListInsertItem treeListInsertItem2 = this.Item as ITreeListInsertItem;
						TreeListEditableItem treeListEditableItem4 = this.Item as TreeListEditableItem;
						if (treeListInsertItem2 != null)
						{
							if (treeListInsertItem2.ParentItem != null)
							{
								treeListInsertItem2.ParentItem.IsChildInserted = false;
							}
							else
							{
								ownerTreeList.IsItemInserted = false;
							}
							ownerTreeList.Rebind();
							return;
						}
						if (treeListEditableItem4 != null)
						{
							treeListEditableItem4.Edit = false;
							ownerTreeList.Rebind();
						}
					}
					return;
				}
				ownerTreeList.CallOnDeleteCommand(this);
				if (this.Canceled)
				{
					return;
				}
				if (ownerTreeList.IsUsingModelBinding || ownerTreeList.IsBoundUsingDataSourceIDInternal)
				{
					ownerTreeList.PerformDelete((TreeListDataItem)this.Item, true);
				}
				ownerTreeList.Rebind();
				return;
			}
		}
	}
}
