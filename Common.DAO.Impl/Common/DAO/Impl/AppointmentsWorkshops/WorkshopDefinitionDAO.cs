using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsWorkshops;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AppointmentsWorkshops
{
	// Token: 0x02000141 RID: 321
	public class WorkshopDefinitionDAO : IWorkshopDefinitionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0005FDB5 File Offset: 0x0005DFB5
		public WorkshopDefinitionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0005FDE5 File Offset: 0x0005DFE5
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0005FDED File Offset: 0x0005DFED
		public OperationContext OpContext { get; set; }

		// Token: 0x06000947 RID: 2375 RVA: 0x0005FDF8 File Offset: 0x0005DFF8
		internal static WorkshopDefinition GetWorkshopDefinitionFromReader(IDataReader reader, OperationContext opContext)
		{
			bool flag = reader == null || reader["workshopid"] == DBNull.Value;
			WorkshopDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<PersonBase> list = new List<PersonBase>();
				for (int i = 1; i <= 3; i++)
				{
					string text = "p" + i.ToString();
					string name = text + "personid";
					int num = (reader[name] == DBNull.Value) ? 0 : ((int)reader[name]);
					bool flag2 = num > 0;
					if (flag2)
					{
						PersonBase user = PeopleDAO.GetPersonFromReader(text, reader, opContext, null);
						bool flag3 = user != null && list.Find((PersonBase f) => f.PersonId == user.PersonId) == null;
						if (flag3)
						{
							list.Add(user);
						}
					}
				}
				result = new WorkshopDefinition
				{
					WorkshopId = (int)reader["workshopid"],
					WorkshopTitle = reader["workshoptitle"].ToString(),
					WorkshopDescription = reader["workshopdescription"].ToString(),
					AppTypeParent = AppointmentTypeDAO.GetAppTypeFromReader("", reader),
					Fee = (double)reader["workshopfee"],
					MaxAttendeeDefaultCount = (int)reader["maxattendees"],
					WorkshopLocation = reader["location"].ToString(),
					WorkshopNotes = reader["note"].ToString(),
					Facilitators = list
				};
			}
			return result;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0005FFB0 File Offset: 0x0005E1B0
		internal static WorkshopDefinition GetWorkshopDefinitionFromReader(IDataReader reader, OperationContext opContext, IBatchDecryptor batchDecryptor)
		{
			bool flag = reader == null || reader["workshopid"] == DBNull.Value;
			WorkshopDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<PersonBase> list = new List<PersonBase>();
				for (int i = 1; i <= 3; i++)
				{
					string text = "p" + i.ToString();
					string name = text + "personid";
					int num = (reader[name] == DBNull.Value) ? 0 : ((int)reader[name]);
					bool flag2 = num > 0;
					if (flag2)
					{
						PersonBase user = PeopleDAO.GetPersonFromReader(text, reader, opContext, batchDecryptor);
						bool flag3 = user != null && list.Find((PersonBase f) => f.PersonId == user.PersonId) == null;
						if (flag3)
						{
							list.Add(user);
						}
					}
				}
				result = new WorkshopDefinition
				{
					WorkshopId = (int)reader["workshopid"],
					WorkshopTitle = reader["workshoptitle"].ToString(),
					WorkshopDescription = reader["workshopdescription"].ToString(),
					AppTypeParent = AppointmentTypeDAO.GetAppTypeFromReader("", reader),
					Fee = (double)reader["workshopfee"],
					MaxAttendeeDefaultCount = (int)reader["maxattendees"],
					WorkshopLocation = reader["location"].ToString(),
					WorkshopNotes = reader["note"].ToString(),
					Facilitators = list
				};
			}
			return result;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00060168 File Offset: 0x0005E368
		public IList<WorkshopDefinition> LoadWorkshopDefinitions(IList<int> AllowedAppointmentTypes)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@apptypeids", DbType.String, string.Join(",", AllowedAppointmentTypes.ToList<int>().ConvertAll<string>((int aat) => aat.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IList<WorkshopDefinition> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tw.workshopid,w.apptypeid,w.workshopTitle,w.workshopDescription,w.MaxAttendees,w.WorkshopFee,\r\n\t\tw.partners,w.note,w.location,w.availableForOnlineBooking,w.waitingListMaxUsers,\r\n\t\tat.[description] AS apptypedescription,at.defaultcolour,at.iscourse,at.isworkshop,at.appointmentTypeGroupID,\r\n        atg.title AS apptypegrouptitle,\r\n\t\tw.personID AS p1personid,p.firstName AS p1firstname,p.lastName AS p1lastname,p.student_no AS p1student_no,\r\n        pgp.groupid AS p1groupid,\r\n\t\tw.personID2 AS p2personid,p2.firstName AS p2firstname,p2.lastName AS p2lastname,p2.student_no AS p2student_no,\r\n        pgp2.groupid AS p2groupid,\r\n\t\tw.personID3 AS p3personid,p3.firstName AS p3firstname,p3.lastName AS p3lastname,p3.student_no AS p3student_no,\r\n        pgp3.groupid AS p3groupid\r\nFROM\tWorkshops w LEFT JOIN AppointmentTypes at ON at.AppTypeID=w.AppTypeID \r\n        LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\n\t\tLEFT JOIN people p ON p.PersonID=w.personID\r\n        LEFT JOIN peoplegroups pgp ON pgp.personid=p.personid AND pgp.groupid<10\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=w.personID2\r\n        LEFT JOIN peoplegroups pgp2 ON pgp2.personid=p2.personid AND pgp2.groupid<10\r\n\t\tLEFT JOIN people p3 ON p3.PersonID=w.personID3\r\n        LEFT JOIN peoplegroups pgp3 ON pgp3.personid=p3.personid AND pgp3.groupid<10\r\nWHERE   w.isactive=1 AND w.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,','))\r\nORDER BY at.[description],w.AppTypeID,w.workshopTitle,pgp.groupid,pgp2.groupid,pgp3.groupid", parameters))
			{
				List<WorkshopDefinition> list = new List<WorkshopDefinition>();
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						WorkshopDefinition workshopDefinitionFromReader = WorkshopDefinitionDAO.GetWorkshopDefinitionFromReader(dataReader, this.OpContext);
						bool flag2 = workshopDefinitionFromReader != null;
						if (flag2)
						{
							list.Add(workshopDefinitionFromReader);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00060244 File Offset: 0x0005E444
		public void DeleteWorkshopDefinition(int workshopEventId)
		{
			this.DatabaseManager.ExecuteNonQuery("if exists(select 1 from AppointmentWorkshops where WorkshopID=@workshopid)\r\nbegin\r\n\tupdate Workshops SET isactive=0 WHERE WorkshopID=@workshopid\r\nend\r\nelse\r\nbegin\r\n\tDELETE FROM Workshops WHERE WorkshopID=@workshopid\r\nend", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@workshopid", DbType.Int32, workshopEventId)
			});
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00060284 File Offset: 0x0005E484
		public int CreateWorkshopDefinition(WorkshopDefinition workshopDefinition)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@workshopid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, workshopDefinition.AppTypeParent.AppTypeId),
				this.DatabaseManager.GetParameter("@workshoptitle", DbType.String, workshopDefinition.WorkshopTitle ?? string.Empty),
				this.DatabaseManager.GetParameter("@workshopdescription", DbType.String, workshopDefinition.WorkshopDescription ?? string.Empty),
				this.DatabaseManager.GetParameter("@maxattendees", DbType.Int32, workshopDefinition.MaxAttendeeDefaultCount),
				this.DatabaseManager.GetParameter("@workshopfee", DbType.Double, workshopDefinition.Fee),
				this.DatabaseManager.GetParameter("@personid", DbType.Int32, (workshopDefinition.Facilitators != null && workshopDefinition.Facilitators.Count > 0) ? workshopDefinition.Facilitators[0].PersonId : 0),
				this.DatabaseManager.GetParameter("@personid2", DbType.Int32, (workshopDefinition.Facilitators != null && workshopDefinition.Facilitators.Count > 1) ? workshopDefinition.Facilitators[1].PersonId : 0),
				this.DatabaseManager.GetParameter("@personid3", DbType.Int32, (workshopDefinition.Facilitators != null && workshopDefinition.Facilitators.Count > 2) ? workshopDefinition.Facilitators[2].PersonId : 0),
				this.DatabaseManager.GetParameter("@note", DbType.String, workshopDefinition.WorkshopNotes),
				this.DatabaseManager.GetParameter("@location", DbType.String, workshopDefinition.WorkshopLocation)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO Workshops(AppTypeID, workshopTitle, workshopDescription, MaxAttendees, WorkshopFee, personID, personID2, personID3, note, location)\r\nVALUES (@apptypeid, @workshoptitle, @workshopdescription, @maxattendees, @workshopfee, @personid, @personid2, @personid3, @note, @location)\r\nSET @workshopid = SCOPE_IDENTITY()", array);
			bool flag = !(array[0].Value is DBNull);
			if (flag)
			{
				workshopDefinition.WorkshopId = (int)array[0].Value;
			}
			return workshopDefinition.WorkshopId;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000604A8 File Offset: 0x0005E6A8
		public void UpdateWorkshopDefinition(WorkshopDefinition workshopDefinition)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@workshopid", DbType.Int32, workshopDefinition.WorkshopId),
				this.DatabaseManager.GetParameter("@apptypeid", DbType.Int32, workshopDefinition.AppTypeParent.AppTypeId),
				this.DatabaseManager.GetParameter("@workshoptitle", DbType.String, workshopDefinition.WorkshopTitle ?? string.Empty),
				this.DatabaseManager.GetParameter("@workshopdescription", DbType.String, workshopDefinition.WorkshopDescription ?? string.Empty),
				this.DatabaseManager.GetParameter("@maxattendees", DbType.Int32, workshopDefinition.MaxAttendeeDefaultCount),
				this.DatabaseManager.GetParameter("@workshopfee", DbType.Double, workshopDefinition.Fee),
				this.DatabaseManager.GetParameter("@personid", DbType.Int32, (workshopDefinition.Facilitators != null && workshopDefinition.Facilitators.Count > 0) ? workshopDefinition.Facilitators[0].PersonId : 0),
				this.DatabaseManager.GetParameter("@personid2", DbType.Int32, (workshopDefinition.Facilitators != null && workshopDefinition.Facilitators.Count > 1) ? workshopDefinition.Facilitators[1].PersonId : 0),
				this.DatabaseManager.GetParameter("@personid3", DbType.Int32, (workshopDefinition.Facilitators != null && workshopDefinition.Facilitators.Count > 2) ? workshopDefinition.Facilitators[2].PersonId : 0),
				this.DatabaseManager.GetParameter("@note", DbType.String, workshopDefinition.WorkshopNotes),
				this.DatabaseManager.GetParameter("@location", DbType.String, workshopDefinition.WorkshopLocation)
			};
			this.DatabaseManager.ExecuteNonQuery("update Workshops\r\nSET AppTypeID = @apptypeid,\r\n\tworkshopTitle = @workshoptitle,\r\n\tworkshopDescription = @workshopdescription,\r\n\tMaxAttendees = @maxattendees,\r\n\tWorkshopFee = @workshopfee,\r\n\tpersonID = @personid,\r\n\tpersonID2 = @personid2,\r\n\tpersonID3 = @personid3,\r\n\tnote = @note,\r\n\tlocation = @location\r\nWHERE WorkshopID = @workshopid", parameters);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000606A0 File Offset: 0x0005E8A0
		public WorkshopDefinition LoadWorkshopDefinitionById(int WorkshopId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@workshopid", DbType.Int32, WorkshopId)
			};
			WorkshopDefinition result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tw.workshopid,w.apptypeid,w.workshopTitle,w.workshopDescription,w.MaxAttendees,w.WorkshopFee,\r\n\t\tw.partners,w.note,w.location,w.availableForOnlineBooking,w.waitingListMaxUsers,\r\n\t\tat.[description] AS apptypedescription,at.defaultcolour,at.iscourse,at.isworkshop,at.appointmentTypeGroupID,\r\n        atg.title AS apptypegrouptitle,\r\n\t\tw.personID AS p1personid,p.firstName AS p1firstname,p.lastName AS p1lastname,p.student_no AS p1student_no,\r\n        pgp.groupid AS p1groupid,\r\n\t\tw.personID2 AS p2personid,p2.firstName AS p2firstname,p2.lastName AS p2lastname,p2.student_no AS p2student_no,\r\n        pgp2.groupid AS p2groupid,\r\n\t\tw.personID3 AS p3personid,p3.firstName AS p3firstname,p3.lastName AS p3lastname,p3.student_no AS p3student_no,\r\n        pgp3.groupid AS p3groupid\r\nFROM\tWorkshops w LEFT JOIN AppointmentTypes at ON at.AppTypeID=w.AppTypeID \r\n        LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\n\t\tLEFT JOIN people p ON p.PersonID=w.personID\r\n        LEFT JOIN peoplegroups pgp ON pgp.personid=p.personid AND pgp.groupid<10\r\n\t\tLEFT JOIN people p2 ON p2.PersonID=w.personID2\r\n        LEFT JOIN peoplegroups pgp2 ON pgp2.personid=p2.personid AND pgp2.groupid<10\r\n\t\tLEFT JOIN people p3 ON p3.PersonID=w.personID3\r\n        LEFT JOIN peoplegroups pgp3 ON pgp3.personid=p3.personid AND pgp3.groupid<10\r\nWHERE  w.isactive=1 AND w.workshopid=@workshopid\r\nORDER BY at.[description],w.AppTypeID,w.workshopTitle,pgp.groupid,pgp2.groupid,pgp3.groupid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					bool flag2 = dataReader.Read();
					if (flag2)
					{
						return WorkshopDefinitionDAO.GetWorkshopDefinitionFromReader(dataReader, this.OpContext);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x04000599 RID: 1433
		private DatabaseLayer DatabaseManager;
	}
}
