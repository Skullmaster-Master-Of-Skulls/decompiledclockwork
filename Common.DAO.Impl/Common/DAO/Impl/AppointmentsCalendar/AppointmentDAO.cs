using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000164 RID: 356
	public class AppointmentDAO : IAppointmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0006C75A File Offset: 0x0006A95A
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0006C762 File Offset: 0x0006A962
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0006C76C File Offset: 0x0006A96C
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

		// Token: 0x06000A5C RID: 2652 RVA: 0x0006C7A2 File Offset: 0x0006A9A2
		public AppointmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0006C7D3 File Offset: 0x0006A9D3
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0006C7DB File Offset: 0x0006A9DB
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A5F RID: 2655 RVA: 0x0006C7E4 File Offset: 0x0006A9E4
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

		// Token: 0x06000A60 RID: 2656 RVA: 0x0006C824 File Offset: 0x0006AA24
		private static void AddCalendarInfoToBaseExtendedAppointment(IDataReader reader, ref Appointment app, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = app == null || reader == null;
			if (!flag)
			{
				bool flag2 = AppointmentDAO.ReaderContainsColumn(reader, "iconnum");
				if (flag2)
				{
					int iconNum = (reader["iconnum"] is DBNull) ? -1 : ((int)reader["iconnum"]);
					bool flag3 = iconNum >= 0 && app.Icons.FirstOrDefault((AppointmentIcon f) => f.IconNum == iconNum) == null;
					if (flag3)
					{
						int num = (reader["screennum"] is DBNull) ? 0 : ((int)reader["screennum"]);
						AppointmentIcon appointmentIcon = new AppointmentIcon();
						object screen;
						if (num <= 0)
						{
							screen = null;
						}
						else
						{
							(screen = new DynamicFormBase()).ScreenNum = num;
						}
						appointmentIcon.Screen = screen;
						appointmentIcon.Icon = new IconInfo
						{
							IconNum = iconNum,
							IconText = reader["icontext"].ToString(),
							IconLetterIdentifier = reader["iconletteridentifier"].ToString()
						};
						AppointmentIcon item = appointmentIcon;
						app.Icons.Add(item);
					}
				}
				bool flag4 = app.CaseInfo == null && AppointmentDAO.ReaderContainsColumn(reader, "caseid");
				if (flag4)
				{
					int num2 = (reader["caseid"] == DBNull.Value) ? 0 : ((int)reader["caseid"]);
					bool flag5 = num2 > 0;
					if (flag5)
					{
						app.CaseInfo = new CaseBase
						{
							InfoPcId = num2
						};
					}
				}
				bool flag6 = app.TestExamInfo == null && AppointmentDAO.ReaderContainsColumn(reader, "lucourseid");
				if (flag6)
				{
					int num3 = (reader["lucourseid"] == DBNull.Value) ? 0 : ((int)reader["lucourseid"]);
					bool flag7 = num3 > 0;
					if (flag7)
					{
						bool flag8 = batchDecryptor == null;
						if (flag8)
						{
							batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
						}
						app.TestExamInfo = new BasicAppointmentTestExamInfo
						{
							Course = new LookupCourseBase
							{
								Subject = new LookupSubject
								{
									SubjectDescription = reader["subject"].ToString()
								},
								Course = reader["course"].ToString(),
								LuCourseId = num3
							},
							TestNote = ((reader["testnote"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])reader["testnote"])),
							StudentNote = ((reader["studentnote"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])reader["studentnote"])),
							ExamId = (AppointmentDAO.ReaderContainsColumn(reader, "examid") ? ((reader["examid"] is DBNull) ? 0 : ((int)reader["examid"])) : 0)
						};
					}
				}
				bool flag9 = app.WorkshopInfo == null && AppointmentDAO.ReaderContainsColumn(reader, "workshopid");
				if (flag9)
				{
					int num4 = (reader["workshopid"] == DBNull.Value) ? 0 : ((int)reader["workshopid"]);
					bool flag10 = num4 > 0;
					if (flag10)
					{
						app.WorkshopInfo = new AppointmentWorkshopInfo
						{
							WorkshopId = num4,
							WorkshopTitle = reader["workshoptitle"].ToString()
						};
					}
				}
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0006CBD4 File Offset: 0x0006ADD4
		internal static IList<Appointment> GetAppointmentsFromReader(IDataReader reader, OperationContext opContext)
		{
			bool flag = reader != null;
			IList<Appointment> result;
			if (flag)
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				List<Appointment> list = new List<Appointment>();
				Appointment appointment = null;
				while (reader.Read())
				{
					int num = (int)reader["appointmentid"];
					bool flag2 = appointment == null || appointment.AppointmentId != num;
					if (flag2)
					{
						appointment = BaseAppointmentDAO.GetMainBaseExtendedAppointment<Appointment>(reader, opContext, batchDecryptor);
						list.Add(appointment);
					}
					BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(reader, appointment, opContext, batchDecryptor);
					AppointmentDAO.AddCalendarInfoToBaseExtendedAppointment(reader, ref appointment, opContext, batchDecryptor);
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0006CC8C File Offset: 0x0006AE8C
		[DebuggerStepThrough]
		private Task<IList<Appointment>> GetAppointmentsFromReaderAsync(DbDataReader reader)
		{
			AppointmentDAO.<GetAppointmentsFromReaderAsync>d__15 <GetAppointmentsFromReaderAsync>d__ = new AppointmentDAO.<GetAppointmentsFromReaderAsync>d__15();
			<GetAppointmentsFromReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<Appointment>>.Create();
			<GetAppointmentsFromReaderAsync>d__.<>4__this = this;
			<GetAppointmentsFromReaderAsync>d__.reader = reader;
			<GetAppointmentsFromReaderAsync>d__.<>1__state = -1;
			<GetAppointmentsFromReaderAsync>d__.<>t__builder.Start<AppointmentDAO.<GetAppointmentsFromReaderAsync>d__15>(ref <GetAppointmentsFromReaderAsync>d__);
			return <GetAppointmentsFromReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0006CCD8 File Offset: 0x0006AED8
		public Appointment LoadDeletedAppointmentById(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    @appid AS appointmentid,aa.startdate,aa.enddate,aa.apptypeid,at.[description],\r\n            aa.subject AS subtitle,aatt.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM    archive_appointments aa LEFT JOIN archive_attendees aatt ON aatt.appointmentid=aa.appointmentid\r\n        LEFT JOIN people p ON p.personid=aatt.personid\r\n        LEFT JOIN AppointmentTypes at ON aa.apptypeid=at.apptypeid\r\nWHERE   aa.appointmentid=@appid AND aa.auditaction='DEL'\r\nORDER BY aa.auditdatetime DESC", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					bool flag2 = dataReader["apptypeid"] != DBNull.Value;
					AppType appType;
					if (flag2)
					{
						appType = new AppType
						{
							AppTypeId = (int)dataReader["apptypeid"],
							Description = dataReader["description"].ToString()
						};
					}
					else
					{
						appType = null;
					}
					bool flag3 = dataReader["personid"] != DBNull.Value;
					Attendee attendee;
					if (flag3)
					{
						attendee = new Attendee
						{
							IsNoShow = false,
							Person = new PersonBase
							{
								PersonId = (int)dataReader["personid"],
								FirstName = ((dataReader["firstname"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["firstname"])),
								MiddleName = ((dataReader["middlename"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["middlename"])),
								LastName = ((dataReader["lastname"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["lastname"])),
								Student_no = ((dataReader["student_no"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["student_no"]))
							}
						};
					}
					else
					{
						attendee = null;
					}
					List<Attendee> list = new List<Attendee>();
					bool flag4 = attendee != null;
					if (flag4)
					{
						list.Add(attendee);
					}
					Appointment appointment = new Appointment
					{
						AppointmentId = AppointmentId,
						StartDateTime = (DateTime)dataReader["startdate"],
						EndDateTime = (DateTime)dataReader["enddate"],
						AppType = appType,
						Attendees = list,
						SubTitle = ((dataReader["subtitle"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["subtitle"]))
					};
					while (dataReader.Read())
					{
						bool flag5 = dataReader["personid"] != DBNull.Value;
						if (flag5)
						{
							int pid = (int)dataReader["personid"];
							bool flag6 = appointment.Attendees.Find((Attendee a) => a.Person.PersonId == pid) == null;
							if (flag6)
							{
								appointment.Attendees.Add(new Attendee
								{
									IsNoShow = false,
									Person = new PersonBase
									{
										PersonId = (int)dataReader["personid"],
										FirstName = ((dataReader["firstname"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["firstname"])),
										MiddleName = ((dataReader["middlename"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["middlename"])),
										LastName = ((dataReader["lastname"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["lastname"])),
										Student_no = ((dataReader["student_no"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])dataReader["student_no"]))
									}
								});
							}
						}
					}
					return appointment;
				}
			}
			return null;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0006D174 File Offset: 0x0006B374
		public Appointment LoadAppointment(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			Appointment result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT a.appointmentid,a.apptypeid,a.startdate,a.enddate,a.cancelled\r\n ,att.personid,att.noshow,att.misccode,am.memotext,ai.screennum,ai.iconnum,aif.icontext,aif.iconletteridentifier\r\n ,a.dateadded,a.whoadded,am.isencrypted,a.ishidden,a.islocked,a.overridecolour\r\n ,p.firstname,p.lastname,p.student_no,pg.groupid,aw.workshopid,ac.lucourseid,w.workshoptitle\r\n ,w.maxattendees,lucd.altlookupstring AS subject,lc.course,a.extraattendeescount\r\n ,a.appcode,a.groupcode,ac.originalstartdatetime,ac.originalenddatetime\r\n ,ac.appointmentcourseid,ac.testnote,lucd2.altlookupstring,lucd2.email,lucd2.phone,lc.section\r\n ,ac.studentnote,acr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle\r\n ,acr.cancelreasontext,acr.cancelledbypersonid,acr.cancelleddate \r\n ,a.actualstarttime,a.actualendtime,a.subject AS subtitle,a.location,aw.maxattendees AS appmaxattendees ,a.caseid \r\n ,at.description AS apptypedescription,atg.title AS apptypegrouptitle,at.appointmenttypegroupid\r\n ,at.defaultcolour,att.attendeeid,a.examid\r\n FROM apps a LEFT JOIN appointmentmemos am ON am.appointmentid=a.appointmentid \r\n LEFT JOIN appointmenticons ai ON ai.appointmentid=a.appointmentid \r\n LEFT JOIN AppointmentIconInfo aif ON aif.iconindex=ai.iconnum\r\n LEFT JOIN attendees att ON att.appointmentid=a.appointmentid \r\n LEFT JOIN people p ON p.personid=att.personid \r\n LEFT JOIN peoplegroups pg ON pg.personid=att.personid AND pg.groupid<10 --pg.isprimarygroup=1 \r\n LEFT JOIN appointmentworkshops aw ON aw.appointmentid=a.appointmentid \r\n LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid \r\n LEFT JOIN workshops w ON w.workshopid=aw.workshopid \r\n LEFT JOIN lucourses lc ON lc.lucourseid=ac.lucourseid \r\n LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=lc.subjectid \r\n LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=lc.instructorid \r\n LEFT JOIN appointmentcancelledreason acr ON acr.appointmentid=a.appointmentid LEFT JOIN cancelreason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\n WHERE a.appointmentid=@appid\r\n ORDER BY a.startdate,a.appointmentid,pg.groupid,a.personid,ai.screennum,ai.iconnum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Appointment> list = AppointmentDAO.GetAppointmentsFromReader(dataReader, this.OpContext).ToList<Appointment>();
					bool flag2 = list == null || list.Count < 1;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = list[0];
					}
				}
			}
			return result;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0006D218 File Offset: 0x0006B418
		public List<Appointment> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime)
		{
			DbParameter[] array = new DbParameter[7];
			array[0] = this.DatabaseManager.GetParameter("@sd", DbType.DateTime, (StartDateTime == DateTime.MinValue) ? DBNull.Value : StartDateTime.Date);
			array[1] = this.DatabaseManager.GetParameter("@ed", DbType.DateTime, (EndDateTime == DateTime.MinValue) ? DBNull.Value : EndDateTime.AddDays(1.0).Date);
			array[2] = this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, HideCancelled);
			int num = 3;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeids";
			DbType pType = DbType.String;
			object value;
			if (AppTypeIds != null)
			{
				value = string.Join(",", AppTypeIds.ConvertAll<string>((int at) => at.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			int num2 = 4;
			DatabaseLayer databaseManager2 = this.DatabaseManager;
			string pName2 = "@pids";
			DbType pType2 = DbType.String;
			object value2;
			if (PersonIds != null)
			{
				value2 = string.Join(",", PersonIds.ConvertAll<string>((int p) => p.ToString()).ToArray());
			}
			else
			{
				value2 = "";
			}
			array[num2] = databaseManager2.GetParameter(pName2, pType2, value2);
			array[5] = this.DatabaseManager.GetParameter("@checkpsicons", DbType.Boolean, LoadPerStudentDataIcons);
			array[6] = this.DatabaseManager.GetParameter("@checkanicons", DbType.Boolean, LoadPerAnonymousDataIcons);
			DbParameter[] parameters = array;
			List<Appointment> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SET ARITHABORT ON \r\nEXECUTE LoadAppointments @pids,@apptypeids,@sd,@ed,@checkpsicons,@checkanicons,@hidecancelled", parameters))
			{
				result = ((dataReader == null) ? null : AppointmentDAO.GetAppointmentsFromReader(dataReader, this.OpContext).ToList<Appointment>());
			}
			return result;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0006D3E8 File Offset: 0x0006B5E8
		[DebuggerStepThrough]
		public Task<IList<Appointment>> LoadAppointmentsAsync(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime)
		{
			AppointmentDAO.<LoadAppointmentsAsync>d__19 <LoadAppointmentsAsync>d__ = new AppointmentDAO.<LoadAppointmentsAsync>d__19();
			<LoadAppointmentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<Appointment>>.Create();
			<LoadAppointmentsAsync>d__.<>4__this = this;
			<LoadAppointmentsAsync>d__.PersonIds = PersonIds;
			<LoadAppointmentsAsync>d__.AppTypeIds = AppTypeIds;
			<LoadAppointmentsAsync>d__.HideCancelled = HideCancelled;
			<LoadAppointmentsAsync>d__.LoadPerStudentDataIcons = LoadPerStudentDataIcons;
			<LoadAppointmentsAsync>d__.LoadPerAnonymousDataIcons = LoadPerAnonymousDataIcons;
			<LoadAppointmentsAsync>d__.StartDateTime = StartDateTime;
			<LoadAppointmentsAsync>d__.EndDateTime = EndDateTime;
			<LoadAppointmentsAsync>d__.<>1__state = -1;
			<LoadAppointmentsAsync>d__.<>t__builder.Start<AppointmentDAO.<LoadAppointmentsAsync>d__19>(ref <LoadAppointmentsAsync>d__);
			return <LoadAppointmentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0006D464 File Offset: 0x0006B664
		public int RecoverDeletedAppointment(int AppointmentId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,\r\nappcode,groupcode,caseid,examid,totalbreakminutes,sittingid)\r\nSELECT \r\napptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,\r\nappcode,groupcode,caseid,examid,totalbreakminutes,sittingid\r\nFROM archive_appointments \r\nWHERE appointmentid=@appid;\r\n\r\nDECLARE @newappid int\r\nSET @newappid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() As int))\r\n\r\nINSERT INTO attendees(PersonID,AppointmentID,noShow,miscCode) \r\nSELECT personid,@newappid,noshow,misccode FROM archive_attendees \r\nWHERE AppointmentID=@appid;\r\n\r\nINSERT INTO AppointmentCourses(AppointmentID,LUCourseID,originalStartDateTime,originalEndDateTime,testNote,studentNote) \r\nSELECT @newappid,lucourseid,originalStartDateTime,originalEndDateTime,testNote,studentNote\r\nFROM archive_appointmentCourses WHERE AppointmentID=@appid;\r\n\r\nINSERT INTO AppointmentWorkshops(AppointmentID,WorkshopID,PublishOnline,location,maxattendees) \r\nSELECT @newappid,WorkshopID,PublishOnline,location,maxattendees\r\nFROM archive_appointmentWorkshops WHERE AppointmentID=@appid;\r\n\r\nINSERT INTO AppointmentMemos(AppointmentID,memotext,isencrypted)\r\nSELECT @newappid,memotext,isencrypted\r\nFROM archive_appointmentMemos WHERE AppointmentID=@appid;\r\n\r\nSELECT @newappid AS appointmentid", parameters);
			int num = (int)obj;
			bool flag = num > 0;
			if (flag)
			{
				string[] array = new string[]
				{
					"AccommodationsTest",
					"AppointmentCancelledReason",
					"AppointmentIcons",
					"AppointmentLastModifiedDate",
					"AppointmentNotesArchive",
					"AppointmentRecurringInstance",
					"AppointmentsDeleted",
					"appointmentsdeleteddates",
					"AppointmentsModifiedDates",
					"AppointmentsReminder_Notification",
					"Availability2Items",
					"Caching_Appointments",
					"DateTimeInfoPA",
					"DateTimeInfoPJA",
					"emailout",
					"ImageInfoPA",
					"ImageInfoPJA",
					"MainInfoPA",
					"MainInfoPJA",
					"OtherInfoPA",
					"OtherInfoPJA",
					"WorkshopFeesPaid",
					"WaitingList",
					"ScreenData"
				};
				List<Exception> list = new List<Exception>();
				foreach (string str in array)
				{
					string query = "UPDATE " + str + " SET appointmentid=@appidnew WHERE appointmentid=@appidold";
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@appidold", DbType.Int32, AppointmentId),
						this.DatabaseManager.GetParameter("@appidnew", DbType.Int32, num)
					};
					try
					{
						this.DatabaseManager.ExecuteNonQuery(query, parameters);
					}
					catch (Exception item)
					{
						list.Add(item);
					}
				}
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					CWLogger.Logger.Error("AppointmentDAO:RecoverDeletedAppointment:Appid={0}:NewAppId={1}:errs={2}", AppointmentId.ToString(), num.ToString(), string.Join("\r\n", list.ToList<Exception>().ConvertAll<string>((Exception g) => "* " + g.ToString()).ToArray()));
				}
			}
			return num;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0006D6B4 File Offset: 0x0006B8B4
		public void MergeAllAppointments(int PersonIdNew, int PersonIdOld)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld)
			};
			List<int> list = new List<int>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT appointmentid FROM (SELECT appointmentid FROM attendees WHERE personid=@oldpid UNION SELECT appointmentid FROM appointments WHERE personid=@oldpid) x", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						int num = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
						bool flag2 = num > 0;
						if (flag2)
						{
							list.Add(num);
						}
					}
				}
			}
			DbParameter[] parameters2 = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew),
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE attendees SET personid=@newpid WHERE personid=@oldpid;\r\nUPDATE appointments SET personid=@newpid WHERE personid=@oldpid", parameters2);
			foreach (int appointmentId in list)
			{
				this.appLogDao.LogAppModifications(appointmentId, eHowModifiedCode.InsertUpdate, eAppointmentModifiedItemType.None);
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0006D80C File Offset: 0x0006BA0C
		public int CreateAppointmentEnsureUsersNotDoubleBooked(Appointment app, int[] PidsToEnsureNotDoubleBooked, DbTransaction transaction = null)
		{
			bool flag = PidsToEnsureNotDoubleBooked == null || PidsToEnsureNotDoubleBooked.Length < 1;
			int result;
			if (flag)
			{
				result = this.CreateAppointment(app, transaction);
			}
			else
			{
				IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
				int num = baseAppointmentDAO.CreateBaseExtendedAppointmentEnsureUsersNotDoubleBooked(app, PidsToEnsureNotDoubleBooked, transaction);
				bool flag2 = num < 1;
				if (flag2)
				{
					throw new Exception("AppointmentDAO:CreateAppointmentEnsureUsersNotDoubleBooked:Couldn't create appointment");
				}
				this.CreateAppointmentExtendedParts(num, app, transaction);
				result = num;
			}
			return result;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0006D870 File Offset: 0x0006BA70
		public int GetNumberOfAppointmentsWithAppType(int appTypeId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@apptypeid", DbType.Int32, appTypeId)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT COUNT(appointmentid) FROM appointments WHERE apptypeid=@apptypeid", parameters);
			bool flag = obj == null || obj is DBNull || !(obj is int);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0006D8F0 File Offset: 0x0006BAF0
		public void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@apptypeidtoreplace", DbType.Int32, appTypeIdToReplace),
				databaseLayer.GetParameter("@apptypeidtokeep", DbType.Int32, appTypeIdToKeep)
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointments SET apptypeid=@apptypeidtokeep WHERE apptypeid=@apptypeidtoreplace", parameters);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0006D958 File Offset: 0x0006BB58
		public int CreateAppointment(Appointment app, DbTransaction transaction = null)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			int num = baseAppointmentDAO.CreateBaseExtendedAppointment(app, transaction);
			bool flag = num < 1;
			if (flag)
			{
				throw new Exception("AppointmentDAO:CreateAppointment:Couldn't create appointment");
			}
			this.CreateAppointmentExtendedParts(num, app, transaction);
			return num;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0006D9A0 File Offset: 0x0006BBA0
		private void CreateAppointmentExtendedParts(int appId, Appointment app, DbTransaction transaction = null)
		{
			bool flag = app.CancelInfo != null;
			if (flag)
			{
				IAppointmentCancelInfoDAO appointmentCancelInfoDAO = new AppointmentCancelInfoDAO(this.OpContext);
				appointmentCancelInfoDAO.InsertOrUpdateAppointmentCancelInfo(appId, app.CancelInfo, transaction);
			}
			bool flag2 = app.Icons != null;
			if (flag2)
			{
				IAppointmentIconDAO appointmentIconDAO = new AppointmentIconDAO(this.OpContext);
				foreach (AppointmentIcon appointmentIcon in app.Icons)
				{
					appointmentIcon.AppointmentIconId = appointmentIconDAO.InsertOrUpdateAppointmentIcon(appId, appointmentIcon, transaction);
				}
			}
			bool flag3 = app.WorkshopInfo != null;
			if (flag3)
			{
				WorkshopAppointmentDAO.UpdateWorkshopAppointmentInfo(appId, app.WorkshopInfo.WorkshopId, app.WorkshopInfo.MaxAttendeeCount, this.OpContext, null);
			}
			bool flag4 = app.CaseInfo != null && app.CaseInfo.InfoPcId > 0;
			if (flag4)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, appId),
					this.DatabaseManager.GetParameter("@caseid", DbType.Int32, app.CaseInfo.InfoPcId)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET caseid=@caseid WHERE appointmentid=@appid", parameters);
			}
			TestBookingDAO.CreateTestExamInfo(appId, app.TestExamInfo, this.OpContext, null);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0006DB0C File Offset: 0x0006BD0C
		public void UpdateAppointment(Appointment Appointment, DbTransaction transaction = null)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateBaseExtendedAppointment(Appointment, transaction);
			IAppointmentCancelInfoDAO appointmentCancelInfoDAO = new AppointmentCancelInfoDAO(this.OpContext);
			appointmentCancelInfoDAO.InsertOrUpdateAppointmentCancelInfo(Appointment.AppointmentId, Appointment.CancelInfo, transaction);
			IList<AppointmentIcon> list = Appointment.Icons ?? new List<AppointmentIcon>();
			IAppointmentIconDAO appointmentIconDAO = new AppointmentIconDAO(this.OpContext);
			List<int> iconNums = list.ToList<AppointmentIcon>().ConvertAll<int>((AppointmentIcon f) => f.IconNum);
			appointmentIconDAO.DeleteAppointmentIconsNotInList(Appointment.AppointmentId, iconNums, transaction);
			foreach (AppointmentIcon icon in list)
			{
				appointmentIconDAO.InsertOrUpdateAppointmentIcon(Appointment.AppointmentId, icon, transaction);
			}
			AppointmentWorkshopInfo workshopInfo = Appointment.WorkshopInfo;
			int workshopId = (workshopInfo != null) ? workshopInfo.WorkshopId : 0;
			WorkshopAppointmentDAO.UpdateWorkshopAppointmentInfo(Appointment.AppointmentId, workshopId, (Appointment.WorkshopInfo == null) ? 0 : Appointment.WorkshopInfo.MaxAttendeeCount, this.OpContext, null);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, Appointment.AppointmentId),
				this.DatabaseManager.GetParameter("@caseid", DbType.Int32, (Appointment.CaseInfo == null || Appointment.CaseInfo.InfoPcId < 1) ? DBNull.Value : Appointment.CaseInfo.InfoPcId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET caseid=@caseid WHERE appointmentid=@appid", parameters);
			BasicAppointmentTestExamInfo testExamInfo = Appointment.TestExamInfo;
			int num = (testExamInfo != null) ? testExamInfo.ExamId : 0;
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, Appointment.AppointmentId),
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, (num > 0) ? num : DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointments SET examid=@examid WHERE appointmentid=@appid", parameters);
			int num2 = (Appointment.TestExamInfo == null || Appointment.TestExamInfo.Course == null) ? 0 : Appointment.TestExamInfo.Course.LuCourseId;
			DbParameter[] array = new DbParameter[7];
			array[0] = this.DatabaseManager.GetOutputParameter("@appointmentcourseid", DbType.Int32, 0);
			array[1] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, Appointment.AppointmentId);
			array[2] = this.DatabaseManager.GetParameter("@lucid", DbType.Int32, num2);
			int num3 = 3;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@originalstartdatetime";
			DbType pType = DbType.DateTime;
			BasicAppointmentTestExamInfo testExamInfo2 = Appointment.TestExamInfo;
			DateTime? dateTime = (testExamInfo2 != null) ? new DateTime?(testExamInfo2.ClassStartDateTime) : null;
			array[num3] = databaseManager.GetParameter(pName, pType, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
			int num4 = 4;
			DatabaseLayer databaseManager2 = this.DatabaseManager;
			string pName2 = "@originalenddatetime";
			DbType pType2 = DbType.DateTime;
			BasicAppointmentTestExamInfo testExamInfo3 = Appointment.TestExamInfo;
			dateTime = ((testExamInfo3 != null) ? new DateTime?(testExamInfo3.ClassEndDateTime) : null);
			array[num4] = databaseManager2.GetParameter(pName2, pType2, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
			int num5 = 5;
			DatabaseLayer databaseManager3 = this.DatabaseManager;
			string pName3 = "@testnote";
			DbType pType3 = DbType.Binary;
			BasicAppointmentTestExamInfo testExamInfo4 = Appointment.TestExamInfo;
			array[num5] = databaseManager3.GetParameter(pName3, pType3, string.IsNullOrEmpty((testExamInfo4 != null) ? testExamInfo4.TestNote : null) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(Appointment.TestExamInfo.TestNote));
			int num6 = 6;
			DatabaseLayer databaseManager4 = this.DatabaseManager;
			string pName4 = "@studentnote";
			DbType pType4 = DbType.Binary;
			BasicAppointmentTestExamInfo testExamInfo5 = Appointment.TestExamInfo;
			array[num6] = databaseManager4.GetParameter(pName4, pType4, string.IsNullOrEmpty((testExamInfo5 != null) ? testExamInfo5.StudentNote : null) ? DBNull.Value : this.DatabaseManager.Encryption.Encrypt(Appointment.TestExamInfo.StudentNote));
			parameters = array;
			this.DatabaseManager.ExecuteNonQuery("IF @lucid IS NULL OR @lucid<1\r\nBEGIN\r\n    DELETE FROM appointmentcourses WHERE appointmentid=@appid\r\n    SET @appointmentcourseid=0\r\nEND\r\nELSE\r\nBEGIN\r\n    IF EXISTS(SELECT appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)\r\n        UPDATE appointmentcourses SET lucourseid=@lucid,originalstartdatetime=@originalstartdatetime,originalenddatetime=@originalenddatetime,testnote=@testnote,studentnote=@studentnote \r\n        WHERE appointmentid=@appid\r\n    ELSE \r\n        INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote)\r\n        VALUES (@appid,@lucid,@originalstartdatetime,@originalenddatetime,@testnote,@studentnote)\r\n\r\n    SET @appointmentcourseid=(SELECT TOP 1 appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)\r\nEND", parameters);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0006DF00 File Offset: 0x0006C100
		public void CancelAppointment(int AppointmentId, AppCancelInfo CancelInfo, DbTransaction transaction = null)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateAppointmentCancelledValue(AppointmentId, true, CancelInfo, transaction);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0006DF28 File Offset: 0x0006C128
		public void UnCancelAppointment(int AppointmentId, DbTransaction transaction = null)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateAppointmentCancelledValue(AppointmentId, false, null, transaction);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0006DF50 File Offset: 0x0006C150
		public void MarkAppointmentTentative(int AppointmentId, DbTransaction transaction = null)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateAppointmentAppCodeValue(AppointmentId, -1, transaction);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0006DF74 File Offset: 0x0006C174
		public void UnMarkAppointmentTentative(int AppointmentId, DbTransaction transaction = null)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			BaseBasicAppointment baseBasicAppointment = baseAppointmentDAO.LoadBaseBasicAppointmentById(AppointmentId);
			bool flag = baseBasicAppointment == null;
			if (!flag)
			{
				AppShowTimeAsType showTimeAs = baseBasicAppointment.ShowTimeAs;
				int num = (showTimeAs != null) ? showTimeAs.AppCode : 0;
				bool flag2 = num == -1;
				if (flag2)
				{
					baseAppointmentDAO.UpdateAppointmentAppCodeValue(AppointmentId, 0, transaction);
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0006DFC8 File Offset: 0x0006C1C8
		public void UpdateAttendeeNoShow(int AppointmentId, int PersonId, bool newNoShow, DbTransaction transaction = null)
		{
			IAppointmentAttendeeDAO appointmentAttendeeDAO = new AppointmentAttendeeDAO(this.OpContext);
			appointmentAttendeeDAO.UpdateNoShowValue(AppointmentId, PersonId, newNoShow, transaction);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
		public IDictionary<int, IList<AppointmentBasicSlot>> LoadUncancelledBookedSlots(IList<int> personIds, DateTime startDate, int numDays)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", (from g in personIds
			select g.ToString()).ToArray<string>()));
			array[1] = this.DatabaseManager.GetParameter("@startDate", DbType.DateTime, startDate.Date);
			array[2] = this.DatabaseManager.GetParameter("@endDate", DbType.DateTime, startDate.Date.AddDays((double)(numDays - 1)));
			DbParameter[] parameters = array;
			IDictionary<int, IList<AppointmentBasicSlot>> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startDate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @endDate))\r\n\r\nSELECT orderid AS personid INTO #tpids FROM splitorderids(@pids,',')\r\n\r\nSELECT  DISTINCT app.appointmentid,app.startdate,app.enddate,att.PersonID\r\nFROM    appointments app LEFT JOIN attendees att ON att.AppointmentID=app.AppointmentID\r\nWHERE   app.startDate>=@sd AND app.startDate<@ed \r\n        AND app.cancelled=0\r\n        AND att.personid IN (SELECT personid FROM #tpids)\r\nORDER BY att.PersonID,app.appointmentid\r\n\r\nDROP TABLE #tpids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, IList<AppointmentBasicSlot>> dictionary = new Dictionary<int, IList<AppointmentBasicSlot>>();
					IList<AppointmentBasicSlot> list = null;
					int num = 0;
					while (dataReader.Read())
					{
						int num2 = (int)dataReader["personid"];
						bool flag2 = list == null || num != num2;
						if (flag2)
						{
							list = new List<AppointmentBasicSlot>();
							num = num2;
							dictionary.Add(num, list);
						}
						list.Add(new AppointmentBasicSlot
						{
							AppointmentId = (int)dataReader["appointmentid"],
							StartDateTime = (DateTime)dataReader["startdate"],
							EndDateTime = (DateTime)dataReader["enddate"]
						});
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0006E198 File Offset: 0x0006C398
		public IList<Appointment> LoadAllAppointmentsInADay(DateTime DayToLoadAppointmentsFor, bool ShowCancelled = false, int NumDaysToLoadAppointmentsFor = 1, int[] AppTypeIds = null)
		{
			DbParameter[] array = new DbParameter[7];
			array[0] = this.DatabaseManager.GetParameter("@sd", DbType.DateTime, DayToLoadAppointmentsFor.Date);
			array[1] = this.DatabaseManager.GetParameter("@ed", DbType.DateTime, DayToLoadAppointmentsFor.Date.AddDays((double)NumDaysToLoadAppointmentsFor).AddMinutes(-1.0));
			array[2] = this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, !ShowCancelled);
			int num = 3;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeids";
			DbType pType = DbType.String;
			object value;
			if (AppTypeIds != null)
			{
				value = string.Join(",", AppTypeIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			array[4] = this.DatabaseManager.GetParameter("@pids", DbType.String, "");
			array[5] = this.DatabaseManager.GetParameter("@checkpsicons", DbType.Boolean, false);
			array[6] = this.DatabaseManager.GetParameter("@checkanicons", DbType.Boolean, false);
			DbParameter[] parameters = array;
			IList<Appointment> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SET ARITHABORT ON \r\nEXECUTE LoadAppointments @pids,@apptypeids,@sd,@ed,@checkpsicons,@checkanicons,@hidecancelled", parameters))
			{
				result = ((dataReader == null) ? null : AppointmentDAO.GetAppointmentsFromReader(dataReader, this.OpContext).ToList<Appointment>());
			}
			return result;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0006E318 File Offset: 0x0006C518
		public int GetNumberOfNonCancelledAppointments(int PersonId, DateTime StartDate, DateTime? EndDate, bool excludeTestsExams, params int[] AppTypeIdsToCheck)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, (EndDate != null) ? EndDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@apptypeids", DbType.String, (AppTypeIdsToCheck == null || AppTypeIdsToCheck.Length < 1) ? DBNull.Value : AppTypeIdsToCheck.ToList<int>().CommaSeparatedValuesWithoutSpace<int>()),
				databaseLayer.GetParameter("@excludeTestsExams", DbType.Boolean, excludeTestsExams)
			};
			return (int)databaseLayer.ExecuteScalar("SELECT orderid AS apptypeid INTO #t1 FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nIF NOT @enddate IS NULL\r\n\tSET @enddate=DATEADD(day,1,DATEADD(D, 0, DATEDIFF(D, 0, @enddate)))\r\n\r\nSELECT\tCOUNT(DISTINCT att.appointmentid)\r\nFROM\tattendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID \r\nWHERE\tatt.PersonID=@pid AND app.cancelled=0 \r\n\t\tAND app.startDate >= @startdate\r\n\t\tAND (@enddate IS NULL OR app.endDate<@enddate)\r\n\t\tAND (@apptypeids IS NULL OR @apptypeids='' OR app.AppTypeID IN (SELECT apptypeid FROM #t1))\r\n        AND NOT (DATEPART(hh,app.startdate)=0 AND DATEPART(hh,app.enddate)=1 AND DATEPART(n,app.startdate)=0 AND DATEPART(n,app.enddate)=0) --not a poc\r\n        AND (@excludeTestsExams=0 OR app.examid IS NULL OR app.examid<1 )\r\n\r\nDROP TABLE #t1", parameters);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0006E3F8 File Offset: 0x0006C5F8
		public int GetNumberOfConsecutiveNoshows(int PersonId, DateTime StartDate, int MaxNumberOfNoShowsToCheckFor, params int[] AppTypeIdsToCheck)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate),
				databaseLayer.GetParameter("@maxnum", DbType.Int32, MaxNumberOfNoShowsToCheckFor),
				databaseLayer.GetParameter("@apptypeids", DbType.String, (AppTypeIdsToCheck == null || AppTypeIdsToCheck.Length < 1) ? DBNull.Value : AppTypeIdsToCheck.ToList<int>().CommaSeparatedValuesWithoutSpace<int>())
			};
			int result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS apptypeid INTO #t1 FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nSELECT\tDISTINCT TOP(@maxnum) app.AppointmentID,att.noShow,app.startDate\r\nFROM\tattendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID \r\nWHERE\tatt.PersonID=@pid \r\n\t\tAND app.cancelled=0 \r\n\t\tAND app.startDate <= @startdate\r\n\t\tAND (@apptypeids IS NULL OR @apptypeids='' OR app.AppTypeID IN (SELECT apptypeid FROM #t1))\r\nORDER BY app.startDate DESC\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = 0;
				}
				else
				{
					int num = 0;
					while (dataReader.Read())
					{
						bool flag2 = !(dataReader["noshow"] is DBNull) && Convert.ToBoolean(dataReader["noshow"]);
						bool flag3 = !flag2;
						if (flag3)
						{
							return num;
						}
						num++;
					}
					result = num;
				}
			}
			return result;
		}

		// Token: 0x04000661 RID: 1633
		private IAppointmentLogDAO _appLogDao;
	}
}
