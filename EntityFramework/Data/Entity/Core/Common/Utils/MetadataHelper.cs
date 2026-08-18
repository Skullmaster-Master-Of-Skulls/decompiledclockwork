using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x0200032C RID: 812
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal static class MetadataHelper
	{
		// Token: 0x06001BEB RID: 7147 RVA: 0x000896C8 File Offset: 0x000878C8
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

		// Token: 0x06001BEC RID: 7148 RVA: 0x00089790 File Offset: 0x00087990
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

		// Token: 0x06001BED RID: 7149 RVA: 0x000897D0 File Offset: 0x000879D0
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

		// Token: 0x06001BEE RID: 7150 RVA: 0x00089813 File Offset: 0x00087A13
		internal static FunctionParameter GetReturnParameter(EdmFunction functionImport, int resultSetIndex)
		{
			if (functionImport.ReturnParameters.Count <= resultSetIndex)
			{
				return null;
			}
			return functionImport.ReturnParameters[resultSetIndex];
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00089831 File Offset: 0x00087A31
		internal static EdmFunction GetFunctionImport(string functionName, string defaultContainerName, MetadataWorkspace workspace, out string containerName, out string functionImportName)
		{
			CommandHelper.ParseFunctionImportCommandText(functionName, defaultContainerName, out containerName, out functionImportName);
			return CommandHelper.FindFunctionImport(workspace, containerName, functionImportName);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00089848 File Offset: 0x00087A48
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

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00089870 File Offset: 0x00087A70
		internal static void CheckFunctionImportReturnType<TElement>(EdmType expectedEdmType, MetadataWorkspace workspace)
		{
			EdmType item = expectedEdmType;
			bool flag;
			if (Helper.IsSpatialType(expectedEdmType, out flag))
			{
				item = PrimitiveType.GetEdmPrimitiveType(flag ? PrimitiveTypeKind.Geography : PrimitiveTypeKind.Geometry);
			}
			EdmType edmType;
			if (!workspace.TryDetermineCSpaceModelType<TElement>(out edmType) || !edmType.EdmEquals(item))
			{
				throw new InvalidOperationException(Strings.ObjectContext_ExecuteFunctionTypeMismatch(typeof(TElement).FullName, expectedEdmType.FullName));
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000898CC File Offset: 0x00087ACC
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

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000898FC File Offset: 0x00087AFC
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

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00089958 File Offset: 0x00087B58
		internal static bool IsNonRefSimpleMember(EdmMember member)
		{
			return member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType || member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.EnumType;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00089984 File Offset: 0x00087B84
		internal static bool HasDiscreteDomain(EdmType edmType)
		{
			PrimitiveType primitiveType = edmType as PrimitiveType;
			return primitiveType != null && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Boolean;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000899A8 File Offset: 0x00087BA8
		internal static EntityType GetEntityTypeForEnd(AssociationEndMember end)
		{
			RefType refType = (RefType)end.TypeUsage.EdmType;
			EntityTypeBase elementType = refType.ElementType;
			return (EntityType)elementType;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000899D3 File Offset: 0x00087BD3
		internal static EntitySet GetEntitySetAtEnd(AssociationSet associationSet, AssociationEndMember endMember)
		{
			return associationSet.AssociationSetEnds[endMember.Name].EntitySet;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000899EC File Offset: 0x00087BEC
		internal static AssociationEndMember GetOtherAssociationEnd(AssociationEndMember endMember)
		{
			ReadOnlyMetadataCollection<EdmMember> members = endMember.DeclaringType.Members;
			EdmMember edmMember = members[0];
			if (!object.ReferenceEquals(endMember, edmMember))
			{
				return (AssociationEndMember)edmMember;
			}
			return (AssociationEndMember)members[1];
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00089A2C File Offset: 0x00087C2C
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

		// Token: 0x06001BFA RID: 7162 RVA: 0x00089A9C File Offset: 0x00087C9C
		internal static bool IsAssociationValidForEntityType(AssociationSetEnd toEnd, EntityType type)
		{
			AssociationSetEnd oppositeEnd = MetadataHelper.GetOppositeEnd(toEnd);
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(oppositeEnd.CorrespondingAssociationEndMember);
			return entityTypeForEnd.IsAssignableFrom(type);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00089ADC File Offset: 0x00087CDC
		internal static AssociationSetEnd GetOppositeEnd(AssociationSetEnd end)
		{
			return (from e in end.ParentAssociationSet.AssociationSetEnds
			where !e.EdmEquals(end)
			select e).Single<AssociationSetEnd>();
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00089B20 File Offset: 0x00087D20
		internal static bool IsComposable(EdmFunction function)
		{
			MetadataProperty metadataProperty;
			if (function.MetadataProperties.TryGetValue("IsComposableAttribute", false, out metadataProperty))
			{
				return (bool)metadataProperty.Value;
			}
			return !function.IsFunctionImport;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00089B57 File Offset: 0x00087D57
		internal static bool IsMemberNullable(EdmMember member)
		{
			return Helper.IsEdmProperty(member) && ((EdmProperty)member).Nullable;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00089BC4 File Offset: 0x00087DC4
		internal static IEnumerable<EntitySet> GetInfluencingEntitySetsForTable(EntitySet table, MetadataWorkspace workspace)
		{
			ItemCollection itemCollection = null;
			workspace.TryGetItemCollection(DataSpace.CSSpace, out itemCollection);
			EntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap((StorageMappingItemCollection)itemCollection, table.EntityContainer);
			return (from map in entityContainerMap.EntitySetMaps
			where map.TypeMappings.Any((TypeMapping typeMap) => typeMap.MappingFragments.Any((MappingFragment mappingFrag) => mappingFrag.TableSet.EdmEquals(table)))
			select map into m
			select m.Set).Cast<EntitySet>().Distinct<EntitySet>();
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00089C44 File Offset: 0x00087E44
		internal static IEnumerable<EdmType> GetTypeAndSubtypesOf(EdmType type, MetadataWorkspace workspace, bool includeAbstractTypes)
		{
			return MetadataHelper.GetTypeAndSubtypesOf(type, workspace.GetItemCollection(DataSpace.CSpace), includeAbstractTypes);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00089F2C File Offset: 0x0008812C
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
			foreach (EdmType subType in MetadataHelper.GetTypeAndSubtypesOf<EntityType>(type, itemCollection, includeAbstractTypes))
			{
				yield return subType;
			}
			foreach (EdmType subType2 in MetadataHelper.GetTypeAndSubtypesOf<ComplexType>(type, itemCollection, includeAbstractTypes))
			{
				yield return subType2;
			}
			yield break;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0008A1A0 File Offset: 0x000883A0
		private static IEnumerable<EdmType> GetTypeAndSubtypesOf<T_EdmType>(EdmType type, ItemCollection itemCollection, bool includeAbstractTypes) where T_EdmType : EdmType
		{
			T_EdmType specificType = type as T_EdmType;
			if (specificType != null)
			{
				IEnumerable<T_EdmType> typesInWorkSpace = itemCollection.GetItems<T_EdmType>();
				foreach (T_EdmType typeInWorkSpace in typesInWorkSpace)
				{
					if (!specificType.Equals(typeInWorkSpace) && Helper.IsSubtypeOf(typeInWorkSpace, specificType))
					{
						if (!includeAbstractTypes)
						{
							T_EdmType t_EdmType = typeInWorkSpace;
							if (t_EdmType.Abstract)
							{
								continue;
							}
						}
						yield return typeInWorkSpace;
					}
				}
			}
			yield break;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0008A308 File Offset: 0x00088508
		internal static IEnumerable<EdmType> GetTypeAndParentTypesOf(EdmType type, bool includeAbstractTypes)
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

		// Token: 0x06001C03 RID: 7171 RVA: 0x0008A32C File Offset: 0x0008852C
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

		// Token: 0x06001C04 RID: 7172 RVA: 0x0008A3A4 File Offset: 0x000885A4
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

		// Token: 0x06001C05 RID: 7173 RVA: 0x0008A3DC File Offset: 0x000885DC
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

		// Token: 0x06001C06 RID: 7174 RVA: 0x0008A428 File Offset: 0x00088628
		[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCode", Justification = "Based on Bug VSTS Pioneer #433188: IsVisibleOutsideAssembly is wrong on generic instantiations.")]
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
				if (thisEnd.Equals(referentialConstraint.ToRole))
				{
					enumerable = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.FromProperties);
					second = (EntityType)((RefType)referentialConstraint.FromRole.TypeUsage.EdmType).ElementType;
				}
				else
				{
					if (!thisEnd.Equals(referentialConstraint.FromRole))
					{
						continue;
					}
					enumerable = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties);
					second = (EntityType)((RefType)referentialConstraint.ToRole.TypeUsage.EdmType).ElementType;
				}
				foreach (EdmMember first in enumerable)
				{
					associationkeys.Remove(new Pair<EdmMember, EntityType>(first, second));
				}
			}
			return associationkeys.IsSubsetOf(other);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0008A594 File Offset: 0x00088794
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

		// Token: 0x06001C08 RID: 7176 RVA: 0x0008A604 File Offset: 0x00088804
		internal static bool IsExtentAtSomeRelationshipEnd(AssociationSet relationshipSet, EntitySetBase extent)
		{
			return Helper.IsEntitySet(extent) && MetadataHelper.GetSomeEndForEntitySet(relationshipSet, extent) != null;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0008A620 File Offset: 0x00088820
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

		// Token: 0x06001C0A RID: 7178 RVA: 0x0008A688 File Offset: 0x00088888
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

		// Token: 0x06001C0B RID: 7179 RVA: 0x0008A708 File Offset: 0x00088908
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

		// Token: 0x06001C0C RID: 7180 RVA: 0x0008A780 File Offset: 0x00088980
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

		// Token: 0x06001C0D RID: 7181 RVA: 0x0008A7A7 File Offset: 0x000889A7
		internal static bool IsPartOfEntityTypeKey(EdmMember member)
		{
			return Helper.IsEntityType(member.DeclaringType) && Helper.IsEdmProperty(member) && ((EntityType)member.DeclaringType).KeyMembers.Contains(member);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0008A7D8 File Offset: 0x000889D8
		internal static TypeUsage GetElementType(TypeUsage typeUsage)
		{
			if (BuiltInTypeKind.CollectionType == typeUsage.EdmType.BuiltInTypeKind)
			{
				TypeUsage typeUsage2 = ((CollectionType)typeUsage.EdmType).TypeUsage;
				return MetadataHelper.GetElementType(typeUsage2);
			}
			return typeUsage;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0008A80C File Offset: 0x00088A0C
		internal static int GetLowerBoundOfMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (multiplicity == RelationshipMultiplicity.Many || multiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0008A818 File Offset: 0x00088A18
		internal static int? GetUpperBoundOfMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (multiplicity == RelationshipMultiplicity.One || multiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				return new int?(1);
			}
			return null;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0008A83C File Offset: 0x00088A3C
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

		// Token: 0x06001C12 RID: 7186 RVA: 0x0008A8E4 File Offset: 0x00088AE4
		internal static ConcurrencyMode GetConcurrencyMode(EdmMember member)
		{
			return MetadataHelper.GetConcurrencyMode(member.TypeUsage);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		internal static ConcurrencyMode GetConcurrencyMode(TypeUsage typeUsage)
		{
			Facet facet;
			if (typeUsage.Facets.TryGetValue("ConcurrencyMode", false, out facet) && facet.Value != null)
			{
				return (ConcurrencyMode)facet.Value;
			}
			return ConcurrencyMode.None;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0008A930 File Offset: 0x00088B30
		internal static StoreGeneratedPattern GetStoreGeneratedPattern(EdmMember member)
		{
			Facet facet;
			if (member.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null)
			{
				return (StoreGeneratedPattern)facet.Value;
			}
			return StoreGeneratedPattern.None;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0008A970 File Offset: 0x00088B70
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

		// Token: 0x06001C16 RID: 7190 RVA: 0x0008A9A4 File Offset: 0x00088BA4
		[SuppressMessage("Microsoft.Cryptographic.Standard", "CA5350:Microsoft.Cryptographic.Standard", Justification = "MD5CryptoServiceProvider is not used for cryptography/security purposes and we do it only for v1 and v1.1 for compatibility reasons.")]
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

		// Token: 0x06001C17 RID: 7191 RVA: 0x0008A9CC File Offset: 0x00088BCC
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

		// Token: 0x06001C18 RID: 7192 RVA: 0x0008A9FC File Offset: 0x00088BFC
		internal static TypeUsage ConvertStoreTypeUsageToEdmTypeUsage(TypeUsage storeTypeUsage)
		{
			return storeTypeUsage.ModelTypeUsage.ShallowCopy(FacetValues.NullFacetValues);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0008AA1B File Offset: 0x00088C1B
		internal static byte GetPrecision(this TypeUsage type)
		{
			return type.GetFacetValue("Precision");
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0008AA28 File Offset: 0x00088C28
		internal static byte GetScale(this TypeUsage type)
		{
			return type.GetFacetValue("Scale");
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0008AA35 File Offset: 0x00088C35
		internal static int GetMaxLength(this TypeUsage type)
		{
			return type.GetFacetValue("MaxLength");
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0008AA42 File Offset: 0x00088C42
		internal static T GetFacetValue<T>(this TypeUsage type, string facetName)
		{
			return (T)((object)type.Facets[facetName].Value);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0008AA5A File Offset: 0x00088C5A
		internal static NavigationPropertyAccessor GetNavigationPropertyAccessor(EntityType sourceEntityType, AssociationEndMember sourceMember, AssociationEndMember targetMember)
		{
			return MetadataHelper.GetNavigationPropertyAccessor(sourceEntityType, sourceMember.DeclaringType.FullName, sourceMember.Name, targetMember.Name);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0008AA7C File Offset: 0x00088C7C
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
