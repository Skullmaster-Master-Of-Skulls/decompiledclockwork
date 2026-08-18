using System;
using System.Configuration.Internal;
using System.Threading;

namespace System.Configuration
{
	// Token: 0x02000017 RID: 23
	internal sealed class ClientConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00008A50 File Offset: 0x00006C50
		internal ClientConfigurationSystem()
		{
			this._configSystem = new ConfigSystem();
			this._configSystem.Init(typeof(ClientConfigurationHost), new object[2]);
			this._configHost = (ClientConfigurationHost)this._configSystem.Host;
			this._configRoot = this._configSystem.Root;
			this._configRoot.ConfigRemoved += this.OnConfigRemoved;
			this._isAppConfigHttp = this._configHost.IsAppConfigHttp;
			string schemeDelimiter = Uri.SchemeDelimiter;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008ADE File Offset: 0x00006CDE
		private bool IsSectionUsedInInit(string configKey)
		{
			return configKey == "system.diagnostics" || (this._isAppConfigHttp && configKey.StartsWith("system.net/", StringComparison.Ordinal));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008B05 File Offset: 0x00006D05
		private bool DoesSectionOnlyUseMachineConfig(string configKey)
		{
			return this._isAppConfigHttp && configKey.StartsWith("system.net/", StringComparison.Ordinal);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008B20 File Offset: 0x00006D20
		private void EnsureInit(string configKey)
		{
			bool flag = false;
			lock (this)
			{
				if (!this._isUserConfigInited)
				{
					if (!this._isInitInProgress)
					{
						this._isInitInProgress = true;
						flag = true;
					}
					else if (!this.IsSectionUsedInInit(configKey))
					{
						Monitor.Wait(this);
					}
				}
			}
			if (flag)
			{
				try
				{
					try
					{
						this._machineConfigRecord = this._configRoot.GetConfigRecord("MACHINE");
						this._machineConfigRecord.ThrowIfInitErrors();
						this._isMachineConfigInited = true;
						if (this._isAppConfigHttp)
						{
							ConfigurationManagerHelperFactory.Instance.EnsureNetConfigLoaded();
						}
						this._configHost.RefreshConfigPaths();
						string configPath;
						if (this._configHost.HasLocalConfig)
						{
							configPath = "MACHINE/EXE/ROAMING_USER/LOCAL_USER";
						}
						else if (this._configHost.HasRoamingConfig)
						{
							configPath = "MACHINE/EXE/ROAMING_USER";
						}
						else
						{
							configPath = "MACHINE/EXE";
						}
						this._completeConfigRecord = this._configRoot.GetConfigRecord(configPath);
						this._completeConfigRecord.ThrowIfInitErrors();
						this._isUserConfigInited = true;
					}
					catch (Exception inner)
					{
						this._initError = new ConfigurationErrorsException(SR.GetString("Config_client_config_init_error"), inner);
						throw this._initError;
					}
				}
				catch
				{
					ConfigurationManager.SetInitError(this._initError);
					this._isMachineConfigInited = true;
					this._isUserConfigInited = true;
					throw;
				}
				finally
				{
					lock (this)
					{
						try
						{
							ConfigurationManager.CompleteConfigInit();
							this._isInitInProgress = false;
						}
						finally
						{
							Monitor.PulseAll(this);
						}
					}
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008CD0 File Offset: 0x00006ED0
		private void PrepareClientConfigSystem(string sectionName)
		{
			if (!this._isUserConfigInited)
			{
				this.EnsureInit(sectionName);
			}
			if (this._initError != null)
			{
				throw this._initError;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008CF0 File Offset: 0x00006EF0
		private void OnConfigRemoved(object sender, InternalConfigEventArgs e)
		{
			try
			{
				IInternalConfigRecord configRecord = this._configRoot.GetConfigRecord(this._completeConfigRecord.ConfigPath);
				this._completeConfigRecord = configRecord;
				this._completeConfigRecord.ThrowIfInitErrors();
			}
			catch (Exception inner)
			{
				this._initError = new ConfigurationErrorsException(SR.GetString("Config_client_config_init_error"), inner);
				ConfigurationManager.SetInitError(this._initError);
				throw this._initError;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008D64 File Offset: 0x00006F64
		object IInternalConfigSystem.GetSection(string sectionName)
		{
			this.PrepareClientConfigSystem(sectionName);
			IInternalConfigRecord internalConfigRecord = null;
			if (this.DoesSectionOnlyUseMachineConfig(sectionName))
			{
				if (this._isMachineConfigInited)
				{
					internalConfigRecord = this._machineConfigRecord;
				}
			}
			else if (this._isUserConfigInited)
			{
				internalConfigRecord = this._completeConfigRecord;
			}
			if (internalConfigRecord != null)
			{
				return internalConfigRecord.GetSection(sectionName);
			}
			return null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008DAF File Offset: 0x00006FAF
		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
			this.PrepareClientConfigSystem(sectionName);
			if (this._isMachineConfigInited)
			{
				this._machineConfigRecord.RefreshSection(sectionName);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000874E File Offset: 0x0000694E
		bool IInternalConfigSystem.SupportsUserConfig
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000161 RID: 353
		private const string SystemDiagnosticsConfigKey = "system.diagnostics";

		// Token: 0x04000162 RID: 354
		private const string SystemNetGroupKey = "system.net/";

		// Token: 0x04000163 RID: 355
		private IConfigSystem _configSystem;

		// Token: 0x04000164 RID: 356
		private IInternalConfigRoot _configRoot;

		// Token: 0x04000165 RID: 357
		private ClientConfigurationHost _configHost;

		// Token: 0x04000166 RID: 358
		private IInternalConfigRecord _machineConfigRecord;

		// Token: 0x04000167 RID: 359
		private IInternalConfigRecord _completeConfigRecord;

		// Token: 0x04000168 RID: 360
		private Exception _initError;

		// Token: 0x04000169 RID: 361
		private bool _isInitInProgress;

		// Token: 0x0400016A RID: 362
		private bool _isMachineConfigInited;

		// Token: 0x0400016B RID: 363
		private bool _isUserConfigInited;

		// Token: 0x0400016C RID: 364
		private bool _isAppConfigHttp;
	}
}
