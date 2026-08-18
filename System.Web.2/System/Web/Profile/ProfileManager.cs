using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Profile
{
	// Token: 0x02000163 RID: 355
	public static class ProfileManager
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0003A901 File Offset: 0x00038B01
		internal static ProfilePropertySettingsCollection DynamicProfileProperties
		{
			get
			{
				return ProfileManager.s_dynamicProperties;
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0003A908 File Offset: 0x00038B08
		public static void AddDynamicProfileProperty(ProfilePropertySettings property)
		{
			BuildManager.ThrowIfPreAppStartNotRunning();
			ProfileManager.s_dynamicProperties.Add(property);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0003A91A File Offset: 0x00038B1A
		public static bool DeleteProfile(string username)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			return ProfileManager.Provider.DeleteProfiles(new string[]
			{
				username
			}) != 0;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0003A944 File Offset: 0x00038B44
		public static int DeleteProfiles(ProfileInfoCollection profiles)
		{
			if (profiles == null)
			{
				throw new ArgumentNullException("profiles");
			}
			if (profiles.Count < 1)
			{
				throw new ArgumentException(SR.GetString("Parameter_collection_empty", new object[]
				{
					"profiles"
				}), "profiles");
			}
			foreach (object obj in profiles)
			{
				ProfileInfo profileInfo = (ProfileInfo)obj;
				string userName = profileInfo.UserName;
				SecUtility.CheckParameter(ref userName, true, true, true, 0, "UserName");
			}
			return ProfileManager.Provider.DeleteProfiles(profiles);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0003A9F0 File Offset: 0x00038BF0
		public static int DeleteProfiles(string[] usernames)
		{
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 0, "usernames");
			return ProfileManager.Provider.DeleteProfiles(usernames);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0003AA0D File Offset: 0x00038C0D
		public static int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return ProfileManager.Provider.DeleteInactiveProfiles(authenticationOption, userInactiveSinceDate);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0003AA1C File Offset: 0x00038C1C
		public static int GetNumberOfProfiles(ProfileAuthenticationOption authenticationOption)
		{
			return ProfileManager.Provider.GetNumberOfInactiveProfiles(authenticationOption, DateTime.Now.AddDays(1.0));
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0003AA4A File Offset: 0x00038C4A
		public static int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return ProfileManager.Provider.GetNumberOfInactiveProfiles(authenticationOption, userInactiveSinceDate);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0003AA58 File Offset: 0x00038C58
		public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption)
		{
			int num;
			return ProfileManager.Provider.GetAllProfiles(authenticationOption, 0, int.MaxValue, out num);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0003AA78 File Offset: 0x00038C78
		public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.GetAllProfiles(authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003AA88 File Offset: 0x00038C88
		public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int num;
			return ProfileManager.Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0003AAA9 File Offset: 0x00038CA9
		public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0003AABC File Offset: 0x00038CBC
		public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			int num;
			return ProfileManager.Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, 0, int.MaxValue, out num);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0003AAF0 File Offset: 0x00038CF0
		public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			return ProfileManager.Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003AB50 File Offset: 0x00038D50
		public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			int num;
			return ProfileManager.Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0003AB84 File Offset: 0x00038D84
		public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			return ProfileManager.Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0003ABE6 File Offset: 0x00038DE6
		public static bool Enabled
		{
			get
			{
				if (!ProfileManager.s_Initialized && !ProfileManager.s_InitializedEnabled)
				{
					ProfileManager.InitializeEnabled(false);
				}
				return ProfileManager.s_Enabled;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0003AC01 File Offset: 0x00038E01
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x0003AC0D File Offset: 0x00038E0D
		public static string ApplicationName
		{
			get
			{
				return ProfileManager.Provider.ApplicationName;
			}
			set
			{
				ProfileManager.Provider.ApplicationName = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0003AC1A File Offset: 0x00038E1A
		public static bool AutomaticSaveEnabled
		{
			get
			{
				HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
				ProfileManager.InitializeEnabled(false);
				return ProfileManager.s_AutomaticSaveEnabled;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0003AC36 File Offset: 0x00038E36
		public static ProfileProvider Provider
		{
			get
			{
				HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
				ProfileManager.Initialize(true);
				if (ProfileManager.s_Provider == null)
				{
					throw new InvalidOperationException(SR.GetString("Profile_default_provider_not_found"));
				}
				return ProfileManager.s_Provider;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0003AC69 File Offset: 0x00038E69
		public static ProfileProviderCollection Providers
		{
			get
			{
				HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
				ProfileManager.Initialize(true);
				return ProfileManager.s_Providers;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0003AC88 File Offset: 0x00038E88
		private static void InitializeEnabled(bool initProviders)
		{
			if (!ProfileManager.s_Initialized || !ProfileManager.s_InitializedProviders || !ProfileManager.s_InitializeDefaultProvider)
			{
				object obj = ProfileManager.s_Lock;
				lock (obj)
				{
					if (!ProfileManager.s_Initialized || !ProfileManager.s_InitializedProviders || !ProfileManager.s_InitializeDefaultProvider)
					{
						try
						{
							ProfileSection profileAppConfig = MTConfigUtil.GetProfileAppConfig();
							if (!ProfileManager.s_InitializedEnabled)
							{
								ProfileManager.s_Enabled = (profileAppConfig.Enabled && HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low));
								ProfileManager.s_AutomaticSaveEnabled = (ProfileManager.s_Enabled && profileAppConfig.AutomaticSaveEnabled);
								ProfileManager.s_InitializedEnabled = true;
							}
							if (initProviders && ProfileManager.s_Enabled && (!ProfileManager.s_InitializedProviders || !ProfileManager.s_InitializeDefaultProvider))
							{
								ProfileManager.InitProviders(profileAppConfig);
							}
						}
						catch (Exception ex)
						{
							ProfileManager.s_InitException = ex;
						}
						ProfileManager.s_Initialized = true;
					}
				}
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0003AD6C File Offset: 0x00038F6C
		private static void Initialize(bool throwIfNotEnabled)
		{
			ProfileManager.InitializeEnabled(true);
			if (ProfileManager.s_InitException != null)
			{
				throw ProfileManager.s_InitException;
			}
			if (throwIfNotEnabled && !ProfileManager.s_Enabled)
			{
				throw new ProviderException(SR.GetString("Profile_not_enabled"));
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0003AD9C File Offset: 0x00038F9C
		private static void InitProviders(ProfileSection config)
		{
			if (!ProfileManager.s_InitializedProviders)
			{
				ProfileManager.s_Providers = new ProfileProviderCollection();
				if (config.Providers != null)
				{
					ProvidersHelper.InstantiateProviders(config.Providers, ProfileManager.s_Providers, typeof(ProfileProvider));
				}
				ProfileManager.s_InitializedProviders = true;
			}
			bool flag = !HostingEnvironment.IsHosted || BuildManager.PreStartInitStage == PreStartInitStage.AfterPreStartInit;
			if (!ProfileManager.s_InitializeDefaultProvider && flag)
			{
				ProfileManager.s_Providers.SetReadOnly();
				if (config.DefaultProvider == null)
				{
					throw new ProviderException(SR.GetString("Profile_default_provider_not_specified"));
				}
				ProfileManager.s_Provider = ProfileManager.s_Providers[config.DefaultProvider];
				if (ProfileManager.s_Provider == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Profile_default_provider_not_found"), config.ElementInformation.Properties["providers"].Source, config.ElementInformation.Properties["providers"].LineNumber);
				}
				ProfileManager.s_InitializeDefaultProvider = true;
			}
		}

		// Token: 0x04001513 RID: 5395
		private static ProfilePropertySettingsCollection s_dynamicProperties = new ProfilePropertySettingsCollection();

		// Token: 0x04001514 RID: 5396
		private static ProfileProvider s_Provider;

		// Token: 0x04001515 RID: 5397
		private static ProfileProviderCollection s_Providers;

		// Token: 0x04001516 RID: 5398
		private static bool s_Enabled;

		// Token: 0x04001517 RID: 5399
		private static bool s_Initialized;

		// Token: 0x04001518 RID: 5400
		private static bool s_InitializedProviders;

		// Token: 0x04001519 RID: 5401
		private static bool s_InitializeDefaultProvider;

		// Token: 0x0400151A RID: 5402
		private static object s_Lock = new object();

		// Token: 0x0400151B RID: 5403
		private static Exception s_InitException;

		// Token: 0x0400151C RID: 5404
		private static bool s_InitializedEnabled;

		// Token: 0x0400151D RID: 5405
		private static bool s_AutomaticSaveEnabled;
	}
}
