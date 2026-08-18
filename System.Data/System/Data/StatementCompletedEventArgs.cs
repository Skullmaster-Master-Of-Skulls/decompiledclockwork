using System;

namespace System.Data
{
	// Token: 0x020000D3 RID: 211
	public sealed class StatementCompletedEventArgs : EventArgs
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x002129E8 File Offset: 0x00211DE8
		public StatementCompletedEventArgs(int recordCount)
		{
			this._recordCount = recordCount;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00212A08 File Offset: 0x00211E08
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x040008F0 RID: 2288
		private readonly int _recordCount;
	}
}
