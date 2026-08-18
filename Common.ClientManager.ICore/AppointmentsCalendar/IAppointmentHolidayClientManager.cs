using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar
{
	// Token: 0x02000097 RID: 151
	public interface IAppointmentHolidayClientManager : IWebService
	{
		// Token: 0x060004AD RID: 1197
		IList<HolidayDTO> LoadHolidays(DateTime StartDate, DateTime EndDate);

		// Token: 0x060004AE RID: 1198
		int CreateHoliday(HolidayDTO holiday);

		// Token: 0x060004AF RID: 1199
		void DeleteHoliday(int HolidayId);

		// Token: 0x060004B0 RID: 1200
		void UpdateHoliday(HolidayDTO Holiday);
	}
}
