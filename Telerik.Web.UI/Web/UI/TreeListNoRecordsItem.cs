using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001257 RID: 4695
	public class TreeListNoRecordsItem : TreeListItem
	{
		// Token: 0x0600C175 RID: 49525 RVA: 0x002B2578 File Offset: 0x002B0778
		public TreeListNoRecordsItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
		}

		// Token: 0x0600C176 RID: 49526 RVA: 0x002B2584 File Offset: 0x002B0784
		public override void Initialize(IList<TreeListColumn> columns)
		{
			this.NoRecordContentCell = this.CreateCellObject();
			this.Cells.Add(this.NoRecordContentCell);
			ITemplate template = base.OwnerTreeList.NoRecordsTemplate;
			if (template == null)
			{
				template = new TreeListNoRecordsDefaultTempate(base.OwnerTreeList);
			}
			template.InstantiateIn(this.NoRecordContentCell);
			this.CallOnItemCreated();
		}

		// Token: 0x17003E60 RID: 15968
		// (get) Token: 0x0600C177 RID: 49527 RVA: 0x002B25DC File Offset: 0x002B07DC
		// (set) Token: 0x0600C178 RID: 49528 RVA: 0x002B25E4 File Offset: 0x002B07E4
		public TableCell NoRecordContentCell { get; protected set; }
	}
}
