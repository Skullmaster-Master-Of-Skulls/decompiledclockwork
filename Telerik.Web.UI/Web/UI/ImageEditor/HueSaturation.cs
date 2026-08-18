using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000523 RID: 1315
	[ToolboxItem(false)]
	public class HueSaturation : ImageEditorDialog
	{
		// Token: 0x06002EEF RID: 12015 RVA: 0x000996E0 File Offset: 0x000978E0
		public HueSaturation(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
			this.Localization = base.ParentImageEditor.Localization.Dialogs;
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x00099700 File Offset: 0x00097900
		public override string DialogName
		{
			get
			{
				return "HueSaturation";
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x00099707 File Offset: 0x00097907
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x0009970E File Offset: 0x0009790E
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.HueSaturation_Title;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x00099725 File Offset: 0x00097925
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x0009972D File Offset: 0x0009792D
		public DialogsStrings Localization { get; set; }

		// Token: 0x06002EF5 RID: 12021 RVA: 0x00099738 File Offset: 0x00097938
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			this.isMetroTouch = base.IsTouchSkin();
			base.SetChildControlRenderMode("hueSlider");
			base.SetChildControlRenderMode("saturationSlider");
			this.AdjustSliderRendering("hueSlider");
			this.AdjustSliderRendering("saturationSlider");
			this.SetControlToolTip("hueTxt", this.Localization.Hue_TxtBoxToolTip);
			this.SetControlToolTip("saturationTxt", this.Localization.Saturation_TxtBoxToolTip);
			RadButton radButton = this.SetRadButtonText("applyBtn", this.Localization.HueSaturation_ApplyBtn_Text);
			this.SetControlToolTip(radButton, this.Localization.HueSaturation_ApplyBtn_ToolTip);
			RadButton radButton2 = this.SetRadButtonText("resetBtn", this.Localization.HueSaturation_ResetBtn_Text);
			this.SetControlToolTip(radButton2, this.Localization.HueSaturation_ApplyBtn_ToolTip);
			base.SetChildControlRenderMode(radButton);
			base.SetChildControlRenderMode(radButton2);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x00099814 File Offset: 0x00097A14
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

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0009987C File Offset: 0x00097A7C
		private WebControl SetControlToolTip(string controlId, string toolTip)
		{
			WebControl webControl = (WebControl)base.FindControlRecursive(controlId);
			if (webControl != null)
			{
				webControl.ToolTip = toolTip;
			}
			return webControl;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000998A1 File Offset: 0x00097AA1
		private void SetControlToolTip(WebControl control, string toolTip)
		{
			if (control != null)
			{
				control.ToolTip = toolTip;
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000998B0 File Offset: 0x00097AB0
		private RadButton SetRadButtonText(string buttonId, string text)
		{
			RadButton radButton = (RadButton)base.FindControlRecursive(buttonId);
			if (radButton != null)
			{
				radButton.Text = text;
			}
			return radButton;
		}

		// Token: 0x04000C56 RID: 3158
		private bool isMetroTouch;
	}
}
