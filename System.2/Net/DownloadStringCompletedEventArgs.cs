using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200016E RID: 366
	public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x000499F2 File Offset: 0x00047BF2
		internal DownloadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00049A05 File Offset: 0x00047C05
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04001220 RID: 4640
		private string m_Result;
	}
}
