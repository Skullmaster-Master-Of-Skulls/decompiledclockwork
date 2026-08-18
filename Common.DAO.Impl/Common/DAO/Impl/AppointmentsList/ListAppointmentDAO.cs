using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsList;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AppointmentsList
{
	// Token: 0x0200015C RID: 348
	public class ListAppointmentDAO : IListAppointmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0006A504 File Offset: 0x00068704
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0006A50C File Offset: 0x0006870C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000A1E RID: 2590 RVA: 0x0006A515 File Offset: 0x00068715
		public ListAppointmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0006A546 File Offset: 0x00068746
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0006A54E File Offset: 0x0006874E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A21 RID: 2593 RVA: 0x0006A558 File Offset: 0x00068758
		private Availability2Marker GetAvailability2MarkerFromRecord(IDataReader record)
		{
			bool flag = record == null || record["availability2markerid"] is DBNull;
			Availability2Marker result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new Availability2Marker
				{
					Availability2MarkerId = (int)record["availability2markerid"],
					MarkerColourArgB = ((record["markercolourargb"] is DBNull) ? null : new int?((int)record["markercolourargb"])),
					MarkerText = record["markertext"].ToString().Trim(),
					OrderNum = (int)record["markerordernum"]
				};
			}
			return result;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0006A618 File Offset: 0x00068818
		private ClosedDay GetClosedDayFromReader(IDataReader reader)
		{
			int availability2ItemsClosedDaysId = (int)reader["Availability2ItemsClosedDaysId"];
			PersonBase personBase;
			if (reader["personid"] == DBNull.Value)
			{
				personBase = null;
			}
			else
			{
				PersonBase personBase2 = new PersonBase();
				personBase2.PersonId = (int)reader["personid"];
				personBase2.FirstName = this.DatabaseManager.Encryption.Decrypt((byte[])reader["firstname"]);
				personBase2.LastName = this.DatabaseManager.Encryption.Decrypt((byte[])reader["lastname"]);
				personBase2.CoreGroup = eCoreGroup.Staff;
				personBase2.MiddleName = "";
				personBase = personBase2;
				personBase2.Student_no = "";
			}
			PersonBase staff = personBase;
			string note = (reader["note"] == DBNull.Value) ? "" : ((string)reader["note"]);
			DateTime dateClosed = (DateTime)reader["dateclosed"];
			return new ClosedDay
			{
				Availability2ItemsClosedDaysId = availability2ItemsClosedDaysId,
				Staff = staff,
				DateClosed = dateClosed,
				Note = note
			};
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0006A740 File Offset: 0x00068940
		private T GetAvailability2ItemFromReader<T>(IDataReader reader) where T : Availability2Item
		{
			int availability2ItemId = (int)reader["Availability2ItemId"];
			DateTime startDateTime = (DateTime)reader["startdatetime"];
			DateTime endDateTime = (DateTime)reader["enddatetime"];
			bool isActive = reader["isactive"] != DBNull.Value && Convert.ToBoolean(reader["isactive"]);
			bool isAvailable = reader["isavailable"] != DBNull.Value && Convert.ToBoolean(reader["isavailable"]);
			Availability2Note availabilityNote = new Availability2Note
			{
				ColourArgB = ((reader["colourargb"] != DBNull.Value) ? new int?((int)reader["colourargb"]) : null),
				Text = ((reader["note"] != DBNull.Value) ? ((string)reader["note"]) : "")
			};
			int personId = (int)reader["personid"];
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			t.Availability2ItemId = availability2ItemId;
			t.StartDateTime = startDateTime;
			t.EndDateTime = endDateTime;
			t.IsActive = isActive;
			t.IsAvailable = isAvailable;
			t.AvailabilityNote = availabilityNote;
			t.PersonId = personId;
			return t;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0006A8D4 File Offset: 0x00068AD4
		private bool ReaderContainsColumn(IDataReader reader, string colName)
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

		// Token: 0x06000A25 RID: 2597 RVA: 0x0006A914 File Offset: 0x00068B14
		private List<Availability2Item> GetAvailability2ListFromReader(IDataReader reader)
		{
			bool flag = reader != null;
			List<Availability2Item> result;
			if (flag)
			{
				List<Availability2Item> list = new List<Availability2Item>();
				while (reader.Read())
				{
					Availability2Item availability2ItemFromReader = this.GetAvailability2ItemFromReader<Availability2Item>(reader);
					bool flag2 = availability2ItemFromReader != null;
					if (flag2)
					{
						list.Add(availability2ItemFromReader);
					}
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0006A968 File Offset: 0x00068B68
		public IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment)
		{
			return this.LoadAppointments(PersonIds, StartDate, NumDays, LoadIsStudentsFirstAppointment, false);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0006A988 File Offset: 0x00068B88
		public IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			IList<ListAppointment> list = baseAppointmentDAO.LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<ListAppointment>(StartDate, StartDate.Date.AddDays((double)NumDays).AddMinutes(-1.0), PersonIds);
			if (HideCancelledAppointments)
			{
				list = (from g in list
				where !g.IsCancelled
				select g).ToList<ListAppointment>();
			}
			bool flag = !LoadIsStudentsFirstAppointment;
			IList<ListAppointment> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				foreach (ListAppointment listAppointment in list)
				{
					List<Attendee> list2 = listAppointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup == eCoreGroup.Students);
					bool flag2 = list2 == null;
					if (!flag2)
					{
						foreach (Attendee attendee in list2)
						{
							DbParameter[] array = new DbParameter[]
							{
								this.DatabaseManager.GetOutputParameter("@minappid", DbType.Int32, 0),
								this.DatabaseManager.GetParameter("@pid", DbType.Int32, attendee.Person.PersonId)
							};
							this.DatabaseManager.ExecuteNonQuery("SET @minappid=(SELECT MIN(appointmentid) FROM apps WHERE personid=@pid AND cancelled=0 AND noshow=0)", array);
							bool flag3 = array[0].Value is DBNull;
							if (!flag3)
							{
								int num = (int)array[0].Value;
								listAppointment.IsStudentsFirstApp = (num == listAppointment.AppointmentId);
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0006AB88 File Offset: 0x00068D88
		public int CreateListAppointment(ListAppointment Appointment)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			int num = baseAppointmentDAO.CreateBaseExtendedAppointment(Appointment, null);
			Appointment.AppointmentId = num;
			IList<Availability2Item> list = this.LoadOverlappingAvailabilitiesWithAppointment(Appointment);
			foreach (Availability2Item availability2Item in list)
			{
				this.MarkAvailabilityWithAppointment(availability2Item.Availability2ItemId, num);
			}
			return num;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0006AC08 File Offset: 0x00068E08
		public void UpdateListAppointment(ListAppointment Appointment)
		{
			ListAppointment listAppointment = this.LoadAppointmentById(Appointment.AppointmentId, false);
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateBaseExtendedAppointment(Appointment, null);
			bool flag = listAppointment != null;
			if (flag)
			{
				int num = (listAppointment.Staff == null) ? 0 : listAppointment.Staff.PersonId;
				int num2 = (Appointment.Staff == null) ? 0 : Appointment.Staff.PersonId;
				bool flag2 = listAppointment.StartDateTime != Appointment.StartDateTime || listAppointment.EndDateTime != Appointment.EndDateTime || num != num2;
				if (flag2)
				{
					IList<Availability2Item> list = this.LoadOverlappingAvailabilitiesWithAppointment(listAppointment);
					foreach (Availability2Item availability2Item in list)
					{
						this.MarkAvailabilityWithAppointment(availability2Item.Availability2ItemId, 0);
					}
					IList<Availability2Item> list2 = this.LoadOverlappingAvailabilitiesWithAppointment(Appointment);
					foreach (Availability2Item availability2Item2 in list2)
					{
						this.MarkAvailabilityWithAppointment(availability2Item2.Availability2ItemId, Appointment.AppointmentId);
					}
				}
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0006AD5C File Offset: 0x00068F5C
		public int CreateAvailability(int PersonId, DateTime StartDateTime, DateTime EndDateTime, int ColourArgB, string Note)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@sdt", DbType.DateTime, StartDateTime),
				this.DatabaseManager.GetParameter("@edt", DbType.DateTime, EndDateTime),
				this.DatabaseManager.GetParameter("@colourargb", DbType.Int32, ColourArgB),
				this.DatabaseManager.GetParameter("@note", DbType.String, Note)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("INSERT INTO Availability2Items (personid,startdatetime,enddatetime,appointmentid,colourargb,note,isactive,isavailable) \r\nSELECT @pid,@startdatetime,@enddatetime,NULL,@colourargb,@note,1,1\r\nWHERE NOT EXISTS(SELECT availability2itemid FROM availability2items WHERE personid=@pid AND NOT ( enddatetime <= @startdatetime OR startdatetime >= @enddatetime));\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS availability2itemid", parameters);
			return (int)dataTable.Rows[0][0];
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0006AE20 File Offset: 0x00069020
		public IList<Availability2Item> LoadOverlappingAvailabilitiesWithAppointment(ListAppointment app)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, app.StartDateTime);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, app.EndDateTime);
			array[2] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", app.Attendees.ConvertAll<string>((Attendee g) => g.Person.PersonId.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IList<Availability2Item> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,a.isactive,a.isavailable,apps.cancelled,apps.noshow,apps.misccode\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN apps ON apps.appointmentid=a.appointmentid AND apps.personid=a.personid\r\nWHERE\ta.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n        AND NOT ( enddatetime <= @startdate OR startdatetime >= @enddate)\r\n        AND a.isactive=1\r\n        AND a.isavailable=1\r\nORDER BY a.startdatetime", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetAvailability2ListFromReader(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0006AF08 File Offset: 0x00069108
		public List<Availability2Item> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@startdatetime", DbType.DateTime, StartDateTime),
				this.DatabaseManager.GetParameter("@enddatetime", DbType.DateTime, EndDateTime)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,apps.startDate,apps.endDate,apps.AppTypeID\r\n\t\t,apps.PersonID AS studentpersonid,pstud.lastName AS studentlastname,pstud.firstName AS studentfirstname,pstud.middleName AS studentmiddlename,pstud.student_no AS studentstudent_no \r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable,apps.cancelled,apps.noshow,apps.misccode\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note,am.memotext AS memo\r\n        ,at.description\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\n\t\tLEFT JOIN apps ON apps.AppointmentID=a.appointmentid AND apps.personid IN (SELECT personid FROM PeopleGroups WHERE GroupID=1)\r\n\t\tLEFT JOIN AppointmentTypes at ON at.AppTypeID=apps.AppTypeID \r\n\t\tLEFT JOIN people pstud ON pstud.PersonID=apps.PersonID \r\n        LEFT JOIN appointmentmemos am ON am.appointmentid=apps.appointmentid\r\nWHERE\ta.personid=@pid AND NOT ( a.enddatetime <= @startdatetime OR a.startdatetime >= @enddatetime)\r\n        AND a.isactive=1\r\n        AND a.isavailable=1\r\n        AND (apps.appointmentid IS NULL OR apps.cancelled=0)\r\nORDER BY a.startdatetime", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetAvailability2ListFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0006AFBC File Offset: 0x000691BC
		public void MarkAvailabilityWithAppointment(int availability2itemid, int appointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appointmentid", DbType.Int32, (appointmentId > 0) ? appointmentId : DBNull.Value),
				this.DatabaseManager.GetParameter("@availability2itemid", DbType.Int32, availability2itemid)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE availability2items SET appointmentid=@appointmentid WHERE availability2itemid=@availability2itemid", parameters);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0006B024 File Offset: 0x00069224
		public List<Availability2Item> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", PersonIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
			array[1] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDateTime.Date);
			array[2] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDateTime.Date.AddDays(1.0).AddMinutes(-1.0));
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\n\t\tLEFT JOIN Availability2ItemsClosedDays ac ON ac.personid=a.personid AND ac.dateclosed=DATEADD(dd,DATEDIFF(dd,0,a.startdatetime),0)\r\nWHERE\ta.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n\t\tAND a.startdatetime >= @startdate AND a.startdatetime < @enddate\r\n        AND a.appointmentid IS NULL\r\n\t\tAND a.isactive=1\r\n        AND ac.Availability2ItemsClosedDaysId IS NULL\r\nORDER BY a.startdatetime,a.personid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetAvailability2ListFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0006B130 File Offset: 0x00069330
		public IList<ClosedDay> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", PersonIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			array[1] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date);
			array[2] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date);
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @sd datetime, @ed datetime\r\nSET @sd = DATEADD(dd, DATEDIFF(dd,0,@startdate), 0)\r\nSET @ed = DATEADD(dd, DATEDIFF(dd,0,@enddate), 0)\r\nSET @ed = DATEADD( day,1,@ed)\r\n\r\nSELECT    c.Availability2ItemsClosedDaysId,c.personid,c.dateclosed,c.note\r\n            ,p.firstname,p.lastname\r\nFROM        Availability2ItemsClosedDays c LEFT JOIN people p ON p.personid=c.personid\r\nWHERE       c.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n            AND c.dateclosed>=@sd AND c.dateclosed<@ed\r\nORDER BY    c.personid,c.dateclosed", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<ClosedDay> list = new List<ClosedDay>();
					while (dataReader.Read())
					{
						ClosedDay closedDayFromReader = this.GetClosedDayFromReader(dataReader);
						bool flag2 = closedDayFromReader != null;
						if (flag2)
						{
							list.Add(closedDayFromReader);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0006B250 File Offset: 0x00069450
		public void CreateClosedDay(IList<ClosedDay> ClosedDays)
		{
			foreach (ClosedDay closedDay in ClosedDays)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, closedDay.Staff.PersonId),
					this.DatabaseManager.GetParameter("@date", DbType.DateTime, closedDay.DateClosed.Date),
					this.DatabaseManager.GetParameter("@note", DbType.String, closedDay.Note ?? "")
				};
				DataTable dataTable = this.DatabaseManager.ExecuteQuery("IF EXISTS(SELECT Availability2ItemsClosedDaysId FROM Availability2ItemsClosedDays WHERE personid=@pid AND dateclosed=@date)\r\nBEGIN\r\n    UPDATE Availability2ItemsClosedDays SET note=@note WHERE personid=@pid AND dateclosed=@date\r\n    SELECT Availability2ItemsClosedDaysId FROM Availability2ItemsClosedDays WHERE personid=@pid AND dateclosed=@date\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO Availability2ItemsClosedDays(personid,dateclosed,note) VALUES (@pid,@date,@note);\r\n    SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS Availability2ItemsClosedDaysId\r\nEND", parameters);
				int availability2ItemsClosedDaysId = (int)dataTable.Rows[0][0];
				closedDay.Availability2ItemsClosedDaysId = availability2ItemsClosedDaysId;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0006B348 File Offset: 0x00069548
		public void DeleteClosedDay(int PersonId, DateTime Date)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@dt", DbType.DateTime, Date.Date)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM Availability2ItemsClosedDays WHERE personid=@pid AND dateclosed=@dt", parameters);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0006B3AC File Offset: 0x000695AC
		public void CreateAvailabilities(List<Availability2Item> Availabilities)
		{
			foreach (Availability2Item availability2Item in Availabilities)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, availability2Item.PersonId),
					this.DatabaseManager.GetParameter("@startdatetime", DbType.DateTime, availability2Item.StartDateTime),
					this.DatabaseManager.GetParameter("@enddatetime", DbType.DateTime, availability2Item.EndDateTime),
					this.DatabaseManager.GetParameter("@colourargb", DbType.Int32, (availability2Item.AvailabilityNote == null) ? new int?(0) : availability2Item.AvailabilityNote.ColourArgB),
					this.DatabaseManager.GetParameter("@note", DbType.String, (availability2Item.AvailabilityNote == null) ? "" : availability2Item.AvailabilityNote.Text)
				};
				DataTable dataTable = this.DatabaseManager.ExecuteQuery("INSERT INTO Availability2Items (personid,startdatetime,enddatetime,appointmentid,colourargb,note,isactive,isavailable) \r\nSELECT @pid,@startdatetime,@enddatetime,NULL,@colourargb,@note,1,1\r\nWHERE NOT EXISTS(SELECT availability2itemid FROM availability2items WHERE personid=@pid AND NOT ( enddatetime <= @startdatetime OR startdatetime >= @enddatetime));\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS availability2itemid", parameters);
				bool flag = dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
				if (flag)
				{
					availability2Item.Availability2ItemId = (int)dataTable.Rows[0][0];
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0006B53C File Offset: 0x0006973C
		public void DeleteAvailability(List<int> AvailabilityIds)
		{
			foreach (int num in AvailabilityIds)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@id", DbType.Int32, num)
				};
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM Availability2Items WHERE availability2itemid=@id", parameters);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0006B5BC File Offset: 0x000697BC
		public void UpdateAvailability(List<Availability2Item> Availabilities)
		{
			foreach (Availability2Item availability2Item in Availabilities)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@id", DbType.Int32, availability2Item.Availability2ItemId),
					this.DatabaseManager.GetParameter("@startdatetime", DbType.DateTime, availability2Item.StartDateTime),
					this.DatabaseManager.GetParameter("@enddatetime", DbType.DateTime, availability2Item.EndDateTime),
					this.DatabaseManager.GetParameter("@colourargb", DbType.Int32, (availability2Item.AvailabilityNote == null) ? new int?(0) : availability2Item.AvailabilityNote.ColourArgB),
					this.DatabaseManager.GetParameter("@note", DbType.String, (availability2Item.AvailabilityNote == null) ? "" : availability2Item.AvailabilityNote.Text)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE availability2items SET colourargb=@colourargb,note=@note,startdatetime=@startdatetime,enddatetime=@enddatetime WHERE availability2itemid=@id", parameters);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0006B6EC File Offset: 0x000698EC
		public IList<Availability2Item> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@sd", DbType.DateTime, StartDate.Date);
			array[1] = this.DatabaseManager.GetParameter("@ed", DbType.DateTime, StartDate.Date.AddDays((double)NumDays).AddSeconds(-1.0));
			array[2] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", PersonIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\ta.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n\t\tAND a.startdatetime >= @sd AND a.startdatetime <= @ed\r\n\t\tAND a.isactive=1\r\nORDER BY a.personid,a.startdatetime", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetAvailability2ListFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0006B7F8 File Offset: 0x000699F8
		public ListAppointment LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			ListAppointment listAppointment = baseAppointmentDAO.LoadBaseExtendedAppointmentById<ListAppointment>(AppointmentId);
			bool flag = listAppointment == null;
			ListAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				if (LoadIsStudentsFirstAppointment)
				{
					List<Attendee> list = listAppointment.Attendees.FindAll((Attendee att) => att.Person.CoreGroup == eCoreGroup.Students);
					bool flag2 = list != null;
					if (flag2)
					{
						foreach (Attendee attendee in list)
						{
							DbParameter[] array = new DbParameter[]
							{
								this.DatabaseManager.GetOutputParameter("@minappid", DbType.Int32, 0),
								this.DatabaseManager.GetParameter("@pid", DbType.Int32, attendee.Person.PersonId)
							};
							this.DatabaseManager.ExecuteNonQuery("SET @minappid=(SELECT MIN(appointmentid) FROM apps WHERE personid=@pid AND cancelled=0 AND noshow=0)", array);
							bool flag3 = !(array[0].Value is DBNull);
							if (flag3)
							{
								int num = (int)array[0].Value;
								listAppointment.IsStudentsFirstApp = (num == listAppointment.AppointmentId);
							}
						}
					}
				}
				result = listAppointment;
			}
			return result;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0006B954 File Offset: 0x00069B54
		public Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sdt", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@edt", DbType.DateTime, StartDate.Date.AddDays((double)NumDays)),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			Dictionary<DateTime, eAvailabilityCode> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT \r\n\tCONVERT(DATETIME, FLOOR(CONVERT(FLOAT, a.startdatetime))) AS dt,\r\n\tCASE WHEN a.appointmentid IS NULL OR a.appointmentid IN (SELECT appointmentid FROM appointments WHERE cancelled=1)\r\n\t\tTHEN CAST(1 AS bit) ELSE CAST(0 AS bit) END As HasAvailableSlot,\r\n\t--CASE WHEN a.appointmentid IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END As HasBookedSlot,\r\n\tCASE WHEN NOT c.availability2itemscloseddaysid IS NULL THEN CAST(1 as bit) ELSE CAST(0 as bit) END AS IsClosed\r\nFROM availability2items a LEFT JOIN availability2itemscloseddays c \r\n\t\tON c.personid=a.personid AND CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, c.dateclosed)))=CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, a.startdatetime)))\r\nWHERE a.startdatetime>=@sdt AND a.startdatetime<@edt AND a.personid=@pid\r\nORDER BY dt,HasAvailableSlot", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<DateTime, eAvailabilityCode> dictionary = new Dictionary<DateTime, eAvailabilityCode>();
					while (dataReader.Read())
					{
						DateTime key = (DateTime)dataReader["dt"];
						bool flag2 = Convert.ToBoolean(dataReader["hasavailableslot"]);
						bool flag3 = !dictionary.ContainsKey(key);
						if (flag3)
						{
							bool flag4 = dataReader["isclosed"] != DBNull.Value && Convert.ToBoolean(dataReader["isclosed"]);
							bool flag5 = flag4;
							eAvailabilityCode value;
							if (flag5)
							{
								value = eAvailabilityCode.ClosedDay;
							}
							else
							{
								bool flag6 = !flag2;
								if (flag6)
								{
									value = eAvailabilityCode.Full;
								}
								else
								{
									value = eAvailabilityCode.AtLeastOneAvailable;
								}
							}
							dictionary.Add(key, value);
						}
						else
						{
							eAvailabilityCode eAvailabilityCode = dictionary[key];
							bool flag7 = eAvailabilityCode != eAvailabilityCode.ClosedDay;
							if (flag7)
							{
								bool flag8 = flag2 && eAvailabilityCode != eAvailabilityCode.AtLeastOneAvailable;
								if (flag8)
								{
									dictionary.Remove(key);
									dictionary.Add(key, eAvailabilityCode.AtLeastOneAvailable);
								}
							}
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0006BB10 File Offset: 0x00069D10
		public Availability2Item LoadAvailabilityById(int Availability2ItemId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, Availability2ItemId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\ta.Availability2ItemId=@id\r\n\t\tAND a.isactive=1\r\nORDER BY a.startdatetime", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				bool flag2 = dataReader.Read();
				if (flag2)
				{
					return this.GetAvailability2ItemFromReader<Availability2Item>(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0006BB98 File Offset: 0x00069D98
		public IList<Availability2ItemWithAppointmentId> LoadUniqueAvailabilitiesForAllPeopleWithAppointmentIds(DateTime StartDate, DateTime EndDate)
		{
			DateTime date = StartDate.Date;
			DateTime dateTime = EndDate.Date.AddDays(1.0).AddMinutes(-1.0);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			IList<Availability2ItemWithAppointmentId> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.Availability2ItemId,a.personid,a.appointmentid\r\n\t\t,p.firstName,p.lastName\r\n        ,a.isactive,a.isavailable\r\n        ,a.startdatetime,a.enddatetime,a.colourargb,a.note\r\nFROM\tAvailability2Items a LEFT JOIN people p ON p.PersonID=a.PersonID \r\nWHERE\tNOT ( a.enddatetime <= @startdate OR a.startdatetime >= @enddate)\r\n\t\tAND a.isactive=1\r\nORDER BY a.personid,a.startdatetime", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Availability2ItemWithAppointmentId> list = new List<Availability2ItemWithAppointmentId>();
					while (dataReader.Read())
					{
						Availability2ItemWithAppointmentId availability2ItemFromReader = this.GetAvailability2ItemFromReader<Availability2ItemWithAppointmentId>(dataReader);
						bool flag2 = availability2ItemFromReader != null;
						if (flag2)
						{
							availability2ItemFromReader.AppointmentId = ((dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]));
							list.Add(availability2ItemFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0006BCC4 File Offset: 0x00069EC4
		public IList<ListAppointment> LoadAllAppointments(DateTime StartDate, int NumDays, bool ShowCancelled = false)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			return baseAppointmentDAO.LoadBaseExtendedAppointmentsByDateRange<ListAppointment>(StartDate, StartDate.Date.AddDays((double)NumDays).AddMinutes(-1.0), ShowCancelled);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0006BD10 File Offset: 0x00069F10
		public IList<Availability2Marker> LoadAvailability2Markers()
		{
			IList<Availability2Marker> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT availability2markerid,markertext,markercolourargb,markerordernum FROM availability2marker ORDER BY markerordernum,markertext"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Availability2Marker> list = new List<Availability2Marker>();
					while (dataReader.Read())
					{
						Availability2Marker availability2MarkerFromRecord = this.GetAvailability2MarkerFromRecord(dataReader);
						bool flag2 = availability2MarkerFromRecord != null;
						if (flag2)
						{
							list.Add(availability2MarkerFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0006BD90 File Offset: 0x00069F90
		public int CreateAvailability2Marker(Availability2Marker Marker)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("availability2markerid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@markertext", DbType.String, Marker.MarkerText ?? ""),
				this.DatabaseManager.GetParameter("@markercolourargb", DbType.Int32, (Marker.MarkerColourArgB != null) ? Marker.MarkerColourArgB.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@markerordernum", DbType.Int32, Marker.OrderNum)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO availability2marker (markertext,markercolourargb,markerordernum) VALUES (@markertext,@markercolourargb,@markerordernum);\r\nSET @availability2markerid=(SELECT CAST( SCOPE_IDENTITY() AS int))", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0006BE74 File Offset: 0x0006A074
		public void DeleteAvailability2Marker(int Availability2MarkerId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("availability2markerid", DbType.Int32, Availability2MarkerId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM availability2marker WHERE availability2markerid=@availability2markerid", parameters);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0006BEB8 File Offset: 0x0006A0B8
		public void UpdateAvailability2Marker(Availability2Marker Marker)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("availability2markerid", DbType.Int32, Marker.Availability2MarkerId),
				this.DatabaseManager.GetParameter("@markertext", DbType.String, Marker.MarkerText ?? ""),
				this.DatabaseManager.GetParameter("@markercolourargb", DbType.Int32, (Marker.MarkerColourArgB != null) ? Marker.MarkerColourArgB.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@markerordernum", DbType.Int32, Marker.OrderNum)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE availability2marker SET markertext=@markertext,markercolourargb=@markercolourargb,markerordernum=@markerordernum WHERE availability2markerid=@availability2markerid", parameters);
		}
	}
}
