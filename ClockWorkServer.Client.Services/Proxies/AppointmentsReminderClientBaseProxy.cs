using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsReminder;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000027 RID: 39
	internal class AppointmentsReminderClientBaseProxy : ClientBase<IAppointmentsReminder>, IAppointmentsReminder, IService
	{
		// Token: 0x0600021D RID: 541 RVA: 0x000075D8 File Offset: 0x000057D8
		public AppointmentsReminderClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000075E3 File Offset: 0x000057E3
		public AppointmentsReminderClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000075F0 File Offset: 0x000057F0
		public AddMeToExclusionListResp AddMeToExclusionList(AddMeToExclusionListReq request)
		{
			return base.Channel.AddMeToExclusionList(request);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007610 File Offset: 0x00005810
		public RemoveMeFromExclusionListResp RemoveMeFromExclusionList(RemoveMeFromExclusionListReq request)
		{
			return base.Channel.RemoveMeFromExclusionList(request);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007630 File Offset: 0x00005830
		public IsAppointmentReminderEnableResp IsAppointmentsReminderEnable(IsAppointmentReminderEnableReq request)
		{
			return base.Channel.IsAppointmentsReminderEnable(request);
		}
	}
}
