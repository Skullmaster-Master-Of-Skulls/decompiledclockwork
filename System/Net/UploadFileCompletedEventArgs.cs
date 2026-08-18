using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000497 RID: 1175
	public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x0008D2EC File Offset: 0x0008C2EC
		internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x0008D2FF File Offset: 0x0008C2FF
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400246B RID: 9323
		private byte[] m_Result;
	}
}
