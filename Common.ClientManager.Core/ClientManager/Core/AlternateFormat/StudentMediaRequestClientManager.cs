using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x020000A4 RID: 164
	public class StudentMediaRequestClientManager : IStudentMediaRequestClientManager, IWebService
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x0001B678 File Offset: 0x00019878
		public StudentMediaRequestDTO CreateStudentMediaRequest(StudentMediaRequestDTO studentMediaRequest)
		{
			CreateStudentMediaReq createStudentMediaReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateStudentMediaReq>();
			createStudentMediaReq.MediaRequest = studentMediaRequest;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().CreateStudentMediaRequest(createStudentMediaReq).MediaRequest;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001B6B0 File Offset: 0x000198B0
		public void UpdateStudentMediaRequest(StudentMediaRequestDTO studentMediaRequest)
		{
			UpdateStudentMediaReq updateStudentMediaReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateStudentMediaReq>();
			updateStudentMediaReq.MediaRequest = studentMediaRequest;
			ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().UpdateStudentMediaRequest(updateStudentMediaReq);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public StudentMediaRequestDTO LoadStudentMediaRequestById(int studentMediaRequestId)
		{
			LoadStudentMediaRequestByIdReq loadStudentMediaRequestByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentMediaRequestByIdReq>();
			loadStudentMediaRequestByIdReq.StudentMediaRequestId = studentMediaRequestId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadStudentMediaRequestById(loadStudentMediaRequestByIdReq).MediaRequest;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001B718 File Offset: 0x00019918
		public IList<MediaContentRequestedInfoDTO> LoadStudentMediaRequestByStatus(MediaRequestStatus status)
		{
			LoadStudentMediaRequestByStatusReq loadStudentMediaRequestByStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentMediaRequestByStatusReq>();
			loadStudentMediaRequestByStatusReq.Status = status;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadStudentMediaRequestByStatus(loadStudentMediaRequestByStatusReq).StudentMediaRequests;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001B750 File Offset: 0x00019950
		[DebuggerStepThrough]
		public Task<IList<MediaContentRequestedInfoExtendedDTO>> LoadAllStudentMediaRequestByStudentAndDatesAsync(int studentId, DateTime startdate, DateTime enddate)
		{
			StudentMediaRequestClientManager.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__4 <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__ = new StudentMediaRequestClientManager.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__4();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentRequestedInfoExtendedDTO>>.Create();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>4__this = this;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.studentId = studentId;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.startdate = startdate;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.enddate = enddate;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>1__state = -1;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Start<StudentMediaRequestClientManager.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__4>(ref <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__);
			return <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001B7AC File Offset: 0x000199AC
		public IList<MediaContentRequestedInfoDTO> LoadAllApprovedMediaRequest(int campusId = 0)
		{
			LoadAllApprovedMediaRequestReq loadAllApprovedMediaRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllApprovedMediaRequestReq>();
			loadAllApprovedMediaRequestReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllApprovedMediaRequest(loadAllApprovedMediaRequestReq).StudentMediaRequests;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public IList<MediaContentRequestedInfoDTO> LoadAllToBeApprovedMediaRequest(int campusId = 0)
		{
			LoadAllToBeApprovedMediaRequestReq loadAllToBeApprovedMediaRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllToBeApprovedMediaRequestReq>();
			loadAllToBeApprovedMediaRequestReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllToBeApprovedMediaRequest(loadAllToBeApprovedMediaRequestReq).StudentMediaRequests;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001B81C File Offset: 0x00019A1C
		public IList<MediaContentRequestedInfoDTO> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0)
		{
			LoadAllToBeApprovedMediaRequestByStudentReq loadAllToBeApprovedMediaRequestByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllToBeApprovedMediaRequestByStudentReq>();
			loadAllToBeApprovedMediaRequestByStudentReq.StudentId = studentId;
			loadAllToBeApprovedMediaRequestByStudentReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllToBeApprovedMediaRequestByStudent(loadAllToBeApprovedMediaRequestByStudentReq).StudentMediaRequests;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001B85C File Offset: 0x00019A5C
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequest(int campusId = 0)
		{
			LoadAllCompletedStudentMediaRequestReq loadAllCompletedStudentMediaRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllCompletedStudentMediaRequestReq>();
			loadAllCompletedStudentMediaRequestReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllCompletedStudentMediaRequest(loadAllCompletedStudentMediaRequestReq).StudentMediaRequests;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001B894 File Offset: 0x00019A94
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			LoadAllCompletedStudentMediaRequestByStudentReq loadAllCompletedStudentMediaRequestByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllCompletedStudentMediaRequestByStudentReq>();
			loadAllCompletedStudentMediaRequestByStudentReq.StudentId = studentId;
			loadAllCompletedStudentMediaRequestByStudentReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllCompletedStudentMediaRequestByStudent(loadAllCompletedStudentMediaRequestByStudentReq).StudentMediaRequests;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequest(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			LoadAllCompletedStudentMediaRequestByDateReq loadAllCompletedStudentMediaRequestByDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllCompletedStudentMediaRequestByDateReq>();
			loadAllCompletedStudentMediaRequestByDateReq.StartDate = startDate;
			loadAllCompletedStudentMediaRequestByDateReq.EndDate = endDate;
			loadAllCompletedStudentMediaRequestByDateReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllCompletedStudentMediaRequestByDate(loadAllCompletedStudentMediaRequestByDateReq).StudentMediaRequests;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001B91C File Offset: 0x00019B1C
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequestByStudentAndDate(int studentId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			LoadAllCompletedStudentMediaRequestByStudentAndDateReq loadAllCompletedStudentMediaRequestByStudentAndDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllCompletedStudentMediaRequestByStudentAndDateReq>();
			loadAllCompletedStudentMediaRequestByStudentAndDateReq.StudentId = studentId;
			loadAllCompletedStudentMediaRequestByStudentAndDateReq.StartDate = startDate;
			loadAllCompletedStudentMediaRequestByStudentAndDateReq.EndDate = endDate;
			loadAllCompletedStudentMediaRequestByStudentAndDateReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllCompletedStudentMediaRequestByStudentAndDate(loadAllCompletedStudentMediaRequestByStudentAndDateReq).StudentMediaRequests;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001B96C File Offset: 0x00019B6C
		public IList<MediaContentRequestedInfoDTO> LoadAllInProgressStudentMediaRequest(int campusId = 0)
		{
			LoadAllInProgressStudentMediaRequestReq loadAllInProgressStudentMediaRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllInProgressStudentMediaRequestReq>();
			loadAllInProgressStudentMediaRequestReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllInProgressStudentMediaRequest(loadAllInProgressStudentMediaRequestReq).StudentMediaRequests;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		public IList<MediaContentRequestedInfoDTO> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			LoadAllInProgressStudentMediaRequestByStudentReq loadAllInProgressStudentMediaRequestByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllInProgressStudentMediaRequestByStudentReq>();
			loadAllInProgressStudentMediaRequestByStudentReq.StudentId = studentId;
			loadAllInProgressStudentMediaRequestByStudentReq.CampusId = campusId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllInProgressStudentMediaRequestByStudent(loadAllInProgressStudentMediaRequestByStudentReq).StudentMediaRequests;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		public ProofOfPurchaseInfoDTO AcceptProofOfPurchaseReceipt(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			AcceptProofOfPurchaseReceiptReq acceptProofOfPurchaseReceiptReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AcceptProofOfPurchaseReceiptReq>();
			acceptProofOfPurchaseReceiptReq.ProofOfPurchase = proofOfPurchaseInfo;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().AcceptProofOfPurchaseReceipt(acceptProofOfPurchaseReceiptReq).ProofOfPurchase;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001BA1C File Offset: 0x00019C1C
		public bool RejectProofOfPurchaseReceipt(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			RejectProofOfPurchaseReceiptReq rejectProofOfPurchaseReceiptReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RejectProofOfPurchaseReceiptReq>();
			rejectProofOfPurchaseReceiptReq.ProofOfPurchase = proofOfPurchaseInfo;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().RejectProofOfPurchaseReceipt(rejectProofOfPurchaseReceiptReq).Rejected;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001BA54 File Offset: 0x00019C54
		public void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo)
		{
			UpdateStudentContentMediaRequestInfoReq updateStudentContentMediaRequestInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateStudentContentMediaRequestInfoReq>();
			updateStudentContentMediaRequestInfoReq.MediaContentRequestedInfo = requestedInfo;
			ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().UpdateStudentContentMediaRequestInfo(updateStudentContentMediaRequestInfoReq);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001BA84 File Offset: 0x00019C84
		public int AddStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo)
		{
			AddStudentContentMediaRequestInfoReq addStudentContentMediaRequestInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddStudentContentMediaRequestInfoReq>();
			addStudentContentMediaRequestInfoReq.MediaContentRequestedInfo = requestedInfo;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().AddStudentContentMediaRequestInfo(addStudentContentMediaRequestInfoReq).MediaContentRequestedInfoId;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001BABC File Offset: 0x00019CBC
		public void DeleteStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo)
		{
			DeleteStudentContentMediaRequestInfoReq deleteStudentContentMediaRequestInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteStudentContentMediaRequestInfoReq>();
			deleteStudentContentMediaRequestInfoReq.MediaContentRequestedInfo = requestedInfo;
			ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().DeleteStudentContentMediaRequestInfo(deleteStudentContentMediaRequestInfoReq);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001BAEC File Offset: 0x00019CEC
		public ProofOfPurchaseInfoDTO DownloadProofOfPurchase(int proofOfPurchaseId)
		{
			DownloadProofOfPurchaseReq downloadProofOfPurchaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DownloadProofOfPurchaseReq>();
			downloadProofOfPurchaseReq.ProofOfPurchaseId = proofOfPurchaseId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().DownloadProofOfPurchase(downloadProofOfPurchaseReq).ProofOfPurchase;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001BB24 File Offset: 0x00019D24
		[DebuggerStepThrough]
		public Task<ProofOfPurchaseInfoDTO> DownloadProofOfPurchaseAsync(int proofOfPurchaseId)
		{
			StudentMediaRequestClientManager.<DownloadProofOfPurchaseAsync>d__20 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestClientManager.<DownloadProofOfPurchaseAsync>d__20();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfoDTO>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestClientManager.<DownloadProofOfPurchaseAsync>d__20>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001BB70 File Offset: 0x00019D70
		public IList<MediaContentRequestedInfoDTO> LoadAllMediaRequestInfoByJobId(int jobId)
		{
			LoadAllMediaRequestInfoByJobIdReq loadAllMediaRequestInfoByJobIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllMediaRequestInfoByJobIdReq>();
			loadAllMediaRequestInfoByJobIdReq.JobId = jobId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadAllMediaRequestInfoByJobId(loadAllMediaRequestInfoByJobIdReq).MediaContentRequestedList;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		public bool IsMediaContentAlreadyRequested(int studentId, MediaContentIdentifierDTO identifier)
		{
			IsMediaContentAlreadyRequestedReq isMediaContentAlreadyRequestedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsMediaContentAlreadyRequestedReq>();
			isMediaContentAlreadyRequestedReq.Identifier = identifier;
			isMediaContentAlreadyRequestedReq.StudentPersonId = studentId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().IsMediaContentAlreadyRequested(isMediaContentAlreadyRequestedReq).WasRequested;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		public void DeleteStudentContentMediaRequestInfo(int requestedInfoId)
		{
			DeleteStudentContentMediaRequestInfoByIdReq deleteStudentContentMediaRequestInfoByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteStudentContentMediaRequestInfoByIdReq>();
			deleteStudentContentMediaRequestInfoByIdReq.MediaContentRequestInfoId = requestedInfoId;
			ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().DeleteStudentContentMediaRequestInfoById(deleteStudentContentMediaRequestInfoByIdReq);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001BC18 File Offset: 0x00019E18
		public int UploadProofOfPurchase(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			UploadProofOfPurchaseReq uploadProofOfPurchaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UploadProofOfPurchaseReq>();
			uploadProofOfPurchaseReq.ProofOfPurchaseInfo = proofOfPurchaseInfo;
			return proofOfPurchaseInfo.ProofOfPurchaseId = ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().UploadProofOfPurchase(uploadProofOfPurchaseReq).ProofOfPurchaseId;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001BC58 File Offset: 0x00019E58
		[DebuggerStepThrough]
		public Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			StudentMediaRequestClientManager.<UploadProofOfPurchaseAsync>d__25 <UploadProofOfPurchaseAsync>d__ = new StudentMediaRequestClientManager.<UploadProofOfPurchaseAsync>d__25();
			<UploadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadProofOfPurchaseAsync>d__.<>4__this = this;
			<UploadProofOfPurchaseAsync>d__.proofOfPurchaseInfo = proofOfPurchaseInfo;
			<UploadProofOfPurchaseAsync>d__.<>1__state = -1;
			<UploadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestClientManager.<UploadProofOfPurchaseAsync>d__25>(ref <UploadProofOfPurchaseAsync>d__);
			return <UploadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		public MediaContentRequestedInfoDTO LoadMediaContentRequestedInfoById(int mediaContentRequestedInfoId)
		{
			LoadMediaContentRequestedInfoByIdReq loadMediaContentRequestedInfoByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentRequestedInfoByIdReq>();
			loadMediaContentRequestedInfoByIdReq.MediaContentRequestedId = mediaContentRequestedInfoId;
			return ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().LoadMediaContentRequestedInfoById(loadMediaContentRequestedInfoByIdReq).MediaContentRequestedInfo;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001BCDC File Offset: 0x00019EDC
		public IList<MediaContentFormat> GetAllowedMediaContentFormatsForStudentToRequest(int pid, MediaContentIdentifierDTO mediaContentIdentifier, int selectedLuCourseId = 0)
		{
			GetAllowedMediaContentFormatsForStudentToRequestReq getAllowedMediaContentFormatsForStudentToRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllowedMediaContentFormatsForStudentToRequestReq>();
			getAllowedMediaContentFormatsForStudentToRequestReq.PersonId = pid;
			getAllowedMediaContentFormatsForStudentToRequestReq.MediaContentIdentifier = mediaContentIdentifier;
			getAllowedMediaContentFormatsForStudentToRequestReq.SelectedLuCourseId = selectedLuCourseId;
			GetAllowedMediaContentFormatsForStudentToRequestResp allowedMediaContentFormatsForStudentToRequest = ClientServiceFactory.GetClientInstance<IStudentMediaRequest>().GetAllowedMediaContentFormatsForStudentToRequest(getAllowedMediaContentFormatsForStudentToRequestReq);
			return (allowedMediaContentFormatsForStudentToRequest != null) ? allowedMediaContentFormatsForStudentToRequest.AllowedFormats : null;
		}
	}
}
