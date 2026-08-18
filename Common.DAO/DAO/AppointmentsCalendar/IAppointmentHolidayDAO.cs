using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.AppointmentsCalendar
{
	// Token: 0x020000C6 RID: 198
	public interface IAppointmentHolidayDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000560 RID: 1376
		IList<Holiday> LoadAllHolidays();

		// Token: 0x06000561 RID: 1377
		Task<IList<Holiday>> LoadAllHolidaysAsync();

		// Token: 0x06000562 RID: 1378
		int CreateHoliday(Holiday holiday);

		// Token: 0x06000563 RID: 1379
		Task<int> CreateHolidayAsync(Holiday holiday);

		// Token: 0x06000564 RID: 1380
		void DeleteHoliday(int HolidayId);

		// Token: 0x06000565 RID: 1381
		void UpdateHoliday(Holiday Holiday);

		// Token: 0x06000566 RID: 1382
		[Obsolete("Don't use this - it's only currently used to convert old legacy recurring schedule datatable to holiday")]
		IList<OldRecurringSchedule> LoadOldRecurringSchedule();

		// Token: 0x06000567 RID: 1383
		Task<IList<OldRecurringSchedule>> LoadOldRecurringScheduleAsync();

		// Token: 0x06000568 RID: 1384
		IList<DateTime> LoadDaysWithNoRoomAvailability(DateTime StartDate, DateTime EndDate, IList<DateTime> datesToSkip, params int[] RoomPids);
	}
}
