using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200005E RID: 94
	internal class CasesClientBaseProxy : ClientBase<ICases>, ICases, IService
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public CasesClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000C303 File Offset: 0x0000A503
		public CasesClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000C310 File Offset: 0x0000A510
		public LoadCasesForDisplayForStudentResp LoadCasesForDisplayForStudent(LoadCasesForDisplayForStudentReq Request)
		{
			return base.Channel.LoadCasesForDisplayForStudent(Request);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000C330 File Offset: 0x0000A530
		public LoadCaseByIdResp LoadCaseById(LoadCaseByIdReq Request)
		{
			return base.Channel.LoadCaseById(Request);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000C350 File Offset: 0x0000A550
		public CreateCaseResp CreateCase(CreateCaseReq Request)
		{
			return base.Channel.CreateCase(Request);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C36E File Offset: 0x0000A56E
		public void DeleteCase(DeleteCaseReq Request)
		{
			base.Channel.DeleteCase(Request);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C37E File Offset: 0x0000A57E
		public void UpdateCase(UpdateCaseReq Request)
		{
			base.Channel.UpdateCase(Request);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000C390 File Offset: 0x0000A590
		public LoadBasicAppointmentsByCaseResp LoadBasicAppointmentsByCase(LoadBasicAppointmentsByCaseReq Request)
		{
			return base.Channel.LoadBasicAppointmentsByCase(Request);
		}
	}
}
