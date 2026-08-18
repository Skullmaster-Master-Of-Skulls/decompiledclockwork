using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000256 RID: 598
	internal struct IDBPropertiesWrapper : IDisposable
	{
		// Token: 0x0600208F RID: 8335 RVA: 0x002810B8 File Offset: 0x002804B8
		internal IDBPropertiesWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IDBProperties);
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x002810D8 File Offset: 0x002804D8
		internal UnsafeNativeMethods.IDBProperties Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x002810F8 File Offset: 0x002804F8
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

		// Token: 0x0400152B RID: 5419
		private object _unknown;

		// Token: 0x0400152C RID: 5420
		private UnsafeNativeMethods.IDBProperties _value;
	}
}
