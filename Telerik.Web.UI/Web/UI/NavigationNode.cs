using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Nav.Renderers;
using Telerik.Web.UI.Navigation;

namespace Telerik.Web.UI
{
	// Token: 0x02000624 RID: 1572
	[ToolboxItem(false)]
	public class NavigationNode : WebControl, IItem, INamingContainer, INavigationNodeContainer
	{
		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x000BBAE9 File Offset: 0x000B9CE9
		// (set) Token: 0x06003918 RID: 14616 RVA: 0x000BBAF1 File Offset: 0x000B9CF1
		internal bool IsFirst { get; set; }

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x000BBAFA File Offset: 0x000B9CFA
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x000BBB02 File Offset: 0x000B9D02
		internal bool IsLast { get; set; }

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000BBB0B File Offset: 0x000B9D0B
		// (set) Token: 0x0600391C RID: 14620 RVA: 0x000BBB13 File Offset: 0x000B9D13
		internal bool IsRoot { get; set; }

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000BBB1C File Offset: 0x000B9D1C
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x000BBB24 File Offset: 0x000B9D24
		internal RadNavigation Nav { get; set; }

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x000BBB2D File Offset: 0x000B9D2D
		internal string CurrentImageUrl
		{
			get
			{
				if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return this.DisabledImageUrl;
				}
				if (!string.IsNullOrEmpty(this.SelectedImageUrl))
				{
					return this.SelectedImageUrl;
				}
				return this.ImageUrl;
			}
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000BBB68 File Offset: 0x000B9D68
		internal void ApplyTemplate(ITemplate parentTemplate)
		{
			if (parentTemplate == null)
			{
				return;
			}
			int num = this.Controls.Count;
			parentTemplate.InstantiateIn(this);
			while (num > 0 && !this.Controls.IsReadOnly)
			{
				this.Controls.Add(this.Controls[0]);
				num--;
			}
			this.IsTemplateInstantiated = true;
			this.DataBind();
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000BBBC7 File Offset: 0x000B9DC7
		internal ITemplate TemplateToApply
		{
			get
			{
				if (this.NodeTemplate != null)
				{
					return this.NodeTemplate;
				}
				if (this.Owner != null && this.Owner.NodeTemplate != null)
				{
					return this.Owner.NodeTemplate;
				}
				return null;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x000BBBFA File Offset: 0x000B9DFA
		// (set) Token: 0x06003923 RID: 14627 RVA: 0x000BBC02 File Offset: 0x000B9E02
		internal bool IsTemplateInstantiated { get; set; }

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000BBC0B File Offset: 0x000B9E0B
		// (set) Token: 0x06003925 RID: 14629 RVA: 0x000BBC13 File Offset: 0x000B9E13
		internal bool IsContentTemplateInstantiated { get; set; }

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000BBC1C File Offset: 0x000B9E1C
		internal bool ShouldRenderToggleButton
		{
			get
			{
				return this.Nodes.Count > 0 || this.ContentTemplate != null;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000BBC3A File Offset: 0x000B9E3A
		internal virtual IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateNodeRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000BBC56 File Offset: 0x000B9E56
		internal virtual IRenderer CreateNodeRenderer()
		{
			return RendererFactory.CreateNodeRenderer(this);
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000BBC5E File Offset: 0x000B9E5E
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000BBC67 File Offset: 0x000B9E67
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.ContentTemplate != null && this.Nodes.Count > 0)
			{
				throw new NavigationNodeTemplateException("Cannot set ContentTemplate on a NavigationNode, which has child Nodes.");
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000BBC91 File Offset: 0x000B9E91
		void IItem.DataBind()
		{
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000BBC94 File Offset: 0x000B9E94
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (!string.IsNullOrEmpty(this._nav.DataFieldID))
			{
				this.ID = properties.GetPropertyValue(dataItem, this._nav.DataFieldID).ToString();
			}
			if (!string.IsNullOrEmpty(this._nav.DataTextField))
			{
				this.Text = properties.GetPropertyValue(dataItem, this._nav.DataTextField, this._nav.DataTextFormatString);
			}
			else if (!string.IsNullOrEmpty(this._nav.DataTextFormatString))
			{
				this.Text = string.Format(CultureInfo.CurrentCulture, this._nav.DataTextFormatString, new object[]
				{
					dataItem
				});
			}
			else
			{
				this.Text = dataItem.ToString();
			}
			if (!string.IsNullOrEmpty(this._nav.DataNavigateUrlField))
			{
				this.NavigateUrl = DataBinder.GetPropertyValue(dataItem, this._nav.DataNavigateUrlField, null);
			}
			INavigateUIData navigateUIData = dataItem as INavigateUIData;
			if (navigateUIData != null)
			{
				this.Text = navigateUIData.Name;
				this.NavigateUrl = navigateUIData.NavigateUrl;
				this.ToolTip = navigateUIData.Description;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000BBDA6 File Offset: 0x000B9FA6
		IList IItem.Children
		{
			get
			{
				return this.Nodes;
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x000BBDAE File Offset: 0x000B9FAE
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000BBDB4 File Offset: 0x000B9FB4
		internal void RenderTemplate(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!(control is NavigationNode) && !(control is NavigationNodeContentTemplateContainer))
				{
					control.RenderControl(writer);
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000BBE2C File Offset: 0x000BA02C
		public NavigationNode()
		{
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000BBE34 File Offset: 0x000BA034
		public NavigationNode(string text)
		{
			this.Text = text;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000BBE43 File Offset: 0x000BA043
		public NavigationNode(string text, string navigateUrl)
		{
			this.Text = text;
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000BBE59 File Offset: 0x000BA059
		public NavigationNode(RadNavigation control)
		{
			this._nav = control;
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x000BBE68 File Offset: 0x000BA068
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public NavigationNodeCollection Nodes
		{
			get
			{
				if (this._nodes == null)
				{
					this._nodes = new NavigationNodeCollection(this.Nav, this);
				}
				return this._nodes;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000BBE8A File Offset: 0x000BA08A
		// (set) Token: 0x06003936 RID: 14646 RVA: 0x000BBEAA File Offset: 0x000BA0AA
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000BBEBD File Offset: 0x000BA0BD
		// (set) Token: 0x06003938 RID: 14648 RVA: 0x000BBEDD File Offset: 0x000BA0DD
		[Description("The URL to which the menu Node navigates when selected.")]
		[Bindable(true)]
		[Category("Navigation")]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string NavigateUrl
		{
			get
			{
				return (string)(this.ViewState["NavigateUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000BBEF0 File Offset: 0x000BA0F0
		// (set) Token: 0x0600393A RID: 14650 RVA: 0x000BBF10 File Offset: 0x000BA110
		[Description("The navigation target used when the menu Node is selected.")]
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		[Category("Navigation")]
		public virtual string Target
		{
			get
			{
				return (string)(this.ViewState["Target"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000BBF23 File Offset: 0x000BA123
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x000BBF43 File Offset: 0x000BA143
		[Category("Appearance")]
		[Description("The URL for the image for the Node.")]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x000BBF56 File Offset: 0x000BA156
		// (set) Token: 0x0600393E RID: 14654 RVA: 0x000BBF76 File Offset: 0x000BA176
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("The URL for the image when the mouse moves over the Node.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string HoveredImageUrl
		{
			get
			{
				return (string)(this.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000BBF89 File Offset: 0x000BA189
		// (set) Token: 0x06003940 RID: 14656 RVA: 0x000BBFA9 File Offset: 0x000BA1A9
		[UrlProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Appearance")]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000BBFBC File Offset: 0x000BA1BC
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000BBFDC File Offset: 0x000BA1DC
		[DefaultValue("")]
		[Category("Appearance")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Description("The image used when the Node is selected.")]
		public string SelectedImageUrl
		{
			get
			{
				return (string)(this.ViewState["SelectedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x000BBFEF File Offset: 0x000BA1EF
		// (set) Token: 0x06003944 RID: 14660 RVA: 0x000BC010 File Offset: 0x000BA210
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Whether the Node is selected or not")]
		public bool Selected
		{
			get
			{
				return (bool)(this.ViewState["Selected"] ?? false);
			}
			set
			{
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000BC028 File Offset: 0x000BA228
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x000BC048 File Offset: 0x000BA248
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The CSS that is used in sprite image scenarios.")]
		public string SpriteCssClass
		{
			get
			{
				return (string)(this.ViewState["SpriteCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SpriteCssClass"] = value;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000BC05B File Offset: 0x000BA25B
		// (set) Token: 0x06003948 RID: 14664 RVA: 0x000BC063 File Offset: 0x000BA263
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[TemplateContainer(typeof(NavigationNode))]
		public ITemplate NodeTemplate { get; set; }

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x000BC06C File Offset: 0x000BA26C
		// (set) Token: 0x0600394A RID: 14666 RVA: 0x000BC074 File Offset: 0x000BA274
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[TemplateContainer(typeof(NavigationNodeContentTemplateContainer))]
		public ITemplate ContentTemplate { get; set; }

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x000BC07D File Offset: 0x000BA27D
		[Browsable(false)]
		public NavigationNodeContentTemplateContainer ContentTemplateContainer
		{
			get
			{
				if (this._content == null)
				{
					this._content = new NavigationNodeContentTemplateContainer(this);
					this.Controls.Add(this._content);
				}
				return this._content;
			}
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000BC0AC File Offset: 0x000BA2AC
		public void ApplyContentTemplate()
		{
			if (this.ContentTemplate != null)
			{
				if (this._content == null)
				{
					this._content = new NavigationNodeContentTemplateContainer(this);
					this.Controls.Add(this._content);
				}
				this._content.Controls.Clear();
				this.ContentTemplate.InstantiateIn(this._content);
				this.IsContentTemplateInstantiated = true;
				this.DataBind();
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000BC114 File Offset: 0x000BA314
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x000BC130 File Offset: 0x000BA330
		public Dictionary<string, object> TemplateData
		{
			get
			{
				return (Dictionary<string, object>)(this.ViewState["TemplateData"] ?? null);
			}
			set
			{
				this.ViewState["TemplateData"] = value;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000BC143 File Offset: 0x000BA343
		// (set) Token: 0x06003950 RID: 14672 RVA: 0x000BC14B File Offset: 0x000BA34B
		[Browsable(false)]
		public RadNavigation Owner { get; internal set; }

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000BC154 File Offset: 0x000BA354
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x000BC15C File Offset: 0x000BA35C
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x04000F3D RID: 3901
		private const string TemplateExceptionMessage = "Cannot set ContentTemplate on a NavigationNode, which has child Nodes.";

		// Token: 0x04000F3E RID: 3902
		private RadNavigation _nav;

		// Token: 0x04000F3F RID: 3903
		private NavigationNodeCollection _nodes;

		// Token: 0x04000F40 RID: 3904
		private IRenderer _renderer;

		// Token: 0x04000F41 RID: 3905
		private NavigationNodeContentTemplateContainer _content;
	}
}
