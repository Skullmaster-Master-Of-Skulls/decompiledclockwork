using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000017 RID: 23
	[ServiceContract(Name = "ClassTestDefinitionService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IClassTestDefinition : IService
	{
		// Token: 0x060000E0 RID: 224
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestBaseByIdResp LoadClassTestBaseById(LoadClassTestBaseByIdReq Request);

		// Token: 0x060000E1 RID: 225
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		[ServiceKnownType(typeof(ClassTestDTO))]
		void UpdateClassTestDefinitionBase(UpdateClassTestDefinitionBaseReq Request);

		// Token: 0x060000E2 RID: 226
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateClassTestDefinitionBaseResp CreateClassTestDefinitionBase(CreateClassTestDefinitionBaseReq Request);

		// Token: 0x060000E3 RID: 227
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestDefinitionsResp LoadClassTestDefinitions(LoadClassTestDefinitionsReq request);

		// Token: 0x060000E4 RID: 228
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveClassTestDefinition(SaveClassTestDefinitionReq request);

		// Token: 0x060000E5 RID: 229
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateTestDelivered(UpdateTestDeliveredReq Request);

		// Token: 0x060000E6 RID: 230
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestByIdResp LoadClassTestById(LoadClassTestByIdReq Request);

		// Token: 0x060000E7 RID: 231
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestForEditByIdResp LoadClassTestForEditById(LoadClassTestForEditByIdReq Request);

		// Token: 0x060000E8 RID: 232
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateClassTestDefinition(UpdateClassTestDefinitionReq Request);

		// Token: 0x060000E9 RID: 233
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateInstructorSubmittedTestInfo(UpdateInstructorSubmittedTestInfoReq Request);

		// Token: 0x060000EA RID: 234
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateInstructorContactedInfo(UpdateInstructorContactedInfoReq Request);

		// Token: 0x060000EB RID: 235
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateTestPickedUp(UpdateTestPickedUpReq Request);

		// Token: 0x060000EC RID: 236
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq Request);

		// Token: 0x060000ED RID: 237
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestForExamRequestByIdResp LoadClassTestForExamRequestById(LoadClassTestForExamRequestByIdReq Request);

		// Token: 0x060000EE RID: 238
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestsForExamRequestByDateRangeResp LoadClassTestsForExamRequestByDateRange(LoadClassTestsForExamRequestByDateRangeReq Request);

		// Token: 0x060000EF RID: 239
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadClassTestsForDisplayResp LoadClassTestsForDisplay(LoadClassTestsForDisplayReq Request);

		// Token: 0x060000F0 RID: 240
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveInstructorHasSubmittedInformationAboutThisTestMarkerResp RemoveInstructorHasSubmittedInformationAboutThisTestMarker(RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq Request);
	}
}
