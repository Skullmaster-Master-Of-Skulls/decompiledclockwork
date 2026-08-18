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
	// Token: 0x02000097 RID: 151
	[ServiceContract(Name = "TutorService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITutor : IService
	{
		// Token: 0x06000428 RID: 1064
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SearchForTutorsResp SearchForTutors(SearchForTutorsReq Request);

		// Token: 0x06000429 RID: 1065
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTutorByPersonIdResp LoadTutorByPersonId(LoadTutorByPersonIdReq Request);

		// Token: 0x0600042A RID: 1066
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		TryToBookTutorAppointmentResp TryToBookTutorAppointment(TryToBookTutorAppointmentReq Request);

		// Token: 0x0600042B RID: 1067
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RecordConfidentialityAgreementSignedByTutor(RecordConfidentialityAgreementSignedByTutorReq Request);

		// Token: 0x0600042C RID: 1068
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsConfidentialityAgreementSigningRequiredForTutorResp IsConfidentialityAgreementSigningRequiredForTutor(IsConfidentialityAgreementSigningRequiredForTutorReq Request);

		// Token: 0x0600042D RID: 1069
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateTutorResp CreateTutor(CreateTutorReq Request);

		// Token: 0x0600042E RID: 1070
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RegisterTutorByExistingPersonId(RegisterTutorByExistingPersonIdReq Request);

		// Token: 0x0600042F RID: 1071
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTutorStatusResp GetTutorStatus(GetTutorStatusReq Request);

		// Token: 0x06000430 RID: 1072
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllTutorsResp LoadAllTutors(LoadAllTutorsReq Request);

		// Token: 0x06000431 RID: 1073
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ActivateTutor(ActivateTutorReq Request);

		// Token: 0x06000432 RID: 1074
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeActivateTutor(DeActivateTutorReq Request);

		// Token: 0x06000433 RID: 1075
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTutorAppointmentResp LoadTutorAppointment(LoadTutorAppointmentReq Request);

		// Token: 0x06000434 RID: 1076
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadTutorWithActiveStatusByIdResp LoadTutorWithActiveStatusById(LoadTutorWithActiveStatusByIdReq Request);

		// Token: 0x06000435 RID: 1077
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTutorStatusesResp GetTutorStatuses(GetTutorStatusesReq Request);
	}
}
