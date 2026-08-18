using System;
using System.Collections.Generic;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace System.Data.Common.Utils
{
	// Token: 0x02000396 RID: 918
	internal static class MetadataHelper
	{
		// Token: 0x060032B0 RID: 12976 RVA: 0x000C5EC0 File Offset: 0x000C40C0
		internal static bool TryGetFunctionImportReturnType<T>(EdmFunction functionImport, int resultSetIndex, out T returnType) where T : EdmType
		{
			T t;
			if (MetadataHelper.TryGetWrappedReturnEdmTypeFromFunctionImport<T>(functionImport, resultSetIndex, out t) && ((typeof(EntityType).Equals(typeof(T)) && t is EntityType) || (typeof(ComplexType).Equals(typeof(T)) && t is ComplexType) || (typeof(StructuralType).Equals(typeof(T)) && t is StructuralType) || (typeof(EdmType).Equals(typeof(T)) && t != null)))
			{
				returnType = t;
				return true;
			}
			returnType = default(T);
			return false;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000C5F88 File Offset: 0x000C4188
		private static bool TryGetWrappedReturnEdmTypeFromFunctionImport<T>(EdmFunction functionImport, int resultSetIndex, out T resultType) where T : EdmType
		{
			resultType = default(T);
			CollectionType collectionType;
			if (MetadataHelper.TryGetFunctionImportReturnCollectionType(functionImport, resultSetIndex, out collectionType))
			{
				resultType = (collectionType.TypeUsage.EdmType as T);
				return true;
			}
			return false;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000C5FC8 File Offset: 0x000C41C8
		internal static bool TryGetFunctionImportReturnCollectionType(EdmFunction functionImport, int resultSetIndex, out CollectionType collectionType)
		{
			FunctionParameter returnParameter = MetadataHelper.GetReturnParameter(functionImport, resultSetIndex);
			if (returnParameter != null && returnParameter.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				collectionType = (CollectionType)returnParameter.TypeUsage.EdmType;
				return true;
			}
			collectionType = null;
			return false;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000C600B File Offset: 0x000C420B
		internal static FunctionParameter GetReturnParameter(EdmFunction functionImport, int resultSetIndex)
		{
			if (functionImport.ReturnParameters.Count <= resultSetIndex)
			{
				return null;
			}
			return functionImport.ReturnParameters[resultSetIndex];
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000C6029 File Offset: 0x000C4229
		internal static EdmFunction GetFunctionImport(string functionName, string defaultContainerName, MetadataWorkspace workspace, out string containerName, out string functionImportName)
		{
			CommandHelper.ParseFunctionImportCommandText(functionName, defaultContainerName, out containerName, out functionImportName);
			return CommandHelper.FindFunctionImport(workspace, containerName, functionImportName);
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000C6040 File Offset: 0x000C4240
		internal static EdmType GetAndCheckFunctionImportReturnType<TElement>(EdmFunction functionImport, int resultSetIndex, MetadataWorkspace workspace)
		{
			EdmType edmType;
			if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, resultSetIndex, out edmType))
			{
				throw EntityUtil.ExecuteFunctionCalledWithNonReaderFunction(functionImport);
			}
			MetadataHelper.CheckFunctionImportReturnType<TElement>(edmType, workspace);
			return edmType;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000C6068 File Offset: 0x000C4268
		internal static void CheckFunctionImportReturnType<TElement>(EdmType expectedEdmType, MetadataWorkspace workspace)
		{
			EdmType item = expectedEdmType;
			bool flag;
			if (Helper.IsSpatialType(expectedEdmType, out flag))
			{
				item = PrimitiveType.GetEdmPrimitiveType(flag ? PrimitiveTypeKind.Geography : PrimitiveTypeKind.Geometry);
			}
			EdmType edmType;
			if (!MetadataHelper.TryDetermineCSpaceModelType<TElement>(workspace, out edmType) || !edmType.EdmEquals(item))
			{
				throw EntityUtil.ExecuteFunctionTypeMismatch(typeof(TElement), expectedEdmType);
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000C60B4 File Offset: 0x000C42B4
		internal static ParameterDirection ParameterModeToParameterDirection(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return ParameterDirection.Input;
			case ParameterMode.Out:
				return ParameterDirection.Output;
			case ParameterMode.InOut:
				return ParameterDirection.InputOutput;
			case ParameterMode.ReturnValue:
				return ParameterDirection.ReturnValue;
			default:
				return (ParameterDirection)0;
			}
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000C60D7 File Offset: 0x000C42D7
		internal static bool TryDetermineCSpaceModelType<T>(MetadataWorkspace workspace, out EdmType modelEdmType)
		{
			return MetadataHelper.TryDetermineCSpaceModelType(typeof(T), workspace, out modelEdmType);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000C60EC File Offset: 0x000C42EC
		internal static bool TryDetermineCSpaceModelType(Type type, MetadataWorkspace workspace, out EdmType modelEdmType)
		{
			Type nonNullableType = TypeSystem.GetNonNullableType(type);
			workspace.ImplicitLoadAssemblyForType(nonNullableType, Assembly.GetCallingAssembly());
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)workspace.GetItemCollection(DataSpace.OSpace);
			EdmType item;
			Map map;
			if (objectItemCollection.TryGetItem<EdmType>(nonNullableType.FullName, out item) && workspace.TryGetMap(item, DataSpace.OCSpace, out map))
			{
				ObjectTypeMapping objectTypeMapping = (ObjectTypeMapping)map;
				modelEdmType = objectTypeMapping.EdmType;
				return true;
			}
			modelEdmType = null;
			return false;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000C614C File Offset: 0x000C434C
		internal static bool DoesMemberExist(StructuralType type, EdmMember member)
		{
			foreach (EdmMember edmMember in type.Members)
			{
				if (edmMember.Equals(member))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000C61A8 File Offset: 0x000C43A8
		internal static bool IsNonRefSimpleMember(EdmMember member)
		{
			return member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType || member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.EnumType;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000C61D4 File Offset: 0x000C43D4
		internal static bool HasDiscreteDomain(EdmType edmType)
		{
			PrimitiveType primitiveType = edmType as PrimitiveType;
			return primitiveType != null && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Boolean;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000C61F8 File Offset: 0x000C43F8
		internal static EntityType GetEntityTypeForEnd(AssociationEndMember end)
		{
			RefType refType = (RefType)end.TypeUsage.EdmType;
			EntityTypeBase elementType = refType.ElementType;
			return (EntityType)elementType;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000C6223 File Offset: 0x000C4423
		internal static EntitySet GetEntitySetAtEnd(AssociationSet associationSet, AssociationEndMember endMember)
		{
			return associationSet.AssociationSetEnds[endMember.Name].EntitySet;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000C623C File Offset: 0x000C443C
		internal static AssociationEndMember GetOtherAssociationEnd(AssociationEndMember endMember)
		{
			ReadOnlyMetadataCollection<EdmMember> members = endMember.DeclaringType.Members;
			EdmMember edmMember = members[0];
			if (endMember != edmMember)
			{
				return (AssociationEndMember)edmMember;
			}
			return (AssociationEndMember)members[1];
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000C6274 File Offset: 0x000C4474
		internal static bool IsEveryOtherEndAtLeastOne(AssociationSet associationSet, AssociationEndMember member)
		{
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
				if (!correspondingAssociationEndMember.Equals(member) && MetadataHelper.GetLowerBoundOfMultiplicity(correspondingAssociationEndMember.RelationshipMultiplicity) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000C62E4 File Offset: 0x000C44E4
		internal static bool IsAssociationValidForEntityType(AssociationSetEnd toEnd, EntityType type)
		{
			AssociationSetEnd oppositeEnd = MetadataHelper.GetOppositeEnd(toEnd);
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(oppositeEnd.CorrespondingAssociationEndMember);
			return entityTypeForEnd.IsAssignableFrom(type);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000C630C File Offset: 0x000C450C
		internal static AssociationSetEnd GetOppositeEnd(AssociationSetEnd end)
		{
			return (from e in end.ParentAssociationSet.AssociationSetEnds
			where !e.EdmEquals(end)
			select e).Single<AssociationSetEnd>();
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000C6350 File Offset: 0x000C4550
		internal static bool IsComposable(EdmFunction function)
		{
			MetadataProperty metadataProperty;
			if (function.MetadataProperties.TryGetValue("IsComposableAttribute", false, out metadataProperty))
			{
				return (bool)metadataProperty.Value;
			}
			return !function.IsFunctionImport;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000C6387 File Offset: 0x000C4587
		internal static bool IsMemberNullable(EdmMember member)
		{
			return Helper.IsEdmProperty(member) && ((EdmProperty)member).Nullable;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000C63A0 File Offset: 0x000C45A0
		internal static IEnumerable<EntitySet> GetInfluencingEntitySetsForTable(EntitySet table, MetadataWorkspace workspace)
		{
			ItemCollection itemCollection = null;
			workspace.TryGetItemCollection(DataSpace.CSSpace, out itemCollection);
			StorageEntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap((StorageMappingItemCollection)itemCollection, table.EntityContainer);
			Func<StorageMappingFragment, bool> <>9__3;
			Func<StorageTypeMapping, bool> <>9__2;
			return (from m in entityContainerMap.EntitySetMaps.Where(delegate(StorageSetMapping map)
			{
				IEnumerable<StorageTypeMapping> typeMappings = map.TypeMappings;
				Func<StorageTypeMapping, bool> predicate;
				if ((predicate = <>9__2) == null)
				{
					predicate = (<>9__2 = delegate(StorageTypeMapping typeMap)
					{
						IEnumerable<StorageMappingFragment> mappingFragments = typeMap.MappingFragments;
						Func<StorageMappingFragment, bool> predicate2;
						if ((predicate2 = <>9__3) == null)
						{
							predicate2 = (<>9__3 = ((StorageMappingFragment mappingFrag) => mappingFrag.TableSet.EdmEquals(table)));
						}
						return mappingFragments.Any(predicate2);
					});
				}
				return typeMappings.Any(predicate);
			})
			select m.Set).Cast<EntitySet>().Distinct<EntitySet>();
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000C6422 File Offset: 0x000C4622
		internal static IEnumerable<EdmType> GetTypeAndSubtypesOf(EdmType type, MetadataWorkspace workspace, bool includeAbstractTypes)
		{
			return MetadataHelper.GetTypeAndSubtypesOf(type, workspace.GetItemCollection(DataSpace.CSpace), includeAbstractTypes);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000C6432 File Offset: 0x000C4632
		internal static IEnumerable<EdmType> GetTypeAndSubtypesOf(EdmType type, ItemCollection itemCollection, bool includeAbstractTypes)
		{
			if (Helper.IsRefType(type))
			{
				type = ((RefType)type).ElementType;
			}
			if (includeAbstractTypes || !type.Abstract)
			{
				yield return type;
			}
			foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf<EntityType>(type, itemCollection, includeAbstractTypes))
			{
				yield return edmType;
			}
			IEnumerator<EdmType> enumerator = null;
			foreach (EdmType edmType2 in MetadataHelper.GetTypeAndSubtypesOf<ComplexType>(type, itemCollection, includeAbstractTypes))
			{
				yield return edmType2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000C6450 File Offset: 0x000C4650
		private static IEnumerable<EdmType> GetTypeAndSubtypesOf<T_EdmType>(EdmType type, ItemCollection itemCollection, bool includeAbstractTypes) where T_EdmType : EdmType
		{
			T_EdmType specificType = type as T_EdmType;
			if (specificType != null)
			{
				IEnumerable<T_EdmType> items = itemCollection.GetItems<T_EdmType>();
				foreach (T_EdmType t_EdmType in items)
				{
					if (!specificType.Equals(t_EdmType) && Helper.IsSubtypeOf(t_EdmType, specificType) && (includeAbstractTypes || !t_EdmType.Abstract))
					{
						yield return t_EdmType;
					}
				}
				IEnumerator<T_EdmType> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000C646E File Offset: 0x000C466E
		internal static IEnumerable<EdmType> GetTypeAndParentTypesOf(EdmType type, ItemCollection itemCollection, bool includeAbstractTypes)
		{
			if (Helper.IsRefType(type))
			{
				type = ((RefType)type).ElementType;
			}
			for (EdmType specificType = type; specificType != null; specificType = (specificType.BaseType as EntityType))
			{
				if (includeAbstractTypes || !specificType.Abstract)
				{
					yield return specificType;
				}
			}
			yield break;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000C6488 File Offset: 0x000C4688
		internal static Dictionary<EntityType, Set<EntityType>> BuildUndirectedGraphOfTypes(EdmItemCollection edmItemCollection)
		{
			Dictionary<EntityType, Set<EntityType>> dictionary = new Dictionary<EntityType, Set<EntityType>>();
			IEnumerable<EntityType> items = edmItemCollection.GetItems<EntityType>();
			foreach (EntityType entityType in items)
			{
				if (entityType.BaseType != null)
				{
					EntityType entityType2 = entityType.BaseType as EntityType;
					MetadataHelper.AddDirectedEdgeBetweenEntityTypes(dictionary, entityType, entityType2);
					MetadataHelper.AddDirectedEdgeBetweenEntityTypes(dictionary, entityType2, entityType);
				}
			}
			return dictionary;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000C6500 File Offset: 0x000C4700
		internal static bool IsParentOf(EntityType a, EntityType b)
		{
			for (EntityType entityType = b.BaseType as EntityType; entityType != null; entityType = (entityType.BaseType as EntityType))
			{
				if (entityType.EdmEquals(a))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000C6538 File Offset: 0x000C4738
		private static void AddDirectedEdgeBetweenEntityTypes(Dictionary<EntityType, Set<EntityType>> graph, EntityType a, EntityType b)
		{
			Set<EntityType> set;
			if (graph.ContainsKey(a))
			{
				set = graph[a];
			}
			else
			{
				set = new Set<EntityType>();
				graph.Add(a, set);
			}
			set.Add(b);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000C6570 File Offset: 0x000C4770
		internal static bool DoesEndKeySubsumeAssociationSetKey(AssociationSet assocSet, AssociationEndMember thisEnd, HashSet<Pair<EdmMember, EntityType>> associationkeys)
		{
			AssociationType elementType = assocSet.ElementType;
			EntityType thisEndsEntityType = (EntityType)((RefType)thisEnd.TypeUsage.EdmType).ElementType;
			HashSet<Pair<EdmMember, EntityType>> other = new HashSet<Pair<EdmMember, EntityType>>(from edmMember in thisEndsEntityType.KeyMembers
			select new Pair<EdmMember, EntityType>(edmMember, thisEndsEntityType));
			foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
			{
				IEnumerable<EdmMember> enumerable;
				EntityType second;
				if (thisEnd.Equals((AssociationEndMember)referentialConstraint.ToRole))
				{
					enumerable = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.FromProperties);
					second = (EntityType)((RefType)((AssociationEndMember)referentialConstraint.FromRole).TypeUsage.EdmType).ElementType;
				}
				else
				{
					if (!thisEnd.Equals((AssociationEndMember)referentialConstraint.FromRole))
					{
						continue;
					}
					enumerable = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties);
					second = (EntityType)((RefType)((AssociationEndMember)referentialConstraint.ToRole).TypeUsage.EdmType).ElementType;
				}
				foreach (EdmMember first in enumerable)
				{
					associationkeys.Remove(new Pair<EdmMember, EntityType>(first, second));
				}
			}
			return associationkeys.IsSubsetOf(other);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000C66F4 File Offset: 0x000C48F4
		internal static bool DoesEndFormKey(AssociationSet associationSet, AssociationEndMember end)
		{
			foreach (EdmMember edmMember in associationSet.ElementType.Members)
			{
				AssociationEndMember associationEndMember = (AssociationEndMember)edmMember;
				if (!associationEndMember.Equals(end) && associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000C6764 File Offset: 0x000C4964
		internal static bool IsExtentAtSomeRelationshipEnd(AssociationSet relationshipSet, EntitySetBase extent)
		{
			return Helper.IsEntitySet(extent) && MetadataHelper.GetSomeEndForEntitySet(relationshipSet, (EntitySet)extent) != null;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000C6780 File Offset: 0x000C4980
		internal static AssociationEndMember GetSomeEndForEntitySet(AssociationSet associationSet, EntitySetBase entitySet)
		{
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				if (associationSetEnd.EntitySet.Equals(entitySet))
				{
					return associationSetEnd.CorrespondingAssociationEndMember;
				}
			}
			return null;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000C67E8 File Offset: 0x000C49E8
		internal static List<AssociationSet> GetAssociationsForEntitySets(EntitySet entitySet1, EntitySet entitySet2)
		{
			List<AssociationSet> list = new List<AssociationSet>();
			foreach (EntitySetBase entitySetBase in entitySet1.EntityContainer.BaseEntitySets)
			{
				if (Helper.IsRelationshipSet(entitySetBase))
				{
					AssociationSet associationSet = (AssociationSet)entitySetBase;
					if (MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet1) && MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet2))
					{
						list.Add(associationSet);
					}
				}
			}
			return list;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000C6868 File Offset: 0x000C4A68
		internal static AssociationSet GetAssociationsForEntitySetAndAssociationType(EntityContainer entityContainer, string entitySetName, AssociationType associationType, string endName, out EntitySet entitySet)
		{
			entitySet = null;
			AssociationSet result = null;
			ReadOnlyMetadataCollection<EntitySetBase> baseEntitySets = entityContainer.BaseEntitySets;
			int count = baseEntitySets.Count;
			for (int i = 0; i < count; i++)
			{
				EntitySetBase entitySetBase = baseEntitySets[i];
				if (entitySetBase.ElementType == associationType)
				{
					AssociationSet associationSet = (AssociationSet)entitySetBase;
					EntitySet entitySet2 = associationSet.AssociationSetEnds[endName].EntitySet;
					if (entitySet2.Name == entitySetName)
					{
						result = associationSet;
						entitySet = entitySet2;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000C68E0 File Offset: 0x000C4AE0
		internal static List<AssociationSet> GetAssociationsForEntitySet(EntitySetBase entitySet)
		{
			List<AssociationSet> list = new List<AssociationSet>();
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				if (Helper.IsRelationshipSet(entitySetBase))
				{
					AssociationSet associationSet = (AssociationSet)entitySetBase;
					if (MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet))
					{
						list.Add(associationSet);
					}
				}
			}
			return list;
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000C6958 File Offset: 0x000C4B58
		internal static bool IsSuperTypeOf(EdmType superType, EdmType subType)
		{
			for (EdmType edmType = subType; edmType != null; edmType = edmType.BaseType)
			{
				if (edmType.Equals(superType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000C6980 File Offset: 0x000C4B80
		internal static PrimitiveTypeKind GetPrimitiveTypeKind(TypeUsage typeUsage)
		{
			PrimitiveType primitiveType = (PrimitiveType)typeUsage.EdmType;
			return primitiveType.PrimitiveTypeKind;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000C699F File Offset: 0x000C4B9F
		internal static bool IsPartOfEntityTypeKey(EdmMember member)
		{
			return Helper.IsEntityType(member.DeclaringType) && Helper.IsEdmProperty(member) && ((EntityType)member.DeclaringType).KeyMembers.Contains(member);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000C69D0 File Offset: 0x000C4BD0
		internal static TypeUsage GetElementType(TypeUsage typeUsage)
		{
			if (BuiltInTypeKind.CollectionType == typeUsage.EdmType.BuiltInTypeKind)
			{
				TypeUsage typeUsage2 = ((CollectionType)typeUsage.EdmType).TypeUsage;
				return MetadataHelper.GetElementType(typeUsage2);
			}
			return typeUsage;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000C6A04 File Offset: 0x000C4C04
		internal static int GetLowerBoundOfMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (multiplicity == RelationshipMultiplicity.Many || multiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000C6A10 File Offset: 0x000C4C10
		internal static int? GetUpperBoundOfMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (multiplicity == RelationshipMultiplicity.One || multiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				return new int?(1);
			}
			return null;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000C6A34 File Offset: 0x000C4C34
		internal static Set<EdmMember> GetConcurrencyMembersForTypeHierarchy(EntityTypeBase superType, EdmItemCollection edmItemCollection)
		{
			Set<EdmMember> set = new Set<EdmMember>();
			foreach (EdmType edmType in MetadataHelper.GetTypeAndSubtypesOf(superType, edmItemCollection, true))
			{
				StructuralType structuralType = (StructuralType)edmType;
				foreach (EdmMember edmMember in structuralType.Members)
				{
					ConcurrencyMode concurrencyMode = MetadataHelper.GetConcurrencyMode(edmMember);
					if (concurrencyMode == ConcurrencyMode.Fixed)
					{
						set.Add(edmMember);
					}
				}
			}
			return set;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000C6ADC File Offset: 0x000C4CDC
		internal static ConcurrencyMode GetConcurrencyMode(EdmMember member)
		{
			return MetadataHelper.GetConcurrencyMode(member.TypeUsage);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000C6AEC File Offset: 0x000C4CEC
		internal static ConcurrencyMode GetConcurrencyMode(TypeUsage typeUsage)
		{
			Facet facet;
			if (typeUsage.Facets.TryGetValue("ConcurrencyMode", false, out facet) && facet.Value != null)
			{
				return (ConcurrencyMode)facet.Value;
			}
			return ConcurrencyMode.None;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000C6B28 File Offset: 0x000C4D28
		internal static StoreGeneratedPattern GetStoreGeneratedPattern(EdmMember member)
		{
			Facet facet;
			if (member.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null)
			{
				return (StoreGeneratedPattern)facet.Value;
			}
			return StoreGeneratedPattern.None;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000C6B68 File Offset: 0x000C4D68
		internal static bool CheckIfAllErrorsAreWarnings(IList<EdmSchemaError> schemaErrors)
		{
			int count = schemaErrors.Count;
			for (int i = 0; i < count; i++)
			{
				EdmSchemaError edmSchemaError = schemaErrors[i];
				if (edmSchemaError.Severity != EdmSchemaErrorSeverity.Warning)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000C6B9C File Offset: 0x000C4D9C
		internal static string GenerateHashForAllExtentViewsContent(double schemaVersion, IEnumerable<KeyValuePair<string, string>> extentViews)
		{
			CompressingHashBuilder compressingHashBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(schemaVersion));
			foreach (KeyValuePair<string, string> keyValuePair in extentViews)
			{
				compressingHashBuilder.AppendLine(keyValuePair.Key);
				compressingHashBuilder.AppendLine(keyValuePair.Value);
			}
			return compressingHashBuilder.ComputeHash();
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000C6C0C File Offset: 0x000C4E0C
		internal static HashAlgorithm CreateMetadataHashAlgorithm(double schemaVersion)
		{
			HashAlgorithm result;
			if (schemaVersion < 2.0)
			{
				result = new MD5CryptoServiceProvider();
			}
			else
			{
				result = MetadataHelper.CreateSHA256HashAlgorithm();
			}
			return result;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000C6C34 File Offset: 0x000C4E34
		internal static SHA256 CreateSHA256HashAlgorithm()
		{
			SHA256 result;
			try
			{
				result = new SHA256CryptoServiceProvider();
			}
			catch (PlatformNotSupportedException)
			{
				result = new SHA256Managed();
			}
			return result;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000C6C64 File Offset: 0x000C4E64
		internal static TypeUsage ConvertStoreTypeUsageToEdmTypeUsage(TypeUsage storeTypeUsage)
		{
			return storeTypeUsage.GetModelTypeUsage().ShallowCopy(FacetValues.NullFacetValues);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000C6C83 File Offset: 0x000C4E83
		internal static byte GetPrecision(this TypeUsage type)
		{
			return type.GetFacetValue("Precision");
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000C6C90 File Offset: 0x000C4E90
		internal static byte GetScale(this TypeUsage type)
		{
			return type.GetFacetValue("Scale");
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000C6C9D File Offset: 0x000C4E9D
		internal static int GetMaxLength(this TypeUsage type)
		{
			return type.GetFacetValue("MaxLength");
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000C6CAA File Offset: 0x000C4EAA
		internal static T GetFacetValue<T>(this TypeUsage type, string facetName)
		{
			return (T)((object)type.Facets[facetName].Value);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000C6CC2 File Offset: 0x000C4EC2
		internal static NavigationPropertyAccessor GetNavigationPropertyAccessor(EntityType sourceEntityType, AssociationEndMember sourceMember, AssociationEndMember targetMember)
		{
			return MetadataHelper.GetNavigationPropertyAccessor(sourceEntityType, sourceMember.DeclaringType.FullName, sourceMember.Name, targetMember.Name);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
		internal static NavigationPropertyAccessor GetNavigationPropertyAccessor(EntityType entityType, string relationshipType, string fromName, string toName)
		{
			NavigationProperty navigationProperty;
			if (entityType.TryGetNavigationProperty(relationshipType, fromName, toName, out navigationProperty))
			{
				return navigationProperty.Accessor;
			}
			return NavigationPropertyAccessor.NoNavigationProperty;
		}
	}
}
