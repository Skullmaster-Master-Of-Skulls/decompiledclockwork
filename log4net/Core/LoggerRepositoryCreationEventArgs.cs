using System;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02000062 RID: 98
	public class LoggerRepositoryCreationEventArgs : EventArgs
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000B38C File Offset: 0x0000958C
		public LoggerRepositoryCreationEventArgs(ILoggerRepository repository)
		{
			this.m_repository = repository;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B39B File Offset: 0x0000959B
		public ILoggerRepository LoggerRepository
		{
			get
			{
				return this.m_repository;
			}
		}

		// Token: 0x0400016E RID: 366
		private ILoggerRepository m_repository;
	}
}
