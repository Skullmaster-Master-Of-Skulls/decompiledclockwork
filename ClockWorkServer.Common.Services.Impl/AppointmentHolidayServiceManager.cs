using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200000E RID: 14
	public class AppointmentHolidayServiceManager : IAppointmentHoliday, IService
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000464C File Offset: 0x0000284C
		public LoadHolidaysResp LoadHolidays(LoadHolidaysReq Request)
		{
			IAppointmentHolidayManager appointmentHolidayManager = new AppointmentHolidayManager(Request.GetOperationContext());
			IList<Holiday> list = appointmentHolidayManager.LoadHolidays(Request.StartDate, Request.EndDate);
			LoadHolidaysResp loadHolidaysResp = new LoadHolidaysResp();
			IList<HolidayDTO> holidays;
			if (list != null)
			{
				holidays = list.ToList<Holiday>().ConvertAll<HolidayDTO>((Holiday g) => g.ToDTO());
			}
			else
			{
				holidays = null;
			}
			loadHolidaysResp.Holidays = holidays;
			return loadHolidaysResp;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000046BC File Offset: 0x000028BC
		public CreateHolidayResp CreateHoliday(CreateHolidayReq Request)
		{
			IAppointmentHolidayManager appointmentHolidayManager = new AppointmentHolidayManager(Request.GetOperationContext());
			return new CreateHolidayResp
			{
				HolidayId = appointmentHolidayManager.CreateHoliday(Request.Holiday.ToDomainObject())
			};
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000046F8 File Offset: 0x000028F8
		public void DeleteHoliday(DeleteHolidayReq Request)
		{
			IAppointmentHolidayManager appointmentHolidayManager = new AppointmentHolidayManager(Request.GetOperationContext());
			appointmentHolidayManager.DeleteHoliday(Request.HolidayId);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004720 File Offset: 0x00002920
		public void UpdateHoliday(UpdateHolidayReq Request)
		{
			IAppointmentHolidayManager appointmentHolidayManager = new AppointmentHolidayManager(Request.GetOperationContext());
			appointmentHolidayManager.UpdateHoliday(Request.Holiday.ToDomainObject());
		}
	}
}
