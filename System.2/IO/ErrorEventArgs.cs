using System;

namespace System.IO
{
	// Token: 0x020003FB RID: 1019
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x0600265E RID: 9822 RVA: 0x000B0F3A File Offset: 0x000AF13A
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000B0F49 File Offset: 0x000AF149
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x040020BB RID: 8379
		private Exception exception;
	}
}
