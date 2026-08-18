using System;

namespace System.Data.Objects
{
	// Token: 0x02000140 RID: 320
	internal class ObjectStateEntryOriginalDbUpdatableDataRecord_Internal : OriginalValueRecord
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x0004C888 File Offset: 0x0004AA88
		internal ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
			EntityUtil.CheckArgumentNull<EntityEntry>(cacheEntry, "cacheEntry");
			EntityUtil.CheckArgumentNull<object>(userObject, "userObject");
			EntityUtil.CheckArgumentNull<StateManagerTypeMetadata>(metadata, "metadata");
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Deleted)
			{
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0004C8D6 File Offset: 0x0004AAD6
		protected override object GetRecordValue(int ordinal)
		{
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalUpdatableInternal);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0004C8F6 File Offset: 0x0004AAF6
		protected override void SetRecordValue(int ordinal, object value)
		{
			(this._cacheEntry as EntityEntry).SetOriginalEntityValue(this._metadata, ordinal, this._userObject, value);
		}
	}
}
