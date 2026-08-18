using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200010E RID: 270
	internal sealed class XPathNodeInfoAtom
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x0004B402 File Offset: 0x0004A402
		public XPathNodeInfoAtom(XPathNodePageInfo pageInfo)
		{
			this.pageInfo = pageInfo;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0004B414 File Offset: 0x0004A414
		public XPathNodeInfoAtom(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this.Init(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004B440 File Offset: 0x0004A440
		public void Init(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this.localName = localName;
			this.namespaceUri = namespaceUri;
			this.prefix = prefix;
			this.baseUri = baseUri;
			this.pageParent = pageParent;
			this.pageSibling = pageSibling;
			this.pageSimilar = pageSimilar;
			this.doc = doc;
			this.lineNumBase = lineNumBase;
			this.linePosBase = linePosBase;
			this.next = null;
			this.pageInfo = null;
			this.hashCode = 0;
			this.localNameHash = 0;
			for (int i = 0; i < this.localName.Length; i++)
			{
				this.localNameHash += (this.localNameHash << 7 ^ (int)this.localName[i]);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0004B4EE File Offset: 0x0004A4EE
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this.pageInfo;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0004B4F6 File Offset: 0x0004A4F6
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0004B4FE File Offset: 0x0004A4FE
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0004B506 File Offset: 0x0004A506
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0004B50E File Offset: 0x0004A50E
		public string BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0004B516 File Offset: 0x0004A516
		public XPathNode[] SiblingPage
		{
			get
			{
				return this.pageSibling;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0004B51E File Offset: 0x0004A51E
		public XPathNode[] SimilarElementPage
		{
			get
			{
				return this.pageSimilar;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0004B526 File Offset: 0x0004A526
		public XPathNode[] ParentPage
		{
			get
			{
				return this.pageParent;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0004B52E File Offset: 0x0004A52E
		public XPathDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0004B536 File Offset: 0x0004A536
		public int LineNumberBase
		{
			get
			{
				return this.lineNumBase;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0004B53E File Offset: 0x0004A53E
		public int LinePositionBase
		{
			get
			{
				return this.linePosBase;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0004B546 File Offset: 0x0004A546
		public int LocalNameHashCode
		{
			get
			{
				return this.localNameHash;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0004B54E File Offset: 0x0004A54E
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x0004B556 File Offset: 0x0004A556
		public XPathNodeInfoAtom Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0004B560 File Offset: 0x0004A560
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				int num = RuntimeHelpers.GetHashCode(this.localName);
				num = Ref.CombineHashRef(num, this.namespaceUri);
				num = Ref.CombineHashRef(num, this.prefix);
				num = Ref.CombineHashRef(num, this.baseUri);
				num = Ref.CombineHashRef(num, this.pageSibling);
				num = Ref.CombineHashRef(num, this.pageParent);
				num = Ref.CombineHashRef(num, this.pageSimilar);
				num = Ref.CombineHash(num, this.lineNumBase);
				num = Ref.CombineHash(num, this.linePosBase);
				this.hashCode = ((num == 0) ? 1 : num);
			}
			return this.hashCode;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0004B600 File Offset: 0x0004A600
		public override bool Equals(object other)
		{
			XPathNodeInfoAtom xpathNodeInfoAtom = other as XPathNodeInfoAtom;
			return this.GetHashCode() == xpathNodeInfoAtom.GetHashCode() && this.localName == xpathNodeInfoAtom.localName && this.pageSibling == xpathNodeInfoAtom.pageSibling && this.namespaceUri == xpathNodeInfoAtom.namespaceUri && this.pageParent == xpathNodeInfoAtom.pageParent && this.pageSimilar == xpathNodeInfoAtom.pageSimilar && this.prefix == xpathNodeInfoAtom.prefix && this.baseUri == xpathNodeInfoAtom.baseUri && this.lineNumBase == xpathNodeInfoAtom.lineNumBase && this.linePosBase == xpathNodeInfoAtom.linePosBase;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004B6A8 File Offset: 0x0004A6A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("hash=");
			stringBuilder.Append(this.GetHashCode());
			stringBuilder.Append(", ");
			if (this.localName.Length != 0)
			{
				stringBuilder.Append('{');
				stringBuilder.Append(this.namespaceUri);
				stringBuilder.Append('}');
				if (this.prefix.Length != 0)
				{
					stringBuilder.Append(this.prefix);
					stringBuilder.Append(':');
				}
				stringBuilder.Append(this.localName);
				stringBuilder.Append(", ");
			}
			if (this.pageParent != null)
			{
				stringBuilder.Append("parent=");
				stringBuilder.Append(this.pageParent[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			if (this.pageSibling != null)
			{
				stringBuilder.Append("sibling=");
				stringBuilder.Append(this.pageSibling[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			if (this.pageSimilar != null)
			{
				stringBuilder.Append("similar=");
				stringBuilder.Append(this.pageSimilar[0].PageInfo.PageNumber);
				stringBuilder.Append(", ");
			}
			stringBuilder.Append("lineNum=");
			stringBuilder.Append(this.lineNumBase);
			stringBuilder.Append(", ");
			stringBuilder.Append("linePos=");
			stringBuilder.Append(this.linePosBase);
			return stringBuilder.ToString();
		}

		// Token: 0x04000AB7 RID: 2743
		private string localName;

		// Token: 0x04000AB8 RID: 2744
		private string namespaceUri;

		// Token: 0x04000AB9 RID: 2745
		private string prefix;

		// Token: 0x04000ABA RID: 2746
		private string baseUri;

		// Token: 0x04000ABB RID: 2747
		private XPathNode[] pageParent;

		// Token: 0x04000ABC RID: 2748
		private XPathNode[] pageSibling;

		// Token: 0x04000ABD RID: 2749
		private XPathNode[] pageSimilar;

		// Token: 0x04000ABE RID: 2750
		private XPathDocument doc;

		// Token: 0x04000ABF RID: 2751
		private int lineNumBase;

		// Token: 0x04000AC0 RID: 2752
		private int linePosBase;

		// Token: 0x04000AC1 RID: 2753
		private int hashCode;

		// Token: 0x04000AC2 RID: 2754
		private int localNameHash;

		// Token: 0x04000AC3 RID: 2755
		private XPathNodeInfoAtom next;

		// Token: 0x04000AC4 RID: 2756
		private XPathNodePageInfo pageInfo;
	}
}
