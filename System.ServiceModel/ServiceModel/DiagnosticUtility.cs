using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel
{
	// Token: 0x020000A0 RID: 160
	internal static class DiagnosticUtility
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00010298 File Offset: 0x0000E498
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0001029F File Offset: 0x0000E49F
		internal static SourceLevels Level
		{
			get
			{
				return DiagnosticUtility.level;
			}
			[SecurityCritical]
			set
			{
				if (DiagnosticUtility.diagnosticTrace != null)
				{
					DiagnosticUtility.DiagnosticTrace.Level = value;
					DiagnosticUtility.UpdateLevel();
				}
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private static void UpdateLevel()
		{
			DiagnosticUtility.level = DiagnosticUtility.DiagnosticTrace.Level;
			DiagnosticUtility.tracingEnabled = DiagnosticUtility.DiagnosticTrace.TracingEnabled;
			DiagnosticUtility.shouldTraceCritical = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Critical);
			DiagnosticUtility.shouldTraceError = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Error);
			DiagnosticUtility.shouldTraceInformation = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Information);
			DiagnosticUtility.shouldTraceWarning = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Warning);
			DiagnosticUtility.shouldTraceVerbose = DiagnosticUtility.DiagnosticTrace.ShouldTrace(TraceEventType.Verbose);
			DiagnosticUtility.shouldUseActivity = DiagnosticUtility.DiagnosticTrace.ShouldUseActivity;
			WaitCallbackActionItem.ShouldUseActivity = DiagnosticUtility.shouldUseActivity;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0001034D File Offset: 0x0000E54D
		internal static LegacyDiagnosticTrace DiagnosticTrace
		{
			get
			{
				return DiagnosticUtility.diagnosticTrace;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00010354 File Offset: 0x0000E554
		internal static ExceptionUtility ExceptionUtility
		{
			get
			{
				return DiagnosticUtility.exceptionUtility ?? DiagnosticUtility.GetExceptionUtility();
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010364 File Offset: 0x0000E564
		private static ExceptionUtility GetExceptionUtility()
		{
			object obj = DiagnosticUtility.lockObject;
			lock (obj)
			{
				if (DiagnosticUtility.exceptionUtility == null)
				{
					DiagnosticUtility.exceptionUtility = new ExceptionUtility("System.ServiceModel", "System.ServiceModel 4.0.0.0", DiagnosticUtility.diagnosticTrace, FxTrace.Exception);
				}
			}
			return DiagnosticUtility.exceptionUtility;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000103C8 File Offset: 0x0000E5C8
		internal static Utility Utility
		{
			get
			{
				return DiagnosticUtility.utility ?? DiagnosticUtility.GetUtility();
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000103D8 File Offset: 0x0000E5D8
		private static Utility GetUtility()
		{
			object obj = DiagnosticUtility.lockObject;
			lock (obj)
			{
				if (DiagnosticUtility.utility == null)
				{
					DiagnosticUtility.utility = new Utility(DiagnosticUtility.ExceptionUtility);
				}
			}
			return DiagnosticUtility.utility;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0001042C File Offset: 0x0000E62C
		internal static EventLogger EventLog
		{
			get
			{
				return new EventLogger("System.ServiceModel 4.0.0.0", DiagnosticUtility.diagnosticTrace);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0001043D File Offset: 0x0000E63D
		internal static EventLogger UnsafeEventLog
		{
			[SecuritySafeCritical]
			get
			{
				return EventLogger.UnsafeCreateEventLogger("System.ServiceModel 4.0.0.0", DiagnosticUtility.diagnosticTrace);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001044E File Offset: 0x0000E64E
		private static LegacyDiagnosticTrace InitializeTracing()
		{
			DiagnosticUtility.InitDiagnosticTraceImpl(TraceSourceKind.DiagnosticTraceSource, "System.ServiceModel");
			if (!DiagnosticUtility.diagnosticTrace.HaveListeners)
			{
				DiagnosticUtility.diagnosticTrace = null;
			}
			return DiagnosticUtility.diagnosticTrace;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00010472 File Offset: 0x0000E672
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void InitDiagnosticTraceImpl(TraceSourceKind sourceType, string traceSourceName)
		{
			DiagnosticUtility.diagnosticTrace = new LegacyDiagnosticTrace(sourceType, traceSourceName, "System.ServiceModel 4.0.0.0");
			DiagnosticUtility.UpdateLevel();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00010492 File Offset: 0x0000E692
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0001048A File Offset: 0x0000E68A
		internal static bool TracingEnabled
		{
			get
			{
				return DiagnosticUtility.tracingEnabled;
			}
			set
			{
				DiagnosticUtility.tracingEnabled = value;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001049C File Offset: 0x0000E69C
		internal static bool ShouldTrace(TraceEventType type)
		{
			bool result = false;
			if (DiagnosticUtility.TracingEnabled)
			{
				switch (type)
				{
				case TraceEventType.Critical:
					result = DiagnosticUtility.ShouldTraceCritical;
					break;
				case TraceEventType.Error:
					result = DiagnosticUtility.ShouldTraceError;
					break;
				case (TraceEventType)3:
					break;
				case TraceEventType.Warning:
					result = DiagnosticUtility.ShouldTraceWarning;
					break;
				default:
					if (type != TraceEventType.Information)
					{
						if (type == TraceEventType.Verbose)
						{
							result = DiagnosticUtility.ShouldTraceVerbose;
						}
					}
					else
					{
						result = DiagnosticUtility.ShouldTraceInformation;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000104FC File Offset: 0x0000E6FC
		internal static void TraceHandledException(Exception exception, TraceEventType traceEventType)
		{
			FxTrace.Exception.TraceHandledException(exception, traceEventType);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0001050A File Offset: 0x0000E70A
		internal static bool ShouldTraceCritical
		{
			get
			{
				return DiagnosticUtility.shouldTraceCritical;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00010511 File Offset: 0x0000E711
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00010518 File Offset: 0x0000E718
		internal static bool ShouldUseActivity
		{
			get
			{
				return DiagnosticUtility.shouldUseActivity;
			}
			set
			{
				DiagnosticUtility.shouldUseActivity = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00010520 File Offset: 0x0000E720
		internal static bool ShouldTraceError
		{
			get
			{
				return DiagnosticUtility.shouldTraceError;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00010527 File Offset: 0x0000E727
		internal static bool ShouldTraceWarning
		{
			get
			{
				return DiagnosticUtility.shouldTraceWarning;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001052E File Offset: 0x0000E72E
		internal static bool ShouldTraceInformation
		{
			get
			{
				return DiagnosticUtility.shouldTraceInformation;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010535 File Offset: 0x0000E735
		internal static bool ShouldTraceVerbose
		{
			get
			{
				return DiagnosticUtility.shouldTraceVerbose;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0001053C File Offset: 0x0000E73C
		[Conditional("DEBUG")]
		internal static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00010540 File Offset: 0x0000E740
		[Conditional("DEBUG")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void DebugAssert(string message)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00010544 File Offset: 0x0000E744
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Exception FailFast(string message)
		{
			try
			{
				try
				{
					DiagnosticUtility.ExceptionUtility.TraceFailFast(message);
				}
				finally
				{
					Environment.FailFast(message);
				}
			}
			catch
			{
			}
			Environment.FailFast(message);
			return null;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00010590 File Offset: 0x0000E790
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Exception InvokeFinalHandler(Exception exception)
		{
			try
			{
				try
				{
					DiagnosticUtility.ExceptionUtility.TraceFailFastException(exception);
				}
				finally
				{
					Environment.FailFast(null);
				}
			}
			catch
			{
			}
			Environment.FailFast(null);
			return null;
		}

		// Token: 0x0400091C RID: 2332
		private const string TraceSourceName = "System.ServiceModel";

		// Token: 0x0400091D RID: 2333
		internal const string EventSourceName = "System.ServiceModel 4.0.0.0";

		// Token: 0x0400091E RID: 2334
		internal const string DefaultTraceListenerName = "Default";

		// Token: 0x0400091F RID: 2335
		private static SourceLevels level = SourceLevels.Off;

		// Token: 0x04000920 RID: 2336
		private static bool tracingEnabled = false;

		// Token: 0x04000921 RID: 2337
		private static bool shouldUseActivity = false;

		// Token: 0x04000922 RID: 2338
		private static bool shouldTraceVerbose = false;

		// Token: 0x04000923 RID: 2339
		private static bool shouldTraceInformation = false;

		// Token: 0x04000924 RID: 2340
		private static bool shouldTraceWarning = false;

		// Token: 0x04000925 RID: 2341
		private static bool shouldTraceError = false;

		// Token: 0x04000926 RID: 2342
		private static bool shouldTraceCritical = false;

		// Token: 0x04000927 RID: 2343
		private static LegacyDiagnosticTrace diagnosticTrace = DiagnosticUtility.InitializeTracing();

		// Token: 0x04000928 RID: 2344
		private static object lockObject = new object();

		// Token: 0x04000929 RID: 2345
		private static ExceptionUtility exceptionUtility = null;

		// Token: 0x0400092A RID: 2346
		private static Utility utility = null;
	}
}
