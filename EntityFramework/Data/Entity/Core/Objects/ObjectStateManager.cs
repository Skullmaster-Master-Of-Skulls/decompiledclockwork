using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B0 RID: 1456
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class ObjectStateManager : IEntityStateManager
	{
		// Token: 0x060039D2 RID: 14802 RVA: 0x00111E2B File Offset: 0x0011002B
		internal ObjectStateManager()
		{
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x00111E40 File Offset: 0x00110040
		public ObjectStateManager(MetadataWorkspace metadataWorkspace)
		{
			Check.NotNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			this._metadataWorkspace = metadataWorkspace;
			this._metadataStore = new Dictionary<EdmType, StateManagerTypeMetadata>();
			this._metadataMapping = new Dictionary<EntitySetQualifiedType, StateManagerTypeMetadata>(EntitySetQualifiedType.EqualityComparer);
			this._isDisposed = false;
			this._entityWrapperFactory = new EntityWrapperFactory();
			this.TransactionManager = new TransactionManager();
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x00111EA9 File Offset: 0x001100A9
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x00111EB1 File Offset: 0x001100B1
		internal virtual object ChangingObject { get; set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x00111EBA File Offset: 0x001100BA
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x00111EC2 File Offset: 0x001100C2
		internal virtual string ChangingEntityMember { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x00111ECB File Offset: 0x001100CB
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x00111ED3 File Offset: 0x001100D3
		internal virtual string ChangingMember { get; set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x00111EDC File Offset: 0x001100DC
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x00111EE4 File Offset: 0x001100E4
		internal virtual EntityState ChangingState { get; set; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x00111EED File Offset: 0x001100ED
		// (set) Token: 0x060039DD RID: 14813 RVA: 0x00111EF5 File Offset: 0x001100F5
		internal virtual bool SaveOriginalValues { get; set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x00111EFE File Offset: 0x001100FE
		// (set) Token: 0x060039DF RID: 14815 RVA: 0x00111F06 File Offset: 0x00110106
		internal virtual object ChangingOldValue { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060039E0 RID: 14816 RVA: 0x00111F0F File Offset: 0x0011010F
		internal virtual bool InRelationshipFixup
		{
			get
			{
				return this._inRelationshipFixup;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x00111F17 File Offset: 0x00110117
		internal virtual ComplexTypeMaterializer ComplexTypeMaterializer
		{
			get
			{
				if (this._complexTypeMaterializer == null)
				{
					this._complexTypeMaterializer = new ComplexTypeMaterializer(this.MetadataWorkspace);
				}
				return this._complexTypeMaterializer;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x00111F38 File Offset: 0x00110138
		// (set) Token: 0x060039E3 RID: 14819 RVA: 0x00111F40 File Offset: 0x00110140
		internal virtual TransactionManager TransactionManager { get; private set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x00111F49 File Offset: 0x00110149
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x00111F51 File Offset: 0x00110151
		public virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadataWorkspace;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060039E6 RID: 14822 RVA: 0x00111F59 File Offset: 0x00110159
		// (remove) Token: 0x060039E7 RID: 14823 RVA: 0x00111F72 File Offset: 0x00110172
		public event CollectionChangeEventHandler ObjectStateManagerChanged
		{
			add
			{
				this.onObjectStateManagerChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onObjectStateManagerChangedDelegate, value);
			}
			remove
			{
				this.onObjectStateManagerChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onObjectStateManagerChangedDelegate, value);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060039E8 RID: 14824 RVA: 0x00111F8B File Offset: 0x0011018B
		// (remove) Token: 0x060039E9 RID: 14825 RVA: 0x00111FA4 File Offset: 0x001101A4
		internal event CollectionChangeEventHandler EntityDeleted
		{
			add
			{
				this.onEntityDeletedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onEntityDeletedDelegate, value);
			}
			remove
			{
				this.onEntityDeletedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onEntityDeletedDelegate, value);
			}
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x00111FBD File Offset: 0x001101BD
		internal virtual void OnObjectStateManagerChanged(CollectionChangeAction action, object entity)
		{
			if (this.onObjectStateManagerChangedDelegate != null)
			{
				this.onObjectStateManagerChangedDelegate(this, new CollectionChangeEventArgs(action, entity));
			}
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x00111FDA File Offset: 0x001101DA
		private void OnEntityDeleted(CollectionChangeAction action, object entity)
		{
			if (this.onEntityDeletedDelegate != null)
			{
				this.onEntityDeletedDelegate(this, new CollectionChangeEventArgs(action, entity));
			}
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x00111FF8 File Offset: 0x001101F8
		internal virtual EntityEntry AddKeyEntry(EntityKey entityKey, EntitySet entitySet)
		{
			EntityEntry entityEntry = this.FindEntityEntry(entityKey);
			if (entityEntry != null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(entitySet.ElementType.Name));
			}
			return this.InternalAddEntityEntry(entityKey, entitySet);
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x00112030 File Offset: 0x00110230
		internal EntityEntry GetOrAddKeyEntry(EntityKey entityKey, EntitySet entitySet)
		{
			EntityEntry result;
			if (this.TryGetEntityEntry(entityKey, out result))
			{
				return result;
			}
			return this.InternalAddEntityEntry(entityKey, entitySet);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x00112054 File Offset: 0x00110254
		private EntityEntry InternalAddEntityEntry(EntityKey entityKey, EntitySet entitySet)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(entitySet.ElementType);
			EntityEntry entityEntry = new EntityEntry(entityKey, entitySet, this, orAddStateManagerTypeMetadata);
			this.AddEntityEntryToDictionary(entityEntry, entityEntry.State);
			return entityEntry;
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x00112088 File Offset: 0x00110288
		private void ValidateProxyType(IEntityWrapper wrappedEntity)
		{
			Type identityType = wrappedEntity.IdentityType;
			Type type = wrappedEntity.Entity.GetType();
			if (identityType != type)
			{
				ClrEntityType item = this.MetadataWorkspace.GetItem<ClrEntityType>(identityType.FullNameWithNesting(), DataSpace.OSpace);
				EntityProxyTypeInfo proxyType = EntityProxyFactory.GetProxyType(item, this.MetadataWorkspace);
				if (proxyType == null || proxyType.ProxyType != type)
				{
					throw new InvalidOperationException(Strings.EntityProxyTypeInfo_DuplicateOSpaceType(identityType.FullName));
				}
			}
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x001120F4 File Offset: 0x001102F4
		internal virtual EntityEntry AddEntry(IEntityWrapper wrappedObject, EntityKey passedKey, EntitySet entitySet, string argumentName, bool isAdded)
		{
			EntityKey entityKey = passedKey;
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedObject.IdentityType, entitySet);
			this.ValidateProxyType(wrappedObject);
			EdmType edmType = orAddStateManagerTypeMetadata.CdmMetadata.EdmType;
			if (isAdded && !entitySet.ElementType.IsAssignableFrom(edmType))
			{
				throw new ArgumentException(Strings.ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(wrappedObject.Entity.GetType().Name, TypeHelpers.GetFullName(entitySet.EntityContainer.Name, entitySet.Name)), argumentName);
			}
			EntityKey entityKey2;
			if (isAdded)
			{
				entityKey2 = wrappedObject.GetEntityKeyFromEntity();
			}
			else
			{
				entityKey2 = wrappedObject.EntityKey;
			}
			if (entityKey2 != null)
			{
				entityKey = entityKey2;
				if (entityKey == null)
				{
					throw new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
				}
				if (wrappedObject.EntityKey != entityKey)
				{
					throw new InvalidOperationException(Strings.EntityKey_DoesntMatchKeyOnEntity(wrappedObject.Entity.GetType().FullName));
				}
			}
			if (entityKey != null && !entityKey.IsTemporary && !isAdded)
			{
				this.CheckKeyMatchesEntity(wrappedObject, entityKey, entitySet, false);
			}
			EntityEntry entityEntry;
			if (!isAdded || ((!(entityKey2 == null) || (entityEntry = this.FindEntityEntry(wrappedObject.Entity)) == null) && (!(entityKey2 != null) || (entityEntry = this.FindEntityEntry(entityKey2)) == null)))
			{
				if (entityKey == null || (isAdded && !entityKey.IsTemporary))
				{
					entityKey = new EntityKey(entitySet);
					wrappedObject.EntityKey = entityKey;
				}
				if (!wrappedObject.OwnsRelationshipManager)
				{
					wrappedObject.RelationshipManager.ClearRelatedEndWrappers();
				}
				EntityEntry entityEntry2 = new EntityEntry(wrappedObject, entityKey, entitySet, this, orAddStateManagerTypeMetadata, isAdded ? EntityState.Added : EntityState.Unchanged);
				entityEntry2.AttachObjectStateManagerToEntity();
				this.AddEntityEntryToDictionary(entityEntry2, entityEntry2.State);
				this.OnObjectStateManagerChanged(CollectionChangeAction.Add, entityEntry2.Entity);
				if (!isAdded)
				{
					this.FixupReferencesByForeignKeys(entityEntry2, false);
				}
				return entityEntry2;
			}
			if (entityEntry.Entity != wrappedObject.Entity)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey(wrappedObject.IdentityType.FullName));
			}
			if (entityEntry.State != EntityState.Added)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(entityEntry.State));
			}
			return null;
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x001122C8 File Offset: 0x001104C8
		internal virtual void FixupReferencesByForeignKeys(EntityEntry newEntry, bool replaceAddedRefs = false)
		{
			if (!((EntitySet)newEntry.EntitySet).HasForeignKeyRelationships)
			{
				return;
			}
			newEntry.FixupReferencesByForeignKeys(replaceAddedRefs, null);
			foreach (EntityEntry entityEntry in this.GetNonFixedupEntriesContainingForeignKey(newEntry.EntityKey))
			{
				entityEntry.FixupReferencesByForeignKeys(false, newEntry.EntitySet);
			}
			this.RemoveForeignKeyFromIndex(newEntry.EntityKey);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x00112348 File Offset: 0x00110548
		internal virtual void AddEntryContainingForeignKeyToIndex(EntityReference relatedEnd, EntityKey foreignKey, EntityEntry entry)
		{
			HashSet<Tuple<EntityReference, EntityEntry>> hashSet;
			if (!this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				hashSet = new HashSet<Tuple<EntityReference, EntityEntry>>();
				this._danglingForeignKeys.Add(foreignKey, hashSet);
			}
			hashSet.Add(Tuple.Create<EntityReference, EntityEntry>(relatedEnd, entry));
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x00112390 File Offset: 0x00110590
		[Conditional("DEBUG")]
		internal virtual void AssertEntryDoesNotExistInForeignKeyIndex(EntityEntry entry)
		{
			foreach (Tuple<EntityReference, EntityEntry> tuple in this._danglingForeignKeys.SelectMany((KeyValuePair<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>> kv) => kv.Value))
			{
				if (tuple.Item2.State != EntityState.Detached)
				{
					EntityState state = entry.State;
				}
			}
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x0011241C File Offset: 0x0011061C
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", Justification = "This method is compiled only when the compilation symbol DEBUG is defined")]
		[Conditional("DEBUG")]
		internal virtual void AssertAllForeignKeyIndexEntriesAreValid()
		{
			if (this.GetMaxEntityEntriesForDetectChanges() > 100)
			{
				return;
			}
			new HashSet<ObjectStateEntry>(this.GetObjectStateEntriesInternal(~EntityState.Detached));
			foreach (Tuple<EntityReference, EntityEntry> tuple in this._danglingForeignKeys.SelectMany((KeyValuePair<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>> kv) => kv.Value))
			{
			}
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x001124A0 File Offset: 0x001106A0
		internal virtual void RemoveEntryFromForeignKeyIndex(EntityReference relatedEnd, EntityKey foreignKey, EntityEntry entry)
		{
			HashSet<Tuple<EntityReference, EntityEntry>> hashSet;
			if (this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				hashSet.Remove(Tuple.Create<EntityReference, EntityEntry>(relatedEnd, entry));
			}
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x001124CB File Offset: 0x001106CB
		internal virtual void RemoveForeignKeyFromIndex(EntityKey foreignKey)
		{
			this._danglingForeignKeys.Remove(foreignKey);
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x001124E4 File Offset: 0x001106E4
		internal virtual IEnumerable<EntityEntry> GetNonFixedupEntriesContainingForeignKey(EntityKey foreignKey)
		{
			HashSet<Tuple<EntityReference, EntityEntry>> source;
			if (this._danglingForeignKeys.TryGetValue(foreignKey, out source))
			{
				return (from e in source
				select e.Item2).ToList<EntityEntry>();
			}
			return Enumerable.Empty<EntityEntry>();
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0011252F File Offset: 0x0011072F
		internal virtual void RememberEntryWithConceptualNull(EntityEntry entry)
		{
			if (this._entriesWithConceptualNulls == null)
			{
				this._entriesWithConceptualNulls = new HashSet<EntityEntry>();
			}
			this._entriesWithConceptualNulls.Add(entry);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x00112551 File Offset: 0x00110751
		internal virtual bool SomeEntryWithConceptualNullExists()
		{
			return this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Count != 0;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x0011256E File Offset: 0x0011076E
		internal virtual bool EntryHasConceptualNull(EntityEntry entry)
		{
			return this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Contains(entry);
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x00112588 File Offset: 0x00110788
		internal virtual void ForgetEntryWithConceptualNull(EntityEntry entry, bool resetAllKeys)
		{
			if (!entry.IsKeyEntry && this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Remove(entry) && entry.RelationshipManager.HasRelationships)
			{
				foreach (RelatedEnd relatedEnd in entry.RelationshipManager.Relationships)
				{
					EntityReference entityReference = relatedEnd as EntityReference;
					if (entityReference != null && ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
					{
						if (!resetAllKeys)
						{
							this._entriesWithConceptualNulls.Add(entry);
							break;
						}
						entityReference.SetCachedForeignKey(null, null);
					}
				}
			}
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x00112634 File Offset: 0x00110834
		internal virtual void PromoteKeyEntryInitialization(ObjectContext contextToAttach, EntityEntry keyEntry, IEntityWrapper wrappedEntity, bool replacingEntry)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedEntity.IdentityType, (EntitySet)keyEntry.EntitySet);
			this.ValidateProxyType(wrappedEntity);
			keyEntry.PromoteKeyEntry(wrappedEntity, orAddStateManagerTypeMetadata);
			this.AddEntryToKeylessStore(keyEntry);
			if (replacingEntry)
			{
				wrappedEntity.SetChangeTracker(null);
			}
			wrappedEntity.SetChangeTracker(keyEntry);
			if (contextToAttach != null)
			{
				wrappedEntity.AttachContext(contextToAttach, (EntitySet)keyEntry.EntitySet, MergeOption.AppendOnly);
			}
			wrappedEntity.TakeSnapshot(keyEntry);
			this.OnObjectStateManagerChanged(CollectionChangeAction.Add, keyEntry.Entity);
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x001126AC File Offset: 0x001108AC
		internal virtual void PromoteKeyEntry(EntityEntry keyEntry, IEntityWrapper wrappedEntity, bool replacingEntry, bool setIsLoaded, bool keyEntryInitialized)
		{
			if (!keyEntryInitialized)
			{
				this.PromoteKeyEntryInitialization(null, keyEntry, wrappedEntity, replacingEntry);
			}
			bool flag = true;
			try
			{
				foreach (RelationshipEntry relationshipEntry in this.CopyOfRelationshipsByKey(keyEntry.EntityKey))
				{
					if (relationshipEntry.State != EntityState.Deleted)
					{
						AssociationEndMember associationEndMember = keyEntry.GetAssociationEndMember(relationshipEntry);
						AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(associationEndMember);
						EntityEntry otherEndOfRelationship = keyEntry.GetOtherEndOfRelationship(relationshipEntry);
						ObjectStateManager.AddEntityToCollectionOrReference(MergeOption.AppendOnly, wrappedEntity, associationEndMember, otherEndOfRelationship.WrappedEntity, otherAssociationEnd, setIsLoaded, true, true);
					}
				}
				this.FixupReferencesByForeignKeys(keyEntry, false);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					keyEntry.DetachObjectStateManagerFromEntity();
					this.RemoveEntryFromKeylessStore(wrappedEntity);
					keyEntry.DegradeEntry();
				}
			}
			if (this.TransactionManager.IsAttachTracking)
			{
				this.TransactionManager.PromotedKeyEntries.Add(wrappedEntity.Entity, keyEntry);
			}
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x0011277C File Offset: 0x0011097C
		internal virtual void TrackPromotedRelationship(RelatedEnd relatedEnd, IEntityWrapper wrappedEntity)
		{
			IList<IEntityWrapper> list;
			if (!this.TransactionManager.PromotedRelationships.TryGetValue(relatedEnd, out list))
			{
				list = new List<IEntityWrapper>();
				this.TransactionManager.PromotedRelationships.Add(relatedEnd, list);
			}
			list.Add(wrappedEntity);
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x001127C0 File Offset: 0x001109C0
		internal virtual void DegradePromotedRelationships()
		{
			foreach (KeyValuePair<RelatedEnd, IList<IEntityWrapper>> keyValuePair in this.TransactionManager.PromotedRelationships)
			{
				foreach (IEntityWrapper entityWrapper in keyValuePair.Value)
				{
					if (keyValuePair.Key.RemoveFromCache(entityWrapper, false, false))
					{
						keyValuePair.Key.OnAssociationChanged(CollectionChangeAction.Remove, entityWrapper.Entity);
					}
				}
			}
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0011286C File Offset: 0x00110A6C
		internal static void AddEntityToCollectionOrReference(MergeOption mergeOption, IEntityWrapper wrappedSource, AssociationEndMember sourceMember, IEntityWrapper wrappedTarget, AssociationEndMember targetMember, bool setIsLoaded, bool relationshipAlreadyExists, bool inKeyEntryPromotion)
		{
			RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
			if (targetMember.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				EntityReference entityReference = (EntityReference)relatedEndInternal;
				switch (mergeOption)
				{
				case MergeOption.AppendOnly:
					if (inKeyEntryPromotion && !entityReference.IsEmpty() && !object.ReferenceEquals(entityReference.ReferenceValue.Entity, wrappedTarget.Entity))
					{
						throw new InvalidOperationException(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
					}
					break;
				case MergeOption.OverwriteChanges:
				case MergeOption.PreserveChanges:
				{
					IEntityWrapper referenceValue = entityReference.ReferenceValue;
					if (referenceValue != null && referenceValue.Entity != null && referenceValue != wrappedTarget)
					{
						RelationshipEntry relationshipEntry = relatedEndInternal.FindRelationshipEntryInObjectStateManager(referenceValue);
						relatedEndInternal.RemoveAll();
						if (relationshipEntry != null && relationshipEntry.State == EntityState.Deleted)
						{
							relationshipEntry.AcceptChanges();
						}
					}
					break;
				}
				}
			}
			RelatedEnd relatedEnd = null;
			if (mergeOption == MergeOption.NoTracking)
			{
				relatedEnd = relatedEndInternal.GetOtherEndOfRelationship(wrappedTarget);
				if (relatedEnd.IsLoaded)
				{
					throw new InvalidOperationException(Strings.Collections_CannotFillTryDifferentMergeOption(relatedEnd.SourceRoleName, relatedEnd.RelationshipName));
				}
			}
			if (relatedEnd == null)
			{
				relatedEnd = relatedEndInternal.GetOtherEndOfRelationship(wrappedTarget);
			}
			relatedEndInternal.Add(wrappedTarget, true, true, relationshipAlreadyExists, true, true);
			ObjectStateManager.UpdateRelatedEnd(relatedEndInternal, wrappedTarget, setIsLoaded, mergeOption);
			ObjectStateManager.UpdateRelatedEnd(relatedEnd, wrappedSource, setIsLoaded, mergeOption);
			if (inKeyEntryPromotion && wrappedSource.Context.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				wrappedSource.Context.ObjectStateManager.TrackPromotedRelationship(relatedEndInternal, wrappedTarget);
				wrappedSource.Context.ObjectStateManager.TrackPromotedRelationship(relatedEnd, wrappedSource);
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x001129D0 File Offset: 0x00110BD0
		private static void UpdateRelatedEnd(RelatedEnd relatedEnd, IEntityWrapper wrappedRelatedEntity, bool setIsLoaded, MergeOption mergeOption)
		{
			AssociationEndMember associationEndMember = (AssociationEndMember)relatedEnd.ToEndMember;
			if (associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One || associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				if (setIsLoaded)
				{
					relatedEnd.IsLoaded = true;
				}
				if (mergeOption == MergeOption.NoTracking)
				{
					EntityKey entityKey = wrappedRelatedEntity.EntityKey;
					if (entityKey == null)
					{
						throw new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
					}
					((EntityReference)relatedEnd).DetachedEntityKey = entityKey;
				}
			}
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x00112A2C File Offset: 0x00110C2C
		internal virtual int UpdateRelationships(ObjectContext context, MergeOption mergeOption, AssociationSet associationSet, AssociationEndMember sourceMember, IEntityWrapper wrappedSource, AssociationEndMember targetMember, IList targets, bool setIsLoaded)
		{
			int num = 0;
			EntityKey entityKey = wrappedSource.EntityKey;
			context.ObjectStateManager.TransactionManager.BeginGraphUpdate();
			try
			{
				if (targets != null)
				{
					if (mergeOption == MergeOption.NoTracking)
					{
						RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
						if (!relatedEndInternal.IsEmpty())
						{
							throw new InvalidOperationException(Strings.Collections_CannotFillTryDifferentMergeOption(relatedEndInternal.SourceRoleName, relatedEndInternal.RelationshipName));
						}
					}
					foreach (object obj in targets)
					{
						IEntityWrapper entityWrapper = obj as IEntityWrapper;
						if (entityWrapper == null)
						{
							entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(obj, context);
						}
						num++;
						if (mergeOption == MergeOption.NoTracking)
						{
							ObjectStateManager.AddEntityToCollectionOrReference(MergeOption.NoTracking, wrappedSource, sourceMember, entityWrapper, targetMember, setIsLoaded, true, false);
						}
						else
						{
							ObjectStateManager objectStateManager = context.ObjectStateManager;
							EntityKey entityKey2 = entityWrapper.EntityKey;
							EntityState entityState;
							if (!ObjectStateManager.TryUpdateExistingRelationships(context, mergeOption, associationSet, sourceMember, entityKey, wrappedSource, targetMember, entityKey2, setIsLoaded, out entityState))
							{
								bool flag = true;
								switch (sourceMember.RelationshipMultiplicity)
								{
								case RelationshipMultiplicity.ZeroOrOne:
								case RelationshipMultiplicity.One:
									flag = !ObjectStateManager.TryUpdateExistingRelationships(context, mergeOption, associationSet, targetMember, entityKey2, entityWrapper, sourceMember, entityKey, setIsLoaded, out entityState);
									break;
								}
								if (flag)
								{
									if (entityState != EntityState.Deleted)
									{
										ObjectStateManager.AddEntityToCollectionOrReference(mergeOption, wrappedSource, sourceMember, entityWrapper, targetMember, setIsLoaded, false, false);
									}
									else
									{
										RelationshipWrapper wrapper = new RelationshipWrapper(associationSet, sourceMember.Name, entityKey, targetMember.Name, entityKey2);
										objectStateManager.AddNewRelation(wrapper, EntityState.Deleted);
									}
								}
							}
						}
					}
				}
				if (num == 0)
				{
					ObjectStateManager.EnsureCollectionNotNull(sourceMember, wrappedSource, targetMember);
				}
			}
			finally
			{
				context.ObjectStateManager.TransactionManager.EndGraphUpdate();
			}
			return num;
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x00112C0C File Offset: 0x00110E0C
		private static void EnsureCollectionNotNull(AssociationEndMember sourceMember, IEntityWrapper wrappedSource, AssociationEndMember targetMember)
		{
			RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
			AssociationEndMember associationEndMember = (AssociationEndMember)relatedEndInternal.ToEndMember;
			if (associationEndMember != null && associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many && relatedEndInternal.TargetAccessor.HasProperty)
			{
				wrappedSource.EnsureCollectionNotNull(relatedEndInternal);
			}
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x00112C64 File Offset: 0x00110E64
		internal virtual void RemoveRelationships(MergeOption mergeOption, AssociationSet associationSet, EntityKey sourceKey, AssociationEndMember sourceMember)
		{
			List<RelationshipEntry> list = new List<RelationshipEntry>(16);
			if (mergeOption == MergeOption.OverwriteChanges)
			{
				using (EntityEntry.RelationshipEndEnumerator enumerator = this.FindRelationshipsByKey(sourceKey).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RelationshipEntry relationshipEntry = enumerator.Current;
						if (relationshipEntry.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey))
						{
							list.Add(relationshipEntry);
						}
					}
					goto IL_A9;
				}
			}
			if (mergeOption == MergeOption.PreserveChanges)
			{
				foreach (RelationshipEntry relationshipEntry2 in this.FindRelationshipsByKey(sourceKey))
				{
					if (relationshipEntry2.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey) && relationshipEntry2.State != EntityState.Added)
					{
						list.Add(relationshipEntry2);
					}
				}
			}
			IL_A9:
			foreach (RelationshipEntry relationshipToRemove in list)
			{
				ObjectStateManager.RemoveRelatedEndsAndDetachRelationship(relationshipToRemove, true);
			}
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x00112D74 File Offset: 0x00110F74
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal static bool TryUpdateExistingRelationships(ObjectContext context, MergeOption mergeOption, AssociationSet associationSet, AssociationEndMember sourceMember, EntityKey sourceKey, IEntityWrapper wrappedSource, AssociationEndMember targetMember, EntityKey targetKey, bool setIsLoaded, out EntityState newEntryState)
		{
			newEntryState = EntityState.Unchanged;
			if (associationSet.ElementType.IsForeignKey)
			{
				return true;
			}
			bool flag = true;
			ObjectStateManager objectStateManager = context.ObjectStateManager;
			List<RelationshipEntry> list = null;
			List<RelationshipEntry> list2 = null;
			foreach (RelationshipEntry relationshipEntry in objectStateManager.FindRelationshipsByKey(sourceKey))
			{
				if (relationshipEntry.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey))
				{
					if (targetKey == relationshipEntry.RelationshipWrapper.GetOtherEntityKey(sourceKey))
					{
						if (list2 == null)
						{
							list2 = new List<RelationshipEntry>(16);
						}
						list2.Add(relationshipEntry);
					}
					else
					{
						switch (targetMember.RelationshipMultiplicity)
						{
						case RelationshipMultiplicity.ZeroOrOne:
						case RelationshipMultiplicity.One:
							switch (mergeOption)
							{
							case MergeOption.AppendOnly:
								if (relationshipEntry.State != EntityState.Deleted)
								{
									flag = false;
								}
								break;
							case MergeOption.OverwriteChanges:
								if (list == null)
								{
									list = new List<RelationshipEntry>(16);
								}
								list.Add(relationshipEntry);
								break;
							case MergeOption.PreserveChanges:
							{
								EntityState state = relationshipEntry.State;
								switch (state)
								{
								case EntityState.Unchanged:
									if (list == null)
									{
										list = new List<RelationshipEntry>(16);
									}
									list.Add(relationshipEntry);
									break;
								case EntityState.Detached | EntityState.Unchanged:
									break;
								case EntityState.Added:
									newEntryState = EntityState.Deleted;
									break;
								default:
									if (state == EntityState.Deleted)
									{
										newEntryState = EntityState.Deleted;
										if (list == null)
										{
											list = new List<RelationshipEntry>(16);
										}
										list.Add(relationshipEntry);
									}
									break;
								}
								break;
							}
							}
							break;
						}
					}
				}
			}
			if (list != null)
			{
				foreach (RelationshipEntry relationshipEntry2 in list)
				{
					if (relationshipEntry2.State != EntityState.Detached)
					{
						ObjectStateManager.RemoveRelatedEndsAndDetachRelationship(relationshipEntry2, setIsLoaded);
					}
				}
			}
			if (list2 != null)
			{
				foreach (RelationshipEntry relationshipEntry3 in list2)
				{
					flag = false;
					switch (mergeOption)
					{
					case MergeOption.OverwriteChanges:
						if (relationshipEntry3.State == EntityState.Added)
						{
							relationshipEntry3.AcceptChanges();
						}
						else if (relationshipEntry3.State == EntityState.Deleted)
						{
							EntityEntry entityEntry = objectStateManager.GetEntityEntry(targetKey);
							if (entityEntry.State != EntityState.Deleted)
							{
								if (!entityEntry.IsKeyEntry)
								{
									ObjectStateManager.AddEntityToCollectionOrReference(mergeOption, wrappedSource, sourceMember, entityEntry.WrappedEntity, targetMember, setIsLoaded, true, false);
								}
								relationshipEntry3.RevertDelete();
							}
						}
						break;
					case MergeOption.PreserveChanges:
						if (relationshipEntry3.State == EntityState.Added)
						{
							relationshipEntry3.AcceptChanges();
						}
						break;
					}
				}
			}
			return !flag;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x00113020 File Offset: 0x00111220
		internal static void RemoveRelatedEndsAndDetachRelationship(RelationshipEntry relationshipToRemove, bool setIsLoaded)
		{
			if (setIsLoaded)
			{
				ObjectStateManager.UnloadReferenceRelatedEnds(relationshipToRemove);
			}
			if (relationshipToRemove.State != EntityState.Deleted)
			{
				relationshipToRemove.Delete();
			}
			if (relationshipToRemove.State != EntityState.Detached)
			{
				relationshipToRemove.AcceptChanges();
			}
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x0011304C File Offset: 0x0011124C
		private static void UnloadReferenceRelatedEnds(RelationshipEntry relationshipEntry)
		{
			ObjectStateManager objectStateManager = relationshipEntry.ObjectStateManager;
			ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = relationshipEntry.RelationshipWrapper.AssociationEndMembers;
			ObjectStateManager.UnloadReferenceRelatedEnds(objectStateManager, relationshipEntry, relationshipEntry.RelationshipWrapper.GetEntityKey(0), associationEndMembers[1].Name);
			ObjectStateManager.UnloadReferenceRelatedEnds(objectStateManager, relationshipEntry, relationshipEntry.RelationshipWrapper.GetEntityKey(1), associationEndMembers[0].Name);
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x001130AC File Offset: 0x001112AC
		private static void UnloadReferenceRelatedEnds(ObjectStateManager cache, RelationshipEntry relationshipEntry, EntityKey sourceEntityKey, string targetRoleName)
		{
			EntityEntry entityEntry = cache.GetEntityEntry(sourceEntityKey);
			if (entityEntry.WrappedEntity.Entity != null)
			{
				EntityReference entityReference = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEndInternal(((AssociationSet)relationshipEntry.EntitySet).ElementType.FullName, targetRoleName) as EntityReference;
				if (entityReference != null)
				{
					entityReference.IsLoaded = false;
				}
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x00113104 File Offset: 0x00111304
		internal virtual EntityEntry AttachEntry(EntityKey entityKey, IEntityWrapper wrappedObject, EntitySet entitySet)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedObject.IdentityType, entitySet);
			this.ValidateProxyType(wrappedObject);
			this.CheckKeyMatchesEntity(wrappedObject, entityKey, entitySet, true);
			if (!wrappedObject.OwnsRelationshipManager)
			{
				wrappedObject.RelationshipManager.ClearRelatedEndWrappers();
			}
			EntityEntry entityEntry = new EntityEntry(wrappedObject, entityKey, entitySet, this, orAddStateManagerTypeMetadata, EntityState.Unchanged);
			entityEntry.AttachObjectStateManagerToEntity();
			this.AddEntityEntryToDictionary(entityEntry, entityEntry.State);
			this.OnObjectStateManagerChanged(CollectionChangeAction.Add, entityEntry.Entity);
			return entityEntry;
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x00113170 File Offset: 0x00111370
		private void CheckKeyMatchesEntity(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySetForType, bool forAttach)
		{
			EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
			if (entitySet == null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_InvalidKey);
			}
			entityKey.ValidateEntityKey(this._metadataWorkspace, entitySet);
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedEntity.IdentityType, entitySetForType);
			for (int i = 0; i < entitySet.ElementType.KeyMembers.Count; i++)
			{
				EdmMember edmMember = entitySet.ElementType.KeyMembers[i];
				int ordinalforCLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(edmMember.Name);
				if (ordinalforCLayerMemberName < 0)
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_InvalidKey);
				}
				object value = orAddStateManagerTypeMetadata.Member(ordinalforCLayerMemberName).GetValue(wrappedEntity.Entity);
				object y = entityKey.FindValueByName(edmMember.Name);
				if (!ByValueEqualityComparer.Default.Equals(value, y))
				{
					throw new InvalidOperationException(forAttach ? Strings.ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach : Strings.ObjectStateManager_KeyPropertyDoesntMatchValueInKey);
				}
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x0011324C File Offset: 0x0011144C
		internal virtual RelationshipEntry AddNewRelation(RelationshipWrapper wrapper, EntityState desiredState)
		{
			RelationshipEntry relationshipEntry = new RelationshipEntry(this, desiredState, wrapper);
			this.AddRelationshipEntryToDictionary(relationshipEntry, desiredState);
			this.AddRelationshipToLookup(relationshipEntry);
			return relationshipEntry;
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x00113274 File Offset: 0x00111474
		internal virtual RelationshipEntry AddRelation(RelationshipWrapper wrapper, EntityState desiredState)
		{
			RelationshipEntry relationshipEntry = this.FindRelationship(wrapper);
			if (relationshipEntry == null)
			{
				relationshipEntry = this.AddNewRelation(wrapper, desiredState);
			}
			else if (EntityState.Deleted != relationshipEntry.State)
			{
				if (EntityState.Unchanged == desiredState)
				{
					relationshipEntry.AcceptChanges();
				}
				else if (EntityState.Deleted == desiredState)
				{
					relationshipEntry.AcceptChanges();
					relationshipEntry.Delete(false);
				}
			}
			else if (EntityState.Deleted != desiredState)
			{
				relationshipEntry.RevertDelete();
			}
			return relationshipEntry;
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x001132CC File Offset: 0x001114CC
		private void AddRelationshipToLookup(RelationshipEntry relationship)
		{
			this.AddRelationshipEndToLookup(relationship.RelationshipWrapper.Key0, relationship);
			if (!relationship.RelationshipWrapper.Key0.Equals(relationship.RelationshipWrapper.Key1))
			{
				this.AddRelationshipEndToLookup(relationship.RelationshipWrapper.Key1, relationship);
			}
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x0011331C File Offset: 0x0011151C
		private void AddRelationshipEndToLookup(EntityKey key, RelationshipEntry relationship)
		{
			EntityEntry entityEntry = this.GetEntityEntry(key);
			entityEntry.AddRelationshipEnd(relationship);
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x00113338 File Offset: 0x00111538
		private void DeleteRelationshipFromLookup(RelationshipEntry relationship)
		{
			this.DeleteRelationshipEndFromLookup(relationship.RelationshipWrapper.Key0, relationship);
			if (!relationship.RelationshipWrapper.Key0.Equals(relationship.RelationshipWrapper.Key1))
			{
				this.DeleteRelationshipEndFromLookup(relationship.RelationshipWrapper.Key1, relationship);
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x00113388 File Offset: 0x00111588
		private void DeleteRelationshipEndFromLookup(EntityKey key, RelationshipEntry relationship)
		{
			EntityEntry entityEntry = this.GetEntityEntry(key);
			entityEntry.RemoveRelationshipEnd(relationship);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x001133A4 File Offset: 0x001115A4
		internal virtual RelationshipEntry FindRelationship(RelationshipSet relationshipSet, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
		{
			if (roleAndKey1.Value == null || roleAndKey2.Value == null)
			{
				return null;
			}
			return this.FindRelationship(new RelationshipWrapper((AssociationSet)relationshipSet, roleAndKey1, roleAndKey2));
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x001133D0 File Offset: 0x001115D0
		internal virtual RelationshipEntry FindRelationship(RelationshipWrapper relationshipWrapper)
		{
			RelationshipEntry result = null;
			if ((this._unchangedRelationshipStore == null || !this._unchangedRelationshipStore.TryGetValue(relationshipWrapper, out result)) && (this._deletedRelationshipStore == null || !this._deletedRelationshipStore.TryGetValue(relationshipWrapper, out result)) && this._addedRelationshipStore != null)
			{
				this._addedRelationshipStore.TryGetValue(relationshipWrapper, out result);
			}
			return result;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x00113428 File Offset: 0x00111628
		internal virtual RelationshipEntry DeleteRelationship(RelationshipSet relationshipSet, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
		{
			RelationshipEntry relationshipEntry = this.FindRelationship(relationshipSet, roleAndKey1, roleAndKey2);
			if (relationshipEntry != null)
			{
				relationshipEntry.Delete(false);
			}
			return relationshipEntry;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x0011344A File Offset: 0x0011164A
		internal virtual void DeleteKeyEntry(EntityEntry keyEntry)
		{
			if (keyEntry != null && keyEntry.IsKeyEntry)
			{
				this.ChangeState(keyEntry, keyEntry.State, EntityState.Detached);
			}
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x00113468 File Offset: 0x00111668
		internal virtual RelationshipEntry[] CopyOfRelationshipsByKey(EntityKey key)
		{
			return this.FindRelationshipsByKey(key).ToArray();
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x00113484 File Offset: 0x00111684
		internal virtual EntityEntry.RelationshipEndEnumerable FindRelationshipsByKey(EntityKey key)
		{
			return new EntityEntry.RelationshipEndEnumerable(this.FindEntityEntry(key));
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x00113492 File Offset: 0x00111692
		IEnumerable<IEntityStateEntry> IEntityStateManager.FindRelationshipsByKey(EntityKey key)
		{
			return this.FindRelationshipsByKey(key);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x001134B0 File Offset: 0x001116B0
		[Conditional("DEBUG")]
		private void ValidateKeylessEntityStore()
		{
			Dictionary<EntityKey, EntityEntry>[] array = new Dictionary<EntityKey, EntityEntry>[]
			{
				this._unchangedEntityStore,
				this._modifiedEntityStore,
				this._addedEntityStore,
				this._deletedEntityStore
			};
			if (this._keylessEntityStore != null)
			{
				if (this._keylessEntityStore.Count == array.Sum(delegate(Dictionary<EntityKey, EntityEntry> s)
				{
					if (s != null)
					{
						return s.Count;
					}
					return 0;
				}))
				{
					return;
				}
			}
			if (this._keylessEntityStore != null)
			{
				foreach (EntityEntry entityEntry in this._keylessEntityStore.Values)
				{
					bool flag = false;
					if (this._addedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag = this._addedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
					if (this._modifiedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag |= this._modifiedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
					if (this._deletedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag |= this._deletedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
					if (this._unchangedEntityStore != null)
					{
						EntityEntry entityEntry2;
						flag |= this._unchangedEntityStore.TryGetValue(entityEntry.EntityKey, out entityEntry2);
					}
				}
			}
			foreach (Dictionary<EntityKey, EntityEntry> dictionary in array)
			{
				if (dictionary != null)
				{
					foreach (EntityEntry entityEntry3 in dictionary.Values)
					{
						if (entityEntry3.Entity != null && !(entityEntry3.Entity is IEntityWithKey))
						{
							EntityEntry entityEntry4;
							this._keylessEntityStore.TryGetValue(entityEntry3.Entity, out entityEntry4);
						}
					}
				}
			}
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x00113684 File Offset: 0x00111884
		private bool TryGetEntryFromKeylessStore(object entity, out EntityEntry entryRef)
		{
			entryRef = null;
			if (entity == null)
			{
				return false;
			}
			if (this._keylessEntityStore != null && this._keylessEntityStore.TryGetValue(entity, out entryRef))
			{
				return true;
			}
			entryRef = null;
			return false;
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x001136AB File Offset: 0x001118AB
		public virtual IEnumerable<ObjectStateEntry> GetObjectStateEntries(EntityState state)
		{
			if ((EntityState.Detached & state) != (EntityState)0)
			{
				throw new ArgumentException(Strings.ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager);
			}
			return this.GetObjectStateEntriesInternal(state);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x00113860 File Offset: 0x00111A60
		IEnumerable<IEntityStateEntry> IEntityStateManager.GetEntityStateEntries(EntityState state)
		{
			foreach (ObjectStateEntry stateEntry in this.GetObjectStateEntriesInternal(state))
			{
				yield return stateEntry;
			}
			yield break;
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x00113884 File Offset: 0x00111A84
		internal virtual bool HasChanges()
		{
			return (this._addedRelationshipStore != null && this._addedRelationshipStore.Count > 0) || (this._addedEntityStore != null && this._addedEntityStore.Count > 0) || (this._modifiedEntityStore != null && this._modifiedEntityStore.Count > 0) || (this._deletedRelationshipStore != null && this._deletedRelationshipStore.Count > 0) || (this._deletedEntityStore != null && this._deletedEntityStore.Count > 0);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x00113904 File Offset: 0x00111B04
		internal virtual int GetObjectStateEntriesCount(EntityState state)
		{
			int num = 0;
			if ((EntityState.Added & state) != (EntityState)0)
			{
				num += ((this._addedRelationshipStore != null) ? this._addedRelationshipStore.Count : 0);
				num += ((this._addedEntityStore != null) ? this._addedEntityStore.Count : 0);
			}
			if ((EntityState.Modified & state) != (EntityState)0)
			{
				num += ((this._modifiedEntityStore != null) ? this._modifiedEntityStore.Count : 0);
			}
			if ((EntityState.Deleted & state) != (EntityState)0)
			{
				num += ((this._deletedRelationshipStore != null) ? this._deletedRelationshipStore.Count : 0);
				num += ((this._deletedEntityStore != null) ? this._deletedEntityStore.Count : 0);
			}
			if ((EntityState.Unchanged & state) != (EntityState)0)
			{
				num += ((this._unchangedRelationshipStore != null) ? this._unchangedRelationshipStore.Count : 0);
				num += ((this._unchangedEntityStore != null) ? this._unchangedEntityStore.Count : 0);
			}
			return num;
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x001139D8 File Offset: 0x00111BD8
		private int GetMaxEntityEntriesForDetectChanges()
		{
			int num = 0;
			if (this._addedEntityStore != null)
			{
				num += this._addedEntityStore.Count;
			}
			if (this._modifiedEntityStore != null)
			{
				num += this._modifiedEntityStore.Count;
			}
			if (this._deletedEntityStore != null)
			{
				num += this._deletedEntityStore.Count;
			}
			if (this._unchangedEntityStore != null)
			{
				num += this._unchangedEntityStore.Count;
			}
			return num;
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x00113A40 File Offset: 0x00111C40
		internal virtual IEnumerable<ObjectStateEntry> GetObjectStateEntriesInternal(EntityState state)
		{
			int num = this.GetObjectStateEntriesCount(state);
			ObjectStateEntry[] array = new ObjectStateEntry[num];
			num = 0;
			if ((EntityState.Added & state) != (EntityState)0 && this._addedRelationshipStore != null)
			{
				foreach (KeyValuePair<RelationshipWrapper, RelationshipEntry> keyValuePair in this._addedRelationshipStore)
				{
					array[num++] = keyValuePair.Value;
				}
			}
			if ((EntityState.Deleted & state) != (EntityState)0 && this._deletedRelationshipStore != null)
			{
				foreach (KeyValuePair<RelationshipWrapper, RelationshipEntry> keyValuePair2 in this._deletedRelationshipStore)
				{
					array[num++] = keyValuePair2.Value;
				}
			}
			if ((EntityState.Unchanged & state) != (EntityState)0 && this._unchangedRelationshipStore != null)
			{
				foreach (KeyValuePair<RelationshipWrapper, RelationshipEntry> keyValuePair3 in this._unchangedRelationshipStore)
				{
					array[num++] = keyValuePair3.Value;
				}
			}
			if ((EntityState.Added & state) != (EntityState)0 && this._addedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair4 in this._addedEntityStore)
				{
					array[num++] = keyValuePair4.Value;
				}
			}
			if ((EntityState.Modified & state) != (EntityState)0 && this._modifiedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair5 in this._modifiedEntityStore)
				{
					array[num++] = keyValuePair5.Value;
				}
			}
			if ((EntityState.Deleted & state) != (EntityState)0 && this._deletedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair6 in this._deletedEntityStore)
				{
					array[num++] = keyValuePair6.Value;
				}
			}
			if ((EntityState.Unchanged & state) != (EntityState)0 && this._unchangedEntityStore != null)
			{
				foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair7 in this._unchangedEntityStore)
				{
					array[num++] = keyValuePair7.Value;
				}
			}
			return array;
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x00113CCC File Offset: 0x00111ECC
		private IList<EntityEntry> GetEntityEntriesForDetectChanges()
		{
			if (!this._detectChangesNeeded)
			{
				return null;
			}
			List<EntityEntry> list = null;
			this.GetEntityEntriesForDetectChanges(this._addedEntityStore, ref list);
			this.GetEntityEntriesForDetectChanges(this._modifiedEntityStore, ref list);
			this.GetEntityEntriesForDetectChanges(this._deletedEntityStore, ref list);
			this.GetEntityEntriesForDetectChanges(this._unchangedEntityStore, ref list);
			if (list == null)
			{
				this._detectChangesNeeded = false;
			}
			return list;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x00113D28 File Offset: 0x00111F28
		private void GetEntityEntriesForDetectChanges(Dictionary<EntityKey, EntityEntry> entityStore, ref List<EntityEntry> entries)
		{
			if (entityStore != null)
			{
				foreach (EntityEntry entityEntry in entityStore.Values)
				{
					if (entityEntry.RequiresAnyChangeTracking)
					{
						if (entries == null)
						{
							entries = new List<EntityEntry>(this.GetMaxEntityEntriesForDetectChanges());
						}
						entries.Add(entityEntry);
					}
				}
			}
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x00113D98 File Offset: 0x00111F98
		internal virtual void FixupKey(EntityEntry entry)
		{
			EntityKey entityKey = entry.EntityKey;
			EntitySet entitySet = (EntitySet)entry.EntitySet;
			bool hasForeignKeyRelationships = entitySet.HasForeignKeyRelationships;
			bool hasIndependentRelationships = entitySet.HasIndependentRelationships;
			if (hasForeignKeyRelationships)
			{
				entry.FixupForeignKeysByReference();
			}
			EntityKey entityKey2;
			try
			{
				entityKey2 = new EntityKey((EntitySet)entry.EntitySet, entry.CurrentValues);
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException(Strings.ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid, innerException);
			}
			EntityEntry entityEntry = this.FindEntityEntry(entityKey2);
			if (entityEntry != null)
			{
				if (!entityEntry.IsKeyEntry)
				{
					throw new InvalidOperationException(Strings.ObjectStateManager_CannotFixUpKeyToExistingValues(entry.WrappedEntity.IdentityType.FullName));
				}
				entityKey2 = entityEntry.EntityKey;
			}
			RelationshipEntry[] array = null;
			if (hasIndependentRelationships)
			{
				array = entry.GetRelationshipEnds().ToArray();
				foreach (RelationshipEntry relationshipEntry in array)
				{
					this.RemoveObjectStateEntryFromDictionary(relationshipEntry, relationshipEntry.State);
				}
			}
			this.RemoveObjectStateEntryFromDictionary(entry, EntityState.Added);
			this.ResetEntityKey(entry, entityKey2);
			if (hasIndependentRelationships)
			{
				entry.UpdateRelationshipEnds(entityKey, entityEntry);
				foreach (RelationshipEntry relationshipEntry2 in array)
				{
					this.AddRelationshipEntryToDictionary(relationshipEntry2, relationshipEntry2.State);
				}
			}
			if (entityEntry != null)
			{
				this.PromoteKeyEntry(entityEntry, entry.WrappedEntity, true, false, false);
				entry = entityEntry;
			}
			else
			{
				this.AddEntityEntryToDictionary(entry, EntityState.Unchanged);
			}
			if (hasForeignKeyRelationships)
			{
				this.FixupReferencesByForeignKeys(entry, false);
			}
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x00113F00 File Offset: 0x00112100
		internal virtual void ReplaceKeyWithTemporaryKey(EntityEntry entry)
		{
			EntityKey entityKey = entry.EntityKey;
			EntityKey value = new EntityKey(entry.EntitySet);
			RelationshipEntry[] array = entry.GetRelationshipEnds().ToArray();
			foreach (RelationshipEntry relationshipEntry in array)
			{
				this.RemoveObjectStateEntryFromDictionary(relationshipEntry, relationshipEntry.State);
			}
			this.RemoveObjectStateEntryFromDictionary(entry, entry.State);
			this.ResetEntityKey(entry, value);
			entry.UpdateRelationshipEnds(entityKey, null);
			foreach (RelationshipEntry relationshipEntry2 in array)
			{
				this.AddRelationshipEntryToDictionary(relationshipEntry2, relationshipEntry2.State);
			}
			this.AddEntityEntryToDictionary(entry, EntityState.Added);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x00113FAC File Offset: 0x001121AC
		private void ResetEntityKey(EntityEntry entry, EntityKey value)
		{
			EntityKey entityKey = entry.WrappedEntity.EntityKey;
			if (entityKey == null || value.Equals(entityKey))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_AcceptChangesEntityKeyIsNotValid);
			}
			try
			{
				this._inRelationshipFixup = true;
				entry.WrappedEntity.EntityKey = value;
				IEntityWrapper wrappedEntity = entry.WrappedEntity;
				if (wrappedEntity.EntityKey != value)
				{
					throw new InvalidOperationException(Strings.EntityKey_DoesntMatchKeyOnEntity(wrappedEntity.Entity.GetType().FullName));
				}
			}
			finally
			{
				this._inRelationshipFixup = false;
			}
			entry.EntityKey = value;
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x00114048 File Offset: 0x00112248
		public virtual ObjectStateEntry ChangeObjectState(object entity, EntityState entityState)
		{
			Check.NotNull<object>(entity, "entity");
			EntityUtil.CheckValidStateForChangeEntityState(entityState);
			EntityEntry entityEntry = null;
			this.TransactionManager.BeginLocalPublicAPI();
			try
			{
				EntityKey entityKey = entity as EntityKey;
				entityEntry = ((entityKey != null) ? this.FindEntityEntry(entityKey) : this.FindEntityEntry(entity));
				if (entityEntry == null)
				{
					if (entityState == EntityState.Detached)
					{
						return null;
					}
					throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
				}
				else
				{
					entityEntry.ChangeObjectState(entityState);
				}
			}
			finally
			{
				this.TransactionManager.EndLocalPublicAPI();
			}
			return entityEntry;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x001140E0 File Offset: 0x001122E0
		public virtual ObjectStateEntry ChangeRelationshipState(object sourceEntity, object targetEntity, string navigationProperty, EntityState relationshipState)
		{
			EntityEntry entityEntry;
			EntityEntry targetEntry;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out targetEntry);
			Check.NotEmpty(navigationProperty, "navigationProperty");
			RelatedEnd relatedEnd = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEnd(navigationProperty, false);
			return this.ChangeRelationshipState(entityEntry, targetEntry, relatedEnd, relationshipState);
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x00114124 File Offset: 0x00112324
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual ObjectStateEntry ChangeRelationshipState<TEntity>(TEntity sourceEntity, object targetEntity, Expression<Func<TEntity, object>> navigationPropertySelector, EntityState relationshipState) where TEntity : class
		{
			EntityEntry entityEntry;
			EntityEntry targetEntry;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out targetEntry);
			bool throwArgumentException;
			string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(navigationPropertySelector, out throwArgumentException);
			RelatedEnd relatedEnd = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEnd(navigationProperty, throwArgumentException);
			return this.ChangeRelationshipState(entityEntry, targetEntry, relatedEnd, relationshipState);
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x0011416C File Offset: 0x0011236C
		public virtual ObjectStateEntry ChangeRelationshipState(object sourceEntity, object targetEntity, string relationshipName, string targetRoleName, EntityState relationshipState)
		{
			EntityEntry entityEntry;
			EntityEntry targetEntry;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out targetEntry);
			RelatedEnd relatedEndInternal = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEndInternal(relationshipName, targetRoleName);
			return this.ChangeRelationshipState(entityEntry, targetEntry, relatedEndInternal, relationshipState);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x001141A4 File Offset: 0x001123A4
		private ObjectStateEntry ChangeRelationshipState(EntityEntry sourceEntry, EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState relationshipState)
		{
			ObjectStateManager.VerifyInitialStateForChangeRelationshipState(sourceEntry, targetEntry, relatedEnd, relationshipState);
			RelationshipWrapper relationshipWrapper = new RelationshipWrapper((AssociationSet)relatedEnd.RelationshipSet, new KeyValuePair<string, EntityKey>(relatedEnd.SourceRoleName, sourceEntry.EntityKey), new KeyValuePair<string, EntityKey>(relatedEnd.TargetRoleName, targetEntry.EntityKey));
			RelationshipEntry relationshipEntry = this.FindRelationship(relationshipWrapper);
			if (relationshipEntry == null && relationshipState == EntityState.Detached)
			{
				return null;
			}
			this.TransactionManager.BeginLocalPublicAPI();
			try
			{
				if (relationshipEntry != null)
				{
					relationshipEntry.ChangeRelationshipState(targetEntry, relatedEnd, relationshipState);
				}
				else
				{
					relationshipEntry = this.CreateRelationship(targetEntry, relatedEnd, relationshipWrapper, relationshipState);
				}
			}
			finally
			{
				this.TransactionManager.EndLocalPublicAPI();
			}
			if (relationshipState != EntityState.Detached)
			{
				return relationshipEntry;
			}
			return null;
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x0011424C File Offset: 0x0011244C
		private void VerifyParametersForChangeRelationshipState(object sourceEntity, object targetEntity, out EntityEntry sourceEntry, out EntityEntry targetEntry)
		{
			sourceEntry = this.GetEntityEntryByObjectOrEntityKey(sourceEntity);
			targetEntry = this.GetEntityEntryByObjectOrEntityKey(targetEntity);
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x00114264 File Offset: 0x00112464
		private static void VerifyInitialStateForChangeRelationshipState(EntityEntry sourceEntry, EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState relationshipState)
		{
			relatedEnd.VerifyType(targetEntry.WrappedEntity);
			if (relatedEnd.IsForeignKey)
			{
				throw new NotSupportedException(Strings.ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations);
			}
			EntityUtil.CheckValidStateForChangeRelationshipState(relationshipState, "relationshipState");
			if ((sourceEntry.State == EntityState.Deleted || targetEntry.State == EntityState.Deleted) && relationshipState != EntityState.Deleted && relationshipState != EntityState.Detached)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateEntityDeleted);
			}
			if ((sourceEntry.State == EntityState.Added || targetEntry.State == EntityState.Added) && relationshipState != EntityState.Added && relationshipState != EntityState.Detached)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateEntityAdded);
			}
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x001142E8 File Offset: 0x001124E8
		private RelationshipEntry CreateRelationship(EntityEntry targetEntry, RelatedEnd relatedEnd, RelationshipWrapper relationshipWrapper, EntityState requestedState)
		{
			RelationshipEntry relationshipEntry = null;
			switch (requestedState)
			{
			case EntityState.Detached:
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Unchanged:
				relatedEnd.Add(targetEntry.WrappedEntity, true, false, false, false, true);
				relationshipEntry = this.FindRelationship(relationshipWrapper);
				relationshipEntry.AcceptChanges();
				break;
			case EntityState.Added:
				relatedEnd.Add(targetEntry.WrappedEntity, true, false, false, false, true);
				relationshipEntry = this.FindRelationship(relationshipWrapper);
				break;
			default:
				if (requestedState == EntityState.Deleted)
				{
					relationshipEntry = this.AddNewRelation(relationshipWrapper, EntityState.Deleted);
				}
				break;
			}
			return relationshipEntry;
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x00114360 File Offset: 0x00112560
		private EntityEntry GetEntityEntryByObjectOrEntityKey(object o)
		{
			EntityKey entityKey = o as EntityKey;
			EntityEntry entityEntry = (entityKey != null) ? this.FindEntityEntry(entityKey) : this.FindEntityEntry(o);
			if (entityEntry == null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(o.GetType().FullName));
			}
			if (entityEntry.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateKeyEntry);
			}
			return entityEntry;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x001143BB File Offset: 0x001125BB
		IEntityStateEntry IEntityStateManager.GetEntityStateEntry(EntityKey key)
		{
			return this.GetEntityEntry(key);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x001143C4 File Offset: 0x001125C4
		public virtual ObjectStateEntry GetObjectStateEntry(EntityKey key)
		{
			ObjectStateEntry result;
			if (!this.TryGetObjectStateEntry(key, out result))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistForEntityKey);
			}
			return result;
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x001143E8 File Offset: 0x001125E8
		internal virtual EntityEntry GetEntityEntry(EntityKey key)
		{
			EntityEntry result;
			if (!this.TryGetEntityEntry(key, out result))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistForEntityKey);
			}
			return result;
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x0011440C File Offset: 0x0011260C
		public virtual ObjectStateEntry GetObjectStateEntry(object entity)
		{
			ObjectStateEntry result;
			if (!this.TryGetObjectStateEntry(entity, out result))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
			}
			return result;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x0011443C File Offset: 0x0011263C
		internal virtual EntityEntry GetEntityEntry(object entity)
		{
			EntityEntry entityEntry = this.FindEntityEntry(entity);
			if (entityEntry == null)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
			}
			return entityEntry;
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0011446C File Offset: 0x0011266C
		public virtual bool TryGetObjectStateEntry(object entity, out ObjectStateEntry entry)
		{
			Check.NotNull<object>(entity, "entity");
			entry = null;
			EntityKey entityKey = entity as EntityKey;
			if (entityKey != null)
			{
				return this.TryGetObjectStateEntry(entityKey, out entry);
			}
			entry = this.FindEntityEntry(entity);
			return entry != null;
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x001144B4 File Offset: 0x001126B4
		bool IEntityStateManager.TryGetEntityStateEntry(EntityKey key, out IEntityStateEntry entry)
		{
			ObjectStateEntry objectStateEntry;
			bool result = this.TryGetObjectStateEntry(key, out objectStateEntry);
			entry = objectStateEntry;
			return result;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x001144D0 File Offset: 0x001126D0
		bool IEntityStateManager.TryGetReferenceKey(EntityKey dependentKey, AssociationEndMember principalRole, out EntityKey principalKey)
		{
			EntityEntry entityEntry;
			if (!this.TryGetEntityEntry(dependentKey, out entityEntry))
			{
				principalKey = null;
				return false;
			}
			return entityEntry.TryGetReferenceKey(principalRole, out principalKey);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x001144F8 File Offset: 0x001126F8
		public virtual bool TryGetObjectStateEntry(EntityKey key, out ObjectStateEntry entry)
		{
			EntityEntry entityEntry;
			bool result = this.TryGetEntityEntry(key, out entityEntry);
			entry = entityEntry;
			return result;
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x00114514 File Offset: 0x00112714
		internal virtual bool TryGetEntityEntry(EntityKey key, out EntityEntry entry)
		{
			entry = null;
			bool result;
			if (key.IsTemporary)
			{
				result = (this._addedEntityStore != null && this._addedEntityStore.TryGetValue(key, out entry));
			}
			else
			{
				result = ((this._unchangedEntityStore != null && this._unchangedEntityStore.TryGetValue(key, out entry)) || (this._modifiedEntityStore != null && this._modifiedEntityStore.TryGetValue(key, out entry)) || (this._deletedEntityStore != null && this._deletedEntityStore.TryGetValue(key, out entry)));
			}
			return result;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x00114594 File Offset: 0x00112794
		internal virtual EntityEntry FindEntityEntry(EntityKey key)
		{
			EntityEntry result = null;
			if (key != null)
			{
				this.TryGetEntityEntry(key, out result);
			}
			return result;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x001145B4 File Offset: 0x001127B4
		internal virtual EntityEntry FindEntityEntry(object entity)
		{
			EntityEntry entityEntry = null;
			IEntityWithKey entityWithKey = entity as IEntityWithKey;
			if (entityWithKey != null)
			{
				EntityKey entityKey = entityWithKey.EntityKey;
				if (entityKey != null)
				{
					this.TryGetEntityEntry(entityKey, out entityEntry);
				}
			}
			else
			{
				this.TryGetEntryFromKeylessStore(entity, out entityEntry);
			}
			if (entityEntry != null && !object.ReferenceEquals(entity, entityEntry.Entity))
			{
				entityEntry = null;
			}
			return entityEntry;
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x00114604 File Offset: 0x00112804
		public virtual RelationshipManager GetRelationshipManager(object entity)
		{
			RelationshipManager result;
			if (!this.TryGetRelationshipManager(entity, out result))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity);
			}
			return result;
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x00114628 File Offset: 0x00112828
		public virtual bool TryGetRelationshipManager(object entity, out RelationshipManager relationshipManager)
		{
			Check.NotNull<object>(entity, "entity");
			IEntityWithRelationships entityWithRelationships = entity as IEntityWithRelationships;
			if (entityWithRelationships != null)
			{
				relationshipManager = entityWithRelationships.RelationshipManager;
				if (relationshipManager == null)
				{
					throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
				}
				if (relationshipManager.WrappedOwner.Entity != entity)
				{
					throw new InvalidOperationException(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
				}
			}
			else
			{
				IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingStateManager(entity, this);
				if (entityWrapper.Context == null)
				{
					relationshipManager = null;
					return false;
				}
				relationshipManager = entityWrapper.RelationshipManager;
			}
			return true;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x001146A0 File Offset: 0x001128A0
		internal virtual void ChangeState(RelationshipEntry entry, EntityState oldState, EntityState newState)
		{
			if (newState == EntityState.Detached)
			{
				this.DeleteRelationshipFromLookup(entry);
				this.RemoveObjectStateEntryFromDictionary(entry, oldState);
				entry.Reset();
				return;
			}
			this.RemoveObjectStateEntryFromDictionary(entry, oldState);
			this.AddRelationshipEntryToDictionary(entry, newState);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x001146CC File Offset: 0x001128CC
		internal virtual void ChangeState(EntityEntry entry, EntityState oldState, EntityState newState)
		{
			bool flag = !entry.IsKeyEntry;
			if (newState == EntityState.Detached)
			{
				foreach (RelationshipEntry relationshipEntry in this.CopyOfRelationshipsByKey(entry.EntityKey))
				{
					this.ChangeState(relationshipEntry, relationshipEntry.State, EntityState.Detached);
				}
				this.RemoveObjectStateEntryFromDictionary(entry, oldState);
				IEntityWrapper wrappedEntity = entry.WrappedEntity;
				entry.Reset();
				if (flag && wrappedEntity.Entity != null && !this.TransactionManager.IsAttachTracking)
				{
					this.OnEntityDeleted(CollectionChangeAction.Remove, wrappedEntity.Entity);
					this.OnObjectStateManagerChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
				}
			}
			else
			{
				this.RemoveObjectStateEntryFromDictionary(entry, oldState);
				this.AddEntityEntryToDictionary(entry, newState);
			}
			if (newState == EntityState.Deleted)
			{
				entry.RemoveFromForeignKeyIndex();
				this.ForgetEntryWithConceptualNull(entry, true);
				if (flag)
				{
					this.OnEntityDeleted(CollectionChangeAction.Remove, entry.Entity);
					this.OnObjectStateManagerChanged(CollectionChangeAction.Remove, entry.Entity);
				}
			}
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x001147A4 File Offset: 0x001129A4
		private void AddRelationshipEntryToDictionary(RelationshipEntry entry, EntityState state)
		{
			Dictionary<RelationshipWrapper, RelationshipEntry> dictionary = null;
			switch (state)
			{
			case EntityState.Unchanged:
				if (this._unchangedRelationshipStore == null)
				{
					this._unchangedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
				}
				dictionary = this._unchangedRelationshipStore;
				break;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				if (this._addedRelationshipStore == null)
				{
					this._addedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
				}
				dictionary = this._addedRelationshipStore;
				break;
			default:
				if (state == EntityState.Deleted)
				{
					if (this._deletedRelationshipStore == null)
					{
						this._deletedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
					}
					dictionary = this._deletedRelationshipStore;
				}
				break;
			}
			dictionary.Add(entry.RelationshipWrapper, entry);
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00114830 File Offset: 0x00112A30
		private void AddEntityEntryToDictionary(EntityEntry entry, EntityState state)
		{
			if (entry.RequiresAnyChangeTracking)
			{
				this._detectChangesNeeded = true;
			}
			Dictionary<EntityKey, EntityEntry> dictionary = null;
			switch (state)
			{
			case EntityState.Unchanged:
				if (this._unchangedEntityStore == null)
				{
					this._unchangedEntityStore = new Dictionary<EntityKey, EntityEntry>();
				}
				dictionary = this._unchangedEntityStore;
				break;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				if (this._addedEntityStore == null)
				{
					this._addedEntityStore = new Dictionary<EntityKey, EntityEntry>();
				}
				dictionary = this._addedEntityStore;
				break;
			default:
				if (state != EntityState.Deleted)
				{
					if (state == EntityState.Modified)
					{
						if (this._modifiedEntityStore == null)
						{
							this._modifiedEntityStore = new Dictionary<EntityKey, EntityEntry>();
						}
						dictionary = this._modifiedEntityStore;
					}
				}
				else
				{
					if (this._deletedEntityStore == null)
					{
						this._deletedEntityStore = new Dictionary<EntityKey, EntityEntry>();
					}
					dictionary = this._deletedEntityStore;
				}
				break;
			}
			dictionary.Add(entry.EntityKey, entry);
			this.AddEntryToKeylessStore(entry);
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x001148F4 File Offset: 0x00112AF4
		private void AddEntryToKeylessStore(EntityEntry entry)
		{
			if (entry.Entity != null && !(entry.Entity is IEntityWithKey))
			{
				if (this._keylessEntityStore == null)
				{
					this._keylessEntityStore = new Dictionary<object, EntityEntry>(ObjectReferenceEqualityComparer.Default);
				}
				if (!this._keylessEntityStore.ContainsKey(entry.Entity))
				{
					this._keylessEntityStore.Add(entry.Entity, entry);
				}
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x00114954 File Offset: 0x00112B54
		private void RemoveObjectStateEntryFromDictionary(RelationshipEntry entry, EntityState state)
		{
			Dictionary<RelationshipWrapper, RelationshipEntry> dictionary = null;
			switch (state)
			{
			case EntityState.Unchanged:
				dictionary = this._unchangedRelationshipStore;
				break;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				dictionary = this._addedRelationshipStore;
				break;
			default:
				if (state == EntityState.Deleted)
				{
					dictionary = this._deletedRelationshipStore;
				}
				break;
			}
			dictionary.Remove(entry.RelationshipWrapper);
			if (dictionary.Count == 0)
			{
				switch (state)
				{
				case EntityState.Unchanged:
					this._unchangedRelationshipStore = null;
					return;
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					this._addedRelationshipStore = null;
					return;
				default:
					if (state != EntityState.Deleted)
					{
						return;
					}
					this._deletedRelationshipStore = null;
					break;
				}
			}
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x001149E0 File Offset: 0x00112BE0
		private void RemoveObjectStateEntryFromDictionary(EntityEntry entry, EntityState state)
		{
			Dictionary<EntityKey, EntityEntry> dictionary = null;
			switch (state)
			{
			case EntityState.Unchanged:
				dictionary = this._unchangedEntityStore;
				break;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				dictionary = this._addedEntityStore;
				break;
			default:
				if (state != EntityState.Deleted)
				{
					if (state == EntityState.Modified)
					{
						dictionary = this._modifiedEntityStore;
					}
				}
				else
				{
					dictionary = this._deletedEntityStore;
				}
				break;
			}
			dictionary.Remove(entry.EntityKey);
			this.RemoveEntryFromKeylessStore(entry.WrappedEntity);
			if (dictionary.Count == 0)
			{
				switch (state)
				{
				case EntityState.Unchanged:
					this._unchangedEntityStore = null;
					return;
				case EntityState.Detached | EntityState.Unchanged:
					break;
				case EntityState.Added:
					this._addedEntityStore = null;
					return;
				default:
					if (state == EntityState.Deleted)
					{
						this._deletedEntityStore = null;
						return;
					}
					if (state != EntityState.Modified)
					{
						return;
					}
					this._modifiedEntityStore = null;
					break;
				}
			}
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x00114A92 File Offset: 0x00112C92
		internal virtual void RemoveEntryFromKeylessStore(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity != null && wrappedEntity.Entity != null && !(wrappedEntity.Entity is IEntityWithKey))
			{
				this._keylessEntityStore.Remove(wrappedEntity.Entity);
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x00114AC0 File Offset: 0x00112CC0
		internal virtual StateManagerTypeMetadata GetOrAddStateManagerTypeMetadata(Type entityType, EntitySet entitySet)
		{
			StateManagerTypeMetadata result;
			if (!this._metadataMapping.TryGetValue(new EntitySetQualifiedType(entityType, entitySet), out result))
			{
				result = this.AddStateManagerTypeMetadata(entitySet, (ObjectTypeMapping)this.MetadataWorkspace.GetMap(entityType.FullNameWithNesting(), DataSpace.OSpace, DataSpace.OCSpace));
			}
			return result;
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x00114B04 File Offset: 0x00112D04
		internal virtual StateManagerTypeMetadata GetOrAddStateManagerTypeMetadata(EdmType edmType)
		{
			StateManagerTypeMetadata result;
			if (!this._metadataStore.TryGetValue(edmType, out result))
			{
				result = this.AddStateManagerTypeMetadata(edmType, (ObjectTypeMapping)this.MetadataWorkspace.GetMap(edmType, DataSpace.OCSpace));
			}
			return result;
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x00114B3C File Offset: 0x00112D3C
		private StateManagerTypeMetadata AddStateManagerTypeMetadata(EntitySet entitySet, ObjectTypeMapping mapping)
		{
			EdmType edmType = mapping.EdmType;
			StateManagerTypeMetadata stateManagerTypeMetadata;
			if (!this._metadataStore.TryGetValue(edmType, out stateManagerTypeMetadata))
			{
				stateManagerTypeMetadata = new StateManagerTypeMetadata(edmType, mapping);
				this._metadataStore.Add(edmType, stateManagerTypeMetadata);
			}
			EntitySetQualifiedType key = new EntitySetQualifiedType(mapping.ClrType.ClrType, entitySet);
			if (!this._metadataMapping.ContainsKey(key))
			{
				this._metadataMapping.Add(key, stateManagerTypeMetadata);
				return stateManagerTypeMetadata;
			}
			throw new InvalidOperationException(Strings.Mapping_CannotMapCLRTypeMultipleTimes(stateManagerTypeMetadata.CdmMetadata.EdmType.FullName));
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x00114BC4 File Offset: 0x00112DC4
		private StateManagerTypeMetadata AddStateManagerTypeMetadata(EdmType edmType, ObjectTypeMapping mapping)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata = new StateManagerTypeMetadata(edmType, mapping);
			this._metadataStore.Add(edmType, stateManagerTypeMetadata);
			return stateManagerTypeMetadata;
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x00114BE7 File Offset: 0x00112DE7
		internal virtual void Dispose()
		{
			this._isDisposed = true;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x00114BF0 File Offset: 0x00112DF0
		internal virtual bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x00114BF8 File Offset: 0x00112DF8
		internal virtual void DetectChanges()
		{
			IList<EntityEntry> entityEntriesForDetectChanges = this.GetEntityEntriesForDetectChanges();
			if (entityEntriesForDetectChanges == null)
			{
				return;
			}
			if (this.TransactionManager.BeginDetectChanges())
			{
				try
				{
					ObjectStateManager.DetectChangesInNavigationProperties(entityEntriesForDetectChanges);
					ObjectStateManager.DetectChangesInScalarAndComplexProperties(entityEntriesForDetectChanges);
					ObjectStateManager.DetectChangesInForeignKeys(entityEntriesForDetectChanges);
					this.DetectConflicts(entityEntriesForDetectChanges);
					this.TransactionManager.BeginAlignChanges();
					this.AlignChangesInRelationships(entityEntriesForDetectChanges);
				}
				finally
				{
					this.TransactionManager.EndAlignChanges();
					this.TransactionManager.EndDetectChanges();
				}
			}
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x00114C74 File Offset: 0x00112E74
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void DetectConflicts(IList<EntityEntry> entries)
		{
			TransactionManager transactionManager = this.TransactionManager;
			foreach (EntityEntry entityEntry in entries)
			{
				Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary;
				transactionManager.AddedRelationshipsByGraph.TryGetValue(entityEntry.WrappedEntity, out dictionary);
				Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary2;
				transactionManager.AddedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary2);
				if (dictionary != null && dictionary.Count > 0 && entityEntry.State == EntityState.Deleted)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
				}
				if (dictionary2 != null)
				{
					foreach (KeyValuePair<RelatedEnd, HashSet<EntityKey>> keyValuePair in dictionary2)
					{
						if ((entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified) && keyValuePair.Key.IsDependentEndOfReferentialConstraint(true) && keyValuePair.Value.Count > 0)
						{
							throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
						}
						EntityReference entityReference = keyValuePair.Key as EntityReference;
						if (entityReference != null && keyValuePair.Value.Count > 1)
						{
							throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(keyValuePair.Key.RelationshipNavigation.To, keyValuePair.Key.RelationshipNavigation.RelationshipName));
						}
					}
				}
				if (dictionary != null)
				{
					Dictionary<string, KeyValuePair<object, IntBox>> properties = new Dictionary<string, KeyValuePair<object, IntBox>>();
					foreach (KeyValuePair<RelatedEnd, HashSet<IEntityWrapper>> keyValuePair2 in dictionary)
					{
						if (keyValuePair2.Key.IsForeignKey && (entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified) && keyValuePair2.Key.IsDependentEndOfReferentialConstraint(true) && keyValuePair2.Value.Count > 0)
						{
							throw new InvalidOperationException(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
						}
						EntityReference entityReference2 = keyValuePair2.Key as EntityReference;
						if (entityReference2 != null)
						{
							if (keyValuePair2.Value.Count > 1)
							{
								throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(keyValuePair2.Key.RelationshipNavigation.To, keyValuePair2.Key.RelationshipNavigation.RelationshipName));
							}
							if (keyValuePair2.Value.Count == 1)
							{
								IEntityWrapper entityWrapper = keyValuePair2.Value.First<IEntityWrapper>();
								HashSet<EntityKey> hashSet = null;
								Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary3;
								if (dictionary2 != null)
								{
									dictionary2.TryGetValue(keyValuePair2.Key, out hashSet);
								}
								else if (transactionManager.AddedRelationshipsByPrincipalKey.TryGetValue(entityEntry.WrappedEntity, out dictionary3))
								{
									dictionary3.TryGetValue(keyValuePair2.Key, out hashSet);
								}
								Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary4;
								HashSet<EntityKey> hashSet2;
								if (hashSet != null && hashSet.Count > 0)
								{
									EntityKey permanentKey = this.GetPermanentKey(entityEntry.WrappedEntity, entityReference2, entityWrapper);
									if (permanentKey != hashSet.First<EntityKey>())
									{
										throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(entityReference2.RelationshipNavigation.To, entityReference2.RelationshipNavigation.RelationshipName));
									}
								}
								else if (transactionManager.DeletedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary4) && dictionary4.TryGetValue(keyValuePair2.Key, out hashSet2) && hashSet2.Count > 0)
								{
									throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(entityReference2.RelationshipNavigation.To, entityReference2.RelationshipNavigation.RelationshipName));
								}
								EntityEntry entityEntry2 = this.FindEntityEntry(entityWrapper.Entity);
								if (entityEntry2 != null && (entityEntry2.State == EntityState.Unchanged || entityEntry2.State == EntityState.Modified))
								{
									Dictionary<string, KeyValuePair<object, IntBox>> dictionary5 = new Dictionary<string, KeyValuePair<object, IntBox>>();
									entityEntry2.GetOtherKeyProperties(dictionary5);
									foreach (ReferentialConstraint referentialConstraint in ((AssociationType)entityReference2.RelationMetadata).ReferentialConstraints)
									{
										if (referentialConstraint.ToRole == entityReference2.FromEndMember)
										{
											for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
											{
												EntityEntry.AddOrIncreaseCounter(referentialConstraint, properties, referentialConstraint.ToProperties[i].Name, dictionary5[referentialConstraint.FromProperties[i].Name].Key);
											}
											break;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x001150EC File Offset: 0x001132EC
		internal virtual EntityKey GetPermanentKey(IEntityWrapper entityFrom, RelatedEnd relatedEndFrom, IEntityWrapper entityTo)
		{
			EntityKey entityKey = null;
			if (entityTo.ObjectStateEntry != null)
			{
				entityKey = entityTo.ObjectStateEntry.EntityKey;
			}
			if (entityKey == null || entityKey.IsTemporary)
			{
				entityKey = this.CreateEntityKey(ObjectStateManager.GetEntitySetOfOtherEnd(entityFrom, relatedEndFrom), entityTo.Entity);
			}
			return entityKey;
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x00115138 File Offset: 0x00113338
		private static EntitySet GetEntitySetOfOtherEnd(IEntityWrapper entity, RelatedEnd relatedEnd)
		{
			AssociationSet associationSet = (AssociationSet)relatedEnd.RelationshipSet;
			EntitySet entitySet = associationSet.AssociationSetEnds[0].EntitySet;
			if (entitySet.Name != entity.EntityKey.EntitySetName)
			{
				return entitySet;
			}
			return associationSet.AssociationSetEnds[1].EntitySet;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x00115190 File Offset: 0x00113390
		private static void DetectChangesInForeignKeys(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
				{
					entityEntry.DetectChangesInForeignKeys();
				}
			}
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x001151EC File Offset: 0x001133EC
		private void AlignChangesInRelationships(IList<EntityEntry> entries)
		{
			this.PerformDelete(entries);
			this.PerformAdd(entries);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x001151FC File Offset: 0x001133FC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void PerformAdd(IList<EntityEntry> entries)
		{
			TransactionManager transactionManager = this.TransactionManager;
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Detached && !entityEntry.IsKeyEntry)
				{
					foreach (RelatedEnd relatedEnd in entityEntry.WrappedEntity.RelationshipManager.Relationships)
					{
						HashSet<EntityKey> hashSet = null;
						Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
						if (relatedEnd is EntityReference && transactionManager.AddedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary))
						{
							dictionary.TryGetValue(relatedEnd, out hashSet);
						}
						HashSet<IEntityWrapper> hashSet2 = null;
						Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
						if (transactionManager.AddedRelationshipsByGraph.TryGetValue(entityEntry.WrappedEntity, out dictionary2))
						{
							dictionary2.TryGetValue(relatedEnd, out hashSet2);
						}
						if (hashSet != null)
						{
							foreach (EntityKey key in hashSet)
							{
								EntityEntry entityEntry2;
								if (this.TryGetEntityEntry(key, out entityEntry2) && entityEntry2.WrappedEntity.Entity != null)
								{
									hashSet2 = ((hashSet2 != null) ? hashSet2 : new HashSet<IEntityWrapper>());
									if (entityEntry2.State != EntityState.Deleted)
									{
										hashSet2.Remove(entityEntry2.WrappedEntity);
										this.PerformAdd(entityEntry.WrappedEntity, relatedEnd, entityEntry2.WrappedEntity, true);
									}
								}
								else
								{
									EntityReference reference = relatedEnd as EntityReference;
									entityEntry.FixupEntityReferenceByForeignKey(reference);
								}
							}
						}
						if (hashSet2 != null)
						{
							foreach (IEntityWrapper entityToAdd in hashSet2)
							{
								this.PerformAdd(entityEntry.WrappedEntity, relatedEnd, entityToAdd, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x00115424 File Offset: 0x00113624
		private void PerformAdd(IEntityWrapper wrappedOwner, RelatedEnd relatedEnd, IEntityWrapper entityToAdd, bool isForeignKeyChange)
		{
			relatedEnd.ValidateStateForAdd(relatedEnd.WrappedOwner);
			relatedEnd.ValidateStateForAdd(entityToAdd);
			if (relatedEnd.IsPrincipalEndOfReferentialConstraint())
			{
				EntityReference entityReference = relatedEnd.GetOtherEndOfRelationship(entityToAdd) as EntityReference;
				if (entityReference != null && this.IsReparentingReference(entityToAdd, entityReference))
				{
					this.TransactionManager.EntityBeingReparented = entityReference.GetDependentEndOfReferentialConstraint(entityReference.ReferenceValue.Entity);
				}
			}
			else if (relatedEnd.IsDependentEndOfReferentialConstraint(false))
			{
				EntityReference entityReference2 = relatedEnd as EntityReference;
				if (entityReference2 != null && this.IsReparentingReference(wrappedOwner, entityReference2))
				{
					this.TransactionManager.EntityBeingReparented = entityReference2.GetDependentEndOfReferentialConstraint(entityReference2.ReferenceValue.Entity);
				}
			}
			try
			{
				relatedEnd.Add(entityToAdd, false, false, false, true, !isForeignKeyChange);
			}
			finally
			{
				this.TransactionManager.EntityBeingReparented = null;
			}
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x001154EC File Offset: 0x001136EC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void PerformDelete(IList<EntityEntry> entries)
		{
			TransactionManager transactionManager = this.TransactionManager;
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Detached && entityEntry.State != EntityState.Deleted && !entityEntry.IsKeyEntry)
				{
					foreach (RelatedEnd relatedEnd in entityEntry.WrappedEntity.RelationshipManager.Relationships)
					{
						HashSet<EntityKey> hashSet = null;
						EntityReference entityReference = relatedEnd as EntityReference;
						Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
						if (entityReference != null && transactionManager.DeletedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary))
						{
							dictionary.TryGetValue(entityReference, out hashSet);
						}
						HashSet<IEntityWrapper> hashSet2 = null;
						Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
						if (transactionManager.DeletedRelationshipsByGraph.TryGetValue(entityEntry.WrappedEntity, out dictionary2))
						{
							dictionary2.TryGetValue(relatedEnd, out hashSet2);
						}
						if (hashSet != null)
						{
							foreach (EntityKey entityKey in hashSet)
							{
								IEntityWrapper entityWrapper = null;
								EntityEntry entityEntry2;
								if (this.TryGetEntityEntry(entityKey, out entityEntry2) && entityEntry2.WrappedEntity.Entity != null)
								{
									entityWrapper = entityEntry2.WrappedEntity;
								}
								else if (entityReference != null && entityReference.ReferenceValue != NullEntityWrapper.NullWrapper && entityReference.ReferenceValue.EntityKey.IsTemporary && this.TryGetEntityEntry(entityReference.ReferenceValue.EntityKey, out entityEntry2) && entityEntry2.WrappedEntity.Entity != null)
								{
									EntityKey key = new EntityKey((EntitySet)entityEntry2.EntitySet, entityEntry2.CurrentValues);
									if (entityKey == key)
									{
										entityWrapper = entityEntry2.WrappedEntity;
									}
								}
								if (entityWrapper != null)
								{
									hashSet2 = ((hashSet2 != null) ? hashSet2 : new HashSet<IEntityWrapper>());
									bool preserveForeignKey = this.ShouldPreserveForeignKeyForDependent(entityEntry.WrappedEntity, relatedEnd, entityWrapper, hashSet2);
									hashSet2.Remove(entityWrapper);
									if (entityReference != null && this.IsReparentingReference(entityEntry.WrappedEntity, entityReference))
									{
										this.TransactionManager.EntityBeingReparented = entityReference.GetDependentEndOfReferentialConstraint(entityReference.ReferenceValue.Entity);
									}
									try
									{
										relatedEnd.Remove(entityWrapper, preserveForeignKey);
									}
									finally
									{
										this.TransactionManager.EntityBeingReparented = null;
									}
									if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted || entityEntry.IsKeyEntry)
									{
										break;
									}
								}
								if (entityReference != null && entityReference.IsForeignKey && entityReference.IsDependentEndOfReferentialConstraint(false))
								{
									entityReference.SetCachedForeignKey(ForeignKeyFactory.CreateKeyFromForeignKeyValues(entityEntry, entityReference), entityEntry);
								}
							}
						}
						if (hashSet2 != null)
						{
							foreach (IEntityWrapper entityWrapper2 in hashSet2)
							{
								bool preserveForeignKey2 = this.ShouldPreserveForeignKeyForPrincipal(entityEntry.WrappedEntity, relatedEnd, entityWrapper2, hashSet2);
								if (entityReference != null && this.IsReparentingReference(entityEntry.WrappedEntity, entityReference))
								{
									this.TransactionManager.EntityBeingReparented = entityReference.GetDependentEndOfReferentialConstraint(entityReference.ReferenceValue.Entity);
								}
								try
								{
									relatedEnd.Remove(entityWrapper2, preserveForeignKey2);
								}
								finally
								{
									this.TransactionManager.EntityBeingReparented = null;
								}
								if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted || entityEntry.IsKeyEntry)
								{
									break;
								}
							}
						}
						if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted || entityEntry.IsKeyEntry)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x001158DC File Offset: 0x00113ADC
		private bool ShouldPreserveForeignKeyForPrincipal(IEntityWrapper entity, RelatedEnd relatedEnd, IEntityWrapper relatedEntity, HashSet<IEntityWrapper> entitiesToDelete)
		{
			bool result = false;
			if (relatedEnd.IsForeignKey)
			{
				RelatedEnd otherEndOfRelationship = relatedEnd.GetOtherEndOfRelationship(relatedEntity);
				if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
				{
					HashSet<EntityKey> hashSet = null;
					Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
					Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
					if (this.TransactionManager.DeletedRelationshipsByForeignKey.TryGetValue(relatedEntity, out dictionary) && dictionary.TryGetValue(otherEndOfRelationship, out hashSet) && hashSet.Count > 0 && this.TransactionManager.DeletedRelationshipsByGraph.TryGetValue(relatedEntity, out dictionary2) && dictionary2.TryGetValue(otherEndOfRelationship, out entitiesToDelete))
					{
						result = this.ShouldPreserveForeignKeyForDependent(relatedEntity, otherEndOfRelationship, entity, entitiesToDelete);
					}
				}
			}
			return result;
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x00115960 File Offset: 0x00113B60
		private bool ShouldPreserveForeignKeyForDependent(IEntityWrapper entity, RelatedEnd relatedEnd, IEntityWrapper relatedEntity, HashSet<IEntityWrapper> entitiesToDelete)
		{
			bool flag = entitiesToDelete.Contains(relatedEntity);
			return !flag || (flag && !this.HasAddedReference(entity, relatedEnd as EntityReference));
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x00115990 File Offset: 0x00113B90
		private bool HasAddedReference(IEntityWrapper wrappedOwner, EntityReference reference)
		{
			HashSet<IEntityWrapper> hashSet = null;
			Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary;
			return reference != null && this.TransactionManager.AddedRelationshipsByGraph.TryGetValue(wrappedOwner, out dictionary) && dictionary.TryGetValue(reference, out hashSet) && hashSet.Count > 0;
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x001159D0 File Offset: 0x00113BD0
		private bool IsReparentingReference(IEntityWrapper wrappedEntity, EntityReference reference)
		{
			TransactionManager transactionManager = this.TransactionManager;
			if (reference.IsPrincipalEndOfReferentialConstraint())
			{
				wrappedEntity = reference.ReferenceValue;
				reference = ((wrappedEntity.Entity == null) ? null : (reference.GetOtherEndOfRelationship(wrappedEntity) as EntityReference));
			}
			if (wrappedEntity.Entity != null && reference != null)
			{
				HashSet<EntityKey> hashSet = null;
				Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
				if (transactionManager.AddedRelationshipsByForeignKey.TryGetValue(wrappedEntity, out dictionary) && dictionary.TryGetValue(reference, out hashSet) && hashSet.Count > 0)
				{
					return true;
				}
				HashSet<IEntityWrapper> hashSet2 = null;
				Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary2;
				if (transactionManager.AddedRelationshipsByGraph.TryGetValue(wrappedEntity, out dictionary2) && dictionary2.TryGetValue(reference, out hashSet2) && hashSet2.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x00115A6C File Offset: 0x00113C6C
		private static void DetectChangesInNavigationProperties(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.WrappedEntity.RequiresRelationshipChangeTracking)
				{
					entityEntry.DetectChangesInRelationshipsOfSingleEntity();
				}
			}
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x00115AC0 File Offset: 0x00113CC0
		private static void DetectChangesInScalarAndComplexProperties(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Added && (entityEntry.RequiresScalarChangeTracking || entityEntry.RequiresComplexChangeTracking))
				{
					entityEntry.DetectChangesInProperties(!entityEntry.RequiresScalarChangeTracking);
				}
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x00115B2C File Offset: 0x00113D2C
		internal virtual EntityKey CreateEntityKey(EntitySet entitySet, object entity)
		{
			ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(EntityUtil.GetEntityIdentityType(entity.GetType()), entitySet);
			object[] array = new object[keyMembers.Count];
			for (int i = 0; i < keyMembers.Count; i++)
			{
				string name = keyMembers[i].Name;
				int ordinalforCLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(name);
				if (ordinalforCLayerMemberName < 0)
				{
					throw new ArgumentException(Strings.ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(entity.GetType().FullName, entitySet.Name), "entity");
				}
				array[i] = orAddStateManagerTypeMetadata.Member(ordinalforCLayerMemberName).GetValue(entity);
				if (array[i] == null)
				{
					throw new InvalidOperationException(Strings.EntityKey_NullKeyValue(name, entitySet.ElementType.Name));
				}
			}
			if (array.Length == 1)
			{
				return new EntityKey(entitySet, array[0]);
			}
			return new EntityKey(entitySet, array);
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x00115BF8 File Offset: 0x00113DF8
		// (set) Token: 0x06003A5B RID: 14939 RVA: 0x00115C00 File Offset: 0x00113E00
		internal virtual object EntityInvokingFKSetter { get; set; }

		// Token: 0x040015F8 RID: 5624
		private const int InitialListSize = 16;

		// Token: 0x040015F9 RID: 5625
		private Dictionary<EntityKey, EntityEntry> _addedEntityStore;

		// Token: 0x040015FA RID: 5626
		private Dictionary<EntityKey, EntityEntry> _modifiedEntityStore;

		// Token: 0x040015FB RID: 5627
		private Dictionary<EntityKey, EntityEntry> _deletedEntityStore;

		// Token: 0x040015FC RID: 5628
		private Dictionary<EntityKey, EntityEntry> _unchangedEntityStore;

		// Token: 0x040015FD RID: 5629
		private Dictionary<object, EntityEntry> _keylessEntityStore;

		// Token: 0x040015FE RID: 5630
		private Dictionary<RelationshipWrapper, RelationshipEntry> _addedRelationshipStore;

		// Token: 0x040015FF RID: 5631
		private Dictionary<RelationshipWrapper, RelationshipEntry> _deletedRelationshipStore;

		// Token: 0x04001600 RID: 5632
		private Dictionary<RelationshipWrapper, RelationshipEntry> _unchangedRelationshipStore;

		// Token: 0x04001601 RID: 5633
		private readonly Dictionary<EdmType, StateManagerTypeMetadata> _metadataStore;

		// Token: 0x04001602 RID: 5634
		private readonly Dictionary<EntitySetQualifiedType, StateManagerTypeMetadata> _metadataMapping;

		// Token: 0x04001603 RID: 5635
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x04001604 RID: 5636
		private CollectionChangeEventHandler onObjectStateManagerChangedDelegate;

		// Token: 0x04001605 RID: 5637
		private CollectionChangeEventHandler onEntityDeletedDelegate;

		// Token: 0x04001606 RID: 5638
		private bool _inRelationshipFixup;

		// Token: 0x04001607 RID: 5639
		private bool _isDisposed;

		// Token: 0x04001608 RID: 5640
		private ComplexTypeMaterializer _complexTypeMaterializer;

		// Token: 0x04001609 RID: 5641
		private readonly Dictionary<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>> _danglingForeignKeys = new Dictionary<EntityKey, HashSet<Tuple<EntityReference, EntityEntry>>>();

		// Token: 0x0400160A RID: 5642
		private HashSet<EntityEntry> _entriesWithConceptualNulls;

		// Token: 0x0400160B RID: 5643
		private readonly EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x0400160C RID: 5644
		private bool _detectChangesNeeded;
	}
}
