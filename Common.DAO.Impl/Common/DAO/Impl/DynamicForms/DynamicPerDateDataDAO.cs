using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000DF RID: 223
	public class DynamicPerDateDataDAO : IDynamicPerDateDataDAO
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x00042482 File Offset: 0x00040682
		public DynamicPerDateDataDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00042494 File Offset: 0x00040694
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0004249C File Offset: 0x0004069C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000689 RID: 1673 RVA: 0x000424A8 File Offset: 0x000406A8
		public static IList<PerDateEntryWithChildEntries> GetPerDateEntriesWithChildEntriesFromReader(OperationContext OpContext, IDataReader reader)
		{
			bool flag = reader == null;
			IList<PerDateEntryWithChildEntries> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<PerDateEntryWithChildEntries> list = new List<PerDateEntryWithChildEntries>();
				PerDateEntryWithChildEntries perDateEntryWithChildEntries = null;
				while (reader.Read())
				{
					int num = (reader["appointmentid"] is DBNull) ? 0 : ((int)reader["appointmentid"]);
					bool flag2 = num < 1;
					if (!flag2)
					{
						bool flag3 = perDateEntryWithChildEntries == null || perDateEntryWithChildEntries.AppointmentId != num;
						if (flag3)
						{
							perDateEntryWithChildEntries = DynamicPerDateDataDAO.GetPerDateEntryFromRecord<PerDateEntryWithChildEntries>(OpContext, reader, null);
							bool flag4 = perDateEntryWithChildEntries == null;
							if (flag4)
							{
								continue;
							}
							perDateEntryWithChildEntries.ChildEntries = new List<PerDateEntry>();
							list.Add(perDateEntryWithChildEntries);
						}
						PerDateEntry perDateEntryFromRecord = DynamicPerDateDataDAO.GetPerDateEntryFromRecord<PerDateEntry>(OpContext, reader, "sub");
						bool flag5 = perDateEntryFromRecord == null;
						if (!flag5)
						{
							perDateEntryWithChildEntries.ChildEntries.Add(perDateEntryFromRecord);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0004258C File Offset: 0x0004078C
		public static T GetPerDateEntryFromRecord<T>(OperationContext OpContext, IDataReader record, string prefix = null) where T : PerDateEntry
		{
			string text = prefix ?? "";
			bool flag = record == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string text2 = record[text + "description"].ToString();
				bool flag2 = text2.Equals(".deleted.", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					T t = Activator.CreateInstance<T>();
					t.AppointmentId = (int)record[text + "appointmentid"];
					t.DateEntered = (DateTime)record[text + "dateentered"];
					t.Description = text2;
					t.ScreenNum = (int)record[text + "screennum"];
					t.Student = PeopleDAO.GetPersonFromReader(text, record, OpContext, null);
					t.WhoEntered = PeopleDAO.GetPersonFromReader(text + "who", record, OpContext, null);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000426AC File Offset: 0x000408AC
		public static PerDateEntry GetPerDateEntryFromRecord(OperationContext OpContext, IDataReader record)
		{
			return DynamicPerDateDataDAO.GetPerDateEntryFromRecord<PerDateEntry>(OpContext, record, null);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000426C8 File Offset: 0x000408C8
		public IList<PerDateEntry> LoadPerDateEntries(int StudentPersonId, int ScreenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, StudentPersonId),
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			IList<PerDateEntry> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    pm.appointmentid,pm.personid,pm.dateentered,\r\n            pm.whoentered AS whopersonid,pwho.firstname AS whofirstname,pwho.lastname AS wholastname,pwho.student_no AS whostudent_no,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            pm.[description],pm.screennum\r\nFROM        infopm pm LEFT JOIN people p ON p.personid=pm.personid\r\n            LEFT JOIN people pwho ON pwho.personid=pm.whoentered\r\nWHERE       pm.personid=@pid AND pm.screennum=@screennum\r\nORDER BY pm.dateentered desc", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PerDateEntry> list = new List<PerDateEntry>();
					while (dataReader.Read())
					{
						PerDateEntry perDateEntryFromRecord = DynamicPerDateDataDAO.GetPerDateEntryFromRecord(this.OpContext, dataReader);
						bool flag2 = perDateEntryFromRecord != null;
						if (flag2)
						{
							list.Add(perDateEntryFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0004279C File Offset: 0x0004099C
		public IList<PerDateEntryWithChildEntries> LoadPerDateEntriesWithChildEntries(int StudentPersonId, int ParentScreenNum, int ChildScreenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, StudentPersonId),
				databaseLayer.GetParameter("@parentscreennum", DbType.Int32, ParentScreenNum),
				databaseLayer.GetParameter("@childscreennum", DbType.Int32, ChildScreenNum)
			};
			IList<PerDateEntryWithChildEntries> perDateEntriesWithChildEntriesFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    pm.appointmentid,pm.personid,pm.dateentered,\r\n            pm.whoentered AS whopersonid,pwho.firstname AS whofirstname,pwho.lastname AS wholastname,pwho.student_no AS whostudent_no,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            pm.[description],pm.screennum\r\nFROM        infopm pm LEFT JOIN people p ON p.personid=pm.personid\r\n            LEFT JOIN people pwho ON pwho.personid=pm.whoentered\r\nWHERE       pm.personid=@pid AND pm.screennum=@screennum\r\nORDER BY pm.dateentered desc", parameters))
			{
				perDateEntriesWithChildEntriesFromReader = DynamicPerDateDataDAO.GetPerDateEntriesWithChildEntriesFromReader(this.OpContext, dataReader);
			}
			return perDateEntriesWithChildEntriesFromReader;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00042848 File Offset: 0x00040A48
		public IList<PersonBase> LoadUniqueStudentsWithPerDateDataEnteredByForm(int ScreenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			IList<PersonBase> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    DISTINCT ipm.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        infopm ipm LEFT JOIN people p ON p.personid=ipm.personid\r\nWHERE       ipm.screennum=@screennum AND p.isactive=1", parameters))
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
						PersonBase student = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
						bool flag2 = student != null && student.PersonId > 0 && list.Find((PersonBase s) => s.PersonId == student.PersonId) == null;
						if (flag2)
						{
							list.Add(student);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0004294C File Offset: 0x00040B4C
		public int CreatePerDateEntry(PerDateEntry perDateEntry)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = perDateEntry.Student == null || perDateEntry.Student.PersonId < 1;
			if (flag)
			{
				throw new Exception("Can't create perdateentery with missing student.  Student provided is null or personid<1.");
			}
			bool flag2 = perDateEntry.ScreenNum < 1;
			if (flag2)
			{
				throw new Exception("Can't create perdateentery with missing screennum.  Screennum provided is <1.");
			}
			DbParameter[] array = new DbParameter[5];
			array[0] = databaseLayer.GetParameter("@dateentered", DbType.DateTime, perDateEntry.DateEntered);
			int num = 1;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@whoentered";
			DbType pType = DbType.Int32;
			PersonBase whoEntered = perDateEntry.WhoEntered;
			array[num] = databaseLayer2.GetParameter(pName, pType, (whoEntered != null) ? whoEntered.PersonId : 0);
			array[2] = databaseLayer.GetParameter("@personid", DbType.Int32, perDateEntry.Student.PersonId);
			array[3] = databaseLayer.GetParameter("@description", DbType.String, perDateEntry.Description ?? "");
			array[4] = databaseLayer.GetParameter("@screennum", DbType.Int32, perDateEntry.ScreenNum);
			DbParameter[] parameters = array;
			return (int)databaseLayer.ExecuteScalar("INSERT INTO infopm (dateentered,whoentered,personid,description,screennum) VALUES (@dateentered,@whoentered,@personid,@description,@screennum);\r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS appointmentid", parameters);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00042A6C File Offset: 0x00040C6C
		public PerDateEntry GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0)),
				databaseLayer.GetParameter("@pid", DbType.Int32, StudentPersonId),
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    pm.appointmentid,pm.personid,pm.dateentered,\r\n            pm.whoentered AS whopersonid,pwho.firstname AS whofirstname,pwho.lastname AS wholastname,pwho.student_no AS whostudent_no,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            pm.[description],pm.screennum\r\nFROM        infopm pm LEFT JOIN people p ON p.personid=pm.personid\r\n            LEFT JOIN people pwho ON pwho.personid=pm.whoentered\r\nWHERE       pm.personid=@pid AND pm.screennum=@screennum\r\n            AND pm.dateentered BETWEEN @startdate AND @enddate\r\n            AND NOT CAST(pm.description AS varchar(max))='.DELETED.'\r\nORDER BY pm.dateentered desc", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					PerDateEntry perDateEntryFromRecord = DynamicPerDateDataDAO.GetPerDateEntryFromRecord(this.OpContext, dataReader);
					bool flag2 = perDateEntryFromRecord == null;
					if (!flag2)
					{
						return perDateEntryFromRecord;
					}
				}
			}
			return null;
		}
	}
}
