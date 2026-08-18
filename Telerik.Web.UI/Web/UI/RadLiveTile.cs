using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.LiveTile;

namespace Telerik.Web.UI
{
	// Token: 0x02000906 RID: 2310
	[ClientScriptResource("Telerik.Web.UI.RadLiveTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	[Designer("Telerik.Web.Design.RadLiveTileDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadLiveTile), "Telerik.Web.UI.TileList.png")]
	public class RadLiveTile : RadBaseTile
	{
		// Token: 0x06005768 RID: 22376 RVA: 0x0010B3A2 File Offset: 0x001095A2
		public RadLiveTile()
		{
			this._webServiceSettings = new WebServiceSettings(this.ViewState);
			this._clientTemplateAnimationSettings = new ClientTemplateAnimationSettings(this.ViewState);
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x0010B3CC File Offset: 0x001095CC
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			if (!string.IsNullOrEmpty(this.ODataDataSourceID))
			{
				descriptor.AddScriptProperty("odataClientSettings", javaScriptSerializer.Serialize(ODataClientSettings.FromRadLiveTileControl(this)));
			}
			base.DescribeProperty<int>(descriptor, "templateAnimationDuration", this.ClientTemplateAnimationSettings.AnimationDuration, 500);
			base.DescribeProperty<string>(descriptor, "templateAnimationEasing", this.ClientTemplateAnimationSettings.Easing, "");
			base.DescribeProperty<ClientTemplateAnimation>(descriptor, "templateAnimation", this.ClientTemplateAnimationSettings.Animation, ClientTemplateAnimation.Fade);
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x0010B46C File Offset: 0x0010966C
		// (set) Token: 0x0600576B RID: 22379 RVA: 0x0010B48C File Offset: 0x0010968C
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[Description("Gets or sets the HTML template that will be instantiated in the tile after live data request.")]
		[ClientControlProperty]
		public virtual string ClientTemplate
		{
			get
			{
				return (this.ViewState["ClientTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x0010B49F File Offset: 0x0010969F
		// (set) Token: 0x0600576D RID: 22381 RVA: 0x0010B4BF File Offset: 0x001096BF
		[DefaultValue("")]
		[ClientControlProperty]
		public string Value
		{
			get
			{
				return ((string)this.ViewState["Value"]) ?? "";
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x0600576E RID: 22382 RVA: 0x0010B4D2 File Offset: 0x001096D2
		// (set) Token: 0x0600576F RID: 22383 RVA: 0x0010B4F3 File Offset: 0x001096F3
		[DefaultValue(0)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Gets or sets when the interval (in milliseconds) after which the tile will automatically update the content.")]
		public int UpdateInterval
		{
			get
			{
				return (int)(this.ViewState["UpdateInterval"] ?? 0);
			}
			set
			{
				this.ViewState["UpdateInterval"] = value;
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x06005770 RID: 22384 RVA: 0x0010B50B File Offset: 0x0010970B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the settings(service path and method name)for the web service used to populate items.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x0010B513 File Offset: 0x00109713
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Description("Defines the setting configuring the animation of the client template which occurs on data update.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		public ClientTemplateAnimationSettings ClientTemplateAnimationSettings
		{
			get
			{
				return this._clientTemplateAnimationSettings;
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x06005772 RID: 22386 RVA: 0x0010B51B File Offset: 0x0010971B
		// (set) Token: 0x06005773 RID: 22387 RVA: 0x0010B53B File Offset: 0x0010973B
		[Category("Data")]
		[Description("Gets or sets the ODataDataSource used for data binding the client template.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ODataDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ODataDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ODataDataSourceID"] = value;
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x06005774 RID: 22388 RVA: 0x0010B54E File Offset: 0x0010974E
		// (set) Token: 0x06005775 RID: 22389 RVA: 0x0010B56E File Offset: 0x0010976E
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataModelID
		{
			get
			{
				return (string)(this.ViewState["DataModelID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataModelID"] = value;
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x06005776 RID: 22390 RVA: 0x0010B581 File Offset: 0x00109781
		[ClientControlProperty]
		internal override string TileType
		{
			get
			{
				return "RadLiveTile";
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x0010B588 File Offset: 0x00109788
		// (set) Token: 0x06005778 RID: 22392 RVA: 0x0010B5A8 File Offset: 0x001097A8
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function which handles the templateDataBound client-side event.")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("templateDataBound")]
		public string OnClientTemplateDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientTemplateDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTemplateDataBound"] = value;
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x0010B5BB File Offset: 0x001097BB
		// (set) Token: 0x0600577A RID: 22394 RVA: 0x0010B5DB File Offset: 0x001097DB
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function which handles the dataLoading client-side event")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dataLoading")]
		public string OnClientDataLoading
		{
			get
			{
				return (string)(this.ViewState["OnClientDataLoading"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataLoading"] = value;
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x0010B5EE File Offset: 0x001097EE
		// (set) Token: 0x0600577C RID: 22396 RVA: 0x0010B60E File Offset: 0x0010980E
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function which is executed after the data request returns the data successfully.")]
		[ClientPropertyName("dataLoaded")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientDataLoaded
		{
			get
			{
				return (string)(this.ViewState["OnClientDataLoaded"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataLoaded"] = value;
			}
		}

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x0600577D RID: 22397 RVA: 0x0010B621 File Offset: 0x00109821
		// (set) Token: 0x0600577E RID: 22398 RVA: 0x0010B641 File Offset: 0x00109841
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function which is executed after the data request returns error.")]
		[ClientPropertyName("dataLoadingError")]
		public string OnClientDataLoadingError
		{
			get
			{
				return (string)(this.ViewState["OnClientDataLoadingError"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataLoadingError"] = value;
			}
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x0010B654 File Offset: 0x00109854
		protected override void RenderTileBody(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileClientTemplateContainer");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x0010B674 File Offset: 0x00109874
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "clientTemplate", this.ClientTemplate, "");
			base.DescribeProperty<string>(descriptor, "tileType", this.TileType, null);
			base.DescribeProperty<int>(descriptor, "updateInterval", this.UpdateInterval, 0);
			base.DescribeProperty<string>(descriptor, "value", this.Value, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x0010B6DC File Offset: 0x001098DC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "dataLoaded", this.OnClientDataLoaded);
			RadWebControl.DescribeEvent(descriptor, "dataLoading", this.OnClientDataLoading);
			RadWebControl.DescribeEvent(descriptor, "dataLoadingError", this.OnClientDataLoadingError);
			RadWebControl.DescribeEvent(descriptor, "templateDataBound", this.OnClientTemplateDataBound);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04001551 RID: 5457
		private readonly WebServiceSettings _webServiceSettings;

		// Token: 0x04001552 RID: 5458
		private ClientTemplateAnimationSettings _clientTemplateAnimationSettings;
	}
}
