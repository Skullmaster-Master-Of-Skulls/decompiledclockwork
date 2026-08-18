using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000119 RID: 281
	internal class XmlNodeReaderNavigator
	{
		// Token: 0x060013A1 RID: 5025 RVA: 0x00051848 File Offset: 0x0004FA48
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

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0005195C File Offset: 0x0004FB5C
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0005198B File Offset: 0x0004FB8B
		public string NamespaceURI
		{
			get
			{
				return this.curNode.NamespaceURI;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00051998 File Offset: 0x0004FB98
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

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00051A1D File Offset: 0x0004FC1D
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00051A53 File Offset: 0x0004FC53
		internal bool IsOnAttrVal
		{
			get
			{
				return this.bOnAttrVal;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00051A5B File Offset: 0x0004FC5B
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00051A6D File Offset: 0x0004FC6D
		internal bool CreatedOnAttribute
		{
			get
			{
				return this.bCreatedOnAttribute;
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00051A78 File Offset: 0x0004FC78
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

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00051ADA File Offset: 0x0004FCDA
		public string Prefix
		{
			get
			{
				return this.curNode.Prefix;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00051AE7 File Offset: 0x0004FCE7
		public bool HasValue
		{
			get
			{
				return this.nAttrInd != -1 || (this.curNode.Value != null || this.curNode.NodeType == XmlNodeType.DocumentType);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00051B14 File Offset: 0x0004FD14
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00051C31 File Offset: 0x0004FE31
		public string BaseURI
		{
			get
			{
				return this.curNode.BaseURI;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00051C3E File Offset: 0x0004FE3E
		public XmlSpace XmlSpace
		{
			get
			{
				return this.curNode.XmlSpace;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00051C4B File Offset: 0x0004FE4B
		public string XmlLang
		{
			get
			{
				return this.curNode.XmlLang;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00051C58 File Offset: 0x0004FE58
		public bool IsEmptyElement
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Element && ((XmlElement)this.curNode).IsEmpty;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00051C7A File Offset: 0x0004FE7A
		public bool IsDefault
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.curNode).Specified;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00051C9F File Offset: 0x0004FE9F
		public IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.curNode.SchemaInfo;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00051CAC File Offset: 0x0004FEAC
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00051CB4 File Offset: 0x0004FEB4
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

		// Token: 0x060013B5 RID: 5045 RVA: 0x00051D5D File Offset: 0x0004FF5D
		private void CheckIndexCondition(int attributeIndex)
		{
			if (attributeIndex < 0 || attributeIndex >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00051D78 File Offset: 0x0004FF78
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

		// Token: 0x060013B7 RID: 5047 RVA: 0x00051E57 File Offset: 0x00050057
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

		// Token: 0x060013B8 RID: 5048 RVA: 0x00051E96 File Offset: 0x00050096
		public string GetDeclarationAttr(int i)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			return this.decNodeAttributes[i].value;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00051EB8 File Offset: 0x000500B8
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

		// Token: 0x060013BA RID: 5050 RVA: 0x00051F04 File Offset: 0x00050104
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

		// Token: 0x060013BB RID: 5051 RVA: 0x00051F9D File Offset: 0x0005019D
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

		// Token: 0x060013BC RID: 5052 RVA: 0x00051FC8 File Offset: 0x000501C8
		public string GetDocumentTypeAttr(int i)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			return this.docTypeNodeAttributes[i].value;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00051FEC File Offset: 0x000501EC
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

		// Token: 0x060013BE RID: 5054 RVA: 0x00052038 File Offset: 0x00050238
		private string GetAttributeFromElement(XmlElement elem, string name)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00052058 File Offset: 0x00050258
		public string GetAttribute(string name)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name);
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			return null;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000520E4 File Offset: 0x000502E4
		private string GetAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00052108 File Offset: 0x00050308
		public string GetAttribute(string name, string ns)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name, ns);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name, ns);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (ns.Length != 0)
					{
						return null;
					}
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			else
			{
				if (ns.Length != 0)
				{
					return null;
				}
				return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
			}
			return null;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000521A8 File Offset: 0x000503A8
		public string GetAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.curNode).Attributes[attributeIndex].Value;
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.elemNode).Attributes[attributeIndex].Value;
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDocumentTypeAttr(attributeIndex);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDeclarationAttr(attributeIndex);
				}
			}
			throw new ArgumentOutOfRangeException("attributeIndex");
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00052253 File Offset: 0x00050453
		public void LogMove(int level)
		{
			this.logNode = this.curNode;
			this.nLogLevel = level;
			this.nLogAttrInd = this.nAttrInd;
			this.logAttrIndex = this.attrIndex;
			this.bLogOnAttrVal = this.bOnAttrVal;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0005228C File Offset: 0x0005048C
		public void RollBackMove(ref int level)
		{
			this.curNode = this.logNode;
			level = this.nLogLevel;
			this.nAttrInd = this.nLogAttrInd;
			this.attrIndex = this.logAttrIndex;
			this.bOnAttrVal = this.bLogOnAttrVal;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000522C8 File Offset: 0x000504C8
		private bool IsOnDeclOrDocType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				return nodeType == XmlNodeType.XmlDeclaration || nodeType == XmlNodeType.DocumentType;
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000522F0 File Offset: 0x000504F0
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

		// Token: 0x060013C7 RID: 5063 RVA: 0x00052350 File Offset: 0x00050550
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

		// Token: 0x060013C8 RID: 5064 RVA: 0x0005241E File Offset: 0x0005061E
		public bool MoveToAttribute(string name)
		{
			return this.MoveToAttribute(name, string.Empty);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0005242C File Offset: 0x0005062C
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

		// Token: 0x060013CA RID: 5066 RVA: 0x0005248C File Offset: 0x0005068C
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

		// Token: 0x060013CB RID: 5067 RVA: 0x00052534 File Offset: 0x00050734
		public void MoveToAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						return;
					}
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.elemNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
				else
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
				}
			}
			else
			{
				if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
				{
					return;
				}
				this.CheckIndexCondition(attributeIndex);
				this.nAttrInd = attributeIndex;
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000525EC File Offset: 0x000507EC
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
			XmlAttributeCollection attributes = this.elemNode.Attributes;
			int i = this.attrIndex + 1;
			this.attrIndex = i;
			this.curNode = attributes[i];
			return true;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00052754 File Offset: 0x00050954
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

		// Token: 0x060013CE RID: 5070 RVA: 0x0005278C File Offset: 0x0005098C
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

		// Token: 0x060013CF RID: 5071 RVA: 0x000527C4 File Offset: 0x000509C4
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

		// Token: 0x060013D0 RID: 5072 RVA: 0x000527F4 File Offset: 0x000509F4
		public bool MoveToNext()
		{
			if (this.curNode.NodeType != XmlNodeType.Attribute)
			{
				return this.MoveToNextSibling(this.curNode);
			}
			return this.MoveToNextSibling(this.elemNode);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00052820 File Offset: 0x00050A20
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

		// Token: 0x060013D2 RID: 5074 RVA: 0x00052884 File Offset: 0x00050A84
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

		// Token: 0x060013D3 RID: 5075 RVA: 0x00052960 File Offset: 0x00050B60
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

		// Token: 0x060013D4 RID: 5076 RVA: 0x000529D0 File Offset: 0x00050BD0
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

		// Token: 0x060013D5 RID: 5077 RVA: 0x00052B34 File Offset: 0x00050D34
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

		// Token: 0x060013D6 RID: 5078 RVA: 0x00052CD4 File Offset: 0x00050ED4
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
					if (this.curNode.NodeType == XmlNodeType.EntityReference & bResolveEntity)
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

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00052DE8 File Offset: 0x00050FE8
		public XmlDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x04000564 RID: 1380
		private XmlNode curNode;

		// Token: 0x04000565 RID: 1381
		private XmlNode elemNode;

		// Token: 0x04000566 RID: 1382
		private XmlNode logNode;

		// Token: 0x04000567 RID: 1383
		private int attrIndex;

		// Token: 0x04000568 RID: 1384
		private int logAttrIndex;

		// Token: 0x04000569 RID: 1385
		private XmlNameTable nameTable;

		// Token: 0x0400056A RID: 1386
		private XmlDocument doc;

		// Token: 0x0400056B RID: 1387
		private int nAttrInd;

		// Token: 0x0400056C RID: 1388
		private const string strPublicID = "PUBLIC";

		// Token: 0x0400056D RID: 1389
		private const string strSystemID = "SYSTEM";

		// Token: 0x0400056E RID: 1390
		private const string strVersion = "version";

		// Token: 0x0400056F RID: 1391
		private const string strStandalone = "standalone";

		// Token: 0x04000570 RID: 1392
		private const string strEncoding = "encoding";

		// Token: 0x04000571 RID: 1393
		private int nDeclarationAttrCount;

		// Token: 0x04000572 RID: 1394
		private int nDocTypeAttrCount;

		// Token: 0x04000573 RID: 1395
		private int nLogLevel;

		// Token: 0x04000574 RID: 1396
		private int nLogAttrInd;

		// Token: 0x04000575 RID: 1397
		private bool bLogOnAttrVal;

		// Token: 0x04000576 RID: 1398
		private bool bCreatedOnAttribute;

		// Token: 0x04000577 RID: 1399
		internal XmlNodeReaderNavigator.VirtualAttribute[] decNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000578 RID: 1400
		internal XmlNodeReaderNavigator.VirtualAttribute[] docTypeNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000579 RID: 1401
		private bool bOnAttrVal;

		// Token: 0x02000439 RID: 1081
		internal struct VirtualAttribute
		{
			// Token: 0x06003044 RID: 12356 RVA: 0x00113F9E File Offset: 0x0011219E
			internal VirtualAttribute(string name, string value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x04001C38 RID: 7224
			internal string name;

			// Token: 0x04001C39 RID: 7225
			internal string value;
		}
	}
}
