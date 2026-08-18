using System;
using System.Collections;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02000079 RID: 121
	public class WrapperMap
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0000E168 File Offset: 0x0000C368
		public WrapperMap(WrapperCreationHandler createWrapperHandler)
		{
			this.m_createWrapperHandler = createWrapperHandler;
			this.m_shutdownHandler = new LoggerRepositoryShutdownEventHandler(this.ILoggerRepository_Shutdown);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000E194 File Offset: 0x0000C394
		public virtual ILoggerWrapper GetWrapper(ILogger logger)
		{
			if (logger == null)
			{
				return null;
			}
			ILoggerWrapper result;
			lock (this)
			{
				Hashtable hashtable = (Hashtable)this.m_repositories[logger.Repository];
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					this.m_repositories[logger.Repository] = hashtable;
					logger.Repository.ShutdownEvent += this.m_shutdownHandler;
				}
				ILoggerWrapper loggerWrapper = hashtable[logger] as ILoggerWrapper;
				if (loggerWrapper == null)
				{
					loggerWrapper = this.CreateNewWrapperObject(logger);
					hashtable[logger] = loggerWrapper;
				}
				result = loggerWrapper;
			}
			return result;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000E238 File Offset: 0x0000C438
		protected Hashtable Repositories
		{
			get
			{
				return this.m_repositories;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000E240 File Offset: 0x0000C440
		protected virtual ILoggerWrapper CreateNewWrapperObject(ILogger logger)
		{
			if (this.m_createWrapperHandler != null)
			{
				return this.m_createWrapperHandler(logger);
			}
			return null;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000E258 File Offset: 0x0000C458
		protected virtual void RepositoryShutdown(ILoggerRepository repository)
		{
			lock (this)
			{
				this.m_repositories.Remove(repository);
				repository.ShutdownEvent -= this.m_shutdownHandler;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		private void ILoggerRepository_Shutdown(object sender, EventArgs e)
		{
			ILoggerRepository loggerRepository = sender as ILoggerRepository;
			if (loggerRepository != null)
			{
				this.RepositoryShutdown(loggerRepository);
			}
		}

		// Token: 0x040001D7 RID: 471
		private readonly Hashtable m_repositories = new Hashtable();

		// Token: 0x040001D8 RID: 472
		private readonly WrapperCreationHandler m_createWrapperHandler;

		// Token: 0x040001D9 RID: 473
		private readonly LoggerRepositoryShutdownEventHandler m_shutdownHandler;
	}
}
