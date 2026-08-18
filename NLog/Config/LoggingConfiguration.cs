using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Internal;
using NLog.Layouts;
using NLog.Targets;

namespace NLog.Config
{
	// Token: 0x0200004D RID: 77
	public class LoggingConfiguration
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005780 File Offset: 0x00003980
		public LoggingConfiguration()
		{
			this.LoggingRules = new List<LoggingRule>();
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000057BE File Offset: 0x000039BE
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000057C6 File Offset: 0x000039C6
		[Obsolete("This option will be removed in NLog 5")]
		public bool ExceptionLoggingOldStyle { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000057CF File Offset: 0x000039CF
		public IDictionary<string, SimpleLayout> Variables
		{
			get
			{
				return this.variables;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000057D7 File Offset: 0x000039D7
		public ReadOnlyCollection<Target> ConfiguredNamedTargets
		{
			get
			{
				return new List<Target>(this.targets.Values).AsReadOnly();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000057EE File Offset: 0x000039EE
		public virtual IEnumerable<string> FileNamesToWatch
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000057F6 File Offset: 0x000039F6
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000057FE File Offset: 0x000039FE
		public IList<LoggingRule> LoggingRules { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005807 File Offset: 0x00003A07
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000580F File Offset: 0x00003A0F
		[CanBeNull]
		public CultureInfo DefaultCultureInfo { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005818 File Offset: 0x00003A18
		public ReadOnlyCollection<Target> AllTargets
		{
			get
			{
				IEnumerable<Target> first = this.configItems.OfType<Target>();
				return first.Concat(this.targets.Values).Distinct(LoggingConfiguration.TargetNameComparer).ToList<Target>().AsReadOnly();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005856 File Offset: 0x00003A56
		public void AddTarget([NotNull] Target target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.AddTarget(target.Name, target);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005874 File Offset: 0x00003A74
		public void AddTarget(string name, Target target)
		{
			if (name == null)
			{
				throw new ArgumentException("Target name cannot be null", "name");
			}
			InternalLogger.Debug("Registering target {0}: {1}", new object[]
			{
				name,
				target.GetType().FullName
			});
			this.targets[name] = target;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000058C8 File Offset: 0x00003AC8
		public Target FindTargetByName(string name)
		{
			Target result;
			if (!this.targets.TryGetValue(name, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000058E8 File Offset: 0x00003AE8
		public TTarget FindTargetByName<TTarget>(string name) where TTarget : Target
		{
			return this.FindTargetByName(name) as TTarget;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000058FC File Offset: 0x00003AFC
		public void AddRule(LogLevel minLevel, LogLevel maxLevel, string targetName, string loggerNamePattern = "*")
		{
			Target target = this.FindTargetByName(targetName);
			if (target == null)
			{
				throw new NLogRuntimeException("Target '{0}' not found", new object[]
				{
					targetName
				});
			}
			this.AddRule(minLevel, maxLevel, target, loggerNamePattern);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005936 File Offset: 0x00003B36
		public void AddRule(LogLevel minLevel, LogLevel maxLevel, Target target, string loggerNamePattern = "*")
		{
			this.LoggingRules.Add(new LoggingRule(loggerNamePattern, minLevel, maxLevel, target));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005950 File Offset: 0x00003B50
		public void AddRuleForOneLevel(LogLevel level, string targetName, string loggerNamePattern = "*")
		{
			Target target = this.FindTargetByName(targetName);
			if (target == null)
			{
				throw new NLogConfigurationException("Target '{0}' not found", new object[]
				{
					targetName
				});
			}
			this.AddRuleForOneLevel(level, target, loggerNamePattern);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005988 File Offset: 0x00003B88
		public void AddRuleForOneLevel(LogLevel level, Target target, string loggerNamePattern = "*")
		{
			LoggingRule loggingRule = new LoggingRule(loggerNamePattern, target);
			loggingRule.EnableLoggingForLevel(level);
			this.LoggingRules.Add(loggingRule);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000059B0 File Offset: 0x00003BB0
		public void AddRuleForAllLevels(string targetName, string loggerNamePattern = "*")
		{
			Target target = this.FindTargetByName(targetName);
			if (target == null)
			{
				throw new NLogRuntimeException("Target '{0}' not found", new object[]
				{
					targetName
				});
			}
			this.AddRuleForAllLevels(target, loggerNamePattern);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000059E8 File Offset: 0x00003BE8
		public void AddRuleForAllLevels(Target target, string loggerNamePattern = "*")
		{
			LoggingRule loggingRule = new LoggingRule(loggerNamePattern, target);
			loggingRule.EnableLoggingForLevels(LogLevel.MinLevel, LogLevel.MaxLevel);
			this.LoggingRules.Add(loggingRule);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005A19 File Offset: 0x00003C19
		public virtual LoggingConfiguration Reload()
		{
			return this;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005A1C File Offset: 0x00003C1C
		public void RemoveTarget(string name)
		{
			this.targets.Remove(name);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005A2C File Offset: 0x00003C2C
		public void Install(InstallationContext installationContext)
		{
			if (installationContext == null)
			{
				throw new ArgumentNullException("installationContext");
			}
			this.InitializeAll();
			List<IInstallable> installableItems = this.GetInstallableItems();
			foreach (IInstallable installable in installableItems)
			{
				installationContext.Info("Installing '{0}'", new object[]
				{
					installable
				});
				try
				{
					installable.Install(installationContext);
					installationContext.Info("Finished installing '{0}'.", new object[]
					{
						installable
					});
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Install of '{0}' failed.", new object[]
					{
						installable
					});
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					installationContext.Error("Install of '{0}' failed: {1}.", new object[]
					{
						installable,
						ex
					});
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005B24 File Offset: 0x00003D24
		public void Uninstall(InstallationContext installationContext)
		{
			if (installationContext == null)
			{
				throw new ArgumentNullException("installationContext");
			}
			this.InitializeAll();
			List<IInstallable> installableItems = this.GetInstallableItems();
			foreach (IInstallable installable in installableItems)
			{
				installationContext.Info("Uninstalling '{0}'", new object[]
				{
					installable
				});
				try
				{
					installable.Uninstall(installationContext);
					installationContext.Info("Finished uninstalling '{0}'.", new object[]
					{
						installable
					});
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Uninstall of '{0}' failed.", new object[]
					{
						installable
					});
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					installationContext.Error("Uninstall of '{0}' failed: {1}.", new object[]
					{
						installable,
						ex
					});
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005C1C File Offset: 0x00003E1C
		internal void Close()
		{
			InternalLogger.Debug("Closing logging configuration...");
			List<ISupportsInitialize> supportsInitializes = this.GetSupportsInitializes(false);
			foreach (ISupportsInitialize supportsInitialize in supportsInitializes)
			{
				InternalLogger.Trace("Closing {0}", new object[]
				{
					supportsInitialize
				});
				try
				{
					supportsInitialize.Close();
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "Exception while closing.");
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			InternalLogger.Debug("Finished closing logging configuration.");
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005CC4 File Offset: 0x00003EC4
		internal void Dump()
		{
			if (!InternalLogger.IsDebugEnabled)
			{
				return;
			}
			InternalLogger.Debug("--- NLog configuration dump ---");
			InternalLogger.Debug("Targets:");
			List<Target> list = this.targets.Values.ToList<Target>();
			foreach (Target target in list)
			{
				InternalLogger.Debug("{0}", new object[]
				{
					target
				});
			}
			InternalLogger.Debug("Rules:");
			List<LoggingRule> list2 = this.LoggingRules.ToList<LoggingRule>();
			foreach (LoggingRule loggingRule in list2)
			{
				InternalLogger.Debug("{0}", new object[]
				{
					loggingRule
				});
			}
			InternalLogger.Debug("--- End of NLog configuration dump ---");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005DD0 File Offset: 0x00003FD0
		internal void FlushAllTargets(AsyncContinuation asyncContinuation)
		{
			List<Target> list = new List<Target>();
			List<LoggingRule> list2 = this.LoggingRules.ToList<LoggingRule>();
			foreach (LoggingRule loggingRule in list2)
			{
				List<Target> list3 = loggingRule.Targets.ToList<Target>();
				foreach (Target item in list3)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			AsyncHelpers.ForEachItemInParallel<Target>(list, asyncContinuation, delegate(Target target, AsyncContinuation cont)
			{
				target.Flush(cont);
			});
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005EA4 File Offset: 0x000040A4
		internal void ValidateConfig()
		{
			List<object> list = new List<object>();
			List<LoggingRule> list2 = this.LoggingRules.ToList<LoggingRule>();
			foreach (LoggingRule item in list2)
			{
				list.Add(item);
			}
			List<Target> list3 = this.targets.Values.ToList<Target>();
			foreach (Target item2 in list3)
			{
				list.Add(item2);
			}
			this.configItems = ObjectGraphScanner.FindReachableObjects<object>(list.ToArray());
			InternalLogger.Info("Found {0} configuration items", new object[]
			{
				this.configItems.Count
			});
			foreach (object o in this.configItems)
			{
				PropertyHelper.CheckRequiredParameters(o);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005FD4 File Offset: 0x000041D4
		internal void InitializeAll()
		{
			this.ValidateConfig();
			List<ISupportsInitialize> supportsInitializes = this.GetSupportsInitializes(true);
			foreach (ISupportsInitialize supportsInitialize in supportsInitializes)
			{
				InternalLogger.Trace("Initializing {0}", new object[]
				{
					supportsInitialize
				});
				try
				{
					supportsInitialize.Initialize(this);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					if (LogManager.ThrowExceptions)
					{
						throw new NLogConfigurationException("Error during initialization of " + supportsInitialize, ex);
					}
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006080 File Offset: 0x00004280
		internal void EnsureInitialized()
		{
			this.InitializeAll();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006088 File Offset: 0x00004288
		private List<IInstallable> GetInstallableItems()
		{
			return this.configItems.OfType<IInstallable>().ToList<IInstallable>();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000609C File Offset: 0x0000429C
		private List<ISupportsInitialize> GetSupportsInitializes(bool reverse = false)
		{
			IEnumerable<ISupportsInitialize> source = this.configItems.OfType<ISupportsInitialize>();
			if (reverse)
			{
				source = source.Reverse<ISupportsInitialize>();
			}
			return source.ToList<ISupportsInitialize>();
		}

		// Token: 0x0400008C RID: 140
		private readonly IDictionary<string, Target> targets = new Dictionary<string, Target>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400008D RID: 141
		private List<object> configItems = new List<object>();

		// Token: 0x0400008E RID: 142
		private readonly Dictionary<string, SimpleLayout> variables = new Dictionary<string, SimpleLayout>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400008F RID: 143
		private static IEqualityComparer<Target> TargetNameComparer = new LoggingConfiguration.TargetNameEq();

		// Token: 0x0200004E RID: 78
		private class TargetNameEq : IEqualityComparer<Target>
		{
			// Token: 0x06000199 RID: 409 RVA: 0x000060D1 File Offset: 0x000042D1
			public bool Equals(Target x, Target y)
			{
				return string.Equals(x.Name, y.Name);
			}

			// Token: 0x0600019A RID: 410 RVA: 0x000060E4 File Offset: 0x000042E4
			public int GetHashCode(Target obj)
			{
				if (obj.Name == null)
				{
					return 0;
				}
				return obj.Name.GetHashCode();
			}
		}
	}
}
