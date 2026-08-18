using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB1 RID: 3761
	[ToolboxItem(false)]
	public class Crop : ImageEditorDialog
	{
		// Token: 0x06008F49 RID: 36681 RVA: 0x0020456A File Offset: 0x0020276A
		public Crop(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D4D RID: 11597
		// (get) Token: 0x06008F4A RID: 36682 RVA: 0x00204574 File Offset: 0x00202774
		public override string DialogName
		{
			get
			{
				return "Crop";
			}
		}

		// Token: 0x17002D4E RID: 11598
		// (get) Token: 0x06008F4B RID: 36683 RVA: 0x0020457B File Offset: 0x0020277B
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D4F RID: 11599
		// (get) Token: 0x06008F4C RID: 36684 RVA: 0x00204582 File Offset: 0x00202782
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Crop_Title;
			}
		}

		// Token: 0x06008F4D RID: 36685 RVA: 0x0020459C File Offset: 0x0020279C
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.IsTouchSkin();
			this._lblAspectRatio = (HtmlGenericControl)base.FindControlRecursive("lblAspectRatio");
			if (this._lblAspectRatio != null)
			{
				this._lblAspectRatio.InnerText = dialogs.Crop_AspectRatio + ":";
			}
			this._rieAspectRatio = (RadComboBox)base.FindControlRecursive("rieAspectRatio");
			if (this._rieAspectRatio != null)
			{
				this._rieAspectRatio.ToolTip = dialogs.Crop_AspectRatioToolTip;
				if (flag)
				{
					this._rieAspectRatio.Width = Unit.Pixel(175);
				}
				base.SetChildControlRenderMode(this._rieAspectRatio);
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
					this._btnConstraint.Width = Unit.Pixel(20);
					this._btnConstraint.ToggleStates[0].PrimaryIconTop = Unit.Pixel(3);
					this._btnConstraint.ToggleStates[0].PrimaryIconLeft = Unit.Pixel(3);
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
					this._btnSwap.Width = Unit.Pixel(20);
					this._btnSwap.Icon.PrimaryIconTop = Unit.Pixel(4);
					this._btnSwap.Icon.PrimaryIconLeft = Unit.Pixel(2);
					if (flag)
					{
						this._btnSwap.Width = Unit.Pixel(30);
						this._btnSwap.Icon.PrimaryIconTop = Unit.Pixel(8);
						this._btnSwap.Icon.PrimaryIconLeft = Unit.Pixel(8);
					}
				}
				base.SetChildControlRenderMode(this._btnSwap);
			}
			this._btnApply = (RadButton)base.FindControlRecursive("btnApply");
			if (this._btnApply != null)
			{
				this._btnApply.Text = (this._btnApply.ToolTip = dialogs.Crop_Button);
				base.SetChildControlRenderMode(this._btnApply);
			}
			this._btnCancel = (RadButton)base.FindControlRecursive("btnCancel");
			if (this._btnCancel != null)
			{
				this._btnCancel.Text = (this._btnCancel.ToolTip = dialogs.Common_Cancel);
				base.SetChildControlRenderMode(this._btnCancel);
			}
		}

		// Token: 0x06008F4E RID: 36686 RVA: 0x00204AE8 File Offset: 0x00202CE8
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._lblAspectRatio != null && this._rieAspectRatio != null)
			{
				this._lblAspectRatio.Attributes.Add("for", this._rieAspectRatio.ClientID + "_Input");
			}
			base.Render(writer);
		}

		// Token: 0x040027D8 RID: 10200
		private RadComboBox _rieAspectRatio;

		// Token: 0x040027D9 RID: 10201
		private HtmlGenericControl _lblAspectRatio;

		// Token: 0x040027DA RID: 10202
		private TextBox _txtWidth;

		// Token: 0x040027DB RID: 10203
		private Label _lblWidth;

		// Token: 0x040027DC RID: 10204
		private TextBox _txtHeight;

		// Token: 0x040027DD RID: 10205
		private Label _lblHeight;

		// Token: 0x040027DE RID: 10206
		private Literal _lPosition;

		// Token: 0x040027DF RID: 10207
		private TextBox _txtX;

		// Token: 0x040027E0 RID: 10208
		private Label _lblX;

		// Token: 0x040027E1 RID: 10209
		private TextBox _txtY;

		// Token: 0x040027E2 RID: 10210
		private Label _lblY;

		// Token: 0x040027E3 RID: 10211
		private RadButton _btnConstraint;

		// Token: 0x040027E4 RID: 10212
		private RadButton _btnSwap;

		// Token: 0x040027E5 RID: 10213
		private RadButton _btnApply;

		// Token: 0x040027E6 RID: 10214
		private RadButton _btnCancel;
	}
}
