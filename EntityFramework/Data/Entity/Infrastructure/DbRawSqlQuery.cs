using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000299 RID: 665
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class DbRawSqlQuery : IEnumerable, IListSource, IDbAsyncEnumerable
	{
		// Token: 0x0600177B RID: 6011 RVA: 0x0007881E File Offset: 0x00076A1E
		internal DbRawSqlQuery(InternalSqlQuery internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0007882D File Offset: 0x00076A2D
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbRawSqlQuery AsStreaming()
		{
			if (this._internalQuery != null)
			{
				return new DbRawSqlQuery(this._internalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00078849 File Offset: 0x00076A49
		public virtual IEnumerator GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("GetEnumerator").GetEnumerator();
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0007885B File Offset: 0x00076A5B
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0007886D File Offset: 0x00076A6D
		public virtual Task ForEachAsync(Action<object> action)
		{
			Check.NotNull<Action<object>>(action, "action");
			return this.ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00078887 File Offset: 0x00076A87
		public virtual Task ForEachAsync(Action<object> action, CancellationToken cancellationToken)
		{
			Check.NotNull<Action<object>>(action, "action");
			return this.ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0007889D File Offset: 0x00076A9D
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual Task<List<object>> ToListAsync()
		{
			return this.ToListAsync<object>();
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000788A5 File Offset: 0x00076AA5
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual Task<List<object>> ToListAsync(CancellationToken cancellationToken)
		{
			return this.ToListAsync(cancellationToken);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000788AE File Offset: 0x00076AAE
		public override string ToString()
		{
			if (this._internalQuery != null)
			{
				return this._internalQuery.ToString();
			}
			return base.ToString();
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x000788CA File Offset: 0x00076ACA
		internal InternalSqlQuery InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000788D2 File Offset: 0x00076AD2
		private InternalSqlQuery GetInternalQueryWithCheck(string memberName)
		{
			if (this._internalQuery == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSqlQuery).Name));
			}
			return this._internalQuery;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00078908 File Offset: 0x00076B08
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0007890B File Offset: 0x00076B0B
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00078912 File Offset: 0x00076B12
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0007891B File Offset: 0x00076B1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00078923 File Offset: 0x00076B23
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000857 RID: 2135
		private readonly InternalSqlQuery _internalQuery;
	}
}
