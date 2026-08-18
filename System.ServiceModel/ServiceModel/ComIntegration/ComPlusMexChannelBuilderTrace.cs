using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001EF RID: 495
	internal static class ComPlusMexChannelBuilderTrace
	{
		// Token: 0x06000FC5 RID: 4037 RVA: 0x00038B70 File Offset: 0x00036D70
		public static void Trace(TraceEventType type, int traceCode, string description, ContractDescription contract, Binding binding, string address)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusMexChannelBuilderSchema extendedData = new ComPlusMexChannelBuilderSchema(contract.Name, contract.Namespace, binding.Name, binding.Namespace, address);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
