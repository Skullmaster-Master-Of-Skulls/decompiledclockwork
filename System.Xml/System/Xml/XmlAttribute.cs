using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000CA RID: 202
	public class XmlAttribute : XmlNode
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x000368DC File Offset: 0x000358DC
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00036939 File Offset: 0x00035939
		internal int LocalNameHash
		{
			get
			{
				return this.name.HashCode;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00036946 File Offset: 0x00035946
		protected internal XmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(doc.AddAttrXmlName(prefix, localName, namespaceURI, null), doc)
		{
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0003695B File Offset: 0x0003595B
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00036963 File Offset: 0x00035963
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

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003696C File Offset: 0x0003596C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlAttribute.CopyChildren(ownerDocument, this, true);
			return xmlAttribute;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000369A3 File Offset: 0x000359A3
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x000369A6 File Offset: 0x000359A6
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000369B3 File Offset: 0x000359B3
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000369C0 File Offset: 0x000359C0
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000369CD File Offset: 0x000359CD
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000369DA File Offset: 0x000359DA
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00036A05 File Offset: 0x00035A05
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00036A08 File Offset: 0x00035A08
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00036A15 File Offset: 0x00035A15
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00036A1D File Offset: 0x00035A1D
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

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00036A26 File Offset: 0x00035A26
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00036A30 File Offset: 0x00035A30
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

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00036A64 File Offset: 0x00035A64
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

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00036AA4 File Offset: 0x00035AA4
		internal void ResetOwnerElementInElementIdAttrMap(string oldInnerText)
		{
			XmlElement ownerElement = this.OwnerElement;
			if (ownerElement != null)
			{
				ownerElement.Attributes.ResetParentInElementIdAttrMap(oldInnerText, this.InnerText);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00036ACD File Offset: 0x00035ACD
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00036AD0 File Offset: 0x00035AD0
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

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00036B62 File Offset: 0x00035B62
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00036B6A File Offset: 0x00035B6A
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

		// Token: 0x06000BFC RID: 3068 RVA: 0x00036B73 File Offset: 0x00035B73
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.EntityReference;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00036B7F File Offset: 0x00035B7F
		public virtual bool Specified
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00036B84 File Offset: 0x00035B84
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

		// Token: 0x06000BFF RID: 3071 RVA: 0x00036BBC File Offset: 0x00035BBC
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

		// Token: 0x06000C00 RID: 3072 RVA: 0x00036BF4 File Offset: 0x00035BF4
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

		// Token: 0x06000C01 RID: 3073 RVA: 0x00036C2C File Offset: 0x00035C2C
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

		// Token: 0x06000C02 RID: 3074 RVA: 0x00036C64 File Offset: 0x00035C64
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

		// Token: 0x06000C03 RID: 3075 RVA: 0x00036C9C File Offset: 0x00035C9C
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

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00036CD2 File Offset: 0x00035CD2
		public virtual XmlElement OwnerElement
		{
			get
			{
				return this.parentNode as XmlElement;
			}
		}

		// Token: 0x170002AE RID: 686
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00036CE0 File Offset: 0x00035CE0
		public override string InnerXml
		{
			set
			{
				this.RemoveAll();
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.LoadInnerXmlAttribute(this, value);
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00036D01 File Offset: 0x00035D01
		public override void WriteTo(XmlWriter w)
		{
			w.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			this.WriteContentTo(w);
			w.WriteEndAttribute();
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00036D28 File Offset: 0x00035D28
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00036D4F File Offset: 0x00035D4F
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

		// Token: 0x06000C09 RID: 3081 RVA: 0x00036D6A File Offset: 0x00035D6A
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00036D73 File Offset: 0x00035D73
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

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00036D8A File Offset: 0x00035D8A
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

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00036DA5 File Offset: 0x00035DA5
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00036DB4 File Offset: 0x00035DB4
		internal override string XPLocalName
		{
			get
			{
				string localName = this.name.LocalName;
				if (localName == this.OwnerDocument.strXmlns)
				{
					return string.Empty;
				}
				return localName;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00036DE7 File Offset: 0x00035DE7
		internal bool IsNamespace
		{
			get
			{
				return Ref.Equal(this.name.NamespaceURI, this.name.OwnerDocument.strReservedXmlns);
			}
		}

		// Token: 0x040008ED RID: 2285
		private XmlName name;

		// Token: 0x040008EE RID: 2286
		private XmlLinkedNode lastChild;
	}
}
