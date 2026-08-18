using System;

namespace System.Web.UI
{
	// Token: 0x02000043 RID: 67
	public class AsyncPostBackErrorEventArgs : EventArgs
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x000110AF File Offset: 0x0000F2AF
		public AsyncPostBackErrorEventArgs(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this._exception = exception;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000110CC File Offset: 0x0000F2CC
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x04000102 RID: 258
		private readonly Exception _exception;
	}
}
