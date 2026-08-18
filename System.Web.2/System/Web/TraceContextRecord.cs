using System;

namespace System.Web
{
	// Token: 0x02000103 RID: 259
	public sealed class TraceContextRecord
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0002D68E File Offset: 0x0002B88E
		public TraceContextRecord(string category, string msg, bool isWarning, Exception errorInfo)
		{
			this._category = category;
			this._message = msg;
			this._isWarning = isWarning;
			this._errorInfo = errorInfo;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002D6B3 File Offset: 0x0002B8B3
		public string Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0002D6BB File Offset: 0x0002B8BB
		public Exception ErrorInfo
		{
			get
			{
				return this._errorInfo;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002D6C3 File Offset: 0x0002B8C3
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002D6CB File Offset: 0x0002B8CB
		public bool IsWarning
		{
			get
			{
				return this._isWarning;
			}
		}

		// Token: 0x040005EE RID: 1518
		private string _category;

		// Token: 0x040005EF RID: 1519
		private string _message;

		// Token: 0x040005F0 RID: 1520
		private Exception _errorInfo;

		// Token: 0x040005F1 RID: 1521
		private bool _isWarning;
	}
}
