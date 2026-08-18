using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200054C RID: 1356
	[Serializable]
	public class TimeTableItem
	{
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000306E8 File Offset: 0x0002E8E8
		// (set) Token: 0x06002BAB RID: 11179 RVA: 0x00030700 File Offset: 0x0002E900
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

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x00030710 File Offset: 0x0002E910
		// (set) Token: 0x06002BAD RID: 11181 RVA: 0x00030728 File Offset: 0x0002E928
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

		// Token: 0x06002BAE RID: 11182 RVA: 0x00030738 File Offset: 0x0002E938
		public bool IsTimeTableValidForDate(DateTime date)
		{
			bool flag = this.courseStartDate == DateTime.MinValue || this.courseEndDate == DateTime.MinValue;
			return flag || (date >= this.courseStartDate && date < this.courseEndDate.AddDays(1.0));
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000307A4 File Offset: 0x0002E9A4
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x000307BC File Offset: 0x0002E9BC
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

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000307C8 File Offset: 0x0002E9C8
		public TimeTableItem(int luCourseId, DayOfWeek dayOfWeek, int startMinutes, int endMinutes, string location)
		{
			this.luCourseId = luCourseId;
			this.dayOfWeek = dayOfWeek;
			this.startMinutes = startMinutes;
			this.endMinutes = endMinutes;
			this.location = location;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00030820 File Offset: 0x0002EA20
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

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00030AE4 File Offset: 0x0002ECE4
		// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x00030AFC File Offset: 0x0002ECFC
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

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x00030B08 File Offset: 0x0002ED08
		// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x00030B20 File Offset: 0x0002ED20
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

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x00030B2C File Offset: 0x0002ED2C
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

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x00030B8C File Offset: 0x0002ED8C
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x00030BA4 File Offset: 0x0002EDA4
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

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x00030BB0 File Offset: 0x0002EDB0
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x00030BC8 File Offset: 0x0002EDC8
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

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x00030BD4 File Offset: 0x0002EDD4
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x00030BEC File Offset: 0x0002EDEC
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

		// Token: 0x06002BBE RID: 11198 RVA: 0x00030BF8 File Offset: 0x0002EDF8
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

		// Token: 0x06002BBF RID: 11199 RVA: 0x00030C6C File Offset: 0x0002EE6C
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

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00030CB0 File Offset: 0x0002EEB0
		public static bool Overlaps(IList<TimeTableItem> items, DateTime startTime, DateTime endTime)
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

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00030D20 File Offset: 0x0002EF20
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

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00030D84 File Offset: 0x0002EF84
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

		// Token: 0x04001EDF RID: 7903
		private int startMinutes;

		// Token: 0x04001EE0 RID: 7904
		private int endMinutes;

		// Token: 0x04001EE1 RID: 7905
		private string location;

		// Token: 0x04001EE2 RID: 7906
		private int luCourseId;

		// Token: 0x04001EE3 RID: 7907
		private DayOfWeek dayOfWeek;

		// Token: 0x04001EE4 RID: 7908
		private int timetableId = 0;

		// Token: 0x04001EE5 RID: 7909
		private DateTime courseStartDate = DateTime.MinValue;

		// Token: 0x04001EE6 RID: 7910
		private DateTime courseEndDate = DateTime.MinValue;

		// Token: 0x04001EE7 RID: 7911
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
