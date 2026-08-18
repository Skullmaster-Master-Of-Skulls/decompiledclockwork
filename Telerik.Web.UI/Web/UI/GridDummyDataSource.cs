using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020010F0 RID: 4336
	internal class GridDummyDataSource : ICollection, IEnumerable
	{
		// Token: 0x0600B1A3 RID: 45475 RVA: 0x00269994 File Offset: 0x00267B94
		public GridDummyDataSource(int dataItemCount)
		{
			this.dataItemCount = dataItemCount;
		}

		// Token: 0x0600B1A4 RID: 45476 RVA: 0x002699A4 File Offset: 0x00267BA4
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x0600B1A5 RID: 45477 RVA: 0x002699D4 File Offset: 0x00267BD4
		public IEnumerator GetEnumerator()
		{
			return new GridDummyDataSource.GridDummyDataSourceEnumerator(this.dataItemCount);
		}

		// Token: 0x1700397E RID: 14718
		// (get) Token: 0x0600B1A6 RID: 45478 RVA: 0x002699E1 File Offset: 0x00267BE1
		public int Count
		{
			get
			{
				return this.dataItemCount;
			}
		}

		// Token: 0x1700397F RID: 14719
		// (get) Token: 0x0600B1A7 RID: 45479 RVA: 0x002699E9 File Offset: 0x00267BE9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003980 RID: 14720
		// (get) Token: 0x0600B1A8 RID: 45480 RVA: 0x002699EC File Offset: 0x00267BEC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003981 RID: 14721
		// (get) Token: 0x0600B1A9 RID: 45481 RVA: 0x002699EF File Offset: 0x00267BEF
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002E9E RID: 11934
		private int dataItemCount;

		// Token: 0x020010F1 RID: 4337
		private class GridDummyDataSourceEnumerator : IEnumerator
		{
			// Token: 0x0600B1AA RID: 45482 RVA: 0x002699F2 File Offset: 0x00267BF2
			public GridDummyDataSourceEnumerator(int count)
			{
				this.count = count;
				this.index = -1;
			}

			// Token: 0x0600B1AB RID: 45483 RVA: 0x00269A08 File Offset: 0x00267C08
			public bool MoveNext()
			{
				this.index++;
				return this.index < this.count;
			}

			// Token: 0x0600B1AC RID: 45484 RVA: 0x00269A26 File Offset: 0x00267C26
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x17003982 RID: 14722
			// (get) Token: 0x0600B1AD RID: 45485 RVA: 0x00269A2F File Offset: 0x00267C2F
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x04002E9F RID: 11935
			private int count;

			// Token: 0x04002EA0 RID: 11936
			private int index;
		}
	}
}
