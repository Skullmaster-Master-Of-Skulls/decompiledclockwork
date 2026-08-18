using System;

namespace System.Configuration
{
	// Token: 0x0200005C RID: 92
	public sealed class ExeConfigurationFileMap : ConfigurationFileMap
	{
		// Token: 0x06000391 RID: 913 RVA: 0x000137DA File Offset: 0x000119DA
		public ExeConfigurationFileMap()
		{
			this._exeConfigFilename = string.Empty;
			this._roamingUserConfigFilename = string.Empty;
			this._localUserConfigFilename = string.Empty;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00013803 File Offset: 0x00011A03
		public ExeConfigurationFileMap(string machineConfigFileName) : base(machineConfigFileName)
		{
			this._exeConfigFilename = string.Empty;
			this._roamingUserConfigFilename = string.Empty;
			this._localUserConfigFilename = string.Empty;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001382D File Offset: 0x00011A2D
		private ExeConfigurationFileMap(string machineConfigFileName, string exeConfigFilename, string roamingUserConfigFilename, string localUserConfigFilename) : base(machineConfigFileName)
		{
			this._exeConfigFilename = exeConfigFilename;
			this._roamingUserConfigFilename = roamingUserConfigFilename;
			this._localUserConfigFilename = localUserConfigFilename;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001384C File Offset: 0x00011A4C
		public override object Clone()
		{
			return new ExeConfigurationFileMap(base.MachineConfigFilename, this._exeConfigFilename, this._roamingUserConfigFilename, this._localUserConfigFilename);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001386B File Offset: 0x00011A6B
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00013873 File Offset: 0x00011A73
		public string ExeConfigFilename
		{
			get
			{
				return this._exeConfigFilename;
			}
			set
			{
				this._exeConfigFilename = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001387C File Offset: 0x00011A7C
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00013884 File Offset: 0x00011A84
		public string RoamingUserConfigFilename
		{
			get
			{
				return this._roamingUserConfigFilename;
			}
			set
			{
				this._roamingUserConfigFilename = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001388D File Offset: 0x00011A8D
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00013895 File Offset: 0x00011A95
		public string LocalUserConfigFilename
		{
			get
			{
				return this._localUserConfigFilename;
			}
			set
			{
				this._localUserConfigFilename = value;
			}
		}

		// Token: 0x04000264 RID: 612
		private string _exeConfigFilename;

		// Token: 0x04000265 RID: 613
		private string _roamingUserConfigFilename;

		// Token: 0x04000266 RID: 614
		private string _localUserConfigFilename;
	}
}
