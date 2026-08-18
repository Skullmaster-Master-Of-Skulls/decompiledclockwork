using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB5 RID: 3765
	[ToolboxItem(false)]
	public class Rotate : ImageEditorDialog
	{
		// Token: 0x06008F5F RID: 36703 RVA: 0x0020540A File Offset: 0x0020360A
		public Rotate(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D59 RID: 11609
		// (get) Token: 0x06008F60 RID: 36704 RVA: 0x00205414 File Offset: 0x00203614
		public override string DialogName
		{
			get
			{
				return "Rotate";
			}
		}

		// Token: 0x17002D5A RID: 11610
		// (get) Token: 0x06008F61 RID: 36705 RVA: 0x0020541B File Offset: 0x0020361B
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D5B RID: 11611
		// (get) Token: 0x06008F62 RID: 36706 RVA: 0x00205422 File Offset: 0x00203622
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Rotate_Title;
			}
		}

		// Token: 0x06008F63 RID: 36707 RVA: 0x0020543C File Offset: 0x0020363C
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			MainStrings main = base.ParentImageEditor.Localization.Main;
			bool flag = base.IsTouchSkin();
			this._rotateRight = (RadButton)base.FindControlRecursive("RotateRight");
			if (this._rotateRight != null)
			{
				this._rotateRight.ToolTip = main.RotateRight;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					this._rotateRight.Width = Unit.Pixel(25);
					this._rotateRight.Icon.PrimaryIconTop = Unit.Pixel(4);
					this._rotateRight.Icon.PrimaryIconLeft = Unit.Pixel(5);
					if (flag)
					{
						this._rotateRight.Width = Unit.Pixel(30);
						this._rotateRight.Icon.PrimaryIconTop = Unit.Pixel(8);
						this._rotateRight.Icon.PrimaryIconLeft = Unit.Pixel(9);
					}
				}
				base.SetChildControlRenderMode(this._rotateRight);
			}
			this._rotateLeft = (RadButton)base.FindControlRecursive("RotateLeft");
			if (this._rotateLeft != null)
			{
				this._rotateLeft.ToolTip = main.RotateLeft;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					this._rotateLeft.Width = Unit.Pixel(25);
					this._rotateLeft.Icon.PrimaryIconTop = Unit.Pixel(4);
					this._rotateLeft.Icon.PrimaryIconLeft = Unit.Pixel(5);
					if (flag)
					{
						this._rotateLeft.Width = Unit.Pixel(30);
						this._rotateLeft.Icon.PrimaryIconTop = Unit.Pixel(8);
						this._rotateLeft.Icon.PrimaryIconLeft = Unit.Pixel(9);
					}
				}
				base.SetChildControlRenderMode(this._rotateLeft);
			}
			this._ddlDegrees = (RadComboBox)base.FindControlRecursive("DdlDegrees");
			if (this._ddlDegrees != null)
			{
				this._ddlDegrees.ToolTip = dialogs.Rotate_DropDownToolTip;
				if (flag)
				{
					this._ddlDegrees.Width = Unit.Pixel(70);
				}
				base.SetChildControlRenderMode(this._ddlDegrees);
			}
		}

		// Token: 0x040027F9 RID: 10233
		private RadComboBox _ddlDegrees;

		// Token: 0x040027FA RID: 10234
		private RadButton _rotateRight;

		// Token: 0x040027FB RID: 10235
		private RadButton _rotateLeft;
	}
}
