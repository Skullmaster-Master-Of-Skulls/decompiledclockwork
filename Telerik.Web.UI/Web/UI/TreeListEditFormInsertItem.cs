using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001251 RID: 4689
	public class TreeListEditFormInsertItem : TreeListEditFormItem, ITreeListInsertItem
	{
		// Token: 0x0600C168 RID: 49512 RVA: 0x002B2447 File Offset: 0x002B0647
		public TreeListEditFormInsertItem(RadTreeList ownerTreeList, TreeListDataItem parentItem, bool isDataBinding) : base(ownerTreeList, parentItem, isDataBinding)
		{
			if (base.ParentItem != null && base.ParentItem.IsChildInserted)
			{
				base.ParentItem.InsertItem = this;
			}
		}

		// Token: 0x17003E5C RID: 15964
		// (get) Token: 0x0600C169 RID: 49513 RVA: 0x002B2473 File Offset: 0x002B0673
		public bool IsRoot
		{
			get
			{
				return base.ParentItem == null;
			}
		}

		// Token: 0x17003E5D RID: 15965
		// (get) Token: 0x0600C16A RID: 49514 RVA: 0x002B247E File Offset: 0x002B067E
		// (set) Token: 0x0600C16B RID: 49515 RVA: 0x002B2481 File Offset: 0x002B0681
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
						base.ParentItem.IsChildInserted = false;
						return;
					}
					base.OwnerTreeList.IsItemInserted = false;
				}
			}
		}
	}
}
