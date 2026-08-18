using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000075 RID: 117
	[ServiceContract(Name = "StudentCommonInfoService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentCommonInfo : IService
	{
		// Token: 0x0600036E RID: 878
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentCommonInfoResp LoadStudentCommonInfo(LoadStudentCommonInfoReq Request);

		// Token: 0x0600036F RID: 879
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentByEmailAddressResp LoadStudentByEmailAddress(LoadStudentByEmailAddressReq Request);

		// Token: 0x06000370 RID: 880
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMyStudentsResp LoadMyStudents(LoadMyStudentsReq Request);

		// Token: 0x06000371 RID: 881
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsWithCommonInfoResp LoadStudentsWithCommonInfo(LoadStudentsWithCommonInfoReq Request);
	}
}
