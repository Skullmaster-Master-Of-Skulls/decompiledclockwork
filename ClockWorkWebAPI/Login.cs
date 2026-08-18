using System;
using System.Data;
using System.Globalization;
using EncryptionClassLibrary;

namespace ClockWorkWebAPI
{
	// Token: 0x0200001B RID: 27
	public class Login
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000BD78 File Offset: 0x00009F78
		public static CultureInfo GetCurrentCultureInfo(object languagePersistVar)
		{
			string text = (languagePersistVar == null) ? "en-us" : languagePersistVar.ToString().Trim();
			bool flag = text.CompareTo("fr-CA") == 0;
			CultureInfo result;
			if (flag)
			{
				result = new CultureInfo("fr-CA");
			}
			else
			{
				result = new CultureInfo("en-us");
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public static Person LoginUser(db conn, IEncryption tripleDES, string student_no)
		{
			return Login.LoginUser(conn, tripleDES, student_no, "");
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public static Person LoginUser(db conn, IEncryption tripleDES, string student_no, string _email)
		{
			byte[] value = tripleDES.Encrypt(student_no.ToUpper());
			conn.Da.SelectCommand.CommandText = "SELECT personid,firstname,lastname FROM people WHERE student_no=@sne AND isactive=1";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@sne", value);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			bool flag = dataTable.Rows.Count > 0;
			Person result;
			if (flag)
			{
				int num = (int)dataTable.Rows[0][0];
				byte[] bytes = (byte[])dataTable.Rows[0]["firstname"];
				string name = Core.BytesToString(bytes, true, tripleDES);
				bool flag2 = _email.Trim().Length > 0;
				string email;
				if (flag2)
				{
					email = _email;
				}
				else
				{
					conn.Da.SelectCommand.CommandText = "SELECT mi.controlvalue,dc.setting3,dc.controlid FROM otherinfops mi LEFT JOIN dynamiccontrols dc ON dc.controlid=mi.controlid WHERE mi.personid=@pid AND mi.controlid IN (SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260 UNION SELECT CAST(settingstringvalue AS int) AS settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=260)";
					conn.Da.SelectCommand.Parameters.Clear();
					conn.Da.SelectCommand.Parameters.AddWithValue("@pid", num);
					dataTable = new DataTable();
					conn.Da.Fill(dataTable);
					bool flag3 = dataTable.Rows.Count > 0;
					if (flag3)
					{
						byte[] bytes2 = (byte[])dataTable.Rows[0]["controlvalue"];
						int num2 = (int)dataTable.Rows[0]["setting3"];
						bool flag4 = num2 == 1;
						if (flag4)
						{
							email = Core.BytesToString(bytes2, true, tripleDES);
						}
						else
						{
							email = Core.BytesToString(bytes2, false, null);
						}
					}
					else
					{
						email = "";
					}
				}
				result = new Person(num, name, email)
				{
					StudentNumber = student_no
				};
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
