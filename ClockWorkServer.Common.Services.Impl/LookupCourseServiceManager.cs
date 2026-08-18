using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000060 RID: 96
	public class LookupCourseServiceManager : ILookupCourse, IService
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00010440 File Offset: 0x0000E640
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010454 File Offset: 0x0000E654
		public LoadCoursesBySubjectAndSessionResp LoadCoursesBySubjectAndSession(LoadCoursesBySubjectAndSessionReq request)
		{
			LookupCourseManager lookupCourseManager = new LookupCourseManager(request.GetOperationContext());
			List<LookupCourse> list = lookupCourseManager.LoadCoursesBySubjectAndSession(request.Session.ToDomainObject(), request.SubjectId);
			LoadCoursesBySubjectAndSessionResp loadCoursesBySubjectAndSessionResp = new LoadCoursesBySubjectAndSessionResp();
			loadCoursesBySubjectAndSessionResp.Courses = list.ConvertAll<LookupCourseDTO>((LookupCourse c) => c.ToDTO());
			return loadCoursesBySubjectAndSessionResp;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000104BC File Offset: 0x0000E6BC
		public LoadInstructorByUsernameResp LoadInstructorByUsername(LoadInstructorByUsernameReq request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructorByUsername(request.Username);
			return new LoadInstructorByUsernameResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000104FC File Offset: 0x0000E6FC
		public LoadInstructorByEmailResp LoadInstructorByEmail(LoadInstructorByEmailReq request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructorByEmail(request.Email);
			return new LoadInstructorByEmailResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001053C File Offset: 0x0000E73C
		public LoadInstructorResp LoadInstructor(LoadInstructorReq request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructor(request.InstructorId);
			return new LoadInstructorResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001057C File Offset: 0x0000E77C
		public void SaveInstructor(SaveInstructorReq request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(request.GetOperationContext());
			lookupInstructorManager.SaveInstructor(request.Instructor.ToDomainObject());
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000105A8 File Offset: 0x0000E7A8
		public void SaveInstructorsForCourse(SaveInstructorsForCourseReq request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(request.GetOperationContext());
			lookupInstructorManager.SaveInstructorsForCourse(request.LuCourseId, request.Instructors.ConvertAll<LookupInstructor>((LookupInstructorDTO p) => p.ToDomainObject()), request.UpdateInstructorInfo);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00010600 File Offset: 0x0000E800
		public LoadCourseByLuCourseIdResp LoadCourseByLuCourseId(LoadCourseByLuCourseIdReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			return new LoadCourseByLuCourseIdResp
			{
				Course = lookupCourseManager.LoadCourse(Request.LuCourseId).ToDTO()
			};
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001063C File Offset: 0x0000E83C
		public CreateLookupCourseResp CreateLookupCourse(CreateLookupCourseReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			return new CreateLookupCourseResp
			{
				LuCourseId = lookupCourseManager.CreateLookupCourse(Request.Course.ToDomainObject())
			};
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010678 File Offset: 0x0000E878
		public void UpdateCourseInstructorExemption(UpdateCourseInstructorExemptionReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			lookupCourseManager.UpdateCourseInstructorExemption(Request.LuCourseId, Request.InstructorId, Request.NewIsInstructorExemptFromCourseList);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000106AC File Offset: 0x0000E8AC
		public LoadCourseBasesBySearchStringResp LoadCourseBasesBySearchString(LoadCourseBasesBySearchStringReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			IList<LookupCourseBase> list = lookupCourseManager.LoadCourseBasesBySearchString(Request.StartDate, Request.EndDate, Request.SearchString);
			LoadCourseBasesBySearchStringResp loadCourseBasesBySearchStringResp = new LoadCourseBasesBySearchStringResp();
			IList<LookupCourseBaseDTO> courseBases;
			if (list == null)
			{
				courseBases = null;
			}
			else
			{
				courseBases = list.ToList<LookupCourseBase>().ConvertAll<LookupCourseBaseDTO>((LookupCourseBase f) => f.ToDTO());
			}
			loadCourseBasesBySearchStringResp.CourseBases = courseBases;
			return loadCourseBasesBySearchStringResp;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010720 File Offset: 0x0000E920
		public CreateLookupCourseBaseResp CreateLookupCourseBase(CreateLookupCourseBaseReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			LookupCourse lookupCourse = lookupCourseManager.CreateLookupCourseBase(Request.CourseBase.ToDomainObject());
			return new CreateLookupCourseBaseResp
			{
				NewCourse = ((lookupCourse != null) ? lookupCourse.ToDTO() : null)
			};
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00010768 File Offset: 0x0000E968
		public LoadIsLookupCourseExemptFromDataSyncResp LoadIsLookupCourseExemptFromDataSync(LoadIsLookupCourseExemptFromDataSyncReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			IDictionary<int, bool> isExemptFromDataSyncList = lookupCourseManager.LoadIsLookupCourseExemptFromDataSync(Request.LuCourseIds);
			return new LoadIsLookupCourseExemptFromDataSyncResp
			{
				IsExemptFromDataSyncList = isExemptFromDataSyncList
			};
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public void UpdateLookupCourseExemptionFromDataSync(UpdateLookupCourseExemptionFromDataSyncReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			lookupCourseManager.UpdateLookupCourseExemptionFromDataSync(Request.LuCourseId, Request.NewIsExempt);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000107D0 File Offset: 0x0000E9D0
		public LoadDurationTermSubjectsBySessionResp LoadDurationTermSubjectsBySession(LoadDurationTermSubjectsBySessionReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			IList<LookupDurationTermSubject> list = lookupCourseManager.LoadDurationTermSubjectsBySession(Request.Session.ToDomainObject());
			LoadDurationTermSubjectsBySessionResp loadDurationTermSubjectsBySessionResp = new LoadDurationTermSubjectsBySessionResp();
			IList<LookupDurationTermSubjectDTO> durationTermSubjects;
			if (list == null)
			{
				durationTermSubjects = null;
			}
			else
			{
				durationTermSubjects = list.ToList<LookupDurationTermSubject>().ConvertAll<LookupDurationTermSubjectDTO>((LookupDurationTermSubject g) => g.ToDTO());
			}
			loadDurationTermSubjectsBySessionResp.DurationTermSubjects = durationTermSubjects;
			return loadDurationTermSubjectsBySessionResp;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001083C File Offset: 0x0000EA3C
		public LoadStudentsCoursesBySessionResp LoadStudentsCoursesBySession(LoadStudentsCoursesBySessionReq request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(request.GetOperationContext());
			List<CourseRegistration> list = lookupCourseManager.LoadStudentsCourses(request.Session.ToDomainObject(), request.PersonId);
			LoadStudentsCoursesBySessionResp loadStudentsCoursesBySessionResp = new LoadStudentsCoursesBySessionResp();
			IList<CourseRegistrationDTO> courses;
			if (list == null)
			{
				courses = null;
			}
			else
			{
				courses = list.ToList<CourseRegistration>().ConvertAll<CourseRegistrationDTO>((CourseRegistration g) => g.ToDTO());
			}
			loadStudentsCoursesBySessionResp.Courses = courses;
			return loadStudentsCoursesBySessionResp;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public LoadStudentsCoursesByDatesResp LoadStudentsCoursesByDates(LoadStudentsCoursesByDatesReq request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(request.GetOperationContext());
			List<CourseRegistration> list = lookupCourseManager.LoadStudentsCourses(request.PersonId, request.StartDate, request.EndDate);
			LoadStudentsCoursesByDatesResp loadStudentsCoursesByDatesResp = new LoadStudentsCoursesByDatesResp();
			IList<CourseRegistrationDTO> courses;
			if (list == null)
			{
				courses = null;
			}
			else
			{
				courses = list.ToList<CourseRegistration>().ConvertAll<CourseRegistrationDTO>((CourseRegistration g) => g.ToDTO());
			}
			loadStudentsCoursesByDatesResp.Courses = courses;
			return loadStudentsCoursesByDatesResp;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00010924 File Offset: 0x0000EB24
		public LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			ILookupCourseManager lookupCourseManager2 = lookupCourseManager;
			IList<int> luCourseIds = Request.LuCourseIds;
			List<int> lucids = lookupCourseManager2.LoadLookupCourseIdsWithAtLeastOneClassTestDefinition((luCourseIds != null) ? luCourseIds.ToList<int>() : null, Request.StartDate, Request.EndDate);
			return new LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp
			{
				Lucids = lucids
			};
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00010974 File Offset: 0x0000EB74
		public LoadUniqueCourseDateRangesBySessionResp LoadUniqueCourseDateRangesBySession(LoadUniqueCourseDateRangesBySessionReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			IList<LookupCourseDateRange> list = lookupCourseManager.LoadUniqueCourseDateRangesBySession(Request.Session.ToDomainObject());
			LoadUniqueCourseDateRangesBySessionResp loadUniqueCourseDateRangesBySessionResp = new LoadUniqueCourseDateRangesBySessionResp();
			IList<LookupCourseDateRangeDTO> uniqueDateRanges;
			if (list == null)
			{
				uniqueDateRanges = null;
			}
			else
			{
				uniqueDateRanges = (from g in list
				select g.ToDTO()).ToList<LookupCourseDateRangeDTO>();
			}
			loadUniqueCourseDateRangesBySessionResp.UniqueDateRanges = uniqueDateRanges;
			return loadUniqueCourseDateRangesBySessionResp;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000109E0 File Offset: 0x0000EBE0
		public UpdateCourseDateRangeResp UpdateCourseDateRange(UpdateCourseDateRangeReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			lookupCourseManager.UpdateCourseDateRange(Request.OldDateRange.ToDomainObject(), Request.NewDateRange.ToDomainObject());
			return new UpdateCourseDateRangeResp();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010A20 File Offset: 0x0000EC20
		public LoadCoursesInDateRangeResp LoadCoursesInDateRange(LoadCoursesInDateRangeReq Request)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(Request.GetOperationContext());
			IList<LookupCourseBase> list = lookupCourseManager.LoadCoursesInDateRange(Request.DateRange.ToDomainObject());
			LoadCoursesInDateRangeResp loadCoursesInDateRangeResp = new LoadCoursesInDateRangeResp();
			IList<LookupCourseBaseDTO> courseBases;
			if (list == null)
			{
				courseBases = null;
			}
			else
			{
				courseBases = (from g in list
				select g.ToDTO()).ToList<LookupCourseBaseDTO>();
			}
			loadCoursesInDateRangeResp.CourseBases = courseBases;
			return loadCoursesInDateRangeResp;
		}
	}
}
