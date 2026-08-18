using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000009 RID: 9
	[ServiceContract(Name = "MediaJobService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMediaJob : IService
	{
		// Token: 0x0600002E RID: 46
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddMediaJobNoteResp AddMediaJobNote(AddMediaJobNoteReq request);

		// Token: 0x0600002F RID: 47
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaJobNoteResp UpdateMediaJobNote(UpdateMediaJobNoteReq request);

		// Token: 0x06000030 RID: 48
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetRunningNotesByMediaJobResp GetRunningNotesByMediaJob(GetRunningNotesByMediaJobReq request);

		// Token: 0x06000031 RID: 49
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveMediaJobByIdResp GetActiveMediaJobById(GetActiveMediaJobByIdReq request);

		// Token: 0x06000032 RID: 50
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveMediaJobByMediaContentAndFormatResp GetActiveMediaJobByMediaContentAndFormat(GetActiveMediaJobByMediaContentAndFormatReq request);

		// Token: 0x06000033 RID: 51
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCountActiveMediaJobByMediaContentPerFormatIdResp GetCountActiveMediaJobByMediaContentPerFormatId(GetCountActiveMediaJobByMediaContentPerFormatIdReq request);

		// Token: 0x06000034 RID: 52
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCountActiveMediaJobByMediaContentAndFormatResp GetCountActiveMediaJobByMediaContentAndFormat(GetCountActiveMediaJobByMediaContentAndFormatReq request);

		// Token: 0x06000035 RID: 53
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveMediaJobsByAssignedStaffResp GetActiveMediaJobsByAssignedStaff(GetActiveMediaJobsByAssignedStaffReq request);

		// Token: 0x06000036 RID: 54
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveMediaJobsByExpiredInLessThanResp GetActiveMediaJobsByExpiredInLessThan(GetActiveMediaJobsByExpiredInLessThanReq request);

		// Token: 0x06000037 RID: 55
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveExpiredMediaJobsResp GetActiveExpiredMediaJobs(GetActiveExpiredMediaJobsReq request);

		// Token: 0x06000038 RID: 56
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveJobsResp GetActiveJobs(GetActiveJobsReq request);

		// Token: 0x06000039 RID: 57
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveJobsByStudentResp GetActiveJobsByStudent(GetActiveJobsByStudentReq request);

		// Token: 0x0600003A RID: 58
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedMediaJobByIdResp GetCompletedMediaJobById(GetCompletedMediaJobByIdReq request);

		// Token: 0x0600003B RID: 59
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCancelledMediaJobByIdResp GetCancelledMediaJobById(GetCancelledMediaJobByIdReq request);

		// Token: 0x0600003C RID: 60
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedMediaJobByMediaContentAndFormatResp GetCompletedMediaJobByMediaContentAndFormat(GetCompletedMediaJobByMediaContentAndFormatReq request);

		// Token: 0x0600003D RID: 61
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedMediaJobsByAssignedStaffResp GetCompletedMediaJobsByAssignedStaff(GetCompletedMediaJobsByAssignedStaffReq request);

		// Token: 0x0600003E RID: 62
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedJobsByDateRangeResp GetCompletedJobsByDateRange(GetCompletedJobsByDateRangeReq request);

		// Token: 0x0600003F RID: 63
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCancelledJobsByDateRangeResp GetCancelledJobsByDateRange(GetCancelledJobsByDateRangeReq request);

		// Token: 0x06000040 RID: 64
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedJobsResp GetCompletedJobs(GetCompletedJobsReq request);

		// Token: 0x06000041 RID: 65
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCancelledJobsResp GetCancelledJobs(GetCancelledJobsReq request);

		// Token: 0x06000042 RID: 66
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedJobsByStudentResp GetCompletedJobsByStudent(GetCompletedJobsByStudentReq request);

		// Token: 0x06000043 RID: 67
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedJobsByStudentAndDateRangeResp GetCompletedJobsByStudentAndDateRange(GetCompletedJobsByStudentAndDateRangeReq request);

		// Token: 0x06000044 RID: 68
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCancelledJobsByStudentAndDateRangeResp GetCancelledJobsByStudentAndDateRange(GetCancelledJobsByStudentAndDateRangeReq request);

		// Token: 0x06000045 RID: 69
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCompletedJobsByStaffAndDateRangeResp GetCompletedJobsByStaffAndDateRange(GetCompletedJobsByStaffAndDateRangeReq request);

		// Token: 0x06000046 RID: 70
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetCancelledJobsByStaffAndDateRangeResp GetCancelledJobsByStaffAndDateRange(GetCancelledJobsByStaffAndDateRangeReq request);

		// Token: 0x06000047 RID: 71
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateMediaJobResp CreateMediaJob(CreateMediaJobReq request);

		// Token: 0x06000048 RID: 72
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaJobResp UpdateMediaJob(UpdateMediaJobReq request);

		// Token: 0x06000049 RID: 73
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CancelMediaJobResp CancelMediaJob(CancelMediaJobReq request);

		// Token: 0x0600004A RID: 74
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MarkMediaJobAsCompletedResp MarkMediaJobAsCompleted(MarkMediaJobAsCompletedReq request);

		// Token: 0x0600004B RID: 75
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeMediaJobStatusResp ChangeMediaJobStatus(ChangeMediaJobStatusReq request);
	}
}
