using System;
using System.Collections.Generic;
using System.Data;

namespace ClockWorkWebAPI
{
	// Token: 0x0200000D RID: 13
	public class AvailabilityGroup
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000054CC File Offset: 0x000036CC
		public AvailabilityGroup(int id, string title, int durationMinutes, int colour)
		{
			this.id = id;
			this.title = title;
			this.durationMinutes = durationMinutes;
			this.colour = colour;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000054F4 File Offset: 0x000036F4
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000550C File Offset: 0x0000370C
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005524 File Offset: 0x00003724
		public int DurationMinutes
		{
			get
			{
				return this.durationMinutes;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000553C File Offset: 0x0000373C
		public int Colour
		{
			get
			{
				return this.colour;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005554 File Offset: 0x00003754
		public static List<AvailabilityGroup> ParseAvailabilityGroups(DataTable availabilityGroups, string groupIdColonDurationMinutesCommaSeparated)
		{
			string[] array = groupIdColonDurationMinutesCommaSeparated.Split(new char[]
			{
				','
			});
			List<AvailabilityGroup> list = new List<AvailabilityGroup>();
			foreach (string text in array)
			{
				int num = text.IndexOf(':');
				bool flag = num > 0;
				if (flag)
				{
					string s = text.Substring(0, num);
					string s2 = text.Substring(num + 1);
					int num2 = int.Parse(s);
					bool flag2 = false;
					foreach (object obj in availabilityGroups.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num3 = (int)dataRow["availabilitygroupid"];
						bool flag3 = num3 == num2;
						if (flag3)
						{
							string text2 = (string)dataRow["availabilitytitle"];
							int num4 = (int)dataRow["colour"];
							list.Add(new AvailabilityGroup(int.Parse(s), text2, int.Parse(s2), num4));
							flag2 = true;
							break;
						}
					}
					bool flag4 = !flag2;
					if (flag4)
					{
					}
				}
				else
				{
					list.Add(new AvailabilityGroup(int.Parse(text), "", 0, 0));
				}
			}
			return list;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000056C8 File Offset: 0x000038C8
		public static AvailabilityGroup FindAvailabilityGroup(List<AvailabilityGroup> availabilityGroups, int availabilityGroupId)
		{
			foreach (AvailabilityGroup availabilityGroup in availabilityGroups)
			{
				bool flag = availabilityGroup.Id == availabilityGroupId;
				if (flag)
				{
					return availabilityGroup;
				}
			}
			return null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000572C File Offset: 0x0000392C
		public static int FindDurationMinutes(List<AvailabilityGroup> groups, AvailabilityScheduleRange range)
		{
			foreach (AvailabilityGroup availabilityGroup in groups)
			{
				bool flag = availabilityGroup.Id == range.AvailabilityGroupId && availabilityGroup.DurationMinutes > 0;
				if (flag)
				{
					return availabilityGroup.DurationMinutes;
				}
			}
			return 60;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000057A8 File Offset: 0x000039A8
		public static string GetIdsCommaSeparated(List<AvailabilityGroup> groups)
		{
			string text = "";
			for (int i = 0; i < groups.Count; i++)
			{
				bool flag = i > 0;
				if (flag)
				{
					text += ",";
				}
				text += groups[i].Id.ToString();
			}
			return text;
		}

		// Token: 0x04000032 RID: 50
		private int id;

		// Token: 0x04000033 RID: 51
		private string title;

		// Token: 0x04000034 RID: 52
		private int durationMinutes;

		// Token: 0x04000035 RID: 53
		private int colour;
	}
}
