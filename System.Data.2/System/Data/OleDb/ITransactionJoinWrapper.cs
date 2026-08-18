using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200027C RID: 636
	internal struct ITransactionJoinWrapper : IDisposable
	{
		// Token: 0x0600269F RID: 9887 RVA: 0x00105614 File Offset: 0x00104A14
		internal ITransactionJoinWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as NativeMethods.ITransactionJoin);
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x00105634 File Offset: 0x00104A34
		internal NativeMethods.ITransactionJoin Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x00105648 File Offset: 0x00104A48
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

		// Token: 0x04001842 RID: 6210
		private object _unknown;

		// Token: 0x04001843 RID: 6211
		private NativeMethods.ITransactionJoin _value;
	}
}
