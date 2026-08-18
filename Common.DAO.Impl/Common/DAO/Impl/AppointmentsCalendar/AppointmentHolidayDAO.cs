using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000160 RID: 352
	public class AppointmentHolidayDAO : IAppointmentHolidayDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x0006C181 File Offset: 0x0006A381
		public AppointmentHolidayDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0006C193 File Offset: 0x0006A393
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0006C19B File Offset: 0x0006A39B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A4E RID: 2638 RVA: 0x0006C1A4 File Offset: 0x0006A3A4
		private Holiday GetHolidayFromRecord(IDataReader record)
		{
			bool flag = record == null || record["dt"] is DBNull || record["holidayid"] is DBNull;
			Holiday result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new Holiday
				{
					HolidayId = (int)record["holidayid"],
					Date = (DateTime)record["dt"],
					Title = record["title"].ToString(),
					Description = record["description"].ToString()
				};
			}
			return result;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0006C24C File Offset: 0x0006A44C
		public IList<Holiday> LoadAllHolidays()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<Holiday> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT holidayid,title,description,dt FROM AppointmentsHolidays ORDER BY dt"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Holiday> list = new List<Holiday>();
					while (dataReader.Read())
					{
						Holiday holidayFromRecord = this.GetHolidayFromRecord(dataReader);
						bool flag2 = holidayFromRecord != null;
						if (flag2)
						{
							list.Add(holidayFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0006C2E4 File Offset: 0x0006A4E4
		[DebuggerStepThrough]
		public Task<IList<Holiday>> LoadAllHolidaysAsync()
		{
			AppointmentHolidayDAO.<LoadAllHolidaysAsync>d__7 <LoadAllHolidaysAsync>d__ = new AppointmentHolidayDAO.<LoadAllHolidaysAsync>d__7();
			<LoadAllHolidaysAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<Holiday>>.Create();
			<LoadAllHolidaysAsync>d__.<>4__this = this;
			<LoadAllHolidaysAsync>d__.<>1__state = -1;
			<LoadAllHolidaysAsync>d__.<>t__builder.Start<AppointmentHolidayDAO.<LoadAllHolidaysAsync>d__7>(ref <LoadAllHolidaysAsync>d__);
			return <LoadAllHolidaysAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0006C328 File Offset: 0x0006A528
		public int CreateHoliday(Holiday holiday)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@holidayid", DbType.Int32, 0),
				databaseLayer.GetParameter("@title", DbType.String, holiday.Title ?? ""),
				databaseLayer.GetParameter("@description", DbType.String, holiday.Description ?? ""),
				databaseLayer.GetParameter("@dt", DbType.DateTime, holiday.Date)
			};
			databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT holidayid FROM appointmentsholidays WHERE title=@title AND dt=@dt)\r\nBEGIN\r\n    INSERT INTO appointmentsholidays (title,description,dt) VALUES (@title,@description,@dt);\r\n    SET @holidayid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS holidayid)\r\nEND", array);
			bool flag = array[0].Value == null || array[0].Value is DBNull;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)array[0].Value;
			}
			return result;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0006C404 File Offset: 0x0006A604
		[DebuggerStepThrough]
		public Task<int> CreateHolidayAsync(Holiday holiday)
		{
			AppointmentHolidayDAO.<CreateHolidayAsync>d__9 <CreateHolidayAsync>d__ = new AppointmentHolidayDAO.<CreateHolidayAsync>d__9();
			<CreateHolidayAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<CreateHolidayAsync>d__.<>4__this = this;
			<CreateHolidayAsync>d__.holiday = holiday;
			<CreateHolidayAsync>d__.<>1__state = -1;
			<CreateHolidayAsync>d__.<>t__builder.Start<AppointmentHolidayDAO.<CreateHolidayAsync>d__9>(ref <CreateHolidayAsync>d__);
			return <CreateHolidayAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0006C450 File Offset: 0x0006A650
		[Obsolete("Don't use this - it's only currently used to convert old legacy recurring schedule datatable to holiday")]
		public IList<OldRecurringSchedule> LoadOldRecurringSchedule()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<OldRecurringSchedule> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT * FROM recurringschedule WHERE personid<0 AND isworkinghours=0"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OldRecurringSchedule> list = new List<OldRecurringSchedule>();
					while (dataReader.Read())
					{
						list.Add(new OldRecurringSchedule
						{
							ActiveStartDate = ((dataReader["activestartdate"] is DBNull) ? null : new DateTime?((DateTime)dataReader["activestartdate"])),
							ActiveEndDate = ((dataReader["activeenddate"] is DBNull) ? null : new DateTime?((DateTime)dataReader["activeenddate"])),
							Description = dataReader["description"].ToString().Trim(),
							StartDateTime = (DateTime)dataReader["startdatetime"],
							EndDateTime = (DateTime)dataReader["enddatetime"],
							EveryTypeCode = ((dataReader["everytypecode"] is DBNull) ? 0 : ((int)dataReader["everytypecode"])),
							MultiplyBy = Math.Max((dataReader["multiplyby"] is DBNull) ? 1 : ((int)dataReader["multiplyby"]), 1)
						});
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0006C614 File Offset: 0x0006A814
		[DebuggerStepThrough]
		[Obsolete("Don't use this - it's only currently used to convert old legacy recurring schedule datatable to holiday")]
		public Task<IList<OldRecurringSchedule>> LoadOldRecurringScheduleAsync()
		{
			AppointmentHolidayDAO.<LoadOldRecurringScheduleAsync>d__11 <LoadOldRecurringScheduleAsync>d__ = new AppointmentHolidayDAO.<LoadOldRecurringScheduleAsync>d__11();
			<LoadOldRecurringScheduleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OldRecurringSchedule>>.Create();
			<LoadOldRecurringScheduleAsync>d__.<>4__this = this;
			<LoadOldRecurringScheduleAsync>d__.<>1__state = -1;
			<LoadOldRecurringScheduleAsync>d__.<>t__builder.Start<AppointmentHolidayDAO.<LoadOldRecurringScheduleAsync>d__11>(ref <LoadOldRecurringScheduleAsync>d__);
			return <LoadOldRecurringScheduleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0006C658 File Offset: 0x0006A858
		public void DeleteHoliday(int HolidayId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@holidayid", DbType.Int32, HolidayId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM appointmentsholidays WHERE holidayid=@holidayid", parameters);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0006C6AC File Offset: 0x0006A8AC
		public void UpdateHoliday(Holiday Holiday)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@holidayid", DbType.Int32, Holiday.HolidayId),
				databaseLayer.GetParameter("@title", DbType.String, Holiday.Title ?? ""),
				databaseLayer.GetParameter("@description", DbType.String, Holiday.Description ?? ""),
				databaseLayer.GetParameter("@dt", DbType.DateTime, Holiday.Date)
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointmentsholidays SET title=@title,description=@description,dt=@dt WHERE holidayid=@holidayid", parameters);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<DateTime> LoadDaysWithNoRoomAvailability(DateTime StartDate, DateTime EndDate, IList<DateTime> datesToSkip, params int[] RoomPids)
		{
			throw new NotImplementedException();
		}
	}
}
