using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003EE RID: 1006
	internal sealed class DummyDataSource : ICollection, IEnumerable
	{
		// Token: 0x06003098 RID: 12440 RVA: 0x0009E965 File Offset: 0x0009CB65
		internal DummyDataSource(int dataItemCount)
		{
			this.dataItemCount = dataItemCount;
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x0009E974 File Offset: 0x0009CB74
		public int Count
		{
			get
			{
				return this.dataItemCount;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0009E97C File Offset: 0x0009CB7C
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0009E9AC File Offset: 0x0009CBAC
		public IEnumerator GetEnumerator()
		{
			return new DummyDataSource.DummyDataSourceEnumerator(this.dataItemCount);
		}

		// Token: 0x04002095 RID: 8341
		private int dataItemCount;

		// Token: 0x020009A1 RID: 2465
		private class DummyDataSourceEnumerator : IEnumerator
		{
			// Token: 0x06006B58 RID: 27480 RVA: 0x0017E7E3 File Offset: 0x0017C9E3
			public DummyDataSourceEnumerator(int count)
			{
				this.count = count;
				this.index = -1;
			}

			// Token: 0x17001D9B RID: 7579
			// (get) Token: 0x06006B59 RID: 27481 RVA: 0x0000298D File Offset: 0x00000B8D
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006B5A RID: 27482 RVA: 0x0017E7F9 File Offset: 0x0017C9F9
			public bool MoveNext()
			{
				this.index++;
				return this.index < this.count;
			}

			// Token: 0x06006B5B RID: 27483 RVA: 0x0017E817 File Offset: 0x0017CA17
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04003934 RID: 14644
			private int count;

			// Token: 0x04003935 RID: 14645
			private int index;
		}
	}
}
