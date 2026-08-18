using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.Caching;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class Student
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000F390 File Offset: 0x0000D590
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000F3CC File Offset: 0x0000D5CC
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000F414 File Offset: 0x0000D614
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000F420 File Offset: 0x0000D620
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000F438 File Offset: 0x0000D638
		public string Student_no
		{
			get
			{
				return this.student_no;
			}
			set
			{
				this.student_no = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000F444 File Offset: 0x0000D644
		public string Name
		{
			get
			{
				return string.IsNullOrEmpty(this.firstName) ? this.lastName.ToString() : (this.firstName + " " + this.lastName);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000F488 File Offset: 0x0000D688
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		public string CounsellorName
		{
			get
			{
				return this.counsellorName;
			}
			set
			{
				this.counsellorName = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public string CounsellorEmail
		{
			get
			{
				return this.counsellorEmail;
			}
			set
			{
				this.counsellorEmail = value;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public Student()
		{
			this.firstName = "";
			this.middleName = "";
			this.lastName = "";
			this.student_no = "";
			this.email = "";
			this.counsellorName = "";
			this.counsellorEmail = "";
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000F558 File Offset: 0x0000D758
		public Student(int pid)
		{
			this.personId = pid;
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "DECLARE @emailcid int\r\nSET @emailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)\r\n\r\nDECLARE @counsellorcid int\r\nSET @counsellorcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=99671)\r\n\r\nDECLARE @counselloremailcid varchar(max)\r\nSET @counselloremailcid = (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=428)\r\n\r\nSELECT    p.firstname,p.middlename,p.lastname,p.student_no,p.personid\r\n          ,oi.valtext AS emailvaltext,oi.valbytes AS emailvalbytes\r\n          ,ps2.valbytes AS counsellorname\r\n          ,ois.valtext AS counselloremailvaltext,ois.valbytes AS counselloremailvalbytes,ois.valbytesisencrypted\r\nFROM        people p LEFT JOIN perstudentdata2 oi ON oi.personid=p.personid AND oi.controlid=@emailcid\r\n            LEFT JOIN perstudentdata2 ps2 ON ps2.personid=p.personid AND ps2.controlid=@counsellorcid\r\n            LEFT JOIN perstudentdata2 ois ON ois.controlid=@counselloremailcid AND ois.personid=ps2.valint\r\nWHERE       p.personid=@pid AND p.isactive=1";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid)
			});
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"middlename",
					"lastname",
					"student_no",
					"counsellorname"
				});
				DataRow dataRow = dataTable.Rows[0];
				this.firstName = dataRow["firstname"].ToString();
				this.middleName = dataRow["middlename"].ToString();
				this.lastName = dataRow["lastname"].ToString();
				this.student_no = dataRow["student_no"].ToString();
				this.email = dataRow["emailvaltext"].ToString();
				this.counsellorName = dataRow["counsellorname"].ToString();
				bool flag2 = dataRow["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow["valbytesisencrypted"]);
				bool flag3 = flag2;
				if (flag3)
				{
					byte[] array = (dataRow["counselloremailvalbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dataRow["counselloremailvalbytes"]);
					this.counsellorEmail = ((array.Length != 0) ? encryption.Decrypt(array) : "");
				}
				else
				{
					this.counsellorEmail = dataRow["counselloremailvaltext"].ToString();
				}
				bool flag4 = string.IsNullOrEmpty(this.email) && dataRow["emailvalbytes"] != DBNull.Value;
				if (flag4)
				{
					this.email = encryption.Decrypt((byte[])dataRow["emailvalbytes"]);
				}
			}
			else
			{
				this.firstName = "";
				this.middleName = "";
				this.lastName = "";
				this.student_no = "";
				this.email = "";
				this.counsellorName = "";
				this.counsellorEmail = "";
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public static Exception ActivateStudent(int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Exception result;
			try
			{
				string query = "INSERT INTO peoplepreviousyears (personid,dateactive) VALUES (@pid,getdate())";
				clockWork.ExecuteNonQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, pid)
				});
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000F828 File Offset: 0x0000DA28
		public static int CreateUser(string snum, string fn, string mn, string ln, string groupIds, db conn)
		{
			string[] array = groupIds.Split(new char[]
			{
				','
			});
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = int.Parse(array[i]);
			}
			return Student.CreateUser(snum, fn, mn, ln, array2, conn);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000F884 File Offset: 0x0000DA84
		public static int CreateUser(string snum, string fn, string mn, string ln, int[] groupIds, db conn)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			byte[] value = encryption.Encrypt(snum);
			byte[] value2 = encryption.Encrypt(fn);
			byte[] value3 = encryption.Encrypt(ln);
			byte[] value4 = encryption.Encrypt(mn);
			string query = "INSERT INTO people (student_no,firstname,middlename,lastname,dateadded,isactive) VALUES (@sne,@fne,@mne,@lne,getdate(),1); SET @id=SCOPE_IDENTITY();";
			object obj = clockWork.ExecuteScalar(query, new DbParameter[]
			{
				clockWork.GetParameter("@sne", DbType.Binary, value),
				clockWork.GetParameter("@fne", DbType.Binary, value2),
				clockWork.GetParameter("@mne", DbType.Binary, value4),
				clockWork.GetParameter("@lne", DbType.Binary, value3),
				clockWork.GetOutputParameter("@id", DbType.Int32, 0)
			});
			bool flag = obj is DBNull || obj == null;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = (int)obj;
			}
			bool flag2 = num <= 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				bool flag3 = snum.Length < 1;
				if (flag3)
				{
					query = "UPDATE people SET student_no=@sne WHERE personid=@pid";
					clockWork.ExecuteNonQuery(query, new DbParameter[]
					{
						clockWork.GetParameter("@sne", DbType.Binary, encryption.Encrypt("user" + num.ToString())),
						clockWork.GetParameter("@pid", DbType.Int32, num)
					});
				}
				bool flag4 = false;
				for (int i = 0; i < groupIds.Length; i++)
				{
					query = "INSERT INTO peoplegroups (personid,groupid,isprimarygroup) VALUES (@pid,@gid,@primarygroup)";
					DbParameter[] array = new DbParameter[3];
					array[0] = clockWork.GetParameter("@pid", DbType.Int32, num);
					array[1] = clockWork.GetParameter("@gid", DbType.Int32, groupIds[i]);
					bool flag5 = groupIds[i] == 1;
					if (flag5)
					{
						array[2] = clockWork.GetParameter("@primarygroup", DbType.Boolean, true);
						flag4 = true;
					}
					else
					{
						array[2] = clockWork.GetParameter("@primarygroup", DbType.Boolean, false);
					}
					clockWork.ExecuteNonQuery(query, array);
				}
				bool flag6 = !flag4;
				if (flag6)
				{
					query = "INSERT INTO peoplegroups (personid,groupid,isprimarygroup) VALUES (@pid,@gid,@primarygroup)";
					clockWork.ExecuteNonQuery(query, new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, num),
						clockWork.GetParameter("@gid", DbType.Int32, 1),
						clockWork.GetParameter("@primarygroup", DbType.Boolean, true)
					});
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000FB04 File Offset: 0x0000DD04
		public static string LookupEmail(int pid, int emailCid, bool emailEncrypted)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, emailCid)
			});
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			string result;
			if (flag)
			{
				result = Core.BytesToString((byte[])dataTable.Rows[0][0], emailEncrypted, encryption);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public static Exception AddRowToListView(int pid, int cid, params string[] cellData)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Exception result;
			try
			{
				string query = "SELECT o.controlvalue FROM otherinfops o WHERE o.personid=@pid AND o.controlid=@cid";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, pid),
					clockWork.GetParameter("@cid", DbType.Int32, cid)
				});
				bool flag = dataTable.Rows.Count > 0;
				bool flag2;
				string text;
				if (flag)
				{
					flag2 = true;
					byte[] bytes = (byte[])dataTable.Rows[0][0];
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					text = utf8Encoding.GetString(bytes);
				}
				else
				{
					flag2 = false;
					text = "";
				}
				text = text.Trim();
				bool flag3 = text.Length > 0;
				if (flag3)
				{
					text += "\t";
				}
				for (int i = 0; i < cellData.Length; i++)
				{
					text += cellData[i];
					text += "\0";
				}
				text += DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
				UTF8Encoding utf8Encoding2 = new UTF8Encoding();
				byte[] bytes2 = utf8Encoding2.GetBytes(text);
				bool flag4 = flag2;
				if (flag4)
				{
					query = "UPDATE otherinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@cid";
					clockWork.ExecuteNonQuery(query, new DbParameter[]
					{
						clockWork.GetParameter("@cv", DbType.Binary, bytes2),
						clockWork.GetParameter("@pid", DbType.Int32, pid),
						clockWork.GetParameter("@cid", DbType.Int32, cid)
					});
				}
				else
				{
					query = "INSERT INTO otherinfops (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@cv)";
					clockWork.ExecuteNonQuery(query, new DbParameter[]
					{
						clockWork.GetParameter("@cv", DbType.Binary, bytes2),
						clockWork.GetParameter("@pid", DbType.Int32, pid),
						clockWork.GetParameter("@cid", DbType.Int32, cid)
					});
				}
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		[Obsolete("Use AppointmentBookingStudentWebClientManager.MarkStudentBannedFromOnlineAppointmentBooking instead")]
		public static DateTime BanStudent(int banCid, int pid, Cache Cache)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool flag = banCid > 0;
			DateTime result;
			if (flag)
			{
				string query = "DELETE FROM datetimeinfops WHERE personid=@pid AND controlid=@cid";
				clockWork.ExecuteNonQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, pid),
					clockWork.GetParameter("@cid", DbType.Int32, banCid)
				});
				query = "INSERT INTO datetimeinfops (screennum,controlid,controlvlaue,personid) VALUES (0,@cid,@cv,@pid)";
				DateTime dateTime = DateTime.Now.AddDays((double)webSettingsClientManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedNumDays));
				clockWork.ExecuteNonQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@cv", DbType.DateTime, dateTime)
				});
				result = dateTime;
			}
			else
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x04000136 RID: 310
		private int personId;

		// Token: 0x04000137 RID: 311
		private string firstName;

		// Token: 0x04000138 RID: 312
		private string middleName;

		// Token: 0x04000139 RID: 313
		private string lastName;

		// Token: 0x0400013A RID: 314
		private string student_no;

		// Token: 0x0400013B RID: 315
		private string email;

		// Token: 0x0400013C RID: 316
		private string counsellorName;

		// Token: 0x0400013D RID: 317
		private string counsellorEmail;
	}
}
