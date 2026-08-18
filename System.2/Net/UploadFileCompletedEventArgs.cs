using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000176 RID: 374
	public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00049A76 File Offset: 0x00047C76
		internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00049A89 File Offset: 0x00047C89
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001224 RID: 4644
		private byte[] m_Result;
	}
}
