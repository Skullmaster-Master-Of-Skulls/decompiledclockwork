using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000127 RID: 295
	[RequiredScript(typeof(Html5Pdf))]
	[ClientScriptResource("Telerik.Web.UI.RadClientExportManager", "Telerik.Web.UI.ClientExportManager.RadClientExportManagerScripts.js")]
	[TelerikToolboxCategory("Visualization")]
	[ToolboxData("<{0}:RadClientExportManager runat=\"server\"></{0}:RadClientExportManager>")]
	[Designer("Telerik.Web.Design.RadClientExportManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxBitmap(typeof(RadClientExportManager), "Telerik.Web.UI.ClientExportManager.png")]
	public class RadClientExportManager : RadWebControl
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x0002D229 File Offset: 0x0002B429
		protected override void OnInit(EventArgs e)
		{
			this.EnableEmbeddedSkins = false;
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.InitComplete += this.Page_InitComplete;
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002D258 File Offset: 0x0002B458
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ClientExportManagerPdfSettingsConverter(),
				new ClientExportManagerSvgSettingsConverter(),
				new ClientExportManagerImageSettingsConverter()
			});
			string script = javaScriptSerializer.Serialize(this.PdfSettings);
			descriptor.AddScriptProperty("pdfSettings", script);
			string script2 = javaScriptSerializer.Serialize(this.ImageSettings);
			descriptor.AddScriptProperty("imageSettings", script2);
			string script3 = javaScriptSerializer.Serialize(this.SvgSettings);
			descriptor.AddScriptProperty("svgSettings", script3);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "pdfExporting", this.OnClientPdfExporting);
			RadWebControl.DescribeEvent(descriptor, "pdfExported", this.OnClientPdfExported);
			RadWebControl.DescribeEvent(descriptor, "imageExporting", this.OnClientImageExporting);
			RadWebControl.DescribeEvent(descriptor, "imageExported", this.OnClientImageExported);
			RadWebControl.DescribeEvent(descriptor, "svgExporting", this.OnClientSvgExporting);
			RadWebControl.DescribeEvent(descriptor, "svgExported", this.OnClientSvgExported);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002D35F File Offset: 0x0002B55F
		protected override void OnPreRender(EventArgs e)
		{
			this.EnableEmbeddedSkins = false;
			base.OnPreRender(e);
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0002D36F File Offset: 0x0002B56F
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002D373 File Offset: 0x0002B573
		protected void Page_InitComplete(object sender, EventArgs e)
		{
			this.Page.InitComplete -= this.Page_InitComplete;
			this.Page.Items[typeof(RadClientExportManager)] = this;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0002D3A7 File Offset: 0x0002B5A7
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0002D3C2 File Offset: 0x0002B5C2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ClientExportManagerPdfSettings PdfSettings
		{
			get
			{
				if (this._pdfSettings == null)
				{
					this._pdfSettings = new ClientExportManagerPdfSettings();
				}
				return this._pdfSettings;
			}
			set
			{
				this._pdfSettings = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0002D3CB File Offset: 0x0002B5CB
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0002D3E6 File Offset: 0x0002B5E6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ClientExportManagerImageSettings ImageSettings
		{
			get
			{
				if (this._imageSettings == null)
				{
					this._imageSettings = new ClientExportManagerImageSettings();
				}
				return this._imageSettings;
			}
			set
			{
				this._imageSettings = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0002D3EF File Offset: 0x0002B5EF
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0002D40A File Offset: 0x0002B60A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ClientExportManagerSvgSettings SvgSettings
		{
			get
			{
				if (this._svgSettings == null)
				{
					this._svgSettings = new ClientExportManagerSvgSettings();
				}
				return this._svgSettings;
			}
			set
			{
				this._svgSettings = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002D413 File Offset: 0x0002B613
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0002D433 File Offset: 0x0002B633
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Browsable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed after the control is loaded")]
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Client-side events")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0002D446 File Offset: 0x0002B646
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0002D466 File Offset: 0x0002B666
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed before the PDF file will be exported")]
		[Bindable(true)]
		[Category("Client-side events")]
		[Browsable(true)]
		public string OnClientPdfExporting
		{
			get
			{
				return ((string)this.ViewState["OnClientPdfExporting"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientPdfExporting"] = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002D479 File Offset: 0x0002B679
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0002D499 File Offset: 0x0002B699
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed after the PDF file is exported")]
		[Browsable(true)]
		[Bindable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientPdfExported
		{
			get
			{
				return ((string)this.ViewState["OnClientPdfExported"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientPdfExported"] = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0002D4AC File Offset: 0x0002B6AC
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0002D4CC File Offset: 0x0002B6CC
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the client-side function which will be executed before the image file will be exported")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientImageExporting
		{
			get
			{
				return ((string)this.ViewState["OnClientImageExporting"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientImageExporting"] = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0002D4DF File Offset: 0x0002B6DF
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0002D4FF File Offset: 0x0002B6FF
		[Category("Client-side events")]
		[Browsable(true)]
		[Bindable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed after the image file is exported")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientImageExported
		{
			get
			{
				return ((string)this.ViewState["OnClientImageExported"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientImageExported"] = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0002D512 File Offset: 0x0002B712
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0002D532 File Offset: 0x0002B732
		[Description("Gets or sets the name of the client-side function which will be executed before the SVG file will be exported")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientSvgExporting
		{
			get
			{
				return ((string)this.ViewState["OnClientSvgExporting"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientSvgExporting"] = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0002D545 File Offset: 0x0002B745
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0002D565 File Offset: 0x0002B765
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the client-side function which will be executed after the SVG file is exported")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientSvgExported
		{
			get
			{
				return ((string)this.ViewState["OnClientSvgExported"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientSvgExported"] = value;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002D578 File Offset: 0x0002B778
		public static RadClientExportManager GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new Exception("The page could not be null.");
			}
			return page.Items[typeof(RadClientExportManager)] as RadClientExportManager;
		}

		// Token: 0x04000304 RID: 772
		private ClientExportManagerPdfSettings _pdfSettings;

		// Token: 0x04000305 RID: 773
		private ClientExportManagerImageSettings _imageSettings;

		// Token: 0x04000306 RID: 774
		private ClientExportManagerSvgSettings _svgSettings;
	}
}
