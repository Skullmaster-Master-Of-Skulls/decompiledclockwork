using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x02000226 RID: 550
	[ListBindable(false)]
	[Serializable]
	public sealed class OleDbErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x06001F82 RID: 8066 RVA: 0x0027B338 File Offset: 0x0027A738
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0027B3A8 File Offset: 0x0027A7A8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0027B3B8 File Offset: 0x0027A7B8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0027B3C8 File Offset: 0x0027A7C8
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

		// Token: 0x17000450 RID: 1104
		public OleDbError this[int index]
		{
			get
			{
				return this.items[index] as OleDbError;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0027B408 File Offset: 0x0027A808
		internal void AddRange(ICollection c)
		{
			this.items.AddRange(c);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0027B428 File Offset: 0x0027A828
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0027B448 File Offset: 0x0027A848
		public void CopyTo(OleDbError[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0027B468 File Offset: 0x0027A868
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040012F5 RID: 4853
		private readonly ArrayList items;
	}
}
