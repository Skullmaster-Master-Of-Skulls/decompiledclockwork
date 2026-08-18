using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000076 RID: 118
	public class StudentCommonInfoDAO : IStudentCommonInfoDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00017DAD File Offset: 0x00015FAD
		public StudentCommonInfoDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00017DDD File Offset: 0x00015FDD
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00017DE5 File Offset: 0x00015FE5
		public OperationContext OpContext { get; set; }

		// Token: 0x060002D9 RID: 729 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public StudentCommonInfo GetCommonInfoFromRecord(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return this.GetCommonInfoFromRecord(record, "", decryptor);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00017E10 File Offset: 0x00016010
		public StudentCommonInfo GetCommonInfoFromRecord(IDataReader record, string prefix, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			StudentCommonInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IBatchDecryptor batchDecryptor2 = batchDecryptor;
				if (batchDecryptor == null)
				{
					eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
					OperationContext opContext = this.OpContext;
					batchDecryptor2 = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
				}
				IBatchDecryptor batchDecryptor3 = batchDecryptor2;
				bool flag2 = prefix == "";
				if (flag2)
				{
					prefix = null;
				}
				string name = (prefix ?? "") + "email";
				string name2 = (prefix ?? "") + "emailisnotencrypted";
				string name3 = (prefix ?? "") + "advisorpersonid";
				string colName = (prefix ?? "") + "advisorfirstname";
				string colName2 = (prefix ?? "") + "advisorlastname";
				string name4 = (prefix ?? "") + "gender";
				string name5 = (prefix ?? "") + "personid";
				byte[] array = (record[name] == DBNull.Value) ? new byte[0] : ((byte[])record[name]);
				bool flag3 = array.Length < 1;
				string email;
				if (flag3)
				{
					email = "";
				}
				else
				{
					email = ((record[name2] != DBNull.Value && Convert.ToBoolean(record[name2])) ? Convert.ToString(array) : batchDecryptor3.Decrypt(array));
				}
				int num = (record[name3] != DBNull.Value) ? ((int)record[name3]) : 0;
				bool flag4 = num > 0;
				PersonBase assignedCounsellor;
				if (flag4)
				{
					assignedCounsellor = new PersonBase
					{
						PersonId = num,
						FirstName = record.DecryptString(batchDecryptor3, colName),
						LastName = record.DecryptString(batchDecryptor3, colName2),
						Student_no = "",
						MiddleName = ""
					};
				}
				else
				{
					assignedCounsellor = null;
				}
				object obj = record[name4];
				bool flag5 = obj != DBNull.Value;
				eGender gender;
				if (flag5)
				{
					string text = record[name4].ToString();
					bool flag6 = text.IndexOf("f", StringComparison.OrdinalIgnoreCase) >= 0;
					if (flag6)
					{
						gender = eGender.Female;
					}
					else
					{
						bool flag7 = text.IndexOf("m", StringComparison.OrdinalIgnoreCase) >= 0;
						if (flag7)
						{
							gender = eGender.Male;
						}
						else
						{
							gender = eGender.Unknown;
						}
					}
				}
				else
				{
					gender = eGender.Unknown;
				}
				string colName3 = (prefix ?? "") + "advisoremail";
				string colName4 = (prefix ?? "") + "advisortitle";
				string colName5 = (prefix ?? "") + "advisorphone";
				string colName6 = (prefix ?? "") + "phone";
				string name6 = (prefix ?? "") + "dateofbirth";
				result = new StudentCommonInfo
				{
					PersonId = ((record[name5] == DBNull.Value) ? 0 : ((int)record[name5])),
					Email = email,
					AssignedCounsellor = assignedCounsellor,
					AssignedCounsellorEmail = record.DecryptString(batchDecryptor3, colName3),
					AssignedCounsellorTitle = record.DecryptString(batchDecryptor3, colName4),
					AssignedCounsellorPhone = record.DecryptString(batchDecryptor3, colName5),
					Phone = record.DecryptString(batchDecryptor3, colName6),
					DateOfBirth = ((record[name6] == DBNull.Value) ? null : new DateTime?((DateTime)record[name6])),
					Gender = gender
				};
			}
			return result;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00018190 File Offset: 0x00016390
		public StudentWithCommonInfo LoadStudentWithCommonInfo(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\n            p.personid,c.email,c.oktoemail,c.emailisnotencrypted,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,'' AS advisorstudent_no,\r\n            c.advisortitle,c.advisoremail,c.advisorphone,c.phone,c.dateofbirth,c.gender\r\nFROM        people p LEFT JOIN common c ON c.personid=p.personid\r\nWHERE       p.personid=@pid AND p.isactive=1 AND NOT p.personid IS NULL", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					StudentCommonInfo commonInfoFromRecord = this.GetCommonInfoFromRecord(dataReader, null);
					PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
					return new StudentWithCommonInfo
					{
						Student = personFromReader,
						CommonInfo = commonInfoFromRecord
					};
				}
			}
			return null;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00018240 File Offset: 0x00016440
		public IList<StudentWithCommonInfo> LoadStudentsWithCommonInfo(IList<int> PersonIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", (from g in PersonIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			List<StudentWithCommonInfo> list = new List<StudentWithCommonInfo>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\n            p.personid,c.email,c.oktoemail,c.emailisnotencrypted,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,'' AS advisorstudent_no,\r\n            c.advisortitle,c.advisoremail,c.advisorphone,c.phone,c.dateofbirth,c.gender\r\nFROM        people p LEFT JOIN common c ON c.personid=p.personid\r\nWHERE       p.isactive=1 AND NOT p.personid IS NULL AND p.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\nORDER BY p.personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					StudentCommonInfo commonInfoFromRecord = this.GetCommonInfoFromRecord(dataReader, null);
					PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
					bool flag2 = personFromReader != null;
					if (flag2)
					{
						list.Add(new StudentWithCommonInfo
						{
							Student = personFromReader,
							CommonInfo = commonInfoFromRecord
						});
					}
				}
			}
			return list;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00018340 File Offset: 0x00016540
		[DebuggerStepThrough]
		public Task<IList<StudentWithCommonInfo>> LoadStudentsWithCommonInfoAsync(IList<int> PersonIds)
		{
			StudentCommonInfoDAO.<LoadStudentsWithCommonInfoAsync>d__10 <LoadStudentsWithCommonInfoAsync>d__ = new StudentCommonInfoDAO.<LoadStudentsWithCommonInfoAsync>d__10();
			<LoadStudentsWithCommonInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentWithCommonInfo>>.Create();
			<LoadStudentsWithCommonInfoAsync>d__.<>4__this = this;
			<LoadStudentsWithCommonInfoAsync>d__.PersonIds = PersonIds;
			<LoadStudentsWithCommonInfoAsync>d__.<>1__state = -1;
			<LoadStudentsWithCommonInfoAsync>d__.<>t__builder.Start<StudentCommonInfoDAO.<LoadStudentsWithCommonInfoAsync>d__10>(ref <LoadStudentsWithCommonInfoAsync>d__);
			return <LoadStudentsWithCommonInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001838C File Offset: 0x0001658C
		public StudentCommonInfo LoadStudentCommonInfo(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.personid,c.email,c.oktoemail,c.emailisnotencrypted,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,'' AS advisorstudent_no,\r\n            c.advisortitle,c.advisoremail,c.advisorphone,c.phone,c.dateofbirth,c.gender\r\nFROM        common c\r\nWHERE       c.personid=@pid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetCommonInfoFromRecord(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00018410 File Offset: 0x00016610
		public IList<StudentWithCommonInfo> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, CounsellorPersonId),
				this.DatabaseManager.GetParameter("@sd", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@ed", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@showappswith", DbType.Boolean, ShowStudentsIHaveAppsWith),
				this.DatabaseManager.GetParameter("@showadvisorfor", DbType.Boolean, ShowStudentsIAmAdvisorFor),
				this.DatabaseManager.GetParameter("@includecancelled", DbType.Boolean, IncludeCancelledAppointments),
				this.DatabaseManager.GetParameter("@includenoshow", DbType.Boolean, IncludeNoShowAppointments)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC sp_Students_MyStudents @pid,@sd,@ed,@showappswith,@showadvisorfor,@includecancelled,@includenoshow", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<StudentWithCommonInfo> list = new List<StudentWithCommonInfo>();
					while (dataReader.Read())
					{
						StudentCommonInfo commonInfoFromRecord = this.GetCommonInfoFromRecord(dataReader, null);
						PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
						list.Add(new StudentWithCommonInfo
						{
							Student = personFromReader,
							CommonInfo = commonInfoFromRecord
						});
					}
					list.Sort((StudentWithCommonInfo g, StudentWithCommonInfo h) => g.Student.GetStudentName().CompareTo(h.Student.GetStudentName()));
					return list;
				}
			}
			return null;
		}

		// Token: 0x0400012D RID: 301
		private DatabaseLayer DatabaseManager;
	}
}
