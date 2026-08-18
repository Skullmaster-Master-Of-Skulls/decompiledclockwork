using System;
using System.Text;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200010F RID: 271
	internal sealed class XPathNodeInfoTable
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x0004B843 File Offset: 0x0004A843
		public XPathNodeInfoTable()
		{
			this.hashTable = new XPathNodeInfoAtom[32];
			this.sizeTable = 0;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0004B860 File Offset: 0x0004A860
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

		// Token: 0x06001098 RID: 4248 RVA: 0x0004B8C0 File Offset: 0x0004A8C0
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

		// Token: 0x06001099 RID: 4249 RVA: 0x0004B968 File Offset: 0x0004A968
		private void AddInfo(XPathNodeInfoAtom info)
		{
			int num = info.GetHashCode() & this.hashTable.Length - 1;
			info.Next = this.hashTable[num];
			this.hashTable[num] = info;
			this.sizeTable++;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004B9AC File Offset: 0x0004A9AC
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

		// Token: 0x04000AC5 RID: 2757
		private const int DefaultTableSize = 32;

		// Token: 0x04000AC6 RID: 2758
		private XPathNodeInfoAtom[] hashTable;

		// Token: 0x04000AC7 RID: 2759
		private int sizeTable;

		// Token: 0x04000AC8 RID: 2760
		private XPathNodeInfoAtom infoCached;
	}
}
