using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000791 RID: 1937
	internal class InternalDbQuery<TElement> : DbQuery, IOrderedQueryable<TElement>, IQueryable<TElement>, IEnumerable<!0>, IOrderedQueryable, IQueryable, IEnumerable, IDbAsyncEnumerable<!0>, IDbAsyncEnumerable
	{
		// Token: 0x060057AF RID: 22447 RVA: 0x00179858 File Offset: 0x00177A58
		public InternalDbQuery(IInternalQuery<TElement> internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x00179867 File Offset: 0x00177A67
		internal override IInternalQuery InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x0017986F File Offset: 0x00177A6F
		public override DbQuery Include(string path)
		{
			Check.NotEmpty(path, "path");
			return new InternalDbQuery<TElement>(this._internalQuery.Include(path));
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0017988E File Offset: 0x00177A8E
		public override DbQuery AsNoTracking()
		{
			return new InternalDbQuery<TElement>(this._internalQuery.AsNoTracking());
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x001798A0 File Offset: 0x00177AA0
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public override DbQuery AsStreaming()
		{
			return new InternalDbQuery<TElement>(this._internalQuery.AsStreaming());
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x001798B2 File Offset: 0x00177AB2
		internal override DbQuery WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return new InternalDbQuery<TElement>(this._internalQuery.WithExecutionStrategy(executionStrategy));
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x001798C5 File Offset: 0x00177AC5
		internal override IInternalQuery GetInternalQueryWithCheck(string memberName)
		{
			return this._internalQuery;
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x001798CD File Offset: 0x00177ACD
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._internalQuery.GetEnumerator();
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001798DA File Offset: 0x00177ADA
		public IDbAsyncEnumerator<TElement> GetAsyncEnumerator()
		{
			return this._internalQuery.GetAsyncEnumerator();
		}

		// Token: 0x04002348 RID: 9032
		private readonly IInternalQuery<TElement> _internalQuery;
	}
}
