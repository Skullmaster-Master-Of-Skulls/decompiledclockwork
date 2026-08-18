using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Encryption;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.Encryption;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000078 RID: 120
	public class PeopleDAO : IPeopleDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000189B0 File Offset: 0x00016BB0
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x000189B8 File Offset: 0x00016BB8
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060002E9 RID: 745 RVA: 0x000189C1 File Offset: 0x00016BC1
		public PeopleDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000189F2 File Offset: 0x00016BF2
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000189FA File Offset: 0x00016BFA
		public OperationContext OpContext { get; set; }

		// Token: 0x060002EC RID: 748 RVA: 0x00018A04 File Offset: 0x00016C04
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

		// Token: 0x060002ED RID: 749 RVA: 0x00018A44 File Offset: 0x00016C44
		internal static bool RecordContainsColumn(IDataRecord record, string colName)
		{
			for (int i = 0; i < record.FieldCount; i++)
			{
				bool flag = record.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00018A84 File Offset: 0x00016C84
		private List<Group> GetGroupsFromTable(DataTable t)
		{
			List<Group> list = new List<Group>();
			foreach (object obj in t.Rows)
			{
				DataRow dr = (DataRow)obj;
				Group groupFromDataRow = this.GetGroupFromDataRow(dr);
				bool flag = groupFromDataRow != null;
				if (flag)
				{
					list.Add(groupFromDataRow);
				}
			}
			return list;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00018B08 File Offset: 0x00016D08
		private PersonBase GetPersonFromDataRows(DataTable t, int startIndex, int endIndex)
		{
			DataRow dataRow = t.Rows[startIndex];
			object obj = dataRow["student_no"];
			object obj2 = dataRow["firstname"];
			object obj3 = dataRow["middlename"];
			object obj4 = dataRow["lastname"];
			PersonBase personBase = new PersonBase
			{
				PersonId = (int)dataRow["personid"],
				FirstName = ((obj2 == DBNull.Value) ? "" : ((string)obj2)),
				MiddleName = ((obj3 == DBNull.Value) ? "" : ((string)obj3)),
				LastName = ((obj4 == DBNull.Value) ? "" : ((string)obj4)),
				Student_no = ((obj == DBNull.Value) ? "" : ((string)obj)),
				CoreGroup = eCoreGroup.Unknown
			};
			for (int i = startIndex; i <= endIndex; i++)
			{
				DataRow dataRow2 = t.Rows[i];
				int num = (dataRow2["groupid"] is DBNull) ? 0 : ((int)dataRow2["groupid"]);
				bool flag = num <= 0;
				if (!flag)
				{
					eCoreGroup coreGroup = num.GetCoreGroup();
					bool flag2 = coreGroup == eCoreGroup.Unknown;
					if (!flag2)
					{
						personBase.CoreGroup = coreGroup;
						break;
					}
				}
			}
			return personBase;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00018C78 File Offset: 0x00016E78
		private Group GetGroupFromDataRow(DataRow dr)
		{
			object obj = dr["groupid"];
			object obj2 = dr.Table.Columns.Contains("description") ? dr["description"] : "";
			bool flag = dr.Table.Columns.Contains("description");
			string text;
			if (flag)
			{
				text = "description";
			}
			else
			{
				bool flag2 = dr.Table.Columns.Contains("groupdescription");
				if (flag2)
				{
					text = "groupdescription";
				}
				else
				{
					text = null;
				}
			}
			Group group = new Group
			{
				GroupId = ((obj != DBNull.Value) ? ((int)dr["groupid"]) : 0),
				Description = ((text != null) ? dr[text].ToString() : "")
			};
			bool flag3 = dr.Table.Columns.Contains("viewappsvisible");
			if (flag3)
			{
				object obj3 = dr["viewappsvisible"];
				group.VisibleInCalendar = (obj3 != DBNull.Value && Convert.ToBoolean(obj3));
			}
			return group;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00018D94 File Offset: 0x00016F94
		private Group GetGroupFromRecord(IDataReader record)
		{
			return PeopleGroupDAO.GetGroupFromRecord(record);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00018DAC File Offset: 0x00016FAC
		public List<PersonBase> GetPeopleFromDataTable(DataTable table)
		{
			int num;
			return this.GetPeopleFromDataTable(table, out num);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00018DC8 File Offset: 0x00016FC8
		public List<PersonBase> GetPeopleFromDataTable(DataTable table, out int BiggestPid)
		{
			List<PersonBase> list = new List<PersonBase>();
			DataTable dataTable = this.DatabaseManager.Encryption.EncryptOrDecryptNameDataTableBatch(false, table, new string[]
			{
				"firstname",
				"middlename",
				"lastname",
				"student_no"
			});
			int i = 0;
			int num = 0;
			while (i < dataTable.Rows.Count)
			{
				DataRow dataRow = dataTable.Rows[i];
				int num2 = (int)dataRow["personid"];
				bool flag = num2 > num;
				if (flag)
				{
					num = num2;
				}
				int j;
				for (j = i; j < dataTable.Rows.Count; j++)
				{
					DataRow dataRow2 = dataTable.Rows[j];
					int num3 = (int)dataRow2["personid"];
					bool flag2 = num3 != num2;
					if (flag2)
					{
						break;
					}
				}
				PersonBase personFromDataRows = this.GetPersonFromDataRows(dataTable, i, j - 1);
				bool flag3 = personFromDataRows != null;
				if (flag3)
				{
					bool flag4 = dataTable.Columns.Contains("groupid");
					if (flag4)
					{
						personFromDataRows.Groups = new List<Group>();
						for (int k = i; k < j; k++)
						{
							Group grp = this.GetGroupFromDataRow(dataTable.Rows[k]);
							bool flag5 = grp != null && grp.GroupId > 0 && personFromDataRows.Groups.FirstOrDefault((Group h) => h.GroupId == grp.GroupId) == null;
							if (flag5)
							{
								personFromDataRows.Groups.Add(grp);
							}
						}
					}
					list.Add(personFromDataRows);
				}
				i = j;
			}
			BiggestPid = num;
			return list;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00018FA8 File Offset: 0x000171A8
		public static BasicPerson GetBasicPersonFromRecord(string colPrefix, IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			BasicPerson result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string name = colPrefix + "personid";
				string name2 = colPrefix + "firstname";
				string text = colPrefix + "middlename";
				string name3 = colPrefix + "lastname";
				string name4 = colPrefix + "student_no";
				int num = (record[name] is DBNull) ? 0 : ((int)record[name]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new BasicPerson
					{
						PersonId = num,
						FirstName = ((record[name2] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record[name2])),
						MiddleName = (PeopleDAO.RecordContainsColumn(record, text) ? ((record[text] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record[text])) : ""),
						LastName = ((record[name3] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record[name3])),
						StudentNumber = ((record[name4] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record[name4]))
					};
				}
			}
			return result;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00019120 File Offset: 0x00017320
		public static T GetPersonBaseFromReader<T>(string colPrefix, IDataReader reader, OperationContext opContext, IBatchDecryptor batchDecryptor = null) where T : PersonBase
		{
			string text = colPrefix + "personid";
			bool flag = !PeopleDAO.ReaderContainsColumn(reader, text) || reader[text] == DBNull.Value;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string name = colPrefix + "firstname";
				string text2 = colPrefix + "middlename";
				string name2 = colPrefix + "lastname";
				string name3 = colPrefix + "student_no";
				string text3 = colPrefix + "groupid";
				string text4 = colPrefix + "groupdescription";
				object obj = reader[name];
				object obj2 = reader[name2];
				object obj3 = reader[name3];
				int num = (int)reader[text];
				bool flag2 = num < 1;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption;
					T t = Activator.CreateInstance<T>();
					t.PersonId = num;
					t.FirstName = ((obj == DBNull.Value) ? "" : ((batchDecryptor == null) ? ((encryption != null) ? encryption.Decrypt((byte[])obj) : null) : batchDecryptor.Decrypt((byte[])obj)));
					t.LastName = ((obj2 == DBNull.Value) ? "" : ((batchDecryptor == null) ? ((encryption != null) ? encryption.Decrypt((byte[])obj2) : null) : batchDecryptor.Decrypt((byte[])obj2)));
					t.Student_no = ((obj3 == DBNull.Value) ? "" : ((batchDecryptor == null) ? ((encryption != null) ? encryption.Decrypt((byte[])obj3) : null) : batchDecryptor.Decrypt((byte[])obj3)));
					t.PersonId = num;
					bool flag3 = PeopleDAO.ReaderContainsColumn(reader, text2);
					if (flag3)
					{
						object obj4 = reader[text2];
						t.MiddleName = ((obj4 == DBNull.Value) ? "" : ((batchDecryptor == null) ? encryption.Decrypt((byte[])obj4) : batchDecryptor.Decrypt((byte[])obj4)));
					}
					bool flag4 = PeopleDAO.ReaderContainsColumn(reader, text3);
					if (flag4)
					{
						object obj5 = reader[text3];
						bool flag5 = obj5 != DBNull.Value;
						if (flag5)
						{
							int groupId = (int)obj5;
							t.CoreGroup = new Group
							{
								GroupId = groupId
							}.GetCoreGroup();
							bool flag6 = PeopleDAO.ReaderContainsColumn(reader, text4);
							if (flag6)
							{
								t.Groups = new List<Group>
								{
									new Group
									{
										GroupId = groupId,
										Description = reader[text4].ToString().Trim()
									}
								};
							}
						}
					}
					result = t;
				}
			}
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001940C File Offset: 0x0001760C
		public static PersonBase GetPersonFromReader(string colPrefix, IDataReader reader, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			return PeopleDAO.GetPersonBaseFromReader<PersonBase>(colPrefix, reader, opContext, batchDecryptor);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00019428 File Offset: 0x00017628
		public List<int> LoadAllowedStudentPids(string studentSpecificSql, List<int> gids, bool useRestrictive)
		{
			List<int> list = new List<int>();
			bool flag = studentSpecificSql.Length <= 0;
			List<int> result;
			if (flag)
			{
				result = this.LoadAllowedPids(gids, useRestrictive, 1);
			}
			else
			{
				string query = "SELECT DISTINCT p.personid FROM people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE p.isactive=1 AND " + studentSpecificSql;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(query))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						return list;
					}
					while (dataReader.Read())
					{
						list.Add((int)dataReader[0]);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000194CC File Offset: 0x000176CC
		public List<int> LoadAllowedStaffPids(List<int> gids, bool useRestrictive)
		{
			return this.LoadAllowedPids(gids, useRestrictive, 2);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000194E8 File Offset: 0x000176E8
		public List<int> LoadAllowedRoomPids(List<int> gids, bool useRestrictive)
		{
			return this.LoadAllowedPids(gids, useRestrictive, 3);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00019504 File Offset: 0x00017704
		public List<int> LoadAllowedResourcePids(List<int> gids, bool useRestrictive)
		{
			return this.LoadAllowedPids(gids, useRestrictive, 4);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00019520 File Offset: 0x00017720
		private List<int> LoadAllowedPids(List<int> gids, bool useRestrictive, int primaryGroupId)
		{
			List<int> list = new List<int>();
			bool flag = gids.Count < 1 && !useRestrictive;
			if (flag)
			{
				gids.Add(primaryGroupId);
			}
			bool flag2 = gids.Count > 0;
			if (flag2)
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", gids.ConvertAll<string>((int f) => f.ToString()).ToArray()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT orderid AS groupid INTO #t1 FROM splitorderids(@gids,',');\r\n\r\nSELECT   DISTINCT pg.personid\r\nFROM    peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE   pg.groupid IN (SELECT groupid FROM #t1)\r\n        AND p.isactive=1\r\n\r\nDROP TABLE #t1", parameters))
				{
					bool flag3 = dataReader != null;
					if (flag3)
					{
						while (dataReader.Read())
						{
							int num = (dataReader[0] == DBNull.Value) ? 0 : ((int)dataReader[0]);
							bool flag4 = num > 0;
							if (flag4)
							{
								list.Add(num);
							}
						}
						return list;
					}
				}
			}
			return list;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00019644 File Offset: 0x00017844
		private string GetUniqueString()
		{
			return Guid.NewGuid().ToString() + "T";
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00019674 File Offset: 0x00017874
		public int CreateUser(PersonBase User, List<int> GroupIds)
		{
			bool flag = User.Student_no == null || User.Student_no.Trim().Length < 1;
			if (flag)
			{
				User.Student_no = this.GetUniqueString();
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@fne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.FirstName ?? "")),
				this.DatabaseManager.GetParameter("@mne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.MiddleName ?? "")),
				this.DatabaseManager.GetParameter("@lne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.LastName ?? "")),
				this.DatabaseManager.GetParameter("@sne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt((User.Student_no ?? "").Trim().ToUpper()))
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("INSERT INTO people (firstname,middlename,lastname,student_no,isactive,dateadded) VALUES (@fne,@mne,@lne,@sne,1,getdate());\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS personid", parameters))
			{
				bool flag2 = dataReader != null;
				if (flag2)
				{
					bool flag3 = dataReader.Read();
					if (flag3)
					{
						object obj = dataReader[0];
						bool flag4 = obj != DBNull.Value;
						if (flag4)
						{
							int num = (int)obj;
							parameters = new DbParameter[]
							{
								this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
								this.DatabaseManager.GetParameter("@pid", DbType.Int32, num)
							};
							this.DatabaseManager.ExecuteNonQuery("INSERT INTO peopledatesadded (personid,dateadded,whoadded ) VALUES (@pid,getdate(),@whoami)", parameters);
							DbParameter[] array = new DbParameter[2];
							array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, num);
							array[1] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", GroupIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
							parameters = array;
							this.DatabaseManager.ExecuteNonQuery("INSERT INTO peoplegroups (groupid,personid,isprimarygroup) \r\n    SELECT orderid AS groupid,@pid,\r\n        CASE WHEN ( orderid>=1 AND orderid<=4 ) THEN CAST(1 as bit)\r\n        ELSE CAST(0 AS bit)\r\n        END FROM splitorderids(@gids,',') WHERE NOT EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=orderid)", parameters);
							return num;
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000198F8 File Offset: 0x00017AF8
		public List<Group> LoadAllGroups()
		{
			DataTable t = this.DatabaseManager.ExecuteQuery("SELECT    groupid,description,isprimary,viewappsvisible,fulldescription,ordernum \r\nFROM        groups\r\nORDER BY description,ordernum");
			return this.GetGroupsFromTable(t);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00019924 File Offset: 0x00017B24
		public List<Group> LoadAllRoomGroups()
		{
			DataTable t = this.DatabaseManager.ExecuteQuery("SELECT groupid INTO #t1 FROM peoplegroups WHERE personid IN (SELECT personid FROM peoplegroups WHERE groupid=3);\r\n\r\nSELECT    groupid,description,isprimary,viewappsvisible,fulldescription,ordernum \r\nFROM        groups\r\nWHERE   groupid IN (SELECT groupid FROM #t1)\r\nORDER BY description,ordernum;\r\n\r\nDROP TABLE #t1");
			return this.GetGroupsFromTable(t);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00019950 File Offset: 0x00017B50
		public List<PersonBase> LoadGroupMembers(int GroupId)
		{
			return this.LoadGroupMembers(new int[]
			{
				GroupId
			});
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00019974 File Offset: 0x00017B74
		public List<PersonBase> LoadGroupMembersByPersonIds(int[] GroupIds, IList<int> PersonIds)
		{
			bool flag = PersonIds == null;
			if (flag)
			{
				PersonIds = new List<int>();
			}
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", GroupIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			array[1] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", PersonIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			DataTable table = this.DatabaseManager.ExecuteQuery("SELECT orderid AS groupid INTO #t1 FROM splitorderids(@gids,',');\r\nSELECT orderid AS personid INTO #t2 FROM splitorderids(@pids,',');\r\n\r\nSELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\n            LEFT JOIN peoplegroups pg2 ON pg2.personid=pg.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       pg.personid IN (SELECT personid FROM #t2) \r\n            AND pg.groupid IN (SELECT groupid FROM #t1)\r\n            AND p.isactive=1\r\nORDER BY    pg.personid;\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2", parameters);
			return this.GetPeopleFromDataTable(table);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00019A54 File Offset: 0x00017C54
		public List<PersonBase> LoadGroupMembers(int[] GroupIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", GroupIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			DbParameter[] parameters = array;
			DataTable table = this.DatabaseManager.ExecuteQuery("SELECT orderid AS groupid INTO #t1 FROM splitorderids(@gids,',');\r\n\r\nSELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\n            LEFT JOIN peoplegroups pg2 ON pg2.personid=pg.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       pg.groupid IN (SELECT groupid FROM #t1)\r\n            AND p.isactive=1\r\nORDER BY    pg.personid;\r\n\r\nDROP TABLE #t1", parameters);
			return this.GetPeopleFromDataTable(table);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00019AD4 File Offset: 0x00017CD4
		public PersonBase LoadPerson(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			DataTable table = this.DatabaseManager.ExecuteQuery("EXEC LoadPersonByPersonId @pid", parameters);
			List<PersonBase> peopleFromDataTable = this.GetPeopleFromDataTable(table);
			bool flag = peopleFromDataTable.Count > 0;
			PersonBase result;
			if (flag)
			{
				result = peopleFromDataTable[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00019B40 File Offset: 0x00017D40
		public List<PersonBase> LoadAllUserObjectsAndBiggestPid(out int BiggestPid, bool LoadIsActivatedStatusForStudents = false)
		{
			DataTable table = this.DatabaseManager.ExecuteQuery("IF @loadisactive=0\r\nBEGIN\r\n    SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n                ,pg.groupid,g.description\r\n    FROM        people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid<10\r\n                LEFT JOIN groups g ON g.groupid=pg.groupid\r\n    WHERE       p.isactive=1\r\n    ORDER BY    p.personid\r\nEND\r\nELSE \r\nBEGIN\r\n    SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n                ,pg.groupid,g.description\r\n    FROM        people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid<10\r\n                LEFT JOIN groups g ON g.groupid=pg.groupid\r\n    WHERE       p.isactive=1\r\n    ORDER BY    p.personid\r\nEND ", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@loadisactive", DbType.Boolean, LoadIsActivatedStatusForStudents)
			});
			List<PersonBase> peopleFromDataTable = this.GetPeopleFromDataTable(table, out BiggestPid);
			CWLogger.Logger.Debug("LoadAllUserObjectsAndBiggestPid:biggestpid={0}", BiggestPid.ToString());
			return peopleFromDataTable;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00019BA4 File Offset: 0x00017DA4
		public List<PersonBase> LoadAllUserObjects(bool LoadIsActivatedStatusForStudents = false)
		{
			DataTable table = this.DatabaseManager.ExecuteQuery("IF @loadisactive=0\r\nBEGIN\r\n    SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n                ,pg.groupid,g.description\r\n    FROM        people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid<10\r\n                LEFT JOIN groups g ON g.groupid=pg.groupid\r\n    WHERE       p.isactive=1\r\n    ORDER BY    p.personid\r\nEND\r\nELSE \r\nBEGIN\r\n    SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n                ,pg.groupid,g.description\r\n    FROM        people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid<10\r\n                LEFT JOIN groups g ON g.groupid=pg.groupid\r\n    WHERE       p.isactive=1\r\n    ORDER BY    p.personid\r\nEND ", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@loadisactive", DbType.Boolean, LoadIsActivatedStatusForStudents)
			});
			return this.GetPeopleFromDataTable(table);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00003998 File Offset: 0x00001B98
		public List<int> LoadAllowedStudentPids(List<int> GroupIds, string OverrideSql)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00019BF0 File Offset: 0x00017DF0
		public DateTime? GetStudentAccommodationExpiryDate(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @cid int\r\nSET @cid=(SELECT TOP 1 controlid FROM DynamicScreenControls WHERE screennum=4 AND controlID IN (SELECT controlID FROM DynamicControls WHERE ControlCode=6 AND Setting2=1) ORDER BY orderNum)\r\n\r\nSELECT controlvalue FROM DateTimeInfoAccommodationPS WHERE PersonID=@pid AND ControlID=@cid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					bool flag2 = dataReader.Read();
					if (flag2)
					{
						object obj = dataReader["controlvalue"];
						bool flag3 = obj != DBNull.Value;
						if (flag3)
						{
							return new DateTime?((DateTime)obj);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00003998 File Offset: 0x00001B98
		public List<int> LoadAllowedStudentPids()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00019CA8 File Offset: 0x00017EA8
		public PersonBase LoadPersonByStudentNumber(string Student_No)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Student_No.ToUpper().Trim()))
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC LoadPersonByStudentNumber @sne", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00019D48 File Offset: 0x00017F48
		public IList<PersonBase> LoadPersonsByStudentNumber(string Student_No)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Student_No.ToUpper().Trim()))
			};
			DataTable table = this.DatabaseManager.ExecuteQuery("EXEC LoadPersonByStudentNumber @sne", parameters);
			return this.GetPeopleFromDataTable(table);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00019DAC File Offset: 0x00017FAC
		public DateTime GetPersonDateAdded(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			object obj = this.DatabaseManager.ExecuteScalar("SELECT dateadded FROM people WHERE personid=@pid", parameters);
			bool flag = obj != null && obj is DateTime;
			DateTime result;
			if (flag)
			{
				result = (DateTime)obj;
			}
			else
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00019E14 File Offset: 0x00018014
		public PersonBase LoadAnyNonDeletedAccountByStudentNumber(string snum)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@snume", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(snum.Trim().ToUpper()))
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p\r\nWHERE       p.isactive=1 \r\n            AND p.student_no=@snume", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00019EB4 File Offset: 0x000180B4
		public PersonBase LoadStudentByEmail(string Email, int ControlId, bool EmailIsEncrypted)
		{
			byte[] value;
			byte[] value2;
			byte[] value3;
			if (EmailIsEncrypted)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				value = utf8Encoding.GetBytes(Email);
				value2 = null;
				value3 = null;
			}
			else
			{
				value = this.DatabaseManager.Encryption.Encrypt(Email);
				value2 = null;
				value3 = null;
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@email0", DbType.Binary, value),
				this.DatabaseManager.GetParameter("@emaillower", DbType.Binary, value3),
				this.DatabaseManager.GetParameter("@emailupper", DbType.Binary, value2),
				this.DatabaseManager.GetParameter("@cid", DbType.Int32, ControlId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p\r\nWHERE       p.isactive=1 AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            AND p.personid IN (SELECT personid FROM perstudentdata WHERE controlid=@cid AND (valbytes=@))", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
				}
			}
			return null;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00019FC4 File Offset: 0x000181C4
		public void UpdateUser(PersonBase User)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@fne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.FirstName ?? "")),
				this.DatabaseManager.GetParameter("@mne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.MiddleName ?? "")),
				this.DatabaseManager.GetParameter("@lne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.LastName ?? "")),
				this.DatabaseManager.GetParameter("@sne", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(User.Student_no ?? "")),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, User.PersonId)
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT personid FROM people WHERE isactive=1 AND student_no=@sne)\r\n    UPDATE people SET firstname=@fne,middlename=@mne,lastname=@lne WHERE personid=@pid\r\nelse \r\n    UPDATE people SET firstname=@fne,middlename=@mne,lastname=@lne,student_no=@sne WHERE personid=@pid", parameters);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001A0D8 File Offset: 0x000182D8
		public int CreateGroup(Group Group)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@description", DbType.String, Group.Description ?? ""),
				this.DatabaseManager.GetParameter("@isprimary", DbType.Boolean, true),
				this.DatabaseManager.GetParameter("@viewappsvisible", DbType.Boolean, Group.VisibleInCalendar),
				this.DatabaseManager.GetParameter("@fulldescription", DbType.String, Group.FullDescription ?? ""),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Group.OrderNum)
			};
			return (int)this.DatabaseManager.ExecuteScalar("INSERT INTO groups (description,isprimary,viewappsvisible,fulldescription,ordernum)\r\nVALUES (@description,@isprimary,@viewappsvisible,@fulldescription,@ordernum);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS groupid", parameters);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001A1A8 File Offset: 0x000183A8
		public void UpdateGroup(Group Group)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@description", DbType.String, Group.Description ?? ""),
				this.DatabaseManager.GetParameter("@viewappsvisible", DbType.Boolean, Group.VisibleInCalendar),
				this.DatabaseManager.GetParameter("@fulldescription", DbType.String, Group.FullDescription ?? ""),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Group.OrderNum),
				this.DatabaseManager.GetParameter("@gid", DbType.Int32, Group.GroupId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE groups SET description=@description,viewappsvisible=@viewappsvisible,fulldescription=@fulldescription,\r\nordernum=@ordernum WHERE groupid=@gid", parameters);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001A278 File Offset: 0x00018478
		public bool DeleteGroup(int GroupId)
		{
			List<PersonBase> list = this.LoadGroupMembers(GroupId);
			bool flag = list.Count > 0;
			if (flag)
			{
				throw new AbortedDueToSafetyCheck(string.Format("Group cannot be deleted because it contains {0} member(s).", list.Count.ToString()));
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@gid", DbType.Int32, GroupId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM groups WHERE groupid=@gid", parameters);
			return true;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001A2F8 File Offset: 0x000184F8
		public bool DeleteUser(int PersonId, bool JustDeactivate)
		{
			if (JustDeactivate)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE people SET isactive=0 WHERE personid=@pid", parameters);
				return true;
			}
			throw new AbortedDueToSafetyCheck("Full delete of users is currently not implemented.");
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001A354 File Offset: 0x00018554
		public PersonBase UnDeleteUser(int PersonId)
		{
			PersonBase personBase = this.LoadPerson(PersonId);
			PersonBase personBase2 = this.LoadPersonByStudentNumber(personBase.Student_no);
			bool flag = personBase2 != null;
			if (flag)
			{
				personBase.Student_no = personBase.Student_no + "__" + personBase.PersonId.ToString();
				this.UpdateUser(personBase);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE people SET isactive=1 WHERE personid=@pid", parameters);
			return personBase;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001A3EC File Offset: 0x000185EC
		public IList<Group> LoadUserGroupMemberships(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT pg.groupid,g.description \r\nFROM peoplegroups pg LEFT JOIN groups g ON g.groupid=pg.groupid \r\nWHERE pg.personid=@pid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<Group> list = new List<Group>();
					while (dataReader.Read())
					{
						Group groupFromRecord = this.GetGroupFromRecord(dataReader);
						bool flag2 = groupFromRecord != null;
						if (flag2)
						{
							list.Add(groupFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001A498 File Offset: 0x00018698
		public void AddUserToGroups(int PersonId, IList<int> GroupIds)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", GroupIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO peoplegroups (groupid,personid,isprimarygroup) \r\n    SELECT orderid AS groupid,@pid,\r\n        CASE WHEN ( orderid>=1 AND orderid<=4 ) THEN CAST(1 as bit)\r\n        ELSE CAST(0 AS bit)\r\n        END FROM splitorderids(@gids,',') WHERE NOT EXISTS(SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=orderid)", parameters);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001A528 File Offset: 0x00018728
		public IList<PersonBase> LoadPersonsByIds(IList<int> PersonIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", PersonIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			DataTable table = this.DatabaseManager.ExecuteQuery("EXEC LoadPersonsByPersonIds @pids", parameters);
			return this.GetPeopleFromDataTable(table);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001A5A8 File Offset: 0x000187A8
		public int GetLastPersonIdAddedToClockWork()
		{
			int result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT MAX(personid) AS personid FROM peoplelastaddition"))
			{
				bool flag = dataReader == null || !dataReader.Read() || dataReader["personid"] is DBNull;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (int)dataReader["personid"];
				}
			}
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001A620 File Offset: 0x00018820
		public IList<int> GetPidsGreaterThan(int pid)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, pid)
			};
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT personid FROM people WHERE personid>@pid", parameters))
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
						bool flag2 = dataReader["personid"] != DBNull.Value;
						if (flag2)
						{
							int item = (int)dataReader["personid"];
							bool flag3 = !list.Contains(item);
							if (flag3)
							{
								list.Add(item);
							}
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001A6F4 File Offset: 0x000188F4
		public IDictionary<string, int> LoadPersonIdsByStudentNumbers2(IList<string> StudentNumbers)
		{
			List<string> items = (from g in StudentNumbers
			where !string.IsNullOrEmpty(g) && g.Trim().Length > 0
			select g).Distinct<string>().ToList<string>();
			IEncryptionDAO encryptionDAO = new EncryptionDAO(this.DatabaseManager.Encryption);
			List<byte[]> list = (from g in encryptionDAO.EncryptData(items)
			where g != null && g.Length != 0
			select g).ToList<byte[]>();
			IList<Chunk> list2 = list.BreakdownItemsIntoChunks(2000);
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			bool flag = list2.Count < 1;
			IDictionary<string, int> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
				foreach (Chunk chunk in list2)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("SELECT student_no,personid FROM people WHERE isactive=1 AND (");
					List<DbParameter> list3 = new List<DbParameter>();
					for (int i = chunk.Start; i <= chunk.End; i++)
					{
						byte[] value = list[i];
						string text = "@p" + i.ToString();
						list3.Add(this.DatabaseManager.GetParameter(text, DbType.Binary, value));
						stringBuilder.AppendFormat("{0}student_no={1}", (i == 0) ? "" : " OR ", text);
					}
					stringBuilder.Append(")");
					using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(stringBuilder.ToString(), list3.ToArray()))
					{
						bool flag2 = dataReader == null;
						if (flag2)
						{
							return null;
						}
						while (dataReader.Read())
						{
							string text2 = (dataReader["student_no"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])dataReader["student_no"]);
							int num = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
							bool flag3 = text2.Length > 0 && num > 0 && !dictionary.ContainsKey(text2);
							if (flag3)
							{
								dictionary.Add(text2, num);
							}
						}
						return dictionary;
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001A9AC File Offset: 0x00018BAC
		public IList<int> LoadPersonIdsByStudentNumbers(IList<string> StudentNumbers)
		{
			IDictionary<string, int> source = this.LoadPersonIdsByStudentNumbers2(StudentNumbers);
			return (from g in source
			select g.Value).Distinct<int>().ToList<int>();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		public void RemoveUserFromGroups(int PersonId, IList<int> GroupIds)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = this.DatabaseManager.GetParameter("@gids", DbType.String, string.Join(",", GroupIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM peoplegroups WHERE personid=@pid AND groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))", parameters);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001AA88 File Offset: 0x00018C88
		public PersonBaseWithExtendedInfo LoadPersonWithExtendedInfo(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			PersonBaseWithExtendedInfo result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC LoadPersonByPersonId @pid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					PersonBaseWithExtendedInfo personBaseFromReader = PeopleDAO.GetPersonBaseFromReader<PersonBaseWithExtendedInfo>("", dataReader, this.OpContext, null);
					bool flag2 = personBaseFromReader == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						bool flag3 = PeopleDAO.ReaderContainsColumn(dataReader, "dateadded") && dataReader["dateadded"] != DBNull.Value;
						if (flag3)
						{
							personBaseFromReader.DateAdded = (DateTime)dataReader["dateadded"];
						}
						bool flag4 = personBaseFromReader.Groups == null;
						if (flag4)
						{
							personBaseFromReader.Groups = new List<Group>();
						}
						while (dataReader.Read())
						{
							int gid = (dataReader["groupid"] is DBNull) ? 0 : ((int)dataReader["groupid"]);
							bool flag5 = gid > 0 && personBaseFromReader.Groups.FirstOrDefault((Group g) => g.GroupId == gid) == null;
							if (flag5)
							{
								personBaseFromReader.Groups.Add(new Group
								{
									GroupId = gid,
									Description = dataReader["groupdescription"].ToString()
								});
							}
						}
						result = personBaseFromReader;
					}
				}
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001AC44 File Offset: 0x00018E44
		public bool IsUserInGroup(int PersonId, int GroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@gid", DbType.Int32, GroupId)
			};
			bool result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT persongroupid FROM peoplegroups WHERE personid=@pid AND groupid=@gid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					while (dataReader.Read())
					{
						int num = (dataReader[0] is DBNull) ? 0 : ((int)dataReader[0]);
						bool flag2 = num > 0;
						if (flag2)
						{
							return true;
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public string GetTempStudentNumber()
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@uniqueid", DbType.Int32, 0)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO uniqueids (dateadded) VALUES (getdate())\r\nSET @uniqueid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS uniqueid)\r\n\r\nDELETE FROM uniqueids WHERE uniqueid=@uniqueid", array);
			bool flag = array[0].Value == null || array[0].Value is DBNull;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = array[0].Value.ToString();
			}
			return result;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001AD94 File Offset: 0x00018F94
		public IList<PersonBase> LoadDeletedAccounts(params int[] GroupIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@gids";
			DbType pType = DbType.String;
			object value;
			if (GroupIds != null)
			{
				value = string.Join(",", (from g in GroupIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			List<PersonBase> list = new List<PersonBase>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\t\tp.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\n            ,pg2.groupid,g.description\r\nFROM        people p LEFT JOIN peoplegroups pg2 ON pg2.personid=p.personid\r\n            LEFT JOIN groups g ON g.groupid=pg2.groupid\r\nWHERE       p.isactive=0\r\n            AND (@gids IS NULL OR @gids='' OR p.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,','))))\r\nORDER BY    p.personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
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
			}
			return list;
		}
	}
}
