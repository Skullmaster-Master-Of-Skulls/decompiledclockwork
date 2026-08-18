using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000229 RID: 553
	internal sealed class XdrValidator : BaseValidator
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x0007E2FB File Offset: 0x0007D2FB
		internal XdrValidator(BaseValidator validator) : base(validator)
		{
			this.Init();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0007E315 File Offset: 0x0007D315
		internal XdrValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, ValidationEventHandler eventHandler) : base(reader, schemaCollection, eventHandler)
		{
			this.Init();
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0007E334 File Offset: 0x0007D334
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

		// Token: 0x06001A3A RID: 6714 RVA: 0x0007E3C0 File Offset: 0x0007D3C0
		public override void Validate()
		{
			if (this.IsInlineSchemaStarted)
			{
				this.ProcessInlineSchema();
				return;
			}
			XmlNodeType nodeType = this.reader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				this.ValidateElement();
				if (this.reader.IsEmptyElement)
				{
					goto IL_6C;
				}
				return;
			case XmlNodeType.Attribute:
				return;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			default:
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
					base.ValidateWhitespace();
					return;
				case XmlNodeType.SignificantWhitespace:
					break;
				case XmlNodeType.EndElement:
					goto IL_6C;
				default:
					return;
				}
				break;
			}
			base.ValidateText();
			return;
			IL_6C:
			this.ValidateEndElement();
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0007E440 File Offset: 0x0007D440
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

		// Token: 0x06001A3C RID: 6716 RVA: 0x0007E4FC File Offset: 0x0007D4FC
		private void ValidateChildElement()
		{
			if (this.context.NeedValidateChildren)
			{
				int num = 0;
				this.context.ElementDecl.ContentValidator.ValidateElement(this.elementName, this.context, out num);
				if (num < 0)
				{
					XmlSchemaValidator.ElementValidationError(this.elementName, this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, false);
				}
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0007E57F File Offset: 0x0007D57F
		private bool IsInlineSchemaStarted
		{
			get
			{
				return this.inlineSchemaParser != null;
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0007E590 File Offset: 0x0007D590
		private void ProcessInlineSchema()
		{
			if (!this.inlineSchemaParser.ParseReaderNode())
			{
				this.inlineSchemaParser.FinishParsing();
				SchemaInfo xdrSchema = this.inlineSchemaParser.XdrSchema;
				if (xdrSchema != null && xdrSchema.ErrorCount == 0)
				{
					foreach (object obj in xdrSchema.TargetNamespaces.Keys)
					{
						string ns = (string)obj;
						if (!base.SchemaInfo.HasSchema(ns))
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

		// Token: 0x06001A3F RID: 6719 RVA: 0x0007E654 File Offset: 0x0007D654
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

		// Token: 0x06001A40 RID: 6720 RVA: 0x0007E6D4 File Offset: 0x0007D6D4
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
					XmlSchemaValidator.CompleteValidationError(this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, false);
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

		// Token: 0x06001A41 RID: 6721 RVA: 0x0007E7B8 File Offset: 0x0007D7B8
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
			if (elementDecl == null && this.schemaInfo.TargetNamespaces.Contains(this.context.Namespace))
			{
				base.SendValidationEvent("Sch_UndeclaredElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return elementDecl;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0007E92C File Offset: 0x0007D92C
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

		// Token: 0x06001A43 RID: 6723 RVA: 0x0007EB40 File Offset: 0x0007DB40
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasDefaultAttribute)
			{
				foreach (SchemaAttDef attdef in this.context.ElementDecl.DefaultAttDefs)
				{
					this.reader.AddDefaultAttribute(attdef);
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

		// Token: 0x06001A44 RID: 6724 RVA: 0x0007EC3C File Offset: 0x0007DC3C
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

		// Token: 0x06001A45 RID: 6725 RVA: 0x0007EDAC File Offset: 0x0007DDAC
		private void LoadSchema(string uri)
		{
			if (base.SchemaInfo.TargetNamespaces.Contains(uri))
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
			base.SchemaInfo.Add(schemaInfo, base.EventHandler);
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0007EE39 File Offset: 0x0007DE39
		private bool HasSchema
		{
			get
			{
				return this.schemaInfo.SchemaType != SchemaType.None;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0007EE4C File Offset: 0x0007DE4C
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0007EE74 File Offset: 0x0007DE74
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

		// Token: 0x06001A49 RID: 6729 RVA: 0x0007EF2D File Offset: 0x0007DF2D
		public override void CompleteValidation()
		{
			if (this.HasSchema)
			{
				this.CheckForwardRefs();
				return;
			}
			base.SendValidationEvent(new XmlSchemaException("Xml_NoValidation", string.Empty), XmlSeverityType.Warning);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0007EF54 File Offset: 0x0007DF54
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
								foreach (string text in array)
								{
									this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, text);
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

		// Token: 0x06001A4B RID: 6731 RVA: 0x0007F188 File Offset: 0x0007E188
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
								foreach (string text in array)
								{
									BaseValidator.ProcessEntity(sinfo, text, sender, eventhandler, baseUri, lineNo, linePos);
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

		// Token: 0x06001A4C RID: 6732 RVA: 0x0007F2C4 File Offset: 0x0007E2C4
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0007F2E6 File Offset: 0x0007E2E6
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0007F300 File Offset: 0x0007E300
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

		// Token: 0x06001A4F RID: 6735 RVA: 0x0007F38D File Offset: 0x0007E38D
		private void Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0007F3C0 File Offset: 0x0007E3C0
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

		// Token: 0x06001A51 RID: 6737 RVA: 0x0007F42B File Offset: 0x0007E42B
		private XmlQualifiedName QualifiedName(string name, string ns)
		{
			return new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, base.NameTable, base.SchemaNames));
		}

		// Token: 0x04001098 RID: 4248
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001099 RID: 4249
		private const string x_schema = "x-schema:";

		// Token: 0x0400109A RID: 4250
		private HWStack validationStack;

		// Token: 0x0400109B RID: 4251
		private Hashtable attPresence;

		// Token: 0x0400109C RID: 4252
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x0400109D RID: 4253
		private XmlNamespaceManager nsManager;

		// Token: 0x0400109E RID: 4254
		private bool isProcessContents;

		// Token: 0x0400109F RID: 4255
		private Hashtable IDs;

		// Token: 0x040010A0 RID: 4256
		private IdRefNode idRefListHead;

		// Token: 0x040010A1 RID: 4257
		private Parser inlineSchemaParser;
	}
}
