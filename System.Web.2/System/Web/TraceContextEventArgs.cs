using System;
using System.Collections;

namespace System.Web
{
	// Token: 0x02000101 RID: 257
	public sealed class TraceContextEventArgs : EventArgs
	{
		// Token: 0x06000F70 RID: 3952 RVA: 0x0002D677 File Offset: 0x0002B877
		public TraceContextEventArgs(ICollection records)
		{
			this._records = records;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002D686 File Offset: 0x0002B886
		public ICollection TraceRecords
		{
			get
			{
				return this._records;
			}
		}

		// Token: 0x040005ED RID: 1517
		private ICollection _records;
	}
}
