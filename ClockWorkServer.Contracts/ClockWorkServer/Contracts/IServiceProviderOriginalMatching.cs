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
	// Token: 0x0200007B RID: 123
	[ServiceContract(Name = "ServiceProviderOriginalMatchingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProviderOriginalMatching : IService
	{
		// Token: 0x0600037A RID: 890
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAssignmentsByProviderAndAssignedDateResp LoadAssignmentsByProviderAndAssignedDate(LoadAssignmentsByProviderAndAssignedDateReq Request);
	}
}
