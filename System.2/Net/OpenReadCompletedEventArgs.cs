using System;
using System.ComponentModel;
using System.IO;

namespace System.Net
{
	// Token: 0x0200016A RID: 362
	public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x000499B0 File Offset: 0x00047BB0
		internal OpenReadCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000499C3 File Offset: 0x00047BC3
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0400121E RID: 4638
		private Stream m_Result;
	}
}
