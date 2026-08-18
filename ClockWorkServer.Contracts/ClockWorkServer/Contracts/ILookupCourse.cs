using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200009C RID: 156
	[ServiceContract(Name = "LookupCourseService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILookupCourse : IService
	{
		// Token: 0x06000458 RID: 1112
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateLookupCourseResp CreateLookupCourse(CreateLookupCourseReq Request);

		// Token: 0x06000459 RID: 1113
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseByLuCourseIdResp LoadCourseByLuCourseId(LoadCourseByLuCourseIdReq Request);

		// Token: 0x0600045A RID: 1114
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCoursesBySubjectAndSessionResp LoadCoursesBySubjectAndSession(LoadCoursesBySubjectAndSessionReq request);

		// Token: 0x0600045B RID: 1115
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateCourseInstructorExemption(UpdateCourseInstructorExemptionReq Request);

		// Token: 0x0600045C RID: 1116
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseBasesBySearchStringResp LoadCourseBasesBySearchString(LoadCourseBasesBySearchStringReq Request);

		// Token: 0x0600045D RID: 1117
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateLookupCourseBaseResp CreateLookupCourseBase(CreateLookupCourseBaseReq Request);

		// Token: 0x0600045E RID: 1118
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadIsLookupCourseExemptFromDataSyncResp LoadIsLookupCourseExemptFromDataSync(LoadIsLookupCourseExemptFromDataSyncReq Request);

		// Token: 0x0600045F RID: 1119
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateLookupCourseExemptionFromDataSync(UpdateLookupCourseExemptionFromDataSyncReq Request);

		// Token: 0x06000460 RID: 1120
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDurationTermSubjectsBySessionResp LoadDurationTermSubjectsBySession(LoadDurationTermSubjectsBySessionReq Request);

		// Token: 0x06000461 RID: 1121
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsCoursesBySessionResp LoadStudentsCoursesBySession(LoadStudentsCoursesBySessionReq request);

		// Token: 0x06000462 RID: 1122
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsCoursesByDatesResp LoadStudentsCoursesByDates(LoadStudentsCoursesByDatesReq request);

		// Token: 0x06000463 RID: 1123
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq Request);

		// Token: 0x06000464 RID: 1124
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUniqueCourseDateRangesBySessionResp LoadUniqueCourseDateRangesBySession(LoadUniqueCourseDateRangesBySessionReq Request);

		// Token: 0x06000465 RID: 1125
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateCourseDateRangeResp UpdateCourseDateRange(UpdateCourseDateRangeReq Request);

		// Token: 0x06000466 RID: 1126
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCoursesInDateRangeResp LoadCoursesInDateRange(LoadCoursesInDateRangeReq Request);
	}
}
