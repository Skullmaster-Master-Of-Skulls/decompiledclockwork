using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Diagnostics;

namespace System.Configuration
{
	// Token: 0x0200005F RID: 95
	[DebuggerDisplay("FactoryRecord {ConfigKey}")]
	internal class FactoryRecord : IConfigErrorInfo
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x000138FC File Offset: 0x00011AFC
		private FactoryRecord(string configKey, string group, string name, object factory, string factoryTypeName, SimpleBitVector32 flags, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, OverrideModeSetting overrideModeDefault, string filename, int lineNumber, ICollection<ConfigurationException> errors)
		{
			this._configKey = configKey;
			this._group = group;
			this._name = name;
			this._factory = factory;
			this._factoryTypeName = factoryTypeName;
			this._flags = flags;
			this._allowDefinition = allowDefinition;
			this._allowExeDefinition = allowExeDefinition;
			this._overrideModeDefault = overrideModeDefault;
			this._filename = filename;
			this._lineNumber = lineNumber;
			this.AddErrors(errors);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001396C File Offset: 0x00011B6C
		internal FactoryRecord(string configKey, string group, string name, string factoryTypeName, string filename, int lineNumber)
		{
			this._configKey = configKey;
			this._group = group;
			this._name = name;
			this._factoryTypeName = factoryTypeName;
			this.IsGroup = true;
			this._filename = filename;
			this._lineNumber = lineNumber;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000139A8 File Offset: 0x00011BA8
		internal FactoryRecord(string configKey, string group, string name, string factoryTypeName, bool allowLocation, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, OverrideModeSetting overrideModeDefault, bool restartOnExternalChanges, bool requirePermission, bool isFromTrustedConfigRecord, bool isUndeclared, string filename, int lineNumber)
		{
			this._configKey = configKey;
			this._group = group;
			this._name = name;
			this._factoryTypeName = factoryTypeName;
			this._allowDefinition = allowDefinition;
			this._allowExeDefinition = allowExeDefinition;
			this._overrideModeDefault = overrideModeDefault;
			this.AllowLocation = allowLocation;
			this.RestartOnExternalChanges = restartOnExternalChanges;
			this.RequirePermission = requirePermission;
			this.IsFromTrustedConfigRecord = isFromTrustedConfigRecord;
			this.IsUndeclared = isUndeclared;
			this._filename = filename;
			this._lineNumber = lineNumber;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00013A28 File Offset: 0x00011C28
		internal FactoryRecord CloneSection(string filename, int lineNumber)
		{
			return new FactoryRecord(this._configKey, this._group, this._name, this._factory, this._factoryTypeName, this._flags, this._allowDefinition, this._allowExeDefinition, this._overrideModeDefault, filename, lineNumber, this.Errors);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00013A78 File Offset: 0x00011C78
		internal FactoryRecord CloneSectionGroup(string factoryTypeName, string filename, int lineNumber)
		{
			if (this._factoryTypeName != null)
			{
				factoryTypeName = this._factoryTypeName;
			}
			return new FactoryRecord(this._configKey, this._group, this._name, this._factory, factoryTypeName, this._flags, this._allowDefinition, this._allowExeDefinition, this._overrideModeDefault, filename, lineNumber, this.Errors);
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00013AD3 File Offset: 0x00011CD3
		internal string ConfigKey
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00013ADB File Offset: 0x00011CDB
		internal string Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00013AE3 File Offset: 0x00011CE3
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00013AEB File Offset: 0x00011CEB
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00013AF3 File Offset: 0x00011CF3
		internal object Factory
		{
			get
			{
				return this._factory;
			}
			set
			{
				this._factory = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00013AFC File Offset: 0x00011CFC
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00013B04 File Offset: 0x00011D04
		internal string FactoryTypeName
		{
			get
			{
				return this._factoryTypeName;
			}
			set
			{
				this._factoryTypeName = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00013B0D File Offset: 0x00011D0D
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00013B15 File Offset: 0x00011D15
		internal ConfigurationAllowDefinition AllowDefinition
		{
			get
			{
				return this._allowDefinition;
			}
			set
			{
				this._allowDefinition = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00013B1E File Offset: 0x00011D1E
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00013B26 File Offset: 0x00011D26
		internal ConfigurationAllowExeDefinition AllowExeDefinition
		{
			get
			{
				return this._allowExeDefinition;
			}
			set
			{
				this._allowExeDefinition = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00013B2F File Offset: 0x00011D2F
		internal OverrideModeSetting OverrideModeDefault
		{
			get
			{
				return this._overrideModeDefault;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00013B37 File Offset: 0x00011D37
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00013B45 File Offset: 0x00011D45
		internal bool AllowLocation
		{
			get
			{
				return this._flags[1];
			}
			set
			{
				this._flags[1] = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00013B54 File Offset: 0x00011D54
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00013B62 File Offset: 0x00011D62
		internal bool RestartOnExternalChanges
		{
			get
			{
				return this._flags[2];
			}
			set
			{
				this._flags[2] = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00013B71 File Offset: 0x00011D71
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00013B7F File Offset: 0x00011D7F
		internal bool RequirePermission
		{
			get
			{
				return this._flags[4];
			}
			set
			{
				this._flags[4] = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00013B8E File Offset: 0x00011D8E
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00013B9C File Offset: 0x00011D9C
		internal bool IsGroup
		{
			get
			{
				return this._flags[8];
			}
			set
			{
				this._flags[8] = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00013BAB File Offset: 0x00011DAB
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00013BBA File Offset: 0x00011DBA
		internal bool IsFromTrustedConfigRecord
		{
			get
			{
				return this._flags[16];
			}
			set
			{
				this._flags[16] = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00013BCA File Offset: 0x00011DCA
		// (set) Token: 0x060003BE RID: 958 RVA: 0x00013BD9 File Offset: 0x00011DD9
		internal bool IsUndeclared
		{
			get
			{
				return this._flags[64];
			}
			set
			{
				this._flags[64] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00013BE9 File Offset: 0x00011DE9
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00013BF8 File Offset: 0x00011DF8
		internal bool IsFactoryTrustedWithoutAptca
		{
			get
			{
				return this._flags[32];
			}
			set
			{
				this._flags[32] = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00013C08 File Offset: 0x00011E08
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00013C10 File Offset: 0x00011E10
		public string Filename
		{
			get
			{
				return this._filename;
			}
			set
			{
				this._filename = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00013C19 File Offset: 0x00011E19
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00013C21 File Offset: 0x00011E21
		public int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
			set
			{
				this._lineNumber = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00013C2A File Offset: 0x00011E2A
		internal bool HasFile
		{
			get
			{
				return this._lineNumber >= 0;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013C38 File Offset: 0x00011E38
		internal bool IsEquivalentType(IInternalConfigHost host, string typeName)
		{
			try
			{
				if (this._factoryTypeName == typeName)
				{
					return true;
				}
				Type typeWithReflectionPermission;
				Type typeWithReflectionPermission2;
				if (host != null)
				{
					typeWithReflectionPermission = TypeUtil.GetTypeWithReflectionPermission(host, typeName, false);
					typeWithReflectionPermission2 = TypeUtil.GetTypeWithReflectionPermission(host, this._factoryTypeName, false);
				}
				else
				{
					typeWithReflectionPermission = TypeUtil.GetTypeWithReflectionPermission(typeName, false);
					typeWithReflectionPermission2 = TypeUtil.GetTypeWithReflectionPermission(this._factoryTypeName, false);
				}
				return typeWithReflectionPermission != null && typeWithReflectionPermission == typeWithReflectionPermission2;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013CB4 File Offset: 0x00011EB4
		internal bool IsEquivalentSectionGroupFactory(IInternalConfigHost host, string typeName)
		{
			return typeName == null || this._factoryTypeName == null || this.IsEquivalentType(host, typeName);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00013CCB File Offset: 0x00011ECB
		internal bool IsEquivalentSectionFactory(IInternalConfigHost host, string typeName, bool allowLocation, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, bool restartOnExternalChanges, bool requirePermission)
		{
			return allowLocation == this.AllowLocation && allowDefinition == this.AllowDefinition && allowExeDefinition == this.AllowExeDefinition && restartOnExternalChanges == this.RestartOnExternalChanges && requirePermission == this.RequirePermission && this.IsEquivalentType(host, typeName);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00013D08 File Offset: 0x00011F08
		internal List<ConfigurationException> Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00013D10 File Offset: 0x00011F10
		internal bool HasErrors
		{
			get
			{
				return ErrorsHelper.GetHasErrors(this._errors);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00013D1D File Offset: 0x00011F1D
		internal void AddErrors(ICollection<ConfigurationException> coll)
		{
			ErrorsHelper.AddErrors(ref this._errors, coll);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00013D2B File Offset: 0x00011F2B
		internal void ThrowOnErrors()
		{
			ErrorsHelper.ThrowOnErrors(this._errors);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00013D38 File Offset: 0x00011F38
		internal bool IsIgnorable()
		{
			if (this._factory != null)
			{
				return this._factory is IgnoreSectionHandler;
			}
			return this._factoryTypeName != null && this._factoryTypeName.Contains("System.Configuration.IgnoreSection");
		}

		// Token: 0x0400026C RID: 620
		private const int Flag_AllowLocation = 1;

		// Token: 0x0400026D RID: 621
		private const int Flag_RestartOnExternalChanges = 2;

		// Token: 0x0400026E RID: 622
		private const int Flag_RequirePermission = 4;

		// Token: 0x0400026F RID: 623
		private const int Flag_IsGroup = 8;

		// Token: 0x04000270 RID: 624
		private const int Flag_IsFromTrustedConfigRecord = 16;

		// Token: 0x04000271 RID: 625
		private const int Flag_IsFactoryTrustedWithoutAptca = 32;

		// Token: 0x04000272 RID: 626
		private const int Flag_IsUndeclared = 64;

		// Token: 0x04000273 RID: 627
		private string _configKey;

		// Token: 0x04000274 RID: 628
		private string _group;

		// Token: 0x04000275 RID: 629
		private string _name;

		// Token: 0x04000276 RID: 630
		private SimpleBitVector32 _flags;

		// Token: 0x04000277 RID: 631
		private string _factoryTypeName;

		// Token: 0x04000278 RID: 632
		private ConfigurationAllowDefinition _allowDefinition;

		// Token: 0x04000279 RID: 633
		private ConfigurationAllowExeDefinition _allowExeDefinition;

		// Token: 0x0400027A RID: 634
		private OverrideModeSetting _overrideModeDefault;

		// Token: 0x0400027B RID: 635
		private string _filename;

		// Token: 0x0400027C RID: 636
		private int _lineNumber;

		// Token: 0x0400027D RID: 637
		private object _factory;

		// Token: 0x0400027E RID: 638
		private List<ConfigurationException> _errors;
	}
}
