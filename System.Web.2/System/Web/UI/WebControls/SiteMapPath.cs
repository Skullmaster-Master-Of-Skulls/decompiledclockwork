using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004CE RID: 1230
	[Designer("System.Web.UI.Design.WebControls.SiteMapPathDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class SiteMapPath : CompositeControl
	{
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000C5028 File Offset: 0x000C3228
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

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x000C5056 File Offset: 0x000C3256
		// (set) Token: 0x06003CFC RID: 15612 RVA: 0x000C505E File Offset: 0x000C325E
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x000C5067 File Offset: 0x000C3267
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x000C5095 File Offset: 0x000C3295
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x000C509D File Offset: 0x000C329D
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

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x000C50A8 File Offset: 0x000C32A8
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x000C50D1 File Offset: 0x000C32D1
		[DefaultValue(-1)]
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x000C50F8 File Offset: 0x000C32F8
		// (set) Token: 0x06003D03 RID: 15619 RVA: 0x000C5121 File Offset: 0x000C3321
		[DefaultValue(PathDirection.RootToCurrent)]
		[WebCategory("Appearance")]
		[WebSysDescription("SiteMapPath_PathDirection")]
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

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000C514C File Offset: 0x000C334C
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x000C5179 File Offset: 0x000C3379
		[DefaultValue(" > ")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("SiteMapPath_PathSeparator")]
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

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000C518C File Offset: 0x000C338C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("SiteMapPath_PathSeparatorStyle")]
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

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x000C51BA File Offset: 0x000C33BA
		// (set) Token: 0x06003D08 RID: 15624 RVA: 0x000C51C2 File Offset: 0x000C33C2
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SiteMapNodeItem))]
		[WebSysDescription("SiteMapPath_PathSeparatorTemplate")]
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

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000C51CC File Offset: 0x000C33CC
		// (set) Token: 0x06003D0A RID: 15626 RVA: 0x000C5263 File Offset: 0x000C3463
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("SiteMapPath_Provider")]
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

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000C526C File Offset: 0x000C346C
		// (set) Token: 0x06003D0C RID: 15628 RVA: 0x000C5295 File Offset: 0x000C3495
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

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x000C52AD File Offset: 0x000C34AD
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

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000C52DB File Offset: 0x000C34DB
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x000C52E3 File Offset: 0x000C34E3
		[Browsable(false)]
		[DefaultValue(null)]
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

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000C52EC File Offset: 0x000C34EC
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000B2546 File Offset: 0x000B0746
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[WebSysDefaultValue("SiteMapPath_Default_SkipToContentText")]
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

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000C5320 File Offset: 0x000C3520
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000C5349 File Offset: 0x000C3549
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapPath_ShowToolTips")]
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

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000C5364 File Offset: 0x000C3564
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x000C5391 File Offset: 0x000C3591
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapPath_SiteMapProvider")]
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

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06003D16 RID: 15638 RVA: 0x000C53AB File Offset: 0x000C35AB
		// (remove) Token: 0x06003D17 RID: 15639 RVA: 0x000C53BE File Offset: 0x000C35BE
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemCreated")]
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

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06003D18 RID: 15640 RVA: 0x000C53D1 File Offset: 0x000C35D1
		// (remove) Token: 0x06003D19 RID: 15641 RVA: 0x000C53E4 File Offset: 0x000C35E4
		[WebCategory("Action")]
		[WebSysDescription("SiteMapPath_OnItemDataBound")]
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

		// Token: 0x06003D1A RID: 15642 RVA: 0x000C53F7 File Offset: 0x000C35F7
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateControlHierarchy();
			base.ClearChildState();
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000C5410 File Offset: 0x000C3610
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

		// Token: 0x06003D1C RID: 15644 RVA: 0x000C5468 File Offset: 0x000C3668
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
				int num = index;
				index = num + 1;
				this.CreateItem(num, SiteMapNodeItemType.Parent, node);
			}
			else
			{
				int num = index;
				index = num + 1;
				this.CreateItem(num, SiteMapNodeItemType.Root, node);
			}
			this.CreateItem(index, SiteMapNodeItemType.PathSeparator, null);
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000C54C0 File Offset: 0x000C36C0
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

		// Token: 0x06003D1E RID: 15646 RVA: 0x000C5528 File Offset: 0x000C3728
		private void CopyStyle(Style toStyle, Style fromStyle)
		{
			if (fromStyle != null && fromStyle.IsSet(8192))
			{
				toStyle.Font.Underline = fromStyle.Font.Underline;
			}
			toStyle.CopyFrom(fromStyle);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000C5558 File Offset: 0x000C3758
		private void CreateMergedStyles()
		{
			this._mergedCurrentNodeStyle = new Style();
			this.CopyStyle(this._mergedCurrentNodeStyle, this._nodeStyle);
			this.CopyStyle(this._mergedCurrentNodeStyle, this._currentNodeStyle);
			this._mergedRootNodeStyle = new Style();
			this.CopyStyle(this._mergedRootNodeStyle, this._nodeStyle);
			this.CopyStyle(this._mergedRootNodeStyle, this._rootNodeStyle);
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000C55C3 File Offset: 0x000C37C3
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000C55D0 File Offset: 0x000C37D0
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

		// Token: 0x06003D22 RID: 15650 RVA: 0x000C57D0 File Offset: 0x000C39D0
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

		// Token: 0x06003D23 RID: 15651 RVA: 0x000C5844 File Offset: 0x000C3A44
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			this.Controls.Clear();
			base.ClearChildState();
			this.CreateControlHierarchy();
			base.ChildControlsCreated = true;
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000C586C File Offset: 0x000C3A6C
		protected virtual void OnItemCreated(SiteMapNodeItemEventArgs e)
		{
			SiteMapNodeItemEventHandler siteMapNodeItemEventHandler = (SiteMapNodeItemEventHandler)base.Events[SiteMapPath._eventItemCreated];
			if (siteMapNodeItemEventHandler != null)
			{
				siteMapNodeItemEventHandler(this, e);
			}
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000C589C File Offset: 0x000C3A9C
		protected virtual void OnItemDataBound(SiteMapNodeItemEventArgs e)
		{
			SiteMapNodeItemEventHandler siteMapNodeItemEventHandler = (SiteMapNodeItemEventHandler)base.Events[SiteMapPath._eventItemDataBound];
			if (siteMapNodeItemEventHandler != null)
			{
				siteMapNodeItemEventHandler(this, e);
			}
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000C58CA File Offset: 0x000C3ACA
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				base.ChildControlsCreated = false;
				this.EnsureChildControls();
			}
			base.Render(writer);
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000C58E8 File Offset: 0x000C3AE8
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			ControlRenderingHelper.WriteSkipLinkStart(writer, this.RenderingCompatibility, base.DesignMode, this.SkipLinkText, base.SpacerImageUrl, this.ClientID);
			base.RenderContents(writer);
			ControlRenderingHelper.WriteSkipLinkEnd(writer, base.DesignMode, this.SkipLinkText, this.ClientID);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000C5938 File Offset: 0x000C3B38
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

		// Token: 0x06003D29 RID: 15657 RVA: 0x000C59D0 File Offset: 0x000C3BD0
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

		// Token: 0x040023A6 RID: 9126
		private const string _defaultSeparator = " > ";

		// Token: 0x040023A7 RID: 9127
		private static readonly object _eventItemCreated = new object();

		// Token: 0x040023A8 RID: 9128
		private static readonly object _eventItemDataBound = new object();

		// Token: 0x040023A9 RID: 9129
		private SiteMapProvider _provider;

		// Token: 0x040023AA RID: 9130
		private Style _currentNodeStyle;

		// Token: 0x040023AB RID: 9131
		private Style _rootNodeStyle;

		// Token: 0x040023AC RID: 9132
		private Style _nodeStyle;

		// Token: 0x040023AD RID: 9133
		private Style _pathSeparatorStyle;

		// Token: 0x040023AE RID: 9134
		private Style _mergedCurrentNodeStyle;

		// Token: 0x040023AF RID: 9135
		private Style _mergedRootNodeStyle;

		// Token: 0x040023B0 RID: 9136
		private ITemplate _currentNodeTemplate;

		// Token: 0x040023B1 RID: 9137
		private ITemplate _rootNodeTemplate;

		// Token: 0x040023B2 RID: 9138
		private ITemplate _nodeTemplate;

		// Token: 0x040023B3 RID: 9139
		private ITemplate _pathSeparatorTemplate;
	}
}
