using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Databases;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000074 RID: 116
	public class TimeTableItem
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000271F4 File Offset: 0x000253F4
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0002720C File Offset: 0x0002540C
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0002721C File Offset: 0x0002541C
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00027234 File Offset: 0x00025434
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

		// Token: 0x060005DB RID: 1499 RVA: 0x00027244 File Offset: 0x00025444
		public bool IsTimeTableValidForDate(DateTime date)
		{
			bool flag = this.courseStartDate == DateTime.MinValue || this.courseEndDate == DateTime.MinValue;
			return flag || (date >= this.courseStartDate && date < this.courseEndDate.AddDays(1.0));
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000272B0 File Offset: 0x000254B0
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x000272C8 File Offset: 0x000254C8
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

		// Token: 0x060005DE RID: 1502 RVA: 0x000272D4 File Offset: 0x000254D4
		public TimeTableItem(int luCourseId, DayOfWeek dayOfWeek, int startMinutes, int endMinutes, string location)
		{
			this.luCourseId = luCourseId;
			this.dayOfWeek = dayOfWeek;
			this.startMinutes = startMinutes;
			this.endMinutes = endMinutes;
			this.location = location;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0002732C File Offset: 0x0002552C
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
				bool flag2 = dr[columnName] != DBNull.Value && dr[columnName2] != DBNull.Value;
				if (flag2)
				{
					int num = (int)dr[columnName];
					int num2 = (int)dr[columnName2];
					string text2 = (dr.Table == null || !dr.Table.Columns.Contains(text) || dr[text] == DBNull.Value) ? "" : ((string)dr[text]);
					DayOfWeek dayOfWeek = (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), i);
					TimeTableItem timeTableItem = new TimeTableItem((dr.Table == null || !dr.Table.Columns.Contains("lucourseid") || dr["lucourseid"] == DBNull.Value) ? 0 : ((int)dr["lucourseid"]), dayOfWeek, num, num2, text2);
					timeTableItem.TimetableId = ((dr.Table == null || !dr.Table.Columns.Contains("timetableid") || dr["timetableid"] == DBNull.Value) ? 0 : ((int)dr["timetableid"]));
					bool flag3 = flag && dr["startdate"] != DBNull.Value && dr["enddate"] != DBNull.Value;
					if (flag3)
					{
						bool flag4 = dr.Table.Columns["startdate"].DataType != typeof(DateTime);
						if (flag4)
						{
							DateTime dateTime;
							DateTime dateTime2;
							bool flag5 = DateTime.TryParse(dr["startdate"].ToString(), out dateTime) && DateTime.TryParse(dr["enddate"].ToString(), out dateTime2);
							if (flag5)
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

		// Token: 0x060005E0 RID: 1504 RVA: 0x000275F0 File Offset: 0x000257F0
		public void DeleteFromDatabase(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "DELETE FROM timetable WHERE timetableid=@id";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@id", this.timetableId);
			da.Fill(new DataTable());
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00027650 File Offset: 0x00025850
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00027668 File Offset: 0x00025868
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00027674 File Offset: 0x00025874
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0002768C File Offset: 0x0002588C
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

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00027698 File Offset: 0x00025898
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000276F8 File Offset: 0x000258F8
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00027710 File Offset: 0x00025910
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

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0002771C File Offset: 0x0002591C
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00027734 File Offset: 0x00025934
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00027740 File Offset: 0x00025940
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00027758 File Offset: 0x00025958
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

		// Token: 0x060005EC RID: 1516 RVA: 0x00027764 File Offset: 0x00025964
		public bool Overlaps(DateTime compareStartTime, DateTime compareEndTime)
		{
			bool flag = this.dayOfWeek == compareStartTime.DayOfWeek;
			bool result;
			if (flag)
			{
				int num = compareStartTime.Hour * 60 + compareStartTime.Minute;
				int num2 = compareEndTime.Hour * 60 + compareEndTime.Minute;
				bool flag2 = num < this.endMinutes && num2 > this.startMinutes;
				result = flag2;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000277D8 File Offset: 0x000259D8
		public bool Overlaps(DayOfWeek dayOfWeek, int smins, int emins)
		{
			bool flag = dayOfWeek == this.dayOfWeek;
			bool result;
			if (flag)
			{
				bool flag2 = smins < this.endMinutes && emins > this.startMinutes;
				result = flag2;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002781C File Offset: 0x00025A1C
		public static bool Overlaps(List<TimeTableItem> items, DateTime startTime, DateTime endTime)
		{
			foreach (TimeTableItem timeTableItem in items)
			{
				bool flag = timeTableItem.IsTimeTableValidForDate(startTime.Date) && timeTableItem.Overlaps(startTime, endTime);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00027890 File Offset: 0x00025A90
		public static bool Overlaps(List<TimeTableItem> items, DayOfWeek dayOfWeek, int smins, int emins)
		{
			foreach (TimeTableItem timeTableItem in items)
			{
				bool flag = timeTableItem.Overlaps(dayOfWeek, smins, emins);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000278F4 File Offset: 0x00025AF4
		public static void AddToDatabase(List<TimeTableItem> ties, UnivDataAdapter da, int lucid)
		{
			List<List<TimeTableItem>> list = new List<List<TimeTableItem>>();
			using (List<TimeTableItem>.Enumerator enumerator = ties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TimeTableItem tti = enumerator.Current;
					bool flag = false;
					Predicate<TimeTableItem> <>9__0;
					for (int i = 0; i < list.Count; i++)
					{
						List<TimeTableItem> list2 = list[i];
						Predicate<TimeTableItem> match;
						if ((match = <>9__0) == null)
						{
							match = (<>9__0 = ((TimeTableItem item) => item.DayOfWeek == tti.DayOfWeek));
						}
						TimeTableItem timeTableItem = list2.Find(match);
						bool flag2 = timeTableItem == null;
						if (flag2)
						{
							list[i].Add(tti);
							flag = true;
							break;
						}
					}
					bool flag3 = !flag;
					if (flag3)
					{
						list.Add(new List<TimeTableItem>
						{
							tti
						});
					}
				}
			}
			foreach (List<TimeTableItem> ties2 in list)
			{
				TimeTableItem.AddToDatabaseOneRow(ties2, da, lucid);
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00027A3C File Offset: 0x00025C3C
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
			for (int j = 0; j < TimeTableItem.daysOfWeek.Length; j++)
			{
				string text = "@" + TimeTableItem.daysOfWeek[j] + "startminutes";
				string text2 = "@" + TimeTableItem.daysOfWeek[j] + "endminutes";
				string value = "@" + TimeTableItem.daysOfWeek[j] + "room";
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
			for (int k = 0; k < TimeTableItem.daysOfWeek.Length; k++)
			{
				bool flag = !list.Contains(k);
				if (flag)
				{
					da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[k] + "startminutes", 0);
					da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[k] + "endminutes", 0);
					da.SelectCommand.Parameters.Add("@" + TimeTableItem.daysOfWeek[k] + "room", "");
				}
			}
			string text3;
			da.Fill(new DataTable(), out text3);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00027DF8 File Offset: 0x00025FF8
		public static List<TimeTableItem> LoadCourseTimetable(int lucid, UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT * FROM timetable WHERE lucourseid=@lucid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucid", lucid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool flag = dataTable.Rows.Count > 0;
			List<TimeTableItem> result;
			if (flag)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dr = (DataRow)obj;
					List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(dr);
					bool flag2 = timetableItems.Count > 0;
					if (flag2)
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

		// Token: 0x060005F3 RID: 1523 RVA: 0x00027EE4 File Offset: 0x000260E4
		public static List<TimeTableItem> LoadCourseTimetable(int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT * FROM timetable WHERE lucourseid=@lucid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			});
			bool flag = dataTable.Rows.Count > 0;
			List<TimeTableItem> result;
			if (flag)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dr = (DataRow)obj;
					List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(dr);
					bool flag2 = timetableItems.Count > 0;
					if (flag2)
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

		// Token: 0x060005F4 RID: 1524 RVA: 0x00027FC0 File Offset: 0x000261C0
		public static string TimeTableItemsToString(List<TimeTableItem> items)
		{
			bool flag = items == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", items.ConvertAll<string>((TimeTableItem t1) => string.Format("{0} {1}", t1.DayOfWeek.ToString(), t1.TimeDescription)).ToArray());
			}
			return result;
		}

		// Token: 0x04000320 RID: 800
		private int startMinutes;

		// Token: 0x04000321 RID: 801
		private int endMinutes;

		// Token: 0x04000322 RID: 802
		private string location;

		// Token: 0x04000323 RID: 803
		private int luCourseId;

		// Token: 0x04000324 RID: 804
		private DayOfWeek dayOfWeek;

		// Token: 0x04000325 RID: 805
		private int timetableId = 0;

		// Token: 0x04000326 RID: 806
		private DateTime courseStartDate = DateTime.MinValue;

		// Token: 0x04000327 RID: 807
		private DateTime courseEndDate = DateTime.MinValue;

		// Token: 0x04000328 RID: 808
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
