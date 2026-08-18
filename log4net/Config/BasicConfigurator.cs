using System;
using System.Collections;
using System.Reflection;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x0200004A RID: 74
	public sealed class BasicConfigurator
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00008DC0 File Offset: 0x00006FC0
		private BasicConfigurator()
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public static ICollection Configure()
		{
			return BasicConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008DDC File Offset: 0x00006FDC
		public static ICollection Configure(params IAppender[] appenders)
		{
			ArrayList arrayList = new ArrayList();
			ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				BasicConfigurator.InternalConfigure(repository, appenders);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008E2C File Offset: 0x0000702C
		public static ICollection Configure(IAppender appender)
		{
			return BasicConfigurator.Configure(new IAppender[]
			{
				appender
			});
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008E4C File Offset: 0x0000704C
		public static ICollection Configure(ILoggerRepository repository)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				PatternLayout patternLayout = new PatternLayout();
				patternLayout.ConversionPattern = "%timestamp [%thread] %level %logger %ndc - %message%newline";
				patternLayout.ActivateOptions();
				ConsoleAppender consoleAppender = new ConsoleAppender();
				consoleAppender.Layout = patternLayout;
				consoleAppender.ActivateOptions();
				BasicConfigurator.InternalConfigure(repository, new IAppender[]
				{
					consoleAppender
				});
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008ECC File Offset: 0x000070CC
		public static ICollection Configure(ILoggerRepository repository, IAppender appender)
		{
			return BasicConfigurator.Configure(repository, new IAppender[]
			{
				appender
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008EEC File Offset: 0x000070EC
		public static ICollection Configure(ILoggerRepository repository, params IAppender[] appenders)
		{
			ArrayList arrayList = new ArrayList();
			using (new LogLog.LogReceivedAdapter(arrayList))
			{
				BasicConfigurator.InternalConfigure(repository, appenders);
			}
			repository.ConfigurationMessages = arrayList;
			return arrayList;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00008F34 File Offset: 0x00007134
		private static void InternalConfigure(ILoggerRepository repository, params IAppender[] appenders)
		{
			IBasicRepositoryConfigurator basicRepositoryConfigurator = repository as IBasicRepositoryConfigurator;
			if (basicRepositoryConfigurator != null)
			{
				basicRepositoryConfigurator.Configure(appenders);
				return;
			}
			LogLog.Warn(BasicConfigurator.declaringType, "BasicConfigurator: Repository [" + repository + "] does not support the BasicConfigurator");
		}

		// Token: 0x04000145 RID: 325
		private static readonly Type declaringType = typeof(BasicConfigurator);
	}
}
