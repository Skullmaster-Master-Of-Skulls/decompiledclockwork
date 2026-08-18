using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x0200073A RID: 1850
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name is intentional")]
	public abstract class DbSet : DbQuery, IInternalSetAdapter
	{
		// Token: 0x060053AE RID: 21422 RVA: 0x0017008F File Offset: 0x0016E28F
		protected internal DbSet()
		{
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00170097 File Offset: 0x0016E297
		public virtual object Find(params object[] keyValues)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("Find", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x001700C2 File Offset: 0x0016E2C2
		public virtual Task<object> FindAsync(params object[] keyValues)
		{
			return this.FindAsync(CancellationToken.None, keyValues);
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x001700D0 File Offset: 0x0016E2D0
		public virtual Task<object> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("FindAsync", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x001700FB File Offset: 0x0016E2FB
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public virtual IList Local
		{
			get
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented("Local", this.GetType().Name, typeof(DbSet).Name));
			}
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00170126 File Offset: 0x0016E326
		public virtual object Attach(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.GetInternalSetWithCheck("Attach").Attach(entity);
			return entity;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x00170146 File Offset: 0x0016E346
		public virtual object Add(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.GetInternalSetWithCheck("Add").Add(entity);
			return entity;
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x00170166 File Offset: 0x0016E366
		public virtual IEnumerable AddRange(IEnumerable entities)
		{
			Check.NotNull<IEnumerable>(entities, "entities");
			this.GetInternalSetWithCheck("AddRange").AddRange(entities);
			return entities;
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x00170186 File Offset: 0x0016E386
		public virtual object Remove(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.GetInternalSetWithCheck("Remove").Remove(entity);
			return entity;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x001701A6 File Offset: 0x0016E3A6
		public virtual IEnumerable RemoveRange(IEnumerable entities)
		{
			Check.NotNull<IEnumerable>(entities, "entities");
			this.GetInternalSetWithCheck("RemoveRange").RemoveRange(entities);
			return entities;
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x001701C6 File Offset: 0x0016E3C6
		public virtual object Create()
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("Create", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x001701F1 File Offset: 0x0016E3F1
		public virtual object Create(Type derivedEntityType)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("Create", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0017021C File Offset: 0x0016E41C
		public new DbSet<TEntity> Cast<TEntity>() where TEntity : class
		{
			if (this.InternalSet == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			if (typeof(TEntity) != this.InternalSet.ElementType)
			{
				throw Error.DbEntity_BadTypeForCast(typeof(DbSet).Name, typeof(TEntity).Name, this.InternalSet.ElementType.Name);
			}
			return (DbSet<TEntity>)this.InternalSet.InternalContext.Set<TEntity>();
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x001702A1 File Offset: 0x0016E4A1
		IInternalSet IInternalSetAdapter.InternalSet
		{
			get
			{
				return this.InternalSet;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060053BC RID: 21436 RVA: 0x001702A9 File Offset: 0x0016E4A9
		internal virtual IInternalSet InternalSet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x001702AC File Offset: 0x0016E4AC
		internal virtual IInternalSet GetInternalSetWithCheck(string memberName)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x001702D3 File Offset: 0x0016E4D3
		public virtual DbSqlQuery SqlQuery(string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbSqlQuery((this.InternalSet == null) ? null : new InternalSqlSetQuery(this.InternalSet, sql, false, parameters));
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0017030B File Offset: 0x0016E50B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00170314 File Offset: 0x0016E514
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0017031C File Offset: 0x0016E51C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
