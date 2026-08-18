using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.AppointmentsRecurring;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.Common.DAO.Impl.AppointmentsRecurring
{
	// Token: 0x02000121 RID: 289
	public class RecurringAppointmentDAO : IRecurringAppointmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x00054030 File Offset: 0x00052230
		public RecurringAppointmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00054060 File Offset: 0x00052260
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x00054068 File Offset: 0x00052268
		public OperationContext OpContext { get; set; }

		// Token: 0x06000836 RID: 2102 RVA: 0x00054074 File Offset: 0x00052274
		public AppointmentRecurringInfo LoadCurrentRecurringAppointmentsSet(int MasterGroupCode)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, MasterGroupCode)
			};
			AppointmentRecurringInfo result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE a.groupcode=@groupcode ORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					AppointmentRecurringInfo appointmentRecurringInfo = new AppointmentRecurringInfo
					{
						Appointments = new List<RecurringAppointment>(),
						MasterGroupCode = MasterGroupCode
					};
					RecurringAppointment recurringAppointment = null;
					while (dataReader.Read())
					{
						int num = (dataReader["appointmentid"] == DBNull.Value) ? 0 : ((int)dataReader["appointmentid"]);
						bool flag2 = recurringAppointment == null || recurringAppointment.AppointmentId != num;
						if (flag2)
						{
							recurringAppointment = BaseAppointmentDAO.GetMainBaseBasicAppointment<RecurringAppointment>(dataReader, this.OpContext);
							appointmentRecurringInfo.Appointments.Add(recurringAppointment);
						}
						int num2 = (dataReader["personid"] == DBNull.Value) ? 0 : ((int)dataReader["personid"]);
						bool flag3 = num2 > 0;
						if (flag3)
						{
							Attendee attendee = AppointmentAttendeeDAO.GetAttendeeFromRecord(dataReader, this.OpContext, "", null);
							bool flag4 = attendee != null && recurringAppointment.Attendees.Find((Attendee f) => f.Person.PersonId == attendee.Person.PersonId) == null;
							if (flag4)
							{
								recurringAppointment.Attendees.Add(attendee);
							}
						}
					}
					result = appointmentRecurringInfo;
				}
			}
			return result;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0005421C File Offset: 0x0005241C
		public void UpdateRecurringGroupCode(int AppointmentId, int GroupCode, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, GroupCode)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET groupcode=@groupcode WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0005427C File Offset: 0x0005247C
		public IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			IDictionary<int, bool> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC sp_Calendar_AllowedAppointmentsToEditFromRecurringSet @appid,@pid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
					while (dataReader.Read())
					{
						int key = (int)dataReader["appointmentid"];
						bool value = (bool)dataReader["isAllowed"];
						bool flag2 = !dictionary.ContainsKey(key);
						if (flag2)
						{
							dictionary.Add(key, value);
						}
						else
						{
							dictionary[key] = false;
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x040004DE RID: 1246
		private DatabaseLayer DatabaseManager;
	}
}
