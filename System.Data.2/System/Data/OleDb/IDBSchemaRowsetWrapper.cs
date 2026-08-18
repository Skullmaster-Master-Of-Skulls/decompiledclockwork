using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200027A RID: 634
	internal struct IDBSchemaRowsetWrapper : IDisposable
	{
		// Token: 0x06002699 RID: 9881 RVA: 0x00105554 File Offset: 0x00104954
		internal IDBSchemaRowsetWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IDBSchemaRowset);
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x00105574 File Offset: 0x00104974
		internal UnsafeNativeMethods.IDBSchemaRowset Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x00105588 File Offset: 0x00104988
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

		// Token: 0x0400183E RID: 6206
		private object _unknown;

		// Token: 0x0400183F RID: 6207
		private UnsafeNativeMethods.IDBSchemaRowset _value;
	}
}
