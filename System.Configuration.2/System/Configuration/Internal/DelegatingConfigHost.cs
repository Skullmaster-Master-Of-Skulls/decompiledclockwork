using System;
using System.IO;
using System.Security;
using System.Xml;

namespace System.Configuration.Internal
{
	// Token: 0x020000AB RID: 171
	public class DelegatingConfigHost : IInternalConfigHost, IInternalConfigurationBuilderHost
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x000115BE File Offset: 0x0000F7BE
		protected DelegatingConfigHost()
		{
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001F679 File Offset: 0x0001D879
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001F681 File Offset: 0x0001D881
		protected IInternalConfigHost Host
		{
			get
			{
				return this._host;
			}
			set
			{
				this._host = value;
				this._configBuilderHost = (this._host as IInternalConfigurationBuilderHost);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001F69B File Offset: 0x0001D89B
		protected IInternalConfigurationBuilderHost ConfigBuilderHost
		{
			get
			{
				return this._configBuilderHost;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001F6A3 File Offset: 0x0001D8A3
		public virtual void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			this.Host.Init(configRoot, hostInitParams);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001F6B2 File Offset: 0x0001D8B2
		public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
		{
			this.Host.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath, configRoot, hostInitConfigurationParams);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001F6C6 File Offset: 0x0001D8C6
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			return this.Host.IsConfigRecordRequired(configPath);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			return this.Host.IsInitDelayed(configRecord);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001F6E2 File Offset: 0x0001D8E2
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			this.Host.RequireCompleteInit(configRecord);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		public virtual bool IsSecondaryRoot(string configPath)
		{
			return this.Host.IsSecondaryRoot(configPath);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001F6FE File Offset: 0x0001D8FE
		public virtual string GetStreamName(string configPath)
		{
			return this.Host.GetStreamName(configPath);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001F70C File Offset: 0x0001D90C
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			return this.Host.GetStreamNameForConfigSource(streamName, configSource);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001F71B File Offset: 0x0001D91B
		public virtual object GetStreamVersion(string streamName)
		{
			return this.Host.GetStreamVersion(streamName);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001F729 File Offset: 0x0001D929
		public virtual Stream OpenStreamForRead(string streamName)
		{
			return this.Host.OpenStreamForRead(streamName);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001F737 File Offset: 0x0001D937
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			return this.Host.OpenStreamForRead(streamName, assertPermissions);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001F746 File Offset: 0x0001D946
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			return this.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001F756 File Offset: 0x0001D956
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			return this.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, assertPermissions);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001F768 File Offset: 0x0001D968
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
			this.Host.WriteCompleted(streamName, success, writeContext);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001F778 File Offset: 0x0001D978
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			this.Host.WriteCompleted(streamName, success, writeContext, assertPermissions);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001F78A File Offset: 0x0001D98A
		public virtual void DeleteStream(string streamName)
		{
			this.Host.DeleteStream(streamName);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001F798 File Offset: 0x0001D998
		public virtual bool IsFile(string streamName)
		{
			return this.Host.IsFile(streamName);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001F7A6 File Offset: 0x0001D9A6
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return this.Host.SupportsChangeNotifications;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			return this.Host.StartMonitoringStreamForChanges(streamName, callback);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001F7C2 File Offset: 0x0001D9C2
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			this.Host.StopMonitoringStreamForChanges(streamName, callback);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001F7D1 File Offset: 0x0001D9D1
		public virtual bool SupportsRefresh
		{
			get
			{
				return this.Host.SupportsRefresh;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001F7DE File Offset: 0x0001D9DE
		public virtual bool SupportsPath
		{
			get
			{
				return this.Host.SupportsPath;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001F7EB File Offset: 0x0001D9EB
		public virtual bool SupportsLocation
		{
			get
			{
				return this.Host.SupportsLocation;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		public virtual bool IsAboveApplication(string configPath)
		{
			return this.Host.IsAboveApplication(configPath);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001F806 File Offset: 0x0001DA06
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return this.Host.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001F816 File Offset: 0x0001DA16
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			this.Host.VerifyDefinitionAllowed(configPath, allowDefinition, allowExeDefinition, errorInfo);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001F828 File Offset: 0x0001DA28
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return this.Host.GetConfigPathFromLocationSubPath(configPath, locationSubPath);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001F837 File Offset: 0x0001DA37
		public virtual bool IsLocationApplicable(string configPath)
		{
			return this.Host.IsLocationApplicable(configPath);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001F845 File Offset: 0x0001DA45
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			return this.Host.IsTrustedConfigPath(configPath);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001F853 File Offset: 0x0001DA53
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			return this.Host.IsFullTrustSectionWithoutAptcaAllowed(configRecord);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001F861 File Offset: 0x0001DA61
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			this.Host.GetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001F871 File Offset: 0x0001DA71
		public virtual IDisposable Impersonate()
		{
			return this.Host.Impersonate();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001F87E File Offset: 0x0001DA7E
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			return this.Host.PrefetchAll(configPath, streamName);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001F88D File Offset: 0x0001DA8D
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return this.Host.PrefetchSection(sectionGroupName, sectionName);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001F89C File Offset: 0x0001DA9C
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return this.Host.CreateDeprecatedConfigContext(configPath);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001F8AA File Offset: 0x0001DAAA
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return this.Host.CreateConfigurationContext(configPath, locationSubPath);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001F8B9 File Offset: 0x0001DAB9
		public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return this.Host.DecryptSection(encryptedXml, protectionProvider, protectedConfigSection);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001F8C9 File Offset: 0x0001DAC9
		public virtual string EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return this.Host.EncryptSection(clearTextXml, protectionProvider, protectedConfigSection);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001F8D9 File Offset: 0x0001DAD9
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			return this.Host.GetConfigType(typeName, throwOnError);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public virtual string GetConfigTypeName(Type t)
		{
			return this.Host.GetConfigTypeName(t);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001F8F6 File Offset: 0x0001DAF6
		public virtual bool IsRemote
		{
			get
			{
				return this.Host.IsRemote;
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001F903 File Offset: 0x0001DB03
		public virtual XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder)
		{
			if (this.ConfigBuilderHost != null)
			{
				return this.ConfigBuilderHost.ProcessRawXml(rawXml, builder);
			}
			return rawXml;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001F91C File Offset: 0x0001DB1C
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder)
		{
			if (this.ConfigBuilderHost != null)
			{
				return this.ConfigBuilderHost.ProcessConfigurationSection(configSection, builder);
			}
			return configSection;
		}

		// Token: 0x0400044E RID: 1102
		private IInternalConfigHost _host;

		// Token: 0x0400044F RID: 1103
		private IInternalConfigurationBuilderHost _configBuilderHost;
	}
}
