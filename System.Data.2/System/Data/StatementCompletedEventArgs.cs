using System;

namespace System.Data
{
	// Token: 0x0200011E RID: 286
	public sealed class StatementCompletedEventArgs : EventArgs
	{
		// Token: 0x06001153 RID: 4435 RVA: 0x00085884 File Offset: 0x00084C84
		public StatementCompletedEventArgs(int recordCount)
		{
			this._recordCount = recordCount;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000858A0 File Offset: 0x00084CA0
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x040005CB RID: 1483
		private readonly int _recordCount;
	}
}
