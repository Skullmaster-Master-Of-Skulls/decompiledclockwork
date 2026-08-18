using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200056B RID: 1387
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal sealed class EntityEntry : ObjectStateEntry
	{
		// Token: 0x060035B0 RID: 13744 RVA: 0x000FE3AB File Offset: 0x000FC5AB
		internal EntityEntry() : base(new ObjectStateManager(), null, EntityState.Unchanged)
		{
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000FE3BA File Offset: 0x000FC5BA
		internal EntityEntry(ObjectStateManager stateManager) : base(stateManager, null, EntityState.Unchanged)
		{
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000FE3C5 File Offset: 0x000FC5C5
		internal EntityEntry(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySet, ObjectStateManager cache, StateManagerTypeMetadata typeMetadata, EntityState state) : base(cache, entitySet, state)
		{
			this._wrappedEntity = wrappedEntity;
			this._cacheTypeMetadata = typeMetadata;
			this._entityKey = entityKey;
			wrappedEntity.ObjectStateEntry = this;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000FE400 File Offset: 0x000FC600
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

		// Token: 0x060035B4 RID: 13748 RVA: 0x000FE4C7 File Offset: 0x000FC6C7
		internal EntityEntry(EntityKey entityKey, EntitySet entitySet, ObjectStateManager cache, StateManagerTypeMetadata typeMetadata) : base(cache, entitySet, EntityState.Unchanged)
		{
			this._wrappedEntity = NullEntityWrapper.NullWrapper;
			this._entityKey = entityKey;
			this._cacheTypeMetadata = typeMetadata;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000FE4F2 File Offset: 0x000FC6F2
		public override bool IsRelationship
		{
			get
			{
				base.ValidateState();
				return false;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000FE4FB File Offset: 0x000FC6FB
		public override object Entity
		{
			get
			{
				base.ValidateState();
				return this._wrappedEntity.Entity;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000FE50E File Offset: 0x000FC70E
		// (set) Token: 0x060035B8 RID: 13752 RVA: 0x000FE51C File Offset: 0x000FC71C
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

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000FE718 File Offset: 0x000FC918
		internal IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyDependents
		{
			get
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> foreignKey in ((EntitySet)base.EntitySet).ForeignKeyDependents)
				{
					ReferentialConstraint constraint = foreignKey.Item2;
					EntityType dependentType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)constraint.ToRole);
					if (dependentType.IsAssignableFrom(this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType))
					{
						yield return foreignKey;
					}
				}
				yield break;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000FE928 File Offset: 0x000FCB28
		internal IEnumerable<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyPrincipals
		{
			get
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> foreignKey in ((EntitySet)base.EntitySet).ForeignKeyPrincipals)
				{
					ReferentialConstraint constraint = foreignKey.Item2;
					EntityType dependentType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)constraint.FromRole);
					if (dependentType.IsAssignableFrom(this._cacheTypeMetadata.DataRecordInfo.RecordType.EdmType))
					{
						yield return foreignKey;
					}
				}
				yield break;
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000FEA9C File Offset: 0x000FCC9C
		public override IEnumerable<string> GetModifiedProperties()
		{
			base.ValidateState();
			if (EntityState.Modified == base.State && this._modifiedFields != null)
			{
				for (int i = 0; i < this._modifiedFields.Length; i++)
				{
					if (this._modifiedFields[i])
					{
						yield return this.GetCLayerName(i, this._cacheTypeMetadata);
					}
				}
			}
			yield break;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000FEABC File Offset: 0x000FCCBC
		public override void SetModifiedProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			int modifiedPropertyInternal = this.ValidateAndGetOrdinalForProperty(propertyName, "SetModifiedProperty");
			if (EntityState.Unchanged == base.State)
			{
				base.State = EntityState.Modified;
				this._cache.ChangeState(this, EntityState.Unchanged, base.State);
			}
			this.SetModifiedPropertyInternal(modifiedPropertyInternal);
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000FEB0D File Offset: 0x000FCD0D
		internal void SetModifiedPropertyInternal(int ordinal)
		{
			if (this._modifiedFields == null)
			{
				this._modifiedFields = new BitArray(this.GetFieldCount(this._cacheTypeMetadata));
			}
			this._modifiedFields[ordinal] = true;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000FEB3C File Offset: 0x000FCD3C
		private int ValidateAndGetOrdinalForProperty(string propertyName, string methodName)
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
			}
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(propertyName);
			if (ordinalforOLayerMemberName == -1)
			{
				throw new ArgumentException(Strings.ObjectStateEntry_SetModifiedOnInvalidProperty(propertyName));
			}
			if (base.State == EntityState.Added || base.State == EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_SetModifiedStates(methodName));
			}
			return ordinalforOLayerMemberName;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000FEBA0 File Offset: 0x000FCDA0
		public override void RejectPropertyChanges(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
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
				for (int i = 0; i < this._modifiedFields.Length; i++)
				{
					if (this._modifiedFields[i])
					{
						return;
					}
				}
				this.ChangeObjectState(EntityState.Unchanged);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000FEC5B File Offset: 0x000FCE5B
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override DbDataRecord OriginalValues
		{
			get
			{
				return this.InternalGetOriginalValues(true);
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000FEC64 File Offset: 0x000FCE64
		public override OriginalValueRecord GetUpdatableOriginalValues()
		{
			return (OriginalValueRecord)this.InternalGetOriginalValues(false);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000FEC74 File Offset: 0x000FCE74
		private DbDataRecord InternalGetOriginalValues(bool readOnly)
		{
			base.ValidateState();
			if (base.State == EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
			}
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.DetectChangesInComplexProperties();
			if (readOnly)
			{
				return new ObjectStateEntryDbDataRecord(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
			return new ObjectStateEntryOriginalDbUpdatableDataRecord_Public(this, this._cacheTypeMetadata, this._wrappedEntity.Entity, -1);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000FECE8 File Offset: 0x000FCEE8
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

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000FED38 File Offset: 0x000FCF38
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override CurrentValueRecord CurrentValues
		{
			get
			{
				base.ValidateState();
				if (base.State == EntityState.Deleted)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_CurrentValuesDoesNotExist);
				}
				if (this.IsKeyEntry)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
				}
				return new ObjectStateEntryDbUpdatableDataRecord(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000FED89 File Offset: 0x000FCF89
		public override void Delete()
		{
			this.Delete(true);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000FED94 File Offset: 0x000FCF94
		public override void AcceptChanges()
		{
			base.ValidateState();
			if (base.ObjectStateManager.EntryHasConceptualNull(this))
			{
				throw new InvalidOperationException(Strings.ObjectContext_CommitWithConceptualNull);
			}
			EntityState state = base.State;
			switch (state)
			{
			case EntityState.Unchanged:
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
			{
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
			default:
				if (state != EntityState.Deleted)
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
				break;
			}
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000FEEA0 File Offset: 0x000FD0A0
		public override void SetModified()
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
			}
			if (EntityState.Unchanged == base.State)
			{
				base.State = EntityState.Modified;
				this._cache.ChangeState(this, EntityState.Unchanged, base.State);
				return;
			}
			if (EntityState.Modified != base.State)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_SetModifiedStates("SetModified"));
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000FEF05 File Offset: 0x000FD105
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

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000FEF43 File Offset: 0x000FD143
		internal override BitArray ModifiedProperties
		{
			get
			{
				return this._modifiedFields;
			}
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000FEF4C File Offset: 0x000FD14C
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

		// Token: 0x060035CB RID: 13771 RVA: 0x000FEFAC File Offset: 0x000FD1AC
		public override void ApplyCurrentValues(object currentEntity)
		{
			Check.NotNull<object>(currentEntity, "currentEntity");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			IEntityWrapper wrappedCurrentEntity = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(currentEntity, base.ObjectStateManager);
			this.ApplyCurrentValuesInternal(wrappedCurrentEntity);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000FF000 File Offset: 0x000FD200
		public override void ApplyOriginalValues(object originalEntity)
		{
			Check.NotNull<object>(originalEntity, "originalEntity");
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			IEntityWrapper wrappedOriginalEntity = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(originalEntity, base.ObjectStateManager);
			this.ApplyOriginalValuesInternal(wrappedOriginalEntity);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000FF051 File Offset: 0x000FD251
		internal void AddRelationshipEnd(RelationshipEntry item)
		{
			item.SetNextRelationshipEnd(this.EntityKey, this._headRelationshipEnds);
			this._headRelationshipEnds = item;
			this._countRelationshipEnds++;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000FF07C File Offset: 0x000FD27C
		internal bool ContainsRelationshipEnd(RelationshipEntry item)
		{
			for (RelationshipEntry relationshipEntry = this._headRelationshipEnds; relationshipEntry != null; relationshipEntry = relationshipEntry.GetNextRelationshipEnd(this.EntityKey))
			{
				if (object.ReferenceEquals(relationshipEntry, item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000FF0B0 File Offset: 0x000FD2B0
		internal void RemoveRelationshipEnd(RelationshipEntry item)
		{
			RelationshipEntry relationshipEntry = this._headRelationshipEnds;
			RelationshipEntry relationshipEntry2 = null;
			bool flag = false;
			while (relationshipEntry != null)
			{
				bool flag2 = object.ReferenceEquals(this.EntityKey, relationshipEntry.Key0) || (!object.ReferenceEquals(this.EntityKey, relationshipEntry.Key1) && this.EntityKey.Equals(relationshipEntry.Key0));
				if (object.ReferenceEquals(item, relationshipEntry))
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

		// Token: 0x060035D0 RID: 13776 RVA: 0x000FF184 File Offset: 0x000FD384
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

		// Token: 0x060035D1 RID: 13777 RVA: 0x000FF1DE File Offset: 0x000FD3DE
		internal EntityEntry.RelationshipEndEnumerable GetRelationshipEnds()
		{
			return new EntityEntry.RelationshipEndEnumerable(this);
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000FF1E6 File Offset: 0x000FD3E6
		internal override bool IsKeyEntry
		{
			get
			{
				return null == this._wrappedEntity.Entity;
			}
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000FF1F6 File Offset: 0x000FD3F6
		internal override DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject)
		{
			if (Helper.IsEntityType(metadata.CdmMetadata.EdmType) && this._entityKey != null)
			{
				return new EntityRecordInfo(metadata.DataRecordInfo, this._entityKey, (EntitySet)base.EntitySet);
			}
			return metadata.DataRecordInfo;
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000FF238 File Offset: 0x000FD438
		internal override void Reset()
		{
			this.RemoveFromForeignKeyIndex();
			this._cache.ForgetEntryWithConceptualNull(this, true);
			this.DetachObjectStateManagerFromEntity();
			this._wrappedEntity = NullEntityWrapper.NullWrapper;
			this._entityKey = null;
			this._modifiedFields = null;
			this._originalValues = null;
			this._originalComplexObjects = null;
			this.SetChangeTrackingFlags();
			base.Reset();
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000FF291 File Offset: 0x000FD491
		internal override Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata)
		{
			return metadata.GetFieldType(ordinal);
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000FF29A File Offset: 0x000FD49A
		internal override string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata)
		{
			return metadata.CLayerMemberName(ordinal);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000FF2A3 File Offset: 0x000FD4A3
		internal override int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata)
		{
			return metadata.GetOrdinalforCLayerMemberName(name);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000FF2AC File Offset: 0x000FD4AC
		internal override void RevertDelete()
		{
			base.State = ((this._modifiedFields == null) ? EntityState.Unchanged : EntityState.Modified);
			this._cache.ChangeState(this, EntityState.Deleted, base.State);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000FF2D4 File Offset: 0x000FD4D4
		internal override int GetFieldCount(StateManagerTypeMetadata metadata)
		{
			return metadata.FieldCount;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000FF2DC File Offset: 0x000FD4DC
		private void CascadeAcceptChanges()
		{
			foreach (RelationshipEntry relationshipEntry in this._cache.CopyOfRelationshipsByKey(this.EntityKey))
			{
				relationshipEntry.AcceptChanges();
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000FF313 File Offset: 0x000FD513
		internal override void SetModifiedAll()
		{
			base.ValidateState();
			if (this._modifiedFields == null)
			{
				this._modifiedFields = new BitArray(this.GetFieldCount(this._cacheTypeMetadata));
			}
			this._modifiedFields.SetAll(true);
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000FF346 File Offset: 0x000FD546
		internal override void EntityMemberChanging(string entityMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanging(entityMemberName, null, null);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000FF364 File Offset: 0x000FD564
		internal override void EntityMemberChanged(string entityMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanged(entityMemberName, null, null);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000FF382 File Offset: 0x000FD582
		internal override void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanging(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000FF3A0 File Offset: 0x000FD5A0
		internal override void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
			}
			this.EntityMemberChanged(entityMemberName, complexObject, complexObjectMemberName);
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000FF3BE File Offset: 0x000FD5BE
		internal IEntityWrapper WrappedEntity
		{
			get
			{
				return this._wrappedEntity;
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000FF3C8 File Offset: 0x000FD5C8
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
						throw new InvalidOperationException(Strings.ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging);
					}
					if (base.State != this._cache.ChangingState)
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_ChangedInDifferentStateFromChanging(this._cache.ChangingState, base.State));
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
							this.AddOriginalValueAt(-1, stateManagerMemberMetadata, obj, changingOldValue);
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

		// Token: 0x060035E2 RID: 13794 RVA: 0x000FF650 File Offset: 0x000FD850
		internal void SetCurrentEntityValue(string memberName, object newValue)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			this.SetCurrentEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, newValue);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000FF684 File Offset: 0x000FD884
		internal void SetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, object newValue)
		{
			base.ValidateState();
			if (base.State == EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
			}
			EntityState state = base.State;
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, userObject);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (num >= 0)
				{
					this._originalValues.RemoveAt(num);
				}
				object value = stateManagerMemberMetadata.GetValue(userObject);
				if (value == null)
				{
					throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(stateManagerMemberMetadata.CLayerName));
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
				this.AddOriginalValueAt(num, stateManagerMemberMetadata, userObject, newValue);
			}
			if (state == EntityState.Unchanged)
			{
				base.State = EntityState.Modified;
			}
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000FF744 File Offset: 0x000FD944
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
			this._cache.SaveOriginalValues = ((base.State == EntityState.Unchanged || base.State == EntityState.Modified) && this.FindOriginalValueIndex(stateManagerMemberMetadata, obj) == -1);
			object value = stateManagerMemberMetadata.GetValue(obj);
			this.SetCachedChangingValues(entityMemberName, obj, changingMember, base.State, value);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000FF7B8 File Offset: 0x000FD9B8
		internal object GetOriginalEntityValue(string memberName)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			return this.GetOriginalEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, ObjectStateValueRecord.OriginalReadonly);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000FF7EB File Offset: 0x000FD9EB
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord)
		{
			return this.GetOriginalEntityValue(metadata, ordinal, userObject, updatableRecord, -1);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000FF7F9 File Offset: 0x000FD9F9
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			base.ValidateState();
			return this.GetOriginalEntityValue(metadata, metadata.Member(ordinal), ordinal, userObject, updatableRecord, parentEntityPropertyIndex);
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000FF818 File Offset: 0x000FDA18
		internal object GetOriginalEntityValue(StateManagerTypeMetadata metadata, StateManagerMemberMetadata memberMetadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			int num = this.FindOriginalValueIndex(memberMetadata, userObject);
			if (num >= 0)
			{
				return this._originalValues[num].OriginalValue ?? DBNull.Value;
			}
			return this.GetCurrentEntityValue(metadata, ordinal, userObject, updatableRecord, parentEntityPropertyIndex);
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000FF85C File Offset: 0x000FDA5C
		internal object GetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord)
		{
			return this.GetCurrentEntityValue(metadata, ordinal, userObject, updatableRecord, -1);
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000FF86C File Offset: 0x000FDA6C
		internal object GetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, ObjectStateValueRecord updatableRecord, int parentEntityPropertyIndex)
		{
			base.ValidateState();
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			object obj = stateManagerMemberMetadata.GetValue(userObject);
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
			return obj ?? DBNull.Value;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000FF960 File Offset: 0x000FDB60
		internal int FindOriginalValueIndex(StateManagerMemberMetadata metadata, object instance)
		{
			if (this._originalValues != null)
			{
				for (int i = 0; i < this._originalValues.Count; i++)
				{
					if (object.ReferenceEquals(this._originalValues[i].UserObject, instance) && object.ReferenceEquals(this._originalValues[i].MemberMetadata, metadata))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000FF9C0 File Offset: 0x000FDBC0
		internal AssociationEndMember GetAssociationEndMember(RelationshipEntry relationshipEntry)
		{
			base.ValidateState();
			return relationshipEntry.RelationshipWrapper.GetAssociationEndMember(this.EntityKey);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000FF9E6 File Offset: 0x000FDBE6
		internal EntityEntry GetOtherEndOfRelationship(RelationshipEntry relationshipEntry)
		{
			return this._cache.GetEntityEntry(relationshipEntry.RelationshipWrapper.GetOtherEntityKey(this.EntityKey));
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000FFA04 File Offset: 0x000FDC04
		internal void ExpandComplexTypeAndAddValues(StateManagerMemberMetadata memberMetadata, object oldComplexObject, object newComplexObject, bool useOldComplexObject)
		{
			if (newComplexObject == null)
			{
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(memberMetadata.CLayerName));
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
						if (obj == null)
						{
							int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, oldComplexObject);
							if (num >= 0)
							{
								this._originalValues.RemoveAt(num);
							}
						}
					}
					this.ExpandComplexTypeAndAddValues(stateManagerMemberMetadata, obj, stateManagerMemberMetadata.GetValue(newComplexObject), useOldComplexObject);
				}
				else
				{
					object userObject = newComplexObject;
					int num2 = -1;
					object value;
					if (useOldComplexObject)
					{
						value = stateManagerMemberMetadata.GetValue(newComplexObject);
						userObject = oldComplexObject;
					}
					else if (oldComplexObject != null)
					{
						value = stateManagerMemberMetadata.GetValue(oldComplexObject);
						num2 = this.FindOriginalValueIndex(stateManagerMemberMetadata, oldComplexObject);
						if (num2 >= 0)
						{
							value = this._originalValues[num2].OriginalValue;
						}
					}
					else
					{
						value = stateManagerMemberMetadata.GetValue(newComplexObject);
					}
					this.AddOriginalValueAt(num2, stateManagerMemberMetadata, userObject, value);
				}
			}
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000FFB08 File Offset: 0x000FDD08
		internal int GetAndValidateChangeMemberInfo(string entityMemberName, object complexObject, string complexObjectMemberName, out StateManagerTypeMetadata typeMetadata, out string changingMemberName, out object changingObject)
		{
			Check.NotNull<string>(entityMemberName, "entityMemberName");
			typeMetadata = null;
			changingMemberName = null;
			changingObject = null;
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
						throw new ArgumentException(Strings.ComplexObject_ComplexChangeRequestedOnScalarProperty(entityMemberName));
					}
					stateManagerTypeMetadata = this._cache.GetOrAddStateManagerTypeMetadata(complexObject.GetType(), (EntitySet)base.EntitySet);
					ordinalforOLayerMemberName = stateManagerTypeMetadata.GetOrdinalforOLayerMemberName(complexObjectMemberName);
					if (ordinalforOLayerMemberName == -1)
					{
						throw new ArgumentException(Strings.ObjectStateEntry_ChangeOnUnmappedComplexProperty(complexObjectMemberName));
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
			if (!(entityMemberName == "-EntityKey-"))
			{
				throw new ArgumentException(Strings.ObjectStateEntry_ChangeOnUnmappedProperty(entityMemberName));
			}
			if (!this._cache.InRelationshipFixup)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CantSetEntityKey);
			}
			this.SetCachedChangingValues(null, null, null, base.State, null);
			return -2;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000FFC54 File Offset: 0x000FDE54
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

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000FFCB0 File Offset: 0x000FDEB0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal OriginalValueRecord EditableOriginalValues
		{
			get
			{
				return new ObjectStateEntryOriginalDbUpdatableDataRecord_Internal(this, this._cacheTypeMetadata, this._wrappedEntity.Entity);
			}
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000FFCCC File Offset: 0x000FDECC
		internal void DetachObjectStateManagerFromEntity()
		{
			if (!this.IsKeyEntry)
			{
				this._wrappedEntity.SetChangeTracker(null);
				this._wrappedEntity.DetachContext();
				if (!this._cache.TransactionManager.IsAttachTracking || this._cache.TransactionManager.OriginalMergeOption != MergeOption.NoTracking)
				{
					this._wrappedEntity.EntityKey = null;
				}
			}
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000FFD40 File Offset: 0x000FDF40
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
						this.AddOriginalValueAt(-1, stateManagerMemberMetadata, this._wrappedEntity.Entity, value);
					}
				}
			}
			this.TakeSnapshotOfForeignKeys();
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000FFDE4 File Offset: 0x000FDFE4
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

		// Token: 0x060035F5 RID: 13813 RVA: 0x000FFE64 File Offset: 0x000FE064
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
				else if (this.FindOriginalValueIndex(stateManagerMemberMetadata, complexValue) == -1)
				{
					this.AddOriginalValueAt(-1, stateManagerMemberMetadata, complexValue, value);
				}
			}
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000FFEE4 File Offset: 0x000FE0E4
		private void AddComplexObjectSnapshot(object userObject, int ordinal, object complexObject)
		{
			if (complexObject == null)
			{
				return;
			}
			this.CheckForDuplicateComplexObjects(complexObject);
			if (this._originalComplexObjects == null)
			{
				this._originalComplexObjects = new Dictionary<object, Dictionary<int, object>>(ObjectReferenceEqualityComparer.Default);
			}
			Dictionary<int, object> dictionary;
			if (!this._originalComplexObjects.TryGetValue(userObject, out dictionary))
			{
				dictionary = new Dictionary<int, object>();
				this._originalComplexObjects.Add(userObject, dictionary);
			}
			dictionary.Add(ordinal, complexObject);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000FFF40 File Offset: 0x000FE140
		private void CheckForDuplicateComplexObjects(object complexObject)
		{
			if (this._originalComplexObjects == null || complexObject == null)
			{
				return;
			}
			foreach (Dictionary<int, object> dictionary in this._originalComplexObjects.Values)
			{
				foreach (object objB in dictionary.Values)
				{
					if (object.ReferenceEquals(complexObject, objB))
					{
						throw new InvalidOperationException(Strings.ObjectStateEntry_ComplexObjectUsedMultipleTimes(this.Entity.GetType().FullName, complexObject.GetType().FullName));
					}
				}
			}
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x00100008 File Offset: 0x000FE208
		public override bool IsPropertyChanged(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return this.DetectChangesInProperty(this.ValidateAndGetOrdinalForProperty(propertyName, "IsPropertyChanged"), false, true);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x0010002C File Offset: 0x000FE22C
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "originalValueFound", Justification = "Used in the debug build")]
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
				int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, this._wrappedEntity.Entity);
				if (num < 0)
				{
					return this.GetModifiedProperties().Contains(stateManagerMemberMetadata.CLayerName);
				}
				object originalValue = this._originalValues[num].OriginalValue;
				if (!object.Equals(value, originalValue))
				{
					flag = true;
					if (stateManagerMemberMetadata.IsPartOfKey)
					{
						if (!ByValueEqualityComparer.Default.Equals(value, originalValue))
						{
							throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyProperty(stateManagerMemberMetadata.CLayerName));
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

		// Token: 0x060035FA RID: 13818 RVA: 0x0010018C File Offset: 0x000FE38C
		internal void DetectChangesInProperties(bool detectOnlyComplexProperties)
		{
			int fieldCount = this.GetFieldCount(this._cacheTypeMetadata);
			for (int i = 0; i < fieldCount; i++)
			{
				this.DetectChangesInProperty(i, detectOnlyComplexProperties, false);
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x001001BC File Offset: 0x000FE3BC
		private bool DetectChangesInComplexType(StateManagerMemberMetadata topLevelMember, StateManagerMemberMetadata complexMember, object complexValue, object oldComplexValue, ref bool changeDetected, bool detectOnly)
		{
			if (complexValue == null)
			{
				if (oldComplexValue == null)
				{
					return false;
				}
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(complexMember.CLayerName));
			}
			else
			{
				if (!object.ReferenceEquals(oldComplexValue, complexValue))
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
						int num = this.FindOriginalValueIndex(stateManagerMemberMetadata, complexValue);
						object objB = (num == -1) ? null : this._originalValues[num].OriginalValue;
						if (!object.Equals(value, objB))
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

		// Token: 0x060035FC RID: 13820 RVA: 0x0010032C File Offset: 0x000FE52C
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

		// Token: 0x060035FD RID: 13821 RVA: 0x00100360 File Offset: 0x000FE560
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

		// Token: 0x060035FE RID: 13822 RVA: 0x00100440 File Offset: 0x000FE640
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

		// Token: 0x060035FF RID: 13823 RVA: 0x00100538 File Offset: 0x000FE738
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

		// Token: 0x06003600 RID: 13824 RVA: 0x0010067C File Offset: 0x000FE87C
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
						throw Error.RelatedEnd_UnableToAddRelationshipWithDeletedEntity();
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
							ReferentialConstraint referentialConstraint = ((AssociationType)relatedEnd.RelationMetadata).ReferentialConstraints[0];
							if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(@object.GetCurrentEntityValue), entityEntry2.EntityKey))
							{
								throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
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
				entityWrapper = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(o, base.ObjectStateManager);
			}
			if (!relatedEnd.ContainsEntity(entityWrapper))
			{
				relatedEnd.AddToLocalCache(entityWrapper, true);
				relatedEnd.OnAssociationChanged(CollectionChangeAction.Add, entityWrapper.Entity);
			}
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x0010082C File Offset: 0x000FEA2C
		internal void DetectChangesInRelationshipsOfSingleEntity()
		{
			StateManagerTypeMetadata cacheTypeMetadata = this._cacheTypeMetadata;
			ReadOnlyMetadataCollection<NavigationProperty> navigationProperties = (cacheTypeMetadata.CdmMetadata.EdmType as EntityType).NavigationProperties;
			foreach (NavigationProperty navigationProperty in navigationProperties)
			{
				RelatedEnd relatedEndInternal = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(navigationProperty.RelationshipType.FullName, navigationProperty.ToEndMember.Name);
				object navigationPropertyValue = this.WrappedEntity.GetNavigationPropertyValue(relatedEndInternal);
				HashSet<object> hashSet = new HashSet<object>(ObjectReferenceEqualityComparer.Default);
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
							goto IL_102;
						}
					}
					hashSet.Add(navigationPropertyValue);
				}
				IL_102:
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

		// Token: 0x06003602 RID: 13826 RVA: 0x00100A6C File Offset: 0x000FEC6C
		private void AddRelationshipDetectedByGraph(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<IEntityWrapper>>> relationships, object relatedObject, RelatedEnd relatedEndFrom, bool verifyForAdd)
		{
			IEntityWrapper entityWrapper = base.ObjectStateManager.EntityWrapperFactory.WrapEntityUsingStateManager(relatedObject, base.ObjectStateManager);
			EntityEntry.AddDetectedRelationship<IEntityWrapper>(relationships, entityWrapper, relatedEndFrom);
			RelatedEnd otherEndOfRelationship = relatedEndFrom.GetOtherEndOfRelationship(entityWrapper);
			if (verifyForAdd && otherEndOfRelationship is EntityReference && base.ObjectStateManager.FindEntityEntry(relatedObject) == null)
			{
				otherEndOfRelationship.VerifyNavigationPropertyForAdd(this._wrappedEntity);
			}
			EntityEntry.AddDetectedRelationship<IEntityWrapper>(relationships, this._wrappedEntity, otherEndOfRelationship);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x00100AD4 File Offset: 0x000FECD4
		private void AddRelationshipDetectedByForeignKey(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> relationships, Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<EntityKey>>> principalRelationships, EntityKey relatedKey, EntityEntry relatedEntry, RelatedEnd relatedEndFrom)
		{
			EntityEntry.AddDetectedRelationship<EntityKey>(relationships, relatedKey, relatedEndFrom);
			if (relatedEntry != null)
			{
				IEntityWrapper wrappedEntity = relatedEntry.WrappedEntity;
				RelatedEnd otherEndOfRelationship = relatedEndFrom.GetOtherEndOfRelationship(wrappedEntity);
				EntityKey permanentKey = base.ObjectStateManager.GetPermanentKey(relatedEntry.WrappedEntity, otherEndOfRelationship, this.WrappedEntity);
				EntityEntry.AddDetectedRelationship<EntityKey>(principalRelationships, permanentKey, otherEndOfRelationship);
			}
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x00100B24 File Offset: 0x000FED24
		private static void AddDetectedRelationship<T>(Dictionary<IEntityWrapper, Dictionary<RelatedEnd, HashSet<T>>> relationships, T relatedObject, RelatedEnd relatedEnd)
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
					throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(relatedEnd.RelationshipNavigation.To, relatedEnd.RelationshipNavigation.RelationshipName));
				}
			}
			hashSet.Add(relatedObject);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x00100BBC File Offset: 0x000FEDBC
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

		// Token: 0x06003606 RID: 13830 RVA: 0x00100C84 File Offset: 0x000FEE84
		internal void Delete(bool doFixup)
		{
			base.ValidateState();
			if (this.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotDeleteOnKeyEntry);
			}
			if (doFixup && base.State != EntityState.Deleted)
			{
				this.RelationshipManager.NullAllFKsInDependentsForWhichThisIsThePrincipal();
				this.NullAllForeignKeys();
				this.FixupRelationships();
			}
			EntityState state = base.State;
			switch (state)
			{
			case EntityState.Unchanged:
				if (!doFixup)
				{
					this.DeleteRelationshipsThatReferenceKeys(null, null);
				}
				this._cache.ChangeState(this, EntityState.Unchanged, EntityState.Deleted);
				base.State = EntityState.Deleted;
				break;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				this._cache.ChangeState(this, EntityState.Added, EntityState.Detached);
				return;
			default:
				if (state != EntityState.Deleted)
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
				break;
			}
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x00100D44 File Offset: 0x000FEF44
		private void NullAllForeignKeys()
		{
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				EntityReference entityReference = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType.FullName, tuple.Item2.FromRole.Name) as EntityReference;
				entityReference.NullAllForeignKeys();
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x00100DC8 File Offset: 0x000FEFC8
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

		// Token: 0x06003609 RID: 13833 RVA: 0x00100E68 File Offset: 0x000FF068
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

		// Token: 0x0600360A RID: 13834 RVA: 0x00100F3C File Offset: 0x000FF13C
		private void FixupRelationships()
		{
			RelationshipManager relationshipManager = this._wrappedEntity.RelationshipManager;
			relationshipManager.RemoveEntityFromRelationships();
			this.DeleteRelationshipsThatReferenceKeys(null, null);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x00100F64 File Offset: 0x000FF164
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

		// Token: 0x0600360C RID: 13836 RVA: 0x00101010 File Offset: 0x000FF210
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

		// Token: 0x0600360D RID: 13837 RVA: 0x001010B0 File Offset: 0x000FF2B0
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
										EntityEntry.AddOrIncreaseCounter(referentialConstraint, properties, referentialConstraint.ToProperties[i].Name, entityKeyMember.Value);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x00101264 File Offset: 0x000FF464
		internal static void AddOrIncreaseCounter(ReferentialConstraint constraint, Dictionary<string, KeyValuePair<object, IntBox>> properties, string propertyName, object propertyValue)
		{
			if (!properties.ContainsKey(propertyName))
			{
				properties[propertyName] = new KeyValuePair<object, IntBox>(propertyValue, new IntBox(1));
				return;
			}
			KeyValuePair<object, IntBox> keyValuePair = properties[propertyName];
			if (!ByValueEqualityComparer.Default.Equals(keyValuePair.Key, propertyValue))
			{
				throw new InvalidOperationException(constraint.BuildConstraintExceptionMessage());
			}
			keyValuePair.Value.Value = keyValuePair.Value.Value + 1;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x001012D0 File Offset: 0x000FF4D0
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
										throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x001014A8 File Offset: 0x000FF6A8
		internal void PromoteKeyEntry(IEntityWrapper wrappedEntity, StateManagerTypeMetadata typeMetadata)
		{
			this._wrappedEntity = wrappedEntity;
			this._wrappedEntity.ObjectStateEntry = this;
			this._cacheTypeMetadata = typeMetadata;
			this.SetChangeTrackingFlags();
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x001014CC File Offset: 0x000FF6CC
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
			this._wrappedEntity = NullEntityWrapper.NullWrapper;
			this.SetChangeTrackingFlags();
			this._cache.OnObjectStateManagerChanged(CollectionChangeAction.Remove, entity);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x0010159D File Offset: 0x000FF79D
		internal void AttachObjectStateManagerToEntity()
		{
			this._wrappedEntity.SetChangeTracker(this);
			this._wrappedEntity.TakeSnapshot(this);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x001015B8 File Offset: 0x000FF7B8
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

		// Token: 0x06003614 RID: 13844 RVA: 0x00101650 File Offset: 0x000FF850
		internal void AddOriginalValueAt(int index, StateManagerMemberMetadata memberMetadata, object userObject, object value)
		{
			StateManagerValue stateManagerValue = new StateManagerValue(memberMetadata, userObject, value);
			if (index >= 0)
			{
				this._originalValues[index] = stateManagerValue;
				return;
			}
			if (this._originalValues == null)
			{
				this._originalValues = new List<StateManagerValue>();
			}
			this._originalValues.Add(stateManagerValue);
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0010169C File Offset: 0x000FF89C
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
						throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyProperty(stateManagerMemberMetadata.CLayerName));
					}
				}
			}
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x00101718 File Offset: 0x000FF918
		internal object GetCurrentEntityValue(string memberName)
		{
			int ordinalforOLayerMemberName = this._cacheTypeMetadata.GetOrdinalforOLayerMemberName(memberName);
			return this.GetCurrentEntityValue(this._cacheTypeMetadata, ordinalforOLayerMemberName, this._wrappedEntity.Entity, ObjectStateValueRecord.CurrentUpdatable);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0010174C File Offset: 0x000FF94C
		internal void VerifyEntityValueIsEditable(StateManagerTypeMetadata typeMetadata, int ordinal, string memberName)
		{
			if (base.State == EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyDetachedDeletedEntries);
			}
			StateManagerMemberMetadata stateManagerMemberMetadata = typeMetadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsPartOfKey && base.State != EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyProperty(memberName));
			}
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x00101794 File Offset: 0x000FF994
		internal void SetCurrentEntityValue(StateManagerTypeMetadata metadata, int ordinal, object userObject, object newValue)
		{
			base.ValidateState();
			StateManagerMemberMetadata stateManagerMemberMetadata = metadata.Member(ordinal);
			if (stateManagerMemberMetadata.IsComplex)
			{
				if (newValue == null || newValue == DBNull.Value)
				{
					throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(stateManagerMemberMetadata.CLayerName));
				}
				IExtendedDataRecord extendedDataRecord = newValue as IExtendedDataRecord;
				if (extendedDataRecord == null)
				{
					throw new ArgumentException(Strings.ObjectStateEntry_InvalidTypeForComplexTypeProperty, "newValue");
				}
				newValue = this._cache.ComplexTypeMaterializer.CreateComplex(extendedDataRecord, extendedDataRecord.DataRecordInfo, null);
			}
			this._wrappedEntity.SetCurrentValue(this, stateManagerMemberMetadata, ordinal, userObject, newValue);
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0010181C File Offset: 0x000FFA1C
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

		// Token: 0x0600361A RID: 13850 RVA: 0x00101882 File Offset: 0x000FFA82
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[Conditional("DEBUG")]
		private void VerifyIsNotRelated()
		{
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x00101884 File Offset: 0x000FFA84
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
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
					throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
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
					throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
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
						throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
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
					throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "requestedState");
				}
				return;
			}
			if (requestedState == EntityState.Unchanged)
			{
				return;
			}
			throw new InvalidOperationException(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x00101B84 File Offset: 0x000FFD84
		internal void UpdateOriginalValues(object entity)
		{
			EntityState state = base.State;
			this.UpdateRecordWithSetModified(entity, this.EditableOriginalValues);
			if (state == EntityState.Unchanged && base.State == EntityState.Modified)
			{
				base.ObjectStateManager.ChangeState(this, state, EntityState.Modified);
			}
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x00101BC2 File Offset: 0x000FFDC2
		internal void UpdateRecordWithoutSetModified(object value, DbUpdatableDataRecord current)
		{
			this.UpdateRecord(value, current, EntityEntry.UpdateRecordBehavior.WithoutSetModified, -1);
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x00101BCE File Offset: 0x000FFDCE
		internal void UpdateRecordWithSetModified(object value, DbUpdatableDataRecord current)
		{
			this.UpdateRecord(value, current, EntityEntry.UpdateRecordBehavior.WithSetModified, -1);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x00101BDC File Offset: 0x000FFDDC
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
						throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(fieldMetadata.FieldType.Name));
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

		// Token: 0x06003620 RID: 13856 RVA: 0x00101CF8 File Offset: 0x000FFEF8
		internal bool HasRecordValueChanged(DbDataRecord record, int propertyIndex, object newFieldValue)
		{
			object value = record.GetValue(propertyIndex);
			return (value != newFieldValue && (DBNull.Value == newFieldValue || DBNull.Value == value || !ByValueEqualityComparer.Default.Equals(value, newFieldValue))) || (this._cache.EntryHasConceptualNull(this) && this._modifiedFields != null && this._modifiedFields[propertyIndex]);
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x00101D58 File Offset: 0x000FFF58
		internal void ApplyCurrentValuesInternal(IEntityWrapper wrappedCurrentEntity)
		{
			if (base.State != EntityState.Modified && base.State != EntityState.Unchanged)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityMustBeUnchangedOrModified(base.State.ToString()));
			}
			if (this.WrappedEntity.IdentityType != wrappedCurrentEntity.IdentityType)
			{
				throw new ArgumentException(Strings.ObjectContext_EntitiesHaveDifferentType(this.Entity.GetType().FullName, wrappedCurrentEntity.Entity.GetType().FullName));
			}
			this.CompareKeyProperties(wrappedCurrentEntity.Entity);
			this.UpdateCurrentValueRecord(wrappedCurrentEntity.Entity);
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x00101DEE File Offset: 0x000FFFEE
		internal void UpdateCurrentValueRecord(object value)
		{
			this._wrappedEntity.UpdateCurrentValueRecord(value, this);
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x00101E00 File Offset: 0x00100000
		internal void ApplyOriginalValuesInternal(IEntityWrapper wrappedOriginalEntity)
		{
			if (base.State != EntityState.Modified && base.State != EntityState.Unchanged && base.State != EntityState.Deleted)
			{
				throw new InvalidOperationException(Strings.ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(base.State.ToString()));
			}
			if (this.WrappedEntity.IdentityType != wrappedOriginalEntity.IdentityType)
			{
				throw new ArgumentException(Strings.ObjectContext_EntitiesHaveDifferentType(this.Entity.GetType().FullName, wrappedOriginalEntity.Entity.GetType().FullName));
			}
			this.CompareKeyProperties(wrappedOriginalEntity.Entity);
			this.UpdateOriginalValues(wrappedOriginalEntity.Entity);
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x00101EA0 File Offset: 0x001000A0
		internal void RemoveFromForeignKeyIndex()
		{
			if (!this.IsKeyEntry)
			{
				foreach (EntityReference entityReference in this.FindFKRelatedEnds())
				{
					foreach (EntityKey foreignKey in entityReference.GetAllKeyValues())
					{
						this._cache.RemoveEntryFromForeignKeyIndex(entityReference, foreignKey, this);
					}
				}
			}
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x00101F98 File Offset: 0x00100198
		internal void FixupReferencesByForeignKeys(bool replaceAddedRefs, EntitySetBase restrictTo = null)
		{
			this._cache.TransactionManager.BeginGraphUpdate();
			bool setIsLoaded = !this._cache.TransactionManager.IsAttachTracking && !this._cache.TransactionManager.IsAddTracking;
			try
			{
				foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in from t in this.ForeignKeyDependents
				where restrictTo == null || t.Item1.SourceSet.Identity == restrictTo.Identity || t.Item1.TargetSet.Identity == restrictTo.Identity
				select t)
				{
					EntityReference entityReference = this.WrappedEntity.RelationshipManager.GetRelatedEndInternal(tuple.Item1.ElementType, (AssociationEndMember)tuple.Item2.FromRole) as EntityReference;
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

		// Token: 0x06003626 RID: 13862 RVA: 0x001020AC File Offset: 0x001002AC
		internal void FixupEntityReferenceByForeignKey(EntityReference reference)
		{
			reference.IsLoaded = false;
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

		// Token: 0x06003627 RID: 13863 RVA: 0x001021D0 File Offset: 0x001003D0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
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
				if (this._cache.TryGetEntityEntry(foreignKey, out entityEntry) && !entityEntry.IsKeyEntry && entityEntry.State != EntityState.Deleted && (replaceExistingRef || EntityEntry.WillNotRefSteal(relatedEnd, entityEntry.WrappedEntity)) && relatedEnd.CanSetEntityType(entityEntry.WrappedEntity))
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
						relatedEnd.IsLoaded = true;
						return;
					}
				}
				else
				{
					this._cache.AddEntryContainingForeignKeyToIndex(relatedEnd, foreignKey, this);
					if (flag && replaceExistingRef && relatedEnd.ReferenceValue.Entity != null)
					{
						relatedEnd.ReferenceValue = NullEntityWrapper.NullWrapper;
						return;
					}
				}
			}
			else if (flag)
			{
				if (replaceExistingRef && (relatedEnd.ReferenceValue.Entity != null || relatedEnd.EntityKey != null))
				{
					relatedEnd.ReferenceValue = NullEntityWrapper.NullWrapper;
				}
				if (setIsLoaded)
				{
					relatedEnd.IsLoaded = true;
				}
			}
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x0010239C File Offset: 0x0010059C
		private static bool WillNotRefSteal(EntityReference refToPrincipal, IEntityWrapper wrappedPrincipal)
		{
			RelatedEnd otherEndOfRelationship = refToPrincipal.GetOtherEndOfRelationship(wrappedPrincipal);
			EntityReference entityReference = otherEndOfRelationship as EntityReference;
			if (refToPrincipal.ReferenceValue.Entity == null && refToPrincipal.NavigationPropertyIsNullOrMissing() && (entityReference == null || (entityReference.ReferenceValue.Entity == null && entityReference.NavigationPropertyIsNullOrMissing())))
			{
				return true;
			}
			if (entityReference != null && (object.ReferenceEquals(entityReference.ReferenceValue.Entity, refToPrincipal.WrappedOwner.Entity) || entityReference.CheckIfNavigationPropertyContainsEntity(refToPrincipal.WrappedOwner)))
			{
				return true;
			}
			if (entityReference == null || object.ReferenceEquals(refToPrincipal.ReferenceValue.Entity, wrappedPrincipal.Entity) || refToPrincipal.CheckIfNavigationPropertyContainsEntity(wrappedPrincipal))
			{
				return false;
			}
			throw new InvalidOperationException(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(entityReference.RelationshipNavigation.To, entityReference.RelationshipNavigation.RelationshipName));
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x00102460 File Offset: 0x00100660
		internal bool TryGetReferenceKey(AssociationEndMember principalRole, out EntityKey principalKey)
		{
			EntityReference entityReference = this.RelationshipManager.GetRelatedEnd(principalRole.DeclaringType.FullName, principalRole.Name) as EntityReference;
			if (entityReference.CachedValue.Entity == null || entityReference.CachedValue.ObjectStateEntry == null)
			{
				principalKey = null;
				return false;
			}
			principalKey = (entityReference.EntityKey ?? entityReference.CachedValue.ObjectStateEntry.EntityKey);
			return principalKey != null;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x001024D4 File Offset: 0x001006D4
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

		// Token: 0x0600362B RID: 13867 RVA: 0x0010251C File Offset: 0x0010071C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
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
								goto IL_122;
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
								throw new InvalidOperationException(Strings.RelationshipManager_CircularRelationshipsWithReferentialConstraints);
							}
						}
						else
						{
							visited.Add(this);
							objectStateEntry.FixupForeignKeysByReference(visited);
							visited.Remove(this);
						}
					}
					IL_122:
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

		// Token: 0x0600362C RID: 13868 RVA: 0x00102864 File Offset: 0x00100A64
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

		// Token: 0x0600362D RID: 13869 RVA: 0x00102900 File Offset: 0x00100B00
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

		// Token: 0x0600362E RID: 13870 RVA: 0x001029E0 File Offset: 0x00100BE0
		internal void FindRelatedEntityKeysByForeignKeys(out Dictionary<RelatedEnd, HashSet<EntityKey>> relatedEntities, bool useOriginalValues)
		{
			relatedEntities = null;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in this.ForeignKeyDependents)
			{
				AssociationSet item = tuple.Item1;
				ReferentialConstraint item2 = tuple.Item2;
				string identity = item2.ToRole.Identity;
				ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = item.AssociationSetEnds;
				AssociationEndMember correspondingAssociationEndMember;
				if (associationSetEnds[0].CorrespondingAssociationEndMember.Identity == identity)
				{
					correspondingAssociationEndMember = associationSetEnds[1].CorrespondingAssociationEndMember;
				}
				else
				{
					correspondingAssociationEndMember = associationSetEnds[0].CorrespondingAssociationEndMember;
				}
				EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(item, correspondingAssociationEndMember);
				EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(this, item2, entitySetAtEnd, useOriginalValues);
				if (entityKey != null)
				{
					EntityReference key = this.RelationshipManager.GetRelatedEndInternal(item.ElementType, (AssociationEndMember)item2.FromRole) as EntityReference;
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

		// Token: 0x0600362F RID: 13871 RVA: 0x00102B0C File Offset: 0x00100D0C
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

		// Token: 0x06003630 RID: 13872 RVA: 0x00102B94 File Offset: 0x00100D94
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
						EntityEntry.AddDetectedRelationship<EntityKey>(transactionManager.DeletedRelationshipsByForeignKey, cachedForeignKey, entityReference);
					}
					else if (!entityKey.Equals(cachedForeignKey) && (!flag || ForeignKeyFactory.IsConceptualNullKeyChanged(cachedForeignKey, entityKey)))
					{
						EntityEntry relatedEntry2;
						base.ObjectStateManager.TryGetEntityEntry(entityKey, out relatedEntry2);
						this.AddRelationshipDetectedByForeignKey(transactionManager.AddedRelationshipsByForeignKey, transactionManager.AddedRelationshipsByPrincipalKey, entityKey, relatedEntry2, entityReference);
						if (!flag)
						{
							EntityEntry.AddDetectedRelationship<EntityKey>(transactionManager.DeletedRelationshipsByForeignKey, cachedForeignKey, entityReference);
						}
					}
				}
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x00102CB4 File Offset: 0x00100EB4
		internal bool RequiresComplexChangeTracking
		{
			get
			{
				return this._requiresComplexChangeTracking;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x00102CBC File Offset: 0x00100EBC
		internal bool RequiresScalarChangeTracking
		{
			get
			{
				return this._requiresScalarChangeTracking;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x00102CC4 File Offset: 0x00100EC4
		internal bool RequiresAnyChangeTracking
		{
			get
			{
				return this._requiresAnyChangeTracking;
			}
		}

		// Token: 0x040014B3 RID: 5299
		internal const int s_EntityRoot = -1;

		// Token: 0x040014B4 RID: 5300
		private StateManagerTypeMetadata _cacheTypeMetadata;

		// Token: 0x040014B5 RID: 5301
		private EntityKey _entityKey;

		// Token: 0x040014B6 RID: 5302
		private IEntityWrapper _wrappedEntity;

		// Token: 0x040014B7 RID: 5303
		private BitArray _modifiedFields;

		// Token: 0x040014B8 RID: 5304
		private List<StateManagerValue> _originalValues;

		// Token: 0x040014B9 RID: 5305
		private Dictionary<object, Dictionary<int, object>> _originalComplexObjects;

		// Token: 0x040014BA RID: 5306
		private bool _requiresComplexChangeTracking;

		// Token: 0x040014BB RID: 5307
		private bool _requiresScalarChangeTracking;

		// Token: 0x040014BC RID: 5308
		private bool _requiresAnyChangeTracking;

		// Token: 0x040014BD RID: 5309
		private RelationshipEntry _headRelationshipEnds;

		// Token: 0x040014BE RID: 5310
		private int _countRelationshipEnds;

		// Token: 0x0200056C RID: 1388
		internal struct RelationshipEndEnumerable : IEnumerable<RelationshipEntry>, IEnumerable<IEntityStateEntry>, IEnumerable
		{
			// Token: 0x06003635 RID: 13877 RVA: 0x00102CCC File Offset: 0x00100ECC
			internal RelationshipEndEnumerable(EntityEntry entityEntry)
			{
				this._entityEntry = entityEntry;
			}

			// Token: 0x06003636 RID: 13878 RVA: 0x00102CD5 File Offset: 0x00100ED5
			public EntityEntry.RelationshipEndEnumerator GetEnumerator()
			{
				return new EntityEntry.RelationshipEndEnumerator(this._entityEntry);
			}

			// Token: 0x06003637 RID: 13879 RVA: 0x00102CE2 File Offset: 0x00100EE2
			IEnumerator<IEntityStateEntry> IEnumerable<IEntityStateEntry>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003638 RID: 13880 RVA: 0x00102CEF File Offset: 0x00100EEF
			IEnumerator<RelationshipEntry> IEnumerable<RelationshipEntry>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003639 RID: 13881 RVA: 0x00102CFC File Offset: 0x00100EFC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600363A RID: 13882 RVA: 0x00102D0C File Offset: 0x00100F0C
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

			// Token: 0x040014C0 RID: 5312
			internal static readonly RelationshipEntry[] EmptyRelationshipEntryArray = new RelationshipEntry[0];

			// Token: 0x040014C1 RID: 5313
			private readonly EntityEntry _entityEntry;
		}

		// Token: 0x0200056D RID: 1389
		internal struct RelationshipEndEnumerator : IEnumerator<RelationshipEntry>, IEnumerator<IEntityStateEntry>, IDisposable, IEnumerator
		{
			// Token: 0x0600363C RID: 13884 RVA: 0x00102D89 File Offset: 0x00100F89
			internal RelationshipEndEnumerator(EntityEntry entityEntry)
			{
				this._entityEntry = entityEntry;
				this._current = null;
			}

			// Token: 0x17000817 RID: 2071
			// (get) Token: 0x0600363D RID: 13885 RVA: 0x00102D99 File Offset: 0x00100F99
			public RelationshipEntry Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000818 RID: 2072
			// (get) Token: 0x0600363E RID: 13886 RVA: 0x00102DA1 File Offset: 0x00100FA1
			IEntityStateEntry IEnumerator<IEntityStateEntry>.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000819 RID: 2073
			// (get) Token: 0x0600363F RID: 13887 RVA: 0x00102DA9 File Offset: 0x00100FA9
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003640 RID: 13888 RVA: 0x00102DB1 File Offset: 0x00100FB1
			public void Dispose()
			{
			}

			// Token: 0x06003641 RID: 13889 RVA: 0x00102DB4 File Offset: 0x00100FB4
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
				return null != this._current;
			}

			// Token: 0x06003642 RID: 13890 RVA: 0x00102E0C File Offset: 0x0010100C
			public void Reset()
			{
			}

			// Token: 0x040014C2 RID: 5314
			private readonly EntityEntry _entityEntry;

			// Token: 0x040014C3 RID: 5315
			private RelationshipEntry _current;
		}

		// Token: 0x0200056E RID: 1390
		private enum UpdateRecordBehavior
		{
			// Token: 0x040014C5 RID: 5317
			WithoutSetModified,
			// Token: 0x040014C6 RID: 5318
			WithSetModified
		}
	}
}
