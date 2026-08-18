using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x0200009D RID: 157
	public class LookupSubjectDAO : ILookupSubjectDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00027084 File Offset: 0x00025284
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0002708C File Offset: 0x0002528C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000446 RID: 1094 RVA: 0x00027095 File Offset: 0x00025295
		public LookupSubjectDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000270C6 File Offset: 0x000252C6
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x000270CE File Offset: 0x000252CE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000449 RID: 1097 RVA: 0x000270D8 File Offset: 0x000252D8
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
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

		// Token: 0x0600044A RID: 1098 RVA: 0x00027118 File Offset: 0x00025318
		internal static LookupSubject GetSubjectFromCourseRecord(string colNamePrefix, IDataReader record)
		{
			object obj = record[colNamePrefix + "subjectid"];
			bool flag = obj == DBNull.Value;
			LookupSubject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = colNamePrefix + "subjectcode";
				object obj2 = LookupSubjectDAO.ReaderContainsColumn(record, text) ? record[text] : DBNull.Value;
				string text2 = colNamePrefix + "subject";
				object obj3 = LookupSubjectDAO.ReaderContainsColumn(record, text2) ? record[text2] : record[colNamePrefix + "subjectdescription"];
				string text3 = colNamePrefix + "subjectemail";
				object obj4 = LookupSubjectDAO.ReaderContainsColumn(record, text3) ? record[text3] : DBNull.Value;
				LookupSubject lookupSubject = new LookupSubject
				{
					Id = (int)obj,
					SubjectCode = ((obj2 == DBNull.Value) ? "" : ((string)obj2)),
					SubjectDescription = ((obj3 == DBNull.Value) ? "" : ((string)obj3)),
					SubjectEmail = ((obj4 == DBNull.Value) ? "" : ((string)obj4))
				};
				result = lookupSubject;
			}
			return result;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00027240 File Offset: 0x00025440
		private static LookupSubject GetSubjectFromRecord(string colNamePrefix, IDataRecord record)
		{
			return new LookupSubject
			{
				Id = (int)record[colNamePrefix + "lucoursedataid"],
				SubjectCode = record[colNamePrefix + "lookupstring"].ToString(),
				SubjectDescription = record[colNamePrefix + "altlookupstring"].ToString(),
				SubjectEmail = record[colNamePrefix + "email"].ToString()
			};
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000272D0 File Offset: 0x000254D0
		public List<LookupSubject> LoadAllLookupSubjects()
		{
			List<LookupSubject> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT lucd.lucoursedataid,lucd.lookupstring,lucd.altlookupstring,lucd.email \r\nFROM    lucoursedata lucd \r\nWHERE   lucd.lookuplisttype=0 AND lucd.lucoursedataid IN \r\n    (SELECT subjectid AS lucoursedataid FROM lucourses)\r\nORDER BY lucd.altlookupstring"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupSubject> list = new List<LookupSubject>();
					while (dataReader.Read())
					{
						LookupSubject subjectFromRecord = LookupSubjectDAO.GetSubjectFromRecord("", dataReader);
						bool flag2 = subjectFromRecord != null;
						if (flag2)
						{
							list.Add(subjectFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00027358 File Offset: 0x00025558
		public List<LookupSubject> LoadLookupSubjectsBySession(Session Session)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, Session.StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, Session.EndDate)
			};
			List<LookupSubject> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT lucd.lucoursedataid,lucd.lookupstring,lucd.altlookupstring,lucd.email \r\nFROM    lucoursedata lucd \r\nWHERE   lucd.lookuplisttype=0 AND lucd.lucoursedataid IN \r\n    (SELECT subjectid AS lucoursedataid FROM lucourses WHERE NOT ( enddate <= @startdate OR startdate > @enddate))\r\nORDER BY lucd.altlookupstring", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupSubject> list = new List<LookupSubject>();
					while (dataReader.Read())
					{
						LookupSubject subjectFromRecord = LookupSubjectDAO.GetSubjectFromRecord("", dataReader);
						bool flag2 = subjectFromRecord != null;
						if (flag2)
						{
							list.Add(subjectFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00027428 File Offset: 0x00025628
		public LookupSubject LoadLookupSubject(int SubjectId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, SubjectId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid,lucd.lookupstring,lucd.altlookupstring,lucd.email \r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=0 AND lucd.lucoursedataid=@subjectid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return LookupSubjectDAO.GetSubjectFromRecord("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000274AC File Offset: 0x000256AC
		public void SaveSubject(LookupSubject subject)
		{
			bool flag = subject.SubjectId > 0;
			if (flag)
			{
				DbParameter[] array = new DbParameter[3];
				array[0] = this.DatabaseManager.GetParameter("@subjectid", DbType.Int32, subject.SubjectId);
				array[1] = this.DatabaseManager.GetParameter("@email", DbType.String, subject.SubjectEmail ?? "");
				int num = 2;
				DatabaseLayer databaseManager = this.DatabaseManager;
				string pName = "@subjectcode";
				DbType pType = DbType.String;
				string subjectCode = subject.SubjectCode;
				array[num] = databaseManager.GetParameter(pName, pType, ((subjectCode != null) ? subjectCode.Trim() : null) ?? "");
				DbParameter[] parameters = array;
				this.DatabaseManager.ExecuteNonQuery("UPDATE lucoursedata SET lookupstring=@subjectcode,email=@email WHERE lucoursedataid=@subjectid", parameters);
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@subjectdescription", DbType.String, subject.SubjectDescription ?? ""),
					this.DatabaseManager.GetParameter("@email", DbType.String, subject.SubjectEmail ?? ""),
					this.DatabaseManager.GetParameter("@subjectcode", DbType.String, subject.SubjectCode ?? "")
				};
				object obj = this.DatabaseManager.ExecuteScalar("IF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=0 AND ((NOT lookupstring='' AND lookupstring=@subjectcode) OR (NOT altlookupstring='' AND altlookupstring=@subjectdescription)))\r\n    SELECT CAST(0 AS int) AS lucoursedataid\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email) VALUES (0,@subjectcode,@subjectdescription,@email);\r\n    SELECT CAST(SCOPE_IDENTITY() AS INT) AS lucoursedataid\r\nEND", parameters);
				bool flag2 = obj == null;
				if (flag2)
				{
					throw new Exception("Failed to insert new subject.");
				}
				subject.SubjectId = (int)obj;
				bool flag3 = subject.SubjectId < 1;
				if (flag3)
				{
					throw new Exception("Failed to insert new subject (invalid subject id returned).");
				}
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00027620 File Offset: 0x00025820
		public LookupSubject LoadLookupSubjectBySubjectCode(string SubjectCode)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@subjectcode", DbType.String, SubjectCode)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid,lucd.lookupstring,lucd.altlookupstring,lucd.email \r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=0 AND NOT lucd.lookupstring='' AND lucd.lookupstring=@subjectcode", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return LookupSubjectDAO.GetSubjectFromRecord("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000276A0 File Offset: 0x000258A0
		public LookupSubject LoadLookupSubjectBySubjectDescription(string SubjectDescription)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@subjectdescription", DbType.String, SubjectDescription)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid,lucd.lookupstring,lucd.altlookupstring,lucd.email \r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=0 AND NOT lucd.altlookupstring='' AND lucd.altlookupstring=@subjectdescription", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return LookupSubjectDAO.GetSubjectFromRecord("", dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00027720 File Offset: 0x00025920
		public LookupSubject LoadLookupSubject(string SubjectCode, string SubjectDescription)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@subjectcode", DbType.String, SubjectCode),
				this.DatabaseManager.GetParameter("@subjectdescription", DbType.String, SubjectDescription)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    lucd.lucoursedataid,lucd.lookupstring,lucd.altlookupstring,lucd.email \r\nFROM        lucoursedata lucd \r\nWHERE       lucd.lookuplisttype=0 AND ((NOT lucd.lookupstring='' AND lucd.lookupstring=@subjectcode) OR (NOT lucd.altlookupstring='' AND lucd.altlookupstring=@subjectdescription))", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return LookupSubjectDAO.GetSubjectFromRecord("", dataReader);
				}
			}
			return null;
		}
	}
}
