using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000587 RID: 1415
	internal sealed class EntityWrapperWithRelationships<TEntity> : EntityWrapper<TEntity> where TEntity : class, IEntityWithRelationships
	{
		// Token: 0x0600374C RID: 14156 RVA: 0x001062CC File Offset: 0x001044CC
		internal EntityWrapperWithRelationships(TEntity entity, EntityKey key, EntitySet entitySet, ObjectContext context, MergeOption mergeOption, Type identityType, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals) : base(entity, entity.RelationshipManager, key, entitySet, context, mergeOption, identityType, propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x001062FD File Offset: 0x001044FD
		internal EntityWrapperWithRelationships(TEntity entity, Func<object, IPropertyAccessorStrategy> propertyStrategy, Func<object, IChangeTrackingStrategy> changeTrackingStrategy, Func<object, IEntityKeyStrategy> keyStrategy, bool overridesEquals) : base(entity, entity.RelationshipManager, propertyStrategy, changeTrackingStrategy, keyStrategy, overridesEquals)
		{
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x00106319 File Offset: 0x00104519
		public override bool OwnsRelationshipManager
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x0010631C File Offset: 0x0010451C
		public override void TakeSnapshotOfRelationships(EntityEntry entry)
		{
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x0010631E File Offset: 0x0010451E
		public override bool RequiresRelationshipChangeTracking
		{
			get
			{
				return false;
			}
		}
	}
}
