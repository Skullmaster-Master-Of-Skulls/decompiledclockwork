using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.ServiceModel
{
	// Token: 0x0200017D RID: 381
	internal static class FxTrace
	{
		// Token: 0x06000B1E RID: 2846 RVA: 0x00028D59 File Offset: 0x00026F59
		[SecuritySafeCritical]
		public static void UpdateEventDefinitions(EventDescriptor[] eventDescriptors, ushort[] end2EndEvents)
		{
			FxTrace.EnsureEtwProviderInitialized();
			FxTrace.eventDescriptors = eventDescriptors;
			FxTrace.end2EndEvents = new SortedSet<ushort>(end2EndEvents);
			FxTrace.UpdateEnabledEventsList(FxTrace.diagnosticTrace);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00028D7B File Offset: 0x00026F7B
		public static bool IsEventEnabled(int index)
		{
			return FxTrace.enabledEvents == null || FxTrace.enabledEvents[index];
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00028D8D File Offset: 0x00026F8D
		public static bool ShouldTraceCritical
		{
			get
			{
				return FxTrace.shouldTraceCritical;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00028D94 File Offset: 0x00026F94
		public static bool TracingEnabled
		{
			get
			{
				return FxTrace.tracingEnabled;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00028D9B File Offset: 0x00026F9B
		public static bool ShouldTraceError
		{
			get
			{
				return FxTrace.shouldTraceError;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00028DA2 File Offset: 0x00026FA2
		public static bool ShouldTraceInformation
		{
			get
			{
				return FxTrace.shouldTraceInformation;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00028DA9 File Offset: 0x00026FA9
		public static bool ShouldTraceVerbose
		{
			get
			{
				return FxTrace.shouldTraceVerbose;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00028DB0 File Offset: 0x00026FB0
		public static bool ShouldTraceWarning
		{
			get
			{
				return FxTrace.shouldTraceWarning;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00028DB7 File Offset: 0x00026FB7
		public static bool ShouldTraceCriticalToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceCriticalToTraceSource;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00028DBE File Offset: 0x00026FBE
		public static bool ShouldTraceErrorToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceErrorToTraceSource;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00028DC5 File Offset: 0x00026FC5
		public static bool ShouldTraceInformationToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceInformationToTraceSource;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00028DCC File Offset: 0x00026FCC
		public static bool ShouldTraceVerboseToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceVerboseToTraceSource;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00028DD3 File Offset: 0x00026FD3
		public static bool ShouldTraceWarningToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceWarningToTraceSource;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00028DDA File Offset: 0x00026FDA
		public static ExceptionTrace Exception
		{
			get
			{
				if (FxTrace.exceptionTrace == null)
				{
					FxTrace.exceptionTrace = new ExceptionTrace(FxTrace.EventSourceName, FxTrace.Trace);
				}
				return FxTrace.exceptionTrace;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00028DFC File Offset: 0x00026FFC
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				FxTrace.EnsureEtwProviderInitialized();
				return FxTrace.diagnosticTrace;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00028E08 File Offset: 0x00027008
		public static EventLogger EventLog
		{
			get
			{
				return new EventLogger(FxTrace.EventSourceName, FxTrace.Trace);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00028E19 File Offset: 0x00027019
		private static string EventSourceName
		{
			get
			{
				if (FxTrace.eventSourceName == null)
				{
					FxTrace.eventSourceName = "System.ServiceModel" + " " + "4.0.0.0";
				}
				return FxTrace.eventSourceName;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00028E40 File Offset: 0x00027040
		[SecuritySafeCritical]
		private static void UpdateEnabledEventsList(EtwDiagnosticTrace trace)
		{
			object obj = FxTrace.lockObject;
			lock (obj)
			{
				int num = -1;
				EventDescriptor[] array = FxTrace.eventDescriptors;
				if (array != null)
				{
					if (FxTrace.enabledEvents == null)
					{
						FxTrace.enabledEvents = new bool[array.Length];
					}
					for (int i = 0; i < FxTrace.enabledEvents.Length; i++)
					{
						EventDescriptor eventDescriptor = array[i];
						bool flag2 = FxTrace.Trace.IsEtwEventEnabled(ref eventDescriptor);
						FxTrace.enabledEvents[i] = flag2;
						if (flag2 && !FxTrace.Trace.IsEnd2EndActivityTracingEnabled && FxTrace.end2EndEvents.Contains((ushort)eventDescriptor.EventId))
						{
							FxTrace.Trace.SetEnd2EndActivityTracingEnabled(true);
						}
						if (flag2 && num < (int)eventDescriptor.Level)
						{
							num = (int)eventDescriptor.Level;
						}
					}
					FxTrace.shouldTraceCritical = (FxTrace.shouldTraceCriticalToTraceSource || (trace.ShouldTraceToEtw(TraceEventLevel.Critical) && num >= 1));
					FxTrace.shouldTraceError = (FxTrace.shouldTraceErrorToTraceSource || (trace.ShouldTraceToEtw(TraceEventLevel.Error) && num >= 2));
					FxTrace.shouldTraceWarning = (FxTrace.shouldTraceWarningToTraceSource || (trace.ShouldTraceToEtw(TraceEventLevel.Warning) && num >= 3));
					FxTrace.shouldTraceInformation = (FxTrace.shouldTraceInformationToTraceSource || (trace.ShouldTraceToEtw(TraceEventLevel.Informational) && num >= 4));
					FxTrace.shouldTraceVerbose = (FxTrace.shouldTraceVerboseToTraceSource || (trace.ShouldTraceToEtw(TraceEventLevel.Verbose) && num >= 5));
				}
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00028FD0 File Offset: 0x000271D0
		[SecuritySafeCritical]
		private static EtwDiagnosticTrace InitializeTracing()
		{
			FxTrace.etwProviderId = EtwDiagnosticTrace.DefaultEtwProviderId;
			EtwDiagnosticTrace etwDiagnosticTrace = new EtwDiagnosticTrace("System.ServiceModel", FxTrace.etwProviderId);
			if (etwDiagnosticTrace.EtwProvider != null)
			{
				EtwDiagnosticTrace etwDiagnosticTrace2 = etwDiagnosticTrace;
				etwDiagnosticTrace2.RefreshState = (Action)Delegate.Combine(etwDiagnosticTrace2.RefreshState, new Action(delegate()
				{
					FxTrace.UpdateLevel();
				}));
			}
			FxTrace.UpdateLevel(etwDiagnosticTrace);
			return etwDiagnosticTrace;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002903C File Offset: 0x0002723C
		private static void UpdateLevel(EtwDiagnosticTrace trace)
		{
			if (trace == null)
			{
				return;
			}
			FxTrace.tracingEnabled = trace.TracingEnabled;
			FxTrace.shouldTraceCriticalToTraceSource = trace.ShouldTraceToTraceSource(TraceEventLevel.Critical);
			FxTrace.shouldTraceErrorToTraceSource = trace.ShouldTraceToTraceSource(TraceEventLevel.Error);
			FxTrace.shouldTraceWarningToTraceSource = trace.ShouldTraceToTraceSource(TraceEventLevel.Warning);
			FxTrace.shouldTraceInformationToTraceSource = trace.ShouldTraceToTraceSource(TraceEventLevel.Informational);
			FxTrace.shouldTraceVerboseToTraceSource = trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose);
			FxTrace.shouldTraceCritical = (FxTrace.shouldTraceCriticalToTraceSource || trace.ShouldTraceToEtw(TraceEventLevel.Critical));
			FxTrace.shouldTraceError = (FxTrace.shouldTraceErrorToTraceSource || trace.ShouldTraceToEtw(TraceEventLevel.Error));
			FxTrace.shouldTraceWarning = (FxTrace.shouldTraceWarningToTraceSource || trace.ShouldTraceToEtw(TraceEventLevel.Warning));
			FxTrace.shouldTraceInformation = (FxTrace.shouldTraceInformationToTraceSource || trace.ShouldTraceToEtw(TraceEventLevel.Informational));
			FxTrace.shouldTraceVerbose = (FxTrace.shouldTraceVerboseToTraceSource || trace.ShouldTraceToEtw(TraceEventLevel.Verbose));
			FxTrace.UpdateEnabledEventsList(trace);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00029108 File Offset: 0x00027308
		private static void UpdateLevel()
		{
			FxTrace.UpdateLevel(FxTrace.Trace);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00029114 File Offset: 0x00027314
		private static void EnsureEtwProviderInitialized()
		{
			if (FxTrace.diagnosticTrace == null)
			{
				object obj = FxTrace.lockObject;
				lock (obj)
				{
					if (FxTrace.diagnosticTrace == null)
					{
						FxTrace.diagnosticTrace = FxTrace.InitializeTracing();
					}
				}
			}
		}

		// Token: 0x04000BF6 RID: 3062
		private const string baseEventSourceName = "System.ServiceModel";

		// Token: 0x04000BF7 RID: 3063
		private const string EventSourceVersion = "4.0.0.0";

		// Token: 0x04000BF8 RID: 3064
		private static Guid etwProviderId;

		// Token: 0x04000BF9 RID: 3065
		private static string eventSourceName;

		// Token: 0x04000BFA RID: 3066
		private static ExceptionTrace exceptionTrace;

		// Token: 0x04000BFB RID: 3067
		private static bool[] enabledEvents;

		// Token: 0x04000BFC RID: 3068
		private static SortedSet<ushort> end2EndEvents;

		// Token: 0x04000BFD RID: 3069
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000BFE RID: 3070
		private static object lockObject = new object();

		// Token: 0x04000BFF RID: 3071
		private static bool tracingEnabled = true;

		// Token: 0x04000C00 RID: 3072
		private static bool shouldTraceVerbose = true;

		// Token: 0x04000C01 RID: 3073
		private static bool shouldTraceInformation = true;

		// Token: 0x04000C02 RID: 3074
		private static bool shouldTraceWarning = true;

		// Token: 0x04000C03 RID: 3075
		private static bool shouldTraceError = true;

		// Token: 0x04000C04 RID: 3076
		private static bool shouldTraceCritical = true;

		// Token: 0x04000C05 RID: 3077
		private static bool shouldTraceVerboseToTraceSource = true;

		// Token: 0x04000C06 RID: 3078
		private static bool shouldTraceInformationToTraceSource = true;

		// Token: 0x04000C07 RID: 3079
		private static bool shouldTraceWarningToTraceSource = true;

		// Token: 0x04000C08 RID: 3080
		private static bool shouldTraceErrorToTraceSource = true;

		// Token: 0x04000C09 RID: 3081
		private static bool shouldTraceCriticalToTraceSource = true;

		// Token: 0x04000C0A RID: 3082
		private static EtwDiagnosticTrace diagnosticTrace;
	}
}
