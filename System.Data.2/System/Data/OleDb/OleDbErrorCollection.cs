using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x0200024F RID: 591
	[ListBindable(false)]
	[Serializable]
	public sealed class OleDbErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x0010084C File Offset: 0x000FFC4C
		internal OleDbErrorCollection(UnsafeNativeMethods.IErrorInfo errorInfo)
		{
			ArrayList arrayList = new ArrayList();
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OS> IErrorRecords\n");
			UnsafeNativeMethods.IErrorRecords errorRecords = errorInfo as UnsafeNativeMethods.IErrorRecords;
			if (errorRecords != null)
			{
				int recordCount = errorRecords.GetRecordCount();
				Bid.Trace("<oledb.IErrorRecords.GetRecordCount|API|OS|RET> RecordCount=%d\n", recordCount);
				for (int i = 0; i < recordCount; i++)
				{
					OleDbError value = new OleDbError(errorRecords, i);
					arrayList.Add(value);
				}
			}
			this.items = arrayList;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x001008B0 File Offset: 0x000FFCB0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x001008C0 File Offset: 0x000FFCC0
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x001008D0 File Offset: 0x000FFCD0
		public int Count
		{
			get
			{
				ArrayList arrayList = this.items;
				if (arrayList == null)
				{
					return 0;
				}
				return arrayList.Count;
			}
		}

		// Token: 0x17000619 RID: 1561
		public OleDbError this[int index]
		{
			get
			{
				return this.items[index] as OleDbError;
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00100910 File Offset: 0x000FFD10
		internal void AddRange(ICollection c)
		{
			this.items.AddRange(c);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0010092C File Offset: 0x000FFD2C
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00100948 File Offset: 0x000FFD48
		public void CopyTo(OleDbError[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00100964 File Offset: 0x000FFD64
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400160A RID: 5642
		private readonly ArrayList items;
	}
}
