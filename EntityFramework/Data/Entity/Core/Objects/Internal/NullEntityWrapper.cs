using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200058E RID: 1422
	internal class NullEntityWrapper : IEntityWrapper
	{
		// Token: 0x0600378A RID: 14218 RVA: 0x00107B9E File Offset: 0x00105D9E
		private NullEntityWrapper()
		{
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x00107BA6 File Offset: 0x00105DA6
		internal static IEntityWrapper NullWrapper
		{
			get
			{
				return NullEntityWrapper._nullWrapper;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x00107BAD File Offset: 0x00105DAD
		public RelationshipManager RelationshipManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x00107BB0 File Offset: 0x00105DB0
		public bool OwnsRelationshipManager
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x00107BB3 File Offset: 0x00105DB3
		public object Entity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x00107BB6 File Offset: 0x00105DB6
		// (set) Token: 0x06003790 RID: 14224 RVA: 0x00107BB9 File Offset: 0x00105DB9
		public EntityEntry ObjectStateEntry
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x00107BBB File Offset: 0x00105DBB
		public void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x00107BBD File Offset: 0x00105DBD
		public bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			return false;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x00107BC0 File Offset: 0x00105DC0
		// (set) Token: 0x06003794 RID: 14228 RVA: 0x00107BC3 File Offset: 0x00105DC3
		public EntityKey EntityKey
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x00107BC5 File Offset: 0x00105DC5
		public EntityKey GetEntityKeyFromEntity()
		{
			return null;
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x00107BC8 File Offset: 0x00105DC8
		// (set) Token: 0x06003797 RID: 14231 RVA: 0x00107BCB File Offset: 0x00105DCB
		public ObjectContext Context
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x00107BCD File Offset: 0x00105DCD
		public MergeOption MergeOption
		{
			get
			{
				return MergeOption.NoTracking;
			}
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x00107BD0 File Offset: 0x00105DD0
		public void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x00107BD2 File Offset: 0x00105DD2
		public void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x00107BD4 File Offset: 0x00105DD4
		public void DetachContext()
		{
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x00107BD6 File Offset: 0x00105DD6
		public void SetChangeTracker(IEntityChangeTracker changeTracker)
		{
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x00107BD8 File Offset: 0x00105DD8
		public void TakeSnapshot(EntityEntry entry)
		{
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x00107BDA File Offset: 0x00105DDA
		public void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x00107BDC File Offset: 0x00105DDC
		public Type IdentityType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x00107BDF File Offset: 0x00105DDF
		public void EnsureCollectionNotNull(RelatedEnd relatedEnd)
		{
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x00107BE1 File Offset: 0x00105DE1
		public object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			return null;
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x00107BE4 File Offset: 0x00105DE4
		public void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x00107BE6 File Offset: 0x00105DE6
		public void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x00107BE8 File Offset: 0x00105DE8
		public void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value)
		{
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x00107BEA File Offset: 0x00105DEA
		// (set) Token: 0x060037A6 RID: 14246 RVA: 0x00107BED File Offset: 0x00105DED
		public bool InitializingProxyRelatedEnds
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x00107BEF File Offset: 0x00105DEF
		public void UpdateCurrentValueRecord(object value, EntityEntry entry)
		{
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x00107BF1 File Offset: 0x00105DF1
		public bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x00107BF4 File Offset: 0x00105DF4
		public bool OverridesEqualsOrGetHashCode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400155E RID: 5470
		private static readonly IEntityWrapper _nullWrapper = new NullEntityWrapper();
	}
}
