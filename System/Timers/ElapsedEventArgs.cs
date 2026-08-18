using System;

namespace System.Timers
{
	// Token: 0x02000735 RID: 1845
	public class ElapsedEventArgs : EventArgs
	{
		// Token: 0x06003854 RID: 14420 RVA: 0x000EDAF0 File Offset: 0x000ECAF0
		internal ElapsedEventArgs(int low, int high)
		{
			long fileTime = (long)high << 32 | ((long)low & (long)((ulong)-1));
			this.signalTime = DateTime.FromFileTime(fileTime);
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000EDB1B File Offset: 0x000ECB1B
		public DateTime SignalTime
		{
			get
			{
				return this.signalTime;
			}
		}

		// Token: 0x04003242 RID: 12866
		private DateTime signalTime;
	}
}
