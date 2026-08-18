using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005AF RID: 1455
	internal sealed class ObjectStateEntryDbUpdatableDataRecord : CurrentValueRecord
	{
		// Token: 0x060039CE RID: 14798 RVA: 0x00111D48 File Offset: 0x0010FF48
		internal ObjectStateEntryDbUpdatableDataRecord(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject) : base(cacheEntry, metadata, userObject)
		{
			switch (cacheEntry.State)
			{
			default:
				return;
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x00111D80 File Offset: 0x0010FF80
		internal ObjectStateEntryDbUpdatableDataRecord(RelationshipEntry cacheEntry) : base(cacheEntry)
		{
			switch (cacheEntry.State)
			{
			default:
				return;
			}
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x00111DB4 File Offset: 0x0010FFB4
		protected override object GetRecordValue(int ordinal)
		{
			if (this._cacheEntry.IsRelationship)
			{
				return (this._cacheEntry as RelationshipEntry).GetCurrentRelationValue(ordinal);
			}
			return (this._cacheEntry as EntityEntry).GetCurrentEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.CurrentUpdatable);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x00111DF3 File Offset: 0x0010FFF3
		protected override void SetRecordValue(int ordinal, object value)
		{
			if (this._cacheEntry.IsRelationship)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
			}
			(this._cacheEntry as EntityEntry).SetCurrentEntityValue(this._metadata, ordinal, this._userObject, value);
		}
	}
}
