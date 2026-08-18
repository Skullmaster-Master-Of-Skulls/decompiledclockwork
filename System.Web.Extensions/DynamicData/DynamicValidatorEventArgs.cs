using System;

namespace System.Web.DynamicData
{
	// Token: 0x02000107 RID: 263
	public class DynamicValidatorEventArgs : EventArgs
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x00031071 File Offset: 0x0002F271
		public DynamicValidatorEventArgs(Exception exception, DynamicDataSourceOperation operation)
		{
			this._exception = exception;
			this._operation = operation;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00031087 File Offset: 0x0002F287
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0003108F File Offset: 0x0002F28F
		public DynamicDataSourceOperation Operation
		{
			get
			{
				return this._operation;
			}
		}

		// Token: 0x040003E4 RID: 996
		private DynamicDataSourceOperation _operation;

		// Token: 0x040003E5 RID: 997
		private Exception _exception;
	}
}
