using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.Intake;
using TechnoPro.Common.DAO.Intake;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Intake;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Intake;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Intake
{
	// Token: 0x020000EC RID: 236
	public class IntakeAccountManager : IIntakeAccountManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0003AE4B File Offset: 0x0003904B
		public IntakeAccountManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0003AE5D File Offset: 0x0003905D
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0003AE65 File Offset: 0x00039065
		public OperationContext OpContext { get; set; }

		// Token: 0x0600092C RID: 2348 RVA: 0x0003AE70 File Offset: 0x00039070
		public int CreateNewIntakeAccount(IntakeUserAccount UserAccount)
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			return intakeAccountDAO.CreateNewIntakeAccount(UserAccount);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0003AE98 File Offset: 0x00039098
		public IList<IntakeEntry> LoadPendingIntakeEntries()
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			return intakeAccountDAO.LoadPendingIntakeEntries();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0003AEBC File Offset: 0x000390BC
		public IList<IntakeEntryQueueItem> LoadPendingIntakeEntryQueueItems()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Intake_MultiDepartmentIntakeSettings, false);
			MultiDepartmentIntakeSettings multiDepartmentIntakeSettings = (settingValue_String != null) ? settingValue_String.DeserializeMultiDepartmentIntakeSettings() : null;
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			return intakeAccountDAO.LoadPendingIntakeEntryQueueItems((multiDepartmentIntakeSettings != null) ? multiDepartmentIntakeSettings.DepartmentChooserControlId : 0);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0003AF20 File Offset: 0x00039120
		public void UpdateActiveIntakeStatus(string snum, Guid newIntakeStatusId)
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			int[] intakePersonIds = intakeAccountDAO.LoadIntakePersonIdsByStudentNumber(snum);
			this.UpdateActiveIntakeStatus(intakePersonIds, newIntakeStatusId);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0003AF4C File Offset: 0x0003914C
		public void UpdateActiveIntakeStatus(int[] intakePersonIds, Guid newIntakeStatusId)
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			intakeAccountDAO.UpdateActiveIntakeStatus(intakePersonIds, newIntakeStatusId);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003AF70 File Offset: 0x00039170
		public void UpdateActiveIntakeNote(int[] intakePersonIds, string newNote)
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			intakeAccountDAO.UpdateActiveIntakeNote(intakePersonIds, newNote);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0003AF94 File Offset: 0x00039194
		public void RemoveIntake(string student_no)
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			intakeAccountDAO.MarkIntakesInactiveByStudentNumber(student_no);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0003AFB8 File Offset: 0x000391B8
		public IList<IntakeStatus> LoadLookupStatuses()
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			return intakeAccountDAO.LoadLookupStatuses();
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0003AFDC File Offset: 0x000391DC
		public void UpdateActiveIntakeStatusAndNote(int[] intakePersonIds, string newNote, Guid newIntakeStatusId)
		{
			this.UpdateActiveIntakeStatus(intakePersonIds, newIntakeStatusId);
			this.UpdateActiveIntakeNote(intakePersonIds, newNote);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003AFF4 File Offset: 0x000391F4
		public CreateRealStudentAccountFromIntakeResult CreateRealStudentAccountFromIntakeAndRemoveIntake(string snum, IList<int> gids)
		{
			string text = (snum ?? "").Trim().ToUpper();
			bool flag = gids == null;
			if (flag)
			{
				gids = new List<int>();
			}
			bool flag2 = !gids.Contains(1);
			if (flag2)
			{
				gids.Add(1);
			}
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			IntakePerson intakePerson = intakeAccountDAO.LoadIntakePersonByStudentNumber(snum);
			PersonBase personBase = new PersonBase
			{
				Student_no = text,
				FirstName = (intakePerson.FirstName ?? "").Trim(),
				MiddleName = (intakePerson.MiddleName ?? "").Trim(),
				LastName = (intakePerson.LastName ?? "").Trim(),
				CoreGroup = eCoreGroup.Students,
				IsActivated = new bool?(true)
			};
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			IList<int> list = peopleManager.LoadPersonIdsByStudentNumbers(new string[]
			{
				text
			}.ToList<string>());
			int num;
			if (list == null)
			{
				num = 0;
			}
			else
			{
				num = (from g in list
				where g > 0
				select g).FirstOrDefault<int>();
			}
			int num2 = num;
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Intake_MultiDepartmentIntakeSettings, false);
			MultiDepartmentIntakeSettings multiDepartmentIntakeSettings = (settingValue_String != null) ? settingValue_String.DeserializeMultiDepartmentIntakeSettings() : null;
			bool flag3 = num2 > 0;
			int num3;
			if (flag3)
			{
				bool flag4 = multiDepartmentIntakeSettings != null && multiDepartmentIntakeSettings.IsEnabled;
				if (!flag4)
				{
					return new CreateRealStudentAccountFromIntakeResult
					{
						Status = eCreateRealStudentAccountFromIntakeStatus.FailedStudentNumberAlreadyExistsInClockWork
					};
				}
				PersonBase personBase2 = peopleManager.LoadPerson(num2);
				bool flag5 = personBase2 == null;
				if (flag5)
				{
					return new CreateRealStudentAccountFromIntakeResult
					{
						Status = eCreateRealStudentAccountFromIntakeStatus.FailedUnknown
					};
				}
				num3 = num2;
				bool flag6 = personBase2.Groups == null;
				if (flag6)
				{
					personBase2.Groups = new List<TechnoPro.Common.Public.Entities.People.Group>();
				}
				using (IEnumerator<int> enumerator = gids.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int g = enumerator.Current;
						bool flag7 = g > 0 && personBase2.Groups.All((TechnoPro.Common.Public.Entities.People.Group h) => h.GroupId != g);
						if (flag7)
						{
							personBase2.Groups.Add(new TechnoPro.Common.Public.Entities.People.Group
							{
								GroupId = g
							});
						}
					}
				}
				personBase.PersonId = num3;
				personBase.Groups = personBase2.Groups;
				peopleManager.UpdateUser(personBase, true);
			}
			else
			{
				num3 = peopleManager.CreateUser(personBase, gids.ToList<int>());
			}
			bool flag8 = num3 < 1;
			CreateRealStudentAccountFromIntakeResult result;
			if (flag8)
			{
				result = new CreateRealStudentAccountFromIntakeResult
				{
					Status = eCreateRealStudentAccountFromIntakeStatus.FailedUnknown
				};
			}
			else
			{
				try
				{
					IDataSyncDataManager dataSyncDataManager = new DataSyncDataManager(this.OpContext);
					dataSyncDataManager.DataSyncDataLegacy(text);
					bool flag9 = multiDepartmentIntakeSettings != null && multiDepartmentIntakeSettings.IsEnabled;
					if (flag9)
					{
						dataSyncDataManager.DataSyncIntakeData(text, true);
					}
					IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(this.OpContext);
					dataSyncCourseManager.DataSyncCourses(text, 0);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Warn("IntakeAccountManager:CreateRealStudentAccountFromIntakeAndRemoveIntake:Data sync failed: {0}", ex.ToString());
				}
				this.RemoveIntake(text);
				string text2 = (intakePerson.Email ?? "").Trim();
				bool flag10 = text2.Length > 0;
				if (flag10)
				{
					int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_EmailControlID);
					bool flag11 = settingValue_Int > 0;
					if (flag11)
					{
						IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
						IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
						dynamicDataManager.SaveData(new DynamicDataContext
						{
							PrimaryId = num3
						}, new DynamicData[]
						{
							new DynamicData
							{
								Field = dynamicFieldManager.LoadFieldByControlId(settingValue_Int),
								Value = text2
							}
						}.ToList<DynamicData>(), eDynamicFormType.PerStudent);
					}
				}
				result = new CreateRealStudentAccountFromIntakeResult
				{
					Status = eCreateRealStudentAccountFromIntakeStatus.SuccessfullyCreatedStudentAccount,
					PersonId = num3
				};
			}
			return result;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003B418 File Offset: 0x00039618
		public IList<DynamicData> LoadIntakeFormData(string snum)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.INTAKE_FormNum);
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			return intakeAccountDAO.LoadIntakeFormData(snum, settingValue);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003B45C File Offset: 0x0003965C
		public IDictionary<string, ePreIntakeStatus> GetIntakeStatuses(params string[] studentNumbers)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			IDictionary<string, int> snumsWithPids = peopleManager.LoadPersonIdsByStudentNumbers2(studentNumbers);
			List<int> allowedStudentPids = peopleManager.LoadAllowedStudentPids();
			return studentNumbers.ToDictionary((string g) => g, delegate(string g)
			{
				int num = snumsWithPids.ContainsKey(g) ? snumsWithPids[g] : 0;
				bool flag = num < 1;
				ePreIntakeStatus result;
				if (flag)
				{
					result = ePreIntakeStatus.ReadyToIntake;
				}
				else
				{
					bool flag2 = !allowedStudentPids.Contains(num);
					if (flag2)
					{
						result = ePreIntakeStatus.ReadyToIntake;
					}
					else
					{
						IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
						string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Intake_MultiDepartmentIntakeSettings, false);
						MultiDepartmentIntakeSettings multiDepartmentIntakeSettings = (settingValue_String != null) ? settingValue_String.DeserializeMultiDepartmentIntakeSettings() : null;
						bool flag3 = multiDepartmentIntakeSettings != null && multiDepartmentIntakeSettings.IsEnabled;
						if (flag3)
						{
							result = ePreIntakeStatus.ReadyToIntake;
						}
						else
						{
							result = ePreIntakeStatus.StudentNumberAlreadyExists;
						}
					}
				}
				return result;
			});
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003B4D4 File Offset: 0x000396D4
		public void RemoveIntakes(int[] intakePersonIds)
		{
			IIntakeAccountDAO intakeAccountDAO = new IntakeAccountDAO(this.OpContext);
			intakeAccountDAO.MarkIntakesInactiveByPersonIds(intakePersonIds);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003B4F8 File Offset: 0x000396F8
		public void SyncIntakeData(string snum, bool removeIntakesWhenDone)
		{
			IDataSyncDataManager dataSyncDataManager = new DataSyncDataManager(this.OpContext);
			dataSyncDataManager.DataSyncIntakeData(snum, removeIntakesWhenDone);
		}
	}
}
