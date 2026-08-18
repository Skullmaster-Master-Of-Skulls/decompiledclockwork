using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.DAO.Impl.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AppointmentsCalendar
{
	// Token: 0x0200014A RID: 330
	public class AppointmentHolidayManager : IAppointmentHolidayManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0006F8F4 File Offset: 0x0006DAF4
		public AppointmentHolidayManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentHolidayDAO(opContext);
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0006F912 File Offset: 0x0006DB12
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x0006F91A File Offset: 0x0006DB1A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0006F924 File Offset: 0x0006DB24
		private void ConvertOldRecurringScheduleToHoliday()
		{
			try
			{
				IAppointmentHolidayDAO appointmentHolidayDAO = new AppointmentHolidayDAO(this.OpContext);
				IList<OldRecurringSchedule> list = appointmentHolidayDAO.LoadOldRecurringSchedule();
				List<Holiday> list2 = new List<Holiday>();
				foreach (OldRecurringSchedule oldRecurringSchedule in list)
				{
					bool flag = oldRecurringSchedule.ActiveStartDate != null && oldRecurringSchedule.ActiveEndDate != null;
					DateTime date;
					DateTime t;
					if (flag)
					{
						date = oldRecurringSchedule.ActiveStartDate.Value.Date;
						t = oldRecurringSchedule.ActiveEndDate.Value.Date.AddDays(1.0).AddMinutes(-1.0);
					}
					else
					{
						date = new DateTime(DateTime.Now.Year - 5, 1, 1);
						t = new DateTime(DateTime.Now.Year + 2, 12, 31, 23, 59, 0);
					}
					DateTime t2 = date;
					switch (oldRecurringSchedule.EveryTypeCode)
					{
					case 2:
					{
						DayOfWeek dayOfWeek = oldRecurringSchedule.StartDateTime.DayOfWeek;
						while (t2.DayOfWeek != dayOfWeek)
						{
							t2 = t2.AddDays(1.0);
						}
						break;
					}
					case 3:
						try
						{
							DateTime dateTime = new DateTime(t2.Year, t2.Month, oldRecurringSchedule.StartDateTime.Day);
							t2 = dateTime;
						}
						catch
						{
							t2 = new DateTime(t2.Year, t2.Month, 1).AddMonths(1).AddDays(-1.0);
						}
						break;
					case 4:
						t2 = new DateTime(t2.Year, oldRecurringSchedule.StartDateTime.Month, oldRecurringSchedule.StartDateTime.Day);
						break;
					}
					List<DateTime> list3 = new List<DateTime>();
					int num = 0;
					while (t2 <= t)
					{
						num++;
						list3.Add(t2.Date);
						switch (oldRecurringSchedule.EveryTypeCode)
						{
						case 1:
							t2 = t2.AddDays((double)oldRecurringSchedule.MultiplyBy);
							break;
						case 2:
							t2 = t2.AddDays((double)(oldRecurringSchedule.MultiplyBy * 7));
							break;
						case 3:
							t2 = t2.AddMonths(oldRecurringSchedule.MultiplyBy);
							break;
						case 4:
							t2 = t2.AddYears(oldRecurringSchedule.MultiplyBy);
							break;
						default:
							t2 = t2.AddYears(100);
							break;
						}
					}
					string description = "Auto-converted from old format: " + DateTime.Now.ToString("yyyy-MM-dd");
					foreach (DateTime dateTime2 in list3)
					{
						list2.Add(new Holiday
						{
							Title = (oldRecurringSchedule.Description ?? ""),
							Description = description,
							Date = dateTime2.Date
						});
					}
					foreach (Holiday holiday in list2)
					{
						appointmentHolidayDAO.CreateHoliday(holiday);
					}
				}
				IMiscCodeManager miscCodeManager = new MiscCodeManager(this.OpContext);
				miscCodeManager.SaveMiscCodeValue(eMiscCode.FinishedConvertingOldRecurringScheduleToHolidays, DateTime.Now.ToString("yyyy-MM-dd"));
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ConvertOldRecurringScheduleToHoliday: {0}", ex.ToString());
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0006FD88 File Offset: 0x0006DF88
		[DebuggerStepThrough]
		private Task ConvertOldRecurringScheduleToHolidayAsync()
		{
			AppointmentHolidayManager.<ConvertOldRecurringScheduleToHolidayAsync>d__7 <ConvertOldRecurringScheduleToHolidayAsync>d__ = new AppointmentHolidayManager.<ConvertOldRecurringScheduleToHolidayAsync>d__7();
			<ConvertOldRecurringScheduleToHolidayAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ConvertOldRecurringScheduleToHolidayAsync>d__.<>4__this = this;
			<ConvertOldRecurringScheduleToHolidayAsync>d__.<>1__state = -1;
			<ConvertOldRecurringScheduleToHolidayAsync>d__.<>t__builder.Start<AppointmentHolidayManager.<ConvertOldRecurringScheduleToHolidayAsync>d__7>(ref <ConvertOldRecurringScheduleToHolidayAsync>d__);
			return <ConvertOldRecurringScheduleToHolidayAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0006FDCC File Offset: 0x0006DFCC
		private void ClearAllHolidaysInCache()
		{
			string key = "holidays";
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(key);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0006FDF0 File Offset: 0x0006DFF0
		public IList<Holiday> LoadHolidays(DateTime StartDate, DateTime EndDate)
		{
			IAppointmentHolidayDAO appointmentHolidayDAO = new AppointmentHolidayDAO(this.OpContext);
			string key = "holidays";
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<Holiday> list = (IList<Holiday>)cacheStorageManager[key];
			bool flag = list == null;
			if (flag)
			{
				list = appointmentHolidayDAO.LoadAllHolidays();
				bool flag2 = list != null;
				if (flag2)
				{
					cacheStorageManager.Insert(key, list, TimeSpan.FromHours(1.0));
				}
			}
			bool flag3 = list != null && list.Count < 1;
			if (flag3)
			{
				IMiscCodeManager miscCodeManager = new MiscCodeManager(this.OpContext);
				string value = miscCodeManager.LoadMiscCodeValue(eMiscCode.FinishedConvertingOldRecurringScheduleToHolidays);
				bool flag4 = string.IsNullOrEmpty(value);
				if (flag4)
				{
					this.ConvertOldRecurringScheduleToHoliday();
				}
				list = appointmentHolidayDAO.LoadAllHolidays();
				bool flag5 = list != null;
				if (flag5)
				{
					cacheStorageManager.Insert(key, list, TimeSpan.FromHours(1.0));
				}
			}
			bool flag6 = list == null;
			IList<Holiday> result;
			if (flag6)
			{
				result = new List<Holiday>();
			}
			else
			{
				result = (from g in list
				where g.Date >= StartDate.Date && g.Date < EndDate.Date.AddDays(1.0)
				select g).ToList<Holiday>();
			}
			return result;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0006FF18 File Offset: 0x0006E118
		[DebuggerStepThrough]
		public Task<IList<Holiday>> LoadHolidaysAsync(DateTime StartDate, DateTime EndDate)
		{
			AppointmentHolidayManager.<LoadHolidaysAsync>d__10 <LoadHolidaysAsync>d__ = new AppointmentHolidayManager.<LoadHolidaysAsync>d__10();
			<LoadHolidaysAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<Holiday>>.Create();
			<LoadHolidaysAsync>d__.<>4__this = this;
			<LoadHolidaysAsync>d__.StartDate = StartDate;
			<LoadHolidaysAsync>d__.EndDate = EndDate;
			<LoadHolidaysAsync>d__.<>1__state = -1;
			<LoadHolidaysAsync>d__.<>t__builder.Start<AppointmentHolidayManager.<LoadHolidaysAsync>d__10>(ref <LoadHolidaysAsync>d__);
			return <LoadHolidaysAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0006FF6C File Offset: 0x0006E16C
		public int CreateHoliday(Holiday holiday)
		{
			int num = this.dao.CreateHoliday(holiday);
			bool flag = num > 0;
			if (flag)
			{
				this.ClearAllHolidaysInCache();
			}
			return num;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0006FF9B File Offset: 0x0006E19B
		public void DeleteHoliday(int HolidayId)
		{
			this.dao.DeleteHoliday(HolidayId);
			this.ClearAllHolidaysInCache();
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0006FFB2 File Offset: 0x0006E1B2
		public void UpdateHoliday(Holiday Holiday)
		{
			this.dao.UpdateHoliday(Holiday);
			this.ClearAllHolidaysInCache();
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0006FFCC File Offset: 0x0006E1CC
		public IList<DateTime> LoadHolidayDatesOrDaysWithNoRoomAvailability(DateTime StartDate, DateTime EndDate, params int[] RoomPids)
		{
			IList<Holiday> source = this.LoadHolidays(StartDate, EndDate);
			List<DateTime> list = (from g in source
			select g.Date).ToList<DateTime>();
			List<DateTime> second = this.LoadDaysWithNoRoomAvailability(StartDate, EndDate, list, RoomPids).ToList<DateTime>();
			return (from g in list.Union(second)
			select g.Date).Distinct<DateTime>().ToList<DateTime>();
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00070058 File Offset: 0x0006E258
		private IList<DateTime> LoadDaysWithNoRoomAvailability(DateTime StartDate, DateTime EndDate, IList<DateTime> datesToSkip, params int[] RoomPids)
		{
			return this.dao.LoadDaysWithNoRoomAvailability(StartDate, EndDate, datesToSkip, RoomPids);
		}

		// Token: 0x040002B3 RID: 691
		private IAppointmentHolidayDAO dao;
	}
}
