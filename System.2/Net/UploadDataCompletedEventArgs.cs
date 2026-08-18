using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000174 RID: 372
	public class UploadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00049A55 File Offset: 0x00047C55
		internal UploadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00049A68 File Offset: 0x00047C68
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001223 RID: 4643
		private byte[] m_Result;
	}
}
