using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000077 RID: 119
	public class StudentManagementDAO : IStudentManagementDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x000185A0 File Offset: 0x000167A0
		public StudentManagementDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000185D0 File Offset: 0x000167D0
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x000185D8 File Offset: 0x000167D8
		public OperationContext OpContext { get; set; }

		// Token: 0x060002E3 RID: 739 RVA: 0x000185E4 File Offset: 0x000167E4
		public IList<PersonBase> LoadActiveStudents(DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date)
			};
			IList<PersonBase> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("CREATE TABLE #tpids (personid INT)\r\n\r\nINSERT INTO #tpids\r\n\tEXEC ActiveStudentPids @startdate,@enddate\r\n\r\nSELECT \tt.personid,p.lastname,p.firstname,p.middlename,p.student_no\r\nFROM \t#tpids t LEFT JOIN people p ON p.personid=t.personid\r\nORDER BY t.personid;\r\n\r\nDROP TABLE #tpids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PersonBase> list = new List<PersonBase>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, batchDecryptor);
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

		// Token: 0x060002E4 RID: 740 RVA: 0x000186D4 File Offset: 0x000168D4
		private string GetSubstring(string s, int numCharacters, bool fromBeginning)
		{
			bool flag = s == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = s.Trim();
				bool flag2 = text.Length < numCharacters;
				if (flag2)
				{
					result = text;
				}
				else
				{
					result = (fromBeginning ? text.Substring(0, numCharacters) : text.Substring(text.Length - numCharacters));
				}
			}
			return result;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001872C File Offset: 0x0001692C
		public IList<PersonBase> PermanentlyDeleteStudents(IList<PersonBase> StudentsToDelete)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			try
			{
				foreach (PersonBase personBase in StudentsToDelete)
				{
					int personId = personBase.PersonId;
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, personId)
					};
					databaseLayer.ExecuteNonQueryTransaction("EXEC sp_DATABASE_DeleteStudent @pid", transaction, parameters);
					string value = Convert.ToBase64String(databaseLayer.Encryption.Encrypt(string.Concat(new string[]
					{
						this.GetSubstring(personBase.FirstName, 1, true),
						".",
						this.GetSubstring(personBase.MiddleName, 1, true),
						".",
						this.GetSubstring(personBase.LastName, 1, true),
						".",
						this.GetSubstring(personBase.Student_no, 3, false)
					})));
					parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, personId),
						databaseLayer.GetParameter("@staffpid", DbType.Int32, this.OpContext.WhoAmI),
						databaseLayer.GetParameter("@name", DbType.String, value)
					};
					databaseLayer.ExecuteNonQueryTransaction("INSERT INTO generallog (success,personid,whodidit,logtype,logsubtype,logdatetime,id1,id2,id3,generallognote)\r\nVALUES (1,@pid,@staffpid,99, 0, getdate(), 0,0,0,@name)", transaction, parameters);
				}
				databaseLayer.CommitDbTransaction(transaction);
				return StudentsToDelete;
			}
			catch (Exception ex)
			{
				databaseLayer.RollbackDbTransaction(transaction);
				CWLogger.Logger.Error("Common.DAO.Impl.People.StudentManagementDAO.PermanentlyDeleteStudents:Failed, transaction was rolled back.err={0}", ex.ToString());
			}
			return null;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00018908 File Offset: 0x00016B08
		public string LoadStudentNumber(int PersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId)
			};
			string result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT student_no FROM people WHERE personid=@pid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = databaseLayer.Encryption.Decrypt((byte[])dataReader["student_no"]);
				}
			}
			return result;
		}

		// Token: 0x0400012F RID: 303
		private DatabaseLayer DatabaseManager;
	}
}
