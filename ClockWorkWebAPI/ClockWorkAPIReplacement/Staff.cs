using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200006C RID: 108
	public class Staff
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00022D38 File Offset: 0x00020F38
		public Staff()
		{
			this.Pid = 0;
			this.FirstName = "";
			this.LastName = "";
			this.Title = "";
			this.Phone = "";
			this.Email = "";
			this.Signature = null;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00022D99 File Offset: 0x00020F99
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00022DA1 File Offset: 0x00020FA1
		public int Pid { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00022DAA File Offset: 0x00020FAA
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00022DB2 File Offset: 0x00020FB2
		public string FirstName { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00022DBB File Offset: 0x00020FBB
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00022DC3 File Offset: 0x00020FC3
		public string LastName { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00022DCC File Offset: 0x00020FCC
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x00022DD4 File Offset: 0x00020FD4
		public string Title { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00022DDD File Offset: 0x00020FDD
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00022DE5 File Offset: 0x00020FE5
		public string Phone { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00022DEE File Offset: 0x00020FEE
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00022DF6 File Offset: 0x00020FF6
		public string Email { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00022DFF File Offset: 0x00020FFF
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00022E07 File Offset: 0x00021007
		public byte[] Signature { get; set; }

		// Token: 0x06000549 RID: 1353 RVA: 0x00022E10 File Offset: 0x00021010
		public static Staff LoadStaffInfo(int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "DECLARE @titlecid int, @emailcid int, @phonecid int, @signaturecid int\r\nSET @titlecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=443)\r\nSET @emailcid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=428)\r\nSET @phonecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=429)\r\nSET @signaturecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=99719)\r\n\r\nSELECT\tp.personid,p.firstname,p.lastname,p.student_no\r\n        ,pt.valbytesisencrypted AS TitleEncrypted,pt.valtext AS titletext\r\n        ,pe.valbytesisencrypted AS EmailEncrypted,pe.valtext AS emailtext\r\n        ,pp.valbytesisencrypted AS PhoneEncrypted,pp.valtext AS phonetext\r\n\t\t,pt.valbytes AS Title,pe.valbytes AS Email,pp.valbytes AS Phone,ps.controlvalue AS sig\r\nFROM\tpeople p LEFT JOIN perstudentdata2 pt ON pt.PersonID=p.PersonID AND pt.ControlID=@titlecid\r\n\t\tLEFT JOIN perstudentdata2 pe ON pe.PersonID=p.PersonID AND pe.ControlID=@emailcid\r\n\t\tLEFT JOIN perstudentdata2 pp ON pp.PersonID=p.PersonID AND pp.ControlID=@phonecid\r\n\t\tLEFT JOIN imageinfops ps ON ps.PersonID=p.PersonID AND ps.ControlID=@signaturecid\r\nWHERE\tp.PersonID=@pid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid)
			});
			bool flag = dataTable.Rows.Count > 0;
			Staff result;
			if (flag)
			{
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no"
				});
				DataRow dataRow = dataTable.Rows[0];
				Staff staff = new Staff();
				staff.Pid = pid;
				staff.FirstName = dataRow["firstname"].ToString();
				staff.LastName = dataRow["lastname"].ToString();
				staff.Phone = Staff.GetDynamicDataValue(dataRow, encryption, "PhoneEncrypted", "Phone", "phonetext");
				staff.Email = Staff.GetDynamicDataValue(dataRow, encryption, "EmailEncrypted", "Email", "emailtext");
				staff.Title = Staff.GetDynamicDataValue(dataRow, encryption, "TitleEncrypted", "Title", "titletext");
				bool flag2 = dataRow["sig"] == DBNull.Value;
				if (flag2)
				{
					staff.Signature = null;
				}
				else
				{
					string text;
					byte[] signature = Utility.ExtractImageBytes((byte[])dataRow["sig"], out text);
					staff.Signature = signature;
				}
				result = staff;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00022FA4 File Offset: 0x000211A4
		private static string GetDynamicDataValue(DataRow dr, IEncryption tripleDES, string valBytesIsEncryptedColname, string bytesColname, string textColname)
		{
			bool flag = dr[valBytesIsEncryptedColname] != DBNull.Value && Convert.ToBoolean(dr[valBytesIsEncryptedColname]) && dr[bytesColname] != DBNull.Value;
			string result;
			if (flag)
			{
				byte[] encryptedText = (byte[])dr[bytesColname];
				result = tripleDES.Decrypt(encryptedText);
			}
			else
			{
				result = dr[textColname].ToString();
			}
			return result;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00023010 File Offset: 0x00021210
		public static Staff LoadStaffInfoFromAssignedCounsellorForStudent(int studentPid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "DECLARE @counsellorcid int, @titlecid int, @emailcid int, @phonecid int, @signaturecid int\r\nSET @counsellorcid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=99671)\r\nSET @titlecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=443)\r\nSET @emailcid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=428)\r\nSET @phonecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=429)\r\nSET @signaturecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=99719)\r\n\r\nSELECT\tmp.controlvalue AS personid,p.firstname,p.lastname,p.student_no\r\n        ,pt.valbytesisencrypted AS TitleEncrypted,pt.valtext AS titletext\r\n        ,pe.valbytesisencrypted AS EmailEncrypted,pe.valtext AS emailtext\r\n        ,pp.valbytesisencrypted AS PhoneEncrypted,pp.valtext AS phonetext\r\n\t\t,pt.valbytes AS Title,pe.valbytes AS Email,pp.valbytes AS Phone,ps.controlvalue AS sig\r\nFROM\tmaininfops mp LEFT JOIN people p ON p.personid=mp.controlvalue\r\n        LEFT JOIN perstudentdata2 pt ON pt.PersonID=p.PersonID AND pt.ControlID=@titlecid\r\n\t\tLEFT JOIN perstudentdata2 pe ON pe.PersonID=p.PersonID AND pe.ControlID=@emailcid\r\n\t\tLEFT JOIN perstudentdata2 pp ON pp.PersonID=p.PersonID AND pp.ControlID=@phonecid\r\n\t\tLEFT JOIN imageinfops ps ON ps.PersonID=p.PersonID AND ps.ControlID=@signaturecid\r\nWHERE\tmp.PersonID=@studentpid AND mp.controlid=@counsellorcid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@studentpid", DbType.Int32, studentPid)
			});
			bool flag = dataTable.Rows.Count > 0;
			Staff result;
			if (flag)
			{
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no"
				});
				DataRow dataRow = dataTable.Rows[0];
				Staff staff = new Staff();
				staff.Pid = (int)dataRow["personid"];
				staff.FirstName = dataRow["firstname"].ToString();
				staff.LastName = dataRow["lastname"].ToString();
				staff.Phone = Staff.GetDynamicDataValue(dataRow, encryption, "PhoneEncrypted", "Phone", "phonetext");
				staff.Email = Staff.GetDynamicDataValue(dataRow, encryption, "EmailEncrypted", "Email", "emailtext");
				staff.Title = Staff.GetDynamicDataValue(dataRow, encryption, "TitleEncrypted", "Title", "titletext");
				bool flag2 = dataRow["sig"] == DBNull.Value;
				if (flag2)
				{
					staff.Signature = null;
				}
				else
				{
					string text;
					byte[] signature = Utility.ExtractImageBytes((byte[])dataRow["sig"], out text);
					staff.Signature = signature;
				}
				result = staff;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000231B4 File Offset: 0x000213B4
		public static string LookupStaffSignatureBase64(int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid)
			};
			string query = "DECLARE @sigcid INT\r\nSET @sigcid=(SELECT settingvalue AS titlecid FROM settingsgroups WHERE groupid=-1 AND settingcode=99719)\r\nSELECT controlvalue FROM imageinfops WHERE controlid=@sigcid AND personid=@pid";
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			string result;
			if (flag)
			{
				byte[] array = (byte[])dataTable.Rows[0][0];
				string text;
				array = Utility.ExtractImageBytes(array, out text);
				result = Convert.ToBase64String(array);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00023264 File Offset: 0x00021464
		public static string LookupStaffTitle(int pid, UnivDataAdapter da, IEncryption tripleDES)
		{
			int settingInt = Staff.GetSettingInt(da, 443);
			return Staff.LookupStaffTextInfo(pid, settingInt, da, tripleDES);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002328C File Offset: 0x0002148C
		private static int GetSettingInt(UnivDataAdapter da, int setting)
		{
			da.SelectCommand.CommandText = "SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=@code";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@code", setting);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool flag = dataTable.Rows.Count > 0;
			int result;
			if (flag)
			{
				result = ((dataTable.Rows[0][0] == DBNull.Value) ? 0 : ((int)dataTable.Rows[0][0]));
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00023334 File Offset: 0x00021534
		public static string LookupStaffPhone(int pid, UnivDataAdapter da, IEncryption tripleDES)
		{
			int settingInt = Staff.GetSettingInt(da, 429);
			return Staff.LookupStaffTextInfo(pid, settingInt, da, tripleDES);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002335C File Offset: 0x0002155C
		public static string LookupStaffTextInfo(int pid, int cid, UnivDataAdapter da, IEncryption tripleDES)
		{
			bool flag = cid < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				da.SelectCommand.CommandText = "SELECT ops.controlvalue,dc.controlcode,dc.setting1,dc.setting2,dc.setting3 FROM otherinfops ops LEFT JOIN dynamiccontrols dc ON dc.controlid=ops.controlid WHERE ops.personid=@pid AND ops.controlid=@cid";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", pid);
				da.SelectCommand.Parameters.Add("@cid", cid);
				DataTable dataTable = new DataTable();
				string text;
				da.Fill(dataTable, out text);
				bool flag2 = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
				if (flag2)
				{
					byte[] bytes = (byte[])dataTable.Rows[0][0];
					object obj = dataTable.Rows[0]["setting3"];
					bool decrypt = obj != null && (int)obj == 1;
					result = Utility.BytesToString(bytes, decrypt, tripleDES);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}
	}
}
