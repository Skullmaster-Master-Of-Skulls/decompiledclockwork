using System;

namespace System.IO
{
	// Token: 0x02000727 RID: 1831
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x060037F3 RID: 14323 RVA: 0x000EC6FE File Offset: 0x000EB6FE
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000EC70D File Offset: 0x000EB70D
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x04003205 RID: 12805
		private Exception exception;
	}
}
