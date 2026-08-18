using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F1 RID: 497
	internal static class ComPlusChannelCreatedTrace
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00038BFC File Offset: 0x00036DFC
		public static void Trace(TraceEventType type, int traceCode, string description, Uri address, Type contractType)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusChannelCreatedSchema extendedData = new ComPlusChannelCreatedSchema(address, (contractType != null) ? contractType.ToString() : null);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
