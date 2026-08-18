using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects.Internal;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Objects
{
	// Token: 0x02000152 RID: 338
	internal sealed class EntityEntry : ObjectStateEntry
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x0005454A File Offset: 0x0005274A
		internal EntityEntry(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySet, ObjectStateManager cache, StateManagerTypeMetadata typeMetadata, EntityState state) : base(cache, entitySet, state)
		{
			this._wrappedEntity = wrappedEntity;
			this._cacheTypeMetadata = typeMetadata;
			this._entityKey = entityKey;
			wrappedEntity.ObjectStateEntry = this;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0005457C File Offset: 0x0005277C
		private void SetChangeTrackingFlags()
		{
			this._requiresScalarChangeTracking = (this.Entity != null && !(this.Entity is IEntityWithChangeTracker));
			bool requiresComplexChangeTracking;
			if (this.Entity != null)
			{
				if (!this._requiresScalarChangeTracking)
				{
					if (this.WrappedEntity.IdentityType != this.Entity.GetType())
					{
						requiresComplexChangeTracking = this._cacheTypeMetadata.Members.Any((StateManagerMemberMetadata m) => m.IsComplex);
					}
					else
					{
						requiresComplexChangeTracking = false;
					}
				}
				else
				{
					requiresComplexChangeTracking = true;
				}
			}
			else
			{
				requiresComplexChangeTracking = false;
			}
			this._requiresComplexChangeTracking = requiresComplexChangeTracking;
			this._requiresAnyChangeTracking = (this.Entity != null && (!(this.Entity is IEntityWithRelationships) || this._requiresComplexChangeTracking || this._requiresScalarChangeTracking));
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00054645 File Offset: 0x00052845
		internal EntityEntry(EntityKey entityKey, EntitySet entitySet, ObjectStateManager cache, StateManagerTypeMetadata typeMetadata) : base(cache, entitySet, EntityState.Unchanged)
		{
			this._wrappedEntity = EntityWrapperFactory.NullWrapper;
			this._entityKey = entityKey;
			this._cacheTypeMetadata = typeMetadata;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00054670 File Offset: 0x00052870
		public override bool IsRelationship
		{
			get
			{
				base.ValidateState();
				return false;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x00054679 File Offset: 0x00052879
		public override object Entity
		{
			get
			{
				base.ValidateState();
				return this._wrappedEntity.Entity;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005468C File Offset: 0x0005288C
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0005469A File Offset: 0x0005289A
		public override EntityKey EntityKey
		{
			get
			{
				base.ValidateState();
				return this._entityKey;
			}
			internal set
			{
				this._entityKey = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000546A4 File Offset: 0x000528A4
		internal IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyDependents
		{
			get
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in ((EntitySet)base.EntitySet).ForeignKeyDependents)
				{
					AssociationSet item = tuple.Item1;
					ReferentialConstraint item2 = tuple.Item2;
					EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.ToRole);
					if (entityTypeForEnd.IsAssignableFrom(this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType))
					{
						yield return tuple;
					}
				}
				IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x000546C4 File Offset: 0x000528C4
		internal IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyPrincipals
		{
			get
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in ((EntitySet)base.EntitySet).ForeignKeyPrincipals)
				{
					AssociationSet item = tuple.Item1;
					ReferentialConstraint item2 = tuple.Item2;
					EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.FromRole);
					if (entityTypeForEnd.IsAssignableFrom(this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType))
					{
						yield return tuple;
					}
				}
				IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000546E1 File Offset: 0x000528E1
		public override IEnumerable<string> GetModifiedProperties()
		{
			base.ValidateState();
			if (EntityState.Modified == base.State && this._modifiedFields != null)
			{
				int num;
				for (int i = 0; i < this._modifiedFields.Count; i = num + 1)
				{
					if (this._modifiedFields[i])
					{
						yield return this.GetCLayerName(i, this._cacheTypeMetadata);
					}
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000546F4 File Offset: 0x000528F4
		public override void SetModifiedProperty(string propertyName)
		{
			int modifiedPropertyInternal = this.ValidateAndGetOrdinalForProperty(propertyName, "SetModifiedProperty");
			if (EntityState.Unchanged == base.State)
			{
				base.State = EntityState.Modified;
				this._cache.ChangeState(this, EntityState.Unchanged, base.State);
			}
			this.SetModifiedPropertyInternal(modifiedPropertyInternal);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00054739 File Offset: 0x00052939
		internal void SetModifiedPropertyInternal(int ordinal)
		{
			if (this._modifiedFields == null)
			{
				this._modifiedFields = new BitArray(this.GetFieldCount(this._cacheTypeMetadata));
			}
			this._modifiedFields[ordinal] = true;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00054768 File Offset: 0x00052968
		private int ValidateAndGetOrdinalForProperty(string propertyName, string methodName)
		{
			EntityUtil.CheckArgumentNull<string>(propertyName, "propertyName");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotModifyKeyEntryState();
			}
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(propertyName);
			if (ordinalforOLayerMemberName == -1)
			{
				throw EntityUtil.InvalidModifiedPropertyName(propertyName);
			}
			if (base.State == EntityState.Added || base.State == EntityState.Deleted)
			{
				throw EntityUtil.SetModifiedStates(methodName);
			}
			return ordinalforOLayerMemberName;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000547C8 File Offset: 0x000529C8
		public override void RejectPropertyChanges(string propertyName)
		{
			int num = this.ValidateAndGetOrdinalForProperty(propertyName, "RejectPropertyChanges");
			if (base.State == EntityState.Unchanged)
			{
				return;
			}
			if (this._modifiedFields != null && this._modifiedFields[num])
			{
				this.DetectChangesInComplexProperties();
				object originalEntityValue = this.GetOriginalEntityValue(this._cacheTypeMetadata, num, this._wrappedEntity.Entity, ObjectStateValueRecord.OriginalReadonly);
				this.SetCurrentEntityValue(this._cacheTypeMetadata, num, this._wrappedEntity.Entity, originalEntityValue);
				this._modifiedFields[num] = false;
				for (int i = 0; i < this._modifiedFields.Count; i++)
				{
					if (this._modifiedFields[i])
					{
						return;
					}
				}
				this.ChangeObjectState(EntityState.Unchanged);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00054877 File Offset: 0x00052A77
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override DbDataRecord OriginalValues
		{
			get
			{
				return this.InternalGetOriginalValues(true);
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00054880 File Offset: 0x00052A80
		public override OriginalValueRecord GetUpdatableOriginalValues()
		{
			return (OriginalValueRecord)this.InternalGetOriginalValues(false);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00054890 File Offset: 0x00052A90
		private DbDataRecord InternalGetOriginalValues(bool readOnly)
		{
			base.ValidateState();
			if (base.State == EntityState.Added)
			{
				throw EntityUtil.OriginalValuesDoesNotExist();
			}
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			this.DetectChangesInComplexProperties();
			if (readOnly)
			{
				return new ObjectStateEntryDbDataRecord(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
			return new ObjectStateEntryOriginalDbUpdatableDataRecord_Public(this, this._cacheTypeMetadata, this._wrappedEntity.Entity, -1);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000548FC File Offset: 0x00052AFC
		private void DetectChangesInComplexProperties()
		{
			if (this.RequiresScalarChangeTracking)
			{
				base.ObjectStateManager.TransactionManager.BeginOriginalValuesGetter();
				try
				{
					this.DetectChangesInProperties(true);
				}
				finally
				{
					base.ObjectStateManager.TransactionManager.EndOriginalValuesGetter();
				}
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0005494C File Offset: 0x00052B4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override CurrentValueRecord CurrentValues
		{
			get
			{
				base.ValidateState();
				if (base.State == EntityState.Deleted)
				{
					throw EntityUtil.CurrentValuesDoesNotExist();
				}
				if (this.IsKeyEntry)
				{
					throw EntityUtil.CannotAccessKeyEntryValues();
				}
				return new ObjectStateEntryDbUpdatableDataRecord(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00054988 File Offset: 0x00052B88
		public override void Delete()
		{
			this.Delete(true);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00054994 File Offset: 0x00052B94
		public override void AcceptChanges()
		{
			base.ValidateState();
			if (base.ObjectStateManager.EntryHasConceptualNull(this))
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
			EntityState state = base.State;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					bool flag = this.RetrieveAndCheckReferentialConstraintValuesInAcceptChanges();
					this._cache.FixupKey(this);
					this._modifiedFields = null;
					this._originalValues = null;
					this._originalComplexObjects = null;
					base.State = EntityState.Unchanged;
					if (flag)
					{
						this.RelationshipManager.CheckReferentialConstraintProperties(this);
					}
					this._wrappedEntity.TakeSnapshot(this);
					return;
				}
			}
			else if (state != EntityState.Deleted)
			{
				if (state != EntityState.Modified)
				{
					return;
				}
				this._cache.ChangeState(this, EntityState.Modified, EntityState.Unchanged);
				this._modifiedFields = null;
				this._originalValues = null;
				this._originalComplexObjects = null;
				base.State = EntityState.Unchanged;
				this._cache.FixupReferencesByForeignKeys(this, false);
				this.RelationshipManager.CheckReferentialConstraintProperties(this);
				this._wrappedEntity.TakeSnapshot(this);
			}
			else
			{
				this.CascadeAcceptChanges();
				if (this._cache != null)
				{
					this._cache.ChangeState(this, EntityState.Deleted, EntityState.Detached);
					return;
				}
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00054A9C File Offset: 0x00052C9C
		public override void SetModified()
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotModifyKeyEntryState();
			}
			if (EntityState.Unchanged == base.State)
			{
				base.State = EntityState.Modified;
				this._cache.ChangeState(this, EntityState.Unchanged, base.State);
				return;
			}
			if (EntityState.Modified != base.State)
			{
				throw EntityUtil.SetModifiedStates("SetModified");
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00054AF7 File Offset: 0x00052CF7
		public override RelationshipManager RelationshipManager
		{
			get
			{
				base.ValidateState();
				if (this.IsKeyEntry)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers);
				}
				if (this.WrappedEntity.Entity == null)
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity);
				}
				return this.WrappedEntity.RelationshipManager;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00054B35 File Offset: 0x00052D35
		internal override BitArray ModifiedProperties
		{
			get
			{
				return this._modifiedFields;
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00054B40 File Offset: 0x00052D40
		public override void ChangeState(EntityState state)
		{
			EntityUtil.CheckValidStateForChangeEntityState(state);
			if (base.State == EntityState.Detached && state == EntityState.Detached)
			{
				return;
			}
			base.ValidateState();
			ObjectStateManager objectStateManager = base.ObjectStateManager;
			objectStateManager.TransactionManager.BeginLocalPublicAPI();
			try
			{
				this.ChangeObjectState(state);
			}
			finally
			{
				objectStateManager.TransactionManager.EndLocalPublicAPI();
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00054BA0 File Offset: 0x00052DA0
		public override void ApplyCurrentValues(object currentEntity)
		{
			EntityUtil.CheckArgumentNull<object>(currentEntity, "currentEntity");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			IEntityWrapper wrappedCurrentEntity = EntityWrapperFactory.WrapEntityUsingStateManager(currentEntity, base.ObjectStateManager);
			this.ApplyCurrentValuesInternal(wrappedCurrentEntity);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00054BE4 File Offset: 0x00052DE4
		public override void ApplyOriginalValues(object originalEntity)
		{
			EntityUtil.CheckArgumentNull<object>(originalEntity, "originalEntity");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			IEntityWrapper wrappedOriginalEntity = EntityWrapperFactory.WrapEntityUsingStateManager(originalEntity, base.ObjectStateManager);
			this.ApplyOriginalValuesInternal(wrappedOriginalEntity);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00054C25 File Offset: 0x00052E25
		internal void AddRelationshipEnd(RelationshipEntry item)
		{
			item.SetNextRelationshipEnd(this.EntityKey, this._headRelationshipEnds);
			this._headRelationshipEnds = item;
			this._countRelationshipEnds++;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00054C50 File Offset: 0x00052E50
		internal bool ContainsRelationshipEnd(RelationshipEntry item)
		{
			for (RelationshipEntry relationshipEntry = this._headRelationshipEnds; relationshipEntry != null; relationshipEntry = relationshipEntry.GetNextRelationshipEnd(this.EntityKey))
			{
				if (relationshipEntry == item)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00054C80 File Offset: 0x00052E80
		internal void RemoveRelationshipEnd(RelationshipEntry item)
		{
			RelationshipEntry relationshipEntry = this._headRelationshipEnds;
			RelationshipEntry relationshipEntry2 = null;
			bool flag = false;
			while (relationshipEntry != null)
			{
				bool flag2 = this.EntityKey == relationshipEntry.Key0 || (this.EntityKey != relationshipEntry.Key1 && this.EntityKey.Equals(relationshipEntry.Key0));
				if (item == relationshipEntry)
				{
					RelationshipEntry relationshipEntry3;
					if (flag2)
					{
						relationshipEntry3 = relationshipEntry.NextKey0;
						relationshipEntry.NextKey0 = null;
					}
					else
					{
						relationshipEntry3 = relationshipEntry.NextKey1;
						relationshipEntry.NextKey1 = null;
					}
					if (relationshipEntry2 == null)
					{
						this._headRelationshipEnds = relationshipEntry3;
					}
					else if (flag)
					{
						relationshipEntry2.NextKey0 = relationshipEntry3;
					}
					else
					{
						relationshipEntry2.NextKey1 = relationshipEntry3;
					}
					this._countRelationshipEnds--;
					return;
				}
				relationshipEntry2 = relationshipEntry;
				relationshipEntry = (flag2 ? relationshipEntry.NextKey0 : relationshipEntry.NextKey1);
				flag = flag2;
			}
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00054D48 File Offset: 0x00052F48
		internal void UpdateRelationshipEnds(EntityKey oldKey, EntityEntry promotedEntry)
		{
			int num = 0;
			RelationshipEntry relationshipEntry = this._headRelationshipEnds;
			while (relationshipEntry != null)
			{
				RelationshipEntry relationshipEntry2 = relationshipEntry;
				relationshipEntry = relationshipEntry.GetNextRelationshipEnd(oldKey);
				relationshipEntry2.ChangeRelatedEnd(oldKey, this.EntityKey);
				if (promotedEntry != null && !promotedEntry.ContainsRelationshipEnd(relationshipEntry2))
				{
					promotedEntry.AddRelationshipEnd(relationshipEntry2);
				}
				num++;
			}
			if (promotedEntry != null)
			{
				this._headRelationshipEnds = null;
				this._countRelationshipEnds = 0;
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00054DA2 File Offset: 0x00052FA2
		internal EntityEntry.RelationshipEndEnumerable GetRelationshipEnds()
		{
			return new EntityEntry.RelationshipEndEnumerable(this);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00054DAA File Offset: 0x00052FAA
		internal override bool IsKeyEntry
		{
			get
			{
				return this._wrappedEntity.Entity == null;
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00054DBA File Offset: 0x00052FBA
		internal override DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject)
		{
			if (Helper.IsEntityType(metadata.CdmMetadata.EdmType) && this._entityKey != null)
			{
				return new EntityRecordInfo(metadata.DataRecordInfo, this._entityKey, (EntitySet)base.EntitySet);
			}
			return metadata.DataRecordInfo;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00054DFC File Offset: 0x00052FFC
		internal override void Reset()
		{
			this.RemoveFromForeignKeyIndex();
			this._cache.ForgetEntryWithConceptualNull(this, true);
			this.DetachObjectStateManagerFromEntity();
			this._wrappedEntity = EntityWrapperFactory.NullWrapper;
			this._entityKey = null;
			this._modifiedFields = null;
			this._originalValues = null;
			this._originalComplexObjects = null;
			this.SetChangeTrackingFlags();
			base.Reset();
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00054E55 File Offset: 0x00053055
		internal override Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata)
		{
			return metadata.GetFieldType(ordinal);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00054E5E File Offset: 0x0005305E
		internal override string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata)
		{
			return metadata.CLayerMemberName(ordinal);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00054E67 File Offset: 0x00053067
		internal override int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata)
		{
			return metadata.GetOrdinalforCLayerMemberName(name);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00054E70 File Offset: 0x00053070
		internal override void RevertDelete()
		{
			base.State = ((this._modifiedFields == null) ? EntityState.Unchanged : EntityState.Modified);
			this._cache.ChangeState(this, EntityState.Deleted, base.State);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00054E98 File Offset: 0x00053098
		internal override int GetFieldCount(StateManagerTypeMetadata metadata)
		{
			return metadata.FieldCount;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00054EA0 File Offset: 0x000530A0
		private void CascadeAcceptChanges()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
			{
				relationshipEntry.AcceptChanges();
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00054ED7 File Offset: 0x000530D7
		internal override void SetModifiedAll()
		{
			base.ValidateState();
			if (this._modifiedFields == null)
			{
				this._modifiedFields = new BitArray(this.GetFieldCount(this._cacheTypeMetadata));
			}
			this._modifiedFields.SetAll(true);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00054F0A File Offset: 0x0005310A
		internal override void EntityMemberChanging(string entityMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			this.EntityMemberChanging(entityMemberName, null, null);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00054F23 File Offset: 0x00053123
		internal override void EntityMemberChanged(string entityMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			this.EntityMemberChanged(entityMemberName, null, null);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00054F3C File Offset: 0x0005313C
		internal override void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			EntityUtil.CheckArgumentNull<string>(complexObjectMemberName, "complexObjectMemberName");
			EntityUtil.CheckArgumentNull<object>(complexObject, "complexObject");
			this.EntityMemberChanging(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00054F6D File Offset: 0x0005316D
		internal override void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotAccessKeyEntryValues();
			}
			EntityUtil.CheckArgumentNull<string>(complexObjectMemberName, "complexObjectMemberName");
			EntityUtil.CheckArgumentNull<object>(complexObject, "complexObject");
			this.EntityMemberChanged(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00054F9E File Offset: 0x0005319E
		internal IEntityWrapper WrappedEntity
		{
			get
			{
				return this._wrappedEntity;
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00054FA8 File Offset: 0x000531A8
		private void EntityMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			try
			{
				StateManagerTypeMetadata stateManagerTypeMetadata;
				string a;
				object obj;
				int andValidateChangeMemberInfo = this.GetAndValidateChangeMemberInfo(entityMemberName, complexObject, complexObjectMemberName, out stateManagerTypeMetadata, out a, out obj);
				if (andValidateChangeMemberInfo != -2)
				{
					if (obj != this._cache.ChangingObject || a != this._cache.ChangingMember || entityMemberName != this._cache.ChangingEntityMember)
					{
						throw EntityUtil.EntityValueChangedWithoutEntityValueChanging();
					}
					if (base.State != this._cache.ChangingState)
					{
						throw EntityUtil.ChangedInDifferentStateFromChanging(base.State, this._cache.ChangingState);
					}
					object changingOldValue = this._cache.ChangingOldValue;
					object obj2 = null;
					StateManagerMemberMetadata stateManagerMemberMetadata = null;
					if (this._cache.SaveOriginalValues)
					{
						stateManagerMemberMetadata = stateManagerTypeMetadata.Member(andValidateChangeMemberInfo);
						if (stateManagerMemberMetadata.IsComplex && changingOldValue != null)
						{
							obj2 = stateManagerMemberMetadata.GetValue(obj);
							this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, changingOldValue, obj2, false);
						}
						else
						{
							this.AddOriginalValue(stateManagerMemberMetadata, obj, changingOldValue);
						}
					}
					TransactionManager transactionManager = base.ObjectStateManager.TransactionManager;
					List<Pair<string, string>> list;
					if (complexObject == null && (transactionManager.IsAlignChanges || !transactionManager.IsDetectChanges) && this.IsPropertyAForeignKey(entityMemberName, out list))
					{
						foreach (Pair<string, string> pair in list)
						{
							string first = pair.First;
							string second = pair.Second;
							RelatedEnd relatedEndInternal = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(first, second);
							EntityReference entityReference = relatedEndInternal as EntityReference;
							if (!transactionManager.IsFixupByReference)
							{
								if (stateManagerMemberMetadata == null)
								{
									stateManagerMemberMetadata = stateManagerTypeMetadata.Member(andValidateChangeMemberInfo);
								}
								if (obj2 == null)
								{
									obj2 = stateManagerMemberMetadata.GetValue(obj);
								}
								bool flag = ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey);
								if (!ByValueEqualityComparer.Default.Equals(changingOldValue, obj2) || flag)
								{
									this.FixupEntityReferenceByForeignKey(entityReference);
								}
							}
						}
					}
					if (this._cache != null && !this._cache.TransactionManager.IsOriginalValuesGetter)
					{
						EntityState state = base.State;
						if (base.State != EntityState.Added)
						{
							base.State = EntityState.Modified;
						}
						if (base.State == EntityState.Modified)
						{
							this.SetModifiedProperty(entityMemberName);
						}
						if (state != base.State)
						{
							this._cache.ChangeState(this, state, base.State);
						}
					}
				}
			}
			finally
			{
				this.SetCachedChangingValues(null, null, null, EntityState.Detached, null);
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0005521C File Offset: 0x0005341C
		internal void SetCurrentEntityValue(string memberName, object newValue)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			this.SetCurrentEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, newValue);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00055250 File Offset: 0x00053450
		internal void SetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, object newValue)
		{
			base.ValidateState();
			if (base.State == EntityState.Added)
			{
				throw EntityUtil.OriginalValuesDoesNotExist();
			}
			EntityState state = base.State;
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			object obj;
			if (this.FindOriginalValue(stateManagerMemberMetadata, userObject, out obj))
			{
				this._originalValues.Remove((StateManagerValue)obj);
			}
			if (stateManagerMemberMetadata.IsComplex)
			{
				object value = stateManagerMemberMetadata.GetValue(userObject);
				if (value == null)
				{
					throw EntityUtil.NullableComplexTypesNotSupported(stateManagerMemberMetadata.CLayerName);
				}
				IExtendedDataRecord extendedDataRecord = newValue as IExtendedDataRecord;
				if (extendedDataRecord != null)
				{
					newValue = this._cache.ComplexTypeMaterializer.CreateComplex(extendedDataRecord, extendedDataRecord.DataRecordInfo, null);
				}
				this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, value, newValue, true);
			}
			else
			{
				this.AddOriginalValue(stateManagerMemberMetadata, userObject, newValue);
			}
			if (state == EntityState.Unchanged)
			{
				base.State = EntityState.Modified;
			}
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005530C File Offset: 0x0005350C
		private void EntityMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata;
			string changingMember;
			object obj;
			int andValidateChangeMemberInfo = this.GetAndValidateChangeMemberInfo(entityMemberName, complexObject, complexObjectMemberName, out stateManagerTypeMetadata, out changingMember, out obj);
			if (andValidateChangeMemberInfo == -2)
			{
				return;
			}
			StateManagerMemberMetadata stateManagerMemberMetadata = stateManagerTypeMetadata.Member(andValidateChangeMemberInfo);
			this._cache.SaveOriginalValues = ((base.State == EntityState.Unchanged || base.State == EntityState.Modified) && !this.FindOriginalValue(stateManagerMemberMetadata, obj));
			object value = stateManagerMemberMetadata.GetValue(obj);
			this.SetCachedChangingValues(entityMemberName, obj, changingMember, base.State, value);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00055380 File Offset: 0x00053580
		internal object GetOriginalEntityValue(string memberName)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			return this.GetOriginalEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, ObjectStateValueRecord.OriginalReadonly);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000553B3 File Offset: 0x000535B3
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord)
		{
			return this.GetOriginalEntityValue(metadata, ordinal, userObject, updatableRecord, -1);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000553C4 File Offset: 0x000535C4
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			base.ValidateState();
			StateManagerMemberMetadata metadata2 = metadata.Member(ordinal);
			object obj;
			if (this.FindOriginalValue(metadata2, userObject, out obj))
			{
				return ((StateManagerValue)obj).originalValue ?? DBNull.Value;
			}
			return this.GetCurrentEntityValue(metadata, ordinal, userObject, updatableRecord, parentEntityPropertyIndex);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005540D File Offset: 0x0005360D
		internal object GetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord)
		{
			return this.GetCurrentEntityValue(metadata, ordinal, userObject, updatableRecord, -1);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005541C File Offset: 0x0005361C
		internal object GetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			base.ValidateState();
			object obj = null;
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			if (!metadata.IsMemberPartofShadowState(ordinal))
			{
				obj = stateManagerMemberMetadata.GetValue(userObject);
				if (stateManagerMemberMetadata.IsComplex && obj != null)
				{
					switch (updatableRecord)
					{
					case ObjectStateValueRecord.OriginalReadonly:
						obj = new ObjectStateEntryDbDataRecord(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj);
						break;
					case ObjectStateValueRecord.CurrentUpdatable:
						obj = new ObjectStateEntryDbUpdatableDataRecord(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj);
						break;
					case ObjectStateValueRecord.OriginalUpdatableInternal:
						obj = new ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj);
						break;
					case ObjectStateValueRecord.OriginalUpdatablePublic:
						obj = new ObjectStateEntryOriginalDbUpdatableDataRecord_Public(this, this._cache.GetOrAddStateManagerTypeMetadata(stateManagerMemberMetadata.CdmMetadata.TypeUsage.EdmType), obj, parentEntityPropertyIndex);
						break;
					}
				}
			}
			return obj ?? DBNull.Value;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00055518 File Offset: 0x00053718
		private bool FindOriginalValue(StateManagerMemberMetadata metadata, object instance)
		{
			object obj;
			return this.FindOriginalValue(metadata, instance, out obj);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00055530 File Offset: 0x00053730
		internal bool FindOriginalValue(StateManagerMemberMetadata metadata, object instance, out object value)
		{
			bool result = false;
			object obj = null;
			if (this._originalValues != null)
			{
				foreach (StateManagerValue stateManagerValue in this._originalValues)
				{
					if (stateManagerValue.userObject == instance && stateManagerValue.memberMetadata == metadata)
					{
						result = true;
						obj = stateManagerValue;
						break;
					}
				}
			}
			value = obj;
			return result;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000555AC File Offset: 0x000537AC
		internal AssociationEndMember GetAssociationEndMember(RelationshipEntry relationshipEntry)
		{
			base.ValidateState();
			return relationshipEntry.RelationshipWrapper.GetAssociationEndMember(this.EntityKey);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000555D2 File Offset: 0x000537D2
		internal EntityEntry GetOtherEndOfRelationship(RelationshipEntry relationshipEntry)
		{
			return this._cache.GetEntityEntry(relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey));
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000555F0 File Offset: 0x000537F0
		private void ExpandComplexTypeAndAddValues(StateManagerMemberMetadata memberMetadata, object oldComplexObject, object newComplexObject, bool useOldComplexObject)
		{
			if (newComplexObject == null)
			{
				throw EntityUtil.NullableComplexTypesNotSupported(memberMetadata.CLayerName);
			}
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(memberMetadata.CdmMetadata.TypeUsage.EdmType);
			for (int i = 0; i < orAddStateManagerTypeMetadata.FieldCount; i++)
			{
				StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
				if (stateManagerMemberMetadata.IsComplex)
				{
					object obj = null;
					if (oldComplexObject != null)
					{
						obj = stateManagerMemberMetadata.GetValue(oldComplexObject);
						object obj2;
						if (obj == null && this.FindOriginalValue(stateManagerMemberMetadata, oldComplexObject, out obj2))
						{
							this._originalValues.Remove((StateManagerValue)obj2);
						}
					}
					this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, obj, stateManagerMemberMetadata.GetValue(newComplexObject), useOldComplexObject);
				}
				else
				{
					object userObject = newComplexObject;
					object value;
					if (useOldComplexObject)
					{
						value = stateManagerMemberMetadata.GetValue(newComplexObject);
						userObject = oldComplexObject;
					}
					else if (oldComplexObject != null)
					{
						value = stateManagerMemberMetadata.GetValue(oldComplexObject);
						object obj2;
						if (this.FindOriginalValue(stateManagerMemberMetadata, oldComplexObject, out obj2))
						{
							StateManagerValue stateManagerValue = (StateManagerValue)obj2;
							this._originalValues.Remove(stateManagerValue);
							value = stateManagerValue.originalValue;
						}
					}
					else
					{
						value = stateManagerMemberMetadata.GetValue(newComplexObject);
					}
					this.AddOriginalValue(stateManagerMemberMetadata, userObject, value);
				}
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000556FC File Offset: 0x000538FC
		internal int GetAndValidateChangeMemberInfo(string entityMemberName, object complexObject, string complexObjectMemberName, out StateManagerTypeMetadata typeMetadata, out string changingMemberName, out object changingObject)
		{
			typeMetadata = null;
			changingMemberName = null;
			changingObject = null;
			EntityUtil.CheckArgumentNull<string>(entityMemberName, "entityMemberName");
			base.ValidateState();
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(entityMemberName);
			if (ordinalforOLayerMemberName != -1)
			{
				StateManagerTypeMetadata stateManagerTypeMetadata;
				string text;
				object obj;
				if (complexObject != null)
				{
					if (!this._cacheTypeMetadata.Member(ordinalforOLayerMemberName).IsComplex)
					{
						throw EntityUtil.ComplexChangeRequestedOnScalarProperty(entityMemberName);
					}
					stateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(complexObject.GetType(), (EntitySet)base.EntitySet);
					ordinalforOLayerMemberName = stateManagerTypeMetadata.GetOrdinalforOLayerMemberName(complexObjectMemberName);
					if (ordinalforOLayerMemberName == -1)
					{
						throw EntityUtil.ChangeOnUnmappedComplexProperty(complexObjectMemberName);
					}
					text = complexObjectMemberName;
					obj = complexObject;
				}
				else
				{
					stateManagerTypeMetadata = this._cacheTypeMetadata;
					text = entityMemberName;
					obj = this.Entity;
					if (this.WrappedEntity.IdentityType != this.Entity.GetType() && this.Entity is IEntityWithChangeTracker && this.IsPropertyAForeignKey(entityMemberName))
					{
						this._cache.EntityInvokingFKSetter = this.WrappedEntity.Entity;
					}
				}
				this.VerifyEntityValueIsEditable(stateManagerTypeMetadata, ordinalforOLayerMemberName, text);
				typeMetadata = stateManagerTypeMetadata;
				changingMemberName = text;
				changingObject = obj;
				return ordinalforOLayerMemberName;
			}
			if (!(entityMemberName == StructuralObject.EntityKeyPropertyName))
			{
				throw EntityUtil.ChangeOnUnmappedProperty(entityMemberName);
			}
			if (!this._cache.InRelationshipFixup)
			{
				throw EntityUtil.CantSetEntityKey();
			}
			this.SetCachedChangingValues(null, null, null, base.State, null);
			return -2;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00055834 File Offset: 0x00053A34
		private void SetCachedChangingValues(string entityMemberName, object changingObject, string changingMember, EntityState changingState, object oldValue)
		{
			this._cache.ChangingEntityMember = entityMemberName;
			this._cache.ChangingObject = changingObject;
			this._cache.ChangingMember = changingMember;
			this._cache.ChangingState = changingState;
			this._cache.ChangingOldValue = oldValue;
			if (changingState == EntityState.Detached)
			{
				this._cache.SaveOriginalValues = false;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00055890 File Offset: 0x00053A90
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal OriginalValueRecord EditableOriginalValues
		{
			get
			{
				return new ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000558AC File Offset: 0x00053AAC
		internal void DetachObjectStateManagerFromEntity()
		{
			if (!this.IsKeyEntry)
			{
				this._wrappedEntity.SetChangeTracker(null);
				this._wrappedEntity.DetachContext();
				if (this._cache.TransactionManager.IsAttachTracking)
				{
					MergeOption? originalMergeOption = this._cache.TransactionManager.OriginalMergeOption;
					MergeOption mergeOption = MergeOption.NoTracking;
					if (originalMergeOption.GetValueOrDefault() == mergeOption & originalMergeOption != null)
					{
						return;
					}
				}
				this._wrappedEntity.EntityKey = null;
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00055920 File Offset: 0x00053B20
		internal void TakeSnapshot(bool onlySnapshotComplexProperties)
		{
			if (base.State != EntityState.Added)
			{
				StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
				int fieldCount = this.GetFieldCount(cacheTypeMetadata);
				for (int i = 0; i < fieldCount; i++)
				{
					StateManagerMemberMetadata stateManagerMemberMetadata = cacheTypeMetadata.Member(i);
					if (stateManagerMemberMetadata.IsComplex)
					{
						object value = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
						this.AddComplexObjectSnapshot(this.Entity, i, value);
						this.TakeSnapshotOfComplexType(stateManagerMemberMetadata, value);
					}
					else if (!onlySnapshotComplexProperties)
					{
						object value = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
						this.AddOriginalValue(stateManagerMemberMetadata, this._wrappedEntity.Entity, value);
					}
				}
			}
			this.TakeSnapshotOfForeignKeys();
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000559C4 File Offset: 0x00053BC4
		internal void TakeSnapshotOfForeignKeys()
		{
			Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
			this.FindRelatedEntityKeysByForeignKeys(out dictionary, false);
			if (dictionary != null)
			{
				foreach (KeyValuePair<RelatedEnd, HashSet<EntityKey>> keyValuePair in dictionary)
				{
					EntityReference entityReference = keyValuePair.Key as EntityReference;
					if (!ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
					{
						entityReference.SetCachedForeignKey(keyValuePair.Value.First<EntityKey>(), this);
					}
				}
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00055A44 File Offset: 0x00053C44
		private void TakeSnapshotOfComplexType(StateManagerMemberMetadata member, object complexValue)
		{
			if (complexValue == null)
			{
				return;
			}
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(member.CdmMetadata.TypeUsage.EdmType);
			for (int i = 0; i < orAddStateManagerTypeMetadata.FieldCount; i++)
			{
				StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
				object value = stateManagerMemberMetadata.GetValue(complexValue);
				if (stateManagerMemberMetadata.IsComplex)
				{
					this.AddComplexObjectSnapshot(complexValue, i, value);
					this.TakeSnapshotOfComplexType(stateManagerMemberMetadata, value);
				}
				else if (!this.FindOriginalValue(stateManagerMemberMetadata, complexValue))
				{
					this.AddOriginalValue(stateManagerMemberMetadata, complexValue, value);
				}
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00055AC0 File Offset: 0x00053CC0
		private void AddComplexObjectSnapshot(object userObject, int ordinal, object complexObject)
		{
			if (complexObject == null)
			{
				return;
			}
			this.CheckForDuplicateComplexObjects(complexObject);
			if (this._originalComplexObjects == null)
			{
				this._originalComplexObjects = new Dictionary<object, Dictionary<int, object>>();
			}
			Dictionary<int, object> dictionary;
			if (!this._originalComplexObjects.TryGetValue(userObject, out dictionary))
			{
				dictionary = new Dictionary<int, object>();
				this._originalComplexObjects.Add(userObject, dictionary);
			}
			dictionary.Add(ordinal, complexObject);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00055B18 File Offset: 0x00053D18
		private void CheckForDuplicateComplexObjects(object complexObject)
		{
			if (this._originalComplexObjects == null || complexObject == null)
			{
				return;
			}
			foreach (Dictionary<int, object> dictionary in this._originalComplexObjects.Values)
			{
				foreach (object obj in dictionary.Values)
				{
					if (complexObject == obj)
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_ComplexObjectUsedMultipleTimes(this.Entity.GetType().FullName, complexObject.GetType().FullName));
					}
				}
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00055BDC File Offset: 0x00053DDC
		public override bool IsPropertyChanged(string propertyName)
		{
			return this.DetectChangesInProperty(this.ValidateAndGetOrdinalForProperty(propertyName, "IsPropertyChanged"), false, true);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00055BF4 File Offset: 0x00053DF4
		private bool DetectChangesInProperty(int ordinal, bool detectOnlyComplexProperties, bool detectOnly)
		{
			bool flag = false;
			StateManagerMemberMetadata stateManagerMemberMetadata = this._cacheTypeMetadata.Member(ordinal);
			object value = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (base.State != EntityState.Deleted)
				{
					object complexObjectSnapshot = this.GetComplexObjectSnapshot(this.Entity, ordinal);
					bool flag2 = this.DetectChangesInComplexType(stateManagerMemberMetadata, stateManagerMemberMetadata, value, complexObjectSnapshot, ref flag, detectOnly);
					if (flag2)
					{
						this.CheckForDuplicateComplexObjects(value);
						if (!detectOnly)
						{
							((IEntityChangeTracker)this).EntityMemberChanging(stateManagerMemberMetadata.CLayerName);
							this._cache.ChangingOldValue = complexObjectSnapshot;
							((IEntityChangeTracker)this).EntityMemberChanged(stateManagerMemberMetadata.CLayerName);
						}
						this.UpdateComplexObjectSnapshot(stateManagerMemberMetadata, this.Entity, ordinal, value);
						if (!flag)
						{
							this.DetectChangesInComplexType(stateManagerMemberMetadata, stateManagerMemberMetadata, value, complexObjectSnapshot, ref flag, detectOnly);
						}
					}
				}
			}
			else if (!detectOnlyComplexProperties)
			{
				object obj;
				bool flag3 = this.FindOriginalValue(stateManagerMemberMetadata, this._wrappedEntity.Entity, out obj);
				object originalValue = ((StateManagerValue)obj).originalValue;
				if (!object.Equals(value, originalValue))
				{
					flag = true;
					if (stateManagerMemberMetadata.IsPartOfKey)
					{
						if (!ByValueEqualityComparer.Default.Equals(value, originalValue))
						{
							throw EntityUtil.CannotModifyKeyProperty(stateManagerMemberMetadata.CLayerName);
						}
					}
					else if (base.State != EntityState.Deleted && !detectOnly)
					{
						((IEntityChangeTracker)this).EntityMemberChanging(stateManagerMemberMetadata.CLayerName);
						((IEntityChangeTracker)this).EntityMemberChanged(stateManagerMemberMetadata.CLayerName);
					}
				}
			}
			return flag;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00055D2C File Offset: 0x00053F2C
		internal void DetectChangesInProperties(bool detectOnlyComplexProperties)
		{
			int fieldCount = this.GetFieldCount(this._cacheTypeMetadata);
			for (int i = 0; i < fieldCount; i++)
			{
				this.DetectChangesInProperty(i, detectOnlyComplexProperties, false);
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00055D5C File Offset: 0x00053F5C
		private bool DetectChangesInComplexType(StateManagerMemberMetadata topLevelMember, StateManagerMemberMetadata complexMember, object complexValue, object oldComplexValue, ref bool changeDetected, bool detectOnly)
		{
			if (complexValue == null)
			{
				if (oldComplexValue == null)
				{
					return false;
				}
				throw EntityUtil.NullableComplexTypesNotSupported(complexMember.CLayerName);
			}
			else
			{
				if (oldComplexValue != complexValue)
				{
					return true;
				}
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(complexMember.CdmMetadata.TypeUsage.EdmType);
				for (int i = 0; i < this.GetFieldCount(orAddStateManagerTypeMetadata); i++)
				{
					StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
					object value = stateManagerMemberMetadata.GetValue(complexValue);
					if (stateManagerMemberMetadata.IsComplex)
					{
						if (base.State != EntityState.Deleted)
						{
							object complexObjectSnapshot = this.GetComplexObjectSnapshot(complexValue, i);
							bool flag = this.DetectChangesInComplexType(topLevelMember, stateManagerMemberMetadata, value, complexObjectSnapshot, ref changeDetected, detectOnly);
							if (flag)
							{
								this.CheckForDuplicateComplexObjects(value);
								if (!detectOnly)
								{
									((IEntityChangeTracker)this).EntityComplexMemberChanging(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
									this._cache.ChangingOldValue = complexObjectSnapshot;
									((IEntityChangeTracker)this).EntityComplexMemberChanged(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
								}
								this.UpdateComplexObjectSnapshot(stateManagerMemberMetadata, complexValue, i, value);
								if (!changeDetected)
								{
									this.DetectChangesInComplexType(topLevelMember, stateManagerMemberMetadata, value, complexObjectSnapshot, ref changeDetected, detectOnly);
								}
							}
						}
					}
					else
					{
						object obj;
						bool flag2 = this.FindOriginalValue(stateManagerMemberMetadata, complexValue, out obj);
						if (!object.Equals(value, flag2 ? ((StateManagerValue)obj).originalValue : null))
						{
							changeDetected = true;
							if (!detectOnly)
							{
								((IEntityChangeTracker)this).EntityComplexMemberChanging(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
								((IEntityChangeTracker)this).EntityComplexMemberChanged(topLevelMember.CLayerName, complexValue, stateManagerMemberMetadata.CLayerName);
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00055EBC File Offset: 0x000540BC
		private object GetComplexObjectSnapshot(object parentObject, int parentOrdinal)
		{
			object result = null;
			Dictionary<int, object> dictionary;
			if (this._originalComplexObjects != null && this._originalComplexObjects.TryGetValue(parentObject, out dictionary))
			{
				dictionary.TryGetValue(parentOrdinal, out result);
			}
			return result;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00055EF0 File Offset: 0x000540F0
		internal void UpdateComplexObjectSnapshot(StateManagerMemberMetadata member, object userObject, int ordinal, object currentValue)
		{
			bool flag = true;
			Dictionary<int, object> dictionary;
			if (this._originalComplexObjects != null && this._originalComplexObjects.TryGetValue(userObject, out dictionary))
			{
				object obj;
				dictionary.TryGetValue(ordinal, out obj);
				dictionary[ordinal] = currentValue;
				if (obj != null && this._originalComplexObjects.TryGetValue(obj, out dictionary))
				{
					this._originalComplexObjects.Remove(obj);
					this._originalComplexObjects.Add(currentValue, dictionary);
					StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(member.CdmMetadata.TypeUsage.EdmType);
					for (int i = 0; i < orAddStateManagerTypeMetadata.FieldCount; i++)
					{
						StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(i);
						if (stateManagerMemberMetadata.IsComplex)
						{
							object value = stateManagerMemberMetadata.GetValue(currentValue);
							this.UpdateComplexObjectSnapshot(stateManagerMemberMetadata, currentValue, i, value);
						}
					}
				}
				flag = false;
			}
			if (flag)
			{
				this.AddComplexObjectSnapshot(userObject, ordinal, currentValue);
			}
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00055FD0 File Offset: 0x000541D0
		internal void FixupFKValuesFromNonAddedReferences()
		{
			if (!((EntitySet)base.EntitySet).HasForeignKeyRelationships)
			{
				return;
			}
			Dictionary<int, object> changedFKs = new Dictionary<int, object>();
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				if (entityReference.TargetAccessor.HasProperty)
				{
					object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(entityReference);
					ObjectStateEntry objectStateEntry;
					if (navigationPropertyValue != null && this._cache.TryGetObjectStateEntry(navigationPropertyValue, out objectStateEntry) && (objectStateEntry.State == EntityState.Modified || objectStateEntry.State == EntityState.Unchanged))
					{
						entityReference.UpdateForeignKeyValues(this.WrappedEntity, ((EntityEntry)objectStateEntry).WrappedEntity, changedFKs, false);
					}
				}
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000560C8 File Offset: 0x000542C8
		internal void TakeSnapshotOfRelationships()
		{
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
			ReadOnlyMetadataCollection<NavigationProperty> navigationProperties = (cacheTypeMetadata.CdmMetadata.EdmType as EntityType).NavigationProperties;
			foreach (NavigationProperty navigationProperty in navigationProperties)
			{
				RelatedEnd relatedEndInternal = relationshipManager.GetRelatedEndInternal(navigationProperty.RelationshipType.FullName, navigationProperty.ToEndMember.Name);
				object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(relatedEndInternal);
				if (navigationPropertyValue != null)
				{
					if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						IEnumerable enumerable = navigationPropertyValue as IEnumerable;
						if (enumerable == null)
						{
							throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(navigationProperty.Name, this.Entity.GetType().FullName));
						}
						using (IEnumerator enumerator2 = enumerable.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								if (obj != null)
								{
									this.TakeSnapshotOfSingleRelationship(relatedEndInternal, navigationProperty, obj);
								}
							}
							continue;
						}
					}
					this.TakeSnapshotOfSingleRelationship(relatedEndInternal, navigationProperty, navigationPropertyValue);
				}
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00056210 File Offset: 0x00054410
		private void TakeSnapshotOfSingleRelationship(RelatedEnd relatedEnd, NavigationProperty n, object o)
		{
			EntityEntry entityEntry = base.ObjectStateManager.FindEntityEntry(o);
			IEntityWrapper entityWrapper;
			if (entityEntry != null)
			{
				entityWrapper = entityEntry._wrappedEntity;
				RelatedEnd relatedEndInternal = entityWrapper.RelationshipManager.GetRelatedEndInternal(n.RelationshipType.FullName, n.FromEndMember.Name);
				if (!relatedEndInternal.ContainsEntity(this._wrappedEntity))
				{
					if (entityWrapper.ObjectStateEntry.State == EntityState.Deleted)
					{
						throw EntityUtil.UnableToAddRelationshipWithDeletedEntity();
					}
					if (base.ObjectStateManager.TransactionManager.IsAttachTracking && (base.State & (EntityState.Unchanged | EntityState.Modified)) != (EntityState)0 && (entityWrapper.ObjectStateEntry.State & (EntityState.Unchanged | EntityState.Modified)) != (EntityState)0)
					{
						EntityEntry entityEntry2 = null;
						EntityEntry @object = null;
						if (relatedEnd.IsDependentEndOfReferentialConstraint(false))
						{
							entityEntry2 = entityWrapper.ObjectStateEntry;
							@object = this;
						}
						else if (relatedEndInternal.IsDependentEndOfReferentialConstraint(false))
						{
							entityEntry2 = this;
							@object = entityWrapper.ObjectStateEntry;
						}
						if (entityEntry2 != null)
						{
							ReferentialConstraint constraint = ((AssociationType)relatedEnd.RelationMetadata).ReferentialConstraints[0];
							if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(@object.GetCurrentEntityValue), entityEntry2.EntityKey))
							{
								throw EntityUtil.InconsistentReferentialConstraintProperties();
							}
						}
					}
					EntityReference entityReference = relatedEndInternal as EntityReference;
					if (entityReference != null && entityReference.NavigationPropertyIsNullOrMissing())
					{
						base.ObjectStateManager.TransactionManager.AlignedEntityReferences.Add(entityReference);
					}
					relatedEndInternal.AddToLocalCache(this._wrappedEntity, true);
					relatedEndInternal.OnAssociationChanged(CollectionChangeAction.Add, this._wrappedEntity.Entity);
				}
			}
			else if (!base.ObjectStateManager.TransactionManager.WrappedEntities.TryGetValue(o, out entityWrapper))
			{
				entityWrapper = EntityWrapperFactory.WrapEntityUsingStateManager(o, base.ObjectStateManager);
			}
			if (!relatedEnd.ContainsEntity(entityWrapper))
			{
				relatedEnd.AddToLocalCache(entityWrapper, true);
				relatedEnd.OnAssociationChanged(CollectionChangeAction.Add, entityWrapper.Entity);
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000563AC File Offset: 0x000545AC
		internal void DetectChangesInRelationshipsOfSingleEntity()
		{
			StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
			ReadOnlyMetadataCollection<NavigationProperty> navigationProperties = (cacheTypeMetadata.CdmMetadata.EdmType as EntityType).NavigationProperties;
			foreach (NavigationProperty navigationProperty in navigationProperties)
			{
				RelatedEnd relatedEndInternal = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(navigationProperty.RelationshipType.FullName, navigationProperty.ToEndMember.Name);
				object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(relatedEndInternal);
				HashSet<object> hashSet = new HashSet<object>();
				if (navigationPropertyValue != null)
				{
					if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						IEnumerable enumerable = navigationPropertyValue as IEnumerable;
						if (enumerable == null)
						{
							throw new EntityException(Strings.ObjectStateEntry_UnableToEnumerateCollection(navigationProperty.Name, this.Entity.GetType().FullName));
						}
						using (IEnumerator enumerator2 = enumerable.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								if (obj != null)
								{
									hashSet.Add(obj);
								}
							}
							goto IL_FE;
						}
					}
					hashSet.Add(navigationPropertyValue);
				}
				IL_FE:
				foreach (object obj2 in relatedEndInternal.GetInternalEnumerable())
				{
					if (!hashSet.Contains(obj2))
					{
						this.AddRelationshipDetectedByGraph(base.ObjectStateManager.TransactionManager.DeletedRelationshipsByGraph, obj2, relatedEndInternal, false);
					}
					else
					{
						hashSet.Remove(obj2);
					}
				}
				foreach (object relatedObject in hashSet)
				{
					this.AddRelationshipDetectedByGraph(base.ObjectStateManager.TransactionManager.AddedRelationshipsByGraph, relatedObject, relatedEndInternal, true);
				}
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000565EC File Offset: 0x000547EC
		private void AddRelationshipDetectedByGraph(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> relationships, object relatedObject, RelatedEnd relatedEndFrom, bool verifyForAdd)
		{
			IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingStateManager(relatedObject, base.ObjectStateManager);
			this.AddDetectedRelationship<IEntityWrapper>(relationships, entityWrapper, relatedEndFrom);
			RelatedEnd otherEndOfRelationship = relatedEndFrom.GetOtherEndOfRelationship(entityWrapper);
			if (verifyForAdd && otherEndOfRelationship is EntityReference && base.ObjectStateManager.FindEntityEntry(relatedObject) == null)
			{
				otherEndOfRelationship.VerifyNavigationPropertyForAdd(this._wrappedEntity);
			}
			this.AddDetectedRelationship<IEntityWrapper>(relationships, this._wrappedEntity, otherEndOfRelationship);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0005664C File Offset: 0x0005484C
		private void AddRelationshipDetectedByForeignKey(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> relationships, Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> principalRelationships, EntityKey relatedKey, EntityEntry relatedEntry, RelatedEnd relatedEndFrom)
		{
			this.AddDetectedRelationship<EntityKey>(relationships, relatedKey, relatedEndFrom);
			if (relatedEntry != null)
			{
				IEntityWrapper wrappedEntity = relatedEntry.WrappedEntity;
				RelatedEnd otherEndOfRelationship = relatedEndFrom.GetOtherEndOfRelationship(wrappedEntity);
				EntityKey permanentKey = base.ObjectStateManager.GetPermanentKey(relatedEntry.WrappedEntity, otherEndOfRelationship, this.WrappedEntity);
				this.AddDetectedRelationship<EntityKey>(principalRelationships, permanentKey, otherEndOfRelationship);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0005669C File Offset: 0x0005489C
		private void AddDetectedRelationship<T>(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<T>>> relationships, T relatedObject, RelatedEnd relatedEnd)
		{
			Dictionary<RelatedEnd, HashSet<T>> dictionary;
			if (!relationships.TryGetValue(relatedEnd.WrappedOwner, out dictionary))
			{
				dictionary = new Dictionary<RelatedEnd, HashSet<T>>();
				relationships.Add(relatedEnd.WrappedOwner, dictionary);
			}
			HashSet<T> hashSet;
			if (!dictionary.TryGetValue(relatedEnd, out hashSet))
			{
				hashSet = new HashSet<T>();
				dictionary.Add(relatedEnd, hashSet);
			}
			else if (relatedEnd is EntityReference)
			{
				T t = hashSet.First<T>();
				if (!object.Equals(t, relatedObject))
				{
					throw EntityUtil.CannotAddMoreThanOneEntityToEntityReference(relatedEnd.RelationshipNavigation.To, relatedEnd.RelationshipNavigation.RelationshipName);
				}
			}
			hashSet.Add(relatedObject);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00056730 File Offset: 0x00054930
		internal void Detach()
		{
			base.ValidateState();
			bool flag = false;
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			flag = (base.State != EntityState.Added && this.IsOneEndOfSomeRelationship());
			this._cache.TransactionManager.BeginDetaching();
			try
			{
				relationshipManager.DetachEntityFromRelationships(base.State);
			}
			finally
			{
				this._cache.TransactionManager.EndDetaching();
			}
			this.DetachRelationshipsEntries(relationshipManager);
			IEntityWrapper wrappedEntity = this._wrappedEntity;
			EntityKey entityKey = this._entityKey;
			EntityState state = base.State;
			if (flag)
			{
				this.DegradeEntry();
			}
			else
			{
				this._wrappedEntity.ObjectStateEntry = null;
				this._cache.ChangeState(this, base.State, EntityState.Detached);
			}
			if (state != EntityState.Added)
			{
				wrappedEntity.EntityKey = entityKey;
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000567F8 File Offset: 0x000549F8
		internal void Delete(bool doFixup)
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw EntityUtil.CannotCallDeleteOnKeyEntry();
			}
			if (doFixup && base.State != EntityState.Deleted)
			{
				this.RelationshipManager.NullAllFKsInDependentsForWhichThisIsThePrincipal();
				this.NullAllForeignKeys();
				this.FixupRelationships();
			}
			EntityState state = base.State;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					this._cache.ChangeState(this, EntityState.Added, EntityState.Detached);
					return;
				}
				else
				{
					if (!doFixup)
					{
						this.DeleteRelationshipsThatReferenceKeys(null, null);
					}
					this._cache.ChangeState(this, EntityState.Unchanged, EntityState.Deleted);
					base.State = EntityState.Deleted;
				}
			}
			else if (state != EntityState.Deleted)
			{
				if (state != EntityState.Modified)
				{
					return;
				}
				if (!doFixup)
				{
					this.DeleteRelationshipsThatReferenceKeys(null, null);
				}
				this._cache.ChangeState(this, EntityState.Modified, EntityState.Deleted);
				base.State = EntityState.Deleted;
				return;
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000568AC File Offset: 0x00054AAC
		private void NullAllForeignKeys()
		{
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				entityReference.NullAllForeignKeys();
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00056930 File Offset: 0x00054B30
		private bool IsOneEndOfSomeRelationship()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(this.EntityKey))
			{
				RelationshipMultiplicity relationshipMultiplicity = this.GetAssociationEndMember(relationshipEntry).RelationshipMultiplicity;
				if (relationshipMultiplicity == RelationshipMultiplicity.One || relationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				{
					EntityKey otherEntityKey = relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey);
					EntityEntry entityEntry = this._cache.GetEntityEntry(otherEntityKey);
					if (!entityEntry.IsKeyEntry)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000569D4 File Offset: 0x00054BD4
		private void DetachRelationshipsEntries(RelationshipManager relationshipManager)
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
			{
				EntityKey otherEntityKey = relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey);
				EntityEntry entityEntry = this._cache.GetEntityEntry(otherEntityKey);
				if (entityEntry.IsKeyEntry)
				{
					if (relationshipEntry.State != EntityState.Deleted)
					{
						AssociationEndMember associationEndMember = relationshipEntry.RelationshipWrapper.GetAssociationEndMember(otherEntityKey);
						EntityReference entityReference = (EntityReference)relationshipManager.GetRelatedEndInternal(associationEndMember.DeclaringType.FullName, associationEndMember.Name);
						entityReference.DetachedEntityKey = otherEntityKey;
					}
					relationshipEntry.DeleteUnnecessaryKeyEntries();
					relationshipEntry.DetachRelationshipEntry();
				}
				else if (relationshipEntry.State == EntityState.Deleted)
				{
					RelationshipMultiplicity relationshipMultiplicity = this.GetAssociationEndMember(relationshipEntry).RelationshipMultiplicity;
					if (relationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						relationshipEntry.DetachRelationshipEntry();
					}
				}
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00056AA4 File Offset: 0x00054CA4
		private void FixupRelationships()
		{
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			relationshipManager.RemoveEntityFromRelationships();
			this.DeleteRelationshipsThatReferenceKeys(null, null);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00056ACC File Offset: 0x00054CCC
		internal void DeleteRelationshipsThatReferenceKeys(RelationshipSet relationshipSet, RelationshipEndMember endMember)
		{
			if (base.State != EntityState.Detached)
			{
				foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
				{
					if (relationshipEntry.State != EntityState.Deleted && (relationshipSet == null || relationshipSet == relationshipEntry.EntitySet))
					{
						EntityEntry otherEndOfRelationship = this.GetOtherEndOfRelationship(relationshipEntry);
						if (endMember == null || endMember == otherEndOfRelationship.GetAssociationEndMember(relationshipEntry))
						{
							for (int j = 0; j < 2; j++)
							{
								EntityKey entityKey = relationshipEntry.GetCurrentRelationValue(j) as EntityKey;
								if (entityKey != null)
								{
									EntityEntry entityEntry = this._cache.GetEntityEntry(entityKey);
									if (entityEntry.IsKeyEntry)
									{
										relationshipEntry.Delete(false);
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00056B78 File Offset: 0x00054D78
		private bool RetrieveAndCheckReferentialConstraintValuesInAcceptChanges()
		{
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			List<string> list;
			bool flag;
			bool result = relationshipManager.FindNamesOfReferentialConstraintProperties(out list, out flag, true);
			if (list != null)
			{
				HashSet<object> visited = new HashSet<object>();
				Dictionary<string, KeyValuePair<object, IntBox>> dictionary;
				relationshipManager.RetrieveReferentialConstraintProperties(out dictionary, visited, false);
				foreach (KeyValuePair<string, KeyValuePair<object, IntBox>> keyValuePair in dictionary)
				{
					this.SetCurrentEntityValue(keyValuePair.Key, keyValuePair.Value.Key);
				}
			}
			if (flag)
			{
				this.CheckReferentialConstraintPropertiesInDependents();
			}
			return result;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00056C18 File Offset: 0x00054E18
		internal void RetrieveReferentialConstraintPropertiesFromKeyEntries(Dictionary<string, KeyValuePair<object, IntBox>> properties)
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(this.EntityKey))
			{
				EntityEntry otherEndOfRelationship = this.GetOtherEndOfRelationship(relationshipEntry);
				if (otherEndOfRelationship.IsKeyEntry)
				{
					AssociationSet associationSet = (AssociationSet)relationshipEntry.EntitySet;
					foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
					{
						string name = this.GetAssociationEndMember(relationshipEntry).Name;
						if (referentialConstraint.ToRole.Name == name)
						{
							IList<EntityKeyMember> entityKeyValues = otherEndOfRelationship.EntityKey.EntityKeyValues;
							foreach (EntityKeyMember entityKeyMember in entityKeyValues)
							{
								for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
								{
									if (referentialConstraint.FromProperties[i].Name == entityKeyMember.Key)
									{
										EntityEntry.AddOrIncreaseCounter(properties, referentialConstraint.ToProperties[i].Name, entityKeyMember.Value);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00056DD0 File Offset: 0x00054FD0
		internal static void AddOrIncreaseCounter(Dictionary<string, KeyValuePair<object, IntBox>> properties, string propertyName, object propertyValue)
		{
			if (!properties.ContainsKey(propertyName))
			{
				properties[propertyName] = new KeyValuePair<object, IntBox>(propertyValue, new IntBox(1));
				return;
			}
			KeyValuePair<object, IntBox> keyValuePair = properties[propertyName];
			if (!ByValueEqualityComparer.Default.Equals(keyValuePair.Key, propertyValue))
			{
				throw EntityUtil.InconsistentReferentialConstraintProperties();
			}
			keyValuePair.Value.Value = keyValuePair.Value.Value + 1;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00056E38 File Offset: 0x00055038
		private void CheckReferentialConstraintPropertiesInDependents()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(this.EntityKey))
			{
				EntityEntry otherEndOfRelationship = this.GetOtherEndOfRelationship(relationshipEntry);
				if (otherEndOfRelationship.State == EntityState.Unchanged || otherEndOfRelationship.State == EntityState.Modified)
				{
					AssociationSet associationSet = (AssociationSet)relationshipEntry.EntitySet;
					foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
					{
						string name = this.GetAssociationEndMember(relationshipEntry).Name;
						if (referentialConstraint.FromRole.Name == name)
						{
							IList<EntityKeyMember> entityKeyValues = otherEndOfRelationship.EntityKey.EntityKeyValues;
							foreach (EntityKeyMember entityKeyMember in entityKeyValues)
							{
								for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
								{
									if (referentialConstraint.ToProperties[i].Name == entityKeyMember.Key && !ByValueEqualityComparer.Default.Equals(this.GetCurrentEntityValue(referentialConstraint.FromProperties[i].Name), entityKeyMember.Value))
									{
										throw EntityUtil.InconsistentReferentialConstraintProperties();
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0005700C File Offset: 0x0005520C
		internal void PromoteKeyEntry(IEntityWrapper wrappedEntity, IExtendedDataRecord shadowValues, StateManagerTypeMetadata typeMetadata)
		{
			this._wrappedEntity = wrappedEntity;
			this._wrappedEntity.ObjectStateEntry = this;
			this._cacheTypeMetadata = typeMetadata;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00057030 File Offset: 0x00055230
		internal void DegradeEntry()
		{
			this._entityKey = this.EntityKey;
			this.RemoveFromForeignKeyIndex();
			this._wrappedEntity.SetChangeTracker(null);
			this._modifiedFields = null;
			this._originalValues = null;
			this._originalComplexObjects = null;
			if (base.State == EntityState.Added)
			{
				this._wrappedEntity.EntityKey = null;
				this._entityKey = null;
			}
			if (base.State != EntityState.Unchanged)
			{
				this._cache.ChangeState(this, base.State, EntityState.Unchanged);
				base.State = EntityState.Unchanged;
			}
			this._cache.RemoveEntryFromKeylessStore(this._wrappedEntity);
			this._wrappedEntity.DetachContext();
			this._wrappedEntity.ObjectStateEntry = null;
			object entity = this._wrappedEntity.Entity;
			this._wrappedEntity = EntityWrapperFactory.NullWrapper;
			this.SetChangeTrackingFlags();
			this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Remove, entity);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00057101 File Offset: 0x00055301
		internal void AttachObjectStateManagerToEntity()
		{
			this._wrappedEntity.SetChangeTracker(this);
			this._wrappedEntity.TakeSnapshot(this);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005711C File Offset: 0x0005531C
		internal void GetOtherKeyProperties(Dictionary<string, KeyValuePair<object, IntBox>> properties)
		{
			EntityType entityType = this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType as EntityType;
			foreach (EdmMember edmMember in entityType.KeyMembers)
			{
				if (!properties.ContainsKey(edmMember.Name))
				{
					properties[edmMember.Name] = new KeyValuePair<object, IntBox>(this.GetCurrentEntityValue(edmMember.Name), new IntBox(1));
				}
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000571B4 File Offset: 0x000553B4
		internal void AddOriginalValue(StateManagerMemberMetadata memberMetadata, object userObject, object value)
		{
			if (this._originalValues == null)
			{
				this._originalValues = new List<StateManagerValue>();
			}
			this._originalValues.Add(new StateManagerValue(memberMetadata, userObject, value));
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000571DC File Offset: 0x000553DC
		internal void CompareKeyProperties(object changed)
		{
			StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
			int fieldCount = this.GetFieldCount(cacheTypeMetadata);
			for (int i = 0; i < fieldCount; i++)
			{
				StateManagerMemberMetadata stateManagerMemberMetadata = cacheTypeMetadata.Member(i);
				if (stateManagerMemberMetadata.IsPartOfKey)
				{
					object value = stateManagerMemberMetadata.GetValue(changed);
					object value2 = stateManagerMemberMetadata.GetValue(this._wrappedEntity.Entity);
					if (!ByValueEqualityComparer.Default.Equals(value, value2))
					{
						throw EntityUtil.CannotModifyKeyProperty(stateManagerMemberMetadata.CLayerName);
					}
				}
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00057254 File Offset: 0x00055454
		internal object GetCurrentEntityValue(string memberName)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			return this.GetCurrentEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, ObjectStateValueRecord.CurrentUpdatable);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00057288 File Offset: 0x00055488
		internal void VerifyEntityValueIsEditable(StateManagerTypeMetadata typeMetadata, int ordinal, string memberName)
		{
			if (base.State == EntityState.Deleted)
			{
				throw EntityUtil.CantModifyDetachedDeletedEntries();
			}
			StateManagerMemberMetadata stateManagerMemberMetadata = typeMetadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsPartOfKey && base.State != EntityState.Added)
			{
				throw EntityUtil.CannotModifyKeyProperty(memberName);
			}
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000572C4 File Offset: 0x000554C4
		internal void SetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, object newValue)
		{
			base.ValidateState();
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (newValue == null || newValue == DBNull.Value)
				{
					throw EntityUtil.NullableComplexTypesNotSupported(stateManagerMemberMetadata.CLayerName);
				}
				IExtendedDataRecord extendedDataRecord = newValue as IExtendedDataRecord;
				if (extendedDataRecord == null)
				{
					throw EntityUtil.InvalidTypeForComplexTypeProperty("value");
				}
				newValue = this._cache.ComplexTypeMaterializer.CreateComplex(extendedDataRecord, extendedDataRecord.DataRecordInfo, null);
			}
			this._wrappedEntity.SetCurrentValue(this, stateManagerMemberMetadata, ordinal, userObject, newValue);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00057344 File Offset: 0x00055544
		private void TransitionRelationshipsForAdd()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
			{
				if (relationshipEntry.State == EntityState.Unchanged)
				{
					base.ObjectStateManager.ChangeState(relationshipEntry, EntityState.Unchanged, EntityState.Added);
					relationshipEntry.State = EntityState.Added;
				}
				else if (relationshipEntry.State == EntityState.Deleted)
				{
					relationshipEntry.DeleteUnnecessaryKeyEntries();
					relationshipEntry.DetachRelationshipEntry();
				}
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		private void VerifyIsNotRelated()
		{
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000573AC File Offset: 0x000555AC
		internal void ChangeObjectState(EntityState requestedState)
		{
			if (!this.IsKeyEntry)
			{
				EntityState state = base.State;
				switch (state)
				{
				case EntityState.Detached:
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Unchanged:
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Detach();
						return;
					case EntityState.Unchanged:
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						base.ObjectStateManager.ReplaceKeyWithTemporaryKey(this);
						this._modifiedFields = null;
						this._originalValues = null;
						this._originalComplexObjects = null;
						base.State = EntityState.Added;
						this.TransitionRelationshipsForAdd();
						return;
					default:
						if (requestedState == EntityState.Deleted)
						{
							this.Delete(true);
							return;
						}
						if (requestedState == EntityState.Modified)
						{
							this.SetModified();
							this.SetModifiedAll();
							return;
						}
						break;
					}
					throw EntityUtil.InvalidEntityStateArgument("state");
				case EntityState.Added:
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Detach();
						return;
					case EntityState.Unchanged:
						this.AcceptChanges();
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						this.TransitionRelationshipsForAdd();
						return;
					default:
						if (requestedState == EntityState.Deleted)
						{
							this._cache.ForgetEntryWithConceptualNull(this, true);
							this.AcceptChanges();
							this.Delete(true);
							return;
						}
						if (requestedState == EntityState.Modified)
						{
							this.AcceptChanges();
							this.SetModified();
							this.SetModifiedAll();
							return;
						}
						break;
					}
					throw EntityUtil.InvalidEntityStateArgument("state");
				default:
					if (state == EntityState.Deleted)
					{
						switch (requestedState)
						{
						case EntityState.Detached:
							this.Detach();
							return;
						case EntityState.Unchanged:
							this._modifiedFields = null;
							this._originalValues = null;
							this._originalComplexObjects = null;
							base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Unchanged);
							base.State = EntityState.Unchanged;
							this._wrappedEntity.TakeSnapshot(this);
							this._cache.FixupReferencesByForeignKeys(this, false);
							this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Add, this.Entity);
							return;
						case EntityState.Detached | EntityState.Unchanged:
							break;
						case EntityState.Added:
							this.TransitionRelationshipsForAdd();
							base.ObjectStateManager.ReplaceKeyWithTemporaryKey(this);
							this._modifiedFields = null;
							this._originalValues = null;
							this._originalComplexObjects = null;
							base.State = EntityState.Added;
							this._cache.FixupReferencesByForeignKeys(this, false);
							this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Add, this.Entity);
							return;
						default:
							if (requestedState == EntityState.Deleted)
							{
								return;
							}
							if (requestedState == EntityState.Modified)
							{
								base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Modified);
								base.State = EntityState.Modified;
								this.SetModifiedAll();
								this._cache.FixupReferencesByForeignKeys(this, false);
								this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Add, this.Entity);
								return;
							}
							break;
						}
						throw EntityUtil.InvalidEntityStateArgument("state");
					}
					if (state != EntityState.Modified)
					{
						return;
					}
					switch (requestedState)
					{
					case EntityState.Detached:
						this.Detach();
						return;
					case EntityState.Unchanged:
						this.AcceptChanges();
						return;
					case EntityState.Detached | EntityState.Unchanged:
						break;
					case EntityState.Added:
						base.ObjectStateManager.ReplaceKeyWithTemporaryKey(this);
						this._modifiedFields = null;
						this._originalValues = null;
						this._originalComplexObjects = null;
						base.State = EntityState.Added;
						this.TransitionRelationshipsForAdd();
						return;
					default:
						if (requestedState == EntityState.Deleted)
						{
							this.Delete(true);
							return;
						}
						if (requestedState == EntityState.Modified)
						{
							this.SetModified();
							this.SetModifiedAll();
							return;
						}
						break;
					}
					throw EntityUtil.InvalidEntityStateArgument("state");
				}
				return;
			}
			if (requestedState == EntityState.Unchanged)
			{
				return;
			}
			throw EntityUtil.CannotModifyKeyEntryState();
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00057684 File Offset: 0x00055884
		internal void UpdateOriginalValues(object entity)
		{
			EntityState state = base.State;
			this.UpdateRecordWithSetModified(entity, this.EditableOriginalValues);
			if (state == EntityState.Unchanged && base.State == EntityState.Modified)
			{
				base.ObjectStateManager.ChangeState(this, state, EntityState.Modified);
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000576C2 File Offset: 0x000558C2
		internal void UpdateRecordWithoutSetModified(object value, DbUpdatableDataRecord current)
		{
			this.UpdateRecord(value, current, EntityEntry.UpdateRecordBehavior.WithoutSetModified, -1);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000576CE File Offset: 0x000558CE
		internal void UpdateRecordWithSetModified(object value, DbUpdatableDataRecord current)
		{
			this.UpdateRecord(value, current, EntityEntry.UpdateRecordBehavior.WithSetModified, -1);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000576DC File Offset: 0x000558DC
		private void UpdateRecord(object value, DbUpdatableDataRecord current, EntityEntry.UpdateRecordBehavior behavior, int propertyIndex)
		{
			StateManagerTypeMetadata metadata = current._metadata;
			DataRecordInfo dataRecordInfo = metadata.DataRecordInfo;
			foreach (FieldMetadata fieldMetadata in dataRecordInfo.FieldMetadata)
			{
				int ordinal = fieldMetadata.Ordinal;
				StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
				object obj = stateManagerMemberMetadata.GetValue(value) ?? DBNull.Value;
				if (Helper.IsComplexType(fieldMetadata.FieldType.TypeUsage.EdmType))
				{
					object value2 = current.GetValue(ordinal);
					if (value2 == DBNull.Value)
					{
						throw EntityUtil.NullableComplexTypesNotSupported(fieldMetadata.FieldType.Name);
					}
					if (obj != DBNull.Value)
					{
						this.UpdateRecord(obj, (DbUpdatableDataRecord)value2, behavior, (propertyIndex == -1) ? ordinal : propertyIndex);
					}
				}
				else if (this.HasRecordValueChanged(current, ordinal, obj) && !stateManagerMemberMetadata.IsPartOfKey)
				{
					current.SetValue(ordinal, obj);
					if (behavior == EntityEntry.UpdateRecordBehavior.WithSetModified)
					{
						this.SetModifiedPropertyInternal((propertyIndex == -1) ? ordinal : propertyIndex);
					}
				}
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000577F4 File Offset: 0x000559F4
		internal bool HasRecordValueChanged(DbDataRecord record, int propertyIndex, object newFieldValue)
		{
			object value = record.GetValue(propertyIndex);
			return (value != newFieldValue && (DBNull.Value == newFieldValue || DBNull.Value == value || !ByValueEqualityComparer.Default.Equals(value, newFieldValue))) || (this._cache.EntryHasConceptualNull(this) && this._modifiedFields != null && this._modifiedFields[propertyIndex]);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00057854 File Offset: 0x00055A54
		internal void ApplyCurrentValuesInternal(IEntityWrapper wrappedCurrentEntity)
		{
			if (base.State != EntityState.Modified && base.State != EntityState.Unchanged)
			{
				throw EntityUtil.EntityMustBeUnchangedOrModified(base.State);
			}
			if (this.WrappedEntity.IdentityType != wrappedCurrentEntity.IdentityType)
			{
				throw EntityUtil.EntitiesHaveDifferentType(this.Entity.GetType().FullName, wrappedCurrentEntity.Entity.GetType().FullName);
			}
			this.CompareKeyProperties(wrappedCurrentEntity.Entity);
			this.UpdateCurrentValueRecord(wrappedCurrentEntity.Entity);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000578D6 File Offset: 0x00055AD6
		internal void UpdateCurrentValueRecord(object value)
		{
			this._wrappedEntity.UpdateCurrentValueRecord(value, this);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000578E8 File Offset: 0x00055AE8
		internal void ApplyOriginalValuesInternal(IEntityWrapper wrappedOriginalEntity)
		{
			if (base.State != EntityState.Modified && base.State != EntityState.Unchanged && base.State != EntityState.Deleted)
			{
				throw EntityUtil.EntityMustBeUnchangedOrModifiedOrDeleted(base.State);
			}
			if (this.WrappedEntity.IdentityType != wrappedOriginalEntity.IdentityType)
			{
				throw EntityUtil.EntitiesHaveDifferentType(this.Entity.GetType().FullName, wrappedOriginalEntity.Entity.GetType().FullName);
			}
			this.CompareKeyProperties(wrappedOriginalEntity.Entity);
			this.UpdateOriginalValues(wrappedOriginalEntity.Entity);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00057974 File Offset: 0x00055B74
		internal void RemoveFromForeignKeyIndex()
		{
			if (!this.IsKeyEntry)
			{
				foreach (EntityReference entityReference in this.FindFKRelatedEnds())
				{
					foreach (EntityKey foreignKey in entityReference.GetAllKeyValues())
					{
						this._cache.RemoveEntryFromForeignKeyIndex(foreignKey, this);
					}
				}
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00057A04 File Offset: 0x00055C04
		internal void FixupReferencesByForeignKeys(bool replaceAddedRefs)
		{
			this._cache.TransactionManager.BeginGraphUpdate();
			bool setIsLoaded = !this._cache.TransactionManager.IsAttachTracking && !this._cache.TransactionManager.IsAddTracking;
			try
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
				{
					EntityReference entityReference = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
					if (!ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
					{
						this.FixupEntityReferenceToPrincipal(entityReference, null, setIsLoaded, replaceAddedRefs);
					}
				}
			}
			finally
			{
				this._cache.TransactionManager.EndGraphUpdate();
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00057AF0 File Offset: 0x00055CF0
		internal void FixupEntityReferenceByForeignKey(EntityReference reference)
		{
			reference.SetIsLoaded(false);
			bool flag = ForeignKeyFactory.IsConceptualNullKey(reference.CachedForeignKey);
			if (flag)
			{
				base.ObjectStateManager.ForgetEntryWithConceptualNull(this, false);
			}
			IEntityWrapper referenceValue = reference.ReferenceValue;
			EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, reference);
			bool flag2;
			if (entityKey == null || referenceValue.Entity == null)
			{
				flag2 = true;
			}
			else
			{
				EntityKey entityKey2 = referenceValue.EntityKey;
				EntityEntry objectStateEntry = referenceValue.ObjectStateEntry;
				if ((entityKey2 == null || entityKey2.IsTemporary) && objectStateEntry != null)
				{
					entityKey2 = new EntityKey((EntitySet)objectStateEntry.EntitySet, objectStateEntry.CurrentValues);
				}
				flag2 = !entityKey.Equals(entityKey2);
			}
			if (this._cache.TransactionManager.RelationshipBeingUpdated != reference)
			{
				if (!flag2)
				{
					return;
				}
				ObjectStateManager cache = this._cache;
				this._cache.TransactionManager.BeginGraphUpdate();
				if (entityKey != null)
				{
					this._cache.TransactionManager.EntityBeingReparented = this.Entity;
				}
				try
				{
					this.FixupEntityReferenceToPrincipal(reference, entityKey, false, true);
					return;
				}
				finally
				{
					this._cache.TransactionManager.EntityBeingReparented = null;
					this._cache.TransactionManager.EndGraphUpdate();
				}
			}
			this.FixupEntityReferenceToPrincipal(reference, entityKey, false, false);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00057C1C File Offset: 0x00055E1C
		internal void FixupEntityReferenceToPrincipal(EntityReference relatedEnd, EntityKey foreignKey, bool setIsLoaded, bool replaceExistingRef)
		{
			if (foreignKey == null)
			{
				foreignKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, relatedEnd);
			}
			bool flag = this._cache.TransactionManager.RelationshipBeingUpdated != relatedEnd && (!this._cache.TransactionManager.IsForeignKeyUpdate || relatedEnd.ReferenceValue.ObjectStateEntry == null || relatedEnd.ReferenceValue.ObjectStateEntry.State != EntityState.Added);
			relatedEnd.SetCachedForeignKey(foreignKey, this);
			base.ObjectStateManager.ForgetEntryWithConceptualNull(this, false);
			if (foreignKey != null)
			{
				EntityEntry entityEntry;
				if (this._cache.TryGetEntityEntry(foreignKey, out entityEntry) && !entityEntry.IsKeyEntry && entityEntry.State != EntityState.Deleted && (replaceExistingRef || this.WillNotRefSteal(relatedEnd, entityEntry.WrappedEntity)) && relatedEnd.CanSetEntityType(entityEntry.WrappedEntity))
				{
					if (flag)
					{
						if (this._cache.TransactionManager.PopulatedEntityReferences != null)
						{
							this._cache.TransactionManager.PopulatedEntityReferences.Add(relatedEnd);
						}
						relatedEnd.SetEntityKey(foreignKey, true);
						if (this._cache.TransactionManager.PopulatedEntityReferences != null)
						{
							EntityReference entityReference = relatedEnd.GetOtherEndOfRelationship(entityEntry.WrappedEntity) as EntityReference;
							if (entityReference != null)
							{
								this._cache.TransactionManager.PopulatedEntityReferences.Add(entityReference);
							}
						}
					}
					if (setIsLoaded && entityEntry.State != EntityState.Added)
					{
						relatedEnd.SetIsLoaded(true);
						return;
					}
				}
				else
				{
					this._cache.AddEntryContainingForeignKeyToIndex(foreignKey, this);
					if (flag && replaceExistingRef && relatedEnd.ReferenceValue.Entity != null)
					{
						relatedEnd.ReferenceValue = EntityWrapperFactory.NullWrapper;
						return;
					}
				}
			}
			else if (flag)
			{
				if (replaceExistingRef && (relatedEnd.ReferenceValue.Entity != null || relatedEnd.EntityKey != null))
				{
					relatedEnd.ReferenceValue = EntityWrapperFactory.NullWrapper;
				}
				if (setIsLoaded)
				{
					relatedEnd.SetIsLoaded(true);
				}
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00057DE8 File Offset: 0x00055FE8
		private bool WillNotRefSteal(EntityReference refToPrincipal, IEntityWrapper wrappedPrincipal)
		{
			RelatedEnd otherEndOfRelationship = refToPrincipal.GetOtherEndOfRelationship(wrappedPrincipal);
			EntityReference entityReference = otherEndOfRelationship as EntityReference;
			if (refToPrincipal.ReferenceValue.Entity == null && refToPrincipal.NavigationPropertyIsNullOrMissing() && (entityReference == null || (entityReference.ReferenceValue.Entity == null && entityReference.NavigationPropertyIsNullOrMissing())))
			{
				return true;
			}
			if (entityReference != null && (entityReference.ReferenceValue.Entity == refToPrincipal.WrappedOwner.Entity || entityReference.CheckIfNavigationPropertyContainsEntity(refToPrincipal.WrappedOwner)))
			{
				return true;
			}
			if (entityReference == null || refToPrincipal.ReferenceValue.Entity == wrappedPrincipal.Entity || refToPrincipal.CheckIfNavigationPropertyContainsEntity(wrappedPrincipal))
			{
				return false;
			}
			throw EntityUtil.CannotAddMoreThanOneEntityToEntityReference(entityReference.RelationshipNavigation.To, entityReference.RelationshipNavigation.RelationshipName);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00057E9C File Offset: 0x0005609C
		internal bool TryGetReferenceKey(AssociationEndMember principalRole, out EntityKey principalKey)
		{
			EntityReference entityReference = ((RelatedEnd)this.RelationshipManager.GetRelatedEnd(principalRole.DeclaringType.FullName, principalRole.Name)) as EntityReference;
			if (entityReference.CachedValue.Entity == null || entityReference.CachedValue.ObjectStateEntry == null)
			{
				principalKey = null;
				return false;
			}
			principalKey = (entityReference.EntityKey ?? entityReference.CachedValue.ObjectStateEntry.EntityKey);
			return principalKey != null;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00057F14 File Offset: 0x00056114
		internal void FixupForeignKeysByReference()
		{
			this._cache.TransactionManager.BeginFixupKeysByReference();
			try
			{
				this.FixupForeignKeysByReference(null);
			}
			finally
			{
				this._cache.TransactionManager.EndFixupKeysByReference();
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00057F5C File Offset: 0x0005615C
		private void FixupForeignKeysByReference(List<EntityEntry> visited)
		{
			EntitySet entitySet = base.EntitySet as EntitySet;
			if (!entitySet.HasForeignKeyRelationships)
			{
				return;
			}
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				IEntityWrapper referenceValue = entityReference.ReferenceValue;
				if (referenceValue.Entity != null)
				{
					EntityEntry objectStateEntry = referenceValue.ObjectStateEntry;
					bool? flag = null;
					if (objectStateEntry != null && objectStateEntry.State == EntityState.Added)
					{
						if (objectStateEntry == this)
						{
							flag = new bool?(entityReference.GetOtherEndOfRelationship(referenceValue) is EntityReference);
							bool? flag2 = flag;
							if (!flag2.Value)
							{
								goto IL_11B;
							}
						}
						visited = (visited ?? new List<EntityEntry>());
						if (visited.Contains(this))
						{
							if (flag == null)
							{
								flag = new bool?(entityReference.GetOtherEndOfRelationship(referenceValue) is EntityReference);
							}
							if (flag.Value)
							{
								throw EntityUtil.CircularRelationshipsWithReferentialConstraints();
							}
						}
						else
						{
							visited.Add(this);
							objectStateEntry.FixupForeignKeysByReference(visited);
							visited.Remove(this);
						}
					}
					IL_11B:
					entityReference.UpdateForeignKeyValues(this.WrappedEntity, referenceValue, null, false);
				}
				else
				{
					EntityKey entityKey = entityReference.EntityKey;
					if (entityKey != null && !entityKey.IsTemporary)
					{
						entityReference.UpdateForeignKeyValues(this.WrappedEntity, entityKey);
					}
				}
			}
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple2 in this.ForeignKeyPrincipals)
			{
				bool flag3 = false;
				bool flag4 = false;
				EntityKey entityKey2 = this.WrappedEntity.EntityKey;
				RelatedEnd relatedEndInternal = this.RelationshipManager.GetRelatedEndInternal(tuple2.Item1.ElementType.FullName, tuple2.Item2.ToRole.Name);
				foreach (IEntityWrapper entityWrapper in relatedEndInternal.GetWrappedEntities())
				{
					EntityEntry objectStateEntry2 = entityWrapper.ObjectStateEntry;
					if (objectStateEntry2.State != EntityState.Added && !flag4)
					{
						flag4 = true;
						foreach (EdmProperty edmProperty in tuple2.Item2.ToProperties)
						{
							int ordinalforOLayerMemberName = objectStateEntry2._cacheTypeMetadata.GetOrdinalforOLayerMemberName(edmProperty.Name);
							StateManagerMemberMetadata stateManagerMemberMetadata = objectStateEntry2._cacheTypeMetadata.Member(ordinalforOLayerMemberName);
							if (stateManagerMemberMetadata.IsPartOfKey)
							{
								flag3 = true;
								break;
							}
						}
					}
					if (objectStateEntry2.State == EntityState.Added || (objectStateEntry2.State == EntityState.Modified && !flag3))
					{
						EntityReference entityReference2 = relatedEndInternal.GetOtherEndOfRelationship(entityWrapper) as EntityReference;
						entityReference2.UpdateForeignKeyValues(entityWrapper, this.WrappedEntity, null, false);
					}
				}
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000582A8 File Offset: 0x000564A8
		private bool IsPropertyAForeignKey(string propertyName)
		{
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				foreach (EdmProperty edmProperty in tuple.Item2.ToProperties)
				{
					if (edmProperty.Name == propertyName)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00058348 File Offset: 0x00056548
		private bool IsPropertyAForeignKey(string propertyName, out List<Pair<string, string>> relationships)
		{
			relationships = null;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				foreach (EdmProperty edmProperty in tuple.Item2.ToProperties)
				{
					if (edmProperty.Name == propertyName)
					{
						if (relationships == null)
						{
							relationships = new List<Pair<string, string>>();
						}
						relationships.Add(new Pair<string, string>(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name));
						break;
					}
				}
			}
			return relationships != null;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00058424 File Offset: 0x00056624
		internal void FindRelatedEntityKeysByForeignKeys(out Dictionary<RelatedEnd, HashSet<EntityKey>> relatedEntities, bool useOriginalValues)
		{
			relatedEntities = null;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				AssociationSet item = tuple.Item1;
				ReferentialConstraint item2 = tuple.Item2;
				string identity = item2.ToRole.Identity;
				ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = item.AssociationSetEnds;
				AssociationEndMember correspondingAssociationEndMember2;
				if (associationSetEnds[0].CorrespondingAssociationEndMember.Identity == identity)
				{
					AssociationEndMember correspondingAssociationEndMember = associationSetEnds[0].CorrespondingAssociationEndMember;
					correspondingAssociationEndMember2 = associationSetEnds[1].CorrespondingAssociationEndMember;
				}
				else
				{
					AssociationEndMember correspondingAssociationEndMember = associationSetEnds[1].CorrespondingAssociationEndMember;
					correspondingAssociationEndMember2 = associationSetEnds[0].CorrespondingAssociationEndMember;
				}
				EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(item, correspondingAssociationEndMember2);
				EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, item2, entitySetAtEnd, useOriginalValues);
				if (entityKey != null)
				{
					EntityReference key = this.RelationshipManager.GetRelatedEndInternal(item.ElementType.FullName, item2.FromRole.Name) as EntityReference;
					relatedEntities = ((relatedEntities != null) ? relatedEntities : new Dictionary<RelatedEnd, HashSet<EntityKey>>());
					HashSet<EntityKey> hashSet;
					if (!relatedEntities.TryGetValue(key, out hashSet))
					{
						hashSet = new HashSet<EntityKey>();
						relatedEntities.Add(key, hashSet);
					}
					hashSet.Add(entityKey);
				}
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0005857C File Offset: 0x0005677C
		internal IEnumerable<EntityReference> FindFKRelatedEnds()
		{
			HashSet<EntityReference> hashSet = new HashSet<EntityReference>();
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference item = this.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				hashSet.Add(item);
			}
			return hashSet;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00058604 File Offset: 0x00056804
		internal void DetectChangesInForeignKeys()
		{
			TransactionManager transactionManager = base.ObjectStateManager.TransactionManager;
			foreach (EntityReference entityReference in this.FindFKRelatedEnds())
			{
				EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, entityReference);
				EntityKey cachedForeignKey = entityReference.CachedForeignKey;
				bool flag = ForeignKeyFactory.IsConceptualNullKey(cachedForeignKey);
				if (cachedForeignKey != null || entityKey != null)
				{
					if (cachedForeignKey == null)
					{
						EntityEntry relatedEntry;
						base.ObjectStateManager.TryGetEntityEntry(entityKey, out relatedEntry);
						this.AddRelationshipDetectedByForeignKey(transactionManager.AddedRelationshipsByForeignKey, transactionManager.AddedRelationshipsByPrincipalKey, entityKey, relatedEntry, entityReference);
					}
					else if (entityKey == null)
					{
						this.AddDetectedRelationship<EntityKey>(transactionManager.DeletedRelationshipsByForeignKey, cachedForeignKey, entityReference);
					}
					else if (!entityKey.Equals(cachedForeignKey) && (!flag || ForeignKeyFactory.IsConceptualNullKeyChanged(cachedForeignKey, entityKey)))
					{
						EntityEntry relatedEntry2;
						base.ObjectStateManager.TryGetEntityEntry(entityKey, out relatedEntry2);
						this.AddRelationshipDetectedByForeignKey(transactionManager.AddedRelationshipsByForeignKey, transactionManager.AddedRelationshipsByPrincipalKey, entityKey, relatedEntry2, entityReference);
						if (!flag)
						{
							this.AddDetectedRelationship<EntityKey>(transactionManager.DeletedRelationshipsByForeignKey, cachedForeignKey, entityReference);
						}
					}
				}
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x00058728 File Offset: 0x00056928
		internal bool RequiresComplexChangeTracking
		{
			get
			{
				return this._requiresComplexChangeTracking;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00058730 File Offset: 0x00056930
		internal bool RequiresScalarChangeTracking
		{
			get
			{
				return this._requiresScalarChangeTracking;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00058738 File Offset: 0x00056938
		internal bool RequiresAnyChangeTracking
		{
			get
			{
				return this._requiresAnyChangeTracking;
			}
		}

		// Token: 0x04000AD3 RID: 2771
		private StateManagerTypeMetadata _cacheTypeMetadata;

		// Token: 0x04000AD4 RID: 2772
		private EntityKey _entityKey;

		// Token: 0x04000AD5 RID: 2773
		private IEntityWrapper _wrappedEntity;

		// Token: 0x04000AD6 RID: 2774
		private BitArray _modifiedFields;

		// Token: 0x04000AD7 RID: 2775
		private List<StateManagerValue> _originalValues;

		// Token: 0x04000AD8 RID: 2776
		private Dictionary<object, Dictionary<int, object>> _originalComplexObjects;

		// Token: 0x04000AD9 RID: 2777
		private bool _requiresComplexChangeTracking;

		// Token: 0x04000ADA RID: 2778
		private bool _requiresScalarChangeTracking;

		// Token: 0x04000ADB RID: 2779
		private bool _requiresAnyChangeTracking;

		// Token: 0x04000ADC RID: 2780
		private RelationshipEntry _headRelationshipEnds;

		// Token: 0x04000ADD RID: 2781
		private int _countRelationshipEnds;

		// Token: 0x04000ADE RID: 2782
		internal const int s_EntityRoot = -1;

		// Token: 0x020004A8 RID: 1192
		internal struct RelationshipEndEnumerable : IEnumerable<RelationshipEntry>, IEnumerable, IEnumerable<IEntityStateEntry>
		{
			// Token: 0x06003C42 RID: 15426 RVA: 0x000E2B48 File Offset: 0x000E0D48
			internal RelationshipEndEnumerable(EntityEntry entityEntry)
			{
				this._entityEntry = entityEntry;
			}

			// Token: 0x06003C43 RID: 15427 RVA: 0x000E2B51 File Offset: 0x000E0D51
			public EntityEntry.RelationshipEndEnumerator GetEnumerator()
			{
				return new EntityEntry.RelationshipEndEnumerator(this._entityEntry);
			}

			// Token: 0x06003C44 RID: 15428 RVA: 0x000E2B5E File Offset: 0x000E0D5E
			IEnumerator<IEntityStateEntry> IEnumerable<IEntityStateEntry>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003C45 RID: 15429 RVA: 0x000E2B5E File Offset: 0x000E0D5E
			IEnumerator<RelationshipEntry> IEnumerable<RelationshipEntry>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003C46 RID: 15430 RVA: 0x000E2B5E File Offset: 0x000E0D5E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003C47 RID: 15431 RVA: 0x000E2B6C File Offset: 0x000E0D6C
			internal RelationshipEntry[] ToArray()
			{
				RelationshipEntry[] array = null;
				if (this._entityEntry != null && 0 < this._entityEntry._countRelationshipEnds)
				{
					RelationshipEntry relationshipEntry = this._entityEntry._headRelationshipEnds;
					array = new RelationshipEntry[this._entityEntry._countRelationshipEnds];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = relationshipEntry;
						relationshipEntry = relationshipEntry.GetNextRelationshipEnd(this._entityEntry.EntityKey);
					}
				}
				return array ?? EntityEntry.RelationshipEndEnumerable.EmptyRelationshipEntryArray;
			}

			// Token: 0x04001A44 RID: 6724
			internal static readonly RelationshipEntry[] EmptyRelationshipEntryArray = new RelationshipEntry[0];

			// Token: 0x04001A45 RID: 6725
			private readonly EntityEntry _entityEntry;
		}

		// Token: 0x020004A9 RID: 1193
		internal struct RelationshipEndEnumerator : IEnumerator<RelationshipEntry>, IDisposable, IEnumerator, IEnumerator<IEntityStateEntry>
		{
			// Token: 0x06003C49 RID: 15433 RVA: 0x000E2BE9 File Offset: 0x000E0DE9
			internal RelationshipEndEnumerator(EntityEntry entityEntry)
			{
				this._entityEntry = entityEntry;
				this._current = null;
			}

			// Token: 0x17000AE7 RID: 2791
			// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000E2BF9 File Offset: 0x000E0DF9
			public RelationshipEntry Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000AE8 RID: 2792
			// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000E2BF9 File Offset: 0x000E0DF9
			IEntityStateEntry IEnumerator<IEntityStateEntry>.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000AE9 RID: 2793
			// (get) Token: 0x06003C4C RID: 15436 RVA: 0x000E2BF9 File Offset: 0x000E0DF9
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003C4D RID: 15437 RVA: 0x000089D0 File Offset: 0x00006BD0
			public void Dispose()
			{
			}

			// Token: 0x06003C4E RID: 15438 RVA: 0x000E2C04 File Offset: 0x000E0E04
			public bool MoveNext()
			{
				if (this._entityEntry != null)
				{
					if (this._current == null)
					{
						this._current = this._entityEntry._headRelationshipEnds;
					}
					else
					{
						this._current = this._current.GetNextRelationshipEnd(this._entityEntry.EntityKey);
					}
				}
				return this._current != null;
			}

			// Token: 0x06003C4F RID: 15439 RVA: 0x000089D0 File Offset: 0x00006BD0
			public void Reset()
			{
			}

			// Token: 0x04001A46 RID: 6726
			private readonly EntityEntry _entityEntry;

			// Token: 0x04001A47 RID: 6727
			private RelationshipEntry _current;
		}

		// Token: 0x020004AA RID: 1194
		private enum UpdateRecordBehavior
		{
			// Token: 0x04001A49 RID: 6729
			WithoutSetModified,
			// Token: 0x04001A4A RID: 6730
			WithSetModified
		}
	}
}
