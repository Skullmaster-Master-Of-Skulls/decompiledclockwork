using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsReminder;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;

namespace TechnoPro.Common.DAO.Impl.AppointmentsReminder
{
	// Token: 0x0200015A RID: 346
	public class AppointmentsReminderDAO : IAppointmentsReminderDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x00069E34 File Offset: 0x00068034
		public AppointmentsReminderDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00069E48 File Offset: 0x00068048
		public IList<AppointmentReminder> LoadAppointmentsReminder()
		{
			List<AppointmentReminder> list = new List<AppointmentReminder>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("EXECUTE sp_LoadAppointmentsReminder"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						AppointmentReminder appointmentReminderFromReader = this.GetAppointmentReminderFromReader(dataReader, batchDecryptor);
						bool flag2 = appointmentReminderFromReader != null;
						if (flag2)
						{
							list.Add(appointmentReminderFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00069EF0 File Offset: 0x000680F0
		public void ChangeAppointmentsReminderNotificationStatus(IList<int> appReminderIdList, bool alreadyNotified)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appreminderids", DbType.String, appReminderIdList.CommaSeparatedValues<int>()),
				databaseLayer.GetParameter("@alreadynotified", DbType.Boolean, alreadyNotified)
			};
			databaseLayer.ExecuteStoredProcedure("sp_ChangeAppointmentsReminderStatus", parameters);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00069F58 File Offset: 0x00068158
		public bool AddPeopleToExclusionList(int personId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@personid", DbType.Int32, personId)
			};
			return databaseLayer.ExecuteNonQuery("if not exists (select 1 from AppointmentsReminder_PeopleExclusionList where PersonID=@personid)\r\n                begin\r\n                    insert into AppointmentsReminder_PeopleExclusionList (PersonID) values (@personid)\r\n                end", parameters) > 0;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00069FB0 File Offset: 0x000681B0
		public bool RemovePeopleFromExclusionList(int personId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@personid", DbType.Int32, personId)
			};
			return databaseLayer.ExecuteNonQuery("delete from AppointmentsReminder_PeopleExclusionList where PersonID=@personid", parameters) > 0;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0006A008 File Offset: 0x00068208
		public bool IsPersonInExclusionList(int personId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@personid", DbType.Int32, personId)
			};
			return databaseLayer.ExecuteScalar("select PersonID from [AppointmentsReminder_PeopleExclusionList] where PersonID=@personid", parameters) != null;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0006A060 File Offset: 0x00068260
		public IList<int> LoadPeopleExclusionList()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<int> list = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select PersonID from AppointmentsReminder_PeopleExclusionList"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						list.Add(dataReader.GetInt32(0));
					}
				}
			}
			return list;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0006A0E8 File Offset: 0x000682E8
		public IList<int> LoadGroupInclusionList()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<int> list = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select GroupID from AppointmentsReminder_GroupInclusionList"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						list.Add(dataReader.GetInt32(0));
					}
				}
			}
			return list;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0006A170 File Offset: 0x00068370
		public int AddAppointmentReminder(AppointmentReminder appReminder)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@appreminderid", DbType.Int32, 0),
				databaseLayer.GetParameter("@appointmentid", DbType.Int32, appReminder.AppointmentID),
				databaseLayer.GetParameter("@personid", DbType.Int32, appReminder.AttendeePersonID),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, appReminder.StartDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, appReminder.EndDate),
				databaseLayer.GetParameter("@subject", DbType.String, string.IsNullOrEmpty(appReminder.Subject) ? string.Empty : appReminder.Subject),
				databaseLayer.GetParameter("@notificationdatetime", DbType.DateTime, appReminder.NotificationDatetime)
			};
			databaseLayer.ExecuteNonQuery("insert into AppointmentsReminder_Notification \r\n\t\t            (AppointmentID, PersonID, startDate, endDate, [Subject], NotificationDatetime)\r\n            values\t(@appointmentid, @personid, @startdate, @enddate, @subject, @notificationdatetime)\r\n            set @appreminderid = SCOPE_IDENTITY()", array);
			return appReminder.AppointmentReminderID = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0006A298 File Offset: 0x00068498
		public void UpdateAppointmentReminder(AppointmentReminder appReminder)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appointmentid", DbType.Int32, appReminder.AppointmentID),
				databaseLayer.GetParameter("@personid", DbType.Int32, appReminder.AttendeePersonID),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, appReminder.StartDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, appReminder.EndDate),
				databaseLayer.GetParameter("@subject", DbType.String, string.IsNullOrEmpty(appReminder.Subject) ? string.Empty : appReminder.Subject),
				databaseLayer.GetParameter("@notificationdatetime", DbType.DateTime, appReminder.NotificationDatetime)
			};
			databaseLayer.ExecuteNonQuery("update AppointmentsReminder_Notification\r\n                set\t startDate\t\t\t\t= @startdate\r\n\t            ,endDate\t\t\t\t= @enddate\r\n\t            ,[Subject]\t\t\t\t= @subject\r\n\t            ,NotificationDatetime\t= @notificationdatetime\r\n            where AppointmentID = @appointmentid and PersonID = @personid", parameters);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0006A384 File Offset: 0x00068584
		public void DeleteAppointmentReminder(int appointmentID, int attPersonID)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appointmentid", DbType.Int32, appointmentID),
				databaseLayer.GetParameter("@personid", DbType.Int32, attPersonID)
			};
			databaseLayer.ExecuteNonQuery("update AppointmentsReminder_Notification\r\n            set\t WasDeleted = 1\r\n            where AppointmentID = @appointmentid and PersonID = @personid\r\n", parameters);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0006A3EC File Offset: 0x000685EC
		private AppointmentReminder GetAppointmentReminderFromReader(IDataReader reader, IBatchDecryptor decryptor)
		{
			return new AppointmentReminder
			{
				AppointmentReminderID = (int)reader["AppointmentReminderID"],
				AppointmentID = (int)reader["AppointmentID"],
				StartDate = (DateTime)reader["startDate"],
				EndDate = (DateTime)reader["endDate"],
				Subject = ((reader["Subject"] is DBNull) ? string.Empty : decryptor.Decrypt((byte[])reader["Subject"])),
				AlreadyNotified = (bool)reader["AlreadyNotified"],
				WasDeleted = (bool)reader["WasDeleted"],
				NotificationDatetime = (DateTime)reader["NotificationDatetime"],
				AttendeePersonID = (int)reader["PersonID"]
			};
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0006A4F3 File Offset: 0x000686F3
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0006A4FB File Offset: 0x000686FB
		public OperationContext OpContext { get; set; }
	}
}
