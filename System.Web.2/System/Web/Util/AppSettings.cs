using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.Util
{
	// Token: 0x020001E6 RID: 486
	internal static class AppSettings
	{
		// Token: 0x060017CA RID: 6090 RVA: 0x0004AECC File Offset: 0x000490CC
		private static void EnsureSettingsLoaded()
		{
			if (!AppSettings._settingsInitialized)
			{
				object appSettingsLock = AppSettings._appSettingsLock;
				lock (appSettingsLock)
				{
					if (!AppSettings._settingsInitialized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = AppSettings.GetAppSettingsSection();
						}
						finally
						{
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseHostHeaderForRequestUrl"], out AppSettings._useHostHeaderForRequestUrl))
							{
								AppSettings._useHostHeaderForRequestUrl = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AllowAnonymousImpersonation"], out AppSettings._allowAnonymousImpersonation))
							{
								AppSettings._allowAnonymousImpersonation = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:ScriptResourceAllowNonJsFiles"], out AppSettings._scriptResourceAllowNonJsFiles))
							{
								AppSettings._scriptResourceAllowNonJsFiles = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyEncryption"], out AppSettings._useLegacyEncryption))
							{
								AppSettings._useLegacyEncryption = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyMachineKeyEncryption"], out AppSettings._useLegacyMachineKeyEncryption))
							{
								AppSettings._useLegacyMachineKeyEncryption = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyFormsAuthenticationTicketCompatibility"], out AppSettings._useLegacyFormsAuthenticationTicketCompatibility))
							{
								AppSettings._useLegacyFormsAuthenticationTicketCompatibility = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyEventValidationCompatibility"], out AppSettings._useLegacyEventValidationCompatibility))
							{
								AppSettings._useLegacyEventValidationCompatibility = false;
							}
							AppSettings._allowInsecureDeserialization = AppSettings.GetNullableBooleanValue(nameValueCollection, "aspnet:AllowInsecureDeserialization");
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AlwaysIgnoreViewStateValidationErrors"], out AppSettings._alwaysIgnoreViewStateValidationErrors))
							{
								AppSettings._alwaysIgnoreViewStateValidationErrors = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AllowRelaxedHttpUserName"], out AppSettings._allowRelaxedHttpUserName))
							{
								AppSettings._allowRelaxedHttpUserName = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:JavaScriptDoNotEncodeAmpersand"], out AppSettings._javaScriptDoNotEncodeAmpersand))
							{
								AppSettings._javaScriptDoNotEncodeAmpersand = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseTaskFriendlySynchronizationContext"], out AppSettings._useTaskFriendlySynchronizationContext))
							{
								AppSettings._useTaskFriendlySynchronizationContext = BinaryCompatibility.Current.TargetsAtLeastFramework45;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AllowAsyncDuringSyncStages"], out AppSettings._allowAsyncDuringSyncStages))
							{
								AppSettings._allowAsyncDuringSyncStages = false;
							}
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["aspnet:MaxHttpCollectionKeys"], out AppSettings._maxHttpCollectionKeys) || AppSettings._maxHttpCollectionKeys < 0)
							{
								AppSettings._maxHttpCollectionKeys = int.MaxValue;
							}
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["aspnet:MaxJsonDeserializerMembers"], out AppSettings._maxJsonDeserializerMembers) || AppSettings._maxJsonDeserializerMembers < 0)
							{
								AppSettings._maxJsonDeserializerMembers = int.MaxValue;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:DoNotDisposeSpecialHttpApplicationInstances"], out AppSettings._doNotDisposeSpecialHttpApplicationInstances))
							{
								AppSettings._doNotDisposeSpecialHttpApplicationInstances = false;
							}
							if (nameValueCollection != null)
							{
								AppSettings._formsAuthReturnUrlVar = nameValueCollection["aspnet:FormsAuthReturnUrlVar"];
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:RestrictXmlControls"], out AppSettings._restrictXmlControls))
							{
								AppSettings._restrictXmlControls = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AllowRelaxedRelativeUrl"], out AppSettings._allowRelaxedRelativeUrl))
							{
								AppSettings._allowRelaxedRelativeUrl = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyRequestUrlGeneration"], out AppSettings._useLegacyRequestUrlGeneration))
							{
								AppSettings._useLegacyRequestUrlGeneration = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AllowUtf7RequestContentEncoding"], out AppSettings._allowUtf7RequestContentEncoding))
							{
								AppSettings._allowUtf7RequestContentEncoding = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AllowRelaxedUnicodeDecoding"], out AppSettings._allowRelaxedUnicodeDecoding))
							{
								AppSettings._allowRelaxedUnicodeDecoding = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:DontUsePercentUUrlEncoding"], out AppSettings._dontUsePercentUUrlEncoding))
							{
								AppSettings._dontUsePercentUUrlEncoding = BinaryCompatibility.Current.TargetsAtLeastFramework452;
							}
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["aspnet:UpdatePanelMaxScriptLength"], out AppSettings._updatePanelMaxScriptLength) || AppSettings._updatePanelMaxScriptLength < 0)
							{
								AppSettings._updatePanelMaxScriptLength = 0;
							}
							AppSettings._maxConcurrentCompilations = AppSettings.GetNullableIntegerValue(nameValueCollection, "aspnet:MaxConcurrentCompilations");
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["aspnet:MaxAcceptLanguageFallbackCount"], out AppSettings._maxAcceptLanguageFallbackCount) || AppSettings._maxAcceptLanguageFallbackCount <= 0)
							{
								AppSettings._maxAcceptLanguageFallbackCount = 3;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:PortableCompilationOutput"], out AppSettings._portableCompilationOutput))
							{
								AppSettings._portableCompilationOutput = false;
							}
							if (nameValueCollection == null || string.IsNullOrWhiteSpace(AppSettings._portableCompilationOutputSnapshotType = nameValueCollection["aspnet:PortableCompilationOutputSnapshotType"]))
							{
								AppSettings._portableCompilationOutputSnapshotType = null;
							}
							if (nameValueCollection == null || string.IsNullOrWhiteSpace(AppSettings._portableCompilationOutputSnapshotTypeOptions = nameValueCollection["aspnet:PortableCompilationOutputSnapshotTypeOptions"]))
							{
								AppSettings._portableCompilationOutputSnapshotTypeOptions = null;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:EnsureSessionStateLockedOnFlush"], out AppSettings._ensureSessionStateLockedOnFlush))
							{
								AppSettings._ensureSessionStateLockedOnFlush = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseRandomizedStringHashAlgorithm"], out AppSettings._useRandomizedStringHashAlgorithm))
							{
								AppSettings._useRandomizedStringHashAlgorithm = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:EnableAsyncModelBinding"], out AppSettings._enableAsyncModelBinding))
							{
								AppSettings._enableAsyncModelBinding = BinaryCompatibility.Current.TargetsAtLeastFramework46;
							}
							if (nameValueCollection == null || !int.TryParse(nameValueCollection["aspnet:RequestQueueLimitPerSession"], out AppSettings._requestQueueLimitPerSession) || AppSettings._requestQueueLimitPerSession < 0)
							{
								AppSettings._requestQueueLimitPerSession = (BinaryCompatibility.Current.TargetsAtLeastFramework463 ? 50 : int.MaxValue);
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:LogMembershipPasswordFormatWarning"], out AppSettings._logMembershipPasswordFormatWarning))
							{
								AppSettings._logMembershipPasswordFormatWarning = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:AvoidDuplicatedSetCookie"], out AppSettings._avoidDuplicatedSetCookie))
							{
								AppSettings._avoidDuplicatedSetCookie = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:GetValidationMemberName"], out AppSettings._getValidationMemberName))
							{
								AppSettings._getValidationMemberName = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyClientServicesJsonHandling"], out AppSettings._useLegacyClientServicesJsonHandling))
							{
								AppSettings._useLegacyClientServicesJsonHandling = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyMultiValueHeaderHandling"], out AppSettings._useLegacyMultiValueHeaderHandling))
							{
								AppSettings._useLegacyMultiValueHeaderHandling = !BinaryCompatibility.Current.TargetsAtLeastFramework48;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:SuppressSameSiteNone"], out AppSettings._suppressSameSiteNone))
							{
								AppSettings._suppressSameSiteNone = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:HandleNoMoreFiles"], out AppSettings._handleNoMoreFiles))
							{
								AppSettings._handleNoMoreFiles = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:VerifyVirtualPathFromDiskCache"], out AppSettings._verifyVirtualPathFromDiskCache))
							{
								AppSettings._verifyVirtualPathFromDiskCache = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:EnsureCookieDefaults"], out AppSettings._fixCookieDefaults))
							{
								AppSettings._fixCookieDefaults = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:DisableAppPathModifier"], out AppSettings._disableAppPathModifier))
							{
								AppSettings._disableAppPathModifier = true;
								RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
								if (appConfig != null)
								{
									try
									{
										appConfig.IgnoreConfigErrors = true;
										SessionStateSection sessionState = appConfig.SessionState;
										bool disableAppPathModifier;
										if (sessionState == null || sessionState.Cookieless != HttpCookieMode.UseUri)
										{
											AnonymousIdentificationSection anonymousIdentification = appConfig.AnonymousIdentification;
											if (anonymousIdentification == null || anonymousIdentification.Cookieless != HttpCookieMode.UseUri)
											{
												AuthenticationSection authentication = appConfig.Authentication;
												int num;
												if (authentication == null)
												{
													num = 0;
												}
												else
												{
													FormsAuthenticationConfiguration forms = authentication.Forms;
													HttpCookieMode? httpCookieMode = (forms != null) ? new HttpCookieMode?(forms.Cookieless) : null;
													HttpCookieMode httpCookieMode2 = HttpCookieMode.UseUri;
													num = ((httpCookieMode.GetValueOrDefault() == httpCookieMode2 & httpCookieMode != null) ? 1 : 0);
												}
												disableAppPathModifier = (num == 0);
												goto IL_669;
											}
										}
										disableAppPathModifier = false;
										IL_669:
										AppSettings._disableAppPathModifier = disableAppPathModifier;
									}
									finally
									{
										appConfig.IgnoreConfigErrors = false;
									}
								}
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:CheckMemoryBytes"], out AppSettings._checkMemoryBytes))
							{
								AppSettings._checkMemoryBytes = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:RestoreAggressiveCookielessPathRemoval"], out AppSettings._restoreAggressiveCookielessPathRemoval))
							{
								AppSettings._restoreAggressiveCookielessPathRemoval = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:JsonDeserializerLimitedDate"], out AppSettings._jsonDeserializerLimitedDate))
							{
								AppSettings._jsonDeserializerLimitedDate = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:FileNameUtilUseLegacyInvalidChars"], out AppSettings._fileNameUtilUseLegacyInvalidChars))
							{
								AppSettings._fileNameUtilUseLegacyInvalidChars = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["aspnet:UseLegacyCacheKeyHash"], out AppSettings._useLegacyCacheKeyHash))
							{
								AppSettings._useLegacyCacheKeyHash = true;
							}
							AppSettings._settingsInitialized = true;
						}
					}
				}
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0004B654 File Offset: 0x00049854
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private static NameValueCollection GetAppSettingsSection()
		{
			if (!HostingEnvironment.IsHosted)
			{
				return ConfigurationManager.AppSettings;
			}
			CachedPathData applicationPathData = CachedPathData.GetApplicationPathData();
			if (applicationPathData != null && applicationPathData.ConfigRecord != null)
			{
				return applicationPathData.ConfigRecord.GetSection("appSettings") as NameValueCollection;
			}
			return null;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0004B698 File Offset: 0x00049898
		private static bool? GetNullableBooleanValue(NameValueCollection settings, string key)
		{
			bool value;
			if (settings == null || !bool.TryParse(settings[key], out value))
			{
				return null;
			}
			return new bool?(value);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0004B6C8 File Offset: 0x000498C8
		private static int? GetNullableIntegerValue(NameValueCollection settings, string key)
		{
			int value;
			if (settings == null || !int.TryParse(settings[key], out value))
			{
				return null;
			}
			return new int?(value);
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0004B6F8 File Offset: 0x000498F8
		internal static bool UseHostHeaderForRequestUrl
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useHostHeaderForRequestUrl;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0004B704 File Offset: 0x00049904
		internal static bool AllowAnonymousImpersonation
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowAnonymousImpersonation;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0004B710 File Offset: 0x00049910
		internal static bool ScriptResourceAllowNonJsFiles
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._scriptResourceAllowNonJsFiles;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x0004B71C File Offset: 0x0004991C
		internal static bool UseLegacyEncryption
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyEncryption;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0004B728 File Offset: 0x00049928
		internal static bool UseLegacyMachineKeyEncryption
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyMachineKeyEncryption;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0004B734 File Offset: 0x00049934
		internal static bool UseLegacyFormsAuthenticationTicketCompatibility
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyFormsAuthenticationTicketCompatibility;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0004B740 File Offset: 0x00049940
		internal static bool UseLegacyEventValidationCompatibility
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyEventValidationCompatibility;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0004B74C File Offset: 0x0004994C
		internal static bool? AllowInsecureDeserialization
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowInsecureDeserialization;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0004B758 File Offset: 0x00049958
		internal static bool AlwaysIgnoreViewStateValidationErrors
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._alwaysIgnoreViewStateValidationErrors;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0004B764 File Offset: 0x00049964
		internal static bool AllowRelaxedHttpUserName
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowRelaxedHttpUserName;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0004B770 File Offset: 0x00049970
		internal static bool JavaScriptDoNotEncodeAmpersand
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._javaScriptDoNotEncodeAmpersand;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0004B77C File Offset: 0x0004997C
		internal static bool UseTaskFriendlySynchronizationContext
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useTaskFriendlySynchronizationContext;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0004B788 File Offset: 0x00049988
		internal static bool AllowAsyncDuringSyncStages
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowAsyncDuringSyncStages;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0004B794 File Offset: 0x00049994
		internal static int MaxHttpCollectionKeys
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._maxHttpCollectionKeys;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0004B7A0 File Offset: 0x000499A0
		internal static int MaxJsonDeserializerMembers
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._maxJsonDeserializerMembers;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0004B7AC File Offset: 0x000499AC
		internal static bool DoNotDisposeSpecialHttpApplicationInstances
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._doNotDisposeSpecialHttpApplicationInstances;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0004B7B8 File Offset: 0x000499B8
		internal static string FormsAuthReturnUrlVar
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._formsAuthReturnUrlVar;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x0004B7C4 File Offset: 0x000499C4
		internal static bool RestrictXmlControls
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._restrictXmlControls;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0004B7D0 File Offset: 0x000499D0
		internal static bool AllowRelaxedRelativeUrl
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowRelaxedRelativeUrl;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0004B7DC File Offset: 0x000499DC
		internal static bool UseLegacyRequestUrlGeneration
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyRequestUrlGeneration;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0004B7E8 File Offset: 0x000499E8
		internal static bool AllowUtf7RequestContentEncoding
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowUtf7RequestContentEncoding;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0004B7F4 File Offset: 0x000499F4
		internal static bool AllowRelaxedUnicodeDecoding
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._allowRelaxedUnicodeDecoding;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0004B800 File Offset: 0x00049A00
		internal static bool DontUsePercentUUrlEncoding
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._dontUsePercentUUrlEncoding;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0004B80C File Offset: 0x00049A0C
		internal static int UpdatePanelMaxScriptLength
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._updatePanelMaxScriptLength;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0004B818 File Offset: 0x00049A18
		internal static int? MaxConcurrentCompilations
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._maxConcurrentCompilations;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0004B824 File Offset: 0x00049A24
		internal static int MaxAcceptLanguageFallbackCount
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._maxAcceptLanguageFallbackCount;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0004B830 File Offset: 0x00049A30
		internal static bool PortableCompilationOutput
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._portableCompilationOutput;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0004B83C File Offset: 0x00049A3C
		internal static string PortableCompilationOutputSnapshotType
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._portableCompilationOutputSnapshotType;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0004B848 File Offset: 0x00049A48
		internal static string PortableCompilationOutputSnapshotTypeOptions
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._portableCompilationOutputSnapshotTypeOptions;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0004B854 File Offset: 0x00049A54
		internal static bool EnsureSessionStateLockedOnFlush
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._ensureSessionStateLockedOnFlush;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0004B860 File Offset: 0x00049A60
		internal static bool UseRandomizedStringHashAlgorithm
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useRandomizedStringHashAlgorithm;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0004B86C File Offset: 0x00049A6C
		internal static bool EnableAsyncModelBinding
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._enableAsyncModelBinding;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0004B878 File Offset: 0x00049A78
		internal static int RequestQueueLimitPerSession
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._requestQueueLimitPerSession;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0004B884 File Offset: 0x00049A84
		internal static bool LogMembershipPasswordFormatWarning
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._logMembershipPasswordFormatWarning;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0004B890 File Offset: 0x00049A90
		internal static bool AvoidDuplicatedSetCookie
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._avoidDuplicatedSetCookie;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0004B89C File Offset: 0x00049A9C
		internal static bool GetValidationMemberName
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._getValidationMemberName;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0004B8A8 File Offset: 0x00049AA8
		internal static bool UseLegacyClientServicesJsonHandling
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyClientServicesJsonHandling;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0004B8B4 File Offset: 0x00049AB4
		internal static bool UseLegacyMultiValueHeaderHandling
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyMultiValueHeaderHandling;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0004B8C0 File Offset: 0x00049AC0
		internal static bool SuppressSameSiteNone
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._suppressSameSiteNone;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0004B8CC File Offset: 0x00049ACC
		internal static bool HandleNoMoreFiles
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._handleNoMoreFiles;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0004B8D8 File Offset: 0x00049AD8
		internal static bool VerifyVirtualPathFromDiskCache
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._verifyVirtualPathFromDiskCache;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		internal static bool FixCookieDefaults
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._fixCookieDefaults;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0004B8F0 File Offset: 0x00049AF0
		internal static bool DisableAppPathModifier
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._disableAppPathModifier;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0004B8FC File Offset: 0x00049AFC
		internal static bool CheckMemoryBytes
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._checkMemoryBytes;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0004B908 File Offset: 0x00049B08
		internal static bool RestoreAggressiveCookielessPathRemoval
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._restoreAggressiveCookielessPathRemoval;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0004B914 File Offset: 0x00049B14
		internal static bool JsonDeserializerLimitedDate
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._jsonDeserializerLimitedDate;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0004B920 File Offset: 0x00049B20
		internal static bool FileNameUtilUseLegacyInvalidChars
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._fileNameUtilUseLegacyInvalidChars;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0004B92C File Offset: 0x00049B2C
		internal static bool UseLegacyCacheKeyHash
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings._useLegacyCacheKeyHash;
			}
		}

		// Token: 0x04001731 RID: 5937
		private static volatile bool _settingsInitialized = false;

		// Token: 0x04001732 RID: 5938
		private static object _appSettingsLock = new object();

		// Token: 0x04001733 RID: 5939
		private static bool _useHostHeaderForRequestUrl;

		// Token: 0x04001734 RID: 5940
		private static bool _allowAnonymousImpersonation;

		// Token: 0x04001735 RID: 5941
		private static bool _scriptResourceAllowNonJsFiles;

		// Token: 0x04001736 RID: 5942
		private static bool _useLegacyEncryption;

		// Token: 0x04001737 RID: 5943
		private static bool _useLegacyMachineKeyEncryption;

		// Token: 0x04001738 RID: 5944
		private static bool _useLegacyFormsAuthenticationTicketCompatibility;

		// Token: 0x04001739 RID: 5945
		private static bool _useLegacyEventValidationCompatibility;

		// Token: 0x0400173A RID: 5946
		private static bool? _allowInsecureDeserialization;

		// Token: 0x0400173B RID: 5947
		private static bool _alwaysIgnoreViewStateValidationErrors;

		// Token: 0x0400173C RID: 5948
		private static bool _allowRelaxedHttpUserName;

		// Token: 0x0400173D RID: 5949
		private static bool _javaScriptDoNotEncodeAmpersand;

		// Token: 0x0400173E RID: 5950
		private static bool _useTaskFriendlySynchronizationContext;

		// Token: 0x0400173F RID: 5951
		private static bool _allowAsyncDuringSyncStages;

		// Token: 0x04001740 RID: 5952
		private const int DefaultMaxHttpCollectionKeys = 2147483647;

		// Token: 0x04001741 RID: 5953
		private static int _maxHttpCollectionKeys = int.MaxValue;

		// Token: 0x04001742 RID: 5954
		private const int DefaultMaxJsonDeserializerMembers = 2147483647;

		// Token: 0x04001743 RID: 5955
		private static int _maxJsonDeserializerMembers = int.MaxValue;

		// Token: 0x04001744 RID: 5956
		private static bool _doNotDisposeSpecialHttpApplicationInstances;

		// Token: 0x04001745 RID: 5957
		private static string _formsAuthReturnUrlVar;

		// Token: 0x04001746 RID: 5958
		private static bool _restrictXmlControls;

		// Token: 0x04001747 RID: 5959
		private static bool _allowRelaxedRelativeUrl;

		// Token: 0x04001748 RID: 5960
		private static bool _useLegacyRequestUrlGeneration;

		// Token: 0x04001749 RID: 5961
		private static bool _allowUtf7RequestContentEncoding;

		// Token: 0x0400174A RID: 5962
		private static bool _allowRelaxedUnicodeDecoding;

		// Token: 0x0400174B RID: 5963
		private static bool _dontUsePercentUUrlEncoding;

		// Token: 0x0400174C RID: 5964
		private static int _updatePanelMaxScriptLength;

		// Token: 0x0400174D RID: 5965
		private static int? _maxConcurrentCompilations;

		// Token: 0x0400174E RID: 5966
		private const int DefaultMaxAcceptLanguageFallbackCount = 3;

		// Token: 0x0400174F RID: 5967
		private static int _maxAcceptLanguageFallbackCount;

		// Token: 0x04001750 RID: 5968
		private static bool _portableCompilationOutput;

		// Token: 0x04001751 RID: 5969
		private static string _portableCompilationOutputSnapshotType;

		// Token: 0x04001752 RID: 5970
		private static string _portableCompilationOutputSnapshotTypeOptions;

		// Token: 0x04001753 RID: 5971
		private static bool _ensureSessionStateLockedOnFlush;

		// Token: 0x04001754 RID: 5972
		private static bool _useRandomizedStringHashAlgorithm;

		// Token: 0x04001755 RID: 5973
		private static bool _enableAsyncModelBinding;

		// Token: 0x04001756 RID: 5974
		internal const int UnlimitedRequestsPerSession = 2147483647;

		// Token: 0x04001757 RID: 5975
		internal const int DefaultRequestQueueLimitPerSession = 50;

		// Token: 0x04001758 RID: 5976
		private static int _requestQueueLimitPerSession;

		// Token: 0x04001759 RID: 5977
		private static bool _logMembershipPasswordFormatWarning;

		// Token: 0x0400175A RID: 5978
		private static bool _avoidDuplicatedSetCookie;

		// Token: 0x0400175B RID: 5979
		private static bool _getValidationMemberName;

		// Token: 0x0400175C RID: 5980
		private static bool _useLegacyClientServicesJsonHandling;

		// Token: 0x0400175D RID: 5981
		private static bool _useLegacyMultiValueHeaderHandling;

		// Token: 0x0400175E RID: 5982
		private static bool _suppressSameSiteNone;

		// Token: 0x0400175F RID: 5983
		private static bool _handleNoMoreFiles;

		// Token: 0x04001760 RID: 5984
		private static bool _verifyVirtualPathFromDiskCache;

		// Token: 0x04001761 RID: 5985
		private static bool _fixCookieDefaults;

		// Token: 0x04001762 RID: 5986
		private static bool _disableAppPathModifier;

		// Token: 0x04001763 RID: 5987
		private static bool _checkMemoryBytes;

		// Token: 0x04001764 RID: 5988
		private static bool _restoreAggressiveCookielessPathRemoval;

		// Token: 0x04001765 RID: 5989
		private static bool _jsonDeserializerLimitedDate;

		// Token: 0x04001766 RID: 5990
		private static bool _fileNameUtilUseLegacyInvalidChars;

		// Token: 0x04001767 RID: 5991
		private static bool _useLegacyCacheKeyHash;
	}
}
