using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.DAO.Impl.AppointmentSync
{
	// Token: 0x0200013C RID: 316
	public class ClockWorkSyncDAO : IClockWorkSyncDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0005D841 File Offset: 0x0005BA41
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0005D849 File Offset: 0x0005BA49
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0005D852 File Offset: 0x0005BA52
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0005D85A File Offset: 0x0005BA5A
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000919 RID: 2329 RVA: 0x0005D863 File Offset: 0x0005BA63
		public ClockWorkSyncDAO(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0005D894 File Offset: 0x0005BA94
		private void InsertUpdateMemo(ClockWorkSyncAppointment oldAppointment, ClockWorkSyncAppointment newAppointment)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, newAppointment.AppointmentId),
				this.DatabaseManager.GetParameter("@memotext", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(newAppointment.GetMemoRtf()))
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT appmemoid FROM appointmentmemos WHERE appointmentid=@appid)\r\n    UPDATE appointmentmemos SET memotext=@memotext,isencrypted=1 WHERE appointmentid=@appid\r\nELSE\r\n    INSERT INTO appointmentmemos(appointmentid,memotext,isencrypted) VALUES (@appid,@memotext,1)", parameters);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0005D908 File Offset: 0x0005BB08
		private void CreateMemo(ClockWorkSyncAppointment app)
		{
			bool flag = !string.IsNullOrEmpty(app.Memo);
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, app.AppointmentId),
					this.DatabaseManager.GetParameter("@memotext", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(app.GetMemoRtf()))
				};
				this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointmentmemos (appointmentid,memotext,isencrypted) VALUES (@appid,@memotext,1)", parameters);
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0005D990 File Offset: 0x0005BB90
		private void UpdateSyncAttendees(int AppointmentId, List<ClockWorkSyncAttendee> OldAttendees, List<ClockWorkSyncAttendee> NewAttendees)
		{
			CWLogger.Logger.Debug("ClockWorkSyncDAO::UpdateSyncAttendees: OldAttendess=" + OldAttendees.CommaSeparatedValues<ClockWorkSyncAttendee>() + " NewAttendess=" + NewAttendees.CommaSeparatedValues<ClockWorkSyncAttendee>());
			List<ClockWorkExternalApplicationSyncUser> allSyncUsers = new List<ClockWorkExternalApplicationSyncUser>(this.OpContext.SyncSettings.SyncUsers);
			allSyncUsers.AddRange(this.OpContext.SyncSettings.DisabledSyncUsers);
			bool flag = OldAttendees != null;
			if (flag)
			{
				List<ClockWorkSyncAttendee> list = OldAttendees.FindAll((ClockWorkSyncAttendee oa) => oa.AttendeeId > 0 && allSyncUsers.Any((ClockWorkExternalApplicationSyncUser su) => su.ClockWorkUser.PersonId == oa.Attendee.PersonId) && NewAttendees.Find((ClockWorkSyncAttendee nn) => nn.Attendee.PersonId == oa.Attendee.PersonId) == null);
				foreach (ClockWorkSyncAttendee clockWorkSyncAttendee in list)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
						this.DatabaseManager.GetParameter("@pid", DbType.Int32, clockWorkSyncAttendee.Attendee.PersonId)
					};
					this.DatabaseManager.ExecuteNonQuery("DELETE FROM attendees WHERE appointmentid=@appid AND personid=@pid", parameters);
				}
			}
			foreach (ClockWorkSyncAttendee clockWorkSyncAttendee2 in NewAttendees)
			{
				DbParameter[] parameters2 = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, clockWorkSyncAttendee2.Attendee.PersonId),
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
					this.DatabaseManager.GetParameter("@noshow", DbType.Boolean, clockWorkSyncAttendee2.IsNoShow),
					this.DatabaseManager.GetParameter("@misccode", DbType.Int32, clockWorkSyncAttendee2.MiscCode)
				};
				CWLogger.Logger.Debug(string.Format("ClockWorkSyncDAO::UpdateSyncAttendees: Inserting/updating attendee pid={0} into appid={1}", clockWorkSyncAttendee2.Attendee.PersonId, AppointmentId));
				DataTable dataTable = this.DatabaseManager.ExecuteQuery("DECLARE @rm bit\r\nIF EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=3)\r\n\tSET @rm = 1\r\nELSE\r\n\tSET @rm = 0\r\nIF NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\nBEGIN\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,@noshow,@misccode);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS attendeeid\r\nEND\r\nELSE\r\nBEGIN\r\n    UPDATE attendees SET noshow=@noshow,misccode=@misccode WHERE appointmentid=@appid AND personid=@pid;\r\n    SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid;\r\nEND", parameters2);
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					clockWorkSyncAttendee2.AttendeeId = (int)dataTable.Rows[0][0];
					CWLogger.Logger.Debug(string.Format("ClockWorkSyncDAO::UpdateSyncAttendees: Attendee inserted/updated attid={0}", clockWorkSyncAttendee2.AttendeeId));
				}
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		private ClockWorkSyncAppointment GetSyncAppointmentFromRecord(IDataReader record, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			ClockWorkSyncAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = batchDecryptor == null;
				if (flag2)
				{
					eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
					SyncOperationContext opContext = this.OpContext;
					batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
				}
				int num = (int)record["appointmentid"];
				object obj = record["memotext"];
				object obj2 = record["isencrypted"];
				object obj3 = record["location"];
				object obj4 = record["subtitle"];
				object obj5 = record["lastdatemodified"];
				string text = string.Empty;
				try
				{
					bool flag3 = obj2 != DBNull.Value && Convert.ToBoolean(obj2);
					bool flag4 = obj == DBNull.Value;
					if (flag4)
					{
						text = "";
					}
					else
					{
						bool flag5 = flag3;
						if (flag5)
						{
							text = this.DatabaseManager.Encryption.Decrypt((byte[])obj);
						}
						else
						{
							UTF8Encoding utf8Encoding = new UTF8Encoding();
							text = utf8Encoding.GetString((byte[])obj);
						}
					}
					bool flag6 = text == null;
					if (flag6)
					{
						text = "";
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("memo({0}): {1}", num, ex), ex);
					throw ex;
				}
				string text2 = string.Empty;
				try
				{
					text2 = ((obj3 == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])obj3));
				}
				catch (Exception ex2)
				{
					CWLogger.Logger.ErrorException(string.Format("location({0}): {1}", num, ex2), ex2);
					throw ex2;
				}
				int num2 = text.IndexOf("{\\rtf1\\");
				bool flag7 = num2 > 0;
				if (flag7)
				{
					string text3 = text.Substring(0, num2);
					text = text.Substring(num2);
					text2 = ((text2.Length > 0) ? (text2 + "; " + text3) : text3);
				}
				string text4 = string.Empty;
				try
				{
					text4 = ((obj4 == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])obj4));
				}
				catch (Exception ex3)
				{
					CWLogger.Logger.ErrorException(string.Format("subtitle({0}): {1}", num, ex3), ex3);
					byte[] array = (byte[])obj4;
					CWLogger.Logger.Error(string.Format("subtitle({0}): length={1}", num, (array != null) ? array.Length : 0));
					throw ex3;
				}
				ClockWorkSyncAppointment clockWorkSyncAppointment = new ClockWorkSyncAppointment
				{
					AppointmentId = num,
					StartDateTime = (DateTime)record["startdate"],
					EndDateTime = (DateTime)record["enddate"],
					IsCancelled = (bool)record["cancelled"],
					IsPrivate = (bool)record["ishidden"],
					Memo = text,
					Subtitle = (text4 ?? ""),
					AppointmentType = this.GetSyncAppTypeFromRecord(record),
					Location = text2
				};
				clockWorkSyncAppointment.Memo = clockWorkSyncAppointment.GetMemoPlainText();
				clockWorkSyncAppointment.IsAllDayEvent = (clockWorkSyncAppointment.StartDateTime.Hour == 0 && clockWorkSyncAppointment.StartDateTime.Minute == 1 && clockWorkSyncAppointment.EndDateTime.Hour == 23 && clockWorkSyncAppointment.EndDateTime.Minute == 59);
				clockWorkSyncAppointment.LastModifiedTime = ((obj5 != DBNull.Value) ? ((DateTime)obj5) : (clockWorkSyncAppointment.LastModifiedTime = (DateTime)record["dateadded"]));
				clockWorkSyncAppointment.Mapping = AppointmentSyncMappingDAO.GetMappingFromReader(record);
				result = clockWorkSyncAppointment;
			}
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0005E034 File Offset: 0x0005C234
		private ClockWorkSyncAppType GetSyncAppTypeFromRecord(IDataReader record)
		{
			return (record == null) ? null : new ClockWorkSyncAppType
			{
				AppTypeId = ((record["apptypeid"] is DBNull) ? 0 : ((int)record["apptypeid"])),
				Description = record["apptypedescription"].ToString()
			};
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0005E098 File Offset: 0x0005C298
		private ClockWorkSyncAttendee GetSyncAttendeeFromReader(IDataReader reader, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = batchDecryptor == null;
			if (flag)
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				SyncOperationContext opContext = this.OpContext;
				batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
			}
			return new ClockWorkSyncAttendee
			{
				AttendeeId = ((reader["attendeeid"] is DBNull) ? 0 : ((int)reader["attendeeid"])),
				MiscCode = ((reader["misccode"] is DBNull) ? 0 : ((int)reader["misccode"])),
				IsNoShow = (!(reader["noshow"] is DBNull) && (bool)reader["noshow"]),
				Attendee = this.GetSyncPersonFromRecord(reader, batchDecryptor)
			};
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0005E170 File Offset: 0x0005C370
		private ClockWorkSyncPersonBase GetSyncPersonFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			ClockWorkSyncPersonBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ClockWorkSyncPersonBase
				{
					PersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"])),
					FirstName = ((record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"])),
					LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
					Student_no = ((record["student_no"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["student_no"]))
				};
			}
			return result;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0005E268 File Offset: 0x0005C468
		public DateTime GetClockWorkAppointmentLastModifiedDateTime(int appointmentId)
		{
			ClockWorkSyncAppointment clockWorkSyncAppointment = this.LoadClockWorkAppointmentById(appointmentId);
			bool flag = clockWorkSyncAppointment == null;
			DateTime result;
			if (flag)
			{
				result = DateTime.Now;
			}
			else
			{
				result = clockWorkSyncAppointment.LastModifiedTime;
			}
			return result;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0005E298 File Offset: 0x0005C498
		public ClockWorkSyncAppointmentChangeResponse LoadAppointmentChanges(ClockWorkSyncAppointmentChangeRequest request)
		{
			List<ClockWorkSyncAppointmentChange> list = new List<ClockWorkSyncAppointmentChange>();
			DateTime clockWorkSyncState = this.ResetSyncState(request.ClockWorkPersonId);
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@syncstate", DbType.DateTime, request.ClockWorkSyncState.Value),
				databaseLayer.GetParameter("@pid", DbType.Int32, request.ClockWorkPersonId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("IF DATEDIFF(day,@syncstate,getdate()) < 45\r\nBEGIN\r\n\tSELECT x.howModifiedCode,x.appointmentID,x.dateModified \r\n\tINTO #t1\r\n\tFROM\r\n\t(\r\n\tSELECT am.howModifiedCode,am.appointmentID,am.dateModified\r\n\t\tFROM    appointmentsmodifieddates am\r\n\t\tWHERE    am.dateModified>=@syncstate AND personid>0\r\n\t\tUNION\r\n\t\tSELECT    0 AS howModifiedCode,a.appointmentID,a.dateadded\r\n\t\tFROM    AppointmentsFastLoad a\r\n\t\tWHERE    a.dateadded>=@syncstate\r\n\t) x\r\n\r\n\tSELECT\tdistinct y.howModifiedCode,y.appointmentID,y.dateModified,y.isHidden,y.isAllDayEvent\r\n\tFROM\r\n\t(\r\n\tSELECT\tCASE WHEN COALESCE(a.cancelled,CAST(1 AS BIT))=1 THEN 2 ELSE #t1.howModifiedCode END AS howModifiedCode,#t1.appointmentID,#t1.dateModified,a.ishidden,\r\n\t\t\tCASE WHEN DATEPART(hour,a.startdate)=0 AND DATEPART(minute,a.startdate)=1 AND DATEPART(hour,a.enddate)=23 AND DATEPART(minute,a.enddate)=59 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS isAllDayEvent\r\n\tFROM\t#t1 LEFT JOIN appointments a ON a.appointmentid=#t1.appointmentid\r\n\t\t\tLEFT JOIN attendees att ON att.AppointmentID=#t1.appointmentID \r\n\tWHERE\tatt.PersonID=@pid AND not (DATEDIFF(mi,CAST( FLOOR( CAST( startdate AS FLOAT ) ) AS DATETIME ),startdate)=0\r\n\t\t\tAND DATEDIFF(mi,CAST( FLOOR( CAST( enddate AS FLOAT ) ) AS DATETIME ),enddate)=60)\r\n\tUNION ALL\r\n\tSELECT\t2 AS howModifiedCode,#t1.appointmentID,#t1.dateModified ,a.ishidden,\r\n\t\t\tCASE WHEN DATEPART(hour,a.startdate)=0 AND DATEPART(minute,a.startdate)=1 AND DATEPART(hour,a.enddate)=23 AND DATEPART(minute,a.enddate)=59 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS isAllDayEvent\r\n\tFROM\t#t1 LEFT JOIN archive_appointments a ON a.AppointmentID=#t1.appointmentID\r\n\t\t\tLEFT JOIN archive_attendees att ON att.AppointmentID=#t1.appointmentID\r\n\tWHERE\tNOT #t1.appointmentID IN (SELECT appointmentid FROM appointments) --appointment was deleted\r\n\t\t\tAND att.PersonID=@pid AND not (DATEDIFF(mi,CAST( FLOOR( CAST( startdate AS FLOAT ) ) AS DATETIME ),startdate)=0\r\n\t\t\tAND DATEDIFF(mi,CAST( FLOOR( CAST( enddate AS FLOAT ) ) AS DATETIME ),enddate)=60)\r\n\t) y\r\n\tORDER BY y.appointmentID, y.dateModified DESC\r\n\r\n\tDROP TABLE #t1\r\nEND\r\nELSE\r\nBEGIN\r\n\tSELECT x.howModifiedCode,x.appointmentID,x.dateModified\r\n\tINTO #t2\r\n\tFROM\r\n\t(\r\n\tSELECT am.howModifiedCode,am.appointmentID,am.dateModified\r\n\t\tFROM    appointmentsmodifieddates am\r\n\t\tWHERE    am.dateModified>=@syncstate AND personid>0\r\n\t\tUNION\r\n\t\tSELECT    0 AS howModifiedCode,a.appointmentID,a.dateadded\r\n\t\tFROM    appointments a\r\n\t\tWHERE    a.dateadded>=@syncstate\r\n\t) x\r\n\r\n\tSELECT\tdistinct y.howModifiedCode,y.appointmentID,y.dateModified,y.isHidden,y.isAllDayEvent\r\n\tFROM\r\n\t(\r\n\tSELECT\tCASE WHEN COALESCE(a.cancelled,CAST(1 AS BIT))=1 THEN 2 ELSE #t2.howModifiedCode END AS howModifiedCode,#t2.appointmentID,#t2.dateModified,a.ishidden,\r\n\t\t\tCASE WHEN DATEPART(hour,a.startdate)=0 AND DATEPART(minute,a.startdate)=1 AND DATEPART(hour,a.enddate)=23 AND DATEPART(minute,a.enddate)=59 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS isAllDayEvent\r\n\tFROM\t#t2 LEFT JOIN appointments a ON a.appointmentid=#t2.appointmentid\r\n\t\t\tLEFT JOIN attendees att ON att.AppointmentID=#t2.appointmentID \r\n\tWHERE\tatt.PersonID=@pid AND not (DATEDIFF(mi,CAST( FLOOR( CAST( startdate AS FLOAT ) ) AS DATETIME ),startdate)=0\r\n\t\t\tAND DATEDIFF(mi,CAST( FLOOR( CAST( enddate AS FLOAT ) ) AS DATETIME ),enddate)=60)\r\n\tUNION ALL\r\n\tSELECT\t2 AS howModifiedCode,#t2.appointmentID,#t2.dateModified ,a.ishidden,\r\n\t\t\tCASE WHEN DATEPART(hour,a.startdate)=0 AND DATEPART(minute,a.startdate)=1 AND DATEPART(hour,a.enddate)=23 AND DATEPART(minute,a.enddate)=59 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS isAllDayEvent\r\n\tFROM\t#t2 LEFT JOIN archive_appointments a ON a.AppointmentID=#t2.appointmentID\r\n\t\t\tLEFT JOIN archive_attendees att ON att.AppointmentID=#t2.appointmentID\r\n\tWHERE\tNOT #t2.appointmentID IN (SELECT appointmentid FROM appointments) --appointment was deleted\r\n\t\t\tAND att.PersonID=@pid AND not (DATEDIFF(mi,CAST( FLOOR( CAST( startdate AS FLOAT ) ) AS DATETIME ),startdate)=0\r\n\t\t\tAND DATEDIFF(mi,CAST( FLOOR( CAST( enddate AS FLOAT ) ) AS DATETIME ),enddate)=60)\r\n\t) y\r\n\tORDER BY y.appointmentID, y.dateModified DESC\r\n\r\n\tDROP TABLE #t2\r\nEND", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IAppointmentSyncMappingDAO appointmentSyncMappingDAO = new AppointmentSyncMappingDAO(this.OpContext);
					int num = 0;
					while (dataReader.Read())
					{
						int num2 = (int)dataReader["appointmentID"];
						bool flag2 = num2 == num;
						if (!flag2)
						{
							ClockWorkExternalAppMapping mapping = appointmentSyncMappingDAO.LoadMappingByClockWorkAppointmentId(num2);
							DateTime lastModifiedDate = (DateTime)dataReader["dateModified"];
							eAppointmentSyncChangeType appointmentSyncChangeType = (eAppointmentSyncChangeType)Enum.Parse(typeof(eAppointmentSyncChangeType), dataReader["howModifiedCode"].ToString());
							list.Add(new ClockWorkSyncAppointmentChange
							{
								AppointmentSyncChangeType = appointmentSyncChangeType,
								ClockWorkAppointmentID = num2,
								Mapping = mapping,
								LastModifiedDate = lastModifiedDate,
								IsPrivate = Convert.ToBoolean(dataReader["isHidden"]),
								IsAllDayEvent = Convert.ToBoolean(dataReader["isAllDayEvent"])
							});
							num = num2;
						}
					}
				}
			}
			return new ClockWorkSyncAppointmentChangeResponse
			{
				ClockWorkSyncState = clockWorkSyncState,
				ClockWorkAppointmentChanges = list
			};
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0005E474 File Offset: 0x0005C674
		public DateTime ResetSyncState(int clockworkPersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			return (DateTime)databaseLayer.ExecuteScalar("SELECT GETDATE()");
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0005E4B0 File Offset: 0x0005C6B0
		public void CreateClockWorkSyncAppointment(ClockWorkSyncAppointment appointment)
		{
			bool isAllDayEvent = appointment.IsAllDayEvent;
			if (isAllDayEvent)
			{
				DateTime startDateTime = appointment.StartDateTime;
				appointment.StartDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 0, 1, 0);
				appointment.EndDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 23, 59, 0);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, appointment.StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, appointment.EndDateTime),
				this.DatabaseManager.GetParameter("@iscancelled", DbType.Boolean, appointment.IsCancelled),
				this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, appointment.IsPrivate),
				this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(appointment.Subtitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(appointment.Subtitle)),
				this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(appointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(appointment.Location)),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, appointment.AppointmentType.AppTypeId),
				this.DatabaseManager.GetParameter("@whobooked", DbType.Int32, this.OpContext.WhoAmI)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked\r\n                            ,overridecolour,extraattendeescount,appcode,groupcode,actualstarttime,actualendtime\r\n                            ,location,examid,caseid,totalbreakminutes,sittingid,subject)\r\nVALUES (@apptypeid,@startdate,@enddate,@iscancelled,getdate(),@whobooked,@ishidden,0\r\n        ,NULL,0,0,-1,NULL,NULL\r\n        ,@location,NULL,NULL,0,NULL,@subtitle);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS appointmentid;", parameters);
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				appointment.AppointmentId = (int)dataTable.Rows[0][0];
			}
			this.UpdateSyncAttendees(appointment.AppointmentId, null, appointment.Attendees);
			this.CreateMemo(appointment);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		public void UpdateClockWorkSyncAppointment(ClockWorkSyncAppointment appointment)
		{
			bool flag = !string.IsNullOrEmpty(appointment.Subtitle) && appointment.Subtitle.IndexOf(":") >= 0;
			if (flag)
			{
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT at.description FROM appointments a LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid WHERE a.appointmentid=@appointmentid", new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appointmentid", DbType.Int32, appointment.AppointmentId)
				}))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						string text = dataReader["description"].ToString().Trim();
						bool flag3 = text.Length > 0;
						if (flag3)
						{
							text += ": ";
							bool flag4 = appointment.Subtitle.StartsWith(text, StringComparison.OrdinalIgnoreCase);
							if (flag4)
							{
								appointment.Subtitle = appointment.Subtitle.Substring(text.Length);
							}
						}
					}
				}
			}
			bool isAllDayEvent = appointment.IsAllDayEvent;
			if (isAllDayEvent)
			{
				DateTime startDateTime = appointment.StartDateTime;
				appointment.StartDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 0, 1, 0);
				appointment.EndDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 23, 59, 0);
			}
			ClockWorkSyncAppointment clockWorkSyncAppointment = this.LoadClockWorkAppointmentById(appointment.AppointmentId);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, appointment.StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, appointment.EndDateTime),
				this.DatabaseManager.GetParameter("@iscancelled", DbType.Boolean, appointment.IsCancelled),
				this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, appointment.IsPrivate),
				this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(appointment.Subtitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(appointment.Subtitle)),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, appointment.AppointmentType.AppTypeId),
				this.DatabaseManager.GetParameter("@appointmentid", DbType.Int32, appointment.AppointmentId),
				this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(appointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(appointment.Location))
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET startdate=@startdate,enddate=@enddate,cancelled=@iscancelled,ishidden=@ishidden,\r\nsubject=@subtitle,location=@location --apptypeid=@apptypeid\r\nWHERE appointmentid=@appointmentid", parameters);
			this.UpdateSyncAttendees(appointment.AppointmentId, (clockWorkSyncAppointment == null) ? null : clockWorkSyncAppointment.Attendees, appointment.Attendees);
			this.InsertUpdateMemo(clockWorkSyncAppointment, appointment);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0005E9BC File Offset: 0x0005CBBC
		public ClockWorkSyncAppointment LoadClockWorkAppointmentById(int appointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteStoredProcedureReader("sp_AppSync_LoadAppointmentSync", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					ClockWorkSyncAppointment syncAppointmentFromRecord = this.GetSyncAppointmentFromRecord(dataReader, null);
					bool flag2 = syncAppointmentFromRecord == null;
					if (flag2)
					{
						return null;
					}
					syncAppointmentFromRecord.Attendees = this.LoadAttendees(syncAppointmentFromRecord.AppointmentId);
					return syncAppointmentFromRecord;
				}
			}
			return null;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0005EA68 File Offset: 0x0005CC68
		public List<ClockWorkSyncAppointment> LoadClockWorkAppointments(List<int> personIds, DateTime startDate, DateTime endDate, bool includeCancelled)
		{
			DbParameter[] array = new DbParameter[4];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, startDate);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, endDate);
			array[2] = this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, !includeCancelled);
			array[3] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", personIds.ConvertAll<string>((int pp) => pp.ToString()).ToArray()));
			DbParameter[] parameters = array;
			List<ClockWorkSyncAppointment> list = new List<ClockWorkSyncAppointment>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteStoredProcedureReader("sp_AppSync_LoadAppointmentsSync", CommandOverrideSettings.CommandOverrideSettingsTimeout120, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
					SyncOperationContext opContext = this.OpContext;
					IBatchDecryptor batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						ClockWorkSyncAppointment syncAppointmentFromRecord = this.GetSyncAppointmentFromRecord(dataReader, batchDecryptor);
						bool flag2 = syncAppointmentFromRecord != null;
						if (flag2)
						{
							list.Add(syncAppointmentFromRecord);
						}
					}
				}
			}
			List<ClockWorkSyncAppointment> list2 = list.Distinct(new ClockWorkSyncDAO.ClockWorkSyncAppointmentComparer()).ToList<ClockWorkSyncAppointment>();
			int[] array2 = (from g in list2
			select g.AppointmentId).Distinct<int>().ToArray<int>();
			bool flag3 = array2.Length != 0;
			if (flag3)
			{
				IDictionary<int, List<ClockWorkSyncAttendee>> dictionary = this.LoadAttendees(array2);
				using (IEnumerator<KeyValuePair<int, List<ClockWorkSyncAttendee>>> enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, List<ClockWorkSyncAttendee>> kvp = enumerator.Current;
						ClockWorkSyncAppointment clockWorkSyncAppointment = list2.FirstOrDefault((ClockWorkSyncAppointment g) => g.AppointmentId == kvp.Key);
						bool flag4 = clockWorkSyncAppointment != null;
						if (flag4)
						{
							clockWorkSyncAppointment.Attendees = kvp.Value;
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0005EC94 File Offset: 0x0005CE94
		private List<ClockWorkSyncAttendee> LoadAttendees(int appointmentId)
		{
			IDictionary<int, List<ClockWorkSyncAttendee>> dictionary = this.LoadAttendees(new int[]
			{
				appointmentId
			});
			return dictionary.ContainsKey(appointmentId) ? dictionary[appointmentId] : new List<ClockWorkSyncAttendee>();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0005ECD0 File Offset: 0x0005CED0
		private IDictionary<int, List<ClockWorkSyncAttendee>> LoadAttendees(int[] appointmentIds)
		{
			Dictionary<int, List<ClockWorkSyncAttendee>> result = new Dictionary<int, List<ClockWorkSyncAttendee>>();
			string[] appointmentIds2 = (from g in appointmentIds
			select g.ToString()).ToArray<string>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			IBatchDecryptor batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
			for (int i = 0; i < appointmentIds.Length; i += 100)
			{
				this.LoadAttendeesInOneCall(ref result, appointmentIds2, i, 100, batchDecryptor);
			}
			return result;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0005ED5C File Offset: 0x0005CF5C
		private void LoadAttendeesInOneCall(ref Dictionary<int, List<ClockWorkSyncAttendee>> appIdsWithAttendees, string[] appointmentIds, int startIndex, int count, IBatchDecryptor batchDecryptor)
		{
			int num = Math.Min(appointmentIds.Length - startIndex, count);
			bool flag = num < 1;
			if (!flag)
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				SyncOperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@appids", DbType.String, string.Join(",", appointmentIds, startIndex, num))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT orderid AS appointmentid INTO #t1 FROM splitorderids(@appids,',')\r\n\r\nSELECT\tatt.AppointmentID,att.AttendeeID,att.PersonID,att.miscCode,att.noShow,\r\n\t\tp.firstName,p.lastName,p.student_no\r\nFROM\tAttendees att LEFT JOIN people p ON p.PersonID=att.PersonID\r\nWHERE\tatt.AppointmentID IN (SELECT appointmentid FROM #t1)\r\n\r\nDROP TABLE #t1", parameters))
				{
					bool flag2 = dataReader == null;
					if (!flag2)
					{
						int num2 = 0;
						List<ClockWorkSyncAttendee> list = null;
						while (dataReader.Read())
						{
							int num3 = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
							bool flag3 = num3 < 1;
							if (!flag3)
							{
								ClockWorkSyncAttendee syncAttendeeFromReader = this.GetSyncAttendeeFromReader(dataReader, batchDecryptor);
								bool flag4 = num2 == num3;
								if (flag4)
								{
									list.Add(syncAttendeeFromReader);
								}
								else
								{
									bool flag5 = appIdsWithAttendees.ContainsKey(num3);
									if (flag5)
									{
										List<ClockWorkSyncAttendee> list2 = appIdsWithAttendees[num3];
										list2.Add(syncAttendeeFromReader);
										num2 = num3;
										list = list2;
									}
									else
									{
										List<ClockWorkSyncAttendee> list3 = new List<ClockWorkSyncAttendee>
										{
											syncAttendeeFromReader
										};
										appIdsWithAttendees.Add(num3, list3);
										num2 = num3;
										list = list3;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0005EED4 File Offset: 0x0005D0D4
		public void UpdateClockWorkSyncAppointmentReadOnlyStatus(int appointmentId, bool newReadOnlyStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@isreadonly", DbType.Boolean, newReadOnlyStatus)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET islocked=@isreadonly WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x020002AA RID: 682
		internal class ClockWorkSyncAppointmentComparer : IEqualityComparer<ClockWorkSyncAppointment>
		{
			// Token: 0x06000F6E RID: 3950 RVA: 0x0008E36C File Offset: 0x0008C56C
			public bool Equals(ClockWorkSyncAppointment x, ClockWorkSyncAppointment y)
			{
				return x.AppointmentId == y.AppointmentId;
			}

			// Token: 0x06000F6F RID: 3951 RVA: 0x0008E38C File Offset: 0x0008C58C
			public int GetHashCode(ClockWorkSyncAppointment obj)
			{
				return obj.AppointmentId.GetHashCode();
			}
		}
	}
}
