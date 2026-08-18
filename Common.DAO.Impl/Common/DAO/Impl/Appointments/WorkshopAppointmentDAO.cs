using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsWorkshops;
using TechnoPro.Common.DAO.Impl.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000138 RID: 312
	public class WorkshopAppointmentDAO : IWorkshopAppointmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0005CF0E File Offset: 0x0005B10E
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0005CF16 File Offset: 0x0005B116
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000905 RID: 2309 RVA: 0x0005CF1F File Offset: 0x0005B11F
		public WorkshopAppointmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0005CF4A File Offset: 0x0005B14A
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0005CF52 File Offset: 0x0005B152
		public OperationContext OpContext { get; set; }

		// Token: 0x06000908 RID: 2312 RVA: 0x0005CF5C File Offset: 0x0005B15C
		private void LoadWorkshopAppointmentInfo(WorkshopAppointment app)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, app.AppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tw.workshopid,w.apptypeid,w.workshopTitle,w.workshopDescription,w.MaxAttendees,w.WorkshopFee,\r\n\t\tw.partners,w.note,w.location,w.availableForOnlineBooking,w.waitingListMaxUsers,\r\n\t\tat.[description] as apptypedescription,at.appointmentTypeGroupID,at.defaultColour,at.isWorkshop,at.isCourse,\r\n\t\tatg.AppointmentTypeGroupID,atg.title as apptypegrouptitle,\r\n\t\tw.personID AS p1personid,p.firstName AS p1firstname,p.lastName AS p1lastname,p.student_no AS p1student_no,\r\n\t\tw.personID2 AS p2personid,p2.firstName AS p2firstname,p2.lastName AS p2lastname,p2.student_no AS p2student_no,\r\n\t\tw.personID3 AS p3personid,p3.firstName AS p3firstname,p3.lastName AS p3lastname,p3.student_no AS p3student_no,\r\n        aw.maxattendees AS maxattendeescount\r\nFROM\tappointmentworkshops aw LEFT JOIN Workshops w ON w.workshopid=aw.workshopid\r\n        LEFT JOIN AppointmentTypes at ON at.AppTypeID=w.AppTypeID \r\n        LEFT JOIN AppointmentTypeGroups atg ON at.appointmentTypeGroupID=atg.AppointmentTypeGroupID\r\n\t\tLEFT JOIN people p ON p.PersonID=w.personID\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=w.personID2\r\n\t\tLEFT JOIN people p3 ON p3.PersonID=w.personID3\r\nWHERE   aw.appointmentid=@appid\r\nORDER BY at.[description],w.AppTypeID,w.workshopTitle", parameters))
			{
				bool flag = dataReader == null;
				if (!flag)
				{
					bool flag2 = dataReader.Read();
					if (flag2)
					{
						app.WorkshopDefinition = WorkshopDefinitionDAO.GetWorkshopDefinitionFromReader(dataReader, this.OpContext);
						app.MaxAttendeeCount = ((dataReader["maxattendeescount"] == DBNull.Value) ? 0 : ((int)dataReader["maxattendeescount"]));
					}
					IAppointmentIconDAO appointmentIconDAO = new AppointmentIconDAO(this.OpContext);
					app.Icons = appointmentIconDAO.LoadAppointmentIconsByAppointment(app.AppointmentId);
				}
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0005D038 File Offset: 0x0005B238
		private void LoadWorkshopAppointmentInfo(WorkshopAppointment app, IBatchDecryptor batchDecryptor, IDictionary<int, IList<AppointmentIcon>> appIcons)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, app.AppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tw.workshopid,w.apptypeid,w.workshopTitle,w.workshopDescription,w.MaxAttendees,w.WorkshopFee,\r\n\t\tw.partners,w.note,w.location,w.availableForOnlineBooking,w.waitingListMaxUsers,\r\n\t\tat.[description] as apptypedescription,at.appointmentTypeGroupID,at.defaultColour,at.isWorkshop,at.isCourse,\r\n\t\tatg.AppointmentTypeGroupID,atg.title as apptypegrouptitle,\r\n\t\tw.personID AS p1personid,p.firstName AS p1firstname,p.lastName AS p1lastname,p.student_no AS p1student_no,\r\n\t\tw.personID2 AS p2personid,p2.firstName AS p2firstname,p2.lastName AS p2lastname,p2.student_no AS p2student_no,\r\n\t\tw.personID3 AS p3personid,p3.firstName AS p3firstname,p3.lastName AS p3lastname,p3.student_no AS p3student_no,\r\n        aw.maxattendees AS maxattendeescount\r\nFROM\tappointmentworkshops aw LEFT JOIN Workshops w ON w.workshopid=aw.workshopid\r\n        LEFT JOIN AppointmentTypes at ON at.AppTypeID=w.AppTypeID \r\n        LEFT JOIN AppointmentTypeGroups atg ON at.appointmentTypeGroupID=atg.AppointmentTypeGroupID\r\n\t\tLEFT JOIN people p ON p.PersonID=w.personID\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=w.personID2\r\n\t\tLEFT JOIN people p3 ON p3.PersonID=w.personID3\r\nWHERE   aw.appointmentid=@appid\r\nORDER BY at.[description],w.AppTypeID,w.workshopTitle", parameters))
			{
				bool flag = dataReader == null;
				if (!flag)
				{
					bool flag2 = dataReader.Read();
					if (flag2)
					{
						app.WorkshopDefinition = WorkshopDefinitionDAO.GetWorkshopDefinitionFromReader(dataReader, this.OpContext, batchDecryptor);
						app.MaxAttendeeCount = ((dataReader["maxattendeescount"] == DBNull.Value) ? 0 : ((int)dataReader["maxattendeescount"]));
					}
					IList<AppointmentIcon> icons;
					if (!appIcons.ContainsKey(app.AppointmentId))
					{
						IList<AppointmentIcon> list = new List<AppointmentIcon>();
						icons = list;
					}
					else
					{
						icons = appIcons[app.AppointmentId];
					}
					app.Icons = icons;
				}
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00003998 File Offset: 0x00001B98
		private void CreateWorkshopInfo(Appointment app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00003998 File Offset: 0x00001B98
		private void UpdateWorkshopInfo(Appointment app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0005D124 File Offset: 0x0005B324
		private void UpdateWorkshopAppointmentInfo(WorkshopAppointment app, DbTransaction transaction = null)
		{
			int appointmentId = app.AppointmentId;
			WorkshopDefinition workshopDefinition = app.WorkshopDefinition;
			WorkshopAppointmentDAO.UpdateWorkshopAppointmentInfo(appointmentId, (workshopDefinition != null) ? workshopDefinition.WorkshopId : 0, app.MaxAttendeeCount, this.OpContext, transaction);
			IAppointmentIconDAO appointmentIconDAO = new AppointmentIconDAO(this.OpContext);
			IAppointmentIconDAO appointmentIconDAO2 = appointmentIconDAO;
			int appointmentId2 = app.AppointmentId;
			IList<AppointmentIcon> icons = app.Icons;
			IList<int> list;
			if (icons == null)
			{
				list = null;
			}
			else
			{
				list = icons.ToList<AppointmentIcon>().ConvertAll<int>((AppointmentIcon f) => f.Icon.IconNum);
			}
			appointmentIconDAO2.DeleteAppointmentIconsNotInList(appointmentId2, list ?? new List<int>(), transaction);
			bool flag = app.Icons == null;
			if (!flag)
			{
				foreach (AppointmentIcon icon in app.Icons)
				{
					appointmentIconDAO.InsertOrUpdateAppointmentIcon(app.AppointmentId, icon, transaction);
				}
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0005D210 File Offset: 0x0005B410
		public static void UpdateWorkshopAppointmentInfo(int AppointmentId, int WorkshopId, int MaxAttendeeCount, OperationContext opContext, DbTransaction transaction = null)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@workshopid", DbType.Int32, WorkshopId),
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@maxattendees", DbType.Int32, MaxAttendeeCount)
			};
			databaseLayer.ExecuteNonQuery("IF exists (SELECT 1 from appointmentworkshops where appointmentid=@appid)\r\n\tbegin\r\n\t\tUPDATE appointmentworkshops SET workshopid=@workshopid,maxattendees=@maxattendees WHERE appointmentid=@appid\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tinsert INTO appointmentworkshops (appointmentid, workshopid, maxattendees) VALUES(@appid, @workshopid, @maxattendees)\r\n\tend", parameters);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0005D288 File Offset: 0x0005B488
		public IList<WorkshopAppointment> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId, IList<int> AllowedAppTypeIds)
		{
			DbParameter[] array = new DbParameter[4];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.AddDays(1.0));
			int num = 2;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeids";
			DbType pType = DbType.String;
			object value;
			if (AllowedAppTypeIds != null)
			{
				value = string.Join(",", AllowedAppTypeIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			array[3] = this.DatabaseManager.GetParameter("@workshopid", DbType.Int32, WorkshopId);
			DbParameter[] parameters = array;
			List<WorkshopAppointment> list = new List<WorkshopAppointment>();
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE   at.isWorkshop=1\r\n\t\tAND a.startdate >= @startdate AND a.startdate < @enddate\r\n        AND a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,','))\r\n        AND (@workshopid IS NULL OR @workshopid < 1 OR a.appointmentid IN (SELECT appointmentid FROM appointmentworkshops WHERE workshopid=@workshopid))\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					WorkshopAppointment workshopAppointment = null;
					while (dataReader.Read())
					{
						int num2 = (int)dataReader["appointmentid"];
						bool flag2 = workshopAppointment == null || workshopAppointment.AppointmentId != num2;
						if (flag2)
						{
							workshopAppointment = BaseAppointmentDAO.GetMainBaseExtendedAppointment<WorkshopAppointment>(dataReader, this.OpContext, batchDecryptor);
							list.Add(workshopAppointment);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, workshopAppointment, this.OpContext, batchDecryptor);
					}
				}
			}
			IAppointmentIconDAO appointmentIconDAO = new AppointmentIconDAO(this.OpContext);
			IDictionary<int, IList<AppointmentIcon>> appIcons = appointmentIconDAO.LoadAppointmentIconsByAppointments((from g in list
			select g.AppointmentId).Distinct<int>().ToArray<int>());
			foreach (WorkshopAppointment app in list)
			{
				this.LoadWorkshopAppointmentInfo(app, batchDecryptor, appIcons);
			}
			return (from f in list
			where f.WorkshopDefinition != null && f.WorkshopDefinition.WorkshopId == WorkshopId
			select f).ToList<WorkshopAppointment>();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0005D4D8 File Offset: 0x0005B6D8
		public WorkshopAppointment LoadWorkshopAppointmentById(int AppointmentId, IList<int> AllowedAppTypeIds)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			WorkshopAppointment workshopAppointment = baseAppointmentDAO.LoadBaseExtendedAppointmentById<WorkshopAppointment>(AppointmentId);
			bool flag = workshopAppointment == null;
			WorkshopAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				this.LoadWorkshopAppointmentInfo(workshopAppointment);
				bool flag2 = workshopAppointment.AppType == null || !AllowedAppTypeIds.Contains(workshopAppointment.AppType.AppTypeId);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = workshopAppointment;
				}
			}
			return result;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0005D53C File Offset: 0x0005B73C
		public void UpdateWorkshopAppointment(WorkshopAppointment WorkshopApp)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateBaseExtendedAppointment(WorkshopApp, null);
			this.UpdateWorkshopAppointmentInfo(WorkshopApp, null);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0005D568 File Offset: 0x0005B768
		public int CreateWorkshopAppointment(WorkshopAppointment WorkshopApp)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			int result = baseAppointmentDAO.CreateBaseExtendedAppointment(WorkshopApp, null);
			this.UpdateWorkshopAppointmentInfo(WorkshopApp, null);
			return result;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0005D59C File Offset: 0x0005B79C
		public IList<WorkshopAppointment> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.AddDays(1.0)),
				this.DatabaseManager.GetParameter("@apptypeids", DbType.String, appTypeId.ToString())
			};
			List<WorkshopAppointment> list = new List<WorkshopAppointment>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE   at.isWorkshop=1\r\n\t\tAND a.startdate >= @startdate AND a.startdate < @enddate\r\n        AND a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,','))\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					WorkshopAppointment workshopAppointment = null;
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = workshopAppointment == null || workshopAppointment.AppointmentId != num;
						if (flag2)
						{
							workshopAppointment = BaseAppointmentDAO.GetMainBaseExtendedAppointment<WorkshopAppointment>(dataReader, this.OpContext, batchDecryptor);
							list.Add(workshopAppointment);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, workshopAppointment, this.OpContext, batchDecryptor);
					}
				}
			}
			foreach (WorkshopAppointment app in list)
			{
				this.LoadWorkshopAppointmentInfo(app);
			}
			return (from f in list
			where f.WorkshopDefinition == null || f.WorkshopDefinition.WorkshopId <= 0
			select f).ToList<WorkshopAppointment>();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0005D748 File Offset: 0x0005B948
		public void UpdateAppointmentWorkshopId(int AppointmentId, int NewWorkshopId)
		{
			bool flag = NewWorkshopId < 1;
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
				};
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentworkshops WHERE appointmentid=@appid", parameters);
			}
			else
			{
				DbParameter[] parameters2 = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
					this.DatabaseManager.GetParameter("@workshopid", DbType.Int32, NewWorkshopId)
				};
				this.DatabaseManager.ExecuteNonQuery("IF exists (SELECT 1 from appointmentworkshops where appointmentid=@appid)\r\n\tbegin\r\n\t\tUPDATE appointmentworkshops SET workshopid=@workshopid WHERE appointmentid=@appid\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tinsert INTO appointmentworkshops (appointmentid, workshopid) VALUES(@appid, @workshopid)\r\n\tend", parameters2);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0005D7E4 File Offset: 0x0005B9E4
		public void UpdateWorkshopAppointmentMaxAttendees(int appointmentId, int newMaxAttendees)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@maxattendees", DbType.Int32, newMaxAttendees)
			};
			this.DatabaseManager.ExecuteNonQuery("IF exists (SELECT 1 from appointmentworkshops where appointmentid=@appid)\r\n\tbegin\r\n\t\tUPDATE appointmentworkshops SET maxattendees=@maxattendees WHERE appointmentid=@appid\r\n\tend\r\nelse\r\n\tbegin\r\n        DECLARE @primaryAppId int = (SELECT groupcode FROM appointments WHERE appointmentid=@appid)\r\n        DECLARE @workshopId int = (SELECT workshopid FROM appointmentworkshops WHERE appointmentid=@primaryAppId)\r\n        IF NOT @workshopId IS NULL\r\n\t\t    insert INTO appointmentworkshops (appointmentid, workshopid, maxattendees) VALUES(@appid, @workshopId, @maxattendees)\r\n\tend", parameters);
		}
	}
}
