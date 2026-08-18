using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000495 RID: 1173
	public class UploadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023F7 RID: 9207 RVA: 0x0008D2CB File Offset: 0x0008C2CB
		internal UploadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x0008D2DE File Offset: 0x0008C2DE
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400246A RID: 9322
		private byte[] m_Result;
	}
}
