using System;

namespace System.Threading
{
	// Token: 0x020003D7 RID: 983
	public class ThreadExceptionEventArgs : EventArgs
	{
		// Token: 0x060025E6 RID: 9702 RVA: 0x000B0242 File Offset: 0x000AE442
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000B0251 File Offset: 0x000AE451
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04002075 RID: 8309
		private Exception exception;
	}
}
