using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.IdentityModel.Diagnostics.Application
{
	// Token: 0x020001ED RID: 493
	internal static class FxTrace
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x000469C3 File Offset: 0x00044BC3
		[SecuritySafeCritical]
		public static void UpdateEventDefinitions(EventDescriptor[] eventDescriptors, ushort[] end2EndEvents)
		{
			FxTrace.EnsureEtwProviderInitialized();
			FxTrace.eventDescriptors = eventDescriptors;
			FxTrace.end2EndEvents = new SortedSet<ushort>(end2EndEvents);
			FxTrace.UpdateEnabledEventsList(FxTrace.diagnosticTrace);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000469E5 File Offset: 0x00044BE5
		public static bool IsEventEnabled(int index)
		{
			return FxTrace.enabledEvents == null || FxTrace.enabledEvents[index];
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000469F7 File Offset: 0x00044BF7
		public static bool ShouldTraceCritical
		{
			get
			{
				return FxTrace.shouldTraceCritical;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000469FE File Offset: 0x00044BFE
		public static bool TracingEnabled
		{
			get
			{
				return FxTrace.tracingEnabled;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00046A05 File Offset: 0x00044C05
		public static bool ShouldTraceError
		{
			get
			{
				return FxTrace.shouldTraceError;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00046A0C File Offset: 0x00044C0C
		public static bool ShouldTraceInformation
		{
			get
			{
				return FxTrace.shouldTraceInformation;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00046A13 File Offset: 0x00044C13
		public static bool ShouldTraceVerbose
		{
			get
			{
				return FxTrace.shouldTraceVerbose;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00046A1A File Offset: 0x00044C1A
		public static bool ShouldTraceWarning
		{
			get
			{
				return FxTrace.shouldTraceWarning;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00046A21 File Offset: 0x00044C21
		public static bool ShouldTraceCriticalToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceCriticalToTraceSource;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00046A28 File Offset: 0x00044C28
		public static bool ShouldTraceErrorToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceErrorToTraceSource;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00046A2F File Offset: 0x00044C2F
		public static bool ShouldTraceInformationToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceInformationToTraceSource;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00046A36 File Offset: 0x00044C36
		public static bool ShouldTraceVerboseToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceVerboseToTraceSource;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00046A3D File Offset: 0x00044C3D
		public static bool ShouldTraceWarningToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceWarningToTraceSource;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00046A44 File Offset: 0x00044C44
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

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00046A66 File Offset: 0x00044C66
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				FxTrace.EnsureEtwProviderInitialized();
				return FxTrace.diagnosticTrace;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00046A72 File Offset: 0x00044C72
		public static EventLogger EventLog
		{
			get
			{
				return new EventLogger(FxTrace.EventSourceName, FxTrace.Trace);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00046A83 File Offset: 0x00044C83
		private static string EventSourceName
		{
			get
			{
				if (FxTrace.eventSourceName == null)
				{
					FxTrace.eventSourceName = "System.IdentityModel" + " " + "4.0.0.0";
				}
				return FxTrace.eventSourceName;
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00046AAC File Offset: 0x00044CAC
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

		// Token: 0x06001083 RID: 4227 RVA: 0x00046C3C File Offset: 0x00044E3C
		[SecuritySafeCritical]
		private static EtwDiagnosticTrace InitializeTracing()
		{
			FxTrace.etwProviderId = EtwDiagnosticTrace.DefaultEtwProviderId;
			EtwDiagnosticTrace etwDiagnosticTrace = new EtwDiagnosticTrace("System.IdentityModel", FxTrace.etwProviderId);
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

		// Token: 0x06001084 RID: 4228 RVA: 0x00046CA8 File Offset: 0x00044EA8
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

		// Token: 0x06001085 RID: 4229 RVA: 0x00046D74 File Offset: 0x00044F74
		private static void UpdateLevel()
		{
			FxTrace.UpdateLevel(FxTrace.Trace);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00046D80 File Offset: 0x00044F80
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

		// Token: 0x04000E4D RID: 3661
		private const string baseEventSourceName = "System.IdentityModel";

		// Token: 0x04000E4E RID: 3662
		private const string EventSourceVersion = "4.0.0.0";

		// Token: 0x04000E4F RID: 3663
		private static Guid etwProviderId;

		// Token: 0x04000E50 RID: 3664
		private static string eventSourceName;

		// Token: 0x04000E51 RID: 3665
		private static ExceptionTrace exceptionTrace;

		// Token: 0x04000E52 RID: 3666
		private static bool[] enabledEvents;

		// Token: 0x04000E53 RID: 3667
		private static SortedSet<ushort> end2EndEvents;

		// Token: 0x04000E54 RID: 3668
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000E55 RID: 3669
		private static object lockObject = new object();

		// Token: 0x04000E56 RID: 3670
		private static bool tracingEnabled = true;

		// Token: 0x04000E57 RID: 3671
		private static bool shouldTraceVerbose = true;

		// Token: 0x04000E58 RID: 3672
		private static bool shouldTraceInformation = true;

		// Token: 0x04000E59 RID: 3673
		private static bool shouldTraceWarning = true;

		// Token: 0x04000E5A RID: 3674
		private static bool shouldTraceError = true;

		// Token: 0x04000E5B RID: 3675
		private static bool shouldTraceCritical = true;

		// Token: 0x04000E5C RID: 3676
		private static bool shouldTraceVerboseToTraceSource = true;

		// Token: 0x04000E5D RID: 3677
		private static bool shouldTraceInformationToTraceSource = true;

		// Token: 0x04000E5E RID: 3678
		private static bool shouldTraceWarningToTraceSource = true;

		// Token: 0x04000E5F RID: 3679
		private static bool shouldTraceErrorToTraceSource = true;

		// Token: 0x04000E60 RID: 3680
		private static bool shouldTraceCriticalToTraceSource = true;

		// Token: 0x04000E61 RID: 3681
		private static EtwDiagnosticTrace diagnosticTrace;
	}
}
