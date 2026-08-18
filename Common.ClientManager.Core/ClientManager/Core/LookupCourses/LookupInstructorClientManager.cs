using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x02000040 RID: 64
	public class LookupInstructorClientManager : ILookupInstructorClientManager, IWebService
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000B220 File Offset: 0x00009420
		public LookupInstructorDTO LoadInstructor(int InstructorId)
		{
			LoadInstructorReq loadInstructorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorReq>();
			loadInstructorReq.InstructorId = InstructorId;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructor(loadInstructorReq).Instructor;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B258 File Offset: 0x00009458
		public int SaveInstructor(LookupInstructorDTO instructor)
		{
			SaveInstructorReq saveInstructorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveInstructorReq>();
			saveInstructorReq.Instructor = instructor;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().SaveInstructor(saveInstructorReq).InstructorId;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B290 File Offset: 0x00009490
		public IList<LookupCourseDTO> LoadCoursesByInstructor(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, int PermissionLevel)
		{
			LoadInstructorCoursesReq loadInstructorCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorCoursesReq>();
			loadInstructorCoursesReq.InstructorId = InstructorId;
			loadInstructorCoursesReq.AlternateContactId = AlternateContactId;
			loadInstructorCoursesReq.StartDate = StartDate;
			loadInstructorCoursesReq.EndDate = EndDate;
			loadInstructorCoursesReq.PermissionLevel = PermissionLevel;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorCourses(loadInstructorCoursesReq).Courses;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public IList<LookupInstructorDTO> SaveInstructorsForCourse(int LuCourseId, List<LookupInstructorDTO> Instructors, bool UpdateInstructorInfo)
		{
			SaveInstructorsForCourseReq saveInstructorsForCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveInstructorsForCourseReq>();
			saveInstructorsForCourseReq.LuCourseId = LuCourseId;
			saveInstructorsForCourseReq.Instructors = Instructors;
			saveInstructorsForCourseReq.UpdateInstructorInfo = UpdateInstructorInfo;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().SaveInstructorsForCourse(saveInstructorsForCourseReq).Instructors;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B330 File Offset: 0x00009530
		public LookupInstructorDTO LoadInstructorByUsername(string username)
		{
			LoadInstructorByUsernameReq loadInstructorByUsernameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorByUsernameReq>();
			loadInstructorByUsernameReq.Username = username;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorByUsername(loadInstructorByUsernameReq).Instructor;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B368 File Offset: 0x00009568
		public LookupInstructorDTO LoadInstructorByEmail(string email)
		{
			LoadInstructorByEmailReq loadInstructorByEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorByEmailReq>();
			loadInstructorByEmailReq.Email = email;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorByEmail(loadInstructorByEmailReq).Instructor;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B3A0 File Offset: 0x000095A0
		public LookupInstructorDTO LoadInstructorByEmployeeId(string employeeId)
		{
			LoadInstructorByEmployeeIdReq loadInstructorByEmployeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorByEmployeeIdReq>();
			loadInstructorByEmployeeIdReq.EmployeeId = employeeId;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorByEmployeeId(loadInstructorByEmployeeIdReq).Instructor;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IList<LookupCourseDTO> LoadInstructorCourses(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate)
		{
			LoadInstructorCoursesReq loadInstructorCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorCoursesReq>();
			loadInstructorCoursesReq.InstructorId = InstructorId;
			loadInstructorCoursesReq.AlternateContactId = AlternateContactId;
			loadInstructorCoursesReq.PermissionLevel = PermissionLevel;
			loadInstructorCoursesReq.MustHaveClassTestDefinition = MustHaveClassTestDefinition;
			loadInstructorCoursesReq.StartDate = StartDate;
			loadInstructorCoursesReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorCourses(loadInstructorCoursesReq).Courses;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B438 File Offset: 0x00009638
		public IList<LookupCourseDTO> LoadInstructorCoursesWithAtLeastOneStudentRegistered(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate)
		{
			LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq loadInstructorCoursesWithAtLeastOneStudentRegisteredReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq>();
			loadInstructorCoursesWithAtLeastOneStudentRegisteredReq.InstructorId = InstructorId;
			loadInstructorCoursesWithAtLeastOneStudentRegisteredReq.AlternateContactId = AlternateContactId;
			loadInstructorCoursesWithAtLeastOneStudentRegisteredReq.PermissionLevel = PermissionLevel;
			loadInstructorCoursesWithAtLeastOneStudentRegisteredReq.MustHaveClassTestDefinition = MustHaveClassTestDefinition;
			loadInstructorCoursesWithAtLeastOneStudentRegisteredReq.StartDate = StartDate;
			loadInstructorCoursesWithAtLeastOneStudentRegisteredReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorCoursesWithAtLeastOneStudentRegistered(loadInstructorCoursesWithAtLeastOneStudentRegisteredReq).Courses;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B498 File Offset: 0x00009698
		public IList<LookupInstructorDTO> LoadAllAssignedInstructors()
		{
			LoadAllAssignedInstructorsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllAssignedInstructorsReq>();
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadAllAssignedInstructors(request).Instructors;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public IList<LookupInstructorDTO> LoadInstructorsBySearchString(string SearchString)
		{
			LoadInstructorsBySearchStringReq loadInstructorsBySearchStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorsBySearchStringReq>();
			loadInstructorsBySearchStringReq.SearchString = SearchString;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorsBySearchString(loadInstructorsBySearchStringReq).Instructors;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B500 File Offset: 0x00009700
		public void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync)
		{
			AssignInstructorToCourseReq assignInstructorToCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignInstructorToCourseReq>();
			assignInstructorToCourseReq.InstructorId = InstructorId;
			assignInstructorToCourseReq.LuCourseId = LuCourseId;
			assignInstructorToCourseReq.IsAssignmentExemptFromDataSync = IsAssignmentExemptFromDataSync;
			ClientServiceFactory.GetClientInstance<ILookupInstructor>().AssignInstructorToCourse(assignInstructorToCourseReq);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B540 File Offset: 0x00009740
		public void RemoveInstructorFromCourse(int InstructorId, int LuCourseId)
		{
			RemoveInstructorFromCourseReq removeInstructorFromCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveInstructorFromCourseReq>();
			removeInstructorFromCourseReq.InstructorId = InstructorId;
			removeInstructorFromCourseReq.LuCourseId = LuCourseId;
			ClientServiceFactory.GetClientInstance<ILookupInstructor>().RemoveInstructorFromCourse(removeInstructorFromCourseReq);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B578 File Offset: 0x00009778
		public IList<LookupInstructorDTO> LoadInstructorsByCourse(int LuCourseId)
		{
			LoadInstructorsByCourseReq loadInstructorsByCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorsByCourseReq>();
			loadInstructorsByCourseReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadInstructorsByCourse(loadInstructorsByCourseReq).Instructors;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus)
		{
			UpdateInstructorDataSyncExemptionReq updateInstructorDataSyncExemptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateInstructorDataSyncExemptionReq>();
			updateInstructorDataSyncExemptionReq.InstructorId = InstructorId;
			updateInstructorDataSyncExemptionReq.NewInstructorExemptStatus = NewInstructorExemptStatus;
			ClientServiceFactory.GetClientInstance<ILookupInstructor>().UpdateInstructorDataSyncExemption(updateInstructorDataSyncExemptionReq);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId)
		{
			GetUniqueCourseRegistrationStartDatesByInstructorReq getUniqueCourseRegistrationStartDatesByInstructorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetUniqueCourseRegistrationStartDatesByInstructorReq>();
			getUniqueCourseRegistrationStartDatesByInstructorReq.InstructorId = InstructorId;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().GetUniqueCourseRegistrationStartDatesByInstructor(getUniqueCourseRegistrationStartDatesByInstructorReq).Dates;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B620 File Offset: 0x00009820
		public IList<StudentWithRequestAndCourseInfoDTO> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			GetStudentsWithApprovedRequestsByCourseDateReq getStudentsWithApprovedRequestsByCourseDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetStudentsWithApprovedRequestsByCourseDateReq>();
			getStudentsWithApprovedRequestsByCourseDateReq.InstructorId = InstructorId;
			getStudentsWithApprovedRequestsByCourseDateReq.AlternateContactId = AlternateContactId;
			getStudentsWithApprovedRequestsByCourseDateReq.StartDate = StartDate;
			getStudentsWithApprovedRequestsByCourseDateReq.EndDate = EndDate;
			getStudentsWithApprovedRequestsByCourseDateReq.ClockWorkSettingsInstanceName = clientCache.InstanceName;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().GetStudentsWithApprovedRequestsByCourseDate(getStudentsWithApprovedRequestsByCourseDateReq).StudentsWithApprovedRequests;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B684 File Offset: 0x00009884
		public IList<StudentWithCourseAndAccommodationInfoDTO> LoadStudentsWithCourseAndAccommodationInfosByCourses(int instructorId, int altContactId, params int[] lucids)
		{
			LoadStudentsWithCourseAndAccommodationInfosByCoursesReq loadStudentsWithCourseAndAccommodationInfosByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsWithCourseAndAccommodationInfosByCoursesReq>();
			loadStudentsWithCourseAndAccommodationInfosByCoursesReq.InstructorId = instructorId;
			loadStudentsWithCourseAndAccommodationInfosByCoursesReq.AlternateContactId = altContactId;
			loadStudentsWithCourseAndAccommodationInfosByCoursesReq.LuCourseIds = lucids;
			return ClientServiceFactory.GetClientInstance<ILookupInstructor>().LoadStudentsWithCourseAndAccommodationInfosByCourses(loadStudentsWithCourseAndAccommodationInfosByCoursesReq).StudentsWithCourseAndAccommodationInfos;
		}
	}
}
