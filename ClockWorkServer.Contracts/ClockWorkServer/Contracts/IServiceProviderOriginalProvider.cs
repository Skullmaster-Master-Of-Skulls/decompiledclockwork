using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.ClockWorkServer.Contracts.Faults.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200007C RID: 124
	[ServiceContract(Name = "ServiceProviderOriginalProviderService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProviderOriginalProvider : IService
	{
		// Token: 0x0600037B RID: 891
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request);

		// Token: 0x0600037C RID: 892
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderBaseByIdResp LoadProviderBaseById(LoadProviderBaseByIdReq Request);
	}
}
