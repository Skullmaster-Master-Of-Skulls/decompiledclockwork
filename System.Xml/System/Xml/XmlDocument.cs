using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D5 RID: 213
	public class XmlDocument : XmlNode
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x0003839C File Offset: 0x0003739C
		public XmlDocument() : this(new XmlImplementation())
		{
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000383A9 File Offset: 0x000373A9
		public XmlDocument(XmlNameTable nt) : this(new XmlImplementation(nt))
		{
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000383B8 File Offset: 0x000373B8
		protected internal XmlDocument(XmlImplementation imp)
		{
			this.implementation = imp;
			this.domNameTable = new DomNameTable(this);
			XmlNameTable nameTable = this.NameTable;
			nameTable.Add(string.Empty);
			this.strDocumentName = nameTable.Add("#document");
			this.strDocumentFragmentName = nameTable.Add("#document-fragment");
			this.strCommentName = nameTable.Add("#comment");
			this.strTextName = nameTable.Add("#text");
			this.strCDataSectionName = nameTable.Add("#cdata-section");
			this.strEntityName = nameTable.Add("#entity");
			this.strID = nameTable.Add("id");
			this.strNonSignificantWhitespaceName = nameTable.Add("#whitespace");
			this.strSignificantWhitespaceName = nameTable.Add("#significant-whitespace");
			this.strXmlns = nameTable.Add("xmlns");
			this.strXml = nameTable.Add("xml");
			this.strSpace = nameTable.Add("space");
			this.strLang = nameTable.Add("lang");
			this.strReservedXmlns = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.strReservedXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.strEmpty = nameTable.Add(string.Empty);
			this.baseURI = string.Empty;
			this.objLock = new object();
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00038517 File Offset: 0x00037517
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0003851F File Offset: 0x0003751F
		internal SchemaInfo DtdSchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00038528 File Offset: 0x00037528
		internal static void CheckName(string name)
		{
			XmlCharType instance = XmlCharType.Instance;
			for (int i = 0; i < name.Length; i++)
			{
				if (!instance.IsNCNameChar(name[i]))
				{
					throw new XmlException("Xml_BadNameChar", XmlException.BuildCharExceptionStr(name[i]));
				}
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00038574 File Offset: 0x00037574
		internal XmlName AddXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.AddName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00038594 File Offset: 0x00037594
		internal XmlName GetXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.GetName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000385B4 File Offset: 0x000375B4
		internal XmlName AddAttrXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			XmlName xmlName = this.AddXmlName(prefix, localName, namespaceURI, schemaInfo);
			if (!this.IsLoading)
			{
				object prefix2 = xmlName.Prefix;
				object namespaceURI2 = xmlName.NamespaceURI;
				object localName2 = xmlName.LocalName;
				if ((prefix2 == this.strXmlns || (prefix2 == this.strEmpty && localName2 == this.strXmlns)) ^ namespaceURI2 == this.strReservedXmlns)
				{
					throw new ArgumentException(Res.GetString("Xdom_Attr_Reserved_XmlNS", new object[]
					{
						namespaceURI
					}));
				}
			}
			return xmlName;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00038637 File Offset: 0x00037637
		internal bool AddIdInfo(XmlName eleName, XmlName attrName)
		{
			if (this.htElementIDAttrDecl == null || this.htElementIDAttrDecl[eleName] == null)
			{
				if (this.htElementIDAttrDecl == null)
				{
					this.htElementIDAttrDecl = new Hashtable();
				}
				this.htElementIDAttrDecl.Add(eleName, attrName);
				return true;
			}
			return false;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00038674 File Offset: 0x00037674
		private XmlName GetIDInfoByElement_(XmlName eleName)
		{
			XmlName xmlName = this.GetXmlName(eleName.Prefix, eleName.LocalName, string.Empty, null);
			if (xmlName != null)
			{
				return (XmlName)this.htElementIDAttrDecl[xmlName];
			}
			return null;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000386B0 File Offset: 0x000376B0
		internal XmlName GetIDInfoByElement(XmlName eleName)
		{
			if (this.htElementIDAttrDecl == null)
			{
				return null;
			}
			return this.GetIDInfoByElement_(eleName);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000386C4 File Offset: 0x000376C4
		private WeakReference GetElement(ArrayList elementList, XmlElement elem)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in elementList)
			{
				WeakReference weakReference = (WeakReference)obj;
				if (!weakReference.IsAlive)
				{
					arrayList.Add(weakReference);
				}
				else if ((XmlElement)weakReference.Target == elem)
				{
					return weakReference;
				}
			}
			foreach (object obj2 in arrayList)
			{
				WeakReference obj3 = (WeakReference)obj2;
				elementList.Remove(obj3);
			}
			return null;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00038790 File Offset: 0x00037790
		internal void AddElementWithId(string id, XmlElement elem)
		{
			if (this.htElementIdMap == null || !this.htElementIdMap.Contains(id))
			{
				if (this.htElementIdMap == null)
				{
					this.htElementIdMap = new Hashtable();
				}
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new WeakReference(elem));
				this.htElementIdMap.Add(id, arrayList);
				return;
			}
			ArrayList arrayList2 = (ArrayList)this.htElementIdMap[id];
			if (this.GetElement(arrayList2, elem) == null)
			{
				arrayList2.Add(new WeakReference(elem));
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00038810 File Offset: 0x00037810
		internal void RemoveElementWithId(string id, XmlElement elem)
		{
			if (this.htElementIdMap != null && this.htElementIdMap.Contains(id))
			{
				ArrayList arrayList = (ArrayList)this.htElementIdMap[id];
				WeakReference element = this.GetElement(arrayList, elem);
				if (element != null)
				{
					arrayList.Remove(element);
					if (arrayList.Count == 0)
					{
						this.htElementIdMap.Remove(id);
					}
				}
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003886C File Offset: 0x0003786C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument xmlDocument = this.Implementation.CreateDocument();
			xmlDocument.SetBaseURI(this.baseURI);
			if (deep)
			{
				xmlDocument.ImportChildren(this, xmlDocument, deep);
			}
			return xmlDocument;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0003889E File Offset: 0x0003789E
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x000388A2 File Offset: 0x000378A2
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000388A5 File Offset: 0x000378A5
		public virtual XmlDocumentType DocumentType
		{
			get
			{
				return (XmlDocumentType)this.FindChild(XmlNodeType.DocumentType);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x000388B4 File Offset: 0x000378B4
		internal virtual XmlDeclaration Declaration
		{
			get
			{
				if (this.HasChildNodes)
				{
					return this.FirstChild as XmlDeclaration;
				}
				return null;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000388D8 File Offset: 0x000378D8
		public XmlImplementation Implementation
		{
			get
			{
				return this.implementation;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x000388E0 File Offset: 0x000378E0
		public override string Name
		{
			get
			{
				return this.strDocumentName;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000388E8 File Offset: 0x000378E8
		public override string LocalName
		{
			get
			{
				return this.strDocumentName;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000388F0 File Offset: 0x000378F0
		public XmlElement DocumentElement
		{
			get
			{
				return (XmlElement)this.FindChild(XmlNodeType.Element);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000388FE File Offset: 0x000378FE
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00038901 File Offset: 0x00037901
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x00038909 File Offset: 0x00037909
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00038912 File Offset: 0x00037912
		public override XmlDocument OwnerDocument
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00038915 File Offset: 0x00037915
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00038936 File Offset: 0x00037936
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet(this.NameTable);
				}
				return this.schemas;
			}
			set
			{
				this.schemas = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0003893F File Offset: 0x0003793F
		internal bool CanReportValidity
		{
			get
			{
				return this.reportValidity;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00038947 File Offset: 0x00037947
		internal bool HasSetResolver
		{
			get
			{
				return this.bSetResolver;
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003894F File Offset: 0x0003794F
		internal XmlResolver GetResolver()
		{
			return this.resolver;
		}

		// Token: 0x170002EA RID: 746
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00038958 File Offset: 0x00037958
		public virtual XmlResolver XmlResolver
		{
			set
			{
				if (value != null)
				{
					try
					{
						new NamedPermissionSet("FullTrust").Demand();
					}
					catch (SecurityException inner)
					{
						throw new SecurityException(Res.GetString("Xml_UntrustedCodeSettingResolver"), inner);
					}
				}
				this.resolver = value;
				if (!this.bSetResolver)
				{
					this.bSetResolver = true;
				}
				XmlDocumentType documentType = this.DocumentType;
				if (documentType != null)
				{
					documentType.DtdSchemaInfo = null;
				}
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000389C4 File Offset: 0x000379C4
		internal override bool IsValidChildType(XmlNodeType type)
		{
			if (type != XmlNodeType.Element)
			{
				switch (type)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					return true;
				case XmlNodeType.DocumentType:
					if (this.DocumentType != null)
					{
						throw new InvalidOperationException(Res.GetString("Xdom_DualDocumentTypeNode"));
					}
					return true;
				case XmlNodeType.XmlDeclaration:
					if (this.Declaration != null)
					{
						throw new InvalidOperationException(Res.GetString("Xdom_DualDeclarationNode"));
					}
					return true;
				}
				return false;
			}
			if (this.DocumentElement != null)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_DualDocumentElementNode"));
			}
			return true;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00038A60 File Offset: 0x00037A60
		private bool HasNodeTypeInPrevSiblings(XmlNodeType nt, XmlNode refNode)
		{
			if (refNode == null)
			{
				return false;
			}
			XmlNode xmlNode = null;
			if (refNode.ParentNode != null)
			{
				xmlNode = refNode.ParentNode.FirstChild;
			}
			while (xmlNode != null)
			{
				if (xmlNode.NodeType == nt)
				{
					return true;
				}
				if (xmlNode == refNode)
				{
					break;
				}
				xmlNode = xmlNode.NextSibling;
			}
			return false;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00038AA4 File Offset: 0x00037AA4
		private bool HasNodeTypeInNextSiblings(XmlNodeType nt, XmlNode refNode)
		{
			for (XmlNode xmlNode = refNode; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == nt)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00038ACC File Offset: 0x00037ACC
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			if (refChild == null)
			{
				refChild = this.FirstChild;
			}
			if (refChild == null)
			{
				return true;
			}
			XmlNodeType nodeType = newChild.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					return refChild.NodeType != XmlNodeType.XmlDeclaration;
				case XmlNodeType.Document:
					break;
				case XmlNodeType.DocumentType:
					if (refChild.NodeType != XmlNodeType.XmlDeclaration)
					{
						return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild.PreviousSibling);
					}
					break;
				default:
					if (nodeType == XmlNodeType.XmlDeclaration)
					{
						return refChild == this.FirstChild;
					}
					break;
				}
			}
			else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
			{
				return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild);
			}
			return false;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00038B5C File Offset: 0x00037B5C
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			if (refChild == null)
			{
				refChild = this.LastChild;
			}
			if (refChild == null)
			{
				return true;
			}
			XmlNodeType nodeType = newChild.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					return true;
				case XmlNodeType.DocumentType:
					return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild);
				}
				return false;
			}
			return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild.NextSibling);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00038BD0 File Offset: 0x00037BD0
		public XmlAttribute CreateAttribute(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			this.SetDefaultNamespace(empty, empty2, ref empty3);
			return this.CreateAttribute(empty, empty2, empty3);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00038C0C File Offset: 0x00037C0C
		internal void SetDefaultNamespace(string prefix, string localName, ref string namespaceURI)
		{
			if (prefix == this.strXmlns || (prefix.Length == 0 && localName == this.strXmlns))
			{
				namespaceURI = this.strReservedXmlns;
				return;
			}
			if (prefix == this.strXml)
			{
				namespaceURI = this.strReservedXml;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00038C5C File Offset: 0x00037C5C
		public virtual XmlCDataSection CreateCDataSection(string data)
		{
			this.fCDataNodesPresent = true;
			return new XmlCDataSection(data, this);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00038C6C File Offset: 0x00037C6C
		public virtual XmlComment CreateComment(string data)
		{
			return new XmlComment(data, this);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00038C75 File Offset: 0x00037C75
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlDocumentType CreateDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentType(name, publicId, systemId, internalSubset, this);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00038C82 File Offset: 0x00037C82
		public virtual XmlDocumentFragment CreateDocumentFragment()
		{
			return new XmlDocumentFragment(this);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00038C8C File Offset: 0x00037C8C
		public XmlElement CreateElement(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			return this.CreateElement(empty, empty2, string.Empty);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00038CBC File Offset: 0x00037CBC
		internal void AddDefaultAttributes(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator enumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)enumerator.Value;
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed)
					{
						string attrPrefix = string.Empty;
						string name = schemaAttDef.Name.Name;
						string attrNamespaceURI = string.Empty;
						if (dtdSchemaInfo.SchemaType == SchemaType.DTD)
						{
							attrPrefix = schemaAttDef.Name.Namespace;
						}
						else
						{
							attrPrefix = schemaAttDef.Prefix;
							attrNamespaceURI = schemaAttDef.Name.Namespace;
						}
						XmlAttribute attributeNode = this.PrepareDefaultAttribute(schemaAttDef, attrPrefix, name, attrNamespaceURI);
						elem.SetAttributeNode(attributeNode);
					}
				}
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00038D80 File Offset: 0x00037D80
		private SchemaElementDecl GetSchemaElementDecl(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			if (dtdSchemaInfo != null)
			{
				XmlQualifiedName key = new XmlQualifiedName(elem.LocalName, (dtdSchemaInfo.SchemaType == SchemaType.DTD) ? elem.Prefix : elem.NamespaceURI);
				return (SchemaElementDecl)dtdSchemaInfo.ElementDecls[key];
			}
			return null;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00038DD0 File Offset: 0x00037DD0
		private XmlAttribute PrepareDefaultAttribute(SchemaAttDef attdef, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			this.SetDefaultNamespace(attrPrefix, attrLocalname, ref attrNamespaceURI);
			XmlAttribute xmlAttribute = this.CreateDefaultAttribute(attrPrefix, attrLocalname, attrNamespaceURI);
			xmlAttribute.InnerXml = attdef.DefaultValueRaw;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
			if (xmlUnspecifiedAttribute != null)
			{
				xmlUnspecifiedAttribute.SetSpecified(false);
			}
			return xmlAttribute;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00038E10 File Offset: 0x00037E10
		public virtual XmlEntityReference CreateEntityReference(string name)
		{
			return new XmlEntityReference(name, this);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00038E19 File Offset: 0x00037E19
		public virtual XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new XmlProcessingInstruction(target, data, this);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00038E23 File Offset: 0x00037E23
		public virtual XmlDeclaration CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclaration(version, encoding, standalone, this);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00038E2E File Offset: 0x00037E2E
		public virtual XmlText CreateTextNode(string text)
		{
			return new XmlText(text, this);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00038E37 File Offset: 0x00037E37
		public virtual XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new XmlSignificantWhitespace(text, this);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00038E40 File Offset: 0x00037E40
		public override XPathNavigator CreateNavigator()
		{
			return this.CreateNavigator(this);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00038E4C File Offset: 0x00037E4C
		protected internal virtual XPathNavigator CreateNavigator(XmlNode node)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.SignificantWhitespace:
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null)
				{
					for (;;)
					{
						XmlNodeType nodeType = parentNode.NodeType;
						if (nodeType == XmlNodeType.Attribute)
						{
							break;
						}
						if (nodeType != XmlNodeType.EntityReference)
						{
							goto IL_76;
						}
						parentNode = parentNode.ParentNode;
						if (parentNode == null)
						{
							goto IL_76;
						}
					}
					return null;
				}
				IL_76:
				node = this.NormalizeText(node);
				break;
			}
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return null;
			case XmlNodeType.Whitespace:
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null)
				{
					for (;;)
					{
						XmlNodeType nodeType = parentNode.NodeType;
						if (nodeType == XmlNodeType.Document || nodeType == XmlNodeType.Attribute)
						{
							break;
						}
						if (nodeType != XmlNodeType.EntityReference)
						{
							goto IL_AB;
						}
						parentNode = parentNode.ParentNode;
						if (parentNode == null)
						{
							goto IL_AB;
						}
					}
					return null;
				}
				IL_AB:
				node = this.NormalizeText(node);
				break;
			}
			}
			return new DocumentXPathNavigator(this, node);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00038F14 File Offset: 0x00037F14
		internal static bool IsTextNode(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			default:
				switch (nt)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00038F4C File Offset: 0x00037F4C
		private XmlNode NormalizeText(XmlNode n)
		{
			XmlNode xmlNode = null;
			while (XmlDocument.IsTextNode(n.NodeType))
			{
				xmlNode = n;
				n = n.PreviousSibling;
				if (n == null)
				{
					XmlNode xmlNode2 = xmlNode;
					while (xmlNode2.ParentNode != null && xmlNode2.ParentNode.NodeType == XmlNodeType.EntityReference)
					{
						if (xmlNode2.ParentNode.PreviousSibling != null)
						{
							n = xmlNode2.ParentNode.PreviousSibling;
							break;
						}
						xmlNode2 = xmlNode2.ParentNode;
						if (xmlNode2 == null)
						{
							break;
						}
					}
				}
				if (n == null)
				{
					break;
				}
				while (n.NodeType == XmlNodeType.EntityReference)
				{
					n = n.LastChild;
				}
			}
			return xmlNode;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00038FCC File Offset: 0x00037FCC
		public virtual XmlWhitespace CreateWhitespace(string text)
		{
			return new XmlWhitespace(text, this);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00038FD5 File Offset: 0x00037FD5
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00038FE0 File Offset: 0x00037FE0
		public XmlAttribute CreateAttribute(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateAttribute(empty, empty2, namespaceURI);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003900C File Offset: 0x0003800C
		public XmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateElement(empty, empty2, namespaceURI);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00039038 File Offset: 0x00038038
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00039044 File Offset: 0x00038044
		public virtual XmlElement GetElementById(string elementId)
		{
			if (this.htElementIdMap != null)
			{
				ArrayList arrayList = (ArrayList)this.htElementIdMap[elementId];
				if (arrayList != null)
				{
					foreach (object obj in arrayList)
					{
						WeakReference weakReference = (WeakReference)obj;
						XmlElement xmlElement = (XmlElement)weakReference.Target;
						if (xmlElement != null && xmlElement.IsConnected())
						{
							return xmlElement;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000390D4 File Offset: 0x000380D4
		public virtual XmlNode ImportNode(XmlNode node, bool deep)
		{
			return this.ImportNodeInternal(node, deep);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000390E0 File Offset: 0x000380E0
		private XmlNode ImportNodeInternal(XmlNode node, bool deep)
		{
			if (node == null)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Import_NullNode"));
			}
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
			{
				XmlNode xmlNode = this.CreateElement(node.Prefix, node.LocalName, node.NamespaceURI);
				this.ImportAttributes(node, xmlNode);
				if (deep)
				{
					this.ImportChildren(node, xmlNode, deep);
					return xmlNode;
				}
				return xmlNode;
			}
			case XmlNodeType.Attribute:
			{
				XmlNode xmlNode = this.CreateAttribute(node.Prefix, node.LocalName, node.NamespaceURI);
				this.ImportChildren(node, xmlNode, true);
				return xmlNode;
			}
			case XmlNodeType.Text:
				return this.CreateTextNode(node.Value);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(node.Value);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(node.Name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(node.Name, node.Value);
			case XmlNodeType.Comment:
				return this.CreateComment(node.Value);
			case XmlNodeType.DocumentType:
			{
				XmlDocumentType xmlDocumentType = (XmlDocumentType)node;
				return this.CreateDocumentType(xmlDocumentType.Name, xmlDocumentType.PublicId, xmlDocumentType.SystemId, xmlDocumentType.InternalSubset);
			}
			case XmlNodeType.DocumentFragment:
			{
				XmlNode xmlNode = this.CreateDocumentFragment();
				if (deep)
				{
					this.ImportChildren(node, xmlNode, deep);
					return xmlNode;
				}
				return xmlNode;
			}
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(node.Value);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(node.Value);
			case XmlNodeType.XmlDeclaration:
			{
				XmlDeclaration xmlDeclaration = (XmlDeclaration)node;
				return this.CreateXmlDeclaration(xmlDeclaration.Version, xmlDeclaration.Encoding, xmlDeclaration.Standalone);
			}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Xdom_Import"), new object[]
			{
				node.NodeType.ToString()
			}));
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000392CC File Offset: 0x000382CC
		private void ImportAttributes(XmlNode fromElem, XmlNode toElem)
		{
			int count = fromElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				if (fromElem.Attributes[i].Specified)
				{
					toElem.Attributes.SetNamedItem(this.ImportNodeInternal(fromElem.Attributes[i], true));
				}
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00039324 File Offset: 0x00038324
		private void ImportChildren(XmlNode fromNode, XmlNode toNode, bool deep)
		{
			for (XmlNode xmlNode = fromNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				toNode.AppendChild(this.ImportNodeInternal(xmlNode, deep));
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00039353 File Offset: 0x00038353
		public XmlNameTable NameTable
		{
			get
			{
				return this.implementation.NameTable;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00039360 File Offset: 0x00038360
		public virtual XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlAttribute(this.AddAttrXmlName(prefix, localName, namespaceURI, null), this);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00039372 File Offset: 0x00038372
		protected internal virtual XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlUnspecifiedAttribute(prefix, localName, namespaceURI, this);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00039380 File Offset: 0x00038380
		public virtual XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			XmlElement xmlElement = new XmlElement(this.AddXmlName(prefix, localName, namespaceURI, null), true, this);
			if (!this.IsLoading)
			{
				this.AddDefaultAttributes(xmlElement);
			}
			return xmlElement;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x000393AF File Offset: 0x000383AF
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x000393B7 File Offset: 0x000383B7
		public bool PreserveWhitespace
		{
			get
			{
				return this.preserveWhitespace;
			}
			set
			{
				this.preserveWhitespace = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000393C0 File Offset: 0x000383C0
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x000393C3 File Offset: 0x000383C3
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x000393DF File Offset: 0x000383DF
		internal XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
			set
			{
				this.entities = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000393E8 File Offset: 0x000383E8
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x000393F0 File Offset: 0x000383F0
		internal bool IsLoading
		{
			get
			{
				return this.isLoading;
			}
			set
			{
				this.isLoading = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x000393F9 File Offset: 0x000383F9
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00039401 File Offset: 0x00038401
		internal bool ActualLoadingStatus
		{
			get
			{
				return this.actualLoadingStatus;
			}
			set
			{
				this.actualLoadingStatus = value;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003940C File Offset: 0x0003840C
		public virtual XmlNode CreateNode(XmlNodeType type, string prefix, string name, string namespaceURI)
		{
			switch (type)
			{
			case XmlNodeType.Element:
				if (prefix != null)
				{
					return this.CreateElement(prefix, name, namespaceURI);
				}
				return this.CreateElement(name, namespaceURI);
			case XmlNodeType.Attribute:
				if (prefix != null)
				{
					return this.CreateAttribute(prefix, name, namespaceURI);
				}
				return this.CreateAttribute(name, namespaceURI);
			case XmlNodeType.Text:
				return this.CreateTextNode(string.Empty);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(string.Empty);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(name, string.Empty);
			case XmlNodeType.Comment:
				return this.CreateComment(string.Empty);
			case XmlNodeType.Document:
				return new XmlDocument();
			case XmlNodeType.DocumentType:
				return this.CreateDocumentType(name, string.Empty, string.Empty, string.Empty);
			case XmlNodeType.DocumentFragment:
				return this.CreateDocumentFragment();
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(string.Empty);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(string.Empty);
			case XmlNodeType.XmlDeclaration:
				return this.CreateXmlDeclaration("1.0", null, null);
			}
			throw new ArgumentException(Res.GetString("Arg_CannotCreateNode", new object[]
			{
				type
			}));
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0003953F File Offset: 0x0003853F
		public virtual XmlNode CreateNode(string nodeTypeString, string name, string namespaceURI)
		{
			return this.CreateNode(this.ConvertToNodeType(nodeTypeString), name, namespaceURI);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00039550 File Offset: 0x00038550
		public virtual XmlNode CreateNode(XmlNodeType type, string name, string namespaceURI)
		{
			return this.CreateNode(type, null, name, namespaceURI);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003955C File Offset: 0x0003855C
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlNode ReadNode(XmlReader reader)
		{
			XmlNode result = null;
			try
			{
				this.IsLoading = true;
				XmlLoader xmlLoader = new XmlLoader();
				result = xmlLoader.ReadCurrentNode(this, reader);
			}
			finally
			{
				this.IsLoading = false;
			}
			return result;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003959C File Offset: 0x0003859C
		internal XmlNodeType ConvertToNodeType(string nodeTypeString)
		{
			if (nodeTypeString == "element")
			{
				return XmlNodeType.Element;
			}
			if (nodeTypeString == "attribute")
			{
				return XmlNodeType.Attribute;
			}
			if (nodeTypeString == "text")
			{
				return XmlNodeType.Text;
			}
			if (nodeTypeString == "cdatasection")
			{
				return XmlNodeType.CDATA;
			}
			if (nodeTypeString == "entityreference")
			{
				return XmlNodeType.EntityReference;
			}
			if (nodeTypeString == "entity")
			{
				return XmlNodeType.Entity;
			}
			if (nodeTypeString == "processinginstruction")
			{
				return XmlNodeType.ProcessingInstruction;
			}
			if (nodeTypeString == "comment")
			{
				return XmlNodeType.Comment;
			}
			if (nodeTypeString == "document")
			{
				return XmlNodeType.Document;
			}
			if (nodeTypeString == "documenttype")
			{
				return XmlNodeType.DocumentType;
			}
			if (nodeTypeString == "documentfragment")
			{
				return XmlNodeType.DocumentFragment;
			}
			if (nodeTypeString == "notation")
			{
				return XmlNodeType.Notation;
			}
			if (nodeTypeString == "significantwhitespace")
			{
				return XmlNodeType.SignificantWhitespace;
			}
			if (nodeTypeString == "whitespace")
			{
				return XmlNodeType.Whitespace;
			}
			throw new ArgumentException(Res.GetString("Xdom_Invalid_NT_String", new object[]
			{
				nodeTypeString
			}));
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0003969C File Offset: 0x0003869C
		private XmlTextReader SetupReader(XmlTextReader tr)
		{
			tr.XmlValidatingReaderCompatibilityMode = true;
			tr.EntityHandling = EntityHandling.ExpandCharEntities;
			if (this.HasSetResolver)
			{
				tr.XmlResolver = this.GetResolver();
			}
			return tr;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000396C4 File Offset: 0x000386C4
		public virtual void Load(string filename)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(filename, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00039704 File Offset: 0x00038704
		public virtual void Load(Stream inStream)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(inStream, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Impl.Close(false);
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003974C File Offset: 0x0003874C
		public virtual void Load(TextReader txtReader)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(txtReader, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Impl.Close(false);
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00039794 File Offset: 0x00038794
		public virtual void Load(XmlReader reader)
		{
			try
			{
				this.IsLoading = true;
				this.actualLoadingStatus = true;
				this.RemoveAll();
				this.fEntRefNodesPresent = false;
				this.fCDataNodesPresent = false;
				this.reportValidity = true;
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.Load(this, reader, this.preserveWhitespace);
			}
			finally
			{
				this.IsLoading = false;
				this.actualLoadingStatus = false;
				this.reportValidity = true;
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00039808 File Offset: 0x00038808
		public virtual void LoadXml(string xml)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(new StringReader(xml), this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00039850 File Offset: 0x00038850
		internal Encoding TextEncoding
		{
			get
			{
				if (this.Declaration != null)
				{
					string encoding = this.Declaration.Encoding;
					if (encoding.Length > 0)
					{
						return System.Text.Encoding.GetEncoding(encoding);
					}
				}
				return null;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00039882 File Offset: 0x00038882
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0003988A File Offset: 0x0003888A
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.LoadXml(value);
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00039894 File Offset: 0x00038894
		public virtual void Save(string filename)
		{
			if (this.DocumentElement == null)
			{
				throw new XmlException("Xml_InvalidXmlDocument", Res.GetString("Xdom_NoRootEle"));
			}
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(filename, this.TextEncoding);
			try
			{
				if (!this.preserveWhitespace)
				{
					xmlDOMTextWriter.Formatting = Formatting.Indented;
				}
				this.WriteTo(xmlDOMTextWriter);
			}
			finally
			{
				xmlDOMTextWriter.Flush();
				xmlDOMTextWriter.Close();
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00039900 File Offset: 0x00038900
		public virtual void Save(Stream outStream)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(outStream, this.TextEncoding);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.WriteTo(xmlDOMTextWriter);
			xmlDOMTextWriter.Flush();
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00039938 File Offset: 0x00038938
		public virtual void Save(TextWriter writer)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(writer);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.Save(xmlDOMTextWriter);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00039964 File Offset: 0x00038964
		public virtual void Save(XmlWriter w)
		{
			XmlNode xmlNode = this.FirstChild;
			if (xmlNode == null)
			{
				return;
			}
			if (w.WriteState == WriteState.Start)
			{
				if (xmlNode is XmlDeclaration)
				{
					if (this.Standalone.Length == 0)
					{
						w.WriteStartDocument();
					}
					else if (this.Standalone == "yes")
					{
						w.WriteStartDocument(true);
					}
					else if (this.Standalone == "no")
					{
						w.WriteStartDocument(false);
					}
					xmlNode = xmlNode.NextSibling;
				}
				else
				{
					w.WriteStartDocument();
				}
			}
			while (xmlNode != null)
			{
				xmlNode.WriteTo(w);
				xmlNode = xmlNode.NextSibling;
			}
			w.Flush();
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x000399FD File Offset: 0x000389FD
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00039A08 File Offset: 0x00038A08
		public override void WriteContentTo(XmlWriter xw)
		{
			foreach (object obj in this)
			{
				XmlNode xmlNode = (XmlNode)obj;
				xmlNode.WriteTo(xw);
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00039A5C File Offset: 0x00038A5C
		public void Validate(ValidationEventHandler validationEventHandler)
		{
			this.Validate(validationEventHandler, this);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00039A68 File Offset: 0x00038A68
		public void Validate(ValidationEventHandler validationEventHandler, XmlNode nodeToValidate)
		{
			if (this.schemas == null || this.schemas.Count == 0)
			{
				throw new InvalidOperationException(Res.GetString("XmlDocument_NoSchemaInfo"));
			}
			XmlDocument document = nodeToValidate.Document;
			if (document != this)
			{
				throw new ArgumentException(Res.GetString("XmlDocument_NodeNotFromDocument", new object[]
				{
					"nodeToValidate"
				}));
			}
			if (nodeToValidate == this)
			{
				this.reportValidity = false;
			}
			DocumentSchemaValidator documentSchemaValidator = new DocumentSchemaValidator(this, this.schemas, validationEventHandler);
			documentSchemaValidator.Validate(nodeToValidate);
			if (nodeToValidate == this)
			{
				this.reportValidity = true;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000CE9 RID: 3305 RVA: 0x00039AF1 File Offset: 0x00038AF1
		// (remove) Token: 0x06000CEA RID: 3306 RVA: 0x00039B0A File Offset: 0x00038B0A
		public event XmlNodeChangedEventHandler NodeInserting
		{
			add
			{
				this.onNodeInsertingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeInsertingDelegate, value);
			}
			remove
			{
				this.onNodeInsertingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeInsertingDelegate, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000CEB RID: 3307 RVA: 0x00039B23 File Offset: 0x00038B23
		// (remove) Token: 0x06000CEC RID: 3308 RVA: 0x00039B3C File Offset: 0x00038B3C
		public event XmlNodeChangedEventHandler NodeInserted
		{
			add
			{
				this.onNodeInsertedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeInsertedDelegate, value);
			}
			remove
			{
				this.onNodeInsertedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeInsertedDelegate, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000CED RID: 3309 RVA: 0x00039B55 File Offset: 0x00038B55
		// (remove) Token: 0x06000CEE RID: 3310 RVA: 0x00039B6E File Offset: 0x00038B6E
		public event XmlNodeChangedEventHandler NodeRemoving
		{
			add
			{
				this.onNodeRemovingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeRemovingDelegate, value);
			}
			remove
			{
				this.onNodeRemovingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeRemovingDelegate, value);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000CEF RID: 3311 RVA: 0x00039B87 File Offset: 0x00038B87
		// (remove) Token: 0x06000CF0 RID: 3312 RVA: 0x00039BA0 File Offset: 0x00038BA0
		public event XmlNodeChangedEventHandler NodeRemoved
		{
			add
			{
				this.onNodeRemovedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeRemovedDelegate, value);
			}
			remove
			{
				this.onNodeRemovedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeRemovedDelegate, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000CF1 RID: 3313 RVA: 0x00039BB9 File Offset: 0x00038BB9
		// (remove) Token: 0x06000CF2 RID: 3314 RVA: 0x00039BD2 File Offset: 0x00038BD2
		public event XmlNodeChangedEventHandler NodeChanging
		{
			add
			{
				this.onNodeChangingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeChangingDelegate, value);
			}
			remove
			{
				this.onNodeChangingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeChangingDelegate, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000CF3 RID: 3315 RVA: 0x00039BEB File Offset: 0x00038BEB
		// (remove) Token: 0x06000CF4 RID: 3316 RVA: 0x00039C04 File Offset: 0x00038C04
		public event XmlNodeChangedEventHandler NodeChanged
		{
			add
			{
				this.onNodeChangedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeChangedDelegate, value);
			}
			remove
			{
				this.onNodeChangedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeChangedDelegate, value);
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00039C20 File Offset: 0x00038C20
		internal override XmlNodeChangedEventArgs GetEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.reportValidity = false;
			switch (action)
			{
			case XmlNodeChangedAction.Insert:
				if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
				{
					return null;
				}
				break;
			case XmlNodeChangedAction.Remove:
				if (this.onNodeRemovingDelegate == null && this.onNodeRemovedDelegate == null)
				{
					return null;
				}
				break;
			case XmlNodeChangedAction.Change:
				if (this.onNodeChangingDelegate == null && this.onNodeChangedDelegate == null)
				{
					return null;
				}
				break;
			}
			return new XmlNodeChangedEventArgs(node, oldParent, newParent, oldValue, newValue, action);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00039C90 File Offset: 0x00038C90
		internal XmlNodeChangedEventArgs GetInsertEventArgsForLoad(XmlNode node, XmlNode newParent)
		{
			if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
			{
				return null;
			}
			string value = node.Value;
			return new XmlNodeChangedEventArgs(node, null, newParent, value, value, XmlNodeChangedAction.Insert);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00039CC4 File Offset: 0x00038CC4
		internal override void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				switch (args.Action)
				{
				case XmlNodeChangedAction.Insert:
					if (this.onNodeInsertingDelegate != null)
					{
						this.onNodeInsertingDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Remove:
					if (this.onNodeRemovingDelegate != null)
					{
						this.onNodeRemovingDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Change:
					if (this.onNodeChangingDelegate != null)
					{
						this.onNodeChangingDelegate(this, args);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00039D30 File Offset: 0x00038D30
		internal override void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				switch (args.Action)
				{
				case XmlNodeChangedAction.Insert:
					if (this.onNodeInsertedDelegate != null)
					{
						this.onNodeInsertedDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Remove:
					if (this.onNodeRemovedDelegate != null)
					{
						this.onNodeRemovedDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Change:
					if (this.onNodeChangedDelegate != null)
					{
						this.onNodeChangedDelegate(this, args);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00039D9C File Offset: 0x00038D9C
		internal XmlAttribute GetDefaultAttribute(XmlElement elem, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator enumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)enumerator.Value;
					if ((schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed) && schemaAttDef.Name.Name == attrLocalname && ((dtdSchemaInfo.SchemaType == SchemaType.DTD && schemaAttDef.Name.Namespace == attrPrefix) || (dtdSchemaInfo.SchemaType != SchemaType.DTD && schemaAttDef.Name.Namespace == attrNamespaceURI)))
					{
						return this.PrepareDefaultAttribute(schemaAttDef, attrPrefix, attrLocalname, attrNamespaceURI);
					}
				}
			}
			return null;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00039E5C File Offset: 0x00038E5C
		internal string Version
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Version;
				}
				return null;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00039E7C File Offset: 0x00038E7C
		internal string Encoding
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Encoding;
				}
				return null;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00039E9C File Offset: 0x00038E9C
		internal string Standalone
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Standalone;
				}
				return null;
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00039EBC File Offset: 0x00038EBC
		internal XmlEntity GetEntityNode(string name)
		{
			if (this.DocumentType != null)
			{
				XmlNamedNodeMap xmlNamedNodeMap = this.DocumentType.Entities;
				if (xmlNamedNodeMap != null)
				{
					return (XmlEntity)xmlNamedNodeMap.GetNamedItem(name);
				}
			}
			return null;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00039EF0 File Offset: 0x00038EF0
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.reportValidity)
				{
					XmlElement documentElement = this.DocumentElement;
					if (documentElement != null)
					{
						switch (documentElement.SchemaInfo.Validity)
						{
						case XmlSchemaValidity.Valid:
							return XmlDocument.ValidSchemaInfo;
						case XmlSchemaValidity.Invalid:
							return XmlDocument.InvalidSchemaInfo;
						}
					}
				}
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00039F3E File Offset: 0x00038F3E
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00039F46 File Offset: 0x00038F46
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00039F50 File Offset: 0x00038F50
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_TypeConflict"));
			}
			if (!this.CanInsertAfter(newChild, this.LastChild))
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Node_Insert_Location"));
			}
			XmlNodeChangedEventArgs insertEventArgsForLoad = this.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				this.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
			}
			else
			{
				xmlLinkedNode.next = this.lastChild.next;
				this.lastChild.next = xmlLinkedNode;
			}
			this.lastChild = xmlLinkedNode;
			xmlLinkedNode.SetParentForLoad(this);
			if (insertEventArgsForLoad != null)
			{
				this.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00039FFB File Offset: 0x00038FFB
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00039FFE File Offset: 0x00038FFE
		internal bool HasEntityReferences
		{
			get
			{
				return this.fEntRefNodesPresent;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0003A008 File Offset: 0x00039008
		internal XmlAttribute NamespaceXml
		{
			get
			{
				if (this.namespaceXml == null)
				{
					this.namespaceXml = new XmlAttribute(this.AddAttrXmlName(this.strXmlns, this.strXml, this.strReservedXmlns, null), this);
					this.namespaceXml.Value = this.strReservedXml;
				}
				return this.namespaceXml;
			}
		}

		// Token: 0x040008FC RID: 2300
		private XmlImplementation implementation;

		// Token: 0x040008FD RID: 2301
		private DomNameTable domNameTable;

		// Token: 0x040008FE RID: 2302
		private XmlLinkedNode lastChild;

		// Token: 0x040008FF RID: 2303
		private XmlNamedNodeMap entities;

		// Token: 0x04000900 RID: 2304
		private Hashtable htElementIdMap;

		// Token: 0x04000901 RID: 2305
		private Hashtable htElementIDAttrDecl;

		// Token: 0x04000902 RID: 2306
		private SchemaInfo schemaInfo;

		// Token: 0x04000903 RID: 2307
		private XmlSchemaSet schemas;

		// Token: 0x04000904 RID: 2308
		private bool reportValidity;

		// Token: 0x04000905 RID: 2309
		private bool actualLoadingStatus;

		// Token: 0x04000906 RID: 2310
		private XmlNodeChangedEventHandler onNodeInsertingDelegate;

		// Token: 0x04000907 RID: 2311
		private XmlNodeChangedEventHandler onNodeInsertedDelegate;

		// Token: 0x04000908 RID: 2312
		private XmlNodeChangedEventHandler onNodeRemovingDelegate;

		// Token: 0x04000909 RID: 2313
		private XmlNodeChangedEventHandler onNodeRemovedDelegate;

		// Token: 0x0400090A RID: 2314
		private XmlNodeChangedEventHandler onNodeChangingDelegate;

		// Token: 0x0400090B RID: 2315
		private XmlNodeChangedEventHandler onNodeChangedDelegate;

		// Token: 0x0400090C RID: 2316
		internal bool fEntRefNodesPresent;

		// Token: 0x0400090D RID: 2317
		internal bool fCDataNodesPresent;

		// Token: 0x0400090E RID: 2318
		private bool preserveWhitespace;

		// Token: 0x0400090F RID: 2319
		private bool isLoading;

		// Token: 0x04000910 RID: 2320
		internal string strDocumentName;

		// Token: 0x04000911 RID: 2321
		internal string strDocumentFragmentName;

		// Token: 0x04000912 RID: 2322
		internal string strCommentName;

		// Token: 0x04000913 RID: 2323
		internal string strTextName;

		// Token: 0x04000914 RID: 2324
		internal string strCDataSectionName;

		// Token: 0x04000915 RID: 2325
		internal string strEntityName;

		// Token: 0x04000916 RID: 2326
		internal string strID;

		// Token: 0x04000917 RID: 2327
		internal string strXmlns;

		// Token: 0x04000918 RID: 2328
		internal string strXml;

		// Token: 0x04000919 RID: 2329
		internal string strSpace;

		// Token: 0x0400091A RID: 2330
		internal string strLang;

		// Token: 0x0400091B RID: 2331
		internal string strEmpty;

		// Token: 0x0400091C RID: 2332
		internal string strNonSignificantWhitespaceName;

		// Token: 0x0400091D RID: 2333
		internal string strSignificantWhitespaceName;

		// Token: 0x0400091E RID: 2334
		internal string strReservedXmlns;

		// Token: 0x0400091F RID: 2335
		internal string strReservedXml;

		// Token: 0x04000920 RID: 2336
		internal string baseURI;

		// Token: 0x04000921 RID: 2337
		private XmlResolver resolver;

		// Token: 0x04000922 RID: 2338
		internal bool bSetResolver;

		// Token: 0x04000923 RID: 2339
		internal object objLock;

		// Token: 0x04000924 RID: 2340
		private XmlAttribute namespaceXml;

		// Token: 0x04000925 RID: 2341
		internal static EmptyEnumerator EmptyEnumerator = new EmptyEnumerator();

		// Token: 0x04000926 RID: 2342
		internal static IXmlSchemaInfo NotKnownSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.NotKnown);

		// Token: 0x04000927 RID: 2343
		internal static IXmlSchemaInfo ValidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Valid);

		// Token: 0x04000928 RID: 2344
		internal static IXmlSchemaInfo InvalidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Invalid);
	}
}
