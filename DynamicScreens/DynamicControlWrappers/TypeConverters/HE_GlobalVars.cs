using System;
using System.Data;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x02000020 RID: 32
	public class HE_GlobalVars
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00019424 File Offset: 0x00018424
		public static string[] _ListOfRules
		{
			get
			{
				if (HE_GlobalVars.listGroups == null)
				{
					HE_GlobalVars.listGroups = new DataTable();
				}
				string[] array = new string[HE_GlobalVars.listGroups.Rows.Count + 1];
				array[0] = "";
				for (int i = 0; i < HE_GlobalVars.listGroups.Rows.Count; i++)
				{
					array[i + 1] = HE_GlobalVars.GetGroupDisplayString(HE_GlobalVars.listGroups.Rows[i]);
				}
				return array;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000194AC File Offset: 0x000184AC
		private static string GetGroupDisplayString(DataRow dr)
		{
			return dr["description"].ToString() + "." + dr["groupid"].ToString();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000194E8 File Offset: 0x000184E8
		public static void GetLookupGroupIdAndDescriptionFromDisplayString(string displayString, out int lookupGroupId, out string description)
		{
			if (displayString.Length > 0)
			{
				int num = displayString.LastIndexOf('.');
				if (num > 0)
				{
					description = displayString.Substring(0, num);
					string s = displayString.Substring(num + 1);
					try
					{
						lookupGroupId = int.Parse(s);
					}
					catch
					{
						lookupGroupId = 0;
					}
				}
				else
				{
					description = "";
					lookupGroupId = 0;
				}
			}
			else
			{
				lookupGroupId = 0;
				description = displayString;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00019570 File Offset: 0x00018570
		private static DataRow FindDataRow(int lookupGroupId)
		{
			if (HE_GlobalVars.listGroups != null)
			{
				foreach (object obj in HE_GlobalVars.listGroups.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow["groupid"];
					if (num == lookupGroupId)
					{
						return dataRow;
					}
				}
			}
			return null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00019610 File Offset: 0x00018610
		public static string FindDisplayString(int lookupGroupId)
		{
			DataRow dataRow = HE_GlobalVars.FindDataRow(lookupGroupId);
			string result;
			if (dataRow == null)
			{
				result = "";
			}
			else
			{
				result = HE_GlobalVars.GetGroupDisplayString(dataRow);
			}
			return result;
		}

		// Token: 0x04000158 RID: 344
		public static DataTable listGroups;
	}
}
