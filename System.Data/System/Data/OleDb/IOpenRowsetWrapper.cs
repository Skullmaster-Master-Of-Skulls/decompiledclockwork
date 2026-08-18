using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000258 RID: 600
	internal struct IOpenRowsetWrapper : IDisposable
	{
		// Token: 0x06002095 RID: 8341 RVA: 0x00281198 File Offset: 0x00280598
		internal IOpenRowsetWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IOpenRowset);
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x002811B8 File Offset: 0x002805B8
		internal UnsafeNativeMethods.IOpenRowset Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x002811D8 File Offset: 0x002805D8
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

		// Token: 0x0400152F RID: 5423
		private object _unknown;

		// Token: 0x04001530 RID: 5424
		private UnsafeNativeMethods.IOpenRowset _value;
	}
}
