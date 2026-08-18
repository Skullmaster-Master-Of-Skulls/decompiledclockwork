using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace iTextSharp.text.xml
{
	// Token: 0x02000049 RID: 73
	public abstract class ParserBase
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000A45C File Offset: 0x0000945C
		public void Parse(XmlDocument xDoc)
		{
			string outerXml = xDoc.OuterXml;
			StringReader input = new StringReader(outerXml);
			XmlTextReader reader = new XmlTextReader(input);
			this.Parse(reader);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A488 File Offset: 0x00009488
		public void Parse(XmlTextReader reader)
		{
			try
			{
				while (reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					{
						string namespaceURI = reader.NamespaceURI;
						string name = reader.Name;
						bool isEmptyElement = reader.IsEmptyElement;
						Hashtable hashtable = new Hashtable();
						if (reader.HasAttributes)
						{
							for (int i = 0; i < reader.AttributeCount; i++)
							{
								reader.MoveToAttribute(i);
								hashtable.Add(reader.Name, reader.Value);
							}
						}
						this.StartElement(namespaceURI, name, name, hashtable);
						if (isEmptyElement)
						{
							this.EndElement(namespaceURI, name, name);
						}
						break;
					}
					case XmlNodeType.Attribute:
						break;
					case XmlNodeType.Text:
						this.Characters(reader.Value, 0, reader.Value.Length);
						break;
					default:
						switch (nodeType)
						{
						case XmlNodeType.Whitespace:
							this.Characters(reader.Value, 0, reader.Value.Length);
							break;
						case XmlNodeType.EndElement:
							this.EndElement(reader.NamespaceURI, reader.Name, reader.Name);
							break;
						}
						break;
					}
				}
			}
			catch (XmlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A5E8 File Offset: 0x000095E8
		public void Parse(string url)
		{
			XmlTextReader reader = new XmlTextReader(url);
			this.Parse(reader);
		}

		// Token: 0x060001EE RID: 494
		public abstract void StartElement(string uri, string lname, string name, Hashtable attrs);

		// Token: 0x060001EF RID: 495
		public abstract void EndElement(string uri, string lname, string name);

		// Token: 0x060001F0 RID: 496
		public abstract void Characters(string content, int start, int length);
	}
}
