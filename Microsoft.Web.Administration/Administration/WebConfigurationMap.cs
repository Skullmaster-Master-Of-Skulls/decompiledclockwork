using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000077 RID: 119
	public class WebConfigurationMap
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00008FC0 File Offset: 0x00007FC0
		public WebConfigurationMap() : this(null, null)
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00008FCA File Offset: 0x00007FCA
		public WebConfigurationMap(string machineConfigurationPath, string rootWebConfigurationPath)
		{
			this._machineConfigurationPath = machineConfigurationPath;
			this._rootWebConfigurationPath = rootWebConfigurationPath;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00008FE0 File Offset: 0x00007FE0
		public string MachineConfigurationPath
		{
			get
			{
				return this._machineConfigurationPath;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00008FE8 File Offset: 0x00007FE8
		public string RootWebConfigurationPath
		{
			get
			{
				return this._rootWebConfigurationPath;
			}
		}

		// Token: 0x0400012E RID: 302
		private string _machineConfigurationPath;

		// Token: 0x0400012F RID: 303
		private string _rootWebConfigurationPath;
	}
}
