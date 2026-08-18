using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000020 RID: 32
	[SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
	internal static class EdmModelSemanticValidationRules
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00005365 File Offset: 0x00003565
		private static string GetQualifiedName(INamedDataModelItem item, string qualifiedPrefix)
		{
			return qualifiedPrefix + "." + item.Name;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005378 File Offset: 0x00003578
		private static bool AreRelationshipEndsEqual(KeyValuePair<AssociationSet, EntitySet> left, KeyValuePair<AssociationSet, EntitySet> right)
		{
			return object.ReferenceEquals(left.Value, right.Value) && object.ReferenceEquals(left.Key.ElementType, right.Key.ElementType);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000053D4 File Offset: 0x000035D4
		private static bool IsReferentialConstraintReadyForValidation(AssociationType association)
		{
			ReferentialConstraint constraint = association.Constraint;
			if (constraint == null)
			{
				return false;
			}
			if (constraint.FromRole == null || constraint.ToRole == null)
			{
				return false;
			}
			if (constraint.FromRole.GetEntityType() == null || constraint.ToRole.GetEntityType() == null)
			{
				return false;
			}
			if (constraint.ToProperties.Any<EdmProperty>())
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = constraint.ToProperties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmProperty edmProperty = enumerator.Current;
						if (edmProperty == null)
						{
							return false;
						}
						if (edmProperty.TypeUsage == null || edmProperty.TypeUsage.EdmType == null)
						{
							return false;
						}
					}
					goto IL_99;
				}
				return false;
				IL_99:
				IEnumerable<EdmProperty> validKey = constraint.FromRole.GetEntityType().GetValidKey();
				if (validKey.Any<EdmProperty>())
				{
					return validKey.All((EdmProperty propRef) => propRef != null && propRef.TypeUsage != null && propRef.TypeUsage.EdmType != null);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000054CC File Offset: 0x000036CC
		private static void IsKeyProperty(List<EdmProperty> roleProperties, RelationshipEndMember roleElement, out bool isKeyProperty, out bool areAllPropertiesNullable, out bool isAnyPropertyNullable, out bool isSubsetOfKeyProperties)
		{
			isKeyProperty = true;
			areAllPropertiesNullable = true;
			isAnyPropertyNullable = false;
			isSubsetOfKeyProperties = true;
			if (roleElement.GetEntityType().GetValidKey().Count<EdmProperty>() != roleProperties.Count<EdmProperty>())
			{
				isKeyProperty = false;
			}
			for (int i = 0; i < roleProperties.Count<EdmProperty>(); i++)
			{
				if (isSubsetOfKeyProperties)
				{
					List<EdmProperty> list = roleElement.GetEntityType().GetValidKey().ToList<EdmProperty>();
					if (!list.Contains(roleProperties[i]))
					{
						isKeyProperty = false;
						isSubsetOfKeyProperties = false;
					}
				}
				bool nullable = roleProperties[i].Nullable;
				areAllPropertiesNullable = (areAllPropertiesNullable && nullable);
				isAnyPropertyNullable = (isAnyPropertyNullable || nullable);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000555B File Offset: 0x0000375B
		private static void AddMemberNameToHashSet(INamedDataModelItem item, HashSet<string> memberNameList, EdmModelValidationContext context, Func<string, string> getErrorString)
		{
			if (!string.IsNullOrWhiteSpace(item.Name) && !memberNameList.Add(item.Name))
			{
				context.AddError((MetadataItem)item, "Name", getErrorString(item.Name));
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005598 File Offset: 0x00003798
		private static bool CheckForInheritanceCycle<T>(T type, Func<T, T> getBaseType) where T : class
		{
			T t = getBaseType(type);
			if (t != null)
			{
				T t2 = t;
				T t3 = t;
				for (;;)
				{
					t3 = getBaseType(t3);
					if (object.ReferenceEquals(t2, t3))
					{
						break;
					}
					if (t2 == null)
					{
						return false;
					}
					t2 = getBaseType(t2);
					if (t3 != null)
					{
						t3 = getBaseType(t3);
					}
					if (t3 == null)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005601 File Offset: 0x00003801
		private static bool IsPrimitiveTypesEqual(EdmProperty primitiveType1, EdmProperty primitiveType2)
		{
			return primitiveType1.PrimitiveType.PrimitiveTypeKind == primitiveType2.PrimitiveType.PrimitiveTypeKind;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000561B File Offset: 0x0000381B
		private static bool IsEdmSystemNamespace(string namespaceName)
		{
			return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System";
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000566C File Offset: 0x0000386C
		private static bool IsTypeDefinesNewConcurrencyProperties(EntityType entityType)
		{
			return (from property in entityType.DeclaredProperties
			where property.TypeUsage != null
			select property).Any((EdmProperty property) => property.PrimitiveType != null && property.ConcurrencyMode != ConcurrencyMode.None);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000056C4 File Offset: 0x000038C4
		private static bool TypeIsSubTypeOf(EntityType entityType, Dictionary<EntityType, EntitySet> baseEntitySetTypes, out EntitySet set)
		{
			if (entityType.IsTypeHierarchyRoot())
			{
				set = null;
				return false;
			}
			foreach (EntityType key in entityType.ToHierarchy())
			{
				if (baseEntitySetTypes.ContainsKey(key))
				{
					set = baseEntitySetTypes[key];
					return true;
				}
			}
			set = null;
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005734 File Offset: 0x00003934
		private static bool IsTypeHierarchyRoot(this EntityType entityType)
		{
			return entityType.BaseType == null;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000573F File Offset: 0x0000393F
		private static bool IsForeignKey(this AssociationType association, double version)
		{
			return version >= 2.0 && association.Constraint != null;
		}

		// Token: 0x04000036 RID: 54
		internal static readonly EdmModelValidationRule<EdmFunction> EdmFunction_ComposableFunctionImportsNotAllowed_V1_V2 = new EdmModelValidationRule<EdmFunction>(delegate(EdmModelValidationContext context, EdmFunction function)
		{
			if (function.IsFunctionImport && function.IsComposableAttribute)
			{
				context.AddError(function, null, Strings.EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion);
			}
		});

		// Token: 0x04000037 RID: 55
		internal static readonly EdmModelValidationRule<EdmFunction> EdmFunction_DuplicateParameterName = new EdmModelValidationRule<EdmFunction>(delegate(EdmModelValidationContext context, EdmFunction function)
		{
			HashSet<string> memberNameList = new HashSet<string>();
			foreach (FunctionParameter functionParameter in function.Parameters)
			{
				if (functionParameter != null && !string.IsNullOrWhiteSpace(functionParameter.Name))
				{
					EdmModelSemanticValidationRules.AddMemberNameToHashSet(functionParameter, memberNameList, context, new Func<string, string>(Strings.ParameterNameAlreadyDefinedDuplicate));
				}
			}
		});

		// Token: 0x04000038 RID: 56
		internal static readonly EdmModelValidationRule<EdmType> EdmType_SystemNamespaceEncountered = new EdmModelValidationRule<EdmType>(delegate(EdmModelValidationContext context, EdmType edmType)
		{
			if (EdmModelSemanticValidationRules.IsEdmSystemNamespace(edmType.NamespaceName) && edmType.BuiltInTypeKind != BuiltInTypeKind.RowType && edmType.BuiltInTypeKind != BuiltInTypeKind.CollectionType && edmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				context.AddError(edmType, null, Strings.EdmModel_Validator_Semantic_SystemNamespaceEncountered(edmType.Name));
			}
		});

		// Token: 0x04000039 RID: 57
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_SimilarRelationshipEnd = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			List<KeyValuePair<AssociationSet, EntitySet>> list = new List<KeyValuePair<AssociationSet, EntitySet>>();
			List<KeyValuePair<AssociationSet, EntitySet>> list2 = new List<KeyValuePair<AssociationSet, EntitySet>>();
			foreach (AssociationSet associationSet in edmEntityContainer.AssociationSets)
			{
				KeyValuePair<AssociationSet, EntitySet> sourceEnd = new KeyValuePair<AssociationSet, EntitySet>(associationSet, associationSet.SourceSet);
				KeyValuePair<AssociationSet, EntitySet> targetEnd = new KeyValuePair<AssociationSet, EntitySet>(associationSet, associationSet.TargetSet);
				KeyValuePair<AssociationSet, EntitySet> keyValuePair = list.FirstOrDefault((KeyValuePair<AssociationSet, EntitySet> e) => EdmModelSemanticValidationRules.AreRelationshipEndsEqual(e, sourceEnd));
				KeyValuePair<AssociationSet, EntitySet> keyValuePair2 = list2.FirstOrDefault((KeyValuePair<AssociationSet, EntitySet> e) => EdmModelSemanticValidationRules.AreRelationshipEndsEqual(e, targetEnd));
				if (!keyValuePair.Equals(default(KeyValuePair<AssociationSet, EntitySet>)))
				{
					context.AddError(edmEntityContainer, null, Strings.EdmModel_Validator_Semantic_SimilarRelationshipEnd(keyValuePair.Key.ElementType.SourceEnd.Name, keyValuePair.Key.Name, associationSet.Name, keyValuePair.Value.Name, edmEntityContainer.Name));
				}
				else
				{
					list.Add(sourceEnd);
				}
				if (!keyValuePair2.Equals(default(KeyValuePair<AssociationSet, EntitySet>)))
				{
					context.AddError(edmEntityContainer, null, Strings.EdmModel_Validator_Semantic_SimilarRelationshipEnd(keyValuePair2.Key.ElementType.TargetEnd.Name, keyValuePair2.Key.Name, associationSet.Name, keyValuePair2.Value.Name, edmEntityContainer.Name));
				}
				else
				{
					list2.Add(targetEnd);
				}
			}
		});

		// Token: 0x0400003A RID: 58
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_InvalidEntitySetNameReference = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			if (edmEntityContainer.AssociationSets != null)
			{
				foreach (AssociationSet associationSet in edmEntityContainer.AssociationSets)
				{
					if (associationSet.SourceSet != null && associationSet.ElementType != null && associationSet.ElementType.SourceEnd != null && !edmEntityContainer.EntitySets.Contains(associationSet.SourceSet))
					{
						context.AddError(associationSet.SourceSet, null, Strings.EdmModel_Validator_Semantic_InvalidEntitySetNameReference(associationSet.SourceSet.Name, associationSet.ElementType.SourceEnd.Name));
					}
					if (associationSet.TargetSet != null && associationSet.ElementType != null && associationSet.ElementType.TargetEnd != null && !edmEntityContainer.EntitySets.Contains(associationSet.TargetSet))
					{
						context.AddError(associationSet.TargetSet, null, Strings.EdmModel_Validator_Semantic_InvalidEntitySetNameReference(associationSet.TargetSet.Name, associationSet.ElementType.TargetEnd.Name));
					}
				}
			}
		});

		// Token: 0x0400003B RID: 59
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_ConcurrencyRedefinedOnSubTypeOfEntitySetType = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			Dictionary<EntityType, EntitySet> dictionary = new Dictionary<EntityType, EntitySet>();
			foreach (EntitySet entitySet in edmEntityContainer.EntitySets)
			{
				if (entitySet != null && entitySet.ElementType != null && !dictionary.ContainsKey(entitySet.ElementType))
				{
					dictionary.Add(entitySet.ElementType, entitySet);
				}
			}
			foreach (EntityType entityType in context.Model.EntityTypes)
			{
				EntitySet entitySet2;
				if (EdmModelSemanticValidationRules.TypeIsSubTypeOf(entityType, dictionary, out entitySet2) && EdmModelSemanticValidationRules.IsTypeDefinesNewConcurrencyProperties(entityType))
				{
					context.AddError(entityType, null, Strings.EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType(EdmModelSemanticValidationRules.GetQualifiedName(entityType, entityType.NamespaceName), EdmModelSemanticValidationRules.GetQualifiedName(entitySet2.ElementType, entitySet2.ElementType.NamespaceName), EdmModelSemanticValidationRules.GetQualifiedName(entitySet2, entitySet2.EntityContainer.Name)));
				}
			}
		});

		// Token: 0x0400003C RID: 60
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_DuplicateEntityContainerMemberName = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			HashSet<string> memberNameList = new HashSet<string>();
			foreach (EntitySetBase item in edmEntityContainer.BaseEntitySets)
			{
				EdmModelSemanticValidationRules.AddMemberNameToHashSet(item, memberNameList, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName));
			}
		});

		// Token: 0x0400003D RID: 61
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_DuplicateEntitySetTable = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (EntitySetBase entitySetBase in edmEntityContainer.BaseEntitySets)
			{
				if (!string.IsNullOrWhiteSpace(entitySetBase.Table) && !hashSet.Add(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					entitySetBase.Schema,
					entitySetBase.Table
				})))
				{
					context.AddError(entitySetBase, "Name", Strings.DuplicateEntitySetTable(entitySetBase.Name, entitySetBase.Schema, entitySetBase.Table));
				}
			}
		});

		// Token: 0x0400003E RID: 62
		internal static readonly EdmModelValidationRule<EntitySet> EdmEntitySet_EntitySetTypeHasNoKeys = new EdmModelValidationRule<EntitySet>(delegate(EdmModelValidationContext context, EntitySet edmEntitySet)
		{
			if (edmEntitySet.ElementType != null && !edmEntitySet.ElementType.GetValidKey().Any<EdmProperty>())
			{
				context.AddError(edmEntitySet, "EntityType", Strings.EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys(edmEntitySet.Name, edmEntitySet.ElementType.Name));
			}
		});

		// Token: 0x0400003F RID: 63
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_DuplicateEndName = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (edmAssociationSet.ElementType != null && edmAssociationSet.ElementType.SourceEnd != null && edmAssociationSet.ElementType.TargetEnd != null && edmAssociationSet.ElementType.SourceEnd.Name == edmAssociationSet.ElementType.TargetEnd.Name)
			{
				context.AddError(edmAssociationSet.SourceSet, "Name", Strings.EdmModel_Validator_Semantic_DuplicateEndName(edmAssociationSet.ElementType.SourceEnd.Name));
			}
		});

		// Token: 0x04000040 RID: 64
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_DuplicatePropertyNameSpecifiedInEntityKey = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			List<EdmProperty> list = edmEntityType.GetKeyProperties().ToList<EdmProperty>();
			if (list.Count > 0)
			{
				List<EdmProperty> list2 = new List<EdmProperty>();
				using (List<EdmProperty>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmProperty key = enumerator.Current;
						if (key != null && !list2.Contains(key))
						{
							if (list.Count((EdmProperty p) => key.Equals(p)) > 1)
							{
								context.AddError(key, null, Strings.EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(edmEntityType.Name, key.Name));
							}
							list2.Add(key);
						}
					}
				}
			}
		});

		// Token: 0x04000041 RID: 65
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_InvalidKeyNullablePart = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			foreach (EdmProperty edmProperty in edmEntityType.GetValidKey())
			{
				if (edmProperty.IsPrimitiveType && edmProperty.Nullable)
				{
					context.AddError(edmProperty, "Nullable", Strings.EdmModel_Validator_Semantic_InvalidKeyNullablePart(edmProperty.Name, edmEntityType.Name));
				}
			}
		});

		// Token: 0x04000042 RID: 66
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_EntityKeyMustBeScalar = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			foreach (EdmProperty edmProperty in edmEntityType.GetValidKey())
			{
				if (!edmProperty.IsUnderlyingPrimitiveType)
				{
					context.AddError(edmProperty, null, Strings.EdmModel_Validator_Semantic_EntityKeyMustBeScalar(edmEntityType.Name, edmProperty.Name));
				}
			}
		});

		// Token: 0x04000043 RID: 67
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_InvalidKeyKeyDefinedInBaseClass = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			if (edmEntityType.BaseType != null && (from key in edmEntityType.KeyProperties
			where edmEntityType.DeclaredMembers.Contains(key)
			select key).Any<EdmProperty>())
			{
				context.AddError(edmEntityType.BaseType, null, Strings.EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(edmEntityType.Name, edmEntityType.BaseType.Name));
			}
		});

		// Token: 0x04000044 RID: 68
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_KeyMissingOnEntityType = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			if (edmEntityType.BaseType == null && edmEntityType.KeyProperties.Count == 0)
			{
				context.AddError(edmEntityType, null, Strings.EdmModel_Validator_Semantic_KeyMissingOnEntityType(edmEntityType.Name));
			}
		});

		// Token: 0x04000045 RID: 69
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_InvalidMemberNameMatchesTypeName = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			List<EdmProperty> list = edmEntityType.Properties.ToList<EdmProperty>();
			if (!string.IsNullOrWhiteSpace(edmEntityType.Name) && list.Count > 0)
			{
				foreach (EdmProperty edmProperty in list)
				{
					if (edmProperty != null && context.IsCSpace && edmProperty.Name.EqualsOrdinal(edmEntityType.Name))
					{
						context.AddError(edmProperty, "Name", Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty.Name, EdmModelSemanticValidationRules.GetQualifiedName(edmEntityType, edmEntityType.NamespaceName)));
					}
				}
				if (edmEntityType.DeclaredNavigationProperties.Any<NavigationProperty>())
				{
					foreach (NavigationProperty navigationProperty in edmEntityType.DeclaredNavigationProperties)
					{
						if (navigationProperty != null && navigationProperty.Name.EqualsOrdinal(edmEntityType.Name))
						{
							context.AddError(navigationProperty, "Name", Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(navigationProperty.Name, EdmModelSemanticValidationRules.GetQualifiedName(edmEntityType, edmEntityType.NamespaceName)));
						}
					}
				}
			}
		});

		// Token: 0x04000046 RID: 70
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_PropertyNameAlreadyDefinedDuplicate = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			HashSet<string> memberNameList = new HashSet<string>();
			foreach (EdmProperty edmProperty in edmEntityType.Properties)
			{
				if (edmProperty != null && !string.IsNullOrWhiteSpace(edmProperty.Name))
				{
					EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmProperty, memberNameList, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
				}
			}
			if (edmEntityType.DeclaredNavigationProperties.Any<NavigationProperty>())
			{
				foreach (NavigationProperty navigationProperty in edmEntityType.DeclaredNavigationProperties)
				{
					if (navigationProperty != null && !string.IsNullOrWhiteSpace(navigationProperty.Name))
					{
						EdmModelSemanticValidationRules.AddMemberNameToHashSet(navigationProperty, memberNameList, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
					}
				}
			}
		});

		// Token: 0x04000047 RID: 71
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_CycleInTypeHierarchy = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			if (EdmModelSemanticValidationRules.CheckForInheritanceCycle<EntityType>(edmEntityType, (EntityType et) => (EntityType)et.BaseType))
			{
				context.AddError(edmEntityType, "BaseType", Strings.EdmModel_Validator_Semantic_CycleInTypeHierarchy(EdmModelSemanticValidationRules.GetQualifiedName(edmEntityType, edmEntityType.NamespaceName)));
			}
		});

		// Token: 0x04000048 RID: 72
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_BadNavigationPropertyUndefinedRole = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.Association != null && edmNavigationProperty.Association.SourceEnd != null && edmNavigationProperty.Association.TargetEnd != null && edmNavigationProperty.Association.SourceEnd.Name != null && edmNavigationProperty.Association.TargetEnd.Name != null && edmNavigationProperty.ToEndMember != edmNavigationProperty.Association.SourceEnd && edmNavigationProperty.ToEndMember != edmNavigationProperty.Association.TargetEnd)
			{
				context.AddError(edmNavigationProperty, null, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(edmNavigationProperty.Association.SourceEnd.Name, edmNavigationProperty.Association.TargetEnd.Name, edmNavigationProperty.Association.Name));
			}
		});

		// Token: 0x04000049 RID: 73
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_BadNavigationPropertyRolesCannotBeTheSame = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.Association != null && edmNavigationProperty.Association.SourceEnd != null && edmNavigationProperty.Association.TargetEnd != null && edmNavigationProperty.ToEndMember == edmNavigationProperty.GetFromEnd())
			{
				context.AddError(edmNavigationProperty, "ToRole", Strings.EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame);
			}
		});

		// Token: 0x0400004A RID: 74
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_BadNavigationPropertyBadFromRoleType = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			AssociationEndMember fromEnd;
			if (edmNavigationProperty.Association != null && (fromEnd = edmNavigationProperty.GetFromEnd()) != null)
			{
				EntityType entityType = null;
				IList<EntityType> list = (context.Model.EntityTypes as IList<EntityType>) ?? context.Model.EntityTypes.ToList<EntityType>();
				for (int i = 0; i < list.Count; i++)
				{
					EntityType entityType2 = list[i];
					ReadOnlyMetadataCollection<NavigationProperty> declaredNavigationProperties = entityType2.DeclaredNavigationProperties;
					if (declaredNavigationProperties.Contains(edmNavigationProperty))
					{
						entityType = entityType2;
						break;
					}
				}
				EntityType entityType3 = fromEnd.GetEntityType();
				if (entityType != entityType3)
				{
					context.AddError(edmNavigationProperty, "FromRole", Strings.BadNavigationPropertyBadFromRoleType(edmNavigationProperty.Name, entityType3.Name, fromEnd.Name, edmNavigationProperty.Association.Name, entityType.Name));
				}
			}
		});

		// Token: 0x0400004B RID: 75
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_InvalidOperationMultipleEndsInAssociation = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd != null && edmAssociationType.SourceEnd.DeleteBehavior != OperationAction.None && edmAssociationType.TargetEnd != null && edmAssociationType.TargetEnd.DeleteBehavior != OperationAction.None)
			{
				context.AddError(edmAssociationType, null, Strings.EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation);
			}
		});

		// Token: 0x0400004C RID: 76
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_EndWithManyMultiplicityCannotHaveOperationsSpecified = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd != null && edmAssociationType.SourceEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && edmAssociationType.SourceEnd.DeleteBehavior != OperationAction.None)
			{
				context.AddError(edmAssociationType.SourceEnd, "OnDelete", Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(edmAssociationType.SourceEnd.Name, edmAssociationType.Name));
			}
			if (edmAssociationType.TargetEnd != null && edmAssociationType.TargetEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && edmAssociationType.TargetEnd.DeleteBehavior != OperationAction.None)
			{
				context.AddError(edmAssociationType.TargetEnd, "OnDelete", Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(edmAssociationType.TargetEnd.Name, edmAssociationType.Name));
			}
		});

		// Token: 0x0400004D RID: 77
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_EndNameAlreadyDefinedDuplicate = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd != null && edmAssociationType.TargetEnd != null && edmAssociationType.SourceEnd.Name == edmAssociationType.TargetEnd.Name)
			{
				context.AddError(edmAssociationType.SourceEnd, "Name", Strings.EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(edmAssociationType.SourceEnd.Name));
			}
		});

		// Token: 0x0400004E RID: 78
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_SameRoleReferredInReferentialConstraint = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (EdmModelSemanticValidationRules.IsReferentialConstraintReadyForValidation(edmAssociationType) && edmAssociationType.Constraint.FromRole.Name == edmAssociationType.Constraint.ToRole.Name)
			{
				context.AddError(edmAssociationType.Constraint.ToRole, null, Strings.EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(edmAssociationType.Name));
			}
		});

		// Token: 0x0400004F RID: 79
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_ValidateReferentialConstraint = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (EdmModelSemanticValidationRules.IsReferentialConstraintReadyForValidation(edmAssociationType))
			{
				ReferentialConstraint constraint = edmAssociationType.Constraint;
				RelationshipEndMember fromRole = constraint.FromRole;
				RelationshipEndMember toRole = constraint.ToRole;
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				EdmModelSemanticValidationRules.IsKeyProperty(constraint.ToProperties.ToList<EdmProperty>(), toRole, out flag, out flag2, out flag3, out flag4);
				bool flag5;
				bool flag6;
				bool flag7;
				bool flag8;
				EdmModelSemanticValidationRules.IsKeyProperty(constraint.FromRole.GetEntityType().GetValidKey().ToList<EdmProperty>(), fromRole, out flag5, out flag6, out flag7, out flag8);
				bool flag9 = context.Model.SchemaVersion <= 1.1;
				if (fromRole.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					context.AddError(fromRole, null, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne(fromRole.Name, edmAssociationType.Name));
				}
				else if (flag2 && fromRole.RelationshipMultiplicity == RelationshipMultiplicity.One)
				{
					string errorMessage = Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1(fromRole.Name, edmAssociationType.Name);
					context.AddError(edmAssociationType, null, errorMessage);
				}
				else if (((flag9 && !flag2) || (!flag9 && !flag3)) && fromRole.RelationshipMultiplicity != RelationshipMultiplicity.One)
				{
					string errorMessage2;
					if (flag9)
					{
						errorMessage2 = Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1(fromRole.Name, edmAssociationType.Name);
					}
					else
					{
						errorMessage2 = Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2(fromRole.Name, edmAssociationType.Name);
					}
					context.AddError(edmAssociationType, null, errorMessage2);
				}
				if (!flag4 && !edmAssociationType.IsForeignKey(context.Model.SchemaVersion) && context.IsCSpace)
				{
					context.AddError(toRole, null, Strings.EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint(toRole.Name, EdmModelSemanticValidationRules.GetQualifiedName(toRole.GetEntityType(), toRole.GetEntityType().NamespaceName), EdmModelSemanticValidationRules.GetQualifiedName(edmAssociationType, edmAssociationType.NamespaceName)));
				}
				if (flag)
				{
					if (toRole.RelationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						context.AddError(toRole, null, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne(toRole.Name, edmAssociationType.Name));
					}
				}
				else if (toRole.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					context.AddError(toRole, null, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany(toRole.Name, edmAssociationType.Name));
				}
				List<EdmProperty> list = fromRole.GetEntityType().GetValidKey().ToList<EdmProperty>();
				List<EdmProperty> list2 = constraint.ToProperties.ToList<EdmProperty>();
				if (list2.Count != list.Count)
				{
					context.AddError(constraint, null, Strings.EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint);
					return;
				}
				EdmModelSemanticValidationRules.<>c__DisplayClass65 CS$<>8__locals1 = new EdmModelSemanticValidationRules.<>c__DisplayClass65();
				CS$<>8__locals1.principalProperties = constraint.FromProperties.ToList<EdmProperty>();
				int count = list2.Count;
				int i;
				for (i = 0; i < count; i++)
				{
					EdmProperty edmProperty = list2[i];
					EdmProperty edmProperty2 = list.SingleOrDefault((EdmProperty p) => p.Name == CS$<>8__locals1.principalProperties[i].Name);
					if (edmProperty2 != null && edmProperty != null && edmProperty2.TypeUsage != null && edmProperty.TypeUsage != null && edmProperty2.IsPrimitiveType && edmProperty.IsPrimitiveType && !EdmModelSemanticValidationRules.IsPrimitiveTypesEqual(edmProperty, edmProperty2))
					{
						context.AddError(constraint, null, Strings.EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(constraint.ToProperties.ToList<EdmProperty>()[i].Name, toRole.GetEntityType().Name, edmProperty2.Name, fromRole.GetEntityType().Name, edmAssociationType.Name));
					}
				}
			}
		});

		// Token: 0x04000050 RID: 80
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_InvalidPropertyInRelationshipConstraint = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.Constraint != null && edmAssociationType.Constraint.ToRole != null && edmAssociationType.Constraint.ToRole.GetEntityType() != null)
			{
				List<EdmProperty> list = edmAssociationType.Constraint.ToRole.GetEntityType().Properties.ToList<EdmProperty>();
				foreach (EdmProperty edmProperty in edmAssociationType.Constraint.ToProperties)
				{
					if (edmProperty != null && !list.Contains(edmProperty))
					{
						context.AddError(edmProperty, null, Strings.EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint(edmProperty.Name, edmAssociationType.Constraint.ToRole.Name));
					}
				}
			}
		});

		// Token: 0x04000051 RID: 81
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_InvalidIsAbstract = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.Abstract)
			{
				context.AddError(edmComplexType, "Abstract", Strings.EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
			}
		});

		// Token: 0x04000052 RID: 82
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_InvalidIsPolymorphic = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.BaseType != null)
			{
				context.AddError(edmComplexType, "BaseType", Strings.EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
			}
		});

		// Token: 0x04000053 RID: 83
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_InvalidMemberNameMatchesTypeName = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (!string.IsNullOrWhiteSpace(edmComplexType.Name) && edmComplexType.Properties.Any<EdmProperty>())
			{
				foreach (EdmProperty edmProperty in edmComplexType.Properties)
				{
					if (edmProperty != null && edmProperty.Name.EqualsOrdinal(edmComplexType.Name))
					{
						context.AddError(edmProperty, "Name", Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty.Name, EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
					}
				}
			}
		});

		// Token: 0x04000054 RID: 84
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_PropertyNameAlreadyDefinedDuplicate = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.Properties.Any<EdmProperty>())
			{
				HashSet<string> memberNameList = new HashSet<string>();
				foreach (EdmProperty edmProperty in edmComplexType.Properties)
				{
					if (!string.IsNullOrWhiteSpace(edmProperty.Name))
					{
						EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmProperty, memberNameList, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
					}
				}
			}
		});

		// Token: 0x04000055 RID: 85
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_PropertyNameAlreadyDefinedDuplicate_V1_1 = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.Properties.Any<EdmProperty>())
			{
				HashSet<string> memberNameList = new HashSet<string>();
				foreach (EdmProperty edmProperty in edmComplexType.Properties)
				{
					if (edmProperty != null && !string.IsNullOrWhiteSpace(edmProperty.Name))
					{
						EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmProperty, memberNameList, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
					}
				}
			}
		});

		// Token: 0x04000056 RID: 86
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_CycleInTypeHierarchy_V1_1 = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (EdmModelSemanticValidationRules.CheckForInheritanceCycle<ComplexType>(edmComplexType, (ComplexType ct) => (ComplexType)ct.BaseType))
			{
				context.AddError(edmComplexType, "BaseType", Strings.EdmModel_Validator_Semantic_CycleInTypeHierarchy(EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
			}
		});

		// Token: 0x04000057 RID: 87
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidCollectionKind = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.CollectionKind != CollectionKind.None)
			{
				context.AddError(edmProperty, "CollectionKind", Strings.EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1(edmProperty.Name));
			}
		});

		// Token: 0x04000058 RID: 88
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidCollectionKind_V1_1 = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.CollectionKind != CollectionKind.None && edmProperty.TypeUsage != null && !edmProperty.IsCollectionType)
			{
				context.AddError(edmProperty, "CollectionKind", Strings.EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection(edmProperty.Name));
			}
		});

		// Token: 0x04000059 RID: 89
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_NullableComplexType = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage != null && edmProperty.ComplexType != null && edmProperty.Nullable)
			{
				context.AddError(edmProperty, "Nullable", Strings.EdmModel_Validator_Semantic_NullableComplexType(edmProperty.Name));
			}
		});

		// Token: 0x0400005A RID: 90
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidPropertyType = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage.EdmType != null && !edmProperty.IsPrimitiveType && !edmProperty.IsComplexType)
			{
				context.AddError(edmProperty, "Type", Strings.EdmModel_Validator_Semantic_InvalidPropertyType(edmProperty.IsCollectionType ? "CollectionType" : edmProperty.TypeUsage.EdmType.BuiltInTypeKind.ToString()));
			}
		});

		// Token: 0x0400005B RID: 91
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidPropertyType_V1_1 = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage != null && edmProperty.TypeUsage.EdmType != null && !edmProperty.IsPrimitiveType && !edmProperty.IsComplexType && !edmProperty.IsCollectionType)
			{
				context.AddError(edmProperty, "Type", Strings.EdmModel_Validator_Semantic_InvalidPropertyType_V1_1(edmProperty.TypeUsage.EdmType.BuiltInTypeKind.ToString()));
			}
		});

		// Token: 0x0400005C RID: 92
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidPropertyType_V3 = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage != null && edmProperty.TypeUsage.EdmType != null && !edmProperty.IsPrimitiveType && !edmProperty.IsComplexType && !edmProperty.IsEnumType)
			{
				context.AddError(edmProperty, "Type", Strings.EdmModel_Validator_Semantic_InvalidPropertyType_V3(edmProperty.TypeUsage.EdmType.BuiltInTypeKind.ToString()));
			}
		});

		// Token: 0x0400005D RID: 93
		internal static readonly EdmModelValidationRule<EdmModel> EdmNamespace_TypeNameAlreadyDefinedDuplicate = new EdmModelValidationRule<EdmModel>(delegate(EdmModelValidationContext context, EdmModel model)
		{
			HashSet<string> memberNameList = new HashSet<string>();
			foreach (EdmType item in model.NamespaceItems)
			{
				EdmModelSemanticValidationRules.AddMemberNameToHashSet(item, memberNameList, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate));
			}
		});
	}
}
