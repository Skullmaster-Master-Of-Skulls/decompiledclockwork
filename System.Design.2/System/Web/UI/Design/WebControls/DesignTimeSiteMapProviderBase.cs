using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C2 RID: 194
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DesignTimeSiteMapProviderBase : StaticSiteMapProvider
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00021105 File Offset: 0x0001F305
		internal DesignTimeSiteMapProviderBase(IDesignerHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			this._host = host;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00021122 File Offset: 0x0001F322
		public override SiteMapNode CurrentNode
		{
			get
			{
				this.BuildDesignTimeSiteMapInternal();
				return this._currentNode;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00021134 File Offset: 0x0001F334
		internal string DocumentAppRelativeUrl
		{
			get
			{
				if (this._host != null)
				{
					IComponent rootComponent = this._host.RootComponent;
					if (rootComponent != null)
					{
						WebFormsRootDesigner webFormsRootDesigner = this._host.GetDesigner(rootComponent) as WebFormsRootDesigner;
						if (webFormsRootDesigner != null)
						{
							return webFormsRootDesigner.DocumentUrl;
						}
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00021179 File Offset: 0x0001F379
		protected internal override SiteMapNode GetRootNodeCore()
		{
			this.BuildDesignTimeSiteMapInternal();
			return this._rootNode;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00021188 File Offset: 0x0001F388
		private SiteMapNode BuildDesignTimeSiteMapInternal()
		{
			if (this._rootNode != null)
			{
				return this._rootNode;
			}
			this._rootNode = new SiteMapNode(this, DesignTimeSiteMapProviderBase._rootNodeText + " url", DesignTimeSiteMapProviderBase._rootNodeText + " url", DesignTimeSiteMapProviderBase._rootNodeText, DesignTimeSiteMapProviderBase._rootNodeText);
			this._currentNode = new SiteMapNode(this, DesignTimeSiteMapProviderBase._currentNodeText + " url", DesignTimeSiteMapProviderBase._currentNodeText + " url", DesignTimeSiteMapProviderBase._currentNodeText, DesignTimeSiteMapProviderBase._currentNodeText);
			SiteMapNode siteMapNode = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._parentNodeText);
			SiteMapNode node = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._siblingNodeText1);
			SiteMapNode node2 = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._siblingNodeText2);
			SiteMapNode node3 = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._siblingNodeText3);
			SiteMapNode node4 = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._childNodeText1);
			SiteMapNode node5 = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._childNodeText2);
			SiteMapNode node6 = this.CreateNewSiteMapNode(DesignTimeSiteMapProviderBase._childNodeText3);
			this.AddNode(this._rootNode);
			this.AddNode(siteMapNode, this._rootNode);
			this.AddNode(node, siteMapNode);
			this.AddNode(this._currentNode, siteMapNode);
			this.AddNode(node2, siteMapNode);
			this.AddNode(node3, siteMapNode);
			this.AddNode(node4, this._currentNode);
			this.AddNode(node5, this._currentNode);
			this.AddNode(node6, this._currentNode);
			return this._rootNode;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000212D1 File Offset: 0x0001F4D1
		public override SiteMapNode BuildSiteMap()
		{
			return this.BuildDesignTimeSiteMapInternal();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000212DC File Offset: 0x0001F4DC
		private SiteMapNode CreateNewSiteMapNode(string text)
		{
			string text2 = text + "url";
			return new SiteMapNode(this, text2, text2, text, text);
		}

		// Token: 0x04000387 RID: 903
		private SiteMapNode _rootNode;

		// Token: 0x04000388 RID: 904
		private SiteMapNode _currentNode;

		// Token: 0x04000389 RID: 905
		private static readonly string _rootNodeText = SR.GetString("DesignTimeSiteMapProvider_RootNodeText");

		// Token: 0x0400038A RID: 906
		private static readonly string _parentNodeText = SR.GetString("DesignTimeSiteMapProvider_ParentNodeText");

		// Token: 0x0400038B RID: 907
		private static readonly string _siblingNodeText = SR.GetString("DesignTimeSiteMapProvider_SiblingNodeText");

		// Token: 0x0400038C RID: 908
		private static readonly string _currentNodeText = SR.GetString("DesignTimeSiteMapProvider_CurrentNodeText");

		// Token: 0x0400038D RID: 909
		private static readonly string _childNodeText = SR.GetString("DesignTimeSiteMapProvider_ChildNodeText");

		// Token: 0x0400038E RID: 910
		private static readonly string _siblingNodeText1 = DesignTimeSiteMapProviderBase._siblingNodeText + " 1";

		// Token: 0x0400038F RID: 911
		private static readonly string _siblingNodeText2 = DesignTimeSiteMapProviderBase._siblingNodeText + " 2";

		// Token: 0x04000390 RID: 912
		private static readonly string _siblingNodeText3 = DesignTimeSiteMapProviderBase._siblingNodeText + " 3";

		// Token: 0x04000391 RID: 913
		private static readonly string _childNodeText1 = DesignTimeSiteMapProviderBase._childNodeText + " 1";

		// Token: 0x04000392 RID: 914
		private static readonly string _childNodeText2 = DesignTimeSiteMapProviderBase._childNodeText + " 2";

		// Token: 0x04000393 RID: 915
		private static readonly string _childNodeText3 = DesignTimeSiteMapProviderBase._childNodeText + " 3";

		// Token: 0x04000394 RID: 916
		protected IDesignerHost _host;
	}
}
