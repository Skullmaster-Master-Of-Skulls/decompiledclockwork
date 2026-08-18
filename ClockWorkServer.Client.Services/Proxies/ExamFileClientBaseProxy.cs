using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200002D RID: 45
	internal class ExamFileClientBaseProxy : ClientBase<IExamFile>, IExamFile, IService
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00008298 File Offset: 0x00006498
		public ExamFileClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000082A3 File Offset: 0x000064A3
		public ExamFileClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000082B0 File Offset: 0x000064B0
		public CreateExamFileResp CreateExamFile(CreateExamFileReq Request)
		{
			return base.Channel.CreateExamFile(Request);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000082D0 File Offset: 0x000064D0
		public DeleteExamFileResp DeleteExamFile(DeleteExamFileReq Request)
		{
			return base.Channel.DeleteExamFile(Request);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000082F0 File Offset: 0x000064F0
		public LoadExamFileByIdResp LoadExamFileById(LoadExamFileByIdReq Request)
		{
			return base.Channel.LoadExamFileById(Request);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008310 File Offset: 0x00006510
		public LoadExamFilesByExamResp LoadExamFilesByExam(LoadExamFilesByExamReq Request)
		{
			return base.Channel.LoadExamFilesByExam(Request);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008330 File Offset: 0x00006530
		public LoadExamFilesByExamCheckProfAltContactPermissionsResp LoadExamFilesByExamCheckProfAltContactPermissions(LoadExamFilesByExamCheckProfAltContactPermissionsReq Request)
		{
			return base.Channel.LoadExamFilesByExamCheckProfAltContactPermissions(Request);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008350 File Offset: 0x00006550
		public LoadExamFileByIdCheckProfAltContactPermissionsResp LoadExamFileByIdCheckProfAltContactPermissions(LoadExamFileByIdCheckProfAltContactPermissionsReq Request)
		{
			return base.Channel.LoadExamFileByIdCheckProfAltContactPermissions(Request);
		}
	}
}
