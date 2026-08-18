using System;

namespace System.Data.Objects
{
	// Token: 0x02000141 RID: 321
	internal sealed class ObjectStateEntryOriginalDbUpdatableDataRecord_Public : ObjectStateEntryOriginalDbUpdatableDataRecord_Internal
	{
		// Token: 0x0600170F RID: 5903 RVA: 0x0004C916 File Offset: 0x0004AB16
		internal ObjectStateEntryOriginalDbUpdatableDataRecord_Public(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject, int parentEntityPropertyIndex) : base(cacheEntry, metadata, userObject)
		{
			this._parentEntityPropertyIndex = parentEntityPropertyIndex;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0004C929 File Offset: 0x0004AB29
		protected override object GetRecordValue(int ordinal)
		{
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalUpdatablePublic, this.GetPropertyIndex(ordinal));
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0004C950 File Offset: 0x0004AB50
		protected override void SetRecordValue(int ordinal, object value)
		{
			StateManagerMemberMetadata stateManagerMemberMetadata = this._metadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsComplex)
			{
				throw EntityUtil.SetOriginalComplexProperties(stateManagerMemberMetadata.CLayerName);
			}
			object obj = value ?? DBNull.Value;
			EntityEntry entityEntry = this._cacheEntry as EntityEntry;
			EntityState state = entityEntry.State;
			if (entityEntry.HasRecordValueChanged(this, ordinal, obj))
			{
				if (stateManagerMemberMetadata.IsPartOfKey)
				{
					throw EntityUtil.SetOriginalPrimaryKey(stateManagerMemberMetadata.CLayerName);
				}
				Type clrType = stateManagerMemberMetadata.ClrType;
				if (DBNull.Value == obj && clrType.IsValueType && !stateManagerMemberMetadata.CdmMetadata.Nullable)
				{
					throw EntityUtil.NullOriginalValueForNonNullableProperty(stateManagerMemberMetadata.CLayerName, stateManagerMemberMetadata.ClrMetadata.Name, stateManagerMemberMetadata.ClrMetadata.DeclaringType.FullName);
				}
				base.SetRecordValue(ordinal, value);
				if (state == EntityState.Unchanged && entityEntry.State == EntityState.Modified)
				{
					entityEntry.ObjectStateManager.ChangeState(entityEntry, state, EntityState.Modified);
				}
				entityEntry.SetModifiedPropertyInternal(this.GetPropertyIndex(ordinal));
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0004CA3D File Offset: 0x0004AC3D
		private int GetPropertyIndex(int ordinal)
		{
			if (this._parentEntityPropertyIndex != -1)
			{
				return this._parentEntityPropertyIndex;
			}
			return ordinal;
		}

		// Token: 0x04000A71 RID: 2673
		private int _parentEntityPropertyIndex;
	}
}
