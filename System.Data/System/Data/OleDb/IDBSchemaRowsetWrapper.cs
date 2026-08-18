using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000257 RID: 599
	internal struct IDBSchemaRowsetWrapper : IDisposable
	{
		// Token: 0x06002092 RID: 8338 RVA: 0x00281128 File Offset: 0x00280528
		internal IDBSchemaRowsetWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IDBSchemaRowset);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x00281148 File Offset: 0x00280548
		internal UnsafeNativeMethods.IDBSchemaRowset Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00281168 File Offset: 0x00280568
		public void Dispose()
		{
			object unknown = this._unknown;
			this._unknown = null;
			this._value = null;
			if (unknown != null)
			{
				Marshal.ReleaseComObject(unknown);
			}
		}

		// Token: 0x0400152D RID: 5421
		private object _unknown;

		// Token: 0x0400152E RID: 5422
		private UnsafeNativeMethods.IDBSchemaRowset _value;
	}
}
