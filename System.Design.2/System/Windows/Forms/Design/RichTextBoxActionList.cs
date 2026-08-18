using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000326 RID: 806
	internal class RichTextBoxActionList : DesignerActionList
	{
		// Token: 0x06001FDE RID: 8158 RVA: 0x000C1213 File Offset: 0x000BF413
		public RichTextBoxActionList(RichTextBoxDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000C1228 File Offset: 0x000BF428
		public void EditLines()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Lines");
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000C1244 File Offset: 0x000BF444
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "EditLines", SR.GetString("EditLinesDisplayName"), SR.GetString("LinksCategoryName"), SR.GetString("EditLinesDescription"), true)
			};
		}

		// Token: 0x0400189A RID: 6298
		private RichTextBoxDesigner _designer;
	}
}
