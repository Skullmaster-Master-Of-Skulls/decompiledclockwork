using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010D8 RID: 4312
	public class GridCommandEventArgs : CommandEventArgs, IGridCommandEvent
	{
		// Token: 0x0600B0BD RID: 45245 RVA: 0x00263804 File Offset: 0x00261A04
		public GridCommandEventArgs(GridItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		// Token: 0x0600B0BE RID: 45246 RVA: 0x0026381B File Offset: 0x00261A1B
		internal GridCommandEventArgs(GridItem item, object commandSource, string name, object argument) : base(name, argument)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		// Token: 0x17003943 RID: 14659
		// (get) Token: 0x0600B0BF RID: 45247 RVA: 0x00263834 File Offset: 0x00261A34
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x17003944 RID: 14660
		// (get) Token: 0x0600B0C0 RID: 45248 RVA: 0x0026383C File Offset: 0x00261A3C
		public GridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17003945 RID: 14661
		// (get) Token: 0x0600B0C1 RID: 45249 RVA: 0x00263844 File Offset: 0x00261A44
		// (set) Token: 0x0600B0C2 RID: 45250 RVA: 0x0026384C File Offset: 0x00261A4C
		public bool Canceled
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x0600B0C3 RID: 45251 RVA: 0x00263858 File Offset: 0x00261A58
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual void ExecuteCommand(object source)
		{
			string commandName = base.CommandName;
			if (string.Compare(commandName, "Edit", true) == 0)
			{
				this.Item.Edit = true;
				this.Item.OwnerTableView.OwnerGrid.CallOnEditCommand(this);
				if (this.Canceled)
				{
					return;
				}
				this.Item.OwnerTableView.Rebind();
				return;
			}
			else if (string.Compare(commandName, "EditSelected", true) == 0)
			{
				if (this.Item.OwnerTableView.OwnerGrid.SelectedIndexes.Count == 0)
				{
					return;
				}
				foreach (object obj in this.Item.OwnerTableView.OwnerGrid.SelectedItems)
				{
					GridDataItem gridDataItem = (GridDataItem)obj;
					this.item = gridDataItem;
					gridDataItem.Edit = true;
					gridDataItem.OwnerTableView.OwnerGrid.CallOnEditCommand(this);
					bool canceled = this.Canceled;
				}
				this.Item.OwnerTableView.OwnerGrid.Rebind();
				return;
			}
			else if (string.Compare(commandName, "EditAll", true) == 0)
			{
				if (this.Item.OwnerTableView.OwnerGrid.Items.Count == 0)
				{
					return;
				}
				foreach (object obj2 in this.Item.OwnerTableView.OwnerGrid.Items)
				{
					GridDataItem gridDataItem2 = (GridDataItem)obj2;
					this.item = gridDataItem2;
					gridDataItem2.Edit = true;
					gridDataItem2.OwnerTableView.OwnerGrid.CallOnEditCommand(this);
					bool canceled2 = this.Canceled;
				}
				this.Item.OwnerTableView.OwnerGrid.Rebind();
				return;
			}
			else if (string.Compare(commandName, "Update", true) == 0)
			{
				this.Item.OwnerTableView.OwnerGrid.CallOnUpdateCommand(this);
				if (this.Canceled)
				{
					return;
				}
				if (this.Item.OwnerTableView.IsUsingModelBinding || this.Item.OwnerTableView.AllowAutomaticUpdates)
				{
					this.Item.OwnerTableView.PerformUpdate((GridEditableItem)this.Item, true);
					if (!this.Item.OwnerTableView.IsModelValid)
					{
						this.item.OwnerTableView.IsModelValid = true;
						return;
					}
				}
				else
				{
					this.Item.Edit = false;
				}
				if (!this.Item.OwnerTableView.SuppressRebindOnUpdate)
				{
					this.Item.OwnerTableView.Rebind();
					return;
				}
				this.Item.OwnerTableView.SuppressRebindOnUpdate = false;
				return;
			}
			else if (string.Compare(commandName, "UpdateEdited", true) == 0)
			{
				if (this.Item.OwnerTableView.OwnerGrid.EditIndexes.Count == 0)
				{
					return;
				}
				int num = 0;
				int num2 = 0;
				bool flag = true;
				foreach (object obj3 in this.Item.OwnerTableView.OwnerGrid.EditItems)
				{
					GridEditableItem gridEditableItem = (GridEditableItem)obj3;
					if (gridEditableItem.OwnerTableView.EditMode != GridEditMode.InPlace)
					{
						this.item = ((GridDataItem)gridEditableItem).EditFormItem;
					}
					else
					{
						this.item = gridEditableItem;
					}
					gridEditableItem.OwnerTableView.OwnerGrid.CallOnUpdateCommand(this);
					if (this.Canceled)
					{
						num++;
					}
					else
					{
						num2++;
						if (gridEditableItem.OwnerTableView.EditMode == GridEditMode.InPlace)
						{
							if (gridEditableItem.OwnerTableView.AllowAutomaticUpdates)
							{
								gridEditableItem.OwnerTableView.PerformUpdate(gridEditableItem, true);
							}
							else
							{
								gridEditableItem.Edit = false;
							}
						}
						else if (this.Item.OwnerTableView.AllowAutomaticUpdates)
						{
							gridEditableItem.OwnerTableView.PerformUpdate(((GridDataItem)gridEditableItem).EditFormItem, true);
							if (!gridEditableItem.OwnerTableView.IsModelValid)
							{
								flag = false;
								gridEditableItem.OwnerTableView.IsModelValid = true;
							}
						}
						else
						{
							gridEditableItem.Edit = false;
						}
					}
				}
				if (num2 > 0 && !this.Item.OwnerTableView.SuppressRebindOnUpdate && flag)
				{
					this.Item.OwnerTableView.OwnerGrid.Rebind();
					return;
				}
				if (this.Item.OwnerTableView.SuppressRebindOnUpdate)
				{
					this.Item.OwnerTableView.SuppressRebindOnUpdate = false;
				}
				return;
			}
			else
			{
				if (string.Compare(commandName, "CancelAll", true) == 0)
				{
					foreach (object obj4 in this.Item.OwnerTableView.OwnerGrid.EditItems)
					{
						GridDataItem gridDataItem3 = (GridDataItem)obj4;
						gridDataItem3.Edit = false;
					}
					this.Item.OwnerTableView.IsItemInserted = false;
					this.Item.OwnerTableView.OwnerGrid.Rebind();
					return;
				}
				if (string.Compare(commandName, "Cancel", true) == 0)
				{
					this.Item.OwnerTableView.OwnerGrid.CallOnCancelCommand(this);
					if (this.Canceled)
					{
						return;
					}
					this.Item.Edit = false;
					this.Item.OwnerTableView.Rebind();
					return;
				}
				else if (string.Compare(commandName, "Delete", true) == 0)
				{
					this.item.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToLast;
					this.Item.OwnerTableView.OwnerGrid.CallOnDeleteCommand(this);
					if (this.Canceled)
					{
						return;
					}
					if (this.Item.OwnerTableView.IsUsingModelBinding || this.Item.OwnerTableView.AllowAutomaticDeletes)
					{
						this.Item.OwnerTableView.PerformDelete((GridEditableItem)this.Item, true);
					}
					this.Item.OwnerTableView.Rebind();
					return;
				}
				else if (string.Compare(commandName, "DeleteSelected", true) == 0)
				{
					if (this.Item.OwnerTableView.OwnerGrid.SelectedIndexes.Count == 0)
					{
						return;
					}
					foreach (object obj5 in this.Item.OwnerTableView.OwnerGrid.SelectedItems)
					{
						GridDataItem gridDataItem4 = (GridDataItem)obj5;
						gridDataItem4.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToLast;
						this.item = gridDataItem4;
						gridDataItem4.OwnerTableView.OwnerGrid.CallOnDeleteCommand(this);
						if (!this.Canceled && gridDataItem4.OwnerTableView.AllowAutomaticDeletes)
						{
							gridDataItem4.OwnerTableView.PerformDelete(gridDataItem4, true);
						}
					}
					this.Item.OwnerTableView.OwnerGrid.Rebind();
					return;
				}
				else if (string.Compare(commandName, "InitInsert", true) == 0)
				{
					if (this.Canceled)
					{
						return;
					}
					this.Item.OwnerTableView.InsertItem();
					return;
				}
				else
				{
					if (string.Compare(commandName, "PerformInsert", true) != 0)
					{
						if (string.Compare(commandName, "RebindGrid", true) == 0)
						{
							this.Item.OwnerTableView.Rebind();
						}
						if (string.Compare(commandName, "ExportToPdf", true) == 0)
						{
							this.Item.OwnerTableView.ExportToPdf();
						}
						if (string.Compare(commandName, "ExportToCsv", true) == 0)
						{
							this.Item.OwnerTableView.ExportToCSV();
						}
						if (string.Compare(commandName, "ExportToExcel", true) == 0)
						{
							this.Item.OwnerTableView.ExportToExcel();
						}
						if (string.Compare(commandName, "ExportToWord", true) == 0)
						{
							this.Item.OwnerTableView.ExportToWord();
						}
						return;
					}
					this.Item.OwnerTableView.OwnerGrid.CallOnInsertCommand(this);
					if (this.Canceled)
					{
						return;
					}
					if (this.item.OwnerTableView.IsUsingModelBinding || this.Item.OwnerTableView.AllowAutomaticInserts)
					{
						this.Item.OwnerTableView.PerformInsert();
						if (!this.Item.OwnerTableView.IsModelValid)
						{
							this.item.OwnerTableView.IsModelValid = true;
							return;
						}
					}
					else
					{
						if (!(this.Item is IGridInsertItem))
						{
							this.Item.OwnerTableView.GetInsertItem().Edit = false;
						}
						else
						{
							this.Item.Edit = false;
						}
						this.Item.OwnerTableView.Rebind();
					}
					return;
				}
			}
		}

		// Token: 0x0600B0C4 RID: 45252 RVA: 0x002640B8 File Offset: 0x002622B8
		protected void SetCommandSource(object commandSource)
		{
			this.commandSource = commandSource;
		}

		// Token: 0x04002E6A RID: 11882
		private object commandSource;

		// Token: 0x04002E6B RID: 11883
		private GridItem item;

		// Token: 0x04002E6C RID: 11884
		private bool _cancel;
	}
}
