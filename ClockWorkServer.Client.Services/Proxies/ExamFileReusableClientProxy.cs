using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200002C RID: 44
	public class ExamFileReusableClientProxy : WCFTokenBasedReusableClientProxy<IExamFile>, IExamFile, IService
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000812E File Offset: 0x0000632E
		public ExamFileReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008139 File Offset: 0x00006339
		public ExamFileReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008148 File Offset: 0x00006348
		public CreateExamFileResp CreateExamFile(CreateExamFileReq Request)
		{
			return this.WrapServiceMethod<CreateExamFileResp>(() => this.Proxy.CreateExamFile(Request));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008180 File Offset: 0x00006380
		public DeleteExamFileResp DeleteExamFile(DeleteExamFileReq Request)
		{
			return this.WrapServiceMethod<DeleteExamFileResp>(() => this.Proxy.DeleteExamFile(Request));
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000081B8 File Offset: 0x000063B8
		public LoadExamFileByIdResp LoadExamFileById(LoadExamFileByIdReq Request)
		{
			return this.WrapServiceMethod<LoadExamFileByIdResp>(() => this.Proxy.LoadExamFileById(Request));
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000081F0 File Offset: 0x000063F0
		public LoadExamFilesByExamResp LoadExamFilesByExam(LoadExamFilesByExamReq Request)
		{
			return this.WrapServiceMethod<LoadExamFilesByExamResp>(() => this.Proxy.LoadExamFilesByExam(Request));
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008228 File Offset: 0x00006428
		public LoadExamFilesByExamCheckProfAltContactPermissionsResp LoadExamFilesByExamCheckProfAltContactPermissions(LoadExamFilesByExamCheckProfAltContactPermissionsReq Request)
		{
			return this.WrapServiceMethod<LoadExamFilesByExamCheckProfAltContactPermissionsResp>(() => this.Proxy.LoadExamFilesByExamCheckProfAltContactPermissions(Request));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008260 File Offset: 0x00006460
		public LoadExamFileByIdCheckProfAltContactPermissionsResp LoadExamFileByIdCheckProfAltContactPermissions(LoadExamFileByIdCheckProfAltContactPermissionsReq Request)
		{
			return this.WrapServiceMethod<LoadExamFileByIdCheckProfAltContactPermissionsResp>(() => this.Proxy.LoadExamFileByIdCheckProfAltContactPermissions(Request));
		}
	}
}
