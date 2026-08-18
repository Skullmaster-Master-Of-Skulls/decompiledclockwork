using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000128 RID: 296
	internal class DtdParser : IDtdParser
	{
		// Token: 0x0600154C RID: 5452 RVA: 0x0005B418 File Offset: 0x00059618
		private DtdParser()
		{
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005B485 File Offset: 0x00059685
		internal static IDtdParser Create()
		{
			return new DtdParser();
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0005B48C File Offset: 0x0005968C
		private void Initialize(IDtdParserAdapter readerAdapter)
		{
			this.readerAdapter = readerAdapter;
			this.readerAdapterWithValidation = (readerAdapter as IDtdParserAdapterWithValidation);
			this.nameTable = readerAdapter.NameTable;
			IDtdParserAdapterWithValidation dtdParserAdapterWithValidation = readerAdapter as IDtdParserAdapterWithValidation;
			if (dtdParserAdapterWithValidation != null)
			{
				this.validate = dtdParserAdapterWithValidation.DtdValidation;
			}
			IDtdParserAdapterV1 dtdParserAdapterV = readerAdapter as IDtdParserAdapterV1;
			if (dtdParserAdapterV != null)
			{
				this.v1Compat = dtdParserAdapterV.V1CompatibilityMode;
				this.normalize = dtdParserAdapterV.Normalization;
				this.supportNamespaces = dtdParserAdapterV.Namespaces;
			}
			this.schemaInfo = new SchemaInfo();
			this.schemaInfo.SchemaType = SchemaType.DTD;
			this.stringBuilder = new StringBuilder();
			Uri baseUri = readerAdapter.BaseUri;
			if (baseUri != null)
			{
				this.documentBaseUri = baseUri.ToString();
			}
			this.freeFloatingDtd = false;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0005B544 File Offset: 0x00059744
		private void InitializeFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter)
		{
			this.Initialize(adapter);
			if (docTypeName == null || docTypeName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(docTypeName, "docTypeName");
			}
			XmlConvert.VerifyName(docTypeName);
			int num = docTypeName.IndexOf(':');
			if (num == -1)
			{
				this.schemaInfo.DocTypeName = new XmlQualifiedName(this.nameTable.Add(docTypeName));
			}
			else
			{
				this.schemaInfo.DocTypeName = new XmlQualifiedName(this.nameTable.Add(docTypeName.Substring(0, num)), this.nameTable.Add(docTypeName.Substring(num + 1)));
			}
			if (systemId != null && systemId.Length > 0)
			{
				int invCharPos;
				if ((invCharPos = this.xmlCharType.IsOnlyCharData(systemId)) >= 0)
				{
					this.ThrowInvalidChar(this.curPos, systemId, invCharPos);
				}
				this.systemId = systemId;
			}
			if (publicId != null && publicId.Length > 0)
			{
				int invCharPos;
				if ((invCharPos = this.xmlCharType.IsPublicId(publicId)) >= 0)
				{
					this.ThrowInvalidChar(this.curPos, publicId, invCharPos);
				}
				this.publicId = publicId;
			}
			if (internalSubset != null && internalSubset.Length > 0)
			{
				this.readerAdapter.PushInternalDtd(baseUri, internalSubset);
				this.hasFreeFloatingInternalSubset = true;
			}
			Uri baseUri2 = this.readerAdapter.BaseUri;
			if (baseUri2 != null)
			{
				this.documentBaseUri = baseUri2.ToString();
			}
			this.freeFloatingDtd = true;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0005B68D File Offset: 0x0005988D
		IDtdInfo IDtdParser.ParseInternalDtd(IDtdParserAdapter adapter, bool saveInternalSubset)
		{
			this.Initialize(adapter);
			this.Parse(saveInternalSubset);
			return this.schemaInfo;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0005B6A3 File Offset: 0x000598A3
		IDtdInfo IDtdParser.ParseFreeFloatingDtd(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter)
		{
			this.InitializeFreeFloatingDtd(baseUri, docTypeName, publicId, systemId, internalSubset, adapter);
			this.Parse(false);
			return this.schemaInfo;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005B6C1 File Offset: 0x000598C1
		private bool ParsingInternalSubset
		{
			get
			{
				return this.externalEntitiesDepth == 0;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0005B6CC File Offset: 0x000598CC
		private bool IgnoreEntityReferences
		{
			get
			{
				return this.scanningFunction == DtdParser.ScanningFunction.CondSection3;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0005B6D8 File Offset: 0x000598D8
		private bool SaveInternalSubsetValue
		{
			get
			{
				return this.readerAdapter.EntityStackLength == 0 && this.internalSubsetValueSb != null;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0005B6F2 File Offset: 0x000598F2
		private bool ParsingTopLevelMarkup
		{
			get
			{
				return this.scanningFunction == DtdParser.ScanningFunction.SubsetContent || (this.scanningFunction == DtdParser.ScanningFunction.ParamEntitySpace && this.savedScanningFunction == DtdParser.ScanningFunction.SubsetContent);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0005B713 File Offset: 0x00059913
		private bool SupportNamespaces
		{
			get
			{
				return this.supportNamespaces;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0005B71B File Offset: 0x0005991B
		private bool Normalize
		{
			get
			{
				return this.normalize;
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0005B724 File Offset: 0x00059924
		private void Parse(bool saveInternalSubset)
		{
			if (this.freeFloatingDtd)
			{
				this.ParseFreeFloatingDtd();
			}
			else
			{
				this.ParseInDocumentDtd(saveInternalSubset);
			}
			this.schemaInfo.Finish();
			if (this.validate && this.undeclaredNotations != null)
			{
				foreach (DtdParser.UndeclaredNotation undeclaredNotation in this.undeclaredNotations.Values)
				{
					for (DtdParser.UndeclaredNotation undeclaredNotation2 = undeclaredNotation; undeclaredNotation2 != null; undeclaredNotation2 = undeclaredNotation2.next)
					{
						this.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("Sch_UndeclaredNotation", undeclaredNotation.name, this.BaseUriStr, undeclaredNotation.lineNo, undeclaredNotation.linePos));
					}
				}
			}
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005B7E0 File Offset: 0x000599E0
		private void ParseInDocumentDtd(bool saveInternalSubset)
		{
			this.LoadParsingBuffer();
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			this.nextScaningFunction = DtdParser.ScanningFunction.Doctype1;
			if (this.GetToken(false) != DtdParser.Token.QName)
			{
				this.OnUnexpectedError();
			}
			this.schemaInfo.DocTypeName = this.GetNameQualified(true);
			DtdParser.Token token = this.GetToken(false);
			if (token == DtdParser.Token.SYSTEM || token == DtdParser.Token.PUBLIC)
			{
				this.ParseExternalId(token, DtdParser.Token.DOCTYPE, out this.publicId, out this.systemId);
				token = this.GetToken(false);
			}
			if (token != DtdParser.Token.GreaterThan)
			{
				if (token == DtdParser.Token.LeftBracket)
				{
					if (saveInternalSubset)
					{
						this.SaveParsingBuffer();
						this.internalSubsetValueSb = new StringBuilder();
					}
					this.ParseInternalSubset();
				}
				else
				{
					this.OnUnexpectedError();
				}
			}
			this.SaveParsingBuffer();
			if (this.systemId != null && this.systemId.Length > 0)
			{
				this.ParseExternalSubset();
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005B8A1 File Offset: 0x00059AA1
		private void ParseFreeFloatingDtd()
		{
			if (this.hasFreeFloatingInternalSubset)
			{
				this.LoadParsingBuffer();
				this.ParseInternalSubset();
				this.SaveParsingBuffer();
			}
			if (this.systemId != null && this.systemId.Length > 0)
			{
				this.ParseExternalSubset();
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0005B8D9 File Offset: 0x00059AD9
		private void ParseInternalSubset()
		{
			this.ParseSubset();
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		private void ParseExternalSubset()
		{
			if (!this.readerAdapter.PushExternalSubset(this.systemId, this.publicId))
			{
				return;
			}
			Uri baseUri = this.readerAdapter.BaseUri;
			if (baseUri != null)
			{
				this.externalDtdBaseUri = baseUri.ToString();
			}
			this.externalEntitiesDepth++;
			this.LoadParsingBuffer();
			this.ParseSubset();
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005B948 File Offset: 0x00059B48
		private void ParseSubset()
		{
			for (;;)
			{
				DtdParser.Token token = this.GetToken(false);
				int num = this.currentEntityId;
				switch (token)
				{
				case DtdParser.Token.AttlistDecl:
					this.ParseAttlistDecl();
					break;
				case DtdParser.Token.ElementDecl:
					this.ParseElementDecl();
					break;
				case DtdParser.Token.EntityDecl:
					this.ParseEntityDecl();
					break;
				case DtdParser.Token.NotationDecl:
					this.ParseNotationDecl();
					break;
				case DtdParser.Token.Comment:
					this.ParseComment();
					break;
				case DtdParser.Token.PI:
					this.ParsePI();
					break;
				case DtdParser.Token.CondSectionStart:
					if (this.ParsingInternalSubset)
					{
						this.Throw(this.curPos - 3, "Xml_InvalidConditionalSection");
					}
					this.ParseCondSection();
					num = this.currentEntityId;
					break;
				case DtdParser.Token.CondSectionEnd:
					if (this.condSectionDepth > 0)
					{
						this.condSectionDepth--;
						if (this.validate && this.currentEntityId != this.condSectionEntityIds[this.condSectionDepth])
						{
							this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
						}
					}
					else
					{
						this.Throw(this.curPos - 3, "Xml_UnexpectedCDataEnd");
					}
					break;
				case DtdParser.Token.Eof:
					goto IL_1A9;
				default:
					if (token == DtdParser.Token.RightBracket)
					{
						goto IL_126;
					}
					break;
				}
				if (this.currentEntityId != num)
				{
					if (this.validate)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
					}
					else if (!this.v1Compat)
					{
						this.Throw(this.curPos, "Sch_ParEntityRefNesting");
					}
				}
			}
			IL_126:
			if (this.ParsingInternalSubset)
			{
				if (this.condSectionDepth != 0)
				{
					this.Throw(this.curPos, "Xml_UnclosedConditionalSection");
				}
				if (this.internalSubsetValueSb != null)
				{
					this.SaveParsingBuffer(this.curPos - 1);
					this.schemaInfo.InternalDtdSubset = this.internalSubsetValueSb.ToString();
					this.internalSubsetValueSb = null;
				}
				if (this.GetToken(false) != DtdParser.Token.GreaterThan)
				{
					this.ThrowUnexpectedToken(this.curPos, ">");
					return;
				}
			}
			else
			{
				this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			}
			return;
			IL_1A9:
			if (this.ParsingInternalSubset && !this.freeFloatingDtd)
			{
				this.Throw(this.curPos, "Xml_IncompleteDtdContent");
			}
			if (this.condSectionDepth != 0)
			{
				this.Throw(this.curPos, "Xml_UnclosedConditionalSection");
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0005BB8C File Offset: 0x00059D8C
		private void ParseAttlistDecl()
		{
			if (this.GetToken(true) == DtdParser.Token.QName)
			{
				XmlQualifiedName nameQualified = this.GetNameQualified(true);
				SchemaElementDecl schemaElementDecl;
				if (!this.schemaInfo.ElementDecls.TryGetValue(nameQualified, out schemaElementDecl) && !this.schemaInfo.UndeclaredElementDecls.TryGetValue(nameQualified, out schemaElementDecl))
				{
					schemaElementDecl = new SchemaElementDecl(nameQualified, nameQualified.Namespace);
					this.schemaInfo.UndeclaredElementDecls.Add(nameQualified, schemaElementDecl);
				}
				SchemaAttDef schemaAttDef = null;
				DtdParser.Token token;
				for (;;)
				{
					token = this.GetToken(false);
					if (token != DtdParser.Token.QName)
					{
						break;
					}
					XmlQualifiedName nameQualified2 = this.GetNameQualified(true);
					schemaAttDef = new SchemaAttDef(nameQualified2, nameQualified2.Namespace);
					schemaAttDef.IsDeclaredInExternal = !this.ParsingInternalSubset;
					schemaAttDef.LineNumber = this.LineNo;
					schemaAttDef.LinePosition = this.LinePos - (this.curPos - this.tokenStartPos);
					bool flag = schemaElementDecl.GetAttDef(schemaAttDef.Name) != null;
					this.ParseAttlistType(schemaAttDef, schemaElementDecl, flag);
					this.ParseAttlistDefault(schemaAttDef, flag);
					if (schemaAttDef.Prefix.Length > 0 && schemaAttDef.Prefix.Equals("xml"))
					{
						if (schemaAttDef.Name.Name == "space")
						{
							if (this.v1Compat)
							{
								string text = schemaAttDef.DefaultValueExpanded.Trim();
								if (text.Equals("preserve") || text.Equals("default"))
								{
									schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlSpace;
								}
							}
							else
							{
								schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlSpace;
								if (schemaAttDef.TokenizedType != XmlTokenizedType.ENUMERATION)
								{
									this.Throw("Xml_EnumerationRequired", string.Empty, schemaAttDef.LineNumber, schemaAttDef.LinePosition);
								}
								if (this.validate)
								{
									schemaAttDef.CheckXmlSpace(this.readerAdapterWithValidation.ValidationEventHandling);
								}
							}
						}
						else if (schemaAttDef.Name.Name == "lang")
						{
							schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlLang;
						}
					}
					if (!flag)
					{
						schemaElementDecl.AddAttDef(schemaAttDef);
					}
				}
				if (token == DtdParser.Token.GreaterThan)
				{
					if (this.v1Compat && schemaAttDef != null && schemaAttDef.Prefix.Length > 0 && schemaAttDef.Prefix.Equals("xml") && schemaAttDef.Name.Name == "space")
					{
						schemaAttDef.Reserved = SchemaAttDef.Reserve.XmlSpace;
						if (schemaAttDef.Datatype.TokenizedType != XmlTokenizedType.ENUMERATION)
						{
							this.Throw("Xml_EnumerationRequired", string.Empty, schemaAttDef.LineNumber, schemaAttDef.LinePosition);
						}
						if (this.validate)
						{
							schemaAttDef.CheckXmlSpace(this.readerAdapterWithValidation.ValidationEventHandling);
						}
					}
					return;
				}
			}
			this.OnUnexpectedError();
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0005BE14 File Offset: 0x0005A014
		private void ParseAttlistType(SchemaAttDef attrDef, SchemaElementDecl elementDecl, bool ignoreErrors)
		{
			DtdParser.Token token = this.GetToken(true);
			if (token != DtdParser.Token.CDATA)
			{
				elementDecl.HasNonCDataAttribute = true;
			}
			if (this.IsAttributeValueType(token))
			{
				attrDef.TokenizedType = (XmlTokenizedType)token;
				attrDef.SchemaType = XmlSchemaType.GetBuiltInSimpleType(attrDef.Datatype.TypeCode);
				if (token == DtdParser.Token.ID)
				{
					if (this.validate && elementDecl.IsIdDeclared)
					{
						SchemaAttDef attDef = elementDecl.GetAttDef(attrDef.Name);
						if ((attDef == null || attDef.Datatype.TokenizedType != XmlTokenizedType.ID) && !ignoreErrors)
						{
							this.SendValidationEvent(XmlSeverityType.Error, "Sch_IdAttrDeclared", elementDecl.Name.ToString());
						}
					}
					elementDecl.IsIdDeclared = true;
					return;
				}
				if (token != DtdParser.Token.NOTATION)
				{
					return;
				}
				if (this.validate)
				{
					if (elementDecl.IsNotationDeclared && !ignoreErrors)
					{
						this.SendValidationEvent(this.curPos - 8, XmlSeverityType.Error, "Sch_DupNotationAttribute", elementDecl.Name.ToString());
					}
					else
					{
						if (elementDecl.ContentValidator != null && elementDecl.ContentValidator.ContentType == XmlSchemaContentType.Empty && !ignoreErrors)
						{
							this.SendValidationEvent(this.curPos - 8, XmlSeverityType.Error, "Sch_NotationAttributeOnEmptyElement", elementDecl.Name.ToString());
						}
						elementDecl.IsNotationDeclared = true;
					}
				}
				if (this.GetToken(true) == DtdParser.Token.LeftParen && this.GetToken(false) == DtdParser.Token.Name)
				{
					do
					{
						string nameString = this.GetNameString();
						if (!this.schemaInfo.Notations.ContainsKey(nameString))
						{
							this.AddUndeclaredNotation(nameString);
						}
						if (this.validate && !this.v1Compat && attrDef.Values != null && attrDef.Values.Contains(nameString) && !ignoreErrors)
						{
							this.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("Xml_AttlistDuplNotationValue", nameString, this.BaseUriStr, this.LineNo, this.LinePos));
						}
						attrDef.AddValue(nameString);
						DtdParser.Token token2 = this.GetToken(false);
						if (token2 == DtdParser.Token.RightParen)
						{
							return;
						}
						if (token2 != DtdParser.Token.Or)
						{
							break;
						}
					}
					while (this.GetToken(false) == DtdParser.Token.Name);
				}
			}
			else if (token == DtdParser.Token.LeftParen)
			{
				attrDef.TokenizedType = XmlTokenizedType.ENUMERATION;
				attrDef.SchemaType = XmlSchemaType.GetBuiltInSimpleType(attrDef.Datatype.TypeCode);
				if (this.GetToken(false) == DtdParser.Token.Nmtoken)
				{
					attrDef.AddValue(this.GetNameString());
					for (;;)
					{
						DtdParser.Token token3 = this.GetToken(false);
						if (token3 == DtdParser.Token.RightParen)
						{
							break;
						}
						if (token3 != DtdParser.Token.Or || this.GetToken(false) != DtdParser.Token.Nmtoken)
						{
							goto IL_286;
						}
						string nmtokenString = this.GetNmtokenString();
						if (this.validate && !this.v1Compat && attrDef.Values != null && attrDef.Values.Contains(nmtokenString) && !ignoreErrors)
						{
							this.SendValidationEvent(XmlSeverityType.Error, new XmlSchemaException("Xml_AttlistDuplEnumValue", nmtokenString, this.BaseUriStr, this.LineNo, this.LinePos));
						}
						attrDef.AddValue(nmtokenString);
					}
					return;
				}
			}
			IL_286:
			this.OnUnexpectedError();
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0005C0B0 File Offset: 0x0005A2B0
		private void ParseAttlistDefault(SchemaAttDef attrDef, bool ignoreErrors)
		{
			DtdParser.Token token = this.GetToken(true);
			switch (token)
			{
			case DtdParser.Token.REQUIRED:
				attrDef.Presence = SchemaDeclBase.Use.Required;
				return;
			case DtdParser.Token.IMPLIED:
				attrDef.Presence = SchemaDeclBase.Use.Implied;
				return;
			case DtdParser.Token.FIXED:
				attrDef.Presence = SchemaDeclBase.Use.Fixed;
				if (this.GetToken(true) != DtdParser.Token.Literal)
				{
					goto IL_CF;
				}
				break;
			default:
				if (token != DtdParser.Token.Literal)
				{
					goto IL_CF;
				}
				break;
			}
			if (this.validate && attrDef.Datatype.TokenizedType == XmlTokenizedType.ID && !ignoreErrors)
			{
				this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_AttListPresence", string.Empty);
			}
			if (attrDef.TokenizedType != XmlTokenizedType.CDATA)
			{
				attrDef.DefaultValueExpanded = this.GetValueWithStrippedSpaces();
			}
			else
			{
				attrDef.DefaultValueExpanded = this.GetValue();
			}
			attrDef.ValueLineNumber = this.literalLineInfo.lineNo;
			attrDef.ValueLinePosition = this.literalLineInfo.linePos + 1;
			DtdValidator.SetDefaultTypedValue(attrDef, this.readerAdapter);
			return;
			IL_CF:
			this.OnUnexpectedError();
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0005C194 File Offset: 0x0005A394
		private void ParseElementDecl()
		{
			if (this.GetToken(true) == DtdParser.Token.QName)
			{
				SchemaElementDecl schemaElementDecl = null;
				XmlQualifiedName nameQualified = this.GetNameQualified(true);
				if (this.schemaInfo.ElementDecls.TryGetValue(nameQualified, out schemaElementDecl))
				{
					if (this.validate)
					{
						this.SendValidationEvent(this.curPos - nameQualified.Name.Length, XmlSeverityType.Error, "Sch_DupElementDecl", this.GetNameString());
					}
				}
				else
				{
					if (this.schemaInfo.UndeclaredElementDecls.TryGetValue(nameQualified, out schemaElementDecl))
					{
						this.schemaInfo.UndeclaredElementDecls.Remove(nameQualified);
					}
					else
					{
						schemaElementDecl = new SchemaElementDecl(nameQualified, nameQualified.Namespace);
					}
					this.schemaInfo.ElementDecls.Add(nameQualified, schemaElementDecl);
				}
				schemaElementDecl.IsDeclaredInExternal = !this.ParsingInternalSubset;
				DtdParser.Token token = this.GetToken(true);
				if (token != DtdParser.Token.LeftParen)
				{
					if (token != DtdParser.Token.ANY)
					{
						if (token != DtdParser.Token.EMPTY)
						{
							goto IL_181;
						}
						schemaElementDecl.ContentValidator = ContentValidator.Empty;
					}
					else
					{
						schemaElementDecl.ContentValidator = ContentValidator.Any;
					}
				}
				else
				{
					int startParenEntityId = this.currentEntityId;
					DtdParser.Token token2 = this.GetToken(false);
					if (token2 != DtdParser.Token.None)
					{
						if (token2 != DtdParser.Token.PCDATA)
						{
							goto IL_181;
						}
						ParticleContentValidator particleContentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
						particleContentValidator.Start();
						particleContentValidator.OpenGroup();
						this.ParseElementMixedContent(particleContentValidator, startParenEntityId);
						schemaElementDecl.ContentValidator = particleContentValidator.Finish(true);
					}
					else
					{
						ParticleContentValidator particleContentValidator2 = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
						particleContentValidator2.Start();
						particleContentValidator2.OpenGroup();
						this.ParseElementOnlyContent(particleContentValidator2, startParenEntityId);
						schemaElementDecl.ContentValidator = particleContentValidator2.Finish(true);
					}
				}
				if (this.GetToken(false) != DtdParser.Token.GreaterThan)
				{
					this.ThrowUnexpectedToken(this.curPos, ">");
				}
				return;
			}
			IL_181:
			this.OnUnexpectedError();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0005C328 File Offset: 0x0005A528
		private void ParseElementOnlyContent(ParticleContentValidator pcv, int startParenEntityId)
		{
			Stack<DtdParser.ParseElementOnlyContent_LocalFrame> stack = new Stack<DtdParser.ParseElementOnlyContent_LocalFrame>();
			DtdParser.ParseElementOnlyContent_LocalFrame parseElementOnlyContent_LocalFrame = new DtdParser.ParseElementOnlyContent_LocalFrame(startParenEntityId);
			stack.Push(parseElementOnlyContent_LocalFrame);
			for (;;)
			{
				DtdParser.Token token = this.GetToken(false);
				if (token != DtdParser.Token.QName)
				{
					if (token == DtdParser.Token.LeftParen)
					{
						pcv.OpenGroup();
						parseElementOnlyContent_LocalFrame = new DtdParser.ParseElementOnlyContent_LocalFrame(this.currentEntityId);
						stack.Push(parseElementOnlyContent_LocalFrame);
						continue;
					}
					if (token != DtdParser.Token.GreaterThan)
					{
						goto IL_148;
					}
					this.Throw(this.curPos, "Xml_InvalidContentModel");
					goto IL_14E;
				}
				else
				{
					pcv.AddName(this.GetNameQualified(true), null);
					this.ParseHowMany(pcv);
				}
				IL_78:
				DtdParser.Token token2 = this.GetToken(false);
				switch (token2)
				{
				case DtdParser.Token.RightParen:
					pcv.CloseGroup();
					if (this.validate && this.currentEntityId != parseElementOnlyContent_LocalFrame.startParenEntityId)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
					}
					this.ParseHowMany(pcv);
					break;
				case DtdParser.Token.GreaterThan:
					this.Throw(this.curPos, "Xml_InvalidContentModel");
					break;
				case DtdParser.Token.Or:
					if (parseElementOnlyContent_LocalFrame.parsingSchema == DtdParser.Token.Comma)
					{
						this.Throw(this.curPos, "Xml_InvalidContentModel");
					}
					pcv.AddChoice();
					parseElementOnlyContent_LocalFrame.parsingSchema = DtdParser.Token.Or;
					continue;
				default:
					if (token2 == DtdParser.Token.Comma)
					{
						if (parseElementOnlyContent_LocalFrame.parsingSchema == DtdParser.Token.Or)
						{
							this.Throw(this.curPos, "Xml_InvalidContentModel");
						}
						pcv.AddSequence();
						parseElementOnlyContent_LocalFrame.parsingSchema = DtdParser.Token.Comma;
						continue;
					}
					goto IL_148;
				}
				IL_14E:
				stack.Pop();
				if (stack.Count > 0)
				{
					parseElementOnlyContent_LocalFrame = stack.Peek();
					goto IL_78;
				}
				break;
				IL_148:
				this.OnUnexpectedError();
				goto IL_14E;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0005C4A0 File Offset: 0x0005A6A0
		private void ParseHowMany(ParticleContentValidator pcv)
		{
			switch (this.GetToken(false))
			{
			case DtdParser.Token.Star:
				pcv.AddStar();
				return;
			case DtdParser.Token.QMark:
				pcv.AddQMark();
				return;
			case DtdParser.Token.Plus:
				pcv.AddPlus();
				return;
			default:
				return;
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0005C4E0 File Offset: 0x0005A6E0
		private void ParseElementMixedContent(ParticleContentValidator pcv, int startParenEntityId)
		{
			bool flag = false;
			int num = -1;
			int num2 = this.currentEntityId;
			for (;;)
			{
				DtdParser.Token token = this.GetToken(false);
				if (token == DtdParser.Token.RightParen)
				{
					break;
				}
				if (token == DtdParser.Token.Or)
				{
					if (!flag)
					{
						flag = true;
					}
					else
					{
						pcv.AddChoice();
					}
					if (this.validate)
					{
						num = this.currentEntityId;
						if (num2 < num)
						{
							this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
						}
					}
					if (this.GetToken(false) == DtdParser.Token.QName)
					{
						XmlQualifiedName nameQualified = this.GetNameQualified(true);
						if (pcv.Exists(nameQualified) && this.validate)
						{
							this.SendValidationEvent(XmlSeverityType.Error, "Sch_DupElement", nameQualified.ToString());
						}
						pcv.AddName(nameQualified, null);
						if (!this.validate)
						{
							continue;
						}
						num2 = this.currentEntityId;
						if (num2 < num)
						{
							this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
							continue;
						}
						continue;
					}
				}
				this.OnUnexpectedError();
			}
			pcv.CloseGroup();
			if (this.validate && this.currentEntityId != startParenEntityId)
			{
				this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
			}
			if (this.GetToken(false) == DtdParser.Token.Star && flag)
			{
				pcv.AddStar();
				return;
			}
			if (flag)
			{
				this.ThrowUnexpectedToken(this.curPos, "*");
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0005C620 File Offset: 0x0005A820
		private void ParseEntityDecl()
		{
			bool flag = false;
			DtdParser.Token token = this.GetToken(true);
			if (token != DtdParser.Token.Name)
			{
				if (token != DtdParser.Token.Percent)
				{
					goto IL_1D6;
				}
				flag = true;
				if (this.GetToken(true) != DtdParser.Token.Name)
				{
					goto IL_1D6;
				}
			}
			XmlQualifiedName nameQualified = this.GetNameQualified(false);
			SchemaEntity schemaEntity = new SchemaEntity(nameQualified, flag);
			schemaEntity.BaseURI = this.BaseUriStr;
			schemaEntity.DeclaredURI = ((this.externalDtdBaseUri.Length == 0) ? this.documentBaseUri : this.externalDtdBaseUri);
			if (flag)
			{
				if (!this.schemaInfo.ParameterEntities.ContainsKey(nameQualified))
				{
					this.schemaInfo.ParameterEntities.Add(nameQualified, schemaEntity);
				}
			}
			else if (!this.schemaInfo.GeneralEntities.ContainsKey(nameQualified))
			{
				this.schemaInfo.GeneralEntities.Add(nameQualified, schemaEntity);
			}
			schemaEntity.DeclaredInExternal = !this.ParsingInternalSubset;
			schemaEntity.ParsingInProgress = true;
			DtdParser.Token token2 = this.GetToken(true);
			if (token2 - DtdParser.Token.PUBLIC > 1)
			{
				if (token2 != DtdParser.Token.Literal)
				{
					goto IL_1D6;
				}
				schemaEntity.Text = this.GetValue();
				schemaEntity.Line = this.literalLineInfo.lineNo;
				schemaEntity.Pos = this.literalLineInfo.linePos;
			}
			else
			{
				string pubid;
				string url;
				this.ParseExternalId(token2, DtdParser.Token.EntityDecl, out pubid, out url);
				schemaEntity.IsExternal = true;
				schemaEntity.Url = url;
				schemaEntity.Pubid = pubid;
				if (this.GetToken(false) == DtdParser.Token.NData)
				{
					if (flag)
					{
						this.ThrowUnexpectedToken(this.curPos - 5, ">");
					}
					if (!this.whitespaceSeen)
					{
						this.Throw(this.curPos - 5, "Xml_ExpectingWhiteSpace", "NDATA");
					}
					if (this.GetToken(true) != DtdParser.Token.Name)
					{
						goto IL_1D6;
					}
					schemaEntity.NData = this.GetNameQualified(false);
					string name = schemaEntity.NData.Name;
					if (!this.schemaInfo.Notations.ContainsKey(name))
					{
						this.AddUndeclaredNotation(name);
					}
				}
			}
			if (this.GetToken(false) == DtdParser.Token.GreaterThan)
			{
				schemaEntity.ParsingInProgress = false;
				return;
			}
			IL_1D6:
			this.OnUnexpectedError();
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0005C80C File Offset: 0x0005AA0C
		private void ParseNotationDecl()
		{
			if (this.GetToken(true) != DtdParser.Token.Name)
			{
				this.OnUnexpectedError();
			}
			XmlQualifiedName nameQualified = this.GetNameQualified(false);
			SchemaNotation schemaNotation = null;
			if (!this.schemaInfo.Notations.ContainsKey(nameQualified.Name))
			{
				if (this.undeclaredNotations != null)
				{
					this.undeclaredNotations.Remove(nameQualified.Name);
				}
				schemaNotation = new SchemaNotation(nameQualified);
				this.schemaInfo.Notations.Add(schemaNotation.Name.Name, schemaNotation);
			}
			else if (this.validate)
			{
				this.SendValidationEvent(this.curPos - nameQualified.Name.Length, XmlSeverityType.Error, "Sch_DupNotation", nameQualified.Name);
			}
			DtdParser.Token token = this.GetToken(true);
			if (token == DtdParser.Token.SYSTEM || token == DtdParser.Token.PUBLIC)
			{
				string pubid;
				string systemLiteral;
				this.ParseExternalId(token, DtdParser.Token.NOTATION, out pubid, out systemLiteral);
				if (schemaNotation != null)
				{
					schemaNotation.SystemLiteral = systemLiteral;
					schemaNotation.Pubid = pubid;
				}
			}
			else
			{
				this.OnUnexpectedError();
			}
			if (this.GetToken(false) != DtdParser.Token.GreaterThan)
			{
				this.OnUnexpectedError();
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0005C900 File Offset: 0x0005AB00
		private void AddUndeclaredNotation(string notationName)
		{
			if (this.undeclaredNotations == null)
			{
				this.undeclaredNotations = new Dictionary<string, DtdParser.UndeclaredNotation>();
			}
			DtdParser.UndeclaredNotation undeclaredNotation = new DtdParser.UndeclaredNotation(notationName, this.LineNo, this.LinePos - notationName.Length);
			DtdParser.UndeclaredNotation undeclaredNotation2;
			if (this.undeclaredNotations.TryGetValue(notationName, out undeclaredNotation2))
			{
				undeclaredNotation.next = undeclaredNotation2.next;
				undeclaredNotation2.next = undeclaredNotation;
				return;
			}
			this.undeclaredNotations.Add(notationName, undeclaredNotation);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0005C96C File Offset: 0x0005AB6C
		private void ParseComment()
		{
			this.SaveParsingBuffer();
			try
			{
				if (this.SaveInternalSubsetValue)
				{
					this.readerAdapter.ParseComment(this.internalSubsetValueSb);
					this.internalSubsetValueSb.Append("-->");
				}
				else
				{
					this.readerAdapter.ParseComment(null);
				}
			}
			catch (XmlException ex)
			{
				if (!(ex.ResString == "Xml_UnexpectedEOF") || this.currentEntityId == 0)
				{
					throw;
				}
				this.SendValidationEvent(XmlSeverityType.Error, "Sch_ParEntityRefNesting", null);
			}
			this.LoadParsingBuffer();
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0005CA00 File Offset: 0x0005AC00
		private void ParsePI()
		{
			this.SaveParsingBuffer();
			if (this.SaveInternalSubsetValue)
			{
				this.readerAdapter.ParsePI(this.internalSubsetValueSb);
				this.internalSubsetValueSb.Append("?>");
			}
			else
			{
				this.readerAdapter.ParsePI(null);
			}
			this.LoadParsingBuffer();
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0005CA54 File Offset: 0x0005AC54
		private void ParseCondSection()
		{
			int num = this.currentEntityId;
			DtdParser.Token token = this.GetToken(false);
			if (token != DtdParser.Token.IGNORE)
			{
				if (token == DtdParser.Token.INCLUDE && this.GetToken(false) == DtdParser.Token.LeftBracket)
				{
					if (this.validate && num != this.currentEntityId)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
					}
					if (this.validate)
					{
						if (this.condSectionEntityIds == null)
						{
							this.condSectionEntityIds = new int[2];
						}
						else if (this.condSectionEntityIds.Length == this.condSectionDepth)
						{
							int[] destinationArray = new int[this.condSectionEntityIds.Length * 2];
							Array.Copy(this.condSectionEntityIds, 0, destinationArray, 0, this.condSectionEntityIds.Length);
							this.condSectionEntityIds = destinationArray;
						}
						this.condSectionEntityIds[this.condSectionDepth] = num;
					}
					this.condSectionDepth++;
					return;
				}
			}
			else if (this.GetToken(false) == DtdParser.Token.LeftBracket)
			{
				if (this.validate && num != this.currentEntityId)
				{
					this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
				}
				if (this.GetToken(false) == DtdParser.Token.CondSectionEnd)
				{
					if (this.validate && num != this.currentEntityId)
					{
						this.SendValidationEvent(this.curPos, XmlSeverityType.Error, "Sch_ParEntityRefNesting", string.Empty);
						return;
					}
					return;
				}
			}
			this.OnUnexpectedError();
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0005CB9C File Offset: 0x0005AD9C
		private void ParseExternalId(DtdParser.Token idTokenType, DtdParser.Token declType, out string publicId, out string systemId)
		{
			LineInfo keywordLineInfo = new LineInfo(this.LineNo, this.LinePos - 6);
			publicId = null;
			systemId = null;
			if (this.GetToken(true) != DtdParser.Token.Literal)
			{
				this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			}
			if (idTokenType == DtdParser.Token.SYSTEM)
			{
				systemId = this.GetValue();
				if (systemId.IndexOf('#') >= 0)
				{
					this.Throw(this.curPos - systemId.Length - 1, "Xml_FragmentId", new string[]
					{
						systemId.Substring(systemId.IndexOf('#')),
						systemId
					});
				}
				if (declType == DtdParser.Token.DOCTYPE && !this.freeFloatingDtd)
				{
					this.literalLineInfo.linePos = this.literalLineInfo.linePos + 1;
					this.readerAdapter.OnSystemId(systemId, keywordLineInfo, this.literalLineInfo);
					return;
				}
			}
			else
			{
				publicId = this.GetValue();
				int num;
				if ((num = this.xmlCharType.IsPublicId(publicId)) >= 0)
				{
					this.ThrowInvalidChar(this.curPos - 1 - publicId.Length + num, publicId, num);
				}
				if (declType == DtdParser.Token.DOCTYPE && !this.freeFloatingDtd)
				{
					this.literalLineInfo.linePos = this.literalLineInfo.linePos + 1;
					this.readerAdapter.OnPublicId(publicId, keywordLineInfo, this.literalLineInfo);
					if (this.GetToken(false) == DtdParser.Token.Literal)
					{
						if (!this.whitespaceSeen)
						{
							this.Throw("Xml_ExpectingWhiteSpace", new string(this.literalQuoteChar, 1), this.literalLineInfo.lineNo, this.literalLineInfo.linePos);
						}
						systemId = this.GetValue();
						this.literalLineInfo.linePos = this.literalLineInfo.linePos + 1;
						this.readerAdapter.OnSystemId(systemId, keywordLineInfo, this.literalLineInfo);
						return;
					}
					this.ThrowUnexpectedToken(this.curPos, "\"", "'");
					return;
				}
				else
				{
					if (this.GetToken(false) == DtdParser.Token.Literal)
					{
						if (!this.whitespaceSeen)
						{
							this.Throw("Xml_ExpectingWhiteSpace", new string(this.literalQuoteChar, 1), this.literalLineInfo.lineNo, this.literalLineInfo.linePos);
						}
						systemId = this.GetValue();
						return;
					}
					if (declType != DtdParser.Token.NOTATION)
					{
						this.ThrowUnexpectedToken(this.curPos, "\"", "'");
					}
				}
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0005CDD0 File Offset: 0x0005AFD0
		private DtdParser.Token GetToken(bool needWhiteSpace)
		{
			this.whitespaceSeen = false;
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							goto IL_14D;
						case '\n':
							this.whitespaceSeen = true;
							this.curPos++;
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						case '\r':
							this.whitespaceSeen = true;
							if (this.chars[this.curPos + 1] == '\n')
							{
								if (this.Normalize)
								{
									this.SaveParsingBuffer();
									IDtdParserAdapter dtdParserAdapter = this.readerAdapter;
									int currentPosition = dtdParserAdapter.CurrentPosition;
									dtdParserAdapter.CurrentPosition = currentPosition + 1;
								}
								this.curPos += 2;
							}
							else
							{
								if (this.curPos + 1 >= this.charsUsed && !this.readerAdapter.IsEof)
								{
									goto IL_388;
								}
								this.chars[this.curPos] = '\n';
								this.curPos++;
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						}
						break;
					}
					if (this.curPos != this.charsUsed)
					{
						this.ThrowInvalidChar(this.chars, this.charsUsed, this.curPos);
						goto IL_388;
					}
					goto IL_388;
				}
				else if (c != ' ')
				{
					if (c != '%')
					{
						break;
					}
					if (this.charsUsed - this.curPos < 2)
					{
						goto IL_388;
					}
					if (this.xmlCharType.IsWhiteSpace(this.chars[this.curPos + 1]))
					{
						break;
					}
					if (this.IgnoreEntityReferences)
					{
						this.curPos++;
						continue;
					}
					this.HandleEntityReference(true, false, false);
					continue;
				}
				IL_14D:
				this.whitespaceSeen = true;
				this.curPos++;
				continue;
				IL_388:
				if ((this.readerAdapter.IsEof || this.ReadData() == 0) && !this.HandleEntityEnd(false))
				{
					if (this.scanningFunction == DtdParser.ScanningFunction.SubsetContent)
					{
						return DtdParser.Token.Eof;
					}
					this.Throw(this.curPos, "Xml_IncompleteDtdContent");
				}
			}
			if (needWhiteSpace && !this.whitespaceSeen && this.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
			{
				this.Throw(this.curPos, "Xml_ExpectingWhiteSpace", this.ParseUnexpectedToken(this.curPos));
			}
			this.tokenStartPos = this.curPos;
			for (;;)
			{
				switch (this.scanningFunction)
				{
				case DtdParser.ScanningFunction.SubsetContent:
					goto IL_2A9;
				case DtdParser.ScanningFunction.Name:
					goto IL_294;
				case DtdParser.ScanningFunction.QName:
					goto IL_29B;
				case DtdParser.ScanningFunction.Nmtoken:
					goto IL_2A2;
				case DtdParser.ScanningFunction.Doctype1:
					goto IL_2B0;
				case DtdParser.ScanningFunction.Doctype2:
					goto IL_2B7;
				case DtdParser.ScanningFunction.Element1:
					goto IL_2BE;
				case DtdParser.ScanningFunction.Element2:
					goto IL_2C5;
				case DtdParser.ScanningFunction.Element3:
					goto IL_2CC;
				case DtdParser.ScanningFunction.Element4:
					goto IL_2D3;
				case DtdParser.ScanningFunction.Element5:
					goto IL_2DA;
				case DtdParser.ScanningFunction.Element6:
					goto IL_2E1;
				case DtdParser.ScanningFunction.Element7:
					goto IL_2E8;
				case DtdParser.ScanningFunction.Attlist1:
					goto IL_2EF;
				case DtdParser.ScanningFunction.Attlist2:
					goto IL_2F6;
				case DtdParser.ScanningFunction.Attlist3:
					goto IL_2FD;
				case DtdParser.ScanningFunction.Attlist4:
					goto IL_304;
				case DtdParser.ScanningFunction.Attlist5:
					goto IL_30B;
				case DtdParser.ScanningFunction.Attlist6:
					goto IL_312;
				case DtdParser.ScanningFunction.Attlist7:
					goto IL_319;
				case DtdParser.ScanningFunction.Entity1:
					goto IL_33C;
				case DtdParser.ScanningFunction.Entity2:
					goto IL_343;
				case DtdParser.ScanningFunction.Entity3:
					goto IL_34A;
				case DtdParser.ScanningFunction.Notation1:
					goto IL_320;
				case DtdParser.ScanningFunction.CondSection1:
					goto IL_351;
				case DtdParser.ScanningFunction.CondSection2:
					goto IL_358;
				case DtdParser.ScanningFunction.CondSection3:
					goto IL_35F;
				case DtdParser.ScanningFunction.SystemId:
					goto IL_327;
				case DtdParser.ScanningFunction.PublicId1:
					goto IL_32E;
				case DtdParser.ScanningFunction.PublicId2:
					goto IL_335;
				case DtdParser.ScanningFunction.ClosingTag:
					goto IL_366;
				case DtdParser.ScanningFunction.ParamEntitySpace:
					this.whitespaceSeen = true;
					this.scanningFunction = this.savedScanningFunction;
					continue;
				}
				break;
			}
			return DtdParser.Token.None;
			IL_294:
			return this.ScanNameExpected();
			IL_29B:
			return this.ScanQNameExpected();
			IL_2A2:
			return this.ScanNmtokenExpected();
			IL_2A9:
			return this.ScanSubsetContent();
			IL_2B0:
			return this.ScanDoctype1();
			IL_2B7:
			return this.ScanDoctype2();
			IL_2BE:
			return this.ScanElement1();
			IL_2C5:
			return this.ScanElement2();
			IL_2CC:
			return this.ScanElement3();
			IL_2D3:
			return this.ScanElement4();
			IL_2DA:
			return this.ScanElement5();
			IL_2E1:
			return this.ScanElement6();
			IL_2E8:
			return this.ScanElement7();
			IL_2EF:
			return this.ScanAttlist1();
			IL_2F6:
			return this.ScanAttlist2();
			IL_2FD:
			return this.ScanAttlist3();
			IL_304:
			return this.ScanAttlist4();
			IL_30B:
			return this.ScanAttlist5();
			IL_312:
			return this.ScanAttlist6();
			IL_319:
			return this.ScanAttlist7();
			IL_320:
			return this.ScanNotation1();
			IL_327:
			return this.ScanSystemId();
			IL_32E:
			return this.ScanPublicId1();
			IL_335:
			return this.ScanPublicId2();
			IL_33C:
			return this.ScanEntity1();
			IL_343:
			return this.ScanEntity2();
			IL_34A:
			return this.ScanEntity3();
			IL_351:
			return this.ScanCondSection1();
			IL_358:
			return this.ScanCondSection2();
			IL_35F:
			return this.ScanCondSection3();
			IL_366:
			return this.ScanClosingTag();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005D1AC File Offset: 0x0005B3AC
		private DtdParser.Token ScanSubsetContent()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c != '<')
				{
					if (c == ']')
					{
						if (this.charsUsed - this.curPos < 2 && !this.readerAdapter.IsEof)
						{
							goto IL_513;
						}
						if (this.chars[this.curPos + 1] != ']')
						{
							goto Block_40;
						}
						if (this.charsUsed - this.curPos < 3 && !this.readerAdapter.IsEof)
						{
							goto IL_513;
						}
						if (this.chars[this.curPos + 1] == ']' && this.chars[this.curPos + 2] == '>')
						{
							goto Block_43;
						}
					}
					if (this.charsUsed - this.curPos != 0)
					{
						this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
					}
				}
				else
				{
					char c2 = this.chars[this.curPos + 1];
					if (c2 != '!')
					{
						if (c2 == '?')
						{
							goto IL_41B;
						}
						if (this.charsUsed - this.curPos >= 2)
						{
							goto Block_38;
						}
					}
					else
					{
						char c3 = this.chars[this.curPos + 2];
						if (c3 <= 'A')
						{
							if (c3 != '-')
							{
								if (c3 == 'A')
								{
									if (this.charsUsed - this.curPos >= 9)
									{
										goto Block_22;
									}
									goto IL_513;
								}
							}
							else
							{
								if (this.chars[this.curPos + 3] == '-')
								{
									goto Block_35;
								}
								if (this.charsUsed - this.curPos >= 4)
								{
									this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
									goto IL_513;
								}
								goto IL_513;
							}
						}
						else if (c3 != 'E')
						{
							if (c3 != 'N')
							{
								if (c3 == '[')
								{
									goto IL_38A;
								}
							}
							else
							{
								if (this.charsUsed - this.curPos >= 10)
								{
									goto Block_28;
								}
								goto IL_513;
							}
						}
						else if (this.chars[this.curPos + 3] == 'L')
						{
							if (this.charsUsed - this.curPos >= 9)
							{
								break;
							}
							goto IL_513;
						}
						else if (this.chars[this.curPos + 3] == 'N')
						{
							if (this.charsUsed - this.curPos >= 8)
							{
								goto Block_17;
							}
							goto IL_513;
						}
						else
						{
							if (this.charsUsed - this.curPos >= 4)
							{
								goto Block_21;
							}
							goto IL_513;
						}
						if (this.charsUsed - this.curPos >= 3)
						{
							this.Throw(this.curPos + 2, "Xml_ExpectDtdMarkup");
						}
					}
				}
				IL_513:
				if (this.ReadData() == 0)
				{
					this.Throw(this.charsUsed, "Xml_IncompleteDtdContent");
				}
			}
			if (this.chars[this.curPos + 4] != 'E' || this.chars[this.curPos + 5] != 'M' || this.chars[this.curPos + 6] != 'E' || this.chars[this.curPos + 7] != 'N' || this.chars[this.curPos + 8] != 'T')
			{
				this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			}
			this.curPos += 9;
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			this.nextScaningFunction = DtdParser.ScanningFunction.Element1;
			return DtdParser.Token.ElementDecl;
			Block_17:
			if (this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'T' || this.chars[this.curPos + 7] != 'Y')
			{
				this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			}
			this.curPos += 8;
			this.scanningFunction = DtdParser.ScanningFunction.Entity1;
			return DtdParser.Token.EntityDecl;
			Block_21:
			this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			return DtdParser.Token.None;
			Block_22:
			if (this.chars[this.curPos + 3] != 'T' || this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'L' || this.chars[this.curPos + 6] != 'I' || this.chars[this.curPos + 7] != 'S' || this.chars[this.curPos + 8] != 'T')
			{
				this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			}
			this.curPos += 9;
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.AttlistDecl;
			Block_28:
			if (this.chars[this.curPos + 3] != 'O' || this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'A' || this.chars[this.curPos + 6] != 'T' || this.chars[this.curPos + 7] != 'I' || this.chars[this.curPos + 8] != 'O' || this.chars[this.curPos + 9] != 'N')
			{
				this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			}
			this.curPos += 10;
			this.scanningFunction = DtdParser.ScanningFunction.Name;
			this.nextScaningFunction = DtdParser.ScanningFunction.Notation1;
			return DtdParser.Token.NotationDecl;
			IL_38A:
			this.curPos += 3;
			this.scanningFunction = DtdParser.ScanningFunction.CondSection1;
			return DtdParser.Token.CondSectionStart;
			Block_35:
			this.curPos += 4;
			return DtdParser.Token.Comment;
			IL_41B:
			this.curPos += 2;
			return DtdParser.Token.PI;
			Block_38:
			this.Throw(this.curPos, "Xml_ExpectDtdMarkup");
			return DtdParser.Token.None;
			Block_40:
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.RightBracket;
			Block_43:
			this.curPos += 3;
			return DtdParser.Token.CondSectionEnd;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005D6EC File Offset: 0x0005B8EC
		private DtdParser.Token ScanNameExpected()
		{
			this.ScanName();
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Name;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005D702 File Offset: 0x0005B902
		private DtdParser.Token ScanQNameExpected()
		{
			this.ScanQName();
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.QName;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0005D718 File Offset: 0x0005B918
		private DtdParser.Token ScanNmtokenExpected()
		{
			this.ScanNmtoken();
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Nmtoken;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0005D730 File Offset: 0x0005B930
		private DtdParser.Token ScanDoctype1()
		{
			char c = this.chars[this.curPos];
			if (c <= 'P')
			{
				if (c == '>')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					return DtdParser.Token.GreaterThan;
				}
				if (c == 'P')
				{
					if (!this.EatPublicKeyword())
					{
						this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Doctype2;
					this.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					return DtdParser.Token.PUBLIC;
				}
			}
			else
			{
				if (c == 'S')
				{
					if (!this.EatSystemKeyword())
					{
						this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Doctype2;
					this.scanningFunction = DtdParser.ScanningFunction.SystemId;
					return DtdParser.Token.SYSTEM;
				}
				if (c == '[')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					return DtdParser.Token.LeftBracket;
				}
			}
			this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
			return DtdParser.Token.None;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0005D80C File Offset: 0x0005BA0C
		private DtdParser.Token ScanDoctype2()
		{
			char c = this.chars[this.curPos];
			if (c == '>')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
				return DtdParser.Token.GreaterThan;
			}
			if (c == '[')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
				return DtdParser.Token.LeftBracket;
			}
			this.Throw(this.curPos, "Xml_ExpectSubOrClose");
			return DtdParser.Token.None;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0005D874 File Offset: 0x0005BA74
		private DtdParser.Token ScanClosingTag()
		{
			if (this.chars[this.curPos] != '>')
			{
				this.ThrowUnexpectedToken(this.curPos, ">");
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
			return DtdParser.Token.GreaterThan;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0005D8B0 File Offset: 0x0005BAB0
		private DtdParser.Token ScanElement1()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c != '(')
				{
					if (c != 'A')
					{
						if (c != 'E')
						{
							goto IL_10A;
						}
						if (this.charsUsed - this.curPos >= 5)
						{
							if (this.chars[this.curPos + 1] == 'M' && this.chars[this.curPos + 2] == 'P' && this.chars[this.curPos + 3] == 'T' && this.chars[this.curPos + 4] == 'Y')
							{
								goto Block_7;
							}
							goto IL_10A;
						}
					}
					else if (this.charsUsed - this.curPos >= 3)
					{
						if (this.chars[this.curPos + 1] == 'N' && this.chars[this.curPos + 2] == 'Y')
						{
							goto Block_10;
						}
						goto IL_10A;
					}
					IL_11B:
					if (this.ReadData() == 0)
					{
						this.Throw(this.curPos, "Xml_IncompleteDtdContent");
						continue;
					}
					continue;
					IL_10A:
					this.Throw(this.curPos, "Xml_InvalidContentModel");
					goto IL_11B;
				}
				break;
			}
			this.scanningFunction = DtdParser.ScanningFunction.Element2;
			this.curPos++;
			return DtdParser.Token.LeftParen;
			Block_7:
			this.curPos += 5;
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.EMPTY;
			Block_10:
			this.curPos += 3;
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.ANY;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0005D9F8 File Offset: 0x0005BBF8
		private DtdParser.Token ScanElement2()
		{
			if (this.chars[this.curPos] == '#')
			{
				while (this.charsUsed - this.curPos < 7)
				{
					if (this.ReadData() == 0)
					{
						this.Throw(this.curPos, "Xml_IncompleteDtdContent");
					}
				}
				if (this.chars[this.curPos + 1] == 'P' && this.chars[this.curPos + 2] == 'C' && this.chars[this.curPos + 3] == 'D' && this.chars[this.curPos + 4] == 'A' && this.chars[this.curPos + 5] == 'T' && this.chars[this.curPos + 6] == 'A')
				{
					this.curPos += 7;
					this.scanningFunction = DtdParser.ScanningFunction.Element6;
					return DtdParser.Token.PCDATA;
				}
				this.Throw(this.curPos + 1, "Xml_ExpectPcData");
			}
			this.scanningFunction = DtdParser.ScanningFunction.Element3;
			return DtdParser.Token.None;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0005DAEC File Offset: 0x0005BCEC
		private DtdParser.Token ScanElement3()
		{
			char c = this.chars[this.curPos];
			if (c == '(')
			{
				this.curPos++;
				return DtdParser.Token.LeftParen;
			}
			if (c != '>')
			{
				this.ScanQName();
				this.scanningFunction = DtdParser.ScanningFunction.Element4;
				return DtdParser.Token.QName;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
			return DtdParser.Token.GreaterThan;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005DB4C File Offset: 0x0005BD4C
		private DtdParser.Token ScanElement4()
		{
			this.scanningFunction = DtdParser.ScanningFunction.Element5;
			char c = this.chars[this.curPos];
			DtdParser.Token result;
			if (c != '*')
			{
				if (c != '+')
				{
					if (c != '?')
					{
						return DtdParser.Token.None;
					}
					result = DtdParser.Token.QMark;
				}
				else
				{
					result = DtdParser.Token.Plus;
				}
			}
			else
			{
				result = DtdParser.Token.Star;
			}
			if (this.whitespaceSeen)
			{
				this.Throw(this.curPos, "Xml_ExpectNoWhitespace");
			}
			this.curPos++;
			return result;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0005DBBC File Offset: 0x0005BDBC
		private DtdParser.Token ScanElement5()
		{
			char c = this.chars[this.curPos];
			if (c <= ',')
			{
				if (c == ')')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.Element4;
					return DtdParser.Token.RightParen;
				}
				if (c == ',')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.Element3;
					return DtdParser.Token.Comma;
				}
			}
			else
			{
				if (c == '>')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
					return DtdParser.Token.GreaterThan;
				}
				if (c == '|')
				{
					this.curPos++;
					this.scanningFunction = DtdParser.ScanningFunction.Element3;
					return DtdParser.Token.Or;
				}
			}
			this.Throw(this.curPos, "Xml_ExpectOp");
			return DtdParser.Token.None;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0005DC68 File Offset: 0x0005BE68
		private DtdParser.Token ScanElement6()
		{
			char c = this.chars[this.curPos];
			if (c == ')')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Element7;
				return DtdParser.Token.RightParen;
			}
			if (c != '|')
			{
				this.ThrowUnexpectedToken(this.curPos, ")", "|");
				return DtdParser.Token.None;
			}
			this.curPos++;
			this.nextScaningFunction = DtdParser.ScanningFunction.Element6;
			this.scanningFunction = DtdParser.ScanningFunction.QName;
			return DtdParser.Token.Or;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0005DCE0 File Offset: 0x0005BEE0
		private DtdParser.Token ScanElement7()
		{
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			if (this.chars[this.curPos] == '*' && !this.whitespaceSeen)
			{
				this.curPos++;
				return DtdParser.Token.Star;
			}
			return DtdParser.Token.None;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0005DD18 File Offset: 0x0005BF18
		private DtdParser.Token ScanAttlist1()
		{
			char c = this.chars[this.curPos];
			if (c == '>')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
				return DtdParser.Token.GreaterThan;
			}
			if (!this.whitespaceSeen)
			{
				this.Throw(this.curPos, "Xml_ExpectingWhiteSpace", this.ParseUnexpectedToken(this.curPos));
			}
			this.ScanQName();
			this.scanningFunction = DtdParser.ScanningFunction.Attlist2;
			return DtdParser.Token.QName;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005DD88 File Offset: 0x0005BF88
		private DtdParser.Token ScanAttlist2()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c <= 'C')
				{
					if (c == '(')
					{
						break;
					}
					if (c != 'C')
					{
						goto IL_44E;
					}
					if (this.charsUsed - this.curPos >= 5)
					{
						goto Block_6;
					}
				}
				else if (c != 'E')
				{
					if (c != 'I')
					{
						if (c != 'N')
						{
							goto IL_44E;
						}
						if (this.charsUsed - this.curPos >= 8 || this.readerAdapter.IsEof)
						{
							char c2 = this.chars[this.curPos + 1];
							if (c2 == 'M')
							{
								goto IL_390;
							}
							if (c2 == 'O')
							{
								goto Block_24;
							}
							this.Throw(this.curPos, "Xml_InvalidAttributeType");
						}
					}
					else if (this.charsUsed - this.curPos >= 6)
					{
						goto Block_17;
					}
				}
				else if (this.charsUsed - this.curPos >= 9)
				{
					this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
					if (this.chars[this.curPos + 1] != 'N' || this.chars[this.curPos + 2] != 'T' || this.chars[this.curPos + 3] != 'I' || this.chars[this.curPos + 4] != 'T')
					{
						this.Throw(this.curPos, "Xml_InvalidAttributeType");
					}
					char c3 = this.chars[this.curPos + 5];
					if (c3 == 'I')
					{
						goto IL_17C;
					}
					if (c3 == 'Y')
					{
						goto IL_1C3;
					}
					this.Throw(this.curPos, "Xml_InvalidAttributeType");
				}
				IL_45F:
				if (this.ReadData() == 0)
				{
					this.Throw(this.curPos, "Xml_IncompleteDtdContent");
					continue;
				}
				continue;
				IL_44E:
				this.Throw(this.curPos, "Xml_InvalidAttributeType");
				goto IL_45F;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.Nmtoken;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist5;
			return DtdParser.Token.LeftParen;
			Block_6:
			if (this.chars[this.curPos + 1] != 'D' || this.chars[this.curPos + 2] != 'A' || this.chars[this.curPos + 3] != 'T' || this.chars[this.curPos + 4] != 'A')
			{
				this.Throw(this.curPos, "Xml_InvalidAttributeType1");
			}
			this.curPos += 5;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
			return DtdParser.Token.CDATA;
			IL_17C:
			if (this.chars[this.curPos + 6] != 'E' || this.chars[this.curPos + 7] != 'S')
			{
				this.Throw(this.curPos, "Xml_InvalidAttributeType");
			}
			this.curPos += 8;
			return DtdParser.Token.ENTITIES;
			IL_1C3:
			this.curPos += 6;
			return DtdParser.Token.ENTITY;
			Block_17:
			this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
			if (this.chars[this.curPos + 1] != 'D')
			{
				this.Throw(this.curPos, "Xml_InvalidAttributeType");
			}
			if (this.chars[this.curPos + 2] != 'R')
			{
				this.curPos += 2;
				return DtdParser.Token.ID;
			}
			if (this.chars[this.curPos + 3] != 'E' || this.chars[this.curPos + 4] != 'F')
			{
				this.Throw(this.curPos, "Xml_InvalidAttributeType");
			}
			if (this.chars[this.curPos + 5] != 'S')
			{
				this.curPos += 5;
				return DtdParser.Token.IDREF;
			}
			this.curPos += 6;
			return DtdParser.Token.IDREFS;
			Block_24:
			if (this.chars[this.curPos + 2] != 'T' || this.chars[this.curPos + 3] != 'A' || this.chars[this.curPos + 4] != 'T' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'O' || this.chars[this.curPos + 7] != 'N')
			{
				this.Throw(this.curPos, "Xml_InvalidAttributeType");
			}
			this.curPos += 8;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist3;
			return DtdParser.Token.NOTATION;
			IL_390:
			if (this.chars[this.curPos + 2] != 'T' || this.chars[this.curPos + 3] != 'O' || this.chars[this.curPos + 4] != 'K' || this.chars[this.curPos + 5] != 'E' || this.chars[this.curPos + 6] != 'N')
			{
				this.Throw(this.curPos, "Xml_InvalidAttributeType");
			}
			this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
			if (this.chars[this.curPos + 7] == 'S')
			{
				this.curPos += 8;
				return DtdParser.Token.NMTOKENS;
			}
			this.curPos += 7;
			return DtdParser.Token.NMTOKEN;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0005E214 File Offset: 0x0005C414
		private DtdParser.Token ScanAttlist3()
		{
			if (this.chars[this.curPos] == '(')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Name;
				this.nextScaningFunction = DtdParser.ScanningFunction.Attlist4;
				return DtdParser.Token.LeftParen;
			}
			this.ThrowUnexpectedToken(this.curPos, "(");
			return DtdParser.Token.None;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0005E268 File Offset: 0x0005C468
		private DtdParser.Token ScanAttlist4()
		{
			char c = this.chars[this.curPos];
			if (c == ')')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
				return DtdParser.Token.RightParen;
			}
			if (c != '|')
			{
				this.ThrowUnexpectedToken(this.curPos, ")", "|");
				return DtdParser.Token.None;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.Name;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist4;
			return DtdParser.Token.Or;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
		private DtdParser.Token ScanAttlist5()
		{
			char c = this.chars[this.curPos];
			if (c == ')')
			{
				this.curPos++;
				this.scanningFunction = DtdParser.ScanningFunction.Attlist6;
				return DtdParser.Token.RightParen;
			}
			if (c != '|')
			{
				this.ThrowUnexpectedToken(this.curPos, ")", "|");
				return DtdParser.Token.None;
			}
			this.curPos++;
			this.scanningFunction = DtdParser.ScanningFunction.Nmtoken;
			this.nextScaningFunction = DtdParser.ScanningFunction.Attlist5;
			return DtdParser.Token.Or;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0005E358 File Offset: 0x0005C558
		private DtdParser.Token ScanAttlist6()
		{
			for (;;)
			{
				char c = this.chars[this.curPos];
				if (c == '"')
				{
					break;
				}
				if (c != '#')
				{
					if (c == '\'')
					{
						break;
					}
					this.Throw(this.curPos, "Xml_ExpectAttType");
				}
				else if (this.charsUsed - this.curPos >= 6)
				{
					char c2 = this.chars[this.curPos + 1];
					if (c2 == 'F')
					{
						goto IL_1E1;
					}
					if (c2 != 'I')
					{
						if (c2 == 'R')
						{
							if (this.charsUsed - this.curPos >= 9)
							{
								goto Block_6;
							}
						}
						else
						{
							this.Throw(this.curPos, "Xml_ExpectAttType");
						}
					}
					else if (this.charsUsed - this.curPos >= 8)
					{
						goto Block_13;
					}
				}
				if (this.ReadData() == 0)
				{
					this.Throw(this.curPos, "Xml_IncompleteDtdContent");
				}
			}
			this.ScanLiteral(DtdParser.LiteralType.AttributeValue);
			this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.Literal;
			Block_6:
			if (this.chars[this.curPos + 2] != 'E' || this.chars[this.curPos + 3] != 'Q' || this.chars[this.curPos + 4] != 'U' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'R' || this.chars[this.curPos + 7] != 'E' || this.chars[this.curPos + 8] != 'D')
			{
				this.Throw(this.curPos, "Xml_ExpectAttType");
			}
			this.curPos += 9;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.REQUIRED;
			Block_13:
			if (this.chars[this.curPos + 2] != 'M' || this.chars[this.curPos + 3] != 'P' || this.chars[this.curPos + 4] != 'L' || this.chars[this.curPos + 5] != 'I' || this.chars[this.curPos + 6] != 'E' || this.chars[this.curPos + 7] != 'D')
			{
				this.Throw(this.curPos, "Xml_ExpectAttType");
			}
			this.curPos += 8;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
			return DtdParser.Token.IMPLIED;
			IL_1E1:
			if (this.chars[this.curPos + 2] != 'I' || this.chars[this.curPos + 3] != 'X' || this.chars[this.curPos + 4] != 'E' || this.chars[this.curPos + 5] != 'D')
			{
				this.Throw(this.curPos, "Xml_ExpectAttType");
			}
			this.curPos += 6;
			this.scanningFunction = DtdParser.ScanningFunction.Attlist7;
			return DtdParser.Token.FIXED;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0005E600 File Offset: 0x0005C800
		private DtdParser.Token ScanAttlist7()
		{
			char c = this.chars[this.curPos];
			if (c == '"' || c == '\'')
			{
				this.ScanLiteral(DtdParser.LiteralType.AttributeValue);
				this.scanningFunction = DtdParser.ScanningFunction.Attlist1;
				return DtdParser.Token.Literal;
			}
			this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			return DtdParser.Token.None;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0005E650 File Offset: 0x0005C850
		private unsafe DtdParser.Token ScanLiteral(DtdParser.LiteralType literalType)
		{
			char c = this.chars[this.curPos];
			char value = (literalType == DtdParser.LiteralType.AttributeValue) ? ' ' : '\n';
			int num = this.currentEntityId;
			this.literalLineInfo.Set(this.LineNo, this.LinePos);
			this.curPos++;
			this.tokenStartPos = this.curPos;
			this.stringBuilder.Length = 0;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[this.chars[this.curPos]] & 128) == 0 || this.chars[this.curPos] == '%')
				{
					if (this.chars[this.curPos] == c && this.currentEntityId == num)
					{
						break;
					}
					int num2 = this.curPos - this.tokenStartPos;
					if (num2 > 0)
					{
						this.stringBuilder.Append(this.chars, this.tokenStartPos, num2);
						this.tokenStartPos = this.curPos;
					}
					char c2 = this.chars[this.curPos];
					if (c2 <= '\'')
					{
						switch (c2)
						{
						case '\t':
							if (literalType == DtdParser.LiteralType.AttributeValue && this.Normalize)
							{
								this.stringBuilder.Append(' ');
								this.tokenStartPos++;
							}
							this.curPos++;
							continue;
						case '\n':
							this.curPos++;
							if (this.Normalize)
							{
								this.stringBuilder.Append(value);
								this.tokenStartPos = this.curPos;
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						case '\v':
						case '\f':
							goto IL_54E;
						case '\r':
							if (this.chars[this.curPos + 1] == '\n')
							{
								if (this.Normalize)
								{
									if (literalType == DtdParser.LiteralType.AttributeValue)
									{
										this.stringBuilder.Append(this.readerAdapter.IsEntityEolNormalized ? "  " : " ");
									}
									else
									{
										this.stringBuilder.Append(this.readerAdapter.IsEntityEolNormalized ? "\r\n" : "\n");
									}
									this.tokenStartPos = this.curPos + 2;
									this.SaveParsingBuffer();
									IDtdParserAdapter dtdParserAdapter = this.readerAdapter;
									int currentPosition = dtdParserAdapter.CurrentPosition;
									dtdParserAdapter.CurrentPosition = currentPosition + 1;
								}
								this.curPos += 2;
							}
							else
							{
								if (this.curPos + 1 == this.charsUsed)
								{
									goto IL_5D4;
								}
								this.curPos++;
								if (this.Normalize)
								{
									this.stringBuilder.Append(value);
									this.tokenStartPos = this.curPos;
								}
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						default:
							switch (c2)
							{
							case '"':
							case '\'':
								break;
							case '#':
							case '$':
								goto IL_54E;
							case '%':
								if (literalType != DtdParser.LiteralType.EntityReplText)
								{
									this.curPos++;
									continue;
								}
								this.HandleEntityReference(true, true, literalType == DtdParser.LiteralType.AttributeValue);
								this.tokenStartPos = this.curPos;
								continue;
							case '&':
								if (literalType == DtdParser.LiteralType.SystemOrPublicID)
								{
									this.curPos++;
									continue;
								}
								if (this.curPos + 1 == this.charsUsed)
								{
									goto IL_5D4;
								}
								if (this.chars[this.curPos + 1] == '#')
								{
									this.SaveParsingBuffer();
									int num3 = this.readerAdapter.ParseNumericCharRef(this.SaveInternalSubsetValue ? this.internalSubsetValueSb : null);
									this.LoadParsingBuffer();
									this.stringBuilder.Append(this.chars, this.curPos, num3 - this.curPos);
									this.readerAdapter.CurrentPosition = num3;
									this.tokenStartPos = num3;
									this.curPos = num3;
									continue;
								}
								this.SaveParsingBuffer();
								if (literalType == DtdParser.LiteralType.AttributeValue)
								{
									int num4 = this.readerAdapter.ParseNamedCharRef(true, this.SaveInternalSubsetValue ? this.internalSubsetValueSb : null);
									this.LoadParsingBuffer();
									if (num4 >= 0)
									{
										this.stringBuilder.Append(this.chars, this.curPos, num4 - this.curPos);
										this.readerAdapter.CurrentPosition = num4;
										this.tokenStartPos = num4;
										this.curPos = num4;
										continue;
									}
									this.HandleEntityReference(false, true, true);
									this.tokenStartPos = this.curPos;
									continue;
								}
								else
								{
									int num5 = this.readerAdapter.ParseNamedCharRef(false, null);
									this.LoadParsingBuffer();
									if (num5 >= 0)
									{
										this.tokenStartPos = this.curPos;
										this.curPos = num5;
										continue;
									}
									this.stringBuilder.Append('&');
									this.curPos++;
									this.tokenStartPos = this.curPos;
									XmlQualifiedName entityName = this.ScanEntityName();
									this.VerifyEntityReference(entityName, false, false, false);
									continue;
								}
								break;
							default:
								goto IL_54E;
							}
							break;
						}
					}
					else
					{
						if (c2 == '<')
						{
							if (literalType == DtdParser.LiteralType.AttributeValue)
							{
								this.Throw(this.curPos, "Xml_BadAttributeChar", XmlException.BuildCharExceptionArgs('<', '\0'));
							}
							this.curPos++;
							continue;
						}
						if (c2 != '>')
						{
							goto IL_54E;
						}
					}
					this.curPos++;
					continue;
					IL_54E:
					if (this.curPos != this.charsUsed)
					{
						char ch = this.chars[this.curPos];
						if (!XmlCharType.IsHighSurrogate((int)ch))
						{
							goto IL_5B9;
						}
						if (this.curPos + 1 != this.charsUsed)
						{
							this.curPos++;
							if (XmlCharType.IsLowSurrogate((int)this.chars[this.curPos]))
							{
								this.curPos++;
								continue;
							}
							goto IL_5B9;
						}
					}
					IL_5D4:
					if ((this.readerAdapter.IsEof || this.ReadData() == 0) && (literalType == DtdParser.LiteralType.SystemOrPublicID || !this.HandleEntityEnd(true)))
					{
						this.Throw(this.curPos, "Xml_UnclosedQuote");
					}
					this.tokenStartPos = this.curPos;
				}
				else
				{
					this.curPos++;
				}
			}
			if (this.stringBuilder.Length > 0)
			{
				this.stringBuilder.Append(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos);
			}
			this.curPos++;
			this.literalQuoteChar = c;
			return DtdParser.Token.Literal;
			IL_5B9:
			this.ThrowInvalidChar(this.chars, this.charsUsed, this.curPos);
			return DtdParser.Token.None;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0005EC74 File Offset: 0x0005CE74
		private XmlQualifiedName ScanEntityName()
		{
			try
			{
				this.ScanName();
			}
			catch (XmlException ex)
			{
				this.Throw("Xml_ErrorParsingEntityName", string.Empty, ex.LineNumber, ex.LinePosition);
			}
			if (this.chars[this.curPos] != ';')
			{
				this.ThrowUnexpectedToken(this.curPos, ";");
			}
			XmlQualifiedName nameQualified = this.GetNameQualified(false);
			this.curPos++;
			return nameQualified;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0005ECF4 File Offset: 0x0005CEF4
		private DtdParser.Token ScanNotation1()
		{
			char c = this.chars[this.curPos];
			if (c == 'P')
			{
				if (!this.EatPublicKeyword())
				{
					this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
				}
				this.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
				this.scanningFunction = DtdParser.ScanningFunction.PublicId1;
				return DtdParser.Token.PUBLIC;
			}
			if (c != 'S')
			{
				this.Throw(this.curPos, "Xml_ExpectExternalOrPublicId");
				return DtdParser.Token.None;
			}
			if (!this.EatSystemKeyword())
			{
				this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
			}
			this.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
			this.scanningFunction = DtdParser.ScanningFunction.SystemId;
			return DtdParser.Token.SYSTEM;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0005ED88 File Offset: 0x0005CF88
		private DtdParser.Token ScanSystemId()
		{
			if (this.chars[this.curPos] != '"' && this.chars[this.curPos] != '\'')
			{
				this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			}
			this.ScanLiteral(DtdParser.LiteralType.SystemOrPublicID);
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Literal;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		private DtdParser.Token ScanEntity1()
		{
			if (this.chars[this.curPos] == '%')
			{
				this.curPos++;
				this.nextScaningFunction = DtdParser.ScanningFunction.Entity2;
				this.scanningFunction = DtdParser.ScanningFunction.Name;
				return DtdParser.Token.Percent;
			}
			this.ScanName();
			this.scanningFunction = DtdParser.ScanningFunction.Entity2;
			return DtdParser.Token.Name;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0005EE34 File Offset: 0x0005D034
		private DtdParser.Token ScanEntity2()
		{
			char c = this.chars[this.curPos];
			if (c <= '\'')
			{
				if (c == '"' || c == '\'')
				{
					this.ScanLiteral(DtdParser.LiteralType.EntityReplText);
					this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
					return DtdParser.Token.Literal;
				}
			}
			else
			{
				if (c == 'P')
				{
					if (!this.EatPublicKeyword())
					{
						this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Entity3;
					this.scanningFunction = DtdParser.ScanningFunction.PublicId1;
					return DtdParser.Token.PUBLIC;
				}
				if (c == 'S')
				{
					if (!this.EatSystemKeyword())
					{
						this.Throw(this.curPos, "Xml_ExpectExternalOrClose");
					}
					this.nextScaningFunction = DtdParser.ScanningFunction.Entity3;
					this.scanningFunction = DtdParser.ScanningFunction.SystemId;
					return DtdParser.Token.SYSTEM;
				}
			}
			this.Throw(this.curPos, "Xml_ExpectExternalIdOrEntityValue");
			return DtdParser.Token.None;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005EEEC File Offset: 0x0005D0EC
		private DtdParser.Token ScanEntity3()
		{
			if (this.chars[this.curPos] == 'N')
			{
				while (this.charsUsed - this.curPos < 5)
				{
					if (this.ReadData() == 0)
					{
						goto IL_9A;
					}
				}
				if (this.chars[this.curPos + 1] == 'D' && this.chars[this.curPos + 2] == 'A' && this.chars[this.curPos + 3] == 'T' && this.chars[this.curPos + 4] == 'A')
				{
					this.curPos += 5;
					this.scanningFunction = DtdParser.ScanningFunction.Name;
					this.nextScaningFunction = DtdParser.ScanningFunction.ClosingTag;
					return DtdParser.Token.NData;
				}
			}
			IL_9A:
			this.scanningFunction = DtdParser.ScanningFunction.ClosingTag;
			return DtdParser.Token.None;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0005EFA0 File Offset: 0x0005D1A0
		private DtdParser.Token ScanPublicId1()
		{
			if (this.chars[this.curPos] != '"' && this.chars[this.curPos] != '\'')
			{
				this.ThrowUnexpectedToken(this.curPos, "\"", "'");
			}
			this.ScanLiteral(DtdParser.LiteralType.SystemOrPublicID);
			this.scanningFunction = DtdParser.ScanningFunction.PublicId2;
			return DtdParser.Token.Literal;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0005EFF8 File Offset: 0x0005D1F8
		private DtdParser.Token ScanPublicId2()
		{
			if (this.chars[this.curPos] != '"' && this.chars[this.curPos] != '\'')
			{
				this.scanningFunction = this.nextScaningFunction;
				return DtdParser.Token.None;
			}
			this.ScanLiteral(DtdParser.LiteralType.SystemOrPublicID);
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.Literal;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0005F04C File Offset: 0x0005D24C
		private DtdParser.Token ScanCondSection1()
		{
			if (this.chars[this.curPos] != 'I')
			{
				this.Throw(this.curPos, "Xml_ExpectIgnoreOrInclude");
			}
			this.curPos++;
			for (;;)
			{
				if (this.charsUsed - this.curPos >= 5)
				{
					char c = this.chars[this.curPos];
					if (c == 'G')
					{
						goto IL_121;
					}
					if (c != 'N')
					{
						goto IL_1AA;
					}
					if (this.charsUsed - this.curPos >= 6)
					{
						break;
					}
				}
				if (this.ReadData() == 0)
				{
					this.Throw(this.curPos, "Xml_IncompleteDtdContent");
				}
			}
			if (this.chars[this.curPos + 1] == 'C' && this.chars[this.curPos + 2] == 'L' && this.chars[this.curPos + 3] == 'U' && this.chars[this.curPos + 4] == 'D' && this.chars[this.curPos + 5] == 'E' && !this.xmlCharType.IsNameSingleChar(this.chars[this.curPos + 6]))
			{
				this.nextScaningFunction = DtdParser.ScanningFunction.SubsetContent;
				this.scanningFunction = DtdParser.ScanningFunction.CondSection2;
				this.curPos += 6;
				return DtdParser.Token.INCLUDE;
			}
			goto IL_1AA;
			IL_121:
			if (this.chars[this.curPos + 1] == 'N' && this.chars[this.curPos + 2] == 'O' && this.chars[this.curPos + 3] == 'R' && this.chars[this.curPos + 4] == 'E' && !this.xmlCharType.IsNameSingleChar(this.chars[this.curPos + 5]))
			{
				this.nextScaningFunction = DtdParser.ScanningFunction.CondSection3;
				this.scanningFunction = DtdParser.ScanningFunction.CondSection2;
				this.curPos += 5;
				return DtdParser.Token.IGNORE;
			}
			IL_1AA:
			this.Throw(this.curPos - 1, "Xml_ExpectIgnoreOrInclude");
			return DtdParser.Token.None;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0005F239 File Offset: 0x0005D439
		private DtdParser.Token ScanCondSection2()
		{
			if (this.chars[this.curPos] != '[')
			{
				this.ThrowUnexpectedToken(this.curPos, "[");
			}
			this.curPos++;
			this.scanningFunction = this.nextScaningFunction;
			return DtdParser.Token.LeftBracket;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0005F27C File Offset: 0x0005D47C
		private unsafe DtdParser.Token ScanCondSection3()
		{
			int num = 0;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[this.chars[this.curPos]] & 64) == 0 || this.chars[this.curPos] == ']')
				{
					char c = this.chars[this.curPos];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.curPos++;
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						case '\v':
						case '\f':
							goto IL_21B;
						case '\r':
							if (this.chars[this.curPos + 1] == '\n')
							{
								this.curPos += 2;
							}
							else
							{
								if (this.curPos + 1 >= this.charsUsed && !this.readerAdapter.IsEof)
								{
									goto IL_29F;
								}
								this.curPos++;
							}
							this.readerAdapter.OnNewLine(this.curPos);
							continue;
						default:
							if (c != '"' && c != '&')
							{
								goto IL_21B;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c != '<')
						{
							if (c != ']')
							{
								goto IL_21B;
							}
							if (this.charsUsed - this.curPos < 3)
							{
								goto IL_29F;
							}
							if (this.chars[this.curPos + 1] != ']' || this.chars[this.curPos + 2] != '>')
							{
								this.curPos++;
								continue;
							}
							if (num > 0)
							{
								num--;
								this.curPos += 3;
								continue;
							}
							break;
						}
						else
						{
							if (this.charsUsed - this.curPos < 3)
							{
								goto IL_29F;
							}
							if (this.chars[this.curPos + 1] != '!' || this.chars[this.curPos + 2] != '[')
							{
								this.curPos++;
								continue;
							}
							num++;
							this.curPos += 3;
							continue;
						}
					}
					this.curPos++;
					continue;
					IL_21B:
					if (this.curPos != this.charsUsed)
					{
						char ch = this.chars[this.curPos];
						if (!XmlCharType.IsHighSurrogate((int)ch))
						{
							goto IL_284;
						}
						if (this.curPos + 1 != this.charsUsed)
						{
							this.curPos++;
							if (XmlCharType.IsLowSurrogate((int)this.chars[this.curPos]))
							{
								this.curPos++;
								continue;
							}
							goto IL_284;
						}
					}
					IL_29F:
					if (this.readerAdapter.IsEof || this.ReadData() == 0)
					{
						if (this.HandleEntityEnd(false))
						{
							continue;
						}
						this.Throw(this.curPos, "Xml_UnclosedConditionalSection");
					}
					this.tokenStartPos = this.curPos;
				}
				else
				{
					this.curPos++;
				}
			}
			this.curPos += 3;
			this.scanningFunction = DtdParser.ScanningFunction.SubsetContent;
			return DtdParser.Token.CondSectionEnd;
			IL_284:
			this.ThrowInvalidChar(this.chars, this.charsUsed, this.curPos);
			return DtdParser.Token.None;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0005F56A File Offset: 0x0005D76A
		private void ScanName()
		{
			this.ScanQName(false);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0005F573 File Offset: 0x0005D773
		private void ScanQName()
		{
			this.ScanQName(this.SupportNamespaces);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0005F584 File Offset: 0x0005D784
		private unsafe void ScanQName(bool isQName)
		{
			this.tokenStartPos = this.curPos;
			int num = -1;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[this.chars[this.curPos]] & 4) != 0 || this.chars[this.curPos] == ':')
				{
					this.curPos++;
				}
				else if (this.curPos + 1 >= this.charsUsed)
				{
					if (this.ReadDataInName())
					{
						continue;
					}
					this.Throw(this.curPos, "Xml_UnexpectedEOF", "Name");
				}
				else
				{
					this.Throw(this.curPos, "Xml_BadStartNameChar", XmlException.BuildCharExceptionArgs(this.chars, this.charsUsed, this.curPos));
				}
				for (;;)
				{
					if ((this.xmlCharType.charProperties[this.chars[this.curPos]] & 8) != 0)
					{
						this.curPos++;
					}
					else if (this.chars[this.curPos] == ':')
					{
						if (isQName)
						{
							break;
						}
						this.curPos++;
					}
					else
					{
						if (this.curPos != this.charsUsed)
						{
							goto IL_175;
						}
						if (!this.ReadDataInName())
						{
							goto Block_9;
						}
					}
				}
				if (num != -1)
				{
					this.Throw(this.curPos, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(':', '\0'));
				}
				num = this.curPos - this.tokenStartPos;
				this.curPos++;
			}
			Block_9:
			if (this.tokenStartPos == this.curPos)
			{
				this.Throw(this.curPos, "Xml_UnexpectedEOF", "Name");
			}
			IL_175:
			this.colonPos = ((num == -1) ? -1 : (this.tokenStartPos + num));
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0005F71C File Offset: 0x0005D91C
		private bool ReadDataInName()
		{
			int num = this.curPos - this.tokenStartPos;
			this.curPos = this.tokenStartPos;
			bool result = this.ReadData() != 0;
			this.tokenStartPos = this.curPos;
			this.curPos += num;
			return result;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0005F768 File Offset: 0x0005D968
		private unsafe void ScanNmtoken()
		{
			this.tokenStartPos = this.curPos;
			int num;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[this.chars[this.curPos]] & 8) != 0 || this.chars[this.curPos] == ':')
				{
					this.curPos++;
				}
				else
				{
					if (this.curPos < this.charsUsed)
					{
						break;
					}
					num = this.curPos - this.tokenStartPos;
					this.curPos = this.tokenStartPos;
					if (this.ReadData() == 0)
					{
						if (num > 0)
						{
							goto Block_5;
						}
						this.Throw(this.curPos, "Xml_UnexpectedEOF", "NmToken");
					}
					this.tokenStartPos = this.curPos;
					this.curPos += num;
				}
			}
			if (this.curPos - this.tokenStartPos == 0)
			{
				this.Throw(this.curPos, "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(this.chars, this.charsUsed, this.curPos));
			}
			return;
			Block_5:
			this.tokenStartPos = this.curPos;
			this.curPos += num;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0005F87C File Offset: 0x0005DA7C
		private bool EatPublicKeyword()
		{
			while (this.charsUsed - this.curPos < 6)
			{
				if (this.ReadData() == 0)
				{
					return false;
				}
			}
			if (this.chars[this.curPos + 1] != 'U' || this.chars[this.curPos + 2] != 'B' || this.chars[this.curPos + 3] != 'L' || this.chars[this.curPos + 4] != 'I' || this.chars[this.curPos + 5] != 'C')
			{
				return false;
			}
			this.curPos += 6;
			return true;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0005F918 File Offset: 0x0005DB18
		private bool EatSystemKeyword()
		{
			while (this.charsUsed - this.curPos < 6)
			{
				if (this.ReadData() == 0)
				{
					return false;
				}
			}
			if (this.chars[this.curPos + 1] != 'Y' || this.chars[this.curPos + 2] != 'S' || this.chars[this.curPos + 3] != 'T' || this.chars[this.curPos + 4] != 'E' || this.chars[this.curPos + 5] != 'M')
			{
				return false;
			}
			this.curPos += 6;
			return true;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
		private XmlQualifiedName GetNameQualified(bool canHavePrefix)
		{
			if (this.colonPos == -1)
			{
				return new XmlQualifiedName(this.nameTable.Add(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos));
			}
			if (canHavePrefix)
			{
				return new XmlQualifiedName(this.nameTable.Add(this.chars, this.colonPos + 1, this.curPos - this.colonPos - 1), this.nameTable.Add(this.chars, this.tokenStartPos, this.colonPos - this.tokenStartPos));
			}
			this.Throw(this.tokenStartPos, "Xml_ColonInLocalName", this.GetNameString());
			return null;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0005FA61 File Offset: 0x0005DC61
		private string GetNameString()
		{
			return new string(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0005FA81 File Offset: 0x0005DC81
		private string GetNmtokenString()
		{
			return this.GetNameString();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0005FA89 File Offset: 0x0005DC89
		private string GetValue()
		{
			if (this.stringBuilder.Length == 0)
			{
				return new string(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos - 1);
			}
			return this.stringBuilder.ToString();
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0005FAC4 File Offset: 0x0005DCC4
		private string GetValueWithStrippedSpaces()
		{
			string value = (this.stringBuilder.Length == 0) ? new string(this.chars, this.tokenStartPos, this.curPos - this.tokenStartPos - 1) : this.stringBuilder.ToString();
			return DtdParser.StripSpaces(value);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0005FB14 File Offset: 0x0005DD14
		private int ReadData()
		{
			this.SaveParsingBuffer();
			int result = this.readerAdapter.ReadData();
			this.LoadParsingBuffer();
			return result;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0005FB3A File Offset: 0x0005DD3A
		private void LoadParsingBuffer()
		{
			this.chars = this.readerAdapter.ParsingBuffer;
			this.charsUsed = this.readerAdapter.ParsingBufferLength;
			this.curPos = this.readerAdapter.CurrentPosition;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0005FB6F File Offset: 0x0005DD6F
		private void SaveParsingBuffer()
		{
			this.SaveParsingBuffer(this.curPos);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0005FB80 File Offset: 0x0005DD80
		private void SaveParsingBuffer(int internalSubsetValueEndPos)
		{
			if (this.SaveInternalSubsetValue)
			{
				int currentPosition = this.readerAdapter.CurrentPosition;
				if (internalSubsetValueEndPos - currentPosition > 0)
				{
					this.internalSubsetValueSb.Append(this.chars, currentPosition, internalSubsetValueEndPos - currentPosition);
				}
			}
			this.readerAdapter.CurrentPosition = this.curPos;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0005FBCE File Offset: 0x0005DDCE
		private bool HandleEntityReference(bool paramEntity, bool inLiteral, bool inAttribute)
		{
			this.curPos++;
			return this.HandleEntityReference(this.ScanEntityName(), paramEntity, inLiteral, inAttribute);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0005FBF0 File Offset: 0x0005DDF0
		private bool HandleEntityReference(XmlQualifiedName entityName, bool paramEntity, bool inLiteral, bool inAttribute)
		{
			this.SaveParsingBuffer();
			if (paramEntity && this.ParsingInternalSubset && !this.ParsingTopLevelMarkup)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, "Xml_InvalidParEntityRef");
			}
			SchemaEntity schemaEntity = this.VerifyEntityReference(entityName, paramEntity, true, inAttribute);
			if (schemaEntity == null)
			{
				return false;
			}
			if (schemaEntity.ParsingInProgress)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, paramEntity ? "Xml_RecursiveParEntity" : "Xml_RecursiveGenEntity", entityName.Name);
			}
			int num;
			if (schemaEntity.IsExternal)
			{
				if (!this.readerAdapter.PushEntity(schemaEntity, out num))
				{
					return false;
				}
				this.externalEntitiesDepth++;
			}
			else
			{
				if (schemaEntity.Text.Length == 0)
				{
					return false;
				}
				if (!this.readerAdapter.PushEntity(schemaEntity, out num))
				{
					return false;
				}
			}
			this.currentEntityId = num;
			if (paramEntity && !inLiteral && this.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
			{
				this.savedScanningFunction = this.scanningFunction;
				this.scanningFunction = DtdParser.ScanningFunction.ParamEntitySpace;
			}
			this.LoadParsingBuffer();
			return true;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0005FCFC File Offset: 0x0005DEFC
		private bool HandleEntityEnd(bool inLiteral)
		{
			this.SaveParsingBuffer();
			IDtdEntityInfo dtdEntityInfo;
			if (!this.readerAdapter.PopEntity(out dtdEntityInfo, out this.currentEntityId))
			{
				return false;
			}
			this.LoadParsingBuffer();
			if (dtdEntityInfo == null)
			{
				if (this.scanningFunction == DtdParser.ScanningFunction.ParamEntitySpace)
				{
					this.scanningFunction = this.savedScanningFunction;
				}
				return false;
			}
			if (dtdEntityInfo.IsExternal)
			{
				this.externalEntitiesDepth--;
			}
			if (!inLiteral && this.scanningFunction != DtdParser.ScanningFunction.ParamEntitySpace)
			{
				this.savedScanningFunction = this.scanningFunction;
				this.scanningFunction = DtdParser.ScanningFunction.ParamEntitySpace;
			}
			return true;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0005FD80 File Offset: 0x0005DF80
		private SchemaEntity VerifyEntityReference(XmlQualifiedName entityName, bool paramEntity, bool mustBeDeclared, bool inAttribute)
		{
			SchemaEntity schemaEntity;
			if (paramEntity)
			{
				this.schemaInfo.ParameterEntities.TryGetValue(entityName, out schemaEntity);
			}
			else
			{
				this.schemaInfo.GeneralEntities.TryGetValue(entityName, out schemaEntity);
			}
			if (schemaEntity == null)
			{
				if (paramEntity)
				{
					if (this.validate)
					{
						this.SendValidationEvent(this.curPos - entityName.Name.Length - 1, XmlSeverityType.Error, "Xml_UndeclaredParEntity", entityName.Name);
					}
				}
				else if (mustBeDeclared)
				{
					if (!this.ParsingInternalSubset)
					{
						if (this.validate)
						{
							this.SendValidationEvent(this.curPos - entityName.Name.Length - 1, XmlSeverityType.Error, "Xml_UndeclaredEntity", entityName.Name);
						}
					}
					else
					{
						this.Throw(this.curPos - entityName.Name.Length - 1, "Xml_UndeclaredEntity", entityName.Name);
					}
				}
				return null;
			}
			if (!schemaEntity.NData.IsEmpty)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, "Xml_UnparsedEntityRef", entityName.Name);
			}
			if (inAttribute && schemaEntity.IsExternal)
			{
				this.Throw(this.curPos - entityName.Name.Length - 1, "Xml_ExternalEntityInAttValue", entityName.Name);
			}
			return schemaEntity;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0005FEBC File Offset: 0x0005E0BC
		private void SendValidationEvent(int pos, XmlSeverityType severity, string code, string arg)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.BaseUriStr, this.LineNo, this.LinePos + (pos - this.curPos)));
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0005FEF3 File Offset: 0x0005E0F3
		private void SendValidationEvent(XmlSeverityType severity, string code, string arg)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.BaseUriStr, this.LineNo, this.LinePos));
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0005FF18 File Offset: 0x0005E118
		private void SendValidationEvent(XmlSeverityType severity, XmlSchemaException e)
		{
			IValidationEventHandling validationEventHandling = this.readerAdapterWithValidation.ValidationEventHandling;
			if (validationEventHandling != null)
			{
				validationEventHandling.SendEvent(e, severity);
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0005FF3C File Offset: 0x0005E13C
		private bool IsAttributeValueType(DtdParser.Token token)
		{
			return token >= DtdParser.Token.CDATA && token <= DtdParser.Token.NOTATION;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0005FF4B File Offset: 0x0005E14B
		private int LineNo
		{
			get
			{
				return this.readerAdapter.LineNo;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0005FF58 File Offset: 0x0005E158
		private int LinePos
		{
			get
			{
				return this.curPos - this.readerAdapter.LineStartPosition;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0005FF6C File Offset: 0x0005E16C
		private string BaseUriStr
		{
			get
			{
				Uri baseUri = this.readerAdapter.BaseUri;
				if (!(baseUri != null))
				{
					return string.Empty;
				}
				return baseUri.ToString();
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0005FF9A File Offset: 0x0005E19A
		private void OnUnexpectedError()
		{
			this.Throw(this.curPos, "Xml_InternalError");
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0005FFAD File Offset: 0x0005E1AD
		private void Throw(int curPos, string res)
		{
			this.Throw(curPos, res, string.Empty);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0005FFBC File Offset: 0x0005E1BC
		private void Throw(int curPos, string res, string arg)
		{
			this.curPos = curPos;
			Uri baseUri = this.readerAdapter.BaseUri;
			this.readerAdapter.Throw(new XmlException(res, arg, this.LineNo, this.LinePos, (baseUri == null) ? null : baseUri.ToString()));
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0006000C File Offset: 0x0005E20C
		private void Throw(int curPos, string res, string[] args)
		{
			this.curPos = curPos;
			Uri baseUri = this.readerAdapter.BaseUri;
			this.readerAdapter.Throw(new XmlException(res, args, this.LineNo, this.LinePos, (baseUri == null) ? null : baseUri.ToString()));
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0006005C File Offset: 0x0005E25C
		private void Throw(string res, string arg, int lineNo, int linePos)
		{
			Uri baseUri = this.readerAdapter.BaseUri;
			this.readerAdapter.Throw(new XmlException(res, arg, lineNo, linePos, (baseUri == null) ? null : baseUri.ToString()));
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0006009C File Offset: 0x0005E29C
		private void ThrowInvalidChar(int pos, string data, int invCharPos)
		{
			this.Throw(pos, "Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(data, invCharPos));
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000600B1 File Offset: 0x0005E2B1
		private void ThrowInvalidChar(char[] data, int length, int invCharPos)
		{
			this.Throw(invCharPos, "Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(data, length, invCharPos));
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000600C7 File Offset: 0x0005E2C7
		private void ThrowUnexpectedToken(int pos, string expectedToken)
		{
			this.ThrowUnexpectedToken(pos, expectedToken, null);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000600D4 File Offset: 0x0005E2D4
		private void ThrowUnexpectedToken(int pos, string expectedToken1, string expectedToken2)
		{
			string text = this.ParseUnexpectedToken(pos);
			if (expectedToken2 != null)
			{
				this.Throw(this.curPos, "Xml_UnexpectedTokens2", new string[]
				{
					text,
					expectedToken1,
					expectedToken2
				});
				return;
			}
			this.Throw(this.curPos, "Xml_UnexpectedTokenEx", new string[]
			{
				text,
				expectedToken1
			});
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00060130 File Offset: 0x0005E330
		private string ParseUnexpectedToken(int startPos)
		{
			if (this.xmlCharType.IsNCNameSingleChar(this.chars[startPos]))
			{
				int num = startPos;
				while (this.xmlCharType.IsNCNameSingleChar(this.chars[num]))
				{
					num++;
				}
				int num2 = num - startPos;
				return new string(this.chars, startPos, (num2 > 0) ? num2 : 1);
			}
			return new string(this.chars, startPos, 1);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00060198 File Offset: 0x0005E398
		internal static string StripSpaces(string value)
		{
			int length = value.Length;
			if (length <= 0)
			{
				return string.Empty;
			}
			int num = 0;
			StringBuilder stringBuilder = null;
			while (value[num] == ' ')
			{
				num++;
				if (num == length)
				{
					return " ";
				}
			}
			int i;
			for (i = num; i < length; i++)
			{
				if (value[i] == ' ')
				{
					int num2 = i + 1;
					while (num2 < length && value[num2] == ' ')
					{
						num2++;
					}
					if (num2 == length)
					{
						if (stringBuilder == null)
						{
							return value.Substring(num, i - num);
						}
						stringBuilder.Append(value, num, i - num);
						return stringBuilder.ToString();
					}
					else if (num2 > i + 1)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(length);
						}
						stringBuilder.Append(value, num, i - num + 1);
						num = num2;
						i = num2 - 1;
					}
				}
			}
			if (stringBuilder != null)
			{
				if (i > num)
				{
					stringBuilder.Append(value, num, i - num);
				}
				return stringBuilder.ToString();
			}
			if (num != 0)
			{
				return value.Substring(num, length - num);
			}
			return value;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00060280 File Offset: 0x0005E480
		Task<IDtdInfo> IDtdParser.ParseInternalDtdAsync(IDtdParserAdapter adapter, bool saveInternalSubset)
		{
			DtdParser.<System-Xml-IDtdParser-ParseInternalDtdAsync>d__153 <System-Xml-IDtdParser-ParseInternalDtdAsync>d__;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDtdInfo>.Create();
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>4__this = this;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.adapter = adapter;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.saveInternalSubset = saveInternalSubset;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>1__state = -1;
			<System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>t__builder.Start<DtdParser.<System-Xml-IDtdParser-ParseInternalDtdAsync>d__153>(ref <System-Xml-IDtdParser-ParseInternalDtdAsync>d__);
			return <System-Xml-IDtdParser-ParseInternalDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000602D4 File Offset: 0x0005E4D4
		Task<IDtdInfo> IDtdParser.ParseFreeFloatingDtdAsync(string baseUri, string docTypeName, string publicId, string systemId, string internalSubset, IDtdParserAdapter adapter)
		{
			DtdParser.<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__154 <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDtdInfo>.Create();
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>4__this = this;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.baseUri = baseUri;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.docTypeName = docTypeName;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.publicId = publicId;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.systemId = systemId;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.internalSubset = internalSubset;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.adapter = adapter;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>1__state = -1;
			<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>t__builder.Start<DtdParser.<System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__154>(ref <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__);
			return <System-Xml-IDtdParser-ParseFreeFloatingDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0006034C File Offset: 0x0005E54C
		private Task ParseAsync(bool saveInternalSubset)
		{
			DtdParser.<ParseAsync>d__155 <ParseAsync>d__;
			<ParseAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAsync>d__.<>4__this = this;
			<ParseAsync>d__.saveInternalSubset = saveInternalSubset;
			<ParseAsync>d__.<>1__state = -1;
			<ParseAsync>d__.<>t__builder.Start<DtdParser.<ParseAsync>d__155>(ref <ParseAsync>d__);
			return <ParseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00060398 File Offset: 0x0005E598
		private Task ParseInDocumentDtdAsync(bool saveInternalSubset)
		{
			DtdParser.<ParseInDocumentDtdAsync>d__156 <ParseInDocumentDtdAsync>d__;
			<ParseInDocumentDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseInDocumentDtdAsync>d__.<>4__this = this;
			<ParseInDocumentDtdAsync>d__.saveInternalSubset = saveInternalSubset;
			<ParseInDocumentDtdAsync>d__.<>1__state = -1;
			<ParseInDocumentDtdAsync>d__.<>t__builder.Start<DtdParser.<ParseInDocumentDtdAsync>d__156>(ref <ParseInDocumentDtdAsync>d__);
			return <ParseInDocumentDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000603E4 File Offset: 0x0005E5E4
		private Task ParseFreeFloatingDtdAsync()
		{
			DtdParser.<ParseFreeFloatingDtdAsync>d__157 <ParseFreeFloatingDtdAsync>d__;
			<ParseFreeFloatingDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseFreeFloatingDtdAsync>d__.<>4__this = this;
			<ParseFreeFloatingDtdAsync>d__.<>1__state = -1;
			<ParseFreeFloatingDtdAsync>d__.<>t__builder.Start<DtdParser.<ParseFreeFloatingDtdAsync>d__157>(ref <ParseFreeFloatingDtdAsync>d__);
			return <ParseFreeFloatingDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00060427 File Offset: 0x0005E627
		private Task ParseInternalSubsetAsync()
		{
			return this.ParseSubsetAsync();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00060430 File Offset: 0x0005E630
		private Task ParseExternalSubsetAsync()
		{
			DtdParser.<ParseExternalSubsetAsync>d__159 <ParseExternalSubsetAsync>d__;
			<ParseExternalSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseExternalSubsetAsync>d__.<>4__this = this;
			<ParseExternalSubsetAsync>d__.<>1__state = -1;
			<ParseExternalSubsetAsync>d__.<>t__builder.Start<DtdParser.<ParseExternalSubsetAsync>d__159>(ref <ParseExternalSubsetAsync>d__);
			return <ParseExternalSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00060474 File Offset: 0x0005E674
		private Task ParseSubsetAsync()
		{
			DtdParser.<ParseSubsetAsync>d__160 <ParseSubsetAsync>d__;
			<ParseSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseSubsetAsync>d__.<>4__this = this;
			<ParseSubsetAsync>d__.<>1__state = -1;
			<ParseSubsetAsync>d__.<>t__builder.Start<DtdParser.<ParseSubsetAsync>d__160>(ref <ParseSubsetAsync>d__);
			return <ParseSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000604B8 File Offset: 0x0005E6B8
		private Task ParseAttlistDeclAsync()
		{
			DtdParser.<ParseAttlistDeclAsync>d__161 <ParseAttlistDeclAsync>d__;
			<ParseAttlistDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttlistDeclAsync>d__.<>4__this = this;
			<ParseAttlistDeclAsync>d__.<>1__state = -1;
			<ParseAttlistDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseAttlistDeclAsync>d__161>(ref <ParseAttlistDeclAsync>d__);
			return <ParseAttlistDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000604FC File Offset: 0x0005E6FC
		private Task ParseAttlistTypeAsync(SchemaAttDef attrDef, SchemaElementDecl elementDecl, bool ignoreErrors)
		{
			DtdParser.<ParseAttlistTypeAsync>d__162 <ParseAttlistTypeAsync>d__;
			<ParseAttlistTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttlistTypeAsync>d__.<>4__this = this;
			<ParseAttlistTypeAsync>d__.attrDef = attrDef;
			<ParseAttlistTypeAsync>d__.elementDecl = elementDecl;
			<ParseAttlistTypeAsync>d__.ignoreErrors = ignoreErrors;
			<ParseAttlistTypeAsync>d__.<>1__state = -1;
			<ParseAttlistTypeAsync>d__.<>t__builder.Start<DtdParser.<ParseAttlistTypeAsync>d__162>(ref <ParseAttlistTypeAsync>d__);
			return <ParseAttlistTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00060558 File Offset: 0x0005E758
		private Task ParseAttlistDefaultAsync(SchemaAttDef attrDef, bool ignoreErrors)
		{
			DtdParser.<ParseAttlistDefaultAsync>d__163 <ParseAttlistDefaultAsync>d__;
			<ParseAttlistDefaultAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttlistDefaultAsync>d__.<>4__this = this;
			<ParseAttlistDefaultAsync>d__.attrDef = attrDef;
			<ParseAttlistDefaultAsync>d__.ignoreErrors = ignoreErrors;
			<ParseAttlistDefaultAsync>d__.<>1__state = -1;
			<ParseAttlistDefaultAsync>d__.<>t__builder.Start<DtdParser.<ParseAttlistDefaultAsync>d__163>(ref <ParseAttlistDefaultAsync>d__);
			return <ParseAttlistDefaultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000605AC File Offset: 0x0005E7AC
		private Task ParseElementDeclAsync()
		{
			DtdParser.<ParseElementDeclAsync>d__164 <ParseElementDeclAsync>d__;
			<ParseElementDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementDeclAsync>d__.<>4__this = this;
			<ParseElementDeclAsync>d__.<>1__state = -1;
			<ParseElementDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseElementDeclAsync>d__164>(ref <ParseElementDeclAsync>d__);
			return <ParseElementDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000605F0 File Offset: 0x0005E7F0
		private Task ParseElementOnlyContentAsync(ParticleContentValidator pcv, int startParenEntityId)
		{
			DtdParser.<ParseElementOnlyContentAsync>d__165 <ParseElementOnlyContentAsync>d__;
			<ParseElementOnlyContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementOnlyContentAsync>d__.<>4__this = this;
			<ParseElementOnlyContentAsync>d__.pcv = pcv;
			<ParseElementOnlyContentAsync>d__.startParenEntityId = startParenEntityId;
			<ParseElementOnlyContentAsync>d__.<>1__state = -1;
			<ParseElementOnlyContentAsync>d__.<>t__builder.Start<DtdParser.<ParseElementOnlyContentAsync>d__165>(ref <ParseElementOnlyContentAsync>d__);
			return <ParseElementOnlyContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00060644 File Offset: 0x0005E844
		private Task ParseHowManyAsync(ParticleContentValidator pcv)
		{
			DtdParser.<ParseHowManyAsync>d__166 <ParseHowManyAsync>d__;
			<ParseHowManyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseHowManyAsync>d__.<>4__this = this;
			<ParseHowManyAsync>d__.pcv = pcv;
			<ParseHowManyAsync>d__.<>1__state = -1;
			<ParseHowManyAsync>d__.<>t__builder.Start<DtdParser.<ParseHowManyAsync>d__166>(ref <ParseHowManyAsync>d__);
			return <ParseHowManyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00060690 File Offset: 0x0005E890
		private Task ParseElementMixedContentAsync(ParticleContentValidator pcv, int startParenEntityId)
		{
			DtdParser.<ParseElementMixedContentAsync>d__167 <ParseElementMixedContentAsync>d__;
			<ParseElementMixedContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementMixedContentAsync>d__.<>4__this = this;
			<ParseElementMixedContentAsync>d__.pcv = pcv;
			<ParseElementMixedContentAsync>d__.startParenEntityId = startParenEntityId;
			<ParseElementMixedContentAsync>d__.<>1__state = -1;
			<ParseElementMixedContentAsync>d__.<>t__builder.Start<DtdParser.<ParseElementMixedContentAsync>d__167>(ref <ParseElementMixedContentAsync>d__);
			return <ParseElementMixedContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000606E4 File Offset: 0x0005E8E4
		private Task ParseEntityDeclAsync()
		{
			DtdParser.<ParseEntityDeclAsync>d__168 <ParseEntityDeclAsync>d__;
			<ParseEntityDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEntityDeclAsync>d__.<>4__this = this;
			<ParseEntityDeclAsync>d__.<>1__state = -1;
			<ParseEntityDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseEntityDeclAsync>d__168>(ref <ParseEntityDeclAsync>d__);
			return <ParseEntityDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00060728 File Offset: 0x0005E928
		private Task ParseNotationDeclAsync()
		{
			DtdParser.<ParseNotationDeclAsync>d__169 <ParseNotationDeclAsync>d__;
			<ParseNotationDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseNotationDeclAsync>d__.<>4__this = this;
			<ParseNotationDeclAsync>d__.<>1__state = -1;
			<ParseNotationDeclAsync>d__.<>t__builder.Start<DtdParser.<ParseNotationDeclAsync>d__169>(ref <ParseNotationDeclAsync>d__);
			return <ParseNotationDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0006076C File Offset: 0x0005E96C
		private Task ParseCommentAsync()
		{
			DtdParser.<ParseCommentAsync>d__170 <ParseCommentAsync>d__;
			<ParseCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseCommentAsync>d__.<>4__this = this;
			<ParseCommentAsync>d__.<>1__state = -1;
			<ParseCommentAsync>d__.<>t__builder.Start<DtdParser.<ParseCommentAsync>d__170>(ref <ParseCommentAsync>d__);
			return <ParseCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000607B0 File Offset: 0x0005E9B0
		private Task ParsePIAsync()
		{
			DtdParser.<ParsePIAsync>d__171 <ParsePIAsync>d__;
			<ParsePIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParsePIAsync>d__.<>4__this = this;
			<ParsePIAsync>d__.<>1__state = -1;
			<ParsePIAsync>d__.<>t__builder.Start<DtdParser.<ParsePIAsync>d__171>(ref <ParsePIAsync>d__);
			return <ParsePIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000607F4 File Offset: 0x0005E9F4
		private Task ParseCondSectionAsync()
		{
			DtdParser.<ParseCondSectionAsync>d__172 <ParseCondSectionAsync>d__;
			<ParseCondSectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseCondSectionAsync>d__.<>4__this = this;
			<ParseCondSectionAsync>d__.<>1__state = -1;
			<ParseCondSectionAsync>d__.<>t__builder.Start<DtdParser.<ParseCondSectionAsync>d__172>(ref <ParseCondSectionAsync>d__);
			return <ParseCondSectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00060838 File Offset: 0x0005EA38
		private Task<Tuple<string, string>> ParseExternalIdAsync(DtdParser.Token idTokenType, DtdParser.Token declType)
		{
			DtdParser.<ParseExternalIdAsync>d__173 <ParseExternalIdAsync>d__;
			<ParseExternalIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<string, string>>.Create();
			<ParseExternalIdAsync>d__.<>4__this = this;
			<ParseExternalIdAsync>d__.idTokenType = idTokenType;
			<ParseExternalIdAsync>d__.declType = declType;
			<ParseExternalIdAsync>d__.<>1__state = -1;
			<ParseExternalIdAsync>d__.<>t__builder.Start<DtdParser.<ParseExternalIdAsync>d__173>(ref <ParseExternalIdAsync>d__);
			return <ParseExternalIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0006088C File Offset: 0x0005EA8C
		private Task<DtdParser.Token> GetTokenAsync(bool needWhiteSpace)
		{
			DtdParser.<GetTokenAsync>d__174 <GetTokenAsync>d__;
			<GetTokenAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<GetTokenAsync>d__.<>4__this = this;
			<GetTokenAsync>d__.needWhiteSpace = needWhiteSpace;
			<GetTokenAsync>d__.<>1__state = -1;
			<GetTokenAsync>d__.<>t__builder.Start<DtdParser.<GetTokenAsync>d__174>(ref <GetTokenAsync>d__);
			return <GetTokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000608D8 File Offset: 0x0005EAD8
		private Task<DtdParser.Token> ScanSubsetContentAsync()
		{
			DtdParser.<ScanSubsetContentAsync>d__175 <ScanSubsetContentAsync>d__;
			<ScanSubsetContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanSubsetContentAsync>d__.<>4__this = this;
			<ScanSubsetContentAsync>d__.<>1__state = -1;
			<ScanSubsetContentAsync>d__.<>t__builder.Start<DtdParser.<ScanSubsetContentAsync>d__175>(ref <ScanSubsetContentAsync>d__);
			return <ScanSubsetContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0006091C File Offset: 0x0005EB1C
		private Task<DtdParser.Token> ScanNameExpectedAsync()
		{
			DtdParser.<ScanNameExpectedAsync>d__176 <ScanNameExpectedAsync>d__;
			<ScanNameExpectedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanNameExpectedAsync>d__.<>4__this = this;
			<ScanNameExpectedAsync>d__.<>1__state = -1;
			<ScanNameExpectedAsync>d__.<>t__builder.Start<DtdParser.<ScanNameExpectedAsync>d__176>(ref <ScanNameExpectedAsync>d__);
			return <ScanNameExpectedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00060960 File Offset: 0x0005EB60
		private Task<DtdParser.Token> ScanQNameExpectedAsync()
		{
			DtdParser.<ScanQNameExpectedAsync>d__177 <ScanQNameExpectedAsync>d__;
			<ScanQNameExpectedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanQNameExpectedAsync>d__.<>4__this = this;
			<ScanQNameExpectedAsync>d__.<>1__state = -1;
			<ScanQNameExpectedAsync>d__.<>t__builder.Start<DtdParser.<ScanQNameExpectedAsync>d__177>(ref <ScanQNameExpectedAsync>d__);
			return <ScanQNameExpectedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000609A4 File Offset: 0x0005EBA4
		private Task<DtdParser.Token> ScanNmtokenExpectedAsync()
		{
			DtdParser.<ScanNmtokenExpectedAsync>d__178 <ScanNmtokenExpectedAsync>d__;
			<ScanNmtokenExpectedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanNmtokenExpectedAsync>d__.<>4__this = this;
			<ScanNmtokenExpectedAsync>d__.<>1__state = -1;
			<ScanNmtokenExpectedAsync>d__.<>t__builder.Start<DtdParser.<ScanNmtokenExpectedAsync>d__178>(ref <ScanNmtokenExpectedAsync>d__);
			return <ScanNmtokenExpectedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000609E8 File Offset: 0x0005EBE8
		private Task<DtdParser.Token> ScanDoctype1Async()
		{
			DtdParser.<ScanDoctype1Async>d__179 <ScanDoctype1Async>d__;
			<ScanDoctype1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanDoctype1Async>d__.<>4__this = this;
			<ScanDoctype1Async>d__.<>1__state = -1;
			<ScanDoctype1Async>d__.<>t__builder.Start<DtdParser.<ScanDoctype1Async>d__179>(ref <ScanDoctype1Async>d__);
			return <ScanDoctype1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00060A2C File Offset: 0x0005EC2C
		private Task<DtdParser.Token> ScanElement1Async()
		{
			DtdParser.<ScanElement1Async>d__180 <ScanElement1Async>d__;
			<ScanElement1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanElement1Async>d__.<>4__this = this;
			<ScanElement1Async>d__.<>1__state = -1;
			<ScanElement1Async>d__.<>t__builder.Start<DtdParser.<ScanElement1Async>d__180>(ref <ScanElement1Async>d__);
			return <ScanElement1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00060A70 File Offset: 0x0005EC70
		private Task<DtdParser.Token> ScanElement2Async()
		{
			DtdParser.<ScanElement2Async>d__181 <ScanElement2Async>d__;
			<ScanElement2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanElement2Async>d__.<>4__this = this;
			<ScanElement2Async>d__.<>1__state = -1;
			<ScanElement2Async>d__.<>t__builder.Start<DtdParser.<ScanElement2Async>d__181>(ref <ScanElement2Async>d__);
			return <ScanElement2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00060AB4 File Offset: 0x0005ECB4
		private Task<DtdParser.Token> ScanElement3Async()
		{
			DtdParser.<ScanElement3Async>d__182 <ScanElement3Async>d__;
			<ScanElement3Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanElement3Async>d__.<>4__this = this;
			<ScanElement3Async>d__.<>1__state = -1;
			<ScanElement3Async>d__.<>t__builder.Start<DtdParser.<ScanElement3Async>d__182>(ref <ScanElement3Async>d__);
			return <ScanElement3Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00060AF8 File Offset: 0x0005ECF8
		private Task<DtdParser.Token> ScanAttlist1Async()
		{
			DtdParser.<ScanAttlist1Async>d__183 <ScanAttlist1Async>d__;
			<ScanAttlist1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanAttlist1Async>d__.<>4__this = this;
			<ScanAttlist1Async>d__.<>1__state = -1;
			<ScanAttlist1Async>d__.<>t__builder.Start<DtdParser.<ScanAttlist1Async>d__183>(ref <ScanAttlist1Async>d__);
			return <ScanAttlist1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00060B3C File Offset: 0x0005ED3C
		private Task<DtdParser.Token> ScanAttlist2Async()
		{
			DtdParser.<ScanAttlist2Async>d__184 <ScanAttlist2Async>d__;
			<ScanAttlist2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanAttlist2Async>d__.<>4__this = this;
			<ScanAttlist2Async>d__.<>1__state = -1;
			<ScanAttlist2Async>d__.<>t__builder.Start<DtdParser.<ScanAttlist2Async>d__184>(ref <ScanAttlist2Async>d__);
			return <ScanAttlist2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00060B80 File Offset: 0x0005ED80
		private Task<DtdParser.Token> ScanAttlist6Async()
		{
			DtdParser.<ScanAttlist6Async>d__185 <ScanAttlist6Async>d__;
			<ScanAttlist6Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanAttlist6Async>d__.<>4__this = this;
			<ScanAttlist6Async>d__.<>1__state = -1;
			<ScanAttlist6Async>d__.<>t__builder.Start<DtdParser.<ScanAttlist6Async>d__185>(ref <ScanAttlist6Async>d__);
			return <ScanAttlist6Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00060BC4 File Offset: 0x0005EDC4
		private Task<DtdParser.Token> ScanLiteralAsync(DtdParser.LiteralType literalType)
		{
			DtdParser.<ScanLiteralAsync>d__186 <ScanLiteralAsync>d__;
			<ScanLiteralAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanLiteralAsync>d__.<>4__this = this;
			<ScanLiteralAsync>d__.literalType = literalType;
			<ScanLiteralAsync>d__.<>1__state = -1;
			<ScanLiteralAsync>d__.<>t__builder.Start<DtdParser.<ScanLiteralAsync>d__186>(ref <ScanLiteralAsync>d__);
			return <ScanLiteralAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00060C10 File Offset: 0x0005EE10
		private Task<DtdParser.Token> ScanNotation1Async()
		{
			DtdParser.<ScanNotation1Async>d__187 <ScanNotation1Async>d__;
			<ScanNotation1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanNotation1Async>d__.<>4__this = this;
			<ScanNotation1Async>d__.<>1__state = -1;
			<ScanNotation1Async>d__.<>t__builder.Start<DtdParser.<ScanNotation1Async>d__187>(ref <ScanNotation1Async>d__);
			return <ScanNotation1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00060C54 File Offset: 0x0005EE54
		private Task<DtdParser.Token> ScanSystemIdAsync()
		{
			DtdParser.<ScanSystemIdAsync>d__188 <ScanSystemIdAsync>d__;
			<ScanSystemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanSystemIdAsync>d__.<>4__this = this;
			<ScanSystemIdAsync>d__.<>1__state = -1;
			<ScanSystemIdAsync>d__.<>t__builder.Start<DtdParser.<ScanSystemIdAsync>d__188>(ref <ScanSystemIdAsync>d__);
			return <ScanSystemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00060C98 File Offset: 0x0005EE98
		private Task<DtdParser.Token> ScanEntity1Async()
		{
			DtdParser.<ScanEntity1Async>d__189 <ScanEntity1Async>d__;
			<ScanEntity1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanEntity1Async>d__.<>4__this = this;
			<ScanEntity1Async>d__.<>1__state = -1;
			<ScanEntity1Async>d__.<>t__builder.Start<DtdParser.<ScanEntity1Async>d__189>(ref <ScanEntity1Async>d__);
			return <ScanEntity1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00060CDC File Offset: 0x0005EEDC
		private Task<DtdParser.Token> ScanEntity2Async()
		{
			DtdParser.<ScanEntity2Async>d__190 <ScanEntity2Async>d__;
			<ScanEntity2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanEntity2Async>d__.<>4__this = this;
			<ScanEntity2Async>d__.<>1__state = -1;
			<ScanEntity2Async>d__.<>t__builder.Start<DtdParser.<ScanEntity2Async>d__190>(ref <ScanEntity2Async>d__);
			return <ScanEntity2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00060D20 File Offset: 0x0005EF20
		private Task<DtdParser.Token> ScanEntity3Async()
		{
			DtdParser.<ScanEntity3Async>d__191 <ScanEntity3Async>d__;
			<ScanEntity3Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanEntity3Async>d__.<>4__this = this;
			<ScanEntity3Async>d__.<>1__state = -1;
			<ScanEntity3Async>d__.<>t__builder.Start<DtdParser.<ScanEntity3Async>d__191>(ref <ScanEntity3Async>d__);
			return <ScanEntity3Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00060D64 File Offset: 0x0005EF64
		private Task<DtdParser.Token> ScanPublicId1Async()
		{
			DtdParser.<ScanPublicId1Async>d__192 <ScanPublicId1Async>d__;
			<ScanPublicId1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanPublicId1Async>d__.<>4__this = this;
			<ScanPublicId1Async>d__.<>1__state = -1;
			<ScanPublicId1Async>d__.<>t__builder.Start<DtdParser.<ScanPublicId1Async>d__192>(ref <ScanPublicId1Async>d__);
			return <ScanPublicId1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00060DA8 File Offset: 0x0005EFA8
		private Task<DtdParser.Token> ScanPublicId2Async()
		{
			DtdParser.<ScanPublicId2Async>d__193 <ScanPublicId2Async>d__;
			<ScanPublicId2Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanPublicId2Async>d__.<>4__this = this;
			<ScanPublicId2Async>d__.<>1__state = -1;
			<ScanPublicId2Async>d__.<>t__builder.Start<DtdParser.<ScanPublicId2Async>d__193>(ref <ScanPublicId2Async>d__);
			return <ScanPublicId2Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00060DEC File Offset: 0x0005EFEC
		private Task<DtdParser.Token> ScanCondSection1Async()
		{
			DtdParser.<ScanCondSection1Async>d__194 <ScanCondSection1Async>d__;
			<ScanCondSection1Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanCondSection1Async>d__.<>4__this = this;
			<ScanCondSection1Async>d__.<>1__state = -1;
			<ScanCondSection1Async>d__.<>t__builder.Start<DtdParser.<ScanCondSection1Async>d__194>(ref <ScanCondSection1Async>d__);
			return <ScanCondSection1Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00060E30 File Offset: 0x0005F030
		private Task<DtdParser.Token> ScanCondSection3Async()
		{
			DtdParser.<ScanCondSection3Async>d__195 <ScanCondSection3Async>d__;
			<ScanCondSection3Async>d__.<>t__builder = AsyncTaskMethodBuilder<DtdParser.Token>.Create();
			<ScanCondSection3Async>d__.<>4__this = this;
			<ScanCondSection3Async>d__.<>1__state = -1;
			<ScanCondSection3Async>d__.<>t__builder.Start<DtdParser.<ScanCondSection3Async>d__195>(ref <ScanCondSection3Async>d__);
			return <ScanCondSection3Async>d__.<>t__builder.Task;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00060E73 File Offset: 0x0005F073
		private Task ScanNameAsync()
		{
			return this.ScanQNameAsync(false);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00060E7C File Offset: 0x0005F07C
		private Task ScanQNameAsync()
		{
			return this.ScanQNameAsync(this.SupportNamespaces);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00060E8C File Offset: 0x0005F08C
		private Task ScanQNameAsync(bool isQName)
		{
			DtdParser.<ScanQNameAsync>d__198 <ScanQNameAsync>d__;
			<ScanQNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ScanQNameAsync>d__.<>4__this = this;
			<ScanQNameAsync>d__.isQName = isQName;
			<ScanQNameAsync>d__.<>1__state = -1;
			<ScanQNameAsync>d__.<>t__builder.Start<DtdParser.<ScanQNameAsync>d__198>(ref <ScanQNameAsync>d__);
			return <ScanQNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00060ED8 File Offset: 0x0005F0D8
		private Task<bool> ReadDataInNameAsync()
		{
			DtdParser.<ReadDataInNameAsync>d__199 <ReadDataInNameAsync>d__;
			<ReadDataInNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadDataInNameAsync>d__.<>4__this = this;
			<ReadDataInNameAsync>d__.<>1__state = -1;
			<ReadDataInNameAsync>d__.<>t__builder.Start<DtdParser.<ReadDataInNameAsync>d__199>(ref <ReadDataInNameAsync>d__);
			return <ReadDataInNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00060F1C File Offset: 0x0005F11C
		private Task ScanNmtokenAsync()
		{
			DtdParser.<ScanNmtokenAsync>d__200 <ScanNmtokenAsync>d__;
			<ScanNmtokenAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ScanNmtokenAsync>d__.<>4__this = this;
			<ScanNmtokenAsync>d__.<>1__state = -1;
			<ScanNmtokenAsync>d__.<>t__builder.Start<DtdParser.<ScanNmtokenAsync>d__200>(ref <ScanNmtokenAsync>d__);
			return <ScanNmtokenAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00060F60 File Offset: 0x0005F160
		private Task<bool> EatPublicKeywordAsync()
		{
			DtdParser.<EatPublicKeywordAsync>d__201 <EatPublicKeywordAsync>d__;
			<EatPublicKeywordAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<EatPublicKeywordAsync>d__.<>4__this = this;
			<EatPublicKeywordAsync>d__.<>1__state = -1;
			<EatPublicKeywordAsync>d__.<>t__builder.Start<DtdParser.<EatPublicKeywordAsync>d__201>(ref <EatPublicKeywordAsync>d__);
			return <EatPublicKeywordAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00060FA4 File Offset: 0x0005F1A4
		private Task<bool> EatSystemKeywordAsync()
		{
			DtdParser.<EatSystemKeywordAsync>d__202 <EatSystemKeywordAsync>d__;
			<EatSystemKeywordAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<EatSystemKeywordAsync>d__.<>4__this = this;
			<EatSystemKeywordAsync>d__.<>1__state = -1;
			<EatSystemKeywordAsync>d__.<>t__builder.Start<DtdParser.<EatSystemKeywordAsync>d__202>(ref <EatSystemKeywordAsync>d__);
			return <EatSystemKeywordAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00060FE8 File Offset: 0x0005F1E8
		private Task<int> ReadDataAsync()
		{
			DtdParser.<ReadDataAsync>d__203 <ReadDataAsync>d__;
			<ReadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadDataAsync>d__.<>4__this = this;
			<ReadDataAsync>d__.<>1__state = -1;
			<ReadDataAsync>d__.<>t__builder.Start<DtdParser.<ReadDataAsync>d__203>(ref <ReadDataAsync>d__);
			return <ReadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0006102B File Offset: 0x0005F22B
		private Task<bool> HandleEntityReferenceAsync(bool paramEntity, bool inLiteral, bool inAttribute)
		{
			this.curPos++;
			return this.HandleEntityReferenceAsync(this.ScanEntityName(), paramEntity, inLiteral, inAttribute);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0006104C File Offset: 0x0005F24C
		private Task<bool> HandleEntityReferenceAsync(XmlQualifiedName entityName, bool paramEntity, bool inLiteral, bool inAttribute)
		{
			DtdParser.<HandleEntityReferenceAsync>d__205 <HandleEntityReferenceAsync>d__;
			<HandleEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<HandleEntityReferenceAsync>d__.<>4__this = this;
			<HandleEntityReferenceAsync>d__.entityName = entityName;
			<HandleEntityReferenceAsync>d__.paramEntity = paramEntity;
			<HandleEntityReferenceAsync>d__.inLiteral = inLiteral;
			<HandleEntityReferenceAsync>d__.inAttribute = inAttribute;
			<HandleEntityReferenceAsync>d__.<>1__state = -1;
			<HandleEntityReferenceAsync>d__.<>t__builder.Start<DtdParser.<HandleEntityReferenceAsync>d__205>(ref <HandleEntityReferenceAsync>d__);
			return <HandleEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000626 RID: 1574
		private IDtdParserAdapter readerAdapter;

		// Token: 0x04000627 RID: 1575
		private IDtdParserAdapterWithValidation readerAdapterWithValidation;

		// Token: 0x04000628 RID: 1576
		private XmlNameTable nameTable;

		// Token: 0x04000629 RID: 1577
		private SchemaInfo schemaInfo;

		// Token: 0x0400062A RID: 1578
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x0400062B RID: 1579
		private string systemId = string.Empty;

		// Token: 0x0400062C RID: 1580
		private string publicId = string.Empty;

		// Token: 0x0400062D RID: 1581
		private bool normalize = true;

		// Token: 0x0400062E RID: 1582
		private bool validate;

		// Token: 0x0400062F RID: 1583
		private bool supportNamespaces = true;

		// Token: 0x04000630 RID: 1584
		private bool v1Compat;

		// Token: 0x04000631 RID: 1585
		private char[] chars;

		// Token: 0x04000632 RID: 1586
		private int charsUsed;

		// Token: 0x04000633 RID: 1587
		private int curPos;

		// Token: 0x04000634 RID: 1588
		private DtdParser.ScanningFunction scanningFunction;

		// Token: 0x04000635 RID: 1589
		private DtdParser.ScanningFunction nextScaningFunction;

		// Token: 0x04000636 RID: 1590
		private DtdParser.ScanningFunction savedScanningFunction;

		// Token: 0x04000637 RID: 1591
		private bool whitespaceSeen;

		// Token: 0x04000638 RID: 1592
		private int tokenStartPos;

		// Token: 0x04000639 RID: 1593
		private int colonPos;

		// Token: 0x0400063A RID: 1594
		private StringBuilder internalSubsetValueSb;

		// Token: 0x0400063B RID: 1595
		private int externalEntitiesDepth;

		// Token: 0x0400063C RID: 1596
		private int currentEntityId;

		// Token: 0x0400063D RID: 1597
		private bool freeFloatingDtd;

		// Token: 0x0400063E RID: 1598
		private bool hasFreeFloatingInternalSubset;

		// Token: 0x0400063F RID: 1599
		private StringBuilder stringBuilder;

		// Token: 0x04000640 RID: 1600
		private int condSectionDepth;

		// Token: 0x04000641 RID: 1601
		private LineInfo literalLineInfo = new LineInfo(0, 0);

		// Token: 0x04000642 RID: 1602
		private char literalQuoteChar = '"';

		// Token: 0x04000643 RID: 1603
		private string documentBaseUri = string.Empty;

		// Token: 0x04000644 RID: 1604
		private string externalDtdBaseUri = string.Empty;

		// Token: 0x04000645 RID: 1605
		private Dictionary<string, DtdParser.UndeclaredNotation> undeclaredNotations;

		// Token: 0x04000646 RID: 1606
		private int[] condSectionEntityIds;

		// Token: 0x04000647 RID: 1607
		private const int CondSectionEntityIdsInitialSize = 2;

		// Token: 0x02000441 RID: 1089
		private enum Token
		{
			// Token: 0x04001C5F RID: 7263
			CDATA,
			// Token: 0x04001C60 RID: 7264
			ID,
			// Token: 0x04001C61 RID: 7265
			IDREF,
			// Token: 0x04001C62 RID: 7266
			IDREFS,
			// Token: 0x04001C63 RID: 7267
			ENTITY,
			// Token: 0x04001C64 RID: 7268
			ENTITIES,
			// Token: 0x04001C65 RID: 7269
			NMTOKEN,
			// Token: 0x04001C66 RID: 7270
			NMTOKENS,
			// Token: 0x04001C67 RID: 7271
			NOTATION,
			// Token: 0x04001C68 RID: 7272
			None,
			// Token: 0x04001C69 RID: 7273
			PERef,
			// Token: 0x04001C6A RID: 7274
			AttlistDecl,
			// Token: 0x04001C6B RID: 7275
			ElementDecl,
			// Token: 0x04001C6C RID: 7276
			EntityDecl,
			// Token: 0x04001C6D RID: 7277
			NotationDecl,
			// Token: 0x04001C6E RID: 7278
			Comment,
			// Token: 0x04001C6F RID: 7279
			PI,
			// Token: 0x04001C70 RID: 7280
			CondSectionStart,
			// Token: 0x04001C71 RID: 7281
			CondSectionEnd,
			// Token: 0x04001C72 RID: 7282
			Eof,
			// Token: 0x04001C73 RID: 7283
			REQUIRED,
			// Token: 0x04001C74 RID: 7284
			IMPLIED,
			// Token: 0x04001C75 RID: 7285
			FIXED,
			// Token: 0x04001C76 RID: 7286
			QName,
			// Token: 0x04001C77 RID: 7287
			Name,
			// Token: 0x04001C78 RID: 7288
			Nmtoken,
			// Token: 0x04001C79 RID: 7289
			Quote,
			// Token: 0x04001C7A RID: 7290
			LeftParen,
			// Token: 0x04001C7B RID: 7291
			RightParen,
			// Token: 0x04001C7C RID: 7292
			GreaterThan,
			// Token: 0x04001C7D RID: 7293
			Or,
			// Token: 0x04001C7E RID: 7294
			LeftBracket,
			// Token: 0x04001C7F RID: 7295
			RightBracket,
			// Token: 0x04001C80 RID: 7296
			PUBLIC,
			// Token: 0x04001C81 RID: 7297
			SYSTEM,
			// Token: 0x04001C82 RID: 7298
			Literal,
			// Token: 0x04001C83 RID: 7299
			DOCTYPE,
			// Token: 0x04001C84 RID: 7300
			NData,
			// Token: 0x04001C85 RID: 7301
			Percent,
			// Token: 0x04001C86 RID: 7302
			Star,
			// Token: 0x04001C87 RID: 7303
			QMark,
			// Token: 0x04001C88 RID: 7304
			Plus,
			// Token: 0x04001C89 RID: 7305
			PCDATA,
			// Token: 0x04001C8A RID: 7306
			Comma,
			// Token: 0x04001C8B RID: 7307
			ANY,
			// Token: 0x04001C8C RID: 7308
			EMPTY,
			// Token: 0x04001C8D RID: 7309
			IGNORE,
			// Token: 0x04001C8E RID: 7310
			INCLUDE
		}

		// Token: 0x02000442 RID: 1090
		private enum ScanningFunction
		{
			// Token: 0x04001C90 RID: 7312
			SubsetContent,
			// Token: 0x04001C91 RID: 7313
			Name,
			// Token: 0x04001C92 RID: 7314
			QName,
			// Token: 0x04001C93 RID: 7315
			Nmtoken,
			// Token: 0x04001C94 RID: 7316
			Doctype1,
			// Token: 0x04001C95 RID: 7317
			Doctype2,
			// Token: 0x04001C96 RID: 7318
			Element1,
			// Token: 0x04001C97 RID: 7319
			Element2,
			// Token: 0x04001C98 RID: 7320
			Element3,
			// Token: 0x04001C99 RID: 7321
			Element4,
			// Token: 0x04001C9A RID: 7322
			Element5,
			// Token: 0x04001C9B RID: 7323
			Element6,
			// Token: 0x04001C9C RID: 7324
			Element7,
			// Token: 0x04001C9D RID: 7325
			Attlist1,
			// Token: 0x04001C9E RID: 7326
			Attlist2,
			// Token: 0x04001C9F RID: 7327
			Attlist3,
			// Token: 0x04001CA0 RID: 7328
			Attlist4,
			// Token: 0x04001CA1 RID: 7329
			Attlist5,
			// Token: 0x04001CA2 RID: 7330
			Attlist6,
			// Token: 0x04001CA3 RID: 7331
			Attlist7,
			// Token: 0x04001CA4 RID: 7332
			Entity1,
			// Token: 0x04001CA5 RID: 7333
			Entity2,
			// Token: 0x04001CA6 RID: 7334
			Entity3,
			// Token: 0x04001CA7 RID: 7335
			Notation1,
			// Token: 0x04001CA8 RID: 7336
			CondSection1,
			// Token: 0x04001CA9 RID: 7337
			CondSection2,
			// Token: 0x04001CAA RID: 7338
			CondSection3,
			// Token: 0x04001CAB RID: 7339
			Literal,
			// Token: 0x04001CAC RID: 7340
			SystemId,
			// Token: 0x04001CAD RID: 7341
			PublicId1,
			// Token: 0x04001CAE RID: 7342
			PublicId2,
			// Token: 0x04001CAF RID: 7343
			ClosingTag,
			// Token: 0x04001CB0 RID: 7344
			ParamEntitySpace,
			// Token: 0x04001CB1 RID: 7345
			None
		}

		// Token: 0x02000443 RID: 1091
		private enum LiteralType
		{
			// Token: 0x04001CB3 RID: 7347
			AttributeValue,
			// Token: 0x04001CB4 RID: 7348
			EntityReplText,
			// Token: 0x04001CB5 RID: 7349
			SystemOrPublicID
		}

		// Token: 0x02000444 RID: 1092
		private class UndeclaredNotation
		{
			// Token: 0x0600305D RID: 12381 RVA: 0x001142FE File Offset: 0x001124FE
			internal UndeclaredNotation(string name, int lineNo, int linePos)
			{
				this.name = name;
				this.lineNo = lineNo;
				this.linePos = linePos;
				this.next = null;
			}

			// Token: 0x04001CB6 RID: 7350
			internal string name;

			// Token: 0x04001CB7 RID: 7351
			internal int lineNo;

			// Token: 0x04001CB8 RID: 7352
			internal int linePos;

			// Token: 0x04001CB9 RID: 7353
			internal DtdParser.UndeclaredNotation next;
		}

		// Token: 0x02000445 RID: 1093
		private class ParseElementOnlyContent_LocalFrame
		{
			// Token: 0x0600305E RID: 12382 RVA: 0x00114322 File Offset: 0x00112522
			public ParseElementOnlyContent_LocalFrame(int startParentEntityIdParam)
			{
				this.startParenEntityId = startParentEntityIdParam;
				this.parsingSchema = DtdParser.Token.None;
			}

			// Token: 0x04001CBA RID: 7354
			public int startParenEntityId;

			// Token: 0x04001CBB RID: 7355
			public DtdParser.Token parsingSchema;
		}
	}
}
