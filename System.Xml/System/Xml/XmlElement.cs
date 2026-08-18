using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000DA RID: 218
	public class XmlElement : XmlLinkedNode
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x0003B4C8 File Offset: 0x0003A4C8
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
			if (name.Prefix.Length >= 3 && !doc.IsLoading && string.Compare(name.Prefix, 0, "xml", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentException(Res.GetString("Xdom_Ele_Prefix"));
			}
			this.name = name;
			if (empty)
			{
				this.lastChild = this;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003B56B File Offset: 0x0003A56B
		protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(doc.AddXmlName(prefix, localName, namespaceURI, null), true, doc)
		{
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0003B581 File Offset: 0x0003A581
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x0003B589 File Offset: 0x0003A589
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

		// Token: 0x06000D4D RID: 3405 RVA: 0x0003B594 File Offset: 0x0003A594
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

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0003B68C File Offset: 0x0003A68C
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003B699 File Offset: 0x0003A699
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0003B6A6 File Offset: 0x0003A6A6
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0003B6B3 File Offset: 0x0003A6B3
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x0003B6C0 File Offset: 0x0003A6C0
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

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0003B6EB File Offset: 0x0003A6EB
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0003B6EE File Offset: 0x0003A6EE
		public override XmlNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0003B6F6 File Offset: 0x0003A6F6
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0003B703 File Offset: 0x0003A703
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003B708 File Offset: 0x0003A708
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0003B7A3 File Offset: 0x0003A7A3
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0003B7AE File Offset: 0x0003A7AE
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

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0003B7DA File Offset: 0x0003A7DA
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0003B7ED File Offset: 0x0003A7ED
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

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003B7F8 File Offset: 0x0003A7F8
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

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003B84C File Offset: 0x0003A84C
		public override XmlAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					lock (this.OwnerDocument.objLock)
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

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003B8A8 File Offset: 0x0003A8A8
		public virtual bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003B8C4 File Offset: 0x0003A8C4
		public virtual string GetAttribute(string name)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003B8E8 File Offset: 0x0003A8E8
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

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003B929 File Offset: 0x0003A929
		public virtual void RemoveAttribute(string name)
		{
			if (this.HasAttributes)
			{
				this.Attributes.RemoveNamedItem(name);
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003B940 File Offset: 0x0003A940
		public virtual XmlAttribute GetAttributeNode(string name)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[name];
			}
			return null;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003B958 File Offset: 0x0003A958
		public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr)
		{
			if (newAttr.OwnerElement != null)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Attr_InUse"));
			}
			return (XmlAttribute)this.Attributes.SetNamedItem(newAttr);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003B983 File Offset: 0x0003A983
		public virtual XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr)
		{
			if (this.HasAttributes)
			{
				return this.Attributes.Remove(oldAttr);
			}
			return null;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003B99B File Offset: 0x0003A99B
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003B9A4 File Offset: 0x0003A9A4
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003B9CC File Offset: 0x0003A9CC
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

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003BA16 File Offset: 0x0003AA16
		public virtual void RemoveAttribute(string localName, string namespaceURI)
		{
			this.RemoveAttributeNode(localName, namespaceURI);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003BA21 File Offset: 0x0003AA21
		public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[localName, namespaceURI];
			}
			return null;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0003BA3C File Offset: 0x0003AA3C
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

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003BA78 File Offset: 0x0003AA78
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

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003BAA6 File Offset: 0x0003AAA6
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003BAB0 File Offset: 0x0003AAB0
		public virtual bool HasAttribute(string name)
		{
			return this.GetAttributeNode(name) != null;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003BABF File Offset: 0x0003AABF
		public virtual bool HasAttribute(string localName, string namespaceURI)
		{
			return this.GetAttributeNode(localName, namespaceURI) != null;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003BACF File Offset: 0x0003AACF
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

		// Token: 0x06000D70 RID: 3440 RVA: 0x0003BB10 File Offset: 0x0003AB10
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

		// Token: 0x06000D71 RID: 3441 RVA: 0x0003BBA8 File Offset: 0x0003ABA8
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

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003BBFC File Offset: 0x0003ABFC
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003BC23 File Offset: 0x0003AC23
		public virtual XmlNode RemoveAttributeAt(int i)
		{
			if (this.HasAttributes)
			{
				return this.attributes.RemoveAt(i);
			}
			return null;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003BC3B File Offset: 0x0003AC3B
		public virtual void RemoveAllAttributes()
		{
			if (this.HasAttributes)
			{
				this.attributes.RemoveAll();
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003BC50 File Offset: 0x0003AC50
		public override void RemoveAll()
		{
			base.RemoveAll();
			this.RemoveAllAttributes();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003BC5E File Offset: 0x0003AC5E
		internal void RemoveAllChildren()
		{
			base.RemoveAll();
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0003BC66 File Offset: 0x0003AC66
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003BC6E File Offset: 0x0003AC6E
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0003BC78 File Offset: 0x0003AC78
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

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0003BC99 File Offset: 0x0003AC99
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0003BCA4 File Offset: 0x0003ACA4
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

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0003BCEE File Offset: 0x0003ACEE
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

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003BD0E File Offset: 0x0003AD0E
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0003BD17 File Offset: 0x0003AD17
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0003BD1A File Offset: 0x0003AD1A
		internal override string XPLocalName
		{
			get
			{
				return this.LocalName;
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003BD24 File Offset: 0x0003AD24
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

		// Token: 0x04000947 RID: 2375
		private XmlName name;

		// Token: 0x04000948 RID: 2376
		private XmlAttributeCollection attributes;

		// Token: 0x04000949 RID: 2377
		private XmlLinkedNode lastChild;
	}
}
