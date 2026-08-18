using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	// Token: 0x0200016C RID: 364
	public class OpenWriteCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x000499D1 File Offset: 0x00047BD1
		internal OpenWriteCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x000499E4 File Offset: 0x00047BE4
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400121F RID: 4639
		private Stream m_Result;
	}
}
