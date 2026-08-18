using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000308 RID: 776
	[DebuggerDisplay("Namespace={Namespace}, PublicKeyToken={PublicKeyToken}, Version={Version}")]
	internal class Schema : SchemaElement
	{
		// Token: 0x06002DF6 RID: 11766 RVA: 0x000ADE35 File Offset: 0x000AC035
		public Schema(SchemaManager schemaManager) : base(null)
		{
			this._schemaManager = schemaManager;
			this._errors = new List<EdmSchemaError>();
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000ADE5B File Offset: 0x000AC05B
		internal IList<EdmSchemaError> Resolve()
		{
			this.ResolveTopLevelNames();
			if (this._errors.Count != 0)
			{
				return this.ResetErrors();
			}
			this.ResolveSecondLevelNames();
			return this.ResetErrors();
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000ADE83 File Offset: 0x000AC083
		internal IList<EdmSchemaError> ValidateSchema()
		{
			this.Validate();
			return this.ResetErrors();
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000ADE91 File Offset: 0x000AC091
		internal void AddError(EdmSchemaError error)
		{
			this._errors.Add(error);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000ADEA0 File Offset: 0x000AC0A0
		internal IList<EdmSchemaError> Parse(XmlReader sourceReader, string sourceLocation)
		{
			try
			{
				XmlReaderSettings settings = this.CreateXmlReaderSettings();
				XmlReader sourceReader2 = XmlReader.Create(sourceReader, settings);
				return this.InternalParse(sourceReader2, sourceLocation);
			}
			catch (IOException message)
			{
				base.AddError(ErrorCode.IOException, EdmSchemaErrorSeverity.Error, sourceReader, message);
			}
			return this.ResetErrors();
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000ADEF0 File Offset: 0x000AC0F0
		private IList<EdmSchemaError> InternalParse(XmlReader sourceReader, string sourceLocation)
		{
			base.Schema = this;
			this.Location = sourceLocation;
			try
			{
				if (sourceReader.NodeType != XmlNodeType.Element)
				{
					while (sourceReader.Read() && sourceReader.NodeType != XmlNodeType.Element)
					{
					}
				}
				base.GetPositionInfo(sourceReader);
				List<string> primarySchemaNamespaces = Schema.SomSchemaSetHelper.GetPrimarySchemaNamespaces(this.DataModel);
				if (sourceReader.EOF)
				{
					if (sourceLocation != null)
					{
						base.AddError(ErrorCode.EmptyFile, EdmSchemaErrorSeverity.Error, Strings.EmptyFile(sourceLocation));
					}
					else
					{
						base.AddError(ErrorCode.EmptyFile, EdmSchemaErrorSeverity.Error, Strings.EmptySchemaTextReader);
					}
				}
				else if (!primarySchemaNamespaces.Contains(sourceReader.NamespaceURI))
				{
					Func<object, object, object, string> func = new Func<object, object, object, string>(Strings.UnexpectedRootElement);
					if (string.IsNullOrEmpty(sourceReader.NamespaceURI))
					{
						func = new Func<object, object, object, string>(Strings.UnexpectedRootElementNoNamespace);
					}
					string commaDelimitedString = Helper.GetCommaDelimitedString(primarySchemaNamespaces);
					base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, func(sourceReader.NamespaceURI, sourceReader.LocalName, commaDelimitedString));
				}
				else
				{
					this.SchemaXmlNamespace = sourceReader.NamespaceURI;
					if (this.DataModel == SchemaDataModelOption.EntityDataModel)
					{
						if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2006/04/edm")
						{
							this.SchemaVersion = 1.0;
						}
						else if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2007/05/edm")
						{
							this.SchemaVersion = 1.1;
						}
						else if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2008/09/edm")
						{
							this.SchemaVersion = 2.0;
						}
						else
						{
							this.SchemaVersion = 3.0;
						}
					}
					else if (this.DataModel == SchemaDataModelOption.ProviderDataModel)
					{
						if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2006/04/edm/ssdl")
						{
							this.SchemaVersion = 1.0;
						}
						else if (this.SchemaXmlNamespace == "http://schemas.microsoft.com/ado/2009/02/edm/ssdl")
						{
							this.SchemaVersion = 2.0;
						}
						else
						{
							this.SchemaVersion = 3.0;
						}
					}
					string localName = sourceReader.LocalName;
					if (localName == "Schema" || localName == "ProviderManifest")
					{
						this.HandleTopLevelSchemaElement(sourceReader);
						sourceReader.Read();
					}
					else
					{
						base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, Strings.UnexpectedRootElement(sourceReader.NamespaceURI, sourceReader.LocalName, this.SchemaXmlNamespace));
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				base.AddError(ErrorCode.InternalError, EdmSchemaErrorSeverity.Error, ex.Message);
			}
			catch (UnauthorizedAccessException message)
			{
				base.AddError(ErrorCode.UnauthorizedAccessException, EdmSchemaErrorSeverity.Error, sourceReader, message);
			}
			catch (IOException message2)
			{
				base.AddError(ErrorCode.IOException, EdmSchemaErrorSeverity.Error, sourceReader, message2);
			}
			catch (SecurityException message3)
			{
				base.AddError(ErrorCode.SecurityError, EdmSchemaErrorSeverity.Error, sourceReader, message3);
			}
			catch (XmlException message4)
			{
				base.AddError(ErrorCode.XmlError, EdmSchemaErrorSeverity.Error, sourceReader, message4);
			}
			return this.ResetErrors();
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000AE1E8 File Offset: 0x000AC3E8
		internal static XmlReaderSettings CreateEdmStandardXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.CheckCharacters = true;
			xmlReaderSettings.CloseInput = false;
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.IgnoreProcessingInstructions = true;
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessIdentityConstraints;
			xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessSchemaLocation;
			xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessInlineSchema;
			return xmlReaderSettings;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000AE25C File Offset: 0x000AC45C
		private XmlReaderSettings CreateXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.ValidationEventHandler += this.OnSchemaValidationEvent;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			XmlSchemaSet schemaSet = Schema.SomSchemaSetHelper.GetSchemaSet(this.DataModel);
			xmlReaderSettings.Schemas = schemaSet;
			return xmlReaderSettings;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000AE2AC File Offset: 0x000AC4AC
		internal void OnSchemaValidationEvent(object sender, ValidationEventArgs e)
		{
			XmlReader xmlReader = sender as XmlReader;
			if (xmlReader != null && !this.IsValidateableXmlNamespace(xmlReader.NamespaceURI, xmlReader.NodeType == XmlNodeType.Attribute))
			{
				if (this.SchemaVersion == 1.0 || this.SchemaVersion == 1.1)
				{
					return;
				}
				if (xmlReader.NodeType == XmlNodeType.Attribute || e.Severity == XmlSeverityType.Warning)
				{
					return;
				}
			}
			if (this.SchemaVersion >= 2.0 && xmlReader.NodeType == XmlNodeType.Attribute && e.Severity == XmlSeverityType.Warning)
			{
				return;
			}
			EdmSchemaErrorSeverity severity = EdmSchemaErrorSeverity.Error;
			if (e.Severity == XmlSeverityType.Warning)
			{
				severity = EdmSchemaErrorSeverity.Warning;
			}
			base.AddError(ErrorCode.XmlError, severity, e.Exception.LineNumber, e.Exception.LinePosition, e.Message);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000AE368 File Offset: 0x000AC568
		public bool IsParseableXmlNamespace(string xmlNamespaceUri, bool isAttribute)
		{
			if (string.IsNullOrEmpty(xmlNamespaceUri) && isAttribute)
			{
				return true;
			}
			if (this._parseableXmlNamespaces == null)
			{
				this._parseableXmlNamespaces = new HashSet<string>();
				foreach (XmlSchemaResource xmlSchemaResource in XmlSchemaResource.GetMetadataSchemaResourceMap(this.SchemaVersion).Values)
				{
					this._parseableXmlNamespaces.Add(xmlSchemaResource.NamespaceUri);
				}
			}
			return this._parseableXmlNamespaces.Contains(xmlNamespaceUri);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000AE3FC File Offset: 0x000AC5FC
		public bool IsValidateableXmlNamespace(string xmlNamespaceUri, bool isAttribute)
		{
			if (string.IsNullOrEmpty(xmlNamespaceUri) && isAttribute)
			{
				return true;
			}
			if (this._validatableXmlNamespaces == null)
			{
				HashSet<string> hashSet = new HashSet<string>();
				double schemaVersion = (this.SchemaVersion == 0.0) ? 3.0 : this.SchemaVersion;
				foreach (XmlSchemaResource schemaResource in XmlSchemaResource.GetMetadataSchemaResourceMap(schemaVersion).Values)
				{
					Schema.AddAllSchemaResourceNamespaceNames(hashSet, schemaResource);
				}
				if (this.SchemaVersion == 0.0)
				{
					return hashSet.Contains(xmlNamespaceUri);
				}
				this._validatableXmlNamespaces = hashSet;
			}
			return this._validatableXmlNamespaces.Contains(xmlNamespaceUri);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000AE4C0 File Offset: 0x000AC6C0
		private static void AddAllSchemaResourceNamespaceNames(HashSet<string> hashSet, XmlSchemaResource schemaResource)
		{
			hashSet.Add(schemaResource.NamespaceUri);
			foreach (XmlSchemaResource schemaResource2 in schemaResource.ImportedSchemas)
			{
				Schema.AddAllSchemaResourceNamespaceNames(hashSet, schemaResource2);
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000AE500 File Offset: 0x000AC700
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.AliasResolver.ResolveNamespaces();
			foreach (SchemaElement schemaElement in this.SchemaTypes)
			{
				schemaElement.ResolveTopLevelNames();
			}
			foreach (Function function in this.Functions)
			{
				function.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000AE5A4 File Offset: 0x000AC7A4
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (SchemaElement schemaElement in this.SchemaTypes)
			{
				schemaElement.ResolveSecondLevelNames();
			}
			foreach (Function function in this.Functions)
			{
				function.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000AE640 File Offset: 0x000AC840
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.Namespace))
			{
				base.AddError(ErrorCode.MissingNamespaceAttribute, EdmSchemaErrorSeverity.Error, Strings.MissingNamespaceAttribute);
				return;
			}
			if (!string.IsNullOrEmpty(this.Alias) && EdmItemCollection.IsSystemNamespace(this.ProviderManifest, this.Alias))
			{
				base.AddError(ErrorCode.CannotUseSystemNamespaceAsAlias, EdmSchemaErrorSeverity.Error, Strings.CannotUseSystemNamespaceAsAlias(this.Alias));
			}
			if (this.ProviderManifest != null && EdmItemCollection.IsSystemNamespace(this.ProviderManifest, this.Namespace))
			{
				base.AddError(ErrorCode.SystemNamespace, EdmSchemaErrorSeverity.Error, Strings.SystemNamespaceEncountered(this.Namespace));
			}
			foreach (SchemaElement schemaElement in this.SchemaTypes)
			{
				schemaElement.Validate();
			}
			foreach (Function function in this.Functions)
			{
				this.AddFunctionType(function);
				function.Validate();
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000AE760 File Offset: 0x000AC960
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000AE768 File Offset: 0x000AC968
		internal string SchemaXmlNamespace
		{
			get
			{
				return this._schemaXmlNamespace;
			}
			private set
			{
				this._schemaXmlNamespace = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000AE771 File Offset: 0x000AC971
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._schemaManager.GetProviderManifest(delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
				{
					base.AddError(code, severity, message);
				});
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000AE78A File Offset: 0x000AC98A
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x000AE792 File Offset: 0x000AC992
		internal double SchemaVersion
		{
			get
			{
				return this._schemaVersion;
			}
			set
			{
				this._schemaVersion = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x000AE79B File Offset: 0x000AC99B
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x000AE7A3 File Offset: 0x000AC9A3
		internal virtual string Alias
		{
			get
			{
				return this._alias;
			}
			private set
			{
				this._alias = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000AE7AC File Offset: 0x000AC9AC
		// (set) Token: 0x06002E0D RID: 11789 RVA: 0x000AE7B4 File Offset: 0x000AC9B4
		internal virtual string Namespace
		{
			get
			{
				return this._namespaceName;
			}
			private set
			{
				this._namespaceName = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000AE7BD File Offset: 0x000AC9BD
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x000AE7C5 File Offset: 0x000AC9C5
		internal string Location
		{
			get
			{
				return this._location;
			}
			private set
			{
				this._location = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000AE7D0 File Offset: 0x000AC9D0
		internal MetadataProperty SchemaSource
		{
			get
			{
				if (this._schemaSourceProperty == null)
				{
					this._schemaSourceProperty = new MetadataProperty("SchemaSource", EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.String), false, (this._location != null) ? this._location : string.Empty);
				}
				return this._schemaSourceProperty;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000AE81D File Offset: 0x000ACA1D
		internal List<SchemaType> SchemaTypes
		{
			get
			{
				if (this._schemaTypes == null)
				{
					this._schemaTypes = new List<SchemaType>();
				}
				return this._schemaTypes;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000AE838 File Offset: 0x000ACA38
		public override string FQName
		{
			get
			{
				return this.Namespace;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000AE840 File Offset: 0x000ACA40
		private List<Function> Functions
		{
			get
			{
				if (this._functions == null)
				{
					this._functions = new List<Function>();
				}
				return this._functions;
			}
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000AE85C File Offset: 0x000ACA5C
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "EntityType"))
			{
				this.HandleEntityTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ComplexType"))
			{
				this.HandleInlineTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Association"))
			{
				this.HandleAssociationElement(reader);
				return true;
			}
			if (this.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "Using"))
				{
					this.HandleUsingElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Function"))
				{
					this.HandleModelFunctionElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "EnumType"))
				{
					this.HandleEnumTypeElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "ValueTerm"))
				{
					base.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Annotations"))
				{
					base.SkipElement(reader);
					return true;
				}
			}
			if (this.DataModel == SchemaDataModelOption.EntityDataModel || this.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				if (base.CanHandleElement(reader, "EntityContainer"))
				{
					this.HandleEntityContainerTypeElement(reader);
					return true;
				}
				if (this.DataModel == SchemaDataModelOption.ProviderDataModel && base.CanHandleElement(reader, "Function"))
				{
					this.HandleFunctionElement(reader);
					return true;
				}
			}
			else
			{
				if (base.CanHandleElement(reader, "Types"))
				{
					this.SkipThroughElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Functions"))
				{
					this.SkipThroughElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Function"))
				{
					this.HandleFunctionElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Type"))
				{
					this.HandleTypeInformationElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000A9C93 File Offset: 0x000A7E93
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000AE9DC File Offset: 0x000ACBDC
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (this._depth == 1)
			{
				return false;
			}
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Alias"))
			{
				this.HandleAliasAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Namespace"))
			{
				this.HandleNamespaceAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Provider"))
			{
				this.HandleProviderAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ProviderManifestToken"))
			{
				this.HandleProviderManifestTokenAttribute(reader);
				return true;
			}
			if (reader.NamespaceURI == "http://schemas.microsoft.com/ado/2009/02/edm/annotation" && reader.LocalName == "UseStrongSpatialTypes")
			{
				this.HandleUseStrongSpatialTypesAnnotation(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000AEA85 File Offset: 0x000ACC85
		protected override void HandleAttributesComplete()
		{
			if (this._depth < 2)
			{
				return;
			}
			if (this._depth == 2)
			{
				this._schemaManager.EnsurePrimitiveSchemaIsLoaded(this.SchemaVersion);
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000AEAB4 File Offset: 0x000ACCB4
		protected override void SkipThroughElement(XmlReader reader)
		{
			try
			{
				this._depth++;
				base.SkipThroughElement(reader);
			}
			finally
			{
				this._depth--;
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000AEAF8 File Offset: 0x000ACCF8
		internal bool ResolveTypeName(SchemaElement usingElement, string typeName, out SchemaType type)
		{
			type = null;
			string text;
			string text2;
			Utils.ExtractNamespaceAndName(this.DataModel, typeName, out text, out text2);
			string text3 = text;
			if (text3 == null)
			{
				text3 = ((this.ProviderManifest == null) ? this._namespaceName : this.ProviderManifest.NamespaceName);
			}
			string text4;
			if (text == null || !this.AliasResolver.TryResolveAlias(text3, out text4))
			{
				text4 = text3;
			}
			if (!this.SchemaManager.TryResolveType(text4, text2, out type))
			{
				if (text == null)
				{
					usingElement.AddError(ErrorCode.NotInNamespace, EdmSchemaErrorSeverity.Error, Strings.NotNamespaceQualified(typeName));
				}
				else if (!this.SchemaManager.IsValidNamespaceName(text4))
				{
					usingElement.AddError(ErrorCode.BadNamespace, EdmSchemaErrorSeverity.Error, Strings.BadNamespaceOrAlias(text));
				}
				else if (text4 != text3)
				{
					usingElement.AddError(ErrorCode.NotInNamespace, EdmSchemaErrorSeverity.Error, Strings.NotInNamespaceAlias(text2, text4, text3));
				}
				else
				{
					usingElement.AddError(ErrorCode.NotInNamespace, EdmSchemaErrorSeverity.Error, Strings.NotInNamespaceNoAlias(text2, text4));
				}
				return false;
			}
			if (this.DataModel != SchemaDataModelOption.EntityDataModel && type.Schema != this && type.Schema != this.SchemaManager.PrimitiveSchema)
			{
				usingElement.AddError(ErrorCode.InvalidNamespaceOrAliasSpecified, EdmSchemaErrorSeverity.Error, Strings.InvalidNamespaceOrAliasSpecified(text));
				return false;
			}
			return true;
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000AEBFA File Offset: 0x000ACDFA
		internal AliasResolver AliasResolver
		{
			get
			{
				if (this._aliasResolver == null)
				{
					this._aliasResolver = new AliasResolver(this);
				}
				return this._aliasResolver;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000AEC16 File Offset: 0x000ACE16
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this.SchemaManager.DataModel;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000AEC23 File Offset: 0x000ACE23
		internal SchemaManager SchemaManager
		{
			get
			{
				return this._schemaManager;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000AEC2C File Offset: 0x000ACE2C
		internal bool UseStrongSpatialTypes
		{
			get
			{
				return this._useStrongSpatialTypes ?? true;
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000AEC54 File Offset: 0x000ACE54
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.Namespace, null);
			if (!returnValue.Succeeded)
			{
				return;
			}
			this.Namespace = returnValue.Value;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000AEC85 File Offset: 0x000ACE85
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000AEC9C File Offset: 0x000ACE9C
		private void HandleProviderAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000AECE8 File Offset: 0x000ACEE8
		private void HandleProviderManifestTokenAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderManifestTokenNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000AED34 File Offset: 0x000ACF34
		private void HandleUseStrongSpatialTypesAnnotation(XmlReader reader)
		{
			bool value = false;
			if (base.HandleBoolAttribute(reader, ref value))
			{
				this._useStrongSpatialTypes = new bool?(value);
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000AED5C File Offset: 0x000ACF5C
		private void HandleUsingElement(XmlReader reader)
		{
			UsingElement usingElement = new UsingElement(this);
			usingElement.Parse(reader);
			this.AliasResolver.Add(usingElement);
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000AED84 File Offset: 0x000ACF84
		private void HandleEnumTypeElement(XmlReader reader)
		{
			SchemaEnumType schemaEnumType = new SchemaEnumType(this);
			schemaEnumType.Parse(reader);
			this.TryAddType(schemaEnumType, true);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000AEDA8 File Offset: 0x000ACFA8
		private void HandleTopLevelSchemaElement(XmlReader reader)
		{
			try
			{
				this._depth += 2;
				base.Parse(reader);
			}
			finally
			{
				this._depth -= 2;
			}
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000AEDEC File Offset: 0x000ACFEC
		private void HandleEntityTypeElement(XmlReader reader)
		{
			SchemaEntityType schemaEntityType = new SchemaEntityType(this);
			schemaEntityType.Parse(reader);
			this.TryAddType(schemaEntityType, true);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000AEE10 File Offset: 0x000AD010
		private void HandleTypeInformationElement(XmlReader reader)
		{
			TypeElement typeElement = new TypeElement(this);
			typeElement.Parse(reader);
			this.TryAddType(typeElement, true);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000AEE34 File Offset: 0x000AD034
		private void HandleFunctionElement(XmlReader reader)
		{
			Function function = new Function(this);
			function.Parse(reader);
			this.Functions.Add(function);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000AEE5C File Offset: 0x000AD05C
		private void HandleModelFunctionElement(XmlReader reader)
		{
			ModelFunction modelFunction = new ModelFunction(this);
			modelFunction.Parse(reader);
			this.Functions.Add(modelFunction);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000AEE84 File Offset: 0x000AD084
		private void HandleAssociationElement(XmlReader reader)
		{
			Relationship relationship = new Relationship(this, RelationshipKind.Association);
			relationship.Parse(reader);
			this.TryAddType(relationship, true);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000AEEA8 File Offset: 0x000AD0A8
		private void HandleInlineTypeElement(XmlReader reader)
		{
			SchemaComplexType schemaComplexType = new SchemaComplexType(this);
			schemaComplexType.Parse(reader);
			this.TryAddType(schemaComplexType, true);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000AEECC File Offset: 0x000AD0CC
		private void HandleEntityContainerTypeElement(XmlReader reader)
		{
			EntityContainer entityContainer = new EntityContainer(this);
			entityContainer.Parse(reader);
			this.TryAddContainer(entityContainer, true);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000AEEF0 File Offset: 0x000AD0F0
		private List<EdmSchemaError> ResetErrors()
		{
			List<EdmSchemaError> errors = this._errors;
			this._errors = new List<EdmSchemaError>();
			return errors;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000AEF10 File Offset: 0x000AD110
		protected void TryAddType(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			this.SchemaManager.SchemaTypes.Add(schemaType, doNotAddErrorForEmptyName, new Func<object, string>(Strings.TypeNameAlreadyDefinedDuplicate));
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000AEF3C File Offset: 0x000AD13C
		protected void TryAddContainer(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			this.SchemaManager.SchemaTypes.Add(schemaType, doNotAddErrorForEmptyName, new Func<object, string>(Strings.EntityContainerAlreadyExists));
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000AEF68 File Offset: 0x000AD168
		protected void AddFunctionType(Function function)
		{
			string p = (this.DataModel == SchemaDataModelOption.EntityDataModel) ? "Conceptual" : "Storage";
			if (this.SchemaVersion >= 2.0 && this.SchemaManager.SchemaTypes.ContainsKey(function.FQName))
			{
				function.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.AmbiguousFunctionAndType(function.FQName, p));
				return;
			}
			AddErrorKind addErrorKind = this.SchemaManager.SchemaTypes.TryAdd(function);
			if (addErrorKind != AddErrorKind.Succeeded)
			{
				function.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.AmbiguousFunctionOverload(function.FQName, p));
				return;
			}
			this.SchemaTypes.Add(function);
		}

		// Token: 0x040013FF RID: 5119
		private const int RootDepth = 2;

		// Token: 0x04001400 RID: 5120
		private List<EdmSchemaError> _errors = new List<EdmSchemaError>();

		// Token: 0x04001401 RID: 5121
		private List<Function> _functions;

		// Token: 0x04001402 RID: 5122
		private AliasResolver _aliasResolver;

		// Token: 0x04001403 RID: 5123
		private string _location;

		// Token: 0x04001404 RID: 5124
		private string _alias;

		// Token: 0x04001405 RID: 5125
		protected string _namespaceName;

		// Token: 0x04001406 RID: 5126
		private string _schemaXmlNamespace;

		// Token: 0x04001407 RID: 5127
		private List<SchemaType> _schemaTypes;

		// Token: 0x04001408 RID: 5128
		private int _depth;

		// Token: 0x04001409 RID: 5129
		private double _schemaVersion;

		// Token: 0x0400140A RID: 5130
		private SchemaManager _schemaManager;

		// Token: 0x0400140B RID: 5131
		private bool? _useStrongSpatialTypes;

		// Token: 0x0400140C RID: 5132
		private static IList<string> _emptyPathList = new List<string>(0).AsReadOnly();

		// Token: 0x0400140D RID: 5133
		private HashSet<string> _validatableXmlNamespaces;

		// Token: 0x0400140E RID: 5134
		private HashSet<string> _parseableXmlNamespaces;

		// Token: 0x0400140F RID: 5135
		private static readonly string[] ClientNamespaceOfSchemasMissingStoreSuffix = new string[]
		{
			"System.Storage.Sync.Utility",
			"System.Storage.Sync.Services"
		};

		// Token: 0x04001410 RID: 5136
		private MetadataProperty _schemaSourceProperty;

		// Token: 0x02000638 RID: 1592
		private static class SomSchemaSetHelper
		{
			// Token: 0x06004396 RID: 17302 RVA: 0x000F5DDC File Offset: 0x000F3FDC
			internal static List<string> GetPrimarySchemaNamespaces(SchemaDataModelOption dataModel)
			{
				List<string> list = new List<string>();
				if (dataModel == SchemaDataModelOption.EntityDataModel)
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm");
					list.Add("http://schemas.microsoft.com/ado/2007/05/edm");
					list.Add("http://schemas.microsoft.com/ado/2008/09/edm");
					list.Add("http://schemas.microsoft.com/ado/2009/11/edm");
				}
				else if (dataModel == SchemaDataModelOption.ProviderDataModel)
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm/ssdl");
					list.Add("http://schemas.microsoft.com/ado/2009/02/edm/ssdl");
					list.Add("http://schemas.microsoft.com/ado/2009/11/edm/ssdl");
				}
				else
				{
					list.Add("http://schemas.microsoft.com/ado/2006/04/edm/providermanifest");
				}
				return list;
			}

			// Token: 0x06004397 RID: 17303 RVA: 0x000F5E53 File Offset: 0x000F4053
			internal static XmlSchemaSet GetSchemaSet(SchemaDataModelOption dataModel)
			{
				return Schema.SomSchemaSetHelper._cachedSchemaSets.Evaluate(dataModel);
			}

			// Token: 0x06004398 RID: 17304 RVA: 0x000F5E60 File Offset: 0x000F4060
			private static XmlSchemaSet ComputeSchemaSet(SchemaDataModelOption dataModel)
			{
				List<string> primarySchemaNamespaces = Schema.SomSchemaSetHelper.GetPrimarySchemaNamespaces(dataModel);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				xmlSchemaSet.XmlResolver = null;
				Dictionary<string, XmlSchemaResource> metadataSchemaResourceMap = XmlSchemaResource.GetMetadataSchemaResourceMap(3.0);
				HashSet<string> schemasAlreadyAdded = new HashSet<string>();
				foreach (string key in primarySchemaNamespaces)
				{
					XmlSchemaResource schemaResource = metadataSchemaResourceMap[key];
					Schema.SomSchemaSetHelper.AddXmlSchemaToSet(xmlSchemaSet, schemaResource, schemasAlreadyAdded);
				}
				xmlSchemaSet.Compile();
				return xmlSchemaSet;
			}

			// Token: 0x06004399 RID: 17305 RVA: 0x000F5EEC File Offset: 0x000F40EC
			private static void AddXmlSchemaToSet(XmlSchemaSet schemaSet, XmlSchemaResource schemaResource, HashSet<string> schemasAlreadyAdded)
			{
				foreach (XmlSchemaResource schemaResource2 in schemaResource.ImportedSchemas)
				{
					Schema.SomSchemaSetHelper.AddXmlSchemaToSet(schemaSet, schemaResource2, schemasAlreadyAdded);
				}
				if (!schemasAlreadyAdded.Contains(schemaResource.NamespaceUri))
				{
					Stream resourceStream = Schema.SomSchemaSetHelper.GetResourceStream(schemaResource.ResourceName);
					XmlSchema schema = XmlSchema.Read(resourceStream, null);
					schemaSet.Add(schema);
					schemasAlreadyAdded.Add(schemaResource.NamespaceUri);
				}
			}

			// Token: 0x0600439A RID: 17306 RVA: 0x000F5F58 File Offset: 0x000F4158
			private static Stream GetResourceStream(string resourceName)
			{
				Stream result = null;
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				if (executingAssembly != null)
				{
					result = executingAssembly.GetManifestResourceStream(resourceName);
				}
				return result;
			}

			// Token: 0x04001EC2 RID: 7874
			private static Memoizer<SchemaDataModelOption, XmlSchemaSet> _cachedSchemaSets = new Memoizer<SchemaDataModelOption, XmlSchemaSet>(new Func<SchemaDataModelOption, XmlSchemaSet>(Schema.SomSchemaSetHelper.ComputeSchemaSet), EqualityComparer<SchemaDataModelOption>.Default);
		}
	}
}
