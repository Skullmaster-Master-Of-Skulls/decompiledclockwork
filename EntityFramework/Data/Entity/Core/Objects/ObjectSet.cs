using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005AA RID: 1450
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class ObjectSet<TEntity> : ObjectQuery<TEntity>, IObjectSet<TEntity>, IQueryable<TEntity>, IEnumerable<!0>, IQueryable, IEnumerable where TEntity : class
	{
		// Token: 0x0600399D RID: 14749 RVA: 0x0011163D File Offset: 0x0010F83D
		internal ObjectSet(EntitySet entitySet, ObjectContext context) : base(entitySet, context, MergeOption.AppendOnly)
		{
			this._entitySet = entitySet;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x0011164F File Offset: 0x0010F84F
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x00111657 File Offset: 0x0010F857
		public void AddObject(TEntity entity)
		{
			base.Context.AddObject(this.FullyQualifiedEntitySetName, entity);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x00111670 File Offset: 0x0010F870
		public void Attach(TEntity entity)
		{
			base.Context.AttachTo(this.FullyQualifiedEntitySetName, entity);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x00111689 File Offset: 0x0010F889
		public void DeleteObject(TEntity entity)
		{
			base.Context.DeleteObject(entity, this.EntitySet);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x001116A2 File Offset: 0x0010F8A2
		public void Detach(TEntity entity)
		{
			base.Context.Detach(entity, this.EntitySet);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x001116BB File Offset: 0x0010F8BB
		public TEntity ApplyCurrentValues(TEntity currentEntity)
		{
			return base.Context.ApplyCurrentValues<TEntity>(this.FullyQualifiedEntitySetName, currentEntity);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x001116CF File Offset: 0x0010F8CF
		public TEntity ApplyOriginalValues(TEntity originalEntity)
		{
			return base.Context.ApplyOriginalValues<TEntity>(this.FullyQualifiedEntitySetName, originalEntity);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x001116E3 File Offset: 0x0010F8E3
		public TEntity CreateObject()
		{
			return base.Context.CreateObject<TEntity>();
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x001116F0 File Offset: 0x0010F8F0
		public T CreateObject<T>() where T : class, TEntity
		{
			return base.Context.CreateObject<T>();
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x00111700 File Offset: 0x0010F900
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

		// Token: 0x040015F2 RID: 5618
		private readonly EntitySet _entitySet;
	}
}
