using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.AlternativeFormat.Adapters
{
	// Token: 0x02000162 RID: 354
	public static class CourseAdapter
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x00075630 File Offset: 0x00073830
		public static string ToDisplayString(this LookupCourseBase course)
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
