using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000279 RID: 633
	internal struct IDBPropertiesWrapper : IDisposable
	{
		// Token: 0x06002696 RID: 9878 RVA: 0x001054F4 File Offset: 0x001048F4
		internal IDBPropertiesWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IDBProperties);
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x00105514 File Offset: 0x00104914
		internal UnsafeNativeMethods.IDBProperties Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00105528 File Offset: 0x00104928
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

		// Token: 0x0400183C RID: 6204
		private object _unknown;

		// Token: 0x0400183D RID: 6205
		private UnsafeNativeMethods.IDBProperties _value;
	}
}
