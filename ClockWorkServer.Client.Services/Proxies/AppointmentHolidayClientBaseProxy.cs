using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200001D RID: 29
	internal class AppointmentHolidayClientBaseProxy : ClientBase<IAppointmentHoliday>, IAppointmentHoliday, IService
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005CF5 File Offset: 0x00003EF5
		public AppointmentHolidayClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005D00 File Offset: 0x00003F00
		public AppointmentHolidayClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005D0C File Offset: 0x00003F0C
		public CreateHolidayResp CreateHoliday(CreateHolidayReq Request)
		{
			return base.Channel.CreateHoliday(Request);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005D2A File Offset: 0x00003F2A
		public void DeleteHoliday(DeleteHolidayReq Request)
		{
			base.Channel.DeleteHoliday(Request);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005D3C File Offset: 0x00003F3C
		public LoadHolidaysResp LoadHolidays(LoadHolidaysReq Request)
		{
			return base.Channel.LoadHolidays(Request);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005D5A File Offset: 0x00003F5A
		public void UpdateHoliday(UpdateHolidayReq Request)
		{
			base.Channel.UpdateHoliday(Request);
		}
	}
}
