using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects.Internal;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Objects
{
	// Token: 0x02000148 RID: 328
	public class ObjectStateManager : IEntityStateManager
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x0004FBA4 File Offset: 0x0004DDA4
		[CLSCompliant(false)]
		public ObjectStateManager(MetadataWorkspace metadataWorkspace)
		{
			EntityUtil.CheckArgumentNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			this._metadataWorkspace = metadataWorkspace;
			this._metadataStore = new Dictionary<EdmType, StateManagerTypeMetadata>();
			this._metadataMapping = new Dictionary<EntitySetQualifiedType, StateManagerTypeMetadata>(EntitySetQualifiedType.EqualityComparer);
			this._isDisposed = false;
			this.TransactionManager = new TransactionManager();
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0004FC02 File Offset: 0x0004DE02
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0004FC0A File Offset: 0x0004DE0A
		internal object ChangingObject
		{
			get
			{
				return this._changingObject;
			}
			set
			{
				this._changingObject = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0004FC13 File Offset: 0x0004DE13
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0004FC1B File Offset: 0x0004DE1B
		internal string ChangingEntityMember
		{
			get
			{
				return this._changingEntityMember;
			}
			set
			{
				this._changingEntityMember = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0004FC24 File Offset: 0x0004DE24
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		internal string ChangingMember
		{
			get
			{
				return this._changingMember;
			}
			set
			{
				this._changingMember = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0004FC35 File Offset: 0x0004DE35
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x0004FC3D File Offset: 0x0004DE3D
		internal EntityState ChangingState
		{
			get
			{
				return this._changingState;
			}
			set
			{
				this._changingState = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0004FC46 File Offset: 0x0004DE46
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x0004FC4E File Offset: 0x0004DE4E
		internal bool SaveOriginalValues
		{
			get
			{
				return this._saveOriginalValues;
			}
			set
			{
				this._saveOriginalValues = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0004FC57 File Offset: 0x0004DE57
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x0004FC5F File Offset: 0x0004DE5F
		internal object ChangingOldValue
		{
			get
			{
				return this._changingOldValue;
			}
			set
			{
				this._changingOldValue = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0004FC68 File Offset: 0x0004DE68
		internal bool InRelationshipFixup
		{
			get
			{
				return this._inRelationshipFixup;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0004FC70 File Offset: 0x0004DE70
		internal ComplexTypeMaterializer ComplexTypeMaterializer
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0004FC91 File Offset: 0x0004DE91
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x0004FC99 File Offset: 0x0004DE99
		internal TransactionManager TransactionManager { get; private set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0004FCA2 File Offset: 0x0004DEA2
		[CLSCompliant(false)]
		public MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadataWorkspace;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060017CD RID: 6093 RVA: 0x0004FCAA File Offset: 0x0004DEAA
		// (remove) Token: 0x060017CE RID: 6094 RVA: 0x0004FCC3 File Offset: 0x0004DEC3
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

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060017CF RID: 6095 RVA: 0x0004FCDC File Offset: 0x0004DEDC
		// (remove) Token: 0x060017D0 RID: 6096 RVA: 0x0004FCF5 File Offset: 0x0004DEF5
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

		// Token: 0x060017D1 RID: 6097 RVA: 0x0004FD0E File Offset: 0x0004DF0E
		internal void OnObjectStateManagerChanged(CollectionChangeAction action, object entity)
		{
			if (this.onObjectStateManagerChangedDelegate != null)
			{
				this.onObjectStateManagerChangedDelegate(this, new CollectionChangeEventArgs(action, entity));
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0004FD2B File Offset: 0x0004DF2B
		private void OnEntityDeleted(CollectionChangeAction action, object entity)
		{
			if (this.onEntityDeletedDelegate != null)
			{
				this.onEntityDeletedDelegate(this, new CollectionChangeEventArgs(action, entity));
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0004FD48 File Offset: 0x0004DF48
		internal EntityEntry AddKeyEntry(EntityKey entityKey, EntitySet entitySet)
		{
			EntityEntry entityEntry = this.FindEntityEntry(entityKey);
			if (entityEntry != null)
			{
				throw EntityUtil.ObjectStateManagerContainsThisEntityKey();
			}
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(entitySet.ElementType);
			entityEntry = new EntityEntry(entityKey, entitySet, this, orAddStateManagerTypeMetadata);
			this.AddEntityEntryToDictionary(entityEntry, entityEntry.State);
			return entityEntry;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0004FD8C File Offset: 0x0004DF8C
		private void ValidateProxyType(IEntityWrapper wrappedEntity)
		{
			Type identityType = wrappedEntity.IdentityType;
			Type type = wrappedEntity.Entity.GetType();
			if (identityType != type)
			{
				ClrEntityType item = this.MetadataWorkspace.GetItem<ClrEntityType>(identityType.FullName, DataSpace.OSpace);
				EntityProxyTypeInfo proxyType = EntityProxyFactory.GetProxyType(item);
				if (proxyType == null || proxyType.ProxyType != type)
				{
					throw EntityUtil.DuplicateTypeForProxyType(identityType);
				}
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0004FDE8 File Offset: 0x0004DFE8
		internal EntityEntry AddEntry(IEntityWrapper wrappedObject, EntityKey passedKey, EntitySet entitySet, string argumentName, bool isAdded)
		{
			EntityKey entityKey = passedKey;
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedObject.IdentityType, entitySet);
			this.ValidateProxyType(wrappedObject);
			EdmType edmType = orAddStateManagerTypeMetadata.CdmMetadata.EdmType;
			if (isAdded && !entitySet.ElementType.IsAssignableFrom(edmType))
			{
				throw EntityUtil.EntityTypeDoesNotMatchEntitySet(wrappedObject.Entity.GetType().Name, TypeHelpers.GetFullName(entitySet), argumentName);
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
				EntityUtil.CheckEntityKeyNull(entityKey);
				EntityUtil.CheckEntityKeysMatch(wrappedObject, entityKey);
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
				throw EntityUtil.ObjectStateManagerContainsThisEntityKey();
			}
			if (entityEntry.State != EntityState.Added)
			{
				throw EntityUtil.ObjectStateManagerDoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(entityEntry.State);
			}
			return null;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0004FF64 File Offset: 0x0004E164
		internal void FixupReferencesByForeignKeys(EntityEntry newEntry, bool replaceAddedRefs = false)
		{
			if (!((EntitySet)newEntry.EntitySet).HasForeignKeyRelationships)
			{
				return;
			}
			newEntry.FixupReferencesByForeignKeys(replaceAddedRefs);
			foreach (EntityEntry entityEntry in this.GetNonFixedupEntriesContainingForeignKey(newEntry.EntityKey))
			{
				entityEntry.FixupReferencesByForeignKeys(false);
			}
			this.RemoveForeignKeyFromIndex(newEntry.EntityKey);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0004FFE0 File Offset: 0x0004E1E0
		internal void AddEntryContainingForeignKeyToIndex(EntityKey foreignKey, EntityEntry entry)
		{
			HashSet<EntityEntry> hashSet;
			if (!this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				hashSet = new HashSet<EntityEntry>();
				this._danglingForeignKeys.Add(foreignKey, hashSet);
			}
			hashSet.Add(entry);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00050018 File Offset: 0x0004E218
		[Conditional("DEBUG")]
		internal void AssertEntryDoesNotExistInForeignKeyIndex(EntityEntry entry)
		{
			foreach (EntityEntry entityEntry in this._danglingForeignKeys.SelectMany((KeyValuePair<EntityKey, HashSet<EntityEntry>> kv) => kv.Value))
			{
				if (entityEntry.State != EntityState.Detached)
				{
					EntityState state = entry.State;
				}
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00050094 File Offset: 0x0004E294
		[Conditional("DEBUG")]
		internal void AssertAllForeignKeyIndexEntriesAreValid()
		{
			if (this._danglingForeignKeys.Count == 0)
			{
				return;
			}
			HashSet<ObjectStateEntry> hashSet = new HashSet<ObjectStateEntry>(this.GetObjectStateEntriesInternal(~EntityState.Detached));
			foreach (EntityEntry entityEntry in this._danglingForeignKeys.SelectMany((KeyValuePair<EntityKey, HashSet<EntityEntry>> kv) => kv.Value))
			{
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0005011C File Offset: 0x0004E31C
		internal void RemoveEntryFromForeignKeyIndex(EntityKey foreignKey, EntityEntry entry)
		{
			HashSet<EntityEntry> hashSet;
			if (this._danglingForeignKeys.TryGetValue(foreignKey, out hashSet))
			{
				hashSet.Remove(entry);
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00050141 File Offset: 0x0004E341
		internal void RemoveForeignKeyFromIndex(EntityKey foreignKey)
		{
			this._danglingForeignKeys.Remove(foreignKey);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00050150 File Offset: 0x0004E350
		internal IEnumerable<EntityEntry> GetNonFixedupEntriesContainingForeignKey(EntityKey foreignKey)
		{
			HashSet<EntityEntry> source;
			if (this._danglingForeignKeys.TryGetValue(foreignKey, out source))
			{
				return source.ToList<EntityEntry>();
			}
			return Enumerable.Empty<EntityEntry>();
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00050179 File Offset: 0x0004E379
		internal void RememberEntryWithConceptualNull(EntityEntry entry)
		{
			if (this._entriesWithConceptualNulls == null)
			{
				this._entriesWithConceptualNulls = new HashSet<EntityEntry>();
			}
			this._entriesWithConceptualNulls.Add(entry);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0005019B File Offset: 0x0004E39B
		internal bool SomeEntryWithConceptualNullExists()
		{
			return this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Count != 0;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000501B5 File Offset: 0x0004E3B5
		internal bool EntryHasConceptualNull(EntityEntry entry)
		{
			return this._entriesWithConceptualNulls != null && this._entriesWithConceptualNulls.Contains(entry);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000501D0 File Offset: 0x0004E3D0
		internal void ForgetEntryWithConceptualNull(EntityEntry entry, bool resetAllKeys)
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

		// Token: 0x060017E1 RID: 6113 RVA: 0x0005027C File Offset: 0x0004E47C
		internal void PromoteKeyEntryInitialization(ObjectContext contextToAttach, EntityEntry keyEntry, IEntityWrapper wrappedEntity, IExtendedDataRecord shadowValues, bool replacingEntry)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedEntity.IdentityType, (EntitySet)keyEntry.EntitySet);
			this.ValidateProxyType(wrappedEntity);
			keyEntry.PromoteKeyEntry(wrappedEntity, shadowValues, orAddStateManagerTypeMetadata);
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

		// Token: 0x060017E2 RID: 6114 RVA: 0x000502F8 File Offset: 0x0004E4F8
		internal void PromoteKeyEntry(EntityEntry keyEntry, IEntityWrapper wrappedEntity, IExtendedDataRecord shadowValues, bool replacingEntry, bool setIsLoaded, bool keyEntryInitialized, string argumentName)
		{
			if (!keyEntryInitialized)
			{
				this.PromoteKeyEntryInitialization(null, keyEntry, wrappedEntity, shadowValues, replacingEntry);
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

		// Token: 0x060017E3 RID: 6115 RVA: 0x000503C8 File Offset: 0x0004E5C8
		internal void TrackPromotedRelationship(RelatedEnd relatedEnd, IEntityWrapper wrappedEntity)
		{
			IList<IEntityWrapper> list;
			if (!this.TransactionManager.PromotedRelationships.TryGetValue(relatedEnd, out list))
			{
				list = new List<IEntityWrapper>();
				this.TransactionManager.PromotedRelationships.Add(relatedEnd, list);
			}
			list.Add(wrappedEntity);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0005040C File Offset: 0x0004E60C
		internal void DegradePromotedRelationships()
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

		// Token: 0x060017E5 RID: 6117 RVA: 0x000504B8 File Offset: 0x0004E6B8
		internal static void AddEntityToCollectionOrReference(MergeOption mergeOption, IEntityWrapper wrappedSource, AssociationEndMember sourceMember, IEntityWrapper wrappedTarget, AssociationEndMember targetMember, bool setIsLoaded, bool relationshipAlreadyExists, bool inKeyEntryPromotion)
		{
			RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
			if (targetMember.RelationshipMultiplicity != RelationshipMultiplicity.Many)
			{
				EntityReference entityReference = (EntityReference)relatedEndInternal;
				switch (mergeOption)
				{
				case MergeOption.AppendOnly:
					if (inKeyEntryPromotion && !entityReference.IsEmpty() && entityReference.ReferenceValue.Entity != wrappedTarget.Entity)
					{
						throw EntityUtil.EntityConflictsWithKeyEntry();
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
					throw EntityUtil.CannotFillTryDifferentMergeOption(relatedEnd.SourceRoleName, relatedEnd.RelationshipName);
				}
			}
			if (relatedEnd == null)
			{
				relatedEnd = relatedEndInternal.GetOtherEndOfRelationship(wrappedTarget);
			}
			relatedEndInternal.Add(wrappedTarget, true, true, relationshipAlreadyExists, true, true);
			ObjectStateManager.UpdateRelatedEnd(relatedEndInternal, wrappedSource, wrappedTarget, setIsLoaded, mergeOption);
			ObjectStateManager.UpdateRelatedEnd(relatedEnd, wrappedTarget, wrappedSource, setIsLoaded, mergeOption);
			if (inKeyEntryPromotion && wrappedSource.Context.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				wrappedSource.Context.ObjectStateManager.TrackPromotedRelationship(relatedEndInternal, wrappedTarget);
				wrappedSource.Context.ObjectStateManager.TrackPromotedRelationship(relatedEnd, wrappedSource);
			}
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00050600 File Offset: 0x0004E800
		private static void UpdateRelatedEnd(RelatedEnd relatedEnd, IEntityWrapper wrappedEntity, IEntityWrapper wrappedRelatedEntity, bool setIsLoaded, MergeOption mergeOption)
		{
			AssociationEndMember associationEndMember = (AssociationEndMember)relatedEnd.ToEndMember;
			if (associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One || associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
			{
				if (setIsLoaded)
				{
					relatedEnd.SetIsLoaded(true);
				}
				if (mergeOption == MergeOption.NoTracking)
				{
					EntityKey entityKey = wrappedRelatedEntity.EntityKey;
					EntityUtil.CheckEntityKeyNull(entityKey);
					((EntityReference)relatedEnd).DetachedEntityKey = entityKey;
				}
			}
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00050654 File Offset: 0x0004E854
		internal static int UpdateRelationships(ObjectContext context, MergeOption mergeOption, AssociationSet associationSet, AssociationEndMember sourceMember, EntityKey sourceKey, IEntityWrapper wrappedSource, AssociationEndMember targetMember, IList targets, bool setIsLoaded)
		{
			int num = 0;
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
							throw EntityUtil.CannotFillTryDifferentMergeOption(relatedEndInternal.SourceRoleName, relatedEndInternal.RelationshipName);
						}
					}
					foreach (object obj in targets)
					{
						IEntityWrapper entityWrapper = obj as IEntityWrapper;
						if (entityWrapper == null)
						{
							entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(obj, context);
						}
						num++;
						if (mergeOption == MergeOption.NoTracking)
						{
							ObjectStateManager.AddEntityToCollectionOrReference(MergeOption.NoTracking, wrappedSource, sourceMember, entityWrapper, targetMember, setIsLoaded, true, false);
						}
						else
						{
							ObjectStateManager objectStateManager = context.ObjectStateManager;
							EntityKey entityKey = entityWrapper.EntityKey;
							EntityState entityState;
							if (!ObjectStateManager.TryUpdateExistingRelationships(context, mergeOption, associationSet, sourceMember, sourceKey, wrappedSource, targetMember, entityKey, setIsLoaded, out entityState))
							{
								bool flag = true;
								RelationshipMultiplicity relationshipMultiplicity = sourceMember.RelationshipMultiplicity;
								if (relationshipMultiplicity > RelationshipMultiplicity.One)
								{
									if (relationshipMultiplicity != RelationshipMultiplicity.Many)
									{
									}
								}
								else
								{
									flag = !ObjectStateManager.TryUpdateExistingRelationships(context, mergeOption, associationSet, targetMember, entityKey, entityWrapper, sourceMember, sourceKey, setIsLoaded, out entityState);
								}
								if (flag)
								{
									if (entityState != EntityState.Deleted)
									{
										ObjectStateManager.AddEntityToCollectionOrReference(mergeOption, wrappedSource, sourceMember, entityWrapper, targetMember, setIsLoaded, false, false);
									}
									else
									{
										RelationshipWrapper wrapper = new RelationshipWrapper(associationSet, sourceMember.Name, sourceKey, targetMember.Name, entityKey);
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

		// Token: 0x060017E8 RID: 6120 RVA: 0x00050810 File Offset: 0x0004EA10
		private static void EnsureCollectionNotNull(AssociationEndMember sourceMember, IEntityWrapper wrappedSource, AssociationEndMember targetMember)
		{
			RelatedEnd relatedEndInternal = wrappedSource.RelationshipManager.GetRelatedEndInternal(sourceMember.DeclaringType.FullName, targetMember.Name);
			AssociationEndMember associationEndMember = (AssociationEndMember)relatedEndInternal.ToEndMember;
			if (associationEndMember != null && associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many && relatedEndInternal.TargetAccessor.HasProperty)
			{
				wrappedSource.EnsureCollectionNotNull(relatedEndInternal);
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00050868 File Offset: 0x0004EA68
		internal static void RemoveRelationships(ObjectContext context, MergeOption mergeOption, AssociationSet associationSet, EntityKey sourceKey, AssociationEndMember sourceMember)
		{
			List<RelationshipEntry> list = new List<RelationshipEntry>(16);
			if (mergeOption == MergeOption.OverwriteChanges)
			{
				using (EntityEntry.RelationshipEndEnumerator enumerator = context.ObjectStateManager.FindRelationshipsByKey(sourceKey).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RelationshipEntry relationshipEntry = enumerator.Current;
						if (relationshipEntry.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey))
						{
							list.Add(relationshipEntry);
						}
					}
					goto IL_B4;
				}
			}
			if (mergeOption == MergeOption.PreserveChanges)
			{
				foreach (RelationshipEntry relationshipEntry2 in context.ObjectStateManager.FindRelationshipsByKey(sourceKey))
				{
					if (relationshipEntry2.IsSameAssociationSetAndRole(associationSet, sourceMember, sourceKey) && relationshipEntry2.State != EntityState.Added)
					{
						list.Add(relationshipEntry2);
					}
				}
			}
			IL_B4:
			foreach (RelationshipEntry relationshipToRemove in list)
			{
				ObjectStateManager.RemoveRelatedEndsAndDetachRelationship(relationshipToRemove, true);
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00050988 File Offset: 0x0004EB88
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
						RelationshipMultiplicity relationshipMultiplicity = targetMember.RelationshipMultiplicity;
						if (relationshipMultiplicity > RelationshipMultiplicity.One)
						{
							if (relationshipMultiplicity != RelationshipMultiplicity.Many)
							{
							}
						}
						else
						{
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
								if (state != EntityState.Unchanged)
								{
									if (state != EntityState.Added)
									{
										if (state == EntityState.Deleted)
										{
											newEntryState = EntityState.Deleted;
											if (list == null)
											{
												list = new List<RelationshipEntry>(16);
											}
											list.Add(relationshipEntry);
										}
									}
									else
									{
										newEntryState = EntityState.Deleted;
									}
								}
								else
								{
									if (list == null)
									{
										list = new List<RelationshipEntry>(16);
									}
									list.Add(relationshipEntry);
								}
								break;
							}
							}
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

		// Token: 0x060017EB RID: 6123 RVA: 0x00050BF8 File Offset: 0x0004EDF8
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

		// Token: 0x060017EC RID: 6124 RVA: 0x00050C24 File Offset: 0x0004EE24
		private static void UnloadReferenceRelatedEnds(RelationshipEntry relationshipEntry)
		{
			ObjectStateManager objectStateManager = relationshipEntry.ObjectStateManager;
			ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = relationshipEntry.RelationshipWrapper.AssociationEndMembers;
			ObjectStateManager.UnloadReferenceRelatedEnds(objectStateManager, relationshipEntry, relationshipEntry.RelationshipWrapper.GetEntityKey(0), associationEndMembers[1].Name);
			ObjectStateManager.UnloadReferenceRelatedEnds(objectStateManager, relationshipEntry, relationshipEntry.RelationshipWrapper.GetEntityKey(1), associationEndMembers[0].Name);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00050C84 File Offset: 0x0004EE84
		private static void UnloadReferenceRelatedEnds(ObjectStateManager cache, RelationshipEntry relationshipEntry, EntityKey sourceEntityKey, string targetRoleName)
		{
			EntityEntry entityEntry = cache.GetEntityEntry(sourceEntityKey);
			if (entityEntry.WrappedEntity.Entity != null)
			{
				EntityReference entityReference = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEndInternal(((AssociationSet)relationshipEntry.EntitySet).ElementType.FullName, targetRoleName) as EntityReference;
				if (entityReference != null)
				{
					entityReference.SetIsLoaded(false);
				}
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00050CDC File Offset: 0x0004EEDC
		internal EntityEntry AttachEntry(EntityKey entityKey, IEntityWrapper wrappedObject, EntitySet entitySet, string argumentName)
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

		// Token: 0x060017EF RID: 6127 RVA: 0x00050D48 File Offset: 0x0004EF48
		private void CheckKeyMatchesEntity(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySetForType, bool forAttach)
		{
			EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
			if (entitySet == null)
			{
				throw EntityUtil.InvalidKey();
			}
			entityKey.ValidateEntityKey(this._metadataWorkspace, entitySet);
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this.GetOrAddStateManagerTypeMetadata(wrappedEntity.IdentityType, entitySetForType);
			for (int i = 0; i < entitySet.ElementType.KeyMembers.Count; i++)
			{
				EdmMember edmMember = entitySet.ElementType.KeyMembers[i];
				int ordinalforCLayerMemberName = orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(edmMember.Name);
				if (ordinalforCLayerMemberName < 0)
				{
					throw EntityUtil.InvalidKey();
				}
				object value = orAddStateManagerTypeMetadata.Member(ordinalforCLayerMemberName).GetValue(wrappedEntity.Entity);
				object y = entityKey.FindValueByName(edmMember.Name);
				if (!ByValueEqualityComparer.Default.Equals(value, y))
				{
					throw EntityUtil.KeyPropertyDoesntMatchValueInKey(forAttach);
				}
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00050E08 File Offset: 0x0004F008
		internal RelationshipEntry AddNewRelation(RelationshipWrapper wrapper, EntityState desiredState)
		{
			RelationshipEntry relationshipEntry = new RelationshipEntry(this, desiredState, wrapper);
			this.AddRelationshipEntryToDictionary(relationshipEntry, desiredState);
			this.AddRelationshipToLookup(relationshipEntry);
			return relationshipEntry;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00050E30 File Offset: 0x0004F030
		internal RelationshipEntry AddRelation(RelationshipWrapper wrapper, EntityState desiredState)
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

		// Token: 0x060017F2 RID: 6130 RVA: 0x00050E88 File Offset: 0x0004F088
		private void AddRelationshipToLookup(RelationshipEntry relationship)
		{
			this.AddRelationshipEndToLookup(relationship.RelationshipWrapper.Key0, relationship);
			if (!relationship.RelationshipWrapper.Key0.Equals(relationship.RelationshipWrapper.Key1))
			{
				this.AddRelationshipEndToLookup(relationship.RelationshipWrapper.Key1, relationship);
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		private void AddRelationshipEndToLookup(EntityKey key, RelationshipEntry relationship)
		{
			EntityEntry entityEntry = this.GetEntityEntry(key);
			entityEntry.AddRelationshipEnd(relationship);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		private void DeleteRelationshipFromLookup(RelationshipEntry relationship)
		{
			this.DeleteRelationshipEndFromLookup(relationship.RelationshipWrapper.Key0, relationship);
			if (!relationship.RelationshipWrapper.Key0.Equals(relationship.RelationshipWrapper.Key1))
			{
				this.DeleteRelationshipEndFromLookup(relationship.RelationshipWrapper.Key1, relationship);
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00050F44 File Offset: 0x0004F144
		private void DeleteRelationshipEndFromLookup(EntityKey key, RelationshipEntry relationship)
		{
			EntityEntry entityEntry = this.GetEntityEntry(key);
			entityEntry.RemoveRelationshipEnd(relationship);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00050F60 File Offset: 0x0004F160
		internal RelationshipEntry FindRelationship(RelationshipSet relationshipSet, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
		{
			if (roleAndKey1.Value == null || roleAndKey2.Value == null)
			{
				return null;
			}
			return this.FindRelationship(new RelationshipWrapper((AssociationSet)relationshipSet, roleAndKey1, roleAndKey2));
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00050F8C File Offset: 0x0004F18C
		internal RelationshipEntry FindRelationship(RelationshipWrapper relationshipWrapper)
		{
			RelationshipEntry result = null;
			if ((this._unchangedRelationshipStore == null || !this._unchangedRelationshipStore.TryGetValue(relationshipWrapper, out result)) && (this._deletedRelationshipStore == null || !this._deletedRelationshipStore.TryGetValue(relationshipWrapper, out result)))
			{
				bool flag = this._addedRelationshipStore != null && this._addedRelationshipStore.TryGetValue(relationshipWrapper, out result);
			}
			return result;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00050FEC File Offset: 0x0004F1EC
		internal RelationshipEntry DeleteRelationship(RelationshipSet relationshipSet, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
		{
			RelationshipEntry relationshipEntry = this.FindRelationship(relationshipSet, roleAndKey1, roleAndKey2);
			if (relationshipEntry != null)
			{
				relationshipEntry.Delete(false);
			}
			return relationshipEntry;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0005100E File Offset: 0x0004F20E
		internal void DeleteKeyEntry(EntityEntry keyEntry)
		{
			if (keyEntry != null && keyEntry.IsKeyEntry)
			{
				this.ChangeState(keyEntry, keyEntry.State, EntityState.Detached);
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0005102C File Offset: 0x0004F22C
		internal RelationshipEntry[] CopyOfRelationshipsByKey(EntityKey key)
		{
			return this.FindRelationshipsByKey(key).ToArray();
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00051048 File Offset: 0x0004F248
		internal EntityEntry.RelationshipEndEnumerable FindRelationshipsByKey(EntityKey key)
		{
			return new EntityEntry.RelationshipEndEnumerable(this.FindEntityEntry(key));
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00051056 File Offset: 0x0004F256
		IEnumerable<IEntityStateEntry> IEntityStateManager.FindRelationshipsByKey(EntityKey key)
		{
			return this.FindRelationshipsByKey(key);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00051064 File Offset: 0x0004F264
		[Conditional("DEBUG")]
		private void ValidateKeylessEntityStore()
		{
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
			Dictionary<EntityKey, EntityEntry>[] array = new Dictionary<EntityKey, EntityEntry>[]
			{
				this._unchangedEntityStore,
				this._modifiedEntityStore,
				this._addedEntityStore,
				this._deletedEntityStore
			};
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

		// Token: 0x060017FE RID: 6142 RVA: 0x000511FC File Offset: 0x0004F3FC
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

		// Token: 0x060017FF RID: 6143 RVA: 0x00051223 File Offset: 0x0004F423
		public IEnumerable<ObjectStateEntry> GetObjectStateEntries(EntityState state)
		{
			if ((EntityState.Detached & state) != (EntityState)0)
			{
				throw EntityUtil.DetachedObjectStateEntriesDoesNotExistInObjectStateManager();
			}
			return this.GetObjectStateEntriesInternal(state);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00051237 File Offset: 0x0004F437
		IEnumerable<IEntityStateEntry> IEntityStateManager.GetEntityStateEntries(EntityState state)
		{
			foreach (ObjectStateEntry objectStateEntry in this.GetObjectStateEntriesInternal(state))
			{
				yield return objectStateEntry;
			}
			ObjectStateEntry[] array = null;
			yield break;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00051250 File Offset: 0x0004F450
		internal int GetObjectStateEntriesCount(EntityState state)
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

		// Token: 0x06001802 RID: 6146 RVA: 0x00051324 File Offset: 0x0004F524
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

		// Token: 0x06001803 RID: 6147 RVA: 0x0005138C File Offset: 0x0004F58C
		private ObjectStateEntry[] GetObjectStateEntriesInternal(EntityState state)
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

		// Token: 0x06001804 RID: 6148 RVA: 0x00051618 File Offset: 0x0004F818
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

		// Token: 0x06001805 RID: 6149 RVA: 0x00051674 File Offset: 0x0004F874
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

		// Token: 0x06001806 RID: 6150 RVA: 0x000516E4 File Offset: 0x0004F8E4
		internal void FixupKey(EntityEntry entry)
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
					throw EntityUtil.CannotFixUpKeyToExistingValues();
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
				this.PromoteKeyEntry(entityEntry, entry.WrappedEntity, null, true, false, false, "AcceptChanges");
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

		// Token: 0x06001807 RID: 6151 RVA: 0x00051840 File Offset: 0x0004FA40
		internal void ReplaceKeyWithTemporaryKey(EntityEntry entry)
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

		// Token: 0x06001808 RID: 6152 RVA: 0x000518EC File Offset: 0x0004FAEC
		private void ResetEntityKey(EntityEntry entry, EntityKey value)
		{
			EntityKey entityKey = entry.WrappedEntity.EntityKey;
			if (entityKey == null || value.Equals(entityKey))
			{
				throw EntityUtil.AcceptChangesEntityKeyIsNotValid();
			}
			try
			{
				this._inRelationshipFixup = true;
				entry.WrappedEntity.EntityKey = value;
				EntityUtil.CheckEntityKeysMatch(entry.WrappedEntity, value);
			}
			finally
			{
				this._inRelationshipFixup = false;
			}
			entry.EntityKey = value;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00051960 File Offset: 0x0004FB60
		public ObjectStateEntry ChangeObjectState(object entity, EntityState entityState)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
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
					throw EntityUtil.NoEntryExistsForObject(entity);
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

		// Token: 0x0600180A RID: 6154 RVA: 0x000519E8 File Offset: 0x0004FBE8
		public ObjectStateEntry ChangeRelationshipState(object sourceEntity, object targetEntity, string navigationProperty, EntityState relationshipState)
		{
			EntityEntry entityEntry;
			EntityEntry targetEntry;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out targetEntry);
			EntityUtil.CheckStringArgument(navigationProperty, "navigationProperty");
			RelatedEnd relatedEnd = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEnd(navigationProperty, false);
			return this.ChangeRelationshipState(entityEntry, targetEntry, relatedEnd, relationshipState);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00051A2C File Offset: 0x0004FC2C
		public ObjectStateEntry ChangeRelationshipState<TEntity>(TEntity sourceEntity, object targetEntity, Expression<Func<TEntity, object>> navigationPropertySelector, EntityState relationshipState) where TEntity : class
		{
			EntityEntry entityEntry;
			EntityEntry targetEntry;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out targetEntry);
			bool throwArgumentException;
			string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(navigationPropertySelector, out throwArgumentException);
			RelatedEnd relatedEnd = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEnd(navigationProperty, throwArgumentException);
			return this.ChangeRelationshipState(entityEntry, targetEntry, relatedEnd, relationshipState);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00051A74 File Offset: 0x0004FC74
		public ObjectStateEntry ChangeRelationshipState(object sourceEntity, object targetEntity, string relationshipName, string targetRoleName, EntityState relationshipState)
		{
			EntityEntry entityEntry;
			EntityEntry targetEntry;
			this.VerifyParametersForChangeRelationshipState(sourceEntity, targetEntity, out entityEntry, out targetEntry);
			RelatedEnd relatedEndInternal = entityEntry.WrappedEntity.RelationshipManager.GetRelatedEndInternal(relationshipName, targetRoleName);
			return this.ChangeRelationshipState(entityEntry, targetEntry, relatedEndInternal, relationshipState);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00051AAC File Offset: 0x0004FCAC
		private ObjectStateEntry ChangeRelationshipState(EntityEntry sourceEntry, EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState relationshipState)
		{
			this.VerifyInitialStateForChangeRelationshipState(sourceEntry, targetEntry, relatedEnd, relationshipState);
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

		// Token: 0x0600180E RID: 6158 RVA: 0x00051B58 File Offset: 0x0004FD58
		private void VerifyParametersForChangeRelationshipState(object sourceEntity, object targetEntity, out EntityEntry sourceEntry, out EntityEntry targetEntry)
		{
			EntityUtil.CheckArgumentNull<object>(sourceEntity, "sourceEntity");
			EntityUtil.CheckArgumentNull<object>(targetEntity, "targetEntity");
			sourceEntry = this.GetEntityEntryByObjectOrEntityKey(sourceEntity);
			targetEntry = this.GetEntityEntryByObjectOrEntityKey(targetEntity);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00051B88 File Offset: 0x0004FD88
		private void VerifyInitialStateForChangeRelationshipState(EntityEntry sourceEntry, EntityEntry targetEntry, RelatedEnd relatedEnd, EntityState relationshipState)
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

		// Token: 0x06001810 RID: 6160 RVA: 0x00051C10 File Offset: 0x0004FE10
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

		// Token: 0x06001811 RID: 6161 RVA: 0x00051C88 File Offset: 0x0004FE88
		private EntityEntry GetEntityEntryByObjectOrEntityKey(object o)
		{
			EntityKey entityKey = o as EntityKey;
			EntityEntry entityEntry = (entityKey != null) ? this.FindEntityEntry(entityKey) : this.FindEntityEntry(o);
			if (entityEntry == null)
			{
				throw EntityUtil.NoEntryExistsForObject(o);
			}
			if (entityEntry.IsKeyEntry)
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotChangeRelationshipStateKeyEntry);
			}
			return entityEntry;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00051CD4 File Offset: 0x0004FED4
		IEntityStateEntry IEntityStateManager.GetEntityStateEntry(EntityKey key)
		{
			return this.GetEntityEntry(key);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00051CE0 File Offset: 0x0004FEE0
		public ObjectStateEntry GetObjectStateEntry(EntityKey key)
		{
			ObjectStateEntry result;
			if (!this.TryGetObjectStateEntry(key, out result))
			{
				throw EntityUtil.NoEntryExistForEntityKey();
			}
			return result;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00051D00 File Offset: 0x0004FF00
		internal EntityEntry GetEntityEntry(EntityKey key)
		{
			EntityEntry result;
			if (!this.TryGetEntityEntry(key, out result))
			{
				throw EntityUtil.NoEntryExistForEntityKey();
			}
			return result;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00051D20 File Offset: 0x0004FF20
		public ObjectStateEntry GetObjectStateEntry(object entity)
		{
			ObjectStateEntry result;
			if (!this.TryGetObjectStateEntry(entity, out result))
			{
				throw EntityUtil.NoEntryExistsForObject(entity);
			}
			return result;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00051D40 File Offset: 0x0004FF40
		internal EntityEntry GetEntityEntry(object entity)
		{
			EntityEntry entityEntry = this.FindEntityEntry(entity);
			if (entityEntry == null)
			{
				throw EntityUtil.NoEntryExistsForObject(entity);
			}
			return entityEntry;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00051D60 File Offset: 0x0004FF60
		public bool TryGetObjectStateEntry(object entity, out ObjectStateEntry entry)
		{
			entry = null;
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			EntityKey entityKey = entity as EntityKey;
			if (entityKey != null)
			{
				return this.TryGetObjectStateEntry(entityKey, out entry);
			}
			entry = this.FindEntityEntry(entity);
			return entry != null;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		bool IEntityStateManager.TryGetEntityStateEntry(EntityKey key, out IEntityStateEntry entry)
		{
			ObjectStateEntry objectStateEntry;
			bool result = this.TryGetObjectStateEntry(key, out objectStateEntry);
			entry = objectStateEntry;
			return result;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00051DC0 File Offset: 0x0004FFC0
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

		// Token: 0x0600181A RID: 6170 RVA: 0x00051DE8 File Offset: 0x0004FFE8
		public bool TryGetObjectStateEntry(EntityKey key, out ObjectStateEntry entry)
		{
			EntityEntry entityEntry;
			bool result = this.TryGetEntityEntry(key, out entityEntry);
			entry = entityEntry;
			return result;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00051E04 File Offset: 0x00050004
		internal bool TryGetEntityEntry(EntityKey key, out EntityEntry entry)
		{
			entry = null;
			EntityUtil.CheckArgumentNull<EntityKey>(key, "key");
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

		// Token: 0x0600181C RID: 6172 RVA: 0x00051E90 File Offset: 0x00050090
		internal EntityEntry FindEntityEntry(EntityKey key)
		{
			EntityEntry result = null;
			if (key != null)
			{
				this.TryGetEntityEntry(key, out result);
			}
			return result;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00051EB0 File Offset: 0x000500B0
		internal EntityEntry FindEntityEntry(object entity)
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
			if (entityEntry != null && entity != entityEntry.Entity)
			{
				entityEntry = null;
			}
			return entityEntry;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00051EF8 File Offset: 0x000500F8
		public RelationshipManager GetRelationshipManager(object entity)
		{
			RelationshipManager result;
			if (!this.TryGetRelationshipManager(entity, out result))
			{
				throw new InvalidOperationException(Strings.ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity);
			}
			return result;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00051F1C File Offset: 0x0005011C
		public bool TryGetRelationshipManager(object entity, out RelationshipManager relationshipManager)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			IEntityWithRelationships entityWithRelationships = entity as IEntityWithRelationships;
			if (entityWithRelationships != null)
			{
				relationshipManager = entityWithRelationships.RelationshipManager;
				if (relationshipManager == null)
				{
					throw EntityUtil.UnexpectedNullRelationshipManager();
				}
				if (relationshipManager.WrappedOwner.Entity != entity)
				{
					throw EntityUtil.InvalidRelationshipManagerOwner();
				}
			}
			else
			{
				IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingStateManager(entity, this);
				if (entityWrapper.Context == null)
				{
					relationshipManager = null;
					return false;
				}
				relationshipManager = entityWrapper.RelationshipManager;
			}
			return true;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00051F84 File Offset: 0x00050184
		internal void ChangeState(RelationshipEntry entry, EntityState oldState, EntityState newState)
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

		// Token: 0x06001821 RID: 6177 RVA: 0x00051FB0 File Offset: 0x000501B0
		internal void ChangeState(EntityEntry entry, EntityState oldState, EntityState newState)
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

		// Token: 0x06001822 RID: 6178 RVA: 0x00052084 File Offset: 0x00050284
		private void AddRelationshipEntryToDictionary(RelationshipEntry entry, EntityState state)
		{
			Dictionary<RelationshipWrapper, RelationshipEntry> dictionary = null;
			if (state != EntityState.Unchanged)
			{
				if (state != EntityState.Added)
				{
					if (state == EntityState.Deleted)
					{
						if (this._deletedRelationshipStore == null)
						{
							this._deletedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
						}
						dictionary = this._deletedRelationshipStore;
					}
				}
				else
				{
					if (this._addedRelationshipStore == null)
					{
						this._addedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
					}
					dictionary = this._addedRelationshipStore;
				}
			}
			else
			{
				if (this._unchangedRelationshipStore == null)
				{
					this._unchangedRelationshipStore = new Dictionary<RelationshipWrapper, RelationshipEntry>();
				}
				dictionary = this._unchangedRelationshipStore;
			}
			dictionary.Add(entry.RelationshipWrapper, entry);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00052100 File Offset: 0x00050300
		private void AddEntityEntryToDictionary(EntityEntry entry, EntityState state)
		{
			if (entry.RequiresAnyChangeTracking)
			{
				this._detectChangesNeeded = true;
			}
			Dictionary<EntityKey, EntityEntry> dictionary = null;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state == EntityState.Added)
					{
						if (this._addedEntityStore == null)
						{
							this._addedEntityStore = new Dictionary<EntityKey, EntityEntry>();
						}
						dictionary = this._addedEntityStore;
					}
				}
				else
				{
					if (this._unchangedEntityStore == null)
					{
						this._unchangedEntityStore = new Dictionary<EntityKey, EntityEntry>();
					}
					dictionary = this._unchangedEntityStore;
				}
			}
			else if (state != EntityState.Deleted)
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
			dictionary.Add(entry.EntityKey, entry);
			this.AddEntryToKeylessStore(entry);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000521BC File Offset: 0x000503BC
		private void AddEntryToKeylessStore(EntityEntry entry)
		{
			if (entry.Entity != null && !(entry.Entity is IEntityWithKey))
			{
				if (this._keylessEntityStore == null)
				{
					this._keylessEntityStore = new Dictionary<object, EntityEntry>(new ObjectReferenceEqualityComparer());
				}
				if (!this._keylessEntityStore.ContainsKey(entry.Entity))
				{
					this._keylessEntityStore.Add(entry.Entity, entry);
				}
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0005221C File Offset: 0x0005041C
		private void RemoveObjectStateEntryFromDictionary(RelationshipEntry entry, EntityState state)
		{
			Dictionary<RelationshipWrapper, RelationshipEntry> dictionary = null;
			if (state != EntityState.Unchanged)
			{
				if (state != EntityState.Added)
				{
					if (state == EntityState.Deleted)
					{
						dictionary = this._deletedRelationshipStore;
					}
				}
				else
				{
					dictionary = this._addedRelationshipStore;
				}
			}
			else
			{
				dictionary = this._unchangedRelationshipStore;
			}
			bool flag = dictionary.Remove(entry.RelationshipWrapper);
			if (dictionary.Count == 0)
			{
				if (state == EntityState.Unchanged)
				{
					this._unchangedRelationshipStore = null;
					return;
				}
				if (state == EntityState.Added)
				{
					this._addedRelationshipStore = null;
					return;
				}
				if (state != EntityState.Deleted)
				{
					return;
				}
				this._deletedRelationshipStore = null;
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0005228C File Offset: 0x0005048C
		private void RemoveObjectStateEntryFromDictionary(EntityEntry entry, EntityState state)
		{
			Dictionary<EntityKey, EntityEntry> dictionary = null;
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state == EntityState.Added)
					{
						dictionary = this._addedEntityStore;
					}
				}
				else
				{
					dictionary = this._unchangedEntityStore;
				}
			}
			else if (state != EntityState.Deleted)
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
			bool flag = dictionary.Remove(entry.EntityKey);
			this.RemoveEntryFromKeylessStore(entry.WrappedEntity);
			if (dictionary.Count == 0)
			{
				if (state <= EntityState.Added)
				{
					if (state == EntityState.Unchanged)
					{
						this._unchangedEntityStore = null;
						return;
					}
					if (state != EntityState.Added)
					{
						return;
					}
					this._addedEntityStore = null;
					return;
				}
				else
				{
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
				}
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0005232D File Offset: 0x0005052D
		internal void RemoveEntryFromKeylessStore(IEntityWrapper wrappedEntity)
		{
			if (wrappedEntity != null && wrappedEntity.Entity != null && !(wrappedEntity.Entity is IEntityWithKey))
			{
				this._keylessEntityStore.Remove(wrappedEntity.Entity);
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0005235C File Offset: 0x0005055C
		internal StateManagerTypeMetadata GetOrAddStateManagerTypeMetadata(Type entityType, EntitySet entitySet)
		{
			StateManagerTypeMetadata result;
			if (!this._metadataMapping.TryGetValue(new EntitySetQualifiedType(entityType, entitySet), out result))
			{
				result = this.AddStateManagerTypeMetadata(entitySet, (ObjectTypeMapping)this.MetadataWorkspace.GetMap(entityType.FullName, DataSpace.OSpace, DataSpace.OCSpace));
			}
			return result;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000523A0 File Offset: 0x000505A0
		internal StateManagerTypeMetadata GetOrAddStateManagerTypeMetadata(EdmType edmType)
		{
			StateManagerTypeMetadata result;
			if (!this._metadataStore.TryGetValue(edmType, out result))
			{
				result = this.AddStateManagerTypeMetadata(edmType, (ObjectTypeMapping)this.MetadataWorkspace.GetMap(edmType, DataSpace.OCSpace));
			}
			return result;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000523D8 File Offset: 0x000505D8
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
			throw EntityUtil.InvalidOperation(Strings.Mapping_CannotMapCLRTypeMultipleTimes(stateManagerTypeMetadata.CdmMetadata.EdmType.FullName));
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00052460 File Offset: 0x00050660
		private StateManagerTypeMetadata AddStateManagerTypeMetadata(EdmType edmType, ObjectTypeMapping mapping)
		{
			StateManagerTypeMetadata stateManagerTypeMetadata = new StateManagerTypeMetadata(edmType, mapping);
			this._metadataStore.Add(edmType, stateManagerTypeMetadata);
			return stateManagerTypeMetadata;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00052483 File Offset: 0x00050683
		internal void Dispose()
		{
			this._isDisposed = true;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x0005248C File Offset: 0x0005068C
		internal bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00052494 File Offset: 0x00050694
		internal void DetectChanges()
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
					this.DetectChangesInNavigationProperties(entityEntriesForDetectChanges);
					this.DetectChangesInScalarAndComplexProperties(entityEntriesForDetectChanges);
					this.DetectChangesInForeignKeys(entityEntriesForDetectChanges);
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

		// Token: 0x0600182F RID: 6191 RVA: 0x00052510 File Offset: 0x00050710
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
					throw EntityUtil.UnableToAddRelationshipWithDeletedEntity();
				}
				if (dictionary2 != null)
				{
					foreach (KeyValuePair<RelatedEnd, HashSet<EntityKey>> keyValuePair in dictionary2)
					{
						if ((entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified) && keyValuePair.Key.IsDependentEndOfReferentialConstraint(true) && keyValuePair.Value.Count > 0)
						{
							throw EntityUtil.CannotChangeReferentialConstraintProperty();
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
							throw EntityUtil.CannotChangeReferentialConstraintProperty();
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
										if (referentialConstraint.ToRole == entityReference2.FromEndProperty)
										{
											for (int i = 0; i < referentialConstraint.FromProperties.Count; i++)
											{
												EntityEntry.AddOrIncreaseCounter(properties, referentialConstraint.ToProperties[i].Name, dictionary5[referentialConstraint.FromProperties[i].Name].Key);
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

		// Token: 0x06001830 RID: 6192 RVA: 0x00052974 File Offset: 0x00050B74
		internal EntityKey GetPermanentKey(IEntityWrapper entityFrom, RelatedEnd relatedEndFrom, IEntityWrapper entityTo)
		{
			EntityKey entityKey = null;
			if (entityTo.ObjectStateEntry != null)
			{
				entityKey = entityTo.ObjectStateEntry.EntityKey;
			}
			if (entityKey == null || entityKey.IsTemporary)
			{
				entityKey = this.CreateEntityKey(this.GetEntitySetOfOtherEnd(entityFrom, relatedEndFrom), entityTo.Entity);
			}
			return entityKey;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000529C0 File Offset: 0x00050BC0
		private EntitySet GetEntitySetOfOtherEnd(IEntityWrapper entity, RelatedEnd relatedEnd)
		{
			AssociationSet associationSet = (AssociationSet)relatedEnd.RelationshipSet;
			EntitySet entitySet = associationSet.AssociationSetEnds[0].EntitySet;
			if (entitySet.Name != entity.EntityKey.EntitySetName)
			{
				return entitySet;
			}
			return associationSet.AssociationSetEnds[1].EntitySet;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00052A18 File Offset: 0x00050C18
		private void DetectChangesInForeignKeys(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
				{
					entityEntry.DetectChangesInForeignKeys();
				}
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00052A74 File Offset: 0x00050C74
		private void AlignChangesInRelationships(IList<EntityEntry> entries)
		{
			this.PerformDelete(entries);
			this.PerformAdd(entries);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00052A84 File Offset: 0x00050C84
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

		// Token: 0x06001835 RID: 6197 RVA: 0x00052CAC File Offset: 0x00050EAC
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

		// Token: 0x06001836 RID: 6198 RVA: 0x00052D78 File Offset: 0x00050F78
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
						Dictionary<RelatedEnd, HashSet<EntityKey>> dictionary;
						if (relatedEnd is EntityReference && transactionManager.DeletedRelationshipsByForeignKey.TryGetValue(entityEntry.WrappedEntity, out dictionary))
						{
							dictionary.TryGetValue(relatedEnd as EntityReference, out hashSet);
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
								EntityReference entityReference = relatedEnd as EntityReference;
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
									if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted)
									{
										break;
									}
									if (entityEntry.IsKeyEntry)
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
								EntityReference entityReference2 = relatedEnd as EntityReference;
								if (entityReference2 != null && this.IsReparentingReference(entityEntry.WrappedEntity, entityReference2))
								{
									this.TransactionManager.EntityBeingReparented = entityReference2.GetDependentEndOfReferentialConstraint(entityReference2.ReferenceValue.Entity);
								}
								try
								{
									relatedEnd.Remove(entityWrapper2, preserveForeignKey2);
								}
								finally
								{
									this.TransactionManager.EntityBeingReparented = null;
								}
								if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted)
								{
									break;
								}
								if (entityEntry.IsKeyEntry)
								{
									break;
								}
							}
						}
						if (entityEntry.State == EntityState.Detached || entityEntry.State == EntityState.Deleted)
						{
							break;
						}
						if (entityEntry.IsKeyEntry)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00053184 File Offset: 0x00051384
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

		// Token: 0x06001838 RID: 6200 RVA: 0x00053208 File Offset: 0x00051408
		private bool ShouldPreserveForeignKeyForDependent(IEntityWrapper entity, RelatedEnd relatedEnd, IEntityWrapper relatedEntity, HashSet<IEntityWrapper> entitiesToDelete)
		{
			bool flag = entitiesToDelete.Contains(relatedEntity);
			return !flag || (flag && !this.HasAddedReference(entity, relatedEnd as EntityReference));
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00053238 File Offset: 0x00051438
		private bool HasAddedReference(IEntityWrapper wrappedOwner, EntityReference reference)
		{
			HashSet<IEntityWrapper> hashSet = null;
			Dictionary<RelatedEnd, HashSet<IEntityWrapper>> dictionary;
			return reference != null && this.TransactionManager.AddedRelationshipsByGraph.TryGetValue(wrappedOwner, out dictionary) && dictionary.TryGetValue(reference, out hashSet) && hashSet.Count > 0;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00053278 File Offset: 0x00051478
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

		// Token: 0x0600183B RID: 6203 RVA: 0x00053314 File Offset: 0x00051514
		private void DetectChangesInNavigationProperties(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.WrappedEntity.RequiresRelationshipChangeTracking)
				{
					entityEntry.DetectChangesInRelationshipsOfSingleEntity();
				}
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00053368 File Offset: 0x00051568
		private void DetectChangesInScalarAndComplexProperties(IList<EntityEntry> entries)
		{
			foreach (EntityEntry entityEntry in entries)
			{
				if (entityEntry.State != EntityState.Added && (entityEntry.RequiresScalarChangeTracking || entityEntry.RequiresComplexChangeTracking))
				{
					entityEntry.DetectChangesInProperties(!entityEntry.RequiresScalarChangeTracking);
				}
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000533D4 File Offset: 0x000515D4
		internal EntityKey CreateEntityKey(EntitySet entitySet, object entity)
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
					throw EntityUtil.EntityTypeDoesNotMatchEntitySet(entity.GetType().FullName, entitySet.Name, "entity");
				}
				array[i] = orAddStateManagerTypeMetadata.Member(ordinalforCLayerMemberName).GetValue(entity);
				if (array[i] == null)
				{
					throw EntityUtil.NullKeyValue(name, entitySet.ElementType.Name);
				}
			}
			if (array.Length == 1)
			{
				return new EntityKey(entitySet, array[0]);
			}
			return new EntityKey(entitySet, array);
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00053496 File Offset: 0x00051696
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x0005349E File Offset: 0x0005169E
		internal object EntityInvokingFKSetter { get; set; }

		// Token: 0x04000A9A RID: 2714
		private const int _initialListSize = 16;

		// Token: 0x04000A9B RID: 2715
		private Dictionary<EntityKey, EntityEntry> _addedEntityStore;

		// Token: 0x04000A9C RID: 2716
		private Dictionary<EntityKey, EntityEntry> _modifiedEntityStore;

		// Token: 0x04000A9D RID: 2717
		private Dictionary<EntityKey, EntityEntry> _deletedEntityStore;

		// Token: 0x04000A9E RID: 2718
		private Dictionary<EntityKey, EntityEntry> _unchangedEntityStore;

		// Token: 0x04000A9F RID: 2719
		private Dictionary<object, EntityEntry> _keylessEntityStore;

		// Token: 0x04000AA0 RID: 2720
		private Dictionary<RelationshipWrapper, RelationshipEntry> _addedRelationshipStore;

		// Token: 0x04000AA1 RID: 2721
		private Dictionary<RelationshipWrapper, RelationshipEntry> _deletedRelationshipStore;

		// Token: 0x04000AA2 RID: 2722
		private Dictionary<RelationshipWrapper, RelationshipEntry> _unchangedRelationshipStore;

		// Token: 0x04000AA3 RID: 2723
		private readonly Dictionary<EdmType, StateManagerTypeMetadata> _metadataStore;

		// Token: 0x04000AA4 RID: 2724
		private readonly Dictionary<EntitySetQualifiedType, StateManagerTypeMetadata> _metadataMapping;

		// Token: 0x04000AA5 RID: 2725
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x04000AA6 RID: 2726
		private CollectionChangeEventHandler onObjectStateManagerChangedDelegate;

		// Token: 0x04000AA7 RID: 2727
		private CollectionChangeEventHandler onEntityDeletedDelegate;

		// Token: 0x04000AA8 RID: 2728
		private bool _inRelationshipFixup;

		// Token: 0x04000AA9 RID: 2729
		private bool _isDisposed;

		// Token: 0x04000AAA RID: 2730
		private ComplexTypeMaterializer _complexTypeMaterializer;

		// Token: 0x04000AAB RID: 2731
		private readonly Dictionary<EntityKey, HashSet<EntityEntry>> _danglingForeignKeys = new Dictionary<EntityKey, HashSet<EntityEntry>>();

		// Token: 0x04000AAC RID: 2732
		private HashSet<EntityEntry> _entriesWithConceptualNulls;

		// Token: 0x04000AAD RID: 2733
		private object _changingObject;

		// Token: 0x04000AAE RID: 2734
		private string _changingMember;

		// Token: 0x04000AAF RID: 2735
		private string _changingEntityMember;

		// Token: 0x04000AB0 RID: 2736
		private EntityState _changingState;

		// Token: 0x04000AB1 RID: 2737
		private bool _saveOriginalValues;

		// Token: 0x04000AB2 RID: 2738
		private object _changingOldValue;

		// Token: 0x04000AB3 RID: 2739
		private bool _detectChangesNeeded;
	}
}
