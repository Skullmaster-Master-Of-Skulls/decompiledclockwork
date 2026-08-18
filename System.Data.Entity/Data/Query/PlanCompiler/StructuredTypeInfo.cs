using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Globalization;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200006D RID: 109
	internal class StructuredTypeInfo
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x0002D07E File Offset: 0x0002B27E
		private StructuredTypeInfo(HashSet<string> typesNeedingNullSentinel)
		{
			this.m_typeInfoMap = new Dictionary<TypeUsage, TypeInfo>(TypeUsageEqualityComparer.Instance);
			this.m_typeInfoMapPopulated = false;
			this.m_typesNeedingNullSentinel = typesNeedingNullSentinel;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002D0A4 File Offset: 0x0002B2A4
		internal static void Process(Command itree, HashSet<TypeUsage> referencedTypes, HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes, Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps, RelPropertyHelper relPropertyHelper, HashSet<string> typesNeedingNullSentinel, out StructuredTypeInfo structuredTypeInfo)
		{
			structuredTypeInfo = new StructuredTypeInfo(typesNeedingNullSentinel);
			structuredTypeInfo.Process(itree, referencedTypes, referencedEntitySets, freeFloatingEntityConstructorTypes, discriminatorMaps, relPropertyHelper);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002D0C0 File Offset: 0x0002B2C0
		private void Process(Command itree, HashSet<TypeUsage> referencedTypes, HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes, Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps, RelPropertyHelper relPropertyHelper)
		{
			PlanCompiler.Assert(itree != null, "null itree?");
			this.m_stringType = itree.StringType;
			this.m_intType = itree.IntegerType;
			this.m_relPropertyHelper = relPropertyHelper;
			this.ProcessEntitySets(referencedEntitySets, freeFloatingEntityConstructorTypes);
			this.ProcessDiscriminatorMaps(discriminatorMaps);
			this.ProcessTypes(referencedTypes);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0002D113 File Offset: 0x0002B313
		internal EntitySet[] EntitySetIdToEntitySetMap
		{
			get
			{
				return this.m_entitySetIdToEntitySetMap;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0002D11B File Offset: 0x0002B31B
		internal RelPropertyHelper RelPropertyHelper
		{
			get
			{
				return this.m_relPropertyHelper;
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002D124 File Offset: 0x0002B324
		internal EntitySet GetEntitySet(EntityTypeBase type)
		{
			EntityTypeBase rootType = StructuredTypeInfo.GetRootType(type);
			EntitySet result;
			if (!this.m_entityTypeToEntitySetMap.TryGetValue(rootType, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002D14C File Offset: 0x0002B34C
		internal int GetEntitySetId(EntitySet e)
		{
			int result = 0;
			if (!this.m_entitySetToEntitySetIdMap.TryGetValue(e, out result))
			{
				PlanCompiler.Assert(false, "no such entity set?");
			}
			return result;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002D177 File Offset: 0x0002B377
		internal Set<EntitySet> GetEntitySets()
		{
			return new Set<EntitySet>(this.m_entitySetIdToEntitySetMap).MakeReadOnly();
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002D18C File Offset: 0x0002B38C
		internal TypeInfo GetTypeInfo(TypeUsage type)
		{
			if (!TypeUtils.IsStructuredType(type))
			{
				return null;
			}
			TypeInfo result = null;
			if (!this.m_typeInfoMap.TryGetValue(type, out result))
			{
				PlanCompiler.Assert(!TypeUtils.IsStructuredType(type) || !this.m_typeInfoMapPopulated, "cannot find typeInfo for type " + ((type != null) ? type.ToString() : null));
			}
			return result;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
		private void AddEntityTypeToSetEntry(EntityType entityType, EntitySet entitySet)
		{
			EntityTypeBase rootType = StructuredTypeInfo.GetRootType(entityType);
			bool flag = true;
			EntitySet entitySet2;
			if (entitySet == null)
			{
				flag = false;
			}
			else if (this.m_entityTypeToEntitySetMap.TryGetValue(rootType, out entitySet2) && entitySet2 != entitySet)
			{
				flag = false;
			}
			if (flag)
			{
				this.m_entityTypeToEntitySetMap[rootType] = entitySet;
				return;
			}
			this.m_entityTypeToEntitySetMap[rootType] = null;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002D23C File Offset: 0x0002B43C
		private void ProcessEntitySets(HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes)
		{
			this.AssignEntitySetIds(referencedEntitySets);
			this.m_entityTypeToEntitySetMap = new Dictionary<EntityTypeBase, EntitySet>();
			foreach (EntitySet entitySet in referencedEntitySets)
			{
				this.AddEntityTypeToSetEntry(entitySet.ElementType, entitySet);
			}
			foreach (EntityType entityType in freeFloatingEntityConstructorTypes)
			{
				this.AddEntityTypeToSetEntry(entityType, null);
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		private void ProcessDiscriminatorMaps(Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps)
		{
			Dictionary<EntitySetBase, ExplicitDiscriminatorMap> dictionary = null;
			if (discriminatorMaps != null)
			{
				dictionary = new Dictionary<EntitySetBase, ExplicitDiscriminatorMap>(discriminatorMaps.Count, discriminatorMaps.Comparer);
				foreach (KeyValuePair<EntitySetBase, DiscriminatorMapInfo> keyValuePair in discriminatorMaps)
				{
					EntitySetBase key = keyValuePair.Key;
					ExplicitDiscriminatorMap discriminatorMap = keyValuePair.Value.DiscriminatorMap;
					if (discriminatorMap != null)
					{
						EntityTypeBase rootType = StructuredTypeInfo.GetRootType(key.ElementType);
						bool flag = this.GetEntitySet(rootType) != null;
						if (flag)
						{
							dictionary.Add(key, discriminatorMap);
						}
					}
				}
				if (dictionary.Count == 0)
				{
					dictionary = null;
				}
			}
			this.m_discriminatorMaps = dictionary;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002D394 File Offset: 0x0002B594
		private void AssignEntitySetIds(HashSet<EntitySet> referencedEntitySets)
		{
			this.m_entitySetIdToEntitySetMap = new EntitySet[referencedEntitySets.Count];
			this.m_entitySetToEntitySetIdMap = new Dictionary<EntitySet, int>();
			int num = 0;
			foreach (EntitySet entitySet in referencedEntitySets)
			{
				if (!this.m_entitySetToEntitySetIdMap.ContainsKey(entitySet))
				{
					this.m_entitySetIdToEntitySetMap[num] = entitySet;
					this.m_entitySetToEntitySetIdMap[entitySet] = num;
					num++;
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002D424 File Offset: 0x0002B624
		private void ProcessTypes(HashSet<TypeUsage> referencedTypes)
		{
			this.PopulateTypeInfoMap(referencedTypes);
			this.AssignTypeIds();
			this.ExplodeTypes();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002D43C File Offset: 0x0002B63C
		private void PopulateTypeInfoMap(HashSet<TypeUsage> referencedTypes)
		{
			foreach (TypeUsage type in referencedTypes)
			{
				this.CreateTypeInfoForType(type);
			}
			this.m_typeInfoMapPopulated = true;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002D494 File Offset: 0x0002B694
		private bool TryGetDiscriminatorMap(EdmType type, out ExplicitDiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			if (this.m_discriminatorMaps == null)
			{
				return false;
			}
			if (type.BuiltInTypeKind != BuiltInTypeKind.EntityType)
			{
				return false;
			}
			EntityTypeBase rootType = StructuredTypeInfo.GetRootType((EntityType)type);
			EntitySet entitySet;
			return this.m_entityTypeToEntitySetMap.TryGetValue(rootType, out entitySet) && entitySet != null && this.m_discriminatorMaps.TryGetValue(entitySet, out discriminatorMap);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002D4EC File Offset: 0x0002B6EC
		private void CreateTypeInfoForType(TypeUsage type)
		{
			while (TypeUtils.IsCollectionType(type))
			{
				type = TypeHelpers.GetEdmType<CollectionType>(type).TypeUsage;
			}
			if (TypeUtils.IsStructuredType(type))
			{
				ExplicitDiscriminatorMap discriminatorMap;
				this.TryGetDiscriminatorMap(type.EdmType, out discriminatorMap);
				this.CreateTypeInfoForStructuredType(type, discriminatorMap);
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002D530 File Offset: 0x0002B730
		private TypeInfo CreateTypeInfoForStructuredType(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap)
		{
			PlanCompiler.Assert(TypeUtils.IsStructuredType(type), "expected structured type. Found " + ((type != null) ? type.ToString() : null));
			TypeInfo typeInfo = this.GetTypeInfo(type);
			if (typeInfo != null)
			{
				return typeInfo;
			}
			TypeInfo superTypeInfo = null;
			RefType refType;
			if (type.EdmType.BaseType != null)
			{
				superTypeInfo = this.CreateTypeInfoForStructuredType(TypeUsage.Create(type.EdmType.BaseType), discriminatorMap);
			}
			else if (TypeHelpers.TryGetEdmType<RefType>(type, out refType))
			{
				EntityType entityType = refType.ElementType as EntityType;
				if (entityType != null && entityType.BaseType != null)
				{
					TypeUsage type2 = TypeHelpers.CreateReferenceTypeUsage(entityType.BaseType as EntityType);
					superTypeInfo = this.CreateTypeInfoForStructuredType(type2, discriminatorMap);
				}
			}
			foreach (object obj in TypeHelpers.GetDeclaredStructuralMembers(type))
			{
				EdmMember edmMember = (EdmMember)obj;
				this.CreateTypeInfoForType(edmMember.TypeUsage);
			}
			EntityTypeBase entityType2;
			if (TypeHelpers.TryGetEdmType<EntityTypeBase>(type, out entityType2))
			{
				foreach (RelProperty relProperty in this.m_relPropertyHelper.GetDeclaredOnlyRelProperties(entityType2))
				{
					this.CreateTypeInfoForType(relProperty.ToEnd.TypeUsage);
				}
			}
			typeInfo = TypeInfo.Create(type, superTypeInfo, discriminatorMap);
			this.m_typeInfoMap.Add(type, typeInfo);
			return typeInfo;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
		private void AssignTypeIds()
		{
			int num = 0;
			foreach (KeyValuePair<TypeUsage, TypeInfo> keyValuePair in this.m_typeInfoMap)
			{
				if (keyValuePair.Value.RootType.DiscriminatorMap != null)
				{
					EntityType entityType = (EntityType)keyValuePair.Key.EdmType;
					keyValuePair.Value.TypeId = keyValuePair.Value.RootType.DiscriminatorMap.GetTypeId(entityType);
				}
				else if (keyValuePair.Value.IsRootType && (TypeSemantics.IsEntityType(keyValuePair.Key) || TypeSemantics.IsComplexType(keyValuePair.Key)))
				{
					this.AssignRootTypeId(keyValuePair.Value, string.Format(CultureInfo.InvariantCulture, "{0}X", new object[]
					{
						num
					}));
					num++;
				}
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002D79C File Offset: 0x0002B99C
		private void AssignRootTypeId(TypeInfo typeInfo, string typeId)
		{
			typeInfo.TypeId = typeId;
			this.AssignTypeIdsToSubTypes(typeInfo);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002D7AC File Offset: 0x0002B9AC
		private void AssignTypeIdsToSubTypes(TypeInfo typeInfo)
		{
			int num = 0;
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.AssignTypeId(typeInfo2, num);
				num++;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002D808 File Offset: 0x0002BA08
		private void AssignTypeId(TypeInfo typeInfo, int subtypeNum)
		{
			typeInfo.TypeId = string.Format(CultureInfo.InvariantCulture, "{0}{1}X", new object[]
			{
				typeInfo.SuperType.TypeId,
				subtypeNum
			});
			this.AssignTypeIdsToSubTypes(typeInfo);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002D843 File Offset: 0x0002BA43
		private bool NeedsTypeIdProperty(TypeInfo typeInfo)
		{
			return typeInfo.ImmediateSubTypes.Count > 0 && !TypeSemantics.IsReferenceType(typeInfo.Type);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002D863 File Offset: 0x0002BA63
		private bool NeedsNullSentinelProperty(TypeInfo typeInfo)
		{
			return this.m_typesNeedingNullSentinel.Contains(typeInfo.Type.EdmType.Identity);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002D880 File Offset: 0x0002BA80
		private bool NeedsEntitySetIdProperty(TypeInfo typeInfo)
		{
			RefType refType = typeInfo.Type.EdmType as RefType;
			EntityType entityType;
			if (refType != null)
			{
				entityType = (refType.ElementType as EntityType);
			}
			else
			{
				entityType = (typeInfo.Type.EdmType as EntityType);
			}
			return entityType != null && this.GetEntitySet(entityType) == null;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002D8D4 File Offset: 0x0002BAD4
		private void ExplodeTypes()
		{
			foreach (KeyValuePair<TypeUsage, TypeInfo> keyValuePair in this.m_typeInfoMap)
			{
				if (keyValuePair.Value.IsRootType)
				{
					this.ExplodeType(keyValuePair.Value);
				}
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002D93C File Offset: 0x0002BB3C
		private TypeInfo ExplodeType(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type))
			{
				TypeInfo typeInfo = this.GetTypeInfo(type);
				this.ExplodeType(typeInfo);
				return typeInfo;
			}
			if (TypeUtils.IsCollectionType(type))
			{
				TypeUsage typeUsage = TypeHelpers.GetEdmType<CollectionType>(type).TypeUsage;
				this.ExplodeType(typeUsage);
				return null;
			}
			return null;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002D981 File Offset: 0x0002BB81
		private void ExplodeType(TypeInfo typeInfo)
		{
			this.ExplodeRootStructuredType(typeInfo.RootType);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002D990 File Offset: 0x0002BB90
		private void ExplodeRootStructuredType(RootTypeInfo rootType)
		{
			if (rootType.FlattenedType != null)
			{
				return;
			}
			if (this.NeedsTypeIdProperty(rootType))
			{
				rootType.AddPropertyRef(TypeIdPropertyRef.Instance);
				if (rootType.DiscriminatorMap != null)
				{
					rootType.TypeIdKind = TypeIdKind.UserSpecified;
					rootType.TypeIdType = Helper.GetModelTypeUsage(rootType.DiscriminatorMap.DiscriminatorProperty);
				}
				else
				{
					rootType.TypeIdKind = TypeIdKind.Generated;
					rootType.TypeIdType = this.m_stringType;
				}
			}
			if (this.NeedsEntitySetIdProperty(rootType))
			{
				rootType.AddPropertyRef(EntitySetIdPropertyRef.Instance);
			}
			if (this.NeedsNullSentinelProperty(rootType))
			{
				rootType.AddPropertyRef(NullSentinelPropertyRef.Instance);
			}
			this.ExplodeRootStructuredTypeHelper(rootType);
			if (TypeSemantics.IsEntityType(rootType.Type))
			{
				this.AddRelProperties(rootType);
			}
			this.CreateFlattenedRecordType(rootType);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002DA40 File Offset: 0x0002BC40
		private void ExplodeRootStructuredTypeHelper(TypeInfo typeInfo)
		{
			RootTypeInfo rootType = typeInfo.RootType;
			RefType refType;
			IEnumerable enumerable;
			if (TypeHelpers.TryGetEdmType<RefType>(typeInfo.Type, out refType))
			{
				if (!typeInfo.IsRootType)
				{
					return;
				}
				enumerable = refType.ElementType.KeyMembers;
			}
			else
			{
				enumerable = TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type);
			}
			foreach (object obj in enumerable)
			{
				EdmMember edmMember = (EdmMember)obj;
				TypeInfo typeInfo2 = this.ExplodeType(edmMember.TypeUsage);
				if (typeInfo2 == null)
				{
					rootType.AddPropertyRef(new SimplePropertyRef(edmMember));
				}
				else
				{
					foreach (PropertyRef propertyRef in typeInfo2.PropertyRefList)
					{
						rootType.AddPropertyRef(propertyRef.CreateNestedPropertyRef(edmMember));
					}
				}
			}
			foreach (TypeInfo typeInfo3 in typeInfo.ImmediateSubTypes)
			{
				this.ExplodeRootStructuredTypeHelper(typeInfo3);
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002DB80 File Offset: 0x0002BD80
		private void AddRelProperties(TypeInfo typeInfo)
		{
			EntityTypeBase entityType = (EntityTypeBase)typeInfo.Type.EdmType;
			foreach (RelProperty relProperty in this.m_relPropertyHelper.GetDeclaredOnlyRelProperties(entityType))
			{
				EdmType edmType = relProperty.ToEnd.TypeUsage.EdmType;
				TypeInfo typeInfo2 = this.GetTypeInfo(relProperty.ToEnd.TypeUsage);
				this.ExplodeType(typeInfo2);
				foreach (PropertyRef propertyRef in typeInfo2.PropertyRefList)
				{
					typeInfo.RootType.AddPropertyRef(propertyRef.CreateNestedPropertyRef(relProperty));
				}
			}
			foreach (TypeInfo typeInfo3 in typeInfo.ImmediateSubTypes)
			{
				this.AddRelProperties(typeInfo3);
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		private void CreateFlattenedRecordType(RootTypeInfo type)
		{
			bool flag = TypeSemantics.IsEntityType(type.Type) && type.ImmediateSubTypes.Count == 0;
			List<KeyValuePair<string, TypeUsage>> list = new List<KeyValuePair<string, TypeUsage>>();
			HashSet<string> hashSet = new HashSet<string>();
			int num = 0;
			foreach (PropertyRef propertyRef in type.PropertyRefList)
			{
				string text = null;
				if (flag)
				{
					SimplePropertyRef simplePropertyRef = propertyRef as SimplePropertyRef;
					if (simplePropertyRef != null)
					{
						text = simplePropertyRef.Property.Name;
					}
				}
				if (text == null)
				{
					text = "F" + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				while (hashSet.Contains(text))
				{
					text = "F" + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				TypeUsage propertyType = this.GetPropertyType(type, propertyRef);
				list.Add(new KeyValuePair<string, TypeUsage>(text, propertyType));
				hashSet.Add(text);
			}
			type.FlattenedType = TypeHelpers.CreateRowType(list);
			IEnumerator<PropertyRef> enumerator2 = type.PropertyRefList.GetEnumerator();
			foreach (EdmProperty newProperty in type.FlattenedType.Properties)
			{
				if (!enumerator2.MoveNext())
				{
					PlanCompiler.Assert(false, "property refs count and flattened type member count mismatch?");
				}
				type.AddPropertyMapping(enumerator2.Current, newProperty);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002DE2C File Offset: 0x0002C02C
		private TypeUsage GetNewType(TypeUsage type)
		{
			if (TypeUtils.IsStructuredType(type))
			{
				TypeInfo typeInfo = this.GetTypeInfo(type);
				return typeInfo.FlattenedTypeUsage;
			}
			TypeUsage typeUsage;
			if (TypeHelpers.TryGetCollectionElementType(type, out typeUsage))
			{
				TypeUsage newType = this.GetNewType(typeUsage);
				if (newType.EdmEquals(typeUsage))
				{
					return type;
				}
				return TypeHelpers.CreateCollectionTypeUsage(newType);
			}
			else
			{
				if (TypeUtils.IsEnumerationType(type))
				{
					return TypeHelpers.CreateEnumUnderlyingTypeUsage(type);
				}
				if (TypeSemantics.IsStrongSpatialType(type))
				{
					return TypeHelpers.CreateSpatialUnionTypeUsage(type);
				}
				return type;
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002DE94 File Offset: 0x0002C094
		private TypeUsage GetPropertyType(RootTypeInfo typeInfo, PropertyRef p)
		{
			TypeUsage typeUsage = null;
			PropertyRef propertyRef = null;
			while (p is NestedPropertyRef)
			{
				NestedPropertyRef nestedPropertyRef = (NestedPropertyRef)p;
				p = nestedPropertyRef.OuterProperty;
				propertyRef = nestedPropertyRef.InnerProperty;
			}
			if (p is TypeIdPropertyRef)
			{
				if (propertyRef != null && propertyRef is SimplePropertyRef)
				{
					TypeUsage typeUsage2 = ((SimplePropertyRef)propertyRef).Property.TypeUsage;
					TypeInfo typeInfo2 = this.GetTypeInfo(typeUsage2);
					typeUsage = typeInfo2.RootType.TypeIdType;
				}
				else
				{
					typeUsage = typeInfo.TypeIdType;
				}
			}
			else if (p is EntitySetIdPropertyRef || p is NullSentinelPropertyRef)
			{
				typeUsage = this.m_intType;
			}
			else if (p is RelPropertyRef)
			{
				typeUsage = (p as RelPropertyRef).Property.ToEnd.TypeUsage;
			}
			else
			{
				SimplePropertyRef simplePropertyRef = p as SimplePropertyRef;
				if (simplePropertyRef != null)
				{
					typeUsage = Helper.GetModelTypeUsage(simplePropertyRef.Property);
				}
			}
			typeUsage = this.GetNewType(typeUsage);
			PlanCompiler.Assert(typeUsage != null, "unrecognized property type?");
			return typeUsage;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002DF73 File Offset: 0x0002C173
		private static EntityTypeBase GetRootType(EntityTypeBase type)
		{
			while (type.BaseType != null)
			{
				type = (EntityTypeBase)type.BaseType;
			}
			return type;
		}

		// Token: 0x04000802 RID: 2050
		private TypeUsage m_stringType;

		// Token: 0x04000803 RID: 2051
		private TypeUsage m_intType;

		// Token: 0x04000804 RID: 2052
		private Dictionary<TypeUsage, TypeInfo> m_typeInfoMap;

		// Token: 0x04000805 RID: 2053
		private bool m_typeInfoMapPopulated;

		// Token: 0x04000806 RID: 2054
		private EntitySet[] m_entitySetIdToEntitySetMap;

		// Token: 0x04000807 RID: 2055
		private Dictionary<EntitySet, int> m_entitySetToEntitySetIdMap;

		// Token: 0x04000808 RID: 2056
		private Dictionary<EntityTypeBase, EntitySet> m_entityTypeToEntitySetMap;

		// Token: 0x04000809 RID: 2057
		private Dictionary<EntitySetBase, ExplicitDiscriminatorMap> m_discriminatorMaps;

		// Token: 0x0400080A RID: 2058
		private RelPropertyHelper m_relPropertyHelper;

		// Token: 0x0400080B RID: 2059
		private HashSet<string> m_typesNeedingNullSentinel;
	}
}
