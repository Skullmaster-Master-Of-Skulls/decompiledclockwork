using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Targets;

namespace NLog
{
	// Token: 0x02000126 RID: 294
	public sealed class LogManager
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x0001890C File Offset: 0x00016B0C
		static LogManager()
		{
			LogManager.SetupTerminationEvents();
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00018927 File Offset: 0x00016B27
		private LogManager()
		{
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001892F File Offset: 0x00016B2F
		internal static LogFactory LogFactory
		{
			get
			{
				return LogManager.factory;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000A3C RID: 2620 RVA: 0x00018936 File Offset: 0x00016B36
		// (remove) Token: 0x06000A3D RID: 2621 RVA: 0x00018943 File Offset: 0x00016B43
		public static event EventHandler<LoggingConfigurationChangedEventArgs> ConfigurationChanged
		{
			add
			{
				LogManager.factory.ConfigurationChanged += value;
			}
			remove
			{
				LogManager.factory.ConfigurationChanged -= value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000A3E RID: 2622 RVA: 0x00018950 File Offset: 0x00016B50
		// (remove) Token: 0x06000A3F RID: 2623 RVA: 0x0001895D File Offset: 0x00016B5D
		public static event EventHandler<LoggingConfigurationReloadedEventArgs> ConfigurationReloaded
		{
			add
			{
				LogManager.factory.ConfigurationReloaded += value;
			}
			remove
			{
				LogManager.factory.ConfigurationReloaded -= value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001896A File Offset: 0x00016B6A
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00018976 File Offset: 0x00016B76
		public static bool ThrowExceptions
		{
			get
			{
				return LogManager.factory.ThrowExceptions;
			}
			set
			{
				LogManager.factory.ThrowExceptions = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00018983 File Offset: 0x00016B83
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0001898F File Offset: 0x00016B8F
		public static bool? ThrowConfigExceptions
		{
			get
			{
				return LogManager.factory.ThrowConfigExceptions;
			}
			set
			{
				LogManager.factory.ThrowConfigExceptions = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001899C File Offset: 0x00016B9C
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x000189B2 File Offset: 0x00016BB2
		internal static IAppDomain CurrentAppDomain
		{
			get
			{
				IAppDomain result;
				if ((result = LogManager.currentAppDomain) == null)
				{
					result = (LogManager.currentAppDomain = AppDomainWrapper.CurrentDomain);
				}
				return result;
			}
			set
			{
				LogManager.currentAppDomain.DomainUnload -= LogManager.TurnOffLogging;
				LogManager.currentAppDomain.ProcessExit -= LogManager.TurnOffLogging;
				LogManager.currentAppDomain = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000189E6 File Offset: 0x00016BE6
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x000189F2 File Offset: 0x00016BF2
		public static LoggingConfiguration Configuration
		{
			get
			{
				return LogManager.factory.Configuration;
			}
			set
			{
				LogManager.factory.Configuration = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000189FF File Offset: 0x00016BFF
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00018A0B File Offset: 0x00016C0B
		public static LogLevel GlobalThreshold
		{
			get
			{
				return LogManager.factory.GlobalThreshold;
			}
			set
			{
				LogManager.factory.GlobalThreshold = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00018A2D File Offset: 0x00016C2D
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00018A4C File Offset: 0x00016C4C
		[Obsolete("Use Configuration.DefaultCultureInfo property instead")]
		public static LogManager.GetCultureInfo DefaultCultureInfo
		{
			get
			{
				return () => LogManager.factory.DefaultCultureInfo ?? CultureInfo.CurrentCulture;
			}
			set
			{
				throw new NotSupportedException("Setting the DefaultCultureInfo delegate is no longer supported. Use the Configuration.DefaultCultureInfo property to change the default CultureInfo.");
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00018A58 File Offset: 0x00016C58
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Logger GetCurrentClassLogger()
		{
			return LogManager.factory.GetLogger(LogManager.GetClassFullName());
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00018A69 File Offset: 0x00016C69
		internal static bool IsHiddenAssembly(Assembly assembly)
		{
			return LogManager._hiddenAssemblies != null && LogManager._hiddenAssemblies.Contains(assembly);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00018A80 File Offset: 0x00016C80
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void AddHiddenAssembly(Assembly assembly)
		{
			lock (LogManager.lockObject)
			{
				if (LogManager._hiddenAssemblies == null || !LogManager._hiddenAssemblies.Contains(assembly))
				{
					LogManager._hiddenAssemblies = new HashSet<Assembly>(LogManager._hiddenAssemblies ?? Enumerable.Empty<Assembly>())
					{
						assembly
					};
				}
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00018AF4 File Offset: 0x00016CF4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Logger GetCurrentClassLogger(Type loggerType)
		{
			return LogManager.factory.GetLogger(LogManager.GetClassFullName(), loggerType);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00018B06 File Offset: 0x00016D06
		[CLSCompliant(false)]
		public static Logger CreateNullLogger()
		{
			return LogManager.factory.CreateNullLogger();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00018B12 File Offset: 0x00016D12
		[CLSCompliant(false)]
		public static Logger GetLogger(string name)
		{
			return LogManager.factory.GetLogger(name);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00018B1F File Offset: 0x00016D1F
		[CLSCompliant(false)]
		public static Logger GetLogger(string name, Type loggerType)
		{
			return LogManager.factory.GetLogger(name, loggerType);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00018B2D File Offset: 0x00016D2D
		public static void ReconfigExistingLoggers()
		{
			LogManager.factory.ReconfigExistingLoggers();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00018B39 File Offset: 0x00016D39
		public static void Flush()
		{
			LogManager.factory.Flush();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00018B45 File Offset: 0x00016D45
		public static void Flush(TimeSpan timeout)
		{
			LogManager.factory.Flush(timeout);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00018B52 File Offset: 0x00016D52
		public static void Flush(int timeoutMilliseconds)
		{
			LogManager.factory.Flush(timeoutMilliseconds);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00018B5F File Offset: 0x00016D5F
		public static void Flush(AsyncContinuation asyncContinuation)
		{
			LogManager.factory.Flush(asyncContinuation);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00018B6C File Offset: 0x00016D6C
		public static void Flush(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			LogManager.factory.Flush(asyncContinuation, timeout);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00018B7A File Offset: 0x00016D7A
		public static void Flush(AsyncContinuation asyncContinuation, int timeoutMilliseconds)
		{
			LogManager.factory.Flush(asyncContinuation, timeoutMilliseconds);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00018B88 File Offset: 0x00016D88
		public static IDisposable DisableLogging()
		{
			return LogManager.factory.SuspendLogging();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00018B94 File Offset: 0x00016D94
		public static void EnableLogging()
		{
			LogManager.factory.ResumeLogging();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00018BA0 File Offset: 0x00016DA0
		public static bool IsLoggingEnabled()
		{
			return LogManager.factory.IsLoggingEnabled();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00018BAC File Offset: 0x00016DAC
		public static void Shutdown()
		{
			if (LogManager.Configuration != null && LogManager.Configuration.AllTargets != null)
			{
				foreach (Target target in LogManager.Configuration.AllTargets)
				{
					if (target != null)
					{
						target.Dispose();
					}
				}
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00018C14 File Offset: 0x00016E14
		private static void SetupTerminationEvents()
		{
			try
			{
				LogManager.CurrentAppDomain.ProcessExit += LogManager.TurnOffLogging;
				LogManager.CurrentAppDomain.DomainUnload += LogManager.TurnOffLogging;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Error setting up termination events.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00018C78 File Offset: 0x00016E78
		private static string GetClassFullName()
		{
			int num = 2;
			MethodBase method;
			string result;
			for (;;)
			{
				StackFrame stackFrame = new StackFrame(num, false);
				method = stackFrame.GetMethod();
				Type declaringType = method.DeclaringType;
				if (declaringType == null)
				{
					break;
				}
				num++;
				result = declaringType.FullName;
				if (!declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase))
				{
					return result;
				}
			}
			result = method.Name;
			return result;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00018CD6 File Offset: 0x00016ED6
		private static void TurnOffLogging(object sender, EventArgs args)
		{
			InternalLogger.Info("Shutting down logging...");
			LogManager.Configuration = null;
			InternalLogger.Info("Logger has been shut down.");
		}

		// Token: 0x040002A0 RID: 672
		private static readonly LogFactory factory = new LogFactory();

		// Token: 0x040002A1 RID: 673
		private static IAppDomain currentAppDomain;

		// Token: 0x040002A2 RID: 674
		private static ICollection<Assembly> _hiddenAssemblies;

		// Token: 0x040002A3 RID: 675
		private static readonly object lockObject = new object();

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000A63 RID: 2659
		[Obsolete]
		public delegate CultureInfo GetCultureInfo();
	}
}
