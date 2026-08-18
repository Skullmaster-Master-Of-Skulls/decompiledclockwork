using System;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x02000202 RID: 514
	public class ServiceProviderRequestLegacyInfo
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00016CB4 File Offset: 0x00014EB4
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x00016CBC File Offset: 0x00014EBC
		public bool fsBSWD { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00016CC5 File Offset: 0x00014EC5
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x00016CCD File Offset: 0x00014ECD
		public eServiceProviderRequestDetailLegacyItemStatus fsOsapStatus { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00016CD6 File Offset: 0x00014ED6
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00016CDE File Offset: 0x00014EDE
		public bool fsWSIB { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00016CE7 File Offset: 0x00014EE7
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00016CEF File Offset: 0x00014EEF
		public BinaryFile fsWSIBLetterOfApprovalFile { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00016CF8 File Offset: 0x00014EF8
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x00016D00 File Offset: 0x00014F00
		public string fsWSIBCaseWorkerPhone { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00016D09 File Offset: 0x00014F09
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x00016D11 File Offset: 0x00014F11
		public bool fsFirstNations { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00016D1A File Offset: 0x00014F1A
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x00016D22 File Offset: 0x00014F22
		public BinaryFile fsFirstNationsLetterOfApprovalFile { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00016D2B File Offset: 0x00014F2B
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x00016D33 File Offset: 0x00014F33
		public string fsFirstNationsCaseWorkerPhone { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00016D3C File Offset: 0x00014F3C
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x00016D44 File Offset: 0x00014F44
		public bool fsInterpreterFund { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00016D4D File Offset: 0x00014F4D
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x00016D55 File Offset: 0x00014F55
		public int fsInterpreterFundCode { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00016D5E File Offset: 0x00014F5E
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00016D66 File Offset: 0x00014F66
		public bool fsOther { get; set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00016D6F File Offset: 0x00014F6F
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00016D77 File Offset: 0x00014F77
		public string fsOtherDetail { get; set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00016D80 File Offset: 0x00014F80
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00016D88 File Offset: 0x00014F88
		public eServiceProviderRequestDetailLegacyItemStatus fsBSWDStatus { get; set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00016D91 File Offset: 0x00014F91
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x00016D99 File Offset: 0x00014F99
		public eServiceProviderRequestDetailLegacyItemStatus fsWSIBStatus { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00016DA2 File Offset: 0x00014FA2
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00016DAA File Offset: 0x00014FAA
		public eServiceProviderRequestDetailLegacyItemStatus fsFirstNationsStatus { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00016DB3 File Offset: 0x00014FB3
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00016DBB File Offset: 0x00014FBB
		public eServiceProviderRequestDetailLegacyItemStatus fsInterpreterFundStatus { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00016DC4 File Offset: 0x00014FC4
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00016DCC File Offset: 0x00014FCC
		public eServiceProviderRequestDetailLegacyItemStatus fsOtherStatus { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00016DD5 File Offset: 0x00014FD5
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00016DDD File Offset: 0x00014FDD
		public BinaryFile fsOtherFile { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00016DE6 File Offset: 0x00014FE6
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x00016DEE File Offset: 0x00014FEE
		public DateTime dateentered2 { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00016DF7 File Offset: 0x00014FF7
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x00016DFF File Offset: 0x00014FFF
		public bool fsSsd { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00016E08 File Offset: 0x00015008
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x00016E10 File Offset: 0x00015010
		public eServiceProviderRequestDetailLegacyItemStatus fsSsdStatus { get; set; }
	}
}
