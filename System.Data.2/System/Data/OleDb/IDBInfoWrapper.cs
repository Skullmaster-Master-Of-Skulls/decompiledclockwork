using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000278 RID: 632
	internal struct IDBInfoWrapper : IDisposable
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x00105494 File Offset: 0x00104894
		internal IDBInfoWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IDBInfo);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x001054B4 File Offset: 0x001048B4
		internal UnsafeNativeMethods.IDBInfo Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x001054C8 File Offset: 0x001048C8
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

		// Token: 0x0400183A RID: 6202
		private object _unknown;

		// Token: 0x0400183B RID: 6203
		private UnsafeNativeMethods.IDBInfo _value;
	}
}
