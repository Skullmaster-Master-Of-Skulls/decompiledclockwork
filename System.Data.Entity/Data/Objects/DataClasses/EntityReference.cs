using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200018E RID: 398
	[DataContract]
	[Serializable]
	public abstract class EntityReference : RelatedEnd
	{
		// Token: 0x06001C7C RID: 7292 RVA: 0x0005FE50 File Offset: 0x0005E050
		internal EntityReference()
		{
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0005FE58 File Offset: 0x0005E058
		internal EntityReference(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer) : base(wrappedOwner, navigation, relationshipFixer)
		{
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00060930 File Offset: 0x0005EB30
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x00060A70 File Offset: 0x0005EC70
		[DataMember]
		public EntityKey EntityKey
		{
			get
			{
				if (base.ObjectContext != null && !base.UsingNoTracking)
				{
					EntityKey entityKey = null;
					if (this.CachedValue.Entity != null)
					{
						entityKey = this.CachedValue.EntityKey;
						if (entityKey != null && !RelatedEnd.IsValidEntityKeyType(entityKey))
						{
							entityKey = null;
						}
					}
					else if (base.IsForeignKey)
					{
						if (base.IsDependentEndOfReferentialConstraint(false) && this._cachedForeignKey != null)
						{
							if (!ForeignKeyFactory.IsConceptualNullKey(this._cachedForeignKey))
							{
								entityKey = this._cachedForeignKey;
							}
						}
						else
						{
							entityKey = this.DetachedEntityKey;
						}
					}
					else
					{
						EntityKey entityKey2 = base.WrappedOwner.EntityKey;
						foreach (RelationshipEntry relationshipEntry in base.ObjectContext.ObjectStateManager.FindRelationshipsByKey(entityKey2))
						{
							if (relationshipEntry.State != EntityState.Deleted && relationshipEntry.IsSameAssociationSetAndRole((AssociationSet)base.RelationshipSet, (AssociationEndMember)base.FromEndProperty, entityKey2))
							{
								entityKey = relationshipEntry.RelationshipWrapper.GetOtherEntityKey(entityKey2);
							}
						}
					}
					return entityKey;
				}
				return this.DetachedEntityKey;
			}
			set
			{
				this.SetEntityKey(value, false);
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00060A7C File Offset: 0x0005EC7C
		internal void SetEntityKey(EntityKey value, bool forceFixup)
		{
			if (value != null && value == this.EntityKey && (this.ReferenceValue.Entity != null || (this.ReferenceValue.Entity == null && !forceFixup)))
			{
				return;
			}
			if (base.ObjectContext != null && !base.UsingNoTracking)
			{
				if (value != null && !RelatedEnd.IsValidEntityKeyType(value))
				{
					throw EntityUtil.CannotSetSpecialKeys();
				}
				if (value == null)
				{
					if (this.AttemptToNullFKsOnRefOrKeySetToNull())
					{
						this.DetachedEntityKey = null;
						return;
					}
					this.ReferenceValue = EntityWrapperFactory.NullWrapper;
					return;
				}
				else
				{
					EntitySet entitySet = value.GetEntitySet(base.ObjectContext.MetadataWorkspace);
					base.CheckRelationEntitySet(entitySet);
					value.ValidateEntityKey(base.ObjectContext.MetadataWorkspace, entitySet, true, "value");
					ObjectStateManager objectStateManager = base.ObjectContext.ObjectStateManager;
					bool flag = false;
					bool flag2 = false;
					EntityEntry entityEntry = objectStateManager.FindEntityEntry(value);
					if (entityEntry != null)
					{
						if (!entityEntry.IsKeyEntry)
						{
							this.ReferenceValue = entityEntry.WrappedEntity;
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag2 = !base.IsForeignKey;
						flag = true;
					}
					if (flag)
					{
						EntityKey entityKey = this.ValidateOwnerWithRIConstraints((entityEntry == null) ? null : entityEntry.WrappedEntity, value, true);
						base.ValidateStateForAdd(base.WrappedOwner);
						if (flag2)
						{
							objectStateManager.AddKeyEntry(value, entitySet);
						}
						objectStateManager.TransactionManager.EntityBeingReparented = base.WrappedOwner.Entity;
						try
						{
							this.ClearCollectionOrRef(null, null, false);
						}
						finally
						{
							objectStateManager.TransactionManager.EntityBeingReparented = null;
						}
						if (!base.IsForeignKey)
						{
							RelationshipWrapper wrapper = new RelationshipWrapper((AssociationSet)base.RelationshipSet, base.RelationshipNavigation.From, entityKey, base.RelationshipNavigation.To, value);
							EntityState desiredState = EntityState.Added;
							if (!entityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(false))
							{
								desiredState = EntityState.Unchanged;
							}
							objectStateManager.AddNewRelation(wrapper, desiredState);
							return;
						}
						this.DetachedEntityKey = value;
						if (base.IsDependentEndOfReferentialConstraint(false))
						{
							this.UpdateForeignKeyValues(base.WrappedOwner, value);
							return;
						}
					}
				}
			}
			else
			{
				this.DetachedEntityKey = value;
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00060C78 File Offset: 0x0005EE78
		internal bool AttemptToNullFKsOnRefOrKeySetToNull()
		{
			if (this.ReferenceValue.Entity != null || base.WrappedOwner.Entity == null || base.WrappedOwner.Context == null || base.UsingNoTracking || !base.IsForeignKey)
			{
				return false;
			}
			if (base.WrappedOwner.ObjectStateEntry.State != EntityState.Added && base.IsDependentEndOfReferentialConstraint(true))
			{
				throw EntityUtil.CannotChangeReferentialConstraintProperty();
			}
			this.RemoveFromLocalCache(EntityWrapperFactory.NullWrapper, true, false);
			return true;
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00060CEF File Offset: 0x0005EEEF
		internal EntityKey AttachedEntityKey
		{
			get
			{
				return this.EntityKey;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00060CF7 File Offset: 0x0005EEF7
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x00060CFF File Offset: 0x0005EEFF
		internal EntityKey DetachedEntityKey
		{
			get
			{
				return this._detachedEntityKey;
			}
			set
			{
				this._detachedEntityKey = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00060D08 File Offset: 0x0005EF08
		internal EntityKey CachedForeignKey
		{
			get
			{
				return this.EntityKey ?? this._cachedForeignKey;
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00060D1C File Offset: 0x0005EF1C
		internal void SetCachedForeignKey(EntityKey newForeignKey, EntityEntry source)
		{
			if (base.ObjectContext != null && base.ObjectContext.ObjectStateManager != null && source != null && this._cachedForeignKey != null && !ForeignKeyFactory.IsConceptualNullKey(this._cachedForeignKey) && this._cachedForeignKey != newForeignKey)
			{
				base.ObjectContext.ObjectStateManager.RemoveEntryFromForeignKeyIndex(this._cachedForeignKey, source);
			}
			this._cachedForeignKey = newForeignKey;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00060D88 File Offset: 0x0005EF88
		internal IEnumerable<EntityKey> GetAllKeyValues()
		{
			if (this.EntityKey != null)
			{
				yield return this.EntityKey;
			}
			if (this._cachedForeignKey != null)
			{
				yield return this._cachedForeignKey;
			}
			if (this._detachedEntityKey != null)
			{
				yield return this._detachedEntityKey;
			}
			yield break;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C88 RID: 7304
		internal abstract IEntityWrapper CachedValue { get; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C89 RID: 7305
		// (set) Token: 0x06001C8A RID: 7306
		internal abstract IEntityWrapper ReferenceValue { get; set; }

		// Token: 0x06001C8B RID: 7307 RVA: 0x00060D98 File Offset: 0x0005EF98
		internal EntityKey ValidateOwnerWithRIConstraints(IEntityWrapper targetEntity, EntityKey targetEntityKey, bool checkBothEnds)
		{
			EntityKey entityKey = base.WrappedOwner.EntityKey;
			if (entityKey != null && !entityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(true))
			{
				this.ValidateSettingRIConstraints(targetEntity, targetEntityKey == null, this.CachedForeignKey != null && this.CachedForeignKey != targetEntityKey);
			}
			else if (checkBothEnds && targetEntity != null && targetEntity.Entity != null)
			{
				EntityReference entityReference = base.GetOtherEndOfRelationship(targetEntity) as EntityReference;
				if (entityReference != null)
				{
					entityReference.ValidateOwnerWithRIConstraints(base.WrappedOwner, entityKey, false);
				}
			}
			return entityKey;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00060E20 File Offset: 0x0005F020
		internal void ValidateSettingRIConstraints(IEntityWrapper targetEntity, bool settingToNull, bool changingForeignKeyValue)
		{
			bool flag = targetEntity != null && targetEntity.MergeOption == MergeOption.NoTracking;
			if (settingToNull || changingForeignKeyValue || (targetEntity != null && !flag && (targetEntity.ObjectStateEntry == null || (this.EntityKey == null && targetEntity.ObjectStateEntry.State == EntityState.Deleted) || (this.CachedForeignKey == null && targetEntity.ObjectStateEntry.State == EntityState.Added))))
			{
				throw EntityUtil.CannotChangeReferentialConstraintProperty();
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x00060E8E File Offset: 0x0005F08E
		internal override bool CanDeferredLoad
		{
			get
			{
				return this.IsEmpty();
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00060E98 File Offset: 0x0005F098
		internal void UpdateForeignKeyValues(IEntityWrapper dependentEntity, IEntityWrapper principalEntity, Dictionary<int, object> changedFKs, bool forceChange)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)base.RelationMetadata).ReferentialConstraints[0];
			bool flag = base.WrappedOwner.EntityKey != null && !base.WrappedOwner.EntityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(true);
			ObjectStateManager objectStateManager = base.ObjectContext.ObjectStateManager;
			objectStateManager.TransactionManager.BeginForeignKeyUpdate(this);
			try
			{
				EntitySet entitySet = ((AssociationSet)base.RelationshipSet).AssociationSetEnds[base.ToEndMember.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(principalEntity.IdentityType, entitySet);
				EntitySet entitySet2 = ((AssociationSet)base.RelationshipSet).AssociationSetEnds[base.FromEndProperty.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata2 = objectStateManager.GetOrAddStateManagerTypeMetadata(dependentEntity.IdentityType, entitySet2);
				ReadOnlyMetadataCollection<EdmProperty> fromProperties = referentialConstraint.FromProperties;
				int count = fromProperties.Count;
				string[] array = null;
				object[] array2 = null;
				if (count > 1)
				{
					array = entitySet.ElementType.KeyMemberNames;
					array2 = new object[count];
				}
				for (int i = 0; i < count; i++)
				{
					int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(fromProperties[i].Name);
					object value = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName).GetValue(principalEntity.Entity);
					int ordinalforOLayerMemberName2 = orAddStateManagerTypeMetadata2.GetOrdinalforOLayerMemberName(referentialConstraint.ToProperties[i].Name);
					bool flag2 = !ByValueEqualityComparer.Default.Equals(orAddStateManagerTypeMetadata2.Member(ordinalforOLayerMemberName2).GetValue(dependentEntity.Entity), value);
					if (forceChange || flag2)
					{
						if (flag)
						{
							this.ValidateSettingRIConstraints(principalEntity, value == null, flag2);
						}
						if (changedFKs != null)
						{
							object x;
							if (changedFKs.TryGetValue(ordinalforOLayerMemberName2, out x))
							{
								if (!ByValueEqualityComparer.Default.Equals(x, value))
								{
									throw new InvalidOperationException(Strings.Update_ReferentialConstraintIntegrityViolation);
								}
							}
							else
							{
								changedFKs[ordinalforOLayerMemberName2] = value;
							}
						}
						dependentEntity.SetCurrentValue(dependentEntity.ObjectStateEntry, orAddStateManagerTypeMetadata2.Member(ordinalforOLayerMemberName2), -1, dependentEntity.Entity, value);
					}
					if (count > 1)
					{
						int num = Array.IndexOf<string>(array, fromProperties[i].Name);
						array2[num] = value;
					}
					else
					{
						this.SetCachedForeignKey(new EntityKey(entitySet, value), dependentEntity.ObjectStateEntry);
					}
				}
				if (count > 1)
				{
					this.SetCachedForeignKey(new EntityKey(entitySet, array2), dependentEntity.ObjectStateEntry);
				}
				if (base.WrappedOwner.ObjectStateEntry != null)
				{
					objectStateManager.ForgetEntryWithConceptualNull(base.WrappedOwner.ObjectStateEntry, false);
				}
			}
			finally
			{
				objectStateManager.TransactionManager.EndForeignKeyUpdate();
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00061128 File Offset: 0x0005F328
		internal void UpdateForeignKeyValues(IEntityWrapper dependentEntity, EntityKey principalKey)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)base.RelationMetadata).ReferentialConstraints[0];
			ObjectStateManager objectStateManager = base.ObjectContext.ObjectStateManager;
			objectStateManager.TransactionManager.BeginForeignKeyUpdate(this);
			try
			{
				EntitySet entitySet = ((AssociationSet)base.RelationshipSet).AssociationSetEnds[base.FromEndProperty.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(dependentEntity.IdentityType, entitySet);
				for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
				{
					object obj = principalKey.FindValueByName(referentialConstraint.FromProperties[i].Name);
					int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(referentialConstraint.ToProperties[i].Name);
					object value = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName).GetValue(dependentEntity.Entity);
					if (!ByValueEqualityComparer.Default.Equals(value, obj))
					{
						dependentEntity.SetCurrentValue(dependentEntity.ObjectStateEntry, orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName), -1, dependentEntity.Entity, obj);
					}
				}
				this.SetCachedForeignKey(principalKey, dependentEntity.ObjectStateEntry);
				if (base.WrappedOwner.ObjectStateEntry != null)
				{
					objectStateManager.ForgetEntryWithConceptualNull(base.WrappedOwner.ObjectStateEntry, false);
				}
			}
			finally
			{
				objectStateManager.TransactionManager.EndForeignKeyUpdate();
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00061278 File Offset: 0x0005F478
		internal object GetDependentEndOfReferentialConstraint(object relatedValue)
		{
			if (!base.IsDependentEndOfReferentialConstraint(false))
			{
				return relatedValue;
			}
			return base.WrappedOwner.Entity;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00061290 File Offset: 0x0005F490
		internal bool NavigationPropertyIsNullOrMissing()
		{
			return !base.TargetAccessor.HasProperty || base.WrappedOwner.GetNavigationPropertyValue(this) == null;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x000612B0 File Offset: 0x0005F4B0
		internal void NullAllForeignKeys()
		{
			ObjectStateManager objectStateManager = base.ObjectContext.ObjectStateManager;
			EntityEntry objectStateEntry = base.WrappedOwner.ObjectStateEntry;
			TransactionManager transactionManager = objectStateManager.TransactionManager;
			if (!transactionManager.IsGraphUpdate && !transactionManager.IsAttachTracking && !transactionManager.IsRelatedEndAdd)
			{
				ReferentialConstraint referentialConstraint = ((AssociationType)base.RelationMetadata).ReferentialConstraints.Single<ReferentialConstraint>();
				if (base.TargetRoleName == referentialConstraint.FromRole.Name)
				{
					if (transactionManager.IsDetaching)
					{
						EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(objectStateEntry, this);
						if (entityKey != null)
						{
							objectStateManager.AddEntryContainingForeignKeyToIndex(entityKey, objectStateEntry);
							return;
						}
					}
					else if (objectStateManager.EntityInvokingFKSetter != base.WrappedOwner.Entity && !transactionManager.IsForeignKeyUpdate)
					{
						transactionManager.BeginForeignKeyUpdate(this);
						try
						{
							bool flag = true;
							bool flag2 = objectStateEntry != null && (objectStateEntry.State == EntityState.Modified || objectStateEntry.State == EntityState.Unchanged);
							EntitySet entitySet = ((AssociationSet)base.RelationshipSet).AssociationSetEnds[base.FromEndProperty.Name].EntitySet;
							StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(base.WrappedOwner.IdentityType, entitySet);
							for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
							{
								string name = referentialConstraint.ToProperties[i].Name;
								int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(name);
								StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName);
								if (stateManagerMemberMetadata.ClrMetadata.Nullable)
								{
									if (stateManagerMemberMetadata.GetValue(base.WrappedOwner.Entity) != null)
									{
										base.WrappedOwner.SetCurrentValue(base.WrappedOwner.ObjectStateEntry, orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName), -1, base.WrappedOwner.Entity, null);
									}
									else if (flag2 && base.WrappedOwner.ObjectStateEntry.OriginalValues.GetValue(ordinalforOLayerMemberName) != null)
									{
										objectStateEntry.SetModifiedProperty(name);
									}
									flag = false;
								}
								else if (flag2)
								{
									objectStateEntry.SetModifiedProperty(name);
								}
							}
							if (flag)
							{
								if (objectStateEntry != null)
								{
									EntityKey entityKey2 = this.CachedForeignKey;
									if (entityKey2 == null)
									{
										entityKey2 = ForeignKeyFactory.CreateKeyFromForeignKeyValues(objectStateEntry, this);
									}
									if (entityKey2 != null)
									{
										this.SetCachedForeignKey(ForeignKeyFactory.CreateConceptualNullKey(entityKey2), objectStateEntry);
										objectStateManager.RememberEntryWithConceptualNull(objectStateEntry);
									}
								}
							}
							else
							{
								this.SetCachedForeignKey(null, objectStateEntry);
							}
						}
						finally
						{
							transactionManager.EndForeignKeyUpdate();
						}
					}
				}
			}
		}

		// Token: 0x04000BB1 RID: 2993
		private EntityKey _detachedEntityKey;

		// Token: 0x04000BB2 RID: 2994
		[NonSerialized]
		private EntityKey _cachedForeignKey;
	}
}
