using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200180B RID: 6155
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class RadColorPickerLocalization : StateManager
	{
		// Token: 0x17004883 RID: 18563
		// (get) Token: 0x0600EFCF RID: 61391 RVA: 0x00369DCC File Offset: 0x00367FCC
		// (set) Token: 0x0600EFD0 RID: 61392 RVA: 0x00369DDE File Offset: 0x00367FDE
		[Description("Gets or sets the tooltip of the icon.")]
		[DefaultValue("Pick Color")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string PickColorText
		{
			get
			{
				return base.GetViewStateValue<string>("PickColorText", "Pick Color");
			}
			set
			{
				base.ViewState["PickColorText"] = value;
			}
		}

		// Token: 0x17004884 RID: 18564
		// (get) Token: 0x0600EFD1 RID: 61393 RVA: 0x00369DF1 File Offset: 0x00367FF1
		// (set) Token: 0x0600EFD2 RID: 61394 RVA: 0x00369E03 File Offset: 0x00368003
		[Description("Gets or sets the text in the icon.")]
		[Localizable(true)]
		[DefaultValue("(Current Color is {0})")]
		[NotifyParentProperty(true)]
		public string CurrentColorText
		{
			get
			{
				return base.GetViewStateValue<string>("CurrentColorText", "(Current Color is {0})");
			}
			set
			{
				base.ViewState["CurrentColorText"] = value;
			}
		}

		// Token: 0x17004885 RID: 18565
		// (get) Token: 0x0600EFD3 RID: 61395 RVA: 0x00369E16 File Offset: 0x00368016
		// (set) Token: 0x0600EFD4 RID: 61396 RVA: 0x00369E28 File Offset: 0x00368028
		[NotifyParentProperty(true)]
		[DefaultValue("No Color")]
		[Description("Gets or sets the text for the no color box.")]
		[Localizable(true)]
		public string NoColorText
		{
			get
			{
				return base.GetViewStateValue<string>("NoColorText", "No Color");
			}
			set
			{
				base.ViewState["NoColorText"] = value;
			}
		}

		// Token: 0x17004886 RID: 18566
		// (get) Token: 0x0600EFD5 RID: 61397 RVA: 0x00369E3B File Offset: 0x0036803B
		// (set) Token: 0x0600EFD6 RID: 61398 RVA: 0x00369E4D File Offset: 0x0036804D
		[DefaultValue("Web")]
		[Description("Gets or sets the text for the tab of the Web Palette mode.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string WebPaletteTabText
		{
			get
			{
				return base.GetViewStateValue<string>("WebPaletteTabText", "Web");
			}
			set
			{
				base.ViewState["WebPaletteTabText"] = value;
			}
		}

		// Token: 0x17004887 RID: 18567
		// (get) Token: 0x0600EFD7 RID: 61399 RVA: 0x00369E60 File Offset: 0x00368060
		// (set) Token: 0x0600EFD8 RID: 61400 RVA: 0x00369E72 File Offset: 0x00368072
		[Localizable(true)]
		[Description("Gets or sets the text for the tab of the RGB Sliders palette mode.")]
		[DefaultValue("RGB")]
		[NotifyParentProperty(true)]
		public string RGBSlidersTabText
		{
			get
			{
				return base.GetViewStateValue<string>("RGBSlidersTabText", "RGB");
			}
			set
			{
				base.ViewState["RGBSlidersTabText"] = value;
			}
		}

		// Token: 0x17004888 RID: 18568
		// (get) Token: 0x0600EFD9 RID: 61401 RVA: 0x00369E85 File Offset: 0x00368085
		// (set) Token: 0x0600EFDA RID: 61402 RVA: 0x00369E97 File Offset: 0x00368097
		[Description("Gets or sets the text for the tab of the HSB palette mode.")]
		[DefaultValue("HSB")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HSBTabText
		{
			get
			{
				return base.GetViewStateValue<string>("HSBTabText", "HSB");
			}
			set
			{
				base.ViewState["HSBTabText"] = value;
			}
		}

		// Token: 0x17004889 RID: 18569
		// (get) Token: 0x0600EFDB RID: 61403 RVA: 0x00369EAA File Offset: 0x003680AA
		// (set) Token: 0x0600EFDC RID: 61404 RVA: 0x00369EBC File Offset: 0x003680BC
		[DefaultValue("HSV")]
		[Description("Gets or sets the text for the tab of the HSV palette mode.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HSVTabText
		{
			get
			{
				return base.GetViewStateValue<string>("HSVTabText", "HSV");
			}
			set
			{
				base.ViewState["HSVTabText"] = value;
			}
		}

		// Token: 0x1700488A RID: 18570
		// (get) Token: 0x0600EFDD RID: 61405 RVA: 0x00369ECF File Offset: 0x003680CF
		// (set) Token: 0x0600EFDE RID: 61406 RVA: 0x00369EE1 File Offset: 0x003680E1
		[DefaultValue("Apply")]
		[Localizable(true)]
		[Description("Gets or sets the text for the 'Apply' button.")]
		[NotifyParentProperty(true)]
		public string ApplyButtonText
		{
			get
			{
				return base.GetViewStateValue<string>("ApplyButtonText", "Apply");
			}
			set
			{
				base.ViewState["ApplyButtonText"] = value;
			}
		}

		// Token: 0x1700488B RID: 18571
		// (get) Token: 0x0600EFDF RID: 61407 RVA: 0x00369EF4 File Offset: 0x003680F4
		// (set) Token: 0x0600EFE0 RID: 61408 RVA: 0x00369F06 File Offset: 0x00368106
		[Description("Gets or sets the text for the increase handle of the RGB Slider. (This property is added for complete localization of the slider control. By default it is not used.) ")]
		[DefaultValue("Increase")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string RGBSlidersIncreaseText
		{
			get
			{
				return base.GetViewStateValue<string>("RGBSlidersIncreaseText", "Increase");
			}
			set
			{
				base.ViewState["RGBSlidersIncreaseText"] = value;
			}
		}

		// Token: 0x1700488C RID: 18572
		// (get) Token: 0x0600EFE1 RID: 61409 RVA: 0x00369F19 File Offset: 0x00368119
		// (set) Token: 0x0600EFE2 RID: 61410 RVA: 0x00369F2B File Offset: 0x0036812B
		[NotifyParentProperty(true)]
		[DefaultValue("Decrease")]
		[Localizable(true)]
		[Description("Gets or sets the text for the decrease handle of the RGB Slider. (This property is added for complete localization of the slider control. By default it is not used.) ")]
		public string RGBSlidersDecreaseText
		{
			get
			{
				return base.GetViewStateValue<string>("RGBSlidersDecreaseText", "Decrease");
			}
			set
			{
				base.ViewState["RGBSlidersDecreaseText"] = value;
			}
		}

		// Token: 0x1700488D RID: 18573
		// (get) Token: 0x0600EFE3 RID: 61411 RVA: 0x00369F3E File Offset: 0x0036813E
		// (set) Token: 0x0600EFE4 RID: 61412 RVA: 0x00369F50 File Offset: 0x00368150
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Drag")]
		[Description("Gets or sets the text for the drag handle of the RGB Slider.")]
		public string RGBSlidersDragText
		{
			get
			{
				return base.GetViewStateValue<string>("RGBSlidersDragText", "Drag");
			}
			set
			{
				base.ViewState["RGBSlidersDragText"] = value;
			}
		}

		// Token: 0x1700488E RID: 18574
		// (get) Token: 0x0600EFE5 RID: 61413 RVA: 0x00369F63 File Offset: 0x00368163
		// (set) Token: 0x0600EFE6 RID: 61414 RVA: 0x00369F75 File Offset: 0x00368175
		[DefaultValue("Drag")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets the text for the drag handle of the HSB Slider.")]
		public string HSBSliderDragText
		{
			get
			{
				return base.GetViewStateValue<string>("HSBSliderDragText", "Drag");
			}
			set
			{
				base.ViewState["HSBSliderDragText"] = value;
			}
		}

		// Token: 0x1700488F RID: 18575
		// (get) Token: 0x0600EFE7 RID: 61415 RVA: 0x00369F88 File Offset: 0x00368188
		// (set) Token: 0x0600EFE8 RID: 61416 RVA: 0x00369F9A File Offset: 0x0036819A
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Drag")]
		[Description("Gets or sets the text for the drag handle of the HSV Slider.")]
		public string HSVSliderDragText
		{
			get
			{
				return base.GetViewStateValue<string>("HSVSliderDragText", "Drag");
			}
			set
			{
				base.ViewState["HSVSliderDragText"] = value;
			}
		}

		// Token: 0x17004890 RID: 18576
		// (get) Token: 0x0600EFE9 RID: 61417 RVA: 0x00369FAD File Offset: 0x003681AD
		// (set) Token: 0x0600EFEA RID: 61418 RVA: 0x00369FBF File Offset: 0x003681BF
		[DefaultValue("blank")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets ot sets the title of the icon when no color is selected.")]
		public string BlankColorText
		{
			get
			{
				return base.GetViewStateValue<string>("BlankColorText", "blank");
			}
			set
			{
				base.ViewState["BlankColorText"] = value;
			}
		}

		// Token: 0x17004891 RID: 18577
		// (get) Token: 0x0600EFEB RID: 61419 RVA: 0x00369FD2 File Offset: 0x003681D2
		// (set) Token: 0x0600EFEC RID: 61420 RVA: 0x00369FE4 File Offset: 0x003681E4
		[DefaultValue("Custom Color")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets the text for the Custom Color icon tooltip.")]
		public string CustomColor
		{
			get
			{
				return base.GetViewStateValue<string>("CustomColor", "Custom Color");
			}
			set
			{
				base.ViewState["CustomColor"] = value;
			}
		}

		// Token: 0x17004892 RID: 18578
		// (get) Token: 0x0600EFED RID: 61421 RVA: 0x00369FF7 File Offset: 0x003681F7
		// (set) Token: 0x0600EFEE RID: 61422 RVA: 0x0036A009 File Offset: 0x00368209
		[DefaultValue("Recent Colors")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Gets or sets the text for the Recent Colors label.")]
		public string RecentColors
		{
			get
			{
				return base.GetViewStateValue<string>("RecentColors", "Recent Colors");
			}
			set
			{
				base.ViewState["RecentColors"] = value;
			}
		}

		// Token: 0x17004893 RID: 18579
		// (get) Token: 0x0600EFEF RID: 61423 RVA: 0x0036A01C File Offset: 0x0036821C
		// (set) Token: 0x0600EFF0 RID: 61424 RVA: 0x0036A02E File Offset: 0x0036822E
		[NotifyParentProperty(true)]
		[DefaultValue("OK")]
		[Localizable(true)]
		[Description("Gets or sets the text for 'OK' button.")]
		public string OkButtonText
		{
			get
			{
				return base.GetViewStateValue<string>("OkButtonText", "OK");
			}
			set
			{
				base.ViewState["OkButtonText"] = value;
			}
		}

		// Token: 0x17004894 RID: 18580
		// (get) Token: 0x0600EFF1 RID: 61425 RVA: 0x0036A041 File Offset: 0x00368241
		// (set) Token: 0x0600EFF2 RID: 61426 RVA: 0x0036A053 File Offset: 0x00368253
		[Description("Gets or sets the text for 'Cancel' button.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Cancel")]
		public string CancelButtonText
		{
			get
			{
				return base.GetViewStateValue<string>("CancelButtonText", "Cancel");
			}
			set
			{
				base.ViewState["CancelButtonText"] = value;
			}
		}

		// Token: 0x17004895 RID: 18581
		// (get) Token: 0x0600EFF3 RID: 61427 RVA: 0x0036A066 File Offset: 0x00368266
		// (set) Token: 0x0600EFF4 RID: 61428 RVA: 0x0036A078 File Offset: 0x00368278
		[NotifyParentProperty(true)]
		[DefaultValue("Color hexadecimal code")]
		[Localizable(true)]
		[Description("Gets or sets the text for the color hexadecimal code input.")]
		public string HexInputTitle
		{
			get
			{
				return base.GetViewStateValue<string>("HexInputTitle", "Color hexadecimal code");
			}
			set
			{
				base.ViewState["HexInputTitle"] = value;
			}
		}
	}
}
