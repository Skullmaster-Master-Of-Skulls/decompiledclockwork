using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000F9 RID: 249
	internal class DomNameTable
	{
		// Token: 0x06001129 RID: 4393 RVA: 0x00048986 File Offset: 0x00046B86
		public DomNameTable(XmlDocument document)
		{
			this.ownerDocument = document;
			this.nameTable = document.NameTable;
			this.entries = new XmlName[64];
			this.mask = 63;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000489B8 File Offset: 0x00046BB8
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

		// Token: 0x0600112B RID: 4395 RVA: 0x00048A58 File Offset: 0x00046C58
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
			int num2 = this.count;
			this.count = num2 + 1;
			if (num2 == this.mask)
			{
				this.Grow();
			}
			return xmlName2;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00048B70 File Offset: 0x00046D70
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

		// Token: 0x040004C9 RID: 1225
		private XmlName[] entries;

		// Token: 0x040004CA RID: 1226
		private int count;

		// Token: 0x040004CB RID: 1227
		private int mask;

		// Token: 0x040004CC RID: 1228
		private XmlDocument ownerDocument;

		// Token: 0x040004CD RID: 1229
		private XmlNameTable nameTable;

		// Token: 0x040004CE RID: 1230
		private const int InitialSize = 64;
	}
}
