using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000053 RID: 83
	public static class CourseAdapter
	{
		// Token: 0x06000267 RID: 615 RVA: 0x000052F0 File Offset: 0x000034F0
		public static string ToDisplayString(this LookupCourseBaseDTO course)
		{
			return (course != null) ? string.Format("{0} {1} {2} {3}", new object[]
			{
				(course.Subject != null) ? (course.Subject.SubjectDescription ?? string.Empty) : string.Empty,
				course.Course ?? string.Empty,
				course.TimeOfDay ?? string.Empty,
				course.Section ?? string.Empty
			}) : string.Empty;
		}
	}
}
