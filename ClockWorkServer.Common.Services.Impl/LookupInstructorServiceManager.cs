using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.StudentAccommodationRequests;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000062 RID: 98
	public class LookupInstructorServiceManager : ILookupInstructor, IService
	{
		// Token: 0x06000399 RID: 921 RVA: 0x00010B54 File Offset: 0x0000ED54
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00010B68 File Offset: 0x0000ED68
		public LoadInstructorResp LoadInstructor(LoadInstructorReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructor(Request.InstructorId);
			return new LoadInstructorResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		public SaveInstructorResp SaveInstructor(SaveInstructorReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			int instructorId = lookupInstructorManager.SaveInstructor(Request.Instructor.ToDomainObject());
			return new SaveInstructorResp
			{
				InstructorId = instructorId
			};
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		public SaveInstructorsForCourseResp SaveInstructorsForCourse(SaveInstructorsForCourseReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			List<LookupInstructor> list = lookupInstructorManager.SaveInstructorsForCourse(Request.LuCourseId, Request.Instructors.ConvertAll<LookupInstructor>((LookupInstructorDTO f) => f.ToDomainObject()), Request.UpdateInstructorInfo);
			SaveInstructorsForCourseResp saveInstructorsForCourseResp = new SaveInstructorsForCourseResp();
			List<LookupInstructorDTO> instructors;
			if (list != null)
			{
				instructors = list.ConvertAll<LookupInstructorDTO>((LookupInstructor f) => f.ToDTO());
			}
			else
			{
				instructors = null;
			}
			saveInstructorsForCourseResp.Instructors = instructors;
			return saveInstructorsForCourseResp;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public LoadInstructorByUsernameResp LoadInstructorByUsername(LoadInstructorByUsernameReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructorByUsername(Request.Username);
			return new LoadInstructorByUsernameResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public LoadInstructorByEmailResp LoadInstructorByEmail(LoadInstructorByEmailReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructorByEmail(Request.Email);
			return new LoadInstructorByEmailResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00010CFC File Offset: 0x0000EEFC
		public LoadInstructorCoursesResp LoadInstructorCourses(LoadInstructorCoursesReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			List<LookupCourse> list = lookupInstructorManager.LoadInstructorCourses(Request.InstructorId, Request.AlternateContactId, Request.PermissionLevel, Request.MustHaveClassTestDefinition, Request.StartDate, Request.EndDate);
			LoadInstructorCoursesResp loadInstructorCoursesResp = new LoadInstructorCoursesResp();
			List<LookupCourseDTO> courses;
			if (list != null)
			{
				courses = list.ConvertAll<LookupCourseDTO>((LookupCourse f) => f.ToDTO());
			}
			else
			{
				courses = null;
			}
			loadInstructorCoursesResp.Courses = courses;
			return loadInstructorCoursesResp;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00010D7C File Offset: 0x0000EF7C
		public LoadAllAssignedInstructorsResp LoadAllAssignedInstructors(LoadAllAssignedInstructorsReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			List<LookupInstructor> list = lookupInstructorManager.LoadAllAssignedInstructors();
			LoadAllAssignedInstructorsResp loadAllAssignedInstructorsResp = new LoadAllAssignedInstructorsResp();
			List<LookupInstructorDTO> instructors;
			if (list != null)
			{
				instructors = list.ConvertAll<LookupInstructorDTO>((LookupInstructor f) => f.ToDTO());
			}
			else
			{
				instructors = null;
			}
			loadAllAssignedInstructorsResp.Instructors = instructors;
			return loadAllAssignedInstructorsResp;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public LoadInstructorByEmployeeIdResp LoadInstructorByEmployeeId(LoadInstructorByEmployeeIdReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			LookupInstructor lookupInstructor = lookupInstructorManager.LoadInstructorByEmployeeId(Request.EmployeeId);
			return new LoadInstructorByEmployeeIdResp
			{
				Instructor = lookupInstructor.ToDTO()
			};
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00010E18 File Offset: 0x0000F018
		public LoadInstructorsBySearchStringResp LoadInstructorsBySearchString(LoadInstructorsBySearchStringReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			IList<LookupInstructor> list = lookupInstructorManager.LoadInstructorsBySearchString(Request.SearchString);
			LoadInstructorsBySearchStringResp loadInstructorsBySearchStringResp = new LoadInstructorsBySearchStringResp();
			IList<LookupInstructorDTO> instructors;
			if (list != null)
			{
				instructors = list.ToList<LookupInstructor>().ConvertAll<LookupInstructorDTO>((LookupInstructor f) => f.ToDTO());
			}
			else
			{
				instructors = null;
			}
			loadInstructorsBySearchStringResp.Instructors = instructors;
			return loadInstructorsBySearchStringResp;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00010E80 File Offset: 0x0000F080
		public void AssignInstructorToCourse(AssignInstructorToCourseReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			lookupInstructorManager.AssignInstructorToCourse(Request.InstructorId, Request.LuCourseId, Request.IsAssignmentExemptFromDataSync);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public void RemoveInstructorFromCourse(RemoveInstructorFromCourseReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			lookupInstructorManager.RemoveInstructorFromCourse(Request.InstructorId, Request.LuCourseId);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public LoadInstructorsByCourseResp LoadInstructorsByCourse(LoadInstructorsByCourseReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			IList<LookupInstructor> list = lookupInstructorManager.LoadInstructorsByCourse(Request.LuCourseId);
			LoadInstructorsByCourseResp loadInstructorsByCourseResp = new LoadInstructorsByCourseResp();
			IList<LookupInstructorDTO> instructors;
			if (list != null)
			{
				instructors = list.ToList<LookupInstructor>().ConvertAll<LookupInstructorDTO>((LookupInstructor f) => f.ToDTO());
			}
			else
			{
				instructors = null;
			}
			loadInstructorsByCourseResp.Instructors = instructors;
			return loadInstructorsByCourseResp;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010F4C File Offset: 0x0000F14C
		public UpdateInstructorDataSyncExemptionResp UpdateInstructorDataSyncExemption(UpdateInstructorDataSyncExemptionReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			lookupInstructorManager.UpdateInstructorDataSyncExemption(Request.InstructorId, Request.NewInstructorExemptStatus);
			return new UpdateInstructorDataSyncExemptionResp();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010F84 File Offset: 0x0000F184
		public GetUniqueCourseRegistrationStartDatesByInstructorResp GetUniqueCourseRegistrationStartDatesByInstructor(GetUniqueCourseRegistrationStartDatesByInstructorReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			IList<DateTime> uniqueCourseRegistrationStartDatesByInstructor = lookupInstructorManager.GetUniqueCourseRegistrationStartDatesByInstructor(Request.InstructorId);
			return new GetUniqueCourseRegistrationStartDatesByInstructorResp
			{
				Dates = uniqueCourseRegistrationStartDatesByInstructor
			};
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010FBC File Offset: 0x0000F1BC
		public LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp LoadInstructorCoursesWithAtLeastOneStudentRegistered(LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			IList<LookupCourse> list = lookupInstructorManager.LoadInstructorCoursesWithAtLeastOneStudentRegistered(Request.InstructorId, Request.AlternateContactId, Request.PermissionLevel, Request.MustHaveClassTestDefinition, Request.StartDate, Request.EndDate);
			LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp loadInstructorCoursesWithAtLeastOneStudentRegisteredResp = new LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp();
			List<LookupCourseDTO> courses;
			if (list != null)
			{
				courses = list.ToList<LookupCourse>().ConvertAll<LookupCourseDTO>((LookupCourse f) => f.ToDTO());
			}
			else
			{
				courses = null;
			}
			loadInstructorCoursesWithAtLeastOneStudentRegisteredResp.Courses = courses;
			return loadInstructorCoursesWithAtLeastOneStudentRegisteredResp;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00011044 File Offset: 0x0000F244
		public GetStudentsWithApprovedRequestsByCourseDateResp GetStudentsWithApprovedRequestsByCourseDate(GetStudentsWithApprovedRequestsByCourseDateReq Request)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(Request.GetOperationContext());
			IList<StudentWithRequestAndCourseInfo> studentsWithApprovedRequestsByCourseDate = lookupInstructorManager.GetStudentsWithApprovedRequestsByCourseDate(Request.InstructorId, Request.AlternateContactId, Request.StartDate, Request.EndDate, Request.ClockWorkSettingsInstanceName);
			GetStudentsWithApprovedRequestsByCourseDateResp getStudentsWithApprovedRequestsByCourseDateResp = new GetStudentsWithApprovedRequestsByCourseDateResp();
			IList<StudentWithRequestAndCourseInfoDTO> studentsWithApprovedRequests;
			if (studentsWithApprovedRequestsByCourseDate != null)
			{
				studentsWithApprovedRequests = (from g in studentsWithApprovedRequestsByCourseDate
				select g.ToDTO()).ToList<StudentWithRequestAndCourseInfoDTO>();
			}
			else
			{
				studentsWithApprovedRequests = null;
			}
			getStudentsWithApprovedRequestsByCourseDateResp.StudentsWithApprovedRequests = studentsWithApprovedRequests;
			return getStudentsWithApprovedRequestsByCourseDateResp;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public LoadStudentsWithCourseAndAccommodationInfosByCoursesResp LoadStudentsWithCourseAndAccommodationInfosByCourses(LoadStudentsWithCourseAndAccommodationInfosByCoursesReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			int[] array = Request.LuCourseIds;
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(operationContext);
			int[] allowedLucids = lookupInstructorManager.FindAllCoursesAnInstructorOrAltContactIsAllowed(Request.InstructorId, Request.AlternateContactId, 4);
			array = (from g in array
			where allowedLucids.Contains(g)
			select g).ToArray<int>();
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(operationContext);
			IList<StudentWithCourseAndAccommodationInfo> list = courseRegistrationManager.LoadStudentsWithCourseAndAccommodationInfosByCourseIds(array);
			LoadStudentsWithCourseAndAccommodationInfosByCoursesResp loadStudentsWithCourseAndAccommodationInfosByCoursesResp = new LoadStudentsWithCourseAndAccommodationInfosByCoursesResp();
			IList<StudentWithCourseAndAccommodationInfoDTO> studentsWithCourseAndAccommodationInfos;
			if (list == null)
			{
				studentsWithCourseAndAccommodationInfos = null;
			}
			else
			{
				studentsWithCourseAndAccommodationInfos = (from g in list
				select g.ToDTO()).ToList<StudentWithCourseAndAccommodationInfoDTO>();
			}
			loadStudentsWithCourseAndAccommodationInfosByCoursesResp.StudentsWithCourseAndAccommodationInfos = studentsWithCourseAndAccommodationInfos;
			return loadStudentsWithCourseAndAccommodationInfosByCoursesResp;
		}
	}
}
