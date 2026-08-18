using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200017B RID: 379
	internal interface IEntityWrapper
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001B83 RID: 7043
		RelationshipManager RelationshipManager { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001B84 RID: 7044
		bool OwnsRelationshipManager { get; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001B85 RID: 7045
		object Entity { get; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001B86 RID: 7046
		// (set) Token: 0x06001B87 RID: 7047
		EntityEntry ObjectStateEntry { get; set; }

		// Token: 0x06001B88 RID: 7048
		void EnsureCollectionNotNull(RelatedEnd relatedEnd);

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001B89 RID: 7049
		// (set) Token: 0x06001B8A RID: 7050
		EntityKey EntityKey { get; set; }

		// Token: 0x06001B8B RID: 7051
		EntityKey GetEntityKeyFromEntity();

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001B8C RID: 7052
		// (set) Token: 0x06001B8D RID: 7053
		ObjectContext Context { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001B8E RID: 7054
		MergeOption MergeOption { get; }

		// Token: 0x06001B8F RID: 7055
		void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption);

		// Token: 0x06001B90 RID: 7056
		void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption);

		// Token: 0x06001B91 RID: 7057
		void DetachContext();

		// Token: 0x06001B92 RID: 7058
		void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06001B93 RID: 7059
		void TakeSnapshot(EntityEntry entry);

		// Token: 0x06001B94 RID: 7060
		void TakeSnapshotOfRelationships(EntityEntry entry);

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001B95 RID: 7061
		Type IdentityType { get; }

		// Token: 0x06001B96 RID: 7062
		void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06001B97 RID: 7063
		bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x06001B98 RID: 7064
		object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x06001B99 RID: 7065
		void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06001B9A RID: 7066
		void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06001B9B RID: 7067
		void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001B9C RID: 7068
		// (set) Token: 0x06001B9D RID: 7069
		bool InitializingProxyRelatedEnds { get; set; }

		// Token: 0x06001B9E RID: 7070
		void UpdateCurrentValueRecord(object value, EntityEntry entry);

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001B9F RID: 7071
		bool RequiresRelationshipChangeTracking { get; }
	}
}
