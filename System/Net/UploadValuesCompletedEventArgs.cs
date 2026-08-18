using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000499 RID: 1177
	public class UploadValuesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002403 RID: 9219 RVA: 0x0008D30D File Offset: 0x0008C30D
		internal UploadValuesCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0008D320 File Offset: 0x0008C320
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400246C RID: 9324
		private byte[] m_Result;
	}
}
