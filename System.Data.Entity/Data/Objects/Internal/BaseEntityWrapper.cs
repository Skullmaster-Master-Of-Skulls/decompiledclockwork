using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000165 RID: 357
	internal abstract class BaseEntityWrapper<TEntity> : IEntityWrapper
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x0005B956 File Offset: 0x00059B56
		protected BaseEntityWrapper(TEntity entity, RelationshipManager relationshipManager)
		{
			if (relationshipManager == null)
			{
				throw EntityUtil.UnexpectedNullRelationshipManager();
			}
			this._relationshipManager = relationshipManager;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0005B970 File Offset: 0x00059B70
		protected BaseEntityWrapper(TEntity entity, RelationshipManager relationshipManager, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType)
		{
			if (relationshipManager == null)
			{
				throw EntityUtil.UnexpectedNullRelationshipManager();
			}
			this._identityType = identityType;
			this._relationshipManager = relationshipManager;
			this.RelationshipManager.SetWrappedOwner(this, entity);
			if (entitySet != null)
			{
				this.Context = context;
				this.MergeOption = mergeOption;
				this.RelationshipManager.AttachContextToRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0005B9D0 File Offset: 0x00059BD0
		public RelationshipManager RelationshipManager
		{
			get
			{
				return this._relationshipManager;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0005B9D8 File Offset: 0x00059BD8
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0005B9E0 File Offset: 0x00059BE0
		public ObjectContext Context { get; set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0005B9E9 File Offset: 0x00059BE9
		// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x0005B9F8 File Offset: 0x00059BF8
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

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0005BA1C File Offset: 0x00059C1C
		// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x0005BA29 File Offset: 0x00059C29
		public bool InitializingProxyRelatedEnds
		{
			get
			{
				return (this._flags & BaseEntityWrapper<TEntity>.WrapperFlags.InitializingRelatedEnds) > BaseEntityWrapper<TEntity>.WrapperFlags.None;
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

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0005BA4C File Offset: 0x00059C4C
		public void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			this.Context = context;
			this.MergeOption = mergeOption;
			if (entitySet != null)
			{
				this.RelationshipManager.AttachContextToRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0005BA6D File Offset: 0x00059C6D
		public void ResetContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (this.Context != context)
			{
				this.Context = context;
				this.MergeOption = mergeOption;
				this.RelationshipManager.ResetContextOnRelatedEnds(context, entitySet, mergeOption);
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0005BA94 File Offset: 0x00059C94
		public void DetachContext()
		{
			if (this.Context != null && this.Context.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				MergeOption? originalMergeOption = this.Context.ObjectStateManager.TransactionManager.OriginalMergeOption;
				MergeOption mergeOption = MergeOption.NoTracking;
				if (originalMergeOption.GetValueOrDefault() == mergeOption & originalMergeOption != null)
				{
					this.MergeOption = MergeOption.NoTracking;
					goto IL_5B;
				}
			}
			this.Context = null;
			IL_5B:
			this.RelationshipManager.DetachContextFromRelatedEnds();
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0005BB07 File Offset: 0x00059D07
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0005BB0F File Offset: 0x00059D0F
		public EntityEntry ObjectStateEntry { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0005BB18 File Offset: 0x00059D18
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

		// Token: 0x06001AAE RID: 6830
		public abstract void EnsureCollectionNotNull(RelatedEnd relatedEnd);

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001AAF RID: 6831
		// (set) Token: 0x06001AB0 RID: 6832
		public abstract EntityKey EntityKey { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001AB1 RID: 6833
		public abstract bool OwnsRelationshipManager { get; }

		// Token: 0x06001AB2 RID: 6834
		public abstract EntityKey GetEntityKeyFromEntity();

		// Token: 0x06001AB3 RID: 6835
		public abstract void SetChangeTracker(IEntityChangeTracker changeTracker);

		// Token: 0x06001AB4 RID: 6836
		public abstract void TakeSnapshot(EntityEntry entry);

		// Token: 0x06001AB5 RID: 6837
		public abstract void TakeSnapshotOfRelationships(EntityEntry entry);

		// Token: 0x06001AB6 RID: 6838
		public abstract object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x06001AB7 RID: 6839
		public abstract void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06001AB8 RID: 6840
		public abstract void RemoveNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06001AB9 RID: 6841
		public abstract void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06001ABA RID: 6842
		public abstract bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001ABB RID: 6843
		public abstract object Entity { get; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001ABC RID: 6844
		public abstract TEntity TypedEntity { get; }

		// Token: 0x06001ABD RID: 6845
		public abstract void SetCurrentValue(EntityEntry entry, StateManagerMemberMetadata member, int ordinal, object target, object value);

		// Token: 0x06001ABE RID: 6846
		public abstract void UpdateCurrentValueRecord(object value, EntityEntry entry);

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001ABF RID: 6847
		public abstract bool RequiresRelationshipChangeTracking { get; }

		// Token: 0x04000B29 RID: 2857
		private readonly RelationshipManager _relationshipManager;

		// Token: 0x04000B2A RID: 2858
		private Type _identityType;

		// Token: 0x04000B2B RID: 2859
		private BaseEntityWrapper<TEntity>.WrapperFlags _flags;

		// Token: 0x020004B8 RID: 1208
		[Flags]
		private enum WrapperFlags
		{
			// Token: 0x04001A79 RID: 6777
			None = 0,
			// Token: 0x04001A7A RID: 6778
			NoTracking = 1,
			// Token: 0x04001A7B RID: 6779
			InitializingRelatedEnds = 2
		}
	}
}
