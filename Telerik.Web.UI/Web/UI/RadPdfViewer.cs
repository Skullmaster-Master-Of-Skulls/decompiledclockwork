using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.PdfViewer;

namespace Telerik.Web.UI
{
	// Token: 0x02000654 RID: 1620
	[EmbeddedSkin("PdfViewer", typeof(RadPdfViewer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadPdfViewer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadPdfViewer))]
	[RequiredScript(typeof(jQueryPlugins))]
	[TelerikToolboxCategory("Visualization")]
	[ToolboxBitmap(typeof(RadPdfViewer), "Telerik.Web.UI.PdfViewer.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadPdfViewer))]
	[Designer("Telerik.Web.Design.RadPdfViewerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadPdfViewer))]
	[ToolboxData("<{0}:RadPdfViewer runat=\"server\"></{0}:RadPdfViewer>")]
	[ParseChildren(ChildrenAsProperties = true)]
	[RequiredScript(typeof(Html5PdfViewer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadPdfViewer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadPdfViewer))]
	[EmbeddedSkin("PdfViewer", "Default", typeof(RadPdfViewer))]
	[ClientScriptResource("Telerik.Web.UI.RadPdfViewer", "Telerik.Web.UI.PdfViewer.Scripts.RadPdfViewer.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadPdfViewer : RadWebControl
	{
		// Token: 0x06003B69 RID: 15209 RVA: 0x000C19A4 File Offset: 0x000BFBA4
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadWebControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadWebControl.DescribeEvent(descriptor, "render", this.ClientEvents.OnRender);
			RadWebControl.DescribeEvent(descriptor, "open", this.ClientEvents.OnOpen);
			RadWebControl.DescribeEvent(descriptor, "error", this.ClientEvents.OnError);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000C1A28 File Offset: 0x000BFC28
		public RadPdfViewer()
		{
			this.Width = 1000;
			this.Height = 1200;
			this.serializer.MaxJsonLength = this.MaxSerializerLength;
			this.RegisterJSConverters();
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000C1A7D File Offset: 0x000BFC7D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PdfjsProcessing PdfjsProcessingSettings
		{
			get
			{
				if (this._pdfjsProcessing == null)
				{
					this._pdfjsProcessing = new PdfjsProcessing();
				}
				return this._pdfjsProcessing;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000C1A98 File Offset: 0x000BFC98
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DefaultPageSize DefaultPageSizeSettings
		{
			get
			{
				if (this._defaultPageSize == null)
				{
					this._defaultPageSize = new DefaultPageSize();
				}
				return this._defaultPageSize;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x000C1AB3 File Offset: 0x000BFCB3
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x000C1AD4 File Offset: 0x000BFCD4
		[DefaultValue(1)]
		public int ActivePage
		{
			get
			{
				return (int)(this.ViewState["ActivePage"] ?? 1);
			}
			set
			{
				this.ViewState["ActivePage"] = value;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000C1AEC File Offset: 0x000BFCEC
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x000C1B0C File Offset: 0x000BFD0C
		[TypeConverter(typeof(ScaleConverter))]
		public Scale Scale
		{
			get
			{
				return (Scale)(this.ViewState["Scale"] ?? new Scale());
			}
			set
			{
				this.ViewState["Scale"] = value;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000C1B1F File Offset: 0x000BFD1F
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x000C1B48 File Offset: 0x000BFD48
		[DefaultValue(0.5)]
		public double ZoomMin
		{
			get
			{
				return (double)(this.ViewState["ZoomMin"] ?? 0.5);
			}
			set
			{
				this.ViewState["ZoomMin"] = value;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000C1B60 File Offset: 0x000BFD60
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000C1B89 File Offset: 0x000BFD89
		[DefaultValue(4.0)]
		public double ZoomMax
		{
			get
			{
				return (double)(this.ViewState["ZoomMax"] ?? 4.0);
			}
			set
			{
				this.ViewState["ZoomMax"] = value;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000C1BA1 File Offset: 0x000BFDA1
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000C1BCA File Offset: 0x000BFDCA
		[DefaultValue(0.25)]
		public double ZoomRate
		{
			get
			{
				return (double)(this.ViewState["ZoomRate"] ?? 0.25);
			}
			set
			{
				this.ViewState["ZoomRate"] = value;
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000C1BE2 File Offset: 0x000BFDE2
		// (set) Token: 0x06003B78 RID: 15224 RVA: 0x000C1C07 File Offset: 0x000BFE07
		[DefaultValue(2147483647)]
		public int MaxSerializerLength
		{
			get
			{
				return (int)(this.ViewState["MaxSerializerLength"] ?? int.MaxValue);
			}
			set
			{
				this.ViewState["MaxSerializerLength"] = value;
				this.serializer.MaxJsonLength = value;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000C1C2B File Offset: 0x000BFE2B
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000C1C4C File Offset: 0x000BFE4C
		[DefaultValue(true)]
		public bool ToolBar
		{
			get
			{
				return (bool)(this.ViewState["ToolBar"] ?? true);
			}
			set
			{
				this.ViewState["ToolBar"] = value;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000C1C64 File Offset: 0x000BFE64
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolBar ToolBarSettings
		{
			get
			{
				if (this._toolBar == null)
				{
					this._toolBar = new ToolBar();
				}
				return this._toolBar;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x000C1C7F File Offset: 0x000BFE7F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Messages MessagesSettings
		{
			get
			{
				if (this._messages == null)
				{
					this._messages = new Messages();
				}
				return this._messages;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000C1C9A File Offset: 0x000BFE9A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PdfViewerClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new PdfViewerClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000C1CB5 File Offset: 0x000BFEB5
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			this.Skin = "Default";
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write("  \r\n    <style>\r\n        .viewer {\r\n            font-family: Arial, Helvetica, sans-serif;\r\n            font-weight: normal;\r\n        }\r\n\r\n        div.toolbar {\r\n            background-color: #dedede;\r\n            height: 40px;\r\n            text-align: center;\r\n        }\r\n\r\n        .textbox {\r\n            text-align: center;\r\n            width: 30px;\r\n            margin-top: 4px;\r\n            height: 25px;\r\n            float: left;\r\n            margin-left: 20px;\r\n        }\r\n\r\n        .combo-input {\r\n            text-align: center;\r\n            height: 25px;\r\n            margin-top: 4px;\r\n            margin-right: 127px;\r\n        }\r\n\r\n        .page-info {\r\n            float: left;\r\n            margin: 10px 6px;\r\n        }\r\n    </style>\r\n\r\n    <div style='width: 800px; height: 300px;' class='viewer'>\r\n        <div class='toolbar'>\r\n            <input class='textbox ' value='1' />\r\n            <span class='page-info'>of 1 page</span>\r\n            <input class='combo-input' value='Automatic Width'>\r\n        </div>\r\n        <div style='width: 100%; height: 100%; text-align: center; padding-top: 50px;'>\r\n            PDF Content\r\n        </div>\r\n    </div>");
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000C1CD9 File Offset: 0x000BFED9
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			base.RenderContents(writer);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000C1CE9 File Offset: 0x000BFEE9
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
				this.RenderScriptsNoScriptManager(writer);
			}
			if (!base.DesignMode)
			{
				base.Render(writer);
			}
			else
			{
				this.RenderDesignTimeHtml(writer);
			}
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000C1D27 File Offset: 0x000BFF27
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000C1D47 File Offset: 0x000BFF47
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000C1D58 File Offset: 0x000BFF58
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadPdfViewerConverter(),
				new FileConverter(),
				new PdfjsProcessingConverter(),
				new DefaultPageSizeConverter(),
				new ToolBarConverter(),
				new ToolBarMessagesConverter(),
				new PagerConverter(),
				new ErrorMessagesConverter(),
				new LabelsConverter(),
				new ExportAsDialogConverter(),
				new DialogsConverter(),
				new MessagesConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000C1DFD File Offset: 0x000BFFFD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000C1E01 File Offset: 0x000C0001
		protected override string CssClassFormatString
		{
			get
			{
				return "RadPdfViewer RadPdfViewer_{0}";
			}
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000C1E08 File Offset: 0x000C0008
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.DefaultPageSizeSettings).LoadViewState(array[num++]);
			((IStateManager)this.MessagesSettings).LoadViewState(array[num++]);
			((IStateManager)this.PdfjsProcessingSettings).LoadViewState(array[num++]);
			((IStateManager)this.ToolBarSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000C1E88 File Offset: 0x000C0088
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.DefaultPageSizeSettings).SaveViewState(),
				((IStateManager)this.MessagesSettings).SaveViewState(),
				((IStateManager)this.PdfjsProcessingSettings).SaveViewState(),
				((IStateManager)this.ToolBarSettings).SaveViewState()
			};
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000C1EEE File Offset: 0x000C00EE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.DefaultPageSizeSettings).TrackViewState();
			((IStateManager)this.MessagesSettings).TrackViewState();
			((IStateManager)this.PdfjsProcessingSettings).TrackViewState();
			((IStateManager)this.ToolBarSettings).TrackViewState();
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000C1F30 File Offset: 0x000C0130
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadPdfViewerClientState radPdfViewerClientState = null;
			AdvancedJavaScriptSerializer advancedJavaScriptSerializer = new AdvancedJavaScriptSerializer();
			try
			{
				radPdfViewerClientState = advancedJavaScriptSerializer.Deserialize<RadPdfViewerClientState>(text);
			}
			catch (InvalidOperationException ex)
			{
			}
			catch (ArgumentException ex2)
			{
			}
			if (radPdfViewerClientState == null)
			{
				return false;
			}
			this.LoadClientState(radPdfViewerClientState);
			return false;
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000C1F98 File Offset: 0x000C0198
		private void LoadClientState(RadPdfViewerClientState clientState)
		{
			this.ActivePage = clientState.ActivePage;
			this.Scale = clientState.Scale;
		}

		// Token: 0x0400101C RID: 4124
		private PdfjsProcessing _pdfjsProcessing;

		// Token: 0x0400101D RID: 4125
		private DefaultPageSize _defaultPageSize;

		// Token: 0x0400101E RID: 4126
		private ToolBar _toolBar;

		// Token: 0x0400101F RID: 4127
		private Messages _messages;

		// Token: 0x04001020 RID: 4128
		private PdfViewerClientEvents _clientEvents;

		// Token: 0x04001021 RID: 4129
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();
	}
}
