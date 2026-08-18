using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000107 RID: 263
	public class XmlElement : XmlLinkedNode
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x0004D288 File Offset: 0x0004B488
		internal XmlElement(XmlName name, bool empty, XmlDocument doc) : base(doc)
		{
			this.parentNode = null;
			if (!doc.IsLoading)
			{
				XmlDocument.CheckName(name.Prefix);
				XmlDocument.CheckName(name.LocalName);
			}
			if (name.LocalName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xdom_Empty_LocalName"));
			}
			this.name = name;
			if (empty)
			{
				this.lastChild = this;
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004D2EF File Offset: 0x0004B4EF
		protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(doc.AddXmlName(prefix, localName, namespaceURI, null), true, doc)
		{
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004D305 File Offset: 0x0004B505
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0004D30D File Offset: 0x0004B50D
		internal XmlName XmlName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0004D318 File Offset: 0x0004B518
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			bool isLoading = ownerDocument.IsLoading;
			ownerDocument.IsLoading = true;
			XmlElement xmlElement = ownerDocument.CreateElement(this.Prefix, this.LocalName, this.NamespaceURI);
			ownerDocument.IsLoading = isLoading;
			if (xmlElement.IsEmpty != this.IsEmpty)
			{
				xmlElement.IsEmpty = this.IsEmpty;
			}
			if (this.HasAttributes)
			{
				foreach (object obj in this.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					XmlAttribute xmlAttribute2 = (XmlAttribute)xmlAttribute.CloneNode(true);
					if (xmlAttribute is XmlUnspecifiedAttribute && !xmlAttribute.Specified)
					{
						((XmlUnspecifiedAttribute)xmlAttribute2).SetSpecified(false);
					}
					xmlElement.Attributes.InternalAppendAttribute(xmlAttribute2);
				}
			}
			if (deep)
			{
				xmlElement.CopyChildren(ownerDocument, this, deep);
			}
			return xmlElement;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0004D410 File Offset: 0x0004B610
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0004D41D File Offset: 0x0004B61D
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004D42A File Offset: 0x0004B62A
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0004D437 File Offset: 0x0004B637
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x0004D444 File Offset: 0x0004B644
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0004D46F File Offset: 0x0004B66F
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0004D472 File Offset: 0x0004B672
		public override XmlNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004D47A File Offset: 0x0004B67A
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004D487 File Offset: 0x0004B687
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004D48C File Offset: 0x0004B68C
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null || this.lastChild == this)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode2 = this.lastChild;
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
				else
				{
					xmlLinkedNode.SetParentForLoad(this);
				}
			}
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004D527 File Offset: 0x0004B727
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x0004D532 File Offset: 0x0004B732
		public bool IsEmpty
		{
			get
			{
				return this.lastChild == this;
			}
			set
			{
				if (value)
				{
					if (this.lastChild != this)
					{
						this.RemoveAllChildren();
						this.lastChild = this;
						return;
					}
				}
				else if (this.lastChild == this)
				{
					this.lastChild = null;
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004D55E File Offset: 0x0004B75E
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0004D571 File Offset: 0x0004B771
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild != this)
				{
					return this.lastChild;
				}
				return null;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0004D57C File Offset: 0x0004B77C
		internal override bool IsValidChildType(XmlNodeType type)
		{
			switch (type)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return true;
			}
			return false;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004D5D0 File Offset: 0x0004B7D0
		public override XmlAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					object objLock = this.OwnerDocument.objLock;
					lock (objLock)
					{
						if (this.attributes == null)
						{
							this.attributes = new XmlAttributeCollection(this);
						}
					}
				}
				return this.attributes;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0004D634 File Offset: 0x0004B834
		public virtual bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0004D650 File Offset: 0x0004B850
		public virtual string GetAttribute(string name)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0004D674 File Offset: 0x0004B874
		public virtual void SetAttribute(string name, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(name);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
				return;
			}
			xmlAttribute.Value = value;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004D6B5 File Offset: 0x0004B8B5
		public virtual void RemoveAttribute(string name)
		{
			if (this.HasAttributes)
			{
				this.Attributes.RemoveNamedItem(name);
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004D6CC File Offset: 0x0004B8CC
		public virtual XmlAttribute GetAttributeNode(string name)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[name];
			}
			return null;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0004D6E4 File Offset: 0x0004B8E4
		public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr)
		{
			if (newAttr.OwnerElement != null)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Attr_InUse"));
			}
			return (XmlAttribute)this.Attributes.SetNamedItem(newAttr);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004D70F File Offset: 0x0004B90F
		public virtual XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr)
		{
			if (this.HasAttributes)
			{
				return this.Attributes.Remove(oldAttr);
			}
			return null;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004D727 File Offset: 0x0004B927
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004D730 File Offset: 0x0004B930
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004D758 File Offset: 0x0004B958
		public virtual string SetAttribute(string localName, string namespaceURI, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			else
			{
				xmlAttribute.Value = value;
			}
			return value;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004D7A2 File Offset: 0x0004B9A2
		public virtual void RemoveAttribute(string localName, string namespaceURI)
		{
			this.RemoveAttributeNode(localName, namespaceURI);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004D7AD File Offset: 0x0004B9AD
		public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[localName, namespaceURI];
			}
			return null;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004D7C8 File Offset: 0x0004B9C8
		public virtual XmlAttribute SetAttributeNode(string localName, string namespaceURI)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			return xmlAttribute;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004D804 File Offset: 0x0004BA04
		public virtual XmlAttribute RemoveAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
				this.Attributes.Remove(attributeNode);
				return attributeNode;
			}
			return null;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004D832 File Offset: 0x0004BA32
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004D83C File Offset: 0x0004BA3C
		public virtual bool HasAttribute(string name)
		{
			return this.GetAttributeNode(name) != null;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004D848 File Offset: 0x0004BA48
		public virtual bool HasAttribute(string localName, string namespaceURI)
		{
			return this.GetAttributeNode(localName, namespaceURI) != null;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0004D858 File Offset: 0x0004BA58
		public override void WriteTo(XmlWriter w)
		{
			if (base.GetType() == typeof(XmlElement))
			{
				XmlElement.WriteElementTo(w, this);
				return;
			}
			this.WriteStartElement(w);
			if (this.IsEmpty)
			{
				w.WriteEndElement();
				return;
			}
			this.WriteContentTo(w);
			w.WriteFullEndElement();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004D8A8 File Offset: 0x0004BAA8
		private static void WriteElementTo(XmlWriter writer, XmlElement e)
		{
			XmlNode xmlNode = e;
			XmlNode xmlNode2 = e;
			for (;;)
			{
				e = (xmlNode2 as XmlElement);
				if (e != null && e.GetType() == typeof(XmlElement))
				{
					e.WriteStartElement(writer);
					if (e.IsEmpty)
					{
						writer.WriteEndElement();
					}
					else
					{
						if (e.lastChild != null)
						{
							xmlNode2 = e.FirstChild;
							continue;
						}
						writer.WriteFullEndElement();
					}
				}
				else
				{
					xmlNode2.WriteTo(writer);
				}
				while (xmlNode2 != xmlNode && xmlNode2 == xmlNode2.ParentNode.LastChild)
				{
					xmlNode2 = xmlNode2.ParentNode;
					writer.WriteFullEndElement();
				}
				if (xmlNode2 == xmlNode)
				{
					break;
				}
				xmlNode2 = xmlNode2.NextSibling;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0004D944 File Offset: 0x0004BB44
		private void WriteStartElement(XmlWriter w)
		{
			w.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
			if (this.HasAttributes)
			{
				XmlAttributeCollection xmlAttributeCollection = this.Attributes;
				for (int i = 0; i < xmlAttributeCollection.Count; i++)
				{
					XmlAttribute xmlAttribute = xmlAttributeCollection[i];
					xmlAttribute.WriteTo(w);
				}
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0004D998 File Offset: 0x0004BB98
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004D9BF File Offset: 0x0004BBBF
		public virtual XmlNode RemoveAttributeAt(int i)
		{
			if (this.HasAttributes)
			{
				return this.attributes.RemoveAt(i);
			}
			return null;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004D9D7 File Offset: 0x0004BBD7
		public virtual void RemoveAllAttributes()
		{
			if (this.HasAttributes)
			{
				this.attributes.RemoveAll();
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004D9EC File Offset: 0x0004BBEC
		public override void RemoveAll()
		{
			base.RemoveAll();
			this.RemoveAllAttributes();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004D9FA File Offset: 0x0004BBFA
		internal void RemoveAllChildren()
		{
			base.RemoveAll();
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0004DA02 File Offset: 0x0004BC02
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0004DA0A File Offset: 0x0004BC0A
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x0004DA14 File Offset: 0x0004BC14
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.LoadInnerXmlElement(this, value);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004DA35 File Offset: 0x0004BC35
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x0004DA40 File Offset: 0x0004BC40
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null && lastNode.NodeType == XmlNodeType.Text && lastNode.next == lastNode)
				{
					lastNode.Value = value;
					return;
				}
				this.RemoveAllChildren();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0004DA8A File Offset: 0x0004BC8A
		public override XmlNode NextSibling
		{
			get
			{
				if (this.parentNode != null && this.parentNode.LastNode != this)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0004DAAA File Offset: 0x0004BCAA
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004DAB3 File Offset: 0x0004BCB3
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004DAB6 File Offset: 0x0004BCB6
		internal override string XPLocalName
		{
			get
			{
				return this.LocalName;
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0004DAC0 File Offset: 0x0004BCC0
		internal override string GetXPAttribute(string localName, string ns)
		{
			if (ns == this.OwnerDocument.strReservedXmlns)
			{
				return null;
			}
			XmlAttribute attributeNode = this.GetAttributeNode(localName, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x04000526 RID: 1318
		private XmlName name;

		// Token: 0x04000527 RID: 1319
		private XmlAttributeCollection attributes;

		// Token: 0x04000528 RID: 1320
		private XmlLinkedNode lastChild;
	}
}
