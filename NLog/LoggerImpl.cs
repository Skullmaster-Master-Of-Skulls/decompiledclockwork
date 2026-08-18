using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using NLog.Common;
using NLog.Config;
using NLog.Filters;
using NLog.Internal;
using NLog.Targets;

namespace NLog
{
	// Token: 0x02000123 RID: 291
	internal static class LoggerImpl
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x000180CC File Offset: 0x000162CC
		internal static void Write([NotNull] Type loggerType, TargetWithFilterChain targets, LogEventInfo logEvent, LogFactory factory)
		{
			if (targets == null)
			{
				return;
			}
			StackTraceUsage stackTraceUsage = targets.GetStackTraceUsage();
			if (stackTraceUsage != StackTraceUsage.None && !logEvent.HasStackTrace)
			{
				StackTrace stackTrace = new StackTrace(0, stackTraceUsage == StackTraceUsage.WithSource);
				int userStackFrame = LoggerImpl.FindCallingMethodOnStackTrace(stackTrace, loggerType);
				logEvent.SetStackTrace(stackTrace, userStackFrame);
			}
			int originalThreadId = Thread.CurrentThread.ManagedThreadId;
			AsyncContinuation onException = delegate(Exception ex)
			{
				if (ex != null && factory.ThrowExceptions && Thread.CurrentThread.ManagedThreadId == originalThreadId)
				{
					throw new NLogRuntimeException("Exception occurred in NLog", ex);
				}
			};
			for (TargetWithFilterChain targetWithFilterChain = targets; targetWithFilterChain != null; targetWithFilterChain = targetWithFilterChain.NextInChain)
			{
				if (!LoggerImpl.WriteToTargetWithFilterChain(targetWithFilterChain, logEvent, onException))
				{
					return;
				}
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000181A0 File Offset: 0x000163A0
		internal static int FindCallingMethodOnStackTrace([NotNull] StackTrace stackTrace, [NotNull] Type loggerType)
		{
			StackFrame[] frames = stackTrace.GetFrames();
			if (frames == null)
			{
				return 0;
			}
			List<LoggerImpl.StackFrameWithIndex> list = frames.Select((StackFrame f, int i) => new LoggerImpl.StackFrameWithIndex(i, f)).ToList<LoggerImpl.StackFrameWithIndex>();
			List<LoggerImpl.StackFrameWithIndex> list2 = (from p in list
			where !LoggerImpl.SkipAssembly(p.StackFrame)
			select p).ToList<LoggerImpl.StackFrameWithIndex>();
			IEnumerable<LoggerImpl.StackFrameWithIndex> source = list2.SkipWhile((LoggerImpl.StackFrameWithIndex p) => !LoggerImpl.IsLoggerType(p.StackFrame, loggerType));
			List<LoggerImpl.StackFrameWithIndex> list3 = source.SkipWhile((LoggerImpl.StackFrameWithIndex p) => LoggerImpl.IsLoggerType(p.StackFrame, loggerType)).ToList<LoggerImpl.StackFrameWithIndex>();
			List<LoggerImpl.StackFrameWithIndex> list4 = list3;
			if (!list4.Any<LoggerImpl.StackFrameWithIndex>())
			{
				list4 = list2;
			}
			return LoggerImpl.FindIndexOfCallingMethod(list, list4);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00018260 File Offset: 0x00016460
		private static int FindIndexOfCallingMethod(List<LoggerImpl.StackFrameWithIndex> allStackFrames, List<LoggerImpl.StackFrameWithIndex> candidateStackFrames)
		{
			LoggerImpl.StackFrameWithIndex stackFrameWithIndex = candidateStackFrames.FirstOrDefault<LoggerImpl.StackFrameWithIndex>();
			LoggerImpl.StackFrameWithIndex stackFrameWithIndex2 = stackFrameWithIndex;
			if (stackFrameWithIndex2 != null)
			{
				if (stackFrameWithIndex2.StackFrame.GetMethod().Name == "MoveNext" && allStackFrames.Count > stackFrameWithIndex2.StackFrameIndex)
				{
					LoggerImpl.StackFrameWithIndex stackFrameWithIndex3 = allStackFrames[stackFrameWithIndex2.StackFrameIndex + 1];
					Type declaringType = stackFrameWithIndex3.StackFrame.GetMethod().DeclaringType;
					if (declaringType == typeof(AsyncTaskMethodBuilder) || declaringType == typeof(AsyncTaskMethodBuilder<>))
					{
						candidateStackFrames = candidateStackFrames.Skip(1).ToList<LoggerImpl.StackFrameWithIndex>();
						return LoggerImpl.FindIndexOfCallingMethod(allStackFrames, candidateStackFrames);
					}
				}
				return stackFrameWithIndex2.StackFrameIndex;
			}
			return 0;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00018308 File Offset: 0x00016508
		private static bool SkipAssembly(StackFrame frame)
		{
			MethodBase method = frame.GetMethod();
			Assembly assembly = (method.DeclaringType != null) ? method.DeclaringType.Assembly : method.Module.Assembly;
			return LoggerImpl.SkipAssembly(assembly);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001834C File Offset: 0x0001654C
		private static bool IsLoggerType(StackFrame frame, Type loggerType)
		{
			MethodBase method = frame.GetMethod();
			Type declaringType = method.DeclaringType;
			return declaringType != null && loggerType == declaringType;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001837C File Offset: 0x0001657C
		private static bool SkipAssembly(Assembly assembly)
		{
			return assembly == LoggerImpl.nlogAssembly || assembly == LoggerImpl.mscorlibAssembly || assembly == LoggerImpl.systemAssembly || LogManager.IsHiddenAssembly(assembly);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000183B8 File Offset: 0x000165B8
		private static bool WriteToTargetWithFilterChain(TargetWithFilterChain targetListHead, LogEventInfo logEvent, AsyncContinuation onException)
		{
			Target target = targetListHead.Target;
			FilterResult filterResult = LoggerImpl.GetFilterResult(targetListHead.FilterChain, logEvent);
			if (filterResult == FilterResult.Ignore || filterResult == FilterResult.IgnoreFinal)
			{
				if (InternalLogger.IsDebugEnabled)
				{
					InternalLogger.Debug("{0}.{1} Rejecting message because of a filter.", new object[]
					{
						logEvent.LoggerName,
						logEvent.Level
					});
				}
				return filterResult != FilterResult.IgnoreFinal;
			}
			target.WriteAsyncLogEvent(logEvent.WithContinuation(onException));
			return filterResult != FilterResult.LogFinal;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00018428 File Offset: 0x00016628
		private static FilterResult GetFilterResult(IList<Filter> filterChain, LogEventInfo logEvent)
		{
			FilterResult filterResult = FilterResult.Neutral;
			FilterResult result;
			try
			{
				for (int i = 0; i < filterChain.Count; i++)
				{
					Filter filter = filterChain[i];
					filterResult = filter.GetFilterResult(logEvent);
					if (filterResult != FilterResult.Neutral)
					{
						break;
					}
				}
				result = filterResult;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Exception during filter evaluation. Message will be ignore.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				result = FilterResult.Ignore;
			}
			return result;
		}

		// Token: 0x0400028D RID: 653
		private const int StackTraceSkipMethods = 0;

		// Token: 0x0400028E RID: 654
		private static readonly Assembly nlogAssembly = typeof(LoggerImpl).Assembly;

		// Token: 0x0400028F RID: 655
		private static readonly Assembly mscorlibAssembly = typeof(string).Assembly;

		// Token: 0x04000290 RID: 656
		private static readonly Assembly systemAssembly = typeof(Debug).Assembly;

		// Token: 0x02000124 RID: 292
		private class StackFrameWithIndex
		{
			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000184CE File Offset: 0x000166CE
			// (set) Token: 0x06000A20 RID: 2592 RVA: 0x000184D6 File Offset: 0x000166D6
			public int StackFrameIndex { get; private set; }

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06000A21 RID: 2593 RVA: 0x000184DF File Offset: 0x000166DF
			// (set) Token: 0x06000A22 RID: 2594 RVA: 0x000184E7 File Offset: 0x000166E7
			public StackFrame StackFrame { get; private set; }

			// Token: 0x06000A23 RID: 2595 RVA: 0x000184F0 File Offset: 0x000166F0
			public StackFrameWithIndex(int stackFrameIndex, StackFrame stackFrame)
			{
				this.StackFrameIndex = stackFrameIndex;
				this.StackFrame = stackFrame;
			}
		}
	}
}
