using System;
using System.Text;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000176 RID: 374
	public static class ReportAdapter
	{
		// Token: 0x06001048 RID: 4168 RVA: 0x00077F74 File Offset: 0x00076174
		public static string GetResultString(this DataSyncExternalCourseSyncResult result)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = result.CourseRegistrationAction > eDataSyncCourseRegistrationAction.eNoChange;
			if (flag)
			{
				stringBuilder.AppendFormat("COURSE REG CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseRegistrationAction), result.CourseRegistrationAction));
			}
			else
			{
				bool flag2 = result.LookupCourseAction > eDataSyncCourseLookupCourseAction.eNoChange;
				if (flag2)
				{
					stringBuilder.AppendFormat("LOOKUP COURSE CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseLookupCourseAction), result.LookupCourseAction));
				}
				else
				{
					bool flag3 = result.InstructorAction > eDataSyncCourseInstructorAction.eNoChange;
					if (flag3)
					{
						stringBuilder.AppendFormat("INSTRUCTOR CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseInstructorAction), result.InstructorAction));
					}
					else
					{
						bool flag4 = result.MiscAction > eDataSyncCourseMiscAction.eNoAction;
						if (flag4)
						{
							stringBuilder.AppendFormat("MISC CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseMiscAction), result.MiscAction));
						}
						else
						{
							bool flag5 = result.ErrorAction > eDataSyncCourseError.eNoError;
							if (flag5)
							{
								stringBuilder.AppendFormat("ERROR CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseError), result.ErrorAction));
							}
							else
							{
								stringBuilder.Append("UNKNOWN ACTION");
							}
						}
					}
				}
			}
			stringBuilder.AppendFormat(" [lucid={0},iid={1},ext={2},msg={3}]", new object[]
			{
				result.Lucid.ToString(),
				result.InstructorId.ToString(),
				(result.ExternalCourse == null) ? "NULL" : string.Format("{0} {1} {2} {3} {4}", new object[]
				{
					result.ExternalCourse.Term,
					result.ExternalCourse.Subject,
					result.ExternalCourse.Course,
					result.ExternalCourse.Section,
					result.ExternalCourse.TimeOfDay
				}),
				result.Msg ?? ""
			});
			return stringBuilder.ToString();
		}
	}
}
