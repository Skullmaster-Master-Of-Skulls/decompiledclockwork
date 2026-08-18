using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200073B RID: 1851
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name is intentional")]
	public class DbQuery<TResult> : IOrderedQueryable<TResult>, IQueryable<TResult>, IEnumerable<!0>, IOrderedQueryable, IQueryable, IEnumerable, IListSource, IInternalQueryAdapter, IDbAsyncEnumerable<!0>, IDbAsyncEnumerable
	{
		// Token: 0x060053C2 RID: 21442 RVA: 0x00170324 File Offset: 0x0016E524
		internal DbQuery(IInternalQuery<TResult> internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00170333 File Offset: 0x0016E533
		public virtual DbQuery<TResult> Include(string path)
		{
			Check.NotEmpty(path, "path");
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.Include(path));
			}
			return this;
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0017035C File Offset: 0x0016E55C
		public virtual DbQuery<TResult> AsNoTracking()
		{
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.AsNoTracking());
			}
			return this;
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00170378 File Offset: 0x0016E578
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbQuery<TResult> AsStreaming()
		{
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00170394 File Offset: 0x0016E594
		internal virtual DbQuery<TResult> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.WithExecutionStrategy(executionStrategy));
			}
			return this;
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x001703B1 File Offset: 0x0016E5B1
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x001703B4 File Offset: 0x0016E5B4
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x001703BB File Offset: 0x0016E5BB
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerator<TResult> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("IEnumerable<TResult>.GetEnumerator").GetEnumerator();
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x001703CD File Offset: 0x0016E5CD
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("IEnumerable.GetEnumerator").GetEnumerator();
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x001703DF File Offset: 0x0016E5DF
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x001703F1 File Offset: 0x0016E5F1
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator<TResult> IDbAsyncEnumerable<!0>.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable<TResult>.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060053CD RID: 21453 RVA: 0x00170403 File Offset: 0x0016E603
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		Type IQueryable.ElementType
		{
			get
			{
				return this.GetInternalQueryWithCheck("IQueryable.ElementType").ElementType;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x00170415 File Offset: 0x0016E615
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetInternalQueryWithCheck("IQueryable.Expression").Expression;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x00170428 File Offset: 0x0016E628
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IQueryProvider IQueryable.Provider
		{
			get
			{
				IQueryProvider result;
				if ((result = this._provider) == null)
				{
					result = (this._provider = new DbQueryProvider(this.GetInternalQueryWithCheck("IQueryable.Provider").InternalContext, this.GetInternalQueryWithCheck("IQueryable.Provider")));
				}
				return result;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x00170468 File Offset: 0x0016E668
		IInternalQuery IInternalQueryAdapter.InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x00170470 File Offset: 0x0016E670
		internal IInternalQuery<TResult> InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00170478 File Offset: 0x0016E678
		private IInternalQuery<TResult> GetInternalQueryWithCheck(string memberName)
		{
			if (this._internalQuery == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet<>).Name));
			}
			return this._internalQuery;
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x001704AE File Offset: 0x0016E6AE
		public override string ToString()
		{
			if (this._internalQuery != null)
			{
				return this._internalQuery.ToString();
			}
			return base.ToString();
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x001704CA File Offset: 0x0016E6CA
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Intentionally just implicit to reduce API clutter.")]
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public static implicit operator DbQuery(DbQuery<TResult> entry)
		{
			if (entry._internalQuery == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			return new InternalDbQuery<TResult>(entry._internalQuery);
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x001704EA File Offset: 0x0016E6EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x001704F3 File Offset: 0x0016E6F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x001704FB File Offset: 0x0016E6FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002270 RID: 8816
		private readonly IInternalQuery<TResult> _internalQuery;

		// Token: 0x04002271 RID: 8817
		private IQueryProvider _provider;
	}
}
