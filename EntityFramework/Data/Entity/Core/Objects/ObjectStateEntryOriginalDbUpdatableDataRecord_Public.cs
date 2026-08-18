using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005AD RID: 1453
	internal sealed class ObjectStateEntryOriginalDbUpdatableDataRecord_Public : ObjectStateEntryOriginalDbUpdatableDataRecord_Internal
	{
		// Token: 0x060039AC RID: 14764 RVA: 0x001117BA File Offset: 0x0010F9BA
		internal ObjectStateEntryOriginalDbUpdatableDataRecord_Public(EntityEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject, int parentEntityPropertyIndex) : base(cacheEntry, metadata, userObject)
		{
			this._parentEntityPropertyIndex = parentEntityPropertyIndex;
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x001117CD File Offset: 0x0010F9CD
		protected override object GetRecordValue(int ordinal)
		{
			return (this._cacheEntry as EntityEntry).GetOriginalEntityValue(this._metadata, ordinal, this._userObject, ObjectStateValueRecord.OriginalUpdatablePublic, this.GetPropertyIndex(ordinal));
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x001117F4 File Offset: 0x0010F9F4
		protected override void SetRecordValue(int ordinal, object value)
		{
			StateManagerMemberMetadata stateManagerMemberMetadata = this._metadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsComplex)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_SetOriginalComplexProperties(stateManagerMemberMetadata.CLayerName));
			}
			object obj = value ?? DBNull.Value;
			EntityEntry entityEntry = this._cacheEntry as EntityEntry;
			EntityState state = entityEntry.State;
			if (entityEntry.HasRecordValueChanged(this, ordinal, obj))
			{
				if (stateManagerMemberMetadata.IsPartOfKey)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_SetOriginalPrimaryKey(stateManagerMemberMetadata.CLayerName));
				}
				Type clrType = stateManagerMemberMetadata.ClrType;
				if (DBNull.Value == obj && clrType.IsValueType() && !stateManagerMemberMetadata.CdmMetadata.Nullable)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_NullOriginalValueForNonNullableProperty(stateManagerMemberMetadata.CLayerName, stateManagerMemberMetadata.ClrMetadata.Name, stateManagerMemberMetadata.ClrMetadata.DeclaringType.FullName));
				}
				base.SetRecordValue(ordinal, value);
				if (state == EntityState.Unchanged && entityEntry.State == EntityState.Modified)
				{
					entityEntry.ObjectStateManager.ChangeState(entityEntry, state, EntityState.Modified);
				}
				entityEntry.SetModifiedPropertyInternal(this.GetPropertyIndex(ordinal));
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x001118F0 File Offset: 0x0010FAF0
		private int GetPropertyIndex(int ordinal)
		{
			if (this._parentEntityPropertyIndex != -1)
			{
				return this._parentEntityPropertyIndex;
			}
			return ordinal;
		}

		// Token: 0x040015F3 RID: 5619
		private readonly int _parentEntityPropertyIndex;
	}
}
