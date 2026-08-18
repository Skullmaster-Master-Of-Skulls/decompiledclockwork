using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000699 RID: 1689
	internal class TypeInfo
	{
		// Token: 0x060042DC RID: 17116 RVA: 0x0013CBB4 File Offset: 0x0013ADB4
		internal static TypeInfo Create(TypeUsage type, TypeInfo superTypeInfo, ExplicitDiscriminatorMap discriminatorMap)
		{
			TypeInfo result;
			if (superTypeInfo == null)
			{
				result = new RootTypeInfo(type, discriminatorMap);
			}
			else
			{
				result = new TypeInfo(type, superTypeInfo);
			}
			return result;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0013CBD7 File Offset: 0x0013ADD7
		protected TypeInfo(TypeUsage type, TypeInfo superType)
		{
			this.m_type = type;
			this.m_immediateSubTypes = new List<TypeInfo>();
			this.m_superType = superType;
			if (superType != null)
			{
				superType.m_immediateSubTypes.Add(this);
				this.m_rootType = superType.RootType;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x0013CC13 File Offset: 0x0013AE13
		internal bool IsRootType
		{
			get
			{
				return this.m_rootType == null;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0013CC1E File Offset: 0x0013AE1E
		internal List<TypeInfo> ImmediateSubTypes
		{
			get
			{
				return this.m_immediateSubTypes;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x0013CC26 File Offset: 0x0013AE26
		internal TypeInfo SuperType
		{
			get
			{
				return this.m_superType;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x0013CC2E File Offset: 0x0013AE2E
		internal RootTypeInfo RootType
		{
			get
			{
				return this.m_rootType ?? ((RootTypeInfo)this);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x0013CC40 File Offset: 0x0013AE40
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x0013CC48 File Offset: 0x0013AE48
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x0013CC50 File Offset: 0x0013AE50
		internal object TypeId { get; set; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x0013CC59 File Offset: 0x0013AE59
		internal virtual RowType FlattenedType
		{
			get
			{
				return this.RootType.FlattenedType;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x0013CC66 File Offset: 0x0013AE66
		internal virtual TypeUsage FlattenedTypeUsage
		{
			get
			{
				return this.RootType.FlattenedTypeUsage;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x0013CC73 File Offset: 0x0013AE73
		internal virtual EdmProperty EntitySetIdProperty
		{
			get
			{
				return this.RootType.EntitySetIdProperty;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x0013CC80 File Offset: 0x0013AE80
		internal bool HasEntitySetIdProperty
		{
			get
			{
				return this.RootType.EntitySetIdProperty != null;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x0013CC93 File Offset: 0x0013AE93
		internal virtual EdmProperty NullSentinelProperty
		{
			get
			{
				return this.RootType.NullSentinelProperty;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x0013CCA0 File Offset: 0x0013AEA0
		internal bool HasNullSentinelProperty
		{
			get
			{
				return this.RootType.NullSentinelProperty != null;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x0013CCB3 File Offset: 0x0013AEB3
		internal virtual EdmProperty TypeIdProperty
		{
			get
			{
				return this.RootType.TypeIdProperty;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x0013CCC0 File Offset: 0x0013AEC0
		internal bool HasTypeIdProperty
		{
			get
			{
				return this.RootType.TypeIdProperty != null;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x0013CCD3 File Offset: 0x0013AED3
		internal virtual IEnumerable<PropertyRef> PropertyRefList
		{
			get
			{
				return this.RootType.PropertyRefList;
			}
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0013CCE0 File Offset: 0x0013AEE0
		internal EdmProperty GetNewProperty(PropertyRef propertyRef)
		{
			EdmProperty result;
			this.TryGetNewProperty(propertyRef, true, out result);
			return result;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0013CCF9 File Offset: 0x0013AEF9
		internal bool TryGetNewProperty(PropertyRef propertyRef, bool throwIfMissing, out EdmProperty newProperty)
		{
			return this.RootType.TryGetNewProperty(propertyRef, throwIfMissing, out newProperty);
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0013CF18 File Offset: 0x0013B118
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Non-EdmProperty")]
		internal IEnumerable<PropertyRef> GetKeyPropertyRefs()
		{
			EntityTypeBase entityType = null;
			RefType refType = null;
			if (TypeHelpers.TryGetEdmType<RefType>(this.m_type, out refType))
			{
				entityType = refType.ElementType;
			}
			else
			{
				entityType = TypeHelpers.GetEdmType<EntityTypeBase>(this.m_type);
			}
			foreach (EdmMember p in entityType.KeyMembers)
			{
				PlanCompiler.Assert(p is EdmProperty, "Non-EdmProperty key members are not supported");
				SimplePropertyRef spr = new SimplePropertyRef(p);
				yield return spr;
			}
			yield break;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0013D0F4 File Offset: 0x0013B2F4
		internal IEnumerable<PropertyRef> GetIdentityPropertyRefs()
		{
			if (this.HasEntitySetIdProperty)
			{
				yield return EntitySetIdPropertyRef.Instance;
			}
			foreach (PropertyRef p in this.GetKeyPropertyRefs())
			{
				yield return p;
			}
			yield break;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0013D29C File Offset: 0x0013B49C
		internal IEnumerable<PropertyRef> GetAllPropertyRefs()
		{
			foreach (PropertyRef p in this.PropertyRefList)
			{
				yield return p;
			}
			yield break;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x0013D448 File Offset: 0x0013B648
		internal IEnumerable<EdmProperty> GetAllProperties()
		{
			foreach (EdmProperty i in this.FlattenedType.Properties)
			{
				yield return i;
			}
			yield break;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0013D468 File Offset: 0x0013B668
		internal List<TypeInfo> GetTypeHierarchy()
		{
			List<TypeInfo> result = new List<TypeInfo>();
			this.GetTypeHierarchy(result);
			return result;
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x0013D484 File Offset: 0x0013B684
		private void GetTypeHierarchy(List<TypeInfo> result)
		{
			result.Add(this);
			foreach (TypeInfo typeInfo in this.ImmediateSubTypes)
			{
				typeInfo.GetTypeHierarchy(result);
			}
		}

		// Token: 0x040018B3 RID: 6323
		private readonly TypeUsage m_type;

		// Token: 0x040018B4 RID: 6324
		private readonly List<TypeInfo> m_immediateSubTypes;

		// Token: 0x040018B5 RID: 6325
		private readonly TypeInfo m_superType;

		// Token: 0x040018B6 RID: 6326
		private readonly RootTypeInfo m_rootType;
	}
}
