using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000CF RID: 207
	public interface IStudentMediaRequestDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005F5 RID: 1525
		StudentMediaRequest CreateStudentMediaRequest(StudentMediaRequest studentMediaRequest);

		// Token: 0x060005F6 RID: 1526
		void UpdateStudentMediaRequest(StudentMediaRequest studentMediaRequest);

		// Token: 0x060005F7 RID: 1527
		StudentMediaRequest LoadStudentMediaRequestById(int studentMediaRequestId);

		// Token: 0x060005F8 RID: 1528
		MediaContentRequestedInfo LoadMediaContentRequestInfoById(int mediaContentRequestInfoId);

		// Token: 0x060005F9 RID: 1529
		MediaContentRequestedInfo LoadArchiveMediaContentRequestInfoById(int mediaContentRequestInfoId);

		// Token: 0x060005FA RID: 1530
		MediaContentRequestedInfo LoadMediaContentRequestInfoByMediaContentPerFormatAndStudent(int studentPersonId, int mediaContentPerFormatId);

		// Token: 0x060005FB RID: 1531
		bool IsMediaContentAlreadyRequested(int studentId, MediaContentIdentifier identifier);

		// Token: 0x060005FC RID: 1532
		void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060005FD RID: 1533
		Task UpdateStudentContentMediaRequestInfoAsync(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060005FE RID: 1534
		int AddStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo);

		// Token: 0x060005FF RID: 1535
		void DeleteStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo, MediaRequestStatus status = MediaRequestStatus.Rejected_by_Staff);

		// Token: 0x06000600 RID: 1536
		void UpdateAvailableDownloadingTime(MediaContentRequestedInfo requestedInfo);

		// Token: 0x06000601 RID: 1537
		bool IsProofOfPurchaseAvailable(Guid mediaContentUniqueId, int studentPersonId);

		// Token: 0x06000602 RID: 1538
		ProofOfPurchaseInfo DownloadProofOfPurchase(Guid mediaContentUniqueId, int studentPersonId);

		// Token: 0x06000603 RID: 1539
		ProofOfPurchaseInfo DownloadProofOfPurchase(int proofOfPurchaseId);

		// Token: 0x06000604 RID: 1540
		Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(Guid mediaContentUniqueId, int studentPersonId);

		// Token: 0x06000605 RID: 1541
		Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(int proofOfPurchaseId);

		// Token: 0x06000606 RID: 1542
		int UploadProofOfPurchase(ProofOfPurchaseInfo proofOfPurchaseInfo);

		// Token: 0x06000607 RID: 1543
		Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfo proofOfPurchaseInfo);

		// Token: 0x06000608 RID: 1544
		void UpdateProofOfPurchase(ProofOfPurchaseInfo proofOfPurchase);

		// Token: 0x06000609 RID: 1545
		void DeleteProofOfPurchase(int proofOfPurchaseId);

		// Token: 0x0600060A RID: 1546
		Task DeleteProofOfPurchaseAsync(int proofOfPurchaseId);

		// Token: 0x0600060B RID: 1547
		IList<MediaContentRequestedInfo> LoadAllMediaRequestInfoByJobId(int jobId);

		// Token: 0x0600060C RID: 1548
		IList<MediaContentRequestedInfo> LoadStudentMediaRequestByStatus(MediaRequestStatus status);

		// Token: 0x0600060D RID: 1549
		IList<MediaContentRequestedInfo> LoadAllApprovedMediaRequest(int campusId = 0);

		// Token: 0x0600060E RID: 1550
		IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequest(int campusId = 0);

		// Token: 0x0600060F RID: 1551
		IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x06000610 RID: 1552
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(int campusId = 0);

		// Token: 0x06000611 RID: 1553
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x06000612 RID: 1554
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(DateTime startdate, DateTime endDate, int campusId = 0);

		// Token: 0x06000613 RID: 1555
		IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, DateTime startdate, DateTime endDate, int campusId = 0);

		// Token: 0x06000614 RID: 1556
		IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x06000615 RID: 1557
		IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequest(int campusId = 0);

		// Token: 0x06000616 RID: 1558
		Task<IList<MediaContentRequestedInfo>> LoadAllMediaContentRequestInfoByMediaContentAndStudentAsync(int studentPersonId, Guid mediaContentId);

		// Token: 0x06000617 RID: 1559
		Task<IList<MediaContentRequestedInfoExtended>> LoadAllStudentMediaRequestByStudentAsync(int studentId, DateTime startdate, DateTime enddate);

		// Token: 0x06000618 RID: 1560
		void MarkMediaContentRequestedAsCompleted(int mediaContentRequestInfoId, MediaRequestStatus status, DateTime availableStartTime, DateTime availableEndTime, int mediaContentPerFormatId);
	}
}
