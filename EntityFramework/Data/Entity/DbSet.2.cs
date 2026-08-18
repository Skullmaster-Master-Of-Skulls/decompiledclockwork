using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x0200073D RID: 1853
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name is intentional")]
	public class DbSet<TEntity> : DbQuery<TEntity>, IDbSet<TEntity>, IQueryable<TEntity>, IEnumerable<!0>, IQueryable, IEnumerable, IInternalSetAdapter where TEntity : class
	{
		// Token: 0x060053DF RID: 21471 RVA: 0x00170503 File Offset: 0x0016E703
		internal DbSet(InternalSet<TEntity> internalSet) : base(internalSet)
		{
			this._internalSet = internalSet;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x00170513 File Offset: 0x0016E713
		protected DbSet() : this(null)
		{
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x0017051C File Offset: 0x0016E71C
		public virtual TEntity Find(params object[] keyValues)
		{
			return this.GetInternalSetWithCheck("Find").Find(keyValues);
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0017052F File Offset: 0x0016E72F
		public virtual Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			return this.GetInternalSetWithCheck("FindAsync").FindAsync(cancellationToken, keyValues);
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x00170543 File Offset: 0x0016E743
		public virtual Task<TEntity> FindAsync(params object[] keyValues)
		{
			return this.FindAsync(CancellationToken.None, keyValues);
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x00170551 File Offset: 0x0016E751
		public virtual ObservableCollection<TEntity> Local
		{
			get
			{
				return this.GetInternalSetWithCheck("Local").Local;
			}
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x00170563 File Offset: 0x0016E763
		public virtual TEntity Attach(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.GetInternalSetWithCheck("Attach").Attach(entity);
			return entity;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x00170588 File Offset: 0x0016E788
		public virtual TEntity Add(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.GetInternalSetWithCheck("Add").Add(entity);
			return entity;
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x001705AD File Offset: 0x0016E7AD
		public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
		{
			Check.NotNull<IEnumerable<TEntity>>(entities, "entities");
			this.GetInternalSetWithCheck("AddRange").AddRange(entities);
			return entities;
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x001705CD File Offset: 0x0016E7CD
		public virtual TEntity Remove(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.GetInternalSetWithCheck("Remove").Remove(entity);
			return entity;
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x001705F2 File Offset: 0x0016E7F2
		public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
		{
			Check.NotNull<IEnumerable<TEntity>>(entities, "entities");
			this.GetInternalSetWithCheck("RemoveRange").RemoveRange(entities);
			return entities;
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x00170612 File Offset: 0x0016E812
		public virtual TEntity Create()
		{
			return this.GetInternalSetWithCheck("Create").Create();
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x00170624 File Offset: 0x0016E824
		public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
		{
			return (TDerivedEntity)((object)this.GetInternalSetWithCheck("Create").Create(typeof(TDerivedEntity)));
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0017064C File Offset: 0x0016E84C
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public static implicit operator DbSet(DbSet<TEntity> entry)
		{
			Check.NotNull<DbSet<TEntity>>(entry, "entry");
			if (entry._internalSet == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			return (DbSet)entry._internalSet.InternalContext.Set(entry._internalSet.ElementType);
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060053ED RID: 21485 RVA: 0x00170698 File Offset: 0x0016E898
		IInternalSet IInternalSetAdapter.InternalSet
		{
			get
			{
				return this._internalSet;
			}
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x001706A0 File Offset: 0x0016E8A0
		private InternalSet<TEntity> GetInternalSetWithCheck(string memberName)
		{
			if (this._internalSet == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet<>).Name));
			}
			return this._internalSet;
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x001706D6 File Offset: 0x0016E8D6
		public virtual DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbSqlQuery<TEntity>((this._internalSet != null) ? new InternalSqlSetQuery(this._internalSet, sql, false, parameters) : null);
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0017070E File Offset: 0x0016E90E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x00170717 File Offset: 0x0016E917
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0017071F File Offset: 0x0016E91F
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002272 RID: 8818
		private readonly InternalSet<TEntity> _internalSet;
	}
}
