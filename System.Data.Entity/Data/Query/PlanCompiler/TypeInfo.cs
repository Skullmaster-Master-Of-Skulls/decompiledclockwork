using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200007D RID: 125
	internal class TypeInfo
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00033090 File Offset: 0x00031290
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

		// Token: 0x0600092A RID: 2346 RVA: 0x000330B3 File Offset: 0x000312B3
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000330EF File Offset: 0x000312EF
		internal bool IsRootType
		{
			get
			{
				return this.m_rootType == null;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000330FA File Offset: 0x000312FA
		internal List<TypeInfo> ImmediateSubTypes
		{
			get
			{
				return this.m_immediateSubTypes;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00033102 File Offset: 0x00031302
		internal TypeInfo SuperType
		{
			get
			{
				return this.m_superType;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0003310A File Offset: 0x0003130A
		internal RootTypeInfo RootType
		{
			get
			{
				return this.m_rootType ?? ((RootTypeInfo)this);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0003311C File Offset: 0x0003131C
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00033124 File Offset: 0x00031324
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0003312C File Offset: 0x0003132C
		internal object TypeId
		{
			get
			{
				return this.m_typeId;
			}
			set
			{
				this.m_typeId = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00033135 File Offset: 0x00031335
		internal virtual RowType FlattenedType
		{
			get
			{
				return this.RootType.FlattenedType;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00033142 File Offset: 0x00031342
		internal virtual TypeUsage FlattenedTypeUsage
		{
			get
			{
				return this.RootType.FlattenedTypeUsage;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0003314F File Offset: 0x0003134F
		internal virtual EdmProperty EntitySetIdProperty
		{
			get
			{
				return this.RootType.EntitySetIdProperty;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0003315C File Offset: 0x0003135C
		internal bool HasEntitySetIdProperty
		{
			get
			{
				return this.RootType.EntitySetIdProperty != null;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0003316C File Offset: 0x0003136C
		internal virtual EdmProperty NullSentinelProperty
		{
			get
			{
				return this.RootType.NullSentinelProperty;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00033179 File Offset: 0x00031379
		internal bool HasNullSentinelProperty
		{
			get
			{
				return this.RootType.NullSentinelProperty != null;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00033189 File Offset: 0x00031389
		internal virtual EdmProperty TypeIdProperty
		{
			get
			{
				return this.RootType.TypeIdProperty;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00033196 File Offset: 0x00031396
		internal bool HasTypeIdProperty
		{
			get
			{
				return this.RootType.TypeIdProperty != null;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000331A6 File Offset: 0x000313A6
		internal virtual IEnumerable<PropertyRef> PropertyRefList
		{
			get
			{
				return this.RootType.PropertyRefList;
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000331B4 File Offset: 0x000313B4
		internal EdmProperty GetNewProperty(PropertyRef propertyRef)
		{
			EdmProperty result;
			bool flag = this.TryGetNewProperty(propertyRef, true, out result);
			return result;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000331CD File Offset: 0x000313CD
		internal bool TryGetNewProperty(PropertyRef propertyRef, bool throwIfMissing, out EdmProperty newProperty)
		{
			return this.RootType.TryGetNewProperty(propertyRef, throwIfMissing, out newProperty);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000331DD File Offset: 0x000313DD
		internal IEnumerable<PropertyRef> GetKeyPropertyRefs()
		{
			RefType refType = null;
			EntityTypeBase entityTypeBase;
			if (TypeHelpers.TryGetEdmType<RefType>(this.m_type, out refType))
			{
				entityTypeBase = refType.ElementType;
			}
			else
			{
				entityTypeBase = TypeHelpers.GetEdmType<EntityTypeBase>(this.m_type);
			}
			foreach (EdmMember edmMember in entityTypeBase.KeyMembers)
			{
				PlanCompiler.Assert(edmMember is EdmProperty, "Non-EdmProperty key members are not supported");
				SimplePropertyRef simplePropertyRef = new SimplePropertyRef(edmMember);
				yield return simplePropertyRef;
			}
			ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmMember>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000331ED File Offset: 0x000313ED
		internal IEnumerable<PropertyRef> GetIdentityPropertyRefs()
		{
			if (this.HasEntitySetIdProperty)
			{
				yield return EntitySetIdPropertyRef.Instance;
			}
			foreach (PropertyRef propertyRef in this.GetKeyPropertyRefs())
			{
				yield return propertyRef;
			}
			IEnumerator<PropertyRef> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000331FD File Offset: 0x000313FD
		internal IEnumerable<PropertyRef> GetAllPropertyRefs()
		{
			foreach (PropertyRef propertyRef in this.PropertyRefList)
			{
				yield return propertyRef;
			}
			IEnumerator<PropertyRef> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0003320D File Offset: 0x0003140D
		internal IEnumerable<EdmProperty> GetAllProperties()
		{
			foreach (EdmProperty edmProperty in this.FlattenedType.Properties)
			{
				yield return edmProperty;
			}
			ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmProperty>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00033220 File Offset: 0x00031420
		internal List<TypeInfo> GetTypeHierarchy()
		{
			List<TypeInfo> result = new List<TypeInfo>();
			this.GetTypeHierarchy(result);
			return result;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0003323C File Offset: 0x0003143C
		private void GetTypeHierarchy(List<TypeInfo> result)
		{
			result.Add(this);
			foreach (TypeInfo typeInfo in this.ImmediateSubTypes)
			{
				typeInfo.GetTypeHierarchy(result);
			}
		}

		// Token: 0x04000870 RID: 2160
		private readonly TypeUsage m_type;

		// Token: 0x04000871 RID: 2161
		private object m_typeId;

		// Token: 0x04000872 RID: 2162
		private List<TypeInfo> m_immediateSubTypes;

		// Token: 0x04000873 RID: 2163
		private readonly TypeInfo m_superType;

		// Token: 0x04000874 RID: 2164
		private readonly RootTypeInfo m_rootType;
	}
}
