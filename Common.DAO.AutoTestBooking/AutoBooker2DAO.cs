using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;

namespace TechnoPro.Common.DAO.AutoTestBooking
{
	// Token: 0x02000002 RID: 2
	public class AutoBooker2DAO : IAutoBooker2DAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public OperationContext OpContext { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public AutoBooker2DAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		public bool DoesStudentHaveAnExistingTestWithClassDateMatching(int pid, int lucid, DateTime classDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucid),
				databaseLayer.GetParameter("@d1", DbType.DateTime, classDate.Date),
				databaseLayer.GetParameter("@d2", DbType.DateTime, classDate.Date.AddDays(1.0))
			};
			object obj = databaseLayer.ExecuteScalar("SELECT\tapp.AppointmentID\r\nFROM\tattendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID\r\n\t\tLEFT JOIN exams e ON e.examid=app.examid\r\nWHERE\tatt.PersonID=@pid AND NOT app.examid IS NULL\r\n\t\tAND app.cancelled=0\r\n\t\tAND e.lucourseid=@lucid\r\n\t\tAND e.dateoftest>=@d1 AND e.dateoftest<@d2", parameters);
			return obj != null && obj != DBNull.Value && obj is int && (int)obj > 0;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000213C File Offset: 0x0000033C
		public IList<TryToBookAvailability> LoadStudentAppointments(int pid, DateTime date, int AppIdToIgnoreWhenCheckingStudentsSchedule)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string query = "DECLARE @dt2 datetime\r\nSET @dt2=DATEADD(day,1,@dt)\r\nSELECT    att.appointmentid,app.startdate,app.enddate\r\nFROM      attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid \r\nWHERE     att.personid=@pid AND NOT app.appointmentid IS NULL AND NOT app.appointmentid=@appidtoignore AND app.cancelled=0\r\n\t\t  AND app.startdate>=@dt AND app.startdate<@dt2\r\nORDER BY app.startdate";
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@dt", DbType.DateTime, date.Date),
				databaseLayer.GetParameter("@appidtoignore", DbType.Int32, AppIdToIgnoreWhenCheckingStudentsSchedule)
			};
			List<TryToBookAvailability> list = new List<TryToBookAvailability>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				if (dataReader != null)
				{
					List<int> list2 = new List<int>();
					while (dataReader.Read())
					{
						int num = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
						if (num > 0 && !list2.Contains(num))
						{
							list.Add(new TryToBookAvailability
							{
								StartDateTime = (DateTime)dataReader["startdate"],
								EndDateTime = (DateTime)dataReader["enddate"]
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002270 File Offset: 0x00000470
		public int GetNumberOfTestsAndExamsStudentHasInADay(int pid, int lucid, DateTime date)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DateTime date2 = date.Date;
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucid),
				databaseLayer.GetParameter("@dt1", DbType.DateTime, date2),
				databaseLayer.GetParameter("@dt2", DbType.DateTime, date2.AddDays(1.0))
			};
			string query = "SELECT    COUNT(app.appointmentid) AS num\r\nFROM        appointments app \r\nWHERE       NOT app.examid IS NULL \r\n            AND app.startdate>=@dt1 AND app.startdate<@dt2 \r\n            AND app.appointmentid IN (SELECT appointmentid FROM attendees WHERE personid=@pid)";
			object obj = databaseLayer.ExecuteScalar(query, parameters);
			if (obj != null && obj != DBNull.Value && obj is int)
			{
				return (int)obj;
			}
			throw new DatabaseSelectFailedException(string.Concat(new string[]
			{
				"Invalid count from AutoBooker2DAO:GetNumberOfTestsAndExamsStudentHasInADay:pid=",
				pid.ToString(),
				":lucid=",
				lucid.ToString(),
				":date=",
				date.ToString("yyyy-MM-dd")
			}));
		}
	}
}
