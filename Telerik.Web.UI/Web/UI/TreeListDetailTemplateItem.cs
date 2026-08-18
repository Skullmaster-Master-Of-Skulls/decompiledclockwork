using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200125E RID: 4702
	public class TreeListDetailTemplateItem : TreeListItem, INamingContainer
	{
		// Token: 0x0600C1E8 RID: 49640 RVA: 0x002B4F2A File Offset: 0x002B312A
		public TreeListDetailTemplateItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding, TreeListDataItem parentItem) : base(ownerTreeList, itemType, isDataBinding)
		{
			this.ParentItem = parentItem;
		}

		// Token: 0x17003E86 RID: 16006
		// (get) Token: 0x0600C1E9 RID: 49641 RVA: 0x002B4F3D File Offset: 0x002B313D
		// (set) Token: 0x0600C1EA RID: 49642 RVA: 0x002B4F45 File Offset: 0x002B3145
		public virtual object DataItem { get; set; }

		// Token: 0x17003E87 RID: 16007
		// (get) Token: 0x0600C1EB RID: 49643 RVA: 0x002B4F4E File Offset: 0x002B314E
		// (set) Token: 0x0600C1EC RID: 49644 RVA: 0x002B4F56 File Offset: 0x002B3156
		public TreeListDataItem ParentItem { get; set; }

		// Token: 0x0600C1ED RID: 49645 RVA: 0x002B4F60 File Offset: 0x002B3160
		public override void Initialize(IList<TreeListColumn> columns)
		{
			TableCellCollection cells = this.Cells;
			int num = this.ParentItem.HierarchyIndex.NestedLevel + 1;
			for (int i = 0; i < num; i++)
			{
				this.Cells.Add(this.CreateCellObject());
			}
			TableCell tableCell = this.CreateCellObject();
			cells.Add(tableCell);
			this.TemplateContentCell = tableCell;
			base.OwnerTreeList.DetailTemplate.InstantiateIn(tableCell);
			this.CallOnItemCreated();
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x17003E88 RID: 16008
		// (get) Token: 0x0600C1EE RID: 49646 RVA: 0x002B4FE6 File Offset: 0x002B31E6
		// (set) Token: 0x0600C1EF RID: 49647 RVA: 0x002B4FEE File Offset: 0x002B31EE
		public TableCell TemplateContentCell { get; set; }
	}
}
