using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.CourseRegistrations;
using TechnoPro.Common.DAO.Impl.CourseRegistrations;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.CourseRegistrations
{
	// Token: 0x02000118 RID: 280
	public class CourseRegistrationManager : ICourseRegistrationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00053497 File Offset: 0x00051697
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0005349F File Offset: 0x0005169F
		internal ICourseRegistrationDAO dao { get; set; }

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000534A8 File Offset: 0x000516A8
		public CourseRegistrationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new CourseRegistrationDAO(opContext);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000534C7 File Offset: 0x000516C7
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x000534CF File Offset: 0x000516CF
		public OperationContext OpContext { get; set; }

		// Token: 0x06000BCA RID: 3018 RVA: 0x000534D8 File Offset: 0x000516D8
		public void DeleteCourseRegistration(int CoursesId)
		{
			this.dao.DeleteCourseRegistration(CoursesId);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000534E8 File Offset: 0x000516E8
		public List<CourseRegistration> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			return this.dao.LoadStudentsCourses(StartDate, EndDate, PersonId, IncludeDroppedCourses);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0005350C File Offset: 0x0005170C
		public IList<CourseRegistrationWithStudentSpecificInfo> LoadStudentsCoursesWithStudentSpecificInfos(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			return this.dao.LoadStudentsCoursesWithStudentSpecificInfo(StartDate, EndDate, PersonId, IncludeDroppedCourses);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0005352E File Offset: 0x0005172E
		public void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatus NewStatus)
		{
			this.dao.ChangeCourseRegistrationStatus(CoursesId, NewStatus);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00053540 File Offset: 0x00051740
		public CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid, bool? ExemptCourseFromDataSyncForStudent)
		{
			return this.dao.RegisterStudentInCourse(StudentPid, Lucid, ExemptCourseFromDataSyncForStudent);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00053560 File Offset: 0x00051760
		public CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid)
		{
			return this.dao.RegisterStudentInCourse(StudentPid, Lucid, null);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00053588 File Offset: 0x00051788
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId)
		{
			return this.dao.GetUniqueCourseRegistrationStartDatesByStudent(PersonId);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000535A6 File Offset: 0x000517A6
		public void MergeCourseRegistrations(int PersonIdNew, int PersonIdOld)
		{
			this.dao.MergeCourseRegistrations(PersonIdNew, PersonIdOld);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000535B8 File Offset: 0x000517B8
		public IList<CourseRegistration> LoadCourseRegistrationsByCourse(int LuCourseId)
		{
			return this.dao.LoadCourseRegistrationsByCourse(LuCourseId);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000535D6 File Offset: 0x000517D6
		public void SetDateLetterIssued(int PersonId, int LuCourseId, DateTime? Date)
		{
			this.dao.SetDateLetterIssued(PersonId, LuCourseId, Date);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000535E8 File Offset: 0x000517E8
		public void SetDateLetterReturned(int PersonId, int LuCourseId, DateTime? Date)
		{
			this.dao.SetDateLetterReturned(PersonId, LuCourseId, Date);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000535FA File Offset: 0x000517FA
		public void SetProfLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date)
		{
			this.dao.SetProfLastViewedLetter(PersonId, LuCourseId, Date);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0005360C File Offset: 0x0005180C
		public void SetStudentLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date)
		{
			this.dao.SetStudentLastViewedLetter(PersonId, LuCourseId, Date);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00053620 File Offset: 0x00051820
		public void SetProfLastViewedLetters(int PersonId, IList<int> LuCourseIds, DateTime? Date)
		{
			bool flag = LuCourseIds == null;
			if (!flag)
			{
				foreach (int luCourseId in LuCourseIds)
				{
					this.SetProfLastViewedLetter(PersonId, luCourseId, Date);
				}
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00053678 File Offset: 0x00051878
		public void SetStudentLastViewedLetters(int PersonId, IList<int> LuCourseIds, DateTime? Date)
		{
			bool flag = LuCourseIds == null;
			if (!flag)
			{
				foreach (int luCourseId in LuCourseIds)
				{
					this.SetStudentLastViewedLetter(PersonId, luCourseId, Date);
				}
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000536D0 File Offset: 0x000518D0
		public void SetDateLetterIssued(int CoursesId, DateTime? Date)
		{
			this.dao.SetDateLetterIssued(CoursesId, Date);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000536E1 File Offset: 0x000518E1
		public void SetDateLetterReturned(int CoursesId, DateTime? Date)
		{
			this.dao.SetDateLetterReturned(CoursesId, Date);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000536F2 File Offset: 0x000518F2
		public void SetProfLastViewedLetter(int CoursesId, DateTime? Date)
		{
			this.dao.SetProfLastViewedLetter(CoursesId, Date);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00053703 File Offset: 0x00051903
		public void SetStudentLastViewedLetter(int CoursesId, DateTime? Date)
		{
			this.dao.SetStudentLastViewedLetter(CoursesId, Date);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00053714 File Offset: 0x00051914
		public StudentCourseList LoadCoursesStudentIsAllowedToBookTestsForNow(int StudentPersonId)
		{
			ISessionManager sessionManager = new SessionManager(this.OpContext);
			Session currentSession = sessionManager.GetCurrentSession();
			ISettingManager settingManager = new SettingManager(this.OpContext);
			int settingValue = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_CourseEndDateAuthorizationExtensionInDays);
			string settingValue2 = settingManager.GetSettingValue<string>(Setting.TESTBOOKING_SpecialAccommodations);
			List<SpecialAccommodation> source = SpecialAccommodation.LoadSpecialAccommodations(settingValue2, "");
			List<int> list = (from h in (from g in source
			where g.IsActive && g.SpecialAccommodationType == SpecialAccommodationType.CantBookOnline
			select g.ControlId).Distinct<int>()
			where h > 0
			select h).ToList<int>();
			DateTime startDate = currentSession.StartDate;
			DateTime endDate = (settingValue > 0) ? currentSession.EndDate.AddDays((double)settingValue) : currentSession.EndDate;
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
			List<CourseRegistration> list2 = courseRegistrationManager.LoadStudentsCourses(startDate, endDate, StudentPersonId, false);
			bool flag = list.Count < 1;
			StudentCourseList result;
			if (flag)
			{
				result = new StudentCourseList
				{
					Courses = list2
				};
			}
			else
			{
				IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
				IList<int> coursesStudentHasCantBookCheckedIn = accommodationsManager.LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(StudentPersonId, list.ToArray(), (from g in list2
				select g.Course.LuCourseId).ToArray<int>());
				StudentCourseList studentCourseList2;
				if (coursesStudentHasCantBookCheckedIn.Count >= 1)
				{
					StudentCourseList studentCourseList = new StudentCourseList();
					studentCourseList.Courses = (from g in list2
					where !coursesStudentHasCantBookCheckedIn.Contains(g.Course.LuCourseId)
					select g).ToList<CourseRegistration>();
					studentCourseList2 = studentCourseList;
					studentCourseList.AtLeastOneCourseRemovedBecauseOfSpecialAccommodationNotAllowedToBookRestriction = true;
				}
				else
				{
					(studentCourseList2 = new StudentCourseList()).Courses = list2;
				}
				result = studentCourseList2;
			}
			return result;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000538F4 File Offset: 0x00051AF4
		public StudentCourseList LoadCoursesStudentIsAllowedToBookFinalExamsForNow(int StudentPersonId)
		{
			ISessionManager sessionManager = new SessionManager(this.OpContext);
			Session currentSession = sessionManager.GetCurrentSession();
			ISettingManager settingManager = new SettingManager(this.OpContext);
			int settingValue = settingManager.GetSettingValue<int>(Setting.EXAMBOOKING_CourseEndDateAuthorizationExtensionInDays);
			string settingValue2 = settingManager.GetSettingValue<string>(Setting.EXAMBOOKING_SpecialAccommodations);
			List<SpecialAccommodation> source = SpecialAccommodation.LoadSpecialAccommodations(settingValue2, "");
			List<int> list = (from h in (from g in source
			where g.IsActive && g.SpecialAccommodationType == SpecialAccommodationType.CantBookOnline
			select g.ControlId).Distinct<int>()
			where h > 0
			select h).ToList<int>();
			DateTime startDate = currentSession.StartDate;
			DateTime endDate = (settingValue > 0) ? currentSession.EndDate.AddDays((double)settingValue) : currentSession.EndDate;
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
			List<CourseRegistration> list2 = courseRegistrationManager.LoadStudentsCourses(startDate, endDate, StudentPersonId, false);
			bool flag = list.Count < 1;
			StudentCourseList result;
			if (flag)
			{
				result = new StudentCourseList
				{
					Courses = list2
				};
			}
			else
			{
				IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
				IList<int> coursesStudentHasCantBookCheckedIn = accommodationsManager.LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(StudentPersonId, list.ToArray(), (from g in list2
				select g.Course.LuCourseId).ToArray<int>());
				StudentCourseList studentCourseList2;
				if (coursesStudentHasCantBookCheckedIn.Count >= 1)
				{
					StudentCourseList studentCourseList = new StudentCourseList();
					studentCourseList.Courses = (from g in list2
					where !coursesStudentHasCantBookCheckedIn.Contains(g.Course.LuCourseId)
					select g).ToList<CourseRegistration>();
					studentCourseList2 = studentCourseList;
					studentCourseList.AtLeastOneCourseRemovedBecauseOfSpecialAccommodationNotAllowedToBookRestriction = true;
				}
				else
				{
					(studentCourseList2 = new StudentCourseList()).Courses = list2;
				}
				result = studentCourseList2;
			}
			return result;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00053AD4 File Offset: 0x00051CD4
		public CourseRegistration LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid)
		{
			return this.dao.LoadCourseRegistrationsByStudentAndCourse(StudentPid, Lucid);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00053AF4 File Offset: 0x00051CF4
		public IList<PersonBase> LoadStudentsWithActiveRegisteredCoursesAndActiveAccommodations(DateTime StartDate, DateTime EndDate)
		{
			ISettingManager settingManager = new SettingManager(this.OpContext);
			int settingValue = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			return this.dao.LoadStudentsWithActiveRegisteredCoursesAndActiveAccommodations(StartDate, EndDate, settingValue);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00053B2C File Offset: 0x00051D2C
		public IList<CourseRegistration> LoadStudentsCoursesBatch(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, bool IncludeDroppedCourses)
		{
			return this.dao.LoadStudentsCoursesBatch(StartDate, EndDate, PersonIds, IncludeDroppedCourses);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00053B50 File Offset: 0x00051D50
		public IList<CourseRegistration> LoadActiveStudentsWithCourses(DateTime StartDate, DateTime EndDate, bool IncludeDroppedCourses = false)
		{
			return this.dao.LoadActiveStudentsWithCourses(StartDate, EndDate, IncludeDroppedCourses);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00053B70 File Offset: 0x00051D70
		public bool IsInstructorOrAltContactTeachingStudentsCourse(int StudentPersonId, int LuCourseId, int InstructorId, int AlternateContactId)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
			CourseRegistration courseRegistration = courseRegistrationManager.LoadCourseRegistrationsByStudentAndCourse(StudentPersonId, LuCourseId);
			bool flag = courseRegistration == null || courseRegistration.Course == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = InstructorId > 0 && courseRegistration.Course.Instructors != null && courseRegistration.Course.Instructors.FirstOrDefault((LookupInstructor g) => g.InstructorId == InstructorId) != null;
				result = (flag2 || (AlternateContactId > 0 && courseRegistration.Course.AlternateContacts != null && courseRegistration.Course.AlternateContacts.FirstOrDefault((AlternateContact g) => g.AlternateContactId == AlternateContactId) != null));
			}
			return result;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00053C44 File Offset: 0x00051E44
		public int[] LoadStudentCourseRegistrationLuCourseIds(int studentPersonId, bool includeDroppedCourses)
		{
			return this.dao.LoadStudentCourseRegistrationLuCourseIds(studentPersonId, includeDroppedCourses);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00053C64 File Offset: 0x00051E64
		public IList<StudentWithCourseAndAccommodationInfo> LoadStudentsWithCourseAndAccommodationInfosByCourseIds(params int[] lucids)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			int settingValue2 = webSettingManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
			ICourseRegistrationDAO courseRegistrationDAO = new CourseRegistrationDAO(this.OpContext);
			return courseRegistrationDAO.LoadStudentsWithCourseAndAccommodationInfosByCourseIds(settingValue, settingValue2, lucids);
		}
	}
}
