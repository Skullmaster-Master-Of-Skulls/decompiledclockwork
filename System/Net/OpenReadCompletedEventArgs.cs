using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	// Token: 0x0200048B RID: 1163
	public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x0008D226 File Offset: 0x0008C226
		internal OpenReadCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x0008D239 File Offset: 0x0008C239
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04002465 RID: 9317
		private Stream m_Result;
	}
}
