using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Configuration.Internal
{
	// Token: 0x020000B3 RID: 179
	[ComVisible(false)]
	public interface IInternalConfigHost
	{
		// Token: 0x06000703 RID: 1795
		void Init(IInternalConfigRoot configRoot, params object[] hostInitParams);

		// Token: 0x06000704 RID: 1796
		void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams);

		// Token: 0x06000705 RID: 1797
		bool IsConfigRecordRequired(string configPath);

		// Token: 0x06000706 RID: 1798
		bool IsInitDelayed(IInternalConfigRecord configRecord);

		// Token: 0x06000707 RID: 1799
		void RequireCompleteInit(IInternalConfigRecord configRecord);

		// Token: 0x06000708 RID: 1800
		bool IsSecondaryRoot(string configPath);

		// Token: 0x06000709 RID: 1801
		string GetStreamName(string configPath);

		// Token: 0x0600070A RID: 1802
		string GetStreamNameForConfigSource(string streamName, string configSource);

		// Token: 0x0600070B RID: 1803
		object GetStreamVersion(string streamName);

		// Token: 0x0600070C RID: 1804
		Stream OpenStreamForRead(string streamName);

		// Token: 0x0600070D RID: 1805
		Stream OpenStreamForRead(string streamName, bool assertPermissions);

		// Token: 0x0600070E RID: 1806
		Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext);

		// Token: 0x0600070F RID: 1807
		Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions);

		// Token: 0x06000710 RID: 1808
		void WriteCompleted(string streamName, bool success, object writeContext);

		// Token: 0x06000711 RID: 1809
		void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions);

		// Token: 0x06000712 RID: 1810
		void DeleteStream(string streamName);

		// Token: 0x06000713 RID: 1811
		bool IsFile(string streamName);

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000714 RID: 1812
		bool SupportsChangeNotifications { get; }

		// Token: 0x06000715 RID: 1813
		object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

		// Token: 0x06000716 RID: 1814
		void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000717 RID: 1815
		bool SupportsRefresh { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000718 RID: 1816
		bool SupportsPath { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000719 RID: 1817
		bool SupportsLocation { get; }

		// Token: 0x0600071A RID: 1818
		bool IsAboveApplication(string configPath);

		// Token: 0x0600071B RID: 1819
		string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath);

		// Token: 0x0600071C RID: 1820
		bool IsLocationApplicable(string configPath);

		// Token: 0x0600071D RID: 1821
		bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition);

		// Token: 0x0600071E RID: 1822
		void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo);

		// Token: 0x0600071F RID: 1823
		bool IsTrustedConfigPath(string configPath);

		// Token: 0x06000720 RID: 1824
		bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord);

		// Token: 0x06000721 RID: 1825
		void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady);

		// Token: 0x06000722 RID: 1826
		IDisposable Impersonate();

		// Token: 0x06000723 RID: 1827
		bool PrefetchAll(string configPath, string streamName);

		// Token: 0x06000724 RID: 1828
		bool PrefetchSection(string sectionGroupName, string sectionName);

		// Token: 0x06000725 RID: 1829
		object CreateDeprecatedConfigContext(string configPath);

		// Token: 0x06000726 RID: 1830
		object CreateConfigurationContext(string configPath, string locationSubPath);

		// Token: 0x06000727 RID: 1831
		string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection);

		// Token: 0x06000728 RID: 1832
		string EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection);

		// Token: 0x06000729 RID: 1833
		Type GetConfigType(string typeName, bool throwOnError);

		// Token: 0x0600072A RID: 1834
		string GetConfigTypeName(Type t);

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600072B RID: 1835
		bool IsRemote { get; }
	}
}
