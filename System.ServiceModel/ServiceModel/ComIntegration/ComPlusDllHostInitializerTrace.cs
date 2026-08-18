using System;
using System.Diagnostics;
using System.ServiceModel.Configuration;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E7 RID: 487
	internal static class ComPlusDllHostInitializerTrace
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00038440 File Offset: 0x00036640
		public static void Trace(TraceEventType type, int traceCode, string description, Guid appid)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusDllHostInitializerSchema extendedData = new ComPlusDllHostInitializerSchema(appid);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003846C File Offset: 0x0003666C
		public static void Trace(TraceEventType type, int traceCode, string description, Guid appid, Guid clsid, ServiceElement service)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				foreach (object obj in service.Endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj;
					ComPlusDllHostInitializerAddingHostSchema extendedData = new ComPlusDllHostInitializerAddingHostSchema(appid, clsid, service.BehaviorConfiguration, service.Name, serviceEndpointElement.Address.ToString(), serviceEndpointElement.BindingConfiguration, serviceEndpointElement.BindingName, serviceEndpointElement.BindingNamespace, serviceEndpointElement.Binding, serviceEndpointElement.Contract);
					TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
				}
			}
		}
	}
}
