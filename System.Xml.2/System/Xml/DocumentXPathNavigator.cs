using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F0 RID: 240
	internal sealed class DocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x060010AA RID: 4266 RVA: 0x00046560 File Offset: 0x00044760
		public DocumentXPathNavigator(XmlDocument document, XmlNode node)
		{
			this.document = document;
			this.ResetPosition(node);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00046576 File Offset: 0x00044776
		public DocumentXPathNavigator(DocumentXPathNavigator other)
		{
			this.document = other.document;
			this.source = other.source;
			this.attributeIndex = other.attributeIndex;
			this.namespaceParent = other.namespaceParent;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000465AE File Offset: 0x000447AE
		public override XPathNavigator Clone()
		{
			return new DocumentXPathNavigator(this);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000465B8 File Offset: 0x000447B8
		public override void SetValue(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNode xmlNode = this.source;
			switch (xmlNode.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				break;
			case XmlNodeType.Attribute:
				if (!((XmlAttribute)xmlNode).IsNamespace)
				{
					xmlNode.InnerText = value;
					return;
				}
				goto IL_B8;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			{
				this.CalibrateText();
				xmlNode = this.source;
				XmlNode xmlNode2 = this.TextEnd(xmlNode);
				if (xmlNode != xmlNode2)
				{
					if (xmlNode.IsReadOnly)
					{
						throw new InvalidOperationException(Res.GetString("Xdom_Node_Modify_ReadOnly"));
					}
					DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode.NextSibling, xmlNode2);
				}
				break;
			}
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentType:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				goto IL_B8;
			default:
				goto IL_B8;
			}
			xmlNode.InnerText = value;
			return;
			IL_B8:
			throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0004668C File Offset: 0x0004488C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.document.NameTable;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00046699 File Offset: 0x00044899
		public override XPathNodeType NodeType
		{
			get
			{
				this.CalibrateText();
				return this.source.XPNodeType;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x000466AC File Offset: 0x000448AC
		public override string LocalName
		{
			get
			{
				return this.source.XPLocalName;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x000466BC File Offset: 0x000448BC
		public override string NamespaceURI
		{
			get
			{
				XmlAttribute xmlAttribute = this.source as XmlAttribute;
				if (xmlAttribute != null && xmlAttribute.IsNamespace)
				{
					return string.Empty;
				}
				return this.source.NamespaceURI;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x000466F4 File Offset: 0x000448F4
		public override string Name
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.ProcessingInstruction)
						{
							return string.Empty;
						}
					}
					else
					{
						if (!((XmlAttribute)this.source).IsNamespace)
						{
							return this.source.Name;
						}
						string localName = this.source.LocalName;
						if (Ref.Equal(localName, this.document.strXmlns))
						{
							return string.Empty;
						}
						return localName;
					}
				}
				return this.source.Name;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00046770 File Offset: 0x00044970
		public override string Prefix
		{
			get
			{
				XmlAttribute xmlAttribute = this.source as XmlAttribute;
				if (xmlAttribute != null && xmlAttribute.IsNamespace)
				{
					return string.Empty;
				}
				return this.source.Prefix;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x000467A8 File Offset: 0x000449A8
		public override string Value
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						switch (nodeType)
						{
						case XmlNodeType.Document:
							return this.ValueDocument;
						case XmlNodeType.DocumentFragment:
							goto IL_39;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							goto IL_4C;
						}
						return this.source.Value;
					}
					IL_4C:
					return this.ValueText;
				}
				IL_39:
				return this.source.InnerText;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00046814 File Offset: 0x00044A14
		private string ValueDocument
		{
			get
			{
				XmlElement documentElement = this.document.DocumentElement;
				if (documentElement != null)
				{
					return documentElement.InnerText;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0004683C File Offset: 0x00044A3C
		private string ValueText
		{
			get
			{
				this.CalibrateText();
				string text = this.source.Value;
				XmlNode xmlNode = this.NextSibling(this.source);
				if (xmlNode != null && xmlNode.IsText)
				{
					StringBuilder stringBuilder = new StringBuilder(text);
					do
					{
						stringBuilder.Append(xmlNode.Value);
						xmlNode = this.NextSibling(xmlNode);
					}
					while (xmlNode != null && xmlNode.IsText);
					text = stringBuilder.ToString();
				}
				return text;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x000468A2 File Offset: 0x00044AA2
		public override string BaseURI
		{
			get
			{
				return this.source.BaseURI;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x000468B0 File Offset: 0x00044AB0
		public override bool IsEmptyElement
		{
			get
			{
				XmlElement xmlElement = this.source as XmlElement;
				return xmlElement != null && xmlElement.IsEmpty;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x000468D4 File Offset: 0x00044AD4
		public override string XmlLang
		{
			get
			{
				return this.source.XmlLang;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x000468E1 File Offset: 0x00044AE1
		public override object UnderlyingObject
		{
			get
			{
				this.CalibrateText();
				return this.source;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x000468F0 File Offset: 0x00044AF0
		public override bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this.source as XmlElement;
				if (xmlElement != null && xmlElement.HasAttributes)
				{
					XmlAttributeCollection attributes = xmlElement.Attributes;
					for (int i = 0; i < attributes.Count; i++)
					{
						XmlAttribute xmlAttribute = attributes[i];
						if (!xmlAttribute.IsNamespace)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004693F File Offset: 0x00044B3F
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.source.GetXPAttribute(localName, namespaceURI);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00046950 File Offset: 0x00044B50
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null && xmlElement.HasAttributes)
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				int i = 0;
				while (i < attributes.Count)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (xmlAttribute.LocalName == localName && xmlAttribute.NamespaceURI == namespaceURI)
					{
						if (!xmlAttribute.IsNamespace)
						{
							this.source = xmlAttribute;
							this.attributeIndex = i;
							return true;
						}
						return false;
					}
					else
					{
						i++;
					}
				}
			}
			return false;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000469CC File Offset: 0x00044BCC
		public override bool MoveToFirstAttribute()
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null && xmlElement.HasAttributes)
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (!xmlAttribute.IsNamespace)
					{
						this.source = xmlAttribute;
						this.attributeIndex = i;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00046A2C File Offset: 0x00044C2C
		public override bool MoveToNextAttribute()
		{
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute == null || xmlAttribute.IsNamespace)
			{
				return false;
			}
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(xmlAttribute, out xmlAttributeCollection, this.attributeIndex) && !DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, xmlAttributeCollection, out this.attributeIndex))
			{
				return false;
			}
			for (int i = this.attributeIndex + 1; i < xmlAttributeCollection.Count; i++)
			{
				xmlAttribute = xmlAttributeCollection[i];
				if (!xmlAttribute.IsNamespace)
				{
					this.source = xmlAttribute;
					this.attributeIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00046AAC File Offset: 0x00044CAC
		public override string GetNamespace(string name)
		{
			XmlNode xmlNode = this.source;
			while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element)
			{
				XmlAttribute xmlAttribute = xmlNode as XmlAttribute;
				if (xmlAttribute != null)
				{
					xmlNode = xmlAttribute.OwnerElement;
				}
				else
				{
					xmlNode = xmlNode.ParentNode;
				}
			}
			XmlElement xmlElement = xmlNode as XmlElement;
			if (xmlElement != null)
			{
				string localName;
				if (name != null && name.Length != 0)
				{
					localName = name;
				}
				else
				{
					localName = this.document.strXmlns;
				}
				string strReservedXmlns = this.document.strReservedXmlns;
				XmlAttribute attributeNode;
				for (;;)
				{
					attributeNode = xmlElement.GetAttributeNode(localName, strReservedXmlns);
					if (attributeNode != null)
					{
						break;
					}
					xmlElement = (xmlElement.ParentNode as XmlElement);
					if (xmlElement == null)
					{
						goto IL_87;
					}
				}
				return attributeNode.Value;
			}
			IL_87:
			if (name == this.document.strXml)
			{
				return this.document.strReservedXml;
			}
			if (name == this.document.strXmlns)
			{
				return this.document.strReservedXmlns;
			}
			return string.Empty;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00046B84 File Offset: 0x00044D84
		public override bool MoveToNamespace(string name)
		{
			if (name == this.document.strXmlns)
			{
				return false;
			}
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null)
			{
				string localName;
				if (name != null && name.Length != 0)
				{
					localName = name;
				}
				else
				{
					localName = this.document.strXmlns;
				}
				string strReservedXmlns = this.document.strReservedXmlns;
				XmlAttribute attributeNode;
				for (;;)
				{
					attributeNode = xmlElement.GetAttributeNode(localName, strReservedXmlns);
					if (attributeNode != null)
					{
						break;
					}
					xmlElement = (xmlElement.ParentNode as XmlElement);
					if (xmlElement == null)
					{
						goto Block_6;
					}
				}
				this.namespaceParent = (XmlElement)this.source;
				this.source = attributeNode;
				return true;
				Block_6:
				if (name == this.document.strXml)
				{
					this.namespaceParent = (XmlElement)this.source;
					this.source = this.document.NamespaceXml;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00046C4C File Offset: 0x00044E4C
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement == null)
			{
				return false;
			}
			int maxValue = int.MaxValue;
			switch (scope)
			{
			case XPathNamespaceScope.All:
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceGlobal(ref attributes, ref maxValue))
				{
					this.source = this.document.NamespaceXml;
				}
				else
				{
					this.source = attributes[maxValue];
					this.attributeIndex = maxValue;
				}
				this.namespaceParent = xmlElement;
				break;
			}
			case XPathNamespaceScope.ExcludeXml:
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceGlobal(ref attributes, ref maxValue))
				{
					return false;
				}
				XmlAttribute xmlAttribute = attributes[maxValue];
				while (Ref.Equal(xmlAttribute.LocalName, this.document.strXml))
				{
					if (!DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref attributes, ref maxValue))
					{
						return false;
					}
					xmlAttribute = attributes[maxValue];
				}
				this.source = xmlAttribute;
				this.attributeIndex = maxValue;
				this.namespaceParent = xmlElement;
				break;
			}
			case XPathNamespaceScope.Local:
			{
				if (!xmlElement.HasAttributes)
				{
					return false;
				}
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref maxValue))
				{
					return false;
				}
				this.source = attributes[maxValue];
				this.attributeIndex = maxValue;
				this.namespaceParent = xmlElement;
				break;
			}
			default:
				return false;
			}
			return true;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00046D6C File Offset: 0x00044F6C
		private static bool MoveToFirstNamespaceLocal(XmlAttributeCollection attributes, ref int index)
		{
			for (int i = attributes.Count - 1; i >= 0; i--)
			{
				XmlAttribute xmlAttribute = attributes[i];
				if (xmlAttribute.IsNamespace)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00046DA4 File Offset: 0x00044FA4
		private static bool MoveToFirstNamespaceGlobal(ref XmlAttributeCollection attributes, ref int index)
		{
			if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
			{
				return true;
			}
			for (XmlElement xmlElement = attributes.parent.ParentNode as XmlElement; xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
			{
				if (xmlElement.HasAttributes)
				{
					attributes = xmlElement.Attributes;
					if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00046E00 File Offset: 0x00045000
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute == null || !xmlAttribute.IsNamespace)
			{
				return false;
			}
			int num = this.attributeIndex;
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(xmlAttribute, out xmlAttributeCollection, num) && !DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, xmlAttributeCollection, out num))
			{
				return false;
			}
			switch (scope)
			{
			case XPathNamespaceScope.All:
				while (DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref num))
				{
					xmlAttribute = xmlAttributeCollection[num];
					if (!this.PathHasDuplicateNamespace(xmlAttribute.OwnerElement, this.namespaceParent, xmlAttribute.LocalName))
					{
						this.source = xmlAttribute;
						this.attributeIndex = num;
						return true;
					}
				}
				if (this.PathHasDuplicateNamespace(null, this.namespaceParent, this.document.strXml))
				{
					return false;
				}
				this.source = this.document.NamespaceXml;
				return true;
			case XPathNamespaceScope.ExcludeXml:
				while (DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref num))
				{
					xmlAttribute = xmlAttributeCollection[num];
					string localName = xmlAttribute.LocalName;
					if (!this.PathHasDuplicateNamespace(xmlAttribute.OwnerElement, this.namespaceParent, localName) && !Ref.Equal(localName, this.document.strXml))
					{
						this.source = xmlAttribute;
						this.attributeIndex = num;
						return true;
					}
				}
				return false;
			case XPathNamespaceScope.Local:
				if (xmlAttribute.OwnerElement != this.namespaceParent)
				{
					return false;
				}
				if (!DocumentXPathNavigator.MoveToNextNamespaceLocal(xmlAttributeCollection, ref num))
				{
					return false;
				}
				this.source = xmlAttributeCollection[num];
				this.attributeIndex = num;
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00046F54 File Offset: 0x00045154
		private static bool MoveToNextNamespaceLocal(XmlAttributeCollection attributes, ref int index)
		{
			for (int i = index - 1; i >= 0; i--)
			{
				XmlAttribute xmlAttribute = attributes[i];
				if (xmlAttribute.IsNamespace)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00046F88 File Offset: 0x00045188
		private static bool MoveToNextNamespaceGlobal(ref XmlAttributeCollection attributes, ref int index)
		{
			if (DocumentXPathNavigator.MoveToNextNamespaceLocal(attributes, ref index))
			{
				return true;
			}
			for (XmlElement xmlElement = attributes.parent.ParentNode as XmlElement; xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
			{
				if (xmlElement.HasAttributes)
				{
					attributes = xmlElement.Attributes;
					if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00046FE4 File Offset: 0x000451E4
		private bool PathHasDuplicateNamespace(XmlElement top, XmlElement bottom, string localName)
		{
			string strReservedXmlns = this.document.strReservedXmlns;
			while (bottom != null && bottom != top)
			{
				XmlAttribute attributeNode = bottom.GetAttributeNode(localName, strReservedXmlns);
				if (attributeNode != null)
				{
					return true;
				}
				bottom = (bottom.ParentNode as XmlElement);
			}
			return false;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00047024 File Offset: 0x00045224
		public override string LookupNamespace(string prefix)
		{
			string text = base.LookupNamespace(prefix);
			if (text != null)
			{
				text = this.NameTable.Add(text);
			}
			return text;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0004704C File Offset: 0x0004524C
		public override bool MoveToNext()
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText && this.source.IsText)
			{
				xmlNode = this.NextSibling(this.TextEnd(xmlNode));
				if (xmlNode == null)
				{
					return false;
				}
			}
			XmlNode parent = this.ParentNode(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(parent, xmlNode))
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000470BC File Offset: 0x000452BC
		public override bool MoveToPrevious()
		{
			XmlNode xmlNode = this.PreviousSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText)
			{
				if (this.source.IsText)
				{
					xmlNode = this.PreviousSibling(this.TextStart(xmlNode));
					if (xmlNode == null)
					{
						return false;
					}
				}
				else
				{
					xmlNode = this.TextStart(xmlNode);
				}
			}
			XmlNode parent = this.ParentNode(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(parent, xmlNode))
			{
				xmlNode = this.PreviousSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00047134 File Offset: 0x00045334
		public override bool MoveToFirst()
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.ParentNode(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			XmlNode xmlNode2 = this.FirstChild(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(xmlNode, xmlNode2))
			{
				xmlNode2 = this.NextSibling(xmlNode2);
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0004718C File Offset: 0x0004538C
		public override bool MoveToFirstChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			XmlNode xmlNode;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
				{
					return false;
				}
				xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
				while (!DocumentXPathNavigator.IsValidChild(this.source, xmlNode))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
			}
			else
			{
				xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00047200 File Offset: 0x00045400
		public override bool MoveToParent()
		{
			XmlNode xmlNode = this.ParentNode(this.source);
			if (xmlNode != null)
			{
				this.source = xmlNode;
				return true;
			}
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute != null)
			{
				xmlNode = (xmlAttribute.IsNamespace ? this.namespaceParent : xmlAttribute.OwnerElement);
				if (xmlNode != null)
				{
					this.source = xmlNode;
					this.namespaceParent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00047260 File Offset: 0x00045460
		public override void MoveToRoot()
		{
			for (;;)
			{
				XmlNode xmlNode = this.source.ParentNode;
				if (xmlNode == null)
				{
					XmlAttribute xmlAttribute = this.source as XmlAttribute;
					if (xmlAttribute == null)
					{
						break;
					}
					xmlNode = (xmlAttribute.IsNamespace ? this.namespaceParent : xmlAttribute.OwnerElement);
					if (xmlNode == null)
					{
						break;
					}
				}
				this.source = xmlNode;
			}
			this.namespaceParent = null;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000472B8 File Offset: 0x000454B8
		public override bool MoveTo(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator != null && this.document == documentXPathNavigator.document)
			{
				this.source = documentXPathNavigator.source;
				this.attributeIndex = documentXPathNavigator.attributeIndex;
				this.namespaceParent = documentXPathNavigator.namespaceParent;
				return true;
			}
			return false;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00047304 File Offset: 0x00045504
		public override bool MoveToId(string id)
		{
			XmlElement elementById = this.document.GetElementById(id);
			if (elementById != null)
			{
				this.source = elementById;
				this.namespaceParent = null;
				return true;
			}
			return false;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00047334 File Offset: 0x00045534
		public override bool MoveToChild(string localName, string namespaceUri)
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.FirstChild(this.source);
			if (xmlNode != null)
			{
				while (xmlNode.NodeType != XmlNodeType.Element || !(xmlNode.LocalName == localName) || !(xmlNode.NamespaceURI == namespaceUri))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				this.source = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0004739C File Offset: 0x0004559C
		public override bool MoveToChild(XPathNodeType type)
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.FirstChild(this.source);
			if (xmlNode != null)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				if (contentKindMask == 0)
				{
					return false;
				}
				while ((1 << (int)xmlNode.XPNodeType & contentKindMask) == 0)
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				this.source = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000473FC File Offset: 0x000455FC
		public override bool MoveToFollowing(string localName, string namespaceUri, XPathNavigator end)
		{
			XmlNode xmlNode = null;
			DocumentXPathNavigator documentXPathNavigator = end as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				if (this.document != documentXPathNavigator.document)
				{
					return false;
				}
				XmlNodeType nodeType = documentXPathNavigator.source.NodeType;
				if (nodeType == XmlNodeType.Attribute)
				{
					documentXPathNavigator = (DocumentXPathNavigator)documentXPathNavigator.Clone();
					if (!documentXPathNavigator.MoveToNonDescendant())
					{
						return false;
					}
				}
				xmlNode = documentXPathNavigator.source;
			}
			XmlNode xmlNode2 = this.source;
			if (xmlNode2.NodeType == XmlNodeType.Attribute)
			{
				xmlNode2 = ((XmlAttribute)xmlNode2).OwnerElement;
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			for (;;)
			{
				XmlNode firstChild = xmlNode2.FirstChild;
				if (firstChild != null)
				{
					xmlNode2 = firstChild;
				}
				else
				{
					XmlNode nextSibling;
					for (;;)
					{
						nextSibling = xmlNode2.NextSibling;
						if (nextSibling != null)
						{
							break;
						}
						XmlNode parentNode = xmlNode2.ParentNode;
						if (parentNode == null)
						{
							return false;
						}
						xmlNode2 = parentNode;
					}
					xmlNode2 = nextSibling;
				}
				if (xmlNode2 == xmlNode)
				{
					return false;
				}
				if (xmlNode2.NodeType == XmlNodeType.Element && !(xmlNode2.LocalName != localName) && !(xmlNode2.NamespaceURI != namespaceUri))
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000474DC File Offset: 0x000456DC
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XmlNode xmlNode = null;
			DocumentXPathNavigator documentXPathNavigator = end as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				if (this.document != documentXPathNavigator.document)
				{
					return false;
				}
				XmlNodeType nodeType = documentXPathNavigator.source.NodeType;
				if (nodeType == XmlNodeType.Attribute)
				{
					documentXPathNavigator = (DocumentXPathNavigator)documentXPathNavigator.Clone();
					if (!documentXPathNavigator.MoveToNonDescendant())
					{
						return false;
					}
				}
				xmlNode = documentXPathNavigator.source;
			}
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (contentKindMask == 0)
			{
				return false;
			}
			XmlNode xmlNode2 = this.source;
			XmlNodeType nodeType2 = xmlNode2.NodeType;
			if (nodeType2 != XmlNodeType.Attribute)
			{
				if (nodeType2 - XmlNodeType.Text <= 1 || nodeType2 - XmlNodeType.Whitespace <= 1)
				{
					xmlNode2 = this.TextEnd(xmlNode2);
				}
			}
			else
			{
				xmlNode2 = ((XmlAttribute)xmlNode2).OwnerElement;
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			for (;;)
			{
				XmlNode firstChild = xmlNode2.FirstChild;
				if (firstChild != null)
				{
					xmlNode2 = firstChild;
				}
				else
				{
					XmlNode nextSibling;
					for (;;)
					{
						nextSibling = xmlNode2.NextSibling;
						if (nextSibling != null)
						{
							break;
						}
						XmlNode parentNode = xmlNode2.ParentNode;
						if (parentNode == null)
						{
							return false;
						}
						xmlNode2 = parentNode;
					}
					xmlNode2 = nextSibling;
				}
				if (xmlNode2 == xmlNode)
				{
					return false;
				}
				if ((1 << (int)xmlNode2.XPNodeType & contentKindMask) != 0)
				{
					goto Block_14;
				}
			}
			return false;
			Block_14:
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000475D0 File Offset: 0x000457D0
		public override bool MoveToNext(string localName, string namespaceUri)
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			while (xmlNode.NodeType != XmlNodeType.Element || !(xmlNode.LocalName == localName) || !(xmlNode.NamespaceURI == namespaceUri))
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0004762C File Offset: 0x0004582C
		public override bool MoveToNext(XPathNodeType type)
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText && this.source.IsText)
			{
				xmlNode = this.NextSibling(this.TextEnd(xmlNode));
				if (xmlNode == null)
				{
					return false;
				}
			}
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (contentKindMask == 0)
			{
				return false;
			}
			while ((1 << (int)xmlNode.XPNodeType & contentKindMask) == 0)
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x000476A4 File Offset: 0x000458A4
		public override bool HasChildren
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return this.FirstChild(this.source) != null;
				}
				if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
				{
					return false;
				}
				XmlNode xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
				while (!DocumentXPathNavigator.IsValidChild(this.source, xmlNode))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00047714 File Offset: 0x00045914
		public override bool IsSamePosition(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				return this.source == documentXPathNavigator.source && this.namespaceParent == documentXPathNavigator.namespaceParent;
			}
			return false;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00047758 File Offset: 0x00045958
		public override bool IsDescendant(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			return documentXPathNavigator != null && DocumentXPathNavigator.IsDescendant(this.source, documentXPathNavigator.source);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00047782 File Offset: 0x00045982
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.source.SchemaInfo;
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00047790 File Offset: 0x00045990
		public override bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			XmlDocument xmlDocument;
			if (this.source.NodeType == XmlNodeType.Document)
			{
				xmlDocument = (XmlDocument)this.source;
			}
			else
			{
				xmlDocument = this.source.OwnerDocument;
				if (schemas != null)
				{
					throw new ArgumentException(Res.GetString("XPathDocument_SchemaSetNotAllowed", null));
				}
			}
			if (schemas == null && xmlDocument != null)
			{
				schemas = xmlDocument.Schemas;
			}
			if (schemas == null || schemas.Count == 0)
			{
				throw new InvalidOperationException(Res.GetString("XmlDocument_NoSchemaInfo"));
			}
			return new DocumentSchemaValidator(xmlDocument, schemas, validationEventHandler)
			{
				PsviAugmentation = false
			}.Validate(this.source);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00047820 File Offset: 0x00045A20
		private static XmlNode OwnerNode(XmlNode node)
		{
			XmlNode parentNode = node.ParentNode;
			if (parentNode != null)
			{
				return parentNode;
			}
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				return xmlAttribute.OwnerElement;
			}
			return null;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004784C File Offset: 0x00045A4C
		private static int GetDepth(XmlNode node)
		{
			int num = 0;
			for (XmlNode node2 = DocumentXPathNavigator.OwnerNode(node); node2 != null; node2 = DocumentXPathNavigator.OwnerNode(node2))
			{
				num++;
			}
			return num;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00047874 File Offset: 0x00045A74
		private XmlNodeOrder Compare(XmlNode node1, XmlNode node2)
		{
			if (node1.XPNodeType == XPathNodeType.Attribute)
			{
				if (node2.XPNodeType == XPathNodeType.Attribute)
				{
					XmlElement ownerElement = ((XmlAttribute)node1).OwnerElement;
					if (ownerElement.HasAttributes)
					{
						XmlAttributeCollection attributes = ownerElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute == node1)
							{
								return XmlNodeOrder.Before;
							}
							if (xmlAttribute == node2)
							{
								return XmlNodeOrder.After;
							}
						}
					}
					return XmlNodeOrder.Unknown;
				}
				return XmlNodeOrder.Before;
			}
			else
			{
				if (node2.XPNodeType == XPathNodeType.Attribute)
				{
					return XmlNodeOrder.After;
				}
				XmlNode nextSibling = node1.NextSibling;
				while (nextSibling != null && nextSibling != node2)
				{
					nextSibling = nextSibling.NextSibling;
				}
				if (nextSibling == null)
				{
					return XmlNodeOrder.After;
				}
				return XmlNodeOrder.Before;
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00047904 File Offset: 0x00045B04
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator == null)
			{
				return XmlNodeOrder.Unknown;
			}
			this.CalibrateText();
			documentXPathNavigator.CalibrateText();
			if (this.source == documentXPathNavigator.source && this.namespaceParent == documentXPathNavigator.namespaceParent)
			{
				return XmlNodeOrder.Same;
			}
			if (this.namespaceParent != null || documentXPathNavigator.namespaceParent != null)
			{
				return base.ComparePosition(other);
			}
			XmlNode xmlNode = this.source;
			XmlNode xmlNode2 = documentXPathNavigator.source;
			XmlNode xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
			XmlNode xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
			if (xmlNode3 != xmlNode4)
			{
				int num = DocumentXPathNavigator.GetDepth(xmlNode);
				int num2 = DocumentXPathNavigator.GetDepth(xmlNode2);
				if (num2 > num)
				{
					while (xmlNode2 != null && num2 > num)
					{
						xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlNode2);
						num2--;
					}
					if (xmlNode == xmlNode2)
					{
						return XmlNodeOrder.Before;
					}
					xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
				}
				else if (num > num2)
				{
					while (xmlNode != null && num > num2)
					{
						xmlNode = DocumentXPathNavigator.OwnerNode(xmlNode);
						num--;
					}
					if (xmlNode == xmlNode2)
					{
						return XmlNodeOrder.After;
					}
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
				}
				while (xmlNode3 != null && xmlNode4 != null)
				{
					if (xmlNode3 == xmlNode4)
					{
						return this.Compare(xmlNode, xmlNode2);
					}
					xmlNode = xmlNode3;
					xmlNode2 = xmlNode4;
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
					xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
				}
				return XmlNodeOrder.Unknown;
			}
			if (xmlNode3 == null)
			{
				return XmlNodeOrder.Unknown;
			}
			return this.Compare(xmlNode, xmlNode2);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00047A24 File Offset: 0x00045C24
		XmlNode IHasXmlNode.GetNode()
		{
			return this.source;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00047A2C File Offset: 0x00045C2C
		public override XPathNodeIterator SelectDescendants(string localName, string namespaceURI, bool matchSelf)
		{
			string text = this.document.NameTable.Get(namespaceURI);
			if (text == null || this.source.NodeType == XmlNodeType.Attribute)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			string text2 = this.document.NameTable.Get(localName);
			if (text2 == null)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			if (text2.Length == 0)
			{
				if (matchSelf)
				{
					return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this, text);
				}
				return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this, text);
			}
			else
			{
				if (matchSelf)
				{
					return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this, text2, text);
				}
				return new DocumentXPathNodeIterator_ElemChildren(this, text2, text);
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00047AB0 File Offset: 0x00045CB0
		public override XPathNodeIterator SelectDescendants(XPathNodeType nt, bool includeSelf)
		{
			if (nt != XPathNodeType.Element)
			{
				return base.SelectDescendants(nt, includeSelf);
			}
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.Element)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			if (includeSelf)
			{
				return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
			}
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00047AF6 File Offset: 0x00045CF6
		public override bool CanEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00047AFC File Offset: 0x00045CFC
		public override XmlWriter PrependChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Element && nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.PrependChild, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00047B6C File Offset: 0x00045D6C
		public override XmlWriter AppendChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Element && nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.AppendChild, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00047BDC File Offset: 0x00045DDC
		public override XmlWriter InsertAfter()
		{
			XmlNode xmlNode = this.source;
			switch (xmlNode.NodeType)
			{
			case XmlNodeType.Attribute:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				xmlNode = this.TextEnd(xmlNode);
				break;
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.InsertSiblingAfter, xmlNode, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(xmlNode.ParentNode, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00047C80 File Offset: 0x00045E80
		public override XmlWriter InsertBefore()
		{
			switch (this.source.NodeType)
			{
			case XmlNodeType.Attribute:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.CalibrateText();
				break;
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.InsertSiblingBefore, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source.ParentNode, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00047D28 File Offset: 0x00045F28
		public override XmlWriter CreateAttributes()
		{
			if (this.source.NodeType != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.AppendAttribute, this.source, this.document);
			documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(this.source, this.document);
			return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00047D8C File Offset: 0x00045F8C
		public override XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
		{
			DocumentXPathNavigator documentXPathNavigator = lastSiblingToReplace as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				XmlNode xmlNode = this.source;
				XmlNode xmlNode2 = documentXPathNavigator.source;
				if (xmlNode == xmlNode2)
				{
					switch (xmlNode.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentFragment:
						throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
						break;
					}
				}
				else
				{
					if (xmlNode2.IsText)
					{
						xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
					}
					if (!DocumentXPathNavigator.IsFollowingSibling(xmlNode, xmlNode2))
					{
						throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
					}
				}
				DocumentXmlWriter documentXmlWriter = new DocumentXmlWriter(DocumentXmlWriterType.ReplaceToFollowingSibling, xmlNode, this.document);
				documentXmlWriter.NamespaceManager = DocumentXPathNavigator.GetNamespaceManager(xmlNode.ParentNode, this.document);
				documentXmlWriter.Navigator = this;
				documentXmlWriter.EndNode = xmlNode2;
				return new XmlWellFormedWriter(documentXmlWriter, documentXmlWriter.Settings);
			}
			if (lastSiblingToReplace == null)
			{
				throw new ArgumentNullException("lastSiblingToReplace");
			}
			throw new NotSupportedException();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00047EA0 File Offset: 0x000460A0
		public override void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			DocumentXPathNavigator documentXPathNavigator = lastSiblingToDelete as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				XmlNode xmlNode = this.source;
				XmlNode xmlNode2 = documentXPathNavigator.source;
				if (xmlNode == xmlNode2)
				{
					XmlNode xmlNode3;
					switch (xmlNode.NodeType)
					{
					case XmlNodeType.Element:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
						break;
					case XmlNodeType.Attribute:
					{
						XmlAttribute xmlAttribute = (XmlAttribute)xmlNode;
						if (xmlAttribute.IsNamespace)
						{
							goto IL_E1;
						}
						xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlAttribute);
						DocumentXPathNavigator.DeleteAttribute(xmlAttribute, this.attributeIndex);
						if (xmlNode3 != null)
						{
							this.ResetPosition(xmlNode3);
							return;
						}
						return;
					}
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
						break;
					case XmlNodeType.EntityReference:
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentType:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
						goto IL_E1;
					default:
						goto IL_E1;
					}
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
					DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode, xmlNode2);
					if (xmlNode3 != null)
					{
						this.ResetPosition(xmlNode3);
						return;
					}
					return;
					IL_E1:
					throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
				}
				if (xmlNode2.IsText)
				{
					xmlNode2 = documentXPathNavigator.TextEnd(xmlNode2);
				}
				if (!DocumentXPathNavigator.IsFollowingSibling(xmlNode, xmlNode2))
				{
					throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
				}
				XmlNode xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode);
				DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode, xmlNode2);
				if (xmlNode4 != null)
				{
					this.ResetPosition(xmlNode4);
				}
				return;
			}
			if (lastSiblingToDelete == null)
			{
				throw new ArgumentNullException("lastSiblingToDelete");
			}
			throw new NotSupportedException();
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00047FE4 File Offset: 0x000461E4
		public override void DeleteSelf()
		{
			XmlNode xmlNode = this.source;
			XmlNode end = xmlNode;
			XmlNode xmlNode2;
			switch (xmlNode.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
				break;
			case XmlNodeType.Attribute:
			{
				XmlAttribute xmlAttribute = (XmlAttribute)xmlNode;
				if (xmlAttribute.IsNamespace)
				{
					goto IL_AF;
				}
				xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlAttribute);
				DocumentXPathNavigator.DeleteAttribute(xmlAttribute, this.attributeIndex);
				if (xmlNode2 != null)
				{
					this.ResetPosition(xmlNode2);
					return;
				}
				return;
			}
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.CalibrateText();
				xmlNode = this.source;
				end = this.TextEnd(xmlNode);
				break;
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentType:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				goto IL_AF;
			default:
				goto IL_AF;
			}
			xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlNode);
			DocumentXPathNavigator.DeleteToFollowingSibling(xmlNode, end);
			if (xmlNode2 != null)
			{
				this.ResetPosition(xmlNode2);
				return;
			}
			return;
			IL_AF:
			throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000480B0 File Offset: 0x000462B0
		private static void DeleteAttribute(XmlAttribute attribute, int index)
		{
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(attribute, out xmlAttributeCollection, index) && !DocumentXPathNavigator.ResetAttributePosition(attribute, xmlAttributeCollection, out index))
			{
				throw new InvalidOperationException(Res.GetString("Xpn_MissingParent"));
			}
			if (attribute.IsReadOnly)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Modify_ReadOnly"));
			}
			xmlAttributeCollection.RemoveAt(index);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00048104 File Offset: 0x00046304
		internal static void DeleteToFollowingSibling(XmlNode node, XmlNode end)
		{
			XmlNode parentNode = node.ParentNode;
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_MissingParent"));
			}
			if (node.IsReadOnly || end.IsReadOnly)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Modify_ReadOnly"));
			}
			while (node != end)
			{
				XmlNode oldChild = node;
				node = node.NextSibling;
				parentNode.RemoveChild(oldChild);
			}
			parentNode.RemoveChild(node);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0004816C File Offset: 0x0004636C
		private static XmlNamespaceManager GetNamespaceManager(XmlNode node, XmlDocument document)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(document.NameTable);
			List<XmlElement> list = new List<XmlElement>();
			while (node != null)
			{
				XmlElement xmlElement = node as XmlElement;
				if (xmlElement != null && xmlElement.HasAttributes)
				{
					list.Add(xmlElement);
				}
				node = node.ParentNode;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				xmlNamespaceManager.PushScope();
				XmlAttributeCollection attributes = list[i].Attributes;
				for (int j = 0; j < attributes.Count; j++)
				{
					XmlAttribute xmlAttribute = attributes[j];
					if (xmlAttribute.IsNamespace)
					{
						string prefix = (xmlAttribute.Prefix.Length == 0) ? string.Empty : xmlAttribute.LocalName;
						xmlNamespaceManager.AddNamespace(prefix, xmlAttribute.Value);
					}
				}
			}
			return xmlNamespaceManager;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00048230 File Offset: 0x00046430
		internal void ResetPosition(XmlNode node)
		{
			this.source = node;
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				XmlElement ownerElement = xmlAttribute.OwnerElement;
				if (ownerElement != null)
				{
					DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, ownerElement.Attributes, out this.attributeIndex);
					if (xmlAttribute.IsNamespace)
					{
						this.namespaceParent = ownerElement;
					}
				}
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004827C File Offset: 0x0004647C
		private static bool ResetAttributePosition(XmlAttribute attribute, XmlAttributeCollection attributes, out int index)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Count; i++)
				{
					if (attribute == attributes[i])
					{
						index = i;
						return true;
					}
				}
			}
			index = 0;
			return false;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000482B0 File Offset: 0x000464B0
		private static bool CheckAttributePosition(XmlAttribute attribute, out XmlAttributeCollection attributes, int index)
		{
			XmlElement ownerElement = attribute.OwnerElement;
			if (ownerElement != null)
			{
				attributes = ownerElement.Attributes;
				if (index >= 0 && index < attributes.Count && attribute == attributes[index])
				{
					return true;
				}
			}
			else
			{
				attributes = null;
			}
			return false;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000482F0 File Offset: 0x000464F0
		private void CalibrateText()
		{
			for (XmlNode node = this.PreviousText(this.source); node != null; node = this.PreviousText(node))
			{
				this.ResetPosition(node);
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00048320 File Offset: 0x00046520
		private XmlNode ParentNode(XmlNode node)
		{
			XmlNode parentNode = node.ParentNode;
			if (!this.document.HasEntityReferences)
			{
				return parentNode;
			}
			return this.ParentNodeTail(parentNode);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004834A File Offset: 0x0004654A
		private XmlNode ParentNodeTail(XmlNode parent)
		{
			while (parent != null && parent.NodeType == XmlNodeType.EntityReference)
			{
				parent = parent.ParentNode;
			}
			return parent;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00048364 File Offset: 0x00046564
		private XmlNode FirstChild(XmlNode node)
		{
			XmlNode firstChild = node.FirstChild;
			if (!this.document.HasEntityReferences)
			{
				return firstChild;
			}
			return this.FirstChildTail(firstChild);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004838E File Offset: 0x0004658E
		private XmlNode FirstChildTail(XmlNode child)
		{
			while (child != null && child.NodeType == XmlNodeType.EntityReference)
			{
				child = child.FirstChild;
			}
			return child;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000483A8 File Offset: 0x000465A8
		private XmlNode NextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (!this.document.HasEntityReferences)
			{
				return nextSibling;
			}
			return this.NextSiblingTail(node, nextSibling);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000483D3 File Offset: 0x000465D3
		private XmlNode NextSiblingTail(XmlNode node, XmlNode sibling)
		{
			while (sibling == null)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				sibling = node.NextSibling;
			}
			while (sibling != null && sibling.NodeType == XmlNodeType.EntityReference)
			{
				sibling = sibling.FirstChild;
			}
			return sibling;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00048410 File Offset: 0x00046610
		private XmlNode PreviousSibling(XmlNode node)
		{
			XmlNode previousSibling = node.PreviousSibling;
			if (!this.document.HasEntityReferences)
			{
				return previousSibling;
			}
			return this.PreviousSiblingTail(node, previousSibling);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004843B File Offset: 0x0004663B
		private XmlNode PreviousSiblingTail(XmlNode node, XmlNode sibling)
		{
			while (sibling == null)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				sibling = node.PreviousSibling;
			}
			while (sibling != null && sibling.NodeType == XmlNodeType.EntityReference)
			{
				sibling = sibling.LastChild;
			}
			return sibling;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00048478 File Offset: 0x00046678
		private XmlNode PreviousText(XmlNode node)
		{
			XmlNode previousText = node.PreviousText;
			if (!this.document.HasEntityReferences)
			{
				return previousText;
			}
			return this.PreviousTextTail(node, previousText);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000484A4 File Offset: 0x000466A4
		private XmlNode PreviousTextTail(XmlNode node, XmlNode text)
		{
			if (text != null)
			{
				return text;
			}
			if (!node.IsText)
			{
				return null;
			}
			XmlNode xmlNode;
			for (xmlNode = node.PreviousSibling; xmlNode == null; xmlNode = node.PreviousSibling)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
			}
			while (xmlNode != null)
			{
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.EntityReference)
					{
						xmlNode = xmlNode.LastChild;
						continue;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return null;
					}
				}
				return xmlNode;
			}
			return null;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00048516 File Offset: 0x00046716
		internal static bool IsFollowingSibling(XmlNode left, XmlNode right)
		{
			do
			{
				left = left.NextSibling;
				if (left == null)
				{
					return false;
				}
			}
			while (left != right);
			return true;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0004852C File Offset: 0x0004672C
		private static bool IsDescendant(XmlNode top, XmlNode bottom)
		{
			do
			{
				XmlNode xmlNode = bottom.ParentNode;
				if (xmlNode == null)
				{
					XmlAttribute xmlAttribute = bottom as XmlAttribute;
					if (xmlAttribute == null)
					{
						return false;
					}
					xmlNode = xmlAttribute.OwnerElement;
					if (xmlNode == null)
					{
						return false;
					}
				}
				bottom = xmlNode;
			}
			while (top != bottom);
			return true;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00048564 File Offset: 0x00046764
		private static bool IsValidChild(XmlNode parent, XmlNode child)
		{
			XmlNodeType nodeType = parent.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Document)
				{
					if (nodeType == XmlNodeType.DocumentFragment)
					{
						XmlNodeType nodeType2 = child.NodeType;
						switch (nodeType2)
						{
						case XmlNodeType.Element:
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
						case XmlNodeType.ProcessingInstruction:
						case XmlNodeType.Comment:
							break;
						case XmlNodeType.Attribute:
						case XmlNodeType.EntityReference:
						case XmlNodeType.Entity:
							return false;
						default:
							if (nodeType2 - XmlNodeType.Whitespace > 1)
							{
								return false;
							}
							break;
						}
						return true;
					}
				}
				else
				{
					XmlNodeType nodeType3 = child.NodeType;
					if (nodeType3 == XmlNodeType.Element || nodeType3 - XmlNodeType.ProcessingInstruction <= 1)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000485D8 File Offset: 0x000467D8
		private XmlNode TextStart(XmlNode node)
		{
			XmlNode result;
			do
			{
				result = node;
				node = this.PreviousSibling(node);
			}
			while (node != null && node.IsText);
			return result;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000485FC File Offset: 0x000467FC
		private XmlNode TextEnd(XmlNode node)
		{
			XmlNode result;
			do
			{
				result = node;
				node = this.NextSibling(node);
			}
			while (node != null && node.IsText);
			return result;
		}

		// Token: 0x040004BE RID: 1214
		private XmlDocument document;

		// Token: 0x040004BF RID: 1215
		private XmlNode source;

		// Token: 0x040004C0 RID: 1216
		private int attributeIndex;

		// Token: 0x040004C1 RID: 1217
		private XmlElement namespaceParent;
	}
}
