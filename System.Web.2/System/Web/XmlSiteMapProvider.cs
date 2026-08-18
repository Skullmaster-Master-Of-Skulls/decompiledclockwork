using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;
using System.Xml;

namespace System.Web
{
	// Token: 0x02000111 RID: 273
	public class XmlSiteMapProvider : StaticSiteMapProvider, IDisposable
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		private ArrayList ChildProviderList
		{
			get
			{
				ArrayList arrayList = this._childProviderList;
				if (arrayList == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._childProviderList == null)
						{
							arrayList = ArrayList.ReadOnly(new ArrayList(this.ChildProviderTable.Keys));
							this._childProviderList = arrayList;
						}
						else
						{
							arrayList = this._childProviderList;
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0002EF64 File Offset: 0x0002D164
		private Hashtable ChildProviderTable
		{
			get
			{
				if (this._childProviderTable == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._childProviderTable == null)
						{
							this._childProviderTable = new Hashtable();
						}
					}
				}
				return this._childProviderTable;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0002EFC0 File Offset: 0x0002D1C0
		public override SiteMapNode RootNode
		{
			get
			{
				this.BuildSiteMap();
				SiteMapNode node = base.ReturnNodeIfAccessible(this._siteMapNode);
				return XmlSiteMapProvider.ApplyModifierIfExists(node);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0002EFE7 File Offset: 0x0002D1E7
		public override SiteMapNode CurrentNode
		{
			get
			{
				return XmlSiteMapProvider.ApplyModifierIfExists(base.CurrentNode);
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0002EFF4 File Offset: 0x0002D1F4
		public override SiteMapNode GetParentNode(SiteMapNode node)
		{
			SiteMapNode parentNode = base.GetParentNode(node);
			return XmlSiteMapProvider.ApplyModifierIfExists(parentNode);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0002F010 File Offset: 0x0002D210
		public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
		{
			SiteMapNodeCollection childNodes = base.GetChildNodes(node);
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null || !httpContext.Response.UsePathModifier || childNodes.Count == 0)
			{
				return childNodes;
			}
			SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection(childNodes.Count);
			foreach (object obj in childNodes)
			{
				SiteMapNode node2 = (SiteMapNode)obj;
				siteMapNodeCollection.Add(XmlSiteMapProvider.ApplyModifierIfExists(node2));
			}
			return siteMapNodeCollection;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0002F0A4 File Offset: 0x0002D2A4
		protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (parentNode == null)
			{
				throw new ArgumentNullException("parentNode");
			}
			SiteMapProvider provider = node.Provider;
			SiteMapProvider provider2 = parentNode.Provider;
			if (provider != this)
			{
				throw new ArgumentException(SR.GetString("XmlSiteMapProvider_cannot_add_node", new object[]
				{
					node.ToString()
				}), "node");
			}
			if (provider2 != this)
			{
				throw new ArgumentException(SR.GetString("XmlSiteMapProvider_cannot_add_node", new object[]
				{
					parentNode.ToString()
				}), "parentNode");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this.RemoveNode(node);
				this.AddNodeInternal(node, parentNode, null);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0002F168 File Offset: 0x0002D368
		private void AddNodeInternal(SiteMapNode node, SiteMapNode parentNode, XmlNode xmlNode)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				string url = node.Url;
				string key = node.Key;
				bool flag2 = false;
				if (!string.IsNullOrEmpty(url))
				{
					if (base.UrlTable[url] != null)
					{
						if (xmlNode != null)
						{
							throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Multiple_Nodes_With_Identical_Url", new object[]
							{
								url
							}), xmlNode);
						}
						throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Multiple_Nodes_With_Identical_Url", new object[]
						{
							url
						}));
					}
					else
					{
						flag2 = true;
					}
				}
				if (base.KeyTable.Contains(key))
				{
					if (xmlNode != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Multiple_Nodes_With_Identical_Key", new object[]
						{
							key
						}), xmlNode);
					}
					throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Multiple_Nodes_With_Identical_Key", new object[]
					{
						key
					}));
				}
				else
				{
					if (flag2)
					{
						base.UrlTable[url] = node;
					}
					base.KeyTable[key] = node;
					if (parentNode != null)
					{
						base.ParentNodeTable[node] = parentNode;
						if (base.ChildNodeCollectionTable[parentNode] == null)
						{
							base.ChildNodeCollectionTable[parentNode] = new SiteMapNodeCollection();
						}
						((SiteMapNodeCollection)base.ChildNodeCollectionTable[parentNode]).Add(node);
					}
				}
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected virtual void AddProvider(string providerName, SiteMapNode parentNode)
		{
			if (parentNode == null)
			{
				throw new ArgumentNullException("parentNode");
			}
			if (parentNode.Provider != this)
			{
				throw new ArgumentException(SR.GetString("XmlSiteMapProvider_cannot_add_node", new object[]
				{
					parentNode.ToString()
				}), "parentNode");
			}
			SiteMapNode nodeFromProvider = this.GetNodeFromProvider(providerName);
			this.AddNodeInternal(nodeFromProvider, parentNode, null);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0002F318 File Offset: 0x0002D518
		public override SiteMapNode BuildSiteMap()
		{
			SiteMapNode siteMapNode = this._siteMapNode;
			if (siteMapNode != null)
			{
				return siteMapNode;
			}
			XmlDocument configDocument = this.GetConfigDocument();
			object @lock = this._lock;
			SiteMapNode siteMapNode2;
			lock (@lock)
			{
				if (this._siteMapNode != null)
				{
					siteMapNode2 = this._siteMapNode;
				}
				else
				{
					this.Clear();
					this.CheckSiteMapFileExists();
					try
					{
						using (Stream stream = this._normalizedVirtualPath.OpenFile())
						{
							XmlReader reader = new XmlTextReader(stream);
							configDocument.Load(reader);
						}
					}
					catch (XmlException ex)
					{
						string filename = this._virtualPath.VirtualPathString;
						string text = this._normalizedVirtualPath.MapPathInternal();
						if (text != null && HttpRuntime.HasPathDiscoveryPermission(text))
						{
							filename = text;
						}
						throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Error_loading_Config_file", new object[]
						{
							this._virtualPath,
							ex.Message
						}), ex, filename, ex.LineNumber);
					}
					catch (Exception ex2)
					{
						throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Error_loading_Config_file", new object[]
						{
							this._virtualPath,
							ex2.Message
						}), ex2);
					}
					XmlNode xmlNode = null;
					foreach (object obj in configDocument.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (string.Equals(xmlNode2.Name, "siteMap", StringComparison.Ordinal))
						{
							xmlNode = xmlNode2;
							break;
						}
					}
					if (xmlNode == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Top_Element_Must_Be_SiteMap"), configDocument);
					}
					bool enableLocalization = false;
					HandlerBase.GetAndRemoveBooleanAttribute(xmlNode, "enableLocalization", ref enableLocalization);
					base.EnableLocalization = enableLocalization;
					XmlNode xmlNode3 = null;
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode4 = (XmlNode)obj2;
						if (xmlNode4.NodeType == XmlNodeType.Element)
						{
							if (!"siteMapNode".Equals(xmlNode4.Name))
							{
								throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Only_SiteMapNode_Allowed"), xmlNode4);
							}
							if (xmlNode3 != null)
							{
								throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Only_One_SiteMapNode_Required_At_Top"), xmlNode4);
							}
							xmlNode3 = xmlNode4;
						}
					}
					if (xmlNode3 == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Only_One_SiteMapNode_Required_At_Top"), xmlNode);
					}
					Queue queue = new Queue(50);
					queue.Enqueue(null);
					queue.Enqueue(xmlNode3);
					this._siteMapNode = this.ConvertFromXmlNode(queue);
					siteMapNode2 = this._siteMapNode;
				}
			}
			return siteMapNode2;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0002F624 File Offset: 0x0002D824
		private void CheckSiteMapFileExists()
		{
			if (!Util.VirtualFileExistsWithAssert(this._normalizedVirtualPath))
			{
				throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_FileName_does_not_exist", new object[]
				{
					this._virtualPath
				}));
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0002F654 File Offset: 0x0002D854
		protected override void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.ChildProviderTable.Clear();
				this._siteMapNode = null;
				this._childProviderList = null;
				base.Clear();
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
		private SiteMapNode ConvertFromXmlNode(Queue queue)
		{
			SiteMapNode siteMapNode = null;
			while (queue.Count != 0)
			{
				SiteMapNode parentNode = (SiteMapNode)queue.Dequeue();
				XmlNode xmlNode = (XmlNode)queue.Dequeue();
				if (!"siteMapNode".Equals(xmlNode.Name))
				{
					throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_Only_SiteMapNode_Allowed"), xmlNode);
				}
				string text = null;
				HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "provider", ref text);
				SiteMapNode siteMapNode2;
				if (text != null)
				{
					siteMapNode2 = this.GetNodeFromProvider(text);
					HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
					HandlerBase.CheckForNonCommentChildNodes(xmlNode);
				}
				else
				{
					string text2 = null;
					HandlerBase.GetAndRemoveNonEmptyStringAttribute(xmlNode, "siteMapFile", ref text2);
					if (text2 != null)
					{
						siteMapNode2 = this.GetNodeFromSiteMapFile(xmlNode, VirtualPath.Create(text2));
					}
					else
					{
						siteMapNode2 = this.GetNodeFromXmlNode(xmlNode, queue);
					}
				}
				this.AddNodeInternal(siteMapNode2, parentNode, xmlNode);
				if (siteMapNode == null)
				{
					siteMapNode = siteMapNode2;
				}
			}
			return siteMapNode;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0002F776 File Offset: 0x0002D976
		protected virtual void Dispose(bool disposing)
		{
			if (this._handler != null)
			{
				HttpRuntime.FileChangesMonitor.StopMonitoringFile(this._filename, this._handler);
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0002F796 File Offset: 0x0002D996
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
		private void EnsureChildSiteMapProviderUpToDate(SiteMapProvider childProvider)
		{
			SiteMapNode siteMapNode = (SiteMapNode)this.ChildProviderTable[childProvider];
			SiteMapNode rootNodeCore = childProvider.GetRootNodeCore();
			if (rootNodeCore == null)
			{
				throw new ProviderException(SR.GetString("XmlSiteMapProvider_invalid_sitemapnode_returned", new object[]
				{
					childProvider.Name
				}));
			}
			if (!siteMapNode.Equals(rootNodeCore))
			{
				if (siteMapNode == null)
				{
					return;
				}
				object @lock = this._lock;
				lock (@lock)
				{
					siteMapNode = (SiteMapNode)this.ChildProviderTable[childProvider];
					if (siteMapNode != null)
					{
						rootNodeCore = childProvider.GetRootNodeCore();
						if (rootNodeCore == null)
						{
							throw new ProviderException(SR.GetString("XmlSiteMapProvider_invalid_sitemapnode_returned", new object[]
							{
								childProvider.Name
							}));
						}
						if (!siteMapNode.Equals(rootNodeCore))
						{
							if (this._siteMapNode.Equals(siteMapNode))
							{
								base.UrlTable.Remove(siteMapNode.Url);
								base.KeyTable.Remove(siteMapNode.Key);
								base.UrlTable.Add(rootNodeCore.Url, rootNodeCore);
								base.KeyTable.Add(rootNodeCore.Key, rootNodeCore);
								this._siteMapNode = rootNodeCore;
							}
							SiteMapNode siteMapNode2 = (SiteMapNode)base.ParentNodeTable[siteMapNode];
							if (siteMapNode2 != null)
							{
								SiteMapNodeCollection siteMapNodeCollection = (SiteMapNodeCollection)base.ChildNodeCollectionTable[siteMapNode2];
								int num = siteMapNodeCollection.IndexOf(siteMapNode);
								if (num != -1)
								{
									siteMapNodeCollection.Remove(siteMapNode);
									siteMapNodeCollection.Insert(num, rootNodeCore);
								}
								else
								{
									siteMapNodeCollection.Add(rootNodeCore);
								}
								base.ParentNodeTable[rootNodeCore] = siteMapNode2;
								base.ParentNodeTable.Remove(siteMapNode);
								base.UrlTable.Remove(siteMapNode.Url);
								base.KeyTable.Remove(siteMapNode.Key);
								base.UrlTable.Add(rootNodeCore.Url, rootNodeCore);
								base.KeyTable.Add(rootNodeCore.Key, rootNodeCore);
							}
							else
							{
								XmlSiteMapProvider xmlSiteMapProvider = this.ParentProvider as XmlSiteMapProvider;
								if (xmlSiteMapProvider != null)
								{
									xmlSiteMapProvider.EnsureChildSiteMapProviderUpToDate(this);
								}
							}
							this.ChildProviderTable[childProvider] = rootNodeCore;
							this._childProviderList = null;
						}
					}
				}
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0002F9CC File Offset: 0x0002DBCC
		public override SiteMapNode FindSiteMapNode(string rawUrl)
		{
			SiteMapNode siteMapNode = base.FindSiteMapNode(rawUrl);
			if (siteMapNode == null)
			{
				foreach (object obj in this.ChildProviderList)
				{
					SiteMapProvider siteMapProvider = (SiteMapProvider)obj;
					this.EnsureChildSiteMapProviderUpToDate(siteMapProvider);
					siteMapNode = siteMapProvider.FindSiteMapNode(rawUrl);
					if (siteMapNode != null)
					{
						return siteMapNode;
					}
				}
				return siteMapNode;
			}
			return siteMapNode;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0002FA48 File Offset: 0x0002DC48
		public override SiteMapNode FindSiteMapNodeFromKey(string key)
		{
			SiteMapNode siteMapNode = base.FindSiteMapNodeFromKey(key);
			if (siteMapNode == null)
			{
				foreach (object obj in this.ChildProviderList)
				{
					SiteMapProvider siteMapProvider = (SiteMapProvider)obj;
					this.EnsureChildSiteMapProviderUpToDate(siteMapProvider);
					siteMapNode = siteMapProvider.FindSiteMapNodeFromKey(key);
					if (siteMapNode != null)
					{
						return siteMapNode;
					}
				}
				return siteMapNode;
			}
			return siteMapNode;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		private XmlDocument GetConfigDocument()
		{
			if (this._document != null)
			{
				return this._document;
			}
			if (!this._initialized)
			{
				throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Not_Initialized"));
			}
			if (this._virtualPath == null)
			{
				throw new ArgumentException(SR.GetString("XmlSiteMapProvider_missing_siteMapFile", new object[]
				{
					"siteMapFile"
				}));
			}
			if (!this._virtualPath.Extension.Equals(".sitemap", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Invalid_Extension", new object[]
				{
					this._virtualPath
				}));
			}
			this._normalizedVirtualPath = this._virtualPath.CombineWithAppRoot();
			this._normalizedVirtualPath.FailIfNotWithinAppRoot();
			this.CheckSiteMapFileExists();
			this._parentSiteMapFileCollection = new StringCollection();
			XmlSiteMapProvider xmlSiteMapProvider = this.ParentProvider as XmlSiteMapProvider;
			if (xmlSiteMapProvider != null && xmlSiteMapProvider._parentSiteMapFileCollection != null)
			{
				if (xmlSiteMapProvider._parentSiteMapFileCollection.Contains(this._normalizedVirtualPath.VirtualPathString))
				{
					throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_FileName_already_in_use", new object[]
					{
						this._virtualPath
					}));
				}
				foreach (string value in xmlSiteMapProvider._parentSiteMapFileCollection)
				{
					this._parentSiteMapFileCollection.Add(value);
				}
			}
			this._parentSiteMapFileCollection.Add(this._normalizedVirtualPath.VirtualPathString);
			this._filename = HostingEnvironment.MapPathInternal(this._normalizedVirtualPath);
			if (!string.IsNullOrEmpty(this._filename))
			{
				this._handler = new FileChangeEventHandler(this.OnConfigFileChange);
				HttpRuntime.FileChangesMonitor.StartMonitoringFile(this._filename, this._handler);
				base.ResourceKey = new FileInfo(this._filename).Name;
			}
			this._document = new ConfigXmlDocument();
			return this._document;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0002FCA8 File Offset: 0x0002DEA8
		private SiteMapNode GetNodeFromProvider(string providerName)
		{
			SiteMapProvider providerFromName = this.GetProviderFromName(providerName);
			if (providerFromName is XmlSiteMapProvider)
			{
				XmlSiteMapProvider xmlSiteMapProvider = (XmlSiteMapProvider)providerFromName;
				StringCollection stringCollection = new StringCollection();
				if (this._parentSiteMapFileCollection != null)
				{
					foreach (string value in this._parentSiteMapFileCollection)
					{
						stringCollection.Add(value);
					}
				}
				xmlSiteMapProvider.BuildSiteMap();
				stringCollection.Add(this._normalizedVirtualPath.VirtualPathString);
				if (stringCollection.Contains(VirtualPath.GetVirtualPathString(xmlSiteMapProvider._normalizedVirtualPath)))
				{
					throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_FileName_already_in_use", new object[]
					{
						xmlSiteMapProvider._virtualPath
					}));
				}
				xmlSiteMapProvider._parentSiteMapFileCollection = stringCollection;
			}
			SiteMapNode rootNodeCore = providerFromName.GetRootNodeCore();
			if (rootNodeCore == null)
			{
				throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_invalid_GetRootNodeCore", new object[]
				{
					providerFromName.Name
				}));
			}
			this.ChildProviderTable.Add(providerFromName, rootNodeCore);
			this._childProviderList = null;
			providerFromName.ParentProvider = this;
			return rootNodeCore;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0002FDC8 File Offset: 0x0002DFC8
		private SiteMapNode GetNodeFromSiteMapFile(XmlNode xmlNode, VirtualPath siteMapFile)
		{
			bool securityTrimmingEnabled = base.SecurityTrimmingEnabled;
			HandlerBase.GetAndRemoveBooleanAttribute(xmlNode, "securityTrimmingEnabled", ref securityTrimmingEnabled);
			HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
			HandlerBase.CheckForNonCommentChildNodes(xmlNode);
			XmlSiteMapProvider xmlSiteMapProvider = new XmlSiteMapProvider();
			siteMapFile = this._normalizedVirtualPath.Parent.Combine(siteMapFile);
			xmlSiteMapProvider.ParentProvider = this;
			xmlSiteMapProvider.Initialize(siteMapFile, securityTrimmingEnabled);
			xmlSiteMapProvider.BuildSiteMap();
			SiteMapNode siteMapNode = xmlSiteMapProvider._siteMapNode;
			this.ChildProviderTable.Add(xmlSiteMapProvider, siteMapNode);
			this._childProviderList = null;
			return siteMapNode;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0002FE44 File Offset: 0x0002E044
		private void HandleResourceAttribute(XmlNode xmlNode, ref NameValueCollection collection, string attrName, ref string text, bool allowImplicitResource)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			string text2 = text.TrimStart(new char[]
			{
				' '
			});
			if (text2 != null && text2.Length > 10 && text2.ToLower(CultureInfo.InvariantCulture).StartsWith("$resources:", StringComparison.Ordinal))
			{
				if (!allowImplicitResource)
				{
					throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_multiple_resource_definition", new object[]
					{
						attrName
					}), xmlNode);
				}
				string text3 = text2.Substring(11);
				if (text3.Length == 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_resourceKey_cannot_be_empty"), xmlNode);
				}
				int num = text3.IndexOf(',');
				if (num == -1)
				{
					throw new ConfigurationErrorsException(SR.GetString("XmlSiteMapProvider_invalid_resource_key", new object[]
					{
						text3
					}), xmlNode);
				}
				string text4 = text3.Substring(0, num);
				string text5 = text3.Substring(num + 1);
				int num2 = text5.IndexOf(',');
				if (num2 != -1)
				{
					text = text5.Substring(num2 + 1);
					text5 = text5.Substring(0, num2);
				}
				else
				{
					text = null;
				}
				if (collection == null)
				{
					collection = new NameValueCollection();
				}
				collection.Add(attrName, text4.Trim());
				collection.Add(attrName, text5.Trim());
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0002FF78 File Offset: 0x0002E178
		private SiteMapNode GetNodeFromXmlNode(XmlNode xmlNode, Queue queue)
		{
			SiteMapNode siteMapNode = null;
			string title = null;
			string text = null;
			string description = null;
			string text2 = null;
			string text3 = null;
			HandlerBase.GetAndRemoveStringAttribute(xmlNode, "url", ref text);
			HandlerBase.GetAndRemoveStringAttribute(xmlNode, "title", ref title);
			HandlerBase.GetAndRemoveStringAttribute(xmlNode, "description", ref description);
			HandlerBase.GetAndRemoveStringAttribute(xmlNode, "roles", ref text2);
			HandlerBase.GetAndRemoveStringAttribute(xmlNode, "resourceKey", ref text3);
			if (!string.IsNullOrEmpty(text3) && !this.ValidateResource(base.ResourceKey, text3 + ".title"))
			{
				text3 = null;
			}
			HandlerBase.CheckForbiddenAttribute(xmlNode, "securityTrimmingEnabled");
			NameValueCollection explicitResourceKeys = null;
			bool allowImplicitResource = string.IsNullOrEmpty(text3);
			this.HandleResourceAttribute(xmlNode, ref explicitResourceKeys, "title", ref title, allowImplicitResource);
			this.HandleResourceAttribute(xmlNode, ref explicitResourceKeys, "description", ref description, allowImplicitResource);
			ArrayList arrayList = new ArrayList();
			if (text2 != null)
			{
				int num = text2.IndexOf('?');
				if (num != -1)
				{
					throw new ConfigurationErrorsException(SR.GetString("Auth_rule_names_cant_contain_char", new object[]
					{
						text2[num].ToString(CultureInfo.InvariantCulture)
					}), xmlNode);
				}
				foreach (string text4 in text2.Split(XmlSiteMapProvider._seperators))
				{
					string text5 = text4.Trim();
					if (text5.Length > 0)
					{
						arrayList.Add(text5);
					}
				}
			}
			arrayList = ArrayList.ReadOnly(arrayList);
			string key = null;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				if (!UrlPath.IsAbsolutePhysicalPath(text) && UrlPath.IsRelativeUrl(text))
				{
					text = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, text);
				}
				string b = HttpUtility.UrlDecode(text);
				if (!string.Equals(text, b, StringComparison.Ordinal))
				{
					throw new ConfigurationErrorsException(SR.GetString("Property_Had_Malformed_Url", new object[]
					{
						"url",
						text
					}), xmlNode);
				}
				key = text.ToLowerInvariant();
			}
			else
			{
				key = Guid.NewGuid().ToString();
			}
			XmlSiteMapProvider.ReadOnlyNameValueCollection readOnlyNameValueCollection = new XmlSiteMapProvider.ReadOnlyNameValueCollection();
			readOnlyNameValueCollection.SetReadOnly(false);
			foreach (object obj in xmlNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string value = xmlAttribute.Value;
				this.HandleResourceAttribute(xmlNode, ref explicitResourceKeys, xmlAttribute.Name, ref value, allowImplicitResource);
				readOnlyNameValueCollection[xmlAttribute.Name] = value;
			}
			readOnlyNameValueCollection.SetReadOnly(true);
			siteMapNode = new SiteMapNode(this, key, text, title, description, arrayList, readOnlyNameValueCollection, explicitResourceKeys, text3);
			siteMapNode.ReadOnly = true;
			foreach (object obj2 in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				if (xmlNode2.NodeType == XmlNodeType.Element)
				{
					queue.Enqueue(siteMapNode);
					queue.Enqueue(xmlNode2);
				}
			}
			return siteMapNode;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0003026C File Offset: 0x0002E46C
		private SiteMapProvider GetProviderFromName(string providerName)
		{
			SiteMapProvider siteMapProvider = SiteMap.Providers[providerName];
			if (siteMapProvider == null)
			{
				throw new ProviderException(SR.GetString("Provider_Not_Found", new object[]
				{
					providerName
				}));
			}
			return siteMapProvider;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000302A3 File Offset: 0x0002E4A3
		protected internal override SiteMapNode GetRootNodeCore()
		{
			this.BuildSiteMap();
			return this._siteMapNode;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000302B4 File Offset: 0x0002E4B4
		public override void Initialize(string name, NameValueCollection attributes)
		{
			if (this._initialized)
			{
				throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_Cannot_Be_Inited_Twice"));
			}
			if (attributes != null)
			{
				if (string.IsNullOrEmpty(attributes["description"]))
				{
					attributes.Remove("description");
					attributes.Add("description", SR.GetString("XmlSiteMapProvider_Description"));
				}
				string virtualPath = null;
				ProviderUtil.GetAndRemoveStringAttribute(attributes, "siteMapFile", name, ref virtualPath);
				this._virtualPath = VirtualPath.CreateAllowNull(virtualPath);
			}
			base.Initialize(name, attributes);
			if (attributes != null)
			{
				ProviderUtil.CheckUnrecognizedAttributes(attributes, name);
			}
			this._initialized = true;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00030344 File Offset: 0x0002E544
		private void Initialize(VirtualPath virtualPath, bool secuityTrimmingEnabled)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("siteMapFile", virtualPath.VirtualPathString);
			nameValueCollection.Add("securityTrimmingEnabled", Util.GetStringFromBool(secuityTrimmingEnabled));
			this.Initialize(virtualPath.VirtualPathString, nameValueCollection);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00030388 File Offset: 0x0002E588
		private void OnConfigFileChange(object sender, FileChangeEvent e)
		{
			XmlSiteMapProvider xmlSiteMapProvider = this.ParentProvider as XmlSiteMapProvider;
			if (xmlSiteMapProvider != null)
			{
				xmlSiteMapProvider.OnConfigFileChange(sender, e);
			}
			this.Clear();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000303B4 File Offset: 0x0002E5B4
		protected internal override void RemoveNode(SiteMapNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			SiteMapProvider provider = node.Provider;
			if (provider != this)
			{
				for (SiteMapProvider parentProvider = provider.ParentProvider; parentProvider != this; parentProvider = parentProvider.ParentProvider)
				{
					if (parentProvider == null)
					{
						throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_cannot_remove_node", new object[]
						{
							node.ToString(),
							this.Name,
							provider.Name
						}));
					}
				}
			}
			if (node.Equals(provider.GetRootNodeCore()))
			{
				throw new InvalidOperationException(SR.GetString("SiteMapProvider_cannot_remove_root_node"));
			}
			if (provider != this)
			{
				provider.RemoveNode(node);
			}
			base.RemoveNode(node);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00030454 File Offset: 0x0002E654
		protected virtual void RemoveProvider(string providerName)
		{
			if (providerName == null)
			{
				throw new ArgumentNullException("providerName");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				SiteMapProvider providerFromName = this.GetProviderFromName(providerName);
				SiteMapNode siteMapNode = (SiteMapNode)this.ChildProviderTable[providerFromName];
				if (siteMapNode == null)
				{
					throw new InvalidOperationException(SR.GetString("XmlSiteMapProvider_cannot_find_provider", new object[]
					{
						providerFromName.Name,
						this.Name
					}));
				}
				providerFromName.ParentProvider = null;
				this.ChildProviderTable.Remove(providerFromName);
				this._childProviderList = null;
				base.RemoveNode(siteMapNode);
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00030504 File Offset: 0x0002E704
		private bool ValidateResource(string classKey, string resourceKey)
		{
			try
			{
				HttpContext.GetGlobalResourceObject(classKey, resourceKey);
			}
			catch (MissingManifestResourceException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00030534 File Offset: 0x0002E734
		private static SiteMapNode ApplyModifierIfExists(SiteMapNode node)
		{
			HttpContext httpContext = HttpContext.Current;
			if (node == null || httpContext == null || !httpContext.Response.UsePathModifier)
			{
				return node;
			}
			SiteMapNode siteMapNode = node.Clone();
			siteMapNode.Url = httpContext.Response.ApplyAppPathModifier(node.Url);
			return siteMapNode;
		}

		// Token: 0x0400068F RID: 1679
		private string _filename;

		// Token: 0x04000690 RID: 1680
		private VirtualPath _virtualPath;

		// Token: 0x04000691 RID: 1681
		private VirtualPath _normalizedVirtualPath;

		// Token: 0x04000692 RID: 1682
		private SiteMapNode _siteMapNode;

		// Token: 0x04000693 RID: 1683
		private XmlDocument _document;

		// Token: 0x04000694 RID: 1684
		private bool _initialized;

		// Token: 0x04000695 RID: 1685
		private FileChangeEventHandler _handler;

		// Token: 0x04000696 RID: 1686
		private StringCollection _parentSiteMapFileCollection;

		// Token: 0x04000697 RID: 1687
		private const string _providerAttribute = "provider";

		// Token: 0x04000698 RID: 1688
		private const string _siteMapFileAttribute = "siteMapFile";

		// Token: 0x04000699 RID: 1689
		private const string _siteMapNodeName = "siteMapNode";

		// Token: 0x0400069A RID: 1690
		private const string _xmlSiteMapFileExtension = ".sitemap";

		// Token: 0x0400069B RID: 1691
		private const string _resourcePrefix = "$resources:";

		// Token: 0x0400069C RID: 1692
		private const int _resourcePrefixLength = 10;

		// Token: 0x0400069D RID: 1693
		private const char _resourceKeySeparator = ',';

		// Token: 0x0400069E RID: 1694
		private static readonly char[] _seperators = new char[]
		{
			';',
			','
		};

		// Token: 0x0400069F RID: 1695
		private ArrayList _childProviderList;

		// Token: 0x040006A0 RID: 1696
		private Hashtable _childProviderTable;

		// Token: 0x020008F8 RID: 2296
		private class ReadOnlyNameValueCollection : NameValueCollection
		{
			// Token: 0x06006880 RID: 26752 RVA: 0x00174021 File Offset: 0x00172221
			public ReadOnlyNameValueCollection()
			{
				base.IsReadOnly = true;
			}

			// Token: 0x06006881 RID: 26753 RVA: 0x00174030 File Offset: 0x00172230
			internal void SetReadOnly(bool isReadonly)
			{
				base.IsReadOnly = isReadonly;
			}
		}
	}
}
