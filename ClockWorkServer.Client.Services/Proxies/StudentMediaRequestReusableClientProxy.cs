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
	// Token: 0x02000016 RID: 22
	public class StudentMediaRequestReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentMediaRequest>, IStudentMediaRequest, IService
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00004E1A File Offset: 0x0000301A
		public StudentMediaRequestReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004E25 File Offset: 0x00003025
		public StudentMediaRequestReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004E34 File Offset: 0x00003034
		public CreateStudentMediaResp CreateStudentMediaRequest(CreateStudentMediaReq request)
		{
			return this.WrapServiceMethod<CreateStudentMediaResp>(() => this.Proxy.CreateStudentMediaRequest(request));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004E6C File Offset: 0x0000306C
		public UpdateStudentMediaResp UpdateStudentMediaRequest(UpdateStudentMediaReq request)
		{
			return this.WrapServiceMethod<UpdateStudentMediaResp>(() => this.Proxy.UpdateStudentMediaRequest(request));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004EA4 File Offset: 0x000030A4
		public LoadStudentMediaRequestByIdResp LoadStudentMediaRequestById(LoadStudentMediaRequestByIdReq request)
		{
			return this.WrapServiceMethod<LoadStudentMediaRequestByIdResp>(() => this.Proxy.LoadStudentMediaRequestById(request));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004EDC File Offset: 0x000030DC
		public LoadStudentMediaRequestByStatusResp LoadStudentMediaRequestByStatus(LoadStudentMediaRequestByStatusReq request)
		{
			return this.WrapServiceMethod<LoadStudentMediaRequestByStatusResp>(() => this.Proxy.LoadStudentMediaRequestByStatus(request));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004F14 File Offset: 0x00003114
		public LoadAllApprovedMediaRequestResp LoadAllApprovedMediaRequest(LoadAllApprovedMediaRequestReq request)
		{
			return this.WrapServiceMethod<LoadAllApprovedMediaRequestResp>(() => this.Proxy.LoadAllApprovedMediaRequest(request));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004F4C File Offset: 0x0000314C
		public LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequest(LoadAllToBeApprovedMediaRequestReq request)
		{
			return this.WrapServiceMethod<LoadAllToBeApprovedMediaRequestResp>(() => this.Proxy.LoadAllToBeApprovedMediaRequest(request));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004F84 File Offset: 0x00003184
		public LoadAllToBeApprovedMediaRequestResp LoadAllToBeApprovedMediaRequestByStudent(LoadAllToBeApprovedMediaRequestByStudentReq request)
		{
			return this.WrapServiceMethod<LoadAllToBeApprovedMediaRequestResp>(() => this.Proxy.LoadAllToBeApprovedMediaRequestByStudent(request));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004FBC File Offset: 0x000031BC
		public LoadAllInProgressStudentMediaRequestResp LoadAllInProgressStudentMediaRequest(LoadAllInProgressStudentMediaRequestReq request)
		{
			return this.WrapServiceMethod<LoadAllInProgressStudentMediaRequestResp>(() => this.Proxy.LoadAllInProgressStudentMediaRequest(request));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004FF4 File Offset: 0x000031F4
		public LoadAllInProgressStudentMediaRequestByStudentResp LoadAllInProgressStudentMediaRequestByStudent(LoadAllInProgressStudentMediaRequestByStudentReq request)
		{
			return this.WrapServiceMethod<LoadAllInProgressStudentMediaRequestByStudentResp>(() => this.Proxy.LoadAllInProgressStudentMediaRequestByStudent(request));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000502C File Offset: 0x0000322C
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequest(LoadAllCompletedStudentMediaRequestReq request)
		{
			return this.WrapServiceMethod<LoadAllCompletedStudentMediaRequestResp>(() => this.Proxy.LoadAllCompletedStudentMediaRequest(request));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005064 File Offset: 0x00003264
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByDate(LoadAllCompletedStudentMediaRequestByDateReq request)
		{
			return this.WrapServiceMethod<LoadAllCompletedStudentMediaRequestResp>(() => this.Proxy.LoadAllCompletedStudentMediaRequestByDate(request));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000509C File Offset: 0x0000329C
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudent(LoadAllCompletedStudentMediaRequestByStudentReq request)
		{
			return this.WrapServiceMethod<LoadAllCompletedStudentMediaRequestResp>(() => this.Proxy.LoadAllCompletedStudentMediaRequestByStudent(request));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000050D4 File Offset: 0x000032D4
		public LoadAllCompletedStudentMediaRequestResp LoadAllCompletedStudentMediaRequestByStudentAndDate(LoadAllCompletedStudentMediaRequestByStudentAndDateReq request)
		{
			return this.WrapServiceMethod<LoadAllCompletedStudentMediaRequestResp>(() => this.Proxy.LoadAllCompletedStudentMediaRequestByStudentAndDate(request));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000510C File Offset: 0x0000330C
		[DebuggerStepThrough]
		public Task<LoadAllStudentMediaRequestByStudentAndDatesResp> LoadAllStudentMediaRequestByStudentAndDatesAsync(LoadAllStudentMediaRequestByStudentAndDatesReq request)
		{
			StudentMediaRequestReusableClientProxy.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__15 <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__ = new StudentMediaRequestReusableClientProxy.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__15();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAllStudentMediaRequestByStudentAndDatesResp>.Create();
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>4__this = this;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.request = request;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>1__state = -1;
			<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Start<StudentMediaRequestReusableClientProxy.<LoadAllStudentMediaRequestByStudentAndDatesAsync>d__15>(ref <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__);
			return <LoadAllStudentMediaRequestByStudentAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005158 File Offset: 0x00003358
		public UpdateStudentContentMediaRequestInfoResp UpdateStudentContentMediaRequestInfo(UpdateStudentContentMediaRequestInfoReq request)
		{
			return this.WrapServiceMethod<UpdateStudentContentMediaRequestInfoResp>(() => this.Proxy.UpdateStudentContentMediaRequestInfo(request));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005190 File Offset: 0x00003390
		public AddStudentContentMediaRequestInfoResp AddStudentContentMediaRequestInfo(AddStudentContentMediaRequestInfoReq request)
		{
			return this.WrapServiceMethod<AddStudentContentMediaRequestInfoResp>(() => this.Proxy.AddStudentContentMediaRequestInfo(request));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000051C8 File Offset: 0x000033C8
		public DeleteStudentContentMediaRequestInfoResp DeleteStudentContentMediaRequestInfo(DeleteStudentContentMediaRequestInfoReq request)
		{
			return this.WrapServiceMethod<DeleteStudentContentMediaRequestInfoResp>(() => this.Proxy.DeleteStudentContentMediaRequestInfo(request));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005200 File Offset: 0x00003400
		public DeleteStudentContentMediaRequestInfoByIdResp DeleteStudentContentMediaRequestInfoById(DeleteStudentContentMediaRequestInfoByIdReq request)
		{
			return this.WrapServiceMethod<DeleteStudentContentMediaRequestInfoByIdResp>(() => this.Proxy.DeleteStudentContentMediaRequestInfoById(request));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005238 File Offset: 0x00003438
		public DownloadProofOfPurchaseResp DownloadProofOfPurchase(DownloadProofOfPurchaseReq request)
		{
			return this.WrapServiceMethod<DownloadProofOfPurchaseResp>(() => this.Proxy.DownloadProofOfPurchase(request));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005270 File Offset: 0x00003470
		[DebuggerStepThrough]
		public Task<DownloadProofOfPurchaseResp> DownloadProofOfPurchaseAsync(DownloadProofOfPurchaseReq request)
		{
			StudentMediaRequestReusableClientProxy.<DownloadProofOfPurchaseAsync>d__21 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestReusableClientProxy.<DownloadProofOfPurchaseAsync>d__21();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadProofOfPurchaseResp>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.request = request;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestReusableClientProxy.<DownloadProofOfPurchaseAsync>d__21>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000052BC File Offset: 0x000034BC
		public UploadProofOfPurchaseResp UploadProofOfPurchase(UploadProofOfPurchaseReq request)
		{
			return this.WrapServiceMethod<UploadProofOfPurchaseResp>(() => this.Proxy.UploadProofOfPurchase(request));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000052F4 File Offset: 0x000034F4
		[DebuggerStepThrough]
		public Task<UploadProofOfPurchaseResp> UploadProofOfPurchaseAsync(UploadProofOfPurchaseReq request)
		{
			StudentMediaRequestReusableClientProxy.<UploadProofOfPurchaseAsync>d__23 <UploadProofOfPurchaseAsync>d__ = new StudentMediaRequestReusableClientProxy.<UploadProofOfPurchaseAsync>d__23();
			<UploadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UploadProofOfPurchaseResp>.Create();
			<UploadProofOfPurchaseAsync>d__.<>4__this = this;
			<UploadProofOfPurchaseAsync>d__.request = request;
			<UploadProofOfPurchaseAsync>d__.<>1__state = -1;
			<UploadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestReusableClientProxy.<UploadProofOfPurchaseAsync>d__23>(ref <UploadProofOfPurchaseAsync>d__);
			return <UploadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005340 File Offset: 0x00003540
		public LoadAllMediaRequestInfoByJobIdResp LoadAllMediaRequestInfoByJobId(LoadAllMediaRequestInfoByJobIdReq request)
		{
			return this.WrapServiceMethod<LoadAllMediaRequestInfoByJobIdResp>(() => this.Proxy.LoadAllMediaRequestInfoByJobId(request));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005378 File Offset: 0x00003578
		public IsMediaContentAlreadyRequestedResp IsMediaContentAlreadyRequested(IsMediaContentAlreadyRequestedReq request)
		{
			return this.WrapServiceMethod<IsMediaContentAlreadyRequestedResp>(() => this.Proxy.IsMediaContentAlreadyRequested(request));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000053B0 File Offset: 0x000035B0
		public LoadMediaContentRequestedInfoByIdResp LoadMediaContentRequestedInfoById(LoadMediaContentRequestedInfoByIdReq request)
		{
			return this.WrapServiceMethod<LoadMediaContentRequestedInfoByIdResp>(() => this.Proxy.LoadMediaContentRequestedInfoById(request));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000053E8 File Offset: 0x000035E8
		public AcceptProofOfPurchaseReceiptResp AcceptProofOfPurchaseReceipt(AcceptProofOfPurchaseReceiptReq request)
		{
			return this.WrapServiceMethod<AcceptProofOfPurchaseReceiptResp>(() => this.Proxy.AcceptProofOfPurchaseReceipt(request));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005420 File Offset: 0x00003620
		public RejectProofOfPurchaseReceiptResp RejectProofOfPurchaseReceipt(RejectProofOfPurchaseReceiptReq request)
		{
			return this.WrapServiceMethod<RejectProofOfPurchaseReceiptResp>(() => this.Proxy.RejectProofOfPurchaseReceipt(request));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005458 File Offset: 0x00003658
		public GetAllowedMediaContentFormatsForStudentToRequestResp GetAllowedMediaContentFormatsForStudentToRequest(GetAllowedMediaContentFormatsForStudentToRequestReq Request)
		{
			return this.WrapServiceMethod<GetAllowedMediaContentFormatsForStudentToRequestResp>(() => this.Proxy.GetAllowedMediaContentFormatsForStudentToRequest(Request));
		}
	}
}
