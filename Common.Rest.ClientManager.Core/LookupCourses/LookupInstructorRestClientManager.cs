using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000036 RID: 54
	public class LookupInstructorRestClientManager : BearerTokenRestProxy<ILookupInstructorClientManager>, ILookupInstructorClientManager, IWebService
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00006D32 File Offset: 0x00004F32
		public LookupInstructorRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006D3C File Offset: 0x00004F3C
		public LookupInstructorRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006D47 File Offset: 0x00004F47
		public LookupInstructorDTO LoadInstructor(int InstructorId)
		{
			return base.Get<LookupInstructorDTO>(string.Format("lookupinstructor/instructorid/{0}", InstructorId), true);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006D60 File Offset: 0x00004F60
		public int SaveInstructor(LookupInstructorDTO instructor)
		{
			return base.Post<LookupInstructorDTO, int>(instructor, "lookupinstructor");
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006D70 File Offset: 0x00004F70
		public IList<LookupInstructorDTO> SaveInstructorsForCourse(int LuCourseId, List<LookupInstructorDTO> Instructors, bool UpdateInstructorInfo)
		{
			SaveInstructorsForCourseReq saveInstructorsForCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveInstructorsForCourseReq>();
			saveInstructorsForCourseReq.LuCourseId = LuCourseId;
			saveInstructorsForCourseReq.Instructors = Instructors;
			saveInstructorsForCourseReq.UpdateInstructorInfo = UpdateInstructorInfo;
			return base.Post<SaveInstructorsForCourseReq, IList<LookupInstructorDTO>>(saveInstructorsForCourseReq, "lookupinstructor/saveforcourse");
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006DA9 File Offset: 0x00004FA9
		public LookupInstructorDTO LoadInstructorByUsername(string username)
		{
			return base.Get<LookupInstructorDTO>(string.Format("lookupinstructor/username/{0}", username), true);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006DBD File Offset: 0x00004FBD
		public LookupInstructorDTO LoadInstructorByEmail(string email)
		{
			return base.Get<LookupInstructorDTO>(string.Format("lookupinstructor/email/{0}", email), true);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006DD1 File Offset: 0x00004FD1
		public LookupInstructorDTO LoadInstructorByEmployeeId(string employeeId)
		{
			return base.Get<LookupInstructorDTO>(string.Format("lookupinstructor/employeeid/{0}", employeeId), true);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public IList<LookupCourseDTO> LoadInstructorCourses(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<LookupCourseDTO>(string.Format("lookupinstructor/courses/instructorid/{0}/alternatecontactid/{1}/permissionlevel/{2}/range/{3}/{4}?musthaveclasstestdefinition={5}", new object[]
			{
				InstructorId,
				AlternateContactId,
				PermissionLevel,
				StartDate,
				EndDate,
				MustHaveClassTestDefinition
			}), true);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006E48 File Offset: 0x00005048
		public IList<LookupCourseDTO> LoadInstructorCoursesWithAtLeastOneStudentRegistered(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<LookupCourseDTO>(string.Format("lookupinstructor/courseswithatleastonestudentregistered/instructorid/{0}/alternatecontactid/{1}/permissionlevel/{2}/range/{3}/{4}?musthaveclasstestdefinition={5}", new object[]
			{
				InstructorId,
				AlternateContactId,
				PermissionLevel,
				StartDate,
				EndDate,
				MustHaveClassTestDefinition
			}), true);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006EA5 File Offset: 0x000050A5
		public IList<LookupInstructorDTO> LoadAllAssignedInstructors()
		{
			return base.GetMany<LookupInstructorDTO>("lookupinstructor/allassigned", true);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006EB3 File Offset: 0x000050B3
		public IList<LookupInstructorDTO> LoadInstructorsBySearchString(string SearchString)
		{
			return base.GetMany<LookupInstructorDTO>(string.Format("lookupinstructor/matching?searchstring={0}", SearchString), true);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006EC8 File Offset: 0x000050C8
		public void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync)
		{
			AssignInstructorToCourseReq assignInstructorToCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignInstructorToCourseReq>();
			assignInstructorToCourseReq.InstructorId = InstructorId;
			assignInstructorToCourseReq.LuCourseId = LuCourseId;
			assignInstructorToCourseReq.IsAssignmentExemptFromDataSync = IsAssignmentExemptFromDataSync;
			base.Post<AssignInstructorToCourseReq>(assignInstructorToCourseReq, "lookupinstructor/assigntocourse");
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006F01 File Offset: 0x00005101
		public void RemoveInstructorFromCourse(int InstructorId, int LuCourseId)
		{
			base.Delete(string.Format("lookupinstructor/removefromcourse/instructorid/{0}/lucourseid/{1}", InstructorId, LuCourseId));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006F1F File Offset: 0x0000511F
		public IList<LookupInstructorDTO> LoadInstructorsByCourse(int LuCourseId)
		{
			return base.GetMany<LookupInstructorDTO>(string.Format("lookupinstructor/lucourseid/{0}", LuCourseId), true);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006F38 File Offset: 0x00005138
		public void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus)
		{
			UpdateInstructorDataSyncExemptionReq updateInstructorDataSyncExemptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateInstructorDataSyncExemptionReq>();
			updateInstructorDataSyncExemptionReq.InstructorId = InstructorId;
			updateInstructorDataSyncExemptionReq.NewInstructorExemptStatus = NewInstructorExemptStatus;
			base.Put<UpdateInstructorDataSyncExemptionReq>(updateInstructorDataSyncExemptionReq, "lookupinstructor/datasync");
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006F6A File Offset: 0x0000516A
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId)
		{
			return base.GetMany<DateTime>(string.Format("lookupinstructor/uniquecourseregistrationstartdates/instructorid/{0}", InstructorId), true);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006F83 File Offset: 0x00005183
		public IList<LookupCourseDTO> LoadCoursesByInstructor(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, int PermissionLevel)
		{
			return this.LoadInstructorCourses(InstructorId, AlternateContactId, PermissionLevel, false, StartDate, EndDate);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006F94 File Offset: 0x00005194
		public IList<StudentWithRequestAndCourseInfoDTO> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			return base.GetMany<StudentWithRequestAndCourseInfoDTO>(string.Format("lookupinstructor/studentswithapprovedrequests/instructorid/{0}/alternatecontactid/{1}/range/{2}/{3}?settingsinstancename={4}", new object[]
			{
				InstructorId,
				AlternateContactId,
				StartDate,
				EndDate,
				clientCache.InstanceName
			}), true);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00002BEE File Offset: 0x00000DEE
		public IList<StudentWithCourseAndAccommodationInfoDTO> LoadStudentsWithCourseAndAccommodationInfosByCourses(int instructorId, int altContactId, params int[] lucids)
		{
			throw new NotImplementedException();
		}
	}
}
