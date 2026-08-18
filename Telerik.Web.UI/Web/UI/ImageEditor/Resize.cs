using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB4 RID: 3764
	[ToolboxItem(false)]
	public class Resize : ImageEditorDialog
	{
		// Token: 0x06008F59 RID: 36697 RVA: 0x00204F14 File Offset: 0x00203114
		public Resize(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D56 RID: 11606
		// (get) Token: 0x06008F5A RID: 36698 RVA: 0x00204F1E File Offset: 0x0020311E
		public override string DialogName
		{
			get
			{
				return "Resize";
			}
		}

		// Token: 0x17002D57 RID: 11607
		// (get) Token: 0x06008F5B RID: 36699 RVA: 0x00204F25 File Offset: 0x00203125
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D58 RID: 11608
		// (get) Token: 0x06008F5C RID: 36700 RVA: 0x00204F2C File Offset: 0x0020312C
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Resize_Title;
			}
		}

		// Token: 0x06008F5D RID: 36701 RVA: 0x00204F44 File Offset: 0x00203144
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.IsTouchSkin();
			this._lblPresetSizes = (HtmlGenericControl)base.FindControlRecursive("lblPresetSizes");
			if (this._lblPresetSizes != null)
			{
				this._lblPresetSizes.InnerText = dialogs.Resize_PresetSizes + ":";
			}
			this._presetSizes = (RadComboBox)base.FindControlRecursive("PresetSizes");
			if (this._presetSizes != null)
			{
				this._presetSizes.ToolTip = dialogs.Resize_PresetSizesToolTip;
				if (flag)
				{
					this._presetSizes.Width = Unit.Pixel(175);
				}
				base.SetChildControlRenderMode(this._presetSizes);
			}
			this._lblWidth = (Label)base.FindControlRecursive("lblWidth");
			if (this._lblWidth != null)
			{
				this._lblWidth.Text = dialogs.Common_Width + ":";
			}
			this._txtWidth = (TextBox)base.FindControlRecursive("TxtWidth");
			if (this._txtWidth != null)
			{
				this._txtWidth.ToolTip = dialogs.Common_Width;
			}
			this._lblHeight = (Label)base.FindControlRecursive("lblHeight");
			if (this._lblHeight != null)
			{
				this._lblHeight.Text = dialogs.Common_Height + ":";
			}
			this._txtHeight = (TextBox)base.FindControlRecursive("TxtHeight");
			if (this._txtHeight != null)
			{
				this._txtHeight.ToolTip = dialogs.Common_Height;
			}
			this._lblPercent = (Label)base.FindControlRecursive("lblPercent");
			if (this._lblPercent != null)
			{
				this._lblPercent.Text = dialogs.Resize_Percentage + ":";
			}
			this._txtPercent = (TextBox)base.FindControlRecursive("TxtPercent");
			if (this._txtPercent != null)
			{
				this._txtPercent.ToolTip = dialogs.Resize_PercentageToolTip;
			}
			this._btnConstraint = (RadButton)base.FindControlRecursive("BtnConstraint");
			if (this._btnConstraint != null)
			{
				this._btnConstraint.ToolTip = dialogs.Common_ConstrainProportions;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					if (flag)
					{
						this._btnConstraint.Width = Unit.Pixel(30);
						this._btnConstraint.ToggleStates[0].PrimaryIconTop = Unit.Pixel(8);
						this._btnConstraint.ToggleStates[0].PrimaryIconLeft = Unit.Pixel(9);
						this._btnConstraint.ToggleStates[1].PrimaryIconTop = Unit.Pixel(9);
						this._btnConstraint.ToggleStates[1].PrimaryIconLeft = Unit.Pixel(9);
					}
					else
					{
						this._btnConstraint.Width = Unit.Pixel(20);
						this._btnConstraint.ToggleStates[0].PrimaryIconTop = Unit.Pixel(3);
						this._btnConstraint.ToggleStates[1].PrimaryIconTop = Unit.Pixel(4);
					}
				}
				base.SetChildControlRenderMode(this._btnConstraint);
			}
			this._btnSwap = (RadButton)base.FindControlRecursive("BtnSwap");
			if (this._btnSwap != null)
			{
				this._btnSwap.ToolTip = dialogs.Common_SwapWidthHeight;
				if (base.ParentImageEditor.RenderMode == RenderMode.Classic)
				{
					if (flag)
					{
						this._btnSwap.Width = Unit.Pixel(30);
						this._btnSwap.Icon.PrimaryIconTop = Unit.Pixel(8);
						this._btnSwap.Icon.PrimaryIconLeft = Unit.Pixel(8);
					}
					else
					{
						this._btnSwap.Width = Unit.Pixel(20);
						this._btnSwap.Icon.PrimaryIconTop = Unit.Pixel(4);
						this._btnSwap.Icon.PrimaryIconLeft = Unit.Pixel(4);
					}
				}
				base.SetChildControlRenderMode(this._btnSwap);
			}
			this._btnResize = (RadButton)base.FindControlRecursive("BtnResize");
			if (this._btnResize != null)
			{
				this._btnResize.Text = (this._btnResize.ToolTip = dialogs.Resize_Button);
				base.SetChildControlRenderMode(this._btnResize);
			}
			this._btnCancel = (RadButton)base.FindControlRecursive("BtnCancel");
			if (this._btnCancel != null)
			{
				this._btnCancel.Text = (this._btnCancel.ToolTip = dialogs.Common_Cancel);
				base.SetChildControlRenderMode(this._btnCancel);
			}
		}

		// Token: 0x06008F5E RID: 36702 RVA: 0x002053BC File Offset: 0x002035BC
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._lblPresetSizes != null && this._presetSizes != null)
			{
				this._lblPresetSizes.Attributes.Add("for", this._presetSizes.ClientID + "_Input");
			}
			base.Render(writer);
		}

		// Token: 0x040027ED RID: 10221
		private RadComboBox _presetSizes;

		// Token: 0x040027EE RID: 10222
		private HtmlGenericControl _lblPresetSizes;

		// Token: 0x040027EF RID: 10223
		private TextBox _txtWidth;

		// Token: 0x040027F0 RID: 10224
		private Label _lblWidth;

		// Token: 0x040027F1 RID: 10225
		private TextBox _txtHeight;

		// Token: 0x040027F2 RID: 10226
		private Label _lblHeight;

		// Token: 0x040027F3 RID: 10227
		private TextBox _txtPercent;

		// Token: 0x040027F4 RID: 10228
		private Label _lblPercent;

		// Token: 0x040027F5 RID: 10229
		private RadButton _btnConstraint;

		// Token: 0x040027F6 RID: 10230
		private RadButton _btnSwap;

		// Token: 0x040027F7 RID: 10231
		private RadButton _btnResize;

		// Token: 0x040027F8 RID: 10232
		private RadButton _btnCancel;
	}
}
