using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200027B RID: 635
	internal struct IOpenRowsetWrapper : IDisposable
	{
		// Token: 0x0600269C RID: 9884 RVA: 0x001055B4 File Offset: 0x001049B4
		internal IOpenRowsetWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as UnsafeNativeMethods.IOpenRowset);
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x001055D4 File Offset: 0x001049D4
		internal UnsafeNativeMethods.IOpenRowset Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x001055E8 File Offset: 0x001049E8
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

		// Token: 0x04001840 RID: 6208
		private object _unknown;

		// Token: 0x04001841 RID: 6209
		private UnsafeNativeMethods.IOpenRowset _value;
	}
}
