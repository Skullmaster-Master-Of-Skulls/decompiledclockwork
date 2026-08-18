using System;
using System.Collections.Generic;
using System.Data;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x02000028 RID: 40
	public class DataSyncTimetableItem
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0003B07C File Offset: 0x0003A07C
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0003B093 File Offset: 0x0003A093
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0003B09C File Offset: 0x0003A09C
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0003B0B3 File Offset: 0x0003A0B3
		public int StartMinutes { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0003B0BC File Offset: 0x0003A0BC
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0003B0D3 File Offset: 0x0003A0D3
		public int EndMinutes { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0003B0DC File Offset: 0x0003A0DC
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0003B0F3 File Offset: 0x0003A0F3
		public string Room { get; set; }

		// Token: 0x060002C7 RID: 711 RVA: 0x0003B0FC File Offset: 0x0003A0FC
		public override string ToString()
		{
			DateTime dateTime = new DateTime(2000, 1, 1);
			DateTime dateTime2 = new DateTime(2000, 1, 1);
			dateTime = dateTime.AddMinutes((double)this.StartMinutes);
			dateTime2 = dateTime2.AddMinutes((double)this.EndMinutes);
			return string.Format("{0} . {1} to {2}", this.DayOfWeek.ToString(), dateTime.ToString("h:mm tt"), dateTime2.ToString("h:mm tt"));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0003B17C File Offset: 0x0003A17C
		public static List<DataSyncTimetableItem> ParseTimeTableItems(DataRow dr)
		{
			List<DataSyncTimetableItem> list = new List<DataSyncTimetableItem>();
			string[] array = new string[]
			{
				"sun",
				"mon",
				"tue",
				"wed",
				"thu",
				"fri",
				"sat"
			};
			for (int i = 0; i < array.Length; i++)
			{
				string str = array[i];
				string columnName = str + "startminutes";
				string columnName2 = str + "endminutes";
				int num = (dr[columnName] == DBNull.Value) ? 0 : ((int)dr[columnName]);
				int num2 = (dr[columnName2] == DBNull.Value) ? 0 : ((int)dr[columnName2]);
				if (num > 0 || num2 > 0)
				{
					list.Add(new DataSyncTimetableItem
					{
						DayOfWeek = (DayOfWeek)i,
						StartMinutes = num,
						EndMinutes = num2,
						Room = dr[str + "room"].ToString()
					});
				}
			}
			return list;
		}
	}
}
