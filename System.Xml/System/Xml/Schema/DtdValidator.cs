using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020001F3 RID: 499
	internal sealed class DtdValidator : BaseValidator
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x0006973B File Offset: 0x0006873B
		internal DtdValidator(XmlValidatingReaderImpl reader, ValidationEventHandler eventHandler, bool processIdentityConstraints) : base(reader, null, eventHandler)
		{
			this.processIdentityConstraints = processIdentityConstraints;
			this.Init();
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00069760 File Offset: 0x00068760
		private void Init()
		{
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.name = XmlQualifiedName.Empty;
			this.attPresence = new Hashtable();
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
			this.Push(this.name);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000697BC File Offset: 0x000687BC
		public override void Validate()
		{
			if (this.schemaInfo.SchemaType == SchemaType.DTD)
			{
				switch (this.reader.NodeType)
				{
				case XmlNodeType.Element:
					this.ValidateElement();
					if (!this.reader.IsEmptyElement)
					{
						return;
					}
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					return;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					base.ValidateText();
					return;
				case XmlNodeType.EntityReference:
					if (!this.GenEntity(new XmlQualifiedName(this.reader.LocalName, this.reader.Prefix)))
					{
						base.ValidateText();
						return;
					}
					return;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					this.ValidatePIComment();
					return;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (this.MeetsStandAloneConstraint())
					{
						base.ValidateWhitespace();
						return;
					}
					return;
				case XmlNodeType.EndElement:
					break;
				default:
					return;
				}
				this.ValidateEndElement();
				return;
			}
			if (this.reader.Depth == 0 && this.reader.NodeType == XmlNodeType.Element)
			{
				base.SendValidationEvent("Xml_NoDTDPresent", this.name.ToString(), XmlSeverityType.Warning);
			}
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000698C4 File Offset: 0x000688C4
		private bool MeetsStandAloneConstraint()
		{
			if (this.reader.StandAlone && this.context.ElementDecl != null && this.context.ElementDecl.IsDeclaredInExternal && this.context.ElementDecl.ContentValidator.ContentType == XmlSchemaContentType.ElementOnly)
			{
				base.SendValidationEvent("Sch_StandAlone");
				return false;
			}
			return true;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00069923 File Offset: 0x00068923
		private void ValidatePIComment()
		{
			if (this.context.NeedValidateChildren && this.context.ElementDecl.ContentValidator == ContentValidator.Empty)
			{
				base.SendValidationEvent("Sch_InvalidPIComment");
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00069954 File Offset: 0x00068954
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, this.reader.Prefix);
			if (this.reader.Depth == 0 && !this.schemaInfo.DocTypeName.IsEmpty && !this.schemaInfo.DocTypeName.Equals(this.elementName))
			{
				base.SendValidationEvent("Sch_RootMatchDocType");
			}
			else
			{
				this.ValidateChildElement();
			}
			this.ProcessElement();
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000699D4 File Offset: 0x000689D4
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

		// Token: 0x060017EB RID: 6123 RVA: 0x00069A58 File Offset: 0x00068A58
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				base.Reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				if (base.Reader.IsEmptyElement && this.context.ElementDecl.DefaultValueTyped != null)
				{
					base.Reader.TypedValueObject = this.context.ElementDecl.DefaultValueTyped;
					this.context.IsNill = true;
				}
				if (this.context.ElementDecl.HasRequiredAttribute)
				{
					this.attPresence.Clear();
				}
			}
			if (base.Reader.MoveToFirstAttribute())
			{
				do
				{
					try
					{
						this.reader.SchemaTypeObject = null;
						SchemaAttDef attDef = this.context.ElementDecl.GetAttDef(new XmlQualifiedName(this.reader.LocalName, this.reader.Prefix));
						if (attDef != null)
						{
							if (this.context.ElementDecl != null && this.context.ElementDecl.HasRequiredAttribute)
							{
								this.attPresence.Add(attDef.Name, attDef);
							}
							base.Reader.SchemaTypeObject = attDef.SchemaType;
							if (attDef.Datatype != null && !this.reader.IsDefault)
							{
								this.CheckValue(base.Reader.Value, attDef);
							}
						}
						else
						{
							base.SendValidationEvent("Sch_UndeclaredAttribute", this.reader.Name);
						}
					}
					catch (XmlSchemaException ex)
					{
						ex.SetSource(base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
						base.SendValidationEvent(ex);
					}
				}
				while (base.Reader.MoveToNextAttribute());
				base.Reader.MoveToElement();
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00069C1C File Offset: 0x00068C1C
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasRequiredAttribute)
			{
				try
				{
					this.context.ElementDecl.CheckAttributes(this.attPresence, base.Reader.StandAlone);
				}
				catch (XmlSchemaException ex)
				{
					ex.SetSource(base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
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

		// Token: 0x060017ED RID: 6125 RVA: 0x00069CD8 File Offset: 0x00068CD8
		private void ProcessElement()
		{
			SchemaElementDecl elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			this.Push(this.elementName);
			if (elementDecl != null)
			{
				this.context.ElementDecl = elementDecl;
				this.ValidateStartElement();
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = true;
				elementDecl.ContentValidator.InitValidation(this.context);
				return;
			}
			base.SendValidationEvent("Sch_UndeclaredElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			this.context.ElementDecl = null;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00069D6E File Offset: 0x00068D6E
		public override void CompleteValidation()
		{
			if (this.schemaInfo.SchemaType == SchemaType.DTD)
			{
				do
				{
					this.ValidateEndElement();
				}
				while (this.Pop());
				this.CheckForwardRefs();
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00069D94 File Offset: 0x00068D94
		private void ValidateEndElement()
		{
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

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00069E65 File Offset: 0x00068E65
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00069E8C File Offset: 0x00068E8C
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.processIdentityConstraints)
				{
					if (this.FindId(name) != null)
					{
						base.SendValidationEvent("Sch_DupId", name);
						return;
					}
					this.AddID(name, this.context.LocalName);
					return;
				}
				break;
			case XmlTokenizedType.IDREF:
				if (this.processIdentityConstraints && this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				BaseValidator.ProcessEntity(this.schemaInfo, name, this, base.EventHandler, base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
				break;
			default:
				return;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00069F58 File Offset: 0x00068F58
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
					object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, DtdValidator.namespaceManager);
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

		// Token: 0x060017F3 RID: 6131 RVA: 0x0006A130 File Offset: 0x00069130
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0006A152 File Offset: 0x00069152
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0006A16C File Offset: 0x0006916C
		private bool GenEntity(XmlQualifiedName qname)
		{
			string text = qname.Name;
			if (text[0] == '#')
			{
				return false;
			}
			if (SchemaEntity.IsPredefinedEntity(text))
			{
				return false;
			}
			SchemaEntity entity = this.GetEntity(qname, false);
			if (entity == null)
			{
				throw new XmlException("Xml_UndeclaredEntity", text);
			}
			if (!entity.NData.IsEmpty)
			{
				throw new XmlException("Xml_UnparsedEntityRef", text);
			}
			if (this.reader.StandAlone && entity.DeclaredInExternal)
			{
				base.SendValidationEvent("Sch_StandAlone");
			}
			return true;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0006A1E9 File Offset: 0x000691E9
		private SchemaEntity GetEntity(XmlQualifiedName qname, bool fParameterEntity)
		{
			if (fParameterEntity)
			{
				return (SchemaEntity)this.schemaInfo.ParameterEntities[qname];
			}
			return (SchemaEntity)this.schemaInfo.GeneralEntities[qname];
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0006A21C File Offset: 0x0006921C
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

		// Token: 0x060017F8 RID: 6136 RVA: 0x0006A288 File Offset: 0x00069288
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

		// Token: 0x060017F9 RID: 6137 RVA: 0x0006A315 File Offset: 0x00069315
		private bool Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
				return true;
			}
			return false;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0006A34C File Offset: 0x0006934C
		public static void SetDefaultTypedValue(SchemaAttDef attdef, IDtdParserAdapter readerAdapter)
		{
			try
			{
				string text = attdef.DefaultValueExpanded;
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					if (datatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						text = text.Trim();
					}
					attdef.DefaultValueTyped = datatype.ParseValue(text, readerAdapter.NameTable, readerAdapter.NamespaceManager);
				}
			}
			catch (Exception)
			{
				XmlSchemaException exception = new XmlSchemaException("Sch_AttributeDefaultDataType", attdef.Name.ToString());
				readerAdapter.SendValidationEvent(XmlSeverityType.Error, exception);
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0006A3C8 File Offset: 0x000693C8
		public static void CheckDefaultValue(SchemaAttDef attdef, SchemaInfo sinfo, IDtdParserAdapter readerAdapter)
		{
			try
			{
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					object defaultValueTyped = attdef.DefaultValueTyped;
					XmlTokenizedType tokenizedType = datatype.TokenizedType;
					if (tokenizedType == XmlTokenizedType.ENTITY)
					{
						Uri baseUri = readerAdapter.BaseUri;
						string baseUri2 = (baseUri == null) ? string.Empty : baseUri.ToString();
						if (datatype.Variety == XmlSchemaDatatypeVariety.List)
						{
							string[] array = (string[])defaultValueTyped;
							foreach (string text in array)
							{
								BaseValidator.ProcessEntity(sinfo, text, readerAdapter, readerAdapter.EventHandler, baseUri2, attdef.ValueLineNum, attdef.ValueLinePos);
							}
						}
						else
						{
							BaseValidator.ProcessEntity(sinfo, (string)defaultValueTyped, readerAdapter, readerAdapter.EventHandler, baseUri2, attdef.ValueLineNum, attdef.ValueLinePos);
						}
					}
					else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(defaultValueTyped))
					{
						XmlSchemaException exception = new XmlSchemaException("Sch_EnumerationValue", defaultValueTyped.ToString(), readerAdapter.BaseUri.ToString(), attdef.ValueLineNum, attdef.ValueLinePos);
						readerAdapter.SendValidationEvent(XmlSeverityType.Error, exception);
					}
				}
			}
			catch (Exception)
			{
				XmlSchemaException exception2 = new XmlSchemaException("Sch_AttributeDefaultDataType", attdef.Name.ToString());
				readerAdapter.SendValidationEvent(XmlSeverityType.Error, exception2);
			}
		}

		// Token: 0x04000E37 RID: 3639
		private const int STACK_INCREMENT = 10;

		// Token: 0x04000E38 RID: 3640
		private static DtdValidator.NamespaceManager namespaceManager = new DtdValidator.NamespaceManager();

		// Token: 0x04000E39 RID: 3641
		private HWStack validationStack;

		// Token: 0x04000E3A RID: 3642
		private Hashtable attPresence;

		// Token: 0x04000E3B RID: 3643
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000E3C RID: 3644
		private Hashtable IDs;

		// Token: 0x04000E3D RID: 3645
		private IdRefNode idRefListHead;

		// Token: 0x04000E3E RID: 3646
		private bool processIdentityConstraints;

		// Token: 0x020001F4 RID: 500
		private class NamespaceManager : XmlNamespaceManager
		{
			// Token: 0x060017FD RID: 6141 RVA: 0x0006A50C File Offset: 0x0006950C
			public override string LookupNamespace(string prefix)
			{
				return prefix;
			}
		}
	}
}
