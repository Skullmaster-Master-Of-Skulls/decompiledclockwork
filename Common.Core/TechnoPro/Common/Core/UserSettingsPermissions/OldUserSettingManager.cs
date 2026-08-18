using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.Impl.UserSettingsPermissions;
using TechnoPro.Common.DAO.UserSettingsPermissions;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.Core.UserSettingsPermissions
{
	// Token: 0x0200002B RID: 43
	public class OldUserSettingManager : IOldUserSettingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000766A File Offset: 0x0000586A
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007672 File Offset: 0x00005872
		public IOldSettingDAO OldSettingDAO { get; set; }

		// Token: 0x06000172 RID: 370 RVA: 0x0000767B File Offset: 0x0000587B
		public OldUserSettingManager()
		{
			this.OldSettingDAO = new OldSettingDAO(this.OpContext);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007697 File Offset: 0x00005897
		public OldUserSettingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.OldSettingDAO = new OldSettingDAO(opContext);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000076B6 File Offset: 0x000058B6
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000076BE File Offset: 0x000058BE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000176 RID: 374 RVA: 0x000076C8 File Offset: 0x000058C8
		private List<OldUserSetting> GetAllUserSettings(int WhoAmI)
		{
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			List<OldUserSetting> list = (List<OldUserSetting>)userDatabaseCacheStorageManager[WhoAmI, "uUserSettings"];
			bool flag = list == null;
			if (flag)
			{
				list = this.LoadAllUserSettings(WhoAmI);
				userDatabaseCacheStorageManager.Insert(WhoAmI, "uUserSettings", list, TimeSpan.FromMinutes(30.0));
			}
			return list;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007734 File Offset: 0x00005934
		[DebuggerStepThrough]
		private Task<List<OldUserSetting>> GetAllUserSettingsAsync(int WhoAmI)
		{
			OldUserSettingManager.<GetAllUserSettingsAsync>d__11 <GetAllUserSettingsAsync>d__ = new OldUserSettingManager.<GetAllUserSettingsAsync>d__11();
			<GetAllUserSettingsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<OldUserSetting>>.Create();
			<GetAllUserSettingsAsync>d__.<>4__this = this;
			<GetAllUserSettingsAsync>d__.WhoAmI = WhoAmI;
			<GetAllUserSettingsAsync>d__.<>1__state = -1;
			<GetAllUserSettingsAsync>d__.<>t__builder.Start<OldUserSettingManager.<GetAllUserSettingsAsync>d__11>(ref <GetAllUserSettingsAsync>d__);
			return <GetAllUserSettingsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007780 File Offset: 0x00005980
		private List<OldUserSetting> GetUserSettings(int WhoAmI, eSettingCode settingCode)
		{
			List<OldUserSetting> allUserSettings = this.GetAllUserSettings(WhoAmI);
			return allUserSettings.GetUserSettings(settingCode);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000077A4 File Offset: 0x000059A4
		public bool UserHasAnySettings(int WhoAmI, eSettingCode settingCode)
		{
			List<OldUserSetting> userSettings = this.GetUserSettings(WhoAmI, settingCode);
			return userSettings.Count > 0;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000077C8 File Offset: 0x000059C8
		public List<int> GetSettingValue_ConcatenatedIntList(int WhoAmI, eSettingCode settingCode)
		{
			return this.GetSettingValue_ConcatenatedIntList(WhoAmI, settingCode, null);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000077E4 File Offset: 0x000059E4
		[DebuggerStepThrough]
		public Task<List<int>> GetSettingValue_ConcatenatedIntListAsync(int WhoAmI, eSettingCode settingCode)
		{
			OldUserSettingManager.<GetSettingValue_ConcatenatedIntListAsync>d__15 <GetSettingValue_ConcatenatedIntListAsync>d__ = new OldUserSettingManager.<GetSettingValue_ConcatenatedIntListAsync>d__15();
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<int>>.Create();
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>4__this = this;
			<GetSettingValue_ConcatenatedIntListAsync>d__.WhoAmI = WhoAmI;
			<GetSettingValue_ConcatenatedIntListAsync>d__.settingCode = settingCode;
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>1__state = -1;
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>t__builder.Start<OldUserSettingManager.<GetSettingValue_ConcatenatedIntListAsync>d__15>(ref <GetSettingValue_ConcatenatedIntListAsync>d__);
			return <GetSettingValue_ConcatenatedIntListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007838 File Offset: 0x00005A38
		public List<int> GetSettingValue_ConcatenatedIntList(int WhoAmI, eSettingCode settingCode, string overrideDefaultValue)
		{
			List<OldUserSetting> allUserSettings = this.GetAllUserSettings(WhoAmI);
			return allUserSettings.GetSettingValue_ConcatenatedIntList(settingCode, overrideDefaultValue);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000785C File Offset: 0x00005A5C
		[DebuggerStepThrough]
		public Task<List<int>> GetSettingValue_ConcatenatedIntListAsync(int WhoAmI, eSettingCode settingCode, string overrideDefaultValue)
		{
			OldUserSettingManager.<GetSettingValue_ConcatenatedIntListAsync>d__17 <GetSettingValue_ConcatenatedIntListAsync>d__ = new OldUserSettingManager.<GetSettingValue_ConcatenatedIntListAsync>d__17();
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<int>>.Create();
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>4__this = this;
			<GetSettingValue_ConcatenatedIntListAsync>d__.WhoAmI = WhoAmI;
			<GetSettingValue_ConcatenatedIntListAsync>d__.settingCode = settingCode;
			<GetSettingValue_ConcatenatedIntListAsync>d__.overrideDefaultValue = overrideDefaultValue;
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>1__state = -1;
			<GetSettingValue_ConcatenatedIntListAsync>d__.<>t__builder.Start<OldUserSettingManager.<GetSettingValue_ConcatenatedIntListAsync>d__17>(ref <GetSettingValue_ConcatenatedIntListAsync>d__);
			return <GetSettingValue_ConcatenatedIntListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000078B8 File Offset: 0x00005AB8
		public string GetSettingValue_String(int WhoAmI, eSettingCode settingCode)
		{
			return this.GetSettingValue_String(WhoAmI, settingCode, false, null);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000078D4 File Offset: 0x00005AD4
		public string GetSettingValue_String(int WhoAmI, eSettingCode settingCode, bool concatenateValues)
		{
			return this.GetSettingValue_String(WhoAmI, settingCode, concatenateValues, null);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000078F0 File Offset: 0x00005AF0
		public string GetSettingValue_String(int WhoAmI, eSettingCode settingCode, string overrideDefaultValue)
		{
			return this.GetSettingValue_String(WhoAmI, settingCode, false, overrideDefaultValue);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000790C File Offset: 0x00005B0C
		public string GetSettingValue_String(int WhoAmI, eSettingCode settingCode, bool concatenateValues, string overrideDefaultValue)
		{
			List<OldUserSetting> allUserSettings = this.GetAllUserSettings(WhoAmI);
			return allUserSettings.GetSettingValue_String(settingCode, concatenateValues, overrideDefaultValue);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007930 File Offset: 0x00005B30
		public int GetSettingValue_Int(int WhoAmI, eSettingCode settingCode)
		{
			List<OldUserSetting> allUserSettings = this.GetAllUserSettings(WhoAmI);
			return allUserSettings.GetSettingValue_Int(settingCode);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007954 File Offset: 0x00005B54
		public bool GetSettingValue_Bool(int WhoAmI, eSettingCode settingCode)
		{
			List<OldUserSetting> allUserSettings = this.GetAllUserSettings(WhoAmI);
			return allUserSettings.GetSettingValue_Bool(settingCode);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007978 File Offset: 0x00005B78
		public bool GetSettingValue_Bool(int WhoAmI, eSettingCode settingCode, bool defaultValue)
		{
			List<OldUserSetting> allUserSettings = this.GetAllUserSettings(WhoAmI);
			return allUserSettings.GetSettingValue_Bool(settingCode, new bool?(defaultValue));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000079A0 File Offset: 0x00005BA0
		public List<OldUserSetting> LoadAllUserSettings(int WhoAmI)
		{
			OperationContext opContext = this.OpContext;
			int num = (opContext != null) ? opContext.WhoAmI : 0;
			bool flag = num == WhoAmI || WhoAmI <= 0;
			List<OldUserSetting> result;
			if (flag)
			{
				result = this.OldSettingDAO.LoadAllUserSettings(WhoAmI);
			}
			else
			{
				PeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				bool flag2 = !peopleGroupManager.IsAdmin(num);
				if (flag2)
				{
					throw new PermissionDeniedException("Not admin");
				}
				result = this.OldSettingDAO.LoadAllUserSettings(WhoAmI);
			}
			return result;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007A1C File Offset: 0x00005C1C
		[DebuggerStepThrough]
		public Task<List<OldUserSetting>> LoadAllUserSettingsAsync(int WhoAmI)
		{
			OldUserSettingManager.<LoadAllUserSettingsAsync>d__26 <LoadAllUserSettingsAsync>d__ = new OldUserSettingManager.<LoadAllUserSettingsAsync>d__26();
			<LoadAllUserSettingsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<OldUserSetting>>.Create();
			<LoadAllUserSettingsAsync>d__.<>4__this = this;
			<LoadAllUserSettingsAsync>d__.WhoAmI = WhoAmI;
			<LoadAllUserSettingsAsync>d__.<>1__state = -1;
			<LoadAllUserSettingsAsync>d__.<>t__builder.Start<OldUserSettingManager.<LoadAllUserSettingsAsync>d__26>(ref <LoadAllUserSettingsAsync>d__);
			return <LoadAllUserSettingsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007A68 File Offset: 0x00005C68
		public IList<OldUserSetting> LoadPersonSettings(int PersonId)
		{
			int num = (this.OpContext == null) ? 0 : this.OpContext.WhoAmI;
			bool flag = num != PersonId && num > 0;
			if (flag)
			{
				PeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				bool flag2 = !peopleGroupManager.IsAdmin(num);
				if (flag2)
				{
					throw new PermissionDeniedException("Not admin");
				}
			}
			return this.OldSettingDAO.LoadPersonSettings(PersonId);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public IList<OldUserSetting> LoadGroupSettings(int GroupId)
		{
			int num = (this.OpContext == null) ? 0 : this.OpContext.WhoAmI;
			PeopleManager peopleManager = new PeopleManager(this.OpContext);
			bool flag = !peopleManager.IsUserInGroup(num, GroupId);
			if (flag)
			{
				PeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				bool flag2 = num > 0 && !peopleGroupManager.IsAdmin(num);
				if (flag2)
				{
					throw new PermissionDeniedException("Not admin");
				}
			}
			return this.OldSettingDAO.LoadGroupSettings(GroupId);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007B5C File Offset: 0x00005D5C
		public IList<OldUserSetting> LoadEveryoneSettings()
		{
			return this.OldSettingDAO.LoadEveryoneSettings();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007B7C File Offset: 0x00005D7C
		public void UpdateUserSettings(int WhoAmI, int PersonId, List<OldUserSetting> Settings)
		{
			foreach (OldUserSetting oldUserSetting in Settings)
			{
				oldUserSetting.PersonOrGroupId = PersonId;
				oldUserSetting.SettingType = eOldUserSettingType.PersonSetting;
			}
			this.SaveSettings(Settings);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public void UpdateGroupSettings(int WhoAmI, int GroupId, List<OldUserSetting> Settings)
		{
			foreach (OldUserSetting oldUserSetting in Settings)
			{
				oldUserSetting.PersonOrGroupId = GroupId;
				oldUserSetting.SettingType = ((GroupId > 0) ? eOldUserSettingType.GroupSetting : eOldUserSettingType.EveryoneSetting);
			}
			this.SaveSettings(Settings);
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			userDatabaseCacheStorageManager.Remove(OldUserSettingManager.cacheKeys);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007C70 File Offset: 0x00005E70
		public void SaveSettings(IList<OldUserSetting> Settings)
		{
			List<int> list = new List<int>();
			foreach (OldUserSetting oldUserSetting in Settings)
			{
				eOldUserSettingType settingType = oldUserSetting.SettingType;
				eOldUserSettingType eOldUserSettingType = settingType;
				if (eOldUserSettingType - eOldUserSettingType.GroupSetting > 1)
				{
					bool flag = oldUserSetting.ModificationStatus == eDataItemModificationStatus.Deleted;
					if (flag)
					{
						this.OldSettingDAO.DeletePersonSettingValue(oldUserSetting);
					}
					else
					{
						this.OldSettingDAO.CreateOrUpdatePersonSettingValue(oldUserSetting);
					}
				}
				else
				{
					bool flag2 = oldUserSetting.ModificationStatus == eDataItemModificationStatus.Deleted;
					if (flag2)
					{
						this.OldSettingDAO.DeleteGroupSettingValue(oldUserSetting);
					}
					else
					{
						this.OldSettingDAO.CreateOrUpdateGroupSettingValue(oldUserSetting);
					}
				}
				bool flag3 = oldUserSetting.SettingType == eOldUserSettingType.PersonSetting && !list.Contains(oldUserSetting.PersonOrGroupId);
				if (flag3)
				{
					list.Add(oldUserSetting.PersonOrGroupId);
				}
			}
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			userDatabaseCacheStorageManager.Remove(OldUserSettingManager.cacheKeys);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007D84 File Offset: 0x00005F84
		public void ClearCacheForUser(int PersonId)
		{
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			foreach (string key in OldUserSettingManager.cacheKeys)
			{
				userDatabaseCacheStorageManager.Remove(PersonId, key);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007DCC File Offset: 0x00005FCC
		public OldUserSetting GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode)
		{
			return this.OldSettingDAO.GetUserPersonalSettingValue(PersonId, SettingCode);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007DEB File Offset: 0x00005FEB
		public void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal)
		{
			this.OldSettingDAO.SetUserPersonalSettingValue(PersonId, SettingCode, IntVal, StringVal);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007E00 File Offset: 0x00006000
		public OldUserSettingReportForUserSet LoadUserSettingReportForUserSet(int PersonId)
		{
			List<OldUserSetting> list = this.LoadAllUserSettings(PersonId).ToList<OldUserSetting>();
			list.Sort(delegate(OldUserSetting g1, OldUserSetting g2)
			{
				bool flag2 = g1.SettingCode != g2.SettingCode;
				int result;
				if (flag2)
				{
					result = g1.SettingCode.CompareTo(g2.SettingCode);
				}
				else
				{
					bool flag3 = g1.SettingType != g2.SettingType;
					if (flag3)
					{
						result = g1.SettingType.CompareTo(g2.SettingType);
					}
					else
					{
						result = g1.PersonOrGroupId.CompareTo(g2.PersonOrGroupId);
					}
				}
				return result;
			});
			OldUserSettingReportForUserSet oldUserSettingReportForUserSet = new OldUserSettingReportForUserSet
			{
				PersonId = PersonId,
				SettingsWithReports = new List<OldUserSettingReportForUser>()
			};
			int j;
			for (int i = 0; i < list.Count; i = j)
			{
				OldUserSetting oldUserSetting = list[i];
				for (j = i + 1; j < list.Count; j++)
				{
					OldUserSetting oldUserSetting2 = list[j];
					bool flag = oldUserSetting.SettingCode != oldUserSetting2.SettingCode;
					if (flag)
					{
						break;
					}
				}
				OldUserSettingReportForUser oldUserSettingReportForUser = new OldUserSettingReportForUser();
				oldUserSettingReportForUser.SettingCode = oldUserSetting.SettingCode;
				oldUserSettingReportForUser.Items = (from g in list.GetRange(i, j - i)
				select new OldUserSettingReportForUserItem
				{
					PersonOrGroupId = g.PersonOrGroupId,
					SettingType = g.SettingType,
					IntVal = g.IntVal,
					StringVal = g.StringVal
				}).ToList<OldUserSettingReportForUserItem>();
				OldUserSettingReportForUser item = oldUserSettingReportForUser;
				oldUserSettingReportForUserSet.SettingsWithReports.Add(item);
			}
			return oldUserSettingReportForUserSet;
		}

		// Token: 0x04000054 RID: 84
		private static string[] cacheKeys = new string[]
		{
			"uAllowedAppointmentTypes",
			"uUserSettings",
			"uAllowedResourcePids",
			"uAllowedRoomPids",
			"uAllowedStaffPids",
			"uAllowedStudentPids"
		};
	}
}
