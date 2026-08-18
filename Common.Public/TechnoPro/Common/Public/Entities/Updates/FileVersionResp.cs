using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200014E RID: 334
	public class FileVersionResp : BusinessBase<string>
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00011590 File Offset: 0x0000F790
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string FileVersion
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

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000115A8 File Offset: 0x0000F7A8
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public string SecondaryFileVersion { get; set; }
	}
}
