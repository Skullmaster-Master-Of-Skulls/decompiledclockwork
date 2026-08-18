using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001FF RID: 511
	public class ServiceProviderBase : BusinessBase<int>
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00016B9C File Offset: 0x00014D9C
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderId
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

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00016BB4 File Offset: 0x00014DB4
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x00016BBC File Offset: 0x00014DBC
		public string FirstName { get; set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00016BC5 File Offset: 0x00014DC5
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x00016BCD File Offset: 0x00014DCD
		public string LastName { get; set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00016BD6 File Offset: 0x00014DD6
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x00016BDE File Offset: 0x00014DDE
		public string MiddleName { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00016BE7 File Offset: 0x00014DE7
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00016BEF File Offset: 0x00014DEF
		public string StudentNumber { get; set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00016BF8 File Offset: 0x00014DF8
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00016C00 File Offset: 0x00014E00
		public string Username { get; set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00016C09 File Offset: 0x00014E09
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x00016C11 File Offset: 0x00014E11
		public string NickName { get; set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00016C1A File Offset: 0x00014E1A
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x00016C22 File Offset: 0x00014E22
		public bool RegistrationIsComplete { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00016C2B File Offset: 0x00014E2B
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x00016C33 File Offset: 0x00014E33
		public string Email { get; set; }
	}
}
