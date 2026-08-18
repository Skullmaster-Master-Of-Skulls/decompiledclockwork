using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200124E RID: 4686
	public class TreeListDataInsertItem : TreeListEditableItem, ITreeListInsertItem
	{
		// Token: 0x0600C13F RID: 49471 RVA: 0x002B0E48 File Offset: 0x002AF048
		public TreeListDataInsertItem(RadTreeList ownerTreeList, TreeListDataItem parentItem, bool isDataBinding) : base(ownerTreeList, TreeListItemType.EditItem, isDataBinding)
		{
			this.ParentItem = parentItem;
			if (this.ParentItem != null && this.ParentItem.IsChildInserted)
			{
				this.ParentItem.InsertItem = this;
			}
		}

		// Token: 0x17003E50 RID: 15952
		// (get) Token: 0x0600C140 RID: 49472 RVA: 0x002B0E7F File Offset: 0x002AF07F
		// (set) Token: 0x0600C141 RID: 49473 RVA: 0x002B0E87 File Offset: 0x002AF087
		public TreeListDataItem ParentItem { get; private set; }

		// Token: 0x17003E51 RID: 15953
		// (get) Token: 0x0600C142 RID: 49474 RVA: 0x002B0E90 File Offset: 0x002AF090
		public bool IsRoot
		{
			get
			{
				return this.ParentItem == null;
			}
		}

		// Token: 0x0600C143 RID: 49475 RVA: 0x002B0E9C File Offset: 0x002AF09C
		public override void Initialize(IList<TreeListColumn> columns)
		{
			int num = (this.ParentItem != null) ? (this.ParentItem.HierarchyIndex.NestedLevel + 1) : 1;
			for (int i = 0; i < num; i++)
			{
				this.Cells.Add(this.CreateCellObject());
			}
			this.InitializeInEditMode(columns);
		}

		// Token: 0x0600C144 RID: 49476 RVA: 0x002B0EEC File Offset: 0x002AF0EC
		public virtual void InitializeInEditMode(IList<TreeListColumn> columns)
		{
			TableCellCollection cells = this.Cells;
			for (int i = 0; i < columns.Count; i++)
			{
				TableCell cell = this.CreateCellObject();
				cells.Add(cell);
				TreeListColumn treeListColumn = columns[i];
				TreeListEditableColumn treeListEditableColumn = treeListColumn as TreeListEditableColumn;
				if (treeListEditableColumn != null && !treeListEditableColumn.ReadOnly)
				{
					this.InitializeColumnEditor(cell, i, treeListEditableColumn);
				}
				else
				{
					treeListColumn.InitializeCell(cell, i, this);
				}
			}
			this.CallOnItemCreated();
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CellsDataBound(columns);
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x17003E52 RID: 15954
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		public TableCell this[string columnUniqueName]
		{
			get
			{
				TreeListColumn[] renderColumns = base.OwnerTreeList.RenderColumns;
				int num = (!this.IsRoot) ? (this.ParentItem.HierarchyIndex.NestedLevel + 1) : 1;
				int num2 = 0;
				bool flag = false;
				foreach (TreeListColumn treeListColumn in renderColumns)
				{
					if (treeListColumn.UniqueName.Trim().ToUpper() == columnUniqueName.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					num2++;
				}
				if (flag)
				{
					return this.Cells[num2 + num];
				}
				throw new Exception("Cannot find a cell bound to column name '" + columnUniqueName + "'");
			}
		}

		// Token: 0x17003E53 RID: 15955
		// (get) Token: 0x0600C146 RID: 49478 RVA: 0x002B101E File Offset: 0x002AF21E
		public override bool IsInEditMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003E54 RID: 15956
		// (get) Token: 0x0600C147 RID: 49479 RVA: 0x002B1021 File Offset: 0x002AF221
		// (set) Token: 0x0600C148 RID: 49480 RVA: 0x002B1024 File Offset: 0x002AF224
		public override bool Edit
		{
			get
			{
				return true;
			}
			set
			{
				if (!value)
				{
					if (!this.IsRoot)
					{
						this.ParentItem.IsChildInserted = false;
						return;
					}
					base.OwnerTreeList.IsItemInserted = false;
				}
			}
		}
	}
}
