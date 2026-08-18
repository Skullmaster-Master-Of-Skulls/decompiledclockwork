using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200004E RID: 78
	internal static class ServiceModelAppSettings
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000AEFB File Offset: 0x000090FB
		internal static bool UseLegacyCertificateUsagePolicy
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.useLegacyCertificateUsagePolicy;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000AF07 File Offset: 0x00009107
		internal static bool HttpTransportPerFactoryConnectionPool
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.httpTransportPerFactoryConnectionPool;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000AF13 File Offset: 0x00009113
		internal static bool EnsureUniquePerformanceCounterInstanceNames
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.ensureUniquePerformanceCounterInstanceNames;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000AF1F File Offset: 0x0000911F
		internal static bool DisableOperationContextAsyncFlow
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.disableOperationContextAsyncFlow;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000AF2B File Offset: 0x0000912B
		internal static bool UseConfiguredTransportSecurityHeaderLayout
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.useConfiguredTransportSecurityHeaderLayout;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000AF37 File Offset: 0x00009137
		internal static bool UseBestMatchNamedPipeUri
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.useBestMatchNamedPipeUri;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000AF43 File Offset: 0x00009143
		internal static bool DeferSslStreamServerCertificateCleanup
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.deferSslStreamServerCertificateCleanup;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000AF4F File Offset: 0x0000914F
		internal static bool FailOnSocketDuplicationError
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.failOnSocketDuplicationError;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000AF5B File Offset: 0x0000915B
		internal static bool EnsureStreamUpgradeOpenTimeout
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.ensureStreamUpgradeOpenTimeout;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000AF67 File Offset: 0x00009167
		internal static bool EnableLegacyUpnUsernameFix
		{
			get
			{
				ServiceModelAppSettings.EnsureSettingsLoaded();
				return ServiceModelAppSettings.enableLegacyUpnUsernameFix;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000AF74 File Offset: 0x00009174
		private static void EnsureSettingsLoaded()
		{
			if (!ServiceModelAppSettings.settingsInitalized)
			{
				object obj = ServiceModelAppSettings.appSettingsLock;
				lock (obj)
				{
					if (!ServiceModelAppSettings.settingsInitalized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = ConfigurationManager.AppSettings;
						}
						catch (ConfigurationErrorsException)
						{
						}
						finally
						{
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:useLegacyCertificateUsagePolicy"], out ServiceModelAppSettings.useLegacyCertificateUsagePolicy))
							{
								ServiceModelAppSettings.useLegacyCertificateUsagePolicy = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:httpTransportBinding:useUniqueConnectionPoolPerFactory"], out ServiceModelAppSettings.httpTransportPerFactoryConnectionPool))
							{
								ServiceModelAppSettings.httpTransportPerFactoryConnectionPool = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:ensureUniquePerformanceCounterInstanceNames"], out ServiceModelAppSettings.ensureUniquePerformanceCounterInstanceNames))
							{
								ServiceModelAppSettings.ensureUniquePerformanceCounterInstanceNames = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:disableOperationContextAsyncFlow"], out ServiceModelAppSettings.disableOperationContextAsyncFlow))
							{
								ServiceModelAppSettings.disableOperationContextAsyncFlow = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:useConfiguredTransportSecurityHeaderLayout"], out ServiceModelAppSettings.useConfiguredTransportSecurityHeaderLayout))
							{
								ServiceModelAppSettings.useConfiguredTransportSecurityHeaderLayout = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:useBestMatchNamedPipeUri"], out ServiceModelAppSettings.useBestMatchNamedPipeUri))
							{
								ServiceModelAppSettings.useBestMatchNamedPipeUri = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:deferSslStreamServerCertificateCleanup"], out ServiceModelAppSettings.deferSslStreamServerCertificateCleanup))
							{
								ServiceModelAppSettings.deferSslStreamServerCertificateCleanup = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:failOnSocketDuplicationError"], out ServiceModelAppSettings.failOnSocketDuplicationError))
							{
								ServiceModelAppSettings.failOnSocketDuplicationError = false;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:ensureStreamUpgradeOpenTimeout"], out ServiceModelAppSettings.ensureStreamUpgradeOpenTimeout))
							{
								ServiceModelAppSettings.ensureStreamUpgradeOpenTimeout = true;
							}
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["wcf:enableLegacyUpnUsernameFix"], out ServiceModelAppSettings.enableLegacyUpnUsernameFix))
							{
								ServiceModelAppSettings.enableLegacyUpnUsernameFix = false;
							}
							ServiceModelAppSettings.settingsInitalized = true;
						}
					}
				}
			}
		}

		// Token: 0x0400029E RID: 670
		internal const string HttpTransportPerFactoryConnectionPoolString = "wcf:httpTransportBinding:useUniqueConnectionPoolPerFactory";

		// Token: 0x0400029F RID: 671
		internal const string EnsureUniquePerformanceCounterInstanceNamesString = "wcf:ensureUniquePerformanceCounterInstanceNames";

		// Token: 0x040002A0 RID: 672
		internal const string UseConfiguredTransportSecurityHeaderLayoutString = "wcf:useConfiguredTransportSecurityHeaderLayout";

		// Token: 0x040002A1 RID: 673
		internal const string UseBestMatchNamedPipeUriString = "wcf:useBestMatchNamedPipeUri";

		// Token: 0x040002A2 RID: 674
		internal const string DisableOperationContextAsyncFlowString = "wcf:disableOperationContextAsyncFlow";

		// Token: 0x040002A3 RID: 675
		internal const string UseLegacyCertificateUsagePolicyString = "wcf:useLegacyCertificateUsagePolicy";

		// Token: 0x040002A4 RID: 676
		internal const string DeferSslStreamServerCertificateCleanupString = "wcf:deferSslStreamServerCertificateCleanup";

		// Token: 0x040002A5 RID: 677
		internal const string FailOnSocketDuplicationErrorString = "wcf:failOnSocketDuplicationError";

		// Token: 0x040002A6 RID: 678
		internal const string EnsureStreamUpgradeOpenTimeoutString = "wcf:ensureStreamUpgradeOpenTimeout";

		// Token: 0x040002A7 RID: 679
		internal const string EnableLegacyUpnUsernameFixString = "wcf:enableLegacyUpnUsernameFix";

		// Token: 0x040002A8 RID: 680
		private const bool DefaultHttpTransportPerFactoryConnectionPool = false;

		// Token: 0x040002A9 RID: 681
		private const bool DefaultEnsureUniquePerformanceCounterInstanceNames = false;

		// Token: 0x040002AA RID: 682
		private const bool DefaultUseConfiguredTransportSecurityHeaderLayout = false;

		// Token: 0x040002AB RID: 683
		private const bool DefaultUseBestMatchNamedPipeUri = false;

		// Token: 0x040002AC RID: 684
		private const bool DefaultUseLegacyCertificateUsagePolicy = false;

		// Token: 0x040002AD RID: 685
		private const bool DefaultDisableOperationContextAsyncFlow = true;

		// Token: 0x040002AE RID: 686
		private const bool DefaultDeferSslStreamServerCertificateCleanup = false;

		// Token: 0x040002AF RID: 687
		private const bool DefaultFailOnSocketDuplicationError = false;

		// Token: 0x040002B0 RID: 688
		private const bool DefaultEnsureStreamUpgradeOpenTimeout = true;

		// Token: 0x040002B1 RID: 689
		private const bool DefaultEnableLegacyUpnUsernameFix = false;

		// Token: 0x040002B2 RID: 690
		private static bool useLegacyCertificateUsagePolicy;

		// Token: 0x040002B3 RID: 691
		private static bool httpTransportPerFactoryConnectionPool;

		// Token: 0x040002B4 RID: 692
		private static bool ensureUniquePerformanceCounterInstanceNames;

		// Token: 0x040002B5 RID: 693
		private static bool useConfiguredTransportSecurityHeaderLayout;

		// Token: 0x040002B6 RID: 694
		private static bool useBestMatchNamedPipeUri;

		// Token: 0x040002B7 RID: 695
		private static bool disableOperationContextAsyncFlow;

		// Token: 0x040002B8 RID: 696
		private static bool deferSslStreamServerCertificateCleanup;

		// Token: 0x040002B9 RID: 697
		private static bool failOnSocketDuplicationError;

		// Token: 0x040002BA RID: 698
		private static bool ensureStreamUpgradeOpenTimeout;

		// Token: 0x040002BB RID: 699
		private static bool enableLegacyUpnUsernameFix;

		// Token: 0x040002BC RID: 700
		private static volatile bool settingsInitalized = false;

		// Token: 0x040002BD RID: 701
		private static object appSettingsLock = new object();
	}
}
