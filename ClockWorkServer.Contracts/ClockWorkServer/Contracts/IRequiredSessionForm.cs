using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.ClockWorkServer.Contracts.Faults.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000078 RID: 120
	[ServiceContract(Name = "RequiredSessionFormService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	[XtraTimeService]
	public interface IRequiredSessionForm : IService
	{
		// Token: 0x06000376 RID: 886
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInfoPmIdForCurrentSessionResp LoadInfoPmIdForCurrentSession(LoadInfoPmIdForCurrentSessionReq Request);

		// Token: 0x06000377 RID: 887
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadInfoPmIdForSessionResp LoadInfoPmIdForSession(LoadInfoPmIdForSessionReq Request);
	}
}
