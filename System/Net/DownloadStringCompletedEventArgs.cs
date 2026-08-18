using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200048F RID: 1167
	public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023E5 RID: 9189 RVA: 0x0008D268 File Offset: 0x0008C268
		internal DownloadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x0008D27B File Offset: 0x0008C27B
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04002467 RID: 9319
		private string m_Result;
	}
}
