using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001EB RID: 491
	internal static class TraceUtility
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x000464EF File Offset: 0x000446EF
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription)
		{
			TraceUtility.TraceEvent(severity, traceCode, traceDescription, null, null, null);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000464FC File Offset: 0x000446FC
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData, object source, Exception exception)
		{
			if (DiagnosticUtility.ShouldTrace(severity))
			{
				Guid activityId = DiagnosticTraceBase.ActivityId;
				string msdnTraceCode = LegacyDiagnosticTrace.GenerateMsdnTraceCode("System.IdentityModel", TraceUtility.traceCodes[traceCode]);
				DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, msdnTraceCode, traceDescription, extendedData, exception, activityId, source);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00046541 File Offset: 0x00044741
		internal static void TraceString(TraceEventType eventType, string formatString, params object[] args)
		{
			if (DiagnosticUtility.ShouldTrace(eventType))
			{
				if (args != null && args.Length != 0)
				{
					TraceUtility.TraceEvent(eventType, 786432, string.Format(CultureInfo.InvariantCulture, formatString, args));
					return;
				}
				TraceUtility.TraceEvent(eventType, 786432, formatString);
			}
		}

		// Token: 0x04000E47 RID: 3655
		private static Dictionary<int, string> traceCodes = new Dictionary<int, string>(5)
		{
			{
				786432,
				"IdentityModel"
			},
			{
				786434,
				"AuthorizationContextCreated"
			},
			{
				786435,
				"AuthorizationPolicyEvaluated"
			},
			{
				786436,
				"ServiceBindingCheck"
			},
			{
				786437,
				"ChannelBindingCheck"
			},
			{
				786438,
				"Diagnostics"
			}
		};
	}
}
