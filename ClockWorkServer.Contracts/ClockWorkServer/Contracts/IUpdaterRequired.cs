using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200009B RID: 155
	[ServiceContract(Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IUpdaterRequired : IService, IConnectivity
	{
		// Token: 0x06000457 RID: 1111
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		UpdateRequiredResponse IsUpdateRequired(UpdateRequiredRequest updateRequiredReq);
	}
}
