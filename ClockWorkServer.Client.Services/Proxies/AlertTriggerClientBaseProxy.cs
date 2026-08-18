using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000005 RID: 5
	internal class AlertTriggerClientBaseProxy : ClientBase<IAlertTrigger>, IAlertTrigger, IService
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002680 File Offset: 0x00000880
		public AlertTriggerClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000268B File Offset: 0x0000088B
		public AlertTriggerClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002698 File Offset: 0x00000898
		public CheckForTriggerAlertsResp CheckForTriggerAlerts(CheckForTriggerAlertsReq Request)
		{
			return base.Channel.CheckForTriggerAlerts(Request);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026B8 File Offset: 0x000008B8
		public AllowedToBookAppointmentForStudentResp AllowedToBookAppointmentForStudent(AllowedToBookAppointmentForStudentReq Request)
		{
			return base.Channel.AllowedToBookAppointmentForStudent(Request);
		}
	}
}
