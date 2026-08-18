using System;
using System.Reflection;
using log4net.Appender;
using log4net.Core;
using log4net.Repository;

namespace log4net
{
	// Token: 0x02000124 RID: 292
	public sealed class LogManager
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x0001A085 File Offset: 0x00018285
		private LogManager()
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001A08D File Offset: 0x0001828D
		public static ILog Exists(string name)
		{
			return LogManager.Exists(Assembly.GetCallingAssembly(), name);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001A09A File Offset: 0x0001829A
		public static ILog[] GetCurrentLoggers()
		{
			return LogManager.GetCurrentLoggers(Assembly.GetCallingAssembly());
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001A0A6 File Offset: 0x000182A6
		public static ILog GetLogger(string name)
		{
			return LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001A0B3 File Offset: 0x000182B3
		public static ILog Exists(string repository, string name)
		{
			return LogManager.WrapLogger(LoggerManager.Exists(repository, name));
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001A0C1 File Offset: 0x000182C1
		public static ILog Exists(Assembly repositoryAssembly, string name)
		{
			return LogManager.WrapLogger(LoggerManager.Exists(repositoryAssembly, name));
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001A0CF File Offset: 0x000182CF
		public static ILog[] GetCurrentLoggers(string repository)
		{
			return LogManager.WrapLoggers(LoggerManager.GetCurrentLoggers(repository));
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001A0DC File Offset: 0x000182DC
		public static ILog[] GetCurrentLoggers(Assembly repositoryAssembly)
		{
			return LogManager.WrapLoggers(LoggerManager.GetCurrentLoggers(repositoryAssembly));
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001A0E9 File Offset: 0x000182E9
		public static ILog GetLogger(string repository, string name)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repository, name));
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001A0F7 File Offset: 0x000182F7
		public static ILog GetLogger(Assembly repositoryAssembly, string name)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repositoryAssembly, name));
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001A105 File Offset: 0x00018305
		public static ILog GetLogger(Type type)
		{
			return LogManager.GetLogger(Assembly.GetCallingAssembly(), type.FullName);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001A117 File Offset: 0x00018317
		public static ILog GetLogger(string repository, Type type)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repository, type));
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001A125 File Offset: 0x00018325
		public static ILog GetLogger(Assembly repositoryAssembly, Type type)
		{
			return LogManager.WrapLogger(LoggerManager.GetLogger(repositoryAssembly, type));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001A133 File Offset: 0x00018333
		public static void Shutdown()
		{
			LoggerManager.Shutdown();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001A13A File Offset: 0x0001833A
		public static void ShutdownRepository()
		{
			LogManager.ShutdownRepository(Assembly.GetCallingAssembly());
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001A146 File Offset: 0x00018346
		public static void ShutdownRepository(string repository)
		{
			LoggerManager.ShutdownRepository(repository);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001A14E File Offset: 0x0001834E
		public static void ShutdownRepository(Assembly repositoryAssembly)
		{
			LoggerManager.ShutdownRepository(repositoryAssembly);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001A156 File Offset: 0x00018356
		public static void ResetConfiguration()
		{
			LogManager.ResetConfiguration(Assembly.GetCallingAssembly());
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001A162 File Offset: 0x00018362
		public static void ResetConfiguration(string repository)
		{
			LoggerManager.ResetConfiguration(repository);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001A16A File Offset: 0x0001836A
		public static void ResetConfiguration(Assembly repositoryAssembly)
		{
			LoggerManager.ResetConfiguration(repositoryAssembly);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001A172 File Offset: 0x00018372
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository()
		{
			return LogManager.GetRepository(Assembly.GetCallingAssembly());
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001A17E File Offset: 0x0001837E
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(string repository)
		{
			return LogManager.GetRepository(repository);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001A186 File Offset: 0x00018386
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(Assembly repositoryAssembly)
		{
			return LogManager.GetRepository(repositoryAssembly);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001A18E File Offset: 0x0001838E
		public static ILoggerRepository GetRepository()
		{
			return LogManager.GetRepository(Assembly.GetCallingAssembly());
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001A19A File Offset: 0x0001839A
		public static ILoggerRepository GetRepository(string repository)
		{
			return LoggerManager.GetRepository(repository);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001A1A2 File Offset: 0x000183A2
		public static ILoggerRepository GetRepository(Assembly repositoryAssembly)
		{
			return LoggerManager.GetRepository(repositoryAssembly);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001A1AA File Offset: 0x000183AA
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(Type repositoryType)
		{
			return LogManager.CreateRepository(Assembly.GetCallingAssembly(), repositoryType);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001A1B7 File Offset: 0x000183B7
		public static ILoggerRepository CreateRepository(Type repositoryType)
		{
			return LogManager.CreateRepository(Assembly.GetCallingAssembly(), repositoryType);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001A1C4 File Offset: 0x000183C4
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository)
		{
			return LoggerManager.CreateRepository(repository);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001A1CC File Offset: 0x000183CC
		public static ILoggerRepository CreateRepository(string repository)
		{
			return LoggerManager.CreateRepository(repository);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001A1D4 File Offset: 0x000183D4
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repository, repositoryType);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001A1DD File Offset: 0x000183DD
		public static ILoggerRepository CreateRepository(string repository, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repository, repositoryType);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001A1E6 File Offset: 0x000183E6
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(Assembly repositoryAssembly, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001A1EF File Offset: 0x000183EF
		public static ILoggerRepository CreateRepository(Assembly repositoryAssembly, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001A1F8 File Offset: 0x000183F8
		public static ILoggerRepository[] GetAllRepositories()
		{
			return LoggerManager.GetAllRepositories();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001A200 File Offset: 0x00018400
		public static bool Flush(int millisecondsTimeout)
		{
			IFlushable flushable = LoggerManager.GetRepository(Assembly.GetCallingAssembly()) as IFlushable;
			return flushable != null && flushable.Flush(millisecondsTimeout);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001A229 File Offset: 0x00018429
		private static ILog WrapLogger(ILogger logger)
		{
			return (ILog)LogManager.s_wrapperMap.GetWrapper(logger);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001A23C File Offset: 0x0001843C
		private static ILog[] WrapLoggers(ILogger[] loggers)
		{
			ILog[] array = new ILog[loggers.Length];
			for (int i = 0; i < loggers.Length; i++)
			{
				array[i] = LogManager.WrapLogger(loggers[i]);
			}
			return array;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001A26C File Offset: 0x0001846C
		private static ILoggerWrapper WrapperCreationHandler(ILogger logger)
		{
			return new LogImpl(logger);
		}

		// Token: 0x0400031A RID: 794
		private static readonly WrapperMap s_wrapperMap = new WrapperMap(new WrapperCreationHandler(LogManager.WrapperCreationHandler));
	}
}
