using System;

namespace log4net.Util
{
	// Token: 0x02000106 RID: 262
	public class LogReceivedEventArgs : EventArgs
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00017AB1 File Offset: 0x00015CB1
		public LogReceivedEventArgs(LogLog loglog)
		{
			this.loglog = loglog;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00017AC0 File Offset: 0x00015CC0
		public LogLog LogLog
		{
			get
			{
				return this.loglog;
			}
		}

		// Token: 0x040002D0 RID: 720
		private readonly LogLog loglog;
	}
}
