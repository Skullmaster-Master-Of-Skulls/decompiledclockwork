using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000FA RID: 250
	public class XmlAttribute : XmlNode
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x00048BE8 File Offset: 0x00046DE8
		internal XmlAttribute(XmlName name, XmlDocument doc) : base(doc)
		{
			this.parentNode = null;
			if (!doc.IsLoading)
			{
				XmlDocument.CheckName(name.Prefix);
				XmlDocument.CheckName(name.LocalName);
			}
			if (name.LocalName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xdom_Attr_Name"));
			}
			this.name = name;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00048C45 File Offset: 0x00046E45
		internal int LocalNameHash
		{
			get
			{
				return this.name.HashCode;
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00048C52 File Offset: 0x00046E52
		protected internal XmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(doc.AddAttrXmlName(prefix, localName, namespaceURI, null), doc)
		{
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00048C67 File Offset: 0x00046E67
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00048C6F File Offset: 0x00046E6F
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

		// Token: 0x06001132 RID: 4402 RVA: 0x00048C78 File Offset: 0x00046E78
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlAttribute.CopyChildren(ownerDocument, this, true);
			return xmlAttribute;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00048CAF File Offset: 0x00046EAF
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00048CB2 File Offset: 0x00046EB2
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00048CBF File Offset: 0x00046EBF
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00048CCC File Offset: 0x00046ECC
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00048CD9 File Offset: 0x00046ED9
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x00048CE6 File Offset: 0x00046EE6
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddAttrXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00048D11 File Offset: 0x00046F11
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00048D14 File Offset: 0x00046F14
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00048D21 File Offset: 0x00046F21
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x00048D29 File Offset: 0x00046F29
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00048D32 File Offset: 0x00046F32
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000347 RID: 839
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00048D3C File Offset: 0x00046F3C
		public override string InnerText
		{
			set
			{
				if (this.PrepareOwnerElementInElementIdAttrMap())
				{
					string innerText = base.InnerText;
					base.InnerText = value;
					this.ResetOwnerElementInElementIdAttrMap(innerText);
					return;
				}
				base.InnerText = value;
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00048D70 File Offset: 0x00046F70
		internal bool PrepareOwnerElementInElementIdAttrMap()
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			if (ownerDocument.DtdSchemaInfo != null)
			{
				XmlElement ownerElement = this.OwnerElement;
				if (ownerElement != null)
				{
					return ownerElement.Attributes.PrepareParentInElementIdAttrMap(this.Prefix, this.LocalName);
				}
			}
			return false;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00048DB0 File Offset: 0x00046FB0
		internal void ResetOwnerElementInElementIdAttrMap(string oldInnerText)
		{
			XmlElement ownerElement = this.OwnerElement;
			if (ownerElement != null)
			{
				ownerElement.Attributes.ResetParentInElementIdAttrMap(oldInnerText, this.InnerText);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00048DD9 File Offset: 0x00046FD9
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00048DDC File Offset: 0x00046FDC
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
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

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00048E6E File Offset: 0x0004706E
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00048E76 File Offset: 0x00047076
		internal override XmlLinkedNode LastNode
		{
			get
			{
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00048E7F File Offset: 0x0004707F
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.EntityReference;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00048E8B File Offset: 0x0004708B
		public virtual bool Specified
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00048E90 File Offset: 0x00047090
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.InsertBefore(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.InsertBefore(newChild, refChild);
			}
			return result;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00048EC8 File Offset: 0x000470C8
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.InsertAfter(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.InsertAfter(newChild, refChild);
			}
			return result;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00048F00 File Offset: 0x00047100
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.ReplaceChild(newChild, oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.ReplaceChild(newChild, oldChild);
			}
			return result;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00048F38 File Offset: 0x00047138
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.RemoveChild(oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.RemoveChild(oldChild);
			}
			return result;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00048F70 File Offset: 0x00047170
		public override XmlNode PrependChild(XmlNode newChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.PrependChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.PrependChild(newChild);
			}
			return result;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00048FA8 File Offset: 0x000471A8
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.AppendChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.AppendChild(newChild);
			}
			return result;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00048FDE File Offset: 0x000471DE
		public virtual XmlElement OwnerElement
		{
			get
			{
				return this.parentNode as XmlElement;
			}
		}

		// Token: 0x1700034C RID: 844
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x00048FEC File Offset: 0x000471EC
		public override string InnerXml
		{
			set
			{
				this.RemoveAll();
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.LoadInnerXmlAttribute(this, value);
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0004900D File Offset: 0x0004720D
		public override void WriteTo(XmlWriter w)
		{
			w.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			this.WriteContentTo(w);
			w.WriteEndAttribute();
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00049034 File Offset: 0x00047234
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0004905B File Offset: 0x0004725B
		public override string BaseURI
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00049076 File Offset: 0x00047276
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004907F File Offset: 0x0004727F
		internal override XmlSpace XmlSpace
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlSpace;
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00049096 File Offset: 0x00047296
		internal override string XmlLang
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlLang;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x000490B1 File Offset: 0x000472B1
		internal override XPathNodeType XPNodeType
		{
			get
			{
				if (this.IsNamespace)
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000490BE File Offset: 0x000472BE
		internal override string XPLocalName
		{
			get
			{
				if (this.name.Prefix.Length == 0 && this.name.LocalName == "xmlns")
				{
					return string.Empty;
				}
				return this.name.LocalName;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x000490FA File Offset: 0x000472FA
		internal bool IsNamespace
		{
			get
			{
				return Ref.Equal(this.name.NamespaceURI, this.name.OwnerDocument.strReservedXmlns);
			}
		}

		// Token: 0x040004CF RID: 1231
		private XmlName name;

		// Token: 0x040004D0 RID: 1232
		private XmlLinkedNode lastChild;
	}
}
