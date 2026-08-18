using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000964 RID: 2404
	public class TreeListItem : TreeListTableRow, INamingContainer
	{
		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06005B7B RID: 23419 RVA: 0x00116DEC File Offset: 0x00114FEC
		// (set) Token: 0x06005B7C RID: 23420 RVA: 0x00116DF4 File Offset: 0x00114FF4
		public TreeListItemType ItemType { get; protected set; }

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x06005B7D RID: 23421 RVA: 0x00116DFD File Offset: 0x00114FFD
		// (set) Token: 0x06005B7E RID: 23422 RVA: 0x00116E05 File Offset: 0x00115005
		public RadTreeList OwnerTreeList { get; internal set; }

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06005B7F RID: 23423 RVA: 0x00116E0E File Offset: 0x0011500E
		// (set) Token: 0x06005B80 RID: 23424 RVA: 0x00116E16 File Offset: 0x00115016
		public virtual bool IsDataBinding { get; set; }

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06005B81 RID: 23425 RVA: 0x00116E1F File Offset: 0x0011501F
		// (set) Token: 0x06005B82 RID: 23426 RVA: 0x00116E27 File Offset: 0x00115027
		internal TreeListItemDecorator Decorator { get; private set; }

		// Token: 0x06005B83 RID: 23427 RVA: 0x00116E30 File Offset: 0x00115030
		public TreeListItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding)
		{
			this.OwnerTreeList = ownerTreeList;
			this.ItemType = itemType;
			this.IsDataBinding = isDataBinding;
			this.SetupDecorator();
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x00116E54 File Offset: 0x00115054
		protected virtual void SetupDecorator()
		{
			TreeListItemType itemType = this.ItemType;
			if (itemType <= TreeListItemType.DetailTemplateItem)
			{
				if (itemType <= TreeListItemType.HeaderItem)
				{
					switch (itemType)
					{
					case TreeListItemType.Item:
						this.Decorator = new TreeListItemDecorator(this);
						return;
					case TreeListItemType.AlternatingItem:
						this.Decorator = new TreeListAlternatingItemDecorator(this);
						return;
					case TreeListItemType.Item | TreeListItemType.AlternatingItem:
						break;
					case TreeListItemType.SelectedItem:
						this.Decorator = new TreeListSelectedItemDecorator(this);
						return;
					default:
						if (itemType != TreeListItemType.HeaderItem)
						{
							return;
						}
						this.Decorator = new TreeListHeaderItemDecorator(this);
						return;
					}
				}
				else
				{
					if (itemType == TreeListItemType.PagerItem)
					{
						this.Decorator = new TreeListPagerItemDecorator(this);
						return;
					}
					if (itemType != TreeListItemType.DetailTemplateItem)
					{
						return;
					}
					this.Decorator = new TreeListDetailTemplateItemDecorator(this);
					return;
				}
			}
			else
			{
				if (itemType <= TreeListItemType.EditItem)
				{
					if (itemType == TreeListItemType.NoRecordsTemplateItem)
					{
						this.Decorator = new TreeListNoRecordsItemDecorator(this);
						return;
					}
					if (itemType != TreeListItemType.EditItem)
					{
						return;
					}
				}
				else if (itemType != TreeListItemType.EditFormItem)
				{
					if (itemType == TreeListItemType.FooterItem)
					{
						this.Decorator = new TreeListFooterItemDecorator(this);
						return;
					}
					if (itemType != TreeListItemType.CommandItem)
					{
						return;
					}
					this.Decorator = new TreeListCommandItemDecorator(this);
					return;
				}
				this.Decorator = new TreeListEditItemDecorator(this);
			}
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x00116F54 File Offset: 0x00115154
		public virtual void Initialize(IList<TreeListColumn> columns)
		{
			TableCellCollection cells = this.Cells;
			for (int i = 0; i < columns.Count; i++)
			{
				TableCell cell = this.CreateCellObject();
				cells.Add(cell);
				columns[i].InitializeCell(cell, i, this);
			}
			this.CallOnItemCreated();
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CellsDataBound(columns);
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x00116FB8 File Offset: 0x001151B8
		internal virtual int CalculateCellSpan(IList<TreeListColumn> columns)
		{
			int num = 0;
			foreach (TreeListColumn treeListColumn in columns)
			{
				if (treeListColumn.Visible && treeListColumn.Display)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06005B87 RID: 23431 RVA: 0x00117010 File Offset: 0x00115210
		internal virtual bool IsExportable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x00117013 File Offset: 0x00115213
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.OwnerTreeList.IsExporting || this.IsExportable)
			{
				base.Render(writer);
			}
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x00117031 File Offset: 0x00115231
		public virtual void PrepareItemStyle()
		{
			this.Decorator.DecorateItem(this.OwnerTreeList, this.OwnerTreeList.RenderColumns);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0011704F File Offset: 0x0011524F
		protected virtual TableCell CreateCellObject()
		{
			return new TreeListTableCell(true);
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00117058 File Offset: 0x00115258
		protected virtual void CallOnItemCreated()
		{
			TreeListItemCreatedEventArgs e = new TreeListItemCreatedEventArgs(this);
			this.OwnerTreeList.FireItemCreated(e);
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x00117078 File Offset: 0x00115278
		protected virtual void CallOnItemDataBound()
		{
			TreeListItemDataBoundEventArgs e = new TreeListItemDataBoundEventArgs(this);
			this.OwnerTreeList.FireItemDataBound(e);
		}

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06005B8D RID: 23437 RVA: 0x00117098 File Offset: 0x00115298
		// (remove) Token: 0x06005B8E RID: 23438 RVA: 0x001170D0 File Offset: 0x001152D0
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public event TreeListCellDataBoundEvent CellDataBound;

		// Token: 0x06005B8F RID: 23439 RVA: 0x00117108 File Offset: 0x00115308
		protected virtual void CellsDataBound(IList<TreeListColumn> columns)
		{
			TableCellCollection cells = this.Cells;
			for (int i = 0; i < columns.Count; i++)
			{
				if (cells.Count <= i)
				{
					return;
				}
				this.OnCellDataBound(columns[i], cells[i]);
			}
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0011714C File Offset: 0x0011534C
		protected virtual void OnCellDataBound(TreeListColumn column, TableCell cell)
		{
			if (this.CellDataBound != null)
			{
				TreeListCellDataBoundEventArgs args = new TreeListCellDataBoundEventArgs(column, cell);
				this.CellDataBound(this, args);
			}
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x00117178 File Offset: 0x00115378
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			if (args is CommandEventArgs && !(args is TreeListCommandEventArgs))
			{
				TreeListCommandEventArgs args2 = TreeListCommandEventArgsFactory.CreateCommandEventArgs(this, source, args as CommandEventArgs);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x001171B8 File Offset: 0x001153B8
		[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void FireCommandEvent(string commandName, object commandArgument)
		{
			CommandEventArgs args = new CommandEventArgs(commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}
	}
}
