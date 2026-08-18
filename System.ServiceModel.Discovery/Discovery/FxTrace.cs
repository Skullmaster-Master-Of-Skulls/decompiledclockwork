using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200005C RID: 92
	internal static class FxTrace
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		public static void UpdateEventDefinitions(EventDescriptor[] eventDescriptors, ushort[] end2EndEvents)
		{
			FxTrace.EnsureEtwProviderInitialized();
			FxTrace.eventDescriptors = eventDescriptors;
			FxTrace.end2EndEvents = new SortedSet<ushort>(end2EndEvents);
			FxTrace.UpdateEnabledEventsList(FxTrace.diagnosticTrace);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E90E File Offset: 0x0000CB0E
		public static bool IsEventEnabled(int index)
		{
			return FxTrace.enabledEvents == null || FxTrace.enabledEvents[index];
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000E920 File Offset: 0x0000CB20
		public static bool ShouldTraceCritical
		{
			get
			{
				return FxTrace.shouldTraceCritical;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E927 File Offset: 0x0000CB27
		public static bool TracingEnabled
		{
			get
			{
				return FxTrace.tracingEnabled;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000E92E File Offset: 0x0000CB2E
		public static bool ShouldTraceError
		{
			get
			{
				return FxTrace.shouldTraceError;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000E935 File Offset: 0x0000CB35
		public static bool ShouldTraceInformation
		{
			get
			{
				return FxTrace.shouldTraceInformation;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000E93C File Offset: 0x0000CB3C
		public static bool ShouldTraceVerbose
		{
			get
			{
				return FxTrace.shouldTraceVerbose;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000E943 File Offset: 0x0000CB43
		public static bool ShouldTraceWarning
		{
			get
			{
				return FxTrace.shouldTraceWarning;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000E94A File Offset: 0x0000CB4A
		public static bool ShouldTraceCriticalToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceCriticalToTraceSource;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000E951 File Offset: 0x0000CB51
		public static bool ShouldTraceErrorToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceErrorToTraceSource;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000E958 File Offset: 0x0000CB58
		public static bool ShouldTraceInformationToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceInformationToTraceSource;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000E95F File Offset: 0x0000CB5F
		public static bool ShouldTraceVerboseToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceVerboseToTraceSource;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000E966 File Offset: 0x0000CB66
		public static bool ShouldTraceWarningToTraceSource
		{
			get
			{
				return FxTrace.shouldTraceWarningToTraceSource;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000E96D File Offset: 0x0000CB6D
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000E98F File Offset: 0x0000CB8F
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				FxTrace.EnsureEtwProviderInitialized();
				return FxTrace.diagnosticTrace;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E99B File Offset: 0x0000CB9B
		public static EventLogger EventLog
		{
			get
			{
				return new EventLogger(FxTrace.EventSourceName, FxTrace.Trace);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000E9AC File Offset: 0x0000CBAC
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

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
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

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000EB64 File Offset: 0x0000CD64
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

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
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

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		private static void UpdateLevel()
		{
			FxTrace.UpdateLevel(FxTrace.Trace);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
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

		// Token: 0x0400011B RID: 283
		private const string baseEventSourceName = "System.ServiceModel";

		// Token: 0x0400011C RID: 284
		private const string EventSourceVersion = "4.0.0.0";

		// Token: 0x0400011D RID: 285
		private static Guid etwProviderId;

		// Token: 0x0400011E RID: 286
		private static string eventSourceName;

		// Token: 0x0400011F RID: 287
		private static ExceptionTrace exceptionTrace;

		// Token: 0x04000120 RID: 288
		private static bool[] enabledEvents;

		// Token: 0x04000121 RID: 289
		private static SortedSet<ushort> end2EndEvents;

		// Token: 0x04000122 RID: 290
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000123 RID: 291
		private static object lockObject = new object();

		// Token: 0x04000124 RID: 292
		private static bool tracingEnabled = true;

		// Token: 0x04000125 RID: 293
		private static bool shouldTraceVerbose = true;

		// Token: 0x04000126 RID: 294
		private static bool shouldTraceInformation = true;

		// Token: 0x04000127 RID: 295
		private static bool shouldTraceWarning = true;

		// Token: 0x04000128 RID: 296
		private static bool shouldTraceError = true;

		// Token: 0x04000129 RID: 297
		private static bool shouldTraceCritical = true;

		// Token: 0x0400012A RID: 298
		private static bool shouldTraceVerboseToTraceSource = true;

		// Token: 0x0400012B RID: 299
		private static bool shouldTraceInformationToTraceSource = true;

		// Token: 0x0400012C RID: 300
		private static bool shouldTraceWarningToTraceSource = true;

		// Token: 0x0400012D RID: 301
		private static bool shouldTraceErrorToTraceSource = true;

		// Token: 0x0400012E RID: 302
		private static bool shouldTraceCriticalToTraceSource = true;

		// Token: 0x0400012F RID: 303
		private static EtwDiagnosticTrace diagnosticTrace;
	}
}
