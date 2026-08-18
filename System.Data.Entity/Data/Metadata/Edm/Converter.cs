using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.EntityModel.SchemaObjectModel;
using System.Globalization;
using System.Linq;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000209 RID: 521
	internal static class Converter
	{
		// Token: 0x06002283 RID: 8835 RVA: 0x00079924 File Offset: 0x00077B24
		static Converter()
		{
			EnumType enumType = new EnumType("ConcurrencyMode", "Edm", PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32), false, DataSpace.CSpace);
			foreach (string text in Enum.GetNames(typeof(ConcurrencyMode)))
			{
				enumType.AddMember(new EnumMember(text, (int)Enum.Parse(typeof(ConcurrencyMode), text, false)));
			}
			EnumType enumType2 = new EnumType("StoreGeneratedPattern", "Edm", PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32), false, DataSpace.CSpace);
			foreach (string text2 in Enum.GetNames(typeof(StoreGeneratedPattern)))
			{
				enumType2.AddMember(new EnumMember(text2, (int)Enum.Parse(typeof(StoreGeneratedPattern), text2, false)));
			}
			Converter.ConcurrencyModeFacet = new FacetDescription("ConcurrencyMode", enumType, null, null, ConcurrencyMode.None);
			Converter.StoreGeneratedPatternFacet = new FacetDescription("StoreGeneratedPattern", enumType2, null, null, StoreGeneratedPattern.None);
			Converter.CollationFacet = new FacetDescription("Collation", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String), null, null, string.Empty);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00079A8C File Offset: 0x00077C8C
		internal static IEnumerable<GlobalItem> ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			Dictionary<SchemaElement, GlobalItem> dictionary = new Dictionary<SchemaElement, GlobalItem>();
			Converter.ConvertSchema(somSchema, providerManifest, new Converter.ConversionCache(itemCollection), dictionary);
			return dictionary.Values;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00079AB4 File Offset: 0x00077CB4
		internal static IEnumerable<GlobalItem> ConvertSchema(IList<Schema> somSchemas, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			Dictionary<SchemaElement, GlobalItem> dictionary = new Dictionary<SchemaElement, GlobalItem>();
			Converter.ConversionCache convertedItemCache = new Converter.ConversionCache(itemCollection);
			foreach (Schema somSchema in somSchemas)
			{
				Converter.ConvertSchema(somSchema, providerManifest, convertedItemCache, dictionary);
			}
			return dictionary.Values;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00079B14 File Offset: 0x00077D14
		private static void ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			List<Function> list = new List<Function>();
			foreach (SchemaType schemaType in somSchema.SchemaTypes)
			{
				if (Converter.LoadSchemaElement(schemaType, providerManifest, convertedItemCache, newGlobalItems) == null && schemaType is Function)
				{
					list.Add(schemaType as Function);
				}
			}
			foreach (SchemaEntityType element in somSchema.SchemaTypes.OfType<SchemaEntityType>())
			{
				Converter.LoadEntityTypePhase2(element, providerManifest, convertedItemCache, newGlobalItems);
			}
			foreach (Function element2 in list)
			{
				Converter.LoadSchemaElement(element2, providerManifest, convertedItemCache, newGlobalItems);
			}
			if (convertedItemCache.ItemCollection.DataSpace == DataSpace.CSpace)
			{
				EdmItemCollection edmItemCollection = (EdmItemCollection)convertedItemCache.ItemCollection;
				edmItemCollection.EdmVersion = somSchema.SchemaVersion;
				return;
			}
			StoreItemCollection storeItemCollection = convertedItemCache.ItemCollection as StoreItemCollection;
			if (storeItemCollection != null)
			{
				storeItemCollection.StoreSchemaVersion = somSchema.SchemaVersion;
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00079C54 File Offset: 0x00077E54
		internal static MetadataItem LoadSchemaElement(SchemaType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			GlobalItem result;
			if (newGlobalItems.TryGetValue(element, out result))
			{
				return result;
			}
			EntityContainer entityContainer = element as EntityContainer;
			if (entityContainer != null)
			{
				result = Converter.ConvertToEntityContainer(entityContainer, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is SchemaEntityType)
			{
				result = Converter.ConvertToEntityType((SchemaEntityType)element, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is Relationship)
			{
				result = Converter.ConvertToAssociationType((Relationship)element, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is SchemaComplexType)
			{
				result = Converter.ConvertToComplexType((SchemaComplexType)element, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is Function)
			{
				result = Converter.ConvertToFunction((Function)element, providerManifest, convertedItemCache, null, newGlobalItems);
			}
			else
			{
				if (!(element is SchemaEnumType))
				{
					return null;
				}
				result = Converter.ConvertToEnumType((SchemaEnumType)element, newGlobalItems);
			}
			return result;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x00079D04 File Offset: 0x00077F04
		private static EntityContainer ConvertToEntityContainer(EntityContainer element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityContainer entityContainer = new EntityContainer(element.Name, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, entityContainer);
			foreach (EntityContainerEntitySet set in element.EntitySets)
			{
				entityContainer.AddEntitySetBase(Converter.ConvertToEntitySet(set, entityContainer.Name, providerManifest, convertedItemCache, newGlobalItems));
			}
			foreach (EntityContainerRelationshipSet relationshipSet in element.RelationshipSets)
			{
				entityContainer.AddEntitySetBase(Converter.ConvertToAssociationSet(relationshipSet, providerManifest, convertedItemCache, entityContainer, newGlobalItems));
			}
			foreach (Function somFunction in element.FunctionImports)
			{
				entityContainer.AddFunctionImport(Converter.ConvertToFunction(somFunction, providerManifest, convertedItemCache, entityContainer, newGlobalItems));
			}
			if (element.Documentation != null)
			{
				entityContainer.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, entityContainer);
			return entityContainer;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00079E30 File Offset: 0x00078030
		private static EntityType ConvertToEntityType(SchemaEntityType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			string[] array = null;
			if (element.DeclaredKeyProperties.Count != 0)
			{
				array = new string[element.DeclaredKeyProperties.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = element.DeclaredKeyProperties[i].Property.Name;
				}
			}
			EdmProperty[] array2 = new EdmProperty[element.Properties.Count];
			int num = 0;
			foreach (StructuredProperty somProperty in element.Properties)
			{
				array2[num++] = Converter.ConvertToProperty(somProperty, providerManifest, convertedItemCache, newGlobalItems);
			}
			EntityType entityType = new EntityType(element.Name, element.Namespace, Converter.GetDataSpace(providerManifest), array, array2);
			if (element.BaseType != null)
			{
				entityType.BaseType = (EdmType)Converter.LoadSchemaElement(element.BaseType, providerManifest, convertedItemCache, newGlobalItems);
			}
			entityType.Abstract = element.IsAbstract;
			if (element.Documentation != null)
			{
				entityType.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, entityType);
			newGlobalItems.Add(element, entityType);
			return entityType;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00079F60 File Offset: 0x00078160
		private static void LoadEntityTypePhase2(SchemaEntityType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)newGlobalItems[element];
			foreach (NavigationProperty somNavigationProperty in element.NavigationProperties)
			{
				entityType.AddMember(Converter.ConvertToNavigationProperty(entityType, somNavigationProperty, providerManifest, convertedItemCache, newGlobalItems));
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00079FC4 File Offset: 0x000781C4
		private static ComplexType ConvertToComplexType(SchemaComplexType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			ComplexType complexType = new ComplexType(element.Name, element.Namespace, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, complexType);
			foreach (StructuredProperty somProperty in element.Properties)
			{
				complexType.AddMember(Converter.ConvertToProperty(somProperty, providerManifest, convertedItemCache, newGlobalItems));
			}
			complexType.Abstract = element.IsAbstract;
			if (element.BaseType != null)
			{
				complexType.BaseType = (EdmType)Converter.LoadSchemaElement(element.BaseType, providerManifest, convertedItemCache, newGlobalItems);
			}
			if (element.Documentation != null)
			{
				complexType.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, complexType);
			return complexType;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0007A088 File Offset: 0x00078288
		private static AssociationType ConvertToAssociationType(Relationship element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			AssociationType associationType = new AssociationType(element.Name, element.Namespace, element.IsForeignKey, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, associationType);
			foreach (IRelationshipEnd relationshipEnd in element.Ends)
			{
				RelationshipEnd relationshipEnd2 = (RelationshipEnd)relationshipEnd;
				SchemaType type = relationshipEnd2.Type;
				EntityType endMemberType = (EntityType)Converter.LoadSchemaElement(type, providerManifest, convertedItemCache, newGlobalItems);
				AssociationEndMember associationEndMember = Converter.InitializeAssociationEndMember(associationType, relationshipEnd2, endMemberType);
				Converter.AddOtherContent(relationshipEnd2, associationEndMember);
				foreach (OnOperation onOperation in relationshipEnd2.Operations)
				{
					if (onOperation.Operation == Operation.Delete)
					{
						OperationAction deleteBehavior = OperationAction.None;
						System.Data.EntityModel.SchemaObjectModel.Action action = onOperation.Action;
						if (action != System.Data.EntityModel.SchemaObjectModel.Action.None)
						{
							if (action == System.Data.EntityModel.SchemaObjectModel.Action.Cascade)
							{
								deleteBehavior = OperationAction.Cascade;
							}
						}
						else
						{
							deleteBehavior = OperationAction.None;
						}
						associationEndMember.DeleteBehavior = deleteBehavior;
					}
				}
				if (relationshipEnd2.Documentation != null)
				{
					associationEndMember.Documentation = Converter.ConvertToDocumentation(relationshipEnd2.Documentation);
				}
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				ReferentialConstraint referentialConstraint = element.Constraints[i];
				AssociationEndMember associationEndMember2 = (AssociationEndMember)associationType.Members[referentialConstraint.PrincipalRole.Name];
				AssociationEndMember associationEndMember3 = (AssociationEndMember)associationType.Members[referentialConstraint.DependentRole.Name];
				EntityTypeBase elementType = ((RefType)associationEndMember2.TypeUsage.EdmType).ElementType;
				EntityTypeBase elementType2 = ((RefType)associationEndMember3.TypeUsage.EdmType).ElementType;
				ReferentialConstraint referentialConstraint2 = new ReferentialConstraint(associationEndMember2, associationEndMember3, Converter.GetProperties(elementType, referentialConstraint.PrincipalRole.RoleProperties), Converter.GetProperties(elementType2, referentialConstraint.DependentRole.RoleProperties));
				if (referentialConstraint.Documentation != null)
				{
					referentialConstraint2.Documentation = Converter.ConvertToDocumentation(referentialConstraint.Documentation);
				}
				if (referentialConstraint.PrincipalRole.Documentation != null)
				{
					referentialConstraint2.FromRole.Documentation = Converter.ConvertToDocumentation(referentialConstraint.PrincipalRole.Documentation);
				}
				if (referentialConstraint.DependentRole.Documentation != null)
				{
					referentialConstraint2.ToRole.Documentation = Converter.ConvertToDocumentation(referentialConstraint.DependentRole.Documentation);
				}
				associationType.AddReferentialConstraint(referentialConstraint2);
				Converter.AddOtherContent(element.Constraints[i], referentialConstraint2);
			}
			if (element.Documentation != null)
			{
				associationType.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, associationType);
			return associationType;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0007A328 File Offset: 0x00078528
		private static AssociationEndMember InitializeAssociationEndMember(AssociationType associationType, IRelationshipEnd end, EntityType endMemberType)
		{
			EdmMember edmMember;
			AssociationEndMember associationEndMember;
			if (!associationType.Members.TryGetValue(end.Name, false, out edmMember))
			{
				associationEndMember = new AssociationEndMember(end.Name, endMemberType.GetReferenceType(), end.Multiplicity.Value);
				associationType.AddKeyMember(associationEndMember);
			}
			else
			{
				associationEndMember = (AssociationEndMember)edmMember;
			}
			RelationshipEnd relationshipEnd = end as RelationshipEnd;
			if (relationshipEnd != null && relationshipEnd.Documentation != null)
			{
				associationEndMember.Documentation = Converter.ConvertToDocumentation(relationshipEnd.Documentation);
			}
			return associationEndMember;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0007A3A0 File Offset: 0x000785A0
		private static EdmProperty[] GetProperties(EntityTypeBase entityType, IList<PropertyRefElement> properties)
		{
			EdmProperty[] array = new EdmProperty[properties.Count];
			for (int i = 0; i < properties.Count; i++)
			{
				array[i] = (EdmProperty)entityType.Members[properties[i].Name];
			}
			return array;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0007A3EA File Offset: 0x000785EA
		private static void AddOtherContent(SchemaElement element, MetadataItem item)
		{
			if (element.OtherContent.Count > 0)
			{
				item.AddMetadataProperties(element.OtherContent);
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0007A408 File Offset: 0x00078608
		private static EntitySet ConvertToEntitySet(EntityContainerEntitySet set, string containerName, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntitySet entitySet = new EntitySet(set.Name, set.DbSchema, set.Table, set.DefiningQuery, (EntityType)Converter.LoadSchemaElement(set.EntityType, providerManifest, convertedItemCache, newGlobalItems));
			if (set.Documentation != null)
			{
				entitySet.Documentation = Converter.ConvertToDocumentation(set.Documentation);
			}
			Converter.AddOtherContent(set, entitySet);
			return entitySet;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0007A468 File Offset: 0x00078668
		private static EntitySet GetEntitySet(EntityContainerEntitySet set, EntityContainer container)
		{
			return container.GetEntitySetByName(set.Name, false);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0007A478 File Offset: 0x00078678
		private static AssociationSet ConvertToAssociationSet(EntityContainerRelationshipSet relationshipSet, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, EntityContainer container, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			AssociationType associationType = (AssociationType)Converter.LoadSchemaElement((SchemaType)relationshipSet.Relationship, providerManifest, convertedItemCache, newGlobalItems);
			AssociationSet associationSet = new AssociationSet(relationshipSet.Name, associationType);
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in relationshipSet.Ends)
			{
				EntityType entityType = (EntityType)Converter.LoadSchemaElement(entityContainerRelationshipSetEnd.EntitySet.EntityType, providerManifest, convertedItemCache, newGlobalItems);
				AssociationEndMember endMember = (AssociationEndMember)associationType.Members[entityContainerRelationshipSetEnd.Name];
				AssociationSetEnd associationSetEnd = new AssociationSetEnd(Converter.GetEntitySet(entityContainerRelationshipSetEnd.EntitySet, container), associationSet, endMember);
				Converter.AddOtherContent(entityContainerRelationshipSetEnd, associationSetEnd);
				associationSet.AddAssociationSetEnd(associationSetEnd);
				if (entityContainerRelationshipSetEnd.Documentation != null)
				{
					associationSetEnd.Documentation = Converter.ConvertToDocumentation(entityContainerRelationshipSetEnd.Documentation);
				}
			}
			if (relationshipSet.Documentation != null)
			{
				associationSet.Documentation = Converter.ConvertToDocumentation(relationshipSet.Documentation);
			}
			Converter.AddOtherContent(relationshipSet, associationSet);
			return associationSet;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007A57C File Offset: 0x0007877C
		private static EdmProperty ConvertToProperty(StructuredProperty somProperty, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			TypeUsage typeUsage = null;
			ScalarType scalarType = somProperty.Type as ScalarType;
			if (scalarType != null && somProperty.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				typeUsage = somProperty.TypeUsage;
				Converter.UpdateSentinelValuesInFacets(ref typeUsage);
			}
			else
			{
				EdmType edmType;
				if (scalarType != null)
				{
					edmType = convertedItemCache.ItemCollection.GetItem<PrimitiveType>(somProperty.TypeUsage.EdmType.FullName);
				}
				else
				{
					edmType = (EdmType)Converter.LoadSchemaElement(somProperty.Type, providerManifest, convertedItemCache, newGlobalItems);
				}
				if (somProperty.CollectionKind != CollectionKind.None)
				{
					typeUsage = TypeUsage.Create(new CollectionType(edmType));
				}
				else
				{
					SchemaEnumType schemaEnumType = (scalarType == null) ? (somProperty.Type as SchemaEnumType) : null;
					typeUsage = TypeUsage.Create(edmType);
					if (schemaEnumType != null)
					{
						somProperty.EnsureEnumTypeFacets(convertedItemCache, newGlobalItems);
					}
					if (somProperty.TypeUsage != null)
					{
						Converter.ApplyTypePropertyFacets(somProperty.TypeUsage, ref typeUsage);
					}
				}
			}
			Converter.PopulateGeneralFacets(somProperty, providerManifest, ref typeUsage);
			EdmProperty edmProperty = new EdmProperty(somProperty.Name, typeUsage);
			if (somProperty.Documentation != null)
			{
				edmProperty.Documentation = Converter.ConvertToDocumentation(somProperty.Documentation);
			}
			Converter.AddOtherContent(somProperty, edmProperty);
			return edmProperty;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0007A678 File Offset: 0x00078878
		private static NavigationProperty ConvertToNavigationProperty(EntityType declaringEntityType, NavigationProperty somNavigationProperty, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)Converter.LoadSchemaElement(somNavigationProperty.Type, providerManifest, convertedItemCache, newGlobalItems);
			AssociationType associationType = (AssociationType)Converter.LoadSchemaElement((Relationship)somNavigationProperty.Relationship, providerManifest, convertedItemCache, newGlobalItems);
			IRelationshipEnd relationshipEnd = null;
			somNavigationProperty.Relationship.TryGetEnd(somNavigationProperty.ToEnd.Name, out relationshipEnd);
			RelationshipMultiplicity? multiplicity = relationshipEnd.Multiplicity;
			RelationshipMultiplicity relationshipMultiplicity = RelationshipMultiplicity.Many;
			EdmType edmType;
			if (multiplicity.GetValueOrDefault() == relationshipMultiplicity & multiplicity != null)
			{
				edmType = entityType.GetCollectionType();
			}
			else
			{
				edmType = entityType;
			}
			multiplicity = relationshipEnd.Multiplicity;
			relationshipMultiplicity = RelationshipMultiplicity.One;
			TypeUsage typeUsage;
			if (multiplicity.GetValueOrDefault() == relationshipMultiplicity & multiplicity != null)
			{
				typeUsage = TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(false)
				});
			}
			else
			{
				typeUsage = TypeUsage.Create(edmType);
			}
			Converter.InitializeAssociationEndMember(associationType, somNavigationProperty.ToEnd, entityType);
			Converter.InitializeAssociationEndMember(associationType, somNavigationProperty.FromEnd, declaringEntityType);
			NavigationProperty navigationProperty = new NavigationProperty(somNavigationProperty.Name, typeUsage);
			navigationProperty.RelationshipType = associationType;
			navigationProperty.ToEndMember = (RelationshipEndMember)associationType.Members[somNavigationProperty.ToEnd.Name];
			navigationProperty.FromEndMember = (RelationshipEndMember)associationType.Members[somNavigationProperty.FromEnd.Name];
			if (somNavigationProperty.Documentation != null)
			{
				navigationProperty.Documentation = Converter.ConvertToDocumentation(somNavigationProperty.Documentation);
			}
			Converter.AddOtherContent(somNavigationProperty, navigationProperty);
			return navigationProperty;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0007A7E0 File Offset: 0x000789E0
		private static EdmFunction ConvertToFunction(Function somFunction, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, EntityContainer functionImportEntityContainer, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			GlobalItem globalItem = null;
			if (!somFunction.IsFunctionImport && newGlobalItems.TryGetValue(somFunction, out globalItem))
			{
				return (EdmFunction)globalItem;
			}
			bool flag = somFunction.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
			List<FunctionParameter> list = new List<FunctionParameter>();
			if (somFunction.ReturnTypeList != null)
			{
				int num = 0;
				using (IEnumerator<ReturnType> enumerator = somFunction.ReturnTypeList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReturnType returnType2 = enumerator.Current;
						TypeUsage functionTypeUsage = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, returnType2, providerManifest, flag, returnType2.Type, returnType2.CollectionKind, returnType2.IsRefType, somFunction, convertedItemCache, newGlobalItems);
						if (functionTypeUsage == null)
						{
							return null;
						}
						string str = (num == 0) ? string.Empty : num.ToString(CultureInfo.InvariantCulture);
						num++;
						FunctionParameter item = new FunctionParameter("ReturnType" + str, functionTypeUsage, ParameterMode.ReturnValue);
						Converter.AddOtherContent(returnType2, item);
						list.Add(item);
					}
					goto IL_151;
				}
			}
			if (somFunction.Type != null)
			{
				TypeUsage functionTypeUsage2 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, null, providerManifest, flag, somFunction.Type, somFunction.CollectionKind, somFunction.IsReturnAttributeReftype, somFunction, convertedItemCache, newGlobalItems);
				if (functionTypeUsage2 == null)
				{
					return null;
				}
				list.Add(new FunctionParameter("ReturnType", functionTypeUsage2, ParameterMode.ReturnValue));
			}
			IL_151:
			EntitySet[] entitySets = null;
			string namespaceName;
			if (somFunction.IsFunctionImport)
			{
				FunctionImportElement functionImportElement = (FunctionImportElement)somFunction;
				namespaceName = functionImportElement.Container.Name;
				if (functionImportElement.EntitySet != null)
				{
					EntityContainer functionImportEntityContainer2 = functionImportEntityContainer;
					entitySets = new EntitySet[]
					{
						Converter.GetEntitySet(functionImportElement.EntitySet, functionImportEntityContainer2)
					};
				}
				else if (functionImportElement.ReturnTypeList != null)
				{
					EntityContainer functionImportEntityContainer3 = functionImportEntityContainer;
					entitySets = functionImportElement.ReturnTypeList.Select(delegate(ReturnType returnType)
					{
						if (returnType.EntitySet == null)
						{
							return null;
						}
						return Converter.GetEntitySet(returnType.EntitySet, functionImportEntityContainer);
					}).ToArray<EntitySet>();
				}
			}
			else
			{
				namespaceName = somFunction.Namespace;
			}
			List<FunctionParameter> list2 = new List<FunctionParameter>();
			foreach (Parameter parameter in somFunction.Parameters)
			{
				TypeUsage functionTypeUsage3 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, parameter, providerManifest, flag, parameter.Type, parameter.CollectionKind, parameter.IsRefType, parameter, convertedItemCache, newGlobalItems);
				if (functionTypeUsage3 == null)
				{
					return null;
				}
				FunctionParameter functionParameter = new FunctionParameter(parameter.Name, functionTypeUsage3, Converter.GetParameterMode(parameter.ParameterDirection));
				Converter.AddOtherContent(parameter, functionParameter);
				if (parameter.Documentation != null)
				{
					functionParameter.Documentation = Converter.ConvertToDocumentation(parameter.Documentation);
				}
				list2.Add(functionParameter);
			}
			EdmFunction edmFunction = new EdmFunction(somFunction.Name, namespaceName, Converter.GetDataSpace(providerManifest), new EdmFunctionPayload
			{
				Schema = somFunction.DbSchema,
				StoreFunctionName = somFunction.StoreFunctionName,
				CommandText = somFunction.CommandText,
				EntitySets = entitySets,
				IsAggregate = new bool?(somFunction.IsAggregate),
				IsBuiltIn = new bool?(somFunction.IsBuiltIn),
				IsNiladic = new bool?(somFunction.IsNiladicFunction),
				IsComposable = new bool?(somFunction.IsComposable),
				IsFromProviderManifest = new bool?(flag),
				IsFunctionImport = new bool?(somFunction.IsFunctionImport),
				ReturnParameters = list.ToArray(),
				Parameters = list2.ToArray(),
				ParameterTypeSemantics = new ParameterTypeSemantics?(somFunction.ParameterTypeSemantics)
			});
			if (!somFunction.IsFunctionImport)
			{
				newGlobalItems.Add(somFunction, edmFunction);
			}
			if (somFunction.Documentation != null)
			{
				edmFunction.Documentation = Converter.ConvertToDocumentation(somFunction.Documentation);
			}
			Converter.AddOtherContent(somFunction, edmFunction);
			return edmFunction;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0007ABC4 File Offset: 0x00078DC4
		private static EnumType ConvertToEnumType(SchemaEnumType somEnumType, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			ScalarType scalarType = (ScalarType)somEnumType.UnderlyingType;
			EnumType enumType = new EnumType(somEnumType.Name, somEnumType.Namespace, scalarType.Type, somEnumType.IsFlags, DataSpace.CSpace);
			Type clrEquivalentType = scalarType.Type.ClrEquivalentType;
			foreach (SchemaEnumMember schemaEnumMember in somEnumType.EnumMembers)
			{
				EnumMember enumMember = new EnumMember(schemaEnumMember.Name, Convert.ChangeType(schemaEnumMember.Value, clrEquivalentType, CultureInfo.InvariantCulture));
				if (schemaEnumMember.Documentation != null)
				{
					enumMember.Documentation = Converter.ConvertToDocumentation(schemaEnumMember.Documentation);
				}
				Converter.AddOtherContent(schemaEnumMember, enumMember);
				enumType.AddMember(enumMember);
			}
			if (somEnumType.Documentation != null)
			{
				enumType.Documentation = Converter.ConvertToDocumentation(somEnumType.Documentation);
			}
			Converter.AddOtherContent(somEnumType, enumType);
			newGlobalItems.Add(somEnumType, enumType);
			return enumType;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0007ACC0 File Offset: 0x00078EC0
		private static Documentation ConvertToDocumentation(DocumentationElement element)
		{
			return element.MetadataDocumentation;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0007ACC8 File Offset: 0x00078EC8
		private static TypeUsage GetFunctionTypeUsage(bool isModelFunction, Function somFunction, FacetEnabledSchemaElement somParameter, DbProviderManifest providerManifest, bool areConvertingForProviderManifest, SchemaType type, CollectionKind collectionKind, bool isRefType, SchemaElement schemaElement, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (somParameter != null && areConvertingForProviderManifest && somParameter.HasUserDefinedFacets)
			{
				return somParameter.TypeUsage;
			}
			if (type != null)
			{
				EdmType edmType;
				if (!areConvertingForProviderManifest)
				{
					ScalarType scalarType = type as ScalarType;
					if (scalarType != null)
					{
						if (isModelFunction && somParameter != null)
						{
							if (somParameter.TypeUsage == null)
							{
								somParameter.ValidateAndSetTypeUsage(scalarType);
							}
							return somParameter.TypeUsage;
						}
						if (isModelFunction)
						{
							ModelFunction modelFunction = somFunction as ModelFunction;
							if (modelFunction.TypeUsage == null)
							{
								modelFunction.ValidateAndSetTypeUsage(scalarType);
							}
							return modelFunction.TypeUsage;
						}
						if (somParameter != null && somParameter.HasUserDefinedFacets && somFunction.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
						{
							somParameter.ValidateAndSetTypeUsage(scalarType);
							return somParameter.TypeUsage;
						}
						edmType = Converter.GetPrimitiveType(scalarType, providerManifest);
					}
					else
					{
						edmType = (EdmType)Converter.LoadSchemaElement(type, providerManifest, convertedItemCache, newGlobalItems);
						if (isModelFunction && type is SchemaEnumType)
						{
							if (somParameter != null)
							{
								somParameter.ValidateAndSetTypeUsage(edmType);
								return somParameter.TypeUsage;
							}
							if (somFunction != null)
							{
								ModelFunction modelFunction2 = (ModelFunction)somFunction;
								modelFunction2.ValidateAndSetTypeUsage(edmType);
								return modelFunction2.TypeUsage;
							}
						}
					}
				}
				else if (type is TypeElement)
				{
					TypeElement typeElement = type as TypeElement;
					edmType = typeElement.PrimitiveType;
				}
				else
				{
					ScalarType scalarType2 = type as ScalarType;
					edmType = scalarType2.Type;
				}
				TypeUsage result;
				if (collectionKind != CollectionKind.None)
				{
					result = convertedItemCache.GetCollectionTypeUsageWithNullFacets(edmType);
				}
				else if (edmType is EntityType && isRefType)
				{
					result = TypeUsage.Create(new RefType(edmType as EntityType));
				}
				else
				{
					result = convertedItemCache.GetTypeUsageWithNullFacets(edmType);
				}
				return result;
			}
			if (isModelFunction && somParameter != null && somParameter is Parameter)
			{
				((Parameter)somParameter).ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
				return somParameter.TypeUsage;
			}
			if (somParameter != null && somParameter is ReturnType)
			{
				((ReturnType)somParameter).ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
				return somParameter.TypeUsage;
			}
			return null;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0007AE6A File Offset: 0x0007906A
		private static ParameterMode GetParameterMode(ParameterDirection parameterDirection)
		{
			switch (parameterDirection)
			{
			case ParameterDirection.Input:
				return ParameterMode.In;
			case ParameterDirection.Output:
				return ParameterMode.Out;
			}
			return ParameterMode.InOut;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0007AE88 File Offset: 0x00079088
		private static void ApplyTypePropertyFacets(TypeUsage sourceType, ref TypeUsage targetType)
		{
			Dictionary<string, Facet> dictionary = targetType.Facets.ToDictionary((Facet f) => f.Name);
			bool flag = false;
			foreach (Facet facet in sourceType.Facets)
			{
				Facet facet2;
				if (dictionary.TryGetValue(facet.Name, out facet2))
				{
					if (!facet2.Description.IsConstant)
					{
						flag = true;
						dictionary[facet2.Name] = Facet.Create(facet2.Description, facet.Value);
					}
				}
				else
				{
					flag = true;
					dictionary.Add(facet.Name, facet);
				}
			}
			if (flag)
			{
				targetType = TypeUsage.Create(targetType.EdmType, dictionary.Values);
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0007AF6C File Offset: 0x0007916C
		private static void PopulateGeneralFacets(StructuredProperty somProperty, DbProviderManifest providerManifest, ref TypeUsage propertyTypeUsage)
		{
			bool flag = false;
			Dictionary<string, Facet> dictionary = propertyTypeUsage.Facets.ToDictionary((Facet f) => f.Name);
			if (!somProperty.Nullable)
			{
				dictionary["Nullable"] = Facet.Create(MetadataItem.NullableFacetDescription, false);
				flag = true;
			}
			if (somProperty.Default != null)
			{
				dictionary["DefaultValue"] = Facet.Create(MetadataItem.DefaultValueFacetDescription, somProperty.DefaultAsObject);
				flag = true;
			}
			if (somProperty.Schema.SchemaVersion == 1.1)
			{
				Facet facet = Facet.Create(MetadataItem.CollectionKindFacetDescription, somProperty.CollectionKind);
				dictionary.Add(facet.Name, facet);
				flag = true;
			}
			if (flag)
			{
				propertyTypeUsage = TypeUsage.Create(propertyTypeUsage.EdmType, dictionary.Values);
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007B043 File Offset: 0x00079243
		private static DataSpace GetDataSpace(DbProviderManifest providerManifest)
		{
			if (providerManifest is EdmProviderManifest)
			{
				return DataSpace.CSpace;
			}
			return DataSpace.SSpace;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0007B050 File Offset: 0x00079250
		private static PrimitiveType GetPrimitiveType(ScalarType scalarType, DbProviderManifest providerManifest)
		{
			PrimitiveType result = null;
			string name = scalarType.Name;
			foreach (PrimitiveType primitiveType in providerManifest.GetStoreTypes())
			{
				if (primitiveType.Name == name)
				{
					result = primitiveType;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0007B0B4 File Offset: 0x000792B4
		private static void UpdateSentinelValuesInFacets(ref TypeUsage typeUsage)
		{
			PrimitiveType primitiveType = (PrimitiveType)typeUsage.EdmType;
			if (primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.String || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary)
			{
				Facet facet = typeUsage.Facets["MaxLength"];
				if (Helper.IsUnboundedFacetValue(facet))
				{
					typeUsage = typeUsage.ShallowCopy(new FacetValues
					{
						MaxLength = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength").MaxValue
					});
				}
			}
		}

		// Token: 0x04000EF9 RID: 3833
		internal static readonly FacetDescription ConcurrencyModeFacet;

		// Token: 0x04000EFA RID: 3834
		internal static readonly FacetDescription StoreGeneratedPatternFacet;

		// Token: 0x04000EFB RID: 3835
		internal static readonly FacetDescription CollationFacet;

		// Token: 0x02000538 RID: 1336
		internal class ConversionCache
		{
			// Token: 0x06003E9D RID: 16029 RVA: 0x000E8CD4 File Offset: 0x000E6ED4
			internal ConversionCache(ItemCollection itemCollection)
			{
				this.ItemCollection = itemCollection;
				this._nullFacetsTypeUsage = new Dictionary<EdmType, TypeUsage>();
				this._nullFacetsCollectionTypeUsage = new Dictionary<EdmType, TypeUsage>();
			}

			// Token: 0x06003E9E RID: 16030 RVA: 0x000E8CFC File Offset: 0x000E6EFC
			internal TypeUsage GetTypeUsageWithNullFacets(EdmType edmType)
			{
				TypeUsage typeUsage;
				if (this._nullFacetsTypeUsage.TryGetValue(edmType, out typeUsage))
				{
					return typeUsage;
				}
				typeUsage = TypeUsage.Create(edmType, FacetValues.NullFacetValues);
				this._nullFacetsTypeUsage.Add(edmType, typeUsage);
				return typeUsage;
			}

			// Token: 0x06003E9F RID: 16031 RVA: 0x000E8D38 File Offset: 0x000E6F38
			internal TypeUsage GetCollectionTypeUsageWithNullFacets(EdmType edmType)
			{
				TypeUsage typeUsage;
				if (this._nullFacetsCollectionTypeUsage.TryGetValue(edmType, out typeUsage))
				{
					return typeUsage;
				}
				TypeUsage typeUsageWithNullFacets = this.GetTypeUsageWithNullFacets(edmType);
				typeUsage = TypeUsage.Create(new CollectionType(typeUsageWithNullFacets), FacetValues.NullFacetValues);
				this._nullFacetsCollectionTypeUsage.Add(edmType, typeUsage);
				return typeUsage;
			}

			// Token: 0x04001B97 RID: 7063
			internal readonly ItemCollection ItemCollection;

			// Token: 0x04001B98 RID: 7064
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsTypeUsage;

			// Token: 0x04001B99 RID: 7065
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsCollectionTypeUsage;
		}
	}
}
