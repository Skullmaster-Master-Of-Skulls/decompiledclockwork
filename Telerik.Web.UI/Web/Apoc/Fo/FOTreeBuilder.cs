using System;
using System.Collections;
using System.Text;
using System.Xml;
using Telerik.Web.Apoc.Extensions;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001419 RID: 5145
	internal sealed class FOTreeBuilder
	{
		// Token: 0x0600D2D2 RID: 53970 RVA: 0x002ECCF8 File Offset: 0x002EAEF8
		internal FOTreeBuilder()
		{
			this.options = (ApocDriver.ActiveDriver.Options as PdfRendererOptions);
		}

		// Token: 0x0600D2D3 RID: 53971 RVA: 0x002ECD4C File Offset: 0x002EAF4C
		internal void SetStreamRenderer(StreamRenderer streamRenderer)
		{
			this.streamRenderer = streamRenderer;
		}

		// Token: 0x0600D2D4 RID: 53972 RVA: 0x002ECD55 File Offset: 0x002EAF55
		internal void AddElementMapping(string namespaceURI, Hashtable table)
		{
			this.fobjTable.Add(namespaceURI, table);
			this.namespaces.Add(string.Intern(namespaceURI));
		}

		// Token: 0x0600D2D5 RID: 53973 RVA: 0x002ECD78 File Offset: 0x002EAF78
		internal void AddPropertyMapping(string namespaceURI, Hashtable list)
		{
			PropertyListBuilder propertyListBuilder = (PropertyListBuilder)this.propertylistTable[namespaceURI];
			if (propertyListBuilder == null)
			{
				propertyListBuilder = new PropertyListBuilder();
				propertyListBuilder.AddList(list);
				this.propertylistTable.Add(namespaceURI, propertyListBuilder);
				return;
			}
			propertyListBuilder.AddList(list);
		}

		// Token: 0x0600D2D6 RID: 53974 RVA: 0x002ECDBC File Offset: 0x002EAFBC
		private FObj.Maker GetFObjMaker(string uri, string localName)
		{
			Hashtable hashtable = (Hashtable)this.fobjTable[uri];
			if (hashtable != null)
			{
				return (FObj.Maker)hashtable[localName];
			}
			return null;
		}

		// Token: 0x0600D2D7 RID: 53975 RVA: 0x002ECDEC File Offset: 0x002EAFEC
		private void StartElement(string uri, string localName, Attributes attlist)
		{
			FObj.Maker maker = this.GetFObjMaker(uri, localName);
			PropertyListBuilder propertyListBuilder = (PropertyListBuilder)this.propertylistTable[uri];
			bool flag = false;
			if (maker == null)
			{
				string text = uri + "^" + localName;
				if (!this.unknownFOs.ContainsKey(text))
				{
					this.unknownFOs.Add(text, "");
					ApocDriver.ActiveDriver.FireApocError("Unknown formatting object " + text);
				}
				if (this.namespaces.Contains(string.Intern(uri)))
				{
					maker = new Unknown.Maker();
				}
				else
				{
					maker = new UnknownXMLObj.Maker(uri, localName);
					flag = true;
				}
			}
			PropertyList propertyList;
			if (propertyListBuilder != null)
			{
				propertyList = propertyListBuilder.MakeList(uri, localName, attlist, this.currentFObj);
			}
			else if (flag)
			{
				propertyList = null;
			}
			else
			{
				if (this.currentFObj == null)
				{
					throw new ApocException("Invalid XML or missing namespace");
				}
				propertyList = this.currentFObj.properties;
			}
			FObj fobj = maker.Make(this.currentFObj, propertyList);
			if (this.rootFObj == null)
			{
				this.rootFObj = fobj;
				if (!fobj.GetName().Equals("fo:root"))
				{
					throw new ApocException("Root element must be root, not " + fobj.GetName());
				}
			}
			else if (!(fobj is PageSequence))
			{
				this.currentFObj.AddChild(fobj);
			}
			this.currentFObj = fobj;
		}

		// Token: 0x0600D2D8 RID: 53976 RVA: 0x002ECF28 File Offset: 0x002EB128
		private void EndElement()
		{
			if (this.currentFObj != null)
			{
				this.currentFObj.End();
				if (this.currentFObj is PageSequence)
				{
					this.streamRenderer.Render((PageSequence)this.currentFObj);
				}
				else if (this.currentFObj is ExtensionObj && !(this.currentFObj.getParent() is ExtensionObj))
				{
					this.streamRenderer.AddExtension((ExtensionObj)this.currentFObj);
				}
				this.currentFObj = this.currentFObj.getParent();
			}
		}

		// Token: 0x0600D2D9 RID: 53977 RVA: 0x002ECFB4 File Offset: 0x002EB1B4
		internal void Parse(XmlReader reader)
		{
			try
			{
				object obj = reader.NameTable.Add("http://www.w3.org/2000/xmlns/");
				ApocDriver.ActiveDriver.FireApocInfo("Building formatting object tree");
				this.streamRenderer.StartRenderer();
				while (reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
						break;
					case XmlNodeType.Attribute:
						continue;
					case XmlNodeType.Text:
					{
						char[] array = this.PrepareTextNode(reader.ReadString()).ToCharArray();
						if (this.currentFObj != null)
						{
							this.currentFObj.AddCharacters(array, 0, array.Length);
						}
						if (reader.NodeType != XmlNodeType.Element)
						{
							if (reader.NodeType != XmlNodeType.EndElement)
							{
								continue;
							}
							goto IL_EF;
						}
						break;
					}
					default:
						if (nodeType != XmlNodeType.EndElement)
						{
							continue;
						}
						goto IL_EF;
					}
					Attributes attributes = new Attributes();
					while (reader.MoveToNextAttribute())
					{
						if (!reader.NamespaceURI.Equals(obj))
						{
							SaxAttribute saxAttribute = default(SaxAttribute);
							saxAttribute.Name = reader.Name;
							saxAttribute.NamespaceURI = reader.NamespaceURI;
							saxAttribute.Value = reader.Value;
							attributes.attArray.Add(saxAttribute);
						}
					}
					reader.MoveToElement();
					this.StartElement(reader.NamespaceURI, reader.LocalName, attributes.TrimArray());
					if (reader.IsEmptyElement)
					{
						this.EndElement();
						continue;
					}
					continue;
					IL_EF:
					this.EndElement();
				}
				ApocDriver.ActiveDriver.FireApocInfo("Parsing of document complete, stopping renderer");
				this.streamRenderer.StopRenderer();
			}
			catch (Exception ex)
			{
				ApocDriver.ActiveDriver.FireApocError(ex.ToString());
			}
			finally
			{
				if (reader.ReadState != ReadState.Closed)
				{
					reader.Close();
				}
			}
		}

		// Token: 0x0600D2DA RID: 53978 RVA: 0x002ED180 File Offset: 0x002EB380
		private string PrepareTextNode(string inputString)
		{
			if (this.options.ForceTextWrap)
			{
				char value = '\u001f';
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < inputString.Length; i++)
				{
					char c = inputString[i];
					stringBuilder.Append(c);
					if (!char.IsWhiteSpace(c) && i != inputString.Length - 1)
					{
						stringBuilder.Append(value);
					}
				}
				return stringBuilder.ToString();
			}
			return inputString;
		}

		// Token: 0x04003916 RID: 14614
		private Hashtable fobjTable = new Hashtable();

		// Token: 0x04003917 RID: 14615
		private ArrayList namespaces = new ArrayList();

		// Token: 0x04003918 RID: 14616
		private Hashtable propertylistTable = new Hashtable();

		// Token: 0x04003919 RID: 14617
		private FObj currentFObj;

		// Token: 0x0400391A RID: 14618
		private FObj rootFObj;

		// Token: 0x0400391B RID: 14619
		private Hashtable unknownFOs = new Hashtable();

		// Token: 0x0400391C RID: 14620
		private StreamRenderer streamRenderer;

		// Token: 0x0400391D RID: 14621
		private PdfRendererOptions options;
	}
}
