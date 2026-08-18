using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects.Internal;
using System.Diagnostics;

namespace System.Data.Objects
{
	// Token: 0x02000153 RID: 339
	internal sealed class RelationshipEntry : ObjectStateEntry
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00058740 File Offset: 0x00056940
		internal EntityKey Key0
		{
			get
			{
				return this.RelationshipWrapper.Key0;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0005874D File Offset: 0x0005694D
		internal EntityKey Key1
		{
			get
			{
				return this.RelationshipWrapper.Key1;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00006174 File Offset: 0x00004374
		internal override BitArray ModifiedProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0005875A File Offset: 0x0005695A
		internal RelationshipEntry(ObjectStateManager cache, EntityState state, RelationshipWrapper relationshipWrapper) : base(cache, null, state)
		{
			this._entitySet = relationshipWrapper.AssociationSet;
			this._relationshipWrapper = relationshipWrapper;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00058778 File Offset: 0x00056978
		public override bool IsRelationship
		{
			get
			{
				base.ValidateState();
				return true;
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00058784 File Offset: 0x00056984
		public override void AcceptChanges()
		{
			base.ValidateState();
			EntityState state = base.State;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					this._cache.ChangeState(this, EntityState.Added, EntityState.Unchanged);
					base.State = EntityState.Unchanged;
				}
			}
			else
			{
				if (state != EntityState.Deleted)
				{
					return;
				}
				this.DeleteUnnecessaryKeyEntries();
				if (this._cache != null)
				{
					this._cache.ChangeState(this, EntityState.Deleted, EntityState.Detached);
					return;
				}
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000587E7 File Offset: 0x000569E7
		public override void Delete()
		{
			this.Delete(true);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000587F0 File Offset: 0x000569F0
		public override IEnumerable<string> GetModifiedProperties()
		{
			base.ValidateState();
			yield break;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00058800 File Offset: 0x00056A00
		public override void SetModified()
		{
			base.ValidateState();
			throw EntityUtil.CantModifyRelationState();
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0005880D File Offset: 0x00056A0D
		public override object Entity
		{
			get
			{
				base.ValidateState();
				return null;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0005880D File Offset: 0x00056A0D
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x000089D0 File Offset: 0x00006BD0
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

		// Token: 0x06001936 RID: 6454 RVA: 0x00058800 File Offset: 0x00056A00
		public override void SetModifiedProperty(string propertyName)
		{
			base.ValidateState();
			throw EntityUtil.CantModifyRelationState();
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00058800 File Offset: 0x00056A00
		public override void RejectPropertyChanges(string propertyName)
		{
			base.ValidateState();
			throw EntityUtil.CantModifyRelationState();
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00058800 File Offset: 0x00056A00
		public override bool IsPropertyChanged(string propertyName)
		{
			base.ValidateState();
			throw EntityUtil.CantModifyRelationState();
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00058816 File Offset: 0x00056A16
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override DbDataRecord OriginalValues
		{
			get
			{
				base.ValidateState();
				if (base.State == EntityState.Added)
				{
					throw EntityUtil.OriginalValuesDoesNotExist();
				}
				return new ObjectStateEntryDbDataRecord(this);
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00058833 File Offset: 0x00056A33
		public override OriginalValueRecord GetUpdatableOriginalValues()
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x0005883A File Offset: 0x00056A3A
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
				return new ObjectStateEntryDbUpdatableDataRecord(this);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00058857 File Offset: 0x00056A57
		public override RelationshipManager RelationshipManager
		{
			get
			{
				throw new InvalidOperationException(Strings.ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00058864 File Offset: 0x00056A64
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

		// Token: 0x0600193E RID: 6462 RVA: 0x00058833 File Offset: 0x00056A33
		public override void ApplyCurrentValues(object currentEntity)
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00058833 File Offset: 0x00056A33
		public override void ApplyOriginalValues(object originalEntity)
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsKeyEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00058931 File Offset: 0x00056B31
		internal override int GetFieldCount(StateManagerTypeMetadata metadata)
		{
			return this._relationshipWrapper.AssociationEndMembers.Count;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00058943 File Offset: 0x00056B43
		internal override DataRecordInfo GetDataRecordInfo(StateManagerTypeMetadata metadata, object userObject)
		{
			return new DataRecordInfo(TypeUsage.Create(((RelationshipSet)base.EntitySet).ElementType));
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00058800 File Offset: 0x00056A00
		internal override void SetModifiedAll()
		{
			base.ValidateState();
			throw EntityUtil.CantModifyRelationState();
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0005895F File Offset: 0x00056B5F
		internal override Type GetFieldType(int ordinal, StateManagerTypeMetadata metadata)
		{
			return typeof(EntityKey);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005896B File Offset: 0x00056B6B
		internal override string GetCLayerName(int ordinal, StateManagerTypeMetadata metadata)
		{
			RelationshipEntry.ValidateRelationshipRange(ordinal);
			return this._relationshipWrapper.AssociationEndMembers[ordinal].Name;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0005898C File Offset: 0x00056B8C
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

		// Token: 0x06001947 RID: 6471 RVA: 0x000589BA File Offset: 0x00056BBA
		internal override void RevertDelete()
		{
			base.State = EntityState.Unchanged;
			this._cache.ChangeState(this, EntityState.Deleted, base.State);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00058833 File Offset: 0x00056A33
		internal override void EntityMemberChanging(string entityMemberName)
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00058833 File Offset: 0x00056A33
		internal override void EntityMemberChanged(string entityMemberName)
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00058833 File Offset: 0x00056A33
		internal override void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00058833 File Offset: 0x00056A33
		internal override void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName)
		{
			throw EntityUtil.CantModifyRelationValues();
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000589D8 File Offset: 0x00056BD8
		internal bool IsSameAssociationSetAndRole(AssociationSet associationSet, AssociationEndMember associationMember, EntityKey entityKey)
		{
			if (this._entitySet != associationSet)
			{
				return false;
			}
			if (this._relationshipWrapper.AssociationSet.ElementType.AssociationEndMembers[0].Name == associationMember.Name)
			{
				return entityKey == this.Key0;
			}
			return entityKey == this.Key1;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00058A36 File Offset: 0x00056C36
		private object GetCurrentRelationValue(int ordinal, bool throwException)
		{
			RelationshipEntry.ValidateRelationshipRange(ordinal);
			base.ValidateState();
			if (base.State == EntityState.Deleted && throwException)
			{
				throw EntityUtil.CurrentValuesDoesNotExist();
			}
			return this._relationshipWrapper.GetEntityKey(ordinal);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00058A63 File Offset: 0x00056C63
		private static void ValidateRelationshipRange(int ordinal)
		{
			if (1 < ordinal)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00058A74 File Offset: 0x00056C74
		internal object GetCurrentRelationValue(int ordinal)
		{
			return this.GetCurrentRelationValue(ordinal, true);
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00058A7E File Offset: 0x00056C7E
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x00058A86 File Offset: 0x00056C86
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

		// Token: 0x06001952 RID: 6482 RVA: 0x00058A8F File Offset: 0x00056C8F
		internal override void Reset()
		{
			this._relationshipWrapper = null;
			base.Reset();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00058AA0 File Offset: 0x00056CA0
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

		// Token: 0x06001954 RID: 6484 RVA: 0x00058B08 File Offset: 0x00056D08
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

		// Token: 0x06001955 RID: 6485 RVA: 0x00058BB0 File Offset: 0x00056DB0
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
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return;
					}
					this.DeleteUnnecessaryKeyEntries();
					this.DetachRelationshipEntry();
					return;
				}
				else
				{
					this._cache.ChangeState(this, EntityState.Unchanged, EntityState.Deleted);
					base.State = EntityState.Deleted;
				}
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00058D3B File Offset: 0x00056F3B
		internal object GetOriginalRelationValue(int ordinal)
		{
			return this.GetCurrentRelationValue(ordinal, false);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00058D45 File Offset: 0x00056F45
		internal void DetachRelationshipEntry()
		{
			if (this._cache != null)
			{
				this._cache.ChangeState(this, base.State, EntityState.Detached);
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00058D64 File Offset: 0x00056F64
		internal void ChangeRelationshipState(EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState requestedState)
		{
			EntityState state = base.State;
			if (state != EntityState.Unchanged)
			{
				if (state != EntityState.Added)
				{
					if (state != EntityState.Deleted)
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
						return;
					}
				}
				else
				{
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
				}
			}
			else
			{
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
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00058E78 File Offset: 0x00057078
		internal RelationshipEntry GetNextRelationshipEnd(EntityKey entityKey)
		{
			if (!entityKey.Equals(this.Key0))
			{
				return this.NextKey1;
			}
			return this.NextKey0;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00058E95 File Offset: 0x00057095
		internal void SetNextRelationshipEnd(EntityKey entityKey, RelationshipEntry nextEnd)
		{
			if (entityKey.Equals(this.Key0))
			{
				this.NextKey0 = nextEnd;
				return;
			}
			this.NextKey1 = nextEnd;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00058EB4 File Offset: 0x000570B4
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x00058EBC File Offset: 0x000570BC
		internal RelationshipEntry NextKey0
		{
			get
			{
				return this._nextKey0;
			}
			set
			{
				this._nextKey0 = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00058EC5 File Offset: 0x000570C5
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x00058ECD File Offset: 0x000570CD
		internal RelationshipEntry NextKey1
		{
			get
			{
				return this._nextKey1;
			}
			set
			{
				this._nextKey1 = value;
			}
		}

		// Token: 0x04000ADF RID: 2783
		internal RelationshipWrapper _relationshipWrapper;

		// Token: 0x04000AE0 RID: 2784
		private RelationshipEntry _nextKey0;

		// Token: 0x04000AE1 RID: 2785
		private RelationshipEntry _nextKey1;
	}
}
