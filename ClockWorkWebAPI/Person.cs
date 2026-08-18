using System;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class Person
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000E13C File Offset: 0x0000C33C
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000E154 File Offset: 0x0000C354
		public int PersonId
		{
			get
			{
				return this.personId;
			}
			set
			{
				this.personId = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000E160 File Offset: 0x0000C360
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000E178 File Offset: 0x0000C378
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000E184 File Offset: 0x0000C384
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000E19C File Offset: 0x0000C39C
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public string MiddleName
		{
			get
			{
				return this.middleName;
			}
			set
			{
				this.middleName = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000E208 File Offset: 0x0000C408
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000E214 File Offset: 0x0000C414
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000E22C File Offset: 0x0000C42C
		public string StudentNumber
		{
			get
			{
				return this.studentNumber;
			}
			set
			{
				this.studentNumber = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000E238 File Offset: 0x0000C438
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000E250 File Offset: 0x0000C450
		public int PrimaryGroupId
		{
			get
			{
				return this.primaryGroupId;
			}
			set
			{
				this.primaryGroupId = value;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E25C File Offset: 0x0000C45C
		public Person(int personid, string name, string email)
		{
			this.personId = personid;
			this.firstName = "";
			this.lastName = "";
			this.middleName = "";
			this.email = email;
			this.name = name;
			this.studentNumber = "";
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public Person(int personid, string name, string email, string student_no)
		{
			this.personId = personid;
			this.firstName = "";
			this.lastName = "";
			this.middleName = "";
			this.email = email;
			this.name = name;
			this.studentNumber = student_no;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E318 File Offset: 0x0000C518
		public static DataTable GetDynamicControlPerStudentStringEncryptedTextValues(db conn, int pid, string cids)
		{
			conn.Da.SelectCommand.CommandText = "SELECT controlid,controlvalue FROM otherinfopa WHERE personid=@pid AND controlid IN (SELECT orderid As controlid FROM splitorderids(@cids,','))";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.Add("@pid", pid);
			conn.Da.SelectCommand.Parameters.Add("@cids", cids);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			return conn.TripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"controlvalue"
			});
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public static string GetStudentLastName(int pid, db conn)
		{
			conn.Da.SelectCommand.CommandText = "SELECT lastname FROM people WHERE personid=@pid";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			bool flag = dataTable.Rows.Count > 0;
			string result;
			if (flag)
			{
				result = conn.TripleDES.Decrypt((byte[])dataTable.Rows[0][0]);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E474 File Offset: 0x0000C674
		public static Person GetStudentInfo(db conn, int pid, Page page)
		{
			return Person.GetStudentInfo(pid, page);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000E490 File Offset: 0x0000C690
		public static Person GetStudentInfo(int pid, Page page)
		{
			return Person.GetStudentInfo(pid);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		public static Person GetStudentInfo(int pid)
		{
			string query = "SELECT p.personid,p.student_no,p.firstname,p.middlename,p.lastname,c.email,c.oktoemail,c.emailisnotencrypted\r\nFROM people p LEFT JOIN common c ON c.personid=p.personid\r\nWHERE p.isactive=1 AND p.personid=@pid";
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid)
			};
			DataTable dataTable;
			try
			{
				dataTable = clockWork.ExecuteQuery(query, parameters);
			}
			catch
			{
				dataTable = null;
			}
			bool flag = dataTable == null;
			Person result;
			if (flag)
			{
				result = Person.GetStudentInfoOld(pid);
			}
			else
			{
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					dataTable = clockWork.Encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"lastname",
						"middlename",
						"student_no"
					});
					DataRow dataRow = dataTable.Rows[0];
					bool decrypt = dataRow["emailisnotencrypted"] == DBNull.Value || (int)dataRow["emailisnotencrypted"] <= 0;
					bool flag3 = dataRow["email"] != DBNull.Value;
					string text;
					if (flag3)
					{
						byte[] bytes = (byte[])dataRow["email"];
						text = Core.BytesToString(bytes, decrypt, clockWork.Encryption);
					}
					else
					{
						text = "";
					}
					string arg = dataRow["firstname"].ToString().Trim();
					string arg2 = dataRow["lastname"].ToString().Trim();
					string text2 = dataRow["middlename"].ToString().Trim();
					string student_no = dataRow["student_no"].ToString().Trim();
					result = new Person(pid, string.Format("{0} {1}", arg, arg2), text, student_no)
					{
						FirstName = arg,
						MiddleName = text2,
						LastName = arg2,
						StudentNumber = student_no
					};
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		public static Person GetStudentInfoOld(int pid)
		{
			Person result;
			try
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				IEncryption encryption = clockWork.Encryption;
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.GENERAL_EmailCid);
				bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.GENERAL_EmailEncrypted);
				bool flag = settingValue > 0;
				DataTable dataTable;
				if (flag)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, pid),
						clockWork.GetParameter("@cid", DbType.Int32, settingValue)
					};
					dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentInfo, parameters);
				}
				else
				{
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, pid)
					};
					dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentInfo2, parameters);
					bool flag2 = dataTable.Rows.Count > 0;
					if (flag2)
					{
						DataRow dataRow = dataTable.Rows[0];
						bool flag3 = dataRow["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow["valbytesisencrypted"]);
						byte[] array = (dataRow["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["valbytes"]);
						string text = dataRow["valtext"].ToString();
						bool flag4 = flag3 && array.Length != 0;
						if (flag4)
						{
							dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
							{
								"valbytes"
							});
							dataTable.Columns["valbytes"].ColumnName = "email";
						}
						else
						{
							dataTable.Columns["valtext"].ColumnName = "email";
						}
					}
				}
				bool flag5 = dataTable.Rows.Count > 0;
				if (flag5)
				{
					dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"lastname",
						"middlename",
						"student_no"
					});
					DataRow dataRow2 = dataTable.Rows[0];
					bool flag6 = dataTable.Columns["email"].DataType == typeof(string);
					string text2;
					if (flag6)
					{
						text2 = dataRow2["email"].ToString().Trim();
					}
					else
					{
						byte[] array2 = (dataRow2["email"] == DBNull.Value) ? null : ((byte[])dataRow2["email"]);
						text2 = ((array2 == null) ? "" : Core.BytesToString(array2, settingValue2, encryption));
					}
					result = new Person(pid, dataRow2["firstname"].ToString() + " " + dataRow2["lastname"].ToString(), text2, dataRow2["student_no"].ToString())
					{
						FirstName = dataRow2["firstname"].ToString(),
						LastName = dataRow2["lastname"].ToString(),
						MiddleName = dataRow2["middlename"].ToString()
					};
				}
				else
				{
					result = new Person(pid, "", "");
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ClockWorkWebAPI:Person:GetStudentInfo(pid,page):pid={0}:Error={1}", pid.ToString(), ex.ToString());
				result = new Person(pid, "", "");
			}
			return result;
		}

		// Token: 0x0400009B RID: 155
		private int personId;

		// Token: 0x0400009C RID: 156
		private string firstName;

		// Token: 0x0400009D RID: 157
		private string lastName;

		// Token: 0x0400009E RID: 158
		private string middleName;

		// Token: 0x0400009F RID: 159
		private string name;

		// Token: 0x040000A0 RID: 160
		private string email;

		// Token: 0x040000A1 RID: 161
		private string studentNumber;

		// Token: 0x040000A2 RID: 162
		private int primaryGroupId = 0;
	}
}
