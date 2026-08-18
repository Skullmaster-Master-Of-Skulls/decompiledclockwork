using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000268 RID: 616
	internal sealed class XdrValidator : BaseValidator
	{
		// Token: 0x060024EF RID: 9455 RVA: 0x000CA8E9 File Offset: 0x000C8AE9
		internal XdrValidator(BaseValidator validator) : base(validator)
		{
			this.Init();
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000CA903 File Offset: 0x000C8B03
		internal XdrValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling) : base(reader, schemaCollection, eventHandling)
		{
			this.Init();
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000CA920 File Offset: 0x000C8B20
		private void Init()
		{
			this.nsManager = this.reader.NamespaceManager;
			if (this.nsManager == null)
			{
				this.nsManager = new XmlNamespaceManager(base.NameTable);
				this.isProcessContents = true;
			}
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.name = XmlQualifiedName.Empty;
			this.attPresence = new Hashtable();
			this.Push(XmlQualifiedName.Empty);
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000CA9AC File Offset: 0x000C8BAC
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

		// Token: 0x060024F3 RID: 9459 RVA: 0x000CAA20 File Offset: 0x000C8C20
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, XmlSchemaDatatype.XdrCanonizeUri(this.reader.NamespaceURI, base.NameTable, base.SchemaNames));
			this.ValidateChildElement();
			if (base.SchemaNames.IsXDRRoot(this.elementName.Name, this.elementName.Namespace) && this.reader.Depth > 0)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XDR, base.NameTable, base.SchemaNames, base.EventHandler);
				this.inlineSchemaParser.StartParsing(this.reader, null);
				this.inlineSchemaParser.ParseReaderNode();
				return;
			}
			this.ProcessElement();
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000CAADC File Offset: 0x000C8CDC
		private void ValidateChildElement()
		{
			if (this.context.NeedValidateChildren)
			{
				int num = 0;
				this.context.ElementDecl.ContentValidator.ValidateElement(this.elementName, this.context, out num);
				if (num < 0)
				{
					XmlSchemaValidator.ElementValidationError(this.elementName, this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000CAB5F File Offset: 0x000C8D5F
		private bool IsInlineSchemaStarted
		{
			get
			{
				return this.inlineSchemaParser != null;
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000CAB6C File Offset: 0x000C8D6C
		private void ProcessInlineSchema()
		{
			if (!this.inlineSchemaParser.ParseReaderNode())
			{
				this.inlineSchemaParser.FinishParsing();
				SchemaInfo xdrSchema = this.inlineSchemaParser.XdrSchema;
				if (xdrSchema != null && xdrSchema.ErrorCount == 0)
				{
					foreach (string ns in xdrSchema.TargetNamespaces.Keys)
					{
						if (!this.schemaInfo.HasSchema(ns))
						{
							this.schemaInfo.Add(xdrSchema, base.EventHandler);
							base.SchemaCollection.Add(ns, xdrSchema, null, false);
							break;
						}
					}
				}
				this.inlineSchemaParser = null;
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000CAC2C File Offset: 0x000C8E2C
		private void ProcessElement()
		{
			this.Push(this.elementName);
			if (this.isProcessContents)
			{
				this.nsManager.PopScope();
			}
			this.context.ElementDecl = this.ThoroughGetElementDecl();
			if (this.context.ElementDecl != null)
			{
				this.ValidateStartElement();
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = true;
				this.context.ElementDecl.ContentValidator.InitValidation(this.context);
			}
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000CACAC File Offset: 0x000C8EAC
		private void ValidateEndElement()
		{
			if (this.isProcessContents)
			{
				this.nsManager.PopScope();
			}
			if (this.context.ElementDecl != null)
			{
				if (this.context.NeedValidateChildren && !this.context.ElementDecl.ContentValidator.CompleteValidation(this.context))
				{
					XmlSchemaValidator.CompleteValidationError(this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
				if (this.checkDatatype)
				{
					string value = (!this.hasSibling) ? this.textString : this.textValue.ToString();
					this.CheckValue(value, null);
					this.checkDatatype = false;
					this.textValue.Length = 0;
					this.textString = string.Empty;
				}
			}
			this.Pop();
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000CAD90 File Offset: 0x000C8F90
		private SchemaElementDecl ThoroughGetElementDecl()
		{
			if (this.reader.Depth == 0)
			{
				this.LoadSchema(string.Empty);
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						this.LoadSchema(this.reader.Value);
						if (this.isProcessContents)
						{
							this.nsManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
					}
					if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
					{
						this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			SchemaElementDecl elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			if (elementDecl == null && this.schemaInfo.TargetNamespaces.ContainsKey(this.context.Namespace))
			{
				base.SendValidationEvent("Sch_UndeclaredElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return elementDecl;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000CAF04 File Offset: 0x000C9104
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.SchemaType != null)
				{
					this.reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				}
				else
				{
					this.reader.SchemaTypeObject = this.context.ElementDecl.Datatype;
				}
				if (this.reader.IsEmptyElement && !this.context.IsNill && this.context.ElementDecl.DefaultValueTyped != null)
				{
					this.reader.TypedValueObject = this.context.ElementDecl.DefaultValueTyped;
					this.context.IsNill = true;
				}
				if (this.context.ElementDecl.HasRequiredAttribute)
				{
					this.attPresence.Clear();
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.reader.NamespaceURI != base.SchemaNames.NsXmlNs)
					{
						try
						{
							this.reader.SchemaTypeObject = null;
							SchemaAttDef attributeXdr = this.schemaInfo.GetAttributeXdr(this.context.ElementDecl, this.QualifiedName(this.reader.LocalName, this.reader.NamespaceURI));
							if (attributeXdr != null)
							{
								if (this.context.ElementDecl != null && this.context.ElementDecl.HasRequiredAttribute)
								{
									this.attPresence.Add(attributeXdr.Name, attributeXdr);
								}
								this.reader.SchemaTypeObject = ((attributeXdr.SchemaType != null) ? attributeXdr.SchemaType : attributeXdr.Datatype);
								if (attributeXdr.Datatype != null)
								{
									string value = this.reader.Value;
									this.CheckValue(value, attributeXdr);
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

		// Token: 0x060024FB RID: 9467 RVA: 0x000CB118 File Offset: 0x000C9318
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < this.context.ElementDecl.DefaultAttDefs.Count; i++)
				{
					this.reader.AddDefaultAttribute((SchemaAttDef)this.context.ElementDecl.DefaultAttDefs[i]);
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

		// Token: 0x060024FC RID: 9468 RVA: 0x000CB22C File Offset: 0x000C942C
		private void LoadSchemaFromLocation(string uri)
		{
			if (!XdrBuilder.IsXdrSchema(uri))
			{
				return;
			}
			string relativeUri = uri.Substring("x-schema:".Length);
			XmlReader xmlReader = null;
			SchemaInfo schemaInfo = null;
			try
			{
				Uri uri2 = base.XmlResolver.ResolveUri(base.BaseUri, relativeUri);
				Stream input = (Stream)base.XmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), input, base.NameTable);
				((XmlTextReader)xmlReader).XmlResolver = base.XmlResolver;
				Parser parser = new Parser(SchemaType.XDR, base.NameTable, base.SchemaNames, base.EventHandler);
				parser.XmlResolver = base.XmlResolver;
				parser.Parse(xmlReader, uri);
				while (xmlReader.Read())
				{
				}
				schemaInfo = parser.XdrSchema;
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
			if (schemaInfo != null && schemaInfo.ErrorCount == 0)
			{
				this.schemaInfo.Add(schemaInfo, base.EventHandler);
				base.SchemaCollection.Add(uri, schemaInfo, null, false);
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000CB390 File Offset: 0x000C9590
		private void LoadSchema(string uri)
		{
			if (this.schemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return;
			}
			if (base.XmlResolver == null)
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
				this.LoadSchemaFromLocation(uri);
				return;
			}
			if (schemaInfo.SchemaType != SchemaType.XDR)
			{
				throw new XmlException("Xml_MultipleValidaitonTypes", string.Empty, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
			}
			this.schemaInfo.Add(schemaInfo, base.EventHandler);
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000CB41D File Offset: 0x000C961D
		private bool HasSchema
		{
			get
			{
				return this.schemaInfo.SchemaType > SchemaType.None;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x000CB42D File Offset: 0x000C962D
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000CB454 File Offset: 0x000C9654
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

		// Token: 0x06002501 RID: 9473 RVA: 0x000CB50B File Offset: 0x000C970B
		public override void CompleteValidation()
		{
			if (this.HasSchema)
			{
				this.CheckForwardRefs();
				return;
			}
			base.SendValidationEvent(new XmlSchemaException("Xml_NoValidation", string.Empty), XmlSeverityType.Warning);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000CB534 File Offset: 0x000C9734
		private void CheckValue(string value, SchemaAttDef attdef)
		{
			try
			{
				this.reader.TypedValueObject = null;
				bool flag = attdef != null;
				XmlSchemaDatatype xmlSchemaDatatype = flag ? attdef.Datatype : this.context.ElementDecl.Datatype;
				if (xmlSchemaDatatype != null)
				{
					if (xmlSchemaDatatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						value = value.Trim();
					}
					if (value.Length != 0)
					{
						object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, this.nsManager);
						this.reader.TypedValueObject = obj;
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
						if (schemaDeclBase.MaxLength != (long)((ulong)-1) && (long)value.Length > schemaDeclBase.MaxLength)
						{
							base.SendValidationEvent("Sch_MaxLengthConstraintFailed", value);
						}
						if (schemaDeclBase.MinLength != (long)((ulong)-1) && (long)value.Length < schemaDeclBase.MinLength)
						{
							base.SendValidationEvent("Sch_MinLengthConstraintFailed", value);
						}
						if (schemaDeclBase.Values != null && !schemaDeclBase.CheckEnumeration(obj))
						{
							if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.NOTATION)
							{
								base.SendValidationEvent("Sch_NotationValue", obj.ToString());
							}
							else
							{
								base.SendValidationEvent("Sch_EnumerationValue", obj.ToString());
							}
						}
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
					}
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

		// Token: 0x06002503 RID: 9475 RVA: 0x000CB760 File Offset: 0x000C9960
		public static void CheckDefaultValue(string value, SchemaAttDef attdef, SchemaInfo sinfo, XmlNamespaceManager nsManager, XmlNameTable NameTable, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNo, int linePos)
		{
			try
			{
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					if (datatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						value = value.Trim();
					}
					if (value.Length != 0)
					{
						object obj = datatype.ParseValue(value, NameTable, nsManager);
						XmlTokenizedType tokenizedType = datatype.TokenizedType;
						if (tokenizedType == XmlTokenizedType.ENTITY)
						{
							if (datatype.Variety == XmlSchemaDatatypeVariety.List)
							{
								string[] array = (string[])obj;
								for (int i = 0; i < array.Length; i++)
								{
									BaseValidator.ProcessEntity(sinfo, array[i], sender, eventhandler, baseUri, lineNo, linePos);
								}
							}
							else
							{
								BaseValidator.ProcessEntity(sinfo, (string)obj, sender, eventhandler, baseUri, lineNo, linePos);
							}
						}
						else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(obj))
						{
							XmlSchemaException ex = new XmlSchemaException("Sch_EnumerationValue", obj.ToString(), baseUri, lineNo, linePos);
							if (eventhandler == null)
							{
								throw ex;
							}
							eventhandler(sender, new ValidationEventArgs(ex));
						}
						attdef.DefaultValueTyped = obj;
					}
				}
			}
			catch
			{
				XmlSchemaException ex2 = new XmlSchemaException("Sch_AttributeDefaultDataType", attdef.Name.ToString(), baseUri, lineNo, linePos);
				if (eventhandler == null)
				{
					throw ex2;
				}
				eventhandler(sender, new ValidationEventArgs(ex2));
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000CB890 File Offset: 0x000C9A90
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000CB8B2 File Offset: 0x000C9AB2
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000CB8CC File Offset: 0x000C9ACC
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
			this.context.NeedValidateChildren = false;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000CB959 File Offset: 0x000C9B59
		private void Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000CB98C File Offset: 0x000C9B8C
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

		// Token: 0x06002509 RID: 9481 RVA: 0x000CB9F7 File Offset: 0x000C9BF7
		private XmlQualifiedName QualifiedName(string name, string ns)
		{
			return new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, base.NameTable, base.SchemaNames));
		}

		// Token: 0x04001035 RID: 4149
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001036 RID: 4150
		private HWStack validationStack;

		// Token: 0x04001037 RID: 4151
		private Hashtable attPresence;

		// Token: 0x04001038 RID: 4152
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04001039 RID: 4153
		private XmlNamespaceManager nsManager;

		// Token: 0x0400103A RID: 4154
		private bool isProcessContents;

		// Token: 0x0400103B RID: 4155
		private Hashtable IDs;

		// Token: 0x0400103C RID: 4156
		private IdRefNode idRefListHead;

		// Token: 0x0400103D RID: 4157
		private Parser inlineSchemaParser;

		// Token: 0x0400103E RID: 4158
		private const string x_schema = "x-schema:";
	}
}
