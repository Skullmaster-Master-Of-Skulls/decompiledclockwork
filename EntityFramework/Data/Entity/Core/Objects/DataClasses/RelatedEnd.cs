using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200053B RID: 1339
	[DataContract]
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	[Serializable]
	public abstract class RelatedEnd : IRelatedEnd
	{
		// Token: 0x06003308 RID: 13064 RVA: 0x000F0845 File Offset: 0x000EEA45
		internal RelatedEnd()
		{
			this._wrappedOwner = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000F0858 File Offset: 0x000EEA58
		internal RelatedEnd(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			this.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600330A RID: 13066 RVA: 0x000F0869 File Offset: 0x000EEA69
		// (remove) Token: 0x0600330B RID: 13067 RVA: 0x000F0888 File Offset: 0x000EEA88
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

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600330C RID: 13068 RVA: 0x000F08A7 File Offset: 0x000EEAA7
		// (remove) Token: 0x0600330D RID: 13069 RVA: 0x000F08A9 File Offset: 0x000EEAA9
		internal virtual event CollectionChangeEventHandler AssociationChangedForObjectView
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000F08AB File Offset: 0x000EEAAB
		internal bool IsForeignKey
		{
			get
			{
				return ((AssociationType)this._relationMetadata).IsForeignKey;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000F08BD File Offset: 0x000EEABD
		internal RelationshipNavigation RelationshipNavigation
		{
			get
			{
				return this._navigation;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000F08C5 File Offset: 0x000EEAC5
		[XmlIgnore]
		[SoapIgnore]
		public string RelationshipName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.RelationshipName;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000F08D8 File Offset: 0x000EEAD8
		[XmlIgnore]
		[SoapIgnore]
		public virtual string SourceRoleName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.From;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000F08EB File Offset: 0x000EEAEB
		[XmlIgnore]
		[SoapIgnore]
		public virtual string TargetRoleName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.To;
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000F08FE File Offset: 0x000EEAFE
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerable IRelatedEnd.CreateSourceQuery()
		{
			this.CheckOwnerNull();
			return this.CreateSourceQueryInternal();
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000F090C File Offset: 0x000EEB0C
		internal virtual IEntityWrapper WrappedOwner
		{
			get
			{
				return this._wrappedOwner;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000F0914 File Offset: 0x000EEB14
		internal virtual ObjectContext ObjectContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000F091C File Offset: 0x000EEB1C
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				if (this._entityWrapperFactory == null)
				{
					this._entityWrapperFactory = new EntityWrapperFactory();
				}
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000F0937 File Offset: 0x000EEB37
		[XmlIgnore]
		[SoapIgnore]
		public virtual RelationshipSet RelationshipSet
		{
			get
			{
				this.CheckOwnerNull();
				return this._relationshipSet;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000F0945 File Offset: 0x000EEB45
		internal virtual RelationshipType RelationMetadata
		{
			get
			{
				return this._relationMetadata;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000F094D File Offset: 0x000EEB4D
		internal virtual RelationshipEndMember ToEndMember
		{
			get
			{
				return this._toEndMember;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000F0955 File Offset: 0x000EEB55
		internal bool UsingNoTracking
		{
			get
			{
				return this._usingNoTracking;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000F095D File Offset: 0x000EEB5D
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000F096A File Offset: 0x000EEB6A
		internal virtual RelationshipEndMember FromEndMember
		{
			get
			{
				return this._fromEndMember;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000F0972 File Offset: 0x000EEB72
		// (set) Token: 0x0600331E RID: 13086 RVA: 0x000F0980 File Offset: 0x000EEB80
		[SoapIgnore]
		[XmlIgnore]
		public bool IsLoaded
		{
			get
			{
				this.CheckOwnerNull();
				return this._isLoaded;
			}
			set
			{
				this.CheckOwnerNull();
				this._isLoaded = value;
			}
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000F0990 File Offset: 0x000EEB90
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
					throw Error.Collections_InvalidEntityStateSource();
				}
				entityState = EntityState.Detached;
			}
			else
			{
				entityState = entityEntry.State;
			}
			if (entityState == EntityState.Added && (!this.IsForeignKey || !this.IsDependentEndOfReferentialConstraint(false)))
			{
				throw Error.Collections_InvalidEntityStateSource();
			}
			if ((entityState != EntityState.Detached || !this.UsingNoTracking) && entityState != EntityState.Modified && entityState != EntityState.Unchanged && entityState != EntityState.Deleted && entityState != EntityState.Added)
			{
				hasResults = false;
				return null;
			}
			if (this._sourceQuery == null)
			{
				this._sourceQuery = this.GenerateQueryText();
			}
			ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(this._sourceQuery, this._context, mergeOption);
			hasResults = this.AddQueryParameters<TEntity>(objectQuery);
			objectQuery.Parameters.SetReadOnly(true);
			return objectQuery;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000F0A5C File Offset: 0x000EEC5C
		private string GenerateQueryText()
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			AssociationType associationType = (AssociationType)this._relationMetadata;
			EntitySet entitySet = ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._fromEndMember.Name].EntitySet;
			EntitySet entitySet2 = ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._toEndMember.Name].EntitySet;
			EntityType entityType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)this._toEndMember);
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
				if (!referentialConstraint.ToRole.EdmEquals(this._toEndMember))
				{
					stringBuilder = new StringBuilder("SELECT VALUE P FROM ");
					RelatedEnd.AppendEntitySet(stringBuilder, entitySet2, entityType, ofTypeRequired);
					stringBuilder.Append(" AS P WHERE ");
					AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
					this._sourceQueryParamProperties = toProperties;
					for (int i = 0; i < fromProperties.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(" AND ");
						}
						stringBuilder.Append("P.[");
						stringBuilder.Append(fromProperties[i].Name);
						stringBuilder.Append("] = @");
						stringBuilder.Append(aliasGenerator.Next());
					}
					return stringBuilder.ToString();
				}
				stringBuilder = new StringBuilder("SELECT VALUE D FROM ");
				RelatedEnd.AppendEntitySet(stringBuilder, entitySet2, entityType, ofTypeRequired);
				stringBuilder.Append(" AS D WHERE ");
				AliasGenerator aliasGenerator2 = new AliasGenerator("EntityKeyValue");
				this._sourceQueryParamProperties = fromProperties;
				for (int j = 0; j < toProperties.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" AND ");
					}
					stringBuilder.Append("D.[");
					stringBuilder.Append(toProperties[j].Name);
					stringBuilder.Append("] = @");
					stringBuilder.Append(aliasGenerator2.Next());
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
				stringBuilder.Append(this._fromEndMember.Name);
				stringBuilder.Append("]) = ");
				RelatedEnd.AppendKeyParameterRow(stringBuilder, entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace).ElementType.KeyMembers);
				stringBuilder.Append(") AS [AssociationEntry] INNER JOIN ");
				RelatedEnd.AppendEntitySet(stringBuilder, entitySet2, entityType, ofTypeRequired);
				stringBuilder.Append(" AS [TargetEntity] ON Key([AssociationEntry].[");
				stringBuilder.Append(this._toEndMember.Name);
				stringBuilder.Append("]) = Key(Ref([TargetEntity]))");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000F0DD0 File Offset: 0x000EEFD0
		private bool AddQueryParameters<TEntity>(ObjectQuery<TEntity> query)
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			bool result = true;
			AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
			IEnumerable<EdmMember> enumerable = this._sourceQueryParamProperties ?? entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace).ElementType.KeyMembers;
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
					else if (this.CachedForeignKeyIsConceptualNull())
					{
						obj = null;
					}
					else
					{
						obj = this.GetCurrentValueFromEntity(parameterMember);
					}
					ObjectParameter objectParameter;
					if (obj == null)
					{
						EdmType edmType = parameterMember.TypeUsage.EdmType;
						Type type = Helper.IsPrimitiveType(edmType) ? ((PrimitiveType)edmType).ClrEquivalentType : this.ObjectContext.MetadataWorkspace.GetObjectSpaceType((EnumType)edmType).ClrType;
						objectParameter = new ObjectParameter(aliasGenerator.Next(), type);
						result = false;
					}
					else
					{
						objectParameter = new ObjectParameter(aliasGenerator.Next(), obj);
					}
					objectParameter.TypeUsage = Helper.GetModelTypeUsage(parameterMember);
					query.Parameters.Add(objectParameter);
				}
			}
			return result;
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000F0F6C File Offset: 0x000EF16C
		private object GetCurrentValueFromEntity(EdmMember member)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._context.ObjectStateManager.GetOrAddStateManagerTypeMetadata(member.DeclaringType);
			StateManagerMemberMetadata stateManagerMemberMetadata = orAddStateManagerTypeMetadata.Member(orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(member.Name));
			return stateManagerMemberMetadata.GetValue(this._wrappedOwner.Entity);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000F0FB4 File Offset: 0x000EF1B4
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

		// Token: 0x06003324 RID: 13092 RVA: 0x000F1038 File Offset: 0x000EF238
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
				if (!string.IsNullOrEmpty(targetEntityType.NamespaceName))
				{
					sourceBuilder.Append(targetEntityType.NamespaceName);
					sourceBuilder.Append("].[");
				}
				sourceBuilder.Append(targetEntityType.Name);
				sourceBuilder.Append("])");
			}
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000F10E8 File Offset: 0x000EF2E8
		internal virtual ObjectQuery<TEntity> ValidateLoad<TEntity>(MergeOption mergeOption, string relatedEndName, out bool hasResults)
		{
			ObjectQuery<TEntity> objectQuery = this.CreateSourceQuery<TEntity>(mergeOption, out hasResults);
			if (objectQuery == null)
			{
				throw Error.RelatedEnd_RelatedEndNotAttachedToContext(relatedEndName);
			}
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw Error.Collections_InvalidEntityStateLoad(relatedEndName);
			}
			if (this.UsingNoTracking != (mergeOption == MergeOption.NoTracking))
			{
				throw Error.RelatedEnd_MismatchedMergeOptionOnLoad(mergeOption);
			}
			if (this.UsingNoTracking)
			{
				if (this.IsLoaded)
				{
					throw Error.RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd();
				}
				if (!this.IsEmpty())
				{
					throw Error.RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd();
				}
			}
			return objectQuery;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000F1174 File Offset: 0x000EF374
		public void Load()
		{
			this.Load(this.DefaultMergeOption);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000F1182 File Offset: 0x000EF382
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this.LoadAsync(this.DefaultMergeOption, cancellationToken);
		}

		// Token: 0x06003328 RID: 13096
		public abstract void Load(MergeOption mergeOption);

		// Token: 0x06003329 RID: 13097
		public abstract Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken);

		// Token: 0x0600332A RID: 13098 RVA: 0x000F1194 File Offset: 0x000EF394
		internal void DeferredLoad()
		{
			if (this._wrappedOwner != null && this._wrappedOwner != NullEntityWrapper.NullWrapper && !this.IsLoaded && this._context != null && this._context.ContextOptions.LazyLoadingEnabled && !this._context.InMaterialization && this.CanDeferredLoad && (this.UsingNoTracking || (this._wrappedOwner.ObjectStateEntry != null && (this._wrappedOwner.ObjectStateEntry.State == EntityState.Unchanged || this._wrappedOwner.ObjectStateEntry.State == EntityState.Modified || (this._wrappedOwner.ObjectStateEntry.State == EntityState.Added && this.IsForeignKey && this.IsDependentEndOfReferentialConstraint(false))))))
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

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000F12A0 File Offset: 0x000EF4A0
		internal virtual bool CanDeferredLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000F12A4 File Offset: 0x000EF4A4
		internal virtual void Merge<TEntity>(IEnumerable<TEntity> collection, MergeOption mergeOption, bool setIsLoaded)
		{
			List<IEntityWrapper> list = collection as List<IEntityWrapper>;
			if (list == null)
			{
				list = new List<IEntityWrapper>();
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.TargetRoleName].EntitySet;
				foreach (TEntity tentity in collection)
				{
					IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext);
					if (mergeOption == MergeOption.NoTracking)
					{
						this.EntityWrapperFactory.UpdateNoTrackingWrapper(entityWrapper, this.ObjectContext, entitySet);
					}
					list.Add(entityWrapper);
				}
			}
			this.Merge<TEntity>(list, mergeOption, setIsLoaded);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000F1360 File Offset: 0x000EF560
		internal virtual void Merge<TEntity>(List<IEntityWrapper> collection, MergeOption mergeOption, bool setIsLoaded)
		{
			if (this.WrappedOwner.EntityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			this.ObjectContext.ObjectStateManager.UpdateRelationships(this.ObjectContext, mergeOption, (AssociationSet)this.RelationshipSet, (AssociationEndMember)this.FromEndMember, this.WrappedOwner, (AssociationEndMember)this.ToEndMember, collection, setIsLoaded);
			if (setIsLoaded)
			{
				this._isLoaded = true;
			}
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000F13D1 File Offset: 0x000EF5D1
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IRelatedEnd.Attach(IEntityWithRelationships entity)
		{
			Check.NotNull<IEntityWithRelationships>(entity, "entity");
			((IRelatedEnd)this).Attach(entity);
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000F13E8 File Offset: 0x000EF5E8
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IRelatedEnd.Attach(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.CheckOwnerNull();
			this.Attach(new IEntityWrapper[]
			{
				this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext)
			}, false);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000F142C File Offset: 0x000EF62C
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
							throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
						}
					}
					else
					{
						foreach (IEntityWrapper wrappedEntity in list)
						{
							RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
							if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
							{
								EntityEntry @object = objectStateManager.FindEntityEntry(otherEndOfRelationship.WrappedOwner.Entity);
								if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(@object.GetCurrentEntityValue), entityEntry.EntityKey))
								{
									throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
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

		// Token: 0x06003331 RID: 13105 RVA: 0x000F15F0 File Offset: 0x000EF7F0
		internal void ValidateOwnerForAttach()
		{
			if (this.ObjectContext == null || this.UsingNoTracking)
			{
				throw Error.RelatedEnd_InvalidOwnerStateForAttach();
			}
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.GetEntityEntry(this._wrappedOwner.Entity);
			if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged)
			{
				throw Error.RelatedEnd_InvalidOwnerStateForAttach();
			}
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000F1648 File Offset: 0x000EF848
		internal void ValidateEntityForAttach(IEntityWrapper wrappedEntity, int index, bool allowCollection)
		{
			if (wrappedEntity == null || wrappedEntity.Entity == null)
			{
				if (allowCollection)
				{
					throw Error.RelatedEnd_InvalidNthElementNullForAttach(index);
				}
				throw new ArgumentNullException("wrappedEntity");
			}
			else
			{
				this.VerifyType(wrappedEntity);
				EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry == null || !object.ReferenceEquals(entityEntry.Entity, wrappedEntity.Entity))
				{
					if (allowCollection)
					{
						throw Error.RelatedEnd_InvalidNthElementContextForAttach(index);
					}
					throw Error.RelatedEnd_InvalidEntityContextForAttach();
				}
				else
				{
					if (entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified)
					{
						return;
					}
					if (allowCollection)
					{
						throw Error.RelatedEnd_InvalidNthElementStateForAttach(index);
					}
					throw Error.RelatedEnd_InvalidEntityStateForAttach();
				}
			}
		}

		// Token: 0x06003333 RID: 13107
		internal abstract IEnumerable CreateSourceQueryInternal();

		// Token: 0x06003334 RID: 13108 RVA: 0x000F16EB File Offset: 0x000EF8EB
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IRelatedEnd.Add(IEntityWithRelationships entity)
		{
			Check.NotNull<IEntityWithRelationships>(entity, "entity");
			((IRelatedEnd)this).Add(entity);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000F1700 File Offset: 0x000EF900
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IRelatedEnd.Add(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.Add(this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext));
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000F1726 File Offset: 0x000EF926
		internal void Add(IEntityWrapper wrappedEntity)
		{
			if (this._wrappedOwner.Entity != null)
			{
				this.Add(wrappedEntity, true);
				return;
			}
			this.DisconnectedAdd(wrappedEntity);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000F1745 File Offset: 0x000EF945
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IRelatedEnd.Remove(IEntityWithRelationships entity)
		{
			Check.NotNull<IEntityWithRelationships>(entity, "entity");
			return ((IRelatedEnd)this).Remove(entity);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000F175A File Offset: 0x000EF95A
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IRelatedEnd.Remove(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.DeferredLoad();
			return this.Remove(this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext), false);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000F1787 File Offset: 0x000EF987
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

		// Token: 0x0600333A RID: 13114
		internal abstract void DisconnectedAdd(IEntityWrapper wrappedEntity);

		// Token: 0x0600333B RID: 13115
		internal abstract bool DisconnectedRemove(IEntityWrapper wrappedEntity);

		// Token: 0x0600333C RID: 13116 RVA: 0x000F17B6 File Offset: 0x000EF9B6
		internal void Add(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			if (this._context != null && !this.UsingNoTracking)
			{
				this.ValidateStateForAdd(this._wrappedOwner);
				this.ValidateStateForAdd(wrappedEntity);
			}
			this.Add(wrappedEntity, applyConstraints, false, false, true, true);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000F17E8 File Offset: 0x000EF9E8
		internal void CheckRelationEntitySet(EntitySet set)
		{
			if (((AssociationSet)this._relationshipSet).AssociationSetEnds[this._navigation.To] != null && ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._navigation.To].EntitySet != set)
			{
				throw Error.RelatedEnd_EntitySetIsNotValidForRelationship(set.EntityContainer.Name, set.Name, this._navigation.To, this._relationshipSet.EntityContainer.Name, this._relationshipSet.Name);
			}
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000F187C File Offset: 0x000EFA7C
		internal void ValidateStateForAdd(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw Error.RelatedEnd_UnableToAddRelationshipWithDeletedEntity();
			}
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000F18B4 File Offset: 0x000EFAB4
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
			this.ValidateContextsAreCompatible(otherEndOfRelationship);
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
			this.SynchronizeContexts(otherEndOfRelationship, relationshipAlreadyExists, addRelationshipAsUnchanged);
			if (this.ObjectContext != null && this.IsForeignKey && !this.ObjectContext.ObjectStateManager.TransactionManager.IsGraphUpdate && !this.UpdateDependentEndForeignKey(otherEndOfRelationship, forceForeignKeyChanges))
			{
				otherEndOfRelationship.UpdateDependentEndForeignKey(this, forceForeignKeyChanges);
			}
			otherEndOfRelationship.OnAssociationChanged(CollectionChangeAction.Add, this._wrappedOwner.Entity);
			this.OnAssociationChanged(CollectionChangeAction.Add, wrappedTarget.Entity);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000F19CF File Offset: 0x000EFBCF
		internal virtual void AddToNavigationPropertyIfCompatible(RelatedEnd otherRelatedEnd)
		{
			this.AddToNavigationProperty(otherRelatedEnd.WrappedOwner);
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000F19DD File Offset: 0x000EFBDD
		internal virtual bool CachedForeignKeyIsConceptualNull()
		{
			return false;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000F19E0 File Offset: 0x000EFBE0
		internal virtual bool UpdateDependentEndForeignKey(RelatedEnd targetRelatedEnd, bool forceForeignKeyChanges)
		{
			return false;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000F19E3 File Offset: 0x000EFBE3
		internal virtual void VerifyDetachedKeyMatches(EntityKey entityKey)
		{
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000F19E8 File Offset: 0x000EFBE8
		private void ValidateContextsAreCompatible(RelatedEnd targetRelatedEnd)
		{
			if (object.ReferenceEquals(this.ObjectContext, targetRelatedEnd.ObjectContext) && this.ObjectContext != null)
			{
				if (this.UsingNoTracking != targetRelatedEnd.UsingNoTracking)
				{
					throw Error.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(this.UsingNoTracking ? this._navigation.From : this._navigation.To);
				}
			}
			else if (this.ObjectContext != null && targetRelatedEnd.ObjectContext != null)
			{
				if (this.UsingNoTracking && targetRelatedEnd.UsingNoTracking)
				{
					targetRelatedEnd.WrappedOwner.ResetContext(this.ObjectContext, this.GetTargetEntitySetFromRelationshipSet(), MergeOption.NoTracking);
					return;
				}
				throw Error.RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts();
			}
			else if ((this._context == null || this.UsingNoTracking) && targetRelatedEnd.ObjectContext != null && !targetRelatedEnd.UsingNoTracking)
			{
				targetRelatedEnd.ValidateStateForAdd(targetRelatedEnd.WrappedOwner);
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000F1AB4 File Offset: 0x000EFCB4
		private void SynchronizeContexts(RelatedEnd targetRelatedEnd, bool relationshipAlreadyExists, bool addRelationshipAsUnchanged)
		{
			RelatedEnd relatedEnd = null;
			IEntityWrapper entityWrapper = null;
			IEntityWrapper wrappedOwner = targetRelatedEnd.WrappedOwner;
			if (object.ReferenceEquals(this.ObjectContext, targetRelatedEnd.ObjectContext) && this.ObjectContext != null)
			{
				if (!this.IsForeignKey && !relationshipAlreadyExists && !this.UsingNoTracking)
				{
					if (!this.ObjectContext.ObjectStateManager.TransactionManager.IsLocalPublicAPI && this.WrappedOwner.EntityKey != null && !this.WrappedOwner.EntityKey.IsTemporary && this.IsDependentEndOfReferentialConstraint(false))
					{
						addRelationshipAsUnchanged = true;
					}
					this.AddRelationshipToObjectStateManager(wrappedOwner, addRelationshipAsUnchanged, false);
				}
				if (wrappedOwner.RequiresRelationshipChangeTracking && (this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking || this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking || this.ObjectContext.ObjectStateManager.TransactionManager.IsDetectChanges))
				{
					this.AddToNavigationProperty(wrappedOwner);
					targetRelatedEnd.AddToNavigationProperty(this._wrappedOwner);
					return;
				}
			}
			else if (this.ObjectContext != null || targetRelatedEnd.ObjectContext != null)
			{
				if (this.ObjectContext == null)
				{
					relatedEnd = targetRelatedEnd;
					entityWrapper = this._wrappedOwner;
				}
				else
				{
					relatedEnd = this;
					entityWrapper = wrappedOwner;
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
							if (transactionManager.TrackProcessedEntities)
							{
								if (!transactionManager.WrappedEntities.ContainsKey(entityWrapper.Entity))
								{
									transactionManager.WrappedEntities.Add(entityWrapper.Entity, entityWrapper);
								}
								transactionManager.ProcessedEntities.Add(relatedEnd.WrappedOwner);
							}
							relatedEnd.AddGraphToObjectStateManager(entityWrapper, relationshipAlreadyExists, addRelationshipAsUnchanged, false);
							if (entityWrapper.RequiresRelationshipChangeTracking && this.TargetAccessor.HasProperty)
							{
								targetRelatedEnd.AddToNavigationProperty(this._wrappedOwner);
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
						transactionManager.EndAddTracking();
					}
				}
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000F1CE8 File Offset: 0x000EFEE8
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

		// Token: 0x06003347 RID: 13127 RVA: 0x000F1D70 File Offset: 0x000EFF70
		private void UpdateSnapshotOfRelationships(IEntityWrapper wrappedEntity)
		{
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
			if (!otherEndOfRelationship.ContainsEntity(this.WrappedOwner))
			{
				otherEndOfRelationship.AddToLocalCache(this.WrappedOwner, false);
			}
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000F1DA0 File Offset: 0x000EFFA0
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
				if ((this._context == null || !this._context.ObjectStateManager.TransactionManager.IsLocalPublicAPI) && this._context != null && (deleteEntity || (deleteOwner && RelatedEnd.CheckCascadeDeleteFlag(this._fromEndMember)) || (applyReferentialConstraints && this.IsPrincipalEndOfReferentialConstraint())) && !object.ReferenceEquals(wrappedEntity.Entity, this._context.ObjectStateManager.TransactionManager.EntityBeingReparented) && !object.ReferenceEquals(this._context.ObjectStateManager.EntityInvokingFKSetter, wrappedEntity.Entity))
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

		// Token: 0x06003349 RID: 13129 RVA: 0x000F1F30 File Offset: 0x000F0130
		internal bool IsDependentEndOfReferentialConstraint(bool checkIdentifying)
		{
			if (this._relationMetadata != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == this.FromEndMember)
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

		// Token: 0x0600334A RID: 13130 RVA: 0x000F1FCC File Offset: 0x000F01CC
		internal bool IsPrincipalEndOfReferentialConstraint()
		{
			if (this._relationMetadata != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this._relationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.FromRole == this._fromEndMember)
					{
						EntityType entityType = referentialConstraint.ToRole.GetEntityType();
						return RelatedEnd.CheckIfAllPropertiesAreKeyProperties(entityType.KeyMemberNames, referentialConstraint.ToProperties);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000F2060 File Offset: 0x000F0260
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

		// Token: 0x0600334C RID: 13132 RVA: 0x000F20E4 File Offset: 0x000F02E4
		internal void IncludeEntity(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw Error.RelatedEnd_UnableToAddRelationshipWithDeletedEntity();
			}
			if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
			{
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
				this.ObjectContext.GetTypeUsage(otherEndOfRelationship.WrappedOwner.IdentityType);
				otherEndOfRelationship.AddToNavigationPropertyIfCompatible(this);
			}
			if (entityEntry == null)
			{
				this.AddGraphToObjectStateManager(wrappedEntity, false, addRelationshipAsUnchanged, doAttach);
				return;
			}
			if (this.FindRelationshipEntryInObjectStateManager(wrappedEntity) == null)
			{
				this.VerifyDetachedKeyMatches(wrappedEntity.EntityKey);
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

		// Token: 0x0600334D RID: 13133 RVA: 0x000F2200 File Offset: 0x000F0400
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

		// Token: 0x0600334E RID: 13134
		internal abstract bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper);

		// Token: 0x0600334F RID: 13135
		internal abstract void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper);

		// Token: 0x06003350 RID: 13136 RVA: 0x000F2290 File Offset: 0x000F0490
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

		// Token: 0x06003351 RID: 13137 RVA: 0x000F22ED File Offset: 0x000F04ED
		internal void RemoveFromNavigationProperty(IEntityWrapper wrapper)
		{
			if (this.TargetAccessor.HasProperty && this.CheckIfNavigationPropertyContainsEntity(wrapper))
			{
				this.RemoveFromObjectCache(wrapper);
			}
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000F2310 File Offset: 0x000F0510
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

		// Token: 0x06003353 RID: 13139 RVA: 0x000F2414 File Offset: 0x000F0614
		internal RelationshipEntry FindRelationshipEntryInObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityKey entityKey = wrappedEntity.EntityKey;
			EntityKey entityKey2 = this._wrappedOwner.EntityKey;
			return this._context.ObjectStateManager.FindRelationship(this._relationshipSet, new KeyValuePair<string, EntityKey>(this._navigation.From, entityKey2), new KeyValuePair<string, EntityKey>(this._navigation.To, entityKey));
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000F246C File Offset: 0x000F066C
		internal void Clear(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			this.ClearCollectionOrRef(wrappedEntity, navigation, doCascadeDelete);
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000F2478 File Offset: 0x000F0678
		internal void CheckReferentialConstraintProperties(EntityEntry ownerEntry)
		{
			foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
			{
				if (referentialConstraint.ToRole == this.FromEndMember)
				{
					if (!this.CheckReferentialConstraintPrincipalProperty(ownerEntry, referentialConstraint))
					{
						throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
					}
				}
				else if (referentialConstraint.FromRole == this.FromEndMember && !this.CheckReferentialConstraintDependentProperty(ownerEntry, referentialConstraint))
				{
					throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
				}
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000F2518 File Offset: 0x000F0718
		internal virtual bool CheckReferentialConstraintPrincipalProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			return false;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000F251C File Offset: 0x000F071C
		internal virtual bool CheckReferentialConstraintDependentProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			if (!this.IsEmpty())
			{
				foreach (IEntityWrapper entityWrapper in this.GetWrappedEntities())
				{
					EntityEntry objectStateEntry = entityWrapper.ObjectStateEntry;
					if (objectStateEntry != null && objectStateEntry.State != EntityState.Added && objectStateEntry.State != EntityState.Deleted && objectStateEntry.State != EntityState.Detached && !RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(objectStateEntry.GetCurrentEntityValue), ownerEntry.EntityKey))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000F25B0 File Offset: 0x000F07B0
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

		// Token: 0x06003359 RID: 13145 RVA: 0x000F261A File Offset: 0x000F081A
		public IEnumerator GetEnumerator()
		{
			this.DeferredLoad();
			return this.GetInternalEnumerable().GetEnumerator();
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000F2630 File Offset: 0x000F0830
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

		// Token: 0x0600335B RID: 13147 RVA: 0x000F2710 File Offset: 0x000F0910
		internal virtual void DetachAll(EntityState ownerEntityState)
		{
			List<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (IEntityWrapper item in this.GetWrappedEntities())
			{
				list.Add(item);
			}
			bool flag = ownerEntityState == EntityState.Added || this._fromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many;
			foreach (IEntityWrapper wrappedEntity in list)
			{
				if (!this.ContainsEntity(wrappedEntity))
				{
					return;
				}
				if (flag)
				{
					RelatedEnd.DetachRelationshipFromObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
				}
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
				otherEndOfRelationship.RemoveFromCache(this._wrappedOwner, true, false);
				otherEndOfRelationship.OnAssociationChanged(CollectionChangeAction.Remove, this._wrappedOwner.Entity);
			}
			foreach (IEntityWrapper wrappedEntity2 in list)
			{
				this.GetOtherEndOfRelationship(wrappedEntity2);
				this.RemoveFromCache(wrappedEntity2, false, false);
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000F2860 File Offset: 0x000F0A60
		internal void AddToCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			this.AddToLocalCache(wrappedEntity, applyConstraints);
			this.AddToObjectCache(wrappedEntity);
		}

		// Token: 0x0600335D RID: 13149
		internal abstract void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints);

		// Token: 0x0600335E RID: 13150
		internal abstract void AddToObjectCache(IEntityWrapper wrappedEntity);

		// Token: 0x0600335F RID: 13151 RVA: 0x000F2874 File Offset: 0x000F0A74
		internal bool RemoveFromCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			bool result = this.RemoveFromLocalCache(wrappedEntity, resetIsLoaded, preserveForeignKey);
			this.RemoveFromObjectCache(wrappedEntity);
			return result;
		}

		// Token: 0x06003360 RID: 13152
		internal abstract bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey);

		// Token: 0x06003361 RID: 13153
		internal abstract bool RemoveFromObjectCache(IEntityWrapper wrappedEntity);

		// Token: 0x06003362 RID: 13154
		internal abstract bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists);

		// Token: 0x06003363 RID: 13155
		internal abstract void VerifyType(IEntityWrapper wrappedEntity);

		// Token: 0x06003364 RID: 13156
		internal abstract bool CanSetEntityType(IEntityWrapper wrappedEntity);

		// Token: 0x06003365 RID: 13157
		internal abstract void Include(bool addRelationshipAsUnchanged, bool doAttach);

		// Token: 0x06003366 RID: 13158
		internal abstract void Exclude();

		// Token: 0x06003367 RID: 13159
		internal abstract void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete);

		// Token: 0x06003368 RID: 13160
		internal abstract bool ContainsEntity(IEntityWrapper wrappedEntity);

		// Token: 0x06003369 RID: 13161
		internal abstract IEnumerable GetInternalEnumerable();

		// Token: 0x0600336A RID: 13162
		internal abstract IEnumerable<IEntityWrapper> GetWrappedEntities();

		// Token: 0x0600336B RID: 13163
		internal abstract void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> keyValues, HashSet<object> visited);

		// Token: 0x0600336C RID: 13164
		internal abstract bool IsEmpty();

		// Token: 0x0600336D RID: 13165
		internal abstract void OnRelatedEndClear();

		// Token: 0x0600336E RID: 13166
		internal abstract void ClearWrappedValues();

		// Token: 0x0600336F RID: 13167
		internal abstract void VerifyMultiplicityConstraintsForAdd(bool applyConstraints);

		// Token: 0x06003370 RID: 13168 RVA: 0x000F2894 File Offset: 0x000F0A94
		internal virtual void OnAssociationChanged(CollectionChangeAction collectionChangeAction, object entity)
		{
			if (!this._suppressEvents && this._onAssociationChanged != null)
			{
				this._onAssociationChanged(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000F28BC File Offset: 0x000F0ABC
		internal virtual void AddEntityToObjectStateManager(IEntityWrapper wrappedEntity, bool doAttach)
		{
			EntitySet targetEntitySetFromRelationshipSet = this.GetTargetEntitySetFromRelationshipSet();
			if (!doAttach)
			{
				this._context.AddSingleObject(targetEntitySetFromRelationshipSet, wrappedEntity, "entity");
				return;
			}
			this._context.AttachSingleObject(wrappedEntity, targetEntitySetFromRelationshipSet);
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000F28F4 File Offset: 0x000F0AF4
		internal EntitySet GetTargetEntitySetFromRelationshipSet()
		{
			AssociationSet associationSet = (AssociationSet)this._relationshipSet;
			AssociationEndMember associationEndMember = (AssociationEndMember)this.ToEndMember;
			return associationSet.AssociationSetEnds[associationEndMember.Name].EntitySet;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000F2934 File Offset: 0x000F0B34
		private RelationshipEntry AddRelationshipToObjectStateManager(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			EntityKey entityKey2 = wrappedEntity.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			if (entityKey2 == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			return this.ObjectContext.ObjectStateManager.AddRelation(new RelationshipWrapper((AssociationSet)this._relationshipSet, new KeyValuePair<string, EntityKey>(this._navigation.From, entityKey), new KeyValuePair<string, EntityKey>(this._navigation.To, entityKey2)), (addRelationshipAsUnchanged || doAttach) ? EntityState.Unchanged : EntityState.Added);
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000F29B4 File Offset: 0x000F0BB4
		private static void WalkObjectGraphToIncludeAllRelatedEntities(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity.RelationshipManager.Relationships)
			{
				relatedEnd.Include(addRelationshipAsUnchanged, doAttach);
			}
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000F2A08 File Offset: 0x000F0C08
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

		// Token: 0x06003376 RID: 13174 RVA: 0x000F2A78 File Offset: 0x000F0C78
		private static void RemoveRelationshipFromObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			RelationshipEntry relationshipEntry = RelatedEnd.MarkRelationshipAsDeletedInObjectStateManager(wrappedEntity, wrappedOwner, relationshipSet, navigation);
			if (relationshipEntry != null && relationshipEntry.State != EntityState.Detached)
			{
				relationshipEntry.AcceptChanges();
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000F2AA4 File Offset: 0x000F0CA4
		private void FixupOtherEndOfRelationshipForRemove(IEntityWrapper wrappedEntity, bool preserveForeignKey)
		{
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
			otherEndOfRelationship.Remove(this._wrappedOwner, false, false, false, false, preserveForeignKey);
			otherEndOfRelationship.RemoveFromNavigationProperty(this._wrappedOwner);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000F2AD8 File Offset: 0x000F0CD8
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

		// Token: 0x06003379 RID: 13177 RVA: 0x000F2B14 File Offset: 0x000F0D14
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

		// Token: 0x0600337A RID: 13178 RVA: 0x000F2B70 File Offset: 0x000F0D70
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

		// Token: 0x0600337B RID: 13179 RVA: 0x000F2BD4 File Offset: 0x000F0DD4
		private static void RemoveEntityFromRelatedEnds(IEntityWrapper wrappedEntity1, IEntityWrapper wrappedEntity2, RelationshipNavigation navigation)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity1.RelationshipManager.Relationships)
			{
				bool doCascadeDelete = RelatedEnd.CheckCascadeDeleteFlag(relatedEnd.FromEndMember) || relatedEnd.IsPrincipalEndOfReferentialConstraint();
				relatedEnd.Clear(wrappedEntity2, navigation, doCascadeDelete);
			}
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000F2C44 File Offset: 0x000F0E44
		private static bool CheckCascadeDeleteFlag(RelationshipEndMember relationEndProperty)
		{
			return relationEndProperty != null && relationEndProperty.DeleteBehavior == OperationAction.Cascade;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000F2C54 File Offset: 0x000F0E54
		internal void AttachContext(ObjectContext context, MergeOption mergeOption)
		{
			if (!this._wrappedOwner.InitializingProxyRelatedEnds)
			{
				EntityKey entityKey = this._wrappedOwner.EntityKey;
				if (entityKey == null)
				{
					throw Error.EntityKey_UnexpectedNull();
				}
				EntitySet entitySet = entityKey.GetEntitySet(context.MetadataWorkspace);
				this.AttachContext(context, entitySet, mergeOption);
			}
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000F2C9C File Offset: 0x000F0E9C
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		internal void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
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
				this._entityWrapperFactory = context.EntityWrapperFactory;
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
							throw Error.RelatedEnd_EntitySetIsNotValidForRelationship(entitySet.EntityContainer.Name, entitySet.Name, this._navigation.From, entitySetBase.EntityContainer.Name, entitySetBase.Name);
						}
					}
					string relationshipName = this._navigation.RelationshipName;
					throw Error.Collections_NoRelationshipSetMatched(relationshipName);
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
						this._fromEndMember = associationEndMember;
					}
					if (associationEndMember.Name == this._navigation.To)
					{
						flag3 = true;
						this._toEndMember = associationEndMember;
					}
				}
				if (!flag2 || !flag3)
				{
					throw Error.RelatedEnd_RelatedEndNotFound();
				}
				this.ValidateDetachedEntityKey();
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

		// Token: 0x0600337F RID: 13183 RVA: 0x000F2F04 File Offset: 0x000F1104
		internal virtual void ValidateDetachedEntityKey()
		{
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000F2F08 File Offset: 0x000F1108
		internal void FindRelationshipSet(ObjectContext context, EntitySet entitySet, out EdmType relationshipType, out RelationshipSet relationshipSet)
		{
			if (this._navigation.AssociationType == null || this._navigation.AssociationType.Index < 0)
			{
				RelatedEnd.FindRelationshipSet(context, this._navigation, entitySet, out relationshipType, out relationshipSet);
				return;
			}
			MetadataOptimization metadataOptimization = context.MetadataWorkspace.MetadataOptimization;
			AssociationType cspaceAssociationType = metadataOptimization.GetCSpaceAssociationType(this._navigation.AssociationType);
			relationshipType = cspaceAssociationType;
			relationshipSet = metadataOptimization.FindCSpaceAssociationSet(cspaceAssociationType, this._navigation.From, entitySet);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000F2F80 File Offset: 0x000F1180
		internal static void FindRelationshipSet(ObjectContext context, RelationshipNavigation navigation, EntitySet entitySet, out EdmType relationshipType, out RelationshipSet relationshipSet)
		{
			relationshipType = context.MetadataWorkspace.GetItem<EdmType>(navigation.RelationshipName, DataSpace.CSpace);
			if (relationshipType == null)
			{
				string relationshipName = navigation.RelationshipName;
				throw Error.Collections_NoRelationshipSetMatched(relationshipName);
			}
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				if (entitySetBase.ElementType == relationshipType && ((AssociationSet)entitySetBase).AssociationSetEnds[navigation.From].EntitySet == entitySet)
				{
					relationshipSet = (RelationshipSet)entitySetBase;
					return;
				}
			}
			relationshipSet = null;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000F3030 File Offset: 0x000F1230
		internal void DetachContext()
		{
			if (this._context != null && this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking && this.ObjectContext.ObjectStateManager.TransactionManager.OriginalMergeOption == MergeOption.NoTracking)
			{
				this._usingNoTracking = true;
				return;
			}
			this._sourceQuery = null;
			this._context = null;
			this._relationshipSet = null;
			this._fromEndMember = null;
			this._toEndMember = null;
			this._relationMetadata = null;
			this._isLoaded = false;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000F30C1 File Offset: 0x000F12C1
		internal RelatedEnd GetOtherEndOfRelationship(IEntityWrapper wrappedEntity)
		{
			this.EnsureRelationshipNavigationAccessorsInitialized();
			return wrappedEntity.RelationshipManager.GetRelatedEnd(this._navigation.Reverse, this._relationshipFixer);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000F30E5 File Offset: 0x000F12E5
		internal virtual void CheckOwnerNull()
		{
			if (this._wrappedOwner.Entity == null)
			{
				throw Error.RelatedEnd_OwnerIsNull();
			}
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000F30FA File Offset: 0x000F12FA
		internal void InitializeRelatedEnd(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			this.SetWrappedOwner(wrappedOwner);
			this._navigation = navigation;
			this._relationshipFixer = relationshipFixer;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000F3111 File Offset: 0x000F1311
		internal void SetWrappedOwner(IEntityWrapper wrappedOwner)
		{
			this._wrappedOwner = ((wrappedOwner != null) ? wrappedOwner : NullEntityWrapper.NullWrapper);
			this._owner = (wrappedOwner.Entity as IEntityWithRelationships);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000F3135 File Offset: 0x000F1335
		internal static bool IsValidEntityKeyType(EntityKey entityKey)
		{
			return !entityKey.IsTemporary && !object.ReferenceEquals(EntityKey.EntityNotValidKey, entityKey) && !object.ReferenceEquals(EntityKey.NoEntitySetKey, entityKey);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000F315C File Offset: 0x000F135C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Usage", "CA2238:ImplementSerializationMethodsCorrectly")]
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			this._wrappedOwner = this.EntityWrapperFactory.WrapEntityUsingContext(this._owner, this.ObjectContext);
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000F317C File Offset: 0x000F137C
		internal NavigationProperty NavigationProperty
		{
			get
			{
				if (this.navigationPropertyCache == null && this._wrappedOwner.Context != null && this.TargetAccessor.HasProperty)
				{
					string propertyName = this.TargetAccessor.PropertyName;
					EntityType item = this._wrappedOwner.Context.MetadataWorkspace.GetItem<EntityType>(this._wrappedOwner.IdentityType.FullNameWithNesting(), DataSpace.OSpace);
					NavigationProperty navigationProperty;
					if (!item.NavigationProperties.TryGetValue(propertyName, false, out navigationProperty))
					{
						throw Error.RelationshipManager_NavigationPropertyNotFound(propertyName);
					}
					this.navigationPropertyCache = navigationProperty;
				}
				return this.navigationPropertyCache;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000F3203 File Offset: 0x000F1403
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

		// Token: 0x0600338B RID: 13195 RVA: 0x000F322C File Offset: 0x000F142C
		private void EnsureRelationshipNavigationAccessorsInitialized()
		{
			if (!this.RelationshipNavigation.IsInitialized)
			{
				NavigationPropertyAccessor navigationPropertyAccessor = null;
				NavigationPropertyAccessor navigationPropertyAccessor2 = null;
				string relationshipName = this._navigation.RelationshipName;
				string from = this._navigation.From;
				string to = this._navigation.To;
				AssociationType associationType = (this.RelationMetadata as AssociationType) ?? this._wrappedOwner.RelationshipManager.GetRelationshipType(relationshipName);
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
				if (navigationPropertyAccessor == null || navigationPropertyAccessor2 == null)
				{
					throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
				}
				this.RelationshipNavigation.InitializeAccessors(navigationPropertyAccessor, navigationPropertyAccessor2);
			}
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000F3304 File Offset: 0x000F1504
		internal bool DisableLazyLoading()
		{
			if (this._context == null)
			{
				return false;
			}
			bool lazyLoadingEnabled = this._context.ContextOptions.LazyLoadingEnabled;
			this._context.ContextOptions.LazyLoadingEnabled = false;
			return lazyLoadingEnabled;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000F333E File Offset: 0x000F153E
		internal void ResetLazyLoading(bool state)
		{
			if (this._context != null)
			{
				this._context.ContextOptions.LazyLoadingEnabled = state;
			}
		}

		// Token: 0x0400137B RID: 4987
		private const string _entityKeyParamName = "EntityKeyValue";

		// Token: 0x0400137C RID: 4988
		[Obsolete]
		private IEntityWithRelationships _owner;

		// Token: 0x0400137D RID: 4989
		private RelationshipNavigation _navigation;

		// Token: 0x0400137E RID: 4990
		private IRelationshipFixer _relationshipFixer;

		// Token: 0x0400137F RID: 4991
		internal bool _isLoaded;

		// Token: 0x04001380 RID: 4992
		[NonSerialized]
		private RelationshipSet _relationshipSet;

		// Token: 0x04001381 RID: 4993
		[NonSerialized]
		private ObjectContext _context;

		// Token: 0x04001382 RID: 4994
		[NonSerialized]
		private bool _usingNoTracking;

		// Token: 0x04001383 RID: 4995
		[NonSerialized]
		private RelationshipType _relationMetadata;

		// Token: 0x04001384 RID: 4996
		[NonSerialized]
		private RelationshipEndMember _fromEndMember;

		// Token: 0x04001385 RID: 4997
		[NonSerialized]
		private RelationshipEndMember _toEndMember;

		// Token: 0x04001386 RID: 4998
		[NonSerialized]
		private string _sourceQuery;

		// Token: 0x04001387 RID: 4999
		[NonSerialized]
		private IEnumerable<EdmMember> _sourceQueryParamProperties;

		// Token: 0x04001388 RID: 5000
		[NonSerialized]
		internal bool _suppressEvents;

		// Token: 0x04001389 RID: 5001
		[NonSerialized]
		internal CollectionChangeEventHandler _onAssociationChanged;

		// Token: 0x0400138A RID: 5002
		[NonSerialized]
		private IEntityWrapper _wrappedOwner;

		// Token: 0x0400138B RID: 5003
		[NonSerialized]
		private EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x0400138C RID: 5004
		[NonSerialized]
		private NavigationProperty navigationPropertyCache;
	}
}
