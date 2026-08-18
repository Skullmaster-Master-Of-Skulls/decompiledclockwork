using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsReminder;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000026 RID: 38
	public class AppointmentsReminderReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentsReminder>, IAppointmentsReminder, IService
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00007516 File Offset: 0x00005716
		public AppointmentsReminderReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007521 File Offset: 0x00005721
		public AppointmentsReminderReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007530 File Offset: 0x00005730
		public AddMeToExclusionListResp AddMeToExclusionList(AddMeToExclusionListReq request)
		{
			return this.WrapServiceMethod<AddMeToExclusionListResp>(() => this.Proxy.AddMeToExclusionList(request));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007568 File Offset: 0x00005768
		public RemoveMeFromExclusionListResp RemoveMeFromExclusionList(RemoveMeFromExclusionListReq request)
		{
			return this.WrapServiceMethod<RemoveMeFromExclusionListResp>(() => this.Proxy.RemoveMeFromExclusionList(request));
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000075A0 File Offset: 0x000057A0
		public IsAppointmentReminderEnableResp IsAppointmentsReminderEnable(IsAppointmentReminderEnableReq request)
		{
			return this.WrapServiceMethod<IsAppointmentReminderEnableResp>(() => this.Proxy.IsAppointmentsReminderEnable(request));
		}
	}
}
