using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000017 RID: 23
	internal class StudentMediaRequestClientBaseProxy : ClientBase<IStudentMediaRequest>, IStudentMediaRequest, IService
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00005490 File Offset: 0x00003690
		public StudentMediaRequestClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000549B File Offset: 0x0000369B
		public StudentMediaRequestClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000054A8 File Offset: 0x000036A8
		public CreateStudentMediaResp CreateStudentMediaRequest(CreateStudentMediaReq request)
		{
			return base.Channel.CreateStudentMediaRequest(request);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000054C8 File Offset: 0x000036C8
		public UpdateStudentMediaResp UpdateStudentMediaRequest(UpdateStudentMediaReq request)
		{
			return base.Channel.UpdateStudentMediaRequest(request);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000054E8 File Offset: 0x000036E8
		public LoadStudentMediaRequestByIdResp LoadStudentMediaRequestById(LoadStudentMediaRequestByIdReq request)
		{
			return base.Channel.LoadStudentMediaRequestById(request);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005508 File Offset: 0x00003708
		public LoadStudentMediaRequestByStatusResp LoadStudentMediaRequestByStatus(LoadStudentMediaRequestByStatusReq request)
		{
			return base.Channel.LoadStudentMediaRequestByStatus(request);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005528 File Offset: 0x00003728
		public LoadAllApprovedMediaRequestResp LoadAllApprovedMediaRequest(LoadAllApprovedMediaRequestReq request)
		{
			return base.Channel.LoadAllApprovedMediaRequest(request);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005548 File Offset: 0x00003748
		public LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequest(LoadAllToBeApprovedMediaRequestReq request)
		{
			return base.Channel.LoadAllToBeApprovedMediaRequest(request);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005568 File Offset: 0x00003768
		public LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequestByStudent(LoadAllToBeApprovedMediaRequestByStudentReq request)
		{
			return base.Channel.LoadAllToBeApprovedMediaRequestByStudent(request);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005588 File Offset: 0x00003788
		public LoadAllInProgressStudentMediaRequestResp LoadAllInProgressStudentMediaRequest(LoadAllInProgressStudentMediaRequestReq request)
		{
			return base.Channel.LoadAllInProgressStudentMediaRequest(request);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000055A8 File Offset: 0x000037A8
		public LoadAllInProgressStudentMediaRequestByStudentResp LoadAllInProgressStudentMediaRequestByStudent(LoadAllInProgressStudentMediaRequestByStudentReq request)
		{
			return base.Channel.LoadAllInProgressStudentMediaRequestByStudent(request);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000055C8 File Offset: 0x000037C8
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequest(LoadAllCompletedStudentMediaRequestReq request)
		{
			return base.Channel.LoadAllCompletedStudentMediaRequest(request);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000055E8 File Offset: 0x000037E8
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByDate(LoadAllCompletedStudentMediaRequestByDateReq request)
		{
			return base.Channel.LoadAllCompletedStudentMediaRequestByDate(request);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005608 File Offset: 0x00003808
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudent(LoadAllCompletedStudentMediaRequestByStudentReq request)
		{
			return base.Channel.LoadAllCompletedStudentMediaRequestByStudent(request);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005628 File Offset: 0x00003828
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudentAndDate(LoadAllCompletedStudentMediaRequestByStudentAndDateReq request)
		{
			return base.Channel.LoadAllCompletedStudentMediaRequestByStudentAndDate(request);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005648 File Offset: 0x00003848
		[DebuggerStepThrough]
		public Task<LoadAllStudentMediaRequestByStudentAndDatesResp> LoadAllStudentMediaRequestByStudentAndDatesAsync(LoadAllStudentMediaRequestByStudentAndDatesReq request)
		{
			StudentMediaRequestClientBaseProxy.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__15 <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__ = new StudentMediaRequestClientBaseProxy.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__15();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllStudentMediaRequestByStudentAndDatesResp>.Create();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>4__this = this;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.request = request;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>1__state = -1;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Start<StudentMediaRequestClientBaseProxy.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__15>(ref <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__);
			return <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005694 File Offset: 0x00003894
		public UpdateStudentContentMediaRequestInfoResp UpdateStudentContentMediaRequestInfo(UpdateStudentContentMediaRequestInfoReq request)
		{
			return base.Channel.UpdateStudentContentMediaRequestInfo(request);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000056B4 File Offset: 0x000038B4
		public AddStudentContentMediaRequestInfoResp AddStudentContentMediaRequestInfo(AddStudentContentMediaRequestInfoReq request)
		{
			return base.Channel.AddStudentContentMediaRequestInfo(request);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000056D4 File Offset: 0x000038D4
		public DeleteStudentContentMediaRequestInfoResp DeleteStudentContentMediaRequestInfo(DeleteStudentContentMediaRequestInfoReq request)
		{
			return base.Channel.DeleteStudentContentMediaRequestInfo(request);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000056F4 File Offset: 0x000038F4
		public DeleteStudentContentMediaRequestInfoByIdResp DeleteStudentContentMediaRequestInfoById(DeleteStudentContentMediaRequestInfoByIdReq request)
		{
			return base.Channel.DeleteStudentContentMediaRequestInfoById(request);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005714 File Offset: 0x00003914
		public DownloadProofOfPurchaseResp DownloadProofOfPurchase(DownloadProofOfPurchaseReq request)
		{
			return base.Channel.DownloadProofOfPurchase(request);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005734 File Offset: 0x00003934
		[DebuggerStepThrough]
		public Task<DownloadProofOfPurchaseResp> DownloadProofOfPurchaseAsync(DownloadProofOfPurchaseReq request)
		{
			StudentMediaRequestClientBaseProxy.<DownloadProofOfPurchaseAsync>d__21 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestClientBaseProxy.<DownloadProofOfPurchaseAsync>d__21();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadProofOfPurchaseResp>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.request = request;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestClientBaseProxy.<DownloadProofOfPurchaseAsync>d__21>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005780 File Offset: 0x00003980
		public UploadProofOfPurchaseResp UploadProofOfPurchase(UploadProofOfPurchaseReq request)
		{
			return base.Channel.UploadProofOfPurchase(request);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000057A0 File Offset: 0x000039A0
		[DebuggerStepThrough]
		public Task<UploadProofOfPurchaseResp> UploadProofOfPurchaseAsync(UploadProofOfPurchaseReq request)
		{
			StudentMediaRequestClientBaseProxy.<UploadProofOfPurchaseAsync>d__23 <UploadProofOfPurchaseAsync>d__ = new StudentMediaRequestClientBaseProxy.<UploadProofOfPurchaseAsync>d__23();
			<UploadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadProofOfPurchaseResp>.Create();
			<UploadProofOfPurchaseAsync>d__.<>4__this = this;
			<UploadProofOfPurchaseAsync>d__.request = request;
			<UploadProofOfPurchaseAsync>d__.<>1__state = -1;
			<UploadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestClientBaseProxy.<UploadProofOfPurchaseAsync>d__23>(ref <UploadProofOfPurchaseAsync>d__);
			return <UploadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000057EC File Offset: 0x000039EC
		public LoadAllMediaRequestInfoByJobIdResp LoadAllMediaRequestInfoByJobId(LoadAllMediaRequestInfoByJobIdReq request)
		{
			return base.Channel.LoadAllMediaRequestInfoByJobId(request);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000580C File Offset: 0x00003A0C
		public IsMediaContentAlreadyRequestedResp IsMediaContentAlreadyRequested(IsMediaContentAlreadyRequestedReq request)
		{
			return base.Channel.IsMediaContentAlreadyRequested(request);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000582C File Offset: 0x00003A2C
		public LoadMediaContentRequestedInfoByIdResp LoadMediaContentRequestedInfoById(LoadMediaContentRequestedInfoByIdReq request)
		{
			return base.Channel.LoadMediaContentRequestedInfoById(request);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000584C File Offset: 0x00003A4C
		public AcceptProofOfPurchaseReceiptResp AcceptProofOfPurchaseReceipt(AcceptProofOfPurchaseReceiptReq request)
		{
			return base.Channel.AcceptProofOfPurchaseReceipt(request);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000586C File Offset: 0x00003A6C
		public RejectProofOfPurchaseReceiptResp RejectProofOfPurchaseReceipt(RejectProofOfPurchaseReceiptReq request)
		{
			return base.Channel.RejectProofOfPurchaseReceipt(request);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000588C File Offset: 0x00003A8C
		public GetAllowedMediaContentFormatsForStudentToRequestResp GetAllowedMediaContentFormatsForStudentToRequest(GetAllowedMediaContentFormatsForStudentToRequestReq Request)
		{
			return base.Channel.GetAllowedMediaContentFormatsForStudentToRequest(Request);
		}
	}
}
