using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000059 RID: 89
	internal sealed class XPathNodeInfoAtom
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000CC02 File Offset: 0x0000AE02
		public XPathNodeInfoAtom(XPathNodePageInfo pageInfo)
		{
			this.pageInfo = pageInfo;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000CC14 File Offset: 0x0000AE14
		public XPathNodeInfoAtom(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			this.Init(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000CC40 File Offset: 0x0000AE40
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000CCEE File Offset: 0x0000AEEE
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this.pageInfo;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000CCFE File Offset: 0x0000AEFE
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000CD06 File Offset: 0x0000AF06
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		public string BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000CD16 File Offset: 0x0000AF16
		public XPathNode[] SiblingPage
		{
			get
			{
				return this.pageSibling;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		public XPathNode[] SimilarElementPage
		{
			get
			{
				return this.pageSimilar;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000CD26 File Offset: 0x0000AF26
		public XPathNode[] ParentPage
		{
			get
			{
				return this.pageParent;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		public XPathDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000CD36 File Offset: 0x0000AF36
		public int LineNumberBase
		{
			get
			{
				return this.lineNumBase;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000CD3E File Offset: 0x0000AF3E
		public int LinePositionBase
		{
			get
			{
				return this.linePosBase;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000CD46 File Offset: 0x0000AF46
		public int LocalNameHashCode
		{
			get
			{
				return this.localNameHash;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000CD4E File Offset: 0x0000AF4E
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000CD56 File Offset: 0x0000AF56
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

		// Token: 0x06000343 RID: 835 RVA: 0x0000CD60 File Offset: 0x0000AF60
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

		// Token: 0x06000344 RID: 836 RVA: 0x0000CE00 File Offset: 0x0000B000
		public override bool Equals(object other)
		{
			XPathNodeInfoAtom xpathNodeInfoAtom = other as XPathNodeInfoAtom;
			return this.GetHashCode() == xpathNodeInfoAtom.GetHashCode() && this.localName == xpathNodeInfoAtom.localName && this.pageSibling == xpathNodeInfoAtom.pageSibling && this.namespaceUri == xpathNodeInfoAtom.namespaceUri && this.pageParent == xpathNodeInfoAtom.pageParent && this.pageSimilar == xpathNodeInfoAtom.pageSimilar && this.prefix == xpathNodeInfoAtom.prefix && this.baseUri == xpathNodeInfoAtom.baseUri && this.lineNumBase == xpathNodeInfoAtom.lineNumBase && this.linePosBase == xpathNodeInfoAtom.linePosBase;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
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

		// Token: 0x04000145 RID: 325
		private string localName;

		// Token: 0x04000146 RID: 326
		private string namespaceUri;

		// Token: 0x04000147 RID: 327
		private string prefix;

		// Token: 0x04000148 RID: 328
		private string baseUri;

		// Token: 0x04000149 RID: 329
		private XPathNode[] pageParent;

		// Token: 0x0400014A RID: 330
		private XPathNode[] pageSibling;

		// Token: 0x0400014B RID: 331
		private XPathNode[] pageSimilar;

		// Token: 0x0400014C RID: 332
		private XPathDocument doc;

		// Token: 0x0400014D RID: 333
		private int lineNumBase;

		// Token: 0x0400014E RID: 334
		private int linePosBase;

		// Token: 0x0400014F RID: 335
		private int hashCode;

		// Token: 0x04000150 RID: 336
		private int localNameHash;

		// Token: 0x04000151 RID: 337
		private XPathNodeInfoAtom next;

		// Token: 0x04000152 RID: 338
		private XPathNodePageInfo pageInfo;
	}
}
