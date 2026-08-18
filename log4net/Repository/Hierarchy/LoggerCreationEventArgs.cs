using System;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000C8 RID: 200
	public class LoggerCreationEventArgs : EventArgs
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x00011E92 File Offset: 0x00010092
		public LoggerCreationEventArgs(Logger log)
		{
			this.m_log = log;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00011EA1 File Offset: 0x000100A1
		public Logger Logger
		{
			get
			{
				return this.m_log;
			}
		}

		// Token: 0x04000253 RID: 595
		private Logger m_log;
	}
}
