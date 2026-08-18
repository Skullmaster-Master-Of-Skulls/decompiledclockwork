using System;

namespace log4net.Core
{
	// Token: 0x0200006D RID: 109
	public abstract class LoggerWrapperImpl : ILoggerWrapper
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000C9DB File Offset: 0x0000ABDB
		protected LoggerWrapperImpl(ILogger logger)
		{
			this.m_logger = logger;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		public virtual ILogger Logger
		{
			get
			{
				return this.m_logger;
			}
		}

		// Token: 0x0400019A RID: 410
		private readonly ILogger m_logger;
	}
}
