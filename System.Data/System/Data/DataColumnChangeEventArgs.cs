using System;

namespace System.Data
{
	// Token: 0x02000069 RID: 105
	public class DataColumnChangeEventArgs : EventArgs
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x001EBA58 File Offset: 0x001EAE58
		internal DataColumnChangeEventArgs(DataRow row)
		{
			this._row = row;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x001EBA78 File Offset: 0x001EAE78
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this._row = row;
			this._column = column;
			this._proposedValue = value;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x001EBAA8 File Offset: 0x001EAEA8
		public DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x001EBAC8 File Offset: 0x001EAEC8
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x001EBAE8 File Offset: 0x001EAEE8
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x001EBB08 File Offset: 0x001EAF08
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

		// Token: 0x06000536 RID: 1334 RVA: 0x001EBB28 File Offset: 0x001EAF28
		internal void InitializeColumnChangeEvent(DataColumn column, object value)
		{
			this._column = column;
			this._proposedValue = value;
		}

		// Token: 0x04000709 RID: 1801
		private readonly DataRow _row;

		// Token: 0x0400070A RID: 1802
		private DataColumn _column;

		// Token: 0x0400070B RID: 1803
		private object _proposedValue;
	}
}
