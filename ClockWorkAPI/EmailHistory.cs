using System;
using System.Collections.Generic;
using System.Data;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200008D RID: 141
	public class EmailHistory
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x00026224 File Offset: 0x00025224
		public static bool AddLogEntry(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string emailTypeCode, int personId, int templateId, int sentBy, string ebody, bool successful, string enote, int lucid, int infoPcId, List<int> lucids)
		{
			string commandText = "INSERT INTO emailhistory (personid,templateid,datesent,sentby,ebody,enote,successful,infopcid,emailtypecode,lucourseid) VALUES (@personid,@templateid,getdate(),@sentby,@ebody,@enote,@successful,@infopcid,@emailtypecode,@lucourseid)";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			EmailHistory.AddParameterInt(da, "@personid", personId);
			EmailHistory.AddParameterInt(da, "@templateid", templateId);
			EmailHistory.AddParameterInt(da, "@sentby", sentBy);
			EmailHistory.AddParameterInt(da, "@infopcid", infoPcId);
			EmailHistory.AddParameterInt(da, "@lucourseid", lucid);
			da.SelectCommand.Parameters.Add("@ebody", ClockWorkCore.base64Encode(tripleDES.Encrypt(ebody)));
			da.SelectCommand.Parameters.Add("@enote", ClockWorkCore.base64Encode(tripleDES.Encrypt(enote)));
			da.SelectCommand.Parameters.Add("@successful", successful);
			da.SelectCommand.Parameters.Add("@emailtypecode", emailTypeCode);
			string value;
			da.Fill(new DataTable(), out value);
			bool result;
			if (string.IsNullOrEmpty(value))
			{
				List<int> list = new List<int>();
				list.Add(lucid);
				foreach (int num in lucids)
				{
					if (!list.Contains(num))
					{
						list.Add(num);
						da.SelectCommand.CommandText = commandText;
						da.SelectCommand.Parameters.Clear();
						EmailHistory.AddParameterInt(da, "@personid", personId);
						EmailHistory.AddParameterInt(da, "@templateid", templateId);
						EmailHistory.AddParameterInt(da, "@sentby", sentBy);
						EmailHistory.AddParameterInt(da, "@infopcid", infoPcId);
						EmailHistory.AddParameterInt(da, "@lucourseid", num);
						da.SelectCommand.Parameters.Add("@ebody", "");
						da.SelectCommand.Parameters.Add("@enote", "");
						da.SelectCommand.Parameters.Add("@successful", successful);
						da.SelectCommand.Parameters.Add("@emailtypecode", emailTypeCode);
						da.Fill(new DataTable(), out value);
						if (!string.IsNullOrEmpty(value))
						{
							return false;
						}
					}
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000264B0 File Offset: 0x000254B0
		private static void AddParameterInt(UnivDataAdapter da, string pname, int val)
		{
			if (val > 0)
			{
				da.SelectCommand.Parameters.Add(pname, val);
			}
			else
			{
				da.SelectCommand.CommandText = da.SelectCommand.CommandText.Replace(pname, "NULL");
			}
		}
	}
}
