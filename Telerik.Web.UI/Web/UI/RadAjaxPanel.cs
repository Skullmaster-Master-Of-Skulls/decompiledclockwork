using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FE1 RID: 4065
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadAjaxPanelDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ClientScriptResource("Telerik.Web.UI.RadAjaxPanel", "Telerik.Web.UI.Ajax.Ajax.js")]
	[DefaultProperty("")]
	[DefaultEvent("")]
	[ToolboxData("<{0}:RadAjaxPanel runat=\"server\" width=\"300px\" height=\"200px\"></{0}:RadAjaxPanel>")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadAjaxPanel), "Telerik.Web.UI.Ajax.png")]
	[PersistChildren(true)]
	[ParseChildren(false)]
	public class RadAjaxPanel : RadAjaxControl, IScriptControl
	{
		// Token: 0x06009E1A RID: 40474 RVA: 0x00233E9E File Offset: 0x0023209E
		public RadAjaxPanel()
		{
			base.EnsureLicensing();
		}

		// Token: 0x170031FF RID: 12799
		// (get) Token: 0x06009E1B RID: 40475 RVA: 0x00233EB3 File Offset: 0x002320B3
		// (set) Token: 0x06009E1C RID: 40476 RVA: 0x00233EE2 File Offset: 0x002320E2
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.AjaxLoadingPanelIDConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Description("Gets or sets the ID of the RadAjaxLoadingPanel control that will be displayed over the control during AJAX requests.")]
		[Category("Client")]
		public string LoadingPanelID
		{
			get
			{
				if (this.ViewState["_lp"] != null)
				{
					return (string)this.ViewState["_lp"];
				}
				return "";
			}
			set
			{
				this.ViewState["_lp"] = value;
			}
		}

		// Token: 0x17003200 RID: 12800
		// (get) Token: 0x06009E1D RID: 40477 RVA: 0x00233EF5 File Offset: 0x002320F5
		// (set) Token: 0x06009E1E RID: 40478 RVA: 0x00233F20 File Offset: 0x00232120
		[Description("Gets or sets the render mode of the the RadAjaxPanel. The default value is Block.")]
		[DefaultValue(UpdatePanelRenderMode.Block)]
		[Category("Layout")]
		public UpdatePanelRenderMode RenderMode
		{
			get
			{
				if (this.ViewState["_rm"] != null)
				{
					return (UpdatePanelRenderMode)this.ViewState["_rm"];
				}
				return UpdatePanelRenderMode.Block;
			}
			set
			{
				this.ViewState["_rm"] = value;
			}
		}

		// Token: 0x17003201 RID: 12801
		// (get) Token: 0x06009E1F RID: 40479 RVA: 0x00233F38 File Offset: 0x00232138
		// (set) Token: 0x06009E20 RID: 40480 RVA: 0x00233F61 File Offset: 0x00232161
		[Description("AJAXPanel Wrap")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Wrap
		{
			get
			{
				object obj = this.ViewState["_wp"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["_wp"] = value;
			}
		}

		// Token: 0x17003202 RID: 12802
		// (get) Token: 0x06009E21 RID: 40481 RVA: 0x00233F7C File Offset: 0x0023217C
		// (set) Token: 0x06009E22 RID: 40482 RVA: 0x00233FA5 File Offset: 0x002321A5
		[Description("AJAXPanel HorizontalAlign")]
		[DefaultValue(0)]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				object obj = this.ViewState["_ha"];
				if (obj != null)
				{
					return (HorizontalAlign)obj;
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				this.ViewState["_ha"] = value;
			}
		}

		// Token: 0x17003203 RID: 12803
		// (get) Token: 0x06009E23 RID: 40483 RVA: 0x00233FC0 File Offset: 0x002321C0
		// (set) Token: 0x06009E24 RID: 40484 RVA: 0x00233FED File Offset: 0x002321ED
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Description("Set class attribute to UpdatePanel that will wrap the UpdatedControl")]
		public virtual string UpdatePanelCssClass
		{
			get
			{
				object obj = this.ViewState["_upcc"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["_upcc"] = value;
			}
		}

		// Token: 0x17003204 RID: 12804
		// (get) Token: 0x06009E25 RID: 40485 RVA: 0x00234000 File Offset: 0x00232200
		// (set) Token: 0x06009E26 RID: 40486 RVA: 0x0023402D File Offset: 0x0023222D
		[NotifyParentProperty(true)]
		[Description("AJAXPanel BackImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string BackImageUrl
		{
			get
			{
				string text = (string)this.ViewState["_bi"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["_bi"] = value;
			}
		}

		// Token: 0x06009E27 RID: 40487 RVA: 0x00234040 File Offset: 0x00232240
		internal AjaxSetting GetAjaxSetting()
		{
			if (this.EnableAJAX && this.Visible)
			{
				AjaxSetting ajaxSetting = new AjaxSetting(this.ClientID);
				AjaxUpdatedControl ajaxUpdatedControl = new AjaxUpdatedControl(this.ClientID, this.LoadingPanelID);
				ajaxUpdatedControl.UpdatePanelRenderMode = this.RenderMode;
				ajaxUpdatedControl.UpdatePanelHeight = this.Height;
				ajaxUpdatedControl.UpdatePanelCssClass = this.UpdatePanelCssClass;
				ajaxSetting.UpdatedControls.Add(ajaxUpdatedControl);
				return ajaxSetting;
			}
			return null;
		}

		// Token: 0x17003205 RID: 12805
		// (get) Token: 0x06009E28 RID: 40488 RVA: 0x002340B0 File Offset: 0x002322B0
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (!base.DesignMode && this.Page != null)
				{
					Control control = this.Page.FindControl(string.Format("{0}Panel", this.UniqueID));
					OurUpdatePanel ourUpdatePanel = control as OurUpdatePanel;
					if (ourUpdatePanel != null && (ourUpdatePanel.RenderMode == UpdatePanelRenderMode.Inline || this.RenderMode == UpdatePanelRenderMode.Inline))
					{
						return HtmlTextWriterTag.Span;
					}
				}
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06009E29 RID: 40489 RVA: 0x0023410A File Offset: 0x0023230A
		protected override void RenderContents(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			base.RenderContents(writer);
		}

		// Token: 0x06009E2A RID: 40490 RVA: 0x0023411C File Offset: 0x0023231C
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.isRenderExecuted)
			{
				this.isRenderExecuted = true;
				base.Render(writer);
				if (!this.EnableAJAX || ScriptManager.GetCurrent(this.Page) == null || !ScriptManager.GetCurrent(this.Page).EnablePartialRendering)
				{
					return;
				}
				if (!base.DesignMode)
				{
					ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
				}
			}
		}

		// Token: 0x06009E2B RID: 40491 RVA: 0x00234180 File Offset: 0x00232380
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (control is OurUpdatePanel)
				{
					if (!this.isUpdatePanelRenderExecuted)
					{
						this.isUpdatePanelRenderExecuted = false;
						control.RenderControl(writer);
					}
				}
				else
				{
					control.RenderControl(writer);
				}
			}
		}

		// Token: 0x06009E2C RID: 40492 RVA: 0x002341FC File Offset: 0x002323FC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.EnableAJAX || ScriptManager.GetCurrent(this.Page) == null || !ScriptManager.GetCurrent(this.Page).EnablePartialRendering)
			{
				return;
			}
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<RadAjaxPanel>(this);
			base.EnsureID();
		}

		// Token: 0x06009E2D RID: 40493 RVA: 0x00234250 File Offset: 0x00232450
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (!this.EnableAJAX || !this.Visible)
			{
				return null;
			}
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("Telerik.Web.UI.RadAjaxPanel", this.ClientID);
			scriptControlDescriptor.AddScriptProperty("clientEvents", this.ClientEvents.ClientObjectString);
			scriptControlDescriptor.AddProperty("uniqueID", this.UniqueID);
			Control control = null;
			if (!string.IsNullOrEmpty(this.LoadingPanelID))
			{
				control = base.FindControlRecursive(this.LoadingPanelID);
			}
			string value = (control != null) ? control.ClientID : this.LoadingPanelID;
			scriptControlDescriptor.AddProperty("loadingPanelID", value);
			scriptControlDescriptor.AddProperty("links", this._linksToAppend);
			scriptControlDescriptor.AddProperty("styles", this._stylesToAppend);
			scriptControlDescriptor.AddProperty("enableHistory", this.EnableHistory);
			scriptControlDescriptor.AddProperty("enableAJAX", this.EnableAJAX);
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

		// Token: 0x06009E2E RID: 40494 RVA: 0x002343C4 File Offset: 0x002325C4
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.EnableAJAX || !this.Visible || !this.EnableEmbeddedScripts)
			{
				return null;
			}
			return ScriptRegistrar.GetScriptReferences(this);
		}

		// Token: 0x17003206 RID: 12806
		// (get) Token: 0x06009E2F RID: 40495 RVA: 0x002343E6 File Offset: 0x002325E6
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public override AjaxClientEvents ClientEvents
		{
			get
			{
				return base.ClientEvents;
			}
		}

		// Token: 0x04002C73 RID: 11379
		private bool isRenderExecuted;

		// Token: 0x04002C74 RID: 11380
		private bool isUpdatePanelRenderExecuted = true;
	}
}
