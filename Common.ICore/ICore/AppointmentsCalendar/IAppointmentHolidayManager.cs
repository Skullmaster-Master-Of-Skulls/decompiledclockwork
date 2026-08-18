using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.AppointmentsCalendar
{
	// Token: 0x020000EB RID: 235
	public interface IAppointmentHolidayManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000755 RID: 1877
		IList<Holiday> LoadHolidays(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000756 RID: 1878
		Task<IList<Holiday>> LoadHolidaysAsync(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000757 RID: 1879
		int CreateHoliday(Holiday holiday);

		// Token: 0x06000758 RID: 1880
		void DeleteHoliday(int HolidayId);

		// Token: 0x06000759 RID: 1881
		void UpdateHoliday(Holiday Holiday);

		// Token: 0x0600075A RID: 1882
		IList<DateTime> LoadHolidayDatesOrDaysWithNoRoomAvailability(DateTime StartDate, DateTime EndDate, params int[] RoomPids);
	}
}
