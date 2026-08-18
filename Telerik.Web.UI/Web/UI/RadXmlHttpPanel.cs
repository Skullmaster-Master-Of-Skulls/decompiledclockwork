using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020009A1 RID: 2465
	[ToolboxData("<{0}:RadXmlHttpPanel Runat=server></{0}:RadXmlHttpPanel>")]
	[RequiredScript(typeof(TouchScrollExtender))]
	[ToolboxBitmap(typeof(RadXmlHttpPanel), "Telerik.Web.UI.XmlHttpPanel.png")]
	[Designer("Telerik.Web.Design.RadXmlHttpPanelDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ParseChildren(false)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadXmlHttpPanel", "Telerik.Web.UI.XmlHttpPanel.RadXmlHttpPanel.js")]
	[TelerikToolboxCategory("Miscellaneous")]
	public class RadXmlHttpPanel : RadWebControl, ICallbackEventHandler
	{
		// Token: 0x06005E0A RID: 24074 RVA: 0x0011F5EF File Offset: 0x0011D7EF
		public static string RenderView(string path)
		{
			return RadXmlHttpPanel.RenderView(path, null);
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x0011F5F8 File Offset: 0x0011D7F8
		public static string RenderView(string path, object data)
		{
			Page page = new Page();
			UserControl userControl = (UserControl)page.LoadControl(path);
			if (data != null)
			{
				Type type = userControl.GetType();
				FieldInfo field = type.GetField("Data");
				if (!(field != null))
				{
					throw new ArgumentException("View file: " + path + " does not have a public Data property");
				}
				field.SetValue(userControl, data);
			}
			page.Controls.Add(userControl);
			StringWriter stringWriter = new StringWriter();
			HttpContext.Current.Server.Execute(page, stringWriter, false);
			return stringWriter.ToString();
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x06005E0C RID: 24076 RVA: 0x0011F684 File Offset: 0x0011D884
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x0011F687 File Offset: 0x0011D887
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x0011F68A File Offset: 0x0011D88A
		protected override string CssClassFormatString
		{
			get
			{
				return "RadXmlHttpPanel";
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0011F691 File Offset: 0x0011D891
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this._tagKey;
			}
		}

		// Token: 0x06005E10 RID: 24080 RVA: 0x0011F699 File Offset: 0x0011D899
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (this.Value != null && !string.IsNullOrEmpty(this.Value) && this.Page != null && !this.Page.IsCallback)
			{
				this.OnServiceRequest();
			}
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x0011F6D4 File Offset: 0x0011D8D4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			XmlHttpPanelEventHandler xmlHttpPanelEventHandler = (XmlHttpPanelEventHandler)base.Events[RadXmlHttpPanel.webServiceRequestEvent];
			if (xmlHttpPanelEventHandler != null)
			{
				descriptor.AddProperty("_isCallbackPanel", true);
			}
			Control control = null;
			if (!string.IsNullOrEmpty(this.LoadingPanelID))
			{
				control = ChildControlHelper.FindControlRecursive(this, this.LoadingPanelID, null);
			}
			string value = (control != null) ? control.ClientID : this.LoadingPanelID;
			descriptor.AddProperty("loadingPanelID", value);
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0011F773 File Offset: 0x0011D973
		// (set) Token: 0x06005E13 RID: 24083 RVA: 0x0011F77B File Offset: 0x0011D97B
		[Obsolete("Please use the OnClientResponseEnding event instead")]
		[Browsable(false)]
		[DefaultValue("")]
		public virtual string OnClientResponseEnd
		{
			get
			{
				return this.OnClientResponseEnding;
			}
			set
			{
				this.OnClientResponseEnding = value;
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x0011F784 File Offset: 0x0011D984
		// (set) Token: 0x06005E15 RID: 24085 RVA: 0x0011F7A4 File Offset: 0x0011D9A4
		[ClientPropertyName("responseEnding")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientResponseEnding
		{
			get
			{
				return ((string)this.ViewState["OnClientResponseEnding"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResponseEnding"] = value;
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06005E16 RID: 24086 RVA: 0x0011F7B7 File Offset: 0x0011D9B7
		// (set) Token: 0x06005E17 RID: 24087 RVA: 0x0011F7D7 File Offset: 0x0011D9D7
		[ClientControlEvent]
		[ClientPropertyName("responseEnded")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public virtual string OnClientResponseEnded
		{
			get
			{
				return ((string)this.ViewState["OnClientResponseEnded"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResponseEnded"] = value;
			}
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x06005E18 RID: 24088 RVA: 0x0011F7EA File Offset: 0x0011D9EA
		// (set) Token: 0x06005E19 RID: 24089 RVA: 0x0011F80A File Offset: 0x0011DA0A
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("responseError")]
		public virtual string OnClientResponseError
		{
			get
			{
				return ((string)this.ViewState["OnClientResponseError"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResponseError"] = value;
			}
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x0011F81D File Offset: 0x0011DA1D
		// (set) Token: 0x06005E1B RID: 24091 RVA: 0x0011F83D File Offset: 0x0011DA3D
		[Category("Client")]
		[TypeConverter("Telerik.Web.Design.AjaxLoadingPanelIDConverter")]
		[Description("Gets or sets the ID of the RadAjaxLoadingPanel control that will be displayed over the control during AJAX requests.")]
		[DefaultValue("")]
		public string LoadingPanelID
		{
			get
			{
				return ((string)this.ViewState["LoadingPanelID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["LoadingPanelID"] = value;
			}
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06005E1C RID: 24092 RVA: 0x0011F850 File Offset: 0x0011DA50
		// (set) Token: 0x06005E1D RID: 24093 RVA: 0x0011F871 File Offset: 0x0011DA71
		[DefaultValue(XmlHttpPanelRenderMode.Inline)]
		[Category("Layout")]
		[Description(" Gets or sets a value that indicates how the content of an RadXmlHttpPanel control will be wrapped on a page.")]
		public new XmlHttpPanelRenderMode RenderMode
		{
			get
			{
				return (XmlHttpPanelRenderMode)(this.ViewState["RenderMode"] ?? XmlHttpPanelRenderMode.Inline);
			}
			set
			{
				this.ViewState["RenderMode"] = value;
				this._tagKey = ((value == XmlHttpPanelRenderMode.Block) ? HtmlTextWriterTag.Div : HtmlTextWriterTag.Span);
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x0011F898 File Offset: 0x0011DA98
		// (set) Token: 0x06005E1F RID: 24095 RVA: 0x0011F8A6 File Offset: 0x0011DAA6
		[DefaultValue(false)]
		[ClientControlProperty]
		public bool EnableClientScriptEvaluation
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableClientScriptEvaluation", false);
			}
			set
			{
				this.ViewState["EnableClientScriptEvaluation"] = value;
			}
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x0011F8BE File Offset: 0x0011DABE
		// (set) Token: 0x06005E21 RID: 24097 RVA: 0x0011F8DE File Offset: 0x0011DADE
		[DefaultValue("")]
		[ClientControlProperty]
		public string WebMethodName
		{
			get
			{
				return ((string)this.ViewState["WebMethodName"]) ?? "";
			}
			set
			{
				this.ViewState["WebMethodName"] = value;
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x06005E22 RID: 24098 RVA: 0x0011F8F4 File Offset: 0x0011DAF4
		// (set) Token: 0x06005E23 RID: 24099 RVA: 0x0011F949 File Offset: 0x0011DB49
		[DefaultValue("")]
		[ClientControlProperty]
		[UrlProperty]
		public string WebMethodPath
		{
			get
			{
				string text = ((string)this.ViewState["WebMethodPath"]) ?? "";
				if (text.StartsWith("~/"))
				{
					text = text.Replace("~", HttpContext.Current.Request.ApplicationPath);
				}
				return text;
			}
			set
			{
				this.ViewState["WebMethodPath"] = value;
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x06005E24 RID: 24100 RVA: 0x0011F95C File Offset: 0x0011DB5C
		// (set) Token: 0x06005E25 RID: 24101 RVA: 0x0011F97D File Offset: 0x0011DB7D
		[DefaultValue(XmlHttpPanelWcfRequestMethod.GET)]
		[ClientControlProperty]
		public XmlHttpPanelWcfRequestMethod WcfRequestMethod
		{
			get
			{
				return (XmlHttpPanelWcfRequestMethod)(this.ViewState["WcfRequestMethod"] ?? XmlHttpPanelWcfRequestMethod.GET);
			}
			set
			{
				this.ViewState["WcfRequestMethod"] = value;
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06005E26 RID: 24102 RVA: 0x0011F995 File Offset: 0x0011DB95
		// (set) Token: 0x06005E27 RID: 24103 RVA: 0x0011F9B5 File Offset: 0x0011DBB5
		[DefaultValue("")]
		[UrlProperty]
		[ClientControlProperty]
		public string WcfServicePath
		{
			get
			{
				return ((string)this.ViewState["WcfServicePath"]) ?? "";
			}
			set
			{
				this.ViewState["WcfServicePath"] = value;
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06005E28 RID: 24104 RVA: 0x0011F9C8 File Offset: 0x0011DBC8
		// (set) Token: 0x06005E29 RID: 24105 RVA: 0x0011F9E8 File Offset: 0x0011DBE8
		[ClientControlProperty]
		[DefaultValue("")]
		public string WcfServiceMethod
		{
			get
			{
				return ((string)this.ViewState["WcfServiceMethod"]) ?? "";
			}
			set
			{
				this.ViewState["WcfServiceMethod"] = value;
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x0011F9FB File Offset: 0x0011DBFB
		// (set) Token: 0x06005E2B RID: 24107 RVA: 0x0011FA1B File Offset: 0x0011DC1B
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

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x06005E2C RID: 24108 RVA: 0x0011FA2E File Offset: 0x0011DC2E
		// (set) Token: 0x06005E2D RID: 24109 RVA: 0x0011FA53 File Offset: 0x0011DC53
		[DefaultValue(2097152)]
		[Category("Behavior")]
		[Description("Property to define the maximum length of the Value for the XmlHttpPanel. The default is 2097152\u00a0characters, which is equivalent to 4\u00a0MB of Unicode string data.")]
		public int MaxJsonLength
		{
			get
			{
				return (int)(this.ViewState["MaxJsonLength"] ?? 2097152);
			}
			set
			{
				this.ViewState["MaxJsonLength"] = value;
			}
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x0011FA6C File Offset: 0x0011DC6C
		string ICallbackEventHandler.GetCallbackResult()
		{
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			ControlRenderer.EnsureChildControlsAreNotRegistered(this);
			this.RenderContents(htmlTextWriter);
			htmlTextWriter.Flush();
			return stringWriter.ToString();
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x0011FAA0 File Offset: 0x0011DCA0
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer
			{
				MaxJsonLength = this.MaxJsonLength
			};
			Dictionary<string, object> dictionary = (Dictionary<string, object>)javaScriptSerializer.DeserializeObject(eventArgument);
			this.Value = (string)dictionary["Value"];
			this.OnServiceRequest();
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0011FAEA File Offset: 0x0011DCEA
		protected virtual void OnServiceRequest()
		{
			this.RaiseEvent(RadXmlHttpPanel.webServiceRequestEvent, new RadXmlHttpPanelEventArgs(this.Value));
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0011FB04 File Offset: 0x0011DD04
		private void RaiseEvent(object eventKey, RadXmlHttpPanelEventArgs e)
		{
			XmlHttpPanelEventHandler xmlHttpPanelEventHandler = (XmlHttpPanelEventHandler)base.Events[eventKey];
			if (xmlHttpPanelEventHandler != null)
			{
				xmlHttpPanelEventHandler(this, e);
			}
		}

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06005E32 RID: 24114 RVA: 0x0011FB2E File Offset: 0x0011DD2E
		// (remove) Token: 0x06005E33 RID: 24115 RVA: 0x0011FB41 File Offset: 0x0011DD41
		public event XmlHttpPanelEventHandler ServiceRequest
		{
			add
			{
				base.Events.AddHandler(RadXmlHttpPanel.webServiceRequestEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadXmlHttpPanel.webServiceRequestEvent, value);
			}
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x0011FB54 File Offset: 0x0011DD54
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "enableClientScriptEvaluation", this.EnableClientScriptEvaluation, false);
			base.DescribeProperty<string>(descriptor, "value", this.Value, "");
			base.DescribeProperty<XmlHttpPanelWcfRequestMethod>(descriptor, "wcfRequestMethod", this.WcfRequestMethod, XmlHttpPanelWcfRequestMethod.GET);
			base.DescribeProperty<string>(descriptor, "wcfServiceMethod", this.WcfServiceMethod, "");
			base.DescribeProperty<string>(descriptor, "wcfServicePath", base.ResolveClientUrl(this.WcfServicePath), "");
			base.DescribeProperty<string>(descriptor, "webMethodName", this.WebMethodName, "");
			base.DescribeProperty<string>(descriptor, "webMethodPath", base.ResolveClientUrl(this.WebMethodPath), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x0011FC0D File Offset: 0x0011DE0D
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "responseEnded", this.OnClientResponseEnded);
			RadWebControl.DescribeEvent(descriptor, "responseEnding", this.OnClientResponseEnding);
			RadWebControl.DescribeEvent(descriptor, "responseError", this.OnClientResponseError);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040016AD RID: 5805
		private HtmlTextWriterTag _tagKey = HtmlTextWriterTag.Span;

		// Token: 0x040016AE RID: 5806
		private static readonly object webServiceRequestEvent = new object();
	}
}
