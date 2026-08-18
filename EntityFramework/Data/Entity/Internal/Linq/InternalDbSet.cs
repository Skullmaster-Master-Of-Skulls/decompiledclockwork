using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000792 RID: 1938
	internal class InternalDbSet<TEntity> : DbSet, IQueryable<TEntity>, IEnumerable<!0>, IQueryable, IEnumerable, IDbAsyncEnumerable<!0>, IDbAsyncEnumerable where TEntity : class
	{
		// Token: 0x060057B8 RID: 22456 RVA: 0x001798E7 File Offset: 0x00177AE7
		public InternalDbSet(IInternalSet<TEntity> internalSet)
		{
			this._internalSet = internalSet;
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x001798F6 File Offset: 0x00177AF6
		public static InternalDbSet<TEntity> Create(InternalContext internalContext, IInternalSet internalSet)
		{
			return new InternalDbSet<TEntity>(((IInternalSet<TEntity>)internalSet) ?? new InternalSet<TEntity>(internalContext));
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060057BA RID: 22458 RVA: 0x0017990D File Offset: 0x00177B0D
		internal override IInternalQuery InternalQuery
		{
			get
			{
				return this._internalSet;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060057BB RID: 22459 RVA: 0x00179915 File Offset: 0x00177B15
		internal override IInternalSet InternalSet
		{
			get
			{
				return this._internalSet;
			}
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x0017991D File Offset: 0x00177B1D
		public override DbQuery Include(string path)
		{
			Check.NotEmpty(path, "path");
			return new InternalDbQuery<TEntity>(this._internalSet.Include(path));
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x0017993C File Offset: 0x00177B3C
		public override DbQuery AsNoTracking()
		{
			return new InternalDbQuery<TEntity>(this._internalSet.AsNoTracking());
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x0017994E File Offset: 0x00177B4E
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public override DbQuery AsStreaming()
		{
			return new InternalDbQuery<TEntity>(this._internalSet.AsStreaming());
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x00179960 File Offset: 0x00177B60
		internal override DbQuery WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return new InternalDbQuery<TEntity>(this._internalSet.WithExecutionStrategy(executionStrategy));
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00179973 File Offset: 0x00177B73
		public override object Find(params object[] keyValues)
		{
			return this._internalSet.Find(keyValues);
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x00179986 File Offset: 0x00177B86
		internal override IInternalQuery GetInternalQueryWithCheck(string memberName)
		{
			return this._internalSet;
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x0017998E File Offset: 0x00177B8E
		internal override IInternalSet GetInternalSetWithCheck(string memberName)
		{
			return this._internalSet;
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x00179A8C File Offset: 0x00177C8C
		public override async Task<object> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			return await this._internalSet.FindAsync(cancellationToken, keyValues).WithCurrentCulture<TEntity>();
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x00179AE2 File Offset: 0x00177CE2
		public override IList Local
		{
			get
			{
				return this._internalSet.Local;
			}
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x00179AEF File Offset: 0x00177CEF
		public override object Create()
		{
			return this._internalSet.Create();
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x00179B01 File Offset: 0x00177D01
		public override object Create(Type derivedEntityType)
		{
			Check.NotNull<Type>(derivedEntityType, "derivedEntityType");
			return this._internalSet.Create(derivedEntityType);
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x00179B20 File Offset: 0x00177D20
		public IEnumerator<TEntity> GetEnumerator()
		{
			return this._internalSet.GetEnumerator();
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x00179B2D File Offset: 0x00177D2D
		public IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
		{
			return this._internalSet.GetAsyncEnumerator();
		}

		// Token: 0x04002349 RID: 9033
		private readonly IInternalSet<TEntity> _internalSet;
	}
}
