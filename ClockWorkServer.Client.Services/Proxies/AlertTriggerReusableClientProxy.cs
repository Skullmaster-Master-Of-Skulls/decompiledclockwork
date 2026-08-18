using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000004 RID: 4
	public class AlertTriggerReusableClientProxy : WCFTokenBasedReusableClientProxy<IAlertTrigger>, IAlertTrigger, IService
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000025F7 File Offset: 0x000007F7
		public AlertTriggerReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002602 File Offset: 0x00000802
		public AlertTriggerReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002610 File Offset: 0x00000810
		public CheckForTriggerAlertsResp CheckForTriggerAlerts(CheckForTriggerAlertsReq Request)
		{
			return this.WrapServiceMethod<CheckForTriggerAlertsResp>(() => this.Proxy.CheckForTriggerAlerts(Request));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002648 File Offset: 0x00000848
		public AllowedToBookAppointmentForStudentResp AllowedToBookAppointmentForStudent(AllowedToBookAppointmentForStudentReq Request)
		{
			return this.WrapServiceMethod<AllowedToBookAppointmentForStudentResp>(() => this.Proxy.AllowedToBookAppointmentForStudent(Request));
		}
	}
}
