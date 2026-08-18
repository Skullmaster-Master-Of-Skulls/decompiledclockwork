using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B2 RID: 1202
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal static class Converter
	{
		// Token: 0x06002C4D RID: 11341 RVA: 0x000D7694 File Offset: 0x000D5894
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
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

		// Token: 0x06002C4E RID: 11342 RVA: 0x000D7800 File Offset: 0x000D5A00
		internal static IEnumerable<GlobalItem> ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			Dictionary<SchemaElement, GlobalItem> dictionary = new Dictionary<SchemaElement, GlobalItem>();
			Converter.ConvertSchema(somSchema, providerManifest, new Converter.ConversionCache(itemCollection), dictionary);
			return dictionary.Values;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000D7828 File Offset: 0x000D5A28
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

		// Token: 0x06002C50 RID: 11344 RVA: 0x000D7888 File Offset: 0x000D5A88
		private static void ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			List<Function> list = new List<Function>();
			foreach (SchemaType schemaType in somSchema.SchemaTypes)
			{
				if (Converter.LoadSchemaElement(schemaType, providerManifest, convertedItemCache, newGlobalItems) == null)
				{
					Function function = schemaType as Function;
					if (function != null)
					{
						list.Add(function);
					}
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

		// Token: 0x06002C51 RID: 11345 RVA: 0x000D79CC File Offset: 0x000D5BCC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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

		// Token: 0x06002C52 RID: 11346 RVA: 0x000D7A7C File Offset: 0x000D5C7C
		private static EntityContainer ConvertToEntityContainer(EntityContainer element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityContainer entityContainer = new EntityContainer(element.Name, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, entityContainer);
			foreach (EntityContainerEntitySet set in element.EntitySets)
			{
				entityContainer.AddEntitySetBase(Converter.ConvertToEntitySet(set, providerManifest, convertedItemCache, newGlobalItems));
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

		// Token: 0x06002C53 RID: 11347 RVA: 0x000D7BA8 File Offset: 0x000D5DA8
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

		// Token: 0x06002C54 RID: 11348 RVA: 0x000D7CD8 File Offset: 0x000D5ED8
		private static void LoadEntityTypePhase2(SchemaEntityType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)newGlobalItems[element];
			foreach (NavigationProperty somNavigationProperty in element.NavigationProperties)
			{
				entityType.AddMember(Converter.ConvertToNavigationProperty(entityType, somNavigationProperty, providerManifest, convertedItemCache, newGlobalItems));
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000D7D3C File Offset: 0x000D5F3C
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

		// Token: 0x06002C56 RID: 11350 RVA: 0x000D7E00 File Offset: 0x000D6000
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
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
						switch (onOperation.Action)
						{
						case System.Data.Entity.Core.SchemaObjectModel.Action.None:
							deleteBehavior = OperationAction.None;
							break;
						case System.Data.Entity.Core.SchemaObjectModel.Action.Cascade:
							deleteBehavior = OperationAction.Cascade;
							break;
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

		// Token: 0x06002C57 RID: 11351 RVA: 0x000D80AC File Offset: 0x000D62AC
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

		// Token: 0x06002C58 RID: 11352 RVA: 0x000D8124 File Offset: 0x000D6324
		private static EdmProperty[] GetProperties(EntityTypeBase entityType, IList<PropertyRefElement> properties)
		{
			EdmProperty[] array = new EdmProperty[properties.Count];
			for (int i = 0; i < properties.Count; i++)
			{
				array[i] = (EdmProperty)entityType.Members[properties[i].Name];
			}
			return array;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000D816E File Offset: 0x000D636E
		private static void AddOtherContent(SchemaElement element, MetadataItem item)
		{
			if (element.OtherContent.Count > 0)
			{
				item.AddMetadataProperties(element.OtherContent);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000D818C File Offset: 0x000D638C
		private static EntitySet ConvertToEntitySet(EntityContainerEntitySet set, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntitySet entitySet = new EntitySet(set.Name, set.DbSchema, set.Table, set.DefiningQuery, (EntityType)Converter.LoadSchemaElement(set.EntityType, providerManifest, convertedItemCache, newGlobalItems));
			if (set.Documentation != null)
			{
				entitySet.Documentation = Converter.ConvertToDocumentation(set.Documentation);
			}
			Converter.AddOtherContent(set, entitySet);
			return entitySet;
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000D81EB File Offset: 0x000D63EB
		private static EntitySet GetEntitySet(EntityContainerEntitySet set, EntityContainer container)
		{
			return container.GetEntitySetByName(set.Name, false);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000D81FC File Offset: 0x000D63FC
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

		// Token: 0x06002C5D RID: 11357 RVA: 0x000D8304 File Offset: 0x000D6504
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
			Converter.PopulateGeneralFacets(somProperty, ref typeUsage);
			EdmProperty edmProperty = new EdmProperty(somProperty.Name, typeUsage);
			if (somProperty.Documentation != null)
			{
				edmProperty.Documentation = Converter.ConvertToDocumentation(somProperty.Documentation);
			}
			Converter.AddOtherContent(somProperty, edmProperty);
			return edmProperty;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000D8400 File Offset: 0x000D6600
		private static NavigationProperty ConvertToNavigationProperty(EntityType declaringEntityType, NavigationProperty somNavigationProperty, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)Converter.LoadSchemaElement(somNavigationProperty.Type, providerManifest, convertedItemCache, newGlobalItems);
			AssociationType associationType = (AssociationType)Converter.LoadSchemaElement((Relationship)somNavigationProperty.Relationship, providerManifest, convertedItemCache, newGlobalItems);
			IRelationshipEnd relationshipEnd = null;
			somNavigationProperty.Relationship.TryGetEnd(somNavigationProperty.ToEnd.Name, out relationshipEnd);
			EdmType edmType;
			if (relationshipEnd.Multiplicity == RelationshipMultiplicity.Many)
			{
				edmType = entityType.GetCollectionType();
			}
			else
			{
				edmType = entityType;
			}
			TypeUsage typeUsage;
			if (relationshipEnd.Multiplicity == RelationshipMultiplicity.One)
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

		// Token: 0x06002C5F RID: 11359 RVA: 0x000D8590 File Offset: 0x000D6790
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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
						TypeUsage functionTypeUsage = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, returnType2, providerManifest, flag, returnType2.Type, returnType2.CollectionKind, returnType2.IsRefType, convertedItemCache, newGlobalItems);
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
					goto IL_150;
				}
			}
			if (somFunction.Type != null)
			{
				TypeUsage functionTypeUsage2 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, null, providerManifest, flag, somFunction.Type, somFunction.CollectionKind, somFunction.IsReturnAttributeReftype, convertedItemCache, newGlobalItems);
				if (functionTypeUsage2 == null)
				{
					return null;
				}
				list.Add(new FunctionParameter("ReturnType", functionTypeUsage2, ParameterMode.ReturnValue));
			}
			IL_150:
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
				TypeUsage functionTypeUsage3 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, parameter, providerManifest, flag, parameter.Type, parameter.CollectionKind, parameter.IsRefType, convertedItemCache, newGlobalItems);
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

		// Token: 0x06002C60 RID: 11360 RVA: 0x000D897C File Offset: 0x000D6B7C
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

		// Token: 0x06002C61 RID: 11361 RVA: 0x000D8A74 File Offset: 0x000D6C74
		private static Documentation ConvertToDocumentation(DocumentationElement element)
		{
			return element.MetadataDocumentation;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000D8A7C File Offset: 0x000D6C7C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static TypeUsage GetFunctionTypeUsage(bool isModelFunction, Function somFunction, FacetEnabledSchemaElement somParameter, DbProviderManifest providerManifest, bool areConvertingForProviderManifest, SchemaType type, CollectionKind collectionKind, bool isRefType, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
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
				else
				{
					EntityType entityType = edmType as EntityType;
					if (entityType != null && isRefType)
					{
						result = TypeUsage.Create(new RefType(entityType));
					}
					else
					{
						result = convertedItemCache.GetTypeUsageWithNullFacets(edmType);
					}
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

		// Token: 0x06002C63 RID: 11363 RVA: 0x000D8C1C File Offset: 0x000D6E1C
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

		// Token: 0x06002C64 RID: 11364 RVA: 0x000D8C50 File Offset: 0x000D6E50
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

		// Token: 0x06002C65 RID: 11365 RVA: 0x000D8D38 File Offset: 0x000D6F38
		private static void PopulateGeneralFacets(StructuredProperty somProperty, ref TypeUsage propertyTypeUsage)
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

		// Token: 0x06002C66 RID: 11366 RVA: 0x000D8E0D File Offset: 0x000D700D
		private static DataSpace GetDataSpace(DbProviderManifest providerManifest)
		{
			if (providerManifest is EdmProviderManifest)
			{
				return DataSpace.CSpace;
			}
			return DataSpace.SSpace;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000D8E1C File Offset: 0x000D701C
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

		// Token: 0x06002C68 RID: 11368 RVA: 0x000D8E80 File Offset: 0x000D7080
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

		// Token: 0x04001056 RID: 4182
		internal static readonly FacetDescription ConcurrencyModeFacet;

		// Token: 0x04001057 RID: 4183
		internal static readonly FacetDescription StoreGeneratedPatternFacet;

		// Token: 0x04001058 RID: 4184
		internal static readonly FacetDescription CollationFacet;

		// Token: 0x020004B3 RID: 1203
		internal class ConversionCache
		{
			// Token: 0x06002C6B RID: 11371 RVA: 0x000D8EF6 File Offset: 0x000D70F6
			internal ConversionCache(ItemCollection itemCollection)
			{
				this.ItemCollection = itemCollection;
				this._nullFacetsTypeUsage = new Dictionary<EdmType, TypeUsage>();
				this._nullFacetsCollectionTypeUsage = new Dictionary<EdmType, TypeUsage>();
			}

			// Token: 0x06002C6C RID: 11372 RVA: 0x000D8F1C File Offset: 0x000D711C
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

			// Token: 0x06002C6D RID: 11373 RVA: 0x000D8F58 File Offset: 0x000D7158
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

			// Token: 0x0400105B RID: 4187
			internal readonly ItemCollection ItemCollection;

			// Token: 0x0400105C RID: 4188
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsTypeUsage;

			// Token: 0x0400105D RID: 4189
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsCollectionTypeUsage;
		}
	}
}
