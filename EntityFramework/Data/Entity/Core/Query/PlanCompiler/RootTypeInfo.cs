using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200069A RID: 1690
	internal class RootTypeInfo : TypeInfo
	{
		// Token: 0x060042F6 RID: 17142 RVA: 0x0013D4E0 File Offset: 0x0013B6E0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal RootTypeInfo(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap) : base(type, null)
		{
			PlanCompiler.Assert(type.EdmType.BaseType == null, "only root types allowed here");
			this.m_propertyMap = new Dictionary<PropertyRef, EdmProperty>();
			this.m_propertyRefList = new List<PropertyRef>();
			this.m_discriminatorMap = discriminatorMap;
			this.TypeIdKind = TypeIdKind.Generated;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x0013D531 File Offset: 0x0013B731
		// (set) Token: 0x060042F8 RID: 17144 RVA: 0x0013D539 File Offset: 0x0013B739
		internal TypeIdKind TypeIdKind { get; set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x0013D542 File Offset: 0x0013B742
		// (set) Token: 0x060042FA RID: 17146 RVA: 0x0013D54A File Offset: 0x0013B74A
		internal TypeUsage TypeIdType { get; set; }

		// Token: 0x060042FB RID: 17147 RVA: 0x0013D553 File Offset: 0x0013B753
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

		// Token: 0x060042FC RID: 17148 RVA: 0x0013D591 File Offset: 0x0013B791
		internal void AddPropertyRef(PropertyRef propertyRef)
		{
			this.m_propertyRefList.Add(propertyRef);
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x0013D59F File Offset: 0x0013B79F
		// (set) Token: 0x060042FE RID: 17150 RVA: 0x0013D5A7 File Offset: 0x0013B7A7
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

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x0013D5BC File Offset: 0x0013B7BC
		internal new TypeUsage FlattenedTypeUsage
		{
			get
			{
				return this.m_flattenedTypeUsage;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x0013D5C4 File Offset: 0x0013B7C4
		internal ExplicitDiscriminatorMap DiscriminatorMap
		{
			get
			{
				return this.m_discriminatorMap;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x0013D5CC File Offset: 0x0013B7CC
		internal new EdmProperty EntitySetIdProperty
		{
			get
			{
				return this.m_entitySetIdProperty;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x0013D5D4 File Offset: 0x0013B7D4
		internal new EdmProperty NullSentinelProperty
		{
			get
			{
				return this.m_nullSentinelProperty;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x0013D5DC File Offset: 0x0013B7DC
		internal new IEnumerable<PropertyRef> PropertyRefList
		{
			get
			{
				return this.m_propertyRefList;
			}
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x0013D5E4 File Offset: 0x0013B7E4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TypeInfo")]
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
			PlanCompiler.Assert(false, "no complex structure " + property + " found in TypeInfo");
			return 0;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0013D644 File Offset: 0x0013B844
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal new bool TryGetNewProperty(PropertyRef propertyRef, bool throwIfMissing, out EdmProperty property)
		{
			bool flag = this.m_propertyMap.TryGetValue(propertyRef, out property);
			if (throwIfMissing && !flag)
			{
				PlanCompiler.Assert(false, string.Concat(new object[]
				{
					"Unable to find property ",
					propertyRef,
					" in type ",
					base.Type.EdmType.Identity
				}));
			}
			return flag;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x0013D6A0 File Offset: 0x0013B8A0
		internal new EdmProperty TypeIdProperty
		{
			get
			{
				return this.m_typeIdProperty;
			}
		}

		// Token: 0x040018B8 RID: 6328
		private readonly List<PropertyRef> m_propertyRefList;

		// Token: 0x040018B9 RID: 6329
		private readonly Dictionary<PropertyRef, EdmProperty> m_propertyMap;

		// Token: 0x040018BA RID: 6330
		private EdmProperty m_nullSentinelProperty;

		// Token: 0x040018BB RID: 6331
		private EdmProperty m_typeIdProperty;

		// Token: 0x040018BC RID: 6332
		private readonly ExplicitDiscriminatorMap m_discriminatorMap;

		// Token: 0x040018BD RID: 6333
		private EdmProperty m_entitySetIdProperty;

		// Token: 0x040018BE RID: 6334
		private RowType m_flattenedType;

		// Token: 0x040018BF RID: 6335
		private TypeUsage m_flattenedTypeUsage;
	}
}
