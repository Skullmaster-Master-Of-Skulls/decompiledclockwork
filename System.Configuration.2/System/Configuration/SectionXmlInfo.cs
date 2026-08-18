using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200008B RID: 139
	internal sealed class SectionXmlInfo : IConfigErrorInfo
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0001C3BC File Offset: 0x0001A5BC
		internal SectionXmlInfo(string configKey, string definitionConfigPath, string targetConfigPath, string subPath, string filename, int lineNumber, object streamVersion, string rawXml, string configSource, string configSourceStreamName, object configSourceStreamVersion, string configBuilderName, string protectionProviderName, OverrideModeSetting overrideMode, bool skipInChildApps)
		{
			this._configKey = configKey;
			this._definitionConfigPath = definitionConfigPath;
			this._targetConfigPath = targetConfigPath;
			this._subPath = subPath;
			this._filename = filename;
			this._lineNumber = lineNumber;
			this._streamVersion = streamVersion;
			this._rawXml = rawXml;
			this._configSource = configSource;
			this._configSourceStreamName = configSourceStreamName;
			this._configSourceStreamVersion = configSourceStreamVersion;
			this._configBuilderName = configBuilderName;
			this._protectionProviderName = protectionProviderName;
			this._overrideMode = overrideMode;
			this._skipInChildApps = skipInChildApps;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001C444 File Offset: 0x0001A644
		public string Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001C44C File Offset: 0x0001A64C
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0001C454 File Offset: 0x0001A654
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001C45D File Offset: 0x0001A65D
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001C465 File Offset: 0x0001A665
		internal object StreamVersion
		{
			get
			{
				return this._streamVersion;
			}
			set
			{
				this._streamVersion = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001C46E File Offset: 0x0001A66E
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0001C476 File Offset: 0x0001A676
		internal string ConfigSource
		{
			get
			{
				return this._configSource;
			}
			set
			{
				this._configSource = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001C47F File Offset: 0x0001A67F
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001C487 File Offset: 0x0001A687
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

		// Token: 0x170001CD RID: 461
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001C490 File Offset: 0x0001A690
		internal object ConfigSourceStreamVersion
		{
			set
			{
				this._configSourceStreamVersion = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001C499 File Offset: 0x0001A699
		internal string ConfigKey
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001C4A1 File Offset: 0x0001A6A1
		internal string DefinitionConfigPath
		{
			get
			{
				return this._definitionConfigPath;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001C4A9 File Offset: 0x0001A6A9
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001C4B1 File Offset: 0x0001A6B1
		internal string TargetConfigPath
		{
			get
			{
				return this._targetConfigPath;
			}
			set
			{
				this._targetConfigPath = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001C4BA File Offset: 0x0001A6BA
		internal string SubPath
		{
			get
			{
				return this._subPath;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001C4C2 File Offset: 0x0001A6C2
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0001C4CA File Offset: 0x0001A6CA
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001C4D3 File Offset: 0x0001A6D3
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001C4DB File Offset: 0x0001A6DB
		internal string ConfigBuilderName
		{
			get
			{
				return this._configBuilderName;
			}
			set
			{
				this._configBuilderName = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		internal string ProtectionProviderName
		{
			get
			{
				return this._protectionProviderName;
			}
			set
			{
				this._protectionProviderName = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001C4F5 File Offset: 0x0001A6F5
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0001C4FD File Offset: 0x0001A6FD
		internal OverrideModeSetting OverrideModeSetting
		{
			get
			{
				return this._overrideMode;
			}
			set
			{
				this._overrideMode = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001C506 File Offset: 0x0001A706
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0001C50E File Offset: 0x0001A70E
		internal bool SkipInChildApps
		{
			get
			{
				return this._skipInChildApps;
			}
			set
			{
				this._skipInChildApps = value;
			}
		}

		// Token: 0x04000334 RID: 820
		private string _configKey;

		// Token: 0x04000335 RID: 821
		private string _definitionConfigPath;

		// Token: 0x04000336 RID: 822
		private string _targetConfigPath;

		// Token: 0x04000337 RID: 823
		private string _subPath;

		// Token: 0x04000338 RID: 824
		private string _filename;

		// Token: 0x04000339 RID: 825
		private int _lineNumber;

		// Token: 0x0400033A RID: 826
		private object _streamVersion;

		// Token: 0x0400033B RID: 827
		private string _configSource;

		// Token: 0x0400033C RID: 828
		private string _configSourceStreamName;

		// Token: 0x0400033D RID: 829
		private object _configSourceStreamVersion;

		// Token: 0x0400033E RID: 830
		private bool _skipInChildApps;

		// Token: 0x0400033F RID: 831
		private string _rawXml;

		// Token: 0x04000340 RID: 832
		private string _configBuilderName;

		// Token: 0x04000341 RID: 833
		private string _protectionProviderName;

		// Token: 0x04000342 RID: 834
		private OverrideModeSetting _overrideMode;
	}
}
