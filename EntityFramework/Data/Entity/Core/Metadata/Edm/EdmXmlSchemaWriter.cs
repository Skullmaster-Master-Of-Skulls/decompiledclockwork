using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000028 RID: 40
	internal class EdmXmlSchemaWriter : XmlSchemaWriter
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000088FF File Offset: 0x00006AFF
		private static string SyndicationItemPropertyToString(object value)
		{
			return EdmXmlSchemaWriter._syndicationItemToTargetPath[(int)value];
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000890D File Offset: 0x00006B0D
		private static string SyndicationTextContentKindToString(object value)
		{
			return EdmXmlSchemaWriter._syndicationTextContentKindToString[(int)value];
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000891B File Offset: 0x00006B1B
		public EdmXmlSchemaWriter()
		{
			this._resolver = DbConfiguration.DependencyResolver;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000892E File Offset: 0x00006B2E
		internal EdmXmlSchemaWriter(XmlWriter xmlWriter, double edmVersion, bool serializeDefaultNullability, IDbDependencyResolver resolver = null)
		{
			this._resolver = (resolver ?? DbConfiguration.DependencyResolver);
			this._serializeDefaultNullability = serializeDefaultNullability;
			this._xmlWriter = xmlWriter;
			this._version = edmVersion;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000895C File Offset: 0x00006B5C
		internal virtual void WriteSchemaElementHeader(string schemaNamespace)
		{
			string csdlNamespace = XmlConstants.GetCsdlNamespace(this._version);
			this._xmlWriter.WriteStartElement("Schema", csdlNamespace);
			this._xmlWriter.WriteAttributeString("Namespace", schemaNamespace);
			this._xmlWriter.WriteAttributeString("Alias", "Self");
			if (this._version == 3.0)
			{
				this._xmlWriter.WriteAttributeString("annotation", "UseStrongSpatialTypes", "http://schemas.microsoft.com/ado/2009/02/edm/annotation", "false");
			}
			this._xmlWriter.WriteAttributeString("xmlns", "annotation", null, "http://schemas.microsoft.com/ado/2009/02/edm/annotation");
			this._xmlWriter.WriteAttributeString("xmlns", "customannotation", null, "http://schemas.microsoft.com/ado/2013/11/edm/customannotation");
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008A14 File Offset: 0x00006C14
		internal virtual void WriteSchemaElementHeader(string schemaNamespace, string provider, string providerManifestToken, bool writeStoreSchemaGenNamespace)
		{
			string ssdlNamespace = XmlConstants.GetSsdlNamespace(this._version);
			this._xmlWriter.WriteStartElement("Schema", ssdlNamespace);
			this._xmlWriter.WriteAttributeString("Namespace", schemaNamespace);
			this._xmlWriter.WriteAttributeString("Provider", provider);
			this._xmlWriter.WriteAttributeString("ProviderManifestToken", providerManifestToken);
			this._xmlWriter.WriteAttributeString("Alias", "Self");
			if (writeStoreSchemaGenNamespace)
			{
				this._xmlWriter.WriteAttributeString("xmlns", "store", null, "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator");
			}
			this._xmlWriter.WriteAttributeString("xmlns", "customannotation", null, "http://schemas.microsoft.com/ado/2013/11/edm/customannotation");
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008AC0 File Offset: 0x00006CC0
		private void WritePolymorphicTypeAttributes(EdmType edmType)
		{
			if (edmType.BaseType != null)
			{
				this._xmlWriter.WriteAttributeString("BaseType", XmlSchemaWriter.GetQualifiedTypeName("Self", edmType.BaseType.Name));
			}
			if (edmType.Abstract)
			{
				this._xmlWriter.WriteAttributeString("Abstract", "true");
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008B18 File Offset: 0x00006D18
		public virtual void WriteFunctionElementHeader(EdmFunction function)
		{
			this._xmlWriter.WriteStartElement("Function");
			this._xmlWriter.WriteAttributeString("Name", function.Name);
			this._xmlWriter.WriteAttributeString("Aggregate", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.AggregateAttribute));
			this._xmlWriter.WriteAttributeString("BuiltIn", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.BuiltInAttribute));
			this._xmlWriter.WriteAttributeString("NiladicFunction", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.NiladicFunctionAttribute));
			this._xmlWriter.WriteAttributeString("IsComposable", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.IsComposableAttribute));
			this._xmlWriter.WriteAttributeString("ParameterTypeSemantics", function.ParameterTypeSemanticsAttribute.ToString());
			this._xmlWriter.WriteAttributeString("Schema", function.Schema);
			if (function.StoreFunctionNameAttribute != null && function.StoreFunctionNameAttribute != function.Name)
			{
				this._xmlWriter.WriteAttributeString("StoreFunctionName", function.StoreFunctionNameAttribute);
			}
			if (function.ReturnParameters != null && function.ReturnParameters.Any<FunctionParameter>())
			{
				EdmType edmType = function.ReturnParameters.First<FunctionParameter>().TypeUsage.EdmType;
				if (edmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					this._xmlWriter.WriteAttributeString("ReturnType", EdmXmlSchemaWriter.GetTypeName(edmType));
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008C6C File Offset: 0x00006E6C
		public virtual void WriteFunctionParameterHeader(FunctionParameter functionParameter)
		{
			this._xmlWriter.WriteStartElement("Parameter");
			this._xmlWriter.WriteAttributeString("Name", functionParameter.Name);
			this._xmlWriter.WriteAttributeString("Type", functionParameter.TypeName);
			this._xmlWriter.WriteAttributeString("Mode", functionParameter.Mode.ToString());
			if (functionParameter.IsMaxLength)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", "Max");
			}
			else if (!functionParameter.IsMaxLengthConstant && functionParameter.MaxLength != null)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", functionParameter.MaxLength.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!functionParameter.IsPrecisionConstant && functionParameter.Precision != null)
			{
				this._xmlWriter.WriteAttributeString("Precision", functionParameter.Precision.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!functionParameter.IsScaleConstant && functionParameter.Scale != null)
			{
				this._xmlWriter.WriteAttributeString("Scale", functionParameter.Scale.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008DC2 File Offset: 0x00006FC2
		internal virtual void WriteFunctionReturnTypeElementHeader()
		{
			this._xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008E0C File Offset: 0x0000700C
		internal void WriteEntityTypeElementHeader(EntityType entityType)
		{
			this._xmlWriter.WriteStartElement("EntityType");
			this._xmlWriter.WriteAttributeString("Name", entityType.Name);
			this.WriteExtendedProperties(entityType);
			if (entityType.Annotations.GetClrAttributes() != null)
			{
				foreach (Attribute attribute in entityType.Annotations.GetClrAttributes())
				{
					if (attribute.GetType().FullName.Equals("System.Data.Services.Common.HasStreamAttribute", StringComparison.Ordinal))
					{
						this._xmlWriter.WriteAttributeString("m", "HasStream", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
					}
					else if (attribute.GetType().FullName.Equals("System.Data.Services.MimeTypeAttribute", StringComparison.Ordinal))
					{
						string propertyName = attribute.GetType().GetDeclaredProperty("MemberName").GetValue(attribute, null) as string;
						EdmProperty property = entityType.Properties.SingleOrDefault((EdmProperty p) => p.Name.Equals(propertyName, StringComparison.Ordinal));
						EdmXmlSchemaWriter.AddAttributeAnnotation(property, attribute);
					}
					else if (attribute.GetType().FullName.Equals("System.Data.Services.Common.EntityPropertyMappingAttribute", StringComparison.Ordinal))
					{
						string text = attribute.GetType().GetDeclaredProperty("SourcePath").GetValue(attribute, null) as string;
						int num = text.IndexOf("/", StringComparison.Ordinal);
						string propertyName;
						if (num == -1)
						{
							propertyName = text;
						}
						else
						{
							propertyName = text.Substring(0, num);
						}
						EdmProperty property2 = entityType.Properties.SingleOrDefault((EdmProperty p) => p.Name.Equals(propertyName, StringComparison.Ordinal));
						EdmXmlSchemaWriter.AddAttributeAnnotation(property2, attribute);
					}
				}
			}
			this.WritePolymorphicTypeAttributes(entityType);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008FE0 File Offset: 0x000071E0
		internal void WriteEnumTypeElementHeader(EnumType enumType)
		{
			this._xmlWriter.WriteStartElement("EnumType");
			this._xmlWriter.WriteAttributeString("Name", enumType.Name);
			this._xmlWriter.WriteAttributeString("IsFlags", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(enumType.IsFlags));
			this.WriteExtendedProperties(enumType);
			if (enumType.UnderlyingType != null)
			{
				this._xmlWriter.WriteAttributeString("UnderlyingType", enumType.UnderlyingType.PrimitiveTypeKind.ToString());
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009064 File Offset: 0x00007264
		internal void WriteEnumTypeMemberElementHeader(EnumMember enumTypeMember)
		{
			this._xmlWriter.WriteStartElement("Member");
			this._xmlWriter.WriteAttributeString("Name", enumTypeMember.Name);
			this._xmlWriter.WriteAttributeString("Value", enumTypeMember.Value.ToString());
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000090B4 File Offset: 0x000072B4
		private static void AddAttributeAnnotation(EdmProperty property, Attribute a)
		{
			if (property != null)
			{
				IList<Attribute> clrAttributes = property.Annotations.GetClrAttributes();
				if (clrAttributes != null)
				{
					if (!clrAttributes.Contains(a))
					{
						clrAttributes.Add(a);
						return;
					}
				}
				else
				{
					property.GetMetadataProperties().SetClrAttributes(new List<Attribute>
					{
						a
					});
				}
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000090FD File Offset: 0x000072FD
		internal void WriteComplexTypeElementHeader(ComplexType complexType)
		{
			this._xmlWriter.WriteStartElement("ComplexType");
			this._xmlWriter.WriteAttributeString("Name", complexType.Name);
			this.WriteExtendedProperties(complexType);
			this.WritePolymorphicTypeAttributes(complexType);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009133 File Offset: 0x00007333
		internal virtual void WriteCollectionTypeElementHeader()
		{
			this._xmlWriter.WriteStartElement("CollectionType");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009145 File Offset: 0x00007345
		internal virtual void WriteRowTypeElementHeader()
		{
			this._xmlWriter.WriteStartElement("RowType");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009157 File Offset: 0x00007357
		internal void WriteAssociationTypeElementHeader(AssociationType associationType)
		{
			this._xmlWriter.WriteStartElement("Association");
			this._xmlWriter.WriteAttributeString("Name", associationType.Name);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009180 File Offset: 0x00007380
		internal void WriteAssociationEndElementHeader(RelationshipEndMember associationEnd)
		{
			this._xmlWriter.WriteStartElement("End");
			this._xmlWriter.WriteAttributeString("Role", associationEnd.Name);
			string name = associationEnd.GetEntityType().Name;
			this._xmlWriter.WriteAttributeString("Type", XmlSchemaWriter.GetQualifiedTypeName("Self", name));
			this._xmlWriter.WriteAttributeString("Multiplicity", RelationshipMultiplicityConverter.MultiplicityToString(associationEnd.RelationshipMultiplicity));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000091F5 File Offset: 0x000073F5
		internal void WriteOperationActionElement(string elementName, OperationAction operationAction)
		{
			this._xmlWriter.WriteStartElement(elementName);
			this._xmlWriter.WriteAttributeString("Action", operationAction.ToString());
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009229 File Offset: 0x00007429
		internal void WriteReferentialConstraintElementHeader()
		{
			this._xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000923B File Offset: 0x0000743B
		internal void WriteDelaredKeyPropertiesElementHeader()
		{
			this._xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000924D File Offset: 0x0000744D
		internal void WriteDelaredKeyPropertyRefElement(EdmProperty property)
		{
			this._xmlWriter.WriteStartElement("PropertyRef");
			this._xmlWriter.WriteAttributeString("Name", property.Name);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009280 File Offset: 0x00007480
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal void WritePropertyElementHeader(EdmProperty property)
		{
			this._xmlWriter.WriteStartElement("Property");
			this._xmlWriter.WriteAttributeString("Name", property.Name);
			this._xmlWriter.WriteAttributeString("Type", EdmXmlSchemaWriter.GetTypeReferenceName(property));
			if (property.CollectionKind != CollectionKind.None)
			{
				this._xmlWriter.WriteAttributeString("CollectionKind", property.CollectionKind.ToString());
			}
			if (property.ConcurrencyMode == ConcurrencyMode.Fixed)
			{
				this._xmlWriter.WriteAttributeString("ConcurrencyMode", "Fixed");
			}
			this.WriteExtendedProperties(property);
			if (property.Annotations.GetClrAttributes() != null)
			{
				int num = 0;
				foreach (Attribute attribute in property.Annotations.GetClrAttributes())
				{
					if (attribute.GetType().FullName.Equals("System.Data.Services.MimeTypeAttribute", StringComparison.Ordinal))
					{
						string value = attribute.GetType().GetDeclaredProperty("MimeType").GetValue(attribute, null) as string;
						this._xmlWriter.WriteAttributeString("m", "MimeType", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value);
					}
					else if (attribute.GetType().FullName.Equals("System.Data.Services.Common.EntityPropertyMappingAttribute", StringComparison.Ordinal))
					{
						string str = (num == 0) ? string.Empty : string.Format(CultureInfo.InvariantCulture, "_{0}", new object[]
						{
							num
						});
						string text = attribute.GetType().GetDeclaredProperty("SourcePath").GetValue(attribute, null) as string;
						int num2 = text.IndexOf("/", StringComparison.Ordinal);
						if (num2 != -1 && num2 + 1 < text.Length)
						{
							this._xmlWriter.WriteAttributeString("m", "FC_SourcePath" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text.Substring(num2 + 1));
						}
						object value2 = attribute.GetType().GetDeclaredProperty("TargetSyndicationItem").GetValue(attribute, null);
						string value3 = attribute.GetType().GetDeclaredProperty("KeepInContent").GetValue(attribute, null).ToString();
						PropertyInfo declaredProperty = attribute.GetType().GetDeclaredProperty("CriteriaValue");
						string text2 = null;
						if (declaredProperty != null)
						{
							text2 = (declaredProperty.GetValue(attribute, null) as string);
						}
						if (text2 != null)
						{
							this._xmlWriter.WriteAttributeString("m", "FC_TargetPath" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", EdmXmlSchemaWriter.SyndicationItemPropertyToString(value2));
							this._xmlWriter.WriteAttributeString("m", "FC_KeepInContent" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value3);
							this._xmlWriter.WriteAttributeString("m", "FC_CriteriaValue" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text2);
						}
						else if (string.Equals(value2.ToString(), "CustomProperty", StringComparison.Ordinal))
						{
							string value4 = attribute.GetType().GetDeclaredProperty("TargetPath").GetValue(attribute, null).ToString();
							string value5 = attribute.GetType().GetDeclaredProperty("TargetNamespacePrefix").GetValue(attribute, null).ToString();
							string value6 = attribute.GetType().GetDeclaredProperty("TargetNamespaceUri").GetValue(attribute, null).ToString();
							this._xmlWriter.WriteAttributeString("m", "FC_TargetPath" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value4);
							this._xmlWriter.WriteAttributeString("m", "FC_NsUri" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value6);
							this._xmlWriter.WriteAttributeString("m", "FC_NsPrefix" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value5);
							this._xmlWriter.WriteAttributeString("m", "FC_KeepInContent" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value3);
						}
						else
						{
							object value7 = attribute.GetType().GetDeclaredProperty("TargetTextContentKind").GetValue(attribute, null);
							this._xmlWriter.WriteAttributeString("m", "FC_TargetPath" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", EdmXmlSchemaWriter.SyndicationItemPropertyToString(value2));
							this._xmlWriter.WriteAttributeString("m", "FC_ContentKind" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", EdmXmlSchemaWriter.SyndicationTextContentKindToString(value7));
							this._xmlWriter.WriteAttributeString("m", "FC_KeepInContent" + str, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", value3);
						}
						num++;
					}
				}
			}
			if (property.IsMaxLength)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", "Max");
			}
			else if (!property.IsMaxLengthConstant && property.MaxLength != null)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", property.MaxLength.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!property.IsFixedLengthConstant && property.IsFixedLength != null)
			{
				this._xmlWriter.WriteAttributeString("FixedLength", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(property.IsFixedLength.Value));
			}
			if (!property.IsUnicodeConstant && property.IsUnicode != null)
			{
				this._xmlWriter.WriteAttributeString("Unicode", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(property.IsUnicode.Value));
			}
			if (!property.IsPrecisionConstant && property.Precision != null)
			{
				this._xmlWriter.WriteAttributeString("Precision", property.Precision.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!property.IsScaleConstant && property.Scale != null)
			{
				this._xmlWriter.WriteAttributeString("Scale", property.Scale.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (property.StoreGeneratedPattern != StoreGeneratedPattern.None)
			{
				this._xmlWriter.WriteAttributeString("StoreGeneratedPattern", (property.StoreGeneratedPattern == StoreGeneratedPattern.Computed) ? "Computed" : "Identity");
			}
			if (this._serializeDefaultNullability || !property.Nullable)
			{
				this._xmlWriter.WriteAttributeString("Nullable", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(property.Nullable));
			}
			MetadataProperty metadataProperty;
			if (property.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty))
			{
				this._xmlWriter.WriteAttributeString("StoreGeneratedPattern", "http://schemas.microsoft.com/ado/2009/02/edm/annotation", metadataProperty.Value.ToString());
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000098E4 File Offset: 0x00007AE4
		private static string GetTypeReferenceName(EdmProperty property)
		{
			if (property.IsPrimitiveType)
			{
				return property.TypeName;
			}
			if (property.IsComplexType)
			{
				return XmlSchemaWriter.GetQualifiedTypeName("Self", property.ComplexType.Name);
			}
			return XmlSchemaWriter.GetQualifiedTypeName("Self", property.EnumType.Name);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009934 File Offset: 0x00007B34
		internal void WriteNavigationPropertyElementHeader(NavigationProperty member)
		{
			this._xmlWriter.WriteStartElement("NavigationProperty");
			this._xmlWriter.WriteAttributeString("Name", member.Name);
			this._xmlWriter.WriteAttributeString("Relationship", XmlSchemaWriter.GetQualifiedTypeName("Self", member.Association.Name));
			this._xmlWriter.WriteAttributeString("FromRole", member.GetFromEnd().Name);
			this._xmlWriter.WriteAttributeString("ToRole", member.ToEndMember.Name);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000099C4 File Offset: 0x00007BC4
		internal void WriteReferentialConstraintRoleElement(string roleName, RelationshipEndMember edmAssociationEnd, IEnumerable<EdmProperty> properties)
		{
			this._xmlWriter.WriteStartElement(roleName);
			this._xmlWriter.WriteAttributeString("Role", edmAssociationEnd.Name);
			foreach (EdmProperty edmProperty in properties)
			{
				this._xmlWriter.WriteStartElement("PropertyRef");
				this._xmlWriter.WriteAttributeString("Name", edmProperty.Name);
				this._xmlWriter.WriteEndElement();
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009A64 File Offset: 0x00007C64
		internal virtual void WriteEntityContainerElementHeader(EntityContainer container)
		{
			this._xmlWriter.WriteStartElement("EntityContainer");
			this._xmlWriter.WriteAttributeString("Name", container.Name);
			this.WriteExtendedProperties(container);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009A94 File Offset: 0x00007C94
		internal void WriteAssociationSetElementHeader(AssociationSet associationSet)
		{
			this._xmlWriter.WriteStartElement("AssociationSet");
			this._xmlWriter.WriteAttributeString("Name", associationSet.Name);
			this._xmlWriter.WriteAttributeString("Association", XmlSchemaWriter.GetQualifiedTypeName("Self", associationSet.ElementType.Name));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009AEC File Offset: 0x00007CEC
		internal void WriteAssociationSetEndElement(EntitySet end, string roleName)
		{
			this._xmlWriter.WriteStartElement("End");
			this._xmlWriter.WriteAttributeString("Role", roleName);
			this._xmlWriter.WriteAttributeString("EntitySet", end.Name);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009B3C File Offset: 0x00007D3C
		internal virtual void WriteEntitySetElementHeader(EntitySet entitySet)
		{
			this._xmlWriter.WriteStartElement("EntitySet");
			this._xmlWriter.WriteAttributeString("Name", entitySet.Name);
			this._xmlWriter.WriteAttributeString("EntityType", XmlSchemaWriter.GetQualifiedTypeName("Self", entitySet.ElementType.Name));
			if (!string.IsNullOrWhiteSpace(entitySet.Schema))
			{
				this._xmlWriter.WriteAttributeString("Schema", entitySet.Schema);
			}
			if (!string.IsNullOrWhiteSpace(entitySet.Table))
			{
				this._xmlWriter.WriteAttributeString("Table", entitySet.Table);
			}
			this.WriteExtendedProperties(entitySet);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009BE4 File Offset: 0x00007DE4
		internal virtual void WriteFunctionImportElementHeader(EdmFunction functionImport)
		{
			this._xmlWriter.WriteStartElement("FunctionImport");
			this._xmlWriter.WriteAttributeString("Name", functionImport.Name);
			if (functionImport.IsComposableAttribute)
			{
				this._xmlWriter.WriteAttributeString("IsComposable", "true");
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009C34 File Offset: 0x00007E34
		internal virtual void WriteFunctionImportReturnTypeAttributes(FunctionParameter returnParameter, EntitySet entitySet, bool inline)
		{
			this._xmlWriter.WriteAttributeString(inline ? "ReturnType" : "Type", EdmXmlSchemaWriter.GetTypeName(returnParameter.TypeUsage.EdmType));
			if (entitySet != null)
			{
				this._xmlWriter.WriteAttributeString("EntitySet", entitySet.Name);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009C84 File Offset: 0x00007E84
		internal virtual void WriteFunctionImportParameterElementHeader(FunctionParameter parameter)
		{
			this._xmlWriter.WriteStartElement("Parameter");
			this._xmlWriter.WriteAttributeString("Name", parameter.Name);
			this._xmlWriter.WriteAttributeString("Mode", parameter.Mode.ToString());
			this._xmlWriter.WriteAttributeString("Type", EdmXmlSchemaWriter.GetTypeName(parameter.TypeUsage.EdmType));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009CF7 File Offset: 0x00007EF7
		internal void WriteDefiningQuery(EntitySet entitySet)
		{
			if (!string.IsNullOrWhiteSpace(entitySet.DefiningQuery))
			{
				this._xmlWriter.WriteElementString("DefiningQuery", entitySet.DefiningQuery);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009D1C File Offset: 0x00007F1C
		internal EdmXmlSchemaWriter Replicate(XmlWriter xmlWriter)
		{
			return new EdmXmlSchemaWriter(xmlWriter, this._version, this._serializeDefaultNullability, null);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009D3C File Offset: 0x00007F3C
		internal void WriteExtendedProperties(MetadataItem item)
		{
			foreach (MetadataProperty metadataProperty in from p in item.MetadataProperties
			where p.PropertyKind == PropertyKind.Extended
			select p)
			{
				string ns;
				string text;
				if (EdmXmlSchemaWriter.TrySplitExtendedMetadataPropertyName(metadataProperty.Name, out ns, out text) && metadataProperty.Name != "http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern")
				{
					Func<IMetadataAnnotationSerializer> service = this._resolver.GetService(text);
					string value = (service == null) ? metadataProperty.Value.ToString() : service().Serialize(text, metadataProperty.Value);
					this._xmlWriter.WriteAttributeString(text, ns, value);
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009E0C File Offset: 0x0000800C
		private static bool TrySplitExtendedMetadataPropertyName(string name, out string xmlNamespaceUri, out string attributeName)
		{
			int num = name.LastIndexOf(':');
			if (num < 1 || name.Length <= num + 1)
			{
				xmlNamespaceUri = null;
				attributeName = null;
				return false;
			}
			xmlNamespaceUri = name.Substring(0, num);
			attributeName = name.Substring(num + 1, name.Length - 1 - num);
			return true;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009E5C File Offset: 0x0000805C
		private static string GetTypeName(EdmType type)
		{
			if (type.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[]
				{
					EdmXmlSchemaWriter.GetTypeName(((CollectionType)type).TypeUsage.EdmType)
				});
			}
			if (type.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				return type.FullName;
			}
			return type.Name;
		}

		// Token: 0x040000B4 RID: 180
		private const string AnnotationNamespacePrefix = "annotation";

		// Token: 0x040000B5 RID: 181
		private const string CustomAnnotationNamespacePrefix = "customannotation";

		// Token: 0x040000B6 RID: 182
		private const string StoreSchemaGenNamespacePrefix = "store";

		// Token: 0x040000B7 RID: 183
		private const string DataServicesPrefix = "m";

		// Token: 0x040000B8 RID: 184
		private const string DataServicesNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

		// Token: 0x040000B9 RID: 185
		private const string DataServicesMimeTypeAttribute = "System.Data.Services.MimeTypeAttribute";

		// Token: 0x040000BA RID: 186
		private const string DataServicesHasStreamAttribute = "System.Data.Services.Common.HasStreamAttribute";

		// Token: 0x040000BB RID: 187
		private const string DataServicesEntityPropertyMappingAttribute = "System.Data.Services.Common.EntityPropertyMappingAttribute";

		// Token: 0x040000BC RID: 188
		private readonly bool _serializeDefaultNullability;

		// Token: 0x040000BD RID: 189
		private readonly IDbDependencyResolver _resolver;

		// Token: 0x040000BE RID: 190
		private static readonly string[] _syndicationItemToTargetPath = new string[]
		{
			string.Empty,
			"SyndicationAuthorEmail",
			"SyndicationAuthorName",
			"SyndicationAuthorUri",
			"SyndicationContributorEmail",
			"SyndicationContributorName",
			"SyndicationContributorUri",
			"SyndicationUpdated",
			"SyndicationPublished",
			"SyndicationRights",
			"SyndicationSummary",
			"SyndicationTitle",
			"SyndicationCategoryLabel",
			"SyndicationCategoryScheme",
			"SyndicationCategoryTerm",
			"SyndicationLinkHref",
			"SyndicationLinkHrefLang",
			"SyndicationLinkLength",
			"SyndicationLinkRel",
			"SyndicationLinkTitle",
			"SyndicationLinkType"
		};

		// Token: 0x040000BF RID: 191
		private static readonly string[] _syndicationTextContentKindToString = new string[]
		{
			"text",
			"html",
			"xhtml"
		};

		// Token: 0x02000029 RID: 41
		internal static class SyndicationXmlConstants
		{
			// Token: 0x040000C1 RID: 193
			internal const string SyndAuthorEmail = "SyndicationAuthorEmail";

			// Token: 0x040000C2 RID: 194
			internal const string SyndAuthorName = "SyndicationAuthorName";

			// Token: 0x040000C3 RID: 195
			internal const string SyndAuthorUri = "SyndicationAuthorUri";

			// Token: 0x040000C4 RID: 196
			internal const string SyndPublished = "SyndicationPublished";

			// Token: 0x040000C5 RID: 197
			internal const string SyndRights = "SyndicationRights";

			// Token: 0x040000C6 RID: 198
			internal const string SyndSummary = "SyndicationSummary";

			// Token: 0x040000C7 RID: 199
			internal const string SyndTitle = "SyndicationTitle";

			// Token: 0x040000C8 RID: 200
			internal const string SyndContributorEmail = "SyndicationContributorEmail";

			// Token: 0x040000C9 RID: 201
			internal const string SyndContributorName = "SyndicationContributorName";

			// Token: 0x040000CA RID: 202
			internal const string SyndContributorUri = "SyndicationContributorUri";

			// Token: 0x040000CB RID: 203
			internal const string SyndCategoryLabel = "SyndicationCategoryLabel";

			// Token: 0x040000CC RID: 204
			internal const string SyndContentKindPlaintext = "text";

			// Token: 0x040000CD RID: 205
			internal const string SyndContentKindHtml = "html";

			// Token: 0x040000CE RID: 206
			internal const string SyndContentKindXHtml = "xhtml";

			// Token: 0x040000CF RID: 207
			internal const string SyndUpdated = "SyndicationUpdated";

			// Token: 0x040000D0 RID: 208
			internal const string SyndLinkHref = "SyndicationLinkHref";

			// Token: 0x040000D1 RID: 209
			internal const string SyndLinkRel = "SyndicationLinkRel";

			// Token: 0x040000D2 RID: 210
			internal const string SyndLinkType = "SyndicationLinkType";

			// Token: 0x040000D3 RID: 211
			internal const string SyndLinkHrefLang = "SyndicationLinkHrefLang";

			// Token: 0x040000D4 RID: 212
			internal const string SyndLinkTitle = "SyndicationLinkTitle";

			// Token: 0x040000D5 RID: 213
			internal const string SyndLinkLength = "SyndicationLinkLength";

			// Token: 0x040000D6 RID: 214
			internal const string SyndCategoryTerm = "SyndicationCategoryTerm";

			// Token: 0x040000D7 RID: 215
			internal const string SyndCategoryScheme = "SyndicationCategoryScheme";
		}
	}
}
