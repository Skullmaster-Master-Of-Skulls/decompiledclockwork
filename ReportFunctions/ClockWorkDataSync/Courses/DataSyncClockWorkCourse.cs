using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x02000050 RID: 80
	public class DataSyncClockWorkCourse
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0004EFDC File Offset: 0x0004DFDC
		public DataSyncClockWorkCourse()
		{
			this.Instructors = new List<DataSyncInstructor>();
			this.TimeTableItems = new List<DataSyncTimetableItem>();
			this.IsDropped = false;
			this.ExemptFromDataSync = false;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0004F010 File Offset: 0x0004E010
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0004F027 File Offset: 0x0004E027
		public DataSyncExternalCourse MatchingExternalCourse { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0004F030 File Offset: 0x0004E030
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0004F047 File Offset: 0x0004E047
		public DateTime StartDate { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0004F050 File Offset: 0x0004E050
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0004F067 File Offset: 0x0004E067
		public DateTime EndDate { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0004F070 File Offset: 0x0004E070
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0004F087 File Offset: 0x0004E087
		public string Term { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0004F090 File Offset: 0x0004E090
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0004F0A7 File Offset: 0x0004E0A7
		public string Duration { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0004F0B0 File Offset: 0x0004E0B0
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0004F0C7 File Offset: 0x0004E0C7
		public string Subject { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0004F0D0 File Offset: 0x0004E0D0
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0004F0E7 File Offset: 0x0004E0E7
		public string Course { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0004F0F0 File Offset: 0x0004E0F0
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0004F107 File Offset: 0x0004E107
		public int SubjectId { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0004F110 File Offset: 0x0004E110
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0004F127 File Offset: 0x0004E127
		public int LuCourseId { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0004F130 File Offset: 0x0004E130
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0004F147 File Offset: 0x0004E147
		public string Section { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0004F150 File Offset: 0x0004E150
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0004F167 File Offset: 0x0004E167
		public string TimeOfDay { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0004F170 File Offset: 0x0004E170
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0004F187 File Offset: 0x0004E187
		public string Campus { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0004F190 File Offset: 0x0004E190
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0004F1A7 File Offset: 0x0004E1A7
		public string Location { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0004F1B0 File Offset: 0x0004E1B0
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0004F1C7 File Offset: 0x0004E1C7
		public string Department { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0004F1D0 File Offset: 0x0004E1D0
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0004F1E7 File Offset: 0x0004E1E7
		public bool IsDropped { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0004F1F0 File Offset: 0x0004E1F0
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0004F207 File Offset: 0x0004E207
		public bool ExemptFromDataSync { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0004F210 File Offset: 0x0004E210
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0004F227 File Offset: 0x0004E227
		public int CoursesId { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0004F230 File Offset: 0x0004E230
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0004F247 File Offset: 0x0004E247
		public List<DataSyncInstructor> Instructors { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0004F250 File Offset: 0x0004E250
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0004F267 File Offset: 0x0004E267
		public List<DataSyncTimetableItem> TimeTableItems { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0004F2C0 File Offset: 0x0004E2C0
		public string InstructorsString
		{
			get
			{
				return string.Join(", ", this.Instructors.ConvertAll<string>((DataSyncInstructor i1) => string.Format("{0}: {1}; {2}; {3}", new object[]
				{
					i1.Id.ToString(),
					i1.Name,
					i1.Username,
					i1.Email
				})).ToArray());
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0004F338 File Offset: 0x0004E338
		public DataSyncInstructor AddInstructor(DataSyncInstructor instructor)
		{
			DataSyncInstructor dataSyncInstructor = this.Instructors.Find((DataSyncInstructor i) => i.Id == instructor.Id);
			DataSyncInstructor result;
			if (dataSyncInstructor != null)
			{
				result = dataSyncInstructor;
			}
			else
			{
				this.Instructors.Add(instructor);
				result = instructor;
			}
			return result;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0004F414 File Offset: 0x0004E414
		public static List<DataSyncClockWorkCourse> ParseClockWorkCourses(DataTable t)
		{
			List<DataSyncClockWorkCourse> list = new List<DataSyncClockWorkCourse>();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int lucid = (int)dataRow["lucourseid"];
				DataSyncClockWorkCourse dataSyncClockWorkCourse = list.Find((DataSyncClockWorkCourse c) => c.LuCourseId == lucid);
				if (dataSyncClockWorkCourse == null)
				{
					dataSyncClockWorkCourse = new DataSyncClockWorkCourse();
					dataSyncClockWorkCourse.LuCourseId = (int)dataRow["lucourseid"];
					dataSyncClockWorkCourse.StartDate = (DateTime)dataRow["startdate"];
					dataSyncClockWorkCourse.EndDate = (DateTime)dataRow["enddate"];
					dataSyncClockWorkCourse.Term = dataRow["term"].ToString();
					dataSyncClockWorkCourse.Duration = dataRow["duration"].ToString();
					dataSyncClockWorkCourse.Subject = dataRow["subject"].ToString();
					dataSyncClockWorkCourse.SubjectId = ((dataRow["subjectid"] == DBNull.Value) ? 0 : ((int)dataRow["subjectid"]));
					dataSyncClockWorkCourse.Course = dataRow["course"].ToString();
					dataSyncClockWorkCourse.Section = dataRow["section"].ToString();
					dataSyncClockWorkCourse.TimeOfDay = dataRow["timeofday"].ToString();
					dataSyncClockWorkCourse.Campus = dataRow["campus"].ToString();
					dataSyncClockWorkCourse.Location = dataRow["location"].ToString();
					dataSyncClockWorkCourse.Department = dataRow["department"].ToString();
					int num = t.Columns.Contains("registrationstatus") ? ((dataRow["registrationstatus"] == DBNull.Value) ? 0 : ((int)dataRow["registrationstatus"])) : 0;
					dataSyncClockWorkCourse.IsDropped = (num == 2);
					if (t.Columns.Contains("exemptfromdatasync"))
					{
						dataSyncClockWorkCourse.ExemptFromDataSync = (dataRow["exemptfromdatasync"] != DBNull.Value && Convert.ToBoolean(dataRow["exemptfromdatasync"]));
					}
					else
					{
						dataSyncClockWorkCourse.ExemptFromDataSync = false;
					}
					dataSyncClockWorkCourse.CoursesId = (t.Columns.Contains("coursesid") ? ((dataRow["coursesid"] == DBNull.Value) ? 0 : ((int)dataRow["coursesid"])) : 0);
					list.Add(dataSyncClockWorkCourse);
				}
				DataSyncInstructor dataSyncInstructor = new DataSyncInstructor(dataRow, "pinstructorid", "pinstructorname", "pinstructoremail", "pinstructorusername", "pinstructorphone");
				DataSyncInstructor dataSyncInstructor2 = new DataSyncInstructor(dataRow, "p3instructorid", "p3instructorname", "p3instructoremail", "p3instructorusername", "p3instructorphone");
				if (dataSyncInstructor.Id > 0)
				{
					dataSyncInstructor = dataSyncClockWorkCourse.AddInstructor(dataSyncInstructor);
					dataSyncInstructor.IsPrimary = true;
				}
				if (dataSyncInstructor2.Id > 0)
				{
					dataSyncClockWorkCourse.AddInstructor(dataSyncInstructor2);
				}
				List<DataSyncTimetableItem> list2 = DataSyncTimetableItem.ParseTimeTableItems(dataRow);
				DataSyncTimetableItem tti;
				foreach (DataSyncTimetableItem tti2 in list2)
				{
					tti = tti2;
					if (dataSyncClockWorkCourse.TimeTableItems.Find((DataSyncTimetableItem tt) => tt.DayOfWeek == tti.DayOfWeek && tt.StartMinutes == tti.StartMinutes && tt.EndMinutes == tti.EndMinutes) == null)
					{
						dataSyncClockWorkCourse.TimeTableItems.Add(tti);
					}
				}
			}
			return list;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0004F840 File Offset: 0x0004E840
		public string ToStringShort()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} {1} {2} {3}", new object[]
			{
				this.Subject,
				this.Course,
				this.Section,
				this.TimeOfDay
			});
			return stringBuilder.ToString();
		}
	}
}
