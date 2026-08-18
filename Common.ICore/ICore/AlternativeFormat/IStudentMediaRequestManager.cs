using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000F5 RID: 245
	public interface IStudentMediaRequestManager : IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007E2 RID: 2018
		StudentMediaRequest CreateStudentMediaRequest(StudentMediaRequest studentMediaRequest);

		// Token: 0x060007E3 RID: 2019
		void UpdateStudentMediaRequest(StudentMediaRequest studentMediaRequest);

		// Token: 0x060007E4 RID: 2020
		StudentMediaRequest LoadStudentMediaRequestById(int studentMediaRequestId);

		// Token: 0x060007E5 RID: 2021
		MediaContentRequestedInfo LoadMediaContentRequestedInfoById(int mediaContentRequestedInfoId);

		// Token: 0x060007E6 RID: 2022
		MediaContentRequestedInfo LoadArchiveMediaContentRequestedInfoById(int mediaContentRequestedInfoId);

		// Token: 0x060007E7 RID: 2023
		void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060007E8 RID: 2024
		int AddStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060007E9 RID: 2025
		void DeleteStudentContentMediaRequestInfo(int requestedInfoId);

		// Token: 0x060007EA RID: 2026
		void DeleteStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060007EB RID: 2027
		void UpdateAvailableDownloadingTime(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060007EC RID: 2028
		ProofOfPurchaseInfo AcceptProofOfPurchaseReceipt(ProofOfPurchaseInfo proofOfPurchaseInfo);

		// Token: 0x060007ED RID: 2029
		bool RejectProofOfPurchaseReceipt(ProofOfPurchaseInfo proofOfPurchaseInfo);

		// Token: 0x060007EE RID: 2030
		bool IsProofOfPurchaseAvailable(Guid mediaContentUniqueId, int studentPersonId);

		// Token: 0x060007EF RID: 2031
		ProofOfPurchaseInfo DownloadProofOfPurchase(Guid mediaContentUniqueId, int studentPersonId);

		// Token: 0x060007F0 RID: 2032
		ProofOfPurchaseInfo DownloadProofOfPurchase(int proofOfPurchaseId);

		// Token: 0x060007F1 RID: 2033
		Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(Guid mediaContentUniqueId, int studentPersonId);

		// Token: 0x060007F2 RID: 2034
		Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(int proofOfPurchaseId);

		// Token: 0x060007F3 RID: 2035
		int UploadProofOfPurchase(ProofOfPurchaseInfo proofOfPurchaseInfo);

		// Token: 0x060007F4 RID: 2036
		Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfo proofOfPurchaseInfo);

		// Token: 0x060007F5 RID: 2037
		void DeleteProofOfPurchase(int proofOfPurchaseId);

		// Token: 0x060007F6 RID: 2038
		Task DeleteProofOfPurchaseAsync(int proofOfPurchaseId);

		// Token: 0x060007F7 RID: 2039
		IList<MediaContentRequestedInfo> LoadAllMediaRequestInfoByJobId(int jobId);

		// Token: 0x060007F8 RID: 2040
		IList<MediaContentRequestedInfo> LoadStudentMediaRequestByStatus(MediaRequestStatus status);

		// Token: 0x060007F9 RID: 2041
		IList<MediaContentRequestedInfo> LoadAllApprovedMediaRequest(int campusId = 0);

		// Token: 0x060007FA RID: 2042
		IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequest(int campusId = 0);

		// Token: 0x060007FB RID: 2043
		IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x060007FC RID: 2044
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(int campusId = 0);

		// Token: 0x060007FD RID: 2045
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x060007FE RID: 2046
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(DateTime startdate, DateTime endDate, int campusId = 0);

		// Token: 0x060007FF RID: 2047
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, DateTime startdate, DateTime endDate, int campusId = 0);

		// Token: 0x06000800 RID: 2048
		IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequest(int campusId = 0);

		// Token: 0x06000801 RID: 2049
		IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x06000802 RID: 2050
		Task<IList<MediaContentRequestedInfoExtended>> LoadAllStudentMediaRequestByStudentAsync(int studentId, DateTime startdate, DateTime enddate);

		// Token: 0x06000803 RID: 2051
		bool IsMediaContentAlreadyRequested(int studentId, MediaContentIdentifier identifier);

		// Token: 0x06000804 RID: 2052
		MediaContentRequestedInfo MarkMediaContentRequestedAsCompleted(int mediaContentRequestInfoId, MediaRequestStatus status, DateTime availableStartTime, DateTime availableEndTime, int mediaContentPerFormatId);

		// Token: 0x06000805 RID: 2053
		MediaContentFormat[] GetAllowedMediaContentFormatsForStudentToRequest(int pid, MediaContentIdentifier mediaContentIdentifier, int selectedLuCourseId = 0);
	}
}
