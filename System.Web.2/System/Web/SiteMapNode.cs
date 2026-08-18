using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Resources;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000F6 RID: 246
	public class SiteMapNode : ICloneable, IHierarchyData, INavigateUIData
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x00029988 File Offset: 0x00027B88
		public SiteMapNode(SiteMapProvider provider, string key) : this(provider, key, null, null, null, null, null, null, null)
		{
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000299A4 File Offset: 0x00027BA4
		public SiteMapNode(SiteMapProvider provider, string key, string url) : this(provider, key, url, null, null, null, null, null, null)
		{
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000299C0 File Offset: 0x00027BC0
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title) : this(provider, key, url, title, null, null, null, null, null)
		{
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000299E0 File Offset: 0x00027BE0
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description) : this(provider, key, url, title, description, null, null, null, null)
		{
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00029A00 File Offset: 0x00027C00
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes, NameValueCollection explicitResourceKeys, string implicitResourceKey)
		{
			this._provider = provider;
			this._title = title;
			this._description = description;
			this._roles = roles;
			this._attributes = attributes;
			this._key = key;
			this._resourceKeys = explicitResourceKeys;
			this._resourceKey = implicitResourceKey;
			if (url != null)
			{
				this._url = url.Trim();
			}
			this._virtualPath = this.CreateVirtualPathFromUrl(this._url);
			if (this._key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this._provider == null)
			{
				throw new ArgumentNullException("provider");
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00029A98 File Offset: 0x00027C98
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00029AA0 File Offset: 0x00027CA0
		protected NameValueCollection Attributes
		{
			get
			{
				return this._attributes;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"Attributes"
					}));
				}
				this._attributes = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		public virtual string this[string key]
		{
			get
			{
				string text = null;
				if (this._attributes != null)
				{
					text = this._attributes[key];
				}
				if (this._provider.EnableLocalization)
				{
					string text2 = this.GetImplicitResourceString(key);
					if (text2 != null)
					{
						return text2;
					}
					text2 = this.GetExplicitResourceString(key, text, true);
					if (text2 != null)
					{
						return text2;
					}
				}
				return text;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"Item"
					}));
				}
				if (this._attributes == null)
				{
					this._attributes = new NameValueCollection();
				}
				this._attributes[key] = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00029B73 File Offset: 0x00027D73
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00029B90 File Offset: 0x00027D90
		public virtual SiteMapNodeCollection ChildNodes
		{
			get
			{
				if (this._childNodesSet)
				{
					return this._childNodes;
				}
				return this._provider.GetChildNodes(this);
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"ChildNodes"
					}));
				}
				this._childNodes = value;
				this._childNodesSet = true;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00029BC8 File Offset: 0x00027DC8
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00029C1F File Offset: 0x00027E1F
		[Localizable(true)]
		public virtual string Description
		{
			get
			{
				if (this._provider.EnableLocalization)
				{
					string text = this.GetImplicitResourceString("description");
					if (text != null)
					{
						return text;
					}
					text = this.GetExplicitResourceString("description", this._description, true);
					if (text != null)
					{
						return text;
					}
				}
				if (this._description != null)
				{
					return this._description;
				}
				return string.Empty;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"Description"
					}));
				}
				this._description = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00029C4E File Offset: 0x00027E4E
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00029C58 File Offset: 0x00027E58
		public virtual bool HasChildNodes
		{
			get
			{
				IList childNodes = this.ChildNodes;
				return childNodes != null && childNodes.Count > 0;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00029C7C File Offset: 0x00027E7C
		public virtual SiteMapNode NextSibling
		{
			get
			{
				IList siblingNodes = this.SiblingNodes;
				if (siblingNodes == null)
				{
					return null;
				}
				int num = siblingNodes.IndexOf(this);
				if (num >= 0 && num < siblingNodes.Count - 1)
				{
					return (SiteMapNode)siblingNodes[num + 1];
				}
				return null;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00029CBC File Offset: 0x00027EBC
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00029CD9 File Offset: 0x00027ED9
		public virtual SiteMapNode ParentNode
		{
			get
			{
				if (this._parentNodeSet)
				{
					return this._parentNode;
				}
				return this._provider.GetParentNode(this);
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"ParentNode"
					}));
				}
				this._parentNode = value;
				this._parentNodeSet = true;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00029D10 File Offset: 0x00027F10
		public virtual SiteMapNode PreviousSibling
		{
			get
			{
				IList siblingNodes = this.SiblingNodes;
				if (siblingNodes == null)
				{
					return null;
				}
				int num = siblingNodes.IndexOf(this);
				if (num > 0 && num <= siblingNodes.Count - 1)
				{
					return (SiteMapNode)siblingNodes[num - 1];
				}
				return null;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00029D50 File Offset: 0x00027F50
		public SiteMapProvider Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00029D58 File Offset: 0x00027F58
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00029D60 File Offset: 0x00027F60
		public bool ReadOnly
		{
			get
			{
				return this._readonly;
			}
			set
			{
				this._readonly = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00029D69 File Offset: 0x00027F69
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00029D71 File Offset: 0x00027F71
		public string ResourceKey
		{
			get
			{
				return this._resourceKey;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"ResourceKey"
					}));
				}
				this._resourceKey = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00029DA0 File Offset: 0x00027FA0
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00029DA8 File Offset: 0x00027FA8
		public IList Roles
		{
			get
			{
				return this._roles;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"Roles"
					}));
				}
				this._roles = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00029DD8 File Offset: 0x00027FD8
		public virtual SiteMapNode RootNode
		{
			get
			{
				SiteMapNode rootNode = this._provider.RootProvider.RootNode;
				if (rootNode == null)
				{
					string name = this._provider.RootProvider.Name;
					throw new InvalidOperationException(SR.GetString("SiteMapProvider_Invalid_RootNode", new object[]
					{
						name
					}));
				}
				return rootNode;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00029E28 File Offset: 0x00028028
		private SiteMapNodeCollection SiblingNodes
		{
			get
			{
				SiteMapNode parentNode = this.ParentNode;
				if (parentNode != null)
				{
					return parentNode.ChildNodes;
				}
				return null;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00029E48 File Offset: 0x00028048
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x00029E9F File Offset: 0x0002809F
		[Localizable(true)]
		public virtual string Title
		{
			get
			{
				if (this._provider.EnableLocalization)
				{
					string text = this.GetImplicitResourceString("title");
					if (text != null)
					{
						return text;
					}
					text = this.GetExplicitResourceString("title", this._title, true);
					if (text != null)
					{
						return text;
					}
				}
				if (this._title != null)
				{
					return this._title;
				}
				return string.Empty;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"Title"
					}));
				}
				this._title = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00029ECE File Offset: 0x000280CE
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x00029EE4 File Offset: 0x000280E4
		public virtual string Url
		{
			get
			{
				if (this._url != null)
				{
					return this._url;
				}
				return string.Empty;
			}
			set
			{
				if (this._readonly)
				{
					throw new InvalidOperationException(SR.GetString("SiteMapNode_readonly", new object[]
					{
						"Url"
					}));
				}
				if (value != null)
				{
					this._url = value.Trim();
				}
				this._virtualPath = this.CreateVirtualPathFromUrl(this._url);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00029F38 File Offset: 0x00028138
		internal VirtualPath VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00029F40 File Offset: 0x00028140
		private VirtualPath CreateVirtualPathFromUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return null;
			}
			if (!UrlPath.IsValidVirtualPathWithoutProtocol(url))
			{
				return null;
			}
			if (UrlPath.IsAbsolutePhysicalPath(url))
			{
				return null;
			}
			if (HttpRuntime.AppDomainAppVirtualPath == null)
			{
				return null;
			}
			if (UrlPath.IsRelativeUrl(url) && !UrlPath.IsAppRelativePath(url))
			{
				url = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, url);
			}
			int num = url.IndexOf('?');
			if (num != -1)
			{
				url = url.Substring(0, num);
			}
			return VirtualPath.Create(url, VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00029FB0 File Offset: 0x000281B0
		public virtual SiteMapNode Clone()
		{
			ArrayList roles = null;
			NameValueCollection attributes = null;
			NameValueCollection explicitResourceKeys = null;
			if (this._roles != null)
			{
				roles = new ArrayList(this._roles);
			}
			if (this._attributes != null)
			{
				attributes = new NameValueCollection(this._attributes);
			}
			if (this._resourceKeys != null)
			{
				explicitResourceKeys = new NameValueCollection(this._resourceKeys);
			}
			return new SiteMapNode(this._provider, this.Key, this.Url, this.Title, this.Description, roles, attributes, explicitResourceKeys, this._resourceKey);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002A030 File Offset: 0x00028230
		public virtual SiteMapNode Clone(bool cloneParentNodes)
		{
			SiteMapNode siteMapNode = this.Clone();
			if (cloneParentNodes)
			{
				SiteMapNode siteMapNode2 = siteMapNode;
				SiteMapNode parentNode = this.ParentNode;
				while (parentNode != null)
				{
					SiteMapNode siteMapNode3 = parentNode.Clone();
					siteMapNode2.ParentNode = siteMapNode3;
					siteMapNode3.ChildNodes = new SiteMapNodeCollection(siteMapNode2);
					parentNode = parentNode.ParentNode;
					siteMapNode2 = siteMapNode3;
				}
			}
			return siteMapNode;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0002A07C File Offset: 0x0002827C
		public override bool Equals(object obj)
		{
			SiteMapNode siteMapNode = obj as SiteMapNode;
			return siteMapNode != null && this._key == siteMapNode.Key && string.Equals(this._url, siteMapNode._url, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002A0BC File Offset: 0x000282BC
		public SiteMapNodeCollection GetAllNodes()
		{
			SiteMapNodeCollection collection = new SiteMapNodeCollection();
			this.GetAllNodesRecursive(collection);
			return SiteMapNodeCollection.ReadOnly(collection);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002A0DC File Offset: 0x000282DC
		private void GetAllNodesRecursive(SiteMapNodeCollection collection)
		{
			SiteMapNodeCollection childNodes = this.ChildNodes;
			if (childNodes != null && childNodes.Count > 0)
			{
				collection.AddRange(childNodes);
				foreach (object obj in childNodes)
				{
					SiteMapNode siteMapNode = (SiteMapNode)obj;
					siteMapNode.GetAllNodesRecursive(collection);
				}
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0002A14C File Offset: 0x0002834C
		public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
		{
			return new SiteMapDataSourceView(owner, viewName, this);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0002A156 File Offset: 0x00028356
		public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
		{
			return new SiteMapHierarchicalDataSourceView(this);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0002A160 File Offset: 0x00028360
		protected string GetExplicitResourceString(string attributeName, string defaultValue, bool throwIfNotFound)
		{
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			string text = null;
			if (this._resourceKeys != null)
			{
				string[] values = this._resourceKeys.GetValues(attributeName);
				if (values != null && values.Length > 1)
				{
					try
					{
						text = (ResourceExpressionBuilder.GetGlobalResourceObject(values[0], values[1]) as string);
					}
					catch (MissingManifestResourceException)
					{
						if (defaultValue != null)
						{
							return defaultValue;
						}
					}
					if (text == null && throwIfNotFound)
					{
						throw new InvalidOperationException(SR.GetString("Res_not_found_with_class_and_key", new object[]
						{
							values[0],
							values[1]
						}));
					}
					return text;
				}
			}
			return text;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002A1F8 File Offset: 0x000283F8
		public override int GetHashCode()
		{
			return this._key.GetHashCode();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002A208 File Offset: 0x00028408
		protected string GetImplicitResourceString(string attributeName)
		{
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			string result = null;
			if (!string.IsNullOrEmpty(this._resourceKey))
			{
				try
				{
					result = (ResourceExpressionBuilder.GetGlobalResourceObject(this.Provider.ResourceKey, this.ResourceKey + "." + attributeName) as string);
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002A270 File Offset: 0x00028470
		public virtual bool IsAccessibleToUser(HttpContext context)
		{
			return this._provider.IsAccessibleToUser(context, this);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002A280 File Offset: 0x00028480
		public virtual bool IsDescendantOf(SiteMapNode node)
		{
			for (SiteMapNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				if (parentNode.Equals(node))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002A2AC File Offset: 0x000284AC
		public override string ToString()
		{
			return this.Title;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002A2B4 File Offset: 0x000284B4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0002A2BC File Offset: 0x000284BC
		bool IHierarchyData.HasChildren
		{
			get
			{
				return this.HasChildNodes;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00004335 File Offset: 0x00002535
		object IHierarchyData.Item
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0002A2C4 File Offset: 0x000284C4
		string IHierarchyData.Path
		{
			get
			{
				return this.Key;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0002A2CC File Offset: 0x000284CC
		string IHierarchyData.Type
		{
			get
			{
				return SiteMapNode._siteMapNodeType;
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002A2D3 File Offset: 0x000284D3
		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return this.ChildNodes;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002A2DC File Offset: 0x000284DC
		IHierarchyData IHierarchyData.GetParent()
		{
			SiteMapNode parentNode = this.ParentNode;
			if (parentNode == null)
			{
				return null;
			}
			return parentNode;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0002A2F6 File Offset: 0x000284F6
		string INavigateUIData.Description
		{
			get
			{
				return this.Description;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0002A2AC File Offset: 0x000284AC
		string INavigateUIData.Name
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002A2FE File Offset: 0x000284FE
		string INavigateUIData.NavigateUrl
		{
			get
			{
				return this.Url;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0002A2AC File Offset: 0x000284AC
		string INavigateUIData.Value
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x040005A9 RID: 1449
		private static readonly string _siteMapNodeType = typeof(SiteMapNode).Name;

		// Token: 0x040005AA RID: 1450
		private SiteMapProvider _provider;

		// Token: 0x040005AB RID: 1451
		private bool _readonly;

		// Token: 0x040005AC RID: 1452
		private bool _parentNodeSet;

		// Token: 0x040005AD RID: 1453
		private bool _childNodesSet;

		// Token: 0x040005AE RID: 1454
		private VirtualPath _virtualPath;

		// Token: 0x040005AF RID: 1455
		private string _title;

		// Token: 0x040005B0 RID: 1456
		private string _description;

		// Token: 0x040005B1 RID: 1457
		private string _url;

		// Token: 0x040005B2 RID: 1458
		private string _key;

		// Token: 0x040005B3 RID: 1459
		private string _resourceKey;

		// Token: 0x040005B4 RID: 1460
		private IList _roles;

		// Token: 0x040005B5 RID: 1461
		private NameValueCollection _attributes;

		// Token: 0x040005B6 RID: 1462
		private NameValueCollection _resourceKeys;

		// Token: 0x040005B7 RID: 1463
		private SiteMapNode _parentNode;

		// Token: 0x040005B8 RID: 1464
		private SiteMapNodeCollection _childNodes;
	}
}
