using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000259 RID: 601
	internal struct ITransactionJoinWrapper : IDisposable
	{
		// Token: 0x06002098 RID: 8344 RVA: 0x00281208 File Offset: 0x00280608
		internal ITransactionJoinWrapper(object unknown)
		{
			this._unknown = unknown;
			this._value = (unknown as NativeMethods.ITransactionJoin);
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00281228 File Offset: 0x00280628
		internal NativeMethods.ITransactionJoin Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00281248 File Offset: 0x00280648
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

		// Token: 0x04001531 RID: 5425
		private object _unknown;

		// Token: 0x04001532 RID: 5426
		private NativeMethods.ITransactionJoin _value;
	}
}
