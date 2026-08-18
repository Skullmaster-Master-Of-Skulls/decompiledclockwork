using System;
using NLog.Targets;

namespace NLog.Config
{
	// Token: 0x02000058 RID: 88
	public static class SimpleConfigurator
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00006A71 File Offset: 0x00004C71
		public static void ConfigureForConsoleLogging()
		{
			SimpleConfigurator.ConfigureForConsoleLogging(LogLevel.Info);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006A80 File Offset: 0x00004C80
		public static void ConfigureForConsoleLogging(LogLevel minLevel)
		{
			ConsoleTarget target = new ConsoleTarget();
			LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
			LoggingRule item = new LoggingRule("*", minLevel, target);
			loggingConfiguration.LoggingRules.Add(item);
			LogManager.Configuration = loggingConfiguration;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public static void ConfigureForTargetLogging(Target target)
		{
			SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Info);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public static void ConfigureForTargetLogging(Target target, LogLevel minLevel)
		{
			LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
			LoggingRule item = new LoggingRule("*", minLevel, target);
			loggingConfiguration.LoggingRules.Add(item);
			LogManager.Configuration = loggingConfiguration;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006AFA File Offset: 0x00004CFA
		public static void ConfigureForFileLogging(string fileName)
		{
			SimpleConfigurator.ConfigureForFileLogging(fileName, LogLevel.Info);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006B08 File Offset: 0x00004D08
		public static void ConfigureForFileLogging(string fileName, LogLevel minLevel)
		{
			SimpleConfigurator.ConfigureForTargetLogging(new FileTarget
			{
				FileName = fileName
			}, minLevel);
		}
	}
}
