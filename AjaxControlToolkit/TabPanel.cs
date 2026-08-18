using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit
{
	// Token: 0x02000195 RID: 405
	[ClientScriptResource("Sys.Extended.UI.TabPanel", "Tabs")]
	[RequiredScript(typeof(TabContainer))]
	[ClientCssResource("Tabs")]
	[ToolboxItem(false)]
	[Designer(typeof(TabPanelDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(DynamicPopulateExtender))]
	public class TabPanel : ScriptControlBase
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x0001E371 File Offset: 0x0001C571
		public TabPanel() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0001E37C File Offset: 0x0001C57C
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0001E39C File Offset: 0x0001C59C
		[DefaultValue("")]
		[ClientPropertyName("headerText")]
		[Category("Appearance")]
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0001E3AF File Offset: 0x0001C5AF
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x0001E3B7 File Offset: 0x0001C5B7
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateInstance(TemplateInstance.Single)]
		[Browsable(false)]
		public ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[Browsable(false)]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001E3D1 File Offset: 0x0001C5D1
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0001E3D9 File Offset: 0x0001C5D9
		[DefaultValue(true)]
		[ClientPropertyName("enabled")]
		[Category("Behavior")]
		[ExtenderControlProperty]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001E3E2 File Offset: 0x0001C5E2
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0001E403 File Offset: 0x0001C603
		[ExtenderControlProperty]
		[ClientPropertyName("scrollBars")]
		[DefaultValue(ScrollBars.None)]
		[Category("Behavior")]
		public ScrollBars ScrollBars
		{
			get
			{
				return (ScrollBars)(this.ViewState["ScrollBars"] ?? ScrollBars.None);
			}
			set
			{
				this.ViewState["ScrollBars"] = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001E41B File Offset: 0x0001C61B
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0001E43B File Offset: 0x0001C63B
		[ClientPropertyName("click")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		public string OnClientClick
		{
			get
			{
				return (string)(this.ViewState["OnClientClick"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientClick"] = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0001E44E File Offset: 0x0001C64E
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0001E46E File Offset: 0x0001C66E
		[Category("Behavior")]
		[UrlProperty]
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("dynamicServicePath")]
		public string DynamicServicePath
		{
			get
			{
				return (string)(this.ViewState["DynamicServicePath"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DynamicServicePath"] = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0001E481 File Offset: 0x0001C681
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0001E4A1 File Offset: 0x0001C6A1
		[DefaultValue("")]
		[ClientPropertyName("dynamicServiceMethod")]
		[Category("Behavior")]
		[ExtenderControlProperty]
		public string DynamicServiceMethod
		{
			get
			{
				return (string)(this.ViewState["DynamicServiceMethod"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DynamicServiceMethod"] = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		[DefaultValue("")]
		[Category("Behavior")]
		[ExtenderControlProperty]
		[ClientPropertyName("dynamicContextKey")]
		public string DynamicContextKey
		{
			get
			{
				return (string)(this.ViewState["DynamicContextKey"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DynamicContextKey"] = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0001E4E7 File Offset: 0x0001C6E7
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0001E508 File Offset: 0x0001C708
		[Category("Behavior")]
		[ClientPropertyName("onDemandMode")]
		[ExtenderControlProperty]
		[DefaultValue(OnDemandMode.Always)]
		public OnDemandMode OnDemandMode
		{
			get
			{
				return (OnDemandMode)(this.ViewState["OnDemandMode"] ?? OnDemandMode.Always);
			}
			set
			{
				this.ViewState["OnDemandMode"] = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0001E520 File Offset: 0x0001C720
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0001E540 File Offset: 0x0001C740
		[ExtenderControlEvent]
		[ClientPropertyName("populating")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string OnClientPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPopulating"] = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0001E553 File Offset: 0x0001C753
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0001E573 File Offset: 0x0001C773
		[DefaultValue("")]
		[ExtenderControlEvent]
		[Category("Behavior")]
		[ClientPropertyName("populated")]
		public string OnClientPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPopulated"] = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0001E586 File Offset: 0x0001C786
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0001E58E File Offset: 0x0001C78E
		internal bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0001E597 File Offset: 0x0001C797
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0001E59F File Offset: 0x0001C79F
		[ClientPropertyName("updatePanelID")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ExtenderControlProperty]
		public string UpdatePanelID { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		[ClientPropertyName("wasLoadedOnce")]
		[ExtenderControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool WasLoadedOnce { get; set; }

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001E5BC File Offset: 0x0001C7BC
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this._headerTemplate != null)
			{
				this._headerControl = new Control();
				this._headerTemplate.InstantiateIn(this._headerControl);
				this.Controls.Add(this._headerControl);
			}
			if (this._contentTemplate == null)
			{
				return;
			}
			Control control = new Control();
			this._contentTemplate.InstantiateIn(control);
			if (this._owner.OnDemand && this.OnDemandMode != OnDemandMode.None)
			{
				string id = this.ClientID + "_onDemandPanel";
				Panel panel = new Panel
				{
					ID = id,
					Visible = false
				};
				panel.Controls.Add(control);
				UpdatePanel updatePanel = new UpdatePanel
				{
					ID = this.ClientID + "_updatePanel",
					UpdateMode = UpdatePanelUpdateMode.Conditional
				};
				updatePanel.Load += this.UpdatePanelOnLoad;
				updatePanel.ContentTemplateContainer.Controls.Add(panel);
				this.Controls.Add(updatePanel);
				this.UpdatePanelID = updatePanel.ClientID;
				return;
			}
			this.Controls.Add(control);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		private void UpdatePanelOnLoad(object sender, EventArgs e)
		{
			if (!(sender is UpdatePanel))
			{
				return;
			}
			string id = (sender as UpdatePanel).ID;
			string str = id.Substring(0, id.Length - 12);
			if (!this.Active)
			{
				return;
			}
			Control control = this.FindControl(str + "_onDemandPanel");
			if (control != null && control is Panel)
			{
				control.Visible = true;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E744 File Offset: 0x0001C944
		protected internal virtual void RenderHeader(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_tab");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab");
			this.RenderBeginTag(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_outer");
			this.RenderBeginTag(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_inner");
			this.RenderBeginTag(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_tab");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, "__tab_" + this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
			if (this._owner.UseVerticalStripPlacement)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			this.RenderBeginTag(writer);
			if (this._headerControl != null)
			{
				this._headerControl.Visible = true;
				this._headerControl.RenderControl(writer);
				this._headerControl.Visible = false;
			}
			else
			{
				writer.Write(this.HeaderText);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001E864 File Offset: 0x0001CA64
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (this._owner.UseVerticalStripPlacement)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001E888 File Offset: 0x0001CA88
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._headerControl != null)
			{
				this._headerControl.Visible = false;
			}
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_panel");
			if (!this.Active || !this.Enabled)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderChildren(writer);
			writer.RenderEndTag();
			this.RegisterScriptDescriptors();
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001E902 File Offset: 0x0001CB02
		protected virtual void RegisterScriptDescriptors()
		{
			base.ScriptManager.RegisterScriptDescriptors(this);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001E910 File Offset: 0x0001CB10
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddElementProperty("headerTab", "__tab_" + this.ClientID);
			if (this._owner == null)
			{
				return;
			}
			descriptor.AddComponentProperty("owner", this._owner.ClientID);
			descriptor.AddProperty("ownerID", this._owner.ClientID);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001E974 File Offset: 0x0001CB74
		internal void SetOwner(TabContainer owner)
		{
			this._owner = owner;
		}

		// Token: 0x04000445 RID: 1093
		private bool _active;

		// Token: 0x04000446 RID: 1094
		private ITemplate _contentTemplate;

		// Token: 0x04000447 RID: 1095
		private ITemplate _headerTemplate;

		// Token: 0x04000448 RID: 1096
		private TabContainer _owner;

		// Token: 0x04000449 RID: 1097
		private Control _headerControl;
	}
}
