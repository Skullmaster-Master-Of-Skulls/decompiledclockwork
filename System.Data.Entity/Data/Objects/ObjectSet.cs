using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Objects
{
	// Token: 0x02000158 RID: 344
	public class ObjectSet<TEntity> : ObjectQuery<TEntity>, IObjectSet<TEntity>, IQueryable<TEntity>, IEnumerable<!0>, IEnumerable, IQueryable where TEntity : class
	{
		// Token: 0x06001975 RID: 6517 RVA: 0x0005909B File Offset: 0x0005729B
		internal ObjectSet(EntitySet entitySet, ObjectContext context) : base(entitySet, context, MergeOption.AppendOnly)
		{
			this._entitySet = entitySet;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x000590AD File Offset: 0x000572AD
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000590B5 File Offset: 0x000572B5
		public void AddObject(TEntity entity)
		{
			base.Context.AddObject(this.FullyQualifiedEntitySetName, entity);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000590CE File Offset: 0x000572CE
		public void Attach(TEntity entity)
		{
			base.Context.AttachTo(this.FullyQualifiedEntitySetName, entity);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000590E7 File Offset: 0x000572E7
		public void DeleteObject(TEntity entity)
		{
			base.Context.DeleteObject(entity, this.EntitySet);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00059100 File Offset: 0x00057300
		public void Detach(TEntity entity)
		{
			base.Context.Detach(entity, this.EntitySet);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00059119 File Offset: 0x00057319
		public TEntity ApplyCurrentValues(TEntity currentEntity)
		{
			return base.Context.ApplyCurrentValues<TEntity>(this.FullyQualifiedEntitySetName, currentEntity);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0005912D File Offset: 0x0005732D
		public TEntity ApplyOriginalValues(TEntity originalEntity)
		{
			return base.Context.ApplyOriginalValues<TEntity>(this.FullyQualifiedEntitySetName, originalEntity);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00059141 File Offset: 0x00057341
		public TEntity CreateObject()
		{
			return base.Context.CreateObject<TEntity>();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0005914E File Offset: 0x0005734E
		public T CreateObject<T>() where T : class, TEntity
		{
			return base.Context.CreateObject<T>();
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0005915B File Offset: 0x0005735B
		private string FullyQualifiedEntitySetName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					this._entitySet.EntityContainer.Name,
					this._entitySet.Name
				});
			}
		}

		// Token: 0x04000AE8 RID: 2792
		private readonly EntitySet _entitySet;
	}
}
