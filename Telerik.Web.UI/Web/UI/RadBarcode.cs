using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Barcode;

namespace Telerik.Web.UI
{
	// Token: 0x02000A08 RID: 2568
	[ToolboxBitmap(typeof(RadBarcode), "Telerik.Web.UI.Barcode.png")]
	[Designer("Telerik.Web.Design.RadBarcodeDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[DefaultProperty("Text")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Visualization")]
	public class RadBarcode : WebControl, IScriptControl
	{
		// Token: 0x06006156 RID: 24918 RVA: 0x0016ED48 File Offset: 0x0016CF48
		public RadBarcode()
		{
			this.EnsureLicensing();
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x0016ED58 File Offset: 0x0016CF58
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

		// Token: 0x06006158 RID: 24920 RVA: 0x0016ED90 File Offset: 0x0016CF90
		internal virtual void RenderContentsRectangles(HtmlTextWriter writer)
		{
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0016ED94 File Offset: 0x0016CF94
		protected override void OnPreRender(EventArgs e)
		{
			if (!base.DesignMode && this.RegisterWithScriptManager)
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current != null)
				{
					current.RegisterScriptControl<RadBarcode>(this);
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x0016EDD0 File Offset: 0x0016CFD0
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.RegisterWithScriptManager && !base.DesignMode && (this.ShouldRegisterScripts() || this.EnableAriaSupport))
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current == null)
				{
					throw new InvalidOperationException(string.Format("The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", this.ID));
				}
				current.RegisterScriptDescriptors(this);
				this.RegisterWaiAriaScripts();
			}
			base.Render(writer);
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x0016EE38 File Offset: 0x0016D038
		private bool ShouldRegisterScripts()
		{
			return this.OutputType == BarcodeOutputType.SVG_VML && (HttpContext.Current != null && HttpContext.Current.Request != null) && HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1;
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x0016EE88 File Offset: 0x0016D088
		protected override void RenderContents(HtmlTextWriter writer)
		{
			RadBarcodeBase radBarcodeBase = new SingleSectionBarcode(this.Type);
			if (((SingleSectionBarcode)radBarcodeBase).Code == null)
			{
				switch (this.Type)
				{
				case BarcodeType.EAN8:
					radBarcodeBase = new RadBarcodeEAN8();
					goto IL_AB;
				case BarcodeType.EAN13:
					radBarcodeBase = new RadBarcodeEAN13();
					goto IL_AB;
				case BarcodeType.MSImod10:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo10);
					goto IL_AB;
				case BarcodeType.MSImod11:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo11);
					goto IL_AB;
				case BarcodeType.MSImod1010:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo1010);
					goto IL_AB;
				case BarcodeType.MSImod1110:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo1110);
					goto IL_AB;
				case BarcodeType.UPCA:
					radBarcodeBase = new RadBarcodeUPCA();
					goto IL_AB;
				case BarcodeType.UPCE:
					radBarcodeBase = new RadBarcodeUPCE();
					goto IL_AB;
				}
				radBarcodeBase = null;
			}
			IL_AB:
			if (radBarcodeBase != null)
			{
				radBarcodeBase.Text = this.Text;
				radBarcodeBase.Width = this.Width;
				radBarcodeBase.Height = this.Height;
				radBarcodeBase.RenderChecksum = this.RenderChecksum;
				radBarcodeBase.ShowText = this.ShowText;
				radBarcodeBase.ShowChecksum = this.ShowChecksum;
				radBarcodeBase.ShortLinesLengthPercentage = this.ShortLinesLengthPercentage;
				radBarcodeBase.VerticalTextPositionPercentage = this.VerticalTextPositionPercentage;
				if (this.OutputType == BarcodeOutputType.SVG_VML)
				{
					writer.Write("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\" width=\"{0}%\" height=\"{1}%\" >", 100, 100);
					if (this.Rotation == Rotation.Rotate0)
					{
						radBarcodeBase.RenderContentsRectangles(writer);
					}
					else
					{
						double num = this.Height.Value / this.Width.Value;
						double num2 = radBarcodeBase.Width.Value;
						double num3 = 0.0;
						int num4 = 90;
						if (Rotation.Rotate180 == this.Rotation)
						{
							num = 1.0;
							num4 = 180;
							num2 = radBarcodeBase.Width.Value;
							num3 = radBarcodeBase.Height.Value;
						}
						else if (Rotation.Rotate270 == this.Rotation)
						{
							num4 = 270;
							num2 = 0.0;
							num3 = radBarcodeBase.Height.Value;
						}
						writer.Write(string.Format("<g transform=\"translate({0},{1}),scale({3},{2})\"><g transform=\"rotate({4})\">", new object[]
						{
							num2,
							num3,
							num.ToString(CultureInfo.InvariantCulture),
							(1.0 / num).ToString(CultureInfo.InvariantCulture),
							num4
						}));
						radBarcodeBase.RenderContentsRectangles(writer);
						writer.Write("</g></g>");
					}
					writer.Write("</svg>");
				}
				if (this.OutputType == BarcodeOutputType.EmbeddedPNG)
				{
					string dataURL = radBarcodeBase.GetDataURL(this.LineWidth, this.Rotation);
					if (!string.IsNullOrEmpty(dataURL))
					{
						writer.Write("<img class=\"rbcImg\" src=\"");
						writer.Write(dataURL);
						writer.Write("\"");
						if (!string.IsNullOrEmpty(this.AlternateText))
						{
							writer.Write(" alt=\"{0}\"", this.AlternateText);
						}
						writer.Write(" />");
					}
				}
			}
			else if (this.Type == BarcodeType.QRCode)
			{
				RadBarcodeQRCode radBarcodeQRCode = new RadBarcodeQRCode(this.QRCodeSettings, this.Text);
				string arg = "%";
				int num5 = 100;
				if (this.QRCodeSettings.DotSize == -1)
				{
					int num6 = this.QRCodeSettings.Version * 4 + 25;
					int num7 = (int)Math.Min(this.Width.Value, this.Height.Value);
					this.QRCodeSettings.DotSize = num7 / num6;
				}
				if (this.QRCodeSettings.DotSize != 0)
				{
					arg = "px";
					num5 = this.QRCodeSettings.DotSize * radBarcodeQRCode.NumberOfModules;
				}
				if (this.OutputType == BarcodeOutputType.SVG_VML)
				{
					writer.Write("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\" width=\"{0}{2}\" height=\"{0}{2}\" viewBox=\"0 0 {1} {1}\" >", num5, 2 * radBarcodeQRCode.NumberOfModules, arg);
					radBarcodeQRCode.RenderContentsRectangles(writer, this.Rotation);
					writer.Write("</svg>");
				}
				if (this.OutputType == BarcodeOutputType.EmbeddedPNG)
				{
					string dataURL2 = radBarcodeQRCode.GetDataURL((this.QRCodeSettings.DotSize < 1) ? 1 : this.QRCodeSettings.DotSize, this.Rotation);
					if (!string.IsNullOrEmpty(dataURL2))
					{
						if (this.QRCodeSettings.DotSize > 0)
						{
							writer.Write("<img class=\"rbcImg\" src=\"");
						}
						else
						{
							writer.Write("<img class=\"rbcImg\" style=\"width:100%; height:100%;\" src=\"");
						}
						writer.Write(dataURL2);
						writer.Write("\"");
						if (!string.IsNullOrEmpty(this.AlternateText))
						{
							writer.Write(" alt=\"{0}\"", this.AlternateText);
						}
						writer.Write(" />");
					}
				}
			}
			else if (this.Type == BarcodeType.PDF417)
			{
				RadBarcodePDF417 radBarcodePDF = new RadBarcodePDF417(this.PDF417Settings, this.Text);
				if (this.OutputType == BarcodeOutputType.SVG_VML)
				{
					double num8;
					double num9;
					string text;
					if (this.LineWidth == 0)
					{
						num8 = 100.0;
						num9 = 100.0;
						text = "%";
					}
					else
					{
						num8 = this.Width.Value;
						num9 = this.Height.Value;
						text = "px";
					}
					Pair dimensions = radBarcodePDF.GetDimensions(this.Rotation);
					int num10;
					int num11;
					if (this.Rotation == Rotation.Rotate0 || Rotation.Rotate180 == this.Rotation)
					{
						num10 = 1;
						num11 = this.PDF417Settings.AspectRatio;
					}
					else
					{
						num10 = this.PDF417Settings.AspectRatio;
						num11 = 1;
					}
					writer.Write("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\" width=\"{0}{2}\" height=\"{1}{2}\" viewBox=\"0 0 {3} {4}\" >", new object[]
					{
						num8,
						num9,
						text,
						(int)dimensions.First * num10,
						(int)dimensions.Second * num11
					});
					radBarcodePDF.RenderContentsRectangles(writer, this.PDF417Settings.AspectRatio, num10, num11);
					writer.Write("</svg>");
				}
				if (this.OutputType == BarcodeOutputType.EmbeddedPNG)
				{
					string dataURL3 = radBarcodePDF.GetDataURL((this.LineWidth < 1) ? 1 : this.LineWidth, this.PDF417Settings.AspectRatio, this.Rotation);
					if (!string.IsNullOrEmpty(dataURL3))
					{
						if (this.LineWidth > 0)
						{
							writer.Write("<img class=\"rbcImg\" src=\"");
						}
						else
						{
							writer.Write("<img class=\"rbcImg\" style=\"width:100%; height:100%;\" src=\"");
						}
						writer.Write(dataURL3);
						writer.Write("\"");
						if (!string.IsNullOrEmpty(this.AlternateText))
						{
							writer.Write(" alt=\"{0}\"", this.AlternateText);
						}
						writer.Write(" />");
					}
				}
			}
			base.RenderContents(writer);
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x0016F4FC File Offset: 0x0016D6FC
		private void RegisterWaiAriaScripts()
		{
			string key = "WaiAriaBarcodeScript_" + this.ClientID;
			if (!this.Page.ClientScript.IsStartupScriptRegistered(key))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("(function setBarcodeAriaAttributes(){");
				stringBuilder.AppendLine("var element = document.getElementById('" + this.ClientID + "');");
				stringBuilder.AppendLine("if (element){");
				if (this.OutputType == BarcodeOutputType.SVG_VML)
				{
					stringBuilder.AppendLine("element.setAttribute('role', 'presentation');");
				}
				else
				{
					stringBuilder.AppendLine("element.setAttribute('role', 'img');");
				}
				stringBuilder.AppendLine("element.setAttribute('aria-label', '" + this.ID + "');");
				stringBuilder.AppendLine("element.setAttribute('aria-atomic', 'true');}})();");
				this.Page.ClientScript.RegisterStartupScript(typeof(Page), key, stringBuilder.ToString(), true);
			}
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x0016F5D8 File Offset: 0x0016D7D8
		[Description("Gets rendered Barcode as Image")]
		[Browsable(false)]
		public System.Drawing.Image GetImage()
		{
			if (this.Type == BarcodeType.QRCode)
			{
				RadBarcodeQRCode radBarcodeQRCode = new RadBarcodeQRCode(this.QRCodeSettings, this.Text);
				return radBarcodeQRCode.GetBitmap((this.QRCodeSettings.DotSize < 1) ? 1 : this.QRCodeSettings.DotSize);
			}
			if (this.Type == BarcodeType.PDF417)
			{
				RadBarcodePDF417 radBarcodePDF = new RadBarcodePDF417(this.PDF417Settings, this.Text);
				return radBarcodePDF.GetBitmap((this.LineWidth < 1) ? 1 : this.LineWidth, this.PDF417Settings.AspectRatio);
			}
			RadBarcodeBase radBarcodeBase = new SingleSectionBarcode(this.Type);
			if (((SingleSectionBarcode)radBarcodeBase).Code == null)
			{
				switch (this.Type)
				{
				case BarcodeType.EAN8:
					radBarcodeBase = new RadBarcodeEAN8();
					goto IL_128;
				case BarcodeType.EAN13:
					radBarcodeBase = new RadBarcodeEAN13();
					goto IL_128;
				case BarcodeType.MSImod10:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo10);
					goto IL_128;
				case BarcodeType.MSImod11:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo11);
					goto IL_128;
				case BarcodeType.MSImod1010:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo1010);
					goto IL_128;
				case BarcodeType.MSImod1110:
					radBarcodeBase = new RadBarcodeMSI(CheckMSI.Modulo1110);
					goto IL_128;
				case BarcodeType.UPCA:
					radBarcodeBase = new RadBarcodeUPCA();
					goto IL_128;
				case BarcodeType.UPCE:
					radBarcodeBase = new RadBarcodeUPCE();
					goto IL_128;
				}
				radBarcodeBase = null;
			}
			IL_128:
			if (radBarcodeBase != null)
			{
				radBarcodeBase.Text = this.Text;
				radBarcodeBase.Width = this.Width;
				radBarcodeBase.Height = this.Height;
				radBarcodeBase.RenderChecksum = this.RenderChecksum;
				radBarcodeBase.ShowText = this.ShowText;
				radBarcodeBase.ShowChecksum = this.ShowChecksum;
				radBarcodeBase.ShortLinesLengthPercentage = this.ShortLinesLengthPercentage;
				radBarcodeBase.VerticalTextPositionPercentage = this.VerticalTextPositionPercentage;
				return radBarcodeBase.GetBitmap(this.LineWidth);
			}
			return null;
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x0600615F RID: 24927 RVA: 0x0016F77E File Offset: 0x0016D97E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Specify additional settings when using Type=\"QRCode\"")]
		[Category("Behavior")]
		public QRCodeSettings QRCodeSettings
		{
			get
			{
				if (this.qRCodeSettings == null)
				{
					this.qRCodeSettings = new QRCodeSettings(this.ViewState);
				}
				return this.qRCodeSettings;
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x06006160 RID: 24928 RVA: 0x0016F79F File Offset: 0x0016D99F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Specify additional settings when using Type=\"PDF417\"")]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PDF417Settings PDF417Settings
		{
			get
			{
				if (this.pDF417Settings == null)
				{
					this.pDF417Settings = new PDF417Settings(this.ViewState);
				}
				return this.pDF417Settings;
			}
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x06006161 RID: 24929 RVA: 0x0016F7C0 File Offset: 0x0016D9C0
		// (set) Token: 0x06006162 RID: 24930 RVA: 0x0016F7EF File Offset: 0x0016D9EF
		[Description("Specify the alternate text for the img tag of RadBarcode")]
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string AlternateText
		{
			get
			{
				if (this.ViewState["AlternateText"] != null)
				{
					return this.ViewState["AlternateText"] as string;
				}
				return "";
			}
			set
			{
				this.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x0016F802 File Offset: 0x0016DA02
		// (set) Token: 0x06006164 RID: 24932 RVA: 0x0016F82D File Offset: 0x0016DA2D
		[Description("Specify width of lines in pixels when OutputType is EmbeddedPNG")]
		[DefaultValue(1)]
		[Category("Appearance")]
		public virtual int LineWidth
		{
			get
			{
				if (this.ViewState["LineWidth"] != null)
				{
					return (int)this.ViewState["LineWidth"];
				}
				return 1;
			}
			set
			{
				if (value >= 0)
				{
					this.ViewState["LineWidth"] = value;
					return;
				}
				this.ViewState["LineWidth"] = 0;
			}
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06006165 RID: 24933 RVA: 0x0016F860 File Offset: 0x0016DA60
		// (set) Token: 0x06006166 RID: 24934 RVA: 0x0016F88B File Offset: 0x0016DA8B
		[DefaultValue(BarcodeType.Code128)]
		[Description("Specify the barcode standard that should be used")]
		[Category("Behavior")]
		public virtual BarcodeType Type
		{
			get
			{
				if (this.ViewState["Type"] != null)
				{
					return (BarcodeType)this.ViewState["Type"];
				}
				return BarcodeType.Code128;
			}
			set
			{
				this.ViewState["Type"] = value;
			}
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06006167 RID: 24935 RVA: 0x0016F8A3 File Offset: 0x0016DAA3
		// (set) Token: 0x06006168 RID: 24936 RVA: 0x0016F8CE File Offset: 0x0016DACE
		[Category("Appearance")]
		[DefaultValue(Rotation.Rotate0)]
		[Description("Specify the rotation of the Barcode")]
		public virtual Rotation Rotation
		{
			get
			{
				if (this.ViewState["Rotation"] != null)
				{
					return (Rotation)this.ViewState["Rotation"];
				}
				return Rotation.Rotate0;
			}
			set
			{
				this.ViewState["Rotation"] = value;
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06006169 RID: 24937 RVA: 0x0016F8E6 File Offset: 0x0016DAE6
		// (set) Token: 0x0600616A RID: 24938 RVA: 0x0016F915 File Offset: 0x0016DB15
		[Description("Specify the text that will be encoded as barcode")]
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual string Text
		{
			get
			{
				if (this.ViewState["Text"] != null)
				{
					return (string)this.ViewState["Text"];
				}
				return "";
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x0600616B RID: 24939 RVA: 0x0016F928 File Offset: 0x0016DB28
		// (set) Token: 0x0600616C RID: 24940 RVA: 0x0016F95C File Offset: 0x0016DB5C
		[Category("Appearance")]
		[DefaultValue("300px")]
		[Description("Specify the Width of the control")]
		public override Unit Width
		{
			get
			{
				if (this.ViewState["Width"] != null)
				{
					return (Unit)this.ViewState["Width"];
				}
				return Unit.Pixel(300);
			}
			set
			{
				this.ViewState["Width"] = value;
				base.ControlStyle.Width = value;
			}
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x0600616D RID: 24941 RVA: 0x0016F980 File Offset: 0x0016DB80
		// (set) Token: 0x0600616E RID: 24942 RVA: 0x0016F9B4 File Offset: 0x0016DBB4
		[Category("Appearance")]
		[DefaultValue("300px")]
		[Description("Specify the height of the rendered barcode")]
		public override Unit Height
		{
			get
			{
				if (this.ViewState["Height"] != null)
				{
					return (Unit)this.ViewState["Height"];
				}
				return Unit.Pixel(150);
			}
			set
			{
				this.ViewState["Height"] = value;
				base.ControlStyle.Height = value;
			}
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x0600616F RID: 24943 RVA: 0x0016F9D8 File Offset: 0x0016DBD8
		// (set) Token: 0x06006170 RID: 24944 RVA: 0x0016FA07 File Offset: 0x0016DC07
		[Category("Behavior")]
		[DefaultValue(90f)]
		[Description("Get or set the length ration between shorter and longer lines in the barcode")]
		public virtual float ShortLinesLengthPercentage
		{
			get
			{
				if (this.ViewState["ShortLinesLengthPercentage"] != null)
				{
					return (float)this.ViewState["ShortLinesLengthPercentage"];
				}
				return 90f;
			}
			set
			{
				this.ViewState["ShortLinesLengthPercentage"] = value;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x06006171 RID: 24945 RVA: 0x0016FA1F File Offset: 0x0016DC1F
		// (set) Token: 0x06006172 RID: 24946 RVA: 0x0016FA4E File Offset: 0x0016DC4E
		[Category("Behavior")]
		[Description("Get or set the Y position of the barcode text in percents. By default is 100%. If bottom of the text is cut off by the border of the barcode, than set this property to lower value like 90, or 80, depending on the font size.")]
		[DefaultValue(100f)]
		public virtual float VerticalTextPositionPercentage
		{
			get
			{
				if (this.ViewState["VerticalTextPositionPercentage"] != null)
				{
					return (float)this.ViewState["VerticalTextPositionPercentage"];
				}
				return 100f;
			}
			set
			{
				this.ViewState["VerticalTextPositionPercentage"] = value;
			}
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0016FA68 File Offset: 0x0016DC68
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (base.ControlStyle.Width == Unit.Empty)
			{
				base.ControlStyle.Width = this.Width;
			}
			if (base.ControlStyle.Height == Unit.Empty)
			{
				base.ControlStyle.Height = this.Height;
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0016FACC File Offset: 0x0016DCCC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format("RadBarcode " + cssClass, new object[0]).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x06006175 RID: 24949 RVA: 0x0016FB0F File Offset: 0x0016DD0F
		// (set) Token: 0x06006176 RID: 24950 RVA: 0x0016FB3A File Offset: 0x0016DD3A
		[Category("Behavior")]
		[Description("Get or set whenever to include checksum into the rendered barcode")]
		[DefaultValue(true)]
		public virtual bool RenderChecksum
		{
			get
			{
				return this.ViewState["RenderChecksum"] == null || (bool)this.ViewState["RenderChecksum"];
			}
			set
			{
				this.ViewState["RenderChecksum"] = value;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x06006177 RID: 24951 RVA: 0x0016FB52 File Offset: 0x0016DD52
		// (set) Token: 0x06006178 RID: 24952 RVA: 0x0016FB7D File Offset: 0x0016DD7D
		[DefaultValue(true)]
		[Description("Get or set whenever to show human readable text under the barcode")]
		[Category("Appearance")]
		public virtual bool ShowText
		{
			get
			{
				return this.ViewState["ShowText"] == null || (bool)this.ViewState["ShowText"];
			}
			set
			{
				this.ViewState["ShowText"] = value;
			}
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x06006179 RID: 24953 RVA: 0x0016FB95 File Offset: 0x0016DD95
		// (set) Token: 0x0600617A RID: 24954 RVA: 0x0016FBC0 File Offset: 0x0016DDC0
		[DefaultValue(true)]
		[Category("Appearance")]
		[Description("Get or set whenever to include the checksum after the text under the barcode")]
		public virtual bool ShowChecksum
		{
			get
			{
				return this.ViewState["ShowChecksum"] == null || (bool)this.ViewState["ShowChecksum"];
			}
			set
			{
				this.ViewState["ShowChecksum"] = value;
			}
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x0600617B RID: 24955 RVA: 0x0016FBD8 File Offset: 0x0016DDD8
		// (set) Token: 0x0600617C RID: 24956 RVA: 0x0016FC03 File Offset: 0x0016DE03
		[Description("Change the output type of RadBacrode. \nUse SVG_VML to render SVG (or VML for older browsers) element inside the HTML.\nUse EmbeddedPNG to render img tag with Data URI for src.")]
		[Category("Behavior")]
		[DefaultValue(BarcodeOutputType.SVG_VML)]
		public virtual BarcodeOutputType OutputType
		{
			get
			{
				if (this.ViewState["OutputType"] != null)
				{
					return (BarcodeOutputType)this.ViewState["OutputType"];
				}
				return BarcodeOutputType.SVG_VML;
			}
			set
			{
				this.ViewState["OutputType"] = value;
			}
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x0600617D RID: 24957 RVA: 0x0016FC1B File Offset: 0x0016DE1B
		// (set) Token: 0x0600617E RID: 24958 RVA: 0x0016FC3C File Offset: 0x0016DE3C
		[Category("Behavior")]
		[DefaultValue(false)]
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

		// Token: 0x0600617F RID: 24959 RVA: 0x0016FC54 File Offset: 0x0016DE54
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (this.ShouldRegisterScripts())
			{
				ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("Telerik.Web.UI.RadBarcode", this.ClientID);
				return new ScriptDescriptor[]
				{
					scriptControlDescriptor
				};
			}
			return new ScriptDescriptor[0];
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x0016FC90 File Offset: 0x0016DE90
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (this.ShouldRegisterScripts())
			{
				return new ScriptReference[]
				{
					new ScriptReference("Telerik.Web.UI.Barcode.RadBarcode.js", Assembly.GetExecutingAssembly().FullName)
				};
			}
			return new ScriptReference[0];
		}

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06006181 RID: 24961 RVA: 0x0016FCCC File Offset: 0x0016DECC
		// (set) Token: 0x06006182 RID: 24962 RVA: 0x0016FCF5 File Offset: 0x0016DEF5
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether to register with the ScriptManager control on the page")]
		public virtual bool RegisterWithScriptManager
		{
			get
			{
				object obj = this.ViewState["RegisterWithScriptManager"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["RegisterWithScriptManager"] = value;
			}
		}

		// Token: 0x040017C7 RID: 6087
		private QRCodeSettings qRCodeSettings;

		// Token: 0x040017C8 RID: 6088
		private PDF417Settings pDF417Settings;
	}
}
