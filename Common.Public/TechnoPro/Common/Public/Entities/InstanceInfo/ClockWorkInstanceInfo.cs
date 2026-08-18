using System;

namespace TechnoPro.Common.Public.Entities.InstanceInfo
{
	// Token: 0x0200032E RID: 814
	[Serializable]
	public sealed class ClockWorkInstanceInfo : BusinessBase<string>
	{
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x0001DF34 File Offset: 0x0001C134
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string InstallationPath
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x0001DF4C File Offset: 0x0001C14C
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0001DF54 File Offset: 0x0001C154
		public string Version { get; set; }
	}
}
