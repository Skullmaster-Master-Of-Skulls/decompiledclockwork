using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004B3 RID: 1203
	public class GridBatchEditingEventArgs : EventArgs, IGridCommandEvent
	{
		// Token: 0x06002AD1 RID: 10961 RVA: 0x0008AB83 File Offset: 0x00088D83
		public GridBatchEditingEventArgs(GridItem item, string commandsAsString)
		{
			this.canceled = false;
			this.item = item;
			this.commandsAsString = commandsAsString;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0008ABA0 File Offset: 0x00088DA0
		public GridBatchEditingEventArgs(GridItem item, string commandsAsString, bool isGlobalBatchEdit) : this(item, commandsAsString)
		{
			this.isGlobalBatchEdit = isGlobalBatchEdit;
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x0008ABB1 File Offset: 0x00088DB1
		protected virtual RadGrid OwnerGrid
		{
			get
			{
				return this.Item.OwnerTableView.OwnerGrid;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0008ABC3 File Offset: 0x00088DC3
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x0008ABCB File Offset: 0x00088DCB
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
			set
			{
				this.canceled = value;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x0008ABD4 File Offset: 0x00088DD4
		public GridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x0008ABDC File Offset: 0x00088DDC
		public List<GridBatchEditingCommand> Commands
		{
			get
			{
				if (this.commands == null)
				{
					this.CreateCommands();
				}
				return this.commands;
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		public void ExecuteCommand(object source)
		{
			this.OwnerGrid.CallOnBatchEditCommand(this);
			if (this.Canceled)
			{
				return;
			}
			HashSet<GridTableView> hashSet = new HashSet<GridTableView>();
			foreach (GridBatchEditingCommand gridBatchEditingCommand in this.Commands)
			{
				gridBatchEditingCommand.Execute();
				if (!gridBatchEditingCommand.OwnerTableView.SuppressRebindOnUpdate && !this.isGlobalBatchEdit)
				{
					hashSet.Add(gridBatchEditingCommand.OwnerTableView);
				}
			}
			foreach (GridTableView gridTableView in hashSet)
			{
				gridTableView.Rebind();
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0008ACC0 File Offset: 0x00088EC0
		private void CreateCommands()
		{
			this.commands = new List<GridBatchEditingCommand>();
			string[] array = this.commandsAsString.ToString().Split(new string[]
			{
				";.;"
			}, StringSplitOptions.RemoveEmptyEntries);
			DataSourceView dataSourceView = this.Item.OwnerTableView.GetDataSourceView();
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					this.commands.Add(new GridBatchEditingCommand(this.Item.OwnerTableView, dataSourceView, text));
				}
			}
		}

		// Token: 0x04000B2C RID: 2860
		private GridItem item;

		// Token: 0x04000B2D RID: 2861
		private bool canceled;

		// Token: 0x04000B2E RID: 2862
		private string commandsAsString;

		// Token: 0x04000B2F RID: 2863
		private List<GridBatchEditingCommand> commands;

		// Token: 0x04000B30 RID: 2864
		private bool isGlobalBatchEdit;
	}
}
