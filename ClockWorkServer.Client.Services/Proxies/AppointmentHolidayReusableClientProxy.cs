using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200001C RID: 28
	public class AppointmentHolidayReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentHoliday>, IAppointmentHoliday, IService
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005BFE File Offset: 0x00003DFE
		public AppointmentHolidayReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005C09 File Offset: 0x00003E09
		public AppointmentHolidayReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005C18 File Offset: 0x00003E18
		public CreateHolidayResp CreateHoliday(CreateHolidayReq Request)
		{
			return this.WrapServiceMethod<CreateHolidayResp>(() => this.Proxy.CreateHoliday(Request));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005C50 File Offset: 0x00003E50
		public void DeleteHoliday(DeleteHolidayReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteHoliday(Request);
			});
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005C88 File Offset: 0x00003E88
		public LoadHolidaysResp LoadHolidays(LoadHolidaysReq Request)
		{
			return this.WrapServiceMethod<LoadHolidaysResp>(() => this.Proxy.LoadHolidays(Request));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public void UpdateHoliday(UpdateHolidayReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateHoliday(Request);
			});
		}
	}
}
