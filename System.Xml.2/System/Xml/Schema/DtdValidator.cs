using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000243 RID: 579
	internal sealed class DtdValidator : BaseValidator
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x000B8075 File Offset: 0x000B6275
		internal DtdValidator(XmlValidatingReaderImpl reader, IValidationEventHandling eventHandling, bool processIdentityConstraints) : base(reader, null, eventHandling)
		{
			this.processIdentityConstraints = processIdentityConstraints;
			this.Init();
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000B8098 File Offset: 0x000B6298
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

		// Token: 0x060022A9 RID: 8873 RVA: 0x000B80F4 File Offset: 0x000B62F4
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

		// Token: 0x060022AA RID: 8874 RVA: 0x000B81FC File Offset: 0x000B63FC
		private bool MeetsStandAloneConstraint()
		{
			if (this.reader.StandAlone && this.context.ElementDecl != null && this.context.ElementDecl.IsDeclaredInExternal && this.context.ElementDecl.ContentValidator.ContentType == XmlSchemaContentType.ElementOnly)
			{
				base.SendValidationEvent("Sch_StandAlone");
				return false;
			}
			return true;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000B825B File Offset: 0x000B645B
		private void ValidatePIComment()
		{
			if (this.context.NeedValidateChildren && this.context.ElementDecl.ContentValidator == ContentValidator.Empty)
			{
				base.SendValidationEvent("Sch_InvalidPIComment");
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000B828C File Offset: 0x000B648C
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

		// Token: 0x060022AD RID: 8877 RVA: 0x000B830C File Offset: 0x000B650C
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

		// Token: 0x060022AE RID: 8878 RVA: 0x000B8390 File Offset: 0x000B6590
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

		// Token: 0x060022AF RID: 8879 RVA: 0x000B8554 File Offset: 0x000B6754
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

		// Token: 0x060022B0 RID: 8880 RVA: 0x000B8610 File Offset: 0x000B6810
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

		// Token: 0x060022B1 RID: 8881 RVA: 0x000B86A6 File Offset: 0x000B68A6
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

		// Token: 0x060022B2 RID: 8882 RVA: 0x000B86CC File Offset: 0x000B68CC
		private void ValidateEndElement()
		{
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

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000B879D File Offset: 0x000B699D
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000B87C4 File Offset: 0x000B69C4
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

		// Token: 0x060022B5 RID: 8885 RVA: 0x000B8890 File Offset: 0x000B6A90
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

		// Token: 0x060022B6 RID: 8886 RVA: 0x000B8A60 File Offset: 0x000B6C60
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000B8A82 File Offset: 0x000B6C82
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000B8A9C File Offset: 0x000B6C9C
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

		// Token: 0x060022B9 RID: 8889 RVA: 0x000B8B1C File Offset: 0x000B6D1C
		private SchemaEntity GetEntity(XmlQualifiedName qname, bool fParameterEntity)
		{
			SchemaEntity result;
			if (fParameterEntity)
			{
				if (this.schemaInfo.ParameterEntities.TryGetValue(qname, out result))
				{
					return result;
				}
			}
			else if (this.schemaInfo.GeneralEntities.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000B8B5C File Offset: 0x000B6D5C
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

		// Token: 0x060022BB RID: 8891 RVA: 0x000B8BC8 File Offset: 0x000B6DC8
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

		// Token: 0x060022BC RID: 8892 RVA: 0x000B8C55 File Offset: 0x000B6E55
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

		// Token: 0x060022BD RID: 8893 RVA: 0x000B8C8C File Offset: 0x000B6E8C
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
					attdef.DefaultValueTyped = datatype.ParseValue(text, readerAdapter.NameTable, readerAdapter.NamespaceResolver);
				}
			}
			catch (Exception)
			{
				IValidationEventHandling validationEventHandling = ((IDtdParserAdapterWithValidation)readerAdapter).ValidationEventHandling;
				if (validationEventHandling != null)
				{
					XmlSchemaException exception = new XmlSchemaException("Sch_AttributeDefaultDataType", attdef.Name.ToString());
					validationEventHandling.SendEvent(exception, XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000B8D18 File Offset: 0x000B6F18
		public static void CheckDefaultValue(SchemaAttDef attdef, SchemaInfo sinfo, IValidationEventHandling eventHandling, string baseUriStr)
		{
			try
			{
				if (baseUriStr == null)
				{
					baseUriStr = string.Empty;
				}
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					object defaultValueTyped = attdef.DefaultValueTyped;
					XmlTokenizedType tokenizedType = datatype.TokenizedType;
					if (tokenizedType == XmlTokenizedType.ENTITY)
					{
						if (datatype.Variety == XmlSchemaDatatypeVariety.List)
						{
							string[] array = (string[])defaultValueTyped;
							for (int i = 0; i < array.Length; i++)
							{
								BaseValidator.ProcessEntity(sinfo, array[i], eventHandling, baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
							}
						}
						else
						{
							BaseValidator.ProcessEntity(sinfo, (string)defaultValueTyped, eventHandling, baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
						}
					}
					else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(defaultValueTyped) && eventHandling != null)
					{
						XmlSchemaException exception = new XmlSchemaException("Sch_EnumerationValue", defaultValueTyped.ToString(), baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
						eventHandling.SendEvent(exception, XmlSeverityType.Error);
					}
				}
			}
			catch (Exception)
			{
				if (eventHandling != null)
				{
					XmlSchemaException exception2 = new XmlSchemaException("Sch_AttributeDefaultDataType", attdef.Name.ToString());
					eventHandling.SendEvent(exception2, XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x04000EA6 RID: 3750
		private static DtdValidator.NamespaceManager namespaceManager = new DtdValidator.NamespaceManager();

		// Token: 0x04000EA7 RID: 3751
		private const int STACK_INCREMENT = 10;

		// Token: 0x04000EA8 RID: 3752
		private HWStack validationStack;

		// Token: 0x04000EA9 RID: 3753
		private Hashtable attPresence;

		// Token: 0x04000EAA RID: 3754
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000EAB RID: 3755
		private Hashtable IDs;

		// Token: 0x04000EAC RID: 3756
		private IdRefNode idRefListHead;

		// Token: 0x04000EAD RID: 3757
		private bool processIdentityConstraints;

		// Token: 0x0200048F RID: 1167
		private class NamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06003128 RID: 12584 RVA: 0x0011E11A File Offset: 0x0011C31A
			public override string LookupNamespace(string prefix)
			{
				return prefix;
			}
		}
	}
}
