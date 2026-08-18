using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A1 RID: 1697
	internal class StructuredTypeInfo
	{
		// Token: 0x0600432D RID: 17197 RVA: 0x0013E986 File Offset: 0x0013CB86
		private StructuredTypeInfo(HashSet<string> typesNeedingNullSentinel)
		{
			this.m_typeInfoMap = new Dictionary<TypeUsage, TypeInfo>(TypeUsageEqualityComparer.Instance);
			this.m_typeInfoMapPopulated = false;
			this.m_typesNeedingNullSentinel = typesNeedingNullSentinel;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0013E9AC File Offset: 0x0013CBAC
		internal static void Process(Command itree, HashSet<TypeUsage> referencedTypes, HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes, Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps, RelPropertyHelper relPropertyHelper, HashSet<string> typesNeedingNullSentinel, out StructuredTypeInfo structuredTypeInfo)
		{
			structuredTypeInfo = new StructuredTypeInfo(typesNeedingNullSentinel);
			structuredTypeInfo.Process(itree, referencedTypes, referencedEntitySets, freeFloatingEntityConstructorTypes, discriminatorMaps, relPropertyHelper);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0013E9C8 File Offset: 0x0013CBC8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "itree")]
		private void Process(Command itree, HashSet<TypeUsage> referencedTypes, HashSet<EntitySet> referencedEntitySets, HashSet<EntityType> freeFloatingEntityConstructorTypes, Dictionary<EntitySetBase, DiscriminatorMapInfo> discriminatorMaps, RelPropertyHelper relPropertyHelper)
		{
			PlanCompiler.Assert(null != itree, "null itree?");
			this.m_stringType = itree.StringType;
			this.m_intType = itree.IntegerType;
			this.m_relPropertyHelper = relPropertyHelper;
			this.ProcessEntitySets(referencedEntitySets, freeFloatingEntityConstructorTypes);
			this.ProcessDiscriminatorMaps(discriminatorMaps);
			this.ProcessTypes(referencedTypes);
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x0013EA1E File Offset: 0x0013CC1E
		internal EntitySet[] EntitySetIdToEntitySetMap
		{
			get
			{
				return this.m_entitySetIdToEntitySetMap;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x0013EA26 File Offset: 0x0013CC26
		internal RelPropertyHelper RelPropertyHelper
		{
			get
			{
				return this.m_relPropertyHelper;
			}
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x0013EA30 File Offset: 0x0013CC30
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

		// Token: 0x06004333 RID: 17203 RVA: 0x0013EA58 File Offset: 0x0013CC58
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal int GetEntitySetId(EntitySet e)
		{
			int result = 0;
			if (!this.m_entitySetToEntitySetIdMap.TryGetValue(e, out result))
			{
				PlanCompiler.Assert(false, "no such entity set?");
			}
			return result;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x0013EA83 File Offset: 0x0013CC83
		internal Set<EntitySet> GetEntitySets()
		{
			return new Set<EntitySet>(this.m_entitySetIdToEntitySetMap).MakeReadOnly();
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0013EA98 File Offset: 0x0013CC98
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "typeInfo")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal TypeInfo GetTypeInfo(TypeUsage type)
		{
			if (!TypeUtils.IsStructuredType(type))
			{
				return null;
			}
			TypeInfo result = null;
			if (!this.m_typeInfoMap.TryGetValue(type, out result))
			{
				PlanCompiler.Assert(!TypeUtils.IsStructuredType(type) || !this.m_typeInfoMapPopulated, "cannot find typeInfo for type " + type);
			}
			return result;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x0013EAE8 File Offset: 0x0013CCE8
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

		// Token: 0x06004337 RID: 17207 RVA: 0x0013EB3C File Offset: 0x0013CD3C
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

		// Token: 0x06004338 RID: 17208 RVA: 0x0013EBE0 File Offset: 0x0013CDE0
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

		// Token: 0x06004339 RID: 17209 RVA: 0x0013EC94 File Offset: 0x0013CE94
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

		// Token: 0x0600433A RID: 17210 RVA: 0x0013ED24 File Offset: 0x0013CF24
		private void ProcessTypes(HashSet<TypeUsage> referencedTypes)
		{
			this.PopulateTypeInfoMap(referencedTypes);
			this.AssignTypeIds();
			this.ExplodeTypes();
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0013ED3C File Offset: 0x0013CF3C
		private void PopulateTypeInfoMap(HashSet<TypeUsage> referencedTypes)
		{
			foreach (TypeUsage type in referencedTypes)
			{
				this.CreateTypeInfoForType(type);
			}
			this.m_typeInfoMapPopulated = true;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x0013ED94 File Offset: 0x0013CF94
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

		// Token: 0x0600433D RID: 17213 RVA: 0x0013EDEC File Offset: 0x0013CFEC
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

		// Token: 0x0600433E RID: 17214 RVA: 0x0013EE30 File Offset: 0x0013D030
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private TypeInfo CreateTypeInfoForStructuredType(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap)
		{
			PlanCompiler.Assert(TypeUtils.IsStructuredType(type), "expected structured type. Found " + type);
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

		// Token: 0x0600433F RID: 17215 RVA: 0x0013EF98 File Offset: 0x0013D198
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

		// Token: 0x06004340 RID: 17216 RVA: 0x0013F094 File Offset: 0x0013D294
		private void AssignRootTypeId(TypeInfo typeInfo, string typeId)
		{
			typeInfo.TypeId = typeId;
			this.AssignTypeIdsToSubTypes(typeInfo);
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x0013F0A4 File Offset: 0x0013D2A4
		private void AssignTypeIdsToSubTypes(TypeInfo typeInfo)
		{
			int num = 0;
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.AssignTypeId(typeInfo2, num);
				num++;
			}
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x0013F100 File Offset: 0x0013D300
		private void AssignTypeId(TypeInfo typeInfo, int subtypeNum)
		{
			typeInfo.TypeId = string.Format(CultureInfo.InvariantCulture, "{0}{1}X", new object[]
			{
				typeInfo.SuperType.TypeId,
				subtypeNum
			});
			this.AssignTypeIdsToSubTypes(typeInfo);
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x0013F148 File Offset: 0x0013D348
		private static bool NeedsTypeIdProperty(TypeInfo typeInfo)
		{
			return typeInfo.ImmediateSubTypes.Count > 0 && !TypeSemantics.IsReferenceType(typeInfo.Type);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x0013F168 File Offset: 0x0013D368
		private bool NeedsNullSentinelProperty(TypeInfo typeInfo)
		{
			return this.m_typesNeedingNullSentinel.Contains(typeInfo.Type.EdmType.Identity);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x0013F188 File Offset: 0x0013D388
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

		// Token: 0x06004346 RID: 17222 RVA: 0x0013F1DC File Offset: 0x0013D3DC
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

		// Token: 0x06004347 RID: 17223 RVA: 0x0013F244 File Offset: 0x0013D444
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

		// Token: 0x06004348 RID: 17224 RVA: 0x0013F289 File Offset: 0x0013D489
		private void ExplodeType(TypeInfo typeInfo)
		{
			this.ExplodeRootStructuredType(typeInfo.RootType);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x0013F298 File Offset: 0x0013D498
		private void ExplodeRootStructuredType(RootTypeInfo rootType)
		{
			if (rootType.FlattenedType != null)
			{
				return;
			}
			if (StructuredTypeInfo.NeedsTypeIdProperty(rootType))
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

		// Token: 0x0600434A RID: 17226 RVA: 0x0013F348 File Offset: 0x0013D548
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

		// Token: 0x0600434B RID: 17227 RVA: 0x0013F488 File Offset: 0x0013D688
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

		// Token: 0x0600434C RID: 17228 RVA: 0x0013F5A8 File Offset: 0x0013D7A8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600434D RID: 17229 RVA: 0x0013F734 File Offset: 0x0013D934
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

		// Token: 0x0600434E RID: 17230 RVA: 0x0013F79C File Offset: 0x0013D99C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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
				SimplePropertyRef simplePropertyRef = (SimplePropertyRef)propertyRef;
				if (simplePropertyRef != null)
				{
					TypeUsage typeUsage2 = simplePropertyRef.Property.TypeUsage;
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
				typeUsage = ((RelPropertyRef)p).Property.ToEnd.TypeUsage;
			}
			else
			{
				SimplePropertyRef simplePropertyRef2 = p as SimplePropertyRef;
				if (simplePropertyRef2 != null)
				{
					typeUsage = Helper.GetModelTypeUsage(simplePropertyRef2.Property);
				}
			}
			typeUsage = this.GetNewType(typeUsage);
			PlanCompiler.Assert(null != typeUsage, "unrecognized property type?");
			return typeUsage;
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x0013F87A File Offset: 0x0013DA7A
		private static EntityTypeBase GetRootType(EntityTypeBase type)
		{
			while (type.BaseType != null)
			{
				type = (EntityTypeBase)type.BaseType;
			}
			return type;
		}

		// Token: 0x040018E0 RID: 6368
		private TypeUsage m_stringType;

		// Token: 0x040018E1 RID: 6369
		private TypeUsage m_intType;

		// Token: 0x040018E2 RID: 6370
		private readonly Dictionary<TypeUsage, TypeInfo> m_typeInfoMap;

		// Token: 0x040018E3 RID: 6371
		private bool m_typeInfoMapPopulated;

		// Token: 0x040018E4 RID: 6372
		private EntitySet[] m_entitySetIdToEntitySetMap;

		// Token: 0x040018E5 RID: 6373
		private Dictionary<EntitySet, int> m_entitySetToEntitySetIdMap;

		// Token: 0x040018E6 RID: 6374
		private Dictionary<EntityTypeBase, EntitySet> m_entityTypeToEntitySetMap;

		// Token: 0x040018E7 RID: 6375
		private Dictionary<EntitySetBase, ExplicitDiscriminatorMap> m_discriminatorMaps;

		// Token: 0x040018E8 RID: 6376
		private RelPropertyHelper m_relPropertyHelper;

		// Token: 0x040018E9 RID: 6377
		private readonly HashSet<string> m_typesNeedingNullSentinel;
	}
}
