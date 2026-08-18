using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x0200026A RID: 618
	[XmlRoot("schema", Namespace = "http://www.w3.org/2001/XMLSchema")]
	[__DynamicallyInvokable]
	public class XmlSchema : XmlSchemaObject
	{
		// Token: 0x06002523 RID: 9507 RVA: 0x000CC333 File Offset: 0x000CA533
		public static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(reader), validationEventHandler);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000CC341 File Offset: 0x000CA541
		public static XmlSchema Read(Stream stream, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(stream), validationEventHandler);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000CC350 File Offset: 0x000CA550
		public static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler)
		{
			XmlNameTable xmlNameTable = reader.NameTable;
			Parser parser = new Parser(SchemaType.XSD, xmlNameTable, new SchemaNames(xmlNameTable), validationEventHandler);
			try
			{
				parser.Parse(reader, null);
			}
			catch (XmlSchemaException ex)
			{
				if (validationEventHandler != null)
				{
					validationEventHandler(null, new ValidationEventArgs(ex));
					return null;
				}
				throw ex;
			}
			return parser.XmlSchema;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000CC3B0 File Offset: 0x000CA5B0
		public void Write(Stream stream)
		{
			this.Write(stream, null);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000CC3BC File Offset: 0x000CA5BC
		public void Write(Stream stream, XmlNamespaceManager namespaceManager)
		{
			this.Write(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, namespaceManager);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000CC3E0 File Offset: 0x000CA5E0
		public void Write(TextWriter writer)
		{
			this.Write(writer, null);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000CC3EC File Offset: 0x000CA5EC
		public void Write(TextWriter writer, XmlNamespaceManager namespaceManager)
		{
			this.Write(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, namespaceManager);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000CC40F File Offset: 0x000CA60F
		public void Write(XmlWriter writer)
		{
			this.Write(writer, null);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000CC41C File Offset: 0x000CA61C
		public void Write(XmlWriter writer, XmlNamespaceManager namespaceManager)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlSchema));
			XmlSerializerNamespaces xmlSerializerNamespaces;
			if (namespaceManager != null)
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces();
				bool flag = false;
				if (base.Namespaces != null)
				{
					flag = (base.Namespaces.Namespaces["xs"] != null || base.Namespaces.Namespaces.ContainsValue("http://www.w3.org/2001/XMLSchema"));
				}
				if (!flag && namespaceManager.LookupPrefix("http://www.w3.org/2001/XMLSchema") == null && namespaceManager.LookupNamespace("xs") == null)
				{
					xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				}
				using (IEnumerator enumerator = namespaceManager.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						string text = (string)obj;
						if (text != "xml" && text != "xmlns")
						{
							xmlSerializerNamespaces.Add(text, namespaceManager.LookupNamespace(text));
						}
					}
					goto IL_17B;
				}
			}
			if (base.Namespaces != null && base.Namespaces.Count > 0)
			{
				Hashtable namespaces = base.Namespaces.Namespaces;
				if (namespaces["xs"] == null && !namespaces.ContainsValue("http://www.w3.org/2001/XMLSchema"))
				{
					namespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				}
				xmlSerializerNamespaces = base.Namespaces;
			}
			else
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces();
				xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				if (this.targetNs != null && this.targetNs.Length != 0)
				{
					xmlSerializerNamespaces.Add("tns", this.targetNs);
				}
			}
			IL_17B:
			xmlSerializer.Serialize(writer, this, xmlSerializerNamespaces);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000CC5C0 File Offset: 0x000CA7C0
		[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void Compile(ValidationEventHandler validationEventHandler)
		{
			SchemaInfo schemaInfo = new SchemaInfo();
			schemaInfo.SchemaType = SchemaType.XSD;
			this.CompileSchema(null, XmlReaderSection.CreateDefaultResolver(), schemaInfo, null, validationEventHandler, this.NameTable, false);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000CC5F4 File Offset: 0x000CA7F4
		[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void Compile(ValidationEventHandler validationEventHandler, XmlResolver resolver)
		{
			this.CompileSchema(null, resolver, new SchemaInfo
			{
				SchemaType = SchemaType.XSD
			}, null, validationEventHandler, this.NameTable, false);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000CC624 File Offset: 0x000CA824
		internal bool CompileSchema(XmlSchemaCollection xsc, XmlResolver resolver, SchemaInfo schemaInfo, string ns, ValidationEventHandler validationEventHandler, XmlNameTable nameTable, bool CompileContentModel)
		{
			bool result;
			lock (this)
			{
				if (!new SchemaCollectionPreprocessor(nameTable, null, validationEventHandler)
				{
					XmlResolver = resolver
				}.Execute(this, ns, true, xsc))
				{
					result = false;
				}
				else
				{
					SchemaCollectionCompiler schemaCollectionCompiler = new SchemaCollectionCompiler(nameTable, validationEventHandler);
					this.isCompiled = schemaCollectionCompiler.Execute(this, schemaInfo, CompileContentModel);
					this.SetIsCompiled(this.isCompiled);
					result = this.isCompiled;
				}
			}
			return result;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000CC6B0 File Offset: 0x000CA8B0
		internal void CompileSchemaInSet(XmlNameTable nameTable, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			Compiler compiler = new Compiler(nameTable, eventHandler, null, compilationSettings);
			compiler.Prepare(this, true);
			this.isCompiledBySet = compiler.Compile();
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x000CC6DB File Offset: 0x000CA8DB
		// (set) Token: 0x06002531 RID: 9521 RVA: 0x000CC6E3 File Offset: 0x000CA8E3
		[XmlAttribute("attributeFormDefault")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm AttributeFormDefault
		{
			get
			{
				return this.attributeFormDefault;
			}
			set
			{
				this.attributeFormDefault = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000CC6EC File Offset: 0x000CA8EC
		// (set) Token: 0x06002533 RID: 9523 RVA: 0x000CC6F4 File Offset: 0x000CA8F4
		[XmlAttribute("blockDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod BlockDefault
		{
			get
			{
				return this.blockDefault;
			}
			set
			{
				this.blockDefault = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000CC6FD File Offset: 0x000CA8FD
		// (set) Token: 0x06002535 RID: 9525 RVA: 0x000CC705 File Offset: 0x000CA905
		[XmlAttribute("finalDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod FinalDefault
		{
			get
			{
				return this.finalDefault;
			}
			set
			{
				this.finalDefault = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x000CC70E File Offset: 0x000CA90E
		// (set) Token: 0x06002537 RID: 9527 RVA: 0x000CC716 File Offset: 0x000CA916
		[XmlAttribute("elementFormDefault")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm ElementFormDefault
		{
			get
			{
				return this.elementFormDefault;
			}
			set
			{
				this.elementFormDefault = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000CC71F File Offset: 0x000CA91F
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x000CC727 File Offset: 0x000CA927
		[XmlAttribute("targetNamespace", DataType = "anyURI")]
		public string TargetNamespace
		{
			get
			{
				return this.targetNs;
			}
			set
			{
				this.targetNs = value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000CC730 File Offset: 0x000CA930
		// (set) Token: 0x0600253B RID: 9531 RVA: 0x000CC738 File Offset: 0x000CA938
		[XmlAttribute("version", DataType = "token")]
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000CC741 File Offset: 0x000CA941
		[XmlElement("include", typeof(XmlSchemaInclude))]
		[XmlElement("import", typeof(XmlSchemaImport))]
		[XmlElement("redefine", typeof(XmlSchemaRedefine))]
		public XmlSchemaObjectCollection Includes
		{
			get
			{
				return this.includes;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000CC749 File Offset: 0x000CA949
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		[XmlElement("notation", typeof(XmlSchemaNotation))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000CC751 File Offset: 0x000CA951
		[XmlIgnore]
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled || this.isCompiledBySet;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x000CC763 File Offset: 0x000CA963
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x000CC76B File Offset: 0x000CA96B
		[XmlIgnore]
		internal bool IsCompiledBySet
		{
			get
			{
				return this.isCompiledBySet;
			}
			set
			{
				this.isCompiledBySet = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x000CC774 File Offset: 0x000CA974
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x000CC77C File Offset: 0x000CA97C
		[XmlIgnore]
		internal bool IsPreprocessed
		{
			get
			{
				return this.isPreprocessed;
			}
			set
			{
				this.isPreprocessed = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000CC785 File Offset: 0x000CA985
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x000CC78D File Offset: 0x000CA98D
		[XmlIgnore]
		internal bool IsRedefined
		{
			get
			{
				return this.isRedefined;
			}
			set
			{
				this.isRedefined = value;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x000CC796 File Offset: 0x000CA996
		[XmlIgnore]
		public XmlSchemaObjectTable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectTable();
				}
				return this.attributes;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000CC7B1 File Offset: 0x000CA9B1
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				if (this.attributeGroups == null)
				{
					this.attributeGroups = new XmlSchemaObjectTable();
				}
				return this.attributeGroups;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000CC7CC File Offset: 0x000CA9CC
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				if (this.types == null)
				{
					this.types = new XmlSchemaObjectTable();
				}
				return this.types;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x000CC7E7 File Offset: 0x000CA9E7
		[XmlIgnore]
		public XmlSchemaObjectTable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new XmlSchemaObjectTable();
				}
				return this.elements;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000CC802 File Offset: 0x000CAA02
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x000CC80A File Offset: 0x000CAA0A
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000CC813 File Offset: 0x000CAA13
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x000CC81B File Offset: 0x000CAA1B
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000CC824 File Offset: 0x000CAA24
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000CC82C File Offset: 0x000CAA2C
		[XmlIgnore]
		public XmlSchemaObjectTable Notations
		{
			get
			{
				return this.notations;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x000CC834 File Offset: 0x000CAA34
		[XmlIgnore]
		internal XmlSchemaObjectTable IdentityConstraints
		{
			get
			{
				return this.identityConstraints;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000CC83C File Offset: 0x000CAA3C
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x000CC844 File Offset: 0x000CAA44
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x000CC84D File Offset: 0x000CAA4D
		[XmlIgnore]
		internal int SchemaId
		{
			get
			{
				if (this.schemaId == -1)
				{
					this.schemaId = Interlocked.Increment(ref XmlSchema.globalIdCounter);
				}
				return this.schemaId;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000CC86E File Offset: 0x000CAA6E
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x000CC876 File Offset: 0x000CAA76
		[XmlIgnore]
		internal bool IsChameleon
		{
			get
			{
				return this.isChameleon;
			}
			set
			{
				this.isChameleon = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000CC87F File Offset: 0x000CAA7F
		[XmlIgnore]
		internal Hashtable Ids
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000CC887 File Offset: 0x000CAA87
		[XmlIgnore]
		internal XmlDocument Document
		{
			get
			{
				if (this.document == null)
				{
					this.document = new XmlDocument();
				}
				return this.document;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000CC8A2 File Offset: 0x000CAAA2
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x000CC8AA File Offset: 0x000CAAAA
		[XmlIgnore]
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000CC8B4 File Offset: 0x000CAAB4
		internal new XmlSchema Clone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.includes = this.includes;
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.items = this.items;
			xmlSchema.BaseUri = this.BaseUri;
			SchemaCollectionCompiler.Cleanup(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000CC948 File Offset: 0x000CAB48
		internal XmlSchema DeepClone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.isPreprocessed = this.isPreprocessed;
			for (int i = 0; i < this.items.Count; i++)
			{
				XmlSchemaComplexType xmlSchemaComplexType;
				XmlSchemaObject item;
				XmlSchemaElement xmlSchemaElement;
				XmlSchemaGroup xmlSchemaGroup;
				if ((xmlSchemaComplexType = (this.items[i] as XmlSchemaComplexType)) != null)
				{
					item = xmlSchemaComplexType.Clone(this);
				}
				else if ((xmlSchemaElement = (this.items[i] as XmlSchemaElement)) != null)
				{
					item = xmlSchemaElement.Clone(this);
				}
				else if ((xmlSchemaGroup = (this.items[i] as XmlSchemaGroup)) != null)
				{
					item = xmlSchemaGroup.Clone(this);
				}
				else
				{
					item = this.items[i].Clone();
				}
				xmlSchema.Items.Add(item);
			}
			for (int j = 0; j < this.includes.Count; j++)
			{
				XmlSchemaExternal item2 = (XmlSchemaExternal)this.includes[j].Clone();
				xmlSchema.Includes.Add(item2);
			}
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.BaseUri = this.BaseUri;
			return xmlSchema;
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000CCAA5 File Offset: 0x000CACA5
		// (set) Token: 0x0600255C RID: 9564 RVA: 0x000CCAAD File Offset: 0x000CACAD
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000CCAB6 File Offset: 0x000CACB6
		internal void SetIsCompiled(bool isCompiled)
		{
			this.isCompiled = isCompiled;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000CCABF File Offset: 0x000CACBF
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000CCAC8 File Offset: 0x000CACC8
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000CCAD7 File Offset: 0x000CACD7
		internal XmlNameTable NameTable
		{
			get
			{
				if (this.nameTable == null)
				{
					this.nameTable = new NameTable();
				}
				return this.nameTable;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000CCAF2 File Offset: 0x000CACF2
		internal ArrayList ImportedSchemas
		{
			get
			{
				if (this.importedSchemas == null)
				{
					this.importedSchemas = new ArrayList();
				}
				return this.importedSchemas;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000CCB0D File Offset: 0x000CAD0D
		internal ArrayList ImportedNamespaces
		{
			get
			{
				if (this.importedNamespaces == null)
				{
					this.importedNamespaces = new ArrayList();
				}
				return this.importedNamespaces;
			}
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000CCB28 File Offset: 0x000CAD28
		internal void GetExternalSchemasList(IList extList, XmlSchema schema)
		{
			if (extList.Contains(schema))
			{
				return;
			}
			extList.Add(schema);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					this.GetExternalSchemasList(extList, xmlSchemaExternal.Schema);
				}
			}
		}

		// Token: 0x04001044 RID: 4164
		public const string Namespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x04001045 RID: 4165
		public const string InstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04001046 RID: 4166
		private XmlSchemaForm attributeFormDefault;

		// Token: 0x04001047 RID: 4167
		private XmlSchemaForm elementFormDefault;

		// Token: 0x04001048 RID: 4168
		private XmlSchemaDerivationMethod blockDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04001049 RID: 4169
		private XmlSchemaDerivationMethod finalDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x0400104A RID: 4170
		private string targetNs;

		// Token: 0x0400104B RID: 4171
		private string version;

		// Token: 0x0400104C RID: 4172
		private XmlSchemaObjectCollection includes = new XmlSchemaObjectCollection();

		// Token: 0x0400104D RID: 4173
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x0400104E RID: 4174
		private string id;

		// Token: 0x0400104F RID: 4175
		private XmlAttribute[] moreAttributes;

		// Token: 0x04001050 RID: 4176
		private bool isCompiled;

		// Token: 0x04001051 RID: 4177
		private bool isCompiledBySet;

		// Token: 0x04001052 RID: 4178
		private bool isPreprocessed;

		// Token: 0x04001053 RID: 4179
		private bool isRedefined;

		// Token: 0x04001054 RID: 4180
		private int errorCount;

		// Token: 0x04001055 RID: 4181
		private XmlSchemaObjectTable attributes;

		// Token: 0x04001056 RID: 4182
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04001057 RID: 4183
		private XmlSchemaObjectTable elements = new XmlSchemaObjectTable();

		// Token: 0x04001058 RID: 4184
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04001059 RID: 4185
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();

		// Token: 0x0400105A RID: 4186
		private XmlSchemaObjectTable notations = new XmlSchemaObjectTable();

		// Token: 0x0400105B RID: 4187
		private XmlSchemaObjectTable identityConstraints = new XmlSchemaObjectTable();

		// Token: 0x0400105C RID: 4188
		private static int globalIdCounter = -1;

		// Token: 0x0400105D RID: 4189
		private ArrayList importedSchemas;

		// Token: 0x0400105E RID: 4190
		private ArrayList importedNamespaces;

		// Token: 0x0400105F RID: 4191
		private int schemaId = -1;

		// Token: 0x04001060 RID: 4192
		private Uri baseUri;

		// Token: 0x04001061 RID: 4193
		private bool isChameleon;

		// Token: 0x04001062 RID: 4194
		private Hashtable ids = new Hashtable();

		// Token: 0x04001063 RID: 4195
		private XmlDocument document;

		// Token: 0x04001064 RID: 4196
		private XmlNameTable nameTable;
	}
}
