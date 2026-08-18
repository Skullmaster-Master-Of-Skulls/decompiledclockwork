using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200007E RID: 126
	internal class RootTypeInfo : TypeInfo
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00033298 File Offset: 0x00031498
		internal RootTypeInfo(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap) : base(type, null)
		{
			PlanCompiler.Assert(type.EdmType.BaseType == null, "only root types allowed here");
			this.m_propertyMap = new Dictionary<PropertyRef, EdmProperty>();
			this.m_propertyRefList = new List<PropertyRef>();
			this.m_discriminatorMap = discriminatorMap;
			this.m_typeIdKind = TypeIdKind.Generated;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000332E9 File Offset: 0x000314E9
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x000332F1 File Offset: 0x000314F1
		internal TypeIdKind TypeIdKind
		{
			get
			{
				return this.m_typeIdKind;
			}
			set
			{
				this.m_typeIdKind = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x000332FA File Offset: 0x000314FA
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00033302 File Offset: 0x00031502
		internal TypeUsage TypeIdType
		{
			get
			{
				return this.m_typeIdType;
			}
			set
			{
				this.m_typeIdType = value;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0003330B File Offset: 0x0003150B
		internal void AddPropertyMapping(PropertyRef propertyRef, EdmProperty newProperty)
		{
			this.m_propertyMap[propertyRef] = newProperty;
			if (propertyRef is TypeIdPropertyRef)
			{
				this.m_typeIdProperty = newProperty;
				return;
			}
			if (propertyRef is EntitySetIdPropertyRef)
			{
				this.m_entitySetIdProperty = newProperty;
				return;
			}
			if (propertyRef is NullSentinelPropertyRef)
			{
				this.m_nullSentinelProperty = newProperty;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00033349 File Offset: 0x00031549
		internal void AddPropertyRef(PropertyRef propertyRef)
		{
			this.m_propertyRefList.Add(propertyRef);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00033357 File Offset: 0x00031557
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0003335F File Offset: 0x0003155F
		internal new RowType FlattenedType
		{
			get
			{
				return this.m_flattenedType;
			}
			set
			{
				this.m_flattenedType = value;
				this.m_flattenedTypeUsage = TypeUsage.Create(value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00033374 File Offset: 0x00031574
		internal new TypeUsage FlattenedTypeUsage
		{
			get
			{
				return this.m_flattenedTypeUsage;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0003337C File Offset: 0x0003157C
		internal ExplicitDiscriminatorMap DiscriminatorMap
		{
			get
			{
				return this.m_discriminatorMap;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00033384 File Offset: 0x00031584
		internal new EdmProperty EntitySetIdProperty
		{
			get
			{
				return this.m_entitySetIdProperty;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0003338C File Offset: 0x0003158C
		internal new EdmProperty NullSentinelProperty
		{
			get
			{
				return this.m_nullSentinelProperty;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00033394 File Offset: 0x00031594
		internal new IEnumerable<PropertyRef> PropertyRefList
		{
			get
			{
				return this.m_propertyRefList;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003339C File Offset: 0x0003159C
		internal int GetNestedStructureOffset(PropertyRef property)
		{
			for (int i = 0; i < this.m_propertyRefList.Count; i++)
			{
				NestedPropertyRef nestedPropertyRef = this.m_propertyRefList[i] as NestedPropertyRef;
				if (nestedPropertyRef != null && nestedPropertyRef.InnerProperty.Equals(property))
				{
					return i;
				}
			}
			PlanCompiler.Assert(false, "no complex structure " + ((property != null) ? property.ToString() : null) + " found in TypeInfo");
			return 0;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00033408 File Offset: 0x00031608
		internal new bool TryGetNewProperty(PropertyRef propertyRef, bool throwIfMissing, out EdmProperty property)
		{
			bool flag = this.m_propertyMap.TryGetValue(propertyRef, out property);
			if (throwIfMissing && !flag)
			{
				PlanCompiler.Assert(false, "Unable to find property " + propertyRef.ToString() + " in type " + base.Type.EdmType.Identity);
			}
			return flag;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00033455 File Offset: 0x00031655
		internal new EdmProperty TypeIdProperty
		{
			get
			{
				return this.m_typeIdProperty;
			}
		}

		// Token: 0x04000875 RID: 2165
		private readonly List<PropertyRef> m_propertyRefList;

		// Token: 0x04000876 RID: 2166
		private readonly Dictionary<PropertyRef, EdmProperty> m_propertyMap;

		// Token: 0x04000877 RID: 2167
		private EdmProperty m_nullSentinelProperty;

		// Token: 0x04000878 RID: 2168
		private EdmProperty m_typeIdProperty;

		// Token: 0x04000879 RID: 2169
		private TypeIdKind m_typeIdKind;

		// Token: 0x0400087A RID: 2170
		private TypeUsage m_typeIdType;

		// Token: 0x0400087B RID: 2171
		private readonly ExplicitDiscriminatorMap m_discriminatorMap;

		// Token: 0x0400087C RID: 2172
		private EdmProperty m_entitySetIdProperty;

		// Token: 0x0400087D RID: 2173
		private RowType m_flattenedType;

		// Token: 0x0400087E RID: 2174
		private TypeUsage m_flattenedTypeUsage;
	}
}
