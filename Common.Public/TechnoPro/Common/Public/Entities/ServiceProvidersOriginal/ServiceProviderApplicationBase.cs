using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001FB RID: 507
	public class ServiceProviderApplicationBase : BusinessBase<int>
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x000169C0 File Offset: 0x00014BC0
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x000169D8 File Offset: 0x00014BD8
		public new virtual int Id
		{
			get
			{
				return this.ServiceProviderApplicationId;
			}
			set
			{
				this.ServiceProviderApplicationId = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x000169E3 File Offset: 0x00014BE3
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x000169EB File Offset: 0x00014BEB
		public int ServiceProviderApplicationId { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000169F4 File Offset: 0x00014BF4
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x000169FC File Offset: 0x00014BFC
		public int ServiceProviderId { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00016A05 File Offset: 0x00014C05
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00016A0D File Offset: 0x00014C0D
		public ServiceProviderType ProviderType { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00016A16 File Offset: 0x00014C16
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00016A1E File Offset: 0x00014C1E
		public bool IsActive { get; set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00016A27 File Offset: 0x00014C27
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x00016A2F File Offset: 0x00014C2F
		public ServiceProviderApplicationStatus Status { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00016A38 File Offset: 0x00014C38
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x00016A40 File Offset: 0x00014C40
		public string Note1 { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00016A49 File Offset: 0x00014C49
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00016A51 File Offset: 0x00014C51
		public string Note2 { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00016A5A File Offset: 0x00014C5A
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x00016A62 File Offset: 0x00014C62
		public DateTime DateEntered { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00016A6B File Offset: 0x00014C6B
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00016A73 File Offset: 0x00014C73
		public DateTime DateEntered2 { get; set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00016A7C File Offset: 0x00014C7C
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00016A84 File Offset: 0x00014C84
		public bool IsPermanent { get; set; }
	}
}
