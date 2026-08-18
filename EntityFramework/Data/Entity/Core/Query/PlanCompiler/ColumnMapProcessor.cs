using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200065B RID: 1627
	internal class ColumnMapProcessor
	{
		// Token: 0x06003F8B RID: 16267 RVA: 0x00122ACC File Offset: 0x00120CCC
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

		// Token: 0x06003F8C RID: 16268 RVA: 0x00122B78 File Offset: 0x00120D78
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Vars")]
		internal ColumnMapProcessor(VarRefColumnMap columnMap, VarInfo varInfo, StructuredTypeInfo typeInfo)
		{
			this.m_columnMap = columnMap;
			this.m_varInfo = varInfo;
			PlanCompiler.Assert(varInfo.NewVars != null && varInfo.NewVars.Count > 0, "No new Vars specified");
			this.m_varList = varInfo.NewVars.GetEnumerator();
			this.m_typeInfo = typeInfo;
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x00122BD9 File Offset: 0x00120DD9
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetNextVar")]
		private Var GetNextVar()
		{
			if (this.m_varList.MoveNext())
			{
				return this.m_varList.Current;
			}
			PlanCompiler.Assert(false, "Could not GetNextVar");
			return null;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x00122C00 File Offset: 0x00120E00
		private ColumnMap CreateColumnMap(TypeUsage type, string name)
		{
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.CreateSimpleColumnMap(type, name);
			}
			return this.CreateStructuralColumnMap(type, name);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00122C1C File Offset: 0x00120E1C
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

		// Token: 0x06003F90 RID: 16272 RVA: 0x00122D84 File Offset: 0x00120F84
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EntityType")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "keyColumnMap")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003F91 RID: 16273 RVA: 0x00123058 File Offset: 0x00121258
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
				this.CreateColumnMap(relProperty.ToEnd.TypeUsage, relProperty.ToString());
			}
			foreach (TypeInfo typeInfo2 in typeInfo.ImmediateSubTypes)
			{
				this.BuildRelPropertyColumnMaps(typeInfo2, false);
			}
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x00123140 File Offset: 0x00121340
		private SimpleColumnMap CreateEntitySetIdColumnMap(EdmProperty prop)
		{
			return this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(prop), "__EntitySetId");
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x00123154 File Offset: 0x00121354
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private SimplePolymorphicColumnMap CreatePolymorphicColumnMap(TypeInfo typeInfo, string name)
		{
			Dictionary<object, TypedColumnMap> dictionary = new Dictionary<object, TypedColumnMap>((typeInfo.RootType.DiscriminatorMap == null) ? null : TrailingSpaceComparer.Instance);
			List<TypedColumnMap> list = new List<TypedColumnMap>();
			TypeInfo rootType = typeInfo.RootType;
			SimpleColumnMap typeDiscriminator = this.CreateTypeIdColumnMap(rootType.TypeIdProperty);
			if (TypeSemantics.IsComplexType(typeInfo.Type))
			{
				this.CreateComplexTypeColumnMap(rootType, name, null, dictionary, list);
			}
			else
			{
				this.CreateEntityColumnMap(rootType, name, null, dictionary, list, true);
			}
			TypedColumnMap typedColumnMap = null;
			foreach (TypedColumnMap typedColumnMap2 in list)
			{
				if (TypeSemantics.IsStructurallyEqual(typedColumnMap2.Type, typeInfo.Type))
				{
					typedColumnMap = typedColumnMap2;
					break;
				}
			}
			PlanCompiler.Assert(null != typedColumnMap, "Didn't find requested type in polymorphic type hierarchy?");
			return new SimplePolymorphicColumnMap(typeInfo.Type, name, typedColumnMap.Properties, typeDiscriminator, dictionary);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x00123244 File Offset: 0x00121444
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RowType")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003F95 RID: 16277 RVA: 0x001232E8 File Offset: 0x001214E8
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

		// Token: 0x06003F96 RID: 16278 RVA: 0x00123390 File Offset: 0x00121590
		private SimpleColumnMap CreateSimpleColumnMap(TypeUsage type, string name)
		{
			Var nextVar = this.GetNextVar();
			return new VarRefColumnMap(type, name, nextVar);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x001233AE File Offset: 0x001215AE
		private SimpleColumnMap CreateTypeIdColumnMap(EdmProperty prop)
		{
			return this.CreateSimpleColumnMap(Helper.GetModelTypeUsage(prop), "__TypeId");
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x001233C4 File Offset: 0x001215C4
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
			throw new NotSupportedException(type.Identity);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x00123448 File Offset: 0x00121648
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "entitySet")]
		private EntityIdentity CreateEntityIdentity(EntityType entityType, SimpleColumnMap entitySetIdColumnMap, SimpleColumnMap[] keyColumnMaps)
		{
			if (entitySetIdColumnMap != null)
			{
				return new DiscriminatedEntityIdentity(entitySetIdColumnMap, this.m_typeInfo.EntitySetIdToEntitySetMap, keyColumnMaps);
			}
			EntitySet entitySet = this.m_typeInfo.GetEntitySet(entityType);
			PlanCompiler.Assert(entitySet != null, "Expected non-null entitySet when no entity set ID is required. Entity type = " + entityType);
			return new SimpleEntityIdentity(entitySet, keyColumnMaps);
		}

		// Token: 0x040017B5 RID: 6069
		private const string c_TypeIdColumnName = "__TypeId";

		// Token: 0x040017B6 RID: 6070
		private const string c_EntitySetIdColumnName = "__EntitySetId";

		// Token: 0x040017B7 RID: 6071
		private const string c_NullSentinelColumnName = "__NullSentinel";

		// Token: 0x040017B8 RID: 6072
		private readonly IEnumerator<Var> m_varList;

		// Token: 0x040017B9 RID: 6073
		private readonly VarInfo m_varInfo;

		// Token: 0x040017BA RID: 6074
		private readonly VarRefColumnMap m_columnMap;

		// Token: 0x040017BB RID: 6075
		private readonly StructuredTypeInfo m_typeInfo;
	}
}
