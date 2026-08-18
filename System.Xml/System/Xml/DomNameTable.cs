using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000C8 RID: 200
	internal class DomNameTable
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x00035162 File Offset: 0x00034162
		public DomNameTable(XmlDocument document)
		{
			this.ownerDocument = document;
			this.nameTable = document.NameTable;
			this.entries = new XmlName[64];
			this.mask = 63;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00035194 File Offset: 0x00034194
		public XmlName GetName(string prefix, string localName, string ns, IXmlSchemaInfo schemaInfo)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int hashCode = XmlName.GetHashCode(localName);
			for (XmlName xmlName = this.entries[hashCode & this.mask]; xmlName != null; xmlName = xmlName.next)
			{
				if (xmlName.HashCode == hashCode && (xmlName.LocalName == localName || xmlName.LocalName.Equals(localName)) && (xmlName.Prefix == prefix || xmlName.Prefix.Equals(prefix)) && (xmlName.NamespaceURI == ns || xmlName.NamespaceURI.Equals(ns)) && xmlName.Equals(schemaInfo))
				{
					return xmlName;
				}
			}
			return null;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00035234 File Offset: 0x00034234
		public XmlName AddName(string prefix, string localName, string ns, IXmlSchemaInfo schemaInfo)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int hashCode = XmlName.GetHashCode(localName);
			for (XmlName xmlName = this.entries[hashCode & this.mask]; xmlName != null; xmlName = xmlName.next)
			{
				if (xmlName.HashCode == hashCode && (xmlName.LocalName == localName || xmlName.LocalName.Equals(localName)) && (xmlName.Prefix == prefix || xmlName.Prefix.Equals(prefix)) && (xmlName.NamespaceURI == ns || xmlName.NamespaceURI.Equals(ns)) && xmlName.Equals(schemaInfo))
				{
					return xmlName;
				}
			}
			prefix = this.nameTable.Add(prefix);
			localName = this.nameTable.Add(localName);
			ns = this.nameTable.Add(ns);
			int num = hashCode & this.mask;
			XmlName xmlName2 = XmlName.Create(prefix, localName, ns, hashCode, this.ownerDocument, this.entries[num], schemaInfo);
			this.entries[num] = xmlName2;
			if (this.count++ == this.mask)
			{
				this.Grow();
			}
			return xmlName2;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003534C File Offset: 0x0003434C
		private void Grow()
		{
			int num = this.mask * 2 + 1;
			XmlName[] array = this.entries;
			XmlName[] array2 = new XmlName[num + 1];
			foreach (XmlName xmlName in array)
			{
				while (xmlName != null)
				{
					int num2 = xmlName.HashCode & num;
					XmlName next = xmlName.next;
					xmlName.next = array2[num2];
					array2[num2] = xmlName;
					xmlName = next;
				}
			}
			this.entries = array2;
			this.mask = num;
		}

		// Token: 0x040008E6 RID: 2278
		private const int InitialSize = 64;

		// Token: 0x040008E7 RID: 2279
		private XmlName[] entries;

		// Token: 0x040008E8 RID: 2280
		private int count;

		// Token: 0x040008E9 RID: 2281
		private int mask;

		// Token: 0x040008EA RID: 2282
		private XmlDocument ownerDocument;

		// Token: 0x040008EB RID: 2283
		private XmlNameTable nameTable;
	}
}
