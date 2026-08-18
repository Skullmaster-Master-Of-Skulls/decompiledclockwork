using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000065 RID: 101
	[ServiceContract(Name = "LookupInstructorManagementService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILookupInstructorManagement : IService
	{
		// Token: 0x060002FC RID: 764
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupInstructorsForManagementResp LoadLookupInstructorsForManagement(LoadLookupInstructorsForManagementReq Request);

		// Token: 0x060002FD RID: 765
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteInstructorResp DeleteInstructor(DeleteInstructorReq Request);

		// Token: 0x060002FE RID: 766
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MergeInstructorsResp MergeInstructors(MergeInstructorsReq Request);
	}
}
