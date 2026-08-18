using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000E9 RID: 233
	internal class XmlNodeReaderNavigator
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x0003E44C File Offset: 0x0003D44C
		public XmlNodeReaderNavigator(XmlNode node)
		{
			this.curNode = node;
			this.logNode = node;
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				this.elemNode = null;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = true;
			}
			else
			{
				this.elemNode = node;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = false;
			}
			if (nodeType == XmlNodeType.Document)
			{
				this.doc = (XmlDocument)this.curNode;
			}
			else
			{
				this.doc = node.OwnerDocument;
			}
			this.nameTable = this.doc.NameTable;
			this.nAttrInd = -1;
			this.nDeclarationAttrCount = -1;
			this.nDocTypeAttrCount = -1;
			this.bOnAttrVal = false;
			this.bLogOnAttrVal = false;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0003E57C File Offset: 0x0003D57C
		public XmlNodeType NodeType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd == -1)
				{
					return nodeType;
				}
				if (this.bOnAttrVal)
				{
					return XmlNodeType.Text;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0003E5AB File Offset: 0x0003D5AB
		public string NamespaceURI
		{
			get
			{
				return this.curNode.NamespaceURI;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0003E5B8 File Offset: 0x0003D5B8
		public string Name
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					if (this.bOnAttrVal)
					{
						return string.Empty;
					}
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].name;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].name;
				}
				else
				{
					if (this.IsLocalNameEmpty(this.curNode.NodeType))
					{
						return string.Empty;
					}
					return this.curNode.Name;
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003E63D File Offset: 0x0003D63D
		public string LocalName
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					return this.Name;
				}
				if (this.IsLocalNameEmpty(this.curNode.NodeType))
				{
					return string.Empty;
				}
				return this.curNode.LocalName;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003E673 File Offset: 0x0003D673
		internal bool IsOnAttrVal
		{
			get
			{
				return this.bOnAttrVal;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0003E67B File Offset: 0x0003D67B
		internal XmlNode OwnerElementNode
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return null;
				}
				return this.elemNode;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003E68D File Offset: 0x0003D68D
		internal bool CreatedOnAttribute
		{
			get
			{
				return this.bCreatedOnAttribute;
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003E698 File Offset: 0x0003D698
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0003E6FC File Offset: 0x0003D6FC
		public string Prefix
		{
			get
			{
				return this.curNode.Prefix;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003E709 File Offset: 0x0003D709
		public bool HasValue
		{
			get
			{
				return this.nAttrInd != -1 || (this.curNode.Value != null || this.curNode.NodeType == XmlNodeType.DocumentType);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003E738 File Offset: 0x0003D738
		public string Value
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd != -1)
				{
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].value;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].value;
				}
				else
				{
					string text;
					if (nodeType == XmlNodeType.DocumentType)
					{
						text = ((XmlDocumentType)this.curNode).InternalSubset;
					}
					else if (nodeType == XmlNodeType.XmlDeclaration)
					{
						StringBuilder stringBuilder = new StringBuilder(string.Empty);
						if (this.nDeclarationAttrCount == -1)
						{
							this.InitDecAttr();
						}
						for (int i = 0; i < this.nDeclarationAttrCount; i++)
						{
							stringBuilder.Append(this.decNodeAttributes[i].name + "=\"" + this.decNodeAttributes[i].value + "\"");
							if (i != this.nDeclarationAttrCount - 1)
							{
								stringBuilder.Append(" ");
							}
						}
						text = stringBuilder.ToString();
					}
					else
					{
						text = this.curNode.Value;
					}
					if (text != null)
					{
						return text;
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003E855 File Offset: 0x0003D855
		public string BaseURI
		{
			get
			{
				return this.curNode.BaseURI;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0003E862 File Offset: 0x0003D862
		public XmlSpace XmlSpace
		{
			get
			{
				return this.curNode.XmlSpace;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0003E86F File Offset: 0x0003D86F
		public string XmlLang
		{
			get
			{
				return this.curNode.XmlLang;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003E87C File Offset: 0x0003D87C
		public bool IsEmptyElement
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Element && ((XmlElement)this.curNode).IsEmpty;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0003E89E File Offset: 0x0003D89E
		public bool IsDefault
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.curNode).Specified;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0003E8C3 File Offset: 0x0003D8C3
		public IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.curNode.SchemaInfo;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0003E8D0 File Offset: 0x0003D8D0
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0003E8D8 File Offset: 0x0003D8D8
		public int AttributeCount
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return 0;
				}
				XmlNodeType nodeType = this.curNode.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return ((XmlElement)this.curNode).Attributes.Count;
				}
				if (nodeType == XmlNodeType.Attribute || (this.bOnAttrVal && nodeType != XmlNodeType.XmlDeclaration && nodeType != XmlNodeType.DocumentType))
				{
					return this.elemNode.Attributes.Count;
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount != -1)
					{
						return this.nDeclarationAttrCount;
					}
					this.InitDecAttr();
					return this.nDeclarationAttrCount;
				}
				else
				{
					if (nodeType != XmlNodeType.DocumentType)
					{
						return 0;
					}
					if (this.nDocTypeAttrCount != -1)
					{
						return this.nDocTypeAttrCount;
					}
					this.InitDocTypeAttr();
					return this.nDocTypeAttrCount;
				}
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003E981 File Offset: 0x0003D981
		private void CheckIndexCondition(int attributeIndex)
		{
			if (attributeIndex < 0 || attributeIndex >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003E99C File Offset: 0x0003D99C
		private void InitDecAttr()
		{
			int num = 0;
			string text = this.doc.Version;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "version";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Encoding;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "encoding";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Standalone;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "standalone";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			this.nDeclarationAttrCount = num;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003EA7B File Offset: 0x0003DA7B
		public string GetDeclarationAttr(XmlDeclaration decl, string name)
		{
			if (name == "version")
			{
				return decl.Version;
			}
			if (name == "encoding")
			{
				return decl.Encoding;
			}
			if (name == "standalone")
			{
				return decl.Standalone;
			}
			return null;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003EABA File Offset: 0x0003DABA
		public string GetDeclarationAttr(int i)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			return this.decNodeAttributes[i].value;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003EADC File Offset: 0x0003DADC
		public int GetDecAttrInd(string name)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			for (int i = 0; i < this.nDeclarationAttrCount; i++)
			{
				if (this.decNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003EB28 File Offset: 0x0003DB28
		private void InitDocTypeAttr()
		{
			int num = 0;
			XmlDocumentType documentType = this.doc.DocumentType;
			if (documentType == null)
			{
				this.nDocTypeAttrCount = 0;
				return;
			}
			string text = documentType.PublicId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "PUBLIC";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			text = documentType.SystemId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "SYSTEM";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			this.nDocTypeAttrCount = num;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003EBC1 File Offset: 0x0003DBC1
		public string GetDocumentTypeAttr(XmlDocumentType docType, string name)
		{
			if (name == "PUBLIC")
			{
				return docType.PublicId;
			}
			if (name == "SYSTEM")
			{
				return docType.SystemId;
			}
			return null;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003EBEC File Offset: 0x0003DBEC
		public string GetDocumentTypeAttr(int i)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			return this.docTypeNodeAttributes[i].value;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003EC10 File Offset: 0x0003DC10
		public int GetDocTypeAttrInd(string name)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			for (int i = 0; i < this.nDocTypeAttrCount; i++)
			{
				if (this.docTypeNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003EC5C File Offset: 0x0003DC5C
		private string GetAttributeFromElement(XmlElement elem, string name)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003EC7C File Offset: 0x0003DC7C
		public string GetAttribute(string name)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				return this.GetAttributeFromElement((XmlElement)this.curNode, name);
			case XmlNodeType.Attribute:
				return this.GetAttributeFromElement((XmlElement)this.elemNode, name);
			default:
				if (nodeType == XmlNodeType.DocumentType)
				{
					return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
				}
				if (nodeType != XmlNodeType.XmlDeclaration)
				{
					return null;
				}
				return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003ED08 File Offset: 0x0003DD08
		private string GetAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003ED2C File Offset: 0x0003DD2C
		public string GetAttribute(string name, string ns)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				return this.GetAttributeFromElement((XmlElement)this.curNode, name, ns);
			case XmlNodeType.Attribute:
				return this.GetAttributeFromElement((XmlElement)this.elemNode, name, ns);
			default:
				if (nodeType != XmlNodeType.DocumentType)
				{
					if (nodeType != XmlNodeType.XmlDeclaration)
					{
						return null;
					}
					if (ns.Length != 0)
					{
						return null;
					}
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
				else
				{
					if (ns.Length != 0)
					{
						return null;
					}
					return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
				}
				break;
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003EDD0 File Offset: 0x0003DDD0
		public string GetAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				this.CheckIndexCondition(attributeIndex);
				return ((XmlElement)this.curNode).Attributes[attributeIndex].Value;
			case XmlNodeType.Attribute:
				this.CheckIndexCondition(attributeIndex);
				return ((XmlElement)this.elemNode).Attributes[attributeIndex].Value;
			default:
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDocumentTypeAttr(attributeIndex);
				}
				if (nodeType != XmlNodeType.XmlDeclaration)
				{
					throw new ArgumentOutOfRangeException("attributeIndex");
				}
				this.CheckIndexCondition(attributeIndex);
				return this.GetDeclarationAttr(attributeIndex);
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003EE7D File Offset: 0x0003DE7D
		public void LogMove(int level)
		{
			this.logNode = this.curNode;
			this.nLogLevel = level;
			this.nLogAttrInd = this.nAttrInd;
			this.logAttrIndex = this.attrIndex;
			this.bLogOnAttrVal = this.bOnAttrVal;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003EEB6 File Offset: 0x0003DEB6
		public void RollBackMove(ref int level)
		{
			this.curNode = this.logNode;
			level = this.nLogLevel;
			this.nAttrInd = this.nLogAttrInd;
			this.attrIndex = this.logAttrIndex;
			this.bOnAttrVal = this.bLogOnAttrVal;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003EEF0 File Offset: 0x0003DEF0
		private bool IsOnDeclOrDocType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				return nodeType == XmlNodeType.XmlDeclaration || nodeType == XmlNodeType.DocumentType;
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003EF18 File Offset: 0x0003DF18
		public void ResetToAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.bOnAttrVal)
			{
				if (this.IsOnDeclOrDocType)
				{
					level -= 2;
				}
				else
				{
					while (this.curNode.NodeType != XmlNodeType.Attribute && (this.curNode = this.curNode.ParentNode) != null)
					{
						level--;
					}
				}
				this.bOnAttrVal = false;
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003EF78 File Offset: 0x0003DF78
		public void ResetMove(ref int level, ref XmlNodeType nt)
		{
			this.LogMove(level);
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.nAttrInd != -1)
			{
				if (this.bOnAttrVal)
				{
					level--;
					this.bOnAttrVal = false;
				}
				this.nLogAttrInd = this.nAttrInd;
				level--;
				this.nAttrInd = -1;
				nt = this.curNode.NodeType;
				return;
			}
			if (this.bOnAttrVal && this.curNode.NodeType != XmlNodeType.Attribute)
			{
				this.ResetToAttribute(ref level);
			}
			if (this.curNode.NodeType == XmlNodeType.Attribute)
			{
				this.curNode = ((XmlAttribute)this.curNode).OwnerElement;
				this.attrIndex = -1;
				level--;
				nt = XmlNodeType.Element;
			}
			if (this.curNode.NodeType == XmlNodeType.Element)
			{
				this.elemNode = this.curNode;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003F046 File Offset: 0x0003E046
		public bool MoveToAttribute(string name)
		{
			return this.MoveToAttribute(name, string.Empty);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003F054 File Offset: 0x0003E054
		private bool MoveToAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode;
			if (ns.Length == 0)
			{
				attributeNode = elem.GetAttributeNode(name);
			}
			else
			{
				attributeNode = elem.GetAttributeNode(name, ns);
			}
			if (attributeNode != null)
			{
				this.bOnAttrVal = false;
				this.elemNode = elem;
				this.curNode = attributeNode;
				this.attrIndex = elem.Attributes.FindNodeOffsetNS(attributeNode);
				if (this.attrIndex != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003F0B4 File Offset: 0x0003E0B4
		public bool MoveToAttribute(string name, string namespaceURI)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.curNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.Attribute)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.elemNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.XmlDeclaration && namespaceURI.Length == 0)
			{
				if ((this.nAttrInd = this.GetDecAttrInd(name)) != -1)
				{
					this.bOnAttrVal = false;
					return true;
				}
			}
			else if (nodeType == XmlNodeType.DocumentType && namespaceURI.Length == 0 && (this.nAttrInd = this.GetDocTypeAttrInd(name)) != -1)
			{
				this.bOnAttrVal = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003F15C File Offset: 0x0003E15C
		public void MoveToAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
			{
				this.CheckIndexCondition(attributeIndex);
				XmlAttribute xmlAttribute = ((XmlElement)this.curNode).Attributes[attributeIndex];
				if (xmlAttribute != null)
				{
					this.elemNode = this.curNode;
					this.curNode = xmlAttribute;
					this.attrIndex = attributeIndex;
					return;
				}
				break;
			}
			case XmlNodeType.Attribute:
			{
				this.CheckIndexCondition(attributeIndex);
				XmlAttribute xmlAttribute = ((XmlElement)this.elemNode).Attributes[attributeIndex];
				if (xmlAttribute != null)
				{
					this.curNode = xmlAttribute;
					this.attrIndex = attributeIndex;
					return;
				}
				break;
			}
			default:
				if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
				{
					return;
				}
				this.CheckIndexCondition(attributeIndex);
				this.nAttrInd = attributeIndex;
				break;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003F218 File Offset: 0x0003E218
		public bool MoveToNextAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					if (this.curNode.Attributes.Count > 0)
					{
						level++;
						this.elemNode = this.curNode;
						this.curNode = this.curNode.Attributes[0];
						this.attrIndex = 0;
						return true;
					}
				}
				else if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount == -1)
					{
						this.InitDecAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDeclarationAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				else if (nodeType == XmlNodeType.DocumentType)
				{
					if (this.nDocTypeAttrCount == -1)
					{
						this.InitDocTypeAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDocTypeAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				return false;
			}
			if (this.attrIndex >= this.elemNode.Attributes.Count - 1)
			{
				return false;
			}
			this.curNode = this.elemNode.Attributes[++this.attrIndex];
			return true;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003F380 File Offset: 0x0003E380
		public bool MoveToParent()
		{
			XmlNode parentNode = this.curNode.ParentNode;
			if (parentNode != null)
			{
				this.curNode = parentNode;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = 0;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003F3B8 File Offset: 0x0003E3B8
		public bool MoveToFirstChild()
		{
			XmlNode firstChild = this.curNode.FirstChild;
			if (firstChild != null)
			{
				this.curNode = firstChild;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003F3F0 File Offset: 0x0003E3F0
		private bool MoveToNextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (nextSibling != null)
			{
				this.curNode = nextSibling;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003F420 File Offset: 0x0003E420
		public bool MoveToNext()
		{
			if (this.curNode.NodeType != XmlNodeType.Attribute)
			{
				return this.MoveToNextSibling(this.curNode);
			}
			return this.MoveToNextSibling(this.elemNode);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003F44C File Offset: 0x0003E44C
		public bool MoveToElement()
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nAttrInd != -1)
					{
						this.nAttrInd = -1;
						return true;
					}
				}
			}
			else if (this.elemNode != null)
			{
				this.curNode = this.elemNode;
				this.attrIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003F4B0 File Offset: 0x0003E4B0
		public string LookupNamespace(string prefix)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			if (prefix == "xmlns")
			{
				return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			}
			if (prefix == "xml")
			{
				return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			string name;
			if (prefix.Length == 0)
			{
				name = "xmlns";
			}
			else
			{
				name = "xmlns:" + prefix;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttribute attributeNode = xmlElement.GetAttributeNode(name);
						if (attributeNode != null)
						{
							return attributeNode.Value;
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003F58C File Offset: 0x0003E58C
		internal string DefaultLookupNamespace(string prefix)
		{
			if (!this.bCreatedOnAttribute)
			{
				if (prefix == "xmlns")
				{
					return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (prefix == "xml")
				{
					return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
				}
				if (prefix == string.Empty)
				{
					return this.nameTable.Add(string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003F5FC File Offset: 0x0003E5FC
		internal string LookupPrefix(string namespaceName)
		{
			if (this.bCreatedOnAttribute || namespaceName == null)
			{
				return null;
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return this.nameTable.Add("xmlns");
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return this.nameTable.Add("xml");
			}
			if (namespaceName == string.Empty)
			{
				return string.Empty;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.Value == namespaceName)
							{
								if (xmlAttribute.Prefix.Length == 0 && xmlAttribute.LocalName == "xmlns")
								{
									if (this.LookupNamespace(string.Empty) == namespaceName)
									{
										return string.Empty;
									}
								}
								else if (xmlAttribute.Prefix == "xmlns")
								{
									string localName = xmlAttribute.LocalName;
									if (this.LookupNamespace(localName) == namespaceName)
									{
										return this.nameTable.Add(localName);
									}
								}
							}
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			return null;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003F760 File Offset: 0x0003E760
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (this.bCreatedOnAttribute)
			{
				return dictionary;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.LocalName == "xmlns" && xmlAttribute.Prefix.Length == 0)
							{
								if (!dictionary.ContainsKey(string.Empty))
								{
									dictionary.Add(this.nameTable.Add(string.Empty), this.nameTable.Add(xmlAttribute.Value));
								}
							}
							else if (xmlAttribute.Prefix == "xmlns")
							{
								string localName = xmlAttribute.LocalName;
								if (!dictionary.ContainsKey(localName))
								{
									dictionary.Add(this.nameTable.Add(localName), this.nameTable.Add(xmlAttribute.Value));
								}
							}
						}
					}
					if (scope == XmlNamespaceScope.Local)
					{
						break;
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (scope != XmlNamespaceScope.Local)
			{
				if (dictionary.ContainsKey(string.Empty) && dictionary[string.Empty] == string.Empty)
				{
					dictionary.Remove(string.Empty);
				}
				if (scope == XmlNamespaceScope.All)
				{
					dictionary.Add(this.nameTable.Add("xml"), this.nameTable.Add("http://www.w3.org/XML/1998/namespace"));
				}
			}
			return dictionary;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003F900 File Offset: 0x0003E900
		public bool ReadAttributeValue(ref int level, ref bool bResolveEntity, ref XmlNodeType nt)
		{
			if (this.nAttrInd == -1)
			{
				if (this.curNode.NodeType == XmlNodeType.Attribute)
				{
					XmlNode firstChild = this.curNode.FirstChild;
					if (firstChild != null)
					{
						this.curNode = firstChild;
						nt = this.curNode.NodeType;
						level++;
						this.bOnAttrVal = true;
						return true;
					}
				}
				else if (this.bOnAttrVal)
				{
					if (this.curNode.NodeType == XmlNodeType.EntityReference && bResolveEntity)
					{
						this.curNode = this.curNode.FirstChild;
						nt = this.curNode.NodeType;
						level++;
						bResolveEntity = false;
						return true;
					}
					XmlNode nextSibling = this.curNode.NextSibling;
					if (nextSibling == null)
					{
						XmlNode parentNode = this.curNode.ParentNode;
						if (parentNode != null && parentNode.NodeType == XmlNodeType.EntityReference)
						{
							this.curNode = parentNode;
							nt = XmlNodeType.EndEntity;
							level--;
							return true;
						}
					}
					if (nextSibling != null)
					{
						this.curNode = nextSibling;
						nt = this.curNode.NodeType;
						return true;
					}
					return false;
				}
				return false;
			}
			if (!this.bOnAttrVal)
			{
				this.bOnAttrVal = true;
				level++;
				nt = XmlNodeType.Text;
				return true;
			}
			return false;
		}

		// Token: 0x04000982 RID: 2434
		private const string strPublicID = "PUBLIC";

		// Token: 0x04000983 RID: 2435
		private const string strSystemID = "SYSTEM";

		// Token: 0x04000984 RID: 2436
		private const string strVersion = "version";

		// Token: 0x04000985 RID: 2437
		private const string strStandalone = "standalone";

		// Token: 0x04000986 RID: 2438
		private const string strEncoding = "encoding";

		// Token: 0x04000987 RID: 2439
		private XmlNode curNode;

		// Token: 0x04000988 RID: 2440
		private XmlNode elemNode;

		// Token: 0x04000989 RID: 2441
		private XmlNode logNode;

		// Token: 0x0400098A RID: 2442
		private int attrIndex;

		// Token: 0x0400098B RID: 2443
		private int logAttrIndex;

		// Token: 0x0400098C RID: 2444
		private XmlNameTable nameTable;

		// Token: 0x0400098D RID: 2445
		private XmlDocument doc;

		// Token: 0x0400098E RID: 2446
		private int nAttrInd;

		// Token: 0x0400098F RID: 2447
		private int nDeclarationAttrCount;

		// Token: 0x04000990 RID: 2448
		private int nDocTypeAttrCount;

		// Token: 0x04000991 RID: 2449
		private int nLogLevel;

		// Token: 0x04000992 RID: 2450
		private int nLogAttrInd;

		// Token: 0x04000993 RID: 2451
		private bool bLogOnAttrVal;

		// Token: 0x04000994 RID: 2452
		private bool bCreatedOnAttribute;

		// Token: 0x04000995 RID: 2453
		internal XmlNodeReaderNavigator.VirtualAttribute[] decNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000996 RID: 2454
		internal XmlNodeReaderNavigator.VirtualAttribute[] docTypeNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000997 RID: 2455
		private bool bOnAttrVal;

		// Token: 0x020000EA RID: 234
		internal struct VirtualAttribute
		{
			// Token: 0x06000E42 RID: 3650 RVA: 0x0003FA13 File Offset: 0x0003EA13
			internal VirtualAttribute(string name, string value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x04000998 RID: 2456
			internal string name;

			// Token: 0x04000999 RID: 2457
			internal string value;
		}
	}
}
