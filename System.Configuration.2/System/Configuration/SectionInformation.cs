using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000087 RID: 135
	public sealed class SectionInformation
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0001A6C4 File Offset: 0x000188C4
		internal SectionInformation(ConfigurationSection associatedConfigurationSection)
		{
			this._configKey = string.Empty;
			this._group = string.Empty;
			this._name = string.Empty;
			this._configurationSection = associatedConfigurationSection;
			this._allowDefinition = ConfigurationAllowDefinition.Everywhere;
			this._allowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;
			this._overrideModeDefault = OverrideModeSetting.SectionDefault;
			this._overrideMode = OverrideModeSetting.LocationDefault;
			this._flags[8] = true;
			this._flags[16] = true;
			this._flags[32] = true;
			this._flags[256] = true;
			this._flags[4096] = false;
			this._modifiedFlags = default(SimpleBitVector32);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001A77F File Offset: 0x0001897F
		internal void ResetModifiedFlags()
		{
			this._modifiedFlags = default(SimpleBitVector32);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001A78D File Offset: 0x0001898D
		internal bool IsModifiedFlags()
		{
			return this._modifiedFlags.Data != 0;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001A79D File Offset: 0x0001899D
		internal void AttachToConfigurationRecord(MgmtConfigurationRecord configRecord, FactoryRecord factoryRecord, SectionRecord sectionRecord)
		{
			this.SetRuntimeConfigurationInformation(configRecord, factoryRecord, sectionRecord);
			this._configRecord = configRecord;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001A7B0 File Offset: 0x000189B0
		internal void SetRuntimeConfigurationInformation(BaseConfigurationRecord configRecord, FactoryRecord factoryRecord, SectionRecord sectionRecord)
		{
			this._flags[1] = true;
			this._configKey = factoryRecord.ConfigKey;
			this._group = factoryRecord.Group;
			this._name = factoryRecord.Name;
			this._typeName = factoryRecord.FactoryTypeName;
			this._allowDefinition = factoryRecord.AllowDefinition;
			this._allowExeDefinition = factoryRecord.AllowExeDefinition;
			this._flags[8] = factoryRecord.AllowLocation;
			this._flags[16] = factoryRecord.RestartOnExternalChanges;
			this._flags[32] = factoryRecord.RequirePermission;
			this._overrideModeDefault = factoryRecord.OverrideModeDefault;
			if (factoryRecord.IsUndeclared)
			{
				this._flags[8192] = true;
				this._flags[2] = false;
				this._flags[4] = false;
			}
			else
			{
				this._flags[8192] = false;
				this._flags[2] = (configRecord.GetFactoryRecord(factoryRecord.ConfigKey, false) != null);
				this._flags[4] = configRecord.IsRootDeclaration(factoryRecord.ConfigKey, false);
			}
			this._flags[64] = sectionRecord.Locked;
			this._flags[128] = sectionRecord.LockChildren;
			this._flags[16384] = sectionRecord.LockChildrenWithoutFileInput;
			if (sectionRecord.HasFileInput)
			{
				SectionInput fileInput = sectionRecord.FileInput;
				this._flags[4194304] = fileInput.IsConfigBuilderDetermined;
				this._configBuilder = fileInput.ConfigBuilder;
				this._flags[2048] = fileInput.IsProtectionProviderDetermined;
				this._protectionProvider = fileInput.ProtectionProvider;
				SectionXmlInfo sectionXmlInfo = fileInput.SectionXmlInfo;
				this._configSource = sectionXmlInfo.ConfigSource;
				this._configSourceStreamName = sectionXmlInfo.ConfigSourceStreamName;
				this._overrideMode = sectionXmlInfo.OverrideModeSetting;
				this._flags[256] = !sectionXmlInfo.SkipInChildApps;
				this._configBuilderName = sectionXmlInfo.ConfigBuilderName;
				this._protectionProviderName = sectionXmlInfo.ProtectionProviderName;
			}
			else
			{
				this._flags[4194304] = false;
				this._configBuilder = null;
				this._flags[2048] = false;
				this._protectionProvider = null;
			}
			this._configurationSection.AssociateContext(configRecord);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001AA00 File Offset: 0x00018C00
		internal void DetachFromConfigurationRecord()
		{
			this.RevertToParent();
			this._flags[1] = false;
			this._configRecord = null;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001AA1C File Offset: 0x00018C1C
		private bool IsRuntime
		{
			get
			{
				return this._flags[1] && this._configRecord == null;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001AA37 File Offset: 0x00018C37
		internal bool Attached
		{
			get
			{
				return this._flags[1];
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001AA45 File Offset: 0x00018C45
		private void VerifyDesigntime()
		{
			if (this.IsRuntime)
			{
				throw new InvalidOperationException(SR.GetString("Config_operation_not_runtime"));
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001AA5F File Offset: 0x00018C5F
		private void VerifyIsAttachedToConfigRecord()
		{
			if (this._configRecord == null)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_when_not_attached"));
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001AA7C File Offset: 0x00018C7C
		internal void VerifyIsEditable()
		{
			this.VerifyDesigntime();
			if (this.IsLocked)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_when_locked"));
			}
			if (this._flags[512])
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_parentsection"));
			}
			if (!this._flags[8] && this._configRecord != null && this._configRecord.IsLocationConfig)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_when_location_locked"));
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001AAFC File Offset: 0x00018CFC
		private void VerifyNotParentSection()
		{
			if (this._flags[512])
			{
				throw new InvalidOperationException(SR.GetString("Config_configsection_parentnotvalid"));
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001AB20 File Offset: 0x00018D20
		private void VerifySupportsLocation()
		{
			if (this._configRecord != null && !this._configRecord.RecordSupportsLocation)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_locationattriubtes"));
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001AB48 File Offset: 0x00018D48
		internal void VerifyIsEditableFactory()
		{
			if (this._configRecord != null && this._configRecord.IsLocationConfig)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_in_location_config"));
			}
			if (BaseConfigurationRecord.IsImplicitSection(this.ConfigKey))
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_when_it_is_implicit"));
			}
			if (this._flags[8192])
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsection_when_it_is_undeclared"));
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001ABB9 File Offset: 0x00018DB9
		internal string ConfigKey
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001ABC1 File Offset: 0x00018DC1
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001ABD3 File Offset: 0x00018DD3
		internal bool Removed
		{
			get
			{
				return this._flags[1024];
			}
			set
			{
				this._flags[1024] = value;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		private FactoryRecord FindParentFactoryRecord(bool permitErrors)
		{
			FactoryRecord result = null;
			if (this._configRecord != null && !this._configRecord.Parent.IsRootConfig)
			{
				result = this._configRecord.Parent.FindFactoryRecord(this._configKey, permitErrors);
			}
			return result;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001ABB9 File Offset: 0x00018DB9
		public string SectionName
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001AC2A File Offset: 0x00018E2A
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001AC32 File Offset: 0x00018E32
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public ConfigurationAllowDefinition AllowDefinition
		{
			get
			{
				return this._allowDefinition;
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null && factoryRecord.AllowDefinition != value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
					{
						this._configKey
					}));
				}
				this._allowDefinition = value;
				this._modifiedFlags[131072] = true;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		internal bool AllowDefinitionModified
		{
			get
			{
				return this._modifiedFlags[131072];
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001ACB2 File Offset: 0x00018EB2
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001ACBC File Offset: 0x00018EBC
		public ConfigurationAllowExeDefinition AllowExeDefinition
		{
			get
			{
				return this._allowExeDefinition;
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null && factoryRecord.AllowExeDefinition != value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
					{
						this._configKey
					}));
				}
				this._allowExeDefinition = value;
				this._modifiedFlags[65536] = true;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001AD20 File Offset: 0x00018F20
		internal bool AllowExeDefinitionModified
		{
			get
			{
				return this._modifiedFlags[65536];
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001AD32 File Offset: 0x00018F32
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001AD40 File Offset: 0x00018F40
		public OverrideMode OverrideModeDefault
		{
			get
			{
				return this._overrideModeDefault.OverrideMode;
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null && factoryRecord.OverrideModeDefault.OverrideMode != value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
					{
						this._configKey
					}));
				}
				if (value == OverrideMode.Inherit)
				{
					value = OverrideMode.Allow;
				}
				this._overrideModeDefault.OverrideMode = value;
				this._modifiedFlags[1048576] = true;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001ADB7 File Offset: 0x00018FB7
		internal OverrideModeSetting OverrideModeDefaultSetting
		{
			get
			{
				return this._overrideModeDefault;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001ADBF File Offset: 0x00018FBF
		internal bool OverrideModeDefaultModified
		{
			get
			{
				return this._modifiedFlags[1048576];
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001ADD1 File Offset: 0x00018FD1
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		public bool AllowLocation
		{
			get
			{
				return this._flags[8];
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null && factoryRecord.AllowLocation != value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
					{
						this._configKey
					}));
				}
				this._flags[8] = value;
				this._modifiedFlags[8] = true;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001AE46 File Offset: 0x00019046
		internal bool AllowLocationModified
		{
			get
			{
				return this._modifiedFlags[8];
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001AE54 File Offset: 0x00019054
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001AE61 File Offset: 0x00019061
		public bool AllowOverride
		{
			get
			{
				return this._overrideMode.AllowOverride;
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifySupportsLocation();
				this._overrideMode.AllowOverride = value;
				this._modifiedFlags[2097152] = true;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001AE8C File Offset: 0x0001908C
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001AE9C File Offset: 0x0001909C
		public OverrideMode OverrideMode
		{
			get
			{
				return this._overrideMode.OverrideMode;
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifySupportsLocation();
				this._overrideMode.OverrideMode = value;
				this._modifiedFlags[2097152] = true;
				switch (value)
				{
				case OverrideMode.Inherit:
					this._flags[128] = this._flags[16384];
					return;
				case OverrideMode.Allow:
					this._flags[128] = false;
					return;
				case OverrideMode.Deny:
					this._flags[128] = true;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001AF29 File Offset: 0x00019129
		public OverrideMode OverrideModeEffective
		{
			get
			{
				if (!this._flags[128])
				{
					return OverrideMode.Allow;
				}
				return OverrideMode.Deny;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001AF40 File Offset: 0x00019140
		internal OverrideModeSetting OverrideModeSetting
		{
			get
			{
				return this._overrideMode;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001AF48 File Offset: 0x00019148
		internal bool LocationAttributesAreDefault
		{
			get
			{
				return this._overrideMode.IsDefaultForLocationTag && this._flags[256];
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001AF69 File Offset: 0x00019169
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001AF80 File Offset: 0x00019180
		public string ConfigSource
		{
			get
			{
				if (this._configSource != null)
				{
					return this._configSource;
				}
				return string.Empty;
			}
			set
			{
				this.VerifyIsEditable();
				string text;
				if (!string.IsNullOrEmpty(value))
				{
					text = BaseConfigurationRecord.NormalizeConfigSource(value, null);
				}
				else
				{
					text = null;
				}
				if (text == this._configSource)
				{
					return;
				}
				if (this._configRecord != null)
				{
					this._configRecord.ChangeConfigSource(this, this._configSource, this._configSourceStreamName, text);
				}
				this._configSource = text;
				this._modifiedFlags[262144] = true;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001AFEF File Offset: 0x000191EF
		internal bool ConfigSourceModified
		{
			get
			{
				return this._modifiedFlags[262144];
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001B001 File Offset: 0x00019201
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0001B009 File Offset: 0x00019209
		internal string ConfigSourceStreamName
		{
			get
			{
				return this._configSourceStreamName;
			}
			set
			{
				this._configSourceStreamName = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001B012 File Offset: 0x00019212
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0001B024 File Offset: 0x00019224
		public bool InheritInChildApplications
		{
			get
			{
				return this._flags[256];
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifySupportsLocation();
				this._flags[256] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001B043 File Offset: 0x00019243
		public bool IsDeclared
		{
			get
			{
				this.VerifyNotParentSection();
				return this._flags[2];
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001B057 File Offset: 0x00019257
		public bool IsDeclarationRequired
		{
			get
			{
				this.VerifyNotParentSection();
				return this._flags[4];
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001B06B File Offset: 0x0001926B
		public void ForceDeclaration()
		{
			this.ForceDeclaration(true);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001B074 File Offset: 0x00019274
		public void ForceDeclaration(bool force)
		{
			this.VerifyIsEditable();
			if (force || !this._flags[4])
			{
				if (force && BaseConfigurationRecord.IsImplicitSection(this.SectionName))
				{
					throw new ConfigurationErrorsException(SR.GetString("Cannot_declare_or_remove_implicit_section"));
				}
				if (force && this._flags[8192])
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_cannot_edit_configurationsection_when_it_is_undeclared"));
				}
				this._flags[2] = force;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001B0EA File Offset: 0x000192EA
		private bool IsDefinitionAllowed
		{
			get
			{
				return this._configRecord == null || this._configRecord.IsDefinitionAllowed(this._allowDefinition, this._allowExeDefinition);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001B10D File Offset: 0x0001930D
		public bool IsLocked
		{
			get
			{
				return this._flags[64] || !this.IsDefinitionAllowed || this._configurationSection.ElementInformation.IsLocked;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001B138 File Offset: 0x00019338
		public bool IsProtected
		{
			get
			{
				return this.ProtectionProvider != null;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001B143 File Offset: 0x00019343
		internal string ConfigBuilderName
		{
			get
			{
				return this._configBuilderName;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001B14C File Offset: 0x0001934C
		public ConfigurationBuilder ConfigurationBuilder
		{
			get
			{
				if (!this._flags[4194304] && this._configRecord != null)
				{
					this._configBuilder = this._configRecord.GetConfigBuilderFromName(this._configBuilderName);
					this._flags[4194304] = true;
				}
				return this._configBuilder;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001B1A4 File Offset: 0x000193A4
		public ProtectedConfigurationProvider ProtectionProvider
		{
			get
			{
				if (!this._flags[2048] && this._configRecord != null)
				{
					this._protectionProvider = this._configRecord.GetProtectionProviderFromName(this._protectionProviderName, false);
					this._flags[2048] = true;
				}
				return this._protectionProvider;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001B1FC File Offset: 0x000193FC
		public void ProtectSection(string protectionProvider)
		{
			this.VerifyIsEditable();
			if (!this.AllowLocation || this._configKey == "configProtectedData")
			{
				throw new InvalidOperationException(SR.GetString("Config_not_allowed_to_encrypt_this_section"));
			}
			if (this._configRecord != null)
			{
				if (string.IsNullOrEmpty(protectionProvider))
				{
					protectionProvider = this._configRecord.DefaultProviderName;
				}
				ProtectedConfigurationProvider protectionProviderFromName = this._configRecord.GetProtectionProviderFromName(protectionProvider, true);
				this._protectionProviderName = protectionProvider;
				this._protectionProvider = protectionProviderFromName;
				this._flags[2048] = true;
				this._modifiedFlags[524288] = true;
				return;
			}
			throw new InvalidOperationException(SR.GetString("Must_add_to_config_before_protecting_it"));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001B2A8 File Offset: 0x000194A8
		public void UnprotectSection()
		{
			this.VerifyIsEditable();
			this._protectionProvider = null;
			this._protectionProviderName = null;
			this._flags[2048] = true;
			this._modifiedFlags[524288] = true;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001B2E0 File Offset: 0x000194E0
		internal string ProtectionProviderName
		{
			get
			{
				return this._protectionProviderName;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001B2E8 File Offset: 0x000194E8
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001B2F8 File Offset: 0x000194F8
		public bool RestartOnExternalChanges
		{
			get
			{
				return this._flags[16];
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null && factoryRecord.RestartOnExternalChanges != value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
					{
						this._configKey
					}));
				}
				this._flags[16] = value;
				this._modifiedFlags[16] = true;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001B360 File Offset: 0x00019560
		internal bool RestartOnExternalChangesModified
		{
			get
			{
				return this._modifiedFlags[16];
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001B36F File Offset: 0x0001956F
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001B380 File Offset: 0x00019580
		public bool RequirePermission
		{
			get
			{
				return this._flags[32];
			}
			set
			{
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null && factoryRecord.RequirePermission != value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
					{
						this._configKey
					}));
				}
				this._flags[32] = value;
				this._modifiedFlags[32] = true;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001B3E8 File Offset: 0x000195E8
		internal bool RequirePermissionModified
		{
			get
			{
				return this._modifiedFlags[32];
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001B3F7 File Offset: 0x000195F7
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001B400 File Offset: 0x00019600
		public string Type
		{
			get
			{
				return this._typeName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw ExceptionUtil.PropertyNullOrEmpty("Type");
				}
				this.VerifyIsEditable();
				this.VerifyIsEditableFactory();
				FactoryRecord factoryRecord = this.FindParentFactoryRecord(false);
				if (factoryRecord != null)
				{
					IInternalConfigHost host = null;
					if (this._configRecord != null)
					{
						host = this._configRecord.Host;
					}
					if (!factoryRecord.IsEquivalentType(host, value))
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_tag_name_already_defined", new object[]
						{
							this._configKey
						}));
					}
				}
				this._typeName = value;
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001B480 File Offset: 0x00019680
		public ConfigurationSection GetParentSection()
		{
			this.VerifyDesigntime();
			if (this._flags[512])
			{
				throw new InvalidOperationException(SR.GetString("Config_getparentconfigurationsection_first_instance"));
			}
			ConfigurationSection configurationSection = null;
			if (this._configRecord != null)
			{
				configurationSection = this._configRecord.FindAndCloneImmediateParentSection(this._configurationSection);
				if (configurationSection != null)
				{
					configurationSection.SectionInformation._flags[512] = true;
					configurationSection.SetReadOnly();
				}
			}
			return configurationSection;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001B4F1 File Offset: 0x000196F1
		public string GetRawXml()
		{
			this.VerifyDesigntime();
			this.VerifyNotParentSection();
			if (this.RawXml != null)
			{
				return this.RawXml;
			}
			if (this._configRecord != null)
			{
				return this._configRecord.GetRawXml(this._configKey);
			}
			return null;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001B529 File Offset: 0x00019729
		public void SetRawXml(string rawXml)
		{
			this.VerifyIsEditable();
			if (this._configRecord != null)
			{
				this._configRecord.SetRawXml(this._configurationSection, rawXml);
				return;
			}
			this.RawXml = (string.IsNullOrEmpty(rawXml) ? null : rawXml);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001B55E File Offset: 0x0001975E
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0001B566 File Offset: 0x00019766
		internal string RawXml
		{
			get
			{
				return this._rawXml;
			}
			set
			{
				this._rawXml = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001B56F File Offset: 0x0001976F
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001B581 File Offset: 0x00019781
		public bool ForceSave
		{
			get
			{
				return this._flags[4096];
			}
			set
			{
				this.VerifyIsEditable();
				this._flags[4096] = value;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001B59A File Offset: 0x0001979A
		public void RevertToParent()
		{
			this.VerifyIsEditable();
			this.VerifyIsAttachedToConfigRecord();
			this._configRecord.RevertToParent(this._configurationSection);
		}

		// Token: 0x040002EA RID: 746
		private const int Flag_Attached = 1;

		// Token: 0x040002EB RID: 747
		private const int Flag_Declared = 2;

		// Token: 0x040002EC RID: 748
		private const int Flag_DeclarationRequired = 4;

		// Token: 0x040002ED RID: 749
		private const int Flag_AllowLocation = 8;

		// Token: 0x040002EE RID: 750
		private const int Flag_RestartOnExternalChanges = 16;

		// Token: 0x040002EF RID: 751
		private const int Flag_RequirePermission = 32;

		// Token: 0x040002F0 RID: 752
		private const int Flag_LocationLocked = 64;

		// Token: 0x040002F1 RID: 753
		private const int Flag_ChildrenLocked = 128;

		// Token: 0x040002F2 RID: 754
		private const int Flag_InheritInChildApps = 256;

		// Token: 0x040002F3 RID: 755
		private const int Flag_IsParentSection = 512;

		// Token: 0x040002F4 RID: 756
		private const int Flag_Removed = 1024;

		// Token: 0x040002F5 RID: 757
		private const int Flag_ProtectionProviderDetermined = 2048;

		// Token: 0x040002F6 RID: 758
		private const int Flag_ForceSave = 4096;

		// Token: 0x040002F7 RID: 759
		private const int Flag_IsUndeclared = 8192;

		// Token: 0x040002F8 RID: 760
		private const int Flag_ChildrenLockWithoutFileInput = 16384;

		// Token: 0x040002F9 RID: 761
		private const int Flag_AllowExeDefinitionModified = 65536;

		// Token: 0x040002FA RID: 762
		private const int Flag_AllowDefinitionModified = 131072;

		// Token: 0x040002FB RID: 763
		private const int Flag_ConfigSourceModified = 262144;

		// Token: 0x040002FC RID: 764
		private const int Flag_ProtectionProviderModified = 524288;

		// Token: 0x040002FD RID: 765
		private const int Flag_OverrideModeDefaultModified = 1048576;

		// Token: 0x040002FE RID: 766
		private const int Flag_OverrideModeModified = 2097152;

		// Token: 0x040002FF RID: 767
		private const int Flag_ConfigBuilderDetermined = 4194304;

		// Token: 0x04000300 RID: 768
		private const int Flag_ConfigBuilderModified = 8388608;

		// Token: 0x04000301 RID: 769
		private ConfigurationSection _configurationSection;

		// Token: 0x04000302 RID: 770
		private SafeBitVector32 _flags;

		// Token: 0x04000303 RID: 771
		private SimpleBitVector32 _modifiedFlags;

		// Token: 0x04000304 RID: 772
		private ConfigurationAllowDefinition _allowDefinition;

		// Token: 0x04000305 RID: 773
		private ConfigurationAllowExeDefinition _allowExeDefinition;

		// Token: 0x04000306 RID: 774
		private MgmtConfigurationRecord _configRecord;

		// Token: 0x04000307 RID: 775
		private string _configKey;

		// Token: 0x04000308 RID: 776
		private string _group;

		// Token: 0x04000309 RID: 777
		private string _name;

		// Token: 0x0400030A RID: 778
		private string _typeName;

		// Token: 0x0400030B RID: 779
		private string _rawXml;

		// Token: 0x0400030C RID: 780
		private string _configSource;

		// Token: 0x0400030D RID: 781
		private string _configSourceStreamName;

		// Token: 0x0400030E RID: 782
		private ProtectedConfigurationProvider _protectionProvider;

		// Token: 0x0400030F RID: 783
		private string _protectionProviderName;

		// Token: 0x04000310 RID: 784
		private ConfigurationBuilder _configBuilder;

		// Token: 0x04000311 RID: 785
		private string _configBuilderName;

		// Token: 0x04000312 RID: 786
		private OverrideModeSetting _overrideModeDefault;

		// Token: 0x04000313 RID: 787
		private OverrideModeSetting _overrideMode;
	}
}
