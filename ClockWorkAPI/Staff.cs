using System;
using System.Data;
using DynamicScreens;
using EncryptionClassLibrary;
using SettingsPermissions;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000053 RID: 83
	public class Staff
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x000157C0 File Offset: 0x000147C0
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

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00015824 File Offset: 0x00014824
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0001583B File Offset: 0x0001483B
		public int Pid { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00015844 File Offset: 0x00014844
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0001585B File Offset: 0x0001485B
		public string FirstName { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00015864 File Offset: 0x00014864
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0001587B File Offset: 0x0001487B
		public string LastName { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00015884 File Offset: 0x00014884
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0001589B File Offset: 0x0001489B
		public string Title { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000158A4 File Offset: 0x000148A4
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x000158BB File Offset: 0x000148BB
		public string Phone { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000158C4 File Offset: 0x000148C4
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x000158DB File Offset: 0x000148DB
		public string Email { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000158E4 File Offset: 0x000148E4
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000158FB File Offset: 0x000148FB
		public byte[] Signature { get; set; }

		// Token: 0x06000495 RID: 1173 RVA: 0x00015904 File Offset: 0x00014904
		public static Staff LoadStaffInfo(int pid)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "DECLARE @titlecid int, @emailcid int, @phonecid int, @signaturecid int\r\nSET @titlecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=443)\r\nSET @emailcid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=428)\r\nSET @phonecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=429)\r\nSET @signaturecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=99719)\r\n\r\nSELECT\tp.personid,p.firstname,p.lastname,p.student_no\r\n        ,pt.valbytesisencrypted AS TitleEncrypted,pt.valtext AS titletext\r\n        ,pe.valbytesisencrypted AS EmailEncrypted,pe.valtext AS emailtext\r\n        ,pp.valbytesisencrypted AS PhoneEncrypted,pp.valtext AS phonetext\r\n\t\t,pt.valbytes AS Title,pe.valbytes AS Email,pp.valbytes AS Phone,ps.controlvalue AS sig\r\nFROM\tpeople p LEFT JOIN perstudentdata2 pt ON pt.PersonID=p.PersonID AND pt.ControlID=@titlecid\r\n\t\tLEFT JOIN perstudentdata2 pe ON pe.PersonID=p.PersonID AND pe.ControlID=@emailcid\r\n\t\tLEFT JOIN perstudentdata2 pp ON pp.PersonID=p.PersonID AND pp.ControlID=@phonecid\r\n\t\tLEFT JOIN imageinfops ps ON ps.PersonID=p.PersonID AND ps.ControlID=@signaturecid\r\nWHERE\tp.PersonID=@pid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			Staff result;
			if (dataTable.Rows.Count > 0)
			{
				dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
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
				staff.Phone = Staff.GetDynamicDataValue(dataRow, tripleDES, "PhoneEncrypted", "Phone", "phonetext");
				staff.Email = Staff.GetDynamicDataValue(dataRow, tripleDES, "EmailEncrypted", "Email", "emailtext");
				staff.Title = Staff.GetDynamicDataValue(dataRow, tripleDES, "TitleEncrypted", "Title", "titletext");
				if (dataRow["sig"] == DBNull.Value)
				{
					staff.Signature = null;
				}
				else
				{
					string text;
					byte[] signature = DynamicScreen.ExtractImageBytes((byte[])dataRow["sig"], out text);
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

		// Token: 0x06000496 RID: 1174 RVA: 0x00015AC8 File Offset: 0x00014AC8
		private static string GetDynamicDataValue(DataRow dr, TripleDESEncryptionClass tripleDES, string valBytesIsEncryptedColname, string bytesColname, string textColname)
		{
			string result;
			if (dr[valBytesIsEncryptedColname] != DBNull.Value && Convert.ToBoolean(dr[valBytesIsEncryptedColname]) && dr[bytesColname] != DBNull.Value)
			{
				byte[] inputInBytes = (byte[])dr[bytesColname];
				result = tripleDES.Decrypt(inputInBytes);
			}
			else
			{
				result = dr[textColname].ToString();
			}
			return result;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015B30 File Offset: 0x00014B30
		public static Staff LoadStaffInfoFromAssignedCounsellorForStudent(int studentPid)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "DECLARE @counsellorcid int, @titlecid int, @emailcid int, @phonecid int, @signaturecid int\r\nSET @counsellorcid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=99671)\r\nSET @titlecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=443)\r\nSET @emailcid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=428)\r\nSET @phonecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=429)\r\nSET @signaturecid = (SELECT TOP 1 settingvalue FROM SettingsGroups WHERE settingCode=99719)\r\n\r\nSELECT\tmp.controlvalue AS personid,p.firstname,p.lastname,p.student_no\r\n        ,pt.valbytesisencrypted AS TitleEncrypted,pt.valtext AS titletext\r\n        ,pe.valbytesisencrypted AS EmailEncrypted,pe.valtext AS emailtext\r\n        ,pp.valbytesisencrypted AS PhoneEncrypted,pp.valtext AS phonetext\r\n\t\t,pt.valbytes AS Title,pe.valbytes AS Email,pp.valbytes AS Phone,ps.controlvalue AS sig\r\nFROM\tmaininfops mp LEFT JOIN people p ON p.personid=mp.controlvalue\r\n        LEFT JOIN perstudentdata2 pt ON pt.PersonID=p.PersonID AND pt.ControlID=@titlecid\r\n\t\tLEFT JOIN perstudentdata2 pe ON pe.PersonID=p.PersonID AND pe.ControlID=@emailcid\r\n\t\tLEFT JOIN perstudentdata2 pp ON pp.PersonID=p.PersonID AND pp.ControlID=@phonecid\r\n\t\tLEFT JOIN imageinfops ps ON ps.PersonID=p.PersonID AND ps.ControlID=@signaturecid\r\nWHERE\tmp.PersonID=@studentpid AND mp.controlid=@counsellorcid";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@studentpid", studentPid);
			da.Fill(dataTable);
			Staff result;
			if (dataTable.Rows.Count > 0)
			{
				dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
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
				staff.Phone = Staff.GetDynamicDataValue(dataRow, tripleDES, "PhoneEncrypted", "Phone", "phonetext");
				staff.Email = Staff.GetDynamicDataValue(dataRow, tripleDES, "EmailEncrypted", "Email", "emailtext");
				staff.Title = Staff.GetDynamicDataValue(dataRow, tripleDES, "TitleEncrypted", "Title", "titletext");
				if (dataRow["sig"] == DBNull.Value)
				{
					staff.Signature = null;
				}
				else
				{
					string text;
					byte[] signature = DynamicScreen.ExtractImageBytes((byte[])dataRow["sig"], out text);
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

		// Token: 0x06000498 RID: 1176 RVA: 0x00015D04 File Offset: 0x00014D04
		public static string LookupStaffSignatureBase64(int pid)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "DECLARE @sigcid INT\r\nSET @sigcid=(SELECT settingvalue AS titlecid FROM settingsgroups WHERE groupid=-1 AND settingcode=99719)\r\nSELECT controlvalue FROM imageinfops WHERE controlid=@sigcid AND personid=@pid";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			da.Fill(dataTable);
			string result;
			if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
			{
				byte[] array = (byte[])dataTable.Rows[0][0];
				string text;
				array = DynamicScreen.ExtractImageBytes(array, out text);
				result = Convert.ToBase64String(array);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015DE4 File Offset: 0x00014DE4
		public static string LookupStaffTitle(int pid, Settings settings, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int setting = OldUserSettingClientManager.CurrentInstance.GetSetting(443);
			return Staff.LookupStaffTextInfo(pid, setting, da, tripleDES);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015E10 File Offset: 0x00014E10
		public static string LookupStaffPhone(int pid, Settings settings, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int setting = OldUserSettingClientManager.CurrentInstance.GetSetting(429);
			return Staff.LookupStaffTextInfo(pid, setting, da, tripleDES);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015E3C File Offset: 0x00014E3C
		public static string LookupStaffTextInfo(int pid, int cid, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			string result;
			if (cid < 1)
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
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
				{
					byte[] bytes = (byte[])dataTable.Rows[0][0];
					object obj = dataTable.Rows[0]["setting3"];
					bool decrypt = obj != null && (int)obj == 1;
					result = ClockWorkCore.BytesToString(bytes, decrypt, tripleDES);
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
