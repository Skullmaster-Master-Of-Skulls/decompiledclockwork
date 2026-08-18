using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000172 RID: 370
	public class UploadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DF1 RID: 3569 RVA: 0x00049A34 File Offset: 0x00047C34
		internal UploadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00049A47 File Offset: 0x00047C47
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001222 RID: 4642
		private string m_Result;
	}
}
