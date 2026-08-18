using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001241 RID: 4673
	public class TreeListFooterItem : TreeListItem, INamingContainer
	{
		// Token: 0x0600C0D2 RID: 49362 RVA: 0x002AEDAE File Offset: 0x002ACFAE
		public TreeListFooterItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
			if (!ownerTreeList.ShowFooter)
			{
				this.Visible = false;
			}
		}

		// Token: 0x0600C0D3 RID: 49363 RVA: 0x002AEDC8 File Offset: 0x002ACFC8
		public TreeListFooterItem(RadTreeList ownerTreeList, TreeListHierarchyIndex hierarchyIndex, TreeListDataItem dataItem, bool isDataBinding) : this(ownerTreeList, TreeListItemType.FooterItem, isDataBinding)
		{
			this.HierarchyIndex = hierarchyIndex;
			this.OwnerDataItem = dataItem;
		}

		// Token: 0x0600C0D4 RID: 49364 RVA: 0x002AEDE8 File Offset: 0x002ACFE8
		public override void Initialize(IList<TreeListColumn> columns)
		{
			int num = this.HierarchyIndex.NestedLevel + 2;
			for (int i = 0; i < num; i++)
			{
				this.Cells.Add(this.CreateCellObject());
			}
			base.Initialize(columns);
		}

		// Token: 0x17003E2F RID: 15919
		// (get) Token: 0x0600C0D5 RID: 49365 RVA: 0x002AEE28 File Offset: 0x002AD028
		// (set) Token: 0x0600C0D6 RID: 49366 RVA: 0x002AEE30 File Offset: 0x002AD030
		internal TreeListDataItem OwnerDataItem { get; set; }

		// Token: 0x17003E30 RID: 15920
		// (get) Token: 0x0600C0D7 RID: 49367 RVA: 0x002AEE39 File Offset: 0x002AD039
		// (set) Token: 0x0600C0D8 RID: 49368 RVA: 0x002AEE41 File Offset: 0x002AD041
		public TreeListHierarchyIndex HierarchyIndex { get; internal set; }

		// Token: 0x17003E31 RID: 15921
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public virtual TableCell this[string columnUniqueName]
		{
			get
			{
				TreeListColumn[] renderColumns = base.OwnerTreeList.RenderColumns;
				int num = this.HierarchyIndex.NestedLevel + 2;
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
	}
}
