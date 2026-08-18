using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Targets;

namespace NLog
{
	// Token: 0x0200011D RID: 285
	public class LogFactory : IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600081A RID: 2074 RVA: 0x00012194 File Offset: 0x00010394
		// (remove) Token: 0x0600081B RID: 2075 RVA: 0x000121CC File Offset: 0x000103CC
		public event EventHandler<LoggingConfigurationChangedEventArgs> ConfigurationChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600081C RID: 2076 RVA: 0x00012204 File Offset: 0x00010404
		// (remove) Token: 0x0600081D RID: 2077 RVA: 0x0001223C File Offset: 0x0001043C
		public event EventHandler<LoggingConfigurationReloadedEventArgs> ConfigurationReloaded;

		// Token: 0x0600081E RID: 2078 RVA: 0x00012274 File Offset: 0x00010474
		public LogFactory()
		{
			this.watcher = new MultiFileWatcher();
			this.watcher.OnChange += new FileSystemEventHandler(this.ConfigFileChanged);
			LogFactory.CurrentAppDomain.DomainUnload += this.currentAppDomain_DomainUnload;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000122E0 File Offset: 0x000104E0
		public LogFactory(LoggingConfiguration config) : this()
		{
			this.Configuration = config;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000122EF File Offset: 0x000104EF
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00012305 File Offset: 0x00010505
		public static IAppDomain CurrentAppDomain
		{
			get
			{
				IAppDomain result;
				if ((result = LogFactory.currentAppDomain) == null)
				{
					result = (LogFactory.currentAppDomain = AppDomainWrapper.CurrentDomain);
				}
				return result;
			}
			set
			{
				LogFactory.currentAppDomain = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001230D File Offset: 0x0001050D
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00012315 File Offset: 0x00010515
		public bool ThrowExceptions { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001231E File Offset: 0x0001051E
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00012326 File Offset: 0x00010526
		public bool? ThrowConfigExceptions { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00012330 File Offset: 0x00010530
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00012478 File Offset: 0x00010678
		public LoggingConfiguration Configuration
		{
			get
			{
				if (this.configLoaded)
				{
					return this.config;
				}
				LoggingConfiguration result;
				lock (this.syncRoot)
				{
					if (this.configLoaded)
					{
						result = this.config;
					}
					else
					{
						if (this.config == null)
						{
							this.config = XmlLoggingConfiguration.AppConfig;
						}
						if (this.config == null)
						{
							IEnumerable<string> enumerable = this.GetCandidateConfigFilePaths();
							foreach (string text in enumerable)
							{
								if (File.Exists(text))
								{
									this.LoadLoggingConfiguration(text);
									break;
								}
							}
						}
						if (this.config != null)
						{
							try
							{
								this.config.Dump();
								try
								{
									this.watcher.Watch(this.config.FileNamesToWatch);
								}
								catch (Exception ex)
								{
									if (ex.MustBeRethrownImmediately())
									{
										throw;
									}
									InternalLogger.Warn(ex, "Cannot start file watching. File watching is disabled");
								}
								this.config.InitializeAll();
								this.LogConfigurationInitialized();
							}
							finally
							{
								this.configLoaded = true;
							}
						}
						result = this.config;
					}
				}
				return result;
			}
			set
			{
				try
				{
					this.watcher.StopWatching();
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Cannot stop file watching.");
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
				lock (this.syncRoot)
				{
					LoggingConfiguration loggingConfiguration = this.config;
					if (loggingConfiguration != null)
					{
						InternalLogger.Info("Closing old configuration.");
						this.Flush();
						loggingConfiguration.Close();
					}
					this.config = value;
					if (this.config == null)
					{
						this.configLoaded = false;
					}
					else
					{
						try
						{
							this.config.Dump();
							this.config.InitializeAll();
							this.ReconfigExistingLoggers();
							try
							{
								this.watcher.Watch(this.config.FileNamesToWatch);
							}
							catch (Exception ex2)
							{
								InternalLogger.Warn(ex2, "Cannot start file watching: {0}", new object[]
								{
									string.Join(",", this.config.FileNamesToWatch.ToArray<string>())
								});
								if (ex2.MustBeRethrown())
								{
									throw;
								}
							}
						}
						finally
						{
							this.configLoaded = true;
						}
					}
					this.OnConfigurationChanged(new LoggingConfigurationChangedEventArgs(value, loggingConfiguration));
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x000125C0 File Offset: 0x000107C0
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x000125C8 File Offset: 0x000107C8
		public LogLevel GlobalThreshold
		{
			get
			{
				return this.globalThreshold;
			}
			set
			{
				lock (this.syncRoot)
				{
					this.globalThreshold = value;
					this.ReconfigExistingLoggers();
				}
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00012610 File Offset: 0x00010810
		[CanBeNull]
		public CultureInfo DefaultCultureInfo
		{
			get
			{
				LoggingConfiguration configuration = this.Configuration;
				if (configuration == null)
				{
					return null;
				}
				return configuration.DefaultCultureInfo;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001262F File Offset: 0x0001082F
		private void LogConfigurationInitialized()
		{
			InternalLogger.Info("Configuration initialized.");
			InternalLogger.LogAssemblyVersion(typeof(ILogger).Assembly);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001264F File Offset: 0x0001084F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00012660 File Offset: 0x00010860
		public Logger CreateNullLogger()
		{
			TargetWithFilterChain[] targetsByLevel = new TargetWithFilterChain[LogLevel.MaxLevel.Ordinal + 1];
			Logger logger = new Logger();
			logger.Initialize(string.Empty, new LoggerConfiguration(targetsByLevel, false), this);
			return logger;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001269C File Offset: 0x0001089C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Logger GetCurrentClassLogger()
		{
			StackFrame stackFrame = new StackFrame(1, false);
			return this.GetLogger(stackFrame.GetMethod().DeclaringType.FullName);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000126C8 File Offset: 0x000108C8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public T GetCurrentClassLogger<T>() where T : Logger
		{
			StackFrame stackFrame = new StackFrame(1, false);
			return (T)((object)this.GetLogger(stackFrame.GetMethod().DeclaringType.FullName, typeof(T)));
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00012704 File Offset: 0x00010904
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Logger GetCurrentClassLogger(Type loggerType)
		{
			StackFrame stackFrame = new StackFrame(1, false);
			return this.GetLogger(stackFrame.GetMethod().DeclaringType.FullName, loggerType);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00012730 File Offset: 0x00010930
		public Logger GetLogger(string name)
		{
			return this.GetLogger(new LogFactory.LoggerCacheKey(name, typeof(Logger)));
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00012748 File Offset: 0x00010948
		public T GetLogger<T>(string name) where T : Logger
		{
			return (T)((object)this.GetLogger(new LogFactory.LoggerCacheKey(name, typeof(T))));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00012765 File Offset: 0x00010965
		public Logger GetLogger(string name, Type loggerType)
		{
			return this.GetLogger(new LogFactory.LoggerCacheKey(name, loggerType));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00012774 File Offset: 0x00010974
		public void ReconfigExistingLoggers()
		{
			if (this.config != null)
			{
				this.config.InitializeAll();
			}
			List<Logger> list = new List<Logger>(this.loggerCache.Loggers);
			foreach (Logger logger in list)
			{
				logger.SetConfiguration(this.GetConfigurationForLogger(logger.Name, this.config));
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000127F8 File Offset: 0x000109F8
		public void Flush()
		{
			this.Flush(LogFactory.defaultFlushTimeout);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00012824 File Offset: 0x00010A24
		public void Flush(TimeSpan timeout)
		{
			try
			{
				AsyncHelpers.RunSynchronously(delegate(AsyncContinuation cb)
				{
					this.Flush(cb, timeout);
				});
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error with flush.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00012888 File Offset: 0x00010A88
		public void Flush(int timeoutMilliseconds)
		{
			this.Flush(TimeSpan.FromMilliseconds((double)timeoutMilliseconds));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00012897 File Offset: 0x00010A97
		public void Flush(AsyncContinuation asyncContinuation)
		{
			this.Flush(asyncContinuation, TimeSpan.MaxValue);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000128A5 File Offset: 0x00010AA5
		public void Flush(AsyncContinuation asyncContinuation, int timeoutMilliseconds)
		{
			this.Flush(asyncContinuation, TimeSpan.FromMilliseconds((double)timeoutMilliseconds));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000128B8 File Offset: 0x00010AB8
		public void Flush(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			try
			{
				InternalLogger.Trace("LogFactory.Flush({0})", new object[]
				{
					timeout
				});
				LoggingConfiguration configuration = this.Configuration;
				if (configuration != null)
				{
					InternalLogger.Trace("Flushing all targets...");
					configuration.FlushAllTargets(AsyncHelpers.WithTimeout(asyncContinuation, timeout));
				}
				else
				{
					asyncContinuation(null);
				}
			}
			catch (Exception ex)
			{
				if (this.ThrowExceptions)
				{
					throw;
				}
				InternalLogger.Error(ex, "Error with flush.");
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00012934 File Offset: 0x00010B34
		[Obsolete("Use SuspendLogging() instead.")]
		public IDisposable DisableLogging()
		{
			return this.SuspendLogging();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001293C File Offset: 0x00010B3C
		[Obsolete("Use ResumeLogging() instead.")]
		public void EnableLogging()
		{
			this.ResumeLogging();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00012944 File Offset: 0x00010B44
		public IDisposable SuspendLogging()
		{
			lock (this.syncRoot)
			{
				this.logsEnabled--;
				if (this.logsEnabled == -1)
				{
					this.ReconfigExistingLoggers();
				}
			}
			return new LogFactory.LogEnabler(this);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000129A4 File Offset: 0x00010BA4
		public void ResumeLogging()
		{
			lock (this.syncRoot)
			{
				this.logsEnabled++;
				if (this.logsEnabled == 0)
				{
					this.ReconfigExistingLoggers();
				}
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000129FC File Offset: 0x00010BFC
		public bool IsLoggingEnabled()
		{
			return this.logsEnabled >= 0;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00012A0C File Offset: 0x00010C0C
		protected virtual void OnConfigurationChanged(LoggingConfigurationChangedEventArgs e)
		{
			EventHandler<LoggingConfigurationChangedEventArgs> configurationChanged = this.ConfigurationChanged;
			if (configurationChanged != null)
			{
				configurationChanged(this, e);
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00012A2C File Offset: 0x00010C2C
		internal void ReloadConfigOnTimer(object state)
		{
			LoggingConfiguration loggingConfiguration = (LoggingConfiguration)state;
			InternalLogger.Info("Reloading configuration...");
			lock (this.syncRoot)
			{
				if (this.reloadTimer != null)
				{
					this.reloadTimer.Dispose();
					this.reloadTimer = null;
				}
				if (this.IsDisposing)
				{
					this.watcher.Dispose();
				}
				else
				{
					this.watcher.StopWatching();
					try
					{
						if (this.Configuration != loggingConfiguration)
						{
							throw new NLogConfigurationException("Config changed in between. Not reloading.");
						}
						LoggingConfiguration loggingConfiguration2 = loggingConfiguration.Reload();
						XmlLoggingConfiguration xmlLoggingConfiguration = loggingConfiguration2 as XmlLoggingConfiguration;
						if (xmlLoggingConfiguration != null && (xmlLoggingConfiguration.InitializeSucceeded == null || !xmlLoggingConfiguration.InitializeSucceeded.Value))
						{
							throw new NLogConfigurationException("Configuration.Reload() failed. Invalid XML?");
						}
						if (loggingConfiguration2 == null)
						{
							throw new NLogConfigurationException("Configuration.Reload() returned null. Not reloading.");
						}
						this.Configuration = loggingConfiguration2;
						if (this.ConfigurationReloaded != null)
						{
							this.ConfigurationReloaded(this, new LoggingConfigurationReloadedEventArgs(true, null));
						}
					}
					catch (Exception ex)
					{
						if (ex is NLogConfigurationException)
						{
							InternalLogger.Warn(ex, "NLog configuration while reloading");
						}
						else if (ex.MustBeRethrown())
						{
							throw;
						}
						this.watcher.Watch(loggingConfiguration.FileNamesToWatch);
						EventHandler<LoggingConfigurationReloadedEventArgs> configurationReloaded = this.ConfigurationReloaded;
						if (configurationReloaded != null)
						{
							configurationReloaded(this, new LoggingConfigurationReloadedEventArgs(false, ex));
						}
					}
				}
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00012BB0 File Offset: 0x00010DB0
		private void GetTargetsByLevelForLogger(string name, IEnumerable<LoggingRule> rules, TargetWithFilterChain[] targetsByLevel, TargetWithFilterChain[] lastTargetsByLevel, bool[] suppressedLevels)
		{
			List<LoggingRule> list = new List<LoggingRule>(rules);
			foreach (LoggingRule loggingRule in list)
			{
				if (loggingRule.NameMatches(name))
				{
					for (int i = 0; i <= LogLevel.MaxLevel.Ordinal; i++)
					{
						if (i >= this.GlobalThreshold.Ordinal && !suppressedLevels[i] && loggingRule.IsLoggingEnabledForLevel(LogLevel.FromOrdinal(i)))
						{
							if (loggingRule.Final)
							{
								suppressedLevels[i] = true;
							}
							foreach (Target target in loggingRule.Targets.ToList<Target>())
							{
								TargetWithFilterChain targetWithFilterChain = new TargetWithFilterChain(target, loggingRule.Filters);
								if (lastTargetsByLevel[i] != null)
								{
									lastTargetsByLevel[i].NextInChain = targetWithFilterChain;
								}
								else
								{
									targetsByLevel[i] = targetWithFilterChain;
								}
								lastTargetsByLevel[i] = targetWithFilterChain;
							}
						}
					}
					this.GetTargetsByLevelForLogger(name, loggingRule.ChildRules, targetsByLevel, lastTargetsByLevel, suppressedLevels);
				}
			}
			for (int j = 0; j <= LogLevel.MaxLevel.Ordinal; j++)
			{
				TargetWithFilterChain targetWithFilterChain2 = targetsByLevel[j];
				if (targetWithFilterChain2 != null)
				{
					targetWithFilterChain2.PrecalculateStackTraceUsage();
				}
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00012D08 File Offset: 0x00010F08
		internal LoggerConfiguration GetConfigurationForLogger(string name, LoggingConfiguration configuration)
		{
			TargetWithFilterChain[] array = new TargetWithFilterChain[LogLevel.MaxLevel.Ordinal + 1];
			TargetWithFilterChain[] lastTargetsByLevel = new TargetWithFilterChain[LogLevel.MaxLevel.Ordinal + 1];
			bool[] suppressedLevels = new bool[LogLevel.MaxLevel.Ordinal + 1];
			if (configuration != null && this.IsLoggingEnabled())
			{
				this.GetTargetsByLevelForLogger(name, configuration.LoggingRules, array, lastTargetsByLevel, suppressedLevels);
			}
			InternalLogger.Debug("Targets for {0} by level:", new object[]
			{
				name
			});
			for (int i = 0; i <= LogLevel.MaxLevel.Ordinal; i++)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} =>", new object[]
				{
					LogLevel.FromOrdinal(i)
				});
				for (TargetWithFilterChain targetWithFilterChain = array[i]; targetWithFilterChain != null; targetWithFilterChain = targetWithFilterChain.NextInChain)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", new object[]
					{
						targetWithFilterChain.Target.Name
					});
					if (targetWithFilterChain.FilterChain.Count > 0)
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " ({0} filters)", new object[]
						{
							targetWithFilterChain.FilterChain.Count
						});
					}
				}
				InternalLogger.Debug(stringBuilder.ToString());
			}
			return new LoggerConfiguration(array, configuration != null && configuration.ExceptionLoggingOldStyle);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00012E65 File Offset: 0x00011065
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.watcher.Dispose();
				if (this.reloadTimer != null)
				{
					this.reloadTimer.Dispose();
					this.reloadTimer = null;
				}
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00012E8F File Offset: 0x0001108F
		public IEnumerable<string> GetCandidateConfigFilePaths()
		{
			if (this.candidateConfigFilePaths != null)
			{
				return this.candidateConfigFilePaths.AsReadOnly();
			}
			return LogFactory.GetDefaultCandidateConfigFilePaths();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00012EAA File Offset: 0x000110AA
		public void SetCandidateConfigFilePaths(IEnumerable<string> filePaths)
		{
			this.candidateConfigFilePaths = new List<string>();
			if (filePaths != null)
			{
				this.candidateConfigFilePaths.AddRange(filePaths);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00012EC6 File Offset: 0x000110C6
		public void ResetCandidateConfigFilePath()
		{
			this.candidateConfigFilePaths = null;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000131C4 File Offset: 0x000113C4
		private static IEnumerable<string> GetDefaultCandidateConfigFilePaths()
		{
			if (LogFactory.CurrentAppDomain.BaseDirectory != null)
			{
				yield return Path.Combine(LogFactory.CurrentAppDomain.BaseDirectory, "NLog.config");
			}
			string cf = LogFactory.CurrentAppDomain.ConfigurationFile;
			if (cf != null)
			{
				yield return Path.ChangeExtension(cf, ".nlog");
				if (cf.Contains(".vshost."))
				{
					yield return Path.ChangeExtension(cf.Replace(".vshost.", "."), ".nlog");
				}
				IEnumerable<string> privateBinPaths = LogFactory.CurrentAppDomain.PrivateBinPath;
				if (privateBinPaths != null)
				{
					foreach (string path in privateBinPaths)
					{
						if (path != null)
						{
							yield return Path.Combine(path, "NLog.config");
						}
					}
				}
			}
			Assembly nlogAssembly = typeof(LogFactory).Assembly;
			if (!nlogAssembly.GlobalAssemblyCache && !string.IsNullOrEmpty(nlogAssembly.Location))
			{
				yield return nlogAssembly.Location + ".nlog";
			}
			yield break;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000131DC File Offset: 0x000113DC
		private Logger GetLogger(LogFactory.LoggerCacheKey cacheKey)
		{
			Logger result;
			lock (this.syncRoot)
			{
				Logger logger = this.loggerCache.Retrieve(cacheKey);
				if (logger != null)
				{
					result = logger;
				}
				else
				{
					Logger logger2;
					if (cacheKey.ConcreteType != null && cacheKey.ConcreteType != typeof(Logger))
					{
						string fullName = cacheKey.ConcreteType.FullName;
						try
						{
							if (cacheKey.ConcreteType.IsStaticClass())
							{
								string message = string.Format("GetLogger / GetCurrentClassLogger is '{0}' as loggerType can be a static class and should inherit from Logger", fullName);
								InternalLogger.Error(message);
								if (this.ThrowExceptions)
								{
									throw new NLogRuntimeException(message);
								}
								logger2 = LogFactory.CreateDefaultLogger(ref cacheKey);
							}
							else
							{
								object obj2 = FactoryHelper.CreateInstance(cacheKey.ConcreteType);
								logger2 = (obj2 as Logger);
								if (logger2 == null)
								{
									string message2 = string.Format("GetLogger / GetCurrentClassLogger got '{0}' as loggerType which doesn't inherit from Logger", fullName);
									InternalLogger.Error(message2);
									if (this.ThrowExceptions)
									{
										throw new NLogRuntimeException(message2);
									}
									logger2 = LogFactory.CreateDefaultLogger(ref cacheKey);
								}
							}
							goto IL_118;
						}
						catch (Exception ex)
						{
							InternalLogger.Error(ex, "GetLogger / GetCurrentClassLogger. Cannot create instance of type '{0}'. It should have an default contructor. ", new object[]
							{
								fullName
							});
							if (ex.MustBeRethrown())
							{
								throw;
							}
							logger2 = LogFactory.CreateDefaultLogger(ref cacheKey);
							goto IL_118;
						}
					}
					logger2 = new Logger();
					IL_118:
					if (cacheKey.ConcreteType != null)
					{
						logger2.Initialize(cacheKey.Name, this.GetConfigurationForLogger(cacheKey.Name, this.Configuration), this);
					}
					this.loggerCache.InsertOrUpdate(cacheKey, logger2);
					result = logger2;
				}
			}
			return result;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00013384 File Offset: 0x00011584
		private static Logger CreateDefaultLogger(ref LogFactory.LoggerCacheKey cacheKey)
		{
			cacheKey = new LogFactory.LoggerCacheKey(cacheKey.Name, typeof(Logger));
			return new Logger();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000133B0 File Offset: 0x000115B0
		private void ConfigFileChanged(object sender, EventArgs args)
		{
			InternalLogger.Info("Configuration file change detected! Reloading in {0}ms...", new object[]
			{
				1000
			});
			lock (this.syncRoot)
			{
				if (this.reloadTimer == null)
				{
					this.reloadTimer = new Timer(new TimerCallback(this.ReloadConfigOnTimer), this.Configuration, 1000, -1);
				}
				else
				{
					this.reloadTimer.Change(1000, -1);
				}
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00013448 File Offset: 0x00011648
		private void LoadLoggingConfiguration(string configFile)
		{
			InternalLogger.Debug("Loading config from {0}", new object[]
			{
				configFile
			});
			this.config = new XmlLoggingConfiguration(configFile, this);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00013478 File Offset: 0x00011678
		private void currentAppDomain_DomainUnload(object sender, EventArgs e)
		{
			lock (this.syncRoot)
			{
				this.IsDisposing = true;
				if (this.reloadTimer != null)
				{
					this.reloadTimer.Dispose();
					this.reloadTimer = null;
				}
			}
		}

		// Token: 0x0400026D RID: 621
		private const int ReconfigAfterFileChangedTimeout = 1000;

		// Token: 0x0400026E RID: 622
		private Timer reloadTimer;

		// Token: 0x0400026F RID: 623
		private readonly MultiFileWatcher watcher;

		// Token: 0x04000270 RID: 624
		private static TimeSpan defaultFlushTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000271 RID: 625
		private static IAppDomain currentAppDomain;

		// Token: 0x04000272 RID: 626
		private readonly object syncRoot = new object();

		// Token: 0x04000273 RID: 627
		private LoggingConfiguration config;

		// Token: 0x04000274 RID: 628
		private LogLevel globalThreshold = LogLevel.MinLevel;

		// Token: 0x04000275 RID: 629
		private bool configLoaded;

		// Token: 0x04000276 RID: 630
		private int logsEnabled;

		// Token: 0x04000277 RID: 631
		private readonly LogFactory.LoggerCache loggerCache = new LogFactory.LoggerCache();

		// Token: 0x04000278 RID: 632
		private List<string> candidateConfigFilePaths;

		// Token: 0x0400027B RID: 635
		private bool IsDisposing;

		// Token: 0x0200011E RID: 286
		internal class LoggerCacheKey : IEquatable<LogFactory.LoggerCacheKey>
		{
			// Token: 0x17000180 RID: 384
			// (get) Token: 0x0600084F RID: 2127 RVA: 0x000134E9 File Offset: 0x000116E9
			// (set) Token: 0x06000850 RID: 2128 RVA: 0x000134F1 File Offset: 0x000116F1
			public string Name { get; private set; }

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000851 RID: 2129 RVA: 0x000134FA File Offset: 0x000116FA
			// (set) Token: 0x06000852 RID: 2130 RVA: 0x00013502 File Offset: 0x00011702
			public Type ConcreteType { get; private set; }

			// Token: 0x06000853 RID: 2131 RVA: 0x0001350B File Offset: 0x0001170B
			public LoggerCacheKey(string name, Type concreteType)
			{
				this.Name = name;
				this.ConcreteType = concreteType;
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00013521 File Offset: 0x00011721
			public override int GetHashCode()
			{
				return this.ConcreteType.GetHashCode() ^ this.Name.GetHashCode();
			}

			// Token: 0x06000855 RID: 2133 RVA: 0x0001353C File Offset: 0x0001173C
			public override bool Equals(object obj)
			{
				LogFactory.LoggerCacheKey loggerCacheKey = obj as LogFactory.LoggerCacheKey;
				return !object.ReferenceEquals(loggerCacheKey, null) && this.ConcreteType == loggerCacheKey.ConcreteType && loggerCacheKey.Name == this.Name;
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x00013581 File Offset: 0x00011781
			public bool Equals(LogFactory.LoggerCacheKey key)
			{
				return !object.ReferenceEquals(key, null) && this.ConcreteType == key.ConcreteType && key.Name == this.Name;
			}
		}

		// Token: 0x0200011F RID: 287
		private class LoggerCache
		{
			// Token: 0x06000857 RID: 2135 RVA: 0x000135B4 File Offset: 0x000117B4
			public void InsertOrUpdate(LogFactory.LoggerCacheKey cacheKey, Logger logger)
			{
				this.loggerCache[cacheKey] = new WeakReference(logger);
			}

			// Token: 0x06000858 RID: 2136 RVA: 0x000135C8 File Offset: 0x000117C8
			public Logger Retrieve(LogFactory.LoggerCacheKey cacheKey)
			{
				WeakReference weakReference;
				if (this.loggerCache.TryGetValue(cacheKey, out weakReference))
				{
					return weakReference.Target as Logger;
				}
				return null;
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000859 RID: 2137 RVA: 0x000135F2 File Offset: 0x000117F2
			public IEnumerable<Logger> Loggers
			{
				get
				{
					return this.GetLoggers();
				}
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x000135FC File Offset: 0x000117FC
			private IEnumerable<Logger> GetLoggers()
			{
				List<Logger> list = new List<Logger>(this.loggerCache.Count);
				foreach (WeakReference weakReference in this.loggerCache.Values)
				{
					Logger logger = weakReference.Target as Logger;
					if (logger != null)
					{
						list.Add(logger);
					}
				}
				return list;
			}

			// Token: 0x04000280 RID: 640
			private readonly Dictionary<LogFactory.LoggerCacheKey, WeakReference> loggerCache = new Dictionary<LogFactory.LoggerCacheKey, WeakReference>();
		}

		// Token: 0x02000120 RID: 288
		private class LogEnabler : IDisposable
		{
			// Token: 0x0600085C RID: 2140 RVA: 0x0001368B File Offset: 0x0001188B
			public LogEnabler(LogFactory factory)
			{
				this.factory = factory;
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x0001369A File Offset: 0x0001189A
			void IDisposable.Dispose()
			{
				this.factory.ResumeLogging();
			}

			// Token: 0x04000281 RID: 641
			private LogFactory factory;
		}
	}
}
