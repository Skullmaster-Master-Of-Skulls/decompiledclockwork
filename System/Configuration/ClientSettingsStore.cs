using System;
using System.Collections;
using System.Configuration.Internal;
using System.IO;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020006E8 RID: 1768
	internal sealed class ClientSettingsStore
	{
		// Token: 0x060036A5 RID: 13989 RVA: 0x000E91D8 File Offset: 0x000E81D8
		private Configuration GetUserConfig(bool isRoaming)
		{
			ConfigurationUserLevel userLevel = isRoaming ? ConfigurationUserLevel.PerUserRoaming : ConfigurationUserLevel.PerUserRoamingAndLocal;
			return ClientSettingsStore.ClientSettingsConfigurationHost.OpenExeConfiguration(userLevel);
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000E91F8 File Offset: 0x000E81F8
		private ClientSettingsSection GetConfigSection(Configuration config, string sectionName, bool declare)
		{
			string sectionName2 = "userSettings/" + sectionName;
			ClientSettingsSection clientSettingsSection = null;
			if (config != null)
			{
				clientSettingsSection = (config.GetSection(sectionName2) as ClientSettingsSection);
				if (clientSettingsSection == null && declare)
				{
					this.DeclareSection(config, sectionName);
					clientSettingsSection = (config.GetSection(sectionName2) as ClientSettingsSection);
				}
			}
			return clientSettingsSection;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000E9240 File Offset: 0x000E8240
		private void DeclareSection(Configuration config, string sectionName)
		{
			if (config.GetSectionGroup("userSettings") == null)
			{
				ConfigurationSectionGroup sectionGroup = new UserSettingsGroup();
				config.SectionGroups.Add("userSettings", sectionGroup);
			}
			ConfigurationSectionGroup sectionGroup2 = config.GetSectionGroup("userSettings");
			if (sectionGroup2 != null && sectionGroup2.Sections[sectionName] == null)
			{
				ConfigurationSection configurationSection = new ClientSettingsSection();
				configurationSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
				configurationSection.SectionInformation.RequirePermission = false;
				sectionGroup2.Sections.Add(sectionName, configurationSection);
			}
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000E92C4 File Offset: 0x000E82C4
		internal IDictionary ReadSettings(string sectionName, bool isUserScoped)
		{
			IDictionary dictionary = new Hashtable();
			if (isUserScoped && !ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				return dictionary;
			}
			string str = isUserScoped ? "userSettings/" : "applicationSettings/";
			ConfigurationManager.RefreshSection(str + sectionName);
			ClientSettingsSection clientSettingsSection = ConfigurationManager.GetSection(str + sectionName) as ClientSettingsSection;
			if (clientSettingsSection != null)
			{
				foreach (object obj in clientSettingsSection.Settings)
				{
					SettingElement settingElement = (SettingElement)obj;
					dictionary[settingElement.Name] = new StoredSetting(settingElement.SerializeAs, settingElement.Value.ValueXml);
				}
			}
			return dictionary;
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000E9390 File Offset: 0x000E8390
		internal static IDictionary ReadSettingsFromFile(string configFileName, string sectionName, bool isUserScoped)
		{
			IDictionary dictionary = new Hashtable();
			if (isUserScoped && !ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				return dictionary;
			}
			string str = isUserScoped ? "userSettings/" : "applicationSettings/";
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			ConfigurationUserLevel userLevel = isUserScoped ? ConfigurationUserLevel.PerUserRoaming : ConfigurationUserLevel.None;
			if (isUserScoped)
			{
				exeConfigurationFileMap.ExeConfigFilename = ConfigurationManagerInternalFactory.Instance.ApplicationConfigUri;
				exeConfigurationFileMap.RoamingUserConfigFilename = configFileName;
			}
			else
			{
				exeConfigurationFileMap.ExeConfigFilename = configFileName;
			}
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, userLevel);
			ClientSettingsSection clientSettingsSection = configuration.GetSection(str + sectionName) as ClientSettingsSection;
			if (clientSettingsSection != null)
			{
				foreach (object obj in clientSettingsSection.Settings)
				{
					SettingElement settingElement = (SettingElement)obj;
					dictionary[settingElement.Name] = new StoredSetting(settingElement.SerializeAs, settingElement.Value.ValueXml);
				}
			}
			return dictionary;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000E9494 File Offset: 0x000E8494
		internal ConnectionStringSettingsCollection ReadConnectionStrings()
		{
			return PrivilegedConfigurationManager.ConnectionStrings;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000E949C File Offset: 0x000E849C
		internal void RevertToParent(string sectionName, bool isRoaming)
		{
			if (!ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
			}
			Configuration userConfig = this.GetUserConfig(isRoaming);
			ClientSettingsSection configSection = this.GetConfigSection(userConfig, sectionName, false);
			if (configSection != null)
			{
				configSection.SectionInformation.RevertToParent();
				userConfig.Save();
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000E94EC File Offset: 0x000E84EC
		internal void WriteSettings(string sectionName, bool isRoaming, IDictionary newSettings)
		{
			if (!ConfigurationManagerInternalFactory.Instance.SupportsUserConfig)
			{
				throw new ConfigurationErrorsException(SR.GetString("UserSettingsNotSupported"));
			}
			Configuration userConfig = this.GetUserConfig(isRoaming);
			ClientSettingsSection configSection = this.GetConfigSection(userConfig, sectionName, true);
			if (configSection != null)
			{
				SettingElementCollection settings = configSection.Settings;
				foreach (object obj in newSettings)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					SettingElement settingElement = settings.Get((string)dictionaryEntry.Key);
					if (settingElement == null)
					{
						settingElement = new SettingElement();
						settingElement.Name = (string)dictionaryEntry.Key;
						settings.Add(settingElement);
					}
					StoredSetting storedSetting = (StoredSetting)dictionaryEntry.Value;
					settingElement.SerializeAs = storedSetting.SerializeAs;
					settingElement.Value.ValueXml = storedSetting.Value;
				}
				try
				{
					userConfig.Save();
					return;
				}
				catch (ConfigurationErrorsException ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("SettingsSaveFailed", new object[]
					{
						ex.Message
					}), ex);
				}
			}
			throw new ConfigurationErrorsException(SR.GetString("SettingsSaveFailedNoSection"));
		}

		// Token: 0x040031A0 RID: 12704
		private const string ApplicationSettingsGroupName = "applicationSettings";

		// Token: 0x040031A1 RID: 12705
		private const string UserSettingsGroupName = "userSettings";

		// Token: 0x040031A2 RID: 12706
		private const string ApplicationSettingsGroupPrefix = "applicationSettings/";

		// Token: 0x040031A3 RID: 12707
		private const string UserSettingsGroupPrefix = "userSettings/";

		// Token: 0x020006E9 RID: 1769
		private sealed class ClientSettingsConfigurationHost : DelegatingConfigHost
		{
			// Token: 0x17000CA9 RID: 3241
			// (get) Token: 0x060036AE RID: 13998 RVA: 0x000E963C File Offset: 0x000E863C
			private IInternalConfigClientHost ClientHost
			{
				get
				{
					return (IInternalConfigClientHost)base.Host;
				}
			}

			// Token: 0x17000CAA RID: 3242
			// (get) Token: 0x060036AF RID: 13999 RVA: 0x000E9649 File Offset: 0x000E8649
			internal static IInternalConfigConfigurationFactory ConfigFactory
			{
				get
				{
					if (ClientSettingsStore.ClientSettingsConfigurationHost.s_configFactory == null)
					{
						ClientSettingsStore.ClientSettingsConfigurationHost.s_configFactory = (IInternalConfigConfigurationFactory)TypeUtil.CreateInstanceWithReflectionPermission("System.Configuration.Internal.InternalConfigConfigurationFactory,System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					}
					return ClientSettingsStore.ClientSettingsConfigurationHost.s_configFactory;
				}
			}

			// Token: 0x060036B0 RID: 14000 RVA: 0x000E966B File Offset: 0x000E866B
			private ClientSettingsConfigurationHost()
			{
			}

			// Token: 0x060036B1 RID: 14001 RVA: 0x000E9673 File Offset: 0x000E8673
			public override void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
			{
			}

			// Token: 0x060036B2 RID: 14002 RVA: 0x000E9678 File Offset: 0x000E8678
			public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
			{
				ConfigurationUserLevel configurationUserLevel = (ConfigurationUserLevel)hostInitConfigurationParams[0];
				base.Host = (IInternalConfigHost)TypeUtil.CreateInstanceWithReflectionPermission("System.Configuration.ClientConfigurationHost,System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				ConfigurationUserLevel configurationUserLevel2 = configurationUserLevel;
				string text;
				if (configurationUserLevel2 != ConfigurationUserLevel.None)
				{
					if (configurationUserLevel2 != ConfigurationUserLevel.PerUserRoaming)
					{
						if (configurationUserLevel2 != ConfigurationUserLevel.PerUserRoamingAndLocal)
						{
							throw new ArgumentException(SR.GetString("UnknownUserLevel"));
						}
						text = this.ClientHost.GetLocalUserConfigPath();
					}
					else
					{
						text = this.ClientHost.GetRoamingUserConfigPath();
					}
				}
				else
				{
					text = this.ClientHost.GetExeConfigPath();
				}
				base.Host.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath, configRoot, new object[]
				{
					null,
					null,
					text
				});
			}

			// Token: 0x060036B3 RID: 14003 RVA: 0x000E9710 File Offset: 0x000E8710
			private bool IsKnownConfigFile(string filename)
			{
				return string.Equals(filename, ConfigurationManagerInternalFactory.Instance.MachineConfigPath, StringComparison.OrdinalIgnoreCase) || string.Equals(filename, ConfigurationManagerInternalFactory.Instance.ApplicationConfigUri, StringComparison.OrdinalIgnoreCase) || string.Equals(filename, ConfigurationManagerInternalFactory.Instance.ExeLocalConfigPath, StringComparison.OrdinalIgnoreCase) || string.Equals(filename, ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigPath, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060036B4 RID: 14004 RVA: 0x000E976C File Offset: 0x000E876C
			internal static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
			{
				return ClientSettingsStore.ClientSettingsConfigurationHost.ConfigFactory.Create(typeof(ClientSettingsStore.ClientSettingsConfigurationHost), new object[]
				{
					userLevel
				});
			}

			// Token: 0x060036B5 RID: 14005 RVA: 0x000E979E File Offset: 0x000E879E
			public override Stream OpenStreamForRead(string streamName)
			{
				if (this.IsKnownConfigFile(streamName))
				{
					return base.Host.OpenStreamForRead(streamName, true);
				}
				return base.Host.OpenStreamForRead(streamName);
			}

			// Token: 0x060036B6 RID: 14006 RVA: 0x000E97C4 File Offset: 0x000E87C4
			public override Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
			{
				Stream result;
				if (string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeLocalConfigPath, StringComparison.OrdinalIgnoreCase))
				{
					result = new ClientSettingsStore.QuotaEnforcedStream(base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, true), false);
				}
				else if (string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigPath, StringComparison.OrdinalIgnoreCase))
				{
					result = new ClientSettingsStore.QuotaEnforcedStream(base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, true), true);
				}
				else
				{
					result = base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
				}
				return result;
			}

			// Token: 0x060036B7 RID: 14007 RVA: 0x000E983C File Offset: 0x000E883C
			public override void WriteCompleted(string streamName, bool success, object writeContext)
			{
				if (string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeLocalConfigPath, StringComparison.OrdinalIgnoreCase) || string.Equals(streamName, ConfigurationManagerInternalFactory.Instance.ExeRoamingConfigPath, StringComparison.OrdinalIgnoreCase))
				{
					base.Host.WriteCompleted(streamName, success, writeContext, true);
					return;
				}
				base.Host.WriteCompleted(streamName, success, writeContext);
			}

			// Token: 0x040031A4 RID: 12708
			private const string ClientConfigurationHostTypeName = "System.Configuration.ClientConfigurationHost,System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040031A5 RID: 12709
			private const string InternalConfigConfigurationFactoryTypeName = "System.Configuration.Internal.InternalConfigConfigurationFactory,System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040031A6 RID: 12710
			private static IInternalConfigConfigurationFactory s_configFactory;
		}

		// Token: 0x020006EA RID: 1770
		private sealed class QuotaEnforcedStream : Stream
		{
			// Token: 0x060036B8 RID: 14008 RVA: 0x000E988D File Offset: 0x000E888D
			internal QuotaEnforcedStream(Stream originalStream, bool isRoaming)
			{
				this._originalStream = originalStream;
				this._isRoaming = isRoaming;
			}

			// Token: 0x17000CAB RID: 3243
			// (get) Token: 0x060036B9 RID: 14009 RVA: 0x000E98A3 File Offset: 0x000E88A3
			public override bool CanRead
			{
				get
				{
					return this._originalStream.CanRead;
				}
			}

			// Token: 0x17000CAC RID: 3244
			// (get) Token: 0x060036BA RID: 14010 RVA: 0x000E98B0 File Offset: 0x000E88B0
			public override bool CanWrite
			{
				get
				{
					return this._originalStream.CanWrite;
				}
			}

			// Token: 0x17000CAD RID: 3245
			// (get) Token: 0x060036BB RID: 14011 RVA: 0x000E98BD File Offset: 0x000E88BD
			public override bool CanSeek
			{
				get
				{
					return this._originalStream.CanSeek;
				}
			}

			// Token: 0x17000CAE RID: 3246
			// (get) Token: 0x060036BC RID: 14012 RVA: 0x000E98CA File Offset: 0x000E88CA
			public override long Length
			{
				get
				{
					return this._originalStream.Length;
				}
			}

			// Token: 0x17000CAF RID: 3247
			// (get) Token: 0x060036BD RID: 14013 RVA: 0x000E98D7 File Offset: 0x000E88D7
			// (set) Token: 0x060036BE RID: 14014 RVA: 0x000E98E4 File Offset: 0x000E88E4
			public override long Position
			{
				get
				{
					return this._originalStream.Position;
				}
				set
				{
					if (value < 0L)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("PositionOutOfRange"));
					}
					this.Seek(value, SeekOrigin.Begin);
				}
			}

			// Token: 0x060036BF RID: 14015 RVA: 0x000E9909 File Offset: 0x000E8909
			public override void Close()
			{
				this._originalStream.Close();
			}

			// Token: 0x060036C0 RID: 14016 RVA: 0x000E9916 File Offset: 0x000E8916
			protected override void Dispose(bool disposing)
			{
				if (disposing && this._originalStream != null)
				{
					((IDisposable)this._originalStream).Dispose();
					this._originalStream = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x060036C1 RID: 14017 RVA: 0x000E993C File Offset: 0x000E893C
			public override void Flush()
			{
				this._originalStream.Flush();
			}

			// Token: 0x060036C2 RID: 14018 RVA: 0x000E994C File Offset: 0x000E894C
			public override void SetLength(long value)
			{
				long length = this._originalStream.Length;
				this.EnsureQuota(Math.Max(length, value));
				this._originalStream.SetLength(value);
			}

			// Token: 0x060036C3 RID: 14019 RVA: 0x000E9980 File Offset: 0x000E8980
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this._originalStream.Read(buffer, offset, count);
			}

			// Token: 0x060036C4 RID: 14020 RVA: 0x000E9990 File Offset: 0x000E8990
			public override int ReadByte()
			{
				return this._originalStream.ReadByte();
			}

			// Token: 0x060036C5 RID: 14021 RVA: 0x000E99A0 File Offset: 0x000E89A0
			public override long Seek(long offset, SeekOrigin origin)
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long val;
				switch (origin)
				{
				case SeekOrigin.Begin:
					val = offset;
					break;
				case SeekOrigin.Current:
					val = this._originalStream.Position + offset;
					break;
				case SeekOrigin.End:
					val = length + offset;
					break;
				default:
					throw new ArgumentException(SR.GetString("UnknownSeekOrigin"), "origin");
				}
				this.EnsureQuota(Math.Max(length, val));
				return this._originalStream.Seek(offset, origin);
			}

			// Token: 0x060036C6 RID: 14022 RVA: 0x000E9A28 File Offset: 0x000E8A28
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (!this.CanWrite)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long val = this._originalStream.CanSeek ? (this._originalStream.Position + (long)count) : (this._originalStream.Length + (long)count);
				this.EnsureQuota(Math.Max(length, val));
				this._originalStream.Write(buffer, offset, count);
			}

			// Token: 0x060036C7 RID: 14023 RVA: 0x000E9A98 File Offset: 0x000E8A98
			public override void WriteByte(byte value)
			{
				if (!this.CanWrite)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long val = this._originalStream.CanSeek ? (this._originalStream.Position + 1L) : (this._originalStream.Length + 1L);
				this.EnsureQuota(Math.Max(length, val));
				this._originalStream.WriteByte(value);
			}

			// Token: 0x060036C8 RID: 14024 RVA: 0x000E9B04 File Offset: 0x000E8B04
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
			{
				return this._originalStream.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
			}

			// Token: 0x060036C9 RID: 14025 RVA: 0x000E9B18 File Offset: 0x000E8B18
			public override int EndRead(IAsyncResult asyncResult)
			{
				return this._originalStream.EndRead(asyncResult);
			}

			// Token: 0x060036CA RID: 14026 RVA: 0x000E9B28 File Offset: 0x000E8B28
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
			{
				if (!this.CanWrite)
				{
					throw new NotSupportedException();
				}
				long length = this._originalStream.Length;
				long val = this._originalStream.CanSeek ? (this._originalStream.Position + (long)numBytes) : (this._originalStream.Length + (long)numBytes);
				this.EnsureQuota(Math.Max(length, val));
				return this._originalStream.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
			}

			// Token: 0x060036CB RID: 14027 RVA: 0x000E9B9A File Offset: 0x000E8B9A
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this._originalStream.EndWrite(asyncResult);
			}

			// Token: 0x060036CC RID: 14028 RVA: 0x000E9BA8 File Offset: 0x000E8BA8
			private void EnsureQuota(long size)
			{
				new IsolatedStorageFilePermission(PermissionState.None)
				{
					UserQuota = size,
					UsageAllowed = (this._isRoaming ? IsolatedStorageContainment.DomainIsolationByRoamingUser : IsolatedStorageContainment.DomainIsolationByUser)
				}.Demand();
			}

			// Token: 0x040031A7 RID: 12711
			private Stream _originalStream;

			// Token: 0x040031A8 RID: 12712
			private bool _isRoaming;
		}
	}
}
