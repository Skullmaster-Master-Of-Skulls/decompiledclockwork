using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x02000493 RID: 1171
	public class UploadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x0008D2AA File Offset: 0x0008C2AA
		internal UploadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x0008D2BD File Offset: 0x0008C2BD
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04002469 RID: 9321
		private string m_Result;
	}
}
