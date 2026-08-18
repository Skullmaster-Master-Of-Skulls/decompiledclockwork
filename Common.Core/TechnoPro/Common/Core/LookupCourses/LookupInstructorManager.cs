using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D3 RID: 211
	public class LookupInstructorManager : ILookupInstructorManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0003760D File Offset: 0x0003580D
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00037615 File Offset: 0x00035815
		public ILookupInstructorDAO LookupInstructorDAO { get; set; }

		// Token: 0x06000806 RID: 2054 RVA: 0x0003761E File Offset: 0x0003581E
		public LookupInstructorManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.LookupInstructorDAO = new LookupInstructorDAO(opContext);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0003763D File Offset: 0x0003583D
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00037645 File Offset: 0x00035845
		public OperationContext OpContext { get; set; }

		// Token: 0x06000809 RID: 2057 RVA: 0x00037650 File Offset: 0x00035850
		public List<LookupInstructor> LoadAllAssignedInstructors()
		{
			return this.LookupInstructorDAO.LoadAllAssignedInstructors();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00037670 File Offset: 0x00035870
		public LookupInstructor LoadInstructor(int InstructorId)
		{
			return this.LookupInstructorDAO.LoadInstructor(InstructorId);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00037690 File Offset: 0x00035890
		public List<LookupInstructor> SaveInstructorsForCourse(int LuCourseId, List<LookupInstructor> Instructors, bool updateInstructorInfo)
		{
			this.LookupInstructorDAO.SaveInstructorsForCourse(LuCourseId, Instructors, updateInstructorInfo);
			return Instructors;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000376B4 File Offset: 0x000358B4
		public int SaveInstructor(LookupInstructor instructor)
		{
			this.LookupInstructorDAO.SaveInstructor(instructor);
			return instructor.InstructorId;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000376DC File Offset: 0x000358DC
		public LookupInstructor LoadInstructorByUsername(string username)
		{
			return this.LookupInstructorDAO.LoadInstructorByUsername(username);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000376FC File Offset: 0x000358FC
		public LookupInstructor LoadInstructorByEmployeeId(string employeeId)
		{
			return this.LookupInstructorDAO.LoadInstructorByEmployeeId(employeeId);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0003771C File Offset: 0x0003591C
		public LookupInstructor LoadInstructorByEmail(string email)
		{
			return this.LookupInstructorDAO.LoadInstructorByEmail(email);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0003773C File Offset: 0x0003593C
		public IList<LookupInstructor> LoadInstructorsBySearchString(string SearchString)
		{
			return this.LookupInstructorDAO.LoadInstructorsBySearchString(SearchString);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0003775A File Offset: 0x0003595A
		public void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync)
		{
			this.LookupInstructorDAO.AssignInstructorToCourse(InstructorId, LuCourseId, IsAssignmentExemptFromDataSync);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0003776C File Offset: 0x0003596C
		public void RemoveInstructorFromCourse(int InstructorId, int LuCourseId)
		{
			this.LookupInstructorDAO.RemoveInstructorFromCourse(InstructorId, LuCourseId);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00037780 File Offset: 0x00035980
		public IList<LookupInstructor> LoadInstructorsByCourse(int LuCourseId)
		{
			return this.LookupInstructorDAO.LoadInstructorsByCourse(LuCourseId);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0003779E File Offset: 0x0003599E
		public void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus)
		{
			this.LookupInstructorDAO.UpdateInstructorDataSyncExemption(InstructorId, NewInstructorExemptStatus);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000377B0 File Offset: 0x000359B0
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId)
		{
			return this.LookupInstructorDAO.GetUniqueCourseRegistrationStartDatesByInstructor(InstructorId);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000377D0 File Offset: 0x000359D0
		public List<LookupCourse> LoadInstructorCourses(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate)
		{
			return this.LookupInstructorDAO.LoadInstructorCourses(InstructorId, AlternateContactId, PermissionLevel, MustHaveClassTestDefinition, StartDate, EndDate, false);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000377F8 File Offset: 0x000359F8
		public IList<int> LoadInstructorOrAltContactAssignedLuCourseIds(int InstructorId, int AlternateContactId, bool MustHaveClassTestDefinition, bool MustHaveOneRegisteredStudent)
		{
			return this.LookupInstructorDAO.LoadInstructorOrAltContactAssignedLuCourseIds(InstructorId, AlternateContactId, MustHaveClassTestDefinition, MustHaveOneRegisteredStudent);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003781C File Offset: 0x00035A1C
		public IList<LookupCourse> LoadInstructorCoursesWithAtLeastOneStudentRegistered(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate)
		{
			return this.LookupInstructorDAO.LoadInstructorCourses(InstructorId, AlternateContactId, PermissionLevel, MustHaveClassTestDefinition, StartDate, EndDate, true);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00037844 File Offset: 0x00035A44
		public IList<StudentWithRequestAndCourseInfo> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, string ClockWorkSettingsInstanceName)
		{
			bool flag = string.IsNullOrEmpty(ClockWorkSettingsInstanceName);
			ISettingManager settingManager;
			if (flag)
			{
				settingManager = SettingManager.CurrentInstance;
			}
			else
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				string key = "settingsInstance_" + ClockWorkSettingsInstanceName;
				settingManager = (SettingManager)cacheStorageManager[key];
				bool flag2 = settingManager == null;
				if (flag2)
				{
					settingManager = new SettingManager(ClockWorkSettingsInstanceName);
					cacheStorageManager.Insert(key, settingManager, TimeSpan.FromHours(1.0));
				}
			}
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
			bool flag3 = !settingValue;
			IList<StudentWithRequestAndCourseInfo> result;
			if (flag3)
			{
				result = new List<StudentWithRequestAndCourseInfo>();
			}
			else
			{
				bool settingValue2 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDate);
				bool settingValue3 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForStudentsWhereTheLetterWasGenerated);
				bool settingValue4 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDateAndSelfRegApproved);
				int showIfActiveAccommodationsExpiry_AccExpiryCid = (settingValue2 || settingValue4) ? settingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid) : 0;
				bool settingValue5 = settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired);
				List<StudentWithRequestAndCourseInfo> list = this.LookupInstructorDAO.GetStudentsWithApprovedRequestsByCourseDate(InstructorId, AlternateContactId, StartDate, EndDate, showIfActiveAccommodationsExpiry_AccExpiryCid, settingValue3, settingValue5, settingValue4).ToList<StudentWithRequestAndCourseInfo>();
				int settingValue6 = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				bool flag4 = settingValue6 > 0;
				if (flag4)
				{
					LookupInstructorManager.<>c__DisplayClass25_0 CS$<>8__locals1 = new LookupInstructorManager.<>c__DisplayClass25_0();
					bool settingValue7 = settingManager.GetSettingValue<bool>(Setting.INSTRUCTOR_ReverseDontShowStudentAccommodationCid);
					IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(this.OpContext);
					List<int> list2 = (from g in list.Select(delegate(StudentWithRequestAndCourseInfo g)
					{
						PersonBase student = g.Student;
						return (student != null) ? student.PersonId : 0;
					})
					where g > 0
					select g).Distinct<int>().ToList<int>();
					DataTable dataTable = new DataTable("t0");
					dataTable.Columns.Add("personid", typeof(int));
					foreach (int num in list2)
					{
						dataTable.Rows.Add(new object[]
						{
							num
						});
					}
					DataTable dataTable2 = dynamicDataForReportsManager.CrossReferenceAccommodationDataTemplateOnly(dataTable, new List<int>
					{
						settingValue6
					});
					CS$<>8__locals1.tPids = (from DataRow dr in dataTable2.Rows
					select (dr["personid"] is DBNull) ? 0 : ((int)dr["personid"])).ToArray<int>();
					LookupInstructorManager.<>c__DisplayClass25_0 CS$<>8__locals2 = CS$<>8__locals1;
					IEnumerable<int> noPids;
					if (!settingValue7)
					{
						noPids = from DataRow dr in dataTable2.Rows
						select (dr["personid"] is DBNull) ? 0 : ((int)dr["personid"]);
					}
					else
					{
						noPids = from g in list2
						where !CS$<>8__locals1.tPids.Contains(g)
						select g;
					}
					CS$<>8__locals2.noPids = noPids;
					list = (from g in list
					where g.Student != null && CS$<>8__locals1.noPids.All((int h) => g.Student.PersonId != h)
					select g).ToList<StudentWithRequestAndCourseInfo>();
				}
				list.Sort((StudentWithRequestAndCourseInfo g1, StudentWithRequestAndCourseInfo g2) => g2.RequestDate.CompareTo(g1.RequestDate));
				result = list;
			}
			return result;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00037B4C File Offset: 0x00035D4C
		public int[] FindAllCoursesAnInstructorOrAltContactIsAllowed(int instructorId, int altContactId, int permissionLevel)
		{
			ILookupInstructorDAO lookupInstructorDAO = new LookupInstructorDAO(this.OpContext);
			return lookupInstructorDAO.FindAllCoursesAnInstructorOrAltContactIsAllowed(instructorId, altContactId, permissionLevel);
		}
	}
}
