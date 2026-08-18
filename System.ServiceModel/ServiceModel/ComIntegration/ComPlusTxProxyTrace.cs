using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F3 RID: 499
	internal static class ComPlusTxProxyTrace
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00038C94 File Offset: 0x00036E94
		public static void Trace(TraceEventType type, int traceCode, string description, Guid appid, Guid clsid, Guid transactionID, int instanceID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusTxProxySchema extendedData = new ComPlusTxProxySchema(appid, clsid, transactionID, instanceID);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
