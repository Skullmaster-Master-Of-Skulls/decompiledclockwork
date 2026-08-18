using System;
using System.Collections.Generic;
using System.Data;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x02000004 RID: 4
	public class DataSyncExternalCourse
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021CF File Offset: 0x000011CF
		public DataSyncExternalCourse()
		{
			this.Instructors = new List<DataSyncInstructor>();
			this.TimeTableItems = new List<DataSyncTimetableItem>();
			this.SubjectCode = "";
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002200 File Offset: 0x00001200
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002217 File Offset: 0x00001217
		public DateTime StartDate { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002220 File Offset: 0x00001220
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002237 File Offset: 0x00001237
		public DateTime EndDate { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002240 File Offset: 0x00001240
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002257 File Offset: 0x00001257
		public string Duration { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002260 File Offset: 0x00001260
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002277 File Offset: 0x00001277
		public string Term { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002280 File Offset: 0x00001280
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002297 File Offset: 0x00001297
		public string Subject { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022A0 File Offset: 0x000012A0
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022B7 File Offset: 0x000012B7
		public string Course { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022C0 File Offset: 0x000012C0
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022D7 File Offset: 0x000012D7
		public string TimeOfDay { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022E0 File Offset: 0x000012E0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000022F7 File Offset: 0x000012F7
		public string Section { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002300 File Offset: 0x00001300
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002317 File Offset: 0x00001317
		public string Campus { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002320 File Offset: 0x00001320
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002337 File Offset: 0x00001337
		public string Location { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002340 File Offset: 0x00001340
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002357 File Offset: 0x00001357
		public string Department { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002360 File Offset: 0x00001360
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002377 File Offset: 0x00001377
		public List<DataSyncInstructor> Instructors { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002380 File Offset: 0x00001380
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002397 File Offset: 0x00001397
		public List<DataSyncTimetableItem> TimeTableItems { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023A0 File Offset: 0x000013A0
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023B7 File Offset: 0x000013B7
		public DataSyncClockWorkCourse MatchingClockWorkCourse { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023C0 File Offset: 0x000013C0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000023D7 File Offset: 0x000013D7
		public string SubjectCode { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x000023E0 File Offset: 0x000013E0
		public bool SetDates(string startDateStr, string endDateStr)
		{
			DateTime startDate;
			DateTime endDate;
			bool result;
			if (DateTime.TryParse(startDateStr, out startDate) && DateTime.TryParse(endDateStr, out endDate))
			{
				this.StartDate = startDate;
				this.EndDate = endDate;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002428 File Offset: 0x00001428
		public bool IsInScope(DataSyncTermScope scope)
		{
			return !(scope.EndDate <= this.StartDate) && !(scope.StartDate > this.EndDate);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E8 File Offset: 0x000015E8
		public static List<DataSyncExternalCourse> ParseExternalCourses(DataTable t)
		{
			List<DataSyncExternalCourse> list = new List<DataSyncExternalCourse>();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataSyncExternalCourse externalCourse = new DataSyncExternalCourse();
				externalCourse.SetDates(dataRow["startdate"].ToString(), dataRow["enddate"].ToString());
				externalCourse.Term = dataRow["term"].ToString();
				externalCourse.Duration = dataRow["duration"].ToString();
				externalCourse.Subject = dataRow["subject"].ToString();
				externalCourse.Course = dataRow["course"].ToString();
				externalCourse.TimeOfDay = dataRow["timeofday"].ToString();
				externalCourse.Section = dataRow["section"].ToString();
				externalCourse.Campus = dataRow["campus"].ToString();
				externalCourse.Department = dataRow["department"].ToString();
				externalCourse.Location = dataRow["location"].ToString();
				DataSyncExternalCourse dataSyncExternalCourse = list.Find((DataSyncExternalCourse c) => c.Term.Equals(externalCourse.Term, StringComparison.OrdinalIgnoreCase) && c.Duration.Equals(externalCourse.Duration, StringComparison.OrdinalIgnoreCase) && c.Subject.Equals(externalCourse.Subject, StringComparison.OrdinalIgnoreCase) && c.Course.Equals(externalCourse.Course, StringComparison.OrdinalIgnoreCase) && c.TimeOfDay.Equals(externalCourse.TimeOfDay, StringComparison.OrdinalIgnoreCase) && c.Section.Equals(externalCourse.Section, StringComparison.OrdinalIgnoreCase));
				if (dataSyncExternalCourse == null)
				{
					dataSyncExternalCourse = externalCourse;
					list.Add(dataSyncExternalCourse);
				}
				DataSyncInstructor prof = new DataSyncInstructor(dataRow, "", "instructorname", "instructoremail", "instructorusername", "instructorphone");
				if (!string.IsNullOrEmpty(prof.Username))
				{
					if (dataSyncExternalCourse.Instructors.Find((DataSyncInstructor pr) => pr.Username.Equals(prof.Username, StringComparison.OrdinalIgnoreCase)) == null)
					{
						dataSyncExternalCourse.Instructors.Add(prof);
					}
				}
				else if (!string.IsNullOrEmpty(prof.Email))
				{
					if (dataSyncExternalCourse.Instructors.Find((DataSyncInstructor pr) => pr.Email.Equals(prof.Email, StringComparison.OrdinalIgnoreCase)) == null)
					{
						dataSyncExternalCourse.Instructors.Add(prof);
					}
				}
				else if (!string.IsNullOrEmpty(prof.Name))
				{
					if (dataSyncExternalCourse.Instructors.Find((DataSyncInstructor pr) => pr.Name.Equals(prof.Name, StringComparison.OrdinalIgnoreCase)) == null)
					{
						dataSyncExternalCourse.Instructors.Add(prof);
					}
				}
				List<DataSyncTimetableItem> list2 = DataSyncTimetableItem.ParseTimeTableItems(dataRow);
				DataSyncTimetableItem tti;
				foreach (DataSyncTimetableItem tti2 in list2)
				{
					tti = tti2;
					if (dataSyncExternalCourse.TimeTableItems.Find((DataSyncTimetableItem tt) => tt.DayOfWeek == tti.DayOfWeek && tt.StartMinutes == tti.StartMinutes && tt.EndMinutes == tti.EndMinutes) == null)
					{
						dataSyncExternalCourse.TimeTableItems.Add(tti);
					}
				}
			}
			return list;
		}
	}
}
