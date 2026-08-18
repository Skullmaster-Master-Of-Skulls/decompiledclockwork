using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000178 RID: 376
	public class UploadValuesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x00049A97 File Offset: 0x00047C97
		internal UploadValuesCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00049AAA File Offset: 0x00047CAA
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001225 RID: 4645
		private byte[] m_Result;
	}
}
