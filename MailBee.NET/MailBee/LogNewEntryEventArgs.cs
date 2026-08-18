using System;
using a;

namespace MailBee
{
	// Token: 0x02000035 RID: 53
	public class LogNewEntryEventArgs : CommonEventArgs
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00007DBE File Offset: 0x00006DBE
		internal LogNewEntryEventArgs(LogEntry A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007DCE File Offset: 0x00006DCE
		public LogEntry NewEntry
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x04000151 RID: 337
		private LogEntry a;
	}
}
