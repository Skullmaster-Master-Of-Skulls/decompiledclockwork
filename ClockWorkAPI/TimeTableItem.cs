using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000073 RID: 115
	public class TimeTableItem
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001F080 File Offset: 0x0001E080
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0001F098 File Offset: 0x0001E098
		public DateTime CourseStartDate
		{
			get
			{
				return this.courseStartDate;
			}
			set
			{
				this.courseStartDate = value.Date;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001F0A8 File Offset: 0x0001E0A8
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0001F0C0 File Offset: 0x0001E0C0
		public DateTime CourseEndDate
		{
			get
			{
				return this.courseEndDate;
			}
			set
			{
				this.courseEndDate = value.Date;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001F0D0 File Offset: 0x0001E0D0
		public bool IsTimeTableValidForDate(DateTime date)
		{
			return this.courseStartDate == DateTime.MinValue || this.courseEndDate == DateTime.MinValue || (date >= this.courseStartDate && date < this.courseEndDate.AddDays(1.0));
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001F140 File Offset: 0x0001E140
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0001F158 File Offset: 0x0001E158
		public int TimetableId
		{
			get
			{
				return this.timetableId;
			}
			set
			{
				this.timetableId = value;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001F164 File Offset: 0x0001E164
		public TimeTableItem(int luCourseId, DayOfWeek dayOfWeek, int startMinutes, int endMinutes, string location)
		{
			this.luCourseId = luCourseId;
			this.dayOfWeek = dayOfWeek;
			this.startMinutes = startMinutes;
			this.endMinutes = endMinutes;
			this.location = location;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001F1BC File Offset: 0x0001E1BC
		public static List<TimeTableItem> GetTimetableItems(DataRow dr)
		{
			bool flag = dr.Table != null && dr.Table.Columns.Contains("startdate");
			List<TimeTableItem> list = new List<TimeTableItem>();
			for (int i = 0; i < TimeTableItem.daysOfWeek.Length; i++)
			{
				string str = TimeTableItem.daysOfWeek[i];
				string columnName = str + "startminutes";
				string columnName2 = str + "endminutes";
				string text = str + "room";
				if (dr[columnName] != DBNull.Value && dr[columnName2] != DBNull.Value)
				{
					int num = (int)dr[columnName];
					int num2 = (int)dr[columnName2];
					string text2 = (dr.Table == null || !dr.Table.Columns.Contains(text) || dr[text] == DBNull.Value) ? "" : ((string)dr[text]);
					DayOfWeek dayOfWeek = (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), i);
					TimeTableItem timeTableItem = new TimeTableItem((dr.Table == null || !dr.Table.Columns.Contains("lucourseid") || dr["lucourseid"] == DBNull.Value) ? 0 : ((int)dr["lucourseid"]), dayOfWeek, num, num2, text2);
					timeTableItem.TimetableId = ((dr.Table == null || !dr.Table.Columns.Contains("timetableid") || dr["timetableid"] == DBNull.Value) ? 0 : ((int)dr["timetableid"]));
					if (flag && dr["startdate"] != DBNull.Value && dr["enddate"] != DBNull.Value)
					{
						if (dr.Table.Columns["startdate"].DataType != typeof(DateTime))
						{
							DateTime dateTime;
							DateTime dateTime2;
							if (DateTime.TryParse(dr["startdate"].ToString(), out dateTime) && DateTime.TryParse(dr["enddate"].ToString(), out dateTime2))
							{
								timeTableItem.CourseStartDate = dateTime;
								timeTableItem.CourseEndDate = dateTime2;
							}
						}
						else
						{
							timeTableItem.CourseStartDate = (DateTime)dr["startdate"];
							timeTableItem.CourseEndDate = (DateTime)dr["enddate"];
						}
					}
					list.Add(timeTableItem);
				}
			}
			return list;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001F47C File Offset: 0x0001E47C
		public void DeleteFromDatabase(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "DELETE FROM timetable WHERE timetableid=@id";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@id", this.timetableId);
			da.Fill(new DataTable());
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001F4DC File Offset: 0x0001E4DC
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0001F4F4 File Offset: 0x0001E4F4
		public int StartMinutes
		{
			get
			{
				return this.startMinutes;
			}
			set
			{
				this.startMinutes = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001F500 File Offset: 0x0001E500
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001F518 File Offset: 0x0001E518
		public int EndMinutes
		{
			get
			{
				return this.endMinutes;
			}
			set
			{
				this.endMinutes = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0001F524 File Offset: 0x0001E524
		public string TimeDescription
		{
			get
			{
				DateTime dateTime = new DateTime(2000, 1, 1);
				DateTime dateTime2 = dateTime.AddMinutes((double)this.startMinutes);
				DateTime dateTime3 = dateTime.AddMinutes((double)this.endMinutes);
				return string.Format("{0} to {1}", dateTime2.ToString("h:mm tt"), dateTime3.ToString("h:mm tt"));
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001F588 File Offset: 0x0001E588
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0001F5A0 File Offset: 0x0001E5A0
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001F5AC File Offset: 0x0001E5AC
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0001F5C4 File Offset: 0x0001E5C4
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.dayOfWeek;
			}
			set
			{
				this.dayOfWeek = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001F5D0 File Offset: 0x0001E5D0
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0001F5E8 File Offset: 0x0001E5E8
		public int LuCourseId
		{
			get
			{
				return this.luCourseId;
			}
			set
			{
				this.luCourseId = value;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001F5F4 File Offset: 0x0001E5F4
		public bool Overlaps(DateTime compareStartTime, DateTime compareEndTime)
		{
			bool result;
			if (this.dayOfWeek == compareStartTime.DayOfWeek)
			{
				int num = compareStartTime.Hour * 60 + compareStartTime.Minute;
				int num2 = compareEndTime.Hour * 60 + compareEndTime.Minute;
				result = (num < this.endMinutes && num2 > this.startMinutes);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001F668 File Offset: 0x0001E668
		public bool Overlaps(DayOfWeek dayOfWeek, int smins, int emins)
		{
			return dayOfWeek == this.dayOfWeek && (smins < this.endMinutes && emins > this.startMinutes);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001F6B0 File Offset: 0x0001E6B0
		public static bool Overlaps(List<TimeTableItem> items, DateTime startTime, DateTime endTime)
		{
			foreach (TimeTableItem timeTableItem in items)
			{
				if (timeTableItem.IsTimeTableValidForDate(startTime.Date) && timeTableItem.Overlaps(startTime, endTime))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001F72C File Offset: 0x0001E72C
		public static bool Overlaps(List<TimeTableItem> items, DayOfWeek dayOfWeek, int smins, int emins)
		{
			foreach (TimeTableItem timeTableItem in items)
			{
				if (timeTableItem.Overlaps(dayOfWeek, smins, emins))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001F7C4 File Offset: 0x0001E7C4
		public static void AddToDatabase(List<TimeTableItem> ties, UnivDataAdapter da, int lucid)
		{
			List<List<TimeTableItem>> list = new List<List<TimeTableItem>>();
			TimeTableItem tti;
			foreach (TimeTableItem tti2 in ties)
			{
				tti = tti2;
				bool flag = false;
				for (int i = 0; i < list.Count; i++)
				{
					TimeTableItem timeTableItem = list[i].Find((TimeTableItem item) => item.DayOfWeek == tti.DayOfWeek);
					if (timeTableItem == null)
					{
						list[i].Add(tti);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(new List<TimeTableItem>
					{
						tti
					});
				}
			}
			foreach (List<TimeTableItem> ties2 in list)
			{
				TimeTableItem.AddToDatabaseOneRow(ties2, da, lucid);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001F90C File Offset: 0x0001E90C
		private static void AddToDatabaseOneRow(List<TimeTableItem> ties, UnivDataAdapter da, int lucid)
		{
			StringBuilder stringBuilder = new StringBuilder("INSERT INTO timetable (");
			for (int i = 0; i < TimeTableItem.daysOfWeek.Length; i++)
			{
				stringBuilder.Append(TimeTableItem.daysOfWeek[i]);
				stringBuilder.Append("startminutes");
				stringBuilder.Append(",");
				stringBuilder.Append(TimeTableItem.daysOfWeek[i]);
				stringBuilder.Append("endminutes");
				stringBuilder.Append(",");
				stringBuilder.Append(TimeTableItem.daysOfWeek[i]);
				stringBuilder.Append("room,");
			}
			stringBuilder.Append("timetabletype,lucourseid) SELECT ");
			for (int i = 0; i < TimeTableItem.daysOfWeek.Length; i++)
			{
				string text = "@" + TimeTableItem.daysOfWeek[i] + "startminutes";
				string text2 = "@" + TimeTableItem.daysOfWeek[i] + "endminutes";
				string value = "@" + TimeTableItem.daysOfWeek[i] + "room";
				stringBuilder.Append(string.Concat(new string[]
				{
					"CASE ",
					text,
					" WHEN 0 THEN NULL ELSE ",
					text,
					" END"
				}));
				stringBuilder.Append(",");
				stringBuilder.Append(string.Concat(new string[]
				{
					"CASE ",
					text2,
					" WHEN 0 THEN NULL ELSE ",
					text2,
					" END"
				}));
				stringBuilder.Append(",");
				stringBuilder.Append(value);
				stringBuilder.Append(",");
			}
			stringBuilder.Append("'C',@lucid");
			da.SelectCommand.CommandText = stringBuilder.ToString();
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucid", lucid);
			List<int> list = new List<int>();
			foreach (TimeTableItem timeTableItem in ties)
			{
				int num = (int)timeTableItem.DayOfWeek;
				list.Add(num);
				da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[num] + "startminutes", timeTableItem.StartMinutes);
				da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[num] + "endminutes", timeTableItem.EndMinutes);
				da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[num] + "room", timeTableItem.Location);
			}
			for (int i = 0; i < TimeTableItem.daysOfWeek.Length; i++)
			{
				if (!list.Contains(i))
				{
					da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[i] + "startminutes", 0);
					da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[i] + "endminutes", 0);
					da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[i] + "room", "");
				}
			}
			string text3;
			da.Fill(new DataTable(), out text3);
			if (text3 != null && text3.Length > 0)
			{
				MessageBox.Show(text3);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001FCEC File Offset: 0x0001ECEC
		public static List<TimeTableItem> LoadCourseTimetable(int lucid, UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT * FROM timetable WHERE lucourseid=@lucid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucid", lucid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			List<TimeTableItem> result;
			if (dataTable.Rows.Count > 0)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dr = (DataRow)obj;
					List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(dr);
					if (timetableItems.Count > 0)
					{
						return timetableItems;
					}
				}
				result = new List<TimeTableItem>();
			}
			else
			{
				result = new List<TimeTableItem>();
			}
			return result;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001FDEC File Offset: 0x0001EDEC
		public static List<TimeTableItem> LoadCourseTimetable(int lucid)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			string commandText = "SELECT * FROM timetable WHERE lucourseid=@lucid";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucid", lucid);
			da.Fill(dataTable);
			List<TimeTableItem> result;
			if (dataTable.Rows.Count > 0)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dr = (DataRow)obj;
					List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(dr);
					if (timetableItems.Count > 0)
					{
						return timetableItems;
					}
				}
				result = new List<TimeTableItem>();
			}
			else
			{
				result = new List<TimeTableItem>();
			}
			return result;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001FF34 File Offset: 0x0001EF34
		public static string TimeTableItemsToString(List<TimeTableItem> items)
		{
			string result;
			if (items == null)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", items.ConvertAll<string>((TimeTableItem t1) => string.Format("{0} {1}", t1.DayOfWeek.ToString(), t1.TimeDescription)).ToArray());
			}
			return result;
		}

		// Token: 0x04000302 RID: 770
		private int startMinutes;

		// Token: 0x04000303 RID: 771
		private int endMinutes;

		// Token: 0x04000304 RID: 772
		private string location;

		// Token: 0x04000305 RID: 773
		private int luCourseId;

		// Token: 0x04000306 RID: 774
		private DayOfWeek dayOfWeek;

		// Token: 0x04000307 RID: 775
		private int timetableId = 0;

		// Token: 0x04000308 RID: 776
		private DateTime courseStartDate = DateTime.MinValue;

		// Token: 0x04000309 RID: 777
		private DateTime courseEndDate = DateTime.MinValue;

		// Token: 0x0400030A RID: 778
		private static string[] daysOfWeek = new string[]
		{
			"sun",
			"mon",
			"tue",
			"wed",
			"thu",
			"fri",
			"sat"
		};
	}
}
