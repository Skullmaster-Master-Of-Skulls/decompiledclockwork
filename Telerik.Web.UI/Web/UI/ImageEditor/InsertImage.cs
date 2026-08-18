using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000BB4 RID: 2996
	[ToolboxItem(false)]
	public class InsertImage : ImageEditorDialog
	{
		// Token: 0x060071A8 RID: 29096 RVA: 0x001A99C4 File Offset: 0x001A7BC4
		public InsertImage(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x060071A9 RID: 29097 RVA: 0x001A99CE File Offset: 0x001A7BCE
		public override string DialogName
		{
			get
			{
				return "InsertImage";
			}
		}

		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x060071AA RID: 29098 RVA: 0x001A99D5 File Offset: 0x001A7BD5
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x060071AB RID: 29099 RVA: 0x001A99DC File Offset: 0x001A7BDC
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.InsertImage_Title;
			}
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x001A99F4 File Offset: 0x001A7BF4
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.IsTouchSkin();
			this._lblUrl = (Label)base.FindControlRecursive("lblUrl");
			if (this._lblUrl != null)
			{
				this._lblUrl.Text = dialogs.Common_Url + ":";
			}
			this._url = (TextBox)base.FindControlRecursive("Url");
			if (this._url != null)
			{
				this._url.ToolTip = dialogs.InsertImage_UrlTxtToolTip;
				if (flag)
				{
					this._url.Width = Unit.Pixel(165);
				}
			}
			this._btnOk = (RadButton)base.FindControlRecursive("btnOk");
			if (this._btnOk != null)
			{
				this._btnOk.Text = (this._btnOk.ToolTip = dialogs.Common_Set);
				base.SetChildControlRenderMode(this._btnOk);
			}
			this._lblWidth = (Label)base.FindControlRecursive("lblWidth");
			if (this._lblWidth != null)
			{
				this._lblWidth.Text = dialogs.Common_Width + ":";
			}
			this._txtWidth = (TextBox)base.FindControlRecursive("txtWidth");
			if (this._txtWidth != null)
			{
				this._txtWidth.ToolTip = dialogs.Common_Width;
			}
			this._lblHeight = (Label)base.FindControlRecursive("lblHeight");
			if (this._lblHeight != null)
			{
				this._lblHeight.Text = dialogs.Common_Height + ":";
			}
			this._txtHeight = (TextBox)base.FindControlRecursive("txtHeight");
			if (this._txtHeight != null)
			{
				this._txtHeight.ToolTip = dialogs.Common_Height;
			}
			this._lPosition = (Literal)base.FindControlRecursive("lPosition");
			if (this._lPosition != null)
			{
				this._lPosition.Text = dialogs.Common_Position + ":";
			}
			this._lblX = (Label)base.FindControlRecursive("lblX");
			if (this._lblX != null)
			{
				this._lblX.Text = dialogs.Common_X + " ";
			}
			this._txtX = (TextBox)base.FindControlRecursive("txtX");
			if (this._txtX != null)
			{
				this._txtX.ToolTip = dialogs.Common_Left;
			}
			this._lblY = (Label)base.FindControlRecursive("lblY");
			if (this._lblY != null)
			{
				this._lblY.Text = dialogs.Common_Y + " ";
			}
			this._txtY = (TextBox)base.FindControlRecursive("txtY");
			if (this._txtY != null)
			{
				this._txtY.ToolTip = dialogs.Common_Top;
			}
			this._btnConstraint = (RadButton)base.FindControlRecursive("btnConstraint");
			if (this._btnConstraint != null)
			{
				this._btnConstraint.ToolTip = dialogs.Common_ConstrainProportions;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					this._btnConstraint.Width = Unit.Pixel(23);
					this._btnConstraint.ToggleStates[0].PrimaryIconTop = Unit.Pixel(3);
					this._btnConstraint.ToggleStates[0].PrimaryIconLeft = Unit.Pixel(4);
					this._btnConstraint.ToggleStates[1].PrimaryIconTop = Unit.Pixel(3);
					this._btnConstraint.ToggleStates[1].PrimaryIconLeft = Unit.Pixel(3);
					if (flag)
					{
						this._btnConstraint.Width = Unit.Pixel(30);
						this._btnConstraint.ToggleStates[0].PrimaryIconTop = Unit.Pixel(8);
						this._btnConstraint.ToggleStates[0].PrimaryIconLeft = Unit.Pixel(9);
						this._btnConstraint.ToggleStates[1].PrimaryIconTop = Unit.Pixel(9);
						this._btnConstraint.ToggleStates[1].PrimaryIconLeft = Unit.Pixel(9);
					}
				}
				base.SetChildControlRenderMode(this._btnConstraint);
			}
			this._btnSwap = (RadButton)base.FindControlRecursive("btnSwap");
			if (this._btnSwap != null)
			{
				this._btnSwap.ToolTip = dialogs.Common_SwapWidthHeight;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					this._btnSwap.Width = Unit.Pixel(23);
					this._btnSwap.Icon.PrimaryIconTop = Unit.Pixel(3);
					this._btnSwap.Icon.PrimaryIconLeft = Unit.Pixel(4);
					if (flag)
					{
						this._btnSwap.Width = Unit.Pixel(30);
						this._btnSwap.Icon.PrimaryIconTop = Unit.Pixel(8);
						this._btnSwap.Icon.PrimaryIconLeft = Unit.Pixel(8);
					}
				}
				base.SetChildControlRenderMode(this._btnSwap);
			}
			this._btnInsert = (RadButton)base.FindControlRecursive("btnInsert");
			if (this._btnInsert != null)
			{
				this._btnInsert.Text = (this._btnInsert.ToolTip = dialogs.Common_Insert);
				base.SetChildControlRenderMode(this._btnInsert);
			}
			this._btnCancel = (RadButton)base.FindControlRecursive("btnCancel");
			if (this._btnCancel != null)
			{
				this._btnCancel.Text = (this._btnCancel.ToolTip = dialogs.Common_Cancel);
				base.SetChildControlRenderMode(this._btnCancel);
			}
		}

		// Token: 0x04001EA5 RID: 7845
		private Label _lblUrl;

		// Token: 0x04001EA6 RID: 7846
		private RadButton _btnOk;

		// Token: 0x04001EA7 RID: 7847
		private TextBox _url;

		// Token: 0x04001EA8 RID: 7848
		private TextBox _txtWidth;

		// Token: 0x04001EA9 RID: 7849
		private Label _lblWidth;

		// Token: 0x04001EAA RID: 7850
		private TextBox _txtHeight;

		// Token: 0x04001EAB RID: 7851
		private Label _lblHeight;

		// Token: 0x04001EAC RID: 7852
		private Literal _lPosition;

		// Token: 0x04001EAD RID: 7853
		private TextBox _txtX;

		// Token: 0x04001EAE RID: 7854
		private Label _lblX;

		// Token: 0x04001EAF RID: 7855
		private TextBox _txtY;

		// Token: 0x04001EB0 RID: 7856
		private Label _lblY;

		// Token: 0x04001EB1 RID: 7857
		private RadButton _btnConstraint;

		// Token: 0x04001EB2 RID: 7858
		private RadButton _btnSwap;

		// Token: 0x04001EB3 RID: 7859
		private RadButton _btnInsert;

		// Token: 0x04001EB4 RID: 7860
		private RadButton _btnCancel;
	}
}
