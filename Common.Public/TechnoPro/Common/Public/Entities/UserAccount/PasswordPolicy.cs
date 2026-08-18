using System;

namespace TechnoPro.Common.Public.Entities.UserAccount
{
	// Token: 0x02000137 RID: 311
	public class PasswordPolicy
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x000105EC File Offset: 0x0000E7EC
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x000105F4 File Offset: 0x0000E7F4
		public int MinimumLengthTotal { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x000105FD File Offset: 0x0000E7FD
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00010605 File Offset: 0x0000E805
		public int MinimumLengthLowercase { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001060E File Offset: 0x0000E80E
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00010616 File Offset: 0x0000E816
		public int MinimumLengthUppercase { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001061F File Offset: 0x0000E81F
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00010627 File Offset: 0x0000E827
		public int MinimumLengthSpecialCharacter { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00010630 File Offset: 0x0000E830
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x00010638 File Offset: 0x0000E838
		public int MinimumLengthNumeric { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00010641 File Offset: 0x0000E841
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x00010649 File Offset: 0x0000E849
		public int NumPreviousPasswordsCantUse { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00010652 File Offset: 0x0000E852
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x0001065A File Offset: 0x0000E85A
		public int AutoPasswordExpiryNumDays { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00010663 File Offset: 0x0000E863
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001066B File Offset: 0x0000E86B
		public int MaxFailedAttempts { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00010674 File Offset: 0x0000E874
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001067C File Offset: 0x0000E87C
		public int LockoutDurationMinutes { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00010685 File Offset: 0x0000E885
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001068D File Offset: 0x0000E88D
		public bool EnforcePasswordPolicy { get; set; }
	}
}
