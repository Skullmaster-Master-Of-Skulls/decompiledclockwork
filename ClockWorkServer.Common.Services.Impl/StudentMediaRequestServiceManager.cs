using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200000B RID: 11
	public class StudentMediaRequestServiceManager : IStudentMediaRequest, IService
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003C98 File Offset: 0x00001E98
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003CAC File Offset: 0x00001EAC
		public CreateStudentMediaResp CreateStudentMediaRequest(CreateStudentMediaReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new CreateStudentMediaResp
			{
				MediaRequest = studentMediaRequestManager.CreateStudentMediaRequest(request.MediaRequest.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003CEC File Offset: 0x00001EEC
		public UpdateStudentMediaResp UpdateStudentMediaRequest(UpdateStudentMediaReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			studentMediaRequestManager.UpdateStudentMediaRequest(request.MediaRequest.ToDomainObject());
			return new UpdateStudentMediaResp();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D24 File Offset: 0x00001F24
		public LoadStudentMediaRequestByIdResp LoadStudentMediaRequestById(LoadStudentMediaRequestByIdReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadStudentMediaRequestByIdResp
			{
				MediaRequest = studentMediaRequestManager.LoadStudentMediaRequestById(request.StudentMediaRequestId).ToDTO()
			};
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003D60 File Offset: 0x00001F60
		public LoadStudentMediaRequestByStatusResp LoadStudentMediaRequestByStatus(LoadStudentMediaRequestByStatusReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadStudentMediaRequestByStatusResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadStudentMediaRequestByStatus(request.Status).ToDTO()
			};
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003D9C File Offset: 0x00001F9C
		public LoadAllApprovedMediaRequestResp LoadAllApprovedMediaRequest(LoadAllApprovedMediaRequestReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllApprovedMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllApprovedMediaRequest(request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequest(LoadAllToBeApprovedMediaRequestReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllToBeApprovedMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllToBeApprovedMediaRequest(request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003E14 File Offset: 0x00002014
		public LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequestByStudent(LoadAllToBeApprovedMediaRequestByStudentReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllToBeApprovedMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllToBeApprovedMediaRequestByStudent(request.StudentId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E58 File Offset: 0x00002058
		public LoadAllInProgressStudentMediaRequestResp LoadAllInProgressStudentMediaRequest(LoadAllInProgressStudentMediaRequestReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllInProgressStudentMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllInProgressStudentMediaRequest(request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E94 File Offset: 0x00002094
		public LoadAllInProgressStudentMediaRequestByStudentResp LoadAllInProgressStudentMediaRequestByStudent(LoadAllInProgressStudentMediaRequestByStudentReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllInProgressStudentMediaRequestByStudentResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllInProgressStudentMediaRequestByStudent(request.StudentId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003ED8 File Offset: 0x000020D8
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequest(LoadAllCompletedStudentMediaRequestReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllCompletedStudentMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllCompletedStudentMediaRequest(request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003F14 File Offset: 0x00002114
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByDate(LoadAllCompletedStudentMediaRequestByDateReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllCompletedStudentMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllCompletedStudentMediaRequest(request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003F5C File Offset: 0x0000215C
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudent(LoadAllCompletedStudentMediaRequestByStudentReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllCompletedStudentMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllCompletedStudentMediaRequestByStudent(request.StudentId, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003FA0 File Offset: 0x000021A0
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudentAndDate(LoadAllCompletedStudentMediaRequestByStudentAndDateReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllCompletedStudentMediaRequestResp
			{
				StudentMediaRequests = studentMediaRequestManager.LoadAllCompletedStudentMediaRequestByStudent(request.StudentId, request.StartDate, request.EndDate, request.CampusId).ToDTO()
			};
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003FF0 File Offset: 0x000021F0
		[DebuggerStepThrough]
		public Task<LoadAllStudentMediaRequestByStudentAndDatesResp> LoadAllStudentMediaRequestByStudentAndDatesAsync(LoadAllStudentMediaRequestByStudentAndDatesReq request)
		{
			StudentMediaRequestServiceManager.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__14 <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__ = new StudentMediaRequestServiceManager.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__14();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllStudentMediaRequestByStudentAndDatesResp>.Create();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>4__this = this;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.request = request;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>1__state = -1;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Start<StudentMediaRequestServiceManager.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__14>(ref <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__);
			return <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000403C File Offset: 0x0000223C
		public UpdateStudentContentMediaRequestInfoResp UpdateStudentContentMediaRequestInfo(UpdateStudentContentMediaRequestInfoReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			studentMediaRequestManager.UpdateStudentContentMediaRequestInfo(request.MediaContentRequestedInfo.ToDomainObject());
			return new UpdateStudentContentMediaRequestInfoResp();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004074 File Offset: 0x00002274
		public AddStudentContentMediaRequestInfoResp AddStudentContentMediaRequestInfo(AddStudentContentMediaRequestInfoReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new AddStudentContentMediaRequestInfoResp
			{
				MediaContentRequestedInfoId = studentMediaRequestManager.AddStudentContentMediaRequestInfo(request.MediaContentRequestedInfo.ToDomainObject())
			};
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000040B0 File Offset: 0x000022B0
		public DeleteStudentContentMediaRequestInfoResp DeleteStudentContentMediaRequestInfo(DeleteStudentContentMediaRequestInfoReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			studentMediaRequestManager.DeleteStudentContentMediaRequestInfo(request.MediaContentRequestedInfo.ToDomainObject());
			return new DeleteStudentContentMediaRequestInfoResp();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000040E8 File Offset: 0x000022E8
		public DeleteStudentContentMediaRequestInfoByIdResp DeleteStudentContentMediaRequestInfoById(DeleteStudentContentMediaRequestInfoByIdReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			studentMediaRequestManager.DeleteStudentContentMediaRequestInfo(request.MediaContentRequestInfoId);
			return new DeleteStudentContentMediaRequestInfoByIdResp();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004118 File Offset: 0x00002318
		public DownloadProofOfPurchaseResp DownloadProofOfPurchase(DownloadProofOfPurchaseReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new DownloadProofOfPurchaseResp
			{
				ProofOfPurchase = studentMediaRequestManager.DownloadProofOfPurchase(request.ProofOfPurchaseId).ToDTO()
			};
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004154 File Offset: 0x00002354
		[DebuggerStepThrough]
		public Task<DownloadProofOfPurchaseResp> DownloadProofOfPurchaseAsync(DownloadProofOfPurchaseReq request)
		{
			StudentMediaRequestServiceManager.<DownloadProofOfPurchaseAsync>d__20 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestServiceManager.<DownloadProofOfPurchaseAsync>d__20();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadProofOfPurchaseResp>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.request = request;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestServiceManager.<DownloadProofOfPurchaseAsync>d__20>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000041A0 File Offset: 0x000023A0
		public UploadProofOfPurchaseResp UploadProofOfPurchase(UploadProofOfPurchaseReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new UploadProofOfPurchaseResp
			{
				ProofOfPurchaseId = studentMediaRequestManager.UploadProofOfPurchase(request.ProofOfPurchaseInfo.ToDomainObject())
			};
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000041DC File Offset: 0x000023DC
		[DebuggerStepThrough]
		public Task<UploadProofOfPurchaseResp> UploadProofOfPurchaseAsync(UploadProofOfPurchaseReq request)
		{
			StudentMediaRequestServiceManager.<UploadProofOfPurchaseAsync>d__22 <UploadProofOfPurchaseAsync>d__ = new StudentMediaRequestServiceManager.<UploadProofOfPurchaseAsync>d__22();
			<UploadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadProofOfPurchaseResp>.Create();
			<UploadProofOfPurchaseAsync>d__.<>4__this = this;
			<UploadProofOfPurchaseAsync>d__.request = request;
			<UploadProofOfPurchaseAsync>d__.<>1__state = -1;
			<UploadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestServiceManager.<UploadProofOfPurchaseAsync>d__22>(ref <UploadProofOfPurchaseAsync>d__);
			return <UploadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004228 File Offset: 0x00002428
		public LoadAllMediaRequestInfoByJobIdResp LoadAllMediaRequestInfoByJobId(LoadAllMediaRequestInfoByJobIdReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadAllMediaRequestInfoByJobIdResp
			{
				MediaContentRequestedList = studentMediaRequestManager.LoadAllMediaRequestInfoByJobId(request.JobId).ToDTO()
			};
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004264 File Offset: 0x00002464
		public IsMediaContentAlreadyRequestedResp IsMediaContentAlreadyRequested(IsMediaContentAlreadyRequestedReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new IsMediaContentAlreadyRequestedResp
			{
				WasRequested = studentMediaRequestManager.IsMediaContentAlreadyRequested(request.StudentPersonId, request.Identifier.ToDomainObject())
			};
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000042A8 File Offset: 0x000024A8
		public LoadMediaContentRequestedInfoByIdResp LoadMediaContentRequestedInfoById(LoadMediaContentRequestedInfoByIdReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new LoadMediaContentRequestedInfoByIdResp
			{
				MediaContentRequestedInfo = studentMediaRequestManager.LoadMediaContentRequestedInfoById(request.MediaContentRequestedId).ToDTO()
			};
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000042E4 File Offset: 0x000024E4
		public AcceptProofOfPurchaseReceiptResp AcceptProofOfPurchaseReceipt(AcceptProofOfPurchaseReceiptReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new AcceptProofOfPurchaseReceiptResp
			{
				ProofOfPurchase = studentMediaRequestManager.AcceptProofOfPurchaseReceipt(request.ProofOfPurchase.ToDomainObject()).ToDTO()
			};
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004324 File Offset: 0x00002524
		public RejectProofOfPurchaseReceiptResp RejectProofOfPurchaseReceipt(RejectProofOfPurchaseReceiptReq request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(request.GetOperationContext());
			return new RejectProofOfPurchaseReceiptResp
			{
				Rejected = studentMediaRequestManager.RejectProofOfPurchaseReceipt(request.ProofOfPurchase.ToDomainObject())
			};
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004360 File Offset: 0x00002560
		public GetAllowedMediaContentFormatsForStudentToRequestResp GetAllowedMediaContentFormatsForStudentToRequest(GetAllowedMediaContentFormatsForStudentToRequestReq Request)
		{
			IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(Request.GetOperationContext());
			GetAllowedMediaContentFormatsForStudentToRequestResp getAllowedMediaContentFormatsForStudentToRequestResp = new GetAllowedMediaContentFormatsForStudentToRequestResp();
			IStudentMediaRequestManager studentMediaRequestManager2 = studentMediaRequestManager;
			int personId = Request.PersonId;
			MediaContentIdentifierDTO mediaContentIdentifier = Request.MediaContentIdentifier;
			getAllowedMediaContentFormatsForStudentToRequestResp.AllowedFormats = studentMediaRequestManager2.GetAllowedMediaContentFormatsForStudentToRequest(personId, (mediaContentIdentifier != null) ? mediaContentIdentifier.ToDomainObject() : null, Request.SelectedLuCourseId);
			return getAllowedMediaContentFormatsForStudentToRequestResp;
		}
	}
}
