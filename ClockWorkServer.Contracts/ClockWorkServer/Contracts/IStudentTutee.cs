using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000096 RID: 150
	[ServiceContract(Name = "StudentTuteeService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentTutee : IService
	{
		// Token: 0x06000422 RID: 1058
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetStudentMyTutorsResp GetStudentMyTutors(GetStudentMyTutorsReq Request);

		// Token: 0x06000423 RID: 1059
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkStudentCantFindTutor(MarkStudentCantFindTutorReq Request);

		// Token: 0x06000424 RID: 1060
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void MarkStudentCantFindAvailability(MarkStudentCantFindAvailabilityReq Request);

		// Token: 0x06000425 RID: 1061
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTuteeStatusResp GetTuteeStatus(GetTuteeStatusReq Request);

		// Token: 0x06000426 RID: 1062
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsConfidentialityAgreementSigningRequiredForStudentResp IsConfidentialityAgreementSigningRequiredForStudent(IsConfidentialityAgreementSigningRequiredForStudentReq Request);

		// Token: 0x06000427 RID: 1063
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RecordConfidentialityAgreementSignedByStudent(RecordConfidentialityAgreementSignedByStudentReq Request);
	}
}
