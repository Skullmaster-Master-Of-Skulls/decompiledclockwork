using System;
using System.Collections;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000FC RID: 252
	public abstract class StaticSiteMapProvider : SiteMapProvider
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0002B64C File Offset: 0x0002984C
		internal IDictionary ChildNodeCollectionTable
		{
			get
			{
				if (this._childNodeCollectionTable == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._childNodeCollectionTable == null)
						{
							this._childNodeCollectionTable = new Hashtable();
						}
					}
				}
				return this._childNodeCollectionTable;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0002B6A8 File Offset: 0x000298A8
		internal IDictionary KeyTable
		{
			get
			{
				if (this._keyTable == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._keyTable == null)
						{
							this._keyTable = new Hashtable();
						}
					}
				}
				return this._keyTable;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0002B704 File Offset: 0x00029904
		internal IDictionary ParentNodeTable
		{
			get
			{
				if (this._parentNodeTable == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._parentNodeTable == null)
						{
							this._parentNodeTable = new Hashtable();
						}
					}
				}
				return this._parentNodeTable;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0002B760 File Offset: 0x00029960
		internal IDictionary UrlTable
		{
			get
			{
				if (this._urlTable == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._urlTable == null)
						{
							this._urlTable = new Hashtable(StringComparer.OrdinalIgnoreCase);
						}
					}
				}
				return this._urlTable;
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002B7C0 File Offset: 0x000299C0
		protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				bool flag2 = false;
				string text = node.Url;
				if (!string.IsNullOrEmpty(text))
				{
					if (HttpRuntime.AppDomainAppVirtualPath != null)
					{
						if (!UrlPath.IsAbsolutePhysicalPath(text))
						{
							text = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, text);
							text = UrlPath.MakeVirtualPathAppAbsolute(text);
						}
						if (this.UrlTable[text] != null)
						{
							throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Multiple_Nodes_With_Identical_Url", new object[]
							{
								text
							}));
						}
					}
					flag2 = true;
				}
				string key = node.Key;
				if (this.KeyTable.Contains(key))
				{
					throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Multiple_Nodes_With_Identical_Key", new object[]
					{
						key
					}));
				}
				this.KeyTable[key] = node;
				if (flag2)
				{
					this.UrlTable[text] = node;
				}
				if (parentNode != null)
				{
					this.ParentNodeTable[node] = parentNode;
					if (this.ChildNodeCollectionTable[parentNode] == null)
					{
						this.ChildNodeCollectionTable[parentNode] = new SiteMapNodeCollection();
					}
					((SiteMapNodeCollection)this.ChildNodeCollectionTable[parentNode]).Add(node);
				}
			}
		}

		// Token: 0x06000F39 RID: 3897
		public abstract SiteMapNode BuildSiteMap();

		// Token: 0x06000F3A RID: 3898 RVA: 0x0002B8FC File Offset: 0x00029AFC
		protected virtual void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._childNodeCollectionTable != null)
				{
					this._childNodeCollectionTable.Clear();
				}
				if (this._urlTable != null)
				{
					this._urlTable.Clear();
				}
				if (this._parentNodeTable != null)
				{
					this._parentNodeTable.Clear();
				}
				if (this._keyTable != null)
				{
					this._keyTable.Clear();
				}
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002B984 File Offset: 0x00029B84
		public override SiteMapNode FindSiteMapNodeFromKey(string key)
		{
			SiteMapNode siteMapNode = base.FindSiteMapNodeFromKey(key);
			if (siteMapNode == null)
			{
				siteMapNode = (SiteMapNode)this.KeyTable[key];
			}
			return base.ReturnNodeIfAccessible(siteMapNode);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002B9B8 File Offset: 0x00029BB8
		public override SiteMapNode FindSiteMapNode(string rawUrl)
		{
			if (rawUrl == null)
			{
				throw new ArgumentNullException("rawUrl");
			}
			rawUrl = rawUrl.Trim();
			if (rawUrl.Length == 0)
			{
				return null;
			}
			if (UrlPath.IsAppRelativePath(rawUrl))
			{
				rawUrl = UrlPath.MakeVirtualPathAppAbsolute(rawUrl);
			}
			this.BuildSiteMap();
			return base.ReturnNodeIfAccessible((SiteMapNode)this.UrlTable[rawUrl]);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002BA14 File Offset: 0x00029C14
		public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.BuildSiteMap();
			SiteMapNodeCollection siteMapNodeCollection = (SiteMapNodeCollection)this.ChildNodeCollectionTable[node];
			if (siteMapNodeCollection == null)
			{
				SiteMapNode siteMapNode = (SiteMapNode)this.KeyTable[node.Key];
				if (siteMapNode != null)
				{
					siteMapNodeCollection = (SiteMapNodeCollection)this.ChildNodeCollectionTable[siteMapNode];
				}
			}
			if (siteMapNodeCollection == null)
			{
				return SiteMapNodeCollection.Empty;
			}
			if (!base.SecurityTrimmingEnabled)
			{
				return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection);
			}
			HttpContext context = HttpContext.Current;
			SiteMapNodeCollection siteMapNodeCollection2 = new SiteMapNodeCollection(siteMapNodeCollection.Count);
			foreach (object obj in siteMapNodeCollection)
			{
				SiteMapNode siteMapNode2 = (SiteMapNode)obj;
				if (siteMapNode2.IsAccessibleToUser(context))
				{
					siteMapNodeCollection2.Add(siteMapNode2);
				}
			}
			return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection2);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002BB04 File Offset: 0x00029D04
		public override SiteMapNode GetParentNode(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.BuildSiteMap();
			SiteMapNode siteMapNode = (SiteMapNode)this.ParentNodeTable[node];
			if (siteMapNode == null)
			{
				SiteMapNode siteMapNode2 = (SiteMapNode)this.KeyTable[node.Key];
				if (siteMapNode2 != null)
				{
					siteMapNode = (SiteMapNode)this.ParentNodeTable[siteMapNode2];
				}
			}
			if (siteMapNode == null && this.ParentProvider != null)
			{
				siteMapNode = this.ParentProvider.GetParentNode(node);
			}
			return base.ReturnNodeIfAccessible(siteMapNode);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0002BB88 File Offset: 0x00029D88
		protected internal override void RemoveNode(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				SiteMapNode siteMapNode = (SiteMapNode)this.ParentNodeTable[node];
				if (this.ParentNodeTable.Contains(node))
				{
					this.ParentNodeTable.Remove(node);
				}
				if (siteMapNode != null)
				{
					SiteMapNodeCollection siteMapNodeCollection = (SiteMapNodeCollection)this.ChildNodeCollectionTable[siteMapNode];
					if (siteMapNodeCollection != null && siteMapNodeCollection.Contains(node))
					{
						siteMapNodeCollection.Remove(node);
					}
				}
				string url = node.Url;
				if (url != null && url.Length > 0 && this.UrlTable.Contains(url))
				{
					this.UrlTable.Remove(url);
				}
				string key = node.Key;
				if (this.KeyTable.Contains(key))
				{
					this.KeyTable.Remove(key);
				}
			}
		}

		// Token: 0x040005D2 RID: 1490
		private Hashtable _childNodeCollectionTable;

		// Token: 0x040005D3 RID: 1491
		private Hashtable _keyTable;

		// Token: 0x040005D4 RID: 1492
		private Hashtable _parentNodeTable;

		// Token: 0x040005D5 RID: 1493
		private Hashtable _urlTable;
	}
}
