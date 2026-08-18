using System;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x02000008 RID: 8
	internal class XNodeNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002A72 File Offset: 0x00000C72
		public XNodeNavigator(XNode node, XmlNameTable nameTable)
		{
			this.source = node;
			this.nameTable = ((nameTable != null) ? nameTable : XNodeNavigator.CreateNameTable());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A92 File Offset: 0x00000C92
		public XNodeNavigator(XNodeNavigator other)
		{
			this.source = other.source;
			this.parent = other.parent;
			this.nameTable = other.nameTable;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public override string BaseURI
		{
			get
			{
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					return xobject.BaseUri;
				}
				if (this.parent != null)
				{
					return this.parent.BaseUri;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002AFC File Offset: 0x00000CFC
		public override bool HasAttributes
		{
			get
			{
				XElement xelement = this.source as XElement;
				if (xelement != null)
				{
					XAttribute xattribute = xelement.lastAttr;
					if (xattribute != null)
					{
						for (;;)
						{
							xattribute = xattribute.next;
							if (!xattribute.IsNamespaceDeclaration)
							{
								break;
							}
							if (xattribute == xelement.lastAttr)
							{
								return false;
							}
						}
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002B40 File Offset: 0x00000D40
		public override bool HasChildren
		{
			get
			{
				XContainer xcontainer = this.source as XContainer;
				if (xcontainer != null && xcontainer.content != null)
				{
					XNode xnode = xcontainer.content as XNode;
					if (xnode != null)
					{
						for (;;)
						{
							xnode = xnode.next;
							if (XNodeNavigator.IsContent(xcontainer, xnode))
							{
								break;
							}
							if (xnode == xcontainer.content)
							{
								return false;
							}
						}
						return true;
					}
					string text = (string)xcontainer.content;
					if (text.Length != 0 && (xcontainer.parent != null || xcontainer is XElement))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public override bool IsEmptyElement
		{
			get
			{
				XElement xelement = this.source as XElement;
				return xelement != null && xelement.IsEmpty;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002BDC File Offset: 0x00000DDC
		public override string LocalName
		{
			get
			{
				return this.nameTable.Add(this.GetLocalName());
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002BF0 File Offset: 0x00000DF0
		private string GetLocalName()
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				return xelement.Name.LocalName;
			}
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute != null)
			{
				if (this.parent != null && xattribute.Name.NamespaceName.Length == 0)
				{
					return string.Empty;
				}
				return xattribute.Name.LocalName;
			}
			else
			{
				XProcessingInstruction xprocessingInstruction = this.source as XProcessingInstruction;
				if (xprocessingInstruction != null)
				{
					return xprocessingInstruction.Target;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002C70 File Offset: 0x00000E70
		public override string Name
		{
			get
			{
				string prefix = this.GetPrefix();
				if (prefix.Length == 0)
				{
					return this.nameTable.Add(this.GetLocalName());
				}
				return this.nameTable.Add(prefix + ":" + this.GetLocalName());
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002CBA File Offset: 0x00000EBA
		public override string NamespaceURI
		{
			get
			{
				return this.nameTable.Add(this.GetNamespaceURI());
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CD0 File Offset: 0x00000ED0
		private string GetNamespaceURI()
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				return xelement.Name.NamespaceName;
			}
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute == null)
			{
				return string.Empty;
			}
			if (this.parent != null)
			{
				return string.Empty;
			}
			return xattribute.Name.NamespaceName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002D26 File Offset: 0x00000F26
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002D30 File Offset: 0x00000F30
		public override XPathNodeType NodeType
		{
			get
			{
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					switch (xobject.NodeType)
					{
					case XmlNodeType.Element:
						return XPathNodeType.Element;
					case XmlNodeType.Attribute:
						if (this.parent != null)
						{
							return XPathNodeType.Namespace;
						}
						return XPathNodeType.Attribute;
					case XmlNodeType.ProcessingInstruction:
						return XPathNodeType.ProcessingInstruction;
					case XmlNodeType.Comment:
						return XPathNodeType.Comment;
					case XmlNodeType.Document:
						return XPathNodeType.Root;
					}
					return XPathNodeType.Text;
				}
				return XPathNodeType.Text;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002D98 File Offset: 0x00000F98
		public override string Prefix
		{
			get
			{
				return this.nameTable.Add(this.GetPrefix());
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002DAC File Offset: 0x00000FAC
		private string GetPrefix()
		{
			XElement xelement = this.source as XElement;
			if (xelement == null)
			{
				XAttribute xattribute = this.source as XAttribute;
				if (xattribute != null)
				{
					if (this.parent != null)
					{
						return string.Empty;
					}
					string prefixOfNamespace = xattribute.GetPrefixOfNamespace(xattribute.Name.Namespace);
					if (prefixOfNamespace != null)
					{
						return prefixOfNamespace;
					}
				}
				return string.Empty;
			}
			string prefixOfNamespace2 = xelement.GetPrefixOfNamespace(xelement.Name.Namespace);
			if (prefixOfNamespace2 != null)
			{
				return prefixOfNamespace2;
			}
			return string.Empty;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002E1E File Offset: 0x0000101E
		public override object UnderlyingObject
		{
			get
			{
				if (this.source is string)
				{
					this.source = this.parent.LastNode;
					this.parent = null;
				}
				return this.source;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002E4C File Offset: 0x0000104C
		public override string Value
		{
			get
			{
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					switch (xobject.NodeType)
					{
					case XmlNodeType.Element:
						return ((XElement)xobject).Value;
					case XmlNodeType.Attribute:
						return ((XAttribute)xobject).Value;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						return XNodeNavigator.CollectText((XText)xobject);
					case XmlNodeType.ProcessingInstruction:
						return ((XProcessingInstruction)xobject).Data;
					case XmlNodeType.Comment:
						return ((XComment)xobject).Value;
					case XmlNodeType.Document:
					{
						XElement root = ((XDocument)xobject).Root;
						if (root == null)
						{
							return string.Empty;
						}
						return root.Value;
					}
					}
					return string.Empty;
				}
				return (string)this.source;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F09 File Offset: 0x00001109
		public override bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			throw new NotSupportedException(Res.GetString("NotSupported_CheckValidity"));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002F1A File Offset: 0x0000111A
		public override XPathNavigator Clone()
		{
			return new XNodeNavigator(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F24 File Offset: 0x00001124
		public override bool IsSamePosition(XPathNavigator navigator)
		{
			XNodeNavigator xnodeNavigator = navigator as XNodeNavigator;
			return xnodeNavigator != null && XNodeNavigator.IsSamePosition(this, xnodeNavigator);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002F44 File Offset: 0x00001144
		public override bool MoveTo(XPathNavigator navigator)
		{
			XNodeNavigator xnodeNavigator = navigator as XNodeNavigator;
			if (xnodeNavigator != null)
			{
				this.source = xnodeNavigator.source;
				this.parent = xnodeNavigator.parent;
				return true;
			}
			return false;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F78 File Offset: 0x00001178
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				XAttribute xattribute = xelement.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == localName && xattribute.Name.NamespaceName == namespaceName && !xattribute.IsNamespaceDeclaration)
						{
							break;
						}
						if (xattribute == xelement.lastAttr)
						{
							return false;
						}
					}
					this.source = xattribute;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002FE8 File Offset: 0x000011E8
		public override bool MoveToChild(string localName, string namespaceName)
		{
			XContainer xcontainer = this.source as XContainer;
			if (xcontainer != null && xcontainer.content != null)
			{
				XNode xnode = xcontainer.content as XNode;
				if (xnode != null)
				{
					XElement xelement;
					for (;;)
					{
						xnode = xnode.next;
						xelement = (xnode as XElement);
						if (xelement != null && xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
						{
							break;
						}
						if (xnode == xcontainer.content)
						{
							return false;
						}
					}
					this.source = xelement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003068 File Offset: 0x00001268
		public override bool MoveToChild(XPathNodeType type)
		{
			XContainer xcontainer = this.source as XContainer;
			if (xcontainer != null && xcontainer.content != null)
			{
				XNode xnode = xcontainer.content as XNode;
				if (xnode != null)
				{
					int num = XNodeNavigator.GetElementContentMask(type);
					if ((24 & num) != 0 && xcontainer.parent == null && xcontainer is XDocument)
					{
						num &= -25;
					}
					for (;;)
					{
						xnode = xnode.next;
						if ((1 << (int)xnode.NodeType & num) != 0)
						{
							break;
						}
						if (xnode == xcontainer.content)
						{
							return false;
						}
					}
					this.source = xnode;
					return true;
				}
				string text = (string)xcontainer.content;
				if (text.Length != 0)
				{
					int elementContentMask = XNodeNavigator.GetElementContentMask(type);
					if ((24 & elementContentMask) != 0 && xcontainer.parent == null && xcontainer is XDocument)
					{
						return false;
					}
					if ((8 & elementContentMask) != 0)
					{
						this.source = text;
						this.parent = (XElement)xcontainer;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003140 File Offset: 0x00001340
		public override bool MoveToFirstAttribute()
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				XAttribute xattribute = xelement.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (!xattribute.IsNamespaceDeclaration)
						{
							break;
						}
						if (xattribute == xelement.lastAttr)
						{
							return false;
						}
					}
					this.source = xattribute;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003188 File Offset: 0x00001388
		public override bool MoveToFirstChild()
		{
			XContainer xcontainer = this.source as XContainer;
			if (xcontainer != null && xcontainer.content != null)
			{
				XNode xnode = xcontainer.content as XNode;
				if (xnode != null)
				{
					for (;;)
					{
						xnode = xnode.next;
						if (XNodeNavigator.IsContent(xcontainer, xnode))
						{
							break;
						}
						if (xnode == xcontainer.content)
						{
							return false;
						}
					}
					this.source = xnode;
					return true;
				}
				string text = (string)xcontainer.content;
				if (text.Length != 0 && (xcontainer.parent != null || xcontainer is XElement))
				{
					this.source = text;
					this.parent = (XElement)xcontainer;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000321C File Offset: 0x0000141C
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				XAttribute xattribute = null;
				switch (scope)
				{
				case XPathNamespaceScope.All:
					xattribute = XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement);
					if (xattribute == null)
					{
						xattribute = XNodeNavigator.GetXmlNamespaceDeclaration();
					}
					break;
				case XPathNamespaceScope.ExcludeXml:
					for (xattribute = XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement); xattribute != null; xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute))
					{
						if (!(xattribute.Name.LocalName == "xml"))
						{
							break;
						}
					}
					break;
				case XPathNamespaceScope.Local:
					xattribute = XNodeNavigator.GetFirstNamespaceDeclarationLocal(xelement);
					break;
				}
				if (xattribute != null)
				{
					this.source = xattribute;
					this.parent = xelement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000032A7 File Offset: 0x000014A7
		public override bool MoveToId(string id)
		{
			throw new NotSupportedException(Res.GetString("NotSupported_MoveToId"));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032B8 File Offset: 0x000014B8
		public override bool MoveToNamespace(string localName)
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				if (localName == "xmlns")
				{
					return false;
				}
				if (localName != null && localName.Length == 0)
				{
					localName = "xmlns";
				}
				for (XAttribute xattribute = XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement); xattribute != null; xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute))
				{
					if (xattribute.Name.LocalName == localName)
					{
						this.source = xattribute;
						this.parent = xelement;
						return true;
					}
				}
				if (localName == "xml")
				{
					this.source = XNodeNavigator.GetXmlNamespaceDeclaration();
					this.parent = xelement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003350 File Offset: 0x00001550
		public override bool MoveToNext()
		{
			XNode xnode = this.source as XNode;
			if (xnode != null)
			{
				XContainer xcontainer = xnode.parent;
				if (xcontainer != null && xnode != xcontainer.content)
				{
					XNode next;
					for (;;)
					{
						next = xnode.next;
						if (XNodeNavigator.IsContent(xcontainer, next) && (!(xnode is XText) || !(next is XText)))
						{
							break;
						}
						xnode = next;
						if (xnode == xcontainer.content)
						{
							return false;
						}
					}
					this.source = next;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000033B4 File Offset: 0x000015B4
		public override bool MoveToNext(string localName, string namespaceName)
		{
			XNode xnode = this.source as XNode;
			if (xnode != null)
			{
				XContainer xcontainer = xnode.parent;
				if (xcontainer != null && xnode != xcontainer.content)
				{
					XElement xelement;
					for (;;)
					{
						xnode = xnode.next;
						xelement = (xnode as XElement);
						if (xelement != null && xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
						{
							break;
						}
						if (xnode == xcontainer.content)
						{
							return false;
						}
					}
					this.source = xelement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003430 File Offset: 0x00001630
		public override bool MoveToNext(XPathNodeType type)
		{
			XNode xnode = this.source as XNode;
			if (xnode != null)
			{
				XContainer xcontainer = xnode.parent;
				if (xcontainer != null && xnode != xcontainer.content)
				{
					int num = XNodeNavigator.GetElementContentMask(type);
					if ((24 & num) != 0 && xcontainer.parent == null && xcontainer is XDocument)
					{
						num &= -25;
					}
					XNode next;
					for (;;)
					{
						next = xnode.next;
						if ((1 << (int)next.NodeType & num) != 0 && (!(xnode is XText) || !(next is XText)))
						{
							break;
						}
						xnode = next;
						if (xnode == xcontainer.content)
						{
							return false;
						}
					}
					this.source = next;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000034BC File Offset: 0x000016BC
		public override bool MoveToNextAttribute()
		{
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute != null && this.parent == null)
			{
				XElement xelement = (XElement)xattribute.parent;
				if (xelement != null)
				{
					while (xattribute != xelement.lastAttr)
					{
						xattribute = xattribute.next;
						if (!xattribute.IsNamespaceDeclaration)
						{
							this.source = xattribute;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003514 File Offset: 0x00001714
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute != null && this.parent != null && !XNodeNavigator.IsXmlNamespaceDeclaration(xattribute))
			{
				switch (scope)
				{
				case XPathNamespaceScope.All:
					do
					{
						xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute);
					}
					while (xattribute != null && XNodeNavigator.HasNamespaceDeclarationInScope(xattribute, this.parent));
					if (xattribute == null && !XNodeNavigator.HasNamespaceDeclarationInScope(XNodeNavigator.GetXmlNamespaceDeclaration(), this.parent))
					{
						xattribute = XNodeNavigator.GetXmlNamespaceDeclaration();
					}
					break;
				case XPathNamespaceScope.ExcludeXml:
					do
					{
						xattribute = XNodeNavigator.GetNextNamespaceDeclarationGlobal(xattribute);
						if (xattribute == null)
						{
							break;
						}
					}
					while (xattribute.Name.LocalName == "xml" || XNodeNavigator.HasNamespaceDeclarationInScope(xattribute, this.parent));
					break;
				case XPathNamespaceScope.Local:
					if (xattribute.parent != this.parent)
					{
						return false;
					}
					xattribute = XNodeNavigator.GetNextNamespaceDeclarationLocal(xattribute);
					break;
				}
				if (xattribute != null)
				{
					this.source = xattribute;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000035E8 File Offset: 0x000017E8
		public override bool MoveToParent()
		{
			if (this.parent != null)
			{
				this.source = this.parent;
				this.parent = null;
				return true;
			}
			XObject xobject = (XObject)this.source;
			if (xobject.parent != null)
			{
				this.source = xobject.parent;
				return true;
			}
			return false;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003638 File Offset: 0x00001838
		public override bool MoveToPrevious()
		{
			XNode xnode = this.source as XNode;
			if (xnode != null)
			{
				XContainer xcontainer = xnode.parent;
				if (xcontainer != null)
				{
					XNode xnode2 = (XNode)xcontainer.content;
					if (xnode2.next != xnode)
					{
						XNode xnode3 = null;
						do
						{
							xnode2 = xnode2.next;
							if (XNodeNavigator.IsContent(xcontainer, xnode2))
							{
								xnode3 = ((xnode3 is XText && xnode2 is XText) ? xnode3 : xnode2);
							}
						}
						while (xnode2.next != xnode);
						if (xnode3 != null)
						{
							this.source = xnode3;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000036B0 File Offset: 0x000018B0
		public override XmlReader ReadSubtree()
		{
			XContainer xcontainer = this.source as XContainer;
			if (xcontainer == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_BadNodeType", new object[]
				{
					this.NodeType
				}));
			}
			return new XNodeReader(xcontainer, this.nameTable);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000036FC File Offset: 0x000018FC
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003720 File Offset: 0x00001920
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003744 File Offset: 0x00001944
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003768 File Offset: 0x00001968
		private static string CollectText(XText n)
		{
			string text = n.Value;
			if (n.parent != null)
			{
				while (n != n.parent.content)
				{
					n = (n.next as XText);
					if (n == null)
					{
						break;
					}
					text += n.Value;
				}
			}
			return text;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000037B4 File Offset: 0x000019B4
		private static XmlNameTable CreateNameTable()
		{
			XmlNameTable xmlNameTable = new NameTable();
			xmlNameTable.Add(string.Empty);
			xmlNameTable.Add("http://www.w3.org/2000/xmlns/");
			xmlNameTable.Add("http://www.w3.org/XML/1998/namespace");
			return xmlNameTable;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000037EC File Offset: 0x000019EC
		private static bool IsContent(XContainer c, XNode n)
		{
			return c.parent != null || c is XElement || (1 << (int)n.NodeType & 386) != 0;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003814 File Offset: 0x00001A14
		private static bool IsSamePosition(XNodeNavigator n1, XNodeNavigator n2)
		{
			if (n1.source == n2.source && n1.parent == n2.parent)
			{
				return true;
			}
			if (n1.parent != null ^ n2.parent != null)
			{
				XText xtext = n1.source as XText;
				if (xtext != null)
				{
					return xtext.Value == n2.source && xtext.parent == n2.parent;
				}
				XText xtext2 = n2.source as XText;
				if (xtext2 != null)
				{
					return xtext2.Value == n1.source && xtext2.parent == n1.parent;
				}
			}
			return false;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000038B1 File Offset: 0x00001AB1
		private static bool IsXmlNamespaceDeclaration(XAttribute a)
		{
			return a == XNodeNavigator.GetXmlNamespaceDeclaration();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000038BB File Offset: 0x00001ABB
		private static int GetElementContentMask(XPathNodeType type)
		{
			return XNodeNavigator.ElementContentMasks[(int)type];
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000038C4 File Offset: 0x00001AC4
		private static XAttribute GetFirstNamespaceDeclarationGlobal(XElement e)
		{
			XAttribute firstNamespaceDeclarationLocal;
			for (;;)
			{
				firstNamespaceDeclarationLocal = XNodeNavigator.GetFirstNamespaceDeclarationLocal(e);
				if (firstNamespaceDeclarationLocal != null)
				{
					break;
				}
				e = (e.parent as XElement);
				if (e == null)
				{
					goto Block_1;
				}
			}
			return firstNamespaceDeclarationLocal;
			Block_1:
			return null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000038F0 File Offset: 0x00001AF0
		private static XAttribute GetFirstNamespaceDeclarationLocal(XElement e)
		{
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				for (;;)
				{
					xattribute = xattribute.next;
					if (xattribute.IsNamespaceDeclaration)
					{
						break;
					}
					if (xattribute == e.lastAttr)
					{
						goto IL_24;
					}
				}
				return xattribute;
			}
			IL_24:
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003924 File Offset: 0x00001B24
		private static XAttribute GetNextNamespaceDeclarationGlobal(XAttribute a)
		{
			XElement xelement = (XElement)a.parent;
			if (xelement == null)
			{
				return null;
			}
			XAttribute nextNamespaceDeclarationLocal = XNodeNavigator.GetNextNamespaceDeclarationLocal(a);
			if (nextNamespaceDeclarationLocal != null)
			{
				return nextNamespaceDeclarationLocal;
			}
			xelement = (xelement.parent as XElement);
			if (xelement == null)
			{
				return null;
			}
			return XNodeNavigator.GetFirstNamespaceDeclarationGlobal(xelement);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003968 File Offset: 0x00001B68
		private static XAttribute GetNextNamespaceDeclarationLocal(XAttribute a)
		{
			XElement xelement = (XElement)a.parent;
			if (xelement == null)
			{
				return null;
			}
			while (a != xelement.lastAttr)
			{
				a = a.next;
				if (a.IsNamespaceDeclaration)
				{
					return a;
				}
			}
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000039A2 File Offset: 0x00001BA2
		private static XAttribute GetXmlNamespaceDeclaration()
		{
			if (XNodeNavigator.XmlNamespaceDeclaration == null)
			{
				Interlocked.CompareExchange<XAttribute>(ref XNodeNavigator.XmlNamespaceDeclaration, new XAttribute(XNamespace.Xmlns.GetName("xml"), "http://www.w3.org/XML/1998/namespace"), null);
			}
			return XNodeNavigator.XmlNamespaceDeclaration;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000039D8 File Offset: 0x00001BD8
		private static bool HasNamespaceDeclarationInScope(XAttribute a, XElement e)
		{
			XName name = a.Name;
			while (e != null && e != a.parent)
			{
				if (e.Attribute(name) != null)
				{
					return true;
				}
				e = (e.parent as XElement);
			}
			return false;
		}

		// Token: 0x0400005A RID: 90
		private const int DocumentContentMask = 386;

		// Token: 0x0400005B RID: 91
		private static readonly int[] ElementContentMasks = new int[]
		{
			0,
			2,
			0,
			0,
			24,
			0,
			0,
			128,
			256,
			410
		};

		// Token: 0x0400005C RID: 92
		private new const int TextMask = 24;

		// Token: 0x0400005D RID: 93
		private static XAttribute XmlNamespaceDeclaration;

		// Token: 0x0400005E RID: 94
		private object source;

		// Token: 0x0400005F RID: 95
		private XElement parent;

		// Token: 0x04000060 RID: 96
		private XmlNameTable nameTable;
	}
}
