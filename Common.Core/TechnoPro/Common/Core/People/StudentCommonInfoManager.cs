using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A8 RID: 168
	public class StudentCommonInfoManager : IStudentCommonInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00022D0C File Offset: 0x00020F0C
		private DynamicFieldManager dynamicFieldManager
		{
			get
			{
				DynamicFieldManager result;
				if ((result = this._dynamicFieldManager) == null)
				{
					result = (this._dynamicFieldManager = new DynamicFieldManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00022D38 File Offset: 0x00020F38
		private IDynamicDataDAO dynamicDataDao
		{
			get
			{
				IDynamicDataDAO result;
				if ((result = this._dynamicDataDao) == null)
				{
					result = (this._dynamicDataDao = new DynamicDataDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00022D63 File Offset: 0x00020F63
		public StudentCommonInfoManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new StudentCommonInfoDAO(this.OpContext);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00022D86 File Offset: 0x00020F86
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00022D8E File Offset: 0x00020F8E
		public OperationContext OpContext { get; set; }

		// Token: 0x060005F1 RID: 1521 RVA: 0x00022D98 File Offset: 0x00020F98
		public PersonBase LoadStudentByEmailAddress(string EmailAddress)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_EmailControlID);
			bool flag = settingValue_Int < 1;
			PersonBase result;
			if (flag)
			{
				CWLogger.Logger.Warn("StudentCommonInfoManager.LoadStudentByEmailAddress:Email control id is not defined for user " + this.OpContext.WhoAmI.ToString());
				result = null;
			}
			else
			{
				DynamicField dynamicField = this.dynamicFieldManager.LoadFieldByControlId(settingValue_Int);
				bool flag2 = dynamicField == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IList<PersonBase> list = this.dynamicDataDao.LoadStudentByDataItem(eDynamicFormType.PerStudent, dynamicField, EmailAddress);
					bool flag3 = list == null || list.Count < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = list[0];
					}
				}
			}
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00022E54 File Offset: 0x00021054
		public IList<StudentWithCommonInfo> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0)
		{
			bool flag = CounsellorPersonId != this.OpContext.WhoAmI;
			if (flag)
			{
			}
			return this.dao.LoadMyStudents(CounsellorPersonId, StartDate, EndDate, ShowStudentsIHaveAppsWith, ShowStudentsIAmAdvisorFor, IncludeCancelledAppointments, IncludeNoShowAppointments, OverrideAssignedAdvisorControlId);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00022E98 File Offset: 0x00021098
		public StudentWithCommonInfo LoadStudentWithCommonInfo(int PersonId)
		{
			return this.dao.LoadStudentWithCommonInfo(PersonId);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00022EB8 File Offset: 0x000210B8
		public StudentCommonInfo LoadStudentCommonInfo(int PersonId)
		{
			return this.dao.LoadStudentCommonInfo(PersonId);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00022ED8 File Offset: 0x000210D8
		public IList<StudentWithCommonInfo> LoadStudentsWithCommonInfo(IList<int> PersonIds)
		{
			return this.dao.LoadStudentsWithCommonInfo(PersonIds);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00022EF8 File Offset: 0x000210F8
		[DebuggerStepThrough]
		public Task<IList<StudentWithCommonInfo>> LoadStudentsWithCommonInfoAsync(IList<int> PersonIds)
		{
			StudentCommonInfoManager.<LoadStudentsWithCommonInfoAsync>d__17 <LoadStudentsWithCommonInfoAsync>d__ = new StudentCommonInfoManager.<LoadStudentsWithCommonInfoAsync>d__17();
			<LoadStudentsWithCommonInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentWithCommonInfo>>.Create();
			<LoadStudentsWithCommonInfoAsync>d__.<>4__this = this;
			<LoadStudentsWithCommonInfoAsync>d__.PersonIds = PersonIds;
			<LoadStudentsWithCommonInfoAsync>d__.<>1__state = -1;
			<LoadStudentsWithCommonInfoAsync>d__.<>t__builder.Start<StudentCommonInfoManager.<LoadStudentsWithCommonInfoAsync>d__17>(ref <LoadStudentsWithCommonInfoAsync>d__);
			return <LoadStudentsWithCommonInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400012E RID: 302
		private DynamicFieldManager _dynamicFieldManager;

		// Token: 0x0400012F RID: 303
		private IDynamicDataDAO _dynamicDataDao;

		// Token: 0x04000130 RID: 304
		private IStudentCommonInfoDAO dao;
	}
}
