using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000388 RID: 904
	public abstract class GridItem : GridTableRow, INamingContainer
	{
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00061DA4 File Offset: 0x0005FFA4
		public GridTableView OwnerTableView
		{
			get
			{
				return this._ownerTableView;
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00061DAC File Offset: 0x0005FFAC
		public GridItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex, GridItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.dataSetIndex = dataSetIndex;
			this._ownerTableView = ownerTableView;
			this.SetItemType(itemType);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00061E07 File Offset: 0x00060007
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00061E10 File Offset: 0x00060010
		public void FireCommandEvent(string commandName, object commandArgument)
		{
			CommandEventArgs args = new CommandEventArgs(commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00061E30 File Offset: 0x00060030
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is GridCommandEventArgs)
			{
				base.RaiseBubbleEvent(this, e);
				return true;
			}
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				GridCommandEventArgs args = GridCommandEventArgsFactory.CreateGridCommandEventArgs(this, source, commandEventArgs);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00061E6D File Offset: 0x0006006D
		internal virtual void SetItemDecorator(GridItemDecorator newDecorator)
		{
			this._decorator = newDecorator;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00061E76 File Offset: 0x00060076
		public void RestoreDecorator()
		{
			if (this.Selected)
			{
				this.SetItemType(GridItemType.SelectedItem);
				return;
			}
			if (this.IsInEditMode)
			{
				this.SetItemType(this.itemType);
				return;
			}
			if (!this.IsAlternatingItem())
			{
				this.SetItemType(GridItemType.Item);
				return;
			}
			this.SetItemType(GridItemType.AlternatingItem);
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00061EB5 File Offset: 0x000600B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string OwnerID
		{
			get
			{
				return this.OwnerTableView.ClientID;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00061EC2 File Offset: 0x000600C2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string OwnerGridID
		{
			get
			{
				return this.OwnerTableView.OwnerGrid.ClientID;
			}
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00061ED4 File Offset: 0x000600D4
		protected internal virtual void SetItemType(GridItemType itemType)
		{
			this.itemType = itemType;
			switch (itemType)
			{
			case GridItemType.AlternatingItem:
				this.SetItemDecorator(new GridAlternatingItemDecorator(this));
				return;
			case GridItemType.EditItem:
				this.SetItemDecorator(new GridEditItemDecorator(this));
				return;
			case GridItemType.Footer:
				this.SetItemDecorator(new GridFooterItemDecorator(this));
				return;
			case GridItemType.Header:
				this.SetItemDecorator(new GridHeaderItemDecorator(this));
				return;
			case GridItemType.Pager:
				this.SetItemDecorator(new GridPagerItemDecorator(this));
				return;
			case GridItemType.SelectedItem:
				this.SetItemDecorator(new GridSelectedItemDecorator(this));
				return;
			case GridItemType.NestedView:
				this.SetItemDecorator(new GridNestedViewItemDecorator(this));
				return;
			case GridItemType.GroupHeader:
				this.SetItemDecorator(new GridGroupHeaderDecorator(this));
				return;
			case GridItemType.NoRecordsItem:
				this.SetItemDecorator(new GridNoRecordsItemDecorator(this));
				return;
			case GridItemType.StatusBar:
				this.SetItemDecorator(new GridStatusBarItemDecorator(this));
				return;
			}
			this.SetItemDecorator(new GridItemDecorator(this));
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00061FD4 File Offset: 0x000601D4
		public virtual void Initialize(GridColumn[] columns)
		{
			TableCellCollection cells = this.Cells;
			for (int i = 0; i < columns.Length; i++)
			{
				TableCell cell = this.CreateCellObject(columns[i]);
				cells.Add(cell);
				columns[i].InitializeCell(cell, i, this);
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001F00 RID: 7936 RVA: 0x00062014 File Offset: 0x00060214
		// (remove) Token: 0x06001F01 RID: 7937 RVA: 0x0006204C File Offset: 0x0006024C
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public event GridCellDataBoundEvent CellDataBound;

		// Token: 0x06001F02 RID: 7938 RVA: 0x00062084 File Offset: 0x00060284
		protected virtual void OnCellDataBound(GridColumn column, TableCell cell)
		{
			if (this.CellDataBound != null)
			{
				GridCellDataBoundEventArgs args = new GridCellDataBoundEventArgs(column, cell);
				this.CellDataBound(this, args);
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000620B0 File Offset: 0x000602B0
		protected virtual void CellsDataBound(GridColumn[] columns)
		{
			TableCellCollection cells = this.Cells;
			for (int i = 0; i < columns.Length; i++)
			{
				if (cells.Count <= i)
				{
					return;
				}
				this.OnCellDataBound(columns[i], cells[i]);
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000620EC File Offset: 0x000602EC
		protected virtual TableCell CreateCellObject()
		{
			return new GridTableCell(true);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000620F4 File Offset: 0x000602F4
		protected virtual TableCell CreateCellObject(GridColumn col)
		{
			return new GridTableCell(true)
			{
				Column = col,
				Item = this,
				WrapElements = (this.OwnerTableView.EditMode == GridEditMode.Batch && GridBatchEditingHelper.IsColumnEditable(col))
			};
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00062134 File Offset: 0x00060334
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			GridDetailTemplateItem gridDetailTemplateItem = null;
			this.DataItem = dataItem;
			rows.Add(this);
			if (this is GridDataItem)
			{
				if (this.OwnerTableView.itemTemplate == null)
				{
					this.Initialize(columns);
					if (this.OwnerTableView.DetailItemTemplate != null)
					{
						gridDetailTemplateItem = new GridDetailTemplateItem(this.OwnerTableView);
						gridDetailTemplateItem.Initialize();
						(this as GridDataItem).DetailTemplateItemDataCell = gridDetailTemplateItem.DataCell;
						rows.Add(gridDetailTemplateItem);
					}
				}
				else
				{
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					foreach (GridColumn gridColumn in columns)
					{
						if (gridColumn is GridEditableColumn)
						{
							arrayList.Add(gridColumn);
						}
						else
						{
							arrayList2.Add(gridColumn);
						}
					}
					GridColumn[] array = new GridColumn[arrayList2.Count];
					arrayList2.CopyTo(array, 0);
					this.Initialize(array);
					GridColumn[] array2 = new GridColumn[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					TableCell tableCell = this.CreateCellObject();
					tableCell.Text = "";
					this.Cells.Add(tableCell);
					tableCell.ColumnSpan = this.CalcColSpan(array2, 0, -1);
					if (this.IsInEditMode)
					{
						ITemplate editItemTemplate = this.OwnerTableView.editItemTemplate;
						if (editItemTemplate == null)
						{
							if (!this.OwnerTableView.IsClone)
							{
								throw new GridException("Please define EditItemTemplate for MasterTableView.");
							}
							throw new GridException(string.Format("Please define EditItemTemplate for GridTableView with ID = \"{0}\"", this.OwnerTableView.ID));
						}
						else
						{
							editItemTemplate.InstantiateIn(tableCell);
						}
					}
					else
					{
						this.OwnerTableView.itemTemplate.InstantiateIn(tableCell);
					}
				}
			}
			else
			{
				this.Initialize(columns);
			}
			this.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
			if (!dataBind)
			{
				return;
			}
			this.DataBind();
			if (this is GridDataItem && this.OwnerTableView.DetailItemTemplate != null && gridDetailTemplateItem != null)
			{
				gridDetailTemplateItem.DataItem = this.DataItem;
				gridDetailTemplateItem.DataBind();
			}
			if (this is GridDataItem)
			{
				if (this.OwnerTableView.itemTemplate == null)
				{
					this.CellsDataBound(columns);
				}
			}
			else
			{
				this.CellsDataBound(columns);
			}
			e = new GridItemEventArgs(this, new GridItemDataBound());
			this.OwnerTableView.OwnerGrid.CallOnItemDataBound(e);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00062364 File Offset: 0x00060564
		public virtual void PrepareItemStyle()
		{
			if (!(this is GridDataItem))
			{
				this._decorator.DecorateItem(this.OwnerTableView, this.OwnerTableView.RenderColumns);
				return;
			}
			if (this.OwnerTableView.itemTemplate == null)
			{
				this._decorator.DecorateItem(this.OwnerTableView, this.OwnerTableView.RenderColumns);
				return;
			}
			ArrayList arrayList = new ArrayList();
			foreach (GridColumn gridColumn in this.OwnerTableView.RenderColumns)
			{
				if (!(gridColumn is GridEditableColumn))
				{
					arrayList.Add(gridColumn);
				}
			}
			GridColumn[] array = new GridColumn[arrayList.Count];
			arrayList.CopyTo(array, 0);
			this._decorator.DecorateItem(this.OwnerTableView, array);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00062424 File Offset: 0x00060624
		public virtual void PrepareItemVisibility()
		{
			if (!(this is GridDataItem))
			{
				this._decorator.SetItemVisibility(this.OwnerTableView, this.OwnerTableView.RenderColumns);
				return;
			}
			if (this.OwnerTableView.itemTemplate == null)
			{
				this._decorator.SetItemVisibility(this.OwnerTableView, this.OwnerTableView.RenderColumns);
				return;
			}
			ArrayList arrayList = new ArrayList();
			foreach (GridColumn gridColumn in this.OwnerTableView.RenderColumns)
			{
				if (!(gridColumn is GridEditableColumn))
				{
					arrayList.Add(gridColumn);
				}
			}
			GridColumn[] array = new GridColumn[arrayList.Count];
			arrayList.CopyTo(array, 0);
			this._decorator.SetItemVisibility(this.OwnerTableView, array);
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000624E2 File Offset: 0x000606E2
		public virtual bool HasChildItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000624E5 File Offset: 0x000606E5
		public virtual bool CanExpand
		{
			get
			{
				return this.HasChildItems;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000624ED File Offset: 0x000606ED
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x000624F5 File Offset: 0x000606F5
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000624FE File Offset: 0x000606FE
		public virtual int DataSetIndex
		{
			get
			{
				return this.dataSetIndex;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00062506 File Offset: 0x00060706
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0006250E File Offset: 0x0006070E
		internal void SetClientRowIndex(int index)
		{
			this._clientRowIndex = index;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00062517 File Offset: 0x00060717
		public virtual int ClientRowIndex
		{
			get
			{
				return this._clientRowIndex;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0006251F File Offset: 0x0006071F
		public virtual int RowIndex
		{
			get
			{
				return this.OwnerTableView.GetGridTable().Rows.GetRowIndex(this);
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00062537 File Offset: 0x00060737
		internal void SetTempIndexHierarchical(string value)
		{
			this._tempIndex = value;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x00062540 File Offset: 0x00060740
		public string ItemIndexHierarchical
		{
			get
			{
				object obj = this.ViewState["_iih"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this._tempIndex;
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0006256E File Offset: 0x0006076E
		internal void SetItemIndexHierarchical(string hierarchicalIndex)
		{
			this.ViewState["_iih"] = hierarchicalIndex;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00062581 File Offset: 0x00060781
		public virtual GridItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0006258C File Offset: 0x0006078C
		internal bool IsAlternatingItem()
		{
			return this.ItemIndex % 2 != 0;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000625A9 File Offset: 0x000607A9
		protected virtual bool RemoveSelectedChildren()
		{
			return false;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000625AC File Offset: 0x000607AC
		public void RemoveChildSelectedItems()
		{
			if (!this.HasChildItems)
			{
				return;
			}
			this.RemoveSelectedChildren();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000625BE File Offset: 0x000607BE
		protected virtual void RemoveEditedChildren()
		{
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000625C0 File Offset: 0x000607C0
		public void RemoveChildEditItems()
		{
			if (!this.HasChildItems)
			{
				return;
			}
			this.RemoveEditedChildren();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000625D1 File Offset: 0x000607D1
		public void SetChildrenVisible(bool value)
		{
			if (!this.HasChildItems)
			{
				return;
			}
			this.SetVisibleChildren(value);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000625E3 File Offset: 0x000607E3
		public virtual void SetVisibleChildren(bool value)
		{
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000625E5 File Offset: 0x000607E5
		internal void SetVisibility(bool visible)
		{
			if (visible)
			{
				if (this.Context != null)
				{
					base.Style["display"] = "table-row";
					return;
				}
			}
			else
			{
				base.Style["display"] = "none";
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0006261D File Offset: 0x0006081D
		protected virtual bool GetExpandedDefaultValue()
		{
			return this.OwnerTableView.HierarchyDefaultExpanded;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0006262A File Offset: 0x0006082A
		private void SaveExpandedState(bool value)
		{
			if (value == this.GetExpandedDefaultValue())
			{
				this.ViewState["Expanded"] = null;
				return;
			}
			this.ViewState["Expanded"] = value;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0006265D File Offset: 0x0006085D
		protected virtual void OnExpand()
		{
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0006265F File Offset: 0x0006085F
		protected virtual void OnCollapse()
		{
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00062664 File Offset: 0x00060864
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x00062694 File Offset: 0x00060894
		public virtual bool Expanded
		{
			get
			{
				object obj = this.ViewState["Expanded"];
				if (obj != null)
				{
					return (bool)obj;
				}
				return this.GetExpandedDefaultValue();
			}
			set
			{
				if (!this.CanExpand)
				{
					this.SaveExpandedState(false);
					return;
				}
				if (value == this.Expanded)
				{
					return;
				}
				this.expandedInternal = value;
				this.OwnerTableView.expandedGroupInternal = value;
				this.OwnerTableView.expandedGroupIndexInternal = this.GroupIndexInternal;
				this.SetChildrenVisible(value);
				if (value)
				{
					this.SaveExpandedState(true);
					this.OnExpand();
					if (this.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Conditional)
					{
						this.ConditionalExpanded = true;
						return;
					}
				}
				else
				{
					this.RemoveChildSelectedItems();
					this.RemoveChildEditItems();
					this.SaveExpandedState(false);
					this.OnCollapse();
				}
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00062728 File Offset: 0x00060928
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x00062751 File Offset: 0x00060951
		internal bool ConditionalExpanded
		{
			get
			{
				object obj = this.ViewState["ConditionalExpanded"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ConditionalExpanded"] = value;
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006276C File Offset: 0x0006096C
		private void ExpandHierarchyToTop(GridItem item)
		{
			GridItem parentItem = item.OwnerTableView.ParentItem;
			if (parentItem != null)
			{
				parentItem.Expanded = true;
				this.ExpandHierarchyToTop(parentItem);
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00062796 File Offset: 0x00060996
		public void ExpandHierarchyToTop()
		{
			this.ExpandHierarchyToTop(this);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0006279F File Offset: 0x0006099F
		internal void SetSelected(bool value)
		{
			if (value)
			{
				this.SetItemDecorator(new GridSelectedItemDecorator(this));
				this.ExpandHierarchyToTop();
				return;
			}
			this.RestoreDecorator();
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000627BD File Offset: 0x000609BD
		protected virtual void SetEdited(bool value)
		{
			if (value)
			{
				this.SetItemDecorator(new GridEditItemDecorator(this));
				this.ExpandHierarchyToTop();
				return;
			}
			this.RestoreDecorator();
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x000627DC File Offset: 0x000609DC
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x00062805 File Offset: 0x00060A05
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("RadGridItem_Display")]
		[DefaultValue(true)]
		public virtual bool Display
		{
			get
			{
				object obj = this.ViewState["Display"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Display"] = value;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0006281D File Offset: 0x00060A1D
		// (set) Token: 0x06001F2D RID: 7981 RVA: 0x0006283C File Offset: 0x00060A3C
		public bool Selected
		{
			get
			{
				return this.OwnerTableView.OwnerGrid.SelectedIndexes.Contains(this.ItemIndexHierarchical);
			}
			set
			{
				if (value && this.SelectableMode != GridItemSelectableMode.None)
				{
					this.OwnerTableView.OwnerGrid.SaveSelectedIndexState(this.ItemIndexHierarchical);
				}
				else if (!value)
				{
					this.OwnerTableView.OwnerGrid.RemoveSelectedIndexState(this.ItemIndexHierarchical);
				}
				if (this.SelectableMode != GridItemSelectableMode.None)
				{
					this.SetSelected(value);
				}
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00062898 File Offset: 0x00060A98
		// (set) Token: 0x06001F2F RID: 7983 RVA: 0x000628C4 File Offset: 0x00060AC4
		public GridItemSelectableMode SelectableMode
		{
			get
			{
				object obj = this.ViewState["SelectableMode"];
				if (obj == null)
				{
					return GridItemSelectableMode.ServerAndClientSide;
				}
				return (GridItemSelectableMode)obj;
			}
			set
			{
				if (this.Selected && value == GridItemSelectableMode.None)
				{
					this.Selected = false;
				}
				if (value != GridItemSelectableMode.ServerAndClientSide)
				{
					this.OwnerTableView.OwnerGrid.ClientUnselectableIndexes.Add(this.ItemIndexHierarchical);
				}
				else
				{
					this.OwnerTableView.OwnerGrid.ClientUnselectableIndexes.Remove(this.ItemIndexHierarchical);
				}
				this.ViewState["SelectableMode"] = value;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00062935 File Offset: 0x00060B35
		// (set) Token: 0x06001F31 RID: 7985 RVA: 0x00062954 File Offset: 0x00060B54
		public virtual bool Edit
		{
			get
			{
				return this.OwnerTableView.OwnerGrid.EditIndexes.Contains(this.ItemIndexHierarchical);
			}
			set
			{
				if (!string.IsNullOrEmpty(this.ItemIndexHierarchical) || !value)
				{
					if (value)
					{
						this.OwnerTableView.OwnerGrid.SaveEditIndexState(this.ItemIndexHierarchical);
					}
					else
					{
						this.OwnerTableView.OwnerGrid.RemoveEditIndexState(this.ItemIndexHierarchical);
					}
					this.SetEdited(value);
				}
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000629A9 File Offset: 0x00060BA9
		internal void RestoreChildrenVisible()
		{
			if (this.Expanded && this.Visible)
			{
				this.SetChildrenVisible(true);
				return;
			}
			this.SetChildrenVisible(false);
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000629CA File Offset: 0x00060BCA
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x000629D2 File Offset: 0x00060BD2
		internal int GroupLevel
		{
			get
			{
				return this._groupLevel;
			}
			set
			{
				this._groupLevel = value;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000629DB File Offset: 0x00060BDB
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x000629E3 File Offset: 0x00060BE3
		internal string GroupIndexInternal
		{
			get
			{
				return this._groupIndex;
			}
			set
			{
				this._groupIndex = value;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000629EC File Offset: 0x00060BEC
		public string GroupIndex
		{
			get
			{
				return this._groupIndex;
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000629F4 File Offset: 0x00060BF4
		protected int CalcColSpan(GridColumn[] columns, int FromCellIndex, int ToCellIndex)
		{
			int num = 0;
			int num2 = ToCellIndex;
			if (num2 <= 0 || num2 >= columns.Length)
			{
				num2 = columns.Length - 1;
			}
			for (int i = FromCellIndex; i <= num2; i++)
			{
				if (columns[i].Visible && columns[i].Display)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00062A3A File Offset: 0x00060C3A
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x00062A43 File Offset: 0x00060C43
		public virtual bool IsDataBound
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00062A46 File Offset: 0x00060C46
		public virtual bool IsInEditMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00062A49 File Offset: 0x00060C49
		protected HorizontalAlign LeftAlign()
		{
			if (this.OwnerTableView.Dir == GridTableTextDirection.LTR)
			{
				return HorizontalAlign.Left;
			}
			return HorizontalAlign.Right;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00062A5B File Offset: 0x00060C5B
		protected HorizontalAlign RightAlign()
		{
			if (this.OwnerTableView.Dir == GridTableTextDirection.LTR)
			{
				return HorizontalAlign.Right;
			}
			return HorizontalAlign.Left;
		}

		// Token: 0x040007F1 RID: 2033
		internal bool expandedInternal = true;

		// Token: 0x040007F2 RID: 2034
		private GridItemDecorator _decorator;

		// Token: 0x040007F3 RID: 2035
		private GridTableView _ownerTableView;

		// Token: 0x040007F4 RID: 2036
		private object dataItem;

		// Token: 0x040007F5 RID: 2037
		private int dataSetIndex;

		// Token: 0x040007F6 RID: 2038
		private int itemIndex;

		// Token: 0x040007F7 RID: 2039
		private GridItemType itemType;

		// Token: 0x040007F8 RID: 2040
		private string _groupIndex = "";

		// Token: 0x040007F9 RID: 2041
		private int _groupLevel = -1;

		// Token: 0x040007FB RID: 2043
		private int _clientRowIndex = -1;

		// Token: 0x040007FC RID: 2044
		internal string _tempIndex = string.Empty;
	}
}
