using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000307 RID: 775
	internal class ListControlUnboundActionList : DesignerActionList
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x000B7B9D File Offset: 0x000B5D9D
		public ListControlUnboundActionList(ComponentDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000B7BB2 File Offset: 0x000B5DB2
		public void InvokeItemsDialog()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Items");
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000B7BCC File Offset: 0x000B5DCC
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "InvokeItemsDialog", SR.GetString("ListControlUnboundActionListEditItemsDisplayName"), SR.GetString("ItemsCategoryName"), SR.GetString("ListControlUnboundActionListEditItemsDescription"), true)
			};
		}

		// Token: 0x040017D6 RID: 6102
		private ComponentDesigner _designer;
	}
}
