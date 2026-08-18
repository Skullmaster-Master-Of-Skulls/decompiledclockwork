using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000271 RID: 625
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPProviderDTO
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00006D81 File Offset: 0x00004F81
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00006D89 File Offset: 0x00004F89
		[DataMember]
		public int SPProviderId { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00006D92 File Offset: 0x00004F92
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x00006D9A File Offset: 0x00004F9A
		[DataMember]
		public PersonBaseDTO Person { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00006DA3 File Offset: 0x00004FA3
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x00006DAB File Offset: 0x00004FAB
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00006DB4 File Offset: 0x00004FB4
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x00006DBC File Offset: 0x00004FBC
		[DataMember]
		public string ExternalId { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00006DC5 File Offset: 0x00004FC5
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00006DCD File Offset: 0x00004FCD
		[DataMember]
		public string Specializations { get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00006DD6 File Offset: 0x00004FD6
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00006DDE File Offset: 0x00004FDE
		[DataMember]
		public string Note1 { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00006DE7 File Offset: 0x00004FE7
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00006DEF File Offset: 0x00004FEF
		[DataMember]
		public string Note2 { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00006DF8 File Offset: 0x00004FF8
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00006E00 File Offset: 0x00005000
		[DataMember]
		public string Email { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00006E09 File Offset: 0x00005009
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x00006E11 File Offset: 0x00005011
		[DataMember]
		public string AlternateEmail { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00006E1A File Offset: 0x0000501A
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x00006E22 File Offset: 0x00005022
		[DataMember]
		public string Phone1 { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00006E2B File Offset: 0x0000502B
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x00006E33 File Offset: 0x00005033
		[DataMember]
		public string Phone2 { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00006E3C File Offset: 0x0000503C
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00006E44 File Offset: 0x00005044
		[DataMember]
		public string PhoneNote { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00006E4D File Offset: 0x0000504D
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x00006E55 File Offset: 0x00005055
		[DataMember]
		public string Address1 { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00006E5E File Offset: 0x0000505E
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00006E66 File Offset: 0x00005066
		[DataMember]
		public string Address2 { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00006E6F File Offset: 0x0000506F
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00006E77 File Offset: 0x00005077
		[DataMember]
		public bool Address1IsPrimary { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00006E80 File Offset: 0x00005080
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00006E88 File Offset: 0x00005088
		[DataMember]
		public bool IsActive { get; set; }
	}
}
