using System;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x0200048C RID: 1164
	public class ClockWorkHashAuthentication
	{
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x00026BA5 File Offset: 0x00024DA5
		// (set) Token: 0x06002310 RID: 8976 RVA: 0x00026BAD File Offset: 0x00024DAD
		public string Username { get; set; }

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x00026BB6 File Offset: 0x00024DB6
		// (set) Token: 0x06002312 RID: 8978 RVA: 0x00026BBE File Offset: 0x00024DBE
		public string StampTime { get; set; }

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00026BC7 File Offset: 0x00024DC7
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x00026BCF File Offset: 0x00024DCF
		public string Seed { get; set; }

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x00026BD8 File Offset: 0x00024DD8
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x00026BE0 File Offset: 0x00024DE0
		public string HashValue { get; set; }
	}
}
