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
	// Token: 0x02000102 RID: 258
	public class XmlDocument : XmlNode
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x0004A21A File Offset: 0x0004841A
		public XmlDocument() : this(new XmlImplementation())
		{
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004A227 File Offset: 0x00048427
		public XmlDocument(XmlNameTable nt) : this(new XmlImplementation(nt))
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004A238 File Offset: 0x00048438
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

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004A397 File Offset: 0x00048597
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0004A39F File Offset: 0x0004859F
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

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004A3A8 File Offset: 0x000485A8
		internal static void CheckName(string name)
		{
			int num = ValidateNames.ParseNmtoken(name, 0);
			if (num < name.Length)
			{
				throw new XmlException("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(name, num));
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004A3D8 File Offset: 0x000485D8
		internal XmlName AddXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.AddName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004A3F8 File Offset: 0x000485F8
		internal XmlName GetXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.GetName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004A418 File Offset: 0x00048618
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

		// Token: 0x060011BC RID: 4540 RVA: 0x0004A496 File Offset: 0x00048696
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

		// Token: 0x060011BD RID: 4541 RVA: 0x0004A4D4 File Offset: 0x000486D4
		private XmlName GetIDInfoByElement_(XmlName eleName)
		{
			XmlName xmlName = this.GetXmlName(eleName.Prefix, eleName.LocalName, string.Empty, null);
			if (xmlName != null)
			{
				return (XmlName)this.htElementIDAttrDecl[xmlName];
			}
			return null;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004A510 File Offset: 0x00048710
		internal XmlName GetIDInfoByElement(XmlName eleName)
		{
			if (this.htElementIDAttrDecl == null)
			{
				return null;
			}
			return this.GetIDInfoByElement_(eleName);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004A524 File Offset: 0x00048724
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

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004A5F0 File Offset: 0x000487F0
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

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004A670 File Offset: 0x00048870
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

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004A6CC File Offset: 0x000488CC
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

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004A6FE File Offset: 0x000488FE
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0004A702 File Offset: 0x00048902
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004A705 File Offset: 0x00048905
		public virtual XmlDocumentType DocumentType
		{
			get
			{
				return (XmlDocumentType)this.FindChild(XmlNodeType.DocumentType);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004A714 File Offset: 0x00048914
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

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004A738 File Offset: 0x00048938
		public XmlImplementation Implementation
		{
			get
			{
				return this.implementation;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004A740 File Offset: 0x00048940
		public override string Name
		{
			get
			{
				return this.strDocumentName;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004A748 File Offset: 0x00048948
		public override string LocalName
		{
			get
			{
				return this.strDocumentName;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004A750 File Offset: 0x00048950
		public XmlElement DocumentElement
		{
			get
			{
				return (XmlElement)this.FindChild(XmlNodeType.Element);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0004A75E File Offset: 0x0004895E
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0004A761 File Offset: 0x00048961
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x0004A769 File Offset: 0x00048969
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

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0004A772 File Offset: 0x00048972
		public override XmlDocument OwnerDocument
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004A775 File Offset: 0x00048975
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x0004A796 File Offset: 0x00048996
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0004A79F File Offset: 0x0004899F
		internal bool CanReportValidity
		{
			get
			{
				return this.reportValidity;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004A7A7 File Offset: 0x000489A7
		internal bool HasSetResolver
		{
			get
			{
				return this.bSetResolver;
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004A7AF File Offset: 0x000489AF
		internal XmlResolver GetResolver()
		{
			return this.resolver;
		}

		// Token: 0x17000382 RID: 898
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x0004A7B8 File Offset: 0x000489B8
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

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004A824 File Offset: 0x00048A24
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

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004A8BC File Offset: 0x00048ABC
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

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004A900 File Offset: 0x00048B00
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

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004A928 File Offset: 0x00048B28
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
			if (nodeType <= XmlNodeType.Comment)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.ProcessingInstruction <= 1)
					{
						return refChild.NodeType != XmlNodeType.XmlDeclaration;
					}
				}
				else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
				{
					return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return refChild == this.FirstChild;
				}
			}
			else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
			{
				return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild.PreviousSibling);
			}
			return false;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004A9B4 File Offset: 0x00048BB4
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

		// Token: 0x060011DA RID: 4570 RVA: 0x0004AA28 File Offset: 0x00048C28
		public XmlAttribute CreateAttribute(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			this.SetDefaultNamespace(empty, empty2, ref empty3);
			return this.CreateAttribute(empty, empty2, empty3);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004AA64 File Offset: 0x00048C64
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

		// Token: 0x060011DC RID: 4572 RVA: 0x0004AAB4 File Offset: 0x00048CB4
		public virtual XmlCDataSection CreateCDataSection(string data)
		{
			this.fCDataNodesPresent = true;
			return new XmlCDataSection(data, this);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004AAC4 File Offset: 0x00048CC4
		public virtual XmlComment CreateComment(string data)
		{
			return new XmlComment(data, this);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004AACD File Offset: 0x00048CCD
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlDocumentType CreateDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentType(name, publicId, systemId, internalSubset, this);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004AADA File Offset: 0x00048CDA
		public virtual XmlDocumentFragment CreateDocumentFragment()
		{
			return new XmlDocumentFragment(this);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0004AAE4 File Offset: 0x00048CE4
		public XmlElement CreateElement(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			return this.CreateElement(empty, empty2, string.Empty);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004AB14 File Offset: 0x00048D14
		internal void AddDefaultAttributes(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator.Value;
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

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004ABDC File Offset: 0x00048DDC
		private SchemaElementDecl GetSchemaElementDecl(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			if (dtdSchemaInfo != null)
			{
				XmlQualifiedName key = new XmlQualifiedName(elem.LocalName, (dtdSchemaInfo.SchemaType == SchemaType.DTD) ? elem.Prefix : elem.NamespaceURI);
				SchemaElementDecl result;
				if (dtdSchemaInfo.ElementDecls.TryGetValue(key, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004AC2C File Offset: 0x00048E2C
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

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004AC6C File Offset: 0x00048E6C
		public virtual XmlEntityReference CreateEntityReference(string name)
		{
			return new XmlEntityReference(name, this);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004AC75 File Offset: 0x00048E75
		public virtual XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new XmlProcessingInstruction(target, data, this);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004AC7F File Offset: 0x00048E7F
		public virtual XmlDeclaration CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclaration(version, encoding, standalone, this);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004AC8A File Offset: 0x00048E8A
		public virtual XmlText CreateTextNode(string text)
		{
			return new XmlText(text, this);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004AC93 File Offset: 0x00048E93
		public virtual XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new XmlSignificantWhitespace(text, this);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004AC9C File Offset: 0x00048E9C
		public override XPathNavigator CreateNavigator()
		{
			return this.CreateNavigator(this);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004ACA8 File Offset: 0x00048EA8
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
							goto IL_74;
						}
						parentNode = parentNode.ParentNode;
						if (parentNode == null)
						{
							goto IL_74;
						}
					}
					return null;
				}
				IL_74:
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
							goto IL_A9;
						}
						parentNode = parentNode.ParentNode;
						if (parentNode == null)
						{
							goto IL_A9;
						}
					}
					return null;
				}
				IL_A9:
				node = this.NormalizeText(node);
				break;
			}
			}
			return new DocumentXPathNavigator(this, node);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004AD6E File Offset: 0x00048F6E
		internal static bool IsTextNode(XmlNodeType nt)
		{
			return nt - XmlNodeType.Text <= 1 || nt - XmlNodeType.Whitespace <= 1;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0004AD80 File Offset: 0x00048F80
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

		// Token: 0x060011ED RID: 4589 RVA: 0x0004AE00 File Offset: 0x00049000
		public virtual XmlWhitespace CreateWhitespace(string text)
		{
			return new XmlWhitespace(text, this);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0004AE09 File Offset: 0x00049009
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004AE14 File Offset: 0x00049014
		public XmlAttribute CreateAttribute(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateAttribute(empty, empty2, namespaceURI);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0004AE40 File Offset: 0x00049040
		public XmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateElement(empty, empty2, namespaceURI);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0004AE6C File Offset: 0x0004906C
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004AE78 File Offset: 0x00049078
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

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004AF08 File Offset: 0x00049108
		public virtual XmlNode ImportNode(XmlNode node, bool deep)
		{
			return this.ImportNodeInternal(node, deep);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004AF14 File Offset: 0x00049114
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

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004B100 File Offset: 0x00049300
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

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004B158 File Offset: 0x00049358
		private void ImportChildren(XmlNode fromNode, XmlNode toNode, bool deep)
		{
			for (XmlNode xmlNode = fromNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				toNode.AppendChild(this.ImportNodeInternal(xmlNode, deep));
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004B187 File Offset: 0x00049387
		public XmlNameTable NameTable
		{
			get
			{
				return this.implementation.NameTable;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004B194 File Offset: 0x00049394
		public virtual XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlAttribute(this.AddAttrXmlName(prefix, localName, namespaceURI, null), this);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004B1A6 File Offset: 0x000493A6
		protected internal virtual XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlUnspecifiedAttribute(prefix, localName, namespaceURI, this);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004B1B4 File Offset: 0x000493B4
		public virtual XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			XmlElement xmlElement = new XmlElement(this.AddXmlName(prefix, localName, namespaceURI, null), true, this);
			if (!this.IsLoading)
			{
				this.AddDefaultAttributes(xmlElement);
			}
			return xmlElement;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0004B1E3 File Offset: 0x000493E3
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x0004B1EB File Offset: 0x000493EB
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004B1F4 File Offset: 0x000493F4
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004B1F7 File Offset: 0x000493F7
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0004B213 File Offset: 0x00049413
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004B21C File Offset: 0x0004941C
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x0004B224 File Offset: 0x00049424
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

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004B22D File Offset: 0x0004942D
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0004B235 File Offset: 0x00049435
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

		// Token: 0x06001204 RID: 4612 RVA: 0x0004B240 File Offset: 0x00049440
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

		// Token: 0x06001205 RID: 4613 RVA: 0x0004B36F File Offset: 0x0004956F
		public virtual XmlNode CreateNode(string nodeTypeString, string name, string namespaceURI)
		{
			return this.CreateNode(this.ConvertToNodeType(nodeTypeString), name, namespaceURI);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004B380 File Offset: 0x00049580
		public virtual XmlNode CreateNode(XmlNodeType type, string name, string namespaceURI)
		{
			return this.CreateNode(type, null, name, namespaceURI);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004B38C File Offset: 0x0004958C
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

		// Token: 0x06001208 RID: 4616 RVA: 0x0004B3CC File Offset: 0x000495CC
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

		// Token: 0x06001209 RID: 4617 RVA: 0x0004B4CA File Offset: 0x000496CA
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

		// Token: 0x0600120A RID: 4618 RVA: 0x0004B4F0 File Offset: 0x000496F0
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

		// Token: 0x0600120B RID: 4619 RVA: 0x0004B530 File Offset: 0x00049730
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

		// Token: 0x0600120C RID: 4620 RVA: 0x0004B578 File Offset: 0x00049778
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

		// Token: 0x0600120D RID: 4621 RVA: 0x0004B5C0 File Offset: 0x000497C0
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

		// Token: 0x0600120E RID: 4622 RVA: 0x0004B634 File Offset: 0x00049834
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0004B67C File Offset: 0x0004987C
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

		// Token: 0x1700038A RID: 906
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x0004B6AE File Offset: 0x000498AE
		public override string InnerText
		{
			set
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Document_Innertext"));
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004B6BF File Offset: 0x000498BF
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x0004B6C7 File Offset: 0x000498C7
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

		// Token: 0x06001213 RID: 4627 RVA: 0x0004B6D0 File Offset: 0x000498D0
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
				xmlDOMTextWriter.Flush();
			}
			finally
			{
				xmlDOMTextWriter.Close();
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004B73C File Offset: 0x0004993C
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

		// Token: 0x06001215 RID: 4629 RVA: 0x0004B774 File Offset: 0x00049974
		public virtual void Save(TextWriter writer)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(writer);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.Save(xmlDOMTextWriter);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004B7A0 File Offset: 0x000499A0
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

		// Token: 0x06001217 RID: 4631 RVA: 0x0004B839 File Offset: 0x00049A39
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004B844 File Offset: 0x00049A44
		public override void WriteContentTo(XmlWriter xw)
		{
			foreach (object obj in this)
			{
				XmlNode xmlNode = (XmlNode)obj;
				xmlNode.WriteTo(xw);
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0004B898 File Offset: 0x00049A98
		public void Validate(ValidationEventHandler validationEventHandler)
		{
			this.Validate(validationEventHandler, this);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004B8A4 File Offset: 0x00049AA4
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
		// (add) Token: 0x0600121B RID: 4635 RVA: 0x0004B92B File Offset: 0x00049B2B
		// (remove) Token: 0x0600121C RID: 4636 RVA: 0x0004B944 File Offset: 0x00049B44
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
		// (add) Token: 0x0600121D RID: 4637 RVA: 0x0004B95D File Offset: 0x00049B5D
		// (remove) Token: 0x0600121E RID: 4638 RVA: 0x0004B976 File Offset: 0x00049B76
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
		// (add) Token: 0x0600121F RID: 4639 RVA: 0x0004B98F File Offset: 0x00049B8F
		// (remove) Token: 0x06001220 RID: 4640 RVA: 0x0004B9A8 File Offset: 0x00049BA8
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
		// (add) Token: 0x06001221 RID: 4641 RVA: 0x0004B9C1 File Offset: 0x00049BC1
		// (remove) Token: 0x06001222 RID: 4642 RVA: 0x0004B9DA File Offset: 0x00049BDA
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
		// (add) Token: 0x06001223 RID: 4643 RVA: 0x0004B9F3 File Offset: 0x00049BF3
		// (remove) Token: 0x06001224 RID: 4644 RVA: 0x0004BA0C File Offset: 0x00049C0C
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
		// (add) Token: 0x06001225 RID: 4645 RVA: 0x0004BA25 File Offset: 0x00049C25
		// (remove) Token: 0x06001226 RID: 4646 RVA: 0x0004BA3E File Offset: 0x00049C3E
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

		// Token: 0x06001227 RID: 4647 RVA: 0x0004BA58 File Offset: 0x00049C58
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

		// Token: 0x06001228 RID: 4648 RVA: 0x0004BAC8 File Offset: 0x00049CC8
		internal XmlNodeChangedEventArgs GetInsertEventArgsForLoad(XmlNode node, XmlNode newParent)
		{
			if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
			{
				return null;
			}
			string value = node.Value;
			return new XmlNodeChangedEventArgs(node, null, newParent, value, value, XmlNodeChangedAction.Insert);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0004BAFC File Offset: 0x00049CFC
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

		// Token: 0x0600122A RID: 4650 RVA: 0x0004BB68 File Offset: 0x00049D68
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

		// Token: 0x0600122B RID: 4651 RVA: 0x0004BBD4 File Offset: 0x00049DD4
		internal XmlAttribute GetDefaultAttribute(XmlElement elem, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator.Value;
					if ((schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed) && schemaAttDef.Name.Name == attrLocalname && ((dtdSchemaInfo.SchemaType == SchemaType.DTD && schemaAttDef.Name.Namespace == attrPrefix) || (dtdSchemaInfo.SchemaType != SchemaType.DTD && schemaAttDef.Name.Namespace == attrNamespaceURI)))
					{
						return this.PrepareDefaultAttribute(schemaAttDef, attrPrefix, attrLocalname, attrNamespaceURI);
					}
				}
			}
			return null;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004BC9C File Offset: 0x00049E9C
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004BCBC File Offset: 0x00049EBC
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004BCDC File Offset: 0x00049EDC
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

		// Token: 0x0600122F RID: 4655 RVA: 0x0004BCFC File Offset: 0x00049EFC
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

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004BD30 File Offset: 0x00049F30
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.reportValidity)
				{
					XmlElement documentElement = this.DocumentElement;
					if (documentElement != null)
					{
						XmlSchemaValidity validity = documentElement.SchemaInfo.Validity;
						if (validity == XmlSchemaValidity.Valid)
						{
							return XmlDocument.ValidSchemaInfo;
						}
						if (validity == XmlSchemaValidity.Invalid)
						{
							return XmlDocument.InvalidSchemaInfo;
						}
					}
				}
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004BD76 File Offset: 0x00049F76
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0004BD7E File Offset: 0x00049F7E
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004BD88 File Offset: 0x00049F88
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

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004BE33 File Offset: 0x0004A033
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004BE36 File Offset: 0x0004A036
		internal bool HasEntityReferences
		{
			get
			{
				return this.fEntRefNodesPresent;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004BE40 File Offset: 0x0004A040
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

		// Token: 0x040004DB RID: 1243
		private XmlImplementation implementation;

		// Token: 0x040004DC RID: 1244
		private DomNameTable domNameTable;

		// Token: 0x040004DD RID: 1245
		private XmlLinkedNode lastChild;

		// Token: 0x040004DE RID: 1246
		private XmlNamedNodeMap entities;

		// Token: 0x040004DF RID: 1247
		private Hashtable htElementIdMap;

		// Token: 0x040004E0 RID: 1248
		private Hashtable htElementIDAttrDecl;

		// Token: 0x040004E1 RID: 1249
		private SchemaInfo schemaInfo;

		// Token: 0x040004E2 RID: 1250
		private XmlSchemaSet schemas;

		// Token: 0x040004E3 RID: 1251
		private bool reportValidity;

		// Token: 0x040004E4 RID: 1252
		private bool actualLoadingStatus;

		// Token: 0x040004E5 RID: 1253
		private XmlNodeChangedEventHandler onNodeInsertingDelegate;

		// Token: 0x040004E6 RID: 1254
		private XmlNodeChangedEventHandler onNodeInsertedDelegate;

		// Token: 0x040004E7 RID: 1255
		private XmlNodeChangedEventHandler onNodeRemovingDelegate;

		// Token: 0x040004E8 RID: 1256
		private XmlNodeChangedEventHandler onNodeRemovedDelegate;

		// Token: 0x040004E9 RID: 1257
		private XmlNodeChangedEventHandler onNodeChangingDelegate;

		// Token: 0x040004EA RID: 1258
		private XmlNodeChangedEventHandler onNodeChangedDelegate;

		// Token: 0x040004EB RID: 1259
		internal bool fEntRefNodesPresent;

		// Token: 0x040004EC RID: 1260
		internal bool fCDataNodesPresent;

		// Token: 0x040004ED RID: 1261
		private bool preserveWhitespace;

		// Token: 0x040004EE RID: 1262
		private bool isLoading;

		// Token: 0x040004EF RID: 1263
		internal string strDocumentName;

		// Token: 0x040004F0 RID: 1264
		internal string strDocumentFragmentName;

		// Token: 0x040004F1 RID: 1265
		internal string strCommentName;

		// Token: 0x040004F2 RID: 1266
		internal string strTextName;

		// Token: 0x040004F3 RID: 1267
		internal string strCDataSectionName;

		// Token: 0x040004F4 RID: 1268
		internal string strEntityName;

		// Token: 0x040004F5 RID: 1269
		internal string strID;

		// Token: 0x040004F6 RID: 1270
		internal string strXmlns;

		// Token: 0x040004F7 RID: 1271
		internal string strXml;

		// Token: 0x040004F8 RID: 1272
		internal string strSpace;

		// Token: 0x040004F9 RID: 1273
		internal string strLang;

		// Token: 0x040004FA RID: 1274
		internal string strEmpty;

		// Token: 0x040004FB RID: 1275
		internal string strNonSignificantWhitespaceName;

		// Token: 0x040004FC RID: 1276
		internal string strSignificantWhitespaceName;

		// Token: 0x040004FD RID: 1277
		internal string strReservedXmlns;

		// Token: 0x040004FE RID: 1278
		internal string strReservedXml;

		// Token: 0x040004FF RID: 1279
		internal string baseURI;

		// Token: 0x04000500 RID: 1280
		private XmlResolver resolver;

		// Token: 0x04000501 RID: 1281
		internal bool bSetResolver;

		// Token: 0x04000502 RID: 1282
		internal object objLock;

		// Token: 0x04000503 RID: 1283
		private XmlAttribute namespaceXml;

		// Token: 0x04000504 RID: 1284
		internal static EmptyEnumerator EmptyEnumerator = new EmptyEnumerator();

		// Token: 0x04000505 RID: 1285
		internal static IXmlSchemaInfo NotKnownSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.NotKnown);

		// Token: 0x04000506 RID: 1286
		internal static IXmlSchemaInfo ValidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Valid);

		// Token: 0x04000507 RID: 1287
		internal static IXmlSchemaInfo InvalidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Invalid);
	}
}
