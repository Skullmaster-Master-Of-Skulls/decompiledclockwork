using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl
{
	// Token: 0x0200001A RID: 26
	public class PersonDAO : IPersonDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005588 File Offset: 0x00003788
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005590 File Offset: 0x00003790
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00005599 File Offset: 0x00003799
		public PersonDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000055CA File Offset: 0x000037CA
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000055D2 File Offset: 0x000037D2
		public OperationContext OpContext { get; set; }

		// Token: 0x060000AA RID: 170 RVA: 0x000055DC File Offset: 0x000037DC
		public List<Person> GetPersonsByGroup(int groupid)
		{
			List<Person> list = new List<Person>();
			DbParameter parameter = this.DatabaseManager.GetParameter("@groupid", DbType.Int32, groupid);
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select p.PersonID, p.firstName, p.lastName, p.student_no from People as p\r\n            inner join PeopleGroups as pg on p.PersonID = pg.PersonID\r\n            where pg.GroupID = @groupid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						Person personFromReader = this.GetPersonFromReader(dataReader);
						list.Add(personFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005678 File Offset: 0x00003878
		public Person GetPerson(int personId)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@personid", DbType.Int32, personId);
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from People where PersonID = @personid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetPersonFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000056FC File Offset: 0x000038FC
		private Person GetPersonFromReader(IDataReader record)
		{
			return this.GetPersonFromReader(record, "personid", "firstname", "middlename", "lastname", "student_no");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005730 File Offset: 0x00003930
		public Person GetPersonFromReader(IDataReader record, string pidColname, string fnColName, string mnColName, string lnColName, string snColName)
		{
			bool flag = record[pidColname] == DBNull.Value;
			Person result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(mnColName) && this.ReaderContainsColumn(record, mnColName);
				string middleName;
				if (flag2)
				{
					middleName = ((record[mnColName] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record[mnColName]));
				}
				else
				{
					middleName = "";
				}
				Person person = new Person
				{
					Id = ((string.IsNullOrEmpty(snColName) || record[snColName] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record[snColName])),
					FirstName = ((record[fnColName] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record[fnColName])),
					MiddleName = middleName,
					LastName = ((record[lnColName] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record[lnColName])),
					PersonID = (int)record[pidColname]
				};
				result = person;
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000588C File Offset: 0x00003A8C
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
	}
}
