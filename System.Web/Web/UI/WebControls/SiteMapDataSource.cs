using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200063E RID: 1598
	[WebSysDisplayName("SiteMapDataSource_DisplayName")]
	[ParseChildren(true)]
	[Designer("System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(SiteMapDataSource))]
	[WebSysDescription("SiteMapDataSource_Description")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SiteMapDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x0013E300 File Offset: 0x0013D300
		[WebSysDescription("SiteMapDataSource_ContainsListCollection")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x0013E308 File Offset: 0x0013D308
		// (set) Token: 0x06004ECE RID: 20174 RVA: 0x0013E399 File Offset: 0x0013D399
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("SiteMapDataSource_Provider")]
		[Browsable(false)]
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

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x0013E3B8 File Offset: 0x0013D3B8
		// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x0013E3E1 File Offset: 0x0013D3E1
		[WebSysDescription("SiteMapDataSource_ShowStartingNode")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
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

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x0013E410 File Offset: 0x0013D410
		// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x0013E43D File Offset: 0x0013D43D
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_SiteMapProvider")]
		[DefaultValue("")]
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

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x0013E470 File Offset: 0x0013D470
		// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x0013E499 File Offset: 0x0013D499
		[WebCategory("Behavior")]
		[DefaultValue(0)]
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

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x0013E4C8 File Offset: 0x0013D4C8
		// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x0013E4F1 File Offset: 0x0013D4F1
		[WebCategory("Behavior")]
		[WebSysDescription("SiteMapDataSource_StartFromCurrentNode")]
		[DefaultValue(false)]
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

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x0013E520 File Offset: 0x0013D520
		// (set) Token: 0x06004ED8 RID: 20184 RVA: 0x0013E54D File Offset: 0x0013D54D
		[WebCategory("Behavior")]
		[UrlProperty]
		[DefaultValue("")]
		[WebSysDescription("SiteMapDataSource_StartingNodeUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x06004ED9 RID: 20185 RVA: 0x0013E57C File Offset: 0x0013D57C
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

		// Token: 0x06004EDA RID: 20186 RVA: 0x0013E6F1 File Offset: 0x0013D6F1
		private SiteMapNodeCollection GetNodes(SiteMapNode node)
		{
			if (this.ShowStartingNode)
			{
				return new SiteMapNodeCollection(node);
			}
			return node.ChildNodes;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x0013E708 File Offset: 0x0013D708
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

		// Token: 0x06004EDC RID: 20188 RVA: 0x0013E745 File Offset: 0x0013D745
		public virtual IList GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0013E750 File Offset: 0x0013D750
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

		// Token: 0x06004EDE RID: 20190 RVA: 0x0013E794 File Offset: 0x0013D794
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

		// Token: 0x06004EDF RID: 20191 RVA: 0x0013E7E4 File Offset: 0x0013D7E4
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

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0013E848 File Offset: 0x0013D848
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

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0013E87C File Offset: 0x0013D87C
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

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06004EE2 RID: 20194 RVA: 0x0013E8B3 File Offset: 0x0013D8B3
		// (remove) Token: 0x06004EE3 RID: 20195 RVA: 0x0013E8BC File Offset: 0x0013D8BC
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

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0013E8C5 File Offset: 0x0013D8C5
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0013E8CE File Offset: 0x0013D8CE
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x0013E8D6 File Offset: 0x0013D8D6
		bool IListSource.ContainsListCollection
		{
			get
			{
				return !base.DesignMode && this.ContainsListCollection;
			}
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x0013E8E8 File Offset: 0x0013D8E8
		IList IListSource.GetList()
		{
			if (base.DesignMode)
			{
				return null;
			}
			return this.GetList();
		}

		// Token: 0x04002CB2 RID: 11442
		private const string DefaultViewName = "DefaultView";

		// Token: 0x04002CB3 RID: 11443
		private ICollection _viewNames;

		// Token: 0x04002CB4 RID: 11444
		private SiteMapDataSourceView _dataSourceView;

		// Token: 0x04002CB5 RID: 11445
		private SiteMapProvider _provider;
	}
}
