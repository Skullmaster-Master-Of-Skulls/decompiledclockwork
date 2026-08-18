using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal.QueryStorageServiceProvidersOriginal
{
	// Token: 0x02000067 RID: 103
	internal static class QueryStorageOriginalRequestDetail
	{
		// Token: 0x0400010B RID: 267
		internal const string QS_REQUEST_DETAIL_BY_REQUEST_ID = "SELECT\tspr.ServiceProviderId,sprd.ServiceProviderRequestDetailId,\r\n\t\tsprd.CounsellorPid,p.student_no,p.firstName,p.middleName,p.lastName,\r\n\t\tsprd.dateentered2,sprd.fsBSWD,sprd.fsBSWDStatus,sprd.fsFirstNations,sprd.fsFirstNationsCaseWorkerPhone,sprd.fsFirstNationsLetterOfApprovalFile,\r\n\t\tsprd.fsFirstNationsLetterOfApprovalFilename,sprd.fsFirstNationsStatus,sprd.fsInterpreterFund,sprd.fsInterpreterFundCode,\r\n\t\tsprd.fsInterpreterFundStatus,sprd.fsOsapStatus,sprd.fsOther,sprd.fsOtherDetail,sprd.fsOtherDetail,sprd.fsOtherFile,\r\n\t\tsprd.fsOtherFilename,sprd.fsOtherStatus,sprd.fsSsd,sprd.fsSsdStatus,sprd.fsWSIB,sprd.fsWSIBCaseWorkerPhone,sprd.fsWSIBLetterOfApprovalFile,\r\n\t\tsprd.fsWSIBLetterOfApprovalFilename,sprd.fsWSIBStatus,sprd.[plan],sprd.rationale,sprd.specialrequest\r\nFROM\tServiceProviderRequests spr LEFT JOIN ServiceProviderRequestDetail sprd ON sprd.ServiceProviderRequestDetailId=spr.ServiceProviderRequestDetailId\r\n\t\tLEFT JOIN people p ON p.PersonID=sprd.CounsellorPid\r\nWHERE\tspr.ServiceProviderRequestID=@sprid";
	}
}
