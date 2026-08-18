using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000046 RID: 70
	internal class ColumnMapProcessor
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00018E68 File Offset: 0x00017068
		internal ColumnMap ExpandColumnMap()
		{
			if (this.m_varInfo.Kind == VarInfoKind.CollectionVarInfo)
			{
				return new VarRefColumnMap(this.m_columnMap.Var.Type, this.m_columnMap.Name, ((CollectionVarInfo)this.m_varInfo).NewVar);
			}
			if (this.m_varInfo.Kind == VarInfoKind.PrimitiveTypeVarInfo)
			{
				return new VarRefColumnMap(this.m_columnMap.Var.Type, this.m_columnMap.Name, ((PrimitiveTypeVarInfo)this.m_varInfo).NewVar);
			}
			return this.CreateColumnMap(this.m_columnMap.Var.Type, this.m_columnMap.Name);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018F14 File Offset: 0x00017114
		internal ColumnMapProcessor(VarRefColumnMap columnMap, VarInfo varInfo, StructuredTypeInfo typeInfo)
		{
			this.m_columnMap = columnMap;
			this.m_varInfo = varInfo;
			PlanCompiler.Assert(varInfo.NewVars != null && varInfo.NewVars.Count > 0, "No new Vars specified");
			this.m_varList = varInfo.NewVars.GetEnumerator();
			this.m_typeInfo = typeInfo;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00018F75 File Offset: 0x00017175
		private Var GetNextVar()
		{
			if (this.m_varList.MoveNext())
			{
				return this.m_varList.Current;
			}
			PlanCompiler.Assert(false, "Could not GetNextVar");
			return null;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00018F9C File Offset: 0x0001719C
		private ColumnMap CreateColumnMap(TypeUsage type, string name)
		{
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.CreateSimpleColumnMap(type, name);
			}
			return this.CreateStructuralColumnMap(type, name);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00018FB8 File Offset: 0x000171B8
		private ComplexTypeColumnMap CreateComplexTypeColumnMap(TypeInfo typeInfo, string name, ComplexTypeColumnMap superTypeColumnMap, Dictionary<object, TypedColumnMap> discriminatorMap, List<TypedColumnMap> allMaps)
		{
			List<ColumnMap> list = new List<ColumnMap>();
			SimpleColumnMap nullSentinel = null;
			if (typeInfo.HasNullSentinelProperty)
			{
				nullSentinel = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(typeInfo.NullSentinelProperty), "__NullSentinel");
			}
			IEnumerable enumerable;
			if (superTypeColumnMap != null)
			{
				foreach (ColumnMap item in superTypeColumnMap.Properties)
				{
					list.Add(item);
				}
				enumerable = TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type);
			}
			else
			{
				enumerable = TypeHelpers.GetAllStructuralMembers(typeInfo.Type);
			}
			foreach (object obj in enumerable)
			{
				EdmMember edmMember = (EdmMember)obj;
				ColumnMap item2 = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
				list.Add(item2);
			}
			ComplexTypeColumnMap complexTypeColumnMap = new ComplexTypeColumnMap(typeInfo.Type, name, list.ToArray(), nullSentinel);
			if (discriminatorMap != null)
			{
				discriminatorMap[typeInfo.TypeId] = complexTypeColumnMap;
			}
			if (allMaps != null)
			{
				allMaps.Add(complexTypeColumnMap);
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.CreateComplexTypeColumnMap(typeInfo2, name, complexTypeColumnMap, discriminatorMap, allMaps);
			}
			return complexTypeColumnMap;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001911C File Offset: 0x0001731C
		private EntityColumnMap CreateEntityColumnMap(TypeInfo typeInfo, string name, EntityColumnMap superTypeColumnMap, Dictionary<object, TypedColumnMap> discriminatorMap, List<TypedColumnMap> allMaps, bool handleRelProperties)
		{
			EntityColumnMap entityColumnMap = null;
			List<ColumnMap> list = new List<ColumnMap>();
			if (superTypeColumnMap != null)
			{
				foreach (ColumnMap item in superTypeColumnMap.Properties)
				{
					list.Add(item);
				}
				foreach (object obj in TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type))
				{
					EdmMember edmMember = (EdmMember)obj;
					ColumnMap item2 = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
					list.Add(item2);
				}
				entityColumnMap = new EntityColumnMap(typeInfo.Type, name, list.ToArray(), superTypeColumnMap.EntityIdentity);
			}
			else
			{
				SimpleColumnMap entitySetIdColumnMap = null;
				if (typeInfo.HasEntitySetIdProperty)
				{
					entitySetIdColumnMap = this.CreateEntitySetIdColumnMap(typeInfo.EntitySetIdProperty);
				}
				List<SimpleColumnMap> list2 = new List<SimpleColumnMap>();
				Dictionary<EdmProperty, ColumnMap> dictionary = new Dictionary<EdmProperty, ColumnMap>();
				foreach (object obj2 in TypeHelpers.GetDeclaredStructuralMembers(typeInfo.Type))
				{
					EdmMember edmMember2 = (EdmMember)obj2;
					ColumnMap columnMap = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember2), edmMember2.Name);
					list.Add(columnMap);
					if (TypeSemantics.IsPartOfKey(edmMember2))
					{
						EdmProperty edmProperty = edmMember2 as EdmProperty;
						PlanCompiler.Assert(edmProperty != null, "EntityType key member is not property?");
						dictionary[edmProperty] = columnMap;
					}
				}
				foreach (EdmMember edmMember3 in TypeHelpers.GetEdmType<EntityType>(typeInfo.Type).KeyMembers)
				{
					EdmProperty edmProperty2 = edmMember3 as EdmProperty;
					PlanCompiler.Assert(edmProperty2 != null, "EntityType key member is not property?");
					SimpleColumnMap simpleColumnMap = dictionary[edmProperty2] as SimpleColumnMap;
					PlanCompiler.Assert(simpleColumnMap != null, "keyColumnMap is null");
					list2.Add(simpleColumnMap);
				}
				EntityIdentity entityIdentity = this.CreateEntityIdentity((EntityType)typeInfo.Type.EdmType, entitySetIdColumnMap, list2.ToArray());
				entityColumnMap = new EntityColumnMap(typeInfo.Type, name, list.ToArray(), entityIdentity);
			}
			if (discriminatorMap != null && typeInfo.TypeId != null)
			{
				discriminatorMap[typeInfo.TypeId] = entityColumnMap;
			}
			if (allMaps != null)
			{
				allMaps.Add(entityColumnMap);
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.CreateEntityColumnMap(typeInfo2, name, entityColumnMap, discriminatorMap, allMaps, false);
			}
			if (handleRelProperties)
			{
				this.BuildRelPropertyColumnMaps(typeInfo, true);
			}
			return entityColumnMap;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000193E4 File Offset: 0x000175E4
		private void BuildRelPropertyColumnMaps(TypeInfo typeInfo, bool includeSupertypeRelProperties)
		{
			IEnumerable<RelProperty> enumerable;
			if (includeSupertypeRelProperties)
			{
				enumerable = this.m_typeInfo.RelPropertyHelper.GetRelProperties(typeInfo.Type.EdmType as EntityTypeBase);
			}
			else
			{
				enumerable = this.m_typeInfo.RelPropertyHelper.GetDeclaredOnlyRelProperties(typeInfo.Type.EdmType as EntityTypeBase);
			}
			foreach (RelProperty relProperty in enumerable)
			{
				ColumnMap columnMap = this.CreateColumnMap(relProperty.ToEnd.TypeUsage, relProperty.ToString());
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.BuildRelPropertyColumnMaps(typeInfo2, false);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000194CC File Offset: 0x000176CC
		private SimpleColumnMap CreateEntitySetIdColumnMap(EdmProperty prop)
		{
			return this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(prop), "__EntitySetId");
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000194E0 File Offset: 0x000176E0
		private SimplePolymorphicColumnMap CreatePolymorphicColumnMap(TypeInfo typeInfo, string name)
		{
			Dictionary<object, TypedColumnMap> dictionary = new Dictionary<object, TypedColumnMap>((typeInfo.RootType.DiscriminatorMap == null) ? null : TrailingSpaceComparer.Instance);
			List<TypedColumnMap> list = new List<TypedColumnMap>();
			TypeInfo rootType = typeInfo.RootType;
			SimpleColumnMap typeDiscriminator = this.CreateTypeIdColumnMap(rootType.TypeIdProperty);
			if (TypeSemantics.IsComplexType(typeInfo.Type))
			{
				TypedColumnMap typedColumnMap = this.CreateComplexTypeColumnMap(rootType, name, null, dictionary, list);
			}
			else
			{
				TypedColumnMap typedColumnMap = this.CreateEntityColumnMap(rootType, name, null, dictionary, list, true);
			}
			TypedColumnMap typedColumnMap2 = null;
			foreach (TypedColumnMap typedColumnMap3 in list)
			{
				if (TypeSemantics.IsStructurallyEqual(typedColumnMap3.Type, typeInfo.Type))
				{
					typedColumnMap2 = typedColumnMap3;
					break;
				}
			}
			PlanCompiler.Assert(typedColumnMap2 != null, "Didn't find requested type in polymorphic type hierarchy?");
			return new SimplePolymorphicColumnMap(typeInfo.Type, name, typedColumnMap2.Properties, typeDiscriminator, dictionary);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000195D0 File Offset: 0x000177D0
		private RecordColumnMap CreateRecordColumnMap(TypeInfo typeInfo, string name)
		{
			PlanCompiler.Assert(typeInfo.Type.EdmType is RowType, "not RowType");
			SimpleColumnMap nullSentinel = null;
			if (typeInfo.HasNullSentinelProperty)
			{
				nullSentinel = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(typeInfo.NullSentinelProperty), "__NullSentinel");
			}
			ReadOnlyMetadataCollection<EdmProperty> properties = TypeHelpers.GetProperties(typeInfo.Type);
			ColumnMap[] array = new ColumnMap[properties.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember edmMember = properties[i];
				array[i] = this.CreateColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
			}
			return new RecordColumnMap(typeInfo.Type, name, array, nullSentinel);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00019678 File Offset: 0x00017878
		private RefColumnMap CreateRefColumnMap(TypeInfo typeInfo, string name)
		{
			SimpleColumnMap entitySetIdColumnMap = null;
			if (typeInfo.HasEntitySetIdProperty)
			{
				entitySetIdColumnMap = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(typeInfo.EntitySetIdProperty), "__EntitySetId");
			}
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(typeInfo.Type).ElementType;
			SimpleColumnMap[] array = new SimpleColumnMap[entityType.KeyMembers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember edmMember = entityType.KeyMembers[i];
				array[i] = this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(edmMember), edmMember.Name);
			}
			EntityIdentity entityIdentity = this.CreateEntityIdentity(entityType, entitySetIdColumnMap, array);
			return new RefColumnMap(typeInfo.Type, name, entityIdentity);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00019724 File Offset: 0x00017924
		private SimpleColumnMap CreateSimpleColumnMap(TypeUsage type, string name)
		{
			Var nextVar = this.GetNextVar();
			return new VarRefColumnMap(type, name, nextVar);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00019742 File Offset: 0x00017942
		private SimpleColumnMap CreateTypeIdColumnMap(EdmProperty prop)
		{
			return this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(prop), "__TypeId");
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00019758 File Offset: 0x00017958
		private ColumnMap CreateStructuralColumnMap(TypeUsage type, string name)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			if (TypeSemantics.IsRowType(type))
			{
				return this.CreateRecordColumnMap(typeInfo, name);
			}
			if (TypeSemantics.IsReferenceType(type))
			{
				return this.CreateRefColumnMap(typeInfo, name);
			}
			if (typeInfo.HasTypeIdProperty)
			{
				return this.CreatePolymorphicColumnMap(typeInfo, name);
			}
			if (TypeSemantics.IsComplexType(type))
			{
				return this.CreateComplexTypeColumnMap(typeInfo, name, null, null, null);
			}
			if (TypeSemantics.IsEntityType(type))
			{
				return this.CreateEntityColumnMap(typeInfo, name, null, null, null, true);
			}
			throw EntityUtil.NotSupported(type.Identity);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000197DC File Offset: 0x000179DC
		private EntityIdentity CreateEntityIdentity(EntityType entityType, SimpleColumnMap entitySetIdColumnMap, SimpleColumnMap[] keyColumnMaps)
		{
			if (entitySetIdColumnMap != null)
			{
				return new DiscriminatedEntityIdentity(entitySetIdColumnMap, this.m_typeInfo.EntitySetIdToEntitySetMap, keyColumnMaps);
			}
			EntitySet entitySet = this.m_typeInfo.GetEntitySet(entityType);
			PlanCompiler.Assert(entitySet != null, "Expected non-null entityset when no entitysetid is required. Entity type = " + ((entityType != null) ? entityType.ToString() : null));
			return new SimpleEntityIdentity(entitySet, keyColumnMaps);
		}

		// Token: 0x0400075E RID: 1886
		private IEnumerator<Var> m_varList;

		// Token: 0x0400075F RID: 1887
		private VarInfo m_varInfo;

		// Token: 0x04000760 RID: 1888
		private VarRefColumnMap m_columnMap;

		// Token: 0x04000761 RID: 1889
		private StructuredTypeInfo m_typeInfo;

		// Token: 0x04000762 RID: 1890
		private const string c_TypeIdColumnName = "__TypeId";

		// Token: 0x04000763 RID: 1891
		private const string c_EntitySetIdColumnName = "__EntitySetId";

		// Token: 0x04000764 RID: 1892
		private const string c_NullSentinelColumnName = "__NullSentinel";
	}
}
