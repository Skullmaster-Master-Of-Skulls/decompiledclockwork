using System;
using System.IO;
using System.Text;
using System.Xml;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x020000BA RID: 186
	public class XmpReader
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0001DE14 File Offset: 0x0001CE14
		public XmpReader(byte[] bytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(bytes, 0, bytes.Length);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			XmlTextReader reader = new XmlTextReader(memoryStream);
			this.domDocument = new XmlDocument();
			this.domDocument.PreserveWhitespace = true;
			this.domDocument.Load(reader);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001DE6C File Offset: 0x0001CE6C
		public bool ReplaceNode(string namespaceURI, string localName, string value)
		{
			XmlNodeList elementsByTagName = this.domDocument.GetElementsByTagName(localName, namespaceURI);
			if (elementsByTagName.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < elementsByTagName.Count; i++)
			{
				XmlNode n = elementsByTagName[i];
				this.SetNodeText(this.domDocument, n, value);
			}
			return true;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001DEBC File Offset: 0x0001CEBC
		public bool ReplaceDescriptionAttribute(string namespaceURI, string localName, string value)
		{
			XmlNodeList elementsByTagName = this.domDocument.GetElementsByTagName("Description", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
			if (elementsByTagName.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < elementsByTagName.Count; i++)
			{
				XmlNode xmlNode = elementsByTagName.Item(i);
				XmlNode namedItem = xmlNode.Attributes.GetNamedItem(localName, namespaceURI);
				if (namedItem != null)
				{
					namedItem.Value = value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001DF20 File Offset: 0x0001CF20
		public bool Add(string parent, string namespaceURI, string localName, string value)
		{
			XmlNodeList elementsByTagName = this.domDocument.GetElementsByTagName(parent);
			if (elementsByTagName.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < elementsByTagName.Count; i++)
			{
				XmlNode xmlNode = elementsByTagName[i];
				XmlAttributeCollection attributes = xmlNode.Attributes;
				for (int j = 0; j < attributes.Count; j++)
				{
					XmlNode xmlNode2 = attributes[j];
					if (namespaceURI.Equals(xmlNode2.Value))
					{
						xmlNode2 = this.domDocument.CreateElement(localName);
						xmlNode2.AppendChild(this.domDocument.CreateTextNode(value));
						xmlNode.AppendChild(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001DFC0 File Offset: 0x0001CFC0
		public bool SetNodeText(XmlDocument domDocument, XmlNode n, string value)
		{
			if (n == null)
			{
				return false;
			}
			XmlNode firstChild;
			while ((firstChild = n.FirstChild) != null)
			{
				n.RemoveChild(firstChild);
			}
			n.AppendChild(domDocument.CreateTextNode(value));
			return true;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001DFF8 File Offset: 0x0001CFF8
		public byte[] SerializeDoc()
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] bytes = new UTF8Encoding(false).GetBytes("<?xpacket begin=\"﻿\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>\n");
			memoryStream.Write(bytes, 0, bytes.Length);
			memoryStream.Flush();
			XmlNodeList elementsByTagName = this.domDocument.GetElementsByTagName("x:xmpmeta");
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding(false));
			xmlTextWriter.WriteNode(new XmlNodeReader(elementsByTagName[0]), true);
			xmlTextWriter.Flush();
			bytes = new UTF8Encoding(false).GetBytes("                                                                                                   \n");
			for (int i = 0; i < 20; i++)
			{
				memoryStream.Write(bytes, 0, bytes.Length);
			}
			bytes = new UTF8Encoding(false).GetBytes("<?xpacket end=\"w\"?>");
			memoryStream.Write(bytes, 0, bytes.Length);
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x040002CC RID: 716
		private XmlDocument domDocument;
	}
}
