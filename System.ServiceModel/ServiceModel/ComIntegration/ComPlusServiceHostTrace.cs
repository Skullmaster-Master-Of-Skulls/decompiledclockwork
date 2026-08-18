using System;
using System.Diagnostics;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Web.Services.Description;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E6 RID: 486
	internal static class ComPlusServiceHostTrace
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x000382B8 File Offset: 0x000364B8
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusServiceHostSchema extendedData = new ComPlusServiceHostSchema(info.AppID, info.Clsid);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000382F0 File Offset: 0x000364F0
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, ContractDescription contract)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				XmlQualifiedName contractQname = new XmlQualifiedName(contract.Name, contract.Namespace);
				ComPlusServiceHostCreatedServiceContractSchema extendedData = new ComPlusServiceHostCreatedServiceContractSchema(info.AppID, info.Clsid, contractQname, contract.ContractType.ToString());
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00038348 File Offset: 0x00036548
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, System.ServiceModel.Description.ServiceDescription service)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				WsdlExporter wsdlExporter = new WsdlExporter();
				string ns = "http://tempuri.org/";
				XmlQualifiedName wsdlServiceQName = new XmlQualifiedName("comPlusService", ns);
				wsdlExporter.ExportEndpoints(service.Endpoints, wsdlServiceQName);
				System.Web.Services.Description.ServiceDescription wsdl = wsdlExporter.GeneratedWsdlDocuments[ns];
				ComPlusServiceHostStartedServiceDetailsSchema extendedData = new ComPlusServiceHostStartedServiceDetailsSchema(info.AppID, info.Clsid, wsdl);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000383B4 File Offset: 0x000365B4
		public static void Trace(TraceEventType type, int traceCode, string description, ServiceInfo info, ServiceEndpointCollection endpointCollection)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				foreach (ServiceEndpoint serviceEndpoint in endpointCollection)
				{
					ComPlusServiceHostCreatedServiceEndpointSchema extendedData = new ComPlusServiceHostCreatedServiceEndpointSchema(info.AppID, info.Clsid, serviceEndpoint.Contract.Name, serviceEndpoint.Address.Uri, serviceEndpoint.Binding.Name);
					TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
				}
			}
		}
	}
}
