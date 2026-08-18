using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000009 RID: 9
	internal class EntityStore<TEntity> where TEntity : class
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00006252 File Offset: 0x00004452
		public EntityStore(DbContext context)
		{
			this.Context = context;
			this.DbEntitySet = context.Set<TEntity>();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000626D File Offset: 0x0000446D
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00006275 File Offset: 0x00004475
		public DbContext Context { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000627E File Offset: 0x0000447E
		public IQueryable<TEntity> EntitySet
		{
			get
			{
				return this.DbEntitySet;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00006286 File Offset: 0x00004486
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000628E File Offset: 0x0000448E
		public DbSet<TEntity> DbEntitySet { get; private set; }

		// Token: 0x0600005C RID: 92 RVA: 0x00006298 File Offset: 0x00004498
		public virtual Task<TEntity> GetByIdAsync(object id)
		{
			return this.DbEntitySet.FindAsync(new object[]
			{
				id
			});
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000062BC File Offset: 0x000044BC
		public void Create(TEntity entity)
		{
			this.DbEntitySet.Add(entity);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000062CB File Offset: 0x000044CB
		public void Delete(TEntity entity)
		{
			this.DbEntitySet.Remove(entity);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000062DA File Offset: 0x000044DA
		public virtual void Update(TEntity entity)
		{
			if (entity != null)
			{
				this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
			}
		}
	}
}
