using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.Runtime
{
	// Token: 0x02000039 RID: 57
	internal class TraceCore
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000023D6 File Offset: 0x000005D6
		private TraceCore()
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000085A8 File Offset: 0x000067A8
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TraceCore.resourceManager == null)
				{
					TraceCore.resourceManager = new ResourceManager("System.Runtime.TraceCore", typeof(TraceCore).Assembly);
				}
				return TraceCore.resourceManager;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000085D4 File Offset: 0x000067D4
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000085DB File Offset: 0x000067DB
		internal static CultureInfo Culture
		{
			get
			{
				return TraceCore.resourceCulture;
			}
			set
			{
				TraceCore.resourceCulture = value;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000085E3 File Offset: 0x000067E3
		internal static bool AppDomainUnloadIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 0);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000085F8 File Offset: 0x000067F8
		internal static void AppDomainUnload(EtwDiagnosticTrace trace, string appdomainName, string processName, string processId)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 0))
			{
				TraceCore.WriteEtwEvent(trace, 0, null, appdomainName, processName, processId, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("AppDomainUnload", TraceCore.Culture), new object[]
				{
					appdomainName,
					processName,
					processId
				});
				TraceCore.WriteTraceSource(trace, 0, description, serializedPayload);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000866E File Offset: 0x0000686E
		internal static bool HandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 1);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008684 File Offset: 0x00006884
		internal static void HandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 1))
			{
				TraceCore.WriteEtwEvent(trace, 1, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledException", TraceCore.Culture), new object[]
				{
					param0
				});
				TraceCore.WriteTraceSource(trace, 1, description, serializedPayload);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000086F7 File Offset: 0x000068F7
		internal static bool ShipAssertExceptionMessageIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 2);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000870C File Offset: 0x0000690C
		internal static void ShipAssertExceptionMessage(EtwDiagnosticTrace trace, string param0)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 2))
			{
				TraceCore.WriteEtwEvent(trace, 2, null, param0, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ShipAssertExceptionMessage", TraceCore.Culture), new object[]
				{
					param0
				});
				TraceCore.WriteTraceSource(trace, 2, description, serializedPayload);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008778 File Offset: 0x00006978
		internal static bool ThrowingExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 3);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000878C File Offset: 0x0000698C
		internal static void ThrowingException(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 3))
			{
				TraceCore.WriteEtwEvent(trace, 3, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ThrowingException", TraceCore.Culture), new object[]
				{
					param0,
					param1
				});
				TraceCore.WriteTraceSource(trace, 3, description, serializedPayload);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008804 File Offset: 0x00006A04
		internal static bool UnhandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Critical) || TraceCore.IsEtwEventEnabled(trace, 4);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008818 File Offset: 0x00006A18
		internal static void UnhandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 4))
			{
				TraceCore.WriteEtwEvent(trace, 4, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Critical))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("UnhandledException", TraceCore.Culture), new object[]
				{
					param0
				});
				TraceCore.WriteTraceSource(trace, 4, description, serializedPayload);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000888B File Offset: 0x00006A8B
		internal static bool TraceCodeEventLogCriticalIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Critical) || TraceCore.IsEtwEventEnabled(trace, 5);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000088A0 File Offset: 0x00006AA0
		internal static void TraceCodeEventLogCritical(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 5))
			{
				TraceCore.WriteEtwEvent(trace, 5, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Critical))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogCritical", TraceCore.Culture), new object[0]);
				TraceCore.WriteTraceSource(trace, 5, description, serializedPayload);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000890E File Offset: 0x00006B0E
		internal static bool TraceCodeEventLogErrorIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 6);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008924 File Offset: 0x00006B24
		internal static void TraceCodeEventLogError(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 6))
			{
				TraceCore.WriteEtwEvent(trace, 6, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogError", TraceCore.Culture), new object[0]);
				TraceCore.WriteTraceSource(trace, 6, description, serializedPayload);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008992 File Offset: 0x00006B92
		internal static bool TraceCodeEventLogInfoIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 7);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000089A8 File Offset: 0x00006BA8
		internal static void TraceCodeEventLogInfo(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 7))
			{
				TraceCore.WriteEtwEvent(trace, 7, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogInfo", TraceCore.Culture), new object[0]);
				TraceCore.WriteTraceSource(trace, 7, description, serializedPayload);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008A16 File Offset: 0x00006C16
		internal static bool TraceCodeEventLogVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 8);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008A2C File Offset: 0x00006C2C
		internal static void TraceCodeEventLogVerbose(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 8))
			{
				TraceCore.WriteEtwEvent(trace, 8, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogVerbose", TraceCore.Culture), new object[0]);
				TraceCore.WriteTraceSource(trace, 8, description, serializedPayload);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008A9A File Offset: 0x00006C9A
		internal static bool TraceCodeEventLogWarningIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 9);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008AB0 File Offset: 0x00006CB0
		internal static void TraceCodeEventLogWarning(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 9))
			{
				TraceCore.WriteEtwEvent(trace, 9, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogWarning", TraceCore.Culture), new object[0]);
				TraceCore.WriteTraceSource(trace, 9, description, serializedPayload);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008B21 File Offset: 0x00006D21
		internal static bool HandledExceptionWarningIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 10);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008B38 File Offset: 0x00006D38
		internal static void HandledExceptionWarning(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 10))
			{
				TraceCore.WriteEtwEvent(trace, 10, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionWarning", TraceCore.Culture), new object[]
				{
					param0
				});
				TraceCore.WriteTraceSource(trace, 10, description, serializedPayload);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008BAE File Offset: 0x00006DAE
		internal static bool BufferPoolAllocationIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 11);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008BB8 File Offset: 0x00006DB8
		internal static void BufferPoolAllocation(EtwDiagnosticTrace trace, int Size)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 11))
			{
				TraceCore.WriteEtwEvent(trace, 11, null, Size, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008BEB File Offset: 0x00006DEB
		internal static bool BufferPoolChangeQuotaIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 12);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008BF8 File Offset: 0x00006DF8
		internal static void BufferPoolChangeQuota(EtwDiagnosticTrace trace, int PoolSize, int Delta)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 12))
			{
				TraceCore.WriteEtwEvent(trace, 12, null, PoolSize, Delta, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008C2C File Offset: 0x00006E2C
		internal static bool ActionItemScheduledIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 13);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008C38 File Offset: 0x00006E38
		internal static void ActionItemScheduled(EtwDiagnosticTrace trace, EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 13))
			{
				TraceCore.WriteEtwEvent(trace, 13, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008C6A File Offset: 0x00006E6A
		internal static bool ActionItemCallbackInvokedIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 14);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008C74 File Offset: 0x00006E74
		internal static void ActionItemCallbackInvoked(EtwDiagnosticTrace trace, EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 14))
			{
				TraceCore.WriteEtwEvent(trace, 14, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008CA6 File Offset: 0x00006EA6
		internal static bool HandledExceptionErrorIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 15);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008CBC File Offset: 0x00006EBC
		internal static void HandledExceptionError(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 15))
			{
				TraceCore.WriteEtwEvent(trace, 15, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionError", TraceCore.Culture), new object[]
				{
					param0
				});
				TraceCore.WriteTraceSource(trace, 15, description, serializedPayload);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008D32 File Offset: 0x00006F32
		internal static bool HandledExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 16);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008D48 File Offset: 0x00006F48
		internal static void HandledExceptionVerbose(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 16))
			{
				TraceCore.WriteEtwEvent(trace, 16, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionVerbose", TraceCore.Culture), new object[]
				{
					param0
				});
				TraceCore.WriteTraceSource(trace, 16, description, serializedPayload);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008DBE File Offset: 0x00006FBE
		internal static bool EtwUnhandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 17);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008DC8 File Offset: 0x00006FC8
		internal static void EtwUnhandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 17))
			{
				TraceCore.WriteEtwEvent(trace, 17, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008E02 File Offset: 0x00007002
		internal static bool ThrowingEtwExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 18);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008E0C File Offset: 0x0000700C
		internal static void ThrowingEtwException(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 18))
			{
				TraceCore.WriteEtwEvent(trace, 18, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008E47 File Offset: 0x00007047
		internal static bool ThrowingEtwExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 19);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008E54 File Offset: 0x00007054
		internal static void ThrowingEtwExceptionVerbose(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 19))
			{
				TraceCore.WriteEtwEvent(trace, 19, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008E8F File Offset: 0x0000708F
		internal static bool ThrowingExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 20);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008EA4 File Offset: 0x000070A4
		internal static void ThrowingExceptionVerbose(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 20))
			{
				TraceCore.WriteEtwEvent(trace, 20, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string description = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ThrowingExceptionVerbose", TraceCore.Culture), new object[]
				{
					param0,
					param1
				});
				TraceCore.WriteTraceSource(trace, 20, description, serializedPayload);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008F20 File Offset: 0x00007120
		[SecuritySafeCritical]
		private static void CreateEventDescriptors()
		{
			TraceCore.eventDescriptors = new EventDescriptor[]
			{
				new EventDescriptor(57393, 0, 19, 4, 0, 0, 1152921504606912512L),
				new EventDescriptor(57394, 0, 18, 4, 0, 0, 2305843009213759488L),
				new EventDescriptor(57395, 0, 18, 2, 0, 0, 2305843009213759488L),
				new EventDescriptor(57396, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(57397, 0, 17, 1, 0, 0, 4611686018427453440L),
				new EventDescriptor(57399, 0, 19, 1, 0, 0, 1152921504606912512L),
				new EventDescriptor(57400, 0, 19, 2, 0, 0, 1152921504606912512L),
				new EventDescriptor(57401, 0, 19, 4, 0, 0, 1152921504606912512L),
				new EventDescriptor(57402, 0, 19, 5, 0, 0, 1152921504606912512L),
				new EventDescriptor(57403, 0, 19, 3, 0, 0, 1152921504606912512L),
				new EventDescriptor(57404, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(131, 0, 19, 5, 12, 2509, 1152921504606912512L),
				new EventDescriptor(132, 0, 19, 5, 13, 2509, 1152921504606912512L),
				new EventDescriptor(133, 0, 19, 5, 1, 2593, 1152921504608944128L),
				new EventDescriptor(134, 0, 19, 5, 2, 2593, 1152921504608944128L),
				new EventDescriptor(57405, 0, 17, 2, 0, 0, 4611686018427453440L),
				new EventDescriptor(57406, 0, 18, 5, 0, 0, 2305843009213759488L),
				new EventDescriptor(57408, 0, 17, 1, 0, 0, 4611686018427453440L),
				new EventDescriptor(57410, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(57409, 0, 18, 5, 0, 0, 2305843009213759488L),
				new EventDescriptor(57407, 0, 18, 5, 0, 0, 2305843009213759488L)
			};
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000091F8 File Offset: 0x000073F8
		private static void EnsureEventDescriptors()
		{
			if (TraceCore.eventDescriptorsCreated)
			{
				return;
			}
			lock (TraceCore.syncLock)
			{
				if (!TraceCore.eventDescriptorsCreated)
				{
					TraceCore.CreateEventDescriptors();
					TraceCore.eventDescriptorsCreated = true;
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009250 File Offset: 0x00007450
		[SecuritySafeCritical]
		private static bool IsEtwEventEnabled(EtwDiagnosticTrace trace, int eventIndex)
		{
			if (trace.IsEtwProviderEnabled)
			{
				TraceCore.EnsureEventDescriptors();
				return trace.IsEtwEventEnabled(ref TraceCore.eventDescriptors[eventIndex], false);
			}
			return false;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009273 File Offset: 0x00007473
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009298 File Offset: 0x00007498
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000092BB File Offset: 0x000074BB
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000092DC File Offset: 0x000074DC
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, int eventParam1, string eventParam2)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2
			});
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000930E File Offset: 0x0000750E
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, int eventParam1, int eventParam2, string eventParam3)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3
			});
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000934A File Offset: 0x0000754A
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009369 File Offset: 0x00007569
		[SecuritySafeCritical]
		private static void WriteTraceSource(EtwDiagnosticTrace trace, int eventIndex, string description, TracePayload payload)
		{
			TraceCore.EnsureEventDescriptors();
			trace.WriteTraceSource(ref TraceCore.eventDescriptors[eventIndex], description, payload);
		}

		// Token: 0x040000E4 RID: 228
		private static ResourceManager resourceManager;

		// Token: 0x040000E5 RID: 229
		private static CultureInfo resourceCulture;

		// Token: 0x040000E6 RID: 230
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x040000E7 RID: 231
		private static object syncLock = new object();

		// Token: 0x040000E8 RID: 232
		private static volatile bool eventDescriptorsCreated;
	}
}
