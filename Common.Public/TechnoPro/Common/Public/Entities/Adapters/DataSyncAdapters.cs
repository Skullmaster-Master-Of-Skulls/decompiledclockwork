using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncCourses;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D7 RID: 1495
	public static class DataSyncAdapters
	{
		// Token: 0x06003010 RID: 12304 RVA: 0x0003BD3C File Offset: 0x00039F3C
		public static DataSyncExternalCourse GetExternalCourseFromRowPart(this DataSyncExternalCourseRowPart RowPart)
		{
			DataSyncExternalCourseStudentSpecificRowPart dataSyncExternalCourseStudentSpecificRowPart = RowPart.StudentSpecificInfo ?? new DataSyncExternalCourseStudentSpecificRowPart();
			return new DataSyncExternalCourse
			{
				ExternalCourseId = RowPart.ExternalCourseId,
				Duration = RowPart.Duration,
				Term = RowPart.Term,
				StartDate = RowPart.StartDate,
				EndDate = RowPart.EndDate,
				Subject = RowPart.Subject,
				SubjectLong = RowPart.SubjectLong,
				Course = RowPart.Course,
				Section = RowPart.Section,
				TimeOfDay = RowPart.TimeOfDay,
				Campus = RowPart.Campus,
				Department = RowPart.Department,
				Location = RowPart.Location,
				CourseNote = (RowPart.CourseNote ?? ""),
				TimetableItems = new List<DataSyncExternalCourseTimetableItem>(),
				Instructors = new List<DataSyncExternalCourseInstructor>(),
				AlternateContacts = new List<DataSyncExternalCourseAltContact>(),
				Credits = RowPart.Credits,
				StudentSpecificInfo = new DataSyncExternalCourseStudentSpecific
				{
					Grade = dataSyncExternalCourseStudentSpecificRowPart.Grade,
					GradeLetter = dataSyncExternalCourseStudentSpecificRowPart.GradeLetter,
					InProgressGrade = dataSyncExternalCourseStudentSpecificRowPart.InProgressGrade,
					InProgressGradeLetter = dataSyncExternalCourseStudentSpecificRowPart.InProgressGradeLetter,
					TuitionCost = dataSyncExternalCourseStudentSpecificRowPart.TuitionCost,
					RegistrationDate = dataSyncExternalCourseStudentSpecificRowPart.RegistrationDate,
					RegistrationNote = dataSyncExternalCourseStudentSpecificRowPart.RegistrationNote
				}
			};
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		public static DataSyncExternalCourse GetExternalCourseFromRowParts(this List<DataSyncExternalCourseRowPart> RowParts, int Start, int End)
		{
			bool flag = RowParts == null || RowParts.Count < 1;
			DataSyncExternalCourse result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataSyncExternalCourse externalCourseFromRowPart = RowParts[Start].GetExternalCourseFromRowPart();
				for (int i = Start; i <= End; i++)
				{
					DataSyncExternalCourseInstructor prof = RowParts[i].Instructor;
					bool flag2 = prof != null;
					if (flag2)
					{
						DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor = externalCourseFromRowPart.Instructors.FirstOrDefault((DataSyncExternalCourseInstructor g) => g.IsSameAs(prof));
						bool flag3 = dataSyncExternalCourseInstructor == null;
						if (flag3)
						{
							externalCourseFromRowPart.Instructors.Add(RowParts[i].Instructor);
						}
					}
					bool flag4 = RowParts[i].TimetableItems != null && RowParts[i].TimetableItems.Count > 0;
					if (flag4)
					{
						using (List<DataSyncExternalCourseTimetableItem>.Enumerator enumerator = RowParts[i].TimetableItems.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								DataSyncExternalCourseTimetableItem tti = enumerator.Current;
								DataSyncExternalCourseTimetableItem dataSyncExternalCourseTimetableItem = externalCourseFromRowPart.TimetableItems.FirstOrDefault((DataSyncExternalCourseTimetableItem g) => g.IsSameAs(tti));
								bool flag5 = dataSyncExternalCourseTimetableItem == null;
								if (flag5)
								{
									externalCourseFromRowPart.TimetableItems.Add(tti);
								}
							}
						}
					}
					bool flag6 = RowParts[i].FinalExamInfos == null;
					if (!flag6)
					{
						bool flag7 = externalCourseFromRowPart.FinalExamInfos != null;
						if (!flag7)
						{
							externalCourseFromRowPart.FinalExamInfos = new List<DataSyncExternalCourseFinalExamInfo>();
							using (IEnumerator<DataSyncExternalCourseFinalExamInfo> enumerator2 = RowParts[i].FinalExamInfos.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									DataSyncExternalCourseFinalExamInfo fei = enumerator2.Current;
									DataSyncExternalCourseFinalExamInfo dataSyncExternalCourseFinalExamInfo = externalCourseFromRowPart.FinalExamInfos.FirstOrDefault((DataSyncExternalCourseFinalExamInfo g) => g.IsSameAs(fei));
									bool flag8 = dataSyncExternalCourseFinalExamInfo == null;
									if (flag8)
									{
										externalCourseFromRowPart.FinalExamInfos.Add(fei);
									}
								}
							}
						}
					}
				}
				result = externalCourseFromRowPart;
			}
			return result;
		}
	}
}
