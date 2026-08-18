using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000255 RID: 597
	internal struct IDBInfoWrapper : IDisposable
	{
		// Token: 0x0600208C RID: 8332 RVA: 0x00281048 File Offset: 0x00280448
		internal IDBInfoWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IDBInfo);
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00281068 File Offset: 0x00280468
		internal UnsafeNativeMethods.IDBInfo Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00281088 File Offset: 0x00280488
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

		// Token: 0x04001529 RID: 5417
		private object _unknown;

		// Token: 0x0400152A RID: 5418
		private UnsafeNativeMethods.IDBInfo _value;
	}
}
