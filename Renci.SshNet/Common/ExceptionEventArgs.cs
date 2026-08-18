using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F5 RID: 245
	public class ExceptionEventArgs : EventArgs
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00024773 File Offset: 0x00022973
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0002477B File Offset: 0x0002297B
		public Exception Exception { get; private set; }

		// Token: 0x06000AAE RID: 2734 RVA: 0x00024784 File Offset: 0x00022984
		public ExceptionEventArgs(Exception exception)
		{
			this.Exception = exception;
		}
	}
}
