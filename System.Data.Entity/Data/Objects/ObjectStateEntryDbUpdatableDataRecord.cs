using System;

namespace System.Data.Objects
{
	// Token: 0x0200013F RID: 319
	internal sealed class ObjectStateEntryDbUpdatableDataRecord : CurrentValueRecord
	{
		// Token: 0x06001708 RID: 5896 RVA: 0x0004C790 File Offset: 0x0004A990
		internal ObjectStateEntryDbUpdatableDataRecord(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
			EntityUtil.CheckArgumentNull<EntityEntry>(cacheEntry, "cacheEntry");
			EntityUtil.CheckArgumentNull<object>(userObject, "userObject");
			EntityUtil.CheckArgumentNull<StateManagerTypeMetadata>(metadata, "metadata");
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Added)
			{
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0004C7E0 File Offset: 0x0004A9E0
		internal ObjectStateEntryDbUpdatableDataRecord(RelationshipEntry cacheEntry) : base(cacheEntry)
		{
			EntityUtil.CheckArgumentNull<RelationshipEntry>(cacheEntry, "cacheEntry");
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Added)
			{
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0004C814 File Offset: 0x0004AA14
		protected override object GetRecordValue(int ordinal)
		{
			if (this._cacheEntry.IsRelationship)
			{
				return (this._cacheEntry as RelationshipEntry).GetCurrentRelationValue(ordinal);
			}
			return (this._cacheEntry as EntityEntry).GetCurrentEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.CurrentUpdatable);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004C853 File Offset: 0x0004AA53
		protected override void SetRecordValue(int ordinal, object value)
		{
			if (this._cacheEntry.IsRelationship)
			{
				throw EntityUtil.CantModifyRelationValues();
			}
			(this._cacheEntry as EntityEntry).SetCurrentEntityValue(this._metadata, ordinal, this._userObject, value);
		}
	}
}
