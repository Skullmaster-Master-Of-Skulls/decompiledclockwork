using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x020000A0 RID: 160
	public interface IStudentMediaRequestClientManager : IWebService
	{
		// Token: 0x0600051C RID: 1308
		StudentMediaRequestDTO CreateStudentMediaRequest(StudentMediaRequestDTO studentMediaRequest);

		// Token: 0x0600051D RID: 1309
		void UpdateStudentMediaRequest(StudentMediaRequestDTO studentMediaRequest);

		// Token: 0x0600051E RID: 1310
		bool IsMediaContentAlreadyRequested(int studentId, MediaContentIdentifierDTO identifier);

		// Token: 0x0600051F RID: 1311
		void DeleteStudentContentMediaRequestInfo(int requestedInfoId);

		// Token: 0x06000520 RID: 1312
		StudentMediaRequestDTO LoadStudentMediaRequestById(int studentMediaRequestId);

		// Token: 0x06000521 RID: 1313
		void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo);

		// Token: 0x06000522 RID: 1314
		int AddStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo);

		// Token: 0x06000523 RID: 1315
		IList<MediaContentRequestedInfoDTO> LoadStudentMediaRequestByStatus(MediaRequestStatus status);

		// Token: 0x06000524 RID: 1316
		IList<MediaContentRequestedInfoDTO> LoadAllMediaRequestInfoByJobId(int jobId);

		// Token: 0x06000525 RID: 1317
		Task<IList<MediaContentRequestedInfoExtendedDTO>> LoadAllStudentMediaRequestByStudentAndDatesAsync(int studentId, DateTime startdate, DateTime enddate);

		// Token: 0x06000526 RID: 1318
		IList<MediaContentRequestedInfoDTO> LoadAllApprovedMediaRequest(int campusId = 0);

		// Token: 0x06000527 RID: 1319
		IList<MediaContentRequestedInfoDTO> LoadAllToBeApprovedMediaRequest(int campusId = 0);

		// Token: 0x06000528 RID: 1320
		IList<MediaContentRequestedInfoDTO> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x06000529 RID: 1321
		IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequest(int campusId = 0);

		// Token: 0x0600052A RID: 1322
		IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x0600052B RID: 1323
		IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequest(DateTime startDate, DateTime endDate, int campusId = 0);

		// Token: 0x0600052C RID: 1324
		IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequestByStudentAndDate(int studentId, DateTime startdate, DateTime endDate, int campusId = 0);

		// Token: 0x0600052D RID: 1325
		IList<MediaContentRequestedInfoDTO> LoadAllInProgressStudentMediaRequest(int campusId = 0);

		// Token: 0x0600052E RID: 1326
		IList<MediaContentRequestedInfoDTO> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0);

		// Token: 0x0600052F RID: 1327
		ProofOfPurchaseInfoDTO AcceptProofOfPurchaseReceipt(ProofOfPurchaseInfoDTO proofOfPurchaseInfo);

		// Token: 0x06000530 RID: 1328
		bool RejectProofOfPurchaseReceipt(ProofOfPurchaseInfoDTO proofOfPurchaseInfo);

		// Token: 0x06000531 RID: 1329
		ProofOfPurchaseInfoDTO DownloadProofOfPurchase(int proofOfPurchaseId);

		// Token: 0x06000532 RID: 1330
		Task<ProofOfPurchaseInfoDTO> DownloadProofOfPurchaseAsync(int proofOfPurchaseId);

		// Token: 0x06000533 RID: 1331
		int UploadProofOfPurchase(ProofOfPurchaseInfoDTO proofOfPurchaseInfo);

		// Token: 0x06000534 RID: 1332
		Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfoDTO proofOfPurchaseInfo);

		// Token: 0x06000535 RID: 1333
		MediaContentRequestedInfoDTO LoadMediaContentRequestedInfoById(int mediaContentRequestedInfoId);

		// Token: 0x06000536 RID: 1334
		IList<MediaContentFormat> GetAllowedMediaContentFormatsForStudentToRequest(int pid, MediaContentIdentifierDTO mediaContentIdentifier, int selectedLuCourseId = 0);
	}
}
