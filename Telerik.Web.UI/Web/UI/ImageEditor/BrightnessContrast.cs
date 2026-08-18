using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000BB2 RID: 2994
	[ToolboxItem(false)]
	public class BrightnessContrast : ImageEditorDialog
	{
		// Token: 0x06007197 RID: 29079 RVA: 0x001A96B9 File Offset: 0x001A78B9
		public BrightnessContrast(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
			this.Localization = base.ParentImageEditor.Localization.Dialogs;
		}

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x06007198 RID: 29080 RVA: 0x001A96D9 File Offset: 0x001A78D9
		public override string DialogName
		{
			get
			{
				return "BrightnessContrast";
			}
		}

		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x06007199 RID: 29081 RVA: 0x001A96E0 File Offset: 0x001A78E0
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x0600719A RID: 29082 RVA: 0x001A96E7 File Offset: 0x001A78E7
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.BrightnessContrast_Title;
			}
		}

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x0600719B RID: 29083 RVA: 0x001A96FE File Offset: 0x001A78FE
		// (set) Token: 0x0600719C RID: 29084 RVA: 0x001A9706 File Offset: 0x001A7906
		public DialogsStrings Localization { get; set; }

		// Token: 0x0600719D RID: 29085 RVA: 0x001A9710 File Offset: 0x001A7910
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			this.isMetroTouch = base.IsTouchSkin();
			this.AdjustSliderRendering("brightnessSlider");
			this.AdjustSliderRendering("contrastSlider");
			this.SetControlToolTip("brightnessTxt", this.Localization.Brightness_TxtBoxToolTip);
			this.SetControlToolTip("contrastTxt", this.Localization.Contrast_TxtBoxToolTip);
			RadButton radButton = this.SetRadButtonText("applyBtn", this.Localization.BrightnessContrast_ApplyBtn_Text);
			this.SetControlToolTip(radButton, this.Localization.BrightnessContrast_ApplyBtn_ToolTip);
			RadButton radButton2 = this.SetRadButtonText("resetBtn", this.Localization.BrightnessContrast_ResetBtn_Text);
			this.SetControlToolTip(radButton2, this.Localization.BrightnessContrast_ResetBtn_ToolTip);
			base.SetChildControlRenderMode("brightnessSlider");
			base.SetChildControlRenderMode("contrastSlider");
			base.SetChildControlRenderMode(radButton);
			base.SetChildControlRenderMode(radButton2);
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x001A97EC File Offset: 0x001A79EC
		private void AdjustSliderRendering(string id)
		{
			RadSlider radSlider = (RadSlider)base.FindControlRecursive(id);
			if (radSlider != null)
			{
				if (this.isMetroTouch)
				{
					radSlider.Width = Unit.Pixel(240);
				}
				radSlider.DecreaseText = this.Localization.Common_Decrease;
				radSlider.IncreaseText = this.Localization.Common_Increase;
				radSlider.DragText = this.Localization.Common_SliderDrag;
			}
		}

		// Token: 0x0600719F RID: 29087 RVA: 0x001A9854 File Offset: 0x001A7A54
		private WebControl SetControlToolTip(string controlId, string toolTip)
		{
			WebControl webControl = (WebControl)base.FindControlRecursive(controlId);
			if (webControl != null)
			{
				webControl.ToolTip = toolTip;
			}
			return webControl;
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x001A9879 File Offset: 0x001A7A79
		private void SetControlToolTip(WebControl control, string toolTip)
		{
			if (control != null)
			{
				control.ToolTip = toolTip;
			}
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x001A9888 File Offset: 0x001A7A88
		private RadButton SetRadButtonText(string buttonId, string text)
		{
			RadButton radButton = (RadButton)base.FindControlRecursive(buttonId);
			if (radButton != null)
			{
				radButton.Text = text;
			}
			return radButton;
		}

		// Token: 0x04001EA3 RID: 7843
		private bool isMetroTouch;
	}
}
