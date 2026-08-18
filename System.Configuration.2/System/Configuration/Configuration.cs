using System;
using System.Collections;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.Versioning;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200001A RID: 26
	public sealed class Configuration
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00008F74 File Offset: 0x00007174
		internal Configuration(string locationSubPath, Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			this._typeConfigHost = typeConfigHost;
			this._hostInitConfigurationParams = hostInitConfigurationParams;
			this._configRoot = new InternalConfigRoot(this);
			IInternalConfigHost internalConfigHost = (IInternalConfigHost)TypeUtil.CreateInstanceWithReflectionPermission(typeConfigHost);
			IInternalConfigHost internalConfigHost2 = new UpdateConfigHost(internalConfigHost);
			((IInternalConfigRoot)this._configRoot).Init(internalConfigHost2, true);
			string configPath;
			string text;
			internalConfigHost.InitForConfiguration(ref locationSubPath, out configPath, out text, this._configRoot, hostInitConfigurationParams);
			if (!string.IsNullOrEmpty(locationSubPath) && !internalConfigHost2.SupportsLocation)
			{
				throw ExceptionUtil.UnexpectedError("Configuration::ctor");
			}
			if (string.IsNullOrEmpty(locationSubPath) != string.IsNullOrEmpty(text))
			{
				throw ExceptionUtil.UnexpectedError("Configuration::ctor");
			}
			this._configRecord = (MgmtConfigurationRecord)this._configRoot.GetConfigRecord(configPath);
			if (!string.IsNullOrEmpty(locationSubPath))
			{
				this._configRecord = MgmtConfigurationRecord.Create(this._configRoot, this._configRecord, text, locationSubPath);
			}
			this._configRecord.ThrowIfInitErrors();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000904B File Offset: 0x0000724B
		internal Configuration OpenLocationConfiguration(string locationSubPath)
		{
			return new Configuration(locationSubPath, this._typeConfigHost, this._hostInitConfigurationParams);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000905F File Offset: 0x0000725F
		public AppSettingsSection AppSettings
		{
			get
			{
				return (AppSettingsSection)this.GetSection("appSettings");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00009071 File Offset: 0x00007271
		public ConnectionStringsSection ConnectionStrings
		{
			get
			{
				return (ConnectionStringsSection)this.GetSection("connectionStrings");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00009083 File Offset: 0x00007283
		public string FilePath
		{
			get
			{
				return this._configRecord.ConfigurationFilePath;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00009090 File Offset: 0x00007290
		public bool HasFile
		{
			get
			{
				return this._configRecord.HasStream;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000909D File Offset: 0x0000729D
		public ConfigurationLocationCollection Locations
		{
			get
			{
				if (this._locations == null)
				{
					this._locations = this._configRecord.GetLocationCollection(this);
				}
				return this._locations;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000090BF File Offset: 0x000072BF
		public ContextInformation EvaluationContext
		{
			get
			{
				if (this._evalContext == null)
				{
					this._evalContext = new ContextInformation(this._configRecord);
				}
				return this._evalContext;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000090E0 File Offset: 0x000072E0
		public ConfigurationSectionGroup RootSectionGroup
		{
			get
			{
				if (this._rootSectionGroup == null)
				{
					this._rootSectionGroup = new ConfigurationSectionGroup();
					this._rootSectionGroup.RootAttachToConfigurationRecord(this._configRecord);
				}
				return this._rootSectionGroup;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000910C File Offset: 0x0000730C
		public ConfigurationSectionCollection Sections
		{
			get
			{
				return this.RootSectionGroup.Sections;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00009119 File Offset: 0x00007319
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				return this.RootSectionGroup.SectionGroups;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009128 File Offset: 0x00007328
		public ConfigurationSection GetSection(string sectionName)
		{
			return (ConfigurationSection)this._configRecord.GetSection(sectionName);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009148 File Offset: 0x00007348
		public ConfigurationSectionGroup GetSectionGroup(string sectionGroupName)
		{
			return this._configRecord.GetSectionGroup(sectionGroupName);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00009163 File Offset: 0x00007363
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00009170 File Offset: 0x00007370
		public bool NamespaceDeclared
		{
			get
			{
				return this._configRecord.NamespacePresent;
			}
			set
			{
				this._configRecord.NamespacePresent = value;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000917E File Offset: 0x0000737E
		public void Save()
		{
			this.SaveAsImpl(null, ConfigurationSaveMode.Modified, false);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009189 File Offset: 0x00007389
		public void Save(ConfigurationSaveMode saveMode)
		{
			this.SaveAsImpl(null, saveMode, false);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009194 File Offset: 0x00007394
		public void Save(ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			this.SaveAsImpl(null, saveMode, forceSaveAll);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000919F File Offset: 0x0000739F
		public void SaveAs(string filename)
		{
			this.SaveAs(filename, ConfigurationSaveMode.Modified, false);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000091AA File Offset: 0x000073AA
		public void SaveAs(string filename, ConfigurationSaveMode saveMode)
		{
			this.SaveAs(filename, saveMode, false);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000091B5 File Offset: 0x000073B5
		public void SaveAs(string filename, ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("filename");
			}
			this.SaveAsImpl(filename, saveMode, forceSaveAll);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000091D3 File Offset: 0x000073D3
		private void SaveAsImpl(string filename, ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			if (string.IsNullOrEmpty(filename))
			{
				filename = null;
			}
			else
			{
				filename = Path.GetFullPath(filename);
			}
			if (forceSaveAll)
			{
				this.ForceGroupsRecursive(this.RootSectionGroup);
			}
			this._configRecord.SaveAs(filename, saveMode, forceSaveAll);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009208 File Offset: 0x00007408
		private void ForceGroupsRecursive(ConfigurationSectionGroup group)
		{
			foreach (object obj in group.Sections)
			{
				ConfigurationSection configurationSection = (ConfigurationSection)obj;
				ConfigurationSection configurationSection2 = group.Sections[configurationSection.SectionInformation.Name];
			}
			foreach (object obj2 in group.SectionGroups)
			{
				ConfigurationSectionGroup configurationSectionGroup = (ConfigurationSectionGroup)obj2;
				this.ForceGroupsRecursive(group.SectionGroups[configurationSectionGroup.Name]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000092D0 File Offset: 0x000074D0
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000092D8 File Offset: 0x000074D8
		public Func<string, string> TypeStringTransformer
		{
			get
			{
				return this._TypeStringTransformer;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				if (this._TypeStringTransformer != value)
				{
					this._TypeStringTransformerIsSet = (value != null);
					this._TypeStringTransformer = value;
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000092F9 File Offset: 0x000074F9
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00009301 File Offset: 0x00007501
		public Func<string, string> AssemblyStringTransformer
		{
			get
			{
				return this._AssemblyStringTransformer;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				if (this._AssemblyStringTransformer != value)
				{
					this._AssemblyStringTransformerIsSet = (value != null);
					this._AssemblyStringTransformer = value;
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00009322 File Offset: 0x00007522
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000932A File Offset: 0x0000752A
		public FrameworkName TargetFramework
		{
			get
			{
				return this._TargetFramework;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				this._TargetFramework = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00009333 File Offset: 0x00007533
		internal bool TypeStringTransformerIsSet
		{
			get
			{
				return this._TypeStringTransformerIsSet;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000933B File Offset: 0x0000753B
		internal bool AssemblyStringTransformerIsSet
		{
			get
			{
				return this._AssemblyStringTransformerIsSet;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00009343 File Offset: 0x00007543
		internal Stack SectionsStack
		{
			get
			{
				if (this._SectionsStack == null)
				{
					this._SectionsStack = new Stack();
				}
				return this._SectionsStack;
			}
		}

		// Token: 0x0400016F RID: 367
		private Type _typeConfigHost;

		// Token: 0x04000170 RID: 368
		private object[] _hostInitConfigurationParams;

		// Token: 0x04000171 RID: 369
		private InternalConfigRoot _configRoot;

		// Token: 0x04000172 RID: 370
		private MgmtConfigurationRecord _configRecord;

		// Token: 0x04000173 RID: 371
		private ConfigurationSectionGroup _rootSectionGroup;

		// Token: 0x04000174 RID: 372
		private ConfigurationLocationCollection _locations;

		// Token: 0x04000175 RID: 373
		private ContextInformation _evalContext;

		// Token: 0x04000176 RID: 374
		private Func<string, string> _TypeStringTransformer;

		// Token: 0x04000177 RID: 375
		private Func<string, string> _AssemblyStringTransformer;

		// Token: 0x04000178 RID: 376
		private bool _TypeStringTransformerIsSet;

		// Token: 0x04000179 RID: 377
		private bool _AssemblyStringTransformerIsSet;

		// Token: 0x0400017A RID: 378
		private FrameworkName _TargetFramework;

		// Token: 0x0400017B RID: 379
		private Stack _SectionsStack;
	}
}
