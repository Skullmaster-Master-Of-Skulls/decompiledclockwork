using System;

namespace System.Xml.Linq
{
	// Token: 0x0200002F RID: 47
	internal class XNodeReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000A174 File Offset: 0x00008374
		internal XNodeReader(XNode node, XmlNameTable nameTable, ReaderOptions options)
		{
			this.source = node;
			this.root = node;
			this.nameTable = ((nameTable != null) ? nameTable : XNodeReader.CreateNameTable());
			this.omitDuplicateNamespaces = ((options & ReaderOptions.OmitDuplicateNamespaces) != ReaderOptions.None);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A1AA File Offset: 0x000083AA
		internal XNodeReader(XNode node, XmlNameTable nameTable) : this(node, nameTable, ((node.GetSaveOptionsFromAnnotations() & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None) ? ReaderOptions.OmitDuplicateNamespaces : ReaderOptions.None)
		{
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public override int AttributeCount
		{
			get
			{
				if (!this.IsInteractive)
				{
					return 0;
				}
				int num = 0;
				XElement elementInAttributeScope = this.GetElementInAttributeScope();
				if (elementInAttributeScope != null)
				{
					XAttribute xattribute = elementInAttributeScope.lastAttr;
					if (xattribute != null)
					{
						do
						{
							xattribute = xattribute.next;
							if (!this.omitDuplicateNamespaces || !this.IsDuplicateNamespaceAttribute(xattribute))
							{
								num++;
							}
						}
						while (xattribute != elementInAttributeScope.lastAttr);
					}
				}
				return num;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A218 File Offset: 0x00008418
		public override string BaseURI
		{
			get
			{
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					return xobject.BaseUri;
				}
				xobject = (this.parent as XObject);
				if (xobject != null)
				{
					return xobject.BaseUri;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A258 File Offset: 0x00008458
		public override int Depth
		{
			get
			{
				if (!this.IsInteractive)
				{
					return 0;
				}
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					return XNodeReader.GetDepth(xobject);
				}
				xobject = (this.parent as XObject);
				if (xobject != null)
				{
					return XNodeReader.GetDepth(xobject) + 1;
				}
				return 0;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A2A0 File Offset: 0x000084A0
		private static int GetDepth(XObject o)
		{
			int num = 0;
			while (o.parent != null)
			{
				num++;
				o = o.parent;
			}
			if (o is XDocument)
			{
				num--;
			}
			return num;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A2D2 File Offset: 0x000084D2
		public override bool EOF
		{
			get
			{
				return this.state == ReadState.EndOfFile;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public override bool HasAttributes
		{
			get
			{
				if (!this.IsInteractive)
				{
					return false;
				}
				XElement elementInAttributeScope = this.GetElementInAttributeScope();
				return elementInAttributeScope != null && elementInAttributeScope.lastAttr != null && (!this.omitDuplicateNamespaces || this.GetFirstNonDuplicateNamespaceAttribute(elementInAttributeScope.lastAttr.next) != null);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A32C File Offset: 0x0000852C
		public override bool HasValue
		{
			get
			{
				if (!this.IsInteractive)
				{
					return false;
				}
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					switch (xobject.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
						return true;
					}
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A38C File Offset: 0x0000858C
		public override bool IsEmptyElement
		{
			get
			{
				if (!this.IsInteractive)
				{
					return false;
				}
				XElement xelement = this.source as XElement;
				return xelement != null && xelement.IsEmpty;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A3BA File Offset: 0x000085BA
		public override string LocalName
		{
			get
			{
				return this.nameTable.Add(this.GetLocalName());
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A3D0 File Offset: 0x000085D0
		private string GetLocalName()
		{
			if (!this.IsInteractive)
			{
				return string.Empty;
			}
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				return xelement.Name.LocalName;
			}
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute != null)
			{
				return xattribute.Name.LocalName;
			}
			XProcessingInstruction xprocessingInstruction = this.source as XProcessingInstruction;
			if (xprocessingInstruction != null)
			{
				return xprocessingInstruction.Target;
			}
			XDocumentType xdocumentType = this.source as XDocumentType;
			if (xdocumentType != null)
			{
				return xdocumentType.Name;
			}
			return string.Empty;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A454 File Offset: 0x00008654
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A49E File Offset: 0x0000869E
		public override string NamespaceURI
		{
			get
			{
				return this.nameTable.Add(this.GetNamespaceURI());
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A4B4 File Offset: 0x000086B4
		private string GetNamespaceURI()
		{
			if (!this.IsInteractive)
			{
				return string.Empty;
			}
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
			string namespaceName = xattribute.Name.NamespaceName;
			if (namespaceName.Length == 0 && xattribute.Name.LocalName == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return namespaceName;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A531 File Offset: 0x00008731
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A53C File Offset: 0x0000873C
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.IsInteractive)
				{
					return XmlNodeType.None;
				}
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					if (this.IsEndElement)
					{
						return XmlNodeType.EndElement;
					}
					XmlNodeType nodeType = xobject.NodeType;
					if (nodeType != XmlNodeType.Text)
					{
						return nodeType;
					}
					if (xobject.parent != null && xobject.parent.parent == null && xobject.parent is XDocument)
					{
						return XmlNodeType.Whitespace;
					}
					return XmlNodeType.Text;
				}
				else
				{
					if (this.parent is XDocument)
					{
						return XmlNodeType.Whitespace;
					}
					return XmlNodeType.Text;
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A5B2 File Offset: 0x000087B2
		public override string Prefix
		{
			get
			{
				return this.nameTable.Add(this.GetPrefix());
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A5C8 File Offset: 0x000087C8
		private string GetPrefix()
		{
			if (!this.IsInteractive)
			{
				return string.Empty;
			}
			XElement xelement = this.source as XElement;
			if (xelement == null)
			{
				XAttribute xattribute = this.source as XAttribute;
				if (xattribute != null)
				{
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A63A File Offset: 0x0000883A
		public override ReadState ReadState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A644 File Offset: 0x00008844
		public override XmlReaderSettings Settings
		{
			get
			{
				return new XmlReaderSettings
				{
					CheckCharacters = false
				};
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A660 File Offset: 0x00008860
		public override string Value
		{
			get
			{
				if (!this.IsInteractive)
				{
					return string.Empty;
				}
				XObject xobject = this.source as XObject;
				if (xobject != null)
				{
					switch (xobject.NodeType)
					{
					case XmlNodeType.Attribute:
						return ((XAttribute)xobject).Value;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						return ((XText)xobject).Value;
					case XmlNodeType.ProcessingInstruction:
						return ((XProcessingInstruction)xobject).Data;
					case XmlNodeType.Comment:
						return ((XComment)xobject).Value;
					case XmlNodeType.DocumentType:
						return ((XDocumentType)xobject).InternalSubset;
					}
					return string.Empty;
				}
				return (string)this.source;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A70C File Offset: 0x0000890C
		public override string XmlLang
		{
			get
			{
				if (!this.IsInteractive)
				{
					return string.Empty;
				}
				XElement xelement = this.GetElementInScope();
				if (xelement != null)
				{
					XName name = XNamespace.Xml.GetName("lang");
					XAttribute xattribute;
					for (;;)
					{
						xattribute = xelement.Attribute(name);
						if (xattribute != null)
						{
							break;
						}
						xelement = (xelement.parent as XElement);
						if (xelement == null)
						{
							goto IL_49;
						}
					}
					return xattribute.Value;
				}
				IL_49:
				return string.Empty;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A768 File Offset: 0x00008968
		public override XmlSpace XmlSpace
		{
			get
			{
				if (!this.IsInteractive)
				{
					return XmlSpace.None;
				}
				XElement xelement = this.GetElementInScope();
				if (xelement != null)
				{
					XName name = XNamespace.Xml.GetName("space");
					for (;;)
					{
						XAttribute xattribute = xelement.Attribute(name);
						if (xattribute != null)
						{
							string a = xattribute.Value.Trim(new char[]
							{
								' ',
								'\t',
								'\n',
								'\r'
							});
							if (a == "preserve")
							{
								break;
							}
							if (a == "default")
							{
								return XmlSpace.Default;
							}
						}
						xelement = (xelement.parent as XElement);
						if (xelement == null)
						{
							return XmlSpace.None;
						}
					}
					return XmlSpace.Preserve;
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A7F1 File Offset: 0x000089F1
		public override void Close()
		{
			this.source = null;
			this.parent = null;
			this.root = null;
			this.state = ReadState.Closed;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A810 File Offset: 0x00008A10
		public override string GetAttribute(string name)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				string b;
				string b2;
				XNodeReader.GetNameInAttributeScope(name, elementInAttributeScope, out b, out b2);
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == b && xattribute.Name.NamespaceName == b2)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_82;
						}
					}
					if (this.omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return null;
					}
					return xattribute.Value;
				}
				IL_82:
				return null;
			}
			XDocumentType xdocumentType = this.source as XDocumentType;
			if (xdocumentType != null)
			{
				if (name == "PUBLIC")
				{
					return xdocumentType.PublicId;
				}
				if (name == "SYSTEM")
				{
					return xdocumentType.SystemId;
				}
			}
			return null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A8DC File Offset: 0x00008ADC
		public override string GetAttribute(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				if (localName == "xmlns")
				{
					if (namespaceName != null && namespaceName.Length == 0)
					{
						return null;
					}
					if (namespaceName == "http://www.w3.org/2000/xmlns/")
					{
						namespaceName = string.Empty;
					}
				}
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == localName && xattribute.Name.NamespaceName == namespaceName)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_9F;
						}
					}
					if (this.omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return null;
					}
					return xattribute.Value;
				}
			}
			IL_9F:
			return null;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A98C File Offset: 0x00008B8C
		public override string GetAttribute(int index)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if ((!this.omitDuplicateNamespaces || !this.IsDuplicateNamespaceAttribute(xattribute)) && index-- == 0)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_54;
						}
					}
					return xattribute.Value;
				}
			}
			IL_54:
			return null;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		public override string LookupNamespace(string prefix)
		{
			if (!this.IsInteractive)
			{
				return null;
			}
			if (prefix == null)
			{
				return null;
			}
			XElement elementInScope = this.GetElementInScope();
			if (elementInScope != null)
			{
				XNamespace xnamespace = (prefix.Length == 0) ? elementInScope.GetDefaultNamespace() : elementInScope.GetNamespaceOfPrefix(prefix);
				if (xnamespace != null)
				{
					return this.nameTable.Add(xnamespace.NamespaceName);
				}
			}
			return null;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public override bool MoveToAttribute(string name)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				string b;
				string b2;
				XNodeReader.GetNameInAttributeScope(name, elementInAttributeScope, out b, out b2);
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == b && xattribute.Name.NamespaceName == b2)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							return false;
						}
					}
					if (this.omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return false;
					}
					this.source = xattribute;
					this.parent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AADC File Offset: 0x00008CDC
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				if (localName == "xmlns")
				{
					if (namespaceName != null && namespaceName.Length == 0)
					{
						return false;
					}
					if (namespaceName == "http://www.w3.org/2000/xmlns/")
					{
						namespaceName = string.Empty;
					}
				}
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if (xattribute.Name.LocalName == localName && xattribute.Name.NamespaceName == namespaceName)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							return false;
						}
					}
					if (this.omitDuplicateNamespaces && this.IsDuplicateNamespaceAttribute(xattribute))
					{
						return false;
					}
					this.source = xattribute;
					this.parent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AB94 File Offset: 0x00008D94
		public override void MoveToAttribute(int index)
		{
			if (!this.IsInteractive)
			{
				return;
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null)
			{
				XAttribute xattribute = elementInAttributeScope.lastAttr;
				if (xattribute != null)
				{
					for (;;)
					{
						xattribute = xattribute.next;
						if ((!this.omitDuplicateNamespaces || !this.IsDuplicateNamespaceAttribute(xattribute)) && index-- == 0)
						{
							break;
						}
						if (xattribute == elementInAttributeScope.lastAttr)
						{
							goto IL_64;
						}
					}
					this.source = xattribute;
					this.parent = null;
					return;
				}
			}
			IL_64:
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AC10 File Offset: 0x00008E10
		public override bool MoveToElement()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute == null)
			{
				xattribute = (this.parent as XAttribute);
			}
			if (xattribute != null && xattribute.parent != null)
			{
				this.source = xattribute.parent;
				this.parent = null;
				return true;
			}
			return false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AC64 File Offset: 0x00008E64
		public override bool MoveToFirstAttribute()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement elementInAttributeScope = this.GetElementInAttributeScope();
			if (elementInAttributeScope != null && elementInAttributeScope.lastAttr != null)
			{
				if (this.omitDuplicateNamespaces)
				{
					object firstNonDuplicateNamespaceAttribute = this.GetFirstNonDuplicateNamespaceAttribute(elementInAttributeScope.lastAttr.next);
					if (firstNonDuplicateNamespaceAttribute == null)
					{
						return false;
					}
					this.source = firstNonDuplicateNamespaceAttribute;
				}
				else
				{
					this.source = elementInAttributeScope.lastAttr.next;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000ACCC File Offset: 0x00008ECC
		public override bool MoveToNextAttribute()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				if (this.IsEndElement)
				{
					return false;
				}
				if (xelement.lastAttr != null)
				{
					if (this.omitDuplicateNamespaces)
					{
						object firstNonDuplicateNamespaceAttribute = this.GetFirstNonDuplicateNamespaceAttribute(xelement.lastAttr.next);
						if (firstNonDuplicateNamespaceAttribute == null)
						{
							return false;
						}
						this.source = firstNonDuplicateNamespaceAttribute;
					}
					else
					{
						this.source = xelement.lastAttr.next;
					}
					return true;
				}
				return false;
			}
			else
			{
				XAttribute xattribute = this.source as XAttribute;
				if (xattribute == null)
				{
					xattribute = (this.parent as XAttribute);
				}
				if (xattribute != null && xattribute.parent != null && ((XElement)xattribute.parent).lastAttr != xattribute)
				{
					if (this.omitDuplicateNamespaces)
					{
						object firstNonDuplicateNamespaceAttribute2 = this.GetFirstNonDuplicateNamespaceAttribute(xattribute.next);
						if (firstNonDuplicateNamespaceAttribute2 == null)
						{
							return false;
						}
						this.source = firstNonDuplicateNamespaceAttribute2;
					}
					else
					{
						this.source = xattribute.next;
					}
					this.parent = null;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public override bool Read()
		{
			ReadState readState = this.state;
			if (readState != ReadState.Initial)
			{
				return readState == ReadState.Interactive && this.Read(false);
			}
			this.state = ReadState.Interactive;
			XDocument xdocument = this.source as XDocument;
			return xdocument == null || this.ReadIntoDocument(xdocument);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000ADFC File Offset: 0x00008FFC
		public override bool ReadAttributeValue()
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			XAttribute xattribute = this.source as XAttribute;
			return xattribute != null && this.ReadIntoAttribute(xattribute);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AE2C File Offset: 0x0000902C
		public override bool ReadToDescendant(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			this.MoveToElement();
			XElement xelement = this.source as XElement;
			if (xelement != null && !xelement.IsEmpty)
			{
				if (this.IsEndElement)
				{
					return false;
				}
				foreach (XElement xelement2 in xelement.Descendants())
				{
					if (xelement2.Name.LocalName == localName && xelement2.Name.NamespaceName == namespaceName)
					{
						this.source = xelement2;
						return true;
					}
				}
				this.IsEndElement = true;
				return false;
			}
			return false;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public override bool ReadToFollowing(string localName, string namespaceName)
		{
			while (this.Read())
			{
				XElement xelement = this.source as XElement;
				if (xelement != null && !this.IsEndElement && xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AF38 File Offset: 0x00009138
		public override bool ReadToNextSibling(string localName, string namespaceName)
		{
			if (!this.IsInteractive)
			{
				return false;
			}
			this.MoveToElement();
			if (this.source != this.root)
			{
				XNode xnode = this.source as XNode;
				if (xnode != null)
				{
					foreach (XElement xelement in xnode.ElementsAfterSelf())
					{
						if (xelement.Name.LocalName == localName && xelement.Name.NamespaceName == namespaceName)
						{
							this.source = xelement;
							this.IsEndElement = false;
							return true;
						}
					}
					if (xnode.parent is XElement)
					{
						this.source = xnode.parent;
						this.IsEndElement = true;
						return false;
					}
					goto IL_E0;
				}
				if (this.parent is XElement)
				{
					this.source = this.parent;
					this.parent = null;
					this.IsEndElement = true;
					return false;
				}
			}
			IL_E0:
			return this.ReadToEnd();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B040 File Offset: 0x00009240
		public override void ResolveEntity()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B042 File Offset: 0x00009242
		public override void Skip()
		{
			if (!this.IsInteractive)
			{
				return;
			}
			this.Read(true);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000B058 File Offset: 0x00009258
		internal override IDtdInfo DtdInfo
		{
			get
			{
				if (this.dtdInfoInitialized)
				{
					return this.dtdInfo;
				}
				this.dtdInfoInitialized = true;
				XDocumentType xdocumentType = this.source as XDocumentType;
				if (xdocumentType == null)
				{
					for (XNode xnode = this.root; xnode != null; xnode = xnode.parent)
					{
						XDocument xdocument = xnode as XDocument;
						if (xdocument != null)
						{
							xdocumentType = xdocument.DocumentType;
							break;
						}
					}
				}
				if (xdocumentType != null)
				{
					this.dtdInfo = xdocumentType.DtdInfo;
				}
				return this.dtdInfo;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B0C8 File Offset: 0x000092C8
		bool IXmlLineInfo.HasLineInfo()
		{
			if (this.IsEndElement)
			{
				XElement xelement = this.source as XElement;
				if (xelement != null)
				{
					return xelement.Annotation<LineInfoEndElementAnnotation>() != null;
				}
			}
			else
			{
				IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.HasLineInfo();
				}
			}
			return false;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000B110 File Offset: 0x00009310
		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (this.IsEndElement)
				{
					XElement xelement = this.source as XElement;
					if (xelement != null)
					{
						LineInfoEndElementAnnotation lineInfoEndElementAnnotation = xelement.Annotation<LineInfoEndElementAnnotation>();
						if (lineInfoEndElementAnnotation != null)
						{
							return lineInfoEndElementAnnotation.lineNumber;
						}
					}
				}
				else
				{
					IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LineNumber;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000B15C File Offset: 0x0000935C
		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (this.IsEndElement)
				{
					XElement xelement = this.source as XElement;
					if (xelement != null)
					{
						LineInfoEndElementAnnotation lineInfoEndElementAnnotation = xelement.Annotation<LineInfoEndElementAnnotation>();
						if (lineInfoEndElementAnnotation != null)
						{
							return lineInfoEndElementAnnotation.linePosition;
						}
					}
				}
				else
				{
					IXmlLineInfo xmlLineInfo = this.source as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LinePosition;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000B1A8 File Offset: 0x000093A8
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000B1B8 File Offset: 0x000093B8
		private bool IsEndElement
		{
			get
			{
				return this.parent == this.source;
			}
			set
			{
				this.parent = (value ? this.source : null);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000B1CC File Offset: 0x000093CC
		private bool IsInteractive
		{
			get
			{
				return this.state == ReadState.Interactive;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B1D8 File Offset: 0x000093D8
		private static XmlNameTable CreateNameTable()
		{
			XmlNameTable xmlNameTable = new NameTable();
			xmlNameTable.Add(string.Empty);
			xmlNameTable.Add("http://www.w3.org/2000/xmlns/");
			xmlNameTable.Add("http://www.w3.org/XML/1998/namespace");
			return xmlNameTable;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B210 File Offset: 0x00009410
		private XElement GetElementInAttributeScope()
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				if (this.IsEndElement)
				{
					return null;
				}
				return xelement;
			}
			else
			{
				XAttribute xattribute = this.source as XAttribute;
				if (xattribute != null)
				{
					return (XElement)xattribute.parent;
				}
				xattribute = (this.parent as XAttribute);
				if (xattribute != null)
				{
					return (XElement)xattribute.parent;
				}
				return null;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B270 File Offset: 0x00009470
		private XElement GetElementInScope()
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				return xelement;
			}
			XNode xnode = this.source as XNode;
			if (xnode != null)
			{
				return xnode.parent as XElement;
			}
			XAttribute xattribute = this.source as XAttribute;
			if (xattribute != null)
			{
				return (XElement)xattribute.parent;
			}
			xelement = (this.parent as XElement);
			if (xelement != null)
			{
				return xelement;
			}
			xattribute = (this.parent as XAttribute);
			if (xattribute != null)
			{
				return (XElement)xattribute.parent;
			}
			return null;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B2F4 File Offset: 0x000094F4
		private static void GetNameInAttributeScope(string qualifiedName, XElement e, out string localName, out string namespaceName)
		{
			if (qualifiedName != null && qualifiedName.Length != 0)
			{
				int num = qualifiedName.IndexOf(':');
				if (num != 0 && num != qualifiedName.Length - 1)
				{
					if (num == -1)
					{
						localName = qualifiedName;
						namespaceName = string.Empty;
						return;
					}
					XNamespace namespaceOfPrefix = e.GetNamespaceOfPrefix(qualifiedName.Substring(0, num));
					if (namespaceOfPrefix != null)
					{
						localName = qualifiedName.Substring(num + 1, qualifiedName.Length - num - 1);
						namespaceName = namespaceOfPrefix.NamespaceName;
						return;
					}
				}
			}
			localName = null;
			namespaceName = null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B370 File Offset: 0x00009570
		private bool Read(bool skipContent)
		{
			XElement xelement = this.source as XElement;
			if (xelement != null)
			{
				if (xelement.IsEmpty || this.IsEndElement || skipContent)
				{
					return this.ReadOverNode(xelement);
				}
				return this.ReadIntoElement(xelement);
			}
			else
			{
				XNode xnode = this.source as XNode;
				if (xnode != null)
				{
					return this.ReadOverNode(xnode);
				}
				XAttribute xattribute = this.source as XAttribute;
				if (xattribute != null)
				{
					return this.ReadOverAttribute(xattribute, skipContent);
				}
				return this.ReadOverText(skipContent);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B3E8 File Offset: 0x000095E8
		private bool ReadIntoDocument(XDocument d)
		{
			XNode xnode = d.content as XNode;
			if (xnode != null)
			{
				this.source = xnode.next;
				return true;
			}
			string text = d.content as string;
			if (text != null && text.Length > 0)
			{
				this.source = text;
				this.parent = d;
				return true;
			}
			return this.ReadToEnd();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B440 File Offset: 0x00009640
		private bool ReadIntoElement(XElement e)
		{
			XNode xnode = e.content as XNode;
			if (xnode != null)
			{
				this.source = xnode.next;
				return true;
			}
			string text = e.content as string;
			if (text != null)
			{
				if (text.Length > 0)
				{
					this.source = text;
					this.parent = e;
				}
				else
				{
					this.source = e;
					this.IsEndElement = true;
				}
				return true;
			}
			return this.ReadToEnd();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000B4A8 File Offset: 0x000096A8
		private bool ReadIntoAttribute(XAttribute a)
		{
			this.source = a.value;
			this.parent = a;
			return true;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000B4C0 File Offset: 0x000096C0
		private bool ReadOverAttribute(XAttribute a, bool skipContent)
		{
			XElement xelement = (XElement)a.parent;
			if (xelement == null)
			{
				return this.ReadToEnd();
			}
			if (xelement.IsEmpty || skipContent)
			{
				return this.ReadOverNode(xelement);
			}
			return this.ReadIntoElement(xelement);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B4FC File Offset: 0x000096FC
		private bool ReadOverNode(XNode n)
		{
			if (n == this.root)
			{
				return this.ReadToEnd();
			}
			XNode next = n.next;
			if (next == null || next == n || n == n.parent.content)
			{
				if (n.parent == null || (n.parent.parent == null && n.parent is XDocument))
				{
					return this.ReadToEnd();
				}
				this.source = n.parent;
				this.IsEndElement = true;
			}
			else
			{
				this.source = next;
				this.IsEndElement = false;
			}
			return true;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B584 File Offset: 0x00009784
		private bool ReadOverText(bool skipContent)
		{
			if (this.parent is XElement)
			{
				this.source = this.parent;
				this.parent = null;
				this.IsEndElement = true;
				return true;
			}
			if (this.parent is XAttribute)
			{
				XAttribute a = (XAttribute)this.parent;
				this.parent = null;
				return this.ReadOverAttribute(a, skipContent);
			}
			return this.ReadToEnd();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B5E9 File Offset: 0x000097E9
		private bool ReadToEnd()
		{
			this.state = ReadState.EndOfFile;
			return false;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B5F3 File Offset: 0x000097F3
		private bool IsDuplicateNamespaceAttribute(XAttribute candidateAttribute)
		{
			return candidateAttribute.IsNamespaceDeclaration && this.IsDuplicateNamespaceAttributeInner(candidateAttribute);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B608 File Offset: 0x00009808
		private bool IsDuplicateNamespaceAttributeInner(XAttribute candidateAttribute)
		{
			if (candidateAttribute.Name.LocalName == "xml")
			{
				return true;
			}
			XElement xelement = candidateAttribute.parent as XElement;
			if (xelement == this.root || xelement == null)
			{
				return false;
			}
			for (xelement = (xelement.parent as XElement); xelement != null; xelement = (xelement.parent as XElement))
			{
				XAttribute xattribute = xelement.lastAttr;
				if (xattribute != null)
				{
					while (!(xattribute.name == candidateAttribute.name))
					{
						xattribute = xattribute.next;
						if (xattribute == xelement.lastAttr)
						{
							goto IL_85;
						}
					}
					return xattribute.Value == candidateAttribute.Value;
				}
				IL_85:
				if (xelement == this.root)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B6B8 File Offset: 0x000098B8
		private XAttribute GetFirstNonDuplicateNamespaceAttribute(XAttribute candidate)
		{
			if (!this.IsDuplicateNamespaceAttribute(candidate))
			{
				return candidate;
			}
			XElement xelement = candidate.parent as XElement;
			if (xelement != null && candidate != xelement.lastAttr)
			{
				for (;;)
				{
					candidate = candidate.next;
					if (!this.IsDuplicateNamespaceAttribute(candidate))
					{
						break;
					}
					if (candidate == xelement.lastAttr)
					{
						goto IL_3F;
					}
				}
				return candidate;
			}
			IL_3F:
			return null;
		}

		// Token: 0x040000BC RID: 188
		private object source;

		// Token: 0x040000BD RID: 189
		private object parent;

		// Token: 0x040000BE RID: 190
		private ReadState state;

		// Token: 0x040000BF RID: 191
		private XNode root;

		// Token: 0x040000C0 RID: 192
		private XmlNameTable nameTable;

		// Token: 0x040000C1 RID: 193
		private bool omitDuplicateNamespaces;

		// Token: 0x040000C2 RID: 194
		private IDtdInfo dtdInfo;

		// Token: 0x040000C3 RID: 195
		private bool dtdInfoInitialized;
	}
}
