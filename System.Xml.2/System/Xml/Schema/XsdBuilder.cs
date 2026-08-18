using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020002CE RID: 718
	internal sealed class XsdBuilder : SchemaBuilder
	{
		// Token: 0x06002A6E RID: 10862 RVA: 0x000DC878 File Offset: 0x000DAA78
		internal XsdBuilder(XmlReader reader, XmlNamespaceManager curmgr, XmlSchema schema, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this.reader = reader;
			this.schema = schema;
			this.xso = schema;
			this.namespaceManager = new XsdBuilder.BuilderNamespaceManager(curmgr, reader);
			this.validationEventHandler = eventhandler;
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.stateHistory = new HWStack(10);
			this.currentEntry = XsdBuilder.SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000DC910 File Offset: 0x000DAB10
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				this.xso = null;
				this.currentEntry.InitFunc(this, null);
				this.RecordPosition();
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("Sch_UnsupportedElement", xmlQualifiedName.ToString());
			}
			return false;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000DC974 File Offset: 0x000DAB74
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.currentEntry.Attributes != null)
			{
				for (int i = 0; i < this.currentEntry.Attributes.Length; i++)
				{
					XsdBuilder.XsdAttributeEntry xsdAttributeEntry = this.currentEntry.Attributes[i];
					if (this.schemaNames.TokenToQName[(int)xsdAttributeEntry.Attribute].Equals(xmlQualifiedName))
					{
						try
						{
							xsdAttributeEntry.BuildFunc(this, value);
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
							this.SendValidationEvent("Sch_InvalidXsdAttributeDatatypeValue", new string[]
							{
								name,
								ex.Message
							}, XmlSeverityType.Error);
						}
						return;
					}
				}
			}
			if (!(ns != this.schemaNames.NsXs) || ns.Length == 0)
			{
				this.SendValidationEvent("Sch_UnsupportedAttribute", xmlQualifiedName.ToString());
				return;
			}
			if (ns == this.schemaNames.NsXmlNs)
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				this.namespaces.Add((name == this.schemaNames.QnXmlNs.Name) ? string.Empty : name, value);
				return;
			}
			XmlAttribute xmlAttribute = new XmlAttribute(prefix, name, ns, this.schema.Document);
			xmlAttribute.Value = value;
			this.unhandledAttributes.Add(xmlAttribute);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000DCAFC File Offset: 0x000DACFC
		internal override bool IsContentParsed()
		{
			return this.currentEntry.ParseContent;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000DCB09 File Offset: 0x000DAD09
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			this.markup = markup;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000DCB12 File Offset: 0x000DAD12
		internal override void ProcessCData(string value)
		{
			this.SendValidationEvent("Sch_TextNotAllowed", value);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000DCB20 File Offset: 0x000DAD20
		internal override void StartChildren()
		{
			if (this.xso != null)
			{
				if (this.namespaces != null && this.namespaces.Count > 0)
				{
					this.xso.Namespaces.Namespaces = this.namespaces;
					this.namespaces = null;
				}
				if (this.unhandledAttributes.Count != 0)
				{
					this.xso.SetUnhandledAttributes((XmlAttribute[])this.unhandledAttributes.ToArray(typeof(XmlAttribute)));
					this.unhandledAttributes.Clear();
				}
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000DCBA5 File Offset: 0x000DADA5
		internal override void EndChildren()
		{
			if (this.currentEntry.EndChildFunc != null)
			{
				this.currentEntry.EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000DCBCC File Offset: 0x000DADCC
		private void Push()
		{
			this.stateHistory.Push();
			this.stateHistory[this.stateHistory.Length - 1] = this.currentEntry;
			this.containerStack.Push(this.GetContainer(this.currentEntry.CurrentState));
			this.currentEntry = this.nextEntry;
			if (this.currentEntry.Name != SchemaNames.Token.XsdAnnotation)
			{
				this.hasChild = false;
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000DCC41 File Offset: 0x000DAE41
		private void Pop()
		{
			this.currentEntry = (XsdBuilder.XsdEntry)this.stateHistory.Pop();
			this.SetContainer(this.currentEntry.CurrentState, this.containerStack.Pop());
			this.hasChild = true;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000DCC7C File Offset: 0x000DAE7C
		private SchemaNames.Token CurrentElement
		{
			get
			{
				return this.currentEntry.Name;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x000DCC89 File Offset: 0x000DAE89
		private SchemaNames.Token ParentElement
		{
			get
			{
				return ((XsdBuilder.XsdEntry)this.stateHistory[this.stateHistory.Length - 1]).Name;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000DCCAD File Offset: 0x000DAEAD
		private XmlSchemaObject ParentContainer
		{
			get
			{
				return (XmlSchemaObject)this.containerStack.Peek();
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000DCCC0 File Offset: 0x000DAEC0
		private XmlSchemaObject GetContainer(XsdBuilder.State state)
		{
			XmlSchemaObject result = null;
			switch (state)
			{
			case XsdBuilder.State.Schema:
				result = this.schema;
				break;
			case XsdBuilder.State.Annotation:
				result = this.annotation;
				break;
			case XsdBuilder.State.Include:
				result = this.include;
				break;
			case XsdBuilder.State.Import:
				result = this.import;
				break;
			case XsdBuilder.State.Element:
				result = this.element;
				break;
			case XsdBuilder.State.Attribute:
				result = this.attribute;
				break;
			case XsdBuilder.State.AttributeGroup:
				result = this.attributeGroup;
				break;
			case XsdBuilder.State.AttributeGroupRef:
				result = this.attributeGroupRef;
				break;
			case XsdBuilder.State.AnyAttribute:
				result = this.anyAttribute;
				break;
			case XsdBuilder.State.Group:
				result = this.group;
				break;
			case XsdBuilder.State.GroupRef:
				result = this.groupRef;
				break;
			case XsdBuilder.State.All:
				result = this.all;
				break;
			case XsdBuilder.State.Choice:
				result = this.choice;
				break;
			case XsdBuilder.State.Sequence:
				result = this.sequence;
				break;
			case XsdBuilder.State.Any:
				result = this.anyElement;
				break;
			case XsdBuilder.State.Notation:
				result = this.notation;
				break;
			case XsdBuilder.State.SimpleType:
				result = this.simpleType;
				break;
			case XsdBuilder.State.ComplexType:
				result = this.complexType;
				break;
			case XsdBuilder.State.ComplexContent:
				result = this.complexContent;
				break;
			case XsdBuilder.State.ComplexContentRestriction:
				result = this.complexContentRestriction;
				break;
			case XsdBuilder.State.ComplexContentExtension:
				result = this.complexContentExtension;
				break;
			case XsdBuilder.State.SimpleContent:
				result = this.simpleContent;
				break;
			case XsdBuilder.State.SimpleContentExtension:
				result = this.simpleContentExtension;
				break;
			case XsdBuilder.State.SimpleContentRestriction:
				result = this.simpleContentRestriction;
				break;
			case XsdBuilder.State.SimpleTypeUnion:
				result = this.simpleTypeUnion;
				break;
			case XsdBuilder.State.SimpleTypeList:
				result = this.simpleTypeList;
				break;
			case XsdBuilder.State.SimpleTypeRestriction:
				result = this.simpleTypeRestriction;
				break;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				result = this.identityConstraint;
				break;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				result = this.xpath;
				break;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				result = this.facet;
				break;
			case XsdBuilder.State.AppInfo:
				result = this.appInfo;
				break;
			case XsdBuilder.State.Documentation:
				result = this.documentation;
				break;
			case XsdBuilder.State.Redefine:
				result = this.redefine;
				break;
			}
			return result;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000DCEF8 File Offset: 0x000DB0F8
		private void SetContainer(XsdBuilder.State state, object container)
		{
			switch (state)
			{
			case XsdBuilder.State.Root:
			case XsdBuilder.State.Schema:
				break;
			case XsdBuilder.State.Annotation:
				this.annotation = (XmlSchemaAnnotation)container;
				return;
			case XsdBuilder.State.Include:
				this.include = (XmlSchemaInclude)container;
				return;
			case XsdBuilder.State.Import:
				this.import = (XmlSchemaImport)container;
				return;
			case XsdBuilder.State.Element:
				this.element = (XmlSchemaElement)container;
				return;
			case XsdBuilder.State.Attribute:
				this.attribute = (XmlSchemaAttribute)container;
				return;
			case XsdBuilder.State.AttributeGroup:
				this.attributeGroup = (XmlSchemaAttributeGroup)container;
				return;
			case XsdBuilder.State.AttributeGroupRef:
				this.attributeGroupRef = (XmlSchemaAttributeGroupRef)container;
				return;
			case XsdBuilder.State.AnyAttribute:
				this.anyAttribute = (XmlSchemaAnyAttribute)container;
				return;
			case XsdBuilder.State.Group:
				this.group = (XmlSchemaGroup)container;
				return;
			case XsdBuilder.State.GroupRef:
				this.groupRef = (XmlSchemaGroupRef)container;
				return;
			case XsdBuilder.State.All:
				this.all = (XmlSchemaAll)container;
				return;
			case XsdBuilder.State.Choice:
				this.choice = (XmlSchemaChoice)container;
				return;
			case XsdBuilder.State.Sequence:
				this.sequence = (XmlSchemaSequence)container;
				return;
			case XsdBuilder.State.Any:
				this.anyElement = (XmlSchemaAny)container;
				return;
			case XsdBuilder.State.Notation:
				this.notation = (XmlSchemaNotation)container;
				return;
			case XsdBuilder.State.SimpleType:
				this.simpleType = (XmlSchemaSimpleType)container;
				return;
			case XsdBuilder.State.ComplexType:
				this.complexType = (XmlSchemaComplexType)container;
				return;
			case XsdBuilder.State.ComplexContent:
				this.complexContent = (XmlSchemaComplexContent)container;
				return;
			case XsdBuilder.State.ComplexContentRestriction:
				this.complexContentRestriction = (XmlSchemaComplexContentRestriction)container;
				return;
			case XsdBuilder.State.ComplexContentExtension:
				this.complexContentExtension = (XmlSchemaComplexContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContent:
				this.simpleContent = (XmlSchemaSimpleContent)container;
				return;
			case XsdBuilder.State.SimpleContentExtension:
				this.simpleContentExtension = (XmlSchemaSimpleContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContentRestriction:
				this.simpleContentRestriction = (XmlSchemaSimpleContentRestriction)container;
				return;
			case XsdBuilder.State.SimpleTypeUnion:
				this.simpleTypeUnion = (XmlSchemaSimpleTypeUnion)container;
				return;
			case XsdBuilder.State.SimpleTypeList:
				this.simpleTypeList = (XmlSchemaSimpleTypeList)container;
				return;
			case XsdBuilder.State.SimpleTypeRestriction:
				this.simpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)container;
				return;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				this.identityConstraint = (XmlSchemaIdentityConstraint)container;
				return;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				this.xpath = (XmlSchemaXPath)container;
				return;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				this.facet = (XmlSchemaFacet)container;
				return;
			case XsdBuilder.State.AppInfo:
				this.appInfo = (XmlSchemaAppInfo)container;
				return;
			case XsdBuilder.State.Documentation:
				this.documentation = (XmlSchemaDocumentation)container;
				return;
			case XsdBuilder.State.Redefine:
				this.redefine = (XmlSchemaRedefine)container;
				break;
			default:
				return;
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000DD16B File Offset: 0x000DB36B
		private static void BuildAnnotated_Id(XsdBuilder builder, string value)
		{
			builder.xso.IdAttribute = value;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000DD179 File Offset: 0x000DB379
		private static void BuildSchema_AttributeFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.AttributeFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "attributeFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000DD197 File Offset: 0x000DB397
		private static void BuildSchema_ElementFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.ElementFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "elementFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000DD1B5 File Offset: 0x000DB3B5
		private static void BuildSchema_TargetNamespace(XsdBuilder builder, string value)
		{
			builder.schema.TargetNamespace = value;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000DD1C3 File Offset: 0x000DB3C3
		private static void BuildSchema_Version(XsdBuilder builder, string value)
		{
			builder.schema.Version = value;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000DD1D1 File Offset: 0x000DB3D1
		private static void BuildSchema_FinalDefault(XsdBuilder builder, string value)
		{
			builder.schema.FinalDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "finalDefault");
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000DD1EA File Offset: 0x000DB3EA
		private static void BuildSchema_BlockDefault(XsdBuilder builder, string value)
		{
			builder.schema.BlockDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "blockDefault");
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000DD203 File Offset: 0x000DB403
		private static void InitSchema(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = true;
			builder.xso = builder.schema;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000DD218 File Offset: 0x000DB418
		private static void InitInclude(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("Sch_IncludeLocation", null);
			}
			builder.xso = (builder.include = new XmlSchemaInclude());
			builder.schema.Includes.Add(builder.include);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000DD264 File Offset: 0x000DB464
		private static void BuildInclude_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.include.SchemaLocation = value;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000DD274 File Offset: 0x000DB474
		private static void InitImport(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("Sch_ImportLocation", null);
			}
			builder.xso = (builder.import = new XmlSchemaImport());
			builder.schema.Includes.Add(builder.import);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000DD2C0 File Offset: 0x000DB4C0
		private static void BuildImport_Namespace(XsdBuilder builder, string value)
		{
			builder.import.Namespace = value;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000DD2CE File Offset: 0x000DB4CE
		private static void BuildImport_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.import.SchemaLocation = value;
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000DD2DC File Offset: 0x000DB4DC
		private static void InitRedefine(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("Sch_RedefineLocation", null);
			}
			builder.xso = (builder.redefine = new XmlSchemaRedefine());
			builder.schema.Includes.Add(builder.redefine);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000DD328 File Offset: 0x000DB528
		private static void BuildRedefine_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.redefine.SchemaLocation = value;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000DD336 File Offset: 0x000DB536
		private static void EndRedefine(XsdBuilder builder)
		{
			builder.canIncludeImport = true;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000DD340 File Offset: 0x000DB540
		private static void InitAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attribute = new XmlSchemaAttribute());
			if (builder.ParentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attribute);
			}
			else
			{
				builder.AddAttribute(builder.attribute);
			}
			builder.canIncludeImport = false;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000DD397 File Offset: 0x000DB597
		private static void BuildAttribute_Default(XsdBuilder builder, string value)
		{
			builder.attribute.DefaultValue = value;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000DD3A5 File Offset: 0x000DB5A5
		private static void BuildAttribute_Fixed(XsdBuilder builder, string value)
		{
			builder.attribute.FixedValue = value;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000DD3B3 File Offset: 0x000DB5B3
		private static void BuildAttribute_Form(XsdBuilder builder, string value)
		{
			builder.attribute.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000DD3D1 File Offset: 0x000DB5D1
		private static void BuildAttribute_Use(XsdBuilder builder, string value)
		{
			builder.attribute.Use = (XmlSchemaUse)builder.ParseEnum(value, "use", XsdBuilder.UseStringValues);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000DD3EF File Offset: 0x000DB5EF
		private static void BuildAttribute_Ref(XsdBuilder builder, string value)
		{
			builder.attribute.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000DD408 File Offset: 0x000DB608
		private static void BuildAttribute_Name(XsdBuilder builder, string value)
		{
			builder.attribute.Name = value;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000DD416 File Offset: 0x000DB616
		private static void BuildAttribute_Type(XsdBuilder builder, string value)
		{
			builder.attribute.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000DD430 File Offset: 0x000DB630
		private static void InitElement(XsdBuilder builder, string value)
		{
			builder.xso = (builder.element = new XmlSchemaElement());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.element);
				return;
			}
			switch (parentElement)
			{
			case SchemaNames.Token.XsdAll:
				builder.all.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdChoice:
				builder.choice.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdSequence:
				builder.sequence.Items.Add(builder.element);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000DD4D9 File Offset: 0x000DB6D9
		private static void BuildElement_Abstract(XsdBuilder builder, string value)
		{
			builder.element.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000DD4F2 File Offset: 0x000DB6F2
		private static void BuildElement_Block(XsdBuilder builder, string value)
		{
			builder.element.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000DD50B File Offset: 0x000DB70B
		private static void BuildElement_Default(XsdBuilder builder, string value)
		{
			builder.element.DefaultValue = value;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000DD519 File Offset: 0x000DB719
		private static void BuildElement_Form(XsdBuilder builder, string value)
		{
			builder.element.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000DD537 File Offset: 0x000DB737
		private static void BuildElement_SubstitutionGroup(XsdBuilder builder, string value)
		{
			builder.element.SubstitutionGroup = builder.ParseQName(value, "substitutionGroup");
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000DD550 File Offset: 0x000DB750
		private static void BuildElement_Final(XsdBuilder builder, string value)
		{
			builder.element.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000DD569 File Offset: 0x000DB769
		private static void BuildElement_Fixed(XsdBuilder builder, string value)
		{
			builder.element.FixedValue = value;
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000DD577 File Offset: 0x000DB777
		private static void BuildElement_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.element, value);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000DD586 File Offset: 0x000DB786
		private static void BuildElement_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.element, value);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000DD595 File Offset: 0x000DB795
		private static void BuildElement_Name(XsdBuilder builder, string value)
		{
			builder.element.Name = value;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000DD5A3 File Offset: 0x000DB7A3
		private static void BuildElement_Nillable(XsdBuilder builder, string value)
		{
			builder.element.IsNillable = builder.ParseBoolean(value, "nillable");
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000DD5BC File Offset: 0x000DB7BC
		private static void BuildElement_Ref(XsdBuilder builder, string value)
		{
			builder.element.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000DD5D5 File Offset: 0x000DB7D5
		private static void BuildElement_Type(XsdBuilder builder, string value)
		{
			builder.element.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000DD5F0 File Offset: 0x000DB7F0
		private static void InitSimpleType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.simpleType = new XmlSchemaSimpleType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdElement)
			{
				if (parentElement == SchemaNames.Token.XsdSchema)
				{
					builder.canIncludeImport = false;
					builder.schema.Items.Add(builder.simpleType);
					return;
				}
				if (parentElement != SchemaNames.Token.XsdElement)
				{
					return;
				}
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("Sch_DupXsdElement", "simpleType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("Sch_TypeAfterConstraints", null);
				}
				builder.element.SchemaType = builder.simpleType;
				return;
			}
			else
			{
				if (parentElement != SchemaNames.Token.XsdAttribute)
				{
					switch (parentElement)
					{
					case SchemaNames.Token.XsdSimpleContentRestriction:
						if (builder.simpleContentRestriction.BaseType != null)
						{
							builder.SendValidationEvent("Sch_DupXsdElement", "simpleType");
						}
						if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null || builder.simpleContentRestriction.Facets.Count != 0)
						{
							builder.SendValidationEvent("Sch_SimpleTypeRestriction", null);
						}
						builder.simpleContentRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeList:
						if (builder.simpleTypeList.ItemType != null)
						{
							builder.SendValidationEvent("Sch_DupXsdElement", "simpleType");
						}
						builder.simpleTypeList.ItemType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeRestriction:
						if (builder.simpleTypeRestriction.BaseType != null)
						{
							builder.SendValidationEvent("Sch_DupXsdElement", "simpleType");
						}
						builder.simpleTypeRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeUnion:
						builder.simpleTypeUnion.BaseTypes.Add(builder.simpleType);
						break;
					case SchemaNames.Token.XsdWhitespace:
						break;
					case SchemaNames.Token.XsdRedefine:
						builder.redefine.Items.Add(builder.simpleType);
						return;
					default:
						return;
					}
					return;
				}
				if (builder.attribute.SchemaType != null)
				{
					builder.SendValidationEvent("Sch_DupXsdElement", "simpleType");
				}
				builder.attribute.SchemaType = builder.simpleType;
				return;
			}
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000DD7E6 File Offset: 0x000DB9E6
		private static void BuildSimpleType_Name(XsdBuilder builder, string value)
		{
			builder.simpleType.Name = value;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000DD7F4 File Offset: 0x000DB9F4
		private static void BuildSimpleType_Final(XsdBuilder builder, string value)
		{
			builder.simpleType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000DD810 File Offset: 0x000DBA10
		private static void InitSimpleTypeUnion(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("Sch_DupSimpleTypeChild", null);
			}
			builder.xso = (builder.simpleTypeUnion = new XmlSchemaSimpleTypeUnion());
			builder.simpleType.Content = builder.simpleTypeUnion;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000DD85C File Offset: 0x000DBA5C
		private static void BuildSimpleTypeUnion_MemberTypes(XsdBuilder builder, string value)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName).DeriveByList(null);
			try
			{
				builder.simpleTypeUnion.MemberTypes = (XmlQualifiedName[])xmlSchemaDatatype.ParseValue(value, builder.nameTable, builder.namespaceManager);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000DD8E0 File Offset: 0x000DBAE0
		private static void InitSimpleTypeList(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("Sch_DupSimpleTypeChild", null);
			}
			builder.xso = (builder.simpleTypeList = new XmlSchemaSimpleTypeList());
			builder.simpleType.Content = builder.simpleTypeList;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000DD92B File Offset: 0x000DBB2B
		private static void BuildSimpleTypeList_ItemType(XsdBuilder builder, string value)
		{
			builder.simpleTypeList.ItemTypeName = builder.ParseQName(value, "itemType");
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000DD944 File Offset: 0x000DBB44
		private static void InitSimpleTypeRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("Sch_DupSimpleTypeChild", null);
			}
			builder.xso = (builder.simpleTypeRestriction = new XmlSchemaSimpleTypeRestriction());
			builder.simpleType.Content = builder.simpleTypeRestriction;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000DD98F File Offset: 0x000DBB8F
		private static void BuildSimpleTypeRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleTypeRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000DD9A8 File Offset: 0x000DBBA8
		private static void InitComplexType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexType = new XmlSchemaComplexType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.canIncludeImport = false;
				builder.schema.Items.Add(builder.complexType);
				return;
			}
			if (parentElement == SchemaNames.Token.XsdElement)
			{
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("Sch_DupElement", "complexType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("Sch_TypeAfterConstraints", null);
				}
				builder.element.SchemaType = builder.complexType;
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.complexType);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000DDA63 File Offset: 0x000DBC63
		private static void BuildComplexType_Abstract(XsdBuilder builder, string value)
		{
			builder.complexType.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000DDA7C File Offset: 0x000DBC7C
		private static void BuildComplexType_Block(XsdBuilder builder, string value)
		{
			builder.complexType.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000DDA95 File Offset: 0x000DBC95
		private static void BuildComplexType_Final(XsdBuilder builder, string value)
		{
			builder.complexType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000DDAAE File Offset: 0x000DBCAE
		private static void BuildComplexType_Mixed(XsdBuilder builder, string value)
		{
			builder.complexType.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000DDAC7 File Offset: 0x000DBCC7
		private static void BuildComplexType_Name(XsdBuilder builder, string value)
		{
			builder.complexType.Name = value;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000DDAD8 File Offset: 0x000DBCD8
		private static void InitComplexContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("Sch_ComplexTypeContentModel", "complexContent");
			}
			builder.xso = (builder.complexContent = new XmlSchemaComplexContent());
			builder.complexType.ContentModel = builder.complexContent;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000DDB53 File Offset: 0x000DBD53
		private static void BuildComplexContent_Mixed(XsdBuilder builder, string value)
		{
			builder.complexContent.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000DDB6C File Offset: 0x000DBD6C
		private static void InitComplexContentExtension(XsdBuilder builder, string value)
		{
			if (builder.complexContent.Content != null)
			{
				builder.SendValidationEvent("Sch_ComplexContentContentModel", "extension");
			}
			builder.xso = (builder.complexContentExtension = new XmlSchemaComplexContentExtension());
			builder.complexContent.Content = builder.complexContentExtension;
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000DDBBB File Offset: 0x000DBDBB
		private static void BuildComplexContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.complexContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000DDBD4 File Offset: 0x000DBDD4
		private static void InitComplexContentRestriction(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexContentRestriction = new XmlSchemaComplexContentRestriction());
			builder.complexContent.Content = builder.complexContentRestriction;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000DDC06 File Offset: 0x000DBE06
		private static void BuildComplexContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.complexContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000DDC20 File Offset: 0x000DBE20
		private static void InitSimpleContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("Sch_ComplexTypeContentModel", "simpleContent");
			}
			builder.xso = (builder.simpleContent = new XmlSchemaSimpleContent());
			builder.complexType.ContentModel = builder.simpleContent;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000DDC9C File Offset: 0x000DBE9C
		private static void InitSimpleContentExtension(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("Sch_DupElement", "extension");
			}
			builder.xso = (builder.simpleContentExtension = new XmlSchemaSimpleContentExtension());
			builder.simpleContent.Content = builder.simpleContentExtension;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000DDCEB File Offset: 0x000DBEEB
		private static void BuildSimpleContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000DDD04 File Offset: 0x000DBF04
		private static void InitSimpleContentRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("Sch_DupElement", "restriction");
			}
			builder.xso = (builder.simpleContentRestriction = new XmlSchemaSimpleContentRestriction());
			builder.simpleContent.Content = builder.simpleContentRestriction;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000DDD53 File Offset: 0x000DBF53
		private static void BuildSimpleContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000DDD6C File Offset: 0x000DBF6C
		private static void InitAttributeGroup(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = false;
			builder.xso = (builder.attributeGroup = new XmlSchemaAttributeGroup());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attributeGroup);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.attributeGroup);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000DDDD5 File Offset: 0x000DBFD5
		private static void BuildAttributeGroup_Name(XsdBuilder builder, string value)
		{
			builder.attributeGroup.Name = value;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000DDDE4 File Offset: 0x000DBFE4
		private static void InitAttributeGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attributeGroupRef = new XmlSchemaAttributeGroupRef());
			builder.AddAttribute(builder.attributeGroupRef);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000DDE11 File Offset: 0x000DC011
		private static void BuildAttributeGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.attributeGroupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000DDE2C File Offset: 0x000DC02C
		private static void InitAnyAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.anyAttribute = new XmlSchemaAnyAttribute());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (builder.complexType.ContentModel != null)
					{
						builder.SendValidationEvent("Sch_AttributeMutuallyExclusive", "anyAttribute");
					}
					if (builder.complexType.AnyAttribute != null)
					{
						builder.SendValidationEvent("Sch_DupElement", "anyAttribute");
					}
					builder.complexType.AnyAttribute = builder.anyAttribute;
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (builder.complexContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("Sch_DupElement", "anyAttribute");
					}
					builder.complexContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (builder.complexContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("Sch_DupElement", "anyAttribute");
					}
					builder.complexContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (builder.simpleContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("Sch_DupElement", "anyAttribute");
					}
					builder.simpleContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (builder.simpleContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("Sch_DupElement", "anyAttribute");
					}
					builder.simpleContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				default:
					return;
				}
			}
			else
			{
				if (builder.attributeGroup.AnyAttribute != null)
				{
					builder.SendValidationEvent("Sch_DupElement", "anyAttribute");
				}
				builder.attributeGroup.AnyAttribute = builder.anyAttribute;
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000DDFB5 File Offset: 0x000DC1B5
		private static void BuildAnyAttribute_Namespace(XsdBuilder builder, string value)
		{
			builder.anyAttribute.Namespace = value;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000DDFC3 File Offset: 0x000DC1C3
		private static void BuildAnyAttribute_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyAttribute.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000DDFE4 File Offset: 0x000DC1E4
		private static void InitGroup(XsdBuilder builder, string value)
		{
			builder.xso = (builder.group = new XmlSchemaGroup());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.group);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.group);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000DE04D File Offset: 0x000DC24D
		private static void BuildGroup_Name(XsdBuilder builder, string value)
		{
			builder.group.Name = value;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000DE05C File Offset: 0x000DC25C
		private static void InitGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.groupRef = new XmlSchemaGroupRef()));
			builder.AddParticle(builder.groupRef);
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000DE092 File Offset: 0x000DC292
		private static void BuildParticle_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.particle, value);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000DE0A1 File Offset: 0x000DC2A1
		private static void BuildParticle_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.particle, value);
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000DE0B0 File Offset: 0x000DC2B0
		private static void BuildGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.groupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000DE0CC File Offset: 0x000DC2CC
		private static void InitAll(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.all = new XmlSchemaAll()));
			builder.AddParticle(builder.all);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000DE104 File Offset: 0x000DC304
		private static void InitChoice(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.choice = new XmlSchemaChoice()));
			builder.AddParticle(builder.choice);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000DE13C File Offset: 0x000DC33C
		private static void InitSequence(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.sequence = new XmlSchemaSequence()));
			builder.AddParticle(builder.sequence);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000DE174 File Offset: 0x000DC374
		private static void InitAny(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.anyElement = new XmlSchemaAny()));
			builder.AddParticle(builder.anyElement);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000DE1AA File Offset: 0x000DC3AA
		private static void BuildAny_Namespace(XsdBuilder builder, string value)
		{
			builder.anyElement.Namespace = value;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000DE1B8 File Offset: 0x000DC3B8
		private static void BuildAny_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyElement.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000DE1D8 File Offset: 0x000DC3D8
		private static void InitNotation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.notation = new XmlSchemaNotation());
			builder.canIncludeImport = false;
			builder.schema.Items.Add(builder.notation);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000DE217 File Offset: 0x000DC417
		private static void BuildNotation_Name(XsdBuilder builder, string value)
		{
			builder.notation.Name = value;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000DE225 File Offset: 0x000DC425
		private static void BuildNotation_Public(XsdBuilder builder, string value)
		{
			builder.notation.Public = value;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000DE233 File Offset: 0x000DC433
		private static void BuildNotation_System(XsdBuilder builder, string value)
		{
			builder.notation.System = value;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000DE244 File Offset: 0x000DC444
		private static void InitFacet(XsdBuilder builder, string value)
		{
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdMinExclusive:
				builder.facet = new XmlSchemaMinExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMinInclusive:
				builder.facet = new XmlSchemaMinInclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxExclusive:
				builder.facet = new XmlSchemaMaxExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxInclusive:
				builder.facet = new XmlSchemaMaxInclusiveFacet();
				break;
			case SchemaNames.Token.XsdTotalDigits:
				builder.facet = new XmlSchemaTotalDigitsFacet();
				break;
			case SchemaNames.Token.XsdFractionDigits:
				builder.facet = new XmlSchemaFractionDigitsFacet();
				break;
			case SchemaNames.Token.XsdLength:
				builder.facet = new XmlSchemaLengthFacet();
				break;
			case SchemaNames.Token.XsdMinLength:
				builder.facet = new XmlSchemaMinLengthFacet();
				break;
			case SchemaNames.Token.XsdMaxLength:
				builder.facet = new XmlSchemaMaxLengthFacet();
				break;
			case SchemaNames.Token.XsdEnumeration:
				builder.facet = new XmlSchemaEnumerationFacet();
				break;
			case SchemaNames.Token.XsdPattern:
				builder.facet = new XmlSchemaPatternFacet();
				break;
			case SchemaNames.Token.XsdWhitespace:
				builder.facet = new XmlSchemaWhiteSpaceFacet();
				break;
			}
			builder.xso = builder.facet;
			if (SchemaNames.Token.XsdSimpleTypeRestriction == builder.ParentElement)
			{
				builder.simpleTypeRestriction.Facets.Add(builder.facet);
				return;
			}
			if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null)
			{
				builder.SendValidationEvent("Sch_InvalidFacetPosition", null);
			}
			builder.simpleContentRestriction.Facets.Add(builder.facet);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000DE3D2 File Offset: 0x000DC5D2
		private static void BuildFacet_Fixed(XsdBuilder builder, string value)
		{
			builder.facet.IsFixed = builder.ParseBoolean(value, "fixed");
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000DE3EB File Offset: 0x000DC5EB
		private static void BuildFacet_Value(XsdBuilder builder, string value)
		{
			builder.facet.Value = value;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000DE3FC File Offset: 0x000DC5FC
		private static void InitIdentityConstraint(XsdBuilder builder, string value)
		{
			if (!builder.element.RefName.IsEmpty)
			{
				builder.SendValidationEvent("Sch_ElementRef", null);
			}
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdUnique:
				builder.xso = (builder.identityConstraint = new XmlSchemaUnique());
				break;
			case SchemaNames.Token.XsdKey:
				builder.xso = (builder.identityConstraint = new XmlSchemaKey());
				break;
			case SchemaNames.Token.XsdKeyref:
				builder.xso = (builder.identityConstraint = new XmlSchemaKeyref());
				break;
			}
			builder.element.Constraints.Add(builder.identityConstraint);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000DE49C File Offset: 0x000DC69C
		private static void BuildIdentityConstraint_Name(XsdBuilder builder, string value)
		{
			builder.identityConstraint.Name = value;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000DE4AA File Offset: 0x000DC6AA
		private static void BuildIdentityConstraint_Refer(XsdBuilder builder, string value)
		{
			if (builder.identityConstraint is XmlSchemaKeyref)
			{
				((XmlSchemaKeyref)builder.identityConstraint).Refer = builder.ParseQName(value, "refer");
				return;
			}
			builder.SendValidationEvent("Sch_UnsupportedAttribute", "refer");
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000DE4E8 File Offset: 0x000DC6E8
		private static void InitSelector(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.identityConstraint.Selector = builder.xpath;
				return;
			}
			builder.SendValidationEvent("Sch_DupSelector", builder.identityConstraint.Name);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000DE53E File Offset: 0x000DC73E
		private static void BuildSelector_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000DE54C File Offset: 0x000DC74C
		private static void InitField(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.SendValidationEvent("Sch_SelectorBeforeFields", builder.identityConstraint.Name);
			}
			builder.identityConstraint.Fields.Add(builder.xpath);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000DE5A7 File Offset: 0x000DC7A7
		private static void BuildField_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000DE5B8 File Offset: 0x000DC7B8
		private static void InitAnnotation(XsdBuilder builder, string value)
		{
			if (builder.hasChild && builder.ParentElement != SchemaNames.Token.XsdSchema && builder.ParentElement != SchemaNames.Token.XsdRedefine)
			{
				builder.SendValidationEvent("Sch_AnnotationLocation", null);
			}
			builder.xso = (builder.annotation = new XmlSchemaAnnotation());
			builder.ParentContainer.AddAnnotation(builder.annotation);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000DE614 File Offset: 0x000DC814
		private static void InitAppinfo(XsdBuilder builder, string value)
		{
			builder.xso = (builder.appInfo = new XmlSchemaAppInfo());
			builder.annotation.Items.Add(builder.appInfo);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000DE658 File Offset: 0x000DC858
		private static void BuildAppinfo_Source(XsdBuilder builder, string value)
		{
			builder.appInfo.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000DE66B File Offset: 0x000DC86B
		private static void EndAppinfo(XsdBuilder builder)
		{
			builder.appInfo.Markup = builder.markup;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000DE680 File Offset: 0x000DC880
		private static void InitDocumentation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.documentation = new XmlSchemaDocumentation());
			builder.annotation.Items.Add(builder.documentation);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000DE6C4 File Offset: 0x000DC8C4
		private static void BuildDocumentation_Source(XsdBuilder builder, string value)
		{
			builder.documentation.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000DE6D8 File Offset: 0x000DC8D8
		private static void BuildDocumentation_XmlLang(XsdBuilder builder, string value)
		{
			try
			{
				builder.documentation.Language = value;
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000DE734 File Offset: 0x000DC934
		private static void EndDocumentation(XsdBuilder builder)
		{
			builder.documentation.Markup = builder.markup;
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000DE748 File Offset: 0x000DC948
		private void AddAttribute(XmlSchemaObject value)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null)
					{
						this.SendValidationEvent("Sch_AttributeMutuallyExclusive", "attribute");
					}
					if (this.complexType.AnyAttribute != null)
					{
						this.SendValidationEvent("Sch_AnyAttributeLastChild", null);
					}
					this.complexType.Attributes.Add(value);
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("Sch_AnyAttributeLastChild", null);
					}
					this.complexContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (this.complexContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("Sch_AnyAttributeLastChild", null);
					}
					this.complexContentRestriction.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (this.simpleContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("Sch_AnyAttributeLastChild", null);
					}
					this.simpleContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (this.simpleContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("Sch_AnyAttributeLastChild", null);
					}
					this.simpleContentRestriction.Attributes.Add(value);
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.attributeGroup.AnyAttribute != null)
				{
					this.SendValidationEvent("Sch_AnyAttributeLastChild", null);
				}
				this.attributeGroup.Attributes.Add(value);
			}
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000DE8AC File Offset: 0x000DCAAC
		private void AddParticle(XmlSchemaParticle particle)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdSequence)
			{
				if (parentElement == SchemaNames.Token.XsdGroup)
				{
					if (this.group.Particle != null)
					{
						this.SendValidationEvent("Sch_DupGroupParticle", "particle");
					}
					this.group.Particle = (XmlSchemaGroupBase)particle;
					return;
				}
				if (parentElement - SchemaNames.Token.XsdChoice > 1)
				{
					return;
				}
				((XmlSchemaGroupBase)this.ParentContainer).Items.Add(particle);
				return;
			}
			else
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null || this.complexType.Attributes.Count != 0 || this.complexType.AnyAttribute != null || this.complexType.Particle != null)
					{
						this.SendValidationEvent("Sch_ComplexTypeContentModel", "complexType");
					}
					this.complexType.Particle = particle;
					return;
				}
				if (parentElement == SchemaNames.Token.XsdComplexContentExtension)
				{
					if (this.complexContentExtension.Particle != null || this.complexContentExtension.Attributes.Count != 0 || this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("Sch_ComplexContentContentModel", "ComplexContentExtension");
					}
					this.complexContentExtension.Particle = particle;
					return;
				}
				if (parentElement != SchemaNames.Token.XsdComplexContentRestriction)
				{
					return;
				}
				if (this.complexContentRestriction.Particle != null || this.complexContentRestriction.Attributes.Count != 0 || this.complexContentRestriction.AnyAttribute != null)
				{
					this.SendValidationEvent("Sch_ComplexContentContentModel", "ComplexContentExtension");
				}
				this.complexContentRestriction.Particle = particle;
				return;
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000DEA1C File Offset: 0x000DCC1C
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this.currentEntry.NextStates != null)
			{
				for (int i = 0; i < this.currentEntry.NextStates.Length; i++)
				{
					int num = (int)this.currentEntry.NextStates[i];
					if (this.schemaNames.TokenToQName[(int)XsdBuilder.SchemaEntries[num].Name].Equals(qname))
					{
						this.nextEntry = XsdBuilder.SchemaEntries[num];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000DEA8C File Offset: 0x000DCC8C
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			return this.CurrentElement == SchemaNames.Token.XsdDocumentation || this.CurrentElement == SchemaNames.Token.XsdAppInfo;
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000DEAA4 File Offset: 0x000DCCA4
		private void SetMinOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MinOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("Sch_MinOccursInvalidXsd", null);
			}
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000DEADC File Offset: 0x000DCCDC
		private void SetMaxOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MaxOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("Sch_MaxOccursInvalidXsd", null);
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000DEB14 File Offset: 0x000DCD14
		private bool ParseBoolean(string value, string attributeName)
		{
			bool result;
			try
			{
				result = XmlConvert.ToBoolean(value);
			}
			catch (Exception)
			{
				this.SendValidationEvent("Sch_InvalidXsdAttributeValue", attributeName, value, null);
				result = false;
			}
			return result;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000DEB50 File Offset: 0x000DCD50
		private int ParseEnum(string value, string attributeName, string[] values)
		{
			string text = value.Trim();
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] == text)
				{
					return i + 1;
				}
			}
			this.SendValidationEvent("Sch_InvalidXsdAttributeValue", attributeName, text, null);
			return 0;
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000DEB90 File Offset: 0x000DCD90
		private XmlQualifiedName ParseQName(string value, string attributeName)
		{
			XmlQualifiedName result;
			try
			{
				value = XmlComplianceUtil.NonCDataNormalize(value);
				string text;
				result = XmlQualifiedName.Parse(value, this.namespaceManager, out text);
			}
			catch (Exception)
			{
				this.SendValidationEvent("Sch_InvalidXsdAttributeValue", attributeName, value, null);
				result = XmlQualifiedName.Empty;
			}
			return result;
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000DEBE0 File Offset: 0x000DCDE0
		private int ParseBlockFinalEnum(string value, string attributeName)
		{
			int num = 0;
			string[] array = XmlConvert.SplitString(value);
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < XsdBuilder.DerivationMethodStrings.Length)
				{
					if (array[i] == XsdBuilder.DerivationMethodStrings[j])
					{
						if ((num & XsdBuilder.DerivationMethodValues[j]) != 0 && (num & XsdBuilder.DerivationMethodValues[j]) != XsdBuilder.DerivationMethodValues[j])
						{
							this.SendValidationEvent("Sch_InvalidXsdAttributeValue", attributeName, value, null);
							return 0;
						}
						num |= XsdBuilder.DerivationMethodValues[j];
						flag = true;
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					this.SendValidationEvent("Sch_InvalidXsdAttributeValue", attributeName, value, null);
					return 0;
				}
				if (num == 255 && value.Length > 4)
				{
					this.SendValidationEvent("Sch_InvalidXsdAttributeValue", attributeName, value, null);
					return 0;
				}
			}
			return num;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000DECA8 File Offset: 0x000DCEA8
		private static string ParseUriReference(string s)
		{
			return s;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000DECAC File Offset: 0x000DCEAC
		private void SendValidationEvent(string code, string arg0, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				arg0,
				arg1,
				arg2
			}, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000DECF9 File Offset: 0x000DCEF9
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000DED29 File Offset: 0x000DCF29
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000DED5C File Offset: 0x000DCF5C
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			XmlSchema xmlSchema = this.schema;
			int errorCount = xmlSchema.ErrorCount;
			xmlSchema.ErrorCount = errorCount + 1;
			e.SetSchemaObject(this.schema);
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000DEDAB File Offset: 0x000DCFAB
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000DEDB8 File Offset: 0x000DCFB8
		private void RecordPosition()
		{
			this.xso.SourceUri = this.reader.BaseURI;
			this.xso.LineNumber = this.positionInfo.LineNumber;
			this.xso.LinePosition = this.positionInfo.LinePosition;
			if (this.xso != this.schema)
			{
				this.xso.Parent = this.ParentContainer;
			}
		}

		// Token: 0x04001223 RID: 4643
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001224 RID: 4644
		private static readonly XsdBuilder.State[] SchemaElement = new XsdBuilder.State[]
		{
			XsdBuilder.State.Schema
		};

		// Token: 0x04001225 RID: 4645
		private static readonly XsdBuilder.State[] SchemaSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Include,
			XsdBuilder.State.Import,
			XsdBuilder.State.Redefine,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Element,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.Group,
			XsdBuilder.State.Notation
		};

		// Token: 0x04001226 RID: 4646
		private static readonly XsdBuilder.State[] AttributeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001227 RID: 4647
		private static readonly XsdBuilder.State[] ElementSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Unique,
			XsdBuilder.State.Key,
			XsdBuilder.State.KeyRef
		};

		// Token: 0x04001228 RID: 4648
		private static readonly XsdBuilder.State[] ComplexTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContent,
			XsdBuilder.State.ComplexContent,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001229 RID: 4649
		private static readonly XsdBuilder.State[] SimpleContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContentRestriction,
			XsdBuilder.State.SimpleContentExtension
		};

		// Token: 0x0400122A RID: 4650
		private static readonly XsdBuilder.State[] SimpleContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x0400122B RID: 4651
		private static readonly XsdBuilder.State[] SimpleContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x0400122C RID: 4652
		private static readonly XsdBuilder.State[] ComplexContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.ComplexContentRestriction,
			XsdBuilder.State.ComplexContentExtension
		};

		// Token: 0x0400122D RID: 4653
		private static readonly XsdBuilder.State[] ComplexContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x0400122E RID: 4654
		private static readonly XsdBuilder.State[] ComplexContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x0400122F RID: 4655
		private static readonly XsdBuilder.State[] SimpleTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleTypeList,
			XsdBuilder.State.SimpleTypeRestriction,
			XsdBuilder.State.SimpleTypeUnion
		};

		// Token: 0x04001230 RID: 4656
		private static readonly XsdBuilder.State[] SimpleTypeRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace
		};

		// Token: 0x04001231 RID: 4657
		private static readonly XsdBuilder.State[] SimpleTypeListSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001232 RID: 4658
		private static readonly XsdBuilder.State[] SimpleTypeUnionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001233 RID: 4659
		private static readonly XsdBuilder.State[] RedefineSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Group,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x04001234 RID: 4660
		private static readonly XsdBuilder.State[] AttributeGroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x04001235 RID: 4661
		private static readonly XsdBuilder.State[] GroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence
		};

		// Token: 0x04001236 RID: 4662
		private static readonly XsdBuilder.State[] AllSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element
		};

		// Token: 0x04001237 RID: 4663
		private static readonly XsdBuilder.State[] ChoiceSequenceSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Any
		};

		// Token: 0x04001238 RID: 4664
		private static readonly XsdBuilder.State[] IdentityConstraintSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Selector,
			XsdBuilder.State.Field
		};

		// Token: 0x04001239 RID: 4665
		private static readonly XsdBuilder.State[] AnnotationSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.AppInfo,
			XsdBuilder.State.Documentation
		};

		// Token: 0x0400123A RID: 4666
		private static readonly XsdBuilder.State[] AnnotatedSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation
		};

		// Token: 0x0400123B RID: 4667
		private static readonly XsdBuilder.XsdAttributeEntry[] SchemaAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAttributeFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_AttributeFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaElementFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_ElementFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaTargetNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_TargetNamespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaVersion, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_Version)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinalDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_FinalDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlockDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_BlockDefault))
		};

		// Token: 0x0400123C RID: 4668
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Type)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaUse, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Use))
		};

		// Token: 0x0400123D RID: 4669
		private static readonly XsdBuilder.XsdAttributeEntry[] ElementAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNillable, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Nillable)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSubstitutionGroup, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_SubstitutionGroup)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Type))
		};

		// Token: 0x0400123E RID: 4670
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Mixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Name))
		};

		// Token: 0x0400123F RID: 4671
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001240 RID: 4672
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001241 RID: 4673
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001242 RID: 4674
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContent_Mixed))
		};

		// Token: 0x04001243 RID: 4675
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001244 RID: 4676
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001245 RID: 4677
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Name))
		};

		// Token: 0x04001246 RID: 4678
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x04001247 RID: 4679
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeUnionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMemberTypes, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeUnion_MemberTypes))
		};

		// Token: 0x04001248 RID: 4680
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeListAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaItemType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeList_ItemType))
		};

		// Token: 0x04001249 RID: 4681
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroup_Name))
		};

		// Token: 0x0400124A RID: 4682
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroupRef_Ref))
		};

		// Token: 0x0400124B RID: 4683
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroup_Name))
		};

		// Token: 0x0400124C RID: 4684
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroupRef_Ref))
		};

		// Token: 0x0400124D RID: 4685
		private static readonly XsdBuilder.XsdAttributeEntry[] ParticleAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs))
		};

		// Token: 0x0400124E RID: 4686
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_ProcessContents))
		};

		// Token: 0x0400124F RID: 4687
		private static readonly XsdBuilder.XsdAttributeEntry[] IdentityConstraintAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRefer, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Refer))
		};

		// Token: 0x04001250 RID: 4688
		private static readonly XsdBuilder.XsdAttributeEntry[] SelectorAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSelector_XPath))
		};

		// Token: 0x04001251 RID: 4689
		private static readonly XsdBuilder.XsdAttributeEntry[] FieldAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildField_XPath))
		};

		// Token: 0x04001252 RID: 4690
		private static readonly XsdBuilder.XsdAttributeEntry[] NotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaPublic, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Public)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSystem, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_System))
		};

		// Token: 0x04001253 RID: 4691
		private static readonly XsdBuilder.XsdAttributeEntry[] IncludeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildInclude_SchemaLocation))
		};

		// Token: 0x04001254 RID: 4692
		private static readonly XsdBuilder.XsdAttributeEntry[] ImportAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_SchemaLocation))
		};

		// Token: 0x04001255 RID: 4693
		private static readonly XsdBuilder.XsdAttributeEntry[] FacetAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaValue, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Value))
		};

		// Token: 0x04001256 RID: 4694
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_ProcessContents))
		};

		// Token: 0x04001257 RID: 4695
		private static readonly XsdBuilder.XsdAttributeEntry[] DocumentationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_Source)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.XmlLang, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_XmlLang))
		};

		// Token: 0x04001258 RID: 4696
		private static readonly XsdBuilder.XsdAttributeEntry[] AppinfoAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAppinfo_Source))
		};

		// Token: 0x04001259 RID: 4697
		private static readonly XsdBuilder.XsdAttributeEntry[] RedefineAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildRedefine_SchemaLocation))
		};

		// Token: 0x0400125A RID: 4698
		private static readonly XsdBuilder.XsdAttributeEntry[] AnnotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x0400125B RID: 4699
		private static readonly XsdBuilder.XsdEntry[] SchemaEntries = new XsdBuilder.XsdEntry[]
		{
			new XsdBuilder.XsdEntry(SchemaNames.Token.Empty, XsdBuilder.State.Root, XsdBuilder.SchemaElement, null, null, null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSchema, XsdBuilder.State.Schema, XsdBuilder.SchemaSubelements, XsdBuilder.SchemaAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSchema), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnnotation, XsdBuilder.State.Annotation, XsdBuilder.AnnotationSubelements, XsdBuilder.AnnotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnnotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdInclude, XsdBuilder.State.Include, XsdBuilder.AnnotatedSubelements, XsdBuilder.IncludeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitInclude), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdImport, XsdBuilder.State.Import, XsdBuilder.AnnotatedSubelements, XsdBuilder.ImportAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitImport), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdElement, XsdBuilder.State.Element, XsdBuilder.ElementSubelements, XsdBuilder.ElementAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitElement), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAttribute, XsdBuilder.State.Attribute, XsdBuilder.AttributeSubelements, XsdBuilder.AttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroup, XsdBuilder.AttributeGroupSubelements, XsdBuilder.AttributeGroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.AttributeGroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnyAttribute, XsdBuilder.State.AnyAttribute, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnyAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.Group, XsdBuilder.GroupSubelements, XsdBuilder.GroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.GroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.GroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAll, XsdBuilder.State.All, XsdBuilder.AllSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAll), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdChoice, XsdBuilder.State.Choice, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitChoice), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSequence, XsdBuilder.State.Sequence, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSequence), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAny, XsdBuilder.State.Any, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAny), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdNotation, XsdBuilder.State.Notation, XsdBuilder.AnnotatedSubelements, XsdBuilder.NotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitNotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleType, XsdBuilder.State.SimpleType, XsdBuilder.SimpleTypeSubelements, XsdBuilder.SimpleTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexType, XsdBuilder.State.ComplexType, XsdBuilder.ComplexTypeSubelements, XsdBuilder.ComplexTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContent, XsdBuilder.State.ComplexContent, XsdBuilder.ComplexContentSubelements, XsdBuilder.ComplexContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentRestriction, XsdBuilder.State.ComplexContentRestriction, XsdBuilder.ComplexContentRestrictionSubelements, XsdBuilder.ComplexContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentExtension, XsdBuilder.State.ComplexContentExtension, XsdBuilder.ComplexContentExtensionSubelements, XsdBuilder.ComplexContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContent, XsdBuilder.State.SimpleContent, XsdBuilder.SimpleContentSubelements, XsdBuilder.SimpleContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentExtension, XsdBuilder.State.SimpleContentExtension, XsdBuilder.SimpleContentExtensionSubelements, XsdBuilder.SimpleContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentRestriction, XsdBuilder.State.SimpleContentRestriction, XsdBuilder.SimpleContentRestrictionSubelements, XsdBuilder.SimpleContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeUnion, XsdBuilder.State.SimpleTypeUnion, XsdBuilder.SimpleTypeUnionSubelements, XsdBuilder.SimpleTypeUnionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeUnion), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeList, XsdBuilder.State.SimpleTypeList, XsdBuilder.SimpleTypeListSubelements, XsdBuilder.SimpleTypeListAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeList), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeRestriction, XsdBuilder.State.SimpleTypeRestriction, XsdBuilder.SimpleTypeRestrictionSubelements, XsdBuilder.SimpleTypeRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdUnique, XsdBuilder.State.Unique, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKey, XsdBuilder.State.Key, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKeyref, XsdBuilder.State.KeyRef, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSelector, XsdBuilder.State.Selector, XsdBuilder.AnnotatedSubelements, XsdBuilder.SelectorAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSelector), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdField, XsdBuilder.State.Field, XsdBuilder.AnnotatedSubelements, XsdBuilder.FieldAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitField), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinExclusive, XsdBuilder.State.MinExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinInclusive, XsdBuilder.State.MinInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxExclusive, XsdBuilder.State.MaxExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxInclusive, XsdBuilder.State.MaxInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdTotalDigits, XsdBuilder.State.TotalDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdFractionDigits, XsdBuilder.State.FractionDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdLength, XsdBuilder.State.Length, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinLength, XsdBuilder.State.MinLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxLength, XsdBuilder.State.MaxLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdEnumeration, XsdBuilder.State.Enumeration, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdPattern, XsdBuilder.State.Pattern, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdWhitespace, XsdBuilder.State.WhiteSpace, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAppInfo, XsdBuilder.State.AppInfo, null, XsdBuilder.AppinfoAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAppinfo), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndAppinfo), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdDocumentation, XsdBuilder.State.Documentation, null, XsdBuilder.DocumentationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitDocumentation), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndDocumentation), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdRedefine, XsdBuilder.State.Redefine, XsdBuilder.RedefineSubelements, XsdBuilder.RedefineAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitRedefine), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndRedefine), true)
		};

		// Token: 0x0400125C RID: 4700
		private static readonly int[] DerivationMethodValues = new int[]
		{
			1,
			2,
			4,
			8,
			16,
			255
		};

		// Token: 0x0400125D RID: 4701
		private static readonly string[] DerivationMethodStrings = new string[]
		{
			"substitution",
			"extension",
			"restriction",
			"list",
			"union",
			"#all"
		};

		// Token: 0x0400125E RID: 4702
		private static readonly string[] FormStringValues = new string[]
		{
			"qualified",
			"unqualified"
		};

		// Token: 0x0400125F RID: 4703
		private static readonly string[] UseStringValues = new string[]
		{
			"optional",
			"prohibited",
			"required"
		};

		// Token: 0x04001260 RID: 4704
		private static readonly string[] ProcessContentsStringValues = new string[]
		{
			"skip",
			"lax",
			"strict"
		};

		// Token: 0x04001261 RID: 4705
		private XmlReader reader;

		// Token: 0x04001262 RID: 4706
		private PositionInfo positionInfo;

		// Token: 0x04001263 RID: 4707
		private XsdBuilder.XsdEntry currentEntry;

		// Token: 0x04001264 RID: 4708
		private XsdBuilder.XsdEntry nextEntry;

		// Token: 0x04001265 RID: 4709
		private bool hasChild;

		// Token: 0x04001266 RID: 4710
		private HWStack stateHistory = new HWStack(10);

		// Token: 0x04001267 RID: 4711
		private Stack containerStack = new Stack();

		// Token: 0x04001268 RID: 4712
		private XmlNameTable nameTable;

		// Token: 0x04001269 RID: 4713
		private SchemaNames schemaNames;

		// Token: 0x0400126A RID: 4714
		private XmlNamespaceManager namespaceManager;

		// Token: 0x0400126B RID: 4715
		private bool canIncludeImport;

		// Token: 0x0400126C RID: 4716
		private XmlSchema schema;

		// Token: 0x0400126D RID: 4717
		private XmlSchemaObject xso;

		// Token: 0x0400126E RID: 4718
		private XmlSchemaElement element;

		// Token: 0x0400126F RID: 4719
		private XmlSchemaAny anyElement;

		// Token: 0x04001270 RID: 4720
		private XmlSchemaAttribute attribute;

		// Token: 0x04001271 RID: 4721
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001272 RID: 4722
		private XmlSchemaComplexType complexType;

		// Token: 0x04001273 RID: 4723
		private XmlSchemaSimpleType simpleType;

		// Token: 0x04001274 RID: 4724
		private XmlSchemaComplexContent complexContent;

		// Token: 0x04001275 RID: 4725
		private XmlSchemaComplexContentExtension complexContentExtension;

		// Token: 0x04001276 RID: 4726
		private XmlSchemaComplexContentRestriction complexContentRestriction;

		// Token: 0x04001277 RID: 4727
		private XmlSchemaSimpleContent simpleContent;

		// Token: 0x04001278 RID: 4728
		private XmlSchemaSimpleContentExtension simpleContentExtension;

		// Token: 0x04001279 RID: 4729
		private XmlSchemaSimpleContentRestriction simpleContentRestriction;

		// Token: 0x0400127A RID: 4730
		private XmlSchemaSimpleTypeUnion simpleTypeUnion;

		// Token: 0x0400127B RID: 4731
		private XmlSchemaSimpleTypeList simpleTypeList;

		// Token: 0x0400127C RID: 4732
		private XmlSchemaSimpleTypeRestriction simpleTypeRestriction;

		// Token: 0x0400127D RID: 4733
		private XmlSchemaGroup group;

		// Token: 0x0400127E RID: 4734
		private XmlSchemaGroupRef groupRef;

		// Token: 0x0400127F RID: 4735
		private XmlSchemaAll all;

		// Token: 0x04001280 RID: 4736
		private XmlSchemaChoice choice;

		// Token: 0x04001281 RID: 4737
		private XmlSchemaSequence sequence;

		// Token: 0x04001282 RID: 4738
		private XmlSchemaParticle particle;

		// Token: 0x04001283 RID: 4739
		private XmlSchemaAttributeGroup attributeGroup;

		// Token: 0x04001284 RID: 4740
		private XmlSchemaAttributeGroupRef attributeGroupRef;

		// Token: 0x04001285 RID: 4741
		private XmlSchemaNotation notation;

		// Token: 0x04001286 RID: 4742
		private XmlSchemaIdentityConstraint identityConstraint;

		// Token: 0x04001287 RID: 4743
		private XmlSchemaXPath xpath;

		// Token: 0x04001288 RID: 4744
		private XmlSchemaInclude include;

		// Token: 0x04001289 RID: 4745
		private XmlSchemaImport import;

		// Token: 0x0400128A RID: 4746
		private XmlSchemaAnnotation annotation;

		// Token: 0x0400128B RID: 4747
		private XmlSchemaAppInfo appInfo;

		// Token: 0x0400128C RID: 4748
		private XmlSchemaDocumentation documentation;

		// Token: 0x0400128D RID: 4749
		private XmlSchemaFacet facet;

		// Token: 0x0400128E RID: 4750
		private XmlNode[] markup;

		// Token: 0x0400128F RID: 4751
		private XmlSchemaRedefine redefine;

		// Token: 0x04001290 RID: 4752
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001291 RID: 4753
		private ArrayList unhandledAttributes = new ArrayList();

		// Token: 0x04001292 RID: 4754
		private Hashtable namespaces;

		// Token: 0x020004AD RID: 1197
		private enum State
		{
			// Token: 0x04001F19 RID: 7961
			Root,
			// Token: 0x04001F1A RID: 7962
			Schema,
			// Token: 0x04001F1B RID: 7963
			Annotation,
			// Token: 0x04001F1C RID: 7964
			Include,
			// Token: 0x04001F1D RID: 7965
			Import,
			// Token: 0x04001F1E RID: 7966
			Element,
			// Token: 0x04001F1F RID: 7967
			Attribute,
			// Token: 0x04001F20 RID: 7968
			AttributeGroup,
			// Token: 0x04001F21 RID: 7969
			AttributeGroupRef,
			// Token: 0x04001F22 RID: 7970
			AnyAttribute,
			// Token: 0x04001F23 RID: 7971
			Group,
			// Token: 0x04001F24 RID: 7972
			GroupRef,
			// Token: 0x04001F25 RID: 7973
			All,
			// Token: 0x04001F26 RID: 7974
			Choice,
			// Token: 0x04001F27 RID: 7975
			Sequence,
			// Token: 0x04001F28 RID: 7976
			Any,
			// Token: 0x04001F29 RID: 7977
			Notation,
			// Token: 0x04001F2A RID: 7978
			SimpleType,
			// Token: 0x04001F2B RID: 7979
			ComplexType,
			// Token: 0x04001F2C RID: 7980
			ComplexContent,
			// Token: 0x04001F2D RID: 7981
			ComplexContentRestriction,
			// Token: 0x04001F2E RID: 7982
			ComplexContentExtension,
			// Token: 0x04001F2F RID: 7983
			SimpleContent,
			// Token: 0x04001F30 RID: 7984
			SimpleContentExtension,
			// Token: 0x04001F31 RID: 7985
			SimpleContentRestriction,
			// Token: 0x04001F32 RID: 7986
			SimpleTypeUnion,
			// Token: 0x04001F33 RID: 7987
			SimpleTypeList,
			// Token: 0x04001F34 RID: 7988
			SimpleTypeRestriction,
			// Token: 0x04001F35 RID: 7989
			Unique,
			// Token: 0x04001F36 RID: 7990
			Key,
			// Token: 0x04001F37 RID: 7991
			KeyRef,
			// Token: 0x04001F38 RID: 7992
			Selector,
			// Token: 0x04001F39 RID: 7993
			Field,
			// Token: 0x04001F3A RID: 7994
			MinExclusive,
			// Token: 0x04001F3B RID: 7995
			MinInclusive,
			// Token: 0x04001F3C RID: 7996
			MaxExclusive,
			// Token: 0x04001F3D RID: 7997
			MaxInclusive,
			// Token: 0x04001F3E RID: 7998
			TotalDigits,
			// Token: 0x04001F3F RID: 7999
			FractionDigits,
			// Token: 0x04001F40 RID: 8000
			Length,
			// Token: 0x04001F41 RID: 8001
			MinLength,
			// Token: 0x04001F42 RID: 8002
			MaxLength,
			// Token: 0x04001F43 RID: 8003
			Enumeration,
			// Token: 0x04001F44 RID: 8004
			Pattern,
			// Token: 0x04001F45 RID: 8005
			WhiteSpace,
			// Token: 0x04001F46 RID: 8006
			AppInfo,
			// Token: 0x04001F47 RID: 8007
			Documentation,
			// Token: 0x04001F48 RID: 8008
			Redefine
		}

		// Token: 0x020004AE RID: 1198
		// (Invoke) Token: 0x06003187 RID: 12679
		private delegate void XsdBuildFunction(XsdBuilder builder, string value);

		// Token: 0x020004AF RID: 1199
		// (Invoke) Token: 0x0600318B RID: 12683
		private delegate void XsdInitFunction(XsdBuilder builder, string value);

		// Token: 0x020004B0 RID: 1200
		// (Invoke) Token: 0x0600318F RID: 12687
		private delegate void XsdEndChildFunction(XsdBuilder builder);

		// Token: 0x020004B1 RID: 1201
		private sealed class XsdAttributeEntry
		{
			// Token: 0x06003192 RID: 12690 RVA: 0x0012031D File Offset: 0x0011E51D
			public XsdAttributeEntry(SchemaNames.Token a, XsdBuilder.XsdBuildFunction build)
			{
				this.Attribute = a;
				this.BuildFunc = build;
			}

			// Token: 0x04001F49 RID: 8009
			public SchemaNames.Token Attribute;

			// Token: 0x04001F4A RID: 8010
			public XsdBuilder.XsdBuildFunction BuildFunc;
		}

		// Token: 0x020004B2 RID: 1202
		private sealed class XsdEntry
		{
			// Token: 0x06003193 RID: 12691 RVA: 0x00120333 File Offset: 0x0011E533
			public XsdEntry(SchemaNames.Token n, XsdBuilder.State state, XsdBuilder.State[] nextStates, XsdBuilder.XsdAttributeEntry[] attributes, XsdBuilder.XsdInitFunction init, XsdBuilder.XsdEndChildFunction end, bool parseContent)
			{
				this.Name = n;
				this.CurrentState = state;
				this.NextStates = nextStates;
				this.Attributes = attributes;
				this.InitFunc = init;
				this.EndChildFunc = end;
				this.ParseContent = parseContent;
			}

			// Token: 0x04001F4B RID: 8011
			public SchemaNames.Token Name;

			// Token: 0x04001F4C RID: 8012
			public XsdBuilder.State CurrentState;

			// Token: 0x04001F4D RID: 8013
			public XsdBuilder.State[] NextStates;

			// Token: 0x04001F4E RID: 8014
			public XsdBuilder.XsdAttributeEntry[] Attributes;

			// Token: 0x04001F4F RID: 8015
			public XsdBuilder.XsdInitFunction InitFunc;

			// Token: 0x04001F50 RID: 8016
			public XsdBuilder.XsdEndChildFunction EndChildFunc;

			// Token: 0x04001F51 RID: 8017
			public bool ParseContent;
		}

		// Token: 0x020004B3 RID: 1203
		private class BuilderNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06003194 RID: 12692 RVA: 0x00120370 File Offset: 0x0011E570
			public BuilderNamespaceManager(XmlNamespaceManager nsMgr, XmlReader reader)
			{
				this.nsMgr = nsMgr;
				this.reader = reader;
			}

			// Token: 0x06003195 RID: 12693 RVA: 0x00120388 File Offset: 0x0011E588
			public override string LookupNamespace(string prefix)
			{
				string text = this.nsMgr.LookupNamespace(prefix);
				if (text == null)
				{
					text = this.reader.LookupNamespace(prefix);
				}
				return text;
			}

			// Token: 0x04001F52 RID: 8018
			private XmlNamespaceManager nsMgr;

			// Token: 0x04001F53 RID: 8019
			private XmlReader reader;
		}
	}
}
