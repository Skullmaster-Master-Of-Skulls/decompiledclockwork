using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000595 RID: 1429
	public class MediaVendor : BusinessBase<int>
	{
		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x00033134 File Offset: 0x00031334
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x0000E258 File Offset: 0x0000C458
		public int VendorId
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

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x0003314C File Offset: 0x0003134C
		// (set) Token: 0x06002E75 RID: 11893 RVA: 0x00033154 File Offset: 0x00031354
		public string Name { get; set; }

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x0003315D File Offset: 0x0003135D
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x00033165 File Offset: 0x00031365
		public string Phone { get; set; }

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x0003316E File Offset: 0x0003136E
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x00033176 File Offset: 0x00031376
		public string Cellphone { get; set; }

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x0003317F File Offset: 0x0003137F
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x00033187 File Offset: 0x00031387
		public string Address { get; set; }

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x00033190 File Offset: 0x00031390
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x00033198 File Offset: 0x00031398
		public string Fax { get; set; }

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000331A1 File Offset: 0x000313A1
		// (set) Token: 0x06002E7F RID: 11903 RVA: 0x000331A9 File Offset: 0x000313A9
		public string Email { get; set; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000331B2 File Offset: 0x000313B2
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x000331BA File Offset: 0x000313BA
		public string Website { get; set; }

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000331C3 File Offset: 0x000313C3
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x000331CB File Offset: 0x000313CB
		public string Description { get; set; }

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000331D4 File Offset: 0x000313D4
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x000331DC File Offset: 0x000313DC
		public string Notes { get; set; }
	}
}
