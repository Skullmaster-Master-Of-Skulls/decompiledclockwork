using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x020002A3 RID: 675
	public class LookupCourseRelatedInfo
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x00019E48 File Offset: 0x00018048
		public LookupCourseRelatedInfo()
		{
			this.ClassTestDefinitions = new List<DuplicateCourseClassTestDefinition>();
			this.CourseRegistrations = new List<DuplicateCourseRegistrationInfo>();
			this.StudentReportedTestAppointments = new List<DuplicateCourseStudentReportedInfo>();
			this.ServiceProviderCourseAssignments = new List<DuplicateCourseServiceProviderCourseAssignment>();
			this.ServiceProviderRequestCourseAssignmentsForProviders = new List<DuplicateCourseServiceProviderRequestProvider>();
			this.ServiceProviderRequestCourseRequestsForStudents = new List<DuplicateCourseServiceProviderRequestStudent>();
			this.Timetables = new List<DuplicateCourseTimetable>();
			this.InstructorAssignments = new List<DuplicateCourseInstructorAssignment>();
			this.AlternateContactAssignments = new List<DuplicateCourseAltContactAssignment>();
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00019EC9 File Offset: 0x000180C9
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00019ED1 File Offset: 0x000180D1
		public IList<DuplicateCourseClassTestDefinition> ClassTestDefinitions { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00019EDA File Offset: 0x000180DA
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x00019EE2 File Offset: 0x000180E2
		public IList<DuplicateCourseRegistrationInfo> CourseRegistrations { get; set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00019EEB File Offset: 0x000180EB
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x00019EF3 File Offset: 0x000180F3
		public IList<DuplicateCourseStudentReportedInfo> StudentReportedTestAppointments { get; set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00019EFC File Offset: 0x000180FC
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00019F04 File Offset: 0x00018104
		public IList<DuplicateCourseServiceProviderCourseAssignment> ServiceProviderCourseAssignments { get; set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00019F0D File Offset: 0x0001810D
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00019F15 File Offset: 0x00018115
		public IList<DuplicateCourseServiceProviderRequestProvider> ServiceProviderRequestCourseAssignmentsForProviders { get; set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00019F1E File Offset: 0x0001811E
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00019F26 File Offset: 0x00018126
		public IList<DuplicateCourseServiceProviderRequestStudent> ServiceProviderRequestCourseRequestsForStudents { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00019F2F File Offset: 0x0001812F
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00019F37 File Offset: 0x00018137
		public IList<DuplicateCourseTimetable> Timetables { get; set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00019F40 File Offset: 0x00018140
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00019F48 File Offset: 0x00018148
		public IList<DuplicateCourseInstructorAssignment> InstructorAssignments { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00019F51 File Offset: 0x00018151
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00019F59 File Offset: 0x00018159
		public IList<DuplicateCourseAltContactAssignment> AlternateContactAssignments { get; set; }
	}
}
