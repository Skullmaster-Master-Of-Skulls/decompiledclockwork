using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C7 RID: 1223
	[Designer("System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(SiteMapDataSource))]
	[WebSysDescription("SiteMapDataSource_Description")]
	[WebSysDisplayName("SiteMapDataSource_DisplayName")]
	public class SiteMapDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000C4960 File Offset: 0x000C2B60
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("SiteMapDataSource_ContainsListCollection")]
		public virtual bool ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000C4968 File Offset: 0x000C2B68
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x000C49F7 File Offset: 0x000C2BF7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("SiteMapDataSource_Provider")]
		public SiteMapProvider Provider
		{
			get
			{
				if (this._provider != null)
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
				if (this._provider != value)
				{
					this._provider = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000C4A14 File Offset: 0x000C2C14
		// (set) Token: 0x06003CC9 RID: 15561 RVA: 0x000C4A3D File Offset: 0x000C2C3D
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_ShowStartingNode")]
		public virtual bool ShowStartingNode
		{
			get
			{
				object obj = this.ViewState["ShowStartingNode"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value != this.ShowStartingNode)
				{
					this.ViewState["ShowStartingNode"] = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000C4A6C File Offset: 0x000C2C6C
		// (set) Token: 0x06003CCB RID: 15563 RVA: 0x000C4A99 File Offset: 0x000C2C99
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_SiteMapProvider")]
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
				if (value != this.SiteMapProvider)
				{
					this._provider = null;
					this.ViewState["SiteMapProvider"] = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000C4ACC File Offset: 0x000C2CCC
		// (set) Token: 0x06003CCD RID: 15565 RVA: 0x000C4AF5 File Offset: 0x000C2CF5
		[DefaultValue(0)]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_StartingNodeOffset")]
		public virtual int StartingNodeOffset
		{
			get
			{
				object obj = this.ViewState["StartingNodeOffset"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				if (value != this.StartingNodeOffset)
				{
					this.ViewState["StartingNodeOffset"] = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000C4B24 File Offset: 0x000C2D24
		// (set) Token: 0x06003CCF RID: 15567 RVA: 0x000C4B4D File Offset: 0x000C2D4D
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_StartFromCurrentNode")]
		public virtual bool StartFromCurrentNode
		{
			get
			{
				object obj = this.ViewState["StartFromCurrentNode "];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value != this.StartFromCurrentNode)
				{
					this.ViewState["StartFromCurrentNode "] = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000C4B7C File Offset: 0x000C2D7C
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x000C4BA9 File Offset: 0x000C2DA9
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_StartingNodeUrl")]
		public virtual string StartingNodeUrl
		{
			get
			{
				string text = this.ViewState["StartingNodeUrl"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.StartingNodeUrl)
				{
					this.ViewState["StartingNodeUrl"] = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000C4BD8 File Offset: 0x000C2DD8
		private SiteMapNodeCollection GetNodes()
		{
			int num = this.StartingNodeOffset;
			if (!string.IsNullOrEmpty(this.StartingNodeUrl) && this.StartFromCurrentNode)
			{
				throw new InvalidOperationException(SR.GetString("SiteMapDataSource_StartingNodeUrlAndStartFromcurrentNode_Defined"));
			}
			SiteMapNode siteMapNode;
			if (this.StartFromCurrentNode)
			{
				siteMapNode = this.Provider.CurrentNode;
			}
			else if (!string.IsNullOrEmpty(this.StartingNodeUrl))
			{
				siteMapNode = this.Provider.FindSiteMapNode(this.MakeUrlAbsolute(this.StartingNodeUrl));
				if (siteMapNode == null)
				{
					throw new ArgumentException(SR.GetString("SiteMapPath_CannotFindUrl", new object[]
					{
						this.StartingNodeUrl
					}));
				}
			}
			else
			{
				siteMapNode = this.Provider.RootNode;
			}
			if (siteMapNode == null)
			{
				return null;
			}
			if (num <= 0)
			{
				if (num != 0)
				{
					this.Provider.HintNeighborhoodNodes(siteMapNode, Math.Abs(num), 0);
					SiteMapNode parentNode = siteMapNode.ParentNode;
					while (num < 0 && parentNode != null)
					{
						siteMapNode = siteMapNode.ParentNode;
						parentNode = siteMapNode.ParentNode;
						num++;
					}
				}
				return this.GetNodes(siteMapNode);
			}
			SiteMapNode currentNodeAndHintAncestorNodes = this.Provider.GetCurrentNodeAndHintAncestorNodes(-1);
			if (currentNodeAndHintAncestorNodes == null || !currentNodeAndHintAncestorNodes.IsDescendantOf(siteMapNode) || currentNodeAndHintAncestorNodes.Equals(siteMapNode))
			{
				return null;
			}
			SiteMapNode siteMapNode2 = currentNodeAndHintAncestorNodes;
			for (int i = 0; i < num; i++)
			{
				siteMapNode2 = siteMapNode2.ParentNode;
				if (siteMapNode2 == null || siteMapNode2.Equals(siteMapNode))
				{
					return this.GetNodes(currentNodeAndHintAncestorNodes);
				}
			}
			SiteMapNode siteMapNode3 = currentNodeAndHintAncestorNodes;
			while (siteMapNode2 != null && !siteMapNode2.Equals(siteMapNode))
			{
				siteMapNode3 = siteMapNode3.ParentNode;
				siteMapNode2 = siteMapNode2.ParentNode;
			}
			return this.GetNodes(siteMapNode3);
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000C4D42 File Offset: 0x000C2F42
		private SiteMapNodeCollection GetNodes(SiteMapNode node)
		{
			if (this.ShowStartingNode)
			{
				return new SiteMapNodeCollection(node);
			}
			return node.ChildNodes;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000C4D59 File Offset: 0x000C2F59
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
		{
			if (this.Provider == null)
			{
				throw new HttpException(SR.GetString("SiteMapDataSource_ProviderNotFound", new object[]
				{
					this.SiteMapProvider
				}));
			}
			return this.GetTreeView(viewPath);
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000C4D89 File Offset: 0x000C2F89
		public virtual IList GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000C4D94 File Offset: 0x000C2F94
		internal SiteMapNodeCollection GetPathNodeCollection(string viewPath)
		{
			SiteMapNodeCollection siteMapNodeCollection = null;
			if (string.IsNullOrEmpty(viewPath))
			{
				siteMapNodeCollection = this.GetNodes();
			}
			else
			{
				SiteMapNode siteMapNode = this.Provider.FindSiteMapNodeFromKey(viewPath);
				if (siteMapNode != null)
				{
					siteMapNodeCollection = siteMapNode.ChildNodes;
				}
			}
			if (siteMapNodeCollection == null)
			{
				siteMapNodeCollection = SiteMapNodeCollection.Empty;
			}
			return siteMapNodeCollection;
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000C4DD8 File Offset: 0x000C2FD8
		private HierarchicalDataSourceView GetTreeView(string viewPath)
		{
			if (string.IsNullOrEmpty(viewPath))
			{
				SiteMapNodeCollection nodes = this.GetNodes();
				if (nodes != null)
				{
					return nodes.GetHierarchicalDataSourceView();
				}
			}
			else
			{
				SiteMapNode siteMapNode = this.Provider.FindSiteMapNodeFromKey(viewPath);
				if (siteMapNode != null)
				{
					return siteMapNode.ChildNodes.GetHierarchicalDataSourceView();
				}
			}
			return SiteMapNodeCollection.Empty.GetHierarchicalDataSourceView();
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000C4E28 File Offset: 0x000C3028
		public virtual DataSourceView GetView(string viewName)
		{
			if (this.Provider == null)
			{
				throw new HttpException(SR.GetString("SiteMapDataSource_ProviderNotFound", new object[]
				{
					this.SiteMapProvider
				}));
			}
			if (this._dataSourceView == null)
			{
				this._dataSourceView = SiteMapNodeCollection.ReadOnly(this.GetPathNodeCollection(viewName)).GetDataSourceView(this, string.Empty);
			}
			return this._dataSourceView;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000C4E87 File Offset: 0x000C3087
		public virtual ICollection GetViewNames()
		{
			if (this._viewNames == null)
			{
				this._viewNames = new string[]
				{
					"DefaultView"
				};
			}
			return this._viewNames;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000C4EAC File Offset: 0x000C30AC
		private string MakeUrlAbsolute(string url)
		{
			if (url.Length == 0 || !UrlPath.IsRelativeUrl(url))
			{
				return url;
			}
			string appRelativeTemplateSourceDirectory = base.AppRelativeTemplateSourceDirectory;
			if (appRelativeTemplateSourceDirectory.Length == 0)
			{
				return url;
			}
			return UrlPath.Combine(appRelativeTemplateSourceDirectory, url);
		}

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06003CDB RID: 15579 RVA: 0x000C4EE3 File Offset: 0x000C30E3
		// (remove) Token: 0x06003CDC RID: 15580 RVA: 0x000C4EEC File Offset: 0x000C30EC
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				((IHierarchicalDataSource)this).DataSourceChanged += value;
			}
			remove
			{
				((IHierarchicalDataSource)this).DataSourceChanged -= value;
			}
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000C4EF5 File Offset: 0x000C30F5
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000C4EFE File Offset: 0x000C30FE
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x000C4F06 File Offset: 0x000C3106
		bool IListSource.ContainsListCollection
		{
			get
			{
				return !base.DesignMode && this.ContainsListCollection;
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000C4F18 File Offset: 0x000C3118
		IList IListSource.GetList()
		{
			if (base.DesignMode)
			{
				return null;
			}
			return this.GetList();
		}

		// Token: 0x04002396 RID: 9110
		private const string DefaultViewName = "DefaultView";

		// Token: 0x04002397 RID: 9111
		private ICollection _viewNames;

		// Token: 0x04002398 RID: 9112
		private SiteMapDataSourceView _dataSourceView;

		// Token: 0x04002399 RID: 9113
		private SiteMapProvider _provider;
	}
}
