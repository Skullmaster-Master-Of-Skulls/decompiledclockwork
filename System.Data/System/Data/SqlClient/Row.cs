using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002B1 RID: 689
	internal sealed class Row
	{
		// Token: 0x060022F8 RID: 8952 RVA: 0x0028E0E8 File Offset: 0x0028D4E8
		internal Row(int rowCount)
		{
			this._dataFields = new object[rowCount];
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0028E108 File Offset: 0x0028D508
		internal object[] DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x1700052E RID: 1326
		internal object this[int index]
		{
			get
			{
				return this._dataFields[index];
			}
		}

		// Token: 0x040016C4 RID: 5828
		private object[] _dataFields;
	}
}
