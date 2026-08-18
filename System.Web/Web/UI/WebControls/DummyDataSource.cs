using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200056F RID: 1391
	internal sealed class DummyDataSource : ICollection, IEnumerable
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x00119CE4 File Offset: 0x00118CE4
		internal DummyDataSource(int dataItemCount)
		{
			this.dataItemCount = dataItemCount;
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x00119CF3 File Offset: 0x00118CF3
		public int Count
		{
			get
			{
				return this.dataItemCount;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x00119CFB File Offset: 0x00118CFB
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x00119CFE File Offset: 0x00118CFE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x00119D04 File Offset: 0x00118D04
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x00119D34 File Offset: 0x00118D34
		public IEnumerator GetEnumerator()
		{
			return new DummyDataSource.DummyDataSourceEnumerator(this.dataItemCount);
		}

		// Token: 0x040029B2 RID: 10674
		private int dataItemCount;

		// Token: 0x02000570 RID: 1392
		private class DummyDataSourceEnumerator : IEnumerator
		{
			// Token: 0x06004473 RID: 17523 RVA: 0x00119D41 File Offset: 0x00118D41
			public DummyDataSourceEnumerator(int count)
			{
				this.count = count;
				this.index = -1;
			}

			// Token: 0x170010C4 RID: 4292
			// (get) Token: 0x06004474 RID: 17524 RVA: 0x00119D57 File Offset: 0x00118D57
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004475 RID: 17525 RVA: 0x00119D5A File Offset: 0x00118D5A
			public bool MoveNext()
			{
				this.index++;
				return this.index < this.count;
			}

			// Token: 0x06004476 RID: 17526 RVA: 0x00119D78 File Offset: 0x00118D78
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x040029B3 RID: 10675
			private int count;

			// Token: 0x040029B4 RID: 10676
			private int index;
		}
	}
}
