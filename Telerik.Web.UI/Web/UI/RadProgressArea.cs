using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x02000981 RID: 2433
	[ToolboxBitmap(typeof(RadProgressArea), "Telerik.Web.UI.ProgressArea.png")]
	[ClientScriptResource("Telerik.Web.UI.RadProgressArea", "Telerik.Web.UI.Upload.RadProgressArea.js")]
	[TelerikToolboxCategory("Upload")]
	[Designer("Telerik.Web.Design.RadProgressAreaDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("ProgressArea", typeof(RadProgressArea))]
	[EmbeddedSkin("ProgressArea", "Default", typeof(RadProgressArea))]
	[RequiredScript(typeof(PopupBehavior))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[ToolboxData("<{0}:RadProgressArea Runat=server></{0}:RadProgressArea>")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(MaterialRipple))]
	public class RadProgressArea : RadWebControl, ILocalizableControl, INamingContainer
	{
		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x06005C66 RID: 23654 RVA: 0x0011A112 File Offset: 0x00118312
		// (set) Token: 0x06005C67 RID: 23655 RVA: 0x0011A132 File Offset: 0x00118332
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Specifies the Text that is displayed in the header area of RadProgressArea")]
		public string HeaderText
		{
			get
			{
				return (string)(this.ViewState["HeaderText"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x0011A145 File Offset: 0x00118345
		// (set) Token: 0x06005C69 RID: 23657 RVA: 0x0011A165 File Offset: 0x00118365
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Misc")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06005C6A RID: 23658 RVA: 0x0011A178 File Offset: 0x00118378
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ProgressAreaStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new ProgressAreaStrings(new LocalizationProvider("RadProgressArea", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06005C6B RID: 23659 RVA: 0x0011A1B7 File Offset: 0x001183B7
		// (set) Token: 0x06005C6C RID: 23660 RVA: 0x0011A1D8 File Offset: 0x001183D8
		[DefaultValue("")]
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadProgressArea will look for its .resx localization files.")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x06005C6D RID: 23661 RVA: 0x0011A22C File Offset: 0x0011842C
		// (set) Token: 0x06005C6E RID: 23662 RVA: 0x0011A25A File Offset: 0x0011845A
		[DefaultValue(false)]
		[Category("Appearance")]
		[Bindable(true)]
		[Description("Gets or sets the value indicating whether the Cancel button should be visible.")]
		public bool DisplayCancelButton
		{
			get
			{
				object obj = this.ViewState["DisplayCancelButton"];
				return obj is bool && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayCancelButton"] = value;
			}
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x06005C6F RID: 23663 RVA: 0x0011A274 File Offset: 0x00118474
		// (set) Token: 0x06005C70 RID: 23664 RVA: 0x0011A2A1 File Offset: 0x001184A1
		[Obsolete("Use the Culture property")]
		[Browsable(true)]
		[DefaultValue("en-US")]
		[Description("Specifies the localization of the RadUpload (the language which will be used).")]
		[Bindable(true)]
		[Category("Appearance")]
		public string Language
		{
			get
			{
				string text = this.ViewState["Language"] as string;
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				this.ViewState["Language"] = value;
			}
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x06005C71 RID: 23665 RVA: 0x0011A2B4 File Offset: 0x001184B4
		// (set) Token: 0x06005C72 RID: 23666 RVA: 0x0011A2E1 File Offset: 0x001184E1
		[ClientControlEvent]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Appearance")]
		[Description("Specifies the client-side function to be executed before the Progress Area status is updated.")]
		[Bindable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("progressUpdating")]
		[DefaultValue("")]
		public string OnClientProgressUpdating
		{
			get
			{
				string text = this.ViewState["OnClientProgressUpdating"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["OnClientProgressUpdating"] = value;
			}
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06005C73 RID: 23667 RVA: 0x0011A2F4 File Offset: 0x001184F4
		// (set) Token: 0x06005C74 RID: 23668 RVA: 0x0011A321 File Offset: 0x00118521
		[Description("Specifies the client-side function to be executed when a progress bar is about to be updated.")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Appearance")]
		[Bindable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("progressBarUpdating")]
		[DefaultValue("")]
		public string OnClientProgressBarUpdating
		{
			get
			{
				string text = this.ViewState["OnClientProgressBarUpdating"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["OnClientProgressBarUpdating"] = value;
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x06005C75 RID: 23669 RVA: 0x0011A334 File Offset: 0x00118534
		// (set) Token: 0x06005C76 RID: 23670 RVA: 0x0011A366 File Offset: 0x00118566
		[Bindable(true)]
		[DefaultValue(ProgressIndicators.TotalProgressBar | ProgressIndicators.TotalProgress | ProgressIndicators.TotalProgressPercent | ProgressIndicators.RequestSize | ProgressIndicators.FilesCountBar | ProgressIndicators.FilesCount | ProgressIndicators.FilesCountPercent | ProgressIndicators.SelectedFilesCount | ProgressIndicators.CurrentFileName | ProgressIndicators.TimeElapsed | ProgressIndicators.TimeEstimated | ProgressIndicators.TransferSpeed)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The visible progress indicators.")]
		[Category("Appearance")]
		public ProgressIndicators ProgressIndicators
		{
			get
			{
				object obj = this.ViewState["ProgressIndicators"];
				if (!(obj is int))
				{
					return ProgressIndicators.TotalProgressBar | ProgressIndicators.TotalProgress | ProgressIndicators.TotalProgressPercent | ProgressIndicators.RequestSize | ProgressIndicators.FilesCountBar | ProgressIndicators.FilesCount | ProgressIndicators.FilesCountPercent | ProgressIndicators.SelectedFilesCount | ProgressIndicators.CurrentFileName | ProgressIndicators.TimeElapsed | ProgressIndicators.TimeEstimated | ProgressIndicators.TransferSpeed;
				}
				return (ProgressIndicators)obj;
			}
			set
			{
				this.ViewState["ProgressIndicators"] = (int)value;
			}
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06005C77 RID: 23671 RVA: 0x0011A37E File Offset: 0x0011857E
		// (set) Token: 0x06005C78 RID: 23672 RVA: 0x0011A386 File Offset: 0x00118586
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ProgressPanel))]
		[Browsable(false)]
		[DefaultValue(null)]
		[Description("The template property")]
		public ITemplate ProgressTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x06005C79 RID: 23673 RVA: 0x0011A396 File Offset: 0x00118596
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x0011A39C File Offset: 0x0011859C
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this._progressPanel = new ProgressPanel(this.ProgressIndicators, this.DisplayCancelButton, this.Localization);
			this._progressPanel.ID = "Panel";
			ITemplate template = this.ProgressTemplate;
			if (this.RenderMode == RenderMode.Classic || base.DesignMode)
			{
				if (template == null)
				{
					template = new ClassicProgressTemplate(this);
				}
				template.InstantiateIn(this._progressPanel);
				WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
				webControl.CssClass = "ruShadow";
				webControl.Controls.Add(this._progressPanel);
				this.Controls.Add(webControl);
				return;
			}
			if (template == null)
			{
				template = new LightProgressTemplate(this);
			}
			template.InstantiateIn(this);
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06005C7B RID: 23675 RVA: 0x0011A454 File Offset: 0x00118654
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadUploadProgressArea RadUploadProgressArea_{0}";
				if (!base.DesignMode)
				{
					text += " RadUploadProgressAreaHidden";
				}
				if (base.Attributes["dir"] == "rtl")
				{
					text += " RadUploadProgressArea_rtl RadUploadProgressArea_{0}_rtl";
				}
				return text;
			}
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x0011A4A4 File Offset: 0x001186A4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderContents(writer);
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x0011A4C4 File Offset: 0x001186C4
		protected override void Render(HtmlTextWriter writer)
		{
			this.ControlWidth = this.Width;
			this.ControlHeight = this.Height;
			if (base.DesignMode)
			{
				base.ChildControlsCreated = false;
				this.EnsureChildControls();
			}
			if (this.ProgressPanel.Visible)
			{
				if (writer is Html32TextWriter)
				{
					writer = new HtmlTextWriter(writer);
				}
				base.Render(writer);
			}
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x0011A522 File Offset: 0x00118722
		public override void DataBind()
		{
			this.CreateChildControls();
			base.ChildControlsCreated = true;
			base.DataBind();
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x0011A537 File Offset: 0x00118737
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06005C80 RID: 23680 RVA: 0x0011A53A File Offset: 0x0011873A
		protected internal bool isInDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x0011A542 File Offset: 0x00118742
		private ProgressPanel ProgressPanel
		{
			get
			{
				this.EnsureChildControls();
				return this._progressPanel;
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x0011A550 File Offset: 0x00118750
		// (set) Token: 0x06005C83 RID: 23683 RVA: 0x0011A558 File Offset: 0x00118758
		private Unit ControlWidth
		{
			get
			{
				return this._controlWidth;
			}
			set
			{
				this._controlWidth = value;
			}
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x0011A561 File Offset: 0x00118761
		// (set) Token: 0x06005C85 RID: 23685 RVA: 0x0011A569 File Offset: 0x00118769
		private Unit ControlHeight
		{
			get
			{
				return this._controlHeight;
			}
			set
			{
				this._controlHeight = value;
			}
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x0011A574 File Offset: 0x00118774
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("progressManagerFound", RadProgressManager.IsRegisteredOnPage(this.Page));
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			if (!string.IsNullOrEmpty(this.HeaderText))
			{
				descriptor.AddProperty("_headerText", this.HeaderText);
			}
			if (this.Context.Request.Browser.IsBrowser("Safari"))
			{
				if (this.ControlWidth != Unit.Empty)
				{
					descriptor.AddProperty("_width", this.ControlWidth.ToString());
				}
				if (this.ControlHeight != Unit.Empty)
				{
					descriptor.AddProperty("_height", this.ControlHeight.ToString());
				}
			}
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x0011A650 File Offset: 0x00118850
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x0011A659 File Offset: 0x00118859
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "progressBarUpdating", this.OnClientProgressBarUpdating);
			RadWebControl.DescribeEvent(descriptor, "progressUpdating", this.OnClientProgressUpdating);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04001630 RID: 5680
		private const ProgressIndicators DefaultProgressIndicators = ProgressIndicators.TotalProgressBar | ProgressIndicators.TotalProgress | ProgressIndicators.TotalProgressPercent | ProgressIndicators.RequestSize | ProgressIndicators.FilesCountBar | ProgressIndicators.FilesCount | ProgressIndicators.FilesCountPercent | ProgressIndicators.SelectedFilesCount | ProgressIndicators.CurrentFileName | ProgressIndicators.TimeElapsed | ProgressIndicators.TimeEstimated | ProgressIndicators.TransferSpeed;

		// Token: 0x04001631 RID: 5681
		public static readonly string PrimaryProgressBarElement = "PrimaryProgressBarElement";

		// Token: 0x04001632 RID: 5682
		public static readonly string PrimaryProgressElement = "PrimaryProgressElement";

		// Token: 0x04001633 RID: 5683
		public static readonly string PrimaryTotalName = "PrimaryTotal";

		// Token: 0x04001634 RID: 5684
		public static readonly string PrimaryValueName = "PrimaryValue";

		// Token: 0x04001635 RID: 5685
		public static readonly string PrimaryPercentName = "PrimaryPercent";

		// Token: 0x04001636 RID: 5686
		public static readonly string SecondaryProgressBarElement = "SecondaryProgressBarElement";

		// Token: 0x04001637 RID: 5687
		public static readonly string SecondaryProgressElement = "SecondaryProgressElement";

		// Token: 0x04001638 RID: 5688
		public static readonly string SecondaryTotalName = "SecondaryTotal";

		// Token: 0x04001639 RID: 5689
		public static readonly string SecondaryValueName = "SecondaryValue";

		// Token: 0x0400163A RID: 5690
		public static readonly string SecondaryPercentName = "SecondaryPercent";

		// Token: 0x0400163B RID: 5691
		public static readonly string CurrentOperationName = "CurrentOperation";

		// Token: 0x0400163C RID: 5692
		public static readonly string TimeElapsedName = "TimeElapsed";

		// Token: 0x0400163D RID: 5693
		public static readonly string TimeEstimatedName = "TimeEstimated";

		// Token: 0x0400163E RID: 5694
		public static readonly string SpeedName = "Speed";

		// Token: 0x0400163F RID: 5695
		public static readonly string CancelButtonName = "CancelButton";

		// Token: 0x04001640 RID: 5696
		public static readonly string ProgressAreaHeader = "ProgressAreaHeader";

		// Token: 0x04001641 RID: 5697
		private ProgressAreaStrings _localization;

		// Token: 0x04001642 RID: 5698
		private Unit _controlWidth;

		// Token: 0x04001643 RID: 5699
		private Unit _controlHeight;

		// Token: 0x04001644 RID: 5700
		private ITemplate _contentTemplate;

		// Token: 0x04001645 RID: 5701
		internal ProgressPanel _progressPanel;
	}
}
