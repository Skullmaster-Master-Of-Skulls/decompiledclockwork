using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.Impl.UserSettingsPermissions;
using TechnoPro.Common.DAO.UserSettingsPermissions;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.UserSettingsPermissions
{
	// Token: 0x0200002D RID: 45
	public class PermissionManager : IPermissionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00008924 File Offset: 0x00006B24
		public PermissionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008936 File Offset: 0x00006B36
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000893E File Offset: 0x00006B3E
		public OperationContext OpContext { get; set; }

		// Token: 0x060001AC RID: 428 RVA: 0x00008948 File Offset: 0x00006B48
		public bool IsUserAllowed(int pid, UserPermissionEnum permission)
		{
			UserPermissionIsAllowedSet userPermissionIsAllowedSet = this.LoadUserPermissionSet(pid, false);
			UserPermissionIsAllowed userPermissionIsAllowed = userPermissionIsAllowedSet.GeneralPermissionsAllowed.FirstOrDefault((UserPermissionIsAllowed g) => g.Permission == permission);
			return userPermissionIsAllowed != null && userPermissionIsAllowed.IsAllowed;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008994 File Offset: 0x00006B94
		public UserPermissionIsAllowedSet LoadUserPermissionSet(int pid, bool ignoreCache)
		{
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			bool flag = !ignoreCache;
			UserPermissionIsAllowedSet userPermissionIsAllowedSet;
			if (flag)
			{
				userPermissionIsAllowedSet = (UserPermissionIsAllowedSet)userDatabaseCacheStorageManager[pid, "userPermissionsSet"];
				bool flag2 = userPermissionIsAllowedSet != null;
				if (flag2)
				{
					return userPermissionIsAllowedSet;
				}
			}
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			bool isAdmin = peopleGroupManager.IsAdmin(pid);
			List<UserPermissionEnum> source = ((UserPermissionEnum[])Enum.GetValues(typeof(UserPermissionEnum))).Where(delegate(UserPermissionEnum g)
			{
				UserPermissionAttribute attribute = g.GetAttribute<UserPermissionAttribute>();
				bool flag3 = attribute != null && attribute.IsHidden;
				bool result;
				if (flag3)
				{
					result = false;
				}
				else
				{
					UserPermissionGroupAttribute userPermissionGroupAttribute = (attribute == null) ? null : attribute.Group.GetAttribute<UserPermissionGroupAttribute>();
					result = (userPermissionGroupAttribute == null || !userPermissionGroupAttribute.IsScreenViewModifyCreatePermissions);
				}
				return result;
			}).ToList<UserPermissionEnum>();
			IList<UserPermission> userPermissions = this.LoadUserPermissions(pid, ignoreCache);
			userPermissionIsAllowedSet = new UserPermissionIsAllowedSet
			{
				PersonId = pid,
				GeneralPermissionsAllowed = source.Select(delegate(UserPermissionEnum g)
				{
					UserPermissionIsAllowed result;
					if (!isAdmin)
					{
						result = this.IsUserAllowed(userPermissions, g);
					}
					else
					{
						UserPermissionIsAllowed userPermissionIsAllowed = new UserPermissionIsAllowed();
						userPermissionIsAllowed.Permission = g;
						userPermissionIsAllowed.IsAllowed = true;
						result = userPermissionIsAllowed;
						userPermissionIsAllowed.PermissionType = eUserPermissionType.Person;
					}
					return result;
				}).ToList<UserPermissionIsAllowed>(),
				ScreenNumsAllowedCreateScreen = this.GetPermissionScreenNums(userPermissions, UserPermissionEnum.CreateScreen),
				ScreenNumsAllowedViewScreen = this.GetPermissionScreenNums(userPermissions, UserPermissionEnum.ViewScreen),
				ScreenNumsAllowedModifyScreen = this.GetPermissionScreenNums(userPermissions, UserPermissionEnum.ModifyScreen)
			};
			userDatabaseCacheStorageManager.Insert(pid, "userPermissionsSet", userPermissionIsAllowedSet, TimeSpan.FromMinutes(10.0));
			return userPermissionIsAllowedSet;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008AEC File Offset: 0x00006CEC
		private IList<int> GetPermissionScreenNums(IList<UserPermission> userPermissions, UserPermissionEnum permissionCode)
		{
			bool flag = userPermissions.FirstOrDefault((UserPermission g) => g.Permission == permissionCode && g.PermissionType == eUserPermissionType.Person) != null;
			IList<int> result;
			if (flag)
			{
				result = (from g in userPermissions
				where g.Permission == permissionCode && g.PermissionType == eUserPermissionType.Person
				select g.PermissionValue into h
				where h > 0
				select h).Distinct<int>().ToList<int>();
			}
			else
			{
				result = (from g in userPermissions
				where g.Permission == permissionCode
				select g.PermissionValue into h
				where h > 0
				select h).Distinct<int>().ToList<int>();
			}
			return result;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008BF0 File Offset: 0x00006DF0
		private UserPermissionIsAllowed IsUserAllowed(IList<UserPermission> permissions, UserPermissionEnum permissionCode)
		{
			PermissionManager.eUserPermissionIsAllowedResult isAllowedResult = this.GetIsAllowedResult(permissions, permissionCode, new eUserPermissionType[1]);
			bool flag = isAllowedResult != PermissionManager.eUserPermissionIsAllowedResult.Undetermined;
			UserPermissionIsAllowed result;
			if (flag)
			{
				result = new UserPermissionIsAllowed
				{
					Permission = permissionCode,
					PermissionType = eUserPermissionType.Person,
					IsAllowed = (isAllowedResult == PermissionManager.eUserPermissionIsAllowedResult.Yes)
				};
			}
			else
			{
				isAllowedResult = this.GetIsAllowedResult(permissions, permissionCode, new eUserPermissionType[]
				{
					eUserPermissionType.Group,
					eUserPermissionType.Everyone
				});
				bool flag2 = isAllowedResult != PermissionManager.eUserPermissionIsAllowedResult.Undetermined;
				if (flag2)
				{
					result = new UserPermissionIsAllowed
					{
						Permission = permissionCode,
						PermissionType = eUserPermissionType.Group,
						IsAllowed = (isAllowedResult == PermissionManager.eUserPermissionIsAllowedResult.Yes)
					};
				}
				else
				{
					result = new UserPermissionIsAllowed
					{
						IsAllowed = (permissionCode < UserPermissionEnum.AddCustomLUCourses),
						Permission = permissionCode,
						PermissionType = eUserPermissionType.Everyone
					};
				}
			}
			return result;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008CA4 File Offset: 0x00006EA4
		private PermissionManager.eUserPermissionIsAllowedResult GetIsAllowedResult(IList<UserPermission> allUserPermissions, UserPermissionEnum permissionCode, params eUserPermissionType[] permissionTypes)
		{
			List<UserPermission> list = (from g in allUserPermissions
			where Array.IndexOf<eUserPermissionType>(permissionTypes, g.PermissionType) >= 0 && g.Permission == permissionCode
			select g).ToList<UserPermission>();
			List<UserPermission> list2;
			if (permissionCode >= UserPermissionEnum.AddCustomLUCourses)
			{
				list2 = list;
			}
			else
			{
				list2 = list.Select(delegate(UserPermission g)
				{
					UserPermission userPermission = g.Clone();
					userPermission.PermissionValue = ((g.PermissionValue == 0) ? 1 : 0);
					return userPermission;
				}).ToList<UserPermission>();
			}
			List<UserPermission> list3 = list2;
			bool flag = list3.Count < 1;
			PermissionManager.eUserPermissionIsAllowedResult result;
			if (flag)
			{
				result = PermissionManager.eUserPermissionIsAllowedResult.Undetermined;
			}
			else
			{
				result = (list3.Any((UserPermission g) => g.PermissionValue == 0) ? PermissionManager.eUserPermissionIsAllowedResult.No : PermissionManager.eUserPermissionIsAllowedResult.Yes);
			}
			return result;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008D58 File Offset: 0x00006F58
		public IList<UserPermission> LoadUserPermissions(int pid)
		{
			return this.LoadUserPermissions(pid, false);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008D74 File Offset: 0x00006F74
		public IList<UserPermission> LoadUserPermissions(int pid, bool ignoreCache)
		{
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			bool flag = !ignoreCache;
			IList<UserPermission> list;
			if (flag)
			{
				list = (IList<UserPermission>)userDatabaseCacheStorageManager[pid, "userPermissions"];
				bool flag2 = list != null;
				if (flag2)
				{
					return list;
				}
			}
			IPermissionDAO permissionDAO = new PermissionDAO(this.OpContext);
			list = permissionDAO.LoadUserPermissions(pid);
			userDatabaseCacheStorageManager.Insert(pid, "userPermissions", list, TimeSpan.FromMinutes(10.0));
			return list;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008DFC File Offset: 0x00006FFC
		public bool IsUserAllowedToViewScreen(int pid, int screenNum)
		{
			UserPermissionIsAllowedSet userPermissionIsAllowedSet = this.LoadUserPermissionSet(pid, false);
			return userPermissionIsAllowedSet.ScreenNumsAllowedViewScreen.Contains(screenNum);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008E24 File Offset: 0x00007024
		public bool IsUserAllowedToModifyScreen(int pid, int screenNum)
		{
			UserPermissionIsAllowedSet userPermissionIsAllowedSet = this.LoadUserPermissionSet(pid, false);
			return userPermissionIsAllowedSet.ScreenNumsAllowedModifyScreen.Contains(screenNum);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008E4C File Offset: 0x0000704C
		public bool IsUserAllowedToCreateScreen(int pid, int screenNum)
		{
			UserPermissionIsAllowedSet userPermissionIsAllowedSet = this.LoadUserPermissionSet(pid, false);
			return userPermissionIsAllowedSet.ScreenNumsAllowedCreateScreen.Contains(screenNum);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008E74 File Offset: 0x00007074
		public UserOrGroupJustPermissionSet LoadJustUserPermissions(int pid)
		{
			IPermissionDAO permissionDAO = new PermissionDAO(this.OpContext);
			return permissionDAO.LoadJustUserPermissions(pid);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008E9C File Offset: 0x0000709C
		public UserOrGroupJustPermissionSet LoadJustGroupPermissions(int gid)
		{
			IPermissionDAO permissionDAO = new PermissionDAO(this.OpContext);
			return permissionDAO.LoadJustGroupPermissions(gid);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008EC4 File Offset: 0x000070C4
		public void UpdateJustUserOrGroupPermissions(UserOrGroupJustPermissionSet permissionSet)
		{
			IPermissionDAO permissionDAO = new PermissionDAO(this.OpContext);
			permissionDAO.UpdateJustUserOrGroupPermissions(permissionSet);
			bool flag = permissionSet.PermissionType > eUserPermissionType.Person;
			if (!flag)
			{
				OperationContext opContext = this.OpContext;
				IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
				userDatabaseCacheStorageManager.Remove(permissionSet.PersonOrGroupId, "userPermissionsSet");
			}
		}

		// Token: 0x04000059 RID: 89
		private const string userPermissionSetKey = "userPermissionsSet";

		// Token: 0x0200019D RID: 413
		internal enum eUserPermissionIsAllowedResult
		{
			// Token: 0x040003FA RID: 1018
			Yes,
			// Token: 0x040003FB RID: 1019
			No,
			// Token: 0x040003FC RID: 1020
			Undetermined
		}
	}
}
