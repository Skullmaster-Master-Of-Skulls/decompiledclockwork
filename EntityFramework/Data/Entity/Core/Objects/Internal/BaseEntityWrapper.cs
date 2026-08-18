using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000573 RID: 1395
	internal abstract class BaseEntityWrapper<TEntity> : IEntityWrapper where TEntity : class
	{
		// Token: 0x06003675 RID: 13941 RVA: 0x00103119 File Offset: 0x00101319
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "entity")]
		protected BaseEntityWrapper(TEntity entity, RelationshipManager relationshipManager, bool overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._relationshipManager = relationshipManager;
			if (overridesEquals)
			{
				this._flags = BaseEntityWrapper<TEntity>.WrapperFlags.OverridesEquals;
			}
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x00103140 File Offset: 0x00101340
		protected BaseEntityWrapper(TEntity entity, RelationshipManager relationshipManager, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, bool overridesEquals)
		{
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			this._identityType = identityType;
			this._relationshipManager = relationshipManager;
			if (overridesEquals)
			{
				this._flags = BaseEntityWrapper<TEntity>.WrapperFlags.OverridesEquals;
			}
			this.RelationshipManager.SetWrappedOwner(this, entity);
			if (entitySet != null)
			{
				this.Context = context;
				this.MergeOption = mergeOption;
				this.RelationshipManager.AttachContextToRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x001031B0 File Offset: 0x001013B0
		public RelationshipManager RelationshipManager
		{
			get
			{
				return this._relationshipManager;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x001031B8 File Offset: 0x001013B8
		// (set) Token: 0x06003679 RID: 13945 RVA: 0x001031C0 File Offset: 0x001013C0
		public ObjectContext Context { get; set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x001031C9 File Offset: 0x001013C9
		// (set) Token: 0x0600367B RID: 13947 RVA: 0x001031D8 File Offset: 0x001013D8
		public MergeOption MergeOption
		{
			get
			{
				if ((this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.NoTracking) == BaseEntityWrapper<TEntity>.WrapperFlags.None)
				{
					return MergeOption.AppendOnly;
				}
				return MergeOption.NoTracking;
			}
			private set
			{
				if (value == MergeOption.NoTracking)
				{
					this._flags |= BaseEntityWrapper<TEntity>.WrapperFlags.NoTracking;
					return;
				}
				this._flags &= ~BaseEntityWrapper<TEntity>.WrapperFlags.NoTracking;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x001031FC File Offset: 0x001013FC
		// (set) Token: 0x0600367D RID: 13949 RVA: 0x0010320C File Offset: 0x0010140C
		public bool InitializingProxyRelatedEnds
		{
			get
			{
				return (this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds) != BaseEntityWrapper<TEntity>.WrapperFlags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds;
					return;
				}
				this._flags &= ~BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds;
			}
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x0010322F File Offset: 0x0010142F
		public void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			this.Context = context;
			this.MergeOption = mergeOption;
			if (entitySet != null)
			{
				this.RelationshipManager.AttachContextToRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x00103250 File Offset: 0x00101450
		public void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (!object.ReferenceEquals(this.Context, context))
			{
				this.Context = context;
				this.MergeOption = mergeOption;
				this.RelationshipManager.ResetContextOnRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0010327C File Offset: 0x0010147C
		public void DetachContext()
		{
			if (this.Context != null && this.Context.ObjectStateManager.TransactionManager.IsAttachTracking && this.Context.ObjectStateManager.TransactionManager.OriginalMergeOption == MergeOption.NoTracking)
			{
				this.MergeOption = MergeOption.NoTracking;
			}
			else
			{
				this.Context = null;
			}
			this.RelationshipManager.DetachContextFromRelatedEnds();
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x001032EF File Offset: 0x001014EF
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x001032F7 File Offset: 0x001014F7
		public EntityEntry ObjectStateEntry { get; set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x00103300 File Offset: 0x00101500
		public Type IdentityType
		{
			get
			{
				if (this._identityType == null)
				{
					this._identityType = EntityUtil.GetEntityIdentityType(typeof(TEntity));
				}
				return this._identityType;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x0010332B File Offset: 0x0010152B
		public bool OverridesEqualsOrGetHashCode
		{
			get
			{
				return (this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.OverridesEquals) != BaseEntityWrapper<TEntity>.WrapperFlags.None;
			}
		}

		// Token: 0x06003685 RID: 13957
		public abstract void EnsureCollectionNotNull(RelatedEnd relatedEnd);

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003686 RID: 13958
		// (set) Token: 0x06003687 RID: 13959
		public abstract EntityKey EntityKey { get; set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06003688 RID: 13960
		public abstract bool OwnsRelationshipManager { get; }

		// Token: 0x06003689 RID: 13961
		public abstract EntityKey GetEntityKeyFromEntity();

		// Token: 0x0600368A RID: 13962
		public abstract void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x0600368B RID: 13963
		public abstract void TakeSnapshot(EntityEntry entry);

		// Token: 0x0600368C RID: 13964
		public abstract void TakeSnapshotOfRelationships(EntityEntry entry);

		// Token: 0x0600368D RID: 13965
		public abstract object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x0600368E RID: 13966
		public abstract void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x0600368F RID: 13967
		public abstract void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06003690 RID: 13968
		public abstract void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06003691 RID: 13969
		public abstract bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06003692 RID: 13970
		public abstract object Entity { get; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06003693 RID: 13971
		public abstract TEntity TypedEntity { get; }

		// Token: 0x06003694 RID: 13972
		public abstract void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x06003695 RID: 13973
		public abstract void UpdateCurrentValueRecord(object value, EntityEntry entry);

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003696 RID: 13974
		public abstract bool RequiresRelationshipChangeTracking { get; }

		// Token: 0x040014CF RID: 5327
		private readonly RelationshipManager _relationshipManager;

		// Token: 0x040014D0 RID: 5328
		private Type _identityType;

		// Token: 0x040014D1 RID: 5329
		private BaseEntityWrapper<TEntity>.WrapperFlags _flags;

		// Token: 0x02000574 RID: 1396
		[Flags]
		private enum WrapperFlags
		{
			// Token: 0x040014D5 RID: 5333
			None = 0,
			// Token: 0x040014D6 RID: 5334
			NoTracking = 1,
			// Token: 0x040014D7 RID: 5335
			InitializingRelatedEnds = 2,
			// Token: 0x040014D8 RID: 5336
			OverridesEquals = 4
		}
	}
}
