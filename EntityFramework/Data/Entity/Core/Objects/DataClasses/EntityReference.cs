using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000543 RID: 1347
	[DataContract]
	[Serializable]
	public abstract class EntityReference : RelatedEnd
	{
		// Token: 0x060033DF RID: 13279 RVA: 0x000F4377 File Offset: 0x000F2577
		internal EntityReference()
		{
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000F437F File Offset: 0x000F257F
		internal EntityReference(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer) : base(wrappedOwner, navigation, relationshipFixer)
		{
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000F438C File Offset: 0x000F258C
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000F44C4 File Offset: 0x000F26C4
		[DataMember]
		public EntityKey EntityKey
		{
			get
			{
				if (this.ObjectContext != null && !base.UsingNoTracking)
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
						EntityKey entityKey2 = this.WrappedOwner.EntityKey;
						foreach (RelationshipEntry relationshipEntry in this.ObjectContext.ObjectStateManager.FindRelationshipsByKey(entityKey2))
						{
							if (relationshipEntry.State != EntityState.Deleted && relationshipEntry.IsSameAssociationSetAndRole((AssociationSet)this.RelationshipSet, (AssociationEndMember)this.FromEndMember, entityKey2))
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

		// Token: 0x060033E3 RID: 13283 RVA: 0x000F44D0 File Offset: 0x000F26D0
		internal void SetEntityKey(EntityKey value, bool forceFixup)
		{
			if (value != null && value == this.EntityKey && (this.ReferenceValue.Entity != null || (this.ReferenceValue.Entity == null && !forceFixup)))
			{
				return;
			}
			if (this.ObjectContext != null && !base.UsingNoTracking)
			{
				if (value != null && !RelatedEnd.IsValidEntityKeyType(value))
				{
					throw new ArgumentException(Strings.EntityReference_CannotSetSpecialKeys, "value");
				}
				if (value == null)
				{
					if (this.AttemptToNullFKsOnRefOrKeySetToNull())
					{
						this.DetachedEntityKey = null;
						return;
					}
					this.ReferenceValue = NullEntityWrapper.NullWrapper;
					return;
				}
				else
				{
					EntitySet entitySet = value.GetEntitySet(this.ObjectContext.MetadataWorkspace);
					base.CheckRelationEntitySet(entitySet);
					value.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, entitySet, true, "value");
					ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
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
						base.ValidateStateForAdd(this.WrappedOwner);
						if (flag2)
						{
							objectStateManager.AddKeyEntry(value, entitySet);
						}
						objectStateManager.TransactionManager.EntityBeingReparented = this.WrappedOwner.Entity;
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
							RelationshipWrapper wrapper = new RelationshipWrapper((AssociationSet)this.RelationshipSet, base.RelationshipNavigation.From, entityKey, base.RelationshipNavigation.To, value);
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
							this.UpdateForeignKeyValues(this.WrappedOwner, value);
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

		// Token: 0x060033E4 RID: 13284 RVA: 0x000F46D4 File Offset: 0x000F28D4
		internal bool AttemptToNullFKsOnRefOrKeySetToNull()
		{
			if (this.ReferenceValue.Entity != null || this.WrappedOwner.Entity == null || this.WrappedOwner.Context == null || base.UsingNoTracking || !base.IsForeignKey)
			{
				return false;
			}
			if (this.WrappedOwner.ObjectStateEntry.State != EntityState.Added && base.IsDependentEndOfReferentialConstraint(true))
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
			}
			this.RemoveFromLocalCache(NullEntityWrapper.NullWrapper, true, false);
			return true;
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000F4750 File Offset: 0x000F2950
		internal EntityKey AttachedEntityKey
		{
			get
			{
				return this.EntityKey;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000F4758 File Offset: 0x000F2958
		// (set) Token: 0x060033E7 RID: 13287 RVA: 0x000F4760 File Offset: 0x000F2960
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

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000F4769 File Offset: 0x000F2969
		internal EntityKey CachedForeignKey
		{
			get
			{
				return this.EntityKey ?? this._cachedForeignKey;
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000F477C File Offset: 0x000F297C
		internal void SetCachedForeignKey(EntityKey newForeignKey, EntityEntry source)
		{
			if (this.ObjectContext != null && this.ObjectContext.ObjectStateManager != null && source != null && this._cachedForeignKey != null && !ForeignKeyFactory.IsConceptualNullKey(this._cachedForeignKey) && this._cachedForeignKey != newForeignKey)
			{
				this.ObjectContext.ObjectStateManager.RemoveEntryFromForeignKeyIndex(this, this._cachedForeignKey, source);
			}
			this._cachedForeignKey = newForeignKey;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000F4940 File Offset: 0x000F2B40
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

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060033EB RID: 13291
		internal abstract IEntityWrapper CachedValue { get; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060033EC RID: 13292
		// (set) Token: 0x060033ED RID: 13293
		internal abstract IEntityWrapper ReferenceValue { get; set; }

		// Token: 0x060033EE RID: 13294 RVA: 0x000F4960 File Offset: 0x000F2B60
		internal EntityKey ValidateOwnerWithRIConstraints(IEntityWrapper targetEntity, EntityKey targetEntityKey, bool checkBothEnds)
		{
			EntityKey entityKey = this.WrappedOwner.EntityKey;
			if (entityKey != null && !entityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(true))
			{
				this.ValidateSettingRIConstraints(targetEntity, targetEntityKey == null, this.CachedForeignKey != null && this.CachedForeignKey != targetEntityKey);
			}
			else if (checkBothEnds && targetEntity != null && targetEntity.Entity != null)
			{
				EntityReference entityReference = base.GetOtherEndOfRelationship(targetEntity) as EntityReference;
				if (entityReference != null)
				{
					entityReference.ValidateOwnerWithRIConstraints(this.WrappedOwner, entityKey, false);
				}
			}
			return entityKey;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000F49E8 File Offset: 0x000F2BE8
		internal void ValidateSettingRIConstraints(IEntityWrapper targetEntity, bool settingToNull, bool changingForeignKeyValue)
		{
			bool flag = targetEntity != null && targetEntity.MergeOption == MergeOption.NoTracking;
			if (settingToNull || changingForeignKeyValue || (targetEntity != null && !flag && (targetEntity.ObjectStateEntry == null || (this.EntityKey == null && targetEntity.ObjectStateEntry.State == EntityState.Deleted) || (this.CachedForeignKey == null && targetEntity.ObjectStateEntry.State == EntityState.Added))))
			{
				throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000F4A5C File Offset: 0x000F2C5C
		internal override bool CanDeferredLoad
		{
			get
			{
				return this.IsEmpty();
			}
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000F4A6C File Offset: 0x000F2C6C
		internal void UpdateForeignKeyValues(IEntityWrapper dependentEntity, IEntityWrapper principalEntity, Dictionary<int, object> changedFKs, bool forceChange)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints[0];
			bool flag = this.WrappedOwner.EntityKey != null && !this.WrappedOwner.EntityKey.IsTemporary && base.IsDependentEndOfReferentialConstraint(true);
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			objectStateManager.TransactionManager.BeginForeignKeyUpdate(this);
			try
			{
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.ToEndMember.Name].EntitySet;
				StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(principalEntity.IdentityType, entitySet);
				EntitySet entitySet2 = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.FromEndMember.Name].EntitySet;
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
						this.SetCachedForeignKey((value == null) ? null : new EntityKey(entitySet, value), dependentEntity.ObjectStateEntry);
					}
				}
				if (count > 1)
				{
					this.SetCachedForeignKey(array2.Any((object v) => v == null) ? null : new EntityKey(entitySet, array2), dependentEntity.ObjectStateEntry);
				}
				if (this.WrappedOwner.ObjectStateEntry != null)
				{
					objectStateManager.ForgetEntryWithConceptualNull(this.WrappedOwner.ObjectStateEntry, false);
				}
			}
			finally
			{
				objectStateManager.TransactionManager.EndForeignKeyUpdate();
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000F4D2C File Offset: 0x000F2F2C
		internal void UpdateForeignKeyValues(IEntityWrapper dependentEntity, EntityKey principalKey)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints[0];
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			objectStateManager.TransactionManager.BeginForeignKeyUpdate(this);
			try
			{
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.FromEndMember.Name].EntitySet;
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
				if (this.WrappedOwner.ObjectStateEntry != null)
				{
					objectStateManager.ForgetEntryWithConceptualNull(this.WrappedOwner.ObjectStateEntry, false);
				}
			}
			finally
			{
				objectStateManager.TransactionManager.EndForeignKeyUpdate();
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000F4E7C File Offset: 0x000F307C
		internal object GetDependentEndOfReferentialConstraint(object relatedValue)
		{
			if (!base.IsDependentEndOfReferentialConstraint(false))
			{
				return relatedValue;
			}
			return this.WrappedOwner.Entity;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000F4E94 File Offset: 0x000F3094
		internal bool NavigationPropertyIsNullOrMissing()
		{
			return !base.TargetAccessor.HasProperty || this.WrappedOwner.GetNavigationPropertyValue(this) == null;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000F4EB4 File Offset: 0x000F30B4
		internal override void AddEntityToObjectStateManager(IEntityWrapper wrappedEntity, bool doAttach)
		{
			base.AddEntityToObjectStateManager(wrappedEntity, doAttach);
			if (this.DetachedEntityKey != null)
			{
				EntityKey entityKey = wrappedEntity.EntityKey;
				if (this.DetachedEntityKey != entityKey)
				{
					throw new InvalidOperationException(Strings.EntityReference_EntityKeyValueMismatch);
				}
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000F4EF8 File Offset: 0x000F30F8
		internal override void AddToNavigationPropertyIfCompatible(RelatedEnd otherRelatedEnd)
		{
			if (this.NavigationPropertyIsNullOrMissing())
			{
				base.AddToNavigationProperty(otherRelatedEnd.WrappedOwner);
				EntityEntry entityEntry = otherRelatedEnd.ObjectContext.ObjectStateManager.FindEntityEntry(otherRelatedEnd.WrappedOwner.Entity);
				if (entityEntry != null && otherRelatedEnd.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking && otherRelatedEnd.IsForeignKey && base.IsDependentEndOfReferentialConstraint(false))
				{
					base.MarkForeignKeyPropertiesModified();
					return;
				}
			}
			else if (!this.CheckIfNavigationPropertyContainsEntity(otherRelatedEnd.WrappedOwner))
			{
				throw Error.ObjectStateManager_ConflictingChangesOfRelationshipDetected(base.RelationshipNavigation.To, base.RelationshipNavigation.RelationshipName);
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000F4F91 File Offset: 0x000F3191
		internal override bool CachedForeignKeyIsConceptualNull()
		{
			return ForeignKeyFactory.IsConceptualNullKey(this.CachedForeignKey);
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000F4F9E File Offset: 0x000F319E
		internal override bool UpdateDependentEndForeignKey(RelatedEnd targetRelatedEnd, bool forceForeignKeyChanges)
		{
			if (base.IsDependentEndOfReferentialConstraint(false))
			{
				this.UpdateForeignKeyValues(this.WrappedOwner, targetRelatedEnd.WrappedOwner, null, forceForeignKeyChanges);
				return true;
			}
			return false;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000F4FC0 File Offset: 0x000F31C0
		internal override void ValidateDetachedEntityKey()
		{
			if (this.IsEmpty() && this.DetachedEntityKey != null)
			{
				EntityKey detachedEntityKey = this.DetachedEntityKey;
				if (!RelatedEnd.IsValidEntityKeyType(detachedEntityKey))
				{
					throw Error.EntityReference_CannotSetSpecialKeys();
				}
				EntitySet entitySet = detachedEntityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace);
				base.CheckRelationEntitySet(entitySet);
				detachedEntityKey.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, entitySet);
			}
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000F5024 File Offset: 0x000F3224
		internal override void VerifyDetachedKeyMatches(EntityKey entityKey)
		{
			if (!(this.DetachedEntityKey != null) || !(this.DetachedEntityKey != entityKey))
			{
				return;
			}
			if (entityKey.IsTemporary)
			{
				throw Error.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(base.RelationshipNavigation.To);
			}
			throw new InvalidOperationException(Strings.EntityReference_EntityKeyValueMismatch);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000F5073 File Offset: 0x000F3273
		internal override void DetachAll(EntityState ownerEntityState)
		{
			this.DetachedEntityKey = this.AttachedEntityKey;
			base.DetachAll(ownerEntityState);
			if (base.IsForeignKey)
			{
				this.DetachedEntityKey = null;
			}
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000F5098 File Offset: 0x000F3298
		internal override bool CheckReferentialConstraintPrincipalProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			EntityKey principalKey;
			if (!this.IsEmpty())
			{
				IEntityWrapper referenceValue = this.ReferenceValue;
				if (referenceValue.ObjectStateEntry != null && referenceValue.ObjectStateEntry.State == EntityState.Added)
				{
					return true;
				}
				principalKey = this.ExtractPrincipalKey(referenceValue);
			}
			else
			{
				if ((this.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne && this.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.One) || !(this.DetachedEntityKey != null))
				{
					return true;
				}
				if (base.IsForeignKey && !this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking && !this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking)
				{
					principalKey = this.EntityKey;
				}
				else
				{
					principalKey = this.DetachedEntityKey;
				}
			}
			return RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(ownerEntry.GetCurrentEntityValue), principalKey);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000F515C File Offset: 0x000F335C
		internal override bool CheckReferentialConstraintDependentProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			if (!this.IsEmpty())
			{
				return base.CheckReferentialConstraintDependentProperty(ownerEntry, constraint);
			}
			if ((this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One) && this.DetachedEntityKey != null)
			{
				EntityKey detachedEntityKey = this.DetachedEntityKey;
				if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(detachedEntityKey.FindValueByName), ownerEntry.EntityKey))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000F51C8 File Offset: 0x000F33C8
		private EntityKey ExtractPrincipalKey(IEntityWrapper wrappedRelatedEntity)
		{
			EntitySet targetEntitySetFromRelationshipSet = base.GetTargetEntitySetFromRelationshipSet();
			EntityKey entityKey = wrappedRelatedEntity.EntityKey;
			if (entityKey != null && !entityKey.IsTemporary)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, targetEntitySetFromRelationshipSet);
				entityKey.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, targetEntitySetFromRelationshipSet);
			}
			else
			{
				entityKey = this.ObjectContext.ObjectStateManager.CreateEntityKey(targetEntitySetFromRelationshipSet, wrappedRelatedEntity.Entity);
			}
			return entityKey;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000F5224 File Offset: 0x000F3424
		internal void NullAllForeignKeys()
		{
			ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
			EntityEntry objectStateEntry = this.WrappedOwner.ObjectStateEntry;
			TransactionManager transactionManager = objectStateManager.TransactionManager;
			if (!transactionManager.IsGraphUpdate && !transactionManager.IsAttachTracking && !transactionManager.IsRelatedEndAdd)
			{
				ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints.Single<ReferentialConstraint>();
				if (this.TargetRoleName == referentialConstraint.FromRole.Name)
				{
					if (transactionManager.IsDetaching)
					{
						EntityKey entityKey = ForeignKeyFactory.CreateKeyFromForeignKeyValues(objectStateEntry, this);
						if (entityKey != null)
						{
							objectStateManager.AddEntryContainingForeignKeyToIndex(this, entityKey, objectStateEntry);
							return;
						}
					}
					else if (!object.ReferenceEquals(objectStateManager.EntityInvokingFKSetter, this.WrappedOwner.Entity) && !transactionManager.IsForeignKeyUpdate)
					{
						transactionManager.BeginForeignKeyUpdate(this);
						try
						{
							bool flag = true;
							bool flag2 = objectStateEntry != null && (objectStateEntry.State == EntityState.Modified || objectStateEntry.State == EntityState.Unchanged);
							EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.FromEndMember.Name].EntitySet;
							StateManagerTypeMetadata orAddStateManagerTypeMetadata = objectStateManager.GetOrAddStateManagerTypeMetadata(this.WrappedOwner.IdentityType, entitySet);
							for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
							{
								string name = referentialConstraint.ToProperties[i].Name;
								int ordinalforOLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforOLayerMemberName(name);
								StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName);
								if (stateManagerMemberMetadata.ClrMetadata.Nullable)
								{
									if (stateManagerMemberMetadata.GetValue(this.WrappedOwner.Entity) != null)
									{
										this.WrappedOwner.SetCurrentValue(this.WrappedOwner.ObjectStateEntry, orAddStateManagerTypeMetadata.Member(ordinalforOLayerMemberName), -1, this.WrappedOwner.Entity, null);
									}
									else if (flag2 && this.WrappedOwner.ObjectStateEntry.OriginalValues.GetValue(ordinalforOLayerMemberName) != null)
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

		// Token: 0x04001394 RID: 5012
		private EntityKey _detachedEntityKey;

		// Token: 0x04001395 RID: 5013
		[NonSerialized]
		private EntityKey _cachedForeignKey;
	}
}
