using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Xml.Schema
{
	// Token: 0x02000259 RID: 601
	internal sealed class SchemaCollectionPreprocessor : BaseProcessor
	{
		// Token: 0x060023D0 RID: 9168 RVA: 0x000C309B File Offset: 0x000C129B
		public SchemaCollectionPreprocessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler) : base(nameTable, schemaNames, eventHandler)
		{
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000C30A8 File Offset: 0x000C12A8
		public bool Execute(XmlSchema schema, string targetNamespace, bool loadExternals, XmlSchemaCollection xsc)
		{
			this.schema = schema;
			this.Xmlns = base.NameTable.Add("xmlns");
			this.Cleanup(schema);
			if (loadExternals && this.xmlResolver != null)
			{
				this.schemaLocations = new Hashtable();
				if (schema.BaseUri != null)
				{
					this.schemaLocations.Add(schema.BaseUri, schema.BaseUri);
				}
				this.LoadExternals(schema, xsc);
			}
			this.ValidateIdAttribute(schema);
			this.Preprocess(schema, targetNamespace, SchemaCollectionPreprocessor.Compositor.Root);
			if (!base.HasErrors)
			{
				schema.IsPreprocessed = true;
				for (int i = 0; i < schema.Includes.Count; i++)
				{
					XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
					if (xmlSchemaExternal.Schema != null)
					{
						xmlSchemaExternal.Schema.IsPreprocessed = true;
					}
				}
			}
			return !base.HasErrors;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000C3184 File Offset: 0x000C1384
		private void Cleanup(XmlSchema schema)
		{
			if (schema.IsProcessing)
			{
				return;
			}
			schema.IsProcessing = true;
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					this.Cleanup(xmlSchemaExternal.Schema);
				}
				if (xmlSchemaExternal is XmlSchemaRedefine)
				{
					XmlSchemaRedefine xmlSchemaRedefine = xmlSchemaExternal as XmlSchemaRedefine;
					xmlSchemaRedefine.AttributeGroups.Clear();
					xmlSchemaRedefine.Groups.Clear();
					xmlSchemaRedefine.SchemaTypes.Clear();
				}
			}
			schema.Attributes.Clear();
			schema.AttributeGroups.Clear();
			schema.SchemaTypes.Clear();
			schema.Elements.Clear();
			schema.Groups.Clear();
			schema.Notations.Clear();
			schema.Ids.Clear();
			schema.IdentityConstraints.Clear();
			schema.IsProcessing = false;
		}

		// Token: 0x170007C4 RID: 1988
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x000C326C File Offset: 0x000C146C
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000C3278 File Offset: 0x000C1478
		private void LoadExternals(XmlSchema schema, XmlSchemaCollection xsc)
		{
			if (schema.IsProcessing)
			{
				return;
			}
			schema.IsProcessing = true;
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					if (xmlSchemaExternal is XmlSchemaImport && ((XmlSchemaImport)xmlSchemaExternal).Namespace == "http://www.w3.org/XML/1998/namespace")
					{
						this.buildinIncluded = true;
					}
					else
					{
						Uri baseUri = xmlSchemaExternal.BaseUri;
						if (baseUri != null && this.schemaLocations[baseUri] == null)
						{
							this.schemaLocations.Add(baseUri, baseUri);
						}
						this.LoadExternals(xmlSchemaExternal.Schema, xsc);
					}
				}
				else
				{
					if (xsc != null && xmlSchemaExternal is XmlSchemaImport)
					{
						XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)xmlSchemaExternal;
						string ns = (xmlSchemaImport.Namespace != null) ? xmlSchemaImport.Namespace : string.Empty;
						xmlSchemaExternal.Schema = xsc[ns];
						if (xmlSchemaExternal.Schema != null)
						{
							xmlSchemaExternal.Schema = xmlSchemaExternal.Schema.Clone();
							if (xmlSchemaExternal.Schema.BaseUri != null && this.schemaLocations[xmlSchemaExternal.Schema.BaseUri] == null)
							{
								this.schemaLocations.Add(xmlSchemaExternal.Schema.BaseUri, xmlSchemaExternal.Schema.BaseUri);
							}
							for (int j = 0; j < xmlSchemaExternal.Schema.Includes.Count; j++)
							{
								XmlSchemaExternal xmlSchemaExternal2 = (XmlSchemaExternal)xmlSchemaExternal.Schema.Includes[j];
								if (xmlSchemaExternal2 is XmlSchemaImport)
								{
									XmlSchemaImport xmlSchemaImport2 = (XmlSchemaImport)xmlSchemaExternal2;
									Uri uri = (xmlSchemaImport2.BaseUri != null) ? xmlSchemaImport2.BaseUri : ((xmlSchemaImport2.Schema != null && xmlSchemaImport2.Schema.BaseUri != null) ? xmlSchemaImport2.Schema.BaseUri : null);
									if (uri != null)
									{
										if (this.schemaLocations[uri] != null)
										{
											xmlSchemaImport2.Schema = null;
										}
										else
										{
											this.schemaLocations.Add(uri, uri);
										}
									}
								}
							}
							goto IL_385;
						}
					}
					if (xmlSchemaExternal is XmlSchemaImport && ((XmlSchemaImport)xmlSchemaExternal).Namespace == "http://www.w3.org/XML/1998/namespace")
					{
						if (!this.buildinIncluded)
						{
							this.buildinIncluded = true;
							xmlSchemaExternal.Schema = Preprocessor.GetBuildInSchema();
						}
					}
					else
					{
						string schemaLocation = xmlSchemaExternal.SchemaLocation;
						if (schemaLocation != null)
						{
							Uri uri2 = this.ResolveSchemaLocationUri(schema, schemaLocation);
							if (uri2 != null && this.schemaLocations[uri2] == null)
							{
								Stream schemaEntity = this.GetSchemaEntity(uri2);
								if (schemaEntity != null)
								{
									xmlSchemaExternal.BaseUri = uri2;
									this.schemaLocations.Add(uri2, uri2);
									XmlTextReader xmlTextReader = new XmlTextReader(uri2.ToString(), schemaEntity, base.NameTable);
									xmlTextReader.XmlResolver = this.xmlResolver;
									try
									{
										Parser parser = new Parser(SchemaType.XSD, base.NameTable, base.SchemaNames, base.EventHandler);
										parser.Parse(xmlTextReader, null);
										while (xmlTextReader.Read())
										{
										}
										xmlSchemaExternal.Schema = parser.XmlSchema;
										this.LoadExternals(xmlSchemaExternal.Schema, xsc);
										goto IL_385;
									}
									catch (XmlSchemaException ex)
									{
										base.SendValidationEventNoThrow(new XmlSchemaException("Sch_CannotLoadSchema", new string[]
										{
											schemaLocation,
											ex.Message
										}, ex.SourceUri, ex.LineNumber, ex.LinePosition), XmlSeverityType.Error);
										goto IL_385;
									}
									catch (Exception)
									{
										base.SendValidationEvent("Sch_InvalidIncludeLocation", xmlSchemaExternal, XmlSeverityType.Warning);
										goto IL_385;
									}
									finally
									{
										xmlTextReader.Close();
									}
								}
								base.SendValidationEvent("Sch_InvalidIncludeLocation", xmlSchemaExternal, XmlSeverityType.Warning);
							}
						}
					}
				}
				IL_385:;
			}
			schema.IsProcessing = false;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000C3650 File Offset: 0x000C1850
		private void BuildRefNamespaces(XmlSchema schema)
		{
			this.referenceNamespaces = new Hashtable();
			this.referenceNamespaces.Add("http://www.w3.org/2001/XMLSchema", "http://www.w3.org/2001/XMLSchema");
			this.referenceNamespaces.Add(string.Empty, string.Empty);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaImport xmlSchemaImport = schema.Includes[i] as XmlSchemaImport;
				if (xmlSchemaImport != null)
				{
					string @namespace = xmlSchemaImport.Namespace;
					if (@namespace != null && this.referenceNamespaces[@namespace] == null)
					{
						this.referenceNamespaces.Add(@namespace, @namespace);
					}
				}
			}
			if (schema.TargetNamespace != null && this.referenceNamespaces[schema.TargetNamespace] == null)
			{
				this.referenceNamespaces.Add(schema.TargetNamespace, schema.TargetNamespace);
			}
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000C3714 File Offset: 0x000C1914
		private void Preprocess(XmlSchema schema, string targetNamespace, SchemaCollectionPreprocessor.Compositor compositor)
		{
			if (schema.IsProcessing)
			{
				return;
			}
			schema.IsProcessing = true;
			string text = schema.TargetNamespace;
			if (text != null)
			{
				text = (schema.TargetNamespace = base.NameTable.Add(text));
				if (text.Length == 0)
				{
					base.SendValidationEvent("Sch_InvalidTargetNamespaceAttribute", schema);
				}
				else
				{
					try
					{
						XmlConvert.ToUri(text);
					}
					catch
					{
						base.SendValidationEvent("Sch_InvalidNamespace", schema.TargetNamespace, schema);
					}
				}
			}
			if (schema.Version != null)
			{
				try
				{
					XmlConvert.VerifyTOKEN(schema.Version);
				}
				catch (Exception)
				{
					base.SendValidationEvent("Sch_AttributeValueDataType", "version", schema);
				}
			}
			switch (compositor)
			{
			case SchemaCollectionPreprocessor.Compositor.Root:
				if (targetNamespace == null && schema.TargetNamespace != null)
				{
					targetNamespace = schema.TargetNamespace;
				}
				else if (schema.TargetNamespace == null && targetNamespace != null && targetNamespace.Length == 0)
				{
					targetNamespace = null;
				}
				if (targetNamespace != schema.TargetNamespace)
				{
					base.SendValidationEvent("Sch_MismatchTargetNamespaceEx", targetNamespace, schema.TargetNamespace, schema);
				}
				break;
			case SchemaCollectionPreprocessor.Compositor.Include:
				if (schema.TargetNamespace != null && targetNamespace != schema.TargetNamespace)
				{
					base.SendValidationEvent("Sch_MismatchTargetNamespaceInclude", targetNamespace, schema.TargetNamespace, schema);
				}
				break;
			case SchemaCollectionPreprocessor.Compositor.Import:
				if (targetNamespace != schema.TargetNamespace)
				{
					base.SendValidationEvent("Sch_MismatchTargetNamespaceImport", targetNamespace, schema.TargetNamespace, schema);
				}
				break;
			}
			int i = 0;
			while (i < schema.Includes.Count)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				this.SetParent(xmlSchemaExternal, schema);
				this.PreprocessAnnotation(xmlSchemaExternal);
				string schemaLocation = xmlSchemaExternal.SchemaLocation;
				if (schemaLocation != null)
				{
					try
					{
						XmlConvert.ToUri(schemaLocation);
						goto IL_1B6;
					}
					catch
					{
						base.SendValidationEvent("Sch_InvalidSchemaLocation", schemaLocation, xmlSchemaExternal);
						goto IL_1B6;
					}
					goto IL_18D;
				}
				goto IL_18D;
				IL_1B6:
				if (xmlSchemaExternal.Schema != null)
				{
					if (xmlSchemaExternal is XmlSchemaRedefine)
					{
						this.Preprocess(xmlSchemaExternal.Schema, schema.TargetNamespace, SchemaCollectionPreprocessor.Compositor.Include);
					}
					else if (xmlSchemaExternal is XmlSchemaImport)
					{
						if (((XmlSchemaImport)xmlSchemaExternal).Namespace == null && schema.TargetNamespace == null)
						{
							base.SendValidationEvent("Sch_ImportTargetNamespaceNull", xmlSchemaExternal);
						}
						else if (((XmlSchemaImport)xmlSchemaExternal).Namespace == schema.TargetNamespace)
						{
							base.SendValidationEvent("Sch_ImportTargetNamespace", xmlSchemaExternal);
						}
						this.Preprocess(xmlSchemaExternal.Schema, ((XmlSchemaImport)xmlSchemaExternal).Namespace, SchemaCollectionPreprocessor.Compositor.Import);
					}
					else
					{
						this.Preprocess(xmlSchemaExternal.Schema, schema.TargetNamespace, SchemaCollectionPreprocessor.Compositor.Include);
					}
				}
				else if (xmlSchemaExternal is XmlSchemaImport)
				{
					string @namespace = ((XmlSchemaImport)xmlSchemaExternal).Namespace;
					if (@namespace != null)
					{
						if (@namespace.Length == 0)
						{
							base.SendValidationEvent("Sch_InvalidNamespaceAttribute", @namespace, xmlSchemaExternal);
						}
						else
						{
							try
							{
								XmlConvert.ToUri(@namespace);
							}
							catch (FormatException)
							{
								base.SendValidationEvent("Sch_InvalidNamespace", @namespace, xmlSchemaExternal);
							}
						}
					}
				}
				i++;
				continue;
				IL_18D:
				if ((xmlSchemaExternal is XmlSchemaRedefine || xmlSchemaExternal is XmlSchemaInclude) && xmlSchemaExternal.Schema == null)
				{
					base.SendValidationEvent("Sch_MissRequiredAttribute", "schemaLocation", xmlSchemaExternal);
					goto IL_1B6;
				}
				goto IL_1B6;
			}
			this.BuildRefNamespaces(schema);
			this.targetNamespace = ((targetNamespace == null) ? string.Empty : targetNamespace);
			if (schema.BlockDefault == XmlSchemaDerivationMethod.All)
			{
				this.blockDefault = XmlSchemaDerivationMethod.All;
			}
			else if (schema.BlockDefault == XmlSchemaDerivationMethod.None)
			{
				this.blockDefault = XmlSchemaDerivationMethod.Empty;
			}
			else
			{
				if ((schema.BlockDefault & ~(XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("Sch_InvalidBlockDefaultValue", schema);
				}
				this.blockDefault = (schema.BlockDefault & (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
			}
			if (schema.FinalDefault == XmlSchemaDerivationMethod.All)
			{
				this.finalDefault = XmlSchemaDerivationMethod.All;
			}
			else if (schema.FinalDefault == XmlSchemaDerivationMethod.None)
			{
				this.finalDefault = XmlSchemaDerivationMethod.Empty;
			}
			else
			{
				if ((schema.FinalDefault & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("Sch_InvalidFinalDefaultValue", schema);
				}
				this.finalDefault = (schema.FinalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union));
			}
			this.elementFormDefault = schema.ElementFormDefault;
			if (this.elementFormDefault == XmlSchemaForm.None)
			{
				this.elementFormDefault = XmlSchemaForm.Unqualified;
			}
			this.attributeFormDefault = schema.AttributeFormDefault;
			if (this.attributeFormDefault == XmlSchemaForm.None)
			{
				this.attributeFormDefault = XmlSchemaForm.Unqualified;
			}
			for (int j = 0; j < schema.Includes.Count; j++)
			{
				XmlSchemaExternal xmlSchemaExternal2 = (XmlSchemaExternal)schema.Includes[j];
				if (xmlSchemaExternal2 is XmlSchemaRedefine)
				{
					XmlSchemaRedefine xmlSchemaRedefine = (XmlSchemaRedefine)xmlSchemaExternal2;
					if (xmlSchemaExternal2.Schema != null)
					{
						this.PreprocessRedefine(xmlSchemaRedefine);
					}
					else
					{
						for (int k = 0; k < xmlSchemaRedefine.Items.Count; k++)
						{
							if (!(xmlSchemaRedefine.Items[k] is XmlSchemaAnnotation))
							{
								base.SendValidationEvent("Sch_RedefineNoSchema", xmlSchemaRedefine);
								break;
							}
						}
					}
				}
				XmlSchema xmlSchema = xmlSchemaExternal2.Schema;
				if (xmlSchema != null)
				{
					foreach (object obj in xmlSchema.Elements.Values)
					{
						XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
						base.AddToTable(schema.Elements, xmlSchemaElement.QualifiedName, xmlSchemaElement);
					}
					foreach (object obj2 in xmlSchema.Attributes.Values)
					{
						XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
						base.AddToTable(schema.Attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
					}
					foreach (object obj3 in xmlSchema.Groups.Values)
					{
						XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)obj3;
						base.AddToTable(schema.Groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
					}
					foreach (object obj4 in xmlSchema.AttributeGroups.Values)
					{
						XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj4;
						base.AddToTable(schema.AttributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
					}
					foreach (object obj5 in xmlSchema.SchemaTypes.Values)
					{
						XmlSchemaType xmlSchemaType = (XmlSchemaType)obj5;
						base.AddToTable(schema.SchemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType);
					}
					foreach (object obj6 in xmlSchema.Notations.Values)
					{
						XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj6;
						base.AddToTable(schema.Notations, xmlSchemaNotation.QualifiedName, xmlSchemaNotation);
					}
				}
				this.ValidateIdAttribute(xmlSchemaExternal2);
			}
			List<XmlSchemaObject> list = new List<XmlSchemaObject>();
			for (int l = 0; l < schema.Items.Count; l++)
			{
				this.SetParent(schema.Items[l], schema);
				XmlSchemaAttribute xmlSchemaAttribute2 = schema.Items[l] as XmlSchemaAttribute;
				if (xmlSchemaAttribute2 != null)
				{
					this.PreprocessAttribute(xmlSchemaAttribute2);
					base.AddToTable(schema.Attributes, xmlSchemaAttribute2.QualifiedName, xmlSchemaAttribute2);
				}
				else if (schema.Items[l] is XmlSchemaAttributeGroup)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = (XmlSchemaAttributeGroup)schema.Items[l];
					this.PreprocessAttributeGroup(xmlSchemaAttributeGroup2);
					base.AddToTable(schema.AttributeGroups, xmlSchemaAttributeGroup2.QualifiedName, xmlSchemaAttributeGroup2);
				}
				else if (schema.Items[l] is XmlSchemaComplexType)
				{
					XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)schema.Items[l];
					this.PreprocessComplexType(xmlSchemaComplexType, false);
					base.AddToTable(schema.SchemaTypes, xmlSchemaComplexType.QualifiedName, xmlSchemaComplexType);
				}
				else if (schema.Items[l] is XmlSchemaSimpleType)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)schema.Items[l];
					this.PreprocessSimpleType(xmlSchemaSimpleType, false);
					base.AddToTable(schema.SchemaTypes, xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
				}
				else if (schema.Items[l] is XmlSchemaElement)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)schema.Items[l];
					this.PreprocessElement(xmlSchemaElement2);
					base.AddToTable(schema.Elements, xmlSchemaElement2.QualifiedName, xmlSchemaElement2);
				}
				else if (schema.Items[l] is XmlSchemaGroup)
				{
					XmlSchemaGroup xmlSchemaGroup2 = (XmlSchemaGroup)schema.Items[l];
					this.PreprocessGroup(xmlSchemaGroup2);
					base.AddToTable(schema.Groups, xmlSchemaGroup2.QualifiedName, xmlSchemaGroup2);
				}
				else if (schema.Items[l] is XmlSchemaNotation)
				{
					XmlSchemaNotation xmlSchemaNotation2 = (XmlSchemaNotation)schema.Items[l];
					this.PreprocessNotation(xmlSchemaNotation2);
					base.AddToTable(schema.Notations, xmlSchemaNotation2.QualifiedName, xmlSchemaNotation2);
				}
				else if (!(schema.Items[l] is XmlSchemaAnnotation))
				{
					base.SendValidationEvent("Sch_InvalidCollection", schema.Items[l]);
					list.Add(schema.Items[l]);
				}
			}
			for (int m = 0; m < list.Count; m++)
			{
				schema.Items.Remove(list[m]);
			}
			schema.IsProcessing = false;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000C40B8 File Offset: 0x000C22B8
		private void PreprocessRedefine(XmlSchemaRedefine redefine)
		{
			for (int i = 0; i < redefine.Items.Count; i++)
			{
				this.SetParent(redefine.Items[i], redefine);
				XmlSchemaGroup xmlSchemaGroup = redefine.Items[i] as XmlSchemaGroup;
				if (xmlSchemaGroup != null)
				{
					this.PreprocessGroup(xmlSchemaGroup);
					if (redefine.Groups[xmlSchemaGroup.QualifiedName] != null)
					{
						base.SendValidationEvent("Sch_GroupDoubleRedefine", xmlSchemaGroup);
					}
					else
					{
						base.AddToTable(redefine.Groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
						xmlSchemaGroup.Redefined = (XmlSchemaGroup)redefine.Schema.Groups[xmlSchemaGroup.QualifiedName];
						if (xmlSchemaGroup.Redefined != null)
						{
							this.CheckRefinedGroup(xmlSchemaGroup);
						}
						else
						{
							base.SendValidationEvent("Sch_GroupRedefineNotFound", xmlSchemaGroup);
						}
					}
				}
				else if (redefine.Items[i] is XmlSchemaAttributeGroup)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)redefine.Items[i];
					this.PreprocessAttributeGroup(xmlSchemaAttributeGroup);
					if (redefine.AttributeGroups[xmlSchemaAttributeGroup.QualifiedName] != null)
					{
						base.SendValidationEvent("Sch_AttrGroupDoubleRedefine", xmlSchemaAttributeGroup);
					}
					else
					{
						base.AddToTable(redefine.AttributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
						xmlSchemaAttributeGroup.Redefined = (XmlSchemaAttributeGroup)redefine.Schema.AttributeGroups[xmlSchemaAttributeGroup.QualifiedName];
						if (xmlSchemaAttributeGroup.Redefined != null)
						{
							this.CheckRefinedAttributeGroup(xmlSchemaAttributeGroup);
						}
						else
						{
							base.SendValidationEvent("Sch_AttrGroupRedefineNotFound", xmlSchemaAttributeGroup);
						}
					}
				}
				else if (redefine.Items[i] is XmlSchemaComplexType)
				{
					XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)redefine.Items[i];
					this.PreprocessComplexType(xmlSchemaComplexType, false);
					if (redefine.SchemaTypes[xmlSchemaComplexType.QualifiedName] != null)
					{
						base.SendValidationEvent("Sch_ComplexTypeDoubleRedefine", xmlSchemaComplexType);
					}
					else
					{
						base.AddToTable(redefine.SchemaTypes, xmlSchemaComplexType.QualifiedName, xmlSchemaComplexType);
						XmlSchemaType xmlSchemaType = (XmlSchemaType)redefine.Schema.SchemaTypes[xmlSchemaComplexType.QualifiedName];
						if (xmlSchemaType != null)
						{
							if (xmlSchemaType is XmlSchemaComplexType)
							{
								xmlSchemaComplexType.Redefined = xmlSchemaType;
								this.CheckRefinedComplexType(xmlSchemaComplexType);
							}
							else
							{
								base.SendValidationEvent("Sch_SimpleToComplexTypeRedefine", xmlSchemaComplexType);
							}
						}
						else
						{
							base.SendValidationEvent("Sch_ComplexTypeRedefineNotFound", xmlSchemaComplexType);
						}
					}
				}
				else if (redefine.Items[i] is XmlSchemaSimpleType)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)redefine.Items[i];
					this.PreprocessSimpleType(xmlSchemaSimpleType, false);
					if (redefine.SchemaTypes[xmlSchemaSimpleType.QualifiedName] != null)
					{
						base.SendValidationEvent("Sch_SimpleTypeDoubleRedefine", xmlSchemaSimpleType);
					}
					else
					{
						base.AddToTable(redefine.SchemaTypes, xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
						XmlSchemaType xmlSchemaType2 = (XmlSchemaType)redefine.Schema.SchemaTypes[xmlSchemaSimpleType.QualifiedName];
						if (xmlSchemaType2 != null)
						{
							if (xmlSchemaType2 is XmlSchemaSimpleType)
							{
								xmlSchemaSimpleType.Redefined = xmlSchemaType2;
								this.CheckRefinedSimpleType(xmlSchemaSimpleType);
							}
							else
							{
								base.SendValidationEvent("Sch_ComplexToSimpleTypeRedefine", xmlSchemaSimpleType);
							}
						}
						else
						{
							base.SendValidationEvent("Sch_SimpleTypeRedefineNotFound", xmlSchemaSimpleType);
						}
					}
				}
			}
			foreach (object obj in redefine.Groups)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				redefine.Schema.Groups.Insert((XmlQualifiedName)dictionaryEntry.Key, (XmlSchemaObject)dictionaryEntry.Value);
			}
			foreach (object obj2 in redefine.AttributeGroups)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				redefine.Schema.AttributeGroups.Insert((XmlQualifiedName)dictionaryEntry2.Key, (XmlSchemaObject)dictionaryEntry2.Value);
			}
			foreach (object obj3 in redefine.SchemaTypes)
			{
				DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
				redefine.Schema.SchemaTypes.Insert((XmlQualifiedName)dictionaryEntry3.Key, (XmlSchemaObject)dictionaryEntry3.Value);
			}
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000C4520 File Offset: 0x000C2720
		private int CountGroupSelfReference(XmlSchemaObjectCollection items, XmlQualifiedName name)
		{
			int num = 0;
			for (int i = 0; i < items.Count; i++)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = items[i] as XmlSchemaGroupRef;
				if (xmlSchemaGroupRef != null)
				{
					if (xmlSchemaGroupRef.RefName == name)
					{
						if (xmlSchemaGroupRef.MinOccurs != 1m || xmlSchemaGroupRef.MaxOccurs != 1m)
						{
							base.SendValidationEvent("Sch_MinMaxGroupRedefine", xmlSchemaGroupRef);
						}
						num++;
					}
				}
				else if (items[i] is XmlSchemaGroupBase)
				{
					num += this.CountGroupSelfReference(((XmlSchemaGroupBase)items[i]).Items, name);
				}
				if (num > 1)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000C45C8 File Offset: 0x000C27C8
		private void CheckRefinedGroup(XmlSchemaGroup group)
		{
			int num = 0;
			if (group.Particle != null)
			{
				num = this.CountGroupSelfReference(group.Particle.Items, group.QualifiedName);
			}
			if (num > 1)
			{
				base.SendValidationEvent("Sch_MultipleGroupSelfRef", group);
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000C4608 File Offset: 0x000C2808
		private void CheckRefinedAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			int num = 0;
			for (int i = 0; i < attributeGroup.Attributes.Count; i++)
			{
				XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = attributeGroup.Attributes[i] as XmlSchemaAttributeGroupRef;
				if (xmlSchemaAttributeGroupRef != null && xmlSchemaAttributeGroupRef.RefName == attributeGroup.QualifiedName)
				{
					num++;
				}
			}
			if (num > 1)
			{
				base.SendValidationEvent("Sch_MultipleAttrGroupSelfRef", attributeGroup);
			}
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000C466C File Offset: 0x000C286C
		private void CheckRefinedSimpleType(XmlSchemaSimpleType stype)
		{
			if (stype.Content != null && stype.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)stype.Content;
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName == stype.QualifiedName)
				{
					return;
				}
			}
			base.SendValidationEvent("Sch_InvalidTypeRedefine", stype);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000C46BC File Offset: 0x000C28BC
		private void CheckRefinedComplexType(XmlSchemaComplexType ctype)
		{
			if (ctype.ContentModel != null)
			{
				XmlQualifiedName baseTypeName;
				if (ctype.ContentModel is XmlSchemaComplexContent)
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)ctype.ContentModel;
					if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentRestriction)
					{
						baseTypeName = ((XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content).BaseTypeName;
					}
					else
					{
						baseTypeName = ((XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content).BaseTypeName;
					}
				}
				else
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)ctype.ContentModel;
					if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentRestriction)
					{
						baseTypeName = ((XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content).BaseTypeName;
					}
					else
					{
						baseTypeName = ((XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content).BaseTypeName;
					}
				}
				if (baseTypeName == ctype.QualifiedName)
				{
					return;
				}
			}
			base.SendValidationEvent("Sch_InvalidTypeRedefine", ctype);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000C4778 File Offset: 0x000C2978
		private void PreprocessAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.Name != null)
			{
				this.ValidateNameAttribute(attribute);
				attribute.SetQualifiedName(new XmlQualifiedName(attribute.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("Sch_MissRequiredAttribute", "name", attribute);
			}
			if (attribute.Use != XmlSchemaUse.None)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "use", attribute);
			}
			if (attribute.Form != XmlSchemaForm.None)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "form", attribute);
			}
			this.PreprocessAttributeContent(attribute);
			this.ValidateIdAttribute(attribute);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000C4800 File Offset: 0x000C2A00
		private void PreprocessLocalAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.Name != null)
			{
				this.ValidateNameAttribute(attribute);
				this.PreprocessAttributeContent(attribute);
				attribute.SetQualifiedName(new XmlQualifiedName(attribute.Name, (attribute.Form == XmlSchemaForm.Qualified || (attribute.Form == XmlSchemaForm.None && this.attributeFormDefault == XmlSchemaForm.Qualified)) ? this.targetNamespace : null));
			}
			else
			{
				this.PreprocessAnnotation(attribute);
				if (attribute.RefName.IsEmpty)
				{
					base.SendValidationEvent("Sch_AttributeNameRef", "???", attribute);
				}
				else
				{
					this.ValidateQNameAttribute(attribute, "ref", attribute.RefName);
				}
				if (!attribute.SchemaTypeName.IsEmpty || attribute.SchemaType != null || attribute.Form != XmlSchemaForm.None)
				{
					base.SendValidationEvent("Sch_InvalidAttributeRef", attribute);
				}
				attribute.SetQualifiedName(attribute.RefName);
			}
			this.ValidateIdAttribute(attribute);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000C48D0 File Offset: 0x000C2AD0
		private void PreprocessAttributeContent(XmlSchemaAttribute attribute)
		{
			this.PreprocessAnnotation(attribute);
			if (this.schema.TargetNamespace == "http://www.w3.org/2001/XMLSchema-instance")
			{
				base.SendValidationEvent("Sch_TargetNamespaceXsi", attribute);
			}
			if (!attribute.RefName.IsEmpty)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "ref", attribute);
			}
			if (attribute.DefaultValue != null && attribute.FixedValue != null)
			{
				base.SendValidationEvent("Sch_DefaultFixedAttributes", attribute);
			}
			if (attribute.DefaultValue != null && attribute.Use != XmlSchemaUse.Optional && attribute.Use != XmlSchemaUse.None)
			{
				base.SendValidationEvent("Sch_OptionalDefaultAttribute", attribute);
			}
			if (attribute.Name == this.Xmlns)
			{
				base.SendValidationEvent("Sch_XmlNsAttribute", attribute);
			}
			if (attribute.SchemaType != null)
			{
				this.SetParent(attribute.SchemaType, attribute);
				if (!attribute.SchemaTypeName.IsEmpty)
				{
					base.SendValidationEvent("Sch_TypeMutualExclusive", attribute);
				}
				this.PreprocessSimpleType(attribute.SchemaType, true);
			}
			if (!attribute.SchemaTypeName.IsEmpty)
			{
				this.ValidateQNameAttribute(attribute, "type", attribute.SchemaTypeName);
			}
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000C49E0 File Offset: 0x000C2BE0
		private void PreprocessAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			if (attributeGroup.Name != null)
			{
				this.ValidateNameAttribute(attributeGroup);
				attributeGroup.SetQualifiedName(new XmlQualifiedName(attributeGroup.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("Sch_MissRequiredAttribute", "name", attributeGroup);
			}
			this.PreprocessAttributes(attributeGroup.Attributes, attributeGroup.AnyAttribute, attributeGroup);
			this.PreprocessAnnotation(attributeGroup);
			this.ValidateIdAttribute(attributeGroup);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000C4A48 File Offset: 0x000C2C48
		private void PreprocessElement(XmlSchemaElement element)
		{
			if (element.Name != null)
			{
				this.ValidateNameAttribute(element);
				element.SetQualifiedName(new XmlQualifiedName(element.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("Sch_MissRequiredAttribute", "name", element);
			}
			this.PreprocessElementContent(element);
			if (element.Final == XmlSchemaDerivationMethod.All)
			{
				element.SetFinalResolved(XmlSchemaDerivationMethod.All);
			}
			else if (element.Final == XmlSchemaDerivationMethod.None)
			{
				if (this.finalDefault == XmlSchemaDerivationMethod.All)
				{
					element.SetFinalResolved(XmlSchemaDerivationMethod.All);
				}
				else
				{
					element.SetFinalResolved(this.finalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
			}
			else
			{
				if ((element.Final & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("Sch_InvalidElementFinalValue", element);
				}
				element.SetFinalResolved(element.Final & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
			}
			if (element.Form != XmlSchemaForm.None)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "form", element);
			}
			if (element.MinOccursString != null)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "minOccurs", element);
			}
			if (element.MaxOccursString != null)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "maxOccurs", element);
			}
			if (!element.SubstitutionGroup.IsEmpty)
			{
				this.ValidateQNameAttribute(element, "type", element.SubstitutionGroup);
			}
			this.ValidateIdAttribute(element);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000C4B7C File Offset: 0x000C2D7C
		private void PreprocessLocalElement(XmlSchemaElement element)
		{
			if (element.Name != null)
			{
				this.ValidateNameAttribute(element);
				this.PreprocessElementContent(element);
				element.SetQualifiedName(new XmlQualifiedName(element.Name, (element.Form == XmlSchemaForm.Qualified || (element.Form == XmlSchemaForm.None && this.elementFormDefault == XmlSchemaForm.Qualified)) ? this.targetNamespace : null));
			}
			else
			{
				this.PreprocessAnnotation(element);
				if (element.RefName.IsEmpty)
				{
					base.SendValidationEvent("Sch_ElementNameRef", element);
				}
				else
				{
					this.ValidateQNameAttribute(element, "ref", element.RefName);
				}
				if (!element.SchemaTypeName.IsEmpty || element.IsAbstract || element.Block != XmlSchemaDerivationMethod.None || element.SchemaType != null || element.HasConstraints || element.DefaultValue != null || element.Form != XmlSchemaForm.None || element.FixedValue != null || element.HasNillableAttribute)
				{
					base.SendValidationEvent("Sch_InvalidElementRef", element);
				}
				if (element.DefaultValue != null && element.FixedValue != null)
				{
					base.SendValidationEvent("Sch_DefaultFixedAttributes", element);
				}
				element.SetQualifiedName(element.RefName);
			}
			if (element.MinOccurs > element.MaxOccurs)
			{
				element.MinOccurs = 0m;
				base.SendValidationEvent("Sch_MinGtMax", element);
			}
			if (element.IsAbstract)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "abstract", element);
			}
			if (element.Final != XmlSchemaDerivationMethod.None)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "final", element);
			}
			if (!element.SubstitutionGroup.IsEmpty)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "substitutionGroup", element);
			}
			this.ValidateIdAttribute(element);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000C4D18 File Offset: 0x000C2F18
		private void PreprocessElementContent(XmlSchemaElement element)
		{
			this.PreprocessAnnotation(element);
			if (!element.RefName.IsEmpty)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "ref", element);
			}
			if (element.Block == XmlSchemaDerivationMethod.All)
			{
				element.SetBlockResolved(XmlSchemaDerivationMethod.All);
			}
			else if (element.Block == XmlSchemaDerivationMethod.None)
			{
				if (this.blockDefault == XmlSchemaDerivationMethod.All)
				{
					element.SetBlockResolved(XmlSchemaDerivationMethod.All);
				}
				else
				{
					element.SetBlockResolved(this.blockDefault & (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
			}
			else
			{
				if ((element.Block & ~(XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("Sch_InvalidElementBlockValue", element);
				}
				element.SetBlockResolved(element.Block & (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
			}
			if (element.SchemaType != null)
			{
				this.SetParent(element.SchemaType, element);
				if (!element.SchemaTypeName.IsEmpty)
				{
					base.SendValidationEvent("Sch_TypeMutualExclusive", element);
				}
				if (element.SchemaType is XmlSchemaComplexType)
				{
					this.PreprocessComplexType((XmlSchemaComplexType)element.SchemaType, true);
				}
				else
				{
					this.PreprocessSimpleType((XmlSchemaSimpleType)element.SchemaType, true);
				}
			}
			if (!element.SchemaTypeName.IsEmpty)
			{
				this.ValidateQNameAttribute(element, "type", element.SchemaTypeName);
			}
			if (element.DefaultValue != null && element.FixedValue != null)
			{
				base.SendValidationEvent("Sch_DefaultFixedAttributes", element);
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				this.SetParent(element.Constraints[i], element);
				this.PreprocessIdentityConstraint((XmlSchemaIdentityConstraint)element.Constraints[i]);
			}
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000C4E9C File Offset: 0x000C309C
		private void PreprocessIdentityConstraint(XmlSchemaIdentityConstraint constraint)
		{
			bool flag = true;
			this.PreprocessAnnotation(constraint);
			if (constraint.Name != null)
			{
				this.ValidateNameAttribute(constraint);
				constraint.SetQualifiedName(new XmlQualifiedName(constraint.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("Sch_MissRequiredAttribute", "name", constraint);
				flag = false;
			}
			if (this.schema.IdentityConstraints[constraint.QualifiedName] != null)
			{
				base.SendValidationEvent("Sch_DupIdentityConstraint", constraint.QualifiedName.ToString(), constraint);
				flag = false;
			}
			else
			{
				this.schema.IdentityConstraints.Add(constraint.QualifiedName, constraint);
			}
			if (constraint.Selector == null)
			{
				base.SendValidationEvent("Sch_IdConstraintNoSelector", constraint);
				flag = false;
			}
			if (constraint.Fields.Count == 0)
			{
				base.SendValidationEvent("Sch_IdConstraintNoFields", constraint);
				flag = false;
			}
			if (constraint is XmlSchemaKeyref)
			{
				XmlSchemaKeyref xmlSchemaKeyref = (XmlSchemaKeyref)constraint;
				if (xmlSchemaKeyref.Refer.IsEmpty)
				{
					base.SendValidationEvent("Sch_IdConstraintNoRefer", constraint);
					flag = false;
				}
				else
				{
					this.ValidateQNameAttribute(xmlSchemaKeyref, "refer", xmlSchemaKeyref.Refer);
				}
			}
			if (flag)
			{
				this.ValidateIdAttribute(constraint);
				this.ValidateIdAttribute(constraint.Selector);
				this.SetParent(constraint.Selector, constraint);
				for (int i = 0; i < constraint.Fields.Count; i++)
				{
					this.SetParent(constraint.Fields[i], constraint);
					this.ValidateIdAttribute(constraint.Fields[i]);
				}
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000C5004 File Offset: 0x000C3204
		private void PreprocessSimpleType(XmlSchemaSimpleType simpleType, bool local)
		{
			if (local)
			{
				if (simpleType.Name != null)
				{
					base.SendValidationEvent("Sch_ForbiddenAttribute", "name", simpleType);
				}
			}
			else
			{
				if (simpleType.Name != null)
				{
					this.ValidateNameAttribute(simpleType);
					simpleType.SetQualifiedName(new XmlQualifiedName(simpleType.Name, this.targetNamespace));
				}
				else
				{
					base.SendValidationEvent("Sch_MissRequiredAttribute", "name", simpleType);
				}
				if (simpleType.Final == XmlSchemaDerivationMethod.All)
				{
					simpleType.SetFinalResolved(XmlSchemaDerivationMethod.All);
				}
				else if (simpleType.Final == XmlSchemaDerivationMethod.None)
				{
					if (this.finalDefault == XmlSchemaDerivationMethod.All)
					{
						simpleType.SetFinalResolved(XmlSchemaDerivationMethod.All);
					}
					else
					{
						simpleType.SetFinalResolved(this.finalDefault & (XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union));
					}
				}
				else
				{
					if ((simpleType.Final & ~(XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union)) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("Sch_InvalidSimpleTypeFinalValue", simpleType);
					}
					simpleType.SetFinalResolved(simpleType.Final & (XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union));
				}
			}
			if (simpleType.Content == null)
			{
				base.SendValidationEvent("Sch_NoSimpleTypeContent", simpleType);
			}
			else if (simpleType.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)simpleType.Content;
				this.SetParent(xmlSchemaSimpleTypeRestriction, simpleType);
				for (int i = 0; i < xmlSchemaSimpleTypeRestriction.Facets.Count; i++)
				{
					this.SetParent(xmlSchemaSimpleTypeRestriction.Facets[i], xmlSchemaSimpleTypeRestriction);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseType != null)
				{
					if (!xmlSchemaSimpleTypeRestriction.BaseTypeName.IsEmpty)
					{
						base.SendValidationEvent("Sch_SimpleTypeRestRefBase", xmlSchemaSimpleTypeRestriction);
					}
					this.PreprocessSimpleType(xmlSchemaSimpleTypeRestriction.BaseType, true);
				}
				else if (xmlSchemaSimpleTypeRestriction.BaseTypeName.IsEmpty)
				{
					base.SendValidationEvent("Sch_SimpleTypeRestRefBaseNone", xmlSchemaSimpleTypeRestriction);
				}
				else
				{
					this.ValidateQNameAttribute(xmlSchemaSimpleTypeRestriction, "base", xmlSchemaSimpleTypeRestriction.BaseTypeName);
				}
				this.PreprocessAnnotation(xmlSchemaSimpleTypeRestriction);
				this.ValidateIdAttribute(xmlSchemaSimpleTypeRestriction);
			}
			else if (simpleType.Content is XmlSchemaSimpleTypeList)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)simpleType.Content;
				this.SetParent(xmlSchemaSimpleTypeList, simpleType);
				if (xmlSchemaSimpleTypeList.ItemType != null)
				{
					if (!xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
					{
						base.SendValidationEvent("Sch_SimpleTypeListRefBase", xmlSchemaSimpleTypeList);
					}
					this.SetParent(xmlSchemaSimpleTypeList.ItemType, xmlSchemaSimpleTypeList);
					this.PreprocessSimpleType(xmlSchemaSimpleTypeList.ItemType, true);
				}
				else if (xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
				{
					base.SendValidationEvent("Sch_SimpleTypeListRefBaseNone", xmlSchemaSimpleTypeList);
				}
				else
				{
					this.ValidateQNameAttribute(xmlSchemaSimpleTypeList, "itemType", xmlSchemaSimpleTypeList.ItemTypeName);
				}
				this.PreprocessAnnotation(xmlSchemaSimpleTypeList);
				this.ValidateIdAttribute(xmlSchemaSimpleTypeList);
			}
			else
			{
				XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)simpleType.Content;
				this.SetParent(xmlSchemaSimpleTypeUnion, simpleType);
				int num = xmlSchemaSimpleTypeUnion.BaseTypes.Count;
				if (xmlSchemaSimpleTypeUnion.MemberTypes != null)
				{
					num += xmlSchemaSimpleTypeUnion.MemberTypes.Length;
					for (int j = 0; j < xmlSchemaSimpleTypeUnion.MemberTypes.Length; j++)
					{
						this.ValidateQNameAttribute(xmlSchemaSimpleTypeUnion, "memberTypes", xmlSchemaSimpleTypeUnion.MemberTypes[j]);
					}
				}
				if (num == 0)
				{
					base.SendValidationEvent("Sch_SimpleTypeUnionNoBase", xmlSchemaSimpleTypeUnion);
				}
				for (int k = 0; k < xmlSchemaSimpleTypeUnion.BaseTypes.Count; k++)
				{
					this.SetParent(xmlSchemaSimpleTypeUnion.BaseTypes[k], xmlSchemaSimpleTypeUnion);
					this.PreprocessSimpleType((XmlSchemaSimpleType)xmlSchemaSimpleTypeUnion.BaseTypes[k], true);
				}
				this.PreprocessAnnotation(xmlSchemaSimpleTypeUnion);
				this.ValidateIdAttribute(xmlSchemaSimpleTypeUnion);
			}
			this.ValidateIdAttribute(simpleType);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000C532C File Offset: 0x000C352C
		private void PreprocessComplexType(XmlSchemaComplexType complexType, bool local)
		{
			if (local)
			{
				if (complexType.Name != null)
				{
					base.SendValidationEvent("Sch_ForbiddenAttribute", "name", complexType);
				}
			}
			else
			{
				if (complexType.Name != null)
				{
					this.ValidateNameAttribute(complexType);
					complexType.SetQualifiedName(new XmlQualifiedName(complexType.Name, this.targetNamespace));
				}
				else
				{
					base.SendValidationEvent("Sch_MissRequiredAttribute", "name", complexType);
				}
				if (complexType.Block == XmlSchemaDerivationMethod.All)
				{
					complexType.SetBlockResolved(XmlSchemaDerivationMethod.All);
				}
				else if (complexType.Block == XmlSchemaDerivationMethod.None)
				{
					complexType.SetBlockResolved(this.blockDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
				else
				{
					if ((complexType.Block & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("Sch_InvalidComplexTypeBlockValue", complexType);
					}
					complexType.SetBlockResolved(complexType.Block & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
				if (complexType.Final == XmlSchemaDerivationMethod.All)
				{
					complexType.SetFinalResolved(XmlSchemaDerivationMethod.All);
				}
				else if (complexType.Final == XmlSchemaDerivationMethod.None)
				{
					if (this.finalDefault == XmlSchemaDerivationMethod.All)
					{
						complexType.SetFinalResolved(XmlSchemaDerivationMethod.All);
					}
					else
					{
						complexType.SetFinalResolved(this.finalDefault & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
					}
				}
				else
				{
					if ((complexType.Final & ~(XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction)) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("Sch_InvalidComplexTypeFinalValue", complexType);
					}
					complexType.SetFinalResolved(complexType.Final & (XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction));
				}
			}
			if (complexType.ContentModel != null)
			{
				this.SetParent(complexType.ContentModel, complexType);
				this.PreprocessAnnotation(complexType.ContentModel);
				if (complexType.Particle == null)
				{
					XmlSchemaObjectCollection attributes = complexType.Attributes;
				}
				if (complexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
					if (xmlSchemaSimpleContent.Content == null)
					{
						if (complexType.QualifiedName == XmlQualifiedName.Empty)
						{
							base.SendValidationEvent("Sch_NoRestOrExt", complexType);
						}
						else
						{
							base.SendValidationEvent("Sch_NoRestOrExtQName", complexType.QualifiedName.Name, complexType.QualifiedName.Namespace, complexType);
						}
					}
					else
					{
						this.SetParent(xmlSchemaSimpleContent.Content, xmlSchemaSimpleContent);
						this.PreprocessAnnotation(xmlSchemaSimpleContent.Content);
						if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
						{
							XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content;
							if (xmlSchemaSimpleContentExtension.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("Sch_MissAttribute", "base", xmlSchemaSimpleContentExtension);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaSimpleContentExtension, "base", xmlSchemaSimpleContentExtension.BaseTypeName);
							}
							this.PreprocessAttributes(xmlSchemaSimpleContentExtension.Attributes, xmlSchemaSimpleContentExtension.AnyAttribute, xmlSchemaSimpleContentExtension);
							this.ValidateIdAttribute(xmlSchemaSimpleContentExtension);
						}
						else
						{
							XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
							if (xmlSchemaSimpleContentRestriction.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("Sch_MissAttribute", "base", xmlSchemaSimpleContentRestriction);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaSimpleContentRestriction, "base", xmlSchemaSimpleContentRestriction.BaseTypeName);
							}
							if (xmlSchemaSimpleContentRestriction.BaseType != null)
							{
								this.SetParent(xmlSchemaSimpleContentRestriction.BaseType, xmlSchemaSimpleContentRestriction);
								this.PreprocessSimpleType(xmlSchemaSimpleContentRestriction.BaseType, true);
							}
							this.PreprocessAttributes(xmlSchemaSimpleContentRestriction.Attributes, xmlSchemaSimpleContentRestriction.AnyAttribute, xmlSchemaSimpleContentRestriction);
							this.ValidateIdAttribute(xmlSchemaSimpleContentRestriction);
						}
					}
					this.ValidateIdAttribute(xmlSchemaSimpleContent);
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
					if (xmlSchemaComplexContent.Content == null)
					{
						if (complexType.QualifiedName == XmlQualifiedName.Empty)
						{
							base.SendValidationEvent("Sch_NoRestOrExt", complexType);
						}
						else
						{
							base.SendValidationEvent("Sch_NoRestOrExtQName", complexType.QualifiedName.Name, complexType.QualifiedName.Namespace, complexType);
						}
					}
					else
					{
						if (!xmlSchemaComplexContent.HasMixedAttribute && complexType.IsMixed)
						{
							xmlSchemaComplexContent.IsMixed = true;
						}
						this.SetParent(xmlSchemaComplexContent.Content, xmlSchemaComplexContent);
						this.PreprocessAnnotation(xmlSchemaComplexContent.Content);
						if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
						{
							XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content;
							if (xmlSchemaComplexContentExtension.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("Sch_MissAttribute", "base", xmlSchemaComplexContentExtension);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaComplexContentExtension, "base", xmlSchemaComplexContentExtension.BaseTypeName);
							}
							if (xmlSchemaComplexContentExtension.Particle != null)
							{
								this.SetParent(xmlSchemaComplexContentExtension.Particle, xmlSchemaComplexContentExtension);
								this.PreprocessParticle(xmlSchemaComplexContentExtension.Particle);
							}
							this.PreprocessAttributes(xmlSchemaComplexContentExtension.Attributes, xmlSchemaComplexContentExtension.AnyAttribute, xmlSchemaComplexContentExtension);
							this.ValidateIdAttribute(xmlSchemaComplexContentExtension);
						}
						else
						{
							XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content;
							if (xmlSchemaComplexContentRestriction.BaseTypeName.IsEmpty)
							{
								base.SendValidationEvent("Sch_MissAttribute", "base", xmlSchemaComplexContentRestriction);
							}
							else
							{
								this.ValidateQNameAttribute(xmlSchemaComplexContentRestriction, "base", xmlSchemaComplexContentRestriction.BaseTypeName);
							}
							if (xmlSchemaComplexContentRestriction.Particle != null)
							{
								this.SetParent(xmlSchemaComplexContentRestriction.Particle, xmlSchemaComplexContentRestriction);
								this.PreprocessParticle(xmlSchemaComplexContentRestriction.Particle);
							}
							this.PreprocessAttributes(xmlSchemaComplexContentRestriction.Attributes, xmlSchemaComplexContentRestriction.AnyAttribute, xmlSchemaComplexContentRestriction);
							this.ValidateIdAttribute(xmlSchemaComplexContentRestriction);
						}
						this.ValidateIdAttribute(xmlSchemaComplexContent);
					}
				}
			}
			else
			{
				if (complexType.Particle != null)
				{
					this.SetParent(complexType.Particle, complexType);
					this.PreprocessParticle(complexType.Particle);
				}
				this.PreprocessAttributes(complexType.Attributes, complexType.AnyAttribute, complexType);
			}
			this.ValidateIdAttribute(complexType);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000C5804 File Offset: 0x000C3A04
		private void PreprocessGroup(XmlSchemaGroup group)
		{
			if (group.Name != null)
			{
				this.ValidateNameAttribute(group);
				group.SetQualifiedName(new XmlQualifiedName(group.Name, this.targetNamespace));
			}
			else
			{
				base.SendValidationEvent("Sch_MissRequiredAttribute", "name", group);
			}
			if (group.Particle == null)
			{
				base.SendValidationEvent("Sch_NoGroupParticle", group);
				return;
			}
			if (group.Particle.MinOccursString != null)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "minOccurs", group.Particle);
			}
			if (group.Particle.MaxOccursString != null)
			{
				base.SendValidationEvent("Sch_ForbiddenAttribute", "maxOccurs", group.Particle);
			}
			this.PreprocessParticle(group.Particle);
			this.PreprocessAnnotation(group);
			this.ValidateIdAttribute(group);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000C58C0 File Offset: 0x000C3AC0
		private void PreprocessNotation(XmlSchemaNotation notation)
		{
			if (notation.Name != null)
			{
				this.ValidateNameAttribute(notation);
				notation.QualifiedName = new XmlQualifiedName(notation.Name, this.targetNamespace);
			}
			else
			{
				base.SendValidationEvent("Sch_MissRequiredAttribute", "name", notation);
			}
			if (notation.Public != null)
			{
				try
				{
					XmlConvert.ToUri(notation.Public);
					goto IL_75;
				}
				catch
				{
					base.SendValidationEvent("Sch_InvalidPublicAttribute", notation.Public, notation);
					goto IL_75;
				}
			}
			base.SendValidationEvent("Sch_MissRequiredAttribute", "public", notation);
			IL_75:
			if (notation.System != null)
			{
				try
				{
					XmlConvert.ToUri(notation.System);
				}
				catch
				{
					base.SendValidationEvent("Sch_InvalidSystemAttribute", notation.System, notation);
				}
			}
			this.PreprocessAnnotation(notation);
			this.ValidateIdAttribute(notation);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000C5998 File Offset: 0x000C3B98
		private void PreprocessParticle(XmlSchemaParticle particle)
		{
			XmlSchemaAll xmlSchemaAll = particle as XmlSchemaAll;
			if (xmlSchemaAll != null)
			{
				if (particle.MinOccurs != 0m && particle.MinOccurs != 1m)
				{
					particle.MinOccurs = 1m;
					base.SendValidationEvent("Sch_InvalidAllMin", particle);
				}
				if (particle.MaxOccurs != 1m)
				{
					particle.MaxOccurs = 1m;
					base.SendValidationEvent("Sch_InvalidAllMax", particle);
				}
				for (int i = 0; i < xmlSchemaAll.Items.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaAll.Items[i];
					if (xmlSchemaElement.MaxOccurs != 0m && xmlSchemaElement.MaxOccurs != 1m)
					{
						xmlSchemaElement.MaxOccurs = 1m;
						base.SendValidationEvent("Sch_InvalidAllElementMax", xmlSchemaElement);
					}
					this.SetParent(xmlSchemaElement, particle);
					this.PreprocessLocalElement(xmlSchemaElement);
				}
			}
			else
			{
				if (particle.MinOccurs > particle.MaxOccurs)
				{
					particle.MinOccurs = particle.MaxOccurs;
					base.SendValidationEvent("Sch_MinGtMax", particle);
				}
				XmlSchemaChoice xmlSchemaChoice = particle as XmlSchemaChoice;
				if (xmlSchemaChoice != null)
				{
					XmlSchemaObjectCollection items = xmlSchemaChoice.Items;
					for (int j = 0; j < items.Count; j++)
					{
						this.SetParent(items[j], particle);
						XmlSchemaElement xmlSchemaElement2 = items[j] as XmlSchemaElement;
						if (xmlSchemaElement2 != null)
						{
							this.PreprocessLocalElement(xmlSchemaElement2);
						}
						else
						{
							this.PreprocessParticle((XmlSchemaParticle)items[j]);
						}
					}
				}
				else if (particle is XmlSchemaSequence)
				{
					XmlSchemaObjectCollection items2 = ((XmlSchemaSequence)particle).Items;
					for (int k = 0; k < items2.Count; k++)
					{
						this.SetParent(items2[k], particle);
						XmlSchemaElement xmlSchemaElement3 = items2[k] as XmlSchemaElement;
						if (xmlSchemaElement3 != null)
						{
							this.PreprocessLocalElement(xmlSchemaElement3);
						}
						else
						{
							this.PreprocessParticle((XmlSchemaParticle)items2[k]);
						}
					}
				}
				else if (particle is XmlSchemaGroupRef)
				{
					XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle;
					if (xmlSchemaGroupRef.RefName.IsEmpty)
					{
						base.SendValidationEvent("Sch_MissAttribute", "ref", xmlSchemaGroupRef);
					}
					else
					{
						this.ValidateQNameAttribute(xmlSchemaGroupRef, "ref", xmlSchemaGroupRef.RefName);
					}
				}
				else if (particle is XmlSchemaAny)
				{
					try
					{
						((XmlSchemaAny)particle).BuildNamespaceListV1Compat(this.targetNamespace);
					}
					catch
					{
						base.SendValidationEvent("Sch_InvalidAny", particle);
					}
				}
			}
			this.PreprocessAnnotation(particle);
			this.ValidateIdAttribute(particle);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000C5C28 File Offset: 0x000C3E28
		private void PreprocessAttributes(XmlSchemaObjectCollection attributes, XmlSchemaAnyAttribute anyAttribute, XmlSchemaObject parent)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				this.SetParent(attributes[i], parent);
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					this.PreprocessLocalAttribute(xmlSchemaAttribute);
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)attributes[i];
					if (xmlSchemaAttributeGroupRef.RefName.IsEmpty)
					{
						base.SendValidationEvent("Sch_MissAttribute", "ref", xmlSchemaAttributeGroupRef);
					}
					else
					{
						this.ValidateQNameAttribute(xmlSchemaAttributeGroupRef, "ref", xmlSchemaAttributeGroupRef.RefName);
					}
					this.PreprocessAnnotation(attributes[i]);
					this.ValidateIdAttribute(attributes[i]);
				}
			}
			if (anyAttribute != null)
			{
				try
				{
					this.SetParent(anyAttribute, parent);
					this.PreprocessAnnotation(anyAttribute);
					anyAttribute.BuildNamespaceListV1Compat(this.targetNamespace);
				}
				catch
				{
					base.SendValidationEvent("Sch_InvalidAnyAttribute", anyAttribute);
				}
				this.ValidateIdAttribute(anyAttribute);
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000C5D14 File Offset: 0x000C3F14
		private void ValidateIdAttribute(XmlSchemaObject xso)
		{
			if (xso.IdAttribute != null)
			{
				try
				{
					xso.IdAttribute = base.NameTable.Add(XmlConvert.VerifyNCName(xso.IdAttribute));
					if (this.schema.Ids[xso.IdAttribute] != null)
					{
						base.SendValidationEvent("Sch_DupIdAttribute", xso);
					}
					else
					{
						this.schema.Ids.Add(xso.IdAttribute, xso);
					}
				}
				catch (Exception ex)
				{
					base.SendValidationEvent("Sch_InvalidIdAttribute", ex.Message, xso);
				}
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000C5DAC File Offset: 0x000C3FAC
		private void ValidateNameAttribute(XmlSchemaObject xso)
		{
			string text = xso.NameAttribute;
			if (text == null || text.Length == 0)
			{
				base.SendValidationEvent("Sch_InvalidNameAttributeEx", null, Res.GetString("Sch_NullValue"), xso);
			}
			text = XmlComplianceUtil.NonCDataNormalize(text);
			int num = ValidateNames.ParseNCName(text, 0);
			if (num != text.Length)
			{
				string[] array = XmlException.BuildCharExceptionArgs(text, num);
				string @string = Res.GetString("Xml_BadNameCharWithPos", new object[]
				{
					array[0],
					array[1],
					num
				});
				base.SendValidationEvent("Sch_InvalidNameAttributeEx", text, @string, xso);
				return;
			}
			xso.NameAttribute = base.NameTable.Add(text);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000C5E4C File Offset: 0x000C404C
		private void ValidateQNameAttribute(XmlSchemaObject xso, string attributeName, XmlQualifiedName value)
		{
			try
			{
				value.Verify();
				value.Atomize(base.NameTable);
				if (this.referenceNamespaces[value.Namespace] == null)
				{
					base.SendValidationEvent("Sch_UnrefNS", value.Namespace, xso, XmlSeverityType.Warning);
				}
			}
			catch (Exception ex)
			{
				base.SendValidationEvent("Sch_InvalidAttribute", attributeName, ex.Message, xso);
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000C5EBC File Offset: 0x000C40BC
		private void SetParent(XmlSchemaObject child, XmlSchemaObject parent)
		{
			child.Parent = parent;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000C5EC8 File Offset: 0x000C40C8
		private void PreprocessAnnotation(XmlSchemaObject schemaObject)
		{
			XmlSchemaAnnotated xmlSchemaAnnotated = schemaObject as XmlSchemaAnnotated;
			if (xmlSchemaAnnotated != null && xmlSchemaAnnotated.Annotation != null)
			{
				xmlSchemaAnnotated.Annotation.Parent = schemaObject;
				for (int i = 0; i < xmlSchemaAnnotated.Annotation.Items.Count; i++)
				{
					xmlSchemaAnnotated.Annotation.Items[i].Parent = xmlSchemaAnnotated.Annotation;
				}
			}
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000C5F2C File Offset: 0x000C412C
		private Uri ResolveSchemaLocationUri(XmlSchema enclosingSchema, string location)
		{
			Uri result;
			try
			{
				result = this.xmlResolver.ResolveUri(enclosingSchema.BaseUri, location);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000C5F64 File Offset: 0x000C4164
		private Stream GetSchemaEntity(Uri ruri)
		{
			Stream result;
			try
			{
				result = (Stream)this.xmlResolver.GetEntity(ruri, null, null);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000EF8 RID: 3832
		private XmlSchema schema;

		// Token: 0x04000EF9 RID: 3833
		private string targetNamespace;

		// Token: 0x04000EFA RID: 3834
		private bool buildinIncluded;

		// Token: 0x04000EFB RID: 3835
		private XmlSchemaForm elementFormDefault;

		// Token: 0x04000EFC RID: 3836
		private XmlSchemaForm attributeFormDefault;

		// Token: 0x04000EFD RID: 3837
		private XmlSchemaDerivationMethod blockDefault;

		// Token: 0x04000EFE RID: 3838
		private XmlSchemaDerivationMethod finalDefault;

		// Token: 0x04000EFF RID: 3839
		private Hashtable schemaLocations;

		// Token: 0x04000F00 RID: 3840
		private Hashtable referenceNamespaces;

		// Token: 0x04000F01 RID: 3841
		private string Xmlns;

		// Token: 0x04000F02 RID: 3842
		private const XmlSchemaDerivationMethod schemaBlockDefaultAllowed = XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04000F03 RID: 3843
		private const XmlSchemaDerivationMethod schemaFinalDefaultAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union;

		// Token: 0x04000F04 RID: 3844
		private const XmlSchemaDerivationMethod elementBlockAllowed = XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04000F05 RID: 3845
		private const XmlSchemaDerivationMethod elementFinalAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04000F06 RID: 3846
		private const XmlSchemaDerivationMethod simpleTypeFinalAllowed = XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union;

		// Token: 0x04000F07 RID: 3847
		private const XmlSchemaDerivationMethod complexTypeBlockAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04000F08 RID: 3848
		private const XmlSchemaDerivationMethod complexTypeFinalAllowed = XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction;

		// Token: 0x04000F09 RID: 3849
		private XmlResolver xmlResolver;

		// Token: 0x02000495 RID: 1173
		private enum Compositor
		{
			// Token: 0x04001E3D RID: 7741
			Root,
			// Token: 0x04001E3E RID: 7742
			Include,
			// Token: 0x04001E3F RID: 7743
			Import
		}
	}
}
