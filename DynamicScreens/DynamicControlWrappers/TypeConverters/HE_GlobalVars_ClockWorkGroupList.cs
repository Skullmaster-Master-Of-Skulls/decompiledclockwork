using System;
using System.Data;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200002C RID: 44
	public class HE_GlobalVars_ClockWorkGroupList
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001E830 File Offset: 0x0001D830
		public static string[] _ListOfGroups
		{
			get
			{
				string[] result;
				if (HE_GlobalVars_ClockWorkGroupList.groupsTable != null)
				{
					string[] array = new string[HE_GlobalVars_ClockWorkGroupList.groupsTable.Rows.Count + 1];
					array[0] = "";
					for (int i = 0; i < HE_GlobalVars_ClockWorkGroupList.groupsTable.Rows.Count; i++)
					{
						DataRow dr = HE_GlobalVars_ClockWorkGroupList.groupsTable.Rows[i];
						array[i + 1] = HE_GlobalVars_ClockWorkGroupList.GetDisplayString(dr);
					}
					result = array;
				}
				else
				{
					result = new string[]
					{
						""
					};
				}
				return result;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001E8C8 File Offset: 0x0001D8C8
		public static string GetDisplayString(DataRow dr)
		{
			return dr["description"].ToString() + " . " + dr["groupid"].ToString();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001E904 File Offset: 0x0001D904
		public static void GetLookupGroupIdAndDescriptionFromDisplayString(string displayString, out int lookupGroupId, out string description)
		{
			if (displayString.Length > 0)
			{
				int num = displayString.LastIndexOf('.');
				if (num > 0)
				{
					description = displayString.Substring(0, num).Trim();
					string s = displayString.Substring(num + 1).Trim();
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
					lookupGroupId = 0;
					description = "";
				}
			}
			else
			{
				lookupGroupId = 0;
				description = displayString;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001E998 File Offset: 0x0001D998
		private static DataRow FindDataRow(int lookupGroupId)
		{
			DataRow result;
			if (HE_GlobalVars_ClockWorkGroupList.groupsTable == null)
			{
				result = null;
			}
			else
			{
				foreach (object obj in HE_GlobalVars_ClockWorkGroupList.groupsTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow["groupid"];
					if (num == lookupGroupId)
					{
						return dataRow;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001EA40 File Offset: 0x0001DA40
		public static string FindDisplayString(int groupId)
		{
			DataRow dataRow = HE_GlobalVars_ClockWorkGroupList.FindDataRow(groupId);
			string result;
			if (dataRow == null)
			{
				result = "";
			}
			else
			{
				result = HE_GlobalVars_ClockWorkGroupList.GetDisplayString(dataRow);
			}
			return result;
		}

		// Token: 0x040001CB RID: 459
		public static DataTable groupsTable = null;
	}
}
