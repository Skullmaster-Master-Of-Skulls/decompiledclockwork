using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000F7 RID: 247
	public class AccommodationsManager : IAccommodationsManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0003CC24 File Offset: 0x0003AE24
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0003CC2C File Offset: 0x0003AE2C
		public IAccommodationsDAO dao { get; set; }

		// Token: 0x0600099F RID: 2463 RVA: 0x0003CC35 File Offset: 0x0003AE35
		public AccommodationsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AccommodationsDAO(opContext);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0003CC54 File Offset: 0x0003AE54
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0003CC5C File Offset: 0x0003AE5C
		public OperationContext OpContext { get; set; }

		// Token: 0x060009A2 RID: 2466 RVA: 0x0003CC68 File Offset: 0x0003AE68
		public IList<AccommodationData> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int CourseId)
		{
			bool flag;
			return this.LoadAccommodationsByStudentAndCourseOrTemplate(PersonId, CourseId, out flag);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0003CC84 File Offset: 0x0003AE84
		public IList<AccommodationData> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int CourseId, out bool IsUsingTemplateAccommodations)
		{
			return this.dao.LoadAccommodationsByStudentAndCourseOrTemplate(PersonId, CourseId, out IsUsingTemplateAccommodations);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
		public List<DynamicDataChange> LoadAccommodationChanges(int PersonId, int LuCourseId, DateTime SinceDate)
		{
			return this.dao.LoadAccommodationChanges(PersonId, LuCourseId, SinceDate);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
		public DateTime? GetStudentAccommodationsExpiryDate(int PersonId)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			return this.dao.GetStudentAccommodationsExpiryDate(PersonId, settingValue);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003CD00 File Offset: 0x0003AF00
		public IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodations(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false)
		{
			IList<CourseRegistrationWithAccommodations> list = this.dao.LoadStudentsRegisteredCoursesWithAccommodations(PersonId, StartDate, EndDate, LoadAccommodations);
			if (IncludeOfflineAccommodations)
			{
				IList<CourseRegistrationWithAccommodations> list2 = this.dao.LoadStudentsRegisteredCoursesWithAccommodationsByCourse(PersonId, 1, LoadAccommodations);
				foreach (CourseRegistrationWithAccommodations item in list2)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0003CD80 File Offset: 0x0003AF80
		public IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false)
		{
			IList<CourseRegistrationWithAccommodations> list = this.dao.LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(PersonId, StartDate, EndDate, LoadAccommodations);
			if (IncludeOfflineAccommodations)
			{
				IList<CourseRegistrationWithAccommodations> list2 = this.dao.LoadStudentsAccommodationsAndRequestsForOfflineCourse(PersonId, LoadAccommodations);
				foreach (CourseRegistrationWithAccommodations item in list2)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0003CE00 File Offset: 0x0003B000
		public void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval)
		{
			this.dao.ClearAccommodations(PersonId, CourseId, RequiresApproval);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003CE12 File Offset: 0x0003B012
		public void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds)
		{
			this.dao.MarkAccommodationLetterIssued(PersonId, LuCourseIds);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003CE24 File Offset: 0x0003B024
		public void MergeOrReplaceAccommodations(bool ReplaceExistingAccommodations, int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_Accommodations_CidsToHideFromCourseTabsOnly);
			List<int> list = (settingValue_ConcatenatedIntList != null && settingValue_ConcatenatedIntList.Count > 0) ? settingValue_ConcatenatedIntList : new List<int>();
			int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			bool flag = settingValue > 0;
			if (flag)
			{
				list.Add(settingValue);
			}
			if (ReplaceExistingAccommodations)
			{
				this.dao.ReplaceAccommodations(SourcePersonId, SourceLuCourseId, DestPersonId, DestLuCourseId, list);
			}
			else
			{
				this.dao.MergeAccommodations(SourcePersonId, SourceLuCourseId, DestPersonId, DestLuCourseId, list);
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		public IList<DynamicDataSetWithStudentName> LoadActiveStudentsWithTemplateAccommodations(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadActiveStudentsWithTemplateAccommodations(StartDate, EndDate);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003CEDC File Offset: 0x0003B0DC
		public IList<int> LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(int PersonId, int[] cids, int[] lucids)
		{
			return this.dao.LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(PersonId, cids, lucids);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003CEFC File Offset: 0x0003B0FC
		public IDictionary<int, DateTime?> LoadAccommodationExpiryDatesForStudents(int[] pids)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			return this.dao.LoadAccommodationExpiryDatesForStudents(pids, settingValue);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003CF38 File Offset: 0x0003B138
		[DebuggerStepThrough]
		public Task<IDictionary<int, DateTime?>> LoadAccommodationExpiryDatesForStudentsAsync(int[] pids)
		{
			AccommodationsManager.<LoadAccommodationExpiryDatesForStudentsAsync>d__21 <LoadAccommodationExpiryDatesForStudentsAsync>d__ = new AccommodationsManager.<LoadAccommodationExpiryDatesForStudentsAsync>d__21();
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDictionary<int, DateTime?>>.Create();
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>4__this = this;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.pids = pids;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>1__state = -1;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>t__builder.Start<AccommodationsManager.<LoadAccommodationExpiryDatesForStudentsAsync>d__21>(ref <LoadAccommodationExpiryDatesForStudentsAsync>d__);
			return <LoadAccommodationExpiryDatesForStudentsAsync>d__.<>t__builder.Task;
		}
	}
}
