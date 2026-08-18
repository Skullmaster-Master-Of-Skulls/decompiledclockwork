using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsPointOfContact;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Impl.AppointmentsPointOfContact
{
	// Token: 0x02000122 RID: 290
	public class PointOfContactDAO : IPointOfContactDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0005436C File Offset: 0x0005256C
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00054374 File Offset: 0x00052574
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600083B RID: 2107 RVA: 0x0005437D File Offset: 0x0005257D
		public PointOfContactDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000543AE File Offset: 0x000525AE
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x000543B6 File Offset: 0x000525B6
		public OperationContext OpContext { get; set; }

		// Token: 0x0600083E RID: 2110 RVA: 0x000543C0 File Offset: 0x000525C0
		private PointOfContact GetPointOfContactFromReader(IDataReader reader)
		{
			bool flag = reader != null;
			if (flag)
			{
				bool flag2 = reader["appointmentid"] != DBNull.Value;
				if (flag2)
				{
					int appointmentId = (int)reader["appointmentid"];
					bool flag3 = reader["memotext"] == DBNull.Value;
					string text;
					if (flag3)
					{
						text = "";
					}
					else
					{
						object obj = reader["isencrypted"];
						bool flag4 = obj != DBNull.Value && Convert.ToBoolean(obj);
						bool flag5 = flag4;
						if (flag5)
						{
							text = this.DatabaseManager.Encryption.Decrypt((byte[])reader["memotext"]);
						}
						else
						{
							UTF8Encoding utf8Encoding = new UTF8Encoding();
							text = utf8Encoding.GetString((byte[])reader["memotext"]);
						}
						int num = text.IndexOf("{\\rtf1\\", StringComparison.OrdinalIgnoreCase);
						bool flag6 = num > 0;
						if (flag6)
						{
							string text2 = text.Substring(0, num);
							text = text.Substring(num);
						}
					}
					return new PointOfContact
					{
						AppointmentId = appointmentId,
						Student = AppointmentAttendeeDAO.GetAttendeeFromRecord(reader, this.OpContext, "student", null),
						Staff = AppointmentAttendeeDAO.GetAttendeeFromRecord(reader, this.OpContext, "staff", null),
						WhoBooked = PeopleDAO.GetPersonFromReader("whoentered", reader, this.OpContext, null),
						DateBooked = (DateTime)reader["dateadded"],
						ShowTimeAs = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(reader),
						AppType = AppointmentTypeDAO.GetAppTypeFromReader("", reader),
						Memo = text,
						SubTitle = ((reader["subtitle"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])reader["subtitle"])),
						SessionNotesData = this.GetSessionNotesDataFromReader(appointmentId, reader)
					};
				}
			}
			return null;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000545C8 File Offset: 0x000527C8
		private List<DynamicData> GetSessionNotesDataFromReader(int AppointmentId, IDataReader reader)
		{
			IList<DynamicData> list = new List<DynamicData>();
			bool flag = reader == null;
			List<DynamicData> result;
			if (flag)
			{
				result = list.ToList<DynamicData>();
			}
			else
			{
				DynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
				bool flag6;
				do
				{
					bool flag2 = reader["appointmentid"] != DBNull.Value;
					if (flag2)
					{
						int num = (int)reader["appointmentid"];
						bool flag3 = num != AppointmentId;
						if (flag3)
						{
							break;
						}
						bool flag4 = reader["dataid"] != DBNull.Value;
						if (flag4)
						{
							DynamicData dataFromRecords = dynamicDataDAO.GetDataFromRecords(reader);
							bool flag5 = dataFromRecords != null;
							if (flag5)
							{
								list.Add(dataFromRecords);
							}
						}
					}
					flag6 = !reader.Read();
				}
				while (!flag6);
				dynamicDataDAO.MergeDynamicDataIntoUniqueControlIds<DynamicData>(list);
				result = list.ToList<DynamicData>();
			}
			return result;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000546A8 File Offset: 0x000528A8
		public int CreatePointOfContact(PointOfContact PointOfContact, int screenNumToSaveNotesTo = 0, int rtfTextBoxCidToSaveNotesTo = 0)
		{
			Attendee student = PointOfContact.Student;
			bool flag = ((student != null) ? student.Person : null) == null || PointOfContact.Student.Person.PersonId < 1;
			if (flag)
			{
				CWLogger.Logger.Error("Failed to create point of contact - missing student.");
				throw new Exception("Failed to create point of contact - missing student.");
			}
			DbParameter[] array = new DbParameter[7];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, PointOfContact.StartDateTime);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, PointOfContact.EndDateTime);
			array[2] = this.DatabaseManager.GetParameter("@whoenteredpid", DbType.Int32, this.OpContext.WhoAmI);
			int num = 3;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeid";
			DbType pType = DbType.Int32;
			AppType appType = PointOfContact.AppType;
			array[num] = databaseManager.GetParameter(pName, pType, (appType != null) ? appType.AppTypeId : -1);
			int num2 = 4;
			DatabaseLayer databaseManager2 = this.DatabaseManager;
			string pName2 = "@appcode";
			DbType pType2 = DbType.Int32;
			AppShowTimeAsType showTimeAs = PointOfContact.ShowTimeAs;
			array[num2] = databaseManager2.GetParameter(pName2, pType2, (showTimeAs != null) ? showTimeAs.AppCode : 0);
			array[5] = this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(PointOfContact.SubTitle ?? ""));
			array[6] = this.DatabaseManager.GetParameter("@overrideColour", DbType.Int32, (PointOfContact.PocContext == ePointOfContactContext.Normal) ? DBNull.Value : ((int)PointOfContact.PocContext));
			DbParameter[] parameters = array;
			int num3 = (int)this.DatabaseManager.ExecuteScalar("INSERT INTO appointments (startdate,enddate,personid,apptypeid,appcode,subject,dateadded,overrideColour) \r\nVALUES (@startdate,@enddate,@whoenteredpid,@apptypeid,@appcode,@subtitle,getdate(),@overrideColour)\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS appointmentid", parameters);
			bool flag2 = num3 < 1;
			if (flag2)
			{
				CWLogger.Logger.Error("Failed to create point of contact.");
				throw new Exception("Failed to create point of contact.");
			}
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, num3),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PointOfContact.Student.Person.PersonId),
				this.DatabaseManager.GetParameter("@noshow", DbType.Boolean, false),
				this.DatabaseManager.GetParameter("@misccode", DbType.Int32, 0)
			};
			int num4 = (int)this.DatabaseManager.ExecuteScalar("DECLARE @rm bit\r\nIF EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=3)\r\n\tSET @rm = 1\r\nELSE\r\n\tSET @rm = 0\r\nIF NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\nBEGIN\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,@noshow,@misccode);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS attendeeid\r\nEND\r\nELSE\r\nBEGIN\r\n    UPDATE attendees SET noshow=@noshow,misccode=@misccode WHERE appointmentid=@appid AND personid=@pid;\r\n    SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid;\r\nEND", parameters);
			bool flag3 = num4 < 1;
			if (flag3)
			{
				string message = "Failed to create point of contact student attendee entry in database [appid=" + num3.ToString() + "].";
				CWLogger.Logger.Error(message);
				throw new Exception(message);
			}
			PointOfContact.Student.AttendeeId = num4;
			Attendee staff = PointOfContact.Staff;
			bool flag4 = ((staff != null) ? staff.Person : null) != null && PointOfContact.Staff.Person.PersonId > 0;
			if (flag4)
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, num3),
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, PointOfContact.Staff.Person.PersonId),
					this.DatabaseManager.GetParameter("@noshow", DbType.Boolean, false),
					this.DatabaseManager.GetParameter("@misccode", DbType.Int32, 0)
				};
				int num5 = (int)this.DatabaseManager.ExecuteScalar("DECLARE @rm bit\r\nIF EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=3)\r\n\tSET @rm = 1\r\nELSE\r\n\tSET @rm = 0\r\nIF NOT EXISTS(SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid)\r\nBEGIN\r\n    INSERT INTO attendees (personid,appointmentid,noshow,misccode) VALUES (@pid,@appid,@noshow,@misccode);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS attendeeid\r\nEND\r\nELSE\r\nBEGIN\r\n    UPDATE attendees SET noshow=@noshow,misccode=@misccode WHERE appointmentid=@appid AND personid=@pid;\r\n    SELECT attendeeid FROM attendees WHERE appointmentid=@appid AND personid=@pid;\r\nEND", parameters);
				bool flag5 = num5 < 1;
				if (flag5)
				{
					string message2 = "Failed to create point of contact staff attendee entry in database [appid=" + num3.ToString() + "].";
					CWLogger.Logger.Error(message2);
					throw new Exception(message2);
				}
				PointOfContact.Staff.AttendeeId = num5;
			}
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.InsertOrUpdateAppointmentMemo(num3, PointOfContact.Memo, null);
			bool flag6 = PointOfContact.SessionNotesData != null && PointOfContact.SessionNotesData.Count > 0;
			if (flag6)
			{
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = PointOfContact.Student.Person.PersonId,
					SecondaryId = num3
				};
				IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
				dynamicDataDAO.SaveData(context, PointOfContact.SessionNotesData, eDynamicFormType.PerAppointment);
			}
			return num3;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00054ADC File Offset: 0x00052CDC
		public void UpdatePointOfContact(PointOfContact PointOfContact)
		{
			DbParameter[] array = new DbParameter[5];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, PointOfContact.StartDateTime);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, PointOfContact.EndDateTime);
			int num = 2;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeid";
			DbType pType = DbType.Int32;
			AppType appType = PointOfContact.AppType;
			array[num] = databaseManager.GetParameter(pName, pType, (appType != null) ? appType.AppTypeId : -1);
			int num2 = 3;
			DatabaseLayer databaseManager2 = this.DatabaseManager;
			string pName2 = "@appcode";
			DbType pType2 = DbType.Int32;
			AppShowTimeAsType showTimeAs = PointOfContact.ShowTimeAs;
			array[num2] = databaseManager2.GetParameter(pName2, pType2, (showTimeAs != null) ? showTimeAs.AppCode : 0);
			array[4] = this.DatabaseManager.GetParameter("@subtitle", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(PointOfContact.SubTitle ?? ""));
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET startdate=@startdate,enddate=@enddate,apptypeid=@apptypeid,appcode=@appcode,subject=@subtitle\r\nWHERE appointmentid=@appid", parameters);
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.InsertOrUpdateAppointmentMemo(PointOfContact.AppointmentId, PointOfContact.Memo, null);
			List<DynamicData> list = PointOfContact.SessionNotesData ?? new List<DynamicData>();
			DynamicDataContext context = new DynamicDataContext
			{
				PrimaryId = PointOfContact.Student.Person.PersonId,
				SecondaryId = PointOfContact.AppointmentId
			};
			IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
			dynamicDataDAO.SaveData(context, PointOfContact.SessionNotesData, eDynamicFormType.PerAppointment);
		}
	}
}
