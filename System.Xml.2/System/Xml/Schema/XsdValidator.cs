using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020002D2 RID: 722
	internal sealed class XsdValidator : BaseValidator
	{
		// Token: 0x06002B33 RID: 11059 RVA: 0x000E1A61 File Offset: 0x000DFC61
		internal XsdValidator(BaseValidator validator) : base(validator)
		{
			this.Init();
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000E1A77 File Offset: 0x000DFC77
		internal XsdValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling) : base(reader, schemaCollection, eventHandling)
		{
			this.Init();
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000E1A90 File Offset: 0x000DFC90
		private void Init()
		{
			this.nsManager = this.reader.NamespaceManager;
			if (this.nsManager == null)
			{
				this.nsManager = new XmlNamespaceManager(base.NameTable);
				this.bManageNamespaces = true;
			}
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.attPresence = new Hashtable();
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
			this.processContents = XmlSchemaContentProcessing.Strict;
			this.Push(XmlQualifiedName.Empty);
			this.NsXmlNs = base.NameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXs = base.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = base.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = base.NameTable.Add("type");
			this.XsiNil = base.NameTable.Add("nil");
			this.XsiSchemaLocation = base.NameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = base.NameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = base.NameTable.Add("schema");
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000E1BC8 File Offset: 0x000DFDC8
		public override void Validate()
		{
			if (this.IsInlineSchemaStarted)
			{
				this.ProcessInlineSchema();
				return;
			}
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType - XmlNodeType.Text > 1)
				{
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
						base.ValidateWhitespace();
						return;
					case XmlNodeType.SignificantWhitespace:
						break;
					case XmlNodeType.EndElement:
						goto IL_5E;
					default:
						return;
					}
				}
				base.ValidateText();
				return;
			}
			this.ValidateElement();
			if (!this.reader.IsEmptyElement)
			{
				return;
			}
			IL_5E:
			this.ValidateEndElement();
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000E1C39 File Offset: 0x000DFE39
		public override void CompleteValidation()
		{
			this.CheckForwardRefs();
		}

		// Token: 0x1700097F RID: 2431
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000E1C41 File Offset: 0x000DFE41
		public ValidationState Context
		{
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000E1C4A File Offset: 0x000DFE4A
		public static XmlSchemaDatatype DtQName
		{
			get
			{
				return XsdValidator.dtQName;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000E1C51 File Offset: 0x000DFE51
		private bool IsInlineSchemaStarted
		{
			get
			{
				return this.inlineSchemaParser != null;
			}
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000E1C5C File Offset: 0x000DFE5C
		private void ProcessInlineSchema()
		{
			if (!this.inlineSchemaParser.ParseReaderNode())
			{
				this.inlineSchemaParser.FinishParsing();
				XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
				if (xmlSchema != null && xmlSchema.ErrorCount == 0)
				{
					try
					{
						SchemaInfo schemaInfo = new SchemaInfo();
						schemaInfo.SchemaType = SchemaType.XSD;
						string text = (xmlSchema.TargetNamespace == null) ? string.Empty : xmlSchema.TargetNamespace;
						if (!base.SchemaInfo.TargetNamespaces.ContainsKey(text) && base.SchemaCollection.Add(text, schemaInfo, xmlSchema, true) != null)
						{
							base.SchemaInfo.Add(schemaInfo, base.EventHandler);
						}
					}
					catch (XmlSchemaException ex)
					{
						base.SendValidationEvent("Sch_CannotLoadSchema", new string[]
						{
							base.BaseUri.AbsoluteUri,
							ex.Message
						}, XmlSeverityType.Error);
					}
				}
				this.inlineSchemaParser = null;
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000E1D44 File Offset: 0x000DFF44
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, this.reader.NamespaceURI);
			object particle = this.ValidateChildElement();
			if (this.IsXSDRoot(this.elementName.Name, this.elementName.Namespace) && this.reader.Depth > 0)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XSD, base.NameTable, base.SchemaNames, base.EventHandler);
				this.inlineSchemaParser.StartParsing(this.reader, null);
				this.ProcessInlineSchema();
				return;
			}
			this.ProcessElement(particle);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000E1DE4 File Offset: 0x000DFFE4
		private object ValidateChildElement()
		{
			object obj = null;
			int num = 0;
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					base.SendValidationEvent("Sch_ContentInNill", this.elementName.ToString());
					return null;
				}
				obj = this.context.ElementDecl.ContentValidator.ValidateElement(this.elementName, this.context, out num);
				if (obj == null)
				{
					this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
					if (num == -2)
					{
						base.SendValidationEvent("Sch_AllElement", this.elementName.ToString());
					}
					XmlSchemaValidator.ElementValidationError(this.elementName, this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
			}
			return obj;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000E1EC4 File Offset: 0x000E00C4
		private void ProcessElement(object particle)
		{
			SchemaElementDecl schemaElementDecl = this.FastGetElementDecl(particle);
			this.Push(this.elementName);
			if (this.bManageNamespaces)
			{
				this.nsManager.PushScope();
			}
			XmlQualifiedName xmlQualifiedName;
			string text;
			this.ProcessXsiAttributes(out xmlQualifiedName, out text);
			if (this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (schemaElementDecl == null || !xmlQualifiedName.IsEmpty || text != null)
				{
					schemaElementDecl = this.ThoroughGetElementDecl(schemaElementDecl, xmlQualifiedName, text);
				}
				if (schemaElementDecl == null)
				{
					if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
					{
						base.SendValidationEvent("Sch_UndeclaredElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
					else
					{
						base.SendValidationEvent("Sch_NoElementSchemaFound", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace), XmlSeverityType.Warning);
					}
				}
			}
			this.context.ElementDecl = schemaElementDecl;
			this.ValidateStartElementIdentityConstraints();
			this.ValidateStartElement();
			if (this.context.ElementDecl != null)
			{
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = (this.processContents != XmlSchemaContentProcessing.Skip);
				this.context.ElementDecl.ContentValidator.InitValidation(this.context);
			}
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000E1FE4 File Offset: 0x000E01E4
		private void ProcessXsiAttributes(out XmlQualifiedName xsiType, out string xsiNil)
		{
			string[] array = null;
			string text = null;
			xsiType = XmlQualifiedName.Empty;
			xsiNil = null;
			if (this.reader.Depth == 0)
			{
				this.LoadSchema(string.Empty, null);
				foreach (string uri in this.nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml).Values)
				{
					this.LoadSchema(uri, null);
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, this.NsXmlNs))
					{
						this.LoadSchema(this.reader.Value, null);
						if (this.bManageNamespaces)
						{
							this.nsManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
					}
					else if (Ref.Equal(namespaceURI, this.NsXsi))
					{
						if (Ref.Equal(localName, this.XsiSchemaLocation))
						{
							array = (string[])XsdValidator.dtStringArray.ParseValue(this.reader.Value, base.NameTable, this.nsManager);
						}
						else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
						{
							text = this.reader.Value;
						}
						else if (Ref.Equal(localName, this.XsiType))
						{
							xsiType = (XmlQualifiedName)XsdValidator.dtQName.ParseValue(this.reader.Value, base.NameTable, this.nsManager);
						}
						else if (Ref.Equal(localName, this.XsiNil))
						{
							xsiNil = this.reader.Value;
						}
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			if (text != null)
			{
				this.LoadSchema(string.Empty, text);
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i += 2)
				{
					this.LoadSchema(array[i], array[i + 1]);
				}
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000E2208 File Offset: 0x000E0408
		private void ValidateEndElement()
		{
			if (this.bManageNamespaces)
			{
				this.nsManager.PopScope();
			}
			if (this.context.ElementDecl != null)
			{
				if (!this.context.IsNill)
				{
					if (this.context.NeedValidateChildren && !this.context.ElementDecl.ContentValidator.CompleteValidation(this.context))
					{
						XmlSchemaValidator.CompleteValidationError(this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
					}
					if (this.checkDatatype && !this.context.IsNill)
					{
						string text = (!this.hasSibling) ? this.textString : this.textValue.ToString();
						if (text.Length != 0 || this.context.ElementDecl.DefaultValueTyped == null)
						{
							this.CheckValue(text, null);
							this.checkDatatype = false;
						}
					}
				}
				if (this.HasIdentityConstraints)
				{
					this.EndElementIdentityConstraints();
				}
			}
			this.Pop();
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000E231C File Offset: 0x000E051C
		private SchemaElementDecl FastGetElementDecl(object particle)
		{
			SchemaElementDecl result = null;
			if (particle != null)
			{
				XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					result = xmlSchemaElement.ElementDecl;
				}
				else
				{
					XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
					this.processContents = xmlSchemaAny.ProcessContentsCorrect;
				}
			}
			return result;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000E2358 File Offset: 0x000E0558
		private SchemaElementDecl ThoroughGetElementDecl(SchemaElementDecl elementDecl, XmlQualifiedName xsiType, string xsiNil)
		{
			if (elementDecl == null)
			{
				elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			}
			if (elementDecl != null)
			{
				if (xsiType.IsEmpty)
				{
					if (elementDecl.IsAbstract)
					{
						base.SendValidationEvent("Sch_AbstractElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
						elementDecl = null;
					}
				}
				else if (xsiNil != null && xsiNil.Equals("true"))
				{
					base.SendValidationEvent("Sch_XsiNilAndType");
				}
				else
				{
					SchemaElementDecl elementDecl2;
					if (!this.schemaInfo.ElementDeclsByType.TryGetValue(xsiType, out elementDecl2) && xsiType.Namespace == this.NsXs)
					{
						XmlSchemaSimpleType simpleTypeFromXsdType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName(xsiType.Name, this.NsXs));
						if (simpleTypeFromXsdType != null)
						{
							elementDecl2 = simpleTypeFromXsdType.ElementDecl;
						}
					}
					if (elementDecl2 == null)
					{
						base.SendValidationEvent("Sch_XsiTypeNotFound", xsiType.ToString());
						elementDecl = null;
					}
					else if (!XmlSchemaType.IsDerivedFrom(elementDecl2.SchemaType, elementDecl.SchemaType, elementDecl.Block))
					{
						base.SendValidationEvent("Sch_XsiTypeBlockedEx", new string[]
						{
							xsiType.ToString(),
							XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
						});
						elementDecl = null;
					}
					else
					{
						elementDecl = elementDecl2;
					}
				}
				if (elementDecl != null && elementDecl.IsNillable)
				{
					if (xsiNil != null)
					{
						this.context.IsNill = XmlConvert.ToBoolean(xsiNil);
						if (this.context.IsNill && elementDecl.DefaultValueTyped != null)
						{
							base.SendValidationEvent("Sch_XsiNilAndFixed");
						}
					}
				}
				else if (xsiNil != null)
				{
					base.SendValidationEvent("Sch_InvalidXsiNill");
				}
			}
			return elementDecl;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000E24F0 File Offset: 0x000E06F0
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.IsAbstract)
				{
					base.SendValidationEvent("Sch_AbstractElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				this.reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				if (this.reader.IsEmptyElement && !this.context.IsNill && this.context.ElementDecl.DefaultValueTyped != null)
				{
					this.reader.TypedValueObject = this.UnWrapUnion(this.context.ElementDecl.DefaultValueTyped);
					this.context.IsNill = true;
				}
				else
				{
					this.reader.TypedValueObject = null;
				}
				if (this.context.ElementDecl.HasRequiredAttribute || this.HasIdentityConstraints)
				{
					this.attPresence.Clear();
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.reader.NamespaceURI != this.NsXmlNs && this.reader.NamespaceURI != this.NsXsi)
					{
						try
						{
							this.reader.SchemaTypeObject = null;
							XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.reader.LocalName, this.reader.NamespaceURI);
							bool flag = this.processContents == XmlSchemaContentProcessing.Skip;
							SchemaAttDef attributeXsd = this.schemaInfo.GetAttributeXsd(this.context.ElementDecl, xmlQualifiedName, ref flag);
							if (attributeXsd != null)
							{
								if (this.context.ElementDecl != null && (this.context.ElementDecl.HasRequiredAttribute || this.startIDConstraint != -1))
								{
									this.attPresence.Add(attributeXsd.Name, attributeXsd);
								}
								this.reader.SchemaTypeObject = attributeXsd.SchemaType;
								if (attributeXsd.Datatype != null)
								{
									this.CheckValue(this.reader.Value, attributeXsd);
								}
								if (this.HasIdentityConstraints)
								{
									this.AttributeIdentityConstraints(this.reader.LocalName, this.reader.NamespaceURI, this.reader.TypedValueObject, this.reader.Value, attributeXsd);
								}
							}
							else if (!flag)
							{
								if (this.context.ElementDecl == null && this.processContents == XmlSchemaContentProcessing.Strict && xmlQualifiedName.Namespace.Length != 0 && this.schemaInfo.Contains(xmlQualifiedName.Namespace))
								{
									base.SendValidationEvent("Sch_UndeclaredAttribute", xmlQualifiedName.ToString());
								}
								else
								{
									base.SendValidationEvent("Sch_NoAttributeSchemaFound", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
								}
							}
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
							base.SendValidationEvent(ex);
						}
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000E27EC File Offset: 0x000E09EC
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < this.context.ElementDecl.DefaultAttDefs.Count; i++)
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)this.context.ElementDecl.DefaultAttDefs[i];
					this.reader.AddDefaultAttribute(schemaAttDef);
					if (this.HasIdentityConstraints && !this.attPresence.Contains(schemaAttDef.Name))
					{
						this.AttributeIdentityConstraints(schemaAttDef.Name.Name, schemaAttDef.Name.Namespace, this.UnWrapUnion(schemaAttDef.DefaultValueTyped), schemaAttDef.DefaultValueRaw, schemaAttDef);
					}
				}
			}
			if (this.context.ElementDecl.HasRequiredAttribute)
			{
				try
				{
					this.context.ElementDecl.CheckAttributes(this.attPresence, this.reader.StandAlone);
				}
				catch (XmlSchemaException ex)
				{
					ex.SetSource(this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					base.SendValidationEvent(ex);
				}
			}
			if (this.context.ElementDecl.Datatype != null)
			{
				this.checkDatatype = true;
				this.hasSibling = false;
				this.textString = string.Empty;
				this.textValue.Length = 0;
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000E2954 File Offset: 0x000E0B54
		private void LoadSchemaFromLocation(string uri, string url)
		{
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = base.XmlResolver.ResolveUri(base.BaseUri, url);
				Stream input = (Stream)base.XmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), input, base.NameTable);
				Parser parser = new Parser(SchemaType.XSD, base.NameTable, base.SchemaNames, base.EventHandler);
				parser.XmlResolver = base.XmlResolver;
				SchemaType schemaType = parser.Parse(xmlReader, uri);
				SchemaInfo schemaInfo = new SchemaInfo();
				schemaInfo.SchemaType = schemaType;
				if (schemaType == SchemaType.XSD)
				{
					if (base.SchemaCollection.EventHandler == null)
					{
						base.SchemaCollection.EventHandler = base.EventHandler;
					}
					base.SchemaCollection.Add(uri, schemaInfo, parser.XmlSchema, true);
				}
				base.SchemaInfo.Add(schemaInfo, base.EventHandler);
				while (xmlReader.Read())
				{
				}
			}
			catch (XmlSchemaException ex)
			{
				base.SendValidationEvent("Sch_CannotLoadSchema", new string[]
				{
					uri,
					ex.Message
				}, XmlSeverityType.Error);
			}
			catch (Exception ex2)
			{
				base.SendValidationEvent("Sch_CannotLoadSchema", new string[]
				{
					uri,
					ex2.Message
				}, XmlSeverityType.Warning);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000E2AD8 File Offset: 0x000E0CD8
		private void LoadSchema(string uri, string url)
		{
			if (base.XmlResolver == null)
			{
				return;
			}
			if (base.SchemaInfo.TargetNamespaces.ContainsKey(uri) && this.nsManager.LookupPrefix(uri) != null)
			{
				return;
			}
			SchemaInfo schemaInfo = null;
			if (base.SchemaCollection != null)
			{
				schemaInfo = base.SchemaCollection.GetSchemaInfo(uri);
			}
			if (schemaInfo == null)
			{
				if (url != null)
				{
					this.LoadSchemaFromLocation(uri, url);
				}
				return;
			}
			if (schemaInfo.SchemaType != SchemaType.XSD)
			{
				throw new XmlException("Xml_MultipleValidaitonTypes", string.Empty, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
			}
			base.SchemaInfo.Add(schemaInfo, base.EventHandler);
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000E2B77 File Offset: 0x000E0D77
		private bool HasSchema
		{
			get
			{
				return this.schemaInfo.SchemaType > SchemaType.None;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000E2B87 File Offset: 0x000E0D87
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000E2BB0 File Offset: 0x000E0DB0
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.FindId(name) != null)
				{
					base.SendValidationEvent("Sch_DupId", name);
					return;
				}
				this.AddID(name, this.context.LocalName);
				return;
			case XmlTokenizedType.IDREF:
				if (this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				BaseValidator.ProcessEntity(this.schemaInfo, name, this, base.EventHandler, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
				break;
			default:
				return;
			}
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000E2C68 File Offset: 0x000E0E68
		private void CheckValue(string value, SchemaAttDef attdef)
		{
			try
			{
				this.reader.TypedValueObject = null;
				bool flag = attdef != null;
				XmlSchemaDatatype xmlSchemaDatatype = flag ? attdef.Datatype : this.context.ElementDecl.Datatype;
				if (xmlSchemaDatatype != null)
				{
					object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, this.nsManager, true);
					XmlTokenizedType tokenizedType = xmlSchemaDatatype.TokenizedType;
					if (tokenizedType == XmlTokenizedType.ENTITY || tokenizedType == XmlTokenizedType.ID || tokenizedType == XmlTokenizedType.IDREF)
					{
						if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.List)
						{
							string[] array = (string[])obj;
							for (int i = 0; i < array.Length; i++)
							{
								this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, array[i]);
							}
						}
						else
						{
							this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, (string)obj);
						}
					}
					SchemaDeclBase schemaDeclBase = flag ? attdef : this.context.ElementDecl;
					if (!schemaDeclBase.CheckValue(obj))
					{
						if (flag)
						{
							base.SendValidationEvent("Sch_FixedAttributeValue", attdef.Name.ToString());
						}
						else
						{
							base.SendValidationEvent("Sch_FixedElementValue", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
						}
					}
					if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.Union)
					{
						obj = this.UnWrapUnion(obj);
					}
					this.reader.TypedValueObject = obj;
				}
			}
			catch (XmlSchemaException)
			{
				if (attdef != null)
				{
					base.SendValidationEvent("Sch_AttributeValueDataType", attdef.Name.ToString());
				}
				else
				{
					base.SendValidationEvent("Sch_ElementValueDataType", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
			}
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000E2DF8 File Offset: 0x000E0FF8
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000E2E1A File Offset: 0x000E101A
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000E2E32 File Offset: 0x000E1032
		public bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000E2E50 File Offset: 0x000E1050
		private void Push(XmlQualifiedName elementName)
		{
			this.context = (ValidationState)this.validationStack.Push();
			if (this.context == null)
			{
				this.context = new ValidationState();
				this.validationStack.AddToTop(this.context);
			}
			this.context.LocalName = elementName.Name;
			this.context.Namespace = elementName.Namespace;
			this.context.HasMatched = false;
			this.context.IsNill = false;
			this.context.ProcessContents = this.processContents;
			this.context.NeedValidateChildren = false;
			this.context.Constr = null;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000E2EFC File Offset: 0x000E10FC
		private void Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				if (this.startIDConstraint == this.validationStack.Length)
				{
					this.startIDConstraint = -1;
				}
				this.context = (ValidationState)this.validationStack.Peek();
				this.processContents = this.context.ProcessContents;
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000E2F64 File Offset: 0x000E1164
		private void CheckForwardRefs()
		{
			IdRefNode next;
			for (IdRefNode idRefNode = this.idRefListHead; idRefNode != null; idRefNode = next)
			{
				if (this.FindId(idRefNode.Id) == null)
				{
					base.SendValidationEvent(new XmlSchemaException("Sch_UndeclaredId", idRefNode.Id, this.reader.BaseURI, idRefNode.LineNo, idRefNode.LinePos));
				}
				next = idRefNode.Next;
				idRefNode.Next = null;
			}
			this.idRefListHead = null;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000E2FCF File Offset: 0x000E11CF
		private void ValidateStartElementIdentityConstraints()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.Constraints != null)
				{
					this.AddIdentityConstraints();
				}
				if (this.HasIdentityConstraints)
				{
					this.ElementIdentityConstraints();
				}
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000E3004 File Offset: 0x000E1204
		private bool HasIdentityConstraints
		{
			get
			{
				return this.startIDConstraint != -1;
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000E3014 File Offset: 0x000E1214
		private void AddIdentityConstraints()
		{
			this.context.Constr = new ConstraintStruct[this.context.ElementDecl.Constraints.Length];
			int num = 0;
			for (int i = 0; i < this.context.ElementDecl.Constraints.Length; i++)
			{
				this.context.Constr[num++] = new ConstraintStruct(this.context.ElementDecl.Constraints[i]);
			}
			for (int j = 0; j < this.context.Constr.Length; j++)
			{
				if (this.context.Constr[j].constraint.Role == CompiledIdentityConstraint.ConstraintRole.Keyref)
				{
					bool flag = false;
					for (int k = this.validationStack.Length - 1; k >= ((this.startIDConstraint >= 0) ? this.startIDConstraint : (this.validationStack.Length - 1)); k--)
					{
						if (((ValidationState)this.validationStack[k]).Constr != null)
						{
							ConstraintStruct[] constr = ((ValidationState)this.validationStack[k]).Constr;
							for (int l = 0; l < constr.Length; l++)
							{
								if (constr[l].constraint.name == this.context.Constr[j].constraint.refer)
								{
									flag = true;
									if (constr[l].keyrefTable == null)
									{
										constr[l].keyrefTable = new Hashtable();
									}
									this.context.Constr[j].qualifiedTable = constr[l].keyrefTable;
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag)
					{
						base.SendValidationEvent("Sch_RefNotInScope", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
				}
			}
			if (this.startIDConstraint == -1)
			{
				this.startIDConstraint = this.validationStack.Length - 1;
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000E3200 File Offset: 0x000E1400
		private void ElementIdentityConstraints()
		{
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						if (constr[j].axisSelector.MoveToStartElement(this.reader.LocalName, this.reader.NamespaceURI))
						{
							constr[j].axisSelector.PushKS(base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
						}
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToStartElement(this.reader.LocalName, this.reader.NamespaceURI) && this.context.ElementDecl != null)
							{
								if (this.context.ElementDecl.Datatype == null)
								{
									base.SendValidationEvent("Sch_FieldSimpleTypeExpected", this.reader.LocalName);
								}
								else
								{
									locatedActiveAxis.isMatched = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000E334C File Offset: 0x000E154C
		private void AttributeIdentityConstraints(string name, string ns, object obj, string sobj, SchemaAttDef attdef)
		{
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToAttribute(name, ns))
							{
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									base.SendValidationEvent("Sch_FieldSingleValueExpected", name);
								}
								else if (attdef != null && attdef.Datatype != null)
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(obj, sobj, attdef.Datatype);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000E3450 File Offset: 0x000E1650
		private object UnWrapUnion(object typedValue)
		{
			XsdSimpleValue xsdSimpleValue = typedValue as XsdSimpleValue;
			if (xsdSimpleValue != null)
			{
				typedValue = xsdSimpleValue.TypedValue;
			}
			return typedValue;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000E3470 File Offset: 0x000E1670
		private void EndElementIdentityConstraints()
		{
			for (int i = this.validationStack.Length - 1; i >= this.startIDConstraint; i--)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.isMatched)
							{
								locatedActiveAxis.isMatched = false;
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									base.SendValidationEvent("Sch_FieldSingleValueExpected", this.reader.LocalName);
								}
								else
								{
									string text = (!this.hasSibling) ? this.textString : this.textValue.ToString();
									if (this.reader.TypedValueObject != null && text.Length != 0)
									{
										locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(this.reader.TypedValueObject, text, this.context.ElementDecl.Datatype);
									}
								}
							}
							locatedActiveAxis.EndElement(this.reader.LocalName, this.reader.NamespaceURI);
						}
						if (constr[j].axisSelector.EndElement(this.reader.LocalName, this.reader.NamespaceURI))
						{
							KeySequence keySequence = constr[j].axisSelector.PopKS();
							switch (constr[j].constraint.Role)
							{
							case CompiledIdentityConstraint.ConstraintRole.Unique:
								if (keySequence.IsQualified())
								{
									if (constr[j].qualifiedTable.Contains(keySequence))
									{
										base.SendValidationEvent(new XmlSchemaException("Sch_DuplicateKey", new string[]
										{
											keySequence.ToString(),
											constr[j].constraint.name.ToString()
										}, this.reader.BaseURI, keySequence.PosLine, keySequence.PosCol));
									}
									else
									{
										constr[j].qualifiedTable.Add(keySequence, keySequence);
									}
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Key:
								if (!keySequence.IsQualified())
								{
									base.SendValidationEvent(new XmlSchemaException("Sch_MissingKey", constr[j].constraint.name.ToString(), this.reader.BaseURI, keySequence.PosLine, keySequence.PosCol));
								}
								else if (constr[j].qualifiedTable.Contains(keySequence))
								{
									base.SendValidationEvent(new XmlSchemaException("Sch_DuplicateKey", new string[]
									{
										keySequence.ToString(),
										constr[j].constraint.name.ToString()
									}, this.reader.BaseURI, keySequence.PosLine, keySequence.PosCol));
								}
								else
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Keyref:
								if (constr[j].qualifiedTable != null && keySequence.IsQualified() && !constr[j].qualifiedTable.Contains(keySequence))
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							}
						}
					}
				}
			}
			ConstraintStruct[] constr2 = ((ValidationState)this.validationStack[this.validationStack.Length - 1]).Constr;
			if (constr2 != null)
			{
				for (int l = 0; l < constr2.Length; l++)
				{
					if (constr2[l].constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref && constr2[l].keyrefTable != null)
					{
						foreach (object obj in constr2[l].keyrefTable.Keys)
						{
							KeySequence keySequence2 = (KeySequence)obj;
							if (!constr2[l].qualifiedTable.Contains(keySequence2))
							{
								base.SendValidationEvent(new XmlSchemaException("Sch_UnresolvedKeyref", new string[]
								{
									keySequence2.ToString(),
									constr2[l].constraint.name.ToString()
								}, this.reader.BaseURI, keySequence2.PosLine, keySequence2.PosCol));
							}
						}
					}
				}
			}
		}

		// Token: 0x040012C9 RID: 4809
		private int startIDConstraint = -1;

		// Token: 0x040012CA RID: 4810
		private const int STACK_INCREMENT = 10;

		// Token: 0x040012CB RID: 4811
		private HWStack validationStack;

		// Token: 0x040012CC RID: 4812
		private Hashtable attPresence;

		// Token: 0x040012CD RID: 4813
		private XmlNamespaceManager nsManager;

		// Token: 0x040012CE RID: 4814
		private bool bManageNamespaces;

		// Token: 0x040012CF RID: 4815
		private Hashtable IDs;

		// Token: 0x040012D0 RID: 4816
		private IdRefNode idRefListHead;

		// Token: 0x040012D1 RID: 4817
		private Parser inlineSchemaParser;

		// Token: 0x040012D2 RID: 4818
		private XmlSchemaContentProcessing processContents;

		// Token: 0x040012D3 RID: 4819
		private static readonly XmlSchemaDatatype dtCDATA = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);

		// Token: 0x040012D4 RID: 4820
		private static readonly XmlSchemaDatatype dtQName = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName);

		// Token: 0x040012D5 RID: 4821
		private static readonly XmlSchemaDatatype dtStringArray = XsdValidator.dtCDATA.DeriveByList(null);

		// Token: 0x040012D6 RID: 4822
		private string NsXmlNs;

		// Token: 0x040012D7 RID: 4823
		private string NsXs;

		// Token: 0x040012D8 RID: 4824
		private string NsXsi;

		// Token: 0x040012D9 RID: 4825
		private string XsiType;

		// Token: 0x040012DA RID: 4826
		private string XsiNil;

		// Token: 0x040012DB RID: 4827
		private string XsiSchemaLocation;

		// Token: 0x040012DC RID: 4828
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x040012DD RID: 4829
		private string XsdSchema;
	}
}
