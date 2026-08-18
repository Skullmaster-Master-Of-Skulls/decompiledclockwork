using System;

namespace System.Data
{
	// Token: 0x020000A8 RID: 168
	public class DataColumnChangeEventArgs : EventArgs
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x0005AA68 File Offset: 0x00059E68
		internal DataColumnChangeEventArgs(DataRow row)
		{
			this._row = row;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0005AA84 File Offset: 0x00059E84
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this._row = row;
			this._column = column;
			this._proposedValue = value;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0005AAAC File Offset: 0x00059EAC
		public DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0005AAC0 File Offset: 0x00059EC0
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0005AAD4 File Offset: 0x00059ED4
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0005AAE8 File Offset: 0x00059EE8
		public object ProposedValue
		{
			get
			{
				return this._proposedValue;
			}
			set
			{
				this._proposedValue = value;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0005AAFC File Offset: 0x00059EFC
		internal void InitializeColumnChangeEvent(DataColumn column, object value)
		{
			this._column = column;
			this._proposedValue = value;
		}

		// Token: 0x04000317 RID: 791
		private readonly DataRow _row;

		// Token: 0x04000318 RID: 792
		private DataColumn _column;

		// Token: 0x04000319 RID: 793
		private object _proposedValue;
	}
}
