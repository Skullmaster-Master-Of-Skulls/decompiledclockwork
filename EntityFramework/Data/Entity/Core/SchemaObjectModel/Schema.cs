using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000377 RID: 887
	[DebuggerDisplay("Namespace={Namespace}, PublicKeyToken={PublicKeyToken}, Version={Version}")]
	internal class Schema : SchemaElement
	{
		// Token: 0x06001FCF RID: 8143 RVA: 0x00096CE0 File Offset: 0x00094EE0
		public Schema(SchemaManager schemaManager) : base(null, null)
		{
			this._schemaManager = schemaManager;
			this._errors = new List<EdmSchemaError>();
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00096D07 File Offset: 0x00094F07
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

		// Token: 0x06001FD1 RID: 8145 RVA: 0x00096D2F File Offset: 0x00094F2F
		internal IList<EdmSchemaError> ValidateSchema()
		{
			this.Validate();
			return this.ResetErrors();
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x00096D3D File Offset: 0x00094F3D
		internal void AddError(EdmSchemaError error)
		{
			this._errors.Add(error);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x00096D4C File Offset: 0x00094F4C
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
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

		// Token: 0x06001FD4 RID: 8148 RVA: 0x00096D98 File Offset: 0x00094F98
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
					string localName;
					if ((localName = sourceReader.LocalName) != null && (localName == "Schema" || localName == "ProviderManifest"))
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

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00097094 File Offset: 0x00095294
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

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00097108 File Offset: 0x00095308
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

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00097158 File Offset: 0x00095358
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

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00097214 File Offset: 0x00095414
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

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000972AC File Offset: 0x000954AC
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

		// Token: 0x06001FDA RID: 8154 RVA: 0x00097374 File Offset: 0x00095574
		private static void AddAllSchemaResourceNamespaceNames(HashSet<string> hashSet, XmlSchemaResource schemaResource)
		{
			hashSet.Add(schemaResource.NamespaceUri);
			foreach (XmlSchemaResource schemaResource2 in schemaResource.ImportedSchemas)
			{
				Schema.AddAllSchemaResourceNamespaceNames(hashSet, schemaResource2);
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000973BC File Offset: 0x000955BC
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

		// Token: 0x06001FDC RID: 8156 RVA: 0x00097460 File Offset: 0x00095660
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

		// Token: 0x06001FDD RID: 8157 RVA: 0x000974FC File Offset: 0x000956FC
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x0009761C File Offset: 0x0009581C
		// (set) Token: 0x06001FDF RID: 8159 RVA: 0x00097624 File Offset: 0x00095824
		internal string SchemaXmlNamespace { get; private set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x00097638 File Offset: 0x00095838
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00097651 File Offset: 0x00095851
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00097659 File Offset: 0x00095859
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00097662 File Offset: 0x00095862
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0009766A File Offset: 0x0009586A
		internal virtual string Alias { get; private set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00097673 File Offset: 0x00095873
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0009767B File Offset: 0x0009587B
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00097684 File Offset: 0x00095884
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0009768C File Offset: 0x0009588C
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00097698 File Offset: 0x00095898
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

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x000976E5 File Offset: 0x000958E5
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x00097700 File Offset: 0x00095900
		public override string FQName
		{
			get
			{
				return this.Namespace;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00097708 File Offset: 0x00095908
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

		// Token: 0x06001FED RID: 8173 RVA: 0x00097724 File Offset: 0x00095924
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
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "Annotations"))
				{
					this.SkipElement(reader);
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

		// Token: 0x06001FEE RID: 8174 RVA: 0x000978A1 File Offset: 0x00095AA1
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return base.ProhibitAttribute(namespaceUri, localName) || (namespaceUri == null && localName == "Name" && false);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000978C4 File Offset: 0x00095AC4
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

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0009796D File Offset: 0x00095B6D
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

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0009799C File Offset: 0x00095B9C
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

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000979E0 File Offset: 0x00095BE0
		internal bool ResolveTypeName(SchemaElement usingElement, string typeName, out SchemaType type)
		{
			type = null;
			string text;
			string text2;
			Utils.ExtractNamespaceAndName(typeName, out text, out text2);
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x00097ADC File Offset: 0x00095CDC
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00097AF8 File Offset: 0x00095CF8
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this.SchemaManager.DataModel;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00097B05 File Offset: 0x00095D05
		internal SchemaManager SchemaManager
		{
			get
			{
				return this._schemaManager;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00097B10 File Offset: 0x00095D10
		internal bool UseStrongSpatialTypes
		{
			get
			{
				return this._useStrongSpatialTypes ?? true;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00097B38 File Offset: 0x00095D38
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.Namespace);
			if (!returnValue.Succeeded)
			{
				return;
			}
			this.Namespace = returnValue.Value;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00097B68 File Offset: 0x00095D68
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00097B9C File Offset: 0x00095D9C
		private void HandleProviderAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00097C04 File Offset: 0x00095E04
		private void HandleProviderManifestTokenAttribute(XmlReader reader)
		{
			string value = reader.Value;
			this._schemaManager.ProviderManifestTokenNotification(value, delegate(string message, ErrorCode code, EdmSchemaErrorSeverity severity)
			{
				this.AddError(code, severity, reader, message);
			});
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00097C50 File Offset: 0x00095E50
		private void HandleUseStrongSpatialTypesAnnotation(XmlReader reader)
		{
			bool value = false;
			if (base.HandleBoolAttribute(reader, ref value))
			{
				this._useStrongSpatialTypes = new bool?(value);
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00097C78 File Offset: 0x00095E78
		private void HandleUsingElement(XmlReader reader)
		{
			UsingElement usingElement = new UsingElement(this);
			usingElement.Parse(reader);
			this.AliasResolver.Add(usingElement);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00097CA0 File Offset: 0x00095EA0
		private void HandleEnumTypeElement(XmlReader reader)
		{
			SchemaEnumType schemaEnumType = new SchemaEnumType(this);
			schemaEnumType.Parse(reader);
			this.TryAddType(schemaEnumType, true);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00097CC4 File Offset: 0x00095EC4
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

		// Token: 0x06001FFF RID: 8191 RVA: 0x00097D08 File Offset: 0x00095F08
		private void HandleEntityTypeElement(XmlReader reader)
		{
			SchemaEntityType schemaEntityType = new SchemaEntityType(this);
			schemaEntityType.Parse(reader);
			this.TryAddType(schemaEntityType, true);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00097D2C File Offset: 0x00095F2C
		private void HandleTypeInformationElement(XmlReader reader)
		{
			TypeElement typeElement = new TypeElement(this);
			typeElement.Parse(reader);
			this.TryAddType(typeElement, true);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00097D50 File Offset: 0x00095F50
		private void HandleFunctionElement(XmlReader reader)
		{
			Function function = new Function(this);
			function.Parse(reader);
			this.Functions.Add(function);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00097D78 File Offset: 0x00095F78
		private void HandleModelFunctionElement(XmlReader reader)
		{
			ModelFunction modelFunction = new ModelFunction(this);
			modelFunction.Parse(reader);
			this.Functions.Add(modelFunction);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00097DA0 File Offset: 0x00095FA0
		private void HandleAssociationElement(XmlReader reader)
		{
			Relationship relationship = new Relationship(this, RelationshipKind.Association);
			relationship.Parse(reader);
			this.TryAddType(relationship, true);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00097DC4 File Offset: 0x00095FC4
		private void HandleInlineTypeElement(XmlReader reader)
		{
			SchemaComplexType schemaComplexType = new SchemaComplexType(this);
			schemaComplexType.Parse(reader);
			this.TryAddType(schemaComplexType, true);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00097DE8 File Offset: 0x00095FE8
		private void HandleEntityContainerTypeElement(XmlReader reader)
		{
			EntityContainer entityContainer = new EntityContainer(this);
			entityContainer.Parse(reader);
			this.TryAddContainer(entityContainer, true);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00097E0C File Offset: 0x0009600C
		private List<EdmSchemaError> ResetErrors()
		{
			List<EdmSchemaError> errors = this._errors;
			this._errors = new List<EdmSchemaError>();
			return errors;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00097E2C File Offset: 0x0009602C
		protected void TryAddType(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			this.SchemaManager.SchemaTypes.Add(schemaType, doNotAddErrorForEmptyName, new Func<object, string>(Strings.TypeNameAlreadyDefinedDuplicate));
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00097E58 File Offset: 0x00096058
		protected void TryAddContainer(SchemaType schemaType, bool doNotAddErrorForEmptyName)
		{
			this.SchemaManager.SchemaTypes.Add(schemaType, doNotAddErrorForEmptyName, new Func<object, string>(Strings.EntityContainerAlreadyExists));
			this.SchemaTypes.Add(schemaType);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00097E84 File Offset: 0x00096084
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

		// Token: 0x04000B5D RID: 2909
		private const int RootDepth = 2;

		// Token: 0x04000B5E RID: 2910
		private List<EdmSchemaError> _errors = new List<EdmSchemaError>();

		// Token: 0x04000B5F RID: 2911
		private List<Function> _functions;

		// Token: 0x04000B60 RID: 2912
		private AliasResolver _aliasResolver;

		// Token: 0x04000B61 RID: 2913
		private string _location;

		// Token: 0x04000B62 RID: 2914
		protected string _namespaceName;

		// Token: 0x04000B63 RID: 2915
		private List<SchemaType> _schemaTypes;

		// Token: 0x04000B64 RID: 2916
		private int _depth;

		// Token: 0x04000B65 RID: 2917
		private double _schemaVersion;

		// Token: 0x04000B66 RID: 2918
		private readonly SchemaManager _schemaManager;

		// Token: 0x04000B67 RID: 2919
		private bool? _useStrongSpatialTypes;

		// Token: 0x04000B68 RID: 2920
		private HashSet<string> _validatableXmlNamespaces;

		// Token: 0x04000B69 RID: 2921
		private HashSet<string> _parseableXmlNamespaces;

		// Token: 0x04000B6A RID: 2922
		private MetadataProperty _schemaSourceProperty;

		// Token: 0x02000378 RID: 888
		private static class SomSchemaSetHelper
		{
			// Token: 0x0600200B RID: 8203 RVA: 0x00097F1C File Offset: 0x0009611C
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

			// Token: 0x0600200C RID: 8204 RVA: 0x00097F93 File Offset: 0x00096193
			internal static XmlSchemaSet GetSchemaSet(SchemaDataModelOption dataModel)
			{
				return Schema.SomSchemaSetHelper._cachedSchemaSets.Evaluate(dataModel);
			}

			// Token: 0x0600200D RID: 8205 RVA: 0x00097FA0 File Offset: 0x000961A0
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

			// Token: 0x0600200E RID: 8206 RVA: 0x0009802C File Offset: 0x0009622C
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

			// Token: 0x0600200F RID: 8207 RVA: 0x000980A4 File Offset: 0x000962A4
			private static Stream GetResourceStream(string resourceName)
			{
				return typeof(Schema).Assembly().GetManifestResourceStream(resourceName);
			}

			// Token: 0x04000B6D RID: 2925
			private static readonly Memoizer<SchemaDataModelOption, XmlSchemaSet> _cachedSchemaSets = new Memoizer<SchemaDataModelOption, XmlSchemaSet>(new Func<SchemaDataModelOption, XmlSchemaSet>(Schema.SomSchemaSetHelper.ComputeSchemaSet), EqualityComparer<SchemaDataModelOption>.Default);
		}
	}
}
