using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000089 RID: 137
	[ServiceContract(Name = "StartupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStartup : IService
	{
		// Token: 0x060003BE RID: 958
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCacheClusterFullResp LoadCacheClusterFull(LoadCacheClusterFullReq Request);
	}
}
