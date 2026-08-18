using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.Internal;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000196 RID: 406
	[DataContract]
	[Serializable]
	public abstract class RelatedEnd : IRelatedEnd
	{
		// Token: 0x06001D10 RID: 7440 RVA: 0x00063854 File Offset: 0x00061A54
		internal RelatedEnd()
		{
			this._wrappedOwner = EntityWrapperFactory.NullWrapper;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00063868 File Offset: 0x00061A68
		internal RelatedEnd(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			EntityUtil.CheckArgumentNull<IEntityWrapper>(wrappedOwner, "wrappedOwner");
			EntityUtil.CheckArgumentNull<object>(wrappedOwner.Entity, "wrappedOwner.Entity");
			EntityUtil.CheckArgumentNull<RelationshipNavigation>(navigation, "navigation");
			EntityUtil.CheckArgumentNull<IRelationshipFixer>(relationshipFixer, "fixer");
			this.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001D12 RID: 7442 RVA: 0x000638B9 File Offset: 0x00061AB9
		// (remove) Token: 0x06001D13 RID: 7443 RVA: 0x000638D8 File Offset: 0x00061AD8
		public event CollectionChangeEventHandler AssociationChanged
		{
			add
			{
				this.CheckOwnerNull();
				this._onAssociationChanged = (CollectionChangeEventHandler)Delegate.Combine(this._onAssociationChanged, value);
			}
			remove
			{
				this.CheckOwnerNull();
				this._onAssociationChanged = (CollectionChangeEventHandler)Delegate.Remove(this._onAssociationChanged, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001D14 RID: 7444 RVA: 0x000089D0 File Offset: 0x00006BD0
		// (remove) Token: 0x06001D15 RID: 7445 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual event CollectionChangeEventHandler AssociationChangedForObjectView
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x000638F7 File Offset: 0x00061AF7
		internal bool IsForeignKey
		{
			get
			{
				return ((AssociationType)this._relationMetadata).IsForeignKey;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00063909 File Offset: 0x00061B09
		internal RelationshipNavigation RelationshipNavigation
		{
			get
			{
				return this._navigation;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00063911 File Offset: 0x00061B11
		[SoapIgnore]
		[XmlIgnore]
		public string RelationshipName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.RelationshipName;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x00063924 File Offset: 0x00061B24
		[SoapIgnore]
		[XmlIgnore]
		public string SourceRoleName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.From;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00063937 File Offset: 0x00061B37
		[SoapIgnore]
		[XmlIgnore]
		public string TargetRoleName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.To;
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0006394A File Offset: 0x00061B4A
		IEnumerable IRelatedEnd.CreateSourceQuery()
		{
			this.CheckOwnerNull();
			return this.CreateSourceQueryInternal();
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00063958 File Offset: 0x00061B58
		internal IEntityWrapper WrappedOwner
		{
			get
			{
				return this._wrappedOwner;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x00063960 File Offset: 0x00061B60
		internal ObjectContext ObjectContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00013A81 File Offset: 0x00011C81
		internal virtual void BulkDeleteAll(List<object> list)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x00063968 File Offset: 0x00061B68
		[SoapIgnore]
		[XmlIgnore]
		public RelationshipSet RelationshipSet
		{
			get
			{
				this.CheckOwnerNull();
				return this._relationshipSet;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00063976 File Offset: 0x00061B76
		internal RelationshipType RelationMetadata
		{
			get
			{
				return this._relationMetadata;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0006397E File Offset: 0x00061B7E
		internal RelationshipEndMember ToEndMember
		{
			get
			{
				return this._toEndProperty;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00063986 File Offset: 0x00061B86
		internal bool UsingNoTracking
		{
			get
			{
				return this._usingNoTracking;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0006398E File Offset: 0x00061B8E
		internal MergeOption DefaultMergeOption
		{
			get
			{
				if (!this.UsingNoTracking)
				{
					return MergeOption.AppendOnly;
				}
				return MergeOption.NoTracking;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0006399B File Offset: 0x00061B9B
		internal RelationshipEndMember FromEndProperty
		{
			get
			{
				return this._fromEndProperty;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x000639A3 File Offset: 0x00061BA3
		[SoapIgnore]
		[XmlIgnore]
		public bool IsLoaded
		{
			get
			{
				this.CheckOwnerNull();
				return this._isLoaded;
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000639B1 File Offset: 0x00061BB1
		internal void SetIsLoaded(bool value)
		{
			this._isLoaded = value;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000639BC File Offset: 0x00061BBC
		internal ObjectQuery<TEntity> CreateSourceQuery<TEntity>(MergeOption mergeOption, out bool hasResults)
		{
			if (this._context == null)
			{
				hasResults = false;
				return null;
			}
			EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
			EntityState entityState;
			if (entityEntry == null)
			{
				if (!this.UsingNoTracking)
				{
					throw EntityUtil.InvalidEntityStateSource();
				}
				entityState = EntityState.Detached;
			}
			else
			{
				entityState = entityEntry.State;
			}
			if (entityState == EntityState.Added && (!this.IsForeignKey || !this.IsDependentEndOfReferentialConstraint(false)))
			{
				throw EntityUtil.InvalidEntityStateSource();
			}
			if ((entityState != EntityState.Detached || !this.UsingNoTracking) && entityState != EntityState.Modified && entityState != EntityState.Unchanged && entityState != EntityState.Deleted && entityState != EntityState.Added)
			{
				hasResults = false;
				return null;
			}
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			EntityUtil.CheckEntityKeyNull(entityKey);
			if (this._sourceQuery == null)
			{
				AssociationType associationType = (AssociationType)this._relationMetadata;
				EntitySet entitySet = ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._fromEndProperty.Name].EntitySet;
				EntitySet entitySet2 = ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._toEndProperty.Name].EntitySet;
				EntityType entityType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)this._toEndProperty);
				bool ofTypeRequired = false;
				if (!entitySet2.ElementType.EdmEquals(entityType) && !TypeSemantics.IsSubTypeOf(entitySet2.ElementType, entityType))
				{
					ofTypeRequired = true;
					TypeUsage ospaceTypeUsage = this.ObjectContext.MetadataWorkspace.GetOSpaceTypeUsage(TypeUsage.Create(entityType));
					entityType = (EntityType)ospaceTypeUsage.EdmType;
				}
				StringBuilder stringBuilder;
				if (associationType.IsForeignKey)
				{
					ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints[0];
					ReadOnlyMetadataCollection<EdmProperty> fromProperties = referentialConstraint.FromProperties;
					ReadOnlyMetadataCollection<EdmProperty> toProperties = referentialConstraint.ToProperties;
					if (referentialConstraint.ToRole.EdmEquals(this._toEndProperty))
					{
						stringBuilder = new StringBuilder("SELECT VALUE D FROM ");
						RelatedEnd.AppendEntitySet(stringBuilder, entitySet2, entityType, ofTypeRequired);
						stringBuilder.Append(" AS D WHERE ");
						AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
						this._sourceQueryParamProperties = fromProperties;
						for (int i = 0; i < toProperties.Count; i++)
						{
							if (i > 0)
							{
								stringBuilder.Append(" AND ");
							}
							stringBuilder.Append("D.[");
							stringBuilder.Append(toProperties[i].Name);
							stringBuilder.Append("] = @");
							stringBuilder.Append(aliasGenerator.Next());
						}
					}
					else
					{
						stringBuilder = new StringBuilder("SELECT VALUE P FROM ");
						RelatedEnd.AppendEntitySet(stringBuilder, entitySet2, entityType, ofTypeRequired);
						stringBuilder.Append(" AS P WHERE ");
						AliasGenerator aliasGenerator2 = new AliasGenerator("EntityKeyValue");
						this._sourceQueryParamProperties = toProperties;
						for (int j = 0; j < fromProperties.Count; j++)
						{
							if (j > 0)
							{
								stringBuilder.Append(" AND ");
							}
							stringBuilder.Append("P.[");
							stringBuilder.Append(fromProperties[j].Name);
							stringBuilder.Append("] = @");
							stringBuilder.Append(aliasGenerator2.Next());
						}
						this._sourceQuery = stringBuilder.ToString();
					}
				}
				else
				{
					stringBuilder = new StringBuilder("SELECT VALUE [TargetEntity] FROM (SELECT VALUE x FROM ");
					stringBuilder.Append("[");
					stringBuilder.Append(this._relationshipSet.EntityContainer.Name);
					stringBuilder.Append("].[");
					stringBuilder.Append(this._relationshipSet.Name);
					stringBuilder.Append("] AS x WHERE Key(x.[");
					stringBuilder.Append(this._fromEndProperty.Name);
					stringBuilder.Append("]) = ");
					RelatedEnd.AppendKeyParameterRow(stringBuilder, entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace).ElementType.KeyMembers);
					stringBuilder.Append(") AS [AssociationEntry] INNER JOIN ");
					RelatedEnd.AppendEntitySet(stringBuilder, entitySet2, entityType, ofTypeRequired);
					stringBuilder.Append(" AS [TargetEntity] ON Key([AssociationEntry].[");
					stringBuilder.Append(this._toEndProperty.Name);
					stringBuilder.Append("]) = Key(Ref([TargetEntity]))");
				}
				this._sourceQuery = stringBuilder.ToString();
			}
			ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(this._sourceQuery, this._context, mergeOption);
			AliasGenerator aliasGenerator3 = new AliasGenerator("EntityKeyValue");
			IEnumerable<EdmMember> enumerable = this._sourceQueryParamProperties ?? entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace).ElementType.KeyMembers;
			hasResults = true;
			using (IEnumerator<EdmMember> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmMember parameterMember = enumerator.Current;
					object obj;
					if (this._sourceQueryParamProperties == null)
					{
						obj = this._wrappedOwner.EntityKey.EntityKeyValues.Single((EntityKeyMember ekv) => ekv.Key == parameterMember.Name).Value;
					}
					else
					{
						EntityReference entityReference = this as EntityReference;
						if (entityReference != null && ForeignKeyFactory.IsConceptualNullKey(entityReference.CachedForeignKey))
						{
							obj = null;
						}
						else
						{
							obj = this.GetCurrentValueFromEntity(parameterMember);
						}
					}
					ObjectParameter objectParameter;
					if (obj == null)
					{
						EdmType edmType = parameterMember.TypeUsage.EdmType;
						Type type = Helper.IsPrimitiveType(edmType) ? ((PrimitiveType)edmType).ClrEquivalentType : ((ClrEnumType)this.ObjectContext.MetadataWorkspace.GetObjectSpaceType((EnumType)edmType)).ClrType;
						objectParameter = new ObjectParameter(aliasGenerator3.Next(), type);
						hasResults = false;
					}
					else
					{
						objectParameter = new ObjectParameter(aliasGenerator3.Next(), obj);
					}
					objectParameter.TypeUsage = Helper.GetModelTypeUsage(parameterMember);
					objectQuery.Parameters.Add(objectParameter);
				}
			}
			objectQuery.Parameters.SetReadOnly(true);
			return objectQuery;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00063F5C File Offset: 0x0006215C
		private object GetCurrentValueFromEntity(EdmMember member)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._context.ObjectStateManager.GetOrAddStateManagerTypeMetadata(member.DeclaringType);
			StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(member.Name));
			return stateManagerMemberMetadata.GetValue(this._wrappedOwner.Entity);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00063FA4 File Offset: 0x000621A4
		private static void AppendKeyParameterRow(StringBuilder sourceBuilder, IList<EdmMember> keyMembers)
		{
			sourceBuilder.Append("ROW(");
			AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
			int count = keyMembers.Count;
			for (int i = 0; i < count; i++)
			{
				string value = aliasGenerator.Next();
				sourceBuilder.Append("@");
				sourceBuilder.Append(value);
				sourceBuilder.Append(" AS ");
				sourceBuilder.Append(value);
				if (i < count - 1)
				{
					sourceBuilder.Append(",");
				}
			}
			sourceBuilder.Append(")");
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00064028 File Offset: 0x00062228
		private static void AppendEntitySet(StringBuilder sourceBuilder, EntitySet targetEntitySet, EntityType targetEntityType, bool ofTypeRequired)
		{
			if (ofTypeRequired)
			{
				sourceBuilder.Append("OfType(");
			}
			sourceBuilder.Append("[");
			sourceBuilder.Append(targetEntitySet.EntityContainer.Name);
			sourceBuilder.Append("].[");
			sourceBuilder.Append(targetEntitySet.Name);
			sourceBuilder.Append("]");
			if (ofTypeRequired)
			{
				sourceBuilder.Append(", [");
				if (targetEntityType.NamespaceName != string.Empty)
				{
					sourceBuilder.Append(targetEntityType.NamespaceName);
					sourceBuilder.Append("].[");
				}
				sourceBuilder.Append(targetEntityType.Name);
				sourceBuilder.Append("])");
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000640DC File Offset: 0x000622DC
		internal ObjectQuery<TEntity> ValidateLoad<TEntity>(MergeOption mergeOption, string relatedEndName, out bool hasResults)
		{
			ObjectQuery<TEntity> objectQuery = this.CreateSourceQuery<TEntity>(mergeOption, out hasResults);
			if (objectQuery == null)
			{
				throw EntityUtil.RelatedEndNotAttachedToContext(relatedEndName);
			}
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw EntityUtil.InvalidEntityStateLoad(relatedEndName);
			}
			if (this.UsingNoTracking != (mergeOption == MergeOption.NoTracking))
			{
				throw EntityUtil.MismatchedMergeOptionOnLoad(mergeOption);
			}
			if (this.UsingNoTracking)
			{
				if (this.IsLoaded)
				{
					throw EntityUtil.LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd();
				}
				if (!this.IsEmpty())
				{
					throw EntityUtil.LoadCalledOnNonEmptyNoTrackedRelatedEnd();
				}
			}
			return objectQuery;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00064163 File Offset: 0x00062363
		public void Load()
		{
			this.CheckOwnerNull();
			this.Load(this.DefaultMergeOption);
		}

		// Token: 0x06001D2D RID: 7469
		public abstract void Load(MergeOption mergeOption);

		// Token: 0x06001D2E RID: 7470 RVA: 0x00064178 File Offset: 0x00062378
		internal void DeferredLoad()
		{
			if (this._wrappedOwner != null && this._wrappedOwner != EntityWrapperFactory.NullWrapper && !this.IsLoaded && this._context != null && this._context.ContextOptions.LazyLoadingEnabled && !this._context.InMaterialization && this.CanDeferredLoad && (this.UsingNoTracking || (this._wrappedOwner.ObjectStateEntry != null && (this._wrappedOwner.ObjectStateEntry.State == EntityState.Unchanged || this._wrappedOwner.ObjectStateEntry.State == EntityState.Modified || (this._wrappedOwner.ObjectStateEntry.State == EntityState.Added && this.IsForeignKey && this.IsDependentEndOfReferentialConstraint(false))))))
			{
				this._context.ContextOptions.LazyLoadingEnabled = false;
				try
				{
					this.Load();
				}
				finally
				{
					this._context.ContextOptions.LazyLoadingEnabled = true;
				}
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x00017938 File Offset: 0x00015B38
		internal virtual bool CanDeferredLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00064284 File Offset: 0x00062484
		internal void Merge<TEntity>(IEnumerable<TEntity> collection, MergeOption mergeOption, bool setIsLoaded)
		{
			List<IEntityWrapper> list = collection as List<IEntityWrapper>;
			if (list == null)
			{
				list = new List<IEntityWrapper>();
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.TargetRoleName].EntitySet;
				foreach (TEntity tentity in collection)
				{
					IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext);
					if (mergeOption == MergeOption.NoTracking)
					{
						EntityWrapperFactory.UpdateNoTrackingWrapper(entityWrapper, this.ObjectContext, entitySet);
					}
					list.Add(entityWrapper);
				}
			}
			this.Merge<TEntity>(list, mergeOption, setIsLoaded);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006432C File Offset: 0x0006252C
		internal void Merge<TEntity>(List<IEntityWrapper> collection, MergeOption mergeOption, bool setIsLoaded)
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			EntityUtil.CheckEntityKeyNull(entityKey);
			ObjectStateManager.UpdateRelationships(this.ObjectContext, mergeOption, (AssociationSet)this.RelationshipSet, (AssociationEndMember)this.FromEndProperty, entityKey, this._wrappedOwner, (AssociationEndMember)this.ToEndMember, collection, setIsLoaded);
			if (setIsLoaded)
			{
				this._isLoaded = true;
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006438C File Offset: 0x0006258C
		void IRelatedEnd.Attach(IEntityWithRelationships entity)
		{
			((IRelatedEnd)this).Attach(entity);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00064395 File Offset: 0x00062595
		void IRelatedEnd.Attach(object entity)
		{
			this.CheckOwnerNull();
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			this.Attach(new IEntityWrapper[]
			{
				EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext)
			}, false);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000643C8 File Offset: 0x000625C8
		internal void Attach(IEnumerable<IEntityWrapper> wrappedEntities, bool allowCollection)
		{
			this.CheckOwnerNull();
			this.ValidateOwnerForAttach();
			int num = 0;
			List<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (IEntityWrapper entityWrapper in wrappedEntities)
			{
				this.ValidateEntityForAttach(entityWrapper, num++, allowCollection);
				list.Add(entityWrapper);
			}
			this._suppressEvents = true;
			try
			{
				this.Merge<IEntityWrapper>(list, MergeOption.OverwriteChanges, false);
				ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints.FirstOrDefault<ReferentialConstraint>();
				if (referentialConstraint != null)
				{
					ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
					EntityEntry entityEntry = objectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
					if (this.IsDependentEndOfReferentialConstraint(false))
					{
						if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(entityEntry.GetCurrentEntityValue), list[0].ObjectStateEntry.EntityKey))
						{
							throw EntityUtil.InconsistentReferentialConstraintProperties();
						}
					}
					else
					{
						foreach (IEntityWrapper wrappedEntity in list)
						{
							RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
							if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
							{
								EntityEntry @object = objectStateManager.FindEntityEntry(((EntityReference)otherEndOfRelationship).WrappedOwner.Entity);
								if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(@object.GetCurrentEntityValue), entityEntry.EntityKey))
								{
									throw EntityUtil.InconsistentReferentialConstraintProperties();
								}
							}
						}
					}
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00064584 File Offset: 0x00062784
		internal void ValidateOwnerForAttach()
		{
			if (this.ObjectContext == null || this.UsingNoTracking)
			{
				throw EntityUtil.InvalidOwnerStateForAttach();
			}
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.GetEntityEntry(this._wrappedOwner.Entity);
			if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged)
			{
				throw EntityUtil.InvalidOwnerStateForAttach();
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000645DC File Offset: 0x000627DC
		internal void ValidateEntityForAttach(IEntityWrapper wrappedEntity, int index, bool allowCollection)
		{
			if (wrappedEntity == null || wrappedEntity.Entity == null)
			{
				if (allowCollection)
				{
					throw EntityUtil.InvalidNthElementNullForAttach(index);
				}
				throw EntityUtil.ArgumentNull("wrappedEntity");
			}
			else
			{
				this.VerifyType(wrappedEntity);
				EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry == null || entityEntry.Entity != wrappedEntity.Entity)
				{
					if (allowCollection)
					{
						throw EntityUtil.InvalidNthElementContextForAttach(index);
					}
					throw EntityUtil.InvalidEntityContextForAttach();
				}
				else
				{
					if (entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified)
					{
						return;
					}
					if (allowCollection)
					{
						throw EntityUtil.InvalidNthElementStateForAttach(index);
					}
					throw EntityUtil.InvalidEntityStateForAttach();
				}
			}
		}

		// Token: 0x06001D37 RID: 7479
		internal abstract IEnumerable CreateSourceQueryInternal();

		// Token: 0x06001D38 RID: 7480 RVA: 0x0006466B File Offset: 0x0006286B
		void IRelatedEnd.Add(IEntityWithRelationships entity)
		{
			((IRelatedEnd)this).Add(entity);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x00064674 File Offset: 0x00062874
		void IRelatedEnd.Add(object entity)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			this.Add(EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext));
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x00064694 File Offset: 0x00062894
		internal void Add(IEntityWrapper wrappedEntity)
		{
			if (this._wrappedOwner.Entity != null)
			{
				this.Add(wrappedEntity, true);
				return;
			}
			this.DisconnectedAdd(wrappedEntity);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000646B3 File Offset: 0x000628B3
		bool IRelatedEnd.Remove(IEntityWithRelationships entity)
		{
			return ((IRelatedEnd)this).Remove(entity);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000646BC File Offset: 0x000628BC
		bool IRelatedEnd.Remove(object entity)
		{
			EntityUtil.CheckArgumentNull<object>(entity, "entity");
			this.DeferredLoad();
			return this.Remove(EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext), false);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000646E3 File Offset: 0x000628E3
		internal bool Remove(IEntityWrapper wrappedEntity, bool preserveForeignKey)
		{
			if (this._wrappedOwner.Entity == null)
			{
				return this.DisconnectedRemove(wrappedEntity);
			}
			if (this.ContainsEntity(wrappedEntity))
			{
				this.Remove(wrappedEntity, true, false, false, true, preserveForeignKey);
				return true;
			}
			return false;
		}

		// Token: 0x06001D3E RID: 7486
		internal abstract void DisconnectedAdd(IEntityWrapper wrappedEntity);

		// Token: 0x06001D3F RID: 7487
		internal abstract bool DisconnectedRemove(IEntityWrapper wrappedEntity);

		// Token: 0x06001D40 RID: 7488 RVA: 0x00064712 File Offset: 0x00062912
		internal void Add(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			if (this._context != null && !this.UsingNoTracking)
			{
				this.ValidateStateForAdd(this._wrappedOwner);
				this.ValidateStateForAdd(wrappedEntity);
			}
			this.Add(wrappedEntity, applyConstraints, false, false, true, true);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00064744 File Offset: 0x00062944
		internal void CheckRelationEntitySet(EntitySet set)
		{
			if (((AssociationSet)this._relationshipSet).AssociationSetEnds[this._navigation.To] != null && ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._navigation.To].EntitySet != set)
			{
				throw EntityUtil.EntitySetIsNotValidForRelationship(set.EntityContainer.Name, set.Name, this._navigation.To, this._relationshipSet.EntityContainer.Name, this._relationshipSet.Name);
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000647D8 File Offset: 0x000629D8
		internal void ValidateStateForAdd(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw EntityUtil.UnableToAddRelationshipWithDeletedEntity();
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00064810 File Offset: 0x00062A10
		internal void Add(IEntityWrapper wrappedTarget, bool applyConstraints, bool addRelationshipAsUnchanged, bool relationshipAlreadyExists, bool allowModifyingOtherEndOfRelationship, bool forceForeignKeyChanges)
		{
			if (!this.VerifyEntityForAdd(wrappedTarget, relationshipAlreadyExists))
			{
				return;
			}
			EntityKey entityKey = wrappedTarget.EntityKey;
			if (entityKey != null && this.ObjectContext != null)
			{
				this.CheckRelationEntitySet(entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace));
			}
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedTarget);
			if (this.ObjectContext == otherEndOfRelationship.ObjectContext && this.ObjectContext != null)
			{
				if (this.UsingNoTracking != otherEndOfRelationship.UsingNoTracking)
				{
					throw EntityUtil.CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(this.UsingNoTracking ? this._navigation.From : this._navigation.To);
				}
			}
			else if (this.ObjectContext != null && otherEndOfRelationship.ObjectContext != null)
			{
				if (!this.UsingNoTracking || !otherEndOfRelationship.UsingNoTracking)
				{
					throw EntityUtil.CannotCreateRelationshipEntitiesInDifferentContexts();
				}
				wrappedTarget.ResetContext(this.ObjectContext, this.GetTargetEntitySetFromRelationshipSet(), MergeOption.NoTracking);
			}
			else if ((this._context == null || this.UsingNoTracking) && otherEndOfRelationship.ObjectContext != null && !otherEndOfRelationship.UsingNoTracking)
			{
				otherEndOfRelationship.ValidateStateForAdd(otherEndOfRelationship.WrappedOwner);
			}
			otherEndOfRelationship.VerifyEntityForAdd(this._wrappedOwner, relationshipAlreadyExists);
			otherEndOfRelationship.VerifyMultiplicityConstraintsForAdd(!allowModifyingOtherEndOfRelationship);
			if (this.CheckIfNavigationPropertyContainsEntity(wrappedTarget))
			{
				this.AddToLocalCache(wrappedTarget, applyConstraints);
			}
			else
			{
				this.AddToCache(wrappedTarget, applyConstraints);
			}
			if (otherEndOfRelationship.CheckIfNavigationPropertyContainsEntity(this.WrappedOwner))
			{
				otherEndOfRelationship.AddToLocalCache(this._wrappedOwner, false);
			}
			else
			{
				otherEndOfRelationship.AddToCache(this._wrappedOwner, false);
			}
			RelatedEnd relatedEnd = null;
			IEntityWrapper entityWrapper = null;
			if (this.ObjectContext == otherEndOfRelationship.ObjectContext && this.ObjectContext != null)
			{
				if (!this.IsForeignKey && !relationshipAlreadyExists && !this.UsingNoTracking)
				{
					if (!this.ObjectContext.ObjectStateManager.TransactionManager.IsLocalPublicAPI && this.WrappedOwner.EntityKey != null && !this.WrappedOwner.EntityKey.IsTemporary && this.IsDependentEndOfReferentialConstraint(false))
					{
						addRelationshipAsUnchanged = true;
					}
					this.AddRelationshipToObjectStateManager(wrappedTarget, addRelationshipAsUnchanged, false);
				}
				if (wrappedTarget.RequiresRelationshipChangeTracking && (this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking || this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking || this.ObjectContext.ObjectStateManager.TransactionManager.IsDetectChanges))
				{
					this.AddToNavigationProperty(wrappedTarget);
					otherEndOfRelationship.AddToNavigationProperty(this._wrappedOwner);
				}
			}
			else if (this.ObjectContext != null || otherEndOfRelationship.ObjectContext != null)
			{
				if (this.ObjectContext == null)
				{
					relatedEnd = otherEndOfRelationship;
					entityWrapper = this._wrappedOwner;
				}
				else
				{
					relatedEnd = this;
					entityWrapper = wrappedTarget;
				}
				if (!relatedEnd.UsingNoTracking)
				{
					TransactionManager transactionManager = relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager;
					transactionManager.BeginAddTracking();
					try
					{
						bool flag = true;
						try
						{
							if (relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager.TrackProcessedEntities)
							{
								if (!relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager.WrappedEntities.ContainsKey(entityWrapper.Entity))
								{
									relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager.WrappedEntities.Add(entityWrapper.Entity, entityWrapper);
								}
								relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager.ProcessedEntities.Add(relatedEnd.WrappedOwner);
							}
							relatedEnd.AddGraphToObjectStateManager(entityWrapper, relationshipAlreadyExists, addRelationshipAsUnchanged, false);
							if (entityWrapper.RequiresRelationshipChangeTracking && this.TargetAccessor.HasProperty)
							{
								otherEndOfRelationship.AddToNavigationProperty(this._wrappedOwner);
							}
							flag = false;
						}
						finally
						{
							if (flag)
							{
								relatedEnd.WrappedOwner.Context.ObjectStateManager.DegradePromotedRelationships();
								relatedEnd.FixupOtherEndOfRelationshipForRemove(entityWrapper, false);
								relatedEnd.RemoveFromCache(entityWrapper, false, false);
								entityWrapper.RelationshipManager.NodeVisited = true;
								RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(entityWrapper);
								RelatedEnd.RemoveEntityFromObjectStateManager(entityWrapper);
							}
						}
					}
					finally
					{
						relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager.EndAddTracking();
					}
				}
			}
			if (this.ObjectContext != null && this.IsForeignKey && !this.ObjectContext.ObjectStateManager.TransactionManager.IsGraphUpdate)
			{
				if (this.IsDependentEndOfReferentialConstraint(false))
				{
					((EntityReference)this).UpdateForeignKeyValues(this._wrappedOwner, wrappedTarget, null, forceForeignKeyChanges);
				}
				else if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
				{
					((EntityReference)otherEndOfRelationship).UpdateForeignKeyValues(wrappedTarget, this._wrappedOwner, null, forceForeignKeyChanges);
				}
			}
			otherEndOfRelationship.OnAssociationChanged(CollectionChangeAction.Add, this._wrappedOwner.Entity);
			this.OnAssociationChanged(CollectionChangeAction.Add, wrappedTarget.Entity);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00064C8C File Offset: 0x00062E8C
		private void AddGraphToObjectStateManager(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists, bool addRelationshipAsUnchanged, bool doAttach)
		{
			this.AddEntityToObjectStateManager(wrappedEntity, doAttach);
			if (!relationshipAlreadyExists && this.ObjectContext != null && wrappedEntity.Context != null)
			{
				if (!this.IsForeignKey)
				{
					this.AddRelationshipToObjectStateManager(wrappedEntity, addRelationshipAsUnchanged, doAttach);
				}
				if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
				{
					this.UpdateSnapshotOfRelationships(wrappedEntity);
					if (doAttach)
					{
						EntityEntry entityEntry = this._context.ObjectStateManager.GetEntityEntry(wrappedEntity.Entity);
						wrappedEntity.RelationshipManager.CheckReferentialConstraintProperties(entityEntry);
					}
				}
			}
			RelatedEnd.WalkObjectGraphToIncludeAllRelatedEntities(wrappedEntity, addRelationshipAsUnchanged, doAttach);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00064D14 File Offset: 0x00062F14
		private void UpdateSnapshotOfRelationships(IEntityWrapper wrappedEntity)
		{
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
			if (!otherEndOfRelationship.ContainsEntity(this.WrappedOwner))
			{
				otherEndOfRelationship.AddToLocalCache(this.WrappedOwner, false);
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00064D44 File Offset: 0x00062F44
		internal void Remove(IEntityWrapper wrappedEntity, bool doFixup, bool deleteEntity, bool deleteOwner, bool applyReferentialConstraints, bool preserveForeignKey)
		{
			if (wrappedEntity.RequiresRelationshipChangeTracking && doFixup && this.TargetAccessor.HasProperty && !this.CheckIfNavigationPropertyContainsEntity(wrappedEntity))
			{
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
				otherEndOfRelationship.RemoveFromNavigationProperty(this.WrappedOwner);
			}
			if (!this.ContainsEntity(wrappedEntity))
			{
				return;
			}
			if (this._context != null && doFixup && applyReferentialConstraints && this.IsDependentEndOfReferentialConstraint(false))
			{
				RelatedEnd otherEndOfRelationship2 = this.GetOtherEndOfRelationship(wrappedEntity);
				otherEndOfRelationship2.Remove(this._wrappedOwner, doFixup, deleteEntity, deleteOwner, applyReferentialConstraints, preserveForeignKey);
				return;
			}
			bool flag = this.RemoveFromCache(wrappedEntity, false, preserveForeignKey);
			if (!this.UsingNoTracking && this.ObjectContext != null && !this.IsForeignKey)
			{
				RelatedEnd.MarkRelationshipAsDeletedInObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
			}
			if (doFixup)
			{
				this.FixupOtherEndOfRelationshipForRemove(wrappedEntity, preserveForeignKey);
				if ((this._context == null || !this._context.ObjectStateManager.TransactionManager.IsLocalPublicAPI) && this._context != null && (deleteEntity || (deleteOwner && RelatedEnd.CheckCascadeDeleteFlag(this._fromEndProperty)) || (applyReferentialConstraints && this.IsPrincipalEndOfReferentialConstraint())) && wrappedEntity.Entity != this._context.ObjectStateManager.TransactionManager.EntityBeingReparented && this._context.ObjectStateManager.EntityInvokingFKSetter != wrappedEntity.Entity)
				{
					this.EnsureRelationshipNavigationAccessorsInitialized();
					RelatedEnd.RemoveEntityFromRelatedEnds(wrappedEntity, this._wrappedOwner, this._navigation.Reverse);
					RelatedEnd.MarkEntityAsDeletedInObjectStateManager(wrappedEntity);
				}
			}
			if (flag)
			{
				this.OnAssociationChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00064EC8 File Offset: 0x000630C8
		internal bool IsDependentEndOfReferentialConstraint(bool checkIdentifying)
		{
			if (this._relationMetadata != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == this.FromEndProperty)
					{
						if (checkIdentifying)
						{
							EntityType entityType = referentialConstraint.ToRole.GetEntityType();
							return RelatedEnd.CheckIfAllPropertiesAreKeyProperties(entityType.KeyMemberNames, referentialConstraint.ToProperties);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00064F64 File Offset: 0x00063164
		internal bool IsPrincipalEndOfReferentialConstraint()
		{
			if (this._relationMetadata != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this._relationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.FromRole == this._fromEndProperty)
					{
						EntityType entityType = referentialConstraint.ToRole.GetEntityType();
						return RelatedEnd.CheckIfAllPropertiesAreKeyProperties(entityType.KeyMemberNames, referentialConstraint.ToProperties);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00064FF8 File Offset: 0x000631F8
		internal static bool CheckIfAllPropertiesAreKeyProperties(string[] keyMemberNames, ReadOnlyMetadataCollection<EdmProperty> toProperties)
		{
			foreach (EdmProperty edmProperty in toProperties)
			{
				bool flag = false;
				foreach (string a in keyMemberNames)
				{
					if (a == edmProperty.Name)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0006507C File Offset: 0x0006327C
		internal void IncludeEntity(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw EntityUtil.UnableToAddRelationshipWithDeletedEntity();
			}
			if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
			{
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
				this._context.GetTypeUsage(otherEndOfRelationship.WrappedOwner.IdentityType);
				EntityReference entityReference = otherEndOfRelationship as EntityReference;
				if (entityReference != null)
				{
					if (entityReference.NavigationPropertyIsNullOrMissing())
					{
						otherEndOfRelationship.AddToNavigationProperty(this._wrappedOwner);
						if (entityEntry != null && this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking && this.IsForeignKey && otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
						{
							otherEndOfRelationship.MarkForeignKeyPropertiesModified();
						}
					}
					else if (!entityReference.CheckIfNavigationPropertyContainsEntity(this._wrappedOwner))
					{
						throw new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(entityReference.RelationshipNavigation.To, entityReference.RelationshipNavigation.RelationshipName));
					}
				}
				else
				{
					otherEndOfRelationship.AddToNavigationProperty(this._wrappedOwner);
				}
			}
			if (entityEntry == null)
			{
				this.AddGraphToObjectStateManager(wrappedEntity, false, addRelationshipAsUnchanged, doAttach);
				return;
			}
			if (this.FindRelationshipEntryInObjectStateManager(wrappedEntity) == null)
			{
				EntityReference entityReference2 = this as EntityReference;
				if (entityReference2 != null && entityReference2.DetachedEntityKey != null)
				{
					EntityKey entityKey = wrappedEntity.EntityKey;
					if (entityReference2.DetachedEntityKey != entityKey)
					{
						if (entityKey.IsTemporary)
						{
							throw EntityUtil.CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(this._navigation.To);
						}
						throw EntityUtil.EntityKeyValueMismatch();
					}
				}
				if (this.ObjectContext != null && wrappedEntity.Context != null)
				{
					if (!this.IsForeignKey)
					{
						if (entityEntry.State == EntityState.Added)
						{
							this.AddRelationshipToObjectStateManager(wrappedEntity, addRelationshipAsUnchanged, false);
						}
						else
						{
							this.AddRelationshipToObjectStateManager(wrappedEntity, addRelationshipAsUnchanged, doAttach);
						}
					}
					if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
					{
						this.UpdateSnapshotOfRelationships(wrappedEntity);
						if (doAttach && entityEntry.State != EntityState.Added)
						{
							EntityEntry entityEntry2 = this.ObjectContext.ObjectStateManager.GetEntityEntry(wrappedEntity.Entity);
							wrappedEntity.RelationshipManager.CheckReferentialConstraintProperties(entityEntry2);
						}
					}
				}
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00065264 File Offset: 0x00063464
		internal void MarkForeignKeyPropertiesModified()
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints[0];
			EntityEntry objectStateEntry = this.WrappedOwner.ObjectStateEntry;
			if (objectStateEntry.State == EntityState.Unchanged || objectStateEntry.State == EntityState.Modified)
			{
				foreach (EdmProperty edmProperty in referentialConstraint.ToProperties)
				{
					objectStateEntry.SetModifiedProperty(edmProperty.Name);
				}
			}
		}

		// Token: 0x06001D4C RID: 7500
		internal abstract bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper);

		// Token: 0x06001D4D RID: 7501
		internal abstract void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper);

		// Token: 0x06001D4E RID: 7502 RVA: 0x000652F4 File Offset: 0x000634F4
		internal void AddToNavigationProperty(IEntityWrapper wrapper)
		{
			if (this.TargetAccessor.HasProperty && !this.CheckIfNavigationPropertyContainsEntity(wrapper))
			{
				TransactionManager transactionManager = wrapper.Context.ObjectStateManager.TransactionManager;
				if (transactionManager.IsAddTracking || transactionManager.IsAttachTracking)
				{
					wrapper.Context.ObjectStateManager.TrackPromotedRelationship(this, wrapper);
				}
				this.AddToObjectCache(wrapper);
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00065351 File Offset: 0x00063551
		internal void RemoveFromNavigationProperty(IEntityWrapper wrapper)
		{
			if (this.TargetAccessor.HasProperty && this.CheckIfNavigationPropertyContainsEntity(wrapper))
			{
				this.RemoveFromObjectCache(wrapper);
			}
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00065374 File Offset: 0x00063574
		internal void ExcludeEntity(IEntityWrapper wrappedEntity)
		{
			if (!this._context.ObjectStateManager.TransactionManager.TrackProcessedEntities || (!this._context.ObjectStateManager.TransactionManager.IsAttachTracking && !this._context.ObjectStateManager.TransactionManager.IsAddTracking) || this._context.ObjectStateManager.TransactionManager.ProcessedEntities.Contains(wrappedEntity))
			{
				EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry != null && entityEntry.State != EntityState.Deleted && !wrappedEntity.RelationshipManager.NodeVisited)
				{
					wrappedEntity.RelationshipManager.NodeVisited = true;
					RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(wrappedEntity);
					if (!this.IsForeignKey)
					{
						RelatedEnd.RemoveRelationshipFromObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
					}
					RelatedEnd.RemoveEntityFromObjectStateManager(wrappedEntity);
					return;
				}
				if (!this.IsForeignKey && this.FindRelationshipEntryInObjectStateManager(wrappedEntity) != null)
				{
					RelatedEnd.RemoveRelationshipFromObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
				}
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00065478 File Offset: 0x00063678
		internal RelationshipEntry FindRelationshipEntryInObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityKey entityKey = wrappedEntity.EntityKey;
			EntityKey entityKey2 = this._wrappedOwner.EntityKey;
			return this._context.ObjectStateManager.FindRelationship(this._relationshipSet, new KeyValuePair<string, EntityKey>(this._navigation.From, entityKey2), new KeyValuePair<string, EntityKey>(this._navigation.To, entityKey));
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000654D0 File Offset: 0x000636D0
		internal void Clear(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			this.ClearCollectionOrRef(wrappedEntity, navigation, doCascadeDelete);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000654DC File Offset: 0x000636DC
		internal bool CheckReferentialConstraintProperties(EntityEntry ownerEntry)
		{
			if (!this.IsEmpty() || ((this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One) && ((EntityReference)this).DetachedEntityKey != null))
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == this.FromEndProperty)
					{
						EntityKey principalKey;
						if (this.IsEmpty())
						{
							if (this.IsForeignKey && !this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking && !this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking)
							{
								principalKey = ((EntityReference)this).EntityKey;
							}
							else
							{
								principalKey = ((EntityReference)this).DetachedEntityKey;
							}
						}
						else
						{
							IEntityWrapper referenceValue = ((EntityReference)this).ReferenceValue;
							if (referenceValue.ObjectStateEntry != null && referenceValue.ObjectStateEntry.State == EntityState.Added)
							{
								return true;
							}
							principalKey = this.ExtractPrincipalKey(referenceValue);
						}
						if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(ownerEntry.GetCurrentEntityValue), principalKey))
						{
							return false;
						}
					}
					else if (referentialConstraint.FromRole == this.FromEndProperty)
					{
						if (this.IsEmpty())
						{
							EntityKey detachedEntityKey = ((EntityReference)this).DetachedEntityKey;
							if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(detachedEntityKey.FindValueByName), ownerEntry.EntityKey))
							{
								return false;
							}
						}
						else
						{
							foreach (IEntityWrapper entityWrapper in this.GetWrappedEntities())
							{
								EntityEntry objectStateEntry = entityWrapper.ObjectStateEntry;
								if (objectStateEntry != null && objectStateEntry.State != EntityState.Added && objectStateEntry.State != EntityState.Deleted && objectStateEntry.State != EntityState.Detached && !RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(objectStateEntry.GetCurrentEntityValue), ownerEntry.EntityKey))
								{
									return false;
								}
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00065720 File Offset: 0x00063920
		private EntityKey ExtractPrincipalKey(IEntityWrapper wrappedRelatedEntity)
		{
			EntitySet targetEntitySetFromRelationshipSet = this.GetTargetEntitySetFromRelationshipSet();
			EntityKey entityKey = wrappedRelatedEntity.EntityKey;
			if (entityKey != null && !entityKey.IsTemporary)
			{
				EntityUtil.ValidateEntitySetInKey(entityKey, targetEntitySetFromRelationshipSet);
				entityKey.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, targetEntitySetFromRelationshipSet);
			}
			else
			{
				entityKey = this._context.ObjectStateManager.CreateEntityKey(targetEntitySetFromRelationshipSet, wrappedRelatedEntity.Entity);
			}
			return entityKey;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0006577C File Offset: 0x0006397C
		internal static bool VerifyRIConstraintsWithRelatedEntry(ReferentialConstraint constraint, Func<string, object> getDependentPropertyValue, EntityKey principalKey)
		{
			for (int i = 0; i < constraint.FromProperties.Count; i++)
			{
				string name = constraint.FromProperties[i].Name;
				string name2 = constraint.ToProperties[i].Name;
				object x = principalKey.FindValueByName(name);
				object y = getDependentPropertyValue(name2);
				if (!ByValueEqualityComparer.Default.Equals(x, y))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000657E6 File Offset: 0x000639E6
		public IEnumerator GetEnumerator()
		{
			if (this is EntityReference)
			{
				this.CheckOwnerNull();
			}
			this.DeferredLoad();
			return this.GetInternalEnumerable().GetEnumerator();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00065808 File Offset: 0x00063A08
		internal void RemoveAll()
		{
			List<IEntityWrapper> list = null;
			bool flag = false;
			try
			{
				this._suppressEvents = true;
				foreach (IEntityWrapper item in this.GetWrappedEntities())
				{
					if (list == null)
					{
						list = new List<IEntityWrapper>();
					}
					list.Add(item);
				}
				if (flag = (list != null && list.Count > 0))
				{
					foreach (IEntityWrapper wrappedEntity in list)
					{
						this.Remove(wrappedEntity, true, false, true, true, false);
					}
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			if (flag)
			{
				this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
			}
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000658E4 File Offset: 0x00063AE4
		internal void DetachAll(EntityState ownerEntityState)
		{
			List<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (IEntityWrapper item in this.GetWrappedEntities())
			{
				list.Add(item);
			}
			bool flag = ownerEntityState == EntityState.Added || this._fromEndProperty.RelationshipMultiplicity == RelationshipMultiplicity.Many;
			foreach (IEntityWrapper wrappedEntity in list)
			{
				if (!this.ContainsEntity(wrappedEntity))
				{
					return;
				}
				EntityReference entityReference = this as EntityReference;
				if (entityReference != null)
				{
					entityReference.DetachedEntityKey = entityReference.AttachedEntityKey;
				}
				if (flag)
				{
					RelatedEnd.DetachRelationshipFromObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
				}
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
				otherEndOfRelationship.RemoveFromCache(this._wrappedOwner, true, false);
				otherEndOfRelationship.OnAssociationChanged(CollectionChangeAction.Remove, this._wrappedOwner.Entity);
			}
			if (this.IsForeignKey)
			{
				EntityReference entityReference2 = this as EntityReference;
				if (entityReference2 != null)
				{
					entityReference2.DetachedEntityKey = null;
				}
			}
			foreach (IEntityWrapper wrappedEntity2 in list)
			{
				RelatedEnd otherEndOfRelationship2 = this.GetOtherEndOfRelationship(wrappedEntity2);
				this.RemoveFromCache(wrappedEntity2, false, false);
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00065A6C File Offset: 0x00063C6C
		internal void AddToCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			this.AddToLocalCache(wrappedEntity, applyConstraints);
			this.AddToObjectCache(wrappedEntity);
		}

		// Token: 0x06001D5A RID: 7514
		internal abstract void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints);

		// Token: 0x06001D5B RID: 7515
		internal abstract void AddToObjectCache(IEntityWrapper wrappedEntity);

		// Token: 0x06001D5C RID: 7516 RVA: 0x00065A80 File Offset: 0x00063C80
		internal bool RemoveFromCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			bool result = this.RemoveFromLocalCache(wrappedEntity, resetIsLoaded, preserveForeignKey);
			this.RemoveFromObjectCache(wrappedEntity);
			return result;
		}

		// Token: 0x06001D5D RID: 7517
		internal abstract bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey);

		// Token: 0x06001D5E RID: 7518
		internal abstract bool RemoveFromObjectCache(IEntityWrapper wrappedEntity);

		// Token: 0x06001D5F RID: 7519
		internal abstract bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists);

		// Token: 0x06001D60 RID: 7520
		internal abstract void VerifyType(IEntityWrapper wrappedEntity);

		// Token: 0x06001D61 RID: 7521
		internal abstract bool CanSetEntityType(IEntityWrapper wrappedEntity);

		// Token: 0x06001D62 RID: 7522
		internal abstract void Include(bool addRelationshipAsUnchanged, bool doAttach);

		// Token: 0x06001D63 RID: 7523
		internal abstract void Exclude();

		// Token: 0x06001D64 RID: 7524
		internal abstract void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete);

		// Token: 0x06001D65 RID: 7525
		internal abstract bool ContainsEntity(IEntityWrapper wrappedEntity);

		// Token: 0x06001D66 RID: 7526
		internal abstract IEnumerable GetInternalEnumerable();

		// Token: 0x06001D67 RID: 7527
		internal abstract IEnumerable<IEntityWrapper> GetWrappedEntities();

		// Token: 0x06001D68 RID: 7528
		internal abstract void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> keyValues, HashSet<object> visited);

		// Token: 0x06001D69 RID: 7529
		internal abstract bool IsEmpty();

		// Token: 0x06001D6A RID: 7530
		internal abstract void OnRelatedEndClear();

		// Token: 0x06001D6B RID: 7531
		internal abstract void ClearWrappedValues();

		// Token: 0x06001D6C RID: 7532
		internal abstract void VerifyMultiplicityConstraintsForAdd(bool applyConstraints);

		// Token: 0x06001D6D RID: 7533 RVA: 0x00065AA0 File Offset: 0x00063CA0
		internal virtual void OnAssociationChanged(CollectionChangeAction collectionChangeAction, object entity)
		{
			if (!this._suppressEvents && this._onAssociationChanged != null)
			{
				this._onAssociationChanged(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00065AC8 File Offset: 0x00063CC8
		private void AddEntityToObjectStateManager(IEntityWrapper wrappedEntity, bool doAttach)
		{
			EntitySet targetEntitySetFromRelationshipSet = this.GetTargetEntitySetFromRelationshipSet();
			if (!doAttach)
			{
				this._context.AddSingleObject(targetEntitySetFromRelationshipSet, wrappedEntity, "entity");
			}
			else
			{
				this._context.AttachSingleObject(wrappedEntity, targetEntitySetFromRelationshipSet, "entity");
			}
			EntityReference entityReference = this as EntityReference;
			if (entityReference != null && entityReference.DetachedEntityKey != null)
			{
				EntityKey entityKey = wrappedEntity.EntityKey;
				if (entityReference.DetachedEntityKey != entityKey)
				{
					throw EntityUtil.EntityKeyValueMismatch();
				}
			}
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00065B38 File Offset: 0x00063D38
		internal EntitySet GetTargetEntitySetFromRelationshipSet()
		{
			AssociationSet associationSet = (AssociationSet)this._relationshipSet;
			AssociationEndMember associationEndMember = (AssociationEndMember)this.ToEndMember;
			return associationSet.AssociationSetEnds[associationEndMember.Name].EntitySet;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00065B78 File Offset: 0x00063D78
		private RelationshipEntry AddRelationshipToObjectStateManager(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			EntityKey entityKey2 = wrappedEntity.EntityKey;
			EntityUtil.CheckEntityKeyNull(entityKey);
			EntityUtil.CheckEntityKeyNull(entityKey2);
			return this.ObjectContext.ObjectStateManager.AddRelation(new RelationshipWrapper((AssociationSet)this._relationshipSet, new KeyValuePair<string, EntityKey>(this._navigation.From, entityKey), new KeyValuePair<string, EntityKey>(this._navigation.To, entityKey2)), (addRelationshipAsUnchanged || doAttach) ? EntityState.Unchanged : EntityState.Added);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00065BF0 File Offset: 0x00063DF0
		private static void WalkObjectGraphToIncludeAllRelatedEntities(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity.RelationshipManager.Relationships)
			{
				relatedEnd.Include(addRelationshipAsUnchanged, doAttach);
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00065C44 File Offset: 0x00063E44
		internal static void RemoveEntityFromObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry;
			if (wrappedEntity.Context != null && wrappedEntity.Context.ObjectStateManager.TransactionManager.IsAttachTracking && wrappedEntity.Context.ObjectStateManager.TransactionManager.PromotedKeyEntries.TryGetValue(wrappedEntity.Entity, out entityEntry))
			{
				entityEntry.DegradeEntry();
				return;
			}
			entityEntry = RelatedEnd.MarkEntityAsDeletedInObjectStateManager(wrappedEntity);
			if (entityEntry != null && entityEntry.State != EntityState.Detached)
			{
				entityEntry.AcceptChanges();
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00065CB4 File Offset: 0x00063EB4
		private static void RemoveRelationshipFromObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			RelationshipEntry relationshipEntry = RelatedEnd.MarkRelationshipAsDeletedInObjectStateManager(wrappedEntity, wrappedOwner, relationshipSet, navigation);
			if (relationshipEntry != null && relationshipEntry.State != EntityState.Detached)
			{
				relationshipEntry.AcceptChanges();
			}
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00065CE0 File Offset: 0x00063EE0
		private void FixupOtherEndOfRelationshipForRemove(IEntityWrapper wrappedEntity, bool preserveForeignKey)
		{
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
			otherEndOfRelationship.Remove(this._wrappedOwner, false, false, false, false, preserveForeignKey);
			otherEndOfRelationship.RemoveFromNavigationProperty(this._wrappedOwner);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00065D14 File Offset: 0x00063F14
		private static EntityEntry MarkEntityAsDeletedInObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry = null;
			if (wrappedEntity.Context != null)
			{
				entityEntry = wrappedEntity.Context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry != null)
				{
					entityEntry.Delete(false);
				}
			}
			return entityEntry;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00065D50 File Offset: 0x00063F50
		private static RelationshipEntry MarkRelationshipAsDeletedInObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			RelationshipEntry result = null;
			if (wrappedOwner.Context != null && wrappedEntity.Context != null && relationshipSet != null)
			{
				EntityKey entityKey = wrappedOwner.EntityKey;
				EntityKey entityKey2 = wrappedEntity.EntityKey;
				result = wrappedEntity.Context.ObjectStateManager.DeleteRelationship(relationshipSet, new KeyValuePair<string, EntityKey>(navigation.From, entityKey), new KeyValuePair<string, EntityKey>(navigation.To, entityKey2));
			}
			return result;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00065DAC File Offset: 0x00063FAC
		private static void DetachRelationshipFromObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			if (wrappedOwner.Context != null && wrappedEntity.Context != null && relationshipSet != null)
			{
				EntityKey entityKey = wrappedOwner.EntityKey;
				EntityKey entityKey2 = wrappedEntity.EntityKey;
				RelationshipEntry relationshipEntry = wrappedEntity.Context.ObjectStateManager.FindRelationship(relationshipSet, new KeyValuePair<string, EntityKey>(navigation.From, entityKey), new KeyValuePair<string, EntityKey>(navigation.To, entityKey2));
				if (relationshipEntry != null)
				{
					relationshipEntry.DetachRelationshipEntry();
				}
			}
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00065E10 File Offset: 0x00064010
		private static void RemoveEntityFromRelatedEnds(IEntityWrapper wrappedEntity1, IEntityWrapper wrappedEntity2, RelationshipNavigation navigation)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity1.RelationshipManager.Relationships)
			{
				bool doCascadeDelete = RelatedEnd.CheckCascadeDeleteFlag(relatedEnd.FromEndProperty) || relatedEnd.IsPrincipalEndOfReferentialConstraint();
				relatedEnd.Clear(wrappedEntity2, navigation, doCascadeDelete);
			}
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00065E80 File Offset: 0x00064080
		private static bool CheckCascadeDeleteFlag(RelationshipEndMember relationEndProperty)
		{
			return relationEndProperty != null && relationEndProperty.DeleteBehavior == OperationAction.Cascade;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00065E90 File Offset: 0x00064090
		internal void AttachContext(ObjectContext context, MergeOption mergeOption)
		{
			if (!this._wrappedOwner.InitializingProxyRelatedEnds)
			{
				EntityKey entityKey = this._wrappedOwner.EntityKey;
				EntityUtil.CheckEntityKeyNull(entityKey);
				EntitySet entitySet = entityKey.GetEntitySet(context.MetadataWorkspace);
				this.AttachContext(context, entitySet, mergeOption);
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00065ED4 File Offset: 0x000640D4
		internal void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentNull<ObjectContext>(context, "context");
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			EntityUtil.CheckArgumentNull<EntitySet>(entitySet, "entitySet");
			this._wrappedOwner.RelationshipManager.NodeVisited = false;
			if (this._context == context && this._usingNoTracking == (mergeOption == MergeOption.NoTracking))
			{
				return;
			}
			bool flag = true;
			try
			{
				this._sourceQuery = null;
				this._context = context;
				this._usingNoTracking = (mergeOption == MergeOption.NoTracking);
				EdmType edmType;
				RelationshipSet relationshipSet;
				this.FindRelationshipSet(this._context, entitySet, out edmType, out relationshipSet);
				if (relationshipSet == null)
				{
					foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
					{
						AssociationSet associationSet = entitySetBase as AssociationSet;
						if (associationSet != null && associationSet.ElementType == edmType && associationSet.AssociationSetEnds[this._navigation.From].EntitySet != entitySet && associationSet.AssociationSetEnds[this._navigation.From].EntitySet.ElementType == entitySet.ElementType)
						{
							throw EntityUtil.EntitySetIsNotValidForRelationship(entitySet.EntityContainer.Name, entitySet.Name, this._navigation.From, ((AssociationSet)entitySetBase).EntityContainer.Name, ((AssociationSet)entitySetBase).Name);
						}
					}
					throw EntityUtil.NoRelationshipSetMatched(this._navigation.RelationshipName);
				}
				this._relationshipSet = relationshipSet;
				this._relationMetadata = (RelationshipType)edmType;
				bool flag2 = false;
				bool flag3 = false;
				foreach (AssociationEndMember associationEndMember in ((AssociationType)this._relationMetadata).AssociationEndMembers)
				{
					if (associationEndMember.Name == this._navigation.From)
					{
						flag2 = true;
						this._fromEndProperty = associationEndMember;
					}
					if (associationEndMember.Name == this._navigation.To)
					{
						flag3 = true;
						this._toEndProperty = associationEndMember;
					}
				}
				if (!flag2 || !flag3)
				{
					throw EntityUtil.RelatedEndNotFound();
				}
				if (this.IsEmpty())
				{
					EntityReference entityReference = this as EntityReference;
					if (entityReference != null && entityReference.DetachedEntityKey != null)
					{
						EntityKey detachedEntityKey = entityReference.DetachedEntityKey;
						if (!RelatedEnd.IsValidEntityKeyType(detachedEntityKey))
						{
							throw EntityUtil.CannotSetSpecialKeys();
						}
						EntitySet entitySet2 = detachedEntityKey.GetEntitySet(context.MetadataWorkspace);
						this.CheckRelationEntitySet(entitySet2);
						detachedEntityKey.ValidateEntityKey(this.ObjectContext.MetadataWorkspace, entitySet2);
					}
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.DetachContext();
				}
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000661B0 File Offset: 0x000643B0
		internal void FindRelationshipSet(ObjectContext context, EntitySet entitySet, out EdmType relationshipType, out RelationshipSet relationshipSet)
		{
			relationshipType = context.MetadataWorkspace.GetItem<EdmType>(this._navigation.RelationshipName, DataSpace.CSpace);
			if (relationshipType == null)
			{
				throw EntityUtil.NoRelationshipSetMatched(this._navigation.RelationshipName);
			}
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				if (entitySetBase.ElementType == relationshipType && ((AssociationSet)entitySetBase).AssociationSetEnds[this._navigation.From].EntitySet == entitySet)
				{
					relationshipSet = (RelationshipSet)entitySetBase;
					return;
				}
			}
			relationshipSet = null;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0006626C File Offset: 0x0006446C
		internal void DetachContext()
		{
			if (this._context != null && this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				MergeOption? originalMergeOption = this.ObjectContext.ObjectStateManager.TransactionManager.OriginalMergeOption;
				MergeOption mergeOption = MergeOption.NoTracking;
				if (originalMergeOption.GetValueOrDefault() == mergeOption & originalMergeOption != null)
				{
					this._usingNoTracking = true;
					return;
				}
			}
			this._sourceQuery = null;
			this._context = null;
			this._relationshipSet = null;
			this._fromEndProperty = null;
			this._toEndProperty = null;
			this._relationMetadata = null;
			this._isLoaded = false;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000662FD File Offset: 0x000644FD
		internal static IEnumerable<U> GetResults<U>(ObjectQuery<U> query)
		{
			return query.Execute(query.MergeOption);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0006630B File Offset: 0x0006450B
		internal RelatedEnd GetOtherEndOfRelationship(IEntityWrapper wrappedEntity)
		{
			this.EnsureRelationshipNavigationAccessorsInitialized();
			return wrappedEntity.RelationshipManager.GetRelatedEnd(this._navigation.Reverse, this._relationshipFixer);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0006632F File Offset: 0x0006452F
		internal void CheckOwnerNull()
		{
			if (this._wrappedOwner.Entity == null)
			{
				throw EntityUtil.OwnerIsNull();
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00066344 File Offset: 0x00064544
		internal void InitializeRelatedEnd(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			this.SetWrappedOwner(wrappedOwner);
			this._navigation = navigation;
			this._relationshipFixer = relationshipFixer;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0006635B File Offset: 0x0006455B
		internal void SetWrappedOwner(IEntityWrapper wrappedOwner)
		{
			this._wrappedOwner = ((wrappedOwner != null) ? wrappedOwner : EntityWrapperFactory.NullWrapper);
			this._owner = (wrappedOwner.Entity as IEntityWithRelationships);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0006637F File Offset: 0x0006457F
		internal static bool IsValidEntityKeyType(EntityKey entityKey)
		{
			return !entityKey.IsTemporary && EntityKey.EntityNotValidKey != entityKey && EntityKey.NoEntitySetKey != entityKey;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0006639E File Offset: 0x0006459E
		[OnDeserialized]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserialized(StreamingContext context)
		{
			this._wrappedOwner = EntityWrapperFactory.WrapEntityUsingContext(this._owner, this.ObjectContext);
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000663B8 File Offset: 0x000645B8
		internal NavigationProperty NavigationProperty
		{
			get
			{
				if (this.navigationPropertyCache == null && this._wrappedOwner.Context != null && this.TargetAccessor.HasProperty)
				{
					string propertyName = this.TargetAccessor.PropertyName;
					EntityType item = this._wrappedOwner.Context.MetadataWorkspace.GetItem<EntityType>(this._wrappedOwner.IdentityType.FullName, DataSpace.OSpace);
					NavigationProperty navigationProperty;
					if (!item.NavigationProperties.TryGetValue(propertyName, false, out navigationProperty))
					{
						throw new InvalidOperationException(Strings.RelationshipManager_NavigationPropertyNotFound(propertyName));
					}
					this.navigationPropertyCache = navigationProperty;
				}
				return this.navigationPropertyCache;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x00066444 File Offset: 0x00064644
		internal NavigationPropertyAccessor TargetAccessor
		{
			get
			{
				if (this._wrappedOwner.Entity != null)
				{
					this.EnsureRelationshipNavigationAccessorsInitialized();
					return this.RelationshipNavigation.ToPropertyAccessor;
				}
				return NavigationPropertyAccessor.NoNavigationProperty;
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0006646C File Offset: 0x0006466C
		private void EnsureRelationshipNavigationAccessorsInitialized()
		{
			if (!this.RelationshipNavigation.IsInitialized)
			{
				NavigationPropertyAccessor navigationPropertyAccessor = null;
				NavigationPropertyAccessor navigationPropertyAccessor2 = null;
				AssociationType associationType = this.RelationMetadata as AssociationType;
				string relationshipName = this._navigation.RelationshipName;
				string from = this._navigation.From;
				string to = this._navigation.To;
				if (associationType != null || RelationshipManager.TryGetRelationshipType(this.WrappedOwner, this.WrappedOwner.IdentityType, relationshipName, out associationType) || EntityProxyFactory.TryGetAssociationTypeFromProxyInfo(this.WrappedOwner, relationshipName, to, out associationType))
				{
					AssociationEndMember end;
					if (associationType.AssociationEndMembers.TryGetValue(from, false, out end))
					{
						EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(end);
						navigationPropertyAccessor2 = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd, relationshipName, from, to);
					}
					AssociationEndMember end2;
					if (associationType.AssociationEndMembers.TryGetValue(to, false, out end2))
					{
						EntityType entityTypeForEnd2 = MetadataHelper.GetEntityTypeForEnd(end2);
						navigationPropertyAccessor = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd2, relationshipName, to, from);
					}
				}
				if (navigationPropertyAccessor == null || navigationPropertyAccessor2 == null)
				{
					throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
				}
				this.RelationshipNavigation.InitializeAccessors(navigationPropertyAccessor, navigationPropertyAccessor2);
			}
		}

		// Token: 0x04000BBF RID: 3007
		private const string _entityKeyParamName = "EntityKeyValue";

		// Token: 0x04000BC0 RID: 3008
		[Obsolete]
		private IEntityWithRelationships _owner;

		// Token: 0x04000BC1 RID: 3009
		private RelationshipNavigation _navigation;

		// Token: 0x04000BC2 RID: 3010
		private IRelationshipFixer _relationshipFixer;

		// Token: 0x04000BC3 RID: 3011
		internal bool _isLoaded;

		// Token: 0x04000BC4 RID: 3012
		[NonSerialized]
		private RelationshipSet _relationshipSet;

		// Token: 0x04000BC5 RID: 3013
		[NonSerialized]
		private ObjectContext _context;

		// Token: 0x04000BC6 RID: 3014
		[NonSerialized]
		private bool _usingNoTracking;

		// Token: 0x04000BC7 RID: 3015
		[NonSerialized]
		private RelationshipType _relationMetadata;

		// Token: 0x04000BC8 RID: 3016
		[NonSerialized]
		private RelationshipEndMember _fromEndProperty;

		// Token: 0x04000BC9 RID: 3017
		[NonSerialized]
		private RelationshipEndMember _toEndProperty;

		// Token: 0x04000BCA RID: 3018
		[NonSerialized]
		private string _sourceQuery;

		// Token: 0x04000BCB RID: 3019
		[NonSerialized]
		private IEnumerable<EdmMember> _sourceQueryParamProperties;

		// Token: 0x04000BCC RID: 3020
		[NonSerialized]
		internal bool _suppressEvents;

		// Token: 0x04000BCD RID: 3021
		[NonSerialized]
		internal CollectionChangeEventHandler _onAssociationChanged;

		// Token: 0x04000BCE RID: 3022
		[NonSerialized]
		private IEntityWrapper _wrappedOwner;

		// Token: 0x04000BCF RID: 3023
		[NonSerialized]
		private NavigationProperty navigationPropertyCache;
	}
}
