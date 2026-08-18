using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000586 RID: 1414
	internal sealed class EntityWrapperWithoutRelationships<TEntity> : EntityWrapper<TEntity> where TEntity : class
	{
		// Token: 0x06003747 RID: 14151 RVA: 0x00106280 File Offset: 0x00104480
		internal EntityWrapperWithoutRelationships(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals) : base(entity, RelationshipManager.Create(), key, entitySet, context, mergeOption, identityType, propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x001062A9 File Offset: 0x001044A9
		internal EntityWrapperWithoutRelationships(TEntity entity, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals) : base(entity, RelationshipManager.Create(), propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x001062BD File Offset: 0x001044BD
		public override bool OwnsRelationshipManager
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x001062C0 File Offset: 0x001044C0
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
			entry.TakeSnapshotOfRelationships();
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x001062C8 File Offset: 0x001044C8
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return true;
			}
		}
	}
}
