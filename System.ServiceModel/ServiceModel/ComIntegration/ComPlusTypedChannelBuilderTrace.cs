using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F0 RID: 496
	internal static class ComPlusTypedChannelBuilderTrace
	{
		// Token: 0x06000FC6 RID: 4038 RVA: 0x00038BB8 File Offset: 0x00036DB8
		public static void Trace(TraceEventType type, int v, string description, Type contractType, Binding binding)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusTypedChannelBuilderSchema extendedData = new ComPlusTypedChannelBuilderSchema(contractType.ToString(), (binding != null) ? binding.GetType().ToString() : null);
				TraceUtility.TraceEvent(type, v, SR.GetString(description), extendedData);
			}
		}
	}
}
