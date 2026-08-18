using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.DynamicQueries;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.DynamicQueries;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x02000130 RID: 304
	public class AppointmentTypeManager : IAppointmentTypeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CD5 RID: 3285 RVA: 0x0005946E File Offset: 0x0005766E
		public AppointmentTypeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentTypeDAO(opContext);
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0005948C File Offset: 0x0005768C
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00059494 File Offset: 0x00057694
		public OperationContext OpContext { get; set; }

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000594A0 File Offset: 0x000576A0
		public IList<AppTypeGroupWithAppTypes> LoadAllAppTypesWithGroups(bool ignoreCache)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = eServerCacheItemType.uAllAppTypesWithGroups.ToString();
			IList<AppTypeGroupWithAppTypes> list = ignoreCache ? null : (cacheStorageManager[key] as IList<AppTypeGroupWithAppTypes>);
			bool flag = list != null;
			IList<AppTypeGroupWithAppTypes> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IList<AppTypeGroupWithAppTypes> list2 = this.dao.LoadAllAppTypeGroups(true);
				IList<AppType> list3 = this.dao.LoadOrphanAppTypes();
				bool flag2 = list3.Count > 0;
				if (flag2)
				{
					list2.Add(new AppTypeGroupWithAppTypes
					{
						Group = null,
						SubAppTypes = list3
					});
				}
				cacheStorageManager.Insert(key, list2, TimeSpan.FromHours(3.0));
				result = list2;
			}
			return result;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0005954C File Offset: 0x0005774C
		public AppType LoadAppTypeByAppointmentId(int appointmentId)
		{
			return this.dao.LoadAppTypeByAppointmentId(appointmentId);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0005956C File Offset: 0x0005776C
		public IList<int> GetAppointmentTypeAssociatedPerAppScreenNums(int AppTypeId)
		{
			return this.dao.GetAppointmentTypeAssociatedPerAppScreenNums(AppTypeId);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0005958C File Offset: 0x0005778C
		public List<AppType> LoadAllInactiveAppTypes()
		{
			string key = eServerCacheItemType.uAllInactiveAppTypes.ToString();
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AppType> list = (IList<AppType>)cacheStorageManager[key];
			bool flag = list == null;
			if (flag)
			{
				list = this.dao.LoadAllInactiveAppTypes();
				cacheStorageManager.Insert(key, list, TimeSpan.FromMinutes(10.0));
			}
			return list.ToList<AppType>();
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000595FC File Offset: 0x000577FC
		[DebuggerStepThrough]
		public Task<List<AppType>> LoadAllInactiveAppTypesAsync()
		{
			AppointmentTypeManager.<LoadAllInactiveAppTypesAsync>d__10 <LoadAllInactiveAppTypesAsync>d__ = new AppointmentTypeManager.<LoadAllInactiveAppTypesAsync>d__10();
			<LoadAllInactiveAppTypesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<AppType>>.Create();
			<LoadAllInactiveAppTypesAsync>d__.<>4__this = this;
			<LoadAllInactiveAppTypesAsync>d__.<>1__state = -1;
			<LoadAllInactiveAppTypesAsync>d__.<>t__builder.Start<AppointmentTypeManager.<LoadAllInactiveAppTypesAsync>d__10>(ref <LoadAllInactiveAppTypesAsync>d__);
			return <LoadAllInactiveAppTypesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00059640 File Offset: 0x00057840
		public List<AppType> LoadAllAppTypes()
		{
			return this.LoadAllAppTypes(false);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0005965C File Offset: 0x0005785C
		public List<AppType> LoadAllAppTypes(bool ignoreCache)
		{
			string key = eServerCacheItemType.uAllActiveAppTypes.ToString();
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AppType> list = ignoreCache ? null : ((IList<AppType>)cacheStorageManager[key]);
			bool flag = list != null;
			List<AppType> result;
			if (flag)
			{
				result = list.ToList<AppType>();
			}
			else
			{
				list = this.dao.LoadAllAppTypes();
				cacheStorageManager.Insert(key, list, TimeSpan.FromMinutes(10.0));
				result = list.ToList<AppType>();
			}
			return result;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000596D8 File Offset: 0x000578D8
		[DebuggerStepThrough]
		public Task<List<AppType>> LoadAllAppTypesAsync()
		{
			AppointmentTypeManager.<LoadAllAppTypesAsync>d__13 <LoadAllAppTypesAsync>d__ = new AppointmentTypeManager.<LoadAllAppTypesAsync>d__13();
			<LoadAllAppTypesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<AppType>>.Create();
			<LoadAllAppTypesAsync>d__.<>4__this = this;
			<LoadAllAppTypesAsync>d__.<>1__state = -1;
			<LoadAllAppTypesAsync>d__.<>t__builder.Start<AppointmentTypeManager.<LoadAllAppTypesAsync>d__13>(ref <LoadAllAppTypesAsync>d__);
			return <LoadAllAppTypesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0005971C File Offset: 0x0005791C
		[DebuggerStepThrough]
		public Task<List<AppType>> LoadAllAppTypesAsync(bool ignoreCache)
		{
			AppointmentTypeManager.<LoadAllAppTypesAsync>d__14 <LoadAllAppTypesAsync>d__ = new AppointmentTypeManager.<LoadAllAppTypesAsync>d__14();
			<LoadAllAppTypesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<AppType>>.Create();
			<LoadAllAppTypesAsync>d__.<>4__this = this;
			<LoadAllAppTypesAsync>d__.ignoreCache = ignoreCache;
			<LoadAllAppTypesAsync>d__.<>1__state = -1;
			<LoadAllAppTypesAsync>d__.<>t__builder.Start<AppointmentTypeManager.<LoadAllAppTypesAsync>d__14>(ref <LoadAllAppTypesAsync>d__);
			return <LoadAllAppTypesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00059768 File Offset: 0x00057968
		public AppType LoadAppTypeById(int AppTypeId)
		{
			return this.dao.LoadAppTypeById(AppTypeId);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00059786 File Offset: 0x00057986
		public void UpdateAppType(AppType AppType)
		{
			this.dao.UpdateAppType(AppType);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00059798 File Offset: 0x00057998
		public int CreateAppType(AppType AppType)
		{
			return this.dao.CreateAppType(AppType);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000597B8 File Offset: 0x000579B8
		public void DeleteAppType(int AppTypeId, int AppTypeIdToReplaceWith)
		{
			bool flag = AppTypeIdToReplaceWith > 0;
			if (flag)
			{
				this.dao.DeleteAppType(AppTypeId, AppTypeIdToReplaceWith);
			}
			else
			{
				IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
				int numberOfAppointmentsWithAppType = appointmentManager.GetNumberOfAppointmentsWithAppType(AppTypeId);
				bool flag2 = numberOfAppointmentsWithAppType > 0;
				if (flag2)
				{
					throw new InvalidParameterException("AppointmentTypeManager:DeleteAppType:AppTypeIdToReplaceWith cannot be 0:AppTypeIdToReplaceWith=" + AppTypeIdToReplaceWith.ToString() + ":AppTypeId=" + AppTypeId.ToString());
				}
				this.dao.DeleteAppType(AppTypeId, 0);
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0005982C File Offset: 0x00057A2C
		public void DisableAppType(int appTypeId)
		{
			this.dao.DisableAppType(appTypeId);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0005983C File Offset: 0x00057A3C
		public AppTypeGroup LoadAppTypeGroupById(int AppointmentTypeGroupId)
		{
			return this.dao.LoadAppTypeGroupById(AppointmentTypeGroupId);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0005985C File Offset: 0x00057A5C
		public IList<AppTypeGroupWithAppTypes> LoadAllAppTypeGroups()
		{
			return this.dao.LoadAllAppTypeGroups(false);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0005987C File Offset: 0x00057A7C
		public AppTypeGroupWithAppTypes LoadAppTypeGroupWithAppTypesById(int AppointmentTypeGroupId, bool IncludeInactiveAppTypes = false)
		{
			return this.dao.LoadAppTypeGroupWithAppTypesById(AppointmentTypeGroupId, IncludeInactiveAppTypes);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0005989B File Offset: 0x00057A9B
		public void DeleteAppTypeGroup(int AppointmentTypeGroupId)
		{
			this.dao.DeleteAppTypeGroup(AppointmentTypeGroupId);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000598AC File Offset: 0x00057AAC
		public int CreateAppTypeGroup(AppTypeGroup AppTypeGroup)
		{
			return this.dao.CreateAppTypeGroup(AppTypeGroup);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000598CA File Offset: 0x00057ACA
		public void UpdateAppTypeGroup(AppTypeGroup AppTypeGroup)
		{
			this.dao.UpdateAppTypeGroup(AppTypeGroup);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000598DC File Offset: 0x00057ADC
		public IList<int> GetAllowedAppTypeIds(int personId)
		{
			bool flag = this.OpContext.WhoAmI != personId;
			if (flag)
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				IList<int> groupIdsByPersonId = peopleGroupManager.GetGroupIdsByPersonId(this.OpContext.WhoAmI);
				bool flag2 = !groupIdsByPersonId.Contains(10);
				if (flag2)
				{
					throw new PermissionDeniedException("Not allowed to ask for allowed appointment type ids for another user when you are not super admin.");
				}
			}
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "u" + eServerCacheItemType.uAllowedAppTypeIds.ToString() + "_" + personId.ToString();
			IList<int> list = (IList<int>)cacheStorageManager[key];
			bool flag3 = list != null;
			IList<int> result;
			if (flag3)
			{
				result = list;
			}
			else
			{
				list = new List<int>();
				PointOfContactManager pointOfContactManager = new PointOfContactManager(this.OpContext);
				IList<AppType> list2 = pointOfContactManager.LoadAllowedPOCAppointmentTypes(personId);
				foreach (AppType appType in list2)
				{
					bool flag4 = !list.Contains(appType.AppTypeId);
					if (flag4)
					{
						list.Add(appType.AppTypeId);
					}
				}
				List<AppType> list3 = this.LoadAllAppTypes();
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				string text = oldUserSettingManager.GetSettingValue_String(personId, eSettingCode.SETTING_VisibleAppTypeIds, true);
				bool flag5 = false;
				bool flag6 = string.IsNullOrEmpty(text);
				if (flag6)
				{
					bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(personId, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
					bool flag7 = !settingValue_Bool;
					if (flag7)
					{
						List<int> list4 = list3.ConvertAll<int>((AppType g) => g.AppTypeId);
						foreach (int item in list4)
						{
							list.Add(item);
						}
						bool flag8 = !list.Contains(-1);
						if (flag8)
						{
							list.Add(-1);
						}
						bool flag9 = !list.Contains(0);
						if (flag9)
						{
							list.Add(0);
						}
						flag5 = true;
					}
				}
				bool flag10 = !flag5;
				if (flag10)
				{
					bool flag11 = !string.IsNullOrEmpty(text);
					if (flag11)
					{
						bool flag12 = char.IsLetter(text[0]);
						if (flag12)
						{
							IDynamicQueryDAO dynamicQueryDAO = new DynamicQueryDAO(this.OpContext);
							text = text.Replace("@pid", personId.ToString());
							list = dynamicQueryDAO.LoadIntList(text);
						}
						else
						{
							string[] array = text.Split(new char[]
							{
								','
							}, StringSplitOptions.RemoveEmptyEntries);
							foreach (string s in array)
							{
								int num;
								bool flag13 = int.TryParse(s, out num) && num >= -1 && !list.Contains(num);
								if (flag13)
								{
									list.Add(num);
								}
							}
						}
					}
				}
				bool settingValue_Bool2 = oldUserSettingManager.GetSettingValue_Bool(personId, eSettingCode.SETTING_MedicalScheduler_Enabled);
				bool flag14 = settingValue_Bool2;
				if (flag14)
				{
					bool flag15 = !list.Contains(-1);
					if (flag15)
					{
						list.Add(-1);
					}
					bool flag16 = !list.Contains(0);
					if (flag16)
					{
						list.Add(0);
					}
				}
				cacheStorageManager.Insert(key, list, TimeSpan.FromHours(1.0));
				result = list;
			}
			return result;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00059C38 File Offset: 0x00057E38
		[DebuggerStepThrough]
		public Task<IList<int>> GetAllowedAppTypeIdsAsync(int personId)
		{
			AppointmentTypeManager.<GetAllowedAppTypeIdsAsync>d__27 <GetAllowedAppTypeIdsAsync>d__ = new AppointmentTypeManager.<GetAllowedAppTypeIdsAsync>d__27();
			<GetAllowedAppTypeIdsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<GetAllowedAppTypeIdsAsync>d__.<>4__this = this;
			<GetAllowedAppTypeIdsAsync>d__.personId = personId;
			<GetAllowedAppTypeIdsAsync>d__.<>1__state = -1;
			<GetAllowedAppTypeIdsAsync>d__.<>t__builder.Start<AppointmentTypeManager.<GetAllowedAppTypeIdsAsync>d__27>(ref <GetAllowedAppTypeIdsAsync>d__);
			return <GetAllowedAppTypeIdsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00059C84 File Offset: 0x00057E84
		public IList<AppType> LoadAllowedAppTypes()
		{
			List<AppType> source = this.LoadAllAppTypes();
			IList<int> allowedAppTypeIds = this.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			return (from g in source
			where (g.IsActive == null || g.IsActive.Value) && allowedAppTypeIds.Contains(g.AppTypeId)
			select g).ToList<AppType>();
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00059CD4 File Offset: 0x00057ED4
		public IList<AppType> LoadAllowedTestExamRelatedAppTypes()
		{
			List<AppType> source = this.LoadAllAppTypes();
			IList<int> allowedAppTypeIds = this.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			return (from g in source
			where (g.IsActive == null || g.IsActive.Value) && g.IsTestOrExam && allowedAppTypeIds.Contains(g.AppTypeId)
			select g).ToList<AppType>();
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00059D24 File Offset: 0x00057F24
		public IList<AppType> LoadAllowedWorkshopRelatedAppTypes()
		{
			List<AppType> source = this.LoadAllAppTypes();
			IList<int> allowedAppTypeIds = this.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			return (from g in source
			where (g.IsActive == null || g.IsActive.Value) && g.IsWorkshop && allowedAppTypeIds.Contains(g.AppTypeId)
			select g).ToList<AppType>();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00059D74 File Offset: 0x00057F74
		public AppTypeWithExtendedInfo LoadAppTypeWithExtendedInfoIdById(int appTypeId)
		{
			IAppointmentTypeDAO appointmentTypeDAO = new AppointmentTypeDAO(this.OpContext);
			return appointmentTypeDAO.LoadAppTypeWithExtendedInfoIdById(appTypeId);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00059D9C File Offset: 0x00057F9C
		public void UpdateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType)
		{
			IAppointmentTypeDAO appointmentTypeDAO = new AppointmentTypeDAO(this.OpContext);
			appointmentTypeDAO.UpdateAppTypeWithExtendedInfo(AppType);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00059DC0 File Offset: 0x00057FC0
		public int CreateAppTypeWithExtendedInfo(AppTypeWithExtendedInfo AppType)
		{
			IAppointmentTypeDAO appointmentTypeDAO = new AppointmentTypeDAO(this.OpContext);
			return appointmentTypeDAO.CreateAppTypeWithExtendedInfo(AppType);
		}

		// Token: 0x0400026D RID: 621
		private IAppointmentTypeDAO dao;
	}
}
