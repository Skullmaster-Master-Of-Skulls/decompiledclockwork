using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	// Token: 0x0200048D RID: 1165
	public class OpenWriteCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060023DF RID: 9183 RVA: 0x0008D247 File Offset: 0x0008C247
		internal OpenWriteCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0008D25A File Offset: 0x0008C25A
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x04002466 RID: 9318
		private Stream m_Result;
	}
}
