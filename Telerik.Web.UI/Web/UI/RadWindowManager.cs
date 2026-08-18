using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02001365 RID: 4965
	[LightweightRendering]
	[TelerikToolboxCategory("Container")]
	[ParseChildren(true)]
	[ToolboxBitmap(typeof(RadWindowManager), "Telerik.Web.UI.Window.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadWindowManager", "Telerik.Web.UI.Window.RadWindowManager.js")]
	[Designer("Telerik.Web.Design.RadWindowManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadWindowManager : RadWindowBase
	{
		// Token: 0x170042B2 RID: 17074
		// (get) Token: 0x0600CF6D RID: 53101 RVA: 0x002E03EC File Offset: 0x002DE5EC
		[Description("Gets the collection of RadWindow objects the RadWindowManager has.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public WindowCollection Windows
		{
			get
			{
				if (this._targetControls == null)
				{
					this._targetControls = new WindowCollection(this);
				}
				return this._targetControls;
			}
		}

		// Token: 0x170042B3 RID: 17075
		// (get) Token: 0x0600CF6E RID: 53102 RVA: 0x002E0408 File Offset: 0x002DE608
		// (set) Token: 0x0600CF6F RID: 53103 RVA: 0x002E0416 File Offset: 0x002DE616
		[DefaultValue(false)]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the RadWindow  objects' state (size, location, behavior) will be persisted in a client cookie to restore state over page postbacks/visits.")]
		[ClientControlProperty]
		public bool PreserveClientState
		{
			get
			{
				return base.GetViewStateValue<bool>("PreserveClientState", false);
			}
			set
			{
				this.ViewState["PreserveClientState"] = value;
			}
		}

		// Token: 0x0600CF70 RID: 53104 RVA: 0x002E0430 File Offset: 0x002DE630
		private void RegisterPredefinedDialogScript(string name, string body)
		{
			string text = Guid.NewGuid().ToString().GetHashCode().ToString("x");
			name += text;
			string script = string.Format("\r\n\t\t\t\t  <script type='text/javascript' id='{2}'>\r\n\t\t\t\t\t function {0}()\r\n\t\t\t\t\t {{\r\n\t\t\t\t\t   {1}\r\n\t\t\t\t\t   Sys.Application.remove_load({0});\r\n\t\t\t\t\t   var scriptBlock = document.getElementById('{2}');\r\n\t\t\t\t\t   if(scriptBlock) \r\n\t\t\t\t\t   {{\r\n\t\t\t\t\t\t   var parent = scriptBlock.parentNode;\r\n\t\t\t\t\t\t   if(parent) parent.removeChild(scriptBlock);\r\n\t\t\t\t\t   }}\r\n\t\t\t\t  }};\r\n\t\t\t\t  Sys.Application.add_load({0});</script>", name, body, text);
			ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), text, script, false);
		}

		// Token: 0x0600CF71 RID: 53105 RVA: 0x002E0493 File Offset: 0x002DE693
		public void RadAlert(string text, int? width, int? height, string title, string callBackFnName)
		{
			this.RadAlert(text, width, height, title, callBackFnName, null);
		}

		// Token: 0x0600CF72 RID: 53106 RVA: 0x002E04A4 File Offset: 0x002DE6A4
		public void RadAlert(string text, int? width, int? height, string title, string callBackFnName, string imgUrl)
		{
			string body = string.Format("$find('{0}').radalert('{1}', {2}, {3}, '{4}', {5}, '{6}');", new object[]
			{
				this.ClientID,
				text,
				(width == null) ? "null" : width.ToString(),
				(height == null) ? "null" : height.ToString(),
				title,
				string.IsNullOrEmpty(callBackFnName) ? "null" : callBackFnName,
				(imgUrl == null) ? "null" : imgUrl
			});
			string name = this.ClientID + "_radalert_";
			this.RegisterPredefinedDialogScript(name, body);
		}

		// Token: 0x0600CF73 RID: 53107 RVA: 0x002E0554 File Offset: 0x002DE754
		public void RadConfirm(string text, string callBackFnName, int? width, int? height, object callerObject, string title)
		{
			this.RadConfirm(text, callBackFnName, width, height, callerObject, title, null);
		}

		// Token: 0x0600CF74 RID: 53108 RVA: 0x002E0568 File Offset: 0x002DE768
		public void RadConfirm(string text, string callBackFnName, int? width, int? height, object callerObject, string title, string imgUrl)
		{
			string text2 = string.Format("$find('{0}').radconfirm('{1}', {2}, {3}, {4}, {5}, '{6}', '{7}');", new object[]
			{
				this.ClientID,
				text,
				string.IsNullOrEmpty(callBackFnName) ? "null" : callBackFnName,
				(width == null) ? "null" : width.ToString(),
				(height == null) ? "null" : height.ToString(),
				(callerObject == null) ? "null" : callerObject,
				title,
				(imgUrl == null) ? "null" : imgUrl
			});
			string name = this.ClientID + "_radconfirm_";
			this.RegisterPredefinedDialogScript(name, text2.ToString());
		}

		// Token: 0x0600CF75 RID: 53109 RVA: 0x002E062C File Offset: 0x002DE82C
		public void RadPrompt(string text, string callBackFnName, int? width, int? height, object callerObject, string title, string initialValue)
		{
			string body = string.Format("$find('{0}').radprompt('{1}', {2}, {3}, {4}, {5}, '{6}', '{7}');", new object[]
			{
				this.ClientID,
				text,
				string.IsNullOrEmpty(callBackFnName) ? "null" : callBackFnName,
				(width == null) ? "null" : width.ToString(),
				(height == null) ? "null" : height.ToString(),
				(callerObject == null) ? "null" : callerObject,
				title,
				initialValue
			});
			string name = this.ClientID + "_radprompt_";
			this.RegisterPredefinedDialogScript(name, body);
		}

		// Token: 0x170042B4 RID: 17076
		// (get) Token: 0x0600CF76 RID: 53110 RVA: 0x002E06DF File Offset: 0x002DE8DF
		// (set) Token: 0x0600CF77 RID: 53111 RVA: 0x002E06E7 File Offset: 0x002DE8E7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[Description("The alert template of RadWindowManager.")]
		public ITemplate AlertTemplate
		{
			get
			{
				return this._AlertTemplate;
			}
			set
			{
				this._AlertTemplate = value;
			}
		}

		// Token: 0x170042B5 RID: 17077
		// (get) Token: 0x0600CF78 RID: 53112 RVA: 0x002E06F0 File Offset: 0x002DE8F0
		// (set) Token: 0x0600CF79 RID: 53113 RVA: 0x002E06F8 File Offset: 0x002DE8F8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Description("The confirm template of RadWindowManager.")]
		[NotifyParentProperty(true)]
		public ITemplate ConfirmTemplate
		{
			get
			{
				return this._ConfirmTemplate;
			}
			set
			{
				this._ConfirmTemplate = value;
			}
		}

		// Token: 0x170042B6 RID: 17078
		// (get) Token: 0x0600CF7A RID: 53114 RVA: 0x002E0701 File Offset: 0x002DE901
		// (set) Token: 0x0600CF7B RID: 53115 RVA: 0x002E0709 File Offset: 0x002DE909
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The prompt template of RadWindowManager.")]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public ITemplate PromptTemplate
		{
			get
			{
				return this._PromptTemplate;
			}
			set
			{
				this._PromptTemplate = value;
			}
		}

		// Token: 0x0600CF7C RID: 53116 RVA: 0x002E0714 File Offset: 0x002DE914
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = this.Windows.Count;
			foreach (object obj in this.Windows)
			{
				RadWindow radWindow = (RadWindow)obj;
				stringBuilder.AppendFormat("'{0}'", radWindow.ClientID);
				if (++num < count)
				{
					stringBuilder.Append(",");
				}
			}
			if (count > 0)
			{
				descriptor.AddComponentProperty("child", this.Windows[0].ClientID);
			}
			descriptor.AddScriptProperty("windowControls", "\"[" + stringBuilder.ToString() + "]\"");
		}

		// Token: 0x0600CF7D RID: 53117 RVA: 0x002E07F0 File Offset: 0x002DE9F0
		public void ConfigureWindow(RadWindow wnd)
		{
			PropertyInfo[] properties = wnd.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				object rawViewStateValue = base.GetRawViewStateValue(name);
				if (rawViewStateValue != null && wnd.GetRawViewStateValue(name) == null)
				{
					wnd.SetRawViewStateValue(name, rawViewStateValue);
				}
			}
			if (wnd.Localization.isDefault() && !base.Localization.isDefault())
			{
				wnd.Localization = base.Localization;
			}
			foreach (object obj in base.Shortcuts)
			{
				WindowShortcut windowShortcut = (WindowShortcut)obj;
				if (!wnd.Shortcuts.commandShortcutExists(windowShortcut.CommandName))
				{
					wnd.Shortcuts.Add(windowShortcut);
				}
			}
		}

		// Token: 0x0600CF7E RID: 53118 RVA: 0x002E08E0 File Offset: 0x002DEAE0
		private ITemplate GetTemplateByName(string templateName)
		{
			ITemplate result = null;
			string a;
			if ((a = templateName.ToLower()) != null)
			{
				if (!(a == "alerttemplate"))
				{
					if (!(a == "confirmtemplate"))
					{
						if (a == "prompttemplate")
						{
							result = this.PromptTemplate;
						}
					}
					else
					{
						result = this.ConfirmTemplate;
					}
				}
				else
				{
					result = this.AlertTemplate;
				}
			}
			return result;
		}

		// Token: 0x0600CF7F RID: 53119 RVA: 0x002E093C File Offset: 0x002DEB3C
		private static HtmlTextWriter CreateRenderContext(StringBuilder tempStream)
		{
			StringWriter writer = new StringWriter(tempStream);
			return new HtmlTextWriter(writer);
		}

		// Token: 0x0600CF80 RID: 53120 RVA: 0x002E0958 File Offset: 0x002DEB58
		private string GetTemplateContent(string templateName)
		{
			ITemplate templateByName = this.GetTemplateByName(templateName);
			if (templateByName != null)
			{
				Control control = new RepeaterItem(0, ListItemType.Header);
				templateByName.InstantiateIn(control);
				StringBuilder stringBuilder = new StringBuilder(string.Empty);
				control.RenderControl(RadWindowManager.CreateRenderContext(stringBuilder));
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600CF81 RID: 53121 RVA: 0x002E09A4 File Offset: 0x002DEBA4
		protected override void CreateChildControls()
		{
			if (this.Windows.Count == 0)
			{
				return;
			}
			this.Controls.Clear();
			foreach (object obj in this.Windows)
			{
				RadWindow radWindow = (RadWindow)obj;
				this.ConfigureWindow(radWindow);
				this.Controls.Add(radWindow);
			}
			base.CreateChildControls();
		}

		// Token: 0x0600CF82 RID: 53122 RVA: 0x002E0A28 File Offset: 0x002DEC28
		protected override void ControlPreRender()
		{
			base.EnsureID();
			XmlTextReader reader = new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(this.GetTemplatesPath()));
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			foreach (object obj in xmlDocument.DocumentElement)
			{
				XmlNode xmlNode = (XmlNode)obj;
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
				htmlGenericControl.ClientIDMode = this.ClientIDMode;
				htmlGenericControl.ID = this.ID + "_" + xmlNode.Name;
				htmlGenericControl.Attributes.CssStyle["display"] = "none";
				string templateContent = this.GetTemplateContent(xmlNode.Name);
				htmlGenericControl.InnerHtml = (string.IsNullOrEmpty(templateContent) ? xmlNode.InnerText : templateContent);
				this.Controls.Add(htmlGenericControl);
			}
			base.ControlPreRender();
		}

		// Token: 0x0600CF83 RID: 53123 RVA: 0x002E0B38 File Offset: 0x002DED38
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Windows).LoadViewState(array[1]);
			this.EnsureChildControls();
		}

		// Token: 0x0600CF84 RID: 53124 RVA: 0x002E0B6C File Offset: 0x002DED6C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Windows).SaveViewState()
			};
		}

		// Token: 0x0600CF85 RID: 53125 RVA: 0x002E0B9A File Offset: 0x002DED9A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Windows).TrackViewState();
		}

		// Token: 0x0600CF86 RID: 53126 RVA: 0x002E0BB0 File Offset: 0x002DEDB0
		private string GetTemplatesPath()
		{
			switch (this.ResolvedRenderMode)
			{
			case RenderMode.Classic:
				return RadWindowManager.classicTemplatesPath;
			case RenderMode.Lightweight:
				return RadWindowManager.liteTemplatesPath;
			}
			return RadWindowManager.classicTemplatesPath;
		}

		// Token: 0x0600CF87 RID: 53127 RVA: 0x002E0BEB File Offset: 0x002DEDEB
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "preserveClientState", this.PreserveClientState, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600CF88 RID: 53128 RVA: 0x002E0C07 File Offset: 0x002DEE07
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04003788 RID: 14216
		private WindowCollection _targetControls;

		// Token: 0x04003789 RID: 14217
		private ITemplate _AlertTemplate;

		// Token: 0x0400378A RID: 14218
		private ITemplate _ConfirmTemplate;

		// Token: 0x0400378B RID: 14219
		private ITemplate _PromptTemplate;

		// Token: 0x0400378C RID: 14220
		private static string classicTemplatesPath = "Telerik.Web.UI.Window.CoreTemplates.xml";

		// Token: 0x0400378D RID: 14221
		private static string liteTemplatesPath = "Telerik.Web.UI.Window.LiteTemplates.xml";
	}
}
