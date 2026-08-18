using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C3 RID: 195
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class DesignTimeSiteMapProvider : DesignTimeSiteMapProviderBase
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x000213D0 File Offset: 0x0001F5D0
		internal DesignTimeSiteMapProvider(IDesignerHost host) : base(host)
		{
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x000213DC File Offset: 0x0001F5DC
		public override SiteMapNode CurrentNode
		{
			get
			{
				SiteMapNode siteMapNode;
				SiteMapNode currentNodeFromLiveData = this.GetCurrentNodeFromLiveData(out siteMapNode);
				if (currentNodeFromLiveData != null)
				{
					return currentNodeFromLiveData;
				}
				return base.CurrentNode;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00021400 File Offset: 0x0001F600
		public override SiteMapNode RootNode
		{
			get
			{
				SiteMapNode siteMapNode;
				SiteMapNode currentNodeFromLiveData = this.GetCurrentNodeFromLiveData(out siteMapNode);
				if (siteMapNode != null)
				{
					return siteMapNode;
				}
				return base.RootNode;
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00021424 File Offset: 0x0001F624
		private Stream GetSiteMapFileStream(out string physicalPath)
		{
			physicalPath = string.Empty;
			if (this._host != null)
			{
				IWebApplication webApplication = (IWebApplication)this._host.GetService(typeof(IWebApplication));
				if (webApplication != null)
				{
					IProjectItem projectItemFromUrl = webApplication.GetProjectItemFromUrl("~/web.sitemap");
					if (projectItemFromUrl != null)
					{
						physicalPath = projectItemFromUrl.PhysicalPath;
						IDocumentProjectItem documentProjectItem = projectItemFromUrl as IDocumentProjectItem;
						if (documentProjectItem != null)
						{
							return documentProjectItem.GetContents();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00021488 File Offset: 0x0001F688
		internal new IDictionary UrlTable
		{
			get
			{
				if (this._urlTable == null)
				{
					lock (this)
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

		// Token: 0x06000637 RID: 1591 RVA: 0x000214E4 File Offset: 0x0001F6E4
		public override SiteMapNode BuildSiteMap()
		{
			if (this._rootNode != null)
			{
				return this._rootNode;
			}
			string text = null;
			Stream siteMapFileStream = this.GetSiteMapFileStream(out text);
			XmlDocument xmlDocument = new XmlDocument();
			if (siteMapFileStream == null)
			{
				if (text.Length == 0)
				{
					this._rootNode = base.BuildSiteMap();
					return this._rootNode;
				}
				using (XmlTextReader xmlTextReader = new XmlTextReader(text)
				{
					DtdProcessing = DtdProcessing.Ignore
				})
				{
					xmlDocument.Load(xmlTextReader);
					goto IL_88;
				}
			}
			using (XmlTextReader xmlTextReader2 = new XmlTextReader(siteMapFileStream)
			{
				DtdProcessing = DtdProcessing.Ignore
			})
			{
				xmlDocument.Load(xmlTextReader2);
			}
			IL_88:
			XmlNode xmlNode = null;
			foreach (object obj in xmlDocument.ChildNodes)
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
				this._rootNode = base.BuildSiteMap();
				return this._rootNode;
			}
			try
			{
				this._rootNode = this.ConvertFromXmlNode(xmlNode.FirstChild);
			}
			catch (Exception ex)
			{
				this.Clear();
				this._rootNode = base.BuildSiteMap();
			}
			return this._rootNode;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002164C File Offset: 0x0001F84C
		private string GetAttributeFromXmlNode(XmlNode xmlNode, string attributeName)
		{
			XmlNode namedItem = xmlNode.Attributes.GetNamedItem(attributeName);
			if (namedItem != null)
			{
				return namedItem.Value;
			}
			return null;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00021674 File Offset: 0x0001F874
		private SiteMapNode ConvertFromXmlNode(XmlNode xmlNode)
		{
			if (xmlNode.Attributes.GetNamedItem("provider") != null || xmlNode.Attributes.GetNamedItem("siteMapFile") != null)
			{
				return null;
			}
			string text = null;
			string text2 = this.GetAttributeFromXmlNode(xmlNode, "title");
			string text3 = this.GetAttributeFromXmlNode(xmlNode, "description");
			text = this.GetAttributeFromXmlNode(xmlNode, "url");
			string attributeFromXmlNode = this.GetAttributeFromXmlNode(xmlNode, "roles");
			text2 = this.HandleResourceAttribute(text2);
			text3 = this.HandleResourceAttribute(text3);
			ArrayList arrayList = new ArrayList();
			if (attributeFromXmlNode != null)
			{
				foreach (string text4 in attributeFromXmlNode.Split(DesignTimeSiteMapProvider._seperators))
				{
					string text5 = text4.Trim();
					if (text5.Length > 0)
					{
						arrayList.Add(text5);
					}
				}
			}
			arrayList = ArrayList.ReadOnly(arrayList);
			if (text == null)
			{
				text = string.Empty;
			}
			if (text.Length != 0 && !DesignTimeSiteMapProvider.IsAppRelativePath(text))
			{
				text = "~/" + text;
			}
			string text6 = text;
			if (text6.Length == 0)
			{
				text6 = Guid.NewGuid().ToString();
			}
			SiteMapNode siteMapNode = new SiteMapNode(this, text6, text, text2, text3, arrayList, null, null, null);
			SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection();
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.NodeType == XmlNodeType.Element)
				{
					SiteMapNode siteMapNode2 = this.ConvertFromXmlNode(xmlNode2);
					if (siteMapNode2 != null)
					{
						siteMapNodeCollection.Add(siteMapNode2);
						this.AddNode(siteMapNode2, siteMapNode);
					}
				}
			}
			if (text.Length != 0)
			{
				if (this.UrlTable.Contains(text))
				{
					throw new InvalidOperationException(SR.GetString("DesignTimeSiteMapProvider_Duplicate_Url", new object[]
					{
						text
					}));
				}
				this.UrlTable[text] = siteMapNode;
			}
			return siteMapNode;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00021864 File Offset: 0x0001FA64
		private SiteMapNode GetCurrentNodeFromLiveData(out SiteMapNode rootNode)
		{
			rootNode = this.BuildSiteMap();
			if (rootNode != null && base.DocumentAppRelativeUrl != null)
			{
				return (SiteMapNode)this.UrlTable[base.DocumentAppRelativeUrl];
			}
			return null;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00021894 File Offset: 0x0001FA94
		private string HandleResourceAttribute(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = text.TrimStart(new char[]
				{
					' '
				});
				if (text2.Length > 10 && text2.ToLower(CultureInfo.InvariantCulture).StartsWith("$resources:", StringComparison.Ordinal))
				{
					int num = text2.IndexOf(',');
					if (num != -1)
					{
						num = text2.IndexOf(',', num + 1);
						if (num != -1)
						{
							return text2.Substring(num + 1);
						}
					}
					return string.Empty;
				}
			}
			return text;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00013A74 File Offset: 0x00011C74
		private static bool IsAppRelativePath(string path)
		{
			return path.Length >= 2 && path[0] == '~' && (path[1] == '/' || path[1] == '\\');
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
		{
			return true;
		}

		// Token: 0x04000395 RID: 917
		private const string _providerAttribute = "provider";

		// Token: 0x04000396 RID: 918
		private const string _siteMapFileAttribute = "siteMapFile";

		// Token: 0x04000397 RID: 919
		private const string _siteMapNodeName = "siteMapNode";

		// Token: 0x04000398 RID: 920
		private const string _resourcePrefix = "$resources:";

		// Token: 0x04000399 RID: 921
		private const char _appRelativeCharacter = '~';

		// Token: 0x0400039A RID: 922
		private const int _resourcePrefixLength = 10;

		// Token: 0x0400039B RID: 923
		private static readonly char[] _seperators = new char[]
		{
			';',
			','
		};

		// Token: 0x0400039C RID: 924
		private SiteMapNode _rootNode;

		// Token: 0x0400039D RID: 925
		private Hashtable _urlTable;
	}
}
