using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000036 RID: 54
	[ServiceContract(Name = "StudentConfidentialityAgreementService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentConfidentialityAgreement : IService
	{
		// Token: 0x060001B3 RID: 435
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SignedConfidentialityAgreementResp RecordSignedConfidentialityAgreement(SignedConfidentialityAgreementReq request);

		// Token: 0x060001B4 RID: 436
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LastStudentConfidentialityAgreementResp LastSignedStudentConfidentialityAgreement(LastStudentConfidentialityAgreementReq request);

		// Token: 0x060001B5 RID: 437
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsConfidentialityAgreementSigningRequiredResp IsConfidentialityAgreementSigningRequired(IsConfidentialityAgreementSigningRequiredReq request);

		// Token: 0x060001B6 RID: 438
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetStudentConfidentialityAgreementTextResp GetStudentConfidentialityAgreementText(GetStudentConfidentialityAgreementTextReq request);
	}
}
