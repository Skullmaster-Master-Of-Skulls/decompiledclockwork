using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x020002BC RID: 700
	public sealed class XmlSchemaValidator
	{
		// Token: 0x0600283C RID: 10300 RVA: 0x000D2500 File Offset: 0x000D0700
		public XmlSchemaValidator(XmlNameTable nameTable, XmlSchemaSet schemas, IXmlNamespaceResolver namespaceResolver, XmlSchemaValidationFlags validationFlags)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (namespaceResolver == null)
			{
				throw new ArgumentNullException("namespaceResolver");
			}
			this.nameTable = nameTable;
			this.nsResolver = namespaceResolver;
			this.validationFlags = validationFlags;
			if ((validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None || (validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) != XmlSchemaValidationFlags.None)
			{
				this.schemaSet = new XmlSchemaSet(nameTable);
				this.schemaSet.ValidationEventHandler += schemas.GetEventHandler();
				this.schemaSet.CompilationSettings = schemas.CompilationSettings;
				this.schemaSet.XmlResolver = schemas.GetResolver();
				this.schemaSet.Add(schemas);
				this.validatedNamespaces = new Hashtable();
			}
			else
			{
				this.schemaSet = schemas;
			}
			this.Init();
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000D25E0 File Offset: 0x000D07E0
		private void Init()
		{
			this.validationStack = new HWStack(10);
			this.attPresence = new Hashtable();
			this.Push(XmlQualifiedName.Empty);
			this.dummyPositionInfo = new PositionInfo();
			this.positionInfo = this.dummyPositionInfo;
			this.validationEventSender = this;
			this.currentState = ValidatorState.None;
			this.textValue = new StringBuilder(100);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.contextQName = new XmlQualifiedName();
			this.Reset();
			this.RecompileSchemaSet();
			this.NsXs = this.nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.NsXmlNs = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXml = this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.xsiTypeString = this.nameTable.Add("type");
			this.xsiNilString = this.nameTable.Add("nil");
			this.xsiSchemaLocationString = this.nameTable.Add("schemaLocation");
			this.xsiNoNamespaceSchemaLocationString = this.nameTable.Add("noNamespaceSchemaLocation");
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000D2714 File Offset: 0x000D0914
		private void Reset()
		{
			this.isRoot = true;
			this.rootHasSchema = true;
			while (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
			}
			this.startIDConstraint = -1;
			this.partialValidationType = null;
			if (this.IDs != null)
			{
				this.IDs.Clear();
			}
			if (this.ProcessSchemaHints)
			{
				this.validatedNamespaces.Clear();
			}
		}

		// Token: 0x17000951 RID: 2385
		// (set) Token: 0x0600283F RID: 10303 RVA: 0x000D277F File Offset: 0x000D097F
		public XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000D2788 File Offset: 0x000D0988
		// (set) Token: 0x06002841 RID: 10305 RVA: 0x000D2790 File Offset: 0x000D0990
		public IXmlLineInfo LineInfoProvider
		{
			get
			{
				return this.positionInfo;
			}
			set
			{
				if (value == null)
				{
					this.positionInfo = this.dummyPositionInfo;
					return;
				}
				this.positionInfo = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000D27A9 File Offset: 0x000D09A9
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x000D27B1 File Offset: 0x000D09B1
		public Uri SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
				this.sourceUriString = this.sourceUri.ToString();
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x000D27CB File Offset: 0x000D09CB
		// (set) Token: 0x06002845 RID: 10309 RVA: 0x000D27D3 File Offset: 0x000D09D3
		public object ValidationEventSender
		{
			get
			{
				return this.validationEventSender;
			}
			set
			{
				this.validationEventSender = value;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06002846 RID: 10310 RVA: 0x000D27DC File Offset: 0x000D09DC
		// (remove) Token: 0x06002847 RID: 10311 RVA: 0x000D27F5 File Offset: 0x000D09F5
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000D2810 File Offset: 0x000D0A10
		public void AddSchema(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if ((this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) == XmlSchemaValidationFlags.None)
			{
				return;
			}
			string text = schema.TargetNamespace;
			if (text == null)
			{
				text = string.Empty;
			}
			Hashtable schemaLocations = this.schemaSet.SchemaLocations;
			DictionaryEntry[] array = new DictionaryEntry[schemaLocations.Count];
			schemaLocations.CopyTo(array, 0);
			if (this.validatedNamespaces[text] != null && this.schemaSet.FindSchemaByNSAndUrl(schema.BaseUri, text, array) == null)
			{
				this.SendValidationEvent("Sch_ComponentAlreadySeenForNS", text, XmlSeverityType.Error);
			}
			if (schema.ErrorCount == 0)
			{
				try
				{
					this.schemaSet.Add(schema);
					this.RecompileSchemaSet();
				}
				catch (XmlSchemaException ex)
				{
					this.SendValidationEvent("Sch_CannotLoadSchema", new string[]
					{
						schema.BaseUri.ToString(),
						ex.Message
					}, ex);
				}
				for (int i = 0; i < schema.ImportedSchemas.Count; i++)
				{
					XmlSchema xmlSchema = (XmlSchema)schema.ImportedSchemas[i];
					text = xmlSchema.TargetNamespace;
					if (text == null)
					{
						text = string.Empty;
					}
					if (this.validatedNamespaces[text] != null && this.schemaSet.FindSchemaByNSAndUrl(xmlSchema.BaseUri, text, array) == null)
					{
						this.SendValidationEvent("Sch_ComponentAlreadySeenForNS", text, XmlSeverityType.Error);
						this.schemaSet.RemoveRecursive(schema);
						return;
					}
				}
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000D2970 File Offset: 0x000D0B70
		public void Initialize()
		{
			if (this.currentState != ValidatorState.None && this.currentState != ValidatorState.Finish)
			{
				string name = "Sch_InvalidStateTransition";
				object[] args = new string[]
				{
					XmlSchemaValidator.MethodNames[(int)this.currentState],
					XmlSchemaValidator.MethodNames[1]
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
			this.currentState = ValidatorState.Start;
			this.Reset();
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000D29D0 File Offset: 0x000D0BD0
		public void Initialize(XmlSchemaObject partialValidationType)
		{
			if (this.currentState != ValidatorState.None && this.currentState != ValidatorState.Finish)
			{
				string name = "Sch_InvalidStateTransition";
				object[] args = new string[]
				{
					XmlSchemaValidator.MethodNames[(int)this.currentState],
					XmlSchemaValidator.MethodNames[1]
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (!(partialValidationType is XmlSchemaElement) && !(partialValidationType is XmlSchemaAttribute) && !(partialValidationType is XmlSchemaType))
			{
				throw new ArgumentException(Res.GetString("Sch_InvalidPartialValidationType"));
			}
			this.currentState = ValidatorState.Start;
			this.Reset();
			this.partialValidationType = partialValidationType;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000D2A6A File Offset: 0x000D0C6A
		public void ValidateElement(string localName, string namespaceUri, XmlSchemaInfo schemaInfo)
		{
			this.ValidateElement(localName, namespaceUri, schemaInfo, null, null, null, null);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000D2A7C File Offset: 0x000D0C7C
		public void ValidateElement(string localName, string namespaceUri, XmlSchemaInfo schemaInfo, string xsiType, string xsiNil, string xsiSchemaLocation, string xsiNoNamespaceSchemaLocation)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			this.CheckStateTransition(ValidatorState.Element, XmlSchemaValidator.MethodNames[4]);
			this.ClearPSVI();
			this.contextQName.Init(localName, namespaceUri);
			XmlQualifiedName xmlQualifiedName = this.contextQName;
			bool flag;
			object particle = this.ValidateElementContext(xmlQualifiedName, out flag);
			SchemaElementDecl schemaElementDecl = this.FastGetElementDecl(xmlQualifiedName, particle);
			this.Push(xmlQualifiedName);
			if (flag)
			{
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if ((this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) != XmlSchemaValidationFlags.None && this.xmlResolver != null)
			{
				this.ProcessSchemaLocations(xsiSchemaLocation, xsiNoNamespaceSchemaLocation);
			}
			if (this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (schemaElementDecl == null && this.partialValidationType == null)
				{
					schemaElementDecl = this.compiledSchemaInfo.GetElementDecl(xmlQualifiedName);
				}
				bool declFound = schemaElementDecl != null;
				if (xsiType != null || xsiNil != null)
				{
					schemaElementDecl = this.CheckXsiTypeAndNil(schemaElementDecl, xsiType, xsiNil, ref declFound);
				}
				if (schemaElementDecl == null)
				{
					this.ThrowDeclNotFoundWarningOrError(declFound);
				}
			}
			this.context.ElementDecl = schemaElementDecl;
			XmlSchemaElement schemaElement = null;
			XmlSchemaType schemaType = null;
			if (schemaElementDecl != null)
			{
				this.CheckElementProperties();
				this.attPresence.Clear();
				this.context.NeedValidateChildren = (this.processContents != XmlSchemaContentProcessing.Skip);
				this.ValidateStartElementIdentityConstraints();
				schemaElementDecl.ContentValidator.InitValidation(this.context);
				schemaType = schemaElementDecl.SchemaType;
				schemaElement = this.GetSchemaElement();
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaType = schemaType;
				schemaInfo.SchemaElement = schemaElement;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.Validity = this.context.Validity;
			}
			if (this.ProcessSchemaHints && this.validatedNamespaces[namespaceUri] == null)
			{
				this.validatedNamespaces.Add(namespaceUri, namespaceUri);
			}
			if (this.isRoot)
			{
				this.isRoot = false;
			}
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000D2C25 File Offset: 0x000D0E25
		public object ValidateAttribute(string localName, string namespaceUri, string attributeValue, XmlSchemaInfo schemaInfo)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, namespaceUri, null, attributeValue, schemaInfo);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000D2C41 File Offset: 0x000D0E41
		public object ValidateAttribute(string localName, string namespaceUri, XmlValueGetter attributeValue, XmlSchemaInfo schemaInfo)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, namespaceUri, attributeValue, null, schemaInfo);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000D2C60 File Offset: 0x000D0E60
		private object ValidateAttribute(string lName, string ns, XmlValueGetter attributeValueGetter, string attributeStringValue, XmlSchemaInfo schemaInfo)
		{
			if (lName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (ns == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			ValidatorState validatorState = (this.validationStack.Length > 1) ? ValidatorState.Attribute : ValidatorState.TopLevelAttribute;
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			object obj = null;
			this.attrValid = true;
			XmlSchemaValidity validity = XmlSchemaValidity.NotKnown;
			XmlSchemaAttribute xmlSchemaAttribute = null;
			XmlSchemaSimpleType memberType = null;
			ns = this.nameTable.Add(ns);
			if (Ref.Equal(ns, this.NsXmlNs))
			{
				return null;
			}
			SchemaAttDef schemaAttDef = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(lName, ns);
			if (this.attPresence[xmlQualifiedName] != null)
			{
				this.SendValidationEvent("Sch_DuplicateAttribute", xmlQualifiedName.ToString());
				if (schemaInfo != null)
				{
					schemaInfo.Clear();
				}
				return null;
			}
			if (!Ref.Equal(ns, this.NsXsi))
			{
				XmlSchemaObject xmlSchemaObject = (this.currentState == ValidatorState.TopLevelAttribute) ? this.partialValidationType : null;
				AttributeMatchState attributeMatchState;
				schemaAttDef = this.compiledSchemaInfo.GetAttributeXsd(elementDecl, xmlQualifiedName, xmlSchemaObject, out attributeMatchState);
				switch (attributeMatchState)
				{
				case AttributeMatchState.AttributeFound:
					break;
				case AttributeMatchState.AnyIdAttributeFound:
				{
					if (this.wildID != null)
					{
						this.SendValidationEvent("Sch_MoreThanOneWildId", string.Empty);
						goto IL_3FF;
					}
					this.wildID = schemaAttDef;
					XmlSchemaComplexType xmlSchemaComplexType = elementDecl.SchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType.ContainsIdAttribute(false))
					{
						this.SendValidationEvent("Sch_AttrUseAndWildId", string.Empty);
						goto IL_3FF;
					}
					break;
				}
				case AttributeMatchState.UndeclaredElementAndAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						if (elementDecl == null && this.processContents == XmlSchemaContentProcessing.Strict && xmlQualifiedName.Namespace.Length != 0 && this.compiledSchemaInfo.Contains(xmlQualifiedName.Namespace))
						{
							this.attrValid = false;
							this.SendValidationEvent("Sch_UndeclaredAttribute", xmlQualifiedName.ToString());
							goto IL_3FF;
						}
						if (this.processContents != XmlSchemaContentProcessing.Skip)
						{
							this.SendValidationEvent("Sch_NoAttributeSchemaFound", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
							goto IL_3FF;
						}
						goto IL_3FF;
					}
					break;
				case AttributeMatchState.UndeclaredAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						this.attrValid = false;
						this.SendValidationEvent("Sch_UndeclaredAttribute", xmlQualifiedName.ToString());
						goto IL_3FF;
					}
					break;
				case AttributeMatchState.AnyAttributeLax:
					this.SendValidationEvent("Sch_NoAttributeSchemaFound", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
					goto IL_3FF;
				case AttributeMatchState.AnyAttributeSkip:
					goto IL_3FF;
				case AttributeMatchState.ProhibitedAnyAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						this.attrValid = false;
						this.SendValidationEvent("Sch_ProhibitedAttribute", xmlQualifiedName.ToString());
						goto IL_3FF;
					}
					break;
				case AttributeMatchState.ProhibitedAttribute:
					this.attrValid = false;
					this.SendValidationEvent("Sch_ProhibitedAttribute", xmlQualifiedName.ToString());
					goto IL_3FF;
				case AttributeMatchState.AttributeNameMismatch:
					this.attrValid = false;
					this.SendValidationEvent("Sch_SchemaAttributeNameMismatch", new string[]
					{
						xmlQualifiedName.ToString(),
						((XmlSchemaAttribute)xmlSchemaObject).QualifiedName.ToString()
					});
					goto IL_3FF;
				case AttributeMatchState.ValidateAttributeInvalidCall:
					this.currentState = ValidatorState.Start;
					this.attrValid = false;
					this.SendValidationEvent("Sch_ValidateAttributeInvalidCall", string.Empty);
					goto IL_3FF;
				default:
					goto IL_3FF;
				}
				xmlSchemaAttribute = schemaAttDef.SchemaAttribute;
				if (elementDecl != null)
				{
					this.attPresence.Add(xmlQualifiedName, schemaAttDef);
				}
				object obj2;
				if (attributeValueGetter != null)
				{
					obj2 = attributeValueGetter();
				}
				else
				{
					obj2 = attributeStringValue;
				}
				obj = this.CheckAttributeValue(obj2, schemaAttDef);
				XmlSchemaDatatype datatype = schemaAttDef.Datatype;
				if (datatype.Variety == XmlSchemaDatatypeVariety.Union && obj != null)
				{
					XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
					memberType = xsdSimpleValue.XmlType;
					datatype = xsdSimpleValue.XmlType.Datatype;
					obj = xsdSimpleValue.TypedValue;
				}
				this.CheckTokenizedTypes(datatype, obj, true);
				if (this.HasIdentityConstraints)
				{
					this.AttributeIdentityConstraints(xmlQualifiedName.Name, xmlQualifiedName.Namespace, obj, obj2.ToString(), datatype);
				}
			}
			else
			{
				lName = this.nameTable.Add(lName);
				if (Ref.Equal(lName, this.xsiTypeString) || Ref.Equal(lName, this.xsiNilString) || Ref.Equal(lName, this.xsiSchemaLocationString) || Ref.Equal(lName, this.xsiNoNamespaceSchemaLocationString))
				{
					this.attPresence.Add(xmlQualifiedName, SchemaAttDef.Empty);
				}
				else
				{
					this.attrValid = false;
					this.SendValidationEvent("Sch_NotXsiAttribute", xmlQualifiedName.ToString());
				}
			}
			IL_3FF:
			if (!this.attrValid)
			{
				validity = XmlSchemaValidity.Invalid;
			}
			else if (schemaAttDef != null)
			{
				validity = XmlSchemaValidity.Valid;
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaAttribute = xmlSchemaAttribute;
				schemaInfo.SchemaType = ((xmlSchemaAttribute == null) ? null : xmlSchemaAttribute.AttributeSchemaType);
				schemaInfo.MemberType = memberType;
				schemaInfo.IsDefault = false;
				schemaInfo.Validity = validity;
			}
			if (this.ProcessSchemaHints && this.validatedNamespaces[ns] == null)
			{
				this.validatedNamespaces.Add(ns, ns);
			}
			return obj;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000D30DA File Offset: 0x000D12DA
		public void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributes)
		{
			if (defaultAttributes == null)
			{
				throw new ArgumentNullException("defaultAttributes");
			}
			this.CheckStateTransition(ValidatorState.Attribute, "GetUnspecifiedDefaultAttributes");
			this.GetUnspecifiedDefaultAttributes(defaultAttributes, false);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000D3100 File Offset: 0x000D1300
		public void ValidateEndOfAttributes(XmlSchemaInfo schemaInfo)
		{
			this.CheckStateTransition(ValidatorState.EndOfAttributes, XmlSchemaValidator.MethodNames[6]);
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDecl != null && elementDecl.HasRequiredAttribute)
			{
				this.context.CheckRequiredAttribute = false;
				this.CheckRequiredAttributes(elementDecl);
			}
			if (schemaInfo != null)
			{
				schemaInfo.Validity = this.context.Validity;
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000D3159 File Offset: 0x000D1359
		public void ValidateText(string elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateText(elementValue, null);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000D3171 File Offset: 0x000D1371
		public void ValidateText(XmlValueGetter elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateText(null, elementValue);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000D318C File Offset: 0x000D138C
		private void ValidateText(string elementStringValue, XmlValueGetter elementValueGetter)
		{
			ValidatorState validatorState = (this.validationStack.Length > 1) ? ValidatorState.Text : ValidatorState.TopLevelTextOrWS;
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Sch_ContentInNill", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				switch (this.context.ElementDecl.ContentValidator.ContentType)
				{
				case XmlSchemaContentType.TextOnly:
					if (elementValueGetter != null)
					{
						this.SaveTextValue(elementValueGetter());
						return;
					}
					this.SaveTextValue(elementStringValue);
					return;
				case XmlSchemaContentType.Empty:
					this.SendValidationEvent("Sch_InvalidTextInEmpty", string.Empty);
					return;
				case XmlSchemaContentType.ElementOnly:
				{
					string str = (elementValueGetter != null) ? elementValueGetter().ToString() : elementStringValue;
					if (!this.xmlCharType.IsOnlyWhitespace(str))
					{
						ArrayList arrayList = this.context.ElementDecl.ContentValidator.ExpectedParticles(this.context, false, this.schemaSet);
						if (arrayList == null || arrayList.Count == 0)
						{
							this.SendValidationEvent("Sch_InvalidTextInElement", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
							return;
						}
						this.SendValidationEvent("Sch_InvalidTextInElementExpecting", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, true)
						});
						return;
					}
					break;
				}
				case XmlSchemaContentType.Mixed:
					if (this.context.ElementDecl.DefaultValueTyped != null)
					{
						if (elementValueGetter != null)
						{
							this.SaveTextValue(elementValueGetter());
							return;
						}
						this.SaveTextValue(elementStringValue);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000D332F File Offset: 0x000D152F
		public void ValidateWhitespace(string elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateWhitespace(elementValue, null);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000D3347 File Offset: 0x000D1547
		public void ValidateWhitespace(XmlValueGetter elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateWhitespace(null, elementValue);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000D3360 File Offset: 0x000D1560
		private void ValidateWhitespace(string elementStringValue, XmlValueGetter elementValueGetter)
		{
			ValidatorState validatorState = (this.validationStack.Length > 1) ? ValidatorState.Whitespace : ValidatorState.TopLevelTextOrWS;
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Sch_ContentInNill", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				switch (this.context.ElementDecl.ContentValidator.ContentType)
				{
				case XmlSchemaContentType.TextOnly:
					if (elementValueGetter != null)
					{
						this.SaveTextValue(elementValueGetter());
						return;
					}
					this.SaveTextValue(elementStringValue);
					return;
				case XmlSchemaContentType.Empty:
					this.SendValidationEvent("Sch_InvalidWhitespaceInEmpty", string.Empty);
					return;
				case XmlSchemaContentType.ElementOnly:
					break;
				case XmlSchemaContentType.Mixed:
					if (this.context.ElementDecl.DefaultValueTyped != null)
					{
						if (elementValueGetter != null)
						{
							this.SaveTextValue(elementValueGetter());
							return;
						}
						this.SaveTextValue(elementStringValue);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000D3450 File Offset: 0x000D1650
		public object ValidateEndElement(XmlSchemaInfo schemaInfo)
		{
			return this.InternalValidateEndElement(schemaInfo, null);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000D345A File Offset: 0x000D165A
		public object ValidateEndElement(XmlSchemaInfo schemaInfo, object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			if (this.textValue.Length > 0)
			{
				throw new InvalidOperationException(Res.GetString("Sch_InvalidEndElementCall"));
			}
			return this.InternalValidateEndElement(schemaInfo, typedValue);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000D3490 File Offset: 0x000D1690
		public void SkipToEndElement(XmlSchemaInfo schemaInfo)
		{
			if (this.validationStack.Length <= 1)
			{
				throw new InvalidOperationException(Res.GetString("Sch_InvalidEndElementMultiple", new object[]
				{
					XmlSchemaValidator.MethodNames[10]
				}));
			}
			this.CheckStateTransition(ValidatorState.SkipToEndElement, XmlSchemaValidator.MethodNames[10]);
			if (schemaInfo != null)
			{
				SchemaElementDecl elementDecl = this.context.ElementDecl;
				if (elementDecl != null)
				{
					schemaInfo.SchemaType = elementDecl.SchemaType;
					schemaInfo.SchemaElement = this.GetSchemaElement();
				}
				else
				{
					schemaInfo.SchemaType = null;
					schemaInfo.SchemaElement = null;
				}
				schemaInfo.MemberType = null;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.IsDefault = this.context.IsDefault;
				schemaInfo.Validity = this.context.Validity;
			}
			this.context.ValidationSkipped = true;
			this.currentState = ValidatorState.SkipToEndElement;
			this.Pop();
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000D356A File Offset: 0x000D176A
		public void EndValidation()
		{
			if (this.validationStack.Length > 1)
			{
				throw new InvalidOperationException(Res.GetString("Sch_InvalidEndValidation"));
			}
			this.CheckStateTransition(ValidatorState.Finish, XmlSchemaValidator.MethodNames[11]);
			this.CheckForwardRefs();
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000D35A0 File Offset: 0x000D17A0
		public XmlSchemaParticle[] GetExpectedParticles()
		{
			if (this.currentState != ValidatorState.Start && this.currentState != ValidatorState.TopLevelTextOrWS)
			{
				if (this.context.ElementDecl != null)
				{
					ArrayList arrayList = this.context.ElementDecl.ContentValidator.ExpectedParticles(this.context, false, this.schemaSet);
					if (arrayList != null)
					{
						return arrayList.ToArray(typeof(XmlSchemaParticle)) as XmlSchemaParticle[];
					}
				}
				return XmlSchemaValidator.EmptyParticleArray;
			}
			if (this.partialValidationType == null)
			{
				ICollection values = this.schemaSet.GlobalElements.Values;
				ArrayList arrayList2 = new ArrayList(values.Count);
				foreach (object obj in values)
				{
					XmlSchemaElement p = (XmlSchemaElement)obj;
					ContentValidator.AddParticleToExpected(p, this.schemaSet, arrayList2, true);
				}
				return arrayList2.ToArray(typeof(XmlSchemaParticle)) as XmlSchemaParticle[];
			}
			XmlSchemaElement xmlSchemaElement = this.partialValidationType as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return new XmlSchemaParticle[]
				{
					xmlSchemaElement
				};
			}
			return XmlSchemaValidator.EmptyParticleArray;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000D36C4 File Offset: 0x000D18C4
		public XmlSchemaAttribute[] GetExpectedAttributes()
		{
			if (this.currentState == ValidatorState.Element || this.currentState == ValidatorState.Attribute)
			{
				SchemaElementDecl elementDecl = this.context.ElementDecl;
				ArrayList arrayList = new ArrayList();
				if (elementDecl != null)
				{
					foreach (SchemaAttDef schemaAttDef in elementDecl.AttDefs.Values)
					{
						if (this.attPresence[schemaAttDef.Name] == null)
						{
							arrayList.Add(schemaAttDef.SchemaAttribute);
						}
					}
				}
				if (this.nsResolver.LookupPrefix(this.NsXsi) != null)
				{
					this.AddXsiAttributes(arrayList);
				}
				return arrayList.ToArray(typeof(XmlSchemaAttribute)) as XmlSchemaAttribute[];
			}
			if (this.currentState == ValidatorState.Start && this.partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = this.partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					return new XmlSchemaAttribute[]
					{
						xmlSchemaAttribute
					};
				}
			}
			return XmlSchemaValidator.EmptyAttributeArray;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000D37C4 File Offset: 0x000D19C4
		internal void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributes, bool createNodeData)
		{
			this.currentState = ValidatorState.Attribute;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDecl != null && elementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < elementDecl.DefaultAttDefs.Count; i++)
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)elementDecl.DefaultAttDefs[i];
					if (!this.attPresence.Contains(schemaAttDef.Name) && schemaAttDef.DefaultValueTyped != null)
					{
						string text = this.nameTable.Add(schemaAttDef.Name.Namespace);
						string text2 = string.Empty;
						if (text.Length > 0)
						{
							text2 = this.GetDefaultAttributePrefix(text);
							if (text2 == null || text2.Length == 0)
							{
								this.SendValidationEvent("Sch_DefaultAttributeNotApplied", new string[]
								{
									schemaAttDef.Name.ToString(),
									XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
								});
								goto IL_242;
							}
						}
						XmlSchemaDatatype datatype = schemaAttDef.Datatype;
						if (createNodeData)
						{
							ValidatingReaderNodeData validatingReaderNodeData = new ValidatingReaderNodeData();
							validatingReaderNodeData.LocalName = this.nameTable.Add(schemaAttDef.Name.Name);
							validatingReaderNodeData.Namespace = text;
							validatingReaderNodeData.Prefix = this.nameTable.Add(text2);
							validatingReaderNodeData.NodeType = XmlNodeType.Attribute;
							AttributePSVIInfo attributePSVIInfo = new AttributePSVIInfo();
							XmlSchemaInfo attributeSchemaInfo = attributePSVIInfo.attributeSchemaInfo;
							if (schemaAttDef.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
							{
								XsdSimpleValue xsdSimpleValue = schemaAttDef.DefaultValueTyped as XsdSimpleValue;
								attributeSchemaInfo.MemberType = xsdSimpleValue.XmlType;
								datatype = xsdSimpleValue.XmlType.Datatype;
								attributePSVIInfo.typedAttributeValue = xsdSimpleValue.TypedValue;
							}
							else
							{
								attributePSVIInfo.typedAttributeValue = schemaAttDef.DefaultValueTyped;
							}
							attributeSchemaInfo.IsDefault = true;
							attributeSchemaInfo.Validity = XmlSchemaValidity.Valid;
							attributeSchemaInfo.SchemaType = schemaAttDef.SchemaType;
							attributeSchemaInfo.SchemaAttribute = schemaAttDef.SchemaAttribute;
							validatingReaderNodeData.RawValue = attributeSchemaInfo.XmlType.ValueConverter.ToString(attributePSVIInfo.typedAttributeValue);
							validatingReaderNodeData.AttInfo = attributePSVIInfo;
							defaultAttributes.Add(validatingReaderNodeData);
						}
						else
						{
							defaultAttributes.Add(schemaAttDef.SchemaAttribute);
						}
						this.CheckTokenizedTypes(datatype, schemaAttDef.DefaultValueTyped, true);
						if (this.HasIdentityConstraints)
						{
							this.AttributeIdentityConstraints(schemaAttDef.Name.Name, schemaAttDef.Name.Namespace, schemaAttDef.DefaultValueTyped, schemaAttDef.DefaultValueRaw, datatype);
						}
					}
					IL_242:;
				}
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000D3A28 File Offset: 0x000D1C28
		internal XmlSchemaSet SchemaSet
		{
			get
			{
				return this.schemaSet;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000D3A30 File Offset: 0x000D1C30
		internal XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000D3A38 File Offset: 0x000D1C38
		internal XmlSchemaContentType CurrentContentType
		{
			get
			{
				if (this.context.ElementDecl == null)
				{
					return XmlSchemaContentType.Empty;
				}
				return this.context.ElementDecl.ContentValidator.ContentType;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x000D3A5E File Offset: 0x000D1C5E
		internal XmlSchemaContentProcessing CurrentProcessContents
		{
			get
			{
				return this.processContents;
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000D3A66 File Offset: 0x000D1C66
		internal void SetDtdSchemaInfo(IDtdInfo dtdSchemaInfo)
		{
			this.dtdSchemaInfo = dtdSchemaInfo;
			this.checkEntity = true;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x000D3A76 File Offset: 0x000D1C76
		private bool StrictlyAssessed
		{
			get
			{
				return (this.processContents == XmlSchemaContentProcessing.Strict || this.processContents == XmlSchemaContentProcessing.Lax) && this.context.ElementDecl != null && !this.context.ValidationSkipped;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000D3AA7 File Offset: 0x000D1CA7
		private bool HasSchema
		{
			get
			{
				if (this.isRoot)
				{
					this.isRoot = false;
					if (!this.compiledSchemaInfo.Contains(this.context.Namespace))
					{
						this.rootHasSchema = false;
					}
				}
				return this.rootHasSchema;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000D3ADD File Offset: 0x000D1CDD
		internal string GetConcatenatedValue()
		{
			return this.textValue.ToString();
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000D3AEC File Offset: 0x000D1CEC
		private object InternalValidateEndElement(XmlSchemaInfo schemaInfo, object typedValue)
		{
			if (this.validationStack.Length <= 1)
			{
				throw new InvalidOperationException(Res.GetString("Sch_InvalidEndElementMultiple", new object[]
				{
					XmlSchemaValidator.MethodNames[9]
				}));
			}
			this.CheckStateTransition(ValidatorState.EndElement, XmlSchemaValidator.MethodNames[9]);
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			XmlSchemaType schemaType = null;
			XmlSchemaElement schemaElement = null;
			string text = string.Empty;
			if (elementDecl != null)
			{
				if (this.context.CheckRequiredAttribute && elementDecl.HasRequiredAttribute)
				{
					this.CheckRequiredAttributes(elementDecl);
				}
				if (!this.context.IsNill && this.context.NeedValidateChildren)
				{
					switch (elementDecl.ContentValidator.ContentType)
					{
					case XmlSchemaContentType.TextOnly:
						if (typedValue == null)
						{
							text = this.textValue.ToString();
							typedValue = this.ValidateAtomicValue(text, out xmlSchemaSimpleType);
						}
						else
						{
							typedValue = this.ValidateAtomicValue(typedValue, out xmlSchemaSimpleType);
						}
						break;
					case XmlSchemaContentType.ElementOnly:
						if (typedValue != null)
						{
							throw new InvalidOperationException(Res.GetString("Sch_InvalidEndElementCallTyped"));
						}
						break;
					case XmlSchemaContentType.Mixed:
						if (elementDecl.DefaultValueTyped != null && typedValue == null)
						{
							text = this.textValue.ToString();
							typedValue = this.CheckMixedValueConstraint(text);
						}
						break;
					}
					if (!elementDecl.ContentValidator.CompleteValidation(this.context))
					{
						XmlSchemaValidator.CompleteValidationError(this.context, this.eventHandler, this.nsResolver, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition, this.schemaSet);
						this.context.Validity = XmlSchemaValidity.Invalid;
					}
				}
				if (this.HasIdentityConstraints)
				{
					XmlSchemaType xmlSchemaType = (xmlSchemaSimpleType == null) ? elementDecl.SchemaType : xmlSchemaSimpleType;
					this.EndElementIdentityConstraints(typedValue, text, xmlSchemaType.Datatype);
				}
				schemaType = elementDecl.SchemaType;
				schemaElement = this.GetSchemaElement();
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaType = schemaType;
				schemaInfo.SchemaElement = schemaElement;
				schemaInfo.MemberType = xmlSchemaSimpleType;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.IsDefault = this.context.IsDefault;
				if (this.context.Validity == XmlSchemaValidity.NotKnown && this.StrictlyAssessed)
				{
					this.context.Validity = XmlSchemaValidity.Valid;
				}
				schemaInfo.Validity = this.context.Validity;
			}
			this.Pop();
			return typedValue;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000D3D1C File Offset: 0x000D1F1C
		private void ProcessSchemaLocations(string xsiSchemaLocation, string xsiNoNamespaceSchemaLocation)
		{
			bool flag = false;
			if (xsiNoNamespaceSchemaLocation != null)
			{
				flag = true;
				this.LoadSchema(string.Empty, xsiNoNamespaceSchemaLocation);
			}
			if (xsiSchemaLocation != null)
			{
				object obj;
				Exception ex = XmlSchemaValidator.dtStringArray.TryParseValue(xsiSchemaLocation, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					this.SendValidationEvent("Sch_InvalidValueDetailedAttribute", new string[]
					{
						"schemaLocation",
						xsiSchemaLocation,
						XmlSchemaValidator.dtStringArray.TypeCodeString,
						ex.Message
					}, ex);
					return;
				}
				string[] array = (string[])obj;
				flag = true;
				try
				{
					for (int i = 0; i < array.Length - 1; i += 2)
					{
						this.LoadSchema(array[i], array[i + 1]);
					}
				}
				catch (XmlSchemaException e)
				{
					this.SendValidationEvent(e);
				}
			}
			if (flag)
			{
				this.RecompileSchemaSet();
			}
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000D3DE8 File Offset: 0x000D1FE8
		private object ValidateElementContext(XmlQualifiedName elementName, out bool invalidElementInContext)
		{
			object obj = null;
			int num = 0;
			invalidElementInContext = false;
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Sch_ContentInNill", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return null;
				}
				ContentValidator contentValidator = this.context.ElementDecl.ContentValidator;
				if (contentValidator.ContentType == XmlSchemaContentType.Mixed && this.context.ElementDecl.Presence == SchemaDeclBase.Use.Fixed)
				{
					this.SendValidationEvent("Sch_ElementInMixedWithFixed", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return null;
				}
				XmlQualifiedName xmlQualifiedName = elementName;
				bool flag = false;
				for (;;)
				{
					obj = this.context.ElementDecl.ContentValidator.ValidateElement(xmlQualifiedName, this.context, out num);
					if (obj != null)
					{
						goto IL_115;
					}
					if (num == -2)
					{
						break;
					}
					flag = true;
					XmlSchemaElement substitutionGroupHead = this.GetSubstitutionGroupHead(xmlQualifiedName);
					if (substitutionGroupHead == null)
					{
						goto IL_115;
					}
					xmlQualifiedName = substitutionGroupHead.QualifiedName;
				}
				this.SendValidationEvent("Sch_AllElement", elementName.ToString());
				invalidElementInContext = true;
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				return null;
				IL_115:
				if (flag)
				{
					XmlSchemaElement xmlSchemaElement = obj as XmlSchemaElement;
					if (xmlSchemaElement == null)
					{
						obj = null;
					}
					else if (xmlSchemaElement.RefName.IsEmpty)
					{
						this.SendValidationEvent("Sch_InvalidElementSubstitution", XmlSchemaValidator.BuildElementName(elementName), XmlSchemaValidator.BuildElementName(xmlSchemaElement.QualifiedName));
						invalidElementInContext = true;
						this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
					}
					else
					{
						obj = this.compiledSchemaInfo.GetElement(elementName);
						this.context.NeedValidateChildren = true;
					}
				}
				if (obj == null)
				{
					XmlSchemaValidator.ElementValidationError(elementName, this.context, this.eventHandler, this.nsResolver, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition, this.schemaSet);
					invalidElementInContext = true;
					this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				}
			}
			return obj;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000D3FD8 File Offset: 0x000D21D8
		private XmlSchemaElement GetSubstitutionGroupHead(XmlQualifiedName member)
		{
			XmlSchemaElement element = this.compiledSchemaInfo.GetElement(member);
			if (element != null)
			{
				XmlQualifiedName substitutionGroup = element.SubstitutionGroup;
				if (!substitutionGroup.IsEmpty)
				{
					XmlSchemaElement element2 = this.compiledSchemaInfo.GetElement(substitutionGroup);
					if (element2 != null)
					{
						if ((element2.BlockResolved & XmlSchemaDerivationMethod.Substitution) != XmlSchemaDerivationMethod.Empty)
						{
							this.SendValidationEvent("Sch_SubstitutionNotAllowed", new string[]
							{
								member.ToString(),
								substitutionGroup.ToString()
							});
							return null;
						}
						if (!XmlSchemaType.IsDerivedFrom(element.ElementSchemaType, element2.ElementSchemaType, element2.BlockResolved))
						{
							this.SendValidationEvent("Sch_SubstitutionBlocked", new string[]
							{
								member.ToString(),
								substitutionGroup.ToString()
							});
							return null;
						}
						return element2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000D4088 File Offset: 0x000D2288
		private object ValidateAtomicValue(string stringValue, out XmlSchemaSimpleType memberType)
		{
			object obj = null;
			memberType = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (!this.context.IsNill)
			{
				if (stringValue.Length == 0 && elementDecl.DefaultValueTyped != null)
				{
					SchemaElementDecl elementDeclBeforeXsi = this.context.ElementDeclBeforeXsi;
					if (elementDeclBeforeXsi != null && elementDeclBeforeXsi != elementDecl)
					{
						Exception ex = elementDecl.Datatype.TryParseValue(elementDecl.DefaultValueRaw, this.nameTable, this.nsResolver, out obj);
						if (ex != null)
						{
							this.SendValidationEvent("Sch_InvalidElementDefaultValue", new string[]
							{
								elementDecl.DefaultValueRaw,
								XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
							});
						}
						else
						{
							this.context.IsDefault = true;
						}
					}
					else
					{
						this.context.IsDefault = true;
						obj = elementDecl.DefaultValueTyped;
					}
				}
				else
				{
					obj = this.CheckElementValue(stringValue);
				}
				XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
				XmlSchemaDatatype datatype = elementDecl.Datatype;
				if (xsdSimpleValue != null)
				{
					memberType = xsdSimpleValue.XmlType;
					obj = xsdSimpleValue.TypedValue;
					datatype = memberType.Datatype;
				}
				this.CheckTokenizedTypes(datatype, obj, false);
			}
			return obj;
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000D41A0 File Offset: 0x000D23A0
		private object ValidateAtomicValue(object parsedValue, out XmlSchemaSimpleType memberType)
		{
			memberType = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			object obj = null;
			if (!this.context.IsNill)
			{
				SchemaDeclBase schemaDeclBase = elementDecl;
				XmlSchemaDatatype datatype = elementDecl.Datatype;
				Exception ex = datatype.TryParseValue(parsedValue, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					string text = parsedValue as string;
					if (text == null)
					{
						text = XmlSchemaDatatype.ConcatenatedToString(parsedValue);
					}
					this.SendValidationEvent("Sch_ElementValueDataTypeDetailed", new string[]
					{
						XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace),
						text,
						this.GetTypeName(schemaDeclBase),
						ex.Message
					}, ex);
					return null;
				}
				if (!schemaDeclBase.CheckValue(obj))
				{
					this.SendValidationEvent("Sch_FixedElementValue", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (datatype.Variety == XmlSchemaDatatypeVariety.Union)
				{
					XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
					memberType = xsdSimpleValue.XmlType;
					obj = xsdSimpleValue.TypedValue;
					datatype = memberType.Datatype;
				}
				this.CheckTokenizedTypes(datatype, obj, false);
			}
			return obj;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000D42B8 File Offset: 0x000D24B8
		private string GetTypeName(SchemaDeclBase decl)
		{
			string text = decl.SchemaType.QualifiedName.ToString();
			if (text.Length == 0)
			{
				text = decl.Datatype.TypeCodeString;
			}
			return text;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000D42EC File Offset: 0x000D24EC
		private void SaveTextValue(object value)
		{
			string value2 = value.ToString();
			this.textValue.Append(value2);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000D4310 File Offset: 0x000D2510
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
			this.context.IsDefault = false;
			this.context.CheckRequiredAttribute = true;
			this.context.ValidationSkipped = false;
			this.context.Validity = XmlSchemaValidity.NotKnown;
			this.context.NeedValidateChildren = false;
			this.context.ProcessContents = this.processContents;
			this.context.ElementDeclBeforeXsi = null;
			this.context.Constr = null;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000D43F8 File Offset: 0x000D25F8
		private void Pop()
		{
			ValidationState validationState = (ValidationState)this.validationStack.Pop();
			if (this.startIDConstraint == this.validationStack.Length)
			{
				this.startIDConstraint = -1;
			}
			this.context = (ValidationState)this.validationStack.Peek();
			if (validationState.Validity == XmlSchemaValidity.Invalid)
			{
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if (validationState.ValidationSkipped)
			{
				this.context.ValidationSkipped = true;
			}
			this.processContents = this.context.ProcessContents;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000D4480 File Offset: 0x000D2680
		private void AddXsiAttributes(ArrayList attList)
		{
			XmlSchemaValidator.BuildXsiAttributes();
			if (this.attPresence[XmlSchemaValidator.xsiTypeSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiTypeSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiNilSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiNilSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiSLSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiSLSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiNoNsSLSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiNoNsSLSO);
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000D4520 File Offset: 0x000D2720
		private SchemaElementDecl FastGetElementDecl(XmlQualifiedName elementName, object particle)
		{
			SchemaElementDecl schemaElementDecl = null;
			if (particle != null)
			{
				XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					schemaElementDecl = xmlSchemaElement.ElementDecl;
				}
				else
				{
					XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
					this.processContents = xmlSchemaAny.ProcessContentsCorrect;
				}
			}
			if (schemaElementDecl == null && this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (this.isRoot && this.partialValidationType != null)
				{
					if (this.partialValidationType is XmlSchemaElement)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.partialValidationType;
						if (elementName.Equals(xmlSchemaElement2.QualifiedName))
						{
							schemaElementDecl = xmlSchemaElement2.ElementDecl;
						}
						else
						{
							this.SendValidationEvent("Sch_SchemaElementNameMismatch", elementName.ToString(), xmlSchemaElement2.QualifiedName.ToString());
						}
					}
					else if (this.partialValidationType is XmlSchemaType)
					{
						XmlSchemaType xmlSchemaType = (XmlSchemaType)this.partialValidationType;
						schemaElementDecl = xmlSchemaType.ElementDecl;
					}
					else
					{
						this.SendValidationEvent("Sch_ValidateElementInvalidCall", string.Empty);
					}
				}
				else
				{
					schemaElementDecl = this.compiledSchemaInfo.GetElementDecl(elementName);
				}
			}
			return schemaElementDecl;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000D4614 File Offset: 0x000D2814
		private SchemaElementDecl CheckXsiTypeAndNil(SchemaElementDecl elementDecl, string xsiType, string xsiNil, ref bool declFound)
		{
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
			if (xsiType != null)
			{
				object obj = null;
				Exception ex = XmlSchemaValidator.dtQName.TryParseValue(xsiType, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					this.SendValidationEvent("Sch_InvalidValueDetailedAttribute", new string[]
					{
						"type",
						xsiType,
						XmlSchemaValidator.dtQName.TypeCodeString,
						ex.Message
					}, ex);
				}
				else
				{
					xmlQualifiedName = (obj as XmlQualifiedName);
				}
			}
			if (elementDecl != null)
			{
				if (elementDecl.IsNillable)
				{
					if (xsiNil != null)
					{
						this.context.IsNill = XmlConvert.ToBoolean(xsiNil);
						if (this.context.IsNill && elementDecl.Presence == SchemaDeclBase.Use.Fixed)
						{
							this.SendValidationEvent("Sch_XsiNilAndFixed");
						}
					}
				}
				else if (xsiNil != null)
				{
					this.SendValidationEvent("Sch_InvalidXsiNill");
				}
			}
			if (xmlQualifiedName.IsEmpty)
			{
				if (elementDecl != null && elementDecl.IsAbstract)
				{
					this.SendValidationEvent("Sch_AbstractElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					elementDecl = null;
				}
			}
			else
			{
				SchemaElementDecl schemaElementDecl = this.compiledSchemaInfo.GetTypeDecl(xmlQualifiedName);
				XmlSeverityType severity = XmlSeverityType.Warning;
				if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
				{
					severity = XmlSeverityType.Error;
				}
				if (schemaElementDecl == null && xmlQualifiedName.Namespace == this.NsXs)
				{
					XmlSchemaType xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromXsdType(xmlQualifiedName);
					if (xmlSchemaType == null)
					{
						xmlSchemaType = XmlSchemaType.GetBuiltInComplexType(xmlQualifiedName);
					}
					if (xmlSchemaType != null)
					{
						schemaElementDecl = xmlSchemaType.ElementDecl;
					}
				}
				if (schemaElementDecl == null)
				{
					this.SendValidationEvent("Sch_XsiTypeNotFound", xmlQualifiedName.ToString(), severity);
					elementDecl = null;
				}
				else
				{
					declFound = true;
					if (schemaElementDecl.IsAbstract)
					{
						this.SendValidationEvent("Sch_XsiTypeAbstract", xmlQualifiedName.ToString(), severity);
						elementDecl = null;
					}
					else if (elementDecl != null && !XmlSchemaType.IsDerivedFrom(schemaElementDecl.SchemaType, elementDecl.SchemaType, elementDecl.Block))
					{
						this.SendValidationEvent("Sch_XsiTypeBlockedEx", new string[]
						{
							xmlQualifiedName.ToString(),
							XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
						});
						elementDecl = null;
					}
					else
					{
						if (elementDecl != null)
						{
							schemaElementDecl = schemaElementDecl.Clone();
							schemaElementDecl.Constraints = elementDecl.Constraints;
							schemaElementDecl.DefaultValueRaw = elementDecl.DefaultValueRaw;
							schemaElementDecl.DefaultValueTyped = elementDecl.DefaultValueTyped;
							schemaElementDecl.Block = elementDecl.Block;
						}
						this.context.ElementDeclBeforeXsi = elementDecl;
						elementDecl = schemaElementDecl;
					}
				}
			}
			return elementDecl;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000D4860 File Offset: 0x000D2A60
		private void ThrowDeclNotFoundWarningOrError(bool declFound)
		{
			if (declFound)
			{
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				this.context.NeedValidateChildren = false;
				return;
			}
			if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
			{
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				this.context.NeedValidateChildren = false;
				this.SendValidationEvent("Sch_UndeclaredElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				return;
			}
			this.SendValidationEvent("Sch_NoElementSchemaFound", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace), XmlSeverityType.Warning);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000D4912 File Offset: 0x000D2B12
		private void CheckElementProperties()
		{
			if (this.context.ElementDecl.IsAbstract)
			{
				this.SendValidationEvent("Sch_AbstractElement", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000D494C File Offset: 0x000D2B4C
		private void ValidateStartElementIdentityConstraints()
		{
			if (this.ProcessIdentityConstraints && this.context.ElementDecl.Constraints != null)
			{
				this.AddIdentityConstraints();
			}
			if (this.HasIdentityConstraints)
			{
				this.ElementIdentityConstraints();
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000D497C File Offset: 0x000D2B7C
		private SchemaAttDef CheckIsXmlAttribute(XmlQualifiedName attQName)
		{
			SchemaAttDef result = null;
			if (Ref.Equal(attQName.Namespace, this.NsXml) && (this.validationFlags & XmlSchemaValidationFlags.AllowXmlAttributes) != XmlSchemaValidationFlags.None)
			{
				if (!this.compiledSchemaInfo.Contains(this.NsXml))
				{
					this.AddXmlNamespaceSchema();
				}
				this.compiledSchemaInfo.AttributeDecls.TryGetValue(attQName, out result);
			}
			return result;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000D49D8 File Offset: 0x000D2BD8
		private void AddXmlNamespaceSchema()
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.Add(Preprocessor.GetBuildInSchema());
			xmlSchemaSet.Compile();
			this.schemaSet.Add(xmlSchemaSet);
			this.RecompileSchemaSet();
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000D4A10 File Offset: 0x000D2C10
		internal object CheckMixedValueConstraint(string elementValue)
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (this.context.IsNill)
			{
				return null;
			}
			if (elementValue.Length == 0)
			{
				this.context.IsDefault = true;
				return elementDecl.DefaultValueTyped;
			}
			SchemaDeclBase schemaDeclBase = elementDecl;
			if (schemaDeclBase.Presence == SchemaDeclBase.Use.Fixed && !elementValue.Equals(elementDecl.DefaultValueRaw))
			{
				this.SendValidationEvent("Sch_FixedElementValue", elementDecl.Name.ToString());
			}
			return elementValue;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000D4A84 File Offset: 0x000D2C84
		private void LoadSchema(string uri, string url)
		{
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = this.xmlResolver.ResolveUri(this.sourceUri, url);
				Stream input = (Stream)this.xmlResolver.GetEntity(uri2, null, null);
				XmlReaderSettings readerSettings = this.schemaSet.ReaderSettings;
				readerSettings.CloseInput = true;
				readerSettings.XmlResolver = this.xmlResolver;
				xmlReader = XmlReader.Create(input, readerSettings, uri2.ToString());
				this.schemaSet.Add(uri, xmlReader, this.validatedNamespaces);
				while (xmlReader.Read())
				{
				}
			}
			catch (XmlSchemaException ex)
			{
				this.SendValidationEvent("Sch_CannotLoadSchema", new string[]
				{
					uri,
					ex.Message
				}, ex);
			}
			catch (Exception ex2)
			{
				this.SendValidationEvent("Sch_CannotLoadSchema", new string[]
				{
					uri,
					ex2.Message
				}, ex2, XmlSeverityType.Warning);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000D4B84 File Offset: 0x000D2D84
		internal void RecompileSchemaSet()
		{
			if (!this.schemaSet.IsCompiled)
			{
				try
				{
					this.schemaSet.Compile();
				}
				catch (XmlSchemaException e)
				{
					this.SendValidationEvent(e);
				}
			}
			this.compiledSchemaInfo = this.schemaSet.CompiledInfo;
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000D4BD8 File Offset: 0x000D2DD8
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name, bool attrValue)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.ProcessIdentityConstraints)
				{
					if (this.FindId(name) != null)
					{
						if (attrValue)
						{
							this.attrValid = false;
						}
						this.SendValidationEvent("Sch_DupId", name);
						return;
					}
					if (this.IDs == null)
					{
						this.IDs = new Hashtable();
					}
					this.IDs.Add(name, this.context.LocalName);
					return;
				}
				break;
			case XmlTokenizedType.IDREF:
				if (this.ProcessIdentityConstraints && this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				this.ProcessEntity(name);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000D4C98 File Offset: 0x000D2E98
		private object CheckAttributeValue(object value, SchemaAttDef attdef)
		{
			object obj = null;
			XmlSchemaDatatype datatype = attdef.Datatype;
			string text = value as string;
			Exception ex;
			if (text != null)
			{
				ex = datatype.TryParseValue(text, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					goto IL_78;
				}
			}
			else
			{
				ex = datatype.TryParseValue(value, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					goto IL_78;
				}
			}
			if (!attdef.CheckValue(obj))
			{
				this.attrValid = false;
				this.SendValidationEvent("Sch_FixedAttributeValue", attdef.Name.ToString());
			}
			return obj;
			IL_78:
			this.attrValid = false;
			if (text == null)
			{
				text = XmlSchemaDatatype.ConcatenatedToString(value);
			}
			this.SendValidationEvent("Sch_AttributeValueDataTypeDetailed", new string[]
			{
				attdef.Name.ToString(),
				text,
				this.GetTypeName(attdef),
				ex.Message
			}, ex);
			return null;
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000D4D68 File Offset: 0x000D2F68
		private object CheckElementValue(string stringValue)
		{
			object obj = null;
			SchemaDeclBase elementDecl = this.context.ElementDecl;
			XmlSchemaDatatype datatype = elementDecl.Datatype;
			Exception ex = datatype.TryParseValue(stringValue, this.nameTable, this.nsResolver, out obj);
			if (ex != null)
			{
				this.SendValidationEvent("Sch_ElementValueDataTypeDetailed", new string[]
				{
					XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace),
					stringValue,
					this.GetTypeName(elementDecl),
					ex.Message
				}, ex);
				return null;
			}
			if (!elementDecl.CheckValue(obj))
			{
				this.SendValidationEvent("Sch_FixedElementValue", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return obj;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000D4E1C File Offset: 0x000D301C
		private void CheckTokenizedTypes(XmlSchemaDatatype dtype, object typedValue, bool attrValue)
		{
			if (typedValue == null)
			{
				return;
			}
			XmlTokenizedType tokenizedType = dtype.TokenizedType;
			if (tokenizedType == XmlTokenizedType.ENTITY || tokenizedType == XmlTokenizedType.ID || tokenizedType == XmlTokenizedType.IDREF)
			{
				if (dtype.Variety == XmlSchemaDatatypeVariety.List)
				{
					string[] array = (string[])typedValue;
					for (int i = 0; i < array.Length; i++)
					{
						this.ProcessTokenizedType(dtype.TokenizedType, array[i], attrValue);
					}
					return;
				}
				this.ProcessTokenizedType(dtype.TokenizedType, (string)typedValue, attrValue);
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000D4E82 File Offset: 0x000D3082
		private object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000D4E9C File Offset: 0x000D309C
		private void CheckForwardRefs()
		{
			IdRefNode next;
			for (IdRefNode idRefNode = this.idRefListHead; idRefNode != null; idRefNode = next)
			{
				if (this.FindId(idRefNode.Id) == null)
				{
					this.SendValidationEvent(new XmlSchemaValidationException("Sch_UndeclaredId", idRefNode.Id, this.sourceUriString, idRefNode.LineNo, idRefNode.LinePos), XmlSeverityType.Error);
				}
				next = idRefNode.Next;
				idRefNode.Next = null;
			}
			this.idRefListHead = null;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x000D4F03 File Offset: 0x000D3103
		private bool HasIdentityConstraints
		{
			get
			{
				return this.ProcessIdentityConstraints && this.startIDConstraint != -1;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x000D4F1B File Offset: 0x000D311B
		internal bool ProcessIdentityConstraints
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x000D4F28 File Offset: 0x000D3128
		internal bool ReportValidationWarnings
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ReportValidationWarnings) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000D4F35 File Offset: 0x000D3135
		internal bool ProcessInlineSchema
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x000D4F42 File Offset: 0x000D3142
		internal bool ProcessSchemaLocation
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000D4F4F File Offset: 0x000D314F
		internal bool ProcessSchemaHints
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None || (this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000D4F68 File Offset: 0x000D3168
		private void CheckStateTransition(ValidatorState toState, string methodName)
		{
			if (XmlSchemaValidator.ValidStates[(int)this.currentState, (int)toState])
			{
				this.currentState = toState;
				return;
			}
			object[] args;
			if (this.currentState == ValidatorState.None)
			{
				string name = "Sch_InvalidStartTransition";
				args = new string[]
				{
					methodName,
					XmlSchemaValidator.MethodNames[1]
				};
				throw new InvalidOperationException(Res.GetString(name, args));
			}
			string name2 = "Sch_InvalidStateTransition";
			args = new string[]
			{
				XmlSchemaValidator.MethodNames[(int)this.currentState],
				methodName
			};
			throw new InvalidOperationException(Res.GetString(name2, args));
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000D4FE8 File Offset: 0x000D31E8
		private void ClearPSVI()
		{
			if (this.textValue != null)
			{
				this.textValue.Length = 0;
			}
			this.attPresence.Clear();
			this.wildID = null;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000D5010 File Offset: 0x000D3210
		private void CheckRequiredAttributes(SchemaElementDecl currentElementDecl)
		{
			Dictionary<XmlQualifiedName, SchemaAttDef> attDefs = currentElementDecl.AttDefs;
			foreach (SchemaAttDef schemaAttDef in attDefs.Values)
			{
				if (this.attPresence[schemaAttDef.Name] == null && (schemaAttDef.Presence == SchemaDeclBase.Use.Required || schemaAttDef.Presence == SchemaDeclBase.Use.RequiredFixed))
				{
					this.SendValidationEvent("Sch_MissRequiredAttribute", schemaAttDef.Name.ToString());
				}
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000D50A0 File Offset: 0x000D32A0
		private XmlSchemaElement GetSchemaElement()
		{
			SchemaElementDecl elementDeclBeforeXsi = this.context.ElementDeclBeforeXsi;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDeclBeforeXsi != null && elementDeclBeforeXsi.SchemaElement != null)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)elementDeclBeforeXsi.SchemaElement.Clone(null);
				xmlSchemaElement.SchemaTypeName = XmlQualifiedName.Empty;
				xmlSchemaElement.SchemaType = elementDecl.SchemaType;
				xmlSchemaElement.SetElementType(elementDecl.SchemaType);
				xmlSchemaElement.ElementDecl = elementDecl;
				return xmlSchemaElement;
			}
			return elementDecl.SchemaElement;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000D5114 File Offset: 0x000D3314
		internal string GetDefaultAttributePrefix(string attributeNS)
		{
			IDictionary<string, string> namespacesInScope = this.nsResolver.GetNamespacesInScope(XmlNamespaceScope.All);
			string text = null;
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				string strA = this.nameTable.Add(keyValuePair.Value);
				if (Ref.Equal(strA, attributeNS))
				{
					text = keyValuePair.Key;
					if (text.Length != 0)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000D519C File Offset: 0x000D339C
		private void AddIdentityConstraints()
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			this.context.Constr = new ConstraintStruct[elementDecl.Constraints.Length];
			int num = 0;
			for (int i = 0; i < elementDecl.Constraints.Length; i++)
			{
				this.context.Constr[num++] = new ConstraintStruct(elementDecl.Constraints[i]);
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
						this.SendValidationEvent("Sch_RefNotInScope", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
				}
			}
			if (this.startIDConstraint == -1)
			{
				this.startIDConstraint = this.validationStack.Length - 1;
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000D5378 File Offset: 0x000D3578
		private void ElementIdentityConstraints()
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			string localName = this.context.LocalName;
			string @namespace = this.context.Namespace;
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						if (constr[j].axisSelector.MoveToStartElement(localName, @namespace))
						{
							constr[j].axisSelector.PushKS(this.positionInfo.LineNumber, this.positionInfo.LinePosition);
						}
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToStartElement(localName, @namespace) && elementDecl != null)
							{
								if (elementDecl.Datatype == null || elementDecl.ContentValidator.ContentType == XmlSchemaContentType.Mixed)
								{
									this.SendValidationEvent("Sch_FieldSimpleTypeExpected", localName);
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

		// Token: 0x0600288F RID: 10383 RVA: 0x000D54C0 File Offset: 0x000D36C0
		private void AttributeIdentityConstraints(string name, string ns, object obj, string sobj, XmlSchemaDatatype datatype)
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
									this.SendValidationEvent("Sch_FieldSingleValueExpected", name);
								}
								else
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(obj, sobj, datatype);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000D55AC File Offset: 0x000D37AC
		private void EndElementIdentityConstraints(object typedValue, string stringValue, XmlSchemaDatatype datatype)
		{
			string localName = this.context.LocalName;
			string @namespace = this.context.Namespace;
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
									this.SendValidationEvent("Sch_FieldSingleValueExpected", localName);
								}
								else if (LocalAppContextSwitches.IgnoreEmptyKeySequences)
								{
									if (typedValue != null && stringValue.Length != 0)
									{
										locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(typedValue, stringValue, datatype);
									}
								}
								else if (typedValue != null)
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(typedValue, stringValue, datatype);
								}
							}
							locatedActiveAxis.EndElement(localName, @namespace);
						}
						if (constr[j].axisSelector.EndElement(localName, @namespace))
						{
							KeySequence keySequence = constr[j].axisSelector.PopKS();
							switch (constr[j].constraint.Role)
							{
							case CompiledIdentityConstraint.ConstraintRole.Unique:
								if (keySequence.IsQualified())
								{
									if (constr[j].qualifiedTable.Contains(keySequence))
									{
										this.SendValidationEvent(new XmlSchemaValidationException("Sch_DuplicateKey", new string[]
										{
											keySequence.ToString(),
											constr[j].constraint.name.ToString()
										}, this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
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
									this.SendValidationEvent(new XmlSchemaValidationException("Sch_MissingKey", constr[j].constraint.name.ToString(), this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
								}
								else if (constr[j].qualifiedTable.Contains(keySequence))
								{
									this.SendValidationEvent(new XmlSchemaValidationException("Sch_DuplicateKey", new string[]
									{
										keySequence.ToString(),
										constr[j].constraint.name.ToString()
									}, this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
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
								this.SendValidationEvent(new XmlSchemaValidationException("Sch_UnresolvedKeyref", new string[]
								{
									keySequence2.ToString(),
									constr2[l].constraint.name.ToString()
								}, this.sourceUriString, keySequence2.PosLine, keySequence2.PosCol));
							}
						}
					}
				}
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000D59E8 File Offset: 0x000D3BE8
		private static void BuildXsiAttributes()
		{
			if (XmlSchemaValidator.xsiTypeSO == null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "type";
				xmlSchemaAttribute.SetQualifiedName(new XmlQualifiedName("type", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute.SetAttributeType(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.QName));
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiTypeSO, xmlSchemaAttribute, null);
			}
			if (XmlSchemaValidator.xsiNilSO == null)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "nil";
				xmlSchemaAttribute2.SetQualifiedName(new XmlQualifiedName("nil", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute2.SetAttributeType(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean));
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiNilSO, xmlSchemaAttribute2, null);
			}
			if (XmlSchemaValidator.xsiSLSO == null)
			{
				XmlSchemaSimpleType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
				XmlSchemaAttribute xmlSchemaAttribute3 = new XmlSchemaAttribute();
				xmlSchemaAttribute3.Name = "schemaLocation";
				xmlSchemaAttribute3.SetQualifiedName(new XmlQualifiedName("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute3.SetAttributeType(builtInSimpleType);
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiSLSO, xmlSchemaAttribute3, null);
			}
			if (XmlSchemaValidator.xsiNoNsSLSO == null)
			{
				XmlSchemaSimpleType builtInSimpleType2 = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
				XmlSchemaAttribute xmlSchemaAttribute4 = new XmlSchemaAttribute();
				xmlSchemaAttribute4.Name = "noNamespaceSchemaLocation";
				xmlSchemaAttribute4.SetQualifiedName(new XmlQualifiedName("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute4.SetAttributeType(builtInSimpleType2);
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiNoNsSLSO, xmlSchemaAttribute4, null);
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000D5B1C File Offset: 0x000D3D1C
		internal static void ElementValidationError(XmlQualifiedName name, ValidationState context, ValidationEventHandler eventHandler, object sender, string sourceUri, int lineNo, int linePos, XmlSchemaSet schemaSet)
		{
			if (context.ElementDecl != null)
			{
				ContentValidator contentValidator = context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly || (contentType == XmlSchemaContentType.Mixed && contentValidator != ContentValidator.Mixed && contentValidator != ContentValidator.Any))
				{
					bool flag = schemaSet != null;
					ArrayList arrayList;
					if (flag)
					{
						arrayList = contentValidator.ExpectedParticles(context, false, schemaSet);
					}
					else
					{
						arrayList = contentValidator.ExpectedElements(context, false);
					}
					if (arrayList == null || arrayList.Count == 0)
					{
						if (context.TooComplex)
						{
							XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_InvalidElementContentComplex", new string[]
							{
								XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
								XmlSchemaValidator.BuildElementName(name),
								Res.GetString("Sch_ComplexContentModel")
							}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
							return;
						}
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_InvalidElementContent", new string[]
						{
							XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
							XmlSchemaValidator.BuildElementName(name)
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
					else
					{
						if (context.TooComplex)
						{
							XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_InvalidElementContentExpectingComplex", new string[]
							{
								XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
								XmlSchemaValidator.BuildElementName(name),
								XmlSchemaValidator.PrintExpectedElements(arrayList, flag),
								Res.GetString("Sch_ComplexContentModel")
							}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
							return;
						}
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_InvalidElementContentExpecting", new string[]
						{
							XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
							XmlSchemaValidator.BuildElementName(name),
							XmlSchemaValidator.PrintExpectedElements(arrayList, flag)
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
				}
				else
				{
					if (contentType == XmlSchemaContentType.Empty)
					{
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_InvalidElementInEmptyEx", new string[]
						{
							XmlSchemaValidator.QNameString(context.LocalName, context.Namespace),
							name.ToString()
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
					if (!contentValidator.IsOpen)
					{
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_InvalidElementInTextOnlyEx", new string[]
						{
							XmlSchemaValidator.QNameString(context.LocalName, context.Namespace),
							name.ToString()
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
					}
				}
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000D5D48 File Offset: 0x000D3F48
		internal static void CompleteValidationError(ValidationState context, ValidationEventHandler eventHandler, object sender, string sourceUri, int lineNo, int linePos, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = null;
			bool flag = schemaSet != null;
			if (context.ElementDecl != null)
			{
				if (flag)
				{
					arrayList = context.ElementDecl.ContentValidator.ExpectedParticles(context, true, schemaSet);
				}
				else
				{
					arrayList = context.ElementDecl.ContentValidator.ExpectedElements(context, true);
				}
			}
			if (arrayList == null || arrayList.Count == 0)
			{
				if (context.TooComplex)
				{
					XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_IncompleteContentComplex", new string[]
					{
						XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
						Res.GetString("Sch_ComplexContentModel")
					}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
				}
				XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_IncompleteContent", XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace), sourceUri, lineNo, linePos), XmlSeverityType.Error);
				return;
			}
			if (context.TooComplex)
			{
				XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_IncompleteContentExpectingComplex", new string[]
				{
					XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
					XmlSchemaValidator.PrintExpectedElements(arrayList, flag),
					Res.GetString("Sch_ComplexContentModel")
				}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
				return;
			}
			XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("Sch_IncompleteContentExpecting", new string[]
			{
				XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
				XmlSchemaValidator.PrintExpectedElements(arrayList, flag)
			}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000D5E98 File Offset: 0x000D4098
		internal static string PrintExpectedElements(ArrayList expected, bool getParticles)
		{
			if (getParticles)
			{
				string name = "Sch_ContinuationString";
				object[] args = new string[]
				{
					" "
				};
				string @string = Res.GetString(name, args);
				XmlSchemaParticle xmlSchemaParticle = null;
				ArrayList arrayList = new ArrayList();
				StringBuilder stringBuilder = new StringBuilder();
				if (expected.Count == 1)
				{
					xmlSchemaParticle = (expected[0] as XmlSchemaParticle);
				}
				else
				{
					for (int i = 1; i < expected.Count; i++)
					{
						XmlSchemaParticle xmlSchemaParticle2 = expected[i - 1] as XmlSchemaParticle;
						xmlSchemaParticle = (expected[i] as XmlSchemaParticle);
						XmlQualifiedName qualifiedName = xmlSchemaParticle2.GetQualifiedName();
						if (qualifiedName.Namespace != xmlSchemaParticle.GetQualifiedName().Namespace)
						{
							arrayList.Add(qualifiedName);
							XmlSchemaValidator.PrintNamesWithNS(arrayList, stringBuilder);
							arrayList.Clear();
							stringBuilder.Append(@string);
						}
						else
						{
							arrayList.Add(qualifiedName);
						}
					}
				}
				arrayList.Add(xmlSchemaParticle.GetQualifiedName());
				XmlSchemaValidator.PrintNamesWithNS(arrayList, stringBuilder);
				return stringBuilder.ToString();
			}
			return XmlSchemaValidator.PrintNames(expected);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000D5F98 File Offset: 0x000D4198
		private static string PrintNames(ArrayList expected)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("'");
			stringBuilder.Append(expected[0].ToString());
			for (int i = 1; i < expected.Count; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(expected[i].ToString());
			}
			stringBuilder.Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000D600C File Offset: 0x000D420C
		private static void PrintNamesWithNS(ArrayList expected, StringBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = expected[0] as XmlQualifiedName;
			if (expected.Count == 1)
			{
				if (xmlQualifiedName.Name == "*")
				{
					XmlSchemaValidator.EnumerateAny(builder, xmlQualifiedName.Namespace);
					return;
				}
				if (xmlQualifiedName.Namespace.Length != 0)
				{
					builder.Append(Res.GetString("Sch_ElementNameAndNamespace", new object[]
					{
						xmlQualifiedName.Name,
						xmlQualifiedName.Namespace
					}));
					return;
				}
				builder.Append(Res.GetString("Sch_ElementName", new object[]
				{
					xmlQualifiedName.Name
				}));
				return;
			}
			else
			{
				bool flag = false;
				bool flag2 = true;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < expected.Count; i++)
				{
					xmlQualifiedName = (expected[i] as XmlQualifiedName);
					if (xmlQualifiedName.Name == "*")
					{
						flag = true;
					}
					else
					{
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(xmlQualifiedName.Name);
					}
				}
				if (flag)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(Res.GetString("Sch_AnyElement"));
					return;
				}
				if (xmlQualifiedName.Namespace.Length != 0)
				{
					builder.Append(Res.GetString("Sch_ElementNameAndNamespace", new object[]
					{
						stringBuilder.ToString(),
						xmlQualifiedName.Namespace
					}));
					return;
				}
				builder.Append(Res.GetString("Sch_ElementName", new object[]
				{
					stringBuilder.ToString()
				}));
				return;
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000D6184 File Offset: 0x000D4384
		private static void EnumerateAny(StringBuilder builder, string namespaces)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (namespaces == "##any" || namespaces == "##other")
			{
				stringBuilder.Append(namespaces);
			}
			else
			{
				string[] array = XmlConvert.SplitString(namespaces);
				stringBuilder.Append(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(array[i]);
				}
			}
			builder.Append(Res.GetString("Sch_AnyElementNS", new object[]
			{
				stringBuilder.ToString()
			}));
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000D6210 File Offset: 0x000D4410
		internal static string QNameString(string localName, string ns)
		{
			if (ns.Length == 0)
			{
				return localName;
			}
			return ns + ":" + localName;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000D6228 File Offset: 0x000D4428
		internal static string BuildElementName(XmlQualifiedName qname)
		{
			return XmlSchemaValidator.BuildElementName(qname.Name, qname.Namespace);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000D623B File Offset: 0x000D443B
		internal static string BuildElementName(string localName, string ns)
		{
			if (ns.Length != 0)
			{
				return Res.GetString("Sch_ElementNameAndNamespace", new object[]
				{
					localName,
					ns
				});
			}
			return Res.GetString("Sch_ElementName", new object[]
			{
				localName
			});
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000D6274 File Offset: 0x000D4474
		private void ProcessEntity(string name)
		{
			if (!this.checkEntity)
			{
				return;
			}
			IDtdEntityInfo dtdEntityInfo = null;
			if (this.dtdSchemaInfo != null)
			{
				dtdEntityInfo = this.dtdSchemaInfo.LookupEntity(name);
			}
			if (dtdEntityInfo == null)
			{
				this.SendValidationEvent("Sch_UndeclaredEntity", name);
				return;
			}
			if (dtdEntityInfo.IsUnparsedEntity)
			{
				this.SendValidationEvent("Sch_UnparsedEntityRef", name);
			}
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000D62C5 File Offset: 0x000D44C5
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000D62D3 File Offset: 0x000D44D3
		private void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, args, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000D62FE File Offset: 0x000D44FE
		private void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, arg, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000D6329 File Offset: 0x000D4529
		private void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, new string[]
			{
				arg1,
				arg2
			}, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000D6361 File Offset: 0x000D4561
		private void SendValidationEvent(string code, string[] args, Exception innerException, XmlSeverityType severity)
		{
			if (severity != XmlSeverityType.Warning || this.ReportValidationWarnings)
			{
				this.SendValidationEvent(new XmlSchemaValidationException(code, args, innerException, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
			}
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000D639C File Offset: 0x000D459C
		private void SendValidationEvent(string code, string[] args, Exception innerException)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, args, innerException, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000D63C9 File Offset: 0x000D45C9
		private void SendValidationEvent(XmlSchemaValidationException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000D63D3 File Offset: 0x000D45D3
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(e.GetRes, e.Args, e.SourceUri, e.LineNumber, e.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000D63FF File Offset: 0x000D45FF
		private void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			if (severity != XmlSeverityType.Warning || this.ReportValidationWarnings)
			{
				this.SendValidationEvent(new XmlSchemaValidationException(code, msg, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
			}
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000D6438 File Offset: 0x000D4638
		private void SendValidationEvent(XmlSchemaValidationException e, XmlSeverityType severity)
		{
			bool flag = false;
			if (severity == XmlSeverityType.Error)
			{
				flag = true;
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if (!flag)
			{
				if (this.ReportValidationWarnings && this.eventHandler != null)
				{
					this.eventHandler(this.validationEventSender, new ValidationEventArgs(e, severity));
				}
				return;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(this.validationEventSender, new ValidationEventArgs(e, severity));
				return;
			}
			throw e;
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000D64A6 File Offset: 0x000D46A6
		internal static void SendValidationEvent(ValidationEventHandler eventHandler, object sender, XmlSchemaValidationException e, XmlSeverityType severity)
		{
			if (eventHandler != null)
			{
				eventHandler(sender, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x04001184 RID: 4484
		private XmlSchemaSet schemaSet;

		// Token: 0x04001185 RID: 4485
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x04001186 RID: 4486
		private int startIDConstraint = -1;

		// Token: 0x04001187 RID: 4487
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001188 RID: 4488
		private bool isRoot;

		// Token: 0x04001189 RID: 4489
		private bool rootHasSchema;

		// Token: 0x0400118A RID: 4490
		private bool attrValid;

		// Token: 0x0400118B RID: 4491
		private bool checkEntity;

		// Token: 0x0400118C RID: 4492
		private SchemaInfo compiledSchemaInfo;

		// Token: 0x0400118D RID: 4493
		private IDtdInfo dtdSchemaInfo;

		// Token: 0x0400118E RID: 4494
		private Hashtable validatedNamespaces;

		// Token: 0x0400118F RID: 4495
		private HWStack validationStack;

		// Token: 0x04001190 RID: 4496
		private ValidationState context;

		// Token: 0x04001191 RID: 4497
		private ValidatorState currentState;

		// Token: 0x04001192 RID: 4498
		private Hashtable attPresence;

		// Token: 0x04001193 RID: 4499
		private SchemaAttDef wildID;

		// Token: 0x04001194 RID: 4500
		private Hashtable IDs;

		// Token: 0x04001195 RID: 4501
		private IdRefNode idRefListHead;

		// Token: 0x04001196 RID: 4502
		private XmlQualifiedName contextQName;

		// Token: 0x04001197 RID: 4503
		private string NsXs;

		// Token: 0x04001198 RID: 4504
		private string NsXsi;

		// Token: 0x04001199 RID: 4505
		private string NsXmlNs;

		// Token: 0x0400119A RID: 4506
		private string NsXml;

		// Token: 0x0400119B RID: 4507
		private XmlSchemaObject partialValidationType;

		// Token: 0x0400119C RID: 4508
		private StringBuilder textValue;

		// Token: 0x0400119D RID: 4509
		private ValidationEventHandler eventHandler;

		// Token: 0x0400119E RID: 4510
		private object validationEventSender;

		// Token: 0x0400119F RID: 4511
		private XmlNameTable nameTable;

		// Token: 0x040011A0 RID: 4512
		private IXmlLineInfo positionInfo;

		// Token: 0x040011A1 RID: 4513
		private IXmlLineInfo dummyPositionInfo;

		// Token: 0x040011A2 RID: 4514
		private XmlResolver xmlResolver;

		// Token: 0x040011A3 RID: 4515
		private Uri sourceUri;

		// Token: 0x040011A4 RID: 4516
		private string sourceUriString;

		// Token: 0x040011A5 RID: 4517
		private IXmlNamespaceResolver nsResolver;

		// Token: 0x040011A6 RID: 4518
		private XmlSchemaContentProcessing processContents = XmlSchemaContentProcessing.Strict;

		// Token: 0x040011A7 RID: 4519
		private static XmlSchemaAttribute xsiTypeSO;

		// Token: 0x040011A8 RID: 4520
		private static XmlSchemaAttribute xsiNilSO;

		// Token: 0x040011A9 RID: 4521
		private static XmlSchemaAttribute xsiSLSO;

		// Token: 0x040011AA RID: 4522
		private static XmlSchemaAttribute xsiNoNsSLSO;

		// Token: 0x040011AB RID: 4523
		private string xsiTypeString;

		// Token: 0x040011AC RID: 4524
		private string xsiNilString;

		// Token: 0x040011AD RID: 4525
		private string xsiSchemaLocationString;

		// Token: 0x040011AE RID: 4526
		private string xsiNoNamespaceSchemaLocationString;

		// Token: 0x040011AF RID: 4527
		private static readonly XmlSchemaDatatype dtQName = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName);

		// Token: 0x040011B0 RID: 4528
		private static readonly XmlSchemaDatatype dtCDATA = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);

		// Token: 0x040011B1 RID: 4529
		private static readonly XmlSchemaDatatype dtStringArray = XmlSchemaValidator.dtCDATA.DeriveByList(null);

		// Token: 0x040011B2 RID: 4530
		private const string Quote = "'";

		// Token: 0x040011B3 RID: 4531
		private static XmlSchemaParticle[] EmptyParticleArray = new XmlSchemaParticle[0];

		// Token: 0x040011B4 RID: 4532
		private static XmlSchemaAttribute[] EmptyAttributeArray = new XmlSchemaAttribute[0];

		// Token: 0x040011B5 RID: 4533
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040011B6 RID: 4534
		internal static bool[,] ValidStates = new bool[,]
		{
			{
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				true,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				false
			},
			{
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				true
			},
			{
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				true,
				true,
				true,
				true,
				true
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			}
		};

		// Token: 0x040011B7 RID: 4535
		private static string[] MethodNames = new string[]
		{
			"None",
			"Initialize",
			"top-level ValidateAttribute",
			"top-level ValidateText or ValidateWhitespace",
			"ValidateElement",
			"ValidateAttribute",
			"ValidateEndOfAttributes",
			"ValidateText",
			"ValidateWhitespace",
			"ValidateEndElement",
			"SkipToEndElement",
			"EndValidation"
		};
	}
}
