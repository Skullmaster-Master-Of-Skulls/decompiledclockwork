using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005AC RID: 1452
	internal class ObjectStateEntryOriginalDbUpdatableDataRecord_Internal : OriginalValueRecord
	{
		// Token: 0x060039A9 RID: 14761 RVA: 0x00111750 File Offset: 0x0010F950
		internal ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
			EntityState state = cacheEntry.State;
			if (state == EntityState.Unchanged || state != EntityState.Deleted)
			{
			}
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x0011177A File Offset: 0x0010F97A
		protected override object GetRecordValue(int ordinal)
		{
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalUpdatableInternal);
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x0011179A File Offset: 0x0010F99A
		protected override void SetRecordValue(int ordinal, object value)
		{
			(this._cacheEntry as EntityEntry).SetOriginalEntityValue(this._metadata, ordinal, this._userObject, value);
		}
	}
}
