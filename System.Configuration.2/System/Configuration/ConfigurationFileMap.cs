using System;
using System.IO;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000029 RID: 41
	public class ConfigurationFileMap : ICloneable
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public ConfigurationFileMap()
		{
			this._getFilenameThunk = new Func<string>(ConfigurationFileMap.GetFilenameFromMachineConfigFilePath);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000F928 File Offset: 0x0000DB28
		public ConfigurationFileMap(string machineConfigFilename)
		{
			if (string.IsNullOrEmpty(machineConfigFilename))
			{
				throw new ArgumentNullException("machineConfigFilename");
			}
			if (!File.Exists(machineConfigFilename))
			{
				throw new ArgumentException(SR.GetString("Machine_config_file_not_found", new object[]
				{
					machineConfigFilename
				}), "machineConfigFilename");
			}
			this.MachineConfigFilename = machineConfigFilename;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000F97C File Offset: 0x0000DB7C
		private ConfigurationFileMap(ConfigurationFileMap other)
		{
			this._getFilenameThunk = other._getFilenameThunk;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000F990 File Offset: 0x0000DB90
		public virtual object Clone()
		{
			return new ConfigurationFileMap(this);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000F998 File Offset: 0x0000DB98
		private static string GetFilenameFromMachineConfigFilePath()
		{
			string machineConfigFilePath = ClientConfigurationHost.MachineConfigFilePath;
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, machineConfigFilePath).Demand();
			return machineConfigFilePath;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public string MachineConfigFilename
		{
			get
			{
				return this._getFilenameThunk();
			}
			set
			{
				this._getFilenameThunk = (() => value);
			}
		}

		// Token: 0x040001CC RID: 460
		private Func<string> _getFilenameThunk;
	}
}
