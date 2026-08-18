using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020016B8 RID: 5816
	[TelerikToolboxCategory("Miscellaneous")]
	[DefaultProperty("DataValue")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxData("<{0}:RadBinaryImage runat=server/>")]
	[Designer("Telerik.Web.Design.RadBinaryImageDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadBinaryImage), "Telerik.Web.UI.BinaryImage.png")]
	public class RadBinaryImage : WebControl
	{
		// Token: 0x170044C3 RID: 17603
		// (get) Token: 0x0600E065 RID: 57445 RVA: 0x0031E69F File Offset: 0x0031C89F
		internal static string HandlerRouterKey
		{
			get
			{
				return "rbi";
			}
		}

		// Token: 0x170044C4 RID: 17604
		// (get) Token: 0x0600E066 RID: 57446 RVA: 0x0031E6A6 File Offset: 0x0031C8A6
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Img;
			}
		}

		// Token: 0x0600E067 RID: 57447 RVA: 0x0031E6AA File Offset: 0x0031C8AA
		public RadBinaryImage()
		{
			this.EnsureLicensing();
		}

		// Token: 0x0600E068 RID: 57448 RVA: 0x0031E6B8 File Offset: 0x0031C8B8
		private void EnsureLicensing()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				try
				{
					LicenseManager.Validate(base.GetType());
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600E069 RID: 57449 RVA: 0x0031E6F0 File Offset: 0x0031C8F0
		protected override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x170044C5 RID: 17605
		// (get) Token: 0x0600E06A RID: 57450 RVA: 0x0031E6F4 File Offset: 0x0031C8F4
		// (set) Token: 0x0600E06B RID: 57451 RVA: 0x0031E71D File Offset: 0x0031C91D
		[Category("Layout")]
		[DefaultValue(ImageAlign.NotSet)]
		[Description("Gets or sets the alignment of the RadBinaryImage control in relation to other elements on the Web page")]
		public virtual ImageAlign ImageAlign
		{
			get
			{
				object obj = this.ViewState["ImageAlign"];
				if (obj == null)
				{
					return ImageAlign.NotSet;
				}
				return (ImageAlign)obj;
			}
			set
			{
				if (value < ImageAlign.NotSet || value > ImageAlign.TextTop)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ImageAlign"] = value;
			}
		}

		// Token: 0x170044C6 RID: 17606
		// (get) Token: 0x0600E06C RID: 57452 RVA: 0x0031E74C File Offset: 0x0031C94C
		// (set) Token: 0x0600E06D RID: 57453 RVA: 0x0031E779 File Offset: 0x0031C979
		[Description("Gets or sets the alternate text displayed in the Image control when the image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip.")]
		[DefaultValue("")]
		[Localizable(true)]
		[Bindable(true)]
		[Category("Appearance")]
		public virtual string AlternateText
		{
			get
			{
				string text = (string)this.ViewState["AlternateText"];
				return text ?? string.Empty;
			}
			set
			{
				this.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x170044C7 RID: 17607
		// (get) Token: 0x0600E06E RID: 57454 RVA: 0x0031E78C File Offset: 0x0031C98C
		// (set) Token: 0x0600E06F RID: 57455 RVA: 0x0031E7B9 File Offset: 0x0031C9B9
		[Description("The URL for the file that contains a detailed description for the image.")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[Category("Accessibility")]
		[DefaultValue("")]
		public virtual string DescriptionUrl
		{
			get
			{
				string text = (string)this.ViewState["DescriptionUrl"];
				return text ?? string.Empty;
			}
			set
			{
				this.ViewState["DescriptionUrl"] = value;
			}
		}

		// Token: 0x170044C8 RID: 17608
		// (get) Token: 0x0600E070 RID: 57456 RVA: 0x0031E7CC File Offset: 0x0031C9CC
		// (set) Token: 0x0600E071 RID: 57457 RVA: 0x0031E7F5 File Offset: 0x0031C9F5
		[Category("Accessibility")]
		[Description("Gets or sets a value indicating whether the control generates an alternate text attribute for an empty string value.")]
		[DefaultValue(false)]
		public virtual bool GenerateEmptyAlternateText
		{
			get
			{
				object obj = this.ViewState["GenerateEmptyAlternateText"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["GenerateEmptyAlternateText"] = value;
			}
		}

		// Token: 0x170044C9 RID: 17609
		// (get) Token: 0x0600E072 RID: 57458 RVA: 0x0031E810 File Offset: 0x0031CA10
		// (set) Token: 0x0600E073 RID: 57459 RVA: 0x0031E839 File Offset: 0x0031CA39
		[Description("Gets or sets a value indicating whether the image data will be persisted if the control is invisible.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool PersistDataIfNotVisible
		{
			get
			{
				object obj = this.ViewState["PersistDataIfNotVisible"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PersistDataIfNotVisible"] = value;
			}
		}

		// Token: 0x170044CA RID: 17610
		// (get) Token: 0x0600E074 RID: 57460 RVA: 0x0031E854 File Offset: 0x0031CA54
		// (set) Token: 0x0600E075 RID: 57461 RVA: 0x0031E8E7 File Offset: 0x0031CAE7
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.GridUrlImageColumnEditorForm, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("Gets or sets the location of an image to display in the RadBinaryImage control.")]
		[Bindable(true)]
		[Category("Appearance")]
		public virtual string ImageUrl
		{
			get
			{
				if (this.DataValue != null)
				{
					if (!this.Visible && !this._isPrerenderExecuted && this.PersistDataIfNotVisible)
					{
						this.ProcessImageData();
					}
					string text = this.ImagePersister.GenerateBinaryImageUrl(this.HttpHandlerUrl);
					text = string.Format("{0}&{1}={2}", text, HandlerRouter.HandlerUrlKey, RadBinaryImage.HandlerRouterKey);
					return (string)(this.ViewState["ImageUrl"] = text);
				}
				return ((string)this.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				if (this.DataValue != null)
				{
					throw new InvalidOperationException("Cannot set image Url if DataValue is already set");
				}
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x170044CB RID: 17611
		// (get) Token: 0x0600E076 RID: 57462 RVA: 0x0031E90D File Offset: 0x0031CB0D
		// (set) Token: 0x0600E077 RID: 57463 RVA: 0x0031E92D File Offset: 0x0031CB2D
		[Description("Specifies the URL of the HTTPHandler from which the image will be served.")]
		[Category("Advanced")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		public string HttpHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HttpHandlerUrl"]) ?? RadBinaryImage.handlerDefaultUrl;
			}
			set
			{
				if (!VirtualPathUtility.IsAppRelative(value))
				{
					throw WebResource.GetHttpHandlerUrlNotAppRelative();
				}
				this.ViewState["HttpHandlerUrl"] = value;
			}
		}

		// Token: 0x170044CC RID: 17612
		// (get) Token: 0x0600E078 RID: 57464 RVA: 0x0031E950 File Offset: 0x0031CB50
		// (set) Token: 0x0600E079 RID: 57465 RVA: 0x0031E979 File Offset: 0x0031CB79
		public BinaryImageStorageLocation ImageStorageLocation
		{
			get
			{
				object obj = this.ViewState["ImageStorageLocation"];
				if (obj == null)
				{
					return BinaryImageStorageLocation.Cache;
				}
				return (BinaryImageStorageLocation)obj;
			}
			set
			{
				this.ViewState["ImageStorageLocation"] = value;
				this._persister = null;
			}
		}

		// Token: 0x0600E07A RID: 57466 RVA: 0x0031E998 File Offset: 0x0031CB98
		protected bool IsDefaultHandlerUrl()
		{
			return this.HttpHandlerUrl == RadBinaryImage.handlerDefaultUrl;
		}

		// Token: 0x170044CD RID: 17613
		// (get) Token: 0x0600E07B RID: 57467 RVA: 0x0031E9AA File Offset: 0x0031CBAA
		protected virtual IRadImagePersister ImagePersister
		{
			get
			{
				if (this._persister == null)
				{
					if (this.ImageStorageLocation == BinaryImageStorageLocation.Cache)
					{
						this._persister = new RadImageHttpCachePersister();
					}
					else if (this.ImageStorageLocation == BinaryImageStorageLocation.Session)
					{
						this._persister = new RadImageSessionPersister();
					}
				}
				return this._persister;
			}
		}

		// Token: 0x170044CE RID: 17614
		// (get) Token: 0x0600E07C RID: 57468 RVA: 0x0031E9E3 File Offset: 0x0031CBE3
		// (set) Token: 0x0600E07D RID: 57469 RVA: 0x0031E9EB File Offset: 0x0031CBEB
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue(null)]
		public byte[] DataValue { get; set; }

		// Token: 0x170044CF RID: 17615
		// (get) Token: 0x0600E07E RID: 57470 RVA: 0x0031E9F4 File Offset: 0x0031CBF4
		internal BinaryImageFilterCollection Filters
		{
			get
			{
				if (this._filters == null)
				{
					this._filters = new BinaryImageFilterCollection();
				}
				return this._filters;
			}
		}

		// Token: 0x170044D0 RID: 17616
		// (get) Token: 0x0600E07F RID: 57471 RVA: 0x0031EA10 File Offset: 0x0031CC10
		// (set) Token: 0x0600E080 RID: 57472 RVA: 0x0031EA39 File Offset: 0x0031CC39
		[DefaultValue(typeof(BinaryImageResizeMode), "None")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Specifies the resize mode that RadBinaryImage will use to resize the image. Default value is BinaryImageResizeMode.None, indicating no resizing will be performed.")]
		public virtual BinaryImageResizeMode ResizeMode
		{
			get
			{
				object obj = base.ViewState["ResizeMode"];
				if (obj != null)
				{
					return (BinaryImageResizeMode)obj;
				}
				return BinaryImageResizeMode.None;
			}
			set
			{
				base.ViewState["ResizeMode"] = value;
			}
		}

		// Token: 0x170044D1 RID: 17617
		// (get) Token: 0x0600E081 RID: 57473 RVA: 0x0031EA54 File Offset: 0x0031CC54
		// (set) Token: 0x0600E082 RID: 57474 RVA: 0x0031EA7D File Offset: 0x0031CC7D
		[DefaultValue(typeof(BinaryImageCropPosition), "Center")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Specifies the crop position RadListView will use when cropping the image. This property has a meaning only when the ResizeMode property is set to BinaryImageResizeMode.Crop. Default value is BinaryImageCropPosition.Center.")]
		public virtual BinaryImageCropPosition CropPosition
		{
			get
			{
				object obj = this.ViewState["CropPosition"];
				if (obj != null)
				{
					return (BinaryImageCropPosition)obj;
				}
				return BinaryImageCropPosition.Center;
			}
			set
			{
				this.ViewState["CropPosition"] = value;
			}
		}

		// Token: 0x170044D2 RID: 17618
		// (get) Token: 0x0600E083 RID: 57475 RVA: 0x0031EA98 File Offset: 0x0031CC98
		// (set) Token: 0x0600E084 RID: 57476 RVA: 0x0031EAC5 File Offset: 0x0031CCC5
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Get or set the name of the file which will appear inside of the SaveAs browser dialog")]
		[Localizable(true)]
		public virtual string SavedImageName
		{
			get
			{
				object obj = base.ViewState["SavedImageName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["SavedImageName"] = value;
			}
		}

		// Token: 0x170044D3 RID: 17619
		// (get) Token: 0x0600E085 RID: 57477 RVA: 0x0031EAD8 File Offset: 0x0031CCD8
		// (set) Token: 0x0600E086 RID: 57478 RVA: 0x0031EAF9 File Offset: 0x0031CCF9
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x0600E087 RID: 57479 RVA: 0x0031EB14 File Offset: 0x0031CD14
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(this.ImageUrl));
			}
			this.AddAlignAttribute(writer);
			if (!string.IsNullOrEmpty(this.AlternateText) || this.GenerateEmptyAlternateText)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.AlternateText);
			}
			if (!string.IsNullOrEmpty(this.DescriptionUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, base.ResolveClientUrl(this.DescriptionUrl));
			}
		}

		// Token: 0x0600E088 RID: 57480 RVA: 0x0031EB94 File Offset: 0x0031CD94
		private void AddAlignAttribute(HtmlTextWriter writer)
		{
			if (this.ImageAlign != ImageAlign.NotSet)
			{
				string value;
				switch (this.ImageAlign)
				{
				case ImageAlign.Left:
					value = "left";
					break;
				case ImageAlign.Right:
					value = "right";
					break;
				case ImageAlign.Baseline:
					value = "baseline";
					break;
				case ImageAlign.Top:
					value = "top";
					break;
				case ImageAlign.Middle:
					value = "middle";
					break;
				case ImageAlign.Bottom:
					value = "bottom";
					break;
				case ImageAlign.AbsBottom:
					value = "absbottom";
					break;
				case ImageAlign.AbsMiddle:
					value = "absmiddle";
					break;
				default:
					value = "texttop";
					break;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Align, value);
			}
		}

		// Token: 0x0600E089 RID: 57481 RVA: 0x0031EC28 File Offset: 0x0031CE28
		public override void DataBind()
		{
			base.DataBind();
			this._isPrerenderExecuted = false;
		}

		// Token: 0x0600E08A RID: 57482 RVA: 0x0031EC38 File Offset: 0x0031CE38
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this._isPrerenderExecuted)
			{
				this.ProcessImageData();
			}
			if (this.VisibleWithoutSource || !string.IsNullOrEmpty(this.ImageUrl))
			{
				if (!base.DesignMode && this.EnableAriaSupport)
				{
					this.RegisterWaiAriaScripts();
				}
				base.Render(writer);
			}
		}

		// Token: 0x170044D4 RID: 17620
		// (get) Token: 0x0600E08B RID: 57483 RVA: 0x0031EC88 File Offset: 0x0031CE88
		// (set) Token: 0x0600E08C RID: 57484 RVA: 0x0031ECB1 File Offset: 0x0031CEB1
		[DefaultValue(true)]
		[Description("Set whenever to render <img> tag when the image src is empty string \nDefault value is true.")]
		public bool VisibleWithoutSource
		{
			get
			{
				object obj = this.ViewState["VWS"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["VWS"] = value;
			}
		}

		// Token: 0x0600E08D RID: 57485 RVA: 0x0031ECC9 File Offset: 0x0031CEC9
		protected override void OnPreRender(EventArgs e)
		{
			this._isPrerenderExecuted = true;
			this.ProcessImageData();
			base.OnPreRender(e);
		}

		// Token: 0x0600E08E RID: 57486 RVA: 0x0031ECE0 File Offset: 0x0031CEE0
		internal void ProcessImageData()
		{
			if (this.DataValue != null && this.DataValue.Length > 0)
			{
				byte[] array = BinaryImageFormatHelper.RemoveNonHeaderBytes(this.DataValue);
				this.AddBuildInFilters();
				BinaryImageFilterProcessor binaryImageFilterProcessor = this.CreateFilterProcessor();
				array = binaryImageFilterProcessor.ProcessFilters(array);
				if (this.AutoAdjustImageControlSize)
				{
					this.AutoAdjustImageElementSize(array);
				}
				this.SetImageFileNameToPersister(this.ImagePersister);
				this.ImagePersister.SaveImage(array);
			}
		}

		// Token: 0x0600E08F RID: 57487 RVA: 0x0031ED48 File Offset: 0x0031CF48
		protected virtual void SetImageFileNameToPersister(IRadImagePersister persister)
		{
			IRadImageFileNameContainer radImageFileNameContainer = persister as IRadImageFileNameContainer;
			if (radImageFileNameContainer != null)
			{
				radImageFileNameContainer.ImageFileName = this.SavedImageName;
			}
		}

		// Token: 0x0600E090 RID: 57488 RVA: 0x0031ED6C File Offset: 0x0031CF6C
		private void AddBuildInFilters()
		{
			if (this.Width != Unit.Empty && this.Height != Unit.Empty && this.ResizeMode != BinaryImageResizeMode.None && this.Width.Type == UnitType.Pixel && this.Height.Type == UnitType.Pixel)
			{
				this.Filters.Add(this.CreateImageTransformationFilter());
			}
		}

		// Token: 0x0600E091 RID: 57489 RVA: 0x0031EDD8 File Offset: 0x0031CFD8
		protected virtual BinaryImageTransformationFilter CreateImageTransformationFilter()
		{
			return new BinaryImageTransformationFilter
			{
				Height = (int)this.Height.Value,
				Width = (int)this.Width.Value,
				Mode = this.ResizeMode,
				CropPosition = this.CropPosition
			};
		}

		// Token: 0x0600E092 RID: 57490 RVA: 0x0031EE30 File Offset: 0x0031D030
		private void AutoAdjustImageElementSize(byte[] dataValue)
		{
			if (dataValue == null)
			{
				return;
			}
			System.Drawing.Image image = BinaryImageFormatHelper.CreateImgFromBytes(dataValue);
			this.Width = image.Width;
			this.Height = image.Height;
		}

		// Token: 0x0600E093 RID: 57491 RVA: 0x0031EE6C File Offset: 0x0031D06C
		private void RegisterWaiAriaScripts()
		{
			string key = "WaiAriaBinaryImageScript" + this.ClientID;
			if (!this.Page.ClientScript.IsStartupScriptRegistered(key))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("(function setBinaryImageAriaAttributes(){");
				stringBuilder.AppendLine("var element = document.getElementById('" + this.ClientID + "');");
				stringBuilder.AppendLine("if (element){");
				stringBuilder.AppendLine("element.setAttribute('role', 'img');");
				stringBuilder.AppendLine("element.setAttribute('aria-label', '" + this.ID + "');");
				stringBuilder.AppendLine("element.setAttribute('aria-atomic', 'true');}})();");
				this.Page.ClientScript.RegisterStartupScript(typeof(Page), key, stringBuilder.ToString(), true);
			}
		}

		// Token: 0x170044D5 RID: 17621
		// (get) Token: 0x0600E094 RID: 57492 RVA: 0x0031EF30 File Offset: 0x0031D130
		// (set) Token: 0x0600E095 RID: 57493 RVA: 0x0031EF59 File Offset: 0x0031D159
		[DefaultValue(true)]
		[Description("Specifies if the HTML image element's dimensions are inferred from image's binary data")]
		[Category("Behavior")]
		public bool AutoAdjustImageControlSize
		{
			get
			{
				object obj = this.ViewState["AutoAdjustImageControlSize"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AutoAdjustImageControlSize"] = value;
			}
		}

		// Token: 0x0600E096 RID: 57494 RVA: 0x0031EF71 File Offset: 0x0031D171
		protected virtual BinaryImageFilterProcessor CreateFilterProcessor()
		{
			return new BinaryImageFilterProcessor(this.Filters);
		}

		// Token: 0x0600E097 RID: 57495 RVA: 0x0031EF7E File Offset: 0x0031D17E
		protected override object SaveViewState()
		{
			string imageUrl = this.ImageUrl;
			return base.SaveViewState();
		}

		// Token: 0x040040F3 RID: 16627
		private IRadImagePersister _persister;

		// Token: 0x040040F4 RID: 16628
		private static readonly string handlerDefaultUrl = "~/Telerik.Web.UI.WebResource.axd";

		// Token: 0x040040F5 RID: 16629
		private BinaryImageFilterCollection _filters;

		// Token: 0x040040F6 RID: 16630
		private bool _isPrerenderExecuted;
	}
}
