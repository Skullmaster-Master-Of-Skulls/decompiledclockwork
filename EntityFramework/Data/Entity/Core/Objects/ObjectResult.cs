using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A8 RID: 1448
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public abstract class ObjectResult : IEnumerable, IDisposable, IListSource, IDbAsyncEnumerable
	{
		// Token: 0x06003982 RID: 14722 RVA: 0x001113DE File Offset: 0x0010F5DE
		protected internal ObjectResult()
		{
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x001113E6 File Offset: 0x0010F5E6
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetAsyncEnumeratorInternal();
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x001113EE File Offset: 0x0010F5EE
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorInternal();
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x001113F6 File Offset: 0x0010F5F6
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x001113F9 File Offset: 0x0010F5F9
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			return this.GetIListSourceListInternal();
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003987 RID: 14727
		public abstract Type ElementType { get; }

		// Token: 0x06003988 RID: 14728 RVA: 0x00111401 File Offset: 0x0010F601
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003989 RID: 14729
		protected abstract void Dispose(bool disposing);

		// Token: 0x0600398A RID: 14730 RVA: 0x00111410 File Offset: 0x0010F610
		public virtual ObjectResult<TElement> GetNextResult<TElement>()
		{
			return this.GetNextResultInternal<TElement>();
		}

		// Token: 0x0600398B RID: 14731
		internal abstract IDbAsyncEnumerator GetAsyncEnumeratorInternal();

		// Token: 0x0600398C RID: 14732
		internal abstract IEnumerator GetEnumeratorInternal();

		// Token: 0x0600398D RID: 14733
		internal abstract IList GetIListSourceListInternal();

		// Token: 0x0600398E RID: 14734
		internal abstract ObjectResult<TElement> GetNextResultInternal<TElement>();
	}
}
