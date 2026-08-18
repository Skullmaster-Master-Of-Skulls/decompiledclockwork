using System;
using System.Diagnostics;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001EE RID: 494
	internal static class ComPlusMexChannelBuilderMexCompleteTrace
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x00038AF4 File Offset: 0x00036CF4
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceEndpointCollection serviceEndpointsRetrieved)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				int num = 0;
				ComPlusMexBuilderMetadataRetrievedEndpoint[] array = new ComPlusMexBuilderMetadataRetrievedEndpoint[serviceEndpointsRetrieved.Count];
				foreach (ServiceEndpoint endpoint in serviceEndpointsRetrieved)
				{
					array[num++] = new ComPlusMexBuilderMetadataRetrievedEndpoint(endpoint);
				}
				ComPlusMexBuilderMetadataRetrievedSchema extendedData = new ComPlusMexBuilderMetadataRetrievedSchema(array);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
