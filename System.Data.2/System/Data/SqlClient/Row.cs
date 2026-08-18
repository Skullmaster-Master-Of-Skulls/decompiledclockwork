using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001A6 RID: 422
	internal sealed class Row
	{
		// Token: 0x0600189F RID: 6303 RVA: 0x000ADB10 File Offset: 0x000ACF10
		internal Row(int rowCount)
		{
			this._dataFields = new object[rowCount];
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x000ADB30 File Offset: 0x000ACF30
		internal object[] DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x17000375 RID: 885
		internal object this[int index]
		{
			get
			{
				return this._dataFields[index];
			}
		}

		// Token: 0x04000EB2 RID: 3762
		private object[] _dataFields;
	}
}
