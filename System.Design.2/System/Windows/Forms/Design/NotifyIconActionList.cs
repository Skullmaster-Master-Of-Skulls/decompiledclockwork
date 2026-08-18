using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000365 RID: 869
	internal class NotifyIconActionList : DesignerActionList
	{
		// Token: 0x060023C6 RID: 9158 RVA: 0x000DFDCC File Offset: 0x000DDFCC
		public NotifyIconActionList(NotifyIconDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000DFDE1 File Offset: 0x000DDFE1
		public void ChooseIcon()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Icon");
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000DFDFC File Offset: 0x000DDFFC
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "ChooseIcon", SR.GetString("ChooseIconDisplayName"), true)
			};
		}

		// Token: 0x04001A3C RID: 6716
		private NotifyIconDesigner _designer;
	}
}
