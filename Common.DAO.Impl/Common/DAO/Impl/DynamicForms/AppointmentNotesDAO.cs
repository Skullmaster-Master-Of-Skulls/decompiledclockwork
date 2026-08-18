using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000DA RID: 218
	public class AppointmentNotesDAO : IAppointmentNotesDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00038F78 File Offset: 0x00037178
		public AppointmentNotesDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public AppointmentNotesDAO()
		{
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00038F8A File Offset: 0x0003718A
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00038F92 File Offset: 0x00037192
		public OperationContext OpContext { get; set; }

		// Token: 0x06000603 RID: 1539 RVA: 0x00038F9C File Offset: 0x0003719C
		public NotesAppointment LoadNotesAppointmentByAppointmentId(int appointmentId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, appointmentId)
			};
			NotesAppointment result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  app.appointmentid,app.startDate,app.endDate,app.dateAdded,app.cancelled,app.apptypeid,\r\n        app.ishidden,app.islocked,app.personid AS whobookedpersonid,app.[subject],\r\n        att.attendeeid,att.personid,att.noshow,att.misccode,p.firstname,p.middlename,p.lastname,p.student_no,MIN(pg.groupid) AS groupid,\r\n        acr.cancelreasontext,am.memotext,am.isencrypted,[at].[description] AS apptypedescription,[at].appointmentTypeGroupID,atg.[description] AS apptypegrouptitle,\r\n        ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM    appointments app LEFT JOIN attendees att ON att.AppointmentID=app.appointmentid \r\n        LEFT JOIN people p ON p.personid=att.personid \r\n        LEFT JOIN appointmenttypes [at] ON [at].apptypeid=app.apptypeid \r\n        LEFT JOIN appointmenttypegroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID\r\n        LEFT JOIN appointmentshowtimeas ast ON ast.extraiconid=app.appcode \r\n        LEFT JOIN appointmentmemos am ON am.AppointmentID=app.AppointmentID\r\n        LEFT JOIN appointmentcancelledreason acr ON acr.appointmentid=att.appointmentid\r\n\t\tLEFT JOIN peoplegroups pg ON pg.personid=att.personid AND pg.groupid<10\r\nWHERE   app.appointmentid=@appid\r\nGROUP BY app.appointmentid,app.startDate,app.endDate,app.dateAdded,app.cancelled,app.apptypeid,\r\n        app.ishidden,app.islocked,app.personid,app.[subject],\r\n        att.attendeeid,att.personid,att.noshow,att.misccode,p.firstname,p.middlename,p.lastname,p.student_no,\r\n        acr.cancelreasontext,am.memotext,am.isencrypted,[at].[description],[at].appointmentTypeGroupID,atg.[description],\r\n        ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					NotesAppointment notesAppointmentFromRecord = this.GetNotesAppointmentFromRecord(dataReader, batchDecryptor);
					bool flag2 = notesAppointmentFromRecord == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						List<Attendee> list = new List<Attendee>();
						bool flag4;
						do
						{
							Attendee attendeeFromRecord = AppointmentAttendeeDAO.GetAttendeeFromRecord(dataReader, this.OpContext, "", batchDecryptor);
							bool flag3 = attendeeFromRecord != null;
							if (flag3)
							{
								list.Add(attendeeFromRecord);
							}
							flag4 = !dataReader.Read();
						}
						while (!flag4);
						Attendee attendee = list.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Students);
						bool flag5 = attendee != null;
						if (flag5)
						{
							notesAppointmentFromRecord.IsPrimaryStudentNoShow = attendee.IsNoShow;
							notesAppointmentFromRecord.PrimaryStudent = attendee.Person;
						}
						notesAppointmentFromRecord.Attendees = list;
						result = notesAppointmentFromRecord;
					}
				}
			}
			return result;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000390F8 File Offset: 0x000372F8
		public IList<NotesAppointment> LoadNotesAppointmentsForStudentNoAttendeesNoHasNotes(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			bool flag = appTypeIds != null && appTypeIds.Count < 1;
			if (flag)
			{
				appTypeIds = null;
			}
			DbParameter[] array = new DbParameter[4];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, primaryStudentPersonId);
			int num = 1;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@startdate";
			DbType pType = DbType.DateTime;
			DateTime? dateTime = (dateRange != null) ? new DateTime?(dateRange.Start) : null;
			array[num] = databaseLayer2.GetParameter(pName, pType, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
			int num2 = 2;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@enddate";
			DbType pType2 = DbType.DateTime;
			dateTime = ((dateRange != null) ? new DateTime?(dateRange.End) : null);
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, (dateTime != null) ? dateTime.GetValueOrDefault() : DBNull.Value);
			int num3 = 3;
			DatabaseLayer databaseLayer4 = databaseLayer;
			string pName3 = "@apptypeids";
			DbType pType3 = DbType.String;
			object value;
			if (appTypeIds != null)
			{
				value = string.Join(",", (from g in appTypeIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = DBNull.Value;
			}
			array[num3] = databaseLayer4.GetParameter(pName3, pType3, value);
			DbParameter[] parameters = array;
			IList<NotesAppointment> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @sd datetime\r\nDECLARE @ed datetime\r\nIF NOT @startdate IS NULL\r\nBEGIN\r\n    SET @sd = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\n    SET @ed = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\nEND\r\n\r\nSELECT orderid AS apptypeid INTO #tapptypeids FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nSELECT  app.appointmentid,app.startDate,app.endDate,app.dateAdded,app.cancelled,app.apptypeid,\r\n        app.ishidden,app.islocked,app.personid AS whobookedpersonid,app.[subject],\r\n        att.personid,att.noshow,att.misccode,p.firstname,p.middlename,p.lastname,p.student_no,\r\n        acr.cancelreasontext,am.memotext,am.isencrypted,at.[description] AS apptypedescription,at.appointmentTypeGroupID,atg.[description] AS apptypegrouptitle,\r\n        ast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour\r\nFROM    attendees att LEFT JOIN appointments app ON app.AppointmentID=att.appointmentid \r\n        LEFT JOIN people p ON p.personid=att.personid \r\n        LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n        LEFT JOIN appointmenttypegroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID\r\n        LEFT JOIN appointmentshowtimeas ast ON ast.extraiconid=app.appcode \r\n        LEFT JOIN appointmentmemos am ON am.AppointmentID=app.AppointmentID\r\n        LEFT JOIN appointmentcancelledreason acr ON acr.appointmentid=att.appointmentid\r\nWHERE   att.personid=@pid \r\n        AND (@sd IS NULL OR (app.startDate>=@sd AND app.startDate<@ed))\r\n        AND (@apptypeids IS NULL OR app.apptypeid IN (SELECT apptypeid FROM #tapptypeids))\r\nORDER BY app.startDate DESC,att.appointmentid\r\n\r\nDROP TABLE #tapptypeids", CommandOverrideSettings.CommandOverrideSettingsTimeout180, parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<NotesAppointment> list = new List<NotesAppointment>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					int num4 = 0;
					while (dataReader.Read())
					{
						NotesAppointment notesAppointmentFromRecord = this.GetNotesAppointmentFromRecord(dataReader, batchDecryptor);
						bool flag3 = notesAppointmentFromRecord == null || notesAppointmentFromRecord.AppointmentId < 1 || notesAppointmentFromRecord.AppointmentId == num4;
						if (!flag3)
						{
							num4 = notesAppointmentFromRecord.AppointmentId;
							list.Add(notesAppointmentFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000392DC File Offset: 0x000374DC
		private NotesAppointment GetNotesAppointmentFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["appointmentid"] is DBNull) ? 0 : ((int)record["appointmentid"]);
			bool flag = num < 1;
			NotesAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new NotesAppointment
				{
					AppointmentId = num,
					StartDateTime = (DateTime)record["startdate"],
					EndDateTime = (DateTime)record["enddate"],
					AppointmentType = AppointmentTypeDAO.GetAppTypeFromReader("", record),
					DateBooked = (DateTime)record["dateadded"],
					Subtitle = ((record["subject"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["subject"])),
					ShowTimeAs = AppointmentShowTimeAsDAO.GetShowTimeAsFromRecord(record),
					PrimaryStudent = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor),
					CancelReason = ((record["cancelreasontext"] is DBNull) ? "" : ((string)record["cancelreasontext"])),
					IsPrimaryStudentNoShow = (!(record["noshow"] is DBNull) && Convert.ToBoolean(record["noshow"])),
					MemoText = BaseAppointmentDAO.GetMemo(record, batchDecryptor),
					IsCancelled = (!(record["cancelled"] is DBNull) && Convert.ToBoolean(record["cancelled"])),
					IsPrivate = (!(record["ishidden"] is DBNull) && Convert.ToBoolean(record["ishidden"])),
					IsLocked = (!(record["islocked"] is DBNull) && Convert.ToBoolean(record["islocked"])),
					WhoBookedPersonId = ((record["whobookedpersonid"] is DBNull) ? 0 : ((int)record["whobookedpersonid"]))
				};
			}
			return result;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00039500 File Offset: 0x00037700
		public IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, IList<int> AllowedAppTypeIds, params int[] ScreenNums)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[5];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, PersonId);
			int num = 1;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@screennums";
			DbType pType = DbType.String;
			object value;
			if (ScreenNums != null)
			{
				value = string.Join(",", new List<int>(ScreenNums).ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			array[2] = databaseLayer.GetParameter("@startdate", DbType.DateTime, (DateRange == null) ? DBNull.Value : DateRange.Start.Date);
			array[3] = databaseLayer.GetParameter("@enddate", DbType.DateTime, (DateRange == null) ? DBNull.Value : DateRange.End.AddDays(1.0));
			int num2 = 4;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@apptypeids";
			DbType pType2 = DbType.String;
			object value2;
			if (AllowedAppTypeIds != null)
			{
				value2 = string.Join(",", AllowedAppTypeIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value2 = "";
			}
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, value2);
			DbParameter[] parameters = array;
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    DISTINCT pad.appointmentid\r\nFROM        perappdata2 pad LEFT JOIN appointments a ON a.appointmentid=pad.appointmentid\r\nWHERE       pad.personid=@pid\r\n            AND (@startdate IS NULL OR a.startdate BETWEEN @startdate AND @enddate )\r\n            AND a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,','))\r\n            AND pad.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')))\r\nORDER BY pad.appointmentid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num3 = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
						bool flag2 = num3 > 0 && !list.Contains(num3);
						if (flag2)
						{
							list.Add(num3);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
