using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000320 RID: 800
	internal class PictureBoxActionList : DesignerActionList
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x000C0CB0 File Offset: 0x000BEEB0
		public PictureBoxActionList(PictureBoxDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000C0CC5 File Offset: 0x000BEEC5
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x000C0CD7 File Offset: 0x000BEED7
		public PictureBoxSizeMode SizeMode
		{
			get
			{
				return ((PictureBox)base.Component).SizeMode;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["SizeMode"].SetValue(base.Component, value);
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000C0CFF File Offset: 0x000BEEFF
		public void ChooseImage()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Image");
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000C0D18 File Offset: 0x000BEF18
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "ChooseImage", SR.GetString("ChooseImageDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ChooseImageDescription"), true),
				new DesignerActionPropertyItem("SizeMode", SR.GetString("SizeModeDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("SizeModeDescription"))
			};
		}

		// Token: 0x04001891 RID: 6289
		private PictureBoxDesigner _designer;
	}
}
