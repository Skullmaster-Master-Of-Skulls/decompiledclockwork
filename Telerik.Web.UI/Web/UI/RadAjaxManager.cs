using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FDD RID: 4061
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadAjaxManager), "Telerik.Web.UI.Ajax.png")]
	[ClientScriptResource("Telerik.Web.UI.RadAjaxManager", "Telerik.Web.UI.Ajax.Ajax.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadAjaxManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[DefaultEvent("AjaxRequest")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class RadAjaxManager : RadAjaxControl, IScriptControl
	{
		// Token: 0x06009DF1 RID: 40433 RVA: 0x0023365A File Offset: 0x0023185A
		public RadAjaxManager()
		{
			base.EnsureLicensing();
		}

		// Token: 0x170031ED RID: 12781
		// (get) Token: 0x06009DF2 RID: 40434 RVA: 0x00233674 File Offset: 0x00231874
		// (set) Token: 0x06009DF3 RID: 40435 RVA: 0x0023369D File Offset: 0x0023189D
		[Category("Client")]
		[DefaultValue(typeof(UpdatePanelRenderMode), "Block")]
		[Description("Sets or gets the default RenderMode for the UpdatePanels.")]
		[NotifyParentProperty(true)]
		public virtual UpdatePanelRenderMode UpdatePanelsRenderMode
		{
			get
			{
				object obj = base.ViewState["_uprm"];
				if (obj != null)
				{
					return (UpdatePanelRenderMode)obj;
				}
				return UpdatePanelRenderMode.Block;
			}
			set
			{
				if (value < UpdatePanelRenderMode.Block || value > UpdatePanelRenderMode.Inline)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["_uprm"] = value;
			}
		}

		// Token: 0x170031EE RID: 12782
		// (get) Token: 0x06009DF4 RID: 40436 RVA: 0x002336C8 File Offset: 0x002318C8
		// (set) Token: 0x06009DF5 RID: 40437 RVA: 0x002336F1 File Offset: 0x002318F1
		[NotifyParentProperty(true)]
		[Description("Gets or sets if only the ajax initiator UpdatedControls UpdatePanel parents will be updated.")]
		[Category("Client")]
		[DefaultValue(false)]
		public virtual bool UpdateInitiatorPanelsOnly
		{
			get
			{
				object obj = base.ViewState["UpdateInitiatorPanelsOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["UpdateInitiatorPanelsOnly"] = value;
			}
		}

		// Token: 0x170031EF RID: 12783
		// (get) Token: 0x06009DF6 RID: 40438 RVA: 0x0023370C File Offset: 0x0023190C
		// (set) Token: 0x06009DF7 RID: 40439 RVA: 0x00233739 File Offset: 0x00231939
		[Category("Client")]
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.AjaxLoadingPanelIDConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Description("Sets or gets the default loading panel for every ajax setting")]
		public string DefaultLoadingPanelID
		{
			get
			{
				object obj = this.ViewState["_dlp"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["_dlp"] = value;
			}
		}

		// Token: 0x06009DF8 RID: 40440 RVA: 0x0023374C File Offset: 0x0023194C
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.EnableAJAX || ScriptManager.GetCurrent(this.Page) == null || !ScriptManager.GetCurrent(this.Page).EnablePartialRendering)
			{
				return;
			}
			if (this.selfUpdatePanel != null)
			{
				BaseClass.RenderVersionStamp(writer);
				this.selfUpdatePanel.RenderControl(writer);
			}
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06009DF9 RID: 40441 RVA: 0x002337B4 File Offset: 0x002319B4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.EnableAJAX || ScriptManager.GetCurrent(this.Page) == null || !ScriptManager.GetCurrent(this.Page).EnablePartialRendering)
			{
				return;
			}
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<RadAjaxManager>(this);
		}

		// Token: 0x06009DFA RID: 40442 RVA: 0x00233801 File Offset: 0x00231A01
		public static RadAjaxManager GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(RadAjaxManager)] as RadAjaxManager;
		}

		// Token: 0x06009DFB RID: 40443 RVA: 0x0023382B File Offset: 0x00231A2B
		protected override void RenderChildren(HtmlTextWriter writer)
		{
		}

		// Token: 0x170031F0 RID: 12784
		// (get) Token: 0x06009DFC RID: 40444 RVA: 0x0023382D File Offset: 0x00231A2D
		[Description("Control Configuration")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("Telerik.Web.Design.AjaxSettingsTypeEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Browsable(false)]
		[Category("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AjaxSettingsCollection AjaxSettings
		{
			get
			{
				return this.ajaxSettings;
			}
		}

		// Token: 0x06009DFD RID: 40445 RVA: 0x00233838 File Offset: 0x00231A38
		protected override void CreateChildControls()
		{
			base.EnsureID();
			base.CreateChildControls();
			if (!this.EnableAJAX || !this.Visible)
			{
				return;
			}
			this.selfUpdatePanel = new UpdatePanel();
			this.selfUpdatePanel.ID = this.ID + "SU";
			this.selfUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
			this.selfUpdatePanel.EnableViewState = false;
			LiteralControl child = new LiteralControl(string.Format("<span id=\"{0}\" style=\"display:none;\"></span>", this.ClientID));
			this.selfUpdatePanel.ContentTemplateContainer.Controls.Add(child);
			ProxyScriptControl child2 = new ProxyScriptControl(this);
			this.selfUpdatePanel.ContentTemplateContainer.Controls.Add(child2);
			this.Controls.Add(this.selfUpdatePanel);
		}

		// Token: 0x06009DFE RID: 40446 RVA: 0x002338FA File Offset: 0x00231AFA
		internal void AddedSetting(Control ajaxifiedControl, Control updatedControl)
		{
			if (this.tooLateForAjaxification)
			{
				throw new InvalidOperationException("Controls cannot be ajaxified after Page PreRender.");
			}
			if (this.performImmediateAjaxification)
			{
				base.CreateUpdatePanel(ajaxifiedControl, "", updatedControl);
			}
		}

		// Token: 0x06009DFF RID: 40447 RVA: 0x00233924 File Offset: 0x00231B24
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (!this.EnableAJAX || !this.Visible)
			{
				return null;
			}
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("Telerik.Web.UI.RadAjaxManager", this.ClientID);
			scriptControlDescriptor.AddScriptProperty("ajaxSettings", this.AjaxSettings.SerializeToJavascript(this));
			scriptControlDescriptor.AddScriptProperty("clientEvents", this.ClientEvents.ClientObjectString);
			scriptControlDescriptor.AddProperty("uniqueID", this.UniqueID);
			Control control = null;
			if (!string.IsNullOrEmpty(this.DefaultLoadingPanelID))
			{
				control = base.FindControlRecursive(this.DefaultLoadingPanelID);
			}
			string value = this.DefaultLoadingPanelID;
			if (control != null)
			{
				value = control.ClientID;
			}
			scriptControlDescriptor.AddProperty("defaultLoadingPanelID", value);
			scriptControlDescriptor.AddProperty("links", this._linksToAppend);
			scriptControlDescriptor.AddProperty("styles", this._stylesToAppend);
			scriptControlDescriptor.AddProperty("enableHistory", this.EnableHistory);
			scriptControlDescriptor.AddProperty("enableAJAX", this.EnableAJAX);
			scriptControlDescriptor.AddProperty("updatePanelsRenderMode", this.UpdatePanelsRenderMode);
			scriptControlDescriptor.AddProperty("_updatePanels", string.Join(",", this.plainPanelsClientIDs.ToArray()));
			if (base.RequestQueueSize > 0)
			{
				scriptControlDescriptor.AddProperty("requestQueueSize", base.RequestQueueSize);
			}
			if (base.EnableAriaSupport)
			{
				scriptControlDescriptor.AddProperty("_enableAriaSupport", base.EnableAriaSupport);
			}
			if (this.PostBackControls != null && this.PostBackControls.Length > 0)
			{
				scriptControlDescriptor.AddProperty("_postBackControls", string.Join(",", this.PostBackControls));
			}
			if (this.ShowLoadingPanelForPostBackControls)
			{
				scriptControlDescriptor.AddProperty("_showLoadingPanelForPostBackControls", this.ShowLoadingPanelForPostBackControls);
			}
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06009E00 RID: 40448 RVA: 0x00233AE4 File Offset: 0x00231CE4
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.EnableAJAX || !this.Visible || !this.EnableEmbeddedScripts)
			{
				return null;
			}
			return ScriptRegistrar.GetScriptReferences(this);
		}

		// Token: 0x06009E01 RID: 40449 RVA: 0x00233B08 File Offset: 0x00231D08
		internal string ResolveClientID(string ID)
		{
			string result = "";
			if (!string.IsNullOrEmpty(ID))
			{
				Control control = base.FindControlRecursive(ID);
				if (control != null)
				{
					result = control.ClientID;
				}
			}
			return result;
		}

		// Token: 0x170031F1 RID: 12785
		// (get) Token: 0x06009E02 RID: 40450 RVA: 0x00233B36 File Offset: 0x00231D36
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override short TabIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170031F2 RID: 12786
		// (get) Token: 0x06009E03 RID: 40451 RVA: 0x00233B39 File Offset: 0x00231D39
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170031F3 RID: 12787
		// (get) Token: 0x06009E04 RID: 40452 RVA: 0x00233B3C File Offset: 0x00231D3C
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string AccessKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170031F4 RID: 12788
		// (get) Token: 0x06009E05 RID: 40453 RVA: 0x00233B44 File Offset: 0x00231D44
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x170031F5 RID: 12789
		// (get) Token: 0x06009E06 RID: 40454 RVA: 0x00233B5C File Offset: 0x00231D5C
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BorderColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x170031F6 RID: 12790
		// (get) Token: 0x06009E07 RID: 40455 RVA: 0x00233B72 File Offset: 0x00231D72
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Browsable(false)]
		public override string CssClass
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170031F7 RID: 12791
		// (get) Token: 0x06009E08 RID: 40456 RVA: 0x00233B79 File Offset: 0x00231D79
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return BorderStyle.NotSet;
			}
		}

		// Token: 0x170031F8 RID: 12792
		// (get) Token: 0x06009E09 RID: 40457 RVA: 0x00233B7C File Offset: 0x00231D7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Browsable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return default(Unit);
			}
		}

		// Token: 0x170031F9 RID: 12793
		// (get) Token: 0x06009E0A RID: 40458 RVA: 0x00233B92 File Offset: 0x00231D92
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x170031FA RID: 12794
		// (get) Token: 0x06009E0B RID: 40459 RVA: 0x00233B9C File Offset: 0x00231D9C
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x170031FB RID: 12795
		// (get) Token: 0x06009E0C RID: 40460 RVA: 0x00233BB2 File Offset: 0x00231DB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Browsable(false)]
		public override string ToolTip
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170031FC RID: 12796
		// (get) Token: 0x06009E0D RID: 40461 RVA: 0x00233BBC File Offset: 0x00231DBC
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit Width
		{
			get
			{
				return default(Unit);
			}
		}

		// Token: 0x170031FD RID: 12797
		// (get) Token: 0x06009E0E RID: 40462 RVA: 0x00233BD4 File Offset: 0x00231DD4
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Unit Height
		{
			get
			{
				return default(Unit);
			}
		}

		// Token: 0x06009E0F RID: 40463 RVA: 0x00233BEC File Offset: 0x00231DEC
		internal void RegisterProxy(RadAjaxManagerProxy radAjaxManagerProxy)
		{
			if (!this.proxies.ContainsKey(radAjaxManagerProxy.UniqueID))
			{
				this.proxies.Add(radAjaxManagerProxy.UniqueID, radAjaxManagerProxy);
				foreach (object obj in radAjaxManagerProxy.AjaxSettings)
				{
					AjaxSetting ajaxSetting = (AjaxSetting)obj;
					if (!string.IsNullOrEmpty(ajaxSetting.EventName))
					{
						base.AttachTriggers(ajaxSetting);
					}
				}
			}
		}

		// Token: 0x06009E10 RID: 40464 RVA: 0x00233C78 File Offset: 0x00231E78
		public static string RenderUserControl(string path)
		{
			return RadAjaxManager.RenderUserControl(path, null);
		}

		// Token: 0x06009E11 RID: 40465 RVA: 0x00233C84 File Offset: 0x00231E84
		public static string RenderUserControl(string path, object data)
		{
			HtmlForm htmlForm = new HtmlForm();
			Page page = new Page();
			htmlForm.Page = page;
			HtmlHead child = new HtmlHead();
			page.Controls.Add(child);
			page.Controls.Add(htmlForm);
			UserControl userControl = (UserControl)page.LoadControl(path);
			userControl.ID = path.Split(new char[]
			{
				'.'
			})[0].Replace("/", "").Replace("~", "");
			if (data != null)
			{
				Type type = userControl.GetType();
				FieldInfo field = type.GetField("Data");
				if (!(field != null))
				{
					throw new Exception("View file: " + path + " does not have a public Data property");
				}
				field.SetValue(userControl, data);
			}
			htmlForm.Controls.Add(new ScriptManager());
			htmlForm.Controls.Add(new LiteralControl("FORMSTART"));
			htmlForm.Controls.Add(userControl);
			htmlForm.Controls.Add(new LiteralControl("FORMEND"));
			StringWriter stringWriter = new StringWriter();
			HttpContext.Current.Server.Execute(page, stringWriter, false);
			string str = Regex.Replace(stringWriter.ToString(), "(.|\\n)*?FORMSTART(?<content>(.|\\n)*?)FORMEND(.|\\n)*?", "${content}");
			string str2 = stringWriter.ToString().Substring(stringWriter.ToString().IndexOf("<head>"), stringWriter.ToString().IndexOf("</head>") - stringWriter.ToString().IndexOf("<head>"));
			return str2 + " " + str;
		}

		// Token: 0x04002C6F RID: 11375
		internal UpdatePanel selfUpdatePanel;

		// Token: 0x04002C70 RID: 11376
		internal Dictionary<string, RadAjaxManagerProxy> proxies = new Dictionary<string, RadAjaxManagerProxy>();
	}
}
