using System;
using System.Text;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200005A RID: 90
	internal sealed class XPathNodeInfoTable
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000D043 File Offset: 0x0000B243
		public XPathNodeInfoTable()
		{
			this.hashTable = new XPathNodeInfoAtom[32];
			this.sizeTable = 0;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D060 File Offset: 0x0000B260
		public XPathNodeInfoAtom Create(string localName, string namespaceUri, string prefix, string baseUri, XPathNode[] pageParent, XPathNode[] pageSibling, XPathNode[] pageSimilar, XPathDocument doc, int lineNumBase, int linePosBase)
		{
			XPathNodeInfoAtom xpathNodeInfoAtom;
			if (this.infoCached == null)
			{
				xpathNodeInfoAtom = new XPathNodeInfoAtom(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
			}
			else
			{
				xpathNodeInfoAtom = this.infoCached;
				this.infoCached = xpathNodeInfoAtom.Next;
				xpathNodeInfoAtom.Init(localName, namespaceUri, prefix, baseUri, pageParent, pageSibling, pageSimilar, doc, lineNumBase, linePosBase);
			}
			return this.Atomize(xpathNodeInfoAtom);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		private XPathNodeInfoAtom Atomize(XPathNodeInfoAtom info)
		{
			for (XPathNodeInfoAtom xpathNodeInfoAtom = this.hashTable[info.GetHashCode() & this.hashTable.Length - 1]; xpathNodeInfoAtom != null; xpathNodeInfoAtom = xpathNodeInfoAtom.Next)
			{
				if (info.Equals(xpathNodeInfoAtom))
				{
					info.Next = this.infoCached;
					this.infoCached = info;
					return xpathNodeInfoAtom;
				}
			}
			if (this.sizeTable >= this.hashTable.Length)
			{
				XPathNodeInfoAtom[] array = this.hashTable;
				this.hashTable = new XPathNodeInfoAtom[array.Length * 2];
				foreach (XPathNodeInfoAtom xpathNodeInfoAtom in array)
				{
					while (xpathNodeInfoAtom != null)
					{
						XPathNodeInfoAtom next = xpathNodeInfoAtom.Next;
						this.AddInfo(xpathNodeInfoAtom);
						xpathNodeInfoAtom = next;
					}
				}
			}
			this.AddInfo(info);
			return info;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D168 File Offset: 0x0000B368
		private void AddInfo(XPathNodeInfoAtom info)
		{
			int num = info.GetHashCode() & this.hashTable.Length - 1;
			info.Next = this.hashTable[num];
			this.hashTable[num] = info;
			this.sizeTable++;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.hashTable.Length; i++)
			{
				stringBuilder.AppendFormat("{0,4}: ", i);
				for (XPathNodeInfoAtom xpathNodeInfoAtom = this.hashTable[i]; xpathNodeInfoAtom != null; xpathNodeInfoAtom = xpathNodeInfoAtom.Next)
				{
					if (xpathNodeInfoAtom != this.hashTable[i])
					{
						stringBuilder.Append("\n      ");
					}
					stringBuilder.Append(xpathNodeInfoAtom);
				}
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000153 RID: 339
		private XPathNodeInfoAtom[] hashTable;

		// Token: 0x04000154 RID: 340
		private int sizeTable;

		// Token: 0x04000155 RID: 341
		private XPathNodeInfoAtom infoCached;

		// Token: 0x04000156 RID: 342
		private const int DefaultTableSize = 32;
	}
}
