using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Objects
{
	// Token: 0x02000138 RID: 312
	public abstract class ObjectResult : IEnumerable, IDisposable, IListSource
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x00002050 File Offset: 0x00000250
		internal ObjectResult()
		{
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0004C2DD File Offset: 0x0004A4DD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorInternal();
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0004C2E5 File Offset: 0x0004A4E5
		IList IListSource.GetList()
		{
			return this.GetIListSourceListInternal();
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060016AF RID: 5807
		public abstract Type ElementType { get; }

		// Token: 0x060016B0 RID: 5808
		public abstract void Dispose();

		// Token: 0x060016B1 RID: 5809 RVA: 0x0004C2ED File Offset: 0x0004A4ED
		public ObjectResult<TElement> GetNextResult<TElement>()
		{
			return this.GetNextResultInternal<TElement>();
		}

		// Token: 0x060016B2 RID: 5810
		internal abstract IEnumerator GetEnumeratorInternal();

		// Token: 0x060016B3 RID: 5811
		internal abstract IList GetIListSourceListInternal();

		// Token: 0x060016B4 RID: 5812
		internal abstract ObjectResult<TElement> GetNextResultInternal<TElement>();
	}
}
