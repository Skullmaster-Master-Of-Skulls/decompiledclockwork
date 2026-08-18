using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000738 RID: 1848
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public abstract class DbQuery : IOrderedQueryable, IQueryable, IEnumerable, IListSource, IInternalQueryAdapter, IDbAsyncEnumerable
	{
		// Token: 0x06005399 RID: 21401 RVA: 0x0016FF00 File Offset: 0x0016E100
		internal DbQuery()
		{
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600539A RID: 21402 RVA: 0x0016FF08 File Offset: 0x0016E108
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0016FF0B File Offset: 0x0016E10B
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0016FF12 File Offset: 0x0016E112
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("IEnumerable.GetEnumerator").GetEnumerator();
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0016FF24 File Offset: 0x0016E124
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600539E RID: 21406 RVA: 0x0016FF36 File Offset: 0x0016E136
		public virtual Type ElementType
		{
			get
			{
				return this.GetInternalQueryWithCheck("ElementType").ElementType;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x0016FF48 File Offset: 0x0016E148
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetInternalQueryWithCheck("IQueryable.Expression").Expression;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060053A0 RID: 21408 RVA: 0x0016FF5C File Offset: 0x0016E15C
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IQueryProvider IQueryable.Provider
		{
			get
			{
				IQueryProvider result;
				if ((result = this._provider) == null)
				{
					result = (this._provider = new NonGenericDbQueryProvider(this.GetInternalQueryWithCheck("IQueryable.Provider").InternalContext, this.GetInternalQueryWithCheck("IQueryable.Provider")));
				}
				return result;
			}
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0016FF9C File Offset: 0x0016E19C
		public virtual DbQuery Include(string path)
		{
			return this;
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x0016FF9F File Offset: 0x0016E19F
		public virtual DbQuery AsNoTracking()
		{
			return this;
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0016FFA2 File Offset: 0x0016E1A2
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbQuery AsStreaming()
		{
			return this;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0016FFA5 File Offset: 0x0016E1A5
		internal virtual DbQuery WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return this;
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0016FFA8 File Offset: 0x0016E1A8
		public DbQuery<TElement> Cast<TElement>()
		{
			if (this.InternalQuery == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			if (typeof(TElement) != this.InternalQuery.ElementType)
			{
				throw Error.DbEntity_BadTypeForCast(typeof(DbQuery).Name, typeof(TElement).Name, this.InternalQuery.ElementType.Name);
			}
			return new DbQuery<TElement>((IInternalQuery<TElement>)this.InternalQuery);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00170028 File Offset: 0x0016E228
		public override string ToString()
		{
			if (this.InternalQuery != null)
			{
				return this.InternalQuery.ToString();
			}
			return base.ToString();
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x00170044 File Offset: 0x0016E244
		internal virtual IInternalQuery InternalQuery
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00170047 File Offset: 0x0016E247
		internal virtual IInternalQuery GetInternalQueryWithCheck(string memberName)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x0017006E File Offset: 0x0016E26E
		IInternalQuery IInternalQueryAdapter.InternalQuery
		{
			get
			{
				return this.InternalQuery;
			}
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x00170076 File Offset: 0x0016E276
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x0017007F File Offset: 0x0016E27F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00170087 File Offset: 0x0016E287
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400226F RID: 8815
		private IQueryProvider _provider;
	}
}
