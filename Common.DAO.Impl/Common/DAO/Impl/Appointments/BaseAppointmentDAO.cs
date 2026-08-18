using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x0200012C RID: 300
	public class BaseAppointmentDAO : IBaseAppointmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00059628 File Offset: 0x00057828
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00059630 File Offset: 0x00057830
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00059639 File Offset: 0x00057839
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00059641 File Offset: 0x00057841
		public OperationContext OpContext { get; set; }

		// Token: 0x060008BD RID: 2237 RVA: 0x0005964A File Offset: 0x0005784A
		public BaseAppointmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0005967C File Offset: 0x0005787C
		public void DeleteMemo(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentmemos WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000596C0 File Offset: 0x000578C0
		public void DeleteCancelledReason(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentcancelledreason WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00059704 File Offset: 0x00057904
		public void DeleteIcons(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmenticons WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00059748 File Offset: 0x00057948
		public void DeleteAttendees(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM attendees WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0005978C File Offset: 0x0005798C
		public void DeleteAppointmentWorkshopInfo(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentworkshops WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000597D0 File Offset: 0x000579D0
		public void DeleteTestExamInfo(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentcourses WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00059814 File Offset: 0x00057A14
		public void DeleteAppData(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("BEGIN TRANSACTION\r\nINSERT INTO AppointmentNotesArchive (dataid,personid,appointmentid,controlid,controlvalueint)\r\n    SELECT dataid,personid,appointmentid,controlid,controlvalue FROM maininfopa WHERE appointmentid=@appid;\r\nINSERT INTO AppointmentNotesArchive (dataid,personid,appointmentid,controlid,controlvaluebinary)\r\n    SELECT dataid,personid,appointmentid,controlid,controlvalue FROM otherinfopa WHERE appointmentid=@appid;\r\nINSERT INTO AppointmentNotesArchive (dataid,personid,appointmentid,controlid,controlvaluedatetime)\r\n    SELECT dataid,personid,appointmentid,controlid,controlvalue FROM datetimeinfopa WHERE appointmentid=@appid;\r\nINSERT INTO AppointmentNotesArchive (dataid,personid,appointmentid,controlid,controlvaluebinary)\r\n    SELECT dataid,personid,appointmentid,controlid,controlvalue FROM imageinfopa WHERE appointmentid=@appid;\r\n\r\nDELETE FROM maininfopa WHERE appointmentid=@appid;\r\nDELETE FROM otherinfopa WHERE appointmentid=@appid;\r\nDELETE FROM datetimeinfopa WHERE appointmentid=@appid;\r\nDELETE FROM imageinfopa WHERE appointmentid=@appid;\r\n\r\nCOMMIT TRANSACTION", parameters);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00059858 File Offset: 0x00057A58
		public void DeleteMainAppointment(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointments WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0005989C File Offset: 0x00057A9C
		internal static void AddExtendedInfoToBaseExtendedAppointment(IDataReader reader, BaseExtendedAppointment app, OperationContext opContext, IBatchDecryptor batchDecryptor)
		{
			BaseAppointmentDAO.AddExtendedInfoToBaseBasicAppointment(reader, app, opContext, batchDecryptor);
			Attendee attendee;
			if (app.Attendees != null)
			{
				attendee = app.Attendees.FirstOrDefault((Attendee a) => a.Person.CoreGroup == eCoreGroup.Rooms);
			}
			else
			{
				attendee = null;
			}
			Attendee attendee2 = attendee;
			bool flag = attendee2 != null;
			if (flag)
			{
				app.Room = new AppointmentRoom
				{
					RoomId = attendee2.Person.PersonId,
					RoomTitle = attendee2.Person.FirstName,
					RoomDescription = attendee2.Person.LastName,
					RoomUniqueId = attendee2.Person.Student_no
				};
				app.Attendees.Remove(attendee2);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00059958 File Offset: 0x00057B58
		internal static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00059998 File Offset: 0x00057B98
		internal static T GetMainBaseExtendedAppointment<T>(IDataReader reader, OperationContext opContext, IBatchDecryptor batchDecryptor) where T : BaseExtendedAppointment
		{
			T mainBaseBasicAppointment = BaseAppointmentDAO.GetMainBaseBasicAppointment<T>(reader, opContext, batchDecryptor);
			mainBaseBasicAppointment.Memo = BaseAppointmentDAO.GetMemo(reader, opContext, batchDecryptor);
			mainBaseBasicAppointment.WhoBooked = PeopleDAO.GetPersonFromReader("Wb", reader, opContext, batchDecryptor);
			bool flag = mainBaseBasicAppointment.WhoBooked == null && BaseAppointmentDAO.ReaderContainsColumn(reader, "whoadded") && reader["whoadded"] != DBNull.Value;
			if (flag)
			{
				mainBaseBasicAppointment.WhoBooked = new PersonBase
				{
					PersonId = (int)reader["whoadded"]
				};
			}
			bool flag2 = BaseAppointmentDAO.ReaderContainsColumn(reader, "datebooked");
			if (flag2)
			{
				mainBaseBasicAppointment.DateBooked = (DateTime)reader["datebooked"];
			}
			else
			{
				bool flag3 = BaseAppointmentDAO.ReaderContainsColumn(reader, "dateadded");
				if (flag3)
				{
					mainBaseBasicAppointment.DateBooked = (DateTime)reader["dateadded"];
				}
			}
			mainBaseBasicAppointment.ExtraAttendeesCount = (int)reader["extraattendeescount"];
			mainBaseBasicAppointment.CancelInfo = BaseAppointmentDAO.GetAppCancelInfo(reader, opContext, batchDecryptor);
			mainBaseBasicAppointment.OverrideColour = ((reader["OverrideColour"] is DBNull) ? null : ((int?)reader["OverrideColour"]));
			mainBaseBasicAppointment.ActualStartDateTime = ((reader["ActualStartTime"] is DBNull) ? null : ((DateTime?)reader["ActualStartTime"]));
			mainBaseBasicAppointment.ActualEndDateTime = ((reader["ActualEndTime"] is DBNull) ? null : ((DateTime?)reader["ActualEndTime"]));
			return mainBaseBasicAppointment;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00059B78 File Offset: 0x00057D78
		internal static AppCancelInfo GetAppCancelInfo(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor)
		{
			object obj = record["cancelreasonid"];
			bool flag = obj != DBNull.Value;
			AppCancelInfo result;
			if (flag)
			{
				string text = record["cancelreasongroupname"].ToString();
				AppCancelInfo appCancelInfo = new AppCancelInfo();
				AppCancelReason appCancelReason = new AppCancelReason();
				appCancelReason.CancelReasonId = (int)obj;
				appCancelReason.CancelReasonTitle = record["cancelreasontitle"].ToString();
				object cancelReasonGroup;
				if (text.Length <= 0)
				{
					cancelReasonGroup = null;
				}
				else
				{
					(cancelReasonGroup = new AppCancelReasonGroup()).CancelReasonGroupName = text;
				}
				appCancelReason.CancelReasonGroup = cancelReasonGroup;
				appCancelReason.IsActive = true;
				appCancelReason.OrderNum = 0;
				appCancelReason.Colour = new int?(0);
				appCancelInfo.CancelReason = appCancelReason;
				appCancelInfo.CancelledBy = PeopleDAO.GetPersonFromReader("cb", record, opContext, batchDecryptor);
				appCancelInfo.CancelReasonText = record["cancelreasontext"].ToString();
				appCancelInfo.CancelledDate = ((record["cancelleddate"] == DBNull.Value) ? DateTime.Now : ((DateTime)record["cancelleddate"]));
				result = appCancelInfo;
			}
			else
			{
				bool flag2 = record["cancelreasontext"].ToString().Length > 0;
				if (flag2)
				{
					result = new AppCancelInfo
					{
						CancelReason = null,
						CancelledBy = PeopleDAO.GetPersonFromReader("cb", record, opContext, batchDecryptor),
						CancelReasonText = record["cancelreasontext"].ToString(),
						CancelledDate = ((record["cancelleddate"] == DBNull.Value) ? DateTime.Now : ((DateTime)record["cancelleddate"]))
					};
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00059D18 File Offset: 0x00057F18
		public static T GetMainBaseBasicAppointment<T>(IDataReader record, OperationContext opContext) where T : BaseBasicAppointment
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			object obj = BaseAppointmentDAO.ReaderContainsColumn(record, "subtitle") ? record["subtitle"] : record["subject"];
			t.AppointmentId = (int)record["appointmentid"];
			t.AppType = AppointmentTypeDAO.GetAppTypeFromReader(string.Empty, record);
			t.ShowTimeAs = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(record);
			t.StartDateTime = (DateTime)record["startdate"];
			t.EndDateTime = (DateTime)record["enddate"];
			t.GroupCode = ((record["groupcode"] is DBNull) ? 0 : ((int)record["groupcode"]));
			t.IsCancelled = (bool)record["cancelled"];
			t.IsLocked = (bool)record["islocked"];
			t.IsPrivate = (bool)record["ishidden"];
			t.Location = ((record["location"] is DBNull) ? string.Empty : databaseLayer.Encryption.Decrypt((byte[])record["location"]));
			t.SubTitle = ((obj is DBNull) ? string.Empty : databaseLayer.Encryption.Decrypt((byte[])obj));
			t.Attendees = new List<Attendee>();
			return t;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00059EF8 File Offset: 0x000580F8
		public static T GetMainBaseBasicAppointment<T>(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor) where T : BaseBasicAppointment
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			object obj = BaseAppointmentDAO.ReaderContainsColumn(record, "subtitle") ? record["subtitle"] : record["subject"];
			t.AppointmentId = (int)record["appointmentid"];
			t.AppType = AppointmentTypeDAO.GetAppTypeFromReader(string.Empty, record);
			t.ShowTimeAs = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(record);
			t.StartDateTime = (DateTime)record["startdate"];
			t.EndDateTime = (DateTime)record["enddate"];
			t.GroupCode = ((record["groupcode"] is DBNull) ? 0 : ((int)record["groupcode"]));
			t.IsCancelled = (bool)record["cancelled"];
			t.IsLocked = (bool)record["islocked"];
			t.IsPrivate = (bool)record["ishidden"];
			t.Location = ((record["location"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["location"]));
			t.SubTitle = ((obj is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])obj));
			t.Attendees = new List<Attendee>();
			return t;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0005A0BC File Offset: 0x000582BC
		internal static void AddExtendedInfoToBaseBasicAppointment(IDataReader record, BaseBasicAppointment appointment, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = appointment.Attendees == null;
			if (flag)
			{
				appointment.Attendees = new List<Attendee>();
			}
			int num = (record["attendeeid"] is DBNull) ? 0 : ((int)record["attendeeid"]);
			bool flag2 = num > 0;
			if (flag2)
			{
				Attendee attendee = AppointmentAttendeeDAO.GetAttendeeFromRecord(record, opContext, "", batchDecryptor);
				bool flag3 = attendee != null && appointment.Attendees.Find((Attendee f) => f.Person.PersonId == attendee.Person.PersonId) == null;
				if (flag3)
				{
					appointment.Attendees.Add(attendee);
				}
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0005A16C File Offset: 0x0005836C
		public static string GetMemo(IDataRecord record, OperationContext opContext)
		{
			bool flag = record["memotext"] != DBNull.Value;
			string result;
			if (flag)
			{
				object obj = record["isencrypted"];
				bool flag2 = obj != DBNull.Value && Convert.ToBoolean(obj);
				bool flag3 = flag2;
				if (flag3)
				{
					result = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption.Decrypt((byte[])record["memotext"]);
				}
				else
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					result = utf8Encoding.GetString((byte[])record["memotext"]);
				}
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0005A214 File Offset: 0x00058414
		public static string GetMemo(IDataRecord record, OperationContext opContext, IBatchDecryptor batchDecryptor)
		{
			return BaseAppointmentDAO.GetMemoString(record, batchDecryptor);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0005A230 File Offset: 0x00058430
		public static string GetMemo(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			return BaseAppointmentDAO.GetMemoString(record, batchDecryptor);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0005A24C File Offset: 0x0005844C
		private static string GetMemoString(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record["memotext"] == DBNull.Value;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				object obj = record["isencrypted"];
				bool flag2 = obj != DBNull.Value && Convert.ToBoolean(obj);
				bool flag3 = flag2;
				if (flag3)
				{
					result = batchDecryptor.Decrypt((byte[])record["memotext"]);
				}
				else
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					result = utf8Encoding.GetString((byte[])record["memotext"]);
				}
			}
			return result;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0005A2DC File Offset: 0x000584DC
		private void UpdateBaseBasicAppointment(BaseBasicAppointment basicAppointment, RecurringInstanceSetModifyBehaviour ModifyBehaivour, DbTransaction transaction = null)
		{
			bool flag = ModifyBehaivour == null;
			if (flag)
			{
				ModifyBehaivour = new RecurringInstanceSetModifyBehaviour();
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, basicAppointment.AppointmentId),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, (basicAppointment.AppType != null) ? basicAppointment.AppType.AppTypeId : -1),
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, (basicAppointment.ShowTimeAs != null) ? basicAppointment.ShowTimeAs.AppCode : 0),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, basicAppointment.StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, basicAppointment.EndDateTime),
				this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(basicAppointment.SubTitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(basicAppointment.SubTitle)),
				this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(basicAppointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(basicAppointment.Location)),
				this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, basicAppointment.IsCancelled),
				this.DatabaseManager.GetParameter("@islocked", DbType.Boolean, (ModifyBehaivour.LockedChangeBehaviour == eRecurringInstanceSetPropertyModifyBehaviour.Default || ModifyBehaivour.LockedChangeBehaviour == eRecurringInstanceSetPropertyModifyBehaviour.ApplyChangeToAllAppointmentsInSet) ? basicAppointment.IsLocked : DBNull.Value),
				this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, (ModifyBehaivour.PrivateChangeBehaviour == eRecurringInstanceSetPropertyModifyBehaviour.Default || ModifyBehaivour.PrivateChangeBehaviour == eRecurringInstanceSetPropertyModifyBehaviour.ApplyChangeToAllAppointmentsInSet) ? basicAppointment.IsPrivate : DBNull.Value),
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, basicAppointment.GroupCode)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET \r\n\tAppTypeID=@apptypeid,appCode=@appcode,startDate=@startdate,endDate=@enddate,\r\n\t[Subject]=@subtitle,cancelled=@cancelled,isLocked=COALESCE(@islocked,isLocked),isHidden=COALESCE(@ishidden,isHidden),\r\n\tLocation=@location,groupCode=@groupcode\r\nWHERE AppointmentID=@appid", parameters);
			IAppointmentAttendeeDAO appointmentAttendeeDAO = new AppointmentAttendeeDAO(this.OpContext);
			bool flag2 = ModifyBehaivour.AttendeesChangeBehaviour == eRecurringInstanceSetPropertyModifyBehaviour.Default || ModifyBehaivour.AttendeesChangeBehaviour == eRecurringInstanceSetPropertyModifyBehaviour.ApplyChangeToAllAppointmentsInSet;
			if (flag2)
			{
				appointmentAttendeeDAO.RemoveAttendeesNotInList(basicAppointment.AppointmentId, (from a in basicAppointment.Attendees
				select a.Person.PersonId).ToList<int>(), transaction);
				appointmentAttendeeDAO.InsertOrUpdateAppointmentAttendees(basicAppointment.AppointmentId, basicAppointment.Attendees, transaction);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0005A574 File Offset: 0x00058774
		public void UpdateBaseBasicAppointment(BaseBasicAppointment basicAppointment, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, basicAppointment.AppointmentId),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, (basicAppointment.AppType != null) ? basicAppointment.AppType.AppTypeId : -1),
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, (basicAppointment.ShowTimeAs != null) ? basicAppointment.ShowTimeAs.AppCode : 0),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, basicAppointment.StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, basicAppointment.EndDateTime),
				this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(basicAppointment.SubTitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(basicAppointment.SubTitle)),
				this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(basicAppointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(basicAppointment.Location)),
				this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, basicAppointment.IsCancelled),
				this.DatabaseManager.GetParameter("@islocked", DbType.Boolean, basicAppointment.IsLocked),
				this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, basicAppointment.IsPrivate),
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, basicAppointment.GroupCode)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET \r\n\tAppTypeID=@apptypeid,appCode=@appcode,startDate=@startdate,endDate=@enddate,\r\n\t[Subject]=@subtitle,cancelled=@cancelled,isLocked=COALESCE(@islocked,isLocked),isHidden=COALESCE(@ishidden,isHidden),\r\n\tLocation=@location,groupCode=@groupcode\r\nWHERE AppointmentID=@appid", parameters);
			IAppointmentAttendeeDAO appointmentAttendeeDAO = new AppointmentAttendeeDAO(this.OpContext);
			appointmentAttendeeDAO.RemoveAttendeesNotInList(basicAppointment.AppointmentId, (from a in basicAppointment.Attendees
			select a.Person.PersonId).ToList<int>(), transaction);
			appointmentAttendeeDAO.InsertOrUpdateAppointmentAttendees(basicAppointment.AppointmentId, basicAppointment.Attendees, transaction);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0005A7B2 File Offset: 0x000589B2
		public void UpdateBaseExtendedAppointment(BaseExtendedAppointment exAppointment, DbTransaction transaction = null)
		{
			this.UpdateBaseExtendedAppointment(exAppointment, null, transaction);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0005A7C0 File Offset: 0x000589C0
		public void UpdateBaseExtendedAppointment(BaseExtendedAppointment exAppointment, RecurringInstanceSetModifyBehaviour ModifyBehaivour, DbTransaction transaction = null)
		{
			this.UpdateBaseBasicAppointment(exAppointment, ModifyBehaivour, transaction);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, exAppointment.AppointmentId),
				this.DatabaseManager.GetParameter("@extraattendeescount", DbType.Int32, exAppointment.ExtraAttendeesCount),
				this.DatabaseManager.GetParameter("@actualstarttime", DbType.DateTime, (exAppointment.ActualStartDateTime != null) ? exAppointment.ActualStartDateTime.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@actualendtime", DbType.DateTime, (exAppointment.ActualEndDateTime != null) ? exAppointment.ActualEndDateTime.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@overridecolour", DbType.Int32, (exAppointment.OverrideColour != null) ? exAppointment.OverrideColour.Value : DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE\tappointments SET \r\n\textraAttendeesCount=@extraattendeescount,overrideColour=@overridecolour,\r\n\tActualStartTime=@actualstarttime,ActualEndTime=@actualendtime\r\nWHERE\tAppointmentID=@appid ", parameters);
			this.InsertOrUpdateAppointmentMemo(exAppointment.AppointmentId, exAppointment.Memo, null);
			bool flag = exAppointment.Room == null || exAppointment.Room.RoomId < 1;
			if (flag)
			{
				DbParameter[] array = new DbParameter[2];
				array[0] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, exAppointment.AppointmentId);
				int num = 1;
				DatabaseLayer databaseManager = this.DatabaseManager;
				string pName = "@pids";
				DbType pType = DbType.String;
				object value;
				if (exAppointment.Attendees != null)
				{
					value = string.Join(",", exAppointment.Attendees.ConvertAll<string>((Attendee g) => g.Person.PersonId.ToString()).ToArray());
				}
				else
				{
					value = "";
				}
				array[num] = databaseManager.GetParameter(pName, pType, value);
				parameters = array;
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM attendees WHERE AppointmentID=@appid AND PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3) AND NOT personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))", parameters);
			}
			else
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetOutputParameter("@attendeeid", DbType.Int32, 0),
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, exAppointment.AppointmentId),
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, exAppointment.Room.RoomId)
				};
				this.DatabaseManager.ExecuteNonQuery("IF @pid IS NULL OR @pid<1\r\n\tDELETE FROM attendees WHERE AppointmentID=@appid AND PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3)\r\nELSE \r\nBEGIN\r\n\tDECLARE @currattid int\r\n\tSET @currattid=(SELECT TOP 1 attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid)\r\n    IF @currattid IS NULL OR @currattid<1\r\n    BEGIN\r\n\t\tDELETE FROM attendees WHERE AppointmentID=@appid AND PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3);\r\n        INSERT INTO attendees (appointmentid,personid) VALUES (@appid,@pid)\r\n    END\r\nEND\r\nSET @attendeeid=(SELECT TOP 1 @attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid)", parameters);
			}
			IAppointmentCancelInfoDAO appointmentCancelInfoDAO = new AppointmentCancelInfoDAO(this.OpContext);
			appointmentCancelInfoDAO.InsertOrUpdateAppointmentCancelInfo(exAppointment.AppointmentId, exAppointment.CancelInfo, transaction);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0005AA54 File Offset: 0x00058C54
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@memotext", DbType.Binary, string.IsNullOrEmpty(MemoText) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(MemoText))
			};
			this.DatabaseManager.ExecuteNonQuery("IF @memotext IS NULL\r\n\tDELETE FROM AppointmentMemos WHERE AppointmentID=@appid \r\nELSE\r\nBEGIN \r\n\tIF EXISTS(SELECT appmemoid FROM AppointmentMemos WHERE AppointmentID=@appid)\r\n\t\tUPDATE AppointmentMemos SET memoText=@memotext WHERE AppointmentID=@appid \r\n\telse \r\n\t\tINSERT INTO AppointmentMemos(AppointmentID,memoText,isEncrypted) VALUES (@appid,@memotext,1)\r\nEND\r\nSELECT appmemoid FROM AppointmentMemos WHERE AppointmentID=@appid ", parameters);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0005AACC File Offset: 0x00058CCC
		public int CreateBaseBasicAppointment(BaseBasicAppointment basicAppointment, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appointmentid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, (basicAppointment.AppType != null) ? basicAppointment.AppType.AppTypeId : -1),
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, (basicAppointment.ShowTimeAs != null) ? basicAppointment.ShowTimeAs.AppCode : 0),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, basicAppointment.StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, basicAppointment.EndDateTime),
				this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(basicAppointment.SubTitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(basicAppointment.SubTitle)),
				this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(basicAppointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(basicAppointment.Location)),
				this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, basicAppointment.IsCancelled),
				this.DatabaseManager.GetParameter("@islocked", DbType.Boolean, basicAppointment.IsLocked),
				this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, basicAppointment.IsPrivate),
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, basicAppointment.GroupCode),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@extraattendeescount", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@overridecolour", DbType.Int32, DBNull.Value),
				this.DatabaseManager.GetParameter("@actualstartdatetime", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@actualenddatetime", DbType.DateTime, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointments\r\n    (AppTypeId,startDate,endDate,cancelled,personID,isHidden,isLocked,appCode,groupCode,[Subject],Location,dateAdded,extraattendeescount,overridecolour,actualstarttime,actualendtime)\r\nVALUES\r\n    (@apptypeid,@startdate,@enddate,@cancelled,@pid,@ishidden,@islocked,@appcode,@groupcode,@subtitle,@location,getdate(),@extraattendeescount,@overridecolour,@actualstartdatetime,@actualenddatetime)\r\nSET @appointmentid=SCOPE_IDENTITY()", array);
			basicAppointment.AppointmentId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
			IAppointmentAttendeeDAO appointmentAttendeeDAO = new AppointmentAttendeeDAO(this.OpContext);
			foreach (Attendee attendee in basicAppointment.Attendees)
			{
				appointmentAttendeeDAO.InsertOrUpdateAppointmentAttendee(basicAppointment.AppointmentId, attendee, transaction);
			}
			return basicAppointment.AppointmentId;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0005ADC4 File Offset: 0x00058FC4
		public int CreateBaseExtendedAppointmentEnsureUsersNotDoubleBooked(BaseExtendedAppointment extAppointment, int[] PidsToEnsureNotDoubleBooked, DbTransaction transaction = null)
		{
			int num = (this.OpContext.WhoAmI > 0) ? this.OpContext.WhoAmI : ((extAppointment.WhoBooked == null) ? 0 : extAppointment.WhoBooked.PersonId);
			DbParameter[] array = new DbParameter[17];
			array[0] = this.DatabaseManager.GetOutputParameter("@appointmentid", DbType.Int32, 0);
			array[1] = this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, (extAppointment.AppType != null) ? extAppointment.AppType.AppTypeId : -1);
			array[2] = this.DatabaseManager.GetParameter("@appcode", DbType.Int32, (extAppointment.ShowTimeAs != null) ? extAppointment.ShowTimeAs.AppCode : 0);
			array[3] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, extAppointment.StartDateTime);
			array[4] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, extAppointment.EndDateTime);
			array[5] = this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(extAppointment.SubTitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(extAppointment.SubTitle));
			array[6] = this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(extAppointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(extAppointment.Location));
			array[7] = this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, extAppointment.IsCancelled);
			array[8] = this.DatabaseManager.GetParameter("@islocked", DbType.Boolean, extAppointment.IsLocked);
			array[9] = this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, extAppointment.IsPrivate);
			array[10] = this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, extAppointment.GroupCode);
			array[11] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, num);
			array[12] = this.DatabaseManager.GetParameter("@extraattendeescount", DbType.Int32, extAppointment.ExtraAttendeesCount);
			array[13] = this.DatabaseManager.GetParameter("@overridecolour", DbType.Int32, (extAppointment.OverrideColour != null) ? extAppointment.OverrideColour.Value : DBNull.Value);
			array[14] = this.DatabaseManager.GetParameter("@actualstartdatetime", DbType.DateTime, (extAppointment.ActualStartDateTime != null) ? extAppointment.ActualStartDateTime.Value : DBNull.Value);
			array[15] = this.DatabaseManager.GetParameter("@actualenddatetime", DbType.DateTime, (extAppointment.ActualEndDateTime != null) ? extAppointment.ActualEndDateTime.Value : DBNull.Value);
			int num2 = 16;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@pids";
			DbType pType = DbType.String;
			object value;
			if (PidsToEnsureNotDoubleBooked != null)
			{
				value = string.Join(",", (from g in PidsToEnsureNotDoubleBooked
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num2] = databaseManager.GetParameter(pName, pType, value);
			DbParameter[] array2 = array;
			this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT a.appointmentid,att.personid FROM appointments a LEFT JOIN attendees att ON att.appointmentid=a.appointmentid WHERE a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ) AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND NOT att.personid IS NULL)\r\nBEGIN\r\n    INSERT INTO appointments\r\n        (AppTypeId,startDate,endDate,cancelled,personID,isHidden,isLocked,appCode,groupCode,[Subject],Location,dateAdded,extraattendeescount,overridecolour,actualstarttime,actualendtime)\r\n    VALUES\r\n        (@apptypeid,@startdate,@enddate,@cancelled,@pid,@ishidden,@islocked,@appcode,@groupcode,@subtitle,@location,getdate(),@extraattendeescount,@overridecolour,@actualstartdatetime,@actualenddatetime)\r\n    SET @appointmentid=SCOPE_IDENTITY()\r\nEND\r\nELSE\r\nBEGIN\r\n\tSET @appointmentid=0\r\nEND", array2);
			extAppointment.AppointmentId = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
			this.CreateBaseExtendedAppointmentExtendedParts(extAppointment, transaction);
			return extAppointment.AppointmentId;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0005B158 File Offset: 0x00059358
		public int CreateBaseExtendedAppointment(BaseExtendedAppointment extAppointment, DbTransaction transaction = null)
		{
			int num = (this.OpContext.WhoAmI > 0) ? this.OpContext.WhoAmI : ((extAppointment.WhoBooked == null) ? 0 : extAppointment.WhoBooked.PersonId);
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appointmentid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, (extAppointment.AppType != null) ? extAppointment.AppType.AppTypeId : -1),
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, (extAppointment.ShowTimeAs != null) ? extAppointment.ShowTimeAs.AppCode : 0),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, extAppointment.StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, extAppointment.EndDateTime),
				this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, string.IsNullOrEmpty(extAppointment.SubTitle) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(extAppointment.SubTitle)),
				this.DatabaseManager.GetParameter("@location", DbType.Binary, string.IsNullOrEmpty(extAppointment.Location) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(extAppointment.Location)),
				this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, extAppointment.IsCancelled),
				this.DatabaseManager.GetParameter("@islocked", DbType.Boolean, extAppointment.IsLocked),
				this.DatabaseManager.GetParameter("@ishidden", DbType.Boolean, extAppointment.IsPrivate),
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, extAppointment.GroupCode),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, num),
				this.DatabaseManager.GetParameter("@extraattendeescount", DbType.Int32, extAppointment.ExtraAttendeesCount),
				this.DatabaseManager.GetParameter("@overridecolour", DbType.Int32, (extAppointment.OverrideColour != null) ? extAppointment.OverrideColour.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@actualstartdatetime", DbType.DateTime, (extAppointment.ActualStartDateTime != null) ? extAppointment.ActualStartDateTime.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@actualenddatetime", DbType.DateTime, (extAppointment.ActualEndDateTime != null) ? extAppointment.ActualEndDateTime.Value : DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointments\r\n    (AppTypeId,startDate,endDate,cancelled,personID,isHidden,isLocked,appCode,groupCode,[Subject],Location,dateAdded,extraattendeescount,overridecolour,actualstarttime,actualendtime)\r\nVALUES\r\n    (@apptypeid,@startdate,@enddate,@cancelled,@pid,@ishidden,@islocked,@appcode,@groupcode,@subtitle,@location,getdate(),@extraattendeescount,@overridecolour,@actualstartdatetime,@actualenddatetime)\r\nSET @appointmentid=SCOPE_IDENTITY()", array);
			extAppointment.AppointmentId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
			bool flag = extAppointment.AppointmentId < 1;
			if (flag)
			{
				throw new DatabaseInsertFailedException("Could not create appointment - possible double booking prevention");
			}
			this.CreateBaseExtendedAppointmentExtendedParts(extAppointment, transaction);
			return extAppointment.AppointmentId;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0005B4B4 File Offset: 0x000596B4
		private void CreateBaseExtendedAppointmentExtendedParts(BaseExtendedAppointment extAppointment, DbTransaction transaction)
		{
			bool flag = extAppointment.Memo != null && extAppointment.Memo.Trim().Length > 0;
			if (flag)
			{
				this.InsertOrUpdateAppointmentMemo(extAppointment.AppointmentId, extAppointment.Memo, null);
			}
			int num = (extAppointment.CancelInfo == null || extAppointment.CancelInfo.CancelReason == null || extAppointment.CancelInfo.CancelReason.CancelReasonId < 1) ? 0 : extAppointment.CancelInfo.CancelReason.CancelReasonId;
			string text = (extAppointment.CancelInfo == null) ? "" : (extAppointment.CancelInfo.CancelReasonText ?? "");
			bool flag2 = num > 0 || text.Length > 0;
			if (flag2)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, extAppointment.AppointmentId),
					this.DatabaseManager.GetParameter("@cancelreasonid", DbType.Int32, (num > 0) ? num : DBNull.Value),
					this.DatabaseManager.GetParameter("@cancelreasontext", DbType.String, text),
					this.DatabaseManager.GetParameter("@cancelledbypersonid", DbType.Int32, this.OpContext.WhoAmI),
					this.DatabaseManager.GetParameter("@isempty", DbType.Boolean, false)
				};
				this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT appointmentid FROM appointmentcancelledreason WHERE appointmentid=@appid)\r\nBEGIN\r\n    IF @isempty=1\r\n        DELETE FROM appointmentcancelledreason WHERE appointmentid=@appid\r\n    ELSE\r\n        UPDATE appointmentcancelledreason SET cancelreasonid=@cancelreasonid,cancelreasontext=@cancelreasontext\r\n        WHERE appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    IF @isempty=0\r\n        INSERT INTO appointmentcancelledreason (appointmentid,cancelreasonid,cancelreasontext,cancelledbypersonid,cancelleddate)\r\n        VALUES (@appid,@cancelreasonid,@cancelreasontext,@cancelledbypersonid,getdate())\r\nEND", parameters);
			}
			IAppointmentAttendeeDAO appointmentAttendeeDAO = new AppointmentAttendeeDAO(this.OpContext);
			foreach (Attendee attendee in extAppointment.Attendees)
			{
				appointmentAttendeeDAO.InsertOrUpdateAppointmentAttendee(extAppointment.AppointmentId, attendee, transaction);
			}
			bool flag3 = extAppointment.Room != null && extAppointment.Room.RoomId > 0;
			if (flag3)
			{
				this.InsertOrUpdateAppointmentRoom(extAppointment.AppointmentId, extAppointment.Room.RoomId, transaction);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0005B6C0 File Offset: 0x000598C0
		public IList<BaseBasicAppointment> LoadBaseBasicAppointmentsByPersonAndDateRange(int PersonId, bool hideCancelled, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0)),
				this.DatabaseManager.GetParameter("@pids", DbType.String, PersonId.ToString()),
				this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, hideCancelled)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE   a.startdate BETWEEN @startdate AND @enddate\r\nAND a.appointmentid IN (SELECT appointmentid FROM attendees WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')))\r\nAND (@hidecancelled=0 OR a.cancelled=0)\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<BaseBasicAppointment> list = new List<BaseBasicAppointment>();
					BaseBasicAppointment baseBasicAppointment = null;
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = baseBasicAppointment == null || baseBasicAppointment.AppointmentId != num;
						if (flag2)
						{
							baseBasicAppointment = BaseAppointmentDAO.GetMainBaseBasicAppointment<BaseBasicAppointment>(dataReader, this.OpContext);
							list.Add(baseBasicAppointment);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseBasicAppointment(dataReader, baseBasicAppointment, this.OpContext, null);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0005B83C File Offset: 0x00059A3C
		public void UpdateAppointmentExternalId(int appId, int externalId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, appId),
				databaseLayer.GetParameter("@externalid", DbType.Int32, externalId)
			};
			databaseLayer.ExecuteNonQuery("update Appointments\r\nset\r\n\tExternalId = @externalid\r\nwhere AppointmentID = @appid", parameters);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0005B8A4 File Offset: 0x00059AA4
		public int LoadAppointmentExternalId(int appId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, appId)
			};
			object obj = databaseLayer.ExecuteScalar("select ExternalId from appointments where AppointmentId=@appid", parameters);
			return (obj != null && !Convert.IsDBNull(obj)) ? ((int)obj) : 0;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0005B910 File Offset: 0x00059B10
		public BaseBasicAppointment LoadBaseBasicAppointmentById(int appointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE a.appointmentid=@appid\r\nORDER BY a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					BaseBasicAppointment baseBasicAppointment = null;
					if (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = baseBasicAppointment == null || baseBasicAppointment.AppointmentId != num;
						if (flag2)
						{
							baseBasicAppointment = BaseAppointmentDAO.GetMainBaseBasicAppointment<BaseBasicAppointment>(dataReader, this.OpContext);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseBasicAppointment(dataReader, baseBasicAppointment, this.OpContext, null);
						return baseBasicAppointment;
					}
				}
			}
			return null;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0005B9E0 File Offset: 0x00059BE0
		public T LoadBaseExtendedAppointmentById<T>(int appointmentId) where T : BaseExtendedAppointment
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE a.appointmentid=@appid\r\nORDER BY a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					T t = default(T);
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = t == null || t.AppointmentId != num;
						if (flag2)
						{
							t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
					}
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0005BAE8 File Offset: 0x00059CE8
		public int InsertOrUpdateAppointmentRoom(int appId, int roomId, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@attendeeid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, roomId)
			};
			this.DatabaseManager.ExecuteNonQuery("IF @pid IS NULL OR @pid<1\r\n\tDELETE FROM attendees WHERE AppointmentID=@appid AND PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3)\r\nELSE \r\nBEGIN\r\n\tDECLARE @currattid int\r\n\tSET @currattid=(SELECT TOP 1 attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid)\r\n    IF @currattid IS NULL OR @currattid<1\r\n    BEGIN\r\n\t\tDELETE FROM attendees WHERE AppointmentID=@appid AND PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3);\r\n        INSERT INTO attendees (appointmentid,personid) VALUES (@appid,@pid)\r\n    END\r\nEND\r\nSET @attendeeid=(SELECT TOP 1 @attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid)", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0005BB80 File Offset: 0x00059D80
		public void DeleteAppointmentRoom(int appId, DbTransaction transaction = null)
		{
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM attendees WHERE AppointmentID=@appid AND PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3)", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId)
			});
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0005BBC0 File Offset: 0x00059DC0
		public IList<T> LoadBaseExtendedAppointmentsByDateRange<T>(DateTime StartDateTime, DateTime EndDateTime, bool ShowCancelled = false) where T : BaseExtendedAppointment
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDateTime),
				this.DatabaseManager.GetParameter("@showcancelled", DbType.Boolean, ShowCancelled)
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE a.startdate BETWEEN @startdate AND @enddate AND (@showcancelled=1 OR a.cancelled=0)\r\nORDER BY a.appointmentid,a.AttendeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<T> list = new List<T>();
					T t = default(T);
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = t == null || t.AppointmentId != num;
						if (flag2)
						{
							t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
							list.Add(t);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0005BD04 File Offset: 0x00059F04
		public IList<T> LoadBaseExtendedAppointmentsByDateRangeAndAppType<T>(DateTime StartDateTime, DateTime EndDateTime, IList<int> AppTypeIds) where T : BaseExtendedAppointment
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDateTime);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDateTime);
			int num = 2;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeids";
			DbType pType = DbType.String;
			object value;
			if (AppTypeIds != null)
			{
				value = string.Join(",", AppTypeIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE   a.startdate BETWEEN @startdate AND @enddate\r\n        AND a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,','))\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<T> list = new List<T>();
					T t = default(T);
					while (dataReader.Read())
					{
						int num2 = (int)dataReader["appointmentid"];
						bool flag2 = t == null || t.AppointmentId != num2;
						if (flag2)
						{
							t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
							list.Add(t);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0005BE88 File Offset: 0x0005A088
		public IList<T> LoadBaseExtendedAppointmentsByPersonId<T>(int PersonId) where T : BaseExtendedAppointment
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE   a.appointmentid IN (SELECT appointmentid FROM attendees WHERE personid=@pid)\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<T> list = new List<T>();
					T t = default(T);
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = t == null || t.AppointmentId != num;
						if (flag2)
						{
							t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
							list.Add(t);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0005BF9C File Offset: 0x0005A19C
		public IList<T> LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<T>(DateTime StartDate, DateTime EndDate, IList<int> PersonIds) where T : BaseExtendedAppointment
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0));
			int num = 2;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@pids";
			DbType pType = DbType.String;
			object value;
			if (PersonIds != null)
			{
				value = string.Join(",", PersonIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			try
			{
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE   a.startdate BETWEEN @startdate AND @enddate\r\n        AND a.appointmentid IN (SELECT appointmentid FROM attendees WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')))\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
				{
					bool flag = dataReader != null;
					if (flag)
					{
						List<T> list = new List<T>();
						T t = default(T);
						while (dataReader.Read())
						{
							try
							{
								int num2 = (int)dataReader["appointmentid"];
								bool flag2 = t == null || t.AppointmentId != num2;
								if (flag2)
								{
									t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
									list.Add(t);
								}
								BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
							}
							catch (Exception ex)
							{
								CWLogger.Logger.Error("BaseAppointmentDAO:LoadBaseExtendedAppointmentsByDateRangeAndPersonIds:Error0={0}", ex.ToString());
							}
						}
						return list;
					}
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("BaseAppointmentDAO:LoadBaseExtendedAppointmentsByDateRangeAndPersonIds:Error0={0}", ex2.ToString());
			}
			return null;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0005C1A8 File Offset: 0x0005A3A8
		public void DeleteAppointment(int AppointmentId, DbTransaction transaction = null)
		{
			this.DeleteMemo(AppointmentId, transaction);
			this.DeleteCancelledReason(AppointmentId, transaction);
			this.DeleteIcons(AppointmentId, transaction);
			this.DeleteAttendees(AppointmentId, transaction);
			this.DeleteAppointmentWorkshopInfo(AppointmentId, transaction);
			this.DeleteTestExamInfo(AppointmentId, transaction);
			this.DeleteAppData(AppointmentId, transaction);
			this.DeleteMainAppointment(AppointmentId, transaction);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0005C200 File Offset: 0x0005A400
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				bool flag = this._appLogDao == null;
				if (flag)
				{
					this._appLogDao = new AppointmentLogDAO(this.OpContext);
				}
				return this._appLogDao;
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0005C238 File Offset: 0x0005A438
		public void UpdateDateAndTime(int appId, DateTime startDateTime, DateTime endDateTime, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, startDateTime),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, endDateTime)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET startdate=@startdate,enddate=@enddate WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0005C2B0 File Offset: 0x0005A4B0
		public void UpdateAppointmentCancelledValue(int appId, bool cancelledValue, AppCancelInfo cancelInfo, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId),
				this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, cancelledValue)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET cancelled=@cancelled WHERE appointmentid=@appid", parameters);
			AppointmentCancelInfoDAO appointmentCancelInfoDAO = new AppointmentCancelInfoDAO(this.OpContext);
			appointmentCancelInfoDAO.InsertOrUpdateAppointmentCancelInfo(appId, cancelInfo, transaction);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0005C324 File Offset: 0x0005A524
		public void UpdateAppointmentAppCodeValue(int appId, int appCodeValue, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId),
				this.DatabaseManager.GetParameter("@appcode", DbType.Int32, appCodeValue)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET appcode=@appcode WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0005C384 File Offset: 0x0005A584
		public int FindMatchingExistingAppointment(BaseExtendedAppointment Appointment)
		{
			DbParameter[] array = new DbParameter[4];
			array[0] = this.DatabaseManager.GetOutputParameter("@appointmentid", DbType.Int32, 0);
			array[1] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", Appointment.Attendees.ToList<Attendee>().ConvertAll<string>((Attendee f) => f.Person.PersonId.ToString()).ToArray()));
			array[2] = this.DatabaseManager.GetParameter("@startdatetime", DbType.DateTime, new DateTime(Appointment.StartDateTime.Year, Appointment.StartDateTime.Month, Appointment.StartDateTime.Day, Appointment.StartDateTime.Hour, Appointment.StartDateTime.Minute, 0));
			array[3] = this.DatabaseManager.GetParameter("@enddatetime", DbType.DateTime, new DateTime(Appointment.EndDateTime.Year, Appointment.EndDateTime.Month, Appointment.EndDateTime.Day, Appointment.EndDateTime.Hour, Appointment.EndDateTime.Minute, 0));
			DbParameter[] array2 = array;
			this.DatabaseManager.ExecuteNonQuery("DECLARE @numpids int\r\nSET @numpids=(SELECT COUNT(DISTINCT(orderid)) FROM SplitOrderIDs(@pids,','))\r\n\r\nSET @appointmentid=(SELECT TOP 1 appointmentid FROM\r\n(\r\nSELECT a.appointmentid,COUNT(DISTINCT(personid)) AS ct\r\nFROM apps a \r\nWHERE a.startdate=@startdatetime AND a.enddate=@enddatetime AND a.PersonID IN (SELECT orderid AS personid FROM SplitOrderIDs(@pids,','))\r\nGROUP BY a.AppointmentID \r\nHAVING COUNT(DISTINCT(personid))=@numpids\r\n) x\r\n)", array2);
			return (array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<T> LoadBaseExtendedAppointmentsByDateRangeAndPersonIdsAndAppTypes<T>(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, IList<int> AppTypeIds, bool HideCancelled) where T : BaseExtendedAppointment
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0005C504 File Offset: 0x0005A704
		public IList<T> LoadBaseExtendedAppointmentsByGroupCode<T>(int GroupCode) where T : BaseExtendedAppointment
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@groupcode", DbType.Int32, GroupCode)
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE a.groupcode=@groupcode\r\nORDER BY a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<T> list = new List<T>();
					T t = default(T);
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = t == null || t.AppointmentId != num;
						if (flag2)
						{
							t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
							list.Add(t);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0005C618 File Offset: 0x0005A818
		public IList<PersonBase> LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(int StudentPersonId, IList<int> StaffGroupIds)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPersonId);
			array[1] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", StaffGroupIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IList<PersonBase> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT a.appointmentid,att.personid,p.firstname,p.middlename,p.lastname,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM    apps a LEFT JOIN attendees att ON att.appointmentid=a.appointmentid AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))\r\n        LEFT JOIN people p ON p.personid=att.personid\r\n        LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE   a.personid=@pid AND a.cancelled=0 AND a.noshow=0\r\nGROUP BY a.appointmentid,att.personid,p.firstname,p.middlename,p.lastname,p.student_no", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PersonBase> list = new List<PersonBase>();
					while (dataReader.Read())
					{
						PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
						bool flag2 = personFromReader != null;
						if (flag2)
						{
							list.Add(personFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0005C718 File Offset: 0x0005A918
		public IList<T> LoadBaseExtendedAppointmentsByAppointmentIds<T>(IList<int> AppointmentIds, IList<int> allowedAppTypeIds) where T : BaseExtendedAppointment
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@appids", DbType.String, string.Join(",", AppointmentIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			array[1] = this.DatabaseManager.GetParameter("@apptypeids", DbType.String, string.Join(",", allowedAppTypeIds.ToList<int>().ConvertAll<string>((int h) => h.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\tAttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tpg.groupid,ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM\tapps a LEFT JOIN peoplegroups pg ON pg.personid=a.personid AND pg.groupid<10 \r\n\t\tLEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\nWHERE a.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appids,','))\r\n        AND a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,','))\r\nORDER BY a.appointmentid,a.AttendeeID", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<T> list = new List<T>();
					T t = default(T);
					while (dataReader.Read())
					{
						int num = (int)dataReader["appointmentid"];
						bool flag2 = t == null || t.AppointmentId != num;
						if (flag2)
						{
							t = BaseAppointmentDAO.GetMainBaseExtendedAppointment<T>(dataReader, this.OpContext, batchDecryptor);
							list.Add(t);
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(dataReader, t, this.OpContext, batchDecryptor);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x040004F4 RID: 1268
		private IAppointmentLogDAO _appLogDao;
	}
}
