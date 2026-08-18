using System;
using System.Reflection;
using System.Security;
using System.Text;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x0200006C RID: 108
	public sealed class LoggerManager
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000C464 File Offset: 0x0000A664
		private LoggerManager()
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000C46C File Offset: 0x0000A66C
		static LoggerManager()
		{
			try
			{
				LoggerManager.RegisterAppDomainEvents();
			}
			catch (SecurityException)
			{
				LogLog.Debug(LoggerManager.declaringType, "Security Exception (ControlAppDomain LinkDemand) while trying to register Shutdown handler with the AppDomain. LoggerManager.Shutdown() will not be called automatically when the AppDomain exits. It must be called programmatically.");
			}
			LogLog.Debug(LoggerManager.declaringType, LoggerManager.GetVersionInfo());
			string appSetting = SystemInfo.GetAppSetting("log4net.RepositorySelector");
			if (appSetting != null && appSetting.Length > 0)
			{
				Type type = null;
				try
				{
					type = SystemInfo.GetTypeFromString(appSetting, false, true);
				}
				catch (Exception exception)
				{
					LogLog.Error(LoggerManager.declaringType, "Exception while resolving RepositorySelector Type [" + appSetting + "]", exception);
				}
				if (type != null)
				{
					object obj = null;
					try
					{
						obj = Activator.CreateInstance(type);
					}
					catch (Exception exception2)
					{
						LogLog.Error(LoggerManager.declaringType, "Exception while creating RepositorySelector [" + type.FullName + "]", exception2);
					}
					if (obj != null && obj is IRepositorySelector)
					{
						LoggerManager.s_repositorySelector = (IRepositorySelector)obj;
					}
					else
					{
						LogLog.Error(LoggerManager.declaringType, "RepositorySelector Type [" + type.FullName + "] is not an IRepositorySelector");
					}
				}
			}
			if (LoggerManager.s_repositorySelector == null)
			{
				LoggerManager.s_repositorySelector = new DefaultRepositorySelector(typeof(Hierarchy));
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		private static void RegisterAppDomainEvents()
		{
			AppDomain.CurrentDomain.ProcessExit += LoggerManager.OnProcessExit;
			AppDomain.CurrentDomain.DomainUnload += LoggerManager.OnDomainUnload;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000C5DA File Offset: 0x0000A7DA
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(string repository)
		{
			return LoggerManager.GetRepository(repository);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		[Obsolete("Use GetRepository instead of GetLoggerRepository")]
		public static ILoggerRepository GetLoggerRepository(Assembly repositoryAssembly)
		{
			return LoggerManager.GetRepository(repositoryAssembly);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C5EA File Offset: 0x0000A7EA
		public static ILoggerRepository GetRepository(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000C605 File Offset: 0x0000A805
		public static ILoggerRepository GetRepository(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000C626 File Offset: 0x0000A826
		public static ILogger Exists(string repository, string name)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).Exists(name);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000C655 File Offset: 0x0000A855
		public static ILogger Exists(Assembly repositoryAssembly, string name)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).Exists(name);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000C68A File Offset: 0x0000A88A
		public static ILogger[] GetCurrentLoggers(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).GetCurrentLoggers();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000C6AA File Offset: 0x0000A8AA
		public static ILogger[] GetCurrentLoggers(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).GetCurrentLoggers();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public static ILogger GetLogger(string repository, string name)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).GetLogger(name);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		public static ILogger GetLogger(Assembly repositoryAssembly, string name)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).GetLogger(name);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000C734 File Offset: 0x0000A934
		public static ILogger GetLogger(string repository, Type type)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return LoggerManager.RepositorySelector.GetRepository(repository).GetLogger(type.FullName);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000C76E File Offset: 0x0000A96E
		public static ILogger GetLogger(Assembly repositoryAssembly, Type type)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).GetLogger(type.FullName);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public static void Shutdown()
		{
			foreach (ILoggerRepository loggerRepository in LoggerManager.GetAllRepositories())
			{
				loggerRepository.Shutdown();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		public static void ShutdownRepository(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			LoggerManager.RepositorySelector.GetRepository(repository).Shutdown();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C7FB File Offset: 0x0000A9FB
		public static void ShutdownRepository(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).Shutdown();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C821 File Offset: 0x0000AA21
		public static void ResetConfiguration(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			LoggerManager.RepositorySelector.GetRepository(repository).ResetConfiguration();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C841 File Offset: 0x0000AA41
		public static void ResetConfiguration(Assembly repositoryAssembly)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			LoggerManager.RepositorySelector.GetRepository(repositoryAssembly).ResetConfiguration();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C867 File Offset: 0x0000AA67
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository)
		{
			return LoggerManager.CreateRepository(repository);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C86F File Offset: 0x0000AA6F
		public static ILoggerRepository CreateRepository(string repository)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			return LoggerManager.RepositorySelector.CreateRepository(repository, null);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C88B File Offset: 0x0000AA8B
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(string repository, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repository, repositoryType);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000C894 File Offset: 0x0000AA94
		public static ILoggerRepository CreateRepository(string repository, Type repositoryType)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (repositoryType == null)
			{
				throw new ArgumentNullException("repositoryType");
			}
			return LoggerManager.RepositorySelector.CreateRepository(repository, repositoryType);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		[Obsolete("Use CreateRepository instead of CreateDomain")]
		public static ILoggerRepository CreateDomain(Assembly repositoryAssembly, Type repositoryType)
		{
			return LoggerManager.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C8CD File Offset: 0x0000AACD
		public static ILoggerRepository CreateRepository(Assembly repositoryAssembly, Type repositoryType)
		{
			if (repositoryAssembly == null)
			{
				throw new ArgumentNullException("repositoryAssembly");
			}
			if (repositoryType == null)
			{
				throw new ArgumentNullException("repositoryType");
			}
			return LoggerManager.RepositorySelector.CreateRepository(repositoryAssembly, repositoryType);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000C903 File Offset: 0x0000AB03
		public static ILoggerRepository[] GetAllRepositories()
		{
			return LoggerManager.RepositorySelector.GetAllRepositories();
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C90F File Offset: 0x0000AB0F
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000C916 File Offset: 0x0000AB16
		public static IRepositorySelector RepositorySelector
		{
			get
			{
				return LoggerManager.s_repositorySelector;
			}
			set
			{
				LoggerManager.s_repositorySelector = value;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000C920 File Offset: 0x0000AB20
		private static string GetVersionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			stringBuilder.Append("log4net assembly [").Append(executingAssembly.FullName).Append("]. ");
			stringBuilder.Append("Loaded from [").Append(SystemInfo.AssemblyLocationInfo(executingAssembly)).Append("]. ");
			stringBuilder.Append("(.NET Runtime [").Append(Environment.Version.ToString()).Append("]");
			stringBuilder.Append(" on ").Append(Environment.OSVersion.ToString());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C9CD File Offset: 0x0000ABCD
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			LoggerManager.Shutdown();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		private static void OnProcessExit(object sender, EventArgs e)
		{
			LoggerManager.Shutdown();
		}

		// Token: 0x04000198 RID: 408
		private static readonly Type declaringType = typeof(LoggerManager);

		// Token: 0x04000199 RID: 409
		private static IRepositorySelector s_repositorySelector;
	}
}
