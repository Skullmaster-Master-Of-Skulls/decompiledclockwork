using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B9 RID: 1465
	internal sealed class RelationshipEntry : ObjectStateEntry
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x00116C17 File Offset: 0x00114E17
		internal EntityKey Key0
		{
			get
			{
				return this.RelationshipWrapper.Key0;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x00116C24 File Offset: 0x00114E24
		internal EntityKey Key1
		{
			get
			{
				return this.RelationshipWrapper.Key1;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x00116C31 File Offset: 0x00114E31
		internal override BitArray ModifiedProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x00116C34 File Offset: 0x00114E34
		internal RelationshipEntry(ObjectStateManager cache, EntityState state, RelationshipWrapper relationshipWrapper) : base(cache, null, state)
		{
			this._entitySet = relationshipWrapper.AssociationSet;
			this._relationshipWrapper = relationshipWrapper;
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x00116C52 File Offset: 0x00114E52
		public override bool IsRelationship
		{
			get
			{
				base.ValidateState();
				return true;
			}
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x00116C5C File Offset: 0x00114E5C
		public override void AcceptChanges()
		{
			base.ValidateState();
			EntityState state = base.State;
			switch (state)
			{
			case EntityState.Unchanged:
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				this._cache.ChangeState(this, EntityState.Added, EntityState.Unchanged);
				base.State = EntityState.Unchanged;
				break;
			default:
				if (state != EntityState.Deleted)
				{
					if (state != EntityState.Modified)
					{
						return;
					}
				}
				else
				{
					this.DeleteUnnecessaryKeyEntries();
					if (this._cache != null)
					{
						this._cache.ChangeState(this, EntityState.Deleted, EntityState.Detached);
						return;
					}
				}
				break;
			}
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x00116CC6 File Offset: 0x00114EC6
		public override void Delete()
		{
			this.Delete(true);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x00116D7C File Offset: 0x00114F7C
		public override IEnumerable<string> GetModifiedProperties()
		{
			base.ValidateState();
			yield break;
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x00116D99 File Offset: 0x00114F99
		public override void SetModified()
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x00116DAB File Offset: 0x00114FAB
		public override object Entity
		{
			get
			{
				base.ValidateState();
				return null;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x00116DB4 File Offset: 0x00114FB4
		// (set) Token: 0x06003AA8 RID: 15016 RVA: 0x00116DBD File Offset: 0x00114FBD
		public override EntityKey EntityKey
		{
			get
			{
				base.ValidateState();
				return null;
			}
			internal set
			{
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x00116DBF File Offset: 0x00114FBF
		public override void SetModifiedProperty(string propertyName)
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x00116DD1 File Offset: 0x00114FD1
		public override void RejectPropertyChanges(string propertyName)
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00116DE3 File Offset: 0x00114FE3
		public override bool IsPropertyChanged(string propertyName)
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x00116DF5 File Offset: 0x00114FF5
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override DbDataRecord OriginalValues
		{
			get
			{
				base.ValidateState();
				if (base.State == EntityState.Added)
				{
					throw new InvalidOperationException(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
				}
				return new ObjectStateEntryDbDataRecord(this);
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x00116E17 File Offset: 0x00115017
		public override OriginalValueRecord GetUpdatableOriginalValues()
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x00116E23 File Offset: 0x00115023
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
				return new ObjectStateEntryDbUpdatableDataRecord(this);
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x00116E45 File Offset: 0x00115045
		public override RelationshipManager RelationshipManager
		{
			get
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers);
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x00116E54 File Offset: 0x00115054
		public override void ChangeState(EntityState state)
		{
			EntityUtil.CheckValidStateForChangeRelationshipState(state, "state");
			if (base.State == EntityState.Detached && state == EntityState.Detached)
			{
				return;
			}
			base.ValidateState();
			if (this.RelationshipWrapper.Key0 == this.Key0)
			{
				base.ObjectStateManager.ChangeRelationshipState(this.Key0, this.Key1, this.RelationshipWrapper.AssociationSet.ElementType.FullName, this.RelationshipWrapper.AssociationEndMembers[1].Name, state);
				return;
			}
			base.ObjectStateManager.ChangeRelationshipState(this.Key0, this.Key1, this.RelationshipWrapper.AssociationSet.ElementType.FullName, this.RelationshipWrapper.AssociationEndMembers[0].Name, state);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x00116F21 File Offset: 0x00115121
		public override void ApplyCurrentValues(object currentEntity)
		{
			Check.NotNull<object>(currentEntity, "currentEntity");
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x00116F39 File Offset: 0x00115139
		public override void ApplyOriginalValues(object originalEntity)
		{
			Check.NotNull<object>(originalEntity, "originalEntity");
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x00116F51 File Offset: 0x00115151
		internal override bool IsKeyEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x00116F54 File Offset: 0x00115154
		internal override int GetFieldCount(StateManagerTypeMetadata metadata)
		{
			return this._relationshipWrapper.AssociationEndMembers.Count;
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x00116F66 File Offset: 0x00115166
		internal override DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject)
		{
			return new DataRecordInfo(TypeUsage.Create(((RelationshipSet)base.EntitySet).ElementType));
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x00116F82 File Offset: 0x00115182
		internal override void SetModifiedAll()
		{
			base.ValidateState();
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x00116F94 File Offset: 0x00115194
		internal override Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata)
		{
			return typeof(EntityKey);
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x00116FA0 File Offset: 0x001151A0
		internal override string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata)
		{
			RelationshipEntry.ValidateRelationshipRange(ordinal);
			return this._relationshipWrapper.AssociationEndMembers[ordinal].Name;
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x00116FC0 File Offset: 0x001151C0
		internal override int GetOrdinalforCLayerName(string name, StateManagerTypeMetadata metadata)
		{
			ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = this._relationshipWrapper.AssociationEndMembers;
			AssociationEndMember value;
			if (associationEndMembers.TryGetValue(name, false, out value))
			{
				return associationEndMembers.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x00116FEE File Offset: 0x001151EE
		internal override void RevertDelete()
		{
			base.State = EntityState.Unchanged;
			this._cache.ChangeState(this, EntityState.Deleted, base.State);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0011700A File Offset: 0x0011520A
		internal override void EntityMemberChanging(string entityMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x00117016 File Offset: 0x00115216
		internal override void EntityMemberChanged(string entityMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x00117022 File Offset: 0x00115222
		internal override void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0011702E File Offset: 0x0011522E
		internal override void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			throw new InvalidOperationException(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x0011703C File Offset: 0x0011523C
		internal bool IsSameAssociationSetAndRole(AssociationSet associationSet, AssociationEndMember associationMember, EntityKey entityKey)
		{
			if (!object.ReferenceEquals(this._entitySet, associationSet))
			{
				return false;
			}
			if (this._relationshipWrapper.AssociationSet.ElementType.AssociationEndMembers[0].Name == associationMember.Name)
			{
				return entityKey == this.Key0;
			}
			return entityKey == this.Key1;
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x0011709F File Offset: 0x0011529F
		private object GetCurrentRelationValue(int ordinal, bool throwException)
		{
			RelationshipEntry.ValidateRelationshipRange(ordinal);
			base.ValidateState();
			if (base.State == EntityState.Deleted && throwException)
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_CurrentValuesDoesNotExist);
			}
			return this._relationshipWrapper.GetEntityKey(ordinal);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x001170D0 File Offset: 0x001152D0
		private static void ValidateRelationshipRange(int ordinal)
		{
			if (1 < ordinal)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x001170E1 File Offset: 0x001152E1
		internal object GetCurrentRelationValue(int ordinal)
		{
			return this.GetCurrentRelationValue(ordinal, true);
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x001170EB File Offset: 0x001152EB
		// (set) Token: 0x06003AC4 RID: 15044 RVA: 0x001170F3 File Offset: 0x001152F3
		internal RelationshipWrapper RelationshipWrapper
		{
			get
			{
				return this._relationshipWrapper;
			}
			set
			{
				this._relationshipWrapper = value;
			}
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x001170FC File Offset: 0x001152FC
		internal override void Reset()
		{
			this._relationshipWrapper = null;
			base.Reset();
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x0011710C File Offset: 0x0011530C
		internal void ChangeRelatedEnd(EntityKey oldKey, EntityKey newKey)
		{
			if (!oldKey.Equals(this.Key0))
			{
				this.RelationshipWrapper = new RelationshipWrapper(this.RelationshipWrapper, 1, newKey);
				return;
			}
			if (oldKey.Equals(this.Key1))
			{
				this.RelationshipWrapper = new RelationshipWrapper(this.RelationshipWrapper.AssociationSet, newKey);
				return;
			}
			this.RelationshipWrapper = new RelationshipWrapper(this.RelationshipWrapper, 0, newKey);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x00117174 File Offset: 0x00115374
		internal void DeleteUnnecessaryKeyEntries()
		{
			for (int i = 0; i < 2; i++)
			{
				EntityKey key = this.GetCurrentRelationValue(i, false) as EntityKey;
				EntityEntry entityEntry = this._cache.GetEntityEntry(key);
				if (entityEntry.IsKeyEntry)
				{
					bool flag = false;
					foreach (RelationshipEntry relationshipEntry in this._cache.FindRelationshipsByKey(key))
					{
						if (relationshipEntry != this)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this._cache.DeleteKeyEntry(entityEntry);
						return;
					}
				}
			}
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0011721C File Offset: 0x0011541C
		internal void Delete(bool doFixup)
		{
			base.ValidateState();
			if (doFixup)
			{
				if (base.State != EntityState.Deleted)
				{
					EntityEntry entityEntry = this._cache.GetEntityEntry((EntityKey)this.GetCurrentRelationValue(0));
					IEntityWrapper wrappedEntity = entityEntry.WrappedEntity;
					EntityEntry entityEntry2 = this._cache.GetEntityEntry((EntityKey)this.GetCurrentRelationValue(1));
					IEntityWrapper wrappedEntity2 = entityEntry2.WrappedEntity;
					if (wrappedEntity.Entity != null && wrappedEntity2.Entity != null)
					{
						ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = this._relationshipWrapper.AssociationEndMembers;
						string name = associationEndMembers[1].Name;
						string fullName = ((AssociationSet)this._entitySet).ElementType.FullName;
						wrappedEntity.RelationshipManager.RemoveEntity(name, fullName, wrappedEntity2);
						return;
					}
					EntityKey entityKey;
					RelationshipManager relationshipManager;
					if (wrappedEntity.Entity == null)
					{
						entityKey = entityEntry.EntityKey;
						relationshipManager = wrappedEntity2.RelationshipManager;
					}
					else
					{
						entityKey = entityEntry2.EntityKey;
						relationshipManager = wrappedEntity.RelationshipManager;
					}
					AssociationEndMember associationEndMember = this.RelationshipWrapper.GetAssociationEndMember(entityKey);
					EntityReference entityReference = (EntityReference)relationshipManager.GetRelatedEndInternal(associationEndMember.DeclaringType.FullName, associationEndMember.Name);
					entityReference.DetachedEntityKey = null;
					if (base.State == EntityState.Added)
					{
						this.DeleteUnnecessaryKeyEntries();
						this.DetachRelationshipEntry();
						return;
					}
					this._cache.ChangeState(this, base.State, EntityState.Deleted);
					base.State = EntityState.Deleted;
					return;
				}
			}
			else
			{
				EntityState state = base.State;
				switch (state)
				{
				case EntityState.Unchanged:
					this._cache.ChangeState(this, EntityState.Unchanged, EntityState.Deleted);
					base.State = EntityState.Deleted;
					break;
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					this.DeleteUnnecessaryKeyEntries();
					this.DetachRelationshipEntry();
					return;
				default:
					if (state != EntityState.Modified)
					{
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x001173B2 File Offset: 0x001155B2
		internal object GetOriginalRelationValue(int ordinal)
		{
			return this.GetCurrentRelationValue(ordinal, false);
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x001173BC File Offset: 0x001155BC
		internal void DetachRelationshipEntry()
		{
			if (this._cache != null)
			{
				this._cache.ChangeState(this, base.State, EntityState.Detached);
			}
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x001173DC File Offset: 0x001155DC
		internal void ChangeRelationshipState(EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState requestedState)
		{
			EntityState state = base.State;
			EntityState entityState = state;
			switch (entityState)
			{
			case EntityState.Unchanged:
				switch (requestedState)
				{
				case EntityState.Detached:
					this.Delete();
					this.AcceptChanges();
					return;
				case EntityState.Unchanged:
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					base.ObjectStateManager.ChangeState(this, EntityState.Unchanged, EntityState.Added);
					base.State = EntityState.Added;
					return;
				default:
					if (requestedState != EntityState.Deleted)
					{
						return;
					}
					this.Delete();
					return;
				}
				break;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				switch (requestedState)
				{
				case EntityState.Detached:
					this.Delete();
					return;
				case EntityState.Unchanged:
					this.AcceptChanges();
					return;
				case EntityState.Detached | EntityState.Unchanged:
				case EntityState.Added:
					break;
				default:
					if (requestedState != EntityState.Deleted)
					{
						return;
					}
					this.AcceptChanges();
					this.Delete();
					return;
				}
				break;
			default:
				if (entityState != EntityState.Deleted)
				{
					return;
				}
				switch (requestedState)
				{
				case EntityState.Detached:
					this.AcceptChanges();
					break;
				case EntityState.Unchanged:
					relatedEnd.Add(targetEntry.WrappedEntity, true, false, true, false, true);
					base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Unchanged);
					base.State = EntityState.Unchanged;
					return;
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					relatedEnd.Add(targetEntry.WrappedEntity, true, false, true, false, true);
					base.ObjectStateManager.ChangeState(this, EntityState.Deleted, EntityState.Added);
					base.State = EntityState.Added;
					return;
				default:
					if (requestedState != EntityState.Deleted)
					{
						return;
					}
					break;
				}
				break;
			}
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x00117507 File Offset: 0x00115707
		internal RelationshipEntry GetNextRelationshipEnd(EntityKey entityKey)
		{
			if (!entityKey.Equals(this.Key0))
			{
				return this.NextKey1;
			}
			return this.NextKey0;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x00117524 File Offset: 0x00115724
		internal void SetNextRelationshipEnd(EntityKey entityKey, RelationshipEntry nextEnd)
		{
			if (entityKey.Equals(this.Key0))
			{
				this.NextKey0 = nextEnd;
				return;
			}
			this.NextKey1 = nextEnd;
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x00117543 File Offset: 0x00115743
		// (set) Token: 0x06003ACF RID: 15055 RVA: 0x0011754B File Offset: 0x0011574B
		internal RelationshipEntry NextKey0 { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x00117554 File Offset: 0x00115754
		// (set) Token: 0x06003AD1 RID: 15057 RVA: 0x0011755C File Offset: 0x0011575C
		internal RelationshipEntry NextKey1 { get; set; }

		// Token: 0x04001638 RID: 5688
		internal RelationshipWrapper _relationshipWrapper;
	}
}
