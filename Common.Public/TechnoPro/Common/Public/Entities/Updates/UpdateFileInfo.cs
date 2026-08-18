using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200014D RID: 333
	public class UpdateFileInfo : BusinessBase<string>
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00011510 File Offset: 0x0000F710
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Filename
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

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00011528 File Offset: 0x0000F728
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00011530 File Offset: 0x0000F730
		public FileType FileType { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00011539 File Offset: 0x0000F739
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00011541 File Offset: 0x0000F741
		public int AddressSize { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001154A File Offset: 0x0000F74A
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00011552 File Offset: 0x0000F752
		public string Version { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001155B File Offset: 0x0000F75B
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00011563 File Offset: 0x0000F763
		public eUpdateStatus Status { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001156C File Offset: 0x0000F76C
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00011574 File Offset: 0x0000F774
		public DateTime LastModifiedTime { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001157D File Offset: 0x0000F77D
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00011585 File Offset: 0x0000F785
		public bool IsPublic { get; set; }
	}
}
