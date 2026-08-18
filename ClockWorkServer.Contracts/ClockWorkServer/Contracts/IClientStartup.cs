using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000086 RID: 134
	[ServiceContract(Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[NoSslCertificate]
	public interface IClientStartup : IService, IConnectivity
	{
		// Token: 0x060003BA RID: 954
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest updateRequiredReq);

		// Token: 0x060003BB RID: 955
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		GetClockWorkServerCertificateResp GetClockWorkServerCertificate(GetClockWorkServerCertificateReq request);
	}
}
