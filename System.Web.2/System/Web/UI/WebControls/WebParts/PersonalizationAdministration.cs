using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000557 RID: 1367
	public static class PersonalizationAdministration
	{
		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x000E5067 File Offset: 0x000E3267
		// (set) Token: 0x0600456A RID: 17770 RVA: 0x000E5073 File Offset: 0x000E3273
		public static string ApplicationName
		{
			get
			{
				return PersonalizationAdministration.Provider.ApplicationName;
			}
			set
			{
				PersonalizationAdministration.Provider.ApplicationName = value;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x000E5080 File Offset: 0x000E3280
		public static PersonalizationProvider Provider
		{
			get
			{
				PersonalizationAdministration.Initialize();
				return PersonalizationAdministration._provider;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000E508C File Offset: 0x000E328C
		public static PersonalizationProviderCollection Providers
		{
			get
			{
				PersonalizationAdministration.Initialize();
				return PersonalizationAdministration._providers;
			}
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000E5098 File Offset: 0x000E3298
		private static void Initialize()
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			if (PersonalizationAdministration._initialized)
			{
				return;
			}
			object initializationLock = PersonalizationAdministration._initializationLock;
			lock (initializationLock)
			{
				if (!PersonalizationAdministration._initialized)
				{
					WebPartsSection webParts = RuntimeConfig.GetAppConfig().WebParts;
					WebPartsPersonalization personalization = webParts.Personalization;
					PersonalizationAdministration._providers = new PersonalizationProviderCollection();
					ProvidersHelper.InstantiateProviders(personalization.Providers, PersonalizationAdministration._providers, typeof(PersonalizationProvider));
					PersonalizationAdministration._providers.SetReadOnly();
					PersonalizationAdministration._provider = PersonalizationAdministration._providers[personalization.DefaultProvider];
					if (PersonalizationAdministration._provider == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_provider_must_exist", new object[]
						{
							personalization.DefaultProvider
						}), personalization.ElementInformation.Properties["defaultProvider"].Source, personalization.ElementInformation.Properties["defaultProvider"].LineNumber);
					}
					PersonalizationAdministration._initialized = true;
				}
			}
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000E51AC File Offset: 0x000E33AC
		public static int ResetAllState(PersonalizationScope scope)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			return PersonalizationAdministration.ResetStatePrivate(scope, null, null);
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x000E51BC File Offset: 0x000E33BC
		public static int ResetState(PersonalizationStateInfoCollection data)
		{
			int num = 0;
			PersonalizationProviderHelper.CheckNullEntries(data, "data");
			StringCollection stringCollection = null;
			foreach (object obj in data)
			{
				PersonalizationStateInfo personalizationStateInfo = (PersonalizationStateInfo)obj;
				UserPersonalizationStateInfo userPersonalizationStateInfo = personalizationStateInfo as UserPersonalizationStateInfo;
				if (userPersonalizationStateInfo != null)
				{
					if (PersonalizationAdministration.ResetUserState(userPersonalizationStateInfo.Path, userPersonalizationStateInfo.Username))
					{
						num++;
					}
				}
				else
				{
					if (stringCollection == null)
					{
						stringCollection = new StringCollection();
					}
					stringCollection.Add(personalizationStateInfo.Path);
				}
			}
			if (stringCollection != null)
			{
				string[] array = new string[stringCollection.Count];
				stringCollection.CopyTo(array, 0);
				num += PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.Shared, array, null);
			}
			return num;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000E5280 File Offset: 0x000E3480
		public static bool ResetSharedState(string path)
		{
			path = StringUtil.CheckAndTrimString(path, "path");
			string[] paths = new string[]
			{
				path
			};
			int num = PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.Shared, paths, null);
			if (num > 1)
			{
				throw new HttpException(SR.GetString("PersonalizationAdmin_UnexpectedResetSharedStateReturnValue", new object[]
				{
					num.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return num == 1;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000E52DB File Offset: 0x000E34DB
		public static int ResetSharedState(string[] paths)
		{
			paths = PersonalizationProviderHelper.CheckAndTrimNonEmptyStringEntries(paths, "paths", true, false, -1);
			return PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.Shared, paths, null);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000E52F8 File Offset: 0x000E34F8
		public static int ResetUserState(string path)
		{
			path = StringUtil.CheckAndTrimString(path, "path");
			string[] paths = new string[]
			{
				path
			};
			return PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.User, paths, null);
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x000E5325 File Offset: 0x000E3525
		public static int ResetUserState(string[] usernames)
		{
			usernames = PersonalizationProviderHelper.CheckAndTrimNonEmptyStringEntries(usernames, "usernames", true, true, -1);
			return PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.User, null, usernames);
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x000E5340 File Offset: 0x000E3540
		public static bool ResetUserState(string path, string username)
		{
			path = StringUtil.CheckAndTrimString(path, "path");
			username = PersonalizationProviderHelper.CheckAndTrimStringWithoutCommas(username, "username");
			string[] paths = new string[]
			{
				path
			};
			string[] usernames = new string[]
			{
				username
			};
			int num = PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.User, paths, usernames);
			if (num > 1)
			{
				throw new HttpException(SR.GetString("PersonalizationAdmin_UnexpectedResetUserStateReturnValue", new object[]
				{
					num.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return num == 1;
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x000E53B4 File Offset: 0x000E35B4
		public static int ResetUserState(string path, string[] usernames)
		{
			path = StringUtil.CheckAndTrimString(path, "path");
			usernames = PersonalizationProviderHelper.CheckAndTrimNonEmptyStringEntries(usernames, "usernames", true, true, -1);
			string[] paths = new string[]
			{
				path
			};
			return PersonalizationAdministration.ResetStatePrivate(PersonalizationScope.User, paths, usernames);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x000E53F4 File Offset: 0x000E35F4
		private static int ResetStatePrivate(PersonalizationScope scope, string[] paths, string[] usernames)
		{
			PersonalizationAdministration.Initialize();
			int num = PersonalizationAdministration._provider.ResetState(scope, paths, usernames);
			PersonalizationProviderHelper.CheckNegativeReturnedInteger(num, "ResetState");
			return num;
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x000E5420 File Offset: 0x000E3620
		public static int ResetInactiveUserState(DateTime userInactiveSinceDate)
		{
			return PersonalizationAdministration.ResetInactiveUserStatePrivate(null, userInactiveSinceDate);
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000E5429 File Offset: 0x000E3629
		public static int ResetInactiveUserState(string path, DateTime userInactiveSinceDate)
		{
			path = StringUtil.CheckAndTrimString(path, "path");
			return PersonalizationAdministration.ResetInactiveUserStatePrivate(path, userInactiveSinceDate);
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000E5440 File Offset: 0x000E3640
		private static int ResetInactiveUserStatePrivate(string path, DateTime userInactiveSinceDate)
		{
			PersonalizationAdministration.Initialize();
			int num = PersonalizationAdministration._provider.ResetUserState(path, userInactiveSinceDate);
			PersonalizationProviderHelper.CheckNegativeReturnedInteger(num, "ResetUserState");
			return num;
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x000E546B File Offset: 0x000E366B
		public static int GetCountOfState(PersonalizationScope scope)
		{
			return PersonalizationAdministration.GetCountOfState(scope, null);
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x000E5474 File Offset: 0x000E3674
		public static int GetCountOfState(PersonalizationScope scope, string pathToMatch)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			pathToMatch = StringUtil.CheckAndTrimString(pathToMatch, "pathToMatch", false);
			return PersonalizationAdministration.GetCountOfStatePrivate(scope, new PersonalizationStateQuery
			{
				PathToMatch = pathToMatch
			});
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x000E54AC File Offset: 0x000E36AC
		private static int GetCountOfStatePrivate(PersonalizationScope scope, PersonalizationStateQuery stateQuery)
		{
			PersonalizationAdministration.Initialize();
			int countOfState = PersonalizationAdministration._provider.GetCountOfState(scope, stateQuery);
			PersonalizationProviderHelper.CheckNegativeReturnedInteger(countOfState, "GetCountOfState");
			return countOfState;
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x000E54D8 File Offset: 0x000E36D8
		public static int GetCountOfUserState(string usernameToMatch)
		{
			usernameToMatch = StringUtil.CheckAndTrimString(usernameToMatch, "usernameToMatch", false);
			return PersonalizationAdministration.GetCountOfStatePrivate(PersonalizationScope.User, new PersonalizationStateQuery
			{
				UsernameToMatch = usernameToMatch
			});
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x000E5507 File Offset: 0x000E3707
		public static int GetCountOfInactiveUserState(DateTime userInactiveSinceDate)
		{
			return PersonalizationAdministration.GetCountOfInactiveUserState(null, userInactiveSinceDate);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000E5510 File Offset: 0x000E3710
		public static int GetCountOfInactiveUserState(string pathToMatch, DateTime userInactiveSinceDate)
		{
			pathToMatch = StringUtil.CheckAndTrimString(pathToMatch, "pathToMatch", false);
			return PersonalizationAdministration.GetCountOfStatePrivate(PersonalizationScope.User, new PersonalizationStateQuery
			{
				PathToMatch = pathToMatch,
				UserInactiveSinceDate = userInactiveSinceDate
			});
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000E5546 File Offset: 0x000E3746
		private static PersonalizationStateInfoCollection FindStatePrivate(PersonalizationScope scope, PersonalizationStateQuery stateQuery, int pageIndex, int pageSize, out int totalRecords)
		{
			PersonalizationAdministration.Initialize();
			return PersonalizationAdministration._provider.FindState(scope, stateQuery, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000E5560 File Offset: 0x000E3760
		public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope)
		{
			int num;
			return PersonalizationAdministration.GetAllState(scope, 0, int.MaxValue, out num);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000E557B File Offset: 0x000E377B
		public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope, int pageIndex, int pageSize, out int totalRecords)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			PersonalizationProviderHelper.CheckPageIndexAndSize(pageIndex, pageSize);
			return PersonalizationAdministration.FindStatePrivate(scope, null, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x000E5594 File Offset: 0x000E3794
		public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate)
		{
			int num;
			return PersonalizationAdministration.GetAllInactiveUserState(userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x000E55B0 File Offset: 0x000E37B0
		public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			PersonalizationProviderHelper.CheckPageIndexAndSize(pageIndex, pageSize);
			return PersonalizationAdministration.FindStatePrivate(PersonalizationScope.User, new PersonalizationStateQuery
			{
				UserInactiveSinceDate = userInactiveSinceDate
			}, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x000E55DC File Offset: 0x000E37DC
		public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch)
		{
			int num;
			return PersonalizationAdministration.FindSharedState(pathToMatch, 0, int.MaxValue, out num);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x000E55F8 File Offset: 0x000E37F8
		public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			pathToMatch = StringUtil.CheckAndTrimString(pathToMatch, "pathToMatch", false);
			PersonalizationProviderHelper.CheckPageIndexAndSize(pageIndex, pageSize);
			return PersonalizationAdministration.FindStatePrivate(PersonalizationScope.Shared, new PersonalizationStateQuery
			{
				PathToMatch = pathToMatch
			}, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x000E5634 File Offset: 0x000E3834
		public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch)
		{
			int num;
			return PersonalizationAdministration.FindUserState(pathToMatch, usernameToMatch, 0, int.MaxValue, out num);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x000E5650 File Offset: 0x000E3850
		public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			pathToMatch = StringUtil.CheckAndTrimString(pathToMatch, "pathToMatch", false);
			usernameToMatch = StringUtil.CheckAndTrimString(usernameToMatch, "usernameToMatch", false);
			PersonalizationProviderHelper.CheckPageIndexAndSize(pageIndex, pageSize);
			return PersonalizationAdministration.FindStatePrivate(PersonalizationScope.User, new PersonalizationStateQuery
			{
				PathToMatch = pathToMatch,
				UsernameToMatch = usernameToMatch
			}, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x000E56A0 File Offset: 0x000E38A0
		public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate)
		{
			int num;
			return PersonalizationAdministration.FindInactiveUserState(pathToMatch, usernameToMatch, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x000E56C0 File Offset: 0x000E38C0
		public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			pathToMatch = StringUtil.CheckAndTrimString(pathToMatch, "pathToMatch", false);
			usernameToMatch = StringUtil.CheckAndTrimString(usernameToMatch, "usernameToMatch", false);
			PersonalizationProviderHelper.CheckPageIndexAndSize(pageIndex, pageSize);
			return PersonalizationAdministration.FindStatePrivate(PersonalizationScope.User, new PersonalizationStateQuery
			{
				PathToMatch = pathToMatch,
				UsernameToMatch = usernameToMatch,
				UserInactiveSinceDate = userInactiveSinceDate
			}, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x0400266C RID: 9836
		private static readonly object _initializationLock = new object();

		// Token: 0x0400266D RID: 9837
		private static bool _initialized;

		// Token: 0x0400266E RID: 9838
		private static PersonalizationProvider _provider;

		// Token: 0x0400266F RID: 9839
		private static PersonalizationProviderCollection _providers;

		// Token: 0x04002670 RID: 9840
		internal static readonly DateTime DefaultInactiveSinceDate = DateTime.MaxValue;

		// Token: 0x04002671 RID: 9841
		private const int _defaultPageIndex = 0;

		// Token: 0x04002672 RID: 9842
		private const int _defaultPageSize = 2147483647;
	}
}
