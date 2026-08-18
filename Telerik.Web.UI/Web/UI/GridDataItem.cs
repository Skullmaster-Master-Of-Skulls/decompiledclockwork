using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200113C RID: 4412
	public class GridDataItem : GridEditableItem
	{
		// Token: 0x0600B3B6 RID: 46006 RVA: 0x00273739 File Offset: 0x00271939
		public GridDataItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.Item)
		{
		}

		// Token: 0x0600B3B7 RID: 46007 RVA: 0x00273745 File Offset: 0x00271945
		public GridDataItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex, GridItemType itemType) : base(ownerTableView, itemIndex, dataSetIndex, itemType)
		{
		}

		// Token: 0x0600B3B8 RID: 46008 RVA: 0x00273752 File Offset: 0x00271952
		public string ClientFireCommandFunction(string commandName, string commandArgument)
		{
			return string.Format("if(!$find('{0}').fireCommand('{1}','{2}')) return false;", base.OwnerTableView.ClientID, commandName, commandArgument);
		}

		// Token: 0x0600B3B9 RID: 46009 RVA: 0x0027376B File Offset: 0x0027196B
		internal void SetChildItem(GridItem item)
		{
			this._childItem = item;
		}

		// Token: 0x0600B3BA RID: 46010 RVA: 0x00273774 File Offset: 0x00271974
		internal void SetEditFormItem(GridEditFormItem item)
		{
			this._editFormItem = item;
		}

		// Token: 0x17003A0F RID: 14863
		// (get) Token: 0x0600B3BB RID: 46011 RVA: 0x0027377D File Offset: 0x0027197D
		public GridNestedViewItem ChildItem
		{
			get
			{
				return this._childItem as GridNestedViewItem;
			}
		}

		// Token: 0x17003A10 RID: 14864
		// (get) Token: 0x0600B3BC RID: 46012 RVA: 0x0027378A File Offset: 0x0027198A
		public GridEditFormItem EditFormItem
		{
			get
			{
				return this._editFormItem;
			}
		}

		// Token: 0x17003A11 RID: 14865
		// (get) Token: 0x0600B3BD RID: 46013 RVA: 0x00273792 File Offset: 0x00271992
		public override bool HasChildItems
		{
			get
			{
				return this.ChildItem != null;
			}
		}

		// Token: 0x17003A12 RID: 14866
		// (get) Token: 0x0600B3BE RID: 46014 RVA: 0x002737A0 File Offset: 0x002719A0
		public bool HasEditItem
		{
			get
			{
				return this._editFormItem != null;
			}
		}

		// Token: 0x17003A13 RID: 14867
		// (get) Token: 0x0600B3BF RID: 46015 RVA: 0x002737AE File Offset: 0x002719AE
		// (set) Token: 0x0600B3C0 RID: 46016 RVA: 0x002737B6 File Offset: 0x002719B6
		public TableCell DetailTemplateItemDataCell
		{
			get
			{
				return this.detailItemDataCell;
			}
			internal set
			{
				this.detailItemDataCell = value;
			}
		}

		// Token: 0x17003A14 RID: 14868
		// (get) Token: 0x0600B3C1 RID: 46017 RVA: 0x002737BF File Offset: 0x002719BF
		// (set) Token: 0x0600B3C2 RID: 46018 RVA: 0x002737C7 File Offset: 0x002719C7
		public override bool Display
		{
			get
			{
				return base.Display;
			}
			set
			{
				base.Display = value;
				if (this.DetailTemplateItemDataCell != null)
				{
					(this.DetailTemplateItemDataCell.Parent as GridItem).Display = value;
				}
			}
		}

		// Token: 0x17003A15 RID: 14869
		// (get) Token: 0x0600B3C3 RID: 46019 RVA: 0x002737EE File Offset: 0x002719EE
		// (set) Token: 0x0600B3C4 RID: 46020 RVA: 0x002737F6 File Offset: 0x002719F6
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
				if (this.DetailTemplateItemDataCell != null)
				{
					this.DetailTemplateItemDataCell.Parent.Visible = value;
				}
			}
		}

		// Token: 0x0600B3C5 RID: 46021 RVA: 0x00273818 File Offset: 0x00271A18
		protected override bool RemoveSelectedChildren()
		{
			bool result = false;
			foreach (GridTableView gridTableView in this.ChildItem.NestedTableViews)
			{
				foreach (object obj in gridTableView.ItemsHierarchy)
				{
					GridItem gridItem = (GridItem)obj;
					if (gridItem.Selected)
					{
						gridItem.Selected = false;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600B3C6 RID: 46022 RVA: 0x002738A8 File Offset: 0x00271AA8
		protected override void RemoveEditedChildren()
		{
			foreach (GridTableView gridTableView in this.ChildItem.NestedTableViews)
			{
				foreach (object obj in gridTableView.ItemsHierarchy)
				{
					GridItem gridItem = (GridItem)obj;
					if (gridItem.Edit)
					{
						gridItem.Edit = false;
					}
				}
			}
		}

		// Token: 0x0600B3C7 RID: 46023 RVA: 0x00273930 File Offset: 0x00271B30
		public override void SetVisibleChildren(bool value)
		{
			this.ChildItem.Visible = value;
		}

		// Token: 0x0600B3C8 RID: 46024 RVA: 0x00273940 File Offset: 0x00271B40
		protected override void OnExpand()
		{
			if (base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client)
			{
				if (this.IsParentGroupHeaderCollapsed())
				{
					return;
				}
				if (this.Context != null)
				{
					this.ChildItem.Style["display"] = "table-row";
				}
				if (!base.OwnerTableView.RetainExpandStateOnRebind && base.OwnerTableView.IsRetainingState)
				{
					return;
				}
			}
			if (!base.OwnerTableView.RetainExpandStateOnRebind && !base.OwnerTableView.IsRetainingState && ((base.OwnerTableView.HierarchyLoadMode != GridChildLoadMode.ServerOnDemand && base.OwnerTableView.HierarchyLoadMode != GridChildLoadMode.Conditional) || (base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Conditional && base.ConditionalExpanded)))
			{
				return;
			}
			if (!this.HasChildItems)
			{
				return;
			}
			GridRebindReason gridRebindReason = GridRebindReason.PostBackEvent;
			gridRebindReason |= GridRebindReason.DetailTableBinding;
			base.OwnerTableView.ObtainDataSource(gridRebindReason);
			GridIndexCollection gridIndexCollection = new GridIndexCollection();
			if (base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client && base.OwnerTableView.RetainExpandStateOnRebind)
			{
				foreach (object obj in base.OwnerTableView.OwnerGrid.SelectedIndexes)
				{
					string hierarchicalIndex = (string)obj;
					gridIndexCollection.Add(hierarchicalIndex);
				}
			}
			foreach (GridTableView gridTableView in this.ChildItem.NestedTableViews)
			{
				if (base.OwnerTableView.IsUsingModelBinding)
				{
					gridTableView.SelectMethod = base.OwnerTableView.detailTablesSelectMethods[gridTableView.HierarchyIndex];
				}
				gridTableView.DataBind();
			}
			if (base.OwnerTableView.NestedViewTemplate != null && !string.IsNullOrEmpty(base.OwnerTableView.NestedViewSettings.DataSourceID) && this.ChildItem != null)
			{
				this.ChildItem.PerformDataBindWithDataSource();
			}
			if (base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client && base.OwnerTableView.RetainExpandStateOnRebind)
			{
				foreach (object obj2 in gridIndexCollection)
				{
					string hierarchicalIndex2 = (string)obj2;
					base.OwnerTableView.OwnerGrid.SelectedIndexes.Add(hierarchicalIndex2);
				}
			}
		}

		// Token: 0x0600B3C9 RID: 46025 RVA: 0x00273B8C File Offset: 0x00271D8C
		private bool IsParentGroupHeaderCollapsed()
		{
			GridItem[] items = base.OwnerTableView.GetItems(new GridItemType[]
			{
				GridItemType.GroupHeader
			});
			foreach (GridItem gridItem in items)
			{
				if (base.GroupIndexInternal == gridItem.GroupIndexInternal || (base.GroupIndexInternal.StartsWith(gridItem.GroupIndexInternal + "_") && !gridItem.Expanded))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B3CA RID: 46026 RVA: 0x00273C10 File Offset: 0x00271E10
		protected override void OnCollapse()
		{
			if (base.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client)
			{
				this.ChildItem.Style["display"] = "none";
				return;
			}
			if (base.OwnerTableView.HierarchyLoadMode != GridChildLoadMode.ServerOnDemand)
			{
				return;
			}
			if (!this.HasChildItems)
			{
				return;
			}
			foreach (GridTableView gridTableView in this.ChildItem.NestedTableViews)
			{
				gridTableView.ClearViewState();
			}
		}

		// Token: 0x0600B3CB RID: 46027 RVA: 0x00273C82 File Offset: 0x00271E82
		protected override void SetEdited(bool value)
		{
			base.SetEdited(value);
			if (base.OwnerTableView.EditMode == GridEditMode.EditForms && this.HasEditItem)
			{
				this.EditFormItem.Visible = value;
			}
		}

		// Token: 0x17003A16 RID: 14870
		public override TableCell this[string columnUniqueName]
		{
			get
			{
				GridColumn[] renderColumns = base.OwnerTableView.RenderColumns;
				int num = 0;
				bool flag = false;
				foreach (GridColumn gridColumn in renderColumns)
				{
					if (gridColumn.UniqueName.Trim().ToUpper() == columnUniqueName.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					num++;
				}
				if (!flag)
				{
					throw new GridException("Cannot find a cell bound to column name '" + columnUniqueName + "'");
				}
				if (this.Cells.Count <= num && this.Cells.Count > 0 && base.OwnerTableView.ItemTemplate != null)
				{
					return this.Cells[this.Cells.Count - 1];
				}
				return this.Cells[num];
			}
		}

		// Token: 0x17003A17 RID: 14871
		public override TableCell this[GridColumn column]
		{
			get
			{
				return this[column.UniqueName];
			}
		}

		// Token: 0x17003A18 RID: 14872
		// (get) Token: 0x0600B3CE RID: 46030 RVA: 0x00273D88 File Offset: 0x00271F88
		public override IDictionary SavedOldValues
		{
			get
			{
				object obj = this.ViewState["_sov"];
				if (obj == null)
				{
					obj = new Hashtable();
					this.ViewState["_sov"] = obj;
				}
				return (IDictionary)obj;
			}
		}

		// Token: 0x0600B3CF RID: 46031 RVA: 0x00273DC8 File Offset: 0x00271FC8
		public override void InitializeEditorInCell(IGridEditableColumn column)
		{
			if (base.OwnerTableView.ItemTemplate == null)
			{
				GridEditableColumn column2 = column.Column;
				IGridColumnEditor currentColumnEditor = column2.CurrentColumnEditor;
				currentColumnEditor.InitializeFromControl(this[column.Column]);
			}
		}

		// Token: 0x17003A19 RID: 14873
		// (get) Token: 0x0600B3D0 RID: 46032 RVA: 0x00273E02 File Offset: 0x00272002
		public override bool IsDataBound
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003A1A RID: 14874
		// (get) Token: 0x0600B3D1 RID: 46033 RVA: 0x00273E05 File Offset: 0x00272005
		public override bool IsInEditMode
		{
			get
			{
				return this.ItemType == GridItemType.EditItem;
			}
		}

		// Token: 0x04002F56 RID: 12118
		private GridItem _childItem;

		// Token: 0x04002F57 RID: 12119
		private GridEditFormItem _editFormItem;

		// Token: 0x04002F58 RID: 12120
		private TableCell detailItemDataCell;
	}
}
