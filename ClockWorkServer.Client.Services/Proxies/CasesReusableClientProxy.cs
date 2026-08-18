using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200005D RID: 93
	public class CasesReusableClientProxy : WCFTokenBasedReusableClientProxy<ICases>, ICases, IService
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0000C18F File Offset: 0x0000A38F
		public CasesReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C19A File Offset: 0x0000A39A
		public CasesReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		public LoadCasesForDisplayForStudentResp LoadCasesForDisplayForStudent(LoadCasesForDisplayForStudentReq Request)
		{
			return this.WrapServiceMethod<LoadCasesForDisplayForStudentResp>(() => this.Proxy.LoadCasesForDisplayForStudent(Request));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public LoadCaseByIdResp LoadCaseById(LoadCaseByIdReq Request)
		{
			return this.WrapServiceMethod<LoadCaseByIdResp>(() => this.Proxy.LoadCaseById(Request));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000C218 File Offset: 0x0000A418
		public CreateCaseResp CreateCase(CreateCaseReq Request)
		{
			return this.WrapServiceMethod<CreateCaseResp>(() => this.Proxy.CreateCase(Request));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000C250 File Offset: 0x0000A450
		public void DeleteCase(DeleteCaseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteCase(Request);
			});
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000C288 File Offset: 0x0000A488
		public void UpdateCase(UpdateCaseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateCase(Request);
			});
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public LoadBasicAppointmentsByCaseResp LoadBasicAppointmentsByCase(LoadBasicAppointmentsByCaseReq Request)
		{
			return this.WrapServiceMethod<LoadBasicAppointmentsByCaseResp>(() => this.Proxy.LoadBasicAppointmentsByCase(Request));
		}
	}
}
