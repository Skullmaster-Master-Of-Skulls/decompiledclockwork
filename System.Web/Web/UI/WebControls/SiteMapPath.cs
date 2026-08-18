using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000645 RID: 1605
	[Designer("System.Web.UI.Design.WebControls.SiteMapPathDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SiteMapPath : CompositeControl
	{
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x0013EA11 File Offset: 0x0013DA11
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("SiteMapPath_CurrentNodeStyle")]
		public Style CurrentNodeStyle
		{
			get
			{
				if (this._currentNodeStyle == null)
				{
					this._currentNodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._currentNodeStyle).TrackViewState();
					}
				}
				return this._currentNodeStyle;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x0013EA3F File Offset: 0x0013DA3F
		// (set) Token: 0x06004F03 RID: 20227 RVA: 0x0013EA47 File Offset: 0x0013DA47
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Browsable(false)]
		[TemplateContainer(typeof(SiteMapNodeItem))]
		[WebSysDescription("SiteMapPath_CurrentNodeTemplate")]
		public virtual ITemplate CurrentNodeTemplate
		{
			get
			{
				return this._currentNodeTemplate;
			}
			set
			{
				this._currentNodeTemplate = value;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x0013EA50 File Offset: 0x0013DA50
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[WebSysDescription("SiteMapPath_NodeStyle")]
		public Style NodeStyle
		{
			get
			{
				if (this._nodeStyle == null)
				{
					this._nodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._nodeStyle).TrackViewState();
					}
				}
				return this._nodeStyle;
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0013EA7E File Offset: 0x0013DA7E
		// (set) Token: 0x06004F06 RID: 20230 RVA: 0x0013EA86 File Offset: 0x0013DA86
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SiteMapNodeItem))]
		[WebSysDescription("SiteMapPath_NodeTemplate")]
		public virtual ITemplate NodeTemplate
		{
			get
			{
				return this._nodeTemplate;
			}
			set
			{
				this._nodeTemplate = value;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x0013EA90 File Offset: 0x0013DA90
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x0013EAB9 File Offset: 0x0013DAB9
		[DefaultValue(-1)]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("SiteMapPath_ParentLevelsDisplayed")]
		public virtual int ParentLevelsDisplayed
		{
			get
			{
				object obj = this.ViewState["ParentLevelsDisplayed"];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ParentLevelsDisplayed"] = value;
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x0013EAE0 File Offset: 0x0013DAE0
		// (set) Token: 0x06004F0A RID: 20234 RVA: 0x0013EB09 File Offset: 0x0013DB09
		[WebSysDescription("SiteMapPath_PathDirection")]
		[DefaultValue(PathDirection.RootToCurrent)]
		[WebCategory("Appearance")]
		public virtual PathDirection PathDirection
		{
			get
			{
				object obj = this.ViewState["PathDirection"];
				if (obj == null)
				{
					return PathDirection.RootToCurrent;
				}
				return (PathDirection)obj;
			}
			set
			{
				if (value < PathDirection.RootToCurrent || value > PathDirection.CurrentToRoot)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PathDirection"] = value;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0013EB34 File Offset: 0x0013DB34
		// (set) Token: 0x06004F0C RID: 20236 RVA: 0x0013EB61 File Offset: 0x0013DB61
		[WebSysDescription("SiteMapPath_PathSeparator")]
		[DefaultValue(" > ")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		public virtual string PathSeparator
		{
			get
			{
				string text = (string)this.ViewState["PathSeparator"];
				if (text == null)
				{
					return " > ";
				}
				return text;
			}
			set
			{
				this.ViewState["PathSeparator"] = value;
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06004F0D RID: 20237 RVA: 0x0013EB74 File Offset: 0x0013DB74
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("SiteMapPath_PathSeparatorStyle")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		public Style PathSeparatorStyle
		{
			get
			{
				if (this._pathSeparatorStyle == null)
				{
					this._pathSeparatorStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pathSeparatorStyle).TrackViewState();
					}
				}
				return this._pathSeparatorStyle;
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x0013EBA2 File Offset: 0x0013DBA2
		// (set) Token: 0x06004F0F RID: 20239 RVA: 0x0013EBAA File Offset: 0x0013DBAA
		[Browsable(false)]
		[WebSysDescription("SiteMapPath_PathSeparatorTemplate")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SiteMapNodeItem))]
		public virtual ITemplate PathSeparatorTemplate
		{
			get
			{
				return this._pathSeparatorTemplate;
			}
			set
			{
				this._pathSeparatorTemplate = value;
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x0013EBB4 File Offset: 0x0013DBB4
		// (set) Token: 0x06004F11 RID: 20241 RVA: 0x0013EC4D File Offset: 0x0013DC4D
		[WebSysDescription("SiteMapPath_Provider")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SiteMapProvider Provider
		{
			get
			{
				if (this._provider != null || base.DesignMode)
				{
					return this._provider;
				}
				if (string.IsNullOrEmpty(this.SiteMapProvider))
				{
					this._provider = SiteMap.Provider;
					if (this._provider == null)
					{
						throw new HttpException(SR.GetString("SiteMapDataSource_DefaultProviderNotFound"));
					}
				}
				else
				{
					this._provider = SiteMap.Providers[this.SiteMapProvider];
					if (this._provider == null)
					{
						throw new HttpException(SR.GetString("SiteMapDataSource_ProviderNotFound", new object[]
						{
							this.SiteMapProvider
						}));
					}
				}
				return this._provider;
			}
			set
			{
				this._provider = value;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0013EC58 File Offset: 0x0013DC58
		// (set) Token: 0x06004F13 RID: 20243 RVA: 0x0013EC81 File Offset: 0x0013DC81
		[DefaultValue(false)]
		[WebCategory("Appearance")]
		[WebSysDescription("SiteMapPath_RenderCurrentNodeAsLink")]
		public virtual bool RenderCurrentNodeAsLink
		{
			get
			{
				object obj = this.ViewState["RenderCurrentNodeAsLink"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RenderCurrentNodeAsLink"] = value;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x0013EC99 File Offset: 0x0013DC99
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("SiteMapPath_RootNodeStyle")]
		public Style RootNodeStyle
		{
			get
			{
				if (this._rootNodeStyle == null)
				{
					this._rootNodeStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._rootNodeStyle).TrackViewState();
					}
				}
				return this._rootNodeStyle;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0013ECC7 File Offset: 0x0013DCC7
		// (set) Token: 0x06004F16 RID: 20246 RVA: 0x0013ECCF File Offset: 0x0013DCCF
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SiteMapNodeItem))]
		[WebSysDescription("SiteMapPath_RootNodeTemplate")]
		public virtual ITemplate RootNodeTemplate
		{
			get
			{
				return this._rootNodeTemplate;
			}
			set
			{
				this._rootNodeTemplate = value;
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0013ECD8 File Offset: 0x0013DCD8
		// (set) Token: 0x06004F18 RID: 20248 RVA: 0x0013ED0A File Offset: 0x0013DD0A
		[WebSysDefaultValue("SiteMapPath_Default_SkipToContentText")]
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[WebSysDescription("SiteMapPath_SkipToContentText")]
		public virtual string SkipLinkText
		{
			get
			{
				string text = this.ViewState["SkipLinkText"] as string;
				if (text != null)
				{
					return text;
				}
				return SR.GetString("SiteMapPath_Default_SkipToContentText");
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x0013ED20 File Offset: 0x0013DD20
		// (set) Token: 0x06004F1A RID: 20250 RVA: 0x0013ED49 File Offset: 0x0013DD49
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("SiteMapPath_ShowToolTips")]
		[WebCategory("Behavior")]
		public virtual bool ShowToolTips
		{
			get
			{
				object obj = this.ViewState["ShowToolTips"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowToolTips"] = value;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x0013ED64 File Offset: 0x0013DD64
		// (set) Token: 0x06004F1C RID: 20252 RVA: 0x0013ED91 File Offset: 0x0013DD91
		[DefaultValue("")]
		[WebSysDescription("SiteMapPath_SiteMapProvider")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		public virtual string SiteMapProvider
		{
			get
			{
				string text = this.ViewState["SiteMapProvider"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SiteMapProvider"] = value;
				this._provider = null;
			}
		}

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06004F1D RID: 20253 RVA: 0x0013EDAB File Offset: 0x0013DDAB
		// (remove) Token: 0x06004F1E RID: 20254 RVA: 0x0013EDBE File Offset: 0x0013DDBE
		[WebSysDescription("DataControls_OnItemCreated")]
		[WebCategory("Action")]
		public event SiteMapNodeItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(SiteMapPath._eventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SiteMapPath._eventItemCreated, value);
			}
		}

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x06004F1F RID: 20255 RVA: 0x0013EDD1 File Offset: 0x0013DDD1
		// (remove) Token: 0x06004F20 RID: 20256 RVA: 0x0013EDE4 File Offset: 0x0013DDE4
		[WebSysDescription("SiteMapPath_OnItemDataBound")]
		[WebCategory("Action")]
		public event SiteMapNodeItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(SiteMapPath._eventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(SiteMapPath._eventItemDataBound, value);
			}
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x0013EDF7 File Offset: 0x0013DDF7
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateControlHierarchy();
			base.ClearChildState();
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0013EE10 File Offset: 0x0013DE10
		protected virtual void CreateControlHierarchy()
		{
			if (this.Provider == null)
			{
				return;
			}
			int num = 0;
			this.CreateMergedStyles();
			SiteMapNode currentNodeAndHintAncestorNodes = this.Provider.GetCurrentNodeAndHintAncestorNodes(-1);
			if (currentNodeAndHintAncestorNodes != null)
			{
				SiteMapNode parentNode = currentNodeAndHintAncestorNodes.ParentNode;
				if (parentNode != null)
				{
					this.CreateControlHierarchyRecursive(ref num, parentNode, this.ParentLevelsDisplayed);
				}
				this.CreateItem(num++, SiteMapNodeItemType.Current, currentNodeAndHintAncestorNodes);
			}
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x0013EE68 File Offset: 0x0013DE68
		private void CreateControlHierarchyRecursive(ref int index, SiteMapNode node, int parentLevels)
		{
			if (parentLevels == 0)
			{
				return;
			}
			SiteMapNode parentNode = node.ParentNode;
			if (parentNode != null)
			{
				this.CreateControlHierarchyRecursive(ref index, parentNode, parentLevels - 1);
				this.CreateItem(index++, SiteMapNodeItemType.Parent, node);
			}
			else
			{
				this.CreateItem(index++, SiteMapNodeItemType.Root, node);
			}
			this.CreateItem(index, SiteMapNodeItemType.PathSeparator, null);
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x0013EEC0 File Offset: 0x0013DEC0
		private SiteMapNodeItem CreateItem(int itemIndex, SiteMapNodeItemType itemType, SiteMapNode node)
		{
			SiteMapNodeItem siteMapNodeItem = new SiteMapNodeItem(itemIndex, itemType);
			int index = (this.PathDirection == PathDirection.CurrentToRoot) ? 0 : -1;
			SiteMapNodeItemEventArgs e = new SiteMapNodeItemEventArgs(siteMapNodeItem);
			siteMapNodeItem.SiteMapNode = node;
			this.InitializeItem(siteMapNodeItem);
			this.OnItemCreated(e);
			this.Controls.AddAt(index, siteMapNodeItem);
			siteMapNodeItem.DataBind();
			this.OnItemDataBound(e);
			siteMapNodeItem.SiteMapNode = null;
			siteMapNodeItem.EnableViewState = false;
			return siteMapNodeItem;
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0013EF28 File Offset: 0x0013DF28
		private void CopyStyle(Style toStyle, Style fromStyle)
		{
			if (fromStyle != null && fromStyle.IsSet(8192))
			{
				toStyle.Font.Underline = fromStyle.Font.Underline;
			}
			toStyle.CopyFrom(fromStyle);
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0013EF58 File Offset: 0x0013DF58
		private void CreateMergedStyles()
		{
			this._mergedCurrentNodeStyle = new Style();
			this.CopyStyle(this._mergedCurrentNodeStyle, this._nodeStyle);
			this.CopyStyle(this._mergedCurrentNodeStyle, this._currentNodeStyle);
			this._mergedRootNodeStyle = new Style();
			this.CopyStyle(this._mergedRootNodeStyle, this._nodeStyle);
			this.CopyStyle(this._mergedRootNodeStyle, this._rootNodeStyle);
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0013EFC3 File Offset: 0x0013DFC3
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0013EFD0 File Offset: 0x0013DFD0
		protected virtual void InitializeItem(SiteMapNodeItem item)
		{
			ITemplate template = null;
			Style style = null;
			SiteMapNodeItemType itemType = item.ItemType;
			SiteMapNode siteMapNode = item.SiteMapNode;
			switch (itemType)
			{
			case SiteMapNodeItemType.Root:
				template = ((this.RootNodeTemplate != null) ? this.RootNodeTemplate : this.NodeTemplate);
				style = this._mergedRootNodeStyle;
				break;
			case SiteMapNodeItemType.Parent:
				template = this.NodeTemplate;
				style = this._nodeStyle;
				break;
			case SiteMapNodeItemType.Current:
				template = ((this.CurrentNodeTemplate != null) ? this.CurrentNodeTemplate : this.NodeTemplate);
				style = this._mergedCurrentNodeStyle;
				break;
			case SiteMapNodeItemType.PathSeparator:
				template = this.PathSeparatorTemplate;
				style = this._pathSeparatorStyle;
				break;
			}
			if (template != null)
			{
				template.InstantiateIn(item);
				item.ApplyStyle(style);
				return;
			}
			if (itemType == SiteMapNodeItemType.PathSeparator)
			{
				Literal literal = new Literal();
				literal.Mode = LiteralMode.Encode;
				literal.Text = this.PathSeparator;
				item.Controls.Add(literal);
				item.ApplyStyle(style);
				return;
			}
			if (itemType == SiteMapNodeItemType.Current && !this.RenderCurrentNodeAsLink)
			{
				Literal literal2 = new Literal();
				literal2.Mode = LiteralMode.Encode;
				literal2.Text = siteMapNode.Title;
				item.Controls.Add(literal2);
				item.ApplyStyle(style);
				return;
			}
			HyperLink hyperLink = new HyperLink();
			if (style != null && style.IsSet(8192))
			{
				hyperLink.Font.Underline = style.Font.Underline;
			}
			hyperLink.EnableTheming = false;
			hyperLink.Enabled = this.Enabled;
			if (siteMapNode.Url.StartsWith("\\\\", StringComparison.Ordinal))
			{
				hyperLink.NavigateUrl = base.ResolveClientUrl(HttpUtility.UrlPathEncode(siteMapNode.Url));
			}
			else
			{
				hyperLink.NavigateUrl = ((this.Context != null) ? this.Context.Response.ApplyAppPathModifier(base.ResolveClientUrl(HttpUtility.UrlPathEncode(siteMapNode.Url))) : siteMapNode.Url);
			}
			hyperLink.Text = HttpUtility.HtmlEncode(siteMapNode.Title);
			if (this.ShowToolTips)
			{
				hyperLink.ToolTip = siteMapNode.Description;
			}
			item.Controls.Add(hyperLink);
			hyperLink.ApplyStyle(style);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0013F1D4 File Offset: 0x0013E1D4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				base.LoadViewState(array[0]);
				if (array[1] != null)
				{
					((IStateManager)this.CurrentNodeStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.NodeStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.RootNodeStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.PathSeparatorStyle).LoadViewState(array[4]);
					return;
				}
			}
			else
			{
				base.LoadViewState(null);
			}
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0013F248 File Offset: 0x0013E248
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			this.Controls.Clear();
			base.ClearChildState();
			this.CreateControlHierarchy();
			base.ChildControlsCreated = true;
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0013F270 File Offset: 0x0013E270
		protected virtual void OnItemCreated(SiteMapNodeItemEventArgs e)
		{
			SiteMapNodeItemEventHandler siteMapNodeItemEventHandler = (SiteMapNodeItemEventHandler)base.Events[SiteMapPath._eventItemCreated];
			if (siteMapNodeItemEventHandler != null)
			{
				siteMapNodeItemEventHandler(this, e);
			}
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0013F2A0 File Offset: 0x0013E2A0
		protected virtual void OnItemDataBound(SiteMapNodeItemEventArgs e)
		{
			SiteMapNodeItemEventHandler siteMapNodeItemEventHandler = (SiteMapNodeItemEventHandler)base.Events[SiteMapPath._eventItemDataBound];
			if (siteMapNodeItemEventHandler != null)
			{
				siteMapNodeItemEventHandler(this, e);
			}
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0013F2CE File Offset: 0x0013E2CE
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				base.ChildControlsCreated = false;
				this.EnsureChildControls();
			}
			base.Render(writer);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0013F2EC File Offset: 0x0013E2EC
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			bool flag = !string.IsNullOrEmpty(this.SkipLinkText) && !base.DesignMode;
			string text = this.ClientID + "_SkipLink";
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + text);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.SkipLinkText);
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.SpacerImageUrl);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			base.RenderContents(writer);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, text);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0013F3B8 File Offset: 0x0013E3B8
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._currentNodeStyle != null) ? ((IStateManager)this._currentNodeStyle).SaveViewState() : null,
				(this._nodeStyle != null) ? ((IStateManager)this._nodeStyle).SaveViewState() : null,
				(this._rootNodeStyle != null) ? ((IStateManager)this._rootNodeStyle).SaveViewState() : null,
				(this._pathSeparatorStyle != null) ? ((IStateManager)this._pathSeparatorStyle).SaveViewState() : null
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x0013F450 File Offset: 0x0013E450
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._currentNodeStyle != null)
			{
				((IStateManager)this._currentNodeStyle).TrackViewState();
			}
			if (this._nodeStyle != null)
			{
				((IStateManager)this._nodeStyle).TrackViewState();
			}
			if (this._rootNodeStyle != null)
			{
				((IStateManager)this._rootNodeStyle).TrackViewState();
			}
			if (this._pathSeparatorStyle != null)
			{
				((IStateManager)this._pathSeparatorStyle).TrackViewState();
			}
		}

		// Token: 0x04002CC2 RID: 11458
		private const string _defaultSeparator = " > ";

		// Token: 0x04002CC3 RID: 11459
		private const string _afterSiteMapPathMark = "_SkipLink";

		// Token: 0x04002CC4 RID: 11460
		private static readonly object _eventItemCreated = new object();

		// Token: 0x04002CC5 RID: 11461
		private static readonly object _eventItemDataBound = new object();

		// Token: 0x04002CC6 RID: 11462
		private SiteMapProvider _provider;

		// Token: 0x04002CC7 RID: 11463
		private Style _currentNodeStyle;

		// Token: 0x04002CC8 RID: 11464
		private Style _rootNodeStyle;

		// Token: 0x04002CC9 RID: 11465
		private Style _nodeStyle;

		// Token: 0x04002CCA RID: 11466
		private Style _pathSeparatorStyle;

		// Token: 0x04002CCB RID: 11467
		private Style _mergedCurrentNodeStyle;

		// Token: 0x04002CCC RID: 11468
		private Style _mergedRootNodeStyle;

		// Token: 0x04002CCD RID: 11469
		private ITemplate _currentNodeTemplate;

		// Token: 0x04002CCE RID: 11470
		private ITemplate _rootNodeTemplate;

		// Token: 0x04002CCF RID: 11471
		private ITemplate _nodeTemplate;

		// Token: 0x04002CD0 RID: 11472
		private ITemplate _pathSeparatorTemplate;
	}
}
