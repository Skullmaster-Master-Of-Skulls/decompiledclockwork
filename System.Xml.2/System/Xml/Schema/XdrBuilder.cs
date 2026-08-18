using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x02000267 RID: 615
	internal sealed class XdrBuilder : SchemaBuilder
	{
		// Token: 0x06002499 RID: 9369 RVA: 0x000C8488 File Offset: 0x000C6688
		internal XdrBuilder(XmlReader reader, XmlNamespaceManager curmgr, SchemaInfo sinfo, string targetNamspace, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this._SchemaInfo = sinfo;
			this._TargetNamespace = targetNamspace;
			this._reader = reader;
			this._CurNsMgr = curmgr;
			this.validationEventHandler = eventhandler;
			this._StateHistory = new HWStack(10);
			this._ElementDef = new XdrBuilder.ElementContent();
			this._AttributeDef = new XdrBuilder.AttributeContent();
			this._GroupStack = new HWStack(10);
			this._GroupDef = new XdrBuilder.GroupContent();
			this._NameTable = nameTable;
			this._SchemaNames = schemaNames;
			this._CurState = XdrBuilder.S_SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(this._reader);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000C8540 File Offset: 0x000C6740
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				if (this._CurState._InitFunc != null)
				{
					this._CurState._InitFunc(this, xmlQualifiedName);
				}
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("Sch_UnsupportedElement", XmlQualifiedName.ToString(name, prefix));
			}
			return false;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000C85B4 File Offset: 0x000C67B4
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			int i = 0;
			while (i < this._CurState._Attributes.Length)
			{
				XdrBuilder.XdrAttributeEntry xdrAttributeEntry = this._CurState._Attributes[i];
				if (this._SchemaNames.TokenToQName[(int)xdrAttributeEntry._Attribute].Equals(xmlQualifiedName))
				{
					XdrBuilder.XdrBuildFunction buildFunc = xdrAttributeEntry._BuildFunc;
					if (xdrAttributeEntry._Datatype.TokenizedType == XmlTokenizedType.QName)
					{
						string text;
						XmlQualifiedName xmlQualifiedName2 = XmlQualifiedName.Parse(value, this._CurNsMgr, out text);
						xmlQualifiedName2.Atomize(this._NameTable);
						if (text.Length != 0)
						{
							if (xdrAttributeEntry._Attribute != SchemaNames.Token.SchemaType)
							{
								throw new XmlException("Xml_UnexpectedToken", "NAME");
							}
						}
						else if (this.IsGlobal(xdrAttributeEntry._SchemaFlags))
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name, this._TargetNamespace);
						}
						else
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name);
						}
						buildFunc(this, xmlQualifiedName2, text);
						return;
					}
					buildFunc(this, xdrAttributeEntry._Datatype.ParseValue(value, this._NameTable, this._CurNsMgr), string.Empty);
					return;
				}
				else
				{
					i++;
				}
			}
			if (ns == this._SchemaNames.NsXmlNs && XdrBuilder.IsXdrSchema(value))
			{
				this.LoadSchema(value);
				return;
			}
			if (!this.IsSkipableAttribute(xmlQualifiedName))
			{
				this.SendValidationEvent("Sch_UnsupportedAttribute", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
		}

		// Token: 0x17000811 RID: 2065
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x000C871E File Offset: 0x000C691E
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000C8728 File Offset: 0x000C6928
		private bool LoadSchema(string uri)
		{
			if (this.xmlResolver == null)
			{
				return false;
			}
			uri = this._NameTable.Add(uri);
			if (this._SchemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return false;
			}
			SchemaInfo schemaInfo = null;
			Uri baseUri = this.xmlResolver.ResolveUri(null, this._reader.BaseURI);
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = this.xmlResolver.ResolveUri(baseUri, uri.Substring("x-schema:".Length));
				Stream input = (Stream)this.xmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), input, this._NameTable);
				schemaInfo = new SchemaInfo();
				Parser parser = new Parser(SchemaType.XDR, this._NameTable, this._SchemaNames, this.validationEventHandler);
				parser.XmlResolver = this.xmlResolver;
				parser.Parse(xmlReader, uri);
				schemaInfo = parser.XdrSchema;
			}
			catch (XmlException ex)
			{
				this.SendValidationEvent("Sch_CannotLoadSchema", new string[]
				{
					uri,
					ex.Message
				}, XmlSeverityType.Warning);
				schemaInfo = null;
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
				this._SchemaInfo.Add(schemaInfo, this.validationEventHandler);
				return true;
			}
			return false;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000C8874 File Offset: 0x000C6A74
		internal static bool IsXdrSchema(string uri)
		{
			return uri.Length >= "x-schema:".Length && string.Compare(uri, 0, "x-schema:", 0, "x-schema:".Length, StringComparison.Ordinal) == 0 && !uri.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000C88B3 File Offset: 0x000C6AB3
		internal override bool IsContentParsed()
		{
			return true;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000C88B6 File Offset: 0x000C6AB6
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000C88C7 File Offset: 0x000C6AC7
		internal override void ProcessCData(string value)
		{
			if (this._CurState._AllowText)
			{
				this._Text = value;
				return;
			}
			this.SendValidationEvent("Sch_TextNotAllowed", value);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000C88EA File Offset: 0x000C6AEA
		internal override void StartChildren()
		{
			if (this._CurState._BeginChildFunc != null)
			{
				this._CurState._BeginChildFunc(this);
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000C890A File Offset: 0x000C6B0A
		internal override void EndChildren()
		{
			if (this._CurState._EndChildFunc != null)
			{
				this._CurState._EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000C8930 File Offset: 0x000C6B30
		private void Push()
		{
			this._StateHistory.Push();
			this._StateHistory[this._StateHistory.Length - 1] = this._CurState;
			this._CurState = this._NextState;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000C8968 File Offset: 0x000C6B68
		private void Pop()
		{
			this._CurState = (XdrBuilder.XdrEntry)this._StateHistory.Pop();
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000C8980 File Offset: 0x000C6B80
		private void PushGroupInfo()
		{
			this._GroupStack.Push();
			this._GroupStack[this._GroupStack.Length - 1] = XdrBuilder.GroupContent.Copy(this._GroupDef);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000C89B1 File Offset: 0x000C6BB1
		private void PopGroupInfo()
		{
			this._GroupDef = (XdrBuilder.GroupContent)this._GroupStack.Pop();
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000C89C9 File Offset: 0x000C6BC9
		private static void XDR_InitRoot(XdrBuilder builder, object obj)
		{
			builder._SchemaInfo.SchemaType = SchemaType.XDR;
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
			builder._AttributeDef._AttDef = null;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000C89FB File Offset: 0x000C6BFB
		private static void XDR_BuildRoot_Name(XdrBuilder builder, object obj, string prefix)
		{
			builder._XdrName = (string)obj;
			builder._XdrPrefix = prefix;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000C8A10 File Offset: 0x000C6C10
		private static void XDR_BuildRoot_ID(XdrBuilder builder, object obj, string prefix)
		{
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000C8A14 File Offset: 0x000C6C14
		private static void XDR_BeginRoot(XdrBuilder builder)
		{
			if (builder._TargetNamespace == null)
			{
				if (builder._XdrName != null)
				{
					builder._TargetNamespace = builder._NameTable.Add("x-schema:#" + builder._XdrName);
				}
				else
				{
					builder._TargetNamespace = string.Empty;
				}
			}
			builder._SchemaInfo.TargetNamespaces.Add(builder._TargetNamespace, true);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000C8A78 File Offset: 0x000C6C78
		private static void XDR_EndRoot(XdrBuilder builder)
		{
			while (builder._UndefinedAttributeTypes != null)
			{
				XmlQualifiedName xmlQualifiedName = builder._UndefinedAttributeTypes._TypeName;
				if (xmlQualifiedName.Namespace.Length == 0)
				{
					xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(xmlQualifiedName, out schemaAttDef))
				{
					builder._UndefinedAttributeTypes._Attdef = schemaAttDef.Clone();
					builder._UndefinedAttributeTypes._Attdef.Name = xmlQualifiedName;
					builder.XDR_CheckAttributeDefault(builder._UndefinedAttributeTypes, builder._UndefinedAttributeTypes._Attdef);
				}
				else
				{
					builder.SendValidationEvent("Sch_UndeclaredAttribute", xmlQualifiedName.Name);
				}
				builder._UndefinedAttributeTypes = builder._UndefinedAttributeTypes._Next;
			}
			foreach (object obj in builder._UndeclaredElements.Values)
			{
				SchemaElementDecl schemaElementDecl = (SchemaElementDecl)obj;
				builder.SendValidationEvent("Sch_UndeclaredElement", XmlQualifiedName.ToString(schemaElementDecl.Name.Name, schemaElementDecl.Prefix));
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000C8BA0 File Offset: 0x000C6DA0
		private static void XDR_InitElementType(XdrBuilder builder, object obj)
		{
			builder._ElementDef._ElementDecl = new SchemaElementDecl();
			builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			builder._contentValidator.IsOpen = true;
			builder._ElementDef._ContentAttr = 0;
			builder._ElementDef._OrderAttr = 0;
			builder._ElementDef._MasterGroupRequired = false;
			builder._ElementDef._ExistTerminal = false;
			builder._ElementDef._AllowDataType = true;
			builder._ElementDef._HasDataType = false;
			builder._ElementDef._EnumerationRequired = false;
			builder._ElementDef._AttDefList = new Hashtable();
			builder._ElementDef._MaxLength = uint.MaxValue;
			builder._ElementDef._MinLength = uint.MaxValue;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000C8C54 File Offset: 0x000C6E54
		private static void XDR_BuildElementType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName))
			{
				builder.SendValidationEvent("Sch_DupElementDecl", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
			builder._ElementDef._ElementDecl.Name = xmlQualifiedName;
			builder._ElementDef._ElementDecl.Prefix = prefix;
			builder._SchemaInfo.ElementDecls.Add(xmlQualifiedName, builder._ElementDef._ElementDecl);
			if (builder._UndeclaredElements[xmlQualifiedName] != null)
			{
				builder._UndeclaredElements.Remove(xmlQualifiedName);
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000C8CEA File Offset: 0x000C6EEA
		private static void XDR_BuildElementType_Content(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._ContentAttr = builder.GetContent((XmlQualifiedName)obj);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000C8D03 File Offset: 0x000C6F03
		private static void XDR_BuildElementType_Model(XdrBuilder builder, object obj, string prefix)
		{
			builder._contentValidator.IsOpen = builder.GetModel((XmlQualifiedName)obj);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000C8D1C File Offset: 0x000C6F1C
		private static void XDR_BuildElementType_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._OrderAttr = (builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj));
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000C8D50 File Offset: 0x000C6F50
		private static void XDR_BuildElementType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._HasDataType = true;
			string text = ((string)obj).Trim();
			if (text.Length == 0)
			{
				builder.SendValidationEvent("Sch_MissDtvalue");
				return;
			}
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(text);
			if (xmlSchemaDatatype == null)
			{
				builder.SendValidationEvent("Sch_UnknownDtType", text);
			}
			builder._ElementDef._ElementDecl.Datatype = xmlSchemaDatatype;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000C8DB0 File Offset: 0x000C6FB0
		private static void XDR_BuildElementType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._EnumerationRequired = true;
			builder._ElementDef._ElementDecl.Values = new List<string>((string[])obj);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000C8DD9 File Offset: 0x000C6FD9
		private static void XDR_BuildElementType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._ElementDef._MaxLength, obj, builder);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000C8DED File Offset: 0x000C6FED
		private static void XDR_BuildElementType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._ElementDef._MinLength, obj, builder);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000C8E04 File Offset: 0x000C7004
		private static void XDR_BeginElementType(XdrBuilder builder)
		{
			string text = null;
			string msg = null;
			if (builder._ElementDef._ElementDecl.Name.IsEmpty)
			{
				text = "Sch_MissAttribute";
				msg = "name";
			}
			else
			{
				if (builder._ElementDef._HasDataType)
				{
					if (!builder._ElementDef._AllowDataType)
					{
						text = "Sch_DataTypeTextOnly";
						goto IL_1F4;
					}
					builder._ElementDef._ContentAttr = 2;
				}
				else if (builder._ElementDef._ContentAttr == 0)
				{
					switch (builder._ElementDef._OrderAttr)
					{
					case 0:
						builder._ElementDef._ContentAttr = 3;
						builder._ElementDef._OrderAttr = 1;
						break;
					case 1:
						builder._ElementDef._ContentAttr = 3;
						break;
					case 2:
						builder._ElementDef._ContentAttr = 4;
						break;
					case 3:
						builder._ElementDef._ContentAttr = 4;
						break;
					}
				}
				bool isOpen = builder._contentValidator.IsOpen;
				XdrBuilder.ElementContent elementDef = builder._ElementDef;
				switch (builder._ElementDef._ContentAttr)
				{
				case 1:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Empty;
					builder._contentValidator = null;
					break;
				case 2:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
					builder._GroupDef._Order = 1;
					builder._contentValidator = null;
					break;
				case 3:
					if (elementDef._OrderAttr != 0 && elementDef._OrderAttr != 1)
					{
						text = "Sch_MixedMany";
						goto IL_1F4;
					}
					builder._GroupDef._Order = 1;
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				case 4:
					builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
					if (elementDef._OrderAttr == 0)
					{
						builder._GroupDef._Order = 2;
					}
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				}
				if (elementDef._ContentAttr == 3 || elementDef._ContentAttr == 4)
				{
					builder._contentValidator.Start();
					builder._contentValidator.OpenGroup();
				}
			}
			IL_1F4:
			if (text != null)
			{
				builder.SendValidationEvent(text, msg);
			}
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000C9010 File Offset: 0x000C7210
		private static void XDR_EndElementType(XdrBuilder builder)
		{
			SchemaElementDecl elementDecl = builder._ElementDef._ElementDecl;
			if (builder._UndefinedAttributeTypes != null && builder._ElementDef._AttDefList != null)
			{
				XdrBuilder.DeclBaseInfo declBaseInfo = builder._UndefinedAttributeTypes;
				XdrBuilder.DeclBaseInfo declBaseInfo2 = declBaseInfo;
				while (declBaseInfo != null)
				{
					SchemaAttDef schemaAttDef = null;
					if (declBaseInfo._ElementDecl == elementDecl)
					{
						XmlQualifiedName typeName = declBaseInfo._TypeName;
						schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
						if (schemaAttDef != null)
						{
							declBaseInfo._Attdef = schemaAttDef.Clone();
							declBaseInfo._Attdef.Name = typeName;
							builder.XDR_CheckAttributeDefault(declBaseInfo, schemaAttDef);
							if (declBaseInfo == builder._UndefinedAttributeTypes)
							{
								declBaseInfo = (builder._UndefinedAttributeTypes = declBaseInfo._Next);
								declBaseInfo2 = declBaseInfo;
							}
							else
							{
								declBaseInfo2._Next = declBaseInfo._Next;
								declBaseInfo = declBaseInfo2._Next;
							}
						}
					}
					if (schemaAttDef == null)
					{
						if (declBaseInfo != builder._UndefinedAttributeTypes)
						{
							declBaseInfo2 = declBaseInfo2._Next;
						}
						declBaseInfo = declBaseInfo._Next;
					}
				}
			}
			if (builder._ElementDef._MasterGroupRequired)
			{
				builder._contentValidator.CloseGroup();
				if (!builder._ElementDef._ExistTerminal)
				{
					if (builder._contentValidator.IsOpen)
					{
						builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Any;
						builder._contentValidator = null;
					}
					else if (builder._ElementDef._ContentAttr != 3)
					{
						builder.SendValidationEvent("Sch_ElementMissing");
					}
				}
				else if (builder._GroupDef._Order == 1)
				{
					builder._contentValidator.AddStar();
				}
			}
			if (elementDecl.Datatype != null)
			{
				XmlTokenizedType tokenizedType = elementDecl.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("Sch_MissDtvaluesAttribute");
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("Sch_RequireEnumeration");
				}
			}
			XdrBuilder.CompareMinMaxLength(builder._ElementDef._MinLength, builder._ElementDef._MaxLength, builder);
			elementDecl.MaxLength = (long)((ulong)builder._ElementDef._MaxLength);
			elementDecl.MinLength = (long)((ulong)builder._ElementDef._MinLength);
			if (builder._contentValidator != null)
			{
				builder._ElementDef._ElementDecl.ContentValidator = builder._contentValidator.Finish(true);
				builder._contentValidator = null;
			}
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000C924C File Offset: 0x000C744C
		private static void XDR_InitAttributeType(XdrBuilder builder, object obj)
		{
			XdrBuilder.AttributeContent attributeDef = builder._AttributeDef;
			attributeDef._AttDef = new SchemaAttDef(XmlQualifiedName.Empty, null);
			attributeDef._Required = false;
			attributeDef._Prefix = null;
			attributeDef._Default = null;
			attributeDef._MinVal = 0U;
			attributeDef._MaxVal = 1U;
			attributeDef._EnumerationRequired = false;
			attributeDef._HasDataType = false;
			attributeDef._Global = (builder._StateHistory.Length == 2);
			attributeDef._MaxLength = uint.MaxValue;
			attributeDef._MinLength = uint.MaxValue;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000C92C4 File Offset: 0x000C74C4
		private static void XDR_BuildAttributeType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._Name = xmlQualifiedName;
			builder._AttributeDef._Prefix = prefix;
			builder._AttributeDef._AttDef.Name = xmlQualifiedName;
			if (builder._ElementDef._ElementDecl != null)
			{
				if (builder._ElementDef._AttDefList[xmlQualifiedName] == null)
				{
					builder._ElementDef._AttDefList.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("Sch_DupAttribute", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				builder._AttributeDef._AttDef.Name = xmlQualifiedName;
				if (!builder._SchemaInfo.AttributeDecls.ContainsKey(xmlQualifiedName))
				{
					builder._SchemaInfo.AttributeDecls.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("Sch_DupAttribute", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000C93C0 File Offset: 0x000C75C0
		private static void XDR_BuildAttributeType_Required(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Required = XdrBuilder.IsYes(obj, builder);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000C93D4 File Offset: 0x000C75D4
		private static void XDR_BuildAttributeType_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Default = obj;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000C93E4 File Offset: 0x000C75E4
		private static void XDR_BuildAttributeType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._HasDataType = true;
			builder._AttributeDef._AttDef.Datatype = builder.CheckDatatype(xmlQualifiedName.Name);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000C9420 File Offset: 0x000C7620
		private static void XDR_BuildAttributeType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._EnumerationRequired = true;
			builder._AttributeDef._AttDef.Values = new List<string>((string[])obj);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000C9449 File Offset: 0x000C7649
		private static void XDR_BuildAttributeType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._AttributeDef._MaxLength, obj, builder);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000C945D File Offset: 0x000C765D
		private static void XDR_BuildAttributeType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._AttributeDef._MinLength, obj, builder);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000C9471 File Offset: 0x000C7671
		private static void XDR_BeginAttributeType(XdrBuilder builder)
		{
			if (builder._AttributeDef._Name.IsEmpty)
			{
				builder.SendValidationEvent("Sch_MissAttribute");
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000C9490 File Offset: 0x000C7690
		private static void XDR_EndAttributeType(XdrBuilder builder)
		{
			string text = null;
			if (builder._AttributeDef._HasDataType && builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "Sch_MissDtvaluesAttribute";
					goto IL_164;
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Sch_RequireEnumeration";
					goto IL_164;
				}
				if (builder._AttributeDef._Default != null && tokenizedType == XmlTokenizedType.ID)
				{
					text = "Sch_DefaultIdValue";
					goto IL_164;
				}
			}
			else
			{
				builder._AttributeDef._AttDef.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);
			}
			XdrBuilder.CompareMinMaxLength(builder._AttributeDef._MinLength, builder._AttributeDef._MaxLength, builder);
			builder._AttributeDef._AttDef.MaxLength = (long)((ulong)builder._AttributeDef._MaxLength);
			builder._AttributeDef._AttDef.MinLength = (long)((ulong)builder._AttributeDef._MinLength);
			if (builder._AttributeDef._Default != null)
			{
				builder._AttributeDef._AttDef.DefaultValueRaw = (builder._AttributeDef._AttDef.DefaultValueExpanded = (string)builder._AttributeDef._Default);
				builder.CheckDefaultAttValue(builder._AttributeDef._AttDef);
			}
			builder.SetAttributePresence(builder._AttributeDef._AttDef, builder._AttributeDef._Required);
			IL_164:
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000C960C File Offset: 0x000C780C
		private static void XDR_InitElement(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType || builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("Sch_ElementNotAllowed");
			}
			builder._ElementDef._AllowDataType = false;
			builder._ElementDef._HasType = false;
			builder._ElementDef._MinVal = 1U;
			builder._ElementDef._MaxVal = 1U;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000C9680 File Offset: 0x000C7880
		private static void XDR_BuildElement_Type(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (!builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName) && (SchemaElementDecl)builder._UndeclaredElements[xmlQualifiedName] == null)
			{
				SchemaElementDecl value = new SchemaElementDecl(xmlQualifiedName, prefix);
				builder._UndeclaredElements.Add(xmlQualifiedName, value);
			}
			builder._ElementDef._HasType = true;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			else
			{
				builder._ElementDef._ExistTerminal = true;
			}
			builder._contentValidator.AddName(xmlQualifiedName, null);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000C970B File Offset: 0x000C790B
		private static void XDR_BuildElement_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000C971F File Offset: 0x000C791F
		private static void XDR_BuildElement_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000C9733 File Offset: 0x000C7933
		private static void XDR_EndElement(XdrBuilder builder)
		{
			if (builder._ElementDef._HasType)
			{
				XdrBuilder.HandleMinMax(builder._contentValidator, builder._ElementDef._MinVal, builder._ElementDef._MaxVal);
				return;
			}
			builder.SendValidationEvent("Sch_MissAttribute");
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000C976F File Offset: 0x000C796F
		private static void XDR_InitAttribute(XdrBuilder builder, object obj)
		{
			if (builder._BaseDecl == null)
			{
				builder._BaseDecl = new XdrBuilder.DeclBaseInfo();
			}
			builder._BaseDecl._MinOccurs = 0U;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000C9790 File Offset: 0x000C7990
		private static void XDR_BuildAttribute_Type(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._TypeName = (XmlQualifiedName)obj;
			builder._BaseDecl._Prefix = prefix;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000C97AF File Offset: 0x000C79AF
		private static void XDR_BuildAttribute_Required(XdrBuilder builder, object obj, string prefix)
		{
			if (XdrBuilder.IsYes(obj, builder))
			{
				builder._BaseDecl._MinOccurs = 1U;
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000C97C6 File Offset: 0x000C79C6
		private static void XDR_BuildAttribute_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._Default = obj;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000C97D4 File Offset: 0x000C79D4
		private static void XDR_BeginAttribute(XdrBuilder builder)
		{
			if (builder._BaseDecl._TypeName.IsEmpty)
			{
				builder.SendValidationEvent("Sch_MissAttribute");
			}
			SchemaAttDef schemaAttDef = null;
			XmlQualifiedName typeName = builder._BaseDecl._TypeName;
			string prefix = builder._BaseDecl._Prefix;
			if (builder._ElementDef._AttDefList != null)
			{
				schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
			}
			if (schemaAttDef == null)
			{
				XmlQualifiedName key = typeName;
				if (prefix.Length == 0)
				{
					key = new XmlQualifiedName(typeName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef2;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(key, out schemaAttDef2))
				{
					schemaAttDef = schemaAttDef2.Clone();
					schemaAttDef.Name = typeName;
				}
				else if (prefix.Length != 0)
				{
					builder.SendValidationEvent("Sch_UndeclaredAttribute", XmlQualifiedName.ToString(typeName.Name, prefix));
				}
			}
			if (schemaAttDef != null)
			{
				builder.XDR_CheckAttributeDefault(builder._BaseDecl, schemaAttDef);
			}
			else
			{
				schemaAttDef = new SchemaAttDef(typeName, prefix);
				builder._UndefinedAttributeTypes = new XdrBuilder.DeclBaseInfo
				{
					_Checking = true,
					_Attdef = schemaAttDef,
					_TypeName = builder._BaseDecl._TypeName,
					_ElementDecl = builder._ElementDef._ElementDecl,
					_MinOccurs = builder._BaseDecl._MinOccurs,
					_Default = builder._BaseDecl._Default,
					_Next = builder._UndefinedAttributeTypes
				};
			}
			builder._ElementDef._ElementDecl.AddAttDef(schemaAttDef);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000C993F File Offset: 0x000C7B3F
		private static void XDR_EndAttribute(XdrBuilder builder)
		{
			builder._BaseDecl.Reset();
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000C994C File Offset: 0x000C7B4C
		private static void XDR_InitGroup(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("Sch_GroupDisabled");
			}
			builder.PushGroupInfo();
			builder._GroupDef._MinVal = 1U;
			builder._GroupDef._MaxVal = 1U;
			builder._GroupDef._HasMaxAttr = false;
			builder._GroupDef._HasMinAttr = false;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			builder._ElementDef._ExistTerminal = false;
			builder._contentValidator.OpenGroup();
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000C99E0 File Offset: 0x000C7BE0
		private static void XDR_BuildGroup_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj);
			if (builder._ElementDef._ContentAttr == 3 && builder._GroupDef._Order != 1)
			{
				builder.SendValidationEvent("Sch_MixedMany");
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000C9A20 File Offset: 0x000C7C20
		private static void XDR_BuildGroup_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
			builder._GroupDef._HasMinAttr = true;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000C9A40 File Offset: 0x000C7C40
		private static void XDR_BuildGroup_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
			builder._GroupDef._HasMaxAttr = true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000C9A60 File Offset: 0x000C7C60
		private static void XDR_EndGroup(XdrBuilder builder)
		{
			if (!builder._ElementDef._ExistTerminal)
			{
				builder.SendValidationEvent("Sch_ElementMissing");
			}
			builder._contentValidator.CloseGroup();
			if (builder._GroupDef._Order == 1)
			{
				builder._contentValidator.AddStar();
			}
			if (1 == builder._GroupDef._Order && builder._GroupDef._HasMaxAttr && builder._GroupDef._MaxVal != 4294967295U)
			{
				builder.SendValidationEvent("Sch_ManyMaxOccurs");
			}
			XdrBuilder.HandleMinMax(builder._contentValidator, builder._GroupDef._MinVal, builder._GroupDef._MaxVal);
			builder.PopGroupInfo();
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000C9B04 File Offset: 0x000C7D04
		private static void XDR_InitElementDtType(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("Sch_DupDtType");
			}
			if (!builder._ElementDef._AllowDataType)
			{
				builder.SendValidationEvent("Sch_DataTypeTextOnly");
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000C9B38 File Offset: 0x000C7D38
		private static void XDR_EndElementDtType(XdrBuilder builder)
		{
			if (!builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("Sch_MissAttribute");
			}
			builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
			builder._ElementDef._ContentAttr = 2;
			builder._ElementDef._MasterGroupRequired = false;
			builder._contentValidator = null;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000C9B91 File Offset: 0x000C7D91
		private static void XDR_InitAttributeDtType(XdrBuilder builder, object obj)
		{
			if (builder._AttributeDef._HasDataType)
			{
				builder.SendValidationEvent("Sch_DupDtType");
			}
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000C9BAC File Offset: 0x000C7DAC
		private static void XDR_EndAttributeDtType(XdrBuilder builder)
		{
			string text = null;
			if (!builder._AttributeDef._HasDataType)
			{
				text = "Sch_MissAttribute";
			}
			else if (builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "Sch_MissDtvaluesAttribute";
				}
				else if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Sch_RequireEnumeration";
				}
			}
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000C9C34 File Offset: 0x000C7E34
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this._CurState._NextStates != null)
			{
				for (int i = 0; i < this._CurState._NextStates.Length; i++)
				{
					if (this._SchemaNames.TokenToQName[(int)XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]]._Name].Equals(qname))
					{
						this._NextState = XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000C9CB0 File Offset: 0x000C7EB0
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace != null && !Ref.Equal(@namespace, this._SchemaNames.NsXdr)) || (this._SchemaNames.TokenToQName[38].Equals(qname) || this._SchemaNames.TokenToQName[39].Equals(qname));
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000C9D0C File Offset: 0x000C7F0C
		private bool IsSkipableAttribute(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace.Length != 0 && !Ref.Equal(@namespace, this._SchemaNames.NsXdr) && !Ref.Equal(@namespace, this._SchemaNames.NsDataType)) || (Ref.Equal(@namespace, this._SchemaNames.NsDataType) && this._CurState._Name == SchemaNames.Token.XdrDatatype && (this._SchemaNames.QnDtMax.Equals(qname) || this._SchemaNames.QnDtMin.Equals(qname) || this._SchemaNames.QnDtMaxExclusive.Equals(qname) || this._SchemaNames.QnDtMinExclusive.Equals(qname)));
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000C9DC4 File Offset: 0x000C7FC4
		private int GetOrder(XmlQualifiedName qname)
		{
			int result = 0;
			if (this._SchemaNames.TokenToQName[15].Equals(qname))
			{
				result = 2;
			}
			else if (this._SchemaNames.TokenToQName[16].Equals(qname))
			{
				result = 3;
			}
			else if (this._SchemaNames.TokenToQName[17].Equals(qname))
			{
				result = 1;
			}
			else
			{
				this.SendValidationEvent("Sch_UnknownOrder", qname.Name);
			}
			return result;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000C9E34 File Offset: 0x000C8034
		private void AddOrder()
		{
			switch (this._GroupDef._Order)
			{
			case 1:
			case 3:
				this._contentValidator.AddChoice();
				return;
			case 2:
				this._contentValidator.AddSequence();
				return;
			}
			throw new XmlException("Xml_UnexpectedToken", "NAME");
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000C9E90 File Offset: 0x000C8090
		private static bool IsYes(object obj, XdrBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			bool result = false;
			if (xmlQualifiedName.Name == "yes")
			{
				result = true;
			}
			else if (xmlQualifiedName.Name != "no")
			{
				builder.SendValidationEvent("Sch_UnknownRequired");
			}
			return result;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000C9EDC File Offset: 0x000C80DC
		private static uint ParseMinOccurs(object obj, XdrBuilder builder)
		{
			uint num = 1U;
			if (!XdrBuilder.ParseInteger((string)obj, ref num) || (num != 0U && num != 1U))
			{
				builder.SendValidationEvent("Sch_MinOccursInvalid");
			}
			return num;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000C9F10 File Offset: 0x000C8110
		private static uint ParseMaxOccurs(object obj, XdrBuilder builder)
		{
			uint maxValue = uint.MaxValue;
			string text = (string)obj;
			if (!text.Equals("*") && (!XdrBuilder.ParseInteger(text, ref maxValue) || (maxValue != 4294967295U && maxValue != 1U)))
			{
				builder.SendValidationEvent("Sch_MaxOccursInvalid");
			}
			return maxValue;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000C9F51 File Offset: 0x000C8151
		private static void HandleMinMax(ParticleContentValidator pContent, uint cMin, uint cMax)
		{
			if (pContent != null)
			{
				if (cMax == 4294967295U)
				{
					if (cMin == 0U)
					{
						pContent.AddStar();
						return;
					}
					pContent.AddPlus();
					return;
				}
				else if (cMin == 0U)
				{
					pContent.AddQMark();
				}
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000C9F74 File Offset: 0x000C8174
		private static void ParseDtMaxLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("Sch_DupDtMaxLength");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("Sch_DtMaxLengthInvalid", obj.ToString());
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000C9FAA File Offset: 0x000C81AA
		private static void ParseDtMinLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("Sch_DupDtMinLength");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("Sch_DtMinLengthInvalid", obj.ToString());
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000C9FE0 File Offset: 0x000C81E0
		private static void CompareMinMaxLength(uint cMin, uint cMax, XdrBuilder builder)
		{
			if (cMin != 4294967295U && cMax != 4294967295U && cMin > cMax)
			{
				builder.SendValidationEvent("Sch_DtMinMaxLength");
			}
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000C9FF9 File Offset: 0x000C81F9
		private static bool ParseInteger(string str, ref uint n)
		{
			return uint.TryParse(str, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out n);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000CA008 File Offset: 0x000C8208
		private void XDR_CheckAttributeDefault(XdrBuilder.DeclBaseInfo decl, SchemaAttDef pAttdef)
		{
			if ((decl._Default != null || pAttdef.DefaultValueTyped != null) && decl._Default != null)
			{
				pAttdef.DefaultValueRaw = (pAttdef.DefaultValueExpanded = (string)decl._Default);
				this.CheckDefaultAttValue(pAttdef);
			}
			this.SetAttributePresence(pAttdef, 1U == decl._MinOccurs);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000CA060 File Offset: 0x000C8260
		private void SetAttributePresence(SchemaAttDef pAttdef, bool fRequired)
		{
			if (SchemaDeclBase.Use.Fixed != pAttdef.Presence)
			{
				if (fRequired || SchemaDeclBase.Use.Required == pAttdef.Presence)
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Fixed;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Required;
					return;
				}
				else
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Default;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Implied;
				}
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000CA0B4 File Offset: 0x000C82B4
		private int GetContent(XmlQualifiedName qname)
		{
			int result = 0;
			if (this._SchemaNames.TokenToQName[11].Equals(qname))
			{
				result = 1;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[12].Equals(qname))
			{
				result = 4;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[10].Equals(qname))
			{
				result = 3;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[13].Equals(qname))
			{
				result = 2;
			}
			else
			{
				this.SendValidationEvent("Sch_UnknownContent", qname.Name);
			}
			return result;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000CA164 File Offset: 0x000C8364
		private bool GetModel(XmlQualifiedName qname)
		{
			bool result = false;
			if (this._SchemaNames.TokenToQName[7].Equals(qname))
			{
				result = true;
			}
			else if (this._SchemaNames.TokenToQName[8].Equals(qname))
			{
				result = false;
			}
			else
			{
				this.SendValidationEvent("Sch_UnknownModel", qname.Name);
			}
			return result;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000CA1B8 File Offset: 0x000C83B8
		private XmlSchemaDatatype CheckDatatype(string str)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(str);
			if (xmlSchemaDatatype == null)
			{
				this.SendValidationEvent("Sch_UnknownDtType", str);
			}
			else if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.ID && !this._AttributeDef._Global)
			{
				if (this._ElementDef._ElementDecl.IsIdDeclared)
				{
					this.SendValidationEvent("Sch_IdAttrDeclared", XmlQualifiedName.ToString(this._ElementDef._ElementDecl.Name.Name, this._ElementDef._ElementDecl.Prefix));
				}
				this._ElementDef._ElementDecl.IsIdDeclared = true;
			}
			return xmlSchemaDatatype;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000CA24C File Offset: 0x000C844C
		private void CheckDefaultAttValue(SchemaAttDef attDef)
		{
			string value = attDef.DefaultValueRaw.Trim();
			XdrValidator.CheckDefaultValue(value, attDef, this._SchemaInfo, this._CurNsMgr, this._NameTable, null, this.validationEventHandler, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000CA2A6 File Offset: 0x000C84A6
		private bool IsGlobal(int flags)
		{
			return flags == 256;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000CA2B0 File Offset: 0x000C84B0
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000CA2E1 File Offset: 0x000C84E1
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000CA2EF File Offset: 0x000C84EF
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000CA320 File Offset: 0x000C8520
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			SchemaInfo schemaInfo = this._SchemaInfo;
			int errorCount = schemaInfo.ErrorCount;
			schemaInfo.ErrorCount = errorCount + 1;
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x04000FFB RID: 4091
		private const int XdrSchema = 1;

		// Token: 0x04000FFC RID: 4092
		private const int XdrElementType = 2;

		// Token: 0x04000FFD RID: 4093
		private const int XdrAttributeType = 3;

		// Token: 0x04000FFE RID: 4094
		private const int XdrElement = 4;

		// Token: 0x04000FFF RID: 4095
		private const int XdrAttribute = 5;

		// Token: 0x04001000 RID: 4096
		private const int XdrGroup = 6;

		// Token: 0x04001001 RID: 4097
		private const int XdrElementDatatype = 7;

		// Token: 0x04001002 RID: 4098
		private const int XdrAttributeDatatype = 8;

		// Token: 0x04001003 RID: 4099
		private const int SchemaFlagsNs = 256;

		// Token: 0x04001004 RID: 4100
		private const int StackIncrement = 10;

		// Token: 0x04001005 RID: 4101
		private const int SchemaOrderNone = 0;

		// Token: 0x04001006 RID: 4102
		private const int SchemaOrderMany = 1;

		// Token: 0x04001007 RID: 4103
		private const int SchemaOrderSequence = 2;

		// Token: 0x04001008 RID: 4104
		private const int SchemaOrderChoice = 3;

		// Token: 0x04001009 RID: 4105
		private const int SchemaOrderAll = 4;

		// Token: 0x0400100A RID: 4106
		private const int SchemaContentNone = 0;

		// Token: 0x0400100B RID: 4107
		private const int SchemaContentEmpty = 1;

		// Token: 0x0400100C RID: 4108
		private const int SchemaContentText = 2;

		// Token: 0x0400100D RID: 4109
		private const int SchemaContentMixed = 3;

		// Token: 0x0400100E RID: 4110
		private const int SchemaContentElement = 4;

		// Token: 0x0400100F RID: 4111
		private static readonly int[] S_XDR_Root_Element = new int[]
		{
			1
		};

		// Token: 0x04001010 RID: 4112
		private static readonly int[] S_XDR_Root_SubElements = new int[]
		{
			2,
			3
		};

		// Token: 0x04001011 RID: 4113
		private static readonly int[] S_XDR_ElementType_SubElements = new int[]
		{
			4,
			6,
			3,
			5,
			7
		};

		// Token: 0x04001012 RID: 4114
		private static readonly int[] S_XDR_AttributeType_SubElements = new int[]
		{
			8
		};

		// Token: 0x04001013 RID: 4115
		private static readonly int[] S_XDR_Group_SubElements = new int[]
		{
			4,
			6
		};

		// Token: 0x04001014 RID: 4116
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Root_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaId, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_ID))
		};

		// Token: 0x04001015 RID: 4117
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaContent, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Content)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaModel, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Model)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04001016 RID: 4118
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Default)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04001017 RID: 4119
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Element_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MaxOccurs))
		};

		// Token: 0x04001018 RID: 4120
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Attribute_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Default))
		};

		// Token: 0x04001019 RID: 4121
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Group_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MaxOccurs))
		};

		// Token: 0x0400101A RID: 4122
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x0400101B RID: 4123
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x0400101C RID: 4124
		private static readonly XdrBuilder.XdrEntry[] S_SchemaEntries = new XdrBuilder.XdrEntry[]
		{
			new XdrBuilder.XdrEntry(SchemaNames.Token.Empty, XdrBuilder.S_XDR_Root_Element, null, null, null, null, false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrRoot, XdrBuilder.S_XDR_Root_SubElements, XdrBuilder.S_XDR_Root_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitRoot), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginRoot), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndRoot), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElementType, XdrBuilder.S_XDR_ElementType_SubElements, XdrBuilder.S_XDR_ElementType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginElementType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttributeType, XdrBuilder.S_XDR_AttributeType_SubElements, XdrBuilder.S_XDR_AttributeType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttributeType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElement, null, XdrBuilder.S_XDR_Element_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElement), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElement), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttribute, null, XdrBuilder.S_XDR_Attribute_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttribute), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttribute), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttribute), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrGroup, XdrBuilder.S_XDR_Group_SubElements, XdrBuilder.S_XDR_Group_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitGroup), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndGroup), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_ElementDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementDtType), true),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_AttributeDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeDtType), true)
		};

		// Token: 0x0400101D RID: 4125
		private SchemaInfo _SchemaInfo;

		// Token: 0x0400101E RID: 4126
		private string _TargetNamespace;

		// Token: 0x0400101F RID: 4127
		private XmlReader _reader;

		// Token: 0x04001020 RID: 4128
		private PositionInfo positionInfo;

		// Token: 0x04001021 RID: 4129
		private ParticleContentValidator _contentValidator;

		// Token: 0x04001022 RID: 4130
		private XdrBuilder.XdrEntry _CurState;

		// Token: 0x04001023 RID: 4131
		private XdrBuilder.XdrEntry _NextState;

		// Token: 0x04001024 RID: 4132
		private HWStack _StateHistory;

		// Token: 0x04001025 RID: 4133
		private HWStack _GroupStack;

		// Token: 0x04001026 RID: 4134
		private string _XdrName;

		// Token: 0x04001027 RID: 4135
		private string _XdrPrefix;

		// Token: 0x04001028 RID: 4136
		private XdrBuilder.ElementContent _ElementDef;

		// Token: 0x04001029 RID: 4137
		private XdrBuilder.GroupContent _GroupDef;

		// Token: 0x0400102A RID: 4138
		private XdrBuilder.AttributeContent _AttributeDef;

		// Token: 0x0400102B RID: 4139
		private XdrBuilder.DeclBaseInfo _UndefinedAttributeTypes;

		// Token: 0x0400102C RID: 4140
		private XdrBuilder.DeclBaseInfo _BaseDecl;

		// Token: 0x0400102D RID: 4141
		private XmlNameTable _NameTable;

		// Token: 0x0400102E RID: 4142
		private SchemaNames _SchemaNames;

		// Token: 0x0400102F RID: 4143
		private XmlNamespaceManager _CurNsMgr;

		// Token: 0x04001030 RID: 4144
		private string _Text;

		// Token: 0x04001031 RID: 4145
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001032 RID: 4146
		private Hashtable _UndeclaredElements = new Hashtable();

		// Token: 0x04001033 RID: 4147
		private const string x_schema = "x-schema:";

		// Token: 0x04001034 RID: 4148
		private XmlResolver xmlResolver;

		// Token: 0x02000499 RID: 1177
		private sealed class DeclBaseInfo
		{
			// Token: 0x0600314F RID: 12623 RVA: 0x0011FD03 File Offset: 0x0011DF03
			internal DeclBaseInfo()
			{
				this.Reset();
			}

			// Token: 0x06003150 RID: 12624 RVA: 0x0011FD14 File Offset: 0x0011DF14
			internal void Reset()
			{
				this._Name = XmlQualifiedName.Empty;
				this._Prefix = null;
				this._TypeName = XmlQualifiedName.Empty;
				this._TypePrefix = null;
				this._Default = null;
				this._Revises = null;
				this._MaxOccurs = 1U;
				this._MinOccurs = 1U;
				this._Checking = false;
				this._ElementDecl = null;
				this._Next = null;
				this._Attdef = null;
			}

			// Token: 0x04001EC7 RID: 7879
			internal XmlQualifiedName _Name;

			// Token: 0x04001EC8 RID: 7880
			internal string _Prefix;

			// Token: 0x04001EC9 RID: 7881
			internal XmlQualifiedName _TypeName;

			// Token: 0x04001ECA RID: 7882
			internal string _TypePrefix;

			// Token: 0x04001ECB RID: 7883
			internal object _Default;

			// Token: 0x04001ECC RID: 7884
			internal object _Revises;

			// Token: 0x04001ECD RID: 7885
			internal uint _MaxOccurs;

			// Token: 0x04001ECE RID: 7886
			internal uint _MinOccurs;

			// Token: 0x04001ECF RID: 7887
			internal bool _Checking;

			// Token: 0x04001ED0 RID: 7888
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04001ED1 RID: 7889
			internal SchemaAttDef _Attdef;

			// Token: 0x04001ED2 RID: 7890
			internal XdrBuilder.DeclBaseInfo _Next;
		}

		// Token: 0x0200049A RID: 1178
		private sealed class GroupContent
		{
			// Token: 0x06003151 RID: 12625 RVA: 0x0011FD7D File Offset: 0x0011DF7D
			internal static void Copy(XdrBuilder.GroupContent from, XdrBuilder.GroupContent to)
			{
				to._MinVal = from._MinVal;
				to._MaxVal = from._MaxVal;
				to._Order = from._Order;
			}

			// Token: 0x06003152 RID: 12626 RVA: 0x0011FDA4 File Offset: 0x0011DFA4
			internal static XdrBuilder.GroupContent Copy(XdrBuilder.GroupContent other)
			{
				XdrBuilder.GroupContent groupContent = new XdrBuilder.GroupContent();
				XdrBuilder.GroupContent.Copy(other, groupContent);
				return groupContent;
			}

			// Token: 0x04001ED3 RID: 7891
			internal uint _MinVal;

			// Token: 0x04001ED4 RID: 7892
			internal uint _MaxVal;

			// Token: 0x04001ED5 RID: 7893
			internal bool _HasMaxAttr;

			// Token: 0x04001ED6 RID: 7894
			internal bool _HasMinAttr;

			// Token: 0x04001ED7 RID: 7895
			internal int _Order;
		}

		// Token: 0x0200049B RID: 1179
		private sealed class ElementContent
		{
			// Token: 0x04001ED8 RID: 7896
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04001ED9 RID: 7897
			internal int _ContentAttr;

			// Token: 0x04001EDA RID: 7898
			internal int _OrderAttr;

			// Token: 0x04001EDB RID: 7899
			internal bool _MasterGroupRequired;

			// Token: 0x04001EDC RID: 7900
			internal bool _ExistTerminal;

			// Token: 0x04001EDD RID: 7901
			internal bool _AllowDataType;

			// Token: 0x04001EDE RID: 7902
			internal bool _HasDataType;

			// Token: 0x04001EDF RID: 7903
			internal bool _HasType;

			// Token: 0x04001EE0 RID: 7904
			internal bool _EnumerationRequired;

			// Token: 0x04001EE1 RID: 7905
			internal uint _MinVal;

			// Token: 0x04001EE2 RID: 7906
			internal uint _MaxVal;

			// Token: 0x04001EE3 RID: 7907
			internal uint _MaxLength;

			// Token: 0x04001EE4 RID: 7908
			internal uint _MinLength;

			// Token: 0x04001EE5 RID: 7909
			internal Hashtable _AttDefList;
		}

		// Token: 0x0200049C RID: 1180
		private sealed class AttributeContent
		{
			// Token: 0x04001EE6 RID: 7910
			internal SchemaAttDef _AttDef;

			// Token: 0x04001EE7 RID: 7911
			internal XmlQualifiedName _Name;

			// Token: 0x04001EE8 RID: 7912
			internal string _Prefix;

			// Token: 0x04001EE9 RID: 7913
			internal bool _Required;

			// Token: 0x04001EEA RID: 7914
			internal uint _MinVal;

			// Token: 0x04001EEB RID: 7915
			internal uint _MaxVal;

			// Token: 0x04001EEC RID: 7916
			internal uint _MaxLength;

			// Token: 0x04001EED RID: 7917
			internal uint _MinLength;

			// Token: 0x04001EEE RID: 7918
			internal bool _EnumerationRequired;

			// Token: 0x04001EEF RID: 7919
			internal bool _HasDataType;

			// Token: 0x04001EF0 RID: 7920
			internal bool _Global;

			// Token: 0x04001EF1 RID: 7921
			internal object _Default;
		}

		// Token: 0x0200049D RID: 1181
		// (Invoke) Token: 0x06003157 RID: 12631
		private delegate void XdrBuildFunction(XdrBuilder builder, object obj, string prefix);

		// Token: 0x0200049E RID: 1182
		// (Invoke) Token: 0x0600315B RID: 12635
		private delegate void XdrInitFunction(XdrBuilder builder, object obj);

		// Token: 0x0200049F RID: 1183
		// (Invoke) Token: 0x0600315F RID: 12639
		private delegate void XdrBeginChildFunction(XdrBuilder builder);

		// Token: 0x020004A0 RID: 1184
		// (Invoke) Token: 0x06003163 RID: 12643
		private delegate void XdrEndChildFunction(XdrBuilder builder);

		// Token: 0x020004A1 RID: 1185
		private sealed class XdrAttributeEntry
		{
			// Token: 0x06003166 RID: 12646 RVA: 0x0011FDD7 File Offset: 0x0011DFD7
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = 0;
				this._BuildFunc = build;
			}

			// Token: 0x06003167 RID: 12647 RVA: 0x0011FE00 File Offset: 0x0011E000
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, int schemaFlags, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = schemaFlags;
				this._BuildFunc = build;
			}

			// Token: 0x04001EF2 RID: 7922
			internal SchemaNames.Token _Attribute;

			// Token: 0x04001EF3 RID: 7923
			internal int _SchemaFlags;

			// Token: 0x04001EF4 RID: 7924
			internal XmlSchemaDatatype _Datatype;

			// Token: 0x04001EF5 RID: 7925
			internal XdrBuilder.XdrBuildFunction _BuildFunc;
		}

		// Token: 0x020004A2 RID: 1186
		private sealed class XdrEntry
		{
			// Token: 0x06003168 RID: 12648 RVA: 0x0011FE2A File Offset: 0x0011E02A
			internal XdrEntry(SchemaNames.Token n, int[] states, XdrBuilder.XdrAttributeEntry[] attributes, XdrBuilder.XdrInitFunction init, XdrBuilder.XdrBeginChildFunction begin, XdrBuilder.XdrEndChildFunction end, bool fText)
			{
				this._Name = n;
				this._NextStates = states;
				this._Attributes = attributes;
				this._InitFunc = init;
				this._BeginChildFunc = begin;
				this._EndChildFunc = end;
				this._AllowText = fText;
			}

			// Token: 0x04001EF6 RID: 7926
			internal SchemaNames.Token _Name;

			// Token: 0x04001EF7 RID: 7927
			internal int[] _NextStates;

			// Token: 0x04001EF8 RID: 7928
			internal XdrBuilder.XdrAttributeEntry[] _Attributes;

			// Token: 0x04001EF9 RID: 7929
			internal XdrBuilder.XdrInitFunction _InitFunc;

			// Token: 0x04001EFA RID: 7930
			internal XdrBuilder.XdrBeginChildFunction _BeginChildFunc;

			// Token: 0x04001EFB RID: 7931
			internal XdrBuilder.XdrEndChildFunction _EndChildFunc;

			// Token: 0x04001EFC RID: 7932
			internal bool _AllowText;
		}
	}
}
