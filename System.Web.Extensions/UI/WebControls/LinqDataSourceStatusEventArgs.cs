using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A3 RID: 163
	public class LinqDataSourceStatusEventArgs : EventArgs
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x0001D015 File Offset: 0x0001B215
		public LinqDataSourceStatusEventArgs(object result)
		{
			this._result = result;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001D02B File Offset: 0x0001B22B
		public LinqDataSourceStatusEventArgs(object result, int totalRowCount)
		{
			this._result = result;
			this._totalRowCount = totalRowCount;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001D048 File Offset: 0x0001B248
		public LinqDataSourceStatusEventArgs(Exception exception)
		{
			this._exception = exception;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001D05E File Offset: 0x0001B25E
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001D066 File Offset: 0x0001B266
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001D06E File Offset: 0x0001B26E
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001D077 File Offset: 0x0001B277
		public object Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001D07F File Offset: 0x0001B27F
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
		}

		// Token: 0x0400026A RID: 618
		private Exception _exception;

		// Token: 0x0400026B RID: 619
		private bool _exceptionHandled;

		// Token: 0x0400026C RID: 620
		private object _result;

		// Token: 0x0400026D RID: 621
		private int _totalRowCount = -1;
	}
}
