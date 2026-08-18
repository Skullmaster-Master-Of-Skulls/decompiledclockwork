using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000572 RID: 1394
	internal interface IEntityWrapper
	{
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06003657 RID: 13911
		RelationshipManager RelationshipManager { get; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06003658 RID: 13912
		bool OwnsRelationshipManager { get; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06003659 RID: 13913
		object Entity { get; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600365A RID: 13914
		// (set) Token: 0x0600365B RID: 13915
		EntityEntry ObjectStateEntry { get; set; }

		// Token: 0x0600365C RID: 13916
		void EnsureCollectionNotNull(RelatedEnd relatedEnd);

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600365D RID: 13917
		// (set) Token: 0x0600365E RID: 13918
		EntityKey EntityKey { get; set; }

		// Token: 0x0600365F RID: 13919
		EntityKey GetEntityKeyFromEntity();

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06003660 RID: 13920
		// (set) Token: 0x06003661 RID: 13921
		ObjectContext Context { get; set; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003662 RID: 13922
		MergeOption MergeOption { get; }

		// Token: 0x06003663 RID: 13923
		void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption);

		// Token: 0x06003664 RID: 13924
		void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption);

		// Token: 0x06003665 RID: 13925
		void DetachContext();

		// Token: 0x06003666 RID: 13926
		void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06003667 RID: 13927
		void TakeSnapshot(EntityEntry entry);

		// Token: 0x06003668 RID: 13928
		void TakeSnapshotOfRelationships(EntityEntry entry);

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003669 RID: 13929
		Type IdentityType { get; }

		// Token: 0x0600366A RID: 13930
		void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x0600366B RID: 13931
		bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x0600366C RID: 13932
		object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x0600366D RID: 13933
		void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x0600366E RID: 13934
		void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x0600366F RID: 13935
		void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003670 RID: 13936
		// (set) Token: 0x06003671 RID: 13937
		bool InitializingProxyRelatedEnds { get; set; }

		// Token: 0x06003672 RID: 13938
		void UpdateCurrentValueRecord(object value, EntityEntry entry);

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003673 RID: 13939
		bool RequiresRelationshipChangeTracking { get; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06003674 RID: 13940
		bool OverridesEqualsOrGetHashCode { get; }
	}
}
